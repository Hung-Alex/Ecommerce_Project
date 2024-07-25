using Application.Common.Exceptions;
using Application.Common.Interface;
using Application.DTOs.Internal;
using Application.Features.Products.Specification;
using Domain.Constants;
using Domain.Entities;
using Domain.Entities.Category;
using Domain.Entities.Products;
using Domain.Shared;
using FluentValidation;
using MediatR;
using System.Data;


namespace Application.Features.Products.Commands.CreateProduct
{
    public sealed class CreateProductCommandHandler(IUnitOfWork unitOfWork, IMedia media) : IRequestHandler<CreateProductCommand, Result<bool>>
    {
        public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
        {

            public CreateProductCommandValidator()
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage(nameof(CreateProductCommand.Name));
                RuleFor(x => x.UrlSlug).NotEmpty().WithMessage(nameof(CreateProductCommand.Name));
                RuleFor(x => x.Description).NotEmpty().WithMessage(nameof(CreateProductCommand.Description));
                RuleFor(x => x.Price).NotEmpty().WithMessage(nameof(CreateProductCommand.Price));
                RuleFor(x => x.BrandId).NotEmpty().WithMessage(nameof(CreateProductCommand.BrandId));
                RuleFor(x => x.CategoryId).NotEmpty().WithMessage(nameof(CreateProductCommand.CategoryId));
            }
        }
        public async Task<Result<bool>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var repoProduct = unitOfWork.GetRepository<Product>();
            var repoImage = unitOfWork.GetRepository<Image>();
            var repoCategory = unitOfWork.GetRepository<Categories>();

            // Check if the URL slug already exists
            var isExisted = await repoProduct.FindOneAsync(new UrlSlugIsExistedSpecification(Guid.Empty, request.UrlSlug));
            if (isExisted is not null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.UrlSlugIsExisted(request.UrlSlug));
            }

            // Verify the category exists
            var category = await repoCategory.GetByIdAsync(request.CategoryId);
            if (category is null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(request.CategoryId));
            }

            // Create and add new product
            var newProduct = new Product
            {
                Name = request.Name,
                Description = request.Description,
                UrlSlug = request.UrlSlug,
                Price = request.Price,
                Discount = request.Discount,
                BrandId = request.BrandId,
                CategoryId = request.CategoryId
            };
            repoProduct.Add(newProduct);

            #region Handle Images
            if (request.Images is not null)
            {
                var uploadTasks = request.Images.Select((item, index) =>
                    media.UploadLoadImageAsync(item, UploadFolderConstants.FolderProduct, cancellationToken).ContinueWith(task =>
                    {
                        if (task.Result.IsSuccess == false)
                        {
                            throw new UploadImageException(task.Result.Errors.Select(x => x.Description).ToList());
                        }
                        var uploadResult = task.Result.Data;
                        return new Image
                        {
                            ImageExtension = item.ContentType,
                            ImageUrl = uploadResult.Url,
                            PublicId = uploadResult.PublicId,
                            OrderItem = index + 1,
                            ProductId = newProduct.Id
                        };
                    })
                ).ToList();

                var images = await Task.WhenAll(uploadTasks);
                foreach (var image in images)
                {
                    repoImage.Add(image);
                }
            }
            #endregion

            #region Handle Variants
            if (request.Variant is not null)
            {
                newProduct.ProductSkus = request.Variant
                    .Select(x => new ProductSkus
                    {
                        Name = x.VariantName,
                        Description = x.Description
                    })
                    .ToList();
            }
            #endregion

            await unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);

        }
    }
}
