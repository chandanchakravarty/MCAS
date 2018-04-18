/*------------------------------------------------------------------------------
-- =============================================
-- Author:		<Pradeep Kushwaha>
-- Create date: <14-Dec-2010>
-- Description:	<DPVAT (Cat. 3 e 4) product>
------------------------------------------------------------------------------*/
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
using Cms.Model.Policy.Transportation; 

namespace Cms.Policies.Aspx
{
    public partial class AddDpvatInfo : Cms.Policies.policiesbase
    {
        string CalledFrom;
        System.Resources.ResourceManager objResourceMgr;
        private string strRowId = "";
        ClsProducts objProducts = new ClsProducts();
        protected void Page_Load(object sender, EventArgs e)
        {
            btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1333");            
            if (Request.QueryString["CalledFrom"] != null && Request.QueryString["CalledFrom"].ToString() != "")
                CalledFrom = Request.QueryString["CalledFrom"].ToString();
       
            switch (CalledFrom)
            {
                case "DPVA":////Dpvat(Cat. 3 e 4)
                    hidCALLED_FROM.Value = CalledFrom;
                    base.ScreenId = DPVATscreenId.INDIVIDUAL_INFORMATION;
                    hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCtl_CL");
                    break;
                case "DPVAT2"://Dpvat(Cat. 1,2,9 e 10)
                    hidCALLED_FROM.Value = CalledFrom;
                    base.ScreenId = DPVAT2screenId.INDIVIDUAL_INFORMATION;
                    hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCtl_CL");
                    break;
                default:
                    base.ScreenId = "516_2";
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
            objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.AddDpvatInfo", System.Reflection.Assembly.GetExecutingAssembly());


            btnReset.Attributes.Add("onclick", "javascript: return ResetTheForm();");
            if (!IsPostBack) 
            {
                 this.SetCaptions();
                 this.BindState();
                 this.BindCategory();
                 this.SetErrorMessages();
                 this.BindExceededPremium();
                 if (Request.QueryString["VEHICLE_ID"] != null && Request.QueryString["VEHICLE_ID"].ToString() != "" && Request.QueryString["VEHICLE_ID"].ToString() != "NEW")
                 {

                     hidVEHICLE_ID.Value = Request.QueryString["VEHICLE_ID"].ToString();

                     this.GetOldDataObject(Convert.ToInt32(Request.QueryString["VEHICLE_ID"].ToString()));

                 }
                 else //if (Request.QueryString["VEHICLE_ID"] == null)
                 {
                     btnActivateDeactivate.Enabled = false;
                     btnDelete.Enabled = false;
                     hidVEHICLE_ID.Value = "NEW";
                     
                 }
                 strRowId = hidVEHICLE_ID.Value;
            }
        }
       /// <summary>
        /// Use to Get the Old Data Based on the ID
        /// </summary>
        /// <param name="Vehicle_ID"></param>
        private void GetOldDataObject(Int32 Vehicle_ID)
        {
            ClsCivilTransportVehicleInfo ObjCivilTransportVehicleInfo=new ClsCivilTransportVehicleInfo();

            ObjCivilTransportVehicleInfo.VEHICLE_ID.CurrentValue = Vehicle_ID;
            ObjCivilTransportVehicleInfo.CUSTOMER_ID.CurrentValue = int.Parse(GetCustomerID());
            ObjCivilTransportVehicleInfo.POLICY_ID.CurrentValue = int.Parse(GetPolicyID());
            ObjCivilTransportVehicleInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(GetPolicyVersionID());
            string policystatus = GetPolicyStatus();
            if (objProducts.FetchCivilTransportVehicleInfoData(ref ObjCivilTransportVehicleInfo))
            {
                PopulatePageFromEbixModelObject(this.Page, ObjCivilTransportVehicleInfo);
              
                btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(ObjCivilTransportVehicleInfo.IS_ACTIVE.CurrentValue.ToString().Trim());
                // itrack 867
                string originalversion = ObjCivilTransportVehicleInfo.ORIGINAL_VERSION_ID.CurrentValue.ToString();
                if (originalversion != GetPolicyVersionID() && policystatus == "UENDRS" && originalversion != "0")
                {
                    btnActivateDeactivate.Visible = false;
                }
                base.SetPageModelObject(ObjCivilTransportVehicleInfo);
            }//if (objProducts.FetchCivilTransportVehicleInfoData(ref ObjCivilTransportVehicleInfo))
       
        }//private void GetOldDataObject(Int32 Vehicle_ID)
       
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
                
            }
 
        }
        private void BindCategory()
        {
            cmbCATEGORY.Items.Clear();
            cmbCATEGORY.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("DPVCTE");//DPVAT (Cat. 3 e 4) Category
            cmbCATEGORY.DataTextField = "LookupDesc";
            cmbCATEGORY.DataValueField = "LookupID";
            cmbCATEGORY.DataBind();
            cmbCATEGORY.Items.Insert(0, "");
        }
        private void BindExceededPremium()
        {
            cmbExceeded_Premium.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("yesno");
            cmbExceeded_Premium.DataTextField = "LookupDesc";
            cmbExceeded_Premium.DataValueField = "LookupID";
            cmbExceeded_Premium.DataBind();
        }

