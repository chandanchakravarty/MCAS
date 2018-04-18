/******************************************************************************************
<Author					: -  Charles Gomes
<Start Date				: -	 12-Jan-2010
<End Date				: -	
<Description			: -  Culture & Language Support for Application Page	
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: 
<Modified By			: 
<Purpose				: 
						
****************************************************************************************** */ 

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
using Cms.Model.Application;
using Cms.Model.Quote;
using Cms.CmsWeb;
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;
using Cms.BusinessLayer.BlProcess;
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlQuote ;
using Cms.BusinessLayer.BlClient;
using Cms.BusinessLayer.BlAccount;
using System.Globalization;
using System.Threading;

namespace Cms.Application.Aspx
{	
	public class GeneralInfoEx  : Cms.Application.appbase 
	{
		#region Page controls declaration
			 
		protected System.Web.UI.WebControls.TextBox txtAPP_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtAPP_STATUS;
		protected System.Web.UI.WebControls.TextBox txtAPP_VERSION;
		protected System.Web.UI.WebControls.DropDownList cmbAPP_TERMS;
		protected System.Web.UI.WebControls.TextBox txtAPP_INCEPTION_DATE;
		protected System.Web.UI.WebControls.TextBox txtAPP_EFFECTIVE_DATE;
		protected System.Web.UI.WebControls.TextBox txtAPP_EXPIRATION_DATE;
		protected System.Web.UI.WebControls.Label lblAGENCY_DISPLAY_NAME;
		protected System.Web.UI.WebControls.Label lblManHeader;
		protected System.Web.UI.WebControls.Label lblAppHeader;
		protected System.Web.UI.WebControls.Label lblTermHeader;
		protected System.Web.UI.WebControls.Label lblAgencyHeader;
		protected System.Web.UI.WebControls.Label lblBillingHeader;
		protected System.Web.UI.WebControls.Label lblAllPoliciesHeader;
		protected System.Web.UI.WebControls.DropDownList cmbAPP_LOB;
		protected System.Web.UI.WebControls.DropDownList cmbAPP_SUBLOB;
		protected System.Web.UI.HtmlControls.HtmlSelect cmbCSR;
		protected System.Web.UI.HtmlControls.HtmlSelect cmbProducer;

		protected Cms.CmsWeb.Controls.CmsButton btnBack;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected System.Web.UI.WebControls.DropDownList cmbDOWN_PAY_MODE;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidReset;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyNumber;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDOWN_PAY_MODE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidBILL_TYPE_FLAG;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIsTerminated; 
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCultureName;
		 
		protected Cms.CmsWeb.Controls.CmsButton btnReset;		
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
	
		protected System.Web.UI.HtmlControls.HtmlTableRow policyTR; 

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAPP_TERMS;		
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAPP_EFFECTIVE_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAPP_EXPIRATION_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAPP_LOB;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSTATE_ID;		
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCUSTOMER_ID;
		protected System.Web.UI.WebControls.RangeValidator rngYEAR_AT_CURR_RESI;
		protected System.Web.UI.WebControls.DropDownList cmbPOLICY_TYPE;
		protected System.Web.UI.WebControls.Label capPOLICY_TYPE;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		protected System.Web.UI.WebControls.Label lblPolicies;		
		protected System.Web.UI.WebControls.Label lblAllPolicy;		
		protected System.Web.UI.WebControls.Label lblPoliciesDiscount;		
		protected System.Web.UI.WebControls.Label lblEligbilePolicy;		
		protected System.Web.UI.WebControls.Label capUNDERWRITER;	
		protected System.Web.UI.WebControls.Label capBILL_MORTAGAGEE;			
		protected System.Web.UI.WebControls.Label lblBILL_MORTAGAGEE;
		protected System.Web.UI.WebControls.Label capDOWN_PAY_MODE;	

		#endregion

		#region Local form variables
		//START:*********** Local form variables *************
		bool PrintingError;
		string oldXML;
		string strLOB_ID = "";
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId="", strFormSaved;		
		protected System.Web.UI.WebControls.Label capCUSTOMER_ID;
		protected System.Web.UI.WebControls.TextBox txtCUSTOMERNAME;
		protected System.Web.UI.WebControls.Label capAPP_STATUS;
		protected System.Web.UI.WebControls.Label capAPP_NUMBER;
		protected System.Web.UI.WebControls.Label capAPP_VERSION;
		protected System.Web.UI.WebControls.Label capAPP_TERMS;
		protected System.Web.UI.WebControls.Label capAPP_INCEPTION_DATE;
		protected System.Web.UI.WebControls.Label capAPP_EFFECTIVE_DATE;
		protected System.Web.UI.WebControls.Label capAPP_EXPIRATION_DATE;
		protected System.Web.UI.WebControls.Label capAPP_LOB;
		protected System.Web.UI.WebControls.Label capAPP_SUBLOB;
		protected System.Web.UI.WebControls.Label capAPP_AGENCY_ID;
		protected System.Web.UI.WebControls.Label capCSR;
		protected System.Web.UI.WebControls.Label capProducer;
		protected System.Web.UI.WebControls.Label lblChooseLang;
		protected System.Web.UI.WebControls.HyperLink hlkCalandarDate;
		protected System.Web.UI.WebControls.HyperLink hlkInceptionDate;
		public string primaryKeyValues="";
		public string strNewVersion="";	
		public int strVersionID=-1;
		//private int AGENCY_ID;
		public string strPolicy;
		protected int gIntPopulate = 0,gIntShowQuote=0,gIntShowVerificationResult=0,intSubmitAnyway=0;
		protected string gStrOldXML="";
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSubLOBXml;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_AGENCY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCSR;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidProducer;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCallefroms;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOBID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSUB_LOB;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOBXML,hidDEPT_ID,hidPC_ID,hidDepartmentXml,hidProfitCenterXml,hidPOLICY_TYPE;
		
		protected Cms.CmsWeb.Controls.CmsButton btnCopy;
		private const string CALLED_FROM_CLIENT = "CLT";
		private const string CALLED_FROM_INNER_CLIENT ="InCLT";
		private const string CALLED_FROM_APPLICATION = "APP";
		private const string DefaultAppStatus="Incomplete";
		private const string DefaultAppStatusPt="Incompleto";

		protected System.Web.UI.WebControls.RegularExpressionValidator revAPP_EFFECTIVE_DATE;
		protected Cms.CmsWeb.Controls.CmsButton btnQuote;
		protected System.Web.UI.WebControls.TextBox txtLOB;
		protected System.Web.UI.WebControls.TextBox txtCUSTOMER_NAME;
		protected System.Web.UI.HtmlControls.HtmlImage imgSelect;
	 
		private const string TEMP_APP_NUMBER ="To be generated";
		private const string TEMP_APP_NUMBER_PT ="Para ser gerada";

		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDeleteApp;
		protected System.Web.UI.WebControls.Label capSTATE_ID;
		protected System.Web.UI.WebControls.DropDownList cmbSTATE_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidStateID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidUnderwriter;
		protected System.Web.UI.WebControls.RegularExpressionValidator revAPP_INCEPTION_DATE;
		protected System.Web.UI.WebControls.Label lblFormLoadMessage;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.HtmlControls.HtmlTableRow trFORMMESSAGE;
		protected System.Web.UI.HtmlControls.HtmlTableRow trDETAILS;
		protected System.Web.UI.WebControls.Label capBILL_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbBILL_TYPE_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvBILL_TYPE;
		protected System.Web.UI.WebControls.Label capINSTALL_PLAN_ID,capCOMPLETE_APP,capPROPRTY_INSP_CREDIT;
		protected System.Web.UI.WebControls.DropDownList cmbINSTALL_PLAN_ID;
		protected System.Web.UI.WebControls.CheckBox chkCOMPLETE_APP;
		protected System.Web.UI.WebControls.DropDownList cmbPROPRTY_INSP_CREDIT;
		protected Cms.CmsWeb.Controls.CmsButton btnCustomerAssistant;
		protected System.Web.UI.WebControls.Label capCHARGE_OFF_PRMIUM;		
		protected System.Web.UI.WebControls.Label capRECEIVED_PRMIUM;
		protected System.Web.UI.WebControls.TextBox txtRECEIVED_PRMIUM;
		protected System.Web.UI.WebControls.Label capPROXY_SIGN_OBTAINED;
		protected System.Web.UI.HtmlControls.HtmlTableRow trCOMPLETE_APP;
		protected System.Web.UI.HtmlControls.HtmlTableRow trPropInspCr;
		 
		protected int gIntQuoteID=0,gIntCUSTOMER_ID=0,gIntAPP_ID=0,gIntAPP_VERSION_ID=0;
		protected string gstrLobID="";
		protected System.Web.UI.WebControls.RegularExpressionValidator revCHARGE_OFF_PRMIUM;
		protected System.Web.UI.WebControls.RegularExpressionValidator revRECEIVED_PRMIUM;
		//Defining the business layer class object
		ClsGeneralInformation  objGeneralInformation ;
		ClsMessages objMessages;
		 
		public string appTerms;
		public string divID;
		public string delStr="0";

		public int deptID;
		public string PCID;
		protected System.Web.UI.WebControls.Label capAPP_POLICY_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPOLICY_TYPE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidQuoteXML;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE_ID;
		protected System.Web.UI.WebControls.DropDownList cmbCHARGE_OFF_PRMIUM;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		protected System.Web.UI.WebControls.DropDownList cmbChooseLang;
		protected Cms.CmsWeb.Controls.CmsButton btnVerifyApplication;		
		protected Cms.CmsWeb.Controls.CmsButton btnConvertAppToPolicy;
		
		public string exp_Date;
		
		private const string LOB_HOME="1";
		private const string LOB_PRIVATE_PASSENGER="2";
		private const string LOB_MOTORCYCLE="3";
		private const string LOB_WATERCRAFT="4";
		private const string LOB_UMBRELLA="5";
		private const string LOB_RENTAL_DWELLING="6";
		private const string LOB_AVIATION="8";

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPROXY_SIGN_OBTAINED;
		protected System.Web.UI.WebControls.DropDownList cmbPROXY_SIGN_OBTAINED;
		protected System.Web.UI.WebControls.Label capYEAR_AT_CURR_RESI;
		protected System.Web.UI.WebControls.TextBox txtYEAR_AT_CURR_RESI;
		protected System.Web.UI.WebControls.RegularExpressionValidator revYEAR_AT_CURR_RESI;
		protected System.Web.UI.WebControls.Label capYEARS_AT_PREV_ADD;
		protected System.Web.UI.WebControls.TextBox txtYEARS_AT_PREV_ADD;
		protected System.Web.UI.WebControls.CustomValidator csvYEARS_AT_PREV_ADD;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIs_Agency;
		protected System.Web.UI.WebControls.CustomValidator csvAPP_EFFECTIVE_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvYEARS_AT_PREV_ADD;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRuleVerify;
		protected Cms.CmsWeb.Controls.CmsButton btnSubmitAnyway;
		protected System.Web.UI.HtmlControls.HtmlTableRow trbutton;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINSTALL_PLAN_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFULL_PAY_PLAN_ID;
		protected System.Web.UI.WebControls.Label lblUNDERWRITER;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidBillingPlan;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFocusFlag;
		protected System.Web.UI.WebControls.CustomValidator csvINSTALL_PLAN_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPROPRTY_INSP_CREDIT;
		protected System.Web.UI.WebControls.Label capPIC_OF_LOC;
		protected System.Web.UI.WebControls.DropDownList cmbPIC_OF_LOC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPIC_OF_LOC;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAlert;
		protected System.Web.UI.WebControls.Label capBILL_TYPE_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDOWN_PAY_MODE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidBILL_TYPE_ID;
		protected System.Web.UI.WebControls.Label lblDOWN_PAY_MODE;
		protected System.Web.UI.WebControls.Label lblINSTALL_PLAN_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidINSTALL_PLAN_ID;		
		
		//protected System.Web.UI.HtmlControls.HtmlInputButton btnFetcgIIX;
		private const string LOB_GENERAL_LIABILITY="7";
        //Changed by Charles on 19-May-10 for Itrack 51
        public string strCarrierSystemID;//= CarrierSystemID; //System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID").ToString();
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDEACTIVE_INSTALL_PLAN_ID;
		public string strAgency_ID="";
		protected Cms.CmsWeb.Controls.CmsButton btnSubmitInProgress;
		protected Cms.CmsWeb.Controls.CmsButton btnSubmitAnywayInProgress;
		public string userID;		
		
		#endregion
		
		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			lblMessage.Visible = false;
			strPolicy="0";			
		
			#region Set CustID , AppID, AppVerID
			gIntCUSTOMER_ID		= int.Parse(Request["CUSTOMER_ID"] == null ? "0" : Request["CUSTOMER_ID"].ToString());
			if (hidOldData.Value.Trim() !=null && hidOldData.Value.Trim() !="" && hidOldData.Value.Trim() !="0")
			{
				gIntAPP_ID 			=	int.Parse(Request["APP_ID"] == null || Request["APP_ID"] =="" ? FetchValueFromXML("APP_ID",hidOldData.Value) : Request["APP_ID"].ToString());
				gIntAPP_VERSION_ID 	=	int.Parse(Request["APP_VERSION_ID"] == null || Request["APP_VERSION_ID"] ==""  ?  FetchValueFromXML("APP_VERSION_ID",hidOldData.Value) : Request["APP_VERSION_ID"].ToString());
			}
			#endregion		
	
			objResourceMgr = new System.Resources.ResourceManager("Application.Application",System.Reflection.Assembly.GetExecutingAssembly());	
					
