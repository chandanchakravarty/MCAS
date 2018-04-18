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
using Cms.Model.Maintenance.Claims;
using Cms.Model.Maintenance.Accumulation;
using Cms.Model.Policy;
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher;
using Cms.BusinessLayer.BLClaims;
using Cms.ExceptionPublisher.ExceptionManagement;
using System.Xml;
using Cms.Blcommon;
using Cms.BusinessLayer.BlCommon.Accumulation;
using Cms.BusinessLayer.BlCommon;

namespace Cms.Policies.Aspx
{
    //public partial class AddNatureBusiness : Cms.CmsWeb.cmsbase
    public partial class AddNatureBusiness : Cms.Policies.policiesbase
    {

        #region Page controls declaration
        protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidExtraCover;

        protected Cms.CmsWeb.Controls.CmsButton btnReset;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;
        protected System.Web.UI.WebControls.Label lblMessage;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDETAIL_TYPE_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTYPE_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTRAN_CODE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCRITERIA_DESC;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidBusiness_ID;

        protected System.Web.UI.HtmlControls.HtmlTableRow trbody;
        protected System.Web.UI.HtmlControls.HtmlTableRow trTcode;

        protected System.Web.UI.WebControls.Label capMessages;
       // protected System.Web.UI.WebControls.HyperLink hlkBusiness_Date;
        protected System.Web.UI.WebControls.HyperLink hlkEXPIRATION_DATE;

        private string XmlSchemaFileName = ""; 
        private string XmlFullFilePath = ""; 

        #endregion
       
        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "134_5";

            
            revRetail_Stores.ValidationExpression = aRegExpDoublePositiveNonZero;
            rvRetail_Stores.ErrorMessage = "Please enter numeric value between 0 to 100";
            revRetail_Stores.ErrorMessage = "Please enter valid number";

            revInstallation_Service.ValidationExpression = aRegExpDoublePositiveNonZero;
            rvInstallation_Service.ErrorMessage = "Please enter numeric value between 0 to 100";            
            revInstallation_Service.ErrorMessage = "Please enter valid number";

            revPremise_Installation.ValidationExpression = aRegExpDoublePositiveNonZero;
            rvPremise_Installation.ErrorMessage = "Please enter numeric value between 0 to 100";
            revPremise_Installation.ErrorMessage = "Please enter valid number";

           // revBusiness_Date.ValidationExpression = aRegExpDate;
           // revBusiness_Date.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2077");

            //csvBusiness_Date.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2077");

            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            string _queryValue = (string)Request.QueryString["encQR"];

            string strSysID = GetSystemId();
            if (strSysID == "ALBAUAT")
                strSysID = "ALBA";

            XmlSchemaFileName = "AddNatureBusiness.xml";
            //XmlFullFilePath = Request.PhysicalApplicationPath + "cmsweb/support/PageXml/" + strSysID + "/" + XmlSchemaFileName;
            XmlFullFilePath = Request.PhysicalApplicationPath + "Policies/support/PageXML/" + strSysID + "/" + XmlSchemaFileName;

            btnReset.Attributes.Add("onclick", "javascript:return ResetForm();");
           //hlkBusiness_Date.Attributes.Add("onclick", "fPopCalendar(document.POL_NATURE_OF_BUSINESS.txtBusiness_Date, document.POL_NATURE_OF_BUSINESS.txtBusiness_Date)");
            if (!Page.IsPostBack)
            {
                PopulateDropDown();
                //BI common object
                this.SetErrorMessages();
                if (ClsCommon.IsXMLResourceExists(@Request.PhysicalApplicationPath + "Policies/support/PageXML/" + GetSystemId(), "AddNatureBusiness.xml"))
                {
                    setPageControls(Page, @Request.PhysicalApplicationPath + "/Policies/support/PageXML/" + GetSystemId() + "/AddNatureBusiness.xml");
                }
                ClsNatureBusiness objNature = new ClsNatureBusiness();
                DataSet dsBusiness = objNature.GetBusinessNature(GetCustomerID(),GetPolicyID(),GetPolicyVersionID());
                if (dsBusiness.Tables[0].Rows.Count > 0)
                {
                    hidBusiness_ID.Value = dsBusiness.Tables[0].Rows[0]["BUSINESS_ID"].ToString();
                    LoadData(dsBusiness.Tables[0]);
                }
                else
                {
                    hidBusiness_ID.Value = "NEW";
                }
            }

        }



