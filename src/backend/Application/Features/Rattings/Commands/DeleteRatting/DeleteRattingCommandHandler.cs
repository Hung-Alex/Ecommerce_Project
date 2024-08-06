using Application.Common.Interface;
using Domain.Constants;
using Domain.Entities.Rattings;
using Domain.Shared;
using MediatR;

namespace Application.Features.Rattings.Commands.DeleteRatting
{
    public sealed class DeleteRattingCommandHandler : IRequestHandler<DeleteRattingCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteRattingCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(DeleteRattingCommand request, CancellationToken cancellationToken)
        {
            var repoRatting = _unitOfWork.GetRepository<Ratting>();
            var ratting = await repoRatting.GetByIdAsync(request.Id);
            if (ratting == null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(request.Id));
            }
            repoRatting.Delete(ratting);
            await _unitOfWork.CommitAsync();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
