using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cms.Model.Claims;
using System.Web.UI.HtmlControls;

using Cms.CmsWeb.Controls;
using Cms.CmsWeb;
using Cms.ExceptionPublisher;
using Cms.BusinessLayer.BLClaims;
using Cms.BusinessLayer.BlCommon;

using System.Data;
using System.Globalization;
using System.Collections;

namespace Cms.Claims.Aspx
{
    public partial class AddReserveDetails : Cms.Claims.ClaimBase
    {
        protected Cms.CmsWeb.WebControls.ClaimActivityTop cltClaimActivityTop;
        protected Cms.CmsWeb.WebControls.ClaimTop cltClaimTop;	
        protected string ActivityClientID, ActivityTotalPaymentClientID;
        protected  string NavigateURLPart1;
        protected  string IsVictimEnabled="N";
        protected string DeductibleAmountMessage = "";
       

        double TotalPaymentPayment = 0;
        double TotalRecoveryPayment = 0;
        double TotalOutstandingAmount = 0;
        // FOLLOWING VARIABLE IS USED TO CHECK PAYMENT AMOUNT IS NOT GREATER THEN 20 % OF COVERAGE LIMIT 
        const int DivisionPercenatage = 20;
        int HasErrorOccured = 0;

        ClsAddReserveDetails objAddReserveDetails = new ClsAddReserveDetails();
        System.Resources.ResourceManager objResourceMgr;

        public NumberFormatInfo nfi;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Font.Bold = true;

            if (GetPolicyCurrency() != String.Empty && GetPolicyCurrency() == enumCurrencyId.BR)
                nfi = new CultureInfo(enumCulture.BR, true).NumberFormat;
            else
                nfi = new CultureInfo(enumCulture.US, true).NumberFormat;

            hidPAGE_CULTURE.Value = GetPolicyCurrency();
           
            base.ScreenId = "306_15";
           
            //START:*********** Setting permissions and class (Read/write/execute/delete)  *************
             btnBackToActivity.CmsButtonClass = CmsButtonType.Write;
             btnBackToActivity.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            btnSaveAndContinue.CmsButtonClass = CmsButtonType.Write;
            btnSaveAndContinue.PermissionString = gstrSecurityXML;

            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            //END:*********** Setting permissions and class (Read/write/execute/delete)  *************
           objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.AddReserveDetails", System.Reflection.Assembly.GetExecutingAssembly());


         

            if (!Page.IsPostBack)
            {
                string LobID = GetLOBID();
                if (LobID == ((int)enumLOB.FACLIAB).ToString().ToString() || LobID == ((int)enumLOB.CVLIABTR).ToString().ToString() )
                {
                    chkPersonalInjury.Visible = true;
                    CapPersonalInjury.Visible = true;
                }
                else
                {
                    chkPersonalInjury.Visible = false;
                    CapPersonalInjury.Visible = false;
                }
                pnlPersonalInjury.Visible = false;
                hidQueryString.Value = Request.Url.Query;
                SetCaptions();
                GetQueryStringValues();
                SetErrorMessages();
                GetOldDataObject();
               
            }

           
            //ActivityClientID = cltClaimActivityTop.PanelClientID;
            //ActivityTotalPaymentClientID = cltClaimActivityTop.PanelPaymentClientID;
            //SetClaimActivityTop();
            //SetClaimTop();
        }

        private void SetClaimActivityTop()
        {
            if (hidCLAIM_ID.Value != "" && hidCLAIM_ID.Value != "0")
            {
                cltClaimActivityTop.ClaimID = int.Parse(hidCLAIM_ID.Value);
            }
            if (hidACTIVITY_ID.Value != "" && hidACTIVITY_ID.Value != "0")
            {
                cltClaimActivityTop.ActivityID = int.Parse(hidACTIVITY_ID.Value);
            }
            //cltClaimActivityTop.ActivityID = 1;
            //cltClaimActivityTop.ShowHeaderBand ="Claim";
            cltClaimActivityTop.Visible = true;
        }
        private void SetClaimTop()
        {

            if (hidCUSTOMER_ID.Value != "" && hidCUSTOMER_ID.Value != "0")
            {
                cltClaimTop.CustomerID = int.Parse(hidCUSTOMER_ID.Value);
            }

            if (hidPOLICY_ID.Value != "" && hidPOLICY_ID.Value != "0")
            {
                cltClaimTop.PolicyID = int.Parse(hidPOLICY_ID.Value);
            }

            if (hidPOLICY_VERSION_ID.Value != "" && hidPOLICY_VERSION_ID.Value != "0")
            {
                cltClaimTop.PolicyVersionID = int.Parse(hidPOLICY_VERSION_ID.Value);
            }
            if (hidCLAIM_ID.Value != "" && hidCLAIM_ID.Value != "0")
            {
                cltClaimTop.ClaimID = int.Parse(hidCLAIM_ID.Value);
            }
            if (hidLOB_ID.Value != "" && hidLOB_ID.Value != "0")
            {
                cltClaimTop.LobID = int.Parse(hidLOB_ID.Value);
            }

            cltClaimTop.ShowHeaderBand = "Claim";

            cltClaimTop.Visible = true;
        }

        private void SetCaptions()
        {

            hidPARENT_TAB_NAME.Value = ClsMessages.GetMessage(base.ScreenId, "7");
            lblTitle.Text = hidPARENT_TAB_NAME.Value;

            grdClaimCoverages.Columns[0].HeaderText  = objResourceMgr.GetString("COVERAGE_ID");
            grdClaimCoverages.Columns[1].HeaderText  = objResourceMgr.GetString("RISK_ID");
            grdClaimCoverages.Columns[2].HeaderText  = objResourceMgr.GetString("RESERVE_ID");
            grdClaimCoverages.Columns[3].HeaderText  = objResourceMgr.GetString("COV_DES");
            grdClaimCoverages.Columns[4].HeaderText  = objResourceMgr.GetString("LIMIT");

            CapPersonalInjury.Text                   = objResourceMgr.GetString("PERSONAL_INJURY");
            grdClaimCoverages.Columns[5].HeaderText  = objResourceMgr.GetString("DEDUCTIBLE");

            grdClaimCoverages.Columns[6].HeaderText = objResourceMgr.GetString("PREV_OUTSTANDING");
            grdClaimCoverages.Columns[7].HeaderText = objResourceMgr.GetString("OUTSTANDING");

            grdClaimCoverages.Columns[8].HeaderText  = objResourceMgr.GetString("OUTSTANDING");
            
            grdClaimCoverages.Columns[9].HeaderText = objResourceMgr.GetString("ADJUSTED_AMOUNT");
            grdClaimCoverages.Columns[10].HeaderText  = objResourceMgr.GetString("PAYMENT_AMOUNT");
            grdClaimCoverages.Columns[11].HeaderText = objResourceMgr.GetString("RECOVERY_AMOUNT");
            grdClaimCoverages.Columns[12].HeaderText = objResourceMgr.GetString("RI_RESERVE");
            grdClaimCoverages.Columns[13].HeaderText = objResourceMgr.GetString("CO_RESERVE");
            
            grdClaimCoverages.EmptyDataText = objResourceMgr.GetString("GridEmpty");
            LblPopupDetails.Text = objResourceMgr.GetString("LblPopupDetails");

            hidDeductibleAlertMessage.Value = ClsMessages.GetMessage(base.ScreenId, "6");
            hidRECOVERY_MESSAGE.Value = ClsMessages.GetMessage(base.ScreenId, "21");
            btnBackToActivity.Text = ClsMessages.GetMessage(base.ScreenId, "1");
            hidMESSAGE.Value = ClsMessages.GetMessage(base.ScreenId, "2");
            HidEmptyRecordMessage.Value = ClsMessages.GetMessage(base.ScreenId, "5");

            btnSaveAndContinue.Text = objResourceMgr.GetString("btnSaveAndContinue");
            
        }

        private void GetQueryStringValues()
        {

            if (Request.QueryString["CALLED_FROM"] != null && Request.QueryString["CALLED_FROM"].ToString() != "")
                hidCALLED_FROM.Value = Request.QueryString["CALLED_FROM"];

            if (Request.QueryString["CLAIM_ID"] != null && Request.QueryString["CLAIM_ID"].ToString() != "")
                hidCLAIM_ID.Value = Request.QueryString["CLAIM_ID"].ToString();

            if (Request.QueryString["CUSTOMER_ID"] != null && Request.QueryString["CUSTOMER_ID"].ToString() != "")
                hidCUSTOMER_ID.Value = Request.QueryString["CUSTOMER_ID"].ToString();

            if (Request.QueryString["POLICY_ID"] != null && Request.QueryString["POLICY_ID"].ToString() != "")
                hidPOLICY_ID.Value = Request.QueryString["POLICY_ID"].ToString();

            if (Request.QueryString["POLICY_VERSION_ID"] != null && Request.QueryString["POLICY_VERSION_ID"].ToString() != "")
                hidPOLICY_VERSION_ID.Value = Request.QueryString["POLICY_VERSION_ID"].ToString();

            //if (Request.QueryString["LOB_ID"] != null && Request.QueryString["LOB_ID"].ToString() != "")
            //    hidLOB_ID.Value = Request.QueryString["LOB_ID"].ToString();
            //else
                hidLOB_ID.Value = GetLOBID();

            if (Request.QueryString["ACTIVITY_ID"] != null && Request.QueryString["ACTIVITY_ID"].ToString() != "")
                hidACTIVITY_ID.Value = Request.QueryString["ACTIVITY_ID"].ToString();
            else
                hidACTIVITY_ID.Value="0";

        }

        private void SetErrorMessages()
        {
            //revZIP_CODE.ValidationExpression = aRegExpZip;
            //revEFFECTIVE_DATE.ValidationExpression = aRegExpDate;
            //revEXPIRE_DATE.ValidationExpression = aRegExpDate;



            //rfvPOL_PERSON_ID.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "9");
            //rfvVehicle_ID.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "10");
            //rfvPOL_VOYAGE_ID.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "8");
            //rfvPOL_VESSEL_ID.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "7");
            //rfvPOL_LOCATION_ID.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "6");
            //csvVESSEL_MANUFACTURED_YEAR.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "5");
            //csvVEHICLE_YEAR.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");

            //cvRISK_EXPIRE_DATE.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "11");
            //revEXPIRE_DATE.ErrorMessage = ClsMessages.FetchGeneralMessage("22");
            //revZIP_CODE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1084");
            //revVOYAGE_DEPARTURE_DATE.ErrorMessage = ClsMessages.FetchGeneralMessage("22");
            //revEFFECTIVE_DATE.ErrorMessage = ClsMessages.FetchGeneralMessage("22");
            //revVOYAGE_DEPARTURE_DATE.ValidationExpression = aRegExpDate;

        }

