using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.CommandBuilder;
using WebApplication.Controllers;
using WebApplication.HandleThirdPartyOutput;
using WebApplication.Model;
using WebApplication.RunThirdParty;

namespace web_api_nunit_test.Controllers
{
    public class SelectByLocationControllerTests
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
        private Mock<SelectByLocationOperationParam> selectByLocationOperationParam;
        private Mock<SelectByLocationController> selectByLocationController;

        [SetUp]
        public void BeforeEachTest()
        {
            Random random = new Random();
            command = "command" + random.Next(1000);

            resultObject = new Mock<ResultObject>("test", "test");

            actionResult = new Mock<IActionResult>();

            configuration = new Mock<IConfiguration>();
            configuration.Object["pathToLandFundScriptFolder"] = "pathToLandFundScriptFolder" + random.Next(1000);
            configuration.Object["LandFundScriptNames:analysisScript"] = "LandFundScriptNames:analysisScript" + random.Next(1000);
            configuration.Object["ScriptRunners:qgisCmdPath"] = "ScriptRunners:qgisCmdPath" + random.Next(1000);

            commandCreator = new Mock<ICommandCreator>();
            commandCreator.Setup(x => x.buildSelectByLocationCommand(It.IsAny<SelectByLocationOperationParam>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<String>())).Returns(command);

            processPython = new Mock<IProcessPython>();
            processPython.Setup(x => x.RunCMD(It.IsAny<string>(), It.IsAny<string>())).Returns(resultObject.Object);

            handleOutput = new Mock<IHandleOutput>();
            handleOutput.Setup(x => x.HandleQgisOutput(It.IsAny<ResultObject>())).Returns(actionResult.Object);

            selectByLocationOperationParam = new Mock<SelectByLocationOperationParam>();
            selectByLocationOperationParam.Object.InputLayer = "InputLayer" + random.Next(1000);
            selectByLocationOperationParam.Object.OverlayLayer = "OverlayLayer" + random.Next(1000);


            selectByLocationController = new Mock<SelectByLocationController>(configuration.Object, commandCreator.Object, processPython.Object, handleOutput.Object);
            selectByLocationController.CallBase = true;
        }

        [Test]
        public void Test_Post_CommandBuilder_buildSelectByLocationCommand_called_once_with_any_parameters()
        {
            selectByLocationController.Object.Post(selectByLocationOperationParam.Object);

            Mock.Get(commandCreator.Object).Verify(x => x.buildSelectByLocationCommand(It.IsAny<SelectByLocationOperationParam>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(1));
        }

        [Test]
        public void Test_Post_CommandBuilder_buildSelectByLocationCommand_called_once_with_selectByLocationOperationParam_and_scriptFolder_and_scriptName()
        {
            selectByLocationController.Object.Post(selectByLocationOperationParam.Object);

            Mock.Get(commandCreator.Object).Verify(x => x.buildSelectByLocationCommand(selectByLocationOperationParam.Object, configuration.Object["pathToLandFundScriptFolder"], configuration.Object["LandFundScriptNames:analysisScript"], It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(1));
        }

        [Test]
        public void Test_Post_ProccessPhyton_RunCMD_is_called_once_with_any_parameters()
        {
            selectByLocationController.Object.Post(selectByLocationOperationParam.Object);

            Mock.Get(processPython.Object).Verify(x => x.RunCMD(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(1));
        }

        [Test]
        public void Test_Post_ProccessPhyton_RunCMD_is_called_once_with_command_and_qgisPhyton_script_runner()
        {
            selectByLocationController.Object.Post(selectByLocationOperationParam.Object);

            Mock.Get(processPython.Object).Verify(x => x.RunCMD(command, configuration.Object["ScriptRunners:qgisCmdPath"]), Times.Exactly(1));
        }

        [Test]
        public void Test_Post_OutputHandler_HandleQgisOutput_is_called_once_with_any_result_object()
        {
            selectByLocationController.Object.Post(selectByLocationOperationParam.Object);

            Mock.Get(handleOutput.Object).Verify(x => x.HandleQgisOutput(It.IsAny<ResultObject>()), Times.Exactly(1));
        }

        [Test]
        public void Test_Post_OutputHandler_HandleQgisOutput_is_called_once_with_result_from_ProccessPhyton_RunCMD()
        {
            selectByLocationController.Object.Post(selectByLocationOperationParam.Object);

            Mock.Get(handleOutput.Object).Verify(x => x.HandleQgisOutput(resultObject.Object), Times.Exactly(1));
        }

        [Test]
        public void Test_Post_OutputHandler_HandleQgisOutput_response_is_Post_response()
        {
            IActionResult result = selectByLocationController.Object.Post(selectByLocationOperationParam.Object);

            Assert.AreEqual(actionResult.Object, result);
        }
    }
}
