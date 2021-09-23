using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace CommonLibraries.Config.Internal
{
    internal static class WebRequestUtils
    {
        private static HttpClient HttpClient { get; set; } = new HttpClient();

        private static readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            Converters = new List<JsonConverter> { new StringEnumConverter() },
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public static TResponse ExecutePost<TResponse, TRequest>(string url, TRequest data, int? timeout = null)
        {
            var jsonString = JsonConvert.SerializeObject(data, _jsonSerializerSettings);

            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = HttpClient.PostAsync(url, content).RunSync();

            return ProcessResponseMessage<TResponse>(response, url, JsonConvert.SerializeObject(data, _jsonSerializerSettings));
        }

        private static TResponse ProcessResponseMessage<TResponse>(HttpResponseMessage responseMessage, string url, string parameters)
        {
            if (responseMessage.StatusCode == HttpStatusCode.NoContent)
            {
                return default(TResponse);
            }

            var responseString = responseMessage.Content.ReadAsStringAsync().RunSync();

            if (responseMessage.StatusCode != HttpStatusCode.OK)
            {
                var exception = new InvalidOperationException($"Request({url}) fault with code = {responseMessage.StatusCode}, data: {parameters}");
                if (responseMessage.StatusCode == HttpStatusCode.InternalServerError)
                {
                    exception.Data["ErrorMessage"] = responseString;
                }

                throw exception;
            }

            var ret = JsonConvert.DeserializeObject<TResponse>(responseString, _jsonSerializerSettings);

            if (ret == null)
            {
                var exception = new InvalidOperationException($"Request({url}) can't convert result = {responseString}, by request: {url} and parameters: {parameters}");

                throw exception;
            }

            return ret;
        }
    }
}
