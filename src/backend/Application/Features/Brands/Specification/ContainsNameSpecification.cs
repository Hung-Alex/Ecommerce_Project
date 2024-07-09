using Domain.Entities.Brands;
using Domain.Specifications;
using System.Linq.Expressions;

namespace Application.Features.Brands.Specification
{
    public class ContainsNameSpecification : BaseSpecification<Brand>
    {
        private readonly string _name;
        public ContainsNameSpecification(string name)
        {
            _name = name;
        }
        public override Expression<Func<Brand, bool>> Criteria => b => b.Name.Contains(_name);
    }
}
