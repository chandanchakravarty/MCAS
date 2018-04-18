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
namespace Cms.Client.Aspx
{
	/// <summary>
	/// Summary description for FaxIndex.
	/// </summary>
	public class FaxIndex : Cms.Client.clientbase
	{
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Hidden1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Hidden2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidlocQueryStr;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Hidden3;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        ResourceManager objResourceMgr = null;
		private void Page_Load(object sender, System.EventArgs e)
		{
            
			if(GetCalledFor()!="CLAIM")
				base.ScreenId ="109";
			else
				base.ScreenId ="315";
            objResourceMgr = new ResourceManager("Cms.client.Aspx.FaxIndex", Assembly.GetExecutingAssembly());
			#region GETTING BASE COLOR FOR ROW SELECTION
			string colorScheme=GetColorScheme();
			string colors="";

			switch (int.Parse(colorScheme))
			{
				case 1:
					colors =System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR1").ToString();     
					break;
				case 2:
					colors=System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR2").ToString();     
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

          
			//if(GetCustomerID())
			int customer_ID=GetCustomerID()=="" ?0 : int.Parse(GetCustomerID());
			
			if(customer_ID!=0)
			{
				cltClientTop.CustomerID = customer_ID;
				cltClientTop.Visible = true;
				cltClientTop.ShowHeaderBand ="Client";
			}
			else
			{
				cltClientTop.Visible = false;
			}

			if(customer_ID!=0)
			{
         
			
				#region loading web grid control
				BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("../../Cmsweb/webcontrols/BaseDataGrid.ascx");
				try
				{
					//Setting web grid control properties
					objWebGrid.WebServiceURL = "../../Cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
					
					objWebGrid.SelectClause = "t1.FAX_FROM_NAME FaxFrom,t1.FAX_TO FaxTo,t1.FAX_SUBJECT Faxsubject,t1.FAX_ROW_ID,t1.CUSTOMER_ID";
					objWebGrid.FromClause = "CLT_CUSTOMER_FAX t1 ";					
					objWebGrid.WhereClause = "t1.CUSTOMER_ID=" + customer_ID ;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//Sender Name^Send To^Subject
					objWebGrid.SearchColumnNames = "t1.FAX_FROM_NAME^t1.FAX_TO^t1.FAX_SUBJECT";
					objWebGrid.SearchColumnType = "T^T^T";
					objWebGrid.OrderByClause = "FaxFrom asc";
					objWebGrid.DisplayColumnNumbers = "1^2^3";
					objWebGrid.DisplayColumnNames = "FaxFrom^FaxTo^Faxsubject";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//Sender Name^Send To^Subject
					objWebGrid.DisplayTextLength = "30^30^50";
					objWebGrid.DisplayColumnPercent = "20^20^40";
					objWebGrid.PrimaryColumns = "1";
					objWebGrid.PrimaryColumnsName = "FAX_ROW_ID";
					objWebGrid.ColumnTypes = "B^B^B";
					objWebGrid.AllowDBLClick = "true";
					objWebGrid.FetchColumns = "1^2^3";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button
					//objWebGrid.ExtraButtons = "2^Add New~Single Combined List^0~1";
                    objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons"); //1^Add New^0^addRecord
					objWebGrid.PageSize = int.Parse (GetPageSize());
					objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Fax" ;

					objWebGrid.SelectClass = colors;
					objWebGrid.RequireQuery = "Y";
					objWebGrid.QueryStringColumns = "FAX_ROW_ID^CUSTOMER_ID";
					objWebGrid.DefaultSearch="Y";
					//Adding to controls to gridholder
					GridHolder.Controls.Add(objWebGrid);

					if(Request["CalledFor"]!=null && Request["CalledFor"]!="")
						TabCtl.TabURLs = "Fax.aspx?CalledFor="+ Request["CalledFor"] + "&"; 
				}
				catch(Exception objExcep)
				{
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
				}
				#endregion
			}
			else
			{
				capMessage.Text=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("115");       
				capMessage.Visible=true; 
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