using Domain.Entities.Category;
using Domain.Specifications;
using System.Linq.Expressions;

namespace Application.Features.Category.Specification
{
    public class ContainsNameSpecification : BaseSpecification<Categories>
    {
        private readonly string _name;
        public ContainsNameSpecification(string name)
        {
            _name = name;
        }
        public override Expression<Func<Categories, bool>> Criteria => b => b.Name.Contains(_name);
    }
}
