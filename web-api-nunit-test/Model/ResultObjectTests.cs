using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.Model;

namespace web_api_nunit_test.Model
{
    [TestFixture]
    public class ResultObjectTests
    {
        private static IEnumerable<TestCaseData> IsEmptyTestCaseData
        {
            get
            {
                yield return new TestCaseData(null, null).Returns(true).SetName("when 'stderr' is '{0}' and 'stdout' is '{1}' then isEmpty() returns count {2}.");
                yield return new TestCaseData(null, "").Returns(true).SetName("when 'stderr' is '{0}' and 'stdout' is '{1}' then isEmpty() returns count {2}.");
                yield return new TestCaseData("", null).Returns(true).SetName("when 'stderr' is '{0}' and 'stdout' is '{1}' then isEmpty() returns count {2}.");
                yield return new TestCaseData("", "").Returns(true).SetName("when 'stderr' is '{0}' and 'stdout' is '{1}' then isEmpty() returns count {2}.");
                yield return new TestCaseData(null, "").Returns(true).SetName("when 'stderr' is '{0}' and 'stdout' is '{1}' then isEmpty() returns count {2}.");
                yield return new TestCaseData(null, "test").Returns(false).SetName("when 'stderr' is '{0}' and 'stdout' is '{1}' then isEmpty() returns count {2}.");
                yield return new TestCaseData("test", null).Returns(false).SetName("when 'stderr' is '{0}' and 'stdout' is '{1}' then isEmpty() returns count {2}.");
                yield return new TestCaseData("", "test").Returns(false).SetName("when 'stderr' is '{0}' and 'stdout' is '{1}' then isEmpty() returns count {2}.");
                yield return new TestCaseData("test", "").Returns(false).SetName("when 'stderr' is '{0}' and 'stdout' is '{1}' then isEmpty() returns count {2}.");
                yield return new TestCaseData("test", "test").Returns(false).SetName("when 'stderr' is '{0}' and 'stdout' is '{1}' then isEmpty() returns count {2}.");
            }
        }

        [Test, TestCaseSource(typeof(ResultObjectTests), nameof(IsEmptyTestCaseData))]
        public bool Test_Validate_method(string stderr, string stdout)
        {
            ResultObject resultObject = new ResultObject(stderr, stdout);
            return resultObject.isEmpty();
        }
    }
}
