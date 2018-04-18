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
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer.BlCommon.Reinsurance;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Maintenance.Reinsurance;
using Cms.ExceptionPublisher.ExceptionManagement;


namespace Cms.CmsWeb.Maintenance.Reinsurance
{
	/// <summary>
	/// Summary description for ReinsurancePremiumReports.
	/// </summary>
	public class ReinsurancePremiumReports :Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.DropDownList cmbCONTRACT_NUMBER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCONTRACT_NUMBER;
		protected System.Web.UI.WebControls.DropDownList cmbCONTRACT_DATES;
		protected System.Web.UI.WebControls.Label capCONTRACT_NUMBER;
		protected System.Web.UI.WebControls.Label capCONTRACT_DATES;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCONTRACT_DATES;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.WebControls.Label capTYPE_REPORT;
		protected System.Web.UI.WebControls.DropDownList cmbTYPE_REPORT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTYPE_REPORT;
		protected System.Web.UI.WebControls.Label capREPORT;
		protected System.Web.UI.WebControls.DropDownList cmbREPORT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREPORT;
		protected System.Web.UI.WebControls.Label capEND_MONTH;
		protected System.Web.UI.WebControls.DropDownList cmbEND_MONTH;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEND_MONTH;
		protected System.Web.UI.WebControls.Label capYEAR;
		protected System.Web.UI.WebControls.DropDownList cmbYEAR;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvYEAR;
		protected System.Web.UI.WebControls.Label capMAJOR_PART;
		protected System.Web.UI.WebControls.DropDownList cmbMAJOR_PART;
//		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAJOR_PART;
		protected System.Web.UI.WebControls.Label capMAJOR_DESC;
		protected System.Web.UI.WebControls.TextBox txtMAJOR_DESC;
		protected System.Web.UI.WebControls.Label capTRANSACTION_TYPE;
		protected System.Web.UI.WebControls.ListBox cmbFROMTRANSACTION_TYPE;
		protected System.Web.UI.WebControls.CheckBox chkSelectAll;
		protected System.Web.UI.WebControls.Button btnSELECT_TRANSACTION_TYPE;
		protected System.Web.UI.WebControls.Button btnDESELECT_TRANSACTION_TYPE;
		protected System.Web.UI.WebControls.Label capRECIPIENT;
		protected System.Web.UI.WebControls.ListBox cmbTRANSACTION_TYPE;
		protected System.Web.UI.WebControls.CustomValidator csvTRANSACTION_TYPE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTransactionType;
		protected System.Web.UI.HtmlControls.HtmlTable tblBody;	
	
		protected System.Web.UI.WebControls.Label capSPECIAL_ACCEP;
		protected System.Web.UI.WebControls.DropDownList cmbSPECIAL_ACCEP;
		protected System.Web.UI.WebControls.Label capTOTAL_INSURANCE;
		protected System.Web.UI.WebControls.DropDownList cmbTOTAL_INSURANCE;
		protected System.Web.UI.WebControls.Label capVALUE_FROM;
		protected System.Web.UI.WebControls.TextBox txtVALUE_FROM;
		protected System.Web.UI.WebControls.Label capVALUE_TO;
		protected System.Web.UI.WebControls.TextBox txtVALUE_TO;

		protected System.Web.UI.WebControls.Label capSORT_FIRST;
		protected System.Web.UI.WebControls.DropDownList cmbSORT_FIRST;
		protected System.Web.UI.WebControls.Label capSORT_SEC;
		protected System.Web.UI.WebControls.DropDownList cmbSORT_SEC;
		protected System.Web.UI.WebControls.Label capSORT_THIRD;
		protected System.Web.UI.WebControls.DropDownList cmbSORT_THIRD;
		/*protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;*/
		System.Resources.ResourceManager objResourceMgr;

