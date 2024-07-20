using Application.DTOs.Filters.Rattings;
using Application.DTOs.Responses.Rattings;
using Domain.Shared;
using MediatR;

namespace Application.Features.Rattings.Queries.GetRattingProductById
{
    public record GetRattingProductByIdQuery(RattingFilter Filter) : IRequest<Result<IEnumerable<RattingDTO>>>;
}
