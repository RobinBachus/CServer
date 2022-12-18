using CServer.Classes.ModuleParameterFormats;
using CServer.Classes.ServerComponents;
using CServer.Interfaces;

namespace CServer.Classes.ModuleClasses
{
    /// <summary>
    ///     <inheritdoc cref="IModule"/>
    /// <para>
    ///     This is the class for the BooleanOperations tab/ <see cref="RequestData.Module">Module</see>
    /// </para>
    /// </summary>
    internal class BooleanOperations : IModule
    {
        public RequestData GetResult(RequestData request)
        {
            if (request.Parameters == null)
            {
                throw new NullReferenceException("Parameters array is null");
            }

            BooleanOparationsParamList paramList = new(request);
            bool result;

            if (paramList.mode == BooleanOparationsParamList.Mode.numbers)
            {
                double a = Convert.ToDouble(paramList.param1);
                double b = Convert.ToDouble(paramList.param2);
                
                result = paramList.@operator switch
                {
                    "==" => a == b,
                    ">=" => a >= b,
                    "<=" => a <= b,
                    ">" => a > b,
                    "<" => a < b,
                    "P" => IsPrime(a),
                    _ => throw new NotImplementedException($"Operator {paramList.@operator} not recognized"),
                };
            }
            else {
                result = paramList.param1.Equals(paramList.param2);
            }

            if (paramList.flag)
            {
                result = !result;
            }

            request.Result = result;
            return request;
        }

        /// <summary>
        /// Tests if a number is prime
        /// </summary>
        /// <param name="a">The number to test</param>
        /// <returns><see cref="bool">true</see> if <paramref name="a"/> is prime and <see cref="bool">false</see> if not</returns>
        protected static bool IsPrime(double a)
        {
            if (a <= 1) return false;

            int div = 0;
            for (int i = 0; i < a; i++)
            {
                if ((a % i) == 0) {
                    div++;
                }
            }
            return div < 2;
        }
    }
}
