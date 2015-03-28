using System.Web;
using System.Web.Mvc;

namespace EPiServer.Templates.Alloy.LanguageTool.Infrastructure.AntiForgery
{
    public class AntiForgeryValidationService : IAntiForgeryValidationService
    {
        public bool IsValidToken(string token = "")
        {
            try
            {
                var tokenHeadersString = string.IsNullOrWhiteSpace(token) ?
                        HttpContext.Current.Request.Headers.Get("RequestVerificationToken") :
                        token;
                ValidateRequestHeaderString(tokenHeadersString);
            }
            catch (HttpAntiForgeryException)
            {
                return false;
            }
            return true;
        }

        public string TokenHeaderValue()
        {
            string cookieToken, formToken;
            System.Web.Helpers.AntiForgery.GetTokens(null, out cookieToken, out formToken);
            return string.Concat(cookieToken, ":", formToken);
        }

        private static void ValidateRequestHeaderString(string tokenHeadersString)
        {
            var cookieToken = string.Empty;
            var formToken = string.Empty;

            var tokens = tokenHeadersString.Split(':');
            if (tokens.Length == 2)
            {
                cookieToken = tokens[0].Trim();
                formToken = tokens[1].Trim();
            }

            System.Web.Helpers.AntiForgery.Validate(cookieToken, formToken);
        }
    }
}
