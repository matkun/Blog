using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using NDesk.Options;

namespace LanguageScanner
{
    class Program
    {
        private static readonly Stopwatch StopWatch = new Stopwatch();
        private static string[] _excludeExtensions =
        {
            "dll", "xml", "pdb", "csproj", "designer.cs", "config", "cache", "swf", "css",
            "js", "jpg", "gif", "png", "bmp", "psd", "html", "orig", "txt", "fla", "pdf"
        };
        private static bool _ignoreCase = false;
        private static bool _distinctTranslations = false;

        static void Main(string[] args)
        {
            var rootPath = Directory.GetCurrentDirectory();
            var langPath = string.Concat(Directory.GetCurrentDirectory(), @"\lang");
            var printHelpAndExit = false;

            var options = new OptionSet
                          {
                              { "rp|rootPath=", string.Format("Path to the directory containing your source code; default is '{0}'", rootPath), arg => rootPath = arg },
                              { "lp|langPath=", string.Format("Path to the directory containing your language files; default is '{0}'", langPath), arg => langPath = arg },
                              { "ee|excludeExtensions=", string.Format("File extensions to exclude in code scan; default are {0}", string.Join(",", _excludeExtensions)), arg => _excludeExtensions = arg == null ? _excludeExtensions : arg.Split(',') },
                              { "ic|ignoreCase", string.Format("Makes the scan case insensitive, increases working time; default is '{0}'", _ignoreCase.ToString().ToLower()), arg => _ignoreCase = arg != null },
                              { "dt|distinctTranslations", string.Format("Remove duplicate XPaths, scan faster but file list will be inaccurate; default is '{0}'", _distinctTranslations), arg => _distinctTranslations = arg != null },
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
                StopWatch.Start();

                Console.Out.WriteLine("--- Starting to scan language files for translations ---");
                var translations = ScanLanguageFilesIn(langPath).ToList();
                var langPathTime = StopWatch.Elapsed;
                Console.Out.WriteLine("Language files scanned in: {0}", langPathTime.ToString("g"));
                Console.Out.WriteLine("Number of translations:    {0}", translations.Count());

                Console.Out.WriteLine("--- Starting to retrieve file paths for code files ---");
                var filePaths = FilePathsIn(rootPath).ToArray();
                var getFilePathsTime = StopWatch.Elapsed;
                Console.Out.WriteLine("File paths retrieved in: {0}", getFilePathsTime.ToString("g"));
                Console.Out.WriteLine("Number of file paths:    {0}", filePaths.Count());

                Console.Out.WriteLine("--- Starting to scan for orphans ---");
                
                var possibleOrphans = translations.ConvertAll(t=>t);
                foreach (var path in filePaths)
                {
                    Console.Out.WriteLine("Starting to scan file: {0}", path);
                    var content = _ignoreCase ? File.ReadAllText(path).ToLower() : File.ReadAllText(path);

                    var usedTranslations = translations.Where(translation => content.Contains(translation.XPath));
                    foreach (var translation in usedTranslations)
                    {
                        possibleOrphans.Remove(translation);
                    }

                    Console.Out.WriteLine("Done! Possible orphas now: {0}", possibleOrphans.Count());
                }

                var scanCompleteTime = StopWatch.Elapsed;
                StopWatch.Stop();

                foreach (var orphan in possibleOrphans)
                {
                    Console.Out.WriteLine("{0}: {1} ({2})", orphan.File, orphan.XPath, orphan.Content);
                }
                Console.Out.WriteLine("**************************************");
                Console.Out.WriteLine("Language files scanned in: {0}", langPathTime.ToString("g"));
                Console.Out.WriteLine("File paths retrieved in:   {0}", getFilePathsTime.ToString("g"));
                Console.Out.WriteLine("Scan compleated in:        {0}", scanCompleteTime.ToString("g"));
                Console.Out.WriteLine("Possible orphans found:    {0}", possibleOrphans.Count);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
        }

        private static IEnumerable<string> FilePathsIn(string currentDirectory)
        {
            var paths = Directory.GetFiles(currentDirectory)
                .Where(file => !ShouldExclude(file))
                .ToList();
            foreach (var subDirectory in Directory.GetDirectories(currentDirectory))
            {
                paths.AddRange(FilePathsIn(subDirectory));
            }
            return paths;
        }

        private static bool ShouldExclude(string file)
        {
            return _excludeExtensions.Any(extension => file.ToLower().EndsWith(extension.ToLower()));
        }

        private static IEnumerable<Translation> ScanLanguageFilesIn(string langPath)
        {
            var translations = new List<Translation>();
            var paths = Directory.GetFiles(langPath)
                .Where(file => file.EndsWith(".xml"))
                .ToArray();
            foreach (var file in paths)
            {
                translations.AddRange(TranslationsIn(file));
            }
            return _distinctTranslations ? translations.Distinct() : translations;
        }

        private static IEnumerable<Translation> TranslationsIn(string file)
        {
            var doc = XDocument.Load(file);
            var leaves = from e in doc.Descendants() where !e.Elements().Any() select e;
            return leaves
                .Select(leaf => new Translation
                                        {
                                            File = file,
                                            XPath = _ignoreCase ? leaf.XPath().ToLower() : leaf.XPath(),
                                            Content = leaf.Value
                                        });
        }
    }
}
