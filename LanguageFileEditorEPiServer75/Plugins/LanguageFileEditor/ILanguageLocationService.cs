namespace EPiServer.Templates.Alloy.Plugins.LanguageFileEditor
{
	public interface ILanguageLocationService
	{
		string LanguagePath { get; }
		string PathTo(string filename);
	}
}
