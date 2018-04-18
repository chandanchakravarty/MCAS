/******************************************************************************************
<Author				: -   Ajit Singh Chahal
<Start Date				: -	4/7/2005 12:18:00 PM
<End Date				: -	
<Description				: - 	Code Behind for Vendor Index.
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
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
using Cms.CmsWeb;
using System.Reflection;
using System.Resources;
namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Class to server as code behind logic for Vendor.
	/// </summary>
	public class VendorIndex : Cms.CmsWeb.cmsbase
	{
		/*
		protected System.Web.UI.WebControls.Table tblReport;
		protected System.Web.UI.WebControls.Panel pnlReport;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		*/
		protected System.Web.UI.WebControls.Panel pnlGrid;
		protected System.Web.UI.WebControls.Table tblReport;
		protected System.Web.UI.WebControls.Panel pnlReport;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAddNew;
        ResourceManager objResourceMgr = null;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;

		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId	=	"32";
            objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.VendorIndex", Assembly.GetExecutingAssembly());	
			#region GETTING BASE COLOR FOR ROW SELECTION
			string colorScheme=GetColorScheme();
			string colors="";

			switch (int.Parse(colorScheme))
			{
				case 1:
					colors =System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR1").ToString();     
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
     
			if(Request["AddNew"]!=null && Request["AddNew"].ToString()!="")
				hidAddNew.Value = Request["AddNew"].ToString();

			#region loading web grid control
			BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			try
			{
				//Setting web grid control properties
				objWebGrid.WebServiceURL = "../webservices/BaseDataGridWS.asmx?WSDL";
				//objWebGrid.SelectClause = "t1.VENDOR_ID,t1.VENDOR_CODE,t1.COMPANY_NAME,t1.VENDOR_FNAME,t1.VENDOR_LNAME,t1.VENDOR_ADD1,t1.VENDOR_ADD2,t1.VENDOR_CITY,t1.VENDOR_COUNTRY,t1.VENDOR_STATE,t1.VENDOR_ZIP,t1.VENDOR_PHONE,t1.VENDOR_EXT,t1.VENDOR_FAX,t1.VENDOR_MOBILE,t1.VENDOR_EMAIL,t1.VENDOR_SALUTATION,t1.VENDOR_FEDERAL_NUM,t1.VENDOR_NOTE,t1.VENDOR_ACC_NUMBER,t1.IS_ACTIVE,t1.MODIFIED_BY,t1.VENDOR_FNAME+ ' '+t1.VENDOR_LNAME as Name,t1.VENDOR_ADD1+' '+t1.VENDOR_ADD2 as Address,t2.STATE_NAME";
                objWebGrid.SelectClause = "t1.VENDOR_ID,t1.VENDOR_CODE,t1.COMPANY_NAME,t1.VENDOR_FNAME,t1.VENDOR_LNAME,ISNULL(t1.CHK_MAIL_ADD1,'')+ case when t1.CHK_MAIL_ADD2 !='' then  IsNull(', '+t1.CHK_MAIL_ADD2,'') else '' end as Address,t1.CHK_MAIL_CITY,t1.CHKCOUNTRY,t1.CHK_MAIL_STATE,t1.CHK_MAIL_ZIP,t1.VENDOR_PHONE,t1.VENDOR_EXT,t1.VENDOR_FAX,t1.VENDOR_MOBILE,t1.VENDOR_EMAIL,t1.VENDOR_SALUTATION,t1.VENDOR_FEDERAL_NUM,t1.VENDOR_NOTE,t1.VENDOR_ACC_NUMBER,t1.IS_ACTIVE,t1.MODIFIED_BY,t1.VENDOR_FNAME+ ' '+t1.VENDOR_LNAME as Name,isnull(t1.CHK_MAIL_ADD1,'')+' '+isnull(t1.CHK_MAIL_ADD2,'') as Address,t2.STATE_NAME,t1.SUSEP_NUM";
                objWebGrid.FromClause = "MNT_VENDOR_LIST t1 left outer join MNT_COUNTRY_STATE_LIST t2 on t1.CHK_MAIL_STATE=t2.STATE_ID";
				//objWebGrid.WhereClause = " ";//"(IS_ACTIVE <> 'N' OR  IS_ACTIVE IS NULL)" ;
                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");// "Vendor Code^Contact First Name^Contact Last Name^Company Name^SUSEP Number";
                objWebGrid.SearchColumnNames = "t1.VENDOR_CODE^t1.VENDOR_FNAME^t1.VENDOR_LNAME^t1.COMPANY_NAME^t1.SUSEP_NUM";
				objWebGrid.SearchColumnType = "T^T^T^T^T";
				objWebGrid.OrderByClause = "VENDOR_CODE asc";
				
//				objWebGrid.DisplayColumnNumbers = "2^3^22^23^7^24^10^11^13";
//				objWebGrid.DisplayColumnNames = "VENDOR_CODE^COMPANY_NAME^Name^Address^CHK_MAIL_CITY^CHK_MAIL_STATE^CHK_MAIL_ZIP^VENDOR_PHONE^VENDOR_FAX";
//				objWebGrid.DisplayColumnHeadings = "Vendor Code^Company Name^Name^Address^City^State^Zip^Phone^Fax";
//				objWebGrid.DisplayTextLength = "120^70^100^200^100^100^50^100^90";
//				objWebGrid.DisplayColumnPercent = "11^8^11^20^10^10^10^11^9";
				
				objWebGrid.DisplayColumnNumbers = "2^3^23^7^24^10^11^13^26";
                objWebGrid.DisplayColumnNames = "VENDOR_CODE^COMPANY_NAME^Address^CHK_MAIL_CITY^STATE_NAME^CHK_MAIL_ZIP^VENDOR_PHONE^VENDOR_FAX^SUSEP_NUM";
                //objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Vendor Code^Company Name^Address^City^State^Zip^Phone^Fax^SUSEP Number";
                objWebGrid.DisplayColumnHeadings = "Vendor Code^Company Name^Address^City^State^Postal code^Phone^Fax^SUSEP Number";
				objWebGrid.DisplayTextLength = "120^70^100^200^100^100^50^100^20";
				objWebGrid.DisplayColumnPercent = "11^8^11^18^10^8^13^13^8";

				objWebGrid.PrimaryColumns = "1";
				objWebGrid.PrimaryColumnsName = "t1.VENDOR_ID";
				objWebGrid.ColumnTypes = "B^B^B^B^B^B^B^B^B";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10^11^12^13^14^15^16^17^18^19^20^26";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";
				objWebGrid.PageSize = int.Parse (GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Vendor Information";
				//objWebGrid.FilterColumnName = "";
				//objWebGrid.FilterValue = "";
				objWebGrid.SelectClass = colors;
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch="Y";
				objWebGrid.QueryStringColumns = "VENDOR_ID";

				//specifying text to be shown for filter checkbox
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";
				//specifying column to be used for filtering
				objWebGrid.FilterColumnName="t1.IS_ACTIVE";
				//value of filtering record
				objWebGrid.FilterValue="Y";

				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);
			}
			catch(Exception objExcep)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
			}
			#endregion
            TabCtl.TabURLs = "AddVendor.aspx??&";
            TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
            TabCtl.TabLength = 150;

			#region Window Grid
			/*
			litTextGrid.Text = "<OBJECT id=\"gridObject\" classid=\"" + GetWindowsGridUrl() + "\" VIEWASTEXT>"
				+ "<PARAM NAME=\"SelectClause\" VALUE=\"t1.VENDOR_ID^t1.VENDOR_CODE^t1.VENDOR_FNAME^t1.VENDOR_LNAME^t1.VENDOR_ADD1^t1.VENDOR_ADD2^t1.VENDOR_CITY^t1.VENDOR_COUNTRY^t1.VENDOR_STATE^t1.VENDOR_ZIP^t1.VENDOR_PHONE^t1.VENDOR_EXT^t1.VENDOR_FAX^t1.VENDOR_MOBILE^t1.VENDOR_EMAIL^t1.VENDOR_SALUTATION^t1.VENDOR_FEDERAL_NUM^t1.VENDOR_NOTE^t1.VENDOR_ACC_NUMBER^t1.IS_ACTIVE^t1.MODIFIED_BY^t1.VENDOR_FNAME+ ' '+t1.VENDOR_LNAME as Name^t1.VENDOR_ADD1+' '+t1.VENDOR_ADD2 as Address^t2.STATE_NAME\">"
				//Name22,Address23,statename24,
				+ "<PARAM NAME=\"FromClause\" VALUE=\"MNT_VENDOR_LIST t1,MNT_COUNTRY_STATE_LIST t2\">"
				+ "<PARAM NAME=\"WhereClause\" VALUE=\"t1.VENDOR_STATE=t2.STATE_ID\">"
				+ "<PARAM NAME=\"GroupClause\" VALUE=\"\">"
				+ "<PARAM NAME=\"SearchColumnNames\" VALUE=\"t1.VENDOR_CODE^t1.VENDOR_FNAME^t1.VENDOR_LNAME\">"
				+ "<PARAM NAME=\"SearchColumnHeadings\" VALUE=\"Vendor Code^First Name^Last Name\">"
				+ "<PARAM NAME=\"SearchColumnTypes\" VALUE=\"S^S^S\">"
				+ "<PARAM NAME=\"DisplayColumnNumbers\" VALUE=\"2^22^23^7^24^10^11^13\">"
				+ "<PARAM NAME=\"DisplayColumnHeadings\" VALUE=\"Vendor Code^Name^Address^City^State^Zip^Phone^Fax\">"
				+ "<PARAM NAME=\"DisplayTextLength\" VALUE=\"120^150^200^100^100^50^120^90\">"
				+ "<PARAM NAME=\"PageSize\" VALUE=\"" + GetPageSize() + "\">"
				+ "<PARAM NAME=\"ColumnTypes\" VALUE=\"S^S^S\">"
				+ "<PARAM NAME=\"FetchColumns\" VALUE=\"1^2^3^4^5^6^7^8^9^10^11^12^13^14^15^16^17^18^19^20^21\">"
				+ "<PARAM NAME=\"PrimaryKeyColumns\" VALUE=\"1\">"
				+ "<PARAM NAME=\"GridHeaderText\" VALUE=\"Vendor Information\">"
				+ "<PARAM NAME=\"CacheSize\" VALUE=\"" + GetCacheSize() + "\">"
				+ "<PARAM NAME=\"ColorScheme\" VALUE=\"" + GetWindowsGridColor() + "\">"
				+ "<PARAM NAME=\"ExtraButtons\" VALUE=\"1^Add New^0\">"
				+ "</OBJECT>";
				*/
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