using System.Collections.Generic;
using EPiServer.Templates.Alloy.LanguageTool.Domain;
using EPiServer.Templates.Alloy.LanguageTool.Extensions;
using EPiServer.Templates.Alloy.Models.Pages;

namespace EPiServer.Templates.Alloy.LanguageTool.Resources
{
    public class Sv : IInitialTranslations
    {
        private TranslationGroup[] _initialArray;
        public string IetfLanguageTag { get { return "sv"; } }

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
                                // Initial swedish article page translations goes here
                                new Translation{ResourceKey = "/some/sample/key", Text = "Svensk demotext"},
                                new Translation{ResourceKey = "/some/other/sample/key", Text = "En annan svensk demotext"},

                            }
                    };
                initials.SetupAndAdd(articlePage, IetfLanguageTag);

                var system = new TranslationGroup
                    {
                        Translations = new[]
                            {
                                // These are system translations used in this language tool
                                new Translation{ResourceKey = "/plugins/languagetool/plugin/title", Text = "Översättningar för"},
                                new Translation{ResourceKey = "/plugins/languagetool/plugin/description", Text = "Det här verktyget låter dig ändra på innehållstypspecifika texter; som t.ex. texter bundna till artikelsidtypen. För globala översättningar (sådana som inte är bundna till innehållstyper) vänligen använd verktyget i EPiServers globala meny. För att ändra på texter i andra språk, vänligen byt språkkontext."},
                                new Translation{ResourceKey = "/plugins/languagetool/tool/title", Text = "Översättningar för samtliga innehållstyper"},
                                new Translation{ResourceKey = "/plugins/languagetool/tool/description", Text = "Det här verktyget låter dig ändra på innehållstexter oavsett innehållstyp. För att ändra på texter i andra språk, vänligen byt språkkontext."},
                                new Translation{ResourceKey = "/plugins/languagetool/globaltranslations", Text = "Globala översättningar"},
                                new Translation{ResourceKey = "/plugins/languagetool/resourcekey", Text = "Översättningsnyckel"},
                                new Translation{ResourceKey = "/plugins/languagetool/text", Text = "Översättning"},
                                new Translation{ResourceKey = "/plugins/languagetool/update", Text = "Uppdatera"},
                                new Translation{ResourceKey = "/plugins/languagetool/created", Text = "Skapad"},
                                new Translation{ResourceKey = "/plugins/languagetool/updated", Text = "Ändrad"},
                                new Translation{ResourceKey = "/plugins/languagetool/updatedby", Text = "Ändrad av"},
                                new Translation{ResourceKey = "/plugins/languagetool/success", Text = "Översättningen uppdaterad."},
                                new Translation{ResourceKey = "/plugins/languagetool/errors/noid", Text = "FEL: Det finns inget id för den här översättningen."},
                                new Translation{ResourceKey = "/plugins/languagetool/errors/antiforgery", Text = "FEL: Antiforgery valideringen misslyckades."},
                                new Translation{ResourceKey = "/plugins/languagetool/errors/notguid", Text = "FEL: Id-värdet på den här översättningen är inte en Guid."},
                                new Translation{ResourceKey = "/plugins/languagetool/errors/notranslation", Text = "FEL: Det finns ingen översättning med detta id i databasen."},
                                
                                new Translation{ResourceKey = "/globalmenu/toolbox/label", Text = "Verktyg"},
                                new Translation{ResourceKey = "/globalmenu/toolbox/translations/label", Text = "Översättningar"},
                            }
                    };
                initials.SetupAndAdd(system, IetfLanguageTag);

                _initialArray = initials.ToArray();
                return _initialArray;
            }
        }
    }
}
