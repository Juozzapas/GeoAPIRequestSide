using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Functions;
using WebApplication.Model;

namespace WebApplication.RunThirdParty
{
    public class ProcessPython : IProcessPython
    {
        private readonly ILogger _logger;
        public ProcessPython(ILogger<ProcessPython> logger)
        {
            _logger = logger;
        }
        public ResultObject RunCMD(string args, string filename)
        {
            string eOut = null;
            var process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;

            process.ErrorDataReceived += new DataReceivedEventHandler((sender, e) =>
            { eOut += e.Data; });
            process.StartInfo.FileName = filename;
            process.StartInfo.Arguments = args;
            process.Start();
            // var outputReadTask = Task.Run(() => stream.ReadToEnd());

            //return outputReadTask.Result;
            // To avoid deadlocks, use an asynchronous read operation on at least one of the streams.  
            process.BeginErrorReadLine();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

           _logger.LogInformation($"Stdout:{output},Stderr:{eOut}");

            return new ResultObject(eOut, output);
        }
    }

}
//PythonScriptsRunner.run_cmd(@"C:\Users\j.balciunas\PycharmProjects\Praktika1\lightExample.py", @"C:\Users\j.balciunas\Downloads\d\11.geojson");

//C:\OSGeo4W64\runPythonAwareOfQgis.cmd C:\Users\j.balciunas\PycharmProjects\Praktika1\selectByAttribute.py s s s s