namespace EPiServer.Templates.Alloy.LanguageTool.Wrappers
{
    public interface IUrlRewriteProviderWrapper
    {
        bool ConvertToInternal(UrlBuilder urlBuilder, out object internalObject);
    }
}
