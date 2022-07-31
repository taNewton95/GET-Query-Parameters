using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryParameters.Handlers
{
    public interface IHandler
    {

        internal Expression GetPropertyFieldExpression<T>(string identifier, ParameterExpression parameterExpression)
        {
            var genericTypeArg = typeof(T);

            Expression propertyFieldExpression = null;

            var property = genericTypeArg.GetProperty(identifier);

            if (property != null)
            {
                propertyFieldExpression = Expression.Property(parameterExpression, identifier);
            }
            else
            {
                var field = genericTypeArg.GetField(identifier);

                if (field != null)
                {
                    propertyFieldExpression = Expression.Field(parameterExpression, identifier);
                }
            }

            if (propertyFieldExpression == null)
            {
                throw new Exception($"Could not find field or property '{identifier}'");
            }

            return propertyFieldExpression;
        }

    }
}
