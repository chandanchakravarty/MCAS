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
using Cms.Model.Maintenance.Accounting;
using Cms.BusinessLayer.BlCommon.Accounting;
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher.ExceptionManagement;
using System.IO;
using Cms.Blcommon;
using Cms.Model.Account;

namespace Cms.Account.Aspx
{
    public partial class PolicyMelEventsDetail : Cms.CmsWeb.cmsbase
    {
       // protected Cms.CmsWeb.Controls.CmsButton btnSave;
        ResourceManager objresource;
        ClsPolicyMeleventInfo objClsPolicyMeleventInfo = new ClsPolicyMeleventInfo();
        ClsAccountingEntity objClsAccountingEntity = new ClsAccountingEntity(); 
        private string strRowId = "";
        public string URL;
       


        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "540_0";

            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnDelete.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Delete;
            btnDelete.PermissionString = gstrSecurityXML;          
            
            
            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            SetErrorMessage();
            objresource = new System.Resources.ResourceManager("Cms.Account.Aspx.PolicyMelEventsDetail", System.Reflection.Assembly.GetExecutingAssembly());
            URL = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL();
            txtPolicyNo.Attributes.Add("readonly", "readonly");
            if (!IsPostBack)
            {                
                SetCaption();

                if (Request.QueryString["ROW_ID"] != null && Request.QueryString["ROW_ID"].ToString() != "")
                {

                    hidrowId.Value = Request.QueryString["ROW_ID"].ToString();

                    this.GetOldDataObject(Convert.ToInt32(Request.QueryString["ROW_ID"].ToString()));


                }
                else if (Request.QueryString["ROW_ID"] == null)
                {
                    //btnActivateDeactivate.Visible = false;

                    hidrowId.Value = "NEW";
                    if (String.IsNullOrEmpty(txtDate.Text))
                    {
                        txtDate.Text = ConvertToDate(DateTime.Now.ToString()).ToShortDateString();
                    }

                }    

            }



            hlkDate.Attributes.Add("OnClick", "fPopCalendar(document.getElementById('txtDate'), document.getElementById('txtDate'))");
            txtDate.Attributes.Add("onBlur", "FormatDate()"); 
            btnReset.Attributes.Add("onclick", "javascript: return ResetTheForm();");

            strRowId = hidrowId.Value; 
        }
        #region Web Form Designer generated code


        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {
            //  this.btnResetSeries.Click += new System.EventHandler(this.btnResetSeries_Click);
            this.Load += new System.EventHandler(this.Page_Load);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

       }
        #endregion

