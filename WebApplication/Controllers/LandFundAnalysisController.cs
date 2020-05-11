using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApplication.CommandBuilder;
using WebApplication.HandleThirdPartyOutput;
using WebApplication.Model;
using WebApplication.RunThirdParty;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LandFundAnalysisController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private ICommandCreator _creator;
        private IProcessPython _python;
        private IHandleOutput _outputHandler;
        public LandFundAnalysisController(IConfiguration configuration, ICommandCreator creator, IProcessPython python, IHandleOutput outputHandler)
        {
            Configuration = configuration;
            _creator = creator;
            _python = python;
            _outputHandler = outputHandler;
        }


        // POST: api/Default
        [HttpPost]
        [Consumes("application/json")] // supports only json
        public IActionResult Post(LandFundAnalysisOperationParam myJson)
        {
            var pathToScriptFolder = Configuration["pathToLandFundScriptFolder"];
            var scriptName = Configuration["LandFundScriptNames:analysisScript"];
            var qgisPython = Configuration["ScriptRunners:qgisCmdPath"];
            ResultObject output;
            using (var tempFiles = new TempFileCollection())
            {
                string inputLayerFile = tempFiles.AddExtension("geojson");
                System.IO.File.WriteAllText(inputLayerFile, myJson.InputLayer.ToString());
                string args = _creator.buildLandFundAnalysisCommand(myJson, pathToScriptFolder, scriptName, inputLayerFile);
                output = _python.RunCMD(args, qgisPython);
            }
            return _outputHandler.HandleQgisOutput(output);
        }
    }
}