using System;
using EPiServer.Data.Dynamic;

namespace EPiServer.Templates.Alloy.Plugins.LanguageFileEditor
{
	[EPiServerDataStore(
		StoreName = "LFELangFileBackupStore",
		AutomaticallyCreateStore = true,
		AutomaticallyRemapStore = true)]
	public class LangFileBackupContainer
	{
		public string BackupId { get; set; }
		public string Filename { get; set; }
		public string Content { get; set; }
		public DateTime Created { get; set; }
	}
}