using System;
using System.Threading.Tasks;
using CommonLibraries.RemoteCall.Models;
using Microsoft.Extensions.Configuration;

namespace CommonLibraries.RemoteCall.Services.Implementation
{
    public abstract class BasePingTestService : BaseRemoteCallService, IBasePingTestService
    {
        protected BasePingTestService(
            IConfiguration configuration,
            IRemoteCallHelperService remoteCallHelperService) : base(configuration, remoteCallHelperService)
        { }

        protected abstract string _pingKey { get; }

        public string PingKey => _pingKey;

        protected abstract string _path { get; }

        public async Task<PingTestResponse> Ping()
        {
            try
            {
                var remoteResponse = await ExecuteGetAsync<string>(_path, 3000);

                if (string.IsNullOrEmpty(remoteResponse))
                {
                    return new PingTestResponse()
                    {
                        IsSuccess = false,
                        ErrorMessage = "Удаленный сервер вернул пустой ответ."
                    };
                }
                else if (remoteResponse != PingKey)
                {
                    return new PingTestResponse()
                    {
                        IsSuccess = false,
                        ErrorMessage = "Ответ удаленного сервера не совпадает с ключом приложения."
                    };
                }
                else
                {
                    return new PingTestResponse()
                    {
                        IsSuccess = true
                    };
                }
            }
            catch (Exception e)
            {
                return new PingTestResponse()
                {
                    IsSuccess = false,
                    ErrorMessage = $"Ошибка - {e.Message}."
                };
            }
        }
    }
}
