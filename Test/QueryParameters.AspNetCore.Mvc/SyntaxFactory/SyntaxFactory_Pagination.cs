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
    public class SyntaxFactory_Pagination
    {
        [TestMethod]
        [DataRow("10", null)]
        [DataRow(null, "5")]
        [DataRow(null, null)]
        [DataRow("1", "2")]
        public void Pagination_ValidValues(string take, string skip)
        {
            var paramDictionary = new Dictionary<string, StringValues>();

            if (!string.IsNullOrEmpty(take))
            {
                paramDictionary.Add(SyntaxSettings.PaginationTakeName, take);
            }

            if (!string.IsNullOrEmpty(skip))
            {
                paramDictionary.Add(SyntaxSettings.PaginationSkipName, skip);
            }

            var parseResult = QueryParameters.AspNetCore.Mvc.SyntaxFactory.Pagination(new QueryCollection(paramDictionary));

            Assert.IsNotNull(parseResult);

            if (string.IsNullOrEmpty(take))
            {
                Assert.AreEqual(PaginationSettings.Default.DefaultTake, parseResult.Take);
            }
            else
            {
                Assert.AreEqual(int.Parse(take), parseResult.Take);
            }

            if (string.IsNullOrEmpty(skip))
            {
                Assert.AreEqual(PaginationSettings.Default.DefaultSkip, parseResult.Skip);
            }
            else
            {
                Assert.AreEqual(int.Parse(skip), parseResult.Skip);
            }
        }

        [TestMethod]
        public void Pagination_InvalidValues()
        {
            var paramDictionary = new Dictionary<string, StringValues>()
            {
                { SyntaxSettings.PaginationTakeName, new StringValues("test") },
                { SyntaxSettings.PaginationSkipName, new StringValues("test") }
            };

            var parseResult = QueryParameters.AspNetCore.Mvc.SyntaxFactory.Pagination(new QueryCollection(paramDictionary));

            Assert.IsNotNull(parseResult);

            Assert.AreEqual(PaginationSettings.Default.DefaultTake, parseResult.Take);
            Assert.AreEqual(PaginationSettings.Default.DefaultSkip, parseResult.Skip);
        }

        [TestMethod]
        public void Pagination_ChangeSettings()
        {
            int take = 10, skip = 20;

            var settings = (PaginationSettings)PaginationSettings.Default.Clone();
            settings.DefaultSkip = skip;
            settings.DefaultTake = take;

            var parseResult = QueryParameters.AspNetCore.Mvc.SyntaxFactory.Pagination(new QueryCollection(), paginationSettings: settings);

            Assert.IsNotNull(parseResult);

            Assert.AreEqual(take, parseResult.Take);
            Assert.AreEqual(skip, parseResult.Skip);
        }

    }
}
