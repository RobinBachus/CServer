using CServer.Interfaces;
using System.Net;

namespace CServer.Classes
{
    internal class RequestData : IRequestData
    {
        public RequestData(HttpListenerContext Context)
        {
            this.Context = Context;
        }
        public Modules Module { get; set; }
        public int Submodule { get; set; }
        public string[]? Parameters { get; set; } 
        public object? Result { get; set; }
        public HttpListenerContext Context { get; set; }
    }
}
