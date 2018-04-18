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
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb.WebControls;
using Cms.Model.Maintenance;
using Cms.CmsWeb.Controls;
using System.Resources;
using Cms.Blcommon;

namespace Cms.CmsWeb.Maintenance
{
    public partial class AddDiscountSurcharge : Cms.CmsWeb.cmsbase
    {
        #region Adding Web Controls
        protected Label lblDelete;
        protected Label lblMessage;
        protected Label capTYPE;
        protected Label capLOB_ID;
        protected Label capSUBLOB_ID;
        //protected Label capDISCOUNT_DESCRIPTION;
        protected Label capPERCENTAGE;
        protected Label capEFFECTIVE_DATE;
        protected Label capFINAL_DATE;
        protected Label capMAN_MSG;
        //protected DropDownList cmbTYPE_ID;
        protected DropDownList cmbLOB_ID;
        protected DropDownList cmbSUBLOB_ID;
        //protected TextBox txtDISCOUNT_DESCRIPTION;
        protected TextBox txtPERCENTAGE;
        protected TextBox txtEFFECTIVE_DATE;
        protected TextBox txtFINAL_DATE;
        protected CmsButton btnReset;
        protected CmsButton btnActivateDeactivate;
        protected CmsButton btnDelete;
        protected CmsButton btnSave;
        protected HtmlInputHidden hidLOBXML;
        //protected HtmlInputHidden hidSave;
        protected HyperLink hlkEFFECTIVE_FROM_DATE;
        protected HyperLink hlkEFFECTIVE_TO_DATE;
        protected RequiredFieldValidator rfvEFFECTIVE_DATE;
        protected RegularExpressionValidator revEFFECTIVE_DATE;
        protected RequiredFieldValidator rfvFINAL_DATE;
        protected RegularExpressionValidator revFINAL_DATE;
        ResourceManager objResourceMgr;
        #endregion 

        #region Variables
        //int intRetVal = 0;
        ClsDiscountSurcharge objDiscountSurcharge = new ClsDiscountSurcharge();
        private string strRowId = "";
        #endregion

        //private string strRowId, strFormSaved;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            #region setting screen id
            base.ScreenId = "456_0";
            #endregion

            #region setting security Xml
            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnActivateDeactivate.CmsButtonClass = CmsButtonType.Write;
            btnActivateDeactivate.PermissionString = gstrSecurityXML;

            btnDelete.CmsButtonClass = CmsButtonType.Delete;
            btnDelete.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;
            hidSave.Value = "";
            #endregion

            objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddDiscountSurcharge", System.Reflection.Assembly.GetExecutingAssembly());
            hidSUB_GEN.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1190");
            hlkEFFECTIVE_FROM_DATE.Attributes.Add("OnClick", "fPopCalendar(document.getElementById('txtEFFECTIVE_DATE'), document.getElementById('txtEFFECTIVE_DATE'))");
            hlkEFFECTIVE_TO_DATE.Attributes.Add("OnClick", "fPopCalendar(document.getElementById('txtFINAL_DATE'), document.getElementById('txtFINAL_DATE'))");
            //if (txtPERCENTAGE != null)
            //    txtPERCENTAGE.Attributes.Add("onblur", "javascript:this.value=formatRateBase(this.value)");
            if (!Page.IsPostBack)
            {
                    SetErrorMessage();                     
                    SetCaptions();
                    PopulateProduct();
                    
                    hidLOBXML.Value = ClsCommon.GetXmlForLobWithoutState();  
                    btnReset.Attributes.Add("onclick", "javascript: return ResetTheForm();");
                    
                    if (ClsCommon.IsXMLResourceExists(@Request.PhysicalApplicationPath + "CmsWeb/Support/PageXml/" + GetSystemId(), "AddDiscountSurcharge.xml"))
                    {
                        setPageControls(Page, @Request.PhysicalApplicationPath + "/CmsWeb/support/PageXML/" + GetSystemId() + "/AddDiscountSurcharge.xml");
                    }

                    if (Request.QueryString["DISCOUNT_ID"] != null && Request.QueryString["DISCOUNT_ID"].ToString() != "")
                     {

                         hidDiscountID.Value = Request.QueryString["DISCOUNT_ID"].ToString();

                         this.GetOldDataObject(Convert.ToInt32(Request.QueryString["DISCOUNT_ID"].ToString()));
                         

                     }
                     else if (Request.QueryString["DISCOUNT_ID"] == null)
                     {
                         btnActivateDeactivate.Visible = false;
                         btnDelete.Visible = false;
                         hidDiscountID.Value = "NEW";

                     }
                     strRowId = hidDiscountID.Value;
                     
			}
            
        }

