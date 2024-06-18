using System.Linq.Expressions;

namespace Application.Utils
{
    public static class PredicatedProperty
    {
        public static bool IsExitedProperty<T>(string propertyName)
        {
            return typeof(T).GetProperties().Any(x => string.Equals(x.Name, propertyName, StringComparison.OrdinalIgnoreCase));
        }
        public static Expression<Func<T,object>> BuildProperty<T>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T), "p");
            var property = Expression.Property(parameter, propertyName);
            var lambda = Expression.Lambda<Func<T, object>>(Expression.Convert(property, typeof(object)), parameter);
            return lambda;
        }
    }
}
