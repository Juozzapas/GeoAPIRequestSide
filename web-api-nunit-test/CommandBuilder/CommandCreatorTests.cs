using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.CommandBuilder;
using WebApplication.Model;

namespace web_api_nunit_test.CommandBuilders
{
    public class CommandCreatorTests
    {
        [Test]
        public void Test_buildOgr2Ogr_when_program_is_defined_then_CommandBuilder_AddProgramName_is_called_once()
        {
            Random random = new Random();
            string program = "program" + random.Next(1000);
            string dst_data = "dst_data" + random.Next(1000);
            string src_data = "src_data" + random.Next(1000);

            Mock<TransformOperationParam> transformOperationParam = new Mock<TransformOperationParam>();

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildOgr2Ogr(transformOperationParam.Object, program, dst_data, src_data);

            Mock.Get(commandBuilder.Object).Verify(x => x.AddProgramName(program), Times.Once);
        }

        [Test]
        public void Test_buildOgr2Ogr_CommandBuilder_AdditionalArguments_with_response_type_is_called_once()
        {
            Random random = new Random();
            string program = "program" + random.Next(1000);
            string dst_data = "dst_data" + random.Next(1000);
            string src_data = "src_data" + random.Next(1000);

            Mock<TransformOperationParam> transformOperationParam = new Mock<TransformOperationParam>();

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildOgr2Ogr(transformOperationParam.Object, program, dst_data, src_data);

            Mock.Get(commandBuilder.Object).Verify(x => x.AdditionalArguments("-f", transformOperationParam.Object.Type), Times.Once);
        }