        # region Methods
        private void SetCaptions()
        {
            #region setcaption in resource file
            capTYPE.Text = objResourceMgr.GetString("cmbTYPE_ID");
            capSUBLOB_ID.Text = objResourceMgr.GetString("cmbSUBLOB_ID");
            capLOB_ID.Text = objResourceMgr.GetString("cmbLOB_ID");
            capDISCOUNT_DESCRIPTION.Text = objResourceMgr.GetString("txtDISCOUNT_DESCRIPTION");
            capPERCENTAGE.Text = objResourceMgr.GetString("txtPERCENTAGE");
            capEFFECTIVE_DATE.Text = objResourceMgr.GetString("txtEFFECTIVE_DATE");
            capFINAL_DATE.Text = objResourceMgr.GetString("txtFINAL_DATE");
            capMAN_MSG.Text = objResourceMgr.GetString("capMAN_MSG");
            capLEVEL.Text = objResourceMgr.GetString("cmbLEVEL");
            #endregion

        }

        public void PopulateProduct()
        {
            cmbLOB_ID.DataSource = Cms.CmsWeb.ClsFetcher.LOBs;
            cmbLOB_ID.DataTextField = "LOB_DESC";
            cmbLOB_ID.DataValueField = "LOB_ID";
            cmbLOB_ID.DataBind();
            cmbLOB_ID.Items.Insert(0, "");

            cmbTYPE_ID.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("DISSUR");
            cmbTYPE_ID.DataTextField = "LookupDesc";
            cmbTYPE_ID.DataValueField = "LookupID";
            cmbTYPE_ID.DataBind();
            cmbTYPE_ID.Items.Insert(0, "");

            cmbLEVEL.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("DISSRL");
            cmbLEVEL.DataTextField = "LookupDesc";
            cmbLEVEL.DataValueField = "LookupID";
            cmbLEVEL.DataBind();
            cmbLEVEL.Items.Insert(0, "");

        }/// <summary>
        /// User to Set the Form control (s)'s value Data in the Model info object
        /// </summary>
        /// <param name="objMariTimeInfo"></param>
        private void GetFormValue(ClsDiscountSurchargeInfo objDiscountSurchargeInfo)
        {

            if (txtDISCOUNT_DESCRIPTION.Text.Trim() != "")
            { objDiscountSurchargeInfo.DISCOUNT_DESCRIPTION.CurrentValue = txtDISCOUNT_DESCRIPTION.Text; }
            if (cmbLEVEL.SelectedValue != null)
            { objDiscountSurchargeInfo.LEVEL.CurrentValue = Convert.ToInt32(cmbLEVEL.SelectedValue); }
             if (txtPERCENTAGE.Text.Trim() != "")
             { objDiscountSurchargeInfo.PERCENTAGE.CurrentValue = Convert.ToDouble(txtPERCENTAGE.Text,NfiBaseCurrency); }

            if (txtEFFECTIVE_DATE.Text.Trim() != "")
            { objDiscountSurchargeInfo.EFFECTIVE_DATE.CurrentValue = Convert.ToDateTime(txtEFFECTIVE_DATE.Text); }

            if (txtFINAL_DATE.Text.Trim() != "")
            { objDiscountSurchargeInfo.FINAL_DATE.CurrentValue = Convert.ToDateTime(txtFINAL_DATE.Text); }

            if (cmbTYPE_ID.SelectedItem != null)
            { objDiscountSurchargeInfo.TYPE_ID.CurrentValue = Convert.ToInt32(cmbTYPE_ID.SelectedValue); }

            if (cmbLOB_ID.SelectedItem != null)
            { objDiscountSurchargeInfo.LOB_ID.CurrentValue = Convert.ToInt32(cmbLOB_ID.SelectedValue); }

            //if (cmbSUBLOB_ID.SelectedItem != null)
            { objDiscountSurchargeInfo.SUBLOB_ID.CurrentValue = Convert.ToInt32(hidSUB_LOB.Value); }
            //{ objDiscountSurchargeInfo.DISCOUNT_ID.CurrentValue = Convert.ToInt32(hidDiscountID.Value); }

        }

