using CServer.Classes;


namespace CServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // The port where the server will be listening 
            int port = 8000;
            HttpHandler.HandleHTTPRequest(port);
        }
    }
}