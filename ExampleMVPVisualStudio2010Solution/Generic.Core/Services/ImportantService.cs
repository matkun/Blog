namespace Generic.Core.Services
{
    public class ImportantService : IImportantService
    {
        public string GetImportantHeading(string text)
        {
            return string.IsNullOrEmpty(text) ?
                "Sad text.." :
                string.Concat("Hello ", text);
        }

        public string GetImportantText(string text)
        {
            return string.IsNullOrEmpty(text) ?
                "SAD TEXT.." :
                string.Concat("HELLO ", text);
        }
    }
}
