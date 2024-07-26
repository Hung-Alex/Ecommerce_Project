using Application.Common.Interface;
using Application.Common.Interface.IdentityService;
using Application.DTOs.Responses.Users;
using Domain.Constants;
using Domain.Entities.Users;
using Domain.Shared;
using MediatR;


namespace Application.Features.Users.Queries.GetUserById
{
    public sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserDetailDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IIdentityService _identityService;//use to get application User
        public GetUserByIdQueryHandler(IUnitOfWork unitOfWork, IIdentityService identityService)
        {
            _unitOfWork = unitOfWork;
            _identityService = identityService;

        }
        public async Task<Result<UserDetailDTO>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var repoUser = _unitOfWork.GetRepository<User>();
            var userTask = repoUser.GetByIdAsync(request.UserId);
            var applicationUserTask = _identityService.GetApplicationUserByUserIdAsync(request.UserId);
            await Task.WhenAll(userTask, applicationUserTask);
            var user = await userTask;
            var applicationUser = await applicationUserTask;
            if (user is null || applicationUser is null)
            {
                return Result<UserDetailDTO>.ResultFailures(ErrorConstants.NotFoundWithId(request.UserId));
            }
            return Result<UserDetailDTO>.ResultSuccess(
                new UserDetailDTO()
                {
                    Id = user.Id,
                    UserName = applicationUser.UserName,
                    Email = applicationUser.Email,
                    PhoneNumber = applicationUser.PhoneNumber,
                    IsLockout = applicationUser.IsLockout,
                    Region = user.Region,
                    PostalCode = user.PostalCode,
                    City = user.City,
                    LastName = user.LastName,
                    AvatarImage = user.AvatarImage,
                    Roles = applicationUser.Roles
                }
            );
        }
    }
}
