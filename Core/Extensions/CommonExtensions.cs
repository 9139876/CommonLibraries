using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CommonLibraries.Core.Extensions
{
    public static class CommonExtensions
    {
        private static readonly JsonSerializerSettings _jsonSerializerDefaultSettings = new JsonSerializerSettings
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
            var jsonSerializerSettings = _jsonSerializerDefaultSettings;
            if(IfContainAbstractMembers(value.GetType()))
            {
                jsonSerializerSettings.TypeNameHandling = TypeNameHandling.All;
            }

            return JsonConvert.SerializeObject(value, jsonSerializerSettings);
        }

        public static T Deserialize<T>(this string value)
        {
            var jsonSerializerSettings = _jsonSerializerDefaultSettings;
            if (IfContainAbstractMembers(value.GetType()))
            {
                jsonSerializerSettings.TypeNameHandling = TypeNameHandling.All;
            }

            return JsonConvert.DeserializeObject<T>(value, jsonSerializerSettings);
        }

        private static bool IfContainAbstractMembers(Type type)
        {
            foreach (var property in type.GetProperties())
            {
                var propertyType = property.PropertyType;

                if (propertyType.IsAbstract || propertyType.IsInterface)
                {
                    return true;
                }

                if (IfContainAbstractMembers(propertyType))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
