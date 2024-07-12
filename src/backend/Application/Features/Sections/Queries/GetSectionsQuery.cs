
using Application.DTOs.Responses.Sections;
using Domain.Shared;
using MediatR;

namespace Application.Features.Sections.Queries
{
    public record GetSectionsQuery(int TakeCategories, int TakeItems) : IRequest<Result<IEnumerable<SectionDTO>>>;
}
