using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibraries.ClientApplication.Internal;
using Microsoft.Extensions.Configuration;

namespace CommonLibraries.ClientApplication
{
    public abstract class ClientApplicationStartup
    {
        //protected ClientApplicationStartup()
        //{
            
        //}

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
