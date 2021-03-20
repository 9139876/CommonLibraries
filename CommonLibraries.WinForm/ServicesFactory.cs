using System;
using CommonLibraries.Core.Extensions;
using CommonLibraries.RemoteCall.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace CommonLibraries.ClientApplication
{
    public static class ServicesFactory
    {
        private static readonly ServiceCollection _services;

        private static IServiceProvider _serviceProvider;

        static ServicesFactory()
        {
            _services = new ServiceCollection();
            _services.RegisterRemoteCall();
        }

        public static void RegisterAssemblyServiceAndRepositoryByMember<T>()
        {
            _services.RegisterAssemblyServiceAndRepositoryByMember<T>();
        }

        public static void AddTransientService<TInterface, TClass>()
            where TInterface : class
            where TClass : class, TInterface
        {
            _services.AddTransient<TInterface, TClass>();
        }

        public static void BuildServiceProvider()
        {
            _serviceProvider = _services.BuildServiceProvider();
        }

        public static T GetInstance<T>()
        {
            if (_serviceProvider == null)
            {
                BuildServiceProvider();
            }

            return (T)_serviceProvider.GetService(typeof(T));
        }
    }
}
