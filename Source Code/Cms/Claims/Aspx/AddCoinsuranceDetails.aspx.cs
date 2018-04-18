using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cms.ExceptionPublisher;
using Cms.BusinessLayer.BLClaims;
using Cms.BusinessLayer.BlCommon;
using System.Data;
using System.Globalization;
using Cms.CmsWeb.Controls;
using Cms.CmsWeb;

namespace Cms.Claims.Aspx
{
    public partial class AddCoinsuranceDetails : Cms.Claims.ClaimBase
    {
        System.Resources.ResourceManager objResourceMgr;
        ClsCoinsuranceDetails objCoinsuranceDetails = new ClsCoinsuranceDetails();
        private String strRowId = String.Empty;
        public NumberFormatInfo nfi;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (GetPolicyCurrency() != String.Empty && GetPolicyCurrency() == enumCurrencyId.BR)
                nfi = new CultureInfo(enumCulture.BR, true).NumberFormat;
            else
                nfi = new CultureInfo(enumCulture.US, true).NumberFormat;

            base.ScreenId = "306_14";
            lblMessage.Visible = false;

            //START:*********** Setting permissions and class (Read/write/execute/delete)  *************
            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            //END:*********** Setting permissions and class (Read/write/execute/delete)  *************
            objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.AddCoinsuranceDetails", System.Reflection.Assembly.GetExecutingAssembly());

            if (!Page.IsPostBack)
            {


                hlkCLAIM_REGISTRATION_DATE.Attributes.Add("OnClick", "fPopCalendar(txtCLAIM_REGISTRATION_DATE,txtCLAIM_REGISTRATION_DATE)");
                
                // Added by Santosh Kumar Gautam on 13 Jul 2011
                cmbLITIGATION_FILE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
                cmbLITIGATION_FILE.DataTextField = "LookupDesc";
                cmbLITIGATION_FILE.DataValueField = "LookupID";
                cmbLITIGATION_FILE.DataBind();

                GetQueryStringValues();
                SetCaptions();
                SetErrorMessages();    
                GetOldDataObject();


            }

        }

        private void GetQueryStringValues()
        {
           

            if (Request.QueryString["CLAIM_ID"] != null && Request.QueryString["CLAIM_ID"].ToString() != "")
                hidCLAIM_ID.Value = Request.QueryString["CLAIM_ID"].ToString();

            if (Request.QueryString["CUSTOMER_ID"] != null && Request.QueryString["CUSTOMER_ID"].ToString() != "")
                hidCUSTOMER_ID.Value = Request.QueryString["CUSTOMER_ID"].ToString();

            if (Request.QueryString["POLICY_ID"] != null && Request.QueryString["POLICY_ID"].ToString() != "")
                hidPOLICY_ID.Value = Request.QueryString["POLICY_ID"].ToString();

            if (Request.QueryString["POLICY_VERSION_ID"] != null && Request.QueryString["POLICY_VERSION_ID"].ToString() != "")
                hidPOLICY_VERSION_ID.Value = Request.QueryString["POLICY_VERSION_ID"].ToString();

            if (Request.QueryString["LOB_ID"] != null && Request.QueryString["LOB_ID"].ToString() != "")
                hidLOB_ID.Value = Request.QueryString["LOB_ID"].ToString();

            if (Request.QueryString["LITIGATION_FILE"] != null && Request.QueryString["LITIGATION_FILE"] != "")
                hidLitigation.Value = Request.QueryString["LITIGATION_FILE"].ToString();


            if (Request.QueryString["POLICY_EFFECTIVE_DATE"] != null && Request.QueryString["POLICY_EFFECTIVE_DATE"] != "")
                hidEffDate.Value = Request.QueryString["POLICY_EFFECTIVE_DATE"].ToString();

            if (Request.QueryString["POLICY_EXPIRATION_DATE"] != null && Request.QueryString["POLICY_EXPIRATION_DATE"] != "")
                hidExpDate.Value = Request.QueryString["POLICY_EXPIRATION_DATE"].ToString();


            if (hidLitigation.Value == "10963")
                hidLitigation.Value = "Y";
            else
                hidLitigation.Value = "N";


        }

        private void SetCaptions()
        {

            capLEADER_CLAIM_NUMBER.Text = objResourceMgr.GetString("txtLEADER_CLAIM_NUMBER");
            capCLAIM_REGISTRATION_DATE.Text = objResourceMgr.GetString("txtCLAIM_REGISTRATION_DATE");
            capLEADER_ENDORSEMENT_NUMBER.Text = objResourceMgr.GetString("txtLEADER_ENDORSEMENT_NUMBER");
            capLEADER_POLICY_NUMBER.Text = objResourceMgr.GetString("txtLEADER_POLICY_NUMBER");
            capLEADER_SUSEP_CODE.Text = objResourceMgr.GetString("txtLEADER_SUSEP_CODE");
            capLITIGATION_FILE.Text = objResourceMgr.GetString("cmbLITIGATION_FILE");
           

            lblRequiredFieldsInformation.Text = objResourceMgr.GetString("lblRequiredFieldsInformation");

            hidFutureDateMsg.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "3");

