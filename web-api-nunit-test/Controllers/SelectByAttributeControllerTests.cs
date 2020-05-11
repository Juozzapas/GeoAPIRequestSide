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
    public class SelectByAttributeControllerTests
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
        private Mock<SelectByAttributeOperationParam> selectByAttributeOperationParam;
        private Mock<SelectByAttributeController> selectByAttributeController;

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
            commandCreator.Setup(x => x.buildSelectByAttributeCommand(It.IsAny<SelectByAttributeOperationParam>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(command);

            processPython = new Mock<IProcessPython>();
            processPython.Setup(x => x.RunCMD(It.IsAny<string>(), It.IsAny<string>())).Returns(resultObject.Object);

            handleOutput = new Mock<IHandleOutput>();
            handleOutput.Setup(x => x.HandleQgisOutput(It.IsAny<ResultObject>())).Returns(actionResult.Object);

            selectByAttributeOperationParam = new Mock<SelectByAttributeOperationParam>();
            selectByAttributeOperationParam.Object.InputLayer = "InputLayer" + random.Next(1000);


            selectByAttributeController = new Mock<SelectByAttributeController>(configuration.Object, commandCreator.Object, processPython.Object, handleOutput.Object);
            selectByAttributeController.CallBase = true;
        }

        [Test]
        public void Test_Post_CommandBuilder_buildIntersectionCommand_called_once_with_any_parameters()
        {
            selectByAttributeController.Object.Post(selectByAttributeOperationParam.Object);

            Mock.Get(commandCreator.Object).Verify(x => x.buildSelectByAttributeCommand(It.IsAny<SelectByAttributeOperationParam>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(1));
        }

        [Test]
        public void Test_Post_CommandBuilder_buildIntersectionCommand_called_once_with_selectByAttributeOperationParam_and_scriptFolder_and_scriptName()
        {
            selectByAttributeController.Object.Post(selectByAttributeOperationParam.Object);

            Mock.Get(commandCreator.Object).Verify(x => x.buildSelectByAttributeCommand(selectByAttributeOperationParam.Object, configuration.Object["pathToLandFundScriptFolder"], configuration.Object["LandFundScriptNames:analysisScript"], It.IsAny<string>()), Times.Exactly(1));
        }

        [Test]
        public void Test_Post_ProccessPhyton_RunCMD_is_called_once_with_any_parameters()
        {
            selectByAttributeController.Object.Post(selectByAttributeOperationParam.Object);

            Mock.Get(processPython.Object).Verify(x => x.RunCMD(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(1));
        }

        [Test]
        public void Test_Post_ProccessPhyton_RunCMD_is_called_once_with_command_and_qgisPhyton_script_runner()
        {
            selectByAttributeController.Object.Post(selectByAttributeOperationParam.Object);

            Mock.Get(processPython.Object).Verify(x => x.RunCMD(command, configuration.Object["ScriptRunners:qgisCmdPath"]), Times.Exactly(1));
        }

        [Test]
        public void Test_Post_OutputHandler_HandleQgisOutput_is_called_once_with_any_result_object()
        {
            selectByAttributeController.Object.Post(selectByAttributeOperationParam.Object);

            Mock.Get(handleOutput.Object).Verify(x => x.HandleQgisOutput(It.IsAny<ResultObject>()), Times.Exactly(1));
        }

        [Test]
        public void Test_Post_OutputHandler_HandleQgisOutput_is_called_once_with_result_from_ProccessPhyton_RunCMD()
        {
            selectByAttributeController.Object.Post(selectByAttributeOperationParam.Object);

            Mock.Get(handleOutput.Object).Verify(x => x.HandleQgisOutput(resultObject.Object), Times.Exactly(1));
        }

        [Test]
        public void Test_Post_OutputHandler_HandleQgisOutput_response_is_Post_response()
        {
            IActionResult result = selectByAttributeController.Object.Post(selectByAttributeOperationParam.Object);

            Assert.AreEqual(actionResult.Object, result);
        }
    }
}
