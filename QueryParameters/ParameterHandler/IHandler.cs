using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryParameters
{
    public interface IHandler
    {

        private static ConcurrentDictionary<Type, ConcurrentDictionary<string, Expression>> _PropertyFieldExpressionCache = new();

        internal Expression GetPropertyFieldExpression<T>(string identifier, ParameterExpression parameterExpression)
        {
            var genericTypeArg = typeof(T);

            Expression propertyFieldExpression;

            // Populate the entry for the concurrent dictionary if not already populated
            if (!_PropertyFieldExpressionCache.TryGetValue(genericTypeArg, out var propertyFieldCache))
            {
                propertyFieldCache = new();
                _PropertyFieldExpressionCache.TryAdd(genericTypeArg, propertyFieldCache);
            }

            if (!propertyFieldCache.TryGetValue(identifier, out propertyFieldExpression))
            {
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
                else
                {
                    propertyFieldCache.TryAdd(identifier, propertyFieldExpression);
                }
            }

            return propertyFieldExpression;
        }

    }
}
