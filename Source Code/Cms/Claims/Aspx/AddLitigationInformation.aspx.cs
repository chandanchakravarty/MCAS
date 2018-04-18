using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cms.Model.Claims;
using Cms.CmsWeb.Controls;
using Cms.CmsWeb;
using Cms.ExceptionPublisher;
using Cms.BusinessLayer.BLClaims;
using Cms.BusinessLayer.BlCommon;
using System.Data;
using System.Globalization;

namespace Cms.Claims.Aspx
{
    public partial class AddLitigationInformation : Cms.Claims.ClaimBase
    {
        System.Resources.ResourceManager objResourceMgr;
        ClsLitigationInformation objLitigationInformation = new ClsLitigationInformation();
        private String strRowId = String.Empty;
        public NumberFormatInfo nfi;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (GetPolicyCurrency() != String.Empty && GetPolicyCurrency() == enumCurrencyId.BR)
                nfi = new CultureInfo(enumCulture.BR, true).NumberFormat;
            else
                nfi = new CultureInfo(enumCulture.US, true).NumberFormat;
            SetCultureThread(GetLanguageCode()); // Added by aditya for itrack # 1503 on 09-08-2011
            base.ScreenId = "306_13_1";
            lblMessage.Visible = false;

            //START:*********** Setting permissions and class (Read/write/execute/delete)  *************
            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            btnActivateDeactivate.CmsButtonClass = CmsButtonType.Write;
            btnActivateDeactivate.PermissionString = gstrSecurityXML;


            
            //END:*********** Setting permissions and class (Read/write/execute/delete)  *************
            objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.AddLitigationInformation", System.Reflection.Assembly.GetExecutingAssembly());

         

            if (!Page.IsPostBack)
            {

                hlkJUDICIAL_PROCESS_DATE.Attributes.Add("OnClick", "fPopCalendar(txtJUDICIAL_PROCESS_DATE,txtJUDICIAL_PROCESS_DATE)");
                txtDEFEDANT_OFFERED_AMOUNT.Attributes.Add("onblur", "javascript:this.value=formatAmount(this.value);");
                txtPLAINTIFF_REQUESTED_AMOUNT.Attributes.Add("onblur", "javascript:this.value=formatAmount(this.value);");

                SetCaptions();
                SetErrorMessages();
                FillDropdowns();
                GetQueryStringValues();
                GetOldDataObject();


            }
        }
       
        private void FillDropdowns()
        {
            cmbOPERATION_REASON.DataSource = ClsCommon.GetLookup("OPERSN");
            cmbOPERATION_REASON.DataTextField = "LookupDesc";
            cmbOPERATION_REASON.DataValueField = "LookupID";
            cmbOPERATION_REASON.DataBind();
            cmbOPERATION_REASON.Items.Insert(0, "");


            ClsStates objStates = new ClsStates();
            DataSet ds = objStates.GetStatesCountry(Brazil_Country_ID);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                cmbJUDICIAL_COMPLAINT_STATE.DataSource = ds;
                cmbJUDICIAL_COMPLAINT_STATE.DataTextField = STATE_NAME;
                cmbJUDICIAL_COMPLAINT_STATE.DataValueField = STATE_ID;
                cmbJUDICIAL_COMPLAINT_STATE.DataBind();
            }

