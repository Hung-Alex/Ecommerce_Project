using Application.Common.Interface;
using Application.Features.Products.Specification;
using Domain.Constants;
using Domain.Entities;
using Domain.Entities.Images;
using Domain.Entities.Products;
using Domain.Interface;
using Domain.Shared;
using FluentValidation;
using MediatR;
using System.Data;


namespace Application.Features.Products.Commands.CreateProduct
{
    public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<bool>>
    {
        public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
        {

            private readonly ICategoryRepository _categoryRepository;
            public CreateProductCommandValidator(ICategoryRepository categoryRepository)
            {
                _categoryRepository = categoryRepository;
                RuleFor(x => x.Name).NotEmpty().WithMessage(nameof(CreateProductCommand.Name));
                RuleFor(x => x.UrlSlug).NotEmpty().WithMessage(nameof(CreateProductCommand.Name));
                RuleFor(x => x.Description).NotEmpty().WithMessage(nameof(CreateProductCommand.Description));
                RuleFor(x => x.Price).NotEmpty().WithMessage(nameof(CreateProductCommand.Price));
                RuleFor(x => x.UnitPrice).NotEmpty().WithMessage(nameof(CreateProductCommand.UnitPrice));
                RuleFor(x => x.BrandId).NotEmpty().WithMessage(nameof(CreateProductCommand.BrandId));
                RuleFor(x => x.Collections).NotNull().WithMessage(nameof(CreateProductCommand.Collections));
                RuleForEach(x => x.Collections)
                        .MustAsync(ValidCategoryCombination)
                        .WithMessage("Subcategory must belong to Parent category and both must exist in the database.");
            }

            private async Task<bool> ValidCategoryCombination(AddCategories category, CancellationToken cancellationToken)
            {
                // Check if ParentId exists
                var parentExists = await _categoryRepository.IsExistedAsync(category.ParrentId);
                if (!parentExists)
                    return false;

                // If SubCategory is not null, check if it exists and belongs to ParentId
                if (category.SubCategoryId.HasValue)
                {
                    var subCategoryExists = await _categoryRepository.IsExistedAsync(category.SubCategoryId.Value);
                    var isSubCategoryValid = await _categoryRepository.IsSubCategoryOfParrentAsync(category.SubCategoryId.Value, category.ParrentId);
                    return subCategoryExists && isSubCategoryValid;
                }

                return true;
            }
        }
        private readonly IMedia _media;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductCommandHandler(IMedia media, IUnitOfWork unitOfWork)
        {
            _media = media;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var repoProduct = _unitOfWork.GetRepository<Product>();
            var repoImage = _unitOfWork.GetRepository<Image>();
            var isExisted = await repoProduct.FindOneAsync(new UrlSlugIsExistedSpecification(Guid.Empty, request.UrlSlug));
            if (isExisted != null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.UrlSlugIsExisted(request.UrlSlug));
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
            };
            repoProduct.Add(newProduct);

            #region hanle Images
            var image = new Image();
            #endregion
            if (request.Variant is not null)
            {
                newProduct.ProductSkus = request
               .Variant
               .Select(x => new ProductSkus() { Name = x.VariantName, Description = x.Description })
               .ToList();
            }
            await _unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
