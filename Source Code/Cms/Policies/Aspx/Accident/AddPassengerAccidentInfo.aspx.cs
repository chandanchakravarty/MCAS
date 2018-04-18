//------------------------------------------------------------------------------
// Created by Pradeep Kushwaha on 28-05-2010
// 
//
//
//------------------------------------------------------------------------------
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
using Cms.Blapplication;
using System.Resources;
using Cms.Model.Policy;
using Cms.Model.Policy.Accident;

namespace Cms.Policies.Aspx.Accident
{
    public partial class AddPassengerAccidentInfo : policiesbase
    {
        ResourceManager objresource;
        //Declare Dropdown For Multiple Deductible dropdown

        private static String strRowId = String.Empty;
        ClsProducts objProduct = new ClsProducts();
        int PRO_RISK_ID;
        string CalledFrom;
        public string PAGEFROM;
        public string FIRSTTAB;//in case of quickapp
        #region Control Declaration
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPERSONAL_ACCIDENT_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCALLED_FROM;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTAB_TITLES;
        protected System.Web.UI.WebControls.Label lblDelete;
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.WebControls.HyperLink hlkSTART_DATE;
        protected System.Web.UI.WebControls.HyperLink hlkEND_DATE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revSTART_DATE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revEND_DATE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revNUMBER_OF_PASSENGERS;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvSTART_DATE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvEND_DATE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvNUMBER_OF_PASSENGERS;
        protected System.Web.UI.WebControls.TextBox txtSTART_DATE;
        protected System.Web.UI.WebControls.TextBox txtEND_DATE;
        protected System.Web.UI.WebControls.TextBox txtNUMBER_OF_PASSENGERS;
        protected System.Web.UI.WebControls.Label capSTART_DATE;
        protected System.Web.UI.WebControls.Label capEND_DATE;
        protected System.Web.UI.WebControls.Label capNUMBER_OF_PASSENGERS;
        protected System.Web.UI.WebControls.Label lblFormLoadMessage;
        protected System.Web.UI.WebControls.Label lblManHeader;
        protected System.Web.UI.HtmlControls.HtmlGenericControl tbody;

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

            ClsMessages.SetCustomizedXml(GetLanguageCode());
            if (Request.QueryString["CalledFrom"] != null && Request.QueryString["CalledFrom"].ToString() != "")
            {
                CalledFrom = Request.QueryString["CalledFrom"].ToString();
                hidCALLED_FROM.Value = CalledFrom;
            }
            if (Request.QueryString["PAGEFROM"] != null)
                PAGEFROM = Request.QueryString["PAGEFROM"].ToString();

            FIRSTTAB = ClsMessages.GetTabTitles("499", "TabCtl");
            base.ScreenId = PASSENGERS_PERSONAL_ACCIDENTscreenId.INSURED_INFORMATION;
            btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1333");
            hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCtl");

            btnSave.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;


            btnDelete.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Delete;
            btnDelete.PermissionString = gstrSecurityXML;

            btnReset.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnActivateDeactivate.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnActivateDeactivate.PermissionString = gstrSecurityXML;

            hlkSTART_DATE.Attributes.Add("OnClick", "fPopCalendar(document.POL_PASSENGERS_PERSONAL_ACCIDENT_INFO.txtSTART_DATE,document.POL_PASSENGERS_PERSONAL_ACCIDENT_INFO.txtSTART_DATE)"); //Javascript Implementation for Calender				
            hlkEND_DATE.Attributes.Add("OnClick", "fPopCalendar(document.POL_PASSENGERS_PERSONAL_ACCIDENT_INFO.txtEND_DATE,document.POL_PASSENGERS_PERSONAL_ACCIDENT_INFO.txtEND_DATE)");


            objresource = new System.Resources.ResourceManager("Cms.Policies.Aspx.Accident.AddPassengerAccidentInfo", System.Reflection.Assembly.GetExecutingAssembly());

