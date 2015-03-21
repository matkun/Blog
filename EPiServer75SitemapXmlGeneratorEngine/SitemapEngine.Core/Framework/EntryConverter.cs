using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using SitemapEngine.Core.Domain;
using SitemapEngine.Core.Extensions;
using SitemapEngine.Core.Infrastructure;

namespace SitemapEngine.Core.Framework
{
	public class EntryConverter : IEntryConverter
	{
		public byte[] ToBytes(SitemapEntry[] entries)
		{
			using (var memoryStream = new MemoryStream())
			using (var xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8))
			{
                WriteTo(xmlTextWriter, entries);
				return memoryStream.ToArray();
			}
		}
        
        public byte[] EmptyBytes()
        {
            using (var memoryStream = new MemoryStream())
            using (var xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8))
            {
                xmlTextWriter.WriteStartDocument();
                xmlTextWriter.WriteStartElement("urlset", Constants.XmlNamespace);
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.Flush();

                return memoryStream.ToArray();
            }
        }

        public byte[] IndexBytes(string[] sitemaps)
        {
            using (var memoryStream = new MemoryStream())
            using (var xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8))
            {
                WriteIndexTo(xmlTextWriter, sitemaps);
                return memoryStream.ToArray();
            }
        }

        private static void WriteIndexTo(XmlWriter writer, IEnumerable<string> sitemaps)
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("sitemapindex", Constants.XmlNamespace);

            foreach (var url in sitemaps)
            {
                writer.WriteStartElement("sitemap");
                writer.WriteElementString("loc", url);
                writer.WriteElementString("lastmod", DateTime.Now.FormatWithUtc());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.Flush();
        }

		private static void WriteTo(XmlWriter writer, IEnumerable<SitemapEntry> entries)
		{
			writer.WriteStartDocument();
            writer.WriteStartElement("urlset", Constants.XmlNamespace);

			foreach (var entry in entries)
			{
				var url = Escape(entry.Location.ToString());

				writer.WriteStartElement("url");
				writer.WriteElementString("loc", url);

				if (entry.LastModified.HasValue)
				{
					writer.WriteElementString("lastmod", entry.LastModified.Value.FormatWithUtc());
				}

				if (entry.ChangeFrequency != Frequency.None)
				{
					writer.WriteElementString("changefreq", entry.ChangeFrequency.ToValueString());
				}

				if (!entry.Priority.Equals(float.MinValue))
				{
					writer.WriteElementString("priority", entry.Priority.ToString(CultureInfo.InvariantCulture));
				}

				writer.WriteEndElement();
			}

			writer.WriteEndElement();
			writer.Flush();
		}
        
		private static string Escape(string url)
		{
			var forbidden = new Dictionary<string, string>
			{
				{">", "&gt;"},
				{"<", "&lt;"},
				{"&", "&amp;"},
				{"\"", "&quot;"},
				{"'", "&apos;"},
			};
			var escaped = forbidden
				.Aggregate(url, (current, pair) => Regex.Replace(current, pair.Key, pair.Value));

			return RemoveDiacritics(escaped);
		}

		private static string RemoveDiacritics(string url)
		{
			var chars = url
				.Normalize(NormalizationForm.FormD)
				.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
				.ToArray();
			return new string(chars);
		}
	}
}
