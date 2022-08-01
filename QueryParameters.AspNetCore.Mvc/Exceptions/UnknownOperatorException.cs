using QueryParameters.Entities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryParameters.AspNetCore.Mvc.Exceptions
{
    public class UnknownOperatorException : QueryParameterException
    {

        public string OperatorString { get; }

        public UnknownOperatorException(string message, string operatorString) : base(message)
        {
            OperatorString = operatorString;
        }

    }
}
