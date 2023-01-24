using CServer.Classes.ModuleClasses;
using CServer.Classes.ServerComponents;
using CServer.Interfaces;
using System.Net;
using System.Runtime.CompilerServices;

// Allow test project to test internal class methods
[assembly: InternalsVisibleTo("CServerTests")]

namespace CServer
{
    /// <summary>
    /// This program is the back-end of a <see href="https://github.com/RobinBachus/CSharper">website</see> that allows users to test many interactions with c# 
    /// (and is made purely for me to learn more about c# and web development).
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// <para>
        ///     When the Main function is started, it starts an <see cref="HttpListener"/> and enters a loop. 
        ///     In this loop the program will wait until a request is received. </para>
        /// <para>
        ///     Once a request is received it will be mapped to a <see cref="RequestData"/> object 
        ///     and handled by the appropriate <see cref="IModule">Module Class</see>.
        /// </para>
        ///     Finally a response is sent to the browser containing the results.
        /// </summary>
        static void Main(string[] args)
        {
            // The port where the server will be listening on
            int port = 8000;

            // Start the listener
            using var listener = new HttpListener();
            listener.Prefixes.Add($"http://localhost:{port}/");
            listener.Start();
            Console.WriteLine($"Listening on port {port}...");
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
                    continue;
                }
                    
                try
                {
                    // Process the request in the appropriate module
                    switch (requestData.Module)
                    {
                        // Disregard CORS preflight requests
                        case Modules.Preflight:
                            break;
                        case Modules.Calculations:
                            Calculations.GetResult(requestData);
                            break;
                        case Modules.BooleanOperations:
                            BooleanOperations.GetResult(requestData);
                            break;
                        // Not implemented 
                        case Modules.RandomGenerator:
                            RandomGenerator.GetResult(requestData);
                            break;
                        // Not implemented 
                        case Modules.Converter:
                            Converter.GetResult(requestData);
                            break;
                        default:
                            throw new NotImplementedException("Request module was not recognized");
                    }
                }
                catch (FormatException ex)
                {
                    // A non-disabled field was empty or invalid
                    Information.LogException(ex, $"Failed to handle {requestData.Module} request:", requestData, HttpStatusCode.BadRequest);
                }
                catch (NotImplementedException ex)
                {
                    Information.LogException(ex, $"Failed to handle {requestData.Module} request:", requestData, HttpStatusCode.NotImplemented);
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
