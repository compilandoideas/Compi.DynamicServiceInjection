using Compi.DynamicServiceInjection.Common;
using Compi.DynamicServiceInjection.Service.DataContracts;
using Compi.DynamicServiceInjection.Service.Extensions;

using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

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


					// services.AddHostedService<Worker>();

					// services.AddService(hostContext.Configuration);
					// services.AddServiceWithScrutor(hostContext.Configuration);
					Tests(hostContext);

				})

				.Build();

			host.Run();
		}






		private static void Tests(HostBuilderContext hostContext)
		{
			// Test01(hostContext);

			// Test02();
			// Test03();

			//Test04();

			Test05();
		}



		private static JsonSerializerOptions GetSerializationOptions()
		{
			var encoderSettings = new TextEncoderSettings();

			encoderSettings.AllowRange(UnicodeRanges.All);
			var options = new JsonSerializerOptions
			{
				Encoder = JavaScriptEncoder.Create(encoderSettings)
			};
			return options;
		}



		private static void Test05()
		{

			var options = GetSerializationOptions();

			try
			{
				//var json = @"{
				//                        ""Events"":[
				//                        {
				//                            ""Id"" : 1,
				//                            ""TypeEvent"" : ""qNñwe""
				//                        },
				//                        {
				//                            ""Id"" : 2,
				//                            ""TypeEvent"" : ""aqwe""
				//                        }
				//                    ]}"
				//;



				var json = @"
					
							""data"" : [
								{
									""Id"" : 1,
									""TypeEvent"" : ""qNñwe""
								},
								{
									""Id"" : 2,
									""TypeEvent"" : ""aqwe""
								}
							]							
						";



				Type? type = Assembly.GetAssembly(typeof(DataResult))?.GetType("Compi.DynamicServiceInjection.Service.DataContracts.DataResult");

				var obj = new DataResult();
				var type2 = obj.GetType().FullName;

			

				var deserializedObject = JsonSerializer.Deserialize(json, type, options);


				Console.WriteLine(JsonSerializer.Serialize(deserializedObject, new JsonSerializerOptions { WriteIndented = true }));
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}

		private static void Test04()
		{
			var iocContainer = new IoCContainer();
			iocContainer.Register<IWaterService, TapWaterService>();
			var waterService = iocContainer.Resolve<IWaterService>();

			//iocContainer.Register<IBeanService<Catimor>, ArabicaBeanService<Catimor>>();
			//iocContainer.Register<IBeanService<>, ArabicaBeanService<>>();
			//iocContainer.Register<typeof(IBeanService<>), typeof(ArabicaBeanService<>)>();
			iocContainer.Register(typeof(IBeanService<>), typeof(ArabicaBeanService<>));

			iocContainer.Register<ICoffeeService, CoffeeService>();
			var coffeeService = iocContainer.Resolve<ICoffeeService>();
		}

		private static void Test03()
		{
			var ServiceInsideAssemblyType = Type.GetType("Compi.DynamicServiceInjection.Service.ServiceInsideAssembly");
			var myList = new List<ServiceInsideAssembly>();

			//Console.Write(ServiceInsideAssemblyType?.GetType().Name);
			Console.WriteLine(myList?.GetType().Name);


			var myDicionary = new Dictionary<string, int>();
			var myDictionaryType = myDicionary.GetType();

			foreach (var genericTypeArgument in myDictionaryType.GenericTypeArguments)
			{
				Console.WriteLine(genericTypeArgument);
			}

			foreach (var genericArgument in myDictionaryType.GetGenericArguments())
			{
				Console.WriteLine(genericArgument);
			}



			var myGenericDictionaryType = typeof(Dictionary<,>);


			foreach (var genericTypeArgument in myGenericDictionaryType.GenericTypeArguments)
			{
				Console.WriteLine(genericTypeArgument);
			}

			foreach (var genericArgument in myGenericDictionaryType.GetGenericArguments())
			{
				Console.WriteLine(genericArgument);
			}



			var projectInstance = Activator.CreateInstance(typeof(Project), new object[] { "Proyecto 1", "Descripción" });
			Console.WriteLine(projectInstance?.GetType());


			//var projectInstanceResult = Activator.CreateInstance(typeof(Result<>));
			//Console.Write(projectInstanceResult?.GetType());//error

			//var openResultType = typeof(Result<>);
			//var closedResultType = openResultType.MakeGenericType(typeof(Project));
			//var InstanceClosedResult = Activator.CreateInstance(closedResultType);
			//Console.WriteLine(InstanceClosedResult?.GetType());

			//var type21 = typeof(Project);

			//Console.WriteLine(type21.GetType());
			//var type22 = Type.GetType("Compi.DynamicServiceInjection.Service.Project");
			//Console.WriteLine(type22.GetType());


			var openResultType = Type.GetType("Compi.DynamicServiceInjection.Service.Result`1");
			var closedResultType = openResultType?.MakeGenericType(Type.GetType("Compi.DynamicServiceInjection.Service.Project"));
			var InstanceClosedResult = Activator.CreateInstance(closedResultType);
			Console.Write(InstanceClosedResult?.GetType());


			var methodInfo = closedResultType.GetMethod("AlterAndReturnValue");
			Console.WriteLine(methodInfo);


			var genericMethodInfo = methodInfo?.MakeGenericMethod(typeof(Project));
			genericMethodInfo?.Invoke(InstanceClosedResult, new object[] { new Project() });


		}

		private static void Test02()
		{
			var assembly = Assembly.Load("Compi.DynamicServiceInjection.B");
			var anotherServiceType = assembly.GetType("Compi.DynamicServiceInjection.B.AnotherService");
			var anotherServiceConstructors = anotherServiceType?.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

			foreach (var ctor in anotherServiceConstructors)
			{
				Console.WriteLine(ctor);
			}

			var privateConstructor = anotherServiceType?
				.GetConstructor(
					BindingFlags.Instance | BindingFlags.NonPublic,
					null,
					new Type[] { typeof(string) },
					null
				);

			privateConstructor?.Invoke(new object[] { "Carlos" });




			var anotherService = Activator.CreateInstance(
			   "Compi.DynamicServiceInjection.B",
			   "Compi.DynamicServiceInjection.B.AnotherService",
			   true,
			   BindingFlags.Instance | BindingFlags.NonPublic,
			   null,
			   new object[] { "Eduardo" },
			   null,
			   null)?.Unwrap();

			var nameProperty = anotherService?.GetType().GetProperty("Name");

			var name = nameProperty?.GetValue(anotherService, null);
			nameProperty?.SetValue(anotherService, "Pedro");

			var age = anotherService?.GetType().GetField("_age", BindingFlags.Instance | BindingFlags.NonPublic);
			age?.SetValue(anotherService, 50);


			anotherService?.GetType()
				.InvokeMember(
				"Age",
				BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty,
				null,
				anotherService,
				new object[] { 100 });



			//Methods

			var WriteNameMethod = anotherService?.GetType().GetMethod("WriteName");

			WriteNameMethod?.Invoke(anotherService, null);

			anotherService?.GetType().InvokeMember(
				"DoSomething",
				BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod,
				null,
				anotherService,
				new object[] { "thingy" }
				);



			Console.Write(anotherService);
		}

		private static void Test01(HostBuilderContext hostContext)
		{
			//var assemblyFound = Directory.GetFiles(
			//    System.AppDomain.CurrentDomain.BaseDirectory,
			//    "Compi.DynamicServiceInjection.B.dll",
			//    SearchOption.AllDirectories).First();

			//var assembly = Assembly.LoadFrom(assemblyFound);

			var serviceNamespace = hostContext.Configuration.GetSection("ServiceNamespace").Value;

			var assembly = Assembly.Load(serviceNamespace);//extternal assembly

			var typeFromConfigurationFile = assembly.GetType("Compi.DynamicServiceInjection.A.Service");

			var serviceWithType = Activator.CreateInstance(typeFromConfigurationFile) as IService;
			serviceWithType?.Execute();

			dynamic serviceIntance = Activator.CreateInstance(typeFromConfigurationFile);
			serviceIntance?.Execute();


			var service = Activator.CreateInstance(
				"Compi.DynamicServiceInjection.B",
				"Compi.DynamicServiceInjection.B.Service",
				true,
				BindingFlags.Instance | BindingFlags.Public,
				null,
				null,
				null,
				null)?.Unwrap();

		}
	}

}