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
using Cms.Model.Policy.Transportation;


namespace Cms.Policies.Aspx.Transportation
{

    public partial class AddCivilTransportationVehicleInfo : Cms.Policies.policiesbase
    {

        #region webcontrols Added on page


        protected Cms.CmsWeb.Controls.CmsButton btnReset;
        protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
        protected Cms.CmsWeb.Controls.CmsButton btnDelete;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;


        #endregion

        System.Resources.ResourceManager objResourceMgr;

        ClsProducts objProducts = new ClsProducts();
        private string strRowId = "";
        public string CalledFrom;
        public string PAGEFROM;
        public string FIRSTTAB;//in case of quickapp

        protected void Page_Load(object sender, EventArgs e)
        {
            #region setting screen id


            if (Request.QueryString["CalledFrom"] != null && Request.QueryString["CalledFrom"].ToString() != "")
                CalledFrom = Request.QueryString["CalledFrom"].ToString();

            switch (CalledFrom)
            {
                case "FLVEHICLEINFO": // Facaltative laibility
                    hidCALLED_FROM.Value = CalledFrom;
                    base.ScreenId = FACULTATIVE_LIABILITYscreenId.VEH_INFORMATION_PAGE;
                    hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCtl_FL");
                    // by praveer for itrack no:873
                    tdsubbranch.Visible = false;
                    tdcoapplicant.ColSpan = 2;

                    break;
                case "CLTVEHICLEINFO": //Civil Liability transportation
                    hidCALLED_FROM.Value = CalledFrom;
                    base.ScreenId = CIVIL_LIABILITY_TRANSPORTATIONscreenId.INFORMATION_PAGE;
                    hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCtl_CL");
                    // by praveer for itrack no:873
                    tdsubbranch.Visible = false;
                    tdcoapplicant.ColSpan = 2;
                    break;
                case "AERO": //Aeronautic Product
                    hidCALLED_FROM.Value = CalledFrom;
                    base.ScreenId = AERONAUTICscreenId.VEH_INFORMATION_PAGE;
                    hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCtl_FL");
                    btnMakeModel.Visible = false;
                    txtMAKE_MODEL.ReadOnly = false;
                    // by praveer for itrack no:873
                    tdsubbranch.Visible = false;
                    tdcoapplicant.ColSpan = 2;
                    break;
                case "MTOR":
                    hidCALLED_FROM.Value = CalledFrom;
                    base.ScreenId = MOTORscreenId.VEH_INFORMATION_PAGE;
                    hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCtl_FL");
                    // by praveer for itrack no:873
                    tdsubbranch.Visible = false;
                    tdcoapplicant.ColSpan = 2;
                    break;
                case "CTCL":////Cargo Civil Liability transportation
                    hidCALLED_FROM.Value = CalledFrom;
                    base.ScreenId = CARGO_TRANSPORTATION_CIVIL_LIABILITYscreenId.INFORMATION_PAGE;
                    hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCtl_FL");
                    // by praveer for itrack no:873
                    tdsubbranch.Visible = false;
                    tdcoapplicant.ColSpan = 2;
                    break;
                default:
                    base.ScreenId = FACULTATIVE_LIABILITYscreenId.VEH_INFORMATION_PAGE;
                    break;

            }

            if (Request.QueryString["PAGEFROM"] != null)
                PAGEFROM = Request.QueryString["PAGEFROM"].ToString();

            hidCUSTOMER_ID.Value = GetCustomerID();
            hidPOLICY_ID.Value = GetPolicyID();
            hidPOLICY_VERSION_ID.Value = GetPolicyVersionID();
            #endregion
            btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1333");
            this.SetErrorMessages();

            #region setting security Xml
            btnDelete.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Delete;
            btnDelete.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            btnActivateDeactivate.CmsButtonClass = CmsButtonType.Write;
            btnActivateDeactivate.PermissionString = gstrSecurityXML;

            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnMakeModel.CmsButtonClass = CmsButtonType.Write;
            btnMakeModel.PermissionString = gstrSecurityXML;

            #endregion

            hlkRISK_EFFECTIVE_DATE.Attributes.Add("OnClick", "fPopCalendar(document.POL_CIVIL_TRANSPORT_VEHICLES.txtRISK_EFFECTIVE_DATE,document.POL_CIVIL_TRANSPORT_VEHICLES.txtRISK_EFFECTIVE_DATE)"); //Javascript Implementation for Calender				
            hlkRISK_EXPIRE_DATE.Attributes.Add("OnClick", "fPopCalendar(document.POL_CIVIL_TRANSPORT_VEHICLES.txtRISK_EXPIRE_DATE,document.POL_CIVIL_TRANSPORT_VEHICLES.txtRISK_EXPIRE_DATE)");

            // txtMANDATORY_DEDUCTIBLE.Attributes.Add("onblur", "javascript:this.value=formatRate(this.value)");
            // txtFACULTATIVE_DEDUCTIBLE.Attributes.Add("onblur", "javascript:this.value=formatRate(this.value)");

            objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.Transportation.AddCivilTransportationVehicleInfo", System.Reflection.Assembly.GetExecutingAssembly());

            if (!Page.IsPostBack)
            {
                this.SetCaptions();
                this.Bindsubbranch();
                this.BindCategory();//Use to bind the category from lookup
                this.BindExceededPremium();
                PopulateCoApplicant();
                btnReset.Attributes.Add("onclick", "javascript: return ResetTheForm();");
                FIRSTTAB = ClsMessages.GetTabTitles("486", "TabCtl");
                if (Request.QueryString["VEHICLE_ID"] != null && Request.QueryString["VEHICLE_ID"].ToString() != "" && Request.QueryString["VEHICLE_ID"].ToString() != "NEW")
                {

                    hidVEHICLE_ID.Value = Request.QueryString["VEHICLE_ID"].ToString();

                    this.GetOldDataObject(Convert.ToInt32(Request.QueryString["VEHICLE_ID"].ToString()));

                }
                else //if (Request.QueryString["VEHICLE_ID"] == null)
                {
                    btnActivateDeactivate.Enabled = false;
                    btnDelete.Enabled = false;
                    hidVEHICLE_ID.Value = "NEW";
                    this.FillClientAndVehicleNumber();
                }
                //  cmbCATEGORY.Enabled = false;
                strRowId = hidVEHICLE_ID.Value;
            }
        }

