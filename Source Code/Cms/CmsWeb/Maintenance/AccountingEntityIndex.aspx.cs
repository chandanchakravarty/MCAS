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
using Cms.CmsWeb.Controls;
using Cms.CmsWeb.WebControls;
using System.Reflection; //sneha
using System.Resources; //sneha
/******************************************************************************************
	<Author					: Gaurav Tyagi- >
	<Start Date				: 14 April,2005-	>
	<End Date				: 15 April,2005- >
	<Description			: This file is being used for loading grid control to show Accounting Entity records- >
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - >
	<Modified By			: - >
	<Purpose				: - >
*******************************************************************************************/

namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// This class is used for showing grid that search and display Accounting Entity records
	/// </summary>
	public class AccountingEntityIndex : Cms.CmsWeb.cmsbase
	{
		/*
		protected System.Web.UI.WebControls.Literal litWindowsGrid;
		protected System.Web.UI.WebControls.Panel pnlGrid;
		protected System.Web.UI.HtmlControls.HtmlForm accountingIndex;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		*/

		protected System.Web.UI.WebControls.Panel pnlGrid;
		protected System.Web.UI.WebControls.Table tblReport;
		protected System.Web.UI.WebControls.Panel pnlReport;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.WebControls.Literal litTextGrid;
		protected System.Web.UI.WebControls.Literal objetTextGrid;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		protected Cms.CmsWeb.WebControls.Menu bottomMenu;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;

		string strEntityType;
		string strEntityName;
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.Label lblTemplate; // Holds value for Entity Type
		string strEntityId;  // Holds value for Entity Id
		 private string strCalledFrom="";
         ResourceManager objResourceMgr = null; //sneha
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			#region setting screen id
			if (Request.QueryString["CALLEDFROM"]!=null && Request.QueryString["CALLEDFROM"].ToString().Trim()!="")
			{
				strCalledFrom = Request.QueryString["CALLEDFROM"].ToString().Trim();	
				
			}
			switch(strCalledFrom)
			{
				case "fin" :
				case "FIN" :
					base.ScreenId	=	"35_1";
					break;
				case "tax" :
				case "TAX" :
					base.ScreenId	=	"36_1";
					break;
				default :
					base.ScreenId	=	"36_1";
					break;
			}
			#endregion
            objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.AccountingEntityIndex", Assembly.GetExecutingAssembly());  //sneha
			strEntityType = Request.Params["EntityType"];
			strEntityId = Request.Params["EntityId"];
			strEntityName = Request.Params["EntityName"];

			//litWindowsGrid.Text = GetObjectTag();
			LoadWebGridControl();

			TabCtl.TabURLs = "AddAccountingEntity.aspx?EntityId="+strEntityId+"&EntityType="+strEntityType + "&EntityName="+strEntityName+"&";
            TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId,"1"); //sneha
		}



		private void LoadWebGridControl()
		{
			
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
			AccountingProperties objProps = GetAccountingProperties();
			Control c1= LoadControl("../webcontrols/BaseDataGrid.ascx");
			try
			{
				/*************************************************************************/
				///////////////  SETTING WEB GRID USER CONTROL PROPERTIES /////////////////
				/************************************************************************/
				//specifying webservice URL
				((BaseDataGrid)c1).WebServiceURL="../webservices/BaseDataGridWS.asmx?WSDL";
				//specifying columns for select query
				((BaseDataGrid)c1).SelectClause ="acList.REC_ID,acList.ENTITY_ID,acList.ENTITY_TYPE,isNull(divList.DIV_NAME,'') as [Division Name],isNull(deptList.DEPT_NAME,'') as [Department Name],isNull(pcList.PC_NAME,'') as [Profit Center],acList.IS_ACTIVE,divList.DIV_ID as Division,deptList.DEPT_ID as DeptId,pcList.PC_ID as ProfitCenterId"+objProps.select;
				//specifying tables for from clause
				((BaseDataGrid)c1).FromClause=" MNT_ACCOUNTING_ENTITY_LIST acList left join MNT_DIV_LIST divList ON acList.Division_Id= divList.DIV_ID left join MNT_DEPT_LIST deptList ON acList.DEPARTMENT_Id= deptList.DEPT_ID left join MNT_PROFIT_CENTER_LIST pcList ON acList.PROFIT_CENTER_Id= pcList.PC_ID"+objProps.from;
				//specifying conditions for where clause
				((BaseDataGrid)c1).WhereClause=" ENTITY_TYPE='"+strEntityType+"' and ENTITY_ID='"+strEntityId+"'";
				//specifying Text to be shown in combo box
				((BaseDataGrid)c1).SearchColumnHeadings=""+objProps.displayHeads+"Division Name^Department Name^Profit Center Name";
				//specifying column to be used for combo box
				((BaseDataGrid)c1).SearchColumnNames=""+objProps.Search+"divList.DIV_NAME^deptList.DEPT_NAME^pcList.PC_NAME";
				//search column data type specifying data type of the column to be used for combo box
				((BaseDataGrid)c1).SearchColumnType="T^T^T^T";
				//specifying column for order by clause
				((BaseDataGrid)c1).OrderByClause="REC_ID asc";
				//specifying column numbers of the query to be displyed in grid
				((BaseDataGrid)c1).DisplayColumnNumbers=""+objProps.displayCols+"4^5^6";
				//specifying column names from the query
				((BaseDataGrid)c1).DisplayColumnNames=""+objProps.displayColumnNames+"Division Name^Department Name^Profit Center";//**********
				//specifying text to be shown as column headings
                ((BaseDataGrid)c1).DisplayColumnHeadings = "" + objProps.displayHeads + objResourceMgr.GetString("DispHeading"); //"Division Name^Department Name^Profit Center Name";
				//specifying column heading display text length
				((BaseDataGrid)c1).DisplayTextLength=""+objProps.displayLength+"225^225^150";
				//specifying width percentage for columns
				((BaseDataGrid)c1).DisplayColumnPercent="25^25^25^25";///*********
				//specifying primary column number
				((BaseDataGrid)c1).PrimaryColumns="1";
				//specifying primary column name
				((BaseDataGrid)c1).PrimaryColumnsName="acList.REC_ID";
				//specifying column type of the data grid
				((BaseDataGrid)c1).ColumnTypes = "B^B^B^B";
				//specifying links pages 
				//((BaseDataGrid)c1).ColumnsLink="client.aspx^webindex.aspx^webindex.aspx^webindex.aspx^webindex.aspx";
				//specifying if double click is allowed or not
				((BaseDataGrid)c1).AllowDBLClick="true"; 
				//specifying which columns are to be displayed on first tab
				((BaseDataGrid)c1).FetchColumns="1^2^3^4^5^6^7^8^9^10^11";
				//specifying message to be shown
                ((BaseDataGrid)c1).SearchMessage = objResourceMgr.GetString("SearchMessage"); //"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				//specifying buttons to be displayed on grid
                ((BaseDataGrid)c1).ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord"; //sneha
				//specifying number of the rows to be shown
				((BaseDataGrid)c1).PageSize=int.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("GRID_PAGE_SIZE"));
				//specifying cache size (use for top clause)
                ((BaseDataGrid)c1).CacheSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("GRID_CACHE_SIZE"));
				//specifying image path
                ((BaseDataGrid)c1).ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
				//
                ((BaseDataGrid)c1).HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				/* commented because it was giving compilation error
				((BaseDataGrid)c1).ExtraUserFeature="3^select user_id,dbo.getpropercasename(user_title,user_fname,user_lname) from mnt_user_list"; 
				*/
				//specifying heading
                ((BaseDataGrid)c1).HeaderString = objResourceMgr.GetString("HeaderString"); //"Accounting Entity Information";//sneha
				((BaseDataGrid)c1).SelectClass = colors;
				//specifying text to be shown for filter checkbox
                ((BaseDataGrid)c1).FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";//sneha
				//specifying column to be used for filtering
				((BaseDataGrid)c1).FilterColumnName="acList.IS_ACTIVE";
				//value of filtering record
				((BaseDataGrid)c1).FilterValue="Y";
				// property indicating whether query string is required or not
				((BaseDataGrid)c1).RequireQuery ="Y";
				// column names to create query string
				((BaseDataGrid)c1).QueryStringColumns ="REC_ID^ENTITY_TYPE";
				((BaseDataGrid)c1).DefaultSearch="Y";
				// to show completed task we have to give check box
				GridHolder.Controls.Add(c1);
			}
			catch
			{}
		
		}


		/*
			#region "Code for making the grid object and passing the properties to it
			
			//Assinging the variable to be used for making the grid

			//Defining the contains the objectTextGrid literal control
			//These contains will generate the HTML required to generated the 
			//grid object
			private string GetObjectTag()
			{
				AccountingProperties objProps = GetAccountingProperties();

			return "<OBJECT id=\"gridObject\" classid=\"" + GetWindowsGridUrl() + "\" VIEWASTEXT>"
				+ "<PARAM NAME=\"SelectClause\" VALUE=\"acList.REC_ID as RowId^acList.ENTITY_ID^acList.ENTITY_TYPE^isNull(divList.DIV_NAME,'') as [Division Name]^isNull(deptList.DEPT_NAME,'') as [Department Name]^isNull(pcList.PC_NAME,'') as [Profit Center]^acList.IS_ACTIVE^divList.DIV_ID as Division^deptList.DEPT_ID as DeptId^pcList.PC_ID as ProfitCenterId"+objProps.select+"\">"
				+ "<PARAM NAME=\"FromClause\" VALUE=\"MNT_ACCOUNTING_ENTITY_LIST acList left join MNT_DIV_LIST divList ON acList.Division_Id= divList.DIV_ID left join MNT_DEPT_LIST deptList ON acList.DEPARTMENT_Id= deptList.DEPT_ID left join MNT_PROFIT_CENTER_LIST pcList ON acList.PROFIT_CENTER_Id= pcList.PC_ID"+objProps.from+" \">"
				+ "<PARAM NAME=\"WhereClause\" VALUE=\" ENTITY_TYPE='"+strEntityType+"' and ENTITY_ID='"+strEntityId+"'\">"// (IS_ACTIVE <> 'N' OR  IS_ACTIVE IS NULL) \">"
				+ "<PARAM NAME=\"GroupClause\" VALUE=\"\">"
				//+ "<PARAM NAME=\"FilterColumnName\" VALUE=\"IS_ACTIVE\">"
				//+ "<PARAM NAME=\"FilterColumnValue\" VALUE=\"'Y'\">"
				//+ "<PARAM NAME=\"FilterLabel\" VALUE=\"Include Inactive\">" 
				//+ "<PARAM NAME=\"ShowExcluded\" VALUE=\"true\">"

//				+ "<PARAM NAME=\"SearchColumnNames\" VALUE=\"acList.ENTITY_TYPE^acList.DIVISION_ID^acList.DEPARTMENT_ID^acList.PROFIT_CENTER_ID\">"
				+ "<PARAM NAME=\"SearchColumnNames\" VALUE=\""+objProps.Search+"acList.DIVISION_ID^acList.DEPARTMENT_ID^acList.PROFIT_CENTER_ID\">"
//				+ "<PARAM NAME=\"SearchColumnHeadings\" VALUE=\"Entity Name^Division Name^Department Name^Profit Center Name\">"
				+ "<PARAM NAME=\"SearchColumnHeadings\" VALUE=\""+objProps.SearchColHeading+"Division Name^Department Name^Profit Center Name\">"
				+ "<PARAM NAME=\"SearchColumnTypes\" VALUE=\"I^S^S^S\">"
				+ "<PARAM NAME=\"DisplayColumnNumbers\" VALUE=\""+objProps.displayCols+"4^5^6\">"
				+ "<PARAM NAME=\"DisplayColumnHeadings\" VALUE=\""+objProps.displayHeads+"Division Name^Department Name^Profit Center Name\">"
				+ "<PARAM NAME=\"DisplayTextLength\" VALUE=\""+objProps.displayLength+"225^225^150\">"
				+ "<PARAM NAME=\"PageSize\" VALUE=\"" + GetPageSize() + "\">"
				+ "<PARAM NAME=\"ColumnTypes\" VALUE=\"S^S^S^S\">"
				+ "<PARAM NAME=\"FetchColumns\" VALUE=\"1^2^3^4^5^6^7^8^9^10^11\">"
				+ "<PARAM NAME=\"PrimaryKeyColumns\" VALUE=\"1\">"
				+ "<PARAM NAME=\"GridHeaderText\" VALUE=\"Accounting Entity Information\">"
				+ "<PARAM NAME=\"CacheSize\" VALUE=\"" + GetCacheSize() + "\">"
				+ "<PARAM NAME=\"ImageColumn\" VALUE=\"\">"
				+ "<PARAM NAME=\"ColorScheme\" VALUE=\"" + GetWindowsGridColor() + "\">"
				+ "<PARAM NAME=\"ExtraButtons\" VALUE=\"1^Add New^0\">"
				+ "</OBJECT>";

			

			
			
		}

		#endregion
		*/

		struct AccountingProperties
		{
			public string select;
			public string from;
			public string where;
			public string Search;
			public string SearchColHeading;
			public string displayCols;
			public string displayHeads;
			public string displayLength;
			public string displayColumnNames;
		}


		private AccountingProperties GetAccountingProperties()
		{
			AccountingProperties objProps = new AccountingProperties();

			if(Request.Params["EntityType"]==null || Request.Params["EntityType"].ToString().Length==0)
				return GetBlankAccountingPropObject();

			switch(Request.Params["EntityType"].ToString().ToUpper())
            {
             
				case "FC": // Finance Company 

					objProps.select = ",fcList.COMPANY_NAME as EntityName";
					objProps.from =" inner join MNT_FINANCE_COMPANY_LIST fcList ON acList.ENTITY_Id= fcList.COMPANY_ID  ";
					objProps.where ="";
					objProps.Search="fcList.COMPANY_NAME^";
                    objProps.SearchColHeading = objResourceMgr.GetString("financeComp"); //"Finance Company Name^";
					objProps.displayCols= "11^";
                    objProps.displayHeads = objResourceMgr.GetString("financeComp"); //"Finance Company Name^";
					objProps.displayLength = "225^";
					objProps.displayColumnNames="EntityName^";
					break;

				case "TAX": // TAX Entity

					objProps.select = ",taxList.TAX_NAME as EntityName";
					objProps.from =" inner join MNT_TAX_ENTITY_LIST taxList ON acList.ENTITY_Id= taxList.TAX_ID ";
					objProps.where ="";
					objProps.Search="taxList.TAX_NAME^";
                    objProps.SearchColHeading = objResourceMgr.GetString("taxEntity"); //"Tax Entity Name^";
					objProps.displayCols= "11^";
                    objProps.displayHeads = objResourceMgr.GetString("taxEntity"); //"Tax Entity Name^";
					objProps.displayLength = "225^";
					objProps.displayColumnNames="EntityName^";
					break;
			}
			return objProps;
		}

		private AccountingProperties GetBlankAccountingPropObject()
		{
			AccountingProperties objProps = new AccountingProperties();
			objProps.select = "";
			objProps.from= "";
			objProps.where= "";
			objProps.displayCols= "";
			objProps.displayHeads= "";
			objProps.displayLength = "";
			objProps.Search="";
			objProps.SearchColHeading = "";
			return objProps;
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
