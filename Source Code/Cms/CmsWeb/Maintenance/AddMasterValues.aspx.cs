/******************************************************************************************
<Author					: -		Sneha
<Start Date				: -		31-10-2011
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
    public partial class AddMasterValues : Cms.CmsWeb.cmsbase
    {
        #region Local form variables
        ClsMasterdetailsvalue objMasterDet = new ClsMasterdetailsvalue();
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

                case "2":
                    base.ScreenId = "565_0";
                    XmlSchemaFileName = "AddMasterValues_2.xml";
                    break;


                case "3":
                    base.ScreenId = "566_0";
                    XmlSchemaFileName = "AddMasterValues_3.xml";
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

            ClsMasterDetailValueInfo objMasterdetailInfo = new ClsMasterDetailValueInfo();

            objMasterdetailInfo.TYPE_UNIQUE_ID.CurrentValue = int.Parse(hidTYPE_UNIQUE_ID.Value);
            objMasterdetailInfo = objMasterDet.FetchDataValue(int.Parse(hidTYPE_UNIQUE_ID.Value));
            PopulatePageFromEbixModelObject(this.Page, objMasterdetailInfo);
            base.SetPageModelObject(objMasterdetailInfo);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int intRetval;
            ClsMasterDetailValueInfo objMasterDetailInfo;

            try
            {
                strRowId = hidTYPE_UNIQUE_ID.Value;

                if (strRowId.ToUpper().Equals("NEW"))
                {
                    objMasterDetailInfo = new ClsMasterDetailValueInfo();

                    this.getFormValues(objMasterDetailInfo);

                    objMasterDetailInfo.IS_ACTIVE.CurrentValue = "Y";

                    intRetval = objMasterDet.AddMasterValueInformation(objMasterDetailInfo, XmlFullFilePath);
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

                    objMasterDetailInfo = (ClsMasterDetailValueInfo)base.GetPageModelObject();
                    this.getFormValues(objMasterDetailInfo);


                    intRetval = objMasterDet.UpdateMasterValueInformation(objMasterDetailInfo, XmlFullFilePath);



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

        private void getFormValues(ClsMasterDetailValueInfo objMasterdetailInfo)
        {
            
            if (txtCODE.Text.Trim() != "")
            {
                objMasterdetailInfo.CODE.CurrentValue = Convert.ToString(txtCODE.Text);
            }
            if (txtDESCRIPTION.Text.Trim() != "")
            {
                objMasterdetailInfo.DESCRIPTION.CurrentValue = Convert.ToString(txtDESCRIPTION.Text);
            }
            if (cmbRECOVERY_TYPE.SelectedValue!= "")
            {
                objMasterdetailInfo.RECOVERY_TYPE.CurrentValue = Convert.ToString(cmbRECOVERY_TYPE.SelectedValue);
            }
            if (hidTYPE_ID.Value.Trim() != "")
            {
                objMasterdetailInfo.TYPE_ID.CurrentValue = Convert.ToInt32(TypeID);
            }

        }

        protected void btnActivateDeactivate_Click(object sender, EventArgs e)
        {
            ClsMasterDetailValueInfo objMasterDetailInfo;

            try
            {
                objMasterDetailInfo = (ClsMasterDetailValueInfo)base.GetPageModelObject();

                if (objMasterDetailInfo.IS_ACTIVE.CurrentValue == "Y")
                { objMasterDetailInfo.IS_ACTIVE.CurrentValue = "N"; }
                else
                { objMasterDetailInfo.IS_ACTIVE.CurrentValue = "Y"; }



                int intRetval = objMasterDet.ActivateDeactivateMasterValue(objMasterDetailInfo, XmlFullFilePath);
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
