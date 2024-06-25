using Application.Common.Interface;
using Application.Features.Products.Specification;
using Domain.Constants;
using Domain.Entities.Images;
using Domain.Entities.Products;
using Domain.Shared;
using MediatR;

namespace Application.Features.Products.Commands.AddProductImage
{
    public sealed class AddProductImageCommandHandler : IRequestHandler<AddProductImageCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMedia _media;
        public AddProductImageCommandHandler(IUnitOfWork unitOfWork, IMedia media)
        {
            _unitOfWork = unitOfWork;
            _media = media;
        }
        public async Task<Result<bool>> Handle(AddProductImageCommand request, CancellationToken cancellationToken)
        {
            var repoProduct = _unitOfWork.GetRepository<Product>();
            var product = await repoProduct.FindOneAsync(new GetProductByIdSepecification(request.ProductId));
            if (product == null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(request.ProductId));

            }
            var uploadImage = await _media.UploadLoadImageAsync(request.File);
            if (uploadImage.IsSuccess is false)
            {
                return Result<bool>.ResultFailures(uploadImage.Errors);

            }
            var repoImage = _unitOfWork.GetRepository<Image>();
            var image = new Image()
            {
                ImageUrl = uploadImage.Data.Url
                ,
                PublicId = uploadImage.Data.PublicId
                ,
                ImageExtension = request.File.ContentType
            };
            repoImage.Add(image);
            var productImage = new ProductImages()
            {
                ImageId = image.Id
            ,
                ProductId = product.Id
            };
            product.Images.Add(productImage);
            await _unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
