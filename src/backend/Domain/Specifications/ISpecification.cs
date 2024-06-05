using System.Linq.Expressions;
using Domain.Common;
using Domain.Shared;

namespace Domain.Specifications
{
    public interface ISpecification<T> where T : BaseEntity,IAggregateRoot
    {
        Expression<Func<T,bool>> Criteria {  get; } 
        IList<Expression<Func<T,object>>> Includes { get; }
        IList<string> IncludeStrings { get; }
        Expression<Func<T,object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }
        Expression<Func<T, object>> GroupBy { get; }
        int Take {  get; }
        int Skip { get; }
        bool IsPagingEnabled { get;}
    }
}
