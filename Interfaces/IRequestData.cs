using System.Net;

namespace CServer.Interfaces
{
    enum Modules
    {
        Undefined = -1,
        Preflight = 0,
        Calculations = 1,
        BooleanOperations = 2,
        RandomGenerator = 3,
        Converter = 4
    }

    /// <summary>
    /// Defines the properties available on a request object.
    /// <para>
    /// The <see cref="Context">Context</see> property must be set at creation of a new instance.
    /// </para>
    /// </summary>
    internal interface IRequestData
    {
        /// <summary>
        /// The <see cref="Modules">Module</see> used by the main program to decide the class that processes the request.
        /// </summary>
        public Modules Module { get; set; }
        /// <summary>
        /// An array of the parameters given by the user.
        /// </summary>  
        public string[]? Parameters { get; set; }
        /// <summary>
        /// The object that will be sent back to the browser.
        /// </summary>  
        public object? Result { get; set; }
        /// <summary>
        /// The <see cref="HttpStatusCode">HTTP status code</see> for the response.
        /// </summary>  
        public HttpStatusCode StatusCode { get; set; }
        /// <summary>
        /// The HTTP status description.
        /// </summary> 
        public string StatusDescription { get; set; }
        /// <summary>
        /// The <see cref="HttpListenerContext">context</see> that allows a response to be set.
        /// </summary>  
        public HttpListenerContext Context { get; set; }
    }
}
