﻿using CServer.Classes.ModuleParameterFormats;
using CServer.Classes.ServerComponents;
using CServer.Interfaces;

namespace CServer.Classes.ModuleClasses
{
    internal class BooleanOperations : IModule
    {
        public RequestData GetResult(RequestData request)
        {
            if (request.Parameters == null)
            {
                throw new NullReferenceException("Parameters array is null");
            }

            Information.LogRequest(request);

            BooleanOparationsParamList paramList = new(request);
            bool result;

            if (paramList.mode == BooleanOparationsParamList.Mode.numbers)
            {
                double a = Convert.ToDouble(paramList.param1);
                double b = 0;
                if (paramList.param2 != null)
                {
                    b = Convert.ToDouble(paramList.param2);
                }

                // TODO: Add remaining operators from plan list
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
