using Application.Common.Exceptions;
using Application.Common.Interface;
using Application.DTOs.Internal;
using Application.Features.Products.Specification;
using Domain.Constants;
using Domain.Entities;
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
            var repoImage = _unitOfWork.GetRepository<Image>();
            if (request.File is not null)
            {
                Result<ImageUpload> uploadResult = null;
                int itemOrder = product.Images.Count();
                foreach (var item in request.File)
                {
                    uploadResult = await _media.UploadLoadImageAsync(item, UploadFolderConstants.FolderProduct, cancellationToken);
                    if (uploadResult.IsSuccess is false)
                    {
                        throw new UploadImageException(uploadResult.Errors.Select(x => x.Description).ToList());
                    }
                    repoImage.Add(new Image()
                    {
                        ImageExtension = item.ContentType
                    ,
                        ImageUrl = uploadResult.Data.Url
                    ,
                        PublicId = uploadResult.Data.PublicId
                    ,
                        OrderItem = itemOrder
                    ,
                        ProductId = request.ProductId
                    });
                    itemOrder++;
                }
            }
            await _unitOfWork.CommitAsync();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
