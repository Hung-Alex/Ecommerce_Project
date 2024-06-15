using Domain.Entities.Brands;
using Domain.Specifications;
using System.Linq.Expressions;

namespace Application.Features.Brands.Specification
{
    public class GetBrandByIdSepecification : BaseSpecification<Brand>
    {
        private readonly Guid _id;
        public GetBrandByIdSepecification(Guid id)
        {
            _id = id;
        }
        public override Expression<Func<Brand, bool>> Criteria => x => x.Id == _id;
    }
}
