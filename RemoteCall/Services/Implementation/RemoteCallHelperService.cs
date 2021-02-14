using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using CommonLibraries.RemoteCall.Enums;
using CommonLibraries.RemoteCall.Extensions;
using CommonLibraries.RemoteCall.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace CommonLibraries.RemoteCall.Services.Implementation
{
    internal class RemoteCallHelperService : IRemoteCallHelperService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RemoteCallHelperService(
            IHttpClientFactory httpClientFactory
            )
        {
            _httpClientFactory = httpClientFactory;
        }

        private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            Converters = new List<JsonConverter> { new StringEnumConverter() },
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public async Task<T> ExecuteDeleteAsync<T, U>(string url, U data, int? timeoutInMilliseconds = null, Credentials credentials = null) 
            where U : class where T : class
        {
            var parameters = JsonConvert.SerializeObject(data, _jsonSerializerSettings);

            return await ExecuteDeleteAsync<T>(
                url,
                parameters,
                ContentTypeEnum.ApplicationJson,
                credentials,
                timeoutInMilliseconds);
        }

        public T ExecuteDelete<T, U>(string url, U data, int? timeoutInMilliseconds = null, Credentials credentials = null)
            where U : class where T : class
        {
            var parameters = JsonConvert.SerializeObject(data, _jsonSerializerSettings);

            return ExecuteDelete<T>(
                url,
                parameters,
                ContentTypeEnum.ApplicationJson,
                credentials,
                timeoutInMilliseconds);
        }

        public async Task<T> ExecuteGetAsync<T>(string url, int? timeoutInMilliseconds = null, Credentials credentials = null) where T : class
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                SetAuthorization(httpClient, credentials);

                SetTimeout(httpClient, timeoutInMilliseconds);

                var response = await httpClient.GetAsync(new Uri(url));

                return await response.Content.ReadAsAsync<T>();
            }
        }

        public T ExecuteGet<T>(string url, int? timeoutInMilliseconds = null, Credentials credentials = null) where T : class
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                SetAuthorization(httpClient, credentials);

                SetTimeout(httpClient, timeoutInMilliseconds);

                var response = httpClient.GetAsync(new Uri(url)).RunSync();

                return response.Content.ReadAsAsync<T>().RunSync();
            }
        }

        public async Task<string> ExecuteGetAsStringAsync(string url, int? timeoutInMilliseconds = null, Credentials credentials = null)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                SetAuthorization(httpClient, credentials);

                SetTimeout(httpClient, timeoutInMilliseconds);

                var response = await httpClient.GetAsync(new Uri(url));

                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<byte[]> ExecuteGetAsByteArrayAsync(string url, int? timeoutInMilliseconds = null, Credentials credentials = null)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                SetAuthorization(httpClient, credentials);

                SetTimeout(httpClient, timeoutInMilliseconds);

                var resultString = await httpClient.GetAsync(new Uri(url));

                return await resultString.Content.ReadAsByteArrayAsync();
            }
        }

        public async Task<T> ExecutePostAsync<T, U>(string url, 
            U data, 
            int? timeoutInMilliseconds = null, 
            Credentials credentials = null,
            JsonSerializerSettings jsonSerializerSettings = null) where U : class
        {
            var parameters = JsonConvert.SerializeObject(data, jsonSerializerSettings ?? _jsonSerializerSettings);

            return await ExecutePostAsync<T>(url,
                parameters,
                ContentTypeEnum.ApplicationJson,
                credentials,
                timeoutInMilliseconds);
        }

        public T ExecutePost<T, U>(string url, 
            U data, 
            int? timeoutInMilliseconds = null, 
            Credentials credentials = null,
            JsonSerializerSettings jsonSerializerSettings = null) where T : class where U : class
        {
            var parameters = JsonConvert.SerializeObject(data, jsonSerializerSettings ?? _jsonSerializerSettings);

            return ExecutePostAsync<T>(url,
                parameters,
                ContentTypeEnum.ApplicationJson,
                credentials,
                timeoutInMilliseconds).RunSync();

        }

        public async Task<T> ExecutePutAsync<T, U>(string url, U data, int? timeoutInMilliseconds = null, Credentials credentials = null) where T : class where U : class
        {
            var parameters = JsonConvert.SerializeObject(data, _jsonSerializerSettings);

            return await ExecutePutAsync<T>(url,
                parameters,
                ContentTypeEnum.ApplicationJson,
                credentials,
                timeoutInMilliseconds);
        }


        public T ExecutePut<T, U>(string url, U data, int? timeoutInMilliseconds = null, Credentials credentials = null) where T : class where U : class
        {
            var parameters = JsonConvert.SerializeObject(data, _jsonSerializerSettings);

            return ExecutePutAsync<T>(url,
                parameters,
                ContentTypeEnum.ApplicationJson,
                credentials,
                timeoutInMilliseconds).RunSync();
        }

        public async Task<T> ExecutePostAsXmlAsync<T, U>(string url, U data, int? timeoutInMilliseconds = null,
            ContentTypeEnum? contentType = null, Credentials credentials = null)
            where T : class
            where U : class
        {
            var xmlSerializer = new XmlSerializer(typeof(U));

            string xmlResult = string.Empty;

            using (var sww = new Utf8StringWriter())
            {
                using (var writer = XmlWriter.Create(sww, new XmlWriterSettings { Encoding = new UTF8Encoding(false) }))
                {
                    xmlSerializer.Serialize(writer, data, null);
                    xmlResult = sww.ToString();
                }
            }

            return await ExecutePostAsync<T>(url,
                xmlResult,
                contentType ?? ContentTypeEnum.ApplicationXml,
                credentials,
                timeoutInMilliseconds);
        }

        private async Task<T> ExecuteDeleteAsync<T>(
           string url,
           string parameters,
           ContentTypeEnum contentType = ContentTypeEnum.ApplicationJson,
           Credentials credentials = null,
           int? timeoutInMilliseconds = null) where T : class
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                SetAuthorization(httpClient, credentials);

                SetTimeout(httpClient, timeoutInMilliseconds);

                var mediaType = GetMediaType(contentType);

                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(url),
                    Method = HttpMethod.Delete,
                    Content = new StringContent(parameters, Encoding.UTF8, mediaType),
                };

                var response = await httpClient.SendAsync(request);

                return await response.Content.ReadAsAsync<T>();
            }
        }

        private T ExecuteDelete<T>(
            string url,
            string parameters,
            ContentTypeEnum contentType = ContentTypeEnum.ApplicationJson,
            Credentials credentials = null,
            int? timeoutInMilliseconds = null)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                SetAuthorization(httpClient, credentials);

                SetTimeout(httpClient, timeoutInMilliseconds);

                var mediaType = GetMediaType(contentType);

                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(url),
                    Method = HttpMethod.Delete,
                    Content = new StringContent(parameters, Encoding.UTF8, mediaType),
                };

                var response = httpClient.SendAsync(request).RunSync();

                return response.Content.ReadAsAsync<T>().RunSync();
            }
        }

        private async Task<T> ExecutePostAsync<T>(string url, string parameters, ContentTypeEnum contentType,
           Credentials credentials, int? timeoutInMilliseconds = null)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                SetAuthorization(httpClient, credentials);

                SetTimeout(httpClient, timeoutInMilliseconds);

                var mediaType = GetMediaType(contentType);

                var content = new StringContent(parameters, Encoding.UTF8, mediaType);

                var responseMessage = await httpClient.PostAsync(url, content);

                return await ProcessResponseMessageAsync<T>(responseMessage, url, parameters, contentType);
            }
        }

        private async Task<T> ExecutePutAsync<T>(string url, string parameters, ContentTypeEnum contentType,
            Credentials credentials, int? timeoutInMilliseconds = null) where T : class
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                SetAuthorization(httpClient, credentials);

                SetTimeout(httpClient, timeoutInMilliseconds);

                var mediaType = GetMediaType(contentType);

                var content = new StringContent(parameters, Encoding.UTF8, mediaType);

                var responseMessage = await httpClient.PutAsync(url, content);

                return await ProcessResponseMessageAsync<T>(responseMessage, url, parameters, contentType);
            }
        }

        public async Task<string> ExecutePostAsStringAsync<TRequest>(string url, TRequest data,
            ContentTypeEnum contentType = ContentTypeEnum.ApplicationJson, 
            int? timeoutInMilliseconds = null, 
            Credentials credentials = null, 
            JsonSerializerSettings jsonSerializerSettings = null)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                SetAuthorization(httpClient, credentials);

                SetTimeout(httpClient, timeoutInMilliseconds);

                var jsonString = JsonConvert.SerializeObject(data, jsonSerializerSettings ?? _jsonSerializerSettings);

                var content = new StringContent(jsonString, Encoding.UTF8,
                    contentType == ContentTypeEnum.ApplicationJson ? "application/json" : "application/xml");

                var responseMessage = await httpClient.PostAsync(url, content);

                var res = await ProcessResponseMessageAsStringAsync(responseMessage, url, jsonString, contentType);

                return res;
            }
        }

        public async Task<T> ExecutePostAsync<T>(string url, string parameters, int? timeoutInMilliseconds = null) where T : class
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                SetTimeout(httpClient, timeoutInMilliseconds);

                var content = new StringContent(parameters, Encoding.UTF8, "application/json");

                var httpResponse = httpClient.PostAsync(url, content).Result;

                return await ProcessResponseMessageAsync<T>(httpResponse, url, parameters);
            }
        }

        public async Task<byte[]> ExecutePostWithByteArrayResponseAsync<TRequest>(string url, TRequest parameters, int? timeoutInMilliseconds = null, JsonSerializerSettings jsonSerializerSettings = null)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                SetTimeout(httpClient, timeoutInMilliseconds);

                var jsonSerialized = JsonConvert.SerializeObject(parameters, jsonSerializerSettings ?? _jsonSerializerSettings);

                var content = new StringContent(jsonSerialized, Encoding.UTF8, "application/json");

                var httpResponse = httpClient.PostAsync(url, content).Result;

                return await ProcessByteArrayResponseMessageAsync(httpResponse, url, jsonSerialized);
            }
        }

        public async Task<TResponse> ExecutePostAsync<TResponse, TRequest>(string url, TRequest request, KeyValuePair<string, string> authParameter, int? timeoutInMilliseconds = null, JsonSerializerSettings jsonSerializerSettings = null)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var requestJson = JsonConvert.SerializeObject(request, jsonSerializerSettings ?? _jsonSerializerSettings);

                var httpRequest = new HttpRequestMessage();
                httpRequest.Headers.Authorization = new AuthenticationHeaderValue(authParameter.Key, authParameter.Value);
                httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpRequest.Method = HttpMethod.Post;
                httpRequest.RequestUri = new Uri(url);
                httpRequest.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");

                var httpResponse = await httpClient.SendAsync(httpRequest);
                return await ProcessResponseMessageAsync<TResponse>(httpResponse, url, requestJson);
            }
        }

        public async Task<TResponse> ExecutePostAsync<TResponse>(string url, IEnumerable<KeyValuePair<string, string>> formContentRequest, KeyValuePair<string, string> authParameter, int? timeoutInMilliseconds = null)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var requestJson = JsonConvert.SerializeObject(formContentRequest, _jsonSerializerSettings);

                var httpRequest = new HttpRequestMessage();
                httpRequest.Headers.Authorization = new AuthenticationHeaderValue(authParameter.Key, authParameter.Value);
                httpRequest.Method = HttpMethod.Post;
                httpRequest.RequestUri = new Uri(url);
                httpRequest.Content = new FormUrlEncodedContent(formContentRequest);

                var httpResponse = await httpClient.SendAsync(httpRequest);
                return await ProcessResponseMessageAsync<TResponse>(httpResponse, url, requestJson);
            }
        }

        public string BuildUrl(string schemeAndHost, string urlPath, bool joinUrlAsIs = false)
        {
            var baseUri = new UriBuilder(new Uri(new Uri(schemeAndHost), urlPath));

            return new UriBuilder(scheme: baseUri.Scheme,
                                                host: baseUri.Host,
                                                port: baseUri.Port,
                                                path: baseUri.Path,
                                                extraValue: baseUri.Query).Uri.ToString();
        }

        #region private methods


        private string GenerateCredentials(string login, string password)
            => Convert.ToBase64String(Encoding.ASCII.GetBytes($"{login}:{password}"));

        private void SetAuthorization(HttpClient client, Credentials credentials)
        {
            if (credentials == null) return;

            if ((string.IsNullOrWhiteSpace(credentials.Login) || string.IsNullOrWhiteSpace(credentials.Password))
                 && string.IsNullOrWhiteSpace(credentials.Token))
                return;

            var token = string.IsNullOrEmpty(credentials.Token)
                ? GenerateCredentials(credentials.Login, credentials.Password)
                : credentials.Token;

            client.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue($"{credentials.Type}", token);

            if (credentials.AdditionalCredential != null)
            {
                client.DefaultRequestHeaders.Add(credentials.AdditionalCredential.HeaderKey, 
                    $"{credentials.AdditionalCredential.Type} " +
                    $"{credentials.AdditionalCredential.Token ?? GenerateCredentials(credentials.AdditionalCredential.Login, credentials.AdditionalCredential.Password)}");
            }
        }

        private void SetTimeout(HttpClient client, int? timeoutInMilliseconds)
        {
            if (timeoutInMilliseconds.HasValue)
            {
                client.Timeout = TimeSpan.FromMilliseconds(timeoutInMilliseconds.Value);
            }
        }

        private async Task<T> ProcessResponseMessageAsync<T>(HttpResponseMessage responseMessage, string url, string parameters)
        {
            if (responseMessage.StatusCode == HttpStatusCode.NoContent)
            {
                return default(T);
            }

            if (responseMessage.StatusCode != HttpStatusCode.OK)
            {
                var exception = new WebException($"Request({url}) fault with code = {responseMessage.StatusCode}, data: {parameters}");
                if (responseMessage.StatusCode == HttpStatusCode.InternalServerError)
                {
                    var jsonErrorString = await responseMessage.Content.ReadAsStringAsync();
                    exception.Data["ErrorMessage"] = jsonErrorString;
                }

                throw exception;
            }

            return await responseMessage.Content.ReadAsAsync<T>();
        }

        private async Task<byte[]> ProcessByteArrayResponseMessageAsync(HttpResponseMessage httpResponseMessage, string url, string parameters)
        {
            if (httpResponseMessage.StatusCode == HttpStatusCode.NoContent)
            {
                return new byte[0];
            }

            if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                var exception = new WebException($"Request({url}) fault with code = {httpResponseMessage.StatusCode}, parameters = {parameters}");
                if (httpResponseMessage.StatusCode == HttpStatusCode.InternalServerError)
                {
                    var byteError = await httpResponseMessage.Content.ReadAsByteArrayAsync();
                    exception.Data["ErrorMessage"] = byteError.ToString();
                }

                throw exception;
            }

            return await httpResponseMessage.Content.ReadAsByteArrayAsync();
        }

        private async Task<T> ProcessResponseMessageAsync<T>(HttpResponseMessage responseMessage, string url, string parameters, ContentTypeEnum contentType)
        {
            if (responseMessage.StatusCode == HttpStatusCode.NoContent)
            {
                return default(T);
            }

            T result;
            var response = responseMessage;
            var responseString = await response.Content?.ReadAsStringAsync();

            if (responseMessage.StatusCode != HttpStatusCode.OK && responseMessage.StatusCode != HttpStatusCode.Created)
            {
                var exception = new CustomWebException($"Request({url}) fault with code = {responseMessage.StatusCode}, data: {parameters}", responseMessage.StatusCode, responseString);
                if (responseMessage.StatusCode == HttpStatusCode.InternalServerError)
                {
                    exception.Data["ErrorMessage"] = responseString;
                }

                throw exception;
            }

            if (contentType == ContentTypeEnum.ApplicationJson)
            {
                result = JsonConvert.DeserializeObject<T>(responseString, _jsonSerializerSettings);
            }
            else
            {
                using (var stream = new MemoryStream(StringToUTF8ByteArray(responseString)))
                {
                    result = (T)new XmlSerializer(typeof(T)).Deserialize(stream);
                }

            }

            return result;
        }

        private async Task<string> ProcessResponseMessageAsStringAsync(HttpResponseMessage responseMessage, string url, string parameters, ContentTypeEnum contentType)
        {
            if (responseMessage.StatusCode == HttpStatusCode.NoContent)
            {
                return default(string);
            }

            var responseString = await responseMessage.Content.ReadAsStringAsync();

            if (responseMessage.StatusCode != HttpStatusCode.OK)
            {
                var exception = new CustomWebException($"Request({url}) fault with code = {responseMessage.StatusCode}, data: {parameters}, responseString: {responseString}", responseMessage.StatusCode, responseString);
                if (responseMessage.StatusCode == HttpStatusCode.InternalServerError)
                {
                    exception.Data["ErrorMessage"] = responseString;
                }
                throw exception;
            }

            return responseString;
        }

        private string GetMediaType(ContentTypeEnum contentType)
        {
            switch (contentType)
            {
                case ContentTypeEnum.ApplicationJson:
                    return "application/json";
                case ContentTypeEnum.ApplicationXml:
                    return "application/xml";
                case ContentTypeEnum.TextJson:
                    return "text/json";
                case ContentTypeEnum.TextXml:
                    return "text/xml";
                default:
                    throw new NotSupportedException($"{contentType} is not supported");
            }
        }

        private byte[] StringToUTF8ByteArray(string pXmlString)
        {
            var encoding = new UTF8Encoding();
            byte[] byteArray = encoding.GetBytes(pXmlString);
            return byteArray;
        }
        
        private class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding => Encoding.UTF8;
        }

        #endregion
    }
}
