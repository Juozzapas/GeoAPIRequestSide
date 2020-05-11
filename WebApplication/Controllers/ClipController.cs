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
    public class ClipController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private ICommandCreator _creator;
        private IProcessPython _python;
        private IHandleOutput _outputHandler;
        public ClipController(IConfiguration configuration, ICommandCreator creator, IProcessPython python, IHandleOutput outputHandler)
        {
            Configuration = configuration;
            _creator = creator;
            _python = python;
            _outputHandler = outputHandler;
        }

        // POST: api/Default
        [HttpPost]
        [Consumes("application/json")] // supports only json
        public IActionResult Post(ClipOperationParam myJson)
        {
            var pathToScriptFolder = Configuration["pathToAnalysisScriptFolder"];
            var scriptName = Configuration["AnalysisScriptNames:clipScript"];
            var qgisPython = Configuration["ScriptRunners:qgisCmdPath"];
            ResultObject output;

            using (var tempCollection = new TempFileCollection())
            {
                string inputLayerFile = tempCollection.AddExtension("geojson");

                string overlayLayerFile = Path.ChangeExtension(Path.GetTempFileName(), "geojson");
                tempCollection.AddFile(overlayLayerFile, false);
                System.IO.File.WriteAllText(inputLayerFile, myJson.InputLayer.ToString());
                System.IO.File.WriteAllText(overlayLayerFile, myJson.OverlayLayer.ToString());

                string args = _creator.buildClipCommand(pathToScriptFolder, scriptName, inputLayerFile, overlayLayerFile);

                output = _python.RunCMD(args, qgisPython);

            }
            return _outputHandler.HandleQgisOutput(output);
        }
    }
}