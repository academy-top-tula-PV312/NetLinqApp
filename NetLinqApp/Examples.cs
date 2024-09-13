using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetLinqApp
{
    public class Examples
    {
        class EmployeeEmail
        {
            public string? Name { get; set; }
            public string? Email { get; set; }
        }

        public static void WelcomeExample()
        {
            /*
            LINQ - Language Integrated Query

            LINQ to Object
            LINQ to Xml

            Linq to DataSet
            Linq to Entity

            Parallel LINQ

             */


            var employees = ServiceApp.Init();

            List<Employee> employeesList = new();

            // classic
            foreach (var e in employees)
                if (e.Age > 20)
                    employeesList.Add(e);
            employeesList.Sort((e1, e2) => e1.Name!.CompareTo(e2.Name));

            foreach (var e in employeesList)
                Console.WriteLine($"Name: {e.Name} Age: {e.Age}");
            Console.WriteLine();


            // linq query
            var employeesQuery = from e in employees
                                 where e.Age > 20
                                 orderby e.Name
                                 select e;

            foreach (var e in employeesQuery)
                Console.WriteLine($"Name: {e.Name} Age: {e.Age}");
            Console.WriteLine();

            // linq methods

            var employeesMethod = employees.Where(e => e.Age > 20)
                                           .OrderBy(e => e.Name);

            foreach (var e in employeesMethod)
                Console.WriteLine($"Name: {e.Name} Age: {e.Age}");
            Console.WriteLine();
        }

        public static void ProectionExample()
        {

        var employees = ServiceApp.Init();

            // Select - proection

            // query
            var employeesNameQuery = from e in employees
                                     select e.Name;

            foreach (var employee in employeesNameQuery)
                Console.WriteLine(employee);
            Console.WriteLine();

            // method
            var employeesNameMethod = employees.Select(e => e.Name);

            foreach (var employee in employeesNameMethod)
                Console.WriteLine(employee);
            Console.WriteLine();

            // query
            var employeesEmailQuery = from e in employees
                                      select new
                                      {
                                          EmployeeName = e.Name,
                                          WorkEmail = e.Email,
                                      };
            //select new EmployeeEmail()
            //{
            //    Name = e.Name,
            //    Email = e.Email,
            //};

            foreach (var employee in employeesEmailQuery)
                //Console.WriteLine($"{employee.Name} {employee.Email}");
                Console.WriteLine($"{employee.EmployeeName} {employee.WorkEmail}");
            Console.WriteLine();

            // method
            //var employeesEmailMethod = employees.Select(e => new EmployeeEmail()
            //{
            //    Name = e.Name,
            //    Email = e.Email,
            //});

            var employeesEmailMethod = employees.Select(e => new
            {
                e.Name,
                e.Email,
            });


            foreach (var employee in employeesEmailMethod)
                Console.WriteLine($"{employee.Name} {employee.Email}");
            Console.WriteLine();
        }

        public static void SelectManyExample()
        {
            var employees = ServiceApp.Init();

            // variables into query

            var employeesVars = from e in employees
                                let name = $"Mr. {e.Name}"
                                let year = DateTime.Now.Year - e.Age
                                select new
                                {
                                    Name = name,
                                    Year = year,
                                };

            foreach (var e in employeesVars)
                Console.WriteLine($"{e.Name} {e.Year}");
            Console.WriteLine();



            // SelectMany

            var companies = ServiceApp.InitCompaniesList();

            // select many query
            var emplayeesAllQuery = from c in companies
                                    from e in c.Employees
                                    select e;

            var langsAll = from c in companies
                           from e in c.Employees
                           from l in e.Langs
                           select l;
            foreach (var l in langsAll) Console.Write($"{l} ");
            Console.WriteLine();


            // select many method

            var employeesAll = companies.SelectMany(c => c.Employees);
            ServiceApp.PrintCollection(employeesAll);


            var employeesCompanyMethod = companies.SelectMany(
                    c => c.Employees,
                    (c, e) => new
                    {
                        EmployeeName = e.Name,
                        CompanyTitle = c.Title
                    }
                );

            foreach (var ec in employeesCompanyMethod)
                Console.WriteLine($"{ec.EmployeeName} {ec.CompanyTitle}");

        }

        public static void WhereExample()
        {
            // WHERE - filters

            var employees = ServiceApp.Init();

            // query
            var employeesPolyglot = from e in employees
                                    where e.Langs.Count() > 1
                                    select e;
            ServiceApp.PrintCollection(employeesPolyglot);

            var employeesPolyglot20 = from e in employees
                                          //where e.Langs.Count() > 1 && e.Age > 20
                                      where e.Langs.Count() > 1
                                      where e.Age > 20
                                      select e;
            ServiceApp.PrintCollection(employeesPolyglot20);

            // method
            var employeesPolyglotMethod = employees.Where(e => e.Langs.Count > 1);
            ServiceApp.PrintCollection(employeesPolyglotMethod);

            var employeesPolyglotMethod20 = employees.Where(e => e.Langs.Count > 1)
                                                     .Where(e => e.Age > 20);
            ServiceApp.PrintCollection(employeesPolyglotMethod20);



            // query
            var employeesCsQuery = from e in employees
                                   from l in e.Langs
                                   where l == "C#"
                                   select e;
            ServiceApp.PrintCollection(employeesCsQuery);

            var employeesCsQueryTo20 = from e in employees
                                       from l in e.Langs
                                       where e.Age <= 20
                                       where l == "C#"
                                       select e;
            ServiceApp.PrintCollection(employeesCsQueryTo20);

            // method

            var employeesCsMethod = employees.SelectMany(
                                        e => e.Langs,
                                        (e, l) => new { Employee = e, Lang = l })
                                        //.Where(o => o.Lang == "C#" && o.Employee.Age <= 20)
                                        .Where(o => o.Lang == "C#")
                                        .Where(o => o.Employee.Age <= 20)
                                        .Select(o => o.Employee);
            ServiceApp.PrintCollection(employeesCsMethod);


            List<Shape> shapes = new()
            {
                new Shape(),
                new Rectangle(),
                new Circle(),
                new Square(),
                new Rectangle(),
                new Rectangle(),
                new Circle(),
                new Square(),
            };

            var shapesAll = shapes.OfType<Shape>().ToList();
            ServiceApp.PrintCollection(shapesAll);

            var rects = shapes.OfType<Rectangle>().ToList();
            ServiceApp.PrintCollection(rects);

        }

        public static void OrderByExample()
        {
            var employees = ServiceApp.Init();

            //ServiceApp.PrintCollection(employees);

            // query
            var employeesOrdersQuery = from e in employees
                                       orderby e.Name // descending
                                       select e;
            ServiceApp.PrintCollection(employeesOrdersQuery);


            // method
            var employeesOrdersMethod = employees.OrderBy(e => e.Name); // OrderByDescending(e => e.Name);
            ServiceApp.PrintCollection(employeesOrdersMethod);

            var employeesOrderCompany = employees.OrderBy(e => e.Company.Title);
            ServiceApp.PrintCollection(employeesOrderCompany);


            // query
            var employeesOrderCompanyAgeQuery = from e in employees
                                                orderby e.Company.Title, e.Age descending
                                                select e;
            ServiceApp.PrintCollection(employeesOrderCompanyAgeQuery);


            // method
            var employeesOrderCompanyAgeMethod = employees.OrderBy(e => e.Company.Title)
                                                          .ThenByDescending(e => e.Age);
            ServiceApp.PrintCollection(employeesOrderCompanyAgeMethod);

        }
    }
}
