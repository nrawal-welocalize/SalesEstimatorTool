using System;
using System.ComponentModel;
using System.Drawing;
using SalesEstimatorTool.Data.Attributes;

namespace SalesEstimatorTool.Data.Extensions
{
    public static class EnumExtension
    {
        public static String GetCustomDescription(object objEnum)
        {
            var field = objEnum.GetType().GetField(objEnum.ToString());
            var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : objEnum.ToString();
        }

        public static Color GetCustomColor(object objEnum)
        {
            var field = objEnum.GetType().GetField(objEnum.ToString());
            var attributes = (ColorAttribute[])field.GetCustomAttributes(typeof(ColorAttribute), false);
            return (attributes.Length > 0) ? attributes[0].ForeColor : SystemColors.ControlText;
        }

        public static String Description(this Enum value)
        {
            return GetCustomDescription(value);
        }

        public static Color ForeColor(this Enum value)
        {
            return GetCustomColor(value);
        }
    }
}
