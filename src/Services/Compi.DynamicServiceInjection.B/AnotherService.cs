using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi.DynamicServiceInjection.B
{
    public class AnotherService
    {
        public string Name { get; set; }



        private int _age = 40;

        public int Age 
        { 
            get => _age;
            
            set
            {
                _age=value;
            }
        }




        public AnotherService()
        {

        }

        protected AnotherService(string name)
        {
            Name = name;

            Console.WriteLine(name);
        }



        public void WriteName()
        {
            Console.WriteLine(Name);
        }


        protected void DoSomething(string thingy)
        {
            Console.WriteLine(thingy);
        }
    }
}
