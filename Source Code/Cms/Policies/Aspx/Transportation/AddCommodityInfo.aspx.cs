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
using System.Resources;
using System.Reflection;
using Cms.Model.Policy.Transportation;

namespace Cms.Policies.Aspx.Transportation
{
    public partial class AddCommodityInfo : Cms.Policies.policiesbase
    {
        #region webcontrols Added Declaration on page
        protected System.Web.UI.WebControls.Label lblDelete;
        protected System.Web.UI.WebControls.Label lblMessage;

        protected System.Web.UI.WebControls.Label capCOMMODITY_NUMBER;
        protected System.Web.UI.WebControls.TextBox txtCOMMODITY_NUMBER;
        protected System.Web.UI.WebControls.Label capCOMMODITY;
        protected System.Web.UI.WebControls.TextBox txtCOMMODITY;
  
        protected System.Web.UI.WebControls.Label capDEPARTING_DATE;
        protected System.Web.UI.WebControls.TextBox txtDEPARTING_DATE;
        protected System.Web.UI.WebControls.HyperLink hlkDEPARTING_DATE;
        protected System.Web.UI.WebControls.Image imgDEPARTING_DATE;
         
        protected System.Web.UI.WebControls.Label capORIGIN;
        protected System.Web.UI.WebControls.Label capORIGIN_COUNTRY;
        protected System.Web.UI.WebControls.DropDownList cmbORIGIN_COUNTRY;
        protected System.Web.UI.WebControls.Label capORIGIN_STATE;
        protected System.Web.UI.WebControls.DropDownList cmbORIGIN_STATE;
        protected System.Web.UI.WebControls.Label capORIGIN_CITY;
        protected System.Web.UI.WebControls.TextBox txtORIGIN_CITY;
        protected System.Web.UI.WebControls.Label capDESTINATION;
        protected System.Web.UI.WebControls.Label capDESTINATION_COUNTRY;
        protected System.Web.UI.WebControls.DropDownList cmbDESTINATION_COUNTRY;
        protected System.Web.UI.WebControls.Label capDESTINATION_STATE;
        protected System.Web.UI.WebControls.DropDownList cmbDESTINATION_STATE;
        protected System.Web.UI.WebControls.Label capDESTINATION_CITY;
        protected System.Web.UI.WebControls.TextBox txtDESTINATION_CITY;
        protected System.Web.UI.WebControls.Label capREMARKS;
        protected System.Web.UI.WebControls.TextBox txtREMARKS;
        protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOMMODITY_NUMBER;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOMMODITY;
   
        protected string PAGEFROM;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidEXPIRATIONDATE;
       
        #endregion

        System.Resources.ResourceManager objResourceMgr;

        string CalledFrom;
        ClsProducts objProducts = new ClsProducts();
        private string strRowId = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            #region setting screen id

            if (Request.QueryString["CalledFrom"] != null && Request.QueryString["CalledFrom"].ToString() != "")
            {
                CalledFrom = Request.QueryString["CalledFrom"].ToString();
                switch (CalledFrom.ToUpper().Trim())
                {
                    case "NCTRANS":
                        hidCALLED_FROM.Value = CalledFrom;
                        base.ScreenId = NATIONAL_CARGO_TRANSPORTATIONscreenId.INFORMATION_PAGE;
                        hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCtl");
                        break;
                    case "INTERNTRANS":
                        hidCALLED_FROM.Value = CalledFrom;
                        base.ScreenId = INTERNATIONAL_CARGO_TRANSPORTATIONscreenId.INFORMATION_PAGE;
                        hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCtl");
                        break;

                }
            }

            if (Request.QueryString["PAGEFROM"] != null)
                PAGEFROM = Request.QueryString["PAGEFROM"].ToString();
           
            #endregion

                      

            this.SetErrorMessages();
            Ajax.Utility.RegisterTypeForAjax(typeof(AddCommodityInfo)); 			


            #region setting security Xml
            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnActivateDeactivate.CmsButtonClass = CmsButtonType.Write;
            btnActivateDeactivate.PermissionString = gstrSecurityXML;

