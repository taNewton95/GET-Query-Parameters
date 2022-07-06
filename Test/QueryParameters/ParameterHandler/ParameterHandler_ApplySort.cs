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
    public class ParameterHandler_ApplySort
    {

        private const int DefaultDummyDataRecordCount = 1000;

        [TestMethod]
        public void ApplySort_SingleValid()
        {
            var data = GetDummyData();

            var parameterHandler = new ParameterHandler<DummyClass>();
            parameterHandler.Sort = new List<SortParameter>()
            {
                new SortParameter()
                {
                    Field = nameof(DummyClass.Index1),
                    Direction = SortDirection.Descending
                }
            };

            var sortResult = parameterHandler.ApplySort(data);

            Assert.AreEqual(9, sortResult.First().Index1);
        }

        [TestMethod]
        public void ApplySort_MultipleValid()
        {
            var data = GetDummyData();

            var parameterHandler = new ParameterHandler<DummyClass>();
            parameterHandler.Sort = new List<SortParameter>()
            {
                new SortParameter()
                {
                    Field = nameof(DummyClass.Index1),
                    Direction = SortDirection.Ascending
                },
                new SortParameter()
                {
                    Field = nameof(DummyClass.Index2),
                    Direction = SortDirection.Descending
                },
                new SortParameter()
                {
                    Field = nameof(DummyClass.Index3)
                }
            };

            var sortResult = parameterHandler.ApplySort(data);

            Assert.AreEqual(0, sortResult.First().Index1);
            Assert.AreEqual(9, sortResult.First().Index2);
            Assert.AreEqual(0, sortResult.First().Index3);
        }

        private IQueryable<DummyClass> GetDummyData(int recordCount = DefaultDummyDataRecordCount)
        {
            var retValues = new List<DummyClass>();

            for (int i = 0; i < recordCount; i++)
            {
                retValues.Add(new DummyClass()
                {
                    Index1 = i / 100,
                    Index2 = (i % 100) / 10 ,
                    Index3 = i % 10,
                });
            }

            return retValues.AsQueryable();
        }

        private class DummyClass
        {

            public int Index1;
            public int Index2;
            public int Index3 { get; set; }

        }

    }
}
