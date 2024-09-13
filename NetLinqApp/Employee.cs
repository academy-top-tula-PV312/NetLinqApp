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
        public string? Email { get; set; }
        public Company Company { get; set; }

        public List<string> Langs { get; set; } = new();

        public Employee()
        {
            Id = ++globalId;
        }

        public override string ToString()
        {
            //string langs = "";
            //foreach (var l in Langs)
            //    langs += l + " ";

            return $"[{Id}] Name: {Name}, Age: {Age}, Company: {Company.Title}";
        }
    }
}
