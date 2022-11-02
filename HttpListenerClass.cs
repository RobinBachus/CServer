using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace CServer
{
    internal class HttpListenerClass
    {
        // Code will be commented and documented later :)
        public static void HandleHTTPResquest()
        {
            using var listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8000/");

            listener.Start();

            Console.WriteLine("Listening on port 8000...");

            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest req = context.Request;

                if (req.HttpMethod == "POST")
                {
                    Console.WriteLine($"Received request for {req.Url}");
                    object? content = GetRequestContent(req);
                    Console.WriteLine($"Request content: \n{content}\n");

                    using HttpListenerResponse resp = context.Response;
                    resp.Headers.Set("Content-Type", "text/plain");
                    AddCorsHeaders(resp);

                    string data = "Hello there!";
                    byte[] buffer = Encoding.UTF8.GetBytes(data);
                    resp.ContentLength64 = buffer.Length;

                    using Stream ros = resp.OutputStream;
                    ros.Write(buffer, 0, buffer.Length);
                }

                // Disregard CORS preflight requests
                else if (req.HttpMethod == "OPTIONS")
                {
                    using HttpListenerResponse resp = context.Response;

                    resp.StatusCode = (int)HttpStatusCode.OK;
                    resp.StatusDescription = "Status OK";
                    AddCorsHeaders(resp);
                }
                else
                {
                    Console.WriteLine("How did you get here?");
                }
            }
        }

        // Adds CORS headers to allow cross origin HTTP requests
        private static void AddCorsHeaders(HttpListenerResponse response)
        {
            response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept, X-Requested-With");
            response.AddHeader("Access-Control-Allow-Methods", "GET, POST");
            response.AddHeader("Access-Control-Max-Age", "1728000");
            response.AppendHeader("Access-Control-Allow-Origin", "*");
        }

        // Retrieves the content from an HTTP request
        private static object? GetRequestContent(HttpListenerRequest request)
        {
            string contentString;
            using Stream receiveStream = request.InputStream;
            using StreamReader readStream = new(receiveStream, Encoding.UTF8);
            contentString = readStream.ReadToEnd();
            object? requestContent = JsonConvert.DeserializeObject(contentString);
            if (requestContent == null)
            {
                return null;
            }
            return requestContent;
        }
    }
}
