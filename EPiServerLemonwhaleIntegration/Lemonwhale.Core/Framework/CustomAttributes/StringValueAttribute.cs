using System;

namespace Lemonwhale.Core.Framework.CustomAttributes
{
    public class StringValueAttribute : Attribute
    {
        public string StringValue { get; protected set; }

        public StringValueAttribute(string stringValue)
        {
            StringValue = stringValue;
        }
    }
}
