using System.Web.UI;

namespace EPiServer.CodeSample.BookmarkLinks
{
    public static class ControlExtensions
    {
        public static T FindControlRecursive<T>(this Control control, string id) where T : Control
        {
            var controlTest = control as T;

            if (controlTest != null && (id == null || controlTest.ID.Equals(id)))
            {
                return controlTest;
            }

            foreach (Control ctrl in control.Controls)
            {
                controlTest = FindControlRecursive<T>(ctrl, id);
                if (controlTest != null)
                {
                    return controlTest;
                }
            }

            return null;
        }
    }
}
