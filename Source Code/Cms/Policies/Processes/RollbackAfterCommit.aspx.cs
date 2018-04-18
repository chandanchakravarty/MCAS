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

namespace Policies.Processes
{
	/// <summary>
	/// Summary description for RollbackAfterCommit.
	/// </summary>
	public class RollbackAfterCommit : Cms.Policies.Processes.Processbase
	{
		protected Cms.CmsWeb.WebControls.PolicyTop cltPolicyTop;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected Cms.CmsWeb.Controls.CmsButton btnBack_To_Search;
		protected Cms.CmsWeb.Controls.CmsButton btnBack_To_Customer_Assistant;
		protected Cms.CmsWeb.Controls.CmsButton btnCommit_To_Spool;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidROW_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPROCESS_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Hidden1;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnPolicyDetails;
		//protected Cms.CmsWeb.Controls.CmsButton btnPrintPreview;
		protected Cms.CmsWeb.Controls.CmsButton btnBackToSearch;
		protected Cms.CmsWeb.Controls.CmsButton btnBackToCustomerAssistant;
		protected Cms.CmsWeb.Controls.CmsButton btnGeneratePremiumNotice;
		protected Cms.CmsWeb.Controls.CmsButton btnComplete;
		protected Cms.CmsWeb.Controls.CmsButton btnGet_Premium;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.Controls.CmsButton btnGenerateReinstateDecPage;
		protected Cms.CmsWeb.Controls.CmsButton btnCommitToSpool;
		protected System.Web.UI.WebControls.Label capREASON;
		protected System.Web.UI.WebControls.DropDownList cmbREASON;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREASON;
		protected System.Web.UI.WebControls.Label capOTHER_REASON;
		protected System.Web.UI.WebControls.TextBox txtOTHER_REASON;
		//Uncommented by Charles on 12-Aug-09 for Itrack 6251
		protected System.Web.UI.WebControls.CustomValidator csvOTHER_REASON;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvOTHER_REASON;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDisplayBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidBASE_VERSION_ID;
		protected Cms.CmsWeb.Controls.CmsButton btnRollBack;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidUNDERWRITER;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidNEW_POLICY_VERSION_ID;
		protected System.Web.UI.WebControls.DataGrid dgCommitedProcess;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLAST_REVERT_ID;
		protected Cms.CmsWeb.Controls.CmsButton btnCommitInProgress;
        protected System.Web.UI.WebControls.Label capHeader;
        protected System.Web.UI.WebControls.Label capMessage;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAlertmsg;

