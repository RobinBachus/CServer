using System.Net;

namespace CServer.Interfaces
{
    enum Modules
    {
        Preflight = 0,
        Calculations = 1,
        BooleanOperations = 2,
        RandomGenerator = 3,
        Converter = 4
    }

    internal interface IRequestData
    {
        public Modules Module { get; set; }
        public int Submodule { get; set; }
        public string[]? Parameters { get; set; }
        public object? Result { get; set; }
        public HttpListenerContext Context { get; set; }

    }
}
