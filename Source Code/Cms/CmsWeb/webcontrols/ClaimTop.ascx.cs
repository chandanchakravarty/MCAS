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
    
	
	using System.Globalization;
	//using BritAmazon.BritAmazonWeb.BaseObjects;
	
	/// <summary>
	///		Summary description for ClaimTop1.
	/// </summary>
	public abstract class ClaimTop : System.Web.UI.UserControl
	{
		protected string strHeadClientFullName="";
		
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
		protected int intPolicyId;
		protected int intPolicyVersionId;
		protected int intClaimId;
		protected int intLobId;
		protected string strClaimNumber="";


		// change
		protected System.Web.UI.WebControls.Label lblName, lblType, lblClientType, lblQuoteStatus, lblQuoteStatusValue;
		protected System.Web.UI.WebControls.Label lblNameQP,lblFullNameQP,lblAddressQP,lblClientAddressQP;
		protected System.Web.UI.WebControls.Label lblFullName, lblTitle, lblClientTitle;
		protected System.Web.UI.WebControls.Label lblAddress, lblClientAddress, lblPhone, lblClientPhone;
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
		protected System.Web.UI.WebControls.Label lblSCustomerName1;
		protected System.Web.UI.WebControls.Label lblSCustomerName2;
		protected System.Web.UI.WebControls.Label lblCustomerType;
		protected System.Web.UI.WebControls.Label lblSCustomerType;
		protected System.Web.UI.WebControls.Label lblCustomerAddress;
		protected System.Web.UI.WebControls.Label lblSCustomerAddress;
		protected System.Web.UI.WebControls.Label lblCustomerPhone;
		protected System.Web.UI.WebControls.Label lblSCustomerPhone;
        protected System.Web.UI.WebControls.Label lblAgencyPhone;
        protected System.Web.UI.WebControls.Label lblSAgencyPhone;
		protected System.Web.UI.WebControls.Label lblAppNo;
		protected System.Web.UI.WebControls.Label lblSAppNo1;
		protected System.Web.UI.WebControls.Label lblSAppNo2;
		protected System.Web.UI.WebControls.Label lblAppVersion;
		protected System.Web.UI.WebControls.Label lblSAppVersion;
		protected System.Web.UI.WebControls.Label lblStatus;
		protected System.Web.UI.WebControls.Label lblClientStatus;
		protected System.Web.UI.WebControls.Image Image1;
		private string header="";
		protected System.Web.UI.WebControls.Image CustomerDetailQ2,CustomerDetail,Image2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSubmitApp;
		protected System.Web.UI.WebControls.Image imgVerifyApp;
		protected System.Web.UI.WebControls.Image imgSubmitApp;		
		public string att_note="";
		protected System.Web.UI.WebControls.Image CustomerDetailQ;
		protected System.Web.UI.WebControls.Image CustomerDetail2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomer_Id;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidApp_Id;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidApp_Version_Id;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidReturnPolicyStatus;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCltNewBusinessProcess;
		protected string colorScheme;
		protected System.Web.UI.WebControls.Label lblSClaimNumber1;		
		protected System.Web.UI.WebControls.Label lblSClaimNumber2;		
		protected System.Web.UI.WebControls.Label lblClaimNumber;

		public int CustomerID
		{
			get {return intCustomerID;}
			set {intCustomerID = value;}
		}


		public enum ShowHeader
		{
			Claim
		}

		public string ShowHeaderBand
		{
			get{return header;}
			set
			{
				header=value;
				switch (header)
				{				
					case "Claim":
						ShowClaimLabels();
						break;
				}
			}

		}

		
		
		public int PolicyID
		{
			get{return intPolicyId;}
			set{intPolicyId = value;}
		}
		public int ClaimID
		{
			get{return intClaimId;}
			set{intClaimId = value;}
		}
		public int LobID
		{
			get{return intLobId;}
			set{intLobId = value;}
		}
		public string ClaimNumber
		{
			get{return strClaimNumber;}
			set{strClaimNumber = value;}
		}
		public int PolicyVersionID
		{
			get{return intPolicyVersionId;}
			set{intPolicyVersionId = value;}
		}

        System.Resources.ResourceManager objResourceMgr;

		private void Page_Load(object sender, System.EventArgs e)
        {
           
            objResourceMgr = new System.Resources.ResourceManager("Cms.cmsweb.webcontrols.ClaimTop", System.Reflection.Assembly.GetExecutingAssembly());
			CallPageLoad();
            CustomerDetailQ2.ToolTip = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1930");
        
            
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
		

		public void CallPageLoad()
		{
			//Making the client business client object for retreiving the customer info
			Cms.BusinessLayer.BlClient.ClsCustomer objCustomer = new Cms.BusinessLayer.BlClient.ClsCustomer();

			colorScheme =((cmsbase)this.Page).GetColorScheme();
			string  strCustomerXML="";
			if(CustomerID!=0)			
			{
				strCustomerXML = GetCustomerInfo(CustomerID);								
				ShowCustomerInfo(strCustomerXML);
			}
			else //When we do not have customer data, show NA for customer labels
			{
				lblFullName.Text = "N.A";
				lblClientAddress.Text = "N.A";
				lblClientType.Text = "N.A";
				lblClientPhone.Text = "N.A";
                lblClientStatus.Text = "N.A";
				lblClientTitle.Text = "N.A";

				//CustomerDetail.Text = "N.A";				
			}
			//ShowCustomerLabels(true);
			ShowClaimDetails();
            SetCaptions();					
			
		}

//		private void ShowCustomerLabels(bool flag)
//		{
//			lblFullName.Visible = flag;
//			lblClientAddress.Visible = flag;
//			lblClientType.Visible = flag;
//			lblClientPhone.Visible = flag;
//			lblClientStatus.Visible=flag;
//			lblClientTitle.Visible=flag;
//			CustomerDetail.Visible =flag;			
//		}
		

		/// <summary>
		/// Shows the details of policy on client top control
		/// </summary>
		private void ShowClaimDetails()
		{
            //objResourceMgr = new System.Resources.ResourceManager("Cms.cmsweb.webcontrols.ClaimTop", System.Reflection.Assembly.GetExecutingAssembly());
			//Making the client business client object for retreiving the policy
//			Cms.BusinessLayer.BlApplication.ClsGeneralInformation objApplication = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
//			
//
//			DataSet ds = objApplication.GetPolicyDataSet(CustomerID, PolicyID, PolicyVersionID);

			Cms.BusinessLayer.BLClaims.ClsClaims objClaims = new Cms.BusinessLayer.BLClaims.ClsClaims();
			//hide claim top when we don't have policy data, ie dummy policy case
			
			//We have a case of dummy policy and new claim being added
			//Display claim number and set other fields to display N.A.
			if(CustomerID==0 && ClaimID==0 && PolicyID==0)
			{
				lblSCustomerName1.Text = "N.A";
				lblSCustomerAddress.Text = "N.A";
				lblSCustomerType.Text = "N.A";
				lblSCustomerPhone.Text = "N.A";
				lblSAppNo1.Text = "N.A";
				lblSAppVersion.Text = "N.A";
                lblSClaimNumber2.Text =  objResourceMgr.GetString("EmptyClaimMessage");
				return;
			}

            int LangID = 0;

            cmsbase Obj=new cmsbase();
            LangID = int.Parse( Obj.GetLanguageID());
            DataSet ds = objClaims.GetPolicyClaimDataSet(CustomerID.ToString(), PolicyID.ToString(), PolicyVersionID.ToString(), ClaimID.ToString(), LangID);
			
			//When no data is returned, hide the claim top and return from any further work
			if (ds==null || ds.Tables.Count == 0)
			{
				this.Visible = false;
				return;
			}

			if(CustomerID==0)
			{
				CustomerDetail.Visible = false;
				CustomerDetailQ2.Visible = false;
			}

			if (ds.Tables[0].Rows.Count > 0)
			{
                

				System.Data.DataRow dr = ds.Tables[0].Rows[0];

				if(dr["POLICY_LOB"]!=null && dr["POLICY_LOB"].ToString()!="")
					hidLOB.Value=dr["POLICY_LOB"].ToString(); 
				else
					hidLOB.Value = "0";

				if(dr["CUSTOMERNAME"]!=null && dr["CUSTOMERNAME"].ToString()!="")				
					lblSCustomerName1.Text = dr["CUSTOMERNAME"].ToString();                				
				else
					lblSCustomerName1.Text = "N.A";

				if(dr["ADDRESS"]!=null && dr["ADDRESS"].ToString().Trim()!="")
					lblSCustomerAddress.Text = dr["ADDRESS"].ToString();                		
				else
					lblSCustomerAddress.Text = "N.A";

				if(dr["CUSTOMER_TYPE_DESC"]!=null && dr["CUSTOMER_TYPE_DESC"].ToString()!="")
					lblSCustomerType.Text = dr["CUSTOMER_TYPE_DESC"].ToString();
				else
					lblSCustomerType.Text = "N.A";

				if(dr["CUSTOMER_BUSINESS_PHONE"]!=null && dr["CUSTOMER_BUSINESS_PHONE"].ToString()!="")
					lblSCustomerPhone.Text = dr["CUSTOMER_BUSINESS_PHONE"].ToString();
				else
					lblSCustomerPhone.Text = "N.A";
                lblSAgencyPhone.Text = dr["AGENCY_PHONE"].ToString();
				//Check for policy display version also included as minimum value for effective date and expiration date come alongwith the policy column data.
				//That data essentially is redundant and indicates that no actual data exist.
				if((dr["POLICY"]!=null && dr["POLICY"].ToString()!="")) // && (dr["POLICY_DISP_VERSION"]!=null && dr["POLICY_DISP_VERSION"].ToString()!=""))
					lblSAppNo1.Text      =dr["POLICY"].ToString();
				else
					lblSAppNo1.Text = "N.A";

				if(dr["POLICY_DISP_VERSION"]!=null && dr["POLICY_DISP_VERSION"].ToString()!="")
					lblSAppVersion.Text = dr["POLICY_DISP_VERSION"].ToString() ;				
				else
					lblSAppVersion.Text = "N.A";

				
				if(ClaimID!=0 && dr["CLAIM_NUMBER"]!=null && dr["CLAIM_NUMBER"].ToString()!="")				
					lblSClaimNumber1.Text = dr["CLAIM_NUMBER"].ToString();		
				else
                    lblSClaimNumber2.Text = objResourceMgr.GetString("EmptyClaimMessage");

				if(dr["CUSTOMER_ATTENTION_NOTE"]!=System.DBNull.Value && dr["POLICY_DISP_VERSION"].ToString()!="" && dr["CUSTOMER_ATTENTION_NOTE"].ToString()!="" && dr["CUSTOMER_ATTENTION_NOTE"].ToString()!="0")
				{
					att_note="1";
					Image1.ImageUrl="~/cmsweb/images/att-ecs.gif"; 
				}
				else
				{
					Image1.ImageUrl="~/cmsweb/images/att-ecs-grey.gif";
				}
			}

			
	//			lblSCustomerName1.Visible=true;
	//			lblSCustomerName2.Visible=true;
	//			lblSCustomerAddress.Visible=true;
	//			lblSCustomerType.Visible=true;
	//			lblSCustomerPhone.Visible=true;
	//			lblSAppNo1.Visible=true;
	//			lblSAppNo2.Visible=true;
	//			lblSAppVersion.Visible=true;
			
			//CustomerDetail2.ImageUrl ="~/cmsweb/images" + colorScheme  + "/Customer_Ass.gif";
			CustomerDetailQ2.ImageUrl ="~/cmsweb/images" + colorScheme  + "/Customer_Ass.gif";
			
			//return;
		}
        private void SetCaptions()
        {

            //creating resource manager object (used for reading field and label mapping)
         

           

            lblCustomerName.Text = objResourceMgr.GetString("lblCustomerName");
            lblCustomerAddress.Text = objResourceMgr.GetString("lblCustomerAddress");//"Address";
            lblCustomerType.Text = objResourceMgr.GetString("lblCustomerType");//"Customer Type";
            lblCustomerPhone.Text = objResourceMgr.GetString("lblCustomerPhone");//"Phone";
            lblAppNo.Text = objResourceMgr.GetString("lblAppNo");//"Policy No.";
            lblAppVersion.Text = objResourceMgr.GetString("lblAppVersion");//"Policy Version";
            lblAgencyPhone.Text = objResourceMgr.GetString("lblAgencyPhone");
            lblClaimNumber.Text = objResourceMgr.GetString("lblClaimNumber");//"Claim No.";

        }

	

	
		
		private void ShowClaimLabels()
		{
			pnlApplication.Visible = true;

			lblCustomerName.Visible=true;
			lblCustomerAddress.Visible=true;
			lblCustomerType.Visible=true;
			lblCustomerPhone.Visible=true;
            lblAgencyPhone.Visible = true;
			lblAppNo.Visible=true;
			lblAppVersion.Visible=true;
		
			lblCustomerName.Text="Customer Name";
			lblCustomerAddress.Text="Address";
			lblCustomerType.Text="Customer Type";
			lblCustomerPhone.Text="Phone";

			lblAppNo.Text="Policy No.";
			lblAppVersion.Text="Policy Version";

			lblClaimNumber.Text = "Claim No.";
			
		}
		
		private string GetCustomerInfo(int intCustomerID)
		{
			//Making the object of clsClient for retreiving the information
			Cms.BusinessLayer.BlClient.ClsCustomer objClient;
			objClient = new Cms.BusinessLayer.BlClient.ClsCustomer();
			try
			{
				return objClient.FillCustomerDetails(intCustomerID);			
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
					objFullAddress.Append(dr["CUSTOMER_CITY"] + ", ");
				}

				//Added by Gaurav
				if (!dr["CUSTOMER_STATE_NAME"].ToString().Equals(""))
				{
					objFullAddress.Append(dr["CUSTOMER_STATE_NAME"] + ", ");
				}

				if (!dr["CUSTOMER_COUNTRY_NAME"].ToString().Equals(""))
				{
					objFullAddress.Append(dr["CUSTOMER_COUNTRY_NAME"] + ", ");
				}

				if (!dr["CUSTOMER_ZIP"].ToString().Equals(""))
				{
					objFullAddress.Append(dr["CUSTOMER_ZIP"] + " ");
				}
				
				lblClientAddress.Text = objFullAddress.ToString().Trim();
				objFullAddress = null;
				
				if(dr["IS_ACTIVE"].ToString().Equals("Y"))
				{
					lblClientStatus.Text="Active";
				}
				if(dr["IS_ACTIVE"].ToString().Equals("N"))
				{
					lblClientStatus.Text="Inactive";
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
					lblPhone.Text ="Home Phone";
					lblClientPhone.Text = dr["CUSTOMER_HOME_PHONE"].ToString();		//IF personal,home phone should display
				}
				else
				{
					lblPhone.Text ="Business Phone";
					lblClientPhone.Text = dr["CUSTOMER_BUSINESS_PHONE"].ToString();	//IF Commercial,home phone should display
				}

				lblClientType.Text = dr["CUSTOMER_TYPE_DESC"].ToString();
				lblClientTitle.Text = dr["PREFIX_DESC"].ToString();

			}
			//return;
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