        System.Resources.ResourceManager objResourceMgr = new System.Resources.ResourceManager("Policies.Processes.RollbackAfterCommit", System.Reflection.Assembly.GetExecutingAssembly());
		ClsRollbackAfterCommit  objRevertProcess = new ClsRollbackAfterCommit(); 
		private void Page_Load(object sender, System.EventArgs e)
		{
			#region Setting Screen ID

			((cmsbase) this).ScreenId = "5000_22";
            //objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Processes.RollbackAfterCommit", System.Reflection.Assembly.GetExecutingAssembly());

			#endregion
			#region Setting Attributes
			
			//txtNEW_POLICY_TERM_EFFECTIVE_DATE.Attributes.Add("onblur","javascript:ShowExpirationDate();");
			//btnReset.Attributes.Add("onclick","javascript:return formReset();");
			//txtEFFECTIVE_DATETIME.Attributes.Add("onBlur","javascript:return CallService();");
			//cmbADD_INT.Attributes.Add("onChange","javascript:return cmbADD_INT_Change();");				
			//chkSEND_ALL.Attributes.Add("onClick","javascript: chkSEND_ALL_Change();");
			//btnSave.Attributes.Add("onClick","javascript: GetAssignAddInt();Page_ClientValidate();return Page_IsValid;");
			//btnComplete.Attributes.Add("onClick","javascript: GetAssignAddInt();Page_ClientValidate();return Page_IsValid;");
			//hlkReinstateEffectiveDate.Attributes.Add("OnClick","fPopCalendar(document.REWRITE_PROCESS.txtEFFECTIVE_DATETIME,document.REWRITE_PROCESS.txtEFFECTIVE_DATETIME)"); //Javascript Implementation for Calender				
			//hlkTermEffectiveDate.Attributes.Add("OnClick","fPopCalendar(document.REWRITE_PROCESS.txtNEW_POLICY_TERM_EFFECTIVE_DATE,document.REWRITE_PROCESS.txtNEW_POLICY_TERM_EFFECTIVE_DATE)");
			//cmbPRINT_COMMENTS.Attributes.Add("onchange","javascript:CommentEnable();");
			//cmbREQUESTED_BY.Attributes.Add("onchange","javascript:DisplayAgentPhoneNo();");
			btnPolicyDetails.Attributes.Add("onclick","javascript:return ShowDetailsPolicy();");
			btnReset.Attributes.Add("onclick","javascript:return formReset();");
			//btnComplete.Attributes.Add("onclick","javascript:return ConfirmCommit();");   
			btnComplete.Attributes.Add("onClick","javascript:GetAssignAddInt();HideShowCommitInProgress();Page_ClientValidate();return Page_IsValid;");
            capHeader.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1209");
            capMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
            rfvOTHER_REASON.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1212");
            rfvREASON.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1210");
            csvOTHER_REASON.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1211");
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

//			btnPrintPreview.CmsButtonClass					=	CmsButtonType.Write;
//			btnPrintPreview.PermissionString				=	gstrSecurityXML;

			btnCommitToSpool.CmsButtonClass					=	CmsButtonType.Write;
			btnCommitToSpool.PermissionString				=	gstrSecurityXML;

			btnSave.CmsButtonClass					=	CmsButtonType.Write;
			btnSave.PermissionString				=	gstrSecurityXML;

			btnRollBack.CmsButtonClass					=	CmsButtonType.Write;
			btnRollBack.PermissionString				=	gstrSecurityXML;

			btnComplete.CmsButtonClass					=	CmsButtonType.Write;
			btnComplete.PermissionString				=	gstrSecurityXML;

			btnPolicyDetails.CmsButtonClass = CmsButtonType.Write;
			btnPolicyDetails.PermissionString = gstrSecurityXML;

			btnGet_Premium.CmsButtonClass = CmsButtonType.Write;
			btnGet_Premium.PermissionString = gstrSecurityXML;
				
			btnCommitInProgress.CmsButtonClass = CmsButtonType.Write;
			btnCommitInProgress.PermissionString = gstrSecurityXML;

			#endregion
			cltPolicyTop.UseRequestVariables = false;
			btnComplete.Attributes.Add("onClick","javascript:return CheckLastProcessID();");
			btnRollBack.Attributes.Add("onclick","javascript:HideShowCommit();");

			if(!this.Page.IsPostBack)
			{
				//Fetching the query string values
				GetQueryString();
				//Setting the properties of validation controls
				//SetValidators();
				SetCaptions();
				//Setting the policy top controls setting
				SetPolicyTopControl();
				//SetProcessTopControl();
				// fill dropdowns
				GetDropdownData();
				BindGrid();
				//btnSave.Attributes.Add("onClick","javascript: GetAssignAddInt();Page_ClientValidate();return Page_IsValid;");
				//btnComplete.Attributes.Add("onClick","javascript:CheckLastProcessID();Page_ClientValidate();return Page_IsValid;");

				if (hidPROCESS_ID.Value == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_REVERT_PROCESS.ToString())
				{
					objRevertProcess.BeginTransaction();
					if (objRevertProcess.CheckProcessEligibility(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value), int.Parse(hidPROCESS_ID.Value))==1)
					{
						
						//Starting the process
						//SetDefaultValues();
						StartProcess();
					}
					else
					PopulateProcessInfo();
					objRevertProcess.CommitTransaction();
				}
				else
				{
					//Populating the currently executing process information
					PopulateProcessInfo();
				}
			
				//Populating other data
				//				PopulateOtherInfo();
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
			this.btnBackToSearch.Click += new System.EventHandler(this.btnBack_To_Search_Click);
			this.btnBackToCustomerAssistant.Click += new System.EventHandler(this.btnBack_To_Customer_Assistant_Click);
			this.btnComplete.Click += new System.EventHandler(this.btnComplete_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		private void GetDropdownData()
		{
			cmbREASON.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CNCLRN");
			cmbREASON.DataTextField	= "LookupDesc";
			cmbREASON.DataValueField	= "LookupID";
			cmbREASON.DataBind();
			cmbREASON.Items.Insert(0,"");
			//cmbREASON.Items[0].Value = "0";
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
		private void BindGrid()
		{
			DataTable dtProcess=new DataTable();
			Cms.Model.Policy.Process.ClsProcessInfo objClsProcessInfo = new Cms.Model.Policy.Process.ClsProcessInfo();
			objClsProcessInfo.CUSTOMER_ID=int.Parse(hidCUSTOMER_ID.Value);
			objClsProcessInfo.POLICY_ID=int.Parse(hidPOLICY_ID.Value);
			objClsProcessInfo.POLICY_VERSION_ID=int.Parse(hidPOLICY_VERSION_ID.Value);
			objRevertProcess.BeginTransaction();
			DataSet dsProcess= objRevertProcess.FetchPrevousProcessInfo(objClsProcessInfo,"ALLCOMMIT"); 
			objRevertProcess.CommitTransaction();
			dtProcess=dsProcess.Tables[0]; 
			if (dtProcess.Rows.Count > 0)
			{						
				dgCommitedProcess.DataSource=dtProcess.DefaultView;
				dgCommitedProcess.DataBind();
			}
			//DataTable dt=new DataTable();
			//dt=ClsGeneralInformation.CheckApplicantForPolicy(int.Parse(hidCustomerID.Value),int.Parse(hidPolicyID.Value),int.Parse(hidPolicyVersionID.Value));
			//MapChecked(dt);			
		}

		/// <summary>
		/// Starts the process by calling the StartProcess method of ClsEndorsement
		/// </summary>
		private void StartProcess()
		{
			try
			{
				Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo = new Cms.Model.Policy.Process.ClsProcessInfo();
				objProcessInfo = GetFormValues();
				
				objProcessInfo.CREATED_BY = objProcessInfo.COMPLETED_BY = int.Parse(GetUserId()); 
				objProcessInfo.CREATED_DATETIME = objProcessInfo.COMPLETED_DATETIME = System.DateTime.Now;			
				//hidBASE_VERSION_ID.Value =cltProcessTop.BasePolicyVersionID.ToString(); 	
				Cms.BusinessLayer.BlClient.ClsCustomer objPreProcess=new Cms.BusinessLayer.BlClient.ClsCustomer();
				DataSet dsPreProcessInfo=objPreProcess.GetProcessHeaderDetails(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value) ); 
				if (dsPreProcessInfo!=null )
				{
					objProcessInfo.BASE_POLICY_VERSION_ID=int.Parse(dsPreProcessInfo.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());
					objProcessInfo.BASE_POLICY_DISP_VERSION= dsPreProcessInfo.Tables[0].Rows[0]["PREVIOUS_DISP_VERSION"].ToString();
				}
				else
				{
					lblMessage.Visible = true;
					lblMessage.Text =ClsMessages.FetchGeneralMessage("594") + "Previous process Information not found.";
					hidDisplayBody.Value = "False";
					//dsPreProcessInfo.Clear();
					//dsPreProcessInfo.Dispose(); 
					HideButtons();
					return;
				}
				dsPreProcessInfo.Clear();
				dsPreProcessInfo.Dispose(); 
 				if (objRevertProcess.StartProcess(objProcessInfo) == true)
				{ 
					hidFormSaved.Value = "1";
					//saved sucessfully
					hidROW_ID.Value = objProcessInfo.ROW_ID.ToString();
					hidDisplayBody.Value = "True";
					lblMessage.Text = ClsMessages.FetchGeneralMessage("905");
					hidNEW_POLICY_VERSION_ID.Value = objProcessInfo.NEW_POLICY_VERSION_ID.ToString();   
					//Generating the xml of old data
					SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
					cltPolicyTop.PolicyID = objProcessInfo.POLICY_ID;
					cltPolicyTop.PolicyVersionID = objProcessInfo.NEW_POLICY_VERSION_ID;
					cltPolicyTop.CustomerID = objProcessInfo.CUSTOMER_ID;
					cltPolicyTop.UseRequestVariables = false;
					cltPolicyTop.RefreshPolicy();
					FillhidOldData();
				}
				else
				{
					//Hiding the extra buttons
					//HideButtons();
					SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
					hidDisplayBody.Value = "False";
					lblMessage.Text = ClsMessages.FetchGeneralMessage("594");
				}
				
				lblMessage.Visible = true;

				//Saving the session and refreshing the menu
				//SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
			}
			catch(Exception objExp)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
				lblMessage.Text = objExp.Message.ToString();
				lblMessage.Visible = true;

				//Hiding the extra buttons
				//HideButtons();
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
				objProcessInfo.CREATED_BY = objProcessInfo.COMPLETED_BY = int.Parse(GetUserId()); 
				objProcessInfo.COMPLETED_DATETIME = System.DateTime.Now;			
				//Making model object which will contains old data
				ClsProcessInfo objOldProcessInfo = new ClsProcessInfo();
				base.PopulateModelObject(objOldProcessInfo, hidOldData.Value);

				//Updating the previous endorsement record
				objRevertProcess.BeginTransaction(); 
				objRevertProcess.UpdateProcessInformation(objOldProcessInfo, objProcessInfo);
				objRevertProcess.CommitTransaction();

				lblMessage.Text		= ClsMessages.FetchGeneralMessage("31");
				lblMessage.Visible	= true;
				hidFormSaved.Value = "1";

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
				objRevertProcess.RollbackTransaction();
			}
		}

		private void btnRollBack_Click(object sender, System.EventArgs e)
		{
			//Local Variable Declartions
			ClsProcessInfo objProcessInfo = new ClsProcessInfo();

			try
			{
				

				objProcessInfo = objRevertProcess.GetRunningProcess (int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value));
				
				objProcessInfo.PROCESS_ID = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ROLLBACK_REVERT_PROCESS;
				objProcessInfo.ROW_ID = int.Parse(hidROW_ID.Value); 
				objProcessInfo.COMPLETED_BY = int.Parse(GetUserId());
				objProcessInfo.COMPLETED_DATETIME = DateTime.Now;	

				
							
				if (objRevertProcess.RollbackProcess(objProcessInfo) == true)
				{ 
					
					hidFormSaved.Value = "1";
					hidDisplayBody.Value = "True";
					lblMessage.Text = ClsMessages.FetchGeneralMessage("907");
					GetOldDataXml();
					hidNEW_POLICY_VERSION_ID.Value ="";
					//Hiding the extra buttons
					HideButtons();
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
				else
				{
					hidDisplayBody.Value = "False";
					lblMessage.Text = ClsMessages.FetchGeneralMessage("602");
					cltPolicyTop.CallPageLoad();

					//Saving the session and refreshing the menu
					SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);

					//Refresh the Policy Top.
					cltPolicyTop.PolicyID = objProcessInfo.POLICY_ID;
					cltPolicyTop.PolicyVersionID = objProcessInfo.NEW_POLICY_VERSION_ID;
					cltPolicyTop.CustomerID = objProcessInfo.CUSTOMER_ID;
					cltPolicyTop.UseRequestVariables = false;
					cltPolicyTop.RefreshPolicy();
				}

				lblMessage.Visible = true;

			}
			catch(Exception objExp)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
				lblMessage.Text = objExp.Message.ToString();
				lblMessage.Visible = true;

				//Hiding the extra buttons
				//HideButtons();
			}
		}

