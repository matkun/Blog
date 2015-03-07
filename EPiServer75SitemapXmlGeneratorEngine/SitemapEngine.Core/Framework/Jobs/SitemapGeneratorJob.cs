using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using EPiServer.PlugIn;
using EPiServer.ServiceLocation;
using SitemapEngine.Core.Extensions;
using log4net;

namespace SitemapEngine.Core.Framework.Jobs
{
	[ScheduledPlugIn(
		DisplayName = "Generate Sitemap",
		Description = "Generates a fresh Sitemap from predefined strategies. Entries are stored in the EPiServer DynamicDataStore."
	)]
	public class SitemapGeneratorJob
	{
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SitemapGeneratorJob));

		public static string Execute()
		{
            try
            {
                Logger.Info(string.Format("Starting SitemapGeneratorJob on '{0}'", Environment.MachineName));
                var repository = ServiceLocator.Current.GetInstance<ISitemapRepository>();
                var urlService = ServiceLocator.Current.GetInstance<IHostBindingsService>();

                var timer = Stopwatch.StartNew();
                timer.Start();

                var result = repository.RebuildSitemap();

                foreach (var language in urlService.AllIetfLanguageTags())
                {
                    repository.RefreshSitemapCacheFor(language);
                }

                var message = GetResultMessage(result, timer);
                Logger.Info(string.Format("Job status: SitemapGeneratorJob succeeded (on {0})\n{1}", Environment.MachineName, message.Replace("<br />", "\n")));
                return message;
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("Job status: SitemapGeneratorJob failed (on {0})", Environment.MachineName), e);
                throw;
            }
		}

        private static string GetResultMessage(Dictionary<string, int> entryStatistics, Stopwatch timer)
		{
			var entries = entryStatistics.Aggregate(string.Empty, (current, pair) => current + string.Format("Added '{0}' entries for '{1}'<br />", pair.Value, pair.Key));
            const string message = "Server: {0}<br />{1}in {2}";
            return string.Format(message, Environment.MachineName, entries, timer.ToElapsedTimeString());
		}
	}
}
