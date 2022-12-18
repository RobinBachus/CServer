using CServer.Interfaces;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace CServer.Classes.ServerComponents
{
    /// <summary>
    /// The class that deals with receiving HTTP requests and sending responses to the browser
    /// </summary>
    internal class HttpHandler
    {
        /// <summary>
        /// This method will wait for an HTTP request, check the type (Preflight or User request) and map it to a <see cref="RequestData"/> object
        /// </summary>
        /// <param name="listener">The <see cref="HttpListener"/> object used to receive requests</param>
        /// <returns>The mapped <see cref="RequestData"/> object</returns>
        /// <exception cref="JsonException"><see cref="ConvertRequestContent"/> will throw this exception if it can't deserialize the request</exception>
        public static RequestData GetHTTPRequest(HttpListener listener)
        {
            HttpListenerContext context = listener.GetContext();
            HttpListenerRequest req = context.Request;


            if (req.HttpMethod == "POST")
            {
                Console.WriteLine($"Received request for {req.Url}");
                Console.WriteLine($"from {req.UrlReferrer}");
                RequestData content = ConvertRequestContent(req, context);

                return content;
            }
            else if (req.HttpMethod == "OPTIONS")
            {
                RequestData content = new(context)
                {
                    Module = Modules.Preflight,
                };
                return content;
            }
            else
            {
                RequestData content = new(context)
                {
                    Module = Modules.Undefined,
                    StatusCode = HttpStatusCode.MethodNotAllowed,
                    StatusDescription = "Method Not Allowed"
                };

                return content;
            }
        }

        /// <summary>
        /// Uses the <see cref="RequestData"/> response property to send a response to the browser
        /// </summary>
        /// <param name="request">The <see cref="RequestData"/> request to respond to</param>
        /// <exception cref="NullReferenceException">Thrown if the Result property of the <paramref name="request"/> is empty</exception>
        public static void SetResponse(RequestData request)
        {
            HttpListenerContext context = request.Context;
            using HttpListenerResponse response = context.Response;
            response.StatusCode = (int)request.StatusCode;
            response.StatusDescription = request.StatusDescription;
            AddCorsHeaders(response);

            if (request.Module != Modules.Preflight)
            {
                response.Headers.Set("Content-Type", "text/plain");
                string output;

                if (request.Result != null)
                {
                    // If the status code is 300 or above an error has occurred
                    if (request.StatusCode < HttpStatusCode.Ambiguous)
                    {
                        Information.LogRequest(request);
                    } 
                    output = request.Result.ToString()
                        ?? throw new NullReferenceException("Result.ToString() is null");
                    byte[] buffer = Encoding.UTF8.GetBytes(output);
                    response.ContentLength64 = buffer.Length;

                    using Stream ros = response.OutputStream;
                    ros.Write(buffer, 0, buffer.Length);
                }
            }
        }

        /// <summary>
        /// Adds CORS headers to allow cross origin HTTP requests
        /// </summary>
        /// <param name="response">The response that needs headers</param>
        private static void AddCorsHeaders(HttpListenerResponse response)
        {
            response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept, X-Requested-With");
            response.AddHeader("Access-Control-Allow-Methods", "GET, POST");
            response.AddHeader("Access-Control-Max-Age", "1728000");
            response.AppendHeader("Access-Control-Allow-Origin", "*");
        }

        /// <summary>
        /// Maps the received request JSON to a <see cref="RequestData"/> object and sets the <see cref="HttpListenerContext"/> on that object
        /// </summary>
        /// <param name="serializedRequest">The serialized JSON of the received request</param>
        /// <param name="context">The context object that was initialized when the <paramref name="serializedRequest"/> was received</param>
        /// <returns>A <see cref="RequestData"/> object that contains the deserialized data of <paramref name="serializedRequest"/></returns>
        /// <exception cref="JsonException">Thrown if the method is unable to get content from the <paramref name="serializedRequest"/></exception>
        private static RequestData ConvertRequestContent(HttpListenerRequest serializedRequest, HttpListenerContext context)
        {
            string contentString;
            Stream receiveStream = serializedRequest.InputStream;
            StreamReader readStream = new(receiveStream, Encoding.UTF8);
            contentString = readStream.ReadToEnd();
            RequestData requestData = JsonConvert.DeserializeObject<RequestData>(contentString) 
                ?? throw new JsonException("Unable to get content from request");
            requestData.Context = context;
            return requestData;
        }
    }
}
