/******************************************************************************************
<Author					: - Ajit Singh Chahal
<Start Date				: -	5/30/2005 5:11:42 PM
<End Date				: -	
<Description			: - Code behind for Regular Commission Setup - Agency,
							Additional Commission Setup - Agency,
							Property Inspection Credit.	
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - Code behind for Regular Commission Setup - Agency.

<Modified Date			: - 25/08/2005
<Modified By			: - Anurag Verma
<Purpose				: - Applying Null Check for buttons on aspx page 
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
using Cms.Model.Maintenance.Accounting;
using Cms.BusinessLayer.BlCommon.Accounting;
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher.ExceptionManagement;

namespace Cms.CmsWeb.Maintenance.Accounting
{
    /// <summary>
    /// Code behind for Regular Commission Setup - Agency.
    /// </summary>
    public class AddRegCommSetup_Agency : Cms.CmsWeb.cmsbase
    {
        #region "Control variables"
        protected System.Web.UI.WebControls.Label capSTATE_ID;
        protected System.Web.UI.WebControls.Label capLOB_ID;
        protected System.Web.UI.WebControls.Label capSUB_LOB_ID;
        protected System.Web.UI.WebControls.Label capCLASS_RISK;
        protected System.Web.UI.WebControls.Label capTERM;
        protected System.Web.UI.WebControls.Label capEFFECTIVE_FROM_DATE;
        protected System.Web.UI.WebControls.Label capEFFECTIVE_TO_DATE;
        protected System.Web.UI.WebControls.Label capCOMMISSION_PERCENT;
        protected System.Web.UI.WebControls.Label capCOUNTRY_ID;
        protected System.Web.UI.WebControls.Label capHeader;
        protected System.Web.UI.WebControls.Label capAgency;
        protected Cms.CmsWeb.Controls.CmsButton btnReset;
        protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;
        protected Cms.CmsWeb.Controls.CmsButton btnCopy;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCOMM_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidState;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidSaveLob; //
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidSaveSLob; //
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidSaveClass; //	


        protected System.Web.UI.WebControls.DropDownList cmbSTATE_ID;
        protected System.Web.UI.WebControls.DropDownList cmbLOB_ID;
        protected System.Web.UI.WebControls.DropDownList cmbCLASS_RISK;
        protected System.Web.UI.WebControls.DropDownList cmbTERM;
        protected System.Web.UI.WebControls.DropDownList cmbCOUNTRY_ID;
        protected System.Web.UI.WebControls.TextBox txtEFFECTIVE_FROM_DATE;
        protected System.Web.UI.WebControls.TextBox txtEFFECTIVE_TO_DATE;
        protected System.Web.UI.WebControls.TextBox txtCOMMISSION_PERCENT;



        protected System.Web.UI.WebControls.RequiredFieldValidator rfvSTATE_ID;
        protected System.Web.UI.WebControls.CompareValidator cpvSTATE_ID;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOB_ID;
        protected System.Web.UI.WebControls.CompareValidator cpvLOB_ID;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvSUB_LOB_ID;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvTERM;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvEFFECTIVE_FROM_DATE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvEFFECTIVE_TO_DATE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOMMISSION_PERCENT;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOUNTRY_ID;
        protected System.Web.UI.WebControls.CompareValidator cpvCOUNTRY_ID;
        protected System.Web.UI.WebControls.CustomValidator csvCOMMISSION_PERCENT;

        protected System.Web.UI.WebControls.RegularExpressionValidator revEFFECTIVE_FROM_DATE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revEFFECTIVE_TO_DATE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCOMMISSION_PERCENT;
        protected System.Web.UI.WebControls.Label lblMessage;

        #endregion

        #region Local form variables
        //START:*********** Local form variables *************
        string oldXML;
        //creating resource manager object (used for reading field and label mapping)
        System.Resources.ResourceManager objResourceMgr;
        private string strRowId, strFormSaved;
        //private int intLoggedInUserID;
        protected System.Web.UI.WebControls.Label lblCOUNTRY_NAME;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCOUNTRY_ID;

        //Constants
        const string COUNTRYID = "0";//countryid hard coded for USA for save case 
       // const string COUNTRY_NAME = "";
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOBXML;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidSUB_LOB_ID;
        protected System.Web.UI.WebControls.DropDownList cmbSUB_LOB_ID;
        protected System.Web.UI.WebControls.CustomValidator csvCHECK_DATE;
        protected System.Web.UI.WebControls.Label capREMARKS;
        protected System.Web.UI.WebControls.TextBox txtREMARKS;
        protected System.Web.UI.WebControls.HyperLink hlkFROM_DATE;
        protected System.Web.UI.WebControls.HyperLink hlkTO_DATE;

        //Defining the business layer class object
        ClsRegCommSetup_Agency objRegCommSetup_Agency;
        protected System.Web.UI.WebControls.DropDownList cmbAGENCY_ID;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvAGENCY_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidClass;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidSelectedClass;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidREGULAR_COMMISSION_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCOMMI_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidLobXMLForClass;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;
        public string strCalledFrom = "";
        //END:*********** Local variables *************

        #endregion
        #region Set Validators ErrorMessages
        /// <summary>
        /// Method to set validation control error masessages.
        /// Parameters: none
        /// Return Type: none
        /// </summary>
        private void SetErrorMessages()
        {
            rfvSTATE_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "2");
            cpvSTATE_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "2");
            rfvLOB_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1");
            cpvLOB_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1");
            rfvSUB_LOB_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "3");
            rfvTERM.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "4");
            rfvEFFECTIVE_FROM_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
            rfvEFFECTIVE_TO_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "6");
            rfvCOMMISSION_PERCENT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "7");
            csvCOMMISSION_PERCENT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "8");

            revEFFECTIVE_FROM_DATE.ValidationExpression = aRegExpDate;
            revEFFECTIVE_TO_DATE.ValidationExpression = aRegExpDate;
            revCOMMISSION_PERCENT.ValidationExpression = aRegExpCurrencyformat;

            revEFFECTIVE_FROM_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("179");
            revEFFECTIVE_TO_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("179");
            revCOMMISSION_PERCENT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
            rfvCOUNTRY_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("33");
            cpvCOUNTRY_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("33");
            csvCHECK_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "9");
            rfvAGENCY_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "10");
        }
        #endregion
        #region PageLoad event
        private void Page_Load(object sender, System.EventArgs e)
        {
            btnReset.Attributes.Add("onclick", "javascript:return formReset();");
            //btnSave.Attributes.Add("onclick","return Validate();");
            Ajax.Utility.RegisterTypeForAjax(typeof(AddRegCommSetup_Agency));
            // phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
            base.ScreenId = "178_0";
            if (Request.QueryString["COMMISSION_TYPE"] != null && Request.QueryString["COMMISSION_TYPE"].Length > 0)
            {
                strCalledFrom = Request.QueryString["COMMISSION_TYPE"];
            }

            switch (strCalledFrom)
            {
                case "a":
                case "A":
                    base.ScreenId = "179_0";
                    break;
                case "r":
                case "R":
                    base.ScreenId = "178_0";
                    break;
                default:
                    base.ScreenId = "178_0";
                    break;
            }
            lblMessage.Visible = false;
           

            #region Button Permissions
            //START:*********** Setting permissions and class (Read/write/execute/delete)  *************
            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnActivateDeactivate.CmsButtonClass = CmsButtonType.Write;
            btnActivateDeactivate.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            btnCopy.CmsButtonClass = CmsButtonType.Write;
            btnCopy.PermissionString = gstrSecurityXML;

            //END:*********** Setting permissions and class (Read/write/execute/delete)  *************
            #endregion
            objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.Accounting.AddRegCommSetup_Agency", System.Reflection.Assembly.GetExecutingAssembly());

            hlkTO_DATE.Attributes.Add("OnClick", "fPopCalendar(document.ACT_REG_COMM_SETUP.txtEFFECTIVE_TO_DATE,document.ACT_REG_COMM_SETUP.txtEFFECTIVE_TO_DATE)");
            hlkFROM_DATE.Attributes.Add("OnClick", "fPopCalendar(document.ACT_REG_COMM_SETUP.txtEFFECTIVE_FROM_DATE,document.ACT_REG_COMM_SETUP.txtEFFECTIVE_FROM_DATE)");

            //DataTable dt = Cms.CmsWeb.ClsFetcher.ActiveState;
            //cmbSTATE_ID.DataSource = dt;
            ////				DataRow row = dt.NewRow();
            ////				row["State_Name"] = "All";
            ////				row["State_Id"] = "0";
            ////				dt.Rows.InsertAt(row,0);
            //cmbSTATE_ID.DataTextField = "State_Name";
            //cmbSTATE_ID.DataValueField = "State_Id";
            //cmbSTATE_ID.DataBind();
            ////cmbSTATE_ID.Items.Insert(0,"All");
            //cmbSTATE_ID.Items.Insert(0, new ListItem("", ""));
            //cmbSTATE_ID.Items.Insert(1, new ListItem("All", "9999"));

            if (!Page.IsPostBack)
            {

                #region "Loading singleton"
                DataTable dtcountry = Cms.CmsWeb.ClsFetcher.Country;
                cmbCOUNTRY_ID.DataSource = dtcountry;
                cmbCOUNTRY_ID.DataTextField = "Country_Name";
                cmbCOUNTRY_ID.DataValueField = "Country_Id";
                SetCultureThread(GetLanguageCode());
                cmbCOUNTRY_ID.DataBind();
                cmbCOUNTRY_ID.Items.Insert(0, new ListItem("", "0"));
                SetErrorMessages();

                //Added by Ruchika Chauhan on 9-Jan-2012 for TFS # 836
                DataTable dtProd = Cms.CmsWeb.ClsFetcher.LOBs;
                cmbLOB_ID.DataSource = dtProd;
                cmbLOB_ID.DataTextField = "LOB_DESC";
                cmbLOB_ID.DataValueField = "LOB_ID";
                SetCultureThread(GetLanguageCode());
                cmbLOB_ID.DataBind();
                //cmbLOB_ID.Items.Insert(0, new ListItem("All", "0"));//added by avijit on 13/02/2012.
                cmbLOB_ID.Items.Insert(0, new ListItem("", "0")); 

                SetErrorMessages();
                //Added till here
                
                #endregion//Loading singleton

                string url = ClsCommon.GetLookupURL();
                SetCaptions();
                if (ClsCommon.IsXMLResourceExists(@Request.PhysicalApplicationPath + "CmsWeb/support/PageXML/" + GetSystemId(), "AddRegCommSetup_Agency.xml"))
                {
                    setPageControls(Page, @Request.PhysicalApplicationPath + "/CmsWeb/support/PageXML/" + GetSystemId() + "/AddRegCommSetup_Agency.xml");
                }
                BindData();
               
                btnCopy.Attributes.Add("onClick", "javascript:CopyRecords();return false;");
                //btnCopy.Attributes.Add("onClick","javascript:OpenLookupWithFunction(\"" + url + "\",\"COMM_ID\",\"REGULAR_COMMISSION_ID\",\"hidCOMMI_ID\",\"hidREGULAR_COMMISSION_ID\",\"RegularCommission\",\"RegularCommission\",'','splitRegularCommission();')");
                if (Request.QueryString["COMM_ID"] != null)
                {
                    hidOldData.Value = ClsRegCommSetup_Agency.GetXmlForPageControls(Request.QueryString["COMM_ID"].ToString());
                    FillControlsDefault();

                    if (hidOldData.Value != "")
                    {
                        //Selecting the state id and then filling the associated line of busieness for that state
                        System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                        doc.LoadXml(hidOldData.Value);

                        //System.Xml.XmlNode nod = doc.SelectSingleNode("/NewDataSet/Table/STATE_ID");
                        //if (nod != null)
                        //{
                        //    cmbSTATE_ID.SelectedValue = nod.InnerText;
                        //    cmbSTATE_ID_SelectedIndexChanged(null, null);
                        //    //hidFormSaved.Value = "1";
                        //}
                        //nod = null;
                        //doc = null;

                    }
                }

               
               //GetLobsInDropDown(cmbLOB_ID);
                //hidLOBXML.Value = Cms.BusinessLayer.BlApplication.clsPkgLobDetails.GetXmlForLobByState();
                hidLOBXML.Value = ClsCommon.GetXmlForLobByState();


                //hidClass.Value = ClsCommon.GetXmlForClass();
                Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGenInfo = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
                hidLobXMLForClass.Value = objGenInfo.GetLOBBYSTATEID().GetXml();
                hidClass.Value = objGenInfo.GetAllClassOnStateId();
                hidClass.Value = "<NewDataSet>" + hidClass.Value + "</NewDataSet>";
                hidClass.Value = hidClass.Value.Replace("\\", "");

                //				hidStateXML.Value = ClsCommon.GetXmlForLobByState();
                //displaying agency dd in case of additional commision
                if (Request.QueryString["COMMISSION_TYPE"] != null && Request.QueryString["COMMISSION_TYPE"] == "A")
                {
                    ClsAgency.GetAgencyNamesInDropDown(cmbAGENCY_ID);
                    cmbAGENCY_ID.Items.Insert(0, new ListItem("", ""));
                    //Itrack 3933
                    string strCarrierSystemID = CarrierSystemID;
                    string agencyID = "";
                    DataSet objDataSet = ClsAgency.GetAgencyIDAndNameFromCode(strCarrierSystemID);
                    if (objDataSet.Tables[0].Rows.Count > 0)
                    {
                        agencyID = objDataSet.Tables[0].Rows[0]["AGENCY_ID"].ToString();
                        cmbAGENCY_ID.Items.Remove(cmbAGENCY_ID.Items.FindByValue(agencyID));
                    }
                }
               

                //Get The Value Of Class Field
                if (hidOldData.Value != "")
                {
                    hidSelectedClass.Value = Cms.BusinessLayer.BlApplication.ClsGeneralInformation.FetchValueFromXML("CLASS_RISK", hidOldData.Value);
                }


                //				ClsRegCommSetup_Agency.GetLOBInDropDown(cmbLOB_ID);
                //				ClsRegCommSetup_Agency.GetSUB_LOBInDropDown(cmbSUB_LOB_ID);
                //				
                //				//-- this code has to be chaged
                //				cmbCLASS_RISK.DataSource		=	ClsCommon.GetLookup("DRTCD");
                //				cmbCLASS_RISK.DataTextField		=	"LookupDesc";
                //				cmbCLASS_RISK.DataValueField	=	"LookupID";
                //				cmbCLASS_RISK.DataBind();
                //				cmbCLASS_RISK.Items.Insert(0,new ListItem("All","0"));
                //				cmbCLASS_RISK.Items.Insert(0,new ListItem("",""));
                //			
                //-- this code has to be chaged

                //hidCOUNTRY_ID.Value = COUNTRYID;
                lblCOUNTRY_NAME.Text = COUNTRY_NAME;
                if (hidOldData.Value != "")
                {
                    hidIS_ACTIVE.Value = ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value);
                    if (hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
                    {
                        btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchActivateDeactivateButtonsText("Y");
                    }

                    else
                    {
                        btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchActivateDeactivateButtonsText("N");
                    }
                }
               
                
               
            }


        }//end pageload

        private void GetClassData()
        {
            if (hidOldData.Value != "")
            {
                hidSelectedClass.Value = Cms.BusinessLayer.BlApplication.ClsGeneralInformation.FetchValueFromXML("CLASS_RISK", hidOldData.Value);
            }
        }
        #endregion

        //This method populates all LOBs based on StateID and returns 
        //Generic Type values of tables
        [System.Web.Services.WebMethod]
        public static System.Collections.Generic.Dictionary<string, object> GetSubLOBs(string STATE_ID)
        {
            try
            {
                System.Collections.Generic.Dictionary<string, object> dd = new System.Collections.Generic.Dictionary<string, object>();
                Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGenInfo = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();

                DataSet ds = new DataSet();
                ds = objGenInfo.GetLOBBYSTATEID(Convert.ToInt32(STATE_ID));
                return Cms.CmsWeb.support.ClsjQueryCommon.ToJson(ds.Tables[0]);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [System.Web.Services.WebMethod]
        public static System.Collections.Generic.Dictionary<string, object> GetSubSubLOBs(string STATE_ID, string LOB_ID)
        {
            try
            {
                System.Collections.Generic.Dictionary<string, object> dd = new System.Collections.Generic.Dictionary<string, object>();
                Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGenInfo = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();

                DataSet ds = new DataSet();
                ds = objGenInfo.GetSubLOBBYSTATELOBID(Convert.ToInt32(STATE_ID), Convert.ToInt32(LOB_ID));
                return Cms.CmsWeb.support.ClsjQueryCommon.ToJson(ds.Tables[0]);


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        #region GetFormValue
        /// <summary>
        /// Fetch form's value and stores into model class object and return that object.
        /// </summary>
        private ClsRegCommSetup_AgencyInfo GetFormValue()
        {
            //Creating the Model object for holding the New data
            ClsRegCommSetup_AgencyInfo objRegCommSetup_AgencyInfo;
            objRegCommSetup_AgencyInfo = new ClsRegCommSetup_AgencyInfo();

            //objRegCommSetup_AgencyInfo.COUNTRY_ID			=	int.Parse(hidCOUNTRY_ID.Value);
            objRegCommSetup_AgencyInfo.COUNTRY_ID = int.Parse(cmbCOUNTRY_ID.SelectedValue);
            if (!string.IsNullOrEmpty(Request["cmbSTATE_ID"]))
            {
                objRegCommSetup_AgencyInfo.STATE_ID = int.Parse(Request["cmbSTATE_ID"].ToString().Trim());
            }
            if (hidSaveLob.Value != null && hidSaveLob.Value.Length > 0)
                objRegCommSetup_AgencyInfo.LOB_ID = int.Parse(hidSaveLob.Value);

            objRegCommSetup_AgencyInfo.LOB_ID				=	int.Parse(cmbLOB_ID.SelectedValue);
            objRegCommSetup_AgencyInfo.SUB_LOB_ID = -1;

            //Get The Sub Lob
            //int sublobId = 0;
            //sublobId= int.Parse(Request["cmbSUB_LOB_ID"]);
            //objRegCommSetup_AgencyInfo.SUB_LOB_ID =sublobId;

            //Commmnetd
            //if(hidSUB_LOB_ID.Value!=null && hidSUB_LOB_ID.Value.Length>0)
            //	objRegCommSetup_AgencyInfo.SUB_LOB_ID			=	int.Parse(hidSUB_LOB_ID.Value);
            if (Request["cmbSUB_LOB_ID"] != null)
                objRegCommSetup_AgencyInfo.SUB_LOB_ID = int.Parse(Request["cmbSUB_LOB_ID"].ToString().Trim());

            //int classValue=int.Parse(Request["cmbCLASS_RISK"]);
            //Commnetd
            //if(hidSelectedClass.Value!=null && hidSelectedClass.Value.Length>0)
            //	objRegCommSetup_AgencyInfo.CLASS_RISK		=	int.Parse(hidSelectedClass.Value);
            if (Request["cmbCLASS_RISK"] != null)
                objRegCommSetup_AgencyInfo.CLASS_RISK = int.Parse(Request["cmbCLASS_RISK"].ToString().Trim());




            objRegCommSetup_AgencyInfo.TERM = cmbTERM.SelectedValue;
            objRegCommSetup_AgencyInfo.EFFECTIVE_FROM_DATE = ConvertToDate(txtEFFECTIVE_FROM_DATE.Text);
            objRegCommSetup_AgencyInfo.EFFECTIVE_TO_DATE = ConvertToDate(txtEFFECTIVE_TO_DATE.Text);
            //objRegCommSetup_AgencyInfo.EFFECTIVE_FROM_DATE = DateTime.Parse(txtEFFECTIVE_FROM_DATE.Text);
            //objRegCommSetup_AgencyInfo.EFFECTIVE_TO_DATE = DateTime.Parse(txtEFFECTIVE_TO_DATE.Text);
            objRegCommSetup_AgencyInfo.COMMISSION_PERCENT = double.Parse(txtCOMMISSION_PERCENT.Text);
            objRegCommSetup_AgencyInfo.COMMISSION_TYPE = Request.QueryString["COMMISSION_TYPE"];
            objRegCommSetup_AgencyInfo.REMARKS = txtREMARKS.Text;

            if (Request.QueryString["COMMISSION_TYPE"] != null && Request.QueryString["COMMISSION_TYPE"] == "A")
            {
                objRegCommSetup_AgencyInfo.AGENCY_ID = int.Parse(cmbAGENCY_ID.SelectedValue);
            }

            //These  assignments are common to all pages.
            strFormSaved = hidFormSaved.Value;
            strRowId = hidCOMM_ID.Value;
            oldXML = hidOldData.Value;
            //Returning the model object

            return objRegCommSetup_AgencyInfo;
        }
        #endregion

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {
            //this.cmbSTATE_ID.SelectedIndexChanged += new System.EventHandler(this.cmbSTATE_ID_SelectedIndexChanged);
            this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        #region "Web Event Handlers"

        [System.Web.Services.WebMethod]
        public static System.Collections.Generic.Dictionary<string, object> AjaxFillState(string COUNTRY_ID)
        {


            try
            {
                System.Collections.Generic.Dictionary<string, object> dd = new System.Collections.Generic.Dictionary<string, object>();
                CmsWeb.webservices.ClsWebServiceCommon obj = new CmsWeb.webservices.ClsWebServiceCommon();

                DataSet ds = new DataSet();
                ds = obj.FillActiveState(Convert.ToInt32(COUNTRY_ID));
                return Cms.CmsWeb.support.ClsjQueryCommon.ToJson(ds.Tables[0]);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// If form is posted back then add entry in database using the BL object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            try
            {
                int intRetVal;	//For retreiving the return value of business class save function
                objRegCommSetup_Agency = new ClsRegCommSetup_Agency(true);

                //Retreiving the form values into model class object
                ClsRegCommSetup_AgencyInfo objRegCommSetup_AgencyInfo = GetFormValue();

                if (strRowId.ToUpper().Equals("NEW")) //save case
                {
                    objRegCommSetup_AgencyInfo.CREATED_BY = int.Parse(GetUserId());
                    objRegCommSetup_AgencyInfo.CREATED_DATETIME = DateTime.Now;
                    objRegCommSetup_AgencyInfo.MODIFIED_BY = objRegCommSetup_AgencyInfo.CREATED_BY;
                    objRegCommSetup_AgencyInfo.LAST_UPDATED_DATETIME = objRegCommSetup_AgencyInfo.CREATED_DATETIME;
                    objRegCommSetup_AgencyInfo.IS_ACTIVE = "Y";


                    //Calling the add method of business layer class
                    intRetVal = objRegCommSetup_Agency.Add(objRegCommSetup_AgencyInfo);

                    if (intRetVal > 0)
                    {
                        hidCOMM_ID.Value = objRegCommSetup_AgencyInfo.COMM_ID.ToString();
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "29");
                        hidFormSaved.Value = "1";
                        hidIS_ACTIVE.Value = "Y";
                        hidOldData.Value = ClsRegCommSetup_Agency.GetXmlForPageControls(objRegCommSetup_AgencyInfo.COMM_ID.ToString());
                        FillControlsDefault();

                        GetClassData();

                    }
                    else if (intRetVal == -1)
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("883");
                        hidFormSaved.Value = "2";
                        FillControlsAfterNoSave();

                    }
                    else if (intRetVal == -2)
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("883");
                        hidFormSaved.Value = "2";
                        FillControlsAfterNoSave();
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "20");
                        hidFormSaved.Value = "2";
                        FillControlsAfterNoSave();
                    }
                    lblMessage.Visible = true;
                } // end save case
                else //UPDATE CASE
                {

                    //Creating the Model object for holding the Old data
                    ClsRegCommSetup_AgencyInfo objOldRegCommSetup_AgencyInfo;
                    objOldRegCommSetup_AgencyInfo = new ClsRegCommSetup_AgencyInfo();

                    //Setting  the Old Page details(XML File containing old details) into the Model Object
                    base.PopulateModelObject(objOldRegCommSetup_AgencyInfo, hidOldData.Value);

                    //Setting those values into the Model object which are not in the page
                    objRegCommSetup_AgencyInfo.COMM_ID = int.Parse(strRowId);
                    objRegCommSetup_AgencyInfo.MODIFIED_BY = int.Parse(GetUserId());
                    objRegCommSetup_AgencyInfo.LAST_UPDATED_DATETIME = DateTime.Now;
                    //Updating the record using business layer class object
                    intRetVal = objRegCommSetup_Agency.Update(objOldRegCommSetup_AgencyInfo, objRegCommSetup_AgencyInfo);
                    if (intRetVal > 0)			// update successfully performed
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "31");
                        hidFormSaved.Value = "1";
                        hidOldData.Value = ClsRegCommSetup_Agency.GetXmlForPageControls(objRegCommSetup_AgencyInfo.COMM_ID.ToString());
                        GetClassData();
                        FillControlsDefault();

                    }
                    else if (intRetVal == -1)	// Duplicate code exist, update failed
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("883");
                        hidFormSaved.Value = "2";
                        FillControlsAfterNoSave();
                    }
                    else if (intRetVal == -2)
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("883");
                        hidFormSaved.Value = "2";
                        FillControlsAfterNoSave();
                    }
                    else
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "20");
                        hidFormSaved.Value = "2";
                        FillControlsAfterNoSave();
                    }
                    lblMessage.Visible = true;
                }


                if (hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
                {
                    btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchActivateDeactivateButtonsText("Y");
                }

                else
                {
                    btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchActivateDeactivateButtonsText("N");
                }


            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";
                //ExceptionManager.Publish(ex);
            }
            finally
            {
                if (objRegCommSetup_Agency != null)
                    objRegCommSetup_Agency.Dispose();
            }

        }

        /// <summary>
        /// Activates and deactivates  .
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
        {
            try
            {
                Cms.BusinessLayer.BlCommon.stuTransactionInfo objStuTransactionInfo = new Cms.BusinessLayer.BlCommon.stuTransactionInfo();
                objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
                objStuTransactionInfo.loggedInUserName = GetUserName();
                objRegCommSetup_Agency = new ClsRegCommSetup_Agency();

                if (hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
                {
                    objStuTransactionInfo.transactionDescription = "Deactivated Succesfully.";
                    objRegCommSetup_Agency.TransactionInfoParams = objStuTransactionInfo;
                    objRegCommSetup_Agency.ActivateDeactivate(hidCOMM_ID.Value, "N");
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "41");
                    hidIS_ACTIVE.Value = "N";
                    btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchActivateDeactivateButtonsText("Y");
                }
                else
                {
                    objStuTransactionInfo.transactionDescription = "Activated Succesfully.";
                    objRegCommSetup_Agency.TransactionInfoParams = objStuTransactionInfo;
                    objRegCommSetup_Agency.ActivateDeactivate(hidCOMM_ID.Value, "Y");
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "40");
                    hidIS_ACTIVE.Value = "Y";
                    btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchActivateDeactivateButtonsText("N");
                }
                hidOldData.Value = ClsRegCommSetup_Agency.GetXmlForPageControls(hidCOMM_ID.Value);
                hidFormSaved.Value = "1";
                if (hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
                {
                    btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchActivateDeactivateButtonsText("Y");
                }

                else
                {
                    btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchActivateDeactivateButtonsText("N");
                }
                FillControlsDefault();


            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                ExceptionManager.Publish(ex);
            }
            finally
            {
                lblMessage.Visible = true;
                if (objRegCommSetup_Agency != null)
                    objRegCommSetup_Agency.Dispose();
            }
        }
        #endregion
        private void SetCaptions()
        {
            capSTATE_ID.Text = objResourceMgr.GetString("cmbSTATE_ID");
            capLOB_ID.Text = objResourceMgr.GetString("cmbLOB_ID");
            capSUB_LOB_ID.Text = objResourceMgr.GetString("cmbSUB_LOB_ID");
            capCLASS_RISK.Text = objResourceMgr.GetString("cmbCLASS_RISK");
            capTERM.Text = objResourceMgr.GetString("cmbTERM");
            capEFFECTIVE_FROM_DATE.Text = objResourceMgr.GetString("txtEFFECTIVE_FROM_DATE");
            capEFFECTIVE_TO_DATE.Text = objResourceMgr.GetString("txtEFFECTIVE_TO_DATE");
            capREMARKS.Text = objResourceMgr.GetString("txtREMARKS");
            capCOMMISSION_PERCENT.Text = objResourceMgr.GetString("txtCOMMISSION_PERCENT");
            capCOUNTRY_ID.Text = objResourceMgr.GetString("cmbCOUNTRY_ID");
            capHeader.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
            btnCopy.Text = objResourceMgr.GetString("btnCopy");
            capAgency.Text = objResourceMgr.GetString("capAgency");
        }
        private void BindData()
        {
            cmbTERM.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("TERM");
            cmbTERM.DataTextField = "LookupDesc";
            cmbTERM.DataValueField = "LookupCode";
            cmbTERM.DataBind();
        }
        # region FillControlsDefault
        private void FillControlsDefault()
        {
            hidState.Value = ClsCommon.FetchValueFromXML("STATE_ID", hidOldData.Value);
            hidSaveLob.Value = ClsCommon.FetchValueFromXML("LOB_ID", hidOldData.Value);
            hidSaveSLob.Value = ClsCommon.FetchValueFromXML("SUB_LOB_ID", hidOldData.Value);
            hidSaveClass.Value = ClsCommon.FetchValueFromXML("CLASS_RISK", hidOldData.Value);
            hidCOUNTRY_ID.Value = ClsCommon.FetchValueFromXML("COUNTRY_ID", hidOldData.Value);

        }
        #endregion

        # region FillControlsAfterNoSave
        private void FillControlsAfterNoSave()
        {

            if (Request["cmbLOB_ID"] != null)
            {
                if (int.Parse(Request["cmbLOB_ID"].ToString()) > 0)
                {
                    int id = int.Parse(Request["cmbLOB_ID"].ToString());
                    hidSaveLob.Value = id.ToString();
                }

            }
            if (Request["cmbSUB_LOB_ID"] != null)
            {
                if (int.Parse(Request["cmbSUB_LOB_ID"].ToString()) > 0)
                {
                    int id = int.Parse(Request["cmbSUB_LOB_ID"].ToString());
                    hidSaveSLob.Value = id.ToString();
                }

            }
            if (Request["cmbCLASS_RISK"] != null)
            {
                if (int.Parse(Request["cmbCLASS_RISK"].ToString()) > 0)
                {
                    int id = int.Parse(Request["cmbCLASS_RISK"].ToString());
                    hidSaveClass.Value = id.ToString();
                }

            }
            if (Request["cmbSTATE_ID"] != null)
            {
                hidState.Value = Request["cmbSTATE_ID"].ToString();
            }
            if (Request["cmbCOUNTRY_ID"] != null)
            {
                hidCOUNTRY_ID.Value = Request["cmbCOUNTRY_ID"].ToString();
            }


        }
        #endregion
        #region "Fill DropDowns"
        public static void GetLobsInDropDown(DropDownList objDropDownList, string selectedValue)
        {

            DataTable objDataTable = Cms.CmsWeb.ClsFetcher.LOBs;
            objDropDownList.Items.Clear();
            objDropDownList.Items.Add("");
            objDropDownList.Items.Add(new ListItem("ALL", "0"));
            for (int i = 0; i < objDataTable.DefaultView.Count; i++)
            {
                objDropDownList.Items.Add(new ListItem(objDataTable.DefaultView[i]["LOB_DESC"].ToString(), objDataTable.DefaultView[i]["LOB_ID"].ToString()));
                //if (selectedValue != null && selectedValue.Length > 0 && objDataTable.DefaultView[i]["LOB_ID"].ToString().Equals(selectedValue))
                //    objDropDownList.SelectedIndex = i;
            }
        }

        private void btnCopy_Click(object sender, System.EventArgs e)
        {
            //cmbSTATE_ID_SelectedIndexChanged(null, null);
        }

        private void cmbSTATE_ID_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            /*try
            {
                int stateID;
				
                Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGenInfo=new Cms.BusinessLayer.BlApplication.ClsGeneralInformation(); 
//				DataSet dsLOB=new DataSet(); 
                stateID=cmbSTATE_ID.SelectedItem==null?-1:int.Parse(cmbSTATE_ID.SelectedItem.Value); 
                if(stateID!=-1)
                {
					
//					dsLOB=objGenInfo.GetLOBBYSTATEID(stateID);
//					cmbLOB_ID.DataSource=dsLOB;
//					cmbLOB_ID.DataTextField="LOB_DESC";
//					cmbLOB_ID.DataValueField="LOB_ID"; 
//					cmbLOB_ID.DataBind(); 
					
                //	if(stateID!=49)
                //	{
                        //cmbLOB_ID.Items.Insert(0,new ListItem("All","0"));					
                        //cmbLOB_ID.Items.Insert(0,"");
                //	}					
                    //Get The Class For Lob and Sub Lob
                    hidClass.Value =objGenInfo.GetAllClassOnStateId(stateID);
                    hidClass.Value ="<NewDataSet>" + hidClass.Value  + "</NewDataSet>";
                    hidClass.Value = hidClass.Value.Replace("\\","");
                    hidState.Value =stateID.ToString();
                }
				
            }
            catch(Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }*/
        }

        public static void GetLobsInDropDown(DropDownList objDropDownList)
        {
            GetLobsInDropDown(objDropDownList, null);
        }
        #endregion



    }
}
