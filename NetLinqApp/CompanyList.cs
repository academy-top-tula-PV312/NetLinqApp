using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetLinqApp
{
    public class CompanyList
    {
        public string? Title { get; set; }
        public List<Employee> Employees { get; set; } = new();
    }
}
