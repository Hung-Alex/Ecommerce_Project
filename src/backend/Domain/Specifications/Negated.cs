using System.Linq.Expressions;
using Domain.Common;
using Domain.Shared;

namespace Domain.Specifications
{
    public class NotSpecification<T>:BaseSpecification<T> where T : BaseEntity,IAggregateRoot
    {
        private readonly ISpecification<T> _inner;

        public NotSpecification(ISpecification<T> inner)
        {
            _inner = inner;
        }

        // NegatedSpecification
        public override Expression<Func<T, bool>> Criteria
        {
            get
            {
                var objParam = Expression.Parameter(typeof(T), "obj");

                var newExpr = Expression.Lambda<Func<T, bool>>(
                    Expression.Not(
                        Expression.Invoke(_inner.Criteria, objParam)
                    ),
                    objParam
                );

                return newExpr;
            }
        }
    }
}
