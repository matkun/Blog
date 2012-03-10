using System.Net;

namespace Lemonwhale.Core.AdapterPattern
{
    public class WebClientWrapper : IWebClientWrapper
    {
        private readonly WebClient _webClient;
        public WebClientWrapper()
        {
            _webClient = new WebClient();
        }

        public string DownloadString(string address)
        {
            return _webClient.DownloadString(address);
        }
    }
}
