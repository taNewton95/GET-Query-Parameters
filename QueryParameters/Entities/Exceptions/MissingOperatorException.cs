using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryParameters.Entities.Exceptions
{
    public class MissingOperatorException : QueryParameterException
    {

        public MissingOperatorException() : base()
        {

        }

        public MissingOperatorException(string message) : base(message)
        {

        }

    }
}
