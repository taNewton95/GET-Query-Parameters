using QueryParameters.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryParameters
{
    public interface ISortHandler : IHandler
    {

        Expression GetOrderByExpression<T>(FilterElementExpression filterElementExpression, ParameterExpression parameterExpression);
        Expression GetThenByExpression<T>(FilterElementExpression filterElementExpression, ParameterExpression parameterExpression);

    }
}
