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
    public partial class AddClaimCoverages : Cms.Claims.ClaimBase
    {

        System.Resources.ResourceManager objResourceMgr;
        ClsClaimCoverages objClaimCoverages = new ClsClaimCoverages();
        private String strRowId = String.Empty;
        ClsClaimCoveragesInfo objClaimCoveragesInfo;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (GetLOBID() == ((int)enumLOB.PAPEACC).ToString().ToString())
                cmbVICTIM_ID.Enabled = false;


            Ajax.Utility.RegisterTypeForAjax(typeof(AddClaimCoverages));

            base.ScreenId = "306_14_1";
            lblMessage.Visible = false;

            //START:*********** Setting permissions and class (Read/write/execute/delete)  *************
            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            btnDelete.CmsButtonClass = CmsButtonType.Write;
            btnDelete.PermissionString = gstrSecurityXML;

            //END:*********** Setting permissions and class (Read/write/execute/delete)  *************
            objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.AddClaimCoverages", System.Reflection.Assembly.GetExecutingAssembly());

         

            if (!Page.IsPostBack)
            {

                txtLIMIT_1.Attributes.Add("onblur", "javascript:this.value=formatAmount(this.value);");
                txtPOLICY_LIMIT.Attributes.Add("onblur", "javascript:this.value=formatAmount(this.value);");
                txtMINIMUM_DEDUCTIBLE.Attributes.Add("onblur", "javascript:this.value=formatAmount(this.value);");

                GetQueryStringValues();
                SetCaptions();
                SetErrorMessages();
                FillDropdowns();
                
                GetOldDataObject();


            }
        }
       
        private void FillDropdowns()
        {

            
            DataTable dtLOBs = Cms.CmsWeb.ClsFetcher.LOBs;
            cmbPRODUCT_LIST.DataSource = dtLOBs;
            cmbPRODUCT_LIST.DataTextField = "LOB_DESC";
            cmbPRODUCT_LIST.DataValueField = "LOB_ID";
            cmbPRODUCT_LIST.DataBind();

            if (hidPRODUCT_ID.Value != "")
                cmbPRODUCT_LIST.SelectedValue = hidPRODUCT_ID.Value;
            cmbLIMIT_OVERRIDE.DataSource = ClsCommon.GetLookup("YESNO");
            cmbLIMIT_OVERRIDE.DataTextField = "LookupDesc";
            cmbLIMIT_OVERRIDE.DataValueField = "LookupID";
            cmbLIMIT_OVERRIDE.DataBind();

            ClsVictimInformation ObjVictim = new ClsVictimInformation();
            cmbVICTIM_ID.DataSource = ObjVictim.GetClaimVictimList(int.Parse(hidCLAIM_ID.Value));
            cmbVICTIM_ID.DataTextField = "NAME";
            cmbVICTIM_ID.DataValueField = "VICTIM_ID";
            cmbVICTIM_ID.DataBind();

            ListItem itm = new ListItem("", "");
            cmbVICTIM_ID.Items.Insert(0, itm);
            cmbVICTIM_ID.SelectedIndex = 0;

            FillProductCoverages(int.Parse(cmbPRODUCT_LIST.SelectedValue));
            
            
        }
        private void GetOldDataObject()
        {

            //if (hidALLOW_ADD_COVERAGE.Value != "Y")
            //{
            //    btnCopy.Visible = false;
            //    //btnSave.Visible = false;
            //    //btnReset.Visible = false;

            //}

            if (hidACC_COI_FLG.Value == "Y")
                chkCOVERAGE_SI_FLAG.Enabled = true;
            else
                chkCOVERAGE_SI_FLAG.Enabled = false;

            strRowId = hidCLAIM_COV_ID.Value;
            if (strRowId.ToUpper().Equals("NEW"))
            {
                btnDelete.Visible = false;
                btnCopy.Visible = false;
                return;
            }

            

            objClaimCoveragesInfo = new ClsClaimCoveragesInfo();

            // FILL STATE AS PER THE COUNTRY BECAUSE STATE IS FILL BY USING AJAX WHICH IS CLEAR ON THE POSTBACK          

            objClaimCoveragesInfo.CLAIM_COV_ID.CurrentValue = int.Parse(hidCLAIM_COV_ID.Value);
            objClaimCoveragesInfo.CLAIM_ID.CurrentValue = int.Parse(hidCLAIM_ID.Value);

            DataTable dt = objClaimCoverages.FetchData(ref objClaimCoveragesInfo);

            if (dt != null && dt.Rows.Count > 0)
            {
               

                PopulatePageFromEbixModelObject(this.Page, objClaimCoveragesInfo);
                cmbPRODUCT_LIST.SelectedValue = hidPRODUCT_ID.Value;              
                cmbCOVERAGE_CODE_ID.SelectedValue = objClaimCoveragesInfo.COVERAGE_CODE_ID.CurrentValue.ToString();
                cmbPRODUCT_LIST.Enabled = false;
                cmbCOVERAGE_CODE_ID.Enabled = false;

                PopulatePageFromEbixModelObject(this.Page, objClaimCoveragesInfo);              
                base.SetPageModelObject(objClaimCoveragesInfo);

                //ADDED BY SANTOSH KR GAUTAM ON 11 AUG 2011 (REF ITRACK 1316)
                string DEDUCTIBLE_1_TYPE = dt.Rows[0]["DEDUCTIBLE_1_TYPE"].ToString();

                if (DEDUCTIBLE_1_TYPE != "14575")
                {
                    lblPLS_CHECK_POLICY.Text =  ClsMessages.GetMessage("306_15", "11");
                    lblPLS_CHECK_POLICY.Visible = true;
                }

                if (dt.Rows[0]["ALLOW_COPY"].ToString() == "Y")                
                    btnCopy.Visible=true;
                else
                    btnCopy.Visible = false;
                
                numberFormatInfo.NumberDecimalDigits = 2;
                if (objClaimCoveragesInfo.LIMIT_1.CurrentValue.ToString() != "")
                    txtLIMIT_1.Text = Convert.ToDouble(objClaimCoveragesInfo.LIMIT_1.CurrentValue.ToString()).ToString("N", numberFormatInfo);

                if (objClaimCoveragesInfo.MINIMUM_DEDUCTIBLE.CurrentValue.ToString() != "")
                    txtMINIMUM_DEDUCTIBLE.Text = Convert.ToDouble(objClaimCoveragesInfo.MINIMUM_DEDUCTIBLE.CurrentValue.ToString()).ToString("N", numberFormatInfo);

                if (objClaimCoveragesInfo.POLICY_LIMIT.CurrentValue.ToString() != "")
                    txtPOLICY_LIMIT.Text = Convert.ToDouble(objClaimCoveragesInfo.POLICY_LIMIT.CurrentValue.ToString()).ToString("N", numberFormatInfo);
               
               
                if(objClaimCoveragesInfo.LIMIT_OVERRIDE.CurrentValue=="Y")
                    cmbLIMIT_OVERRIDE.SelectedValue = "10963";
                else
                    cmbLIMIT_OVERRIDE.SelectedValue = "10964";


               
                if(objClaimCoveragesInfo.COVERAGE_SI_FLAG.CurrentValue == "Y")
                    chkCOVERAGE_SI_FLAG.Checked = true;
                else
                    chkCOVERAGE_SI_FLAG.Checked = false;


                if (objClaimCoveragesInfo.IS_RISK_COVERAGE.CurrentValue == "Y")
                {
                    cmbPRODUCT_LIST.Enabled = false;
                    cmbCOVERAGE_CODE_ID.Enabled = false;
                    cmbLIMIT_OVERRIDE.Enabled = false;
                    txtLIMIT_1.ReadOnly = true;
                    txtDEDUCTIBLE1_AMOUNT_TEXT.ReadOnly = true;
                    txtMINIMUM_DEDUCTIBLE.ReadOnly = true;
                    txtPOLICY_LIMIT.ReadOnly = true;                

                }
               

                if (objClaimCoveragesInfo.IS_USER_CREATED.CurrentValue == "Y")
                {
                    btnReset.Visible = true;
                    btnSave.Visible = true;
                    btnDelete.Visible = true;
                }
                else
                {
                    btnDelete.Visible = false;
                }

                // IF CLAIM IS CREATED FROM ACCEPTED COINSURANCE LOAD THEN ALLOW TO
                // MODIFY LIMIT_1 (SUM INSURED) ONLY
                if (hidACC_COI_FLG.Value == "Y")
                {
                    chkCOVERAGE_SI_FLAG.Enabled = true;
                    txtLIMIT_1.ReadOnly = false;
                    btnReset.Visible = true;
                    btnSave.Visible = true;

                }
                else
                    chkCOVERAGE_SI_FLAG.Enabled = false;
              
            }
           
        }

      
       

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int intRetval;


            ClsClaimCoveragesInfo objClaimCoveragesInfo;
            try
            {
                //For new item to add
                strRowId = hidCLAIM_COV_ID.Value;
                if (strRowId.ToUpper().Equals("NEW"))
                {

                    objClaimCoveragesInfo = new ClsClaimCoveragesInfo();
                    this.getFormValues(objClaimCoveragesInfo);

                    objClaimCoveragesInfo.CLAIM_ID.CurrentValue = int.Parse(hidCLAIM_ID.Value);//int.Parse(ViewStateClaimID);

                    objClaimCoveragesInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCUSTOMER_ID.Value);//int.Parse(hidCUSTOMER_ID.Value); //int.Parse(GetCustomerID());
                    objClaimCoveragesInfo.POLICY_ID.CurrentValue = int.Parse(hidPOLICY_ID.Value);                  // int.Parse(GetPolicyID());
                    objClaimCoveragesInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPOLICY_VERSION_ID.Value);               //int.Parse(GetPolicyVersionID());

                    objClaimCoveragesInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                    objClaimCoveragesInfo.CREATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());
                    objClaimCoveragesInfo.IS_ACTIVE.CurrentValue = "Y"; //hidIS_ACTIVE.Value;
                    objClaimCoveragesInfo.CREATE_MODE.CurrentValue = "CREATE"; //hidIS_ACTIVE.Value;


                    intRetval = objClaimCoverages.AddClaimCoverage(objClaimCoveragesInfo);

                    ClsMessages.SetCustomizedXml(GetLanguageCode());

                    if (intRetval > 0)
                    {


                        hidCLAIM_COV_ID.Value = objClaimCoveragesInfo.CLAIM_COV_ID.CurrentValue.ToString();
                        this.GetOldDataObject();
                       
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("29");
                        hidFormSaved.Value = "1";

                        
            
                    }
                    else if (intRetval == -1)
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("18");
                         hidFormSaved.Value = "2";
                    }
                    else if (intRetval == -2)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "4");
                        hidFormSaved.Value = "2";
                    }
                    else if (intRetval == -3)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "6");
                        hidFormSaved.Value = "2";
                    }
                    else if (intRetval == -4)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "8");
                        hidFormSaved.Value = "2";
                    }
                    else if (intRetval == -5)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "9");
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
                    if (txtLIMIT_1.Text == "")
                        txtLIMIT_1.Text = "0";

                    if (txtPOLICY_LIMIT.Text == "")
                        txtPOLICY_LIMIT.Text = "0";

                    if (hidACC_COI_FLG.Value == "Y" && chkCOVERAGE_SI_FLAG.Checked == false && Convert.ToDouble(txtLIMIT_1.Text.Trim(), numberFormatInfo) != Convert.ToDouble(txtPOLICY_LIMIT.Text.Trim(), numberFormatInfo))
                    {
                        // Sum insured should be equal to policy limit
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "10");
                        hidFormSaved.Value = "2";
                        lblMessage.Visible = true;
                        return;
                    }

                    objClaimCoveragesInfo = (ClsClaimCoveragesInfo)base.GetPageModelObject();
                    this.getFormValues(objClaimCoveragesInfo);
                    objClaimCoveragesInfo.IS_ACTIVE.CurrentValue = "Y"; //hidIS_ACTIVE.Value;


                    objClaimCoveragesInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCUSTOMER_ID.Value);//int.Parse(hidCUSTOMER_ID.Value); //int.Parse(GetCustomerID());
                    objClaimCoveragesInfo.POLICY_ID.CurrentValue = int.Parse(hidPOLICY_ID.Value);                  // int.Parse(GetPolicyID());
                    objClaimCoveragesInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPOLICY_VERSION_ID.Value);               //int.Parse(GetPolicyVersionID());

                    objClaimCoveragesInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                    objClaimCoveragesInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                    objClaimCoveragesInfo.LAST_UPDATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());

                    intRetval = objClaimCoverages.UpdateClaimCoverage(objClaimCoveragesInfo);
                    ClsMessages.SetCustomizedXml(GetLanguageCode());
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
                    else if (intRetval == -3)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "6");
                        hidFormSaved.Value = "2";
                    }
                    else if (intRetval == -4)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "7");
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

        private void getFormValues(ClsClaimCoveragesInfo objClaimCoveragesInfo)
        {

            try
            {

                if (cmbCOVERAGE_CODE_ID.SelectedValue != "")
                {
                    objClaimCoveragesInfo.COVERAGE_CODE_ID.CurrentValue = int.Parse(cmbCOVERAGE_CODE_ID.SelectedValue);

                    hidPRODUCT_ID.Value = cmbPRODUCT_LIST.SelectedValue;

                    if (cmbLIMIT_OVERRIDE.SelectedValue == "10963")
                        objClaimCoveragesInfo.LIMIT_OVERRIDE.CurrentValue = "Y";
                    else
                        objClaimCoveragesInfo.LIMIT_OVERRIDE.CurrentValue = "N";

                    if (!string.IsNullOrEmpty(txtMINIMUM_DEDUCTIBLE.Text))
                        objClaimCoveragesInfo.MINIMUM_DEDUCTIBLE.CurrentValue = Convert.ToDouble(txtMINIMUM_DEDUCTIBLE.Text.Trim(), numberFormatInfo);
                    else
                        objClaimCoveragesInfo.MINIMUM_DEDUCTIBLE.CurrentValue = 0;

                    if (!string.IsNullOrEmpty(txtLIMIT_1.Text))
                        objClaimCoveragesInfo.LIMIT_1.CurrentValue = Convert.ToDouble(txtLIMIT_1.Text.Trim(), numberFormatInfo);
                    else
                        objClaimCoveragesInfo.LIMIT_1.CurrentValue = 0;

                    if (!string.IsNullOrEmpty(txtPOLICY_LIMIT.Text))
                        objClaimCoveragesInfo.POLICY_LIMIT.CurrentValue = Convert.ToDouble(txtPOLICY_LIMIT.Text.Trim(), numberFormatInfo);
                    else
                        objClaimCoveragesInfo.POLICY_LIMIT.CurrentValue = 0;

                    if (!string.IsNullOrEmpty(txtDEDUCTIBLE1_AMOUNT_TEXT.Text))
                        objClaimCoveragesInfo.DEDUCTIBLE1_AMOUNT_TEXT.CurrentValue = txtDEDUCTIBLE1_AMOUNT_TEXT.Text.Trim();

                    if (cmbVICTIM_ID.SelectedValue != "")
                        objClaimCoveragesInfo.VICTIM_ID.CurrentValue = int.Parse(cmbVICTIM_ID.SelectedValue);
                    else
                        objClaimCoveragesInfo.VICTIM_ID.CurrentValue = 0;

                    if(chkCOVERAGE_SI_FLAG.Checked==true)
                        objClaimCoveragesInfo.COVERAGE_SI_FLAG.CurrentValue = "Y";
                    else
                        objClaimCoveragesInfo.COVERAGE_SI_FLAG.CurrentValue = "N";

                }

                


            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }

        private void GetQueryStringValues()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["CLAIM_COV_ID"]) && Request.QueryString["CLAIM_COV_ID"] != "NEW")
                hidCLAIM_COV_ID.Value = Request.QueryString["CLAIM_COV_ID"].ToString();
            else
                hidCLAIM_COV_ID.Value = "NEW";

            if (Request.QueryString["CLAIM_ID"] != null && Request.QueryString["CLAIM_ID"].ToString() != "")
                hidCLAIM_ID.Value = Request.QueryString["CLAIM_ID"].ToString();


            if (Request.QueryString["PRODUCT_ID"] != null && Request.QueryString["PRODUCT_ID"].ToString() != "")
                hidPRODUCT_ID.Value = Request.QueryString["PRODUCT_ID"].ToString();
            else
                hidPRODUCT_ID.Value = GetLOBID();


            if (Request.QueryString["ALLOW_ADD_COVERAGE"] != null && Request.QueryString["ALLOW_ADD_COVERAGE"].ToString() != "")
                hidALLOW_ADD_COVERAGE.Value = Request.QueryString["ALLOW_ADD_COVERAGE"].ToString();
                    

            if (Request.QueryString["CUSTOMER_ID"] != null && Request.QueryString["CUSTOMER_ID"].ToString() != "")
                hidCUSTOMER_ID.Value = Request.QueryString["CUSTOMER_ID"].ToString();

            if (Request.QueryString["POLICY_ID"] != null && Request.QueryString["POLICY_ID"].ToString() != "")
                hidPOLICY_ID.Value = Request.QueryString["POLICY_ID"].ToString();

            if (Request.QueryString["POLICY_VERSION_ID"] != null && Request.QueryString["POLICY_VERSION_ID"].ToString() != "")
                hidPOLICY_VERSION_ID.Value = Request.QueryString["POLICY_VERSION_ID"].ToString();

            if (Request.QueryString["LOB_ID"] != null && Request.QueryString["LOB_ID"].ToString() != "")
                hidLOB_ID.Value = Request.QueryString["LOB_ID"].ToString();

            if (Request.QueryString["ACC_COI_FLG"] != null && Request.QueryString["ACC_COI_FLG"].ToString() != "")
                hidACC_COI_FLG.Value = Request.QueryString["ACC_COI_FLG"].ToString();

        }

        private void SetCaptions()
        {
            capCOVERAGE_CODE_ID.Text = objResourceMgr.GetString("cmbCOVERAGE_CODE_ID");
            capMINIMUM_DEDUCTIBLE.Text = objResourceMgr.GetString("txtMINIMUM_DEDUCTIBLE");
            capDEDUCTIBLE1_AMOUNT_TEXT.Text = objResourceMgr.GetString("txtDEDUCTIBLE1_AMOUNT_TEXT");
            capLIMIT_1.Text = objResourceMgr.GetString("txtLIMIT_1");
            capLIMIT_OVERRIDE.Text = objResourceMgr.GetString("cmbLIMIT_OVERRIDE");
            capPOLICY_LIMIT.Text = objResourceMgr.GetString("txtPOLICY_LIMIT");
            capPRODUCT_LIST.Text = objResourceMgr.GetString("cmbPRODUCT_LIST");
            capVICTIM_ID.Text = objResourceMgr.GetString("cmbVICTIM_ID");
            capCOVERAGE_SI_FLAG.Text = objResourceMgr.GetString("chkCOVERAGE_SI_FLAG");
            btnCopy.Text = ClsMessages.GetMessage(base.ScreenId, "5");
            lblRequiredFieldsInformation.Text = objResourceMgr.GetString("lblRequiredFieldsInformation");

        }

        private void SetErrorMessages()
        {
           // revCPF_CNPJ.ValidationExpression = aRegExpCpf_Cnpj;

            ClsMessages.SetCustomizedXml(GetLanguageCode());

            //rfvDEDUCTIBLE_1.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "1");
            //revCPF_CNPJ.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "2");
            //rfvJUDICIAL_PROCESS_NO.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "3");
            //rfvJUDICIAL_COMPLAINT_STATE.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "4");

           
            //revJUDICIAL_PROCESS_DATE.ErrorMessage = ClsMessages.FetchGeneralMessage("22");
            //revPLAINTIFF_REQUESTED_AMOUNT.ValidationExpression = aRegExpCurrencyformat;
            //revPLAINTIFF_REQUESTED_AMOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
            revPOLICY_LIMIT.ValidationExpression = aRegExpCurrencyformat;
            revPOLICY_LIMIT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");

            revLIMIT_1.ValidationExpression = aRegExpCurrencyformat;
            revLIMIT_1.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");

            revMINIMUM_DEDUCTIBLE.ValidationExpression = aRegExpCurrencyformat;
            revMINIMUM_DEDUCTIBLE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");

            rfvCOVERAGE_CODE_ID.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "1");

            csvLIMIT1.ErrorMessage = ClsMessages.FetchGeneralMessage("1776");//Value is exceeding maximum limit 
            csvPOLICY_LIMIT.ErrorMessage = ClsMessages.FetchGeneralMessage("1776");//Value is exceeding maximum limit 
            //rfvCOVERAGE_CODE_ID.ErrorMessage = "Please select coverage";
            

            //revESTIMATE_CLASSIFICATION.ValidationExpression = aRegExpInteger;
           
            //revESTIMATE_CLASSIFICATION.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "5");
            //csvESTIMATE_CLASSIFICATION.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "5");
            

        }

        protected void cmbCOVERAGE_CODE_ID_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cmbPRODUCT_LIST_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPRODUCT_LIST.SelectedValue != "")
            {
                FillProductCoverages(int.Parse(cmbPRODUCT_LIST.SelectedValue));
            }
        }

        private void FillProductCoverages(int ProductID)
        {

             string FetchMode="";
            if (hidCLAIM_COV_ID.Value.ToUpper().Equals("NEW"))
                FetchMode = "NEW";
            else
                FetchMode = "OLD";

            DataTable dt = objClaimCoverages.GetProductCoverages(ProductID, int.Parse(hidCLAIM_ID.Value), int.Parse(GetLanguageID()), FetchMode); ;

            cmbCOVERAGE_CODE_ID.DataSource = dt;
            cmbCOVERAGE_CODE_ID.DataValueField = "COV_ID";
            cmbCOVERAGE_CODE_ID.DataTextField = "COV_DES";
            cmbCOVERAGE_CODE_ID.DataBind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {

            ClsClaimCoveragesInfo objClaimCoveragesInfo = (ClsClaimCoveragesInfo)base.GetPageModelObject();
            objClaimCoveragesInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCUSTOMER_ID.Value);//int.Parse(hidCUSTOMER_ID.Value); //int.Parse(GetCustomerID());
            objClaimCoveragesInfo.POLICY_ID.CurrentValue = int.Parse(hidPOLICY_ID.Value);                  // int.Parse(GetPolicyID());
            objClaimCoveragesInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPOLICY_VERSION_ID.Value);               //int.Parse(GetPolicyVersionID());

            lblMessage.Visible = true;
            ClsMessages.SetCustomizedXml(GetLanguageCode());
            if (objClaimCoverages.DeleteUserCreateCoverage(objClaimCoveragesInfo) > 0)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "3");
              
            }
            else
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "2");
                hidFormSaved.Value = "1";

                btnDelete.Visible = false;
                btnReset.Visible = false;
                btnSave.Visible = false;
                btnCopy.Visible = false;
            }
        }

        protected void btnCopy_Click(object sender, EventArgs e)
        {
            //For new item to add
            strRowId = hidCLAIM_COV_ID.Value;
            int intRetval = 0;
            if (strRowId!="NEW")
            {

                objClaimCoveragesInfo = new ClsClaimCoveragesInfo();
                this.getFormValues(objClaimCoveragesInfo);

                objClaimCoveragesInfo.CLAIM_ID.CurrentValue = int.Parse(hidCLAIM_ID.Value);//int.Parse(ViewStateClaimID);

                objClaimCoveragesInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCUSTOMER_ID.Value); //int.Parse(GetCustomerID());
                objClaimCoveragesInfo.POLICY_ID.CurrentValue = int.Parse(hidPOLICY_ID.Value);                  // int.Parse(GetPolicyID());
                objClaimCoveragesInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPOLICY_VERSION_ID.Value);               //int.Parse(GetPolicyVersionID());

                objClaimCoveragesInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                objClaimCoveragesInfo.CREATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());
                objClaimCoveragesInfo.IS_ACTIVE.CurrentValue = "Y"; //hidIS_ACTIVE.Value;
                objClaimCoveragesInfo.CREATE_MODE.CurrentValue = "COPY";
                objClaimCoveragesInfo.VICTIM_ID.CurrentValue = 0;

                intRetval = objClaimCoverages.AddClaimCoverage(objClaimCoveragesInfo);



                if (intRetval > 0)
                {

                    ClsMessages.SetCustomizedXml(GetLanguageCode());
                    hidCLAIM_COV_ID.Value = objClaimCoveragesInfo.CLAIM_COV_ID.CurrentValue.ToString();
                    this.GetOldDataObject();

                    lblMessage.Text = ClsMessages.FetchGeneralMessage("29");
                    hidFormSaved.Value = "1";



                }
                else if (intRetval == -1)
                {
                    lblMessage.Text = ClsMessages.FetchGeneralMessage("18");
                    hidFormSaved.Value = "2";
                }
                else if (intRetval == -2)
                {
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "4");
                    hidFormSaved.Value = "2";
                }
                else if (intRetval == -3)
                {
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "6");
                    hidFormSaved.Value = "2";
                }
                else if (intRetval == -4)
                {
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "8");
                    hidFormSaved.Value = "2";
                }
                else if (intRetval == -5)
                {
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "9");
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
       
    }
}
