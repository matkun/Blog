using System;
using EPiServer.Data.Dynamic;
using SitemapEngine.Core.Infrastructure;

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
            Bundle = Constants.Bundles.Default;
        }

		public Uri Location { get; set; }
		public DateTime? LastModified { get; set; }
		public Frequency ChangeFrequency { get; set; }
		public float Priority { get; set; }

        public string Language { get; set; }
        public virtual string Bundle { get; set; }

		public bool IsEmpty { get; set; }

		public static SitemapEntry Empty
		{
			get { return new SitemapEntry {IsEmpty = true}; }
		}
	}
}
