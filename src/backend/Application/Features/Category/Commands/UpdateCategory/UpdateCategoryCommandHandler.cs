using Application.Common.Exceptions;
using Application.Common.Interface;
using Application.DTOs.Internal;
using AutoMapper;
using MediatR;
using FluentValidation;
using Application.DTOs.Responses.Category;
using Domain.Entities.Category;
using Domain.Constants;

namespace Application.Features.Category.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryDTO>
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
        public async Task<CategoryDTO> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var repoCategory = _unitOfWork.GetRepository<Categories>();
            var category = await repoCategory.GetByIdAsync(request.Id);
            if (category == null) throw new NotFoundException(ErrorConstants.NotFound + request.Id);
            ImageUpload uploadResult = null;
            if (!(request.Image is null))
            {
                uploadResult = await _media.UploadLoadImageAsync(request.Image, cancellationToken);
            }
            category.UrlSlug = request.UrlSlug;
            category.Name = request.Name;
            category.Description = request.Description;
            if (!(uploadResult is null))
            {
                category.Image = uploadResult.Url;
            }
            await _unitOfWork.Commit();
            return _mapper.Map<CategoryDTO>(category);
        }
    }
}