        #region setcaption in resource file
        private void SetCaptions()
        {
            capMandatoryNotes.Text = objResourceMgr.GetString("capMandatoryNotes");
            capCLIENT_ORDER.Text = objResourceMgr.GetString("txtCLIENT_ORDER");
            capVEHICLE_NUMBER.Text = objResourceMgr.GetString("txtVEHICLE_NUMBER");
            capMANUFACTURED_YEAR.Text = objResourceMgr.GetString("txtMANUFACTURED_YEAR");
            capFIPE_CODE.Text = objResourceMgr.GetString("txtFIPE_CODE");
            btnMakeModel.Text = objResourceMgr.GetString("btnMakeModel");

            capCATEGORY.Text = objResourceMgr.GetString("cmbCATEGORY");
            capCAPACITY.Text = objResourceMgr.GetString("txtCAPACITY");
            capMAKE_MODEL.Text = objResourceMgr.GetString("txtMAKE_MODEL");
            capLICENSE_PLATE.Text = objResourceMgr.GetString("txtLICENSE_PLATE");
            capCHASSIS.Text = objResourceMgr.GetString("txtCHASSIS");

            // capMANDATORY_DEDUCTIBLE.Text = objResourceMgr.GetString("txtMANDATORY_DEDUCTIBLE");
            // capFACULTATIVE_DEDUCTIBLE.Text = objResourceMgr.GetString("txtFACULTATIVE_DEDUCTIBLE");
            capSUB_BRANCH.Text = objResourceMgr.GetString("cmbSUB_BRANCH");
            capRISK_EFFECTIVE_DATE.Text = objResourceMgr.GetString("txtRISK_EFFECTIVE_DATE");
            capRISK_EXPIRE_DATE.Text = objResourceMgr.GetString("txtRISK_EXPIRE_DATE");

            //  capREGION.Text = objResourceMgr.GetString("cmbREGION");
            capCOV_GROUP_CODE.Text = objResourceMgr.GetString("txtCOV_GROUP_CODE");
            capFINANCE_ADJUSTMENT.Text = objResourceMgr.GetString("txtFINANCE_ADJUSTMENT");
            capREFERENCE_PROPOSASL.Text = objResourceMgr.GetString("txtREFERENCE_PROPOSASL");
            capREMARKS.Text = objResourceMgr.GetString("txtREMARKS");
            capCoApplicant.Text = objResourceMgr.GetString("capCoApplicant");
            capZIP_CODE.Text = objResourceMgr.GetString("capZIP_CODE");
            capExceededPremium.Text = objResourceMgr.GetString("cmbExceeded_Premium");


        }   //private void SetCaptions()
        #endregion

        private void FillClientAndVehicleNumber()
        {

            String Numbers = GetMaxIdofClientOrderAndVehicleNumber(hidCUSTOMER_ID.Value, hidPOLICY_ID.Value, hidPOLICY_VERSION_ID.Value, "0", "0", "1");

            String[] Number = Numbers.Split(',');
            if (Number[0] != "0" && Number[1] != "0" & Number[2] == "1")
            {
                txtCLIENT_ORDER.Text = Number[0].ToString();
                txtVEHICLE_NUMBER.Text = Number[1].ToString();

            }

        }

