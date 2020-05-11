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
    public class TransformControllerTests
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
        private Mock<TransformOperationParam> transformOperationParam;
        private Mock<TransformController> transformController;

        [SetUp]
        public void BeforeEachTest()
        {
            Random random = new Random();
            command = "command" + random.Next(1000);

            resultObject = new Mock<ResultObject>("test", "test");

            actionResult = new Mock<IActionResult>();

            configuration = new Mock<IConfiguration>();
            configuration.Object["ScriptRunners:OSGeo4WPath"] = "ScriptRunners:OSGeo4WPath" + random.Next(1000);

            commandCreator = new Mock<ICommandCreator>();
            commandCreator.Setup(x => x.buildOgr2Ogr(It.IsAny<TransformOperationParam>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(command);

            processPython = new Mock<IProcessPython>();
            processPython.Setup(x => x.RunCMD(It.IsAny<string>(), It.IsAny<string>())).Returns(resultObject.Object);

            handleOutput = new Mock<IHandleOutput>();
            handleOutput.Setup(x => x.HandleGdalOutput(It.IsAny<ResultObject>(), It.IsAny<string>())).Returns(actionResult.Object);

            transformOperationParam = new Mock<TransformOperationParam>();
            transformOperationParam.Object.InputLayer = "InputLayer" + random.Next(1000);


            transformController = new Mock<TransformController>(configuration.Object, commandCreator.Object, processPython.Object, handleOutput.Object);
            transformController.CallBase = true;
        }

        [Test]
        public void Test_Posting_CommandBuilder_buildOgr2Ogr_called_once_with_any_parameters()
        {
            transformController.Object.Posting(transformOperationParam.Object);

            Mock.Get(commandCreator.Object).Verify(x => x.buildOgr2Ogr(It.IsAny<TransformOperationParam>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(1));
        }

        [Test]
        public void Test_Posting_CommandBuilder_buildOgr2Ogr_called_once_with_transformOperationParam_and_ogr2ogr()
        {
            transformController.Object.Posting(transformOperationParam.Object);

            Mock.Get(commandCreator.Object).Verify(x => x.buildOgr2Ogr(transformOperationParam.Object, "ogr2ogr", It.IsAny<string>(), It.IsAny <string>()), Times.Exactly(1));
        }

        [Test]
        public void Test_Posting_ProccessPhyton_RunCMD_is_called_once_with_any_parameters()
        {
            transformController.Object.Posting(transformOperationParam.Object);

            Mock.Get(processPython.Object).Verify(x => x.RunCMD(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(1));
        }

        [Test]
        public void Test_Posting_ProccessPhyton_RunCMD_is_called_once_with_command_and_OSGeo4WPath()
        {
            transformController.Object.Posting(transformOperationParam.Object);

            Mock.Get(processPython.Object).Verify(x => x.RunCMD(command, configuration.Object["ScriptRunners:OSGeo4WPath"]), Times.Exactly(1));
        }

        [Test]
        public void Test_Posting_OutputHandler_HandleQgisOutput_is_called_once_with_any_result_object()
        {
            transformController.Object.Posting(transformOperationParam.Object);

            Mock.Get(handleOutput.Object).Verify(x => x.HandleGdalOutput(It.IsAny<ResultObject>(), It.IsAny<string>()), Times.Exactly(1));
        }

        [Test]
        public void Test_Posting_OutputHandler_HandleQgisOutput_is_called_once_with_result_from_ProccessPhyton_RunCMD()
        {
            transformController.Object.Posting(transformOperationParam.Object);

            Mock.Get(handleOutput.Object).Verify(x => x.HandleGdalOutput(resultObject.Object, It.IsAny<string>()), Times.Exactly(1));
        }

        [Test]
        public void Test_Posting_OutputHandler_HandleQgisOutput_response_is_Post_response()
        {
            IActionResult result = transformController.Object.Posting(transformOperationParam.Object);

            Assert.AreEqual(actionResult.Object, result);
        }
    }
}
