using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArgumentClinic
{
    public class Clinic
    {
        private List<IArgument> arguments;

        public Clinic()
        {
            arguments = new List<IArgument>();
        }

        public Clinic AddArgument(IArgument argument)
        {
            arguments.Add(argument);
            return this;
        }

        public void ParseInput(string input)
        {

        }

        public string GetHelp()
        {
            StringBuilder sBuilder = new StringBuilder();

            foreach(var arg in arguments)
            {
                sBuilder.AppendLine(arg.ToString());
            }
            sBuilder.AppendLine("-h, --help\t\tShow this help message");

            return sBuilder.ToString();
        }
    }
}
