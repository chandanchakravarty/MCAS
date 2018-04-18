/******************************************************************************************
<Author				: -		Vijay Arora
<Start Date			: -		20-04-2006
<End Date			: -	
<Description		: - 	Index Page for Default Values of the Claim Events.
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		:		
<Modified By		:
<Purpose			: 
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
using System.Resources;
using System.Reflection;
using Cms.Model.Maintenance; 
using Cms.BusinessLayer.BlCommon;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb.WebControls;



namespace Cms.CmsWeb.Maintenance.Claims
{

	/// <summary>
	/// 
	/// </summary>
	public class DefaultValueClaimsIndex : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Panel pnlGrid;
		protected System.Web.UI.WebControls.Table tblReport;
		protected System.Web.UI.WebControls.Panel pnlReport;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Literal litTextGrid;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		private string TypeID = "";
        ResourceManager objResourceMgr = null;

		private void Page_Load(object sender, System.EventArgs e)
		{
			#region Setting screen id and Type ID
			   if(Request.QueryString["TYPE_ID"] != null && Request.QueryString["TYPE_ID"] != "")
			{
				TypeID	=	Request.QueryString["TYPE_ID"];
			}

		//Added by Sibin on 20 Oct 08 to add it into Claims - Default Values Permission List
			
			switch(TypeID)
			{
				case "Catastrophe/Event Types" :
				case "1" :
					base.ScreenId	=	"288";
					break;

				case "Party Types" :
				case "2" :
					base.ScreenId	=	"289";
					break;

				case "Claimant Types" :
				case "3" :
					base.ScreenId	=	"290";
					break;

				case "Expert/Service Provider Types" :
				case "4" :
					base.ScreenId	=	"291";
					break;

				case "Loss/Sub Types" :
				case "5" :
					base.ScreenId	=	"292";
					break;
				
				case "Recovery Type" :
				case "6" :
					base.ScreenId	=	"293";
					break;

				case "Claim Status" :
				case "7" :
					base.ScreenId	=	"294";
					break;

				case "Claim Transaction Code" :
				case "8" :
					base.ScreenId	=	"295";
					break;

				case "Service Types List" :
				case "9" :
					base.ScreenId	=	"296";
					break;
			}


			//base.ScreenId	=	"288";
			#endregion
            objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.Claims.DefaultValueClaimsIndex", Assembly.GetExecutingAssembly());
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
				
				string sSELECTCLAUSE="", sWHERECLAUSE="", sFROMCLAUSE="";
				
				if (TypeID.Trim() == "8")		//Claims Transaction Codes.
				{
					sSELECTCLAUSE= " CTD.*, MLV.LOOKUP_VALUE_DESC AS TRAN_DESC ";
                    sFROMCLAUSE = " CLM_TYPE_DETAIL CTD LEFT OUTER JOIN MNT_LOB_MASTER MLB ON MLB.LOB_ID = CTD.LOSS_DEPARTMENT LEFT JOIN MNT_COVERAGE MC ON MC.COV_ID = CTD.LOSS_EXTRA_COVER LEFT JOIN MNT_LOOKUP_VALUES MLV ON MLV.LOOKUP_UNIQUE_ID = CTD.TRANSACTION_CODE";
					sWHERECLAUSE = " TYPE_ID = " + TypeID;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings1");//"Description^Transaction Code";	
					objWebGrid.SearchColumnNames = "DETAIL_TYPE_DESCRIPTION^LOOKUP_VALUE_DESC";
					objWebGrid.SearchColumnType = "T^T";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings1");//"Description^Transaction Code";	
					objWebGrid.DisplayColumnNumbers = "3^10";
					objWebGrid.DisplayColumnNames = "DETAIL_TYPE_DESCRIPTION^TRAN_DESC";
					objWebGrid.DisplayTextLength = "60^40";
					objWebGrid.DisplayColumnPercent = "60^40";
					objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10";
					objWebGrid.ColumnTypes = "B^B";				
					objWebGrid.FilterColumnName = "CTD.IS_ACTIVE";
                }//Added by Ruchika Chauhan on 14-Jan-2012 for TFS Bug #3229
                else if ((TypeID.Trim() == "1") || (TypeID.Trim() == "3") || (TypeID.Trim() == "4") || (TypeID.Trim() == "6") || (TypeID.Trim() == "9")) 
                {
                    sSELECTCLAUSE = objResourceMgr.GetString("SelectClause");
                    sFROMCLAUSE = objResourceMgr.GetString("FromClause");
                    sWHERECLAUSE = " TYPE_ID = " + TypeID;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings3");
                    objWebGrid.SearchColumnNames = objResourceMgr.GetString("SearchColumnNames3");
                    objWebGrid.SearchColumnType = objResourceMgr.GetString("SearchColumnType");
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings3");
                    objWebGrid.DisplayColumnNumbers = objResourceMgr.GetString("DisplayColumnNumbers3");
                    objWebGrid.DisplayColumnNames = objResourceMgr.GetString("DisplayColumnNames3");
                    objWebGrid.DisplayTextLength = objResourceMgr.GetString("DisplayTextLength3");
                    objWebGrid.DisplayColumnPercent = objResourceMgr.GetString("DisplayColumnPercent3");
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10^11^12^13^14^15^16^17^18^19^20^21^22";
                    objWebGrid.ColumnTypes = objResourceMgr.GetString("ColumnTypes3");
                    objWebGrid.FilterColumnName = "CTD.IS_ACTIVE";

                }//Added by Ruchika Chauhan on 31-Jan-2012 for TFS Bug #3229
                else if (TypeID.Trim() == "2")
                {
                    sSELECTCLAUSE = objResourceMgr.GetString("SelectClause");
                    sFROMCLAUSE = objResourceMgr.GetString("FromClause");
                    sWHERECLAUSE = " TYPE_ID = " + TypeID;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings2");
                    objWebGrid.SearchColumnNames = objResourceMgr.GetString("SearchColumnNames");
                    objWebGrid.SearchColumnType = objResourceMgr.GetString("SearchColumnType");
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings2");
                    objWebGrid.DisplayColumnNumbers = objResourceMgr.GetString("DisplayColumnNumbers2");
                    objWebGrid.DisplayColumnNames = objResourceMgr.GetString("DisplayColumnNames2");
                    objWebGrid.DisplayTextLength = objResourceMgr.GetString("DisplayTextLength7");
                    objWebGrid.DisplayColumnPercent = objResourceMgr.GetString("DisplayColumnPercent2");
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10^11^12^13^14^15^16^17^18^19^20^21^22";
                    objWebGrid.ColumnTypes = objResourceMgr.GetString("ColumnTypes7");
                    objWebGrid.FilterColumnName = "CTD.IS_ACTIVE";

                }   
                else if (TypeID.Trim() == "5")
                {
                    sSELECTCLAUSE = objResourceMgr.GetString("SelectClause");
                    sFROMCLAUSE = objResourceMgr.GetString("FromClause");
                    sWHERECLAUSE = " TYPE_ID = " + TypeID;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings5");
                    objWebGrid.SearchColumnNames = objResourceMgr.GetString("SearchColumnNames5");
                    objWebGrid.SearchColumnType = objResourceMgr.GetString("SearchColumnType5");
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings5");
                    objWebGrid.DisplayColumnNumbers = objResourceMgr.GetString("DisplayColumnNumbers5");
                    objWebGrid.DisplayColumnNames = objResourceMgr.GetString("DisplayColumnNames5");
                    objWebGrid.DisplayTextLength = objResourceMgr.GetString("DisplayTextLength");
                    objWebGrid.DisplayColumnPercent = objResourceMgr.GetString("DisplayColumnPercent");
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10^11^12^13^14^15^16^17^18^19^20^21^22";
                    objWebGrid.ColumnTypes = objResourceMgr.GetString("ColumnTypes");
                    objWebGrid.FilterColumnName = "CTD.IS_ACTIVE";

                } //Added till here    
                else if (TypeID.Trim() == "7")
                {
                    sSELECTCLAUSE = objResourceMgr.GetString("SelectClause");
                    sFROMCLAUSE = objResourceMgr.GetString("FromClause");
                    sWHERECLAUSE = " TYPE_ID = " + TypeID;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings7");
                    objWebGrid.SearchColumnNames = objResourceMgr.GetString("SearchColumnNames7");
                    objWebGrid.SearchColumnType = objResourceMgr.GetString("SearchColumnType");
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings7");
                    objWebGrid.DisplayColumnNumbers = objResourceMgr.GetString("DisplayColumnNumbers7");
                    objWebGrid.DisplayColumnNames = objResourceMgr.GetString("DisplayColumnNames7");
                    objWebGrid.DisplayTextLength = objResourceMgr.GetString("DisplayTextLength7");
                    objWebGrid.DisplayColumnPercent = objResourceMgr.GetString("DisplayColumnPercent7");
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10^11^12^13^14^15^16^17^18^19^20^21^22";
                    objWebGrid.ColumnTypes = objResourceMgr.GetString("ColumnTypes7");
                    objWebGrid.FilterColumnName = "CTD.IS_ACTIVE";

                }               
                else
                {
                    //sSELECTCLAUSE= " * ";
                    //sSELECTCLAUSE = "DETAIL_TYPE_ID,TYPE_ID,DETAIL_TYPE_DESCRIPTION,CTD.IS_ACTIVE,TRANSACTION_CODE,IS_SYSTEM_GENERATED,LOSS_TYPE_CODE,";
                    //sSELECTCLAUSE += "LOSS_DEPARTMENT,LOSS_EXTRA_COVER,MLB.LOB_DESC AS DEPARTMENT,MC.COV_DES AS EXTRA_COVER";
                    sSELECTCLAUSE = objResourceMgr.GetString("SelectClause");

                    //sFROMCLAUSE  = " CLM_TYPE_DETAIL CTD INNER JOIN MNT_LOB_MASTER MLB ON MLB.LOB_ID = CTD.LOSS_DEPARTMENT INNER JOIN MNT_COVERAGE MC ON MC.COV_ID = CTD.LOSS_EXTRA_COVER ";
                    sFROMCLAUSE = objResourceMgr.GetString("FromClause");

                    sWHERECLAUSE = " TYPE_ID = " + TypeID;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Description";	
                    //objWebGrid.SearchColumnNames = "DETAIL_TYPE_DESCRIPTION";
                    objWebGrid.SearchColumnNames = objResourceMgr.GetString("SearchColumnNames");

                    //objWebGrid.SearchColumnType = "T";
                    objWebGrid.SearchColumnType = objResourceMgr.GetString("SearchColumnType");

                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Description";

                    //objWebGrid.DisplayColumnNumbers = "3";
                    objWebGrid.DisplayColumnNumbers = objResourceMgr.GetString("DisplayColumnNumbers");

                    //objWebGrid.DisplayColumnNames = "DETAIL_TYPE_DESCRIPTION";
                    objWebGrid.DisplayColumnNames = objResourceMgr.GetString("DisplayColumnNames");

                    //objWebGrid.DisplayTextLength = "100";
                    objWebGrid.DisplayTextLength = objResourceMgr.GetString("DisplayTextLength");

                    //objWebGrid.DisplayColumnPercent = "100";
                    objWebGrid.DisplayColumnPercent = objResourceMgr.GetString("DisplayColumnPercent");

                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10^11^12^13^14^15^16^17^18^19^20^21^22";

                    //objWebGrid.ColumnTypes = "B";				
                    objWebGrid.ColumnTypes = objResourceMgr.GetString("ColumnTypes");


                    objWebGrid.FilterColumnName = "CTD.IS_ACTIVE";
                }

				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				objWebGrid.SelectClause = sSELECTCLAUSE;
				objWebGrid.FromClause = sFROMCLAUSE ; 
				objWebGrid.WhereClause = sWHERECLAUSE;	
				objWebGrid.PrimaryColumns = "1";
				objWebGrid.PrimaryColumnsName = "DETAIL_TYPE_ID";
				
				//objWebGrid.OrderByClause	="DETAIL_TYPE_ID asc";
                objWebGrid.OrderByClause = objResourceMgr.GetString("OrderByClause");

				objWebGrid.AllowDBLClick = "true";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";											
				objWebGrid.PageSize = int.Parse (GetPageSize());
				objWebGrid.CacheSize = int.Parse (GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.SelectClass = colors;
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";				 
				objWebGrid.RequireQuery = "Y";
				objWebGrid.QueryStringColumns = "DETAIL_TYPE_ID";
				objWebGrid.DefaultSearch = "Y";
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";
				
				objWebGrid.FilterValue = "Y";
				
				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);

				TabCtl.TabURLs = "AddDefaultValueClaims.aspx?TYPE_ID=" + TypeID + "&"; 
				TabCtl.TabLength =GetTabLength(TypeID);

				string strTypeString = GetTypeString(TypeID);
				objWebGrid.HeaderString = strTypeString;
				TabCtl.TabTitles = strTypeString;
				

			}
			catch (Exception ex)
			{throw (ex);}
			#endregion
		}
		public string GetTypeString(string TypeID)
		{
			switch(TypeID)
			{
                case "1": return objResourceMgr.GetString("HeaderString1");//"Catastrophe/Event Types";
                case "2": return objResourceMgr.GetString("HeaderString2");//"Party Types";
                case "3": return objResourceMgr.GetString("HeaderString3");//"Claimant Types";
                case "4": return objResourceMgr.GetString("HeaderString4");//"Expert/Service Provider Types";
                case "5": return objResourceMgr.GetString("HeaderString5");//"Loss Types/ Sub Types";
                case "6": return objResourceMgr.GetString("HeaderString6");//"Recovery Type";
                case "7": return objResourceMgr.GetString("HeaderString7");//"Claims Status";
                case "8": return objResourceMgr.GetString("HeaderString8");//"Claim Transaction Codes";
                case "9": return objResourceMgr.GetString("HeaderString9");//"Service Types";
                case "10": return objResourceMgr.GetString("HeaderString10");//"Relationship to the Insured";
                default: return objResourceMgr.GetString("HeaderString11");//"Types Information";

//				case "Catastrophe/Event Types" :
//				case "1" :
//					return "288";
//					break;
//
//				case "Party Types" :
//					return "289";
//
//				case "Claimant Types" :
//				case "3" :
//					return "290";
//					break;
//
//				case "Expert/Service Provider Types" :
//				case "4" :
//					return "291";
//					break;
//
//				case "Loss/Sub Types" :
//				case "5" :
//					return "292";
//					break;
//				
//				case "Recovery Type" :
//				case "6" :
//					return "293";
//					break;
//
//				case "Claim Status" :
//				case "7" :
//					return "294";
//					break;
//
//				case "Claim Transaction Code" :
//				case "8" :
//					return "295";
//					break;
//
//				case "Service Types List" :
//				case "9" :
//					return "296";
//					break;
												
			}


			
		}

		public int GetTabLength(string TypeID)
		{
			switch(TypeID)
			{
				case "1":
				case "4":
				case "5":
				case "8":
				case "10":				
					return 225;				
				default: return 150;
			}
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
