using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cms.CmsWeb.Controls;
using Cms.CmsWeb.WebControls;
using System.Resources;
using Cms.BusinessLayer.BlCommon;
using System.Data;
using Cms.BusinessLayer.BlApplication;
using Cms.Model.Policy.Accident;
using Cms.CmsWeb;

namespace Cms.Policies.Aspx.Accident
{
    public partial class AddInvidualInfo : Cms.Policies.policiesbase
    {
        #region Variables
        private string strRowId = "";
        ResourceManager objResourceMgr;
        public string javasciptCPFmsg, javasciptCNPJmsg, CPF_invalid, CNPJ_invalid;//Added By Pradeep Kushwaha on 02-July-2010 to Get the CPF Message
        ClsProducts ObjProducts = new ClsProducts();
        string CalledFrom;
        public string PAGEFROM;
        public string FIRSTTAB;//in case of quickapp
        #endregion
        protected System.Web.UI.WebControls.CompareValidator cpvREG_ID_ISSUES;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvDATE_OF_BIRTH;
        protected System.Web.UI.WebControls.CustomValidator csvREG_ID_ISSUES; 

       
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.DataBind();
            #region setting screen id
            if (Request.QueryString["CalledFrom"] != null && Request.QueryString["CalledFrom"].ToString() != "")
                CalledFrom = Request.QueryString["CalledFrom"].ToString();

            if (Request.QueryString["PAGEFROM"] != null)
                PAGEFROM = Request.QueryString["PAGEFROM"].ToString();

            FIRSTTAB = ClsMessages.GetTabTitles("497", "TabCtl");
            switch (CalledFrom)
            {
                case "INDPA"://INDIVILUAL PERSONAL ACCIDENT

                    hidCALLED_FROM.Value = CalledFrom;
                    // for itrack 1161
                    base.ScreenId = INDIVIDUAL_PERSONAL_ACCIDENTscreenId.INDIVIDUAL_INFORMATION;//"116_1_2";
                    //Page.DataBind();
                    hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCtl");

                    break;

                case "CPCACC": //FOR THE GROUP PERSONAL ACCEIDENT

                    base.ScreenId = GROUP_PERSONAL_ACCIDENTscreenId.INDIVIDUAL_INFORMATION;
                    hidCALLED_FROM.Value = CalledFrom;
                    hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCtl");
                    
                    break;
                case "MRTG": //Mortgage

                    base.ScreenId = MORTGAGEscreenId.INDIVIDUAL_INFORMATION;
                    hidCALLED_FROM.Value = CalledFrom;
                    hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCtl");

                    break;
                case "GRPLF": //Group Life

                    base.ScreenId = GROUP_LIFEscreenId.INDIVIDUAL_INFORMATION;
                    hidCALLED_FROM.Value = CalledFrom;
                    hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCtl");

                    break;
                    
                    
            }
            // itrack no 1333 by praveer
            if (CalledFrom != "INDPA")
            {
                spnMARITAL_STATUS.Visible = false;
                rfvMARITAL_STATUS.Enabled = false;
                // change by praveer for itrack no 1408
                btnSelect.Enabled = false;
              
            }

           
            #endregion

            //if (CalledFrom == "CPCACC" || CalledFrom == "GRPLF")
            //{
            //    spnGENDER.Visible = true;
            //    spnREG_ID_ORG.Visible = true;
            //    spnSTATE_ID.Visible = true;
            //    rfvSTATE_ID.Enabled = true;
            //    rfvREG_ID_ORG.Enabled = true;
            //    rfvGENDER.Enabled = true;

            //}

            //else {

            //    spnGENDER.Visible = false;
            //    spnREG_ID_ORG.Visible = false;
            //    spnSTATE_ID.Visible = false;
            //    rfvSTATE_ID.Enabled = false;
            //    rfvREG_ID_ORG.Enabled = false;
            //    rfvGENDER.Enabled = false;
            //}

            btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1333");   

            #region setting security Xml
            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnActivateDeactivate.CmsButtonClass = CmsButtonType.Write;
            btnActivateDeactivate.PermissionString = gstrSecurityXML;

            btnDelete.CmsButtonClass = CmsButtonType.Delete;
            btnDelete.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            btnSelect.CmsButtonClass = CmsButtonType.Write;
            btnSelect.PermissionString = gstrSecurityXML;
            #endregion
       

            GetQueryString();

            objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.Accident.AddInvidualInfo", System.Reflection.Assembly.GetExecutingAssembly());
            hlkDATE_OF_BIRTH.Attributes.Add("OnClick", "fPopCalendar(document.getElementById('txtDATE_OF_BIRTH'), document.getElementById('txtDATE_OF_BIRTH'))");
            hlkREG_ID_ISSUES.Attributes.Add("OnClick", "fPopCalendar(txtREG_ID_ISSUES,txtREG_ID_ISSUES)");
           // HidREG_ID_ISSUES.Value = "1"; //Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "116_1_13");
           // HidREG_ID_ISSUES1.Value = "2"; //Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "116_1_16");
            
            
            SetErrorMessage();
          

          
            if (!Page.IsPostBack)
            {
                SetCaptions();
                SetButtonText();
                PopulateGender();
                this.BindExceededPremium();
               
              //  PopulatePosition();
                PopulateStates(5);//For Brazil
                PopulateApplicant();
                FillPosition();
                this.PopulateInsuredObject();
                //btnSelect.Attributes.Add("onclick", "javascript:return false;");
                //btnSelect.Visible = false;
                if (CalledFrom == "INDPA")
                   
                btnReset.Attributes.Add("onclick", "javascript: return ResetTheForm();");
                
                if (Request.QueryString["PERSONAL_INFO_ID"] != null && Request.QueryString["PERSONAL_INFO_ID"].ToString() != "" && Request.QueryString["PERSONAL_INFO_ID"].ToString() != "NEW")
                {

                    hidPERSONAL_INFO_ID.Value = Request.QueryString["PERSONAL_INFO_ID"].ToString();

                    this.GetOldDataObject(Convert.ToInt32(Request.QueryString["PERSONAL_INFO_ID"].ToString()));
                    btnActivateDeactivate.Enabled = true;
                    btnDelete.Visible = true;

                }
                else //if (Request.QueryString["PERSONAL_INFO_ID"] == null)
                {
                    btnActivateDeactivate.Enabled = false;
                    btnDelete.Enabled = false;
                    //cstREG_ID_ISSUES.Enabled = false;
                    hidPERSONAL_INFO_ID.Value = "NEW";
                    cmbMAIN_INSURED.Style.Add("display", "none");
                    capMAIN_INSURED.Style.Add("display", "none");
                   
                    
                }
                strRowId = hidPERSONAL_INFO_ID.Value;
                
            }
             
        }

        #region Methods
        private void SetCaptions()
        {
            #region setcaption in resource file
            capINDIVIDUAL_NAME.Text = objResourceMgr.GetString("txtINDIVIDUAL_NAME");
            capCODE.Text = objResourceMgr.GetString("txtCODE");
            capPOSITION_ID.Text = objResourceMgr.GetString("cmbPOSITION_ID");
            capCPF_NUM.Text = objResourceMgr.GetString("txtCPF_NUM");
            capSTATE_ID.Text = objResourceMgr.GetString("cmbSTATE_ID");
            capDATE_OF_BIRTH.Text = objResourceMgr.GetString("txtDATE_OF_BIRTH");
            capGENDER.Text = objResourceMgr.GetString("cmbGENDER");
            capREG_ID_ISSUES.Text = objResourceMgr.GetString("txtREG_ID_ISSUES");
            capREG_ID_ORG.Text = objResourceMgr.GetString("txtREG_ID_ORG");
            capREMARKS.Text = objResourceMgr.GetString("txtREMARKS");
            capREG_IDEN.Text = objResourceMgr.GetString("txtREG_IDEN");
            capAPPLICANT.Text = objResourceMgr.GetString("cmbAPPLICANT_ID");
            capMAN_MSG.Text = objResourceMgr.GetString("capMAN_MSG");
            capIS_SPOUSE_OR_CHILD.Text = objResourceMgr.GetString("chkIS_SPOUSE_OR_CHILD");
            capMAIN_INSURED.Text = objResourceMgr.GetString("cmbMAIN_INSURED");
            capCITY_OF_BIRTH.Text = objResourceMgr.GetString("txtCITY_OF_BIRTH");
            capMARITAL_STATUS.Text = objResourceMgr.GetString("capMARITAL_STATUS");
            capExceededPremium.Text = objResourceMgr.GetString("cmbExceeded_Premium");
            #endregion

        }

        private void PopulateGender()
        {
            cmbGENDER.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("SEXCD");
            cmbGENDER.DataTextField = "LookupDesc";
            cmbGENDER.DataValueField = "LookupID";
            cmbGENDER.DataBind();
            cmbGENDER.Items.Insert(0, "");
            // itrack no 1333 by praveer
            cmbMARITAL_STATUS.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("MARST");
            cmbMARITAL_STATUS.DataTextField = "LookupDesc";
            cmbMARITAL_STATUS.DataValueField = "LookupCode";
            cmbMARITAL_STATUS.DataBind();
            cmbMARITAL_STATUS.Items.Insert(0, "");
        }

        private void BindExceededPremium()
        {
            cmbExceeded_Premium.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("yesno");
            cmbExceeded_Premium.DataTextField = "LookupDesc";
            cmbExceeded_Premium.DataValueField = "LookupID";
            cmbExceeded_Premium.DataBind();
            //cmbExceeded_Premium.SelectedValue = cmbExceeded_Premium.Items.FindByValue("10946").Value;
        }

