using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetLinqApp
{
    public class Employee
    {
        static int globalId = 0;

        public int Id { get; }
        public string? Name { get; set; }
        public int Age { get; set; }

        public Employee()
        {
            Id = ++globalId;
        }
    }
}
