using System.Linq;
using System.Web.UI;

namespace PageTypeTreeFilter.Extensions
{
    public static class ControlExtensions
    {
        public static T FindParentControlOfType<T>(this Control control) where T : Control
        {
            var parent = control.Parent;
            if (parent == null)
            {
                return null;
            }
            if (parent is T)
            {
                return (T) parent;
            }
            return parent.FindParentControlOfType<T>();
        }

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
