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
    public class SelectByLocationOperationParamTests
    {
        private static IEnumerable<TestCaseData> ValidationTestCaseData
        {
            get
            {
                yield return new TestCaseData(false, false, false, false, 4).SetName("when InputLayer 'IsValidGeoJson' returns '{0}' and OverlayLayer 'IsValidGeoJson' returns {1} and 'isPredicateValid' returns '{2}' and 'isDistanceValid' returns {3} then validation result count is {4}.");
                yield return new TestCaseData(false, false, false, true, 3).SetName("when InputLayer 'IsValidGeoJson' returns '{0}' and OverlayLayer 'IsValidGeoJson' returns {1} and 'isPredicateValid' returns '{2}' and 'isDistanceValid' returns {3} then validation result count is {4}.");
                yield return new TestCaseData(false, false, true, false, 3).SetName("when InputLayer 'IsValidGeoJson' returns '{0}' and OverlayLayer 'IsValidGeoJson' returns {1} and 'isPredicateValid' returns '{2}' and 'isDistanceValid' returns {3} then validation result count is {4}.");
                yield return new TestCaseData(false, false, true, true, 2).SetName("when InputLayer 'IsValidGeoJson' returns '{0}' and OverlayLayer 'IsValidGeoJson' returns {1} and 'isPredicateValid' returns '{2}' and 'isDistanceValid' returns {3} then validation result count is {4}.");
                yield return new TestCaseData(false, true, false, false, 3).SetName("when InputLayer 'IsValidGeoJson' returns '{0}' and OverlayLayer 'IsValidGeoJson' returns {1} and 'isPredicateValid' returns '{2}' and 'isDistanceValid' returns {3} then validation result count is {4}.");
                yield return new TestCaseData(false, true, false, true, 2).SetName("when InputLayer 'IsValidGeoJson' returns '{0}' and OverlayLayer 'IsValidGeoJson' returns {1} and 'isPredicateValid' returns '{2}' and 'isDistanceValid' returns {3} then validation result count is {4}.");
                yield return new TestCaseData(false, true, true, true, 1).SetName("when InputLayer 'IsValidGeoJson' returns '{0}' and OverlayLayer 'IsValidGeoJson' returns {1} and 'isPredicateValid' returns '{2}' and 'isDistanceValid' returns {3} then validation result count is {4}.");
                yield return new TestCaseData(true, false, false, false, 3).SetName("when InputLayer 'IsValidGeoJson' returns '{0}' and OverlayLayer 'IsValidGeoJson' returns {1} and 'isPredicateValid' returns '{2}' and 'isDistanceValid' returns {3} then validation result count is {4}.");
                yield return new TestCaseData(true, false, false, true, 2).SetName("when InputLayer 'IsValidGeoJson' returns '{0}' and OverlayLayer 'IsValidGeoJson' returns {1} and 'isPredicateValid' returns '{2}' and 'isDistanceValid' returns {3} then validation result count is {4}.");
                yield return new TestCaseData(true, false, true, false, 2).SetName("when InputLayer 'IsValidGeoJson' returns '{0}' and OverlayLayer 'IsValidGeoJson' returns {1} and 'isPredicateValid' returns '{2}' and 'isDistanceValid' returns {3} then validation result count is {4}.");
                yield return new TestCaseData(true, false, true, true, 1).SetName("when InputLayer 'IsValidGeoJson' returns '{0}' and OverlayLayer 'IsValidGeoJson' returns {1} and 'isPredicateValid' returns '{2}' and 'isDistanceValid' returns {3} then validation result count is {4}.");
                yield return new TestCaseData(true, true, false, false, 2).SetName("when InputLayer 'IsValidGeoJson' returns '{0}' and OverlayLayer 'IsValidGeoJson' returns {1} and 'isPredicateValid' returns '{2}' and 'isDistanceValid' returns {3} then validation result count is {4}.");
                yield return new TestCaseData(true, true, false, true, 1).SetName("when InputLayer 'IsValidGeoJson' returns '{0}' and OverlayLayer 'IsValidGeoJson' returns {1} and 'isPredicateValid' returns '{2}' and 'isDistanceValid' returns {3} then validation result count is {4}.");
                yield return new TestCaseData(true, true, true, false, 1).SetName("when InputLayer 'IsValidGeoJson' returns '{0}' and OverlayLayer 'IsValidGeoJson' returns {1} and 'isPredicateValid' returns '{2}' and 'isDistanceValid' returns {3} then validation result count is {4}.");
                yield return new TestCaseData(true, true, true, true, 0).SetName("when InputLayer 'IsValidGeoJson' returns '{0}' and OverlayLayer 'IsValidGeoJson' returns {1} and 'isPredicateValid' returns '{2}' and 'isDistanceValid' returns {3} then validation result count is {4}.");
            }
        }

        [Test, TestCaseSource(typeof(SelectByLocationOperationParamTests), nameof(ValidationTestCaseData))]
        public void Test_Validate_method(bool inputLayerValid, bool overlayLayerValid, bool distanceValid, bool predicateValid, int expectedResultCount)
        {
            Random random = new Random();

            string inputLayer = "inputLayer" + random.Next();
            string overlayLayer = "overlayLayer" + random.Next();

            CheckPropertyValidation checkPropertyValidation = new CheckPropertyValidation();

            Mock<IValidationFunctions> mockValidationFunctions = new Mock<IValidationFunctions>();
            mockValidationFunctions.Setup(x => x.IsValidGeoJson(inputLayer)).Returns(inputLayerValid);
            mockValidationFunctions.Setup(x => x.IsValidGeoJson(overlayLayer)).Returns(overlayLayerValid);
            mockValidationFunctions.Setup(x => x.isDistanceValid(It.IsAny<string>())).Returns(distanceValid);
            mockValidationFunctions.Setup(x => x.isPredicateValid(It.IsAny<List<int>>(), It.IsAny<int>(), It.IsAny<int>())).Returns(predicateValid);

            SelectByLocationOperationParam selectByLocationOperationParam = new SelectByLocationOperationParam() { InputLayer = inputLayer, OverlayLayer = overlayLayer, Distance = "distance" + random.Next() };

            IList<ValidationResult> result = checkPropertyValidation.myValidation(selectByLocationOperationParam, mockValidationFunctions);

            Assert.AreEqual(expectedResultCount, result.Count);
        }
    }
}
