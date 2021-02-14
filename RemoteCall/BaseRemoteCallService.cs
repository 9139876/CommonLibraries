using System;
using System.Threading.Tasks;
using CommonLibraries.RemoteCall.Models;
using CommonLibraries.RemoteCall.Services;
using Microsoft.Extensions.Configuration;

namespace CommonLibraries.RemoteCall
{
    public abstract class BaseRemoteCallService
    {
        #region fields

        protected abstract string _apiSchemeAndHostConfigKey { get; set; }

        private readonly IConfiguration _configuration;

        private readonly IRemoteCallHelperService _remoteCallHelperService;

        #endregion

        #region ctor

        protected BaseRemoteCallService(
            IConfiguration configuration,
            IRemoteCallHelperService remoteCallHelperService)
        {
            _configuration = configuration;
            _remoteCallHelperService = remoteCallHelperService;
        }

        #endregion

        protected async Task<TResponse> ExecuteDeleteAsync<TResponse, TRequest>(string path, TRequest request, int? timeoutMiliseconds = null)
            where TRequest : class where TResponse : class
        {
            try
            {
                var url = GetUrl(path);

                var result = await _remoteCallHelperService.ExecuteDeleteAsync<TResponse, TRequest>(url: url, data: request, timeoutInMilliseconds: timeoutMiliseconds);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected TResponse ExecuteDelete<TResponse, TRequest>(string path, TRequest request, int? timeoutMiliseconds = null)
            where TRequest : class where TResponse : class
        {
            try
            {
                var url = GetUrl(path);

                var result = _remoteCallHelperService.ExecuteDelete<TResponse, TRequest>(url: url, data: request, timeoutInMilliseconds: timeoutMiliseconds);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected async Task<byte[]> ExecutePostAsync<TRequest>(string path, TRequest request, int? timeoutMiliseconds = null)
        {
            try
            {
                var url = GetUrl(path);

                var result = await _remoteCallHelperService.ExecutePostWithByteArrayResponseAsync(url: url, parameters: request, timeoutInMilliseconds: timeoutMiliseconds);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected async Task<TResponse> ExecuteGetAsync<TResponse>(string path, int? timeoutMiliseconds = null) where TResponse : class
        {
            try
            {
                var url = GetUrl(path);

                var result = await _remoteCallHelperService.ExecuteGetAsync<TResponse>(url: url, timeoutInMilliseconds: timeoutMiliseconds);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected TResponse ExecuteGet<TResponse>(string path, int? timeoutMiliseconds = null) where TResponse : class
        {
            try
            {
                var url = GetUrl(path);

                var result = _remoteCallHelperService.ExecuteGet<TResponse>(url: url, timeoutInMilliseconds: timeoutMiliseconds);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected async Task<TResponse> ExecutePostWithCredentialsAsync<TResponse, TRequest>(string path, TRequest request, int? timeoutMiliseconds = null, Credentials credentials = null)
            where TRequest : class
        {
            try
            {
                var url = GetUrl(path);

                var result = await _remoteCallHelperService.ExecutePostAsync<TResponse, TRequest>(url: url, data: request, timeoutInMilliseconds: timeoutMiliseconds, credentials: credentials);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected async Task<TResponse> ExecutePostAsync<TResponse, TRequest>(string path, TRequest request, int? timeoutMiliseconds = null)
    where TRequest : class
        {
            try
            {
                var url = GetUrl(path);

                var result = await _remoteCallHelperService.ExecutePostAsync<TResponse, TRequest>(url: url, data: request, timeoutInMilliseconds: timeoutMiliseconds);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected TResponse ExecutePost<TResponse, TRequest>(string path, TRequest request, int? timeoutMiliseconds = null)
            where TRequest : class where TResponse : class
        {
            try
            {
                var url = GetUrl(path);

                var result = _remoteCallHelperService.ExecutePost<TResponse, TRequest>(url: url, data: request, timeoutInMilliseconds: timeoutMiliseconds);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected async Task<TResponse> ExecutePutAsync<TResponse, TRequest>(string path, TRequest request, int? timeoutMiliseconds = null, Credentials credentials = null)
            where TRequest : class where TResponse : class
        {
            try
            {
                var url = GetUrl(path);

                var result = await _remoteCallHelperService.ExecutePutAsync<TResponse, TRequest>(url: url, data: request, timeoutInMilliseconds: timeoutMiliseconds, credentials: credentials);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected TResponse ExecutePut<TResponse, TRequest>(string path, TRequest request, int? timeoutMiliseconds = null)
            where TRequest : class where TResponse : class
        {
            try
            {
                var url = GetUrl(path);

                var result = _remoteCallHelperService.ExecutePut<TResponse, TRequest>(url: url, data: request, timeoutInMilliseconds: timeoutMiliseconds);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected async Task<string> ExecutePostAsStringAsync<TRequest>(string path, TRequest request, int? timeoutMiliseconds = null)
        {
            try
            {
                var url = GetUrl(path);

                var result = await _remoteCallHelperService.ExecutePostAsStringAsync(url: url, data: request, timeoutInMilliseconds: timeoutMiliseconds);

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected async Task<TResponse> ExecuteAuthPutAsync<TResponse, TRequest>(string path, TRequest request,
            Credentials authParameter, int? timeoutMiliseconds = null)
            where TRequest : class
            where TResponse : class
        {
            try
            {
                var url = GetUrl(path);

                var result = await _remoteCallHelperService.ExecutePutAsync<TResponse, TRequest>(
                    url: url,
                    data: request,
                    timeoutInMilliseconds: timeoutMiliseconds,
                    credentials: authParameter);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected async Task<TResponse> ExecuteAuthPostAsync<TResponse, TRequest>(string path, TRequest request,
            Credentials authParameter, int? timeoutMiliseconds = null) where TRequest : class where TResponse : class
        {
            try
            {
                var url = GetUrl(path);

                var result = await _remoteCallHelperService.ExecutePostAsync<TResponse, TRequest>(
                    url: url,
                    data: request,
                    timeoutInMilliseconds: timeoutMiliseconds,
                    credentials: authParameter);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected async Task<TResponse> ExecuteAuthGetAsync<TResponse>(string path,
                Credentials authParameter, int? timeoutMiliseconds = null) where TResponse : class
        {
            try
            {
                var url = GetUrl(path);

                var result = await _remoteCallHelperService.ExecuteGetAsync<TResponse>(
                    url: url,
                    timeoutInMilliseconds: timeoutMiliseconds,
                    credentials: authParameter);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetUrl(string path)
        {
            if (string.IsNullOrEmpty(_apiSchemeAndHostConfigKey) == true)
                throw new InvalidOperationException("ApiSchemeAndHostConfigKey is empty");

            var schemeAndHost = _configuration[_apiSchemeAndHostConfigKey];

            if (string.IsNullOrEmpty(schemeAndHost))
                throw new InvalidOperationException($"schemeAndHost is empty by key = {_apiSchemeAndHostConfigKey}");

            var url = _remoteCallHelperService.BuildUrl(schemeAndHost: schemeAndHost, urlPath: path);
            return url;
        }
    }
}
