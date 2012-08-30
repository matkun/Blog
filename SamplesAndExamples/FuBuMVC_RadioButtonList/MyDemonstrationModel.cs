using System;
using System.Collections.Generic;
using MyProject.Core.Domain;
using MyProject.Core.Infrastructure.Fubu.HtmlConventions;

namespace MyProject.Web.Features.MyDemonstration
{
    public class MyDemonstrationModel
    {
        [EnumRadioButtonList]
		public TextPosition Position { get; set; }
    }
}
