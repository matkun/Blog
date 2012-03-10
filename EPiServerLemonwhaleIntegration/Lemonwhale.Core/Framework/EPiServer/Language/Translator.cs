using EPiServer.Core;

namespace Lemonwhale.Core.Framework.EPiServer.Language
{
    public class Translator : ITranslator
    {
        private static ITranslator _instance;
        public static ITranslator Instance
        {
            get { return _instance ?? (_instance = new Translator()); }
            set { _instance = value; }
        }

        public string Translate(string xPath)
        {
            return LanguageManager.Instance.Translate(xPath);
        }
    }
}
