using Application.Common.Interface;
using Domain.Constants;
using Domain.Entities.Comments;
using Domain.Entities.Posts;
using Domain.Shared;
using FluentValidation;
using MediatR;

namespace Application.Features.Comments.Commands
{
    public class AddCommentCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddCommentCommand, Result<bool>>
    {
        internal class AddCommentCommandValidator : AbstractValidator<AddCommentCommand>
        {
            public AddCommentCommandValidator()
            {
                RuleFor(c => c.Content)
                    .NotEmpty().WithMessage(nameof(AddCommentCommand.Content))
                    .MaximumLength(200).WithMessage("Content has MaxLength 200 char");
            }
        }
        public async Task<Result<bool>> Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {
            var repoComment = unitOfWork.GetRepository<Comment>();
            var repoPost = unitOfWork.GetRepository<Post>();
            var post = await repoPost.GetByIdAsync(request.PostId);
            if (post is null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(request.PostId));
            }
            repoComment.Add(new Comment { PostId = request.PostId, Content = request.Content });
            await unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
