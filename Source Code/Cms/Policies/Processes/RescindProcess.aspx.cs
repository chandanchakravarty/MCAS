/******************************************************************************************
<Author				: -  kranti
<Start Date			: -	 17 jan 2007
<End Date			: -	 
<Description		: -  Class for Policy Rescind Process.
<Review Date		: - 
<Reviewed By		: - 	

Modification History
<Modified By		: - 
<Modified Date		: - 
<Purpose			: -  
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
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb;
using Cms.ExceptionPublisher;
using Cms.CmsWeb.Controls; 
using System.Xml;
using Cms.Model.Policy; 
using Cms.BusinessLayer.BlProcess;
using Cms.Model.Policy.Process;

namespace Cms.Policies.Processes
{
	/// <summary>
	/// Summary description for RescindProcess.
	/// </summary>
	public class RescindProcess : Cms.Policies.policiesbase
	{
		#region "Controls"
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capCOMPLETED_DATETIME;
		protected System.Web.UI.WebControls.Label capREQUESTED_BY;
		protected System.Web.UI.WebControls.DropDownList cmbREQUESTED_BY;
		protected System.Web.UI.WebControls.Label capCANCELLATION_OPTION;
		protected System.Web.UI.WebControls.DropDownList cmbCANCELLATION_OPTION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCANCELLATION_OPTION;
		protected System.Web.UI.WebControls.Label capCANCELLATION_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbCANCELLATION_TYPE;
		protected System.Web.UI.WebControls.Label capREASON;
		protected System.Web.UI.WebControls.DropDownList cmbREASON;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREASON;
		protected System.Web.UI.WebControls.Label capOTHER_REASON;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvOTHER_REASON;
		protected System.Web.UI.WebControls.Label capRETURN_PREMIUM;
		protected System.Web.UI.WebControls.TextBox txtRETURN_PREMIUM;
		//protected System.Web.UI.WebControls.TextBox txtPAST_DUE_PREMIUM;
		protected Cms.CmsWeb.Controls.CmsButton btnBack_To_Customer_Assistant;
		protected Cms.CmsWeb.Controls.CmsButton btnBack_To_Search;
		protected Cms.CmsWeb.Controls.CmsButton btnCommit_To_Spool;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.Controls.CmsButton btnRollBack;
		protected Cms.CmsWeb.Controls.CmsButton btnComplete;
		protected Cms.CmsWeb.Controls.CmsButton btnCalculate_Return_premium;
		protected Cms.CmsWeb.Controls.CmsButton btnPolicyDetails;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidUNDERWRITER;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidROW_ID;		
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEFFECTIVE_TIME;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPROCESS_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Hidden1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDisplayBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidADD_INT_ID;
		//previous status
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPRE_STATUS;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label capEFFECTIVE_DATETIME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEFFECTIVE_DATETIME;
		protected System.Web.UI.WebControls.TextBox txtEFFECTIVE_DATETIME;
		protected System.Web.UI.WebControls.Label capEFFECTIVE_TIME;
		protected System.Web.UI.WebControls.Label capAGENTPHONENO;
		protected System.Web.UI.WebControls.HyperLink hlkEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.TextBox txtAGENT_PHONE_NUMBER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAGENT_PHONE_NUMBER;
		protected Cms.CmsWeb.WebControls.PolicyTop cltPolicyTop;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEFFECTIVE_MINUTE;
		protected System.Web.UI.WebControls.TextBox txtEFFECTIVE_HOUR;
		protected System.Web.UI.WebControls.Label lblEFFECTIVE_HOUR;
		protected System.Web.UI.WebControls.TextBox txtEFFECTIVE_MINUTE;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtEFFECTIVE_SEC;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.DropDownList cmbMERIDIEM;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEFFECTIVE_HOUR;		
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEFFECTIVE_SEC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMERIDIEM;
		protected System.Web.UI.WebControls.RangeValidator rnvEFFECTIVE_HOUR;
		protected System.Web.UI.WebControls.RangeValidator rnvtEFFECTIVE_MINUTE;
		protected System.Web.UI.WebControls.RangeValidator rnvEFFECTIVE_SEC;
		System.Resources.ResourceManager objResourceMgr;
		protected System.Web.UI.WebControls.Label lblOTHER_REASON;
		protected System.Web.UI.WebControls.TextBox txtOTHER_REASON;
		protected System.Web.UI.WebControls.CustomValidator csvOTHER_REASON;		
		protected System.Web.UI.WebControls.Label capPRINTING_OPTIONS;
		protected System.Web.UI.WebControls.CheckBox chkPRINTING_OPTIONS;
		protected System.Web.UI.WebControls.Label capINSURED;
		protected System.Web.UI.WebControls.DropDownList cmbINSURED;
		protected System.Web.UI.WebControls.DropDownList cmbOtherRescissionDate;
		protected System.Web.UI.WebControls.Label capAGENCY_PRINT;
		protected System.Web.UI.WebControls.DropDownList cmbAGENCY_PRINT;
		protected System.Web.UI.WebControls.Label capADD_INT;
		protected System.Web.UI.WebControls.DropDownList cmbADD_INT;
		protected System.Web.UI.WebControls.Label capSEND_ALL;
		protected System.Web.UI.WebControls.CheckBox chkSEND_ALL;
		protected System.Web.UI.WebControls.Label capUnassignLossCodes;
		protected System.Web.UI.WebControls.Label capAssignedLossCodes;
		protected System.Web.UI.WebControls.ListBox cmbUnAssignAddInt;
		protected System.Web.UI.WebControls.ListBox cmbAssignAddInt;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnAssignLossCodes;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnUnAssignLossCodes;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCANCELLATION_TYPE;
		protected System.Web.UI.WebControls.Label lblAGENT_PHONE_NUMBER;
		protected System.Web.UI.WebControls.Label capDateTime;
		protected System.Web.UI.WebControls.TextBox Textbox1;
		protected System.Web.UI.WebControls.HyperLink Hyperlink1;
		protected System.Web.UI.WebControls.RegularExpressionValidator revRETURN_PREMIUM;
		protected System.Web.UI.WebControls.TextBox txtDateTime;
		protected System.Web.UI.WebControls.HyperLink hlkDateTime;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDateTime;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDateTime;
//		protected System.Web.UI.WebControls.TextBox txtEFFECTIVE_DATE;
//		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEFFECTIVE_DATETIME;
		protected System.Web.UI.WebControls.RegularExpressionValidator Regularexpressionvalidator1;
		protected System.Web.UI.WebControls.RequiredFieldValidator Requiredfieldvalidator1;
		protected System.Web.UI.WebControls.Label capOtherRescissionDate;
		protected System.Web.UI.WebControls.DropDownList cmbRESCIND_OPTION;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidNEW_POLICY_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_CURRENT_STATUS;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_PREVIOUS_STATUS;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCOMMIT_FLAG;
//		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDateTime;
//		protected System.Web.UI.WebControls.RegularExpressionValidator revDateTime;	
		protected Cms.CmsWeb.Controls.CmsButton btnCommitInProgress;

		
		ClsRescindProcess objClsRescindProcess  = new ClsRescindProcess();
		#endregion 
		
		# region "Page_Load"
		private void Page_Load(object sender, System.EventArgs e)
		{
			Ajax.Utility.RegisterTypeForAjax(typeof(RescindProcess));						
			//Setting the screen id
			((cmsbase) this).ScreenId = "5000_33";
			hlkEFFECTIVE_DATE.Attributes.Add("OnClick","fPopCalendar(document.PROCESS_RESCIND.txtEFFECTIVE_DATETIME,document.PROCESS_RESCIND.txtEFFECTIVE_DATETIME)");                     
			hlkDateTime.Attributes.Add("OnClick","fPopCalendar(document.PROCESS_RESCIND.txtDateTime,document.PROCESS_RESCIND.txtDateTime)");                     

			//this.cmbREASON.Attributes.Add("onchange","javascript:Check();");			
			//cmbPRINT_COMMENTS.Attributes.Add("onchange","javascript:CommentEnable();");
			cmbREQUESTED_BY.Attributes.Add("onchange","javascript:DisplayAgentPhoneNo();");
			btnPolicyDetails.Attributes.Add("onclick","javascript:return ShowDetailsPolicy();");
			btnReset.Attributes.Add("onclick","javascript:return formReset();");
			btnRollBack.Attributes.Add("onClick","javascript:HideShowCommit();");
			//for showing other rescission date text box
			cmbOtherRescissionDate.Attributes.Add("onchange","javascript:DisplayOtherRescissionDate();");
			cmbMERIDIEM.Attributes.Add("onchange","javascript:disableHourValidator();");//Added by Charles on 1-Sep-09 for Itrack 6323
			
			//Setting the security xml of cmb button
			SetButtonsSecurityXML();
			cltPolicyTop.UseRequestVariables = false;
			if(!Page.IsPostBack)
			{
				//Fetching the query string values
				GetQueryString();
				//Setting the properties of validation controls
				SetValidators();				
				SetCaptions();
				
				//Setting the policy top controls setting
				SetPolicyTopControl();

				// fill dropdowns
				GetDropdownData();
				txtRETURN_PREMIUM.Attributes.Add("onBlur","javascript:this.value=formatCurrency(this.value);");
				txtEFFECTIVE_DATETIME.Attributes.Add("onBlur","javascript:return CallService();");
				cmbADD_INT.Attributes.Add("onChange","javascript:return cmbADD_INT_Change();");	
				chkSEND_ALL.Attributes.Add("onClick","javascript: chkSEND_ALL_Change();");			
				btnSave.Attributes.Add("onClick","javascript: GetAssignAddInt();Page_ClientValidate();return Page_IsValid;");
				btnComplete.Attributes.Add("onClick","javascript:GetAssignAddInt();HideShowCommitInProgress();Page_ClientValidate();return Page_IsValid;");				


				if (hidPROCESS_ID.Value == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_RESCIND.ToString())
				{
					objClsRescindProcess.BeginTransaction();
					if (objClsRescindProcess.CheckProcessEligibility(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value), int.Parse(hidPROCESS_ID.Value))==1)
					{
						//Starting the process
						SetDefaultValues();
						StartProcess();
					}
					else
					{
						//Populating the currently executing process information
						PopulateProcessInfo();
						DataSet dsTemp = ClsPolicyProcess.GetProcessInfoDataSet(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value),
							int.Parse(hidPOLICY_VERSION_ID.Value), int.Parse(hidROW_ID.Value));
						if(dsTemp != null && dsTemp.Tables[0].Rows.Count > 0)
							hidPRE_STATUS.Value = dsTemp.Tables[0].Rows[0]["POLICY_PREVIOUS_STATUS"].ToString();
					}
					objClsRescindProcess.CommitTransaction();
					this.SetFocus("txtEFFECTIVE_DATETIME");
				}
				else
				{
					//Populating the currently executing process information
					PopulateProcessInfo();
					DataSet dsTemp = ClsPolicyProcess.GetProcessInfoDataSet(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value),
						int.Parse(hidPOLICY_VERSION_ID.Value), int.Parse(hidROW_ID.Value));
					if(dsTemp != null && dsTemp.Tables[0].Rows.Count > 0)
						hidPRE_STATUS.Value = dsTemp.Tables[0].Rows[0]["POLICY_PREVIOUS_STATUS"].ToString();
				}

				//Populating other data
				PopulateOtherInfo();				
			}
			//this.SetFocus("txtEFFECTIVE_DATETIME");
		}
		#endregion		


		/// <summary>
		/// Show selected value in combo box
		/// </summary>
		/// <param name="combo"></param>
		/// <param name="combovalue"></param>		
		private void SelectComboValue(DropDownList combo,string combovalue)
		{
			int comboIndex ;
			for(comboIndex = 0;comboIndex<combo.Items.Count;comboIndex++)
			{
				if(combo.Items[comboIndex].Value == combovalue)
				{
					combo.SelectedIndex = comboIndex;
					return;
				}
			}
		}
		/// <summary>
		/// Enum for Rescind option
		/// </summary> 
		public enum enumRescind_Option
		{
			Pro_Rata	=	11994,
			Flat		=	11995,
			Equity		=	11996,
			Not_Applicabl = 13028
			
		}
		
		/// <summary>
		/// Enum for Rescind Type
		/// </summary> 
		public enum enumRescind_Type
		{
			Agents_Request			=	11987,
        	Cancel_Reinstatement	=	11971,
			Cancel_Rewrite			=	11988,
			Company_Request			=	11990,
        	Insured_Request			=	11989,
        	Non_Payment				=	11969,
        	Non_Renewal				=	11991,
        	NSF_No_replacement		=	11993,
			Rescinded				=	11970,
			NSF_Replace				=	11992
			
        }
		/// <summary>
		/// For Customized Letter option
		/// </summary>
		public enum enumCustomized_Letter_Required
		{
			No  = 10964,
			Yes = 10963
		}

		/// <summary>
		/// Set The default value of check box selected 
		/// </summary>
		private void SetDefaultValues()
		{
			chkSEND_ALL.Checked = true;
			hidADD_INT_ID.Value = "";
			for(int i=0;i<cmbUnAssignAddInt.Items.Count;i++)
			{
				hidADD_INT_ID.Value+=cmbUnAssignAddInt.Items[i].Value.ToString() + "~";
			}
		}

		/// <summary>
		/// Fill the dropdowns 
		/// </summary>
		/// <returns></returns>
		private void GetDropdownData()
		{
			cmbOtherRescissionDate.Items.Insert(0,"No");
			cmbOtherRescissionDate.Items[0].Value = "0";
			cmbOtherRescissionDate.Items.Insert(1,"Yes");
			cmbOtherRescissionDate.Items[1].Value = "1";
			/*
			cmbREQUESTED_BY.Items.Insert(0,"Company");
			cmbREQUESTED_BY.Items[0].Value = "0";
			cmbREQUESTED_BY.Items.Insert(1,"Agent");
			cmbREQUESTED_BY.Items[1].Value = "1";			
			cmbREQUESTED_BY.Items.Insert(2,"Customer");
			cmbREQUESTED_BY.Items[2].Value = "2";
			*/
			cmbREQUESTED_BY.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("REQCD");
			cmbREQUESTED_BY.DataTextField	= "LookupDesc";
			cmbREQUESTED_BY.DataValueField	= "LookupID";
			cmbREQUESTED_BY.DataBind();
			cmbREQUESTED_BY.Items.Insert(0,"");
			cmbREQUESTED_BY.Items[0].Value = "0";
			ListItem iListItemReq = null;
			iListItemReq = cmbREQUESTED_BY.Items.FindByValue("6497"); //remove Other option
			if(iListItemReq!=null)
				cmbREQUESTED_BY.Items.Remove(iListItemReq);
			iListItemReq = cmbREQUESTED_BY.Items.FindByValue("14152"); //remove Agency option
			if(iListItemReq!=null)
				cmbREQUESTED_BY.Items.Remove(iListItemReq);
			iListItemReq = cmbREQUESTED_BY.Items.FindByValue("14153"); //remove Wolverine option
			if(iListItemReq!=null)
				cmbREQUESTED_BY.Items.Remove(iListItemReq);
			 //find Company option default
			cmbREQUESTED_BY.SelectedIndex=cmbREQUESTED_BY.Items.IndexOf(cmbREQUESTED_BY.Items.FindByValue("6495"));  
