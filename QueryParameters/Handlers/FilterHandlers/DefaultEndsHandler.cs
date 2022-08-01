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
    public class DefaultEndsHandler : IFilterHandler
    {

        private readonly MethodInfo EndsWithMethodInfo;

        public DefaultEndsHandler()
        {
            EndsWithMethodInfo = typeof(string).GetMethod(nameof(string.EndsWith), new[] { typeof(string) });
        }

        public Expression GetExpression<T>(FilterElementExpression filterElementExpression, ParameterExpression parameterExpression)
        {
            var propertyFieldExpression = ((IHandler)this).GetPropertyFieldExpression<T>(filterElementExpression.Identifier.Identifier, parameterExpression);
            return Expression.Call(propertyFieldExpression, EndsWithMethodInfo, Expression.Constant(filterElementExpression.Value.Value));
        }

    }
}
