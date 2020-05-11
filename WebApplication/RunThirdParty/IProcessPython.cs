using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Model;

namespace WebApplication.RunThirdParty
{
    public interface IProcessPython
    {
        ResultObject RunCMD(string args, string filename);

    }
}

