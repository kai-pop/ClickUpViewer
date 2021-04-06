using System;
namespace ClickUpViewer
{
    public static class Extensions
    {
        public static DateTime? ParseDate(this string value, DateTime? ifNull)
        {
            return string.IsNullOrEmpty(value) ? ifNull : DateTime.Parse(value);
        }
    }
}