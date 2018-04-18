/******************************************************************************************
<Author				: - Pradeep Kushwaha
<Start Date			: -	20-04-2010
<End Date			: -	
<Description		: -  
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: -  
<Modified By		: -   
<Purpose			: -   
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
using Cms.Blapplication;
using System.Resources;
using System.Reflection;
 
using Cms.Model.Policy;
namespace Cms.Policies.Aspx
{
    public partial class ProtectiveDevicesInfo : Cms.Policies.policiesbase
    {

        #region for Controls Declaration
        protected System.Web.UI.WebControls.Label capFIRE_EXTINGUISHER;
        protected System.Web.UI.WebControls.CheckBox chkFIRE_EXTINGUISHER;
        protected System.Web.UI.WebControls.Label capSPL_FIRE_EXTINGUISHER_UNIT;
        protected System.Web.UI.WebControls.CheckBox chkSPL_FIRE_EXTINGUISHER_UNIT;
        protected System.Web.UI.WebControls.Label capMANUAL_FOAM_SYSTEM;
        protected System.Web.UI.WebControls.CheckBox chkMANUAL_FOAM_SYSTEM;
        protected System.Web.UI.WebControls.Label capFOAM;
        protected System.Web.UI.WebControls.CheckBox chkFOAM;
        protected System.Web.UI.WebControls.Label capINERT_GAS_SYSTEM;
        protected System.Web.UI.WebControls.CheckBox chkINERT_GAS_SYSTEM;
        protected System.Web.UI.WebControls.Label capMANUAL_INERT_GAS_SYSTEM;
        protected System.Web.UI.WebControls.CheckBox chkMANUAL_INERT_GAS_SYSTEM;
        protected System.Web.UI.WebControls.Label capCOMBAT_CARS;
        protected System.Web.UI.WebControls.CheckBox chkCOMBAT_CARS;
        protected System.Web.UI.WebControls.Label capCORRAL_SYSTEM;
        protected System.Web.UI.WebControls.CheckBox chkCORRAL_SYSTEM;
        protected System.Web.UI.WebControls.Label capALARM_SYSTEM;
        protected System.Web.UI.WebControls.CheckBox chkALARM_SYSTEM;
        protected System.Web.UI.WebControls.Label capFREE_HYDRANT;
        protected System.Web.UI.WebControls.CheckBox chkFREE_HYDRANT;
        protected System.Web.UI.WebControls.Label capSPRINKLERS;
        protected System.Web.UI.WebControls.CheckBox chkSPRINKLERS;
        protected System.Web.UI.WebControls.Label capSPRINKLERS_CLASSIFICATION;
        protected System.Web.UI.WebControls.TextBox txtSPRINKLERS_CLASSIFICATION;
        protected System.Web.UI.WebControls.Label capFIRE_FIGHTER;
        protected System.Web.UI.WebControls.TextBox txtFIRE_FIGHTER;
        protected System.Web.UI.WebControls.Label capQUESTIION_POINTS;
        protected System.Web.UI.WebControls.TextBox txtQUESTIION_POINTS;
        protected Cms.CmsWeb.Controls.CmsButton btnReset;
        protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
        protected Cms.CmsWeb.Controls.CmsButton btnDelete;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;

        #endregion
        string CalledFrom = string.Empty;
        ClsProducts objProducts = new ClsProducts();
        System.Resources.ResourceManager objResourceMgr;
        private string strRowId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            #region setting screen id
            if (Request.QueryString["CalledFrom"] != null && Request.QueryString["CalledFrom"].ToString() != "")
                CalledFrom = Request.QueryString["CalledFrom"].ToString();
            switch (CalledFrom) 
            {
                case "CompCondo":
                    base.ScreenId = COMPREHENSIVE_CONDOMINIUMscreenId.PROTECTION_DEVICES;
                    break;
                case "RISK":
                    base.ScreenId = DIVERSIFIED_RISKSscreenId.PROTECTION_DEVICES;
                    break;
                default :
                    base.ScreenId = "450_0";
                    break;

            }
            
            #endregion
            this.SetErrorMessages();
            #region setting security Xml
            btnDelete.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Delete;
            btnDelete.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            btnActivateDeactivate.CmsButtonClass = CmsButtonType.Write;
            btnActivateDeactivate.PermissionString = gstrSecurityXML;

            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            #endregion
            
            objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.ProtectiveDevicesInfo", System.Reflection.Assembly.GetExecutingAssembly());
            if (!Page.IsPostBack)
            {
                this.SetCaptions();

                btnReset.Attributes.Add("onclick", "javascript: return ResetTheForm();");

               
                if (Request.QueryString["LOCATION_ID"] != null && Request.QueryString["LOCATION_ID"].ToString() != "")
                {
                    
                    string[] Location_ID = Request.QueryString["LOCATION_ID"].ToString().Split(',');

                    hidLocationId.Value =Location_ID[0];
                    this.GetOldDataObject();
                    strRowId = hidLocationId.Value;
                }
                if(Request.QueryString["RISK_ID"] != null && Request.QueryString["RISK_ID"].ToString() != "")
                {

                    hidRiskId.Value = Request.QueryString["RISK_ID"].ToString();
                    //this.GetOldDataObject();
                    //strRowId = hidRiskId.Value;
                }
               

            }
            
            
        }
        /// <summary>
        /// Use to se the Error Messages for controls validation
        /// </summary>
        private void SetErrorMessages()
        {
            ClsMessages.SetCustomizedXml(GetLanguageCode());
            revSPRINKLERS_CLASSIFICATION.ValidationExpression = aRegExpTextArea100;
            revSPRINKLERS_CLASSIFICATION.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "4");

            revFIRE_FIGHTER.ValidationExpression = aRegExpTextArea100;//string
            revFIRE_FIGHTER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");


            revQUESTIION_POINTS.ValidationExpression = aRegExpDoublePositiveWithZero;
            revQUESTIION_POINTS.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "6");
        }

        /// <summary>
        /// Use to set the caption for every controls
        /// </summary>
        private void SetCaptions()
        {
            
            capFIRE_EXTINGUISHER.Text = objResourceMgr.GetString("chkFIRE_EXTINGUISHER");
            capSPL_FIRE_EXTINGUISHER_UNIT.Text = objResourceMgr.GetString("chkSPL_FIRE_EXTINGUISHER_UNIT");
            capMANUAL_FOAM_SYSTEM.Text = objResourceMgr.GetString("chkMANUAL_FOAM_SYSTEM");
            capFOAM.Text = objResourceMgr.GetString("chkFOAM");
            capINERT_GAS_SYSTEM .Text = objResourceMgr.GetString("chkINERT_GAS_SYSTEM");
            capMANUAL_INERT_GAS_SYSTEM.Text = objResourceMgr.GetString("chkMANUAL_INERT_GAS_SYSTEM");
            capCOMBAT_CARS.Text = objResourceMgr.GetString("chkCOMBAT_CARS");
            capCORRAL_SYSTEM.Text = objResourceMgr.GetString("chkCORRAL_SYSTEM");
            capALARM_SYSTEM.Text = objResourceMgr.GetString("chkALARM_SYSTEM");
            capFREE_HYDRANT.Text = objResourceMgr.GetString("chkFREE_HYDRANT");
            capSPRINKLERS .Text = objResourceMgr.GetString("chkSPRINKLERS");
            capSPRINKLERS_CLASSIFICATION.Text = objResourceMgr.GetString("txtSPRINKLERS_CLASSIFICATION");
            capFIRE_FIGHTER.Text = objResourceMgr.GetString("txtFIRE_FIGHTER");
            capQUESTIION_POINTS.Text = objResourceMgr.GetString("txtQUESTIION_POINTS");
            lblMandatory.Text = objResourceMgr.GetString("lblMandatory");
       }


        /// <summary>
        /// Use to Get the Old Data  
        /// </summary>
        /// <param name="MariTime_ID"></param>
        private void GetOldDataObject()
        {
             ClsProtectiveDevicesInfo ObjProtectiveDevicesInfo=new ClsProtectiveDevicesInfo();

             if ((hidRiskId.Value != "") && (hidRiskId.Value!="0"))
                 ObjProtectiveDevicesInfo.RISK_ID.CurrentValue = Convert.ToInt32(hidRiskId.Value);
             else
                 ObjProtectiveDevicesInfo.RISK_ID.CurrentValue = GetEbixIntDefaultValue();

             if ((hidLocationId.Value != "") && (hidLocationId.Value != "0" ))
                ObjProtectiveDevicesInfo.LOCATION_ID.CurrentValue =Convert.ToInt32(hidLocationId.Value);
             else
                 ObjProtectiveDevicesInfo.LOCATION_ID.CurrentValue = GetEbixIntDefaultValue();

             ObjProtectiveDevicesInfo.CUSTOMER_ID.CurrentValue = int.Parse(GetCustomerID());
             ObjProtectiveDevicesInfo.POLICY_ID.CurrentValue = int.Parse(GetPolicyID());
             ObjProtectiveDevicesInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(GetPolicyVersionID());
             
             if (objProducts.FetchProtectiveDevicesInfoData(ref ObjProtectiveDevicesInfo))
             {
                 
                 PopulatePageFromEbixModelObject(this.Page, ObjProtectiveDevicesInfo);
                 hidCalledFor.Value = "UPDATE";
                 btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(ObjProtectiveDevicesInfo.IS_ACTIVE.CurrentValue.ToString().Trim());
             }
             else
             {
                 btnActivateDeactivate.Visible = false;
                 btnDelete.Visible = false;
                 hidCalledFor.Value = "NEW";

             }
            
                

            base.SetPageModelObject(ObjProtectiveDevicesInfo);
        }//private void GetOldDataObject(Int32 ProtectiveDeviceID)

        private void GetFormValues(ClsProtectiveDevicesInfo ObjProtectiveDevicesInfo)
        {
            try
            {

                if ((hidRiskId.Value != "") && (hidRiskId.Value != "0"))
                    ObjProtectiveDevicesInfo.RISK_ID.CurrentValue = Convert.ToInt32(hidRiskId.Value);
                else
                      ObjProtectiveDevicesInfo.RISK_ID.CurrentValue = GetEbixIntDefaultValue();

                   
                /////////////////////
                if (chkFIRE_EXTINGUISHER.Checked)
                    ObjProtectiveDevicesInfo.FIRE_EXTINGUISHER.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.YES);// 10963;
                else
                    ObjProtectiveDevicesInfo.FIRE_EXTINGUISHER.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.NO);// 10964;

                if (chkSPL_FIRE_EXTINGUISHER_UNIT.Checked)
                    ObjProtectiveDevicesInfo.SPL_FIRE_EXTINGUISHER_UNIT.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.YES);
                else
                    ObjProtectiveDevicesInfo.SPL_FIRE_EXTINGUISHER_UNIT.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.NO);

                if (chkFOAM.Checked)
                    ObjProtectiveDevicesInfo.FOAM.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.YES);
                else
                    ObjProtectiveDevicesInfo.FOAM.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.NO);

                if (chkFREE_HYDRANT.Checked)
                    ObjProtectiveDevicesInfo.FREE_HYDRANT.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.YES);
                else
                    ObjProtectiveDevicesInfo.FREE_HYDRANT.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.NO);


                if (chkMANUAL_FOAM_SYSTEM.Checked)
                    ObjProtectiveDevicesInfo.MANUAL_FOAM_SYSTEM.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.YES);
                else
                    ObjProtectiveDevicesInfo.MANUAL_FOAM_SYSTEM.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.NO);


                if (chkINERT_GAS_SYSTEM.Checked)
                    ObjProtectiveDevicesInfo.INERT_GAS_SYSTEM.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.YES);
                else
                    ObjProtectiveDevicesInfo.INERT_GAS_SYSTEM.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.NO);


                if (chkMANUAL_INERT_GAS_SYSTEM.Checked)
                    ObjProtectiveDevicesInfo.MANUAL_INERT_GAS_SYSTEM.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.YES);
                else
                    ObjProtectiveDevicesInfo.MANUAL_INERT_GAS_SYSTEM.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.NO);

                if (chkCOMBAT_CARS.Checked)
                    ObjProtectiveDevicesInfo.COMBAT_CARS.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.YES);
                else
                    ObjProtectiveDevicesInfo.COMBAT_CARS.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.NO);

                if (chkCORRAL_SYSTEM.Checked)
                    ObjProtectiveDevicesInfo.CORRAL_SYSTEM.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.YES);
                else
                    ObjProtectiveDevicesInfo.CORRAL_SYSTEM.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.NO);

                if (chkALARM_SYSTEM.Checked)
                    ObjProtectiveDevicesInfo.ALARM_SYSTEM.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.YES);
                else
                    ObjProtectiveDevicesInfo.ALARM_SYSTEM.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.NO);

                
                if (chkFIRE_EXTINGUISHER.Checked)
                    ObjProtectiveDevicesInfo.FIRE_EXTINGUISHER.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.YES);
                else
                    ObjProtectiveDevicesInfo.FIRE_EXTINGUISHER.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.NO);

                if (chkSPRINKLERS.Checked)
                    ObjProtectiveDevicesInfo.SPRINKLERS.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.YES);
                else
                    ObjProtectiveDevicesInfo.SPRINKLERS.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.NO);


                if (txtSPRINKLERS_CLASSIFICATION.Text.ToString().Trim() != "")
                    ObjProtectiveDevicesInfo.SPRINKLERS_CLASSIFICATION.CurrentValue = txtSPRINKLERS_CLASSIFICATION.Text.ToString();
                else
                    ObjProtectiveDevicesInfo.SPRINKLERS_CLASSIFICATION.CurrentValue = String.Empty;

                if (txtFIRE_FIGHTER.Text.ToString().Trim() != "")
                    ObjProtectiveDevicesInfo.FIRE_FIGHTER.CurrentValue = txtFIRE_FIGHTER.Text.ToString();
                else
                    ObjProtectiveDevicesInfo.FIRE_FIGHTER.CurrentValue = String.Empty;

                if (txtQUESTIION_POINTS.Text.ToString().Trim() != "")
                    ObjProtectiveDevicesInfo.QUESTIION_POINTS.CurrentValue = Convert.ToDouble(txtQUESTIION_POINTS.Text,numberFormatInfo);
                else
                    ObjProtectiveDevicesInfo.QUESTIION_POINTS.CurrentValue =GetEbixDoubleDefaultValue();

                ///////////////
                if ((hidLocationId.Value != "") && (hidLocationId.Value!="0"))
                    ObjProtectiveDevicesInfo.LOCATION_ID.CurrentValue = Convert.ToInt32(hidLocationId.Value );
                else
                    ObjProtectiveDevicesInfo.LOCATION_ID.CurrentValue = GetEbixIntDefaultValue();


            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }

        ///// <summary>
        ///// Required method for Designer support - do not modify
        ///// the contents of this method with the code editor.
        ///// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ClsProtectiveDevicesInfo objProtectiveDevicesInfo;
            ClsMessages.SetCustomizedXml(GetLanguageCode());
            try
            {

                objProtectiveDevicesInfo = (ClsProtectiveDevicesInfo)base.GetPageModelObject();

                objProtectiveDevicesInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());

                int intRetval = objProducts.DeleteProtectiveDevicesInfo(objProtectiveDevicesInfo);
                if (intRetval > 0)
                {
                    lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "127");
                    hidFormSaved.Value = "1";
                    //trBody.Attributes.Add("style", "display:none");
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
         
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int intRetval;
            strRowId = hidCalledFor.Value;
            ClsMessages.SetCustomizedXml(GetLanguageCode());
            ClsProtectiveDevicesInfo ObjProtectiveDevicesInfo;
            try
            {
                //For The Save Case                
                if (strRowId.ToUpper().Equals("NEW"))
                {

                    ObjProtectiveDevicesInfo = new ClsProtectiveDevicesInfo();
                    this.GetFormValues(ObjProtectiveDevicesInfo);

                    ObjProtectiveDevicesInfo.CUSTOMER_ID.CurrentValue = int.Parse(GetCustomerID());
                    ObjProtectiveDevicesInfo.POLICY_ID.CurrentValue = int.Parse(GetPolicyID());
                    ObjProtectiveDevicesInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(GetPolicyVersionID());

                    ObjProtectiveDevicesInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                    ObjProtectiveDevicesInfo.CREATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now);
                    ObjProtectiveDevicesInfo.IS_ACTIVE.CurrentValue = "Y";


                    intRetval = objProducts.AddProtectiveDevicesInfo(ObjProtectiveDevicesInfo);

                    if (intRetval > 0)
                    {
                        this.GetOldDataObject();

                        hidRiskId.Value = ObjProtectiveDevicesInfo.RISK_ID.CurrentValue.ToString();
                        hidLocationId.Value = ObjProtectiveDevicesInfo.LOCATION_ID.CurrentValue.ToString();


                        lblMessage.Text = ClsMessages.GetMessage("G", "29");
                        hidFormSaved.Value = "1";

                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.GetMessage("G", "20");
                        hidFormSaved.Value = "2";
                    }
                    lblDelete.Visible = false;
                    lblMessage.Visible = true;
                    //base.OpenEndorsementDetails();
                } // if (strRowId.ToUpper().Equals("NEW"))

                else //For The Update cse
                {


                    ObjProtectiveDevicesInfo = (ClsProtectiveDevicesInfo)base.GetPageModelObject();
                    this.GetFormValues(ObjProtectiveDevicesInfo);
                    ObjProtectiveDevicesInfo.IS_ACTIVE.CurrentValue = "Y"; //hidIS_ACTIVE.Value;
                    ObjProtectiveDevicesInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                    ObjProtectiveDevicesInfo.LAST_UPDATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now);
                    //ObjProtectiveDevicesInfo.RISK_ID.CurrentValue =Convert.ToInt32(hidRiskId.Value); 
                    
                    
                    intRetval = objProducts.UpdateProtectiveDevicesInfo(ObjProtectiveDevicesInfo);

                    if (intRetval > 0)
                    {
                        this.GetOldDataObject();


                        lblMessage.Text = ClsMessages.GetMessage("G", "31");
                        hidFormSaved.Value = "1";

                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.GetMessage("G", "20");
                        hidFormSaved.Value = "2";
                    }
                    lblDelete.Visible = false;
                    lblMessage.Visible = true;
                    base.OpenEndorsementDetails();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "3") + " - " + ex.Message;
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

        protected void btnActivateDeactivate_Click(object sender, EventArgs e)
        {
            ClsProtectiveDevicesInfo ObjProtectiveDevicesInfo;
            ClsMessages.SetCustomizedXml(GetLanguageCode());
            try
            {
                ObjProtectiveDevicesInfo = (ClsProtectiveDevicesInfo)base.GetPageModelObject();

                if (ObjProtectiveDevicesInfo.IS_ACTIVE.CurrentValue == "Y")
                { ObjProtectiveDevicesInfo.IS_ACTIVE.CurrentValue = "N"; }
                else
                { ObjProtectiveDevicesInfo.IS_ACTIVE.CurrentValue = "Y"; }


                ObjProtectiveDevicesInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                ObjProtectiveDevicesInfo.LAST_UPDATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"));


                int intRetval = objProducts.ActivateDeactivateProtectiveDevicesInfo(ObjProtectiveDevicesInfo);

                if (intRetval > 0)
                {
                    if (ObjProtectiveDevicesInfo.IS_ACTIVE.CurrentValue == "N")
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "41");
                    }
                    else
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "40");
                    }
                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(ObjProtectiveDevicesInfo.IS_ACTIVE.CurrentValue.ToString().Trim());
                    hidFormSaved.Value = "1";

                    base.SetPageModelObject(ObjProtectiveDevicesInfo);
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



