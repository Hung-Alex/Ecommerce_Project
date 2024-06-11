using Application.Common.Exceptions;
using Application.Common.Interface;
using Application.CQRS.Brands.Specification;
using Domain.Entities.Brands;
using MediatR;


namespace Application.CQRS.Brands.Commands.CreateBrand
{
    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand>
    {
        private readonly IMedia _media;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBrandCommandHandler(IMedia media, IUnitOfWork unitOfWork)
        {
            _media = media;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var repoBranch = _unitOfWork.GetRepository<Brand>();
            var isExisted = await repoBranch.FindOneAsync(new UrlSlugIsExistedSpecification(Guid.Empty, request.UrlSlug));
            if (isExisted != null) throw new ConflictException($"Url slug {request.UrlSlug} is existed");
            var image = await _media.UploadLoadImageAsync(request.FormFile);
            repoBranch.Add(new Brand() { Name = request.Name, Description = request.Description, UrlSlug = request.UrlSlug, LogoImageUrl = image.Url });
            await _unitOfWork.Commit();
        }
    }
}
