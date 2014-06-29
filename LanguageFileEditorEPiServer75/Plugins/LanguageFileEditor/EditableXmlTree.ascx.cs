using System;
using System.Web.UI;
using System.Xml;

namespace EPiServer.Templates.Alloy.Plugins.LanguageFileEditor
{
	public partial class EditableXmlTree : UserControl
	{
		public XmlNodeList DataSource { get; set; }
		public XmlDeclaration XmlDeclaration { get; set; }

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (DataSource == null) return;

			csNodes.DataSource = DataSource;
			csNodes.DataBind();
		}
	}
}