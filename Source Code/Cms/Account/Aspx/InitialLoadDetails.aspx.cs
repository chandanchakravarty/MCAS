/******************************************************************************************
<Author					: -   Sneha
<Start Date				: -	17/08/2011 
<End Date				: -	
<Description			: - 	
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: -
*******************************************************************************************/
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
    public partial class InitialLoadDetails : Cms.Account.AccountBase
    {
        protected System.Web.UI.WebControls.TreeView trViewScreenList;
        protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
        System.Resources.ResourceManager objResourceMgr;
        public string urlImport;
        public string IMPORT_FILE_TYPE;
        public string IMPORT_FILE_TYPE_NAME;
        public string REQUEST_STATUS;
        public string lobId;
        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "551_2";
            SetCultureThread(GetLanguageCode());
            objResourceMgr = new System.Resources.ResourceManager("Cms.Account.Aspx.InitialLoadDetails", System.Reflection.Assembly.GetExecutingAssembly());

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["REQUEST_STATUS"] != null && Request.QueryString["REQUEST_STATUS"].ToString() != "")
                    REQUEST_STATUS = Request.QueryString["REQUEST_STATUS"];

                if (Request.QueryString["IMPORT_REQUEST_ID"] != null && Request.QueryString["IMPORT_REQUEST_ID"].ToString() != "")
                    urlImport = Request.QueryString["IMPORT_REQUEST_ID"];
                if (Request.QueryString["Import_File_Type"] != null && Request.QueryString["Import_File_Type"].ToString() != "")
                    IMPORT_FILE_TYPE = Request.QueryString["Import_File_Type"];
                if (Request.QueryString["Import_File_Type_Name"] != null && Request.QueryString["Import_File_Type_Name"].ToString() != "")
                   hidIMPORT_FILE_TYPE_NAME.Value= Request.QueryString["Import_File_Type_Name"];
                   IMPORT_FILE_TYPE_NAME=hidIMPORT_FILE_TYPE_NAME.Value;
               if (Request.QueryString["lobId"] != null && Request.QueryString["lobId"].ToString() != "")
                       lobId = Request.QueryString["lobId"];

                setcaption();
                hyper_ExceptionDetail.HRef = "/Cms/Account/Aspx/InitialLoadApplicationIndex.aspx?pageMode=" + IMPORT_FILE_TYPE + "_E" + "&IMPORT_REQUEST_ID=" + urlImport + "&IMPORT_FILE_TYPE_NAME=" + IMPORT_FILE_TYPE_NAME + "&lobId=" + lobId;
                hyper_ProcessedRecord.HRef = "/Cms/Account/Aspx/InitialLoadApplicationIndex.aspx?pageMode=" + IMPORT_FILE_TYPE + "_P" + "&IMPORT_REQUEST_ID=" + urlImport + "&IMPORT_FILE_TYPE_NAME=" + IMPORT_FILE_TYPE_NAME + "&lobId=" + lobId;

                if (REQUEST_STATUS == "WTCHS")
                {
                    TdProccessedRecordLink.Visible = false;
                    hidtrue.Value = "false";
                }
                else
                {
                    TdProccessedRecordLink.Visible = true;
                    hidtrue.Value = "true";
                }

                hidStatus.Value = REQUEST_STATUS;
            }
        }
        private void setcaption()
        {
            hyper_ExceptionDetail.InnerText = objResourceMgr.GetString("hyper_ExceptionDetail");
            hyper_ProcessedRecord.InnerText = objResourceMgr.GetString("hyper_ProcessedRecord");
            capHead.Text = hidIMPORT_FILE_TYPE_NAME.Value+' '+ objResourceMgr.GetString("capHead");
        }
    }
}
