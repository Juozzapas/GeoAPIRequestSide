using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.HandleThirdPartyOutput.HandleLogic;
using WebApplication.Model;

namespace WebApplication.HandleThirdPartyOutput
{
    public class HandleOutput: IHandleOutput
    {
        private LibraryOutputHandler gdalHadnler;
        private ScriptOutputHandler qgisHandler;
        public HandleOutput()
        {
            //qgisHandler = new OnQgisSuccess();
            //AbstractChain h2 = new OnQgisFailure();

            //qgisHandler.SetSuccessor(h2);

            // gdalHadnler = new OnGdalSuccess();
            //AbstractChain hd2 = new OnGdalFailure();

            //gdalHadnler.SetSuccessor(hd2);
            gdalHadnler = new LibraryOutputHandler();
            qgisHandler = new ScriptOutputHandler();
        }
        public override IActionResult HandleQgisOutput(ResultObject output)
        {
            return qgisHandler.HandleResponse(output);
        }
        public override IActionResult HandleGdalOutput(ResultObject output, string directory)
        {
            return gdalHadnler.HandleResponse(output, directory);
        }
    }
}
