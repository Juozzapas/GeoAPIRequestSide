using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.HandleThirdPartyOutput.HandleLogic;
using WebApplication.Model;

namespace web_api_nunit_test.HandleThirdPartyOutput
{
    public class ScriptOutputHandlerTests
    {

        [Test, TestCase(TestName = "method  'getQgisResultGeojson' returns stdout pelnumate line when that line is not empty.")]
        public void Test_getQgisResultGeojson_gets_pelnumate_line_when_that_line_is_defined()
        {
            Random random = new Random();
            string expectedResult = "result" + random.Next();
            ResultObject resultObject = new ResultObject("test", "test \n" + expectedResult + "\n");

            ScriptOutputHandler scriptOutputHandler = new ScriptOutputHandler();
            string result = scriptOutputHandler.getQgisResultGeojson(resultObject, "bad" + random.Next());

            Assert.AreEqual(expectedResult, result);
        }

        [Test, TestCase(TestName = "method  'getQgisResultGeojson' returns empty string when passed message is the same as stdout pelnumate line.")]
        public void Test_getQgisResultGeojson_returns_empty_string_when_pelnumate_line_is_same_as_message()
        {
            Random random = new Random();
            string messageParameter = "message" + random.Next();
            ResultObject resultObject = new ResultObject("test", "test \n" + messageParameter + "\n");

            ScriptOutputHandler scriptOutputHandler = new ScriptOutputHandler();
            string result = scriptOutputHandler.getQgisResultGeojson(resultObject, messageParameter);

            Assert.AreEqual("", result);
        }

        [Test, TestCase(TestName = "method  'getQgisResultGeojson' returns stdout pelnumate line string without message parameter.")]
        public void Test_getQgisResultGeojson_message_in_pelnumate_line_is_swapped_with_empty_string()
        {
            Random random = new Random();
            string expectedResult = "result" + random.Next();
            string messageParameter = "message" + random.Next();
            ResultObject resultObject = new ResultObject("test", "test \n" + expectedResult + messageParameter + "\n");

            ScriptOutputHandler scriptOutputHandler = new ScriptOutputHandler();
            string result = scriptOutputHandler.getQgisResultGeojson(resultObject, messageParameter);

            Assert.AreEqual(expectedResult, result);
        }

        [Test, TestCase(TestName = "method 'HandleResponse' returns object with defined content when ResultObject stdout contains 'RESULT_GEOJSON'.")]
        public void Test_HandleResponse_returns_defined_content()
        {
            Random random = new Random();
            string getQgisResultGeojsonresponse = "response" + random.Next();
            Mock<ScriptOutputHandler> mockScriptOutputHandler = new Mock<ScriptOutputHandler>();
            mockScriptOutputHandler.Setup(x => x.getQgisResultGeojson(It.IsAny<ResultObject>(), It.IsAny<string>())).Returns(getQgisResultGeojsonresponse);
            mockScriptOutputHandler.CallBase = true;

            ResultObject resultObject = new ResultObject("", "RESULT_GEOJSON");

            ContentResult result = (ContentResult)mockScriptOutputHandler.Object.HandleResponse(resultObject);

            Assert.AreEqual(getQgisResultGeojsonresponse, result.Content);
        }

        [Test, TestCase(TestName = "method 'HandleResponse' returns object with content type 'application/json' when ResultObject stdout contains 'RESULT_GEOJSON'.")]
        public void Test_HandleResponse_returns_defined_content_type()
        {
            Random random = new Random();
            string getQgisResultGeojsonresponse = "response" + random.Next();
            Mock<ScriptOutputHandler> mockScriptOutputHandler = new Mock<ScriptOutputHandler>();
            mockScriptOutputHandler.Setup(x => x.getQgisResultGeojson(It.IsAny<ResultObject>(), It.IsAny<string>())).Returns(getQgisResultGeojsonresponse);
            mockScriptOutputHandler.CallBase = true;

            ResultObject resultObject = new ResultObject("", "RESULT_GEOJSON");

            ContentResult result = (ContentResult)mockScriptOutputHandler.Object.HandleResponse(resultObject);

            Assert.AreEqual("application/json", result.ContentType);
        }

