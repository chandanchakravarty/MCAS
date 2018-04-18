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
using Cms.Model.Policy;

namespace Cms.Policies.Aspx
{
    public partial class AddRemunerationDetails : Cms.Policies.policiesbase
    {  
        #region Local form variable
        ClsPolicyRemunerationInfo objnewPolicyRemunerationInfo = new ClsPolicyRemunerationInfo();
        ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();
        System.Resources.ResourceManager Objresources;       
        DataTable dtCo_Applicant;
        int CustomerId;
        int PolicyId;
        int PolicyVersionId;
        string TransactionType;
        string CommissionType;      
        DateTime EFFECTIVEDATE;
        //string agencyid;
        private string strRowId = "";
        public static int flag = 0;        
        #endregion
        
        protected void Page_Load(object sender, EventArgs e)
        {           
            try
            {
                base.ScreenId = "224_52";
                #region setting security Xml

                btnSave.CmsButtonClass = CmsButtonType.Write;
                btnSave.PermissionString = gstrSecurityXML;

                btnDelete.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Delete;
                btnDelete.PermissionString = gstrSecurityXML;

                #endregion
                Ajax.Utility.RegisterTypeForAjax(typeof(AddRemunerationDetails));

                Objresources = new System.Resources.ResourceManager("Cms.Policies.Aspx.AddRemunerationDetails", System.Reflection.Assembly.GetExecutingAssembly());

                if (Request.QueryString["CUSTOMER_ID"] != null && Request.QueryString["CUSTOMER_ID"].ToString() != "")
                    CustomerId = int.Parse(Request.QueryString["CUSTOMER_ID"].ToString());
                else
                    CustomerId = int.Parse(GetCustomerID());

                if (Request.QueryString["POLICY_ID"] != null && Request.QueryString["POLICY_ID"].ToString() != "")
                    PolicyId= int.Parse(Request.QueryString["POLICY_ID"].ToString());
                else
                    PolicyId = int.Parse(GetPolicyID());
                //hidPOLICY_ID.Value = GetPolicyID();
                if (Request.QueryString["POLICY_VERSION_ID"] != null && Request.QueryString["POLICY_VERSION_ID"].ToString() != "")
                    PolicyVersionId = int.Parse(Request.QueryString["POLICY_VERSION_ID"].ToString());
                else
                    PolicyVersionId = int.Parse(GetPolicyVersionID());

                //CustomerId = int.Parse(GetCustomerID());
                //PolicyId = int.Parse(GetPolicyID());
                //PolicyVersionId = int.Parse(GetPolicyVersionID());
                TransactionType = GetTransaction_Type();
               // txtCOMMISSION_PERCENT.Attributes.Add("onblur", "javascript:this.value=formatRate(this.value,2)");              
                cmbCOMMISSION_TYPE.Attributes.Add("onChange", "javascript:FillAgency();");
                if (!IsPostBack)
                {
                    FillAgency("");
                   // getdate();
                    SetCaption();
                    LoadDropDown();
                    SetErrorMessage();
                    string Pol_l_DefaultValues = PolicyLabelCommission();
                    if (Pol_l_DefaultValues.Contains("^"))
                    {
                        numberFormatInfo.PercentDecimalDigits = 4;
                        string[] pol_values = Pol_l_DefaultValues.Split('^');
                        txtPOLICY_LEVEL_COMMISSION.Text = pol_values[0].ToString() == "" ? Convert.ToDouble("0").ToString("N", numberFormatInfo) : Convert.ToDouble(pol_values[0].ToString()).ToString("N", numberFormatInfo);
                        flag = int.Parse(pol_values[1].ToString());
                    }
                    this.FillCo_Applicants(cmbCO_APPLICANT_ID, hidCO_APPLICANT_ID.Value);                 
                    if (Request.QueryString["REMUNERATION_ID"] != null && Request.QueryString["REMUNERATION_ID"].ToString() != "")
                    {
                        hidRemunerationId.Value = Request.QueryString["REMUNERATION_ID"].ToString();
                        string strCustomerId = Request.QueryString["CUSTOMER_ID"].ToString();
                        string strPolicyId = Request.QueryString["POLICY_ID"].ToString();
                        string strPolicyVersionId = Request.QueryString["POLICY_VERSION_ID"].ToString();

                        this.GetOldDataObject(int.Parse(hidRemunerationId.Value), int.Parse(strCustomerId), int.Parse(strPolicyId), int.Parse(strPolicyVersionId));
                        
                    }
                    else if (Request.QueryString["REMUNERATION_ID"] == null)
                    {
                        hidRemunerationId.Value = "NEW";
                    }
                    // In case of master policy co-applicant dropdown is visible
                    if (TransactionType =="14560")
                    {                      
                        tdcommission.Visible = false;
                        tdCoapplicant.ColSpan = 2;
                        tdCoapplicant.Width = "100%";

                    }

                    else {
                        tdCoapplicant.Visible = false;
                        rfvCO_APPLICANT_ID.Enabled = false;
                        tdcommission.ColSpan = 2;
                        tdcommission.Width = "100%";
                    
                    }
                    
                   
                }

                strRowId = hidRemunerationId.Value;
            }
            catch (Exception ex) {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            //FillAgency(agencyid);
        }


        private void GetOldDataObject(Int32 REMUNERATION_ID, int CustomerId, int PolicyId, int PolicyVersionId)
        {
            try
            {
                numberFormatInfo.NumberDecimalDigits = 2;
                objnewPolicyRemunerationInfo = objGeneralInformation.FetchData(REMUNERATION_ID, CustomerId, PolicyId, PolicyVersionId);
                FillAgency(objnewPolicyRemunerationInfo.BROKER_ID.CurrentValue.ToString());
                
                PopulatePageFromEbixModelObject(this.Page, objnewPolicyRemunerationInfo);                
                hidRemunerationId.Value = objnewPolicyRemunerationInfo.REMUNERATION_ID.CurrentValue.ToString().Trim();
                if (objnewPolicyRemunerationInfo.COMMISSION_PERCENT.CurrentValue.ToString() != ""){
                    txtCOMMISSION_PERCENT.Text = objnewPolicyRemunerationInfo.COMMISSION_PERCENT.CurrentValue.ToString("N", NfiBaseCurrency);
                }
                CommissionType=objnewPolicyRemunerationInfo.COMMISSION_TYPE.CurrentValue.ToString();
                if (CommissionType == "43")
                {
                    cmbBROKER_ID.SelectedValue = objnewPolicyRemunerationInfo.BROKER_ID.CurrentValue.ToString();
                  
                    cmbBROKER_ID_2.Style.Add("display", "none");
                    rfv2BROKER_ID.Attributes.Add("enabled", "false");
                    rfv2BROKER_ID.Attributes.Add("visible", "false");
                }
                else
                {
                    cmbBROKER_ID_2.SelectedValue = objnewPolicyRemunerationInfo.BROKER_ID.CurrentValue.ToString();
                 
                    cmbBROKER_ID.Style.Add("display", "none");
                    rfvBROKER_ID.Attributes.Add("enabled", "false");
                    rfvBROKER_ID.Attributes.Add("visible", "false");
                    rfvLEADER.Attributes.Add("enabled", "false");
                    rfvLEADER.Attributes.Add("visible", "false");
                }

                //if (objnewPolicyRemunerationInfo.BROKER_ID.CurrentValue.ToString() != "")

                //    cmbBROKER_ID.SelectedValue = objnewPolicyRemunerationInfo.BROKER_ID.CurrentValue.ToString();            

                               
                if (objnewPolicyRemunerationInfo.COMMISSION_PERCENT.CurrentValue.ToString() == "-1")
                    txtCOMMISSION_PERCENT.Text = "";
              
                else
                    txtCOMMISSION_PERCENT.Text = objnewPolicyRemunerationInfo.COMMISSION_PERCENT.CurrentValue.ToString("N", numberFormatInfo);

                if (objnewPolicyRemunerationInfo.BRANCH.CurrentValue.ToString() == "0")
                    txtBRANCH.Text = "";

                hidCO_APPLICANT_ID.Value = objnewPolicyRemunerationInfo.CO_APPLICANT_ID.CurrentValue.ToString();

                if (objnewPolicyRemunerationInfo.BROKER_ID.CurrentValue.ToString() == flag.ToString() && IS_PrimartAppicant(int.Parse(hidCO_APPLICANT_ID.Value)) == true)
                {
                    //agencyid = objnewPolicyRemunerationInfo.BROKER_ID.CurrentValue.ToString();
                    cmbBROKER_ID_2.Enabled = false;
                    cmbBROKER_ID.Enabled = false;
                    cmbCOMMISSION_TYPE.Enabled = false;
                    cmbCO_APPLICANT_ID.Enabled = false;
                }
               
                base.SetPageModelObject(objnewPolicyRemunerationInfo);

            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.FetchGeneralMessage("21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }

        }


        private void SetCaption()
        {
            capCO_APPLICANT_ID.Text = Objresources.GetString("capCO_APPLICANT_ID");
            capCOMMISSION_TYPE.Text = Objresources.GetString("capCOMMISSION_TYPE");
            capBROKER_ID.Text = Objresources.GetString("capBROKER_ID");
            capBRANCH.Text = Objresources.GetString("capBRANCH");
            capCOMMISSION_PERCENT.Text = Objresources.GetString("capCOMMISSION_PERCENT");

            capLEADER.Text = Objresources.GetString("capLEADER");
            capPOLICY_LEVEL_COMMISSION.Text = Objresources.GetString("capPOLICY_LEVEL_COMMISSION");

        } //Set Cation From Resource File For Multilingual

        private void SetErrorMessage() {

            rfvCO_APPLICANT_ID.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "5");
            rfvLEADER.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "7");
            rfvCOMMISSION_TYPE.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "4");
            rfvBROKER_ID.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "1");
            rfvCOMMISSION_PERCENT.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "2");
            revCOMMISSION_PERCENT.ValidationExpression = aRegExpDoublePositiveWithZeroFourDeci;
            revCOMMISSION_PERCENT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
            csvCOMMISSION_PERCENT.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "3");
            revBRANCH.ValidationExpression = aRegExpIntegerPositiveNonZero;           
            revBRANCH.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
           rfv2BROKER_ID.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "1");
         

        }

        private bool IS_PrimartAppicant(int ApplicantID)
        {
            dtCo_Applicant = GetCO_ApplicantDetails();
            bool IS_PRIMARY_APPLICANT = false;
            foreach (DataRow dr in dtCo_Applicant.Rows)
            {
                if (dr["APPLICANT_ID"].ToString() == ApplicantID.ToString() && dr["IS_PRIMARY_APPLICANT"].ToString() == "1")
                {
                    IS_PRIMARY_APPLICANT = true;
                }
            }
            return IS_PRIMARY_APPLICANT;
        }

        private DataTable GetCO_ApplicantDetails()
        {
            DataTable dt = null;
            dt = ClsGeneralInformation.CheckApplicantForPolicy(CustomerId, PolicyId, PolicyVersionId);
            return dt;
        }

        

        private void FillAgency(string AgencyID)
        {
            ClsGeneralInformation objGenInfo = new ClsGeneralInformation();
            DataSet ds = objGenInfo.GetPolicyDetails(CustomerId, PolicyId, PolicyVersionId);
            if (ds!=null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count > 0)
            {
                EFFECTIVEDATE = Convert.ToDateTime(ds.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"]);
             
            }


            cmbBROKER_ID_2.Items.Clear();
            cmbBROKER_ID.Items.Clear();
            ClsAgency objAgency = new ClsAgency();
            DataSet dsTemp = new DataSet();
            //int Broker = (int)enumAgencyType.BROKER_AGENCY;
            dsTemp = objAgency.FillAgency();
           
            DataTable dt = new DataTable();
            //DateTime dtAPP_EFFECTIVE_DATE = new DateTime();
            //dtAPP_EFFECTIVE_DATE = Convert.ToDateTime(txtAPP_EFFECTIVE_DATE.Text);

            string IS_TERMINATED = "N";
            if (AgencyID == "")
                AgencyID = "0";
            if (dsTemp != null && dsTemp.Tables.Count > 0)
            {

                if (dsTemp.Tables[0].Select("AGENCY_ID = " + AgencyID ).Length > 0)
                {
                    DateTime AgencyDateTime = Convert.ToDateTime(dsTemp.Tables[0].Select("AGENCY_ID = " + AgencyID).CopyToDataTable().Rows[0]["TERMINATION_DATE"]);
                    if (AgencyDateTime < EFFECTIVEDATE)
                        IS_TERMINATED = "Y";
                }

                //modified by Lalit .itrack # 634.June 28,2011.dropdown should retriev Sales Agent in alphabetical order and then Brokers also in alphabetical order for pro-Labore and Enrollment fess.
                dt = dsTemp.Tables[0].Select("TERMINATION_DATE >= '" + EFFECTIVEDATE.ToShortDateString() + "' or AGENCY_ID = " + AgencyID + " ", "AGENCY_TYPE_ID DESC,DISPLAY_NAME ASC").CopyToDataTable<DataRow>();
                cmbBROKER_ID_2.DataSource = dt;//dsTemp.Tables[0].Select("TERMINATION_DATE > '" + dtAPP_EFFECTIVE_DATE + "' and AGENCY_TYPE =" + Broker, "AGENCY_NAME_ACTIVE_STATUS ASC").CopyToDataTable();
                cmbBROKER_ID_2.DataTextField = "DISPLAY_NAME";
                cmbBROKER_ID_2.DataValueField = "AGENCY_ID";
                cmbBROKER_ID_2.DataBind();
                cmbBROKER_ID_2.Items.Insert(0, "");

                DataView dv = new DataView(dsTemp.Tables[0], "(TERMINATION_DATE >= '" + EFFECTIVEDATE.ToShortDateString() + "' or AGENCY_ID = " + AgencyID + ") and (AGENCY_TYPE_ID=14701 or AGENCY_TYPE_ID is null)", "DISPLAY_NAME ASC", DataViewRowState.CurrentRows);
                if (GetSystemId().ToString().ToUpper() == "S001" || GetSystemId().ToString().ToUpper() == "SUAT")
                {
                    dt = dsTemp.Tables[0].Select("TERMINATION_DATE >= '" + EFFECTIVEDATE.ToShortDateString() + "' or AGENCY_ID = " + AgencyID + " ", "AGENCY_TYPE_ID DESC,DISPLAY_NAME ASC").CopyToDataTable<DataRow>();
                }
                else
                {
                    dt = dv.ToTable().Select("", "DISPLAY_NAME").CopyToDataTable<DataRow>();
                }
                cmbBROKER_ID.DataSource = dt;//dsTemp.Tables[0].Select("TERMINATION_DATE > '" + dtAPP_EFFECTIVE_DATE + "' and AGENCY_TYPE =" + Broker, "AGENCY_NAME_ACTIVE_STATUS ASC").CopyToDataTable();
                cmbBROKER_ID.DataTextField = "DISPLAY_NAME";
                cmbBROKER_ID.DataValueField = "AGENCY_ID";
                cmbBROKER_ID.DataBind();
                cmbBROKER_ID.Items.Insert(0, "");       
                // setAgencyColor(dsTemp.Tables[0]);
                if (IS_TERMINATED == "Y")
                {
                    cmbBROKER_ID_2.Items.FindByValue(AgencyID).Attributes.Add("style", "color:red");
                    cmbBROKER_ID.Items.FindByValue(AgencyID).Attributes.Add("style", "color:red");
                }

                dsTemp.Dispose();
            }

        }


        private void LoadDropDown() {
            try
            {

                cmbLEADER.DataSource = ClsCommon.GetLookup("YESNO");
                cmbLEADER.DataTextField = "LookupDesc";
                cmbLEADER.DataValueField = "LookupID";
                cmbLEADER.DataBind();
                cmbLEADER.Items.Insert(0, "");


                string strLob = GetLOBID() == "" ? "0" : GetLOBID();
                cmbCOMMISSION_TYPE.DataSource = ClsGeneralInformation.GetCommissionType("COM", int.Parse(strLob));
                cmbCOMMISSION_TYPE.DataTextField = "DISPLAY_DESCRIPTION";
                cmbCOMMISSION_TYPE.DataValueField = "TRAN_ID";
                cmbCOMMISSION_TYPE.DataBind();
                cmbCOMMISSION_TYPE.Items.Insert(0, "");

                //ClsAgency objAgency = new ClsAgency();
                //DataSet dsTemp = new DataSet();
                //dsTemp = objAgency.FillAgency();
                //int Broker = (int)enumAgencyType.BROKER_AGENCY;
                //DataTable dt = new DataTable();

                //DateTime dtAPP_EFFECTIVE_DATE = new DateTime();
                //dtAPP_EFFECTIVE_DATE = Convert.ToDateTime(EFFECTIVEDATE);;
                //dt = dsTemp.Tables[0].Select( "TERMINATION_DATE > '" + dtAPP_EFFECTIVE_DATE + "' ", "AGENCY_TYPE_ID DESC").CopyToDataTable<DataRow>();
                //cmbBROKER_ID_2.DataSource = dt;//dsTemp.Tables[0].Select("TERMINATION_DATE > '" + dtAPP_EFFECTIVE_DATE + "' and AGENCY_TYPE =" + Broker, "AGENCY_NAME_ACTIVE_STATUS ASC").CopyToDataTable();
                //cmbBROKER_ID_2.DataTextField = "DISPLAY_NAME";
                // cmbBROKER_ID_2.DataValueField = "AGENCY_ID";
                // cmbBROKER_ID_2.DataBind();
                // cmbBROKER_ID_2.Items.Insert(0, "");

                // DataView dv = new DataView(dsTemp.Tables[0], "TERMINATION_DATE > '" + dtAPP_EFFECTIVE_DATE + "' and AGENCY_TYPE_ID=14701 or AGENCY_TYPE_ID is null", "AGENCY_DISPLAY_NAME", DataViewRowState.CurrentRows);                
                // dt = dv.ToTable().Select("", "DISPLAY_NAME").CopyToDataTable<DataRow>();
                // cmbBROKER_ID.DataSource = dt;//dsTemp.Tables[0].Select("TERMINATION_DATE > '" + dtAPP_EFFECTIVE_DATE + "' and AGENCY_TYPE =" + Broker, "AGENCY_NAME_ACTIVE_STATUS ASC").CopyToDataTable();
                // cmbBROKER_ID.DataTextField = "DISPLAY_NAME";
                // cmbBROKER_ID.DataValueField = "AGENCY_ID";
                // cmbBROKER_ID.DataBind();
                // cmbBROKER_ID.Items.Insert(0, "");                   
                               

            }


               
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.FetchGeneralMessage("21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
      
        }

       
       



        private string PolicyLabelCommission()
        {
            ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();
            string PlicyLabelCommission = objGeneralInformation.GetPolicyLavelCommission(CustomerId, PolicyId, PolicyVersionId);
            return PlicyLabelCommission;
        } 

        private void FillCo_Applicants(DropDownList cmbCO_APPLICANT, string Selectedvalue)
        {

            try
            {

                DataTable dtCo_Applicant = GetCO_ApplicantDetails();
                string[] COLUMN_NAME = { "APPLICANT_ID", "APPLICANTNAME" };
                cmbCO_APPLICANT.DataSource = dtCo_Applicant.DefaultView.ToTable("CO_APPLICANTS", true, COLUMN_NAME);
                cmbCO_APPLICANT.DataTextField = "APPLICANTNAME";
                cmbCO_APPLICANT.DataValueField = "APPLICANT_ID";
                cmbCO_APPLICANT.DataBind();
                cmbCO_APPLICANT.Items.Insert(0, "");
                if (Selectedvalue != "")
                    cmbCO_APPLICANT.SelectedValue = Selectedvalue;
            }
            catch (Exception ex) {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
        }



        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {
            //  this.btnResetSeries.Click += new System.EventHandler(this.btnResetSeries_Click);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            //  this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion


        private void btnSave_Click(object sender, System.EventArgs e)
        {
            if (Convert.ToDouble(txtCOMMISSION_PERCENT.Text, numberFormatInfo) == 0.0)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "16");
                lblMessage.Visible=true;
                hidFormSaved.Value = "2";
                return;
            }

            try
            {
                int intRetVal;	//For retreiving the return value of business class save function              

                //Retreiving the form values into model class object              

                if (strRowId.ToUpper().Equals("NEW")) //save case              
                {
                    this.GetFormValue(objnewPolicyRemunerationInfo);
                    objnewPolicyRemunerationInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                    objnewPolicyRemunerationInfo.CREATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now);
                    objnewPolicyRemunerationInfo.IS_ACTIVE.CurrentValue = "Y";

                    //Calling the add method of business layer class 
                    intRetVal = objGeneralInformation.AddRemuneration(objnewPolicyRemunerationInfo);

                    if (intRetVal>0)
                    {                      
                         this.GetOldDataObject(objnewPolicyRemunerationInfo.REMUNERATION_ID.CurrentValue,CustomerId,PolicyId,PolicyVersionId);                
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("29");
                        hidFormSaved.Value = "1";    
                         
                        
                       
                    }
                    else if (intRetVal == -2)
                    {
                        if (TransactionType == MASTER_POLICY) // by sneha for iTrack 1407
                        {
                            if (cmbCOMMISSION_TYPE.SelectedValue == "43")
                            {
                                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "17");
                            }
                            else if (cmbCOMMISSION_TYPE.SelectedValue == "44")
                            {
                                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "18");
                            }
                            else if (cmbCOMMISSION_TYPE.SelectedValue == "45")
                            {
                                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "19");
                            }

                        }
                        else
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "6");
                        hidFormSaved.Value = "2";

                    }
                    else if (intRetVal == -3)
                    {

                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "8");
                        hidFormSaved.Value = "2";
                    }
                    else if (intRetVal == -4)
                    {

                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "10");
                        hidFormSaved.Value = "2";
                    }
                    else if (intRetVal == -5)
                    {

                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "13");
                        hidFormSaved.Value = "2";
                    }
                    else if (intRetVal == -6)
                    {

                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "14");
                        hidFormSaved.Value = "2";
                    }
                    else if (intRetVal == -7)
                    {

                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "15");
                        hidFormSaved.Value = "2";
                    }

                    //else if (intRetVal == -8)
                    //{

                    //    lblMessage.Text = "please enter commission percentage greater than 0";//ClsMessages.GetMessage(base.ScreenId, "15");
                    //    hidFormSaved.Value = "2";
                    //}
                    else
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                        hidFormSaved.Value = "2";
                    }
                    lblMessage.Visible = true;
                } // end save case
                else //UPDATE CASE
                {


                    objnewPolicyRemunerationInfo = (ClsPolicyRemunerationInfo)base.GetPageModelObject();

                    this.GetFormValue(objnewPolicyRemunerationInfo);
                    objnewPolicyRemunerationInfo.REMUNERATION_ID.CurrentValue = int.Parse(hidRemunerationId.Value);
                    objnewPolicyRemunerationInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                    objnewPolicyRemunerationInfo.LAST_UPDATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now);


                    int intRetval = objGeneralInformation.UpdateRemuneration(objnewPolicyRemunerationInfo); //objCurrencyRate.UpdateCurrencyRate(objCurrencyRateinfo);

                    if (intRetval > 0)
                    {
                        int RemunerationId= int.Parse(hidRemunerationId.Value);
                        this.GetOldDataObject(RemunerationId, CustomerId, PolicyId, PolicyVersionId);                
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("31");                        
                        hidFormSaved.Value = "1";

                    }

                    else if (intRetval == -2)
                    {
                        if (TransactionType == MASTER_POLICY) // by sneha for iTrack 1407
                        {
                            if (cmbCOMMISSION_TYPE.SelectedValue == "43")
                            {
                                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "17");
                            }
                            else if (cmbCOMMISSION_TYPE.SelectedValue == "44")
                            {
                                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "18");
                            }
                            else if (cmbCOMMISSION_TYPE.SelectedValue == "45")
                            {
                                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "19");
                            }
                            //lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "12");

                        }
                        else
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "6");
                        hidFormSaved.Value = "2";
                    }
                    else if (intRetval == -3)
                    {

                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "8");
                        hidFormSaved.Value = "2";
                    }
                    else if (intRetval == -4)
                    {

                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "10");
                        hidFormSaved.Value = "2";
                    }
                    else if (intRetval == -5)
                    {

                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "13");
                        hidFormSaved.Value = "2";
                    }
                    else if (intRetval == -6)
                    {

                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "14");
                        hidFormSaved.Value = "2";
                    }
                    else if (intRetval == -7)
                    {

                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "15");
                        hidFormSaved.Value = "2";
                    }
                    //else if (intRetval == -8)
                    //{

                    //    lblMessage.Text = "please enter commission percentage greater than 0";//ClsMessages.GetMessage(base.ScreenId, "15");
                    //    hidFormSaved.Value = "2";
                    //}
                    else
                    {
                        lblMessage.Text = lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                        hidFormSaved.Value = "2";
                    }

                    lblMessage.Visible = true;
                }


            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.FetchGeneralMessage("21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";

            }
            finally
            {
               // hidRemunerationId.Value = objnewPolicyRemunerationInfo.REMUNERATION_ID.CurrentValue.ToString();
            }
            //FillAgency(agencyid);

        }

        private void GetFormValue(ClsPolicyRemunerationInfo objnewPolicyRemunerationInfo)
        {
            try
            {
                objnewPolicyRemunerationInfo.CUSTOMER_ID.CurrentValue = CustomerId;
                objnewPolicyRemunerationInfo.POLICY_ID.CurrentValue = PolicyId;
                objnewPolicyRemunerationInfo.POLICY_VERSION_ID.CurrentValue = PolicyVersionId;

                if (TransactionType != MASTER_POLICY)
                {
                    objnewPolicyRemunerationInfo.CO_APPLICANT_ID.CurrentValue = ClsGeneralInformation.GetPolicyPrimary_Applicant(CustomerId, PolicyId, PolicyVersionId);                     
                }
                else
                {
                    if (cmbCO_APPLICANT_ID.SelectedValue != "")
                        objnewPolicyRemunerationInfo.CO_APPLICANT_ID.CurrentValue = int.Parse(cmbCO_APPLICANT_ID.SelectedValue);
                    else                    
                        objnewPolicyRemunerationInfo.CO_APPLICANT_ID.CurrentValue = 0;                    
                }

                if (cmbLEADER.SelectedValue!="")          
                    objnewPolicyRemunerationInfo.LEADER.CurrentValue = int.Parse(cmbLEADER.SelectedValue);          
                else         
                    objnewPolicyRemunerationInfo.LEADER.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.NO);     
                if (cmbCOMMISSION_TYPE.SelectedValue.Trim() != "")
                    objnewPolicyRemunerationInfo.COMMISSION_TYPE.CurrentValue = int.Parse(cmbCOMMISSION_TYPE.SelectedValue);
                
                if (txtBRANCH.Text!="")
                    objnewPolicyRemunerationInfo.BRANCH.CurrentValue = int.Parse(txtBRANCH.Text);
                else
                    objnewPolicyRemunerationInfo.BRANCH.CurrentValue = GetEbixIntDefaultValue();

                if (txtCOMMISSION_PERCENT.Text != "")
                    objnewPolicyRemunerationInfo.COMMISSION_PERCENT.CurrentValue = Convert.ToDouble(txtCOMMISSION_PERCENT.Text, numberFormatInfo);
                else
                    objnewPolicyRemunerationInfo.COMMISSION_PERCENT.CurrentValue = 0;
                objnewPolicyRemunerationInfo.AMOUNT.CurrentValue = 0;


                if (cmbCOMMISSION_TYPE.SelectedValue == "43")
                {
                    if (cmbBROKER_ID.SelectedValue != null && cmbBROKER_ID.SelectedValue != "")
                        objnewPolicyRemunerationInfo.BROKER_ID.CurrentValue = int.Parse(cmbBROKER_ID.SelectedValue);
                    else
                        objnewPolicyRemunerationInfo.BROKER_ID.CurrentValue = GetEbixIntDefaultValue();
                }

                else {
                    if (cmbBROKER_ID_2.SelectedValue != null && cmbBROKER_ID_2.SelectedValue != "")
                        objnewPolicyRemunerationInfo.BROKER_ID.CurrentValue = int.Parse(cmbBROKER_ID_2.SelectedValue);
                    else
                        objnewPolicyRemunerationInfo.BROKER_ID.CurrentValue = GetEbixIntDefaultValue();
                }
               

            }
            catch (Exception ex) {
                lblMessage.Text = ClsMessages.FetchGeneralMessage("21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            
            try
            {
              //  objnewPolicyRemunerationInfo = (ClsPolicyRemunerationInfo)base.GetPageModelObject();

                objnewPolicyRemunerationInfo.REMUNERATION_ID.CurrentValue = int.Parse(hidRemunerationId.Value);
              
                objnewPolicyRemunerationInfo.CUSTOMER_ID.CurrentValue = CustomerId;
                objnewPolicyRemunerationInfo.POLICY_ID.CurrentValue = PolicyId;
                objnewPolicyRemunerationInfo.POLICY_VERSION_ID.CurrentValue = PolicyVersionId;
                objnewPolicyRemunerationInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());                
                    if (cmbBROKER_ID.SelectedValue == flag.ToString()  && cmbCOMMISSION_TYPE.SelectedValue == "43" && IS_PrimartAppicant(int.Parse(hidCO_APPLICANT_ID.Value)) == true)
                {

                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "11");
                    lblMessage.Visible = true;
                    return; 
                }
               
                int intRetval = objGeneralInformation.DeleteBroker(objnewPolicyRemunerationInfo);
                if (intRetval >= 0)
                {
                    lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "127");
                    hidFormSaved.Value = "1";
                    trBody.Attributes.Add("style", "display:none");
                }
                else 
                {
                    lblDelete.Text = ClsMessages.GetMessage(base.ScreenId, "128");
                    hidFormSaved.Value = "2";
                }
                lblDelete.Visible = true;
                lblMessage.Visible = false;
               

            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.FetchGeneralMessage("21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";
            }
        }


        
    }

}
