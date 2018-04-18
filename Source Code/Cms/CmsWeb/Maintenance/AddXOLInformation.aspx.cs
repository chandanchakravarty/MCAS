using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cms.Model.Maintenance;
using Cms.CmsWeb.Controls;
using Cms.CmsWeb;
using Cms.ExceptionPublisher;
using Cms.BusinessLayer.BlCommon;
using System.Data;
using System.Globalization;

namespace Cms.CmsWeb.Maintenance
{
    public partial class AddXOLInformation : Cms.CmsWeb.cmsbase
    {

        System.Resources.ResourceManager objResourceMgr;
        ClsXOLDetails objXOLDetails = new ClsXOLDetails();
        private String strRowId = String.Empty;
        

        protected void Page_Load(object sender, EventArgs e)
        {

            
            base.ScreenId = "262_10_1";
            lblMessage.Visible = false;

            //START:*********** Setting permissions and class (Read/write/execute/delete)  *************
            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            btnActivateDeactivate.CmsButtonClass = CmsButtonType.Write;
            btnActivateDeactivate.PermissionString = gstrSecurityXML;

            //END:*********** Setting permissions and class (Read/write/execute/delete)  *************
            objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddXOLInformation", System.Reflection.Assembly.GetExecutingAssembly());

         

            if (!Page.IsPostBack)
            {
                cmbLOB_ID.Enabled = false;
                txtUSED_AGGREGATE_LIMIT.ReadOnly = true;
                txtFLAT_ADJ_RATE.Attributes.Add("onblur", "javascript:this.value=formatRateBase(this.value,4);ValidatorOnChange();");
                txtREINSTATE_PREMIUM_RATE.Attributes.Add("onblur", "javascript:this.value=formatRateBase(this.value,4);ValidatorOnChange();");
                txtPREMIUM_DISCOUNT.Attributes.Add("onblur", "javascript:this.value=formatRateBase(this.value);ValidatorOnChange();");

                txtAGGREGATE_LIMIT.Attributes.Add("onblur", "javascript:this.value=formatBaseCurrencyAmount(this.value);ValidatorOnChange();");
                txtLOSS_DEDUCTION.Attributes.Add("onblur", "javascript:this.value=formatBaseCurrencyAmount(this.value);ValidatorOnChange();");
                txtMIN_DEPOSIT_PREMIUM.Attributes.Add("onblur", "javascript:this.value=formatBaseCurrencyAmount(this.value);ValidatorOnChange();");
               
               
                
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
            cmbLOB_ID.DataSource = dtLOBs;
            cmbLOB_ID.DataTextField = "LOB_DESC";
            cmbLOB_ID.DataValueField = "LOB_ID";
            cmbLOB_ID.DataBind();

            cmbRECOVERY_BASE.DataSource = ClsCommon.GetLookup("XRBS");
            cmbRECOVERY_BASE.DataTextField = "LookupDesc";
            cmbRECOVERY_BASE.DataValueField = "LookupID";
            cmbRECOVERY_BASE.DataBind();           
            
        }
        private void GetOldDataObject()
        {
            strRowId = hidXOL_ID.Value;
            if (strRowId.ToUpper().Equals("NEW"))
            {
                btnActivateDeactivate.Visible = false;
                btnSave.Visible = true;
                return;
            }

            ClsXOLInfo objXOLInfo = new ClsXOLInfo();

            objXOLInfo.XOL_ID.CurrentValue = int.Parse(strRowId);
            DataTable dt = objXOLDetails.FetchData(ref objXOLInfo);
            
            if (dt != null && dt.Rows.Count > 0)
            {
                PopulatePageFromEbixModelObject(this.Page, objXOLInfo);

                //NfiBaseCurrency.NumberDecimalDigits = 2;      
                //txtINFLATION_RATE.Text = objMonetaryInfo.INFLATION_RATE.CurrentValue.ToString("N", NfiBaseCurrency);
                //txtINTEREST_RATE.Text = objMonetaryInfo.INTEREST_RATE.CurrentValue.ToString("N", NfiBaseCurrency);

                base.SetPageModelObject(objXOLInfo);


                NfiBaseCurrency.NumberDecimalDigits = 2;
                if (dt.Rows[0]["AGGREGATE_LIMIT"].ToString() != null && dt.Rows[0]["AGGREGATE_LIMIT"].ToString() != "")
                    txtAGGREGATE_LIMIT.Text = Convert.ToDouble(dt.Rows[0]["AGGREGATE_LIMIT"].ToString()).ToString("N", NfiBaseCurrency);//layerAmount.InnerXml.Trim();              
                else
                    txtAGGREGATE_LIMIT.Text = "";

              

                if (dt.Rows[0]["LOSS_DEDUCTION"].ToString() != null && dt.Rows[0]["LOSS_DEDUCTION"].ToString() != "")
                    txtLOSS_DEDUCTION.Text = Convert.ToDouble(dt.Rows[0]["LOSS_DEDUCTION"].ToString()).ToString("N", NfiBaseCurrency);//layerAmount.InnerXml.Trim();              
                else
                    txtLOSS_DEDUCTION.Text = "";


                if (dt.Rows[0]["MIN_DEPOSIT_PREMIUM"].ToString() != null && dt.Rows[0]["MIN_DEPOSIT_PREMIUM"].ToString() != "")
                    txtMIN_DEPOSIT_PREMIUM.Text = Convert.ToDouble(dt.Rows[0]["MIN_DEPOSIT_PREMIUM"].ToString()).ToString("N", NfiBaseCurrency);//layerAmount.InnerXml.Trim();              
                else
                    txtMIN_DEPOSIT_PREMIUM.Text = "";


                if (dt.Rows[0]["PREMIUM_DISCOUNT"].ToString() != null && dt.Rows[0]["PREMIUM_DISCOUNT"].ToString() != "")
                    txtPREMIUM_DISCOUNT.Text = Convert.ToDouble(dt.Rows[0]["PREMIUM_DISCOUNT"].ToString()).ToString("N", NfiBaseCurrency);//layerAmount.InnerXml.Trim();              
                else
                    txtPREMIUM_DISCOUNT.Text = "";

               


                if (dt.Rows[0]["USED_AGGREGATE_LIMIT"].ToString() != null && dt.Rows[0]["USED_AGGREGATE_LIMIT"].ToString() != "")
                    txtUSED_AGGREGATE_LIMIT.Text = Convert.ToDouble(dt.Rows[0]["USED_AGGREGATE_LIMIT"].ToString()).ToString("N", NfiBaseCurrency);//USED_AGGREGATE_LIMIT.InnerXml.Trim();              
                else
                    txtUSED_AGGREGATE_LIMIT.Text = "";

                NfiBaseCurrency.NumberDecimalDigits = 4;
                if (dt.Rows[0]["FLAT_ADJ_RATE"].ToString() != null && dt.Rows[0]["FLAT_ADJ_RATE"].ToString() != "")
                    txtFLAT_ADJ_RATE.Text = Convert.ToDouble(dt.Rows[0]["FLAT_ADJ_RATE"].ToString()).ToString("N", NfiBaseCurrency);//layerAmount.InnerXml.Trim();              
                else
                    txtFLAT_ADJ_RATE.Text = "";

                if (dt.Rows[0]["REINSTATE_PREMIUM_RATE"].ToString() != null && dt.Rows[0]["REINSTATE_PREMIUM_RATE"].ToString() != "")
                    txtREINSTATE_PREMIUM_RATE.Text = Convert.ToDouble(dt.Rows[0]["REINSTATE_PREMIUM_RATE"].ToString()).ToString("N", NfiBaseCurrency);//layerAmount.InnerXml.Trim();              
                else
                    txtREINSTATE_PREMIUM_RATE.Text = "";


                hidIS_ACTIVE.Value = objXOLInfo.IS_ACTIVE.CurrentValue;

                if (objXOLInfo.IS_ACTIVE.CurrentValue !="Y")
                    btnSave.Visible = false;
                else
                    btnSave.Visible = true;

                btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objXOLInfo.IS_ACTIVE.CurrentValue.ToString().Trim());
                btnActivateDeactivate.Visible = true;
            }

        }


      
       

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int intRetval;


