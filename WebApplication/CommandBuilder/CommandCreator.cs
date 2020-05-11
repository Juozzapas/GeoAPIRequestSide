using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Model;

[assembly: InternalsVisibleTo("web-api-nunit-test")]
namespace WebApplication.CommandBuilder
{
    public class CommandCreator: ICommandCreator
    {
        public string buildOgr2Ogr(TransformOperationParam json, string program, string dst_data,string src_data)
        {
            ICommandBuilder builder = createNewCommandBuilder();
            builder.AddProgramName(program);
            builder.AdditionalArguments("-f", json.Type);
            if (json.SourceCrs != null && json.TargetCrs != null)
            {
                builder.AdditionalArguments("-s_srs", json.SourceCrs);
                builder.AdditionalArguments("-t_srs", json.TargetCrs);
            }
            else if(json.TargetCrs != null)
            {
                builder.AdditionalArguments("-t_srs", json.TargetCrs);
            }
            if (json.SkipFailures != null)
            {
                builder.SignleParameter(json.SkipFailures);
            }
            builder.AddDataSource(dst_data, src_data);
            
            return builder.GetResult();

        }
        public string buildBufferCommand(BufferOperationParam json, string scriptFolder, string scriptName, string inputLayerFile)
        {
            ICommandBuilder builder = createNewCommandBuilder();
            builder.AddProgramName(scriptFolder + @"\" + scriptName);
            builder.SignleParameter(inputLayerFile);
            builder.SignleParameter(json.Distance.ToString());
            return builder.GetResult();
        }
        public string buildIntersectionCommand(string scriptFolder,string scriptName, string inputLayerFile, string overlayLayerFile)
        {
            ICommandBuilder builder = createNewCommandBuilder();
            builder.AddProgramName(scriptFolder + @"\" + scriptName);
            builder.SignleParameter(inputLayerFile);    
            builder.SignleParameter(overlayLayerFile);
            return builder.GetResult();
        }
        public string buildMergeCommand(MergeVectorLayerOperationParam json, string scriptFolder, string scriptName, string inputLayerFile)
        {
            ICommandBuilder builder = createNewCommandBuilder();
            builder.AddProgramName(scriptFolder + @"\" + scriptName);
            builder.SignleParameter(inputLayerFile);
            builder.SignleParameter(json.Crs);
            return builder.GetResult();
        }
        public string buildSelectByAttributeCommand(SelectByAttributeOperationParam json, string scriptFolder, string scriptName, string inputLayerFile)
        {
            ICommandBuilder builder = createNewCommandBuilder();
            builder.AddProgramName(scriptFolder + @"\" + scriptName);
            builder.SignleParameter(inputLayerFile);
            builder.SignleParameter(json.Field);
            builder.SignleParameter(json.Operator.ToString());
            if(!String.IsNullOrEmpty(json.Value))
                builder.SignleParameter(json.Value);
            return builder.GetResult();
        }
        public string buildSelectByLocationCommand(SelectByLocationOperationParam json, string scriptFolder, string scriptName, string inputLayerFile, string overlayLayerFile)
        {
            ICommandBuilder builder = createNewCommandBuilder();
            builder.AddProgramName(scriptFolder + @"\" + scriptName);
            builder.SignleParameter(inputLayerFile);
            builder.SignleParameter(overlayLayerFile);
            string predicate = String.Join(",", json.predicate.ToArray());
            builder.SignleParameter(predicate);
            if(json.Distance!=null)
                builder.SignleParameter(json.Distance.ToString());
            return builder.GetResult();
        }

        public string buildClipCommand(string scriptFolder, string scriptName, string inputLayerFile, string overlayLayerFile)
        {
            ICommandBuilder builder = createNewCommandBuilder();
            builder.AddProgramName(scriptFolder + @"\" + scriptName);
            builder.SignleParameter(inputLayerFile);
            builder.SignleParameter(overlayLayerFile);

            return builder.GetResult();
        }

        public string buildLandFundAnalysisCommand(LandFundAnalysisOperationParam json, string scriptFolder, string scriptName, string inputLayerFile)
        {
            ICommandBuilder builder = createNewCommandBuilder();
            builder.AddProgramName(scriptFolder + @"\" + scriptName);
            builder.SignleParameter(inputLayerFile);
            string predicate = String.Join(",", json.Predicate.ToArray());
            builder.SignleParameter(predicate);
            if (json.Distance != null)
                builder.SignleParameter(json.Distance.ToString());
            return builder.GetResult();
        }

        protected virtual internal ICommandBuilder createNewCommandBuilder()
        {
            return new CommandBuilder(new StringBuilder());
        }
    }
}
