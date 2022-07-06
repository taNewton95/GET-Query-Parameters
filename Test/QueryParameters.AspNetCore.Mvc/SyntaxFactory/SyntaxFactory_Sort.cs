using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryParameters.AspNetCore.Mvc.Settings;
using QueryParameters.Entities;
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
    public class SyntaxFactory_Sort
    {
        [TestMethod]
        public void Sort_FieldOnly()
        {
            var fieldName = "FieldOnly";

            var sortParams = BasicSortChecks(fieldName);

            var firstVal = sortParams.First();

            Assert.AreEqual(fieldName, firstVal.Field);
            Assert.AreEqual(SortSettings.Default.DefaultDirection, firstVal.Direction);
        }

        [TestMethod]
        public void Sort_Ascending()
        {
            var fieldName = "Ascending";

            var sortParams = BasicSortChecks(fieldName + SyntaxSettings.OperatorDelimiter + SyntaxSettings.SortAscendingOperator);

            var firstVal = sortParams.First();

            Assert.AreEqual(fieldName, firstVal.Field);
            Assert.AreEqual(SortDirection.Ascending, firstVal.Direction);
        }

        [TestMethod]
        public void Sort_Descending()
        {
            var fieldName = "Descending";

            var sortParams = BasicSortChecks(fieldName + SyntaxSettings.OperatorDelimiter + SyntaxSettings.SortDescendingOperator);

            var firstVal = sortParams.First();

            Assert.AreEqual(fieldName, firstVal.Field);
            Assert.AreEqual(SortDirection.Descending, firstVal.Direction);
        }

        [TestMethod]
        public void Sort_Multiple()
        {
            var fieldName = "Descending";

            var sortParams = BasicSortChecks(fieldName + SyntaxSettings.OperatorDelimiter + SyntaxSettings.SortDescendingOperator);

            var firstVal = sortParams.First();

            Assert.AreEqual(fieldName, firstVal.Field);
            Assert.AreEqual(SortDirection.Descending, firstVal.Direction);
        }

        private IEnumerable<SortParameter> BasicSortChecks(string input)
        {
            var strValues = new StringValues(input);

            var parseResult = QueryParameters.AspNetCore.Mvc.SyntaxFactory.Sort(strValues);

            Assert.AreEqual(strValues.Count, parseResult.Count());

            return parseResult;
        }

        [TestMethod]
        public void Sort_OnlyOperator()
        {
            var sortParams = BasicSortChecks(SyntaxSettings.OperatorDelimiter + SyntaxSettings.SortDescendingOperator);

            var firstVal = sortParams.First();

            Assert.IsFalse(firstVal.IsPopulated());
        }

        [TestMethod]
        public void Sort_ChangeSettings()
        {
            var settings = (SortSettings)SortSettings.Default.Clone();
            settings.DefaultDirection = SortDirection.Descending;

            var strValues = new StringValues("Settings");

            var sortParams = QueryParameters.AspNetCore.Mvc.SyntaxFactory.Sort(strValues, sortSettings: settings);

            var firstVal = sortParams.First();

            Assert.AreEqual(settings.DefaultDirection, firstVal.Direction);
        }

        [TestMethod]
        public void Sort_InvalidOperator()
        {
            var fieldName = "Settings";

            var sortParams = BasicSortChecks(fieldName + SyntaxSettings.OperatorDelimiter + "test");

            var firstVal = sortParams.First();

            Assert.AreEqual(fieldName, firstVal.Field);
            Assert.AreEqual(SortSettings.Default.DefaultDirection, firstVal.Direction);
        }

    }
}
