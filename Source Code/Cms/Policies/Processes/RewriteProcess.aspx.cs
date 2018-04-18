/******************************************************************************************
<Author				: -   Pravesh k Chandel
<Start Date			: -	  21-Feb-2007
<End Date			: -	 
<Description		: -  cs file for Rewrite Policy Process.
<Review Date		: - 
<Reviewed By		: - 	
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
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlProcess;
using Cms.Model.Policy.Process;
using Cms.CmsWeb;
using System.Xml;
namespace Cms.Policies.Processes
{
	/// <summary>
	/// Summary description for RewriteProcess.
	/// </summary>
	public class RewriteProcess : Cms.Policies.Processes.Processbase
	{
		protected Cms.CmsWeb.WebControls.PolicyTop cltPolicyTop;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capEFFECTIVE_DATETIME;
		protected System.Web.UI.WebControls.TextBox txtEFFECTIVE_DATETIME;
		protected System.Web.UI.WebControls.HyperLink hlkReinstateEffectiveDate;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEFFECTIVE_DATETIME;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEFFECTIVE_DATETIME;
		protected System.Web.UI.WebControls.Label capEFFECTIVE_TIME;
		protected System.Web.UI.WebControls.TextBox txtEFFECTIVE_HOUR;
		protected System.Web.UI.WebControls.Label lblEFFECTIVE_HOUR;
		protected System.Web.UI.WebControls.TextBox txtEFFECTIVE_MINUTE;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DropDownList cmbMERIDIEM;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEFFECTIVE_HOUR;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEFFECTIVE_MINUTE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMERIDIEM;
		protected System.Web.UI.WebControls.RangeValidator rnvEFFECTIVE_HOUR;
		protected System.Web.UI.WebControls.RangeValidator rnvtEFFECTIVE_MINUTE;
		protected System.Web.UI.WebControls.Label capPOLICY_TERMS;
		protected System.Web.UI.WebControls.DropDownList cmbPOLICY_TERMS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPOLICY_TERMS;
		protected System.Web.UI.WebControls.Label capNEW_POLICY_TERM_EFFECTIVE_DATE;
		protected System.Web.UI.WebControls.TextBox txtNEW_POLICY_TERM_EFFECTIVE_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkTermEffectiveDate;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNEW_POLICY_TERM_EFFECTIVE_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revNEW_POLICY_TERM_EFFECTIVE_DATE;
		protected System.Web.UI.WebControls.Label capNEW_POLICY_TERM_EXPIRATION_DATE;
		protected System.Web.UI.WebControls.TextBox txtNEW_POLICY_TERM_EXPIRATION_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNEW_POLICY_TERM_EXPIRATION_DATE;
		protected System.Web.UI.WebControls.Label capREQUESTED_BY;
		protected System.Web.UI.WebControls.DropDownList cmbREQUESTED_BY;
		//protected System.Web.UI.WebControls.Label capPRINT_COMMENTS;
		//protected System.Web.UI.WebControls.DropDownList cmbPRINT_COMMENTS;
		//protected System.Web.UI.WebControls.Label capCOMMENTS;
		//protected System.Web.UI.WebControls.TextBox txtCOMMENTS;
		protected System.Web.UI.WebControls.Label lblCOMM;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOMMENTS;
		protected System.Web.UI.WebControls.CustomValidator csvCOMMENTS;
		protected System.Web.UI.WebControls.Label capPRINTING_OPTIONS;
		protected System.Web.UI.WebControls.CheckBox chkPRINTING_OPTIONS;
		protected System.Web.UI.WebControls.Label capINSURED;
		protected System.Web.UI.WebControls.DropDownList cmbINSURED;
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
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnRollBack;
		protected Cms.CmsWeb.Controls.CmsButton btnPolicyDetails;
		protected Cms.CmsWeb.Controls.CmsButton btnPrintPreview;
		protected Cms.CmsWeb.Controls.CmsButton btnBackToSearch;
		protected Cms.CmsWeb.Controls.CmsButton btnBackToCustomerAssistant;
		protected Cms.CmsWeb.Controls.CmsButton btnGeneratePremiumNotice;
		protected Cms.CmsWeb.Controls.CmsButton btnComplete;
		protected Cms.CmsWeb.Controls.CmsButton btnGet_Premium;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.Controls.CmsButton btnGenerateReinstateDecPage;
		protected Cms.CmsWeb.Controls.CmsButton btnCommitToSpool;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnAssignLossCodes;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnUnAssignLossCodes;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOBID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidROW_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDisplayBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPROCESS_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidNEW_POLICY_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidUNDERWRITER;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidADD_INT_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_EFFECTIVE_DATE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_EXPIRY_DATE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_TERM;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_CANCEL_DATE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_TYPE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOLD_AGENCY;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_EFF_DATE;
		protected System.Web.UI.WebControls.RangeValidator rngEFFECTIVE_DATE;
		protected Cms.CmsWeb.Controls.CmsButton btnCommitInProgress;

		protected Cms.CmsWeb.Controls.CmsButton btnComitAynway;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnURStatus;
		protected System.Web.UI.HtmlControls.HtmlGenericControl myDIV;
		protected Cms.CmsWeb.Controls.CmsButton btnCommitAnywayInProgress;

		protected System.Web.UI.WebControls.Label capAUTO_ID_CARD;
		protected System.Web.UI.WebControls.DropDownList cmbAUTO_ID_CARD;
		protected System.Web.UI.WebControls.Label capNO_COPIES;
		protected System.Web.UI.WebControls.TextBox txtNO_COPIES;
		protected System.Web.UI.WebControls.RangeValidator rngNO_COPIES;
        protected System.Web.UI.WebControls.Label capHeader;
        protected System.Web.UI.WebControls.Label capMessage;
        protected System.Web.UI.WebControls.Label capTerm;
        protected System.Web.UI.WebControls.Label capPrintingOption;
        protected System.Web.UI.WebControls.Label capAdditionalInt;
        public string str;
        protected System.Web.UI.WebControls.TextBox txtPOLICY_TERMS;
		//bl object for interaction with business layer
		ClsRewriteProcess objProcess = new ClsRewriteProcess();
        protected String PoliyTermIn;
		private void Page_Load(object sender, System.EventArgs e)
		{
			#region Setting Screen ID
            Ajax.Utility.RegisterTypeForAjax(typeof(RewriteProcess));
			((cmsbase) this).ScreenId = "5000_21";
			#endregion
			#region Setting Attributes
			txtNEW_POLICY_TERM_EFFECTIVE_DATE.Attributes.Add("onblur","javascript:ShowExpirationDate();");
			btnReset.Attributes.Add("onclick","javascript:return formReset();");
			//txtEFFECTIVE_DATETIME.Attributes.Add("onBlur","javascript:return CallService();");
			cmbADD_INT.Attributes.Add("onChange","javascript:return cmbADD_INT_Change();");				
			chkSEND_ALL.Attributes.Add("onClick","javascript: chkSEND_ALL_Change();");
			btnSave.Attributes.Add("onClick","javascript: GetAssignAddInt();Page_ClientValidate();return Page_IsValid;");
			//btnComplete.Attributes.Add("onClick","javascript:GetAssignAddInt();HideShowCommitInProgress();Page_ClientValidate();return Page_IsValid;");
			btnComplete.Attributes.Add("onClick","javascript:GetAssignAddInt();Page_ClientValidate();return ConfirmCommit('');");
			btnComitAynway.Attributes.Add("onClick","javascript:GetAssignAddInt();Page_ClientValidate();return ConfirmCommit('ANYWAY');");
			hlkReinstateEffectiveDate.Attributes.Add("OnClick","fPopCalendar(document.REWRITE_PROCESS.txtEFFECTIVE_DATETIME,document.REWRITE_PROCESS.txtEFFECTIVE_DATETIME)"); //Javascript Implementation for Calender				
			hlkTermEffectiveDate.Attributes.Add("OnClick","fPopCalendar(document.REWRITE_PROCESS.txtNEW_POLICY_TERM_EFFECTIVE_DATE,document.REWRITE_PROCESS.txtNEW_POLICY_TERM_EFFECTIVE_DATE)");
			//cmbPRINT_COMMENTS.Attributes.Add("onchange","javascript:CommentEnable();");
			//cmbREQUESTED_BY.Attributes.Add("onchange","javascript:DisplayAgentPhoneNo();");
			btnPolicyDetails.Attributes.Add("onclick","javascript:return ShowDetailsPolicy();");
			btnReset.Attributes.Add("onclick","javascript:return formReset();");
			btnRollBack.Attributes.Add("onclick","javascript:HideShowCommit();");   
			cmbMERIDIEM.Attributes.Add("onchange","javascript:disableHourValidator();");//Added by Charles on 1-Sep-09 for Itrack 6323

			#endregion
			#region Setting Security.
			
			btnReset.CmsButtonClass					=	CmsButtonType.Write;
			btnReset.PermissionString				=	gstrSecurityXML;

			btnBackToSearch.CmsButtonClass					=	CmsButtonType.Write;
			btnBackToSearch.PermissionString				=	gstrSecurityXML;

			btnGenerateReinstateDecPage.CmsButtonClass					=	CmsButtonType.Write;
			btnGenerateReinstateDecPage.PermissionString				=	gstrSecurityXML;

			btnGeneratePremiumNotice.CmsButtonClass					=	CmsButtonType.Write;
			btnGeneratePremiumNotice.PermissionString				=	gstrSecurityXML;

			btnBackToCustomerAssistant.CmsButtonClass					=	CmsButtonType.Write;
			btnBackToCustomerAssistant.PermissionString				=	gstrSecurityXML;

			btnPrintPreview.CmsButtonClass					=	CmsButtonType.Write;
			btnPrintPreview.PermissionString				=	gstrSecurityXML;

			btnCommitToSpool.CmsButtonClass					=	CmsButtonType.Write;
			btnCommitToSpool.PermissionString				=	gstrSecurityXML;

			btnSave.CmsButtonClass					=	CmsButtonType.Write;
			btnSave.PermissionString				=	gstrSecurityXML;

			btnRollBack.CmsButtonClass 					=	CmsButtonType.Write;
			btnRollBack.PermissionString				=	gstrSecurityXML;

			btnComplete.CmsButtonClass					=	CmsButtonType.Write;
			btnComplete.PermissionString				=	gstrSecurityXML;

			btnPolicyDetails.CmsButtonClass = CmsButtonType.Read;
			btnPolicyDetails.PermissionString = gstrSecurityXML;

			btnGet_Premium.CmsButtonClass = CmsButtonType.Read;
			btnGet_Premium.PermissionString = gstrSecurityXML;
				
			btnCommitInProgress.CmsButtonClass = CmsButtonType.Read;
			btnCommitInProgress.PermissionString = gstrSecurityXML;

			btnComitAynway.CmsButtonClass = CmsButtonType.Write;
			btnComitAynway.PermissionString = gstrSecurityXML;

			btnCommitAnywayInProgress.CmsButtonClass = CmsButtonType.Read;
			btnCommitAnywayInProgress.PermissionString = gstrSecurityXML;

			#endregion

			btnComitAynway.Visible=false;
			spnURStatus.Visible=false;			
			myDIV.Visible=false;
			if (!Page.IsPostBack)
			{
				
				SetHiddenValues();
				FillDropDown();
				SetCaptions();
				SetErrorMessages();
                capHeader.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1226");
                capMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
                str = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1948");

                #region PolicyTop Control
                cltPolicyTop.CustomerID = Convert.ToInt32(Request.Params["CUSTOMER_ID"]);
				cltPolicyTop.PolicyID = Convert.ToInt32(Request.Params["POLICY_ID"]);
				cltPolicyTop.PolicyVersionID = Convert.ToInt32(Request.Params["policyVersionID"]);
				#endregion

				if (hidPROCESS_ID.Value == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_REWRITE_PROCESS.ToString())
				{
					objProcess.BeginTransaction();
					if (objProcess.CheckProcessEligibility(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value), int.Parse(hidPROCESS_ID.Value))==1)
					{
						//Starting the process
						StartProcess();
					}
					else
						//Populating the currently executing process information
						PopulateProcessInfo();
						objProcess.CommitTransaction();
				}
				else
				{
					//Populating the currently executing process information
					PopulateProcessInfo();
				}
				if (Convert.ToInt32(hidPROCESS_ID.Value)!= ClsPolicyProcess.POLICY_ROLLBACK_RENEWAL_PROCESS) 
					this.verifyRule(); 

				string JavascriptText="window.open('/cms/application/Aspx/Quote/Quote.aspx?CALLEDFROM=QUOTE_POL&CUSTOMER_ID=" + hidCUSTOMER_ID.Value  + "&POLICY_ID=" + hidPOLICY_ID.Value + "&POLICY_VERSION_ID=" + hidNEW_POLICY_VERSION_ID.Value  + "&LOBID=" + GetLOBID() + "&QUOTE_ID=" + "0" + "&ONLYEFFECTIVE=" + "false" + "&SHOW=" + "0" +  "','Quote','resizable=yes,scrollbars=yes,left=150,top=50,width=700,height=600');";
				btnGet_Premium.Attributes.Add("onClick",JavascriptText + "return false;");
				LoadData();
			}
            PoliyTermIn = ClsMessages.FetchGeneralMessage("1385");
		}

		private void SetDefaultValues()
		{
			chkSEND_ALL.Checked = true;
		}
		private bool verifyRule()
		{
			return verifyRule("");
		}

		private bool verifyRule(string CalledFrom)
		{
			try
			{
				string strRulesStatus="0";
				bool valid=false;	

				//string strRulesHTML = Cms.BusinessLayer.BlProcess.clsprocess.strHTML(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value),out valid,out strRulesStatus);
				if (hidNEW_POLICY_VERSION_ID.Value !="" && hidNEW_POLICY_VERSION_ID.Value!="0")
				{
					Cms.BusinessLayer.BlProcess.clsprocess objProcess = new clsprocess();

					objProcess.SystemID = CarrierSystemID; 
					string strRulesHTML = objProcess.strHTML(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidNEW_POLICY_VERSION_ID.Value),out valid,out strRulesStatus,"REW");
			
					if(valid && strRulesStatus == "0") // then commit
					{
						valid=true;
					}
					else
					{
						// show rules msg		
					
				
						// chk here for referred/rejected cases
						ChkReferedRejCaese(strRulesHTML,CalledFrom);
						//this.mySPAN.InnerHtml=strRulesHTML;
						myDIV.InnerHtml=strRulesHTML;
						myDIV.Visible=true;
						spnURStatus.Visible=true;
						valid=false;
					}
				}
				return valid;
			}
			catch(Exception objExp)
			{
				throw(objExp);
				//return false;
			}
		}
		private void ChkReferedRejCaese(string strRulesHTML)
		{
			ChkReferedRejCaese(strRulesHTML,"");
		}
		/// chk for application referred vs rejected cases
		private void ChkReferedRejCaese(string strRulesHTML,string strCalledFrom)
		{
			try
			{
				System.Xml.XmlDocument objXmlDocument = new XmlDocument();
				strRulesHTML= strRulesHTML.Replace("\t","");
				strRulesHTML= strRulesHTML.Replace("\r\n","");					
				strRulesHTML= strRulesHTML.Replace( "<LINK" ,"<!-- <LINK");				
				strRulesHTML= strRulesHTML.Replace( " rel=\"stylesheet\"> ","rel=\"stylesheet/\"> -->");
				strRulesHTML= strRulesHTML.Replace("<META http-equiv=\"Content-Type\" content=\"text/html; charset=utf-16\">","");					
				objXmlDocument.LoadXml("<RULEHTML>" +  strRulesHTML + "</RULEHTML>");
				
				//chk for referred
				
				XmlNodeList objXmlNodeList = objXmlDocument.GetElementsByTagName("ReferedStatus");
				XmlNodeList objXmlNodeListRej = objXmlDocument.GetElementsByTagName("returnValue");         
				if((objXmlNodeList != null && objXmlNodeList.Count>0) ||(objXmlNodeListRej != null && objXmlNodeListRej.Count>0) )
				{
					
					if(objXmlNodeListRej.Item(0).InnerText=="0")
					{
						btnComitAynway.Visible=false;
						if (strCalledFrom =="COMMIT")
							lblMessage.Text =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1218");//"Unable to commit process. Because Policy has been rejected as shown below." ;
						else
							lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1219");//"Policy has been rejected as shown below." ;
						lblMessage.Visible=true;
					}
					else if(objXmlNodeList.Item(0).InnerText=="0")
					{
						btnComitAynway.Visible=true;
                        if (strCalledFrom == "COMMIT")
                            lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1220");//"Unable to commit process. Because Policy has been referred as shown below." ;
                        else
                            lblMessage.Text = lblMessage.Text + "<br>"+ Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1223");//"<br> Policy has been referred as shown below." ;
						lblMessage.Visible=true;
					}				
				}
				else
				{
                    if (strCalledFrom == "COMMIT")
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1221");//"Unable to commit process. Please fill the mandatory information as shown below." ;
                    else
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1222");//"Please fill the mandatory information as shown below." ;
					lblMessage.Visible=true;
				}
				
			}
			catch(Exception ex)
			{
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1224")//"Following error occured. \n" 
					+ ex.Message + "\n Please try later.";
				lblMessage.Visible = true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
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
			this.btnRollBack.Click += new System.EventHandler(this.btnRollBack_Click);
			this.btnBackToSearch.Click += new System.EventHandler(this.btnBackToSearch_Click);
			this.btnBackToCustomerAssistant.Click += new System.EventHandler(this.btnBackToCustomerAssistant_Click);
			this.btnComplete.Click += new System.EventHandler(this.btnComplete_Click);
			this.btnComitAynway.Click += new System.EventHandler(this.btnComitAynway_Click);
			this.btnGet_Premium.Click += new System.EventHandler(this.btnGet_Premium_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		/// <summary>
		/// Fills the Drop Down Lists
		/// </summary>
		private void FillDropDown()
		{
			cmbMERIDIEM.Items.Insert(0,"AM");
			cmbMERIDIEM.Items[0].Value = "0";
			cmbMERIDIEM.Items.Insert(1,"PM");
			cmbMERIDIEM.Items[1].Value = "1";
		/*
			 cmbREQUESTED_BY.Items.Insert(0,"Agency");
			 cmbREQUESTED_BY.Items.Insert(1,"Wolverine");
			 cmbREQUESTED_BY.Items[0].Value = "0";
			 cmbREQUESTED_BY.Items[1].Value = "1";
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
			iListItemReq = cmbREQUESTED_BY.Items.FindByValue("6496"); //remove 
			if(iListItemReq!=null)
				cmbREQUESTED_BY.Items.Remove(iListItemReq);
			iListItemReq = cmbREQUESTED_BY.Items.FindByValue("6495"); //remove 
			if(iListItemReq!=null)
				cmbREQUESTED_BY.Items.Remove(iListItemReq);
			iListItemReq = cmbREQUESTED_BY.Items.FindByValue("6494"); //remove 
			if(iListItemReq!=null)
				cmbREQUESTED_BY.Items.Remove(iListItemReq);

			cmbPOLICY_TERMS.DataSource = ClsGeneralInformation.GetLOBTerms(Convert.ToInt32(hidLOBID.Value));
			cmbPOLICY_TERMS.DataTextField = "LOOKUP_VALUE_DESC";
			cmbPOLICY_TERMS.DataValueField = "LOOKUP_VALUE_CODE";
			cmbPOLICY_TERMS.DataBind();
			cmbPOLICY_TERMS.Items.Insert(0,"");
			
/*			cmbSAME_AGENCY.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("AGNCY");
			cmbSAME_AGENCY.DataTextField	= "LookupDesc";
			cmbSAME_AGENCY.DataValueField	= "LookupID";
			cmbSAME_AGENCY.DataBind();
			cmbSAME_AGENCY.Items.Insert(0,"");*/

			/*cmbANOTHER_AGENCY.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("AGNCY");
			cmbANOTHER_AGENCY.DataTextField	= "LookupDesc";
			cmbANOTHER_AGENCY.DataValueField	= "LookupID";
			cmbANOTHER_AGENCY.DataBind();
			cmbANOTHER_AGENCY.Items.Insert(0,"");  */

			//
			string  strSystemID			 = GetSystemId();
            string strCarrierSystemID = CarrierSystemID;//System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID").ToString();
			
			/*if ( strSystemID.Trim().ToUpper() != strCarrierSystemID.Trim().ToUpper())
			{
				DataSet objDataSet = Cms.BusinessLayer.BlCommon.ClsAgency.GetAgencyIDAndNameFromCode(strSystemID);

				if (objDataSet.Tables[0].Rows.Count > 0 )
				{

					cmbNEW_AGENCY.Items.Clear();
					cmbNEW_AGENCY.Items.Add(new ListItem(objDataSet.Tables[0].Rows[0]["AGENCY_DISP_NAME"].ToString(),objDataSet.Tables[0].Rows[0]["AGENCY_ID"].ToString()));
					cmbNEW_AGENCY.Items[0].Selected = true;
				}
				//cmbNEW_AGENCY.Enabled = false;
			}
			else
			{	// Carrier user so show all agencies
				Cms.BusinessLayer.BlCommon.ClsAgency.GetAgencyNamesInDropDown(cmbNEW_AGENCY);
			}
			
			cmbNEW_AGENCY.SelectedIndex =cmbNEW_AGENCY.Items.IndexOf(cmbNEW_AGENCY.Items.FindByValue(hidOLD_AGENCY.Value));      	*/
			/*if (hidLOBID.Value !="" &&  hidLOBID.Value!="0")
			{
				if(hidSTATE_ID.Value!="")
				{
					int stateId=Convert.ToInt32(hidSTATE_ID.Value);
					int selVal=int.Parse(hidLOBID.Value );
					if (selVal == 1) //For Homeowners.
					{	
						if(stateId==22)
						{
							cmbPOLICY_TYPE.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("HOPTPM");
						}
						else
						{
							cmbPOLICY_TYPE.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("HOPTYP");
						}
					}
					else if(selVal == 6) 	// For Rental Dwelling.
					{
						if(stateId==22)
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
					if (hidPOLICY_TYPE.Value !="" &&  hidPOLICY_TYPE.Value !="0")
						cmbPOLICY_TYPE.SelectedIndex =cmbPOLICY_TYPE.Items.IndexOf(cmbPOLICY_TYPE.Items.FindByValue(hidPOLICY_TYPE.Value));         
				}
			}*/
			  
			//
			/*cmbPRINT_COMMENTS.Items.Insert(0,"No");
			cmbPRINT_COMMENTS.Items[0].Value = "0";
			cmbPRINT_COMMENTS.Items.Insert(1,"Yes");
			cmbPRINT_COMMENTS.Items[1].Value = "1";*/
			
			IList ListSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("PRNTOP");
			ListItem iListItem = null;			
			if(hidLOBID.Value==((int)enumLOB.AUTOP).ToString() || hidLOBID.Value==((int)enumLOB.CYCL).ToString())
			{
				cmbAUTO_ID_CARD.DataSource=ListSource;
				cmbAUTO_ID_CARD.DataTextField="LookupDesc";
				cmbAUTO_ID_CARD.DataValueField="LookupID";
				cmbAUTO_ID_CARD.DataBind();		

				iListItem = cmbAUTO_ID_CARD.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS.DOWNLOAD).ToString());
				if(iListItem!=null)
					cmbAUTO_ID_CARD.Items.Remove(iListItem);
			}
			cmbINSURED.DataSource=ListSource;
			cmbINSURED.DataTextField="LookupDesc";
			cmbINSURED.DataValueField="LookupID";
			cmbINSURED.DataBind();
		/*
			iListItem = null;
			iListItem = cmbINSURED.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS.DOWNLOAD).ToString());
			if(iListItem!=null)
				cmbINSURED.Items.Remove(iListItem);
			iListItem = null;
			iListItem = cmbINSURED.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS.ON_DEMAND).ToString());
			if(iListItem!=null)
				cmbINSURED.Items.Remove(iListItem);
		*/

			//MICHIGAN_MAILERS #4068
