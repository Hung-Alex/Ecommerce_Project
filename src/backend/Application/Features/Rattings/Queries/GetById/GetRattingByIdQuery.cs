using Application.DTOs.Responses.Rattings;
using Domain.Shared;
using MediatR;

namespace Application.Features.Rattings.Queries.GetById
{
    public record GetRattingByIdQuery(Guid Id) : IRequest<Result<RattingDTO>>;
}
