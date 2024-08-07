﻿using System.Linq.Expressions;
using Domain.Common;
using Domain.Shared;

namespace Domain.Specifications
{
    public class Or<T>:BaseSpecification<T> where T : BaseEntity,IAggregateRoot
    {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;

        public Or(
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
                    Expression.OrElse(
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
