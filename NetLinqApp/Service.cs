using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetLinqApp
{
    public static class ServiceApp
    {
        public static List<Employee> Init()
        {
            return new List<Employee>()
            {
                new(){ Name = "Bobby", Age = 25 },
                new(){ Name = "Jimmy", Age = 31 },
                new(){ Name = "Tommy", Age = 19 },
            };
        }
    }
}
