/*
LINQ - Language Integrated Query

LINQ to Object
LINQ to Xml

Linq to DataSet
Linq to Entity

Parallel LINQ

 */

using NetLinqApp;

var employees = ServiceApp.Init();

List<Employee> employeesList = new();

// classic
foreach(var e in employees)
    if(e.Age > 20)
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