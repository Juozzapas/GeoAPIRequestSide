

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using WebApplication.CommandBuilder;
using WebApplication.Controllers;
using WebApplication.HandleThirdPartyOutput;
using WebApplication.Model;
using WebApplication.RunThirdParty;

namespace web_api_nunit_test.Controllers
{
    public class ClipControllerTests
    {
        // command name
        private string command;

        // mocks
        private Mock<ResultObject> resultObject;
        private Mock<IActionResult> actionResult;
        private Mock<IConfiguration> configuration;
        private Mock<ICommandCreator> commandCreator;
        private Mock<IProcessPython> processPython;
        private Mock<IHandleOutput> handleOutput;
        private Mock<ClipOperationParam> clipOperationParam;
        private Mock<ClipController> clipController;

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
            commandCreator.Setup(x => x.buildClipCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(command);

            processPython = new Mock<IProcessPython>();
            processPython.Setup(x => x.RunCMD(It.IsAny<string>(), It.IsAny<string>())).Returns(resultObject.Object);

            handleOutput = new Mock<IHandleOutput>();
            handleOutput.Setup(x => x.HandleQgisOutput(It.IsAny<ResultObject>())).Returns(actionResult.Object);

            clipOperationParam = new Mock<ClipOperationParam>();
            clipOperationParam.Object.InputLayer = "InputLayer" + random.Next(1000);
            clipOperationParam.Object.OverlayLayer= "OverlayLayer" + random.Next(1000);

            clipController = new Mock<ClipController>(configuration.Object, commandCreator.Object, processPython.Object, handleOutput.Object);
            clipController.CallBase = true;

        }

        [Test]
        public void Test_Post_CommandBuilder_buildClipCommand_called_once_with_any_parameters()
        {
            clipController.Object.Post(clipOperationParam.Object);

            Mock.Get(commandCreator.Object).Verify(x => x.buildClipCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()),Times.Exactly(1));
        }

        [Test]
        public void Test_Post_CommandBuilder_buildClipCommand_called_once_with_pathToAnalysisScriptFolder_and_scriptName()
        {
            clipController.Object.Post(clipOperationParam.Object);

            Mock.Get(commandCreator.Object).Verify(x => x.buildClipCommand(configuration.Object["pathToAnalysisScriptFolder"], configuration.Object["AnalysisScriptNames:bufferScript"], It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(1));
        }

        [Test]
        public void Test_Post_ProccessPhyton_RunCMD_is_called_once_with_any_parameters()
        {
            clipController.Object.Post(clipOperationParam.Object);

            Mock.Get(processPython.Object).Verify(x => x.RunCMD(It.IsAny<String>(), It.IsAny<String>()), Times.Exactly(1));
        }

        [Test]
        public void Test_Post_ProccessPhyton_RunCMD_is_called_once_with_command_and_qgisPhyton_script_runner()
        {
            clipController.Object.Post(clipOperationParam.Object);

            Mock.Get(processPython.Object).Verify(x => x.RunCMD(command, configuration.Object["ScriptRunners:qgisCmdPath"]), Times.Exactly(1));
        }

        [Test]
        public void Test_Post_OutputHandler_HandleQgisOutput_is_called_once_with_any_result_object()
        {
            clipController.Object.Post(clipOperationParam.Object);

            Mock.Get(handleOutput.Object).Verify(x => x.HandleQgisOutput(It.IsAny<ResultObject>()), Times.Exactly(1));
        }

        [Test]
        public void Test_Post_OutputHandler_HandleQgisOutput_is_called_once_with_result_from_ProccessPhyton_RunCMD()
        {
            clipController.Object.Post(clipOperationParam.Object);

            Mock.Get(handleOutput.Object).Verify(x => x.HandleQgisOutput(resultObject.Object), Times.Exactly(1));
        }

        [Test]
        public void Test_Post_OutputHandler_HandleQgisOutput_response_is_Post_response()
        {
            IActionResult result = clipController.Object.Post(clipOperationParam.Object);

            Assert.AreEqual(actionResult.Object, result);
        }
    }
}
