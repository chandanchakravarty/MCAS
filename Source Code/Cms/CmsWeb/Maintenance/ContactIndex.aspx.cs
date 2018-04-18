/******************************************************************************************
<Author				: -   Ajit Singh Chahal
<Start Date				: -	3/11/2005 7:05:13 PM
<End Date				: -	
<Description				: - 	Code behind to contact  manager index- windows grid.
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
using Cms.CmsWeb.Controls;
using System.Reflection;
using System.Resources;
namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Code behind to contact  manager index- windows grid.
	/// </summary>
	public class ContactIndex : Cms.CmsWeb.cmsbase
	{
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
		protected string tableCssClass="tableWidth"; //variable to hold css class name if page is opened directly or from other entity.
		//default css class is 'tableWidth' i.e. if opened directly not in reference to an other entity
        ResourceManager objResourceMgr = null;
        private string strCalledFrom = "";
		private void Page_Load(object sender, System.EventArgs e)
		{


            #region setting screen id
            if (Request.QueryString["CALLEDFROM"] != null && Request.QueryString["CALLEDFROM"].ToString().Trim() != "")
            {
                strCalledFrom = Request.QueryString["CALLEDFROM"].ToString().Trim();

            }
            switch (strCalledFrom)
            {              
                                  
                case "CUSTOMER":
                    base.ScreenId = "134_5";
                    break;
                
                default:
                    base.ScreenId = "38";
                    break;
            }
            #endregion

         
			//base.ScreenId	=	"38";
			//litTextGrid.Text = GetObjectTag();
            objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.ContactIndex", Assembly.GetExecutingAssembly());
			LoadWebGridControl();
			if(Request.QueryString["CONTACT_TYPE_ID"]!=null && Request.QueryString["CONTACT_TYPE_ID"].ToString().Length>0)
			{
				tableCssClass="tableWidthHeader";
			}

            TabCtl.TabURLs = "AddContact.aspx?CONTACTTYPEID=" + Request.QueryString["CONTACT_TYPE_ID"] + "&EntityId=" + Request.QueryString["EntityId"] + "&CallFrom=" + strCalledFrom + "&";
			//TabCtl.TabURLs = "AddContact.aspx?EntityId="+Request.QueryString["EntityId"]+"&";
			if(Request.QueryString.Get("CONTACT_TYPE_ID")==null || Request.QueryString.Get("CONTACT_TYPE_ID").ToString().Length==0)
			{
				Response.Write("<script>var defaultMode=true;</script>");
			}
			else
			{
				Response.Write("<script>var defaultMode=false;</script>");
				bottomMenu.Visible = false;
			}
            
		}
		private string GetObjectTag()
		{
			/*ContactProperties objProps = GetContactProperties();
			return "<OBJECT id=\"gridObject\" classid=\"" + GetWindowsGridUrl() + "\" VIEWASTEXT>"
				+ "<PARAM NAME=\"SelectClause\" VALUE=\"cList.CONTACT_ID^cList.CONTACT_CODE^cList.CONTACT_FNAME+' '+cList.CONTACT_LNAME as 'Contact Name'^cList.CONTACT_ADD1^cList.CONTACT_BUSINESS_PHONE^cList.CONTACT_FAX^cList.CONTACT_MOBILE^cList.CONTACT_LNAME^cList.CONTACT_FNAME^cList.CONTACT_MNAME^cList.CONTACT_ADD2^cList.CONTACT_CITY^cList.CONTACT_STATE^cList.CONTACT_ZIP^cList.CONTACT_COUNTRY^cList.CONTACT_EXT^cList.CONTACT_EMAIL^cList.CONTACT_PAGER^cList.CONTACT_HOME_PHONE^cList.CONTACT_TOLL_FREE^cList.CONTACT_NOTE^cList.IS_ACTIVE^cList.CONTACT_SALUTATION^CONTACT_POS^cList.CONTACT_TYPE_ID^cList.INDIVIDUAL_CONTACT_ID^cList.MODIFIED_BY^cList.CONTACT_ADD1+' '+cList.CONTACT_ADD2 as Address,cList.CONTACT_EXISTENCE_AS^cTypes.CONTACT_TYPE_DESC"+objProps.select+"\">"
				+ "<PARAM NAME=\"FromClause\" VALUE=\"MNT_CONTACT_LIST cList, MNT_CONTACT_TYPES cTypes"+objProps.from+"\">"
				+ "<PARAM NAME=\"WhereClause\" VALUE=\"cList.CONTACT_TYPE_ID = cTypes.CONTACT_TYPE_ID"+objProps.where+"\">"// (IS_ACTIVE <> 'N' OR  IS_ACTIVE IS NULL)\">"
				+ "<PARAM NAME=\"GroupClause\" VALUE=\"\">"
				+ "<PARAM NAME=\"SearchColumnNames\" VALUE=\"cList.CONTACT_CODE^cList.CONTACT_FNAME^cList.CONTACT_LNAME\">"
				+ "<PARAM NAME=\"SearchColumnHeadings\" VALUE=\"Contact Code^First Name^Last Name\">"
				+ "<PARAM NAME=\"SearchColumnTypes\" VALUE=\"S^S^S\">"
				+ "<PARAM NAME=\"DisplayColumnNumbers\" VALUE=\""+objProps.displayCols+"2^3^28^5^6^7^30\">"
				+ "<PARAM NAME=\"DisplayColumnHeadings\" VALUE=\""+objProps.displayHeads+"Contact Code^Name^Address^Phone^Fax^Mobile^Contact Type\">"
				+ "<PARAM NAME=\"DisplayTextLength\" VALUE=\""+objProps.displayLength+"150^150^220^100^100^100^90\">"
				+ "<PARAM NAME=\"PageSize\" VALUE=\"" + GetPageSize() + "\">"
				+ "<PARAM NAME=\"ColumnTypes\" VALUE=\"S^S^S\">"
				+ "<PARAM NAME=\"FetchColumns\" VALUE=\"1^2^9^10^8^4^11^12^13^14^15^16^5^17^6^7^18^19^20^21^22^23^24^25^26^27\">"
				+ "<PARAM NAME=\"PrimaryKeyColumns\" VALUE=\"1\">"
				+ "<PARAM NAME=\"GridHeaderText\" VALUE=\"Contact Information\">"
				+ "<PARAM NAME=\"CacheSize\" VALUE=\"" + GetCacheSize() + "\">"
				+ "<PARAM NAME=\"ColorScheme\" VALUE=\"" + GetWindowsGridColor() + "\">"
				+ "<PARAM NAME=\"ExtraButtons\" VALUE=\"1^Add New^0\">"
				+ "</OBJECT>";*/return "";
		}
		
		#region loading web grid control
		/*1.	cList.CONTACT_ID
		2.	cList.CONTACT_CODE,
		3.	cList.CONTACT_FNAME+' '+cList.CONTACT_LNAME as 'Contact Name',
		4.	cList.CONTACT_ADD1,
		5.	cList.CONTACT_BUSINESS_PHONE,
		6.	cList.CONTACT_FAX
		7.	,cList.CONTACT_MOBILE
		8.	,cList.CONTACT_LNAME,
		9.	cList.CONTACT_FNAME,
		10.	cList.CONTACT_MNAME,
		11.	cList.CONTACT_ADD2,
		12.	cList.CONTACT_CITY
		13.	,cList.CONTACT_STATE
		14.	,cList.CONTACT_ZIP,
		15.	cList.CONTACT_COUNTRY,
		16.	cList.CONTACT_EXT,
		17.	cList.CONTACT_EMAIL,
		18.	cList.CONTACT_PAGER,
		19.	cList.CONTACT_HOME_PHONE,
		20.	cList.CONTACT_TOLL_FREE,
		21.	cList.CONTACT_NOTE,
		22.	cList.IS_ACTIVE,
		23.	cList.CONTACT_SALUTATION,
		24.	CONTACT_POS,
		25.	cList.CONTACT_TYPE_ID,
		26.	cList.INDIVIDUAL_CONTACT_ID,
		27.	cList.MODIFIED_BY,
		28.	cList.CONTACT_ADD1+' '+cList.CONTACT_ADD2 as Address,
		29.	cList.CONTACT_EXISTENCE_AS,
		30.	cTypes.CONTACT_TYPE_DESC,*/

		private void LoadWebGridControl()
		{

			string strTypeId = "";
			
			if ( Request.QueryString["CONTACT_TYPE_ID"] != null )
			{
				strTypeId = Request.QueryString["CONTACT_TYPE_ID"].ToString();
			}

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
			ContactProperties objProps = GetContactProperties();
			Control c1= LoadControl("../webcontrols/BaseDataGrid.ascx");
			try
			{
				/*************************************************************************/
				///////////////  SETTING WEB GRID USER CONTROL PROPERTIES /////////////////
				/************************************************************************/
				//specifying webservice URL
				((BaseDataGrid)c1).WebServiceURL="../webservices/BaseDataGridWS.asmx?WSDL";
				//specifying columns for select query
                ((BaseDataGrid)c1).SelectClause = "cList.CONTACT_ID,cList.CONTACT_CODE,cList.CONTACT_FNAME+' '+cList.CONTACT_LNAME as 'Contact_Name',cList.CONTACT_ADD1,cList.CONTACT_BUSINESS_PHONE,cList.CONTACT_FAX,cList.CONTACT_MOBILE,cList.CONTACT_LNAME,cList.CONTACT_FNAME,cList.CONTACT_MNAME,cList.CONTACT_ADD2,cList.CONTACT_CITY,cList.CONTACT_STATE,cList.CONTACT_ZIP,cList.CONTACT_COUNTRY,cList.CONTACT_EXT,cList.CONTACT_EMAIL,cList.CONTACT_PAGER,cList.CONTACT_HOME_PHONE,cList.CONTACT_TOLL_FREE,cList.CONTACT_NOTE,cList.IS_ACTIVE,cList.CONTACT_SALUTATION,CONTACT_POS,cList.CONTACT_TYPE_ID,cList.INDIVIDUAL_CONTACT_ID,cList.MODIFIED_BY,cList.CONTACT_ADD1+''+ case when cList.NUMBER !='' then isnull(', '+cList.NUMBER,'') else '' end +' '+ case when cList.CONTACT_ADD2!='' then isnull(cList.CONTACT_ADD2,'') else '' end  + ''+ case when cList.DISTRICT !='' then isnull(' - '+cList.DISTRICT,'') else '' end  + ''+ case when cList.CONTACT_CITY !='' then isnull(' - '+cList.CONTACT_CITY,'') else '' end +'/'+MCSL.STATE_CODE+ ''+ case when cList.CONTACT_ZIP !='' then isnull(' - '+cList.CONTACT_ZIP ,'') else '' end  as Address,cList.CONTACT_EXISTENCE_AS,isnull(ctypemulti.CONTACT_TYPE_DESC ,cTypes.CONTACT_TYPE_DESC) as CONTACT_TYPE_DESC" + objProps.select;
				//specifying tables for from clause
                if (objProps.from != "")
                {
                    ((BaseDataGrid)c1).FromClause = " MNT_CONTACT_LIST cList left outer join MNT_COUNTRY_STATE_LIST MCSL on MCSL.STATE_ID=cList.CONTACT_STATE left outer join  MNT_CONTACT_TYPES cTypes on cList.CONTACT_TYPE_ID = cTypes.CONTACT_TYPE_ID left outer join  CLT_CUSTOMER_LIST cust  on cust.CUSTOMER_ID=cList.INDIVIDUAL_CONTACT_ID  left outer join MNT_CONTACT_TYPES_MULTILINGUAL ctypemulti on ctypemulti.CONTACT_TYPE_ID=cTypes.CONTACT_TYPE_ID and ctypemulti.LANG_ID=" + GetLanguageID();
                }
                else
                {
                    ((BaseDataGrid)c1).FromClause = " MNT_CONTACT_LIST cList left outer join MNT_COUNTRY_STATE_LIST MCSL on MCSL.STATE_ID=cList.CONTACT_STATE, MNT_CONTACT_TYPES cTypes" + objProps.from + "  left outer join MNT_CONTACT_TYPES_MULTILINGUAL ctypemulti on ctypemulti.CONTACT_TYPE_ID=cTypes.CONTACT_TYPE_ID and ctypemulti.LANG_ID=" + GetLanguageID();
                }
				//specifying conditions for where clause
             
				((BaseDataGrid)c1).WhereClause=" cList.CONTACT_TYPE_ID = cTypes.CONTACT_TYPE_ID"+objProps.where;
				//specifying Text to be shown in combo box
                ((BaseDataGrid)c1).SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"First Name^Last Name^Contact Code^Contact Type^Business Phone";
                //((BaseDataGrid)c1).SearchColumnNames = "cList.CONTACT_FNAME^cList.CONTACT_LNAME^cList.CONTACT_CODE^isnull(ctypemulti.CONTACT_TYPE_DESC ,cTypes.CONTACT_TYPE_DESC)^cList.CONTACT_BUSINESS_PHONE";
                //specifying column to be used for combo box
                ((BaseDataGrid)c1).SearchColumnNames = "cList.CONTACT_FNAME^cList.CONTACT_CODE^isnull(ctypemulti.CONTACT_TYPE_DESC ,cTypes.CONTACT_TYPE_DESC)^cList.CONTACT_BUSINESS_PHONE";
				//search column data type specifying data type of the column to be used for combo box
				((BaseDataGrid)c1).SearchColumnType="T^T^T^P";
				//specifying column for order by clause
				((BaseDataGrid)c1).OrderByClause="CONTACT_CODE asc";
				
				//For Customer
				if ( strTypeId == "2" )
				{
					//specifying column numbers of the query to be displyed in grid
					((BaseDataGrid)c1).DisplayColumnNumbers="2^"+objProps.displayCols+"3^28^5^6^7^30";
					//specifying column names from the query
					((BaseDataGrid)c1).DisplayColumnNames = "CONTACT_CODE^" + objProps.displayColumnNames + "Contact_Name^Address^CONTACT_BUSINESS_PHONE^CONTACT_FAX^CONTACT_MOBILE^CONTACT_TYPE_DESC";//**********
					//specifying text to be shown as column headings
					((BaseDataGrid)c1).DisplayColumnHeadings= objResourceMgr.GetString("DisplayColumnHeadingsCarr1")+ objProps.displayHeads +objResourceMgr.GetString("DisplayColumnHeadingsCarr2");// "Contact Code^" + objProps.displayHeads + "Name^Address^Business Phone^Fax^Mobile^Contact Type";
					//specifying column heading display text length
					((BaseDataGrid)c1).DisplayTextLength= "150^" + objProps.displayLength + "150^220^100^100^100^90";
					//specifying width percentage for columns
					((BaseDataGrid)c1).DisplayColumnPercent = "14^" + objProps.displayColumnPercent + "15^15^13^13^15^15";///*********
				}
				else
				{
					//specifying column numbers of the query to be displyed in grid
					((BaseDataGrid)c1).DisplayColumnNumbers=objProps.displayCols+"2^3^28^5^6^7^30";
					//specifying column names from the query
					((BaseDataGrid)c1).DisplayColumnNames=objProps.displayColumnNames + "CONTACT_CODE^Contact_Name^Address^CONTACT_BUSINESS_PHONE^CONTACT_FAX^CONTACT_MOBILE^CONTACT_TYPE_DESC";//**********
                    //specifying text to be shown as column headings DisplayColumnHeadingsNoncarr
					((BaseDataGrid)c1).DisplayColumnHeadings=objProps.displayHeads+objResourceMgr.GetString("DisplayColumnHeadingsNonCarr");//"Contact Code^Name^Address^Business Phone^Fax^Mobile^Contact Type";
					//specifying column heading display text length
					((BaseDataGrid)c1).DisplayTextLength=objProps.displayLength+"150^150^220^100^100^100^90";
					//specifying width percentage for columns
					((BaseDataGrid)c1).DisplayColumnPercent=objProps.displayColumnPercent+"14^15^15^13^13^15^15";///*********
				}
				//specifying primary column number
				((BaseDataGrid)c1).PrimaryColumns="1";
				//specifying primary column name
				((BaseDataGrid)c1).PrimaryColumnsName="cList.CONTACT_ID";
				//specifying column type of the data grid
				((BaseDataGrid)c1).ColumnTypes = objProps.ColumnTypes+"B^B^B^B^B^B^B";
				//specifying links pages 
				//((BaseDataGrid)c1).ColumnsLink="client.aspx^webindex.aspx^webindex.aspx^webindex.aspx^webindex.aspx";
				//specifying if double click is allowed or not
				((BaseDataGrid)c1).AllowDBLClick="true"; 
				//specifying which columns are to be displayed on first tab
				((BaseDataGrid)c1).FetchColumns="1^2^9^10^8^4^11^12^13^14^15^16^5^17^6^7^18^19^20^21^22^23^24^25^26^27";
				//specifying message to be shown
                ((BaseDataGrid)c1).SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				//specifying buttons to be displayed on grid
                ((BaseDataGrid)c1).ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";
				//specifying number of the rows to be shown
				((BaseDataGrid)c1).PageSize=int.Parse(GetPageSize());//int.Parse(System.Configuration.ConfigurationSettings.AppSettings.Get("GRID_PAGE_SIZE"));
				//specifying cache size (use for top clause)
				((BaseDataGrid)c1).CacheSize=int.Parse(GetCacheSize());//int.Parse(System.Configuration.ConfigurationSettings.AppSettings.Get("GRID_CACHE_SIZE"));
				//specifying image path
                ((BaseDataGrid)c1).ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
				//
                ((BaseDataGrid)c1).HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				/* commented because it was giving compilation error
				((BaseDataGrid)c1).ExtraUserFeature="3^select user_id,dbo.getpropercasename(user_title,user_fname,user_lname) from mnt_user_list"; 
				*/
				//specifying heading
				((BaseDataGrid)c1).HeaderString = objResourceMgr.GetString("HeaderString");//Contact Information";
				((BaseDataGrid)c1).SelectClass = colors;
				//specifying text to be shown for filter checkbox
                ((BaseDataGrid)c1).FilterLabel = objResourceMgr.GetString("FilterLabel");//Include Inactive";
				//specifying column to be used for filtering
				((BaseDataGrid)c1).FilterColumnName="";//"clist.IS_ACTIVE";//*******
				//value of filtering record
				((BaseDataGrid)c1).FilterValue="";
				// property indicating whether query string is required or not
				((BaseDataGrid)c1).RequireQuery ="Y";
				// column names to create query string
				((BaseDataGrid)c1).QueryStringColumns ="CONTACT_ID^CONTACT_TYPE_ID";
				((BaseDataGrid)c1).DefaultSearch="Y";
				// to show completed task we have to give check box
				//specifying text to be shown for filter checkbox
				((BaseDataGrid)c1).FilterLabel=objResourceMgr.GetString("FilterLabel");//Include Inactive";
				//specifying column to be used for filtering
				((BaseDataGrid)c1).FilterColumnName="cList.IS_ACTIVE";
				//value of filtering record
				((BaseDataGrid)c1).FilterValue="Y";

				GridHolder.Controls.Add(c1);
			}
			catch(Exception objExcep)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
			}

            TabCtl.TabURLs = "AddContact.aspx?&CallFrom=" + strCalledFrom + "&";
            TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
            TabCtl.TabLength = 150;

		}
		#endregion
		struct ContactProperties
		{
			public string select;
			public string from;
			public string where;
			public string displayCols;
			public string displayHeads;
			public string displayLength;
			public string displayColumnNames;
			public string displayColumnPercent;
			public string ColumnTypes;
			public string entityId;
			//public string QueryStringColumns;
		}
		private ContactProperties GetContactProperties()
		{
			ContactProperties objProps = new ContactProperties();
			//return zero length string if typeid is null 
			if(Request.QueryString.Get("CONTACT_TYPE_ID")==null || Request.QueryString.Get("CONTACT_TYPE_ID").ToString().Length==0)
				return GetBlankContactPropObject();
			
			string strTypeId = Request.QueryString["CONTACT_TYPE_ID"].ToString();
			
			//objProps.QueryStringColumns="^CONTACT_TYPE_ID";
			objProps.entityId = Request.QueryString["EntityId"];

			switch(strTypeId) 
			{
				case "1"://Agency
					objProps.select = ",aList.AGENCY_DISPLAY_NAME";
					objProps.from= ",MNT_AGENCY_LIST aList";
					objProps.where= " and cList.contact_type_id=1 and alist.AGENCY_ID = cList.INDIVIDUAL_CONTACT_ID and alist.AGENCY_ID = " + objProps.entityId;
					objProps.displayCols= "31^";
					objProps.displayColumnNames ="AGENCY_DISPLAY_NAME^";
					objProps.displayColumnPercent = "20^";
                    objProps.displayHeads = objResourceMgr.GetString("displayHeadsAgengy");//"Agency Name^";
					objProps.displayLength = "150^";
					objProps.ColumnTypes = "B^";
					break;
				case "2"://Customer
                    objProps.select = ",isnull(cust.CUSTOMER_FIRST_NAME ,'')+ ' ' + isnull(cust.CUSTOMER_LAST_NAME,'') as CustomerName";
					objProps.from= ",CLT_CUSTOMER_LIST cust";
					objProps.where= " and cList.contact_type_id=2 and cust.CUSTOMER_ID=cList.INDIVIDUAL_CONTACT_ID and cust.CUSTOMER_ID = " + objProps.entityId;
					objProps.displayCols= "31^";
					objProps.displayColumnNames ="CustomerName^";
					objProps.displayColumnPercent = "15^";
                    objProps.displayHeads = objResourceMgr.GetString("displayHeadsCustomer");//"Customer Name^";
					objProps.displayLength = "150^";
					objProps.ColumnTypes = "B^";
					break;
				case "3"://Finance Company
					objProps.select = ",fList.COMPANY_NAME^cList.CONTACT_EXISTENCE_AS";
					objProps.from= ",MNT_FINANCE_COMPANY_LIST fList";
					objProps.where= " and cList.contact_type_id=3 and fList.COMPANY_ID = cList.INDIVIDUAL_CONTACT_ID and fList.COMPANY_ID = " + objProps.entityId;
					objProps.displayCols= "31^32^";
					objProps.displayColumnNames = "COMPANY_NAME^CONTACT_EXISTENCE_AS^";
					objProps.displayColumnPercent = "20^";
                    objProps.displayHeads = objResourceMgr.GetString("displayHeadsFinanceCompany");//"Finance Company^Existence As^";
					objProps.displayLength = "150^100^";
					objProps.ColumnTypes = "B^B^";
					break;
				case "4"://Holder
					objProps.select = ",hList.HOLDER_NAME";
					objProps.from= ",MNT_HOLDER_INTEREST_LIST hList";
					objProps.where= " and cList.contact_type_id=4 and hList.HOLDER_ID = cList.INDIVIDUAL_CONTACT_ID and hList.HOLDER_ID = " + objProps.entityId;
					objProps.displayCols= "31^";
                    objProps.displayColumnNames = "HOLDER_NAME^";
					objProps.displayColumnPercent = "20^";
                    objProps.displayHeads = objResourceMgr.GetString("displayHeadsHolder");// "Holder Name^";
					objProps.displayLength = "150^";
					objProps.ColumnTypes = "B^";
					break;
				case "5"://Industry Provider not yet implemented
					objProps.select = ",hList.HOLDER_NAME";
					objProps.from= ",MNT_HOLDER_INTEREST_LIST hList";
					objProps.where= " and cList.contact_type_id=5 and hList.HOLDER_ID = cList.INDIVIDUAL_CONTACT_ID and hList.HOLDER_ID = " + objProps.entityId;
					objProps.displayCols= "31^";
                    objProps.displayColumnNames ="HOLDER_NAME^";
					objProps.displayColumnPercent = "20^";
                    objProps.displayHeads = objResourceMgr.GetString("displayHeadsIndustryProvider");//"Holder Name^";
					objProps.displayLength = "150^";
					objProps.ColumnTypes = "B^";
					break;
				case "6"://Personal
					objProps=GetBlankContactPropObject();
					break;
				case "7"://Tax Entity
					objProps.select = ",TaxList.TAX_NAME^cList.CONTACT_EXISTENCE_AS";
					objProps.from= ",MNT_TAX_ENTITY_LIST TaxList";
					objProps.where= " and cList.contact_type_id=7 and TaxList.TAX_ID = cList.INDIVIDUAL_CONTACT_ID and TaxList.TAX_ID = " + objProps.entityId;
					objProps.displayCols= "31^32^";
                    objProps.displayColumnNames ="TAX_NAME^CONTACT_EXISTENCE_AS^";
					objProps.displayColumnPercent = "20^";
                    objProps.displayHeads = objResourceMgr.GetString("displayHeadsTaxEntity");//"Tax Entity^Existence As^";
					objProps.displayLength = "150^100^";
					objProps.ColumnTypes = "B^B^";
					break;
				case "8"://Vendor
					objProps.select = ",vList.VENDOR_FNAME+' '+vList.VENDOR_LNAME as vendorName^cList.CONTACT_EXISTENCE_AS";
					objProps.from= ",MNT_VENDOR_LIST vList";
					objProps.where= " and cList.contact_type_id=8 and vList.VENDOR_ID = cList.INDIVIDUAL_CONTACT_ID and vList.VENDOR_ID = " + objProps.entityId;
					objProps.displayCols= "31^32^";
                    objProps.displayColumnNames ="vendorName^CONTACT_EXISTENCE_AS^";
					objProps.displayColumnPercent = "20^";
					objProps.displayHeads= objResourceMgr.GetString("displayHeadsVendor");//"Vendor Name^Existence As^";
					objProps.displayLength = "150^100^";
					objProps.ColumnTypes = "B^B^";
					break;
				default:
					objProps=GetBlankContactPropObject();
					break;
			}
			return objProps;
		}
		
		private ContactProperties GetBlankContactPropObject()
		{
			ContactProperties objProps = new ContactProperties();
			objProps.select = "";
			objProps.from= "";
			objProps.where= "";
			objProps.displayCols= "";
			objProps.displayHeads= "";
			objProps.displayLength = "";
			objProps.displayColumnNames = "";
			objProps.displayColumnPercent = "";
			objProps.ColumnTypes = "";

			//objProps.QueryStringColumns="";
			return objProps;
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