namespace Cms.CmsWeb.WebControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Resources;
	using System.Reflection;
	using System.Xml;
	//using BritAmazon.BusinessObjects;
	using Cms.BusinessLayer;
    using Cms.CmsWeb;
	
	using System.Globalization;
	//using BritAmazon.BritAmazonWeb.BaseObjects;
	
	/// <summary	///		Summary description for ClientTop1.
	/// </summary>
	public abstract class ClientTop : System.Web.UI.UserControl
	{
		protected string strHeadClientFullName="";
        protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		protected string lstrSyscode="";
		protected string strHeadClientType="";
		protected string strHeadClientPhone="";
		protected string strHeadClientTitle="";
		protected string gStrStyle="";
		protected string strClientID="";
		protected string strActClientID="";
		protected string gstrUserType="";
		protected string strBrokerID="";
		protected string strType="";
		protected string lstrTitleID="";
		protected string strHeadClientFullAddress="";
		protected string strCarrier="";
		protected string strlblClientInfo="";
		protected string strlblAKADBAName="";
		protected string strlblLocation="";
		protected string strlblAddress="";
		protected string strlblPhone="";
		protected string strlblAttentionNotes="";
		protected int intCustomerID;
		protected int intApplicationID;
		protected int intAppVersionID;
		protected int intPolicyId;
		protected int intPolicyVersionId;
		protected string strFlagApp="";
        protected string strLOB_ID = "";
		public System.Text.StringBuilder sbClaimLink = new System.Text.StringBuilder();      

		// change
		protected System.Web.UI.WebControls.Label lblName, lblType, lblClientType, lblQuoteStatus, lblQuoteStatusValue;
		protected System.Web.UI.WebControls.Label lblNameQP,lblFullNameQP,lblAddressQP,lblClientAddressQP;
		protected System.Web.UI.WebControls.Label lblFullName, lblTitle, lblClientTitle;
        protected System.Web.UI.WebControls.Label lblAddress, lblClientAddress, lblPhone, lblClientPhone, lblCustAgencyPhone, lblSCustAgencyPhone;
		protected System.Web.UI.WebControls.Label lblPolicy, lblPolicyNumber, lblVer, lblVersion, lblDateValue, lblDate;
		protected int gIntCarrierId, gIntBrokerId, gIntClientId, gIntPolicyId, gIntPolicyVersion, gIntProposalVersion, gIntClaimVersion;
		protected System.Web.UI.WebControls.Panel pnlPolicy,PanelClient;
		protected System.Web.UI.WebControls.Label lblClaimant;
		protected System.Web.UI.WebControls.Label lblClaimantVal;
		protected System.Web.UI.WebControls.Label lblClaimNo;
		protected System.Web.UI.WebControls.Label lblClaimNoVal;
		protected System.Web.UI.WebControls.Label lblDateofLoss;
		protected System.Web.UI.WebControls.Label lblDateofLossVal;
		protected System.Web.UI.WebControls.Label lblPolicyNo;
				
		protected System.Web.UI.WebControls.Label lblPolicyNoVal;
		protected System.Web.UI.WebControls.Label lblPolVersion;
		protected System.Web.UI.WebControls.Label lblPolVersionVal;
		protected System.Web.UI.WebControls.Panel pnlClaims;
		protected System.Web.UI.WebControls.Label lblInsureClaimantVal;
		protected System.Web.UI.WebControls.Image AspImageNote,AspImageNote1,AspImageNoteQ;
		protected System.Web.UI.WebControls.Label lblInsuredClaimant;
		protected System.Web.UI.WebControls.Panel PanelQuote;
		protected System.Web.UI.WebControls.Label lblBrokerQ;
		protected System.Web.UI.WebControls.Label lblBrokerNameQ;
		protected System.Web.UI.WebControls.Label lblBrokerQP;
		protected System.Web.UI.WebControls.Label lblBrokerNameQP;
		protected System.Web.UI.WebControls.Label lblProductQ;
		protected System.Web.UI.WebControls.Label lblProductNameQ;
		protected System.Web.UI.WebControls.Label lblQuoteQ;
		protected System.Web.UI.WebControls.Label lblQuoteNumberQ;
		protected System.Web.UI.WebControls.Label lblStatusQ;
		protected System.Web.UI.WebControls.Label lblStatusNameQ;
		protected System.Web.UI.WebControls.Label lblFullNameQ;
		protected System.Web.UI.WebControls.Label lblNameQ;
		protected System.Web.UI.WebControls.Label lblAddressQuote;
		protected System.Web.UI.WebControls.Label lblClientAddressQuote;
		protected System.Web.UI.WebControls.Label lblQuoteVersion;
		protected System.Web.UI.WebControls.Label lblDataQuoteVersion;
		protected System.Web.UI.WebControls.Label lblBrokerContactName;
		protected System.Web.UI.WebControls.Label lblEffectiveDate;
		protected System.Web.UI.WebControls.Label lblEffectiveDateValue;
		protected System.Web.UI.WebControls.Label lblBrokerContactNameP;
		protected System.Web.UI.WebControls.Label lblBrokerContactNamePText;
		protected System.Web.UI.WebControls.Label lblPolicyEfDate;
		protected System.Web.UI.WebControls.Label lblPolicyEfDateData;
		protected System.Web.UI.WebControls.Label lblClaimClass;
		protected System.Web.UI.WebControls.Label lblClaimClassData;
		protected System.Web.UI.WebControls.Label lblLossType;
		protected System.Web.UI.WebControls.Label lblLossTypeData;
		protected System.Web.UI.WebControls.Label lblLosSubType;
		protected System.Web.UI.WebControls.Label lblLosSubTypeData;
		protected System.Web.UI.WebControls.Panel ExistingClaim;
		protected System.Web.UI.WebControls.Label lblClientName;
		protected System.Web.UI.WebControls.Label lblClientNameData;
		protected System.Web.UI.WebControls.Label lblAddress1;
		protected System.Web.UI.WebControls.Label lblAddressData;
		protected System.Web.UI.WebControls.Label lblBroker;
		protected System.Web.UI.WebControls.Label lblBrokerData;
		protected System.Web.UI.WebControls.Label lblPolicyNoData;
		protected System.Web.UI.WebControls.Label lblPolicyVerion;
		protected System.Web.UI.WebControls.Label lblPolicyVerionData;
		protected System.Web.UI.WebControls.Label lblPolicyEfDate1;
		protected System.Web.UI.WebControls.Label lblPolicyEfDate1Data;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label lblBrokerContact;
		protected System.Web.UI.WebControls.Label lblBrokercontact1;
		protected System.Web.UI.WebControls.Label lblBrokercontact1Data;
		protected System.Web.UI.WebControls.Panel NewCliam;
		protected System.Web.UI.WebControls.Panel pnExistingClaim;
		protected System.Web.UI.WebControls.Panel pnNewCliam;
		protected System.Web.UI.WebControls.Label lblInsuredName;
		protected System.Web.UI.WebControls.Label lblInsuredNameVal;
		protected System.Web.UI.WebControls.Label lblAllocatedAdjuster;
		protected System.Web.UI.WebControls.Label lblAllocatedAdjusterVal;
		protected System.Web.UI.WebControls.Label lblClaimDesc;
		protected System.Web.UI.WebControls.Label lblClaimDescVal;
		protected string gStrCalledFrom="";
		protected System.Web.UI.WebControls.Panel pnlApplication;
		protected System.Web.UI.WebControls.Label lblCustomerName;
		protected System.Web.UI.WebControls.Label lblSCustomerName;
		protected System.Web.UI.WebControls.Label lblCustomerType;
		protected System.Web.UI.WebControls.Label lblSCustomerType;
		protected System.Web.UI.WebControls.Label lblCustomerAddress;
		protected System.Web.UI.WebControls.Label lblSCustomerAddress;
		protected System.Web.UI.WebControls.Label lblCustomerPhone;
		protected System.Web.UI.WebControls.Label lblSCustomerPhone;
        protected System.Web.UI.WebControls.Label lblPolCurrency;
        protected System.Web.UI.WebControls.Label lblSPolCurrency;

        protected System.Web.UI.WebControls.Label lblAgencyPhone;
        protected System.Web.UI.WebControls.Label lblSAgencyPhone;

        protected System.Web.UI.WebControls.Label lblEND;
        protected System.Web.UI.WebControls.Label lblEND_DETAILS;
        
		protected System.Web.UI.WebControls.Label lblAppNo;
		protected System.Web.UI.WebControls.Label lblSAppNo;
		protected System.Web.UI.WebControls.Label lblSAppNo1;
		protected System.Web.UI.WebControls.Label lblAppVersion;
		protected System.Web.UI.WebControls.Label lblSAppVersion;
		protected System.Web.UI.WebControls.Label lblStatus;
		protected System.Web.UI.WebControls.Label lblClientStatus;
		protected System.Web.UI.WebControls.Image Image1;
		private string header="";
		protected System.Web.UI.WebControls.Image CustomerDetailQ2,CustomerDetail,Image2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppEffectiveDate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppAgency;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIsAgencyTerminated;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSubmitApp;
		protected System.Web.UI.WebControls.Image imgVerifyApp;
		protected System.Web.UI.WebControls.Image imgQuote;
		protected System.Web.UI.WebControls.Image imgSubmitAnyway;
		protected System.Web.UI.WebControls.Image imgSubmitApp;		
		public string att_note="";
		protected System.Web.UI.WebControls.Image CustomerDetailQ;
		protected System.Web.UI.WebControls.Image CustomerDetail2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomer_Id;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidApp_Id;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidApp_Version_Id;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidReturnPolicyStatus;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCltNewBusinessProcess;
		protected System.Web.UI.WebControls.Label lblClaimText;		
		protected System.Web.UI.HtmlControls.HtmlTableRow trClaimRow;
		protected System.Web.UI.WebControls.Image imgFetchUndisc;


        public int intRuleverification;
        public int intShowQuoteStatus;
		protected string colorScheme;
		private string app_number = "";
        private string POLICY_STATUS = "";

        public string EncryptCustomeridQstr = "";

		public int CustomerID
		{
			get {return intCustomerID;}
			set {intCustomerID = value;}
		}


		public enum ShowHeader
		{
			Application,
			Client,
			Policy
		}

		public string ShowHeaderBand
		{
			get{return header;}
			set
			{
				header=value;
				switch (header)
				{
					case "Client":  
						ShowCustomerDetails();
						break;
					case "Application":
						ShowApplicationDetails();
						break;
					case "Policy":
						ShowPolicyLabels();
						break;
				}
			}

		}

		public int ApplicationID
		{
			get {return intApplicationID;}
			set {intApplicationID = value;}
		}

		public int AppVersionID
		{
			get {return intAppVersionID;}
			set {intAppVersionID = value;}
		}
		
		public int PolicyID
		{
			get{return intPolicyId;}
			set{intPolicyId = value;}
		}

		public int PolicyVersionID
		{
			get{return intPolicyVersionId;}
			set{intPolicyVersionId = value;}
		}

		public string FlagApp
		{
			get{return strFlagApp;}
			set{strFlagApp = value;}
		}

		public string GetSystemId()
		{
			if(Session["systemId"] == null)
			{
				return "";
			}
			return Session["systemId"].ToString(); 
				
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			CallPageLoad();
			//CheckEffectiveDate();
			
			//Check For Application Status If Policy Exists For any Version Of That Application Or Application Is Inactive
			//Then Disable The Image
            CustomerDetail2.ToolTip = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1930");
            CustomerDetailQ2.ToolTip = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1930");
            CustomerDetail.ToolTip = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1930");
            CustomerDetailQ.ToolTip = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1930");
            
			if (ShowHeaderBand.ToUpper() == "POLICY")
			{
                //imgQuote.ImageUrl = "~/cmsweb/Images/quote.gif";
                //imgVerifyApp.ImageUrl    =  "~/cmsweb/Images" + colorScheme  + "/Rule_ver.gif";
                //imgFetchUndisc.ImageUrl  =  "~/cmsweb/Images" + colorScheme  + "/calender.gif";
                //if(hidLOB.Value != "2" && hidLOB.Value != "3" && hidLOB.Value != "1" && hidLOB.Value != "4")
                //    imgFetchUndisc.Visible = false;
                //imgVerifyApp.Visible = true;
                //int intRuleverification = Cms.BusinessLayer.BlApplication.ClsGeneralInformation.CheckForRuleVerificationStatus(intCustomerID, intPolicyId, intPolicyVersionId, "POL");// PolicyVersionID ,,
                //intApplicationID = intPolicyId;  //Set intApplicationID value for pass in javascript Function ShowQuote(),It goes to quote Genrate
                //intAppVersionID = intPolicyVersionId;

                //if (Request.QueryString["POLICY_LOB"] != null)     //Check id query String have "POLICY_LOB" Value
                //{
                //    strLOB_ID = Request.QueryString["POLICY_LOB"].ToString();  //Set Lob_id for javascript:ShowQuote() For Quotegenrate Page
                //}
                //if (intRuleverification == 1)
                //{
                //    imgQuote.Visible = true;
                //}
                //else
                //{
                //    imgQuote.Visible = false;
                    
                //}
			}
			else
			{
				trClaimRow.Attributes.Add("style","display:none");
				//int intResult=Cms.BusinessLayer.BlApplication.ClsGeneralInformation.CheckForApplicationStatus(intCustomerID,intApplicationID,intAppVersionID);
				//int intResultcheck=Cms.BusinessLayer.BlApplication.ClsGeneralInformation.CheckForConverted(intCustomerID,intApplicationID);
				
				string SystemId=GetSystemId();
                string strCarrier = cmsbase.CarrierSystemID;//System.Configuration.ConfigurationManager.AppSettings.Get("CarrierSystemID").ToString();

				/*if(intResult==2 || intResultcheck == 2)
				{
					imgSubmitApp.Visible=imgSubmitAnyway.Visible=imgVerifyApp.Visible=imgQuote.Visible= false;
				}
				else
				{
					if(SystemId.ToUpper()!=strCarrier.ToUpper())
					{
						imgSubmitAnyway.Visible=false;
						imgSubmitApp.Visible=imgVerifyApp.Visible=imgQuote.Visible= true;
					}
					else
					{
						imgSubmitApp.Visible=imgSubmitAnyway.Visible=imgVerifyApp.Visible=imgQuote.Visible= true;
					}
				}*/

				//When the policy has been created, don't show any icons
				//to be done later
				//When the policy has not been
				if(app_number.IndexOf("Unconfirmed") < 0)
				{
					intRuleverification =  Cms.BusinessLayer.BlApplication.ClsGeneralInformation.CheckForRuleVerificationStatus(intCustomerID,intApplicationID,intAppVersionID);
					if(intRuleverification == 1)
					{
						imgSubmitAnyway.Visible=imgSubmitApp.Visible=imgVerifyApp.Visible=imgQuote.Visible= true;
					}
					else
					{
						imgSubmitApp.Visible=imgQuote.Visible=false;
						imgSubmitAnyway.Visible=imgVerifyApp.Visible= true;
					}
				}
			}
			//	if(hidSubmitApp.Value=="Submit")
			//	{
			//		SubmitApplication();
			//		hidSubmitApp.Value="0";
			//	}
			

			//fetchAppInfo();

			if(hidAppEffectiveDate.Value !="" && hidAppEffectiveDate.Value != "0")
			{
				DataSet ds = Cms.BusinessLayer.BlCommon.ClsAgency.GetInactiveAgencyDataset(hidAppEffectiveDate.Value);
				string strAgnID = "";
				string StrIds = "";
				foreach(DataRow	row in ds.Tables[0].Rows)
				{
					strAgnID =row["AGENCY_ID"].ToString();
					StrIds = StrIds + strAgnID + "^";
				}
				string strTerminatedAGENCY_ID=StrIds;
				string[] strTerminatedAgency = strTerminatedAGENCY_ID.Split('^');
				for(int i=0; i< strTerminatedAgency.Length-1; i++)
				{
					if(hidAppAgency.Value == strTerminatedAgency[i].ToString())
					{
						hidIsAgencyTerminated.Value = "1";
					}
				}
			}
            EncryptQuerystr();
		}
		
		private void fetchAppInfo()
		{
			DataSet dsAppInfo = Cms.BusinessLayer.BlApplication.ClsGeneralInformation.FetchApplication(intCustomerID,intApplicationID,intAppVersionID);
			if(dsAppInfo != null && dsAppInfo.Tables[0].Rows.Count>0)
			{
				hidAppEffectiveDate.Value=dsAppInfo.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString();
				hidAppAgency.Value=dsAppInfo.Tables[0].Rows[0]["APP_AGENCY_ID"].ToString();
			}
		}
		//Function for check for submission of application with in 15 days of Effective Date if user is not Wolverine user
		private void CheckEffectiveDate()
		{






        }
        #region Commented Code
        //		private void SubmitApplication()
		//		{
		//			// Code commented by ashwani on 08 Feb 2006 As we h'v to implement submit functionality
		//			//<001> start
		//			/* 
		//			//Response.Write("Application submitted");
		//			try
		//			{				
		//				// Get the 3 keys				
		//				string gstrLobID= hidLOB.Value;
		//				int validApplication=0;
		//				string strCSSNo=((cmsbase)this.Page).GetColorScheme(),strCalled="ANYWAY";
		//				//gIntShowVerificationResult=1;
		//				Cms.BusinessLayer.BlApplication.ClsRatingAndUnderwritingRules objVerifyRules = new Cms.BusinessLayer.BlApplication.ClsRatingAndUnderwritingRules();
		//				string strShowMessage= objVerifyRules.SubmitAnywayAppVerify(intCustomerID,intApplicationID,intAppVersionID,gstrLobID,strCSSNo,out validApplication,strCalled);
		//				if(validApplication==1)
		//				{
		//					Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGenInfo = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
		//					int returnResult;
		//					gstrLobID= hidLOB.Value;
		//					returnResult = objGenInfo.CopyAppToPolicy(intCustomerID ,intApplicationID,intAppVersionID,int.Parse(((cmsbase)this.Page).GetUserId()),gstrLobID,strCalled);
		//					if(returnResult >0)	
		//					{
		//						//base.ScreenId = ScreenId;
		//						//lblMessage.Text= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("532");				
		//						string strSystemID = ((cmsbase)this.Page).GetSystemId();
		//						int userID =int.Parse(((cmsbase)this.Page).GetUserId());						
		//						
		//						
		//						//SetButtonsSecurityXml();
		//						//SetSecurityXML(strSystemID,userID);
		//						//lblMessage.Visible=true;
		//						GetPolicyDetails();
		//						string strRefreshtopMenuScript;
		//						imgSubmitApp.Visible=false;
		//						strRefreshtopMenuScript = "<script language='javascript'>"
		//							//+ "top.topframe.createPolicyTopMenu('" + ((cmsbase)this.Page).GetPolicyID() + "','" + ((cmsbase)this.Page).GetPolicyVersionID() + "','" + ((cmsbase)this.Page).GetCustomerID() + "','" + ((cmsbase)this.Page).GetLOBString() + "');"
		//							+ "top.topframe.enableMenu('1,2,3');" 		
		//							+ "top.topframe.main1.mousein = false;top.topframe.findMouseIn();</script>";
		//
		//						Page.RegisterStartupScript("REFRESHTOPMENU",strRefreshtopMenuScript);
		//						
		//					}
		//					else
		//					{
		//					
		//						//lblMessage.Text= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("533");				
		//						//this.btnConvertAppToPolicy.Attributes.Add("Style","display:inline");
		//						//this.btnQuote.Attributes.Add("Style","display:inline");
		//						//lblMessage.Visible=true;
		//					}
		//					
		//					//added by ashish on 12jan2006
		//					//					if (CheckCustomerMVRFetch()== true)
		//					//					{
		//					//						SetDriverViolations();
		//					//					}
		//
		//				}
		//				else					
		//				{
		//					//lblMessage.Text= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("533");
		//					//page
		//					Page.RegisterHiddenField("hidHTML", strShowMessage);					
		//					Page.RegisterStartupScript("ShowVerifiyDialog","<script>ShowDialogEx();</script>");
		//					//					Page.RegisterStartupScript("ShowVerifiyDialog","<script>document.getElementById('btnVerifyApplication').click();</script>");
		//
		//				}	
		//	
		//			}
		//			catch(Exception ex)
		//			{
		//				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
		//				//lblMessage.Text= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("553") + "\n" + ex.Message.ToString();		
		//				//lblMessage.Visible=true;
		//			}	*/
		//			//<001> End
		//			
		//			// Code added by ashwani on 08 Feb 2006
		//			//<002> Start 
		//			try
		//			{				
		//				
		//				string gstrLobID= hidLOB.Value;
		//				// get the validation range 
		//				Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGenInfo = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
		//				string strIsValid=objGenInfo.GetValidationRange(int.Parse(hidCustomer_Id.Value),int.Parse(hidApp_Id.Value),int.Parse(hidApp_Version_Id.Value));
		//				//string strSystemID = GetSystemId(); 
		//				string strCalled="SUBMIT";
		//				string  strCarrierSystemID = System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID").ToString();
		//				//if not the Wolverine User
		//			/*	if((strSystemID.ToUpper()!=strCarrierSystemID.ToUpper()) && strIsValid=="1")
		//				{
		//					//Application can be submitted only with in 15 days of Effective Date
		//					lblMessage.Text=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("555");				
		//					lblMessage.Visible=true;
		//				}
		//				else */
		////				{
		//					int validApplication=0;
		//					string strCSSNo=((cmsbase)this.Page).GetColorScheme();
		//					//gIntShowVerificationResult=1;
		//					Cms.BusinessLayer.BlApplication.ClsRatingAndUnderwritingRules objVerifyRules = new Cms.BusinessLayer.BlApplication.ClsRatingAndUnderwritingRules();
		//					string strShowHTML = objVerifyRules.VerifyApplication(int.Parse(hidCustomer_Id.Value),int.Parse(hidApp_Id.Value),int.Parse(hidApp_Version_Id.Value),gstrLobID,strCSSNo,out validApplication);
		//				if(validApplication==1)
		//				{
		//					//ClsGeneralInformation objGenInfo = new ClsGeneralInformation();
		//					int returnResult;
		//					returnResult = objGenInfo.CopyAppToPolicy(int.Parse(hidCustomer_Id.Value),int.Parse(hidApp_Id.Value),int.Parse(hidApp_Version_Id.Value) ,int.Parse(((cmsbase)this.Page).GetUserId()),gstrLobID,strCalled);
		//					//						if(returnResult >0)
		//					//						{
		//					//							//Assigning the screen id again for updating security xml 
		//					//							base.ScreenId = ScreenId;
		//					//							//lblMessage.Text= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("532");				
		//					//							string strSystem_ID = GetSystemId();
		//					//							int userID =int.Parse(GetUserId());
		//					//							SetButtonsSecurityXml();
		//					//							SetSecurityXML(strSystem_ID,userID);
		//					//							//Setting the security xml and buttons type of cmsbutton
		//					//							//Setting the details of policy in session and hidden variables
		//					//							GetPolicyDetails();
		//					//							//application converted to policy successfully
		//					//							//lblMessage.Text= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("532") + "<br>Policy Number is " + hidPolicyNumber.Value;
		//					//							lblMessage.Text= "Policy Number " + hidPolicyNumber.Value + " has been created successfully";
		//					//							lblMessage.Visible=true;
		//					//						}
		//					//						else
		//					//						{
		//					//							lblMessage.Text= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("533");	
		//					//							lblMessage.Visible=true;
		//					//						}
		//					//					}
		//					//					else
		//					//						lblMessage.Text= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("533");				
		//					//					if(validApplication==1)
		//					//					{
		//					//						this.btnConvertAppToPolicy.Attributes.Add("Style","display:inline");
		//					//						this.btnQuote.Attributes.Add("Style","display:inline");
		//					//					}				
		//					//					lblMessage.Visible=true;
		//				}
		//				else
		//				{
		//					Page.RegisterHiddenField("hidHTML", strShowHTML);					
		//					Page.RegisterStartupScript("ShowVerifiyDialog","<script>ShowDialogEx();</script>");
		//				}
		//
		//			}
		//			catch(Exception ex)
		//			{
		//				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
		////				lblMessage.Text= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("553") + "\n" + ex.Message.ToString();		
		////				lblMessage.Visible=true;
		//			}
		//			//<002> End
        //		}
        #endregion
        private void GetPolicyDetails()
		{
			Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGeneralInformation = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
			DataSet ds = objGeneralInformation.GetPolicyDetails(intCustomerID,intApplicationID,intAppVersionID);
				
			
			if (ds.Tables[0].Rows.Count > 0)
			{
				if(ds.Tables[0].Rows[0]["POLICY_ID"]!=null && ds.Tables[0].Rows[0]["POLICY_ID"].ToString()!="")
					intPolicyId = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				if(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"]!=null && ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString()!="")
					intPolicyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());
				//hidPolicyId.Value = ds.Tables[0].Rows[0]["POLICY_ID"].ToString();

				//Setting the polic details in session
				((cmsbase)this.Page).SetPolicyID(hidPolicyId.Value);
				//((cmsbase)this.Page).SetPolicyID(hidPolicyId.Value);
				//((cmsbase)this.Page).SetPolicyVersionID(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());
				((cmsbase)this.Page).SetPolicyVersionID(intPolicyVersionId.ToString());
				((cmsbase)this.Page).SetLOBID(hidLOB.Value);				
				//(cmsbase(this.Page)).SetLOBID(FetchValueFromXML("LOB_ID",hidOldData.Value));
			}

			objGeneralInformation = null;

		}
		private string FetchValueFromXML(string nodeName, string XMLString)
		{
			try 
			{
				string strRetval="";
				XmlDocument doc = new XmlDocument();
				doc.LoadXml(XMLString);
				XmlNodeList nodList = doc.GetElementsByTagName(nodeName);
				if (nodList.Count >0)
				{
					strRetval=nodList.Item(0).InnerText;
				}
				return strRetval;

			
			}
			catch(Exception exc)
			{
				throw(exc);
			}
			finally
			{
			
			}
		
		
		}

		//Ravindra(09-11-2009) 
		private void GetQuoteStatus(string showQuote)
		{
			if (showQuote.Trim().Equals("1"))
			{
				imgQuote.Attributes.Add("Style","display:none");
				imgSubmitApp.Attributes.Add("Style","display:inline");
                hidReturnPolicyStatus.Value = "1";
			}
			else
			{
				imgQuote.Attributes.Add("Style","display:none");
				imgSubmitApp.Attributes.Add("Style","display:none");
                hidReturnPolicyStatus.Value = "0";
			}

		}

		
//		private void GetQuoteStatus()
//		{
//			string appXML = Cms.BusinessLayer.BlApplication.ClsGeneralInformation.FetchApplicationXML(intCustomerID,intApplicationID,intAppVersionID);
//			string showQuote = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("SHOW_QUOTE",appXML);
//			
//			if (showQuote.Trim().Equals("1"))
//			{
//				imgQuote.Attributes.Add("Style","display:inline");
//				imgSubmitApp.Attributes.Add("Style","display:inline");				
//			}
//			else
//			{
//				imgQuote.Attributes.Add("Style","display:none");
//				imgSubmitApp.Attributes.Add("Style","display:none");	
//			}
//			
//			//int validApplication=0;
//			//string strRulesHTML = base.strHTML(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value),out valid,out strRulesStatus);
//			//string strRulesHTML = Cms.BusinessLayer.BlProcess.clsprocess.strHTMLMandatory(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value),out valid,out strRulesStatus);
//
//			//Modified Manoj Rathore/Mohit Agarwal 16-Mar-2007
//			/*Cms.BusinessLayer.BlApplication.ClsRatingAndUnderwritingRules objRules = new Cms.BusinessLayer.BlApplication.ClsRatingAndUnderwritingRules();
//			string strCSSNo =((cmsbase)this.Page).GetColorScheme();
//			string strRulesHTML=objRules.VerifyApplication(intCustomerID,intApplicationID,intAppVersionID,hidLOB.Value,strCSSNo,out validApplication);
//			
//			if(validApplication != 0)
//			{
//				imgQuote.Attributes.Add("Style","display:inline");
//				imgSubmitApp.Attributes.Add("Style","display:inline");				
//			}
//			else
//			{
//				imgQuote.Attributes.Add("Style","display:none");
//				imgSubmitApp.Attributes.Add("Style","display:none");	
//			}*/
//
//		}
		

		public void CallPageLoad()
		{
			//Making the client business client object for retreiving the customer info
			Cms.BusinessLayer.BlClient.ClsCustomer objCustomer = new Cms.BusinessLayer.BlClient.ClsCustomer();
			
			string  strCustomerXML = GetCustomerInfo(CustomerID);
			//Response.Write(strCustomerXML);
			//Response.End();
			colorScheme =((cmsbase)this.Page).GetColorScheme();

			ShowCustomerInfo(strCustomerXML);
			
			/*	Commented by Charles on 16-Mar-10 for Policy Page Implementation		
			if (ShowHeaderBand.ToUpper() == "APPLICATION")
				InitiateApplicationHeader();
			else if (ShowHeaderBand.ToUpper() == "POLICY")
			{*/
				ShowPolicyDetails();
                imgSubmitApp.Visible = imgSubmitAnyway.Visible = false;
                
				//imgSubmitApp.Visible=imgSubmitAnyway.Visible=imgQuote.Visible= false;
				CustomerDetail2.ImageUrl ="~/cmsweb/Images" + colorScheme	 + "/Customer_Ass.gif";
			//}
		}

		/// <summary>
		/// Shows the details of policy on client top control
		/// </summary>
		private void ShowPolicyDetails()
		{
			//Making the client business client object for retreiving the policy
			Cms.BusinessLayer.BlApplication.ClsGeneralInformation objApplication = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
             intShowQuoteStatus = 0;
			//DataSet ds = objApplication.GetPolicyDataSet(CustomerID, PolicyID, PolicyVersionID);
            int langId= int.Parse(((cmsbase)this.Page).GetLanguageID());
            DataSet ds = objApplication.GetClaimPolicyDataSet(CustomerID, PolicyID, PolicyVersionID, langId);
			
			if (ds.Tables.Count == 0)
				return;


			if (ds.Tables[0].Rows.Count > 0)
			{
				System.Data.DataRow dr = ds.Tables[0].Rows[0];

				hidLOB.Value=dr["POLICY_LOB"].ToString(); 
				lblSCustomerName.Text = dr["CUSTOMERNAME"].ToString();                
				lblSCustomerAddress.Text = dr["ADDRESS"].ToString();                				
				lblSCustomerType.Text = dr["CUSTOMER_TYPE_DESC"].ToString();
				//lblSCustomerPhone.Text = dr["CUSTOMER_BUSINESS_PHONE"].ToString();
				lblSCustomerPhone.Text = dr["CUSTOMER_HOME_PHONE"].ToString();
                lblSAgencyPhone.Text = dr["AGENCY_PHONE"].ToString();
				lblSAppNo.Text =dr["POLICY"].ToString();
                if (dr["ENDOREMENT_DETAILS"].ToString() != null && dr["ENDOREMENT_DETAILS"].ToString() != "")
                {
                    lblEND_DETAILS.Text = dr["ENDOREMENT_DETAILS"].ToString();
                    lblEND.Visible = true;
                }
				//				lblSAppNo1.Text      =dr["POLICY"].ToString();

				lblSAppVersion.Text = dr["POLICY_DISP_VERSION"].ToString() ;
                lblSPolCurrency.Text = dr["Policy_Currency"].ToString();

				if(dr["CUSTOMER_ATTENTION_NOTE"].ToString()!="0")
				{
					att_note="1";
					Image1.ImageUrl="~/cmsweb/images/att-ecs.gif"; 
				}
				else
				{
					Image1.ImageUrl="~/cmsweb/images/att-ecs-grey.gif";
				}
				#region Display Claims against a policy	
				
				//Modified by Asfa(09-July-2008) - iTrack #4459
				string SystemId=GetSystemId();
				sbClaimLink = objApplication.GetClaimNumberAsLinkForPolicy(ds, SystemId);	
				if(sbClaimLink.Length==0)				
					trClaimRow.Attributes.Add("style","display:none");
				else
				{
					trClaimRow.Attributes.Add("style","display:inline");
					sbClaimLink.Remove(sbClaimLink.Length-2,2);
				}

                intShowQuoteStatus = Convert.ToInt32(dr["SHOW_QUOTE"].ToString());
                POLICY_STATUS = dr["POLICY_STATUS"].ToString();
				#endregion
			}

			
			lblSCustomerName.Visible=true;
			lblSCustomerAddress.Visible=true;
			lblSCustomerType.Visible=true;
			lblSCustomerPhone.Visible=true;
            lblSAgencyPhone.Visible = true;
			lblSAppNo.Visible=true;
			//lblSAppNo1.Visible=true;

			lblSAppVersion.Visible=true;
			
			//CustomerDetail2.ImageUrl ="~/cmsweb/images" + colorScheme  + "/Customer_Ass.gif";
            imgVerifyApp.ImageUrl = "~/cmsweb/Images" + colorScheme + "/Rule_ver.gif";
			CustomerDetailQ2.ImageUrl ="~/cmsweb/images" + colorScheme  + "/Customer_Ass.gif";
            imgQuote.ImageUrl = "~/cmsweb/Images/quote.gif";
            //return;
            GetQuoteStatus(intShowQuoteStatus.ToString());
            imgFetchUndisc.Visible = false;
		}

		private void InitiateApplicationHeader()
		{
			//Making the client business client object for retreiving the customer info
			Cms.BusinessLayer.BlApplication.clsapplication objApplication = new Cms.BusinessLayer.BlApplication.clsapplication();
			
			string  strApplicationXML = GetApplicationInfo(CustomerID,ApplicationID,AppVersionID);
			ShowApplicationInfo(strApplicationXML);
			string showQuote = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("SHOW_QUOTE",strApplicationXML);
			GetQuoteStatus(showQuote);
			hidAppEffectiveDate.Value= Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("APP_EFFECTIVE_DATE",strApplicationXML);
			hidAppAgency.Value= Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("APP_AGENCY_ID",strApplicationXML);
		}

		private void GetLabelNames()
		{
			string strLanguage;
			if (System.Configuration.ConfigurationSettings.AppSettings["Language"] == "ENG")
			{
				strLanguage = "";				
			}
			else
			{
				strLanguage = "." + System.Configuration.ConfigurationSettings.AppSettings["Language"];
				
			}

			string strFileName = "BritAmazon.BritAmazonWeb.Include.ClientTop1";

			strFileName = strFileName + strLanguage;
			ResourceManager aobjResMang = new ResourceManager(strFileName,Assembly.GetExecutingAssembly());

			strlblClientInfo= aobjResMang.GetString("lblClientInfo");
			strlblAKADBAName= aobjResMang.GetString("lblAKADBAName");
			strlblLocation= aobjResMang.GetString("lblLocation");				
			lblName.Text = aobjResMang.GetString("lblName");
			lblType.Text = aobjResMang.GetString("lblType");
			lblAddress.Text = aobjResMang.GetString("lblAddress");
			lblPhone.Text =aobjResMang.GetString("lblPhone");

			
			lblNameQP.Text = aobjResMang.GetString("lblName");	
			lblAddressQP.Text = aobjResMang.GetString("lblAddress");
			lblAddressQuote.Text=aobjResMang.GetString("lblAddress");

			//code added for claims...........................
			lblClaimant.Text=aobjResMang.GetString("lblClaimant");				
			lblInsuredName.Text=aobjResMang.GetString("lblInsuredName");
			lblClaimNo.Text=aobjResMang.GetString("lblClaimNo");
			lblDateofLoss.Text=aobjResMang.GetString("lblDateofLoss");				
			lblPolVersion.Text=aobjResMang.GetString("lblPolVersion");
			lblInsuredClaimant.Text=aobjResMang.GetString("lblInsuredClaimant");
			strlblAttentionNotes=aobjResMang.GetString("lblAttentionNotes");

			
			//Only when claim is available

			lblPolicyEfDate.Text=aobjResMang.GetString("lblPolicyEfDate");
			lblClaimClass.Text=aobjResMang.GetString("lblClaimClass");
			lblLossType.Text=aobjResMang.GetString("lblLossType");
			lblLosSubType.Text=aobjResMang.GetString("lblLosSubType");
			lblDateofLoss.Text=aobjResMang.GetString("lblDateofLoss");
			lblAllocatedAdjuster.Text=aobjResMang.GetString("lblAllocatedAdjuster");
			lblClaimDesc.Text=aobjResMang.GetString("lblClaimDesc");
            
		}

        /// <summary>
        /// Set Client Top Captions
        /// </summary>
        /// <param name="strCalledFrom">Header String</param>
        private void SetCaptions(string strCalledFrom)
        {
            try
            {
                (new cmsbase()).SetCultureThread((new cmsbase()).GetLanguageCode());
                ResourceManager objResourceMgr = new ResourceManager("Cms.CmsWeb.WebControls.ClientTop", Assembly.GetExecutingAssembly());

                switch (strCalledFrom)
                {
                    case "Client":
                        lblName.Text = objResourceMgr.GetString("lblName");
                        lblType.Text = objResourceMgr.GetString("lblType");
                        lblAddress.Text = objResourceMgr.GetString("lblAddress");
                        lblPhone.Text = objResourceMgr.GetString("lblPhone");
                        lblStatus.Text = objResourceMgr.GetString("lblStatus");
                        lblTitle.Text = objResourceMgr.GetString("lblTitle");
                        lblCustAgencyPhone.Text = objResourceMgr.GetString("lblCustAgencyPhone");
       
                        break;
                    case "Application":
                        lblCustomerName.Text = objResourceMgr.GetString("lblCustomerName");
                        lblCustomerAddress.Text = objResourceMgr.GetString("lblCustomerAddress");
                        lblCustomerType.Text = objResourceMgr.GetString("lblCustomerType");
                        lblCustomerPhone.Text = objResourceMgr.GetString("lblCustomerPhone");
                        lblAgencyPhone.Text = objResourceMgr.GetString("lblAgencyPhone");
                        lblAppNo.Text = objResourceMgr.GetString("lblAppNo");
                        lblAppVersion.Text = objResourceMgr.GetString("lblAppVersion");
                        lblPolCurrency.Text = objResourceMgr.GetString("lblPolCurrency");
                        lblEND.Text = objResourceMgr.GetString("lblEND");
                        break;
                    case "Policy":
                        lblCustomerName.Text = objResourceMgr.GetString("lblCustomerName");
                        lblCustomerAddress.Text = objResourceMgr.GetString("lblCustomerAddress");
                        lblCustomerType.Text = objResourceMgr.GetString("lblCustomerType");
                        lblCustomerPhone.Text = objResourceMgr.GetString("lblCustomerPhone");
                        lblAgencyPhone.Text = objResourceMgr.GetString("lblAgencyPhone");  
                        lblAppNo.Text = objResourceMgr.GetString("lblAppNo");
                        lblAppVersion.Text = objResourceMgr.GetString("lblAppVersion");
                        lblPolCurrency.Text = objResourceMgr.GetString("lblPolCurrency");
                        lblEND.Text = objResourceMgr.GetString("lblEND");
                        lblClaimText.Text = objResourceMgr.GetString("lblClaimText");
                        break;
                }
            }
            catch(Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
        }

		private void ShowCustomerDetails()
		{
			PanelClient.Visible = true;

            SetCaptions("Client");
            /*
			lblName.Text = "Name";
			lblType.Text = "Customer Type";
			lblAddress.Text = "Address";
			lblPhone.Text = "Home Phone";
			lblStatus.Text ="Status";
			lblTitle.Text  = "Title";
			*/		

			lblName.Visible = true;
			lblType.Visible = true;
			lblAddress.Visible = true;
			lblPhone.Visible = true;
			lblStatus.Visible= true;
			lblTitle.Visible= true;
            lblCustAgencyPhone.Visible = true;
			lblFullName.Visible = true;
			lblClientType.Visible = true;
			lblClientAddress.Visible = true;
			lblClientPhone.Visible = true;
			lblClientStatus.Visible = true;
			lblTitle.Visible= true;


		}

		private void ShowPolicyLabels()
		{
			pnlApplication.Visible = true;

			lblCustomerName.Visible=true;
			lblCustomerAddress.Visible=true;
			lblCustomerType.Visible=true;
			lblCustomerPhone.Visible=true;
            lblAgencyPhone.Visible = true;
			lblAppNo.Visible=true;
			lblAppVersion.Visible=true;
            lblPolCurrency.Visible = true;
            SetCaptions("Policy");
            /*
			lblCustomerName.Text="Customer Name";
			lblCustomerAddress.Text="Address";
			lblCustomerType.Text="Customer Type";
			lblCustomerPhone.Text="Phone";

			lblAppNo.Text="Policy No.";
			lblAppVersion.Text="Policy Version";
             */
			
		}

		private void ShowApplicationDetails()
		{
			pnlApplication.Visible = true;

			lblCustomerName.Visible=true;
			lblCustomerAddress.Visible=true;
			lblCustomerType.Visible=true;
			lblCustomerPhone.Visible=true;
            lblAgencyPhone.Visible = true;
			lblAppNo.Visible=true;
			lblAppVersion.Visible=true;
			imgSubmitApp.Visible=imgSubmitAnyway.Visible=imgVerifyApp.Visible=imgQuote.Visible= true;
            SetCaptions("Application");
            //lblCustomerName.Text="Customer Name";
            //lblCustomerAddress.Text="Address";
            //lblCustomerType.Text="Customer Type";
            //lblCustomerPhone.Text="Phone";			
            //lblAppNo.Text="App No.";
            //lblAppVersion.Text="App Version";			
		}
		

		private void ShowClaimDetails()
		{
			pnlClaims.Visible=true;
			PanelClient.Visible = false;
			pnlPolicy.Visible = false;
			PanelQuote.Visible = false;
			lblClaimant.Visible=true;
			lblInsuredName.Visible=true;
			lblClaimNo.Visible=true;
			lblDateofLoss.Visible=true;					
			lblPolVersion.Visible=true;
			lblClaimantVal.Visible=true;
			lblInsuredNameVal.Visible=true;
			lblClaimNoVal.Visible=true;
			lblDateofLossVal.Visible=true;					
			lblPolVersionVal.Visible=true;
			lblInsuredClaimant.Visible=true;
			lblInsureClaimantVal.Visible=true;
			lblClaimant.Visible=true;
			lblInsuredName.Visible=true;
			lblPolVersion.Visible=true;
			lblPolicyEfDate.Visible=true;
			lblClaimNo.Visible=true;
			lblClaimClass.Visible=true;
			lblLossType.Visible=true;
			lblLosSubType.Visible=true;
			lblInsuredClaimant.Visible=true;
			lblDateofLoss.Visible=true;
			CustomerDetailQ.ImageUrl ="~/cmsweb/Images" + colorScheme  + "/Customer_Ass.gif";

		}

		private string GetCustomerInfo(int intCustomerID)
		{
			//Making the object of clsClient for retreiving the information
			Cms.BusinessLayer.BlClient.ClsCustomer objClient;
            int langId = int.Parse(((cmsbase)this.Page).GetLanguageID());
			objClient = new Cms.BusinessLayer.BlClient.ClsCustomer();
			try
			{
                return objClient.FillCustomerDetailsForClientTop(intCustomerID, langId);			
			}
			catch (Exception objEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objEx);
				throw(objEx);
			}
			finally
			{
				objClient.Dispose();
			}
		}

		private string GetApplicationInfo(int intCustomerID,int appID,int appVersionID)
		{
			//Making the object of clsClient for retreiving the information
			Cms.BusinessLayer.BlApplication.clsapplication objApplication;
			objApplication= new Cms.BusinessLayer.BlApplication.clsapplication();
			try
			{
				return objApplication.FillApplicationDetails(intCustomerID,appID,appVersionID);			
			}
			catch (Exception objEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objEx);
				throw(objEx);
			}
			finally
			{
				objApplication.Dispose();
			}
		}

		private void ShowCustomerInfo(string strCustomerXML)
		{
			System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
			doc.LoadXml(strCustomerXML);
			
			System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader(new System.IO.StringReader(strCustomerXML));

			DataSet ds = new DataSet();
			ds.ReadXml(reader);
			
			if (ds.Tables.Count == 0)
				return;

			if (ds.Tables[0].Rows.Count > 0)
			{
				System.Data.DataRow dr = ds.Tables[0].Rows[0];

				//Retreiving the full name
				//Making string builder for retreving the full customer name
				System.Text.StringBuilder objFullName = new System.Text.StringBuilder();
				
				objFullName.Append(dr["CUSTOMER_FIRST_NAME"] + " ");

				if (! dr["CUSTOMER_MIDDLE_NAME"].ToString().Equals(""))
				{
					objFullName.Append(dr["CUSTOMER_MIDDLE_NAME"] + " ");
				}

				if (! dr["CUSTOMER_LAST_NAME"].Equals(""))
				{
					objFullName.Append(dr["CUSTOMER_LAST_NAME"] + " ");
				}
				//Added 'Suffix' for Itrack Issue 5485 on 17 April 2009
				if (! dr["CUSTOMER_SUFFIX"].Equals(""))
				{
					objFullName.Append(dr["CUSTOMER_SUFFIX"] + " ");
				}
				lblFullName.Text = objFullName.ToString().Trim();
				objFullName = null;


				//Retreiving the full address
				System.Text.StringBuilder objFullAddress = new System.Text.StringBuilder();
				if (!dr["CUSTOMER_ADDRESS1"].ToString().Equals(""))
				{
					objFullAddress.Append(dr["CUSTOMER_ADDRESS1"] + ", ");
				}
				if (!dr["CUSTOMER_ADDRESS2"].ToString().Equals(""))
				{
					objFullAddress.Append(dr["CUSTOMER_ADDRESS2"] + ", ");
				}
				
				if (!dr["CUSTOMER_CITY"].ToString().Equals(""))
				{
					objFullAddress.Append(dr["CUSTOMER_CITY"] + " - ");
				}

				//Added by Gaurav
				if (!dr["CUSTOMER_STATE_NAME"].ToString().Equals(""))
				{
					objFullAddress.Append(dr["CUSTOMER_STATE_NAME"] + "/");
				}

				if (!dr["CUSTOMER_COUNTRY_NAME"].ToString().Equals(""))
				{
                    objFullAddress.Append(dr["CUSTOMER_STATE_CODE"] + " - ");
				}

				if (!dr["CUSTOMER_ZIP"].ToString().Equals(""))
				{
					objFullAddress.Append(dr["CUSTOMER_ZIP"] + " ");
				}
				
				lblClientAddress.Text = objFullAddress.ToString().Trim();
				objFullAddress = null;
				
				if(dr["IS_ACTIVE"].ToString().Equals("Y"))
				{
                    lblClientStatus.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1328");//"Active";
				}
				if(dr["IS_ACTIVE"].ToString().Equals("N"))
				{
                    lblClientStatus.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1329");//"Inactive";
				}
				if(dr["CUSTOMER_ATTENTION_NOTE"].ToString()!="0")
				{
					att_note="1";
					AspImageNote.ImageUrl="~/cmsweb/images/att-ecs.gif"; 
				}
				else
				{
					AspImageNote.ImageUrl="~/cmsweb/images/att-ecs-grey.gif";
				}


				//Adde By Particular Image from Particular Scheme
                 
				CustomerDetail.ImageUrl="~/cmsweb/Images" + colorScheme  + "/Customer_Ass.gif";

				




				//End =====================

				//Checking the customer type, whether persocal or commercial
				if(dr["CUSTOMER_TYPE"].ToString().Equals("11110"))
				{
                    lblPhone.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1320");//"Home Phone";
					lblClientPhone.Text = dr["CUSTOMER_HOME_PHONE"].ToString();		//IF personal,home phone should display
				}
				else
				{
                    lblPhone.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1321");//"Business Phone";
					lblClientPhone.Text = dr["CUSTOMER_BUSINESS_PHONE"].ToString();	//IF Commercial,home phone should display
				}

				lblClientType.Text = dr["CUSTOMER_TYPE_DESC"].ToString();
				lblClientTitle.Text = dr["PREFIX_DESC"].ToString();
                lblSCustAgencyPhone.Text = dr["AGENCY_PHONE"].ToString();
			}

			lblFullName.Visible = true;
			lblClientAddress.Visible = true;
			lblClientType.Visible = true;
			lblClientPhone.Visible = true;
			lblClientStatus.Visible=true;
			lblClientTitle.Visible=true;
			CustomerDetail.Visible =true;

			
			//return;
		}

                                      
		private void ShowApplicationInfo(string strApplicationXML)
		{
			System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
			doc.LoadXml(strApplicationXML);
			
			System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader(new System.IO.StringReader(strApplicationXML));

			DataSet ds = new DataSet();
			ds.ReadXml(reader);
			
			if (ds.Tables.Count == 0)
				return;

			if (ds.Tables[0].Rows.Count > 0)
			{
				System.Data.DataRow dr = ds.Tables[0].Rows[0];

				hidLOB.Value=dr["APP_LOB"].ToString(); 
				lblSCustomerName.Text = dr["CUSTOMERNAME"].ToString();                
				lblSCustomerAddress.Text = dr["ADDRESS"].ToString();                				
				lblSCustomerType.Text = dr["CUSTOMER_TYPE_DESC"].ToString();
				//lblSCustomerPhone.Text = dr["CUSTOMER_BUSINESS_PHONE"].ToString();
				if(dr["CUSTOMER_TYPE"].ToString().Equals("11110"))
				{
                    lblPhone.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1320");//"Home Phone";
					lblSCustomerPhone.Text = dr["CUSTOMER_HOME_PHONE"].ToString();		//IF personal,home phone should display
				}
				else
				{
                    lblPhone.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1321");//"Business Phone";
					lblSCustomerPhone.Text = dr["CUSTOMER_BUSINESS_PHONE"].ToString();	//IF Commercial,home phone should display
				}
                lblSAgencyPhone.Text = dr["CUSTOMER_BUSINESS_PHONE"].ToString();
				lblSAppNo.Text      =dr["APPLICATION"].ToString();
				lblSAppNo1.Text      =dr["APPLICATION"].ToString();
				lblSAppVersion.Text = dr["APP_VERSION"].ToString() ;
				imgVerifyApp.ImageUrl    =  "~/cmsweb/Images" + colorScheme  + "/Rule_ver.gif";
				imgFetchUndisc.ImageUrl  =  "~/cmsweb/Images" + colorScheme  + "/calender.gif";

				
				if(hidLOB.Value != "2" && hidLOB.Value != "3" && hidLOB.Value != "1" && hidLOB.Value != "4")
					imgFetchUndisc.Visible = false;

				imgSubmitAnyway.ImageUrl    =  "~/cmsweb/Images/Submit_anyway.gif";
				imgQuote.ImageUrl    =  "~/cmsweb/Images/quote.gif";
				imgSubmitApp.ImageUrl    =  "~/cmsweb/Images" + colorScheme  + "/submit_app.gif";				
				if(dr["CUSTOMER_ATTENTION_NOTE"].ToString()!="0")
				{
					att_note="1";
					Image1.ImageUrl="~/cmsweb/images/att-ecs.gif"; 
				}
				else
				{
					Image1.ImageUrl="~/cmsweb/images/att-ecs-grey.gif";
				}

				app_number = dr["APPLICATION"].ToString();
				//Added by Mohit Agarwal 30-Oct-08 ITrack 4956
				if(dr["APPLICATION"].ToString().IndexOf("Unconfirmed") >= 0)
					imgFetchUndisc.Visible = imgSubmitApp.Visible=imgSubmitAnyway.Visible=imgVerifyApp.Visible=imgQuote.Visible= false;			
			}

			CustomerDetailQ2.ImageUrl ="~/cmsweb/Images" + colorScheme  + "/Customer_Ass.gif";
			lblSCustomerName.Visible=true;
			lblSCustomerAddress.Visible=true;
			lblSCustomerType.Visible=true;
			lblSCustomerPhone.Visible=true;
            lblSAgencyPhone.Visible = true;
			lblSAppNo.Visible=true;
			lblSAppNo1.Visible=true;
			lblSAppVersion.Visible=true;
			

			//return;
		}

        private void EncryptQuerystr()
        {
            string Querystr = "";
            string strCALLEDFOR = "";
          //  string ClientIdForCustomerId;
           // string ClientId;
           // string Client;
            try
            {

                if (POLICY_STATUS.ToUpper() == "APPLICATION" || POLICY_STATUS.ToUpper() == "SUSPENDED" || POLICY_STATUS.ToUpper() == "")
                {
                    strCALLEDFOR = "APPLICATION";
                }
                else 
                {
                    strCALLEDFOR = "POLICY";
                }
                Querystr = "customer_id=" + intCustomerID + "&Calledfor="+strCALLEDFOR;
                EncryptCustomeridQstr = "/Cms/client/aspx/CustomerManagerIndex.aspx" + QueryStringModule.EncriptQueryString(Querystr);

                //Querystr = "ClientID=" + intCustomerID;
                //strClientID = QueryStringModule.EncriptQueryString(Querystr);
                
            }
            catch
            {

            }
        
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
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}


