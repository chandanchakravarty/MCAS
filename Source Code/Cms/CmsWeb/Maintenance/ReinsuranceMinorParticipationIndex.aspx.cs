/* ***************************************************************************************
   Author		: Deepak Batra 
   Creation Date: 05/01/2006
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

namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for ReinsuranceMinorParticipationIndex.
	/// </summary>
	public class ReinsuranceMinorParticipationIndex : Cms.CmsWeb.cmsbase
	{
		# region D E C L A R A T I O N 

		protected System.Web.UI.WebControls.Table tblReport;
		protected System.Web.UI.WebControls.Panel pnlReport;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.WebControls.Label capMessage;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		
		protected string Pass_Major_Participants="";
		protected string Pass_Major_Layer="";

		protected string strContractID = "0";
        ResourceManager objResourceMgr = null;

		# endregion 

		# region P A G E   L O A D 
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
			
				// Put user code to initialize the page here

				//base.ScreenId = "262_4";
				base.ScreenId = "262_5";
                objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.ReinsuranceMinorParticipationIndex", Assembly.GetExecutingAssembly());
				Pass_Major_Participants=Request.QueryString["Major_Participants"];
				Pass_Major_Layer=Request.QueryString["Layer"];


				if(Request.QueryString["CONTRACT_ID"]!=null && Request.QueryString["CONTRACT_ID"].ToString().Length>0)
					strContractID = Request.QueryString["CONTRACT_ID"].ToString();

				#region G E T T I N G   B A S E   C O L O R   F O R   R O W   S E L E C T I O N
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

				# region C O D E   F O R   G R I D   C O N T R O L

				Control c1 = LoadControl("../../cmsweb/webcontrols/BaseDataGrid.ascx");

				BaseDataGrid objWebGrid;
				objWebGrid = (BaseDataGrid)c1;

				//Setting web grid control properties
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";

				objWebGrid.SelectClause			= "MRP.MINOR_PARTICIPATION_ID MINOR_PARTICIPATION_ID,MRP.MAJOR_PARTICIPANTS MAJOR_PARTICIPANTS,MRP.MINOR_LAYER MINOR_LAYER,MLV.REIN_COMAPANY_NAME As MINOR_PARTICIPANTS,MRP.MINOR_WHOLE_PERCENT MINOR_WHOLE_PERCENT,MRP.IS_ACTIVE,MRP.CONTRACT_ID as CONTRACT_ID,MLV1.REIN_COMAPANY_NAME MAJOR_PARTICIPANTS1";
				//objWebGrid.SelectClause			+="ISNULL(MRP.REINSURANCE_COMPANY,'') as REINSURANCE_COMPANY,ISNULL(MRP.WHOLE_PERCENT,0) As WHOLE_PERCENT, ISNULL(MRP.REINSURANCE_ACC_NUMBER,0) as REINSURANCE_ACC_NUMBER,";
				//objWebGrid.SelectClause			+="ISNULL(MRP.SEP_ACC, '') AS SEP_ACC, ISNULL(MRP.IS_ACTIVE,'Y') AS IS_ACTIVE,MRP.CREATED_BY AS CREATED_BY,MRP.CREATED_DATETIME AS CREATED_DATETIME,MRP.MODIFIED_BY AS MODIFIED_BY,MRP.LAST_UPDATED_DATETIME AS LAST_UPDATED_DATETIME, MLV.LOOKUP_VALUE_DESC AS LOOKUP_VALUE_DESC, MRCL.REIN_COMAPANY_NAME AS REIN_COMAPANY_NAME";
				objWebGrid.FromClause			= "MNT_REIN_MINOR_PARTICIPATION MRP "; 
				//objWebGrid.FromClause			+= "INNER JOIN MNT_REINSURANCE_CONTRACT MRC ON MRP.CONTRACT_ID =  MRC.CONTRACT_ID ";
				objWebGrid.FromClause			+= "INNER JOIN MNT_REIN_COMAPANY_LIST MLV ON MRP.MINOR_PARTICIPANTS = MLV.REIN_COMAPANY_ID ";
				objWebGrid.FromClause			+= "INNER join MNT_REINSURANCE_MAJORMINOR_PARTICIPATION MAJOR on MAJOR.PARTICIPATION_ID=MRP.MAJOR_PARTICIPANTS ";
				objWebGrid.FromClause			+= "INNER JOIN MNT_REIN_COMAPANY_LIST MLV1 ON MAJOR.REINSURANCE_COMPANY = MLV1.REIN_COMAPANY_ID ";

				objWebGrid.WhereClause			= "MRP.CONTRACT_ID = " + strContractID + "";// AND PARTICIPATION_TYPE = 'MINOR' ";//FOR MINOR PARTICIPATION SCREEN

                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Major Participant^Layer^Minor Participant^% of Whole Layer";
				objWebGrid.SearchColumnNames	= "MLV1.REIN_COMAPANY_NAME^MRP.MINOR_LAYER^MLV.REIN_COMAPANY_NAME^MINOR_WHOLE_PERCENT";
				objWebGrid.SearchColumnType		= "T^T^T^T";
				objWebGrid.OrderByClause		= "MINOR_PARTICIPATION_ID asc";

				objWebGrid.DisplayColumnNumbers = "2^3^4^5";
				objWebGrid.DisplayColumnNames	= "MAJOR_PARTICIPANTS1^MINOR_LAYER^MINOR_PARTICIPANTS^MINOR_WHOLE_PERCENT";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Major Participant^Layer^Minor Participant^% of Whole Layer";
				objWebGrid.DisplayTextLength	= "120^100^150^100";
				objWebGrid.DisplayColumnPercent = "25^25^25^25";
				objWebGrid.PrimaryColumns		= "1";
				objWebGrid.PrimaryColumnsName	= "MINOR_PARTICIPATION_ID";

				objWebGrid.ColumnTypes = "B^B^B^B";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5^6";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";objWebGrid.ExtraButtons = "";
                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";
				objWebGrid.PageSize =int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Minor Participation";
				objWebGrid.SelectClass = colors;
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";
				objWebGrid.FilterColumnName="MRP.IS_ACTIVE";
				objWebGrid.QueryStringColumns = "MINOR_PARTICIPATION_ID^CONTRACT_ID";
				objWebGrid.FilterValue="Y";
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch="Y";
			
				//Adding control to gridholder
				GridHolder.Controls.Add(objWebGrid);

				TabCtl.TabURLs = "AddReinsuranceMinorPart.aspx?ContractID="+strContractID+"&Major_Participants=" +Pass_Major_Participants + "&Layer=" + Pass_Major_Layer +"&";
                TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");	
				# endregion
			}
			catch(Exception oEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);
			}
		}

		# endregion 

		#region W E B  F O R M   D E S I G N E R   G E N E R A T E D   C O D E
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
