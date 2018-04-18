/******************************************************************************************
<Author				: -   Vijay Arora
<Start Date			: -	  07-11-2005
<End Date			: -	 
<Description		: -  Policy Driver Index Page (LOB : Automobile).
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
using Cms.BusinessLayer.BlApplication;
using Cms.CmsWeb.WebControls;
using System.Xml; 
using System.IO;

namespace Cms.Policies.Aspx
{
	/// <summary>
	/// Show the Index page for addDriver Details.
	/// </summary>
	public class DriverDetailsIndex : Cms.Policies.policiesbase
	{
		protected System.Web.UI.WebControls.Panel pnlGrid;
		protected System.Web.UI.WebControls.Table tblReport;
		protected System.Web.UI.WebControls.Panel pnlReport;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.WebControls.Label capMessage;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
		private const string CALLED_FROM_PPA="PPA";
		public string strCalledFrom,strCalledFor;
		public string strCustomerID;
		public string strPolicyID;
		public string strPolicyVersionID;
		protected string capCopyButton;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			GetQueryString();
			#region setting screen id
			switch(strCalledFrom.ToUpper())
			{
				case "PPA" :
					base.ScreenId	=	"228";
					break;
					//Added ScreenID 252 for Operator Index
				case "Home" :
				case "HOME" :
					base.ScreenId	=	"252";
					break;
				case "mot" :
				case "MOT" :
					base.ScreenId	=	"237";
					break;
				case "wat" :
				case "WAT" :
					base.ScreenId	=	"247";
					break;
				case "UMB" :				
					base.ScreenId	=	"278";
					break;
				default :
					base.ScreenId	=	"45";
					break;					

			}
			#endregion

			GetSessionValues();
			
			cltClientTop.CustomerID = int.Parse(GetCustomerID());

			if(GetPolicyID()!="" && GetPolicyID()!=null && GetPolicyID()!="0")
			{
				cltClientTop.PolicyID = int.Parse(GetPolicyID());
				//flag=1;
			}

			if(GetPolicyVersionID()!="" && GetPolicyVersionID()!=null && GetPolicyVersionID()!="0")
			{
				cltClientTop.PolicyVersionID = int.Parse(GetPolicyVersionID());
				//flag=2;
			}
        
			cltClientTop.ShowHeaderBand ="Policy";

			cltClientTop.Visible = true;        

			#region GETTING BASE COLOR FOR ROW SELECTION
			string colorScheme=GetColorScheme();
			string colors="";

			switch (int.Parse(colorScheme))
			{
				case 1:
                    colors = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR1").ToString();     
					break;
				case 2:
                    colors = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR2").ToString();     
					break;
				case 3:
                    colors = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR3").ToString();     
					break;
				case 4:
                    colors = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR4").ToString();     
					break;
			}

			if(colors!="")
			{
				string [] baseColor=colors.Split(new char []{','});  
				if(baseColor.Length>0)
					colors= "#" + baseColor[0];
			}
			#endregion 


			#region loading web grid control
			
			//Control c1 = LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");

			try
			{
				//Setting web grid control properties
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";

				objWebGrid.SelectClause = "CUSTOMER_ID,DRIVER_ID,IsNull(DRIVER_FNAME,'')+ ' ' + IsNull(DRIVER_MNAME,'') + ' ' + IsNull(DRIVER_LNAME,'') DRIVER"
					+ ",DRIVER_CODE,"
					+ " Rtrim(Ltrim(IsNull(DRIVER_ADD1,'') + ' ' + IsNull(DRIVER_ADD2,'') + ' ' + isNull(DRIVER_CITY,'') + ' ' + IsNull(STATE_NAME,''))) Address";

				//Applied condition for Homeowners
				if ((strCalledFrom.Trim() != "WAT") && (strCalledFrom.Trim() != "Home"))
					objWebGrid.SelectClause += ",DRIVER_BUSINESS_PHONE";

				objWebGrid.SelectClause += ",POLICY_VERSION_ID,POLICY_ID";
				//Applied condition for Homeowners	
				if ((strCalledFrom.Trim() == "WAT") || (strCalledFrom.Trim() == "Home"))
				{
					objWebGrid.FromClause = "POL_WATERCRAFT_DRIVER_DETAILS "
						+ " LEFT JOIN MNT_COUNTRY_STATE_LIST ON POL_WATERCRAFT_DRIVER_DETAILS.DRIVER_STATE = MNT_COUNTRY_STATE_LIST.STATE_ID "
						+ " AND POL_WATERCRAFT_DRIVER_DETAILS.DRIVER_COUNTRY = MNT_COUNTRY_STATE_LIST.COUNTRY_ID";

					objWebGrid.FilterColumnName = "POL_WATERCRAFT_DRIVER_DETAILS.IS_ACTIVE";
					objWebGrid.SelectClause += ",POL_WATERCRAFT_DRIVER_DETAILS.IS_ACTIVE";
				}
				else if (strCalledFrom.ToUpper() == "UMB")
				{
					objWebGrid.SelectClause  +=  ",POL_UMBRELLA_DRIVER_DETAILS.IS_ACTIVE, CONVERT(CHAR,POL_UMBRELLA_DRIVER_DETAILS.DRIVER_DOB,101) DRIVER_DOB, LV.LOOKUP_VALUE_DESC AS DRIVER_TYPE";
					objWebGrid.FromClause = "POL_UMBRELLA_DRIVER_DETAILS "
						+ " LEFT JOIN MNT_COUNTRY_STATE_LIST ON POL_UMBRELLA_DRIVER_DETAILS.DRIVER_STATE = MNT_COUNTRY_STATE_LIST.STATE_ID "
						+ " AND POL_UMBRELLA_DRIVER_DETAILS.DRIVER_COUNTRY = MNT_COUNTRY_STATE_LIST.COUNTRY_ID "
						+ " LEFT OUTER JOIN MNT_LOOKUP_VALUES LV ON POL_UMBRELLA_DRIVER_DETAILS.DRIVER_DRIV_TYPE = LV.LOOKUP_UNIQUE_ID ";

					objWebGrid.FilterColumnName = "POL_UMBRELLA_DRIVER_DETAILS.IS_ACTIVE";
				}
				else
				{
					objWebGrid.FromClause = "POL_DRIVER_DETAILS "
						+ " LEFT JOIN MNT_COUNTRY_STATE_LIST ON POL_DRIVER_DETAILS.DRIVER_STATE = MNT_COUNTRY_STATE_LIST.STATE_ID "
						+ " AND POL_DRIVER_DETAILS.DRIVER_COUNTRY = MNT_COUNTRY_STATE_LIST.COUNTRY_ID";

					objWebGrid.FilterColumnName = "POL_DRIVER_DETAILS.IS_ACTIVE";
					objWebGrid.SelectClause += ",POL_DRIVER_DETAILS.IS_ACTIVE";
				}
				
				
				objWebGrid.WhereClause = " CUSTOMER_ID = '" + strCustomerID 
						+ "' AND POLICY_ID = '" + strPolicyID  
						+ "' AND POLICY_VERSION_ID = '" + strPolicyVersionID + "'";

				string AgencyId = GetSystemId();
                //Changed by Charles on 19-May-10 for Itrack 51
                if (AgencyId.ToUpper() != CarrierSystemID.ToUpper())//System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID").ToUpper())
                {
                    //objWebGrid.ExtraButtons = "4^Add New~Copy Applicants~Copy Policy Drivers~Request MVR^0~1~2~3~4^addRecord~copyApplicant~copyApplicationDrivers~FetchMVR";
                    objWebGrid.ExtraButtons = "1^Add New^0^addRecord";
                }
                else
                {
                    //objWebGrid.ExtraButtons = "6^Add New~Copy Applicants~Copy Policy Drivers~Request MVR~Request Loss Report~Prior Loss Tab^0~1~2~3~4^addRecord~copyApplicant~copyApplicationDrivers~FetchMVR~FetchLossReport~PriorLossTab";
                    objWebGrid.ExtraButtons = "1^Add New^0^addRecord";

                }

				//Applied condition for HomeOwners
				if ((strCalledFrom.Trim() == "WAT") ||(strCalledFrom.Trim() == "Home"))
				{
					objWebGrid.SearchColumnHeadings = "Operator Name^Operator Code^Address";
					objWebGrid.SearchColumnNames = "DRIVER_FNAME ! IsNull(DRIVER_MNAME,'') ! IsNull(DRIVER_LNAME,'')^DRIVER_CODE^ IsNull(DRIVER_ADD1,'') ! IsNull(DRIVER_ADD2,'') ! IsNull(DRIVER_CITY,'') ! IsNull(STATE_NAME,'')";
					objWebGrid.DisplayColumnHeadings ="Operator Name^Operator Code^Address";
					objWebGrid.DisplayColumnNames = "DRIVER^DRIVER_CODE^Address";	
					objWebGrid.SearchColumnType = "T^T^T";
					objWebGrid.OrderByClause = "DRIVER ASC";
					objWebGrid.DisplayColumnNumbers = "3^4^5";
					objWebGrid.DisplayTextLength = "75^50^75";
					objWebGrid.DisplayColumnPercent = "30^10^30";
					objWebGrid.PrimaryColumns = "2";
					objWebGrid.PrimaryColumnsName = "DRIVER_ID";
					objWebGrid.ColumnTypes = "B^B^B";
					objWebGrid.HeaderString = "Operator/Household Members Details";					
					objWebGrid.ExtraButtons = "4^Add New~Copy Applicants~Copy Policy Drivers~Request MVR^0~1~2~3~4^addRecord~copyApplicant~copyApplicationDrivers~FetchMVR";
				}
				else if (strCalledFrom.Trim().ToUpper()== "UMB")
				{
					//Modified by Swastika on 1st Mar'06 for Pol Iss #41
					objWebGrid.SearchColumnHeadings = "Driver Name^Date of Birth^Driver Type";
					objWebGrid.SearchColumnNames = "DRIVER_FNAME ! IsNull(DRIVER_MNAME,'') ! IsNull(DRIVER_LNAME,'')^DRIVER_DOB^LV.LOOKUP_VALUE_DESC";
					objWebGrid.DisplayColumnHeadings = "Driver Name^Date of Birth^Driver Type";
					objWebGrid.DisplayColumnNames = "DRIVER^DRIVER_DOB^DRIVER_TYPE";
					objWebGrid.SearchColumnType = "T^D^T";				
					objWebGrid.OrderByClause = "DRIVER ASC";				
					objWebGrid.DisplayColumnNumbers = "3^4^5";
					objWebGrid.DisplayTextLength = "75^50^75";
					objWebGrid.DisplayColumnPercent = "35^30^35";
					objWebGrid.PrimaryColumns = "2";
					objWebGrid.PrimaryColumnsName = "DRIVER_ID";
					objWebGrid.ColumnTypes = "B^B^B";
					objWebGrid.HeaderString = "Driver/Operator - Other Carriers";
					objWebGrid.ExtraButtons = "4^Add New~Copy Applicants~Copy Policy Drivers/Operators~Copy Records^0~1~2~3^addRecord~copyApplicant~copyApplicationDrivers~CopySchRecords";
					TabCtl.TabTitles = "Driver/Operator - Other Carriers";
				}
				else
				{
					//Modified by Swastika on 1st Mar'06 for Pol Iss #41
					objWebGrid.SearchColumnHeadings = "Driver Name^Driver Code^Address^Business Phone";
					objWebGrid.SearchColumnNames = "DRIVER_FNAME ! IsNull(DRIVER_MNAME,'') ! IsNull(DRIVER_LNAME,'')^DRIVER_CODE^ Rtrim(Ltrim(IsNull(DRIVER_ADD1,'') ! ' ' ! IsNull(DRIVER_ADD2,'') ! ' ' ! isNull(DRIVER_CITY,'') ! ' ' ! IsNull(STATE_NAME,'')))^DRIVER_BUSINESS_PHONE";
					objWebGrid.DisplayColumnHeadings = "Driver Name^Driver Code^Address^Business Phone";
					objWebGrid.DisplayColumnNames = "DRIVER^DRIVER_CODE^Address^DRIVER_BUSINESS_PHONE";
					objWebGrid.SearchColumnType = "T^T^T^T";				
					objWebGrid.OrderByClause = "DRIVER ASC";				
					objWebGrid.DisplayColumnNumbers = "3^4^5^6";
					objWebGrid.DisplayTextLength = "75^50^75^50";
					objWebGrid.DisplayColumnPercent = "30^10^30^20";
					objWebGrid.PrimaryColumns = "2";
					objWebGrid.PrimaryColumnsName = "DRIVER_ID";
					objWebGrid.ColumnTypes = "B^B^B^B";
					objWebGrid.HeaderString = "Driver/Household Members Details";
				}
							


				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9";

				objWebGrid.SearchMessage = "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				
				objWebGrid.FilterLabel = "Include Inactive";
				objWebGrid.FilterValue = "Y";			
			        
				
				objWebGrid.PageSize = int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				
				objWebGrid.SelectClass = colors;	
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch="Y";
				objWebGrid.QueryStringColumns = "DRIVER_ID";
				
				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			#endregion

			#region set Workflow cntrol
			//Added ScreenID=252 for Operator
			if(base.ScreenId == "228" || base.ScreenId == "237" || base.ScreenId == "247" || base.ScreenId == "252"  || base.ScreenId == "278")
			{
				myWorkFlow.ScreenID = base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				myWorkFlow.WorkflowModule="POL";
			}
			else
			{
				myWorkFlow.Display	=	false;
			}
			#endregion

			if(strCalledFrom.Trim().ToUpper() == "MOT")
			{
				TabCtl.TabURLs = "../Motorcycle/PolicyAddMotorDriver.aspx?CalledFrom=" + strCalledFrom 
					+ "&CUSTOMER_ID=" + strCustomerID 
					+ "&POLICY_ID=" + strPolicyID  
					+ "&POLICY_VERSION_ID=" + strPolicyVersionID + "&";
				//TabCtl.TabTitles ="Driver Information";
				TabCtl.TabTitles ="Driver/Household Members";
				TabCtl.TabLength=200;
			}//Applied condition for Homeowners
			else if((strCalledFrom.Trim().ToUpper() == "WAT") || (strCalledFrom.Trim().ToUpper() == "HOME"))
			{
				TabCtl.TabURLs = "../Watercraft/PolicyAddWatercraftOperator.aspx?CalledFrom=" + strCalledFrom 
					+ "&CUSTOMER_ID=" + strCustomerID 
					+ "&POLICY_ID=" + strPolicyID  
					+ "&POLICY_VERSION_ID=" + strPolicyVersionID + "&";
				TabCtl.TabTitles ="Operator/Household Members";
				TabCtl.TabLength=225;
			}
			else if((strCalledFrom.Trim().ToUpper() == "UMB"))
			{
				//TabCtl.TabURLs = "AddPolicyDriver.aspx?CalledFrom=" + strCalledFrom 
				TabCtl.TabURLs = "../UmbDriverDetails.aspx?CalledFrom=" + strCalledFrom 
					+ "&CUSTOMER_ID=" + strCustomerID 
					+ "&POLICY_ID=" + strPolicyID  
					+ "&POLICY_VERSION_ID=" + strPolicyVersionID + "&";
				TabCtl.TabTitles ="Driver/Operator Information";
				TabCtl.TabLength=200;
			}
			else
			{
				TabCtl.TabURLs = "AddPolicyDriver.aspx?CalledFrom=" + strCalledFrom 
					+ "&CUSTOMER_ID=" + strCustomerID 
					+ "&POLICY_ID=" + strPolicyID  
					+ "&POLICY_VERSION_ID=" + strPolicyVersionID + "&";
				//TabCtl.TabTitles ="Driver Information";
				TabCtl.TabTitles ="Driver/Household Members";
				TabCtl.TabLength=200;
			}
			
		}

		private void GetSessionValues()
		{
			strCustomerID = GetCustomerID();
			strPolicyID = GetPolicyID();
			strPolicyVersionID = GetPolicyVersionID();  
			
		}

		private void GetQueryString()
		{
			strCalledFrom = Request.Params["CalledFrom"];
			strCalledFor  = Request.Params["CalledFor"];
			if(strCalledFrom == null)
				strCalledFrom = "";

			if(strCalledFor == null)
				strCalledFor = "";

		}
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}