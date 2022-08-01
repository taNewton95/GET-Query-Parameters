using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryParameters.Entities.Exceptions
{
    public class QueryParameterException : Exception
    {

        public QueryParameterException() : base()
        {

        }

        public QueryParameterException(string message) : base(message)
        {

        }

    }
}
