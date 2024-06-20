using Domain.Entities.Category;
using Domain.Specifications;
using System.Linq.Expressions;

namespace Application.Features.Category.Specification
{
    public class GetCategoryByIdSepecification : BaseSpecification<Categories>
    {
        private readonly Guid _id;
        public GetCategoryByIdSepecification(Guid id)
        {
            _id = id;
        }
        public override Expression<Func<Categories, bool>> Criteria => x => x.Id == _id;
    }
}
