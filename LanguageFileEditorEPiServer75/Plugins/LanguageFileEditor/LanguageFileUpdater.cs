using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using EPiServer.Data.Dynamic;
using log4net;
using Newtonsoft.Json.Linq;

namespace EPiServer.Templates.Alloy.Plugins.LanguageFileEditor
{
	public class LanguageFileUpdater : ILanguageFileUpdater
	{
		private readonly ILanguageLocationService _languageLocationService;

		private readonly ILog _auditLogger = LogManager.GetLogger(typeof(LanguageFileUpdater));
		private Dictionary<string, string> TrackingDictionary { get; set; }
		private XmlDocument PatternXmlDocument { get; set; }
		private bool IsNewFile { get; set; }

		public JObject NewContent { get; set; }

		public LanguageFileUpdater(ILanguageLocationService languageLocationService)
		{
			if (languageLocationService == null) throw new ArgumentNullException("languageLocationService");
			_languageLocationService = languageLocationService;
		}

		public void ExecuteApplyFor(string targetFile, string patternFile)
		{
			IsNewFile = !targetFile.ToLower().Equals(patternFile.ToLower());
			PatternXmlDocument = GetPatternXmlDocument(patternFile);
			if (!IsNewFile)
			{
				LoadTrackingDictionaryFor(targetFile);
			}
			ApplyChangesFor(targetFile);
			if (!IsNewFile)
			{
				SaveTrackingDictionaryFor(targetFile);
			}
			SaveLanguageFileChangesFor(targetFile);
		}

		public void ExecuteReapplyFor(string targetFile)
		{
			var store = typeof(ChangeTrackingContainer).GetStore();
			var existingBags = store.FindAsPropertyBag("Filename", targetFile);
			ReapplyChangesFor(existingBags.First());
		}

		public void ExecuteReapplyForAll()
		{
			var store = typeof(ChangeTrackingContainer).GetStore();
			foreach (var bag in store.LoadAllAsPropertyBag())
			{
				ReapplyChangesFor(bag);
			}
		}

		private void ReapplyChangesFor(IDictionary<string, object> bag)
		{
			var filename = bag["Filename"] as string;
			PatternXmlDocument = GetPatternXmlDocument(filename);
			var content = bag["Content"] as Dictionary<string, string>;
			foreach (var pair in content)
			{
				MakeChange(pair.Key, pair.Value);
			}
			SaveLanguageFileChangesFor(filename);

			_auditLogger.InfoFormat("User: {0} reapplied changes for TargetFile: {1}", HttpContext.Current.User.Identity.Name, filename);
		}

		private void LoadTrackingDictionaryFor(string filename)
		{
			var store = typeof(ChangeTrackingContainer).GetStore();
			var bags = store.FindAsPropertyBag("Filename", filename).ToArray();
			if (!bags.Any())
			{
				TrackingDictionary = new Dictionary<string, string>();
				return;
			}
			TrackingDictionary = bags.First()
									 .First(bag => bag.Key.Equals("Content"))
									 .Value as Dictionary<string, string>;
		}

		private void SaveTrackingDictionaryFor(string filename)
		{
			var store = typeof(ChangeTrackingContainer).GetStore();
			var existingBags = store.FindAsPropertyBag("Filename", filename);
			foreach (var existingBag in existingBags)
			{
				store.Delete(existingBag.Id);
			}

			store.Save(
				new ChangeTrackingContainer
					{
						Filename = filename,
						Content = TrackingDictionary
					}
				);
		}

		private void SaveLanguageFileChangesFor(string filename)
		{
			var targetFilePath = _languageLocationService.PathTo(filename);
			using (var xmlTextWriter = new XmlTextWriter(targetFilePath, Encoding.UTF8))
			{
				xmlTextWriter.Formatting = Formatting.Indented;
				PatternXmlDocument.Save(xmlTextWriter);
			}
		}

		private void ApplyChangesFor(string filename)
		{
			foreach (var changePair in NewContent)
			{
				var path = changePair.Key;
				var value = changePair.Value.ToString();

				MakeChange(path, value);

				_auditLogger.InfoFormat("TargetFile: {0}, User: {1}, Path: {2}, NewValue: {3}", filename, HttpContext.Current.User.Identity.Name, path, value);
				if (IsNewFile) continue;
				TrackingDictionary[path] = value;
			}
		}

		private void MakeChange(string path, string value)
		{
			var root = PatternXmlDocument.DocumentElement;
			XmlNode xmlNode;
			if (path.Contains(":"))
			{
				var nodeAttributeArray = path.Split(':');
				var nodePath = nodeAttributeArray[0];
				var attributeName = nodeAttributeArray[1];

				xmlNode = root.SelectSingleNode(nodePath);
				var nodeAttribute = xmlNode.Attributes[attributeName];
				nodeAttribute.Value = value;
				return;
			}
			xmlNode = root.SelectSingleNode(path);
			if (xmlNode.LastChild == null)
			{
				xmlNode.AppendChild(PatternXmlDocument.CreateTextNode(value));
				return;
			}
			xmlNode.LastChild.Value = value;
		}

		private XmlDocument GetPatternXmlDocument(string patternFilename)
		{
			var patternFilePath = _languageLocationService.PathTo(patternFilename);
			if (!File.Exists(patternFilePath)) return null;
			var xmlDocument = new XmlDocument();
			using (var fileStream = new FileStream(patternFilePath, FileMode.Open))
			{
				xmlDocument.Load(fileStream);
			}
			return xmlDocument;
		}
	}
}
