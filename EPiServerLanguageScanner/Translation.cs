using System;

namespace LanguageScanner
{
    public class Translation : IComparable<Translation>
    {
        public string File { get; set; }
        public string XPath { get; set; }
        public string Content { get; set; }
        
        public int CompareTo(Translation other)
        {
            return String.Compare(this.XPath, other.XPath, StringComparison.Ordinal);
        }
    }
}
