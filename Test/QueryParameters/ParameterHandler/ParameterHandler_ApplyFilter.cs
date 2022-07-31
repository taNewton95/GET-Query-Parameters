using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryParameters.Entities;
using QueryParameters.Parameters;
using QueryParameters.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryParameters.Tests.ParameterHandler
{
    [TestClass]
    public class ParameterHandler_ApplyFilter
    {

        private const int DefaultDummyDataRecordCount = 100;

        [TestMethod]
        public void ApplyFilter_NoFilters()
        {
            var data = GetDummyData();

            var parameterHandler = new ParameterHandler<DummyClass>();
            var filterResult = parameterHandler.ApplyFilter(data);

            Assert.AreEqual(DefaultDummyDataRecordCount, filterResult.Count());
        }

        [TestMethod]
        public void ApplyFilter_SingleFilter()
        {
            var data = GetDummyData();

            var parameterHandler = new ParameterHandler<DummyClass>();

            parameterHandler.Filter = new FilterParameter();

            parameterHandler.Filter.Elements.Add(new FilterElementExpression(new IdentifierElement(nameof(DummyClass.String)), FilterElementCondition.Equal, new FilterElementValue("15")));

            var filterResult = parameterHandler.ApplyFilter(data);

            Assert.AreEqual(1, filterResult.Count());

            var firstElement = filterResult.ElementAt(0);

            Assert.AreEqual(firstElement.Index, 15);
        }

        [TestMethod]
        public void ApplyFilter_MultipleFilter()
        {
            var data = GetDummyData();

            var parameterHandler = new ParameterHandler<DummyClass>();

            parameterHandler.Filter = new FilterParameter();

            parameterHandler.Filter.Elements.BeginScope();
            parameterHandler.Filter.Elements.Add(new FilterElementExpression(new IdentifierElement(nameof(DummyClass.Index3)), FilterElementCondition.Equal, new FilterElementValue(3)));
            parameterHandler.Filter.Elements.Add(FilterElementOperator.Or);
            parameterHandler.Filter.Elements.Add(new FilterElementExpression(new IdentifierElement(nameof(DummyClass.Index3)), FilterElementCondition.Equal, new FilterElementValue(8)));
            parameterHandler.Filter.Elements.EndScope();
            parameterHandler.Filter.Elements.Add(FilterElementOperator.And);
            parameterHandler.Filter.Elements.Add(new FilterElementExpression(new IdentifierElement(nameof(DummyClass.Index2)), FilterElementCondition.Equal, new FilterElementValue(2)));

            var filterResult = parameterHandler.ApplyFilter(data);

            Assert.AreEqual(2, filterResult.Count());

            var firstElement = filterResult.ElementAt(0);

            Assert.AreEqual(firstElement.Index, 23);

            var secondElement = filterResult.ElementAt(1);

            Assert.AreEqual(secondElement.Index, 28);
        }

        private IQueryable<DummyClass> GetDummyData(int recordCount = DefaultDummyDataRecordCount)
        {
            var retValues = new List<DummyClass>();

            for (int i = 0; i < recordCount; i++)
            {
                retValues.Add(new DummyClass()
                {
                    Index = i,
                    Index2 = i / 10,
                    Index3 = i % 10,
                    String = i.ToString(),
                });
            }

            return retValues.AsQueryable();
        }

        private class DummyClass
        {

            public int Index;
            public int Index2;
            public int Index3;
            public string String;

        }

    }
}
