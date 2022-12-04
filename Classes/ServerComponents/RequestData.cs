using CServer.Interfaces;
using System.Net;

namespace CServer.Classes.ServerComponents
{
    /// <summary>
    /// An object that contains information about an incoming HTTP request.
    /// </summary>
    internal class RequestData : IRequestData
    {
        /// <summary>
        /// This initializes a RequestData object. 
        /// <list>
        ///     <listheader>
        ///         <term> default values</term>
        ///         <description>
        ///             The default values set when making a new RequestData object
        ///         </description>
        ///     </listheader>
        ///     <item>
        ///         <term> - int <see cref="StatusCode">StatusCode</see></term>
        ///         <description><see cref="HttpStatusCode.OK">HttpStatusCode.OK</see> (=200)</description>
        ///     </item>
        ///     <item>
        ///         <term> - string <see cref="StatusDescription">StatusDescription</see></term>
        ///         <description>"Status OK"</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="Context">The request <see cref="HttpListenerContext">context</see></param>
        public RequestData(HttpListenerContext Context)
        {
            this.Context = Context;
        }

        public Modules Module { get; set; }
        public string[]? Parameters { get; set; }
        public object? Result { get; set; }
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public string StatusDescription { get; set; } = "Status OK";
        public HttpListenerContext Context { get; set; }
    }
}