        protected void btnDelete_Click(object sender, EventArgs e)
        {

            try
            {

                objClsPolicyMeleventInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                objClsPolicyMeleventInfo.LAST_UPDATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now);
        

                objClsPolicyMeleventInfo.ROW_ID.CurrentValue = Convert.ToInt32(hidrowId.Value);

                int intRetval = objClsAccountingEntity.DeletePolicy(objClsPolicyMeleventInfo);
                if (intRetval >= 0)
                {

                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "8");
                    hidFormSaved.Value = "1";
                    //trBody.Attributes.Add("style", "display:none");
                    txtPolicyNo.Text = "";
                    txtDate.Text = "";
                }
                else
                {

                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "9");//ClsMessages.GetMessage(base.ScreenId, "128");
                    hidFormSaved.Value = "2";
                }

                lblMessage.Visible = true;

            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.FetchGeneralMessage("21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";
            }
        }


        private void btnSave_Click(object sender, System.EventArgs e)
        {
            try
            {
                int intRetVal;	//For retreiving the return value of business class save function


                //Retreiving the form values into model class object

                this.GetFormValue(objClsPolicyMeleventInfo);

                if (strRowId.ToUpper().Equals("NEW")) //save case              
                {
                    objClsPolicyMeleventInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                    objClsPolicyMeleventInfo.CREATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now);
                    objClsPolicyMeleventInfo.IS_ACTIVE.CurrentValue = "Y";            

                   
                    //Calling the add method of business layer class
                    objClsPolicyMeleventInfo.RequiredTransactionLog = true;
                    intRetVal = objClsAccountingEntity.AddPolicy(objClsPolicyMeleventInfo);
                    if (intRetVal == 1)
                    {
                        //if (objClsPolicyMeleventInfo.ROW_ID.CurrentValue.ToString() != "1")
                        //{
                        //    this.GetOldDataObject(objClsPolicyMeleventInfo.ROW_ID.CurrentValue);
                        //}
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "1");
                        hidFormSaved.Value = "1";
                        //btnActivateDeactivate.Visible = true;

                        //btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objClsPolicyMeleventInfo.IS_ACTIVE.CurrentValue.ToString().Trim());
                    }
                    else if (intRetVal == -2)
                    {
                        //lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "4");
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "2");
                    }

                    else
                    {
                        //lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "3");

                        hidFormSaved.Value = "2";
                    }
                    lblMessage.Visible = true;
                } // end save case
                else //UPDATE CASE
                {

                    //objClsPolicyMeleventInfo.RequiredTransactionLog = false;
                    objClsPolicyMeleventInfo = (ClsPolicyMeleventInfo)base.GetPageModelObject();

                    this.GetFormValue(objClsPolicyMeleventInfo);

                    objClsPolicyMeleventInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                    objClsPolicyMeleventInfo.LAST_UPDATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now);                    
                    objClsPolicyMeleventInfo.IS_ACTIVE.CurrentValue = "Y";            

                    int intRetval = objClsAccountingEntity.UpdatePolicy(objClsPolicyMeleventInfo);

                    if (intRetval > 0)
                    {
                        this.GetOldDataObject(objClsPolicyMeleventInfo.ROW_ID.CurrentValue);
                        //lblMessage.Text = ClsMessages.FetchGeneralMessage("31");
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "4");
                        //btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objClsPolicyMeleventInfo.IS_ACTIVE.CurrentValue.ToString().Trim());
                        hidFormSaved.Value = "1";

                    }                    
                    else
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "5");
                        //lblMessage.Text = lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                       // hidFormSaved.Value = "2";
                    }

                    lblMessage.Visible = true;
                }


            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";

            }
            finally
            {

            }

        }

        private void GetOldDataObject(Int32 ROW_ID)
        {

            objClsPolicyMeleventInfo = objClsAccountingEntity.FetchData(ROW_ID);
            PopulatePageFromEbixModelObject(this.Page, objClsPolicyMeleventInfo);
            //btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objClsPolicyMeleventInfo.IS_ACTIVE.CurrentValue.ToString().Trim());
            base.SetPageModelObject(objClsPolicyMeleventInfo);
            hidrowId.Value = objClsPolicyMeleventInfo.ROW_ID.CurrentValue.ToString().Trim();

            if (objClsPolicyMeleventInfo.POLICY_NO.CurrentValue.ToString() != "")
                txtPolicyNo.Text = objClsPolicyMeleventInfo.POLICY_NO.CurrentValue.ToString();
            

        }

        private void GetFormValue(ClsPolicyMeleventInfo objClsPolicyMeleventInfo)
        {           
            if (txtDate.Text != "")
            {
                objClsPolicyMeleventInfo.DATE.CurrentValue = ConvertToDate(txtDate.Text.Trim());
            }
            if (txtPolicyNo.Text != "")
            {
                objClsPolicyMeleventInfo.POLICY_NO.CurrentValue = Convert.ToString(txtPolicyNo.Text.Trim());
            }
            if (!String.IsNullOrEmpty(hidCUSTOMER_ID.Value))
            {
                objClsPolicyMeleventInfo.CUSTOMER_ID.CurrentValue = Convert.ToInt32(hidCUSTOMER_ID.Value);
                 
            }
            if (!String.IsNullOrEmpty(hidPOLICY_ID.Value))
           {
               objClsPolicyMeleventInfo.POLICY_ID.CurrentValue = Convert.ToInt32(hidPOLICY_ID.Value);
                  
           }

        }
        private void SetErrorMessage()
        {

           /*rfvCUSTOMER_ID.ErrorMessage = "Please enter Customer id";

            revCUSTOMER_ID.ValidationExpression = aRegExpInteger;
            revCUSTOMER_ID.ErrorMessage = "Error on Customer id";*/

            rfvDate.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "6");
          
            //revDate.ValidationExpression = aRegExpDate;
            //revDate.ErrorMessage = "Error on date";


           /*rfvPolicy_Id.ErrorMessage = "Please enter policy id";
            revPolicy_Id.ErrorMessage = "Error on policy id";
            revPolicy_Id.ValidationExpression = aRegExpInteger;*/

            rfvPolicy_No.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "7");
           
            

        }
        private void SetCaption()
        {

            //capCustomerId.Text = objresource.GetString("capCustomerId");
            capDate.Text = objresource.GetString("capDate");
            //capPolicyID.Text = objresource.GetString("capPolicyID");
            capPolicyNo.Text = objresource.GetString("capPolicyNo");

        }

    }
}
