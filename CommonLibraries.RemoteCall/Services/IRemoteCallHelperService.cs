using System.Collections.Generic;
using System.Threading.Tasks;
using CommonLibraries.RemoteCall.Enums;
using CommonLibraries.RemoteCall.Models;
using Newtonsoft.Json;

namespace CommonLibraries.RemoteCall.Services
{
    public interface IRemoteCallHelperService
    {
        Task<T> ExecuteGetAsync<T>(string url, int? timeoutInMilliseconds = null, Credentials credentials = null) where T : class;

        T ExecuteGet<T>(string url, int? timeoutInMilliseconds = null, Credentials credentials = null) where T : class;

        Task<string> ExecuteGetAsStringAsync(string url, int? timeoutInMilliseconds = null, Credentials credentials = null);

        Task<byte[]> ExecuteGetAsByteArrayAsync(string url, int? timeoutInMilliseconds = null, Credentials credentials = null);

        Task<T> ExecutePostAsync<T, U>(string url, U data, int? timeoutInMilliseconds = null, Credentials credentials = null, JsonSerializerSettings jsonSerializerSettings = null) where U : class;

        T ExecutePost<T, U>(string url, U data, int? timeoutInMilliseconds = null, Credentials credentials = null, JsonSerializerSettings jsonSerializerSettings = null) where T : class where U : class;

        Task<T> ExecutePutAsync<T, U>(string url, U data, int? timeoutInMilliseconds = null, Credentials credentials = null) where T : class where U : class;

        T ExecutePut<T, U>(string url, U data, int? timeoutInMilliseconds = null, Credentials credentials = null) where T : class where U : class;

        Task<T> ExecutePostAsXmlAsync<T, U>(string url, U data, int? timeoutInMilliseconds = null, ContentTypeEnum? contentType = null, Credentials credentials = null)
            where T : class
            where U : class;

        Task<string> ExecutePostAsStringAsync<TRequest>(string url, TRequest data, ContentTypeEnum contentType = ContentTypeEnum.ApplicationJson, int? timeoutInMilliseconds = null, Credentials credentials = null, JsonSerializerSettings jsonSerializerSettings = null);

        Task<T> ExecutePostAsync<T>(string url, string parameters, int? timeoutInMilliseconds = null) where T : class;

        Task<byte[]> ExecutePostWithByteArrayResponseAsync<TRequest>(string url, TRequest parameters, int? timeoutInMilliseconds = null, JsonSerializerSettings jsonSerializerSettings = null);

        Task<TResponse> ExecutePostAsync<TResponse, TRequest>(string url, TRequest request, KeyValuePair<string, string> authParameter, int? timeoutInMilliseconds = null, JsonSerializerSettings jsonSerializerSettings = null);

        Task<TResponse> ExecutePostAsync<TResponse>(string url, IEnumerable<KeyValuePair<string, string>> formContentRequest, KeyValuePair<string, string> authParameter, int? timeoutInMilliseconds = null);

        string BuildUrl(string schemeAndHost, string urlPath, bool joinUrlAsIs = false);
        Task<T> ExecuteDeleteAsync<T, U>(string url, U data, int? timeoutInMilliseconds = null, Credentials credentials = null)
            where T : class
            where U : class;
        T ExecuteDelete<T, U>(string url, U data, int? timeoutInMilliseconds = null, Credentials credentials = null)
            where T : class
            where U : class;
    }
}
