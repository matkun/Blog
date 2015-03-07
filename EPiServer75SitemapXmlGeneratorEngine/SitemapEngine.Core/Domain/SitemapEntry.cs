using System;
using EPiServer.Data.Dynamic;

namespace SitemapEngine.Core.Domain
{
    [EPiServerDataStore(
              StoreName = "SitemapEntry",
              AutomaticallyCreateStore = true,
              AutomaticallyRemapStore = true
          )]
	public class SitemapEntry
	{
		public SitemapEntry()
		{
			ChangeFrequency = Frequency.None;
			Priority = float.MinValue;
        }

		public Uri Location { get; set; }
		public DateTime? LastModified { get; set; }
		public Frequency ChangeFrequency { get; set; }
		public float Priority { get; set; }

		public string Language { get; set; }

		public bool IsEmpty { get; set; }

		public static SitemapEntry Empty
		{
			get { return new SitemapEntry {IsEmpty = true}; }
		}
	}
}
