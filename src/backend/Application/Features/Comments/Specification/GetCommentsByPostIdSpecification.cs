using Domain.Entities.Comments;
using Domain.Specifications;
using System.Linq.Expressions;

namespace Application.Features.Comments.Specification
{
    internal class GetCommentsByPostIdSpecification : BaseSpecification<Comment>
    {
        private readonly Guid _productId;
        public GetCommentsByPostIdSpecification(Guid productId)
        {
            _productId = productId;
            AddInclude(x => x.CreatedByUser);
        }
        public override Expression<Func<Comment, bool>> Criteria => r => r.PostId == _productId;
    }
}
