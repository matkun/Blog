using System.ComponentModel;

namespace EPiServer.CodeSample.ScheduledJobAuditLog
{
    public enum JobAction
    {
        [Description("Saved settings")]
        SavedSettings,

        [Description("Manually started")]
        StartedManually,

        [Description("Manually stopped")]
        StoppedManually
    }
}
