using Application.DTOs.Filters.State;
using Application.DTOs.Responses.State;
using Domain.Shared;
using MediatR;

namespace Application.Features.State.Queries
{
    public record GetStateQuery(StateFilter filter) : IRequest<Result<IEnumerable<StateDTO>>>;
}
