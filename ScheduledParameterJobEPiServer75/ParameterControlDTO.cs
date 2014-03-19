using System.Web.UI;

namespace EPiServer.Templates.Alloy.ScheduledParameterJob
{
    public class ParameterControlDTO
    {
        public string Id { get { return Control.ID; } }
        public bool ShowLabel { get { return !string.IsNullOrEmpty(LabelText); } }

        public string LabelText { get; set; }
        public string Description { get; set; }
        public Control Control { get; set; }
    }
}
