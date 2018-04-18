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

namespace Reports.Aspx
{
    public class EodProcesslog : Cms.CmsWeb.cmsbase
    {
        protected System.Web.UI.WebControls.Label lblExpirationStartDate;
        protected System.Web.UI.WebControls.TextBox txtExpirationStartDate;
        protected System.Web.UI.WebControls.HyperLink hlkExpirationStartDate;
        protected System.Web.UI.WebControls.RegularExpressionValidator revExpirationStartDate;
        protected System.Web.UI.WebControls.CompareValidator cmpExpirationDate;
        protected System.Web.UI.WebControls.Label lblExpirationEndDate;
        protected System.Web.UI.WebControls.TextBox txtExpirationEndDate;
        protected System.Web.UI.WebControls.HyperLink hlkExpirationEndDate;
        protected System.Web.UI.WebControls.RegularExpressionValidator revExpirationEndDate;
        protected System.Web.UI.WebControls.Label lblVendor;
        protected System.Web.UI.WebControls.DropDownList lstVendorList;
        protected System.Web.UI.WebControls.Label lblStatus;
        protected System.Web.UI.WebControls.ListBox lstActivity;
        protected System.Web.UI.WebControls.DropDownList lstStatusList;
        protected System.Web.UI.WebControls.Panel Panel1;
        protected System.Web.UI.WebControls.Label lblHeader;
        protected System.Web.UI.WebControls.Label lblActivity;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidLandId; // .Label lblActivity;

        System.Resources.ResourceManager objResourceMgr;
        protected Cms.CmsWeb.Controls.CmsButton btnReport;

        private void Page_Load(object sender, System.EventArgs e)
        {
            hlkExpirationStartDate.Attributes.Add("OnClick", "fPopCalendar(document.forms[0].txtExpirationStartDate,document.forms[0].txtExpirationStartDate)");
            hlkExpirationEndDate.Attributes.Add("OnClick", "fPopCalendar(document.forms[0].txtExpirationEndDate,document.forms[0].txtExpirationEndDate)");
            revExpirationStartDate.ValidationExpression = aRegExpDate;
            revExpirationEndDate.ValidationExpression = aRegExpDate;
            revExpirationStartDate.ErrorMessage = ClsMessages.FetchGeneralMessage("1380");
            revExpirationEndDate.ErrorMessage = ClsMessages.FetchGeneralMessage("1380");
            cmpExpirationDate.ErrorMessage = ClsMessages.FetchGeneralMessage("447"); //Added by Aditya for TFS BUG # 368
            Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.Text, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            base.ScreenId = "379";
            btnReport.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Read;
            btnReport.PermissionString = gstrSecurityXML;
            objResourceMgr = new System.Resources.ResourceManager("Reports.Aspx.EodProcesslog", System.Reflection.Assembly.GetExecutingAssembly());
            btnReport.Attributes.Add("onClick", "ShowReport();return false;");
            string strLang_id = GetLanguageID();
            if (!IsPostBack)
            {
                if (GetLanguageID() != "")
                    hidLandId.Value = GetLanguageID();
                SetCaption();
                try
                {

                    DataSet ds1 = objDataWrapper.ExecuteDataSet("SELECT EAM.ACTIVITY_ID as ID,ISNULL(EAMM.ACTIVITY_DESCRIPTION,EAM.ACTIVITY_DESCRIPTION)as NAME  FROM EOD_ACTIVITY_MASTER EAM WITH(NOLOCK)LEFT JOIN EOD_ACTIVITY_MASTER_MULTILINGUAL EAMM WITH(NOLOCK)  ON EAM.ACTIVITY_ID=EAMM.ACTIVITY_ID AND EAMM.LANG_ID = " + strLang_id + "  WHERE EAM.PARENT_ACTIVITY_ID IS  NULL");
                    lstVendorList.DataSource = ds1.Tables[0];
                    lstVendorList.DataTextField = "NAME";
                    lstVendorList.DataValueField = "ID";
                    lstVendorList.DataBind();


                    //DataSet ds1 = objDataWrapper.ExecuteDataSet("Select ISNull(COMPANY_NAME,'') + '-' + ISNULL(VENDOR_CODE,'') + '-' + isnull(VENDOR_ACC_NUMBER,'') as NAME ,VENDOR_ID as ID FROM mnt_vendor_list order by Name");

                    if (Cms.BusinessLayer.BlCommon.ClsCommon.BL_LANG_ID == 2)
                    {
                        this.lstVendorList.Items.Insert(0, "Todos");
                        this.lstVendorList.SelectedIndex = 0;

                        this.lstStatusList.Items.Insert(0, "Todos");
                        this.lstStatusList.Items.Insert(1, "Concluídos");
                        this.lstStatusList.Items.Insert(2, "Falha");
                        this.lstStatusList.SelectedIndex = 0;

                    }
                    else
                    {
                        this.lstVendorList.Items.Insert(0, "All");
                        this.lstVendorList.SelectedIndex = 0;

                        this.lstStatusList.Items.Insert(0, "All");
                        this.lstStatusList.Items.Insert(1, "Succeeded");
                        this.lstStatusList.Items.Insert(2, "Failed");
                        this.lstStatusList.SelectedIndex = 0;
                    }

                    DataSet ds2 = objDataWrapper.ExecuteDataSet("SELECT EAM.ACTIVITY_ID,ISNULL(EAMM.ACTIVITY_DESCRIPTION,EAM.ACTIVITY_DESCRIPTION) ACTIVITY_DESCRIPTION  FROM EOD_ACTIVITY_MASTER EAM WITH(NOLOCK)LEFT JOIN EOD_ACTIVITY_MASTER_MULTILINGUAL EAMM WITH(NOLOCK)  ON EAM.ACTIVITY_ID=EAMM.ACTIVITY_ID AND EAMM.LANG_ID=" + strLang_id + "   WHERE EAM.PARENT_ACTIVITY_ID IS NOT NULL ORDER BY EAM.ACTIVITY_DESCRIPTION");
                    lstActivity.DataSource = ds2.Tables[0];
                    lstActivity.DataTextField = "ACTIVITY_DESCRIPTION";
                    lstActivity.DataValueField = "ACTIVITY_ID";
                    lstActivity.DataBind();
                    if (Cms.BusinessLayer.BlCommon.ClsCommon.BL_LANG_ID == 2)
                    {
                        this.lstActivity.Items.Insert(0, "Todos");
                        this.lstActivity.SelectedIndex = 0;
                    }
                    else
                    {
                        this.lstActivity.Items.Insert(0, "All");
                        this.lstActivity.SelectedIndex = 0;
                    }
                }

                catch (Exception ex)
                {
                    Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                }
            }
        }
        private void lstVendorList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            int intActivity = 0;
            string strLang_id = GetLanguageID();
            if (lstVendorList.SelectedIndex > 0)
            {
                intActivity = int.Parse(lstVendorList.SelectedValue.ToString());
            }
            Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.Text, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            DataSet ds1 = new DataSet();

