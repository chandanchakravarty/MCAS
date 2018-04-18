using System;
using Cms.CmsWeb;
using Cms.BusinessLayer.BlProcess;

namespace Cms.Policies
{
	/// <summary>
	/// Summary description for policiesbase.
	/// </summary>
	public class policiesbase :Cms.CmsWeb.cmsbase 
	{
		#region Const defination for different processes
		public const int POLICY_CANCELLATION_PROCESS	= 2;
		public const int POLICY_ENDORSEMENT_PROCESS		= 3;
		public const int POLICY_REINSTATEMENT_PROCESS	= 4;
		public const int POLICY_RENEWAL_PROCESS			= 5;
		public const int POLICY_NON_RENEWAL_PROCESS		= 6;
		public const int POLICY_NEGATE_PROCESS			= 7;
		public const int POLICY_NEW_BUSINESS_PROCESS	= 24;
		//Added by sumit
		//11489>Bass boat (w/Motor)
		//11369>Outboard Boat
		//11487>Outboard (w/Motor)
		//11374>Pontoon (w/Motor)
		public const string TYPE_OF_WATERCRAFT_OUTBOARD_BOAT = "11369";
		public const string TYPE_OF_WATERCRAFT_BASS_BOAT_WITHOUT_MOTOR = "11489";
		public const string TYPE_OF_WATERCRAFT_OUTBOARD_WITHOUT_MOTOR = "11487";
		public const string TYPE_OF_WATERCRAFT_PONTOON = "11374";

		public string POLICY_STATUS_UNDER_ENDORSEMENT = "UENDRS";
		#endregion

		#region LobIDs Const
		public const string LOB_HOME="1";
		public const string LOB_PRIVATE_PASSENGER="2";
		public const string LOB_MOTORCYCLE="3";
		public const string LOB_WATERCRAFT="4";
		public const string LOB_UMBRELLA="5";
		public const string LOB_RENTAL_DWELLING="6";
		public const string LOB_GENERAL_LIABILITY="7";
        //Added by Charles on 19-Mar-10 for Policy Page Implementation
        public const string LOB_AVIATION = "8";
        public const string LOB_ALL_RISKS_AND_NAMED_PERILS = "9";
        public const string LOB_COMPREHENSIVE_CONDOMINIUM = "10";
        public const string LOB_COMPREHENSIVE_COMPANY = "11";
        public const string LOB_GENERAL_CIVIL_LIABILITY = "12";
        public const string LOB_MARITIME = "13";
        public const string LOB_DIVERSIFIED_RISKS = "14";
        public const string LOB_INDIVIDUAL_PERSONAL_ACCIDENT = "15";
        public const string LOB_ROBBERY = "16";
        public const string LOB_FACULTATIVE_LIABILITY = "17";
        public const string LOB_CIVIL_LIABILITY_TRANSPORTATION = "18";
        public const string LOB_DWELLING = "19";
        //Added till here
        public const string LOB_NATIONAL_AND_INTERNATIONAL_TRANSPORTATION = "20";

        //Added By Pradeep Kushwaha on 28-April-2010
        public const string GROUP_PASSENGER_PERSONAL_ACCIDENT  = "21";
        public const string PASSENGER_PERSONAL_ACCIDENT  = "22";
        public const string INTERNATIONAL_CARGO_TRANSPORT  = "23";
        //End Added

        //Added By Lalit For New Product From 25 to 35
        public const string TRADITIONAL_FIRE = "25";
        public const string ENGENEERING_RISKS = "26";
        public const string GLOBAL_OF_BANK = "27";
        public const string AERONAUTIC = "28";
        public const string MOTOR = "29";
        public const string DPVAT = "30";
        public const string CARGO_TRANSPORTATION_CIVIL_LIABILITY = "31";
        public const string JUDICIAL_GUARANTEE = "32";
        public const string MORTGAGE = "33";
        public const string GROUP_LIFE = "34";
        public const string RURAL_LIEN = "35";
        public const string DPVAT2 = "36";
        public const string RETSURTY = "37";
        //End here lalit Changes

        //Added by Agniswar for Singapore changes

