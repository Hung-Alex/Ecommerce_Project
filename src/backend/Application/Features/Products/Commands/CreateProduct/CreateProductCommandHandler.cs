using Application.Common.Exceptions;
using Application.Common.Interface;
using Application.Features.Brands.Commands.CreateBrands;
using Application.Features.Products.Specification;
using Application.Utils;
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
                RuleFor(x => x.Description).NotEmpty().WithMessage(nameof(CreateProductCommand.Description));
                RuleFor(x => x.Price).NotEmpty().WithMessage(nameof(CreateProductCommand.Price));
                RuleFor(x => x.BrandId).NotEmpty().WithMessage(nameof(CreateProductCommand.BrandId));
                RuleFor(x => x.CategoryId).NotEmpty().WithMessage(nameof(CreateProductCommand.CategoryId));
                RuleFor(x => x.UrlSlug)
                     .NotEmpty()
                     .WithMessage(nameof(CreateBrandCommand.UrlSlug))
                     .MustAsync(ValidationExtension.ValidateSlug)
                     .WithMessage(ErrorConstants.UrlSlugInvalid.Description);
            }
        }
        public async Task<Result<bool>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var repoProduct = unitOfWork.GetRepository<Product>();
            var repoImage = unitOfWork.GetRepository<Image>();
            var repoCategory = unitOfWork.GetRepository<Categories>();
            var isExisted = await repoProduct.FindOneAsync(new UrlSlugIsExistedSpecification(Guid.Empty, request.UrlSlug));
            if (isExisted is not null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.UrlSlugIsExisted(request.UrlSlug));
            }
            var category = await repoCategory.GetByIdAsync(request.CategoryId);
            if (category is null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(request.CategoryId));
            }
            var newProduct = new Product()
            {
                Name = request.Name
            ,
                Description = request.Description
            ,
                UrlSlug = request.UrlSlug
            ,
                Price = request.Price
            ,
                Discount = request.Discount
            ,
                BrandId = request.BrandId
            ,
                OldPrice = request.OldPrice
            ,
                CategoryId = request.CategoryId
            ,
                IsStock = request.IsStock
            };
            repoProduct.Add(newProduct);

            if (request.Images is not null)
            {
                var imageTasks = request.Images.Select(async (item, index) =>
                {
                    var uploadResult = await media.UploadLoadImageAsync(item, UploadFolderConstants.FolderProduct, cancellationToken);
                    if (!uploadResult.IsSuccess)
                    {
                        throw new UploadImageException(uploadResult.Errors.Select(x => x.Description).ToList());
                    }
                    repoImage.Add(new Image
                    {
                        ImageExtension = item.ContentType,
                        ImageUrl = uploadResult.Data.Url,
                        PublicId = uploadResult.Data.PublicId,
                        OrderItem = index + 1,
                        ProductId = newProduct.Id
                    });
                });
                await Task.WhenAll(imageTasks);
            }

            await unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
