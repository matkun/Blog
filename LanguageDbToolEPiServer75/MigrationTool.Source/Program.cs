using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using NDesk.Options;

namespace LanguageMigration
{
    public class Program
    {
        private static readonly Stopwatch GlobalStopWatch = new Stopwatch();
        private static readonly Stopwatch LocalStopWatch = new Stopwatch();

        private static string _templateFile;
        public static string TemplateFile
        {
            get
            {
                if(string.IsNullOrWhiteSpace(_templateFile))
                {
                    _templateFile = File.ReadAllText(@".\FileTemplate.txt");
                }
                return _templateFile;
            }
        }

        public static string OutputPath { get; set; }

        static void Main(string[] args)
        {
            OutputPath = string.Concat(Directory.GetCurrentDirectory(), @"\output");
            var langPath = Directory.GetCurrentDirectory();
            var printHelpAndExit = false;

            var options = new OptionSet
                          {
                              { "op|outputPath=", string.Format("Path to the directory where to output the resulting files; default is '{0}'", OutputPath), arg => OutputPath = arg },
                              { "lp|langPath=", string.Format("Path to the directory containing your language files; default is '{0}'", langPath), arg => langPath = arg },
                              { "h|?|help", "Shows the help", arg => printHelpAndExit = arg != null },
                          };
            try
            {
                var unknown = options.Parse(args);
                if (unknown.Any() || printHelpAndExit)
                {
                    options.WriteOptionDescriptions(Console.Out);
                    Environment.Exit(0);
                }
                GlobalStopWatch.Start();
                LocalStopWatch.Start();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Out.WriteLine(string.Empty);
                Console.Out.WriteLine("--- Starting to migrate language files ---");
                Console.Out.WriteLine(string.Empty);
                var translations = ScanLanguageFilesIn(langPath);
                var langPathTime = LocalStopWatch.Elapsed;
                LocalStopWatch.Stop();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Out.WriteLine("Language files scanned in: {0}", langPathTime.ToString("g"));
                Console.Out.WriteLine("Number of translations:    {0}", translations.Count());
                Console.Out.WriteLine(string.Empty);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Out.WriteLine("--- Starting to migrate translations ---");
                Console.Out.WriteLine(string.Empty);

                var groups = translations.GroupBy(t => t.IetfLanguageTag);
                foreach (var group in groups)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Out.Write(@"Creating file: {0}.cs", group.Key);
                    LocalStopWatch.Reset();
                    LocalStopWatch.Start();
                    CreateFileFor(group);
                    var fileTime = LocalStopWatch.Elapsed;
                    LocalStopWatch.Stop();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Out.WriteLine(" - Done in {0}", fileTime.ToString("g"));
                }
                Console.Out.WriteLine(string.Empty);

                var scanCompleteTime = GlobalStopWatch.Elapsed;
                GlobalStopWatch.Stop();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Out.WriteLine("**************************************");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Out.WriteLine("Language files scanned in: {0}", langPathTime.ToString("g"));
                Console.Out.WriteLine("Total migrate time:        {0}", scanCompleteTime.ToString("g"));
                Console.Out.WriteLine("Translations migrated:     {0}", translations.Count());
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Out.WriteLine("**************************************");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Out.WriteLine(string.Empty);
                Console.Out.WriteLine("Now you will have to go through the files created in");
                Console.Out.WriteLine(OutputPath);
                Console.Out.WriteLine("and ensure that they are correct, as well as set the proper");
                Console.Out.WriteLine("content type for the translations; and of course split them");
                Console.Out.WriteLine("up in proper groups.");
                Console.Out.WriteLine(string.Empty);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Out.WriteLine(string.Empty);
                Console.Error.WriteLine(ex);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static Translation[] ScanLanguageFilesIn(string langPath)
        {
            var translations = new List<Translation>();
            var paths = Directory.GetFiles(langPath)
                            .Where(file => file.EndsWith(".xml"))
                            .ToArray();
            foreach (var file in paths)
            {
                translations.AddRange(TranslationsIn(file));
            }
            return translations.ToArray();
        }

        private static IEnumerable<Translation> TranslationsIn(string file)
        {
            var doc = XDocument.Load(file);
            var ietfLanguageTag = doc.Root.Element("language").Attribute("id").Value;
            
            var leaves = from e in doc.Descendants() where !e.Elements().Any() select e;
            return leaves.Select(leaf => new Translation
                                            {
                                                XPath = leaf.XPath(),
                                                Content = leaf.Value,
                                                IetfLanguageTag = ietfLanguageTag
                                            });
        }

        private static void CreateFileFor(IGrouping<string, Translation> group)
        {
            const string @namespace = "TODO.Your.Namespace";
            var ietfLanguageTag = group.Key.ToLower();
            var className = string.Concat(char.ToUpper(ietfLanguageTag[0]), ietfLanguageTag.Substring(1));
            var translationBuilder = new StringBuilder();
            foreach (var translation in group.OrderBy(t => t.XPath))
            {
                translationBuilder.AppendFormat(@"                            new Translation{{ResourceKey = ""{0}"", Text = @""{1}""}},{2}", translation.XPath, translation.Content, Environment.NewLine);
            }
            var content = string.Format(TemplateFile, @namespace, className, ietfLanguageTag, translationBuilder);
            var filePath = string.Format(@"{0}\{1}.cs", OutputPath, className);
            File.WriteAllText(filePath, content);
        }
    }
}
