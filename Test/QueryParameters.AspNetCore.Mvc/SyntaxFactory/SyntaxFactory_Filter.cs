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
    public class SyntaxFactory_Filter
    {
        private const int DefaultDummyDataRecordCount = 100;

        [TestMethod]
        public void Filter_Equals()
        {
            var parameterHandler = new ParameterHandler<DummyClass>();
            parameterHandler.Filter = Mvc.SyntaxFactory.Filter<DummyClass>($"{nameof(DummyClass.Index)} {SyntaxSettings.FilterEqual} 10");
            var result = parameterHandler.ApplyFilter(GetDummyData());

            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void Filter_NotEquals()
        {
            var parameterHandler = new ParameterHandler<DummyClass>();
            parameterHandler.Filter = Mvc.SyntaxFactory.Filter<DummyClass>($"{nameof(DummyClass.Index)} {SyntaxSettings.FilterNotEqual} 10");
            var result = parameterHandler.ApplyFilter(GetDummyData());

            Assert.AreEqual(99, result.Count());
        }

        [TestMethod]
        public void Filter_LessThan()
        {
            var parameterHandler = new ParameterHandler<DummyClass>();
            parameterHandler.Filter = Mvc.SyntaxFactory.Filter<DummyClass>($"{nameof(DummyClass.Index)} {SyntaxSettings.FilterLessThan} 10");
            var result = parameterHandler.ApplyFilter(GetDummyData());

            Assert.AreEqual(10, result.Count());
        }

        [TestMethod]
        public void Filter_LessThanEqual()
        {
            var parameterHandler = new ParameterHandler<DummyClass>();
            parameterHandler.Filter = Mvc.SyntaxFactory.Filter<DummyClass>($"{nameof(DummyClass.Index)} {SyntaxSettings.FilterLessThanEqual} 10");
            var result = parameterHandler.ApplyFilter(GetDummyData());

            Assert.AreEqual(11, result.Count());
        }

        [TestMethod]
        public void Filter_GreaterThan()
        {
            var parameterHandler = new ParameterHandler<DummyClass>();
            parameterHandler.Filter = Mvc.SyntaxFactory.Filter<DummyClass>($"{nameof(DummyClass.Index)} {SyntaxSettings.FilterGreaterThan} 10");
            var result = parameterHandler.ApplyFilter(GetDummyData());

            Assert.AreEqual(89, result.Count());
        }

        [TestMethod]
        public void Filter_GreaterThanEqual()
        {
            var parameterHandler = new ParameterHandler<DummyClass>();
            parameterHandler.Filter = Mvc.SyntaxFactory.Filter<DummyClass>($"{nameof(DummyClass.Index)} {SyntaxSettings.FilterGreaterThanEqual} 10");
            var result = parameterHandler.ApplyFilter(GetDummyData());

            Assert.AreEqual(90, result.Count());
        }

        [TestMethod]
        public void Filter_Starts()
        {
            var parameterHandler = new ParameterHandler<DummyClass>();
            parameterHandler.Filter = Mvc.SyntaxFactory.Filter<DummyClass>($"{nameof(DummyClass.String)} {SyntaxSettings.FilterStarts} '1'");
            var result = parameterHandler.ApplyFilter(GetDummyData());

            Assert.AreEqual(11, result.Count());
        }

        [TestMethod]
        public void Filter_Ends()
        {
            var parameterHandler = new ParameterHandler<DummyClass>();
            parameterHandler.Filter = Mvc.SyntaxFactory.Filter<DummyClass>($"{nameof(DummyClass.String)} {SyntaxSettings.FilterEnds} '1'");
            var result = parameterHandler.ApplyFilter(GetDummyData());

            Assert.AreEqual(10, result.Count());
        }

        [TestMethod]
        public void Filter_Contains()
        {
            var parameterHandler = new ParameterHandler<DummyClass>();
            parameterHandler.Filter = Mvc.SyntaxFactory.Filter<DummyClass>($"{nameof(DummyClass.String)} {SyntaxSettings.FilterContains} '1'");
            var result = parameterHandler.ApplyFilter(GetDummyData());

            Assert.AreEqual(19, result.Count());
        }

        [TestMethod]
        public void Filter_StringSpace()
        {
            var parameterHandler = new ParameterHandler<DummyClass>();
            parameterHandler.Filter = Mvc.SyntaxFactory.Filter<DummyClass>($"{nameof(DummyClass.String2)} {SyntaxSettings.FilterEqual} 'String '' 10'");
            var result = parameterHandler.ApplyFilter(GetDummyData());

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("String ' 10", result.First().String2);
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
                    String2 = "String ' " + i,
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
            public string String2;

        }

    }
}
