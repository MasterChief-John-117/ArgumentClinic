using System.Text;

namespace ArgumentClinic
{
    public class Switch : IArgument
    {
        private string shortForm;
        private string longForm;
        private string description;
        private bool value;
        private ArgumentType type = ArgumentType.Switch;

        public Switch(string shortArg, string description)
        {
            this.shortForm = Global.CleanFlag(shortArg);
            this.description = description;
            if(this.shortForm.Length != 1)
            {
                throw new System.Exception("Short flag must be one alphanumeric character");
            }
        }

        public Switch(string shortArg, string longArg, string description)
        {
            this.shortForm = Global.CleanFlag(shortArg);
            this.description = description;
            if (this.shortForm.Length != 1)
            {
                throw new System.Exception("Short flag must be one alphanumeric character");
            }
            this.longForm = longArg.Trim('-');
        }

        public bool ParseArgs(string[] args)
        {
            for (int idx = 0; idx < args.Length - 1; idx++)
            {
                string cleanFlag = Global.CleanFlag(args[idx]);
                if(shortForm == cleanFlag || longForm == cleanFlag)
                    return true;
            }
            return false;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("-" + shortForm);
            if (!string.IsNullOrEmpty(longForm))
                builder.Append(",-" + longForm);
            builder.Append(" [switch]\t\t" + description);

            return builder.ToString();
        }
    }
}
