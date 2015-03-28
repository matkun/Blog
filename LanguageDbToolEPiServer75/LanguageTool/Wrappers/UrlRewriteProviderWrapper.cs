namespace EPiServer.Templates.Alloy.LanguageTool.Wrappers
{
    public class UrlRewriteProviderWrapper : IUrlRewriteProviderWrapper
    {
        public bool ConvertToInternal(UrlBuilder urlBuilder, out object internalObject)
        {
            return Global.UrlRewriteProvider.ConvertToInternal(urlBuilder, out internalObject);
        }
    }
}
