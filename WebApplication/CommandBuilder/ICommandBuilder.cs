using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.CommandBuilder
{
    public interface ICommandBuilder
    {
        void AdditionalArguments(string param, string argument);
        void AddProgramName(string programName);
        void AddDataSource(string dst_datasource, string src_datasource);
        void SignleParameter(string param);
        string GetResult();
    }
}

