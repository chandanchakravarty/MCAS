using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Text;
using Ebix.DataTypes;
//using DataParser;

namespace Cms.Account.Aspx
{
    public partial class PremiumRefundCheckCreate : Cms.CmsWeb.cmsbase 
    {
       // string UserCulture = DataParser.Base
        protected void Page_Load(object sender, EventArgs e)
        {
            if (AdvCommon.ClsCommon.CONNECTION_STRING == null || AdvCommon.ClsCommon.CONNECTION_STRING == "")
            {
                AdvCommon.ClsCommon.CONNECTION_STRING = Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr;
            }
            if (!IsPostBack)
            {
                lnkCSS.Href = "/cms/cmsweb/css/css" + GetColorScheme() + ".css";
                RefundChkLister.DBConnectionString = AdvCommon.ClsCommon.CONNECTION_STRING;
                RefundChkLister.QueryXMLPath = "http://localhost/cms/Account/Support/PageXml/ALBA/PremiumRefundCheck.xml";
                //D:\Projects\EAWBR\Development\Source Code\Cms\Account\Support\PageXml\ALBA\PremiumRefundCheck.xml
                RefundChkLister.DataBind();
            }
        }
        protected void btnCreate_Click(object sender, EventArgs e)
        {

        }
    }
}