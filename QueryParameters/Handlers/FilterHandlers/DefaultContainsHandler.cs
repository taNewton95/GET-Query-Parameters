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
    public class DefaultContainsHandler : IFilterHandler
    {

        private readonly MethodInfo ContainsMethodInfo;

        public DefaultContainsHandler()
        {
            ContainsMethodInfo = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) });
        }

        public Expression GetExpression<T>(FilterElementExpression filterElementExpression, ParameterExpression parameterExpression)
        {
            var propertyFieldExpression = ((IHandler)this).GetPropertyFieldExpression<T>(filterElementExpression.Identifier.Identifier, parameterExpression);
            return Expression.Call(propertyFieldExpression, ContainsMethodInfo, Expression.Constant(filterElementExpression.Value.Value));
        }

    }
}
