using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cms.XmlManager.Aspx
{
    public partial class XmlMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("Default.aspx", true);
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            Response.Redirect("PreviewXML.aspx", true);
        }
    }
}