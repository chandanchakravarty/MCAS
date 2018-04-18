/******************************************************************************************
	<Author					: - >
	<Start Date				: -	>
	<End Date				: - >
	<Description			: - >
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - > May 3, 2005
	<Modified By			: - > Pradeep
	<Purpose				: - > Added field in Select list for file name
    
    <Modified Date			: - > June 3, 2005
	<Modified By			: - > Pradeep
	<Purpose				: - > Uncommented Colors propery
	
	<Modified Date			: - > 05-10-2005
	<Modified By			: - > Vijay Arora
	<Purpose				: - > Change the where clause for Application Attachment.

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
using Cms.CmsWeb.WebControls;
using System.Resources;
using System.Reflection;
using Cms.BusinessLayer.BlCommon;




namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for AttachmentIndex.
	/// </summary>
	public class AttachmentIndex : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.HtmlControls.HtmlForm Division;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidEntityType;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidEntityId;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        ResourceManager objResourceMgr = null;
		string colors = "";
		private string strCalledFrom="";

		private void Page_Load(object sender, System.EventArgs e)
		{
            objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.Attachmentindex", Assembly.GetExecutingAssembly());
			SetCookieValue();

			hidEntityId.Value			=	Request.Params["EntityId"];
			hidEntityType.Value			=	Request.Params["EntityType"];
            Ajax.Utility.RegisterTypeForAjax(typeof(AttachmentIndex));
			// Put user code to initialize the page here
			#region setting screen id
			if (Request.QueryString["CALLEDFROM"]!=null && Request.QueryString["CALLEDFROM"].ToString().Trim()!="")
			{
				strCalledFrom = Request.QueryString["CALLEDFROM"].ToString().Trim();	
				
			}
			switch(strCalledFrom)
			{

				//Added by Sibin on 20 Oct 08 to add it into Application Details Permission List

				case "application" :
				case "Application" :
					base.ScreenId	=	"201_8";
					break;
				case "reinsurance" :
				case "REINSURANCE" :
					base.ScreenId	=	"262_1";
					break;
				case "bank" :
				case "BANK" :
					base.ScreenId	=	"125_1_2";
					break;
				case "agency" :
				case "AGENCY" :
					base.ScreenId	=	"10_2";
					break;
				case "department" :
				case "DEPARTMENT" :
					base.ScreenId	=	"29_1";
					break;
				case "division" :
				case "DIVISION" :
					base.ScreenId	=	"28_1";
					break;
				
				case "fin" :
				case "FINANCE" :
					base.ScreenId	=	"35_2";
					break;
				case "mortgage" :
				case "MORTGAGE" :
					base.ScreenId	=	"37_1_0";
					break;
				case "profit" :
				case "PROFIT" :
					base.ScreenId	=	"27_1";
					break;

				
				//Added by Sibin on 21 Oct 08 to add it into Policy Details Permission List
				case "policy" :
				case "POLICY" :
					base.ScreenId	=	"224_8";
					break;
 
				case "tax" :
				case "TAX" :
					base.ScreenId	=	"36_1";
					break;
				case "vendor" :
				case "VENDOR" :
					base.ScreenId	=	"32_1";
					break;
				case "reinsurer" :
				case "REINSURER" :
					base.ScreenId	=	"263_1";
					break;
				case "CLAIM" :
					base.ScreenId	=	"306_9";
					break;
                case"inclt":
				case "InCLT" :    		
					base.ScreenId	=	"120_4";
					break;
                case "CUSTOMER":
                    base.ScreenId = "134_4";
                    break;   
				default :
					base.ScreenId	=	"36_1";
					break;
			}
			#endregion

			//Updating the security xml
			UpdateSecurityXml(strCalledFrom);
			
			
            //if ( !Page.IsPostBack )
            //{

				#region GETTING BASE COLOR FOR ROW SELECTION
				
				string colorScheme=GetColorScheme();
				colors="";

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

				BindGrid();
            //}
			
			
			
		}

		private void UpdateSecurityXml(string strCalledFrom)
		{

			if (strCalledFrom.ToUpper() == "APPLICATION")
			{
				//Called from application, hence checking whether policy for the application exist or not
				hidEntityId.Value="0";				
				Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGenInfo = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
				//System.Data.DataSet ds = objGenInfo.GetPolicyDetails(int.Parse(GetCustomerID()), int.Parse(GetAppID()), int.Parse(GetAppVersionID()));
				System.Data.DataSet ds = objGenInfo.GetPolicyDetailsForAttachment(int.Parse(GetCustomerID()), int.Parse(GetAppID()));
				if (ds.Tables[0].Rows.Count > 0)
				{
					//Policy exists for this particulat application, hence changing the security xml to view mode only
					gstrSecurityXML = "<Security><Read>Y</Read><Write>N</Write><Delete>N</Delete><Execute>N</Execute></Security>";
					base.InitializeSecuritySettings();
				}
				ds.Dispose();
			}
			else if(strCalledFrom.ToUpper() == "POLICY")
			{
				hidEntityType.Value="Policy";

				//Here we will check status of policy in session
				try
				{
					//Changing the security xml 
					gstrSecurityXML = "<Security><Read>Y</Read><Write>Y</Write><Delete>Y</Delete><Execute>Y</Execute></Security>";
					base.InitializeSecuritySettings(); 
					// Commmented by swastika acc to iTrack #1434
					// Irrespective of the policy status, the attachment can be added.
					/*
					string strPolicyId = GetPolicyID();
					string strPolicyVerId = GetPolicyVersionID();
					string strCustomerID = GetCustomerID();
					
					Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGenInfo;
					objGenInfo = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();

					System.Data.DataSet ds = objGenInfo.GetPolicyDataSet(int.Parse(strCustomerID)
						, int.Parse(strPolicyId), int.Parse(strPolicyVerId));

					if (ds.Tables[0].Rows.Count > 0)
					{

						//If policy status is not one of following, changing the security xml to read only mode

						string policyStatus = ds.Tables[0].Rows[0]["POLICY_STATUS_CODE"].ToString().ToUpper().Trim();
						if ( ! ( policyStatus == Cms.BusinessLayer.BlCommon.ClsCommon.POLICY_STATUS_UNDER_ENDORSEMENT 
							|| policyStatus == Cms.BusinessLayer.BlCommon.ClsCommon.POLICY_STATUS_UNDER_RENEW
							|| policyStatus == Cms.BusinessLayer.BlCommon.ClsCommon.POLICY_STATUS_UNDER_CORRECTIVE_USER 
							|| policyStatus == Cms.BusinessLayer.BlCommon.ClsCommon.POLICY_STATUS_UNDER_ISSUE
							|| policyStatus == Cms.BusinessLayer.BlCommon.ClsCommon.POLICY_STATUS_SUSPENDED) )
						{

							//Changing the security xml to view mode only
							gstrSecurityXML = "<Security><Read>Y</Read><Write>N</Write><Delete>N</Delete><Execute>N</Execute></Security>";
							base.InitializeSecuritySettings(); 
						}
					}
					
					ds.Dispose();
					*/
				}

				catch
				{
				}
               
			}
           
		}

		private void SetCookieValue ()
		{
			//Setting the cookie if open from customer manager
      //     if (strCalledFrom.ToUpper()!= "INCLT")//Changed from EntityType for Policy Page Implementation
				//return;
            

			string strCarrierSystemID = Cms.CmsWeb.cmsbase.CarrierSystemID;
			string strSystemID = GetSystemId();
			if(strCarrierSystemID.ToUpper()!=strSystemID.ToUpper())
				Response.Cookies["LastVisitedTab"].Value = "3";
			else
                Response.Cookies["LastVisitedTab"].Value = "3";//Changed from 4 for Policy Page Implementation
			Response.Cookies["LastVisitedTab"].Expires = DateTime.Now.Add(new TimeSpan(30,0,0,0,0));
		}
		
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
				
				//added by vj to display the application attachments with application number and application version at customer level.
				if (hidEntityType.Value.ToUpper().Trim() == "CUSTOMER")
				{
					//specifying columns for select query
                    ((BaseDataGrid)c1).SelectClause = "ATTACH_ID,ATTACH_FILE_NAME,ATTACH_FILE_TYPE,isnull(convert(nvarchar(10),ATTACH_DATE_TIME,case when " + ClsCommon.BL_LANG_ID + "=1 then 101 else 103 end),'')as ATTACH_DATE_TIME," + 
						" USER_FNAME + ' ' + USER_LNAME as USER_NAME,ATTACH_FILE_DESC,CONVERT(VARCHAR(10),ATTACH_ID) + '.' + " +  
						" ATTACH_FILE_TYPE as FILE_FULL_NAME,APP_NUMBER, APP_VERSION" ;
					
					//specifying tables for from clause
                    ((BaseDataGrid)c1).FromClause = "MNT_ATTACHMENT_LIST " +
                        " LEFT JOIN MNT_USER_LIST ON MNT_ATTACHMENT_LIST.ATTACH_USER_ID = MNT_USER_LIST.USER_ID" +
                        " LEFT JOIN APP_LIST ON MNT_ATTACHMENT_LIST.ATTACH_CUSTOMER_ID = APP_LIST.CUSTOMER_ID " +
                        " AND MNT_ATTACHMENT_LIST.ATTACH_APP_ID = APP_LIST.APP_ID " +
                        " AND MNT_ATTACHMENT_LIST.ATTACH_APP_VER_ID = APP_LIST.APP_VERSION_ID";
                       // " and MNT_ATTACHMENT_LIST.ATTACH_USER_ID=APP_LIST.CREATED_BY";

					/*((BaseDataGrid)c1).FromClause = "MNT_ATTACHMENT_LIST " + 
						" LEFT JOIN MNT_USER_LIST ON MNT_ATTACHMENT_LIST.ATTACH_USER_ID = MNT_USER_LIST.USER_ID" + 
						" LEFT JOIN APP_LIST ON MNT_ATTACHMENT_LIST.ATTACH_CUSTOMER_ID = APP_LIST.CUSTOMER_ID "; //+ 
						//" AND MNT_ATTACHMENT_LIST.ATTACH_APP_ID = APP_LIST.APP_ID " + 
						//" AND MNT_ATTACHMENT_LIST.ATTACH_APP_VER_ID = APP_LIST.APP_VERSION_ID" ;*/
								
					//specifying conditions for where clause
					//((BaseDataGrid)c1).WhereClause = " (MNT_ATTACHMENT_LIST.IS_ACTIVE ='Y' AND MNT_ATTACHMENT_LIST.ATTACH_CUSTOMER_ID = " + hidEntityId.Value +  
					//								 " ) OR MNT_ATTACHMENT_LIST.ATTACH_ENT_ID = " + hidEntityId.Value;

                    ((BaseDataGrid)c1).WhereClause = " (MNT_ATTACHMENT_LIST.IS_ACTIVE ='Y' AND MNT_ATTACHMENT_LIST.ATTACH_CUSTOMER_ID = " + hidEntityId.Value + " )";// OR MNT_ATTACHMENT_LIST.ATTACH_ENTITY_TYPE = 'Customer'";

					//specifying Text to be shown in combo box
                    ((BaseDataGrid)c1).SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings1");// "File Name^File Type^Date Time Attached^User^File Description^Application Number^Application Version";
				
					//specifying column to be used for combo box
					((BaseDataGrid)c1).SearchColumnNames = "ATTACH_FILE_NAME^ATTACH_FILE_TYPE^ATTACH_DATE_TIME^USER_FNAME ! ' ' !  USER_LNAME^ATTACH_FILE_DESC^APP_NUMBER^APP_VERSION";
				
					//search column data type specifying data type of the column to be used for combo box
					((BaseDataGrid)c1).SearchColumnType="T^T^D^T^T^T^T";				
				
					//specifying column for order by clause
					((BaseDataGrid)c1).OrderByClause = "ATTACH_FILE_NAME ASC";
				
					//specifying column numbers of the query to be displyed in grid
					((BaseDataGrid)c1).DisplayColumnNumbers="2^3^4^5^6^8^9";
				
					//specifying column names from the query
					((BaseDataGrid)c1).DisplayColumnNames = "ATTACH_FILE_NAME^ATTACH_FILE_TYPE^ATTACH_DATE_TIME^" + 
						"USER_NAME^ATTACH_FILE_DESC^APP_NUMBER^APP_VERSION" ;	
				
					//specifying text to be shown as column headings
                    ((BaseDataGrid)c1).DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings1");//"File Name^File Type^Date Time Attached^User^File Description^Application Number^Application Version";
				
					//specifying column heading display text length
					((BaseDataGrid)c1).DisplayTextLength="50^50^50^50^50^35^10";
				
					//specifying width percentage for columns
					((BaseDataGrid)c1).DisplayColumnPercent="15^15^23^23^23^20^20";
				
					//specifying primary column number
					((BaseDataGrid)c1).PrimaryColumns="1";
				
					//specifying primary column name
					((BaseDataGrid)c1).PrimaryColumnsName="ATTACH_ID";
				
					//specifying column type of the data grid
					((BaseDataGrid)c1).ColumnTypes="B^B^B^B^B^B^B";
				
					//specifying links pages 
					//((BaseDataGrid)c1).ColumnsLink="client.aspx^webindex.aspx^webindex.aspx^webindex.aspx^webindex.aspx";
					//specifying if double click is allowed or not
				
					((BaseDataGrid)c1).AllowDBLClick="true"; 
				
					//specifying which columns are to be displayed on first tab
					((BaseDataGrid)c1).FetchColumns="1^2^3^4^5^6^7^8^9";

					// column numbers to create query string
					((BaseDataGrid)c1).QueryStringColumns ="ATTACH_ID^APP_NUMBER^APP_VERSION";
				
				}
				else if(hidEntityType.Value.ToUpper() == "APPLICATION" || hidEntityType.Value.ToUpper() == "POLICY")
				{				
					//specifying columns for select query
					/*((BaseDataGrid)c1).SelectClause = "ATTACH_ID,ATTACH_FILE_NAME,ATTACH_FILE_TYPE,Convert(varchar,ATTACH_DATE_TIME,103) ATTACH_DATE_TIME," + 
						"USER_FNAME + ' ' + USER_LNAME as USER_NAME,ATTACH_FILE_DESC," + 
						"CONVERT(VARCHAR(10),ATTACH_ID) + '.' + ATTACH_FILE_TYPE as FILE_FULL_NAME " ;*/
                    ((BaseDataGrid)c1).SelectClause = "ATTACH_ID,ATTACH_FILE_NAME,ATTACH_FILE_TYPE,isnull(convert(nvarchar(10),ATTACH_DATE_TIME,case when " + ClsCommon.BL_LANG_ID + "=1 then 101 else 103 end),'')as ATTACH_DATE_TIME," + 
						"USER_FNAME + ' ' + USER_LNAME as USER_NAME,ATTACH_FILE_DESC," +
                        "CONVERT(VARCHAR(10),ATTACH_ID) + '.' + ATTACH_FILE_TYPE as FILE_FULL_NAME ,ISNULL(MLVL.LOOKUP_UNIQUE_ID,MLV.LOOKUP_UNIQUE_ID) AS LOOKUP_UNIQUE_ID,ISNULL(MLVL.LOOKUP_VALUE_DESC,MLV.LOOKUP_VALUE_DESC) AS LOOKUP_VALUE_DESC ";
					
					//specifying tables for from clause
					((BaseDataGrid)c1).FromClause = " MNT_ATTACHMENT_LIST LEFT JOIN MNT_USER_LIST ON " +
                        "ATTACH_USER_ID = USER_ID inner join MNT_LOOKUP_VALUES MLV on MLV.LOOKUP_UNIQUE_ID = MNT_ATTACHMENT_LIST.ATTACH_TYPE LEFT OUTER JOIN MNT_LOOKUP_VALUES_MULTILINGUAL MLVL ON MNT_ATTACHMENT_LIST.ATTACH_TYPE =MLVL.LOOKUP_UNIQUE_ID AND MLVL.LANG_ID= " + ClsCommon.BL_LANG_ID + " ";
								
					//added by vj to modified the where clause for application attachment
					if (hidEntityType.Value.ToUpper().Trim() == "APPLICATION")
					{
						//specifying conditions for where clause
						//((BaseDataGrid)c1).WhereClause = " ATTACH_ENTITY_TYPE = '" + hidEntityType.Value + "' and ATTACH_ENT_ID='" + hidEntityId.Value + "' and ATTACH_CUSTOMER_ID='" + Request["CUSTOMER_ID"] + "' and ATTACH_APP_ID='" + Request["APP_ID"] + "' and ATTACH_APP_VER_ID='" + Request["APP_VERSION_ID"] + "' and MNT_ATTACHMENT_LIST.IS_ACTIVE='Y' ";
						((BaseDataGrid)c1).WhereClause = " ATTACH_ENTITY_TYPE = '" + hidEntityType.Value + "' and ATTACH_CUSTOMER_ID='" + Request["CUSTOMER_ID"] + "' and ATTACH_APP_ID='" + Request["APP_ID"] + "' and ATTACH_APP_VER_ID='" + Request["APP_VERSION_ID"] + "' and MNT_ATTACHMENT_LIST.IS_ACTIVE='Y' ";
					}					
					else
					{
						//specifying conditions for where clause
						//((BaseDataGrid)c1).WhereClause = " ATTACH_ENTITY_TYPE = '" + hidEntityType.Value + "'";// and ATTACH_ENT_ID='"; + hidEntityId.Value + "' ";
						//((BaseDataGrid)c1).WhereClause = " (ATTACH_ENTITY_TYPE = '" + hidEntityType.Value + "' and ATTACH_CUSTOMER_ID='" + Request["CUSTOMER_ID"] + "'" + "and ATTACH_POLICY_ID='" + GetPolicyID() + "' and ATTACH_POLICY_VER_TRACKING_ID='" + GetPolicyVersionID() +  "' or (ATTACH_APP_ID='" + Request["APP_ID"] + "' and ATTACH_APP_VER_ID='" + Request["APP_VERSION_ID"] + "' and ATTACH_CUSTOMER_ID='" + Request["CUSTOMER_ID"] + "')) and MNT_ATTACHMENT_LIST.IS_ACTIVE='Y' ";												
						((BaseDataGrid)c1).WhereClause = " (ATTACH_ENTITY_TYPE = '" + hidEntityType.Value + "' and ATTACH_CUSTOMER_ID='" + Request["CUSTOMER_ID"] + "'" + "and ATTACH_POLICY_ID='" + GetPolicyID() + "' and ATTACH_POLICY_VER_TRACKING_ID='" + GetPolicyVersionID() +  "' or (ATTACH_APP_ID='" + Request["APP_ID"] + "' and ATTACH_APP_VER_ID='" + Request["APP_VERSION_ID"] + "' and ATTACH_CUSTOMER_ID='" + Request["CUSTOMER_ID"] + "') or (ATTACH_ENTITY_TYPE = 'Claim' and ATTACH_CUSTOMER_ID='" + Request["CUSTOMER_ID"] + "'" + "and ATTACH_POLICY_ID='" + GetPolicyID() + "' and ATTACH_POLICY_VER_TRACKING_ID='" + GetPolicyVersionID() +  "')) and MNT_ATTACHMENT_LIST.IS_ACTIVE='Y' ";						

					}
				
					//specifying Text to be shown in combo box
                    ((BaseDataGrid)c1).SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings2");// File Name^File Type^Date Time Attached^User^File Description^Attachment Type";
				
					//specifying column to be used for combo box					
                    ((BaseDataGrid)c1).SearchColumnNames = "ATTACH_FILE_NAME^ATTACH_FILE_TYPE^ATTACH_DATE_TIME^USER_FNAME ! ' ' !  USER_LNAME^ATTACH_FILE_DESC^ISNULL(MLVL.LOOKUP_UNIQUE_ID,MLV.LOOKUP_UNIQUE_ID)";
				
					//search column data type specifying data type of the column to be used for combo box					
					((BaseDataGrid)c1).SearchColumnType="T^T^D^T^T^L";
					((BaseDataGrid)c1).DropDownColumns="^^^^^doc_type";
				
					//specifying column for order by clause
					((BaseDataGrid)c1).OrderByClause = "ATTACH_FILE_NAME ASC";
				
					//specifying column numbers of the query to be displyed in grid
					((BaseDataGrid)c1).DisplayColumnNumbers="2^3^4^5^6^10";
				
					//specifying column names from the query
					((BaseDataGrid)c1).DisplayColumnNames = "ATTACH_FILE_NAME^ATTACH_FILE_TYPE^ATTACH_DATE_TIME^" + 
						"USER_NAME^ATTACH_FILE_DESC^LOOKUP_VALUE_DESC" ;	
				
					//specifying text to be shown as column headings
                    ((BaseDataGrid)c1).DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings2");// "File Name^File Type^Date Time Attached^User^File Description^Attachment Type";
				
					//specifying column heading display text length
					((BaseDataGrid)c1).DisplayTextLength="50^50^50^50^50^50";
				
					//specifying width percentage for columns
					((BaseDataGrid)c1).DisplayColumnPercent="15^10^19^15^20^21";
				
					//specifying primary column number
					((BaseDataGrid)c1).PrimaryColumns="1";
				
					//specifying primary column name
					((BaseDataGrid)c1).PrimaryColumnsName="ATTACH_ID";
				
					//specifying column type of the data grid
					((BaseDataGrid)c1).ColumnTypes="B^B^B^B^B^B";
				
					//specifying links pages 
					//((BaseDataGrid)c1).ColumnsLink="client.aspx^webindex.aspx^webindex.aspx^webindex.aspx^webindex.aspx";
					//specifying if double click is allowed or not
				
					((BaseDataGrid)c1).AllowDBLClick="true"; 
				
					//specifying which columns are to be displayed on first tab
					((BaseDataGrid)c1).FetchColumns="1^2^3^4^5^6^7^9^10";

					// column numbers to create query string
					((BaseDataGrid)c1).QueryStringColumns ="ATTACH_ID";
				}
				else if(hidEntityType.Value.ToUpper() == "CLAIM")
				{
					//specifying columns for select query
					/*((BaseDataGrid)c1).SelectClause = "ATTACH_ID,ATTACH_FILE_NAME,ATTACH_FILE_TYPE,Convert(varchar,ATTACH_DATE_TIME,103) ATTACH_DATE_TIME," + 
						"USER_FNAME + ' ' + USER_LNAME as USER_NAME,ATTACH_FILE_DESC," + 
						"CONVERT(VARCHAR(10),ATTACH_ID) + '.' + ATTACH_FILE_TYPE as FILE_FULL_NAME " ;*/
                    ((BaseDataGrid)c1).SelectClause = "ATTACH_ID,ATTACH_FILE_NAME,ATTACH_FILE_TYPE,isnull(convert(nvarchar(10),ATTACH_DATE_TIME,case when " + ClsCommon.BL_LANG_ID + "=1 then 101 else 103 end),'')as ATTACH_DATE_TIME," + 
						"USER_FNAME + ' ' + USER_LNAME as USER_NAME,ATTACH_FILE_DESC," + 
						"CONVERT(VARCHAR(10),ATTACH_ID) + '.' + ATTACH_FILE_TYPE as FILE_FULL_NAME ,MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID, MNT_LOOKUP_VALUES.LOOKUP_VALUE_DESC  " ;
					
					//specifying tables for from clause
					((BaseDataGrid)c1).FromClause = " MNT_ATTACHMENT_LIST LEFT JOIN MNT_USER_LIST ON " + 
						"ATTACH_USER_ID = USER_ID inner join MNT_LOOKUP_VALUES on MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID = MNT_ATTACHMENT_LIST.ATTACH_TYPE " ;
								
					if(hidEntityType.Value.ToUpper() == "CLAIM")
					{
                        ((BaseDataGrid)c1).WhereClause = " ATTACH_ENTITY_TYPE = '" + hidEntityType.Value + "' and ATTACH_CUSTOMER_ID='" + Request["CUSTOMER_ID"] + "'  and ATTACH_ENT_ID = '" + hidEntityId.Value + "' AND MNT_ATTACHMENT_LIST.IS_ACTIVE='Y' ";
					}
					else
					{
						//specifying conditions for where clause
						//((BaseDataGrid)c1).WhereClause = " ATTACH_ENTITY_TYPE = '" + hidEntityType.Value + "'";// and ATTACH_ENT_ID='"; + hidEntityId.Value + "' ";
						//((BaseDataGrid)c1).WhereClause = " (ATTACH_ENTITY_TYPE = '" + hidEntityType.Value + "' and ATTACH_CUSTOMER_ID='" + Request["CUSTOMER_ID"] + "'" + "and ATTACH_POLICY_ID='" + GetPolicyID() + "' and ATTACH_POLICY_VER_TRACKING_ID='" + GetPolicyVersionID() +  "' or (ATTACH_APP_ID='" + Request["APP_ID"] + "' and ATTACH_APP_VER_ID='" + Request["APP_VERSION_ID"] + "' and ATTACH_CUSTOMER_ID='" + Request["CUSTOMER_ID"] + "')) and MNT_ATTACHMENT_LIST.IS_ACTIVE='Y' ";												
						((BaseDataGrid)c1).WhereClause = " (ATTACH_ENTITY_TYPE = '" + hidEntityType.Value + "' and ATTACH_CUSTOMER_ID='" + Request["CUSTOMER_ID"] + "'" + "and ATTACH_POLICY_ID='" + GetPolicyID() + "' and ATTACH_POLICY_VER_TRACKING_ID='" + GetPolicyVersionID() +  "' or (ATTACH_APP_ID='" + Request["APP_ID"] + "' and ATTACH_APP_VER_ID='" + Request["APP_VERSION_ID"] + "' and ATTACH_CUSTOMER_ID='" + Request["CUSTOMER_ID"] + "') or (ATTACH_ENTITY_TYPE = 'Claim' and ATTACH_CUSTOMER_ID='" + Request["CUSTOMER_ID"] + "'" + "and ATTACH_POLICY_ID='" + GetPolicyID() + "' and ATTACH_POLICY_VER_TRACKING_ID='" + GetPolicyVersionID() +  "')) and MNT_ATTACHMENT_LIST.IS_ACTIVE='Y' ";						

					}
				
					//specifying Text to be shown in combo box
					((BaseDataGrid)c1).SearchColumnHeadings =objResourceMgr.GetString("SearchColumnHeadings3");//File Name^File Type^Date Time Attached^User^File Description";
				
					//specifying column to be used for combo box					
					((BaseDataGrid)c1).SearchColumnNames = "ATTACH_FILE_NAME^ATTACH_FILE_TYPE^ATTACH_DATE_TIME^USER_FNAME ! ' ' !  USER_LNAME^ATTACH_FILE_DESC";
				
					//search column data type specifying data type of the column to be used for combo box					
					((BaseDataGrid)c1).SearchColumnType="T^T^D^T^T";
					//((BaseDataGrid)c1).DropDownColumns="^^^^^doc_type";
				
					//specifying column for order by clause
					((BaseDataGrid)c1).OrderByClause = "ATTACH_FILE_NAME ASC";
				
					//specifying column numbers of the query to be displyed in grid
					((BaseDataGrid)c1).DisplayColumnNumbers="2^3^4^5^6";
				
					//specifying column names from the query
					((BaseDataGrid)c1).DisplayColumnNames = "ATTACH_FILE_NAME^ATTACH_FILE_TYPE^ATTACH_DATE_TIME^" + 
						"USER_NAME^ATTACH_FILE_DESC" ;	
				
					//specifying text to be shown as column headings
                    ((BaseDataGrid)c1).DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings3"); //"File Name^File Type^Date Time Attached^User^File Description";
				
					//specifying column heading display text length
					((BaseDataGrid)c1).DisplayTextLength="50^50^50^50^50";
				
					//specifying width percentage for columns
					((BaseDataGrid)c1).DisplayColumnPercent="15^10^19^15^20";
				
					//specifying primary column number
					((BaseDataGrid)c1).PrimaryColumns="1";
				
					//specifying primary column name
					((BaseDataGrid)c1).PrimaryColumnsName="ATTACH_ID";
				
					//specifying column type of the data grid
					((BaseDataGrid)c1).ColumnTypes="B^B^B^B^B";
				
					//specifying links pages 
					//((BaseDataGrid)c1).ColumnsLink="client.aspx^webindex.aspx^webindex.aspx^webindex.aspx^webindex.aspx";
					//specifying if double click is allowed or not
				
					((BaseDataGrid)c1).AllowDBLClick="true"; 
				
					//specifying which columns are to be displayed on first tab
					((BaseDataGrid)c1).FetchColumns="1^2^3^4^5^6^7^9";

					// column numbers to create query string
					((BaseDataGrid)c1).QueryStringColumns ="ATTACH_ID";
				}
				else				
				{				
					//specifying columns for select query
                    ((BaseDataGrid)c1).SelectClause = "ATTACH_ID,ATTACH_FILE_NAME,ATTACH_FILE_TYPE,isnull(convert(nvarchar(10),ATTACH_DATE_TIME,case when " + ClsCommon.BL_LANG_ID + "=1 then 101 else 103 end),'')as ATTACH_DATE_TIME," + 
						"USER_FNAME + ' ' + USER_LNAME as USER_NAME,ATTACH_FILE_DESC," + 
						"CONVERT(VARCHAR(10),ATTACH_ID) + '.' + ATTACH_FILE_TYPE as FILE_FULL_NAME " ;
					/*((BaseDataGrid)c1).SelectClause = "ATTACH_ID,ATTACH_FILE_NAME,ATTACH_FILE_TYPE, convert(char,ATTACH_DATE_TIME,101) ATTACH_DATE_TIME," + 
						"USER_FNAME + ' ' + USER_LNAME as USER_NAME,ATTACH_FILE_DESC," + 
						"CONVERT(VARCHAR(10),ATTACH_ID) + '.' + ATTACH_FILE_TYPE as FILE_FULL_NAME " ;*/
					
					//specifying tables for from clause
					((BaseDataGrid)c1).FromClause = " MNT_ATTACHMENT_LIST LEFT JOIN MNT_USER_LIST ON " + 
						"ATTACH_USER_ID = USER_ID " ;
					
								
					//added by vj to modified the where clause for application attachment
					/*if (hidEntityType.Value.ToUpper().Trim() == "APPLICATION")
					{
						//specifying conditions for where clause
						//((BaseDataGrid)c1).WhereClause = " ATTACH_ENTITY_TYPE = '" + hidEntityType.Value + "' and ATTACH_ENT_ID='" + hidEntityId.Value + "' and ATTACH_CUSTOMER_ID='" + Request["CUSTOMER_ID"] + "' and ATTACH_APP_ID='" + Request["APP_ID"] + "' and ATTACH_APP_VER_ID='" + Request["APP_VERSION_ID"] + "' and MNT_ATTACHMENT_LIST.IS_ACTIVE='Y' ";
						((BaseDataGrid)c1).WhereClause = " ATTACH_ENTITY_TYPE = '" + hidEntityType.Value + "' and ATTACH_CUSTOMER_ID='" + Request["CUSTOMER_ID"] + "' and ATTACH_APP_ID='" + Request["APP_ID"] + "' and ATTACH_APP_VER_ID='" + Request["APP_VERSION_ID"] + "' and MNT_ATTACHMENT_LIST.IS_ACTIVE='Y' ";
					}
					else
					{*/
					//specifying conditions for where clause
					((BaseDataGrid)c1).WhereClause = " ATTACH_ENTITY_TYPE = '" + hidEntityType.Value + "' and ATTACH_ENT_ID='" + hidEntityId.Value + "'";
					//((BaseDataGrid)c1).WhereClause = " ATTACH_ENTITY_TYPE = '" + hidEntityType.Value + "' and ATTACH_CUSTOMER_ID='" + Request["CUSTOMER_ID"] + "'" + "and ATTACH_POLICY_ID='" + GetPolicyID() + "' and ATTACH_POLICY_VER_TRACKING_ID='" + GetPolicyVersionID() +  "' or (ATTACH_APP_ID='" + Request["APP_ID"] + "' and ATTACH_APP_VER_ID='" + Request["APP_VERSION_ID"] + "' and ATTACH_CUSTOMER_ID='" + Request["CUSTOMER_ID"] + "') and MNT_ATTACHMENT_LIST.IS_ACTIVE='Y' ";
					//}
				
					//specifying Text to be shown in combo box
                    ((BaseDataGrid)c1).SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");// "File Name^File Type^Date Time Attached^User^File Description" ;
				
					//specifying column to be used for combo box
					//((BaseDataGrid)c1).SearchColumnNames = "ATTACH_FILE_NAME^ATTACH_FILE_TYPE^REPLACE(Convert(varchar,ATTACH_DATE_TIME,109),' ','')^USER_FNAME ! ' ' !  USER_LNAME^ATTACH_FILE_DESC";
					((BaseDataGrid)c1).SearchColumnNames = "ATTACH_FILE_NAME^ATTACH_FILE_TYPE^ATTACH_DATE_TIME^USER_FNAME ! ' ' !  USER_LNAME^ATTACH_FILE_DESC";
				
					//search column data type specifying data type of the column to be used for combo box
					//((BaseDataGrid)c1).SearchColumnType="T^T^T^T^T";
					((BaseDataGrid)c1).SearchColumnType="T^T^D^T^T";
				
					//specifying column for order by clause
					((BaseDataGrid)c1).OrderByClause = "ATTACH_FILE_NAME ASC";
				
					//specifying column numbers of the query to be displyed in grid
					((BaseDataGrid)c1).DisplayColumnNumbers="2^3^4^5^6";
				
					//specifying column names from the query
					((BaseDataGrid)c1).DisplayColumnNames = "ATTACH_FILE_NAME^ATTACH_FILE_TYPE^ATTACH_DATE_TIME^" + 
						"USER_NAME^ATTACH_FILE_DESC" ;	
				
					//specifying text to be shown as column headings
                    ((BaseDataGrid)c1).DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");// "File Name^File Type^Date Time Attached^User^File Description";
				
					//specifying column heading display text length
					((BaseDataGrid)c1).DisplayTextLength="50^50^50^50^50";
				
					//specifying width percentage for columns
					((BaseDataGrid)c1).DisplayColumnPercent="15^15^23^23^23";
				
					//specifying primary column number
					((BaseDataGrid)c1).PrimaryColumns="1";
				
					//specifying primary column name
					((BaseDataGrid)c1).PrimaryColumnsName="ATTACH_ID";
				
					//specifying column type of the data grid
					((BaseDataGrid)c1).ColumnTypes="B^B^B^B^B";
				
					//specifying links pages 
					//((BaseDataGrid)c1).ColumnsLink="client.aspx^webindex.aspx^webindex.aspx^webindex.aspx^webindex.aspx";
					//specifying if double click is allowed or not
				
					((BaseDataGrid)c1).AllowDBLClick="true"; 
				
					//specifying which columns are to be displayed on first tab
					((BaseDataGrid)c1).FetchColumns="1^2^3^4^5^6^7";

					// column numbers to create query string
					((BaseDataGrid)c1).QueryStringColumns ="ATTACH_ID";				
				}

				//specifying message to be shown
                ((BaseDataGrid)c1).SearchMessage = objResourceMgr.GetString("SearchMessage");//Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button
				
				//specifying buttons to be displayed on grid
                ((BaseDataGrid)c1).ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add^0^addRecord";
				
				//specifying number of the rows to be shown
				((BaseDataGrid)c1).PageSize=int.Parse(GetPageSize());//int.Parse(System.Configuration.ConfigurationSettings.AppSettings.Get("GRID_PAGE_SIZE"));
				
				//specifying cache size (use for top clause)
				((BaseDataGrid)c1).CacheSize=int.Parse(GetCacheSize());//int.Parse(System.Configuration.ConfigurationSettings.AppSettings.Get("GRID_CACHE_SIZE"));
				
				//specifying image path
				((BaseDataGrid)c1).ImagePath=System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim()+ "/images/collapse_icon.gif";
                ((BaseDataGrid)c1).HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				
				//specifying heading
                ((BaseDataGrid)c1).HeaderString = objResourceMgr.GetString("HeaderString");//Attachment Information
				
				((BaseDataGrid)c1).SelectClass = colors;
				
				//specifying text to be shown for filter checkbox
				//((BaseDataGrid)c1).FilterLabel="Show Complete";
				
				//specifying column to be used for filtering
				//((BaseDataGrid)c1).FilterColumnName="LISTOPEN";
				
				//value of filtering record
				//((BaseDataGrid)c1).FilterValue="Y";
				
				// property indiacating whether query string is required or not
				((BaseDataGrid)c1).RequireQuery ="Y";
				
				//Default Search
				((BaseDataGrid)c1).DefaultSearch = "Y";

				// to show completed task we have to give check box
				GridHolder.Controls.Add(c1);

				// Condition to check whether the this page is open from the 
				// customer details so that Button "Back to Search" is visible 
				// only for attachment page open for entering customer details.
				
				if (Request.QueryString["BackOption"] != null && Request.QueryString["BackOption"].ToString()=="Y")
				{
					if(Request.QueryString["EntityName"] != null)
						TabCtl.TabURLs = "AddAttachment.aspx?EntityId=" + hidEntityId.Value + "&EntityType=" + hidEntityType.Value + "&EntityName=" + Request.QueryString["EntityName"].ToString() + "&Grid=web&BackOption=Y&CalledFrom=" + strCalledFrom + "&CUSTOMER_ID=" + Request["CUSTOMER_ID"] +"&APP_ID="+ Request["APP_ID"] + "&APP_VERSION_ID="+ Request["APP_VERSION_ID"]  + "&";
					else
						TabCtl.TabURLs = "AddAttachment.aspx?EntityId=" + hidEntityId.Value + "&EntityType=" + hidEntityType.Value + "&Grid=web&BackOption=Y&CalledFrom=" + strCalledFrom + "&CUSTOMER_ID=" + Request["CUSTOMER_ID"] +"&APP_ID="+ Request["APP_ID"] + "&APP_VERSION_ID="+ Request["APP_VERSION_ID"]  + "&";
				}
				else
				{
					if(Request.QueryString["EntityName"] != null)
						TabCtl.TabURLs = "AddAttachment.aspx?EntityId=" + hidEntityId.Value + "&EntityType=" + hidEntityType.Value + "&EntityName=" + Request.QueryString["EntityName"].ToString() + "&Grid=web&CalledFrom=" + strCalledFrom + "&CUSTOMER_ID=" + Request["CUSTOMER_ID"] +"&APP_ID="+ Request["APP_ID"] + "&APP_VERSION_ID="+ Request["APP_VERSION_ID"]  + "&";
					else
						TabCtl.TabURLs = "AddAttachment.aspx?EntityId=" + hidEntityId.Value + "&EntityType=" + hidEntityType.Value + "&Grid=web&CalledFrom=" + strCalledFrom + "&CUSTOMER_ID=" + Request["CUSTOMER_ID"] +"&APP_ID="+ Request["APP_ID"] + "&APP_VERSION_ID="+ Request["APP_VERSION_ID"]  + "&";
				
				}
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
            TabCtl.TabURLs = "AddAttachment.aspx?EntityId=" + hidEntityId.Value + "&EntityType=" + hidEntityType.Value + " &CalledFrom=" + strCalledFrom + "&";
            TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
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
