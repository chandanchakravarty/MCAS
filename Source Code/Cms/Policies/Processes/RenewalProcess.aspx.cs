/******************************************************************************************
<Author				: -   Vijay Arora
<Start Date			: -	  20-01-2006
<End Date			: -	 
<Description		: -  Class for Renewal Policy Process.
<Review Date		: - 
<Reviewed By		: - 
<modified by		:	Pravesh K. Chandel	
<modified Date		: 
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
using Cms.CmsWeb;
using Cms.BusinessLayer.BlProcess;
using Cms.Model.Policy.Process;
using System.Xml;
using Cms.BusinessLayer.BlApplication;

namespace Cms.Policies.Processes
{
	/// <summary>
	/// Summary description for RenewalProcess.
	/// </summary>
	public class RenewalProcess : Cms.Policies.Processes.Processbase
	{
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capCOMMENTS;
		protected System.Web.UI.WebControls.TextBox txtCOMMENTS;
		protected System.Web.UI.WebControls.CustomValidator csvCOMMENTS;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidROW_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidNEW_POLICY_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidUNDERWRITER; 
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDisplayBody;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnBack_To_Customer_Assistant;
		protected Cms.CmsWeb.Controls.CmsButton btnBack_To_Search;
		//protected Cms.CmsWeb.Controls.CmsButton btnCommit_To_Spool;
		protected Cms.CmsWeb.Controls.CmsButton btnPrint_Preview;
		protected Cms.CmsWeb.Controls.CmsButton btnGenerate_Policy;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPROCESS_ID;
		protected Cms.CmsWeb.WebControls.PolicyTop cltPolicyTop;
		System.Resources.ResourceManager objResourceMgr;
		protected Cms.CmsWeb.Controls.CmsButton btnRollback;
		protected Cms.CmsWeb.Controls.CmsButton btnCommit;
		
		protected Cms.CmsWeb.Controls.CmsButton btnPolicyDetails;
		protected Cms.CmsWeb.Controls.CmsButton btnGet_Premium;
		protected Cms.CmsWeb.Controls.CmsButton btnComitAynway;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnURStatus;
		protected System.Web.UI.HtmlControls.HtmlGenericControl myDIV;
		protected System.Web.UI.WebControls.Label capPRINTING_OPTIONS;
		protected System.Web.UI.WebControls.CheckBox chkPRINTING_OPTIONS;
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
		protected System.Web.UI.WebControls.Label capINSURED;
		protected System.Web.UI.WebControls.DropDownList cmbINSURED;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidADD_INT_ID;
		protected System.Web.UI.WebControls.Label capAGENCY_PRINT;
		protected System.Web.UI.WebControls.DropDownList cmbAGENCY_PRINT;
		protected System.Web.UI.WebControls.Label capAUTO_ID_CARD;
		protected System.Web.UI.WebControls.DropDownList cmbAUTO_ID_CARD;
		protected System.Web.UI.WebControls.Label capNO_COPIES;
		protected System.Web.UI.WebControls.TextBox txtNO_COPIES;
		protected System.Web.UI.WebControls.RangeValidator rngNO_COPIES;
		protected Cms.CmsWeb.Controls.CmsButton btnCommitInProgress;
		protected Cms.CmsWeb.Controls.CmsButton btnCommitAnywayInProgress;
        protected System.Web.UI.WebControls.Label capHeader;
        protected System.Web.UI.WebControls.Label capMessage;
        protected System.Web.UI.WebControls.Label lblPrinting;
        protected System.Web.UI.WebControls.Label Label1;
        //protected Cms.CmsWeb.Controls.CmsButton btnRollback;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCOUNT;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidpopup;

		//bl object for interactio wil business layer
		//ClsNewBusinessProcess objProcess = new ClsNewBusinessProcess();
		ClsRenewalProcess  objProcess = new  ClsRenewalProcess(); 
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			//Setting the screen id
			((cmsbase) this).ScreenId = "5000_32";
			//Setting the security xml of cmb button
			SetButtonsSecurityXML();
			btnPolicyDetails.Attributes.Add("onclick","javascript:return ShowDetailsPolicy();");
			btnReset.Attributes.Add("onclick","javascript:return formReset();");
			btnSave.Attributes.Add("onClick","javascript: GetAssignAddInt();return true;");
			cmbADD_INT.Attributes.Add("onChange","javascript:return cmbADD_INT_Change();");
			chkSEND_ALL.Attributes.Add("onClick","javascript: chkSEND_ALL_Change();");
		
			btnCommit.Attributes.Add("onClick","javascript:GetAssignAddInt();HideShowCommitInProgress();Page_ClientValidate();return Page_IsValid;");
			btnComitAynway.Attributes.Add("onClick","javascript:HideShowCommitAnywayInProgress();GetAssignAddInt();Page_ClientValidate();return Page_IsValid;");				
			btnRollback.Attributes.Add("onclick","javascript:HideShowCommit();");
			//btnComitAynway.Attributes.Add("style","display:none");
			//Client top should not use customer, app and version id from request
			cltPolicyTop.UseRequestVariables = false;
			
			btnComitAynway.Visible=false;
			spnURStatus.Visible=false;			
			myDIV.Visible=false;

			if (!Page.IsPostBack)
			{
				//Fetching the query string values
				GetQueryString();
				
				//Setting the properties of validation controls
				SetValidators();
				
				//Setting the captions of label
				SetCaptions();
				LoadDropDowns();
                capMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
                capHeader.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1232");
				
				//Setting the policy top controls setting
				cltPolicyTop.CustomerID			= int.Parse(hidCUSTOMER_ID.Value);
				cltPolicyTop.PolicyID			= int.Parse(hidPOLICY_ID.Value);
				cltPolicyTop.PolicyVersionID	= int.Parse(hidPOLICY_VERSION_ID.Value);

				if (hidPROCESS_ID.Value == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_RENEWAL_PROCESS.ToString())
				{
					objProcess.BeginTransaction();
					if (objProcess.CheckProcessEligibility(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value), int.Parse(hidPROCESS_ID.Value))==1)
					{
						//Starting the process
                        if (!StartProcess())
                        {
                            //Policy top will be shown for policy passes in request
                            if (Cms.BusinessLayer.BlProcess.ClsPolicyErrMsg.strNonRenewFlag == "False")
                                cltPolicyTop.UseRequestVariables = true;
                        }
                        else
                        {
                            this.verifyRule();
                            //Verify rule message should not come on page load
                            //Added By Lalit April 20,2011 .i-track# 946
                            lblMessage.Text = ClsMessages.FetchGeneralMessage("685");
                        }
					}
					else
					{
						//Populating the currently executing process information
						PopulateProcessInfo();
						if (Convert.ToInt32(hidPROCESS_ID.Value)!= ClsPolicyProcess.POLICY_ROLLBACK_RENEWAL_PROCESS) 
							this.verifyRule(); 
					}
					objProcess.CommitTransaction();
				}
				else
				{
					//Populating the currently executing process information
					PopulateProcessInfo();
                    if (Convert.ToInt32(hidPROCESS_ID.Value) != ClsPolicyProcess.POLICY_ROLLBACK_RENEWAL_PROCESS)
                    {
                        this.verifyRule();
                        //Verify rule message should not come on page load
                        //Added By Lalit April 20,2011 .i-track# 946
                        lblMessage.Text = "";
                    }
				}
				
				string JavascriptText="window.open('/cms/application/Aspx/Quote/Quote.aspx?CALLEDFROM=QUOTE_POL&CUSTOMER_ID=" + hidCUSTOMER_ID.Value  + "&POLICY_ID=" + hidPOLICY_ID.Value + "&POLICY_VERSION_ID=" + hidNEW_POLICY_VERSION_ID.Value  + "&LOBID=" + GetLOBID() + "&QUOTE_ID=" + "0" + "&ONLYEFFECTIVE=" + "false" + "&SHOW=" + "0" +  "','Quote','resizable=yes,scrollbars=yes,left=150,top=50,width=700,height=600');";
				btnGet_Premium.Attributes.Add("onClick",JavascriptText + "return false;");
				//Added By Ravindra Ends Here

                //Added By Shikha Chourasiya
                //-------------Start--------
                //Cms.BusinessLayer.BlProcess.ClsEndorsmentProcess objPolicyProcess = new ClsEndorsmentProcess();

                hidCOUNT.Value = objProcess.rein_Install(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value));
                btnCommit.Attributes.Add("onClick", "javascript:return Installment_Renewal_Result();");

                // ------------End-----------

			}
			//Updating the policy top,session and menus
			//if (hidPOLICY_ID.Value !="" && hidNEW_POLICY_VERSION_ID.Value !="" && hidCUSTOMER_ID.Value !="" )
			//	SetPolicyInSession(int.Parse(hidPOLICY_ID.Value),int.Parse(hidNEW_POLICY_VERSION_ID.Value) , int.Parse(hidCUSTOMER_ID.Value));

		}
		private void LoadDropDowns()
		{
		
			IList ListSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("PRNTOP");
			ListItem iListItem = null;
			if(hidLOB_ID.Value==((int)enumLOB.AUTOP).ToString() || hidLOB_ID.Value==((int)enumLOB.CYCL).ToString())
			{
				cmbAUTO_ID_CARD.DataSource=ListSource;
				cmbAUTO_ID_CARD.DataTextField="LookupDesc";
				cmbAUTO_ID_CARD.DataValueField="LookupID";
				cmbAUTO_ID_CARD.DataBind();		

				
				iListItem = cmbAUTO_ID_CARD.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS.DOWNLOAD).ToString());
				if(iListItem!=null)
					cmbAUTO_ID_CARD.Items.Remove(iListItem);

				//MICHIGAN_MAILERS #Itrack 4068
//				iListItem = null;
//				iListItem = cmbAUTO_ID_CARD.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS.MICHIGAN_MAILERS).ToString());
//				if(iListItem!=null)
//					cmbAUTO_ID_CARD.Items.Remove(iListItem);
			}

			ListSource=null;
			ListSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("PRNTOP");

			cmbINSURED.DataSource=ListSource;
			cmbINSURED.DataTextField="LookupDesc";
			cmbINSURED.DataValueField="LookupID";
			cmbINSURED.DataBind();
			//ListItem iListItem = null;
			iListItem = null;
			iListItem = cmbINSURED.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS.DOWNLOAD).ToString());
			if(iListItem!=null)
				cmbINSURED.Items.Remove(iListItem);
			cmbADD_INT.DataSource=ListSource;
			cmbADD_INT.DataTextField="LookupDesc";
			cmbADD_INT.DataValueField="LookupID";
			cmbADD_INT.DataBind();

			iListItem = null;
			iListItem = cmbADD_INT.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS.DOWNLOAD).ToString());
			if(iListItem!=null)
				cmbADD_INT.Items.Remove(iListItem);

			//MICHIGAN_MAILERS Itrack #4068
//			iListItem = cmbINSURED.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS.MICHIGAN_MAILERS).ToString());
//			if(iListItem!=null)
//				cmbADD_INT.Items.Remove(iListItem);

			cmbAGENCY_PRINT.DataSource=ListSource;
			cmbAGENCY_PRINT.DataTextField="LookupDesc";
			cmbAGENCY_PRINT.DataValueField="LookupID";
			cmbAGENCY_PRINT.DataBind();

			//MICHIGAN_MAILERS Itrack #4068
//			iListItem = cmbINSURED.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS.MICHIGAN_MAILERS).ToString());
//			if(iListItem!=null)
//				cmbAGENCY_PRINT.Items.Remove(iListItem);

			DataTable dtAddIntList = null;
			Cms.BusinessLayer.BlApplication.ClsAdditionalInterest objAdditionalInterest = new Cms.BusinessLayer.BlApplication.ClsAdditionalInterest();
			string PolicyVersionId="0";
			if (hidNEW_POLICY_VERSION_ID.Value!="" && hidNEW_POLICY_VERSION_ID.Value!="0")
					PolicyVersionId=hidNEW_POLICY_VERSION_ID.Value;
			else
					PolicyVersionId=hidPOLICY_VERSION_ID.Value;
			dtAddIntList = objAdditionalInterest.GetAdditionalInterestList(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(PolicyVersionId),int.Parse(hidLOB_ID.Value));
			if(dtAddIntList!=null && dtAddIntList.Rows.Count>0)
			{
				cmbUnAssignAddInt.DataSource = dtAddIntList;
				cmbUnAssignAddInt.DataTextField = "ADD_INT_DETAILS";
				cmbUnAssignAddInt.DataValueField = "ADD_INT_ID";
				cmbUnAssignAddInt.DataBind();
			}
			chkSEND_ALL.Checked =true; 
		}

		/// <summary>
		/// Starts the process by calling the StartProcess method of ClsEndorsement
		/// </summary>
		private bool StartProcess()
		{
			try
			{
				bool RetVal = false;
				Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo = new Cms.Model.Policy.Process.ClsProcessInfo();
				objProcessInfo = GetFormValues();
				objProcessInfo.COMPLETED_BY = int.Parse(GetUserId());
				objProcessInfo.CREATED_BY = int.Parse(GetUserId());
				objProcessInfo.CREATED_DATETIME = DateTime.Now;

				ClsRenewalProcess objProcess = new ClsRenewalProcess();
				
				if (objProcess.StartProcess(objProcessInfo) == true)
				{ 
					hidFormSaved.Value = "1";
					//saved successfully
					hidROW_ID.Value = objProcessInfo.ROW_ID.ToString();	
					hidDisplayBody.Value = "True";
					lblMessage.Text = ClsMessages.FetchGeneralMessage("685");
					//Generating the xml of old data
					GetOldDataXml();

					hidNEW_POLICY_VERSION_ID.Value = objProcessInfo.NEW_POLICY_VERSION_ID.ToString();

					//Setting the new policy in session
					SetPolicyInSession ( objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID );

					RetVal = true;
				}
				else
				{
					hidDisplayBody.Value = "False";
					if (Cms.BusinessLayer.BlProcess.ClsPolicyErrMsg.strMessage!="")
						lblMessage.Text = Cms.BusinessLayer.BlProcess.ClsPolicyErrMsg.strMessage;
					else
						lblMessage.Text = ClsMessages.FetchGeneralMessage("594");
					GetOldDataXml();
					//Hiding the commit and rollback buttons
					HideButtons();
					if(Cms.BusinessLayer.BlProcess.ClsPolicyErrMsg.strNonRenewFlag =="True" && objProcessInfo.NEW_POLICY_VERSION_ID !=0 )
					{
						SetPolicyInSession ( objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID );
						hidNEW_POLICY_VERSION_ID.Value = objProcessInfo.NEW_POLICY_VERSION_ID.ToString();
					}
					else
						SetPolicyInSession ( objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID );
					
					RetVal = false;
				}				

				lblMessage.Visible = true;

				//Refresh the Policy Top.
                

				cltPolicyTop.CallPageLoad();

				return RetVal;
			}
			catch(Exception objExp)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
				lblMessage.Text = objExp.Message.ToString();
				lblMessage.Visible = true;

				//Hiding the commit and rollback buttons
				HideButtons();
				return false;
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
		/// Retreives the old data in the form of xml and will keep in hidOldData hidden field
		/// </summary>
		private void GetOldDataXml()
		{
			if (hidROW_ID.Value.Trim() != "")
			{
				hidOldData.Value = objProcess.GetOldDataXml(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value),
					int.Parse(hidPOLICY_VERSION_ID.Value), int.Parse(hidROW_ID.Value));
			}
		}
		/// <summary>
		/// Retreives the values from form and populates the model object
		/// </summary>
		/// <returns>Model object of ClsProcessInfo type</returns>
		private ClsProcessInfo GetFormValues()
		{
			ClsProcessInfo objProcessInfo = new ClsProcessInfo();
			objProcessInfo.CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);
			objProcessInfo.POLICY_ID = int.Parse(hidPOLICY_ID.Value);
			objProcessInfo.POLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value);
			objProcessInfo.LOB_ID			= int.Parse(hidLOB_ID.Value);   
			objProcessInfo.COMMENTS = txtCOMMENTS.Text;	
			objProcessInfo.CREATED_BY = int.Parse(GetUserId()); 
		
			objProcessInfo.NEW_CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);
			objProcessInfo.NEW_POLICY_ID = int.Parse(hidPOLICY_ID.Value);
			if (hidNEW_POLICY_VERSION_ID.Value!="" && hidNEW_POLICY_VERSION_ID.Value !="0")
					objProcessInfo.NEW_POLICY_VERSION_ID  = int.Parse(hidNEW_POLICY_VERSION_ID.Value);
		
			objProcessInfo.POLICY_CURRENT_STATUS =  objProcess.GetPolicyStatus(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.NEW_POLICY_VERSION_ID);  //GetPolicyStatus();
			objProcessInfo.POLICY_PREVIOUS_STATUS = objProcess.GetPolicyStatus(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.POLICY_VERSION_ID); //GetPolicyStatus();
			
			if(hidOldData.Value=="" || hidOldData.Value=="0")
			{
				if(hidLOB_ID.Value==((int)enumLOB.AUTOP).ToString() || hidLOB_ID.Value==((int)enumLOB.CYCL).ToString())
				{
					objProcessInfo.AUTO_ID_CARD = int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL).ToString());					
				}
			}
			else
			{
				if(hidLOB_ID.Value==((int)enumLOB.AUTOP).ToString() || hidLOB_ID.Value==((int)enumLOB.CYCL).ToString())
				{
					if(cmbAUTO_ID_CARD.SelectedItem!=null && cmbAUTO_ID_CARD.SelectedItem.Value!="")
						objProcessInfo.AUTO_ID_CARD = int.Parse(cmbAUTO_ID_CARD.SelectedItem.Value);
				}
			}
			if(txtNO_COPIES.Text.Trim()!="")
				objProcessInfo.NO_COPIES = int.Parse(txtNO_COPIES.Text.Trim());
			else
			{
				if(hidLOB_ID.Value==((int)enumLOB.AUTOP).ToString() || hidLOB_ID.Value==((int)enumLOB.CYCL).ToString())
					objProcessInfo.NO_COPIES = Cms.BusinessLayer.BlCommon.ClsCommon.PRINT_OPTIONS_AUTO_CYCL_NO_OF_COPIES;
			}

			if(hidUNDERWRITER.Value!="" && hidUNDERWRITER.Value!="0")
				objProcessInfo.UNDERWRITER = int.Parse(hidUNDERWRITER.Value);

			if(chkPRINTING_OPTIONS.Checked)
				objProcessInfo.PRINTING_OPTIONS = (int)(enumYESNO_LOOKUP_CODE.YES);
			else
				objProcessInfo.PRINTING_OPTIONS = (int)(enumYESNO_LOOKUP_CODE.NO);
			if(cmbINSURED.SelectedItem!=null && cmbINSURED.SelectedItem.Value!="")
				objProcessInfo.INSURED = int.Parse(cmbINSURED.SelectedItem.Value);
			
			if(cmbAGENCY_PRINT.SelectedItem!=null && cmbAGENCY_PRINT.SelectedItem.Value!="")
				objProcessInfo.AGENCY_PRINT  = int.Parse(cmbAGENCY_PRINT.SelectedItem.Value);

			if(cmbADD_INT.SelectedItem!=null && cmbADD_INT.SelectedItem.Value!="")
			{
				objProcessInfo.ADD_INT = int.Parse(cmbADD_INT.SelectedItem.Value);

				if(objProcessInfo.ADD_INT==int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL).ToString()))
				{
					objProcessInfo.ADD_INT_ID = hidADD_INT_ID.Value;
					if(chkSEND_ALL.Checked==true)
						objProcessInfo.SEND_ALL = (int)(enumYESNO_LOOKUP_CODE.YES);
					else
						objProcessInfo.SEND_ALL = (int)(enumYESNO_LOOKUP_CODE.NO);
				}
			}


			return objProcessInfo;
		}



		/// <summary>
		/// Sets the caption of Labels from resource file
		/// </summary>
		private void SetCaptions()
		{
			try
			{
				
				objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Processes.RenewalProcess", System.Reflection.Assembly.GetExecutingAssembly());
				capCOMMENTS.Text=	objResourceMgr.GetString("txtCOMMENTS");	

				capINSURED.Text					=	objResourceMgr.GetString("cmbINSURED");			
				capPRINTING_OPTIONS.Text					=	objResourceMgr.GetString("chkPRINTING_OPTIONS");							
				capADD_INT.Text					=	objResourceMgr.GetString("cmbADD_INT");			
				capSEND_ALL.Text				=	objResourceMgr.GetString("chkSEND_ALL");			
				capAGENCY_PRINT.Text				=	objResourceMgr.GetString("txtAGENCY_PRINT");
				capNO_COPIES.Text					=	objResourceMgr.GetString("txtNO_COPIES");							
				capAUTO_ID_CARD.Text				=	objResourceMgr.GetString("cmbAUTO_ID_CARD");
                btnBack_To_Customer_Assistant.Text = ClsMessages.GetButtonsText(base.ScreenId, "btnBack_To_Customer_Assistant");
                btnBack_To_Search.Text = ClsMessages.GetButtonsText(base.ScreenId, "btnBack_To_Search");
                btnRollback.Text = ClsMessages.GetButtonsText(base.ScreenId, "btnRollback");
                btnGet_Premium.Text = ClsMessages.GetButtonsText(base.ScreenId, "btnGet_Premium");
                btnCommit.Text = ClsMessages.GetButtonsText(base.ScreenId,"btnCommit");
                spnURStatus.InnerText = objResourceMgr.GetString("spnURStatus");
                Label1.Text = objResourceMgr.GetString("Label1");
                lblPrinting.Text = objResourceMgr.GetString("lblPrinting");
                btnCommitInProgress.Text = ClsMessages.GetButtonsText(base.ScreenId, "btnCommitInProgress");
                

                
			
				
				
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Sets the property of various validator controls
		/// </summary>
		private void SetValidators()
		{		
			//csvCOMMENTS.ErrorMessage  =  ClsMessages.FetchGeneralMessage("445");  250 charecters
			csvCOMMENTS.ErrorMessage  =  ClsMessages.FetchGeneralMessage("442");  //500 characters
			rngNO_COPIES.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("217");
            hidpopup.Value  = ClsMessages.GetMessage(this.ScreenId, "1");
		}

		/// <summary>
		/// Retreives the query string values into hidden controls
		/// </summary>
		private void GetQueryString()
		{
			hidCUSTOMER_ID.Value		= Request.Params["CUSTOMER_ID"].ToString();
			hidPOLICY_ID.Value			= Request.Params["POLICY_ID"].ToString();
			hidPOLICY_VERSION_ID.Value	= Request.Params["policyVersionID"].ToString();
			//hidLOB_ID.Value = GetLOBID();
			if (Request.Params["process"].ToString().ToUpper() == "RENEWAL")
				hidPROCESS_ID.Value = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_RENEWAL_PROCESS.ToString();
			else if (Request.Params["process"].ToString().ToUpper() == "CRENEWAL")
			{
				hidPROCESS_ID.Value = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_RENEWAL_PROCESS.ToString();
				btnRollback.Attributes.Add("STYLE","DISPLAY:NONE");
			}
			else if (Request.Params["process"].ToString().ToUpper() == "RRENEWAL")
			{
				hidPROCESS_ID.Value = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ROLLBACK_RENEWAL_PROCESS.ToString();
				btnComitAynway.Attributes.Add("STYLE","DISPLAY:NONE");
				btnCommit.Attributes.Add("STYLE","DISPLAY:NONE");
			}
			Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGeneralInformation = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
			DataSet dsPolicy =null;
			if (hidNEW_POLICY_VERSION_ID.Value !="" && hidNEW_POLICY_VERSION_ID.Value !="0") 
			   dsPolicy = objGeneralInformation.GetPolicyDataSet(int.Parse(hidCUSTOMER_ID.Value==""?"0": hidCUSTOMER_ID.Value) ,int.Parse (hidPOLICY_ID.Value ==""?"0":hidPOLICY_ID.Value),int.Parse (hidNEW_POLICY_VERSION_ID.Value==""?"0":hidNEW_POLICY_VERSION_ID.Value));
			else
			   dsPolicy = objGeneralInformation.GetPolicyDataSet(int.Parse(hidCUSTOMER_ID.Value==""?"0": hidCUSTOMER_ID.Value) ,int.Parse (hidPOLICY_ID.Value ==""?"0":hidPOLICY_ID.Value),int.Parse (hidPOLICY_VERSION_ID.Value==""?"0":hidPOLICY_VERSION_ID.Value));
			if(dsPolicy.Tables[0].Rows[0]["UNDERWRITER"]!=null && dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString()!="" && dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString()!="0")
				hidUNDERWRITER.Value = dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString();
			if(dsPolicy.Tables[0].Rows[0]["POLICY_LOB"]!=null && dsPolicy.Tables[0].Rows[0]["POLICY_LOB"].ToString()!="" && dsPolicy.Tables[0].Rows[0]["POLICY_LOB"].ToString()!="0")
				hidLOB_ID.Value = dsPolicy.Tables[0].Rows[0]["POLICY_LOB"].ToString();
			//base.SetLOBID(hidLOB_ID.Value);  
			dsPolicy.Clear();
			dsPolicy.Dispose(); 
	
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

			this.btnBack_To_Customer_Assistant.CmsButtonClass = CmsButtonType.Read;
			this.btnBack_To_Customer_Assistant.PermissionString = gstrSecurityXML;

			this.btnBack_To_Search.CmsButtonClass = CmsButtonType.Read;
			this.btnBack_To_Search.PermissionString = gstrSecurityXML;		

			//this.btnCommit_To_Spool.CmsButtonClass = CmsButtonType.Write;
			//this.btnCommit_To_Spool.PermissionString = gstrSecurityXML;			
			
			this.btnPrint_Preview.CmsButtonClass = CmsButtonType.Read;
			this.btnPrint_Preview.PermissionString = gstrSecurityXML;

			this.btnGenerate_Policy.CmsButtonClass = CmsButtonType.Execute;
			this.btnGenerate_Policy.PermissionString = gstrSecurityXML;

			btnCommit.CmsButtonClass = CmsButtonType.Write;
			btnCommit.PermissionString = gstrSecurityXML;

			btnRollback.CmsButtonClass = CmsButtonType.Write;
			btnRollback.PermissionString = gstrSecurityXML;

			btnPolicyDetails.CmsButtonClass = CmsButtonType.Read;
			btnPolicyDetails.PermissionString = gstrSecurityXML;

			btnGet_Premium.CmsButtonClass = CmsButtonType.Read;
			btnGet_Premium.PermissionString = gstrSecurityXML;
			
			btnComitAynway.CmsButtonClass = CmsButtonType.Write;
			btnComitAynway.PermissionString = gstrSecurityXML;

			btnCommitInProgress.CmsButtonClass = CmsButtonType.Read;
			btnCommitInProgress.PermissionString = gstrSecurityXML;

			btnCommitAnywayInProgress.CmsButtonClass = CmsButtonType.Read;
			btnCommitAnywayInProgress.PermissionString = gstrSecurityXML;

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
				hidNEW_POLICY_VERSION_ID.Value = objProcess.NEW_POLICY_VERSION_ID.ToString();
				hidROW_ID.Value = objProcess.ROW_ID.ToString();
				//Saving the session and refreshing the menu
				SetPolicyInSession(objProcess.POLICY_ID, objProcess.NEW_POLICY_VERSION_ID, objProcess.CUSTOMER_ID);
			
				Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGeneralInformation = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
				DataSet dsPolicy =null;
				if (hidNEW_POLICY_VERSION_ID.Value !="" && hidNEW_POLICY_VERSION_ID.Value !="0") 
					dsPolicy = objGeneralInformation.GetPolicyDataSet(int.Parse(hidCUSTOMER_ID.Value==""?"0": hidCUSTOMER_ID.Value) ,int.Parse (hidPOLICY_ID.Value ==""?"0":hidPOLICY_ID.Value),int.Parse (hidNEW_POLICY_VERSION_ID.Value==""?"0":hidNEW_POLICY_VERSION_ID.Value));
				else
					dsPolicy = objGeneralInformation.GetPolicyDataSet(int.Parse(hidCUSTOMER_ID.Value==""?"0": hidCUSTOMER_ID.Value) ,int.Parse (hidPOLICY_ID.Value ==""?"0":hidPOLICY_ID.Value),int.Parse (hidPOLICY_VERSION_ID.Value==""?"0":hidPOLICY_VERSION_ID.Value));
				if(dsPolicy.Tables[0].Rows[0]["UNDERWRITER"]!=null && dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString()!="" && dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString()!="0")
					hidUNDERWRITER.Value = dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString();
				if(dsPolicy.Tables[0].Rows[0]["POLICY_LOB"]!=null && dsPolicy.Tables[0].Rows[0]["POLICY_LOB"].ToString()!="" && dsPolicy.Tables[0].Rows[0]["POLICY_LOB"].ToString()!="0")
					hidLOB_ID.Value = dsPolicy.Tables[0].Rows[0]["POLICY_LOB"].ToString();
		
				dsPolicy.Clear();
				dsPolicy.Dispose(); 

				hidDisplayBody.Value = "True";
				GetOldDataXml();
			}
			else
			{
				SetPolicyInSession(int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value), int.Parse(hidCUSTOMER_ID.Value));
				hidDisplayBody.Value = "False";
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1217");//"No Process in Progress on this Policy.";
				lblMessage.Visible=true;
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
			this.btnRollback.Click += new System.EventHandler(this.btnRollback_Click);
			this.btnBack_To_Search.Click += new System.EventHandler(this.btnBack_To_Search_Click);
			this.btnBack_To_Customer_Assistant.Click += new System.EventHandler(this.btnBack_To_Customer_Assistant_Click);
			this.btnCommit.Click += new System.EventHandler(this.btnCommit_Click);
			this.btnGet_Premium.Click += new System.EventHandler(this.btnGet_Premium_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnComitAynway.Click += new System.EventHandler(this.btnComitAynway_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		/// <summary>
		/// Update the Information
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			objProcess.BeginTransaction();
			try
			{
				Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo = new Cms.Model.Policy.Process.ClsProcessInfo();
				
				objProcessInfo = GetFormValues();
				objProcessInfo.NEW_POLICY_VERSION_ID = int.Parse(hidNEW_POLICY_VERSION_ID.Value);

				objProcessInfo.PROCESS_STATUS = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.PROCESS_STATUS_PENDING;
				objProcessInfo.PROCESS_ID = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_RENEWAL_PROCESS;
				objProcessInfo.ROW_ID = int.Parse(hidROW_ID.Value);
				

				//Setting the new policy in session
				SetPolicyInSession ( objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID );

				//Making model object which will contains old data
				ClsProcessInfo objOldProcessInfo = new ClsProcessInfo();
				base.PopulateModelObject(objOldProcessInfo, hidOldData.Value);

				
				//Updating the previous endorsement record
				objProcess.UpdateProcessInformation(objOldProcessInfo, objProcessInfo);

				objProcess.CommitTransaction();

				//lblMessage.Text		= ClsMessages.FetchGeneralMessage("31");
				lblMessage.Visible	= true;
				hidFormSaved.Value = "1";

				//Refresh the Policy Top.
				cltPolicyTop.CallPageLoad();
				verifyRule();
                lblMessage.Text = ClsMessages.FetchGeneralMessage("31"); //rule message should come on btncommit_click
				//Updating the policy top,session and menus
				SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
			}
			catch(Exception objExp)
			{
				objProcess.RollbackTransaction();
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
				lblMessage.Text = objExp.Message.ToString();
				lblMessage.Visible = true;
			}
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

					string strRulesHTML = objProcess.strHTML(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidNEW_POLICY_VERSION_ID.Value),out valid,out strRulesStatus,"REN");
			
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
		/// <summary>
		/// Commit the Process
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCommit_Click(object sender, System.EventArgs e)
		{
			
			
			// Check the policy mandatory data
//			if (! base.VerifyPolicy(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value)))
//			{
//				//If rules(mandatory data) are not verified then exiting
//				return;
//			}

			// Check for policy mandatory data 
			
		try
		  {
			bool valid=false;	
			valid=this.verifyRule("COMMIT"); 
			ClsRenewalProcess objProcess = new ClsRenewalProcess();
			
			if(!valid)
			{
				ClsProcessInfo objProcessInfo =GetFormValues();	
				try
				{
					objProcess.BeginTransaction(); 
					objProcessInfo.ROW_ID = int.Parse(hidROW_ID.Value); 
                    //commenter by Lalit.itrack # 1109
                    //not implimentated for brazil
					//objProcess.UpdatePolicyReferToUnderWriter(objProcessInfo.CUSTOMER_ID ,objProcessInfo.POLICY_ID , objProcessInfo.NEW_POLICY_VERSION_ID,"VIOLATION");
                    string msg = ClsMessages.FetchGeneralMessage("1299");//"This Policy has been marked as \"refer to underwriter\".";
					objProcess.AddDiaryEntry(objProcessInfo,msg,msg,true); 
					objProcess.CommitTransaction();
					lblMessage.Text=lblMessage.Text + "<br>" + msg;
                    verifyRule();
				}
				catch
				{
					objProcess.RollbackTransaction();   
 				}
			}
			else
			{

					//ClsRenewalProcess objProcess = new ClsRenewalProcess();

					//ClsProcessInfo objProcessInfo = objProcess.GetRunningProcess (int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value));
					ClsProcessInfo objProcessInfo =GetFormValues();				
					objProcessInfo.PROCESS_ID = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_RENEWAL_PROCESS;
					objProcessInfo.ROW_ID = int.Parse(hidROW_ID.Value); 
					objProcessInfo.COMPLETED_BY = int.Parse(GetUserId());
					objProcessInfo.COMPLETED_DATETIME = DateTime.Now;
					objProcessInfo.CREATED_BY = int.Parse(GetUserId()); 
					//Commiting the process 
					if (objProcess.CommitProcess (objProcessInfo) == true)
					{
					
						//Committed successfully
						hidFormSaved.Value = "1";
						hidDisplayBody.Value = "True";
						lblMessage.Text = ClsMessages.FetchGeneralMessage("692");
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
						lblMessage.Visible = true;
						GetOldDataXml();
						btnCommit.Attributes.Add("style","display:inline");
						btnCommitInProgress.Attributes.Add("style","display:none");

					}
				
					lblMessage.Visible = true;
					
					
				}
				//Refresh the Policy Top.
				cltPolicyTop.CallPageLoad();
				//Updating the policy top,session and menus
				if(hidNEW_POLICY_VERSION_ID.Value !="") 
					SetPolicyInSession(int.Parse(hidPOLICY_ID.Value),int.Parse(hidNEW_POLICY_VERSION_ID.Value) , int.Parse(hidCUSTOMER_ID.Value));
				else
					SetPolicyInSession(int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value) , int.Parse(hidCUSTOMER_ID.Value));
				
				}
				catch(Exception objExp)
				{
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1225")//"Following error occured while commiting process. \n" 
                        + objExp.Message + "\n " + ClsMessages.FetchGeneralMessage("1300");//Please try later.";
					lblMessage.Visible = true;
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
	     		if(hidNEW_POLICY_VERSION_ID.Value !="") 
					SetPolicyInSession(int.Parse(hidPOLICY_ID.Value),int.Parse(hidNEW_POLICY_VERSION_ID.Value) , int.Parse(hidCUSTOMER_ID.Value));
     			else
					SetPolicyInSession(int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value) , int.Parse(hidCUSTOMER_ID.Value));
			btnCommit.Attributes.Add("style","display:inline");
			btnCommitInProgress.Attributes.Add("style","display:none");

				}
		
			}

		/// <summary>
		/// Hides the commit, rollback and other write only button
		/// </summary>
		private void HideButtons()
		{
			btnCommit.Attributes.Add("style","display:none");
			//btnCommit_To_Spool.Attributes.Add("style","display:none");
			btnGenerate_Policy.Attributes.Add("style","display:none");
			btnRollback.Attributes.Add("style","display:none");
			btnSave.Attributes.Add("style","display:none");
			btnCommitInProgress.Attributes.Add("style","display:none");


		}

		/// <summary>
		/// Rollback the Process.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRollback_Click(object sender, System.EventArgs e)
		{
			try
			{
				ClsRenewalProcess objProcess = new ClsRenewalProcess();

				ClsProcessInfo objProcessInfo = objProcess.GetRunningProcess (int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value));
				if (objProcessInfo!=null)
				{
					objProcessInfo.PROCESS_ID = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ROLLBACK_RENEWAL_PROCESS;
					objProcessInfo.COMPLETED_BY = int.Parse(GetUserId());
					objProcessInfo.COMPLETED_DATETIME = DateTime.Now;
					objProcessInfo.ROW_ID = int.Parse(hidROW_ID.Value);  
				
					//Commiting the process 
					if (objProcess.RollbackProcess(objProcessInfo) == true)
					{
						//Committed successfully
						hidFormSaved.Value = "1";
						//hidDisplayBody.Value = "True";
						hidDisplayBody.Value = "False";
						lblMessage.Text = ClsMessages.FetchGeneralMessage("699");
						GetOldDataXml();
						//Hiding the buttons
						HideButtons();
						cltPolicyTop.PolicyVersionID =objProcessInfo.POLICY_VERSION_ID;
						hidNEW_POLICY_VERSION_ID.Value ="";
						//Updating the policy top,session and menus
						SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
					}
					else
					{
						//Error occured
						hidDisplayBody.Value = "False";
						lblMessage.Text = ClsMessages.FetchGeneralMessage("602");
						SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
					}
				}
				else
				{
					hidDisplayBody.Value = "False";
					lblMessage.Text = ClsMessages.FetchGeneralMessage("602");
					//Updating the policy top,session and menus
					SetPolicyInSession(int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value),int.Parse(hidCUSTOMER_ID.Value) );
				}
				lblMessage.Visible = true;
				//Refresh the Policy Top.
				cltPolicyTop.CallPageLoad();
				//Updating the policy top,session and menus
				//SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
			}
			catch(Exception objExp)
			{
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1231")//"Following error occured while rollbacking process. \n" 
					+ objExp.Message + "\n Please try later.";
				lblMessage.Visible = true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
			}
		}

		/// <summary>
		/// Redirected to Customer Manager Search Page.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnBack_To_Search_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("/cms/client/aspx/CustomerManagerSearch.aspx");
		}

		/// <summary>
		/// Redirected to Customer Assistant Tab.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnBack_To_Customer_Assistant_Click(object sender, System.EventArgs e)
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
				base.GeneratePolicyQuote(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidNEW_POLICY_VERSION_ID.Value));
				//Refresh the Policy Top.
				cltPolicyTop.CallPageLoad();
				//Updating the policy top,session and menus
				SetPolicyInSession(int.Parse(hidPOLICY_ID.Value),int.Parse(hidNEW_POLICY_VERSION_ID.Value) , int.Parse(hidCUSTOMER_ID.Value));

			}
			catch(Exception objExp)
			{
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1224")//"Following error occured. \n" 
					+ objExp.Message + "\n Please try later.";
				lblMessage.Visible = true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
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
                        if (strCalledFrom == "COMMIT")
                            lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1218");//"Unable to commit process. Because Policy has been rejected as shown below." ;
                        else
                            lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1219");//"Policy has been rejected as shown below.";
						lblMessage.Visible=true;
					}
					else if(objXmlNodeList.Item(0).InnerText=="0")
					{
						btnComitAynway.Visible=true;
                        if (strCalledFrom == "COMMIT")
                            lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1220");//"Unable to commit process. Because Policy has been referred as shown below." ;
                        else
                            lblMessage.Text = lblMessage.Text + "<br>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1223");// Policy has been referred as shown below." ;
						lblMessage.Visible=true;
					}				
				}
				else
				{
					if (strCalledFrom =="COMMIT")
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

		private void btnComitAynway_Click(object sender, System.EventArgs e)
		{
			try
			{
				ClsRenewalProcess objProcess = new ClsRenewalProcess();

				//ClsProcessInfo objProcessInfo = objProcess.GetRunningProcess (int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value));
				ClsProcessInfo objProcessInfo =GetFormValues();
				objProcessInfo.PROCESS_ID = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_RENEWAL_PROCESS;
				objProcessInfo.ROW_ID = int.Parse(hidROW_ID.Value); 
				objProcessInfo.COMPLETED_BY = int.Parse(GetUserId());
				objProcessInfo.COMPLETED_DATETIME = DateTime.Now;
				objProcessInfo.CREATED_BY = int.Parse(GetUserId());
				//Commiting the process 
				if (objProcess.CommitProcess (objProcessInfo,"COMMITANYWAY") == true)
				{
					
					//Committed successfully
					hidFormSaved.Value = "1";
					hidDisplayBody.Value = "True";
					lblMessage.Text = ClsMessages.FetchGeneralMessage("692");
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
				
				lblMessage.Visible = true;
				//Refresh the Policy Top.
				cltPolicyTop.CallPageLoad();
				//Updating the policy top,session and menus
				SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
			
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
			
			}
		}
	}
}
