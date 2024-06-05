using System.Linq.Expressions;
using Domain.Common;
using Domain.Shared;

namespace Domain.Specifications
{
    public abstract class BaseSpecification<T> : ISpecification<T> where T : BaseEntity, IAggregateRoot
    {
        public abstract Expression<Func<T, bool>> Criteria { get; }
        public IList<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public IList<string> IncludeStrings { get; } = new List<string>();
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }
        public Expression<Func<T, object>> GroupBy { get; private set; }
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; } = false;
        protected virtual void AddInclude(Expression<Func<T, object>> include)
        {
            Includes.Add(include);
        }
        protected virtual void AddIncludeString(string include)
        {
            IncludeStrings.Add(include);
        }
        protected virtual void ApplyOrderBy(Expression<Func<T, object>> orderBy)
        {
            OrderBy = orderBy;
        }
        protected virtual void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescending)
        {
            OrderByDescending = orderByDescending;
        }
        protected virtual void ApplyPaging(int take, int skip)
        {
            Take = take;
            Skip = skip - 1;
            IsPagingEnabled = true;
        }
        protected virtual void ApplyGroupBy(Expression<Func<T, object>> groupBy)
        {
            GroupBy = groupBy;
        }
    }
}
