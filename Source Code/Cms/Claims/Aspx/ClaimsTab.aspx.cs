/******************************************************************************************
<Author				: -   Sumit Chhabra
<Start Date			: -	 05/05/2006
<End Date			: -	 
<Description		: -  Class to display the Claims tabs named Claims Notifcation, etc.
<Review Date		: - 
<Reviewed By		: - 	
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
using Cms.Model.Claims;
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher;
using Cms.BusinessLayer.BLClaims;

namespace Cms.Claims.Aspx
{
    /// <summary>
    /// 
    /// </summary>
    public class ClaimsTab : Cms.Claims.ClaimBase
    {
        protected System.Web.UI.WebControls.Label capMessage;
        protected System.Web.UI.WebControls.Label lblTemplate;
        protected System.Web.UI.HtmlControls.HtmlForm Form1;
        protected System.Web.UI.HtmlControls.HtmlTableRow formTable;
        protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        protected Cms.CmsWeb.WebControls.ClaimTop cltClaimTop;

        protected string strCustomerId, strPolicyID, strPolicyVersionID, strClaimID, strLOB_ID, strAddNew, strNew_Record, strLITIGATION_FILE, strCO_INSURANCE_TYPE, strPolicyCurrency, strIS_VICTIM_CLAIM, strACC_COI_FLG;
        public string strHOMEOWNER, strRECR_VEH, strIN_MARINE, strLOSS_DATE;
        protected System.Web.UI.HtmlControls.HtmlForm PolicyInformation;
        private string strDiaryID = "";

        string strPOLICY_EFFECTIVE_DATE, strPOLICY_EXPIRATION_DATE;

        private void Page_Load(object sender, System.EventArgs e)
        {
            base.ScreenId = "306";

            GetQueryStringValues();
            GetClaimDataSet();
            SetSessionValues();
            SetClaimTop();
            
            

            SetTabs();
        }

        public void GetClaimDataSet()
        {
            Cms.BusinessLayer.BLClaims.ClsClaims objClaims = new Cms.BusinessLayer.BLClaims.ClsClaims();

            DataSet ds = objClaims.GetPolicyClaimDataSet(strCustomerId, strPolicyID, strPolicyVersionID, strClaimID,int.Parse(GetLanguageID()));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                strCustomerId             = ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString();
                strPolicyID               = ds.Tables[0].Rows[0]["POLICY_ID"].ToString();
                strPolicyVersionID        = ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString();
                strLOB_ID                 = ds.Tables[0].Rows[0]["POLICY_LOB"].ToString();
                strLITIGATION_FILE        = ds.Tables[0].Rows[0]["LITIGATION_FILE"].ToString();
                strCO_INSURANCE_TYPE      = ds.Tables[0].Rows[0]["CO_INSURANCE_TYPE"].ToString();
                strPolicyCurrency         = ds.Tables[0].Rows[0]["POLICY_CURRENCY"].ToString();
                strIS_VICTIM_CLAIM        = ds.Tables[0].Rows[0]["IS_VICTIM_CLAIM"].ToString();
                strACC_COI_FLG            = ds.Tables[0].Rows[0]["ACC_COI_FLG"].ToString();
                strPOLICY_EFFECTIVE_DATE  = ds.Tables[0].Rows[0]["POLICY_EFFECTIVE_DATE"].ToString();
                strPOLICY_EXPIRATION_DATE = ds.Tables[0].Rows[0]["POLICY_EXPIRATION_DATE"].ToString();
                
                if (!string.IsNullOrEmpty(strPolicyCurrency))
                {
                  
                    
                    SetPolicyCurrency(strPolicyCurrency);
                }
                if (!string.IsNullOrEmpty(strLOB_ID))
                {
                    SetLOBID(strLOB_ID);
                }

                SetRegExpCulture();
            }
        }

        #region Set Session Values
        private void SetSessionValues()
        {
            if (strCustomerId != null && strCustomerId != "")
                SetCustomerID(strCustomerId);
            if (strPolicyID != null && strPolicyID != "")
                SetPolicyID(strPolicyID);
            if (strPolicyVersionID != null && strPolicyVersionID != "")
                SetPolicyVersionID(strPolicyVersionID);
            if (strClaimID != null && strClaimID != "")
            {
                SetClaimID(strClaimID);
                SetCalledFor("CLAIM");
            }
            else
                SetClaimID("");
           

            //When either of CusotmerID, PolicyID or PolicyVersionID are 0 or null,
            //we have a case of dummy policy, lets reset the session values to 0
            if (strCustomerId == "0" || strPolicyID == "0" || strPolicyVersionID == "0")
            {
                //Clear claims session values
                ClaimBase objClaimBase = new ClaimBase();
                objClaimBase.ClearClaimsSessionValues();
            }

        }
        #endregion

        private void SetClaimTop()
        {

            if (strCustomerId != null && strCustomerId != "")
            {
                cltClaimTop.CustomerID = int.Parse(strCustomerId);
            }

            if (strPolicyID != null && strPolicyID != "")
            {
                cltClaimTop.PolicyID = int.Parse(strPolicyID);
            }

            if (strPolicyVersionID != null && strPolicyVersionID != "")
            {
                cltClaimTop.PolicyVersionID = int.Parse(strPolicyVersionID);
            }
            if (strClaimID != null && strClaimID != "")
            {
                cltClaimTop.ClaimID = int.Parse(strClaimID);
            }
            if (strLOB_ID != null && strLOB_ID != "")
            {
                cltClaimTop.LobID = int.Parse(strLOB_ID);
            }

            cltClaimTop.ShowHeaderBand = "Claim";

            cltClaimTop.Visible = true;
        }

        private void GetQueryStringValues()
        {
            //Done for Itrack Issue 6381 on 5 Oct 09
            if (Request["CUSTOMER_ID"] != null && Request["CUSTOMER_ID"].ToString() != "" && Request["CUSTOMER_ID"].ToString() != "0")
                strCustomerId = Request["CUSTOMER_ID"].ToString();
            else
                strCustomerId = GetCustomerID();

            //Done for Itrack Issue 6381 on 5 Oct 09
            if (Request["POLICY_ID"] != null && Request["POLICY_ID"].ToString() != "" && Request["POLICY_ID"].ToString() != "0")
                strPolicyID = Request["POLICY_ID"].ToString();
            else
                strPolicyID = GetPolicyID();

            //Done for Itrack Issue 6381 on 5 Oct 09
            if (Request["DUMMY_POLICY_ID"] != null && Request["DUMMY_POLICY_ID"].ToString().Trim() != "" && Request["DUMMY_POLICY_ID"].ToString().Trim() != "0")
                strPolicyID = Request["DUMMY_POLICY_ID"].ToString();

            //Dones for Itrack Issue 6381 on 5 Oct 09
            if (Request["POLICY_VERSION_ID"] != null && Request["POLICY_VERSION_ID"].ToString() != "" && Request["POLICY_VERSION_ID"].ToString() != "0")
                strPolicyVersionID = Request["POLICY_VERSION_ID"].ToString();
            else
                strPolicyVersionID = GetPolicyVersionID();


            if (Request["LOB_ID"] != null && Request["LOB_ID"].ToString() != "" && Request["LOB_ID"].ToString() != "0")
                strLOB_ID = Request["LOB_ID"].ToString();
            else
            {
                if (GetLOBID() != "")
                    strLOB_ID = GetLOBID();
                else
                    strLOB_ID = "0";
            }

            if (Request["CLAIM_ID"] != null && Request["CLAIM_ID"].ToString() != "")
                strClaimID = Request["Claim_ID"].ToString();
            else
                strClaimID = "0";

            if (Request["GoToClaim"] != null && Request["GoToClaim"].ToString().Trim() == "1")
            {
                strClaimID = GetClaimID();
                strLOB_ID = GetLOBID();
            }

            if (Request["DIARY_ID"] != null && Request["DIARY_ID"].ToString() != "")
                strDiaryID = Request["DIARY_ID"].ToString();
            else
                strDiaryID = "";


            if (Request["ADD_NEW"] != null && Request["ADD_NEW"].ToString() != "")
                strAddNew = Request["ADD_NEW"].ToString();
            else
                strAddNew = "0";

            if (Request["NEW_RECORD"] != null && Request["NEW_RECORD"].ToString() != "")
                strNew_Record = Request["NEW_RECORD"].ToString();
            else
                strNew_Record = "0";

            if (Request["HOMEOWNER"] != null && Request["HOMEOWNER"].ToString() != "")
                strHOMEOWNER = Request["HOMEOWNER"].ToString();
            else
                strHOMEOWNER = "0";

            if (Request["RECR_VEH"] != null && Request["RECR_VEH"].ToString() != "")
                strRECR_VEH = Request["RECR_VEH"].ToString();
            else
                strRECR_VEH = "0";

            if (Request["IN_MARINE"] != null && Request["IN_MARINE"].ToString() != "")
                strIN_MARINE = Request["IN_MARINE"].ToString();
            else
                strIN_MARINE = "0";
            if (Request["LOSS_DATE"] != null && Request["LOSS_DATE"].ToString() != "")
                strLOSS_DATE = Request["LOSS_DATE"].ToString();





        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            //this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        private void SetTabs()
        {
            ResourceManager objResourceMgr = new ResourceManager("Cms.Claims.Aspx.ClaimsTab", Assembly.GetExecutingAssembly());

            string url;
            string title;
            int TabCounter = 0;

            url = "AddClaimsNotification.aspx?&CUSTOMER_ID=" + strCustomerId + "&POLICY_ID=" + strPolicyID + "&POLICY_VERSION_ID=" + strPolicyVersionID + "&CLAIM_ID=" + strClaimID + "&LOB_ID=" + strLOB_ID + "&ADD_NEW=" + strAddNew + "&HOMEOWNER=" + strHOMEOWNER + "&RECR_VEH=" + strRECR_VEH + "&IN_MARINE=" + strIN_MARINE + "&LOSS_DATE=" + strLOSS_DATE + "&DIARY_ID=" + strDiaryID + "&";
            if (Request["INSURED_NAME"] != null && Request["EFFECTIVE_DATE"] != null && Request["EXPIRATION_DATE"] != null && Request["NOTES"] != null && Request["LOB"] != null && Request["ADDRESS"] != null)
                url += "&INSURED_NAME=" + Request["INSURED_NAME"].ToString() + "&EFFECTIVE_DATE=" + Request["EFFECTIVE_DATE"].ToString() + "&EXPIRATION_DATE=" + Request["EXPIRATION_DATE"].ToString() + "&NOTES=" + Request["NOTES"].ToString() + "&ADDRESS=" + Request["ADDRESS"].ToString() + "&LOB=" + Request["LOB"].ToString() + "&";

            if (Request["DUMMY_POLICY"] != null)
                url += "&DUMMY_POLICY=" + Request["DUMMY_POLICY"].ToString() + "&";

            if (Request["DUMMY_POLICY_ID"] != null)
                url += "&DUMMY_POLICY_ID=" + Request["DUMMY_POLICY_ID"].ToString() + "&";

            title = objResourceMgr.GetString("Claims_Notification");
            TabCtl.TabLength = 120;
            TabCtl.TabTitles = title;
            TabCtl.TabURLs = url;
            TabCounter++;
            if (strClaimID != "" && strClaimID != "0")
            {
                url = "AddOccurrenceDetails.aspx?&CUSTOMER_ID=" + strCustomerId + "&POLICY_ID=" + strPolicyID + "&POLICY_VERSION_ID=" + strPolicyVersionID + "&CLAIM_ID=" + strClaimID + "&LOB_ID=" + strLOB_ID + "&";
                if (strLOB_ID == ((int)enumLOB.GENL).ToString() || strLOB_ID == ((int)enumLOB.HOME).ToString() || strLOB_ID == ((int)enumLOB.REDW).ToString())
                    title = objResourceMgr.GetString("Property_Loss");
                else
                    title = objResourceMgr.GetString("Occurrence_Details");
                TabCtl.TabLength = 120;
                TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                TabCtl.TabTitles = TabCtl.TabTitles + "," + title;
                TabCounter++;

                //LOB: Automobile		
                if (strLOB_ID == ((int)enumLOB.AUTOP).ToString() || strLOB_ID == ((int)enumLOB.CYCL).ToString())
                {
                    TabCtl.TabLength = 120;
                    url = "InsuredVehicleIndex.aspx?&CLAIM_ID=" + strClaimID + "&LOB_ID=" + strLOB_ID;
                    if (strLOB_ID == ((int)enumLOB.CYCL).ToString())
                        title = objResourceMgr.GetString("Insured_Motorcycle");
                    else
                        title = objResourceMgr.GetString("Insured_Vehicle");
                    TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                    TabCtl.TabTitles = TabCtl.TabTitles + "," + title;

                    url = "OwnerDetailsIndex.aspx?TYPE_OF_OWNER=" + ((int)enumTYPE_OF_DRIVER.INSURED_VEHICLE).ToString() + "&CUSTOMER_ID=" + strCustomerId + "&POLICY_ID=" + strPolicyID + "&POLICY_VERSION_ID=" + strPolicyVersionID + "&CLAIM_ID=" + strClaimID + "&LOB_ID=" + strLOB_ID + "&";
                    title = objResourceMgr.GetString("Owner_Details");
                    TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                    TabCtl.TabTitles = TabCtl.TabTitles + "," + title;

                    url = "DriverDetailsIndex.aspx?TYPE_OF_DRIVER=" + ((int)enumTYPE_OF_DRIVER.INSURED_VEHICLE).ToString() + "&CUSTOMER_ID=" + strCustomerId + "&POLICY_ID=" + strPolicyID + "&POLICY_VERSION_ID=" + strPolicyVersionID + "&CLAIM_ID=" + strClaimID + "&LOB_ID=" + strLOB_ID + "&TYPE_OF_OWNER=" + ((int)enumTYPE_OF_OWNER.INSURED_VEHICLE).ToString() + "&";
                    title = objResourceMgr.GetString("Driver_Information");
                    TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                    TabCtl.TabTitles = TabCtl.TabTitles + "," + title;
                }
                //				else if(strLOB_ID==((int)enumLOB.CYCL).ToString())
                //				{
                //					TabCtl.TabLength = 150;
                //					url="InsuredVehicleIndex.aspx?&CLAIM_ID=" + strClaimID + "&LOB_ID=" + strLOB_ID;
                //					title = "Insured Motorcycle";
                //					TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                //					TabCtl.TabTitles = TabCtl.TabTitles + "," + title;	
                //
                //					url="DriverDetailsIndex.aspx?TYPE_OF_DRIVER=" + ((int)enumTYPE_OF_DRIVER.INSURED_VEHICLE).ToString() + "&CUSTOMER_ID=" + strCustomerId + "&POLICY_ID=" + strPolicyID + "&POLICY_VERSION_ID=" + strPolicyVersionID + "&CLAIM_ID=" + strClaimID + "&";
                //					title="Driver Information";
                //					TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                //					TabCtl.TabTitles = TabCtl.TabTitles + "," + title;					
                //				}
                else if (strLOB_ID == ((int)enumLOB.HOME).ToString())
                {
                    //Set tab length to large size as there are fewer tabs for current LOB
                    TabCtl.TabLength = 120;
                    //url = "InsuredLocationIndex.aspx?&CLAIM_ID=" + strClaimID + "&LOB_ID=" + strLOB_ID + "&";
                    //title = objResourceMgr.GetString("Insured_Location");
                    //TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                    //TabCtl.TabTitles = TabCtl.TabTitles + "," + title;

                    //url = "RecrVehiclesIndex.aspx?&CLAIM_ID=" + strClaimID + "&LOB_ID=" + strLOB_ID + "&";
                    //title = objResourceMgr.GetString("RV_Info");
                    //TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                    //TabCtl.TabTitles = TabCtl.TabTitles + "," + title;

                    //url = "InsuredBoatIndex.aspx?&CLAIM_ID=" + strClaimID + "&LOB_ID=" + strLOB_ID + "&";
                    //title = objResourceMgr.GetString("Insured_Boat");
                    //TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                    //TabCtl.TabTitles = TabCtl.TabTitles + "," + title;

                    //url = "DriverDetailsIndex.aspx?TYPE_OF_DRIVER=" + ((int)enumTYPE_OF_DRIVER.INSURED_VEHICLE).ToString() + "&CUSTOMER_ID=" + strCustomerId + "&POLICY_ID=" + strPolicyID + "&POLICY_VERSION_ID=" + strPolicyVersionID + "&CLAIM_ID=" + strClaimID + "&LOB_ID=" + strLOB_ID + "&TYPE_OF_OWNER=" + ((int)enumTYPE_OF_OWNER.INSURED_VEHICLE).ToString() + "&";
                    //title = objResourceMgr.GetString("Operator_Driver");
                    //TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                    //TabCtl.TabTitles = TabCtl.TabTitles + "," + title;

                    /*url="AddPropertyDamaged.aspx?&CLAIM_ID=" + strClaimID + "&LOB_ID=" + strLOB_ID + "&";
                    title = "Scheduled Covg.";
                    TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                    TabCtl.TabTitles = TabCtl.TabTitles + "," + title;*/
                }
                else if (strLOB_ID == ((int)enumLOB.REDW).ToString())
                {
                    //Set tab length to large size as there are fewer tabs for current LOB
                    TabCtl.TabLength = 120;
                    url = "InsuredLocationIndex.aspx?&CLAIM_ID=" + strClaimID + "&LOB_ID=" + strLOB_ID + "&";
                    title = objResourceMgr.GetString("Insured_Location");
                    TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                    TabCtl.TabTitles = TabCtl.TabTitles + "," + title;
                }
                else if (strLOB_ID == ((int)enumLOB.UMB).ToString())
                {
                    TabCtl.TabLength = 120;

                    /*url="InsuredLocationIndex.aspx?&CLAIM_ID=" + strClaimID + "&LOB_ID=" + strLOB_ID + "&";
                    title = "Insured Location";
                    TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                    TabCtl.TabTitles = TabCtl.TabTitles + "," + title;													

                    url="OwnerDetailsIndex.aspx?TYPE_OF_OWNER=" + ((int)enumTYPE_OF_OWNER.INSURED_VEHICLE).ToString() + "&CUSTOMER_ID=" + strCustomerId + "&POLICY_ID=" + strPolicyID + "&POLICY_VERSION_ID=" + strPolicyVersionID + "&CLAIM_ID=" + strClaimID + "&LOB_ID=" + strLOB_ID + "&";
                    title = "Owner Details";
                    TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                    TabCtl.TabTitles = TabCtl.TabTitles + "," + title;	

                    url="DriverDetailsIndex.aspx?TYPE_OF_DRIVER=" + ((int)enumTYPE_OF_DRIVER.INSURED_VEHICLE).ToString() + "&CUSTOMER_ID=" + strCustomerId + "&POLICY_ID=" + strPolicyID + "&POLICY_VERSION_ID=" + strPolicyVersionID + "&CLAIM_ID=" + strClaimID + "&LOB_ID=" + strLOB_ID + "&";
                    title="Driver Information";
                    TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                    TabCtl.TabTitles = TabCtl.TabTitles + "," + title;					

                    url="InsuredVehicleIndex.aspx?&CLAIM_ID=" + strClaimID +  "&LOB_ID=" + strLOB_ID + "&";
                    title="Insured Vehicle";
                    TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                    TabCtl.TabTitles = TabCtl.TabTitles + "," + title;	


                    url="OtherStructIndex.aspx?&CLAIM_ID=" + strClaimID +  "&LOB_ID=" + strLOB_ID + "&";
                    title="Other Struc. Detail";
                    TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                    TabCtl.TabTitles = TabCtl.TabTitles + "," + title;	*/



                    //					url="PropertyDamagedIndex.aspx?&CUSTOMER_ID=" + strCustomerId + "&POLICY_ID="+strPolicyID+"&POLICY_VERSION_ID="+strPolicyVersionID+ "&CLAIM_ID=" + strClaimID + "&LOB_ID=" + strLOB_ID + "&";
                    //					title = "Property Damaged";
                    //					TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                    //					TabCtl.TabTitles = TabCtl.TabTitles + "," + title;								

                }
                else if (strLOB_ID == ((int)enumLOB.GENL).ToString())
                {
                    //Set tab length to large size as there are fewer tabs for current LOB
                    TabCtl.TabLength = 120;
                    url = "LiabilityTypeIndex.aspx?&CLAIM_ID=" + strClaimID + "&LOB_ID=" + strLOB_ID + "&";
                    title = objResourceMgr.GetString("Liability_Type");
                    TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                    TabCtl.TabTitles = TabCtl.TabTitles + "," + title;

                    url = "OwnerDetailsIndex.aspx?TYPE_OF_OWNER=" + ((int)enumTYPE_OF_OWNER.LIABILITY_TYPE_OWNER).ToString() + "&CUSTOMER_ID=" + strCustomerId + "&POLICY_ID=" + strPolicyID + "&POLICY_VERSION_ID=" + strPolicyVersionID + "&CLAIM_ID=" + strClaimID + "&LOB_ID=" + strLOB_ID + "&";
                    title = objResourceMgr.GetString("Owner_Details");
                    TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                    TabCtl.TabTitles = TabCtl.TabTitles + "," + title;

                    url = "OwnerDetailsIndex.aspx?TYPE_OF_OWNER=" + ((int)enumTYPE_OF_OWNER.LIABILITY_TYPE_MANUFACTURER).ToString() + "&CUSTOMER_ID=" + strCustomerId + "&POLICY_ID=" + strPolicyID + "&POLICY_VERSION_ID=" + strPolicyVersionID + "&CLAIM_ID=" + strClaimID + "&LOB_ID=" + strLOB_ID + "&";
                    title = objResourceMgr.GetString("Manufacturer_Details");
                    TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                    TabCtl.TabTitles = TabCtl.TabTitles + "," + title;
                }
                else if (strLOB_ID == ((int)enumLOB.BOAT).ToString())
                {
                    //Set tab length to large size as there are fewer tabs for current LOB
                    //Asfa (19-Mar-2008) - iTrack issue #3791
                    TabCtl.TabLength = 120;

                    url = "InsuredBoatIndex.aspx?&CLAIM_ID=" + strClaimID + "&LOB_ID=" + strLOB_ID + "&";
                    title = objResourceMgr.GetString("Insured_Boat");
                    TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                    TabCtl.TabTitles = TabCtl.TabTitles + "," + title;

                    url = "DriverDetailsIndex.aspx?TYPE_OF_DRIVER=" + ((int)enumTYPE_OF_DRIVER.INSURED_VEHICLE).ToString() + "&CUSTOMER_ID=" + strCustomerId + "&POLICY_ID=" + strPolicyID + "&POLICY_VERSION_ID=" + strPolicyVersionID + "&CLAIM_ID=" + strClaimID + "&LOB_ID=" + strLOB_ID + "&TYPE_OF_OWNER=" + ((int)enumTYPE_OF_OWNER.INSURED_VEHICLE).ToString() + "&";
                    title = objResourceMgr.GetString("Operator_Driver_Information");
                    TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                    TabCtl.TabTitles = TabCtl.TabTitles + "," + title;
                }

                // SANTOSH GAUTAM: ADDED ON 08 Nov 2010
                title = objResourceMgr.GetString("Risk_Information");
                url = "AddRiskInformation.aspx?&CUSTOMER_ID=" + strCustomerId + "&POLICY_ID=" + strPolicyID + "&POLICY_VERSION_ID=" + strPolicyVersionID + "&CLAIM_ID=" + strClaimID + "&LOB_ID=" + strLOB_ID + "&POLICY_EFFECTIVE_DATE=" + strPOLICY_EFFECTIVE_DATE + "&POLICY_EXPIRATION_DATE=" + strPOLICY_EXPIRATION_DATE+"&";
                TabCtl.TabLength = 120;
                TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                TabCtl.TabTitles = TabCtl.TabTitles + "," + title;
                TabCounter++;
              
                // SANTOSH GAUTAM: ADDED ON 12 Nov 2010
                if (strLITIGATION_FILE == "10963")
                {
                    TabCounter++;

                    title = objResourceMgr.GetString("Litigation");
                    url = "LitigationIndex.aspx?&CUSTOMER_ID=" + strCustomerId + "&POLICY_ID=" + strPolicyID + "&POLICY_VERSION_ID=" + strPolicyVersionID + "&CLAIM_ID=" + strClaimID + "&LOB_ID=" + strLOB_ID + "&";
                    TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                    TabCtl.TabLength = 80;
                    TabCtl.TabTitles = TabCtl.TabTitles + "," + title;
                    
                }

                //int intLobID = int.Parse(strLOB_ID);
                // SANTOSH GAUTAM: ADDED ON 12 Nov 2010
                //if (intLobID <9 && intLobID<36)
                {
                    //title = "Property Damaged"; Modified 01 2007
                    title = objResourceMgr.GetString("Third_Party_Damage");
                    url = "PropertyDamagedIndex.aspx?&CUSTOMER_ID=" + strCustomerId + "&POLICY_ID=" + strPolicyID + "&POLICY_VERSION_ID=" + strPolicyVersionID + "&CLAIM_ID=" + strClaimID + "&LOB_ID=" + strLOB_ID + "&";
                    //title = "Property Damaged"; Modified 01 2007
                    TabCtl.TabLength = 120;
                    TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                    TabCtl.TabTitles = TabCtl.TabTitles + "," + title;
                    TabCounter++;
                }
                // SANTOSH GAUTAM: ADDED ON 15 Nov 2010
                if (strCO_INSURANCE_TYPE == "1")
                {

                    title = objResourceMgr.GetString("Coinsurance");
                    url = "AddCoinsuranceDetails.aspx?&CUSTOMER_ID=" + strCustomerId + "&POLICY_ID=" + strPolicyID + "&POLICY_VERSION_ID=" + strPolicyVersionID + "&CLAIM_ID=" + strClaimID + "&LOB_ID=" + strLOB_ID + "&LITIGATION_FILE=" + strLITIGATION_FILE  +"&POLICY_EFFECTIVE_DATE=" + strPOLICY_EFFECTIVE_DATE + "&POLICY_EXPIRATION_DATE=" + strPOLICY_EXPIRATION_DATE + "&";
                    TabCtl.TabLength = 120;
                    TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                    TabCtl.TabTitles = TabCtl.TabTitles + "," + title;
                    TabCounter++;

                }


                // SANTOSH GAUTAM: ADDED ON 08 Feb 2011
                if (strIS_VICTIM_CLAIM == "10963")
                {
                    if (strLOB_ID == ((int)enumLOB.PAPEACC).ToString())
                        title = objResourceMgr.GetString("Passenger_Information");
                     else
                        title = objResourceMgr.GetString("Victim_Information");

                    url = "VictimIndex.aspx?&CUSTOMER_ID=" + strCustomerId + "&POLICY_ID=" + strPolicyID + "&POLICY_VERSION_ID=" + strPolicyVersionID + "&CLAIM_ID=" + strClaimID + "&LOB_ID=" + strLOB_ID + "&";
                    TabCtl.TabLength = 120;
                    TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                    TabCtl.TabTitles = TabCtl.TabTitles + "," + title;
                    TabCounter++;
                }

                // SANTOSH GAUTAM: ADDED ON 15 Nov 2010
                title = objResourceMgr.GetString("Claim_Coverage");
                TabCtl.TabLength = 120;
                url = "ClaimCoveragesIndex.aspx?&CUSTOMER_ID=" + strCustomerId + "&POLICY_ID=" + strPolicyID + "&POLICY_VERSION_ID=" + strPolicyVersionID + "&CLAIM_ID=" + strClaimID + "&LOB_ID=" + strLOB_ID + "&LITIGATION_FILE=" + strLITIGATION_FILE + "&CO_INSURANCE_TYPE=" + strCO_INSURANCE_TYPE + "&IS_VICTIM_CLAIM=" + strIS_VICTIM_CLAIM + "&ACC_COI_FLG="+strACC_COI_FLG+ "&"; 
                TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                TabCtl.TabTitles = TabCtl.TabTitles + "," + title;

                TabCounter++;
              

                //url="/cms/cmsweb/Maintenance/AttachmentIndex.aspx?calledfrom=POLICY&EntityType=Application&EntityId=0&CUSTOMER_ID=" + strCustomerId +"&APP_ID="+ strAppId + "&APP_VERSION_ID="+ strAppVersionId+"&POLICY_LOB="+strLobID+"&";
                url = "/cms/cmsweb/Maintenance/AttachmentIndex.aspx?calledfrom=CLAIM&EntityType=Claim&EntityId=" + strClaimID + "&CUSTOMER_ID=" + strCustomerId + "&LOB_ID=" + strLOB_ID + "&";
                title = objResourceMgr.GetString("Attachment");
                TabCtl.TabLength = 120;
                TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                TabCtl.TabTitles = TabCtl.TabTitles + "," + title;
                TabCounter++;

                if (strCustomerId == "0" || strCustomerId == "")
                {
                    url = "/cms/claims/Aspx/DummyClaimsCoveragesIndex.aspx?EntityId=" + strClaimID + "&";
                    title = objResourceMgr.GetString("Add_Coverages");
                    TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                    TabCtl.TabTitles = TabCtl.TabTitles + "," + title;
                }

                if(TabCounter>8)
                    TabCtl.TabLength = 110;
                /*url="Reports/PaidClaimsByCoverageReport.aspx?&CLAIM_ID=" + strClaimID + "&LOB_ID=" + strLOB_ID + "&";
                title="Coverage Report";
                TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                TabCtl.TabTitles = TabCtl.TabTitles + "," + title;*/
            }
        }
    }
}
