using System.CodeDom.Compiler;
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
    public class SelectByAttributeController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private ICommandCreator _creator;
        private IProcessPython _python;
        private IHandleOutput _outputHandler;
        public SelectByAttributeController(IConfiguration configuration, ICommandCreator creator, IProcessPython python, IHandleOutput outputHandler)
        {
            Configuration = configuration;
            _creator = creator;
            _python = python;
            _outputHandler = outputHandler;
        }

        // POST: api/Default
        [HttpPost]
        [Consumes("application/json")] // supports only json
        public IActionResult Post(SelectByAttributeOperationParam myJson)
        {
            var pathToScriptFolder = Configuration["pathToAnalysisScriptFolder"];
            var scriptName = Configuration["AnalysisScriptNames:selectByAttribute"];
            var qgisPython = Configuration["ScriptRunners:qgisCmdPath"];
            ResultObject output;
            using (var tempFiles = new TempFileCollection())
            {
                string inputLayerFile = tempFiles.AddExtension("geojson");
                System.IO.File.WriteAllText(inputLayerFile, myJson.InputLayer.ToString());  
                string args = _creator.buildSelectByAttributeCommand(myJson, pathToScriptFolder, scriptName, inputLayerFile);
                output = _python.RunCMD(args, qgisPython);

            }
            return _outputHandler.HandleQgisOutput(output);
        }
    }
}