        [Test]
        public void Test_buildOgr2Ogr_when_transformOperationParam_source_and_target_null_then_CommandBuilder_AdditionalArguments_is_called_once()
        {
            Random random = new Random();
            string program = "program" + random.Next(1000);
            string dst_data = "dst_data" + random.Next(1000);
            string src_data = "src_data" + random.Next(1000);

            Mock<TransformOperationParam> transformOperationParam = new Mock<TransformOperationParam>();

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildOgr2Ogr(transformOperationParam.Object, program, dst_data, src_data);

            Mock.Get(commandBuilder.Object).Verify(x => x.AdditionalArguments(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void Test_buildOgr2Ogr_when_transformOperationParam_target_defined_then_CommandBuilder_AdditionalArguments_is_called_twice()
        {
            Random random = new Random();
            string program = "program" + random.Next(1000);
            string dst_data = "dst_data" + random.Next(1000);
            string src_data = "src_data" + random.Next(1000);

            Mock<TransformOperationParam> transformOperationParam = new Mock<TransformOperationParam>();
            transformOperationParam.Object.TargetCrs = "target" + random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildOgr2Ogr(transformOperationParam.Object, program, dst_data, src_data);

            Mock.Get(commandBuilder.Object).Verify(x => x.AdditionalArguments(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void Test_buildOgr2Ogr_when_transformOperationParam_target_defined_then_CommandBuilder_AdditionalArguments_with_target_is_called_once()
        {
            Random random = new Random();
            string program = "program" + random.Next(1000);
            string dst_data = "dst_data" + random.Next(1000);
            string src_data = "src_data" + random.Next(1000);

            Mock<TransformOperationParam> transformOperationParam = new Mock<TransformOperationParam>();
            transformOperationParam.Object.TargetCrs = "target" + random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildOgr2Ogr(transformOperationParam.Object, program, dst_data, src_data);

            Mock.Get(commandBuilder.Object).Verify(x => x.AdditionalArguments("-t_srs", transformOperationParam.Object.TargetCrs), Times.Exactly(1));
        }

        [Test]
        public void Test_buildOgr2Ogr_when_transformOperationParam_source_and_target_defined_then_CommandBuilder_AdditionalArguments_is_called_three_times()
        {
            Random random = new Random();
            string program = "program" + random.Next(1000);
            string dst_data = "dst_data" + random.Next(1000);
            string src_data = "src_data" + random.Next(1000);

            Mock<TransformOperationParam> transformOperationParam = new Mock<TransformOperationParam>();
            transformOperationParam.Object.TargetCrs = "target" + random.Next(1000);
            transformOperationParam.Object.SourceCrs = "source" + random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildOgr2Ogr(transformOperationParam.Object, program, dst_data, src_data);

            Mock.Get(commandBuilder.Object).Verify(x => x.AdditionalArguments(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(3));
        }

        [Test]
        public void Test_buildOgr2Ogr_when_transformOperationParam_source_and_target_defined_then_CommandBuilder_AdditionalArguments_is_called_with_source_and_with_target()
        {
            Random random = new Random();
            string program = "program" + random.Next(1000);
            string dst_data = "dst_data" + random.Next(1000);
            string src_data = "src_data" + random.Next(1000);

            Mock<TransformOperationParam> transformOperationParam = new Mock<TransformOperationParam>();
            transformOperationParam.Object.TargetCrs = "target" + random.Next(1000);
            transformOperationParam.Object.SourceCrs = "source" + random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildOgr2Ogr(transformOperationParam.Object, program, dst_data, src_data);

            Mock.Get(commandBuilder.Object).Verify(x => x.AdditionalArguments("-t_srs", transformOperationParam.Object.TargetCrs), Times.Exactly(1));
            Mock.Get(commandBuilder.Object).Verify(x => x.AdditionalArguments("-s_srs", transformOperationParam.Object.SourceCrs), Times.Exactly(1));
        }

        [Test]
        public void Test_buildOgr2Ogr_when_transformOperationParam_SkipFailures_defined_then_CommandBuilder_SignleParameter_is_called_once()
        {
            Random random = new Random();
            string program = "program" + random.Next(1000);
            string dst_data = "dst_data" + random.Next(1000);
            string src_data = "src_data" + random.Next(1000);

            Mock<TransformOperationParam> transformOperationParam = new Mock<TransformOperationParam>();
            transformOperationParam.Object.SkipFailures = "skip" + random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildOgr2Ogr(transformOperationParam.Object, program, dst_data, src_data);

            Mock.Get(commandBuilder.Object).Verify(x => x.SignleParameter(transformOperationParam.Object.SkipFailures), Times.Exactly(1));
        }

        [Test]
        public void Test_buildOgr2Ogr_when_transformOperationParam_SkipFailures_null_then_CommandBuilder_SignleParameter_is_never_called()
        {
            Random random = new Random();
            string program = "program" + random.Next(1000);
            string dst_data = "dst_data" + random.Next(1000);
            string src_data = "src_data" + random.Next(1000);

            Mock<TransformOperationParam> transformOperationParam = new Mock<TransformOperationParam>();

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildOgr2Ogr(transformOperationParam.Object, program, dst_data, src_data);

            Mock.Get(commandBuilder.Object).Verify(x => x.SignleParameter(It.IsAny<string>()), Times.Never);
        }
        [Test]
        public void Test_buildOgr2Ogr_CommandBuilder_AddDataSource_is_called_once_with_both_data_sources()
        {
            Random random = new Random();
            string program = "program" + random.Next(1000);
            string dst_data = "dst_data" + random.Next(1000);
            string src_data = "src_data" + random.Next(1000);

            Mock<TransformOperationParam> transformOperationParam = new Mock<TransformOperationParam>();

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildOgr2Ogr(transformOperationParam.Object, program, dst_data, src_data);

            Mock.Get(commandBuilder.Object).Verify(x => x.AddDataSource(dst_data, src_data), Times.Once);
        }
        [Test]
        public void Test_buildOgr2Ogr_CommandBuilder_GetResult_is_called_once_()
        {
            Random random = new Random();
            string program = "program" + random.Next(1000);
            string dst_data = "dst_data" + random.Next(1000);
            string src_data = "src_data" + random.Next(1000);

            Mock<TransformOperationParam> transformOperationParam = new Mock<TransformOperationParam>();

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildOgr2Ogr(transformOperationParam.Object, program, dst_data, src_data);

            Mock.Get(commandBuilder.Object).Verify(x => x.GetResult(), Times.Once);
        }

        [Test]
        public void Test_buildOgr2Ogr_result_from_CommandBuilder_is_buildOgr2Ogr_result()
        {
            Random random = new Random();
            string program = "program" + random.Next(1000);
            string dst_data = "dst_data" + random.Next(1000);
            string src_data = "src_data" + random.Next(1000);
            string resultValueExpected = "resultValueExpected" + random.Next(1000);

            Mock<TransformOperationParam> transformOperationParam = new Mock<TransformOperationParam>();

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();
            commandBuilder.Setup(x => x.GetResult()).Returns(resultValueExpected);

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            string result = commandCreator.Object.buildOgr2Ogr(transformOperationParam.Object, program, dst_data, src_data);

            Assert.AreEqual(resultValueExpected, result);
        }

        [Test]
        public void Test_buildBufferCommand_CommandBuilder_AddProgramName_is_called_once_with_formed_name()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);

            Mock<BufferOperationParam> bufferOperationParam = new Mock<BufferOperationParam>();
            bufferOperationParam.Object.Distance = random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildBufferCommand(bufferOperationParam.Object, scriptFolder, scriptName, imputLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.AddProgramName(scriptFolder + @"\" + scriptName), Times.Once);
        }

        [Test]
        public void Test_buildBufferCommand_CommandBuilder_SingleParameter_is_called_twice()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);

            Mock<BufferOperationParam> bufferOperationParam = new Mock<BufferOperationParam>();
            bufferOperationParam.Object.Distance = random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildBufferCommand(bufferOperationParam.Object, scriptFolder, scriptName, imputLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.SignleParameter(It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void Test_buildBufferCommand_CommandBuilder_SingleParameter_is_called_once_with_defined_inputLayerFile()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);

            Mock<BufferOperationParam> bufferOperationParam = new Mock<BufferOperationParam>();
            bufferOperationParam.Object.Distance = random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildBufferCommand(bufferOperationParam.Object, scriptFolder, scriptName, imputLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.SignleParameter(imputLayerFile), Times.Once);
        }

        [Test]
        public void Test_buildBufferCommand_CommandBuilder_SingleParameter_is_called_once_with_defined_bufferOperationParam_distance()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);

            Mock<BufferOperationParam> bufferOperationParam = new Mock<BufferOperationParam>();
            bufferOperationParam.Object.Distance = random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildBufferCommand(bufferOperationParam.Object, scriptFolder, scriptName, imputLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.SignleParameter(bufferOperationParam.Object.Distance.ToString()), Times.Once);
        }

        [Test]
        public void Test_buildBufferCommand_CommandBuilder_GetResult_is_called_once()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);

            Mock<BufferOperationParam> bufferOperationParam = new Mock<BufferOperationParam>();
            bufferOperationParam.Object.Distance = random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildBufferCommand(bufferOperationParam.Object, scriptFolder, scriptName, imputLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.GetResult(), Times.Once);
        }

        [Test]
        public void Test_buildBufferCommand_result_from_CommandBuilder_is_buildBufferCommand_result()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);
            string resultValueExpected = "resultValueExpected" + random.Next(1000);

            Mock<BufferOperationParam> bufferOperationParam = new Mock<BufferOperationParam>();
            bufferOperationParam.Object.Distance = random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();
            commandBuilder.Setup(x => x.GetResult()).Returns(resultValueExpected);

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            string result = commandCreator.Object.buildBufferCommand(bufferOperationParam.Object, scriptFolder, scriptName, imputLayerFile);

            Assert.AreEqual(resultValueExpected, result);
        }

