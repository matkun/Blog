namespace EPiServer.Templates.Alloy.Plugins.LanguageFileEditor
{
	public interface ISecurityValidator
	{
		void EnsureValidUser();
		void EnsureValid(string path);
	}
}
