using System;
using System.Collections.Generic;
using FubuCore.Reflection;
using FubuMVC.Core.UI.Configuration;
using HtmlTags;
using MyProject.Core.Infrastructure.Extensions;

namespace MyProject.Core.Infrastructure.Fubu.HtmlConventions
{
    public class EnumRadioButtonListEditor : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return def.Accessor.HasAttribute<EnumRadioButtonListAttribute>();
        }

        public override HtmlTag Build(ElementRequest request)
        {
            var enumType = request.Accessor.PropertyType;
            var enumValues = Enum.GetValues(enumType);
            var list = new HtmlTag("ul");
            foreach (var enumValue in enumValues)
            {
                var item = ListItemFor(enumValue, request);
                list.Children.Add(item);
            }
            return list;
        }

        private static HtmlTag ListItemFor(object enumValue, ElementRequest request)
        {
            var radioId = string.Format("{0}_{1}", request.ElementId, enumValue);

            var radioButton = new HtmlTag("input")
                .Attr("id", radioId)
                .Attr("name", request.ElementId)
                .Attr("type", "radio")
                .Attr("value", enumValue.ToString());
            if (enumValue.ToString() == request.StringValue())
            {
                radioButton.Attr("checked", "checked");
            }

            var label = new HtmlTag("label")
                .Attr("for", radioId)
                .Encoded(false)
                .Text(enumValue.ToDescriptionString());

            return new HtmlTag("li", tag => tag.Children.AddMany(new[] {radioButton, label}));
        }
    }
}