        [Test]
        public void Test_buildIntersectionCommand_CommandBuilder_AddProgramName_called_once_with_formed_name()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);
            string overlayLayerFile = "overlayLayerFile" + random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildIntersectionCommand(scriptFolder, scriptName, imputLayerFile, overlayLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.AddProgramName(scriptFolder + @"\" + scriptName), Times.Once);
        }

        [Test]
        public void Test_buildIntersectionCommand_CommandBuilder_SignleParameter_called_twice()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);
            string overlayLayerFile = "overlayLayerFile" + random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildIntersectionCommand(scriptFolder, scriptName, imputLayerFile, overlayLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.SignleParameter(It.IsAny<String>()), Times.Exactly(2));
        }

        [Test]
        public void Test_buildIntersectionCommand_CommandBuilder_SignleParameter_called_once_with_inputLayerFile()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);
            string overlayLayerFile = "overlayLayerFile" + random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildIntersectionCommand(scriptFolder, scriptName, imputLayerFile, overlayLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.SignleParameter(imputLayerFile), Times.Exactly(1));
        }

        [Test]
        public void Test_buildIntersectionCommand_CommandBuilder_SignleParameter_called_once_with_overlayLayerFile()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);
            string overlayLayerFile = "overlayLayerFile" + random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildIntersectionCommand(scriptFolder, scriptName, imputLayerFile, overlayLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.SignleParameter(overlayLayerFile), Times.Exactly(1));
        }

        [Test]
        public void Test_buildIntersectionCommand_CommandBuilder_GetResult_called_once()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);
            string overlayLayerFile = "overlayLayerFile" + random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildIntersectionCommand(scriptFolder, scriptName, imputLayerFile, overlayLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.GetResult(), Times.Exactly(1));
        }

        [Test]
        public void Test_buildIntersectionCommand_result_from_CommandBuilder_is_buildIntersectionCommand_result()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);
            string overlayLayerFile = "overlayLayerFile" + random.Next(1000);
            string resultValueExpected = "resultValueExpected" + random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();
            commandBuilder.Setup(x => x.GetResult()).Returns(resultValueExpected);

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            string result = commandCreator.Object.buildIntersectionCommand(scriptFolder, scriptName, imputLayerFile, overlayLayerFile);

            Assert.AreEqual(resultValueExpected, result);
        }

        [Test]
        public void Test_buildMergeCommand_CommandBuilder_AddProgramName_called_once_with_formed_name()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);

            Mock<MergeVectorLayerOperationParam> mergeVectorLayerOperationParam = new Mock<MergeVectorLayerOperationParam>();
            mergeVectorLayerOperationParam.Object.Crs = "crs" + random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildMergeCommand(mergeVectorLayerOperationParam.Object, scriptFolder, scriptName, imputLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.AddProgramName(scriptFolder + @"\" + scriptName), Times.Once);
        }

        [Test]
        public void Test_buildMergeCommand_CommandBuilder_SignleParameter_called_twice()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);

            Mock<MergeVectorLayerOperationParam> mergeVectorLayerOperationParam = new Mock<MergeVectorLayerOperationParam>();
            mergeVectorLayerOperationParam.Object.Crs = "crs" + random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildMergeCommand(mergeVectorLayerOperationParam.Object, scriptFolder, scriptName, imputLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.SignleParameter(It.IsAny<String>()), Times.Exactly(2));
        }

        [Test]
        public void Test_buildMergeCommand_CommandBuilder_SignleParameter_called_once_with_imputLayerFile()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);

            Mock<MergeVectorLayerOperationParam> mergeVectorLayerOperationParam = new Mock<MergeVectorLayerOperationParam>();
            mergeVectorLayerOperationParam.Object.Crs = "crs" + random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildMergeCommand(mergeVectorLayerOperationParam.Object, scriptFolder, scriptName, imputLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.SignleParameter(imputLayerFile), Times.Exactly(1));
        }

        [Test]
        public void Test_buildMergeCommand_CommandBuilder_SignleParameter_called_once_with_mergeVectorLayerOperationParam_crs()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);

            Mock<MergeVectorLayerOperationParam> mergeVectorLayerOperationParam = new Mock<MergeVectorLayerOperationParam>();
            mergeVectorLayerOperationParam.Object.Crs = "crs" + random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildMergeCommand(mergeVectorLayerOperationParam.Object, scriptFolder, scriptName, imputLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.SignleParameter(mergeVectorLayerOperationParam.Object.Crs), Times.Exactly(1));
        }

        [Test]
        public void Test_buildMergeCommand_CommandBuilder_GetResult_called_once()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);

            Mock<MergeVectorLayerOperationParam> mergeVectorLayerOperationParam = new Mock<MergeVectorLayerOperationParam>();
            mergeVectorLayerOperationParam.Object.Crs = "crs" + random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildMergeCommand(mergeVectorLayerOperationParam.Object, scriptFolder, scriptName, imputLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.GetResult(), Times.Exactly(1));
        }

        [Test]
        public void Test_buildMergeCommand_result_from_CommandBuilder_is_buildMergeCommand_result()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);
            string resultValueExpected = "resultValueExpected" + random.Next(1000);

            Mock<MergeVectorLayerOperationParam> mergeVectorLayerOperationParam = new Mock<MergeVectorLayerOperationParam>();
            mergeVectorLayerOperationParam.Object.Crs = "crs" + random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();
            commandBuilder.Setup(x => x.GetResult()).Returns(resultValueExpected);

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            string result = commandCreator.Object.buildMergeCommand(mergeVectorLayerOperationParam.Object, scriptFolder, scriptName, imputLayerFile);

            Assert.AreEqual(resultValueExpected, result);
        }

        [Test]
        public void Test_buildSelectByAttributeCommand_CommandBuilder_AddProgramName_called_once_with_formed_name()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);

            Mock<SelectByAttributeOperationParam> selectByAttributeOperationParam = new Mock<SelectByAttributeOperationParam>();
            selectByAttributeOperationParam.Object.Field = "field" + random.Next(1000);
            selectByAttributeOperationParam.Object.Operator = random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildSelectByAttributeCommand(selectByAttributeOperationParam.Object, scriptFolder, scriptName, imputLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.AddProgramName(scriptFolder + @"\" + scriptName), Times.Exactly(1));
        }

        [Test]
        public void Test_buildSelectByAttributeCommand_when_selectByAttributeOperationParam_value_field_null_CommandBuilder_SingleParameter_called_three_times()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);

            Mock<SelectByAttributeOperationParam> selectByAttributeOperationParam = new Mock<SelectByAttributeOperationParam>();
            selectByAttributeOperationParam.Object.Field = "field" + random.Next(1000);
            selectByAttributeOperationParam.Object.Operator = random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildSelectByAttributeCommand(selectByAttributeOperationParam.Object, scriptFolder, scriptName, imputLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.SignleParameter(It.IsAny<String>()), Times.Exactly(3));
        }

        [Test]
        public void Test_buildSelectByAttributeCommand_CommandBuilder_SingleParameter_called_four_times()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);

            Mock<SelectByAttributeOperationParam> selectByAttributeOperationParam = new Mock<SelectByAttributeOperationParam>();
            selectByAttributeOperationParam.Object.Field = "field" + random.Next(1000);
            selectByAttributeOperationParam.Object.Operator = random.Next(1000);
            selectByAttributeOperationParam.Object.Value = "value" + random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildSelectByAttributeCommand(selectByAttributeOperationParam.Object, scriptFolder, scriptName, imputLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.SignleParameter(It.IsAny<String>()), Times.Exactly(4));
        }

        [Test]
        public void Test_buildSelectByAttributeCommand_when_selectByAttributeOperationParam_value_field_null_CommandBuilder_SingleParameter_with_that_field_value_never_called()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);

            Mock<SelectByAttributeOperationParam> selectByAttributeOperationParam = new Mock<SelectByAttributeOperationParam>();
            selectByAttributeOperationParam.Object.Field = "field" + random.Next(1000);
            selectByAttributeOperationParam.Object.Operator = random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildSelectByAttributeCommand(selectByAttributeOperationParam.Object, scriptFolder, scriptName, imputLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.SignleParameter(selectByAttributeOperationParam.Object.Value), Times.Exactly(0));
        }

        [Test]
        public void Test_buildSelectByAttributeCommand_when_selectByAttributeOperationParam_value_field_defined_CommandBuilder_SingleParameter_with_that_field_value_called_once()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);

            Mock<SelectByAttributeOperationParam> selectByAttributeOperationParam = new Mock<SelectByAttributeOperationParam>();
            selectByAttributeOperationParam.Object.Field = "field" + random.Next(1000);
            selectByAttributeOperationParam.Object.Operator = random.Next(1000);
            selectByAttributeOperationParam.Object.Value = "value" + random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildSelectByAttributeCommand(selectByAttributeOperationParam.Object, scriptFolder, scriptName, imputLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.SignleParameter(selectByAttributeOperationParam.Object.Value), Times.Exactly(1));
        }

        [Test]
        public void Test_buildSelectByAttributeCommand_CommandBuilder_SingleParameter_called_once_with_imputLayerFile()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);

            Mock<SelectByAttributeOperationParam> selectByAttributeOperationParam = new Mock<SelectByAttributeOperationParam>();
            selectByAttributeOperationParam.Object.Field = "field" + random.Next(1000);
            selectByAttributeOperationParam.Object.Operator = random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildSelectByAttributeCommand(selectByAttributeOperationParam.Object, scriptFolder, scriptName, imputLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.SignleParameter(imputLayerFile), Times.Exactly(1));
        }

        [Test]
        public void Test_buildSelectByAttributeCommand_CommandBuilder_SingleParameter_called_once_with_selectByAttributeOperationParam_field()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);

            Mock<SelectByAttributeOperationParam> selectByAttributeOperationParam = new Mock<SelectByAttributeOperationParam>();
            selectByAttributeOperationParam.Object.Field = "field" + random.Next(1000);
            selectByAttributeOperationParam.Object.Operator = random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildSelectByAttributeCommand(selectByAttributeOperationParam.Object, scriptFolder, scriptName, imputLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.SignleParameter(selectByAttributeOperationParam.Object.Field), Times.Exactly(1));
        }

        [Test]
        public void Test_buildSelectByAttributeCommand_CommandBuilder_SingleParameter_called_once_with_selectByAttributeOperationParam_operator()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);

            Mock<SelectByAttributeOperationParam> selectByAttributeOperationParam = new Mock<SelectByAttributeOperationParam>();
            selectByAttributeOperationParam.Object.Field = "field" + random.Next(1000);
            selectByAttributeOperationParam.Object.Operator = random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildSelectByAttributeCommand(selectByAttributeOperationParam.Object, scriptFolder, scriptName, imputLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.SignleParameter(selectByAttributeOperationParam.Object.Operator.ToString()), Times.Exactly(1));
        }

        [Test]
        public void Test_buildSelectByAttributeCommand_CommandBuilder_GetResult_called_once()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);

            Mock<SelectByAttributeOperationParam> selectByAttributeOperationParam = new Mock<SelectByAttributeOperationParam>();
            selectByAttributeOperationParam.Object.Field = "field" + random.Next(1000);
            selectByAttributeOperationParam.Object.Operator = random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildSelectByAttributeCommand(selectByAttributeOperationParam.Object, scriptFolder, scriptName, imputLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.GetResult(), Times.Exactly(1));
        }

        [Test]
        public void Test_buildSelectByAttributeCommand_result_from_CommandBuilder_is_buildSelectByAttributeCommand_result()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);
            string resultValueExpected = "resultValueExpected" + random.Next(1000);

            Mock<SelectByAttributeOperationParam> selectByAttributeOperationParam = new Mock<SelectByAttributeOperationParam>();
            selectByAttributeOperationParam.Object.Field = "field" + random.Next(1000);
            selectByAttributeOperationParam.Object.Operator = random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();
            commandBuilder.Setup(x => x.GetResult()).Returns(resultValueExpected);

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            string result = commandCreator.Object.buildSelectByAttributeCommand(selectByAttributeOperationParam.Object, scriptFolder, scriptName, imputLayerFile);

            Assert.AreEqual(resultValueExpected, result);
        }

        [Test]
        public void Test_buildSelectByLocationCommand_CommandBuilder_AddProgramName_called_once_with_formed_name()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);
            string overlayLayerFile = "overlayLayerFile" + random.Next(1000);

            Mock<SelectByLocationOperationParam> selectByLocationOperationParam = new Mock<SelectByLocationOperationParam>();
            selectByLocationOperationParam.Object.predicate = new List<int> { random.Next(10), random.Next(10), random.Next(10) };

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildSelectByLocationCommand(selectByLocationOperationParam.Object, scriptFolder, scriptName, imputLayerFile, overlayLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.AddProgramName(scriptFolder + @"\" + scriptName), Times.Exactly(1));
        }

        [Test]
        public void Test_buildSelectByLocationCommand_when_distance_null_then_CommandBuilder_SignleParameter_called_three_times()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);
            string overlayLayerFile = "overlayLayerFile" + random.Next(1000);

            Mock<SelectByLocationOperationParam> selectByLocationOperationParam = new Mock<SelectByLocationOperationParam>();
            selectByLocationOperationParam.Object.predicate = new List<int> { random.Next(10), random.Next(10), random.Next(10) };

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildSelectByLocationCommand(selectByLocationOperationParam.Object, scriptFolder, scriptName, imputLayerFile, overlayLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.SignleParameter(It.IsAny<String>()), Times.Exactly(3));
        }

        [Test]
        public void Test_buildSelectByLocationCommand_when_distance_is_defined_then_CommandBuilder_SignleParameter_called_four_times()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);
            string overlayLayerFile = "overlayLayerFile" + random.Next(1000);

            Mock<SelectByLocationOperationParam> selectByLocationOperationParam = new Mock<SelectByLocationOperationParam>();
            selectByLocationOperationParam.Object.predicate = new List<int> { random.Next(10), random.Next(10), random.Next(10) };
            selectByLocationOperationParam.Object.Distance = random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildSelectByLocationCommand(selectByLocationOperationParam.Object, scriptFolder, scriptName, imputLayerFile, overlayLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.SignleParameter(It.IsAny<String>()), Times.Exactly(4));
        }

        [Test]
        public void Test_buildSelectByLocationCommand_when_distance_is_defined_then_CommandBuilder_SignleParameter_called_once_with_distance()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);
            string overlayLayerFile = "overlayLayerFile" + random.Next(1000);

            Mock<SelectByLocationOperationParam> selectByLocationOperationParam = new Mock<SelectByLocationOperationParam>();
            selectByLocationOperationParam.Object.predicate = new List<int> { random.Next(10), random.Next(10), random.Next(10) };
            selectByLocationOperationParam.Object.Distance = random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildSelectByLocationCommand(selectByLocationOperationParam.Object, scriptFolder, scriptName, imputLayerFile, overlayLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.SignleParameter(selectByLocationOperationParam.Object.Distance.ToString()), Times.Exactly(1));
        }

        [Test]
        public void Test_buildSelectByLocationCommand_CommandBuilder_SignleParameter_called_once_with_inputLayerFile()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);
            string overlayLayerFile = "overlayLayerFile" + random.Next(1000);

            Mock<SelectByLocationOperationParam> selectByLocationOperationParam = new Mock<SelectByLocationOperationParam>();
            selectByLocationOperationParam.Object.predicate = new List<int> { random.Next(10), random.Next(10), random.Next(10) };

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildSelectByLocationCommand(selectByLocationOperationParam.Object, scriptFolder, scriptName, imputLayerFile, overlayLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.SignleParameter(imputLayerFile), Times.Exactly(1));
        }

        [Test]
        public void Test_buildSelectByLocationCommand_CommandBuilder_SignleParameter_called_once_with_overlayLayerFile()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);
            string overlayLayerFile = "overlayLayerFile" + random.Next(1000);

            Mock<SelectByLocationOperationParam> selectByLocationOperationParam = new Mock<SelectByLocationOperationParam>();
            selectByLocationOperationParam.Object.predicate = new List<int> { random.Next(10), random.Next(10), random.Next(10) };

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildSelectByLocationCommand(selectByLocationOperationParam.Object, scriptFolder, scriptName, imputLayerFile, overlayLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.SignleParameter(overlayLayerFile), Times.Exactly(1));
        }

        [Test]
        public void Test_buildSelectByLocationCommand_CommandBuilder_SignleParameter_called_once_with_predicate()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);
            string overlayLayerFile = "overlayLayerFile" + random.Next(1000);

            Mock<SelectByLocationOperationParam> selectByLocationOperationParam = new Mock<SelectByLocationOperationParam>();
            selectByLocationOperationParam.Object.predicate = new List<int> { random.Next(10), random.Next(10), random.Next(10) };

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildSelectByLocationCommand(selectByLocationOperationParam.Object, scriptFolder, scriptName, imputLayerFile, overlayLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.SignleParameter(String.Join(",", selectByLocationOperationParam.Object.predicate.ToArray())), Times.Exactly(1));
        }

        [Test]
        public void Test_buildSelectByLocationCommand_CommandBuilder_GetResult_called_once()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);
            string overlayLayerFile = "overlayLayerFile" + random.Next(1000);

            Mock<SelectByLocationOperationParam> selectByLocationOperationParam = new Mock<SelectByLocationOperationParam>();
            selectByLocationOperationParam.Object.predicate = new List<int> { random.Next(10), random.Next(10), random.Next(10) };

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildSelectByLocationCommand(selectByLocationOperationParam.Object, scriptFolder, scriptName, imputLayerFile, overlayLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.GetResult(), Times.Exactly(1));
        }

        [Test]
        public void Test_buildSelectByLocationCommand_result_from_CommandBuilder_GetResult_is_result_from_buildSelectByLocationCommand()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);
            string overlayLayerFile = "overlayLayerFile" + random.Next(1000);
            string expectedResult = "expectedResult" + random.Next(1000);

            Mock<SelectByLocationOperationParam> selectByLocationOperationParam = new Mock<SelectByLocationOperationParam>();
            selectByLocationOperationParam.Object.predicate = new List<int> { random.Next(10), random.Next(10), random.Next(10) };

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();
            commandBuilder.Setup(x => x.GetResult()).Returns(expectedResult);

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            string result = commandCreator.Object.buildSelectByLocationCommand(selectByLocationOperationParam.Object, scriptFolder, scriptName, imputLayerFile, overlayLayerFile);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void Test_buildClipCommand_CommandBuilder_AddProgramName_called_once_with_formed_name()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);
            string overlayLayerFile = "overlayLayerFile" + random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildClipCommand(scriptFolder, scriptName, imputLayerFile, overlayLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.AddProgramName(scriptFolder + @"\" + scriptName), Times.Exactly(1));
        }

        [Test]
        public void Test_buildClipCommand_CommandBuilder_SignleParameter_called_twice()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);
            string overlayLayerFile = "overlayLayerFile" + random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildClipCommand(scriptFolder, scriptName, imputLayerFile, overlayLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.SignleParameter(It.IsAny<String>()), Times.Exactly(2));
        }

        [Test]
        public void Test_buildClipCommand_CommandBuilder_SignleParameter_called_once_with_imputLayerFile()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);
            string overlayLayerFile = "overlayLayerFile" + random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildClipCommand(scriptFolder, scriptName, imputLayerFile, overlayLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.SignleParameter(imputLayerFile), Times.Exactly(1));
        }

        [Test]
        public void Test_buildClipCommand_CommandBuilder_SignleParameter_called_once_with_overlayLayerFile()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);
            string overlayLayerFile = "overlayLayerFile" + random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildClipCommand(scriptFolder, scriptName, imputLayerFile, overlayLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.SignleParameter(overlayLayerFile), Times.Exactly(1));
        }

        [Test]
        public void Test_buildClipCommand_CommandBuilder_GetResult_called_once()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);
            string overlayLayerFile = "overlayLayerFile" + random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildClipCommand(scriptFolder, scriptName, imputLayerFile, overlayLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.GetResult(), Times.Exactly(1));
        }

        [Test]
        public void Test_buildClipCommand_result_from_CommandBuilder_GetResult_is_result_from_buildClipCommand()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);
            string overlayLayerFile = "overlayLayerFile" + random.Next(1000);
            string expectedResult = "expectedResult" + random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();
            commandBuilder.Setup(x => x.GetResult()).Returns(expectedResult);

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            string result = commandCreator.Object.buildClipCommand(scriptFolder, scriptName, imputLayerFile, overlayLayerFile);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void Test_buildLandFundAnalysisCommand_CommandBuilder_AddProgramName_called_once_with_formed_name()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);

            Mock<LandFundAnalysisOperationParam> landFundAnalysisOperationParam = new Mock<LandFundAnalysisOperationParam>();
            landFundAnalysisOperationParam .Object.Predicate= new List<int> { random.Next(10), random.Next(10), random.Next(10) };

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildLandFundAnalysisCommand(landFundAnalysisOperationParam.Object, scriptFolder, scriptName, imputLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.AddProgramName(scriptFolder + @"\" + scriptName), Times.Exactly(1));
        }

        [Test]
        public void Test_buildLandFundAnalysisCommand_when_distance_null_then_CommandBuilder_SignleParameter_called_two_times()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);

            Mock<LandFundAnalysisOperationParam> landFundAnalysisOperationParam = new Mock<LandFundAnalysisOperationParam>();
            landFundAnalysisOperationParam.Object.Predicate = new List<int> { random.Next(10), random.Next(10), random.Next(10) };

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildLandFundAnalysisCommand(landFundAnalysisOperationParam.Object, scriptFolder, scriptName, imputLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.SignleParameter(It.IsAny<String>()), Times.Exactly(2));
        }

        [Test]
        public void Test_buildLandFundAnalysisCommand_when_distance_defined_then_CommandBuilder_SignleParameter_called_three_times()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);

            Mock<LandFundAnalysisOperationParam> landFundAnalysisOperationParam = new Mock<LandFundAnalysisOperationParam>();
            landFundAnalysisOperationParam.Object.Predicate = new List<int> { random.Next(10), random.Next(10), random.Next(10) };
            landFundAnalysisOperationParam.Object.Distance = random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildLandFundAnalysisCommand(landFundAnalysisOperationParam.Object, scriptFolder, scriptName, imputLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.SignleParameter(It.IsAny<String>()), Times.Exactly(3));
        }

        [Test]
        public void Test_buildLandFundAnalysisCommand_when_distance_defined_then_CommandBuilder_SignleParameter_called_once_with_distance()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);

            Mock<LandFundAnalysisOperationParam> landFundAnalysisOperationParam = new Mock<LandFundAnalysisOperationParam>();
            landFundAnalysisOperationParam.Object.Predicate = new List<int> { random.Next(10), random.Next(10), random.Next(10) };
            landFundAnalysisOperationParam.Object.Distance = random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildLandFundAnalysisCommand(landFundAnalysisOperationParam.Object, scriptFolder, scriptName, imputLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.SignleParameter(landFundAnalysisOperationParam.Object.Distance.ToString()), Times.Exactly(1));
        }

        [Test]
        public void Test_buildLandFundAnalysisCommand_CommandBuilder_SignleParameter_called_once_with_imputLayerFile()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);

            Mock<LandFundAnalysisOperationParam> landFundAnalysisOperationParam = new Mock<LandFundAnalysisOperationParam>();
            landFundAnalysisOperationParam.Object.Predicate = new List<int> { random.Next(10), random.Next(10), random.Next(10) };
            landFundAnalysisOperationParam.Object.Distance = random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildLandFundAnalysisCommand(landFundAnalysisOperationParam.Object, scriptFolder, scriptName, imputLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.SignleParameter(imputLayerFile), Times.Exactly(1));
        }

        [Test]
        public void Test_buildLandFundAnalysisCommand_CommandBuilder_SignleParameter_called_once_with_predicates()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);

            Mock<LandFundAnalysisOperationParam> landFundAnalysisOperationParam = new Mock<LandFundAnalysisOperationParam>();
            landFundAnalysisOperationParam.Object.Predicate = new List<int> { random.Next(10), random.Next(10), random.Next(10) };
            landFundAnalysisOperationParam.Object.Distance = random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildLandFundAnalysisCommand(landFundAnalysisOperationParam.Object, scriptFolder, scriptName, imputLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.SignleParameter(String.Join(",", landFundAnalysisOperationParam.Object.Predicate.ToArray())), Times.Exactly(1));
        }

        [Test]
        public void Test_buildLandFundAnalysisCommand_CommandBuilder_GetResult_called_once()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);

            Mock<LandFundAnalysisOperationParam> landFundAnalysisOperationParam = new Mock<LandFundAnalysisOperationParam>();
            landFundAnalysisOperationParam.Object.Predicate = new List<int> { random.Next(10), random.Next(10), random.Next(10) };
            landFundAnalysisOperationParam.Object.Distance = random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            commandCreator.Object.buildLandFundAnalysisCommand(landFundAnalysisOperationParam.Object, scriptFolder, scriptName, imputLayerFile);

            Mock.Get(commandBuilder.Object).Verify(x => x.GetResult(), Times.Exactly(1));
        }

        [Test]
        public void Test_buildLandFundAnalysisCommand_result_from_CommandBuilder_GetResult_is_result_from_buildLandFundAnalysisCommand()
        {
            Random random = new Random();
            string scriptFolder = "scriptFolder" + random.Next(1000);
            string scriptName = "scriptName" + random.Next(1000);
            string imputLayerFile = "imputLayerFile" + random.Next(1000);
            string expectedResult = "expectedResult" + random.Next(1000);

            Mock<LandFundAnalysisOperationParam> landFundAnalysisOperationParam = new Mock<LandFundAnalysisOperationParam>();
            landFundAnalysisOperationParam.Object.Predicate = new List<int> { random.Next(10), random.Next(10), random.Next(10) };
            landFundAnalysisOperationParam.Object.Distance = random.Next(1000);

            Mock<ICommandBuilder> commandBuilder = new Mock<ICommandBuilder>();
            commandBuilder.Setup(x => x.GetResult()).Returns(expectedResult);

            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.Setup(x => x.createNewCommandBuilder()).Returns(commandBuilder.Object);
            commandCreator.CallBase = true;

            string result = commandCreator.Object.buildLandFundAnalysisCommand(landFundAnalysisOperationParam.Object, scriptFolder, scriptName, imputLayerFile);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void Test_createNewCommandBuilder_returns_empty_CommandBuilder()
        {
            Mock<CommandCreator> commandCreator = new Mock<CommandCreator>();
            commandCreator.CallBase = true;
            ICommandBuilder builder = commandCreator.Object.createNewCommandBuilder();

            Assert.IsEmpty(builder.GetResult());
        }


    }
}
