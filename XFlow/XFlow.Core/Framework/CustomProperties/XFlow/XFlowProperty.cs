using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EPiServer.Core;
using EPiServer.PlugIn;

namespace XFlow.Core.CustomProperties.XFlow
{
    [Serializable]
    [PageDefinitionTypePlugIn(DisplayName = "XFlow")]
    public class XFlowProperty : EPiServer.Core.PropertyString
    {
        public override IPropertyControl CreatePropertyControl()
        {
            return new XFlowPropertyType();
        }
    }
    public class XFlowPropertyType : EPiServer.Web.PropertyControls.PropertyTextBoxControlBase
    {
        protected override void SetupEditControls()
        {
            base.SetupEditControls();
            TextBox inputControl = this.EditControl;
            //inputControl.Width = 100;  
            inputControl.Attributes.Add("class", "episize240");
            inputControl.Attributes.Add("onfocus", "blur();");
            inputControl.Text = this.PropertyData.Value as string;
            Control ctrl = inputControl.Parent;
            Literal js = new Literal();
            js.Text = "<script>function addValue(val){ document.getElementById('" + inputControl.ClientID + "').value = val;}</script>";
            ctrl.Controls.Add(js);
            HtmlButton btn = new HtmlButton();
            btn.InnerText = "...";
            btn.Attributes.Add("onclick", "window.open('/Default.aspx?selectedvalue=' + document.getElementById('" + inputControl.ClientID + "').value,'namn','top=300,left=300,width=450,height=400,resizable=yes,status=yes');");
            ctrl.Controls.Add(btn);
        }
    }  
}
