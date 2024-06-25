using Domain.Entities.Tags;
using Domain.Specifications;
using System.Linq.Expressions;

namespace Application.Features.Tags.Specification
{
    public class ContainsNameSpecification : BaseSpecification<Tag>
    {
        private readonly string _name;
        public ContainsNameSpecification(string name)
        {
            _name = name;
        }
        public override Expression<Func<Tag, bool>> Criteria => b => b.Name.Contains(_name);
    }
}