			if(!Page.IsPostBack)
			{				
				#region !PostBack

				#region Add Attributes 	
				hlkCalandarDate.Attributes.Add("OnClick","fPopCalendar(document.APP_LIST.txtAPP_EFFECTIVE_DATE,document.APP_LIST.txtAPP_EFFECTIVE_DATE)"); //Javascript Implementation for Calender				
				hlkInceptionDate.Attributes.Add("OnClick","fPopCalendar(document.APP_LIST.txtAPP_INCEPTION_DATE,document.APP_LIST.txtAPP_INCEPTION_DATE)");
				btnBack.Attributes.Add("onclick","javascript:return DoBack();");
				btnCustomerAssistant.Attributes.Add("onclick","javascript:return DoBackToAssistant();");
				txtAPP_EFFECTIVE_DATE.Attributes.Add("onblur","javascript:setTimeout('ChangeDefaultDate();',100);");
				txtAPP_EFFECTIVE_DATE.Attributes.Add("onChange","javascript:ChangeDefaultDate();");
				btnDelete.Attributes.Add("onclick","javascript:return ShowAlertMessageForDelete();");
				txtYEAR_AT_CURR_RESI.Attributes.Add("onblur","javascript:DisplayPreviousYearDesc()");
				btnConvertAppToPolicy.Attributes.Add("onclick","javascript:HideShowSubmit();");
				btnSubmitAnyway.Attributes.Add("onclick","javascript:HideShowSubmitAnyway();");
			
				//For Lookup
				string rootPath = Cms.BusinessLayer.BlCommon.ClsCommon.GetApplicationPath();
				string url = rootPath + @"/cmsweb/aspx/LookupForm1.aspx";
				imgSelect.Attributes.Add("onclick","javascript:OpenLookupWithFunction('" + url + "','CUSTOMER_ID','Name','hidCustomerID','txtCUSTOMER_NAME','CustAndStateLookupForm','Customer','','SetLookupValues()')");
				btnReset.Attributes.Add("onclick","javascript:return ResetForm();");
				//added on 11 sep 2007
				btnSave.Attributes.Add("onclick","javascript:SetBillTypeFlag();");
				#endregion

				PrintingError = false;
				btnVerifyApplication.Attributes.Add("onClick","ShowDialog();return false;");	
				cmbAPP_TERMS.Attributes.Add("onChange","return cmbAPP_TERMS_Change();");
				cmbAPP_TERMS.Attributes.Add("onkeypress","javascript:if(event.keyCode==13){fillBillingPlan();}");//Added by Charles on 6-Oct-09 for Itrack 6162
				txtAPP_INCEPTION_DATE.Text = DateTime.Now.ToString("MM/dd/yyyy");				
				txtAPP_EFFECTIVE_DATE.Text = DateTime.Now.ToString("MM/dd/yyyy");
				string strSystemID = GetSystemId(); 
                //Changed by Charles on 19-May-10 for Itrack 51
                string strCarrierSystemID = CarrierSystemID;//System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID").ToString();
				if ( strSystemID.Trim().ToUpper() != strCarrierSystemID.Trim().ToUpper())
				{
					hidIs_Agency.Value="1";
				}//END OF SYSTEM ID CHECK		
				#region Set Errors / Captions / Fill Controls / Billing Plan

				if(Session["Default_Culture"] != null)
				{
					if(Session["Default_Culture"].ToString() == "pt-BR")
					{
						cmbChooseLang.SelectedIndex = 1;						
					}				
				}				
				//cmbChooseLang_SelectedIndexChanged(null,null);
				

				//SetErrorMessages();				
				//SetCaptions();				
				FillControls();				
				#endregion

				#region Display Check Complete App Bonus / Convert Button on condition
				// for wolverine user display chkCOMPLETE_APP
				userID = GetSystemId();
				if (userID.ToUpper() != CarrierSystemID.ToUpper())
				{
					//trCOMPLETE_APP.Visible=false;
					btnConvertAppToPolicy.Attributes.Add("Style","display:none");				
				}
				else
				{
					//trCOMPLETE_APP.Visible=true;
					btnConvertAppToPolicy.Attributes.Add("Style","display:inline");
				}
				#endregion
				
				hidLOBXML.Value = ClsCommon.GetXmlForLobByState();
				trFORMMESSAGE.Visible =false;
				trDETAILS.Visible=false;	
				// Fill page for Add and Edit mode			
				bool showDetails=true;
				if (Request.QueryString["APP_ID"]!=null && Request.QueryString["APP_ID"].ToString()!="") //Edit Mode
				{
					#region Fill hidden variables : Cust, App, AppVer, FormSaved,Old Data, Called From
					//Done for Itrack Issue 6133 on 25 Aug 2009
					hidCustomerID.Value = Request.QueryString["CUSTOMER_ID"].Trim().ToString();
					hidAppID.Value =Request.QueryString["APP_ID"].Trim().ToString();//strTemp[1].ToString();
					hidAppVersionID.Value =Request.QueryString["APP_VERSION_ID"].Trim().ToString();//strTemp[2].ToString();
					hidFormSaved.Value ="0";
					hidOldData.Value = @ClsGeneralInformation.FetchApplicationXML(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidAppID.Value ==""?"0":hidAppID.Value),int.Parse (hidAppVersionID.Value==""?"0":hidAppVersionID.Value));
					hidCalledFrom.Value=Request.QueryString["CALLEDFROM"].ToString();					
					#endregion
					
					GetPolicyDetails();
					string strOldLobString="";
					try
					{
						strOldLobString = GetLOBString();
					}
					catch
					{
					}
                   
					if (hidOldData.Value != null && hidOldData.Value !="")
					{
						//Get the agency code and populate the CSR dropdown
						string strAGENCYID = FetchValueFromXML("APP_AGENCY_ID",hidOldData.Value);
						string strBillTypeID = FetchValueFromXML("BILL_TYPE_ID",hidOldData.Value);
						FillCSRDropDown(int.Parse(strAGENCYID ==""?"0":strAGENCYID ));
						FillProducerDropDown(int.Parse(strAGENCYID ==""?"0":strAGENCYID ));
						// Get the LOBID
						strLOB_ID = FetchValueFromXML("LOB_ID",hidOldData.Value);
						hidLOBID.Value =strLOB_ID;
						cmbAPP_LOB.SelectedValue = hidLOBID.Value;
						hidSTATE_ID.Value = FetchValueFromXML("STATE_ID",hidOldData.Value);
						cmbSTATE_ID.SelectedValue = hidSTATE_ID.Value;
						cmbAPP_LOB_SelectedIndexChanged(null,null);
						cmbPOLICY_TYPE.Attributes.Add("style","enabled:none");
						
						hidPOLICY_TYPE.Value =FetchValueFromXML("POLICY_TYPE",hidOldData.Value);
						cmbPOLICY_TYPE.SelectedIndex =cmbPOLICY_TYPE.Items.IndexOf(cmbPOLICY_TYPE.Items.FindByValue(hidPOLICY_TYPE.Value));
						cmbBILL_TYPE_ID.SelectedIndex = cmbBILL_TYPE_ID.Items.IndexOf(cmbBILL_TYPE_ID.Items.FindByValue(strBillTypeID));
						//hidFormSaved value can be changed inside aboove function calls, 
						//Hence we are reseting its value to 0
						//Because page is not postback hence new record mode
						hidFormSaved.Value = "0";

						# region Set LOB String
						switch(strLOB_ID)
						{
							case LOB_HOME:
								SetLOBString("HOME");
								break;
							case LOB_PRIVATE_PASSENGER:
								SetLOBString("PPA");
								break;
							case LOB_MOTORCYCLE:
								SetLOBString("MOT");
								break;
							case LOB_WATERCRAFT:
								SetLOBString("WAT");
								break;
							case LOB_RENTAL_DWELLING:
								SetLOBString("RENT");
								break;
							case LOB_UMBRELLA:
								SetLOBString("UMB");
								break;
							case LOB_GENERAL_LIABILITY:
								SetLOBString("GEN");
								break;		
							case LOB_AVIATION:
								SetLOBString("AVIATION");
								break;		
								
						}
						hidCallefroms.Value =GetLOBString();
						#endregion

						//November 9,2005:Sumit Chhabra:policyTR row was shown only for Homeowner(strLOBID=1)
						//								when it also be visible for RentalDwelling(strLOBID=6)
						if(strLOB_ID!="")
							if(int.Parse(strLOB_ID)==1 || int.Parse(strLOB_ID)==6)
								policyTR.Visible=true; 
							else
								policyTR.Visible=false; 
						SetApplicationCookies(FetchValueFromXML("APP_NUMBER",hidOldData.Value),hidCustomerID.Value,hidAppID.Value,hidAppVersionID.Value);  
						txtLOB.Text = ClsGeneralInformation.GetLOBNameByID(int.Parse(strLOB_ID.Trim()==""?"0":strLOB_ID));
					}//END OF HIDOLDDATA CHECK
					SetAppID(hidAppID.Value.ToString());
					SetCalledFor("APPLICATION");
					SetAppVersionID(hidAppVersionID.Value.ToString());
					SetCustomerID(hidCustomerID.Value.ToString());				 	 

					//Loading the application menu 
					base.ReloadApplicationMenu("");
				

					#region Show/Hide Buttons
					rfvCUSTOMER_ID.Enabled =false;
					btnCopy.Visible =true;					
					btnActivateDeactivate.Visible = true;
					btnDelete.Visible =true;
					btnVerifyApplication.Visible =true;					
					//visible to only wolverine user
					if(strSystemID.ToUpper()==strCarrierSystemID.ToUpper())
						this.btnSubmitAnyway.Visible=true;
					else
						this.btnSubmitAnyway.Visible=false;
					#endregion
				}//END OF IF PART OF REQUEST CHECK
				else	// Add Mode							
				{	
					//Add Mode
					/* If the control is coming from the customer section then the customer name cannot be chaged.
					 * We will show the label in this case */
					policyTR.Visible=false; 
					//btnQuote.Visible =false;
					btnCopy.Visible =false;
					btnActivateDeactivate.Visible = false;
					if (userID.ToUpper() != CarrierSystemID.ToUpper())
					{
						chkCOMPLETE_APP.Checked = true;
					}
					
					if (Request.QueryString["CALLEDFROM"]!= null &&(Request.QueryString["CALLEDFROM"].ToString().Equals(CALLED_FROM_CLIENT)||Request.QueryString["CALLEDFROM"].ToString().Equals(CALLED_FROM_INNER_CLIENT)))
					{
						#region Fill Hidden Values (Cust/App/App Ver/Called From)
						hidFormSaved.Value ="4";
						hidCustomerID.Value		= GetCustomerID();// Request.QueryString["CUSTOMER_ID"]==null?"NEW":Request.QueryString["CUSTOMER_ID"];										 
						hidAppID.Value		= GetAppID();
						hidAppVersionID.Value = GetAppVersionID();
						hidCalledFrom.Value=Request.QueryString["CALLEDFROM"].ToString();  
						#endregion

						#region Check Customer Status / Fill CSR, Producer
						/*	CHECK THE STATUS OF THE CUSTOMER. IF THE CUSTOMER IS INACTIVE THEN SHOW MESSAGE AND EXIT ELSE
						 * 	GET THE CUSTOMER NAME AND STATE ID */
						string stateID="";
						
						DataSet dsCustomer = ClsCustomer.GetCustomerDetails(int.Parse(hidCustomerID.Value.ToString()));
						if (dsCustomer!=null && dsCustomer.Tables[0].Rows.Count>0)
						{
							if (dsCustomer.Tables[0].Rows[0]["IS_CUSTOMER_ACTIVE"].ToString().Trim()=="" || dsCustomer.Tables[0].Rows[0]["IS_CUSTOMER_ACTIVE"].ToString().Trim()=="Y")
							{
								txtCUSTOMERNAME.Text = (dsCustomer.Tables[0].Rows[0]["CUSTOMER_FIRST_NAME"]==null?"":dsCustomer.Tables[0].Rows[0]["CUSTOMER_FIRST_NAME"]) +
									" "+
									(dsCustomer.Tables[0].Rows[0]["CUSTOMER_MIDDLE_NAME"]==null?"":dsCustomer.Tables[0].Rows[0]["CUSTOMER_MIDDLE_NAME"])+
									" "+
									(dsCustomer.Tables[0].Rows[0]["CUSTOMER_LAST_NAME"]==null?"":dsCustomer.Tables[0].Rows[0]["CUSTOMER_LAST_NAME"]);

								stateID= dsCustomer.Tables[0].Rows[0]["CUSTOMER_STATE"]==null?"":dsCustomer.Tables[0].Rows[0]["CUSTOMER_STATE"].ToString()  ;
								strAgency_ID=dsCustomer.Tables[0].Rows[0]["AGENCY_ID"]==null?"":dsCustomer.Tables[0].Rows[0]["AGENCY_ID"].ToString() ;//Changed from CUSTOMER_AGENCY_ID to AGENCY_ID by Charles on 24-Aug-09 for APP/POL Optimization
								
								FillCSRDropDown(int.Parse(strAgency_ID));
								FillProducerDropDown(int.Parse(strAgency_ID));
							}//END OF IF PART OF CUSTOMER ACTIVE CHECK		
							else
							{
								//SHOW MESSAGE AND set the flag for exit								
								lblFormLoadMessage.Text= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"339");	
								showDetails=false;
							}//END OF ELSE PART OF CUSTOMER ACTIVE CHECK		
						}//END OF CUSTOMER  NULL CHECK
						#endregion
						
						cmbSTATE_ID.SelectedIndex=cmbSTATE_ID.Items.IndexOf(cmbSTATE_ID.Items.FindByValue(stateID));
						cmbSTATE_ID_SelectedIndexChanged(null,null);
						
						cmbPOLICY_TYPE.SelectedIndex =cmbPOLICY_TYPE.Items.IndexOf(cmbPOLICY_TYPE.Items.FindByValue(hidPOLICY_TYPE.Value));
						cmbAPP_LOB.Visible =true;
						txtLOB.Text				= "";
						rfvCUSTOMER_ID.Enabled	= false;
						 
					}//END OF IF PART OF CALLED FROM CHECK
					else
					{
						hidCustomerID.Value ="NEW";										 
						txtCUSTOMERNAME.Text ="";
						txtLOB.Text ="";
						cmbAPP_LOB.Visible =true;
						
						rfvCUSTOMER_ID.Enabled =true;
					}//END OF ELSE PART OF CALLED FROM CHECK
					hidAppVersionID.Value ="1";
					hidAppID.Value ="NEW";
                    switch ((new cmsbase()).GetLanguageCode())
					{
						case "pt-BR":
							txtAPP_NUMBER.Text  = TEMP_APP_NUMBER_PT;
                            txtAPP_STATUS.Text = DefaultAppStatusPt;
							break;
						case "en-US":
						default:
							txtAPP_NUMBER.Text  = TEMP_APP_NUMBER;
                            txtAPP_STATUS.Text = DefaultAppStatus;
							break;						
					}
					txtAPP_EXPIRATION_DATE.Text = DateTime.Now.ToString("MM/dd/yyyy");
					txtAPP_EXPIRATION_DATE.ReadOnly =true;
                   
					txtAPP_VERSION.Text			= "1.0";
					btnDelete.Visible =false;
					btnCopy.Visible =false;
					btnActivateDeactivate.Visible = false;
					btnVerifyApplication.Visible =false;
					this.btnSubmitAnyway.Visible=false;
					btnSubmitInProgress.Attributes.Add("style","display:none");
					btnSubmitAnywayInProgress.Attributes.Add("style","display:none");
					
					
				}//END OF ELSE PART OF REQUEST CHECK

				if (showDetails)
				{
					trDETAILS.Visible=true; 
					trFORMMESSAGE.Visible =false;
				}//END OF IF PART OF SHOWDETAILS CHECK
				else
				{
					trDETAILS.Visible=false; 
					trFORMMESSAGE.Visible =true;
					lblFormLoadMessage.Visible =true;
				}//END OF ELSE PART OF SHOWDETAILS CHECK
			
				CheckBillingPlan();//Added by Charles on 14-Sep-09 for APP/POL Optimization
				//added by vj on 14-03-2006
				SetMultiPolicy();
				SetCarrierInsureBillAtPageLoad();
				#endregion

