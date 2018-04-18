/******************************************************************************************
	<Author					: - > Anurag Verma
	<Start Date				: -	> 28/06/2005
	<Description			: - > This file is used to implement webgrid control for the Policy Search Page 

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
using System.Resources;
using System.Reflection;

namespace Cms.Application.Policy
{
	/// <summary>
	/// Summary description for PolicyIndex.
	/// </summary>
	public class PolicyIndex : Cms.Application.appbase  
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

        string rootPath = System.Configuration.ConfigurationManager.AppSettings.Get("CmsPath").Trim();

        ResourceManager objResourceMgr = null ;

		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId = "120_2";
            objResourceMgr = new ResourceManager("Cms.Application.Policy.PolicyIndex", Assembly.GetExecutingAssembly());

			//Setting the cookie value
			SetCookieValue();
			SetSecurityXML(base.ScreenId,int.Parse(GetUserId())); 
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
            
			int customer_ID=GetCustomerID()==""?0:int.Parse(GetCustomerID());
            
			//Gettting the policy processes 
			string strProcess = GetPolicyProcess();
			string strCarrierSystemID = CarrierSystemID.ToUpper();
			string strSystemID = GetSystemId().ToUpper();			

			if(customer_ID!=0)
			{
         
				#region loading web grid control
				Control c1= LoadControl("../../cmsweb/webcontrols/BaseDataGrid.ascx");
				try
				{        
					
					//specifying webservice URL
					((BaseDataGrid)c1).WebServiceURL=httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
					
					//specifying columns for select query					
					((BaseDataGrid)c1).SelectClause =" * ";
					
					//specifying tables for from clause					
					//((BaseDataGrid)c1).FromClause="  ( select distinct convert( decimal(4,1), Isnull(ppcl.policy_disp_version,0)) policy_disp_version,isnull(ppcl.policy_number,'') policy_number,isnull(ppcl.policy_lob,'') policy_lob,dbo.fun_GetPolicyDisplayStatus(ppcl.customer_id,ppcl.policy_id,ppcl.policy_version_id) policy_status,isnull(convert(varchar(15),ppcl.app_effective_date,101) ,'') policy_effective_date,isnull(convert(varchar(15),ppcl.APP_EXPIRATION_DATE,101),'') policy_expiration_date,isnull(LV.LOB_DESC,'') LOB1,ppcl.policy_id,ppcl.app_id,ppcl.app_version_id,ppcl.customer_id customer_id,LV.LOB_ID,ppcl.policy_version_id,1 DRP,'GO' as GO,'" + strProcess + "' as process_value,STATE_CODE,ISNULL(AL.APP_NUMBER,'') + ' ' + ISNULL(AL.APP_VERSION,'') APP_NUMBER,CASE WHEN PPCL.POLICY_STATUS = 'INACTIVE' THEN 'N' ELSE 'Y' END AS STATUS_POLICY ,   PPCL.BILL_TYPE AS BILL_TYPE_AB_DB,case when quote_xml is not null then '<img src=" + (rootPath) +   "/cmsweb/images/quote.gif style=''Border-Width:0'' ImageAlign=absMiddle>'  else '' end  as quote_xml ,isnull(convert(varchar(15),ppcl.POL_VER_EFFECTIVE_DATE,101),'') policy_VER_EFFECTIVE_date,PPCL.AGENCY_ID,AG.AGENCY_CODE FROM POL_CUSTOMER_POLICY_LIST ppcl left join MNT_LOB_MASTER LV on ppcl.policy_lob=LV.LOB_ID LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MSCL ON MSCL.STATE_ID=ppcl.STATE_ID LEFT JOIN POL_POLICY_STATUS_MASTER STATUS_MASTER ON STATUS_MASTER.POLICY_STATUS_CODE = PPCL.POLICY_STATUS LEFT JOIN APP_LIST AL ON PPCL.APP_ID = AL.APP_ID AND PPCL.APP_VERSION_ID = AL.APP_VERSION_ID AND PPCL.CUSTOMER_ID = AL.CUSTOMER_ID LEFT OUTER JOIN QOT_CUSTOMER_QUOTE_LIST_POL QC ON PPCL.CUSTOMER_ID = QC.CUSTOMER_ID AND PPCL.POLICY_ID = QC.POLICY_ID AND PPCL.POLICY_VERSION_ID = QC.POLICY_VERSION_ID LEFT JOIN MNT_AGENCY_LIST AG ON AG.AGENCY_ID=PPCL.AGENCY_ID  ) ppcl ";
                    ((BaseDataGrid)c1).FromClause = @" (select distinct ppcl.policy_disp_version 
                     policy_disp_version,isnull(ppcl.policy_number,'') AS policy_number,isnull(ppcl.policy_lob,'') 
                     policy_lob,dbo.fun_GetPolicyDisplayStatus(ppcl.customer_id,ppcl.policy_id,ppcl.policy_version_id," + GetLanguageID() + @")
                     + CASE WHEN PPP.REVERT_BACK  = 'Y' THEN 
					 CASE WHEN " + GetLanguageID() + @"=2 THEN  
                     '(Cancelar)' ELSE  '(Undo)' END
                       ELSE '' END 
                     AS policy_status,
                     isnull(convert(varchar(15),ppcl.app_effective_date,case " + GetLanguageID() + @" when 2 then 103 else 101 end) ,'') AS policy_effective_date,
                     isnull(convert(varchar(15),ppcl.APP_EXPIRATION_DATE,case " + GetLanguageID() + @" when 2 then 103 else 101 end),'') AS policy_expiration_date,
                     isnull(LVM.LOB_DESC,LV.LOB_DESC) LOB1, ppcl.policy_id,ppcl.app_id,ppcl.app_version_id,ppcl.customer_id customer_id,
                     LV.LOB_ID,ppcl.policy_version_id,1 DRP, case " + GetLanguageID() + @" when 2 then 'IR' else 'GO' end as [GO]
                     ,'" + strProcess + @"' as process_value,ISNULL(STATE_CODE,case " + GetLanguageID() + @" when 2 then 'Todos' else 'All' end) AS STATE_CODE,ISNULL(ppcl.APP_NUMBER,'') + ' ' + ISNULL(ppcl.APP_VERSION,'') APP_NUMBER,
                     CASE WHEN PPCL.POLICY_STATUS IN('INACTIVE','EXPIRED') THEN 'N' ELSE 'Y' END AS STATUS_POLICY ,   
                     case when LVM.LANG_ID=2 then case when ppcl.BILL_TYPE='DB'then 'DIR' else 'AB'end   else PPCL.BILL_TYPE end   AS BILL_TYPE_AB_DB,case when quote_xml is not null 
                     then '<img src=" + (rootPath) +   @"/cmsweb/images/quote.gif style=''Border-Width:0'' ImageAlign=absMiddle>'  else '' end  as quote_xml ,
                     isnull(convert(varchar(15),ppcl.POL_VER_EFFECTIVE_DATE,case " + GetLanguageID() + @" when 2 then 103 else 101 end),'') 
                     policy_VER_EFFECTIVE_date,pol_VER_EFFECTIVE_date,PPCL.AGENCY_ID,AG.AGENCY_CODE 
                     FROM POL_CUSTOMER_POLICY_LIST ppcl with(nolock) 
                     left OUTER join MNT_LOB_MASTER LV with(nolock) on ppcl.policy_lob=LV.LOB_ID 
                     left OUTER join MNT_LOB_MASTER_MULTILINGUAL LVM with(nolock) on ppcl.policy_lob=LVM.LOB_ID and LVM.LANG_ID = " + GetLanguageID() + @"
                     LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MSCL with(nolock) ON MSCL.STATE_ID=ppcl.STATE_ID 
                     LEFT OUTER JOIN QOT_CUSTOMER_QUOTE_LIST_POL QC with(nolock) ON PPCL.CUSTOMER_ID = QC.CUSTOMER_ID AND PPCL.POLICY_ID = QC.POLICY_ID 
                     AND PPCL.POLICY_VERSION_ID = QC.POLICY_VERSION_ID 
                     LEFT OUTER JOIN MNT_AGENCY_LIST AG with(nolock) ON AG.AGENCY_ID=PPCL.AGENCY_ID LEFT OUTER JOIN POL_POLICY_PROCESS PPP WITH(NOLOCK) ON PPP.CUSTOMER_ID = ppcl.customer_id and 
                     PPP.POLICY_ID = ppcl.POLICY_ID and PPP.NEW_POLICY_VERSION_ID = ppcl.POLICY_VERSION_ID
                     where ppcl.APP_STATUS<>'REJECT'  ) ppcl ";

                    ((BaseDataGrid)c1).OrderByClause = "policy_disp_version desc";// changed by praveer for itrack no 1568
					if(strSystemID.ToUpper()==strCarrierSystemID.ToUpper())
					{
						//specifying conditions for where clause					
                        ((BaseDataGrid)c1).WhereClause = " CUSTOMER_ID=" + customer_ID + "AND UPPER(RTRIM(LTRIM(ISNULL(policy_number,'')))) <> '' ";
					}
					else
					{
                        ((BaseDataGrid)c1).WhereClause = " CUSTOMER_ID=" + customer_ID + " AND AGENCY_CODE='" + strSystemID + "'" + "AND UPPER(RTRIM(LTRIM(ISNULL(policy_number,'')))) <> '' ";
					}

					//specifying Text to be shown in combo box					
                    ((BaseDataGrid)c1).SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings"); //"Policy #^Policy Version^Status^App #^Effective Date^Expiration Date^Change Effective Date^LOB^State^Bill Type";
					//specifying column to be used for combo box
                    ((BaseDataGrid)c1).SearchColumnNames = "policy_number^policy_disp_version^policy_status^ISNULL(APP_NUMBER,'')^policy_effective_date^policy_expiration_date^pol_VER_EFFECTIVE_date^LOB_ID^BILL_TYPE_AB_DB";					
					((BaseDataGrid)c1).SearchColumnType="T^T^T^T^D^D^D^L^T";
					//specifying column for order by clause
                    ((BaseDataGrid)c1).OrderByClause = "policy_disp_version desc";// changed by praveer for itrack no 1568
					if(strSystemID.ToUpper()==strCarrierSystemID.ToUpper())
					{
						//search column data type specifying data type of the column to be used for combo box						
						((BaseDataGrid)c1).DropDownColumns="^^^^^^^LOB^";
						//specifying column numbers of the query to be displyed in grid						
						((BaseDataGrid)c1).DisplayColumnNumbers="1^5^19^6^7^8^15^16^18^20^21^22";						
						//specifying column names from the query						
                        ((BaseDataGrid)c1).DisplayColumnNames="policy_number^policy_disp_version^policy_status^APP_NUMBER^policy_effective_date^policy_expiration_date^policy_VER_EFFECTIVE_date^LOB1^DRP^GO^BILL_TYPE_AB_DB^QUOTE_XML";						
						//specifying text to be shown as column headings						
                        ((BaseDataGrid)c1).DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Pol. #^Ver.^Status^App #^Eff. Date^Exp. Date^Change Eff. Date^LOB^state^Process^Go^Bill Type^Rate"; //Added by Sibin for Itrack Issue 5130 on 05 Dec 08
						//specifying column heading display text length						
						((BaseDataGrid)c1).DisplayTextLength="50^50^50^50^50^50^50^50^50^50^50^50";
						//specifying width percentage for columns						
						((BaseDataGrid)c1).DisplayColumnPercent="7^5^10^10^5^5^5^10^10^5^5^5"; //Added by Sibin for Itrack Issue 5130 on 05 Dec 08
						//specifying column type of the data grid						
						if(Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("Write",gstrSecurityXML) == "Y")	
							((BaseDataGrid)c1).ColumnTypes="B^B^B^B^B^B^B^B^DL^BUT^B^IMGLNK";					
						else
							((BaseDataGrid)c1).ColumnTypes="B^B^B^B^B^B^B^B^DL^B^B^IMGLNK";	
						
						((BaseDataGrid)c1).ColumnsLink="^^^^^^^^^^^^Quotegenerator.aspx";
						
					}
					else
					{
						((BaseDataGrid)c1).DropDownColumns="^^^^^^^LOB^";
						//specifying column numbers of the query to be displyed in grid
						((BaseDataGrid)c1).DisplayColumnNumbers="1^2^5^19^6^7^8^15^20";						
						//specifying column names from the query
						((BaseDataGrid)c1).DisplayColumnNames="policy_number^policy_disp_version^policy_status^APP_NUMBER^policy_effective_date^policy_expiration_date^policy_VER_EFFECTIVE_date^LOB1^BILL_TYPE_AB_DB";
						//specifying text to be shown as column headings
                        ((BaseDataGrid)c1).DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadingsNonCarr");//"Pol. #^Ver.^Status^App #^Eff. Date^Exp. Date^Change Eff. Date^LOB^State^Bill Type";
						//specifying column heading display text length
						((BaseDataGrid)c1).DisplayTextLength="50^50^50^50^50^50^50^50^50";
						//specifying width percentage for columns
						((BaseDataGrid)c1).DisplayColumnPercent="10^5^20^15^10^10^10^5^5";
						//specifying column type of the data grid
						((BaseDataGrid)c1).ColumnTypes="B^B^B^B^B^B^B^B^B";
						((BaseDataGrid)c1).ColumnsLink= rootPath + "policies/aspx/policytab.aspx?"; 
					}
					//specifying primary column number
					((BaseDataGrid)c1).PrimaryColumns="12^9^10^11";
					//specifying primary column name
					((BaseDataGrid)c1).PrimaryColumnsName="ppcl.customer_id^ppcl.policy_id^ppcl.policy_version_id^ppcl.LOB_ID";				
					
					
					//specifying if double click is allowed or not
					((BaseDataGrid)c1).AllowDBLClick="true"; 
					//specifying which columns are to be displayed on first tab
					((BaseDataGrid)c1).FetchColumns="1^2^4^5^6^7^8^9^10^11^12^13^14^20^22";
					//specifying message to be shown
                    ((BaseDataGrid)c1).SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                    //((BaseDataGrid)c1).ExtraButtons = objResourceMgr.GetString("ExtraButtons"); //Added by Charles on 10-Mar-10 for Policy Page Implementation
					//specifying buttons to be displayed on grid
					//((BaseDataGrid)c1).ExtraButtons="1^Add^0^addRecord";
					//specifying number of the rows to be shown
					((BaseDataGrid)c1).PageSize =  int.Parse (GetPageSize());
					//specifying cache size (use for top clause)
					((BaseDataGrid)c1).CacheSize = int.Parse (GetCacheSize());

					((BaseDataGrid)c1).Grouping                 = "Y";
                
					//Commented By PAwan
					//objWebGrid.GroupQueryColumns        = "customer_id^CUSTOMER_ID~STATE_NAME1~QQ_ID~QQ_TYPE~QQ_NUMBER~LOB_DESC^customer_id~app_id~app_version_id^customer_id~policy_id~policy_version_id~app_version_id~app_id^customer_id~app_id~app_version_id~policy_id"; 
					((BaseDataGrid)c1).GroupQueryColumns        = "policy_number"; 


					//specifying image path
                    ((BaseDataGrid)c1).ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    ((BaseDataGrid)c1).HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
					//specifying heading
                    ((BaseDataGrid)c1).HeaderString = objResourceMgr.GetString("HeaderString");//"Policy Search";
					((BaseDataGrid)c1).SelectClass = colors;
					//specifying text to be shown for filter checkbox
                    ((BaseDataGrid)c1).FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";
					//specifying column to be used for filtering
					((BaseDataGrid)c1).FilterColumnName="STATUS_POLICY";
					//value of filtering record
					((BaseDataGrid)c1).FilterValue="Y";
					// property indiacating whether query string is required or not
					((BaseDataGrid)c1).RequireQuery ="Y";
					((BaseDataGrid)c1).RequireDropdownList  ="Y";
					// column numbers to create query string
					//((BaseDataGrid)c1).QueryStringColumns ="customer_id^policy_id^policy_version_id";
					((BaseDataGrid)c1).QueryStringColumns ="customer_id^policy_id^policy_version_id^app_id^app_version_id";
                
					// to show completed task we have to give check box
					GridHolder.Controls.Add(c1);
				}
				catch(Exception ex)
				{
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				}
        
				#endregion
			}

		}

		private void SetCookieValue ()
		{
			Response.Cookies["LastVisitedTab"].Value = "1";
			Response.Cookies["LastVisitedTab"].Expires = DateTime.Now.Add(new TimeSpan(30,0,0,0,0));
		}

		/// <summary>
		/// Gives comma seperated process description
		/// </summary>
		private string GetPolicyProcess()
		{
			System.Text.StringBuilder sbProcess = new System.Text.StringBuilder();

			try
			{
				Cms.BusinessLayer.BlAccount.ClsProcess objProcess = new Cms.BusinessLayer.BlAccount.ClsProcess();
				DataTable dt = objProcess.GetProcess();

				foreach(DataRow dr in dt.Rows)
				{
					if (sbProcess.ToString() == "")
					{
						sbProcess.Append(dr["PROCESS_DESC"].ToString() + "^" + dr["PROCESS_SHORTNAME"].ToString());
					}
					else
					{
						sbProcess.Append("," + dr["PROCESS_DESC"].ToString() + "^" + dr["PROCESS_SHORTNAME"].ToString());
					}
				}

			}
			catch (Exception objExp)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
			}
			return sbProcess.ToString();
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{			
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
