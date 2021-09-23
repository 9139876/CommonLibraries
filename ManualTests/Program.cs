using CommonLibraries.Config;
using Microsoft.Extensions.Configuration;
using System;

namespace ManualTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var Configuration = new ConfigurationBuilder()
                .LoadConfiguration(loadFromConfigService: true, reloadAppSettingsOnChange: true, requiredConfigService: true)
                .Build();

            Console.WriteLine("Hello World!");
        }
    }
}
