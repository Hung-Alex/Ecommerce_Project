using Application.Common.Interface;
using AutoMapper;
using MediatR;
using FluentValidation;
using Domain.Constants;
using Domain.Shared;
using Application.DTOs.Responses.Tags;
using Domain.Entities.Tags;

namespace Application.Features.Tags.Commands.UpdateTag;
public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand, Result<TagDTO>>
{
    internal class UpdateCategoryCommandValidator : AbstractValidator<UpdateTagCommand>
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
    private readonly IMapper _mapper;
    public UpdateTagCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Result<TagDTO>> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        var repoTag = _unitOfWork.GetRepository<Tag>();
        var tag = await repoTag.GetByIdAsync(request.Id);
        if (tag == null) return Result<TagDTO>.ResultFailures(ErrorConstants.UserNotFoundWithID(request.Id));
        tag.UrlSlug = request.UrlSlug;
        tag.Name = request.Name;
        tag.Description = request.Description;
        await _unitOfWork.Commit();
        var CategoryDTO = _mapper.Map<TagDTO>(tag);
        return Result<TagDTO>.ResultSuccess(CategoryDTO);
    }
}

