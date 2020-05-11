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
    public class SelectByAttributeOperationParamTests
    {
        private static IEnumerable<TestCaseData> ValidationTestCaseData
        {
            get
            {
                yield return new TestCaseData(false, false, 2).SetName("when 'IsValidGeoJson' returns '{0}' 'isUsedOperatorValid' returns '{1}' then validation result count is {2}.");
                yield return new TestCaseData(true, true, 0).SetName("when 'IsValidGeoJson' returns '{0}' 'isUsedOperatorValid' returns '{1}' then validation result count is {2}.");
                yield return new TestCaseData(false, true, 1).SetName("when 'IsValidGeoJson' returns '{0}' 'isUsedOperatorValid' returns '{1}' then validation result count is {2}.");
                yield return new TestCaseData(true, false, 1).SetName("when 'IsValidGeoJson' returns '{0}' 'isUsedOperatorValid' returns '{1}' then validation result count is {2}.");
            }
        }

        [Test, TestCaseSource(typeof(SelectByAttributeOperationParamTests), nameof(ValidationTestCaseData))]
        public void Test_Validate_method(bool inputLayerValid, bool operatorValid, int expectedResultCount)
        {
            Random random = new Random();

            CheckPropertyValidation checkPropertyValidation = new CheckPropertyValidation();

            Mock<IValidationFunctions> mockValidationFunctions = new Mock<IValidationFunctions>();
            mockValidationFunctions.Setup(x => x.IsValidGeoJson(It.IsAny<string>())).Returns(inputLayerValid);
            mockValidationFunctions.Setup(x => x.isUsedOperatorValid(It.IsAny<int>())).Returns(operatorValid);

            SelectByAttributeOperationParam selectByAttributeOperationParam = new SelectByAttributeOperationParam() { InputLayer = "InputLayer" + random.Next(1000), Operator = random.Next(1000), Field = "field"+random.Next(1000) };

            IList<ValidationResult> result = checkPropertyValidation.myValidation(selectByAttributeOperationParam, mockValidationFunctions);

            Assert.AreEqual(expectedResultCount, result.Count);
        }
    }
}
