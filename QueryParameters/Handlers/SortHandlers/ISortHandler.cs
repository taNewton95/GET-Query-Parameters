using QueryParameters.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryParameters.Handlers
{
    public interface ISortHandler : IHandler
    {

        Expression<Func<T, object>> GetOrderByExpression<T>(SortElementExpression sortElementExpression);

    }
}
