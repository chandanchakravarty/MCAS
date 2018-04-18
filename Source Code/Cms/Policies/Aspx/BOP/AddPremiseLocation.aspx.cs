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


namespace Cms.Policies.Aspx.BOP
{
    public partial class AddPremiseLocation : Cms.Policies.policiesbase
    {
        #region Page controls declaration
       
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPremLoc_ID;
       
        private string XmlSchemaFileName = "";
        private string XmlFullFilePath = "";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            NfiBaseCurrency.NumberDecimalDigits = 2;
            txtANN_REVENUE.Attributes.Add("onBlur", "this.value=formatBaseCurrencyAmount(this.value);");
            base.ScreenId = "134_0";
            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            btndelete.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Delete;
            btndelete.PermissionString = gstrSecurityXML;
            string _queryValue = (string)Request.QueryString["encQR"];

            string strSysID = GetSystemId();
            if (strSysID == "ALBAUAT")
                strSysID = "ALBA";

            XmlSchemaFileName = "AddPremiseLocation.xml";
            XmlFullFilePath = Request.PhysicalApplicationPath + "Policies/support/PageXML/" + strSysID + "/" + XmlSchemaFileName;
            btnReset.Attributes.Add("onclick", "javascript:return ResetForm();");
            //if (ClsCommon.IsXMLResourceExists(@Request.PhysicalApplicationPath + "Policies/support/PageXML/" + GetSystemId(), "AddPremiseLocation.xml"))
            //{
            //    setPageControls(Page, @Request.PhysicalApplicationPath + "/Policies/support/PageXML/" + GetSystemId() + "/AddPremiseLocation.xml");
            //}
            if (!Page.IsPostBack)
            {

                

               //BI common object
                if (ClsCommon.IsXMLResourceExists(@Request.PhysicalApplicationPath + "Policies/support/PageXML/" + GetSystemId(), "AddPremiseLocation.xml"))
                {
                    setPageControls(Page, @Request.PhysicalApplicationPath + "/Policies/support/PageXML/" + GetSystemId() + "/AddPremiseLocation.xml");
                }
                FillDropDown();// changed for TFS# 2300
                ClsPremisesInformation objPremises = new ClsPremisesInformation();
                DataSet dsPremises=objPremises.GetPremisesInformation(GetCustomerID(), GetPolicyID(), GetPolicyVersionID());
                //DataSet dsPremises = objPremises.GetPremisesInformation(GetCustomerID(), "22", GetPolicyVersionID(), "22");
               //DataSet dsBusiness = objPremises.g(GetCustomerID(), GetPolicyID(), GetPolicyVersionID());
                //objPremises.G

                if (dsPremises.Tables[0].Rows.Count > 0)
                {
                    hidPremLoc_ID.Value = dsPremises.Tables[0].Rows[0]["LOCATION_ID"].ToString();
                    LoadData(dsPremises.Tables[0]);
                }
                else
                {
                    this.FillLocationNumber();
                    hidPremLoc_ID.Value = "NEW";
                }

            }
        }

        private void FillDropDown()
        {
            
           
            //********************                     
            cmbSTATE.DataSource = Cms.CmsWeb.ClsFetcher.State;
            cmbSTATE.DataTextField = "STATE_NAME";
            cmbSTATE.DataValueField = "STATE_CODE";
            cmbSTATE.DataBind();// changed for TFS# 2300
                       
            
        }

        private void FillLocationNumber()
        {

            String Numbers = GetMaxIdOfLocationNumber(GetCustomerID(), GetPolicyID(),GetPolicyVersionID(), "1");
            if (Numbers != String.Empty)
            {

                txtLOCATION_ID.Text = Numbers;
                txtLOCATION_ID.ReadOnly = true;


            }
        }

        [System.Web.Services.WebMethod]
        public static String GetMaxIdOfLocationNumber(String CUSTOMER_ID, String POL_ID, String POL_VERSION_ID, String LOCATION_NUMBER)
        {
            ClsPremisesInformation objLocation = new ClsPremisesInformation();
            String ReturnValue = String.Empty;
            Int64 NUMBER = Convert.ToInt64(LOCATION_NUMBER);//Convert String Location Number to Long 


            ReturnValue = objLocation.GetMaxIDOfLocationNumber(Convert.ToInt32(CUSTOMER_ID), Convert.ToInt32(POL_ID), Convert.ToInt32(POL_VERSION_ID), ref NUMBER);

            if (ReturnValue != "-1")//Check if the return value is not -1
            {
                //ReturnValue = ReturnValue; //Return with retunvalue ,location number and flag 
            }
            else
            {
                ReturnValue = String.Empty;//Return empty if there is any error or record not found
            }

            return ReturnValue;//return value
        }


