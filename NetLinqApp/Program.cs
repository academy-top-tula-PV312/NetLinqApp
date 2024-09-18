using NetLinqApp;
using System.ComponentModel.Design.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

XDocument document = XDocument.Load("employees.xml");
XElement? root = document.Root;

// READ
//if (root is not null)
//{
//    foreach (XElement element in root.Elements())
//    {
//        //Console.WriteLine($"{element.Name}: {element.Value}");
//        foreach (XElement inner in element.Elements())
//            foreach(XElement city in inner.Elements("City"))
//                Console.WriteLine($"{city.Name}: {city.Value}");
//        Console.WriteLine();
//    }
//}

// CREATE
//if(root is not null)
//{
//    root.Add(new XElement("Employee",
//                new XElement("Name", "Lolly"),
//                new XElement("Age", "26"),
//                new XElement("Company",
//                    new XElement("Title", "Yandex"),
//                    new XElement("City", "Moscow"))));
//    document.Save("employees.xml");
//}


// UPDATE
//var denny = root?.Elements("Employee")
//                .LastOrDefault(e => e?.Element("Name")?
//                                      .Value
//                                      .ToLower() == "denny");
//                //.Elements("Name")
//                //.FirstOrDefault(n => n.Value.ToLower() == "denny");

//if(denny is not null)
//{
//    denny.Element("Age")!.Value = "33";
//}

// DELETE
var jimmy = root?.Elements("Employee")
                .FirstOrDefault(e => e?.Element("Name")?
                                      .Value
                                      .ToLower() == "jimmy");

if (jimmy is not null)
{
    jimmy.Remove();
    document.Save("employees.xml");
}
