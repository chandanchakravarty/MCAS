/******************************************************************************************
<Author					: -   Ajit Singh Chahal
<Start Date				: -	6/28/2005 10:27:46 AM
<End Date				: -	
<Description			: - 	Code behind for Check Information grid.
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 25/08/2005
<Modified By			: - Anurag Verma
<Purpose				: - Applying Null Check for buttons on aspx page 
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
using Cms.BusinessLayer.BlCommon;

namespace Cms.Account.Aspx
{
	/// <summary>
	/// chedck Information Grid
	/// </summary>
	public class CheckIndex : Cms.Account.AccountBase	
	{
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.WebControls.Label lblEntryNo;
		protected System.Web.UI.WebControls.Label lblDate;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label lblProof;
		protected System.Web.UI.WebControls.Label capCHECK_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbCHECK_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCHECK_TYPE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidKeyValues;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidGridRowClickNumber;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCheckedRowIDs;
		public string strSecurity = "";
		

		private void Page_Load(object sender, System.EventArgs e)
		{
			SetScreenId();	
			strSecurity = gstrSecurityXML;

			
			if(hidCheckedRowIDs.Value!="")
				Commit();
			
			base.ScreenId="";
			if(!IsPostBack)
			{
				//Changes made against itrack #5615.
				Cms.BusinessLayer.BlCommon.ClsCommon.BindLookupDDL(ref cmbCHECK_TYPE,"CPAYTP");
				cmbCHECK_TYPE.Items.Insert(0,new ListItem("All","0"));
				cmbCHECK_TYPE.Items.Insert(5,new ListItem("Premium Refund Checks","1"));
				ListItem Li = new ListItem();
				Li = cmbCHECK_TYPE.Items.FindByText("Claims Checks");
				cmbCHECK_TYPE.Items.Remove(Li);
				Li = cmbCHECK_TYPE.Items.FindByText("Checks for Cancellation and Change Premium Payment");
				cmbCHECK_TYPE.Items.Remove(Li);
				Li = cmbCHECK_TYPE.Items.FindByText("Premium Refund Checks for Over Payment");
				cmbCHECK_TYPE.Items.Remove(Li);
				Li = cmbCHECK_TYPE.Items.FindByText("Premium Refund Checks for Suspense Amount");
				cmbCHECK_TYPE.Items.Remove(Li);
				lblMessage.Visible = false;
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
					colors=System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR2").ToString();     
					break;
				case 3:
					colors=System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR3").ToString();     
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

			#region loading web grid control
			
			Control c1 = LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			BaseDataGrid objWebGrid;
			objWebGrid = (BaseDataGrid)c1;
			// vinv.CREATED_DATETIME1,1 
			//ven.VendorName, 9
			//vinv.INVOICE_NUM, 4
			//APPROVED_BY 11
			//vinv.TRANSACTION_DATE,6
			//vinv.DUE_DATE, 7
			//vinv.INVOICE_AMOUNT 8
			//AppliedAmount 12
			//RemainingAmount 13

			//dbo.MNT_USER_LIST.USER_ID 10
			// vinv.VENDOR_ID,3
			//vinv.INVOICE_ID, 2
			//vinv.REF_PO_NUM, 5

			try
			{
				string CheckTypeValue=",'All' as CHECK_TYPE_ID";
				string WhereClause = "";
				string GroupByClause ="";

				if(cmbCHECK_TYPE.SelectedValue=="")
				{
					WhereClause = "1<0";	
				}
				else if(cmbCHECK_TYPE.SelectedValue=="0")//All is selected
				{
					WhereClause = " isnull(IS_COMMITED,'N')<>'Y' and CHECK_TYPE <> 9937 and ISNULL(PAYMENT_MODE,0) <> 11974"; //Remove Claim Checks and Payment Mode Credit card
				}			
				else if(cmbCHECK_TYPE.SelectedValue=="1")//OverPayment, Suspense Payment, Cancellation Refund Checks
				{
					WhereClause = " CHECK_TYPE IN (2474,9935,9936) AND isnull(IS_COMMITED,'N')<>'Y' and CHECK_TYPE <> 9937 and ISNULL(PAYMENT_MODE,0) <> 11974"; //Remove Claim Checks and Payment Mode Credit card
				}
				else
				{
					WhereClause = "CHECK_TYPE="+cmbCHECK_TYPE.SelectedValue+" and isnull(IS_COMMITED,'N')<>'Y' and CHECK_TYPE <> 9937 and ISNULL(PAYMENT_MODE,0) <> 11974";//Remove Claim Checks
				}
				//
				if(cmbCHECK_TYPE.SelectedValue=="2472" || cmbCHECK_TYPE.SelectedValue=="9938")//Agency n Vendor
				{
					GroupByClause = " group by chk.CREATED_DATETIME,chk.CHECK_TYPE,REQ_SPECIAL_HANDLING,chk.PAYEE_ENTITY_NAME,chk.CHECK_AMOUNT,chk.CHECK_ID,chk.CREATED_DATETIME,chk.PAYEE_ENTITY_ID,chk.CHECK_TYPE";				
				}
				else
				{ 
					//Special handling Added For Itrack #Issue 5509
					GroupByClause = " group by chk.CREATED_DATETIME,chk.CHECK_TYPE,MAL.REQ_SPECIAL_HANDLING,VEN.REQ_SPECIAL_HANDLING,chk.PAYEE_ENTITY_NAME,chk.CHECK_AMOUNT,chk.CHECK_ID,chk.CREATED_DATETIME,chk.PAYEE_ENTITY_ID,chk.CHECK_TYPE";
				}

				TabCtl.TabURLs="AddCheck.aspx?CHECK_TYPE_ID="+cmbCHECK_TYPE.SelectedValue+"&";//i.e. 0
				//TabCtl.TabURLs="AddCheck.aspx?CHECK_TYPE_ID="+cmbCHECK_TYPE.SelectedValue+"&"+"CalledFrom=EditC"+"&";

				//Setting web grid control properties
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				
				objWebGrid.SelectClause = "  *  ";
				
				//objWebGrid.FromClause = " ( SELECT convert(varchar,chk.CREATED_DATETIME,101) as CREATED_DATETIME,convert(varchar,chk.CHECK_DATE,101) as CHECK_DATE,(select lookup_value_desc from mnt_lookup_values with(nolock) where LOOKUP_UNIQUE_ID=chk.CHECK_TYPE) as CHECK_TYPE,chk.CHECK_NUMBER,chk.PAYEE_ENTITY_NAME,chk.CHECK_AMOUNT,chk.CHECK_AMOUNT as AppliedAmount,chk.CHECK_ID,convert(varchar,chk.CREATED_DATETIME,101) as CREATED_DATETIME1"+CheckTypeValue+",chk.PAYEE_ENTITY_ID,CHECK_TYPE AS CHECK_TYPE_ID1 from ACT_CHECK_INFORMATION chk with(nolock) where " + WhereClause + ") Test ";								
				if(cmbCHECK_TYPE.SelectedValue=="9938")//Vendor
				{
					objWebGrid.FromClause = " ( SELECT convert(varchar,chk.CREATED_DATETIME,101) as CREATED_DATETIME,(select lookup_value_desc from mnt_lookup_values with(nolock) where LOOKUP_UNIQUE_ID=chk.CHECK_TYPE) as CHECK_TYPE,chk.PAYEE_ENTITY_NAME,CASE isnull(MVL.REQ_SPECIAL_HANDLING,10964) WHEN 10963 THEN 'Yes'  WHEN 10964 THEN 'No' END  AS  REQ_SPECIAL_HANDLING,convert(varchar(30),convert(money,SUM(IsNull(chk.CHECK_AMOUNT,0))),1)check_amount,chk.CHECK_AMOUNT as AppliedAmount,chk.CHECK_ID,convert(varchar,chk.CREATED_DATETIME,101) as CREATED_DATETIME1"+CheckTypeValue+",chk.PAYEE_ENTITY_ID,CHECK_TYPE AS CHECK_TYPE_ID1, convert(money,SUM(IsNull(chk.CHECK_AMOUNT,0))) check_amount_1 from ACT_CHECK_INFORMATION chk with(nolock) inner JOIN mnt_vendor_list MVL ON chk.PAYEE_ENTITY_ID = MVL.VENDOR_ID where " + WhereClause + GroupByClause + " ) Test ";
				}
				else if(cmbCHECK_TYPE.SelectedValue=="2472")//Agency
				{
					//Special handling Added For Itrack #Issue 5509
					//Inner Join Change in to Left Join For Itrack Issue 6472
					objWebGrid.FromClause = " ( SELECT convert(varchar,chk.CREATED_DATETIME,101) as CREATED_DATETIME,(select lookup_value_desc from mnt_lookup_values with(nolock) where LOOKUP_UNIQUE_ID=chk.CHECK_TYPE) as CHECK_TYPE,chk.PAYEE_ENTITY_NAME,CASE isnull(MAL.REQ_SPECIAL_HANDLING,10964) WHEN 10963 THEN 'Yes' WHEN 10964 THEN 'No' END  AS  REQ_SPECIAL_HANDLING,convert(varchar(30),convert(money,SUM(IsNull(chk.CHECK_AMOUNT,0))),1)check_amount,chk.CHECK_AMOUNT as AppliedAmount,chk.CHECK_ID,convert(varchar,chk.CREATED_DATETIME,101) as CREATED_DATETIME1"+CheckTypeValue+",chk.PAYEE_ENTITY_ID,CHECK_TYPE AS CHECK_TYPE_ID1, convert(money,SUM(IsNull(chk.CHECK_AMOUNT,0))) check_amount_1 from ACT_CHECK_INFORMATION chk with(nolock)left JOIN MNT_AGENCY_LIST MAL ON chk.PAYEE_ENTITY_ID = MAL.AGENCY_ID where " + WhereClause + GroupByClause + " ) Test ";
				}
				else
				{  
					objWebGrid.FromClause = " ( SELECT convert(varchar,chk.CREATED_DATETIME,101) as CREATED_DATETIME,(select lookup_value_desc from mnt_lookup_values with(nolock) where LOOKUP_UNIQUE_ID=chk.CHECK_TYPE) as CHECK_TYPE,chk.PAYEE_ENTITY_NAME,CASE CHK.CHECK_TYPE WHEN '2472' THEN CASE ISNULL(MAL.REQ_SPECIAL_HANDLING,10964) WHEN 10963 THEN 'YES' WHEN 10964 THEN 'NO' END ELSE CASE CHK.CHECK_TYPE WHEN '9938' THEN CASE ISNULL(VEN.REQ_SPECIAL_HANDLING,10964) WHEN 10963 THEN 'YES' WHEN 10964 THEN 'NO' END ELSE 'NO' END END AS  REQ_SPECIAL_HANDLING,convert(varchar(30),convert(money,SUM(IsNull(chk.CHECK_AMOUNT,0))),1)check_amount,chk.CHECK_AMOUNT as AppliedAmount,chk.CHECK_ID,convert(varchar,chk.CREATED_DATETIME,101) as CREATED_DATETIME1"+CheckTypeValue+",chk.PAYEE_ENTITY_ID,CHECK_TYPE AS CHECK_TYPE_ID1, convert(money,SUM(IsNull(chk.CHECK_AMOUNT,0))) check_amount_1 from ACT_CHECK_INFORMATION chk LEFT JOIN MNT_AGENCY_LIST MAL on chk.PAYEE_ENTITY_ID = MAL.AGENCY_ID AND CHECK_TYPE = 2472 AND  CHK.COMM_TYPE <> 'CAC' LEFT JOIN  MNT_VENDOR_LIST VEN ON CHK.PAYEE_ENTITY_ID = VEN.VENDOR_ID AND CHK.CHECK_TYPE = 9938  and CHECK_TYPE <> 9937 where " + WhereClause + GroupByClause + " ) Test ";				
				} 
				/*objWebGrid.SelectClause = " convert(varchar,chk.CREATED_DATETIME,101) as CREATED_DATETIME,convert(varchar,chk.CHECK_DATE,101) as CHECK_DATE,(select lookup_value_desc from mnt_lookup_values with(nolock) where LOOKUP_UNIQUE_ID=chk.CHECK_TYPE) as CHECK_TYPE,chk.CHECK_NUMBER,chk.PAYEE_ENTITY_NAME,chk.CHECK_AMOUNT,";
				objWebGrid.SelectClause +=" chk.CHECK_AMOUNT as AppliedAmount,";
				objWebGrid.SelectClause +=" chk.CHECK_ID,convert(varchar,chk.CREATED_DATETIME,101) as CREATED_DATETIME1"+CheckTypeValue+",chk.PAYEE_ENTITY_ID,CHECK_TYPE AS CHECK_TYPE_ID1"; 
				objWebGrid.FromClause = " ACT_CHECK_INFORMATION chk with(nolock) "; */

				//objWebGrid.SearchColumnHeadings = "Date Created^Transaction Date^Check Number^Payee^Check Amount";
				objWebGrid.SearchColumnHeadings = "Date Created^Payee^Check Amount";
				//objWebGrid.SearchColumnNames = "CREATED_DATETIME^CHECK_DATE^CHECK_NUMBER^PAYEE_ENTITY_NAME^CHECK_AMOUNT";
				objWebGrid.SearchColumnNames = "CAST(CONVERT(VARCHAR,CREATED_DATETIME,101) AS DATETIME)^PAYEE_ENTITY_NAME^CHECK_AMOUNT";