            ClsXOLInfo objXOLInfo;
            try
            {
                //For new item to add
                strRowId = hidXOL_ID.Value;
                if (strRowId.ToUpper().Equals("NEW"))
                {

                    objXOLInfo = new ClsXOLInfo();
                    this.getFormValues(objXOLInfo);

                    objXOLInfo.CONTRACT_ID.CurrentValue = int.Parse(hidCONTRACT_ID.Value);

                    objXOLInfo.CREATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());                   


                    intRetval = objXOLDetails.Add(objXOLInfo);                   


                    if (intRetval > 0)
                    {


                        hidXOL_ID.Value = objXOLInfo.XOL_ID.CurrentValue.ToString();
                        this.GetOldDataObject();
                       
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("29");
                        hidFormSaved.Value = "1";

                        
            
                    }
                    else 
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("18");
                         hidFormSaved.Value = "2";
                    }                    
                    
                    lblMessage.Visible = true;
                }
                else //For The Update cse
                {

                    objXOLInfo = (ClsXOLInfo)base.GetPageModelObject();
                    this.getFormValues(objXOLInfo);


                    objXOLInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                    objXOLInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                    objXOLInfo.LAST_UPDATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());

                    intRetval = objXOLDetails.Update(objXOLInfo);

                    if (intRetval > 0)
                    {
                        
                        this.GetOldDataObject();
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("31");

                      hidFormSaved.Value = "1";
                       

                    }
                    else 
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("18");
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

