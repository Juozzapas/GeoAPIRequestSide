using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;
using WebApplication.CommandBuilder;
using WebApplication.Controllers;
using WebApplication.HandleThirdPartyOutput;
using WebApplication.Model;
using WebApplication.RunThirdParty;

namespace web_api_nunit_test.Controllers
{
    public class BufferControllerTests
    {
        // command name
        private string command;

        // mock elements
        private Mock<ResultObject> resultObject;
        private Mock<IActionResult> actionResult;
        private Mock<IConfiguration> configuration;
        private Mock<ICommandCreator> commandCreator;
        private Mock<IProcessPython> processPython;
        private Mock<IHandleOutput> handleOutput;
        private Mock<BufferOperationParam> bufferOperationParam;
        private Mock<BufferController> bufferController;


       [SetUp]
        public void BeforeEachTest()
        {
            Random random = new Random();
            command = "command" + random.Next(1000);

            resultObject = new Mock<ResultObject>("test", "test");

           actionResult = new Mock<IActionResult>();

            configuration = new Mock<IConfiguration>();
            configuration.Object["pathToAnalysisScriptFolder"] = "pathToAnalysisScriptFolder" + random.Next(1000);
            configuration.Object["AnalysisScriptNames:bufferScript"] = "AnalysisScriptNames:bufferScript" + random.Next(1000);
            configuration.Object["ScriptRunners:qgisCmdPath"] = "ScriptRunners:qgisCmdPath" + random.Next(1000);

            commandCreator = new Mock<ICommandCreator>();
            commandCreator.Setup(x => x.buildBufferCommand(It.IsAny<BufferOperationParam>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(command);

            processPython = new Mock<IProcessPython>();
            processPython.Setup(x => x.RunCMD(It.IsAny<string>(), It.IsAny<string>())).Returns(resultObject.Object);

            handleOutput = new Mock<IHandleOutput>();
            handleOutput.Setup(x => x.HandleQgisOutput(It.IsAny<ResultObject>())).Returns(actionResult.Object);

            bufferOperationParam = new Mock<BufferOperationParam>();
            bufferOperationParam.Object.InputLayer = "inputlayer" + random.Next(1000);

            bufferController = new Mock<BufferController>(configuration.Object, commandCreator.Object, processPython.Object, handleOutput.Object);
            bufferController.CallBase = true;
        }

        [Test]
        public void Test_Post_CommandCreator_buildBufferCommand_is_called_once_with_any_parameters()
        {
            bufferController.Object.Post(bufferOperationParam.Object);

            Mock.Get(commandCreator.Object).Verify(x => x.buildBufferCommand(It.IsAny<BufferOperationParam>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>()), Times.Exactly(1));
        }

        [Test]
        public void Test_Post_CommandCreator_buildBufferCommand_is_called_once_with_exact_params()
        {
            bufferController.Object.Post(bufferOperationParam.Object);

            Mock.Get(commandCreator.Object).Verify(x => x.buildBufferCommand(bufferOperationParam.Object, configuration.Object["pathToAnalysisScriptFolder"], configuration.Object["AnalysisScriptNames:bufferScript"], It.IsAny<String>()), Times.Exactly(1));
        }

        [Test]
        public void Test_Post_ProccessPhyton_RunCMD_is_called_once_with_any_parameters()
        {
            bufferController.Object.Post(bufferOperationParam.Object);

            Mock.Get(processPython.Object).Verify(x => x.RunCMD(It.IsAny<String>(), It.IsAny<String>()), Times.Exactly(1));
        }

        [Test]
        public void Test_Post_ProccessPhyton_RunCMD_is_called_once_with_exact_parameters()
        {
            bufferController.Object.Post(bufferOperationParam.Object);

            Mock.Get(processPython.Object).Verify(x => x.RunCMD(command, configuration.Object["ScriptRunners:qgisCmdPath"]), Times.Exactly(1));
        }

        [Test]
        public void Test_Post_OutputHandler_HandleQgisOutput_is_called_once_with_any_result_object()
        {
            bufferController.Object.Post(bufferOperationParam.Object);

            Mock.Get(handleOutput.Object).Verify(x => x.HandleQgisOutput(It.IsAny<ResultObject>()), Times.Exactly(1));
        }

        [Test]
        public void Test_Post_OutputHandler_HandleQgisOutput_is_called_once_with_result_from_ProccessPhyton_RunCMD()
        {
            bufferController.Object.Post(bufferOperationParam.Object);

            Mock.Get(handleOutput.Object).Verify(x => x.HandleQgisOutput(resultObject.Object), Times.Exactly(1));
        }

        [Test]
        public void Test_Post_OutputHandler_HandleQgisOutput_response_is_Post_response()
        {
            IActionResult result = bufferController.Object.Post(bufferOperationParam.Object);

            Assert.AreEqual(actionResult.Object, result);
        }
    }
}
