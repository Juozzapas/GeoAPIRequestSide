using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Model;

namespace WebApplication.HandleThirdPartyOutput
{
    public abstract class IHandleOutput
    {
        public abstract IActionResult HandleQgisOutput(ResultObject obj);
        public abstract IActionResult HandleGdalOutput(ResultObject obj, string directory);
    }
}
