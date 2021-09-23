using System;
using System.Collections.Generic;
using System.Reflection;
using CommonLibraries.Config.Enums;
using CommonLibraries.Config.Internal;
using CommonLibraries.Config.Models;
using CommonLibraries.Core.Extensions;
using Microsoft.Extensions.Configuration;

namespace CommonLibraries.Config
{
    public static class ConfigExtensions
    {
        private static readonly string _configServiceUrl = "http://configservice.api/api/get-config";

        public static IConfigurationBuilder LoadConfiguration(this IConfigurationBuilder builder, bool loadFromConfigService, bool reloadAppSettingsOnChange, bool requiredConfigService)
        {
            var configurationBuilder = builder as ConfigurationBuilder ?? throw new InvalidCastException("IConfigurationBuilder builder is not a ConfigurationBuilder");

            var environment = GetEnvironment();

            var configs = new Dictionary<string, string>() { { "EnvironmentLocation", environment.ToString() } };

            if (loadFromConfigService)
            {
                try
                {
                    var configRequest = new GetConfigRequest()
                    {
                        Application = Assembly.GetEntryAssembly().GetName().Name,
                        Environment = environment
                    };

                    var configResponse = GetConfigRaw(configRequest);

                    var configRows = configResponse?.ConfigRows ?? new List<ConfigRow>();

                    foreach (var row in configRows)
                    {
                        var key = row.ParentKey != null ? $"{row.ParentKey}:{row.Key}" : row.Key;
                        var value = row.Value;

                        configs.Add(key, value);
                    }
                }
                catch (Exception)
                {
                    if (requiredConfigService)
                    {
                        throw;
                    }
                    else
                    {
                        configs = new Dictionary<string, string>() { { "EnvironmentLocation", environment.ToString() } };
                    }
                }
            }

            configurationBuilder.AddInMemoryCollection(configs);

            configurationBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: reloadAppSettingsOnChange);

            return configurationBuilder;
        }

        private static EnvironmentEnum GetEnvironment()
        {
            var envVariable = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            switch (envVariable)
            {
                case "RemoteServer": return EnvironmentEnum.RemoteServer;

                default: return EnvironmentEnum.Local;
            }
        }

        private static GetConfigResponse GetConfigRaw(GetConfigRequest request)
        {
            return WebRequestUtils.ExecutePost<GetConfigResponse, GetConfigRequest>(url: _configServiceUrl, data: request);
        }
    }
}
