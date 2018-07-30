using System;
using System.Drawing;

namespace SalesEstimatorTool.Data.Attributes
{
    public class ColorAttribute : Attribute
    {
        public ColorAttribute(String name)
        {
            var color = Color.FromName(name);
            ForeColor = color;
        }

        public Color ForeColor { get; private set; }
    }
}