        public const string MOTOR_VEHICLE = "38";
        public const string FIRE = "39";
        public const string MARINE_CARGO = "40";

        //Till here
		#endregion

		public policiesbase()
		{
			
		}

		/// <summary>
		/// Writes the script, which will reload the menu for the specific lob of policy
		/// </summary>
		public void ReloadPolicyMenu(string DefaultPage)
		{
			string strRefreshtopMenuScript;
			strRefreshtopMenuScript = "<script language='javascript'>"
				+ "top.topframe.createPolicyTopMenu('" + GetPolicyID() + "','" + GetPolicyVersionID() + "','" + GetCustomerID() + "','" + GetLOBString() + "');"
				+ "top.topframe.main1.mousein = false;top.topframe.findMouseIn();</script>";

			ClientScript.RegisterStartupScript(this.GetType(),"REFRESHTOPMENU",strRefreshtopMenuScript);
		}
		public void OpenEndorsementDetails()
		{

            return; //itrack no 437
            //Retreiving the active process executing on policy
			Cms.BusinessLayer.BlProcess.ClsEndorsmentProcess objProcess = new Cms.BusinessLayer.BlProcess.ClsEndorsmentProcess ();
			Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo = objProcess.GetRunningProcess( int.Parse(GetCustomerID())
				, int.Parse(GetPolicyID()));

			
			if (objProcessInfo != null)
			{
				//If running process is endorsement process, opening the endorsement pop up window
				if (objProcessInfo.PROCESS_ID == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ENDORSEMENT_PROCESS && objProcessInfo.NEW_POLICY_VERSION_ID == int.Parse(GetPolicyVersionID ()))
				{
					//Policy is under endorsement process, hence showing the endorsement details popup window
					string strOpenEndorsementWin;
                    string QueryStr = "POLICY_ID=" + objProcessInfo.POLICY_ID + "&"
                            + "POLICY_VERSION_ID=" + objProcessInfo.NEW_POLICY_VERSION_ID + "&"
                            + "CUSTOMER_ID=" + objProcessInfo.CUSTOMER_ID + "&"
                            + "ENDORSEMENT_NO=" + objProcessInfo.ENDORSEMENT_NO + "&";
                        if (Session["EndorsementTranIds"] != null)                            
                            QueryStr += "TRANS_ID=" + Session["EndorsementTranIds"].ToString() + "&";
                        else
                            QueryStr += "TRANS_ID=0&";

                    QueryStr = Cms.CmsWeb.QueryStringModule.EncriptQueryString(QueryStr);
                    strOpenEndorsementWin = "<script>"
                            + "winEndorsement = ShowPopup('/cms/policies/processes/endorsementdetails.aspx" + QueryStr
                            + "', 'Endorsement',400, 200);"
							+ "</script>";

					//if (Session["TransactionId"] != null)
					/*if (Session["EndorsementTranIds"] != null)
						strOpenEndorsementWin = "<script>"
                            + "winEndorsement = ShowPopup('/cms/policies/processes/endorsementdetails.aspx?" 
							+ "POLICY_ID=" + objProcessInfo.POLICY_ID + "&"
//							+ "POLICY_VERSION_ID=" + objProcessInfo.POLICY_VERSION_ID + "&" 
							// Changed by Swarup(07-feb-2008) as it is taking old version id when it should take new version id
							+ "POLICY_VERSION_ID=" + objProcessInfo.NEW_POLICY_VERSION_ID + "&"
							+ "CUSTOMER_ID=" + objProcessInfo.CUSTOMER_ID + "&"
							+ "ENDORSEMENT_NO=" + objProcessInfo.ENDORSEMENT_NO + "&"
							+ "TRANS_ID=" + Session["EndorsementTranIds"].ToString() + "&', 'Endorsement',400, 200);"
							+ "</script>";
					else
						strOpenEndorsementWin = "<script>"
							+ "winEndorsement = ShowPopup('/cms/policies/processes/endorsementdetails.aspx?"
							+ "POLICY_ID=" + objProcessInfo.POLICY_ID + "&"
//							+ "POLICY_VERSION_ID=" + objProcessInfo.POLICY_VERSION_ID + "&"
							// Changed by Swarup(07-feb-2008) as it is taking old version id when it should take new version id
							+ "POLICY_VERSION_ID=" + objProcessInfo.NEW_POLICY_VERSION_ID + "&"
							+ "CUSTOMER_ID=" + objProcessInfo.CUSTOMER_ID + "&"
							+ "ENDORSEMENT_NO=" + objProcessInfo.ENDORSEMENT_NO + "&"
							+ "TRANS_ID=0&', 'Endorsement',400, 200);"
							+ "</script>";
                    */

                    ClientScript.RegisterStartupScript(this.GetType(),"OPENWINDOW", strOpenEndorsementWin);
				}
			}
			
		}


