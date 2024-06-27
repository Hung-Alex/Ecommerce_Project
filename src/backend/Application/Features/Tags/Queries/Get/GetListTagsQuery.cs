using Application.DTOs.Filters.Tags;
using Application.DTOs.Responses.Tags;
using Domain.Shared;
using MediatR;

namespace Application.Features.Tags.Queries.Get
{
    public record GetListTagsQuery(TagFilter TagFilter) : IRequest<Result<IEnumerable<TagDTO>>>;
}
