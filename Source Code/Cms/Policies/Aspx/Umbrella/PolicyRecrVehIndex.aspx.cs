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
using Cms.CmsWeb;

namespace Cms.Policies.Aspx.Umbrella
{
	/// <summary>
	/// Summary description for UmrellaRecrVehIndex.
	/// </summary>
	public class PolicyRecrVehIndex : Cms.Policies.policiesbase
	{
		protected ClientTop cltClientTop;
		protected string customerID = "";
		protected string policyID = "";
		protected string policyVersionID = "";
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidEntityType;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidEntityId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidKeyValues;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		string colors = "";


		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId = "276";
			// Put user code to initialize the page here
			#region GETTING BASE COLOR FOR ROW SELECTION
			
			string colorScheme=GetColorScheme();
		
			switch (int.Parse(colorScheme))
			{
				case 1:
					colors=System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR1").ToString();     
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
			
			customerID =  GetCustomerID();
			policyID = GetPolicyID();
			policyVersionID = GetPolicyVersionID();

			// Put user code to initialize the page here
			if ( !Page.IsPostBack )
			{
			
				
				if ( customerID == "" &&
					policyID == "" && 
					policyVersionID == "" 
					)
				{
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","118");
					return;
				}
				
				cltClientTop.PolicyID = int.Parse(policyID);
				cltClientTop.CustomerID = int.Parse(customerID);
				cltClientTop.PolicyVersionID = int.Parse(policyVersionID);
				cltClientTop.ShowHeaderBand = "Policy";
				cltClientTop.Visible= true;

				BindGrid();
				
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

		private void BindGrid()
		{
			Control c1= LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			try
			{
				/*************************************************************************/
				///////////////  SETTING WEB GRID USER CONTROL PROPERTIES /////////////////
				/************************************************************************/
				//specifying webservice URL
				((BaseDataGrid)c1).WebServiceURL= httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				
				//specifying columns for select query
				((BaseDataGrid)c1).SelectClause = "SERIAL, YEAR, MAKE, MODEL, COMPANY_ID_NUMBER, REC_VEH_ID,ACTIVE,MNT_COUNTRY_STATE_LIST.STATE_NAME";
				
				//specifying tables for from clause
				((BaseDataGrid)c1).FromClause = " POL_UMBRELLA_RECREATIONAL_VEHICLES LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST ON POL_UMBRELLA_RECREATIONAL_VEHICLES.STATE_REGISTERED=MNT_COUNTRY_STATE_LIST.STATE_ID";
											  
				//specifying conditions for where clause
				((BaseDataGrid)c1).WhereClause = " POLICY_ID = " + GetPolicyID() + " AND " +
					" POLICY_VERSION_ID = " + GetPolicyVersionID() + " AND " +
					" CUSTOMER_ID = " + GetCustomerID() + " "; 
				
				//specifying Text to be shown in combo box
				((BaseDataGrid)c1).SearchColumnHeadings = "RV #^Year^Make^Model^Serial #";
				
				//specifying column to be used for combo box
				((BaseDataGrid)c1).SearchColumnNames = "COMPANY_ID_NUMBER^YEAR^MAKE^MODEL^SERIAL";
				
				//search column data type specifying data type of the column to be used for combo box
				((BaseDataGrid)c1).SearchColumnType="T^T^T^T^T";
				
				//specifying column for order by clause
				((BaseDataGrid)c1).OrderByClause = "COMPANY_ID_NUMBER ASC";
				
				//specifying column numbers of the query to be displyed in grid
				((BaseDataGrid)c1).DisplayColumnNumbers = "1^2^3^4^5";
				
				//specifying column names from the query
				((BaseDataGrid)c1).DisplayColumnNames = "COMPANY_ID_NUMBER^YEAR^MAKE^MODEL^SERIAL";
				
				//specifying text to be shown as column headings
				((BaseDataGrid)c1).DisplayColumnHeadings = "RV #^Year^Make^Model^Serial #";
				
				//specifying column heading display text length
				((BaseDataGrid)c1).DisplayTextLength="50^50^50^50^50";
				
				//specifying width percentage for columns
				((BaseDataGrid)c1).DisplayColumnPercent="15^15^23^23^23";
				
				//specifying primary column number
				((BaseDataGrid)c1).PrimaryColumns="1";
				
				//specifying primary column name
				((BaseDataGrid)c1).PrimaryColumnsName="REC_VEH_ID";
				
				//specifying column type of the data grid
				((BaseDataGrid)c1).ColumnTypes="B^B^B^B^B";
				
				//specifying links pages 
				//((BaseDataGrid)c1).ColumnsLink="client.aspx^webindex.aspx^webindex.aspx^webindex.aspx^webindex.aspx";
				//specifying if double click is allowed or not
				
				((BaseDataGrid)c1).AllowDBLClick="true"; 
				
				//specifying which columns are to be displayed on first tab
				((BaseDataGrid)c1).FetchColumns = "1^2^3^4^5";
				
				//specifying message to be shown
				((BaseDataGrid)c1).SearchMessage="Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				
				//((BaseDataGrid)c1).ExtraButtons = "2^Add~Delete^0~1";

				//specifying buttons to be displayed on grid
				//((BaseDataGrid)c1).ExtraButtons="1^Add^0^addRecord";
				((BaseDataGrid)c1).ExtraButtons="2^Add New~View Records^0~1^addRecord~CopySchRecords";
				
				//specifying number of the rows to be shown
				((BaseDataGrid)c1).PageSize = int.Parse(GetPageSize());
				
				//specifying cache size (use for top clause)
				((BaseDataGrid)c1).CacheSize = int.Parse(GetCacheSize());
				
				//specifying image path
                ((BaseDataGrid)c1).ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                ((BaseDataGrid)c1).HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				
				//specifying heading
				((BaseDataGrid)c1).HeaderString = "Recreational Vehicles - Other Carriers";
				
				((BaseDataGrid)c1).SelectClass = colors;
				
				((BaseDataGrid)c1).FilterLabel = "Include Inactive";
				((BaseDataGrid)c1).FilterColumnName = "POL_UMBRELLA_RECREATIONAL_VEHICLES.ACTIVE";
				((BaseDataGrid)c1).FilterValue = "Y";
				
				// property indiacating whether query string is required or not
				((BaseDataGrid)c1).RequireQuery ="Y";
				((BaseDataGrid)c1).DefaultSearch ="Y";
				
				// column numbers to create query string
				((BaseDataGrid)c1).QueryStringColumns ="REC_VEH_ID";

				// to show completed task we have to give check box
				GridHolder.Controls.Add(c1);

				//TabCtl.TabURLs = "AddAttachment.aspx?EntityId=" + hidEntityId.Value + "&EntityType=" + hidEntityType.Value + "&Grid=web&";
			}
			catch
			{
			}
			#region set Workflow cntrol
			SetWorkFlow();
			#endregion
		}

		private void SetWorkFlow()
		{
			if(base.ScreenId == "276")
			{
				myWorkFlow.ScreenID = base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID ());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				myWorkFlow.WorkflowModule ="POL";
			}
			else
			{
				myWorkFlow.Display	=	false;
			}
		}
	}
}
