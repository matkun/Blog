using System.Collections.Generic;
using EPiServer.Templates.Alloy.LanguageTool.Domain;
using EPiServer.Templates.Alloy.LanguageTool.Extensions;
using EPiServer.Templates.Alloy.Models.Pages;

namespace EPiServer.Templates.Alloy.LanguageTool.Resources
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
                
                var articlePage = new TranslationGroup
                    {
                        ContentTypeName = typeof(ArticlePage).Name, // Translations editable on pages of type ArticlePage.
                        Translations = new[]
                            {
                                // Initial english article page translations goes here
                                new Translation{ResourceKey = "/some/sample/key", Text = "English demo text"},
                                new Translation{ResourceKey = "/some/other/sample/key", Text = "Another English demo text"},

                            }
                    };
                initials.SetupAndAdd(articlePage, IetfLanguageTag);

                var system = new TranslationGroup
                    {
                        Translations = new[]
                            {
                                // These are system translations used in this language tool
                                new Translation{ResourceKey = "/plugins/languagetool/plugin/title", Text = "Translations for"},
                                new Translation{ResourceKey = "/plugins/languagetool/plugin/description", Text = "This tool allows you to change content type specific texts; like for instance those connected to the ArticlePage. For global translations (those not connected to a content type), please refer to the tool in EPiServers global menu. To change texts in other languages, please change language context."},
                                new Translation{ResourceKey = "/plugins/languagetool/tool/title", Text = "Translations for all content types"},
                                new Translation{ResourceKey = "/plugins/languagetool/tool/description", Text = "This tool allows you to change language specific texts regardless of content type. To change texts in other languages, please change language context."},
                                new Translation{ResourceKey = "/plugins/languagetool/globaltranslations", Text = "Global translations"},
                                new Translation{ResourceKey = "/plugins/languagetool/resourcekey", Text = "Translation key"},
                                new Translation{ResourceKey = "/plugins/languagetool/text", Text = "Translation"},
                                new Translation{ResourceKey = "/plugins/languagetool/update", Text = "Update"},
                                new Translation{ResourceKey = "/plugins/languagetool/created", Text = "Created"},
                                new Translation{ResourceKey = "/plugins/languagetool/updated", Text = "Updated"},
                                new Translation{ResourceKey = "/plugins/languagetool/updatedby", Text = "Updated by"},
                                new Translation{ResourceKey = "/plugins/languagetool/success", Text = "Translation updated successfully."},
                                new Translation{ResourceKey = "/plugins/languagetool/errors/noid", Text = "ERROR: No id value present for this translation."},
                                new Translation{ResourceKey = "/plugins/languagetool/errors/antiforgery", Text = "ERROR: Antiforgery validation failed."},
                                new Translation{ResourceKey = "/plugins/languagetool/errors/notguid", Text = "ERROR: Id value for this translation is not a Guid."},
                                new Translation{ResourceKey = "/plugins/languagetool/errors/notranslation", Text = "ERROR: No translation with this id in the database."},
                                
                                new Translation{ResourceKey = "/globalmenu/toolbox/label", Text = "Toolbox"},
                                new Translation{ResourceKey = "/globalmenu/toolbox/translations/label", Text = "Translations"},
                            }
                    };
                initials.SetupAndAdd(system, IetfLanguageTag);

                _initialArray = initials.ToArray();
                return _initialArray;
            }
        }
    }
}