            btnDelete.CmsButtonClass = CmsButtonType.Delete;
            btnDelete.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            #endregion
            hlkDEPARTING_DATE.Attributes.Add("OnClick", "fPopCalendar(document.POL_COMMODITY_INFO.txtDEPARTING_DATE,document.POL_COMMODITY_INFO.txtDEPARTING_DATE)"); //Javascript Implementation for Calender				

            hidPOL_ID.Value = GetPolicyID();
            hidPOL_VERSION_ID.Value = GetPolicyVersionID();
            hidAPP_ID.Value = GetAppID();
            hidAPP_VERSION_ID.Value = GetAppVersionID();
            hidCUSTOMER_ID.Value = GetCustomerID();
           
            objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.Transportation.AddCommodityInfo", System.Reflection.Assembly.GetExecutingAssembly());

            if (!Page.IsPostBack)
            {
                     this.getdate();
                     this.SetCaptions();
                     this.FillDropdowns();
                     this.BindConveyanceType();
                     this.BindExceededPremium();
                    
                     btnReset.Attributes.Add("onclick", "javascript: return ResetTheForm();");

                     if (Request.QueryString["COMMODITY_ID"] != null && Request.QueryString["COMMODITY_ID"].ToString() != "" && Request.QueryString["COMMODITY_ID"] != "NEW")
                     {

                         hidCommodity_ID.Value = Request.QueryString["COMMODITY_ID"].ToString();

                         this.GetOldDataObject(Convert.ToInt32(Request.QueryString["COMMODITY_ID"].ToString()));

                     }
                     else if (Request.QueryString["COMMODITY_ID"] == null || Request.QueryString["COMMODITY_ID"].ToString().ToUpper() == "NEW")
                     {
                         btnActivateDeactivate.Enabled = false;
                         btnDelete.Enabled = false;
                         hidCommodity_ID.Value = "NEW";
                         this.FillVesselNumber();
                         btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1333"); 
                     }
                     strRowId = hidCommodity_ID.Value;
                    

			}
        }

