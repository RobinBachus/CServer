using CServer.Classes.ServerComponents;

namespace CServer.Interfaces
{
    internal interface IModule
    {
        public RequestData GetResult(RequestData request);
    }
}
