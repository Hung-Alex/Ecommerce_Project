using Application.Common.Interface;
using Domain.Constants;
using Domain.Entities.Tags;
using Domain.Shared;
using MediatR;

namespace Application.Features.Tags.Commands.DeleteTag
{
    public sealed class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteTagCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            var repoTag = _unitOfWork.GetRepository<Tag>();
            var Tag = await repoTag.GetByIdAsync(request.Id);
            if (Tag == null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(request.Id));
            }
            repoTag.Delete(Tag);
            await _unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
