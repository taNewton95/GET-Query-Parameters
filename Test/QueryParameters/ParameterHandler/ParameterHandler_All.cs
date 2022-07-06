using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryParameters.Entities;
using QueryParameters.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryParameters.Tests.ParameterHandler
{
    [TestClass]
    public class ParameterHandler_All
    {

        private const int DefaultDummyDataRecordCount = 100;

        [TestMethod]
        public void ApplyPagination_All()
        {
            var take = 20;
            var skip = 10;

            var data = GetDummyData();

            var parameterHandler = new ParameterHandler<DummyClass>()
            {
                Pagination = new PaginationParameter()
                {
                    Skip = skip,
                    Take = take,
                },
                Sort = new List<SortParameter>()
                {
                    new SortParameter()
                    {
                        Field = "Index",
                        Direction = SortDirection.Descending,
                    },
                }
            };

            var results = parameterHandler.Apply(data);

            Assert.AreEqual(data.Count() - skip - 1, results.First().Index);
            Assert.AreEqual(take, results.Count());
        }

        private IQueryable<DummyClass> GetDummyData(int recordCount = DefaultDummyDataRecordCount)
        {
            var retValues = new List<DummyClass>();

            for (int i = 0; i < recordCount; i++)
            {
                retValues.Add(new DummyClass()
                {
                    Index = i,
                    Name = $"Name-{i}",
                });
            }

            return retValues.AsQueryable();
        }

        private class DummyClass
        {

            public int Index;
            public string Name;

        }

    }
}
