using System;
using System.Web.UI;

namespace PageTypeTreeFilter.Presentation
{
    public interface IView
    {
        Page CurrentPage { get; }
        Uri Uri { get; }
    }
}
