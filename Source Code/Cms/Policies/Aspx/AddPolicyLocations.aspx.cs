using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cms.CmsWeb;
using System.Data;
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb.Controls;
using Cms.Model.Policy;
using System.Web.UI.HtmlControls;
using Cms.BusinessLayer.BlApplication;
using Cms.ExceptionPublisher.ExceptionManagement;
using System.Reflection;
using System.Resources;

namespace Cms.Policies.Aspx
{
    public partial class AddPolicyLocations : Cms.Policies.policiesbase
    {
        #region Variables
        protected DropDownList cmbLOC_COUNTRY;
        protected DropDownList cmbLOC_STATE;
        protected DropDownList cmbACTIVITY_TYPE;
        protected DropDownList cmbOCCUPIED;
        protected DropDownList cmbCONSTRUCTION;
        protected DropDownList cmbLOCATION;
     //   protected TextBox txtCAL_NUM;
        protected TextBox txtLOC_NUM;
        protected TextBox txtNAME;
        protected TextBox txtLOC_ZIP;
        protected TextBox txtLOC_ADD1;
        protected TextBox txtNUMBER;
        protected TextBox txtLOC_ADD2;
        protected TextBox txtDISTRICT;
        protected TextBox txtLOC_CITY;
        protected TextBox txtPHONE_NUMBER;
        protected TextBox txtEXT;
        protected TextBox txtFAX_NUMBER;
        protected TextBox txtCATEGORY;
        protected TextBox txtDESCRIPTION;
        protected Label lblDelete;
        protected Label lblMessage;
        protected Label capLOC_NUM;
        protected Label capNAME;
       // protected Label capCAL_NUM;
        protected Label capLOC_ZIP;
        protected Label capLOC_ADD1;
        protected Label capNUMBER;
        protected Label capLOC_ADD2;
        protected Label capDISTRICT;
        protected Label capLOC_CITY;
        protected Label capLOC_COUNTRY;
        protected Label capLOC_STATE;
        protected Label capOCCUPIED;
        protected Label capPHONE_NUMBER;
        protected Label capEXT;
        protected Label capFAX_NUMBER;
        protected Label capCATEGORY;
        protected Label capACTIVITY_TYPE;
        protected Label capCONSTRUCTION;
        protected Label capMAN_MSG;
        protected Label capLOCATION;
        protected Label capDESCRIPTION;
        protected Label capIsBillingAddress;
        protected HtmlInputHidden hidValidateLoc;//added for tfs# 2701 by pradeep
        protected CmsButton btnReset;
        protected CmsButton btnActivateDeactivate;
        protected CmsButton btnSave;
        protected CmsButton btnDelete;
        protected CmsButton btnSelect;
        protected CmsButton btnPullCustomerAddress;//Added for itrack 921
        protected Label capPullCustomerAddress;//Added for itrack 921
        protected System.Web.UI.WebControls.Image imgZipLookup;
       // int COUNTRY_ID;
        protected RegularExpressionValidator revLOC_ZIP;
        protected RegularExpressionValidator revEXT;
        protected RegularExpressionValidator revFAX_NUMBER;
        protected RegularExpressionValidator revPHONE_NUMBER;
        protected RegularExpressionValidator revLOC_NUM;
     //   protected RegularExpressionValidator revCAL_NUM;
     //   protected RegularExpressionValidator revNUMBER;
      //  protected RequiredFieldValidator rfvtxtCAL_NUM;
        protected RequiredFieldValidator rfvtxtLOC_NUM;
        protected RequiredFieldValidator rfvNAME;
        protected RequiredFieldValidator rfvLOC_ZIP;
        protected RequiredFieldValidator rfvLOC_ADD1;
        protected RequiredFieldValidator rfvNUMBER;
        protected RequiredFieldValidator rfvDISTRICT;
        protected RequiredFieldValidator rfvLOC_CITY;
       // protected RequiredFieldValidator rfvACTIVITY_TYPE;
        protected RequiredFieldValidator rfvLOC_COUNTRY;
        protected RequiredFieldValidator rfvLOC_STATE;
        //protected RequiredFieldValidator rfvOCCUPIED;
        protected CustomValidator csvLOC_NUM;
        protected HtmlInputHidden hidLOCATION_ID;
        protected HtmlInputHidden hidPOL_ID;
        protected HtmlInputHidden hidPOL_VERSION_ID;
        protected HtmlInputHidden hidCUSTOMER_ID;
        protected HtmlInputHidden hidFormSaved;
        protected HtmlInputHidden hidIS_ACTIVE;
        protected HtmlInputHidden hidOldData;
        protected HtmlInputHidden hidNew;
        protected HtmlInputHidden hidTabTitle;
        protected HtmlInputHidden hidLOC_STATE;
        protected HtmlInputHidden hidZipeCodeVerificationMsg;
        protected HtmlInputHidden hidZipCodeParam;
        protected HtmlInputHidden hidLocationNumberMsg;
        protected HtmlInputHidden hidOLD_Location_num;
        protected HtmlInputHidden hidOld_Loc_Msg;
        protected HtmlInputHidden hidIS_BILLING;
        protected HtmlInputHidden hidLOCATION;
        protected HtmlInputHidden hidLocation_Value;
        //Added for itrack-1152/TFS # 2598
        protected HtmlInputHidden hidACTIVITY_TYPE;
        protected HtmlInputHidden hidOCCUPIED;
        protected HtmlInputHidden hidRUBRICA;  
        //Added till here 
        protected System.Web.UI.WebControls.CheckBox chkIsBillingAddress;
        protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
        System.Resources.ResourceManager objResourceMgr;
        protected System.Web.UI.WebControls.HyperLink hlkZipLookup;
        ClsLocation objLocation;
        private string strRowId, strFormSaved;
        public string primaryKeyValues = "";
        string oldXML;
        string ISACTIVE;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

           
            base.ScreenId = "224_0";
            Ajax.Utility.RegisterTypeForAjax(typeof(AddPolicyLocations));
            ClsLocation objLocation = new ClsLocation();
            GetQueryString();
            ClsMessages.SetCustomizedXml(GetLanguageCode());
            btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1333");            
            //START:*********** Setting permissions and class (Read/write/execute/delete)  *************
            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnActivateDeactivate.CmsButtonClass = CmsButtonType.Write;
            btnActivateDeactivate.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            btnDelete.CmsButtonClass = CmsButtonType.Delete;
            btnDelete.PermissionString = gstrSecurityXML;

