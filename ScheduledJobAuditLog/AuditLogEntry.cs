using System;
using EPiServer.Data.Dynamic;

namespace EPiServer.CodeSample.ScheduledJobAuditLog
{
    [EPiServerDataStore(
        StoreName = "AuditLogEntry",
        AutomaticallyCreateStore = true,
        AutomaticallyRemapStore = true
    )]
    public class AuditLogEntry
    {
        public string PluginId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public JobAction Action { get; set; }
        public string Message { get; set; }

        public AuditLogEntry()
        {
            Timestamp = DateTime.Now;
            Username = EPiServer.Security.PrincipalInfo.CurrentPrincipal.Identity.Name;
            Email = EmailFor(Username);
        }

        private static string EmailFor(string username)
        {
            var profile = EPiServer.Personalization.EPiServerProfile.Get(username);
            return profile.EmailWithMembershipFallback ?? "N/A";
        }
    }
}