            btnReset.Attributes.Add("onclick", "javascript: return ResetTheForm();");
            hidCUSTOMER_ID.Value = GetCustomerID();
            hidPOLICY_ID.Value = GetPolicyID();
            hidPOLICY_VERSION_ID.Value = GetPolicyVersionID();
            if (!IsPostBack)
            {
                this.SetCaptions();
                this.SetErrorMessages();
                this.PopulateCoApplicant();
                this.BindExceededPremium();
                if (Request.QueryString["CUSTOMER_ID"] != null && Request.QueryString["CUSTOMER_ID"].ToString() != "")
                    hidCUSTOMER_ID.Value = Request.QueryString["CUSTOMER_ID"].ToString();


                if (Request.QueryString["POL_ID"] != null && Request.QueryString["POL_ID"].ToString() != "")
                    hidPOLICY_ID.Value = Request.QueryString["POL_ID"].ToString();


                if (Request.QueryString["POL_VERSION_ID"] != null && Request.QueryString["POL_VERSION_ID"].ToString() != "")
                    hidPOLICY_VERSION_ID.Value = Request.QueryString["POL_VERSION_ID"].ToString();


                if (Request.QueryString["PERSONAL_ACCIDENT_ID"] != null && Request.QueryString["PERSONAL_ACCIDENT_ID"].ToString() != "" && Request.QueryString["PERSONAL_ACCIDENT_ID"].ToString() != "NEW")
                {
                    hidPERSONAL_ACCIDENT_ID.Value = Request.QueryString["PERSONAL_ACCIDENT_ID"].ToString();

                    this.GetOldDataObject(Convert.ToInt32(hidPERSONAL_ACCIDENT_ID.Value));
                    btnDelete.Enabled = true;
                    btnActivateDeactivate.Enabled = true;
                    btnDelete.Visible = true;
                    strRowId = hidPERSONAL_ACCIDENT_ID.Value;

                }
                else
                {

                    hidPERSONAL_ACCIDENT_ID.Value = "NEW";
                    strRowId = "NEW";
                    btnActivateDeactivate.Enabled = false;
                    btnDelete.Enabled = false;


                }

            }
            lblDelete.Text = "";

            setEndosementNO();

        }
        private void SetErrorMessages()
        {
            ClsMessages.SetCustomizedXml(GetLanguageCode());//added by Lalit,May 23 2011.itrack # 948

            revSTART_DATE.ValidationExpression = aRegExpDate;
            revSTART_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "2");
            rfvSTART_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1");

