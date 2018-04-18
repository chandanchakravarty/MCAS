/******************************************************************************************
<Author					: -  Pravesh K Chandel
<Start Date				: -	 14 Jan 2010 12:44:25 PM
<End Date				: -	
<Description			: - 	Index page 
<Review Date			: - 
<Reviewed By			: - 	
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
using Cms.CmsWeb.WebControls;
//using Cms.BusinessLayer.BlApplication;
namespace Cms.Policies.Aspx.Aviation
{
	/// <summary>
	/// Summary description for PolicyVehicleIndex.
	/// </summary>
	public class PolicyVehicleIndex : Cms.Policies.policiesbase
	{
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;

		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;

		private const string CUSTOMER_SECTION ="CLT";
		private const string APP_SECTION ="APP";
		
		protected string strCalledFrom = "";

		private void Page_Load(object sender, System.EventArgs e)
		{
			#region Setting screen id
			base.ScreenId	=	"449";
			#endregion
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
			BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			try
			{
				string sFECTHCOLUMNS = "";
				string strCUSTOMER_ID =GetCustomerID();
				hidCustomerID.Value =strCUSTOMER_ID;
				string strPolicyID = GetPolicyID();
				hidPolicyID.Value =strPolicyID;
				string strPolVersionID = GetPolicyVersionID();
				hidPolicyVersionID.Value =strPolVersionID;
				string strCALLEDFROM="";
				bool ShowGrid =true;
				string sSELECTCLAUSE="", sWHERECLAUSE="", sFROMCLAUSE="",sQUERYSTRING="";

				/* If the control is coming from CUSTOMER SECTION
				 * If it is coming from customer sectn then check if customerid is selected.
				 *  -if customer is not selected then prompt the user and do not show the grid
				 *  -else show only those vehicle records that belong to the selected customer.
				 * If the control is coming from the APPLICATION SECTION
				 *  - Check if the APPID and APPVERSIONID along with CUSTOMERID are present in the session. If yes then show the vehicles related to the selected app
				 *  else prompt the user to select an application first. Do not show the grid in the latter case.
				 */
					//strCALLEDFROM = Request.QueryString["CALLEDFROM"].ToString();
					hidCalledFrom.Value =strCALLEDFROM;

					if (strCUSTOMER_ID!=null && !strCUSTOMER_ID.Equals("") && strPolicyID!=null && !strPolicyID.Equals("") && strPolVersionID !=null && !strPolVersionID.Equals("") )
					{				
						string strSelectQueryForGrid = "";

						#region SelectQueryForGrid
						strSelectQueryForGrid += " T1.VEHICLE_ID,T1.VEHICLE_ID AS VH_ID,T1.INSURED_VEH_NUMBER,T1.USE_VEHICLE,LV1.LOOKUP_VALUE_DESC AS  USE_VEHICLE_DESC, ";
						strSelectQueryForGrid += " T1.VEHICLE_YEAR,T1.MAKE,MAKE.LOOKUP_VALUE_DESC AS MAKE_DESC,T1.MODEL,MODEL.MODEL AS MODEL_DESC,T1.ENGINE_TYPE,ENTYPE.LOOKUP_VALUE_DESC AS ENTYPE_DESC,";
						strSelectQueryForGrid += " T1.CUSTOMER_ID,T1.POLICY_ID,T1.POLICY_VERSION_ID,AL.POLICY_LOB,T1.IS_ACTIVE, ";
						strSelectQueryForGrid += " T1.WING_TYPE ";
						#endregion

						sSELECTCLAUSE=strSelectQueryForGrid;
						sFROMCLAUSE  = "POL_AVIATION_VEHICLES t1  LEFT JOIN POL_CUSTOMER_POLICY_LIST AL ON AL.POLICY_ID = t1.POLICY_ID  and AL.POLICY_VERSION_ID=t1.POLICY_VERSION_ID AND AL.CUSTOMER_ID=t1.CUSTOMER_ID" +
							" LEFT OUTER JOIN MNT_LOOKUP_VALUES LV1 ON CONVERT(VARCHAR,LV1.LOOKUP_UNIQUE_ID)=T1.USE_VEHICLE " + 
							" LEFT OUTER JOIN MNT_LOOKUP_VALUES ENTYPE ON CONVERT(VARCHAR,ENTYPE.LOOKUP_UNIQUE_ID)=T1.ENGINE_TYPE " +
							" LEFT OUTER JOIN MNT_LOOKUP_VALUES MAKE ON CONVERT(VARCHAR,MAKE.LOOKUP_UNIQUE_ID)=T1.MAKE " +
							" LEFT OUTER JOIN MNT_AVIATION_MODEL_LIST MODEL ON CONVERT(VARCHAR,MODEL.ID)=T1.MODEL " ;
						sWHERECLAUSE = " t1.CUSTOMER_ID="+strCUSTOMER_ID+" AND t1.POLICY_ID="+strPolicyID+" AND t1.POLICY_VERSION_ID ="+strPolVersionID ;	
						sQUERYSTRING = "&CUSTOMER_ID="+strCUSTOMER_ID +"&POLICY_ID="+strPolicyID +"&POLICY_VERSION_ID="+strPolVersionID;
						sFECTHCOLUMNS = "1^3^2^4^5^6^7^8^9^10^11^12^13^14^15^16^17^18^19^20^21^22^23^24^25^26^27^28^29";
					}
					else
					{
						sWHERECLAUSE =" 1<>1 ";
						lblError.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("109");
						ShowGrid =false;
					}
				//Setting web grid control properties
					
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				objWebGrid.SelectClause = sSELECTCLAUSE;
				objWebGrid.FromClause = sFROMCLAUSE ; 
				objWebGrid.WhereClause = sWHERECLAUSE;	

				objWebGrid.OrderByClause	="t1.INSURED_VEH_NUMBER asc";
				objWebGrid.SearchColumnHeadings = "Vehicle #^Vehicle Use^Engine Type^Year^Make^Model";
				objWebGrid.SearchColumnNames = "t1.INSURED_VEH_NUMBER^LV1.LOOKUP_VALUE_DESC^ENTYPE.LOOKUP_VALUE_DESC^t1.VEHICLE_YEAR^MAKE.LOOKUP_VALUE_DESC^MODEL.MODEL";
				objWebGrid.DisplayColumnHeadings = "Vehicle #^Vehicle Use^Engine Type^Year^Make^Model";
				objWebGrid.DisplayColumnNames = "INSURED_VEH_NUMBER^USE_VEHICLE_DESC^ENTYPE_DESC^VEHICLE_YEAR^MAKE_DESC^MODEL_DESC";
				objWebGrid.DisplayColumnNumbers = "2^3^4^5^6^7";
				objWebGrid.DisplayTextLength = "10^10^10^20^10^10";
				objWebGrid.DisplayColumnPercent = "10^10^10^10^10^10";
				objWebGrid.ColumnTypes = "B^B^B^B^B^B";
				objWebGrid.SearchColumnType = "T^T^T^T^T^T";
				
				objWebGrid.PrimaryColumns = "1";
				objWebGrid.PrimaryColumnsName = "t1.VEHICLE_ID";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = sFECTHCOLUMNS;
				objWebGrid.SearchMessage = "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				objWebGrid.ExtraButtons = "1^Add New^0^addRecord";//"1^Add New^0^addRecord";//"1^Add New^0";
				objWebGrid.PageSize = int.Parse (GetPageSize());
				objWebGrid.CacheSize = int.Parse (GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				
				objWebGrid.SelectClass = colors;
				objWebGrid.FilterLabel = "Show Complete";				 
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch = "Y";
				objWebGrid.QueryStringColumns ="VEHICLE_ID";//sQUERYSTRING;

				objWebGrid.FilterLabel = "Include Inactive";
				objWebGrid.FilterColumnName = "t1.Is_Active";
				objWebGrid.FilterValue = "Y";
				objWebGrid.RequireQuery = "Y";
				
				if(ShowGrid)
				{		
					// Pass additional parameters to the next page
					if (strCALLEDFROM != null)
					{
						objWebGrid.HeaderString ="Aviation Vehicle Information" ;
						TabCtl.TabURLs = "AddPolicyVehicle.aspx?CALLEDFROM=" + strCALLEDFROM +  sQUERYSTRING +"&"; //sQUERYSTRING +
						TabCtl.TabTitles ="Aviation Vehicle Info";
						TabCtl.TabLength =175;								
					}

					//Adding to controls to gridholder
					GridHolder.Controls.Add(objWebGrid);

					cltClientTop.CustomerID = int.Parse(GetCustomerID());
					cltClientTop.ApplicationID = int.Parse(GetAppID());
					cltClientTop.AppVersionID = int.Parse(GetAppVersionID());
            
					cltClientTop.PolicyID = int.Parse(GetPolicyID());
					cltClientTop.PolicyVersionID = int.Parse(GetPolicyVersionID());

					cltClientTop.ShowHeaderBand ="Policy";
					cltClientTop.Visible = true;        
				}
				
				
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			#endregion

			#region set Workflow cntrol
			if(base.ScreenId == "449")
			{
				myWorkFlow.ScreenID = base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				myWorkFlow.AddKeyValue("REMARKS","isnull(REMARKS,-2)");
				myWorkFlow.WorkflowModule="POL";
			}
			else
			{
				myWorkFlow.Display	=	false;
			}
			#endregion
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
