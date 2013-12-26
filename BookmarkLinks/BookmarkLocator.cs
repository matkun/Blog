using System;
using System.Linq;
using System.Text;
using System.Web;
using EPiServer.CodeSample.BookmarkLinks.HtmlGenerator;
using EPiServer.Core;
using HtmlAgilityPack;

namespace EPiServer.CodeSample.BookmarkLinks
{
    public class BookmarkLocator
    {
        public static string DefaultBookmark = "None (top of page)";
        private readonly HttpContext _context;

        public BookmarkLocator(HttpContext context)
        {
            if (context == null) throw new ArgumentNullException("context");
            _context = context;
        }

        public string BookmarkSelector()
        {
            var request = _context.Request;
            var pageId = request["pageId"];
            if (string.IsNullOrEmpty(pageId)) return string.Empty;

            var selectedBookmark = request["bookmark"];
            var generator = new HtmlService();
            var html = generator.HtmlFor(new PageReference(pageId));
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            var bookmarksLinks = htmlDocument.DocumentNode
                                    .Descendants("a")
                                    .Where(n => n.InnerText == string.Empty && n.Attributes.Contains("name"))
                                    .ToArray();

            var bookmarks = bookmarksLinks.Select(lnk => lnk.Attributes.First(a => a.Name == "name").Value).ToArray();

            const string optionFormat = "<option value='{0}'{1}>{2}</option>";
            const string selectedAttribute = " selected='selected'";

            var builder = new StringBuilder();
            builder.AppendLine("<div id='ddlBookmarkSelectorContainer'>");
            builder.AppendLine("<label class='episize100 epiindent bookmark-selector-label' for='ddlBookmarkSelector'>Bookmark:</label>");
            builder.AppendLine("<select id='ddlBookmarkSelector' class='episize240' name='ddlBookmarkSelector'>");
            builder.AppendFormat(optionFormat, DefaultBookmark, bookmarks.All(b => b != selectedBookmark) ? selectedAttribute : string.Empty, DefaultBookmark);
            foreach(var bookmark in bookmarks)
            {
                builder.AppendFormat(optionFormat, bookmark, bookmark == selectedBookmark ? selectedAttribute : string.Empty, bookmark);
            }
            builder.AppendLine("</select>");
            builder.AppendLine("</div>");

            return builder.ToString();
        }
    }
}
