using System.IO;
using Microsoft.AspNetCore.Mvc;
using System.CodeDom.Compiler;
using WebApplication.Model;
using WebApplication.CommandBuilder;
using WebApplication.RunThirdParty;
using WebApplication.HandleThirdPartyOutput;
using Microsoft.Extensions.Configuration;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransformController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private ICommandCreator _creator;
        private IProcessPython _python;
        private IHandleOutput _outputHandler;
        public TransformController(IConfiguration configuration, ICommandCreator creator, IProcessPython python, IHandleOutput outputHandler)
        {
            Configuration = configuration;
            _creator = creator;
            _python = python;
            _outputHandler = outputHandler;
        }

        // POST: api/Transform
        [HttpPost]
        public IActionResult Posting(TransformOperationParam json)
        {
            var OSGeo4WShell = Configuration["ScriptRunners:OSGeo4WPath"];
            // Creating temp directory to store ogr2ogr output
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            string tempDirectoryFile = Path.Combine(tempDirectory, Path.ChangeExtension(Path.GetRandomFileName(), json.SingleFileFormat));
            ResultObject output;

            // file disposes after block
            using (TempFileCollection files = new TempFileCollection())
            {
                string file = files.AddExtension("geojson");
                System.IO.File.WriteAllText(file, json.InputLayer.ToString());
                string cmdInput = _creator.buildOgr2Ogr(json, "ogr2ogr", tempDirectoryFile, file);
                output = _python.RunCMD(cmdInput, OSGeo4WShell);
            }
            var result = _outputHandler.HandleGdalOutput(output, tempDirectory); 
            // Deleting source
            DirectoryInfo directory = new DirectoryInfo(tempDirectory);
            directory.Delete(true);

            return result;
        }
    }
}
