using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebApplication.Functions;
using WebApplication.Model;

namespace web_api_nunit_test.Model
{
    [TestFixture]
    public class TransformOperationParamTests
    {
        private static IEnumerable<TestCaseData> ValidationTestCaseData
        {
            get
            {
                yield return new TestCaseData("ESRI Shapefile",0).SetName("when Type is {0} then validation error count is {1}");
                yield return new TestCaseData("CSV",0).SetName("when Type is {0} then validation error count is {1}");
                yield return new TestCaseData("GML",0).SetName("when Type is {0} then validation error count is {1}");
                yield return new TestCaseData("KML",0).SetName("when Type is {0} then validation error count is {1}");
                yield return new TestCaseData("GeoJSON",0).SetName("when Type is {0} then validation error count is {1}");
            }
        }

        [Test, TestCaseSource(typeof(TransformOperationParamTests), nameof(ValidationTestCaseData))]
        public void Test_Validate_method(string type, int expectedResultCount)
        {
            Random random = new Random();

            CheckPropertyValidation checkPropertyValidation = new CheckPropertyValidation();

            Mock<IValidationFunctions> mockValidationFunctions = new Mock<IValidationFunctions>();
            mockValidationFunctions.Setup(x => x.IsValidGeoJson(It.IsAny<string>())).Returns(true);

            TransformOperationParam transformOperationParam = new TransformOperationParam() { InputLayer = "InputLayer" + random.Next(1000), Type = type, SourceCrs = "sourceCrs" + random.Next(1000), TargetCrs = "" + random.Next(1000) };

            IList<ValidationResult> result = checkPropertyValidation.myValidation(transformOperationParam, mockValidationFunctions);

            Assert.AreEqual(expectedResultCount, result.Count);
        }
    }
}
