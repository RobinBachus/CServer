using CServer.Classes.ServerComponents;

namespace CServer.Classes.ModuleParameterFormats
{
    internal class BooleanOparationsParamList: ParamListBase
    {
        public enum Mode
        {
            numbers,
            text
        }

        public BooleanOparationsParamList(RequestData request) 
        {
            if (request.Parameters == null)
            {
                throw new FormatException("No parameters were given");
            }

            this.param1 = request.Parameters[0];
            this.param2 = request.Parameters[1];
            this.@operator = request.Parameters[2];
            this.mode = (Mode)Convert.ToInt32(request.Parameters[3]);
            this.flag = Convert.ToBoolean(request.Parameters[4]);
           
            if (this.param1 == null || this.param1 == "") 
                throw (new FormatException("First field can't be empty"));

            if (this.param2 == "")
                this.param2 = null;

            if (this.param2 == null && !IsSingleInputOpaerator(this.@operator))
            {
                throw new FormatException("Second field empty on an operator that is not single input");
            }
        }

        public readonly string param1;
        public readonly string? param2;
        public readonly string @operator;
        public readonly Mode mode;
        public readonly bool flag;
    }
}
