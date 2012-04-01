using System.Linq;
using System.Web.UI;

namespace XFlow.Core.Extensions
{
    public static class ControlExtensions
    {
        public static Control FindControlRecursive(this Control root, string controlId)
        {
            if (controlId.Equals(root.ID))
            {
                return root;
            }
            return (from Control control in root.Controls
                    select FindControlRecursive(control, controlId))
                .FirstOrDefault(c => c != null);
        }
    }
}
