using Microsoft.VisualStudio.TestTools.UnitTesting;
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

            var filterIdentifier = new FilterElementIdentifier("Index");

            parameterHandler.Filter.Elements.Add(new FilterElementExpression(filterIdentifier, FilterElementCondition.LessThan, new FilterElementValue(10)));

            parameterHandler.Filter.Elements.Add(FilterElementOperator.And);

            parameterHandler.Filter.Elements.Add(new FilterElementExpression(filterIdentifier, FilterElementCondition.GreaterThan, new FilterElementValue(5)));

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
                    Index = i
                });
            }

            return retValues.AsQueryable();
        }

        private class DummyClass
        {

            public int Index;

        }

    }
}
