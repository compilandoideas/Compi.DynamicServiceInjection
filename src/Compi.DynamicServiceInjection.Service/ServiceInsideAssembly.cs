using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi.DynamicServiceInjection.Service
{
    public class ServiceInsideAssembly
    {
        public string Name { get; set; }
        public ServiceInsideAssembly(string name)
        {
            Name = name;    
        }

        public void WriteMessage(string message)
        {
            Console.WriteLine(message);
        }

    }
}
