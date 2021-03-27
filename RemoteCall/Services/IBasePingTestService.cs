using System.Threading.Tasks;
using CommonLibraries.RemoteCall.Models;

namespace CommonLibraries.RemoteCall.Services
{
    public interface IBasePingTestService
    {
        string PingKey { get; }

        Task<PingTestResponse> Ping();
    }
}
