/******************************************************************************************
	<Author					: Avijit Goswami    
	<Start Date				: 19/03/2010
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
using Cms.Model.Maintenance;
using Cms.BusinessLayer.BlCommon;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher;

namespace Cms.CmsWeb.Maintenance
{

    public partial class AddMarineCargoSetAgents : Cms.CmsWeb.cmsbase
    {

        #region DECLARATION

        ClsMarineCargoSetAgentsInfo objMarineCargoSetAgentsInfo;
        ClsMarineCargoSetAgents objClsMarineCargoSetAgents;
        System.Resources.ResourceManager objResourceMgr;
        private string strRowId, strFormSaved;        

        //private string NewscreenId;        
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidREIN_COMPANY_ID;
        #region AUTOMATIC CONTROL

        protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
        protected System.Web.UI.WebControls.Label lblDelete;
        protected System.Web.UI.WebControls.Label lblMessage;
        string oldXML;
        protected System.Web.UI.WebControls.Label capAgentId;
        protected System.Web.UI.WebControls.TextBox txtAgentId;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvAgentId;
        protected System.Web.UI.WebControls.Label capAgentCode;
        protected System.Web.UI.WebControls.TextBox txtAgentCode;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvAgentCode;
        protected System.Web.UI.WebControls.Label capAgentName;
        protected System.Web.UI.WebControls.TextBox txtAgentName;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvAgentName;
        protected System.Web.UI.WebControls.Label capAddress1;
        protected System.Web.UI.WebControls.TextBox txtAddress1;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvAddress1;
        protected System.Web.UI.WebControls.Label capAddress2;
        protected System.Web.UI.WebControls.TextBox txtAddress2;
        protected System.Web.UI.WebControls.Label capCity;
        protected System.Web.UI.WebControls.TextBox txtCity;
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
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAgentId;
        
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidFEDERAL_ID;
        protected System.Web.UI.WebControls.Label capFEDERAL_ID_HID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDETAIL_TYPE_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTAB_TITLES;
        //protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
        protected System.Web.UI.WebControls.Label capMessages;
        
        # endregion
        # endregion DECELARATION


        private void Page_Load(object sender, System.EventArgs e)
        {
            btnReset.Attributes.Add("onclick", "javascript:return Reset();");
            btnReset.Text = Cms.CmsWeb.ClsMessages.FetchGeneralButtonsText("btnReset");
            btnSave.Text = Cms.CmsWeb.ClsMessages.FetchGeneralButtonsText("btnSave");
            btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1333");

            base.ScreenId = "571_0";
            //NewscreenId = "478_1";
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
            
            objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddMarineCargoSetAgents", System.Reflection.Assembly.GetExecutingAssembly());          

            if (!Page.IsPostBack)
            { 
                hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCon");
                SetHiddenFields();
                SetDropdownList();
                #region "Loading singleton"
                //using singleton object for country and state dropdown
                DataTable dt = Cms.CmsWeb.ClsFetcher.Country;
                cmbCountry.DataSource = dt;
                cmbCountry.DataTextField = "Country_Name";
                cmbCountry.DataValueField = "Country_Id";
                cmbCountry.DataBind();
                cmbCountry.Items[0].Selected = true;

                #endregion//Loading singleton

                GetDataForEditMode();
            }
        }

       private void SetDropdownList()
        {
            DataTable dtCountry = Cms.CmsWeb.ClsFetcher.AllCountry;
            cmbCountry.DataSource = dtCountry;
            cmbCountry.DataTextField = "COUNTRY_NAME";
            cmbCountry.DataValueField = "COUNTRY_NAME";
            cmbCountry.DataBind();
        }

       private void SetHiddenFields()
       {
           if (Request.QueryString["AGENT_ID"] != null && Request.QueryString["AGENT_ID"].ToString() != "")
           {
               hidDETAIL_TYPE_ID.Value = Request.QueryString["AGENT_ID"].ToString();
           }

       }
        private void GetDataForEditMode()
        {
            try
            {
                objClsMarineCargoSetAgents = new ClsMarineCargoSetAgents();
                //if (this.hidREIN_COMAPANY_ID.Value == "" || this.hidREIN_COMAPANY_ID.Value == "0") return;
                DataSet oDs = objClsMarineCargoSetAgents.GetDataForPageControls(this.hidDETAIL_TYPE_ID.Value);
                hidOldData.Value = oDs.GetXml();                

                if (oDs.Tables[0].Rows.Count > 0)
                {
                    DataRow oDr = oDs.Tables[0].Rows[0];
                                     
                    this.txtAgentCode.Text = oDr["AGENT_CODE"].ToString();
                    this.txtAgentName.Text = oDr["AGENT_NAME"].ToString();
                    this.txtAddress1.Text = oDr["AGENT_ADDRESS1"].ToString();
                    this.txtAddress2.Text = oDr["AGENT_ADDRESS2"].ToString();
                    this.txtCity.Text = oDr["AGENT_CITY"].ToString();
                    this.txtSurveyCode.Text = oDr["AGENT_SURVEY_CODE"].ToString();                    
                    
                    ListItem li = new ListItem();
                    li = this.cmbCountry.Items.FindByValue(oDr["AGENT_COUNTRY"].ToString());
                    cmbCountry.SelectedIndex = cmbCountry.Items.IndexOf(li);
                }
            }
            catch (Exception oEx)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);
            }
            finally { }
        }
        

        private ClsMarineCargoSetAgentsInfo GetFormValue()
        {
            //Creating the Model object for holding the New data

            ClsMarineCargoSetAgentsInfo objMarineCargoSetAgentsInfo = new ClsMarineCargoSetAgentsInfo();            
            objMarineCargoSetAgentsInfo.AGENT_CODE = txtAgentCode.Text;
            objMarineCargoSetAgentsInfo.AGENT_NAME = txtAgentName.Text;
            objMarineCargoSetAgentsInfo.AGENT_ADDRESS1 = txtAddress1.Text;
            objMarineCargoSetAgentsInfo.AGENT_ADDRESS2 = txtAddress2.Text;
            objMarineCargoSetAgentsInfo.AGENT_CITY = txtCity.Text;
            objMarineCargoSetAgentsInfo.AGENT_COUNTRY = cmbCountry.SelectedValue;
            objMarineCargoSetAgentsInfo.AGENT_SURVEY_CODE = txtSurveyCode.Text;

            strRowId = hidDETAIL_TYPE_ID.Value;
            strFormSaved = hidFormSaved.Value;
            oldXML = hidOldData.Value;

            strFormSaved = hidFormSaved.Value;
            if (hidOldData.Value != "" && hidDETAIL_TYPE_ID.Value!="New")
            {
                strRowId = hidDETAIL_TYPE_ID.Value;
                objMarineCargoSetAgentsInfo.AGENT_ID = int.Parse(hidDETAIL_TYPE_ID.Value);
            }
            else
            {
                strRowId = "New";
                //objMarineCargoSetAgentsInfo.AGENT_ID = int.Parse(hidDETAIL_TYPE_ID.Value);

            }
            oldXML = hidOldData.Value;
           
            return objMarineCargoSetAgentsInfo;
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
                objClsMarineCargoSetAgents = new ClsMarineCargoSetAgents();
                //Retreiving the form values into model class object
                ClsMarineCargoSetAgentsInfo objClsMarineCargoSetAgentsInfo = GetFormValue();
                //objClsMarineCargoSetAgentsInfo.CUSTOM_INFO = objResourceMgr.GetString("txtREIN_COMAPANY_NAME") + ":" + objClsMarineCargoSetAgentsInfo.REIN_COMAPANY_NAME + "<br>"
                //                      + objResourceMgr.GetString("txtREIN_COMAPANY_CODE") + ":" + objClsMarineCargoSetAgentsInfo.REIN_COMAPANY_CODE + "<br>"
                //                      + this.objResourceMgr.GetString("cmbREIN_COMAPANY_TYPE") + ":" + objClsMarineCargoSetAgentsInfo.Items[objClsMarineCargoSetAgentsInfo.SelectedIndex].Text;
                if (strRowId.ToUpper().Equals("NEW")) //Add Mode
                {
                    objClsMarineCargoSetAgentsInfo.CREATED_BY = int.Parse(GetUserId());                    
                    objClsMarineCargoSetAgentsInfo.IS_ACTIVE = "Y";

                    //Calling the add method of business layer class
                    //intRetVal = objClsMarineCargoSetAgents.Add(objClsMarineCargoSetAgentsInfo, xmlString);
                    intRetVal = objClsMarineCargoSetAgents.Add(objClsMarineCargoSetAgentsInfo);
                    if (intRetVal > 0)
                    {
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
                        hidDETAIL_TYPE_ID.Value = objClsMarineCargoSetAgentsInfo.AGENT_ID.ToString();
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "29");
                        hidFormSaved.Value = "1";
                        hidIS_ACTIVE.Value = "Y";
                        this.hidOldData.Value = objClsMarineCargoSetAgents.GetDataForPageControls(this.hidDETAIL_TYPE_ID.Value).GetXml();
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
                // end save case
                else //UPDATE CASE
                {
                    //Creating the Model object for holding the Old data
                    ClsMarineCargoSetAgentsInfo objOldClsMarineCargoSetAgentsInfo;
                    objOldClsMarineCargoSetAgentsInfo = new ClsMarineCargoSetAgentsInfo();

                    //Setting  the Old Page details(XML File containing old details) into the Model Object
                    base.PopulateModelObject(objOldClsMarineCargoSetAgentsInfo, hidOldData.Value);

                    //Setting those values into the Model object which are not in the page
                    //ObjLookUpInfo.LOOKUP_UNIQUE_ID = strRowId;
                    objClsMarineCargoSetAgentsInfo.MODIFIED_BY = int.Parse(GetUserId());
                    objClsMarineCargoSetAgentsInfo.LAST_UPDATED_DATETIME = DateTime.Now;
                    hidIS_ACTIVE.Value = "Y";
                    objClsMarineCargoSetAgentsInfo.IS_ACTIVE = hidIS_ACTIVE.Value;

                    //Updating the record using business layer class object
                    intRetVal = objClsMarineCargoSetAgents.Update(objOldClsMarineCargoSetAgentsInfo, objClsMarineCargoSetAgentsInfo);
                    if (intRetVal > 0)			// update successfully performed
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "31");
                        hidFormSaved.Value = "1";
                        hidOldData.Value = ClsVesselMaster.GetLookUpDetailXml(int.Parse(hidDETAIL_TYPE_ID.Value));
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
                if (objClsMarineCargoSetAgents != null)
                    objClsMarineCargoSetAgents.Dispose();
                btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(hidIS_ACTIVE.Value);
            }
        }
        #endregion        

        private void GenerateXML(string AgentId)
        {
            string strAgentId = AgentId;
            objClsMarineCargoSetAgents = new ClsMarineCargoSetAgents();

            if (strAgentId != "" && strAgentId != null)
            {
                try
                {
                    DataSet ds = new DataSet();
                    ds = objClsMarineCargoSetAgents.GetDataForPageControls(strAgentId);
                    hidOldData.Value = ds.GetXml();                    
                    //hidFormSaved.Value="1"; 
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
                    if (objClsMarineCargoSetAgents != null)
                        objClsMarineCargoSetAgents.Dispose();
                }
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

        private void GetOldDataXML()
        {
            Cms.BusinessLayer.BlCommon.ClsMarineCargoSetAgents objReinsurer = new ClsMarineCargoSetAgents();
            if (hidAgentId.Value.ToString() != "")
            {
                this.hidOldData.Value = objClsMarineCargoSetAgents.GetDataForPageControls(strRowId).GetXml();               
            }
        }      

    }
}
