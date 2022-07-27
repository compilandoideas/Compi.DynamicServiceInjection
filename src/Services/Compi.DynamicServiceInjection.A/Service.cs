using Compi.DynamicServiceInjection.Common;

namespace Compi.DynamicServiceInjection.A
{
    public class Service : IService
    {

        public Service()
        {

        }


        public void Execute()
        {
            Console.WriteLine("Service A");
        }

    }
}