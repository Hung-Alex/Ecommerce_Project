using Domain.Entities.Rattings;
using Domain.Specifications;
using System.Linq.Expressions;

namespace Application.Features.Rattings.Specification
{
    public class GetRattingByIdSepecification : BaseSpecification<Ratting>
    {
        private readonly Guid _id;
        public GetRattingByIdSepecification(Guid id)
        {
            _id = id;
        }
        public override Expression<Func<Ratting, bool>> Criteria => x => x.Id == _id;
    }
}
