using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cms.Policies.Aspx
{
    public partial class Default :  Cms.CmsWeb.cmsbase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["CUSTOMER_ID"] != null && Request.QueryString["CUSTOMER_ID"].ToString() != "")
            {
                hidCUSTOMER_ID.Value = Request.QueryString["CUSTOMER_ID"].ToString();
            }
            if (Request.QueryString["POLICY_ID"] != null && Request.QueryString["POLICY_ID"].ToString() != "")
            {
                hidPOLICY_ID.Value = Request.QueryString["POLICY_ID"].ToString();
            }
            if (Request.QueryString["POLICY_VERSION_ID"] != null && Request.QueryString["POLICY_VERSION_ID"].ToString() != "")
            {
                hidPOLICY_VERSION_ID.Value = Request.QueryString["POLICY_VERSION_ID"].ToString();
            }
            if (Request.QueryString["LOB_ID"] != null && Request.QueryString["LOB_ID"].ToString() != "")
            {
                hidLOB_ID.Value = Request.QueryString["LOB_ID"].ToString();
            }
            if (Request.QueryString["Lob_String"] != null && Request.QueryString["Lob_String"].ToString() != "")
            {
                hidLOB_String.Value = Request.QueryString["Lob_String"].ToString();
            }
        }
    }
}