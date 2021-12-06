using System;
using CommonLibraries.ClientApplication.Internal;
using Microsoft.Extensions.Configuration;

namespace CommonLibraries.ClientApplication
{
    public abstract class ClientApplicationStartup
    {
        protected abstract void ConfigureServices();

        public void UseStartup()
        {
            Configuration.StaticConfiguration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("settings.json")
                .Build();

            ServicesFactory.AddTransientService<IConfiguration, Configuration>();
            ConfigureServices();
        }
    }
}
