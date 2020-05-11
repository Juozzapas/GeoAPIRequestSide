using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.HandleThirdPartyOutput.HandleLogic;
using WebApplication.Model;

namespace web_api_nunit_test.HandleThirdPartyOutput
{
    [TestFixture]
    public class LibraryOutputHandlerTests
    {

        private static IEnumerable<TestCaseData> HandleResponseCodeTestCaseData
        {
            get
            {
                yield return new TestCaseData("Failed to process SRS definition", 400).SetName("method 'HandleResponse' returns object with error code {1} when ResultObject stderr contains 'ERROR' and '{0}' .");
                yield return new TestCaseData("Failed to read GeoJSON data", 400).SetName("method 'HandleResponse' returns object with error code {1} when ResultObject stderr contains 'ERROR' and '{0}' .");
                yield return new TestCaseData("Failed to reproject feature", 400).SetName("method 'HandleResponse' returns object with error code {1} when ResultObject stderr contains 'ERROR' and '{0}' .");
                yield return new TestCaseData("", 400).SetName("method 'HandleResponse' returns object with error code {1} when ResultObject stderr contains 'ERROR' .");
            }
        }

        private static IEnumerable<TestCaseData> HandleResponseMessageTestCaseData
        {
            get
            {
                yield return new TestCaseData("Failed to process SRS definition", "Failed to process SRS definition").SetName("method 'HandleResponse' returns object with error message {1} when ResultObject stderr contains 'ERROR' and '{0}' .");
                yield return new TestCaseData("Failed to read GeoJSON data", "Failed to read GeoJSON data").SetName("method 'HandleResponse' returns object with error message {1} when ResultObject stderr contains 'ERROR' and '{0}' .");
                yield return new TestCaseData("Failed to reproject feature", "Failed to reproject. Geometry probably out of source or destination SRS").SetName("method 'HandleResponse' returns object with error message {1} when ResultObject stderr contains 'ERROR' and '{0}' .");
                yield return new TestCaseData("", "Contact server administrator").SetName("method 'HandleResponse' returns object with error message {1} when ResultObject stderr contains 'ERROR' .");
            }
        }

        [Test, TestCaseSource(typeof(LibraryOutputHandlerTests), nameof(HandleResponseCodeTestCaseData))]
        public void Test_HandleResponse_error_code_400(string message, int code)
        {
            Random random = new Random();
            ResultObject resultObject = new ResultObject("ERROR " + message + random.Next(), "");

            LibraryOutputHandler libraryOutputHandler = new LibraryOutputHandler();

            BadRequestObjectResult result = (BadRequestObjectResult)libraryOutputHandler.HandleResponse(resultObject, "");

            Assert.AreEqual(code, result.StatusCode);
        }

        [Test, TestCaseSource(typeof(LibraryOutputHandlerTests), nameof(HandleResponseMessageTestCaseData))]
        public void Test_HandleResponse_error_message(string message, string expectedMessage)
        {
            Random random = new Random();
            ResultObject resultObject = new ResultObject("ERROR " + message + random.Next(), "");

            LibraryOutputHandler libraryOutputHandler = new LibraryOutputHandler();

            BadRequestObjectResult result = (BadRequestObjectResult)libraryOutputHandler.HandleResponse(resultObject, "");

            Assert.AreEqual("{ errors = " + expectedMessage + " }", result.Value.ToString());
        }
    }
}