        private void getdate()
        {
            ClsGeneralInformation objGenInfo = new ClsGeneralInformation();
            DataSet ds = objGenInfo.GetPolicyDetails(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOL_ID.Value), int.Parse(hidPOL_VERSION_ID.Value));
            if (ds.Tables[0].Rows.Count > 0)
            {
                hidEFFECTIVEDATE.Value = ConvertToDateCulture(Convert.ToDateTime(ds.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()));
                hidEXPIRATIONDATE.Value =ConvertToDateCulture(Convert.ToDateTime(ds.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString()));
            }
        }

        private void BindExceededPremium()
        {
            cmbExceeded_Premium.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("yesno");
            cmbExceeded_Premium.DataTextField = "LookupDesc";
            cmbExceeded_Premium.DataValueField = "LookupID";
            cmbExceeded_Premium.DataBind();
        }

        private void BindConveyanceType()

        {
            if (Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("COVTPE").Select("", "LookupDesc").Length > 0)
            {
                cmbCONVEYANCE_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("COVTPE").Select("", "LookupDesc").CopyToDataTable<DataRow>();
            }
            else
            {
                cmbCONVEYANCE_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("COVTPE");
            }
                cmbCONVEYANCE_TYPE.DataTextField = "LookupDesc";
                cmbCONVEYANCE_TYPE.DataValueField = "LookupID";
                cmbCONVEYANCE_TYPE.DataBind();
                cmbCONVEYANCE_TYPE.Items.Insert(0, "");
            
        }//private void BindConveyanceType()

        private void SetCaptions()
        {
            #region setcaption in resource file


            capCOMMODITY_NUMBER.Text = objResourceMgr.GetString("txtCOMMODITY_NUMBER");
            capCOMMODITY.Text = objResourceMgr.GetString("txtCOMMODITY");
             
            capDEPARTING_DATE.Text = objResourceMgr.GetString("txtDEPARTING_DATE");
            
            capORIGIN_COUNTRY.Text = objResourceMgr.GetString("cmbORIGIN_COUNTRY");
            capORIGIN_STATE.Text = objResourceMgr.GetString("cmbORIGIN_STATE");
            capORIGIN_CITY.Text = objResourceMgr.GetString("txtORIGIN_CITY");
            capDESTINATION_COUNTRY.Text=objResourceMgr.GetString("cmbDESTINATION_COUNTRY");
            capDESTINATION_STATE.Text = objResourceMgr.GetString("cmbDESTINATION_STATE");
            capDESTINATION_CITY.Text = objResourceMgr.GetString("txtDESTINATION_CITY");
            capREMARKS.Text = objResourceMgr.GetString("txtREMARKS");
            capDESTINATION.Text = objResourceMgr.GetString("capDESTINATION");
            capORIGIN.Text = objResourceMgr.GetString("capORIGIN");
            capVoyage_Information.Text = objResourceMgr.GetString("capVoyage_Information");
            capCONVEYANCE_TYPE.Text = objResourceMgr.GetString("cmbCONVEYANCE_TYPE");
            capmandatory.Text = objResourceMgr.GetString("capmandatory");
            capCoApplicant.Text = objResourceMgr.GetString("capCoApplicant");
            capExceededPremium.Text = objResourceMgr.GetString("cmbExceeded_Premium");
            #endregion

        }
        private void FillDropdowns()
        {
            #region Commented for Itrack -856
            /*
            DataTable dt = Cms.CmsWeb.ClsFetcher.AllCountry;

            cmbORIGIN_COUNTRY.DataSource = dt;
            cmbORIGIN_COUNTRY.DataTextField = COUNTRY_NAME;
            cmbORIGIN_COUNTRY.DataValueField = COUNTRY_ID;
            cmbORIGIN_COUNTRY.DataBind();
            cmbORIGIN_COUNTRY.SelectedValue = Convert.ToString(5);

            cmbDESTINATION_COUNTRY.DataSource = dt;
            cmbDESTINATION_COUNTRY.DataTextField = COUNTRY_NAME;
            cmbDESTINATION_COUNTRY.DataValueField = COUNTRY_ID;
            cmbDESTINATION_COUNTRY.DataBind();
            cmbDESTINATION_COUNTRY.SelectedValue = Convert.ToString(5);

            dt = Cms.CmsWeb.ClsFetcher.State;
            cmbORIGIN_STATE.DataSource = dt;
            cmbORIGIN_STATE.DataTextField = STATE_NAME;
            cmbORIGIN_STATE.DataValueField = STATE_ID;
            cmbORIGIN_STATE.DataBind();
            
            cmbORIGIN_STATE.Items.Insert(0, "");
            
            cmbDESTINATION_STATE.DataSource = dt;
            cmbDESTINATION_STATE.DataTextField = STATE_NAME;
            cmbDESTINATION_STATE.DataValueField = STATE_ID;
            cmbDESTINATION_STATE.DataBind();
            cmbDESTINATION_STATE.Items.Insert(0, "");*/
            #endregion

            ClsCommodityInfo CommodityInfo = new ClsCommodityInfo();
            // changes by praveer for itrack no 900
            DataSet ds = CommodityInfo.FetchApplicants(Convert.ToInt32(hidCUSTOMER_ID.Value), Convert.ToInt32(hidAPP_VERSION_ID.Value), Convert.ToInt32(hidPOL_ID.Value));
            cmbCO_APPLICANT_ID.DataSource = ds;
            cmbCO_APPLICANT_ID.DataTextField = "APPLICANT_NAME";
            cmbCO_APPLICANT_ID.DataValueField = "APPLICANT_ID";
            cmbCO_APPLICANT_ID.DataBind();
            cmbCO_APPLICANT_ID.Items.Insert(0, "");
            // changes by praveer for itrack no 900
            string ApplicantID = "";
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr["IS_PRIMARY_APPLICANT"].ToString() == "1")
                {
                    ApplicantID = dr["APPLICANT_ID"].ToString();
                    break;
                }
            }
            if (ApplicantID != "" && ApplicantID != null)
            {
                cmbCO_APPLICANT_ID.SelectedValue = ApplicantID;
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
        private void SetErrorMessages()
        {


            revCOMMODITY_NUMBER.ValidationExpression = aRegExpInteger;
            revCOMMODITY_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "19");

            //revCOMMODITY.ValidationExpression = aRegExpTextArea500;
            //revCOMMODITY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
            rfvCOMMODITY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "4");
            rfvCONVEYANCE_TYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "6");
 
            revDEPARTING_DATE.ValidationExpression = aRegExpDate;
            revDEPARTING_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "7");
            
            rfvORIGIN_STATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "8");
            
            
            revORIGIN_CITY.ValidationExpression = aRegExpTextArea100;
            revORIGIN_CITY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "10");

            rfvDESTINATION_STATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "11");

            revDESTINATION_CITY.ValidationExpression = aRegExpTextArea100;
            revDESTINATION_CITY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "13");

            
            //csvREMARKS.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage("G", "442");
            csvCOMMODITY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage("G", "442");
          
            rfvDEPARTING_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "16");
            rfvDESTINATION_CITY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "12");
           
            
            rfvORIGIN_CITY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "9");
            rfvCOMMODITY_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "18");
            hidCOMMODITY_NUMBER_Msg.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "17");
            csvDEPARTING_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "20");
            rfvCO_APPLICANT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1179");

            rfvORIGN_COUNTRY.ErrorMessage =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "21");
            rfvDEST_COUNTRY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "22");

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

       
        /// <summary>
        /// Use to Get the Old Data Based on the ID
        /// </summary>
        /// <param name="Commodity_ID"></param>
        private void GetOldDataObject(Int32 Commodity_ID)
        {

            ClsCommodityInfo objCommodityInfo = new ClsCommodityInfo();

            objCommodityInfo.COMMODITY_ID.CurrentValue = Commodity_ID;
            objCommodityInfo.CUSTOMER_ID.CurrentValue = int.Parse(GetCustomerID());
            objCommodityInfo.POLICY_ID.CurrentValue = int.Parse(GetPolicyID());
            objCommodityInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(GetPolicyVersionID());
            string policystatus = GetPolicyStatus();

            if (objProducts.FetchCommodityInfoData(ref objCommodityInfo))
            {
                PopulatePageFromEbixModelObject(this.Page, objCommodityInfo);

                btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objCommodityInfo.IS_ACTIVE.CurrentValue.ToString().Trim());
                hidDESTINATION_STATE.Value = objCommodityInfo.DESTINATION_STATE.CurrentValue.ToString();
                hidORIGIN_STATE.Value = objCommodityInfo.ORIGIN_STATE.CurrentValue.ToString();
                hidOLD_COMMODITY_NUMBER.Value = objCommodityInfo.COMMODITY_NUMBER.CurrentValue.ToString();                
                txtCOMMODITY_NUMBER.Text = objCommodityInfo.COMMODITY_NUMBER.CurrentValue.ToString();
                base.SetPageModelObject(objCommodityInfo);
                //itrack no 867
                string originalversion = objCommodityInfo.ORIGINAL_VERSION_ID.CurrentValue.ToString();
                if (originalversion != GetPolicyVersionID() && policystatus == "UENDRS" && originalversion != "0")
                {
                    btnActivateDeactivate.Visible = false;
                }

            }//  if (objProducts.FetchCommodityInfoData(ref objCommodityInfo))


            
        }//private void GetOldDataObject(Int32 Commodity_ID)

        /// <summary>
        /// User to Set the Form control (s)'s value Data in the Model info object
        /// </summary>
        /// <param name="ObjCommodityInfo"></param>
        private void GetFormValue(ClsCommodityInfo ObjCommodityInfo)
        {
            ObjCommodityInfo.ORIGINAL_VERSION_ID.CurrentValue = 0;// int.Parse(GetPolicyVersionID());
            if (txtCOMMODITY_NUMBER.Text.ToString().Trim() != "")
                ObjCommodityInfo.COMMODITY_NUMBER.CurrentValue = Convert.ToDouble(txtCOMMODITY_NUMBER.Text);
            else
                ObjCommodityInfo.COMMODITY_NUMBER.CurrentValue = base.GetEbixDoubleDefaultValue();

            if (txtCOMMODITY.Text.ToString().Trim() != "")
                ObjCommodityInfo.COMMODITY.CurrentValue = Convert.ToString(txtCOMMODITY.Text);
            else
                ObjCommodityInfo.COMMODITY.CurrentValue = String.Empty;

            if ((cmbCONVEYANCE_TYPE.SelectedItem != null) && (cmbCONVEYANCE_TYPE.SelectedItem.Text.ToString().Trim() != ""))
                ObjCommodityInfo.CONVEYANCE_TYPE.CurrentValue = Convert.ToInt32(cmbCONVEYANCE_TYPE.SelectedItem.Value);
            else
                ObjCommodityInfo.CONVEYANCE_TYPE.CurrentValue = base.GetEbixIntDefaultValue();

            if (txtDEPARTING_DATE.Text.ToString().Trim() != "")
                ObjCommodityInfo.DEPARTING_DATE.CurrentValue = ConvertToDate(txtDEPARTING_DATE.Text);
            else
                ObjCommodityInfo.DEPARTING_DATE.CurrentValue = ConvertToDate(null);

            #region Commented on 31-March-2011 for I-Track-856
            /*
            if ((cmbORIGIN_COUNTRY.SelectedItem != null) && (cmbORIGIN_COUNTRY.SelectedItem.Text.ToString().Trim() != ""))
                ObjCommodityInfo.ORIGIN_COUNTRY.CurrentValue = Convert.ToInt32(cmbORIGIN_COUNTRY.SelectedItem.Value);
            else
                ObjCommodityInfo.ORIGIN_COUNTRY.CurrentValue = base.GetEbixIntDefaultValue();

            if ((cmbORIGIN_STATE.SelectedItem != null) && (cmbORIGIN_STATE.SelectedItem.Text.ToString().Trim() != ""))
                ObjCommodityInfo.ORIGIN_STATE.CurrentValue = Convert.ToInt32(cmbORIGIN_STATE.SelectedItem.Value);
            else
                ObjCommodityInfo.ORIGIN_STATE.CurrentValue = base.GetEbixIntDefaultValue();
            
            if ((cmbDESTINATION_COUNTRY.SelectedItem != null) && (cmbDESTINATION_COUNTRY.SelectedItem.Text.ToString().Trim() != ""))
                ObjCommodityInfo.DESTINATION_COUNTRY.CurrentValue = Convert.ToInt32(cmbDESTINATION_COUNTRY.SelectedItem.Value);
            else
                ObjCommodityInfo.DESTINATION_COUNTRY.CurrentValue = base.GetEbixIntDefaultValue();

            if ((cmbDESTINATION_STATE.SelectedItem != null) && (cmbDESTINATION_STATE.SelectedItem.Text.ToString().Trim() != ""))
                ObjCommodityInfo.DESTINATION_STATE.CurrentValue = Convert.ToInt32(cmbDESTINATION_STATE.SelectedItem.Value);
            else
                ObjCommodityInfo.DESTINATION_STATE.CurrentValue = base.GetEbixIntDefaultValue();
            */
            #endregion 

            //Added for itrack 856
            if (txtORIGN_COUNTRY.Text.ToString().Trim() != "")
                ObjCommodityInfo.ORIGN_COUNTRY.CurrentValue = Convert.ToString(txtORIGN_COUNTRY.Text);
            else
                ObjCommodityInfo.ORIGN_COUNTRY.CurrentValue = String.Empty;
            if (txtORIGN_STATE.Text.ToString().Trim() != "")
                ObjCommodityInfo.ORIGN_STATE.CurrentValue = Convert.ToString(txtORIGN_STATE.Text);
            else
                ObjCommodityInfo.ORIGN_STATE.CurrentValue = String.Empty;
            if (txtDEST_COUNTRY.Text.ToString().Trim() != "")
                ObjCommodityInfo.DEST_COUNTRY.CurrentValue = Convert.ToString(txtDEST_COUNTRY.Text);
            else
                ObjCommodityInfo.DEST_COUNTRY.CurrentValue = String.Empty;

            if (txtDEST_STATE.Text.ToString().Trim() != "")
                ObjCommodityInfo.DEST_STATE.CurrentValue = Convert.ToString(txtDEST_STATE.Text);
            else
                ObjCommodityInfo.DEST_STATE.CurrentValue = String.Empty;

            //Added till here 

            if (txtORIGIN_CITY.Text.ToString().Trim() != "")
                ObjCommodityInfo.ORIGIN_CITY.CurrentValue = Convert.ToString(txtORIGIN_CITY.Text);
            else
                ObjCommodityInfo.ORIGIN_CITY.CurrentValue = String.Empty;


            if (txtDESTINATION_CITY.Text.ToString().Trim() != "")
                ObjCommodityInfo.DESTINATION_CITY.CurrentValue = Convert.ToString(txtDESTINATION_CITY.Text);
            else
                ObjCommodityInfo.DESTINATION_CITY.CurrentValue = String.Empty;

            if (txtREMARKS.Text.ToString().Trim() != "")
                ObjCommodityInfo.REMARKS.CurrentValue = Convert.ToString(txtREMARKS.Text);
            else
                ObjCommodityInfo.REMARKS.CurrentValue = String.Empty;


            if (cmbCO_APPLICANT_ID.SelectedValue != null)
                ObjCommodityInfo.CO_APPLICANT_ID.CurrentValue = Convert.ToInt32(cmbCO_APPLICANT_ID.SelectedValue);
            else
                ObjCommodityInfo.CO_APPLICANT_ID.CurrentValue = base.GetEbixIntDefaultValue();

            if (cmbExceeded_Premium.SelectedValue != "")
                ObjCommodityInfo.EXCEEDED_PREMIUM.CurrentValue = int.Parse(cmbExceeded_Premium.SelectedValue);
            else
                ObjCommodityInfo.EXCEEDED_PREMIUM.CurrentValue = GetEbixIntDefaultValue();
            
        }//private void GetFormValue(ClsCommodityInfo ObjCommodityInfo)

        /// <summary>
        /// Use to fire while save and Update 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int intRetval;
            strRowId = hidCommodity_ID.Value;
            ClsCommodityInfo ObjCommodityInfo;
            try
            {
                //For The Save Case                
                if (strRowId.ToUpper().Equals("NEW"))
                {

                    ObjCommodityInfo = new ClsCommodityInfo();
                    this.GetFormValue(ObjCommodityInfo);

                    ObjCommodityInfo.CUSTOMER_ID.CurrentValue = int.Parse(GetCustomerID());
                    ObjCommodityInfo.POLICY_ID.CurrentValue = int.Parse(GetPolicyID());
                    ObjCommodityInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(GetPolicyVersionID());

                    ObjCommodityInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                    ObjCommodityInfo.CREATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now);
                    ObjCommodityInfo.IS_ACTIVE.CurrentValue = "Y";

                    intRetval = objProducts.AddCommodityInfo(ObjCommodityInfo);

                    if (intRetval > 0)
                    {
                        this.GetOldDataObject(ObjCommodityInfo.COMMODITY_ID.CurrentValue);

                        hidCommodity_ID.Value = ObjCommodityInfo.COMMODITY_ID.CurrentValue.ToString();

                        btnActivateDeactivate.Enabled = true;
                        btnDelete.Enabled = true;
                        btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(ObjCommodityInfo.IS_ACTIVE.CurrentValue.Trim());
                        strRowId = hidCommodity_ID.Value;

                        lblMessage.Text = ClsMessages.GetMessage("G", "29");
                        hidFormSaved.Value = "1";
                        base.OpenEndorsementDetails();

                    }
                    else if (intRetval == -2)
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "17");
                        hidFormSaved.Value = "2";
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.GetMessage("G", "20");
                        hidFormSaved.Value = "2";
                    }
                    lblDelete.Visible = false;
                    lblMessage.Visible = true;
                } // if (strRowId.ToUpper().Equals("NEW"))

                else //For The Update cse
                {


                    ObjCommodityInfo = (ClsCommodityInfo)base.GetPageModelObject();
                    this.GetFormValue(ObjCommodityInfo);
                    ObjCommodityInfo.IS_ACTIVE.CurrentValue = "Y"; //hidIS_ACTIVE.Value;
                    ObjCommodityInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                    ObjCommodityInfo.LAST_UPDATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now);

                    intRetval = objProducts.UpdateCommodityInfo(ObjCommodityInfo);

                    if (intRetval > 0)
                    {
                        this.GetOldDataObject(ObjCommodityInfo.COMMODITY_ID.CurrentValue);

                        hidCommodity_ID.Value = ObjCommodityInfo.COMMODITY_ID.CurrentValue.ToString();
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("31");
                        hidFormSaved.Value = "1";
                        base.OpenEndorsementDetails();

                    }
                    else if (intRetval == -2)
                    {
                        lblMessage.Text=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "17");
                        hidFormSaved.Value = "2";
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                        hidFormSaved.Value = "2";
                    }
                    lblDelete.Visible = false;
                    lblMessage.Visible = true;
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
        /// <summary>
        /// Use to fire while click on delele button 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ClsCommodityInfo objCommodityInfo;
            try
            {

                objCommodityInfo = (ClsCommodityInfo)base.GetPageModelObject();

                objCommodityInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());

                int intRetval = objProducts.DeleteCommodityInfo(objCommodityInfo);
                if (intRetval > 0)
                {
                    lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "127");
                    hidFormSaved.Value = "1";
                    hidCommodity_ID.Value = "";
                    trBody.Attributes.Add("style", "display:none");
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
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "2") + " - " + ex.Message ;
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";

            }
        }//protected void btnDelete_Click(object sender, EventArgs e)
        /// <summary>
        /// Use to fire while activate and decativate click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnActivateDeactivate_Click(object sender, EventArgs e)
        {
            ClsCommodityInfo ObjCommodityInfo;

            try
            {
                ObjCommodityInfo = (ClsCommodityInfo)base.GetPageModelObject();

                if (ObjCommodityInfo.IS_ACTIVE.CurrentValue == "Y")
                { ObjCommodityInfo.IS_ACTIVE.CurrentValue = "N"; }
                else
                { ObjCommodityInfo.IS_ACTIVE.CurrentValue = "Y"; }


                ObjCommodityInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                ObjCommodityInfo.LAST_UPDATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now);


                int intRetval = objProducts.ActivateDeactivateCommodityInfo(ObjCommodityInfo);
                if (intRetval > 0)
                {
                    if (ObjCommodityInfo.IS_ACTIVE.CurrentValue == "N")
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "41");
                       // btnActivateDeactivate.Visible = false;
                    }
                    else
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "40");
                    }
                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(ObjCommodityInfo.IS_ACTIVE.CurrentValue.ToString().Trim());
                    hidFormSaved.Value = "1";

                    SetPageModelObject(ObjCommodityInfo);
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

        private void FillVesselNumber()
        {

            String Numbers = GetMaxIdOfVoyageNumber(hidCUSTOMER_ID.Value, hidPOL_ID.Value, hidPOL_VERSION_ID.Value, "0", "1");

            if (Numbers != String.Empty)
            {
                String[] Number = Numbers.Split(',');
                if (Number[0] == "1" && Number[1] == "0" && Number[2] != "0" && Number[3] == "1")
                    txtCOMMODITY_NUMBER.Text = Number[2].ToString();
                else
                    txtCOMMODITY_NUMBER.Text = String.Empty;
            }
        }

        [System.Web.Services.WebMethod]
        public static String GetMaxIdOfVoyageNumber(String CUSTOMER_ID, String POLICY_ID, String POLICY_VERSION_ID, String COMMODITY_NUMBER, String flag)
        {

            CmsWeb.webservices.ClsWebServiceCommon obj = new CmsWeb.webservices.ClsWebServiceCommon();  //Declare the object ob common webservice

            String ReturnValue = String.Empty;
            Int64 VESSELNUMBER = 0;
            Int64 COMMODITYNUMBER = Convert.ToInt64(COMMODITY_NUMBER);

            String CALLEDFOR = "VoyageNo";
            Int64 NUMBER = 0;


            ReturnValue = obj.GetMaxIDofVesselNoandVoyageNo(Convert.ToInt32(CUSTOMER_ID), Convert.ToInt32(POLICY_ID), Convert.ToInt32(POLICY_VERSION_ID), VESSELNUMBER, COMMODITYNUMBER, ref NUMBER, CALLEDFOR, Convert.ToInt32(flag));

            if (ReturnValue != "-1")
                ReturnValue = ReturnValue + "," + COMMODITYNUMBER + "," + NUMBER + "," + flag;
            else
                ReturnValue = String.Empty;

            return ReturnValue;
        }
   }

}

