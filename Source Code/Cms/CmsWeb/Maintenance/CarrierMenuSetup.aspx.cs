
/******************************************************************************************
	<Author					: praveer panghal >
	<Start Date				: 2 sep 2011 -	>
	<End Date				: - >
	<Description			: - >This file is used to Set menu according to carrier
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - 09 sep 2011 >
	<Modified By			: -praveer panghal > 
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
using Cms.BusinessLayer.BlApplication;
using Cms.CustomException;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb;
using Cms.Model.Maintenance.Security;
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher;
using Cms.BusinessLayer.BlCommon;
using System.Text;
using System.Xml;
using System.Collections.Generic;
using System.IO;
using Cms.CmsWeb.Utils;


namespace Cms.CmsWeb.Maintenance
{
    ///<summary>   
    /// This Class is used to provide interface to Menu setup according to carrier. 
    /// </summary>

    public partial class CarrierMenuSetup : Cms.CmsWeb.cmsbase
    {
        #region Page Control Declarations
        System.Resources.ResourceManager objResourceMgr;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {  // Statements which may throw exceptions
                Ajax.Utility.RegisterTypeForAjax(typeof(CarrierMenuSetup));
                base.ScreenId = "552";
                objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.CarrierMenuSetup", System.Reflection.Assembly.GetExecutingAssembly());
                #region Setting permissions
                btnReset.CmsButtonClass = CmsButtonType.Write;
                btnReset.PermissionString = gstrSecurityXML;
                btnSave.CmsButtonClass = CmsButtonType.Write;
                btnSave.PermissionString = gstrSecurityXML;
                btnSelect.CmsButtonClass = CmsButtonType.Write;
                btnSelect.PermissionString = gstrSecurityXML;
                #endregion
                btnReset.Attributes.Add("onclick", "javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
                capMessages.Text = "Menu Setup";
                if (!IsPostBack)
                {

                    LoadDropDown();
                    LoadSubModule();

                }
            }
            catch (Exception ex)
            {
                // Exception caught and handled here

                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);//Publish Exceptions
            }
        }

        private void GetOldData()
        {
            try
            {  // Statements which may throw exceptions

                int MODULE = 1; // Declaring and initializing the variable of integer type at the same time.
                int SUB_MODULE = 1; // Declaring and initializing the variable of integer type at the same time.
                int Carrier_Id=1; // Declaring and initializing the variable of integer type at the same time.

                ClsSecurity objSecurity = new ClsSecurity();// Will return an object.
                if (cmbMODULE.SelectedValue != null && cmbMODULE.SelectedValue != "")
                {
                    if (cmbMODULE.SelectedValue == "All")
                        MODULE = 0;
                    else
                        MODULE = int.Parse(cmbMODULE.SelectedValue);
                }
                if (cmbSUB_MODULE.SelectedValue != null && cmbSUB_MODULE.SelectedValue != "")
                {
                    if (cmbSUB_MODULE.SelectedValue == "All")
                        SUB_MODULE = 0;
                    else
                        SUB_MODULE = int.Parse(cmbSUB_MODULE.SelectedValue);
                }              
                if (cmbCarrier.SelectedValue != null && cmbCarrier.SelectedValue != "")
                {
                    Carrier_Id = int.Parse(cmbCarrier.SelectedValue);
                }

                hidOldDatas.Value = objSecurity.GetScreen(MODULE, SUB_MODULE, Carrier_Id);
            }
            catch (Exception ex)
            {
                // Exception caught and handled here

                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);//Publish Exceptions
            }

        }
        private void LoadDropDown()// load the drop down list for selecting the modules
        {
            try
            {  // Statements which may throw exceptions

                ClsSecurity objsecurity = new ClsSecurity();// Will return an object.
                DataTable dtModule = objsecurity.GetModule().Tables[1];// will return data table.
                cmbMODULE.DataSource = dtModule;
                cmbMODULE.DataValueField = "MODULE_ID";
                cmbMODULE.DataTextField = "MODULE_NAME";
                cmbMODULE.DataBind();
                cmbMODULE.Items.Insert(0, "All");

                DataTable dtCarrier = objsecurity.GetCarrier();// will return data table.
                cmbCarrier.DataSource = dtCarrier;
                cmbCarrier.DataValueField = "CARRIER_ID";
                cmbCarrier.DataTextField = "CARRIER_NAME";
                cmbCarrier.DataBind();
                cmbCarrier.Items.Insert(0, "");
            }
            catch (Exception ex)
            {  // Exception caught and handled here

                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);//Publish Exceptions
            }
        }


        private void LoadSubModule() {
            try
            {  // Statements which may throw exceptions
                ClsSecurity objsecurity = new ClsSecurity();// Will return an object.
                DataTable dtsubModule = objsecurity.GetModule().Tables[2];// will return data table.
                cmbSUB_MODULE.DataSource = dtsubModule;
                cmbSUB_MODULE.DataValueField = "SUB_MODULE_ID";
                cmbSUB_MODULE.DataTextField = "SUB_MODULE_NAME";
                cmbSUB_MODULE.DataBind();
                cmbSUB_MODULE.Items.Insert(0, "All");
            }
            catch (Exception ex)
            {  // Exception caught and handled here

                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);//Publish Exceptions
            }
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) //  default OnInit implementation.
        {
            InitializeComponent();
            base.OnInit(e);// Call the base method, which calls OnInit of the control,
            // which raises the control Init event.

        }
        private void InitializeComponent()
        {
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            //  this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion


        private void btnSelect_Click(object sender, System.EventArgs e) // This method will fetch the Menulist on the basis of selected module in drop down list.
        {
            try
            { // Statements which may throw exceptions

                GetOldData();
                string SUB_MODULE_ID = cmbSUB_MODULE.SelectedValue;
                if (cmbMODULE.SelectedValue != "" && cmbMODULE.SelectedValue != null && cmbMODULE.SelectedValue != "All" && cmbMODULE.SelectedValue != "1")
                {
                   
               DataSet ds = AjaxFillSubModule(cmbMODULE.SelectedValue);
               cmbSUB_MODULE.DataSource = ds;             
               cmbSUB_MODULE.DataValueField = "SUB_MODULE_ID";
               cmbSUB_MODULE.DataTextField = "SUB_MODULE_NAME";
               cmbSUB_MODULE.DataBind();
               cmbSUB_MODULE.Items.Insert(0, "All");
               if (hidsubmoduleid.Value != "" )
                   cmbSUB_MODULE.SelectedValue = hidsubmoduleid.Value;
               else
                   cmbSUB_MODULE.SelectedValue = SUB_MODULE_ID;    

                }
                else if (cmbMODULE.SelectedValue == "All")
                {                   
                    LoadSubModule();
                    cmbSUB_MODULE.SelectedValue = SUB_MODULE_ID;   
                }
                else if (cmbMODULE.SelectedValue == "1")
                {
                    cmbSUB_MODULE.Items.Clear();                 
                    cmbSUB_MODULE.Items.Insert(0, "All");
                }
                lblMessage.Visible = false;
            }
            catch (Exception ex)
            {
                // Exception caught and handled here

                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);//Publish Exceptions
            }
        }

        private void btnSave_Click(object sender, System.EventArgs e)   // This method will Activate and Deactivate the Menu Item.
        {
            ClsSecurity objSecurity = new ClsSecurity();// Will return an object.
            ArrayList arr_menu_isactive = new ArrayList();
            string menu_isactive = "";// Declaring and initializing the variable of string type at the same time
            string[] screenList;// Declaring  the variable of string array type.
            int intRetVal = 0; // Declaring and initializing the variable of integer type at the same time
            int Carrier_Id = 1; // Declaring and initializing the variable of integer type at the same time.
            try
            {   // Statements which may throw exceptions


                screenList = hidIS_ACTIVE.Value.Split('~');// Split string on tilde(~)
                if (cmbCarrier.SelectedValue != null && cmbCarrier.SelectedValue != "")
                {
                    Carrier_Id = int.Parse(cmbCarrier.SelectedValue);
                }

                if (screenList[0] == "0")  // In case when screenlist is Zero i.e no checkbox of IS_ACTIVE is selected and user click the save button
                {                   
                    hidFormSaved.Value = "2";
                    return;
                }

                foreach (string screen in screenList)// Loop through string array with a foreach loop.
                {

                    if (screen != "")
                    {
                        menu_isactive = screen;

                    }
                    arr_menu_isactive.Add(menu_isactive);// add into arrary list

                }
                intRetVal = objSecurity.UpdateMenuList(arr_menu_isactive, Carrier_Id);

                if (intRetVal > 0)
                {
                    lblMessage.Text = ClsMessages.FetchGeneralMessage("29");
                    GetOldData();
                    hidFormSaved.Value = "1";

                }
                else
                {
                    lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                }
                lblMessage.Visible = true;
            }
            catch (Exception ex)
            {
                // Exception caught and handled here

                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);//Publish Exceptions
            }
        }

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
        public DataSet AjaxFillSubModule(string ModuleId)
        {
            try
            {               
                Cms.CmsWeb.webservices.ClsWebServiceCommon obj = new Cms.CmsWeb.webservices.ClsWebServiceCommon();
                string result = "";
                result = obj.FillSubModule(ModuleId);
                DataSet ds = new DataSet();
                ds.ReadXml(new System.IO.StringReader(result));
                return ds;
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                return null;
            }
        }   //fill SubModule from database onchange Modules



    }



}
