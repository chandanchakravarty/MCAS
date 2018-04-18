/******************************************************************************************
<Author				: - Pradeep Kushwaha
<Start Date			: -	10-Nov-2011
<End Date			: -	
<Description		: - Display Initial load commited policy details list
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: -  
<Modified By		: -   
<Purpose			: -   
*******************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.IO;
using Cms.CmsWeb.WebControls;
using System.Reflection;
using System.Resources;
using Cms.BusinessLayer.BlCommon;

namespace Cms.Account.Aspx
{
    public partial class InitialLoadCommitedPolicyList : Cms.Account.AccountBase
    {
        ResourceManager objResourceMgr = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "551_2";
            SetCultureThread(GetLanguageCode());
            objResourceMgr = new System.Resources.ResourceManager("Cms.Account.Aspx.InitialLoadCommitedPolicyList", System.Reflection.Assembly.GetExecutingAssembly());
            string IMPORT_FILE_TYPE = string.Empty;
            string IMPORT_REQUEST_ID = string.Empty;
            if (!Page.IsPostBack)
            {
                
                if (Request.QueryString["IMPORT_REQUEST_ID"] != null && Request.QueryString["IMPORT_REQUEST_ID"].ToString() != "")
                    IMPORT_REQUEST_ID = Request.QueryString["IMPORT_REQUEST_ID"];
                
                setcaption();
                hyper_ExceptionDetail.HRef = "/Cms/Account/Aspx/InitialLoadCommitedPolicyDetail.aspx?pageMode=EX"  + "&IMPORT_REQUEST_ID=" + IMPORT_REQUEST_ID;
                hyper_ProcessedRecord.HRef = "/Cms/Account/Aspx/InitialLoadCommitedPolicyDetail.aspx?pageMode=PR"  + "&IMPORT_REQUEST_ID=" + IMPORT_REQUEST_ID;


            }//if (!Page.IsPostBack)

        }//protected void Page_Load(object sender, EventArgs e)
        private void setcaption()
        {
            hyper_ExceptionDetail.InnerText = objResourceMgr.GetString("hyper_ExceptionDetail");
            hyper_ProcessedRecord.InnerText = objResourceMgr.GetString("hyper_ProcessedRecord");
            capHead.Text = hidIMPORT_FILE_TYPE_NAME.Value + ' ' + objResourceMgr.GetString("capHead");
        }//private void setcaption()
    }
}