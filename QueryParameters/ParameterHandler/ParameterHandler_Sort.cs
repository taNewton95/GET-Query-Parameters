using QueryParameters.Entities;
using QueryParameters.Parameters;
using QueryParameters.Properties;
using QueryParameters.Settings;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryParameters
{
    public partial class ParameterHandler<T>
    {
        
        public IQueryable<T> ApplySort(IQueryable<T> queryable)
        {
            if (Sort == null || !Sort.Any()) return queryable;

            IOrderedQueryable<T> orderedQueryable = null;

            foreach (var item in Sort)
            {
                var fieldType = PropertyFactory.FieldOrPropertyType<T>(item.Field);

                if (fieldType == null) continue;

                void ApplySort_Internal<TMember>()
                {
                    if (orderedQueryable == null)
                    {
                        orderedQueryable = ApplySort(queryable, PropertyFactory.FieldOrPropertyExpression<T, TMember>(item.Field), item.Direction);
                    }
                    else
                    {
                        orderedQueryable = ApplySort(orderedQueryable, PropertyFactory.FieldOrPropertyExpression<T, TMember>(item.Field), item.Direction);
                    }

                    queryable = orderedQueryable;
                }

                var @switch = new Dictionary<Type, Action> {
                    { typeof(short), ApplySort_Internal<short> },
                    { typeof(short?), ApplySort_Internal<short?> },
                    { typeof(int), ApplySort_Internal<int> },
                    { typeof(int?), ApplySort_Internal<int?> },
                    { typeof(long), ApplySort_Internal<long> },
                    { typeof(long?), ApplySort_Internal<long?> },
                    { typeof(decimal), ApplySort_Internal<decimal> },
                    { typeof(decimal?), ApplySort_Internal<decimal?> },
                    { typeof(double), ApplySort_Internal<double> },
                    { typeof(double?), ApplySort_Internal<double?> },
                    { typeof(float), ApplySort_Internal<float> },
                    { typeof(float?), ApplySort_Internal<float?> },
                    { typeof(bool), ApplySort_Internal<bool> },
                    { typeof(bool?), ApplySort_Internal<bool?> },
                    { typeof(string), ApplySort_Internal<string> },
                    { typeof(DateTime), ApplySort_Internal<DateTime> },
                    { typeof(DateTime?), ApplySort_Internal<DateTime?> },
                };

                if (@switch.TryGetValue(fieldType, out var action))
                {
                    action();
                }
                else
                {
                    throw new NotSupportedException($"Sort on type '{fieldType}' not supported");
                }
            }

            return queryable;
        }

        public IOrderedQueryable<T> ApplySort<TMember>(IQueryable<T> queryable, Expression<Func<T, TMember>> expression, SortDirection direction)
        {
            if (direction == SortDirection.Ascending)
            {
                return queryable.OrderBy(expression);
            }
            else if (direction == SortDirection.Descending)
            {
                return queryable.OrderByDescending(expression);
            }
            else
            {
                throw new NotSupportedException($"Sort direction {direction} not supported");
            }
        }

        public IOrderedQueryable<T> ApplySort<TMember>(IOrderedQueryable<T> queryable, Expression<Func<T, TMember>> expression, SortDirection direction)
        {
            if (direction == SortDirection.Ascending)
            {
                return queryable.ThenBy(expression);
            }
            else if (direction == SortDirection.Descending)
            {
                return queryable.ThenByDescending(expression);
            }
            else
            {
                throw new NotSupportedException($"Sort direction {direction} not supported");
            }
        }

    }
}
