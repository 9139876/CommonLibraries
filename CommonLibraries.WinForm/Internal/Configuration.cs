using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace CommonLibraries.ClientApplication.Internal
{
    internal class Configuration : IConfiguration
    {
        public static IConfiguration StaticConfiguration { get; set; }

        public IConfigurationSection GetSection(string key) => StaticConfiguration.GetSection(key);

        public IEnumerable<IConfigurationSection> GetChildren() => StaticConfiguration.GetChildren();

        public IChangeToken GetReloadToken() => StaticConfiguration.GetReloadToken();

        public string this[string key]
        {
            get => StaticConfiguration[key];
            set => StaticConfiguration[key] = value;
        }
    }
}
