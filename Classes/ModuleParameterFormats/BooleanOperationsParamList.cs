using CServer.Classes.ServerComponents;

namespace CServer.Classes.ModuleParameterFormats
{
    /// <inheritdoc/>
    internal class BooleanOperationsParamList: ParamListBase
    {
        public enum Mode
        {
            numbers,
            text
        }

        /// <summary>
        /// Initiates a new <see cref="BooleanOperationsParamList"/> using the parameters from a <see cref="RequestData"/> object
        /// </summary>
        /// <param name="request">The request that holds the parameters</param>
        /// <exception cref="FormatException"></exception>
        public BooleanOperationsParamList(RequestData request) 
        {
            if (request.Parameters == null)
            {
                throw new FormatException("No parameters were given");
            }

            param1 = request.Parameters[0];
            param2 = request.Parameters[1];
            @operator = request.Parameters[2];
            mode = (Mode)Convert.ToInt32(request.Parameters[3]);
            flag = Convert.ToBoolean(request.Parameters[4]);
           
            if (param1 == null || param1 == "") 
                throw (new FormatException("First field can't be empty"));

            if (param2 == "")
                param2 = null;

            if (param2 == null && !IsSingleInputOperator(@operator))
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