//			iListItem = null;
//			iListItem = cmbINSURED.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS.MICHIGAN_MAILERS).ToString());
//			if(iListItem!=null)
//				cmbINSURED.Items.Remove(iListItem);
		

			cmbADD_INT.DataSource=ListSource;
			cmbADD_INT.DataTextField="LookupDesc";
			cmbADD_INT.DataValueField="LookupID";
			cmbADD_INT.DataBind();

			iListItem = null;
			iListItem = cmbADD_INT.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS.DOWNLOAD).ToString());
			if(iListItem!=null)
				cmbADD_INT.Items.Remove(iListItem);

			//MICHIGAN_MAILERS #4068
//			iListItem = null;
//			iListItem = cmbADD_INT.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS.MICHIGAN_MAILERS).ToString());
//			if(iListItem!=null)
//				cmbADD_INT.Items.Remove(iListItem);

			cmbAGENCY_PRINT.DataSource=ListSource;
			cmbAGENCY_PRINT.DataTextField="LookupDesc";
			cmbAGENCY_PRINT.DataValueField="LookupID";
			cmbAGENCY_PRINT.DataBind();

			//MICHIGAN_MAILERS #4068
//			iListItem = null;
//			iListItem = cmbAGENCY_PRINT.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS.MICHIGAN_MAILERS).ToString());
//			if(iListItem!=null)
//				cmbAGENCY_PRINT.Items.Remove(iListItem);

			DataTable dtAddIntList = null;
			Cms.BusinessLayer.BlApplication.ClsAdditionalInterest objAdditionalInterest = new Cms.BusinessLayer.BlApplication.ClsAdditionalInterest();
			dtAddIntList = objAdditionalInterest.GetAdditionalInterestList(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value),int.Parse(hidLOBID.Value));
			if(dtAddIntList!=null && dtAddIntList.Rows.Count>0)
			{
				cmbUnAssignAddInt.DataSource = dtAddIntList;
				cmbUnAssignAddInt.DataTextField = "ADD_INT_DETAILS";
				cmbUnAssignAddInt.DataValueField = "ADD_INT_ID";
				cmbUnAssignAddInt.DataBind();
			}
			
			//For Standard Letter 
			/*cmbSTD_LETTER_REQD.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbSTD_LETTER_REQD.DataTextField="LookupDesc";
			cmbSTD_LETTER_REQD.DataValueField="LookupID";
			cmbSTD_LETTER_REQD.DataBind();*/

			//For Customized Letter 
			/*cmbCUSTOM_LETTER_REQD.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbCUSTOM_LETTER_REQD.DataTextField="LookupDesc";
			cmbCUSTOM_LETTER_REQD.DataValueField="LookupID";
			cmbCUSTOM_LETTER_REQD.DataBind();*/

			//setting default values
			chkSEND_ALL.Checked =true; 
			if (hidPOLICY_CANCEL_DATE.Value!="" && hidPOLICY_CANCEL_DATE.Value!="0")
			{
				txtEFFECTIVE_DATETIME.Text = hidPOLICY_CANCEL_DATE.Value;
				txtNEW_POLICY_TERM_EFFECTIVE_DATE.Text =hidPOLICY_CANCEL_DATE.Value;  
			}
			else
			{
				txtEFFECTIVE_DATETIME.Text = hidPOLICY_EFFECTIVE_DATE.Value;
				txtNEW_POLICY_TERM_EFFECTIVE_DATE.Text =hidPOLICY_EFFECTIVE_DATE.Value;  
			}
			//cmbPOLICY_TERMS.SelectedIndex =cmbPOLICY_TERMS.Items.IndexOf(cmbPOLICY_TERMS.Items.FindByValue(hidPOLICY_TERM.Value));      
			
			//int polTerm=0,sNewMonth=0;
			//if (cmbPOLICY_TERMS.SelectedIndex!=-1)
			// polTerm = int.Parse(cmbPOLICY_TERMS.SelectedValue);
            

	        DateTime NewExpiryDate = Convert.ToDateTime(txtNEW_POLICY_TERM_EFFECTIVE_DATE.Text);
            if (hidPOLICY_TERM.Value != "")
            {
                int AddDays = int.Parse(hidPOLICY_TERM.Value);
                NewExpiryDate = NewExpiryDate.AddDays(AddDays);
            }
            //else 
            //{
            //    NewExpiryDate
            //}
            ////sNewMonth = NewExpiryDate.Month + polTerm;
            //sNewMonth = polTerm;
            //NewExpiryDate=NewExpiryDate.AddMonths(sNewMonth);
            string newCalculatedExpiryDate = NewExpiryDate.ToShortDateString(); //NewExpiryDate.Month.ToString("##") + "/" + NewExpiryDate.Day.ToString("##") + "/" + NewExpiryDate.Year.ToString("####");						
						 
			txtNEW_POLICY_TERM_EXPIRATION_DATE.Text =newCalculatedExpiryDate ;

		}
		/// <summary>
		/// Sets the Hidden Values.
		/// </summary>
		private void SetHiddenValues()
		{
			ClsReinstatementProcess objReinstate = new ClsReinstatementProcess();
			hidCUSTOMER_ID.Value =  Request.Params["CUSTOMER_ID"].ToString();
			hidPOLICY_ID.Value = Request.Params["POLICY_ID"].ToString();
			hidPOLICY_VERSION_ID.Value  = Request.Params["policyVersionID"].ToString();
			hidLOBID.Value = objReinstate.GetLOBID(Convert.ToInt32(hidCUSTOMER_ID.Value),Convert.ToInt32(hidPOLICY_ID.Value),Convert.ToInt32(hidPOLICY_VERSION_ID.Value)).ToString(); 
			//
			Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGeneralInformation = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
			DataSet dsPolicy = objGeneralInformation.GetPolicyDataSet(int.Parse(hidCUSTOMER_ID.Value==""?"0": hidCUSTOMER_ID.Value) ,int.Parse (hidPOLICY_ID.Value ==""?"0":hidPOLICY_ID.Value),int.Parse (hidPOLICY_VERSION_ID.Value==""?"0":hidPOLICY_VERSION_ID.Value));
			if(dsPolicy.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"]!=null && dsPolicy.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()!="")
				hidPOLICY_EFFECTIVE_DATE.Value =ConvertDBDateToCulture(dsPolicy.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()); 
			if(dsPolicy.Tables[0].Rows[0]["APP_EXPIRATION_DATE"]!=null && dsPolicy.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString()!="")
				hidPOLICY_EXPIRY_DATE.Value =ConvertDBDateToCulture(dsPolicy.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString()); 
			if(dsPolicy.Tables[0].Rows[0]["APP_TERMS"]!=null && dsPolicy.Tables[0].Rows[0]["APP_TERMS"].ToString()!="")
				hidPOLICY_TERM.Value = dsPolicy.Tables[0].Rows[0]["APP_TERMS"].ToString(); 
			if(dsPolicy.Tables[0].Rows[0]["POLICY_TYPE"]!=null && dsPolicy.Tables[0].Rows[0]["POLICY_TYPE"].ToString()!="")
				hidPOLICY_TYPE.Value = dsPolicy.Tables[0].Rows[0]["POLICY_TYPE"].ToString(); 
			if(dsPolicy.Tables[0].Rows[0]["STATE_ID"]!=null && dsPolicy.Tables[0].Rows[0]["STATE_ID"].ToString()!="")
				hidSTATE_ID.Value = dsPolicy.Tables[0].Rows[0]["STATE_ID"].ToString(); 
			if(dsPolicy.Tables[0].Rows[0]["AGENCY_ID"]!=null && dsPolicy.Tables[0].Rows[0]["AGENCY_ID"].ToString()!="")
				hidOLD_AGENCY.Value = dsPolicy.Tables[0].Rows[0]["AGENCY_ID"].ToString(); 
			if(dsPolicy.Tables[0].Rows[0]["UNDERWRITER"]!=null && dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString()!="" && dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString()!="0")
				hidUNDERWRITER.Value = dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString();
		
			dsPolicy.Clear();
			dsPolicy.Dispose(); 
			if (Request.Params["process"].ToString().ToUpper() == "REWRTE")  //REWRTE ,CREWRTE,RREWRTE
				hidPROCESS_ID.Value = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_REWRITE_PROCESS.ToString();
			else if (Request.Params["process"].ToString().ToUpper() == "CREWRTE")
			{
				hidPROCESS_ID.Value = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_REWRITE_PROCESS.ToString();
				btnRollBack.Attributes.Add("STYLE","DISPLAY:NONE");
			}
			else if (Request.Params["process"].ToString().ToUpper() == "RREWRTE")
			{
				hidPROCESS_ID.Value = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ROLLBACK_REWRITE_PROCESS.ToString();
				btnComplete.Attributes.Add("STYLE","DISPLAY:NONE");
				btnComitAynway.Attributes.Add("STYLE","DISPLAY:NONE");

			}
			//hidPOLICY_CANCEL_DATE.Value  = (string)(Cms.BusinessLayer.BlProcess.ClsReinstatementProcess.GetPolicyCancel_RenewedDate(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value))).ToString();            
            hidPOLICY_CANCEL_DATE.Value = Cms.BusinessLayer.BlProcess.ClsReinstatementProcess.GetPolicyCancel_RenewedDate(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value)).ToShortDateString();
		}

		/// <summary>
		/// Sets the process information 
		/// </summary>
		private void PopulateProcessInfo()
		{
			ClsPolicyProcess objPro = new ClsPolicyProcess();
			ClsProcessInfo objProcess = objPro.GetRunningProcess(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value));
			if(objProcess!=null)
			{
				hidPOLICY_VERSION_ID.Value = objProcess.POLICY_VERSION_ID.ToString();
				hidROW_ID.Value = objProcess.ROW_ID.ToString();
				hidNEW_POLICY_VERSION_ID.Value = objProcess.NEW_POLICY_VERSION_ID.ToString();
				//hidPOLICY_CANCEL_DATE.Value  = (string)(Cms.BusinessLayer.BlProcess.ClsReinstatementProcess.GetPolicyCancel_RenewedDate(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value))).ToString("MM/dd/yyyy");            
				//
				Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGeneralInformation = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
				DataSet dsPolicy;
				if (hidNEW_POLICY_VERSION_ID.Value !="" && hidNEW_POLICY_VERSION_ID.Value !="0")
					dsPolicy = objGeneralInformation.GetPolicyDataSet(int.Parse(hidCUSTOMER_ID.Value==""?"0": hidCUSTOMER_ID.Value) ,int.Parse (hidPOLICY_ID.Value ==""?"0":hidPOLICY_ID.Value),int.Parse (hidNEW_POLICY_VERSION_ID.Value==""?"0":hidNEW_POLICY_VERSION_ID.Value));
				else
					dsPolicy = objGeneralInformation.GetPolicyDataSet(int.Parse(hidCUSTOMER_ID.Value==""?"0": hidCUSTOMER_ID.Value) ,int.Parse (hidPOLICY_ID.Value ==""?"0":hidPOLICY_ID.Value),int.Parse (hidPOLICY_VERSION_ID.Value==""?"0":hidPOLICY_VERSION_ID.Value));
				if(dsPolicy.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"]!=null && dsPolicy.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()!="")
					hidPOLICY_EFFECTIVE_DATE.Value = ConvertDBDateToCulture(dsPolicy.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()); 
				if(dsPolicy.Tables[0].Rows[0]["APP_EXPIRATION_DATE"]!=null && dsPolicy.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString()!="")
                    hidPOLICY_EXPIRY_DATE.Value = ConvertDBDateToCulture(dsPolicy.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString()); 
				if(dsPolicy.Tables[0].Rows[0]["APP_TERMS"]!=null && dsPolicy.Tables[0].Rows[0]["APP_TERMS"].ToString()!="")
					hidPOLICY_TERM.Value = dsPolicy.Tables[0].Rows[0]["APP_TERMS"].ToString(); 
				if(dsPolicy.Tables[0].Rows[0]["POLICY_TYPE"]!=null && dsPolicy.Tables[0].Rows[0]["POLICY_TYPE"].ToString()!="")
					hidPOLICY_TYPE.Value = dsPolicy.Tables[0].Rows[0]["POLICY_TYPE"].ToString(); 
				if(dsPolicy.Tables[0].Rows[0]["STATE_ID"]!=null && dsPolicy.Tables[0].Rows[0]["STATE_ID"].ToString()!="")
					hidSTATE_ID.Value = dsPolicy.Tables[0].Rows[0]["STATE_ID"].ToString(); 
				if(dsPolicy.Tables[0].Rows[0]["AGENCY_ID"]!=null && dsPolicy.Tables[0].Rows[0]["AGENCY_ID"].ToString()!="")
					hidOLD_AGENCY.Value = dsPolicy.Tables[0].Rows[0]["AGENCY_ID"].ToString(); 
				if(dsPolicy.Tables[0].Rows[0]["UNDERWRITER"]!=null && dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString()!="" && dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString()!="0")
					hidUNDERWRITER.Value = dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString();
		
				dsPolicy.Clear();
				dsPolicy.Dispose(); 
				GetOldDataXml();

				//Saving the session and refreshing the menu
				SetPolicyInSession(objProcess.POLICY_ID, objProcess.NEW_POLICY_VERSION_ID, objProcess.CUSTOMER_ID);

				//Refresh the Policy Top.
				cltPolicyTop.PolicyID = objProcess.POLICY_ID;
				cltPolicyTop.PolicyVersionID = objProcess.NEW_POLICY_VERSION_ID;
				cltPolicyTop.CustomerID = objProcess.CUSTOMER_ID;
				cltPolicyTop.UseRequestVariables = false;
				cltPolicyTop.RefreshPolicy();
			}
			else
			{
				SetPolicyInSession(int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value), int.Parse(hidCUSTOMER_ID.Value));
				hidDisplayBody.Value = "False";
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1217");//"No Process in Progress on this Policy.";
				lblMessage.Visible=true;
			}
		}

		#region SetCaptions
		/// <summary>
		/// Sets the Captions
		/// </summary>
		private void SetCaptions()
		{
			
			System.Resources.ResourceManager objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Processes.RewriteProcess" ,System.Reflection.Assembly.GetExecutingAssembly());
			//capEFFECTIVE_DATETIME.Text =		objResourceMgr.GetString("txtEFFECTIVE_DATETIME"); //Commented by Charles on 31-Aug-09 for Itrack 6323
			capREQUESTED_BY.Text =		objResourceMgr.GetString("cmbREQUESTED_BY");
			capPOLICY_TERMS.Text =		objResourceMgr.GetString("cmbPOLICY_TERMS");
			capNEW_POLICY_TERM_EFFECTIVE_DATE.Text =		objResourceMgr.GetString("txtNEW_POLICY_TERM_EFFECTIVE_DATE");
			capNEW_POLICY_TERM_EXPIRATION_DATE.Text =		objResourceMgr.GetString("txtNEW_POLICY_TERM_EXPIRATION_DATE");

			//this.capCOMMENTS.Text			= objResourceMgr.GetString("txtCOMMENTS");
			this.capEFFECTIVE_DATETIME.Text  	= objResourceMgr.GetString("txtEFFECTIVE_DATETIME");
			this.capEFFECTIVE_TIME.Text		= objResourceMgr.GetString("txtEFFECTIVE_TIME");
			//this.capOTHER_REASON.Text		= objResourceMgr.GetString("txtOTHER_REASON");
			//this.capPRINT_COMMENTS.Text		= objResourceMgr.GetString("cmbPRINT_COMMENTS");
			//this.capREASON.Text				= objResourceMgr.GetString("cmbREASON");
			//this.capRETURN_PREMIUM.Text		= objResourceMgr.GetString("txtRETURN_PREMIUM");
			//this.capCUSTOM_LETTER_REQD.Text =	objResourceMgr.GetString("cmbCUSTOM_LETTER_REQD");
			//this.capSTD_LETTER_REQD.Text 	=	objResourceMgr.GetString("cmbSTD_LETTER_REQD");				
			capINSURED.Text						=	objResourceMgr.GetString("cmbINSURED");			
			capPRINTING_OPTIONS.Text			=	objResourceMgr.GetString("chkPRINTING_OPTIONS");													
			capADD_INT.Text						=	objResourceMgr.GetString("cmbADD_INT");			
			capSEND_ALL.Text					=	objResourceMgr.GetString("chkSEND_ALL");										
			capAGENCY_PRINT.Text				=	objResourceMgr.GetString("cmbAGENCY_PRINT");

			capNO_COPIES.Text					=	objResourceMgr.GetString("txtNO_COPIES");							
			capAUTO_ID_CARD.Text				=	objResourceMgr.GetString("cmbAUTO_ID_CARD");				 							
            btnReset.Text=Cms.CmsWeb.ClsMessages.GetButtonsText(base.ScreenId,"btnReset");
            btnRollBack.Text=Cms.CmsWeb.ClsMessages.GetButtonsText(base.ScreenId,"btnRollBack");
            btnPrintPreview.Text=Cms.CmsWeb.ClsMessages.GetButtonsText(base.ScreenId,"btnPrintPreview");
            btnBackToSearch.Text=Cms.CmsWeb.ClsMessages.GetButtonsText(base.ScreenId,"btnBackToSearch");
            btnBackToCustomerAssistant.Text=Cms.CmsWeb.ClsMessages.GetButtonsText(base.ScreenId,"btnBackToCustomerAssistant");
            btnGeneratePremiumNotice.Text=Cms.CmsWeb.ClsMessages.GetButtonsText(base.ScreenId,"btnGeneratePremiumNotice");
            btnComplete.Text=Cms.CmsWeb.ClsMessages.GetButtonsText(base.ScreenId,"btnComplete");
            btnCommitInProgress.Text = Cms.CmsWeb.ClsMessages.GetButtonsText(base.ScreenId,"btnCommitInProgress");
            btnGet_Premium.Text = Cms.CmsWeb.ClsMessages.GetButtonsText(base.ScreenId,"btnGet_Premium");
            btnGenerateReinstateDecPage.Text = Cms.CmsWeb.ClsMessages.GetButtonsText(base.ScreenId,"btnGenerateReinstateDecPage");
            btnSave.Text = Cms.CmsWeb.ClsMessages.GetButtonsText(base.ScreenId,"btnSave");
            btnCommitToSpool.Text = Cms.CmsWeb.ClsMessages.GetButtonsText(base.ScreenId,"btnCommitToSpool");
            capUnassignLossCodes.Text = objResourceMgr.GetString("capUnassignLossCodes");
            capAssignedLossCodes.Text = objResourceMgr.GetString("capAssignedLossCodes");
            capTerm.Text = objResourceMgr.GetString("capTerm");
            capPrintingOption.Text = objResourceMgr.GetString("capPrintingOption");
            capAdditionalInt.Text = objResourceMgr.GetString("capAdditionalInt");
            spnURStatus.InnerText = objResourceMgr.GetString("spnURStatus");
		}
		#endregion

		#region Set Validators ErrorMessages
		/// <summary>
		/// Sets the Error Messages.
		/// </summary>
		private void SetErrorMessages()
		{
			rfvEFFECTIVE_DATETIME.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"589");
			rfvPOLICY_TERMS.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"590");
			rfvNEW_POLICY_TERM_EXPIRATION_DATE.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"591");
			rfvNEW_POLICY_TERM_EFFECTIVE_DATE.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"589");

			revEFFECTIVE_DATETIME.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage("base.ScreenId","22");
			revNEW_POLICY_TERM_EFFECTIVE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage("base.ScreenId","22");
			revEFFECTIVE_DATETIME.ValidationExpression =aRegExpDate;
			revNEW_POLICY_TERM_EFFECTIVE_DATE.ValidationExpression=aRegExpDate;
			//
			rfvEFFECTIVE_HOUR.ErrorMessage			=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1213");//"Please insert Hours.";
			rfvEFFECTIVE_MINUTE.ErrorMessage		=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1214");//"Please insert Minutes.";

            rfvMERIDIEM.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1215");//"Please select AM/PM.";
			//rfvOTHER_REASON.ErrorMessage			="Please insert Reason description.";
			//rfvREASON.ErrorMessage					="Please select Reason.";	
			//this.csvCOMMENTS.ErrorMessage			= "Please enter only 250 characters.";
			//this.csvOTHER_REASON.ErrorMessage		= "Please enter only 250 characters.";
			//rfvCOMMENTS.ErrorMessage				= "Please enter comments.";
			//revRETURN_PREMIUM.ValidationExpression	= aRegExpDoublePositiveNonZero;
			//revRETURN_PREMIUM.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"116");
			rngEFFECTIVE_DATE.MinimumValue =  Convert.ToDateTime(hidPOLICY_CANCEL_DATE.Value).ToString("d"); 
			//rngEFFECTIVE_DATE.MaximumValue = Convert.ToDateTime(hidPOLICY_EXPIRY_DATE.Value).ToString("d");
			rngEFFECTIVE_DATE.MaximumValue = Convert.ToDateTime("1/1/3000").ToString("d");
			rngEFFECTIVE_DATE.ErrorMessage  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1216");//"Date should be greater or equal to Policy cancellation Date.";
			rngNO_COPIES.ErrorMessage					= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("217");
            rnvEFFECTIVE_HOUR.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("771");
            rnvtEFFECTIVE_MINUTE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("772");
		}
		#endregion
		/// <summary>
		/// Launch the Reinstatement Process.
		/// </summary>
		private void StartProcess()
		{
			try
			{
				Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo = new Cms.Model.Policy.Process.ClsProcessInfo();
				objProcessInfo = GetFormValues();

				objProcessInfo.PROCESS_ID = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_REWRITE_PROCESS;
				objProcessInfo.CREATED_BY = int.Parse(GetUserId());
				objProcessInfo.CREATED_DATETIME = DateTime.Now; 
								
				if (objProcess.StartProcess(objProcessInfo) == true)
				{ 
					hidFormSaved.Value = "1";
					//saved successfully
					hidROW_ID.Value = objProcessInfo.ROW_ID.ToString();
					hidPOLICY_VERSION_ID.Value = objProcessInfo.POLICY_VERSION_ID.ToString(); 
					hidDisplayBody.Value = "True";
					// update Term Effective Expiry date
					objProcess.BeginTransaction();
					objProcess.UpdatePolicyTerm_EffectiveExpirydate(objProcessInfo);
					objProcess.CommitTransaction();	

					lblMessage.Text = ClsMessages.FetchGeneralMessage("899");
					//Generating the xml of old data
					GetOldDataXml();
				}
				else
				{
					hidDisplayBody.Value = "False";
					lblMessage.Text = ClsMessages.FetchGeneralMessage("594");

					//Hiding the extra buttons
					HideButtons();
				}
				
				//Saving the session and refreshing the menu
				hidNEW_POLICY_VERSION_ID.Value = objProcessInfo.NEW_POLICY_VERSION_ID.ToString();
				SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
				
				//Refresh the Policy Top.
				cltPolicyTop.PolicyID = objProcessInfo.POLICY_ID;
				cltPolicyTop.PolicyVersionID = objProcessInfo.NEW_POLICY_VERSION_ID;
				cltPolicyTop.CustomerID = objProcessInfo.CUSTOMER_ID;
				cltPolicyTop.UseRequestVariables = false;
				cltPolicyTop.RefreshPolicy();

				lblMessage.Visible = true;
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
						cmbMERIDIEM.SelectedIndex= 1;
					}
					else 
						cmbMERIDIEM.SelectedIndex= 0;
					
					txtEFFECTIVE_HOUR.Text =   hour.ToString();

					txtEFFECTIVE_MINUTE.Text = Convert.ToDateTime(dsTemp.Tables[0].Rows[0]["EFFECTIVETIME"]).Minute.ToString();
                    txtPOLICY_TERMS.Text = dsTemp.Tables[0].Rows[0]["APP_TERMS"].ToString();
                    
                    //cmbPOLICY_TERMS.SelectedIndex =cmbPOLICY_TERMS.Items.IndexOf(cmbPOLICY_TERMS.Items.FindByValue(dsTemp.Tables[0].Rows[0]["APP_TERMS"].ToString()));  
					//cmbPOLICY_TERMS.Enabled = false;

					if (dsTemp.Tables[0].Rows[0]["EFFECTIVE_DATETIME"] != DBNull.Value)
					{
						txtEFFECTIVE_DATETIME.Text = ConvertDBDateToCulture(dsTemp.Tables[0].Rows[0]["EFFECTIVE_DATETIME"].ToString());
					}

				}

			}
		}
		/// <summary>
		/// Get the Form Values.
		/// </summary>
		/// <returns></returns>
		private Cms.Model.Policy.Process.ClsProcessInfo GetFormValues()
		{
			Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo = new Cms.Model.Policy.Process.ClsProcessInfo();

			objProcessInfo.CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);
			objProcessInfo.POLICY_ID = int.Parse(hidPOLICY_ID.Value);
			objProcessInfo.POLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value);
			
			objProcessInfo.NEW_CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);
			objProcessInfo.NEW_POLICY_ID = int.Parse(hidPOLICY_ID.Value);
			
			if (hidNEW_POLICY_VERSION_ID != null && hidNEW_POLICY_VERSION_ID.Value != "")
				objProcessInfo.NEW_POLICY_VERSION_ID = int.Parse(hidNEW_POLICY_VERSION_ID.Value);
			else
				objProcessInfo.NEW_POLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value);

			objProcessInfo.POLICY_CURRENT_STATUS = objProcess.GetPolicyStatus(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.NEW_POLICY_VERSION_ID);  //GetPolicyStatus();
			objProcessInfo.POLICY_PREVIOUS_STATUS = objProcess.GetPolicyStatus(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.POLICY_VERSION_ID);  //GetPolicyStatus();
			
			
			objProcessInfo.PROCESS_ID = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_REWRITE_PROCESS; 
			if (txtEFFECTIVE_DATETIME.Text != "")
				objProcessInfo.EFFECTIVE_DATETIME = Convert.ToDateTime(txtEFFECTIVE_DATETIME.Text);
			
			if (cmbREQUESTED_BY.SelectedValue != null && cmbREQUESTED_BY.SelectedValue  != "")
				objProcessInfo.REQUESTED_BY = Convert.ToInt32(cmbREQUESTED_BY.SelectedValue);
            if (txtPOLICY_TERMS.Text != "")
                objProcessInfo.POLICY_TERMS = Convert.ToInt32(txtPOLICY_TERMS.Text);    //if (cmbPOLICY_TERMS.SelectedValue != null && cmbPOLICY_TERMS.SelectedValue != "")
            //    objProcessInfo.POLICY_TERMS = Convert.ToInt32(cmbPOLICY_TERMS.SelectedValue);
			else
				objProcessInfo.POLICY_TERMS = int.Parse(hidPOLICY_TERM.Value);

			if (txtNEW_POLICY_TERM_EFFECTIVE_DATE.Text != null &&  txtNEW_POLICY_TERM_EFFECTIVE_DATE.Text != "")
				objProcessInfo.NEW_POLICY_TERM_EFFECTIVE_DATE = Convert.ToDateTime(txtNEW_POLICY_TERM_EFFECTIVE_DATE.Text);
					
			if (txtNEW_POLICY_TERM_EXPIRATION_DATE.Text != null &&  txtNEW_POLICY_TERM_EXPIRATION_DATE.Text != "")
				objProcessInfo.NEW_POLICY_TERM_EXPIRATION_DATE = Convert.ToDateTime(txtNEW_POLICY_TERM_EXPIRATION_DATE.Text);
			//
			/*
			if (cmbPRINT_COMMENTS.SelectedItem != null)
				objProcessInfo.PRINT_COMMENTS = cmbPRINT_COMMENTS.SelectedValue;
			if (cmbPRINT_COMMENTS.SelectedValue != "0")
				objProcessInfo.COMMENTS = txtCOMMENTS.Text;
			else
				objProcessInfo.COMMENTS = "";
				*/
			if(cmbAGENCY_PRINT.SelectedItem!=null && cmbAGENCY_PRINT.SelectedItem.Value!="")
				objProcessInfo.AGENCY_PRINT = int.Parse(cmbAGENCY_PRINT.SelectedItem.Value);
			
			if(chkPRINTING_OPTIONS.Checked)
				objProcessInfo.PRINTING_OPTIONS = (int)(enumYESNO_LOOKUP_CODE.YES);
			else
				objProcessInfo.PRINTING_OPTIONS = (int)(enumYESNO_LOOKUP_CODE.NO);
			if(cmbINSURED.SelectedItem!=null && cmbINSURED.SelectedItem.Value!="")
				objProcessInfo.INSURED = int.Parse(cmbINSURED.SelectedItem.Value);

			objProcessInfo.CREATED_BY = objProcessInfo.COMPLETED_BY = int.Parse(GetUserId()); 
			objProcessInfo.CREATED_DATETIME = objProcessInfo.COMPLETED_DATETIME = System.DateTime.Now;

			if(hidUNDERWRITER.Value!="" && hidUNDERWRITER.Value!="0")
				objProcessInfo.UNDERWRITER = int.Parse(hidUNDERWRITER.Value);

			if(hidSTATE_ID.Value!="" && hidSTATE_ID.Value!="0")
				objProcessInfo.STATE_ID = int.Parse(hidSTATE_ID.Value);

			if(hidLOBID.Value!="" && hidLOBID.Value!="0")
				objProcessInfo.LOB_ID = int.Parse(hidLOBID.Value);
			
			if(cmbADD_INT.SelectedItem!=null && cmbADD_INT.SelectedItem.Value!="")
			{
				objProcessInfo.ADD_INT = int.Parse(cmbADD_INT.SelectedItem.Value);
				//#4068
				if(objProcessInfo.ADD_INT==int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL).ToString())
					|| objProcessInfo.ADD_INT==int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.MICHIGAN_MAILERS).ToString()))
				{
					objProcessInfo.ADD_INT_ID = hidADD_INT_ID.Value;
					if(chkSEND_ALL.Checked==true)
						objProcessInfo.SEND_ALL = (int)(enumYESNO_LOOKUP_CODE.YES);
					else
						objProcessInfo.SEND_ALL = (int)(enumYESNO_LOOKUP_CODE.NO);
				}
			}
			if(hidOldData.Value=="" || hidOldData.Value=="0")
			{
				if(hidLOBID.Value==((int)enumLOB.AUTOP).ToString() || hidLOBID.Value==((int)enumLOB.CYCL).ToString())
				{
					objProcessInfo.AUTO_ID_CARD = int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL).ToString());					
				}
			}
			else
			{
				if(hidLOBID.Value==((int)enumLOB.AUTOP).ToString() || hidLOBID.Value==((int)enumLOB.CYCL).ToString())
				{
					if(cmbAUTO_ID_CARD.SelectedItem!=null && cmbAUTO_ID_CARD.SelectedItem.Value!="")
						objProcessInfo.AUTO_ID_CARD = int.Parse(cmbAUTO_ID_CARD.SelectedItem.Value);
				}
			}
			if(hidLOBID.Value==((int)enumLOB.AUTOP).ToString() || hidLOBID.Value==((int)enumLOB.CYCL).ToString())
			{
				if(txtNO_COPIES.Text.Trim()!="")
					objProcessInfo.NO_COPIES = int.Parse(txtNO_COPIES.Text.Trim());
				else
					objProcessInfo.NO_COPIES = Cms.BusinessLayer.BlCommon.ClsCommon.PRINT_OPTIONS_AUTO_CYCL_NO_OF_COPIES;
			}
			//
			//DateTime EffDate = new DateTime(

			if (txtEFFECTIVE_DATETIME.Text != "")
			{
				DateTime EffDate = ConvertToDate(txtEFFECTIVE_DATETIME.Text);
				
				int Hr = int.Parse(txtEFFECTIVE_HOUR.Text);
				
				if(Hr==12 && cmbMERIDIEM.SelectedIndex==0) //If added by Charles on 31-Aug-09 for Itrack 6323
				{
					Hr=00;
				}
				if(cmbMERIDIEM.SelectedIndex == 1 && Hr!=12 ) //Added condition Hr!=12 by Charles on 31-Aug-09 for Itrack 6323
				{
					Hr+=12;
				}
				/*if(Hr==24) //Commented by Charles on 31-Aug-09 for Itrack 6323
				{
					Hr=00;
				}*/

				objProcessInfo.EFFECTIVE_DATETIME = new DateTime(EffDate.Year, EffDate.Month, EffDate.Day
					, Hr, int.Parse(txtEFFECTIVE_MINUTE.Text), 0);

				objProcessInfo.EFFECTIVE_TIME= objProcessInfo.EFFECTIVE_DATETIME.ToLongTimeString();
			}
			//if(txtRETURN_PREMIUM.Text.Trim()!="")
			//	objProcessInfo.RETURN_PREMIUM = Convert.ToDouble(txtRETURN_PREMIUM.Text.Trim());			
		
			//if(cmbREASON.SelectedItem != null && cmbREASON.SelectedValue != "")
			//	objProcessInfo.REASON = int.Parse(cmbREASON.SelectedValue);
			
			//objProcessInfo.OTHER_REASON = txtOTHER_REASON.Text;
			
			/*if(cmbCUSTOM_LETTER_REQD.SelectedItem!=null && cmbCUSTOM_LETTER_REQD.SelectedItem.Value!="")
				objProcessInfo.CUSTOM_LETTER_REQD = int.Parse(cmbCUSTOM_LETTER_REQD.SelectedItem.Value);*/

			/*if(cmbSTD_LETTER_REQD.SelectedItem!=null && cmbSTD_LETTER_REQD.SelectedItem.Value!="")
				objProcessInfo.STD_LETTER_REQD = int.Parse(cmbSTD_LETTER_REQD.SelectedItem.Value);
			if (cmbSAME_AGENCY.SelectedValue !="")
				objProcessInfo.SAME_AGENCY=int.Parse(cmbSAME_AGENCY.SelectedValue);
			if (cmbANOTHER_AGENCY.SelectedValue!="")
				objProcessInfo.ANOTHER_AGENCY=int.Parse(cmbANOTHER_AGENCY.SelectedValue); 
			if (cmbPOLICY_TYPE.SelectedValue !="" && cmbPOLICY_TYPE.SelectedItem !=null)
				objProcessInfo.POLICY_TYPE =int.Parse(cmbPOLICY_TYPE.SelectedValue);  
			if (cmbNEW_AGENCY.SelectedValue !="")
				objProcessInfo.NEW_AGENCY=int.Parse(cmbNEW_AGENCY.SelectedValue);    */

			return objProcessInfo;
		}
		/// <summary>
		/// Fetch the Details
		/// </summary>
		private void GetOldDataXml()
		{
			if (hidROW_ID.Value.Trim() != "")
			{
				hidOldData.Value = objProcess.GetOldDataXml(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value),
					int.Parse(hidPOLICY_VERSION_ID.Value), int.Parse(hidROW_ID.Value));

                #region remove Node from XML
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(hidOldData.Value);
                XmlNode xNodeMAIN = xDoc.SelectSingleNode("NewDataSet/Table");
                if (xNodeMAIN != null)
                {
                    XmlNode xNode = xNodeMAIN.SelectSingleNode("EFFECTIVE_DATETIME"); //xDoc.NextSibling.SelectSingleNode("RETURN_PREMIUM");
                    if (xNode != null)
                    {
                        xNodeMAIN.RemoveChild(xNode);
                        //hidOldData.Value = xDoc.InnerXml.ToString();
                    }
                    XmlNode xNode1 = xNodeMAIN.SelectSingleNode("NEW_POLICY_TERM_EXPIRATION_DATE"); //xDoc.NextSibling.SelectSingleNode("RETURN_PREMIUM");
                    if (xNode1 != null)
                    {
                        xNodeMAIN.RemoveChild(xNode1);
                        //hidOldData.Value = xDoc.InnerXml.ToString();
                    }
                    XmlNode xNode2 = xNodeMAIN.SelectSingleNode("NEW_POLICY_TERM_EFFECTIVE_DATE"); //xDoc.NextSibling.SelectSingleNode("RETURN_PREMIUM");
                    if (xNode2 != null)
                    {
                        xNodeMAIN.RemoveChild(xNode2);
                       
                    }
                    hidOldData.Value = xDoc.InnerXml.ToString();
                }

                #endregion
			}
		}
		/// <summary>
		/// Update the details for Reinstatement Process.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo = new Cms.Model.Policy.Process.ClsProcessInfo();
				
				objProcessInfo = GetFormValues();
				objProcessInfo.ROW_ID = int.Parse(hidROW_ID.Value);

				//Making model object which will contains old data
				ClsProcessInfo objOldProcessInfo = new ClsProcessInfo();
				base.PopulateModelObject(objOldProcessInfo, hidOldData.Value);


				DateTime EffDateOld = objOldProcessInfo.EFFECTIVE_DATETIME;
				DateTime EffTimeOld = DateTime.MinValue;
				try { EffTimeOld = Convert.ToDateTime(objOldProcessInfo.EFFECTIVE_TIME); }
				catch(Exception){}
				objOldProcessInfo.EFFECTIVE_TIME= EffTimeOld.ToLongTimeString();

				DateTime EffTime = DateTime.MinValue;
				try { EffTime = Convert.ToDateTime(objProcessInfo.EFFECTIVE_DATETIME); }
				catch(Exception){}
				objOldProcessInfo.EFFECTIVE_DATETIME = new DateTime(EffDateOld.Year, EffDateOld.Month, EffDateOld.Day
					, EffTime.Hour, EffTime.Minute
					, 0);

				//Updating the previous endorsement record
				objProcess.BeginTransaction(); 
				objProcess.UpdateProcessInformation(objOldProcessInfo, objProcessInfo);
				//update policy term effective date and expiry date
				objProcess.UpdatePolicyTerm_EffectiveExpirydate(objProcessInfo);  
				objProcess.CommitTransaction();
                hidPOLICY_EFF_DATE.Value = txtEFFECTIVE_DATETIME.Text;				
	
				lblMessage.Text		= ClsMessages.FetchGeneralMessage("31");
				lblMessage.Visible	= true;
				hidFormSaved.Value = "1";
				LoadData();
				GetOldDataXml();//Done for Itrack Issue 5694 on 15 April 2009
				//Refresh the Policy Top.
				cltPolicyTop.CallPageLoad();
				SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
				//Refresh the Policy Top.
				cltPolicyTop.PolicyID = objProcessInfo.POLICY_ID;
				cltPolicyTop.PolicyVersionID = objProcessInfo.NEW_POLICY_VERSION_ID;
				cltPolicyTop.CustomerID = objProcessInfo.CUSTOMER_ID;
				cltPolicyTop.UseRequestVariables = false;
				cltPolicyTop.RefreshPolicy();


			}
			catch(Exception objExp)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
				lblMessage.Text = objExp.Message.ToString();
				lblMessage.Visible = true;
				objProcess.RollbackTransaction();
			}
			
		}



		/// <summary>
		/// Hides the commit and rollback button
		/// </summary>
		private void HideButtons()
		{
			btnSave.Attributes.Add("style","display:none");
			btnComplete.Attributes.Add("style","display:none");
			btnRollBack.Attributes.Add("style","display:none");
			btnCommitInProgress.Attributes.Add("style","display:none");

		}
		private void btnRollBack_Click(object sender, System.EventArgs e)
		{
			//Local Variable Declartions
			ClsProcessInfo objProcessInfo = new ClsProcessInfo();

			try
			{
				

				objProcessInfo = objProcess.GetRunningProcess (int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value));
				
				objProcessInfo.PROCESS_ID = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ROLLBACK_REWRITE_PROCESS;
				objProcessInfo.ROW_ID = int.Parse(hidROW_ID.Value); 
				objProcessInfo.COMPLETED_BY = int.Parse(GetUserId());
				objProcessInfo.COMPLETED_DATETIME = DateTime.Now;	

				
							
				if (objProcess.RollbackProcess(objProcessInfo) == true)
				{ 
					
					hidFormSaved.Value = "1";
					hidDisplayBody.Value = "False";
					lblMessage.Text = ClsMessages.FetchGeneralMessage("901");
					GetOldDataXml();
					hidNEW_POLICY_VERSION_ID.Value ="";
					//Hiding the extra buttons
					LoadData();
					HideButtons();
				}
				else
				{
					hidDisplayBody.Value = "False";
					lblMessage.Text = ClsMessages.FetchGeneralMessage("602");
				}

				lblMessage.Visible = true;

				//Refresh the Policy Top.
				cltPolicyTop.CallPageLoad();

				//Saving the session and refreshing the menu
				SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);

				//Refresh the Policy Top.
				cltPolicyTop.PolicyID = objProcessInfo.POLICY_ID;
				cltPolicyTop.PolicyVersionID = objProcessInfo.POLICY_VERSION_ID;
				cltPolicyTop.CustomerID = objProcessInfo.CUSTOMER_ID;
				cltPolicyTop.UseRequestVariables = false;
				cltPolicyTop.RefreshPolicy();

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
		private void btnComitAynway_Click(object sender, System.EventArgs e)
		{
			try
			{
				ClsRewriteProcess objProcess = new ClsRewriteProcess();

				//ClsProcessInfo objProcessInfo = objProcess.GetRunningProcess (int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value));
				ClsProcessInfo objProcessInfo =GetFormValues();
				objProcessInfo.PROCESS_ID = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_REWRITE_PROCESS;
				objProcessInfo.ROW_ID = int.Parse(hidROW_ID.Value); 
				objProcessInfo.COMPLETED_BY = int.Parse(GetUserId());
				objProcessInfo.COMPLETED_DATETIME = DateTime.Now;
				objProcessInfo.CREATED_BY = int.Parse(GetUserId());
				// update Term Effective Expiry date
				objProcess.BeginTransaction();
				objProcess.UpdatePolicyTerm_EffectiveExpirydate(objProcessInfo);
				objProcess.CommitTransaction();	

				//Commiting the process 
				if (objProcess.CommitProcess (objProcessInfo,"COMMITANYWAY") == true)
				{
					
					//Committed successfully
					hidFormSaved.Value = "1";
					hidDisplayBody.Value = "True";
					lblMessage.Text = ClsMessages.FetchGeneralMessage("900");
					GetOldDataXml();
					
					//Hiding the buttons
					HideButtons();

					//Updating the policy top,session and menus
					SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
				}
				else
				{
					//Error occured
					hidDisplayBody.Value = "False";
					//					lblMessage.Text = ClsMessages.FetchGeneralMessage("601");
					lblMessage.Text = Cms.BusinessLayer.BlProcess.ClsPolicyErrMsg.strMessage;
				}

				hidPOLICY_EFF_DATE.Value = txtEFFECTIVE_DATETIME.Text;	
				
				lblMessage.Visible = true;
				//Refresh the Policy Top.
				cltPolicyTop.CallPageLoad();
				//Updating the policy top,session and menus
				SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);

				//Refresh the Policy Top.
				cltPolicyTop.PolicyID = objProcessInfo.POLICY_ID;
				cltPolicyTop.PolicyVersionID = objProcessInfo.NEW_POLICY_VERSION_ID;
				cltPolicyTop.CustomerID = objProcessInfo.CUSTOMER_ID;
				cltPolicyTop.UseRequestVariables = false;
				cltPolicyTop.RefreshPolicy();
			
			}
			catch(Exception objExp)
			{
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1225")//"Following error occured while commiting process. \n" 
					+ objExp.Message  ; //+ "\n Please try later.";
				lblMessage.Visible = true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
				if(hidNEW_POLICY_VERSION_ID.Value !="") 
					SetPolicyInSession(int.Parse(hidPOLICY_ID.Value),int.Parse(hidNEW_POLICY_VERSION_ID.Value) , int.Parse(hidCUSTOMER_ID.Value));
				else
					SetPolicyInSession(int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value) , int.Parse(hidCUSTOMER_ID.Value));
				//Refresh the Policy Top.
				cltPolicyTop.PolicyID = int.Parse(hidPOLICY_ID.Value);
				if(hidNEW_POLICY_VERSION_ID.Value !="") 
					cltPolicyTop.PolicyVersionID =int.Parse(hidNEW_POLICY_VERSION_ID.Value);
				else
					cltPolicyTop.PolicyVersionID = int.Parse(hidPOLICY_VERSION_ID.Value) ;

				cltPolicyTop.CustomerID = int.Parse(hidCUSTOMER_ID.Value);
				cltPolicyTop.UseRequestVariables = false;
				cltPolicyTop.RefreshPolicy();

			
			}
		}
		private void btnComplete_Click(object sender, System.EventArgs e)
		{
			

			ClsProcessInfo objProcessInfo = new ClsProcessInfo();
			ClsPolicyProcess objPolicyProcess = new ClsPolicyProcess();

			bool valid=false;	
			valid=this.verifyRule("COMMIT"); 
			if(!valid)
			{
				return;
			}
			else
			{

				try
				{
				
					//objProcessInfo = objProcess.GetRunningProcess (int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value));
					objProcessInfo =GetFormValues();
					objProcessInfo.ROW_ID = int.Parse(hidROW_ID.Value); 
					objProcessInfo.PROCESS_ID = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_REWRITE_PROCESS;
					objProcessInfo.CREATED_BY = objProcessInfo.COMPLETED_BY = int.Parse(GetUserId());
					objProcessInfo.COMPLETED_DATETIME = DateTime.Now;	


					//Add By kranti for update Term Effective Expiry date
					objProcess.BeginTransaction();
					objProcess.UpdatePolicyTerm_EffectiveExpirydate(objProcessInfo);
					objProcess.CommitTransaction();				


					if (objProcess.CommitProcess(objProcessInfo) == true)
					{ 
					
						hidFormSaved.Value = "1";
						hidDisplayBody.Value = "True";
						if (ClsPolicyProcess.PrintingErrorFlag ==true)	
							lblMessage.Text = ClsPolicyErrMsg.strMessage;
						else
							lblMessage.Text = ClsMessages.FetchGeneralMessage("900");
						GetOldDataXml();
						//Hiding the extra buttons
						LoadData();
						HideButtons();
				
					}
					else
					{
						hidDisplayBody.Value = "False";
						lblMessage.Text = ClsMessages.FetchGeneralMessage("601") + "<br>" + ClsPolicyErrMsg.strMessage ;
						btnComplete.Attributes.Add("style","display:inline");
						btnCommitInProgress.Attributes.Add("style","display:none");

					}
					lblMessage.Visible = true;
					hidPOLICY_EFF_DATE.Value = txtEFFECTIVE_DATETIME.Text;	
					//Refresh the Policy Top.
					cltPolicyTop.CallPageLoad();

					//Updating the policy top,session and menus
					SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);

					//Refresh the Policy Top.
					cltPolicyTop.PolicyID = objProcessInfo.POLICY_ID;
					cltPolicyTop.PolicyVersionID = objProcessInfo.NEW_POLICY_VERSION_ID;
					cltPolicyTop.CustomerID = objProcessInfo.CUSTOMER_ID;
					cltPolicyTop.UseRequestVariables = false;
					cltPolicyTop.RefreshPolicy();

				}				
				catch(Exception objExp)
				{
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
					lblMessage.Text = objExp.Message.ToString();
					lblMessage.Visible = true;
			
				
					//Added by kranti 26 April 2007	
	
					//Hiding the extra buttons
					HideButtons();

					//Updating the policy top,session and menus
					SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);

					//Refresh the Policy Top.
					cltPolicyTop.PolicyID = objProcessInfo.POLICY_ID;
					cltPolicyTop.PolicyVersionID = objProcessInfo.NEW_POLICY_VERSION_ID;
					cltPolicyTop.CustomerID = objProcessInfo.CUSTOMER_ID;
					cltPolicyTop.UseRequestVariables = false;
					cltPolicyTop.RefreshPolicy();

				}
				finally
				{
					if (objProcess != null)
					{
						objProcess.Dispose();				
					}
				}
			}
		}
	

		private void btnBackToSearch_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("/cms/client/aspx/CustomerManagerSearch.aspx");
		}

		private void btnBackToCustomerAssistant_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("/cms/client/aspx/CustomerManagerIndex.aspx?Customer_ID=" + hidCUSTOMER_ID.Value);
		}
		/// <summary>
		/// Get premium
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnGet_Premium_Click(object sender, System.EventArgs e)
		{
			try 
			{			
				base.GeneratePolicyQuote(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value));
			}
			catch(Exception objExp)
			{
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1224")//"Following error occured. \n" 
					+ objExp.Message + "\n Please try later.";
				lblMessage.Visible = true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
			}
			LoadData();
		}

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public string GetExpDate(string sAPP_TERMS, string sAPP_EFFECTIVE_DATE)
        {
            try
            {
                SetCultureThread(GetLanguageCode());
                return ConvertToDate(sAPP_EFFECTIVE_DATE).AddDays(int.Parse(sAPP_TERMS)).ToShortDateString();
            }
            catch
            {
                return "";
            }
        }
	}
}
