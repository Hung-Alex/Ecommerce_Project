using Domain.Entities.Tags;
using Domain.Specifications;
using System.Linq.Expressions;

namespace Application.Features.Tags.Specification
{
    public class GetTagByIdSepecification : BaseSpecification<Tag>
    {
        private readonly Guid _id;
        public GetTagByIdSepecification(Guid id)
        {
            _id = id;
        }
        public override Expression<Func<Tag, bool>> Criteria => x => x.Id == _id;
    }
}