        private void SetCaptions()
        {
            lblManHeader.Text = ClsMessages.FetchGeneralMessage("1168");
            capTICKET_NUMBER.Text = objResourceMgr.GetString("txtTICKET_NUMBER");
            capCATEGORY.Text = objResourceMgr.GetString("cmbCATEGORY");
            capSTATE_ID.Text = objResourceMgr.GetString("capSTATE_ID");
            capExceededPremium.Text = objResourceMgr.GetString("cmbExceeded_Premium");
        }
        /// <summary>
        /// Use to set the error messages on the controls for validation  
        /// </summary>
        private void SetErrorMessages()
        {
            revTICKET_NUMBER.ValidationExpression = aRegExpInteger;
            revTICKET_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
            rfvTICKET_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "4");
            rfvCATEGORY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "6");
            rfvSTATE_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "7");
           
        }
        /// <summary>
        /// Use to set the form controls values in page model
        /// </summary>
        /// <param name="ObjCivilTransportVehicleInfo"></param>
        private void GetFormValue(ClsCivilTransportVehicleInfo ObjCivilTransportVehicleInfo)
        {
            ObjCivilTransportVehicleInfo.ORIGINAL_VERSION_ID.CurrentValue = 0;// int.Parse(GetPolicyVersionID());
            if (txtTICKET_NUMBER.Text.ToString().Trim() != "")
                ObjCivilTransportVehicleInfo.TICKET_NUMBER.CurrentValue = Convert.ToInt32(txtTICKET_NUMBER.Text);
            else
                ObjCivilTransportVehicleInfo.TICKET_NUMBER.CurrentValue = base.GetEbixIntDefaultValue();

            if ((cmbCATEGORY.SelectedItem != null) && (cmbCATEGORY.SelectedItem.Text.ToString().Trim() != ""))
                ObjCivilTransportVehicleInfo.CATEGORY.CurrentValue = Convert.ToInt32(cmbCATEGORY.SelectedItem.Value);
            else
                ObjCivilTransportVehicleInfo.CATEGORY.CurrentValue = base.GetEbixIntDefaultValue();

            if ((cmbSTATE_ID.SelectedItem != null) && (cmbCATEGORY.SelectedItem.Text.ToString().Trim() != ""))
                ObjCivilTransportVehicleInfo.STATE_ID.CurrentValue = Convert.ToInt32(cmbSTATE_ID.SelectedItem.Value);
            else
                ObjCivilTransportVehicleInfo.STATE_ID.CurrentValue = base.GetEbixIntDefaultValue();

            if (cmbExceeded_Premium.SelectedValue != "")
                ObjCivilTransportVehicleInfo.EXCEEDED_PREMIUM.CurrentValue = int.Parse(cmbExceeded_Premium.SelectedValue);
            else
                ObjCivilTransportVehicleInfo.EXCEEDED_PREMIUM.CurrentValue = GetEbixIntDefaultValue();

        }//private void GetFormValue(ClsCivilTransportVehicleInfo ObjCivilTransportVehicleInfo)

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int intRetval;
            strRowId = hidVEHICLE_ID.Value;
            ClsCivilTransportVehicleInfo ObjCivilTransportVehicleInfo;
            try
            {
                //For The Save Case                
                if (strRowId.ToUpper().Equals("NEW"))
                {

                    ObjCivilTransportVehicleInfo = new ClsCivilTransportVehicleInfo();
                    this.GetFormValue(ObjCivilTransportVehicleInfo);

                    ObjCivilTransportVehicleInfo.CUSTOMER_ID.CurrentValue = int.Parse(GetCustomerID());
                    ObjCivilTransportVehicleInfo.POLICY_ID.CurrentValue = int.Parse(GetPolicyID());
                    ObjCivilTransportVehicleInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(GetPolicyVersionID());

                    ObjCivilTransportVehicleInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                    ObjCivilTransportVehicleInfo.CREATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    ObjCivilTransportVehicleInfo.IS_ACTIVE.CurrentValue = "Y";

                    intRetval = objProducts.AddDPVATCivilTransportVehicleInfo(ObjCivilTransportVehicleInfo, Convert.ToString(hidCALLED_FROM.Value));

                    if (intRetval == 1)
                    {
                        this.GetOldDataObject(ObjCivilTransportVehicleInfo.VEHICLE_ID.CurrentValue);

                        hidVEHICLE_ID.Value = ObjCivilTransportVehicleInfo.VEHICLE_ID.CurrentValue.ToString();
                        btnActivateDeactivate.Enabled = true;
                        btnDelete.Enabled = true;
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("29");
                        hidFormSaved.Value = "1";
                        base.OpenEndorsementDetails();


                    }
                    else if (intRetval == -2)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "25");
                        hidFormSaved.Value = "2";
                    }
                    else if (intRetval == -3)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "28");
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


                    ObjCivilTransportVehicleInfo = (ClsCivilTransportVehicleInfo)base.GetPageModelObject();
                    this.GetFormValue(ObjCivilTransportVehicleInfo);
                    ObjCivilTransportVehicleInfo.IS_ACTIVE.CurrentValue = "Y";
                    ObjCivilTransportVehicleInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                    ObjCivilTransportVehicleInfo.LAST_UPDATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                    intRetval = objProducts.UpdateDPVATCivilTransportVehicleInfo(ObjCivilTransportVehicleInfo, Convert.ToString(hidCALLED_FROM.Value));

                    if (intRetval == 1)
                    {
                        this.GetOldDataObject(ObjCivilTransportVehicleInfo.VEHICLE_ID.CurrentValue);
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("31");
                        hidFormSaved.Value = "1";
                        base.OpenEndorsementDetails();

                    }
                    else if (intRetval == -2)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "25");
                        hidFormSaved.Value = "2";
                    }
                    else if (intRetval == -3)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "28");
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
            ClsCivilTransportVehicleInfo ObjCivilTransportVehicleInfo;
            try
            {

                ObjCivilTransportVehicleInfo = (ClsCivilTransportVehicleInfo)base.GetPageModelObject();

                ObjCivilTransportVehicleInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());

                int intRetval = objProducts.DeleteCivilTransportVehicleInfo(ObjCivilTransportVehicleInfo, Convert.ToString(hidCALLED_FROM.Value));
                if (intRetval > 0)
                {
                    lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "127");
                    hidVEHICLE_ID.Value = "";
                    hidFormSaved.Value = "1";
                    tbody.Attributes.Add("style", "display:none");
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
            ClsCivilTransportVehicleInfo ObjCivilTransportVehicleInfo;

            try
            {
                ObjCivilTransportVehicleInfo = (ClsCivilTransportVehicleInfo)base.GetPageModelObject();

                if (ObjCivilTransportVehicleInfo.IS_ACTIVE.CurrentValue == "Y")
                { ObjCivilTransportVehicleInfo.IS_ACTIVE.CurrentValue = "N"; }
                else
                { ObjCivilTransportVehicleInfo.IS_ACTIVE.CurrentValue = "Y"; }


                ObjCivilTransportVehicleInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                ObjCivilTransportVehicleInfo.LAST_UPDATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now.ToShortDateString());


                int intRetval = objProducts.ActivateDeactivateCivilTransportVehicleInfo(ObjCivilTransportVehicleInfo, Convert.ToString(hidCALLED_FROM.Value));
                if (intRetval > 0)
                {
                    if (ObjCivilTransportVehicleInfo.IS_ACTIVE.CurrentValue == "N")
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("9");
                        //itrack no 867
                       // btnActivateDeactivate.Visible = false;
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("7");
                    }
                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(ObjCivilTransportVehicleInfo.IS_ACTIVE.CurrentValue.ToString().Trim());
                    hidFormSaved.Value = "1";

                    base.SetPageModelObject(ObjCivilTransportVehicleInfo);
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
