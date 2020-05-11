using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using WebApplication.Functions;
using WebApplication.Model;

namespace web_api_nunit_test.Model
{
    [TestFixture]
    public class ClipOperationParamTests
    {

        private static IEnumerable<TestCaseData> ValidationTestCaseData
        {
            get
            {
                yield return new TestCaseData(false, false, 2).SetName("when InputLayer 'IsValidGeoJson' returns '{0}' and OverlayLayer 'IsValidGeoJson' returns '{1}' then validation result count is {2}.");
                yield return new TestCaseData(true, true, 0).SetName("when InputLayer 'IsValidGeoJson' returns '{0}' and OverlayLayer 'IsValidGeoJson' returns '{1}' then validation result count is {2}.");
                yield return new TestCaseData(false, true, 1).SetName("when InputLayer 'IsValidGeoJson' returns '{0}' and OverlayLayer 'IsValidGeoJson' returns '{1}' then validation result count is {2}.");
                yield return new TestCaseData(true, false, 1).SetName("when InputLayer 'IsValidGeoJson' returns '{0}' and OverlayLayer 'IsValidGeoJson' returns '{1}' then validation result count is {2}.");

            }
        }

        [Test, TestCaseSource(typeof(ClipOperationParamTests), nameof(ValidationTestCaseData))]
        public void Test_Validate_method(bool inputLayerValid, bool overlayLayerValid, int expectedResultCount)
        {
            Random random = new Random();

            string inputLayer = "inputLayer" + random.Next(1000);
            string overlayLayer = "overlayLayer" + random.Next(1000);

            CheckPropertyValidation checkPropertyValidation = new CheckPropertyValidation();

            Mock<IValidationFunctions> mockValidationFunctions = new Mock<IValidationFunctions>();
            mockValidationFunctions.Setup(x => x.IsValidGeoJson(inputLayer)).Returns(inputLayerValid);
            mockValidationFunctions.Setup(x => x.IsValidGeoJson(overlayLayer)).Returns(overlayLayerValid);

            ClipOperationParam clipOperationParam = new ClipOperationParam() { InputLayer = inputLayer, OverlayLayer = overlayLayer };

            IList<ValidationResult> result = checkPropertyValidation.myValidation(clipOperationParam, mockValidationFunctions);

            Assert.AreEqual(expectedResultCount, result.Count());
        }
    }

}