		/// <summary>
		/// Sets the specified policy in session and refreshes the menus also, Calling page should have Setmenu javascript method
		/// </summary>
		public void SetPolicyInSession(int PolicyID, int PolicyVersionID, int CustomerID)
		{
			SetPolicyID (PolicyID.ToString());
			SetPolicyVersionID (PolicyVersionID.ToString());
			SetCustomerID( CustomerID.ToString());
			SetCalledFor("POLICY");
				
			Cms.BusinessLayer.BlApplication.ClsGeneralInformation obj = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
			System.Data.DataSet ds = obj.GetPolicyDataSet(CustomerID, PolicyID, PolicyVersionID);

			//Set the Policy Status
			SetPolicyStatus(obj.GetStatusOfPolicy(CustomerID, PolicyID, PolicyVersionID));

			if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
			{
				//Saving the application,version and lob in session
				SetAppID(ds.Tables[0].Rows[0]["APP_ID"].ToString());
				SetAppVersionID(ds.Tables[0].Rows[0]["APP_VERSION_ID"].ToString());
				SetLOBID(ds.Tables[0].Rows[0]["POLICY_LOB"].ToString());
                SetPolicyCurrency(ds.Tables[0].Rows[0]["POLICY_CURRENCY"].ToString());

				switch(GetLOBID())
				{
					case LOB_HOME:
						SetLOBString("HOME");
						break;
					case LOB_PRIVATE_PASSENGER:
						SetLOBString("PPA");
						break;
					case LOB_MOTORCYCLE:
						SetLOBString("MOT");
						break;
					case LOB_WATERCRAFT:
						SetLOBString("WAT");
						break;
					case LOB_RENTAL_DWELLING:
						SetLOBString("RENT");
						break;
					case LOB_UMBRELLA:
						SetLOBString("UMB");
						break;
					case LOB_GENERAL_LIABILITY:
						SetLOBString("GEN");
						break;
                    //Added by Charles on 13-Apr-10 for Policy Page
                    case LOB_AVIATION:
                        SetLOBString("AVIATION");
                        break;
                    case MOTOR_VEHICLE:
                        SetLOBString("MOT");
                        break;
                    default:
                        SetLOBString(ds.Tables[0].Rows[0]["LOB_CODE"].ToString());
                        break;
                   //Added till here
				}
			}


			//Reloading the policy menu
			ReloadPolicyMenu("");

			//Calling the setmenu method which enable or disable the menus
			//Calling page should have set menu method
            ClientScript.RegisterStartupScript(this.GetType(),"ENABLEMENU", "<script>setMenu();</script>");
		}
		
