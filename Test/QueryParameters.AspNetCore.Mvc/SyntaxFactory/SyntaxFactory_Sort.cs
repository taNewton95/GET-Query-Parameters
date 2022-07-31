using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace QueryParameters.AspNetCore.Mvc.Tests.SyntaxFactory
{
    [TestClass]
    public class SyntaxFactory_Sort
    {
        [TestMethod]
        public void Sort_FieldOnly()
        {
            var fieldName = "FieldOnly";

            var sortParam = Mvc.SyntaxFactory.Sort(new StringValues(fieldName));

            Assert.AreEqual(1, sortParam.Elements.Count());
            Assert.AreEqual(fieldName, sortParam.Elements.First().Identifier.Identifier);
        }

        [TestMethod]
        public void Sort_Ascending()
        {
            var fieldName = "FieldOnly asc";

            var sortParam = Mvc.SyntaxFactory.Sort(new StringValues(fieldName));

            var firstVal = sortParam.Elements.First();

            Assert.AreEqual(1, sortParam.Elements.Count());
            Assert.AreEqual("FieldOnly", firstVal.Identifier.Identifier);
            Assert.AreEqual(Entities.SortElementDirection.Ascending, firstVal.Direction);
        }

        [TestMethod]
        public void Sort_Descending()
        {
            var fieldName = "FieldOnly desc";

            var sortParam = Mvc.SyntaxFactory.Sort(new StringValues(fieldName));

            var firstVal = sortParam.Elements.First();

            Assert.AreEqual(1, sortParam.Elements.Count());
            Assert.AreEqual("FieldOnly", firstVal.Identifier.Identifier);
            Assert.AreEqual(Entities.SortElementDirection.Descending, firstVal.Direction);
        }

        [TestMethod]
        public void Sort_Multiple()
        {
            var fieldName = "FieldOnly, FieldOnly asc, FieldOnly desc";

            var sortParam = Mvc.SyntaxFactory.Sort(new StringValues(fieldName));

            Assert.AreEqual(3, sortParam.Elements.Count());

            foreach (var param in sortParam.Elements)
            {
                Assert.AreEqual("FieldOnly", param.Identifier.Identifier);
            }

            Assert.AreEqual(Entities.SortElementDirection.Ascending, sortParam.Elements.ElementAt(0).Direction);
            Assert.AreEqual(Entities.SortElementDirection.Ascending, sortParam.Elements.ElementAt(1).Direction);
            Assert.AreEqual(Entities.SortElementDirection.Descending, sortParam.Elements.ElementAt(2).Direction);
        }

        [TestMethod]
        public void Sort_InvalidOperator()
        {
            var fieldName = "FieldOnly test";

            var sortParam = Mvc.SyntaxFactory.Sort(new StringValues(fieldName));

            Assert.AreEqual(1, sortParam.Elements.Count());
            Assert.AreEqual("FieldOnly", sortParam.Elements.ElementAt(0).Identifier.Identifier);
            Assert.AreEqual(Entities.SortElementDirection.Ascending, sortParam.Elements.ElementAt(0).Direction);
        }

    }
}
