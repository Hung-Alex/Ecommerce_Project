using Application.Common.Exceptions;
using Application.Common.Interface;
using Application.Features.Brands.Specification;
using Application.DTOs.Internal;
using Application.DTOs.Responses.Brand;
using AutoMapper;
using Domain.Entities.Brands;
using MediatR;
using FluentValidation;
using Application.DTOs.Responses.Product;

namespace Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDTO>
    {
        internal class UpdateBrandCommandValidator : AbstractValidator<UpdateProductCommand>
        {
            public UpdateBrandCommandValidator()
            {
                RuleFor(x => x.Id).NotEmpty().WithMessage("Not Null");
                RuleFor(b => b.Name).NotEmpty().WithMessage("Not Null");
                RuleFor(b => b.Description).NotEmpty().WithMessage("Not Null");
                RuleFor(b => b.UrlSlug).NotEmpty().WithMessage("Not Null");
            }
        }
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMedia _media;
        private readonly IMapper _mapper;
        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMedia media, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _media = media;
            _mapper = mapper;
        }
        public async Task<ProductDTO> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productRepo = _unitOfWork.GetRepository<Brand>();
            var brand = await productRepo.GetByIdAsync(request.Id);
            if (brand == null) throw new NotFoundException("");
            ImageUpload uploadResult = null;
            if (!(request.Image is null))
            {
                uploadResult = await _media.UploadLoadImageAsync(request.Image, cancellationToken);
            }
            brand.UrlSlug = request.UrlSlug;
            brand.Name = request.Name;
            brand.Description = request.Description;
            if (!(uploadResult is null))
            {
                brand.LogoImageUrl = uploadResult.Url;
            }
            await _unitOfWork.Commit();
            return _mapper.Map<ProductDTO>(brand);
        }
    }
}
