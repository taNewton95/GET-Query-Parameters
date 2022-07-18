using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryParameters.Entities;
using QueryParameters.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryParameters.Tests.FilterElement
{
    [TestClass]
    public class FilterElement_Condition
    {

        [TestMethod]
        public void ToStringImplemented()
        {
            var filterElementType = typeof(FilterElementCondition);
            var staticFields = filterElementType.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);

            foreach (var staticField in staticFields)
            {
                staticField.GetValue(null).ToString();
            }
        }

    }
}
