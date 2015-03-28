using System.Collections.Generic;
using TODO.Your.Namespace.LanguageTool.Domain;
using TODO.Your.Namespace.LanguageTool.Extensions;

namespace TODO.Your.Namespace.LanguageTool.Resources
{
    public class En : IInitialTranslations
    {
        private TranslationGroup[] _initialArray;
        public string IetfLanguageTag { get { return "en"; } }

        public TranslationGroup[] Initials
        {
            get
            {
                if (_initialArray != null)
                {
                    return _initialArray;
                }
                var initials = new List<TranslationGroup>();
                
                var group = new TranslationGroup
                    {
                        // ContentTypeName = typeof(ArticlePage).Name, // TODO - Split group up and assign content type accordingly; this is an example.
                        Translations = new[]
                        {
                            new Translation{ResourceKey = "/articlepage/relatedlinks/title", Text = @"Related links"},
                            new Translation{ResourceKey = "/articlepage/share/share", Text = @"Share"},
                            new Translation{ResourceKey = "/articlepage/share/titles/facebook", Text = @"Facebook"},
                            new Translation{ResourceKey = "/articlepage/share/titles/googleplus", Text = @"Google+"},
                            new Translation{ResourceKey = "/articlepage/share/titles/linkedin", Text = @"LinkedIn"},
                            new Translation{ResourceKey = "/articlepage/share/titles/twitter", Text = @"Twitter"},
                            new Translation{ResourceKey = "/startpage/quicklinks/label", Text = @"Quick links"},
                            new Translation{ResourceKey = "/startpage/quicklinks/sometext", Text = @"Some interesting text"},
                        }
                    };
                initials.SetupAndAdd(group, IetfLanguageTag);

                _initialArray = initials.ToArray();
                throw new System.Exception("You did not go through your generated language files and follow the instructions.");
                return _initialArray;
            }
        }
    }
}
