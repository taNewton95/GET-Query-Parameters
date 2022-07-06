using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryParameters.AspNetCore.Mvc.Settings;
using QueryParameters.Parameters;
using QueryParameters.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryParameters.AspNetCore.Mvc.Tests.SyntaxFactory
{
    [TestClass]
    public class SyntaxFactory_Filter
    {
        [TestMethod]
        public void Pagination_ValidValues()
        {
            var paramDictionary = new Dictionary<string, StringValues>();

            paramDictionary.Add("filter", "c_name eq '123' and c_id eq 123");

            var parseResult = QueryParameters.AspNetCore.Mvc.SyntaxFactory.Filter(new QueryCollection(paramDictionary));
        }

    }
}