            if (intActivity != 0)
            {
                ds1 = objDataWrapper.ExecuteDataSet("SELECT EAM.ACTIVITY_ID,ISNULL(EAMM.ACTIVITY_DESCRIPTION,EAM.ACTIVITY_DESCRIPTION) ACTIVITY_DESCRIPTION  FROM EOD_ACTIVITY_MASTER EAM WITH(NOLOCK)LEFT JOIN EOD_ACTIVITY_MASTER_MULTILINGUAL EAMM WITH(NOLOCK)  ON EAM.ACTIVITY_ID=EAMM.ACTIVITY_ID AND EAMM.LANG_ID=" + strLang_id + "   WHERE EAM.PARENT_ACTIVITY_ID=" + intActivity + " ORDER BY EAM.ACTIVITY_DESCRIPTION");
            }
            else
            {
                ds1 = objDataWrapper.ExecuteDataSet("SELECT EAM.ACTIVITY_ID,ISNULL(EAMM.ACTIVITY_DESCRIPTION,EAM.ACTIVITY_DESCRIPTION) ACTIVITY_DESCRIPTION  FROM EOD_ACTIVITY_MASTER EAM WITH(NOLOCK)LEFT JOIN EOD_ACTIVITY_MASTER_MULTILINGUAL EAMM WITH(NOLOCK)  ON EAM.ACTIVITY_ID=EAMM.ACTIVITY_ID AND EAMM.LANG_ID=" + strLang_id + " WHERE EAM.PARENT_ACTIVITY_ID IS  NULL ORDER BY EAM.ACTIVITY_DESCRIPTION");
            }
            lstActivity.DataSource = ds1.Tables[0];
            lstActivity.DataTextField = "ACTIVITY_DESCRIPTION";
            lstActivity.DataValueField = "ACTIVITY_ID";
            lstActivity.DataBind();
            this.lstActivity.Items.Insert(0, "Todos");
            this.lstActivity.SelectedIndex = 0;
            cmpExpirationDate.Validate(); //Added by Aditya for TFS BUG # 368

        }
        private void SetCaption()
        {
            lblExpirationEndDate.Text = objResourceMgr.GetString("lblExpirationEndDate");
            lblExpirationStartDate.Text = objResourceMgr.GetString("lblExpirationStartDate");
            lblHeader.Text = objResourceMgr.GetString("lblHeader");
            lblStatus.Text = objResourceMgr.GetString("lblStatus");
            btnReport.Text = objResourceMgr.GetString("btnReport");
            lblVendor.Text = objResourceMgr.GetString("lblVendor");
            lblActivity.Text = objResourceMgr.GetString("lblActivity");
        }
        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
            this.lstVendorList.SelectedIndexChanged += new System.EventHandler(this.lstVendorList_SelectedIndexChanged);
        }
        #endregion
    }
}
