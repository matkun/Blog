using System;
using System.Web.UI;
using EPiServer.Core;

namespace PageTreeExtended
{
    public class PageTreeEventArgs : EventArgs
    {
        public Control Item { get; set; }
        public PageData DataItem { get; set; }
    }
}