            DataTable dt = objLitigationInformation.GetClaimExpertServiceProvider();
            if (dt.Rows.Count > 0)
            {
                cmbEXPERT_SERVICE_ID.DataSource = dt;
                cmbEXPERT_SERVICE_ID.DataTextField = "EXPERT_SERVICE_NAME";
                cmbEXPERT_SERVICE_ID.DataValueField = "EXPERT_SERVICE_ID";
                cmbEXPERT_SERVICE_ID.DataBind();
                cmbEXPERT_SERVICE_ID.Items.Insert(0, "");
            }
        }
        private void GetOldDataObject()
        {
            strRowId = hidLITIGATION_ID.Value;
            if (strRowId.ToUpper().Equals("NEW"))
            {
                btnActivateDeactivate.Visible = false;
                btnSave.Visible = true;
                return;
            }

            ClsLitigationInfo objLitigationInfo = new ClsLitigationInfo();

            // FILL STATE AS PER THE COUNTRY BECAUSE STATE IS FILL BY USING AJAX WHICH IS CLEAR ON THE POSTBACK          

            objLitigationInfo.LITIGATION_ID.CurrentValue = int.Parse(hidLITIGATION_ID.Value);
            DataTable dt = objLitigationInformation.FetchData(ref objLitigationInfo);

            if (dt != null && dt.Rows.Count > 0)
            {
                hidLITIGATION_ID.Value = dt.Rows[0]["LITIGATION_ID"].ToString();


                PopulatePageFromEbixModelObject(this.Page, objLitigationInfo);

                //hidLITIGATION_ID.Value = dt.Rows[0]["JUDICIAL_COMPLAINT_STATE"].ToString();
                //hidLITIGATION_ID.Value = dt.Rows[0]["LITIGATION_ID"].ToString();
                base.SetPageModelObject(objLitigationInfo);

                nfi.NumberDecimalDigits = 2;
                string DEFEDANT_OFFERED_AMOUNT= objLitigationInfo.DEFEDANT_OFFERED_AMOUNT.CurrentValue.ToString();
                if (DEFEDANT_OFFERED_AMOUNT != "" && DEFEDANT_OFFERED_AMOUNT != "0" && DEFEDANT_OFFERED_AMOUNT != "0.0" && DEFEDANT_OFFERED_AMOUNT != "0,0")
                    txtDEFEDANT_OFFERED_AMOUNT.Text = Convert.ToDouble(objLitigationInfo.DEFEDANT_OFFERED_AMOUNT.CurrentValue.ToString()).ToString("N", nfi);
                else
                    txtDEFEDANT_OFFERED_AMOUNT.Text = "";


                string PLAINTIFF_REQUESTED_AMOUNT = objLitigationInfo.PLAINTIFF_REQUESTED_AMOUNT.CurrentValue.ToString();
                if (PLAINTIFF_REQUESTED_AMOUNT != "" && PLAINTIFF_REQUESTED_AMOUNT != "0" && PLAINTIFF_REQUESTED_AMOUNT != "0.0" && PLAINTIFF_REQUESTED_AMOUNT != "0,0")                    
                    txtPLAINTIFF_REQUESTED_AMOUNT.Text = Convert.ToDouble(objLitigationInfo.PLAINTIFF_REQUESTED_AMOUNT.CurrentValue.ToString()).ToString("N", nfi);
                else
                    txtPLAINTIFF_REQUESTED_AMOUNT.Text = "";

                if(objLitigationInfo.IS_ACTIVE.CurrentValue!="Y")
                    btnSave.Visible = false;
                else
                    btnSave.Visible = true;

                btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objLitigationInfo.IS_ACTIVE.CurrentValue.ToString().Trim());
                btnActivateDeactivate.Visible = true;
            }
           
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int intRetval;


            ClsLitigationInfo objLitigationInfo;
            try
            {
                //For new item to add
                strRowId = hidLITIGATION_ID.Value;
                if (strRowId.ToUpper().Equals("NEW"))
                {

                    objLitigationInfo = new ClsLitigationInfo();
                    this.getFormValues(objLitigationInfo);

                    objLitigationInfo.CLAIM_ID.CurrentValue = int.Parse(hidCLAIM_ID.Value);//int.Parse(ViewStateClaimID);

                    objLitigationInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCUSTOMER_ID.Value); //int.Parse(GetCustomerID());
                    objLitigationInfo.POLICY_ID.CurrentValue = int.Parse(hidPOLICY_ID.Value);                  // int.Parse(GetPolicyID());
                    objLitigationInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPOLICY_VERSION_ID.Value);               //int.Parse(GetPolicyVersionID());

                    objLitigationInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                    objLitigationInfo.CREATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());
                    objLitigationInfo.IS_ACTIVE.CurrentValue = "Y"; //hidIS_ACTIVE.Value;


                    intRetval = objLitigationInformation.AddLitigationInformation(objLitigationInfo);
                    hidLITIGATION_ID.Value = objLitigationInfo.LITIGATION_ID.CurrentValue.ToString();


                    if (intRetval > 0)
                    {

                       
                        hidLITIGATION_ID.Value = objLitigationInfo.LITIGATION_ID.CurrentValue.ToString();
                        this.GetOldDataObject();
                       
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("29");
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
                }
                else //For The Update cse
                {

                    objLitigationInfo = (ClsLitigationInfo)base.GetPageModelObject();
                    this.getFormValues(objLitigationInfo);
                    objLitigationInfo.IS_ACTIVE.CurrentValue = "Y"; //hidIS_ACTIVE.Value;

                    objLitigationInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCUSTOMER_ID.Value); //int.Parse(GetCustomerID());
                    objLitigationInfo.POLICY_ID.CurrentValue = int.Parse(hidPOLICY_ID.Value);                  // int.Parse(GetPolicyID());
                    objLitigationInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPOLICY_VERSION_ID.Value);               //int.Parse(GetPolicyVersionID());

                    objLitigationInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                    objLitigationInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                    objLitigationInfo.LAST_UPDATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());

                    intRetval = objLitigationInformation.UpdateLitigationInformation(objLitigationInfo);

                    if (intRetval > 0)
                    {
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
                //if (objNamedPerilsinfo != null)
                //    objNamedPerilsinfo.Dispose();
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {

        }

        private void getFormValues(ClsLitigationInfo objLitigationInfo)
        {
          
            try
            {

                objLitigationInfo.JUDICIAL_PROCESS_NO.CurrentValue = txtJUDICIAL_PROCESS_NO.Text.Trim();

                if (!string.IsNullOrEmpty(cmbJUDICIAL_COMPLAINT_STATE.SelectedValue))
                    objLitigationInfo.JUDICIAL_COMPLAINT_STATE.CurrentValue = int.Parse(cmbJUDICIAL_COMPLAINT_STATE.SelectedValue);
                else
                    objLitigationInfo.JUDICIAL_COMPLAINT_STATE.CurrentValue = 0;

                if (!string.IsNullOrEmpty(txtESTIMATE_CLASSIFICATION.Text))
                    objLitigationInfo.ESTIMATE_CLASSIFICATION.CurrentValue = int.Parse(txtESTIMATE_CLASSIFICATION.Text);
                else
                    objLitigationInfo.ESTIMATE_CLASSIFICATION.CurrentValue = 0;

                if (!string.IsNullOrEmpty(cmbOPERATION_REASON.SelectedValue))
                    objLitigationInfo.OPERATION_REASON.CurrentValue = int.Parse(cmbOPERATION_REASON.SelectedValue);
                else
                    objLitigationInfo.OPERATION_REASON.CurrentValue = 0;

               
                objLitigationInfo.PLAINTIFF_CPF.CurrentValue = txtPLAINTIFF_CPF.Text;               
                objLitigationInfo.PLAINTIFF_NAME.CurrentValue =txtPLAINTIFF_NAME.Text;

               if (!string.IsNullOrEmpty(txtPLAINTIFF_REQUESTED_AMOUNT.Text))
                   objLitigationInfo.PLAINTIFF_REQUESTED_AMOUNT.CurrentValue = Convert.ToDouble(txtPLAINTIFF_REQUESTED_AMOUNT.Text.Trim(), nfi);
               else
                   objLitigationInfo.PLAINTIFF_REQUESTED_AMOUNT.CurrentValue = 0;

               if (!string.IsNullOrEmpty(txtDEFEDANT_OFFERED_AMOUNT.Text))
                   objLitigationInfo.DEFEDANT_OFFERED_AMOUNT.CurrentValue = Convert.ToDouble(txtDEFEDANT_OFFERED_AMOUNT.Text, nfi);
                else
                   objLitigationInfo.DEFEDANT_OFFERED_AMOUNT.CurrentValue = 0;

               if (!string.IsNullOrEmpty(txtJUDICIAL_PROCESS_DATE.Text))
                   objLitigationInfo.JUDICIAL_PROCESS_DATE.CurrentValue = ConvertToDate(txtJUDICIAL_PROCESS_DATE.Text.Trim());
               else
                   objLitigationInfo.JUDICIAL_PROCESS_DATE.CurrentValue = DateTime.MinValue;


               if (!string.IsNullOrEmpty(cmbEXPERT_SERVICE_ID.SelectedValue))
                   objLitigationInfo.EXPERT_SERVICE_ID.CurrentValue = int.Parse(cmbEXPERT_SERVICE_ID.SelectedValue);
               else
                   objLitigationInfo.EXPERT_SERVICE_ID.CurrentValue = 0;
            


            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }

        private void GetQueryStringValues()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["LITIGATION_ID"]) && Request.QueryString["LITIGATION_ID"] != "NEW")
                hidLITIGATION_ID.Value = Request.QueryString["LITIGATION_ID"].ToString();
            else
                hidLITIGATION_ID.Value = "NEW";

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

        }

        private void SetCaptions()
        {

            capDEFEDANT_OFFERED_AMOUNT.Text = objResourceMgr.GetString("txtDEFEDANT_OFFERED_AMOUNT");
            capESTIMATE_CLASSIFICATION.Text = objResourceMgr.GetString("txtESTIMATE_CLASSIFICATION");
            capJUDICIAL_COMPLAINT_STATE.Text = objResourceMgr.GetString("cmbJUDICIAL_COMPLAINT_STATE");
            capJUDICIAL_PROCESS_NO.Text = objResourceMgr.GetString("txtJUDICIAL_PROCESS_NO");
            capOPERATION_REASON.Text = objResourceMgr.GetString("cmbOPERATION_REASON");
            capPLAINTIFF_CPF.Text = objResourceMgr.GetString("txtPLAINTIFF_CPF");
            capPLAINTIFF_NAME.Text = objResourceMgr.GetString("txtPLAINTIFF_NAME");
            capPLAINTIFF_REQUESTED_AMOUNT.Text = objResourceMgr.GetString("txtPLAINTIFF_REQUESTED_AMOUNT");
            capJUDICIAL_PROCESS_DATE.Text = objResourceMgr.GetString("txtJUDICIAL_PROCESS_DATE");
            capEXPERT_SERVICE_ID.Text = objResourceMgr.GetString("cmbEXPERT_SERVICE_ID");   

            lblRequiredFieldsInformation.Text = objResourceMgr.GetString("lblRequiredFieldsInformation");

        }

        private void SetErrorMessages()
        {
            ClsMessages.SetCustomizedXml(GetLanguageCode());
            revCPF_CNPJ.ValidationExpression = aRegExpCpf_Cnpj;
            revJUDICIAL_PROCESS_DATE.ValidationExpression = aRegExpDate;
           

            rfvCPF_CNPJ.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "1");
            revCPF_CNPJ.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "2");
            rfvJUDICIAL_PROCESS_NO.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "3");
            rfvJUDICIAL_COMPLAINT_STATE.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "4");
            rfvJUDICIAL_PROCESS_DATE.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "8"); // Added by aditya for itrack # 1503 on 09-08-2011
            rfvPLAINTIFF_NAME.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "9"); // Added by aditya for itrack # 1503 on 09-08-2011
            rfvPLAINTIFF_REQUESTED_AMOUNT.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "10"); // Added by aditya for itrack # 1503 on 09-08-2011
            rfvDEFEDANT_OFFERED_AMOUNT.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "11"); // Added by aditya for itrack # 1503 on 09-08-2011
            rfvESTIMATE_CLASSIFICATION.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "12"); // Added by aditya for itrack # 1503 on 09-08-2011
            rfvOPERATION_REASON.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "13");  // Added by aditya for itrack # 1503 on 09-08-2011
            
           
            revJUDICIAL_PROCESS_DATE.ErrorMessage = ClsMessages.FetchGeneralMessage("22");
            revPLAINTIFF_REQUESTED_AMOUNT.ValidationExpression = aRegExpCurrencyformat;
            //revPLAINTIFF_REQUESTED_AMOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
            revDEFEDANT_OFFERED_AMOUNT.ValidationExpression = aRegExpCurrencyformat;                                                              
            //revDEFEDANT_OFFERED_AMOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");

            csvDEFEDANT_OFFERED_AMOUNT.ErrorMessage = ClsMessages.FetchGeneralMessage("224_25_6");
            csvPLAINTIFF_REQUESTED_AMOUNT.ErrorMessage = ClsMessages.FetchGeneralMessage("224_25_6");

            revESTIMATE_CLASSIFICATION.ValidationExpression = aRegExpInteger;
           
            revESTIMATE_CLASSIFICATION.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "5");
            csvESTIMATE_CLASSIFICATION.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "5");
            

        }

        protected void btnActivateDeactivate_Click(object sender, EventArgs e)
        {
            ClsLitigationInfo objLitigationInfo = (ClsLitigationInfo)base.GetPageModelObject();
            lblMessage.Visible = true;
            if (hidIS_ACTIVE.Value == "Y")  // DEACTIVATE
            {
                objLitigationInfo.IS_ACTIVE.CurrentValue = "N";

            }
            else  // ACTIVATE
            {
                objLitigationInfo.IS_ACTIVE.CurrentValue = "Y";

            }


            if (objLitigationInformation.ActivateDeactivate(objLitigationInfo) > 0)
            {
                hidFormSaved.Value = "1";

                if (hidIS_ACTIVE.Value == "Y") //DEACTIVATED SUCCESSFULLE
                {
                    hidIS_ACTIVE.Value = "N";
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "7");                    
                    btnSave.Visible = false;
                }
                else                          //ACTIVATED SUCCESSFULLE                
                {
                    hidIS_ACTIVE.Value = "Y";
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "6");                   
                    btnSave.Visible = true;
                }
                btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objLitigationInfo.IS_ACTIVE.CurrentValue.ToString().Trim());
            }
            else
            {

                lblMessage.Text = ClsMessages.FetchGeneralMessage("18");
                hidFormSaved.Value = "2";


            }
        }


       
    }
}
