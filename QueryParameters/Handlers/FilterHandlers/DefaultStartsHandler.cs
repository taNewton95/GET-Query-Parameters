using QueryParameters.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QueryParameters.Handlers
{
    public class DefaultStartsHandler : IFilterHandler
    {

        private readonly MethodInfo StartsWithMethodInfo;

        public DefaultStartsHandler()
        {
            StartsWithMethodInfo = typeof(string).GetMethod(nameof(string.StartsWith), new[] { typeof(string) });
        }

        public Expression GetExpression<T>(FilterElementExpression filterElementExpression, ParameterExpression parameterExpression)
        {
            var propertyFieldExpression = ((IHandler)this).GetPropertyFieldExpression<T>(filterElementExpression.Identifier.Identifier, parameterExpression);
            return Expression.Call(propertyFieldExpression, StartsWithMethodInfo, Expression.Constant(filterElementExpression.Value.Value));
        }

    }
}
