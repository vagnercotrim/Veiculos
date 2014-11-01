using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Veiculos.Infra.Extensions
{
    public static class EnumExtension
    {
        
        public static string GetEnumDescription<TEnum>(this TEnum value)
        {
            DescriptionAttribute[] attributes = GetDescriptionAttribute<TEnum>(value);
            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            return value.ToString();
        }

        public static SelectList ToSelectList<TEnum>(this TEnum obj)
        {
            var values = from TEnum e in Enum.GetValues(typeof(TEnum))
                         select new { Text = e.GetEnumDescription<TEnum>(), Value = e };
            return new SelectList(values, "Value", "Text", obj);
        }
        
        private static DescriptionAttribute[] GetDescriptionAttribute<TEnum>(TEnum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes;
        }

    }
}