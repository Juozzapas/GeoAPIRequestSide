using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Model;

namespace WebApplication.CommandBuilder
{
    public interface ICommandCreator
    {
        public string buildOgr2Ogr(TransformOperationParam json, string program, string dst_data, string src_data);
        public string buildBufferCommand(BufferOperationParam json, string scriptFolder, string scriptName, string inputLayerFile);
        public string buildLandFundAnalysisCommand(LandFundAnalysisOperationParam json, string scriptFolder, string scriptName, string inputLayerFile);
        public string buildIntersectionCommand(string scriptFolder, string scriptName, string inputLayerFile, string overlayLayerFile);
        public string buildClipCommand(string scriptFolder, string scriptName, string inputLayerFile, string overlayLayerFile);
        public string buildMergeCommand(MergeVectorLayerOperationParam json, string scriptFolder, string scriptName, string inputLayerFile);
        public string buildSelectByAttributeCommand(SelectByAttributeOperationParam json, string scriptFolder, string scriptName, string inputLayerFile);
        public string buildSelectByLocationCommand(SelectByLocationOperationParam json, string scriptFolder, string scriptName, string inputLayerFile, string overlayLayerFile);
    }
}
