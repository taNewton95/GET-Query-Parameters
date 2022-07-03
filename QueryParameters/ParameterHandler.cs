using QueryParameters.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryParameters
{
    public class ParameterHandler<T>
    {

        public PaginationParameter Pagination { get; set; }
        public IEnumerable<SortParameter> Sort { get; set; }

        public IQueryable<T> Apply(IQueryable<T> queryable)
        {
            queryable = ApplyPagination(queryable);
            queryable = ApplySort(queryable);

            return queryable;
        }

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

        public IQueryable<T> ApplySort(IQueryable<T> queryable)
        {
            if (Sort == null || !Sort.Any()) return queryable;

            

            return queryable;
        }

    }
}
