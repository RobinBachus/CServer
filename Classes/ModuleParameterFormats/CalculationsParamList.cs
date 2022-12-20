using CServer.Classes.ServerComponents;

namespace CServer.Classes.ModuleParameterFormats
{
    /// <inheritdoc/>
    internal class CalculationsParamList : ParamListBase
    {
        /// <summary>
        /// Initiates a new <see cref="CalculationsParamList"/> using the parameters from a <see cref="RequestData"/> object
        /// </summary>
        /// <param name="request">The request that holds the parameters</param>
        /// <exception cref="FormatException"></exception>
        public CalculationsParamList(RequestData request)
        {
            if (request.Parameters == null)
            {
                throw new FormatException("No parameters were given");
            }

            // I don't null-check thus one because it is a selector on the website, so it should always be a non-empty string
            @operator = request.Parameters[2];

            if (request.Parameters[0] == null || request.Parameters[0] == "")
                throw (new FormatException("First field can't be empty"));

            param1 = Convert.ToDouble(request.Parameters[0]);

            if (request.Parameters[1] == "" && !IsSingleInputOperator(@operator))
            {
                throw new FormatException("Second field empty on an operator that is not single input");
            }
            else if (request.Parameters[1] == "") param2 = 0;
            else param2 = Convert.ToDouble(request.Parameters[1]);
        }

        public readonly double param1;
        public readonly double param2;
        public readonly string @operator;
    }
}
