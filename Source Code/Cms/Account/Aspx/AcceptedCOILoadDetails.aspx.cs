using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cms.CmsWeb.Controls;
using Cms.CmsWeb;
using Cms.BusinessLayer.BlAccount;
using Cms.Model.Account;
using Model.Account;
using System.Threading;
using System.Globalization;
using System.Data;
using System.Reflection;
using System.Resources;

namespace Cms.Account.Aspx
{
    public partial class AcceptedCOILoadDetails : Cms.Account.AccountBase
    {
       
        protected System.Web.UI.WebControls.TreeView trViewScreenList;
        protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
        System.Resources.ResourceManager objResourceMgr;
        public string urlImport;
        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "538_3";
            SetCultureThread(GetLanguageCode());
            objResourceMgr = new System.Resources.ResourceManager("Cms.Account.Aspx.AcceptedCOILoadDetails", System.Reflection.Assembly.GetExecutingAssembly());
           
            if(!Page.IsPostBack)
            {
                setcaption();
                urlImport = Request.QueryString["IMPORT_REQUEST_ID"];
                hyper_application_Detail.HRef = "/Cms/Account/Aspx/AcceptedCOILoadApplicationIndex.aspx?pageMode=APP_P&IMPORT_REQUEST_ID=" + urlImport;
                hyper_Coverage_Detail.HRef = "/Cms/Account/Aspx/AcceptedCOILoadApplicationIndex.aspx?pageMode=COV_P&IMPORT_REQUEST_ID=" + urlImport;
                hyper_application_Detail_Exc.HRef = "/Cms/Account/Aspx/AcceptedCOILoadApplicationIndex.aspx?pageMode=APP_E&IMPORT_REQUEST_ID=" + urlImport;
                hyper_Coverage_Detail_Exc.HRef = "/Cms/Account/Aspx/AcceptedCOILoadApplicationIndex.aspx?pageMode=COV_E&IMPORT_REQUEST_ID=" + urlImport;
                hyper_Ins_Detail_Exc.HRef = "/Cms/Account/Aspx/AcceptedCOILoadApplicationIndex.aspx?pageMode=IST_E&IMPORT_REQUEST_ID=" + urlImport;
                hyper_Ins_Detail.HRef = "/Cms/Account/Aspx/AcceptedCOILoadApplicationIndex.aspx?pageMode=IST_P&IMPORT_REQUEST_ID=" + urlImport;
                hyper_Claim_Paid_Exc.HRef = "/Cms/Account/Aspx/AcceptedCOILoadApplicationIndex.aspx?pageMode=CLM_E&IMPORT_REQUEST_ID=" + urlImport;
                hyper_Claim_Paid.HRef = "/Cms/Account/Aspx/AcceptedCOILoadApplicationIndex.aspx?pageMode=CLM_P&IMPORT_REQUEST_ID=" + urlImport;
                hyper_Claim_Detl.HRef = "/Cms/Account/Aspx/AcceptedCOILoadApplicationIndex.aspx?pageMode=CLM_D_P&IMPORT_REQUEST_ID=" + urlImport;
                hyper_Claim_Detl_Exc.HRef ="/Cms/Account/Aspx/AcceptedCOILoadApplicationIndex.aspx?pageMode=CLM_D_E&IMPORT_REQUEST_ID=" + urlImport;
            }
        }
        private void setcaption()
        {
            hyper_application_Detail.InnerText = objResourceMgr.GetString("hyper_application_Detail");
            hyper_Coverage_Detail.InnerText = objResourceMgr.GetString("hyper_Coverage_Detail");
            hyper_application_Detail_Exc.InnerText = objResourceMgr.GetString("hyper_application_Detail_Exc");
            hyper_Coverage_Detail_Exc.InnerText = objResourceMgr.GetString("hyper_Coverage_Detail_Exc");
            hyper_Ins_Detail.InnerText = objResourceMgr.GetString("hyper_Ins_Detail");
            hyper_Ins_Detail_Exc.InnerText = objResourceMgr.GetString("hyper_Ins_Detail_Exc");
            capHead.Text = objResourceMgr.GetString("capHead");
            capProcessedRecord.Text = objResourceMgr.GetString("capProcessedRecord");
            capExceptionDetail.Text = objResourceMgr.GetString("capExceptionDetail");
            hyper_Claim_Detl_Exc.InnerText = objResourceMgr.GetString("hyper_Claim_Detl_Exc");
            hyper_Claim_Detl.InnerHtml = objResourceMgr.GetString("hyper_Claim_Detl_Exc");
            hyper_Claim_Paid.InnerHtml = objResourceMgr.GetString("hyper_Claim_Paid");
            hyper_Claim_Paid_Exc.InnerHtml = objResourceMgr.GetString("hyper_Claim_Paid");
        }
    }
}
