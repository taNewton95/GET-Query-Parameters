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
            var paginatedResult = parameterHandler.ApplyFilter(data);

            Assert.AreEqual(DefaultDummyDataRecordCount, paginatedResult.Count());
        }

        [TestMethod]
        public void ApplyFilter_SingleFilter()
        {
            var data = GetDummyData();

            var parameterHandler = new ParameterHandler<DummyClass>();

            parameterHandler.Filter = new FilterParameter();

            parameterHandler.Filter.Elements.BeginScope();
            parameterHandler.Filter.Elements.Add(new FilterElementExpression(new IdentifierElement("Index3"), FilterElementCondition.Equal, new FilterElementValue(3)));
            parameterHandler.Filter.Elements.Add(FilterElementOperator.Or);
            parameterHandler.Filter.Elements.Add(new FilterElementExpression(new IdentifierElement("Index3"), FilterElementCondition.Equal, new FilterElementValue(8)));
            parameterHandler.Filter.Elements.EndScope();
            parameterHandler.Filter.Elements.Add(FilterElementOperator.And);
            parameterHandler.Filter.Elements.Add(new FilterElementExpression(new IdentifierElement("Index2"), FilterElementCondition.Equal, new FilterElementValue(2)));

            var paginatedResult = parameterHandler.ApplyFilter(data);

            Assert.AreEqual(DefaultDummyDataRecordCount, paginatedResult.Count());
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