        private void GetOldDataObject()
        {
            ClsMessages.SetCustomizedXml(GetLanguageCode());
            string VictimPrefix = ClsMessages.GetMessage(base.ScreenId, "10");
            string ExpenseHeading = ClsMessages.GetMessage(base.ScreenId, "18");
            string PassengerPrefix = ClsMessages.GetMessage(base.ScreenId, "14");
            string Prefix="";
            DeductibleAmountMessage = ClsMessages.GetMessage(base.ScreenId, "11");

            hidIS_PAYMENT_EXPENSE_TYPE.Value = "";

            NavigateURLPart1 = "RICOIReserveDetails.aspx?CLAIM_ID=" + hidCLAIM_ID.Value + "&";

            string FetchMode = string.Empty;
            if (hidCALLED_FROM.Value.ToUpper().Equals("ACT"))
                FetchMode = "ACT";// FOR ACTIVITY SCREEN
            else
                FetchMode = "NTF"; // FOR NOTIFICATION

            

   


            DataSet ds = objAddReserveDetails.GetClaimCoveragesReserveDetails(int.Parse(hidCLAIM_ID.Value), int.Parse(hidACTIVITY_ID.Value),int.Parse(hidLOB_ID.Value),int.Parse(GetLanguageID()), FetchMode);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (hidACTIVITY_ID.Value=="" ||hidACTIVITY_ID.Value=="0" )
                    btnBackToActivity.Visible = false;


                // IF THIS SCREEN IS OPENED FOR Civil Liability Transportation OR Facultative Liability
                // THEN GROUP GRID ROWS FOR EACH VICTIM
                // AND ALSO PERSONAL INJURY CHECK BOX 
                if (hidLOB_ID.Value == ((int)enumLOB.FACLIAB).ToString().ToString() || hidLOB_ID.Value == ((int)enumLOB.CVLIABTR).ToString().ToString() || hidLOB_ID.Value == ((int)enumLOB.PAPEACC).ToString().ToString())
                {
                    if (ds.Tables[0].Rows[1]["IS_VICTIM_CLAIM"].ToString() == "Y")
                    {
                        IsVictimEnabled = "Y";
                        hidIsVictimEnabled.Value = "Y";

                        if (hidLOB_ID.Value == ((int)enumLOB.PAPEACC).ToString().ToString())
                            Prefix = PassengerPrefix;
                        else
                            Prefix = VictimPrefix;
                    }
                }


                // TEMP CODE 

                DataTable dtTemp = ds.Tables[0];
                string PrevVictim="";
                string CurrentVictim = "";
                int RowCount = ds.Tables[0].Rows.Count;
                DataRow dtRow;
                if(IsVictimEnabled=="Y")
                {


                    string IsRiskCoverage = ds.Tables[0].Rows[1]["IS_RISK_COVERAGE"].ToString();
                    if (ds.Tables[0].Rows[1]["PERSONAL_INJURY"].ToString() == "Y")
                    {
                        chkPersonalInjury.Checked = true;
                    }

                    string IsExpenseHeadingCreated = "";

                    for (int i = 0; i < RowCount; i++)
                    {
                        dtRow = ds.Tables[0].Rows[i];
                        CurrentVictim = dtRow["VICTIM"].ToString();

                        if (PrevVictim != CurrentVictim && CurrentVictim != "" && IsRiskCoverage!="Y")
                        {
                            DataRow drNew = ds.Tables[0].NewRow();
                            drNew["COV_DES"] = Prefix + CurrentVictim;
                            drNew["COVERAGE_ID"] = 0;
                            ds.Tables[0].Rows.InsertAt(drNew, i);
                            ds.Tables[0].AcceptChanges();
                            PrevVictim = CurrentVictim;
                            RowCount = RowCount + 1; // because row is added
                        }
                        //----------------------------------------------------------------
                        // MODIFIED BY SANTOSH KR GAUTAM ON 14 JUL 2011 (REF ITRACK:1031)
                        //----------------------------------------------------------------
                        // CREATE HEADING ROW FOR EXPENSE COVERAGES
                        //if (IsRiskCoverage != "Y" && IsExpenseHeadingCreated=="")
                        //{
                        //    IsExpenseHeadingCreated = "Y";
                        //    DataRow drNew = ds.Tables[0].NewRow();
                        //    drNew["COV_DES"] = ExpenseHeading;
                        //    drNew["COVERAGE_ID"] = 0;
                        //    ds.Tables[0].Rows.InsertAt(drNew, i);
                        //    ds.Tables[0].AcceptChanges();
                        //    PrevVictim = CurrentVictim;
                        //    RowCount = RowCount + 1; // because row is added
                        //}
                    }
               }

              
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["RESERVE_ID"].ToString()))
                {
                    hidIS_RESERVE_EXIST.Value = "Y";
                    hidACTIVITY_ID.Value = ds.Tables[0].Rows[0]["ACTIVITY_ID"].ToString();

                    //grdClaimCoverages.Rows[0].Cells[0].RowSpan = 5;
                    if (hidCALLED_FROM.Value.ToUpper().Equals("NTF"))
                    {
                        btnSave.Visible = false;
                        btnSaveAndContinue.Visible = false;
                       // btnReset.Visible = false;
                    }
                    // IF ACTIVITY IS NOT FOUND THIS MEANS IS THAT PAGE BEING LOADED FOR NEW ACTIVITY

                    if ( ds.Tables.Count>2&& ds.Tables[2].Rows.Count > 0)
                    {
                        hidACTIVITY_TYPE.Value = ds.Tables[2].Rows[0]["ACTIVITY_REASON"].ToString();
                        hidACTIVITY_ACTION_ON_PAYMENT.Value = ds.Tables[2].Rows[0]["ACTION_ON_PAYMENT"].ToString();

                        int ACTIVITY_STATUS = int.Parse(ds.Tables[2].Rows[0]["ACTIVITY_STATUS"].ToString());
                        if (ACTIVITY_STATUS == (int)enumClaimActivityStatus.COMPLETE || ACTIVITY_STATUS == (int)enumClaimActivityStatus.DEACTIVATE)
                        {
                            btnSave.Visible = false;
                            btnSaveAndContinue.Visible = false;
                        }
                    }
                   
                    // FOR PAYMENT TYPE ACTIVITY
                


                }

                string InsuredObject = objResourceMgr.GetString("INSURED_OBJECT_HEADING") + ds.Tables[1].Rows[0][0].ToString();
                DataRow dr = ds.Tables[0].NewRow();
                dr["COV_DES"] = InsuredObject;
                ds.Tables[0].Rows.InsertAt(dr, 0);
                ds.Tables[0].AcceptChanges();
                grdClaimCoverages.DataSource = ds.Tables[0];
                grdClaimCoverages.DataBind();

                hidTOTAL_PAYMENT.Value = TotalPaymentPayment.ToString();
                hidTOTAL_RECOVERY.Value = TotalRecoveryPayment.ToString();
                hidTOTAL_OUTSTANDING.Value = TotalOutstandingAmount.ToString();

                int Activity_Type = int.Parse(hidACTIVITY_TYPE.Value);

                if (Activity_Type==0 ||Activity_Type == (int)enumActivityReason.NEW_RESERVE)
                    btnSaveAndContinue.Visible = false;
                
                // FOR PAYMENT MODE
                if (Activity_Type == (int)enumActivityReason.CLAIM_PAYMENT)
                {
                    btnSave.Visible = false;

                    ClsMessages.SetCustomizedXml(GetLanguageCode());

                    hidMESSAGE.Value = ClsMessages.GetMessage(base.ScreenId, "19");
                    pnlPersonalInjury.Visible = true;
                    ClsMessages.SetCustomizedXml(GetLanguageCode());
                    hidPARENT_TAB_NAME.Value = ClsMessages.GetMessage(base.ScreenId, "8");
                    lblTitle.Text = hidPARENT_TAB_NAME.Value;

                    if(TotalPaymentPayment!=0)
                    {                       
                        hidPAYEE_TAB_CAPTION.Value = objResourceMgr.GetString("PAYMENT_TAB");
                        ClientScript.RegisterStartupScript(this.GetType(),"OpenPayeeTab", "<script>OpenPayeeTab();</script>");
                        ClientScript.RegisterStartupScript(this.GetType(),"ChangeActivityAmount", "<script>ChangeActivityAmount();</script>");

                    }
                }
                // FOR RECOVERY MODE
                else if (Activity_Type == (int)enumActivityReason.RECOVERY )
                {
                    btnSave.Visible = false;
                    hidPARENT_TAB_NAME.Value = ClsMessages.GetMessage(base.ScreenId, "9");
                    lblTitle.Text = hidPARENT_TAB_NAME.Value;
                    if (TotalRecoveryPayment != 0)
                    {
                        hidPAYEE_TAB_CAPTION.Value = objResourceMgr.GetString("RECOVERY_TAB");
                        ClientScript.RegisterStartupScript(this.GetType(),"ChangeActivityAmount", "<script>ChangeActivityAmount();</script>");
                        ClientScript.RegisterStartupScript(this.GetType(), "OpenPayeeTab", "<script>OpenPayeeTab();</script>");
                    }
                }
                // FOR RESERVE MODE
                else if (Activity_Type == (int)enumActivityReason.RESERVE_UPDATE )
                {

                    btnSaveAndContinue.Visible = false;
                    hidPARENT_TAB_NAME.Value = ClsMessages.GetMessage(base.ScreenId, "7");
                    lblTitle.Text = hidPARENT_TAB_NAME.Value;
                    if (TotalOutstandingAmount != 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "ChangeActivityAmount", "<script>ChangeActivityAmount();</script>");

                    }
                  
                }
               
            }
            else
            {
                btnSave.Visible = false;
                btnSaveAndContinue.Visible = false;
                btnBackToActivity.Visible = false;
            }
          
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveReserveDetails();
        }
        // NOT THIS FUNCTION TI COPY CO INSURANCE AND RE INSURANCE DATA INTO [CLM_ACTIVITY_CO_RI_BREAKDOWN] TABLE
        // AND USED TO UPDATE CLM_ACTIVITY_RESERVE TABLES COLUMNS(RI_RESERVE, RI_RESERVE_TRAN, CO_RESERVE, CO_RESERVE_TRAN
        private int CalculateBreakdown()
        {
            string CarrerCode = GetSystemId();
            return objAddReserveDetails.CalculateBreakdown(int.Parse(hidCLAIM_ID.Value), int.Parse(hidACTIVITY_ID.Value));

        }

        private void SaveReserveDetails()
        {
            int intRetval;
            bool HasError = false;
            // try
            {
                ArrayList aList = new ArrayList();
                //For new item to add
                if (hidIS_RESERVE_EXIST.Value.ToUpper().Equals("N"))
                {

                    aList = PopulateArray(out HasError);
                    if (HasError)
                    {
                        HasErrorOccured = 1;
                        BindGrid();
                        return;
                    }

                    // PAGE IS CALLED FROM NOTIFICATION AND 
                    InsertNewActivity();

                    intRetval = objAddReserveDetails.AddReserveDetails(aList, int.Parse(hidACTIVITY_ID.Value));


                    if (intRetval > 0)
                    {
                        HasErrorOccured = 0;
                        CalculateBreakdown();
                        int RetValue = CompleteClaimActivity();


                        //reserve amount is greater then the limit amount
                        if (RetValue == -11)
                        {
                            HasErrorOccured = 1;
                            lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "20");
                            lblMessage.Visible = true;
                            hidACTIVITY_ID.Value = "0";
                            BindGrid();
                            return;
                        }



                        lblMessage.Text = ClsMessages.FetchGeneralMessage("29");
                        GetOldDataObject();
                        ClientScript.RegisterStartupScript(this.GetType(), "GoToActivity", "<script>GoToActivity();</script>");

                    }
                    else if (intRetval == -1)
                    {
                        HasErrorOccured = 1;

                        ClsMessages.SetCustomizedXml(GetLanguageCode());
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("18");
                        BindGrid();
                    }
                    else
                    {
                        HasErrorOccured = 1;
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                        BindGrid();
                    }

                    lblMessage.Visible = true;
                }
                else //For The Update cse
                {

                    aList = PopulateArray(out HasError);

                    if (HasError)
                    {
                        HasErrorOccured = 1;
                        BindGrid();
                        return;

                    }

                    intRetval = objAddReserveDetails.AddReserveDetails(aList, int.Parse(hidACTIVITY_ID.Value));

                    if (intRetval > 0)
                    {
                        HasErrorOccured = 0;
                        CalculateBreakdown();
                        GetOldDataObject();
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("31");

                    }
                    else if (intRetval == -1)
                    {
                        HasErrorOccured = 1;
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("18");
                        BindGrid();
                    }

                    else
                    {
                        HasErrorOccured = 1;
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                        BindGrid();

                    }

                    lblMessage.Visible = true;

                }
            }
        }
        private void InsertNewActivity()
        {
            
            	    ClsActivity  objActivity = new ClsActivity();
					ClsActivityInfo objActivityInfo = new ClsActivityInfo();
					objActivityInfo.CLAIM_ID = int.Parse(hidCLAIM_ID.Value);
					objActivityInfo.CREATED_BY = int.Parse(GetUserId());				
					objActivityInfo.ACTIVITY_REASON = (int)enumActivityReason.NEW_RESERVE;
					objActivityInfo.ACTIVITY_STATUS = (int)enumClaimActivityStatus.INCOMPLETE;
                    objActivityInfo.ACTION_ON_PAYMENT = 165;//NEW RESERVE TYPE(Pls Check in SELECT * FROM CLM_TYPE_DETAIL  WHERE TYPE_ID=8)
                    objActivityInfo.COI_TRAN_TYPE = 14849; // FOR FULL
                    if (objActivity.Add(objActivityInfo) > 0)
                        hidACTIVITY_ID.Value = objActivityInfo.ACTIVITY_ID.ToString();
                    else
                        hidACTIVITY_ID.Value= "0";
					
        }
        private int CompleteClaimActivity()
        {
            ClsActivity objActivity = new ClsActivity();
            ClsActivityInfo objActivityInfo = new ClsActivityInfo();
            objActivityInfo.CLAIM_ID=int.Parse(hidCLAIM_ID.Value);
            objActivityInfo.ACTIVITY_ID = int.Parse(hidACTIVITY_ID.Value);

          // return objAddReserveDetails.CompleteClaimActivity(int.Parse(hidCLAIM_ID.Value), int.Parse(hidACTIVITY_ID.Value),int.Parse(GetUserId()));

           int RetError = 0;
           int RetVal = objActivity.CompleteActivity(objActivityInfo, 11836, 165, int.Parse(GetUserId()), out RetError, int.Parse(GetLanguageID()));
           if (RetError == -11)
               RetVal = RetError;
           else 
               RetVal = 1;

            return RetVal;
            
        }

     
        private ArrayList PopulateArray(out bool HasError)
        {
           
            string IsPersonalInjuryConditionApplied = "";
            string IsRiskCoverage = "Y";
            double CurrentFilledAmount=0;
            double CovaergeLimit = 0;

            Dictionary<int, double> DtCovTotalPaymentWithPersonalInjury = new Dictionary<int, double>();

            if (hidLOB_ID.Value == ((int)enumLOB.FACLIAB).ToString().ToString() || hidLOB_ID.Value == ((int)enumLOB.CVLIABTR).ToString().ToString())
            {
                IsPersonalInjuryConditionApplied = "Y";

                DataSet ds = objAddReserveDetails.GetClaimPaymentAmountWithPersonalInjury(int.Parse(hidCLAIM_ID.Value), int.Parse(hidACTIVITY_ID.Value));
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        int CoverageID = int.Parse(dr["COVERAGE_ID"].ToString());
                        double paymentAmount = double.Parse(dr["PAYMENT_AMOUNT"].ToString());
                        DtCovTotalPaymentWithPersonalInjury.Add(CoverageID, paymentAmount);
                    }
                }

            }

            HasError = false;
            ArrayList arrList = new ArrayList();
            Dictionary<int, double> DtCovLimit = new Dictionary<int, double>();
            Dictionary<int, double> DtCovOut = new Dictionary<int, double>();
            Dictionary<int, string> DtCovLimitOverride = new Dictionary<int, string>();
            Dictionary<int, double> DtCovPayment = new Dictionary<int, double>();
            Dictionary<int, double> DtCovTotalPaymentAmount = new Dictionary<int, double>();
            Dictionary<int, string> DtCoverageText = new Dictionary<int, string>();

            int CoverageCodeID = 0;

            foreach (GridViewRow dr in grdClaimCoverages.Rows)
            {
                Label lblVictim = dr.FindControl("lblVictim") as Label;
                if (dr.RowIndex == 0 || dr.Cells[0].Text == "0" || dr.Cells[0].Text == "&nbsp;" || dr.Cells[0].Text == "")
                    continue;

                ClsAddReserveDetailsInfo objReserveDetailsInfo = new ClsAddReserveDetailsInfo();

                //objReserveDetailsInfo.ACTIVITY_ID.CurrentValue = int.Parse(hidACTIVITY_ID.Value);
                objReserveDetailsInfo.CLAIM_ID.CurrentValue = int.Parse(hidCLAIM_ID.Value);

               
                if (chkPersonalInjury.Checked)
                    objReserveDetailsInfo.PERSONAL_INJURY.CurrentValue = "Y";
                else
                    objReserveDetailsInfo.PERSONAL_INJURY.CurrentValue = "N";

                TextBox txtCO_RESERVE = dr.FindControl("txtCO_RESERVE") as TextBox;
                if (txtCO_RESERVE != null && !string.IsNullOrEmpty(txtCO_RESERVE.Text))
                    objReserveDetailsInfo.CO_RESERVE.CurrentValue = Convert.ToDouble(txtCO_RESERVE.Text.Trim(), nfi);
                else
                    objReserveDetailsInfo.CO_RESERVE.CurrentValue = Convert.ToDouble(0, nfi);

                TextBox txtOUTSTANDING = dr.FindControl("txtOUTSTANDING") as TextBox;
                if (txtOUTSTANDING != null && !string.IsNullOrEmpty(txtOUTSTANDING.Text))
                    objReserveDetailsInfo.OUTSTANDING.CurrentValue = Convert.ToDouble(txtOUTSTANDING.Text.Trim(), nfi);
                else
                    objReserveDetailsInfo.OUTSTANDING.CurrentValue = Convert.ToDouble(0, nfi);

                TextBox txtRI_RESERVE = dr.FindControl("txtRI_RESERVE") as TextBox;
                if (txtRI_RESERVE != null && !string.IsNullOrEmpty(txtRI_RESERVE.Text))
                    objReserveDetailsInfo.RI_RESERVE.CurrentValue = Convert.ToDouble(txtRI_RESERVE.Text.Trim(), nfi);
                else
                    objReserveDetailsInfo.RI_RESERVE.CurrentValue = Convert.ToDouble(0, nfi);

                TextBox txtPREV_OUTSTANDING = dr.FindControl("txtPREV_OUTSTANDING") as TextBox;
                if (txtPREV_OUTSTANDING != null && !string.IsNullOrEmpty(txtPREV_OUTSTANDING.Text))
                    objReserveDetailsInfo.PREV_OUTSTANDING.CurrentValue = Convert.ToDouble(txtPREV_OUTSTANDING.Text.Trim(), nfi);
                else
                    objReserveDetailsInfo.PREV_OUTSTANDING.CurrentValue = Convert.ToDouble(0, nfi);

                HtmlInputHidden hidPAYMENTAMOUNT = dr.FindControl("hidPAYMENTAMOUNT") as HtmlInputHidden;
                if (hidPAYMENTAMOUNT != null && !string.IsNullOrEmpty(hidPAYMENTAMOUNT.Value))
                {
                    objReserveDetailsInfo.PAYMENT_AMOUNT.CurrentValue = Convert.ToDouble(hidPAYMENTAMOUNT.Value.Trim(), nfi);

                    if( hidPAYMENTAMOUNT.Value!="0" && hidPAYMENTAMOUNT.Value!="0.00" && hidPAYMENTAMOUNT.Value!="0,00")  
                    {
                        CoverageCodeID = int.Parse(dr.Cells[22].Text);
                        CurrentFilledAmount =Convert.ToDouble(hidPAYMENTAMOUNT.Value.Trim(), nfi);
                        CovaergeLimit = Convert.ToDouble(dr.Cells[4].Text.Trim(), nfi);
                        IsRiskCoverage = dr.Cells[16].Text.Trim();
                    }
                }
                else
                    objReserveDetailsInfo.PAYMENT_AMOUNT.CurrentValue = Convert.ToDouble(0, nfi);

                TextBox txtRECOVERY_AMOUNT = dr.FindControl("txtRECOVERY_AMOUNT") as TextBox;
                if (txtRECOVERY_AMOUNT != null && !string.IsNullOrEmpty(txtRECOVERY_AMOUNT.Text))
                    objReserveDetailsInfo.RECOVERY_AMOUNT.CurrentValue = Convert.ToDouble(txtRECOVERY_AMOUNT.Text.Trim(), nfi);
                else
                    objReserveDetailsInfo.RECOVERY_AMOUNT.CurrentValue = Convert.ToDouble(0, nfi);


                TextBox txtDEDUCTIBLE_1 = dr.FindControl("txtDEDUCTIBLE_1") as TextBox;
                if (txtDEDUCTIBLE_1 != null && !string.IsNullOrEmpty(txtDEDUCTIBLE_1.Text))
                    objReserveDetailsInfo.DEDUCTIBLE_1.CurrentValue = Convert.ToDouble(txtDEDUCTIBLE_1.Text.Trim(), nfi);
                else
                    objReserveDetailsInfo.DEDUCTIBLE_1.CurrentValue = Convert.ToDouble(0, nfi);

                TextBox txtADJUSTED_AMOUNT = dr.FindControl("txtADJUSTED_AMOUNT") as TextBox;
                if (txtADJUSTED_AMOUNT != null && !string.IsNullOrEmpty(txtADJUSTED_AMOUNT.Text))
                    objReserveDetailsInfo.ADJUSTED_AMOUNT.CurrentValue = Convert.ToDouble(txtADJUSTED_AMOUNT.Text.Trim(), nfi);
                else
                    objReserveDetailsInfo.ADJUSTED_AMOUNT.CurrentValue = Convert.ToDouble(0, nfi);


                objReserveDetailsInfo.ACTIVITY_TYPE.CurrentValue = int.Parse(hidACTIVITY_TYPE.Value);
                objReserveDetailsInfo.ACTION_ON_PAYMENT.CurrentValue = int.Parse(hidACTIVITY_ACTION_ON_PAYMENT.Value);

                if (dr.Cells[18].Text != "" && dr.Cells[18].Text != "&nbsp;")
                    objReserveDetailsInfo.VICTIM_ID.CurrentValue = int.Parse(dr.Cells[18].Text);

                objReserveDetailsInfo.COVERAGE_ID.CurrentValue = int.Parse(dr.Cells[0].Text);

                objReserveDetailsInfo.RISK_ID.CurrentValue = int.Parse(dr.Cells[1].Text);

                objReserveDetailsInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCUSTOMER_ID.Value);
                objReserveDetailsInfo.POLICY_ID.CurrentValue = int.Parse(hidPOLICY_ID.Value);
                objReserveDetailsInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPOLICY_VERSION_ID.Value);

                if (hidIS_RESERVE_EXIST.Value.ToUpper().Equals("N"))
                {
                    objReserveDetailsInfo.CREATED_BY.CurrentValue       = int.Parse(GetUserId());
                    objReserveDetailsInfo.CREATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());
                    objReserveDetailsInfo.IS_ACTIVE.CurrentValue        = "Y";

                    objReserveDetailsInfo.ACTION                        = "I";
                }
                else
                {
                    objReserveDetailsInfo.RESERVE_ID.CurrentValue            = int.Parse(dr.Cells[2].Text);
                    objReserveDetailsInfo.MODIFIED_BY.CurrentValue           = int.Parse(GetUserId());
                    objReserveDetailsInfo.LAST_UPDATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());
                    objReserveDetailsInfo.IS_ACTIVE.CurrentValue             = "Y";

                    objReserveDetailsInfo.ACTION                             = "U";
                }

                if (IsPersonalInjuryConditionApplied == "Y")
                {
                    int CovID = int.Parse(dr.Cells[22].Text);
                    double PrevValue=0;

                    if (DtCovOut.ContainsKey(CovID))
                    {                                             
                           PrevValue            = DtCovOut[CovID];
                           DtCovOut[CovID]      = objReserveDetailsInfo.OUTSTANDING.CurrentValue + PrevValue;
                           PrevValue            = DtCovPayment[CovID];
                           DtCovPayment[CovID]  = objReserveDetailsInfo.PAYMENT_AMOUNT.CurrentValue + PrevValue;
                      
                    }
                    else
                    {
                        // ADD NEW COV IN DICTIONARY
                        DtCovPayment.Add(CovID, objReserveDetailsInfo.PAYMENT_AMOUNT.CurrentValue);
                        DtCovTotalPaymentAmount.Add(CovID, Convert.ToDouble(dr.Cells[14].Text.Trim(), nfi));
                        DtCovLimit.Add(CovID,Convert.ToDouble(dr.Cells[4].Text.Trim(), nfi));
                        DtCovOut.Add(CovID, objReserveDetailsInfo.OUTSTANDING.CurrentValue);
                        DtCovLimitOverride.Add(CovID, dr.Cells[15].Text.Trim());
                        DtCoverageText.Add(CovID, dr.Cells[3].Text.Trim());
                        
                    }


                   

                }
                arrList.Add(objReserveDetailsInfo);
            }

            //  VALIDATE OUTESTANDING AMOUNT
            if (IsPersonalInjuryConditionApplied == "Y")
            {
                // TO VALIDATE RESERVE
                if (hidIS_RESERVE_EXIST.Value.ToUpper().Equals("N") || int.Parse(hidACTIVITY_TYPE.Value) == (int)enumActivityReason.NEW_RESERVE)
                {
                    foreach (var KeyValuePair in DtCovOut)
                    {
                        double CurrentOutstandingAmount = KeyValuePair.Value;
                        double TotalPaymentOfCoverage = DtCovTotalPaymentAmount[KeyValuePair.Key];
                        double TotalLimitOfCoverage = DtCovLimit[KeyValuePair.Key];
                        string LimitOverride = DtCovLimitOverride[KeyValuePair.Key];
                        string CoverageText = DtCoverageText[KeyValuePair.Key];
                        if (LimitOverride == "N" && CurrentOutstandingAmount > (TotalLimitOfCoverage - TotalPaymentOfCoverage))
                        {

                            string ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "12");
                            ErrorMessage = ErrorMessage.Replace("@", ": <i><u>" + CoverageText + "</u></i>");
                            lblMessage.Text = ErrorMessage;
                            lblMessage.Visible = true;
                            HasError = true;
                            break;
                        }


                    }

                }
                //  VALIDATE PAYMENT AMOUNT
                if (int.Parse(hidACTIVITY_TYPE.Value) == (int)enumActivityReason.CLAIM_PAYMENT)
                {

                    foreach (var KeyValuePair in DtCovPayment)
                    {
                        double CurrentPayingAmount = KeyValuePair.Value;
                        int Key = KeyValuePair.Key;
                        double TotalPaymentWithPersonalInjury = 0;
                        if (DtCovTotalPaymentWithPersonalInjury.Count > 0)
                            TotalPaymentWithPersonalInjury = DtCovTotalPaymentWithPersonalInjury[Key];

                        double TotalLimitOfCoverage = DtCovLimit[Key];
                        string LimitOverride = DtCovLimitOverride[Key];
                        string CoverageText = DtCoverageText[Key];
                        double TotalAmountToBePaid = CurrentPayingAmount + TotalPaymentWithPersonalInjury;

                        if (chkPersonalInjury.Checked == true && TotalAmountToBePaid > ((TotalLimitOfCoverage * DivisionPercenatage) / 100))
                        {
                            lblMessage.Font.Bold = false;
                            string ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "13");
                            string PreviousPaidAmountMsg = ClsMessages.GetMessage(base.ScreenId, "15");
                            string CurrentPayingAmountMsg = ClsMessages.GetMessage(base.ScreenId, "16");
                            string CoverageLimitMsg = ClsMessages.GetMessage(base.ScreenId, "17");

                            ErrorMessage = ErrorMessage.Replace("$", DivisionPercenatage.ToString());
                            PreviousPaidAmountMsg = PreviousPaidAmountMsg.Replace("@", TotalPaymentWithPersonalInjury.ToString("N", nfi));
                            CurrentPayingAmountMsg = CurrentPayingAmountMsg.Replace("@", CurrentPayingAmount.ToString("N", nfi));
                            CoverageLimitMsg = CoverageLimitMsg.Replace("@", TotalLimitOfCoverage.ToString("N", nfi));
                            CoverageLimitMsg = CoverageLimitMsg.Replace("$", DivisionPercenatage.ToString());
                            ErrorMessage = "<b>" + ErrorMessage.Replace("@", ": <i><u>" + CoverageText + "</u></i>");
                            ErrorMessage = ErrorMessage + "</b></br>";
                            ErrorMessage = ErrorMessage + "</br><span style='font-weight:none;'>" + PreviousPaidAmountMsg + "+" + CurrentPayingAmountMsg + " > " + CoverageLimitMsg + "</span>";
                            lblMessage.Text = ErrorMessage;
                            lblMessage.Visible = true;
                            HasError = true;
                            break;
                        }


                    }
                }
            }
          //--------------------------------------------------------------------------
          // ADDED BY SANTOSH KR GAUTAM ON 09 AUG 2011 FOR ITRACK 1043 (TFS NO 38)
          // LIMIT CONSTRAINT IS APPLIED FOR POLICY COVERAGE ONLY
          //--------------------------------------------------------------------------
            if (IsRiskCoverage =="Y" && IsPersonalInjuryConditionApplied != "Y" && int.Parse(hidACTIVITY_TYPE.Value) == (int)enumActivityReason.CLAIM_PAYMENT)
            {
                 string ErrorMessage = "";
                 double TotalPaidAmount = 0;

                DataSet ds = objAddReserveDetails.GetTotalClaimPaymentofCoverage(int.Parse(hidCLAIM_ID.Value), CoverageCodeID);

                if (ds.Tables.Count > 1 && ds.Tables[0].Rows.Count > 0 )
                {
                    TotalPaidAmount= double.Parse(ds.Tables[1].Rows[0]["sum"].ToString());

                    if ((CurrentFilledAmount + TotalPaidAmount) > CovaergeLimit)
                    {
                        HasError = true;
                        lblMessage.Visible = true;
                        string Message1 = objResourceMgr.GetString("Message1");
                        string Message2 = objResourceMgr.GetString("Message2");
                        string Message3 = objResourceMgr.GetString("Message3");
                        string Message4 = objResourceMgr.GetString("Message4");
                        string Message5 = objResourceMgr.GetString("Message5");
                        string Message6 = objResourceMgr.GetString("Message6");
                        string Message7 = objResourceMgr.GetString("Message7");

                        ErrorMessage = "<p style=\"color: red\">" + Message1 + "</p></br>";
                        ErrorMessage += "<table  cellpadding=0 cellspacing=0 width=400px > <caption class=\"midcolorc\" style=\"border: 1px solid #000000;  font-weight: bold\"  >"+Message2+" </caption>  <tr><td  class=\"midcolora\" width=200px style=\"border: 1px solid #000000; \" >"+Message3+"</td><td class=\"midcolora\" style=\"border: 1px solid #000000; \"> "+Message4+"</td></tr>";
                    

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {

                            ErrorMessage += "<tr><td class=\"midcolora\" style=\"border: 1px solid #000000\">" + dr["CLAIM_NUMBER"].ToString() + "</td> <td class=\"midcolora\" style=\"border: 1px solid #000000\">" + Convert.ToDouble(dr["PAYMENT_AMOUNT"].ToString()).ToString("N", nfi).ToString() + " </td></tr>";

                        }

                        ErrorMessage += "</table>";
                        ErrorMessage += "</br><table > <tr><td class=\"midcolora\" width=400px style=\" border: 1px solid #000000;\">" +Message5+ Convert.ToDouble(TotalPaidAmount.ToString()).ToString("N", nfi).ToString() + " </td></tr>";
                        ErrorMessage += "<tr><td class=\"midcolora\" style=\"border: 1px solid #000000\">" + Message6+Convert.ToDouble(CurrentFilledAmount.ToString()).ToString("N", nfi).ToString() + " </td></tr>";
                        ErrorMessage += "<tr><td class=\"midcolora\" style=\"border: 1px solid #000000 color: red\"> "+Message7+" <b>" + Convert.ToDouble((CovaergeLimit - TotalPaidAmount).ToString()).ToString("N", nfi).ToString() + "</b> </td></tr>";
                        ErrorMessage += "</table>";

                        lblMessage.Text = ErrorMessage;
                    }
                  
                }
            }

            return arrList;
        }


        protected void BindGrid()
        {
            int Activity_Type = int.Parse(hidACTIVITY_TYPE.Value);
            DeductibleAmountMessage = ClsMessages.GetMessage(base.ScreenId, "11");

            foreach (GridViewRow dr in grdClaimCoverages.Rows)
            {

                if (dr.RowType != DataControlRowType.EmptyDataRow)
                {
                    dr.Cells[0].Visible = false;
                    dr.Cells[1].Visible = false;
                    dr.Cells[2].Visible = false;


                    dr.Cells[14].Visible = false;
                    dr.Cells[15].Visible = false;
                    
                    dr.Cells[16].Visible = false;
                    dr.Cells[17].CssClass = "hiddenColum";
                    dr.Cells[18].Visible = false;
                    dr.Cells[19].Visible = false;

                    dr.Cells[20].Visible = false; // MAX_DEDUCTIBLE
                    dr.Cells[21].Visible = false;
                    dr.Cells[22].Visible = false; // ACTUAL_COVERAGE_ID
                    dr.Cells[23].Visible = false; // IS_RECOVERY_PENDING
                    dr.Cells[24].Visible = false; // RESERVE TYPE

                  

                    // FOR PAYMENT MODE
                    if (Activity_Type == (int)enumActivityReason.CLAIM_PAYMENT)
                    {

                         dr.Cells[8].Visible = false; // Outstanding Textbox
                        dr.Cells[11].Visible = false; // Recovery



                    }
                    // FOR RECOVERY MODE
                    else if (Activity_Type == (int)enumActivityReason.RECOVERY)
                    {
                        dr.Cells[5].Visible = false; // Deductible

                        dr.Cells[6].Visible = false; // PrevOutstanding 
                        dr.Cells[7].Visible = false; // Outstanding 
                        dr.Cells[8].Visible = false; // Outstanding Text
                        dr.Cells[9].Visible = false; // Adjusted Amount
                        dr.Cells[10].Visible = false; // Payment Amount
                    }
                    else // FOR RESERVE MODE
                    {
                        dr.Cells[5].Visible = false; // Deductible

                        dr.Cells[6].Visible = false; // PrevOutstanding
                        dr.Cells[7].Visible = false; // Outstanding
                        dr.Cells[9].Visible = false; // Adjusted Amount
                        dr.Cells[10].Visible = false; // Payment Amount
                        dr.Cells[11].Visible = false; // Recovery Amount
                    }


                  



                }
                if (dr.RowType == DataControlRowType.DataRow)
                {
                    string PaymentAmount = "";
                    string RecoveryAmount = "";
                    string OutstandingAmount = "";

                    if (dr.RowIndex == 0)
                    {
                        dr.Cells[3].ColumnSpan = 18;
                        for (int j = dr.Cells.Count - 1; j > 3; j--)
                        {
                            dr.Cells.RemoveAt(j);
                        }

                        dr.Cells[3].Font.Bold = true;

                        dr.Cells[3].Attributes.Add("align", "left");
                    }
                    else
                    {

                        // MERGE COLUMN FOR VICTIM
                        if (hidIsVictimEnabled.Value == "Y" && (dr.Cells[0].Text == "0" || dr.Cells[0].Text == ""))
                        {
                            dr.Cells[3].ColumnSpan = 18;
                            for (int j = dr.Cells.Count - 1; j > 3; j--)
                            {
                                dr.Cells.RemoveAt(j);
                            }

                            dr.Cells[3].Font.Bold = true;
                            dr.Cells[3].ForeColor = System.Drawing.Color.Blue;
                            dr.Cells[3].Attributes.Add("align", "left");


                        }
                        else
                        {

                            TextBox txtDEDUCTIBLE_1 = new TextBox();
                            TextBox txtRECOVERY_AMOUNT = dr.Cells[11].FindControl("txtRECOVERY_AMOUNT") as TextBox;
                            if (txtRECOVERY_AMOUNT != null)
                            {
                                txtRECOVERY_AMOUNT.Attributes.Add("onblur", "javascript:this.value=formatAmount(this.value);");

                                if (string.IsNullOrEmpty(txtRECOVERY_AMOUNT.Text) || txtRECOVERY_AMOUNT.Text == "0.00" || txtRECOVERY_AMOUNT.Text == "0,00")
                                    txtRECOVERY_AMOUNT.Text = "";
                                else
                                {
                                    RecoveryAmount = txtRECOVERY_AMOUNT.Text;
                                    //txtRECOVERY_AMOUNT.Text = Convert.ToDouble(txtRECOVERY_AMOUNT.Text).ToString("N", nfi);
                                }
                            }

                            HtmlInputHidden hidPAYMENTAMOUNT = dr.Cells[10].FindControl("hidPAYMENTAMOUNT") as HtmlInputHidden;

                            TextBox txtPAYMENT_AMOUNT = dr.Cells[10].FindControl("txtPAYMENT_AMOUNT") as TextBox;
                            if (txtPAYMENT_AMOUNT != null)
                            {

                                txtPAYMENT_AMOUNT.Attributes.Add("readonly", "readonly");
                                if (string.IsNullOrEmpty(txtPAYMENT_AMOUNT.Text) || txtPAYMENT_AMOUNT.Text == "0.00" || txtPAYMENT_AMOUNT.Text == "0,00")
                                    txtPAYMENT_AMOUNT.Text = "";
                                else
                                {

                                    PaymentAmount = txtPAYMENT_AMOUNT.Text;
                                    //txtPAYMENT_AMOUNT.Text = txtPAYMENT_AMOUNT.Text;
                                    hidPAYMENTAMOUNT.Value = Convert.ToDouble(txtPAYMENT_AMOUNT.Text, nfi).ToString(); 
                                }
                                txtPAYMENT_AMOUNT.Attributes.Add("onblur", "javascript:this.value=formatAmount(this.value);");
                            }

                            TextBox txtOUTSTANDING = dr.Cells[8].FindControl("txtOUTSTANDING") as TextBox;
                            if (txtOUTSTANDING != null)
                            {
                                txtOUTSTANDING.Attributes.Add("onblur", "javascript:this.value=formatAmount(this.value);");

                                if (string.IsNullOrEmpty(txtOUTSTANDING.Text) || txtOUTSTANDING.Text == "0.00" || txtOUTSTANDING.Text == "0,00")
                                    txtOUTSTANDING.Text = "";
                                else
                                {
                                    OutstandingAmount = txtOUTSTANDING.Text;
                                    //txtOUTSTANDING.Text = Convert.ToDouble(txtOUTSTANDING.Text).ToString("N", nfi);
                                }
                            }
                            //Added by pradeep for tfs#240/iTrack#1512
                            CustomValidator csvOUTSTANDING1 = dr.Cells[8].FindControl("csvOUTSTANDING1") as CustomValidator;
                            if (csvOUTSTANDING1 != null)
                            {
                                csvOUTSTANDING1.ErrorMessage = ClsMessages.FetchGeneralMessage("1776");
                            }


                            // FOR PAYMENT MODE
                            if (Activity_Type == (int)enumActivityReason.CLAIM_PAYMENT)
                            {
                                RegularExpressionValidator revPAYMENT_AMOUNT = dr.Cells[10].FindControl("revPAYMENT_AMOUNT") as RegularExpressionValidator;
                                if (revPAYMENT_AMOUNT != null)
                                {
                                    revPAYMENT_AMOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
                                    revPAYMENT_AMOUNT.ValidationExpression = aRegExpCurrencyformat;
                                }
                                CustomValidator csvPAYMENT_AMOUNT = dr.Cells[10].FindControl("csvPAYMENT_AMOUNT") as CustomValidator;

                                HtmlInputHidden HidPreviousOutstandingValue = dr.Cells[10].FindControl("HidPreviousOutstandingValue") as HtmlInputHidden;
                                if (csvPAYMENT_AMOUNT != null)
                                    csvPAYMENT_AMOUNT.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "4");

                                //Payment partial or full Activity – Payment amount cannot be greater than Reserve (Prev. Outstanding) Amount.
                                double PrevOutstanding = 0;

                                if (dr.Cells[6].Text != "" && dr.Cells[6].Text != "0.00" && dr.Cells[6].Text != "0,00")
                                    PrevOutstanding = Convert.ToDouble(dr.Cells[6].Text, nfi);

                                HidPreviousOutstandingValue.Value = PrevOutstanding.ToString();

                                if (!string.IsNullOrEmpty(PaymentAmount))
                                    TotalPaymentPayment += Convert.ToDouble(PaymentAmount, nfi); 


                                // NOTE IF ACTIVITY TYPE IS PAYMENT PARTIAL (ACTION ON PAYMENT IS 180) AND
                                // IF USER CAN FILL THE DATA IN ANY ONE TEXTBOX. IF USER FILL DATA IN EXTRA 6 COVERAGES (COVREGES NOT COPIED FROM POLICY)
                                // THEN THIS WOULD BE EXPENSE PAYMENT FOR EXPENSE PYAMENT, FOR SUCH COMDITION I ADDDING A QUERYSTRING PARAMETER USING JAVASCRIPT
                                // TO RECOGNISE ON PAYEE TAB
                                int ActionOnPayment = int.Parse(hidACTIVITY_ACTION_ON_PAYMENT.Value);
                                string IsRiskCoverages = dr.Cells[17].Text;
                                if (ActionOnPayment == (int)enumClaimActionOnPayment.PAYMENT_PARTIAL && !string.IsNullOrEmpty(txtPAYMENT_AMOUNT.Text))
                                {
                                    if (IsRiskCoverages == "N")
                                    {
                                        hidIS_PAYMENT_EXPENSE_TYPE.Value = "Y";
                                    }
                                }


                                txtDEDUCTIBLE_1 = dr.Cells[5].FindControl("txtDEDUCTIBLE_1") as TextBox;

                                if (txtDEDUCTIBLE_1 != null)
                                {
                                    txtDEDUCTIBLE_1.Attributes.Add("onchange", "javascript:this.value=formatAmount(this.value);validateDeductibleAmount('" + txtDEDUCTIBLE_1.ClientID + "');");

                                    //if (txtDEDUCTIBLE_1.Text == "" && txtDEDUCTIBLE_1.Text == "&nbsp;")
                                    //    txtDEDUCTIBLE_1.Text = Convert.ToDouble(txtDEDUCTIBLE_1.Text).ToString("N", nfi);
                                }

                                Image imgDEDUCTIBLE_1 = dr.Cells[5].FindControl("imgDEDUCTIBLE_1") as Image;

                                if (dr.Cells[17].Text != "&nbsp;" && dr.Cells[17].Text.Length > 0)
                                {
                                    if (imgDEDUCTIBLE_1 != null)
                                    {
                                        imgDEDUCTIBLE_1.Attributes.Add("onclick", "javascript:ShowDetails(this," + dr.Cells[17].ClientID + ")");
                                    }
                                }
                                else
                                    imgDEDUCTIBLE_1.Visible = false;

                                RegularExpressionValidator revDEDUCTIBLE_1 = dr.Cells[5].FindControl("revDEDUCTIBLE_1") as RegularExpressionValidator;
                                if (revDEDUCTIBLE_1 != null)
                                {
                                    revDEDUCTIBLE_1.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
                                    revDEDUCTIBLE_1.ValidationExpression = aRegExpCurrencyformat;
                                }


                                Label lblDeductibleAmountMessage = dr.Cells[5].FindControl("lblDeductibleAmountMessage") as Label;

                                if (lblDeductibleAmountMessage != null)
                                {
                                    lblDeductibleAmountMessage.Text = DeductibleAmountMessage;

                                    // SHOW THIS LABEL WHEN :-
                                    // IF VALUES EXISTS IN DEDUCTIBLE_1 TEXT AND MINIMUM_DEDUCTIBLE
                                    // IF VALUES EXISTS IN MAX_DEDUCTIBLE 
                                    if (dr.Cells[17].Text != "&nbsp;" && dr.Cells[17].Text.Length > 0)
                                    {
                                        lblDeductibleAmountMessage.Visible = true;
                                        txtDEDUCTIBLE_1.Visible = false;
                                    }
                                    else if (dr.Cells[20].Text != "0" && dr.Cells[20].Text != "0.00" && dr.Cells[20].Text != "0,00" && dr.Cells[20].Text != "&nbsp;" && dr.Cells[20].Text.Length > 0)
                                    {                                                                       

                                        lblDeductibleAmountMessage.Visible = true;
                                        txtDEDUCTIBLE_1.Visible = false;

                                    }
                                    else
                                    {
                                        lblDeductibleAmountMessage.Visible = false;
                                        txtDEDUCTIBLE_1.Visible = true;
                                    }


                                }



                            }
                            // FOR RECOVERY MODE
                            else if (Activity_Type == (int)enumActivityReason.RECOVERY)
                            {

                                RegularExpressionValidator revRECOVERY_AMOUNT = dr.Cells[11].FindControl("revRECOVERY_AMOUNT") as RegularExpressionValidator;
                                if (revRECOVERY_AMOUNT != null)
                                {
                                    revRECOVERY_AMOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
                                    revRECOVERY_AMOUNT.ValidationExpression = aRegExpCurrencyformat;
                                }
                                CustomValidator csvRECOVERY_AMOUNT = dr.Cells[11].FindControl("csvRECOVERY_AMOUNT") as CustomValidator;
                                if (csvRECOVERY_AMOUNT != null)
                                    csvRECOVERY_AMOUNT.ErrorMessage = ClsMessages.FetchGeneralMessage("1174");

                                if (!string.IsNullOrEmpty(RecoveryAmount))
                                    TotalRecoveryPayment += Convert.ToDouble(RecoveryAmount, nfi);  


                            }
                            // FOR RESERVE MODE
                            else
                            {



                                RegularExpressionValidator revOUTSTANDING = dr.Cells[8].FindControl("revOUTSTANDING") as RegularExpressionValidator;
                                if (revOUTSTANDING != null)
                                {
                                    revOUTSTANDING.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
                                    revOUTSTANDING.ValidationExpression = aRegExpCurrencyformat;
                                }
                                CustomValidator csvOUTSTANDING = dr.Cells[8].FindControl("csvOUTSTANDING") as CustomValidator;
                                if (csvOUTSTANDING != null)
                                    csvOUTSTANDING.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "3");
                                
                                //Added by pradeep for tfs#240/iTrack#1512
                                CustomValidator csvOUTSTANDING2 = dr.Cells[8].FindControl("csvOUTSTANDING1") as CustomValidator;
                                if (csvOUTSTANDING2 != null)
                                {
                                    csvOUTSTANDING2.ErrorMessage = ClsMessages.FetchGeneralMessage("1776");
                                }

                                string IsLimitOverride = dr.Cells[15].Text.Trim();
                                HtmlInputHidden HidOutstandingLimitValue = dr.Cells[8].FindControl("HidOutstandingLimitValue") as HtmlInputHidden;

                                // IF LIMITE OVERRIDE IS YES THEN USER CAN FILL ANY VALUE 
                                if (IsLimitOverride == "Y")
                                {
                                    csvOUTSTANDING.Enabled = false;
                                }
                                else
                                {
                                    double TotalPayment = 0;
                                    double Limit = 0;
                                    if (dr.Cells[14].Text != "&nbsp;" && dr.Cells[14].Text != "&nbsp;" && dr.Cells[14].Text != "0.00" && dr.Cells[14].Text != "0,00")
                                        TotalPayment = Convert.ToDouble(dr.Cells[14].Text, nfi);

                                    if (dr.Cells[4].Text != "" && dr.Cells[4].Text != "&nbsp;" && dr.Cells[4].Text != "0.00" && dr.Cells[4].Text != "0,00")
                                        Limit = Convert.ToDouble(dr.Cells[4].Text, nfi);

                                    HidOutstandingLimitValue.Value = (Limit - TotalPayment).ToString();

                                }

                                if (!string.IsNullOrEmpty(OutstandingAmount))
                                    TotalOutstandingAmount += Convert.ToDouble(OutstandingAmount, nfi); 


                            }

                            Image imgVIEW_COVERAGE_DETAILS = dr.Cells[13].FindControl("imgVIEW_COVERAGE_DETAILS") as Image;

                            string NavigateURL = NavigateURLPart1 + "ACTIVITY_ID=" + hidACTIVITY_ID.Value + "&RESERVE_ID=" + dr.Cells[2].Text;
                            if (imgVIEW_COVERAGE_DETAILS != null)
                                imgVIEW_COVERAGE_DETAILS.Attributes.Add("OnClick", "javascript: window.open('" + NavigateURL + "','','left=20,top=20,width=850,height=600,toolbar=1,resizable=1,location=0,menubar=0');");



                            TextBox txtRI_RESERVE = dr.Cells[12].FindControl("txtRI_RESERVE") as TextBox;
                            TextBox txtCO_RESERVE = dr.Cells[13].FindControl("txtCO_RESERVE") as TextBox;


                            if (txtRI_RESERVE != null)
                            {
                                if (txtCO_RESERVE.Text == "" || txtRI_RESERVE.Text == "0.00" || txtRI_RESERVE.Text == "0,00")
                                    txtRI_RESERVE.Text = string.Empty;
                                //else
                                //    Convert.ToDouble(txtRI_RESERVE.Text).ToString("N", nfi);
                            }


                            if (txtCO_RESERVE != null)
                            {
                                if (txtCO_RESERVE.Text == "" || txtCO_RESERVE.Text == "0.00" || txtCO_RESERVE.Text == "0,00")
                                    txtCO_RESERVE.Text = string.Empty;
                                //else
                                //    Convert.ToDouble(txtCO_RESERVE.Text).ToString("N", nfi);
                            }

                            // HIDE HYPERLINK IF txtRI_RESERVE AND txtCO_RESERVE VALUE IS NULL

                            if (txtRI_RESERVE != null && txtCO_RESERVE != null )
                            {

                                if (string.IsNullOrEmpty(txtRI_RESERVE.Text) && string.IsNullOrEmpty(txtCO_RESERVE.Text) && dr.Cells[23].Text=="N")
                                    imgVIEW_COVERAGE_DETAILS.Visible = false;
                                else
                                    imgVIEW_COVERAGE_DETAILS.Visible = true;
                            }

                            //if (dr.Cells[8].Text !="&nbsp;" && dr.Cells[8].Text != "" && dr.Cells[8].Text != "0.00" && dr.Cells[8].Text != "0,00")
                            //    dr.Cells[8].Text = Convert.ToDouble(dr.Cells[8].Text).ToString("N", nfi);

                            //if (dr.Cells[4].Text !="&nbsp;" && dr.Cells[4].Text != "" && dr.Cells[4].Text != "0.00" && dr.Cells[4].Text != "0,00")
                            //    dr.Cells[4].Text = dr.Cells[4].Text;




                            // ADJUSTED AMOUNT
                            TextBox txtADJUSTED_AMOUNT = dr.Cells[9].FindControl("txtADJUSTED_AMOUNT") as TextBox;
                            if (txtADJUSTED_AMOUNT != null)
                            {
                                txtADJUSTED_AMOUNT.Attributes.Add("onchange", "javascript:this.value=formatAmount(this.value); validateDeductibleAmount('" + txtDEDUCTIBLE_1.ClientID + "');");

                                if (string.IsNullOrEmpty(txtADJUSTED_AMOUNT.Text) || txtADJUSTED_AMOUNT.Text == "0.00" || txtADJUSTED_AMOUNT.Text == "0,00")
                                    txtADJUSTED_AMOUNT.Text = "";
                                //else
                                //{
                                //    txtADJUSTED_AMOUNT.Text = txtADJUSTED_AMOUNT.Text).ToString("N", nfi);
                                //}
                            }

                            RegularExpressionValidator revADJUSTED_AMOUNT = dr.Cells[9].FindControl("revADJUSTED_AMOUNT") as RegularExpressionValidator;
                            if (revADJUSTED_AMOUNT != null)
                            {
                                revADJUSTED_AMOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
                                revADJUSTED_AMOUNT.ValidationExpression = aRegExpCurrencyformat;
                            }
                            //Added by pradeep for tfs#240/iTrack#1512
                            CustomValidator csvADJUSTED_AMOUNT = dr.Cells[9].FindControl("csvADJUSTED_AMOUNT") as CustomValidator;
                            if (csvADJUSTED_AMOUNT != null)
                            {
                                csvADJUSTED_AMOUNT.ErrorMessage = ClsMessages.FetchGeneralMessage("1776"); 
                            }


                            // RECV : PAYMENT IS NOT ALLOWED FOR THIS COVERAGE (REF ITRACK:1263)
                            if (dr.Cells[24].Text == "RECV" && Activity_Type == (int)enumActivityReason.CLAIM_PAYMENT)
                            {
                                txtADJUSTED_AMOUNT.ReadOnly = true;
                                txtDEDUCTIBLE_1.ReadOnly = true;
                            }
                            // PYMT : RECOVERY IS NOT ALLOWED FOR THIS COVERAGE (REF ITRACK:1263)
                            if (dr.Cells[24].Text == "PYMT" && Activity_Type == (int)enumActivityReason.RECOVERY)
                            {
                                txtRECOVERY_AMOUNT.ReadOnly = true;
                            }

                           
                        }


                    }
                }

            }// END OF FOR EACH

          

           
        }



        protected void grdClaimCoverages_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int Activity_Type = int.Parse(hidACTIVITY_TYPE.Value);
            
            if (e.Row.RowType != DataControlRowType.EmptyDataRow)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;

                
                e.Row.Cells[14].Visible = false;
                e.Row.Cells[15].Visible = false;
                e.Row.Cells[16].Visible = false;              
                e.Row.Cells[17].CssClass = "hiddenColum";
                e.Row.Cells[18].Visible = false;
                e.Row.Cells[19].Visible = false;
               
                e.Row.Cells[20].Visible = false; // MAX_DEDUCTIBLE
                e.Row.Cells[21].Visible = false; // 
                e.Row.Cells[22].Visible = false; // ACTUAL_COVERAGE_ID

                e.Row.Cells[23].Visible = false; // IS_RECOVERY_PENDING
                e.Row.Cells[24].Visible = false; // RESERVE TYPE
      

                // FOR PAYMENT MODE
                if (Activity_Type == (int)enumActivityReason.CLAIM_PAYMENT)
                {
                   
                    e.Row.Cells[8].Visible = false; // Outstanding Textbox
                    e.Row.Cells[11].Visible = false; // Recovery


                }
                // FOR RECOVERY MODE
                else if (Activity_Type == (int)enumActivityReason.RECOVERY)
                {
                    
                    e.Row.Cells[5].Visible = false; // Deductible

                    e.Row.Cells[6].Visible = false; // PrevOutstanding 
                    e.Row.Cells[7].Visible = false; // Outstanding 
                    e.Row.Cells[8].Visible = false; // Outstanding Textbox
                    e.Row.Cells[9].Visible = false; // Adjusted Amount
                    e.Row.Cells[10].Visible = false; // Payment Amount
                }
                else // FOR RESERVE MODE
                {
                   
                    e.Row.Cells[5].Visible = false; // Deductible

                    e.Row.Cells[6].Visible = false; // PrevOutstanding
                    e.Row.Cells[7].Visible = false; // Outstanding 
                    e.Row.Cells[9].Visible = false; // Adjusted Amount
                    e.Row.Cells[10].Visible = false; // Payment Amount
                    e.Row.Cells[11].Visible = false; // Recovery Amount
                }

               

              

            }
            if (e.Row.RowType == DataControlRowType.DataRow  )
            {
                string PaymentAmount = "";
                string RecoveryAmount = "";
                string OutstandingAmount = "";

                if (e.Row.RowIndex == 0 )
                {
                    e.Row.Cells[3].ColumnSpan = 18;
                    for (int j = e.Row.Cells.Count - 1; j >3; j--)
                    {                          
                        e.Row.Cells.RemoveAt(j);
                    }

                    e.Row.Cells[3].Font.Bold = true;
                    
                    e.Row.Cells[3].Attributes.Add("align", "left");
                }
                else
                {
                    
                    // MERGE COLUMN FOR VICTIM
                    if (IsVictimEnabled=="Y" &&( e.Row.Cells[0].Text == "0" || e.Row.Cells[0].Text == ""))
                    {
                        e.Row.Cells[3].ColumnSpan = 18;
                        for (int j = e.Row.Cells.Count - 1; j > 3; j--)
                        {
                            e.Row.Cells.RemoveAt(j);
                        }

                        e.Row.Cells[3].Font.Bold = true;
                        e.Row.Cells[3].ForeColor = System.Drawing.Color.Blue;
                        e.Row.Cells[3].Attributes.Add("align", "left");


                    }
                    else
                    {

                            TextBox txtDEDUCTIBLE_1 =new TextBox();
                            TextBox txtRECOVERY_AMOUNT = e.Row.Cells[11].FindControl("txtRECOVERY_AMOUNT") as TextBox;
                            if (txtRECOVERY_AMOUNT != null)
                            {
                                txtRECOVERY_AMOUNT.Attributes.Add("onblur", "javascript:this.value=formatAmount(this.value);");

                                if (string.IsNullOrEmpty(txtRECOVERY_AMOUNT.Text) || txtRECOVERY_AMOUNT.Text == "0.00" || txtRECOVERY_AMOUNT.Text == "0,00")
                                    txtRECOVERY_AMOUNT.Text = "";
                                else
                                {
                                    RecoveryAmount = txtRECOVERY_AMOUNT.Text;
                                    txtRECOVERY_AMOUNT.Text = Convert.ToDouble(txtRECOVERY_AMOUNT.Text).ToString("N", nfi);
                                }
                            }

                            HtmlInputHidden hidPAYMENTAMOUNT = e.Row.Cells[10].FindControl("hidPAYMENTAMOUNT") as HtmlInputHidden;

                            TextBox txtPAYMENT_AMOUNT = e.Row.Cells[10].FindControl("txtPAYMENT_AMOUNT") as TextBox;
                            if (txtPAYMENT_AMOUNT != null)
                            {

                                txtPAYMENT_AMOUNT.Attributes.Add("readonly", "readonly");
                                if (string.IsNullOrEmpty(txtPAYMENT_AMOUNT.Text) || txtPAYMENT_AMOUNT.Text == "0.00" || txtPAYMENT_AMOUNT.Text == "0,00")
                                    txtPAYMENT_AMOUNT.Text = "";
                                else
                                {

                                    PaymentAmount = txtPAYMENT_AMOUNT.Text;
                                    txtPAYMENT_AMOUNT.Text = Convert.ToDouble(txtPAYMENT_AMOUNT.Text).ToString("N", nfi);
                                    hidPAYMENTAMOUNT.Value = txtPAYMENT_AMOUNT.Text;  
                                }
                                txtPAYMENT_AMOUNT.Attributes.Add("onblur", "javascript:this.value=formatAmount(this.value);");
                            }

                            TextBox txtOUTSTANDING = e.Row.Cells[8].FindControl("txtOUTSTANDING") as TextBox;
                            if (txtOUTSTANDING != null)
                            {
                                txtOUTSTANDING.Attributes.Add("onblur", "javascript:this.value=formatAmount(this.value);");

                                if (string.IsNullOrEmpty(txtOUTSTANDING.Text) || txtOUTSTANDING.Text == "0.00" || txtOUTSTANDING.Text == "0,00")
                                    txtOUTSTANDING.Text = "";
                                else
                                {
                                    OutstandingAmount = txtOUTSTANDING.Text;
                                    txtOUTSTANDING.Text = Convert.ToDouble(txtOUTSTANDING.Text).ToString("N", nfi);
                                }
                            }

                            //Added by pradeep for tfs#240/iTrack#1512
                            CustomValidator csvOUTSTANDING1 = e.Row.Cells[8].FindControl("csvOUTSTANDING1") as CustomValidator;
                            if (csvOUTSTANDING1 != null)
                            {
                                csvOUTSTANDING1.ErrorMessage = ClsMessages.FetchGeneralMessage("1776");
                            }


                            // FOR PAYMENT MODE
                            if (Activity_Type == (int)enumActivityReason.CLAIM_PAYMENT)
                            {
                               
                                RegularExpressionValidator revPAYMENT_AMOUNT = e.Row.Cells[10].FindControl("revPAYMENT_AMOUNT") as RegularExpressionValidator;
                                if (revPAYMENT_AMOUNT != null)
                                {
                                    revPAYMENT_AMOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
                                    revPAYMENT_AMOUNT.ValidationExpression = aRegExpCurrencyformat;
                                }
                                CustomValidator csvPAYMENT_AMOUNT = e.Row.Cells[10].FindControl("csvPAYMENT_AMOUNT") as CustomValidator;

                                HtmlInputHidden HidPreviousOutstandingValue = e.Row.Cells[10].FindControl("HidPreviousOutstandingValue") as HtmlInputHidden;
                                if (csvPAYMENT_AMOUNT != null)
                                    csvPAYMENT_AMOUNT.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "4");

                                //Payment partial or full Activity – Payment amount cannot be greater than Reserve (Prev. Outstanding) Amount.
                                double PrevOutstanding = 0;

                                if (e.Row.Cells[6].Text != "" && e.Row.Cells[6].Text != "0.00" && e.Row.Cells[6].Text != "0,00")
                                    PrevOutstanding = double.Parse(e.Row.Cells[6].Text);


                                HidPreviousOutstandingValue.Value = Convert.ToDouble(PrevOutstanding.ToString()).ToString("N", nfi);

                                if (!string.IsNullOrEmpty(PaymentAmount))
                                    TotalPaymentPayment += Convert.ToDouble(PaymentAmount.Trim());


                                // NOTE IF ACTIVITY TYPE IS PAYMENT PARTIAL (ACTION ON PAYMENT IS 180) AND
                                // IF USER CAN FILL THE DATA IN ANY ONE TEXTBOX. IF USER FILL DATA IN EXTRA 6 COVERAGES (COVREGES NOT COPIED FROM POLICY)
                                // THEN THIS WOULD BE EXPENSE PAYMENT FOR EXPENSE PYAMENT, FOR SUCH COMDITION I ADDDING A QUERYSTRING PARAMETER USING JAVASCRIPT
                                // TO RECOGNISE ON PAYEE TAB
                                int ActionOnPayment = int.Parse(hidACTIVITY_ACTION_ON_PAYMENT.Value);
                                string IsRiskCoverages = e.Row.Cells[16].Text;
                                if (ActionOnPayment == (int)enumClaimActionOnPayment.PAYMENT_PARTIAL && !string.IsNullOrEmpty(txtPAYMENT_AMOUNT.Text))
                                {
                                    if (IsRiskCoverages == "N")
                                    {
                                        hidIS_PAYMENT_EXPENSE_TYPE.Value = "Y";
                                    }
                                }


                                txtDEDUCTIBLE_1 = e.Row.Cells[5].FindControl("txtDEDUCTIBLE_1") as TextBox;

                                if (txtDEDUCTIBLE_1 != null)
                                {
                                    txtDEDUCTIBLE_1.Attributes.Add("onchange", "javascript:this.value=formatAmount(this.value);validateDeductibleAmount('" + txtDEDUCTIBLE_1.ClientID + "');");

                                    if (txtDEDUCTIBLE_1.Text != "" && txtDEDUCTIBLE_1.Text != "&nbsp;")
                                        txtDEDUCTIBLE_1.Text = Convert.ToDouble(txtDEDUCTIBLE_1.Text).ToString("N", nfi);
                                }

                                Image imgDEDUCTIBLE_1 = e.Row.Cells[5].FindControl("imgDEDUCTIBLE_1") as Image;

                                if (e.Row.Cells[17].Text != "&nbsp;" && e.Row.Cells[17].Text.Length > 0)
                                {
                                    if (imgDEDUCTIBLE_1 != null)
                                    {
                                        imgDEDUCTIBLE_1.Attributes.Add("onclick", "javascript:ShowDetails(this," + e.Row.Cells[17].ClientID + ")");
                                    }
                                }
                                else
                                    imgDEDUCTIBLE_1.Visible = false;

                                RegularExpressionValidator revDEDUCTIBLE_1 = e.Row.Cells[5].FindControl("revDEDUCTIBLE_1") as RegularExpressionValidator;
                                if (revDEDUCTIBLE_1 != null)
                                {
                                    revDEDUCTIBLE_1.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
                                    revDEDUCTIBLE_1.ValidationExpression = aRegExpCurrencyformat;
                                }


                                Label lblDeductibleAmountMessage = e.Row.Cells[5].FindControl("lblDeductibleAmountMessage") as Label;

                                if (lblDeductibleAmountMessage != null)
                                {
                                    lblDeductibleAmountMessage.Text = DeductibleAmountMessage;
                                    // ADDED BY SANTOSH KR GAUTAM ON 14 JUL 2011(REF ITRACK 1293)
                                    lblDeductibleAmountMessage.ForeColor = System.Drawing.Color.Red;

                                    // SHOW THIS LABEL WHEN :-
                                    // IF VALUES EXISTS IN DEDUCTIBLE_1 TEXT AND MINIMUM_DEDUCTIBLE
                                    // IF VALUES EXISTS IN MAX_DEDUCTIBLE 
                                    if (e.Row.Cells[17].Text != "&nbsp;" && e.Row.Cells[17].Text.Length > 0)
                                    {
                                        lblDeductibleAmountMessage.Visible = true;
                                        txtDEDUCTIBLE_1.Visible = false;
                                    }
                                    else if (e.Row.Cells[20].Text != "0" && e.Row.Cells[20].Text != "&nbsp;" && e.Row.Cells[20].Text != "0.00" && e.Row.Cells[20].Text != "0,00")
                                    {
                                        lblDeductibleAmountMessage.Visible = true;
                                        txtDEDUCTIBLE_1.Visible = false;

                                    }
                                    else
                                    {
                                        lblDeductibleAmountMessage.Visible = false;
                                        txtDEDUCTIBLE_1.Visible = true;
                                    }
                                    
                                    
                                }                               
                              


                            }
                            // FOR RECOVERY MODE
                            else if (Activity_Type == (int)enumActivityReason.RECOVERY)
                            {

                                RegularExpressionValidator revRECOVERY_AMOUNT = e.Row.Cells[11].FindControl("revRECOVERY_AMOUNT") as RegularExpressionValidator;
                                if (revRECOVERY_AMOUNT != null)
                                {
                                    revRECOVERY_AMOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
                                    revRECOVERY_AMOUNT.ValidationExpression = aRegExpCurrencyformat;
                                }
                                CustomValidator csvRECOVERY_AMOUNT = e.Row.Cells[11].FindControl("csvRECOVERY_AMOUNT") as CustomValidator;
                                if (csvRECOVERY_AMOUNT != null)
                                    csvRECOVERY_AMOUNT.ErrorMessage = ClsMessages.FetchGeneralMessage("1174");

                                if (!string.IsNullOrEmpty(RecoveryAmount))
                                    TotalRecoveryPayment += Convert.ToDouble(RecoveryAmount.Trim());


                            }
                            // FOR RESERVE MODE
                            else
                            {



                                RegularExpressionValidator revOUTSTANDING = e.Row.Cells[8].FindControl("revOUTSTANDING") as RegularExpressionValidator;
                                if (revOUTSTANDING != null)
                                {
                                    revOUTSTANDING.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
                                    revOUTSTANDING.ValidationExpression = aRegExpCurrencyformat;
                                }
                                CustomValidator csvOUTSTANDING = e.Row.Cells[8].FindControl("csvOUTSTANDING") as CustomValidator;
                                if (csvOUTSTANDING != null)
                                    csvOUTSTANDING.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "3");

                                //Added by pradeep for tfs#240/iTrack#1512
                                CustomValidator csvOUTSTANDING2 = e.Row.Cells[8].FindControl("csvOUTSTANDING1") as CustomValidator;
                                if (csvOUTSTANDING2 != null)
                                {
                                    csvOUTSTANDING2.ErrorMessage = ClsMessages.FetchGeneralMessage("1776");
                                }

                                string IsLimitOverride = e.Row.Cells[15].Text.Trim();
                                HtmlInputHidden HidOutstandingLimitValue = e.Row.Cells[8].FindControl("HidOutstandingLimitValue") as HtmlInputHidden;

                                // IF LIMITE OVERRIDE IS YES THEN USER CAN FILL ANY VALUE 
                                if (IsLimitOverride == "Y")
                                {
                                    csvOUTSTANDING.Enabled = false;
                                }
                                else
                                {
                                    double TotalPayment = 0;
                                    double Limit = 0;
                                    if (e.Row.Cells[14].Text != "&nbsp;" && e.Row.Cells[14].Text != "&nbsp;" && e.Row.Cells[14].Text != "0.00" && e.Row.Cells[14].Text != "0,00")
                                        TotalPayment = double.Parse(e.Row.Cells[14].Text);

                                    if (e.Row.Cells[4].Text != "" && e.Row.Cells[4].Text != "&nbsp;" && e.Row.Cells[4].Text != "0.00" && e.Row.Cells[4].Text != "0,00")
                                        Limit = double.Parse(e.Row.Cells[4].Text);

                                    HidOutstandingLimitValue.Value = Convert.ToDouble((Limit - TotalPayment).ToString()).ToString("N", nfi);

                                }

                                if (!string.IsNullOrEmpty(OutstandingAmount))
                                    TotalOutstandingAmount += Convert.ToDouble(OutstandingAmount.Trim());


                            }

                            Image imgVIEW_COVERAGE_DETAILS = e.Row.Cells[13].FindControl("imgVIEW_COVERAGE_DETAILS") as Image;

                            string NavigateURL = NavigateURLPart1 + "ACTIVITY_ID=" + hidACTIVITY_ID.Value + "&RESERVE_ID=" + e.Row.Cells[2].Text;
                            if (imgVIEW_COVERAGE_DETAILS != null)
                                imgVIEW_COVERAGE_DETAILS.Attributes.Add("OnClick", "javascript: window.open('" + NavigateURL + "','','left=20,top=20,width=850,height=600,toolbar=1,resizable=1,location=0,menubar=0');");



                            TextBox txtRI_RESERVE = e.Row.Cells[12].FindControl("txtRI_RESERVE") as TextBox;
                            TextBox txtCO_RESERVE = e.Row.Cells[13].FindControl("txtCO_RESERVE") as TextBox;


                            if (txtRI_RESERVE != null)
                            {
                                if (txtCO_RESERVE.Text == "" || txtRI_RESERVE.Text == "0.00" || txtRI_RESERVE.Text == "0,00")
                                    txtRI_RESERVE.Text = string.Empty;
                                else
                                    Convert.ToDouble(txtRI_RESERVE.Text).ToString("N", nfi);
                            }


                            if (txtCO_RESERVE != null)
                            {
                                if (txtCO_RESERVE.Text == "" || txtCO_RESERVE.Text == "0.00" || txtCO_RESERVE.Text == "0,00")
                                    txtCO_RESERVE.Text = string.Empty;
                                else
                                    Convert.ToDouble(txtCO_RESERVE.Text).ToString("N", nfi);
                            }

                            // HIDE HYPERLINK IF txtRI_RESERVE AND txtCO_RESERVE VALUE IS NULL

                            if (txtRI_RESERVE != null && txtCO_RESERVE != null)
                            {

                                if (string.IsNullOrEmpty(txtRI_RESERVE.Text) && string.IsNullOrEmpty(txtCO_RESERVE.Text) && e.Row.Cells[23].Text == "N")
                                    imgVIEW_COVERAGE_DETAILS.Visible = false;
                                else
                                    imgVIEW_COVERAGE_DETAILS.Visible = true;
                            }

                            if (e.Row.Cells[7].Text != "&nbsp;")
                                e.Row.Cells[7].Text = Convert.ToDouble(e.Row.Cells[7].Text).ToString("N", nfi);

                            if (e.Row.Cells[4].Text != "&nbsp;")
                                e.Row.Cells[4].Text = Convert.ToDouble(e.Row.Cells[4].Text).ToString("N", nfi);

                            if (e.Row.Cells[6].Text != "&nbsp;")
                                e.Row.Cells[6].Text = Convert.ToDouble(e.Row.Cells[6].Text).ToString("N", nfi);

                           

                            // ADJUSTED AMOUNT
                            TextBox txtADJUSTED_AMOUNT = e.Row.Cells[9].FindControl("txtADJUSTED_AMOUNT") as TextBox;
                            if (txtADJUSTED_AMOUNT != null)
                            {
                                txtADJUSTED_AMOUNT.Attributes.Add("onchange", "javascript:this.value=formatAmount(this.value); validateDeductibleAmount('" + txtDEDUCTIBLE_1.ClientID + "');");

                                if (string.IsNullOrEmpty(txtADJUSTED_AMOUNT.Text) || txtADJUSTED_AMOUNT.Text == "0.00" || txtADJUSTED_AMOUNT.Text == "0,00")
                                    txtADJUSTED_AMOUNT.Text = "";
                                else
                                {
                                    txtADJUSTED_AMOUNT.Text = Convert.ToDouble(txtADJUSTED_AMOUNT.Text).ToString("N", nfi);
                                }
                            }

                            RegularExpressionValidator revADJUSTED_AMOUNT = e.Row.Cells[9].FindControl("revADJUSTED_AMOUNT") as RegularExpressionValidator;
                            if (revADJUSTED_AMOUNT != null)
                            {
                                revADJUSTED_AMOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
                                revADJUSTED_AMOUNT.ValidationExpression = aRegExpCurrencyformat;
                            }
                            //Added by pradeep for tfs#240/iTrack#1512
                            CustomValidator csvADJUSTED_AMOUNT = e.Row.Cells[9].FindControl("csvADJUSTED_AMOUNT") as CustomValidator;
                            if (csvADJUSTED_AMOUNT != null)
                            {
                                csvADJUSTED_AMOUNT.ErrorMessage = ClsMessages.FetchGeneralMessage("1776");
                            }
                            // RECV : PAYMENT IS NOT ALLOWED FOR THIS COVERAGE (REF ITRACK:1263)
                            if (e.Row.Cells[24].Text == "RECV" && Activity_Type == (int)enumActivityReason.CLAIM_PAYMENT)
                            {
                                txtADJUSTED_AMOUNT.ReadOnly = true;
                                txtDEDUCTIBLE_1.ReadOnly = true;
                            }
                            // PYMT : RECOVERY IS NOT ALLOWED FOR THIS COVERAGE (REF ITRACK:1263)
                            if (e.Row.Cells[24].Text == "PYMT" && Activity_Type == (int)enumActivityReason.RECOVERY)
                            {
                                txtRECOVERY_AMOUNT.ReadOnly = true;
                            }
                          
                              
                       
                    }


                }
            }
        }
        //----------------------------------------------------------------
        // MODIFIED BY SANTOSH KR GAUTAM ON 14 JUL 2011 (REF ITRACK:1029)
        //----------------------------------------------------------------
        protected void btnSaveAndContinue_Click(object sender, EventArgs e)
        {
            SaveReserveDetails();

            if(HasErrorOccured<1)
                RegisterStartupScript("ReLoadClaimsTab", "<script>GoToBeneficiary();</script>");	
        }



        //private void AddNewGridRow()
        //{
        //    Table table = (Table)this.grdClaimCoverages.Controls[0];
        //    string VictimPrefix = ClsMessages.GetMessage(base.ScreenId, "10");
        //    string PrevVictim = ""; 
        //    string CurrentVictim = string.Empty;
        //    int realIndex = 0;
           
        //    foreach (GridViewRow row in grdClaimCoverages.Rows)
        //    {


        //        if (row.RowIndex <= 1)
        //           continue;
                

        //        string VictimCellText= row.Cells[19].Text;
        //        if (VictimCellText != "" && VictimCellText != "&nbsp;")
        //        {
        //            CurrentVictim = VictimCellText;// Convert.ToString(this.grdBROKER.DataKeys[1].Value);                 
        //            realIndex = table.Rows.GetRowIndex(row);

        //            if (PrevVictim != CurrentVictim)
        //            {
                       
        //                table.Controls.AddAt(realIndex, CreateNewRow(realIndex, this.grdClaimCoverages.Columns.Count, VictimPrefix+CurrentVictim));

        //                PrevVictim = CurrentVictim;
        //            }
        //        }
        //    }
        //    DataTable dt = grdClaimCoverages.DataSource as DataTable;
        //  //  table.Controls.AddAt(table.Rows.Count - 1, CreateNewRow(table.Rows.Count - 1, this.grdClaimCoverages.Columns.Count, lastState, currentState));
        //}

        //private GridViewRow CreateNewRow(int Index, int ItemCount, string CurrentVictim)
        //{

        //    GridViewRow groupHeaderRow = new GridViewRow(Index, Index, DataControlRowType.Separator, DataControlRowState.Normal);

        //    TableCell newCell = new TableCell();

        //    newCell.ColumnSpan = ItemCount;

        //    newCell.Text = CurrentVictim;
        //    Label lblVictim = new Label();
        //    //lblVictim.CssClass = "midcolora";
        //    lblVictim.ID = "lblVictim";
        //    lblVictim.Text = CurrentVictim;
        //    lblVictim.ForeColor = System.Drawing.Color.Blue;

        //    newCell.Controls.Add(lblVictim);
        //    newCell.CssClass = "midcolora";
        //    newCell.Font.Bold = true;            
        //    newCell.Attributes.Add("align", "left");
        //    groupHeaderRow.Cells.Add(newCell);
        //    return groupHeaderRow;
        //}

    }
}
