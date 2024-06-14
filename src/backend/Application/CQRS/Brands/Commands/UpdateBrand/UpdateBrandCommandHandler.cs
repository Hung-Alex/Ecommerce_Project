using Application.Common.Exceptions;
using Application.Common.Interface;
using Application.CQRS.Brands.Specification;
using Application.DTOs.Internal;
using Application.DTOs.Responses.Brand;
using AutoMapper;
using Domain.Entities.Brands;
using MediatR;

namespace Application.CQRS.Brands.Commands.UpdateBrand
{
    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, BrandDTOs>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMedia _media;
        private readonly IMapper _mapper;
        public UpdateBrandCommandHandler(IUnitOfWork unitOfWork, IMedia media, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _media = media;
            _mapper = mapper;
        }
        public async Task<BrandDTOs> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var brandRepo = _unitOfWork.GetRepository<Brand>();
            var brand = await brandRepo.GetByIdAsync(request.Id);
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
            return _mapper.Map<BrandDTOs>(brand);
        }
    }
}
