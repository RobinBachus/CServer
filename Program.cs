using CServer.Classes.ModuleClasses;
using CServer.Classes.ServerComponents;
using CServer.Interfaces;
using System.Net;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CServerTests")]

namespace CServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // The port where the server will be listening on
            int port = 8000;

            // Start the listener
            using var listener = new HttpListener();
            listener.Prefixes.Add($"http://localhost:{port}/");
            listener.Start();
            Console.WriteLine("Listening on port 8000...");
            Console.WriteLine("\n-------------------------------------\n");

            while (true) {
                RequestData requestData;
                try
                {
                    // Get the content from an incoming request
                    requestData = HttpHandler.GetHTTPRequest(listener);
                }
                catch (Exception ex)
                {
                    Information.LogException(ex, "Failed to get request data due to an exception:");
                    break;
                }
                    
                try
                {
                    // Process the request in the appropriate module
                    switch (requestData.Module)
                    {
                        case Modules.Preflight:
                            break;
                        case Modules.Calculations:
                            Calculations calculations = new();
                            requestData = calculations.GetResult(requestData);
                            break;
                        case Modules.BooleanOperations:
                            BooleanOperations booleanOperations = new();
                            requestData = booleanOperations.GetResult(requestData);
                            break;
                        // Not implemented 
                        case Modules.RandomGenerator:
                            RandomGenerator randomGenerator = new();
                            requestData = randomGenerator.GetResult(requestData);
                            break;
                        // Not implemented 
                        case Modules.Converter:
                            Converter converter = new();
                            requestData = converter.GetResult(requestData);
                            break;
                        default:
                            throw new NotImplementedException("Request module was not recognized");
                    }
                }
                catch (NotImplementedException ex)
                {
                    Information.LogException(ex, $"Failed to handle {requestData.Module} request:", requestData, 501);
                }
                catch (Exception ex)
                {
                    Information.LogException(ex, $"Failed to handle {requestData.Module} request:", requestData);
                }
                finally
                {
                    // Send response to request origin
                    HttpHandler.SetResponse(requestData);
                }
            }
        }
    }
}