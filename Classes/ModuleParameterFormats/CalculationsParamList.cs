using CServer.Classes.ServerComponents;

namespace CServer.Classes.ModuleParameterFormats
{
    internal class CalculationsParamList : ParamListBase
    {
        public CalculationsParamList(RequestData request)
        {
            if (request.Parameters == null)
            {
                throw new FormatException("No parameters were given");
            }

            this.@operator = request.Parameters[2];

            if (request.Parameters[0] == null || request.Parameters[0] == "")
                throw (new FormatException("First field can't be empty"));

            this.param1 = Convert.ToDouble(request.Parameters[0]);

            if (request.Parameters[1] == "" && !IsSingleInputOpaerator(this.@operator))
            {
                throw new FormatException("Second field empty on an operator that is not single input");
            }
            else if (request.Parameters[1] == "") this.param2 = 0;
            else this.param2 = Convert.ToDouble(request.Parameters[1]);
        }

        public readonly double param1;
        public readonly double param2;
        public readonly string @operator;
    }
}