        /// <summary>
        /// Use to Get the Old Data Based on the ID
        /// </summary>
        /// <param name="MariTime_ID"></param>
        private void GetOldDataObject(Int32 DISCOUNT_ID)
        {
            ClsDiscountSurchargeInfo objDiscountSurchargeInfo;
            objDiscountSurchargeInfo = objDiscountSurcharge.FetchData(DISCOUNT_ID);
            PopulatePageFromEbixModelObject(this.Page, objDiscountSurchargeInfo);
            if(objDiscountSurchargeInfo.PERCENTAGE.CurrentValue.ToString()!="")
                NfiBaseCurrency.NumberDecimalDigits = 2;
                txtPERCENTAGE.Text = objDiscountSurchargeInfo.PERCENTAGE.CurrentValue.ToString("N",NfiBaseCurrency);

            hidOldSUB_LOB.Value = objDiscountSurchargeInfo.SUBLOB_ID.CurrentValue.ToString();
            btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objDiscountSurchargeInfo.IS_ACTIVE.CurrentValue.ToString().Trim());

            hidDiscountID.Value = objDiscountSurchargeInfo.DISCOUNT_ID.CurrentValue.ToString();
            base.SetPageModelObject(objDiscountSurchargeInfo);
         
        }

        private void SetErrorMessage()
        {
            rfvEFFECTIVE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "456_4");
            revEFFECTIVE_DATE.ValidationExpression = aRegExpDate;
            revEFFECTIVE_DATE.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "456_6");
            rfvFINAL_DATE.ErrorMessage =ClsMessages.GetMessage(base.ScreenId, "456_1");
            revFINAL_DATE.ValidationExpression = aRegExpDate;
            revFINAL_DATE.ErrorMessage =ClsMessages.GetMessage(base.ScreenId, "456_6");
            rfvLOB_ID.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("413");
            rfvTYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage("224_29", "1");
            //rfvPERCENTAGE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("493");
            revPERCENTAGE.ValidationExpression = aRegExpDoublePositiveWithZeroFourDeci;//aRegExpDoublePositiveWithZero;
            revPERCENTAGE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2076");
            csvPERCENTAGE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"456_3");
            rfvDISCOUNT_DESCRIPTION.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "456_5");
            cpvFINAL_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("447");
            rfvSUBLOB_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "456_7");
            rfvLEVEL.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "456_10");
            csvFINAL_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "456_11");
            
        }
        #endregion

        #region "Control Events"
        protected void btnSave_Click(object sender, EventArgs e)
        {
            hidSave.Value = "Save";
            strRowId = hidDiscountID.Value;
            ClsDiscountSurchargeInfo objDiscountSurchargeInfo; 
            //For The Save Case 
            try
            {
                if (strRowId.ToUpper().Equals("NEW"))
                {
                    objDiscountSurchargeInfo = new ClsDiscountSurchargeInfo();
                    this.GetFormValue(objDiscountSurchargeInfo);
                    objDiscountSurchargeInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                    objDiscountSurchargeInfo.CREATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now.ToString());
                    objDiscountSurchargeInfo.IS_ACTIVE.CurrentValue = "Y"; //hidIS_ACTIVE.Value;
                    objDiscountSurchargeInfo.LEVEL.CurrentValue = int.Parse(cmbLEVEL.SelectedValue);
                    int intRetval = objDiscountSurcharge.AddDiscountSurcharge(objDiscountSurchargeInfo);

                    if (intRetval > 0)
                    {
                        this.GetOldDataObject(objDiscountSurchargeInfo.DISCOUNT_ID.CurrentValue);

                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "29");
                        hidFormSaved.Value = "1";

                    }
                    else if (intRetval == -1)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "18");
                        hidFormSaved.Value = "2";
                    }

                    else
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "20");
                        hidFormSaved.Value = "2";
                    }
                    lblDelete.Visible = true;
                    lblMessage.Visible = true;
                }

                else //For The Update cse
                {


                    objDiscountSurchargeInfo = (ClsDiscountSurchargeInfo)base.GetPageModelObject();

                    this.GetFormValue(objDiscountSurchargeInfo);
                    //objDiscountSurchargeInfo.IS_ACTIVE.CurrentValue = "Y"; //hidIS_ACTIVE.Value;
                    objDiscountSurchargeInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                    objDiscountSurchargeInfo.LAST_UPDATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now.ToString());
                    objDiscountSurchargeInfo.LEVEL.CurrentValue = int.Parse(cmbLEVEL.SelectedValue);
                   
                    int intRetval = objDiscountSurcharge.UpdateDiscountSurcharge(objDiscountSurchargeInfo);

                    if (intRetval > 0)
                    {
                        this.GetOldDataObject(objDiscountSurchargeInfo.DISCOUNT_ID.CurrentValue);
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "456_2");
                        hidFormSaved.Value = "1";

                    }
                    else if (intRetval == -1)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "18");
                        hidFormSaved.Value = "2";
                    }

                    else
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "20");
                        hidFormSaved.Value = "2";
                    }
                    lblDelete.Visible = false;
                    lblMessage.Visible = true;
                }
                btnActivateDeactivate.Visible = true;
                btnDelete.Visible = true;
                if (objDiscountSurchargeInfo.IS_ACTIVE.CurrentValue == "N")
                {
                    
                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objDiscountSurchargeInfo.IS_ACTIVE.CurrentValue.ToString().Trim());
                }
                else
                {
                    
                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objDiscountSurchargeInfo.IS_ACTIVE.CurrentValue.ToString().Trim());
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

        protected void btnDelete_Click(object sender, EventArgs e)
        {
             ClsDiscountSurchargeInfo objDiscountSurchargeInfo;
            try
            {
                    objDiscountSurchargeInfo = (ClsDiscountSurchargeInfo)base.GetPageModelObject();

                    objDiscountSurchargeInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());

                    int intRetval = objDiscountSurcharge.DeleteDiscountSurcharge(objDiscountSurchargeInfo);
                    if (intRetval > 0)
                    {
                        lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "127");
                        hidFormSaved.Value = "1";
                        trBody.Attributes.Add("style", "display:none");
                    }
                
                        
                    else if (intRetval == -2)
                    {
                        lblDelete.Text = ClsMessages.GetMessage(base.ScreenId, "456_8");
                        hidFormSaved.Value = "2";
                    }
                    else if (intRetval == -1) {
                        lblDelete.Text = ClsMessages.GetMessage(base.ScreenId, "128");
                        hidFormSaved.Value = "2";
                    }
                    lblDelete.Visible = true;
                    lblMessage.Visible = false;
                    
               
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";

            }
        }

        protected void btnActivateDeactivate_Click(object sender, EventArgs e)
        {
            ClsDiscountSurchargeInfo objDiscountSurchargeInfo;

            try
            {
                objDiscountSurchargeInfo = (ClsDiscountSurchargeInfo)base.GetPageModelObject();

                if (objDiscountSurchargeInfo.IS_ACTIVE.CurrentValue == "Y")
                { objDiscountSurchargeInfo.IS_ACTIVE.CurrentValue = "N"; }
                else
                { objDiscountSurchargeInfo.IS_ACTIVE.CurrentValue = "Y"; }


                objDiscountSurchargeInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                objDiscountSurchargeInfo.LAST_UPDATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now.ToString(""));


                int intRetval = objDiscountSurcharge.ActivateDeactivateDiscountSurcharge(objDiscountSurchargeInfo);
                if (intRetval > 0)
                {
                    if (objDiscountSurchargeInfo.IS_ACTIVE.CurrentValue == "N")
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "41");
                        btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objDiscountSurchargeInfo.IS_ACTIVE.CurrentValue.ToString().Trim());
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "40");
                        btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objDiscountSurchargeInfo.IS_ACTIVE.CurrentValue.ToString().Trim());
                    }
                    hidFormSaved.Value = "1";

                    SetPageModelObject(objDiscountSurchargeInfo);
                }
                 else if (intRetval == -2){
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "456_9");
                }
                lblDelete.Visible = false;
                lblMessage.Visible = true;
               
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";

            }
        }
        
        #endregion              
    }
}
