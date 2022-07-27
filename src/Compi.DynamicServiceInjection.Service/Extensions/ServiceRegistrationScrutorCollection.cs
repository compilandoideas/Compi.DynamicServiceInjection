using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scrutor;
using System.Reflection;

namespace Compi.DynamicServiceInjection.Service.Extensions
{
    public static class ServiceRegistrationScrutorCollection
    {
        public static void AddServiceWithScrutor(this IServiceCollection services, IConfiguration configuration)
        {

            var serviceNamespace = configuration.GetSection("ServiceNamespace").Value;

            var assembly = Assembly.Load(serviceNamespace);

            services.Scan(scan =>
                          scan
                          .FromAssemblies(assembly)
                          .AddClasses()
                          .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                          .AsSelfWithInterfaces()
                          .WithSingletonLifetime()                          
                      );

        }
    }
}
