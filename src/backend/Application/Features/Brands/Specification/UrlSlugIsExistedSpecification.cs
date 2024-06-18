using Domain.Entities.Brands;
using Domain.Specifications;
using System.Linq.Expressions;


namespace Application.Features.Brands.Specification
{
    public class UrlSlugIsExistedSpecification : BaseSpecification<Brand>
    {
        private readonly string _slug;
        private readonly Guid _id;
        public UrlSlugIsExistedSpecification(Guid id,string urlslug) 
        {
            _id = id;
            _slug = urlslug;
        }
        public override Expression<Func<Brand, bool>> Criteria=>p=>p.Id!=_id &&p.UrlSlug==_slug ;
    }
}
