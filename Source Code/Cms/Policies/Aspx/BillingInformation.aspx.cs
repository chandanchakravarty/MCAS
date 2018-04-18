/******************************************************************************************
<Author					: -		Sneha
<Start Date				: -		16-11-2011
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
using Cms.Model.Policy;
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb;
using Cms.ExceptionPublisher;
using Cms.BusinessLayer.Blapplication;
using System.Data;
using System.Globalization;

namespace Cms.Policies.Aspx
{
    public partial class BillingInformation : Cms.Policies.policiesbase
    {
        private String strRowId = String.Empty;
        private string XmlFullFilePath = "";
        public string CUSTOMER_ID; 
        public string POLICY_ID;
        public string POLICY_VER_ID;
        Cms.BusinessLayer.BlApplication.ClsInstallmentInfo objInstallmentInfo = new BusinessLayer.BlApplication.ClsInstallmentInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
            base.ScreenId = "224_11";


            btnSave.CmsButtonClass = CmsButtonType.Write;  //Permission made Write instead of Execute by Sibin on 23 Oct 08
            btnSave.PermissionString = gstrSecurityXML;

            btnReset.CmsButtonClass = CmsButtonType.Write;  //Permission made Write instead of Execute by Sibin on 23 Oct 08
            btnReset.PermissionString = gstrSecurityXML;

            //btnActivateDeactivate.CmsButtonClass = CmsButtonType.Write;  //Permission made Write instead of Execute by Sibin on 23 Oct 08
            //btnActivateDeactivate.PermissionString = gstrSecurityXML;

            btnDelete.CmsButtonClass = CmsButtonType.Delete;
            btnDelete.PermissionString = gstrSecurityXML;

            btnReset.Attributes.Add("onclick", "javascript:return ResetTheForm();");
            btnDelete.Attributes.Add("onclick", "javascript:return ResetTheForm();");

            trBody.Attributes.Add("style", "display:none");
            trErrorMsgs.Attributes.Add("style", "display:none");

            string strSysID = GetSystemId();
            if (strSysID == "ALBAUAT")
                strSysID = "ALBA";

            XmlFullFilePath = Request.PhysicalApplicationPath + "/Policies/support/PageXml/" + strSysID + "/" + "BillingInformation.xml";

            if (!Page.IsPostBack)
            {
                
                if (ClsCommon.IsXMLResourceExists(Request.PhysicalApplicationPath + "/Policies/support/PageXML/" + strSysID, "BillingInformation.xml"))
                    setPageControls(Page, Request.PhysicalApplicationPath + "/Policies/support/PageXml/" + strSysID + "/BillingInformation.xml");

                getdata();
                FillDropDown();
                if (hidLOB_ID.Value != "" && hidCUSTOMER_ID.Value != "" && hidPOLICY_ID.Value != "" && hidPOLICY_VERSION_ID.Value!="")
                {
                    hidBILLING_ID.Value = "1";
                    ClsBillingInformationInfo objBillingInformationInfo;
                   
                    objBillingInformationInfo = objInstallmentInfo.FetchId(hidCUSTOMER_ID.Value, hidPOLICY_ID.Value, hidPOLICY_VERSION_ID.Value, hidLOB_ID.Value);
                   
                    hidBILLING_ID.Value = objBillingInformationInfo.BILLING_ID.CurrentValue.ToString();
                    if (hidBILLING_ID.Value == "-1")
                    {
                        hidBILLING_ID.Value = "NEW";
                        btnDelete.Style.Add("display", "none");
                    }
                    else
                    {
                        strRowId = hidBILLING_ID.Value;
                        this.GetOldDataObject();
                    }
                }
                else
                {
                    hidBILLING_ID.Value = "NEW";
                    btnDelete.Style.Add("display", "none");
                }
                strRowId = hidBILLING_ID.Value;

            }
        }

        protected void FillDropDown()
        {
            DataTable dt = objInstallmentInfo.GET_BILL_PLAN();
            cmbBILLING_PLAN.DataSource = dt;
            cmbBILLING_PLAN.DataValueField = "IDEN_PLAN_ID";
            cmbBILLING_PLAN.DataTextField = "PLAN_DESCRIPTION";
            cmbBILLING_PLAN.DataBind();

            
            
        }
        protected void getdata()
        {
            if (Request.QueryString["POLICY_LOB"] != null && Request.QueryString["POLICY_LOB"].ToString() != "")
                hidLOB_ID.Value = Request.QueryString["POLICY_LOB"].ToString();
            else
                hidLOB_ID.Value = GetLOBID();

            if (Request.QueryString["CUSTOMER_ID"] != null && Request.QueryString["CUSTOMER_ID"].ToString() != "")
                hidCUSTOMER_ID.Value = Request.QueryString["CUSTOMER_ID"].ToString();
            else
                hidCUSTOMER_ID.Value = GetCustomerID();

            if (Request.QueryString["POLICY_ID"] != null && Request.QueryString["POLICY_ID"].ToString() != "")
                hidPOLICY_ID.Value = Request.QueryString["POLICY_ID"].ToString();
            else
                hidPOLICY_ID.Value = GetPolicyID();
          
            if (Request.QueryString["POLICY_VERSION_ID"] != null && Request.QueryString["POLICY_VERSION_ID"].ToString() != "")
                hidPOLICY_VERSION_ID.Value = Request.QueryString["POLICY_VERSION_ID"].ToString();
            else
                hidPOLICY_VERSION_ID.Value = GetPolicyVersionID();

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int intRetval;
            ClsBillingInformationInfo objBillingInformationInfo;
            try
            {
                strRowId = hidBILLING_ID.Value;
                if (strRowId.ToUpper().Equals("NEW"))
                {
                    objBillingInformationInfo= new ClsBillingInformationInfo();
                    this.getFormValues(objBillingInformationInfo);
                    objBillingInformationInfo.IS_ACTIVE.CurrentValue = "Y";

                    objBillingInformationInfo.CUSTOMER_ID.CurrentValue = hidCUSTOMER_ID.Value;
                    objBillingInformationInfo.POLICY_ID.CurrentValue=  hidPOLICY_ID.Value;
                    objBillingInformationInfo.POLICY_VERSION_ID.CurrentValue = hidPOLICY_VERSION_ID.Value;
                    objBillingInformationInfo.LOB_ID.CurrentValue= hidLOB_ID.Value;

                    objBillingInformationInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                    objBillingInformationInfo.CREATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());
                
                    intRetval = objInstallmentInfo.AddPolBillinInfo(objBillingInformationInfo, XmlFullFilePath);
                    hidBILLING_ID.Value = objBillingInformationInfo.BILLING_ID.CurrentValue.ToString();
                    

                    if (intRetval > 0)
                    {
                        hidBILLING_ID.Value = objBillingInformationInfo.BILLING_ID.CurrentValue.ToString();

                        this.GetOldDataObject();

                        lblMessage.Text = ClsMessages.FetchGeneralMessage("29");
                        
                         btnDelete.Style.Add("display", "inline"); 
                    }
                    else if (intRetval == -1)
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("18");
                        
                    }

                    else
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                        
                    }
                    lblMessage.Visible = true;
                    trErrorMsgs.Attributes.Add("style", "display:inline");

                }
                else
                {

                    objBillingInformationInfo = (ClsBillingInformationInfo)base.GetPageModelObject();
                    this.getFormValues(objBillingInformationInfo);

                    objBillingInformationInfo.CUSTOMER_ID.CurrentValue = hidCUSTOMER_ID.Value;
                    objBillingInformationInfo.POLICY_ID.CurrentValue = hidPOLICY_ID.Value;
                    objBillingInformationInfo.POLICY_VERSION_ID.CurrentValue = hidPOLICY_VERSION_ID.Value;
                    objBillingInformationInfo.LOB_ID.CurrentValue = hidLOB_ID.Value;

                    objBillingInformationInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                    objBillingInformationInfo.LAST_UPDATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());

                    intRetval = objInstallmentInfo.updatePolBillingInfo(objBillingInformationInfo, XmlFullFilePath);



                    if (intRetval > 0)
                    {
                        hidBILLING_ID.Value = objBillingInformationInfo.BILLING_ID.CurrentValue.ToString();
                        this.GetOldDataObject();
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("31");

                        hidFormSaved.Value = "1";


                    }
                    else if (intRetval == -1)
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("18");
                        hidFormSaved.Value = "2";
                    }

                    else
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                        hidFormSaved.Value = "2";
                    }

                    lblMessage.Visible = true;
                    trErrorMsgs.Attributes.Add("style", "display:inline");
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

        private void GetOldDataObject()
        {

            ClsBillingInformationInfo objBillingInformationInfo = new ClsBillingInformationInfo();

            objBillingInformationInfo.BILLING_ID.CurrentValue = int.Parse(hidBILLING_ID.Value);
            objBillingInformationInfo = objInstallmentInfo.FetchData(Int32.Parse(hidBILLING_ID.Value),hidCUSTOMER_ID.Value,hidPOLICY_ID.Value,hidPOLICY_VERSION_ID.Value,hidLOB_ID.Value);
            PopulatePageFromEbixModelObject(this.Page, objBillingInformationInfo);

            base.SetPageModelObject(objBillingInformationInfo);
        }
        private void getFormValues(ClsBillingInformationInfo objBillingInformationInfo)
        {
            if (cmbBILLING_TYPE.SelectedValue != "" && cmbBILLING_TYPE.SelectedIndex.ToString() != "")
            {
                objBillingInformationInfo.BILLING_TYPE.CurrentValue = Convert.ToString(cmbBILLING_TYPE.SelectedValue);
            }
            if (cmbBILLING_PLAN.SelectedValue != "")
            {
                objBillingInformationInfo.BILLING_PLAN.CurrentValue = Convert.ToString(cmbBILLING_PLAN.SelectedValue);
            }
            if (cmbDOWN_PAYMENT_MODE.SelectedValue != "")
            {
                objBillingInformationInfo.DOWN_PAYMENT_MODE.CurrentValue = Convert.ToString(cmbDOWN_PAYMENT_MODE.SelectedValue);
            }
            if (cmbPROXY_SIGN_OBTAIN.SelectedValue != "")
            {
                objBillingInformationInfo.PROXY_SIGN_OBTAIN.CurrentValue = Convert.ToString(cmbPROXY_SIGN_OBTAIN.SelectedValue);
            }
            if (txtUNDERWRITER.Text != "")
            {
                objBillingInformationInfo.UNDERWRITER.CurrentValue = Convert.ToString(txtUNDERWRITER.Text);
            }
            if (cmbROLLOVER.SelectedValue != "")
            {
                objBillingInformationInfo.ROLLOVER.CurrentValue = Convert.ToString(cmbROLLOVER.SelectedValue);
            }
            if (chkCOMP_APP_BONUS_APPLIES.Checked != false)
            {
                objBillingInformationInfo.COMP_APP_BONUS_APPLIES.CurrentValue = Convert.ToString(chkCOMP_APP_BONUS_APPLIES.Checked);
            }
            if (txtRECIVED_PREMIUM.Text != "")
            {
                objBillingInformationInfo.RECIVED_PREMIUM.CurrentValue = Convert.ToString(txtRECIVED_PREMIUM.Text);
            }
            if (txtCURRENT_RESIDENCE.Text != "")
            {
                objBillingInformationInfo.CURRENT_RESIDENCE.CurrentValue = Convert.ToString(txtCURRENT_RESIDENCE.Text);
            }   
            
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
           
            ClsBillingInformationInfo objBillingInformationInfo;
            int intRetval = 0;
            try
            {
                objBillingInformationInfo = (ClsBillingInformationInfo)base.GetPageModelObject();

                objBillingInformationInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                objBillingInformationInfo.LAST_UPDATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());

                intRetval = objInstallmentInfo.DelPolBillinInfo(objBillingInformationInfo, XmlFullFilePath);

                if (intRetval > 0)
                {
                    lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "127");
                    //hidFormSaved.Value = "5";
                    trBody.Attributes.Add("style", "display:inline");

                }
                else if (intRetval == -1)
                {
                    lblDelete.Text = ClsMessages.GetMessage(base.ScreenId, "128");
                    //hidFormSaved.Value = "2";
                }
                lblDelete.Visible = true;
                lblMessage.Visible = false;
                hidBILLING_ID.Value = "NEW";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";

            }
        }

        //protected void btnActivateDeactivate_Click(object sender, EventArgs e)
        //{
        //    ClsBillingInformationInfo objBillingInformationInfo;

        //    try
        //    {
        //        objBillingInformationInfo = (ClsBillingInformationInfo)base.GetPageModelObject();

        //        if (objBillingInformationInfo.IS_ACTIVE.CurrentValue == "Y")
        //        { objBillingInformationInfo.IS_ACTIVE.CurrentValue = "N"; }
        //        else
        //        { objBillingInformationInfo.IS_ACTIVE.CurrentValue = "Y"; }



        //        int intRetval = objInstallmentInfo.ActivateDeactivatePolBillingInfo(objBillingInformationInfo, XmlFullFilePath);
        //        if (intRetval > 0)
        //        {
        //            if (objBillingInformationInfo.IS_ACTIVE.CurrentValue == "N")
        //            {
        //                // btnActivateDeactivate.Visible = false;
        //                lblMessage.Text = ClsMessages.FetchGeneralMessage("9");

        //            }
        //            else
        //            {
        //                lblMessage.Text = ClsMessages.FetchGeneralMessage("7");
        //            }
        //            btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objBillingInformationInfo.IS_ACTIVE.CurrentValue.ToString().Trim());
        //            hidFormSaved.Value = "1";
        //            btnActivateDeactivate.Enabled = true;

        //            base.SetPageModelObject(objBillingInformationInfo);
        //        }
        //        //lblDelete.Visible = false;
        //        lblMessage.Visible = true;


        //    }
        //    catch (Exception ex)
        //    {

        //        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "1") + " - " + ex.Message;
        //        lblMessage.Visible = true;
        //        Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
        //        hidFormSaved.Value = "2";

        //    }
        //}

   
    }
}