		#region ScreenID
		//Overriding the screen id property of cmsbase class
		public new string ScreenId
		{
			get
			{
				return base.ScreenId;
			}
			set
			{
				base.ScreenId = value;
				
				
				//Here we will check whether the policy exists for the appliation in session
				//If policy exists then user can not edit any information , hence we will change
				//the security xml to view mode only
				try
				{
					string strPolicyId = GetPolicyID();
					string strPolicyVerId = GetPolicyVersionID();
					string strCustomerID = GetCustomerID();
					
					Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGenInfo;
					objGenInfo = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();

					System.Data.DataSet ds = objGenInfo.GetPolicyDataSet(int.Parse(strCustomerID), int.Parse(strPolicyId), int.Parse(strPolicyVerId));
					if (ds.Tables[0].Rows.Count > 0)
					{
						string policyStatus = ds.Tables[0].Rows[0]["POLICY_STATUS_CODE"].ToString().ToUpper().Trim();
                        string appStatus = ds.Tables[0].Rows[0]["APP_STATUS"].ToString().ToUpper().Trim();
						if ( ! ( policyStatus == ClsPolicyProcess.POLICY_STATUS_UNDER_ENDORSEMENT 
							|| policyStatus == ClsPolicyProcess.POLICY_STATUS_UNDER_RENEW
							|| policyStatus == ClsPolicyProcess.POLICY_STATUS_UNDER_CORRECTIVE_USER 
							//|| policyStatus == ClsPolicyProcess.POLICY_STATUS_UNDER_ISSUE
							//|| policyStatus == ClsPolicyProcess.POLICY_STATUS_SUSPENDED
							|| policyStatus == ClsPolicyProcess.POLICY_STATUS_RENEWAL_SUSPENSE 
							|| policyStatus == ClsPolicyProcess.POLICY_STATUS_UNDER_REWRITE 
							|| policyStatus == ClsPolicyProcess.POLICY_STATUS_SUSPENSE_REWRITE
							|| policyStatus == ClsPolicyProcess.POLICY_STATUS_SUSPENSE_ENDORSEMENT
                            || appStatus == ClsPolicyProcess.POLICY_STATUS_APPLICATION
                            //|| appStatus == ClsPolicyProcess.POLICY_STATUS_REJECT
                            //|| policyStatus == ClsPolicyProcess.POLICY_STATUS_REJECT
                            || appStatus == ClsPolicyProcess.POLICY_STATUS_QAPP)//Added by Charles on 19-Mar-2010 for Policy Page Implementation
                            || (GetCalledFor()=="QAPP" && appStatus == ClsPolicyProcess.POLICY_STATUS_APPLICATION)
                            )
							//|| policyStatus == ClsPolicyProcess.POLICY_STATUS_UNDER_REINSTATE  removed on 9 july as Under Reinstate No risk can be updated
							//Changing the security xml to view mode only
							gstrSecurityXML = "<Security><Read>Y</Read><Write>N</Write><Delete>N</Delete><Execute>N</Execute></Security>";

						// If any agency has logged in other than wolverine
						// then, a policy with suspended status will not be shown the buttons.
						// iTrack - 1509
						if(GetSystemId() != CarrierSystemID.ToUpper())//&& (policyStatus == ClsPolicyProcess.POLICY_STATUS_SUSPENDED)) 
						{
							//Changing the security xml to view mode only
							gstrSecurityXML = "<Security><Read>Y</Read><Write>N</Write><Delete>N</Delete><Execute>N</Execute></Security>";
						}
						  base.InitializeSecuritySettings(); 
					}
					ds.Dispose();
                    //Added By Lalit April 11,2011.Master policy implimentation 
                    //For master policy Endorsement will be per co-applicant 
                    //Selected Co-applicant Risk info will be editable on endorsement,rest all non editable
                    int Co_APP_ID ;
                    Co_APP_ID = Cms.BusinessLayer.BlApplication.ClsGeneralInformation.GetPolicyEndorsementCoApplicant
                        (int.Parse(strCustomerID), int.Parse(strPolicyId), int.Parse(strPolicyVerId));
                    SetEndorsementCoApplicant(Co_APP_ID.ToString());

				}
				catch
				{
				}
			}
		}
		#endregion


        //Added By Lalit, April 11 2011.
        /// <summary>
        /// Disable save button at endorseemnt in progress for master policy, if select co-applicant != CoApplicant at endorsement screeen
        /// </summary>
        /// <param name="Co_AppId"></param>
        protected string CustomSecurityXml(string Co_AppId)
        {            
            if (Co_AppId != "NEW")
            {
                string CustomPermissionString = "<Security><Read>Y</Read><Write>N</Write><Delete>N</Delete><Execute>N</Execute></Security>";
                if (GetPolicyStatus() == POLICY_STATUS_UNDER_ENDORSEMENT)
                {
                    if (GetEndorsementCoApplicant().Trim() != Co_AppId.Trim())
                    {
                        return CustomPermissionString;
                    }
                }
            }
            return gstrSecurityXML;
        }


	}
}
