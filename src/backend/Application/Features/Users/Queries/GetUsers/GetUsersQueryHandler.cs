using Application.Common.Interface;
using Application.DTOs.Responses.Users;
using Application.Features.Users.Specification;
using AutoMapper;
using Domain.Entities.Users;
using Domain.Shared;
using MediatR;

namespace Application.Features.Users.Queries.GetUsers
{
    public class GetUsersQueryHandler:IRequestHandler<GetUsersQuery, Result<IEnumerable<UserDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetUsersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<UserDTO>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var userRepo = _unitOfWork.GetRepository<User>();
            var getUsersSpecification = new GetUsersSpecification(request.Filter);
            var users = await userRepo.GetAllAsync(getUsersSpecification);
            var totalItems = await userRepo.CountAsync(getUsersSpecification);
            return new PagingResult<IEnumerable<UserDTO>>(_mapper.Map<IEnumerable<UserDTO>>(users), request.Filter.PageNumber, request.Filter.PageSize, totalItems);
        }
    }   
}
