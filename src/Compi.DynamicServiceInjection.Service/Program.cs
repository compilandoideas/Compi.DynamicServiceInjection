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

                    var typeFromConfigurationFile = assembly.GetType("Compi.DynamicServiceInjection.A.Service");

                    //var serviceWithType = Activator.CreateInstance(typeFromConfigurationFile) as IService;
                    //serviceWithType.Execute();

                    //dynamic serviceIntance = Activator.CreateInstance(typeFromConfigurationFile);
                    //serviceIntance.Execute();



                    List<ConstructorInfo> ctors = assembly
                        .GetTypes()
                        .Select(x =>
                        {
                            Type type = x.IsGenericType ? x.MakeGenericType(typeof(IService).GenericTypeArguments) : x;
                            return type.GetConstructors().Single();
                        })
                        .ToList();

                        Func<IServiceProvider, object> func = provider =>
                        {
                            object current = null;

                            foreach (ConstructorInfo ctor in ctors)
                            {
                                List<ParameterInfo> parameterInfos = ctor.GetParameters().ToList();

                                object[] parameters = GetParameters(parameterInfos, current, provider);

                                current = ctor.Invoke(parameters);
                            }

                            return current;
                        };


                    services.AddSingleton(typeof(IService), func);

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


        private static object[] GetParameters(List<ParameterInfo> parameterInfos, object current, IServiceProvider provider)
        {
            var result = new object[parameterInfos.Count];

            for (int i = 0; i < parameterInfos.Count; i++)
            {
                result[i] = GetParameter(parameterInfos[i], current, provider);
            }

            return result;
        }

        private static object GetParameter(ParameterInfo parameterInfo, object current, IServiceProvider provider)
        {
            Type parameterType = parameterInfo.ParameterType;

            if (IsHandlerInterface(parameterType))
                return current;

            object service = provider.GetService(parameterType);
            if (service != null)
                return service;

            throw new ArgumentException($"Type {parameterType} not found");
        }


        private static bool IsHandlerInterface(Type type)
        {
            if (!type.IsGenericType)
                return false;

            Type typeDefinition = type.GetGenericTypeDefinition();

            return typeDefinition == typeof(IService);
        }
    }
}