using Application.Common.Interface;
using Application.Features.Tags.Specification;
using Domain.Constants;
using Domain.Entities.Tags;
using Domain.Shared;
using FluentValidation;
using MediatR;


namespace Application.Features.Tags.Commands.CreateTag
{
    public sealed class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, Result<bool>>
    {
        internal class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommandValidator>
        {

        }
        private readonly IUnitOfWork _unitOfWork;

        public CreateTagCommandHandler(IMedia media, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            var repoTag = _unitOfWork.GetRepository<Tag>();
            var isExisted = await repoTag.FindOneAsync(new UrlSlugIsExistedSpecification(Guid.Empty, request.UrlSlug));
            if (isExisted != null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.UrlSlugIsExisted(request.UrlSlug));
            }
            repoTag.Add(new Tag() { Name = request.Name, Description = request.Description, UrlSlug = request.UrlSlug });
            await _unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
