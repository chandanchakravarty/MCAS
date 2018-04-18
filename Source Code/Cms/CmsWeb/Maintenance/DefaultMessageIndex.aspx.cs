/******************************************************************************************
	<Author					: - > Anurag Verma
	<Start Date				: -	> 15/04/2005
	<End Date				: - > 
	<Description			: - > Index file for displaying,searching default messages
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - >
	<Modified By			: - >
	<Purpose				: - >
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


namespace Cms.CmsWeb.Maintenance 
{
	/// <summary>
	/// this class is used to display, search default messages.
	/// </summary>
	public class DefaultMessageIndex : Cms.CmsWeb.cmsbase 
	{
		/*
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
        protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
    */
		protected System.Web.UI.WebControls.Panel pnlGrid;
		protected System.Web.UI.WebControls.Table tblReport;
		protected System.Web.UI.WebControls.Panel pnlReport;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.WebControls.Label capMessage;
        protected System.Web.UI.WebControls.PlaceHolder  plhTabHolder;

		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId	=	"39";
           
			string messageType="S"; //variable to store type of message which will be used to load different pages accordingly
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
				
				
				//Setting web grid control properties
				objWebGrid.WebServiceURL = "../webservices/BaseDataGridWS.asmx?WSDL";
				objWebGrid.SelectClause = "msg_id MSG_ID,msg_type MSG_TYPE,msg_code MSG_CODE,msg_desc MSG_DESC,msg_text MSG_TEXT,case msg_position when 'C' then 'Center' when 'L' then 'Left' when 'R' then 'Right' end MSG_POSITION_GRID,case msg_type when 'S' then case msg_apply_to when 'Y,Y' then 'Client,Agency' when 'Y,N' then 'Client' when 'N,Y' then 'Agency' when 'N,N' then ' ' end when 'L' then case msg_apply_to when 'Y' then 'Default' when 'N' then ' ' end end msg_apply_to_grid,msg_position MSG_POSITION,msg_apply_to MSG_APPLY_TO";
				objWebGrid.FromClause = "mnt_message_list" ;
				objWebGrid.WhereClause = "msg_type='" + messageType  + "'";//"(IS_ACTIVE <> 'N' OR  IS_ACTIVE IS NULL)" ;
				objWebGrid.SearchColumnHeadings = "Code^Description^Applies To^Position";
				objWebGrid.SearchColumnNames = "MSG_CODE^MSG_DESC^MSG_APPLY_TO^MSG_POSITION";
				objWebGrid.SearchColumnType = "T^T^T^T";
				objWebGrid.OrderByClause = "MSG_CODE asc";
				objWebGrid.DisplayColumnNumbers = "3^4^7^6";
				objWebGrid.DisplayColumnNames = "MSG_CODE^MSG_DESC^MSG_APPLY_TO^MSG_POSITION";
				objWebGrid.DisplayColumnHeadings = "Code^Description^Applies To^Position";
				objWebGrid.DisplayTextLength = "217^218^217^218";
				objWebGrid.DisplayColumnPercent = "25^25^25^25";
				objWebGrid.PrimaryColumns = "1";
				objWebGrid.PrimaryColumnsName = "msg_id";
				objWebGrid.ColumnTypes = "B^B^B^B";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5^8^9";
				objWebGrid.SearchMessage = "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				objWebGrid.ExtraButtons = "1^Add New^0^addRecord";
				objWebGrid.PageSize = int.Parse (GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.HeaderString ="Default Message";
				objWebGrid.FilterColumnName = "";
				objWebGrid.FilterValue = "";
				objWebGrid.SelectClass = colors;
				objWebGrid.RequireQuery = "Y";
				//objWebGrid.DefaultSearch="Y";
				objWebGrid.QueryStringColumns = "MSG_ID";
				objWebGrid.DefaultSearch="Y";

				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);
			}
			catch(Exception objExcep)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
			}
			#endregion


			
			#region Window Grid

			/*
			string messageType="S"; //variable to store type of message which will be used to load different pages accordingly


            // Assinging the variable to be used for making the grid
            // Defining the contains the objectTextGrid literal control
            // These contains will generate the HTML required to generated the 
            // grid object  

            objectWindowsGrid.Text = "<OBJECT id=\"gridObject\" classid=\"" + GetWindowsGridUrl() + "\" VIEWASTEXT>"
                + "<PARAM NAME=\"SelectClause\" VALUE=\"msg_id MSG_ID^msg_type MSG_TYPE^msg_code MSG_CODE^msg_desc MSG_DESC^msg_text MSG_TEXT^case msg_position when 'C' then 'Center' when 'L' then 'Left' when 'R' then 'Right' end MSG_POSITION_GRID^case msg_type when 'S' then case msg_apply_to when 'Y,Y' then 'Client,Agency' when 'Y,N' then 'Client' when 'N,Y' then 'Agency' when 'N,N' then ' ' end when 'L' then case msg_apply_to when 'Y' then 'Default' when 'N' then ' ' end end msg_apply_to_grid^msg_position MSG_POSITION^msg_apply_to MSG_APPLY_TO\">"
                + "<PARAM NAME=\"FromClause\" VALUE=\"mnt_message_list\">"
                + "<PARAM NAME=\"WhereClause\" VALUE=\" msg_type='" + messageType  + "'\">"
                + "<PARAM NAME=\"GroupClause\" VALUE=\"\">"
                + "<PARAM NAME=\"SearchColumnNames\" VALUE=\"MSG_CODE^MSG_DESC^MSG_APPLY_TO^MSG_POSITION\">"
                + "<PARAM NAME=\"SearchColumnHeadings\" VALUE=\"Code^Description^Applies To^Position\">"
                + "<PARAM NAME=\"SearchColumnTypes\" VALUE=\"S^S^S^S\">"
                + "<PARAM NAME=\"DisplayColumnNumbers\" VALUE=\"3^4^7^6\">"
                + "<PARAM NAME=\"DisplayColumnHeadings\" VALUE=\"Code^Description^Applies To^Position\">"
                + "<PARAM NAME=\"DisplayTextLength\" VALUE=\"205^215^205^205\">"
                + "<PARAM NAME=\"PageSize\" VALUE=\"" + GetPageSize() + "\">"
                + "<PARAM NAME=\"ColumnTypes\" VALUE=\"S^S^S^S\">"
                + "<PARAM NAME=\"FetchColumns\" VALUE=\"1^2^3^4^5^8^9\">"
                + "<PARAM NAME=\"PrimaryKeyColumns\" VALUE=\"1\">";

                if(messageType=="L")
                    objectWindowsGrid.Text += "<PARAM NAME=\"GridHeaderText\" VALUE=\"Late Notice\">";
                else if(messageType=="S")
                    objectWindowsGrid.Text += "<PARAM NAME=\"GridHeaderText\" VALUE=\"Statement Message\">";

                objectWindowsGrid.Text += "<PARAM NAME=\"CacheSize\" VALUE=\"" + GetCacheSize() + "\">"
                + "<PARAM NAME=\"ColorScheme\" VALUE=\"" + GetWindowsGridColor() + "\">"
                + "<PARAM NAME=\"ExtraButtons\" VALUE=\"1^Add New^0\">"
                + "</OBJECT>";

			*/
			#endregion

            /*************************************************************************/
            ///////////////  LOADING TAB USER CONTROL ///////////////////////////////	
            /************************************************************************/
            #region loading tab control
            	Control lCtrlIFrame = LoadControl("../webcontrols/BaseTabControl.ascx");

            //specifying title heading for different tab    
            if(messageType=="S")            	
            {
                ((BaseTabControl)lCtrlIFrame).TabTitles = "Default Message";			
            }
            else if(messageType=="L")
            {
                ((BaseTabControl)lCtrlIFrame).TabTitles = "Late Notice";			
            }
        
            	//specifying page to be load on different tab
            	((BaseTabControl)lCtrlIFrame).TabURLs = "AddDefaultMessage.aspx?msg_type=" + messageType +"&";
            	//specifying tab length
            	((BaseTabControl)lCtrlIFrame).TabLength=200;
            	//specifying tabcontrol ID
            	((BaseTabControl)lCtrlIFrame).ID="TabCtl";
            	plhTabHolder.Controls.Add(lCtrlIFrame);
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
