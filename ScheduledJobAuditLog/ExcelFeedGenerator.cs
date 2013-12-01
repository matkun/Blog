using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EPiServer.CodeSample.ScheduledJobAuditLog
{
    public class ExcelFeedGenerator
    {
        private readonly TableStyle _tableStyle;
        private readonly TableItemStyle _headerStyle;
        private readonly TableItemStyle _itemStyle;

        public ExcelFeedGenerator()
        {
            _tableStyle = new TableStyle
            {
                BorderStyle = BorderStyle.Solid,
                BorderColor = Color.Black,
                BorderWidth = Unit.Parse("1px"),
            };
            _headerStyle = new TableItemStyle
            {
                VerticalAlign = VerticalAlign.Top,
                BackColor = Color.DimGray,
            };
            _itemStyle = new TableItemStyle
            {
                VerticalAlign = VerticalAlign.Top,
                BorderStyle = BorderStyle.Solid,
                BorderColor = Color.DimGray,
            };
        }

        public string ExcelFeedFor(IEnumerable<AuditLogEntry> entries)
        {
            using (var sw = new StringWriter())
            using (var tw = new HtmlTextWriter(sw))
            {
                _tableStyle.AddAttributesToRender(tw);
                tw.RenderBeginTag(HtmlTextWriterTag.Table);

                BuildTableHead(tw);
                BuildTableBody(entries, tw);

                tw.RenderEndTag(); // Table

                return sw.ToString();
            }
        }

        private void BuildTableHead(HtmlTextWriter tw)
        {
            tw.RenderBeginTag(HtmlTextWriterTag.Thead);
            tw.RenderBeginTag(HtmlTextWriterTag.Tr);

            WriteHeaderCell(tw, "Timestamp");
            WriteHeaderCell(tw, "Action");
            WriteHeaderCell(tw, "Username");
            WriteHeaderCell(tw, "Email");
            WriteHeaderCell(tw, "Reason / Message");

            tw.RenderEndTag(); // Tr
            tw.RenderEndTag(); // Thead
        }

        private void BuildTableBody(IEnumerable<AuditLogEntry> entries, HtmlTextWriter tw)
        {
            tw.RenderBeginTag(HtmlTextWriterTag.Tbody);
            foreach (var entry in entries)
            {
                tw.RenderBeginTag(HtmlTextWriterTag.Tr);
                WriteTableCell(tw, entry.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"));
                WriteTableCell(tw, entry.Action.ToDescriptionString());
                WriteTableCell(tw, entry.Username);
                WriteTableCell(tw, entry.Email);
                WriteTableCell(tw, entry.Message.Replace("<br />", " "));
                tw.RenderEndTag(); // Tr
            }
            tw.RenderEndTag(); // Tbody
        }

        private void WriteHeaderCell(HtmlTextWriter htmlTextWriter, string columnTitle)
        {
            if (_headerStyle != null)
            {
                _headerStyle.AddAttributesToRender(htmlTextWriter);
            }
            htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Th);
            htmlTextWriter.Write(HttpUtility.HtmlEncode(columnTitle));
            htmlTextWriter.RenderEndTag();
        }

        private void WriteTableCell(HtmlTextWriter htmlTextWriter, string cellValue)
        {
            if (_itemStyle != null)
            {
                _itemStyle.AddAttributesToRender(htmlTextWriter);
            }
            htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            htmlTextWriter.Write(HttpUtility.HtmlEncode(cellValue));
            htmlTextWriter.RenderEndTag();
        }
    }
}
