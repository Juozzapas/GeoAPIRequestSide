using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Model
{

    public class ResultObject
    {
        public string stderr { get; set; }
        public string stdout { get; set; }
        public string _fileResult { get; set; }
        public ResultObject(string _error, string _output)
        {
            stderr = _error;
            stdout = _output;
        }
        public bool isEmpty()
        {
            if (string.IsNullOrEmpty(stderr) && string.IsNullOrEmpty(stdout))
            {
                return true;
            }
            return false;
        }
    }
}
