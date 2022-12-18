using CServer.Classes.ServerComponents;

namespace CServer.Interfaces
{
    /// <summary>
    /// The Module classes correspond with a tab on the website and contain all the code that is needed to handle requests from that tab.
    /// </summary>
    internal interface IModule
    {
        /// <summary>
        /// Get the result for the request
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The request with the result value set</returns>
        /// <exception cref="NotImplementedException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="FormatException"></exception>
        public RequestData GetResult(RequestData request);
    }
}
