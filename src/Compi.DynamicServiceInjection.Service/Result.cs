using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi.DynamicServiceInjection.Service
{
    public class Result<T>
    {
        public T Value { get; set; }

        public string Message { get; set; }

        public T AlterAndReturnValue<S>(S input)
        {
            // dummy code...
            Console.WriteLine($"Altering value using {input.GetType()}");
            return Value;
        }

    }
}
