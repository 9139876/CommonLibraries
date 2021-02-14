using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CommonLibraries.Core.Extensions
{
    public static class CommonExtensions
    {
        private static JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            Converters = new List<JsonConverter> { new StringEnumConverter() },
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore
        };

        private static string Compose(string paramName, object @object)
        {
            return @object == null ? paramName : $"{paramName} for object:{JsonConvert.SerializeObject(@object)}";
        }

        public static void RequiredNotNull(this object value, string paramName, object @object = null)
        {
            if (value == null)
                throw new InvalidOperationException(Compose(paramName, @object));
        }

        public static void RequiredNotNull(this string value, string paramName, object @object = null)
        {
            if (string.IsNullOrEmpty(value))
                throw new InvalidOperationException(Compose(paramName, @object));

            if (value.Trim() == "")
                throw new InvalidOperationException(Compose(paramName, @object));
        }

        public static string Serialize(this object value)
        {
            return JsonConvert.SerializeObject(value, JsonSerializerSettings);
        }

        public static T Deserialize<T>(this string value)
        {
            return JsonConvert.DeserializeObject<T>(value, JsonSerializerSettings);
        }
    }
}
