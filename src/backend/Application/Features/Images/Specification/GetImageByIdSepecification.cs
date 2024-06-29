using Domain.Entities.Images;
using Domain.Specifications;
using System.Linq.Expressions;

namespace Application.Features.Images.Specification
{
    public class GetImageByIdSepecification : BaseSpecification<Image>
    {
        private readonly Guid _id;
        public GetImageByIdSepecification(Guid id)
        {
            _id = id;
        }
        public override Expression<Func<Image, bool>> Criteria => x => x.Id == _id;
    }
}