		protected Cms.CmsWeb.Controls.CmsButton btnDisplay;
		protected Cms.CmsWeb.Controls.CmsButton btnExport;

//		protected System.Web.UI.HtmlControls.HtmlGenericControl tbDataGrid;
		protected System.Web.UI.WebControls.Label lblDatagrid;

		protected System.Web.UI.WebControls.RegularExpressionValidator revVALUE_FROM;
		protected System.Web.UI.WebControls.RegularExpressionValidator revVALUE_TO;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVALUE_FROM;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVALUE_TO;
		public string strValue ="";
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			base.ScreenId="265";
			#region Setting the properties of CmsButton 
			//START:** Setting permissions and class (Read/write/execute/delete) of Cmsbutton**********
			btnDisplay.CmsButtonClass		= CmsButtonType.Execute;
			btnDisplay.PermissionString		= gstrSecurityXML;
			btnExport.CmsButtonClass		= CmsButtonType.Execute;
			btnExport.PermissionString		= gstrSecurityXML;			
			btnExport.Attributes.Add("onclick","javascript:return MandetoryMessage();");
			btnDisplay.Attributes.Add("onclick","javascript:return MandetoryMessage();");



			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			#endregion

			# region Setting Attributes
			btnSELECT_TRANSACTION_TYPE.Attributes.Add("onclick","javascript:selectTransactionType();return false;");
			btnDESELECT_TRANSACTION_TYPE.Attributes.Add("onclick","javascript:deselectTransactionType();return false;");
			//btnDisplay.Attributes.Add("onClick","javascript:DisplayReport();return false;");
			#endregion

			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.Reinsurance.ReinsurancePremiumReports" ,System.Reflection.Assembly.GetExecutingAssembly());

