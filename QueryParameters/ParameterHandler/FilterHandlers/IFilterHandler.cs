using QueryParameters.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryParameters
{
    public interface IFilterHandler : IHandler
    {

        Expression GetExpression<T>(FilterElementExpression filterElementExpression, ParameterExpression parameterExpression);
        
    }
}
