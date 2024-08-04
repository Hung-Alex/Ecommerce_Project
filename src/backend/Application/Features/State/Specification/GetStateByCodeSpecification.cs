using Domain.Entities;
using Domain.Specifications;
using System.Linq.Expressions;

namespace Application.Features.State.Specification
{
    public class GetStateByCodeSpecification : BaseSpecification<Status>
    {
        private readonly string _code;
        public GetStateByCodeSpecification(string code)
        {
            _code = code;
        }
        public override Expression<Func<Status, bool>> Criteria => s => s.Code == _code;
    }
}