        private void PopulateStates(int COUNTRY_ID)
        {
            ClsStates objStates = new ClsStates();
            if (COUNTRY_ID == 0)
                return;
            cmbSTATE_ID.Items.Clear();


            DataTable dtStates = objStates.GetStatesForCountry(COUNTRY_ID);
            if (dtStates != null && dtStates.Rows.Count > 0)
            {
                cmbSTATE_ID.DataSource = dtStates;
                cmbSTATE_ID.DataTextField = "STATE_NAME";
                cmbSTATE_ID.DataValueField = "STATE_ID";
                cmbSTATE_ID.DataBind();
                cmbSTATE_ID.Items.Insert(0, "");
                if (dtStates.Rows.Count != 0)
                {
                    cmbSTATE_ID.Items[0].Selected = true;
                }
            }
        }

        private void GetQueryString()
        {
            hidCUSTOMER_ID.Value = Request.Params["CUSTOMER_ID"] == null ? GetCustomerID() : Request.Params["CUSTOMER_ID"];
            hidPOL_ID.Value = Request.Params["POL_ID"] == null ? GetPolicyID() : Request.Params["POL_ID"];
            hidPOL_VERSION_ID.Value = Request.Params["POL_VERSION_ID"] == null ? GetPolicyVersionID() : Request.Params["POL_VERSION_ID"]; 
        }

