using QueryParameters.Entities;
using QueryParameters.Handlers;
using QueryParameters.Properties;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace QueryParameters
{
    public partial class ParameterHandler<T>
    {

        private const string _ParameterIdent = "t";

        public IFilterHandler EqualHandler = new DefaultEqualHandler();
        public IFilterHandler NotEqualHandler = new DefaultNotEqualHandler();
        public IFilterHandler LessThanHandler = new DefaultLessThanHandler();
        public IFilterHandler LessThanEqualHandler = new DefaultLessThanEqualHandler();
        public IFilterHandler GreaterThanHandler = new DefaultGreaterThanHandler();
        public IFilterHandler GreaterThanEqualHandler = new DefaultGreaterThanEqualHandler();
        public IFilterHandler StartsHandler = new DefaultStartsHandler();
        public IFilterHandler EndsHandler = new DefaultEndsHandler();
        public IFilterHandler ContainsHandler = new DefaultContainsHandler();

        public IQueryable<T> ApplyFilter(IQueryable<T> queryable)
        {
            if (Filter == null || !Filter.Elements.Any()) return queryable;

            return ApplyFilterCollection(queryable, Filter.Elements);
        }

        private IQueryable<T> ApplyFilterCollection(IQueryable<T> queryable, FilterElementCollection filterElementCollection)
        {
            var param = Expression.Parameter(typeof(T), _ParameterIdent);
            return queryable.Where(Expression.Lambda<Func<T, bool>>(GetFilterExpression(filterElementCollection, param), param));
        }

        private Expression GetFilterExpression(FilterElementCollection filterElementCollection, ParameterExpression parameterExpression)
        {
            Expression expression = null;

            for (int i = 0; i < filterElementCollection.Count(); i++)
            {
                var filterElement = filterElementCollection[i];

                Expression nestedExpression = null;

                if (filterElement is FilterElementCollection collection)
                {
                    nestedExpression = GetFilterExpression(collection, parameterExpression);
                }
                else if (filterElement is FilterElementExpression filterElementExpression)
                {
                    nestedExpression = GetExpressionFromFilterElement(filterElementExpression, parameterExpression);
                }

                if (nestedExpression != null)
                {
                    if (expression == null)
                    {
                        expression = nestedExpression;
                    }
                    else
                    {
                        var expressionOperator = (FilterElementOperator)filterElementCollection[i - 1];

                        if (expressionOperator == FilterElementOperator.And)
                        {
                            expression = Expression.AndAlso(expression, nestedExpression);
                        }
                        else if (expressionOperator == FilterElementOperator.Or)
                        {
                            expression = Expression.OrElse(expression, nestedExpression);
                        }
                    }
                }
            }

            return expression;
        }

        private Expression GetExpressionFromFilterElement(FilterElementExpression filterElementExpression, ParameterExpression parameterExpression)
        {
            IFilterHandler filterHandler = filterElementExpression.Condition._Constant switch
            {
                FilterElementCondition.Constant.Equal => EqualHandler,
                FilterElementCondition.Constant.NotEqual => NotEqualHandler,
                FilterElementCondition.Constant.LessThan => LessThanHandler,
                FilterElementCondition.Constant.LessThanEqual => LessThanEqualHandler,
                FilterElementCondition.Constant.GreaterThan => GreaterThanHandler,
                FilterElementCondition.Constant.GreaterThanEqual => GreaterThanEqualHandler,
                FilterElementCondition.Constant.Starts => StartsHandler,
                FilterElementCondition.Constant.Ends => EndsHandler,
                FilterElementCondition.Constant.Contains => ContainsHandler,
                _ => throw new NotSupportedException($"'{filterElementExpression.Condition._Constant}' not supported"),
            };
            return filterHandler.GetExpression<T>(filterElementExpression, parameterExpression);
        }

    }
}