            revEND_DATE.ValidationExpression = aRegExpDate;
            revEND_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "4");
            rfvEND_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "3");

            revNUMBER_OF_PASSENGERS.ValidationExpression = aRegExpInteger;
            revNUMBER_OF_PASSENGERS.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "6");
            rfvNUMBER_OF_PASSENGERS.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
            cpvEND_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("733");
            rfvCO_APPLICANT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1179");


        }

        private void BindExceededPremium()
        {
            cmbExceeded_Premium.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("yesno");
            cmbExceeded_Premium.DataTextField = "LookupDesc";
            cmbExceeded_Premium.DataValueField = "LookupID";
            cmbExceeded_Premium.DataBind();
        }

        private void PopulateCoApplicant()
        {
            ClsPassengerAccidentInfo PassengerAccidentInfo = new ClsPassengerAccidentInfo();
            // changes by praveer for itrack no 900 
            DataSet ds = PassengerAccidentInfo.FetchApplicants(Convert.ToInt32(hidCUSTOMER_ID.Value), Convert.ToInt32(hidPOLICY_VERSION_ID.Value), Convert.ToInt32(hidPOLICY_ID.Value));
            cmbCO_APPLICANT_ID.DataSource = ds;
            cmbCO_APPLICANT_ID.DataTextField = "APPLICANT_NAME";
            cmbCO_APPLICANT_ID.DataValueField = "APPLICANT_ID";
            cmbCO_APPLICANT_ID.DataBind();
            cmbCO_APPLICANT_ID.Items.Insert(0, "");
            // changes by praveer for itrack no 900(for personal accident for passenges)
            if (hidCALLED_FROM.Value == "PAPEACC" && GetTransaction_Type().Trim() == MASTER_POLICY && GetPolicyStatus().Trim().ToUpper() == POLICY_STATUS_UNDER_ENDORSEMENT)
            {
                if (GetEndorsementCoApplicant().Trim() != "" && GetEndorsementCoApplicant().Trim() != null)
                {
                    cmbCO_APPLICANT_ID.SelectedValue = GetEndorsementCoApplicant().Trim();
                }
            }

            else
            {
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
        }
        private void GetOldDataObject(int PERSONAL_ACCIDENT_ID)
        {
            ClsPassengerAccidentInfo objPassengerAccidentInfo = new ClsPassengerAccidentInfo();

            objPassengerAccidentInfo.PERSONAL_ACCIDENT_ID.CurrentValue = PERSONAL_ACCIDENT_ID;
            objPassengerAccidentInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCUSTOMER_ID.Value);
            objPassengerAccidentInfo.POLICY_ID.CurrentValue = int.Parse(hidPOLICY_ID.Value);
            objPassengerAccidentInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPOLICY_VERSION_ID.Value);
            string policystatus = GetPolicyStatus();


            if (objProduct.FetchPassengerAccidentInfo(ref objPassengerAccidentInfo))
            {
                PopulatePageFromEbixModelObject(this.Page, objPassengerAccidentInfo);

                txtNUMBER_OF_PASSENGERS.Text = objPassengerAccidentInfo.NUMBER_OF_PASSENGERS.CurrentValue.ToString();

                btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objPassengerAccidentInfo.IS_ACTIVE.CurrentValue.ToString().Trim());
                //itrack no 867
                string originalversion = objPassengerAccidentInfo.ORIGINAL_VERSION_ID.CurrentValue.ToString();
                if (originalversion != GetPolicyVersionID() && policystatus == "UENDRS" && originalversion != "0")
                {
                    btnActivateDeactivate.Visible = false;
                }
                SetCustomSecurityxml(objPassengerAccidentInfo.CO_APPLICANT_ID.CurrentValue.ToString(), "");
                base.SetPageModelObject(objPassengerAccidentInfo);
                hidCO_APPLICANT_ID.Value = objPassengerAccidentInfo.CO_APPLICANT_ID.CurrentValue.ToString();
                lblRISK_ORIGINAL_ENDORSEMENT_NO.Text = objPassengerAccidentInfo.RISK_ORIGINAL_ENDORSEMENT_NO.CurrentValue.ToString();

                if (objPassengerAccidentInfo.RISK_ORIGINAL_ENDORSEMENT_NO.CurrentValue.ToString() == "0")
                {
                    capRISK_ORIGINAL_ENDORSEMENT_NO.Visible = false;
                    lblRISK_ORIGINAL_ENDORSEMENT_NO.Visible = false;
                    lblRISK_ORIGINAL_ENDORSEMENT_NO.Text = "";
                }
                else
                {
                    capRISK_ORIGINAL_ENDORSEMENT_NO.Visible = true;
                    lblRISK_ORIGINAL_ENDORSEMENT_NO.Visible = true;
                }


            }// if (objProduct.FetchProductLocationInfo(ref objProductLocationInfo))

        }
        #region Set Captions from resource file for multilingual
        private void SetCaptions()
        {

            capSTART_DATE.Text = objresource.GetString("txtSTART_DATE");
            capEND_DATE.Text = objresource.GetString("txtEND_DATE");
            capNUMBER_OF_PASSENGERS.Text = objresource.GetString("txtNUMBER_OF_PASSENGERS");
            lblManHeader.Text = objresource.GetString("lblManHeader");
            capCoApplicant.Text = objresource.GetString("capCoApplicant");
            capRISK_ORIGINAL_ENDORSEMENT_NO.Text = objresource.GetString("lblRISK_ORIGINAL_ENDORSEMENT_NO");
            capExceededPremium.Text = objresource.GetString("cmbExceeded_Premium");

        }//Set caption from resource file
        #endregion

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ClsMessages.SetCustomizedXml(GetLanguageCode());
            ClsPassengerAccidentInfo objPassengerAccidentInfo;
            int retunvalue = 0;
            try
            {
                String ConfirmValue = String.Empty;

                if (hidPERSONAL_ACCIDENT_ID.Value != "")
                {

                    objPassengerAccidentInfo = (ClsPassengerAccidentInfo)base.GetPageModelObject();
                    objPassengerAccidentInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());

                    objPassengerAccidentInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCUSTOMER_ID.Value);
                    objPassengerAccidentInfo.POLICY_ID.CurrentValue = int.Parse(hidPOLICY_ID.Value);
                    objPassengerAccidentInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPOLICY_VERSION_ID.Value);
                    objPassengerAccidentInfo.PERSONAL_ACCIDENT_ID.CurrentValue = int.Parse(hidPERSONAL_ACCIDENT_ID.Value);

                    retunvalue = objProduct.DeletePassengerAccidentInfo(objPassengerAccidentInfo);
                    if (retunvalue > 0)
                    {
                        lblDelete.Text = ClsMessages.FetchGeneralMessage("127");
                        tbody.Attributes.Add("style", "display:none");
                        hidFormSaved.Value = "1";
                        hidPERSONAL_ACCIDENT_ID.Value = "";

                    }
                    else if (retunvalue == -1)
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("128");
                        hidFormSaved.Value = "2";
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.FetchGeneralMessage("128");
                hidFormSaved.Value = "2";
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
        }

        private void GetFormValue(ClsPassengerAccidentInfo objPassengerAccidentInfo)
        {
            objPassengerAccidentInfo.ORIGINAL_VERSION_ID.CurrentValue = 0;// int.Parse(GetPolicyVersionID());
            if (hidPERSONAL_ACCIDENT_ID.Value != "0")
            {
                strRowId = hidPERSONAL_ACCIDENT_ID.Value;
            }
            else { strRowId = "NEW"; }

            if (txtSTART_DATE.Text != "")
                objPassengerAccidentInfo.START_DATE.CurrentValue = ConvertToDate(txtSTART_DATE.Text);
            else
                objPassengerAccidentInfo.START_DATE.CurrentValue = ConvertToDate(null);
            if (txtEND_DATE.Text != "")
                objPassengerAccidentInfo.END_DATE.CurrentValue = ConvertToDate(txtEND_DATE.Text);
            else
                objPassengerAccidentInfo.END_DATE.CurrentValue = ConvertToDate(null);


            if (txtNUMBER_OF_PASSENGERS.Text != "")
                objPassengerAccidentInfo.NUMBER_OF_PASSENGERS.CurrentValue = double.Parse(txtNUMBER_OF_PASSENGERS.Text);
            else
                objPassengerAccidentInfo.NUMBER_OF_PASSENGERS.CurrentValue = base.GetEbixDoubleDefaultValue();

            if (cmbCO_APPLICANT_ID.SelectedValue != null)
                objPassengerAccidentInfo.CO_APPLICANT_ID.CurrentValue = Convert.ToInt32(cmbCO_APPLICANT_ID.SelectedValue);
            else
                objPassengerAccidentInfo.CO_APPLICANT_ID.CurrentValue = base.GetEbixIntDefaultValue();

            if (lblRISK_ORIGINAL_ENDORSEMENT_NO.Text.Trim() != "")
            {
                objPassengerAccidentInfo.RISK_ORIGINAL_ENDORSEMENT_NO.CurrentValue = int.Parse(lblRISK_ORIGINAL_ENDORSEMENT_NO.Text);
            }
            //lblENDORSEMENT_NO.Text
            if (cmbExceeded_Premium.SelectedValue != "")
                objPassengerAccidentInfo.EXCEEDED_PREMIUM.CurrentValue = int.Parse(cmbExceeded_Premium.SelectedValue);
            else
                objPassengerAccidentInfo.EXCEEDED_PREMIUM.CurrentValue = GetEbixIntDefaultValue();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ClsMessages.SetCustomizedXml(GetLanguageCode());
            int returnvalue = 0;
            ClsPassengerAccidentInfo objPassengerAccidentInfo;
            try
            {

                if (strRowId.ToUpper().Trim().Equals("NEW"))
                { //Add New Record

                    objPassengerAccidentInfo = new ClsPassengerAccidentInfo();
                    this.GetFormValue(objPassengerAccidentInfo);

                    if (!SetCustomSecurityxml(objPassengerAccidentInfo.CO_APPLICANT_ID.CurrentValue.ToString(), "SAVE"))
                        return;
                    objPassengerAccidentInfo.CUSTOMER_ID.CurrentValue = Convert.ToInt32(hidCUSTOMER_ID.Value);
                    objPassengerAccidentInfo.POLICY_ID.CurrentValue = Convert.ToInt32(hidPOLICY_ID.Value);
                    objPassengerAccidentInfo.POLICY_VERSION_ID.CurrentValue = Convert.ToInt32(hidPOLICY_VERSION_ID.Value);

                    objPassengerAccidentInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                    objPassengerAccidentInfo.CREATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now);
                    objPassengerAccidentInfo.IS_ACTIVE.CurrentValue = "Y";

                    returnvalue = objProduct.AddPassengerAccidentInfo(objPassengerAccidentInfo);
                    if (returnvalue > 0)
                    {
                        this.GetOldDataObject(objPassengerAccidentInfo.PERSONAL_ACCIDENT_ID.CurrentValue);
                        hidPERSONAL_ACCIDENT_ID.Value = Convert.ToString(objPassengerAccidentInfo.PERSONAL_ACCIDENT_ID.CurrentValue);
                        hidFormSaved.Value = "1";
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("29");
                        btnActivateDeactivate.Enabled = true;
                        btnDelete.Enabled = true;
                        btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objPassengerAccidentInfo.IS_ACTIVE.CurrentValue.Trim());
                        strRowId = hidPERSONAL_ACCIDENT_ID.Value;
                    }
                    else if (returnvalue == -3)
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1603");
                        hidFormSaved.Value = "2";
                    }
                    else if (returnvalue == -1)
                    {
                        hidFormSaved.Value = "2";
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("18");
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                        hidFormSaved.Value = "2";

                    }


                }
                else
                {  //Update

                    objPassengerAccidentInfo = (ClsPassengerAccidentInfo)base.GetPageModelObject();
                    this.GetFormValue(objPassengerAccidentInfo);
                    if (!SetCustomSecurityxml(objPassengerAccidentInfo.CO_APPLICANT_ID.CurrentValue.ToString(), "SAVE"))
                        return;

                    objPassengerAccidentInfo.IS_ACTIVE.CurrentValue = "Y";
                    objPassengerAccidentInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                    objPassengerAccidentInfo.LAST_UPDATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now);


                    returnvalue = objProduct.UpdatePassengerAccidentInfo(objPassengerAccidentInfo);  //Call Business Layer Function for Update

                    if (returnvalue > 0)
                    {
                        hidFormSaved.Value = "1";
                        hidPERSONAL_ACCIDENT_ID.Value = objPassengerAccidentInfo.PERSONAL_ACCIDENT_ID.CurrentValue.ToString();
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("31");
                        hidCO_APPLICANT_ID.Value = objPassengerAccidentInfo.CO_APPLICANT_ID.CurrentValue.ToString();
                    }
                    else if (returnvalue == -3)
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1603");
                        hidFormSaved.Value = "2";
                    }
                    else if (returnvalue == -1)
                    {
                        hidFormSaved.Value = "2";
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("18");

                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                        hidFormSaved.Value = "2";

                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.FetchGeneralMessage("21") + ":-" + ex.Message;
                hidFormSaved.Value = "2";
            }
            lblDelete.Text = "";
        }

        protected void btnActivateDeactivate_Click(object sender, EventArgs e)
        {
            ClsMessages.SetCustomizedXml(GetLanguageCode());
            int retunvalue = 0;
            ClsPassengerAccidentInfo objPassengerAccidentInfo;
            try
            {
                if (int.TryParse(hidPERSONAL_ACCIDENT_ID.Value, out PRO_RISK_ID))
                {
                    objPassengerAccidentInfo = (ClsPassengerAccidentInfo)base.GetPageModelObject();
                    if (objPassengerAccidentInfo.IS_ACTIVE.CurrentValue == "Y")
                        objPassengerAccidentInfo.IS_ACTIVE.CurrentValue = "N";
                    else
                        objPassengerAccidentInfo.IS_ACTIVE.CurrentValue = "Y";

                    objPassengerAccidentInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                    objPassengerAccidentInfo.LAST_UPDATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now);
                    retunvalue = objProduct.ActivateDeactivatePassengerAccidentInfo(objPassengerAccidentInfo);
                    if (retunvalue > 0)
                    {
                        if (objPassengerAccidentInfo.IS_ACTIVE.CurrentValue == "N")
                        {
                            lblMessage.Text = ClsMessages.FetchGeneralMessage("9");
                            // itrack no 867
                            // btnActivateDeactivate.Visible = false;
                        }
                        else if (objPassengerAccidentInfo.IS_ACTIVE.CurrentValue == "Y")
                        {
                            lblMessage.Text = ClsMessages.FetchGeneralMessage("7");
                        }
                        hidFormSaved.Value = "1";
                        btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objPassengerAccidentInfo.IS_ACTIVE.CurrentValue.ToString().Trim());
                        SetPageModelObject(objPassengerAccidentInfo);
                    }
                }
            }
            catch { }
        }
        //master policy imlimentation
        private bool SetCustomSecurityxml(string CO_APP_ID, string CalledFrom)
        {
            bool Valid = true;
            if (hidCALLED_FROM.Value != "PAPEACC") //for personal accident for passenges
                return Valid;

            if (GetTransaction_Type().Trim() == MASTER_POLICY)
                if (CalledFrom.ToUpper() == "SAVE" && GetPolicyStatus().Trim().ToUpper() == POLICY_STATUS_UNDER_ENDORSEMENT)
                {
                    if (CO_APP_ID != GetEndorsementCoApplicant().Trim())
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("1876");
                        Valid = false;
                    }
                }
                else
                {
                    string SecurityXml = base.CustomSecurityXml(CO_APP_ID);
                    btnSave.PermissionString = SecurityXml;
                    btnDelete.PermissionString = SecurityXml;
                    btnReset.PermissionString = SecurityXml;
                    btnActivateDeactivate.PermissionString = SecurityXml;
                }
            return Valid;
        }
        private void setEndosementNO()
        {
            if (hidCALLED_FROM.Value == "PAPEACC")
            {
                ClsCommon objCommon = new ClsCommon();
                DataSet ds_process = objCommon.GetPolicy_ProcessDetails(Convert.ToInt32(hidCUSTOMER_ID.Value),
                   Convert.ToInt32(hidPOLICY_ID.Value),
                   Convert.ToInt32(hidPOLICY_VERSION_ID.Value));
                if (ds_process != null && ds_process.Tables.Count > 0 && ds_process.Tables[0].Rows.Count > 0)
                {
                    if (ds_process.Tables[0].Rows[0]["PROCESS_ID"].ToString() == "14" || ds_process.Tables[0].Rows[0]["PROCESS_ID"].ToString() == "3")
                    {
                        //if (GetPolicyStatus().ToString().Contains("END"))
                        // {
                        if (hidPERSONAL_ACCIDENT_ID.Value.Trim().ToUpper() == "NEW")
                        {
                            capRISK_ORIGINAL_ENDORSEMENT_NO.Visible = true;
                            lblRISK_ORIGINAL_ENDORSEMENT_NO.Visible = true;

                            ClsGeneralInformation objGenralInfo = new ClsGeneralInformation();
                            DataSet DsPolicy = objGenralInfo.GetPolicyEndorsementDetails(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value));
                            if (DsPolicy != null && DsPolicy.Tables.Count > 0 && DsPolicy.Tables[0].Rows.Count > 0)
                            {
                                lblRISK_ORIGINAL_ENDORSEMENT_NO.Text = DsPolicy.Tables[0].Rows[0]["ENDORSEMENT_NO"].ToString();
                            }

                            //lblRISK_ORIGINAL_ENDORSEMENT_NO.Text = GetEndorsementNo().ToString();
                        }
                    }
                }

            }
        }
    }

}
