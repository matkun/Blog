using System;
using System.Web.UI.WebControls;

namespace SmallSample
{
    public partial class Sample : System.Web.UI.Page
    {
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            // Save the contact information somewhere.
            var name = tbName.Text;
            var email = tbEmail.Text;
            var phone = tbPhone.Text;

            // and create a link to something.
            var link = new HyperLink
            {
                Text = "A download link",
                NavigateUrl = "/somepath/mypdf.pdf"
            };

            phPDFLink.Controls.Add(link);
            phPDFLink.Visible = true;
            phPDFLink.DataBind();
        }
    }
}
