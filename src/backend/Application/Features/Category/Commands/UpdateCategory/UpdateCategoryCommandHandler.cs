using Application.Common.Interface;
using Application.DTOs.Internal;
using AutoMapper;
using MediatR;
using FluentValidation;
using Application.DTOs.Responses.Category;
using Domain.Entities.Category;
using Domain.Constants;
using Domain.Shared;

namespace Application.Features.Category.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<CategoryDTO>>
    {
        internal class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
        {
            public UpdateCategoryCommandValidator()
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
        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork, IMedia media, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _media = media;
            _mapper = mapper;
        }
        public async Task<Result<CategoryDTO>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var repoCategory = _unitOfWork.GetRepository<Categories>();
            var category = await repoCategory.GetByIdAsync(request.Id);
            if (category == null) return Result<CategoryDTO>.ResultFailures(ErrorConstants.UserNotFoundWithID(request.Id));
            if (request.ParrentId is not null)
            {
                var isExistedParrent = await repoCategory.GetByIdAsync(request.ParrentId);
                if (isExistedParrent is null)
                {
                    return Result<CategoryDTO>.ResultFailures(ErrorConstants.NotFoundWithId((Guid)request.ParrentId));
                }
            }
            Result<ImageUpload> uploadResult = null;
            if (!(request.Image is null))
            {
                uploadResult = await _media.UploadLoadImageAsync(request.Image, cancellationToken);
            }
            category.UrlSlug = request.UrlSlug;
            category.Name = request.Name;
            category.Description = request.Description;
            category.ParrentId = request.ParrentId;
            if (!(uploadResult is null))
            {
                category.Image = uploadResult.Data.Url;
            }
            await _unitOfWork.Commit();
            var CategoryDTO = _mapper.Map<CategoryDTO>(category);
            return Result<CategoryDTO>.ResultSuccess(CategoryDTO);
        }
    }
}
