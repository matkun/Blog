using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XFlow.Core.Extensions
{
    public static class StringExtensions
    {
        public static string EscapedJson(this string original)
        {
            if (string.IsNullOrEmpty(original))
                return string.Empty;

            var sb = new StringBuilder(original);
            return sb.Replace("\\", "\\\\").Replace("\"", "\\\"").ToString();
        }
        public static bool TrimmedNullOrEmpty(this string s)
        {
            if (s == null)
                return true;

            return s.Trim() == String.Empty;
        }
    }
}