        private void PopulateCoApplicant()
        {
            ClsCivilTransportVehicleInfo CivilTransportVehicleInfo = new ClsCivilTransportVehicleInfo();
            DataTable dtCO_APPLICANT_ID = CivilTransportVehicleInfo.FetchApplicants(Convert.ToInt32(hidCUSTOMER_ID.Value), Convert.ToInt32(hidPOLICY_VERSION_ID.Value), Convert.ToInt32(hidPOLICY_ID.Value)).Tables[0];
            DataView dvCO_APPLICANT_ID = dtCO_APPLICANT_ID.DefaultView;
            dvCO_APPLICANT_ID.Sort = "APPLICANT_NAME";
            cmbCO_APPLICANT_ID.DataSource = dvCO_APPLICANT_ID;
            cmbCO_APPLICANT_ID.DataTextField = "APPLICANT_NAME";
            cmbCO_APPLICANT_ID.DataValueField = "APPLICANT_ID";
            cmbCO_APPLICANT_ID.DataBind();
            cmbCO_APPLICANT_ID.Items.Insert(0, "");
            // changes by praveer for itrack no 900(Civil Liability transportation and Facaltative laibility  )
            if ((hidCALLED_FROM.Value == "CLTVEHICLEINFO" || hidCALLED_FROM.Value == "FLVEHICLEINFO") && GetTransaction_Type().Trim() == MASTER_POLICY && GetPolicyStatus().Trim().ToUpper() == POLICY_STATUS_UNDER_ENDORSEMENT)
            {
                if ( GetEndorsementCoApplicant().Trim() != "" && GetEndorsementCoApplicant().Trim() != null)
                {
                    cmbCO_APPLICANT_ID.SelectedValue = GetEndorsementCoApplicant().Trim();
                }
            }

            else
            {
                string ApplicantID = "";
                foreach (DataRow dr in dtCO_APPLICANT_ID.Rows)
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

        private void BindExceededPremium()
        {
            cmbExceeded_Premium.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("yesno");
            cmbExceeded_Premium.DataTextField = "LookupDesc";
            cmbExceeded_Premium.DataValueField = "LookupID";
            cmbExceeded_Premium.DataBind();
        }

        private void BindCategory()
        {
            //Modified by Pradeep Kushwaha on 20-Oct-2010 
            IList ilist = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CTEGY");
            DataTable dt = new DataTable();
            if (ilist[0] == null)
                throw new FormatException("Parameter ArrayList empty");
            dt.TableName = ilist[0].GetType().Name;
            dt.Columns.Add("LookupID", typeof(string));
            dt.Columns.Add("LookupDesc", typeof(string));
            DataRow dr;
            System.Reflection.PropertyInfo[] propInfo = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CTEGY")[0].GetType().GetProperties();
            for (int row = 0; row < ilist.Count; row++)
            {
                dr = dt.NewRow();
                object tempObject = ilist[row];
                object LookupID = propInfo[0].GetValue(tempObject, null);
                object LookupDesc = propInfo[2].GetValue(tempObject, null) + " - " + propInfo[1].GetValue(tempObject, null);
                if (LookupID != null)
                    dr[0] = LookupID.ToString();
                if (LookupDesc != null)
                    dr[1] = LookupDesc.ToString();
                dt.Rows.Add(dr);
            }
            DataView dvCATEGORY = dt.DefaultView;
            dvCATEGORY.Sort = "LookupDesc";
            cmbCATEGORY.DataSource = dvCATEGORY;
            cmbCATEGORY.DataTextField = "LookupDesc";
            cmbCATEGORY.DataValueField = "LookupID";
            cmbCATEGORY.DataBind();
            cmbCATEGORY.Items.Insert(0, "");
        }// private void BindMultiDeductible()
        /// <summary>
        /// Use to set the error messages on the controls for validation  
        /// </summary>
        private void SetErrorMessages()
        {
            ClsMessages.SetCustomizedXml(GetLanguageCode());
            revCLIENT_ORDER.ValidationExpression = aRegExpInteger;
            revCLIENT_ORDER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");

            revMANUFACTURED_YEAR.ValidationExpression = aRegExpInteger;
            revMANUFACTURED_YEAR.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "163");

            revVEHICLE_NUMBER.ValidationExpression = aRegExpInteger;
            revVEHICLE_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "7");

            //revMANUFACTURED_YEAR.ValidationExpression = aRegExpInteger;
            this.csvMANUFACTURED_YEAR.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "9");
            //revMANUFACTURED_YEAR.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "9");

            revFIPE_CODE.ValidationExpression = aRegExpTextArea100;
            revFIPE_CODE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "11");

