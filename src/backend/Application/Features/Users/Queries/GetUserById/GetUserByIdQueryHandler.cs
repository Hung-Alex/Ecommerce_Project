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
        private readonly IIdentityService _identityService;//use to get application User
        private readonly IMedia _media;
        public GetUserByIdQueryHandler(IUnitOfWork unitOfWork, IIdentityService identityService, IMedia media)
        {
            _unitOfWork = unitOfWork;
            _identityService = identityService;
            _media = media;
        }
        public async Task<Result<UserDetailDTO>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var repoUser = _unitOfWork.GetRepository<User>();
            var user= await repoUser.GetByIdAsync(request.UserId);
            var applicationUser =  await _identityService.GetApplicationUserByUserIdAsync(request.UserId);       
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
                    City = user.City,
                    FristName = user.FirstName,
                    LastName = user.LastName,
                    AvatarImage = _media.GetUrl(user.AvatarImage),
                    Roles = applicationUser.Roles
                }
            );
        }
    }
}
