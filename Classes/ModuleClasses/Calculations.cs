using CServer.Classes.ServerComponents;
using CServer.Interfaces;

namespace CServer.Classes.ModuleClasses
{
    internal class Calculations : IModule
    {
        const decimal PI = 3.1415926535897932384626433832M;

        public RequestData GetResult(RequestData request)
        {
            if (request.Parameters == null)
            {
                throw new NullReferenceException("Parameters array is null");
            }

            double a = Convert.ToDouble(request.Parameters[0]);
            double b = Convert.ToDouble(request.Parameters[1]);
            string @operator = Convert.ToString(request.Parameters[2]);

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
                _ => throw new NotImplementedException($"Operator {@operator} not recognized"),
            };
            return request;
        }
    }
}