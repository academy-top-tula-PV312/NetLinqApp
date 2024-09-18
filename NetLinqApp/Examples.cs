using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace NetLinqApp
{
    class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string? Company { get; set; }
    }

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

        public static void XmlWelcomeExample()
        {
            XmlDocument document = new XmlDocument();
            document.Load("example.xml");

            XmlElement? root = document.DocumentElement;

            if (root is not null)
            {
                foreach (XmlElement element in root)
                {
                    foreach (XmlAttribute attribute in element.Attributes)
                        Console.Write($"{attribute.Name}: {attribute.Value} ");
                    Console.Write("\t| ");

                    foreach (XmlElement node in element.ChildNodes)
                    {
                        Console.Write($"{node.Name}: {node.InnerText} ");


                        //if(node.Name == "name")
                        //    Console.Write($" Name: {node.InnerText}");
                        //if (node.Name == "age")
                        //    Console.Write($" Age: {node.InnerText} ");
                    }
                    Console.WriteLine();
                }
            }
        }

        public static void XmlCreateElementExample()
        {
            User user = new() { Name = "Mikky", Age = 30, Company = "Mail Group" };

            XmlDocument document = new XmlDocument();
            document.Load("example.xml");
            XmlElement? root = document.DocumentElement;

            XmlElement elementNew = document.CreateElement("user");

            XmlAttribute attributeCompany = document.CreateAttribute("company");

            XmlElement elementName = document.CreateElement("name");
            XmlElement elementAge = document.CreateElement("age");

            XmlText companyText = document.CreateTextNode(user.Company);
            XmlText nameText = document.CreateTextNode(user.Name);
            XmlText ageText = document.CreateTextNode(user.Age.ToString());

            attributeCompany.AppendChild(companyText);
            elementName.AppendChild(nameText);
            elementAge.AppendChild(ageText);

            elementNew.Attributes.Append(attributeCompany);
            elementNew.AppendChild(elementName);
            elementNew.AppendChild(elementAge);

            root?.AppendChild(elementNew);

            document.Save("example.xml");

        }

        public static void XmlSerializationExample()
        {
            var employees = ServiceApp.Init();
            var employee = ServiceApp.InitEmployee();

            //XmlSerializer serializer = new XmlSerializer(typeof(Employee));

            //using(FileStream stream = File.OpenWrite("employee.xml"))
            //{
            //    serializer.Serialize(stream, employee);
            //}

            XmlSerializer serializer = new XmlSerializer(typeof(List<Employee>));

            using (FileStream stream = File.OpenWrite("employees.xml"))
            {
                serializer.Serialize(stream, employees);
            }

            //using (FileStream reader = new FileStream("employees.xml", FileMode.Open))
            //{
            //    var empls = serializer.Deserialize(reader) as List<Employee>;

            //    foreach (var e in empls)
            //    {
            //        Console.WriteLine($"{e.Name} {e.Age} {e.Email}");
            //        Console.WriteLine($"\t{e.Company.Title} {e.Company.City}");
            //        string langs = "";
            //        foreach (var a in e.Langs)
            //            langs += a + " ";
            //        Console.WriteLine($"\t{langs}");
            //        Console.WriteLine();
            //    }

            //}
        }

        public static void XmlXPathExample()
        {
            XmlDocument document = new XmlDocument();
            document.Load("employees.xml");
            XmlElement? root = document.DocumentElement;

            XmlNodeList? nodeList = root?.SelectNodes("//City");

            if (nodeList is not null)
            {
                foreach (XmlNode node in nodeList)
                    Console.WriteLine($"{node.InnerText}\n");
            }
        }

        public static void XmlLinqCreateExample()
        {
            var employee = ServiceApp.InitEmployee();

            //XDocument document = new XDocument();

            //XElement root = new XElement("employee");

            //XElement name = new("name", employee.Name);
            //XElement age = new("age", employee.Age);
            //XElement email = new("email", employee.Email);

            //XElement company = new("company");
            //XElement title = new("title", employee.Company.Title);
            //XAttribute city = new("city", employee.Company.City!);
            //company.Add(title, city);

            //XElement langs = new("langs");
            //foreach(var l in employee.Langs)
            //{
            //    XElement lang = new("lang", l);
            //    langs.Add(lang);
            //}

            //root.Add(name, age, email, company, langs);

            //document.Add(root);
            //document.Save("employee_linq.xml");

            XElement langs = new XElement("langs");
            foreach (var l in employee.Langs)
            {
                XElement lang = new("lang", l);
                langs.Add(lang);
            }

            XDocument document = new XDocument(new XElement("employee",
                new XElement("name", employee.Name),
                new XElement("age", employee.Age),
                new XElement("email", employee.Email),
                new XElement("company",
                    new XAttribute("city", employee.Company.City),
                    new XElement("title", employee.Company.Title)),
                langs
                ));
            document.Save("employee_linq_quick.xml");
        }
    }
}
