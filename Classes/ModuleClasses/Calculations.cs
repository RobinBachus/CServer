using CServer.Classes.ModuleParameterFormats;
using CServer.Classes.ServerComponents;
using CServer.Interfaces;

namespace CServer.Classes.ModuleClasses
{
    internal class Calculations : IModule
    {
        const decimal PI = 3.1415926535897932384626433832M;

        // TODO: document function
        public RequestData GetResult(RequestData request)
        {
            if (request.Parameters == null)
            {
                throw new NullReferenceException("Parameters array is null");
            }

            CalculationsParamList paramList = new(request);

            double a = paramList.param1;
            double b = paramList.param2;
            string @operator = paramList.@operator;

            request.Result = @operator switch
            {
                "+" => a + b,
                "-" => a - b,
                "*" => a * b,
                "/" => a / b,
                "%" => a % b,
                "^" => Math.Pow(a, b),
                "√" => Math.Sqrt(a),
                "π" => Math.Round(PI, Convert.ToInt32(a)),
                _ => throw new NotImplementedException($"Operator '{@operator}' not recognized"),
            };
            return request;
        }
    }
}