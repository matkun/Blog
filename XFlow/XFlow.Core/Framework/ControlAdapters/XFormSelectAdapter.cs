using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EPiServer.UI.WebControls;
using XFlow.Core.Extensions;
using System.Web.UI.Adapters;
using EPiServer.UI.Edit;
using XFlow.Core.Framework.Language;
using XFlow.Core.Framework.Services;

namespace XFlow.Core.Framework.ControlAdapters
{
    public class XFormSelectAdapter : ControlAdapter
    {
        private SortedGridView _sortedGridView;
        private IPageLocatorService _pageLocatorService;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var xFormSelect = (XFormSelect)Control;
            if(xFormSelect == null)
            {
                return;
            }
            _sortedGridView = xFormSelect.FindControlRecursive("GridAvailableXForms") as SortedGridView;
            if (_sortedGridView == null)
            {
                return;
            }

            _pageLocatorService = IoC.IoC.Get<IPageLocatorService>();
            AddXFlowUsedOnPages();
        }
        
        private void AddXFlowUsedOnPages()
        {
            foreach (GridViewRow row in _sortedGridView.Rows)
            {
                AddXFlowPagesTo(row);
            }
        }

        private void AddXFlowPagesTo(TableRow row)
        {
            var columnIndex = UsedOnPagesColumnIndex();
            if (columnIndex == null)
            {
                return;
            }
            var xFormId = GetIdFor(row);
            if(xFormId == Guid.Empty)
            {
                return;
            }
            var pages = _pageLocatorService.PagesUsingXForm(xFormId.ToString());
            if(pages.Any())
            {
                DisableDeleteButtonFor(row);
            }
            const string format = "{0}[{1}]";
            var xflowString = string.Join(", ", pages.Select(page => string.Format(format, page.PageName, page.PageLink)).ToArray());
            xflowString = string.Concat(xflowString, "</strong>");

            if(row.Cells[columnIndex.Value].Text != "<strong></strong>")
            {
                xflowString = string.Concat(", ", xflowString);
            }
            row.Cells[columnIndex.Value].Text = row.Cells[columnIndex.Value].Text.Replace("</strong>", xflowString);
        }

        private int? _columnIndex;
        private int? UsedOnPagesColumnIndex()
        {
            if (_columnIndex != null)
            {
                return _columnIndex;
            }
            var columnHeading = IoC.IoC.Get<ITranslator>().Translate("/edit/editxform/usedonpages");
            for (var i = 0; i < _sortedGridView.HeaderRow.Cells.Count; i++)
            {
                if (_sortedGridView.HeaderRow.Cells[i].Text == columnHeading)
                {
                    _columnIndex = i;
                    break;
                }
            }
            return _columnIndex;
        }

        private static void DisableDeleteButtonFor(Control row)
        {
            var deleteButton = row.FindControlRecursive("DeleteButton") as ToolButton;
            if(deleteButton == null)
            {
                return;
            }
            deleteButton.Enabled = false;
            var span = deleteButton.Parent as HtmlGenericControl;
            if(span == null)
            {
                return;
            }
            const string disableClass = "epi-cmsButtondisabled";
            var spanClasses = span.Attributes["class"];
            if (!string.IsNullOrEmpty(spanClasses) && spanClasses.Contains(disableClass))
            {
                return;
            }
            span.Attributes["class"] = string.Join(" ", new[] {spanClasses, disableClass});
        }

        private static Guid GetIdFor(Control row)
        {
            // Yes, and where would _you_ find the Guid for this row then?
            var editButton = row.FindControlRecursive("EditExistingFormButton") as ToolButton;
            if(editButton == null)
            {
                return Guid.Empty;
            }
            var regex = new Regex(@"([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}");
            var matches = regex.Matches(editButton.OnClientClick);
            Guid formId;
            return matches.Count <= 0 || !Guid.TryParse(matches[0].ToString(), out formId) ? Guid.Empty : formId;
        }
    }
}
