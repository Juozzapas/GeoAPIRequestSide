using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.CommandBuilder
{
    public class CommandBuilder : ICommandBuilder
    {
        private StringBuilder command;
        public CommandBuilder(StringBuilder command)
        {
            this.command = command;
        }

        public void AddDataSource(string dst_datasource, string src_datasource)
        {
            command.AppendFormat(" {0} {1}", dst_datasource, src_datasource);
        }

        public void AdditionalArguments(string param, string argument)
        {
            command.AppendFormat(" {0} \"{1}\"", param, argument);
        }

        public void SignleParameter(string param)
        {
            command.AppendFormat(" {0}", param);
        }

        public void AddProgramName(string programName)
        {
            command.Append(programName);
        }
        public String GetResult()
        {
            return command.ToString();
        }
    }
}
