/******************************************************************************************
<Author				: - Pradeep Kushwaha
<Start Date			: -	16-Dec-2010
<End Date			: -	
<Purpose			: - Use for Penhor Rural product info(Risk page) page
<Description		: - 
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: - 
<Modified By		: - 

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
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb;
using Cms.ExceptionPublisher;
using Cms.CmsWeb.Controls;
using Cms.Model.Policy;

namespace Cms.Policies.Aspx.PenhorRural
{
    public partial class PenhorRuralInfo : Cms.Policies.policiesbase 
    {
        string CalledFrom;
        System.Resources.ResourceManager objResourceMgr;
        private string strRowId = "";
        ClsProducts objProducts = new ClsProducts();
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (Request.QueryString["CalledFrom"] != null && Request.QueryString["CalledFrom"].ToString() != "")
                CalledFrom = Request.QueryString["CalledFrom"].ToString();

            switch (CalledFrom)
            {
                case "RLLE": //Penhor Rural Info
                    hidCALLED_FROM.Value = CalledFrom;
                    base.ScreenId = PENHOR_RURALscreenId.INFORMATION_PAGE;
                    hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCtl_FL");
                    cvFESR_COVERAGE.Enabled = false;
                    cvFESR_COVERAGE.Visible = false;
                    spnFESR_COVERAGE.Visible = false;
                    break;
                case "RETSURTY": //Fiança Locatícia (Rental Surety) 
                    hidCALLED_FROM.Value = CalledFrom;
                    base.ScreenId = RENTAL_SURETYscreenId.INFORMATION_PAGE;
                    hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCtl_RS");
                    break;
                default:
                    base.ScreenId = "526_0";
                    break;

            }
            btnSave.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            btnDelete.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Delete;
            btnDelete.PermissionString = gstrSecurityXML;

            btnReset.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnActivateDeactivate.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnActivateDeactivate.PermissionString = gstrSecurityXML;
         
            objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.PenhorRural.PenhorRuralInfo", System.Reflection.Assembly.GetExecutingAssembly());


            btnReset.Attributes.Add("onclick", "javascript: return ResetTheForm();");
            if (!IsPostBack)
            {
                this.SetCaptions();
                this.BindDropDowns();
                this.BindState();
                this.SetErrorMessages();
                this.BindExceededPremium();

                if (Request.QueryString["PENHOR_RURAL_ID"] != null && Request.QueryString["PENHOR_RURAL_ID"].ToString() != "" && Request.QueryString["PENHOR_RURAL_ID"].ToString() != "NEW")
                {
                    hidPENHOR_RURAL_ID.Value = Request.QueryString["PENHOR_RURAL_ID"].ToString();
                    this.GetOldDataObject(Convert.ToInt32(Request.QueryString["PENHOR_RURAL_ID"].ToString()));

                }
                else 
                {
                    btnDelete.Enabled = false;
                    hidPENHOR_RURAL_ID.Value = "NEW";
                    btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1333");   
                }
                strRowId = hidPENHOR_RURAL_ID.Value;
            }

        }
        private void BindDropDowns()
        {
            cmbMODE.Items.Clear();
            cmbMODE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("PNMODE");//Penhor Rural Product Mode
            cmbMODE.DataTextField = "LookupDesc";
            cmbMODE.DataValueField = "LookupID";
            cmbMODE.DataBind();
            cmbMODE.Items.Insert(0, "");

            cmbPROPERTY.Items.Clear();
            cmbPROPERTY.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("PNPRPT");//Penhor Rural Product Property
            cmbPROPERTY.DataTextField = "LookupDesc";
            cmbPROPERTY.DataValueField = "LookupID";
            cmbPROPERTY.DataBind();
            cmbPROPERTY.Items.Insert(0, "");


            cmbCULTIVATION.Items.Clear();
            cmbCULTIVATION.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("PNCUTI");//Penhor Rural Product Cultivation
            cmbCULTIVATION.DataTextField = "LookupDesc";
            cmbCULTIVATION.DataValueField = "LookupID";
            cmbCULTIVATION.DataBind();
            cmbCULTIVATION.Items.Insert(0, "");

          
        }

        private void BindExceededPremium()
        {
            cmbExceeded_Premium.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("yesno");
            cmbExceeded_Premium.DataTextField = "LookupDesc";
            cmbExceeded_Premium.DataValueField = "LookupID";
            cmbExceeded_Premium.DataBind();
        }
        private void BindState()
        {
            ClsStates objStates = new ClsStates();
            int COUNTRY_ID = 5;
            DataTable dtStates = objStates.GetStatesForCountry(COUNTRY_ID);

            if (dtStates != null && dtStates.Rows.Count > 0)
            {
                cmbSTATE_ID.Items.Clear();
                cmbSTATE_ID.DataSource = dtStates;
                cmbSTATE_ID.DataTextField = "STATE_NAME";
                cmbSTATE_ID.DataValueField = "STATE_ID";
                cmbSTATE_ID.DataBind();
                cmbSTATE_ID.Items.Insert(0, "");

                cmbSUBSIDY_STATE.Items.Clear();
                cmbSUBSIDY_STATE.DataSource = dtStates;
                cmbSUBSIDY_STATE.DataTextField = "STATE_NAME";
                cmbSUBSIDY_STATE.DataValueField = "STATE_ID";
                cmbSUBSIDY_STATE.DataBind();
                cmbSUBSIDY_STATE.Items.Insert(0, "");

            }

        }
        /// <summary>
        /// Use to set the error messages on the controls for validation  
        /// </summary>
        private void SetErrorMessages()
        {
            revITEM_NUMBER.ValidationExpression = aRegExpInteger;
            revITEM_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "13");
            rfvITEM_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "4");
            
            cvFESR_COVERAGE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");

            rfvMODE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "6");

            rfvPROPERTY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "6");
            rfvCULTIVATION.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "8");
            rfvSTATE_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "10"); 
            
            revCITY.ValidationExpression=aRegExpTextArea255;
            revCITY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "14");
            rfvCITY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "9");

            revINSURED_AREA.ValidationExpression = aRegExpInteger;
            revINSURED_AREA.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "15");
            rfvINSURED_AREA.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "11");

            revSUBSIDY_PREMIUM.ValidationExpression = aRegExpCurrencyformat;
            revSUBSIDY_PREMIUM.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "12");
            //itrack 829
            rfvSUBSIDY_PREMIUM.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "16");
            rfvSUBSIDY_STATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "17");
            rfvREMARKS.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "18");
            //till here 
        }
        private void SetCaptions()
        {
            capMandatoryNotes.Text = ClsMessages.FetchGeneralMessage("1168");
            capITEM_NUMBER.Text = objResourceMgr.GetString("txtITEM_NUMBER");
            capFESR_COVERAGE.Text = objResourceMgr.GetString("chkFESR_COVERAGE");
            capMODE.Text = objResourceMgr.GetString("cmbMODE");
            capPROPERTY.Text = objResourceMgr.GetString("cmbPROPERTY");
            capCULTIVATION.Text = objResourceMgr.GetString("cmbCULTIVATION");
            capCITY.Text = objResourceMgr.GetString("txtCITY");
            capSTATE_ID.Text = objResourceMgr.GetString("cmbSTATE_ID");
            capINSURED_AREA.Text = objResourceMgr.GetString("txtINSURED_AREA");
            capSUBSIDY_PREMIUM.Text = objResourceMgr.GetString("txtSUBSIDY_PREMIUM");
            capSUBSIDY_STATE.Text = objResourceMgr.GetString("cmbSUBSIDY_STATE");
            capREMARKS.Text = objResourceMgr.GetString("txtREMARKS");
            capExceededPremium.Text = objResourceMgr.GetString("cmbExceeded_Premium");
        }
        /// <summary>
        /// Use to Get the Old Data Based on the ID
        /// </summary>
        /// <param name="Vehicle_ID"></param>
        private void GetOldDataObject(Int32 intPENHOR_RURAL_ID)
        {
            ClsPenhorRuralInfo objPenhorRuralInfo = new ClsPenhorRuralInfo();

            objPenhorRuralInfo.PENHOR_RURAL_ID.CurrentValue = intPENHOR_RURAL_ID;
            objPenhorRuralInfo.CUSTOMER_ID.CurrentValue = int.Parse(GetCustomerID());
            objPenhorRuralInfo.POLICY_ID.CurrentValue = int.Parse(GetPolicyID());
            objPenhorRuralInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(GetPolicyVersionID());
            string policystatus = GetPolicyStatus();
            if (objProducts.FetchPenhorRuralInfoData(ref objPenhorRuralInfo))
            {
                PopulatePageFromEbixModelObject(this.Page, objPenhorRuralInfo);

                if (objPenhorRuralInfo.SUBSIDY_PREMIUM.CurrentValue.ToString() != "" && objPenhorRuralInfo.SUBSIDY_PREMIUM.CurrentValue!=-1)
                {   
                    numberFormatInfo.NumberDecimalDigits = 2;
                    txtSUBSIDY_PREMIUM.Text = objPenhorRuralInfo.SUBSIDY_PREMIUM.CurrentValue.ToString("N", numberFormatInfo);
                }
                cmbSUBSIDY_STATE.SelectedIndex = cmbSUBSIDY_STATE.Items.IndexOf(cmbSUBSIDY_STATE.Items.FindByValue(objPenhorRuralInfo.SUBSIDY_STATE.CurrentValue.ToString()));

                btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objPenhorRuralInfo.IS_ACTIVE.CurrentValue.ToString().Trim());
                // itrack no 867
                string originalversion = objPenhorRuralInfo.ORIGINAL_VERSION_ID.CurrentValue.ToString();
                if (originalversion != GetPolicyVersionID() && policystatus == "UENDRS" && originalversion != "0")
                {
                    btnActivateDeactivate.Visible = false;
                }

                base.SetPageModelObject(objPenhorRuralInfo);
            }// if (objProducts.FetchPenhorRuralInfoData(ref objPenhorRuralInfo))

        }
        /// <summary>
        /// Use to set the form controls values in page model
        /// </summary>
        /// <param name="ObjCivilTransportVehicleInfo"></param>
        private void GetFormValue(ClsPenhorRuralInfo objPenhorRuralInfo)
        {
            objPenhorRuralInfo.ORIGINAL_VERSION_ID.CurrentValue = 0;// int.Parse(GetPolicyVersionID());
            if (txtITEM_NUMBER.Text.ToString().Trim() != "")
                objPenhorRuralInfo.ITEM_NUMBER.CurrentValue = Convert.ToInt32(txtITEM_NUMBER.Text);
            else
                objPenhorRuralInfo.ITEM_NUMBER.CurrentValue = base.GetEbixIntDefaultValue();

            if (chkFESR_COVERAGE.Checked)
                objPenhorRuralInfo.FESR_COVERAGE.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.YES);// 10963;
            else
                objPenhorRuralInfo.FESR_COVERAGE.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.NO);// 10964;
          
            if (cmbMODE.SelectedValue != "")
                objPenhorRuralInfo.MODE.CurrentValue = int.Parse(cmbMODE.SelectedValue);
            else
                objPenhorRuralInfo.MODE.CurrentValue = GetEbixIntDefaultValue();
            
            if (cmbPROPERTY.SelectedValue != "")
                objPenhorRuralInfo.PROPERTY.CurrentValue = int.Parse(cmbPROPERTY.SelectedValue);
            else
                objPenhorRuralInfo.PROPERTY.CurrentValue = GetEbixIntDefaultValue();

            if (cmbCULTIVATION.SelectedValue != "")
                objPenhorRuralInfo.CULTIVATION.CurrentValue = int.Parse(cmbCULTIVATION.SelectedValue);
            else
                objPenhorRuralInfo.CULTIVATION.CurrentValue = GetEbixIntDefaultValue();

            if (cmbSTATE_ID.SelectedValue != "")
                objPenhorRuralInfo.STATE_ID.CurrentValue = int.Parse(cmbSTATE_ID.SelectedValue);
            else
                objPenhorRuralInfo.STATE_ID.CurrentValue = GetEbixIntDefaultValue();
           
            if (txtCITY.Text.ToString().Trim()!="")
                objPenhorRuralInfo.CITY.CurrentValue = txtCITY.Text.ToString();
            else
                objPenhorRuralInfo.CITY.CurrentValue =String.Empty;

            if (txtINSURED_AREA.Text.ToString().Trim() != "")
                objPenhorRuralInfo.INSURED_AREA.CurrentValue = Convert.ToInt32(txtINSURED_AREA.Text.ToString());
            else
                objPenhorRuralInfo.INSURED_AREA.CurrentValue = GetEbixIntDefaultValue();

            if (txtSUBSIDY_PREMIUM.Text.ToString().Trim() != "")
                objPenhorRuralInfo.SUBSIDY_PREMIUM.CurrentValue = ConvertEbixDoubleValue(txtSUBSIDY_PREMIUM.Text.ToString());
            else
                objPenhorRuralInfo.SUBSIDY_PREMIUM.CurrentValue = GetEbixDoubleDefaultValue();


            if (cmbSUBSIDY_STATE.SelectedValue != "")
                objPenhorRuralInfo.SUBSIDY_STATE.CurrentValue = int.Parse(cmbSUBSIDY_STATE.SelectedValue);
            else
                objPenhorRuralInfo.SUBSIDY_STATE.CurrentValue = GetEbixIntDefaultValue();
            
            if (txtREMARKS.Text != String.Empty)
                objPenhorRuralInfo.REMARKS.CurrentValue = txtREMARKS.Text;
            else
                objPenhorRuralInfo.REMARKS.CurrentValue = String.Empty;

            if (cmbExceeded_Premium.SelectedValue != "")
                objPenhorRuralInfo.EXCEEDED_PREMIUM.CurrentValue = int.Parse(cmbExceeded_Premium.SelectedValue);
            else
                objPenhorRuralInfo.EXCEEDED_PREMIUM.CurrentValue = GetEbixIntDefaultValue();

        }//private void GetFormValue(ClsCivilTransportVehicleInfo ObjCivilTransportVehicleInfo)
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int intRetval;
            strRowId = hidPENHOR_RURAL_ID.Value;
            ClsPenhorRuralInfo objPenhorRuralInfo;
            try
            {
                //For The Save Case                
                if (strRowId.ToUpper().Equals("NEW"))
                {

                    objPenhorRuralInfo = new ClsPenhorRuralInfo();
                    this.GetFormValue(objPenhorRuralInfo);

                    objPenhorRuralInfo.CUSTOMER_ID.CurrentValue = int.Parse(GetCustomerID());
                    objPenhorRuralInfo.POLICY_ID.CurrentValue = int.Parse(GetPolicyID());
                    objPenhorRuralInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(GetPolicyVersionID());

                    objPenhorRuralInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                    objPenhorRuralInfo.CREATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    objPenhorRuralInfo.IS_ACTIVE.CurrentValue = "Y";

                    intRetval = objProducts.AddPenhorRuralInfo(objPenhorRuralInfo);

                    if (intRetval > 0)
                    {
                        this.GetOldDataObject(objPenhorRuralInfo.PENHOR_RURAL_ID.CurrentValue);

                        hidPENHOR_RURAL_ID.Value = objPenhorRuralInfo.PENHOR_RURAL_ID.CurrentValue.ToString();
                        
                        btnDelete.Enabled = true;
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("29");
                        hidFormSaved.Value = "1";
                        base.OpenEndorsementDetails();


                    }
                    else if (intRetval == -10)
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("1384");
                        hidFormSaved.Value = "2";
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                        hidFormSaved.Value = "2";
                    }
                    lblDelete.Visible = false;
                    lblMessage.Visible = true;
                } // if (strRowId.ToUpper().Equals("NEW"))
                else //For The Update cse
                {


                    objPenhorRuralInfo = (ClsPenhorRuralInfo)base.GetPageModelObject();
                    this.GetFormValue(objPenhorRuralInfo);
                    objPenhorRuralInfo.IS_ACTIVE.CurrentValue = "Y";
                    objPenhorRuralInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                    objPenhorRuralInfo.LAST_UPDATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                    intRetval = objProducts.UpdatePenhorRuralInfo(objPenhorRuralInfo);

                    if (intRetval == 1)
                    {
                        this.GetOldDataObject(objPenhorRuralInfo.PENHOR_RURAL_ID.CurrentValue);
                    
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("31");
                        hidFormSaved.Value = "1";
                        base.OpenEndorsementDetails();

                    }
                    else if (intRetval == -10)
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("1384");
                        hidFormSaved.Value = "2";
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                        hidFormSaved.Value = "2";
                    }
                    lblDelete.Visible = false;
                    lblMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "3") + " - " + ex.Message;
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";

            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ClsPenhorRuralInfo objPenhorRuralInfo;
            try
            {

                objPenhorRuralInfo = (ClsPenhorRuralInfo)base.GetPageModelObject();

                objPenhorRuralInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());

                int intRetval = objProducts.DeletePenhorRuralInfo(objPenhorRuralInfo);
                if (intRetval > 0)
                {
                    lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "127");
                    hidPENHOR_RURAL_ID.Value = "";
                    hidFormSaved.Value = "1";
                    trBody.Attributes.Add("style", "display:none");
                }
                else if (intRetval == -1)
                {
                    lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "128");
                    hidFormSaved.Value = "2";
                }
                lblDelete.Visible = true;
                lblMessage.Visible = false;


            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "2") + " - " + ex.Message;
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";

            }
        }

        protected void btnActivateDeactivate_Click(object sender, EventArgs e)
        {
            ClsPenhorRuralInfo objPenhorRuralInfo;

            try
            {
                objPenhorRuralInfo = (ClsPenhorRuralInfo)base.GetPageModelObject();

                if (objPenhorRuralInfo.IS_ACTIVE.CurrentValue == "Y")
                { objPenhorRuralInfo.IS_ACTIVE.CurrentValue = "N"; }
                else
                { objPenhorRuralInfo.IS_ACTIVE.CurrentValue = "Y"; }


                objPenhorRuralInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                objPenhorRuralInfo.LAST_UPDATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now.ToShortDateString());


                int intRetval = objProducts.ActivateDeactivatePenhorRuralInfo(objPenhorRuralInfo);
                if (intRetval > 0)
                {
                    if (objPenhorRuralInfo.IS_ACTIVE.CurrentValue == "N")
                    {
                       // btnActivateDeactivate.Visible = false;
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("9");
                   
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("7");
                    }
                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objPenhorRuralInfo.IS_ACTIVE.CurrentValue.ToString().Trim());
                    hidFormSaved.Value = "1";

                    base.SetPageModelObject(objPenhorRuralInfo);
                }
                lblDelete.Visible = false;
                lblMessage.Visible = true;


            }
            catch (Exception ex)
            {

                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "1") + " - " + ex.Message;
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";

            }
        }

      
    }
}
