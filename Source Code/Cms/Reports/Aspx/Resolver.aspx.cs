using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
//test
namespace Reports.Aspx
{
    public partial class Resolver : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string Temp = Request.QueryString["R"];
            string OrigQueryString = Temp.Replace(" ", "+");

            string NewQueryString = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(OrigQueryString);

            Server.Transfer("ReportViewerNew.aspx?" + NewQueryString);
            //Response.Redirect("ReportViewer.aspx?" + NewQueryString);

        }
    }
}