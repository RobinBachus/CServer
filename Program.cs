using CServer.Classes;
using CServer.Interfaces;
using System.Net;

namespace CServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // The port where the server will be listening on
            int port = 8000;

            using var listener = new HttpListener();
            listener.Prefixes.Add($"http://localhost:{port}/");
            listener.Start();
            Console.WriteLine("Listening on port 8000...");

            while (true) {
                // Get the requests content
                RequestData requestData = HttpHandler.GetHTTPRequest(listener);

                // Get result for request based on module
                switch (requestData.Module)
                {
                    case Modules.Preflight:
                        break;
                    case Modules.Calculations:
                        requestData = Calculations.GetResult(requestData);
                        break;
                    case Modules.BooleanOperations:
                        break;
                    case Modules.RandomGenerator:
                        break;
                    case Modules.Converter:
                        break;
                    default:
                        break;
                }
                HttpHandler.SetResponse(requestData);
            }
        }
    }
}