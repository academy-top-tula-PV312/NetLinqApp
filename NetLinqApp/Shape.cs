using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetLinqApp
{
    public class Shape
    {
        public string Title { get; set; } = null!;
        public Shape(string title = "Shape") => Title = title;

        public override string ToString()
        {
            return Title;
        }
    }

    public class Rectangle : Shape
    {
        public Rectangle() : base("Rectangle") { }
        public Rectangle(string title) : base(title) { }
    }

    public class Circle : Shape
    {
        public Circle() : base("Circle") { }
    }

    public class Square : Rectangle
    {
        public Square() : base("Square") { }
    }
}
