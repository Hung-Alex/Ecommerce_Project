using Application.Common.Interface;
using Application.Features.Products.Specification;
using Domain.Constants;
using Domain.Entities.Products;
using Domain.Shared;
using FluentValidation;
using MediatR;


namespace Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<bool>>
    {
        internal class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommandValidator>
        {

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
            var isExisted = await repoProduct.FindOneAsync(new UrlSlugIsExistedSpecification(Guid.Empty, request.UrlSlug));
            if (isExisted != null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.UrlSlugIsExisted(request.UrlSlug));
            }
            repoProduct.Add(new Product()
            {
                Name = request.Name
            ,
                Description = request.Description
            ,
                UrlSlug = request.UrlSlug
            ,
                Price = request.Price
            ,
                UnitPrice = request.UnitPrice
            ,
                Discount = request.Discount
            });
            await _unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