			if(!IsPostBack)
			{
				fillDropdowns();
				SetCaptions();
				//BindGrid("","");
//				tbDataGrid.Visible=false;
				SetErrorMessages();
				//getstring();
			}
			
		}
		# region SetErrorMessages
		private void SetErrorMessages()
		{
			
			revVALUE_FROM.ValidationExpression     = aRegExpDoublePositiveNonZeroStartWithZero;
			revVALUE_FROM.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
			revVALUE_TO.ValidationExpression     = aRegExpDoublePositiveNonZeroStartWithZero;
			revVALUE_TO.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");

		}
		#endregion

		# region fillDropdowns
		private void fillDropdowns()
		{
			cmbFROMTRANSACTION_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsReinsuranceContact.FillTransactiontype().Tables[0];
			cmbFROMTRANSACTION_TYPE.DataTextField	= "PROCESS_DESC";
			cmbFROMTRANSACTION_TYPE.DataValueField	= "PROCESS_ID";
			cmbFROMTRANSACTION_TYPE.DataBind(); 

			cmbCONTRACT_NUMBER.DataSource = Cms.BusinessLayer.BlCommon.ClsReinsuranceContact.FillContractNumber().Tables[0];
			cmbCONTRACT_NUMBER.DataTextField	= "CONTRACT_NUMBER";
			cmbCONTRACT_NUMBER.DataValueField	= "CONTRACT_ID";
			cmbCONTRACT_NUMBER.DataBind(); 
			cmbCONTRACT_NUMBER.Items.Insert(0,"");

			cmbREPORT.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("RRS");
			cmbREPORT.DataTextField	= "LookupDesc";
			cmbREPORT.DataValueField	= "LookupID";
			cmbREPORT.DataBind(); 

			cmbTOTAL_INSURANCE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbTOTAL_INSURANCE.DataTextField="LookupDesc"; 
			cmbTOTAL_INSURANCE.DataValueField="LookupCode";
			cmbTOTAL_INSURANCE.DataBind();
			cmbTOTAL_INSURANCE.Items[0].Selected=true;

			cmbSPECIAL_ACCEP.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbSPECIAL_ACCEP.DataTextField="LookupDesc"; 
			cmbSPECIAL_ACCEP.DataValueField="LookupCode";
			cmbSPECIAL_ACCEP.DataBind();
			cmbSPECIAL_ACCEP.Items[0].Selected=true;

			FillMonths();
			FillSortCombos();
		}
		private void cmbCONTRACT_NUMBER_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			int contractID;
			Cms.BusinessLayer.BlCommon.ClsReinsuranceContact objReinsuranceContact=new Cms.BusinessLayer.BlCommon.ClsReinsuranceContact(); 
			DataTable dtCONTRACT=new DataTable(); 
			DataTable dtTYPE=new DataTable(); 
			DataTable dtYEAR=new DataTable();
			DataTable dtMAJOR_PART=new DataTable();
			//contractID=cmbCONTRACT_NUMBER.SelectedItem==null?-1:int.Parse(cmbCONTRACT_NUMBER.SelectedItem.Value); 
			contractID=cmbCONTRACT_NUMBER.SelectedItem.Value==""?-1:int.Parse(cmbCONTRACT_NUMBER.SelectedItem.Value); 
			if(contractID!=-1)
			{
					
				dtCONTRACT=objReinsuranceContact.FillContractDates(contractID).Tables[0];
				cmbCONTRACT_DATES.DataSource=dtCONTRACT;
				cmbCONTRACT_DATES.DataTextField	= "DATE";
				cmbCONTRACT_DATES.DataValueField	= "CONTRACT_ID";
				cmbCONTRACT_DATES.DataBind(); 

				dtTYPE = objReinsuranceContact.FillContractDates(contractID).Tables[1];
				cmbTYPE_REPORT.DataSource=dtTYPE;
				cmbTYPE_REPORT.DataTextField	= "RISK_EXPOSURE";
				cmbTYPE_REPORT.DataValueField	= "CONTRACT_ID";
				cmbTYPE_REPORT.DataBind(); 

				dtYEAR=objReinsuranceContact.FillContractDates(contractID).Tables[2];
				cmbYEAR.DataSource=dtYEAR;
				cmbYEAR.DataTextField	= "YEAR";
				cmbYEAR.DataValueField	= "CONTRACT_ID";
				cmbYEAR.DataBind(); 
				
				dtMAJOR_PART=objReinsuranceContact.FillContractDates(contractID).Tables[3];
				cmbMAJOR_PART.DataSource=dtMAJOR_PART;
				cmbMAJOR_PART.DataTextField	= "REIN_COMAPANY_NAME";
				cmbMAJOR_PART.DataValueField	= "PARTICIPATION_ID";
				cmbMAJOR_PART.DataBind(); 
				
			}
			
		}
		#endregion

		# region FillMonths
		private void FillMonths()
		{
			string[] months = {"January","Febuary","March","April","May","June","July","August","September","October","November","December"};
			for(int i=0;i<12;i++)
			{
				cmbEND_MONTH.Items.Add(new ListItem(months[i],(i).ToString()));
			}
		}
		#endregion

		# region FillSortCombos
		private void FillSortCombos()
		{
			string[] sorts = {"Policy number","Named Insured","Effective Date","Expiration Date","Transaction Type","Transaction Date"};
			for(int i=0;i<6;i++)
			{
				cmbSORT_FIRST.Items.Add(new ListItem(sorts[i],(i).ToString()));
				cmbSORT_SEC.Items.Add(new ListItem(sorts[i],(i).ToString()));
				cmbSORT_THIRD.Items.Add(new ListItem(sorts[i],(i).ToString()));
			}
		}
		#endregion

		# region SetCaptions
		private void SetCaptions()
		{
			capCONTRACT_NUMBER.Text					=		objResourceMgr.GetString("cmbCONTRACT_NUMBER");
			capCONTRACT_DATES.Text					=		objResourceMgr.GetString("cmbCONTRACT_DATES");
			capTYPE_REPORT.Text						=		objResourceMgr.GetString("cmbTYPE_REPORT");
			capREPORT.Text							=		objResourceMgr.GetString("cmbREPORT");
			capEND_MONTH.Text						=		objResourceMgr.GetString("cmbEND_MONTH");
			capYEAR.Text							=		objResourceMgr.GetString("cmbYEAR");
			capMAJOR_PART.Text						=		objResourceMgr.GetString("cmbMAJOR_PART");
			capMAJOR_DESC.Text						=		objResourceMgr.GetString("txtMAJOR_DESC");
			capSPECIAL_ACCEP.Text					=		objResourceMgr.GetString("cmbSPECIAL_ACCEP");
			capTOTAL_INSURANCE.Text					=		objResourceMgr.GetString("cmbTOTAL_INSURANCE");
			capVALUE_FROM.Text						=		objResourceMgr.GetString("txtVALUE_FROM");
			capVALUE_TO.Text						=		objResourceMgr.GetString("txtVALUE_TO");
			capSORT_FIRST.Text						=		objResourceMgr.GetString("cmbSORT_FIRST");
			capSORT_SEC.Text						=		objResourceMgr.GetString("cmbSORT_SEC");
			capSORT_THIRD.Text						=		objResourceMgr.GetString("cmbSORT_THIRD");
		}
		#endregion

		# region getstring
		private void getstring()
		{
			string contract_number = "";
			string contract_dates ="";
			string type_report = "";
			string report ="";
			string month_ending = "";
			string year="";
			string major_part ="";
			string major_desc="";	
			string tran_type="";
			string sp_accep ="";
			string tot_value_from="";
			string tot_value_to="";
			string user_id = "";
			string insu_value ="";

			contract_number = cmbCONTRACT_NUMBER.SelectedValue;
			contract_dates = cmbCONTRACT_DATES.SelectedValue;
			type_report = cmbTYPE_REPORT.SelectedValue;
			report = cmbREPORT.SelectedValue;
			month_ending = cmbEND_MONTH.SelectedValue;
			year = cmbYEAR.SelectedValue;
			major_part = cmbMAJOR_PART.SelectedValue;
			major_desc = txtMAJOR_DESC.Text;
			
			sp_accep= cmbSPECIAL_ACCEP.SelectedValue;
			tot_value_from = txtVALUE_FROM.Text;
			tot_value_to =txtVALUE_TO.Text;
			user_id = GetUserId().ToString();
			
			if(txtVALUE_FROM.Text!="" && txtVALUE_TO.Text!="")
			{
				insu_value = txtVALUE_FROM.Text + " to " + txtVALUE_TO.Text;
			}
			if(chkSelectAll.Checked == true)
			{
				tran_type = "A";
			}
			else
				tran_type = hidTransactionType.Value;
				
			strValue = contract_number+  "^"  +contract_dates+ "^" +type_report+ "^" +report+ "^" +month_ending+ "^" +year+ "^" +major_part+ "^" +major_desc+ "^" +sp_accep+ "^" +tot_value_from+ "^" +tot_value_to+ "^" +tran_type+ "^" +user_id+ "^" +insu_value;

			
		}
		#endregion

		private void btnDisplay_Click(object sender, System.EventArgs e)
		{

			getstring();
			RegisterScript();

		}

		private void RegisterScript()
		{
			string strScript = @"<script>" + 
				"DisplayReport();" + 
				//"return false;" + 
				"</script>" 
				;
	

			//if (!Page.IsStartupScriptRegistered("Refresh"))
			{
				ClientScript.RegisterStartupScript(this.GetType(),"Refresh",strScript);

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
			this.cmbCONTRACT_NUMBER.SelectedIndexChanged += new System.EventHandler(this.cmbCONTRACT_NUMBER_SelectedIndexChanged);
			this.btnDisplay.Click += new System.EventHandler(this.btnDisplay_Click);
		}
		#endregion
	}
}
