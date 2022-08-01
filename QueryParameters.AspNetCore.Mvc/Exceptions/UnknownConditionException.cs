using QueryParameters.Entities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryParameters.AspNetCore.Mvc.Exceptions
{
    public class UnknownConditionException : QueryParameterException
    {

        public string ConditionString { get; }

        public UnknownConditionException(string message, string conditionString) : base(message)
        {
            ConditionString = conditionString;
        }

    }
}
