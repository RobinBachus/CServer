using CServer.Classes.ModuleParameterFormats;
using CServer.Classes.ServerComponents;

namespace CServer.Interfaces
{
    internal interface IModule
    {
        /// <summary>
        /// Get the result for the request
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The request with the result value set</returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="NotImplementedException"></exception>
        public RequestData GetResult(RequestData request);
    }
}
