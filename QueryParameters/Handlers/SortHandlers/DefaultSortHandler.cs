using QueryParameters.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryParameters.Handlers
{
    internal class DefaultSortHandler : ISortHandler
    {

        public Expression<Func<T, object>> GetOrderByExpression<T>(SortElementExpression sortElementExpression)
        {
            var param = Expression.Parameter(typeof(T), "t");
            var propertyFieldExpression = ((IHandler)this).GetPropertyFieldExpression<T>(sortElementExpression.Identifier.Identifier, param);
            return Expression.Lambda<Func<T, object>>(Expression.Convert(propertyFieldExpression, typeof(object)), param);
        }

    }
}
