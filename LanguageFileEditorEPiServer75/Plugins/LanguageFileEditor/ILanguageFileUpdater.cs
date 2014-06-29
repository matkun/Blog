using Newtonsoft.Json.Linq;

namespace EPiServer.Templates.Alloy.Plugins.LanguageFileEditor
{
	public interface ILanguageFileUpdater
	{
		JObject NewContent { get; set; }
		void ExecuteApplyFor(string targetFile, string patternFile);
		void ExecuteReapplyFor(string targetFile);
		void ExecuteReapplyForAll();
	}
}
