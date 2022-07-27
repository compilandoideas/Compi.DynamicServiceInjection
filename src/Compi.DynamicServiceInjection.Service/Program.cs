using Compi.DynamicServiceInjection.Common;
using Scrutor;
using System.Reflection;

namespace Compi.DynamicServiceInjection.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, configurationBuilder) =>
                {

                    hostContext.HostingEnvironment.ApplicationName = "Service Dynamic Injection Service";

                    configurationBuilder.Sources.Clear();

                    var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                    configurationBuilder
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                    .AddEnvironmentVariables(prefix: "Config_")
                    .AddCommandLine(args)
                    .Build();



                })
                .ConfigureServices((hostContext, services) =>
                {
                   
                  
                    services.AddHostedService<Worker>();


                    var serviceNamespace = hostContext.Configuration.GetSection("ServiceNamespace").Value;
                  
                    var assembly = Assembly.Load(serviceNamespace);


                    services.Scan(scan =>
                                  scan
                                  .FromAssemblies(assembly)
                                  .AddClasses()
                                  .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                                  .AsSelfWithInterfaces()         
                                  .WithSingletonLifetime()
                              ) ;


                })

                .Build();

            host.Run();
        }
    }
}