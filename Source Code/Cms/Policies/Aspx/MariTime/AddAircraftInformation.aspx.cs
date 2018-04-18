/******************************************************************************************
	<Author					: Avijit Goswami    
	<Start Date				: 21/03/2010
	<End Date				: - >
	<Description			: - > 
	<Review Date			: - >
	<Reviewed By			: - >
	
Modification History

	<Modified Date			: - > 
	<Modified By			: - > 
	<Purpose				: - >
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
using Cms.CmsWeb;
using System.Resources;
using System.Reflection;
using System.Xml;
using Cms.Model.Policy;
using Cms.BusinessLayer.Blapplication;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher;

namespace Cms.Policies.Aspx.MariTime
{

    public partial class AddAircraftInformation : Cms.CmsWeb.cmsbase
    {
        #region DECLARATION

        ClsAircraftInfo objClsAircraftInfo;
        ClsAircraftDetails objClsAircraftDetails;
        System.Resources.ResourceManager objResourceMgr;
        private string strRowId, strFormSaved; 
               
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidREIN_COMPANY_ID;
        #region AUTOMATIC CONTROL

        protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
        protected System.Web.UI.WebControls.Label lblDelete;
        protected System.Web.UI.WebControls.Label lblMessage;
        string oldXML;
        protected System.Web.UI.WebControls.Label capAIRCRAFT_ID;
        protected System.Web.UI.WebControls.TextBox txtAIRCRAFT_ID;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvAIRCRAFT_ID;
        protected System.Web.UI.WebControls.Label capAgentCode;
        protected System.Web.UI.WebControls.TextBox txtAIRCRAFT_NUMBER;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvAgentCode;
        protected System.Web.UI.WebControls.Label capAgentName;
        protected System.Web.UI.WebControls.TextBox txtAIRLINE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvAgentName;
        protected System.Web.UI.WebControls.Label capAddress1;
        protected System.Web.UI.WebControls.TextBox txtAIRCRAFT_FROM;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvAddress1;
        protected System.Web.UI.WebControls.Label capAddress2;
        protected System.Web.UI.WebControls.TextBox txtAIRCRAFT_TO;
        protected System.Web.UI.WebControls.Label capCity;
        protected System.Web.UI.WebControls.TextBox txtAIRWAY_BILL;
        protected System.Web.UI.WebControls.Label capCountry;
        protected System.Web.UI.WebControls.DropDownList cmbCountry;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCountry;
        protected System.Web.UI.WebControls.Label capSurveyCode;
        protected System.Web.UI.WebControls.TextBox txtSurveyCode;
        
        #endregion AUTOMATIC CONTROL

        #region BUTTON CONTROL
        protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
        protected Cms.CmsWeb.Controls.CmsButton btnReset;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;
        protected Cms.CmsWeb.Controls.CmsButton btnDelete;
        # endregion

        #region HIDDEN VARIABLES
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
        
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidReset;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAIRCRAFT_ID;
        
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidFEDERAL_ID;
        protected System.Web.UI.WebControls.Label capFEDERAL_ID_HID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDETAIL_TYPE_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTAB_TITLES;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAirCraftId;        
        protected System.Web.UI.WebControls.Label capMessages;
        
        # endregion
        # endregion DECELARATION


        private void Page_Load(object sender, System.EventArgs e)
        {
            btnReset.Attributes.Add("onclick", "javascript:return Reset();");
            btnReset.Text = Cms.CmsWeb.ClsMessages.FetchGeneralButtonsText("btnReset");
            btnSave.Text = Cms.CmsWeb.ClsMessages.FetchGeneralButtonsText("btnSave");
            btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1333");

            base.ScreenId = "455_4";
            lblMessage.Visible = false;
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
                        
            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnActivateDeactivate.CmsButtonClass = CmsButtonType.Write;
            btnActivateDeactivate.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            btnDelete.CmsButtonClass = CmsButtonType.Write;
            btnDelete.PermissionString = gstrSecurityXML;

            objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.MariTime.AddAircraftInformation", System.Reflection.Assembly.GetExecutingAssembly());          

            if (!Page.IsPostBack)
            { 
                hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCon");                
                GetDataForEditMode();
                GenerateXML();
            }
            SetHiddenFields();
        }
       private void SetHiddenFields()
       {
           if (Request.QueryString["RISK_ID"] != null && Request.QueryString["RISK_ID"].ToString() != "")
           {
               hidDETAIL_TYPE_ID.Value = Request.QueryString["RISK_ID"].ToString();
           }
       }
        private void GetDataForEditMode()
        {
            try
            {
                objClsAircraftDetails = new ClsAircraftDetails();
                DataSet oDs = objClsAircraftDetails.GetDataForPageControls(this.hidDETAIL_TYPE_ID.Value);                
                if (oDs.Tables[0].Rows.Count > 0)
                {
                    DataRow oDr = oDs.Tables[0].Rows[0];                                     
                    this.txtAIRCRAFT_NUMBER.Text = oDr["AIRCRAFT_NUMBER"].ToString();
                    this.txtAIRLINE.Text = oDr["AIRLINE"].ToString();
                    this.txtAIRCRAFT_FROM.Text = oDr["AIRCRAFT_FROM"].ToString();
                    this.txtAIRCRAFT_TO.Text = oDr["AIRCRAFT_TO"].ToString();
                    this.txtAIRWAY_BILL.Text = oDr["AIRWAY_BILL"].ToString();
                }
            }
            catch (Exception oEx)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);
            }
            finally { }
        }        

        private ClsAircraftInfo GetFormValue()
        {
            ClsAircraftInfo objClsAircraftInfo = new ClsAircraftInfo();            
            objClsAircraftInfo.AIRCRAFT_NUMBER = txtAIRCRAFT_NUMBER.Text;
            objClsAircraftInfo.AIRLINE = txtAIRLINE.Text;
            objClsAircraftInfo.AIRCRAFT_FROM = txtAIRCRAFT_FROM.Text;
            objClsAircraftInfo.AIRCRAFT_TO = txtAIRCRAFT_TO.Text;
            objClsAircraftInfo.AIRWAY_BILL = txtAIRWAY_BILL.Text;
                        
            strFormSaved = hidFormSaved.Value;            
            GenerateXML();
			oldXML		= hidOldData.Value;
            if (hidOldData.Value != "" && hidOldData.Value != "0")
                strRowId = hidAirCraftId.Value;  
            else
                strRowId = "NEW";           
            return objClsAircraftInfo;
        } 

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            //this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.Load += new System.EventHandler(this.Page_Load);
        }
        #endregion

        #region "Web Event Handlers"
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            try
            {
                int intRetVal;	//For retreiving the return value of business class save function
                objClsAircraftDetails = new ClsAircraftDetails();                
                ClsAircraftInfo objClsAircraftInfo = GetFormValue();                
                if (strRowId.ToUpper().Equals("NEW"))
                {
                    objClsAircraftInfo.CREATED_BY = int.Parse(GetUserId());                    
                    objClsAircraftInfo.IS_ACTIVE = "Y";
                    objClsAircraftInfo.CREATED_DATETIME = DateTime.Now;
                    //Calling the add method of business layer class                    
                    intRetVal = objClsAircraftDetails.Add(objClsAircraftInfo);
                    if (intRetVal > 0)
                    {
                        ClsMessages.SetCustomizedXml(GetLanguageCode());                        
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "29");
                        GenerateXML();
                        hidFormSaved.Value = "1";
                        hidIS_ACTIVE.Value = "Y";                        
                        hidAirCraftId.Value = objClsAircraftInfo.AIRCRAFT_ID.ToString();
                        strRowId = hidAirCraftId.Value;
                        hidDETAIL_TYPE_ID.Value = hidAirCraftId.Value;                        
                    }
                    else if (intRetVal == -1)
                    {
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "5");
                        hidFormSaved.Value = "2";
                    }
                    else
                    {
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "20");
                        hidFormSaved.Value = "2";
                    }
                    lblMessage.Visible = true;
                }                
                else //UPDATE CASE
                {
                    //Creating the Model object for holding the Old data
                    ClsAircraftInfo objOldClsAircraftInfo;
                    objOldClsAircraftInfo = new ClsAircraftInfo();
                    //Setting  the Old Page details(XML File containing old details) into the Model Object
                    base.PopulateModelObject(objOldClsAircraftInfo, hidOldData.Value);
                    //Setting those values into the Model object which are not in the page                    
                    objClsAircraftInfo.MODIFIED_BY = int.Parse(GetUserId());
                    objClsAircraftInfo.LAST_UPDATED_DATETIME = DateTime.Now;
                    hidIS_ACTIVE.Value = "Y";
                    //Updating the record using business layer class object
                    intRetVal = objClsAircraftDetails.Update(objOldClsAircraftInfo, objClsAircraftInfo);
                    if (intRetVal > 0)
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "31");
                        hidFormSaved.Value = "1";
                        GenerateXML();
                    }
                    else if (intRetVal == -1)
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "18");
                        hidFormSaved.Value = "1";
                    }
                    else
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "20");
                        hidFormSaved.Value = "1";
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
                ExceptionManager.Publish(ex);
            }
            finally
            {
                if (objClsAircraftDetails != null)
                    objClsAircraftDetails.Dispose();
                btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(hidIS_ACTIVE.Value);
            }
        }
        #endregion

        private void GenerateXML()
        {            
            objClsAircraftDetails = new ClsAircraftDetails();
                try
                {
                    DataSet ds = new DataSet();
                    ds = objClsAircraftDetails.GetDataForPageControls(hidDETAIL_TYPE_ID.Value);
                    if (ds.Tables[0].Rows.Count > 0)
                        hidOldData.Value = ds.GetXml();
                    else
                        hidOldData.Value = "";
                }
                catch (Exception ex)
                {
                    ClsMessages.SetCustomizedXml(GetLanguageCode());
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                    lblMessage.Visible = true;
                    Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                    hidFormSaved.Value = "2";
                }
                finally
                {
                    if (objClsAircraftDetails != null)
                        objClsAircraftDetails.Dispose();
                }            
        }

        //#region DEACTIVATE ACTIVATE BUTTON CLICK
        //private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
        //{
        //    try
        //    {
        //        Cms.BusinessLayer.BlCommon.stuTransactionInfo objStuTransactionInfo = new Cms.BusinessLayer.BlCommon.stuTransactionInfo();
        //        objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
        //        objStuTransactionInfo.loggedInUserName = GetUserName();
        //        //objDepartment =  new ClsDepartment();
        //        Cms.BusinessLayer.BlCommon.ClsReinsurer objReinsurer = new ClsReinsurer();

        //        Model.Maintenance.Reinsurance.ClsReinsurerInfo objReinsurerInfo = new ClsReinsurerInfo();
        //        // objReinsurerInfo = GetFormValue();

        //        string strRetVal = "";
        //        string strCustomInfo = objResourceMgr.GetString("txtREIN_COMAPANY_NAME") + ":" + objReinsurerInfo.REIN_COMAPANY_NAME + "<br>"
        //                              + objResourceMgr.GetString("txtREIN_COMAPANY_CODE") + ":" + objReinsurerInfo.REIN_COMAPANY_CODE + "<br>"
        //                              + this.objResourceMgr.GetString("cmbREIN_COMAPANY_TYPE") + ":" + cmbREIN_COMAPANY_TYPE.Items[cmbREIN_COMAPANY_TYPE.SelectedIndex].Text;
        //        /*strCustomInfo = "Line Of Business:" + cmbREIN_LINE_OF_BUSINESS.Items[cmbREIN_LINE_OF_BUSINESS.SelectedIndex].Text +"<br>"
        //            +"State:" + cmbREIN_STATE.Items[cmbREIN_STATE.SelectedIndex].Text +"<br>"
        //            +"1st Split Coverage:" + cmbREIN_IST_SPLIT_COVERAGE.Items[cmbREIN_IST_SPLIT_COVERAGE.SelectedIndex].Text +"<br>"
        //            +"2nd Split Coverage:" + cmbREIN_2ND_SPLIT_COVERAGE.Items[cmbREIN_2ND_SPLIT_COVERAGE.SelectedIndex].Text;*/
        //        if (hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
        //        {
        //            ClsMessages.SetCustomizedXml(GetLanguageCode());
        //            objStuTransactionInfo.transactionDescription = ClsMessages.GetMessage(NewscreenId, "9");
        //            objReinsurer.TransactionInfoParams = objStuTransactionInfo;
        //            strRetVal = objReinsurer.ActivateDeactivate(hidREIN_COMAPANY_ID.Value, "N", strCustomInfo);
        //            lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "41");
        //            hidIS_ACTIVE.Value = "N";
        //            btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchActivateDeactivateButtonsText("N");
        //        }
        //        else
        //        {
        //            ClsMessages.SetCustomizedXml(GetLanguageCode());
        //            objStuTransactionInfo.transactionDescription = ClsMessages.GetMessage(NewscreenId, "10");
        //            objReinsurer.TransactionInfoParams = objStuTransactionInfo;
        //            objReinsurer.ActivateDeactivate(hidREIN_COMAPANY_ID.Value, "Y", strCustomInfo);
        //            lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "40");
        //            hidIS_ACTIVE.Value = "Y";
        //            btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchActivateDeactivateButtonsText("Y");
        //        }

        //        //				if (strRetVal == "-1")
        //        //				{
        //        //					/*Profit Center is assigned*/
        //        //					lblMessage.Text =  ClsMessages.GetMessage(base.ScreenId,"513");
        //        //					lblDelete.Visible = false;
        //        //				}
        //        hidOldData.Value = objReinsurer.GetDataForPageControls(hidREIN_COMAPANY_ID.Value.ToString()).GetXml();
        //        setEncryptXml();
        //        hidFormSaved.Value = "0";
        //        //GetOldDataXML();
        //        hidReset.Value = "0";

        //    }
        //    catch (Exception ex)
        //    {
        //        ClsMessages.SetCustomizedXml(GetLanguageCode());
        //        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
        //        lblMessage.Visible = true;
        //        Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
        //    }
        //    finally
        //    {
        //        lblMessage.Visible = true;
        //        if (objReinsurer != null)
        //            objReinsurer.Dispose();
        //    }
        //}
        //#endregion

        //private void GetOldDataXML()
        //{
        //    ClsAircraftDetails objReinsurer = new ClsAircraftDetails();
        //    if (hidDETAIL_TYPE_ID.Value.ToString() != "")
        //    {
        //        this.hidOldData.Value = objClsAircraftDetails.GetDataForPageControls(strRowId).GetXml();               
        //    }
        //}      

    }
}