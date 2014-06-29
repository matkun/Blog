using EPiServer.Core;

namespace EPiServer.Plugins.LanguageFileEditor
{
    public static class Path
    {
        public static string To(string filename)
        {
            return string.Concat(LanguageManager.Instance.Directory, System.IO.Path.DirectorySeparatorChar, filename);
        }
    }
}
