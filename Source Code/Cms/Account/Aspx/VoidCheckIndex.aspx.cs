/******************************************************************************************
<Author					: -   Ajit Singh Chahal
<Start Date				: -	6/28/2005 10:27:46 AM
<End Date				: -	
<Description			: - 	Code behind for Check Information grid.
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
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
using Cms.CmsWeb.WebControls;
using Cms.BusinessLayer.BlAccount;
using Cms.Model.Account;
using Cms.BusinessLayer.BlCommon;

namespace Cms.Account.Aspx
{
	/// <summary>
	/// chedck Information Grid
	/// </summary>
	public class VoidCheckIndex :Cms.Account.AccountBase	
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
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidJournalInfoXML;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidVoidClicked;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDelString;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidKeyValues;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCheckedRowIDs;
		private string strSelect = "";
		public string strSecurity = "";
		

		private void Page_Load(object sender, System.EventArgs e)
		{
			lblMessage.Visible = false;
			if(Request.QueryString["Mode"] != null && Request.QueryString["Mode"].ToString().ToUpper().Equals("REGISTER"))
				base.ScreenId="207"; // Check Register
			else
				base.ScreenId="206"; // Void Checks

			//Set Security XML
			strSecurity = gstrSecurityXML;
			if(!IsPostBack)
			{
				//Changes made against itrack #5615
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
			}
			else
			{	
				VoidCheck();
			}


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

			#region loading web grid control
			
			Control c1 = LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			BaseDataGrid objWebGrid;
			objWebGrid = (BaseDataGrid)c1;

			try
			{
				string CheckTypeValue=",'All' as CHECK_TYPE_ID";
				string GroupByClause ="";
				string GroupByClauseVoid="";
				if(cmbCHECK_TYPE.SelectedValue=="")
					objWebGrid.WhereClause = "1<0";	
				else if(cmbCHECK_TYPE.SelectedValue=="0")//All is selected
				{ //checks which are committed but not voided
					objWebGrid.WhereClause = " isnull(IS_COMMITED,'N')='Y' "; // and isNull(GL_UPDATE,'0')<=1";				
				}
				else if(cmbCHECK_TYPE.SelectedValue=="1")//OverPayment,Suspense Payment,Cancellation check Refund 
				{ //checks which are committed but not voided
					objWebGrid.WhereClause = " LOOKUP_CHECK_ID IN (2474,9935,9936) AND isnull(IS_COMMITED,'N')='Y' "; 
				}
				else
				{
					CheckTypeValue = ",chk.CHECK_TYPE as CHECK_TYPE_ID";
					//checks which are committed but not voided

					objWebGrid.WhereClause = "LOOKUP_CHECK_ID= '" + cmbCHECK_TYPE.SelectedValue + "' and isnull(IS_COMMITED,'N')='Y' " ; //and isNull(GL_UPDATE,'0')<=1";
				}
				if(Request.QueryString["Mode"]!=null && Request.QueryString["Mode"].ToString().ToUpper().Equals("REGISTER") )
				{
					TabCtl.TabURLs="AddCheck.aspx?CHECK_TYPE_ID="+cmbCHECK_TYPE.SelectedValue+"&"+"CalledFrom=Register"+"&";
				}
				GroupByClause = " group by chk.CREATED_DATETIME,chk.DATE_COMMITTED,mnt_lookup_values.LOOKUP_VALUE_DESC,chk.PAYMENT_MODE,"
					+" chk.CHECK_NUMBER,ACC.ACC_DISP_NUMBER,ACC.ACC_DESCRIPTION,chk.PAYEE_ENTITY_NAME,chk.CHECK_AMOUNT,chk.CHECK_ID,"
					+" chk.CREATED_DATETIME,chk.PAYEE_ENTITY_ID,chk.CHECK_TYPE,chk.IS_COMMITED,chk.GL_UPDATE,mnt_lookup_values.LOOKUP_UNIQUE_ID";

				GroupByClauseVoid = " group by chk.CREATED_DATETIME,chk.DATE_COMMITTED,mnt_lookup_values.LOOKUP_VALUE_DESC,chk.PAYMENT_MODE,"
					+" chk.CHECK_NUMBER,ACC.ACC_DISP_NUMBER,ACC.ACC_DESCRIPTION,chk.PAYEE_ENTITY_NAME,chk.CHECK_AMOUNT,chk.CHECK_ID,"
					+" chk.CREATED_DATETIME,chk.PAYEE_ENTITY_ID,chk.CHECK_TYPE,chk.IS_COMMITED,chk.GL_UPDATE,mnt_lookup_values.LOOKUP_UNIQUE_ID,chk.CHECK_DATE,chk.IS_PRINTED";

				//Setting web grid control properties
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";

				
				//Move local TO VSS.
				objWebGrid.SearchMessage = "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				if(Request.QueryString["Mode"]!=null && Request.QueryString["Mode"].ToString().ToUpper().Equals("REGISTER") )
				{
					strSelect = " (Select convert(varchar,chk.CREATED_DATETIME,101) as CREATED_DATETIME,chk.DATE_COMMITTED "; 
					strSelect += " as CHECK_DATE,mnt_lookup_values.lookup_value_desc + case when chk.payment_mode = 11788 or chk.payment_mode = 11976 then ' (EFT Process)' else '' end	as CHECK_TYPE,RIGHT('000000'+chk.CHECK_NUMBER,6) CHECK_NUMBER,ACC.ACC_DISP_NUMBER ";
					strSelect += " + '-' +ACC.ACC_DESCRIPTION as ACC_DESCRIPTION,chk.PAYEE_ENTITY_NAME,convert(varchar(30),convert(money,SUM(IsNull(chk.CHECK_AMOUNT,0))),1)check_amount, chk.CHECK_AMOUNT ";
					strSelect += " as AppliedAmount, chk.CHECK_ID,convert(varchar,chk.CREATED_DATETIME,101) as CREATED_DATETIME1,'All' ";
					strSelect += " as CHECK_TYPE_ID,chk.PAYEE_ENTITY_ID,CHECK_TYPE AS CHECK_TYPE_ID1,IS_COMMITED,GL_UPDATE,mnt_lookup_values.lookup_unique_id as LOOKUP_CHECK_ID ";
					strSelect += " ,case when chk.payment_mode = 11788 or chk.payment_mode = 11976 then  case GL_UPDATE when 2 then 'Voided(EFT)' else '' end   else  case GL_UPDATE when 2 then 'Voided' else '' end end as STATUS ";
					strSelect += " ,convert(money,SUM(IsNull(chk.CHECK_AMOUNT,0))) check_amount_1 ";
					strSelect += " FROM ACT_CHECK_INFORMATION chk with(nolock) LEFT JOIN ACT_GL_ACCOUNTS ACC with(nolock) ON ACC.ACCOUNT_ID = CHK.ACCOUNT_ID ";
					strSelect += " INNER JOIN mnt_lookup_values with(nolock) ON LOOKUP_UNIQUE_ID=chk.CHECK_TYPE where chk.CHECK_TYPE <> 9937 and ISNULL(chk.PAYMENT_MODE,0) <> 11974 " + GroupByClause + ") Test ";
					objWebGrid.SelectClause = " * ";
					objWebGrid.FromClause = strSelect;
				
					objWebGrid.PrimaryColumns = "6";
					objWebGrid.PrimaryColumnsName = "CHECK_ID";
				
					objWebGrid.AllowDBLClick = "true";
					objWebGrid.FetchColumns = "1^6";
					objWebGrid.SearchColumnHeadings = "Date Created^Transaction Date^Check Type^Check #^From Account^Payee^Check Amount^Status";
					objWebGrid.SearchColumnNames = "CAST(CONVERT(VARCHAR,CREATED_DATETIME,101) AS DATETIME)^CAST(CONVERT(VARCHAR,CHECK_DATE,101) AS DATETIME)^CHECK_TYPE^CHECK_NUMBER^ACC_DESCRIPTION^PAYEE_ENTITY_NAME^CHECK_AMOUNT^case GL_UPDATE when 2 then 'Voided' else '' end";
					objWebGrid.SearchColumnType = "D^D^T^T^T^T^T^T";			
					objWebGrid.DisplayColumnNumbers = "1^2^3^4^5^6^17";
					objWebGrid.DisplayColumnNames = "CREATED_DATETIME1^CHECK_DATE^CHECK_TYPE^CHECK_NUMBER^ACC_DESCRIPTION^PAYEE_ENTITY_NAME^CHECK_AMOUNT^STATUS";
					objWebGrid.DisplayColumnHeadings = "Date Created^Transaction date^Check Type^Check #^From Account^Payee^Check Amount^Status";
					objWebGrid.DisplayTextLength = "10^10^15^5^20^20^10^10";
					objWebGrid.DisplayColumnPercent = "10^10^15^5^20^20^10^10";
					//Modified by Asfa(18-June-2008) - iTrack #3906(Note: 1)
					//objWebGrid.ColumnTypes = "B^B^B^B^B^B^B^B";
					objWebGrid.ColumnTypes = "B^B^B^B^B^B^N^B";
					objWebGrid.HeaderString = "Check Register";
					objWebGrid.OrderByClause = "CHECK_DATE ,CHECK_NUMBER DESC";	
				}
				else
				{
					strSelect = " (Select convert(varchar,chk.CREATED_DATETIME,101) as CREATED_DATETIME,convert(varchar,chk.CHECK_DATE,101) "; 
					strSelect += " as CHECK_DATE,mnt_lookup_values.lookup_value_desc + case when chk.payment_mode = 11788  or chk.payment_mode = 11976 then ' (EFT Process)' else '' end	as CHECK_TYPE,RIGHT('000000'+chk.CHECK_NUMBER,6) CHECK_NUMBER ,ACC.ACC_DISP_NUMBER ";
					strSelect += " + '-' +ACC.ACC_DESCRIPTION as ACC_DESCRIPTION,chk.PAYEE_ENTITY_NAME,convert(varchar(30),convert(money,SUM(IsNull(chk.CHECK_AMOUNT,0))),1)check_amount,convert(varchar(30),convert(money,SUM(IsNull(chk.CHECK_AMOUNT,0))),1) ";
					strSelect += " as AppliedAmount, chk.CHECK_ID,convert(varchar,chk.CREATED_DATETIME,101) as CREATED_DATETIME1,'All' ";
					strSelect += " as CHECK_TYPE_ID,chk.PAYEE_ENTITY_ID,CHECK_TYPE AS CHECK_TYPE_ID1,IS_COMMITED,GL_UPDATE,mnt_lookup_values.lookup_unique_id as LOOKUP_CHECK_ID ";
					strSelect += " ,case when chk.payment_mode = 11788 or chk.payment_mode = 11976 then 'EFT' else case IS_PRINTED when 'Y' then 'Printed' else 'Not Printed' end end as STATUS ";
					strSelect += " ,convert(money,SUM(IsNull(chk.CHECK_AMOUNT,0))) check_amount_1, convert(money,SUM(IsNull(chk.CHECK_AMOUNT,0))) AppliedAmount_1 ";
					strSelect += " FROM ACT_CHECK_INFORMATION chk with(nolock) LEFT JOIN ACT_GL_ACCOUNTS ACC with(nolock) ON ACC.ACCOUNT_ID = CHK.ACCOUNT_ID ";
					strSelect += " INNER JOIN mnt_lookup_values with(nolock) ON LOOKUP_UNIQUE_ID=chk.CHECK_TYPE  WHERE IS_BNK_RECONCILED <> 'Y' AND GL_UPDATE <> 2 and ISNULL(chk.PAYMENT_MODE,0) <> 11974 and  isnull(chk.CHECK_TYPE,0) <> 9937" + GroupByClauseVoid + ") Test ";
					objWebGrid.SelectClause = " * ";
					objWebGrid.FromClause = strSelect;
				
					objWebGrid.PrimaryColumns = "6";
					objWebGrid.PrimaryColumnsName = "CHECK_ID";
				
					objWebGrid.AllowDBLClick = "true";
					objWebGrid.FetchColumns = "1^6";
					objWebGrid.SearchColumnHeadings = "Date Created^Transaction Date^Check Type^Check #^From Account^Payee^Check Amount^Amount To Be Paid^Status";
					objWebGrid.SearchColumnNames = "CAST(CONVERT(VARCHAR,CREATED_DATETIME,101) AS DATETIME)^CAST(CONVERT(VARCHAR,CHECK_DATE,101) AS DATETIME)^CHECK_TYPE^CHECK_NUMBER^ACC_DESCRIPTION^PAYEE_ENTITY_NAME^CHECK_AMOUNT^AppliedAmount^STATUS";
					objWebGrid.SearchColumnType = "D^D^T^T^T^T^T^T^T";			
					objWebGrid.DisplayColumnNumbers = "1^2^3^4^5^6^7^17";
					objWebGrid.DisplayColumnNames = "CREATED_DATETIME1^CHECK_DATE^CHECK_TYPE^CHECK_NUMBER^ACC_DESCRIPTION^PAYEE_ENTITY_NAME^CHECK_AMOUNT^AppliedAmount^STATUS";
					objWebGrid.DisplayColumnHeadings = "Date Created^Transaction date^Check Type^Check #^From Account^Payee^Check Amount^Amount To Be Paid^Status";
					objWebGrid.DisplayTextLength = "11^11^23^10^13^13^13^13^13";
					objWebGrid.DisplayColumnPercent = "11^11^23^10^13^13^13^13^13";

					if(ClsCommon.CheckSecurity(strSecurity))
						objWebGrid.ExtraButtons = "1^Void^0^VoidCheck";
					//Modified by Asfa(18-June-2008) - iTrack #3906(Note: 1)
					//objWebGrid.ColumnTypes = "B^B^B^B^B^B^B^B^B";
					objWebGrid.ColumnTypes = "B^B^B^B^B^B^N^N^B";
					objWebGrid.HeaderString = "Voiding Checks";
					objWebGrid.OrderByClause = "CREATED_DATETIME1 ,CHECK_NUMBER ASC";	
				}
				objWebGrid.RequireCheckbox ="Y";
				objWebGrid.PageSize = int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.SelectClass = colors;	
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch = "Y";
				objWebGrid.QueryStringColumns = "CHECK_ID^CHECK_TYPE_ID1^PAYEE_ENTITY_ID";
				
				objWebGrid.CellHorizontalAlign="7^8";
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void VoidCheck()
		{
			//int intRetVal=0;
			ArrayList al =new ArrayList();
			Cms.Model.Account.ClsChecksInfo objChecksInfo; 
			try
			{
				if (hidCheckedRowIDs.Value !="" && hidCheckedRowIDs.Value !=null)
				{
					String [] arrRows = hidCheckedRowIDs.Value.Split('~');
					for ( int ctr=0;  ctr < arrRows.Length ; ctr++)
					{
						objChecksInfo = new 	Cms.Model.Account.ClsChecksInfo();
						objChecksInfo.CHECK_ID = int.Parse(arrRows[ctr]);
						objChecksInfo.MODIFIED_BY = int.Parse(GetUserId());
						objChecksInfo.LAST_UPDATED_DATETIME = DateTime.Now;
						al.Add(objChecksInfo);
					}
				
					//if(Request.Form["hidlocQueryStr"]!=null && Request.Form["hidlocQueryStr"].ToString().Length>0 && hidVoidClicked.Value.ToString().ToUpper()=="CLICKED")
					ArrayList alStatus;
					Cms.BusinessLayer.BlAccount.ClsChecks objClsChecks = new Cms.BusinessLayer.BlAccount.ClsChecks(); 
			
					if(objClsChecks.VoidCheck(al,out alStatus)>0)
					{
						//lblMessage.Text =  "Checks Voided Successfully";// ClsMessages.FetchGeneralMessage("29");
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("424");
						lblMessage.Visible = true;
					}
					else
					{
						//lblMessage.Text = "Unable to Void the check/'s. Try again later.";//ClsMessages.FetchGeneralMessage("29");		
						lblMessage.Visible=true;
						if( alStatus.Contains(-1))			// Record not existed, hence exiting 
						{
							lblMessage.Text			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("425");
						}
							//else if( intRetVal == -2 )			// Record already voided hence exiting
						else if (alStatus.Contains(-2))	
						{
							lblMessage.Text			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("426");
						}
						else 
						{
							lblMessage.Text			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("427");
						}
						lblMessage.Visible = true;
						hidVoidClicked.Value=""; 
					}
				}
				
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			finally
			{
				
			}
		}//
	}
}