        private void getFormValues(ClsXOLInfo objXOLInfo)
        {

            try
            {

                if (cmbLOB_ID.SelectedValue != "")
                {
                    objXOLInfo.LOB_ID.CurrentValue = int.Parse(cmbLOB_ID.SelectedValue);

                    if (cmbRECOVERY_BASE.SelectedValue != "")
                        objXOLInfo.RECOVERY_BASE.CurrentValue = int.Parse(cmbRECOVERY_BASE.SelectedValue);
                    else
                        objXOLInfo.RECOVERY_BASE.CurrentValue =0;

                   
                    if (!string.IsNullOrEmpty(txtAGGREGATE_LIMIT.Text))
                        objXOLInfo.AGGREGATE_LIMIT.CurrentValue = Convert.ToDouble(txtAGGREGATE_LIMIT.Text.Trim(), NfiBaseCurrency);
                    else
                        objXOLInfo.AGGREGATE_LIMIT.CurrentValue = 0;

                    if (!string.IsNullOrEmpty(txtFLAT_ADJ_RATE.Text))
                        objXOLInfo.FLAT_ADJ_RATE.CurrentValue = Convert.ToDouble(txtFLAT_ADJ_RATE.Text.Trim(), NfiBaseCurrency);
                    else
                        objXOLInfo.FLAT_ADJ_RATE.CurrentValue = 0;

                    if (!string.IsNullOrEmpty(txtLOSS_DEDUCTION.Text))
                        objXOLInfo.LOSS_DEDUCTION.CurrentValue = Convert.ToDouble(txtLOSS_DEDUCTION.Text.Trim(), NfiBaseCurrency);
                    else
                        objXOLInfo.LOSS_DEDUCTION.CurrentValue = 0;

                    if (!string.IsNullOrEmpty(txtMIN_DEPOSIT_PREMIUM.Text))
                        objXOLInfo.MIN_DEPOSIT_PREMIUM.CurrentValue = Convert.ToDouble(txtMIN_DEPOSIT_PREMIUM.Text.Trim(), NfiBaseCurrency);
                    else
                        objXOLInfo.MIN_DEPOSIT_PREMIUM.CurrentValue = 0;

                    if (!string.IsNullOrEmpty(txtPREMIUM_DISCOUNT.Text))
                        objXOLInfo.PREMIUM_DISCOUNT.CurrentValue = Convert.ToDouble(txtPREMIUM_DISCOUNT.Text.Trim(), NfiBaseCurrency);
                    else
                        objXOLInfo.PREMIUM_DISCOUNT.CurrentValue = 0;


                    if (!string.IsNullOrEmpty(txtREINSTATE_NUMBER.Text))
                        objXOLInfo.REINSTATE_NUMBER.CurrentValue = int.Parse(txtREINSTATE_NUMBER.Text.Trim());
                    else
                        objXOLInfo.REINSTATE_NUMBER.CurrentValue = 0;

                    if (!string.IsNullOrEmpty(txtREINSTATE_PREMIUM_RATE.Text))
                        objXOLInfo.REINSTATE_PREMIUM_RATE.CurrentValue = Convert.ToDouble(txtREINSTATE_PREMIUM_RATE.Text.Trim(), NfiBaseCurrency);
                    else
                        objXOLInfo.REINSTATE_PREMIUM_RATE.CurrentValue = 0;

                    if (!string.IsNullOrEmpty(txtMIN_CLAIM_LIMIT.Text))
                        objXOLInfo.MIN_CLAIM_LIMIT.CurrentValue = int.Parse(txtMIN_CLAIM_LIMIT.Text.Trim());
                    else
                        objXOLInfo.MIN_CLAIM_LIMIT.CurrentValue = 0;

                 
                }

                


            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }

        private void GetQueryStringValues()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["XOL_ID"]) && Request.QueryString["XOL_ID"] != "NEW")
                hidXOL_ID.Value = Request.QueryString["XOL_ID"].ToString();
            else
                hidXOL_ID.Value = "NEW";

            if (Request.QueryString["LOB_ID"] != null && Request.QueryString["LOB_ID"].ToString() != "")
                hidLOB_ID.Value = Request.QueryString["LOB_ID"].ToString();
           

            if (Request.QueryString["CONTRACT_ID"] != null && Request.QueryString["CONTRACT_ID"].ToString() != "")
                hidCONTRACT_ID.Value = Request.QueryString["CONTRACT_ID"].ToString();
            else
                hidCONTRACT_ID.Value = "0"; 


        }

        private void SetCaptions()
        {
            capAGGREGATE_LIMIT.Text           = objResourceMgr.GetString("txtAGGREGATE_LIMIT");
            capFLAT_ADJ_RATE.Text             = objResourceMgr.GetString("txtFLAT_ADJ_RATE");
            capLOB_ID.Text                    = objResourceMgr.GetString("cmbLOB_ID");
            capLOSS_DEDUCTION.Text            = objResourceMgr.GetString("txtLOSS_DEDUCTION");
            capMIN_DEPOSIT_PREMIUM.Text       = objResourceMgr.GetString("txtMIN_DEPOSIT_PREMIUM");
            capPREMIUM_DISCOUNT.Text          = objResourceMgr.GetString("txtPREMIUM_DISCOUNT");
            capRECOVERY_BASE.Text             = objResourceMgr.GetString("cmbRECOVERY_BASE");
            capREINSTATE_NUMBER.Text          = objResourceMgr.GetString("txtREINSTATE_NUMBER");
            capREINSTATE_PREMIUM_RATE.Text    = objResourceMgr.GetString("txtREINSTATE_PREMIUM_RATE");
            capUSED_AGGREGATE_LIMIT.Text      = objResourceMgr.GetString("txtUSED_AGGREGATE_LIMIT");
            capMIN_CLAIM_LIMIT.Text           = objResourceMgr.GetString("txtMIN_CLAIM_LIMIT");
          
            lblRequiredFieldsInformation.Text = objResourceMgr.GetString("lblRequiredFieldsInformation");
            
        }

        private void SetErrorMessages()
        {
           // revCPF_CNPJ.ValidationExpression = aRegExpCpf_Cnpj;
           
           

            //rfvDEDUCTIBLE_1.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "1");
            //revCPF_CNPJ.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "2");
            //rfvJUDICIAL_PROCESS_NO.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "3");
            //rfvJUDICIAL_COMPLAINT_STATE.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "4");

           
            //revJUDICIAL_PROCESS_DATE.ErrorMessage = ClsMessages.FetchGeneralMessage("22");
            //revPLAINTIFF_REQUESTED_AMOUNT.ValidationExpression = aRegExpCurrencyformat;
            //revPLAINTIFF_REQUESTED_AMOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
            revAGGREGATE_LIMIT.ValidationExpression         = aRegExpBaseCurrencyformat;
            revLOSS_DEDUCTION.ValidationExpression          = aRegExpBaseCurrencyformat;
            revMIN_DEPOSIT_PREMIUM.ValidationExpression     = aRegExpBaseCurrencyformat;

            revFLAT_ADJ_RATE.ValidationExpression           = aRegExpBaseDouble;
            revPREMIUM_DISCOUNT.ValidationExpression        = aRegExpBaseDouble;
            revREINSTATE_PREMIUM_RATE.ValidationExpression  = aRegExpBaseDouble;

            revREINSTATE_NUMBER.ValidationExpression        = aRegExpInteger;
            revMIN_CLAIM_LIMIT.ValidationExpression         = aRegExpInteger;


            revAGGREGATE_LIMIT.ErrorMessage         = ClsMessages.FetchGeneralMessage("224_25_6");
            revLOSS_DEDUCTION.ErrorMessage          = ClsMessages.FetchGeneralMessage("224_25_6");
            revMIN_DEPOSIT_PREMIUM.ErrorMessage     = ClsMessages.FetchGeneralMessage("224_25_6");

            revFLAT_ADJ_RATE.ErrorMessage           = ClsMessages.GetMessage(base.ScreenId, "1"); 
            revPREMIUM_DISCOUNT.ErrorMessage        = ClsMessages.GetMessage(base.ScreenId, "1"); 
            revREINSTATE_PREMIUM_RATE.ErrorMessage  = ClsMessages.GetMessage(base.ScreenId, "1");

            revMIN_CLAIM_LIMIT.ErrorMessage         = ClsMessages.GetMessage(base.ScreenId, "2"); 
            revREINSTATE_NUMBER.ErrorMessage        = ClsMessages.GetMessage(base.ScreenId, "2"); 

            rfvLOB_ID.ErrorMessage                  = ClsMessages.GetMessage(base.ScreenId, "3");
            rfvRECOVERY_BASE.ErrorMessage           = ClsMessages.GetMessage(base.ScreenId, "4");

            csvFLAT_ADJ_RATE.ErrorMessage           = ClsMessages.GetMessage(base.ScreenId, "1");
            csvPREMIUM_DISCOUNT.ErrorMessage        = ClsMessages.GetMessage(base.ScreenId, "1"); 
            csvREINSTATE_PREMIUM_RATE.ErrorMessage  = ClsMessages.GetMessage(base.ScreenId, "1"); 

        }

        

        protected void btnDelete_Click(object sender, EventArgs e)
        {

           
        }

    
        protected void btnActivateDeactivate_Click(object sender, EventArgs e)
        {
            ClsXOLInfo objXOLInfo = (ClsXOLInfo)base.GetPageModelObject();
            lblMessage.Visible = true;
           if(hidIS_ACTIVE.Value=="Y")  // DEACTIVATE
           {
               objXOLInfo.IS_ACTIVE.CurrentValue="N"; 
              
           }
           else  // ACTIVATE
           {
               objXOLInfo.IS_ACTIVE.CurrentValue="Y";
             
           }
            

            if (objXOLDetails.ActivateDeactivate(objXOLInfo) > 0)
            {
                hidFormSaved.Value = "1";        

                if (hidIS_ACTIVE.Value == "Y") //DEACTIVATED SUCCESSFULLE
                {
                    hidIS_ACTIVE.Value = "N";
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "6");
                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objXOLInfo.IS_ACTIVE.CurrentValue.ToString().Trim());
                    btnSave.Visible = false;
                }
                else                          //ACTIVATED SUCCESSFULLE                
                {
                    hidIS_ACTIVE.Value = "Y";
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "5");
                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objXOLInfo.IS_ACTIVE.CurrentValue.ToString().Trim());
                    btnSave.Visible = true;
                }
                                 
            }
            else
            {

                lblMessage.Text = ClsMessages.FetchGeneralMessage("18");
                hidFormSaved.Value = "2";


            }
        }

       
       
    }
}
