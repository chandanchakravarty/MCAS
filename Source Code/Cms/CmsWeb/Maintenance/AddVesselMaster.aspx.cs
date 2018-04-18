/******************************************************************************************
<Author				: -   Mohit Gupta
<Start Date			: -	8/3/2005 2:18:09 PM 	
Modification History
<Modified Date			: - 22-Apr-2010
<Modified By			: - Charles Gomes
<Purpose				: - Added Multilingual Support for Activate/Deactivate Button
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
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb.WebControls;
using Cms.Model.Maintenance;
using Cms.CmsWeb.Controls;

namespace Cms.CmsWeb.Maintenance
{
    public partial class AddVesselMaster : Cms.CmsWeb.cmsbase
    {
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.WebControls.Label capLOOKUP_VALUE_CODE;
        protected System.Web.UI.WebControls.TextBox txtLOOKUP_VALUE_CODE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOOKUP_VALUE_CODE;
        protected System.Web.UI.WebControls.Label capLOOKUP_VALUE_DESC;
        protected System.Web.UI.WebControls.TextBox txtLOOKUP_VALUE_DESC;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOOKUP_VALUE_DESC;
        protected Cms.CmsWeb.Controls.CmsButton btnReset;
        protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
        protected System.Web.UI.HtmlControls.HtmlImage imgSelect;
        protected System.Web.UI.WebControls.Label capMessages;
        //START:*********** Local form variables *************
        string oldXML;
        //creating resource manager object (used for reading field and label mapping)
        System.Resources.ResourceManager objResourceMgr;
        private string strRowId, strFormSaved;
        //private int	intLoggedInUserID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidLookUp_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOOKUPUNIQUE_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOOKUP_UNIQUE_ID;
        protected System.Web.UI.WebControls.Label capLOOKUP_DESCRIPTION;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOOKUP_ID;
        protected System.Web.UI.WebControls.Label lblCategoryCode;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidLookUp_Name;
        protected System.Web.UI.WebControls.Label capLOOKUP_NAME;
        protected System.Web.UI.WebControls.Label lblLooUpDesc;
        protected System.Web.UI.WebControls.Label lblLooKUpDesc;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidLookUp_Desc;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidLookUpValue_Desc;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledForEnter;
        ClsVesselMaster ObjLookUp;

        private void SetErrorMessages()
        {
        }
        protected void Page_Load(object sender, EventArgs e)
        {

           
            base.ScreenId = "569_0";
            lblMessage.Visible = false;
            SetErrorMessages();
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");

            SetCultureThread(GetLanguageCode());
            objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddVesselMaster", System.Reflection.Assembly.GetExecutingAssembly());
            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnActivateDeactivate.CmsButtonClass = CmsButtonType.Write;
            btnActivateDeactivate.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            btnReset.Attributes.Add("onclick", "javascript:return ResetForm();");

            if (!Page.IsPostBack)
            {
                SetHiddenFields();
                GetOldDataXML();
                FillCombo();
                if (hidOldData.Value != "")
                {
                    DataTable dt;
                    dt = ClsVesselMaster.GetVesselMasterDataForVessel(int.Parse(hidLookUp_ID.Value));
                }
            }

            btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(hidIS_ACTIVE.Value.Trim());
        }

        #region GetFormValue
        /// <summary>
        /// Fetch form's value and stores into model class object and return that object.
        /// </summary>
        private Cms.Model.Maintenance.ClsVesselMasterInfo GetFormValue()
        {
            ClsVesselMasterInfo ObjLookUpInfo;
            ObjLookUpInfo = new ClsVesselMasterInfo();

            ObjLookUpInfo.VESSEL_NAME = txtVESSEL_NAME.Text;
            ObjLookUpInfo.GRT = txtGRT.Text;
            ObjLookUpInfo.IMO = txtIMO.Text;
            ObjLookUpInfo.NRT = txtNRT.Text;
            ObjLookUpInfo.FLAG = txtFLAG.Text;
            ObjLookUpInfo.CLASSIFICATION = txtClassification.Text;
            ObjLookUpInfo.YEAR_BUILT = int.Parse(cmbYear_Built.SelectedValue);
            ObjLookUpInfo.LINER = int.Parse(cmbLiner.SelectedValue);
            ObjLookUpInfo.TYPE_OF_VESSEL = int.Parse(cmbType_of_Vessel.SelectedValue);
            ObjLookUpInfo.CLASS_TYPE = int.Parse(cmbClass_Type.SelectedValue);
            ObjLookUpInfo.CLASS = txtClass.Text;

            strFormSaved = hidFormSaved.Value;
            strRowId = hidLOOKUP_UNIQUE_ID.Value;
            oldXML = hidOldData.Value;
            //Returning the model object

            strFormSaved = hidFormSaved.Value;
            if (hidOldData.Value != "")
            {
                strRowId = hidLOOKUP_UNIQUE_ID.Value;
                ObjLookUpInfo.VESSEL_ID = int.Parse(hidLOOKUP_UNIQUE_ID.Value);
            }
            else
            {
                strRowId = "New";
                 ObjLookUpInfo.VESSEL_ID = int.Parse(hidLookUp_ID.Value);

            }
            oldXML = hidOldData.Value;
            return ObjLookUpInfo;
        }
        #endregion

        private void FillCombo()
        {

            int currYear = DateTime.Now.Year;
            //cmbYEAR_OF_REG.Items.Add("-Select-");
            for (int i = currYear; i > 1940 - 1; i--)
            {
                cmbYear_Built.Items.Add(i.ToString());
            }
            cmbYear_Built.Items.Insert(0, "");
            cmbYear_Built.SelectedIndex = -1;

            DataTable dt = ClsVesselMaster.GetLookupTableDataForVessel("TypeOfVessel");
            cmbType_of_Vessel.DataSource = dt.DefaultView;
            cmbType_of_Vessel.DataTextField = "LOOKUP_VALUE_DESC";
            cmbType_of_Vessel.DataValueField = "LOOKUP_UNIQUE_ID";
            cmbType_of_Vessel.DataBind();
            cmbType_of_Vessel.Items.Insert(0, "");

            DataTable dt1 = ClsVesselMaster.GetLookupTableDataForVessel("ClassUnClass");
            cmbClass_Type.DataSource = dt1.DefaultView;
            cmbClass_Type.DataTextField = "LOOKUP_VALUE_DESC";
            cmbClass_Type.DataValueField = "LOOKUP_UNIQUE_ID";
            cmbClass_Type.DataBind();
            cmbClass_Type.Items.Insert(0, "");

            DataTable dt2 = ClsVesselMaster.GetLookupTableDataForVessel("YesNo");
            cmbLiner.DataSource = dt2.DefaultView;
            cmbLiner.DataTextField = "LOOKUP_VALUE_DESC";
            cmbLiner.DataValueField = "LOOKUP_UNIQUE_ID";
            cmbLiner.DataBind();
            cmbLiner.Items.Insert(0, "");


        }

        private void SetHiddenFields()
        {
            if (Request.QueryString["VESSEL_ID"] != null && Request.QueryString["VESSEL_ID"].ToString() != "")
            {
                hidLOOKUP_UNIQUE_ID.Value = Request.QueryString["VESSEL_ID"].ToString();
            }
         
        }

        private void GetOldDataXML()
        {
            hidOldData.Value = ClsVesselMaster.GetLookUpDetailXml(int.Parse(hidLOOKUP_UNIQUE_ID.Value));
            if (hidOldData.Value != "" && hidOldData.Value != null)
            {
                hidIS_ACTIVE.Value = ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value);
            }
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
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
                ObjLookUp = new ClsVesselMaster();

                //Retreiving the form values into model class object
                ClsVesselMasterInfo ObjLookUpInfo = GetFormValue();
                ObjLookUpInfo.IS_ACTIVE = "Y";

                if (strRowId.ToUpper().Equals("NEW")) //save case
                {
                    int CreatedBy = int.Parse(GetUserId());
                    intRetVal = ObjLookUp.Add(ObjLookUpInfo, CreatedBy);

                    if (intRetVal > 0)
                    {
                        hidLOOKUP_UNIQUE_ID.Value = ObjLookUpInfo.VESSEL_ID.ToString();
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "29");
                        hidFormSaved.Value = "1";
                        hidIS_ACTIVE.Value = "Y";
                        hidOldData.Value = ClsVesselMaster.GetLookUpDetailXml(int.Parse(hidLOOKUP_UNIQUE_ID.Value));


                    }
                    else if (intRetVal == -1)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "18");
                        hidFormSaved.Value = "2";
                        btnActivateDeactivate.Enabled = false;
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
                    ClsVesselMasterInfo objOldLookUpInfo;
                    objOldLookUpInfo = new ClsVesselMasterInfo();

                    //Setting  the Old Page details(XML File containing old details) into the Model Object
                    base.PopulateModelObject(objOldLookUpInfo, hidOldData.Value);

                    //Setting those values into the Model object which are not in the page
                    //ObjLookUpInfo.LOOKUP_UNIQUE_ID = strRowId;
                    ObjLookUpInfo.MODIFIED_BY = int.Parse(GetUserId());
                    ObjLookUpInfo.LAST_UPDATED_DATETIME = DateTime.Now;
                    ObjLookUpInfo.IS_ACTIVE = hidIS_ACTIVE.Value;

                    //Updating the record using business layer class object
                    intRetVal = ObjLookUp.Update(objOldLookUpInfo, ObjLookUpInfo);
                    if (intRetVal > 0)			// update successfully performed
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "31");
                        hidFormSaved.Value = "1";
                        hidOldData.Value = ClsVesselMaster.GetLookUpDetailXml(int.Parse(hidLOOKUP_UNIQUE_ID.Value));
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
                if (ObjLookUp != null)
                    ObjLookUp.Dispose();

                btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(hidIS_ACTIVE.Value);
            }
        }

        private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
        {
            ClsVesselMaster objLookup = new ClsVesselMaster();
            try
            {
                Cms.BusinessLayer.BlCommon.stuTransactionInfo objStuTransactionInfo = new Cms.BusinessLayer.BlCommon.stuTransactionInfo();
                objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
                objStuTransactionInfo.loggedInUserName = GetUserName();

                Model.Maintenance.ClsVesselMasterInfo objLookupInfo;
                objLookupInfo = GetFormValue();
                string strCustomInfo = "";

                if (hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
                {
                    objStuTransactionInfo.transactionDescription = "Vessel Master Deactivated Successfully.";
                    objLookup.TransactionInfoParams = objStuTransactionInfo;
                    objLookup.ActivateDeactivate(hidLOOKUP_UNIQUE_ID.Value, "N");
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "41");
                    hidIS_ACTIVE.Value = "N";
                }
                else
                {
                    objStuTransactionInfo.transactionDescription = "Vessel Master Activated Successfully.";
                    objLookup.TransactionInfoParams = objStuTransactionInfo;
                    objLookup.ActivateDeactivate(hidLOOKUP_UNIQUE_ID.Value, "Y");
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "40");
                    hidIS_ACTIVE.Value = "Y";
                }
                hidFormSaved.Value = "1";
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
                //				if(objUser!= null)
                //					objUser.Dispose();

                btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(hidIS_ACTIVE.Value);
            }
        }


        #endregion
    }
}