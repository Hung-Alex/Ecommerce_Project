using Domain.Entities.Brands;
using Domain.Specifications;
using System.Linq.Expressions;

namespace Application.CQRS.Brands.Specification
{
    public class GetBrandsSpecification : BaseSpecification<Brand>
    {
        private readonly string _name;
        public GetBrandsSpecification(int pageNumber, int pageSize, string Name)
        {
            _name = Name;
            ApplyPaging(pageSize, pageNumber - 1);

        }
        public override Expression<Func<Brand, bool>> Criteria => b => b.Name.Contains(_name);
    }
}
