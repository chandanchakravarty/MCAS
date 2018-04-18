/******************************************************************************************
<Author					: -		Sneha
<Start Date				: -		25-10-2011
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
using Cms.Model.Maintenance;
using Cms.CmsWeb.Controls;
using Cms.CmsWeb;
using Cms.ExceptionPublisher;
using Cms.BusinessLayer.BLClaims;
using Cms.BusinessLayer.BlCommon;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Cms.CmsWeb.Maintenance
{
    public partial class AddMasterSetup : Cms.CmsWeb.cmsbase
    {
        #region Local form variables
        //Cms.BusinessLayer.BlCommon.clsMasterdetails objMasterDet = new Cms.BusinessLayer.BlCommon.clsMasterdetails();
        Cms.BusinessLayer.BlCommon.clsMasterdetails objMasterDet = new clsMasterdetails();
        private string XmlSchemaFileName = "";
        private string XmlFullFilePath = "";
        private string TypeID = "";
        private String strRowId = String.Empty;
        protected Cms.CmsWeb.Controls.CmsButton btnReset;
        protected System.Web.UI.WebControls.DropDownList cmbCOUNTRY;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
          if (Request.QueryString["TYPE_ID"] != null && Request.QueryString["TYPE_ID"] != "")
            {
                TypeID = Request.QueryString["TYPE_ID"];
            }
            switch (TypeID)
            {
                case "1":
                    base.ScreenId = "560_0";
                    XmlSchemaFileName = "AddMasterSetup.xml";
                    break;

                case "2":
                    base.ScreenId = "561_0";
                    XmlSchemaFileName = "AddMasterSetup_2.xml";
                    break;

                case "3":
                    base.ScreenId = "562_0";
                    XmlSchemaFileName = "AddMasterSetup_3.xml";
                    break;

                case "4":
                    base.ScreenId = "563_0";
                    XmlSchemaFileName = "AddMasterSetup_4.xml";
                    break;
            }
            btnReset.CmsButtonClass = CmsButtonType.Write;  //Permission made Write instead of Execute by Sibin on 23 Oct 08
            btnReset.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;  //Permission made Write instead of Execute by Sibin on 23 Oct 08
            btnSave.PermissionString = gstrSecurityXML;

            btnActivateDeactivate.CmsButtonClass = CmsButtonType.Write;
            btnActivateDeactivate.PermissionString = gstrSecurityXML;

            btnReset.Attributes.Add("onclick", "javascript:return ResetTheForm();");

            string strSysID = GetSystemId();
            if (strSysID == "ALBAUAT")
                strSysID = "ALBA";

            XmlFullFilePath = Request.PhysicalApplicationPath + "cmsweb/support/PageXml/" + strSysID + "/" + XmlSchemaFileName;

            if (!Page.IsPostBack)
            {               
                if (ClsCommon.IsXMLResourceExists(Request.PhysicalApplicationPath + "cmsweb/support/PageXML/" + strSysID, XmlSchemaFileName))
                    setPageControls(Page, Request.PhysicalApplicationPath + "cmsweb/support/PageXml/" + strSysID + "/" + XmlSchemaFileName);

                BindNotification();    

               if (Request.QueryString["TYPE_UNIQUE_ID"] != null && Request.QueryString["TYPE_UNIQUE_ID"].ToString() != "")
                {

                    hidTYPE_UNIQUE_ID.Value = Request.QueryString["TYPE_UNIQUE_ID"].ToString();
                    this.GetOldDataObject();
                }
                else if (Request.QueryString["TYPE_UNIQUE_ID"] == null)
                {
                    hidTYPE_UNIQUE_ID.Value = "NEW";
                }
                strRowId = hidTYPE_UNIQUE_ID.Value;
            }
        }

        private void GetOldDataObject()
        {       
            clsMasterDetailInfo objMasterdetailInfo = new clsMasterDetailInfo();
            objMasterdetailInfo.TYPE_UNIQUE_ID.CurrentValue = int.Parse(hidTYPE_UNIQUE_ID.Value);
            objMasterdetailInfo = objMasterDet.FetchData(int.Parse(hidTYPE_UNIQUE_ID.Value));
            PopulatePageFromEbixModelObject(this.Page, objMasterdetailInfo);
            base.SetPageModelObject(objMasterdetailInfo);       
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int intRetval;
            clsMasterDetailInfo objMasterDetailInfo;
            try
            {
                strRowId = hidTYPE_UNIQUE_ID.Value;               
                if (strRowId.ToUpper().Equals("NEW"))
                {
                    objMasterDetailInfo = new clsMasterDetailInfo();
                    
                    this.getFormValues(objMasterDetailInfo);

                    objMasterDetailInfo.IS_ACTIVE.CurrentValue = "Y";

                    intRetval = objMasterDet.AddMasterSetupInformation(objMasterDetailInfo, XmlFullFilePath);
                    hidTYPE_UNIQUE_ID.Value = objMasterDetailInfo.TYPE_UNIQUE_ID.CurrentValue.ToString();

                    if (intRetval > 0)
                    {
                        hidTYPE_UNIQUE_ID.Value = objMasterDetailInfo.TYPE_UNIQUE_ID.CurrentValue.ToString();

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
                else
                {
                    objMasterDetailInfo = (clsMasterDetailInfo)base.GetPageModelObject();
                    this.getFormValues(objMasterDetailInfo);

                    intRetval = objMasterDet.UpdateMastersetUpInformation(objMasterDetailInfo, XmlFullFilePath);

                    if (intRetval > 0)
                    {
                        hidTYPE_UNIQUE_ID.Value = objMasterDetailInfo.TYPE_UNIQUE_ID.CurrentValue.ToString();
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
            }
        }
        protected void BindNotification()
        {
            DataTable dt = Cms.CmsWeb.ClsFetcher.Country;

            cmbCOUNTRY.DataSource = dt;
            cmbCOUNTRY.DataTextField = COUNTRY_NAME;
            cmbCOUNTRY.DataValueField = COUNTRY_ID;
            cmbCOUNTRY.DataBind();            
            hidCountry.Value = Convert.ToString(cmbCOUNTRY.SelectedValue);
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {

        }  
        private void getFormValues(clsMasterDetailInfo objMasterdetailInfo)
        {
            if (txtTYPE_CODE.Text.Trim() != "")
            {
                objMasterdetailInfo.TYPE_CODE.CurrentValue = Convert.ToString(txtTYPE_CODE.Text);
            }
            if (txtTYPE_NAME.Text.Trim() != "")
            {
                objMasterdetailInfo.TYPE_NAME.CurrentValue = Convert.ToString(txtTYPE_NAME.Text);
            }
            if (txtADDRESS.Text.Trim() != "")
            {
                objMasterdetailInfo.ADDRESS.CurrentValue = Convert.ToString(txtADDRESS.Text);
            }
            if (txtADDRESS1.Text.Trim() != "")
            {
                objMasterdetailInfo.ADDRESS1.CurrentValue = Convert.ToString(txtADDRESS1.Text);
            }
            if (txtCITY.Text.Trim() != "")
            {
                objMasterdetailInfo.CITY.CurrentValue = Convert.ToString(txtCITY.Text);
            }
            if (cmbCOUNTRY.SelectedValue != "")
            {
                objMasterdetailInfo.COUNTRY.CurrentValue = Convert.ToString(cmbCOUNTRY.SelectedValue);
            }
            if (txtTEL_NO_OFF.Text.Trim() != "")
            {
                objMasterdetailInfo.TEL_NO_OFF.CurrentValue = Convert.ToString(txtTEL_NO_OFF.Text);
            }
            if (txtMOBILE_NO.Text.Trim() != "")
            {
                objMasterdetailInfo.MOBILE_NO.CurrentValue = Convert.ToString(txtMOBILE_NO.Text);
            }
            if (txtE_MAIL.Text.Trim() != "")
            {
                objMasterdetailInfo.E_MAIL.CurrentValue = Convert.ToString(txtE_MAIL.Text);
            }
            if (txtGST.Text.Trim() != "")
            {
                objMasterdetailInfo.GST.CurrentValue = Convert.ToDouble(txtGST.Text);
            }
            if (txtCONTACT_PERSON.Text.Trim() != "")
            {
                objMasterdetailInfo.CONTACT_PERSON.CurrentValue = Convert.ToString(txtCONTACT_PERSON.Text);
            }
            if (txtPROVINCE.Text.Trim() != "")
            {
                objMasterdetailInfo.PROVINCE.CurrentValue = Convert.ToString(txtPROVINCE.Text);
            }
            if (txtPOST_CODE.Text.Trim() != "")
            {
                objMasterdetailInfo.POST_CODE.CurrentValue = Convert.ToString(txtPOST_CODE.Text);
            }
            if (txtTEL_NO_RES.Text.Trim() != "")
            {
                objMasterdetailInfo.TEL_NO_RES.CurrentValue = Convert.ToString(txtTEL_NO_RES.Text);
            }
            if (txtFAX_NO.Text.Trim() != "")
            {
                objMasterdetailInfo.FAX_NO.CurrentValue = Convert.ToString(txtFAX_NO.Text);
            }
            if (txtGST_REG_NO.Text.Trim() != "")
            {
                objMasterdetailInfo.GST_REG_NO.CurrentValue = Convert.ToString(txtGST_REG_NO.Text);
            }
            if (txtWITHHOLDING_TAX.Text.Trim() != "")
            {
                objMasterdetailInfo.WITHHOLDING_TAX.CurrentValue = Convert.ToDouble(txtWITHHOLDING_TAX.Text);
            }
            if (cmbSTATUS.SelectedItem != null)
            {
                objMasterdetailInfo.STATUS.CurrentValue = Convert.ToString(cmbSTATUS.SelectedValue);
            }
            if (cmbSOLICITOR_TYPE.SelectedItem != null)
            {
                objMasterdetailInfo.SOLICITOR_TYPE.CurrentValue = Convert.ToString(cmbSOLICITOR_TYPE.SelectedValue);
            }
            if (txtPRIVATE_E_MAIL.Text.Trim() != "")
            {
                objMasterdetailInfo.PRIVATE_E_MAIL.CurrentValue = Convert.ToString(txtPRIVATE_E_MAIL.Text);
            }
            if (cmbSURVEYOR_SOURCE.SelectedItem != null)
            {
                objMasterdetailInfo.SURVEYOR_SOURCE.CurrentValue = Convert.ToString(cmbSURVEYOR_SOURCE.SelectedValue);
            }
            if (txtCLASSIFICATION.Text.Trim() != "")
            {
                objMasterdetailInfo.CLASSIFICATION.CurrentValue = Convert.ToString(txtCLASSIFICATION.Text);
            }
            if (txtMEMO.Text.Trim() != "")
            {
                objMasterdetailInfo.MEMO.CurrentValue = Convert.ToString(txtMEMO.Text);
            }
            if (hidTYPE_ID.Value.Trim() != "")
            {
                objMasterdetailInfo.TYPE_ID.CurrentValue = Convert.ToInt32(TypeID);
            }
            //objMaster.IS_ACTIVE.CurrentValue = Convert.ToString(txtIS_ACTIVE.Text);
        }

        protected void btnActivateDeactivate_Click(object sender, EventArgs e)
        {
            clsMasterDetailInfo objMasterDetailInfo;
            try
            {
                objMasterDetailInfo = (clsMasterDetailInfo)base.GetPageModelObject();

                if (objMasterDetailInfo.IS_ACTIVE.CurrentValue == "Y")
                { objMasterDetailInfo.IS_ACTIVE.CurrentValue = "N"; }
                else
                { objMasterDetailInfo.IS_ACTIVE.CurrentValue = "Y"; }

                int intRetval = objMasterDet.ActivateDeactivateMasterDetail(objMasterDetailInfo, XmlFullFilePath);
                if (intRetval > 0)
                {
                    if (objMasterDetailInfo.IS_ACTIVE.CurrentValue == "N")
                    {
                        // btnActivateDeactivate.Visible = false;
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("9");
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("7");
                    }
                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objMasterDetailInfo.IS_ACTIVE.CurrentValue.ToString().Trim());
                    hidFormSaved.Value = "1";
                    btnActivateDeactivate.Enabled = true;

                    base.SetPageModelObject(objMasterDetailInfo);
                }
                //lblDelete.Visible = false;
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
