using System;
using System.Collections.Generic;
using System.Diagnostics;
using CommonLibraries.Config.Enums;
using CommonLibraries.Config.Models;
using CommonLibraries.Core.Extensions;
using Microsoft.Extensions.Configuration;

namespace CommonLibraries.Config
{
    public static class ConfigExtensions
    {
        private static bool isDebugMode = false;

        public static IConfigurationBuilder LoadConfiguration(this IConfigurationBuilder builder, bool reloadAppSettingsOnChange = true)
        {
            var configurationBuilder = builder as ConfigurationBuilder ?? throw new InvalidCastException("IConfigurationBuilder builder is not a ConfigurationBuilder");

            CheckIsDebugMode();

            var environment = new EnvironmentConfigParameters() {Environment = isDebugMode ? EnvironmentEnum.Debug : EnvironmentEnum.Release };

            configurationBuilder.AddInMemoryCollection(new Dictionary<string, string>
            {
                {
                    "EnvironmentConfigParameters", environment.Serialize()
                }
            });

            configurationBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: reloadAppSettingsOnChange);

            return configurationBuilder;
        }

        [Conditional("DEBUG")]
        private static void CheckIsDebugMode()
        {
            isDebugMode = true;
        }
    }
}
