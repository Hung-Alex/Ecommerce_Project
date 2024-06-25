using Application.DTOs.Responses.Tags;
using Domain.Shared;
using MediatR;

namespace Application.Features.Tags.Queries.GetById
{
    public record GetTagByIdQuery(Guid Id) : IRequest<Result<TagDTO>>;
}
