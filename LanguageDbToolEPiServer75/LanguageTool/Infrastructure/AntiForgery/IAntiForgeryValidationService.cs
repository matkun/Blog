namespace EPiServer.Templates.Alloy.LanguageTool.Infrastructure.AntiForgery
{
    public interface IAntiForgeryValidationService
    {
        bool IsValidToken(string token = "");
        string TokenHeaderValue();
    }
}
