using System.Linq.Expressions;
using Domain.Common;
using Domain.Shared;

namespace Domain.Specifications
{
    public class And<T> : BaseSpecification<T> where T : BaseEntity, IAggregateRoot
    {
        private ISpecification<T> _left { get; set; }
        private ISpecification<T> _right { get; set; }
        public And(
            ISpecification<T> left,
            ISpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> Criteria
        {
            get
            {
                var objParam = Expression.Parameter(typeof(T), "obj");

                var newExpr = Expression.Lambda<Func<T, bool>>(
                    Expression.AndAlso(
                        Expression.Invoke(_left.Criteria, objParam),
                        Expression.Invoke(_right.Criteria, objParam)
                    ),
                    objParam
                );

                return newExpr;
            }
        }

    }
}
