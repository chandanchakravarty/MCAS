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
using Cms.CmsWeb;
using System.Resources; 
using System.Reflection; 
using Cms.Model.Maintenance;
using Cms.CmsWeb.Controls; 
using Cms.ExceptionPublisher; 
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Maintenance.Reinsurance;
/******************************************************************************************
    <Author					:Amit Mishra - >
    <Start Date				:October 06,2011 - >
    <End Date				: - >
    <Description			: To be used for Adding/Modifying the Current Credit Rating- >
    <Review Date			: - >
    <Reviewed By			: - >
*******************************************************************************************/
namespace Cms.CmsWeb.Maintenance
{
	
    public class AddRating : Cms.CmsWeb.cmsbase  
	{
        string strRowId = "";
        protected System.Web.UI.WebControls.Label lblDelete;
        protected System.Web.UI.WebControls.Label lblMessage;
        protected Cms.CmsWeb.Controls.CmsButton btnReset;
        protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;
        protected Cms.CmsWeb.Controls.CmsButton btnViewPastRating;
        protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
        protected System.Web.UI.WebControls.Label capAGENCY_ID;
        protected System.Web.UI.WebControls.Label capSTATE_DESC;
        protected System.Web.UI.WebControls.TextBox txtSTATE_DESC;
        protected System.Web.UI.WebControls.Label capAGENCY_RATING;
        protected System.Web.UI.WebControls.TextBox txtAGENCY_RATING;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvAGENCY_ID;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvAGENCY_RATING;
        protected Cms.CmsWeb.Controls.CmsButton btnAddNewRating;
        protected System.Web.UI.WebControls.Label capRATING_YEAR;
        protected System.Web.UI.WebControls.TextBox txtRATING_YEAR;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvRATING_YEAR;
        protected System.Web.UI.WebControls.CompareValidator cpvRATING_YEAR;
        protected System.Web.UI.WebControls.RegularExpressionValidator revRATING_YEAR;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidREIN_COMAPANY_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidRATING_ID;
        protected System.Web.UI.WebControls.DropDownList cmbAGENCY_ID;
        protected System.Web.UI.WebControls.Label capMessages;

        #region Page controls declaration


        #endregion
        #region Local form variables

        //creating resource manager object (used for reading field and label mapping)
        System.Resources.ResourceManager objResourceMgr;






        #endregion


        #region PageLoad event
        private void Page_Load(object sender, System.EventArgs e)
        {
            Ajax.Utility.RegisterTypeForAjax(typeof(AddRating));
            base.ScreenId = "263_1_2";

            lblMessage.Visible = false;
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");

            //START:*********** Setting permissions and class (Read/write/execute/delete)  *************
            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            btnActivateDeactivate.CmsButtonClass = CmsButtonType.Write;
            btnActivateDeactivate.PermissionString = gstrSecurityXML;
            btnAddNewRating.CmsButtonClass = CmsButtonType.Write;
            btnAddNewRating.PermissionString = gstrSecurityXML;
            btnViewPastRating.CmsButtonClass = CmsButtonType.Read;
            btnViewPastRating.PermissionString = gstrSecurityXML;
            string url = ClsCommon.GetLookupURL();//'EFFECTIVE_YEAR', 'EFFECTIVE_YEAR', 'hidCOMPANY_ID', 'hidCOMPANY_ID', 'EditViewRating', 'Select Rating', ''
            btnViewPastRating.Attributes.Add("onclick", "javascript:return EditViewRating();");
            //END:*********** Setting permissions and class (Read/write/execute/delete)  *************
            objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddRating", System.Reflection.Assembly.GetExecutingAssembly());

            if (!Page.IsPostBack)
            {
                GetQueryStringValues();
                LoadDropDowns();
                btnReset.Attributes.Add("onclick", "javascript:return ResetTheForm();");
                btnAddNewRating.Attributes.Add("onclick", "javascript:return ResetTheForm();");
                SetCaptions();
                SetErrorMessages();
                GetOldDataXML();
            }
        }
        #endregion

