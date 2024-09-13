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
            List<Company> list = new List<Company>()
            {
                new(){ Title = "Yandex" },
                new(){ Title = "Ozon" },
                new(){ Title = "Mail" },
                new(){ Title = "Avito" },
            };

            return new List<Employee>()
            {
                new(){ Name = "Bobby", Age = 25, Email = "bobby@mail.ru", Langs = { "C++", "C#" }, Company = list[1] },
                new(){ Name = "Jimmy", Age = 31, Email = "jimmy@yandex.ru", Langs = { "Java", "C++" }, Company = list[3] },
                new(){ Name = "Tommy", Age = 19, Email = "tommy@gmail.com", Langs = { "Pascal", "Go", "C#" }, Company = list[1] },
                new(){ Name = "Sammy", Age = 26, Email = "sammy@mail.ru", Langs = { "JavaScript", "PHP", "Java" }, Company = list[2] },
                new(){ Name = "Denny", Age = 18, Email = "denny@yandex.ru", Langs = { "Kotlin", "Swift", "C#" }, Company = list[0] },
                new(){ Name = "Poppy", Age = 35, Email = "poppy@gmail.com", Langs = { "SQL" }, Company = list[1] },

                new(){ Name = "Bobby", Age = 18, Email = "bobby@mail.ru", Langs = { "C++", "C#" }, Company = list[0] },
                new(){ Name = "Jimmy", Age = 44, Email = "jimmy@yandex.ru", Langs = { "Java", "C++" }, Company = list[2] },
                new(){ Name = "Tommy", Age = 28, Email = "tommy@gmail.com", Langs = { "Pascal", "Go", "C#" }, Company = list[2] },
                new(){ Name = "Sammy", Age = 19, Email = "sammy@mail.ru", Langs = { "JavaScript", "PHP", "Java" }, Company = list[1] },
                new(){ Name = "Denny", Age = 22, Email = "denny@yandex.ru", Langs = { "Kotlin", "Swift", "C#" }, Company = list[3] },
                new(){ Name = "Poppy", Age = 29, Email = "poppy@gmail.com", Langs = { "SQL" }, Company = list[3] },
            };
        }

        public static void PrintCollection<T>(IEnumerable<T> collection)
        {
            foreach (T item in collection)
                Console.WriteLine(item);
            Console.WriteLine();
        }

        public static List<CompanyList> InitCompaniesList()
        {
            var employees = Init();

            return new List<CompanyList>()
            {
                new()
                {
                    Title = "Yandex",
                    Employees = { employees[0], employees[2], employees[4] }
                },
                new()
                {
                    Title = "Ozon",
                    Employees = { employees[1], employees[3], employees[5] }
                },
            };
        }
    }
}
