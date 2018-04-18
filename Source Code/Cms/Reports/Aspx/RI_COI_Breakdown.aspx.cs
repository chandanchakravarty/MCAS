using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Cms.DataLayer;
using Cms.CmsWeb;
using System.Resources;
using System.Reflection;
using System.Configuration;

namespace Cms.Reports.Aspx
{
    public partial class RI_COI_Breakdown : Cms.CmsWeb.cmsbase
    {
        System.Resources.ResourceManager objResourceMgr;
        protected System.Web.UI.WebControls.Panel Panel1;
        protected System.Web.UI.WebControls.Label lblRI;
        protected System.Web.UI.WebControls.DropDownList lstHierarchy;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicy;
        protected System.Web.UI.WebControls.TextBox txtRI_COI;
        protected System.Web.UI.HtmlControls.HtmlImage imgAGENCY_NAME;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidHierarchySelected;
        protected System.Web.UI.WebControls.Label lblHierarchy;
        protected Cms.CmsWeb.Controls.CmsButton btnReport;
       // protected System.Web.UI.WebControls.Label header_field;

       // protected System.Web.UI.HtmlControls.HtmlTableCell header;
        public string URL;
        protected void Page_Load(object sender, EventArgs e)
        {
            SetCultureThread(GetLanguageCode());
            
            objResourceMgr = new System.Resources.ResourceManager("Cms.Reports.Aspx.RI_COI_Breakdown", System.Reflection.Assembly.GetExecutingAssembly());
            if (!Page.IsPostBack)
            {
                SetCaptions();
            }
            base.ScreenId = "530";
            btnReport.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Read;
            btnReport.PermissionString = gstrSecurityXML;
            URL = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL();
            hidPolicy.Value  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1162");
        }

        protected void txtPOLICY_NUMBER_TextChanged(object sender, EventArgs e)
        {

        }

        private void SetCaptions()
        {

            //header.v = objResourceMgr.GetString("txtCUSTOMER_PARENT");
            lblheader_field.Text = objResourceMgr.GetString("lblheader_field");
            btnReport.Text = objResourceMgr.GetString("btnReport");
            lblRI.Text = objResourceMgr.GetString("lblRI");
        }
       
    }
}