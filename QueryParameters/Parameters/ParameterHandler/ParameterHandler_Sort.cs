using QueryParameters.Entities;
using QueryParameters.Handlers;
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

        public ISortHandler SortHandler = new DefaultSortHandler();

        public IQueryable<T> ApplySort(IQueryable<T> queryable)
        {
            if (Sort == null || !Sort.Elements.Any()) return queryable;

            IOrderedQueryable<T> orderedQueryable = null;

            foreach (var item in Sort.Elements)
            {
                if (orderedQueryable == null)
                {
                    if (item.Direction == SortElementDirection.Ascending)
                    {
                        orderedQueryable = queryable.OrderBy(SortHandler.GetOrderByExpression<T>(item));
                    }
                    else if (item.Direction == SortElementDirection.Descending)
                    {
                        orderedQueryable = queryable.OrderByDescending(SortHandler.GetOrderByExpression<T>(item));
                    }
                }
                else
                {
                    if (item.Direction == SortElementDirection.Ascending)
                    {
                        orderedQueryable = orderedQueryable.ThenBy(SortHandler.GetOrderByExpression<T>(item));
                    }
                    else if (item.Direction == SortElementDirection.Descending)
                    {
                        orderedQueryable = orderedQueryable.ThenByDescending(SortHandler.GetOrderByExpression<T>(item));
                    }
                }
            }

            queryable = orderedQueryable;

            return queryable;
        }

    }
}
