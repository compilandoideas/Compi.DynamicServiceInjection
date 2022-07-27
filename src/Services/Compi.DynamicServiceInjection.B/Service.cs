using Compi.DynamicServiceInjection.Common;

namespace Compi.DynamicServiceInjection.B
{
    public class Service : IService
    {

        public Service()
        {

        }


        public void Execute()
        {
            Console.WriteLine("Service B");
        }

    }
}