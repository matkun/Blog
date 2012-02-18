using System.Linq;
using System.Web.UI;

namespace PageTypeTreeFilter
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