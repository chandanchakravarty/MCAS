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

/******************************************************************************************
	<Author					: Gaurav- >
	<Start Date				: Aug 26, 2005-	>
	<End Date				: - >
	<Description			: To be used for displaying the Endorsments - >
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History
	<Modified Date			: - >
	<Modified By			:  - >
	<Purpose				:  - >			
*******************************************************************************************/

namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for DivisionIndex.
	/// </summary>
	public class EndorsementAttachmentIndex : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		string strLobDesc,strSTATE_CODE;
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId = "217_1";
			string strEndorsementId="";
			

			if(Request.QueryString["ENDORSMENT_ID"]!=null && Request.QueryString["ENDORSMENT_ID"].Length>0)
			{
				strEndorsementId = Request.QueryString["ENDORSMENT_ID"].ToString();
			}

			if(Request.QueryString["LobDesc"]!=null && Request.QueryString["LobDesc"].Length>0)
			{
				strLobDesc = Request.QueryString["LobDesc"].ToString();
			}

			if(Request.QueryString["STATE_CODE"]!=null && Request.QueryString["STATE_CODE"].Length>0)
			{
				strSTATE_CODE = Request.QueryString["STATE_CODE"].ToString();
			}

			// Put user code to initialize the page here
			// Assinging the variable to be used for making the grid
			// Defining the contains the objectTextGrid literal control

			// These contains will generate the HTML required to generated the 
			// grid object

			#region GETTING BASE COLOR FOR ROW SELECTION
			string colorScheme=GetColorScheme();
			string colors="";

			switch (int.Parse(colorScheme))
			{
				case 1: 
					colors=System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR1").ToString();     
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
				
				
			BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("../../Cmsweb/webcontrols/BaseDataGrid.ascx");
			try
			{
				//Setting web grid control properties
				objWebGrid.WebServiceURL = "../../Cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				objWebGrid.SelectClause = "ENDORSEMENT_ATTACH_ID,ENDORSEMENT_ID,ATTACH_FILE,VALID_DATE,t1.EDITION_DATE,t4.LOB_CODE,t5.STATE_CODE";
				objWebGrid.FromClause = "MNT_ENDORSEMENT_ATTACHMENT t1 LEFT OUTER JOIN MNT_ENDORSMENT_DETAILS t2 ON t1.ENDORSEMENT_ID = t2.ENDORSMENT_ID LEFT OUTER JOIN MNT_COVERAGE t3 ON t3.COV_ID=t2.SELECT_COVERAGE LEFT OUTER JOIN MNT_LOB_MASTER t4 on t4.LOB_ID=t3.LOB_ID  LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST t5 on t5.STATE_ID=t2.STATE_ID";
                if(strEndorsementId!="" && strEndorsementId!=null)
				objWebGrid.WhereClause ="ENDORSEMENT_ID="+strEndorsementId;		
				objWebGrid.SearchColumnHeadings = "Endorsement Id^Attach File^Start Date^Edition Date";
				objWebGrid.SearchColumnNames = "ENDORSEMENT_ID^ATTACH_FILE^VALID_DATE^EDITION_DATE";
				//objWebGrid.DropDownColumns="^LOB^^^";
				objWebGrid.SearchColumnType = "T^T^D^T";
				objWebGrid.OrderByClause = "ENDORSEMENT_ID asc";
				objWebGrid.DisplayColumnNumbers = "2^3^4^5";
				objWebGrid.DisplayColumnNames = "ENDORSEMENT_ID^ATTACH_FILE^VALID_DATE^EDITION_DATE";
				objWebGrid.DisplayColumnHeadings = "Endorsement Id^Attach File^Start Date^Editon Date";
				objWebGrid.DisplayTextLength = "200^400^200^200";
				objWebGrid.DisplayColumnPercent = "23^38^24^15";
				objWebGrid.PrimaryColumns = "1";
				objWebGrid.PrimaryColumnsName = "ENDORSEMENT_ATTACH_ID";
				objWebGrid.ColumnTypes = "B^B^B^B";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4";
				objWebGrid.SearchMessage = "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				objWebGrid.ExtraButtons = "1^Add New^0^addRecord";
				objWebGrid.PageSize = int.Parse (GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.HeaderString ="Endorsement Attachment Information" ;
				//objWebGrid.SelectClass = colors;
				//objWebGrid.FilterLabel = "Show Inactive";
				//objWebGrid.FilterColumnName = "";
				//objWebGrid.FilterValue = "Y";
				objWebGrid.DefaultSearch="Y";
				objWebGrid.RequireQuery = "Y";
				//objWebGrid.QueryStringColumns = "ENDORSEMENT_ATTACH_ID^LOB_CODE^STATE_CODE";
				objWebGrid.QueryStringColumns = "ENDORSEMENT_ATTACH_ID";
				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);
				TabCtl.TabURLs = "AddEndorsementAttachment.aspx?ENDORSEMENT_ID=" + strEndorsementId + "&LobDesc=" + strLobDesc + "&STATE_CODE=" + strSTATE_CODE + "&";;
			}
			catch(Exception objExcep)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
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