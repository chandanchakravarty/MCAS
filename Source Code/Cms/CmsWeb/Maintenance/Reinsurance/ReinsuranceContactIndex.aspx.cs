/* ***************************************************************************************
   Author		: Harmanjeet Singh
   Creation Date: April 20, 2007
   Last Updated : 
   Reviewed By	: 
   Purpose		: This is Grid file used for Excess Layer for a reinsurance contract. 
   Comments		: 
   ------------------------------------------------------------------------------------- 
   History	Date	     Modified By		Description
   
   ------------------------------------------------------------------------------------- 
   *****************************************************************************************/

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
using Cms.CmsWeb.WebControls;
using System.Resources;
using System.Reflection;


namespace Cms.CmsWeb.Maintenance.Reinsurance
{
	/// <summary>
	/// Summary description for ReinsuranceContactIndex.
	/// </summary>
	public class ReinsuranceContactIndex : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.HtmlControls.HtmlForm Division;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidEntityType;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidEntityId;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        ResourceManager objResourceMgr = null;
		string colors = "";
		protected System.Web.UI.WebControls.Label capMessage;
		private string strCalledFrom="";

		private void Page_Load(object sender, System.EventArgs e)
		{

			SetCookieValue();
            objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.Reinsurance.ReinsuranceContactIndex", Assembly.GetExecutingAssembly());	

			hidEntityId.Value			=	Request.Params["EntityId"];
			hidEntityType.Value			=	Request.Params["EntityType"];

			// Put user code to initialize the page here
			#region setting screen id
			if (Request.QueryString["CALLEDFROM"]!=null && Request.QueryString["CALLEDFROM"].ToString().Trim()!="")
			{
				strCalledFrom = Request.QueryString["CALLEDFROM"].ToString().Trim();	
				
			}
			switch(strCalledFrom)
			{
				case "bank" :
				case "BANK" :
					base.ScreenId	=	"125_1_2";
					break;
				case "agency" :
				case "AGENCY" :
					base.ScreenId	=	"10_2";
					break;
				case "department" :
				case "DEPARTMENT" :
					base.ScreenId	=	"29_1";
					break;
				case "division" :
				case "DIVISION" :
					base.ScreenId	=	"28_1";
					break;
				
				case "fin" :
				case "FINANCE" :
					base.ScreenId	=	"35_2";
					break;
				case "mortgage" :
				case "MORTGAGE" :
					base.ScreenId	=	"37_1";
					break;
				case "profit" :
				case "PROFIT" :
					base.ScreenId	=	"27_1";
					break;
				case "tax" :
				case "TAX" :
					base.ScreenId	=	"36_1";
					break;
				case "vendor" :
				case "VENDOR" :
					base.ScreenId	=	"32_1";
					break;
				case "reinsurer" :
				case "REINSURER" :
					base.ScreenId	=	"263_0_0";
					break;
				case "InCLT" :    		
					base.ScreenId	=	"120_4";
					break;				
				default :
					base.ScreenId	=	"36_1";
					break;
			}
			#endregion

			//Updating the security xml
			//UpdateSecurityXml(strCalledFrom);
			
			
			if ( !Page.IsPostBack )
			{

				#region GETTING BASE COLOR FOR ROW SELECTION
				
				string colorScheme=GetColorScheme();
				colors="";

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

				//BindGrid();
				# region C O D E   F O R   G R I D   C O N T R O L

				Control c1 = LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");

				BaseDataGrid objWebGrid;
				objWebGrid = (BaseDataGrid)c1;

				//Setting web grid control properties
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";

				objWebGrid.SelectClause			= "REIN_CONTACT_ID,REIN_CONTACT_NAME,REIN_CONTACT_CODE,IsNull(REIN_CONTACT_ADDRESS_1,'')+' '+IsNull(REIN_CONTACT_ADDRESS_2,'') As Address,REIN_CONTACT_ADDRESS_1,REIN_CONTACT_CITY,IsNull(MNT_REIN_CONTACT.REIN_CONTACT_STATE,'') as STATE_NAME,ISNULL(REIN_CONTACT_PHONE_1,'')+ ' ' +ISNULL(REIN_CONTACT_PHONE_2,'') + ' ' + ISNULL(REIN_CONTACT_MOBILE,'') AS Phone,REIN_CONTACT_PHONE_2,REIN_CONTACT_MOBILE,REIN_CONTACT_FAX,REIN_CONTACT_PHONE_1,MNT_REIN_CONTACT.IS_ACTIVE";
				objWebGrid.FromClause			= "MNT_REIN_CONTACT"; // Left outer Join MNT_COUNTRY_STATE_LIST SL ON MNT_REIN_CONTACT.REIN_CONTACT_STATE = SL.STATE_ID ";
				objWebGrid.WhereClause			= "REIN_COMAPANY_ID = '"+Request.QueryString["REIN_COMAPANY_ID"]+"'";
                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Contact Name^Contact Code^Address ^City^State^Phone^Fax";
				objWebGrid.SearchColumnNames	= "REIN_CONTACT_NAME^REIN_CONTACT_CODE^IsNull(REIN_CONTACT_ADDRESS_1,'') ! ' ' ! IsNull(REIN_CONTACT_ADDRESS_2,'')^REIN_CONTACT_CITY^SL.STATE_NAME^ISNULL(REIN_CONTACT_PHONE_1,'') ! ' ' ! ISNULL(REIN_CONTACT_PHONE_2,'') ! ' ' ! ISNULL(REIN_CONTACT_MOBILE,'')^REIN_CONTACT_FAX";
				objWebGrid.SearchColumnType		= "T^T^T^T^T^T^T";
				objWebGrid.OrderByClause		= "REIN_CONTACT_NAME asc";
				
				objWebGrid.DisplayColumnNumbers = "2^3^4^6^7^8^10";
				objWebGrid.DisplayColumnNames	= "REIN_CONTACT_NAME^REIN_CONTACT_CODE^REIN_CONTACT_ADDRESS_1^REIN_CONTACT_CITY^STATE_NAME^REIN_CONTACT_PHONE_1^REIN_CONTACT_PHONE_2^REIN_CONTACT_FAX^REIN_CONTACT_MOBILE";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Contact Name^Contact Code^Address 1^City^State^Phone 1^Phone 2^Fax^Mobile";
				objWebGrid.DisplayTextLength	= "100^100^100^100^100^100^100^100^100";
				objWebGrid.DisplayColumnPercent = "15^15^10^10^10^10^10^10^10";
				objWebGrid.PrimaryColumns		= "1";
				objWebGrid.PrimaryColumnsName	= "REIN_CONTACT_ID";

				objWebGrid.ColumnTypes = "B^B^B^B^B^B^B^B^B";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10^11^12";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";objWebGrid.ExtraButtons = "";
                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";
				objWebGrid.PageSize =int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Contact Information";
				objWebGrid.SelectClass = colors;
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";
				objWebGrid.FilterColumnName="MNT_REIN_CONTACT.IS_ACTIVE";
				objWebGrid.QueryStringColumns = "REIN_CONTACT_ID";
				objWebGrid.FilterValue="Y";
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch="Y";
			
				
			
				//Adding control to gridholder
				GridHolder.Controls.Add(objWebGrid);

				TabCtl.TabURLs = "ReinsuranceContact.aspx?REIN_COMAPANY_ID=" + Request.QueryString["REIN_COMAPANY_ID"]+ "&";
                TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");

				# endregion
			
			}

		}

