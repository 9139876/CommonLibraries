using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommonLibraries.Core.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace CommonLibraries.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static readonly List<string> ServiceAndRepositoryPostfix = new List<string> { "Service", "Repository" };

        public static void RegisterClasses(this IServiceCollection services, Assembly assembly, List<string> classesPostfix, ServiceLifetime serviceLifetime, bool optional)
        {
            var implementTypes = new List<Type>();

            foreach (var assemblyDefinedType in assembly.GetTypes())
            {
                if (classesPostfix.Any(x => assemblyDefinedType.Name.EndsWith(x)))
                {
                    var attribute = assemblyDefinedType.GetCustomAttributes(typeof(IgnoreRegistrationAttribute), false).FirstOrDefault();

                    if (attribute != null)
                        continue;

                    if (assemblyDefinedType.IsClass == false)
                        continue;

                    implementTypes.Add(assemblyDefinedType);
                }
            }

            foreach (var implementType in implementTypes)
            {
                var interfaceName = "I" + implementType.Name;
                var interfaceType = implementType.GetInterface(interfaceName);

                if (interfaceType == null && optional == false)
                    throw new InvalidOperationException($"Can't find interface = {interfaceName} for class.Name = {implementType.Name}, class.FullName = {implementType.FullName}. Add 'IgnoreRegistrationAttribute' if need ");

                services.Add(new ServiceDescriptor(interfaceType, implementType,
                        serviceLifetime));
            }
        }

        public static void RegisterCurrentAssemblyClasses(this IServiceCollection services, List<string> classesPostfix, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped, bool optional = false)
        {
            RegisterClasses(services: services,
                assembly: Assembly.GetExecutingAssembly(),
                classesPostfix: classesPostfix,
                serviceLifetime: serviceLifetime,
                optional: optional);
        }

        public static void RegisterCurrentAssemblyServiceAndRepository(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped, bool optional = false)
        {
            RegisterCurrentAssemblyClasses(services: services,
                classesPostfix: ServiceAndRepositoryPostfix,
                serviceLifetime: serviceLifetime,
                optional: optional);
        }

        public static void RegisterAssemblyClassesByMember<T>(this IServiceCollection services, List<string> classesPostfix, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped, bool optional = false)
        {
            RegisterClasses(services: services,
                assembly: typeof(T).Assembly,
                classesPostfix: classesPostfix,
                serviceLifetime: serviceLifetime,
                optional: optional);
        }

        public static void RegisterAssemblyClasses(this IServiceCollection services, string assemblyName, List<string> classesPostfix, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped, bool optional = false)
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(x => x.GetName().Name == assemblyName);

            if (assembly == null)
                throw new InvalidOperationException($"Can't find assembly by Name = {assemblyName}");

            RegisterClasses(services: services,
                assembly: assembly,
                classesPostfix: classesPostfix,
                serviceLifetime: serviceLifetime,
                optional: optional);
        }

        public static void RegisterAssemblyServiceAndRepositoryByMember<T>(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped, bool optional = false)
        {
            RegisterClasses(services: services,
                assembly: typeof(T).Assembly,
                classesPostfix: ServiceAndRepositoryPostfix,
                serviceLifetime: serviceLifetime,
                optional: optional);
        }

        public static void RegisterAssemblyServiceAndRepository(this IServiceCollection services, string assemblyName, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped, bool optional = false)
        {
            RegisterAssemblyClasses(services: services,
                assemblyName: assemblyName,
                classesPostfix: ServiceAndRepositoryPostfix,
                serviceLifetime: serviceLifetime,
                optional: optional);
        }
    }
}
