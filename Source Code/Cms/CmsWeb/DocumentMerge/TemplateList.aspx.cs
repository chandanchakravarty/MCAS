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

namespace Cms.CmsWeb.DocumentMerge
{
	/// <Created By>Deepak Gupta</Created>
	/// <Dated>SEP-08-2006</Dated>
	/// <Purpose>It will show the complete list of Template stored in wolverine system.</Purpose>
	public class TemplateList : cmsbase
	{
		protected Cms.CmsWeb.WebControls.Menu bottomMenu;
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected string strCalledFrom="";
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlTableRow tabCtlRow;
		public string strCssClass="";
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        ResourceManager objResourceMgr = null;

		private void Page_Load(object sender, System.EventArgs e)
		{
			//Setting the cookie value
			SetCookieValue();
	
			//Need TO change this id.
			base.ScreenId ="349";
			strCssClass="tableWidth";
			bottomMenu.Visible = true;
            objResourceMgr = new ResourceManager("Cms.CmsWeb.DocumentMerge.TemplateList", Assembly.GetExecutingAssembly());

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
			
			Control c1 = LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			BaseDataGrid objWebGrid;
			objWebGrid = (BaseDataGrid)c1;

			try
			{
				//Setting web grid control properties
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				//In the following code added TL.IS_ACTIVE for #1237 as the deactivated row was not changing color. Done by SHAILJA on 03/14/2007
                objWebGrid.SelectClause = "TL.TEMPLATE_ID,TL.VERSION,TL.LOB,TL.DISPLAYNAME,TL.[DESCRIPTION],ISNULL(mlmm.LOB_DESC,LM.LOB_DESC) as LOB_DESC,ISNULL(AL.AGENCY_DISPLAY_NAME,'') AGENCY_DISPLAY_NAME,UL.USER_FNAME + ' ' + UL.USER_LNAME [USER_NAME],isnull(mlvm.LOOKUP_VALUE_DESC,LV.LOOKUP_VALUE_DESC) as LOOKUP_VALUE_DESC ,TL.IS_ACTIVE";
                objWebGrid.FromClause = "DOC_TEMPLATE_LIST TL LEFT OUTER JOIN MNT_LOB_MASTER LM ON LM.LOB_ID=TL.LOB LEFT OUTER JOIN MNT_AGENCY_LIST AL ON AL.AGENCY_ID=TL.AGENCY_ID INNER JOIN MNT_USER_LIST UL ON UL.USER_ID=TL.CREATED_BY INNER JOIN MNT_LOOKUP_VALUES LV ON LV.LOOKUP_UNIQUE_ID=TL.LETTERTYPE LEFT OUTER JOIN MNT_LOB_MASTER_MULTILINGUAL mlmm on mlmm.LOB_ID=LM.LOB_ID and mlmm.LANG_ID="+GetLanguageID()+" left outer join MNT_LOOKUP_VALUES_MULTILINGUAL mlvm on mlvm.LOOKUP_UNIQUE_ID=LV.LOOKUP_UNIQUE_ID and mlvm.LANG_ID ="+GetLanguageID();
				objWebGrid.WhereClause = "";
				
				//SEARCH RELATED
                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Template Name^Template Description^Product^Agency Name^Created By^Letter Type";//Line of Business				
				objWebGrid.SearchColumnNames =	  "TL.DISPLAYNAME^TL.[DESCRIPTION]^TL.LOB^AL.AGENCY_DISPLAY_NAME^UL.USER_FNAME ! ' ' ! UL.USER_LNAME^LV.LOOKUP_VALUE_DESC";
				objWebGrid.SearchColumnType =	  "T^T^L^T^T^T";
				objWebGrid.DropDownColumns  =	   "^^LOB^^^";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				objWebGrid.DefaultSearch = "Y";

				objWebGrid.DisplayColumnNames = "DISPLAYNAME^VERSION^DESCRIPTION^LOB_DESC^AGENCY_DISPLAY_NAME^USER_NAME^LOOKUP_VALUE_DESC";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Template Name^Version^Template Description^Product^Agency Name^Created By^Letter Type";//Line of Business
				
				objWebGrid.OrderByClause = "DISPLAYNAME ASC";				
				objWebGrid.DisplayColumnNumbers = "4^2^5^6^7^8^9";
				objWebGrid.DisplayTextLength = "50^10^200^30^40^40^10";
				objWebGrid.DisplayColumnPercent = "17^5^32^13^13^10^10";
				objWebGrid.PrimaryColumns = "1";
				objWebGrid.PrimaryColumnsName = "TEMPLATE_ID";

				objWebGrid.ColumnTypes = "B^B^B^B^B^B^B";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5^6^7";
				
				objWebGrid.SelectClass = colors;

                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";
				objWebGrid.FilterValue = "Y";
				objWebGrid.FilterColumnName = "TL.IS_ACTIVE";

                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";				
				//objWebGrid.ExtraButtons = "2^Add New^0^addRecord";
				objWebGrid.PageSize = int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Templates List";
				
				objWebGrid.RequireQuery = "Y";				
				objWebGrid.QueryStringColumns = "TEMPLATE_ID";
				
				//Adding to controls to gridholder
				GridHolder.Controls.Add(c1);
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			#endregion
            /*============================================================================================
             * SANTOSH GAUTAM : BELOW LINE IS MODIFIED ON 29 OCT 2010
             * 1. OLD VALUE => TabCtl.TabURLs = "TemplateInfo.aspx??";
            *===========================================================================================*/
            TabCtl.TabURLs = "TemplateInfo.aspx?";
            TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
		}
		
		private void SetCookieValue ()
		{
			Response.Cookies["LastVisitedTab"].Value = "0";
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