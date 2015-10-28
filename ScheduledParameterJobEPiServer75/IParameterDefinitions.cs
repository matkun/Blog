using System.Collections.Generic;
using System.Web.UI;

namespace ScheduledParameterJobEPiServer75
{
    public interface IParameterDefinitions
    {
        IEnumerable<ParameterControlDTO> GetParameterControls();
        void SetValue(Control control, object value);
        object GetValue(Control control);
    }
}
