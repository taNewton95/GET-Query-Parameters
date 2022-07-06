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

        public IQueryable<T> ApplyPagination(IQueryable<T> queryable)
        {
            if (Pagination == null || !Pagination.IsPopulated()) return queryable;

            if (Pagination.Skip > 0)
            {
                queryable = queryable.Skip(Pagination.Skip);
            }

            if (Pagination.Take > 0)
            {
                queryable = queryable.Take(Pagination.Take);
            }

            return queryable;
        }

    }
}
