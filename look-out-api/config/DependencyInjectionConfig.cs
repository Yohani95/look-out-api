using System;
using System.Linq;
using System.Reflection;
using look.Application.interfaces;
using look.domain.interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace YourNamespace.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            var appAssembly = Assembly.GetAssembly(typeof(IService<>));
            RegisterInterfaces(services, appAssembly, IsServiceInterface);

            var appAssemblyRepository= Assembly.Load("look.Infrastructure");
            RegisterInterfaces(services, appAssemblyRepository, IsRepositoryInterface);
        }

        private static void RegisterInterfaces(IServiceCollection services, Assembly assembly, Func<Type, bool> interfaceFilter)
        {
            var implementationTypes = assembly.GetTypes()
                .Where(type => !type.IsAbstract && !type.IsInterface)
                .Where(type => type.GetInterfaces().Any(interfaceFilter));

            foreach (var implementationType in implementationTypes)
            {
                var interfaceTypes = implementationType.GetInterfaces()
                    .Where(interfaceFilter);

                foreach (var interfaceType in interfaceTypes)
                {
                    services.AddScoped(interfaceType, implementationType);
                }
            }
        }

        private static bool IsServiceInterface(Type type)
        {
            return type.IsInterface && type.Name.EndsWith("Service");
        }

        private static bool IsRepositoryInterface(Type type)
        {
            return type.IsInterface && type.Name.EndsWith("Repository");
        }
    }
}
