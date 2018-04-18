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
using System.Reflection;
using System.Resources;
using Cms.CmsWeb.Controls;
using Cms.Model.Maintenance;

namespace Cms.CmsWeb.Maintenance
{
    public partial class  AddPortMaster : Cms.CmsWeb.cmsbase
    {
        System.Resources.ResourceManager objResourceMgr;

        string strRowId = "";
        ClsPortMaster objDV;
        private string XmlSchemaFileName = "";
        private string XmlFullFilePath = "";
        string strSysID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = ClsCommon.GetLookupURL();

            base.ScreenId = "239_9_0";
            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            btnActivateDeactivate.CmsButtonClass = CmsButtonType.Write;
            btnActivateDeactivate.PermissionString = gstrSecurityXML;
            btnActivateDeactivate.Visible = true;

            //END:*********** Setting permissions and class (Read/write/execute/delete)  *************
            objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddPortMaster", System.Reflection.Assembly.GetExecutingAssembly());
            hlkFrom_Date.Attributes.Add("OnClick", "fPopCalendar(document.MNT_PORT_MASTER.txtFrom_Date,document.MNT_PORT_MASTER.txtFrom_Date)");
            hlkTo_Date.Attributes.Add("OnClick", "fPopCalendar(document.MNT_PORT_MASTER.txtTo_Date,document.MNT_PORT_MASTER.txtTo_Date)");
            btnReset.Attributes.Add("onclick", "javascript:return ResetForm();");
           if (!Page.IsPostBack)
            {
                if (Request.QueryString["PORT_CODE"] != null && Request.QueryString["PORT_CODE"].ToString().Length > 0)
                    hidDETAIL_TYPE_ID.Value = Request.QueryString["PORT_CODE"].ToString();

                //btnSave.Attributes.Add("onClick","return SelectItem();");

                if (Request.QueryString["PORT_CODE"] != null && Request.QueryString["PORT_CODE"].ToString().Length > 0)
                {
                    hidDETAIL_TYPE_ID.Value = Request.QueryString["PORT_CODE"].ToString();
                    hidOldData.Value = ClsPortMaster.GetXmlForPageControls(hidDETAIL_TYPE_ID.Value);                    
                }
                FillDropDown();
            }
            else // Added by Ruchika Chauhan on 5-March-2012 for TFS Bug # 3635                
            {
                if (hidDETAIL_TYPE_ID.Value == "Y")
                {                    
                    hidLOOKUP.Value = "";
                }
            }
          btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value.ToString()));          
        }

        #region GetFormValue
        /// <summary>
        /// Fetch form's value and stores into model class object and return that object.
        /// </summary>
        private ClsPortMasterInfo GetFormValue()
        {
            //Creating the Model object for holding the New data
            ClsPortMasterInfo objDVInfo = new ClsPortMasterInfo();
            if (hidDETAIL_TYPE_ID.Value!="" && hidDETAIL_TYPE_ID.Value!=null && hidDETAIL_TYPE_ID.Value!="New")
            {
                objDVInfo.PORT_CODE = (txtPort_code.Text == null ? 0 : int.Parse(hidDETAIL_TYPE_ID.Value));
            }
            objDVInfo.ISO_CODE = (txtISO_Code.Text == null ? "" : txtISO_Code.Text);
            objDVInfo.PORT_TYPE = (txtPort_Type.Text == null ? "" : txtPort_Type.Text);
            objDVInfo.COUNTRY = (txtCountry.Text == null ? "" : txtCountry.Text);
            objDVInfo.ADDITIONAL_WAR_RATE = (txtAdditional_War_Rate.Text == null ? 0 : double.Parse(txtAdditional_War_Rate.Text));
            objDVInfo.FROM_DATE = Convert.ToDateTime(Convert.ToDateTime(txtFrom_Date.Text.Trim()).ToShortDateString());
            objDVInfo.TO_DATE = Convert.ToDateTime(Convert.ToDateTime(txtTo_Date.Text.Trim()).ToShortDateString());

            objDVInfo.SETTLEMENT_AGENT_CODE = (txtSettlement_Agent_Code.Text == "" ? "" : txtSettlement_Agent_Code.Text);
            objDVInfo.SETTLING_AGENT_NAME = (hidSettllingAgent.Value == "" ? "" : hidSettllingAgent.Value);

           // objDVInfo.SETTLING_AGENT_NAME = cmbSettling_Agent_Name.SelectedValue;
        


            // if (hidDETAIL_TYPE_ID.Value.ToUpper() != "NEW")
            //objDVInfo.ACCUMULATION_ID= int.Parse(hidDETAIL_TYPE_ID.Value);


            strRowId = hidDETAIL_TYPE_ID.Value;


            return objDVInfo;
        }
       
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            try
            {
                int intRetVal;	//For retreiving the return value of business class save function
                //For retreiving the return value of business class save function
                objDV = new ClsPortMaster();
                //Retreiving the form values into model class object
                ClsPortMasterInfo objDVInfo = GetFormValue();

                if (strRowId.ToUpper().Equals("NEW")) //save case
                {
                //objDVInfo.TYPE_ID = int.Parse(Request["TYPE_ID"].ToString());
                objDVInfo.CREATED_BY = int.Parse(GetUserId());
                objDVInfo.CREATED_DATETIME = DateTime.Now;
                objDVInfo.IS_ACTIVE = "Y";
                //Calling the add method of business layer class
                intRetVal = objDV.Add(objDVInfo, objDVInfo.CREATED_BY);
                if (intRetVal > 0)
                {
                    hidDETAIL_TYPE_ID.Value = objDVInfo.PORT_CODE.ToString();
                    hidIS_ACTIVE.Value = "Y";
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "29");
                    hidFormSaved.Value = "1";
                    hidOldData.Value = ClsPortMaster.GetXmlForPageControls(hidDETAIL_TYPE_ID.Value);
                }
                else if (intRetVal == -1)
                {
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "18");
                    hidFormSaved.Value = "2";
                }
                else
                {
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "20");
                    hidFormSaved.Value = "2";
                }
                lblMessage.Visible = true;
                } // end save case
                else //UPDATE CASE
                {
                    //Creating the Model object for holding the Old data
                    ClsPortMasterInfo objOldDVInfo = new ClsPortMasterInfo();
                    GetOldDataXML();
                    //Setting  the Old Page details(XML File containing old details) into the Model Object
                    base.PopulateModelObject(objOldDVInfo, hidOldData.Value);

                    //Setting those values into the Model object which are not in the page
                    //comment by kuldeep to solve problem in update
                    //objDVInfo.CRITERIA_ID = int.Parse(strRowId);
                    objDVInfo.MODIFIED_BY = int.Parse(GetUserId());
                    objDVInfo.LAST_UPDATED_DATETIME = DateTime.Now;
                    //Updating the record using business layer class object
                    intRetVal = objDV.Update(objOldDVInfo, objDVInfo);
                    if (intRetVal > 0)			// update successfully performed
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "31");
                        hidFormSaved.Value = "1";

                        //PopulateDropDown();
                        hidOldData.Value = ClsPortMaster.GetXmlForPageControls(hidDETAIL_TYPE_ID.Value);
                    }
                    else if (intRetVal == -1)	// Duplicate code exist, update failed
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
                btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(hidIS_ACTIVE.Value);
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
                if (objDV != null)
                    objDV.Dispose();
            }
        }

        private void FillDropDown()
        {
            DataTable dt2 = Cms.BusinessLayer.BlCommon.ClsPortMaster.GetSettlingAgentList();

            cmbSettling_Agent_Name.DataSource = dt2;
            cmbSettling_Agent_Name.DataTextField = "AGENT_NAME";
            cmbSettling_Agent_Name.DataValueField = "AGENT_ID";
            cmbSettling_Agent_Name.DataBind();
            cmbSettling_Agent_Name.Items.Insert(0, " ");
            //cmbSettling_Agent_Name.SelectedIndex = 0; 
        }

        private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
        {            
            ClsPortMasterInfo objPortInfo = GetFormValue();
            ClsPortMaster objPort = new ClsPortMaster();
            int returnResult = 1;
            try
            {
                objPortInfo.PORT_CODE = int.Parse(hidDETAIL_TYPE_ID.Value);
                objPortInfo.MODIFIED_BY = int.Parse(GetUserId());            

                if (hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
                {
                    returnResult = objPort.ActivateDeactivatePortDetails(objPortInfo, "N");
                    if (returnResult > 0)
                    {                        
                        objPortInfo.IS_ACTIVE = "N";
                        btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objPortInfo.IS_ACTIVE.ToString().Trim());
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "41");
                        hidIS_ACTIVE.Value = "N";
                        hidFormSaved.Value = "1";
                    }
                    else if (returnResult == -2)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "853");
                        hidFormSaved.Value = "2";
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "20");
                        hidFormSaved.Value = "2";
                    }
                }
                else
                { 
                    returnResult = objPort.ActivateDeactivatePortDetails(objPortInfo, "Y");
                    if (returnResult > 0)
                    {
                        objPortInfo.IS_ACTIVE = "Y";
                        btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objPortInfo.IS_ACTIVE.ToString().Trim());
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "40");
                        hidIS_ACTIVE.Value = "Y";
                        hidFormSaved.Value = "1";
                    }
                    else if (returnResult == -2)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "853");
                        hidFormSaved.Value = "2";
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "20");
                        hidFormSaved.Value = "2";
                    }
                }
               GetOldDataXML();               
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            finally
            {
                lblMessage.Visible = true;
                if (objPort != null)
                    objPort.Dispose();
                ClientScript.RegisterStartupScript(this.GetType(), "REFRESHGRID", "<script>RefreshWebGrid(1," + hidDETAIL_TYPE_ID.Value + ");</script>");
            }
        }
        private void GetOldDataXML()
        {
            if (hidDETAIL_TYPE_ID.Value != "")
            {
                hidOldData.Value = ClsPortMaster.GetXmlForPageControls(hidDETAIL_TYPE_ID.Value);
            }
            else
                hidOldData.Value = "";

        }
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {


            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnActivateDeactivate.Click += new EventHandler(this.btnActivateDeactivate_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion
    }

}