using System.Linq.Expressions;

namespace Domain.Specifications
{
    public static class PredicatedBuilder
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> sourc, Expression<Func<T, bool>> expression)
        {
            // get param to replace for express 2 
            ParameterExpression parameterSourc = sourc.Parameters[0];
            var replace = new ReplaceParameterExpressionVisistor(expression.Parameters[0], parameterSourc);
            var expressionBody = replace.Visit(expression.Body);
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(sourc.Body, expressionBody), parameterSourc);
        }
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> sourc, Expression<Func<T, bool>> expression)
        {
            // get param to replace for express 2 
            ParameterExpression parameterSourc = sourc.Parameters[0];
            var replace = new ReplaceParameterExpressionVisistor(expression.Parameters[0], parameterSourc);
            var expressionBody = replace.Visit(expression.Body);
            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(sourc.Body, expressionBody), parameterSourc);
        }
    }
    public class ReplaceParameterExpressionVisistor : ExpressionVisitor
    {
        private readonly Expression _oldExpression;
        private readonly Expression _newExpression;
        public ReplaceParameterExpressionVisistor(Expression oldExpression, Expression newExpression)
        {
            _oldExpression = oldExpression;
            _newExpression = newExpression;
        }
        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (ReferenceEquals(node, _oldExpression)) return _newExpression;
            return base.VisitParameter(node);
        }
    }
}