				if(btnActivateDeactivate.Visible && btnActivateDeactivate.Text=="") //Added by Charles on 7-Oct-09 for Itrack 6532
				{
                    SetActivateDeactivate((new cmsbase()).GetLanguageCode());
				}
			}//END OF IF PART OF POSTBACK CHECK				
			else
			{
				appTerms=cmbAPP_TERMS.SelectedItem==null?"":cmbAPP_TERMS.SelectedItem.Value ;  
				exp_Date=txtAPP_EXPIRATION_DATE.Text.Trim(); 
				
				FillCSRDropDown(int.Parse(hidAPP_AGENCY_ID.Value=="" ? "0" :hidAPP_AGENCY_ID.Value));//Changed to hidAPP_AGENCY_ID.Value by Charles on 21-Aug-09 for APP/POL Optimizations
				FillProducerDropDown(int.Parse(hidAPP_AGENCY_ID.Value=="" ? "0" :hidAPP_AGENCY_ID.Value));//Changed to hidAPP_AGENCY_ID.Value by Charles on 21-Aug-09 for APP/POL Optimizations
						
				//cmbChooseLang_SelectedIndexChanged(null,null);				
				
			}//END OF ELSE PART OF POSTBACK CHECK				
	
			#region Show/Hide buttons on value of 'ShowQuote'
			/* this will be visible if the application is  verified at least once.
						* SHOW_QUOTE column in APP_LIST */					 
			string showQuote = "";
			if (hidOldData.Value.Trim() != "" && hidOldData.Value.Trim() != "0")
			{
				showQuote = FetchValueFromXML("SHOW_QUOTE",hidOldData.Value);		
			}
			string strSysID = GetSystemId(); 
			if (showQuote.Trim().Equals("1"))// && strSysID.Trim().ToUpper().Equals("W001"))
			{
				btnQuote.Visible=true;//btnQuote.Attributes.Add("Style","display:inline");
				btnConvertAppToPolicy.Visible=true;//btnConvertAppToPolicy.Attributes.Add("Style","display:inline");
			}
			else
			{
				btnQuote.Visible=false;//btnQuote.Attributes.Add("Style","display:none");
				btnConvertAppToPolicy.Visible=false;//btnConvertAppToPolicy.Attributes.Add("Style","display:none");
			}
			#endregion
			//Showing the messages
			ShowMessages();
			
			#region Setting screen id
			switch(strLOB_ID)
			{
				case "1" : // HOME
					base.ScreenId	=	"201_1";
					break;
				case "2" : // Private passenger automobile
					base.ScreenId	=	"201_2";
					break;
				case "3" : // Motorcycle
					base.ScreenId	=	"201_3";
					break;
				case "4" : // Watercraft
					base.ScreenId	=	"201_4";
					break;
				case "5" : // Umbrella
					base.ScreenId	=	"201_5";
					break;
				case "6" : // Rental dwelling
					base.ScreenId	=	"201_6";
					break;
				case "7" : // General liability
					base.ScreenId	=	"201_7";
					break;
				case "8" : // Aviation
					base.ScreenId	=	"201_10";
					break;
				case "" : // Application is added
					cmsbase cb;
					appbase ab;
					ab = (appbase)this;
					cb = (cmsbase)ab;

					cb.ScreenId = "201_0";
					break;
				default : //
					((cmsbase)this).ScreenId = "201_0";
					break;
			}
			myWorkFlow.IsTop	=	false;
			myWorkFlow.Display	=	false;
			#endregion

			#region Setting permissions and class (Read/write/execute/delete)  
			SetButtonsSecurityXml();
			CheckSecurityXml();
			
			#endregion

			if (Request.Form["__EVENTTARGET"] == "cmbAPP_TERMS_Change")
			{
				cmbAPP_TERMS_SelectedIndexChanged(null,null);				
			}
					
		}
		#endregion

		#region CheckSecurityXml
		private void CheckSecurityXml()
		{
			if(hidCustomerID.Value != "" && hidAppID.Value != "" && hidAppID.Value.ToString().Trim().ToUpper() !="NEW")
			{    	
		
				//Checking whether the application in sessoin is active or not
				int intResult=ClsGeneralInformation.CheckForApplicationStatus(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidAppID.Value ==""?"0":hidAppID.Value),int.Parse(hidAppVersionID.Value==""?"0":hidAppVersionID.Value));
				
				//Policy exists for this particulat application, hence changing the security xml to view mode only
				int intResultcheck=ClsGeneralInformation.CheckForConvertedVersion(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidAppID.Value ==""?"0":hidAppID.Value),int.Parse(hidAppVersionID.Value==""?"0":hidAppVersionID.Value));

				if(intResultcheck == 1 )
				{
					//its one of the version Has Been Converted To Policy, hence changing the security xml to read only mode
					btnDelete.PermissionString ="<Security><Read>Y</Read><Write>N</Write><Delete>Y</Delete><Execute>N</Execute></Security>";
					btnDelete.Visible =false;
					btnCopy.Visible =false;
					btnActivateDeactivate.Visible = false;
					btnVerifyApplication.Visible =false;
					this.btnSubmitAnyway.Visible=false;
					btnSubmitInProgress.Attributes.Add("style","display:none");
					btnSubmitAnywayInProgress.Attributes.Add("style","display:none");
					btnSave.Visible=false;
					//btnActivateDeactivate.Visible=true;
				} 
				else if (intResult == 2 && hidFormSaved.Value != "1" )
				{
					//Application is not active
					btnDelete.Visible =false;
					btnCopy.Visible =false;
					this.btnSubmitAnyway.Visible=false;
					btnSubmitInProgress.Attributes.Add("style","display:none");
					btnSubmitAnywayInProgress.Attributes.Add("style","display:none");
					btnQuote.Visible=false;
					btnSave.PermissionString ="<Security><Read>Y</Read><Write>Y</Write><Delete>N</Delete><Execute>N</Execute></Security>";
					btnActivateDeactivate.PermissionString ="<Security><Read>Y</Read><Write>Y</Write><Delete>N</Delete><Execute>N</Execute></Security>";
					//btnSubmitInprogress.PermissionString = "<Security><Read>Y</Read><Write>Y</Write><Delete>N</Delete><Execute>N</Execute></Security>";
					//btnSubmitInprogress.PermissionString = gstrSecurityXML;
				}
				else
				{
					btnDelete.Visible =true;
					btnCopy.Visible =true;
					this.btnSubmitAnyway.Visible=true;
					btnQuote.Visible=true;
				}
			}
		}	

		#endregion
        
		#region Get Policy Details
		
		private void GetPolicyDetails()
		{

			string policyId = "", policyVersionID = "";

			ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();
			DataSet ds = objGeneralInformation.GetPolicyDetails(int.Parse(hidCustomerID.Value)
				, int.Parse(hidAppID.Value), int.Parse(hidAppVersionID.Value));
			
			if (ds.Tables[0].Rows.Count > 0)
			{
				hidPOLICY_ID.Value = ds.Tables[0].Rows[0]["POLICY_ID"].ToString();
				hidPolicyNumber.Value	=	ds.Tables[0].Rows[0]["POLICY_NUMBER"].ToString();
				hidPOLICY_VERSION_ID.Value=ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString();
				policyId = GetPolicyID();
				policyVersionID = GetPolicyVersionID();
				if (policyId == "")
				{
					SetPolicyID(hidPOLICY_ID.Value);
				}
				else
				{
					if(hidPOLICY_ID.Value!=policyId)
					{
						SetPolicyID(hidPOLICY_ID.Value); 											
						policyVersionID=hidPOLICY_VERSION_ID.Value;
					}
				}
				if (policyVersionID == "")
				{	
					SetPolicyVersionID(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());
				}
				else
				{
					SetPolicyVersionID(policyVersionID);
				}
				if (hidOldData.Value != null && hidOldData.Value != "0" && hidOldData.Value != "")
				{
					SetLOBID(FetchValueFromXML("LOB_ID",hidOldData.Value));
				}
			}
			objGeneralInformation = null;
		}

		private void GetPolicyDetails(int iCustomerId, int iAppId, int iAppVersionId,out int Policy_Id, out int Policy_Version_Id, out string Policy_Number)
		{

			string polId = "", polVerId = "";
			Policy_Id=Policy_Version_Id=0;
			Policy_Number="";

			ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();
			DataSet ds = objGeneralInformation.GetPolicyDetails(iCustomerId,iAppId,iAppVersionId);
			
			if (ds.Tables[0].Rows.Count > 0)
			{
				if(ds.Tables[0].Rows[0]["POLICY_ID"]!=null && ds.Tables[0].Rows[0]["POLICY_ID"].ToString()!="")
					Policy_Id = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());

				if(ds.Tables[0].Rows[0]["POLICY_NUMBER"]!=null && ds.Tables[0].Rows[0]["POLICY_NUMBER"].ToString()!="")
					Policy_Number	=	ds.Tables[0].Rows[0]["POLICY_NUMBER"].ToString();
				
				if(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"]!=null && ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString()!="")
					Policy_Version_Id = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());
				//<end> <002> 
				//Setting the polic details in session
				polId = GetPolicyID();
				polVerId = GetPolicyVersionID();
				if (polId == "")
				{
					SetPolicyID(Policy_Id.ToString());
				}
				else
				{
					if(Policy_Id.ToString()!=polId)
					{
						SetPolicyID(Policy_Id.ToString());									
						polVerId=Policy_Version_Id.ToString();
					}
				}
				if (polVerId == "")
				{	
					SetPolicyVersionID(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());
				}
				else
				{
					SetPolicyVersionID(polVerId);
				}
				//SetLOBID(FetchValueFromXML("LOB_ID",hidOldData.Value));
				SetLOBID(ds.Tables[0].Rows[0]["LOB_ID"].ToString());				
			}

			objGeneralInformation = null;
		}

		#endregion 

		#region Utility Functions ( Button Security / Show Messages / Application Cookies / GetFormValue) etc..

		private void SetButtonsSecurityXml()
		{
			btnReset.CmsButtonClass						=	CmsButtonType.Write;
			btnReset.PermissionString					=	gstrSecurityXML;

			btnBack.CmsButtonClass						=	CmsButtonType.Read ;
			btnBack.PermissionString					=	gstrSecurityXML;

			btnCustomerAssistant.CmsButtonClass			=	CmsButtonType.Read ;
			btnCustomerAssistant.PermissionString		=	gstrSecurityXML;
			
			btnSave.CmsButtonClass						=	CmsButtonType.Write;
			btnSave.PermissionString					=	gstrSecurityXML;

			btnCopy.CmsButtonClass						=	CmsButtonType.Write;
			btnCopy.PermissionString					=	gstrSecurityXML;
			
			btnQuote.CmsButtonClass						=	CmsButtonType.Write;
			btnQuote.PermissionString					=	gstrSecurityXML;

			btnVerifyApplication.CmsButtonClass			=  CmsButtonType.Write;
			btnVerifyApplication.PermissionString		=  gstrSecurityXML;
			
			btnDelete.CmsButtonClass					=	CmsButtonType.Delete;
			btnDelete.PermissionString					=	gstrSecurityXML;

			btnConvertAppToPolicy.CmsButtonClass		=	CmsButtonType.Write;
			btnConvertAppToPolicy.PermissionString		=	gstrSecurityXML;
			
			btnSubmitAnyway.CmsButtonClass				=	CmsButtonType.Write;
			btnSubmitAnyway.PermissionString			=	gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass		= CmsButtonType.Write;
			btnActivateDeactivate.PermissionString		= gstrSecurityXML;

			btnSubmitInProgress.CmsButtonClass			= CmsButtonType.Write;
			btnSubmitInProgress.PermissionString		= gstrSecurityXML;

			btnSubmitAnywayInProgress.CmsButtonClass	= CmsButtonType.Write;
			btnSubmitAnywayInProgress.PermissionString	= gstrSecurityXML;			
		}

		private void ShowMessages()
		{
			if (Request.Params["LoadedAfterSave"] != null)
			{
				if(Request.Params["LoadedAfterSave"].ToString().ToUpper() == "TRUE")
				{
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "29");
					lblMessage.Visible = true;
				}
				else if(Request.Params["LoadedAfterSave"].ToString().ToUpper() == "FALSE")
				{
					lblFormLoadMessage.Text = ClsMessages.GetMessage(base.ScreenId, "406");
					lblFormLoadMessage.Visible = true;					
					hidFormSaved.Value			=	"5";		//If record deleted, fidFormSaved should be 5
					lblFormLoadMessage.Visible	=	true;	
					trDETAILS.Visible			=	false; 
					btnDelete.Visible           =   false;
					delStr						=	"1";
					trFORMMESSAGE.Visible		=	true;
					SetAppID("");
					SetAppVersionID("");
				}
			}
		}

		private void SetApplicationCookies(string appNo,string custID,string appID,string appVersionID)
		{
			# region last 3 Entries
			string AgencyId = GetSystemId();
			if(AgencyId.ToUpper() != CarrierSystemID)
			{
				//Done to Remove Cookies and fetch values from database
				Cms.BusinessLayer.BlApplication.ClsGeneralInformation objLastPageVisited = new ClsGeneralInformation();
				DataSet ds = objLastPageVisited.GetLastVisitedPageEntry(int.Parse(GetUserId()),AgencyId);

				//if(System.Web.HttpContext.Current.Request.Cookies["appNo" + GetSystemId() + "_" + GetUserId() + "_1"] != null)
				if(ds!=null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0]!=null && ds.Tables[0].Rows[0]["LAST_VISITED_APPLICATION"] !=null && ds.Tables[0].Rows[0]["LAST_VISITED_APPLICATION"].ToString()!="")
				{
					//string prevCook = System.Web.HttpContext.Current.Request.Cookies["appNo" + GetSystemId() + "_" + GetUserId() + "_1"].Value;
					string prevCook = ds.Tables[0].Rows[0]["LAST_VISITED_APPLICATION"].ToString();
					string [] cookArr = prevCook.Split('@');
					if(cookArr.Length > 0 && cookArr.Length < 4)
					{
						//System.Web.HttpContext.Current.Response.Cookies["appNo" + GetSystemId() + "_" + GetUserId() + "_1"].Value = appNo + "~" + custID + "~" + appID + "~" + appVersionID + "~" + DateTime.Today.Date + "@" + System.Web.HttpContext.Current.Request.Cookies["appNo" + GetSystemId() + "_" + GetUserId() + "_1"].Value;
						string App_Details=appNo + "~" + custID + "~" + appID + "~" + appVersionID + "~" + DateTime.Today.Date + "@" + ds.Tables[0].Rows[0]["LAST_VISITED_APPLICATION"].ToString();
						Cms.BusinessLayer.BlApplication.ClsGeneralInformation objLastVisitedApp= new ClsGeneralInformation(); 
						objLastVisitedApp.UpdateLastVisitedPageEntry("Application",App_Details,int.Parse(GetUserId()),AgencyId);
					}
					else if(cookArr.Length >= 4)
					{
						int maxindex = cookArr.Length-1;
						if(maxindex >= 3)
							maxindex = 2;

						//System.Web.HttpContext.Current.Response.Cookies["appNo" + GetSystemId() + "_" + GetUserId() + "_1"].Value = appNo + "~" + custID + "~" + appID + "~" + appVersionID + "~" + DateTime.Today.Date + "@";
						string App_Details=appNo + "~" + custID + "~" + appID + "~" + appVersionID + "~" + DateTime.Today.Date + "@" + ds.Tables[0].Rows[0]["LAST_VISITED_APPLICATION"].ToString();
						Cms.BusinessLayer.BlApplication.ClsGeneralInformation objLastVisitedApp= new ClsGeneralInformation(); 
						objLastVisitedApp.UpdateLastVisitedPageEntry("Application",App_Details,int.Parse(GetUserId()),AgencyId);
						for(int cookindex = 0; cookindex < maxindex; cookindex++)
						{
							//System.Web.HttpContext.Current.Response.Cookies["appNo" + GetSystemId() + "_" + GetUserId() + "_1"].Value += cookArr[cookindex] + "@";
							App_Details += cookArr[cookindex] + "@";
						}
						objLastVisitedApp.UpdateLastVisitedPageEntry("Application",App_Details,int.Parse(GetUserId()),AgencyId);
					}
					else
					{
						//System.Web.HttpContext.Current.Response.Cookies["appNo" + GetSystemId() + "_" + GetUserId() + "_1"].Value=appNo + "~" + custID + "~" + appID + "~" + appVersionID + "~" + DateTime.Today.Date + "@";
						string App_Details=appNo + "~" + custID + "~" + appID + "~" + appVersionID + "~" + DateTime.Today.Date + "@" ;
						Cms.BusinessLayer.BlApplication.ClsGeneralInformation objLastVisitedApp= new ClsGeneralInformation(); 
						objLastVisitedApp.UpdateLastVisitedPageEntry("Application",App_Details,int.Parse(GetUserId()),AgencyId);
					}
					//System.Web.HttpContext.Current.Response.Cookies["appNo" + GetSystemId() + "_" + GetUserId() + "_1"].Expires=DateTime.MaxValue;
				}
				else
				{
					//System.Web.HttpContext.Current.Response.Cookies["appNo" + GetSystemId() + "_" + GetUserId() + "_1"].Value=appNo + "~" + custID + "~" + appID + "~" + appVersionID + "~" + DateTime.Today.Date + "@";
					//System.Web.HttpContext.Current.Response.Cookies["appNo" + GetSystemId() + "_" + GetUserId() + "_1"].Expires=DateTime.MaxValue;
					string App_Details=appNo + "~" + custID + "~" + appID + "~" + appVersionID + "~" + DateTime.Today.Date + "@";
					Cms.BusinessLayer.BlApplication.ClsGeneralInformation objLastVisitedApp= new ClsGeneralInformation(); 
					objLastVisitedApp.UpdateLastVisitedPageEntry("Application",App_Details,int.Parse(GetUserId()),AgencyId);
				}
			}
				#endregion
			else
			{
				string App_Details=appNo + "~" + custID + "~" + appID + "~" + appVersionID + "~" + DateTime.Today.Date;
				Cms.BusinessLayer.BlApplication.ClsGeneralInformation objLastVisitedApp= new ClsGeneralInformation(); 
				objLastVisitedApp.UpdateLastVisitedPageEntry("Application",App_Details,int.Parse(GetUserId()),AgencyId);
			}
			
		}
		
		private void SetCaptions()
		{
			capCUSTOMER_ID.Text						=		objResourceMgr.GetString("txtCUSTOMER_ID");
			capAPP_STATUS.Text						=		objResourceMgr.GetString("txtAPP_STATUS");
			capAPP_NUMBER.Text						=		objResourceMgr.GetString("txtAPP_NUMBER");
			capAPP_VERSION.Text						=		objResourceMgr.GetString("txtAPP_VERSION");
			capAPP_TERMS.Text						=		objResourceMgr.GetString("cmbAPP_TERMS");
			capAPP_INCEPTION_DATE.Text				=		objResourceMgr.GetString("txtAPP_INCEPTION_DATE");
			capAPP_EFFECTIVE_DATE.Text				=		objResourceMgr.GetString("txtAPP_EFFECTIVE_DATE");
			capAPP_EXPIRATION_DATE.Text				=		objResourceMgr.GetString("txtAPP_EXPIRATION_DATE");
			capAPP_LOB.Text							=		objResourceMgr.GetString("cmbAPP_LOB");
			capAPP_SUBLOB.Text						=		objResourceMgr.GetString("cmbAPP_SUBLOB");
			capAPP_AGENCY_ID.Text					=		objResourceMgr.GetString("cmbAPP_AGENCY_ID");
			capCSR.Text								=		objResourceMgr.GetString("cmbCSR");
			capProducer.Text						=		objResourceMgr.GetString("cmbProducer");
			capSTATE_ID.Text						=		objResourceMgr.GetString("cmbSTATE_ID");
			capUNDERWRITER.Text						=		objResourceMgr.GetString("cmbUNDERWRITER");

			//billing information	
			capINSTALL_PLAN_ID.Text					=		objResourceMgr.GetString("cmbINSTALL_PLAN_ID");
			capBILL_TYPE_ID.Text					=		objResourceMgr.GetString("cmbBILL_TYPE_ID");
			capCOMPLETE_APP.Text					=		objResourceMgr.GetString("chkCOMPLETE_APP");
			capPROPRTY_INSP_CREDIT.Text				=		objResourceMgr.GetString("cmbPROPRTY_INSP_CREDIT");
			capCHARGE_OFF_PRMIUM.Text				=		objResourceMgr.GetString("cmbCHARGE_OFF_PRMIUM");
			capRECEIVED_PRMIUM.Text					=		objResourceMgr.GetString("txtRECEIVED_PRMIUM");
			capPROXY_SIGN_OBTAINED.Text				=		objResourceMgr.GetString("cmbPROXY_SIGN_OBTAINED");
			capYEAR_AT_CURR_RESI.Text				=		objResourceMgr.GetString("txtYEAR_AT_CURR_RESI");
			capYEARS_AT_PREV_ADD.Text				=		objResourceMgr.GetString("txtYEARS_AT_PREV_ADD");
			capPIC_OF_LOC.Text						=		objResourceMgr.GetString("cmbPIC_OF_LOC");
			capDOWN_PAY_MODE.Text					=		objResourceMgr.GetString("txtDOWN_PAY_MODE");	
			
			//Other Captions
			lblManHeader.Text	= objResourceMgr.GetString("lblManHeader");
			lblChooseLang.Text  = objResourceMgr.GetString("lblChooseLang");
			lblAppHeader.Text	= objResourceMgr.GetString("lblAppHeader");
			lblTermHeader.Text	= objResourceMgr.GetString("lblTermHeader");
			lblAgencyHeader.Text = objResourceMgr.GetString("lblAgencyHeader");
			lblBillingHeader.Text = objResourceMgr.GetString("lblBillingHeader");
			lblAllPoliciesHeader.Text = objResourceMgr.GetString("lblAllPoliciesHeader");
			lblPolicies.Text = objResourceMgr.GetString("lblPolicies");
			lblPoliciesDiscount.Text = objResourceMgr.GetString("lblPoliciesDiscount");
			capAPP_POLICY_TYPE.Text = objResourceMgr.GetString("capAPP_POLICY_TYPE");
		}
		private void FillControls()
		{ 
			hidFULL_PAY_PLAN_ID.Value = Cms.BusinessLayer.BlCommon.Accounting.ClsInstallmentPlan.FullPayPlanID().ToString();
			cmbPROXY_SIGN_OBTAINED.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("yesno");
			cmbPROXY_SIGN_OBTAINED.DataTextField="LookupDesc";
			cmbPROXY_SIGN_OBTAINED.DataValueField="LookupID";
			cmbPROXY_SIGN_OBTAINED.DataBind();
			cmbPROXY_SIGN_OBTAINED.Items.Insert(0,"");

			cmbCHARGE_OFF_PRMIUM.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbCHARGE_OFF_PRMIUM.DataTextField="LookupDesc";
			cmbCHARGE_OFF_PRMIUM.DataValueField="LookupID";
			cmbCHARGE_OFF_PRMIUM.DataBind();
			cmbCHARGE_OFF_PRMIUM.Items.Insert(0,"");
			cmbCHARGE_OFF_PRMIUM.SelectedIndex=1;

			cmbPIC_OF_LOC.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbPIC_OF_LOC.DataTextField="LookupDesc";
			cmbPIC_OF_LOC.DataValueField="LookupID";
			cmbPIC_OF_LOC.DataBind();
			cmbPIC_OF_LOC.Items.Insert(0,"");
			//LOBs
			DataTable dtLOBs = Cms.CmsWeb.ClsFetcher.LOBs;
			cmbAPP_LOB.DataSource			= dtLOBs;
			cmbAPP_LOB.DataTextField		= "LOB_DESC";
			cmbAPP_LOB.DataValueField		= "LOB_ID";
			cmbAPP_LOB.DataBind();
			
			cmbAPP_LOB.Items.Insert(0,new ListItem("",""));
			//state
			DataTable dtState = Cms.CmsWeb.ClsFetcher.ActiveState ;
			cmbSTATE_ID.DataSource			= dtState;
			cmbSTATE_ID.DataTextField		= "STATE_NAME";
			cmbSTATE_ID.DataValueField		= "STATE_ID";
			cmbSTATE_ID.DataBind();
			
			cmbSTATE_ID.Items.Insert(0,new ListItem("",""));
			cmbSTATE_ID.SelectedIndex=0;

			cmbPROPRTY_INSP_CREDIT.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbPROPRTY_INSP_CREDIT.DataTextField="LookupDesc";
			cmbPROPRTY_INSP_CREDIT.DataValueField="LookupID";
			cmbPROPRTY_INSP_CREDIT.DataBind();
			cmbPROPRTY_INSP_CREDIT.Items.Insert(0,"");
			
			string EffectiveDate;
			if(txtAPP_EFFECTIVE_DATE.Text!="")
			{
				EffectiveDate=txtAPP_EFFECTIVE_DATE.Text;
			}
			else
			{
				EffectiveDate="01/01/1950";
			}
			//Cms.BusinessLayer.BlCommon.ClsAgency.GetAgencyWithStatusInDropDown(cmbAPP_AGENCY_ID,EffectiveDate); //Commented by Charles on 21-Aug-09 for APP/POL Optimization 
			//Added by Charles on 21-Aug-09 for APP/POL OPTIMISATION							
			DataTable objDataTable=Cms.BusinessLayer.BlCommon.ClsAgency.GetAgencyWithStatus(EffectiveDate,gIntCUSTOMER_ID).Tables[0];
			if(objDataTable.Rows.Count>0)
			{
				lblAGENCY_DISPLAY_NAME.Text=objDataTable.Rows[0]["AGENCY_NAME_ACTIVE_STATUS"].ToString();
				hidIsTerminated.Value = objDataTable.Rows[0]["IS_TERMINATED"].ToString();	
				hidAPP_AGENCY_ID.Value  = objDataTable.Rows[0]["AGENCY_ID"].ToString();
				//Setting Agency Color
				if(hidIsTerminated.Value == "Y")
				{
					lblAGENCY_DISPLAY_NAME.BackColor = Color.Lavender;
					lblAGENCY_DISPLAY_NAME.ForeColor = Color.Red;
				}					
			}
			objDataTable.Dispose(); //Added till here
			
		}

		private void FillCSRDropDown(int AgencyID)
		{		
			try
			{
				ClsUser objUser = new ClsUser();
                DataTable dtCSRProducers = null;//objUser.GetCSRProducers(AgencyID);
				
				cmbCSR.Items.Clear();
				if(dtCSRProducers!=null && dtCSRProducers.Rows.Count>0)
				{
					cmbCSR.DataSource			= new DataView(dtCSRProducers);
					cmbCSR.DataTextField		= "USERNAME";
					cmbCSR.DataValueField		= "USER_ID";					
					cmbCSR.DataBind();
					for (int i =0;i < cmbCSR.Items.Count ;i++ )
					{
						string arrIsActiveStatus = dtCSRProducers.Rows[i]["IS_ACTIVE"].ToString();
						if(arrIsActiveStatus.Equals("N"))
							cmbCSR.Items[i].Attributes.Add("style", "color:red");
					}
				}
				cmbCSR.Items.Insert(0,new ListItem("",""));
				if(hidOldData.Value == "" || hidOldData.Value==null || hidOldData.Value=="0")
				{
					cmbCSR.Items[0].Value="-1"; 
				}
				string agency_name = GetSystemId();
                //Changed by Charles on 19-May-10 for Itrack 51
                string strCarrierSystemID = CarrierSystemID;// System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID").ToString();
				if(agency_name.ToUpper() != strCarrierSystemID.Trim().ToUpper())
				{
					ListItem li=cmbCSR.Items.FindByValue(GetUserId());
					if(li!=null)
					{
						li.Selected=true;
						hidCSR.Value = li.Value;
					}
				}
			}
			catch(Exception exc)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(exc);
			}
			finally
			{}
		}

		private void FillProducerDropDown(int AgencyID)
		{		
			try
			{
				//fill Producer dropdown
				//DataTable dtAgencyUsers = ClsUser.GetAgencyUsers(AgencyID);
				ClsUser objUser = new ClsUser();
				DataTable dtProducers = objUser.GetProducers(AgencyID);
				cmbProducer.Items.Clear();
				if(dtProducers!=null && dtProducers.Rows.Count>0)
				{
					cmbProducer.DataSource			= new DataView(dtProducers);
					cmbProducer.DataTextField		= "USERNAME";
					cmbProducer.DataValueField		= "USER_ID";					
					cmbProducer.DataBind();
					for (int i =0;i < cmbProducer.Items.Count ;i++ )
					{
						string arrIsActiveStatus = dtProducers.Rows[i]["IS_ACTIVE"].ToString();
						if(arrIsActiveStatus.Equals("N"))
							cmbProducer.Items[i].Attributes.Add("style", "color:red");
					}
				}
				cmbProducer.Items.Insert(0,new ListItem("",""));
				cmbProducer.SelectedIndex=0;
			}
			catch(Exception exc)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(exc);
			}
			finally
			{}
		}
		

		#region Validation Checks
		/// <summary>
		/// validate posted data from form
		/// </summary>
		/// <returns>True if posted data is valid else false</returns>
		private bool doValidationCheck()
		{
			try
			{
				return true;
			}
			catch
			{
				return false;
			}
		}
		
		# endregion
		/// <summary>
		/// fetch the active billing plan XML 
		/// </summary>
		
		private void CheckBillingPlan()
		{
			try
			{
				DataSet ds=new DataSet(); //Moved outside if/else by Charles on 14-Sep-09 for APP/POL Optimization
				if(Request.QueryString["APP_ID"].ToString()!="")
				{
					if (Request.Form["__EVENTTARGET"] == "cmbAPP_TERMS_Change")//Added by Charles on 14-Sep-09 for APP/POL Optimization
					{						
						ds=ClsInstallmentInfo.GetApplicableInstallmentPlans(int.Parse(cmbAPP_TERMS.SelectedValue));
					}						
					else 
					{
						int intPolicyTerm=int.Parse(FetchValueFromXML("APP_TERMS",hidOldData.Value)); //Added till here
						ds = ClsInstallmentInfo.GetApplicableInstallmentPlans(intPolicyTerm,gIntCUSTOMER_ID,int.Parse(Request.QueryString["APP_ID"].ToString()),int.Parse(Request.QueryString["APP_VERSION_ID"].ToString()),"APP");//intPolicyTerm added by Charles on 14-Sep-09 for APP/POL Optimization
					}
					hidBillingPlan.Value	=	ds.GetXml();
					
					//Get Deactive Plan ID PK.
					string PlanXML = hidBillingPlan.Value.ToString();
					XmlDocument doc = new XmlDocument();
					PlanXML =PlanXML.Replace("&AMP;","&amp;");
					PlanXML =PlanXML.Replace("\r\n","");
					doc.LoadXml(PlanXML);
					if(doc!=null)
					{
						foreach(XmlNode node in doc.SelectNodes("NewDataSet/Table"))
						{
							if(node!=null)
							{
								if(node.SelectSingleNode("IS_ACTIVE").InnerText == "N")
									hidDEACTIVE_INSTALL_PLAN_ID.Value =  node.FirstChild.InnerText.ToString();   
							}
						}
						int count =   doc.SelectNodes("NewDataSet/Table/IS_ACTIVE[contains(.,'N')]").Count;
						if(count==0)
							hidDEACTIVE_INSTALL_PLAN_ID.Value = "";
					}
				}
				else
				{//new app
					if(cmbAPP_TERMS.SelectedValue=="") //If/Else added by Charles on 14-Sep-09 for APP/POL Optimization
						ds = ClsInstallmentInfo.GetApplicableInstallmentPlans();
					else
						ds=ClsInstallmentInfo.GetApplicableInstallmentPlans(int.Parse(cmbAPP_TERMS.SelectedValue));//Added by Charles on 14-Sep-09 for APP/POL Optimization

					hidBillingPlan.Value	=	ds.GetXml();
				}			
				cmbINSTALL_PLAN_ID.Items.Clear();//Added by Charles on 14-Sep-09 for APP/POL Optimization
				cmbINSTALL_PLAN_ID.DataSource =ds;
				cmbINSTALL_PLAN_ID.DataTextField = "BILLING_PLAN";			
				cmbINSTALL_PLAN_ID.DataValueField = "INSTALL_PLAN_ID"; 
				cmbINSTALL_PLAN_ID.DataBind();
				cmbINSTALL_PLAN_ID.Items.Insert(0,""); //Added till here
				
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
		//Added to set Insured Bill for Carrier same as Agency {W001 in our case}
		// 14-Feb Mohit Agarwal/Swarup Pal
		private void setCarrierInsuredBill()
		{
			cmbBILL_TYPE_ID.Enabled = true;
            //Changed by Charles on 19-May-10 for Itrack 51
            string strCarrierSystemID = CarrierSystemID;// System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID").ToString();
			string strAGENCY_ID=hidAPP_AGENCY_ID.Value.ToString(); //cmbAPP_AGENCY_ID.Items[cmbAPP_AGENCY_ID.SelectedIndex].Text.ToUpper(); //Changed by Charles on 21-Aug-09 for APP/POL Optimization			
			if(strAGENCY_ID.Equals("27"))//Added by Charles on 24-Aug-09 for APP/POL Optimization 
			{
				for(int index =0;index < cmbBILL_TYPE_ID.Items.Count;index++)
				{	
					if(cmbBILL_TYPE_ID.Items[index].Value == "8460" || cmbBILL_TYPE_ID.Items[index].Value == "11150")
					{
						cmbBILL_TYPE_ID.SelectedIndex = index;
						cmbBILL_TYPE_ID.Enabled = false;
						break;
					}
				}
			}
		}

		private void SetCarrierInsureBillAtPageLoad()
		{
			if (hidOldData.Value != null && hidOldData.Value !="" && hidOldData.Value !="0")
			{
				System.Xml.XmlDocument objXMLDoc = new XmlDocument();
				objXMLDoc.LoadXml(hidOldData.Value);
				string strAgency = ClsCommon.GetNodeValue(objXMLDoc,"//APP_AGENCY_ID");
				
				if(strAgency.Equals("27")) // W001-Wolverine = 27
				{
					cmbBILL_TYPE_ID.Enabled = false;
					return;
				}
			}
			else
			{
				//cmbBILL_TYPE_ID.Enabled = true;
				setCarrierInsuredBill();
			}
		}
		
		private bool ValidInputXML(string inputXML)
		{
			try
			{
				bool retVal=true;
				XmlDocument tempDoc=new XmlDocument();
				if((!inputXML.StartsWith("<ERROR") )&& (inputXML.Trim() != "<INPUTXML></INPUTXML>"))
				{
					tempDoc.LoadXml("<INPUTXML>" + inputXML + "</INPUTXML>");
					XmlElement tempElement = tempDoc.DocumentElement;
					XmlNodeList tempNodes= tempElement.ChildNodes;
					foreach(XmlNode nodTempNode in tempNodes)
					{
						foreach (XmlNode nodTempChild in nodTempNode.ChildNodes)
						{
							if(nodTempChild.InnerText.Trim()=="" || nodTempChild.InnerText.Trim()=="NULL")
							{
								retVal =false;
								break;
							}
						}
					}
				}
				else
				{
					retVal =false;
				}
				return retVal;
			}
			catch(Exception exc)
			{
				throw(exc);
			}
			finally
			{
			
			}
		}

		public bool StartNBSProcess(int iCustomerId, int iAppId, int iAppVersionId, int iPolId, int iPolVersionId, string strCalledFrom, out string strHTML, string strLOB_ID)
		{
			DataSet ds = new DataSet();
			strHTML = "";
			ClsRatingAndUnderwritingRules objVerifyRules = new ClsRatingAndUnderwritingRules(CarrierSystemID);
			ds=objVerifyRules.OldInputXML(iCustomerId,iAppId,iAppVersionId);
			
			string strReturn="";
			
			if(ds.Tables[0].Rows.Count>0)
			{
				strReturn=ds.Tables[0].Rows[0]["APP_VERIFICATION_XML"].ToString(); 
			}							
			
			XmlDocument xmlDocOutput = new XmlDocument();
			strReturn= strReturn.Replace("\t","");
			strReturn= strReturn.Replace("\r\n","");					
			strReturn= strReturn.Replace( "<LINK" ,"<!-- <LINK");				
			strReturn= strReturn.Replace( " rel=\"stylesheet\"> ","rel=\"stylesheet/\"> -->");
			strReturn= strReturn.Replace("<META http-equiv=\"Content-Type\" content=\"text/html; charset=utf-16\">","");					
			xmlDocOutput.LoadXml("<RULEHTML>" +  strReturn + "</RULEHTML>");  
			
			XmlNodeList nodLst= xmlDocOutput.GetElementsByTagName("verifyStatus");
			
			// 0  All ok 
			// 1 -- Not ok
			string strAppStatus="";
			if(nodLst.Count>0)
			{
				strAppStatus=nodLst.Item(0).InnerText; //1 or 0
			}
			
			//if '0' New business process is launched and completed.
			bool retval=false,valid=false;
			
			if (strAppStatus.Trim().Equals("0"))
			{
				Cms.Model.Policy.Process.ClsProcessInfo  objProcessInfo = new Cms.Model.Policy.Process.ClsProcessInfo();
				Cms.BusinessLayer.BlProcess.ClsNewBusinessProcess objProcess = new Cms.BusinessLayer.BlProcess.ClsNewBusinessProcess();
				objProcessInfo.CUSTOMER_ID = iCustomerId;	
				objProcessInfo.POLICY_ID = iPolId;
				objProcessInfo.NEW_POLICY_ID = iPolId;
				objProcessInfo.POLICY_VERSION_ID = iPolVersionId;
				objProcessInfo.NEW_POLICY_VERSION_ID = iPolVersionId;
				objProcessInfo.CREATED_BY = int.Parse(GetUserId());
				objProcessInfo.CREATED_DATETIME = DateTime.Now;
				objProcessInfo.LOB_ID = int.Parse(strLOB_ID);
				//Default values given for printing of documents so that entry should go to print jobs table				
				objProcessInfo.INSURED = (int)clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL;
				objProcessInfo.AGENCY_PRINT = (int)clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL;
				objProcessInfo.ADD_INT = (int)clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL;
				objProcessInfo.AUTO_ID_CARD = (int)Cms.BusinessLayer.BlProcess.clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL;						
				if(objProcessInfo.LOB_ID == (int)enumLOB.AUTOP || objProcessInfo.LOB_ID == (int)enumLOB.CYCL)
					objProcessInfo.NO_COPIES = Cms.BusinessLayer.BlCommon.ClsCommon.PRINT_OPTIONS_AUTO_CYCL_NO_OF_COPIES;

				retval = objProcess.StartProcess(objProcessInfo);
				if (retval == true)
				{
					// Check policy mandatory infos if all ok then commit process
					Cms.BusinessLayer.BlApplication.ClsRatingAndUnderwritingRules objRules = new Cms.BusinessLayer.BlApplication.ClsRatingAndUnderwritingRules(CarrierSystemID);
					string strRulesStatus="0";
					strHTML=objRules.VerifyPolicy(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID ,objProcessInfo.POLICY_VERSION_ID,GetLOBID(),out  valid,GetColorScheme(),out strRulesStatus,"APP");	
					if(valid)
					{
						objProcessInfo.COMPLETED_BY = int.Parse(GetUserId());
						objProcessInfo.COMPLETED_DATETIME = DateTime.Now;
						retval = objProcess.CommitProcess(objProcessInfo);
					}
					else
					{
						if(strCalledFrom==CALLED_FROM_GEN_INFO)
						{
							ClientScript.RegisterHiddenField("hidPOLHTML",strHTML);
							ClientScript.RegisterStartupScript(this.GetType(),"ShowVerifiyDialog","<script>ShowPolicyMsg();</script>");
						}						
					}
				}		
			}

			if(strCalledFrom==CALLED_FROM_GEN_INFO)
				return valid;
			else
			{
				if(retval==true && valid== true)
				{
					// send 5 for policy created sucesfully and accepted.
					return true;
				}	
				else
				{
					return false;
				}
			}
		}

		private void FetchUndisclosedDrivers()
		{
			int nCount =0; 			
			DataSet objDSDriver;
			DataSet objDSPoilcy;			
			string strPoilcyID="";
			string strPoilcyVerNo="";			
			string strStateID="";
    
			ClsGeneralInformation objGenInfo = new ClsGeneralInformation();
			Cms.CmsWeb.Utils.Utility objMVRUtil = new Cms.CmsWeb.Utils.Utility();
			int userID =int.Parse(GetUserId());
			gstrLobID= hidLOBID.Value;
			strStateID= cmbSTATE_ID.SelectedValue ; 


			gIntCUSTOMER_ID		= int.Parse(Request["CUSTOMER_ID"] == null ? "0" : Request["CUSTOMER_ID"].ToString());
			gIntAPP_ID 			=	int.Parse(Request["APP_ID"] == null || Request["APP_ID"] =="" ? FetchValueFromXML("APP_ID",hidOldData.Value) : Request["APP_ID"].ToString());				
			gIntAPP_VERSION_ID 	=	int.Parse(Request["APP_VERSION_ID"] == null || Request["APP_VERSION_ID"] ==""  ?  FetchValueFromXML("APP_VERSION_ID",hidOldData.Value) : Request["APP_VERSION_ID"].ToString());
			
			//getting info about polcy and poilcy ver
			objDSPoilcy = objGenInfo.GetPoilcyInfo(gIntCUSTOMER_ID,gIntAPP_ID,gIntAPP_VERSION_ID);
			if(objDSPoilcy.Tables[0].Rows.Count > 0)
			{
				strPoilcyID=objDSPoilcy.Tables[0].Rows[0]["POLICY_ID"].ToString();
				strPoilcyVerNo=objDSPoilcy.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString();
			}
			//getting driver or operator list on the basis of lob and application 
			objDSDriver = objGenInfo.GetDriverList(gIntCUSTOMER_ID,gIntAPP_ID,gIntAPP_VERSION_ID,gstrLobID);
			
			ClsDriverDetailsInfo[] objDrivers = new ClsDriverDetailsInfo[objDSDriver.Tables[0].Rows.Count];

			// for each driver or operator getting a list of violation from iix web service
			for (nCount =0; nCount <= objDSDriver.Tables[0].Rows.Count -1 ; nCount++)
			{
				string strDriverXml = ClsDriverDetail.GetAppDriverDetailsXML(gIntCUSTOMER_ID, gIntAPP_ID, gIntAPP_VERSION_ID, Convert.ToInt32(objDSDriver.Tables[0].Rows[nCount]["DRIVER_ID"]) );
				objDrivers[nCount] = new Cms.Model.Application.ClsDriverDetailsInfo();
				base.PopulateModelObject(objDrivers[nCount], strDriverXml);

				//objDriver.getd

				string strDateOfBirth="";
				if (objDSDriver.Tables[0].Rows[nCount]["DRIVER_DOB"]!= null)
				{
					strDateOfBirth=Convert.ToDateTime(objDSDriver.Tables[0].Rows[nCount]["DRIVER_DOB"].ToString()).ToString("MMddyyyy");   
				}				
			}

			Cms.CmsWeb.Utils.Utility objUtil = new Cms.CmsWeb.Utils.Utility();
			System.Collections.Specialized.StringCollection DriversCol = objUtil.GetUndisclosedDrivers(objDrivers);		

		}

		//check cutomer last mvr fetch
		private bool CheckCustomerMVRFetch()
		{
			bool bIsRequired=false;
			DataSet objDSMVRDetail= new  DataSet();  
			ClsGeneralInformation objGenInfo = new ClsGeneralInformation();
			gIntCUSTOMER_ID		= int.Parse(Request["CUSTOMER_ID"] == null ? "0" : Request["CUSTOMER_ID"].ToString());
			// mapp all violation code with wolverine violation code and get details 
			objDSMVRDetail= objGenInfo.GetMVRFetchDetail(gIntCUSTOMER_ID);
			if (objDSMVRDetail!= null )
			{
				if(objDSMVRDetail.Tables[0]!= null)
				{
					if ( objDSMVRDetail.Tables[0].Rows.Count>0 )
					{
						if (objDSMVRDetail.Tables[0].Rows[0]["LAST_MVR_SCORE_FETCHED"] != System.DBNull.Value)
						{
							
							if(Convert.ToInt32( objDSMVRDetail.Tables[0].Rows[0]["LAST_MVR_SCORE_FETCHED"]) >0)
							{
								bIsRequired=true;
							} 
						}
						else
						{
							bIsRequired=true;
						}
					}
				}
			}
			return bIsRequired;
		}

		private void SetMultiPolicy()
		{
			//Sumit Chhabra:17-03-2006:Try-Catch statement added to prevent the page from crashing
			//Error comes at GetLOBID() function ..Session value being not set when adding new application
			try
			{
				objGeneralInformation = new ClsGeneralInformation();
				string PolicyNumber = "";
			
				int AgencyID = objGeneralInformation.GetAgencyId(GetSystemId());

			
				if (hidOldData.Value != null && hidOldData.Value != "0" && hidOldData.Value != "")
				{
					SetLOBID(FetchValueFromXML("LOB_ID",hidOldData.Value));
					PolicyNumber = FetchValueFromXML("APP_NUMBER",hidOldData.Value);
					PolicyNumber = PolicyNumber.Substring(0,(PolicyNumber.Length - 3));
				}
			
				lblAllPolicy.Text = objGeneralInformation.GetAllPolicyNumber(int.Parse(GetCustomerID()),AgencyID,PolicyNumber);
				if(GetLOBID()!="")//Added by Charles on 18-Sep-09 for APP/POL Optimization
					lblEligbilePolicy.Text = objGeneralInformation.GetEligiblePolicyNumber(int.Parse(GetCustomerID()),AgencyID,int.Parse(GetLOBID()),PolicyNumber);

				if (lblAllPolicy.Text.Trim() == "")
					lblAllPolicy.Text = "N.A.";

				if (lblEligbilePolicy.Text.Trim() == "")
					lblEligbilePolicy.Text = "N.A.";
			}
			catch
			{
				if (lblAllPolicy.Text.Trim() == "")
					lblAllPolicy.Text = "N.A.";

				if (lblEligbilePolicy.Text.Trim() == "")
					lblEligbilePolicy.Text = "N.A.";
			}			
		}

		private void GetDefaultInstallmentPlan()
		{
			Cms.BusinessLayer.BlCommon.Accounting.ClsInstallmentPlan objPlan = new 
				Cms.BusinessLayer.BlCommon.Accounting.ClsInstallmentPlan();

			hidFULL_PAY_PLAN_ID.Value = Cms.BusinessLayer.BlCommon.Accounting.ClsInstallmentPlan.FullPayPlanID().ToString();
			if(cmbAPP_TERMS.SelectedItem!=null && cmbAPP_TERMS.SelectedItem.Value!="")
			{
				int PolTerm = int.Parse(cmbAPP_TERMS.SelectedItem.Value.ToString());
				int PlanID = objPlan.GetDefaultPlanId(PolTerm);
				if (PlanID != 0)
				{
					if (cmbINSTALL_PLAN_ID.Items.FindByValue(PlanID.ToString()) != null)
					{
						cmbINSTALL_PLAN_ID.SelectedValue = PlanID.ToString();
					}
					hidINSTALL_PLAN_ID.Value = cmbINSTALL_PLAN_ID.SelectedValue;
				}
			}
		}
		
		#region GetFormValue
		private string FetchValueFromXML(string nodeName, string XMLString)
		{
			try 
			{
				string strRetval="";
				XmlDocument doc = new XmlDocument();
				doc.LoadXml(XMLString);
				XmlNodeList nodList = doc.GetElementsByTagName(nodeName);
				if (nodList.Count >0)
				{
					strRetval=nodList.Item(0).InnerText;
				}
				return strRetval;			
			}
			catch(Exception exc)
			{
				throw(exc);
			}
			finally
			{
			
			}		
		}
		
		private ClsGeneralInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsGeneralInfo objGeneralInfo;
			objGeneralInfo = new ClsGeneralInfo();

			if(hidCustomerID.Value.ToString().Trim()!=""  && hidCustomerID.Value.ToString().Trim()!="NEW")
			{
				objGeneralInfo.CUSTOMER_ID = int.Parse(hidCustomerID.Value.ToString()==""?"0":hidCustomerID.Value.ToString());
			}
			else
			{
				string customerID  = GetCustomerID();
				hidCustomerID.Value = customerID ;
				objGeneralInfo.CUSTOMER_ID = int.Parse(customerID.Trim()==""?"0":customerID);
			}			
	
			if (txtYEAR_AT_CURR_RESI.Text.Trim() != "")
				objGeneralInfo.YEAR_AT_CURR_RESI=Convert.ToInt32(txtYEAR_AT_CURR_RESI.Text);
			
			//--- Condition added by mohit on 13/10/2005-------------.
			if (txtYEAR_AT_CURR_RESI.Text.Trim() != "" && int.Parse(txtYEAR_AT_CURR_RESI.Text.Trim()) < 3 && txtYEAR_AT_CURR_RESI.Text.Trim() != "0" )
			{
				//if (txtYEARS_AT_PREV_ADD.Text.Trim() != "")
				objGeneralInfo.YEARS_AT_PREV_ADD=txtYEARS_AT_PREV_ADD.Text;
			}
			else
			{
				objGeneralInfo.YEARS_AT_PREV_ADD="";
			}

			// Get the AppID from the database depending on the customer selected
			if(hidAppID.Value.ToString().Trim()!="" && hidAppID.Value.ToString().Equals ("NEW")) 
			{
				if(txtAPP_NUMBER.Text.Trim()== TEMP_APP_NUMBER || txtAPP_NUMBER.Text.Trim()== TEMP_APP_NUMBER_PT)
				{
					objGeneralInfo.APP_ID = 1;//ClsGeneralInformation.GetAppIDForCustomer(objGeneralInfo.CUSTOMER_ID); //1;
					objGeneralInfo.APP_NUMBER=	ClsGeneralInformation.GenerateApplicationNumber(int.Parse(cmbAPP_LOB.SelectedItem.Value),int.Parse(hidAPP_AGENCY_ID.Value=="" ? "0" : hidAPP_AGENCY_ID.Value));//Changed to hidAPP_AGENCY_ID.Value by Charles on 21-Aug-09 for APP/POL Optimization
				}
				else
				{
					objGeneralInfo.APP_NUMBER=	txtAPP_NUMBER.Text;
					hidAppID.Value = GetAppID();
					objGeneralInfo.APP_ID = int.Parse(hidAppID.Value);
				}
			}
			else
			{
				objGeneralInfo.APP_NUMBER=	txtAPP_NUMBER.Text;
				objGeneralInfo.APP_ID = int.Parse(hidAppID.Value);
				
			}
			objGeneralInfo.APP_VERSION_ID =int.Parse(hidAppVersionID.Value);
			objGeneralInfo.APP_STATUS=	txtAPP_STATUS.Text;
		 
			objGeneralInfo.APP_VERSION=	txtAPP_VERSION.Text;
			
			if(txtAPP_INCEPTION_DATE.Text.Trim()!="")
			{
				objGeneralInfo.APP_INCEPTION_DATE=	Convert.ToDateTime(txtAPP_INCEPTION_DATE.Text);
			}
			objGeneralInfo.APP_EFFECTIVE_DATE=	Convert.ToDateTime(txtAPP_EFFECTIVE_DATE.Text);
			objGeneralInfo.APP_EXPIRATION_DATE=	Convert.ToDateTime(txtAPP_EXPIRATION_DATE.Text);
			objGeneralInfo.APP_LOB=	cmbAPP_LOB.SelectedValue==null?"":cmbAPP_LOB.SelectedValue;
			objGeneralInfo.APP_SUBLOB=	hidSUB_LOB.Value=="" ? "0" : hidSUB_LOB.Value;
			
			objGeneralInfo.CSR = int.Parse(hidCSR.Value=="" ? "-1" : hidCSR.Value);
			
			objGeneralInfo.PRODUCER = int.Parse(hidProducer.Value=="" ? "0" : hidProducer.Value);
			hidLOBID.Value  =	cmbAPP_LOB.SelectedItem.Value==null ?"0":cmbAPP_LOB.SelectedItem.Value;

			if (hidOldData.Value.Trim() != "" && hidOldData.Value.Trim() != "0")
			{
				//Taking from hidden control in edit mode
				objGeneralInfo.POLICY_TYPE = int.Parse(hidPOLICY_TYPE.Value);
			}
			else
			{
				//Taking from combox in add mode
				//if(cmbPOLICY_TYPE.SelectedItem!=null)
				if((hidLOBID.Value==((int)enumLOB.HOME).ToString() || hidLOBID.Value==((int)enumLOB.REDW).ToString()) && cmbPOLICY_TYPE.SelectedItem!=null && cmbPOLICY_TYPE.SelectedItem.Value!="")
				{
					if(cmbPOLICY_TYPE.SelectedItem.Value !="")
					{
						objGeneralInfo.POLICY_TYPE=int.Parse(cmbPOLICY_TYPE.SelectedItem.Value);
						hidPOLICY_TYPE.Value =cmbPOLICY_TYPE.SelectedItem.Value;
					}
				
				}
			}

			objGeneralInfo.COUNTRY_ID =1;

			if(cmbPROXY_SIGN_OBTAINED.SelectedItem!=null && cmbPROXY_SIGN_OBTAINED.SelectedItem.Value!="") 
			{
				objGeneralInfo.PROXY_SIGN_OBTAINED = int.Parse(cmbPROXY_SIGN_OBTAINED.SelectedItem.Value);
			}
			if(cmbSTATE_ID.SelectedItem != null)
			{
				if(cmbSTATE_ID.SelectedItem.Value!="")
					objGeneralInfo.STATE_ID = int.Parse(cmbSTATE_ID.SelectedItem.Value); 
				else
					objGeneralInfo.STATE_ID = int.Parse(hidSTATE_ID.Value);  
			}			
			
			objGeneralInfo.APP_AGENCY_ID=int.Parse(hidAPP_AGENCY_ID.Value=="" ? "0" :hidAPP_AGENCY_ID.Value); //Added by Charles on 21-Aug-09 for APP/POL Optimization

			DataSet ds = new DataSet();
			ds=ClsDefaultHierarchy.GetDefaultHierarchy(Convert.ToInt32(hidAPP_AGENCY_ID.Value)); //Changed to hidAPP_AGENCY_ID.Value by Charles on 21-Aug-09 for APP/POL Optimization
			if(ds!=null && ds.Tables[0].Rows.Count>0)
			{
				objGeneralInfo.DIV_ID=	Convert.ToInt32(ds.Tables[0].Rows[0]["Division"].ToString());
				objGeneralInfo.DEPT_ID=	Convert.ToInt32(ds.Tables[0].Rows[0]["DeptId"].ToString());
				objGeneralInfo.PC_ID=	Convert.ToInt32(ds.Tables[0].Rows[0]["ProfitCenterId"].ToString());
			}
			else
			{
				objGeneralInfo.DIV_ID=	0;
				objGeneralInfo.DEPT_ID=	0;
				objGeneralInfo.PC_ID=	0;
			}
			ds.Dispose();

			if(cmbBILL_TYPE_ID.SelectedItem != null)
			{
				objGeneralInfo.BILL_TYPE_ID     =   int.Parse(cmbBILL_TYPE_ID.SelectedValue);
				if(objGeneralInfo.BILL_TYPE_ID == 8460 || objGeneralInfo.BILL_TYPE_ID == 11150 || objGeneralInfo.BILL_TYPE_ID == 11278 || objGeneralInfo.BILL_TYPE_ID ==11276) //Insured Bill/Insured Bill all terms/Insured Bill 1st term/Mortgagee @renewal 

				{
					if(hidDOWN_PAY_MODE.Value!="")
						objGeneralInfo.DOWN_PAY_MODE = int.Parse(hidDOWN_PAY_MODE.Value); 
					
					if(Request["cmbINSTALL_PLAN_ID"]!=null)
						objGeneralInfo.INSTALL_PLAN_ID = Convert.ToInt32(Request["cmbINSTALL_PLAN_ID"]); 
					else
						objGeneralInfo.INSTALL_PLAN_ID= int.Parse(hidFULL_PAY_PLAN_ID.Value);

				}
				
			}
			
			if(cmbCHARGE_OFF_PRMIUM.SelectedItem!=null)
			{
				objGeneralInfo.CHARGE_OFF_PRMIUM=cmbCHARGE_OFF_PRMIUM.SelectedItem.Value;
			}
			if(txtRECEIVED_PRMIUM.Text.Trim()!="")
			{
				objGeneralInfo.RECEIVED_PRMIUM=double.Parse(txtRECEIVED_PRMIUM.Text);
			}
			objGeneralInfo.COMPLETE_APP = chkCOMPLETE_APP.Checked==true?"Y":"N";

			if(cmbPROPRTY_INSP_CREDIT.SelectedItem != null)
			{
				objGeneralInfo.PROPRTY_INSP_CREDIT=	cmbPROPRTY_INSP_CREDIT.SelectedValue;
			}
			if(cmbPIC_OF_LOC.SelectedItem!=null)
			{
				objGeneralInfo.PIC_OF_LOC=cmbPIC_OF_LOC.SelectedItem.Value;
			}
			// chk for home employee

			string AgencyCode=ClsAgency.GetAgencyCodeFromID(int.Parse(hidAPP_AGENCY_ID.Value=="" ? "0" :hidAPP_AGENCY_ID.Value));//Changed to hidAPP_AGENCY_ID.Value by Charles on 21-Aug-09 for APP/POL Optimization
            //Changed by Charles on 19-May-10 for Itrack 51
            string appAgencyCode = CarrierSystemID;// System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID");
			if ( appAgencyCode.ToUpper().Trim() == AgencyCode.ToUpper().Trim())
				objGeneralInfo.IS_HOME_EMP = true;
			else
				objGeneralInfo.IS_HOME_EMP = false;			
			
			//These  assignments are common to all pages.

			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidAppID.Value;
			oldXML			=	hidOldData.Value;
			
			 
			return objGeneralInfo;
		}
		#endregion

		#region Set Validators ErrorMessages
		private void SetErrorMessages()
		{
			rfvCUSTOMER_ID.ErrorMessage			= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("100");
			rfvAPP_TERMS.ErrorMessage			= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("93");
			rfvAPP_EFFECTIVE_DATE.ErrorMessage	= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("95");
			rfvAPP_EXPIRATION_DATE.ErrorMessage	= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("96");
			rfvAPP_LOB.ErrorMessage				= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("97");
			rfvSTATE_ID.ErrorMessage			= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("35");
			revAPP_EFFECTIVE_DATE.ErrorMessage  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			revAPP_INCEPTION_DATE.ErrorMessage  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			rngYEAR_AT_CURR_RESI.ErrorMessage	= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");			
			rfvBILL_TYPE.ErrorMessage			= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("218");			
			revRECEIVED_PRMIUM.ErrorMessage		= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("611");
			rfvPOLICY_TYPE.ErrorMessage			= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("428");
			rfvPROXY_SIGN_OBTAINED.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("479");
			csvYEARS_AT_PREV_ADD.ErrorMessage	= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("445");			
			revYEAR_AT_CURR_RESI.ErrorMessage	= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
			csvAPP_EFFECTIVE_DATE.ErrorMessage	= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("490");
			rfvYEARS_AT_PREV_ADD.ErrorMessage	= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("520");
			rfvINSTALL_PLAN_ID.ErrorMessage		= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("731");
			rfvPROPRTY_INSP_CREDIT.ErrorMessage	= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("797");
			rfvPIC_OF_LOC.ErrorMessage			= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("798");
			rfvDOWN_PAY_MODE.ErrorMessage		= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1062");
	 
			revYEAR_AT_CURR_RESI.ValidationExpression	= aRegExpInteger;
			revRECEIVED_PRMIUM.ValidationExpression		= aRegExpCurrencyformat;
			revAPP_EFFECTIVE_DATE.ValidationExpression  = aRegExpDate;
			revAPP_INCEPTION_DATE.ValidationExpression	= aRegExpDate;
		}
		#endregion		

		#endregion
		
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.cmbSTATE_ID.SelectedIndexChanged += new System.EventHandler(this.cmbSTATE_ID_SelectedIndexChanged);
			this.cmbAPP_LOB.SelectedIndexChanged += new System.EventHandler(this.cmbAPP_LOB_SelectedIndexChanged);			
			this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
			this.btnSubmitAnyway.Click += new System.EventHandler(this.btnSubmitAnyway_Click);
			this.btnConvertAppToPolicy.Click += new System.EventHandler(this.btnConvertAppToPolicy_Click);
			this.btnVerifyApplication.Click += new System.EventHandler(this.btnVerifyApplication_Click);
			this.btnQuote.Click += new System.EventHandler(this.btnQuote_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);			
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.Load += new System.EventHandler(this.Page_Load);
			//this.cmbChooseLang.SelectedIndexChanged+=new EventHandler(cmbChooseLang_SelectedIndexChanged);
			this.Unload+=new EventHandler(GeneralInfoEx_Unload);
		}
		#endregion
		
		#region "Web Event Handlers"
		/// <summary>
		/// If form is posted back then add entry in database using the BL object
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			ClsGeneralInfo objGeneralInfo = GetFormValue();
			objGeneralInfo.APP_TERMS=	appTerms;
			objGeneralInfo.APP_EXPIRATION_DATE=Convert.ToDateTime(exp_Date);
			try
			{
				//For retrieving the return value of business class save function
				int intRetVal;	
				objGeneralInformation = new  ClsGeneralInformation();
				objGeneralInformation .TransactionRequired =true;

				//Retreiving the form values into model class object
				string strLOB="";
				if(cmbAPP_LOB.SelectedIndex>0)
					strLOB=cmbAPP_LOB.SelectedItem.Text;

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objGeneralInfo.CREATED_BY = int.Parse(GetUserId());
					objGeneralInfo.CREATED_DATETIME = DateTime.Now;
					objGeneralInfo.IS_ACTIVE ="Y";
					strRowId = objGeneralInfo.APP_ID.ToString();
					//Calling the add method of business layer class
					intRetVal = objGeneralInformation.Add1(objGeneralInfo,strLOB);

					if(intRetVal>0)
					{
						hidCustomerID.Value = objGeneralInfo.CUSTOMER_ID.ToString();
						hidAppID.Value  = intRetVal.ToString() ;
						primaryKeyValues=hidAppID.Value  + "^" + hidCustomerID.Value + "^" +  hidAppVersionID.Value; 
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidIS_ACTIVE.Value = "Y";
						SetAppID(intRetVal.ToString());
						SetAppVersionID(hidAppVersionID.Value.ToString());
						SetCustomerID(hidCustomerID.Value.ToString());
						//btnQuote.Visible =true;		
						btnCopy.Visible =true;
						btnActivateDeactivate.Visible = true;
						btnDelete.Visible =true;
						//Loading the page again, so that workflow executes and will show itself
						//RegisterStartupScript("LoadPage","<script>parent.document.location.href='/Cms/Application/aspx/ApplicationTab.aspx?CalledFrom=APP&LoadedAfterSave=true'</script>");
                        ClientScript.RegisterStartupScript(this.GetType(),"LoadPage", "<script>parent.document.location.href='/Cms/Application/aspx/ApplicationTab.aspx?CalledFrom=APP&APP_ID=" + hidAppID.Value + "&APP_VERSION_ID=" + hidAppVersionID.Value + "&LoadedAfterSave=true'</script>");
					
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"18");
						hidFormSaved.Value			=		"2";
					}
					else
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value			=	"2";
					}
					
					lblMessage.Visible = true;	
					hidAppID.Value  = intRetVal.ToString();
					hidOldData.Value = @ClsGeneralInformation.FetchApplicationXML(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidAppID.Value.ToString()==""?"0":hidAppID.Value.ToString()),int.Parse (objGeneralInfo.APP_VERSION_ID.ToString()==""?"0":hidAppVersionID.Value));
					// Get the LOBID
					strLOB_ID = FetchValueFromXML("LOB_ID",hidOldData.Value);
					switch(strLOB_ID)
					{
						case LOB_HOME:
							SetLOBString("HOME");
							break;
						case LOB_PRIVATE_PASSENGER:
							SetLOBString("PPA");
							break;
						case LOB_MOTORCYCLE:
							SetLOBString("MOT");
							break;
						case LOB_WATERCRAFT:
							SetLOBString("WAT");
							break;
						case LOB_RENTAL_DWELLING:
							SetLOBString("RENT");
							break;
						case LOB_UMBRELLA:
							SetLOBString("UMB");
							break;
						case LOB_GENERAL_LIABILITY:
							SetLOBString("GEN");
							break;						
					}
					hidCallefroms.Value =GetLOBString();

				} 
				else //UPDATE CASE
				{

					//Creating the Model object for holding the Old data
					ClsGeneralInfo objOldGeneralInfo;
					objOldGeneralInfo = new ClsGeneralInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldGeneralInfo,hidOldData.Value);
 
					//Setting those values into the Model object which are not in the page
					//objGeneralInfo.CUSTOMER_ID = int.Parse (strRowId);
					objGeneralInfo.MODIFIED_BY = int.Parse(GetUserId());
					objGeneralInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					objGeneralInfo.IS_ACTIVE = hidIS_ACTIVE.Value;

					//Updating the record using business layer class object
					intRetVal	= objGeneralInformation.Update(objOldGeneralInfo,objGeneralInfo,strLOB);
					
					//intRetVal	= objGeneralInformation.Update(objOldGeneralInfo,objGeneralInfo,strLOB);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						primaryKeyValues=hidAppID.Value  + "^" + hidCustomerID.Value + "^" +  hidAppVersionID.Value; 
						hidFormSaved.Value		=	"1";
						SetAppID(hidAppID.Value.ToString());
						SetAppVersionID(hidAppVersionID.Value.ToString());
						SetCustomerID(hidCustomerID.Value.ToString());
						//Added By kasana to BIND BILL TYPE when a Inactive Bill Type is Saved:
						ClsGeneralInformation.GetBillType(cmbBILL_TYPE_ID,Convert.ToInt32(cmbAPP_LOB.SelectedValue),int.Parse(hidCustomerID.Value),hidAppID.Value,int.Parse(hidAppVersionID.Value),"APP");
						cmbBILL_TYPE_ID.SelectedIndex=cmbBILL_TYPE_ID.Items.IndexOf(cmbBILL_TYPE_ID.Items.FindByValue(objGeneralInfo.BILL_TYPE_ID.ToString()));
						//cmbAPP_AGENCY_ID.SelectedIndex=cmbAPP_AGENCY_ID.Items.IndexOf(cmbAPP_AGENCY_ID.Items.FindByValue(objGeneralInfo.APP_AGENCY_ID.ToString())); //Commented by Charles on 21-Aug-09 for APP/POL Optimization
										 
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"18");
						hidFormSaved.Value		=	"2";
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value		=	"2";
					}
					
					lblMessage.Visible = true;
					
					hidOldData.Value = @ClsGeneralInformation.FetchApplicationXML(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (objGeneralInfo.APP_ID.ToString()==""?"0":hidAppID.Value),int.Parse (objGeneralInfo.APP_VERSION_ID.ToString()==""?"0":hidAppVersionID.Value));

					//Refill Bill plan in case of Deactive : 20 june
					CheckBillingPlan();	//Moved here by Charles on 16-Sep-09 for APP/POL Optimization
					// Get the LOBID
					strLOB_ID = FetchValueFromXML("LOB_ID",hidOldData.Value);
					switch(strLOB_ID)
					{
						case LOB_HOME:
							SetLOBString("HOME");
							break;
						case LOB_PRIVATE_PASSENGER:
							SetLOBString("PPA");
							break;
						case LOB_MOTORCYCLE:
							SetLOBString("MOT");
							break;
						case LOB_WATERCRAFT:
							SetLOBString("WAT");
							break;
						case LOB_RENTAL_DWELLING:
							SetLOBString("RENT");
							break;
						case LOB_UMBRELLA:
							SetLOBString("UMB");
							break;
						case LOB_GENERAL_LIABILITY:
							SetLOBString("GEN");
							break;						
					}
					hidCallefroms.Value =GetLOBString();
				}

			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
				//Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objGeneralInformation!= null)
					objGeneralInformation.Dispose();
				//Set Bill Type After saving page :Refer Onchange evnet of Agemcy"
				hidBILL_TYPE_FLAG.Value="0";
			}
		}
		
		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{			
			try
			{
				ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();
				ClsGeneralInfo objGeneralInfo = new ClsGeneralInfo();
				objGeneralInfo.CUSTOMER_ID = int.Parse(hidCustomerID.Value);
				objGeneralInfo.APP_ID = int.Parse(hidAppID.Value);
				objGeneralInfo.APP_VERSION_ID = int.Parse(hidAppVersionID.Value);
				objGeneralInfo.CREATED_BY = objGeneralInfo.MODIFIED_BY = int.Parse(GetUserId());
				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					objGeneralInformation.ActivateDeactivate(objGeneralInfo,"N");
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";	
					//btnActivateDeactivate.Text="Activate";	//Added by Charles on 7-Oct-09 for Itrack 6532
				}
				else
				{
					objGeneralInformation.ActivateDeactivate(objGeneralInfo,"Y");
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";
					//btnActivateDeactivate.Text="Deactivate"; //Added by Charles on 7-Oct-09 for Itrack 6532
				}
                SetActivateDeactivate((new cmsbase()).GetLanguageCode());

				lblMessage.Visible = true;					
				hidOldData.Value = @ClsGeneralInformation.FetchApplicationXML(int.Parse(hidCustomerID.Value),int.Parse(hidAppID.Value),int.Parse(hidAppVersionID.Value));				
				hidFormSaved.Value			=	"1";	
				CheckSecurityXml();
				
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21")+ " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			finally
			{
				lblMessage.Visible = true;				
			}
		}		

		//Added by Mohit/Manoj 19-Jul-2007
		private void SetAgencyBillType()
		{
			ClsAgency objAgency = new ClsAgency();
			DataSet dsAgency = objAgency.FetchData(int.Parse(hidAPP_AGENCY_ID.Value==""?"0":hidAPP_AGENCY_ID.Value));//Changed to hidAPP_AGENCY_ID.Value by Charles on 21-Aug-09 for APP/POL Optimization
			if(dsAgency!= null && dsAgency.Tables[0].Rows.Count > 0)
			{
				string bill_type = dsAgency.Tables[0].Rows[0]["AGENCY_BILL_TYPE"].ToString().Trim();
				
				for(int index =0;index < cmbBILL_TYPE_ID.Items.Count;index++)
				{	
					if(cmbBILL_TYPE_ID.Items[index].Value == bill_type)
					{
						cmbBILL_TYPE_ID.SelectedIndex = index;
						break;
					}
					else
					{
						if(cmbBILL_TYPE_ID.Items[index].Value=="8460" && bill_type=="11150")
						{
							cmbBILL_TYPE_ID.SelectedIndex = index;
							break;
						}
					}
					
				}
			}

		}
		private void cmbAPP_TERMS_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//change the expiry date
		
			if (hidFormSaved.Value !="4")
			{
				hidFormSaved.Value ="3";
			}
			if (cmbAPP_TERMS.SelectedItem !=null && cmbAPP_TERMS.SelectedItem.Value!="")
			{
				int iMonths=0;
				if(cmbAPP_TERMS.SelectedIndex != -1)
					iMonths=int.Parse(cmbAPP_TERMS.SelectedItem.Value);
				txtAPP_EXPIRATION_DATE.Text = Convert.ToDateTime(txtAPP_EFFECTIVE_DATE.Text).AddMonths(iMonths).ToString("MM/dd/yyyy");;
			}
			CheckBillingPlan(); //Added by Charles on 14-Sep-09 for APP/POL Optimization
			GetDefaultInstallmentPlan();
			
		}

		private void btnCopy_Click(object sender, System.EventArgs e)
		{
			try
			{ 
				string CreatedBy = GetUserId();
				string strLOB="";
				ClsGeneralInfo objGeneralInfo = GetFormValue();
				//Added by Asfa (23-Apr-2008) - iTrack issue #4043
				objGeneralInfo.APP_TERMS=	appTerms;
				if(objGeneralInfo.APP_SUBLOB == "0") 
					objGeneralInfo.APP_SUBLOB =null;

				objGeneralInformation = new  ClsGeneralInformation();
				objGeneralInformation .TransactionRequired =true;
				if(cmbAPP_LOB.SelectedIndex>0)
					strLOB=cmbAPP_LOB.SelectedItem.Text;

				int new_Version=-1;
				ClsGeneralInformation objGenInfo = new ClsGeneralInformation();
				//int RetVal = objGenInfo.CopyApplication(objGeneralInfo,int.Parse(hidCustomerID.Value), int.Parse(hidAppID.Value ), int.Parse(hidAppVersionID.Value), int.Parse (CreatedBy==""?"0":CreatedBy),out new_Version);				

				int RetVal = objGenInfo.CopyApplication(objGeneralInfo,int.Parse(hidCustomerID.Value), int.Parse(hidAppID.Value ), int.Parse(hidAppVersionID.Value), int.Parse (CreatedBy==""?"0":CreatedBy),strLOB,out new_Version);				
				if(RetVal>0)
				{					
					lblMessage.Text			=	"Application # -" + txtAPP_NUMBER.Text + "- Version # -" + txtAPP_VERSION.Text +  "- copied successfully."; 
					hidFormSaved.Value		=	"1";
					//RegisterStartupScript("LoadPage","<script>parent.location.href = '/Cms/Application/aspx/ApplicationTab.aspx?CUSTOMER_ID=" +custID + "&APP_ID=" +appID+ "&APP_VERSION_ID=" +new_Version+ "&CALLEDFROM=APP';</script>");
				}
				else 
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
					hidFormSaved.Value		=	"2";
				}
				lblMessage.Visible = true;
					
				hidOldData.Value = @ClsGeneralInformation.FetchApplicationXML(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidAppID.Value.ToString()==""?"0":hidAppID.Value),int.Parse (hidAppVersionID.Value.ToString()==""?"0":hidAppVersionID.Value));
				strNewVersion="1";
				strVersionID=new_Version;
				
				
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			finally
			{}
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{ 
				string DeletedBy = GetUserId();
				string CreatedBy = GetUserId();
				ClsGeneralInfo objGeneralInfo = GetFormValue();
				objGeneralInformation = new  ClsGeneralInformation();
				objGeneralInformation .TransactionRequired =true;
				ClsGeneralInformation objGenInfo = new ClsGeneralInformation();
				int RetVal = objGenInfo.DeleteApplication(objGeneralInfo,int.Parse(hidCustomerID.Value), int.Parse(hidAppID.Value ), int.Parse(hidAppVersionID.Value), int.Parse (DeletedBy==""?"0":DeletedBy));
				if(RetVal>0)
				{
					lblFormLoadMessage.Text		=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"406");
					hidFormSaved.Value			=	"5";		//If record deleted, fidFormSaved should be 5
					lblFormLoadMessage.Visible	=	true;	
					trDETAILS.Visible			=	false; 
					btnDelete.Visible           =   false;
					delStr						=	"1";
					trFORMMESSAGE.Visible		=	true;
					SetAppID("");
					SetAppVersionID("");
                    ClientScript.RegisterStartupScript(this.GetType(),"LoadPage", "<script>parent.document.location.href='/Cms/Application/aspx/ApplicationTab.aspx?CalledFrom=CLT&LoadedAfterSave=false'</script>");
				}
				else 
				{
					lblMessage.Text				=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"407");
					hidFormSaved.Value			=	"2";
					lblMessage.Visible			=	true;	
					delStr						=	"0";
					trDETAILS.Visible			=	true; 
					btnDelete.Visible           =   true;
					trFORMMESSAGE.Visible		=	false;
				}
			}
			catch(Exception ex)
			{throw(ex);}
			finally
			{}
		}

		private void btnQuote_Click(object sender, System.EventArgs e)
		{
			try 
			{
				#region		GENERATE QUOTE
				ClsGeneralInformation objGeneralInfo = new ClsGeneralInformation();
				 
				// Get the 3 keys
				gIntCUSTOMER_ID		= int.Parse(Request["CUSTOMER_ID"] == null ? "0" : Request["CUSTOMER_ID"].ToString());
				gIntAPP_ID 			=	int.Parse(Request["APP_ID"] == null || Request["APP_ID"] =="" ? FetchValueFromXML("APP_ID",hidOldData.Value) : Request["APP_ID"].ToString());
				gIntAPP_VERSION_ID 	=	int.Parse(Request["APP_VERSION_ID"] == null || Request["APP_VERSION_ID"] ==""  ?  FetchValueFromXML("APP_VERSION_ID",hidOldData.Value) : Request["APP_VERSION_ID"].ToString());
				gstrLobID= hidLOBID.Value;
				ClsGenerateQuote objGenerateQuote = new ClsGenerateQuote(CarrierSystemID);

				
				string lIntShowQuote="",verficationMessage="";
				lIntShowQuote = objGenerateQuote.GenerateQuote(gIntCUSTOMER_ID,  gIntAPP_ID ,gIntAPP_VERSION_ID,gstrLobID,out gIntQuoteID).ToString();
				
				if (lIntShowQuote.ToString() == "1"  || lIntShowQuote.ToString() == "4" )
				{
					gIntShowQuote= 	int.Parse(lIntShowQuote);	
				}
		
				else 
				{
					
					string returnValue = lIntShowQuote;
					string[] retVal = lIntShowQuote.Split('^');
					if (retVal.Length>1) // implies tht there is problem in verification layer
					{
						verficationMessage = retVal[1].ToString();
						Session.Add("HtmlStringForXmlVer",verficationMessage);
						Response.Write("<script>window.open('ShowInputVerificationXml.aspx')</script>");
					}					
				}
                
				if (gIntShowQuote == 1)
				{
					btnQuote.Attributes.Add("Style","display:inline");
					btnConvertAppToPolicy.Attributes.Add("Style","display:inline");				
				}

				# endregion
			}
			catch(Exception exc)
			{
				throw(exc);
			}
			finally
			{}
		}

		private void cmbAPP_LOB_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(cmbSTATE_ID.SelectedValue!="" && cmbAPP_LOB.SelectedValue!="")//cmbAPP_LOB.SelectedValue check added by Charles on 25-Aug-09 for APP/POL Optimization
			{ 
				int stateId=Convert.ToInt32(cmbSTATE_ID.SelectedValue);
				try
				{
					int selVal=int.Parse(cmbAPP_LOB.SelectedValue);
					if (selVal == (int)enumLOB.HOME) //For Homeowners.
					{	
						if(stateId == (int)enumState.Michigan)
						{
							cmbPOLICY_TYPE.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("HOPTPM");
						}
						else
						{
							cmbPOLICY_TYPE.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("HOPTYP");
						}
					}
					else if(selVal == (int)enumLOB.REDW) 	// For Rental Dwelling.
					{
						if(stateId == (int)enumState.Michigan)
						{
							cmbPOLICY_TYPE.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("RTPTYP");
					
						}
						else
						{
							cmbPOLICY_TYPE.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("RTPTYI");  
						}
					}					
					cmbPOLICY_TYPE.DataTextField="LookupDesc";
					cmbPOLICY_TYPE.DataValueField="LookupID";
					cmbPOLICY_TYPE.DataBind();
					cmbPOLICY_TYPE.Items.Insert(0,""); 

					cmbAPP_TERMS.DataSource = ClsGeneralInformation.GetLOBTerms(int.Parse(cmbAPP_LOB.SelectedValue));
					cmbAPP_TERMS.DataTextField = "LOOKUP_VALUE_DESC";
					cmbAPP_TERMS.DataValueField = "LOOKUP_VALUE_CODE";
					cmbAPP_TERMS.DataBind();
					cmbAPP_TERMS.Items.Insert(0,"");

					if(cmbAPP_LOB.SelectedItem!=null)
						if(int.Parse(cmbAPP_LOB.SelectedValue)==1 || int.Parse(cmbAPP_LOB.SelectedValue)==6) //Condition added for rental dwelling(Lob Id 6) 
							policyTR.Visible=true; 
						else
							policyTR.Visible=false; 
					//Modified on 6 May 2008			
					ClsGeneralInformation.GetBillType(cmbBILL_TYPE_ID,Convert.ToInt32(cmbAPP_LOB.SelectedValue),int.Parse(hidCustomerID.Value),hidAppID.Value,int.Parse(hidAppVersionID.Value),"APP");
					SetAgencyBillType();
					switch(Convert.ToInt32(cmbAPP_LOB.SelectedValue))
					{
						case 1:
							cmbAPP_TERMS.SelectedIndex =1;
							break;
						case 2:
							cmbAPP_TERMS.SelectedIndex =1;
							break;
						case 3://Changed selected index to 2 from 12,on 31-Jul-09,for Itrack 6205,by Charles
							cmbAPP_TERMS.SelectedIndex =2;
							break;
						case 4:
							cmbAPP_TERMS.SelectedIndex =1;
							break;
						case 5:
							cmbAPP_TERMS.SelectedIndex =1;
							break;
						case 6:
							cmbAPP_TERMS.SelectedIndex =1;
							break;
						case 7:
							cmbAPP_TERMS.SelectedIndex =1;
							break;						
					}
					//Oct5:2005:Sumit:Execute the event corresponding to selection of policy term listbox 
					cmbAPP_TERMS_SelectedIndexChanged(null,null);
					hidFocusFlag.Value="1"; 
					setCarrierInsuredBill();
					//GetDefaultInstallmentPlan(); //Commented by Charles on 25-Aug-09 for APP/POL optimization. Function already called inside cmbAPP_TERMS_SelectedIndexChanged
				}
				catch(Exception ex)
				{
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				}
			}
		}

		private void cmbSTATE_ID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				if(cmbSTATE_ID.SelectedIndex !=0 && cmbSTATE_ID.SelectedValue != "")//Added by Charles on 18-Sep-09 for APP/POL Optimization
				{
					int stateID;
					ClsGeneralInformation objGenInfo=new ClsGeneralInformation(); 
					DataSet dsLOB=new DataSet(); 
					stateID=cmbSTATE_ID.SelectedItem==null?-1:int.Parse(cmbSTATE_ID.SelectedItem.Value);     
					if(stateID!=-1 && stateID != 0)
					{
					
						dsLOB=objGenInfo.GetLOBBYSTATEID(stateID);
						cmbAPP_LOB.DataSource=dsLOB;
						cmbAPP_LOB.DataTextField="LOB_DESC";
						cmbAPP_LOB.DataValueField="LOB_ID"; 
						cmbAPP_LOB.DataBind();						
						cmbAPP_LOB.Items.Insert(0,new ListItem("",""));
						cmbAPP_LOB_SelectedIndexChanged(null,null);
					}
				}
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
		}
		private void SetCookieValue()
		{
			Response.Cookies["LastVisitedTab"].Value = "2";
			Response.Cookies["LastVisitedTab"].Expires = DateTime.Now.Add(new TimeSpan(30,0,0,0,0));	
			Response.Write("<script>this.parent.document.location.href = '/Cms/Client/Aspx/CustomerManagerIndex.aspx';</script>");
		}
		private void btnSubmitAnyway_Click(object sender, System.EventArgs e)
		{			
			try
			{	
				if(hidIsTerminated.Value=="Y")//Added by Charles on 21-Aug-09 for APP/POL Optimization
				{
					lblMessage.Text = ClsMessages.FetchGeneralMessage("1014");
					lblMessage.Visible=true;
					return;
				}
			
				// Get the 3 keys
				gIntCUSTOMER_ID		= int.Parse(Request["CUSTOMER_ID"] == null ? "0" : Request["CUSTOMER_ID"].ToString());
				gIntAPP_ID 			=	int.Parse(Request["APP_ID"] == null || Request["APP_ID"] =="" ? FetchValueFromXML("APP_ID",hidOldData.Value) : Request["APP_ID"].ToString());				
				gIntAPP_VERSION_ID 	=	int.Parse(Request["APP_VERSION_ID"] == null || Request["APP_VERSION_ID"] ==""  ?  FetchValueFromXML("APP_VERSION_ID",hidOldData.Value) : Request["APP_VERSION_ID"].ToString());
				gstrLobID= hidLOBID.Value;
				int validApplication=0,iPolId=0,iPolVersion=0,returnResult=0;
				string strCSSNo=GetColorScheme(),AppStatus="",strMessage="",strPolNumber="",strShowMessage="";
				PrintingError=false;				
				string isActive="";				
				ClsGeneralInformation.CheckIsactiveApplication(gIntCUSTOMER_ID,gIntAPP_ID,gIntAPP_VERSION_ID,out isActive,out strMessage);
				
				//if(isActive.ToString()!="N" && isPolActive.ToString()=="")
				if(isActive!="N")
				{			
					CommonApptoPolSubmit(gIntCUSTOMER_ID, gIntAPP_ID, gIntAPP_VERSION_ID,out iPolId, out iPolVersion, out strPolNumber, strCSSNo, ClsGeneralInformation.CalledFromSubmitAnywayButton,out returnResult, out validApplication,out strShowMessage, out AppStatus, out strMessage);
					if(validApplication==1)
					{
						lblMessage.Text = strMessage;
						txtAPP_STATUS.Text = AppStatus;				
						lblMessage.Visible=true;
						if(returnResult >0)
						{
							SetCookieValue();
						}
					}	
					hidIS_ACTIVE.Value="Y";
					//btnActivateDeactivate.Text="Deactivate";
				}
				else
				{
					lblMessage.Text = strMessage;
					lblMessage.Visible=true;
					hidIS_ACTIVE.Value="N";
					//btnActivateDeactivate.Text="Activate";
				}
				//SetActivateDeactivate(LANG_CULTURE);
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				lblMessage.Text= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("553") + "\n" + ex.Message.ToString();						
				lblMessage.Visible=true;
			}
		}
		
		private void btnVerifyApplication_Click(object sender, System.EventArgs e)
		{			 
			// Get the 3 keys
			gIntCUSTOMER_ID		= int.Parse(Request["CUSTOMER_ID"] == null ? "0" : Request["CUSTOMER_ID"].ToString());
			gIntAPP_ID 			=	int.Parse(Request["APP_ID"] == null || Request["APP_ID"] =="" ? FetchValueFromXML("APP_ID",hidOldData.Value) : Request["APP_ID"].ToString());				
			gIntAPP_VERSION_ID 	=	int.Parse(Request["APP_VERSION_ID"] == null || Request["APP_VERSION_ID"] ==""  ?  FetchValueFromXML("APP_VERSION_ID",hidOldData.Value) : Request["APP_VERSION_ID"].ToString());
			gstrLobID= hidLOBID.Value;
			gIntShowVerificationResult =1;
			hidOldData.Value = @ClsGeneralInformation.FetchApplicationXML(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidAppID.Value.ToString()==""?"0":hidAppID.Value),int.Parse (hidAppVersionID.Value.ToString()==""?"0":hidAppVersionID.Value));
	
		}
		private void btnFetcgIIX_ServerClick(object sender, System.EventArgs e)
		{
			FetchUndisclosedDrivers();
		}

		#endregion

		#region	Convert Application To Policy
		private void btnConvertAppToPolicy_Click(object sender, System.EventArgs e)
		{		
			try
			{				
				if(hidIsTerminated.Value=="Y")
				{
					lblMessage.Text = ClsMessages.FetchGeneralMessage("1014");
					lblMessage.Visible=true;
					return;
				}
				
				gIntCUSTOMER_ID		= int.Parse(hidCustomerID.Value);
				gIntAPP_ID			= int.Parse(hidAppID.Value);
				gIntAPP_VERSION_ID	= int.Parse(hidAppVersionID.Value);

				gstrLobID= hidLOBID.Value;
				// get the validation range 
				ClsGeneralInformation objGenInfo = new ClsGeneralInformation();
				string strIsValid=objGenInfo.GetValidationRange(gIntCUSTOMER_ID,gIntAPP_ID,gIntAPP_VERSION_ID);
				string strSystemID = GetSystemId(); 
				string strCSSNo=GetColorScheme();
                string strMessage = "", AppStatus = "";//strCalled="SUBMIT",
				
				//if not the Wolverine User
                strCarrierSystemID = CarrierSystemID;
				if((strSystemID.ToUpper()!=strCarrierSystemID.ToUpper()) && strIsValid=="1")
				{
					//Application can be submitted only with in 15 days of Effective Date
					lblMessage.Text=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("555");				
					lblMessage.Visible=true;
					btnConvertAppToPolicy.Attributes.Add("Style","display:none");
				}
				else
				{
					int validApplication=0,returnResult=0,iPolId=0, iPolVersion=0;
					string strPolNumber,strHTML;	
					//check for 
					string isActive="";
					//string isPolActive="";
					ClsGeneralInformation.CheckIsactiveApplication(gIntCUSTOMER_ID,gIntAPP_ID,gIntAPP_VERSION_ID,out isActive,out strMessage);
					//ClsGeneralInformation.CheckPolicyExist(gIntCUSTOMER_ID,gIntAPP_ID,gIntAPP_VERSION_ID,out isPolActive,out strMessage);
					if(isActive!="N")
					{					
						CommonApptoPolSubmit(gIntCUSTOMER_ID, gIntAPP_ID,gIntAPP_VERSION_ID,out iPolId, out iPolVersion, out strPolNumber, strCSSNo,ClsGeneralInformation.CalledFromSubmitButton,out returnResult, out validApplication,out strHTML,out AppStatus,out strMessage);
						if(validApplication==1)
						{
							this.btnConvertAppToPolicy.Attributes.Add("Style","display:inline");
							this.btnSubmitInProgress.Attributes.Add("style","display:none");
							this.btnSubmitAnywayInProgress.Attributes.Add("style","display:none");
							this.btnQuote.Attributes.Add("Style","display:inline");
							//SetCookieValue();
						}
						hidIS_ACTIVE.Value="Y";
						//btnActivateDeactivate.Text="Deactivate";
					}
					else
					{
						hidIS_ACTIVE.Value="N";
						//btnActivateDeactivate.Text="Activate";
					}

                    SetActivateDeactivate((new cmsbase()).GetLanguageCode());

					lblMessage.Text = strMessage;
					txtAPP_STATUS.Text = AppStatus;	
					if(returnResult > 0)
					{
						SetCookieValue();
					}
				}
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				lblMessage.Text= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("553") + "\n" + ex.Message.ToString();						
			}	
			finally
			{
				lblMessage.Visible=true;
			}
			
		}
		
		#region Common method to submit an application into that will be called from both app main page and show dialog verify page
		public void CommonApptoPolSubmit(int iCustomerId, int iAppId, int iAppVersionId,out int iPolId, out int iPolVersionId, out string sPolicyNumber, string strCSSNo, string strCalledFrom,out int returnResult, out int ValidApplication,out string strHTML, out string AppStatus, out string strMessage)
		{
			try
			{
				PrintingError=false;
				returnResult=iPolId=iPolVersionId=0;
				ValidApplication=0;
				bool ValidProcess=false;
				strHTML = sPolicyNumber = strMessage = AppStatus="";				
				ClsRatingAndUnderwritingRules objVerifyRules = new ClsRatingAndUnderwritingRules(CarrierSystemID);
				ClsGeneralInformation objGenInfo = new ClsGeneralInformation();
				gstrLobID= GetLOBID();						
				string StartNewBusiness="N",CalledFor="";
				if(strCalledFrom==ClsGeneralInformation.CalledFromSubmitAnywayButton || strCalledFrom==ClsGeneralInformation.CalledFromSubmitAnywayImage)
				{
					if(gstrLobID=="5" || gstrLobID=="7")
					{
						ValidApplication =1;
					}
					else
					{
						strHTML= objVerifyRules.SubmitAnywayAppVerify(iCustomerId,iAppId,iAppVersionId,gstrLobID,strCSSNo,out ValidApplication,ClsGeneralInformation.CalledFromSubmitAnyway);
					}
					CalledFor = "ANYWAY";
				}
				else
				{
					strHTML = objVerifyRules.VerifyApplication(iCustomerId,iAppId,iAppVersionId,gstrLobID,strCSSNo,out ValidApplication);
					CalledFor = "SUBMIT";
				}
				if(ValidApplication==1)
				{
					//checking UnderWriter to be Assigned				
					returnResult=objGenInfo.CheckToAssignedUnderWriter(iCustomerId ,iAppId, iAppVersionId ,gstrLobID,int.Parse(GetUserId()),"VALIDATE");
					if (returnResult==-2)
					{
						strMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("974");
						return;
					}
					//Generate of Pdf has been put in separate method that will be called here
					PrintingError = GenerateAppPdf(iCustomerId,iAppId,iAppVersionId,gstrLobID);

					returnResult = objGenInfo.CopyAppToPolicy(iCustomerId ,iAppId, iAppVersionId ,int.Parse(GetUserId()),gstrLobID,CalledFor);

					if(returnResult >0)
					{
						GetPolicyDetails(iCustomerId,iAppId,iAppVersionId,out iPolId,out iPolVersionId,out sPolicyNumber);												
						if(strCalledFrom==ClsGeneralInformation.CalledFromSubmitButton || strCalledFrom==ClsGeneralInformation.CalledFromSubmitImage)
						{
							StartNewBusiness = objGenInfo.CheckForStartNBSProcess(iCustomerId ,iAppId, iAppVersionId);
							if(StartNewBusiness.ToUpper() == "Y")
							{
								strHTML="";
								ValidProcess = StartNBSProcess(iCustomerId,iAppId, iAppVersionId, iPolId,iPolVersionId,CALLED_FROM_GEN_INFO,out strHTML,gstrLobID);
							}
						}

						if(strCalledFrom==ClsGeneralInformation.CalledFromSubmitAnywayButton || strCalledFrom==ClsGeneralInformation.CalledFromSubmitButton)
						{
							hidPOLICY_ID.Value = iPolId.ToString();
							hidPOLICY_VERSION_ID.Value = iPolId.ToString();
							hidPolicyNumber.Value = sPolicyNumber;
							//Refreshing the old data xml
							hidOldData.Value = @ClsGeneralInformation.FetchApplicationXML(iCustomerId ,iAppId,iAppVersionId);
							lblUNDERWRITER.Text = FetchValueFromXML("UNDERWRITER",hidOldData.Value);
							//Assigning the screen id again for updating security xml 
							base.ScreenId = ScreenId;
							//lblMessage.Text= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("532");				
							string strSystem_ID = GetSystemId();
							int userID =int.Parse(GetUserId());
							SetButtonsSecurityXml();
							//Setting the security xml and buttons type of cmsbutton					
							SetSecurityXML(strSystem_ID,userID);							
						}
						
						
						if(StartNewBusiness.ToUpper() == "Y")
						{
							if(ValidProcess == true)
							{
								AppStatus ="Complete";			
								strMessage = "Policy Number " + sPolicyNumber + " has been created successfully and accepted.";
							}
							else
							{
								AppStatus ="Complete";		
								strMessage = "Policy Number " + sPolicyNumber + " has been created successfully";					

							}														
						}
						else
						{
							AppStatus ="Complete";			
							strMessage = "Policy Number " + sPolicyNumber + " has been created successfully";					

						}					
						if(PrintingError)
							strMessage += ", though Printing of documents has failed";
					}
					else if(returnResult==-1)
					{
						strMessage = "Policy already exists.";						
					}
					else
					{
						strMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("533");							
					}
				}
				else
				{
					if(strCalledFrom==ClsGeneralInformation.CalledFromSubmitAnywayButton)
					{
                        ClientScript.RegisterHiddenField("hidHTML", strHTML);
						ClientScript.RegisterStartupScript(this.GetType(),"ShowVerifiyDialog","<script>ShowDialogEx();</script>");					
					}
					else if(strCalledFrom==ClsGeneralInformation.CalledFromSubmitButton)
						strMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("533");							
				}
			}
			catch(Exception ex)
			{
				throw (ex);
			}
		}
		#endregion
		
		//Common Generate PDF Code that will be called for both app submit and submit anyways
		public bool GenerateAppPdf(int iCustomerId, int iAppId, int iAppVersionId,string strLOB_ID)
		{
			string strLOBCODE="";
			string agency_code="";			
			bool ErrorFlag=false;
			Cms.BusinessLayer.BlProcess.ClsCommonPdfXML objCommonPdfXml = new Cms.BusinessLayer.BlProcess.ClsCommonPdfXML();
			Cms.BusinessLayer.BlProcess.ClsCommonPdf objCommonPdf = new Cms.BusinessLayer.BlProcess.ClsCommonPdf();	
			ClsGeneralInformation objGenInfo = new ClsGeneralInformation();
			try
			{  
				strLOBCODE = objGenInfo.GetLobCodeForLobId(strLOB_ID);
				
				if(strLOB_ID.ToUpper()=="2" || strLOB_ID.ToUpper()=="1")
				{
					objCommonPdf.GenratePdfForMenu(iCustomerId.ToString(),iAppId.ToString(),iAppVersionId.ToString(),"Application","ACORD","",strLOB_ID,"",ref agency_code, "final");
					objCommonPdf.GenratePdfForMenu(iCustomerId.ToString(),iAppId.ToString(),iAppVersionId.ToString(),"Application","DECPAGE","",strLOB_ID,"",ref agency_code, "final");
				}
				else
				{
					objCommonPdfXml.GeneratePdfProxy(iCustomerId.ToString(),iAppId.ToString(),iAppVersionId.ToString(),"Application","ACORD",strLOBCODE,"",ref agency_code,"","", "final");
					//Generate Decpage
					objCommonPdfXml.GeneratePdfProxy(iCustomerId.ToString(),iAppId.ToString(),iAppVersionId.ToString(),"Application","DECPAGE",strLOBCODE,"",ref agency_code,"","", "final");
				}
				//Generate Auto Id Card
				//commeted by Pravesh on 14 may 2007 as per Itrack Issue 1494 no Auto Id Card Required at App Level
				//objCommonPdfXml.GeneratePdfProxy(iCustomerId.ToString(),iAppId.ToString(),iAppVersionId.ToString(),"Application",Cms.BusinessLayer.BlCommon.ClsCommon.DOCUMENT_TYPE_AUTO_ID_CARD,strLOBCODE,"",ref agency_code,Cms.BusinessLayer.BlCommon.ClsCommon.DOCUMENT_TYPE_AUTO_ID_CARD,"", "final");
				
				//objCommonPdfXml.GeneratePdfProxy(iCustomerId.ToString(),iAppId.ToString(),iAppVersionId.ToString(),"Application","DECPAGE",strLOBCODE,"ADDLINT",ref agency_code,"ADDLINT","", "final");				
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				ErrorFlag = true;
			}
			return ErrorFlag;
		}

		# endregion

		private void GeneralInfoEx_Unload(object sender, EventArgs e)
		{
			if(objResourceMgr!=null)
			{
				objResourceMgr.ReleaseAllResources();
			}
		}
        /*
		private void cmbChooseLang_SelectedIndexChanged(object sender, EventArgs e)
		{	
			if(cmbChooseLang.SelectedValue == hidCultureName.Value) 
				return;			
			
			switch(cmbChooseLang.SelectedValue)
			{
				case "pt-BR":
                case "en-US": (new cmsbase()).GetLanguageCode() = cmbChooseLang.SelectedValue; break;
				default: LANG_CULTURE="en-US"; break;
			}
		
			hidCultureName.Value = LANG_CULTURE;
			ClsCommon.BL_LANG_CULTURE = LANG_CULTURE;
			Session["Default_Culture"] = LANG_CULTURE;			

			Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LANG_CULTURE);
			Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;	
		
			Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "M/d/yyyy";
			Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortDatePattern = "M/d/yyyy";
			
			SetCaptions();	
			SetErrorMessages();
			SetButtonCaptions(LANG_CULTURE);
		}*/

		private void SetButtonCaptions(string CultureName)
		{
			capAPP_POLICY_TYPE.Text = objResourceMgr.GetString("capAPP_POLICY_TYPE");
			switch(CultureName)
			{
				case "pt-BR":
					if(btnReset.Visible)
						btnReset.Text = "Restabelecer";
					btnCopy.Text ="Criar Nova Versão";					
					btnSubmitAnyway.Text ="Enviar De qualquer maneira";
					btnSubmitAnywayInProgress.Text ="Enviar De qualquer maneira Em Progresso";
					if(btnConvertAppToPolicy.Visible)
						btnConvertAppToPolicy.Text ="Enviar";
					btnSubmitInProgress.Text ="Enviar Em Progresso";
					btnVerifyApplication.Text ="Verificar Aplicativo";
					if(btnQuote.Visible)
						btnQuote.Text ="Citar";
					btnSave.Text ="Salvar";
					btnBack.Text ="Costas Para Pesquisa";
					btnDelete.Text ="Excluir";
					btnCustomerAssistant.Text ="Costas Para Assistente de Apoio ao Cliente";					
				break;
				case "en-US":
				default:
					if(btnConvertAppToPolicy.Visible)
						btnConvertAppToPolicy.Text ="Submit";
					if(btnQuote.Visible)
						btnQuote.Text ="Quote";
					if(btnReset.Visible)
						btnReset.Text = "Reset";
					btnCopy.Text ="Create New Version";					
					btnSubmitAnyway.Text ="Submit Anyway";
					btnSubmitAnywayInProgress.Text ="Submit Anyway In Progress";					
					btnSubmitInProgress.Text ="Submit In Progress";
					btnVerifyApplication.Text ="Verify App";
					
					btnSave.Text ="Save";
					btnBack.Text ="Back To Search";
					btnDelete.Text ="Delete";
					btnCustomerAssistant.Text ="Back To Customer Assistant";					
				break;
			}
			btnActivateDeactivate.Text="";
			SetActivateDeactivate(CultureName);
		}
		private void SetActivateDeactivate(string CultureName)	
		{
			if(btnActivateDeactivate.Visible)
			{
				switch(CultureName)
				{
					case "pt-BR":
						if(hidIS_ACTIVE.Value=="N")
						{
							btnActivateDeactivate.Text="Ativar";
						}
						else
						{
							btnActivateDeactivate.Text="Desativar";
						}
						break;
					case "en-US":
					default:
						if(hidIS_ACTIVE.Value=="N")
						{
							btnActivateDeactivate.Text="Activate";
						}
						else
						{
							btnActivateDeactivate.Text="Deactivate";
						}
						break;
				}
			}
		}
	}	 
}