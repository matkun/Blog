using System.Collections.Generic;
using EPiServer.Core;

namespace XFlow.Core.Framework.Services
{
    public interface IPageLocatorService
    {
        PageDataCollection PagesUsingXForm(string formId);
        IEnumerable<string> XFlowDefinitions();
    }
}