/*
			cmbPRINT_COMMENTS.Items.Insert(0,"No");
			cmbPRINT_COMMENTS.Items[0].Value = "0";
			cmbPRINT_COMMENTS.Items.Insert(1,"Yes");
			cmbPRINT_COMMENTS.Items[1].Value = "1";
*/
			cmbMERIDIEM.Items.Insert(0,"AM");
			cmbMERIDIEM.Items[0].Value = "0";
			cmbMERIDIEM.Items.Insert(1,"PM");
			cmbMERIDIEM.Items[1].Value = "1";			
						
			cmbREASON.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CNCLRN");
			cmbREASON.DataTextField	= "LookupDesc";
			cmbREASON.DataValueField	= "LookupID";
			cmbREASON.DataBind();
			cmbREASON.Items.Insert(0,"");
			SelectComboValue(cmbREASON,"11545"); //DEFAULD 11545 = Rescission
			/*
			cmbSTD_LETTER_REQD.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbSTD_LETTER_REQD.DataTextField="LookupDesc";
			cmbSTD_LETTER_REQD.DataValueField="LookupID";
			cmbSTD_LETTER_REQD.DataBind();
			
			cmbCUSTOM_LETTER_REQD.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbCUSTOM_LETTER_REQD.DataTextField="LookupDesc";
			cmbCUSTOM_LETTER_REQD.DataValueField="LookupID";
			cmbCUSTOM_LETTER_REQD.DataBind();
			SelectComboValue(cmbCUSTOM_LETTER_REQD,((int) enumCustomized_Letter_Required.Yes).ToString());
			*/	
			//fill dropdown for print 			
			IList ListSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("PRNTOP");
			
			cmbINSURED.DataSource=ListSource;
			cmbINSURED.DataTextField="LookupDesc";
			cmbINSURED.DataValueField="LookupID";
			cmbINSURED.DataBind();
			
			ListItem iListItem = null;
			iListItem = cmbINSURED.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS.MICHIGAN_MAILERS).ToString());
			if(iListItem!=null)
				cmbINSURED.Items.Remove(iListItem);

			iListItem = null;
			iListItem = cmbINSURED.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS.DOWNLOAD).ToString());
			if(iListItem!=null)
				cmbINSURED.Items.Remove(iListItem);
			SelectComboValue(cmbINSURED,((int)clsprocess.enumPROC_PRINT_OPTIONS.NOT_REQUIRED).ToString());
			
			cmbADD_INT.DataSource=ListSource;
			cmbADD_INT.DataTextField="LookupDesc";
			cmbADD_INT.DataValueField="LookupID";
			cmbADD_INT.DataBind();

			iListItem = null;
			iListItem = cmbADD_INT.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS.DOWNLOAD).ToString());
			if(iListItem!=null)
				cmbADD_INT.Items.Remove(iListItem);
			iListItem = null;
			iListItem = cmbADD_INT.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS.MICHIGAN_MAILERS).ToString());
			if(iListItem!=null)
				cmbADD_INT.Items.Remove(iListItem);
			SelectComboValue(cmbADD_INT,((int)clsprocess.enumPROC_PRINT_OPTIONS.NOT_REQUIRED).ToString());

			cmbAGENCY_PRINT.DataSource=ListSource;
			cmbAGENCY_PRINT.DataTextField="LookupDesc";
			cmbAGENCY_PRINT.DataValueField="LookupID";
			cmbAGENCY_PRINT.DataBind();
			iListItem = null;
			iListItem = cmbAGENCY_PRINT.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS.MICHIGAN_MAILERS).ToString());
			if(iListItem!=null)
				cmbAGENCY_PRINT.Items.Remove(iListItem);
			SelectComboValue(cmbAGENCY_PRINT,((int)clsprocess.enumPROC_PRINT_OPTIONS.NOT_REQUIRED).ToString());

			DataTable dtAddIntList = null;
			Cms.BusinessLayer.BlApplication.ClsAdditionalInterest objAdditionalInterest = new Cms.BusinessLayer.BlApplication.ClsAdditionalInterest();
			dtAddIntList = objAdditionalInterest.GetAdditionalInterestList(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value),int.Parse(hidLOB_ID.Value));
			if(dtAddIntList!=null && dtAddIntList.Rows.Count>0)
			{
				cmbUnAssignAddInt.DataSource = dtAddIntList;
				cmbUnAssignAddInt.DataTextField = "ADD_INT_DETAILS";
				cmbUnAssignAddInt.DataValueField = "ADD_INT_ID";
				cmbUnAssignAddInt.DataBind();
			}		
			////////start rescind option
			cmbCANCELLATION_OPTION.DataSource		=	Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CANOPT",null,"S");
			cmbCANCELLATION_OPTION.DataTextField	=	"LookupDesc";
			cmbCANCELLATION_OPTION.DataValueField	=	"LookupID";
			cmbCANCELLATION_OPTION.DataBind();						
			
			iListItem = null;
			iListItem = cmbCANCELLATION_OPTION.Items.FindByValue(((int)enumRescind_Option.Equity ).ToString());
			if(iListItem!=null)
				cmbCANCELLATION_OPTION.Items.Remove(iListItem);
			
			iListItem = null;
			iListItem = cmbCANCELLATION_OPTION.Items.FindByValue(((int)enumRescind_Option.Not_Applicabl).ToString());
			if(iListItem!=null)
				cmbCANCELLATION_OPTION.Items.Remove(iListItem);

			SelectComboValue(cmbCANCELLATION_OPTION ,((int)enumRescind_Option.Flat).ToString());
			///////end rescind option
			
			cmbCANCELLATION_TYPE.DataSource		=	Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CANTYP");
			cmbCANCELLATION_TYPE.DataTextField	=	"LookupDesc";
			cmbCANCELLATION_TYPE.DataValueField	=	"LookupID";
			cmbCANCELLATION_TYPE.DataBind();			
			SelectComboValue(cmbCANCELLATION_TYPE , ((int)enumRescind_Type.Rescinded).ToString());
			cmbCANCELLATION_TYPE.Enabled = false ;

			txtEFFECTIVE_DATETIME.Text = ClsRescindProcess.GetPolicyInceptionDate(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value)).ToString("MM/dd/yyyy");

			txtRETURN_PREMIUM.Text=getReturnPremiumAmount(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value),Convert.ToDateTime(txtEFFECTIVE_DATETIME.Text),cmbCANCELLATION_TYPE.SelectedValue,cmbCANCELLATION_OPTION.SelectedValue).ToString();
		}			

		/// <summary>
		/// Retreives the values from form and populates the model object
		/// </summary>
		/// <returns>Model object of ClsProcessInfo type</returns>
		private Cms.Model.Policy.Process.ClsProcessInfo GetFormValues()
		{
			
			Cms.Model.Policy.Process.ClsProcessInfo objClsProcessInfo = new Cms.Model.Policy.Process.ClsProcessInfo();

			objClsProcessInfo.POLICY_ID = int.Parse(hidPOLICY_ID.Value);
			objClsProcessInfo.POLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value);
			objClsProcessInfo.CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);			
			objClsProcessInfo.PROCESS_ID = ClsPolicyProcess.POLICY_RESCIND;
			objClsProcessInfo.NEW_CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);			
			objClsProcessInfo.NEW_POLICY_ID = int.Parse(hidPOLICY_ID.Value);
			if (hidNEW_POLICY_VERSION_ID.Value !="" && hidNEW_POLICY_VERSION_ID.Value !="0" )
			  objClsProcessInfo.NEW_POLICY_VERSION_ID = int.Parse(hidNEW_POLICY_VERSION_ID.Value);
			else
			   objClsProcessInfo.NEW_POLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value);
			
			//objClsProcessInfo.POLICY_CURRENT_STATUS = GetPolicyStatus();
			objClsProcessInfo.POLICY_CURRENT_STATUS=hidPOLICY_CURRENT_STATUS.Value;
			//Take a hidden variable and put d value of policy previous status below..
			objClsProcessInfo.POLICY_PREVIOUS_STATUS = hidPOLICY_PREVIOUS_STATUS.Value;
			objClsProcessInfo.LOB_ID =int.Parse(hidLOB_ID.Value); 
						
			if (txtEFFECTIVE_DATETIME.Text != "")
			{
				DateTime EffDate = ConvertToDate(txtEFFECTIVE_DATETIME.Text);

				int Hr = int.Parse(txtEFFECTIVE_HOUR.Text);
				
				if(Hr==12 && cmbMERIDIEM.SelectedIndex==0) //If added by Charles on 31-Aug-09 for Itrack 6323
				{
					Hr=00;
				}
				if(cmbMERIDIEM.SelectedIndex == 1 && Hr!=12)//Added condition Hr!=12 by Charles on 31-Aug-09 for Itrack 6323
				{
					Hr+=12;
				}
				/*if(Hr==24) //Commented by Charles on 31-Aug-09 for Itrack 6323
				{
					Hr=00;
				}*/

				objClsProcessInfo.EFFECTIVE_DATETIME = new DateTime(EffDate.Year, EffDate.Month, EffDate.Day, Hr, int.Parse(txtEFFECTIVE_MINUTE.Text), 0);
				objClsProcessInfo.EFFECTIVE_TIME= objClsProcessInfo.EFFECTIVE_DATETIME.ToLongTimeString(); 
			}
			
			if(cmbCANCELLATION_OPTION.SelectedItem != null && cmbCANCELLATION_OPTION.SelectedValue.Trim() != "")
				objClsProcessInfo.CANCELLATION_OPTION  = int.Parse(cmbCANCELLATION_OPTION.SelectedValue);

			if(cmbCANCELLATION_TYPE.SelectedItem != null && cmbCANCELLATION_TYPE.SelectedValue != "")
				objClsProcessInfo.CANCELLATION_TYPE = int.Parse(cmbCANCELLATION_TYPE.SelectedValue);

			if(cmbREASON.SelectedItem != null && cmbREASON.SelectedValue != "")
				objClsProcessInfo.REASON = int.Parse(cmbREASON.SelectedValue);
			
			objClsProcessInfo.OTHER_REASON = txtOTHER_REASON.Text;

			if (cmbREQUESTED_BY.SelectedItem != null && cmbREQUESTED_BY.SelectedValue != "")
				objClsProcessInfo.REQUESTED_BY = int.Parse(cmbREQUESTED_BY.SelectedValue);

			if(txtRETURN_PREMIUM.Text.Trim()!="")
				objClsProcessInfo.RETURN_PREMIUM = Convert.ToDouble(txtRETURN_PREMIUM.Text.Trim());
			/*
			if (cmbPRINT_COMMENTS.SelectedItem != null)
				objClsProcessInfo.PRINT_COMMENTS = cmbPRINT_COMMENTS.SelectedValue;

			
			if (cmbPRINT_COMMENTS.SelectedValue != "0")
				objClsProcessInfo.COMMENTS = txtCOMMENTS.Text;
			else
				objClsProcessInfo.COMMENTS = "";
			*/	
			
			if(cmbAGENCY_PRINT.SelectedItem!=null && cmbAGENCY_PRINT.SelectedItem.Value!="")
				objClsProcessInfo.AGENCY_PRINT = int.Parse(cmbAGENCY_PRINT.SelectedItem.Value);
			
			if(chkPRINTING_OPTIONS.Checked)
				objClsProcessInfo.PRINTING_OPTIONS = (int)(enumYESNO_LOOKUP_CODE.YES);
			else
				objClsProcessInfo.PRINTING_OPTIONS = (int)(enumYESNO_LOOKUP_CODE.NO);

			if(cmbINSURED.SelectedItem!=null && cmbINSURED.SelectedItem.Value!="")
				objClsProcessInfo.INSURED = int.Parse(cmbINSURED.SelectedItem.Value);
						
			objClsProcessInfo.CREATED_BY = objClsProcessInfo.COMPLETED_BY = int.Parse(GetUserId()); 
			objClsProcessInfo.CREATED_DATETIME = objClsProcessInfo.COMPLETED_DATETIME = System.DateTime.Now;
			if(hidUNDERWRITER.Value!="" && hidUNDERWRITER.Value!="0")
				objClsProcessInfo.UNDERWRITER = int.Parse(hidUNDERWRITER.Value);
						
			if(cmbADD_INT.SelectedItem!=null && cmbADD_INT.SelectedItem.Value!="")
			{
				objClsProcessInfo.ADD_INT = int.Parse(cmbADD_INT.SelectedItem.Value);

				if(objClsProcessInfo.ADD_INT==int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL).ToString()))
				{
					objClsProcessInfo.ADD_INT_ID = hidADD_INT_ID.Value;
					if(chkSEND_ALL.Checked==true)
						objClsProcessInfo.SEND_ALL = (int)(enumYESNO_LOOKUP_CODE.YES);
					else
						objClsProcessInfo.SEND_ALL = (int)(enumYESNO_LOOKUP_CODE.NO);
				}
			}