        private void SetErrorMessages()
        {

            //revBusiness_Date.ValidationExpression = aRegExpDate;
            //revBusiness_Date.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");


        }
        private void LoadData(DataTable dtBusiness)
        {
            cmbBusines_Nature.SelectedValue = dtBusiness.Rows[0]["BUSINESS_NATURE"] == DBNull.Value ? "" : dtBusiness.Rows[0]["BUSINESS_NATURE"].ToString();
            txtPrimary_Operation.Text = dtBusiness.Rows[0]["PRIMARY_OPERATION"] == DBNull.Value ? "" : dtBusiness.Rows[0]["PRIMARY_OPERATION"].ToString();
            //txtBusiness_Date.Text = dtBusiness.Rows[0]["BUSINESS_START_DATE"] == DBNull.Value ? "" : Convert.ToDateTime(dtBusiness.Rows[0]["BUSINESS_START_DATE"].ToString()).ToShortDateString();
            txtBUSINESS_START_DATE.Text = dtBusiness.Rows[0]["BUSINESS_START_DATE"] == DBNull.Value ? "" : dtBusiness.Rows[0]["BUSINESS_START_DATE"].ToString();
            txtOther_Operation.Text = dtBusiness.Rows[0]["OTHER_OPERATION"] == DBNull.Value ? "" : dtBusiness.Rows[0]["OTHER_OPERATION"].ToString();
            txtRetail_Stores.Text = dtBusiness.Rows[0]["RETAIL_STORE"] == DBNull.Value ? "" : dtBusiness.Rows[0]["RETAIL_STORE"].ToString();
            txtInstallation_Service.Text = dtBusiness.Rows[0]["REPAIR_WORK"] == DBNull.Value ? "" : dtBusiness.Rows[0]["REPAIR_WORK"].ToString();
            txtPremise_Installation.Text = dtBusiness.Rows[0]["PREMISES_WORK"] == DBNull.Value ? "" : dtBusiness.Rows[0]["PREMISES_WORK"].ToString(); 
        }


        private void PopulateDropDown()
        {
            try
            {
               
               //DataTable dtPAYOR = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("HBLDTY");
               DataTable dtPAYOR = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("NOB");
               DataView dvPAYOR = dtPAYOR.DefaultView;
               dvPAYOR.Sort = "LookupDesc";
               cmbBusines_Nature.DataSource = dvPAYOR;
               cmbBusines_Nature.DataTextField = "LookupDesc";
               cmbBusines_Nature.DataValueField = "LookupID";
               cmbBusines_Nature.DataBind();
               cmbBusines_Nature.Items.Insert(0, "");


            }
            catch (Exception ex)
            { }
        }
      

        #region "Web Event Handlers"
        /// <summary>
        /// If form is posted back then add entry in database using the BL object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        protected void btnSave_Click(object sender, System.EventArgs e)
        {
            int intReturn = 0;
            intReturn = SaveFormValue();
            if (intReturn > 0)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "29");
                hidFormSaved.Value = "1";
                hidOldData.Value = ClsDefaultValues.GetXmlForPageControls(hidDETAIL_TYPE_ID.Value);

                if (hidBusiness_ID.Value == "NEW")
                    lblMessage.Text = "Information saved successfully";
                else
                    lblMessage.Text = "Information updated successfully";
            }
            else if (intReturn == -1)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "29");
                hidFormSaved.Value = "2";
            }
            else
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "20");
                hidFormSaved.Value = "2";
            }
            lblMessage.Visible = true;


        }

        private int SaveFormValue()
        {
            int intRetVal = 0;
            try
            {
                ClsNatureBusiness objNature = new ClsNatureBusiness();
                ClsNatureBusinessInfo objNatureInfo = getFormValue();

                if (hidBusiness_ID.Value == "NEW")
                {
                    intRetVal = objNature.AddNatureBusiness(objNatureInfo, XmlFullFilePath);
                }
                else
                {
                    objNatureInfo.BUSINESS_ID = int.Parse(hidBusiness_ID.Value);
                    intRetVal = objNature.UpdateNatureBusiness(objNatureInfo, XmlFullFilePath);
                }
                return intRetVal;
            }
            catch
            {
                return -1;
            }
        }

        private ClsNatureBusinessInfo getFormValue()
        {
            ClsNatureBusinessInfo obj = new ClsNatureBusinessInfo();

            obj.CUSTOMER_ID = int.Parse(GetCustomerID());
            obj.POLICY_ID = int.Parse(GetPolicyID());
            obj.POLICY_VERSION_ID = int.Parse(GetPolicyVersionID());
            obj.BUSINESS_NATURE = int.Parse(cmbBusines_Nature.SelectedValue);
            obj.PRIMARY_OPERATION = txtPrimary_Operation.Text;
            //obj.BUSINESS_START_DATE = ConvertToDate(txtBusiness_Date.Text);
            obj.BUSINESS_START_DATE = String.IsNullOrEmpty(txtBUSINESS_START_DATE.Text) ? 0 : int.Parse(txtBUSINESS_START_DATE.Text);

            obj.OTHER_OPERATION = txtOther_Operation.Text;
            //obj.REPAIR_WORK = int.Parse(txtInstallation_Service.Text);
            obj.REPAIR_WORK = String.IsNullOrEmpty(txtInstallation_Service.Text) ? 0 : int.Parse(txtInstallation_Service.Text);
            //obj.PREMISES_WORK = int.Parse(txtPremise_Installation.Text);
            obj.PREMISES_WORK = String.IsNullOrEmpty(txtPremise_Installation.Text) ? 0 : int.Parse(txtPremise_Installation.Text);

            //obj.RETAIL_STORE = int.Parse(txtRetail_Stores.Text);
            obj.RETAIL_STORE = String.IsNullOrEmpty(txtRetail_Stores.Text) ? 0 : int.Parse(txtRetail_Stores.Text);                                 


            return obj;
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
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion
    }
}