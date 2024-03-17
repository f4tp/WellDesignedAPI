using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WellDesignedAPI.EntityFramework.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> query, string sortBy, bool sortAscending)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, sortBy);
            var selector = Expression.Lambda(property, parameter);

            var methodName = sortAscending ? "OrderBy" : "OrderByDescending";
            var methodCall = Expression.Call(
                typeof(Queryable),
                methodName,
                new[] { typeof(T), property.Type },
                query.Expression,
                Expression.Quote(selector)
            );

            return query.Provider.CreateQuery<T>(methodCall);
        }
    }
}
