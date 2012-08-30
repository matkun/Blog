using System;
using System.Linq;
using FubuCore.Reflection;
using FubuMVC.Core.UI.Configuration;
using HtmlTags;
using MyProject.Core.Infrastructure.Extensions;

namespace MyProject.Core.Infrastructure.Fubu.HtmlConventions
{
    public class EnumRadioButtonListDisplay : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return def.Accessor.HasAttribute<EnumRadioButtonListAttribute>();
        }

        public override HtmlTag Build(ElementRequest request)
        {
            var enumType = request.Accessor.PropertyType;
            var enumValues = Enum.GetValues(enumType);

            var selectedValue = enumValues.Cast<object>()
                .First(e => e.ToString() == request.StringValue());

            var span = new HtmlTag("span")
                .Encoded(false)
                .Text(selectedValue.ToDescriptionString());
            
            return span;
        }
    }
}