            btnSelect.CmsButtonClass = CmsButtonType.Write;
            btnSelect.PermissionString = gstrSecurityXML;
            //Added for itrack 921
            btnPullCustomerAddress.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnPullCustomerAddress.PermissionString = gstrSecurityXML;
            //Added till here

            btnSelect.Text = Cms.CmsWeb.ClsMessages.FetchGeneralButtonsText("btnSelect");
            btnSave.Text = Cms.CmsWeb.ClsMessages.FetchGeneralButtonsText("btnSave");
            btnDelete.Text = Cms.CmsWeb.ClsMessages.FetchGeneralButtonsText("btnDelete");

            hidTabTitle.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "224_13");
            objResourceMgr = new ResourceManager("Cms.Policies.Aspx.AddPolicyLocations", Assembly.GetExecutingAssembly());
            if (!Page.IsPostBack)
            {

                SetErrorMessage();
                imgZipLookup.Attributes.Add("style", "cursor:hand");
               
                base.VerifyAddressDetailsBR(hlkZipLookup, txtLOC_ADD1,txtDISTRICT, txtLOC_CITY, cmbLOC_STATE, txtLOC_ZIP);


                if (Request.Params["LOCATION_ID"] == null)
                {
                    hidLOCATION_ID.Value = "NEW";
                }
                string url = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL();
                this.FillDropdowns();
                //Added by Pradeep Kushwaha- for itrack 921
                base.RequiredPullCustAdd(txtLOC_ADD1, txtLOC_ADD2, txtLOC_CITY, cmbLOC_COUNTRY, cmbLOC_STATE, txtLOC_ZIP
              , btnPullCustomerAddress, txtNUMBER, txtDISTRICT);
                //Till here 
                PopulateActivityTypeDropDown();
                //PopulateOccupied();
                PopulateConstruction();
                PopulateLOCATION();
                btnReset.Attributes.Add("onclick", "javascript:ResetForm();return false;");
                if (ClsCommon.IsXMLResourceExists(@Request.PhysicalApplicationPath + "Policies/support/PageXML/" + GetSystemId(), "AddPolicyLocations.xml"))
                {
                    setPageControls(Page, @Request.PhysicalApplicationPath + "Policies/support/PageXML/" + GetSystemId() + "/AddPolicyLocations.xml");
                }
                
                GetOldDataXML();
               
                if (hidLOCATION_ID.Value == "NEW")
                {
                    btnDelete.Enabled = false;
                    btnActivateDeactivate.Enabled = false;
                    //Added  by pradeep Kushwaha on 01-06-2010
                    this.FillLocationNumber();//
                    
                }
                else
                {
                    ISACTIVE = ClsCommon.FetchValueFromXML("IS_ACTIVE", oldXML);
                    btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchActivateDeactivateButtonsText(ISACTIVE);
                }
                SetCaptions();
                strRowId = hidLOCATION_ID.Value;
            }
         
        }

        #region Methods

        private void FillDropdowns()
        {

            DataTable dt = Cms.CmsWeb.ClsFetcher.AllCountry;

            cmbLOC_COUNTRY.DataSource = dt;
            cmbLOC_COUNTRY.DataTextField = COUNTRY_NAME;
            cmbLOC_COUNTRY.DataValueField = COUNTRY_ID;
            cmbLOC_COUNTRY.DataBind();
            cmbLOC_COUNTRY.SelectedValue = Convert.ToString(5);

            
            dt = Cms.CmsWeb.ClsFetcher.State;
            cmbLOC_STATE.DataSource = dt;
            cmbLOC_STATE.DataTextField = STATE_NAME;
            cmbLOC_STATE.DataValueField = STATE_ID;
            cmbLOC_STATE.DataBind();
            cmbLOC_STATE.Items.Insert(0, "");
            cmbLOC_COUNTRY.SelectedIndex = cmbLOC_COUNTRY.Items.IndexOf(cmbLOC_COUNTRY.Items.FindByValue("5"));
            PopulateStateDropDown(cmbLOC_STATE, int.Parse(cmbLOC_COUNTRY.SelectedValue));
           

           }

        private void PopulateStateDropDown(DropDownList cmbLOC_STATE, int COUNTRY_ID)
        {
            ClsStates objStates = new ClsStates();


            if (COUNTRY_ID == 0)
                return;

            cmbLOC_STATE.Items.Clear();

            DataTable dtStates = objStates.GetStatesForCountry(COUNTRY_ID);
            if (dtStates != null && dtStates.Rows.Count > 0)
            {
                cmbLOC_STATE.DataSource = dtStates;
                cmbLOC_STATE.DataTextField = STATE_NAME;
                cmbLOC_STATE.DataValueField = STATE_ID;
                cmbLOC_STATE.DataBind();
                cmbLOC_STATE.Items.Insert(0, "");
            }
        }
        			

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
        public DataSet AjaxFillState(string CountryID)
        {
            try
            {
                CmsWeb.webservices.ClsWebServiceCommon obj = new CmsWeb.webservices.ClsWebServiceCommon();
                string result = "";
                result = obj.FillState(CountryID);
                DataSet ds = new DataSet();
                ds.ReadXml(new System.IO.StringReader(result));
                return ds;
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                return null;
            }
        }
 

        //private void PopulateActivityTypeDropDown()
        //{
        //    cmbACTIVITY_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("ACTTE");
        //    cmbACTIVITY_TYPE.DataTextField = "LookupDesc";
        //    cmbACTIVITY_TYPE.DataValueField = "LookupID";
        //    cmbACTIVITY_TYPE.DataBind();
        //    cmbACTIVITY_TYPE.Items.Insert(0, "");
        //    cmbACTIVITY_TYPE.SelectedIndex = 0;
        //}

        //private void PopulateOccupied()
        //{
        //    cmbOCCUPIED.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("OCDAS");
        //    cmbOCCUPIED.DataTextField = "LookupDesc";
        //    cmbOCCUPIED.DataValueField = "LookupID";
        //    cmbOCCUPIED.DataBind();
        //    cmbOCCUPIED.Items.Insert(0, "");
        //    cmbOCCUPIED.SelectedIndex = 0;
        //}

       
        private void PopulateOccupied(string _rubrica, int OCCUPIED_ID)
        {
            //Modified for itrack#1152 / TFS# 2598
            DataSet ds = Cms.BusinessLayer.BlApplication.ClsLocation.SELECTOCCUPIEDAS(_rubrica, OCCUPIED_ID, "RUBRICA");

            if(ds!=null && ds.Tables[0].Rows.Count>0)
                hidRUBRICA.Value = ds.Tables[0].Rows[0]["RUBRICA"].ToString();
            cmbOCCUPIED.DataSource = ds;
            cmbOCCUPIED.DataTextField ="OCCUPIED_AS";
            cmbOCCUPIED.DataValueField ="OCCUPIED_ID";
            cmbOCCUPIED.DataBind();
            cmbOCCUPIED.Items.Insert(0, "");
            cmbOCCUPIED.SelectedIndex = 0;
        }
        
        private void PopulateActivityTypeDropDown()
        {
            //changes by praveer  for itrack no 885
            DataTable dt = Cms.BusinessLayer.BlApplication.ClsLocation.SELECTACTIVITYTYPE();
            DataView dv = new DataView(dt, "TYPE=11109 ", "ACTIVITY_DESC", DataViewRowState.CurrentRows);
            dv.Sort = "ACTIVITY_DESC";
            cmbACTIVITY_TYPE.DataSource = dv;
            cmbACTIVITY_TYPE.DataTextField ="ACTIVITY_DESC";
            cmbACTIVITY_TYPE.DataValueField = "ACTIVITY_ID_RUBRICA";//Modified for itrack#1152 / TFS# 2598
            cmbACTIVITY_TYPE.DataBind();
            cmbACTIVITY_TYPE.Items.Insert(0, "");
            cmbACTIVITY_TYPE.SelectedIndex = 0;
        }
        //Added for itrack#1152 / TFS# 2598
        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public DataSet AjaxGetOccupied(string _rubrica)
        {
            try
            {
                DataSet ds = Cms.BusinessLayer.BlApplication.ClsLocation.SELECTOCCUPIEDAS(_rubrica, 0, "");
                return ds;
            }
            catch
            {
                return null;
            }
        }
        private void SetErrorMessage()
        {
            revLOC_ZIP.ValidationExpression = aRegExpZipBrazil;
            revLOC_ZIP.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1084");
            revEXT.ValidationExpression = aRegExpExtn;
            revEXT.ErrorMessage = ClsMessages.FetchGeneralMessage("25");
            //revFAX_NUMBER.ValidationExpression = aRegExpPhoneBrazil;
            //revFAX_NUMBER.ErrorMessage = ClsMessages.FetchGeneralMessage("1085");
            //revPHONE_NUMBER.ValidationExpression = aRegExpPhoneBrazil;
            //revPHONE_NUMBER.ErrorMessage = ClsMessages.FetchGeneralMessage("1083");
            revPHONE_NUMBER.ValidationExpression = aRegExpAgencyPhone;
            revFAX_NUMBER.ValidationExpression = aRegExpAgencyPhone;
            if (GetLanguageID() == "1")
            {
                revPHONE_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1864");
                revFAX_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1085");
                
            }

            else
            {
                revPHONE_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1913");
                revFAX_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1921");
                
            }

            revLOC_NUM.ValidationExpression = aRegExpInteger;
            revLOC_NUM.ErrorMessage = ClsMessages.FetchGeneralMessage("163");
          //  revCAL_NUM.ValidationExpression = aRegExpInteger;
         //   revCAL_NUM.ErrorMessage = ClsMessages.FetchGeneralMessage("216");
           // revNUMBER.ValidationExpression = aRegExpInteger;
           // revNUMBER.ErrorMessage = ClsMessages.FetchGeneralMessage("163");
         //   rfvtxtCAL_NUM.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "224_1");;
            rfvtxtLOC_NUM.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "224_2");
            rfvNAME.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "224_3");
            rfvLOC_ZIP.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "224_4");
            rfvLOC_ADD1.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "224_5");
            //rfvNUMBER.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "224_6"); Commented by Aditya for TFS BUG # 1911
            rfvDISTRICT.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "224_7");
            rfvLOC_CITY.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "224_8");
            rfvLOC_COUNTRY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "224_9");
            rfvLOC_STATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "224_10");
          //  rfvOCCUPIED.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "224_11");
           // rfvACTIVITY_TYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "224_12");
            hidLocationNumberMsg.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "224_14");
            hidOld_Loc_Msg.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "224_2");
            
        }

        private void GetQueryString()
        {
            hidCUSTOMER_ID.Value = Request.Params["CUSTOMER_ID"];
            if(hidCUSTOMER_ID.Value == "")
                hidCUSTOMER_ID.Value = GetCustomerID();

            hidPOL_ID.Value = Request.Params["POL_ID"];
            if (hidPOL_ID.Value == "")
                hidPOL_ID.Value = GetPolicyID();

            hidPOL_VERSION_ID.Value = Request.Params["POL_VERSION_ID"];
            if (hidPOL_VERSION_ID.Value == "")
                hidPOL_VERSION_ID.Value = GetPolicyVersionID();

            if (Request.Params["LOCATION_ID"] != null)
            {
                hidLOCATION_ID.Value = Request.Params["LOCATION_ID"];
            }

        }

        private ClsPolicyLocationInfo GetFormValue()
        {
            //Creating the Model object for holding the New data
            ClsPolicyLocationInfo objLocationInfo = new ClsPolicyLocationInfo();

            objLocationInfo.POLICY_ID = int.Parse(hidPOL_ID.Value);
            objLocationInfo.CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);
            objLocationInfo.POLICY_VERSION_ID = int.Parse(hidPOL_VERSION_ID.Value);
            if (cmbLOCATION.SelectedIndex != 0)
            { objLocationInfo.SOURCE_LOCATION_ID = Int64.Parse(cmbLOCATION.SelectedValue); }  //Modified int to Int64 as per the tfs#2701 itrack-1366
            
            //if (txtCAL_NUM.Text.Trim() != "")
            //    objLocationInfo.CAL_NUM = txtCAL_NUM.Text;

            if (txtLOC_NUM.Text.Trim() != "")
                objLocationInfo.LOCATION_ID = Int64.Parse(txtLOC_NUM.Text);  //Modified int to Int64 as per the tfs#2701 itrack-1366

            if (txtLOC_NUM.Text.Trim() != "")
                objLocationInfo.LOC_NUM = Int64.Parse(txtLOC_NUM.Text);  //Modified int to Int64 as per the tfs#2701 itrack-1366

            if (txtNAME.Text.Trim() != "")
                objLocationInfo.NAME = txtNAME.Text;

            if (txtLOC_ZIP.Text.Trim() != "")
                objLocationInfo.LOC_ZIP = txtLOC_ZIP.Text;

            if (txtLOC_ADD1.Text.Trim() != "")
                objLocationInfo.LOC_ADD1 = txtLOC_ADD1.Text;

            if (txtNUMBER.Text.Trim() != "")
                objLocationInfo.NUMBER = txtNUMBER.Text;

            if (txtLOC_ADD2.Text.Trim() != "")
                objLocationInfo.LOC_ADD2 = txtLOC_ADD2.Text;

            if (txtDISTRICT.Text.Trim() != "")
                objLocationInfo.DISTRICT = txtDISTRICT.Text;

            if (txtLOC_CITY.Text.Trim() != "")
                objLocationInfo.LOC_CITY = txtLOC_CITY.Text;

            if (txtPHONE_NUMBER.Text.Trim() != "")
                objLocationInfo.PHONE_NUMBER = txtPHONE_NUMBER.Text;

            if (txtEXT.Text.Trim() != "")
                objLocationInfo.EXT= txtEXT.Text;

            if (txtFAX_NUMBER.Text.Trim() != "")
                objLocationInfo.FAX_NUMBER = txtFAX_NUMBER.Text;

            if (txtCATEGORY.Text.Trim() != "")
                objLocationInfo.CATEGORY = txtCATEGORY.Text;

            if (txtDESCRIPTION.Text.Trim() != "")
                objLocationInfo.DESCRIPTION = txtDESCRIPTION.Text;

            objLocationInfo.LOC_STATE = cmbLOC_STATE.SelectedValue;
            
            if (cmbLOC_COUNTRY.SelectedItem != null)
                objLocationInfo.LOC_COUNTRY = cmbLOC_COUNTRY.SelectedItem.Value;
            //modified for itrack-1152/TFS # 2598
            if (cmbACTIVITY_TYPE.SelectedIndex != 0 && cmbACTIVITY_TYPE.SelectedIndex != -1)
                objLocationInfo.ACTIVITY_TYPE = Convert.ToInt32(hidACTIVITY_TYPE.Value);
            //modified for itrack-1152/TFS # 2598
            if (hidOCCUPIED.Value!="")
                objLocationInfo.OCCUPIED = Convert.ToInt32(hidOCCUPIED.Value);

            if (cmbCONSTRUCTION.SelectedIndex != 0)
                objLocationInfo.CONSTRUCTION = Convert.ToInt32(cmbCONSTRUCTION.SelectedItem.Value);
            if (chkIsBillingAddress.Checked == true)
            {
                objLocationInfo.IS_BILLING = "Y";
            }
            else {
                objLocationInfo.IS_BILLING = "N";
            }
           

            ////These  assignments are common to all pages.
            strFormSaved = hidFormSaved.Value;
            strRowId = hidLOCATION_ID.Value;
            //oldXML = hidOldData.Value;
            ////Returning the model object

            return objLocationInfo;
        }

        private void GetOldDataXML()
        {
            if (hidLOCATION_ID.Value != "NEW")
            {
                oldXML = ClsLocation.GetPolicyLocationInfo(int.Parse(hidCUSTOMER_ID.Value)
                    , int.Parse(hidPOL_ID.Value)
                    , int.Parse(hidPOL_VERSION_ID.Value)
                    , int.Parse(hidLOCATION_ID.Value));
                primaryKeyValues = hidLOCATION_ID.Value + "^" + hidCUSTOMER_ID.Value + "^" + hidPOL_ID.Value + "^" + hidPOL_VERSION_ID.Value;
                hidOldData.Value = oldXML;
                hidIS_ACTIVE.Value = ClsCommon.FetchValueFromXML("IS_ACTIVE",oldXML);
                hidLOCATION_ID.Value = ClsCommon.FetchValueFromXML("LOCATION_ID", oldXML);
                hidLOC_STATE.Value = ClsCommon.FetchValueFromXML("LOC_STATE", oldXML);
                hidOLD_Location_num.Value = ClsCommon.FetchValueFromXML("LOC_NUM", oldXML);//Added By pradeep Kushwaha on 05-July-2010
                hidIS_BILLING.Value = ClsCommon.FetchValueFromXML("IS_BILLING", oldXML);  
               
                // for itrack 658
                cmbLOCATION.SelectedIndex = cmbLOCATION.Items.IndexOf(cmbLOCATION.Items.FindByValue(ClsCommon.FetchValueFromXML("LOC_NUM", oldXML)));
               
                if (hidIS_BILLING.Value =="Y")
                {
                    chkIsBillingAddress.Checked = true;
                }

                base.PopulatePageFromModelObject(this.Page, oldXML);

                
                //Added for itrack#1152/TFS# 2598
                int OCCUPIED = 0;
                hidOCCUPIED.Value=ClsCommon.FetchValueFromXML("OCCUPIED", oldXML);
                hidACTIVITY_TYPE.Value = ClsCommon.FetchValueFromXML("ACTIVITY_TYPE", oldXML);

                if(int.TryParse(ClsCommon.FetchValueFromXML("OCCUPIED", oldXML),out OCCUPIED))
                    this.PopulateOccupied("", OCCUPIED);
               
                string _activity_type = ClsCommon.FetchValueFromXML("ACTIVITY_TYPE", oldXML);
                if (_activity_type != "")
                    cmbACTIVITY_TYPE.SelectedIndex = cmbACTIVITY_TYPE.Items.IndexOf(cmbACTIVITY_TYPE.Items.FindByValue(_activity_type + "^" + hidRUBRICA.Value));
                //Added till here 

            }
        }

        private void SetCaptions()
        {
            capLOC_NUM.Text = objResourceMgr.GetString("txtLOC_NUM");
            capNAME.Text = objResourceMgr.GetString("txtNAME");
          //  capCAL_NUM.Text = objResourceMgr.GetString("txtCAL_NUM");
            if (GetSystemId().ToString().ToUpper() == "S001" || GetSystemId().ToString().ToUpper() == "SUAT")
                capLOC_ZIP.Text = "Postal Code";
            else
            capLOC_ZIP.Text = objResourceMgr.GetString("txtLOC_ZIP");
            capLOC_ADD1.Text = objResourceMgr.GetString("txtLOC_ADD1");
            capNUMBER.Text = objResourceMgr.GetString("txtNUMBER");
            capLOC_ADD2.Text = objResourceMgr.GetString("txtLOC_ADD2");
            capDISTRICT.Text = objResourceMgr.GetString("txtDISTRICT");
            capLOC_CITY.Text = objResourceMgr.GetString("txtLOC_CITY");
            capLOC_COUNTRY.Text = objResourceMgr.GetString("cmbLOC_COUNTRY");
            capLOC_STATE.Text = objResourceMgr.GetString("cmbLOC_STATE");
            capOCCUPIED.Text = objResourceMgr.GetString("cmbOCCUPIED");
            capPHONE_NUMBER.Text = objResourceMgr.GetString("txtPHONE_NUMBER");
            capEXT.Text = objResourceMgr.GetString("txtEXT");
            capFAX_NUMBER.Text = objResourceMgr.GetString("txtFAX_NUMBER");
            capCATEGORY.Text = objResourceMgr.GetString("txtCATEGORY");
            capACTIVITY_TYPE.Text = objResourceMgr.GetString("cmbACTIVITY_TYPE");
            capCONSTRUCTION.Text = objResourceMgr.GetString("cmbCONSTRUCTION");
            capMAN_MSG.Text = objResourceMgr.GetString("capMAN_MSG");
            capLOCATION.Text = objResourceMgr.GetString("cmbLOCATION");
            capDESCRIPTION.Text = objResourceMgr.GetString("txtDESCRIPTION");
            capIsBillingAddress.Text = objResourceMgr.GetString("capIsBillingAddress");
            hidZipeCodeVerificationMsg.Value = objResourceMgr.GetString("hidZipeCodeVerificationMsg");
            //btnActivateDeactivate.Text = objResourceMgr.GetString("btnActivateDeactivate");
            capPullCustomerAddress.Text = objResourceMgr.GetString("capPullCustomerAddress");//Added for itrack 921
            btnPullCustomerAddress.Text = objResourceMgr.GetString("btnPullCustomerAddress");//Added for itrack 921
            
        }

        private void PopulateConstruction()
        {
            DataTable dtCONSTRUCTION = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("CONTYP");
            DataView dvCONSTRUCTION = dtCONSTRUCTION.DefaultView;
            dvCONSTRUCTION.Sort = "LookupDesc";

            cmbCONSTRUCTION.DataSource = dvCONSTRUCTION;// Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CONTYP");
            cmbCONSTRUCTION.DataTextField = "LookupDesc";
            cmbCONSTRUCTION.DataValueField = "LookupID";
            cmbCONSTRUCTION.DataBind();
            cmbCONSTRUCTION.Items.Insert(0, "");
            cmbCONSTRUCTION.SelectedIndex = 0;
        }//

        private void FetchLocatiobSourceId()
        {
            objLocation = new ClsLocation();
            DataSet DsSource = new DataSet();
            if (hidLOCATION_ID.Value != "NEW")
            {
                DsSource = objLocation.GetLocationSourceDetails(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidLOCATION_ID.Value));
                cmbLOCATION.SelectedValue = DsSource.Tables[0].Rows[0][0].ToString();
            }
        }

        private void PopulateLOCATION()
        {
            objLocation=new ClsLocation();
            cmbLOCATION.DataSource = objLocation.GetLocationInfoDetails(int.Parse(hidCUSTOMER_ID.Value));
            cmbLOCATION.DataTextField = "LOCATION_ADDRESS";
            cmbLOCATION.DataValueField = "LOC_NUM";
            cmbLOCATION.DataBind();
            cmbLOCATION.Items.Insert(0, "");
            FetchLocatiobSourceId();
        }
        #endregion


        #region Control Events
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
			{
				int intRetVal;	//For retreiving the return value of business class save function
				objLocation = new  ClsLocation();
                strRowId = hidLOCATION_ID.Value;
				//Retreiving the form values into model class object
                ClsPolicyLocationInfo objPLocationInfo = new ClsPolicyLocationInfo();
				objPLocationInfo = GetFormValue();

                if (strRowId.ToUpper().Equals("NEW") || hidNew.Value == "New") //save case
                {
					objPLocationInfo.CREATED_BY = int.Parse(GetUserId());
					objPLocationInfo.CREATED_DATETIME = DateTime.Now;

					//Calling the add method of business layer class
					intRetVal = objLocation.Add(objPLocationInfo);

                    if(intRetVal==3)
                    {//hidSTATE_ID_OLD
                        //objPLocationInfo = new ClsPolicyLocationInfo();
                        hidLOCATION_ID.Value = objPLocationInfo.LOCATION_ID.ToString();
                         
                        primaryKeyValues=hidLOCATION_ID.Value  + "^" + hidCUSTOMER_ID.Value + "^" + hidPOL_ID.Value + "^" + hidPOL_VERSION_ID.Value;  
                        lblMessage.Text	=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"29");
                        hidFormSaved.Value	=	"1";
                        hidIS_ACTIVE.Value = "Y";
                        GetOldDataXML();
                        
                        //Show Delete and Activate And Deactivate button
                        
                        btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchActivateDeactivateButtonsText("Y");
                        btnDelete.Enabled = true;
                        btnActivateDeactivate.Enabled = true;

                        base.OpenEndorsementDetails();
                    }

                    else if (intRetVal == 2)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "224_25_12");
                        hidFormSaved.Value = "2";
                    }
                    else if (intRetVal ==1)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "165");
                        hidFormSaved.Value = "2";
                    }
                    else
                    {
                        base.OpenEndorsementDetails();
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "20");
                        hidFormSaved.Value = "2";
                    }
                    lblMessage.Visible = true;
                    hidNew.Value = "";
				} // end save case
                else //UPDATE CASE
            //    //Creating the Model object for holding the Old data
                {
					ClsPolicyLocationInfo objOldLocationInfo;
					objOldLocationInfo = new ClsPolicyLocationInfo();

                    oldXML = ClsLocation.GetPolicyLocationInfo(int.Parse(hidCUSTOMER_ID.Value)
                    , int.Parse(hidPOL_ID.Value)
                    , int.Parse(hidPOL_VERSION_ID.Value)
                    , int.Parse(hidLOCATION_ID.Value));
					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldLocationInfo,oldXML);

					//Setting those values into the Model object which are not in the page
                    objPLocationInfo.LOCATION_ID = int.Parse(strRowId);
                    objPLocationInfo.MODIFIED_BY = int.Parse(GetUserId());
                    objPLocationInfo.LAST_UPDATED_DATETIME = DateTime.Now;
                    objPLocationInfo.IS_ACTIVE = hidIS_ACTIVE.Value;

					//Updating the record using business layer class object
                    intRetVal = objLocation.Update(objOldLocationInfo, objPLocationInfo);
					if( intRetVal==2 )			// update successfully performed
					{
						primaryKeyValues=hidLOCATION_ID.Value  + "^" + hidCUSTOMER_ID.Value + "^" + hidPOL_ID.Value + "^" + hidPOL_VERSION_ID.Value;  

						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						GetOldDataXML();
                        
                        btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchActivateDeactivateButtonsText("Y");
                        //SetWorkflow();

						//Showing the endorsement popup window
						base.OpenEndorsementDetails();
                       
					}

                    else if (intRetVal == 1)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "224_25_12");
                        hidFormSaved.Value = "2";
                    }
					else if(intRetVal == 0)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"165");
						hidFormSaved.Value		=	"2";
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value		=	"2";
					}
					lblMessage.Visible = true;
                    
                }
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
				//ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objLocation!= null)
					objLocation.Dispose();
			}
		}

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int intRetVal=0;

            objLocation = new ClsLocation();
            ClsPolicyLocationInfo objLocationInfo = GetFormValue();

            objLocationInfo.MODIFIED_BY = int.Parse(GetUserId());

            if (hidLOCATION_ID.Value != null && hidLOCATION_ID.Value != "")
               objLocationInfo.LOCATION_ID = int.Parse(hidLOCATION_ID.Value);

            intRetVal = objLocation.Delete(objLocationInfo);

            if (intRetVal > 0)
            {
                lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "127");
                lblMessage.Visible = true;
                hidFormSaved.Value = "5";
                hidOldData.Value = "";
                trBody.Attributes.Add("style", "display:none");
                lblMessage.Visible = true;
                hidLOCATION_ID.Value = "0";
            }
            else if (intRetVal == -1)
            {

                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "224_15");
                hidFormSaved.Value = "2";
                lblMessage.Visible = true;
            }
           
        }

        protected void btnActivateDeactivate_Click(object sender, EventArgs e)
        {
            int locationID = int.Parse(this.hidLOCATION_ID.Value);
            int polID = int.Parse(hidPOL_ID.Value);
            int polVersionID = int.Parse(hidPOL_VERSION_ID.Value);
            int customerID = int.Parse(hidCUSTOMER_ID.Value);
            int retVal = 0;
            int modifiedby = 0;

            try
            {
                Cms.BusinessLayer.BlCommon.stuTransactionInfo objStuTransactionInfo = new Cms.BusinessLayer.BlCommon.stuTransactionInfo();
                objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
                objStuTransactionInfo.loggedInUserName = GetUserName();
                objLocation = new ClsLocation();
                modifiedby = int.Parse(GetUserId());
                if (hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
                {
                    objStuTransactionInfo.transactionDescription = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("224_1");

                    objLocation.TransactionInfoParams = objStuTransactionInfo;


                    //objLocation.ActivateDeactivate(hidLOCATION_ID.Value, "N");

                    retVal = objLocation.ActivateDeactivatePolicy(customerID, polID, polVersionID, locationID, "N", modifiedby);

                    if (retVal == -1)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "224_16");
                        //lblMessage.Text = "This location cannot be deactivated as it is being used in Dwelling Information.";
                        return;
                    }
                    else if (retVal == -2)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "2");
                        //lblMessage.Text = "This location cannot be deactivated as it is being used in Subjects of insurance.";
                        return;
                    }
                    else if (retVal == 0)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, " 224_14");
                        //lblMessage.Text = "This location cannot be deactivated as it is being used in Subjects of insurance.";
                        return;
                    }
                   

                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "41");
                    hidIS_ACTIVE.Value = "N";
                    base.OpenEndorsementDetails();
                    btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchActivateDeactivateButtonsText("N");
                }
                else
                {
                    objStuTransactionInfo.transactionDescription = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("224_2");
                    objLocation.TransactionInfoParams = objStuTransactionInfo;


                    //objLocation.ActivateDeactivate(hidLOCATION_ID.Value, "Y");

                    retVal = objLocation.ActivateDeactivatePolicy(customerID, polID, polVersionID, locationID, "Y", modifiedby);
                    if (retVal > 0)
                        base.OpenEndorsementDetails();
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "40");
                    hidIS_ACTIVE.Value = "Y";
                    btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchActivateDeactivateButtonsText("Y");
                }
                hidFormSaved.Value = "0";
                //hidFormSaved.Value			=	"1";
                primaryKeyValues = hidLOCATION_ID.Value + "^" + hidCUSTOMER_ID.Value + "^" + hidPOL_ID.Value + "^" + hidPOL_VERSION_ID.Value;
                ClientScript.RegisterStartupScript(this.GetType(), "REFRESHGRID", "<script>RefreshWebGrid('1','" + primaryKeyValues + "',true);</script>");

            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";
            }
            finally
            {
                lblMessage.Visible = true;
                if (objLocation != null)
                 objLocation.Dispose();
            }
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            string CALLEDFROM = "POL_LOC";
           
            if (cmbLOCATION.SelectedValue != "")
            {
                //string ADDRESS = cmbLOCATION.SelectedItem.Text;
                string ADDRESS = hidLOCATION.Value;
                String strLocationValue = hidLocation_Value.Value;
                oldXML = ClsLocation.FetchLocationDetails(int.Parse(hidCUSTOMER_ID.Value), int.Parse(cmbLOCATION.SelectedValue), CALLEDFROM, ADDRESS);
                
                base.PopulatePageFromModelObject(this.Page, oldXML);
             //   FillLocationNumber();
                hidNew.Value = "New";
                hidValidateLoc.Value = "0";//added for tfs#2701 by pradeep
            }
        }
        //Added by Pradeep Kushwaha on 03-05-2010
        /// <summary>
        /// Verify the address based on zipe code
        /// </summary>
        /// <param name="ZIPCODE">Zipe Code</param>
        /// <param name="COUNTRYID">Contry id</param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        public static String GetCustomerAddressDetailsUsingZipeCode(String ZIPCODE, String COUNTRYID)
        {

            CmsWeb.webservices.ClsWebServiceCommon obj = new CmsWeb.webservices.ClsWebServiceCommon();  //Declare the object ob common webservice

            String ReturnValue = String.Empty;
            DataSet ds = new DataSet();

            ReturnValue = obj.GetAddressDetailsBasedOnZipeCode(ZIPCODE, Convert.ToInt32(COUNTRYID));


            return ReturnValue;
        }
        /// <summary>
        /// Fill the location number on page load 
        /// </summary>
        private void FillLocationNumber()
        {
            
            String Numbers = GetMaxIdOfLocationNumber(hidCUSTOMER_ID.Value, "0", "1");
            if (Numbers != String.Empty)
            {
                String[] Number = Numbers.Split(',');
                if (Number[0] != "0" && Number[1] != "0" && Number[2] == "1")
                {
                    txtLOC_NUM.Text = Number[1].ToString();
                     
                }
               
           
            }
        }
        //Added by Pradeep Kushwaha on 03-July-2010
        /// <summary>
        /// Get the Max Location number and check if exists or no
        /// </summary>
        /// <param name="CUSTOMER_ID">Customer ID ( In Param Value)</param>
        /// <param name="LOCATION_NUMBER">Location Number (in Param value)</param>
        /// <param name="flag">in between (1,2)</param>
        /// <returns>Location Number and return value with flag</returns>
        [System.Web.Services.WebMethod]
        public static String GetMaxIdOfLocationNumber(String CUSTOMER_ID,String LOCATION_NUMBER, String flag)
        {
            ClsLocation objLocation=new ClsLocation();
            String ReturnValue = String.Empty;
            Int64 NUMBER = Convert.ToInt64(LOCATION_NUMBER);//Convert String Location Number to Long 


            ReturnValue = objLocation.GetMaxIDOfLocationNumber(Convert.ToInt32(CUSTOMER_ID), ref NUMBER, Convert.ToInt32(flag));

            if (ReturnValue != "-1")//Check if the return value is not -1
                ReturnValue = ReturnValue + ","  + NUMBER + "," + flag; //Return with retunvalue ,location number and flag 
            else
                ReturnValue = String.Empty;//Return empty if there is any error or record not found

            return ReturnValue;//return value
        }

     }
        #endregion      
        
    }