		private void UpdateSecurityXml(string strCalledFrom)
		{

			if (strCalledFrom.ToUpper() == "APPLICATION")
			{
				//Called from application, hence checking whether policy for the application exist or not
				hidEntityId.Value="0";				
				Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGenInfo = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
				//System.Data.DataSet ds = objGenInfo.GetPolicyDetails(int.Parse(GetCustomerID()), int.Parse(GetAppID()), int.Parse(GetAppVersionID()));
				System.Data.DataSet ds = objGenInfo.GetPolicyDetailsForAttachment(int.Parse(GetCustomerID()), int.Parse(GetAppID()));
				if (ds.Tables[0].Rows.Count > 0)
				{
					//Policy exists for this particulat application, hence changing the security xml to view mode only
					gstrSecurityXML = "<Security><Read>Y</Read><Write>N</Write><Delete>N</Delete><Execute>N</Execute></Security>";
					base.InitializeSecuritySettings();
				}
				ds.Dispose();
			}
			else if(strCalledFrom.ToUpper() == "POLICY")
			{
				hidEntityType.Value="Policy";

				//Here we will check status of policy in session
				try
				{
					string strPolicyId = GetPolicyID();
					string strPolicyVerId = GetPolicyVersionID();
					string strCustomerID = GetCustomerID();
					
					Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGenInfo;
					objGenInfo = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();

					System.Data.DataSet ds = objGenInfo.GetPolicyDataSet(int.Parse(strCustomerID)
						, int.Parse(strPolicyId), int.Parse(strPolicyVerId));

					if (ds.Tables[0].Rows.Count > 0)
					{

						//If policy status is not one of following, changing the security xml to read only mode

						string policyStatus = ds.Tables[0].Rows[0]["POLICY_STATUS_CODE"].ToString().ToUpper().Trim();
						if ( ! ( policyStatus == Cms.BusinessLayer.BlCommon.ClsCommon.POLICY_STATUS_UNDER_ENDORSEMENT 
							|| policyStatus == Cms.BusinessLayer.BlCommon.ClsCommon.POLICY_STATUS_UNDER_RENEW
							|| policyStatus == Cms.BusinessLayer.BlCommon.ClsCommon.POLICY_STATUS_UNDER_CORRECTIVE_USER 
							|| policyStatus == Cms.BusinessLayer.BlCommon.ClsCommon.POLICY_STATUS_UNDER_ISSUE
							|| policyStatus == Cms.BusinessLayer.BlCommon.ClsCommon.POLICY_STATUS_SUSPENDED) )
						{

							//Changing the security xml to view mode only
							gstrSecurityXML = "<Security><Read>Y</Read><Write>N</Write><Delete>N</Delete><Execute>N</Execute></Security>";
							base.InitializeSecuritySettings(); 
						}
					}
					ds.Dispose();
				}
				catch
				{
				}
			}
		}

		private void SetCookieValue ()
		{
			//Setting the cookie if open from customer manager
			if (Request.Params["EntityType"].ToUpper() != "INCLT")
				return;


			string strCarrierSystemID = Cms.CmsWeb.cmsbase.CarrierSystemID;
			string strSystemID = GetSystemId();
			if(strCarrierSystemID.ToUpper()!=strSystemID.ToUpper())
				Response.Cookies["LastVisitedTab"].Value = "3";
			else
				Response.Cookies["LastVisitedTab"].Value = "4";
			Response.Cookies["LastVisitedTab"].Expires = DateTime.Now.Add(new TimeSpan(30,0,0,0,0));
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		
	}
}






