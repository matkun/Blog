using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.Adapters;
using System.Web.UI.HtmlControls;
using EPiServer.UI.Edit;
using EPiServer.UI.WebControls;

namespace EPiServer.CodeSample
{
    public class EditPageControlAdapter : ControlAdapter
    {
        private readonly List<string> _hiddenProperties = new List<string>();
        private readonly string[] _allowedRoles = { "Administrators", "SuperUsers" };

        protected override void OnInit(EventArgs e)
        {
            if (!_allowedRoles.Any(role => EPiServer.Security.PrincipalInfo.CurrentPrincipal.IsInRole(role)))
            {
                base.OnInit(e);
                return;
            }

            var editPageControl = (EditPageControl) Control;
            foreach (var propertyData in editPageControl.CurrentPage.Property)
            {
                if (propertyData.DisplayEditUI) continue;

                propertyData.DisplayEditUI = true;
                _hiddenProperties.Add(string.Format("{0} ({1})", propertyData.TranslateDisplayName(), propertyData.Name));
            }
            base.OnInit(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (!_hiddenProperties.Any()) return;

            var names = string.Join(", ", _hiddenProperties);
            var message = new HtmlGenericControl("div")
                {
                    InnerText = string.Concat("The following properties are hidden for normal editors: ", names)
                };
            message.Attributes.Add("style", "color:red;margin-top:5px;margin-left:5px;");
            var editForm = Control.FindControl("EditForm") as PropertyDataForm;
            editForm.Controls.AddAt(0, message);
        }
    }
}
