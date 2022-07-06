using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryParameters.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryParameters.Tests.FilterElement
{
    [TestClass]
    public class FilterElement_Operator
    {

        [TestMethod]
        public void ToStringImplemented()
        {
            var filterElementOperator = typeof(FilterElementOperator);
            var staticFields = filterElementOperator.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);

            foreach (var staticField in staticFields)
            {
                staticField.GetValue(null).ToString();
            }
        }

    }
}
