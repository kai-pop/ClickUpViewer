using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace ClickUpViewer.Infrastructure.WebApi
{
    public class OptionalParamGenerator
    {
        public static string GenerateOptionalParams(object param)
        {
            var listParamKeyValue = new List<string>();
            var props = param.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                object value = prop.GetValue(param);

                if (value != null)
                    listParamKeyValue.Add(SerializePropName(prop) + "=" + SerializePropValue(prop, value));
            }

            return string.Join("&", listParamKeyValue);
        }

        private static string SerializePropName(PropertyInfo prop)
        {
            var arraySuffix = (prop.PropertyType == typeof(List<string>)) ? "%5B%5D" : "";
            //var arraySuffix = (prop.PropertyType == typeof(List<string>)) ? "[]" : "";
            List<JsonPropertyAttribute> attrsJsonProperty = prop.GetCustomAttributes<JsonPropertyAttribute>(true).ToList();
            if (attrsJsonProperty.Count > 0) return attrsJsonProperty[0].PropertyName + arraySuffix;
            return prop.Name;
        }

        private static string SerializePropValue(PropertyInfo prop, object value)
        {
            if (value is DateTime dateTime) return new DateTimeOffset(((DateTime)value)).ToUnixTimeMilliseconds().ToString();
            if (value is TimeSpan timeSpan) return timeSpan.TotalMilliseconds.ToString();
            if (value is IEnumerable<string> list) return string.Join(",", list);

            return value.ToString();
        }
    }
}