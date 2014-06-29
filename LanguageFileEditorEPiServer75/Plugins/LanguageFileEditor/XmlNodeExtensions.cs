using System.Xml;

namespace EPiServer.Templates.Alloy.Plugins.LanguageFileEditor
{
	public static class XmlNodeExtensions
	{
		public static string GetNodePath(this XmlNode xmlNode)
		{
			return xmlNode == null || xmlNode.GetType() == typeof(XmlDocument)
					   ? string.Empty
					   : string.Concat(GetNodePath(xmlNode.ParentNode), "/", xmlNode.Name);
		}
	}
}
