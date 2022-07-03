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
    public class ParameterHandler_ApplyPagination
    {

        private const int DefaultDummyDataRecordCount = 100;

        [TestMethod]
        public void ApplyPagination_NoPagination()
        {
            var data = GetDummyData();

            var parameterHandler = new ParameterHandler<DummyClass>();
            var paginatedResult = parameterHandler.ApplyPagination(data);

            Assert.AreEqual(DefaultDummyDataRecordCount, paginatedResult.Count());
        }

        [TestMethod]
        [DataRow(0, 0)]
        [DataRow(10, 0)]
        [DataRow(0, 20)]
        [DataRow(10, 20)]
        public void ApplyPagination_Valid(int take, int skip)
        {
            var data = GetDummyData();

            var parameterHandler = new ParameterHandler<DummyClass>();
            parameterHandler.Pagination = new PaginationParameter()
            {
                Take = take,
                Skip = skip,
            };

            var paginatedResult = parameterHandler.ApplyPagination(data);

            Assert.AreEqual(take == 0 ? data.Count() - skip : take, paginatedResult.Count());
            Assert.AreEqual(skip, paginatedResult.First().Index);
        }

        [TestMethod]
        public void ApplyPagination_Invalid()
        {
            var data = GetDummyData();

            var parameterHandler = new ParameterHandler<DummyClass>();
            parameterHandler.Pagination = new PaginationParameter()
            {
                Take = -10,
                Skip = -20,
            };

            var paginatedResult = parameterHandler.ApplyPagination(data);

            Assert.AreEqual(data.Count(), paginatedResult.Count());
            Assert.AreEqual(0, paginatedResult.First().Index);
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
