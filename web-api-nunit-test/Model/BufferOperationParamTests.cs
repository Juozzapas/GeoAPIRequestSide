using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebApplication.Functions;
using WebApplication.Model;


namespace web_api_nunit_test.Model
{
    [TestFixture]
    public class BufferOperationParamTests
    {
        private static IEnumerable<TestCaseData> ValidationTestCaseData
        {
            get
            {
                yield return new TestCaseData(false, false, 2).SetName("when 'IsValidGeoJson' returns '{0}' and 'isDistanceValid' returns '{1}' then validation result count is {2}.");
                yield return new TestCaseData(true, true, 0).SetName("when 'IsValidGeoJson' returns '{0}' and 'isDistanceValid' returns '{1}' then validation result count is {2}.");
                yield return new TestCaseData(true, false, 1).SetName("when 'IsValidGeoJson' returns '{0}' and 'isDistanceValid' returns '{1}' then validation result count is {2}.");
                yield return new TestCaseData(false, true, 1).SetName("when 'IsValidGeoJson' returns '{0}' and 'isDistanceValid' returns '{1}' then validation result count is {2}.");
            }
        }

        Random random;

        private CheckPropertyValidation checkPropertyValidation;
        private Mock<IValidationFunctions> mockValidationFunctions;
        private BufferOperationParam bufferOperationParam;

        [SetUp]
        public void SetupBeforeEachTest()
        {
            random = new Random();

            checkPropertyValidation = new CheckPropertyValidation();

            mockValidationFunctions = new Mock<IValidationFunctions>();
        }

        [Test, TestCaseSource(typeof(BufferOperationParamTests), nameof(ValidationTestCaseData))]
        public void Test_Validate_method(bool geoJSONValid, bool distanceValid, int expectedResultCount)
        {
            mockValidationFunctions.Setup(x => x.IsValidGeoJson(It.IsAny<string>())).Returns(geoJSONValid);
            mockValidationFunctions.Setup(x => x.isDistanceValid(It.IsAny<string>())).Returns(distanceValid); 

            bufferOperationParam = new BufferOperationParam() { InputLayer = "inputLayerTypeGeoJSON" + random.Next(), Distance = "distance" + random.Next() };

            IList<ValidationResult> result = checkPropertyValidation.myValidation(bufferOperationParam, mockValidationFunctions);
            
            Assert.AreEqual(expectedResultCount, result.Count());
        }
    }
}
