using EPiServer.Framework.Localization;

namespace EPiServer.Templates.Alloy.LanguageTool.Wrappers
{
	public class LocalizationServiceWrapper : ILocalizationServiceWrapper
	{
		public string GetString(string resourceKey)
		{
			return LocalizationService.Current.GetString(resourceKey);
		}
	}
}