		private void btnComplete_Click(object sender, System.EventArgs e)
		{
			//Local Variable Declartions
			ClsProcessInfo objProcessInfo = new ClsProcessInfo();
			ClsPolicyProcess objPolicyProcess = new ClsPolicyProcess();

			try
			{
				
				//objProcessInfo = objRevertProcess.GetRunningProcess (int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value));
				objProcessInfo=GetFormValues();
				if(objProcessInfo.LAST_REVERT_BACK != "")
				{
					objProcessInfo.ROW_ID = int.Parse(hidROW_ID.Value); 
					objProcessInfo.PROCESS_ID = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_REVERT_PROCESS;
					objProcessInfo.COMPLETED_BY = int.Parse(GetUserId());
					objProcessInfo.COMPLETED_DATETIME = DateTime.Now;	
					//if (hidBASE_VERSION_ID.Value=="" || hidBASE_VERSION_ID.Value =="0")
					//	hidBASE_VERSION_ID.Value =cltProcessTop.BasePolicyVersionID.ToString(); 
					objProcessInfo.BASE_POLICY_VERSION_ID = int.Parse(hidBASE_VERSION_ID.Value);
                    if (objRevertProcess.CheckClaimStatus(objProcessInfo) == -2) 
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("1971");
                        return;
                    }
                    if (objRevertProcess.CommitProcess(objProcessInfo) == true)
					{ 
					
						hidFormSaved.Value = "1";
						hidDisplayBody.Value = "True";
						lblMessage.Text = ClsMessages.FetchGeneralMessage("906");
						GetOldDataXml();

						//Hiding the extra buttons
						HideButtons();
						cltPolicyTop.PolicyVersionID = objProcessInfo.NEW_POLICY_VERSION_ID;
						SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
					}
					else
					{
						hidDisplayBody.Value = "False";
						lblMessage.Text = ClsMessages.FetchGeneralMessage("601");

						cltPolicyTop.PolicyVersionID = objProcessInfo.NEW_POLICY_VERSION_ID;
						SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
						btnComplete.Attributes.Add("style","display:inline");
						btnCommitInProgress.Attributes.Add("style","display:none");

					}

					//Refresh the Policy Top.
			
					cltPolicyTop.PolicyID = objProcessInfo.POLICY_ID;
					cltPolicyTop.CustomerID = objProcessInfo.CUSTOMER_ID;
					cltPolicyTop.UseRequestVariables = false;
					cltPolicyTop.RefreshPolicy();
					cltPolicyTop.CallPageLoad();
					lblMessage.Visible=true;
				}
				else
				{
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1204");//"Please Select last Process to Revert First."; 
					lblMessage.Visible = true;
				}

			}
			catch(Exception objExp)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
				lblMessage.Text = objExp.Message.ToString();
				lblMessage.Visible = true;
				if (hidNEW_POLICY_VERSION_ID.Value!="" && hidNEW_POLICY_VERSION_ID.Value!="0")			
				    SetPolicyInSession(int.Parse(hidPOLICY_ID.Value) ,int.Parse(hidNEW_POLICY_VERSION_ID.Value), int.Parse(hidCUSTOMER_ID.Value));
				else
					SetPolicyInSession(int.Parse(hidPOLICY_ID.Value) ,int.Parse(hidPOLICY_VERSION_ID.Value), int.Parse(hidCUSTOMER_ID.Value));
				btnComplete.Attributes.Add("style","display:inline");
				btnCommitInProgress.Attributes.Add("style","display:none");

