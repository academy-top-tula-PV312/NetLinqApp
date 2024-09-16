using NetLinqApp;
using System.ComponentModel.Design.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

XDocument document = XDocument.Load("employees.xml");
XElement? root = document.Root;

if(root is not null)
{
    foreach(XElement element in root.Elements())
    {
        foreach(XElement inner in element.Elements())
            Console.WriteLine($"{inner.Name}: {inner.Value}");
        Console.WriteLine();
    }
}

