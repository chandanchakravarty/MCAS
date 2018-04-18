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
using Cms.BusinessLayer.BlClient;
using System.Resources;
using System.Reflection;

namespace Cms.CmsWeb.DocumentMerge
{
	/// <summary>
	/// Summary description for SendDocumentIndex.
	/// </summary>
	public class SendDocumentIndex : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.HtmlControls.HtmlForm SEND_DOCUMENT;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		public string strCalledFrom="";
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFor;
		protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
        ResourceManager objResourceMgr = null;
		private void Page_Load(object sender, System.EventArgs e)
		{
            // Put user code to initialize the page here
			//if(GetCalledFor()!="CLAIM")
            if (Request.QueryString["CalledFor"].ToUpper().ToString() == "CLAIM" || Request.QueryString["CalledFor"].ToUpper().ToString() == "POLICY")
				base.ScreenId = "350";

                // for itrack no 1161
            else if (Request["CalledFor"].ToString() == "MAINTENANCE")
            {
                base.ScreenId = "350";
            }
            else
                base.ScreenId = "316";
            objResourceMgr = new ResourceManager("Cms.CmsWeb.DocumentMerge.SendDocumentIndex", Assembly.GetExecutingAssembly());
			// Put user code to initialize the page here
			// Assinging the variable to be used for making the grid
			// Defining the contains the objectTextGrid literal control

			// These contains will generate the HTML required to generated the 
			// grid object
			//			if(Request["CalledFor"]!=null)			
			//				strCalledFrom = Request["CalledFor"]+"";
			//			this.tabLayer.Attributes["src"] = "SendDocument.aspx?CalledFor="+ strCalledFrom;
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

			int customer_ID=GetCustomerID()=="" ?0 : int.Parse(GetCustomerID());

			//			Modified by Praveen Kumar on 10-12-08

			
				
			if (Request["CalledFrom"] != null && Request["CalledFrom"].ToString() == "CLAIMS")
			{
				if(customer_ID!=0)
				{
					//		----------------------------P
					cltClientTop.CustomerID = customer_ID;
					cltClientTop.Visible = true;
					cltClientTop.ShowHeaderBand ="Client";
					#region loading web grid control
					BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("../../Cmsweb/webcontrols/BaseDataGrid.ascx");
					try
					{
						//Setting web grid control properties
						objWebGrid.WebServiceURL = "../../Cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
						
						objWebGrid.SelectClause = "DOC.DISPLAYNAME DISPLAYNAME,L.CLIENT_ID CLIENT_ID,L.MERGE_ID AS MERGE_ID,DOC.LOB,DOC.AGENCY_ID,L.TEMPLATE_ID,DOC.LETTERTYPE,ISNULL(LM.LOB_DESC,'') LOB_DESC,L.MERGE_DATE";
						objWebGrid.FromClause = "DOC_SEND_LETTER  L INNER JOIN DOC_TEMPLATE_LIST DOC ON DOC.TEMPLATE_ID = L.TEMPLATE_ID LEFT OUTER JOIN MNT_LOB_MASTER LM ON LM.LOB_ID=DOC.LOB";	

						if(customer_ID!=0)
							objWebGrid.WhereClause = "L.CLIENT_ID=" + customer_ID + "and isnull(L.TEMPLATE_PATH,'') <> ''";
						else
							objWebGrid.WhereClause = "ISNULL(L.TEMPLATE_PATH,'') <> ''";

						objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//Template Name^Line of Business^Merge Date";
						objWebGrid.SearchColumnNames = "DISPLAYNAME^DOC.LOB^L.MERGE_DATE";
						objWebGrid.SearchColumnType = "T^L^D";
						objWebGrid.DropDownColumns  =	   "^LOB^";
						objWebGrid.OrderByClause = "DISPLAYNAME asc";
						objWebGrid.DisplayColumnNumbers = "1^2^3";
                        objWebGrid.DisplayColumnNames = "DISPLAYNAME^LOB_DESC^MERGE_DATE";
                        objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Template Name^Line of Business^Merge Date";
						objWebGrid.DisplayTextLength = "30^30^50";
						objWebGrid.DisplayColumnPercent = "20^20^40";
						objWebGrid.PrimaryColumns = "1";
						objWebGrid.PrimaryColumnsName = "L.TEMPLATE_ID";
						objWebGrid.ColumnTypes = "B^B^B";
						objWebGrid.AllowDBLClick = "true";
						objWebGrid.FetchColumns = "1^2^3";
                        objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
						//objWebGrid.ExtraButtons = "2^Add New~Single Combined List^0~1";
                        objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//1^Add New^0^addRecord"
						objWebGrid.PageSize = int.Parse (GetPageSize());
						objWebGrid.CacheSize = int.Parse(GetCacheSize());
                        objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                        objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
						objWebGrid.HeaderString =objResourceMgr.GetString("HeaderString");//Letters

						objWebGrid.SelectClass = colors;
						objWebGrid.RequireQuery = "Y";
						objWebGrid.QueryStringColumns = "TEMPLATE_ID^LETTERTYPE^MERGE_ID";
						objWebGrid.DefaultSearch="Y";
						//Adding to controls to gridholder
						GridHolder.Controls.Add(objWebGrid);
						//TabCtl.TabURLs = "SendDocument.aspx?CalledFor="+ Request["CalledFor"] + "&"; 
					}
					catch(Exception objExcep)
					{
						Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
					}
					#endregion
				}
					//         ----------------------------P
				else
				{
					cltClientTop.Visible = false;
					capMessage.Text=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("115");       
					capMessage.Visible=true; 
				}
			}
		
			else
			{
				#region loading web grid control
				BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("../../Cmsweb/webcontrols/BaseDataGrid.ascx");
				try
				{
					//Setting web grid control properties
					objWebGrid.WebServiceURL = "../../Cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
						
					objWebGrid.SelectClause = "DOC.DISPLAYNAME DISPLAYNAME,L.CLIENT_ID CLIENT_ID,L.MERGE_ID AS MERGE_ID,DOC.LOB,DOC.AGENCY_ID,L.TEMPLATE_ID,DOC.LETTERTYPE,ISNULL(LM.LOB_DESC,'') LOB_DESC,L.MERGE_DATE";
					objWebGrid.FromClause = "DOC_SEND_LETTER  L INNER JOIN DOC_TEMPLATE_LIST DOC ON DOC.TEMPLATE_ID = L.TEMPLATE_ID LEFT OUTER JOIN MNT_LOB_MASTER LM ON LM.LOB_ID=DOC.LOB";	

					if(customer_ID!=0)
						objWebGrid.WhereClause = "L.CLIENT_ID=" + customer_ID + "and isnull(L.TEMPLATE_PATH,'') <> ''";
					else
						objWebGrid.WhereClause = "ISNULL(L.TEMPLATE_PATH,'') <> ''";

                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings1");//"Template Name^Line of Business^Merge Date";
					objWebGrid.SearchColumnNames = "DISPLAYNAME^DOC.LOB^L.MERGE_DATE";
					objWebGrid.SearchColumnType = "T^L^D";
					objWebGrid.DropDownColumns  =	   "^LOB^";
					objWebGrid.OrderByClause = "DISPLAYNAME asc";
					objWebGrid.DisplayColumnNumbers = "1^2^3";
					objWebGrid.DisplayColumnNames = "DISPLAYNAME^LOB_DESC^MERGE_DATE";
					objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings1");//Template Name^Line of Business^Merge Date";
					objWebGrid.DisplayTextLength = "30^30^50";
					objWebGrid.DisplayColumnPercent = "20^20^40";
					objWebGrid.PrimaryColumns = "1";
					objWebGrid.PrimaryColumnsName = "L.TEMPLATE_ID";
					objWebGrid.ColumnTypes = "B^B^B";
					objWebGrid.AllowDBLClick = "true";
					objWebGrid.FetchColumns = "1^2^3";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");// "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
					//objWebGrid.ExtraButtons = "2^Add New~Single Combined List^0~1";
                    objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//1^Add New^0^addRecord";
					objWebGrid.PageSize = int.Parse (GetPageSize());
					objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");// Letters

					objWebGrid.SelectClass = colors;
					objWebGrid.RequireQuery = "Y";
					objWebGrid.QueryStringColumns = "TEMPLATE_ID^LETTERTYPE^MERGE_ID";
					objWebGrid.DefaultSearch="Y";
					//Adding to controls to gridholder
					GridHolder.Controls.Add(objWebGrid);
					TabCtl.TabURLs = "SendDocument.aspx?CalledFor="+ Request["CalledFor"] + "&";
                    TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");

				}
				catch(Exception objExcep)
				{
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
				}
				#endregion
			}
		}
		//      ----------------------------------------END PRAVEEN KUMAR		
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
