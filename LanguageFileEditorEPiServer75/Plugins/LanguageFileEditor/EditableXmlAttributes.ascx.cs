using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace EPiServer.Templates.Alloy.Plugins.LanguageFileEditor
{
	public partial class EditableXmlAttributes : UserControl
	{
		public XmlAttributeCollection DataSource { get; set; }

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (DataSource == null) return;

			rptAttributes.DataSource = DataSource;
			rptAttributes.DataBind();
		}

		protected void rptAttributes_OnItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			var phTextBox = e.Item.FindControl("phTextBox") as PlaceHolder;
			if (phTextBox == null) return;
			var attribute = e.Item.DataItem as XmlAttribute;
			var toolTip = string.Concat(attribute.OwnerElement.GetNodePath(), ":", attribute.Name);
			var textBox = new TextBox
							  {
								  ID = "tbAttribute",
								  CssClass = "attribute",
								  Text = attribute.Value,
								  ToolTip = toolTip,
								  Enabled = toolTip != "/languages/language:id" // don't allow changing language!
							  };
			phTextBox.Controls.Add(textBox);
		}
	}
}