        #region GetOldDataXML
        private void GetOldDataXML()
        {
            ClsCurrentCreditRating objCurrentCreditRating = new ClsCurrentCreditRating();
            if (hidREIN_COMAPANY_ID.Value != "" && hidREIN_COMAPANY_ID.Value != "0")
                hidOldData.Value = objCurrentCreditRating.GetOldData(hidREIN_COMAPANY_ID.Value.ToString()).GetXml();
           else
                hidOldData.Value = "";
            if (hidOldData.Value != string.Empty)
            {
                if (hidOldData.Value.ToString() == "<NewDataSet />")
                {
                    cmbAGENCY_ID.Enabled = true;
                    txtRATING_YEAR.Enabled = true;
                }
                else
                {
                    cmbAGENCY_ID.Enabled = false;
                    txtRATING_YEAR.Enabled = false;
                                    }
                cmbAGENCY_ID.SelectedValue = ClsCommon.FetchValueFromXML("AGENCY_ID", hidOldData.Value);
                txtAGENCY_RATING.Text = ClsCommon.FetchValueFromXML("RATING", hidOldData.Value);
                txtRATING_YEAR.Text = ClsCommon.FetchValueFromXML("EFFECTIVE_YEAR", hidOldData.Value);
            }
        }
        #endregion



        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        #region Set Validators ErrorMessages
        /// <summary>
        /// Method to set validation control error masessages.
        /// Parameters: none
        /// Return Type: none
        /// </summary>
        private void SetErrorMessages()
        {
            rfvAGENCY_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2081");
            rfvAGENCY_RATING.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2082");
            rfvRATING_YEAR.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2083");
            revRATING_YEAR.ValidationExpression = aRegExpInteger;
            cpvRATING_YEAR.ValueToCompare = aAppMinYear;//aRegExpInteger;
            cpvRATING_YEAR.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "2");
            revRATING_YEAR.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "2");        
        }

        #endregion

        private void GetQueryStringValues()
        {
            if (Request.QueryString["AgencyId"] != null && Request.QueryString["AgencyId"].ToString() != "")
                hidAGENCY_ID.Value = Request.QueryString["AgencyId"].ToString();
            else
                hidAGENCY_ID.Value = "0";

            if (Request.QueryString["EntityId"] != null && Request.QueryString["EntityId"].ToString() != "")
                hidREIN_COMAPANY_ID.Value = Request.QueryString["EntityId"].ToString();
            else
                hidREIN_COMAPANY_ID.Value = "0";
        }


        private void btnSave_Click(object sender, System.EventArgs e)
        {
          
            try
            {
                int intRetVal;
                //For retreiving the return value of business class save function
                ClsCurrentCreditRating objCurrentCreditRating = new ClsCurrentCreditRating();
                
                //Retreiving the form values into model class object
                   ClsCurrentCreditRatingInfo objCurrentCreditRatingInfo = GetFormValue();

                  //  objCurrentCreditRatingInfo.CREATED_BY = int.Parse(GetUserId());
                    objCurrentCreditRatingInfo.CREATED_DATETIME = DateTime.Now;
                    objCurrentCreditRatingInfo.IS_ACTIVE = "Y";
                    objCurrentCreditRatingInfo.COMPANY_TYPE = "INS";//Ins-Insurer(Carrier),COI-Co-insurer,RI-Re-Insurer;
                    objCurrentCreditRatingInfo.MODIFIED_BY = int.Parse(GetUserId());
                    objCurrentCreditRatingInfo.LAST_UPDATED_DATETIME = DateTime.Now;
                    //Calling the add method of business layer class
                    intRetVal = objCurrentCreditRating.Add(objCurrentCreditRatingInfo);
                    cmbAGENCY_ID.Enabled = false;
                    txtRATING_YEAR.Enabled = false;

                    if (intRetVal == 1 || intRetVal == 3)
                    {
                        hidAGENCY_ID.Value = objCurrentCreditRatingInfo.AGENCY_ID.ToString();
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("31");
                        hidFormSaved.Value = "1";
                        hidIS_ACTIVE.Value = "Y";
                        GetOldDataXML();
                    }
                    else 
                    {
                        hidAGENCY_ID.Value = objCurrentCreditRatingInfo.AGENCY_ID.ToString();
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("29");
                        hidFormSaved.Value = "2";
                        GetOldDataXML();
                    }
               
                lblMessage.Visible = true;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";

            }
        }
      
        private void SetCaptions()
        {
            capAGENCY_RATING.Text = objResourceMgr.GetString("txtAGENCY_RATING");
            capRATING_YEAR.Text = objResourceMgr.GetString("txtRATING_YEAR");
            capAGENCY_ID.Text = objResourceMgr.GetString("cmbAGENCY_ID");
        }


        #region GetFormValue
        private ClsCurrentCreditRatingInfo GetFormValue()
        {
            ClsCurrentCreditRatingInfo objCurrentCreditRatingInfo = new ClsCurrentCreditRatingInfo();
            if (cmbAGENCY_ID.SelectedItem != null && cmbAGENCY_ID.SelectedItem.Value != "")
                objCurrentCreditRatingInfo.AGENCY_ID = int.Parse(cmbAGENCY_ID.SelectedItem.Value);
            objCurrentCreditRatingInfo.COMPANY_ID = int.Parse(hidREIN_COMAPANY_ID.Value);
            objCurrentCreditRatingInfo.RATING = txtAGENCY_RATING.Text.Trim();
            objCurrentCreditRatingInfo.EFFECTIVE_YEAR = Convert.ToInt32(txtRATING_YEAR.Text.Trim());
            if (hidRATING_ID.Value == "" || hidRATING_ID.Value == "0")
            {
                objCurrentCreditRatingInfo.RATING_ID = 0;
            }
            else
            {
                objCurrentCreditRatingInfo.AGENCY_ID = int.Parse(hidAGENCY_ID.Value);
                strRowId = hidAGENCY_ID.Value;
            }
            return objCurrentCreditRatingInfo;
        }
        #endregion

        #region LoadDropDowns
        private void LoadDropDowns()
        {
            cmbAGENCY_ID.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("RTANCY");
            cmbAGENCY_ID.DataTextField = "LookupDesc";
            cmbAGENCY_ID.DataValueField = "LookupID";
            cmbAGENCY_ID.DataBind();
            cmbAGENCY_ID.Items.Insert(0, "");

        }
        #endregion
       
        //[Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        //public DataSet AjaxFillRating(string RatingId)
        //{
        //    try
        //    {
        //        ClsCurrentCreditRating obj = new ClsCurrentCreditRating();
        //        DataSet ds = new DataSet();
        //        string result = "";
        //        ds = obj.GetRatingLogXml(RatingId);
        //        result = ds.GetXml();
        //        //DataTable dt = new DataTable();
        //        //dt = ds.Tables[0];
        //        //LoadDropDowns();
        //        //cmbAGENCY_ID.SelectedValue = dt.Rows[0]["AGENCY_ID"].ToString();
        //        //txtAGENCY_RATING.Text = dt.Rows[0]["RATED"].ToString();
        //        //txtRATING_YEAR.Text = dt.Rows[0]["EFFECTIVE_YEAR"].ToString();
        //        return ds;
                
        //    }
        //    catch (Exception ex)
        //    {
        //        Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
        //        return null;  
        //    }
        //}
      
   
		
	}
}
