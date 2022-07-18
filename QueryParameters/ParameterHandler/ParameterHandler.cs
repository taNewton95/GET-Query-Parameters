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

        public FilterParameter Filter { get; set; }
        public SortParameter Sort { get; set; }
        public PaginationParameter Pagination { get; set; }

        public IQueryable<T> Apply(IQueryable<T> queryable)
        {
            queryable = ApplyFilter(queryable);
            queryable = ApplySort(queryable);
            queryable = ApplyPagination(queryable);

            return queryable;
        }

    }
}