				//Hiding the extra buttons
				//HideButtons();
			}
			finally
			{
				if (objRevertProcess != null)
				{
					objRevertProcess.Dispose();				
				}
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
		/// <summary>
		/// Fetch the Details
		/// </summary>
		private void GetOldDataXml()
		{
			if (hidROW_ID.Value.Trim() != "")
			{
				hidOldData.Value = objRevertProcess.GetOldDataXml(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value),
					int.Parse(hidPOLICY_VERSION_ID.Value), int.Parse(hidROW_ID.Value));
			}
		}


		private void FillhidOldData()
		{
			
			DataSet dsTemp = ClsPolicyProcess.GetProcessInfoDataSet(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value),
				int.Parse(hidPOLICY_VERSION_ID.Value), int.Parse(hidROW_ID.Value));			

			hidOldData.Value = dsTemp.GetXml();


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
			objClsProcessInfo.PROCESS_ID = ClsPolicyProcess.POLICY_REVERT_PROCESS;
			objClsProcessInfo.NEW_CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);			
			objClsProcessInfo.NEW_POLICY_ID = int.Parse(hidPOLICY_ID.Value);
			if (hidNEW_POLICY_VERSION_ID.Value!="" && hidNEW_POLICY_VERSION_ID.Value!="0")
				objClsProcessInfo.NEW_POLICY_VERSION_ID = int.Parse(hidNEW_POLICY_VERSION_ID.Value);
			else
				objClsProcessInfo.NEW_POLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value);
			objClsProcessInfo.POLICY_CURRENT_STATUS = objRevertProcess.GetPolicyStatus(objClsProcessInfo.CUSTOMER_ID,objClsProcessInfo.POLICY_ID,objClsProcessInfo.NEW_POLICY_VERSION_ID);
			objClsProcessInfo.POLICY_PREVIOUS_STATUS = objRevertProcess.GetPolicyStatus(objClsProcessInfo.CUSTOMER_ID,objClsProcessInfo.POLICY_ID,objClsProcessInfo.POLICY_VERSION_ID);
			objClsProcessInfo.LOB_ID = int.Parse(hidLOB_ID.Value) ;
			objClsProcessInfo.STATE_ID	=int.Parse(hidSTATE_ID.Value);

			objClsProcessInfo.LAST_REVERT_BACK	= hidLAST_REVERT_ID.Value;

			if(hidUNDERWRITER.Value!="" && hidUNDERWRITER.Value!="0")
				objClsProcessInfo.UNDERWRITER = int.Parse(hidUNDERWRITER.Value);

			if(cmbREASON.SelectedItem != null && cmbREASON.SelectedValue != "")
				objClsProcessInfo.REASON = int.Parse(cmbREASON.SelectedValue);
			
			objClsProcessInfo.OTHER_REASON = txtOTHER_REASON.Text;

			return objClsProcessInfo;
		}
		/// <summary>
		/// Retreives the query string values into hidden controls
		/// </summary>
		private void GetQueryString()
		{
			hidCUSTOMER_ID.Value		= Request.Params["CUSTOMER_ID"].ToString();
			hidPOLICY_ID.Value			= Request.Params["POLICY_ID"].ToString();
			hidPOLICY_VERSION_ID.Value	= Request.Params["policyVersionID"].ToString();
			//			hidLOB_ID.Value = GetLOBID();
			if (Request.Params["process"].ToString().ToUpper() == "REVERT")
				hidPROCESS_ID.Value = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_REVERT_PROCESS.ToString();
			else if (Request.Params["process"].ToString().ToUpper() == "CREVERT")
			{
				hidPROCESS_ID.Value = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_REVERT_PROCESS.ToString();
				btnRollBack.Attributes.Add("style","display:none");
			}
			else if (Request.Params["process"].ToString().ToUpper() == "RREVERT")
			{
				hidPROCESS_ID.Value = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ROLLBACK_REVERT_PROCESS.ToString();
				btnComplete.Attributes.Add("style","display:none");
			}
//			hidBASE_VERSION_ID.Value =cltProcessTop.BasePolicyVersionID.ToString(); 
			Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGeneralInformation = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
			DataSet dsPolicy = objGeneralInformation.GetPolicyDataSet(int.Parse(hidCUSTOMER_ID.Value==""?"0": hidCUSTOMER_ID.Value) ,int.Parse (hidPOLICY_ID.Value ==""?"0":hidPOLICY_ID.Value),int.Parse (hidPOLICY_VERSION_ID.Value==""?"0":hidPOLICY_VERSION_ID.Value));
			if(dsPolicy.Tables[0].Rows[0]["UNDERWRITER"]!=null && dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString()!="" && dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString()!="0")
				hidUNDERWRITER.Value = dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString();
			hidLOB_ID.Value =dsPolicy.Tables[0].Rows[0]["POLICY_LOB"].ToString();
			hidSTATE_ID.Value=dsPolicy.Tables[0].Rows[0]["STATE_ID"].ToString();
			dsPolicy.Clear();
			dsPolicy.Dispose(); 
				
		}
		private void SetCaptions()
		{
			
			//objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Processes.CancellationProcess" ,System.Reflection.Assembly.GetExecutingAssembly());
			this.capOTHER_REASON.Text		= objResourceMgr.GetString("txtOTHER_REASON");
			this.capREASON.Text				= objResourceMgr.GetString("cmbREASON");
            this.hidAlertmsg.Value = objResourceMgr.GetString("AlertMSG");
            btnBackToCustomerAssistant.Text = ClsMessages.GetButtonsText(base.ScreenId, "btnBackToCustomerAssistant");
            btnBackToSearch.Text = ClsMessages.GetButtonsText(base.ScreenId, "btnBackToSearch");
            btnRollBack.Text = ClsMessages.GetButtonsText(base.ScreenId, "btnRollBack");
            btnGet_Premium.Text = ClsMessages.GetButtonsText(base.ScreenId, "btnGet_Premium");
            btnCommitToSpool.Text= ClsMessages.GetButtonsText(base.ScreenId, "btnCommitToSpool");
            btnComplete.Text = ClsMessages.GetButtonsText(base.ScreenId, "btnComplete");
            btnGeneratePremiumNotice.Text = ClsMessages.GetButtonsText(base.ScreenId, "btnGeneratePremiumNotice");
            btnSave.Text=ClsMessages.GetButtonsText(base.ScreenId, "btnSave");
            btnReset.Text=ClsMessages.GetButtonsText(base.ScreenId, "btnReset");
            btnPolicyDetails.Text=ClsMessages.GetButtonsText(base.ScreenId, "btnPolicyDetails");
            btnCommitInProgress.Text = ClsMessages.GetButtonsText(base.ScreenId, "btnCommitInProgress");
            
		}
		private void SetPolicyTopControl()
		{
			cltPolicyTop.CustomerID = Convert.ToInt32(Request.Params["CUSTOMER_ID"]);
			cltPolicyTop.PolicyID = Convert.ToInt32(Request.Params["POLICY_ID"]);
			cltPolicyTop.PolicyVersionID = Convert.ToInt32(Request.Params["policyVersionID"]);
		}
		private void SetProcessTopControl()
		{
			//cltProcessTop.CustomerID  = Convert.ToInt32(Request.Params["CUSTOMER_ID"]);
			//cltProcessTop.PolicyID = Convert.ToInt32(Request.Params["POLICY_ID"]);
			//cltProcessTop.PolicyVersionID = Convert.ToInt32(Request.Params["policyVersionID"]);
		}
		private void PopulateProcessInfo()
		{
			ClsPolicyProcess objPro = new ClsPolicyProcess();
			ClsProcessInfo objProcess = objPro.GetRunningProcess(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value));
			if (objProcess!=null)
			{
				hidPOLICY_VERSION_ID.Value = objProcess.POLICY_VERSION_ID.ToString();
				hidNEW_POLICY_VERSION_ID.Value =objProcess.NEW_POLICY_VERSION_ID.ToString();
				hidROW_ID.Value = objProcess.ROW_ID.ToString();
				hidDisplayBody.Value = "True";
				//hidBASE_VERSION_ID.Value =cltProcessTop.BasePolicyVersionID.ToString(); 
				Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGeneralInformation = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
				DataSet dsPolicy = objGeneralInformation.GetPolicyDataSet(int.Parse(hidCUSTOMER_ID.Value==""?"0": hidCUSTOMER_ID.Value) ,int.Parse (hidPOLICY_ID.Value ==""?"0":hidPOLICY_ID.Value),int.Parse (hidPOLICY_VERSION_ID.Value==""?"0":hidPOLICY_VERSION_ID.Value));
				if(dsPolicy.Tables[0].Rows[0]["UNDERWRITER"]!=null && dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString()!="" && dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString()!="0")
					hidUNDERWRITER.Value = dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString();
				hidLOB_ID.Value =dsPolicy.Tables[0].Rows[0]["POLICY_LOB"].ToString();
				hidSTATE_ID.Value=dsPolicy.Tables[0].Rows[0]["STATE_ID"].ToString();
				dsPolicy.Clear();
				dsPolicy.Dispose(); 
				LoadData();

				//Saving the session and refreshing the menu
				SetPolicyInSession(objProcess.POLICY_ID, objProcess.NEW_POLICY_VERSION_ID, objProcess.CUSTOMER_ID);
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
				
				if (dsTemp.Tables[0].Rows[0]["REASON"] != DBNull.Value && dsTemp.Tables[0].Rows[0]["REASON"].ToString().Trim() != ""  && dsTemp.Tables[0].Rows[0]["REASON"].ToString().Trim()!="0")
				{
					cmbREASON.SelectedIndex =  cmbREASON.Items.IndexOf(cmbREASON.Items.FindByValue(dsTemp.Tables[0].Rows[0]["REASON"].ToString()));
				}

			
				if (dsTemp.Tables[0].Rows[0]["OTHER_REASON"] != DBNull.Value)
				{
					txtOTHER_REASON.Text = dsTemp.Tables[0].Rows[0]["OTHER_REASON"].ToString();
				}
				hidOldData.Value = dsTemp.GetXml();
				//
				HtmlInputRadioButton htmRadio;
				HtmlInputHidden htmHidID;
				foreach(DataGridItem dgi in dgCommitedProcess.Items)
				{	
					
						htmHidID=(HtmlInputHidden)dgi.FindControl("hidLAST_REVERT");
						htmRadio=(HtmlInputRadioButton)dgi.FindControl("rdbSelect");
					if (dsTemp.Tables[0].Rows[0]["LAST_REVERT_BACK"]!=null)
					{
						if (dsTemp.Tables[0].Rows[0]["LAST_REVERT_BACK"].ToString() == htmHidID.Value)
						{
							htmRadio.Checked=true;
							hidLAST_REVERT_ID.Value=dsTemp.Tables[0].Rows[0]["LAST_REVERT_BACK"].ToString();
						}
						else
						{
							htmRadio.Checked=false;
						}												
					}					
				}							
				//



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

        protected void dgCommitedProcess_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
               //System.Resources.ResourceManager objResourceMgr1  = new System.Resources.ResourceManager("Cms.Policies.Processes.RollbackAfterCommit", System.Reflection.Assembly.GetExecutingAssembly());

            if (e.Item.ItemType == ListItemType.Header) 
            {
                e.Item.Cells[0].Text = objResourceMgr.GetString("SELECT");
                e.Item.Cells[6].Text = objResourceMgr.GetString("NEW_POLICY_VERSION_ID");
                e.Item.Cells[8].Text = objResourceMgr.GetString("PROCESS_NAME");
                e.Item.Cells[9].Text = objResourceMgr.GetString("COMPLETED_DATETIME");
                e.Item.Cells[10].Text = objResourceMgr.GetString("EFFECTIVE_DATE");
                e.Item.Cells[11].Text = objResourceMgr.GetString("CREATED_BY");
                e.Item.Cells[12].Text = objResourceMgr.GetString("PROCESS_STATUS");
            }
            if (e.Item.ItemType == ListItemType.Item) 
            {

            }
        }
	}
}
