using QueryParameters.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryParameters
{
    internal class DefaultAscHandler : ISortHandler
    {

        public Expression GetOrderByExpression<T>(FilterElementExpression filterElementExpression, ParameterExpression parameterExpression)
        {
            throw new NotImplementedException();
        }

        public Expression GetThenByExpression<T>(FilterElementExpression filterElementExpression, ParameterExpression parameterExpression)
        {
            throw new NotImplementedException();
        }

    }
}
