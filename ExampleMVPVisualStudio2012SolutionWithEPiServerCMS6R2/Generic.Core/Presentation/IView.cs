using System;
using EPiServer.Core;

namespace Generic.Core.Presentation
{
    public interface IView : IView<PageData> {}
    public interface IView<T> where T : PageData
    {
        T CurrentPage { get; }
        Uri Uri { get; }
    }
}
