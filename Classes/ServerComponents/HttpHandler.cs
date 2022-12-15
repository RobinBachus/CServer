using CServer.Interfaces;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace CServer.Classes.ServerComponents
{
    internal class HttpHandler
    {
        /// <summary>
        /// If the HTTP listener receives a request, this function will check the type (Preflight or User request) and map the request to a <see cref="RequestData"/> object
        /// </summary>
        /// <param name="listener"></param>
        /// <returns>The mapped <see cref="RequestData"/> object</returns>
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

            // Disregard CORS preflight requests
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

        // TODO: document function
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
                    // If the status code is above 300 an error has occurred
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

        // TODO: document function
        // Adds CORS headers to allow cross origin HTTP requests
        private static void AddCorsHeaders(HttpListenerResponse response)
        {
            response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept, X-Requested-With");
            response.AddHeader("Access-Control-Allow-Methods", "GET, POST");
            response.AddHeader("Access-Control-Max-Age", "1728000");
            response.AppendHeader("Access-Control-Allow-Origin", "*");
        }

        // TODO: document function
        // Retrieves the content from an HTTP request
        private static RequestData ConvertRequestContent(HttpListenerRequest request, HttpListenerContext context)
        {
            string contentString;
            Stream receiveStream = request.InputStream;
            StreamReader readStream = new(receiveStream, Encoding.UTF8);
            contentString = readStream.ReadToEnd();
            RequestData? requestContent = JsonConvert.DeserializeObject<RequestData>(contentString);
            if (requestContent == null)
            {
                throw new Exception("request content was empty");
            }
            requestContent.Context = context;
            return requestContent;
        }
    }
}
