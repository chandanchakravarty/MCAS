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
using Cms.BusinessLayer.BlAccount;
using Cms.Model.Account;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb;
using Cms.CmsWeb.Controls;
using Cms.CmsWeb.WebControls;
using Cms.BusinessLayer.BlCommon.Accounting;
using Cms.BusinessLayer.BlCommon;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;
using APToolkitNET;
using System.Configuration;



namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for CheckPDFPrint.
	/// </summary>
	public class CheckPDFPrint :  Cms.Account.AccountBase
	{
		protected Cms.CmsWeb.Controls.CmsButton btnPrint;
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
		protected System.Web.UI.WebControls.Literal litReport;
		private string strSelect = "";
		//DataSet dsChecksInfo = null;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAccount;

		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
/*			rfvACCOUNT_ID.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			rfvFromCheckNumber.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			rfvToCheckNumber.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
			revFromCheckNumber.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
			revToCheckNumber.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");

			revFromCheckNumber.ValidationExpression = aRegExpInteger;
			revToCheckNumber.ValidationExpression = aRegExpInteger;
*/
		}
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId  = "218";
			btnPrint.CmsButtonClass			=	CmsButtonType.Execute;
			btnPrint.PermissionString		=	gstrSecurityXML;
			lblMessage.Visible = false;
			if(!IsPostBack)
			{
				//Changes made against itrack #5615.
				Cms.BusinessLayer.BlCommon.ClsCommon.BindLookupDDL(ref cmbCHECK_TYPE,"CPAYTP");
				cmbCHECK_TYPE.Items.Insert(0,new System.Web.UI.WebControls.ListItem("All","0"));
				cmbCHECK_TYPE.Items.Insert(5,new System.Web.UI.WebControls.ListItem("Premium Refund Checks","1"));
				System.Web.UI.WebControls.ListItem Li = new System.Web.UI.WebControls.ListItem();
				Li = cmbCHECK_TYPE.Items.FindByText("Claims Checks");
				cmbCHECK_TYPE.Items.Remove(Li);
				Li = cmbCHECK_TYPE.Items.FindByText("Checks for Cancellation and Change Premium Payment");
				cmbCHECK_TYPE.Items.Remove(Li);
				Li = cmbCHECK_TYPE.Items.FindByText("Premium Refund Checks for Over Payment");
				cmbCHECK_TYPE.Items.Remove(Li);
				Li = cmbCHECK_TYPE.Items.FindByText("Premium Refund Checks for Suspense Amount");
				cmbCHECK_TYPE.Items.Remove(Li);
				btnPrint.Attributes.Add("onclick","javascript: OpenDecPage();");
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

			#region loading web grid control
			
			Control c1 = LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			BaseDataGrid objWebGrid;
			objWebGrid = (BaseDataGrid)c1;
			try
			{
				//string CheckTypeValue=",'All' as CHECK_TYPE_ID";
				string GroupByClause ="";
				if(cmbCHECK_TYPE.SelectedValue=="")
					objWebGrid.WhereClause = "1<0";	
				else if(cmbCHECK_TYPE.SelectedValue=="0")//All is selected
				{ //checks which are committed but not voided
					objWebGrid.WhereClause = " isnull(IS_COMMITED,'N')='Y' and isNull(GL_UPDATE,'0')<=1";
				}
				//Changes for itrack #5615
				else if(cmbCHECK_TYPE.SelectedValue=="1")//OverPayment, Suspense Payment, Cancellation Refund Checks
				{ //checks which are committed but not voided
					objWebGrid.WhereClause = " LOOKUP_CHECK_ID IN (2474,9935,9936) AND isnull(IS_COMMITED,'N')='Y' and isNull(GL_UPDATE,'0')<=1";				
				}
				else
				{
					//CheckTypeValue = ",chk.CHECK_TYPE as CHECK_TYPE_ID";
					//checks which are committed but not voided

					objWebGrid.WhereClause = "LOOKUP_CHECK_ID= '" + cmbCHECK_TYPE.SelectedValue + "' and isnull(IS_COMMITED,'N')='Y' and isNull(GL_UPDATE,'0')<=1";
				}
				if(Request.QueryString["Mode"]!=null && Request.QueryString["Mode"].ToString().ToUpper().Equals("REGISTER") )
				{
					TabCtl.TabURLs="AddCheck.aspx?CHECK_TYPE_ID="+cmbCHECK_TYPE.SelectedValue+"&"+"CalledFrom=Register"+"&";
				}
				
				GroupByClause = " group by chk.CREATED_DATETIME,chk.CHECK_DATE,mnt_lookup_values.LOOKUP_VALUE_DESC,chk.CHECK_NUMBER,ACC.ACC_DESCRIPTION"
								+",ACC.ACC_DISP_NUMBER,ACC.ACC_PARENT_ID,T2.ACC_DESCRIPTION,ACC.ACC_DESCRIPTION,ACC.ACC_DISP_NUMBER,chk.PAYEE_ENTITY_NAME,"
								+"chk.CHECK_AMOUNT,chk.CHECK_ID,chk.CREATED_DATETIME,chk.PAYEE_ENTITY_ID,chk.CHECK_TYPE,chk.IS_COMMITED,chk.GL_UPDATE,"
								+"mnt_lookup_values.LOOKUP_UNIQUE_ID";
				//Setting web grid control properties
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";

				strSelect = " (Select convert(varchar,chk.CREATED_DATETIME,101) as CREATED_DATETIME,convert(varchar,chk.CHECK_DATE,101) "; 
				strSelect += " as CHECK_DATE,mnt_lookup_values.lookup_value_desc as CHECK_TYPE,chk.CHECK_NUMBER,";
				strSelect += " case when ACC.acc_parent_id is null then ACC.ACC_DESCRIPTION + ' : ' +  isnull(ACC.ACC_DISP_NUMBER,'')";  
				strSelect += " 	else  isnull(t2.acc_description,'') + ' - ' + ACC.ACC_DESCRIPTION  + ' : ' + isnull(ACC.ACC_DISP_NUMBER,'')";
				strSelect += " 	end as ACC_DESCRIPTION,chk.PAYEE_ENTITY_NAME,convert(varchar(30),convert(money,SUM(IsNull(chk.CHECK_AMOUNT,0))),1)check_amount, convert(varchar(30),convert(money,SUM(IsNull(chk.CHECK_AMOUNT,0))),1)";
				strSelect += " as AppliedAmount, chk.CHECK_ID,convert(varchar,chk.CREATED_DATETIME,101) as CREATED_DATETIME1,'All' ";
				strSelect += " as CHECK_TYPE_ID,chk.PAYEE_ENTITY_ID,CHECK_TYPE AS CHECK_TYPE_ID1,IS_COMMITED,GL_UPDATE,mnt_lookup_values.lookup_unique_id as LOOKUP_CHECK_ID ";
				strSelect += " ,convert(money,SUM(IsNull(chk.CHECK_AMOUNT,0))) check_amount_1, convert(money,SUM(IsNull(chk.CHECK_AMOUNT,0))) AppliedAmount_1 ";
				strSelect += " FROM ACT_CHECK_INFORMATION chk with(nolock) LEFT JOIN ACT_GL_ACCOUNTS ACC with(nolock) ON ACC.ACCOUNT_ID = CHK.ACCOUNT_ID ";
				strSelect += " LEFT OUTER JOIN  ACT_GL_ACCOUNTS T2 with(nolock) ON T2.ACCOUNT_ID = ACC.ACC_PARENT_ID";
				//CHECK_TYPE ADDED FOR iTRACK #ISSUE 5418
				//TO MOVE TO LOCAL VSS
				strSelect += " INNER JOIN mnt_lookup_values with(nolock) ON LOOKUP_UNIQUE_ID=chk.CHECK_TYPE WHERE IS_BNK_RECONCILED <> 'Y' AND ISNULL(IS_PRINTED,'N') <> 'Y' AND  isnull(chk.PAYMENT_MODE,0) NOT IN (11976,11788,11974) AND chk.CHECK_TYPE <> '9937' " + GroupByClause + ") Test ";
                 
				objWebGrid.SelectClause = " * ";
				objWebGrid.FromClause = strSelect;
				//objWebGrid.SelectClause = " convert(varchar,chk.CREATED_DATETIME,101) as CREATED_DATETIME,convert(varchar,chk.CHECK_DATE,101) as CHECK_DATE,mnt_lookup_values.lookup_value_desc as CHECK_TYPE,chk.CHECK_NUMBER,ACC.ACC_DISP_NUMBER + '-' +ACC.ACC_DESCRIPTION as ACC_DESCRIPTION,chk.PAYEE_ENTITY_NAME,chk.CHECK_AMOUNT,";
				//objWebGrid.SelectClause +=" chk.CHECK_AMOUNT as AppliedAmount,";
				//objWebGrid.SelectClause +=" chk.CHECK_ID,convert(varchar,chk.CREATED_DATETIME,101) as CREATED_DATETIME1"+CheckTypeValue+",chk.PAYEE_ENTITY_ID,CHECK_TYPE AS CHECK_TYPE_ID1";
				//objWebGrid.FromClause = " ACT_CHECK_INFORMATION chk LEFT JOIN ACT_GL_ACCOUNTS ACC with(nolock) ON ACC.ACCOUNT_ID = CHK.ACCOUNT_ID INNER JOIN mnt_lookup_values with(nolock) ON LOOKUP_UNIQUE_ID=chk.CHECK_TYPE";
				//objWebGrid.WhereClause = " isnull(IS_COMMITED,'N')<>'Y'";
					
				objWebGrid.SearchColumnHeadings = "Date Created^Transaction Date^Check Type^Check #^From Account^Payee^Check Amount^Amount To Be Paid";
				objWebGrid.SearchColumnNames = "CAST(CONVERT(VARCHAR,CREATED_DATETIME,101) AS DATETIME)^CAST(CONVERT(VARCHAR,CHECK_DATE,101) AS DATETIME)^CHECK_TYPE^CHECK_NUMBER^ACC_DESCRIPTION^PAYEE_ENTITY_NAME^CHECK_AMOUNT^AppliedAmount";
				objWebGrid.SearchColumnType = "D^D^T^T^T^T^T^T";			
				
                
				objWebGrid.DisplayColumnNumbers = "1^2^3^4^5^6^7";
				objWebGrid.DisplayColumnNames = "CREATED_DATETIME1^CHECK_DATE^CHECK_TYPE^CHECK_NUMBER^ACC_DESCRIPTION^PAYEE_ENTITY_NAME^CHECK_AMOUNT^AppliedAmount";
				objWebGrid.DisplayColumnHeadings = "Date Created^Transaction date^Check Type^Check #^From Account^Payee^Check Amount^Amount To Be Paid";
				objWebGrid.DisplayTextLength = "7^7^20^10^23^13^10^10";
				objWebGrid.DisplayColumnPercent = "7^7^20^10^23^13^10^10";
				objWebGrid.PrimaryColumns = "6";
				objWebGrid.PrimaryColumnsName = "CHECK_ID";
				//Modified by Asfa(18-June-2008) - iTrack #3906(Note: 1)
				//objWebGrid.ColumnTypes = "B^B^B^B^B^B^B^B";
				objWebGrid.ColumnTypes = "B^B^B^B^B^B^N^N";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^6";
				objWebGrid.SearchMessage = "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				if(Request.QueryString["Mode"]!=null && Request.QueryString["Mode"].ToString().ToUpper().Equals("REGISTER") )
				{
					objWebGrid.HeaderString = "Check Register";
					objWebGrid.OrderByClause = "CHECK_DATE DESC";	
				}
				else
				{
					objWebGrid.ExtraButtons = "1^Print to PDF^0^OpenDecPage";
					objWebGrid.HeaderString = "Printing Checks";
					//Order by PAYEE_ENTITY_NAME For Itrack Issue #6047. 
					objWebGrid.OrderByClause = "PAYEE_ENTITY_NAME,CREATED_DATETIME1 ASC"; 
					//objWebGrid.OrderByClause = "CREATED_DATETIME1 ASC";	
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

		#region "Pdf write"
		private void WritePdf(PdfFields objPdfFields)
		{
			//User details to be imporsonate, comes from web.config
			string strUerName, strPassWd, strDomain;

			//int intAttachID = 0;	//Holds the id of records
			ClsAttachment objAttachment = new Cms.BusinessLayer.BlCommon.ClsAttachment();
			
			//Getting the user,passs wd and domain from web.config
            strUerName = System.Configuration.ConfigurationManager.AppSettings.Get("IUserName");
            strPassWd = System.Configuration.ConfigurationManager.AppSettings.Get("IPassWd");
            strDomain = System.Configuration.ConfigurationManager.AppSettings.Get("IDomain");

			//if (objAttachment.ImpersonateUser(strUerName, strPassWd, strDomain))
		{
				
			APToolkitNET.Toolkit objPdfToolkit = new Toolkit();
			APToolkitNET.Toolkit objPdfToolkit2 = new Toolkit();
			objPdfToolkit.OpenInputFile(Server.MapPath("/cms/Account/Support")+"\\Cheque_payable.pdf");
			string filename = objPdfToolkit.GetUniqueFileName();
			objPdfToolkit.OpenOutputFile(Server.MapPath("/cms")+"\\"+filename);
			objPdfToolkit2.OpenOutputFile(Server.MapPath("/cms")+"\\sigFile1.pdf");

			//objPdfToolkit.SetFormFieldData("ChkNumber",objPdfFields.checkNumber,1);
			//objPdfToolkit.SetFormFieldData("ChkNumber1",objPdfFields.checkNumber,1);
			for(int i=0;i<2;i++)
			{
				objPdfToolkit.SetFormFieldData("Payee_Entity_name",objPdfFields.payeeEntityName,1);
				objPdfToolkit.SetFormFieldData("Disp_payee_add1",objPdfFields.payeeAddress,1);
				objPdfToolkit.SetFormFieldData("DispcheckMemo",objPdfFields.checkMemo,1);
				objPdfToolkit.SetFormFieldData("Check_Date",objPdfFields.checkDate,1);
				objPdfToolkit.SetFormFieldData("Dispamount",objPdfFields.amount,1);
				objPdfToolkit.SetFormFieldData("DispTAmt",objPdfFields.textAmount,1);
				objPdfToolkit.SetFormFieldData("Payee_entity_name",objPdfFields.payeeEntityName,1);
				if(objPdfFields.signatureFilePath1 != null && objPdfFields.signatureFilePath1.Length>0)
				{
					//int ret=0;
					string imgPath = Server.MapPath(ConfigurationSettings.AppSettings["UploadURL"])+"\\"+objPdfFields.signatureFilePath1;
					//ret=objPdfToolkit2.PrintImage(imgPath,430,615,45,55,true);
					//if(objPdfFields.signatureFilePath2 != null && objPdfFields.signatureFilePath2.Length>0)
					//		objPdfToolkit2.PrintImage(objPdfFields.signatureFilePath2,430,588,45,55,true);

				}
				// r = objPdfToolkit2.CopyForm(0, 0);
				objPdfToolkit2.CloseOutputFile();
				objPdfToolkit.ClearLogosAndImages();
				if(objPdfFields.signatureFilePath1 != null && objPdfFields.signatureFilePath1.Length>0)
					objPdfToolkit.AddLogo(objPdfFields.signatureFilePath1,1);
				
				object r = objPdfToolkit.CopyForm(0, 0);
			}
			//objPdfToolkit.FlattenRemainingFormFields  = 1;
			objPdfToolkit.CloseOutputFile();
			objPdfToolkit.CloseInputFile();
			objAttachment.endImpersonation();
		}
			//else
		{
			//				//Imporsation failed
			//				//lblMessage.Text += "\n Unable to upload the file. User imporsonation failed.";
		}
//			PrintPDF(Server.MapPath("/cms")+"\\"+filename);
		}
		private void PrintPDF(string fileName)
		{
			
		}
		#endregion


	}
}

