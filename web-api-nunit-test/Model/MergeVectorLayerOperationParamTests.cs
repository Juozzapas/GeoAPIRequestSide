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
    public class MergeVectorLayerOperationParamTests
    {
        private static IEnumerable<TestCaseData> ValidationTestCaseData
        {
            get
            {
                yield return new TestCaseData(false, 1).SetName("when 'IsValidGeoJson' returns '{0}' then validation result count is {1}.");
                yield return new TestCaseData(true, 0).SetName("when 'IsValidGeoJson' returns '{0}' then validation result count is {1}.");
            }
        }

        [Test, TestCaseSource(typeof(MergeVectorLayerOperationParamTests), nameof(ValidationTestCaseData))]
        public void Test_Validate_method(bool inputLayerValid, int expectedResultCount)
        {
            Random random = new Random();

            CheckPropertyValidation checkPropertyValidation = new CheckPropertyValidation();

            Mock<IValidationFunctions> mockValidationFunctions = new Mock<IValidationFunctions>();
            mockValidationFunctions.Setup(x => x.IsValidGeoJsonArray(It.IsAny<string>())).Returns(inputLayerValid);

            MergeVectorLayerOperationParam mergeVectorLayerOperationParam = new MergeVectorLayerOperationParam() { InputLayers = "inputLayers" + random.Next(1000) };

            IList<ValidationResult> result = checkPropertyValidation.myValidation(mergeVectorLayerOperationParam, mockValidationFunctions);

            Assert.AreEqual(expectedResultCount, result.Count);
        }
    }
}
