using Compi.DynamicServiceInjection.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Compi.DynamicServiceInjection.Service
{
    public static class ServiceRegistration
    {
        public static void AddService(this IServiceCollection services, IConfiguration configuration)
        {

            var serviceNamespace = configuration.GetSection("ServiceNamespace").Value;

            var assembly = Assembly.Load(serviceNamespace);


            ConstructorInfo ctor = assembly
                .GetTypes()
                .Select(x =>
                {
                    Type type = x.IsGenericType ? x.MakeGenericType(typeof(IService).GenericTypeArguments) : x;
                    return type.GetConstructors().Single();
                })
                .First();

            Func<IServiceProvider, object> func = provider =>
            {
                object current = null;

                List<ParameterInfo> parameterInfos = ctor.GetParameters().ToList();

                object[] parameters = GetParameters(parameterInfos, current, provider);

                current = ctor.Invoke(parameters);


                return current;
            };


            services.AddSingleton(typeof(IService), func);

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