/*
			if(cmbCUSTOM_LETTER_REQD.SelectedItem!=null && cmbCUSTOM_LETTER_REQD.SelectedItem.Value!="")
				objClsProcessInfo.CUSTOM_LETTER_REQD = int.Parse(cmbCUSTOM_LETTER_REQD.SelectedItem.Value);

			if(cmbSTD_LETTER_REQD.SelectedItem!=null && cmbSTD_LETTER_REQD.SelectedItem.Value!="")
				objClsProcessInfo.STD_LETTER_REQD = int.Parse(cmbSTD_LETTER_REQD.SelectedItem.Value);
*/
			//Other Rescission Date			
			if (cmbOtherRescissionDate.SelectedItem != null)
				objClsProcessInfo.OTHER_RES_DATE_CD = cmbOtherRescissionDate.SelectedValue;
			
//			if (cmbOtherRescissionDate.SelectedValue != "0")
//				objClsProcessInfo.OTHER_RES_DATE = ConvertToDate(txtDateTime.Text);
//			else
//				objClsProcessInfo.OTHER_RES_DATE = System.DateTime.Now;			
			
			if (txtDateTime.Text != "")
			{
				DateTime EffDate = ConvertToDate(txtDateTime.Text);				
								
				objClsProcessInfo.OTHER_RES_DATE  = EffDate;
			}

			return objClsProcessInfo;
		}

		/// <summary>
		/// Starts the process by calling the StartProcess method of ClsRescindProcess
		/// </summary>
		private void StartProcess()
		{
			try
			{
				Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo = new Cms.Model.Policy.Process.ClsProcessInfo();
				objProcessInfo = GetFormValues();
				
				objProcessInfo.CREATED_BY = objProcessInfo.COMPLETED_BY = int.Parse(GetUserId()); 
				objProcessInfo.CREATED_DATETIME = objProcessInfo.COMPLETED_DATETIME = System.DateTime.Now;			
				//objProcessInfo.DIARY_NOTE = objProcessInfo.DIARY_SUBJECT = "Rescind Process started";//			

				if (objClsRescindProcess.StartProcess(objProcessInfo) == true)
				{ 
					hidFormSaved.Value = "1";
					//saved successfully
					hidROW_ID.Value = objProcessInfo.ROW_ID.ToString();
					hidDisplayBody.Value = "True";
					hidNEW_POLICY_VERSION_ID.Value = objProcessInfo.NEW_POLICY_VERSION_ID.ToString();  
					lblMessage.Text = ClsMessages.FetchGeneralMessage("890");
					//Generating the xml of old data
					FillhidOldData();
					//Saving the session and refreshing the menu
					//Refresh the Policy Top.
					cltPolicyTop.CallPageLoad();
					SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
				}
				else
				{
					//Hiding the extra buttons
					HideButtons();

					hidDisplayBody.Value = "False";
					lblMessage.Text = ClsMessages.FetchGeneralMessage("594");
					//Refresh the Policy Top.
					cltPolicyTop.CallPageLoad();
					//Saving the session and refreshing the menu
					SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
				}
				
				lblMessage.Visible = true;

				//Saving the session and refreshing the menu
				//SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
			}
			catch(Exception objExp)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
				lblMessage.Text = objExp.Message.ToString();
				lblMessage.Visible = true;

				//Hiding the extra buttons
				HideButtons();
			}

		}

		/// <summary>
		/// Retreives the old data in the form of xml and will keep in hidOldData hidden field
		/// </summary>
		private void GetOldDataXml()
		{
			if (hidROW_ID.Value.Trim() != "")
			{
				hidOldData.Value = objClsRescindProcess.GetOldDataXml(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value),
					int.Parse(hidPOLICY_VERSION_ID.Value), int.Parse(hidROW_ID.Value));
			}
		}
		/// <summary>
		/// Sets the specified policy in session
		/// </summary>
		private void SetPolicyInSession(int PolicyID, int PolicyVersionID, int CustomerID)
		{
			base.SetPolicyInSession(PolicyID, PolicyVersionID, CustomerID);

			//Changing the client top also
			cltPolicyTop.PolicyID = PolicyID;
			cltPolicyTop.PolicyVersionID = PolicyVersionID;
			cltPolicyTop.CustomerID = CustomerID;
			cltPolicyTop.RefreshPolicy();

		}
	
		/// <summary>
		/// Sets the property of various validator controls
		/// </summary>
		private void SetValidators()
		{	 

			revEFFECTIVE_DATETIME.ValidationExpression	=	aRegExpDate;
			revEFFECTIVE_DATETIME.ErrorMessage			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
            rfvCANCELLATION_OPTION.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1227");//"Plese select Cancellation option.";
			rfvEFFECTIVE_DATETIME.ErrorMessage			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1228");//"Please select effective date.";
			rfvEFFECTIVE_HOUR.ErrorMessage				=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1213");//"Please insert Hours.";
			rfvEFFECTIVE_MINUTE.ErrorMessage			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1214");//"Please insert Minutes.";
			rfvCANCELLATION_TYPE.ErrorMessage			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("889");
			
			rfvMERIDIEM.ErrorMessage					=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1215");//"Please select AM/PM.";
			rfvOTHER_REASON.ErrorMessage				=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1229");//"Please insert Reason description.";
			rfvREASON.ErrorMessage						=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1210");//"Please select Reason.";	
			//this.csvCOMMENTS.ErrorMessage				=	"Please enter only 250 characters.";
			this.csvOTHER_REASON.ErrorMessage			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1211");//"Please enter only 250 characters.";
			//rfvCOMMENTS.ErrorMessage					=	"Please enter comments.";
			revRETURN_PREMIUM.ValidationExpression		=	aRegExpDoublePositiveWithZero;
			revRETURN_PREMIUM.ErrorMessage				=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"116");
			//Put the message in customized xml
            rfvDateTime.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1230");//"Please select Other Rescission Date.";
			revDateTime.ValidationExpression			=	aRegExpDate;
			revDateTime.ErrorMessage					=	ClsMessages.FetchGeneralMessage("22");			

		}

		private void SetPolicyTopControl()
		{
			cltPolicyTop.CustomerID = Convert.ToInt32(Request.Params["CUSTOMER_ID"]);
			cltPolicyTop.PolicyID = Convert.ToInt32(Request.Params["POLICY_ID"]);
			cltPolicyTop.PolicyVersionID = Convert.ToInt32(Request.Params["policyVersionID"]);
		}

		/// <summary>
		/// Retreives the query string values into hidden controls
		/// </summary>
		private void GetQueryString()
		{
			hidCUSTOMER_ID.Value		= Request.Params["CUSTOMER_ID"].ToString();
			hidPOLICY_ID.Value			= Request.Params["POLICY_ID"].ToString();
			hidPOLICY_VERSION_ID.Value	= Request.Params["policyVersionID"].ToString();
			
			
			if (Request.Params["process"].ToString().ToUpper() == "DECPOL")
				hidPROCESS_ID.Value = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_RESCIND.ToString();
			else if (Request.Params["process"].ToString().ToUpper() == "RDECPOL")
			{
				hidPROCESS_ID.Value = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ROLLBACK_RESCIND_PROCESS.ToString();
				btnComplete.Attributes.Add("style","display:none");
			}
			else if (Request.Params["process"].ToString().ToUpper() == "CDECPOL")
			{
				hidPROCESS_ID.Value = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_RESCIND_PROCESS.ToString();
				btnRollBack.Attributes.Add("style","display:none");
			}
			ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();			
			DataSet dsPolicy = objGeneralInformation.GetPolicyDataSet(int.Parse(hidCUSTOMER_ID.Value==""?"0": hidCUSTOMER_ID.Value) ,int.Parse (hidPOLICY_ID.Value ==""?"0":hidPOLICY_ID.Value),int.Parse (hidPOLICY_VERSION_ID.Value==""?"0":hidPOLICY_VERSION_ID.Value));

			if(dsPolicy.Tables[0].Rows[0]["UNDERWRITER"]!=null && dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString()!="" && dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString()!="0")
				hidUNDERWRITER.Value = dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString();
			if(dsPolicy.Tables[0].Rows[0]["POLICY_STATUS_CODE"]!=null && dsPolicy.Tables[0].Rows[0]["POLICY_STATUS_CODE"].ToString()!="" && dsPolicy.Tables[0].Rows[0]["POLICY_STATUS_CODE"].ToString()!="0")
			{
				hidPOLICY_PREVIOUS_STATUS.Value = dsPolicy.Tables[0].Rows[0]["POLICY_STATUS_CODE"].ToString();
				hidPOLICY_CURRENT_STATUS.Value  = dsPolicy.Tables[0].Rows[0]["POLICY_STATUS_CODE"].ToString();
			}
			
			hidLOB_ID.Value = dsPolicy.Tables[0].Rows[0]["POLICY_LOB"].ToString();
			//if(dsPolicy.Tables[0].Rows[0]["POLICY_PREVIOUS_STATUS"]!=null && dsPolicy.Tables[0].Rows[0]["POLICY_PREVIOUS_STATUS"].ToString()!="")
				//hidPRE_STATUS.Value = dsPolicy.Tables[0].Rows[0]["POLICY_PREVIOUS_STATUS"].ToString();
			
		}
		private void SetCaptions()
		{
			objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Processes.RescindProcess" ,System.Reflection.Assembly.GetExecutingAssembly());
			
			this.capCANCELLATION_OPTION.Text=	objResourceMgr.GetString("cmbCANCELLATION_OPTION");
			this.capCANCELLATION_TYPE.Text	=	objResourceMgr.GetString("cmbCANCELLATION_TYPE");
			//this.capEFFECTIVE_DATETIME.Text  	=	objResourceMgr.GetString("txtEFFECTIVE_DATETIME");
			this.capEFFECTIVE_TIME.Text		=	objResourceMgr.GetString("txtEFFECTIVE_TIME");
			this.capREASON.Text				=	objResourceMgr.GetString("cmbREASON");
			this.capOTHER_REASON.Text		=	objResourceMgr.GetString("txtOTHER_REASON");	

			this.capREQUESTED_BY.Text		=	objResourceMgr.GetString("capREQUESTED_BY");
			this.capAGENTPHONENO.Text		=	objResourceMgr.GetString("capAGENTPHONENO");
			this.capOtherRescissionDate.Text=	objResourceMgr.GetString("txtOTHER_RES_DATE_CD");
			this.capDateTime.Text				=   objResourceMgr.GetString("txtOTHER_RES_DATE");

			this.capRETURN_PREMIUM.Text		=	objResourceMgr.GetString("txtRETURN_PREMIUM");	
			//this.capPAST_DUE_PREMIUM.Text   =	objResourceMgr.GetString("capPAST_DUE_PREMIUM");
			//this.capPRINT_COMMENTS.Text		=	objResourceMgr.GetString("cmbPRINT_COMMENTS");
			//this.capCOMMENTS.Text			=	objResourceMgr.GetString("txtCOMMENTS");		

			this.capPRINTING_OPTIONS.Text	=	objResourceMgr.GetString("chkPRINTING_OPTIONS");
			this.capINSURED.Text			=	objResourceMgr.GetString("cmbINSURED");			
			this.capAGENCY_PRINT.Text		=	objResourceMgr.GetString("cmbAGENCY_PRINT");
			
			this.capADD_INT.Text			=	objResourceMgr.GetString("cmbADD_INT");				
			this.capSEND_ALL.Text			=	objResourceMgr.GetString("chkSEND_ALL");										
			
			//this.capCUSTOM_LETTER_REQD.Text =	objResourceMgr.GetString("cmbCUSTOM_LETTER_REQD");
			//this.capSTD_LETTER_REQD.Text 	=	objResourceMgr.GetString("cmbSTD_LETTER_REQD");											
							
		}

		/// <summary>
		/// Sets the security xml and type of button
		/// </summary>
		private void SetButtonsSecurityXML()
		{
			btnSave.CmsButtonClass = CmsButtonType.Write;
			btnSave.PermissionString = gstrSecurityXML;

			btnReset.CmsButtonClass = CmsButtonType.Read;
			btnReset.PermissionString = gstrSecurityXML;

			btnRollBack.CmsButtonClass = CmsButtonType.Read;
			btnRollBack.PermissionString = gstrSecurityXML;

			btnComplete.CmsButtonClass = CmsButtonType.Read;
			btnComplete.PermissionString = gstrSecurityXML;

			btnPolicyDetails.CmsButtonClass = CmsButtonType.Read;
			btnPolicyDetails.PermissionString = gstrSecurityXML;

			this.btnBack_To_Customer_Assistant.CmsButtonClass = CmsButtonType.Read;
			this.btnBack_To_Customer_Assistant.PermissionString = gstrSecurityXML;

			this.btnBack_To_Search.CmsButtonClass = CmsButtonType.Read;
			this.btnBack_To_Search.PermissionString = gstrSecurityXML;

			this.btnCalculate_Return_premium.CmsButtonClass = CmsButtonType.Execute;
			this.btnCalculate_Return_premium.PermissionString = gstrSecurityXML;

			btnCommitInProgress.CmsButtonClass = CmsButtonType.Read;
			btnCommitInProgress.PermissionString = gstrSecurityXML;


		}
		
		/// <summary>
		/// Populates other information on this page like agency phone no
		/// </summary>
		private void PopulateOtherInfo()
		{
			ClsRescindProcess objClsRescindProcess =new ClsRescindProcess();
			DataSet ds = objClsRescindProcess.GetAgencyPhoneNo(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value));
			lblAGENT_PHONE_NUMBER.Text = ds.Tables[0].Rows[0]["AGENCY_PHONE"].ToString();
			int POL_ID = int.Parse(hidPOLICY_ID.Value);
			int POL_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value) ;
			int CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value) ;			
			txtEFFECTIVE_DATETIME.Text = ClsRescindProcess.GetPolicyInceptionDate(CUSTOMER_ID,POL_ID,POL_VERSION_ID).ToString("MM/dd/yyyy");
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
			this.btnRollBack.Click += new System.EventHandler(this.btnRollBack_Click);
			this.btnPolicyDetails.Click += new System.EventHandler(this.btnPolicyDetails_Click);
			this.btnBack_To_Search.Click += new System.EventHandler(this.btnBack_To_Search_Click);
			this.btnBack_To_Customer_Assistant.Click += new System.EventHandler(this.btnBack_To_Customer_Assistant_Click);
			this.btnComplete.Click += new System.EventHandler(this.btnComplete_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnCalculate_Return_premium.Click += new System.EventHandler(this.btnCalculate_Return_premium_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		private void btnCalculate_Return_premium_Click(object sender, System.EventArgs e)
		{
			try
			{
				string strScript = "<script>setTimeout('CallService()', 1000);</script>";		
				hidFormSaved.Value = "3";
				ClientScript.RegisterStartupScript(this.GetType(),"CallService",strScript);				
			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;
			}
			finally
			{
			}

		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo = new Cms.Model.Policy.Process.ClsProcessInfo();
				Cms.Model.Policy.Process.ClsProcessInfo objProcessInfotmp ;
				ClsRescindProcess objRescendProcess=new ClsRescindProcess();
				objProcessInfo = GetFormValues();
				objProcessInfotmp=objRescendProcess.GetRunningProcess(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID);    
				objProcessInfo.POLICY_CURRENT_STATUS = objProcessInfotmp.POLICY_CURRENT_STATUS; 
				objProcessInfo.POLICY_PREVIOUS_STATUS  = objProcessInfotmp.POLICY_PREVIOUS_STATUS ;
				objProcessInfo.ROW_ID = int.Parse(hidROW_ID.Value);
				//Making model object which will contains old data
				Cms.Model.Policy.Process.ClsProcessInfo  objOldProcessInfo = new Cms.Model.Policy.Process.ClsProcessInfo();
				base.PopulateModelObject(objOldProcessInfo, hidOldData.Value);

				//Updating the previous process record
				objClsRescindProcess.BeginTransaction();
				objClsRescindProcess.UpdateProcessInformation(objOldProcessInfo, objProcessInfo);
				objClsRescindProcess.CommitTransaction();

				lblMessage.Text		= ClsMessages.FetchGeneralMessage("31");
				lblMessage.Visible	= true;
				hidFormSaved.Value = "1";
				LoadData();

				//Refresh the Policy Top.
				cltPolicyTop.CallPageLoad();
				//Updating the policy top,session and menus
				SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
			}
			catch(Exception objExp)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
				lblMessage.Text = objExp.Message.ToString();
				lblMessage.Visible = true;
				objClsRescindProcess.RollbackTransaction();
			}
		}

		#region Process Complete 

		/// <summary>
		/// Completes the Process.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnComplete_Click(object sender, System.EventArgs e)
		{
			//Local Variable Declartions
			ClsProcessInfo objProcessInfo = new ClsProcessInfo();
			ClsPolicyProcessNoticInfo objProcessNoticeInfo = new ClsPolicyProcessNoticInfo();
			ClsPolicyProcess objProcess = new ClsPolicyProcess();
			//int ReturnPremium = 0;		

			try
			{
				
				//Sets the Values
				objProcessInfo = GetFormValues();
				objProcessInfo.ROW_ID = int.Parse(hidROW_ID.Value);
				objProcessInfo.COMPLETED_BY = int.Parse(GetUserId());
				objProcessInfo.COMPLETED_DATETIME = System.DateTime.Now;
				objProcessInfo.POLICY_PREVIOUS_STATUS = hidPRE_STATUS.Value;
				if (objClsRescindProcess.CommitProcess(objProcessInfo) == true)
				{ 
					
					hidFormSaved.Value = "1";
					hidDisplayBody.Value = "True";
					lblMessage.Text = ClsMessages.FetchGeneralMessage("892");
					//objProcess.GetTotalReturnPremium(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.POLICY_VERSION_ID,objProcessInfo.ROW_ID,out ReturnPremium); 
					//txtRETURN_PREMIUM.Text = ReturnPremium.ToString();
					txtEFFECTIVE_DATETIME.ReadOnly=true;
					hidCOMMIT_FLAG.Value="True";	
					LoadData();
					//Hiding the extra buttons
					HideButtons();

				}
				else
				{
					hidDisplayBody.Value = "False";
					lblMessage.Text = ClsMessages.FetchGeneralMessage("601") + "<BR>" + ClsPolicyErrMsg.strMessage ;
					btnComplete.Attributes.Add("style","display:inline");
					btnCommitInProgress.Attributes.Add("style","display:none");

				}

				//Refresh the Policy Top.
				cltPolicyTop.CallPageLoad();

				lblMessage.Visible = true;

				//Updating the policy top,session and menus
				SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);

			}
			catch(Exception objExp)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
				lblMessage.Text = objExp.Message.ToString();
				lblMessage.Visible = true;
				btnComplete.Attributes.Add("style","display:inline");
				btnCommitInProgress.Attributes.Add("style","display:none");

			}
			finally
			{
				if (objProcess != null)
				{
					objProcess.Dispose();				
				}
				if (objProcessNoticeInfo != null)
				{ 
					objProcessNoticeInfo = null;
				}
			}
		}

		#endregion


		/// <summary>
		/// Hides the commit and rollback button
		/// </summary>
		private void HideButtons()
		{

			btnReset.Visible = false;
			btnRollBack.Visible = false;
			btnComplete.Visible = false;
			btnSave.Visible = false;
			btnCommitInProgress.Attributes.Add("style","display:none");

			
		}
		private double getReturnPremiumAmount(int CustomerId ,int PolicyId,int PolicyVersionId,DateTime ChangeEffDate,string CancellationType,string CancellatoinOption)
		{
			double retAmt = 0.00;
			try
			{
				CancellationType=CancellationType==""?"0":CancellationType;
				CancellatoinOption=CancellatoinOption==""?"0":CancellatoinOption;
				retAmt = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.CalculateReturnPremium(CustomerId,PolicyId,PolicyVersionId,ChangeEffDate,int.Parse(CancellationType),int.Parse(CancellatoinOption));
				return retAmt;	      
			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;
				return retAmt;
			}
		}
		#region Process RollBack
		private void btnRollBack_Click(object sender, System.EventArgs e)
		{
			
			//Local Variable Declartions
			ClsProcessInfo objProcessInfo =  new ClsProcessInfo();
			try
			{					
				
				//Sets the Values				
				objProcessInfo = GetFormValues();
				objProcessInfo.ROW_ID = int.Parse(hidROW_ID.Value);  
				objProcessInfo.PROCESS_ID = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ROLLBACK_RESCIND_PROCESS;
				objProcessInfo.COMPLETED_BY = int.Parse(GetUserId());
				objProcessInfo.COMPLETED_DATETIME = DateTime.Now;
				
				if (objClsRescindProcess.RollbackProcess(objProcessInfo) == true)
				{ 
					hidFormSaved.Value = "1";
					hidDisplayBody.Value = "True";
					hidNEW_POLICY_VERSION_ID.Value ="";
					lblMessage.Text = ClsMessages.FetchGeneralMessage("891");
					GetOldDataXml();
					//Hiding the extra buttons
					HideButtons();
					SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
				}
				else
				{
					hidDisplayBody.Value = "False";
					lblMessage.Text = ClsMessages.FetchGeneralMessage("602");
					//SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
				}
                
				lblMessage.Visible = true;

				//Refresh the Policy Top.
				cltPolicyTop.CallPageLoad();

				lblMessage.Visible = true;
				
				//Updating the policy top,session and menus
				//SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);

			}
			catch(Exception objExp)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
				lblMessage.Text = objExp.Message.ToString();
				lblMessage.Visible = true;		
			}
		}
		#endregion

		private void btnBack_To_Search_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("/cms/client/aspx/CustomerManagerSearch.aspx");
		}

		private void btnBack_To_Customer_Assistant_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("/cms/client/aspx/CustomerManagerIndex.aspx?Customer_ID=" + hidCUSTOMER_ID.Value);
		}

		/// <summary>
		/// Sets the process information 
		/// </summary>
		private void PopulateProcessInfo()
		{			
			ClsPolicyProcess objPro = new ClsPolicyProcess();
			ClsProcessInfo objProcess = objPro.GetRunningProcess(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value));
			if (objProcess !=null)
			{
				hidPOLICY_VERSION_ID.Value = objProcess.POLICY_VERSION_ID.ToString();
				hidNEW_POLICY_VERSION_ID.Value =objProcess.NEW_POLICY_VERSION_ID.ToString();   
				hidROW_ID.Value = objProcess.ROW_ID.ToString();
				hidPOLICY_PREVIOUS_STATUS.Value = objProcess.POLICY_PREVIOUS_STATUS; 
				hidPOLICY_CURRENT_STATUS.Value = objProcess.POLICY_CURRENT_STATUS;   
				hidDisplayBody.Value = "True";
				LoadData();
				ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();			
				DataSet dsPolicy = objGeneralInformation.GetPolicyDataSet(int.Parse(hidCUSTOMER_ID.Value==""?"0": hidCUSTOMER_ID.Value) ,int.Parse (hidPOLICY_ID.Value ==""?"0":hidPOLICY_ID.Value),int.Parse (hidNEW_POLICY_VERSION_ID.Value==""?"0":hidNEW_POLICY_VERSION_ID.Value));

				if(dsPolicy.Tables[0].Rows[0]["UNDERWRITER"]!=null && dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString()!="" && dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString()!="0")
					hidUNDERWRITER.Value = dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString();

				hidLOB_ID.Value = dsPolicy.Tables[0].Rows[0]["POLICY_LOB"].ToString();
				dsPolicy.Clear();
				dsPolicy.Dispose(); 
				//Saving the session and refreshing the menu
				//SetPolicyInSession(objProcess.POLICY_ID, objProcess.POLICY_VERSION_ID, objProcess.CUSTOMER_ID);
				if (hidNEW_POLICY_VERSION_ID.Value !="" &&  hidNEW_POLICY_VERSION_ID.Value !="0" )
					SetPolicyInSession(objProcess.POLICY_ID, objProcess.NEW_POLICY_VERSION_ID , objProcess.CUSTOMER_ID);
				else
					SetPolicyInSession(objProcess.POLICY_ID, objProcess.POLICY_VERSION_ID, objProcess.CUSTOMER_ID);
			}
			else
			{
				SetPolicyInSession(int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value), int.Parse(hidCUSTOMER_ID.Value));
				hidDisplayBody.Value = "False";
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1205");//"No Process in Progress on this Policy.";
				lblMessage.Visible=true;
			}
		}

		private void LoadData()
		{
		
			DataSet dsTemp = new DataSet();

			dsTemp = ClsPolicyProcess.GetProcessInfoDataSet(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value),
				int.Parse(hidPOLICY_VERSION_ID.Value), int.Parse(hidROW_ID.Value));

			if (dsTemp.Tables[0].Rows.Count > 0)
			{	
				if (dsTemp.Tables[0].Rows[0]["EFFECTIVETIME"] != DBNull.Value)
				{
					int hour = 0;
					hour = Convert.ToInt32(Convert.ToDateTime(dsTemp.Tables[0].Rows[0]["EFFECTIVETIME"]).Hour.ToString());
					
					if(hour == 00)//Added by Charles on 31-Aug-09 for Itrack 6323
					{
						hour = 12;
						cmbMERIDIEM.SelectedIndex= 0;
					}
					else if(hour == 12)
					{
						cmbMERIDIEM.SelectedIndex= 1;
					}//Added till here
					else if (hour > 12)
					{
						hour = hour - 12;
					}
					else //else added by Charles on 31-Aug-09 for Itrack 6323
						cmbMERIDIEM.SelectedIndex= 0;
					
					txtEFFECTIVE_HOUR.Text =   hour.ToString();

					txtEFFECTIVE_MINUTE.Text = Convert.ToDateTime(dsTemp.Tables[0].Rows[0]["EFFECTIVETIME"]).Minute.ToString();
				}

				if (dsTemp.Tables[0].Rows[0]["EFFECTIVE_DATETIME"] != DBNull.Value)
				{
					txtEFFECTIVE_DATETIME.Text = dsTemp.Tables[0].Rows[0]["EFFECTIVE_DATETIME"].ToString();
				}
				/*
				if (dsTemp.Tables[0].Rows[0]["COMMENTS"] != DBNull.Value)
				{
					txtCOMMENTS.Text = dsTemp.Tables[0].Rows[0]["COMMENTS"].ToString();
				}
				
				if (dsTemp.Tables[0].Rows[0]["PRINT_COMMENTS"] != DBNull.Value)
				{
					cmbPRINT_COMMENTS.SelectedValue = dsTemp.Tables[0].Rows[0]["PRINT_COMMENTS"].ToString();
				}*/
				
				if (dsTemp.Tables[0].Rows[0]["REQUESTED_BY"] != DBNull.Value)
				{
					cmbREQUESTED_BY.SelectedIndex = cmbREQUESTED_BY.Items.IndexOf(cmbREQUESTED_BY.Items.FindByValue(dsTemp.Tables[0].Rows[0]["REQUESTED_BY"].ToString()));
				}
				
				if (dsTemp.Tables[0].Rows[0]["CANCELLATION_OPTION"] != DBNull.Value)
				{
					cmbCANCELLATION_OPTION.SelectedIndex = cmbCANCELLATION_OPTION.Items.IndexOf(cmbCANCELLATION_OPTION.Items.FindByValue(dsTemp.Tables[0].Rows[0]["CANCELLATION_OPTION"].ToString()));
				}				
			
				if (dsTemp.Tables[0].Rows[0]["CANCELLATION_TYPE"] != DBNull.Value)
				{
					cmbCANCELLATION_TYPE.SelectedIndex = cmbCANCELLATION_TYPE.Items.IndexOf(cmbCANCELLATION_TYPE.Items.FindByValue(dsTemp.Tables[0].Rows[0]["CANCELLATION_TYPE"].ToString()));
				}
				
				if (dsTemp.Tables[0].Rows[0]["REASON"] != DBNull.Value && dsTemp.Tables[0].Rows[0]["REASON"].ToString().Trim() != "0")
				{
					cmbREASON.SelectedIndex = cmbREASON.Items.IndexOf(cmbREASON.Items.FindByValue(dsTemp.Tables[0].Rows[0]["REASON"].ToString()));
				}
			
				if (dsTemp.Tables[0].Rows[0]["OTHER_REASON"] != DBNull.Value)
				{
					txtOTHER_REASON.Text = dsTemp.Tables[0].Rows[0]["OTHER_REASON"].ToString();
				}
				
				if (dsTemp.Tables[0].Rows[0]["RETURN_PREMIUM"] != DBNull.Value && dsTemp.Tables[0].Rows[0]["RETURN_PREMIUM"].ToString()!="0")
				{
					txtRETURN_PREMIUM.Text=String.Format("{0:,#,###}",Convert.ToInt64(dsTemp.Tables[0].Rows[0]["RETURN_PREMIUM"]));				
				}
				
				if (dsTemp.Tables[0].Rows[0]["OTHER_RES_DATE"] != DBNull.Value)
				{
					txtDateTime.Text = dsTemp.Tables[0].Rows[0]["OTHER_RES_DATE"].ToString();
				}
				if (dsTemp.Tables[0].Rows[0]["OTHER_RES_DATE_CD"] != DBNull.Value)
				{
					cmbOtherRescissionDate.SelectedIndex= cmbOtherRescissionDate.Items.IndexOf(cmbOtherRescissionDate.Items.FindByValue(dsTemp.Tables[0].Rows[0]["OTHER_RES_DATE_CD"].ToString()));
				}

				hidOldData.Value = dsTemp.GetXml();
				
			}
		}

		private void FillhidOldData()
		{			
			DataSet dsTemp = ClsPolicyProcess.GetProcessInfoDataSet(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value),
				int.Parse(hidPOLICY_VERSION_ID.Value), int.Parse(hidROW_ID.Value));
			if(dsTemp != null && dsTemp.Tables[0].Rows.Count > 0)
				hidPRE_STATUS.Value = dsTemp.Tables[0].Rows[0]["POLICY_PREVIOUS_STATUS"].ToString();
			hidOldData.Value = dsTemp.GetXml();
		}

		private void btnPolicyDetails_Click(object sender, System.EventArgs e)
		{
		
		}

		[Ajax.AjaxMethod()]
		public string AjaxCancelProcReturnPremium(string CustomerId, string PolicyId, string PolicyVersionId, string ChangeEffDate)
		{
			Cms.CmsWeb.webservices.ClsWebServiceCommon obj =  new Cms.CmsWeb.webservices.ClsWebServiceCommon();
			string result = "";
			result = obj.CancelProcReturnPremium(CustomerId,PolicyId,PolicyVersionId,ChangeEffDate);
			return result;
			
		}

	}
}
