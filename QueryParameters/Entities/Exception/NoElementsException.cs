using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryParameters.Entities.Exceptions
{
    public class NoElementsException : QueryParameterException
    {

        public NoElementsException() : base()
        {

        }

        public NoElementsException(string message) : base(message)
        {

        }

    }
}
