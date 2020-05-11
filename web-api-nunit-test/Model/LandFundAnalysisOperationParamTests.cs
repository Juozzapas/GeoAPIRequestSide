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
    public class LandFundAnalysisOperationParamTests
    {
        private static IEnumerable<TestCaseData> ValidationTestCaseData
        {
            get
            {
                yield return new TestCaseData(false, false, false, 3).SetName("when 'IsValidGeoJson' returns '{0}' and 'isPredicateValid' returns '{1}' and 'isDistanceValid' returns {2} then validation result count is {3}.");
                yield return new TestCaseData(false, false, true, 2).SetName("when 'IsValidGeoJson' returns '{0}' and 'isPredicateValid' returns '{1}' and 'isDistanceValid' returns {2} then validation result count is {3}.");
                yield return new TestCaseData(false, true, false, 2).SetName("when 'IsValidGeoJson' returns '{0}' and 'isPredicateValid' returns '{1}' and 'isDistanceValid' returns {2} then validation result count is {3}.");
                yield return new TestCaseData(false, true, true, 1).SetName("when 'IsValidGeoJson' returns '{0}' and 'isPredicateValid' returns '{1}' and 'isDistanceValid' returns {2} then validation result count is {3}.");
                yield return new TestCaseData(true, false, false, 2).SetName("when 'IsValidGeoJson' returns '{0}' and 'isPredicateValid' returns '{1}' and 'isDistanceValid' returns {2} then validation result count is {3}.");
                yield return new TestCaseData(true, false, true, 1).SetName("when 'IsValidGeoJson' returns '{0}' and 'isPredicateValid' returns '{1}' and 'isDistanceValid' returns {2} then validation result count is {3}.");
                yield return new TestCaseData(true, true, false, 1).SetName("when 'IsValidGeoJson' returns '{0}' and 'isPredicateValid' returns '{1}' and 'isDistanceValid' returns {2} then validation result count is {3}.");
                yield return new TestCaseData(true, true, true, 0).SetName("when 'IsValidGeoJson' returns '{0}' and 'isPredicateValid' returns '{1}' and 'isDistanceValid' returns {2} then validation result count is {3}.");
            }
        }

        [Test, TestCaseSource(typeof(LandFundAnalysisOperationParamTests), nameof(ValidationTestCaseData))]
        public void Test_Validate_method(bool inputLayerValid, bool predicateValid, bool distanceValid, int expectedResultCount)
        {
            Random random = new Random();

            CheckPropertyValidation checkPropertyValidation = new CheckPropertyValidation();

            Mock<IValidationFunctions> mockValidationFunctions = new Mock<IValidationFunctions>();
            mockValidationFunctions.Setup(x => x.IsValidPolygonMultiPolygon(It.IsAny<string>())).Returns(inputLayerValid);
            mockValidationFunctions.Setup(x => x.isPredicateValid(It.IsAny<List<int>>(), It.IsAny<int>(), It.IsAny<int>())).Returns(predicateValid);
            mockValidationFunctions.Setup(x => x.isDistanceValid(It.IsAny<string>())).Returns(distanceValid);

            LandFundAnalysisOperationParam landFundAnalysisOperationParam = new LandFundAnalysisOperationParam() { InputLayer = "inputLayer" + random.Next(1000), Predicate = new List<int>(), Distance = "distance" + random.Next(1000) };

            IList<ValidationResult> result = checkPropertyValidation.myValidation(landFundAnalysisOperationParam, mockValidationFunctions);

            Assert.AreEqual(expectedResultCount, result.Count);
        }
    }
}
