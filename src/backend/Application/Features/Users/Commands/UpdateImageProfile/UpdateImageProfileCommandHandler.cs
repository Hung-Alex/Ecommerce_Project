using Application.Common.Interface;
using Domain.Constants;
using Domain.Entities.Users;
using Domain.Shared;
using MediatR;

namespace Application.Features.Users.Commands.UpdateImageProfile
{
    public sealed class UpdateImageProfileCommandHandler(IUnitOfWork unitOfWork, IMedia media) : IRequestHandler<UpdateImageProfileCommand, Result<bool>>
    {

        public async Task<Result<bool>> Handle(UpdateImageProfileCommand request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepository<User>();
            var user = await repo.GetByIdAsync(request.UserId);
            if (user == null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.UserError.UserNotFoundWithID(request.UserId));
            }
            
            throw new NotImplementedException();
        }
    }
}
