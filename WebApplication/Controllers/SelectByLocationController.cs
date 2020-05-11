using System.CodeDom.Compiler;
using System.IO;
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
    public class SelectByLocationController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private ICommandCreator _creator;
        private IProcessPython _python;
        private IHandleOutput _outputHandler;
        public SelectByLocationController(IConfiguration configuration, ICommandCreator creator, IProcessPython python, IHandleOutput outputHandler)
        {
            Configuration = configuration;
            _creator = creator;
            _python = python;
            _outputHandler = outputHandler;
        }
        // POST: api/Default
        [HttpPost]
        [Consumes("application/json")] // supports only json
        public IActionResult Post(SelectByLocationOperationParam myJson)
        {
            var pathToScriptFolder = Configuration["pathToAnalysisScriptFolder"];
            var scriptName = Configuration["AnalysisScriptNames:selectByLocationScript"];
            var qgisPython = Configuration["ScriptRunners:qgisCmdPath"];
            ResultObject output;
            using (var tempFiles1 = new TempFileCollection())
            {
                string inputLayerFile = tempFiles1.AddExtension("geojson");
                string overlayLayerFile = Path.ChangeExtension(Path.GetTempFileName(), "geojson");
                tempFiles1.AddFile(overlayLayerFile, false);
                System.IO.File.WriteAllText(inputLayerFile, myJson.InputLayer.ToString());
                System.IO.File.WriteAllText(overlayLayerFile, myJson.OverlayLayer.ToString());
                string args = _creator.buildSelectByLocationCommand(myJson, pathToScriptFolder, scriptName, inputLayerFile, overlayLayerFile);
                output = _python.RunCMD(args, qgisPython);


            }
            return _outputHandler.HandleQgisOutput(output);
        }
    }
}