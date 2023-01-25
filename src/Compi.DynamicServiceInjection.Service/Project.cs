using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi.DynamicServiceInjection.Service
{
    public class Project
    {
        public string? Name { get; set; }

        public string? Description { get; set; }



        public Project()
        {

        }

        public Project(string name, string description)
        {
            Name = name;
            Description = description;
        }



        public void ChangeProjectName(string newName)
        {
            Name=newName;
        }
    }
}
