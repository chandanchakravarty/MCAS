/******************************************************************************************
<Author					: - Vijay Arora
<Start Date				: -	30-11-2005
<End Date				: -	
<Description			: - Class for Listing of Policy WaterCraft Equipment Info
<Review Date			: - 
<Reviewed By			: - 	
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

namespace Cms.Policies.Aspx.Watercrafts
{
	/// <summary>
	/// Summary description for WatercraftEquipmentIndex.
	/// </summary>
	public class PolicyWatercraftEquipmentIndex : Cms.Policies.policiesbase
	{
		protected System.Web.UI.WebControls.Panel pnlGrid;
		protected System.Web.UI.WebControls.Table tblReport;
		protected System.Web.UI.WebControls.Panel pnlReport;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;		// Stores the RGB value for grid Base
		protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;

		private void Page_Load(object sender, System.EventArgs e)
		{

			#region SETTING SCREEN ID
			if(GetLOBString()=="WAT")
				base.ScreenId="249"; 
			else if(GetLOBString()=="HOME")
				//base.ScreenId="151";
 			   base.ScreenId="254"; 	
			else if(GetLOBString()=="RENT")
				base.ScreenId="169"; 							
			#endregion


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
			
			#region set Workflow cntrol
			myWorkFlow.ScreenID = base.ScreenId;
			myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
			myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID());
			myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
			myWorkFlow.WorkflowModule="POL";

			//EQUIP_ID
			#endregion
          
			int customer_ID=GetCustomerID()==""?0:int.Parse(GetCustomerID());
			int policy_ID=GetPolicyID()==""?0:int.Parse(GetPolicyID());
			int policy_Version_ID=GetPolicyVersionID()==""?0:int.Parse(GetPolicyVersionID());

			if(customer_ID!=0 && policy_ID!=0 && policy_Version_ID!=0)
			{
         
				#region loading web grid control
				Control c1= LoadControl("../../../cmsweb/webcontrols/BaseDataGrid.ascx");
				try
				{
                
					/*************************************************************************/
					///////////////  SETTING WEB GRID USER CONTROL PROPERTIES /////////////////
					/************************************************************************/
					//specifying webservice URL
					((BaseDataGrid)c1).WebServiceURL=httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
					//specifying columns for select query
					//((BaseDataGrid)c1).SelectClause =" CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,EQUIP_ID,EQUIP_NO,EQUIP_TYPE,SHIP_TO_SHORE,YEAR,MAKE,MODEL,SERIAL_NO,INSURED_VALUE,ASSOCIATED_BOAT,PWED.IS_ACTIVE,CREATED_BY, CREATED_DATETIME, MODIFIED_BY,PWED.LAST_UPDATED_DATETIME,EQUIP_AMOUNT, EQUIPMENT_TYPE, EQUIP_TYPE,MLV1.LOOKUP_VALUE_DESC AS EQUIPMENTTYPE,IsNull(MLV2.LOOKUP_VALUE_DESC,'Others') AS DESCRIPTION ";
					((BaseDataGrid)c1).SelectClause =" CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,EQUIP_ID,EQUIP_NO,EQUIP_TYPE,SHIP_TO_SHORE,YEAR,MAKE,MODEL,SERIAL_NO,INSURED_VALUE,ASSOCIATED_BOAT,PWED.IS_ACTIVE,CREATED_BY, CREATED_DATETIME, MODIFIED_BY,PWED.LAST_UPDATED_DATETIME,EQUIP_AMOUNT, EQUIPMENT_TYPE, EQUIP_TYPE,MLV1.LOOKUP_VALUE_DESC AS EQUIPMENTTYPE,CASE WHEN OTHER_DESCRIPTION !='' THEN OTHER_DESCRIPTION ELSE  ISNULL(MLV2.LOOKUP_VALUE_DESC,'') END  AS DESCRIPTION";
					//specifying tables for from clause
					((BaseDataGrid)c1).FromClause=" POL_WATERCRAFT_EQUIP_DETAILLS PWED LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV1 ON PWED.EQUIPMENT_TYPE = MLV1.LOOKUP_UNIQUE_ID LEFT  JOIN MNT_LOOKUP_VALUES MLV2 ON PWED.EQUIP_TYPE = MLV2.LOOKUP_UNIQUE_ID ";
					//specifying conditions for where clause
					((BaseDataGrid)c1).WhereClause=" CUSTOMER_ID=" + customer_ID + " AND POLICY_ID=" + policy_ID + " AND POLICY_VERSION_ID=" + policy_Version_ID;
					//specifying Text to be shown in combo box
					((BaseDataGrid)c1).SearchColumnHeadings="Equipment ID^Equipment #^Equipment Type^Description^Year^Make^Model^Serial #";
					//specifying column to be used for combo box
					//((BaseDataGrid)c1).SearchColumnNames="EQUIP_ID^EQUIP_NO^MLV1.LOOKUP_VALUE_DESC^MLV2.LOOKUP_VALUE_DESC^YEAR^MAKE^MODEL^SERIAL_NO";
					((BaseDataGrid)c1).SearchColumnNames="EQUIP_ID^EQUIP_NO^MLV1.LOOKUP_VALUE_DESC^MLV2.LOOKUP_VALUE_DESC ! ISNULL(OTHER_DESCRIPTION,'')^YEAR^MAKE^MODEL^SERIAL_NO";					
					//search column data type specifying data type of the column to be used for combo box
					((BaseDataGrid)c1).SearchColumnType="T^T^T^T^T^T^T^T";
					//specifying column for order by clause
					//((BaseDataGrid)c1).OrderByClause="EQUIP_NO  ASC";
					((BaseDataGrid)c1).OrderByClause="EQUIP_ID  ASC";
					//specifying column numbers of the query to be displyed in grid
					((BaseDataGrid)c1).DisplayColumnNumbers="5^8^22^23^9^10^11^19";
					//specifying column names from the query
					((BaseDataGrid)c1).DisplayColumnNames="EQUIP_ID^EQUIP_NO^EQUIPMENTTYPE^DESCRIPTION^YEAR^MAKE^MODEL^SERIAL_NO";
					//specifying text to be shown as column headings
					((BaseDataGrid)c1).DisplayColumnHeadings="ID^Equipment #^Equipment Type^Description^Year^Make^Model^Serial #";
					//specifying column heading display text length
					((BaseDataGrid)c1).DisplayTextLength="3^30^40^40^20^25^25^25";
					//specifying width percentage for columns
					((BaseDataGrid)c1).DisplayColumnPercent="4^11^15^20^5^15^15^15";
					//specifying primary column number
					((BaseDataGrid)c1).PrimaryColumns="4";
					//specifying primary column name
					((BaseDataGrid)c1).PrimaryColumnsName="EQUIP_ID";
					//specifying column type of the data grid
					((BaseDataGrid)c1).ColumnTypes="B^B^B^B^B^B^B^B";
					//specifying links pages 
					//((BaseDataGrid)c1).ColumnsLink="client.aspx^webindex.aspx^webindex.aspx^webindex.aspx^webindex.aspx";
					//specifying if double click is allowed or not
					((BaseDataGrid)c1).AllowDBLClick="true"; 
					//specifying which columns are to be displayed on first tab
					((BaseDataGrid)c1).FetchColumns="1^2^3^4^5^6^7^8^9^10^11^12^13^14^15^16^17^18^19^20^21^22^23";
					//specifying message to be shown
					((BaseDataGrid)c1).SearchMessage="Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
					//specifying buttons to be displayed on grid
					((BaseDataGrid)c1).ExtraButtons="1^Add New^0^addRecord";
					//specifying number of the rows to be shown
                    ((BaseDataGrid)c1).PageSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("GRID_PAGE_SIZE"));
					//specifying cache size (use for top clause)
                    ((BaseDataGrid)c1).CacheSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("GRID_CACHE_SIZE"));
					//specifying image path
                    ((BaseDataGrid)c1).ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    ((BaseDataGrid)c1).HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
					//specifying heading
					((BaseDataGrid)c1).HeaderString = "Equipment Information";
					((BaseDataGrid)c1).SelectClass = colors;
					//specifying text to be shown for filter checkbox
					((BaseDataGrid)c1).FilterLabel="Show Inactive";
					//specifying column to be used for filtering
					((BaseDataGrid)c1).FilterColumnName="PWED.IS_ACTIVE";
					//value of filtering record
					((BaseDataGrid)c1).FilterValue="Y";
					// property indiacating whether query string is required or not
					((BaseDataGrid)c1).RequireQuery ="Y";
					// column numbers to create query string
					((BaseDataGrid)c1).QueryStringColumns ="CUSTOMER_ID^POLICY_ID^POLICY_VERSION_ID^EQUIP_ID";
					((BaseDataGrid)c1).DefaultSearch="Y";
                  
					// to show completed task we have to give check box
					GridHolder.Controls.Add(c1);
				}
				catch(Exception ex)
				{
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				}
        
				#endregion
                
				cltClientTop.CustomerID = int.Parse(GetCustomerID());
				cltClientTop.PolicyID = int.Parse(GetPolicyID());
				cltClientTop.PolicyVersionID = int.Parse(GetPolicyVersionID());
        
				cltClientTop.ShowHeaderBand ="Policy";
				cltClientTop.Visible = true;             
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
