using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace EPiServer.Plugins.LanguageFileEditor
{
    public partial class EditableXmlNode : UserControl
    {
        public XmlNodeList DataSource { get; set; }
        
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (DataSource == null) return;

            rptXmlNodeList.DataSource = DataSource;
            rptXmlNodeList.DataBind();
        }

        protected void rptXmlNodeList_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var phContent = e.Item.FindControl("phContent") as PlaceHolder;
            var phToggleLink = e.Item.FindControl("phToggleLink") as PlaceHolder;
            if (phContent == null) return;

            var xmlNode = e.Item.DataItem as XmlNode;
            if (xmlNode == null) return;

            if (xmlNode.HasChildNodes && xmlNode.FirstChild.GetType() != typeof(XmlText))
            {
                phToggleLink.Controls.Add(new HyperLink {Text = "-", ToolTip = "Expand/Collapse", NavigateUrl = "#"});
                var childNode = LoadControl(@"~\Plugins\LanguageFileEditor\EditableXmlNode.ascx") as EditableXmlNode;
                childNode.DataSource = xmlNode.ChildNodes;
                phContent.Controls.Add(childNode);
                return;
            }
            phToggleLink.Controls.Add(new Literal { Text = "&#160;&#160;" });
            var textBox = new TextBox
                              {
                                  ID = "tbContent",
                                  Text = xmlNode.HasChildNodes ? xmlNode.FirstChild.Value : string.Empty,
                                  ToolTip = xmlNode.GetNodePath()
                              };
            phContent.Controls.Add(textBox);
        }
    }
}