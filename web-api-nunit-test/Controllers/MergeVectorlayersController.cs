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
    public class MergeVectorlayersController
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
        private Mock<MergeVectorLayerOperationParam> mergeVectorLayerOperationParam;
        private Mock<MergeVectorLayersController> mergeVectorLayersController;

        [SetUp]
        public void BeforeEachTest()
        {
            Random random = new Random();
            command = "command" + random.Next(1000);

            resultObject = new Mock<ResultObject>("test", "test");

            actionResult = new Mock<IActionResult>();

            configuration = new Mock<IConfiguration>();
            configuration.Object["pathToAnalysisScriptFolder"] = "pathToAnalysisScriptFolder" + random.Next(1000);
            configuration.Object["AnalysisScriptNames:mergeScript"] = "AnalysisScriptNames:mergeScript" + random.Next(1000);
            configuration.Object["ScriptRunners:qgisCmdPath"] = "ScriptRunners:qgisCmdPath" + random.Next(1000);

            commandCreator = new Mock<ICommandCreator>();
            commandCreator.Setup(x => x.buildMergeCommand(It.IsAny<MergeVectorLayerOperationParam>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(command);

            processPython = new Mock<IProcessPython>();
            processPython.Setup(x => x.RunCMD(It.IsAny<string>(), It.IsAny<string>())).Returns(resultObject.Object);

            handleOutput = new Mock<IHandleOutput>();
            handleOutput.Setup(x => x.HandleQgisOutput(It.IsAny<ResultObject>())).Returns(actionResult.Object);

            mergeVectorLayerOperationParam = new Mock<MergeVectorLayerOperationParam>();
            mergeVectorLayerOperationParam.Object.InputLayers = "InputLayers" + random.Next(1000);


            mergeVectorLayersController = new Mock<MergeVectorLayersController>(configuration.Object, commandCreator.Object, processPython.Object, handleOutput.Object);
            mergeVectorLayersController.CallBase = true;
        }

        [Test]
        public void Test_Post_CommandBuilder_buildMergeCommand_called_once_with_any_parameters()
        {
            mergeVectorLayersController.Object.Post(mergeVectorLayerOperationParam.Object);

            Mock.Get(commandCreator.Object).Verify(x => x.buildMergeCommand(It.IsAny<MergeVectorLayerOperationParam>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(1));
        }

        [Test]
        public void Test_Post_CommandBuilder_buildMergeCommand_called_once_with_mergeVectorLayerOperationParam_and_scriptFolder_and_scriptName()
        {
            mergeVectorLayersController.Object.Post(mergeVectorLayerOperationParam.Object);

            Mock.Get(commandCreator.Object).Verify(x => x.buildMergeCommand(mergeVectorLayerOperationParam.Object, configuration.Object["pathToAnalysisScriptFolder"], configuration.Object["AnalysisScriptNames:mergeScript"], It.IsAny<string>()), Times.Exactly(1));
        }

        [Test]
        public void Test_Post_ProccessPhyton_RunCMD_is_called_once_with_any_parameters()
        {
            mergeVectorLayersController.Object.Post(mergeVectorLayerOperationParam.Object);

            Mock.Get(processPython.Object).Verify(x => x.RunCMD(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(1));
        }

        [Test]
        public void Test_Post_ProccessPhyton_RunCMD_is_called_once_with_command_and_qgisPhyton_script_runner()
        {
            mergeVectorLayersController.Object.Post(mergeVectorLayerOperationParam.Object);

            Mock.Get(processPython.Object).Verify(x => x.RunCMD(command, configuration.Object["ScriptRunners:qgisCmdPath"]), Times.Exactly(1));
        }

        [Test]
        public void Test_Post_OutputHandler_HandleQgisOutput_is_called_once_with_any_result_object()
        {
            mergeVectorLayersController.Object.Post(mergeVectorLayerOperationParam.Object);

            Mock.Get(handleOutput.Object).Verify(x => x.HandleQgisOutput(It.IsAny<ResultObject>()), Times.Exactly(1));
        }

        [Test]
        public void Test_Post_OutputHandler_HandleQgisOutput_is_called_once_with_result_from_ProccessPhyton_RunCMD()
        {
            mergeVectorLayersController.Object.Post(mergeVectorLayerOperationParam.Object);

            Mock.Get(handleOutput.Object).Verify(x => x.HandleQgisOutput(resultObject.Object), Times.Exactly(1));
        }

        [Test]
        public void Test_Post_OutputHandler_HandleQgisOutput_response_is_Post_response()
        {
            IActionResult result = mergeVectorLayersController.Object.Post(mergeVectorLayerOperationParam.Object);

            Assert.AreEqual(actionResult.Object, result);
        }
    }
}