//				objWebGrid.SearchColumnType = "D^D^T^T^T";			
				objWebGrid.SearchColumnType = "D^T^T";			
				objWebGrid.OrderByClause = "CREATED_DATETIME1 ASC";				
				//objWebGrid.DisplayColumnNumbers = "1^2^3^4^5";
				if(cmbCHECK_TYPE.SelectedValue=="9938" || cmbCHECK_TYPE.SelectedValue=="2472"  )
				{ 
					objWebGrid.DisplayColumnNumbers = "1^2^3^4";
					//objWebGrid.DisplayColumnNames = "CREATED_DATETIME1^CHECK_DATE^CHECK_TYPE^CHECK_NUMBER^PAYEE_ENTITY_NAME^CHECK_AMOUNT";
					objWebGrid.DisplayColumnNames = "CREATED_DATETIME1^CHECK_TYPE^PAYEE_ENTITY_NAME^CHECK_AMOUNT^REQ_SPECIAL_HANDLING";
					//objWebGrid.DisplayColumnHeadings = "Date Created^Transaction date^Check Type^Check #^Payee^Check Amount";
					objWebGrid.DisplayColumnHeadings = "Date Created^Check Type^Payee^Check Amount^Req Special Handling";
					//objWebGrid.DisplayTextLength = "12^15^15^27^15";
					//objWebGrid.DisplayTextLength = "15^15^10^15^15^20";
					//objWebGrid.DisplayColumnPercent = "15^15^10^15^15^20";
					objWebGrid.DisplayTextLength = "20^20^25^20^5^10";
					objWebGrid.DisplayColumnPercent = "20^20^25^20^5^10";
					objWebGrid.ColumnTypes = "B^B^B^N^B";
					
				}
				else
				{
					objWebGrid.DisplayColumnNumbers = "1^2^3^4";
					//objWebGrid.DisplayColumnNames = "CREATED_DATETIME1^CHECK_DATE^CHECK_TYPE^CHECK_NUMBER^PAYEE_ENTITY_NAME^CHECK_AMOUNT";
					//Special handling Added For Itrack #Issue 5509
					objWebGrid.DisplayColumnNames = "CREATED_DATETIME1^CHECK_TYPE^PAYEE_ENTITY_NAME^CHECK_AMOUNT^REQ_SPECIAL_HANDLING";
					//objWebGrid.DisplayColumnHeadings = "Date Created^Transaction date^Check Type^Check #^Payee^Check Amount";
					objWebGrid.DisplayColumnHeadings = "Date Created^Check Type^Payee^Check Amount^Req Special Handling";
					//objWebGrid.DisplayTextLength = "12^15^15^27^15";
					objWebGrid.DisplayTextLength = "20^20^25^20^5^10";
					objWebGrid.DisplayColumnPercent = "20^20^25^20^5^10";	
					objWebGrid.ColumnTypes = "B^B^B^N^B";
					
				
				}
				objWebGrid.PrimaryColumns = "6";
				objWebGrid.PrimaryColumnsName = "CHECK_ID";
				//objWebGrid.ColumnTypes = "B^B^B^B^B^B";				
				//Modified by Asfa(18-June-2008) - iTrack #3906(Note: 1)
				//objWebGrid.ColumnTypes = "B^B^B^B";
				//objWebGrid.ColumnTypes = "B^B^B^N";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^6";
				objWebGrid.SearchMessage = "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				//objWebGrid.ExtraButtons = "5^Add New~Items To Be Paid~Preview Item List~Print Item List~Preview check^0~1~2~3~4~5^addRecord~ShowItemsToBePaid~ShowPreviewItemList~ShowPrintItwmList~ShowPreviewCheck";
				//objWebGrid.ExtraButtons = "4^Preview Item List~Print Item List~Preview check~Commit^0~1~2~3^ShowPreviewItemList~ShowPrintItwmList~ShowPreviewCheck~DeleteRecords";

			
				if(ClsCommon.CheckSecurity(strSecurity))
                    objWebGrid.ExtraButtons = "1^Commit^0^DeleteRecords";
				objWebGrid.PageSize = int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
				objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim()+ "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.HeaderString = "Check Information";
				objWebGrid.SelectClass = colors;	
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch = "Y";
				objWebGrid.QueryStringColumns = "CHECK_ID^CHECK_TYPE_ID1^PAYEE_ENTITY_ID";//last two are only for items to be paid.
				// Check box 
				objWebGrid.RequireCheckbox = "Y";
				objWebGrid.CellHorizontalAlign="4";
				
				//Adding to controls to gridholder
				GridHolder.Controls.Add(c1);

				
			}
			catch
			{
			}
			#endregion

		
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
			this.cmbCHECK_TYPE.SelectedIndexChanged += new System.EventHandler(this.cmbCHECK_TYPE_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void cmbCHECK_TYPE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		

		private void SetScreenId()
		{
			base.ScreenId="204";
		}

		/// <summary>
		/// Delets a diary entry from the database
		/// </summary>
		private void Commit()
		{
             bool is_Special_Handling = false;
			ArrayList al =new ArrayList();
			Cms.Model.Account.ClsChecksInfo objCheck; 
			try
			{
				String [] arrRows = hidCheckedRowIDs.Value.Split('~');

				for ( int ctr=0;  ctr < arrRows.Length ; ctr++)
				{
					objCheck = new 	Cms.Model.Account.ClsChecksInfo();
					objCheck.CHECK_ID = int.Parse(arrRows[ctr]);
					objCheck.IS_COMMITED="Y";
					objCheck.DATE_COMMITTED	=	DateTime.Now;
					objCheck.COMMITED_BY	=  int.Parse(GetUserId());
					objCheck.MODIFIED_BY = int.Parse(GetUserId());
					objCheck.LAST_UPDATED_DATETIME = DateTime.Now;

					al.Add(objCheck);
				}
			
				ArrayList alStatus;
				Cms.BusinessLayer.BlAccount.ClsChecks objClsChecks = new Cms.BusinessLayer.BlAccount.ClsChecks(); 
			
				int RetVal = objClsChecks.Commit(al,out alStatus);
				if(RetVal>0)
					lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("342");
				else if(RetVal == -4)
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("204_0","23");
				else if(RetVal == -7)
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("204_0","27");
                else if(RetVal == -10)
					is_Special_Handling = true; 
					//lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("204_0","28"); 
				else
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("204_0","25");

                if ( objClsChecks.Check_Special_handling == true )
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage("204_0","28");
				
				lblMessage.Visible=true;
			}
			catch(Exception ex)
			{
				throw ex;
			}

		}

		
	}
}
