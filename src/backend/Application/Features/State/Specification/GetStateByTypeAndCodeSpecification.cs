using Domain.Entities;
using Domain.Specifications;
using System.Linq.Expressions;

namespace Application.Features.State.Specification
{
    public class GetStateByTypeAndCodeSpecification : BaseSpecification<Status>
    {
        private readonly string _type;
        private readonly string _code;
        public GetStateByTypeAndCodeSpecification(string type, string code)
        {
            _type = type;
            _code = code;
        }
        public override Expression<Func<Status, bool>> Criteria => s => s.Type == _type && s.Code == _code;
    }
}