            if(hidLitigation.Value=="Y")
                hidPolicyExpiryDateMsg.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
            else
                hidPolicyExpiryDateMsg.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "4");

        }

        private void SetErrorMessages()
        {
            ClsMessages.SetCustomizedXml(GetLanguageCode());
            revCLAIM_REGISTRATION_DATE.ValidationExpression = aRegExpDate;
            revCLAIM_REGISTRATION_DATE.ErrorMessage = ClsMessages.FetchGeneralMessage("22");
            rfvLEADER_POLICY_NUMBER.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "2");
            rfvLEADER_SUSEP_CODE.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "1");
           
           
        }

        private void GetOldDataObject()
        {
           
            ClsCoinsuranceInfo objCoinsuranceInfo = new ClsCoinsuranceInfo();

            // FILL STATE AS PER THE COUNTRY BECAUSE STATE IS FILL BY USING AJAX WHICH IS CLEAR ON THE POSTBACK          

            objCoinsuranceInfo.CLAIM_ID.CurrentValue = int.Parse(hidCLAIM_ID.Value);
            DataSet  ds = objCoinsuranceDetails.GetClaimCoinsuranceDetails(ref objCoinsuranceInfo);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];

                hidCOINSURANCE_ID.Value = dt.Rows[0]["COINSURANCE_ID"].ToString();

                //ClsCommon.PopulateEbixPageModel(dt, objCoinsuranceInfo);

                PopulatePageFromEbixModelObject(this.Page, objCoinsuranceInfo);

                //hidLITIGATION_ID.Value = dt.Rows[0]["JUDICIAL_COMPLAINT_STATE"].ToString();
                //hidLITIGATION_ID.Value = dt.Rows[0]["LITIGATION_ID"].ToString();
                base.SetPageModelObject(objCoinsuranceInfo);

                // ADDED BY SANTOSH KR GAUTAM ON 13 JULY 2011 (REF ITRACK :1044)
                if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    if (ds.Tables[1].Rows[0]["IS_RESERVE_CREATED"].ToString() == "Y")
                        cmbLITIGATION_FILE.Enabled = false;
                    else
                        cmbLITIGATION_FILE.Enabled = true;
                }
                
            }

            

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int intRetval;

            ClsCoinsuranceInfo objCoinsuranceInfo;
            try
            {
                //For new item to add
                if (hidCOINSURANCE_ID.Value == "0")
                {

                    objCoinsuranceInfo = new ClsCoinsuranceInfo();
                    this.getFormValues(objCoinsuranceInfo);

                    objCoinsuranceInfo.CLAIM_ID.CurrentValue = int.Parse(hidCLAIM_ID.Value);

                    //objRiskInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCUSTOMER_ID.Value); //int.Parse(GetCustomerID());
                    //objRiskInfo.POLICY_ID.CurrentValue = int.Parse(hidPOLICY_ID.Value);                  // int.Parse(GetPolicyID());
                    //objRiskInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPOLICY_VERSION_ID.Value);               //int.Parse(GetPolicyVersionID());

                    objCoinsuranceInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                    objCoinsuranceInfo.CREATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());
                   // objRiskInfo.IS_ACTIVE.CurrentValue = "Y"; //hidIS_ACTIVE.Value;


                    intRetval = objCoinsuranceDetails.AddClaimCoinsuranceDetails(objCoinsuranceInfo);

                    if (intRetval > 0)
                    {

                        this.GetOldDataObject();                       
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("29");
                      
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
                }
                else //For The Update cse
                {
                    
                     objCoinsuranceInfo = (ClsCoinsuranceInfo)base.GetPageModelObject();
                     this.getFormValues(objCoinsuranceInfo);

                     objCoinsuranceInfo.COINSURANCE_ID.CurrentValue = int.Parse(hidCOINSURANCE_ID.Value);
                     objCoinsuranceInfo.CLAIM_ID.CurrentValue = int.Parse(hidCLAIM_ID.Value);
                     objCoinsuranceInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                     objCoinsuranceInfo.LAST_UPDATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());

                     intRetval = objCoinsuranceDetails.UpdateClaimCoinsuranceDetails(objCoinsuranceInfo);

                    if (intRetval > 0)
                    {
                        this.GetOldDataObject();
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("31");
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
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
              
            }
            finally
            {
                
            }
        }




        private void getFormValues(ClsCoinsuranceInfo objCoinsuranceInfo)
        {
          
            try
            {
                // DAMAGE DESC IS COMMON TO ALL 
                
                 objCoinsuranceInfo.LEADER_SUSEP_CODE.CurrentValue = txtLEADER_SUSEP_CODE.Text.Trim();
                 objCoinsuranceInfo.LEADER_POLICY_NUMBER.CurrentValue = txtLEADER_POLICY_NUMBER.Text.Trim();
                 objCoinsuranceInfo.LEADER_ENDORSEMENT_NUMBER.CurrentValue = txtLEADER_ENDORSEMENT_NUMBER.Text.Trim();
                 objCoinsuranceInfo.LEADER_CLAIM_NUMBER.CurrentValue = txtLEADER_CLAIM_NUMBER.Text.Trim();

                 objCoinsuranceInfo.LEADER_POLICY_NUMBER.CurrentValue = txtLEADER_POLICY_NUMBER.Text.Trim();

                 objCoinsuranceInfo.LITIGATION_FILE.CurrentValue = int.Parse(cmbLITIGATION_FILE.SelectedValue);

                if (!string.IsNullOrEmpty(txtCLAIM_REGISTRATION_DATE.Text))
                    objCoinsuranceInfo.CLAIM_REGISTRATION_DATE.CurrentValue = ConvertToDate(txtCLAIM_REGISTRATION_DATE.Text.Trim());

            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }

    }
}
