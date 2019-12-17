using System;
using System.Text;

namespace ArgumentClinic
{
    public class Argument<T> : IArgument
    {
        private string shortForm;
        private string longForm;
        private bool isRequired;
        private string description;
        private T value;
        private T defaultValue;
        private ArgumentType type = ArgumentType.Argument;

        public Argument(string shortArg, string description, bool required = false, T defaultVal = default(T))
        {
            this.shortForm = Global.CleanFlag(shortArg);
            this.description = description;
            if (this.shortForm.Length != 1)
            {
                throw new System.Exception("Short flag must be one alphanumeric character");
            }
            this.isRequired = required;
            this.defaultValue = defaultVal;
        }

        public Argument(string shortArg, string longArg, string description, bool required = false, T defaultVal = default(T))
        {
            this.shortForm = Global.CleanFlag(shortArg);
            this.description = description;
            if (this.shortForm.Length != 1)
            {
                throw new System.Exception("Short flag must be one alphanumeric character");
            }
            this.longForm = Global.CleanFlag(longArg);
            this.isRequired = required;
            this.defaultValue = defaultVal;
        }

        public T ParseArgs(string[] args)
        {
            /* -1 so we go to one before the end of the arguments
             * The last argument cannot be a flag as there would never be a following value to parse
             */
            for(int idx = 0; idx < args.Length - 1; idx++)
            {
                string cleanFlag = Global.CleanFlag(args[idx]);
                if(shortForm == cleanFlag || longForm == cleanFlag)
                {
                    try
                    {
                        return (T)Convert.ChangeType(args[idx + 1], typeof(T));
                    }
                    catch
                    {
                        throw new Exception($"Could not parse argument {args[idx]} {args[idx+1]} (Expected type {typeof(T).Name})");
                    }
                }
            }

            if (this.isRequired)
            {
                throw new System.Exception($"Argument ({shortForm}|{longForm}) not found");
            }
            else return this.defaultValue;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("-"+shortForm);
            if (!string.IsNullOrEmpty(longForm))
                builder.Append(",-" + longForm);
            builder.Append(" [" + typeof(T).Name + "]\t\t" + description);

            return builder.ToString();
        }
    }
}