        private void LoadData(DataTable dtPremises)
        {
            cmbAREA_LEASED.SelectedValue = dtPremises.Rows[0]["AREA_LEASED"] == DBNull.Value ? "" : dtPremises.Rows[0]["AREA_LEASED"].ToString();

            if ((dtPremises.Rows[0]["STATE"].ToString() != null) && (dtPremises.Rows[0]["STATE"].ToString() != ""))
            {
                cmbSTATE.SelectedValue = dtPremises.Rows[0]["STATE"].ToString();
            }            
            
            cmbINTEREST.SelectedValue = dtPremises.Rows[0]["INTEREST"] == DBNull.Value ? "" : dtPremises.Rows[0]["INTEREST"].ToString();
            txtTOT_AREA.Text = dtPremises.Rows[0]["TOT_AREA"] == DBNull.Value ? "" : dtPremises.Rows[0]["TOT_AREA"].ToString();
           // txtBusiness_Date.Text = dtPremises.Rows[0]["OCC_AREA"] == DBNull.Value ? "" : Convert.ToDateTime(dtPremises.Rows[0]["BUSINESS_START_DATE"].ToString()).ToShortDateString();
            txtOCC_AREA.Text = dtPremises.Rows[0]["OCC_AREA"] == DBNull.Value ? "" : dtPremises.Rows[0]["OCC_AREA"].ToString();
            txtOPEN_AREA.Text = dtPremises.Rows[0]["OPEN_AREA"] == DBNull.Value ? "" : dtPremises.Rows[0]["OPEN_AREA"].ToString();
            txtANN_REVENUE.Text = dtPremises.Rows[0]["ANN_REVENUE"] == DBNull.Value ? "" : dtPremises.Rows[0]["ANN_REVENUE"].ToString();
            txtFL_TM_EMP.Text = dtPremises.Rows[0]["FL_TM_EMP"] == DBNull.Value ? "" : dtPremises.Rows[0]["FL_TM_EMP"].ToString();
            txtPT_TM_EMP.Text = dtPremises.Rows[0]["PT_TM_EMP"] == DBNull.Value ? "" : dtPremises.Rows[0]["PT_TM_EMP"].ToString();
                       
            
            txtCITY.Text = dtPremises.Rows[0]["CITY"] == DBNull.Value ? "" : dtPremises.Rows[0]["CITY"].ToString();

            txtSTREET_ADDR.Text = dtPremises.Rows[0]["STREET_ADDR"] == DBNull.Value ? "" : dtPremises.Rows[0]["STREET_ADDR"].ToString();
            txtBLDNG_ID.Text = dtPremises.Rows[0]["BUILDING"] == DBNull.Value ? "" : dtPremises.Rows[0]["BUILDING"].ToString();
            txtLOCATION_ID.Text = dtPremises.Rows[0]["LOCATION_ID"] == DBNull.Value ? "" : dtPremises.Rows[0]["LOCATION_ID"].ToString();
            txtLOCATION_ID.ReadOnly = true;
            txtCOUNTY.Text = dtPremises.Rows[0]["COUNTY"] == DBNull.Value ? "" : dtPremises.Rows[0]["COUNTY"].ToString();
            txtZIP.Text = dtPremises.Rows[0]["ZIP"] == DBNull.Value ? "" : dtPremises.Rows[0]["ZIP"].ToString();
            

            
        }
        //To save the data on click

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            int intReturn = 0;
            intReturn = SaveFormValue();
            if (intReturn > 0)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "29");
                //hidFormSaved.Value = "1";
                hidOldData.Value = ClsDefaultValues.GetXmlForPageControls(hidDETAIL_TYPE_ID.Value);
               // lblMessage.Text = "Information saved successfully";
                if (hidPremLoc_ID.Value == "NEW")
                    lblMessage.Text = "Information saved successfully";
                else
                    lblMessage.Text = "Information updated successfully";
            }
            else if (intReturn == -1)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "18");
                //hidFormSaved.Value = "2";
            }
            else
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "20");
                //hidFormSaved.Value = "2";

            }
            lblMessage.Visible = true;


        }
        //To save the data on form value
        private int SaveFormValue()
        {
            int intRetVal = 0;
            try
            {
               //ClsNatureBusiness objNature = new ClsNatureBusiness();
                ClsPremisesInformation objPremises = new ClsPremisesInformation();
               //ClsNatureBusinessInfo objNatureInfo = getFormValue();

                ClsPremiseLocationInfo objMPremise = getFormValue();

                if (hidPremLoc_ID.Value == "NEW")
                {
                   // intRetVal = objNature.AddNatureBusiness(objNatureInfo, XmlFullFilePath);
                    intRetVal = objPremises.AddPremisesLocation(objMPremise, XmlFullFilePath);
                }
                else
                {
                    //objNatureInfo.BUSINESS_ID = int.Parse(hidBusiness_ID.Value);
                    objMPremise.PREMLOC_ID = int.Parse(hidPremLoc_ID.Value);
                   // hidPremLoc_ID = int.Parse(hidPremLoc_ID.Value);
                    intRetVal = objPremises.UpdatePremisesLocation(objMPremise, XmlFullFilePath);
                }
                return intRetVal;
            }
            catch(Exception ex)
            {
                return -1;
            }
        }
        // To delete the data on form value
        protected void btndelete_Click(object sender, EventArgs e)
        {



            int intReturn;
            int intPremLocId = int.Parse(hidPremLoc_ID.Value);
			//ClsAgencyInfo objAgencyInfo = GetFormValue();
            ClsPremisesInformation objPremises = new ClsPremisesInformation();
            //ClsNatureBusinessInfo objNatureInfo = getFormValue();

            ClsPremiseLocationInfo objMPremise = getFormValue();
            objMPremise.PREMLOC_ID = intPremLocId;
            intReturn=objPremises.DeletePremisesLocation(objMPremise, XmlFullFilePath);
            if (intReturn > 0)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "29");
                //hidFormSaved.Value = "1";
                hidOldData.Value = ClsDefaultValues.GetXmlForPageControls(hidDETAIL_TYPE_ID.Value);
                lblMessage.Text = "Record Deleted successfully";
                Resetcontrols();
            }
            else if (intReturn == -1)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "18");
               // hidFormSaved.Value = "2";
            }
            else
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "20");
               // hidFormSaved.Value = "2";
                lblMessage.Text = "Record Deleted successfully";
            }
            lblMessage.Visible = true;

        }
        private void Resetcontrols()
        {

        }
        // To get the controls values on Model objects
        private ClsPremiseLocationInfo getFormValue()
        {
           // ClsNatureBusinessInfo obj = new ClsNatureBusinessInfo();
            ClsPremiseLocationInfo obj = new ClsPremiseLocationInfo();

            obj.CUSTOMER_ID = int.Parse(GetCustomerID());
            obj.POLICY_ID = int.Parse(GetPolicyID());
            obj.POLICY_VERSION_ID = int.Parse(GetPolicyVersionID());
            obj.AREA_LEASED = cmbAREA_LEASED.SelectedValue;
            obj.INTEREST = cmbINTEREST.SelectedValue;
            obj.ZIP = txtZIP.Text;
            obj.COUNTY = txtCOUNTY.Text;
            obj.TOT_AREA = String.IsNullOrEmpty(txtTOT_AREA.Text) ? 0 : decimal.Parse(txtTOT_AREA.Text);
            obj.STATE = cmbSTATE.SelectedValue;
            //obj.OPEN_AREA = decimal.Parse(txtTOT_AREA.Text);
            //txtTOT_AREA.Text.

            obj.OPEN_AREA = String.IsNullOrEmpty(txtOPEN_AREA.Text) ? 0 : decimal.Parse(txtOPEN_AREA.Text);
                
               // DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["POLICY_ID"].ToString());


            obj.CITY = txtCITY.Text;
            obj.OCC_AREA = String.IsNullOrEmpty(txtOCC_AREA.Text) ? 0 : decimal.Parse(txtOCC_AREA.Text);
            obj.STREET_ADDR = txtSTREET_ADDR.Text;
            //obj.ANN_REVENUE = String.IsNullOrEmpty(txtANN_REVENUE.Text) ? 0 : decimal.Parse(txtANN_REVENUE.Text);
            obj.ANN_REVENUE = String.IsNullOrEmpty(txtANN_REVENUE.Text) ? 0 : decimal.Parse(txtANN_REVENUE.Text, NfiBaseCurrency);
            //Double.Parse(txtPERCENT_BREAKDOWN8.Text, NfiBaseCurrency);
            obj.BUILDING = String.IsNullOrEmpty(txtBLDNG_ID.Text) ? 0 : int.Parse(txtBLDNG_ID.Text);
            obj.LOCATION_ID = String.IsNullOrEmpty(txtLOCATION_ID.Text) ? 0 : int.Parse(txtLOCATION_ID.Text);
            obj.FL_TM_EMP = txtFL_TM_EMP.Text;
            obj.PT_TM_EMP = txtPT_TM_EMP.Text;
            
            return obj;
        }


        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btndelete.Click +=new EventHandler(this.btndelete_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

      

       
    }
}
