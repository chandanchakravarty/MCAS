/******************************************************************************************
<Author				: -   Anurag Verma
<Start Date			: -	  10/11/2005
<End Date			: -	  
<Description		: -   Policy Dwelling Details Index	
<Review Date		: - 
<Reviewed By		: - 	
Modification History

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
using Cms.CmsWeb;
using Cms.CmsWeb.WebControls;

namespace Cms.Policies.aspx.HomeOwners
{
	/// <summary>
	/// Summary description for PolicyDwellingDetailsIndex.
	/// </summary>
	public class PolicyDwellingDetailsIndex : Cms.Policies.policiesbase
	{
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidEntityType;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidEntityId;
		string customerID = "";
		string polID = "";
		string polVersionID = "";
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidKeyValues;
		protected ClientTop cltClientTop;
		string colors = "";
		string strCalledFrom="";
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;

		//added by vj on 19-10-2005
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRemoveTab;

		private void Page_Load(object sender, System.EventArgs e)
		{
			#region Setting ScreenId
			// Check from where the screen is called.
			if (Request.QueryString["CalledFrom"] != null || Request.QueryString["CalledFrom"] !="")
			{
				strCalledFrom=Request.QueryString["CalledFrom"].ToString();
			}

			TabCtl.TabURLs = "PolicyAddDwellingDetails.aspx?CalledFrom=" + strCalledFrom + "&";
			//Setting screen Id.	
			switch (strCalledFrom) 
			{
				case "Home":
				case "HOME":
					base.ScreenId="239";
					TabCtl.TabLength =110;
					break;
				case "Rental":
				case "RENTAL":
					base.ScreenId="259";
					TabCtl.TabLength =150;
					break;			
			}
			#endregion
			
			#region GETTING BASE COLOR FOR ROW SELECTION
			string colorScheme=GetColorScheme();
	
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
			
			customerID =  GetCustomerID();
			polID = GetPolicyID();
			polVersionID = GetPolicyVersionID();

			//added by vj on 19-10-2005
			hidRemoveTab.Value = "0";

			// Put user code to initialize the page here
			if ( !Page.IsPostBack )
			{
			
				//added by vj on 15-10-2005 
				hidCalledFrom.Value  = Convert.ToString(Request.QueryString["CalledFrom"]);

				
				
				if ( customerID == "" &&
					polID == "" && 
					polVersionID == "" 
					)
				{
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","118");
					return;
				}
				
				cltClientTop.PolicyID = int.Parse(polID);
				cltClientTop.CustomerID = int.Parse(customerID);
				cltClientTop.PolicyVersionID = int.Parse(polVersionID);
				cltClientTop.ShowHeaderBand = "Policy";
				cltClientTop.Visible= true;

				BindGrid();

				SetWorkflow();
				
			}

			
		}

		private void BindGrid()
		{
			string polID = GetPolicyID();
			string polVersionID =  GetPolicyVersionID();
			string customerID = GetCustomerID();

			Control c1= LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
	
			try
			{
				/*************************************************************************/
				///////////////  SETTING WEB GRID USER CONTROL PROPERTIES /////////////////
				/************************************************************************/
				//specifying webservice URL
				((BaseDataGrid)c1).WebServiceURL= httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				
				//specifying columns for select query
				((BaseDataGrid)c1).SelectClause = "ADI.DWELLING_ID,ADI.DWELLING_NUMBER,AL.LOC_NUM,ADI.YEAR_BUILT," + 
					"ADI.PURCHASE_YEAR," + 
					"ISNULL(AL.LOC_ADD1,'') + ' ' + ISNULL(AL.LOC_ADD2,'') + " + 
					"CASE " +  
					"WHEN AL.LOC_ADD2 IS NULL THEN ''" + 
					"ELSE ', '" + 
					"END" +
					"+" +  
					"ISNULL(AL.LOC_CITY,'') +" +  
					"CASE " + 
					"WHEN AL.LOC_CITY IS NULL THEN ''" + 
					"ELSE ', '" + 
					"END" + 
					"+" + 
					"ISNULL(SL.STATE_NAME,'') + ' ' + ISNULL(AL.LOC_ZIP,'') as Address," + 
					//"ADI.POLICY_ID,ADI.POLICY_VERSION_ID,ADI.CUSTOMER_ID"
					"ADI.POLICY_ID,ADI.POLICY_VERSION_ID,ADI.CUSTOMER_ID,ADI.IS_ACTIVE"
					;
				
				//specifying tables for from clause
				((BaseDataGrid)c1).FromClause = " POL_DWELLINGS_INFO ADI " + 
					" INNER JOIN POL_LOCATIONS AL ON " + 
					"ADI.LOCATION_ID = AL.LOCATION_ID " +
					" LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST SL ON " +
					"AL.LOC_COUNTRY = SL.COUNTRY_ID AND " +
					"AL.LOC_STATE = SL.STATE_ID	"
					;
											  
				//specifying conditions for where clause
				((BaseDataGrid)c1).WhereClause = " ADI.POLICY_ID = " + polID + " AND " +
					" ADI.POLICY_VERSION_ID = " + polVersionID + " AND " +
					" ADI.CUSTOMER_ID = " + customerID + " AND " + 
					" AL.POLICY_ID = " + polID + " AND " +
					" AL.POLICY_VERSION_ID = " + polVersionID + " AND " +
					" AL.CUSTOMER_ID = " + customerID; 
				
				//specifying Text to be shown in combo box
				((BaseDataGrid)c1).SearchColumnHeadings = "Property #^Year Built^Purchase Year";
				
				//specifying column to be used for combo box
				((BaseDataGrid)c1).SearchColumnNames = "DWELLING_NUMBER^YEAR_BUILT^PURCHASE_YEAR";
				
				//search column data type specifying data type of the column to be used for combo box
				((BaseDataGrid)c1).SearchColumnType="T^T^T";
				
				//specifying column for order by clause
				((BaseDataGrid)c1).OrderByClause = "DWELLING_NUMBER ASC ";
				
				//specifying column numbers of the query to be displyed in grid
				//((BaseDataGrid)c1).DisplayColumnNumbers = "2^3^4^7^5";
				((BaseDataGrid)c1).DisplayColumnNumbers = "2^3^7^5";
				
				//specifying column names from the query
				//((BaseDataGrid)c1).DisplayColumnNames = "DWELLING_NUMBER^LOC_NUM^SUB_LOC_NUMBER^Address^YEAR_BUILT^PURCHASE_YEAR";
				((BaseDataGrid)c1).DisplayColumnNames = "DWELLING_NUMBER^LOC_NUM^Address^YEAR_BUILT^PURCHASE_YEAR";
				
				//specifying text to be shown as column headings
				//((BaseDataGrid)c1).DisplayColumnHeadings = "Dwelling #^Location #^Sublocation #^Address^Year Built^Purchase Year";
                ((BaseDataGrid)c1).DisplayColumnHeadings = "Property #^Location #^Address^Year Built^Purchase Year";
				
				//specifying column heading display text length
				//((BaseDataGrid)c1).DisplayTextLength="10^10^15^40^10^20";
				((BaseDataGrid)c1).DisplayTextLength="10^10^40^10^20";
				
				//specifying width percentage for columns
				//((BaseDataGrid)c1).DisplayColumnPercent="10^10^15^40^10^20";
				((BaseDataGrid)c1).DisplayColumnPercent="11^11^41^11^21";
				
				//specifying primary column number
				((BaseDataGrid)c1).PrimaryColumns="1^2^3^4";
				
				//specifying primary column name
				((BaseDataGrid)c1).PrimaryColumnsName="DWELLING_ID";
				
				//specifying column type of the data grid
				//((BaseDataGrid)c1).ColumnTypes="B^B^B^B^B^B";
				((BaseDataGrid)c1).ColumnTypes="B^B^B^B^B";
				
				//specifying links pages 
				//((BaseDataGrid)c1).ColumnsLink="client.aspx^webindex.aspx^webindex.aspx^webindex.aspx^webindex.aspx";
				//specifying if double click is allowed or not
				
				((BaseDataGrid)c1).AllowDBLClick="true"; 
				
				//specifying which columns are to be displayed on first tab
				((BaseDataGrid)c1).FetchColumns = "1^2^3^4^6^7^8^9";
				
				//specifying message to be shown
				((BaseDataGrid)c1).SearchMessage="Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				
				//((BaseDataGrid)c1).ExtraButtons = "2^Add~Delete^0~1";

				//specifying buttons to be displayed on grid
				((BaseDataGrid)c1).ExtraButtons="1^Add^0^addRecord";
				
				//specifying number of the rows to be shown
                ((BaseDataGrid)c1).PageSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("GRID_PAGE_SIZE"));
				
				//specifying cache size (use for top clause)
                ((BaseDataGrid)c1).CacheSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("GRID_CACHE_SIZE"));
				
				//specifying image path
                ((BaseDataGrid)c1).ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                ((BaseDataGrid)c1).HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				
				//specifying heading
				((BaseDataGrid)c1).HeaderString = "Property Information";
				
				((BaseDataGrid)c1).SelectClass = colors;

				//specifying text to be shown for filter checkbox
				((BaseDataGrid)c1).FilterLabel="Include Inactive";
				
				//specifying column to be used for filtering
				((BaseDataGrid)c1).FilterColumnName="ADI.IS_ACTIVE";
				
				//value of filtering record
				((BaseDataGrid)c1).FilterValue="Y";
				
				// property indiacating whether query string is required or not
				((BaseDataGrid)c1).RequireQuery ="Y";
				
				// column numbers to create query string
				((BaseDataGrid)c1).QueryStringColumns ="DWELLING_ID";
				((BaseDataGrid)c1).DefaultSearch = "Y";
				// to show completed task we have to give check box
				GridHolder.Controls.Add(c1);

				//TabCtl.TabURLs = "AddAttachment.aspx?EntityId=" + hidEntityId.Value + "&EntityType=" + hidEntityType.Value + "&Grid=web&";

			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
		}

		private void SetWorkflow()
		{
			if(base.ScreenId	==	"239" || base.ScreenId == "259")
			{
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				myWorkFlow.WorkflowModule="POL";
			}
			else
			{
				myWorkFlow.Display	=	false;
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
