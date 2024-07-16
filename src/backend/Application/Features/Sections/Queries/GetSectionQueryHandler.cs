using Application.DTOs.Responses.Sections;
using Domain.Shared;
using MediatR;

namespace Application.Features.Sections.Queries
{
    public sealed class GetSectionQueryHandler() : IRequestHandler<GetSectionsQuery, Result<IEnumerable<SectionDTO>>>
    {
        public async Task<Result<IEnumerable<SectionDTO>>> Handle(GetSectionsQuery request, CancellationToken cancellationToken)
        {
            //var section = await sectionService.GetSectionsAsync(request.TakeCategories, request.TakeItems, cancellationToken);
            //return Result<IEnumerable<SectionDTO>>.ResultSuccess(section);
            return null;
        }
    }
}
