/******************************************************************************************
<Author				    : -   Pradeep Kushwaha
<Start Date				: -	  07-July-2010
<End Date				: -	
<Description			: -   Policy reject reason 
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: -   
<Modified By			: - 
<Purpose				: - 
*******************************************************************************************/
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
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;
using System.Resources;
using Model.Policy;
using System.Reflection;
using Cms.BusinessLayer.BlApplication;

namespace Cms.Policies.Aspx
{
    public partial class PolicyRejectReson : Cms.CmsWeb.cmsbase
    {
        #region Page controls declaration

        private ResourceManager objResourceManager = null;
        
        private ClsPolicyRejectReasonInfo objPolicyRejectReason = new ClsPolicyRejectReasonInfo();
        private static String strRowId = String.Empty;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyVersionID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidREJECT_REASON_ID;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;
       
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.WebControls.Label capREASON_DESC;
        protected System.Web.UI.WebControls.Label lblHeader;
        protected System.Web.UI.WebControls.Label capREASON_TYPE_ID;
        protected System.Web.UI.WebControls.TextBox txtREASON_DESC;
        ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();
        
            
        #endregion 
        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "";

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;
            
            btnClose.CmsButtonClass = CmsButtonType.Write;
            btnClose.PermissionString = gstrSecurityXML;
            
            

            objResourceManager = new ResourceManager("Cms.Policies.Aspx.PolicyRejectReson", Assembly.GetExecutingAssembly());
            btnClose.Attributes.Add("OnClick", "self.close();");
            lblMessage.Visible = false;

            if (!IsPostBack)
            {
                BindReasonType();
                if (Request.QueryString["CUSTOMER_ID"] != null && Request.QueryString["CUSTOMER_ID"].ToString() != "")
                    hidCustomerID.Value = Request.QueryString["CUSTOMER_ID"].ToString();
                else
                    hidCustomerID.Value = GetCustomerID();

                if (Request.QueryString["POLICY_ID"] != null && Request.QueryString["POLICY_ID"].ToString() != "")
                    hidPolicyID.Value = Request.QueryString["POLICY_ID"].ToString();
                else
                    hidPolicyID.Value = GetPolicyID();

                if (Request.QueryString["POLICY_VERSION_ID"] != null && Request.QueryString["POLICY_VERSION_ID"].ToString() != "")
                    hidPolicyVersionID.Value = Request.QueryString["POLICY_VERSION_ID"].ToString();
                else
                    hidPolicyVersionID.Value = GetPolicyVersionID();

                this.InsertRejectReasoninfo();//Use to add reject reason data on default 

                SetCaptions();
                SetErrorMessages();
             

               

            }
          
        }
        /// <summary>
        /// default Addtion of reject reason data 
        /// </summary>
        private void InsertRejectReasoninfo()
        {
            try
            {
                ClsPolicyRejectReasonInfo objPolicyRejectReasonInfo = new ClsPolicyRejectReasonInfo();
                objPolicyRejectReasonInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCustomerID.Value);
                objPolicyRejectReasonInfo.POLICY_ID.CurrentValue = int.Parse(hidPolicyID.Value);
                objPolicyRejectReasonInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPolicyVersionID.Value);

                objPolicyRejectReasonInfo.REASON_TYPE_ID.CurrentValue = 14671;//For Others (Default)


                objPolicyRejectReasonInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                objPolicyRejectReasonInfo.CREATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now);
                objPolicyRejectReasonInfo.IS_ACTIVE.CurrentValue = "Y";
                objPolicyRejectReasonInfo.REASON_DESC.CurrentValue = String.Empty;

                int retVal = objGeneralInformation.AddPolicyRejectReason(objPolicyRejectReasonInfo);
                if (retVal > 0)
                {
                    hidREJECT_REASON_ID.Value = objPolicyRejectReasonInfo.REJECT_REASON_ID.CurrentValue.ToString();
                    strRowId = hidREJECT_REASON_ID.Value;
                    this.GetOldDataObject(Convert.ToInt32(hidREJECT_REASON_ID.Value));
                }
                else
                {
                    hidREJECT_REASON_ID.Value = "NEW";
                    strRowId = "NEW";
                }
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1098") + "\n" + ex.Message.ToString();
                lblMessage.Visible = true;
            }

        }

        private void GetOldDataObject(int REJECT_REASON_ID)
        {
            ClsPolicyRejectReasonInfo objPolicyRejectReasonInfo = new ClsPolicyRejectReasonInfo();

            objPolicyRejectReasonInfo.REJECT_REASON_ID.CurrentValue = REJECT_REASON_ID;
            objPolicyRejectReasonInfo.CUSTOMER_ID.CurrentValue = int.Parse(GetCustomerID());
            objPolicyRejectReasonInfo.POLICY_ID.CurrentValue = int.Parse(GetPolicyID());
            objPolicyRejectReasonInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(GetPolicyVersionID());


            if (objGeneralInformation.FetchRejectReasonInfo(ref objPolicyRejectReasonInfo))
            {
                PopulatePageFromEbixModelObject(this.Page, objPolicyRejectReasonInfo);

                base.SetPageModelObject(objPolicyRejectReasonInfo);
            }// if (objGeneralInformation.FetchRejectReasonInfo(ref objPolicyRejectReasonInfo))

        }
        private void BindReasonType()
        {
            //Bind Reason Type 
            cmbREASON_TYPE_ID.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("RETRST");
            cmbREASON_TYPE_ID.DataTextField = "LookupDesc";
            cmbREASON_TYPE_ID.DataValueField = "LookupID";
            cmbREASON_TYPE_ID.DataBind();
            cmbREASON_TYPE_ID.Items.Insert(0, "");
        }//private void BindReasonType()
        private void SetErrorMessages()
        {
            rfvREASON_TYPE_ID.ErrorMessage = ClsMessages.FetchGeneralMessage("1115"); 
        }
        private void SetCaptions()
        {
            capREASON_TYPE_ID.Text = objResourceManager.GetString("cmbREASON_TYPE_ID");
            capREASON_DESC.Text = objResourceManager.GetString("txtREASON_DESC");
            lblHeader.Text = objResourceManager.GetString("lblHeader");
           
           
        }
        private void GetFormValue(ClsPolicyRejectReasonInfo objPolicyRejectReasonInfo)
        {
            if (hidREJECT_REASON_ID.Value != "0")
            {
                strRowId = hidREJECT_REASON_ID.Value;
            }
            else { strRowId = "NEW"; }

            objPolicyRejectReasonInfo.CUSTOMER_ID.CurrentValue =int.Parse(hidCustomerID.Value);
            objPolicyRejectReasonInfo.POLICY_ID.CurrentValue = int.Parse(hidPolicyID.Value);
            objPolicyRejectReasonInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPolicyVersionID.Value);
            if (cmbREASON_TYPE_ID.SelectedValue != "")
                objPolicyRejectReasonInfo.REASON_TYPE_ID.CurrentValue = int.Parse(cmbREASON_TYPE_ID.SelectedValue);
            else
                objPolicyRejectReasonInfo.REASON_TYPE_ID.CurrentValue = GetEbixIntDefaultValue();

            objPolicyRejectReasonInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
            objPolicyRejectReasonInfo.CREATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now);
            objPolicyRejectReasonInfo.IS_ACTIVE.CurrentValue = "Y";

            if (txtREASON_DESC.Text != String.Empty)
                objPolicyRejectReasonInfo.REASON_DESC.CurrentValue = txtREASON_DESC.Text;
            else
                objPolicyRejectReasonInfo.REASON_DESC.CurrentValue = String.Empty;


        }
        protected void btnSave_Click(object sender, EventArgs e)
        {  
            try
            {
                int retVal = 0;
                ClsPolicyRejectReasonInfo objPolicyRejectReasonInfo = new ClsPolicyRejectReasonInfo();
                if (strRowId.ToUpper().Trim().Equals("NEW"))
                {
                   
                    this.GetFormValue(objPolicyRejectReasonInfo);

                    retVal = objGeneralInformation.AddPolicyRejectReason(objPolicyRejectReasonInfo);

                    if (retVal > 0)
                    {
                        hidREJECT_REASON_ID.Value = objPolicyRejectReasonInfo.REJECT_REASON_ID.CurrentValue.ToString();

                        base.SetPageModelObject(objPolicyRejectReasonInfo);

                        lblMessage.Text = ClsMessages.FetchGeneralMessage("29");
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                        lblMessage.Visible = true;
                    }
                }
                else//For the update case if the reject id is null or empty
                {
                    objPolicyRejectReasonInfo = (ClsPolicyRejectReasonInfo)base.GetPageModelObject();
                    this.GetFormValue(objPolicyRejectReasonInfo);
                    objPolicyRejectReasonInfo.IS_ACTIVE.CurrentValue = "Y";
                    objPolicyRejectReasonInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                    objPolicyRejectReasonInfo.LAST_UPDATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now);


                    retVal = objGeneralInformation.UpdateRejectReasonInfo(objPolicyRejectReasonInfo);  //Call Business Layer Function for Update

                    if (retVal > 0)
                    {
                        hidREJECT_REASON_ID.Value = objPolicyRejectReasonInfo.REJECT_REASON_ID.CurrentValue.ToString();
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("31");
                        lblMessage.Visible = true;
                    }
                    else if (retVal == -1)
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("18");
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                        lblMessage.Visible = true;
                       
                    }
                }
            }
            catch
            {
                lblMessage.Text = ClsMessages.FetchGeneralMessage("1116"); 
                lblMessage.Visible = true;
            }
         
        }
    }
}
