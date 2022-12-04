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

            double a = Convert.ToDouble(request.Parameters[0]);
            double b = Convert.ToDouble(request.Parameters[1]);
            string @operator = Convert.ToString(request.Parameters[2]);

            // TODO: Add remaining operators from plan list
            request.Result = @operator switch
            {
                "==" => a == b,
                _ => true
            };

            return request;
        }
    }
}