            revCAPACITY.ValidationExpression = aRegExpTextArea100;
            revCAPACITY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "12");

            revLICENSE_PLATE.ValidationExpression = aRegExpTextArea100;
            revLICENSE_PLATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "13");

            revCHASSIS.ValidationExpression = aRegExpTextArea100;
            revCHASSIS.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "14");

            //revMANDATORY_DEDUCTIBLE.ValidationExpression = aRegExpDoublePositiveWithZero;
            //revMANDATORY_DEDUCTIBLE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "15");

            //revFACULTATIVE_DEDUCTIBLE.ValidationExpression = aRegExpDoublePositiveWithZero;
            //revFACULTATIVE_DEDUCTIBLE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "16");

            revRISK_EFFECTIVE_DATE.ValidationExpression = aRegExpDate;
            revRISK_EFFECTIVE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "18");

            revRISK_EXPIRE_DATE.ValidationExpression = aRegExpDate;
            revRISK_EXPIRE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "20");

            revCOV_GROUP_CODE.ValidationExpression = aRegExpTextArea100;
            revCOV_GROUP_CODE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "21");

            revFINANCE_ADJUSTMENT.ValidationExpression = aRegExpTextArea100;
            revFINANCE_ADJUSTMENT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "22");

            revREFERENCE_PROPOSASL.ValidationExpression = aRegExpTextArea100;
            revREFERENCE_PROPOSASL.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "23");


            //this.csvREMARKS.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "24");

            rfvMANUFACTURED_YEAR.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "8");
            rfvFIPE_CODE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "10");
            rfvRISK_EFFECTIVE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "17");
            rfvRISK_EXPIRE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "19");
            hidMsg.Value = ClsMessages.GetMessage(base.ScreenId, "25");
            cvRISK_EXPIRE_DATE.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "27");
            rfvCLIENT_ORDER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "4");
            rfvVEHICLE_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "6");
            hidCliendOrderMsg.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "29");
            hidVehicleMsg.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "30");
            rfvLICENSE_PLATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "13");
            rfvCHASSIS.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "14");
            rfvCO_APPLICANT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1179");
            rfvZIP_CODE.ErrorMessage = ClsMessages.FetchGeneralMessage("37");
            revZIP_CODE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1181"); //Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"24");
            revZIP_CODE.ValidationExpression = aRegExpZipBrazil;

            hidZIP_CodeMsg.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1373");
        }

        /// <summary>
        /// Use to Get the Old Data Based on the ID
        /// </summary>
        /// <param name="Vehicle_ID"></param>
        private void GetOldDataObject(Int32 Vehicle_ID)
        {
            ClsCivilTransportVehicleInfo ObjCivilTransportVehicleInfo = new ClsCivilTransportVehicleInfo();

            ObjCivilTransportVehicleInfo.VEHICLE_ID.CurrentValue = Vehicle_ID;
            ObjCivilTransportVehicleInfo.CUSTOMER_ID.CurrentValue = int.Parse(GetCustomerID());
            ObjCivilTransportVehicleInfo.POLICY_ID.CurrentValue = int.Parse(GetPolicyID());
            ObjCivilTransportVehicleInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(GetPolicyVersionID());
            string policystatus = GetPolicyStatus();
            if (objProducts.FetchCivilTransportVehicleInfoData(ref ObjCivilTransportVehicleInfo))
            {
                PopulatePageFromEbixModelObject(this.Page, ObjCivilTransportVehicleInfo);

                hidCATEGORY.Value = ObjCivilTransportVehicleInfo.CATEGORY.CurrentValue.ToString();
                cmbCATEGORY.SelectedIndex = cmbCATEGORY.Items.IndexOf(cmbCATEGORY.Items.FindByValue(hidCATEGORY.Value.ToString()));
                hidCAPACITY.Value = ObjCivilTransportVehicleInfo.CAPACITY.CurrentValue.ToString();
                txtCAPACITY.Text = ObjCivilTransportVehicleInfo.CAPACITY.CurrentValue.ToString();

                txtCLIENT_ORDER.Text = ObjCivilTransportVehicleInfo.CLIENT_ORDER.CurrentValue.ToString();
                txtVEHICLE_NUMBER.Text = ObjCivilTransportVehicleInfo.VEHICLE_NUMBER.CurrentValue.ToString();
                hidOLD_CLIENT_ORDER.Value = ObjCivilTransportVehicleInfo.CLIENT_ORDER.CurrentValue.ToString();
                hidOLD_VEHICLE_NUMBER.Value = ObjCivilTransportVehicleInfo.VEHICLE_NUMBER.CurrentValue.ToString();
                txtZIP_CODE.Text = ObjCivilTransportVehicleInfo.ZIP_CODE.CurrentValue.ToString();

                if (CalledFrom.ToString() != "AERO")
                {
                    cmbCATEGORY.Enabled = false;
                    txtCAPACITY.Enabled = false;
                }
                btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(ObjCivilTransportVehicleInfo.IS_ACTIVE.CurrentValue.ToString().Trim());

                // ITRACK NO 867
                string originalversion = ObjCivilTransportVehicleInfo.ORIGINAL_VERSION_ID.CurrentValue.ToString();
                if (originalversion != GetPolicyVersionID() && policystatus == "UENDRS" && originalversion != "0")
                {
                    btnActivateDeactivate.Visible = false;
                }
                SetCustomSecurityxml(ObjCivilTransportVehicleInfo.CO_APPLICANT_ID.CurrentValue.ToString(), "");
                base.SetPageModelObject(ObjCivilTransportVehicleInfo);
                hidCO_APPLICANT_ID.Value = ObjCivilTransportVehicleInfo.CO_APPLICANT_ID.CurrentValue.ToString();
            }//if (objProducts.FetchCivilTransportVehicleInfoData(ref ObjCivilTransportVehicleInfo))

        }//private void GetOldDataObject(Int32 Vehicle_ID)

        /// <summary>
        /// Use to set the form controls values in page model
        /// </summary>
        /// <param name="ObjCivilTransportVehicleInfo"></param>
        private void GetFormValue(ClsCivilTransportVehicleInfo ObjCivilTransportVehicleInfo)
        {
            ObjCivilTransportVehicleInfo.ORIGINAL_VERSION_ID.CurrentValue = 0;// int.Parse(GetPolicyVersionID());
            if (txtCLIENT_ORDER.Text.ToString().Trim() != "")
                ObjCivilTransportVehicleInfo.CLIENT_ORDER.CurrentValue = Convert.ToDouble(txtCLIENT_ORDER.Text);
            else
                ObjCivilTransportVehicleInfo.CLIENT_ORDER.CurrentValue = base.GetEbixDoubleDefaultValue();

            if (txtVEHICLE_NUMBER.Text.ToString().Trim() != "")
                ObjCivilTransportVehicleInfo.VEHICLE_NUMBER.CurrentValue = Convert.ToDouble(txtVEHICLE_NUMBER.Text);
            else
                ObjCivilTransportVehicleInfo.VEHICLE_NUMBER.CurrentValue = base.GetEbixDoubleDefaultValue();

            if (txtMANUFACTURED_YEAR.Text.ToString().Trim() != "")
                ObjCivilTransportVehicleInfo.MANUFACTURED_YEAR.CurrentValue = Convert.ToInt32(txtMANUFACTURED_YEAR.Text);
            else
                ObjCivilTransportVehicleInfo.MANUFACTURED_YEAR.CurrentValue = base.GetEbixIntDefaultValue();

            if (txtFIPE_CODE.Text.ToString().Trim() != "")
                ObjCivilTransportVehicleInfo.FIPE_CODE.CurrentValue = Convert.ToString(txtFIPE_CODE.Text);
            else
                ObjCivilTransportVehicleInfo.FIPE_CODE.CurrentValue = String.Empty;

            if (cmbExceeded_Premium.SelectedValue != "")
                ObjCivilTransportVehicleInfo.EXCEEDED_PREMIUM.CurrentValue = int.Parse(cmbExceeded_Premium.SelectedValue);
            else
                ObjCivilTransportVehicleInfo.EXCEEDED_PREMIUM.CurrentValue = GetEbixIntDefaultValue();


            if (hidCALLED_FROM.Value.ToString() == "AERO")
            {
                if (cmbCATEGORY.SelectedValue != "")
                    ObjCivilTransportVehicleInfo.CATEGORY.CurrentValue = Convert.ToInt32(cmbCATEGORY.SelectedValue);
                else
                    ObjCivilTransportVehicleInfo.CATEGORY.CurrentValue = base.GetEbixIntDefaultValue();

                if (txtCAPACITY.Text.ToString().Trim() != "")
                    ObjCivilTransportVehicleInfo.CAPACITY.CurrentValue = Convert.ToString(txtCAPACITY.Text);
                else
                    ObjCivilTransportVehicleInfo.CAPACITY.CurrentValue = String.Empty;

                if (txtMAKE_MODEL.Text.ToString().Trim() != "")
                    ObjCivilTransportVehicleInfo.MAKE_MODEL.CurrentValue = Convert.ToString(txtMAKE_MODEL.Text);
                else
                    ObjCivilTransportVehicleInfo.MAKE_MODEL.CurrentValue = String.Empty;
            }
            else
            {
                if (hidCATEGORY.Value != "")
                    ObjCivilTransportVehicleInfo.CATEGORY.CurrentValue = Convert.ToInt32(hidCATEGORY.Value);
                else
                    ObjCivilTransportVehicleInfo.CATEGORY.CurrentValue = base.GetEbixIntDefaultValue();

                if (hidCAPACITY.Value != "")
                    ObjCivilTransportVehicleInfo.CAPACITY.CurrentValue = Convert.ToString(hidCAPACITY.Value);
                else
                    ObjCivilTransportVehicleInfo.CATEGORY.CurrentValue = base.GetEbixIntDefaultValue();

                if (hidMAKE_MODEL.Value.ToString().Trim() != "")
                    ObjCivilTransportVehicleInfo.MAKE_MODEL.CurrentValue = Convert.ToString(hidMAKE_MODEL.Value);
                else
                    ObjCivilTransportVehicleInfo.MAKE_MODEL.CurrentValue = String.Empty;
            }

            if (txtLICENSE_PLATE.Text.ToString().Trim() != "")
                ObjCivilTransportVehicleInfo.LICENSE_PLATE.CurrentValue = Convert.ToString(txtLICENSE_PLATE.Text);
            else
                ObjCivilTransportVehicleInfo.LICENSE_PLATE.CurrentValue = String.Empty;

            if (txtCHASSIS.Text.ToString().Trim() != "")
                ObjCivilTransportVehicleInfo.CHASSIS.CurrentValue = Convert.ToString(txtCHASSIS.Text);
            else
                ObjCivilTransportVehicleInfo.CHASSIS.CurrentValue = String.Empty;

            // if (txtMANDATORY_DEDUCTIBLE.Text.ToString().Trim() != "")
            //    ObjCivilTransportVehicleInfo.MANDATORY_DEDUCTIBLE.CurrentValue = ConvertEbixDoubleValue(txtMANDATORY_DEDUCTIBLE.Text);
            // else
            // ObjCivilTransportVehicleInfo.MANDATORY_DEDUCTIBLE.CurrentValue = base.GetEbixDoubleDefaultValue();

            // if (txtFACULTATIVE_DEDUCTIBLE.Text.ToString().Trim() != "")
            // ObjCivilTransportVehicleInfo.FACULTATIVE_DEDUCTIBLE.CurrentValue = ConvertEbixDoubleValue(txtFACULTATIVE_DEDUCTIBLE.Text);
            // else
            // ObjCivilTransportVehicleInfo.FACULTATIVE_DEDUCTIBLE.CurrentValue =  base.GetEbixDoubleDefaultValue();

            if ((cmbSUB_BRANCH.SelectedItem != null) && (cmbSUB_BRANCH.SelectedItem.Text.ToString().Trim() != ""))
                ObjCivilTransportVehicleInfo.SUB_BRANCH.CurrentValue = Convert.ToInt32(cmbSUB_BRANCH.SelectedItem.Value);
            else
                ObjCivilTransportVehicleInfo.SUB_BRANCH.CurrentValue = Convert.ToInt32(null);

            //if ((cmbREGION.SelectedItem != null) && (cmbREGION.SelectedItem.Text.ToString().Trim() != ""))
            //    ObjCivilTransportVehicleInfo.REGION.CurrentValue = Convert.ToInt32(cmbREGION.SelectedItem.Value);
            //else
            //    ObjCivilTransportVehicleInfo.REGION.CurrentValue = base.GetEbixIntDefaultValue();

            if (txtRISK_EFFECTIVE_DATE.Text.ToString().Trim() != "")
                ObjCivilTransportVehicleInfo.RISK_EFFECTIVE_DATE.CurrentValue = ConvertToDate(txtRISK_EFFECTIVE_DATE.Text);
            else
                ObjCivilTransportVehicleInfo.RISK_EFFECTIVE_DATE.CurrentValue = ConvertToDate(null);

            if (txtRISK_EXPIRE_DATE.Text.ToString().Trim() != "")
                ObjCivilTransportVehicleInfo.RISK_EXPIRE_DATE.CurrentValue = ConvertToDate(txtRISK_EXPIRE_DATE.Text);
            else
                ObjCivilTransportVehicleInfo.RISK_EXPIRE_DATE.CurrentValue = ConvertToDate(null);

            if (txtCOV_GROUP_CODE.Text.ToString().Trim() != "")
                ObjCivilTransportVehicleInfo.COV_GROUP_CODE.CurrentValue = Convert.ToString(txtCOV_GROUP_CODE.Text);
            else
                ObjCivilTransportVehicleInfo.COV_GROUP_CODE.CurrentValue = String.Empty;

            if (txtFINANCE_ADJUSTMENT.Text.ToString().Trim() != "")
                ObjCivilTransportVehicleInfo.FINANCE_ADJUSTMENT.CurrentValue = Convert.ToString(txtFINANCE_ADJUSTMENT.Text);
            else
                ObjCivilTransportVehicleInfo.FINANCE_ADJUSTMENT.CurrentValue = String.Empty;

            if (txtREFERENCE_PROPOSASL.Text.ToString().Trim() != "")
                ObjCivilTransportVehicleInfo.REFERENCE_PROPOSASL.CurrentValue = Convert.ToString(txtREFERENCE_PROPOSASL.Text);
            else
                ObjCivilTransportVehicleInfo.REFERENCE_PROPOSASL.CurrentValue = String.Empty;

            if (txtREMARKS.Text.ToString().Trim() != "")
                ObjCivilTransportVehicleInfo.REMARKS.CurrentValue = Convert.ToString(txtREMARKS.Text);
            else
                ObjCivilTransportVehicleInfo.REMARKS.CurrentValue = String.Empty;

            if (cmbCO_APPLICANT_ID.SelectedValue != null)
                ObjCivilTransportVehicleInfo.CO_APPLICANT_ID.CurrentValue = Convert.ToInt32(cmbCO_APPLICANT_ID.SelectedValue);
            else
                ObjCivilTransportVehicleInfo.CO_APPLICANT_ID.CurrentValue = base.GetEbixIntDefaultValue();


            if (txtZIP_CODE.Text.ToString().Trim() != "")
                ObjCivilTransportVehicleInfo.ZIP_CODE.CurrentValue = Convert.ToString(txtZIP_CODE.Text);
            else
                ObjCivilTransportVehicleInfo.ZIP_CODE.CurrentValue = String.Empty;

            ObjCivilTransportVehicleInfo.CALLED_FROM.CurrentValue = hidCALLED_FROM.Value;

        }//private void GetFormValue(ClsCivilTransportVehicleInfo ObjCivilTransportVehicleInfo)


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

        }//private void InitializeComponent()

        protected void btnActivateDeactivate_Click(object sender, EventArgs e)
        {
            ClsCivilTransportVehicleInfo ObjCivilTransportVehicleInfo;

            try
            {
                ObjCivilTransportVehicleInfo = (ClsCivilTransportVehicleInfo)base.GetPageModelObject();

                if (ObjCivilTransportVehicleInfo.IS_ACTIVE.CurrentValue == "Y")
                { ObjCivilTransportVehicleInfo.IS_ACTIVE.CurrentValue = "N"; }
                else
                { ObjCivilTransportVehicleInfo.IS_ACTIVE.CurrentValue = "Y"; }


                ObjCivilTransportVehicleInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                ObjCivilTransportVehicleInfo.LAST_UPDATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now);


                int intRetval = objProducts.ActivateDeactivateCivilTransportVehicleInfo(ObjCivilTransportVehicleInfo, Convert.ToString(hidCALLED_FROM.Value));
                if (intRetval > 0)
                {
                    if (ObjCivilTransportVehicleInfo.IS_ACTIVE.CurrentValue == "N")
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("9");
                        //itrack no 867
                        // btnActivateDeactivate.Visible = false;
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("7");
                    }
                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(ObjCivilTransportVehicleInfo.IS_ACTIVE.CurrentValue.ToString().Trim());
                    hidFormSaved.Value = "1";

                    base.SetPageModelObject(ObjCivilTransportVehicleInfo);
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
        }//protected void btnActivateDeactivate_Click(object sender, EventArgs e)

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int intRetval;
            strRowId = hidVEHICLE_ID.Value;
            ClsCivilTransportVehicleInfo ObjCivilTransportVehicleInfo;
            try
            {
                //For The Save Case                
                if (strRowId.ToUpper().Equals("NEW"))
                {

                    ObjCivilTransportVehicleInfo = new ClsCivilTransportVehicleInfo();
                    this.GetFormValue(ObjCivilTransportVehicleInfo);
                    if (!SetCustomSecurityxml(ObjCivilTransportVehicleInfo.CO_APPLICANT_ID.CurrentValue.ToString(), "SAVE"))
                        return;
                    ObjCivilTransportVehicleInfo.CUSTOMER_ID.CurrentValue = int.Parse(GetCustomerID());
                    ObjCivilTransportVehicleInfo.POLICY_ID.CurrentValue = int.Parse(GetPolicyID());
                    ObjCivilTransportVehicleInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(GetPolicyVersionID());

                    ObjCivilTransportVehicleInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                    ObjCivilTransportVehicleInfo.CREATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now);
                    ObjCivilTransportVehicleInfo.IS_ACTIVE.CurrentValue = "Y";
                    FIRSTTAB = ClsMessages.GetTabTitles("486", "TabCtl");
                    intRetval = objProducts.AddCivilTransportVehicleInfo(ObjCivilTransportVehicleInfo, Convert.ToString(hidCALLED_FROM.Value));

                    if (intRetval == 1)
                    {
                        this.GetOldDataObject(ObjCivilTransportVehicleInfo.VEHICLE_ID.CurrentValue);

                        hidVEHICLE_ID.Value = ObjCivilTransportVehicleInfo.VEHICLE_ID.CurrentValue.ToString();
                        btnActivateDeactivate.Enabled = true;
                        btnDelete.Enabled = true;
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("29");
                        hidFormSaved.Value = "1";
                        base.OpenEndorsementDetails();


                    }
                    else if (intRetval == -2)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "25");
                        hidFormSaved.Value = "2";
                    }
                    else if (intRetval == -3)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "28");
                        hidFormSaved.Value = "2";
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                        hidFormSaved.Value = "2";
                    }
                    lblDelete.Visible = false;
                    lblMessage.Visible = true;
                } // if (strRowId.ToUpper().Equals("NEW"))
                else //For The Update cse
                {


                    ObjCivilTransportVehicleInfo = (ClsCivilTransportVehicleInfo)base.GetPageModelObject();
                    this.GetFormValue(ObjCivilTransportVehicleInfo);

                    if (!SetCustomSecurityxml(ObjCivilTransportVehicleInfo.CO_APPLICANT_ID.CurrentValue.ToString(), "SAVE"))
                        return;
                    ObjCivilTransportVehicleInfo.IS_ACTIVE.CurrentValue = "Y";
                    ObjCivilTransportVehicleInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                    ObjCivilTransportVehicleInfo.LAST_UPDATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now);

                    intRetval = objProducts.UpdateCivilTransportVehicleInfo(ObjCivilTransportVehicleInfo, Convert.ToString(hidCALLED_FROM.Value));

                    if (intRetval == 1)
                    {
                        this.GetOldDataObject(ObjCivilTransportVehicleInfo.VEHICLE_ID.CurrentValue);
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("31");
                        hidFormSaved.Value = "1";
                        base.OpenEndorsementDetails();

                    }
                    else if (intRetval == -2)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "25");
                        hidFormSaved.Value = "2";
                    }
                    else if (intRetval == -3)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "28");
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
                //if (ObjCivilTransportVehicleInfo != null)
                //    ObjCivilTransportVehicleInfo.Dispose();
            }
        }//protected void btnSave_Click(object sender, EventArgs e)

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ClsCivilTransportVehicleInfo ObjCivilTransportVehicleInfo;
            try
            {

                ObjCivilTransportVehicleInfo = (ClsCivilTransportVehicleInfo)base.GetPageModelObject();

                ObjCivilTransportVehicleInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());

                int intRetval = objProducts.DeleteCivilTransportVehicleInfo(ObjCivilTransportVehicleInfo, Convert.ToString(hidCALLED_FROM.Value));
                if (intRetval > 0)
                {
                    lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "127");
                    hidVEHICLE_ID.Value = "";
                    hidFormSaved.Value = "1";
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
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "2") + " - " + ex.Message;
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";

            }
        }////protected void btnDelete_Click(object sender, EventArgs e)


        /// <summary>
        /// Use for Populate the MakeModel Based on the FipeCode enter by user 
        /// This Method use throught jQuery to populate the makemodel textbox based on fipecode
        /// </summary>
        /// <param name="FipeCode"></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        public static String GetMakeModelUsingFipeCode(String FipeCode)
        {

            CmsWeb.webservices.ClsWebServiceCommon obj = new CmsWeb.webservices.ClsWebServiceCommon();  //Declare the object ob common webservice

            String MakeModel = String.Empty;//Default value null

            MakeModel = obj.GetMakeModelUsingFipeCode(FipeCode);//Call the webservice mathod , to get the MakeModel value based on FipeCode

            //cmbCONSTRUCTION.SelectedIndex = cmbCONSTRUCTION.Items.IndexOf(cmbCONSTRUCTION.Items.FindByValue(ds.Tables[0].Rows[0]["CONSTRUCTION"].ToString()));

            return MakeModel;//return the makeModel Value (if make model exist otherwise null)
        }

        [System.Web.Services.WebMethod]
        public static String GetMaxIdofClientOrderAndVehicleNumber(String CUSTOMER_ID, String POLICY_ID, String POLICY_VERSION_ID, String CLIENTORDER, String VEHICLENUMBER, String flag)
        {

            CmsWeb.webservices.ClsWebServiceCommon obj = new CmsWeb.webservices.ClsWebServiceCommon();  //Declare the object ob common webservice

            String Number = String.Empty;
            Int64 CLIENT_ORDER = 0;
            Int64 VEHICLE_NUMBER = 0;
            CLIENT_ORDER = Convert.ToInt64(CLIENTORDER);
            VEHICLE_NUMBER = Convert.ToInt64(VEHICLENUMBER);

            Number = obj.GetTheMaxIdOFClientOrderAndVehicleNumber(Convert.ToInt32(CUSTOMER_ID), Convert.ToInt32(POLICY_ID), Convert.ToInt32(POLICY_VERSION_ID), ref CLIENT_ORDER, ref VEHICLE_NUMBER, Convert.ToInt32(flag));
            if (Number == "1" && flag == "1")
                Number = CLIENT_ORDER + "," + VEHICLE_NUMBER + "," + flag + ",false";
            else
                Number = CLIENT_ORDER + "," + VEHICLE_NUMBER + "," + flag + "," + Number + ",true";

            return Number;
        }


        private void Bindsubbranch()
        {
            DataTable dtSUB_BRANCH = Cms.BusinessLayer.BlCommon.ClsEndorsmentDetails.GetSUBLOBs("17", GetLanguageID()).Tables[0];
            DataView dvSUB_BRANCH = dtSUB_BRANCH.DefaultView;
            dvSUB_BRANCH.Sort = "SUB_LOB_DESC";
            cmbSUB_BRANCH.DataSource = dvSUB_BRANCH;
            cmbSUB_BRANCH.DataTextField = "SUB_LOB_DESC";
            cmbSUB_BRANCH.DataValueField = "SUB_LOB_ID";
            cmbSUB_BRANCH.DataBind();
            cmbSUB_BRANCH.Items.Insert(0, "");

        } //private void Bindsubbranch()


        //Added by Pradeep Kushwaha on 20-Jan-2011
        /// <summary>
        /// Verify the address based on zipe code
        /// </summary>
        /// <param name="ZIPCODE">Zipe Code</param>
        /// <param name="COUNTRYID">Contry id</param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        public static String GetValidateZipeCode(String ZIPCODE, String COUNTRYID)
        {

            CmsWeb.webservices.ClsWebServiceCommon obj = new CmsWeb.webservices.ClsWebServiceCommon();  //Declare the object ob common webservice

            ZIPCODE = ZIPCODE.Replace("-", "");
            String ReturnValue = String.Empty;
            DataSet ds = new DataSet();

            ReturnValue = obj.GetAddressDetailsBasedOnZipeCode(ZIPCODE, Convert.ToInt32(COUNTRYID));


            return ReturnValue;
        }
        private bool SetCustomSecurityxml(string CO_APP_ID, string CALLED_FROM)
        {
            bool Valid = true;
            string CalledFrom = Request.QueryString["CalledFrom"].ToString();
            hidCALLED_FROM.Value = CalledFrom;

            if (CalledFrom != "CLTVEHICLEINFO" && CalledFrom !=  "FLVEHICLEINFO" ) //for personal accident for passenges
                return Valid;

            if (GetTransaction_Type().Trim() == MASTER_POLICY)
                if (CALLED_FROM.ToUpper() == "SAVE" && GetPolicyStatus().Trim().ToUpper() == POLICY_STATUS_UNDER_ENDORSEMENT)
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
