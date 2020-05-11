using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Model;

namespace WebApplication.HandleThirdPartyOutput.HandleLogic
{
    public class LibraryOutputHandler
    {
        private string getGdalKnownErrors(ResultObject result)
        {

            if (result.stderr.Contains("Failed to process SRS definition"))
            {
                return "Failed to process CRS definition";
            }
            if (result.stderr.Contains("Failed to read GeoJSON data"))
            {
                return "Failed to read GeoJSON data";
            }
            if (result.stderr.Contains("Failed to reproject feature"))
            {
                return "Failed to reproject. Geometry probably out of source or destination SRS";
            }
            return "Contact server administrator";
        }
        public IActionResult HandleResponse(ResultObject result, string directory)
        {
            // if stdour and stderr empty = success
            if (result.isEmpty() || !result.stderr.Contains("ERROR"))
            {
                string zipResult = Path.ChangeExtension(Path.GetTempFileName(), "zip");
                ZipFile.CreateFromDirectory(directory, zipResult);

                var stream = new FileStream(zipResult, FileMode.Open, FileAccess.Read, FileShare.None, 4096, FileOptions.DeleteOnClose);
                return new FileStreamResult(stream, "application/zip");
            }
            else
            {
                string knownErrors = getGdalKnownErrors(result);
                return new BadRequestObjectResult(new { errors = knownErrors });
            }
        }
    }
}
