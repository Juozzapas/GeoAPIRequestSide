using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApplication.Model;

namespace WebApplication.HandleThirdPartyOutput.HandleLogic
{
    public class ScriptOutputHandler
    {
        public virtual string getQgisResultGeojson(ResultObject result, string message)
        {
            string[] allLines = result.stdout.Split('\n');
            string lastLine = allLines[^2]; // lenght -2
            return lastLine.Replace(message, "");
        }
        public IActionResult HandleResponse(ResultObject result)
        {
            if (result.stdout.Contains("RESULT_GEOJSON"))
            {

                string _content = getQgisResultGeojson(result, "RESULT_GEOJSON");
                return new ContentResult { Content = _content, ContentType = "application/json", StatusCode = (int)HttpStatusCode.OK };
            }

            if (result.stdout.Contains("SCRIPT_ERROR"))
            {
                if (result.stdout.Contains("prosessing algorithm error"))
                {
                    return new BadRequestObjectResult(new { errors = "Operation unavailable" });
                }
                return new BadRequestObjectResult(new { errors = result.stdout });
            }

            return new BadRequestObjectResult(new { errors = "Contact server administrator" });


        }
    }
}
