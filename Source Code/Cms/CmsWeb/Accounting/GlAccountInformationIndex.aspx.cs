/******************************************************************************************
<Author					: -   Ajit Singh Chahal
<Start Date				: -	  4/28/2005 9:06:31 PM
<End Date				: -	
<Description			: -   Code behind for pkg Lob details Index.
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
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
		using System.Xml; 
		using System.IO;
		using Cms.CmsWeb.WebControls;
using System.Reflection;
using System.Resources;


		namespace Cms.CmsWeb.Maintenance.Accounting
				  {

		/// <summary>
		/// Code behind for define sub ranges Index.
		/// </summary>
		public class GlAccountInformationIndex : Cms.CmsWeb.cmsbase
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
            ResourceManager objResourceMgr = null;
			private int CacheSize = 1400;

			private void Page_Load(object sender, System.EventArgs e)
			{
				base.ScreenId	=	"125_1";
				TabCtl.TabURLs = "AddGlAccountInformation.aspx?GL_ID="+Session["GL_ID"]+"&";
                objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.Accounting.GlAccountInformationIndex", Assembly.GetExecutingAssembly());
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
				#region loading web grid control
				BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
				try
				{
					
					//Setting web grid control properties
					objWebGrid.WebServiceURL =  httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";;
					objWebGrid.SelectClause =   "t1.GL_ID,t1.ACCOUNT_ID,t1.ACC_TYPE_ID,";

					//objWebGrid.SelectClause +=  " case t1.ACC_LEVEL_TYPE when 'AS' then 'Account/Sub-Account' when 'Head' then 'Heading' else t1.ACC_LEVEL_TYPE end as ACC_LEVEL_TYPE,";

                    objWebGrid.SelectClause += " mlv.LOOKUP_VALUE_DESC as ACC_LEVEL_TYPE,";
					objWebGrid.SelectClause +=  " case when t1.acc_parent_id is null then t1.ACC_DESCRIPTION  else isnull(t3.acc_description,'') + ' - ' + t1.ACC_DESCRIPTION end as ACC_DESCRIPTION,";
					objWebGrid.SelectClause +=	" case t1.ACC_CASH_ACC_TYPE when 'C' then 'Checking' When 'S' then 'Saving' else '-' end as  ACC_CASH_ACC_TYPE,t1.IS_ACTIVE,case t1.GROUP_TYPE when 0 then '-' else t2.ACC_TYPE_DESC end as ACC_TYPE_DESC,t1.ACC_DISP_NUMBER as ACC_NUMBER";
					//objWebGrid.FromClause = "ACT_GL_ACCOUNTS t1  inner join  ACT_TYPE_MASTER t2 on t1.ACC_TYPE_ID = t2.ACC_TYPE_ID and t1.GL_ID=1  LEFT OUTER JOIN  ACT_GL_ACCOUNTS t3 ON t3.account_id = t1.acc_parent_id ";
                    objWebGrid.FromClause = "ACT_GL_ACCOUNTS t1 INNER JOIN MNT_LOOKUP_VALUES mlv on cast(mlv.LOOKUP_UNIQUE_ID as NCHAR)=t1.ACC_LEVEL_TYPE inner join  ACT_TYPE_MASTER t2 on t1.ACC_TYPE_ID = t2.ACC_TYPE_ID and t1.GL_ID=1  LEFT OUTER JOIN  ACT_GL_ACCOUNTS t3 ON t3.account_id = t1.acc_parent_id ";
                    //objWebGrid.WhereClause = " t1.ACC_TYPE_ID = t2.ACC_TYPE_ID and t1.GL_ID="+Session["GL_ID"];
                    objWebGrid.SearchColumnHeadings =objResourceMgr.GetString("SearchColumnHeadings");//"Account Number^Description^Level Type^Sub Type^Cash Type";
					objWebGrid.SearchColumnNames = "t1.ACC_DISP_NUMBER^t1.ACC_DESCRIPTION^case t1.ACC_LEVEL_TYPE when 'AS' then 'Account/Sub-Account' when 'Head' then 'Heading' else t1.ACC_LEVEL_TYPE end^t2.ACC_TYPE_DESC^t1.ACC_CASH_ACC_TYPE";				
					objWebGrid.SearchColumnType = "T^T^T^T^T";
					// changing the order by clause may lead to some problems like - iTrack #2551
					// (Problem is due to non availability of account_id in querystring)
					objWebGrid.OrderByClause = "t1.ACCOUNT_ID desc";
					objWebGrid.DisplayColumnNumbers = "9^5^4^8^6";
					objWebGrid.DisplayColumnNames = "ACC_NUMBER^ACC_DESCRIPTION^ACC_LEVEL_TYPE^ACC_TYPE_DESC^ACC_CASH_ACC_TYPE";
                    objWebGrid.DisplayColumnHeadings =objResourceMgr.GetString("DisplayColumnHeadings");//"Account #^Description^Level Type^Sub Type^Cash Type";
					objWebGrid.DisplayTextLength = "100^100^100^100^100";
					objWebGrid.DisplayColumnPercent = "15^40^15^15^15";
					objWebGrid.PrimaryColumns = "2";
					objWebGrid.PrimaryColumnsName = "t1.ACCOUNT_ID";
					objWebGrid.ColumnTypes = "B^B^B^B^B";
					objWebGrid.AllowDBLClick = "true";
					objWebGrid.FetchColumns = "1^2^3^4^5";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                    objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";
					//specifying number of the rows to be shown
					objWebGrid.PageSize = int.Parse(GetPageSize());
					objWebGrid.CacheSize = CacheSize;
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"GL Accounts";
					objWebGrid.SelectClass = colors;
					objWebGrid.RequireQuery = "Y";
					objWebGrid.QueryStringColumns = "ACCOUNT_ID";
					objWebGrid.DefaultSearch = "Y";
					//specifying text to be shown for filter checkbox
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";
					//specifying column to be used for filtering
					objWebGrid.FilterColumnName="t1.IS_ACTIVE";
					//value of filtering record
					objWebGrid.FilterValue="Y";
					//Adding to controls to gridholder
					GridHolder.Controls.Add(objWebGrid);
				}
				catch(Exception ex)
				{
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				}
                TabCtl.TabURLs = "AddGlAccountInformation.aspx??&";
                TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
                //TabCtl.TabTitles ="MCCA Attachment";
                TabCtl.TabLength = 150;
				#endregion
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