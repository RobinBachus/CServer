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
        /// <param name="request">The <see cref="RequestData"/> object to get results for</param>
        /// <exception cref="NotImplementedException">Usually thrown if the user uses a parameter that is to be implemented in the future</exception>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="FormatException"></exception>
        public static void GetResult(RequestData request) => throw new NotImplementedException("Converter class not yet available");
    }
}
