using System.Linq;
using System.Web.UI;

namespace EPiServer.CodeSample.ScheduledJobAuditLog
{
    public static class ControlExtensions
    {
        public static Control FindControlRecursively(this Control root, string controlId)
        {
            if (controlId.Equals(root.ID))
            {
                return root;
            }
            return (from Control control in root.Controls
                    select FindControlRecursively(control, controlId))
                .FirstOrDefault(c => c != null);
        }
    }
}
