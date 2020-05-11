using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.CommandBuilder;

namespace web_api_nunit_test.CommandBuilders
{
    class CommandBuilderTests
    {
        ICommandBuilder builder;
        StringBuilder stringBuilder;
        [SetUp]
        public void Setup()
        {
            stringBuilder = new StringBuilder();
            builder = new CommandBuilder(stringBuilder);
        }

        [Test]
        public void Test_AddProgrameName_adds_program_name_to_StringBuilder_in_correct_format()
        {
            Random random = new Random();
            string param = "Name" + random.Next(1000);
            builder.AddProgramName(param);
            Assert.AreEqual(stringBuilder.ToString(), param);
        }

        [Test]
        public void Test_AddDataSource_adds_data_sources_to_StringBuilder_in_correct_format()
        {
            Random random = new Random();
            string dst_datasource = "File" + random.Next(1000);
            string src_datasource = "File" + random.Next(1000);
            builder.AddDataSource(dst_datasource, src_datasource);
            Assert.AreEqual(stringBuilder.ToString(), String.Format(" {0} {1}", dst_datasource, src_datasource));
        }

        [Test]
        public void Test_SingleParameter_adds_single_parameter_to_StringBuilder_in_correct_format()
        {
            Random random = new Random();
            string signle_param = "param" + random.Next(1000);
            builder.SignleParameter(signle_param);
            Assert.AreEqual(stringBuilder.ToString(), String.Format(" {0}", signle_param));
        }

        [Test]
        public void Test_GetResult_returns_string_from_StringBuilder()
        {
            Random random = new Random();
            stringBuilder.Append("random");
            stringBuilder.Append(random.Next(1000));
            Assert.AreEqual(stringBuilder.ToString(),builder.GetResult());
        }

        [Test]
        public void Test_AdditionalArguments_adds_arguments_to_StringBuilder_in_correct_format()
        {
            Random random = new Random();
            string param = "-v" + random.Next(1000);
            string argument = "File" + random.Next(1000);
            builder.AdditionalArguments(param, argument);
            Assert.AreEqual(stringBuilder.ToString(), String.Format(" {0} \"{1}\"", param, argument));
        }

    }
}
