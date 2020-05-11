using Microsoft.AspNetCore.Mvc;
using System.CodeDom.Compiler;
using WebApplication.Model;
using WebApplication.CommandBuilder;
using WebApplication.RunThirdParty;
using WebApplication.HandleThirdPartyOutput;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("web-api-nunit-test")]
namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BufferController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private ICommandCreator _creator;
        private IProcessPython _python;
        private IHandleOutput _outputHandler;
        public BufferController(IConfiguration configuration, ICommandCreator creator, IProcessPython python, IHandleOutput outputHandler)
        {
            Configuration = configuration;
            _creator = creator;
            _python = python;
            _outputHandler = outputHandler;
        }

        // POST: api/Default
        [HttpPost]
        [Consumes("application/json")] // supports only json
        public IActionResult Post(BufferOperationParam myJson)
        {
            var pathToScriptFolder = Configuration["pathToAnalysisScriptFolder"];
            var scriptName= Configuration["AnalysisScriptNames:bufferScript"];
            var qgisPython = Configuration["ScriptRunners:qgisCmdPath"];
            ResultObject output;
            using (var tempFiles = new TempFileCollection())
            {
                string inputLayerFile = tempFiles.AddExtension("geojson");
                System.IO.File.WriteAllText(inputLayerFile, myJson.InputLayer.ToString());
                string args = _creator.buildBufferCommand(myJson, pathToScriptFolder, scriptName, inputLayerFile);
                output = _python.RunCMD(args, qgisPython);
                
            }
            return _outputHandler.HandleQgisOutput(output);
        }
    }
}