        private void PopulateApplicant()
        {
            ClsIndividualInfo ObjIndividualInfo = new ClsIndividualInfo();            
            DataTable dt = ObjIndividualInfo.FetchApplicants(Convert.ToInt32(hidCUSTOMER_ID.Value), Convert.ToInt32(hidPOL_VERSION_ID.Value), Convert.ToInt32(hidPOL_ID.Value)).Tables[0];
            DataView dv = dt.DefaultView;
            dv.Sort = "APPLICANT_NAME";
            cmbAPPLICANT_ID.DataSource = dv;
            cmbAPPLICANT_ID.DataTextField = "APPLICANT_NAME";
            cmbAPPLICANT_ID.DataValueField = "APPLICANT_ID";
            cmbAPPLICANT_ID.DataBind();
            cmbAPPLICANT_ID.Items.Insert(0, "");
            // changes by praveer for itrack no 900(for Group life and group personal accident for passenger)

            if ((hidCALLED_FROM.Value == "GRPLF" || hidCALLED_FROM.Value == "CPCACC") && GetTransaction_Type().Trim() == MASTER_POLICY && GetPolicyStatus().Trim().ToUpper() == POLICY_STATUS_UNDER_ENDORSEMENT)
            {
                if ( GetEndorsementCoApplicant().Trim() != "" && GetEndorsementCoApplicant().Trim() != null)
                {
                    cmbAPPLICANT_ID.SelectedValue = GetEndorsementCoApplicant().Trim();
                }
            }

            else
            {
                string ApplicantID = "";
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["IS_PRIMARY_APPLICANT"].ToString() == "1")
                    {
                        ApplicantID = dr["APPLICANT_ID"].ToString();
                        break;
                    }
                }
                if (ApplicantID != "" && ApplicantID != null)
                {
                    cmbAPPLICANT_ID.SelectedValue = ApplicantID;
                }
            }

           
        }
        private void PopulateInsuredObject()
        {
            Int32 PERSONAL_INFO_ID = 0;
            if (hidPERSONAL_INFO_ID.Value != "NEW" && hidPERSONAL_INFO_ID.Value != "")
                PERSONAL_INFO_ID =int.Parse(hidPERSONAL_INFO_ID.Value);

            DataTable dt = ObjProducts.GetInsuredObjectdata(PERSONAL_INFO_ID, Convert.ToInt32(hidCUSTOMER_ID.Value), Convert.ToInt32(hidPOL_ID.Value), Convert.ToInt32(hidPOL_VERSION_ID.Value), "").Tables[0];
           DataView dv = dt.DefaultView;
           dv.Sort = "INDIVIDUAL_NAME";
           cmbMAIN_INSURED.SelectedIndex = -1;
           cmbMAIN_INSURED.Items.Clear();
           cmbMAIN_INSURED.DataSource = dv;
           cmbMAIN_INSURED.DataTextField = "INDIVIDUAL_NAME";
           cmbMAIN_INSURED.DataValueField = "PERSONAL_INFO_ID";
           cmbMAIN_INSURED.DataBind();
           cmbMAIN_INSURED.Items.Insert(0, "");          


        }
        //private void PopulatePosition()
        //{
        //    cmbPOSITION_ID.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("%OCC");
        //    cmbPOSITION_ID.DataTextField = "LookupDesc";
        //    cmbPOSITION_ID.DataValueField = "LookupID";
        //    cmbPOSITION_ID.DataBind();
        //    cmbPOSITION_ID.Items.Insert(0, "");
        //}


    

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
        public DataSet AjaxFillPosition()
        {
            try
            {

               string CustomerType = "11110";
                CmsWeb.webservices.ClsWebServiceCommon obj = new CmsWeb.webservices.ClsWebServiceCommon();
                string result = "";
                result = obj.CustomerTypeTitlesNew(CustomerType);
                DataSet ds = new DataSet();
                ds.ReadXml(new System.IO.StringReader(result));
                return ds;
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                return null;
            }
        }  //Fill customer Position onchange of customer Type

        private void FillPosition()
        {
            DataSet dsPosition = this.AjaxFillPosition();
            if (dsPosition != null && dsPosition.Tables.Count > 0 && dsPosition.Tables[0].Rows.Count > 0)
            {
                cmbPOSITION_ID.DataSource = dsPosition;
                cmbPOSITION_ID.DataTextField = "ACTIVITY_DESC";
                cmbPOSITION_ID.DataValueField = "ACTIVITY_ID";
                cmbPOSITION_ID.DataBind();
                cmbPOSITION_ID.Items.Insert(0, "");

            }
        }

        /// <summary>
        /// Use to Get the Old Data Based on the ID
        /// </summary>
        /// <param name="MariTime_ID"></param>
        private void GetOldDataObject(Int32 PERSONAL_INFO_ID)
        {
            ClsIndividualInfo objIndividualInfo = new ClsIndividualInfo();

            objIndividualInfo.PERSONAL_INFO_ID.CurrentValue = PERSONAL_INFO_ID;
            objIndividualInfo.CUSTOMER_ID.CurrentValue = int.Parse(GetCustomerID());
            objIndividualInfo.POLICY_ID.CurrentValue = int.Parse(GetPolicyID());
            objIndividualInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(GetPolicyVersionID());

            string policystatus = GetPolicyStatus();
            if (ObjProducts.FetchPersonalAccidentInfoData(ref objIndividualInfo))
            {

                PopulatePageFromEbixModelObject(this.Page, objIndividualInfo);

                btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objIndividualInfo.IS_ACTIVE.CurrentValue.ToString().Trim());

              //  itrack no 867
                string originalversion=objIndividualInfo.ORIGINAL_VERSION_ID.CurrentValue.ToString() ;
                if (originalversion != GetPolicyVersionID() && policystatus == "UENDRS" && originalversion!="0")
                {
                    btnActivateDeactivate.Visible = false;
                }
               //ITRACK NO 839
                hidAPPLICANT_ID.Value = objIndividualInfo.APPLICANT_ID.CurrentValue.ToString();
                if (objIndividualInfo.TYPE.CurrentValue.ToString() != "")
                    hidTYPE.Value = objIndividualInfo.TYPE.CurrentValue.ToString();
                SetCustomSecurityxml(objIndividualInfo.APPLICANT_ID.CurrentValue.ToString(), "");
                base.SetPageModelObject(objIndividualInfo);
                this.PopulateInsuredObject();
                //Itrack 851 Added by pradeep Kushwaha on 22 -feb - 2011
                if (objIndividualInfo.IS_SPOUSE_OR_CHILD.CurrentValue ==Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.YES))
                { 

                    cmbMAIN_INSURED.Attributes.Add("style", "display:inline");
                    capMAIN_INSURED.Attributes.Add("style", "display:inline"); 
                    cmbMAIN_INSURED.SelectedIndex = cmbMAIN_INSURED.Items.IndexOf(cmbMAIN_INSURED.Items.FindByValue(objIndividualInfo.MAIN_INSURED.CurrentValue.ToString()));
                }
                else
                {
                    cmbMAIN_INSURED.Attributes.Add("style", "display:none");
                    capMAIN_INSURED.Attributes.Add("style", "display:none"); 
                    //cmbMAIN_INSURED.Style.Add("display", "none");
                    //capMAIN_INSURED.Style.Add("display", "none");
                }
                
                //added till here 
            }
        }

        /// <summary>
        /// User to Set the Form control (s)'s value Data in the Model info object
        /// </summary>
        /// <param name="objMariTimeInfo"></param>
        private void GetFormValue(ClsIndividualInfo objIndividualInfo)
        {
            objIndividualInfo.ORIGINAL_VERSION_ID.CurrentValue = 0; //int.Parse(GetPolicyVersionID());
            if (txtINDIVIDUAL_NAME.Text.Trim() != "")
            { objIndividualInfo.INDIVIDUAL_NAME.CurrentValue = txtINDIVIDUAL_NAME.Text; }
            else
            {
                objIndividualInfo.INDIVIDUAL_NAME.CurrentValue = string.Empty;
            }

            if (txtCODE.Text.Trim() != "")
            { objIndividualInfo.CODE.CurrentValue = txtCODE.Text; }

            else
            { objIndividualInfo.CODE.CurrentValue = string.Empty; ; }

            if (txtCPF_NUM.Text.Trim() != "")
            { objIndividualInfo.CPF_NUM.CurrentValue = txtCPF_NUM.Text; }
            else
            { objIndividualInfo.CPF_NUM.CurrentValue = string.Empty; ; }

            if (txtDATE_OF_BIRTH.Text.Trim() != "")
            { objIndividualInfo.DATE_OF_BIRTH.CurrentValue = ConvertToDate(txtDATE_OF_BIRTH.Text); }
            //else
            //    objIndividualInfo.DATE_OF_BIRTH.CurrentValue =  ConvertToDate(null);

            if (txtREG_ID_ISSUES.Text.Trim() != "")
                objIndividualInfo.REG_ID_ISSUES.CurrentValue = ConvertToDate(txtREG_ID_ISSUES.Text);
            else
            {
                objIndividualInfo.REG_ID_ISSUES.CurrentValue = ConvertToDate(null);
            }

            if (txtREG_IDEN.Text.Trim() != "")
                objIndividualInfo.REG_IDEN.CurrentValue = txtREG_IDEN.Text;
            else
            { objIndividualInfo.REG_IDEN.CurrentValue = string.Empty; }
            if (txtREG_ID_ORG.Text.Trim() != "")
                objIndividualInfo.REG_ID_ORG.CurrentValue = txtREG_ID_ORG.Text;
            else
            { objIndividualInfo.REG_ID_ORG.CurrentValue = string.Empty; }

            if (txtREMARKS.Text.Trim() != "")
                objIndividualInfo.REMARKS.CurrentValue = txtREMARKS.Text;
            else
                objIndividualInfo.REMARKS.CurrentValue = String.Empty;

            if (cmbPOSITION_ID.SelectedValue != "")
                objIndividualInfo.POSITION_ID.CurrentValue = Convert.ToInt32(cmbPOSITION_ID.SelectedValue);
            else
                objIndividualInfo.POSITION_ID.CurrentValue = base.GetEbixIntDefaultValue();

            if (cmbSTATE_ID.SelectedValue != "")
                objIndividualInfo.STATE_ID.CurrentValue = Convert.ToInt32(cmbSTATE_ID.SelectedValue);
            //else
            //   objIndividualInfo.STATE_ID.CurrentValue = base.GetEbixIntDefaultValue();

            if (cmbGENDER.SelectedValue != "")
                objIndividualInfo.GENDER.CurrentValue = Convert.ToInt32(cmbGENDER.SelectedValue);
            else
                objIndividualInfo.GENDER.CurrentValue = base.GetEbixIntDefaultValue();

            if (cmbAPPLICANT_ID.SelectedValue != "")
                objIndividualInfo.APPLICANT_ID.CurrentValue = Convert.ToInt32(cmbAPPLICANT_ID.SelectedValue);
            else
                objIndividualInfo.APPLICANT_ID.CurrentValue = base.GetEbixIntDefaultValue();


            if (chkIS_SPOUSE_OR_CHILD.Checked)
            {


                if (cmbMAIN_INSURED.SelectedValue != "")
                {
                    objIndividualInfo.MAIN_INSURED.CurrentValue = Convert.ToInt32(cmbMAIN_INSURED.SelectedValue);
                    objIndividualInfo.IS_SPOUSE_OR_CHILD.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.YES);
                }
                else
                {
                    objIndividualInfo.IS_SPOUSE_OR_CHILD.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.NO);
                    objIndividualInfo.MAIN_INSURED.CurrentValue = base.GetEbixIntDefaultValue();
                }
            }
            else
            {
                objIndividualInfo.IS_SPOUSE_OR_CHILD.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.NO);
                objIndividualInfo.MAIN_INSURED.CurrentValue = base.GetEbixIntDefaultValue();
            }
            //Itrack 1051 Added by pradeep Kushwaha on 04 -April - 2011
            if (txtCITY_OF_BIRTH.Text.Trim() != "")
                objIndividualInfo.CITY_OF_BIRTH.CurrentValue = txtCITY_OF_BIRTH.Text;
            else
                objIndividualInfo.CITY_OF_BIRTH.CurrentValue = String.Empty;
            // itrack no 1333 by praveer
            if (cmbMARITAL_STATUS.SelectedValue != "")
                objIndividualInfo.MARITAL_STATUS.CurrentValue = cmbMARITAL_STATUS.SelectedValue.ToString();
            else
                objIndividualInfo.MARITAL_STATUS.CurrentValue = String.Empty;
            //Till here 
            if (cmbExceeded_Premium.SelectedValue != "")
                objIndividualInfo.EXCEEDED_PREMIUM.CurrentValue = int.Parse(cmbExceeded_Premium.SelectedValue);
            else
                objIndividualInfo.EXCEEDED_PREMIUM.CurrentValue = GetEbixIntDefaultValue();
        }

        private void SetErrorMessage()
        {
            rfvAPPLICANT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "116_1_4");
            revDATE_OF_BIRTH.ValidationExpression = aRegExpDate;
            revDATE_OF_BIRTH.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "116_1_6");
            rfvINDIVIDUAL_NAME.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "116_1_3");
            revREG_ID_ISSUES.ValidationExpression = aRegExpDate;
            revREG_ID_ISSUES.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "116_1_6");
            rfvCODE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "116_1_5");
            rfvPOSITION_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "116_1_7");
            csvDATE_OF_BIRTH.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "116_1_8");
            rfvCPF_NUM.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1254");
            rfvSTATE_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "116_1_10");
            rfvREG_IDEN.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1644");//Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "116_1_11");
            rfvREG_ID_ISSUES.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "116_1_12");
            rfvDATE_OF_BIRTH.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1405");
                
            cpvREG_ID_ISSUES.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1402");
            //cpvREG_ID_ISSUES2.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1403");
            csvREG_ID_ISSUES.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1403");
           

            javasciptCPFmsg = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "116_1_15");
            javasciptCNPJmsg = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1203");
            CPF_invalid = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "116_1_14");
            CNPJ_invalid = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1206");

            revCPF_NUM.ValidationExpression = aRegExpCpf_Cnpj;
            revCPF_NUM.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "116_1_15");
            rfvGENDER.ErrorMessage = ClsMessages.FetchGeneralMessage("1645");
            rfvREG_ID_ORG.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "116_1_11");//ClsMessages.FetchGeneralMessage("1644");
            rfvCITY_OF_BIRTH.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "116_1_17");
            rfvMARITAL_STATUS.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("480");
        }

        private void SetButtonText()
        {
            btnSave.Text = ClsMessages.FetchGeneralButtonsText("btnSave");
            btnDelete.Text = ClsMessages.FetchGeneralButtonsText("btnDelete");
            btnReset.Text = ClsMessages.FetchGeneralButtonsText("btnReset");
            btnSelect.Text = ClsMessages.FetchGeneralMessage("1862");
        }
        #endregion

        #region Control Events
        protected void btnSave_Click(object sender, EventArgs e)
        {
         
            strRowId = hidPERSONAL_INFO_ID.Value;
            ClsIndividualInfo objIndividualInfo;
            //For The Save Case 
            try
            {
                if (strRowId.ToUpper().Equals("NEW"))
                {
                    objIndividualInfo = new ClsIndividualInfo();
                    this.GetFormValue(objIndividualInfo);
                    if (!SetCustomSecurityxml(objIndividualInfo.APPLICANT_ID.CurrentValue.ToString(), "SAVE"))
                        return;
                    objIndividualInfo.CUSTOMER_ID.CurrentValue = int.Parse(GetCustomerID());
                    objIndividualInfo.POLICY_ID.CurrentValue = int.Parse(GetPolicyID());
                    objIndividualInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(GetPolicyVersionID());

                    objIndividualInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                    objIndividualInfo.CREATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now);
                    objIndividualInfo.IS_ACTIVE.CurrentValue = "Y"; //hidIS_ACTIVE.Value;
                   
                 
              
                    int intRetval = ObjProducts.AddPersonalAccidentInfoData(objIndividualInfo, Convert.ToString(hidCALLED_FROM.Value));
     
                    if (intRetval > 0)
                    {
                        this.GetOldDataObject(objIndividualInfo.PERSONAL_INFO_ID.CurrentValue);

                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "29");
                        hidFormSaved.Value = "1";
                        btnActivateDeactivate.Enabled = true;
                        btnDelete.Enabled = true;
                        hidPERSONAL_INFO_ID.Value = Convert.ToString(objIndividualInfo.PERSONAL_INFO_ID.CurrentValue);
                        btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objIndividualInfo.IS_ACTIVE.CurrentValue.Trim());
                        strRowId = hidPERSONAL_INFO_ID.Value;

                    }
                    else if (intRetval == -1)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "18");
                        hidFormSaved.Value = "2";
                    }
                    //Added by Pradeep on 26-Aug-2011- Message for itrack-1566
                    else if (intRetval == -10)
                    {//-ONLY CPF is duplicated - "CPF já cadastrado."
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2058");
                        hidFormSaved.Value = "2";
                    }//else if (intRetval == -10)
                    else if (intRetval == -11)
                    {//ONLY RG is duplicated - "RG já cadastrado."
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2059");
                        hidFormSaved.Value = "2";
                    }//else if (intRetval == -11)
                    else if (intRetval == -12)
                    {//--ONLY CODE is duplicated - "CODE já cadastrado."
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2060");
                        hidFormSaved.Value = "2";
                    }//else if (intRetval == -12)
                    else if (intRetval == -13)
                    {//--CPF and RG are duplicated - "CPF e RG já cadastrados."
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2061");
                        hidFormSaved.Value = "2";
                    }//else if (intRetval == -13)
                    else if (intRetval == -14)
                    {//--CPF and CODE are duplicated - "CPF e CODE já cadastrados."
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2062");
                        hidFormSaved.Value = "2";
                    }//else if (intRetval == -14)
                    else if (intRetval == -15)
                    {//--CODE and RG are duplicated - "CODE e RG já cadastrados."
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2063");
                        hidFormSaved.Value = "2";
                    }//else if (intRetval == -15)
                    else if (intRetval == -16)
                    {//--CPF , RG and code are duplicated - "CPF, RG e Código já cadastrados."
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2064");
                        hidFormSaved.Value = "2";
                    }//else if (intRetval == -16)
                    //Added till here 
                    else if (intRetval == -5)
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1406") ;// ClsMessages.GetMessage(base.ScreenId, "18");
                        hidFormSaved.Value = "2";
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "20");
                        hidFormSaved.Value = "2";
                    }
                    lblDelete.Visible = false;
                    lblMessage.Visible = true;
                }

                else //For The Update cse
                {


                    objIndividualInfo = (ClsIndividualInfo)base.GetPageModelObject();
                    this.GetFormValue(objIndividualInfo);
                    //objIndividualInfo.IS_ACTIVE.CurrentValue = "Y"; //hidIS_ACTIVE.Value;
                    if (!SetCustomSecurityxml(objIndividualInfo.APPLICANT_ID.CurrentValue.ToString(), "SAVE"))
                        return;
                    objIndividualInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                    objIndividualInfo.LAST_UPDATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now);

                    int intRetval = ObjProducts.UpdatePersonalAccidentInfoData(objIndividualInfo, Convert.ToString(hidCALLED_FROM.Value));

                    if (intRetval > 0)
                    {
                        this.GetOldDataObject(objIndividualInfo.PERSONAL_INFO_ID.CurrentValue);
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("31");
                        hidPERSONAL_INFO_ID.Value = objIndividualInfo.PERSONAL_INFO_ID.CurrentValue.ToString();
                        hidFormSaved.Value = "1";
                        btnActivateDeactivate.Enabled = true;
                        btnDelete.Enabled = true;
                    }
                    else if (intRetval == -1)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "18");
                        hidFormSaved.Value = "2";
                    }
                    //Added by Pradeep on 26-Aug-2011- Message for itrack-1566
                    else if (intRetval == -10)
                    {//-ONLY CPF is duplicated - "CPF já cadastrado."
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2058");
                        hidFormSaved.Value = "2";
                    }//else if (intRetval == -10)
                    else if (intRetval == -11)
                    {//ONLY RG is duplicated - "RG já cadastrado."
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2059");
                        hidFormSaved.Value = "2";
                    }//else if (intRetval == -11)
                    else if (intRetval == -12)
                    {//--ONLY CODE is duplicated - "CODE já cadastrado."
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2060");
                        hidFormSaved.Value = "2";
                    }//else if (intRetval == -12)
                    else if (intRetval == -13)
                    {//--CPF and RG are duplicated - "CPF e RG já cadastrados."
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2061");
                        hidFormSaved.Value = "2";
                    }//else if (intRetval == -13)
                    else if (intRetval == -14)
                    {//--CPF and CODE are duplicated - "CPF e CODE já cadastrados."
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2062");
                        hidFormSaved.Value = "2";
                    }//else if (intRetval == -14)
                    else if (intRetval == -15)
                    {//--CODE and RG are duplicated - "CODE e RG já cadastrados."
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2063");
                        hidFormSaved.Value = "2";
                    }//else if (intRetval == -15)
                    else if (intRetval == -16)
                    {//--CPF , RG and code are duplicated - "CPF, RG e Código já cadastrados."
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2064");
                        hidFormSaved.Value = "2";
                    }//else if (intRetval == -16)
                    //Added till here 
                    else if (intRetval == -5)
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1406");// ClsMessages.GetMessage(base.ScreenId, "18");
                        hidFormSaved.Value = "2";
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "20");
                        hidFormSaved.Value = "2";
                    }
                    lblDelete.Visible = false;
                    lblMessage.Visible = true;
                   
                }
              
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";

            }
            finally
            {
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ClsIndividualInfo objIndividualInfo;
            try
            {
                objIndividualInfo = (ClsIndividualInfo)base.GetPageModelObject();

                objIndividualInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());

                int intRetval = ObjProducts.DeletePersonalAccidentInfoData(objIndividualInfo, Convert.ToString(hidCALLED_FROM.Value));
                if (intRetval > 0)
                {
                    lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "127");
                    hidFormSaved.Value = "1";
                    trBody.Attributes.Add("style", "display:none");
                }
                else if (intRetval == 0)
                {
                    lblDelete.Text = ClsMessages.FetchGeneralMessage("1889");
                    hidFormSaved.Value = "2";
                }
                else if (intRetval == -1)
                {
                    lblDelete.Text = ClsMessages.GetMessage(base.ScreenId, "128");
                    hidFormSaved.Value = "2";
                }
                lblDelete.Visible = true;
                lblMessage.Visible = false;
                hidPERSONAL_INFO_ID.Value = "";

            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";
            }
        }

        protected void btnActivateDeactivate_Click(object sender, EventArgs e)
        {
            ClsIndividualInfo objIndividualInfo;

            try
            {
                objIndividualInfo = (ClsIndividualInfo)base.GetPageModelObject();

                if (objIndividualInfo.IS_ACTIVE.CurrentValue == "Y")
                { objIndividualInfo.IS_ACTIVE.CurrentValue = "N"; }
                else
                { objIndividualInfo.IS_ACTIVE.CurrentValue = "Y"; }


                objIndividualInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                objIndividualInfo.LAST_UPDATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now);


                int intRetval = ObjProducts.ActivateDeactivatePersonalAccidentInfoData(objIndividualInfo, Convert.ToString(hidCALLED_FROM.Value));
                if (intRetval > 0)
                {
                    if (objIndividualInfo.IS_ACTIVE.CurrentValue == "N")
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "41");
                        btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objIndividualInfo.IS_ACTIVE.CurrentValue.ToString().Trim());
                       // itrack no 867
                       // btnActivateDeactivate.Visible = false;
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "40");
                        btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objIndividualInfo.IS_ACTIVE.CurrentValue.ToString().Trim());
                    }
                    hidFormSaved.Value = "1";

                    SetPageModelObject(objIndividualInfo);
                }
                lblDelete.Visible = false;
                lblMessage.Visible = true;


            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";

            }
        }
        //itrack 839
        protected void btnSelect_Click(object sender, EventArgs e)
        {

            //return;
            if ((cmbAPPLICANT_ID.SelectedIndex) > 0)
            {
                ClsIndividualInfo ObjIndividualInfo = new ClsIndividualInfo();
                ObjIndividualInfo = ObjProducts.FetchApplicantsInfoDetails(Convert.ToInt32((cmbAPPLICANT_ID.SelectedValue)), Convert.ToInt32(hidCUSTOMER_ID.Value), Convert.ToInt32(hidPOL_VERSION_ID.Value), Convert.ToInt32(hidPOL_ID.Value));
                PopulatePageFromEbixModelObject(this.Page, ObjIndividualInfo);

                // btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(ObjIndividualInfo.IS_ACTIVE.CurrentValue.ToString().Trim());

                base.SetPageModelObject(ObjIndividualInfo);
                if (ObjIndividualInfo.PERSONAL_INFO_ID.CurrentValue == 0)
                {
                    //btnActivateDeactivate.Visible = false;
                    //btnDelete.Visible = false;
                    hidPERSONAL_INFO_ID.Value = "NEW";
                }
                else
                {
                    hidPERSONAL_INFO_ID.Value = ObjIndividualInfo.PERSONAL_INFO_ID.CurrentValue.ToString();
                }
                strRowId = hidPERSONAL_INFO_ID.Value;
                if (ObjIndividualInfo.TYPE.CurrentValue.ToString() != "") {

                    hidTYPE.Value = ObjIndividualInfo.TYPE.CurrentValue.ToString();
                }
            }
        }
        #endregion
        [System.Web.Services.WebMethod]
        public static String GetInsuredObjectDetailsUsingInsuredId(Int32 PERSONAL_INFO_ID, Int32 CUSTOMER_ID, Int32 POLICY_ID, Int32 POLICY_VERSION_ID)
        {

            ClsProducts objProduct=new ClsProducts();
            String ReturnValue = String.Empty;
            DataSet ds = new DataSet();

            ds = objProduct.GetInsuredObjectdata(PERSONAL_INFO_ID, CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, "CODE");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ReturnValue = ds.Tables[0].Rows[0]["CODE"].ToString();
            }
            return ReturnValue;
        }
        private bool SetCustomSecurityxml(string CO_APP_ID, string CalledFrom)
        {
            bool Valid = true;
            if (hidCALLED_FROM.Value != "GRPLF" //for Group life 
                && hidCALLED_FROM.Value != "CPCACC")  //for group personal accident for passenger
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
    }
}
