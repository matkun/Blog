using EPiServer.Framework;
using EPiServer.Framework.Initialization;

namespace EPiServer.Checksums
{
    [InitializableModule]
    public class ChecksumVerificationModule : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            ChecksumService.VerifyConfigs();
        }

        public void Uninitialize(InitializationEngine context) { }
        public void Preload(string[] parameters) { }
    }
}