        [Test, TestCase(TestName = "method 'HandleResponse' returns object with 200 code when ResultObject stdout contains 'RESULT_GEOJSON'.")]
        public void Test_HandleResponse_returns_200_code()
        {
            Random random = new Random();
            string getQgisResultGeojsonresponse = "response" + random.Next();
            Mock<ScriptOutputHandler> mockScriptOutputHandler = new Mock<ScriptOutputHandler>();
            mockScriptOutputHandler.Setup(x => x.getQgisResultGeojson(It.IsAny<ResultObject>(), It.IsAny<string>())).Returns(getQgisResultGeojsonresponse);
            mockScriptOutputHandler.CallBase = true;

            ResultObject resultObject = new ResultObject("", "RESULT_GEOJSON");

            ContentResult result = (ContentResult)mockScriptOutputHandler.Object.HandleResponse(resultObject);

            Assert.AreEqual(200, result.StatusCode);
        }

        [Test, TestCase(TestName = "method 'HandleResponse' returns object with code 400 when ResultObject stdout contains 'SCRIPT_ERROR' and 'prosessing algorithm error'.")]
        public void Test_HandleResponse_returns_error_with_code_400_1()
        {
            Random random = new Random();
            string getQgisResultGeojsonresponse = "response" + random.Next();
            Mock<ScriptOutputHandler> mockScriptOutputHandler = new Mock<ScriptOutputHandler>();
            mockScriptOutputHandler.Setup(x => x.getQgisResultGeojson(It.IsAny<ResultObject>(), It.IsAny<string>())).Returns(getQgisResultGeojsonresponse);
            mockScriptOutputHandler.CallBase = true;

            ResultObject resultObject = new ResultObject("", "SCRIPT_ERROR, prosessing algorithm error");

            BadRequestObjectResult result = (BadRequestObjectResult)mockScriptOutputHandler.Object.HandleResponse(resultObject);

            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual("{ errors = Operation unavailable }", result.Value.ToString());
        }

        [Test, TestCase(TestName = "method 'HandleResponse' returns object with code 400 when ResultObject stdout contains 'SCRIPT_ERROR'.")]
        public void Test_HandleResponse_returns_error_with_code_400_2()
        {
            Random random = new Random();
            string getQgisResultGeojsonresponse = "response" + random.Next();
            Mock<ScriptOutputHandler> mockScriptOutputHandler = new Mock<ScriptOutputHandler>();
            mockScriptOutputHandler.Setup(x => x.getQgisResultGeojson(It.IsAny<ResultObject>(), It.IsAny<string>())).Returns(getQgisResultGeojsonresponse);
            mockScriptOutputHandler.CallBase = true;

            ResultObject resultObject = new ResultObject("", "SCRIPT_ERROR");

            BadRequestObjectResult result = (BadRequestObjectResult)mockScriptOutputHandler.Object.HandleResponse(resultObject);

            Assert.AreEqual(400, result.StatusCode);
        }

        [Test, TestCase(TestName = "method 'HandleResponse' returns object with code 400 when ResultObject stdout does not contain 'RESULT_GEOJSON' or 'SCRIPT_ERROR'.")]
        public void Test_HandleResponse_returns_error_with_code_400_3()
        {
            Random random = new Random();
            string getQgisResultGeojsonresponse = "response" + random.Next();
            Mock<ScriptOutputHandler> mockScriptOutputHandler = new Mock<ScriptOutputHandler>();
            mockScriptOutputHandler.Setup(x => x.getQgisResultGeojson(It.IsAny<ResultObject>(), It.IsAny<string>())).Returns(getQgisResultGeojsonresponse);
            mockScriptOutputHandler.CallBase = true;

            ResultObject resultObject = new ResultObject("", getQgisResultGeojsonresponse);

            BadRequestObjectResult result = (BadRequestObjectResult)mockScriptOutputHandler.Object.HandleResponse(resultObject);

            Assert.AreEqual(400, result.StatusCode);
        }
    }
}
