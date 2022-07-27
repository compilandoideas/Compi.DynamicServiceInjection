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

                    services.AddService(hostContext.Configuration);


                    //var serviceNamespace = hostContext.Configuration.GetSection("ServiceNamespace").Value;

                    //var assembly = Assembly.Load(serviceNamespace);

                    //var typeFromConfigurationFile = assembly.GetType("Compi.DynamicServiceInjection.A.Service");

                    //var serviceWithType = Activator.CreateInstance(typeFromConfigurationFile) as IService;
                    //serviceWithType.Execute();

                    //dynamic serviceIntance = Activator.CreateInstance(typeFromConfigurationFile);
                    //serviceIntance.Execute();




                    //services.Scan(scan =>
                    //              scan
                    //              .FromAssemblies(assembly)
                    //              .AddClasses()
                    //              .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    //              .AsSelfWithInterfaces()         
                    //              .WithSingletonLifetime()
                    //          ) ;


                })

                .Build();

            host.Run();
        }

    }
      
}