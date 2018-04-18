/******************************************************************************************
<Author				: -  Mohit Agarwal
<Start Date			: -	 16 Jan.,2007
<End Date			: -	 
<Description		: -  Class for Policy Corrective User Process.
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
using Cms.BusinessLayer.BlProcess;
using Cms.Model.Policy.Process;
using Cms.CmsWeb;
using Cms.CmsWeb.Controls;
using System.Xml;

namespace Cms.Policies.Processes
{
	/// <summary>
	/// Summary description for CorrectiveUserProcess.
	/// </summary>
	public class CorrectiveUserProcess  : Cms.Policies.policiesbase
	{
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected Cms.CmsWeb.Controls.CmsButton btnRollBack;
		protected Cms.CmsWeb.Controls.CmsButton btnBack_To_Search;
		protected Cms.CmsWeb.Controls.CmsButton btnBack_To_Customer_Assistant;
		protected Cms.CmsWeb.Controls.CmsButton btnCommit_To_Spool;
		protected Cms.CmsWeb.Controls.CmsButton btnGet_Premium;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidROW_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPROCESS_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDisplayBody;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnURStatus;
		protected System.Web.UI.HtmlControls.HtmlGenericControl myDIV;
		protected Cms.CmsWeb.WebControls.PolicyTop cltPolicyTop;
        System.Resources.ResourceManager objResourceMgr;
        protected System.Web.UI.WebControls.Label capMessage;
        protected System.Web.UI.WebControls.Label capHeader;
		private int new_policy_version = 0;
		private int already_launched = 0;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidNEW_POLICY_VERSION_ID;
		protected Cms.CmsWeb.Controls.CmsButton btnCommitInProgress;

	
		ClsCorrectiveUserProcess objCorrectiveUserProcess = new ClsCorrectiveUserProcess();
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			((cmsbase) this).ScreenId = "5000_24";
			btnCommit_To_Spool.Attributes.Add("onClick","javascript:Showhide();return true;");
			btnRollBack.Attributes.Add("onClick","javascript:HideShowCommit();");
            objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Processes.CorrectiveUserProcess", System.Reflection.Assembly.GetExecutingAssembly());
			SetButtonsSecurityXML();
			GetQueryString();
			bool valid;
            capHeader.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1252");
            capMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
            btnCommit_To_Spool.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1451");
//bool valid = checkRules();
			cltPolicyTop.UseRequestVariables=false;
			myDIV.Visible=false;
			if(!Page.IsPostBack)
			{
				ClsProcessInfo objProcessInfo = new ClsProcessInfo();

				try
				{
//					if(valid)
//					{
						//Sets the Values
					if((int.Parse(hidPROCESS_ID.Value)== Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_CORRECTIVE_USER_PROCESS) && (already_launched != 1))
					{
						objCorrectiveUserProcess.BeginTransaction();
						if (objCorrectiveUserProcess.CheckProcessEligibility(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value), int.Parse(hidPROCESS_ID.Value))==1)
						{
							objProcessInfo = GetFormValues();
							//objProcessInfo.ROW_ID = int.Parse(hidROW_ID.Value);
							objProcessInfo.PROCESS_ID = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_CORRECTIVE_USER_PROCESS;
							objProcessInfo.CREATED_BY = int.Parse(GetUserId());
							objProcessInfo.COMPLETED_BY = int.Parse(GetUserId());
							objProcessInfo.CREATED_DATETIME = DateTime.Now;
					
							objCorrectiveUserProcess.StartProcess(objProcessInfo);
							new_policy_version = objProcessInfo.NEW_POLICY_VERSION_ID;
							//bool valid = checkRules();
							hidNEW_POLICY_VERSION_ID.Value = new_policy_version.ToString();

							hidROW_ID.Value =objProcessInfo.ROW_ID.ToString();  
							//Updating the policy top,session and menus

							//SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
							SetPolicyInSession(objProcessInfo.POLICY_ID, new_policy_version, objProcessInfo.CUSTOMER_ID);
                            lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1157");//"Corrective User Process launched successfully";
							lblMessage.Visible = true;
							 valid=verifyRule("LAUNCH");
						}
						else
						{
							PopulateProcessInfo();
							 valid=verifyRule("");
						}
						objCorrectiveUserProcess.CommitTransaction();
					}
					else
					{
						PopulateProcessInfo();
						valid=verifyRule("");
					}
                    SetCaptions();
				}
				catch//(Exception ex)
				{
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1253");//"Cannot start Corrective User process.Try Again.";
					lblMessage.Visible = true;
				}
				try
				{
					SetPolicyInSession(int.Parse(hidPOLICY_ID.Value), int.Parse(hidNEW_POLICY_VERSION_ID.Value), int.Parse(hidCUSTOMER_ID.Value));
				}
				
				catch//(Exception ex)
				{
				}
				//Refresh the Policy Top.
				cltPolicyTop.CallPageLoad();
			}
			string JavascriptText="window.open('/cms/application/Aspx/Quote/Quote.aspx?CALLEDFROM=QUOTE_POL&CUSTOMER_ID=" + hidCUSTOMER_ID.Value  + "&POLICY_ID=" + hidPOLICY_ID.Value + "&POLICY_VERSION_ID=" + hidNEW_POLICY_VERSION_ID.Value  + "&LOBID=" + GetLOBID() + "&QUOTE_ID=" + "0" + "&ONLYEFFECTIVE=" + "false" + "&SHOW=" + "0" +  "','Quote','resizable=yes,scrollbars=yes,left=150,top=50,width=700,height=600');";
			btnGet_Premium.Attributes.Add("onClick",JavascriptText + "return false;"); 
//			try
//			{
//				SetPolicyInSession(int.Parse(hidPOLICY_ID.Value), int.Parse(hidNEW_POLICY_VERSION_ID.Value), int.Parse(hidCUSTOMER_ID.Value));
//			}
//			catch(Exception ex)
//			{
//			}
		}
        private void SetCaptions()
        {
            objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Processes.CancellationProcess", System.Reflection.Assembly.GetExecutingAssembly());
            btnRollBack.Text = ClsMessages.GetButtonsText(base.ScreenId,"btnRollBack");
            btnBack_To_Customer_Assistant.Text = ClsMessages.GetButtonsText(base.ScreenId,"btnBack_To_Customer_Assistant");
            btnBack_To_Search.Text = ClsMessages.GetButtonsText(base.ScreenId,"btnBack_To_Search");
            //btnGet_Premium.Text = objResourceMgr.GetString("btnGet_Premium");
           // btnCommit_To_Spool.Text = objResourceMgr.GetString("btnCommit_To_Spool");
            spnURStatus.InnerText = objResourceMgr.GetString("spnURStatus");
            btnCommitInProgress.Text = ClsMessages.GetButtonsText(base.ScreenId, "btnCommitInProgress");

        }


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
		/// Sets the process information 
		/// </summary>
		private void PopulateProcessInfo()
		{
			ClsPolicyProcess objPro = new ClsPolicyProcess();
			ClsProcessInfo objProcess = objPro.GetRunningProcess(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value));
			if (objProcess !=null)
			{
				hidPOLICY_VERSION_ID.Value = objProcess.POLICY_VERSION_ID.ToString();
				//hidNEW_POLICY_VERSION_ID.Value = objProcess.NEW_POLICY_VERSION_ID.ToString();
				hidROW_ID.Value = objProcess.ROW_ID.ToString();
				//Saving the session and refreshing the menu
				SetPolicyInSession(objProcess.POLICY_ID, objProcess.NEW_POLICY_VERSION_ID, objProcess.CUSTOMER_ID);
			}
			else
			{
				SetPolicyInSession(int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value), int.Parse(hidCUSTOMER_ID.Value));
				hidDisplayBody.Value = "False";
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1217");//"No Process in Progress on this Policy.";
				lblMessage.Visible=true;
			}
				
			//Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGeneralInformation = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
			//DataSet dsPolicy =null;
			//dsPolicy = objGeneralInformation.GetPolicyDataSet(int.Parse(hidCUSTOMER_ID.Value==""?"0": hidCUSTOMER_ID.Value) ,int.Parse (hidPOLICY_ID.Value ==""?"0":hidPOLICY_ID.Value),int.Parse (hidPOLICY_VERSION_ID.Value==""?"0":hidPOLICY_VERSION_ID.Value));
			//if(dsPolicy.Tables[0].Rows[0]["UNDERWRITER"]!=null && dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString()!="" && dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString()!="0")
			///	hidUNDERWRITER.Value = dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString();
			//if(dsPolicy.Tables[0].Rows[0]["POLICY_LOB"]!=null && dsPolicy.Tables[0].Rows[0]["POLICY_LOB"].ToString()!="" && dsPolicy.Tables[0].Rows[0]["POLICY_LOB"].ToString()!="0")
			//	hidLOB_ID.Value = dsPolicy.Tables[0].Rows[0]["POLICY_LOB"].ToString();
		
			//dsPolicy.Clear();
			//dsPolicy.Dispose(); 
			//hidDisplayBody.Value = "True";
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
		
		private void GetQueryString()
		{
			hidCUSTOMER_ID.Value		= Request.Params["CUSTOMER_ID"].ToString();
			hidPOLICY_ID.Value			= Request.Params["POLICY_ID"].ToString();
			hidPOLICY_VERSION_ID.Value	= Request.Params["policyVersionID"].ToString();
//			hidNEW_POLICY_VERSION_ID.Value = Request.Params["policyVersionID"].ToString();

			try
			{
				if(hidNEW_POLICY_VERSION_ID == null || hidNEW_POLICY_VERSION_ID.Value == "" || hidNEW_POLICY_VERSION_ID.Value == "0")
				{
					ClsProcessInfo objProcessInfo = new ClsProcessInfo();

					objProcessInfo = objCorrectiveUserProcess.GetRunningProcess(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value));    
					if(objProcessInfo.NEW_POLICY_VERSION_ID.ToString() != "" && objProcessInfo.NEW_POLICY_VERSION_ID.ToString() != "0")
					{
						already_launched = 1;
						hidNEW_POLICY_VERSION_ID.Value = objProcessInfo.NEW_POLICY_VERSION_ID.ToString();
						new_policy_version = int.Parse(hidNEW_POLICY_VERSION_ID.Value);
					}
					else
						hidNEW_POLICY_VERSION_ID.Value = Request.Params["policyVersionID"].ToString();
				}
			}
			catch//(Exception ex)
			{
				hidNEW_POLICY_VERSION_ID.Value = Request.Params["policyVersionID"].ToString();
			}
			
			if (Request.Params["process"].ToString().ToUpper() == "CORUSER")
				hidPROCESS_ID.Value = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_CORRECTIVE_USER_PROCESS.ToString();
			else if (Request.Params["process"].ToString().ToUpper() == "CCORUSER")
			{
				hidPROCESS_ID.Value = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_CORRECTIVE_USER_PROCESS.ToString();
				btnRollBack.Attributes.Add("style","display:none");
			}
			else if (Request.Params["process"].ToString().ToUpper() == "RCORUSER")
			{
				hidPROCESS_ID.Value = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ROLLBACK_CORRECTIVE_USER_PROCESS.ToString();
				btnCommit_To_Spool.Attributes.Add("style","display:none");
			}

			Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGeneralInformation = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
			DataSet dsPolicy = objGeneralInformation.GetPolicyDataSet(int.Parse(hidCUSTOMER_ID.Value==""?"0": hidCUSTOMER_ID.Value) ,int.Parse (hidPOLICY_ID.Value ==""?"0":hidPOLICY_ID.Value),int.Parse (hidPOLICY_VERSION_ID.Value==""?"0":hidPOLICY_VERSION_ID.Value));

			hidLOB_ID.Value = dsPolicy.Tables[0].Rows[0]["POLICY_LOB"].ToString();
			dsPolicy.Clear();
			dsPolicy.Dispose(); 
		}

		private void SetButtonsSecurityXML()
		{
			btnRollBack.CmsButtonClass = CmsButtonType.Read;
			btnRollBack.PermissionString = gstrSecurityXML;

			this.btnBack_To_Customer_Assistant.CmsButtonClass = CmsButtonType.Read;
			this.btnBack_To_Customer_Assistant.PermissionString = gstrSecurityXML;

			this.btnBack_To_Search.CmsButtonClass = CmsButtonType.Read;
			this.btnBack_To_Search.PermissionString = gstrSecurityXML;

			this.btnCommit_To_Spool.CmsButtonClass = CmsButtonType.Write;
			this.btnCommit_To_Spool.PermissionString = gstrSecurityXML;

			btnGet_Premium.CmsButtonClass = CmsButtonType.Read;
			btnGet_Premium.PermissionString = gstrSecurityXML;

			btnCommitInProgress.CmsButtonClass = CmsButtonType.Read;
			btnCommitInProgress.PermissionString = gstrSecurityXML;


		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.btnRollBack.Click += new System.EventHandler(this.btnRollBack_Click);
			this.btnBack_To_Search.Click += new System.EventHandler(this.btnBack_To_Search_Click);
			this.btnBack_To_Customer_Assistant.Click += new System.EventHandler(this.btnBack_To_Customer_Assistant_Click);
			this.btnCommit_To_Spool.Click += new System.EventHandler(this.btnCommit_To_Spool_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		/// <summary>
		/// Hides the commit and rollback button
		/// </summary>
		private void HideButtons()
		{
			//btnSave.Attributes.Add("style","display:none");
			btnCommit_To_Spool.Attributes.Add("style","display:none");
			btnRollBack.Attributes.Add("style","display:none");
		}

		private void btnRollBack_Click(object sender, System.EventArgs e)
		{
		
			ClsProcessInfo objProcessInfo = new ClsProcessInfo();
			try
			{
				objProcessInfo = objCorrectiveUserProcess.GetRunningProcess(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value));    
			
				objProcessInfo.PROCESS_ID = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ROLLBACK_CORRECTIVE_USER_PROCESS;
				objProcessInfo.COMPLETED_BY = int.Parse(GetUserId());
				objProcessInfo.COMPLETED_DATETIME = DateTime.Now;	
				hidPROCESS_ID.Value=objProcessInfo.PROCESS_ID.ToString();	
				//Rollbacking the process 
				if (objCorrectiveUserProcess.RollbackProcess(objProcessInfo) == true)
				{
					//Rolled back successfully
					lblMessage.Text = ClsMessages.FetchGeneralMessage("1936");
					hidDisplayBody.Value = "True";
					//Hiding the buttons
					HideButtons();

					//Updating the policy top,session and menus
					SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
				}
				else
				{
					//Error occured
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1242");//"Unable to rollback the process, please try later";
					hidDisplayBody.Value = "False";
				}
				lblMessage.Visible = true;
			}
			catch(Exception objExp)
			{
				cltPolicyTop.UseRequestVariables = true;
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1231")//"Following error occured while rollbacking process. \n" 
					+ objExp.Message + "\n Please try later.";
				lblMessage.Visible = true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
			}
	
		}

		private void btnBack_To_Search_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("/cms/client/aspx/CustomerManagerSearch.aspx");	
		}

		private void btnBack_To_Customer_Assistant_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("/cms/client/aspx/CustomerManagerIndex.aspx?Customer_ID=" + hidCUSTOMER_ID.Value);		
		}

		private ClsProcessInfo GetFormValues()
		{
			ClsProcessInfo objProcess = new ClsProcessInfo();
			ClsNewBusinessProcess objNewBusPro = new ClsNewBusinessProcess();

			objProcess.POLICY_ID = int.Parse(hidPOLICY_ID.Value);
			objProcess.POLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value);
			objProcess.CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);
			objProcess.PROCESS_ID = POLICY_NEW_BUSINESS_PROCESS;			
//			objProcess.COMMENTS = txtCOMMENTS.Text;				
			objProcess.NEW_CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);
			objProcess.LOB_ID = int.Parse(hidLOB_ID.Value);
			objProcess.NEW_POLICY_ID = int.Parse(hidPOLICY_ID.Value);
			objProcess.NEW_POLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value);
			objProcess.POLICY_CURRENT_STATUS = objCorrectiveUserProcess.GetPolicyStatus(objProcess.CUSTOMER_ID,objProcess.POLICY_ID,objProcess.NEW_POLICY_VERSION_ID);
			objProcess.POLICY_PREVIOUS_STATUS = objCorrectiveUserProcess.GetPolicyStatus(objProcess.CUSTOMER_ID,objProcess.POLICY_ID,objProcess.POLICY_VERSION_ID);

/*			if(cmbAGENCY_PRINT.SelectedItem!=null && cmbAGENCY_PRINT.SelectedItem.Value!="")
				objProcess.AGENCY_PRINT = int.Parse(cmbAGENCY_PRINT.SelectedItem.Value);
			
			if(cmbSEND_INSURED_COPY_TO.SelectedItem!=null && cmbSEND_INSURED_COPY_TO.SelectedItem.Value!="")
				objProcess.SEND_INSURED_COPY_TO = int.Parse(cmbSEND_INSURED_COPY_TO.SelectedItem.Value);

			if(chkPRINTING_OPTIONS.Checked)
				objProcess.PRINTING_OPTIONS = (int)(enumYESNO_LOOKUP_CODE.YES);
			else
				objProcess.PRINTING_OPTIONS = (int)(enumYESNO_LOOKUP_CODE.NO);
			if(cmbINSURED.SelectedItem!=null && cmbINSURED.SelectedItem.Value!="")
				objProcess.INSURED = int.Parse(cmbINSURED.SelectedItem.Value);

			if(cmbCUSTOM_LETTER_REQD.SelectedItem!=null && cmbCUSTOM_LETTER_REQD.SelectedItem.Value!="")
				objProcess.CUSTOM_LETTER_REQD = int.Parse(cmbCUSTOM_LETTER_REQD.SelectedItem.Value);

			if(cmbSTD_LETTER_REQD.SelectedItem!=null && cmbSTD_LETTER_REQD.SelectedItem.Value!="")
				objProcess.STD_LETTER_REQD = int.Parse(cmbSTD_LETTER_REQD.SelectedItem.Value);

			objProcess.CREATED_BY = objProcess.COMPLETED_BY = int.Parse(GetUserId()); 
			objProcess.CREATED_DATETIME = objProcess.COMPLETED_DATETIME = System.DateTime.Now;

			if(hidOldData.Value=="" || hidOldData.Value=="0")
			{
				if(hidLOB_ID.Value==((int)enumLOB.AUTOP).ToString() || hidLOB_ID.Value==((int)enumLOB.CYCL).ToString())
				{
					objProcess.AUTO_ID_CARD = int.Parse(((int)enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL).ToString());
					objProcess.NO_COPIES = AUTO_CYCL_NO_OF_COPIES;
				}
				else				
					objProcess.AUTO_ID_CARD = int.Parse(((int)enumPROC_PRINT_OPTIONS.NOT_REQUIRED).ToString());
					
			}
			else
			{
			if(cmbAUTO_ID_CARD.SelectedItem!=null && cmbAUTO_ID_CARD.SelectedItem.Value!="")
				objProcess.AUTO_ID_CARD = int.Parse(cmbAUTO_ID_CARD.SelectedItem.Value);
			else
			{
				if(hidLOB_ID.Value==((int)enumLOB.AUTOP).ToString() || hidLOB_ID.Value==((int)enumLOB.CYCL).ToString())
					objProcess.AUTO_ID_CARD = int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL).ToString());
				else
					objProcess.AUTO_ID_CARD = int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.NOT_REQUIRED).ToString());
			}
			if(txtNO_COPIES.Text.Trim()!="")
				objProcess.NO_COPIES = int.Parse(txtNO_COPIES.Text.Trim());
			else
			{
				if(hidLOB_ID.Value==((int)enumLOB.AUTOP).ToString() || hidLOB_ID.Value==((int)enumLOB.CYCL).ToString())
					objProcess.NO_COPIES = AUTO_CYCL_NO_OF_COPIES;
			}
			//}
			if(cmbADD_INT.SelectedItem!=null && cmbADD_INT.SelectedItem.Value!="")
			{
				objProcess.ADD_INT = int.Parse(cmbADD_INT.SelectedItem.Value);

				if(objProcess.ADD_INT==int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL).ToString()))
				{
					objProcess.ADD_INT_ID = hidADD_INT_ID.Value;
					if(chkSEND_ALL.Checked==true)
						objProcess.SEND_ALL = (int)(enumYESNO_LOOKUP_CODE.YES);
					else
						objProcess.SEND_ALL = (int)(enumYESNO_LOOKUP_CODE.NO);
				}
			}*/
			
			return objProcess;
		}

		private bool checkRules()
		{
			string strRulesStatus="";
			bool valid=false;	

			//string strRulesHTML = base.strHTML(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value),out valid,out strRulesStatus);
			//string strRulesHTML = Cms.BusinessLayer.BlProcess.clsprocess.strHTMLMandatory(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value),out valid,out strRulesStatus);

			Cms.BusinessLayer.BlApplication.ClsRatingAndUnderwritingRules objRules = new Cms.BusinessLayer.BlApplication.ClsRatingAndUnderwritingRules(CarrierSystemID);
			string strRulesHTML=objRules.VerifyPolicyMandatory(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),new_policy_version,hidLOB_ID.Value,out valid,GetColorScheme(),out strRulesStatus);			
			

			string headerHTML;

			// Check if any mandatory rule is violated
			int headerindex = strRulesHTML.IndexOf("Please complete the following information");
			int ruleindex = 0;
			if(headerindex > 0)
			{
				headerHTML = strRulesHTML.Substring(headerindex);
				ruleindex = headerHTML.IndexOf("midcolora");
			}
			if(ruleindex > 0)
				valid = false;
			else
				valid = true;

			if(valid) // && strRulesStatus == "0") // then commit
			{
				valid=true;
				myDIV.Visible=false;
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1251");//"All Underwriting mandatory rules verified";
				lblMessage.Visible = true;
				return true;
			}
			else
			{
				// show rules msg		
					
				
				// chk here for referred/rejected cases
				//ChkReferedRejCaese(strRulesHTML);
				//this.mySPAN.InnerHtml=strRulesHTML;
				myDIV.InnerHtml=strRulesHTML;
				myDIV.Visible=true;
				//spnURStatus.Visible=true;
				valid=false;
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1250");//"Underwriting mandatory rules not verified";
				lblMessage.Visible = true;
				return false;
			}				
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

					string strRulesHTML = objProcess.strHTML(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidNEW_POLICY_VERSION_ID.Value),out valid,out strRulesStatus,"CUP");
			
					if(valid && strRulesStatus == "0") // then commit
					{
						valid=true;
					}
					else
					{
						// show rules msg		
					
						bool ReferFlag=false;
						// chk here for referred/rejected cases
						ChkReferedRejCaese(strRulesHTML,CalledFrom,out ReferFlag);
						//this.mySPAN.InnerHtml=strRulesHTML;
						myDIV.InnerHtml=strRulesHTML;
						myDIV.Visible=true;
						spnURStatus.Visible=true;
						if (ReferFlag==true)
						valid=true;
						else
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
		private void ChkReferedRejCaese(string strRulesHTML,string strCalledFrom, out bool  ReferFlag )
		{
			ReferFlag=false;
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
						//btnComitAynway.Visible=false;
                        if (strCalledFrom == "COMMIT")
                            lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1218");//"Unable to commit process. Because Policy has been rejected as shown below." ;
                        else if (strCalledFrom == "LAUNCH")
                            lblMessage.Text = lblMessage.Text + "<br>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1219");//Policy has been rejected as shown below.";
                        else
                            lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1219");//"Policy has been rejected as shown below.";
						lblMessage.Visible=true;
					}
					else if(objXmlNodeList.Item(0).InnerText=="0")
					{
						//btnComitAynway.Visible=true;
						//btnComitAynway.Visible=false;
                        if (strCalledFrom == "COMMIT")
                            lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1220");//"Unable to commit process. Because Policy has been referred as shown below." ;
                        else
                            lblMessage.Text = lblMessage.Text + "<br>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1223");//Policy has been referred as shown below.";
						lblMessage.Visible=true;
						ReferFlag=true;
					}				
				}
				else
				{
                    if (strCalledFrom == "COMMIT")
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1221");//'"Unable to commit process. Please fill the mandatory information as shown below." ;
                    else
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1222");//"Please fill the mandatory information as shown below.";
					lblMessage.Visible=true;
					//btnComitAynway.Visible=false;
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

		private void btnCommit_To_Spool_Click(object sender, System.EventArgs e)
		{
		
			//Local Variable Declartions
			ClsProcessInfo objProcessInfo = new ClsProcessInfo();

			try
			{
				
				objProcessInfo = objCorrectiveUserProcess.GetRunningProcess(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value));    
				hidNEW_POLICY_VERSION_ID.Value = objProcessInfo.NEW_POLICY_VERSION_ID.ToString();
				new_policy_version = objProcessInfo.NEW_POLICY_VERSION_ID;
				//if(checkRules() == false)
				if (verifyRule("COMMIT")==false)
				{
					//Done for Itrack Issue 5700 on 15 April 2009
					cltPolicyTop.UseRequestVariables = true;
					cltPolicyTop.CallPageLoad();
					//Response.Write("<script language='javascript'> alert('Please verify all mandatory rules, only then you can commit'); </script>");
					return;
				}
				//Sets the Values
				//objProcessInfo = GetFormValues();
				objProcessInfo.LOB_ID = int.Parse(hidLOB_ID.Value);
				objProcessInfo.ROW_ID = int.Parse(hidROW_ID.Value);
				objProcessInfo.PROCESS_ID = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_CORRECTIVE_USER_PROCESS;
				objProcessInfo.CREATED_BY = int.Parse(GetUserId());
				objProcessInfo.COMPLETED_BY = int.Parse(GetUserId());
				objProcessInfo.COMPLETED_DATETIME = DateTime.Now;
				
				if (objCorrectiveUserProcess.CommitProcess(objProcessInfo) == true)
				{ 
					
					hidFormSaved.Value = "1";
					hidDisplayBody.Value = "True";
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1249");//"Corrective User Process committed successfully";
					btnCommitInProgress.Attributes.Add("style","display:none");
					btnCommit_To_Spool.Attributes.Add("style","display:none");
					//btnCommit_To_Spool.Enabled = false;

					//lblMessage.Text = ClsMessages.FetchGeneralMessage("697");

				}
				else
				{
					hidDisplayBody.Value = "False";
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1248");//"Corrective User Process could not be committed";
					btnCommit_To_Spool.Attributes.Add("style","display:inline");
					btnCommitInProgress.Attributes.Add("style","display:none");
					//lblMessage.Text = ClsMessages.FetchGeneralMessage("602");
				}

				//lblMessage.Text = "Process successfully committed for Corrective User";
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
				btnCommit_To_Spool.Attributes.Add("style","display:inline");
				btnCommitInProgress.Attributes.Add("style","display:none");
				//Done for Itrack Issue 5700 on 15 April 2009
				cltPolicyTop.UseRequestVariables = true;
				cltPolicyTop.CallPageLoad();
			}
		}
	}
}
