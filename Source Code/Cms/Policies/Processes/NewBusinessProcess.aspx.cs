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
using Cms.BusinessLayer.BlCommon;


namespace Cms.Policies.Processes
{
	/// <summary>
	/// Summary description for NewBusinessProcess.
	/// </summary>
	public class NewBusinessProcess : Cms.Policies.Processes.Processbase
	{

		protected System.Web.UI.WebControls.Label capAssignAddInt;
		protected System.Web.UI.WebControls.Label capUnAssignAddInt;
		protected System.Web.UI.WebControls.ListBox cmbUnAssignAddInt;
		protected System.Web.UI.WebControls.ListBox cmbAssignAddInt;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidUNDERWRITER;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_EFFECTIVE_DATE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCALLED_FOR;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCOUNT;

		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capCOMMENTS;
		protected System.Web.UI.WebControls.TextBox txtCOMMENTS;
		protected System.Web.UI.WebControls.CheckBox chkComitAynway;
		protected System.Web.UI.WebControls.RangeValidator rngNO_COPIES;

		protected System.Web.UI.WebControls.Label capNO_COPIES;
		protected System.Web.UI.WebControls.TextBox txtNO_COPIES;

		protected System.Web.UI.WebControls.Label capSEND_ALL;
		protected System.Web.UI.WebControls.CheckBox chkSEND_ALL;

		protected System.Web.UI.WebControls.Label capAGENCY_PRINT;
		protected System.Web.UI.WebControls.DropDownList cmbAGENCY_PRINT;

		protected System.Web.UI.WebControls.Label capSEND_INSURED_COPY_TO;
		protected System.Web.UI.WebControls.DropDownList cmbSEND_INSURED_COPY_TO;

		protected System.Web.UI.WebControls.Label capADD_INT;
		protected System.Web.UI.WebControls.DropDownList cmbADD_INT;

		protected System.Web.UI.WebControls.Label capPRINTING_OPTIONS;
		protected System.Web.UI.WebControls.CheckBox chkPRINTING_OPTIONS;
		protected System.Web.UI.WebControls.Label capINSURED;
		protected System.Web.UI.WebControls.DropDownList cmbINSURED;
		protected System.Web.UI.WebControls.Label capAUTO_ID_CARD;
		protected System.Web.UI.WebControls.DropDownList cmbAUTO_ID_CARD;

		protected System.Web.UI.WebControls.Label capSTD_LETTER_REQD;
		protected System.Web.UI.WebControls.DropDownList cmbSTD_LETTER_REQD;
        protected System.Web.UI.WebControls.Label capHeader;
        protected System.Web.UI.WebControls.Label capMessage;
        protected System.Web.UI.WebControls.Label capPrinting;
        protected System.Web.UI.WebControls.Label capAdditional;

		protected System.Web.UI.WebControls.Label capCUSTOM_LETTER_REQD;
		protected System.Web.UI.WebControls.DropDownList cmbCUSTOM_LETTER_REQD;

		protected System.Web.UI.WebControls.CustomValidator csvCOMMENTS;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidADD_INT_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCALLED_FROM;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidVEHICLE_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidROW_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDisplayBody;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidpopup;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnBack_To_Customer_Assistant;
		protected Cms.CmsWeb.Controls.CmsButton btnBack_To_Search;
		//		protected Cms.CmsWeb.Controls.CmsButton btnCommit_To_Spool;
		protected Cms.CmsWeb.Controls.CmsButton btnRescind;		
		protected Cms.CmsWeb.Controls.CmsButton btnPrint_Preview;
		protected Cms.CmsWeb.Controls.CmsButton btnGenerate_Policy;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPROCESS_ID;
		protected Cms.CmsWeb.WebControls.PolicyTop cltPolicyTop;
		System.Resources.ResourceManager objResourceMgr;
		protected Cms.CmsWeb.Controls.CmsButton btnCommit;
		protected Cms.CmsWeb.Controls.CmsButton btnPolicyDetails;
		//		protected Cms.CmsWeb.Controls.CmsButton btnDecline;
		protected Cms.CmsWeb.Controls.CmsButton btnRollback;
		protected Cms.CmsWeb.Controls.CmsButton btnGet_Premium;
		protected int intPolQuote_ID=0,intShowQuote=0,Customer_ID=0,Policy_ID=0,Policy_Version_ID=0;		
		protected Cms.CmsWeb.Controls.CmsButton btnComitAynway;		
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnURStatus;
		protected System.Web.UI.HtmlControls.HtmlGenericControl myDIV;
		//protected const int AUTO_CYCL_NO_OF_COPIES = 2;		
		public const string CalledFromPageLoad="Page_Load";
		private int AgencyTerminationFlag = 1;
		protected System.Web.UI.WebControls.Label capUnassignLossCodes;
		protected System.Web.UI.WebControls.Label capAssignedLossCodes;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnAssignLossCodes;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnUnAssignLossCodes;
		protected Cms.CmsWeb.Controls.CmsButton btnCommitInProgress;
		protected Cms.CmsWeb.Controls.CmsButton btnCommitAnywayInProgress;
//		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIN_PROCESS;
		//bl object for interactio wil business layer
        protected string Status = "";
        ClsNewBusinessProcess objProcess = new ClsNewBusinessProcess();
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			//Setting the screen id
			((cmsbase) this).ScreenId = "5000_29";
			//Setting the security xml of cmb button
			SetButtonsSecurityXML();

			

			chkComitAynway.Visible=false;
			//chkComitAynway.Visible=btnComitAynway.Visible=false;
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
                capHeader.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1237");
                capMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
				LoadDropDowns();				
				btnPolicyDetails.Attributes.Add("onclick","javascript:return ShowDetailsPolicy();");
				btnReset.Attributes.Add("onclick","javascript:return ResetTheForm();");
				//btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
				cmbADD_INT.Attributes.Add("onChange","javascript:return cmbADD_INT_Change();");				
				btnSave.Attributes.Add("onClick","javascript:GetAssignAddInt();Page_ClientValidate();return Page_IsValid;");
				chkComitAynway.Attributes.Add("onClick","javascript:HideShowCommitAnywayButton();");
            
				btnComitAynway.Attributes.Add("onClick","javascript:HideShowCommitAnywayInProgress();GetAssignAddInt();Page_ClientValidate();return Page_IsValid;");				
				btnComitAynway.Attributes.Add("style","display:none");
                btnCommit.Attributes.Add("onClick", "javascript:GetAssignAddInt();HideShowCommitInProgress();Page_ClientValidate();return Page_IsValid;");
               
				btnRollback.Attributes.Add("onClick","javascript:HideShowCommit();");
				chkSEND_ALL.Attributes.Add("onClick","javascript:chkSEND_ALL_Change();");

                //Added By Shikha Chourasiya
                //-------------Start--------
                Cms.BusinessLayer.BlProcess.clsprocess objPolicyProcess = new clsprocess();
                
                hidCOUNT.Value = objPolicyProcess.rein_Install(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value));
                btnCommit.Attributes.Add("onClick", "javascript:return Installment_Result();");

                // ------------End-----------


				
				//Setting the policy top controls setting
				cltPolicyTop.CustomerID			= int.Parse(hidCUSTOMER_ID.Value);
				cltPolicyTop.PolicyID			= int.Parse(hidPOLICY_ID.Value);
				cltPolicyTop.PolicyVersionID	= int.Parse(hidPOLICY_VERSION_ID.Value);
	            //(objProcess.CheckProcessEligibility(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPROCESS_ID.Value)));
				if (hidPROCESS_ID.Value == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_NEW_BUSINESS_PROCESS.ToString())
				{
					objProcess.BeginTransaction();
					if (objProcess.CheckProcessEligibility(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value), int.Parse(hidPROCESS_ID.Value))==1)
					{
						//Starting the process
						SetDefaultValues();
						StartProcess();					
					}
					else
					PopulateProcessInfo();
					objProcess.CommitTransaction();
				}
				else
				{
					//Populating the currently executing process information
					PopulateProcessInfo();
				}

				//Added By Ravindra (07-24-2006)				
				/*string JavascriptText=base.GeneratePolicyQuoteJS(int.Parse(hidCUSTOMER_ID.Value),
																	int.Parse(hidPOLICY_ID.Value),
																	int.Parse(hidPOLICY_VERSION_ID.Value));*/

				//Ravindra(08-31-2006) Moved Generate Policy Quote related code to the page load of 
				// Quote.aspx
				string JavascriptText = "window.open('/cms/application/Aspx/Quote/Quote.aspx?CALLEDFROM=QUOTE_POL&CUSTOMER_ID=" + hidCUSTOMER_ID.Value  + "&POLICY_ID=" + hidPOLICY_ID.Value + "&POLICY_VERSION_ID=" + hidPOLICY_VERSION_ID.Value  + "&LOBID=" + hidLOB_ID.Value + "&QUOTE_ID=" + "0" + "&ONLYEFFECTIVE=" + "false" + "&SHOW=" + "0" +  "','Quote','resizable=yes,scrollbars=yes,left=150,top=50,width=700,height=600');";
				btnGet_Premium.Attributes.Add("onClick",JavascriptText + "return false;");
				JavascriptText = "window.open('/cms/application/Aspx/DecPage.aspx?CALLEDFOR=&CALLEDFROM=POLICY','DecPage','resizable=yes,scrollbars=yes,left=150,top=50,width=700,height=600');";
				btnGenerate_Policy.Attributes.Add("onClick",JavascriptText + "return false;");
				//btnGenerate_Policy.Attributes.Add("onClick","javascript:callItemClicked("1,6,1","/Cms/Application/Aspx/DecPage.aspx?CALLEDFOR=&CALLEDFROM=POLICY:AcordPDF:800:600:SHOWPOPUPWINDOW")

				//Added By Ravindra Ends Here

				//If the process cann't be launched on the given policy, don't verify the rules either
				/*if(AgencyTerminationFlag==1)
				{
					//Check for violation of rules at page load...if the rules are being violated, display the
					//rules verification div tab immmediately
					string strRulesStatus="0";
					bool valid=false;					
					string strRulesHTML = Cms.BusinessLayer.BlProcess.clsprocess.strHTML(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value),out valid,out strRulesStatus);
			
					if(valid && strRulesStatus == "0") 
					{
						valid=true;
					}
					else
					{
						// chk here for referred/rejected cases
						ChkReferredRejCase(strRulesHTML,CalledFromPageLoad);
						//this.mySPAN.InnerHtml=strRulesHTML;
						myDIV.InnerHtml=strRulesHTML;
						myDIV.Visible=true;
						spnURStatus.Visible=true;					
					}		
				}*/
				verifyRule(); 
			}
		}

		private void SetDefaultValues()
		{
			chkSEND_ALL.Checked = true;
			hidADD_INT_ID.Value = "";
			for(int i=0;i<cmbUnAssignAddInt.Items.Count;i++)
			{
				hidADD_INT_ID.Value+=cmbUnAssignAddInt.Items[i].Value.ToString() + "~";
			}
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


			cmbINSURED.DataSource=ListSource;
			cmbINSURED.DataTextField="LookupDesc";
			cmbINSURED.DataValueField="LookupID";
			cmbINSURED.DataBind();

			

			iListItem = null;
			iListItem = cmbINSURED.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS.DOWNLOAD).ToString());
			if(iListItem!=null)
				cmbINSURED.Items.Remove(iListItem);

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

			/*
			cmbSTD_LETTER_REQD.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbSTD_LETTER_REQD.DataTextField="LookupDesc";
			cmbSTD_LETTER_REQD.DataValueField="LookupID";
			cmbSTD_LETTER_REQD.DataBind();
			*/
			cmbSEND_INSURED_COPY_TO.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("SDINCP");
			cmbSEND_INSURED_COPY_TO.DataTextField="LookupDesc";
			cmbSEND_INSURED_COPY_TO.DataValueField="LookupID";
			cmbSEND_INSURED_COPY_TO.DataBind();			
			/*
			cmbCUSTOM_LETTER_REQD.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbCUSTOM_LETTER_REQD.DataTextField="LookupDesc";
			cmbCUSTOM_LETTER_REQD.DataValueField="LookupID";
			cmbCUSTOM_LETTER_REQD.DataBind();
			*/
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
		}

		/// <summary>
		/// Starts the process by calling the StartProcess method of ClsEndorsement
		/// </summary>
		private void StartProcess()
		{
			try
			{
				
				ClsNewBusinessProcess objProcess = new ClsNewBusinessProcess();
				//Check for agency termination
				AgencyTerminationFlag = objProcess.AgenyTerminationVerification(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value),ClsPolicyProcess.POLICY_NEW_BUSINESS_PROCESS);
				if(AgencyTerminationFlag!=1)
				{
					hidDisplayBody.Value = "False";
					if(AgencyTerminationFlag==3)
						lblMessage.Text = ClsMessages.FetchGeneralMessage("917");
					else
						lblMessage.Text = ClsMessages.FetchGeneralMessage("918");
					lblMessage.Visible = true;
					//Hiding the extra buttons
					HideButtons();
					return;
				}	
				Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo = new Cms.Model.Policy.Process.ClsProcessInfo();
				objProcessInfo = GetFormValues();				
				objProcessInfo.CREATED_BY = int.Parse(GetUserId());
				objProcessInfo.CREATED_DATETIME = DateTime.Now;

				if (objProcess.StartProcess(objProcessInfo) == true)
				{ 
					hidFormSaved.Value = "1";
					//saved successfully
					hidROW_ID.Value = objProcessInfo.ROW_ID.ToString();	
					hidDisplayBody.Value = "True";
					lblMessage.Text = ClsMessages.FetchGeneralMessage("681");
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

				lblMessage.Visible = true;
				//Refresh the Policy Top.
				cltPolicyTop.CallPageLoad();

				//Saving the session values
				SetPolicyInSession( objProcessInfo.POLICY_ID, 
					objProcessInfo.POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
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
			ClsProcessInfo objProcess = new ClsProcessInfo();
			ClsNewBusinessProcess objNewBusPro = new ClsNewBusinessProcess();

			//Added by Asfa (11-July-2008) - iTrack #4478
			if (hidROW_ID.Value.Trim() != "")
				objProcess.ROW_ID = int.Parse(hidROW_ID.Value);

			objProcess.POLICY_ID = int.Parse(hidPOLICY_ID.Value);
			objProcess.POLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value);
			objProcess.CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);
			objProcess.PROCESS_ID = POLICY_NEW_BUSINESS_PROCESS;			
			objProcess.COMMENTS = txtCOMMENTS.Text;				
			objProcess.NEW_CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);
			objProcess.NEW_POLICY_ID = int.Parse(hidPOLICY_ID.Value);
			objProcess.NEW_POLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value);
			objProcess.POLICY_CURRENT_STATUS  = objNewBusPro.GetPolicyStatus(objProcess.CUSTOMER_ID,objProcess.POLICY_ID,objProcess.NEW_POLICY_VERSION_ID);  //GetPolicyStatus(); 
			objProcess.POLICY_PREVIOUS_STATUS = objNewBusPro.GetPolicyStatus(objProcess.CUSTOMER_ID,objProcess.POLICY_ID,objProcess.POLICY_VERSION_ID);  //GetPolicyStatus(); 

			if(cmbAGENCY_PRINT.SelectedItem!=null && cmbAGENCY_PRINT.SelectedItem.Value!="")
				objProcess.AGENCY_PRINT = int.Parse(cmbAGENCY_PRINT.SelectedItem.Value);
			
			if(cmbSEND_INSURED_COPY_TO.SelectedItem!=null && cmbSEND_INSURED_COPY_TO.SelectedItem.Value!="")
				objProcess.SEND_INSURED_COPY_TO = int.Parse(cmbSEND_INSURED_COPY_TO.SelectedItem.Value);

			if(chkPRINTING_OPTIONS.Checked)
				objProcess.PRINTING_OPTIONS = (int)(enumYESNO_LOOKUP_CODE.YES);
			else
				objProcess.PRINTING_OPTIONS = (int)(enumYESNO_LOOKUP_CODE.NO);
			if(cmbINSURED.SelectedItem!=null && cmbINSURED.SelectedItem.Value!="")
				objProcess.INSURED = int.Parse(cmbINSURED.SelectedItem.Value);

			//if(cmbCUSTOM_LETTER_REQD.SelectedItem!=null && cmbCUSTOM_LETTER_REQD.SelectedItem.Value!="")
			//	objProcess.CUSTOM_LETTER_REQD = int.Parse(cmbCUSTOM_LETTER_REQD.SelectedItem.Value);

			//if(cmbSTD_LETTER_REQD.SelectedItem!=null && cmbSTD_LETTER_REQD.SelectedItem.Value!="")
			//	objProcess.STD_LETTER_REQD = int.Parse(cmbSTD_LETTER_REQD.SelectedItem.Value);

			objProcess.CREATED_BY = objProcess.COMPLETED_BY = int.Parse(GetUserId()); 
			objProcess.CREATED_DATETIME = objProcess.COMPLETED_DATETIME = System.DateTime.Now;
			if(hidUNDERWRITER.Value!="" && hidUNDERWRITER.Value!="0")
				objProcess.UNDERWRITER = int.Parse(hidUNDERWRITER.Value);

            if (hidAPP_EFFECTIVE_DATE.Value != "" && hidAPP_EFFECTIVE_DATE.Value != "0")
                objProcess.EFFECTIVE_DATETIME =Convert.ToDateTime( ConvertDBDateToCulture(hidAPP_EFFECTIVE_DATE.Value));// ConvertToDate(Convert.ToDateTime(hidAPP_EFFECTIVE_DATE.Value).ToShortDateString());

			if(hidLOB_ID.Value!="" && hidLOB_ID.Value!="0")
				objProcess.LOB_ID = int.Parse(hidLOB_ID.Value);
			

			if(hidOldData.Value=="" || hidOldData.Value=="0")
			{
				if(hidLOB_ID.Value==((int)enumLOB.AUTOP).ToString() || hidLOB_ID.Value==((int)enumLOB.CYCL).ToString())
				{
					objProcess.AUTO_ID_CARD = int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL).ToString());					
				}
//				else				
//					objProcess.AUTO_ID_CARD = int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.NOT_REQUIRED).ToString());
					
			}
			else
			{
				if(hidLOB_ID.Value==((int)enumLOB.AUTOP).ToString() || hidLOB_ID.Value==((int)enumLOB.CYCL).ToString())
				{
					if(cmbAUTO_ID_CARD.SelectedItem!=null && cmbAUTO_ID_CARD.SelectedItem.Value!="")
						objProcess.AUTO_ID_CARD = int.Parse(cmbAUTO_ID_CARD.SelectedItem.Value);
				}
			}
			/*if(hidLOB_ID.Value==((int)enumLOB.AUTOP).ToString() || hidLOB_ID.Value==((int)enumLOB.CYCL).ToString())
			{
				if(cmbAUTO_ID_CARD.SelectedItem!=null && cmbAUTO_ID_CARD.SelectedItem.Value!="")
					objProcess.AUTO_ID_CARD = int.Parse(cmbAUTO_ID_CARD.SelectedItem.Value);
				else
					objProcess.AUTO_ID_CARD = int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL).ToString());
			}
			else
			{
				if(cmbAUTO_ID_CARD.SelectedItem!=null && cmbAUTO_ID_CARD.SelectedItem.Value!="")
					objProcess.AUTO_ID_CARD = int.Parse(cmbAUTO_ID_CARD.SelectedItem.Value);
				else
					objProcess.AUTO_ID_CARD = int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.NOT_REQUIRED).ToString());
			}*/
			if(txtNO_COPIES.Text.Trim()!="")
				objProcess.NO_COPIES = int.Parse(txtNO_COPIES.Text.Trim());
			else
			{
				if(hidLOB_ID.Value==((int)enumLOB.AUTOP).ToString() || hidLOB_ID.Value==((int)enumLOB.CYCL).ToString())
					objProcess.NO_COPIES = Cms.BusinessLayer.BlCommon.ClsCommon.PRINT_OPTIONS_AUTO_CYCL_NO_OF_COPIES;
			}
			//}
			if(cmbADD_INT.SelectedItem!=null && cmbADD_INT.SelectedItem.Value!="")
			{
				objProcess.ADD_INT = int.Parse(cmbADD_INT.SelectedItem.Value);

				//MICHIGAN_MAILERS #Itrack 4068
				if(objProcess.ADD_INT==int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL).ToString())
					|| objProcess.ADD_INT==int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.MICHIGAN_MAILERS).ToString()))
				{
					objProcess.ADD_INT_ID = hidADD_INT_ID.Value;
					if(chkSEND_ALL.Checked==true)
						objProcess.SEND_ALL = (int)(enumYESNO_LOOKUP_CODE.YES);
					else
						objProcess.SEND_ALL = (int)(enumYESNO_LOOKUP_CODE.NO);
				}
			}
			
			return objProcess;
		}



		/// <summary>
		/// Sets the caption of Labels from resource file
		/// </summary>
		private void SetCaptions()
		{
			try
			{
                ClsMessages.SetCustomizedXml(GetLanguageCode());
				objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Processes.NewBusinessProcess", System.Reflection.Assembly.GetExecutingAssembly());
				capCOMMENTS.Text					=	objResourceMgr.GetString("txtCOMMENTS");							
				capNO_COPIES.Text					=	objResourceMgr.GetString("txtNO_COPIES");							
				capAUTO_ID_CARD.Text				=	objResourceMgr.GetString("cmbAUTO_ID_CARD");			
				capINSURED.Text						=	objResourceMgr.GetString("cmbINSURED");			
				capPRINTING_OPTIONS.Text			=	objResourceMgr.GetString("chkPRINTING_OPTIONS");							
				//capSTD_LETTER_REQD.Text				=	objResourceMgr.GetString("cmbSTD_LETTER_REQD");			
				//capCUSTOM_LETTER_REQD.Text			=	objResourceMgr.GetString("cmbCUSTOM_LETTER_REQD");			
				capADD_INT.Text						=	objResourceMgr.GetString("cmbADD_INT");			
				capSEND_ALL.Text					=	objResourceMgr.GetString("chkSEND_ALL");							
				capSEND_INSURED_COPY_TO.Text		=	objResourceMgr.GetString("cmbSEND_INSURED_COPY_TO");			
				capAGENCY_PRINT.Text				=	objResourceMgr.GetString("cmbAGENCY_PRINT");
                btnGet_Premium.Text = ClsMessages.GetButtonsText(ScreenId,"btnGet_Premium");
                btnRollback.Text = ClsMessages.GetButtonsText(ScreenId, "btnRollBack");
                btnCommit.Text = ClsMessages.GetButtonsText(ScreenId,"btnCommit");
                btnBack_To_Customer_Assistant.Text = ClsMessages.GetButtonsText(ScreenId,"btnBack_To_Customer_Assistant");
                btnBack_To_Search.Text = ClsMessages.GetButtonsText(ScreenId,"btnBack_To_Search");
                btnPolicyDetails.Text = ClsMessages.GetButtonsText(ScreenId,"btnPolicyDetails");
                capPrinting.Text = objResourceMgr.GetString("capPrinting");
                capAdditional.Text = objResourceMgr.GetString("capAdditional");
                capUnassignLossCodes.Text = objResourceMgr.GetString("capUnassignLossCodes");
                capAssignedLossCodes.Text = objResourceMgr.GetString("capAssignedLossCodes");
                spnURStatus.InnerText = objResourceMgr.GetString("spnURStatus");
                btnComitAynway.Text = ClsMessages.GetButtonsText(this.ScreenId, "btnComitAynway");
                btnCommitInProgress.Text = ClsMessages.GetButtonsText(this.ScreenId, "btnCommitInProgress");
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
            ClsMessages.SetCustomizedXml(GetLanguageCode());	
			csvCOMMENTS.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("442");
			rngNO_COPIES.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("217");
          
                hidpopup.Value = btnCommitInProgress.Text = ClsMessages.GetMessage(this.ScreenId, "1");

		}

		/// <summary>
		/// Retreives the query string values into hidden controls
		/// </summary>
		private void GetQueryString()
		{
			hidCUSTOMER_ID.Value		= Request.Params["CUSTOMER_ID"].ToString();
			hidPOLICY_ID.Value			= Request.Params["POLICY_ID"].ToString();
			hidPOLICY_VERSION_ID.Value	= Request.Params["policyVersionID"].ToString();
			
			
			if (Request.Params["process"].ToString().ToUpper() == "NBUS")
				hidPROCESS_ID.Value = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_NEW_BUSINESS_PROCESS.ToString();
			else if (Request.Params["process"].ToString().ToUpper() == "CNBUS")
			{
				hidPROCESS_ID.Value = "1";
				hidCALLED_FOR.Value="COMMIT";
				btnRollback.Attributes.Add("STYLE","DISPLAY:NONE");
			}
			else if (Request.Params["process"].ToString().ToUpper() == "RNBUS")
			{
				hidPROCESS_ID.Value = "2";
				hidCALLED_FOR.Value="ROLLBACK";
				btnComitAynway.Attributes.Add("STYLE","DISPLAY:NONE");
				btnCommit.Attributes.Add("STYLE","DISPLAY:NONE");
			}

			Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGeneralInformation = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
			DataSet dsPolicy = objGeneralInformation.GetPolicyDataSet(int.Parse(hidCUSTOMER_ID.Value==""?"0": hidCUSTOMER_ID.Value) ,int.Parse (hidPOLICY_ID.Value ==""?"0":hidPOLICY_ID.Value),int.Parse (hidPOLICY_VERSION_ID.Value==""?"0":hidPOLICY_VERSION_ID.Value));

			if(dsPolicy.Tables[0].Rows[0]["UNDERWRITER"]!=null && dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString()!="" && dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString()!="0")
				hidUNDERWRITER.Value = dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString();

			if(dsPolicy.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"]!=null && dsPolicy.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()!="" && dsPolicy.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()!="0")
				hidAPP_EFFECTIVE_DATE.Value = dsPolicy.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString();

			hidLOB_ID.Value = dsPolicy.Tables[0].Rows[0]["POLICY_LOB"].ToString();
			
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

			
			btnPolicyDetails.CmsButtonClass = CmsButtonType.Read;
			btnPolicyDetails.PermissionString = gstrSecurityXML;

			//			btnDecline.CmsButtonClass = CmsButtonType.Read;
			//			btnDecline.PermissionString = gstrSecurityXML;

			this.btnBack_To_Customer_Assistant.CmsButtonClass = CmsButtonType.Read;
			this.btnBack_To_Customer_Assistant.PermissionString = gstrSecurityXML;

			this.btnBack_To_Search.CmsButtonClass = CmsButtonType.Read;
			this.btnBack_To_Search.PermissionString = gstrSecurityXML;		

			
			this.btnRescind.CmsButtonClass = CmsButtonType.Write;
			this.btnRescind.PermissionString = gstrSecurityXML;			

			//			this.btnCommit_To_Spool.CmsButtonClass = CmsButtonType.Write;
			//			this.btnCommit_To_Spool.PermissionString = gstrSecurityXML;			
			
			this.btnPrint_Preview.CmsButtonClass = CmsButtonType.Read;
			this.btnPrint_Preview.PermissionString = gstrSecurityXML;

			this.btnGenerate_Policy.CmsButtonClass = CmsButtonType.Execute;
			this.btnGenerate_Policy.PermissionString = gstrSecurityXML;

			btnCommit.CmsButtonClass = CmsButtonType.Write;
			btnCommit.PermissionString = gstrSecurityXML;

			btnRollback.CmsButtonClass = CmsButtonType.Write;
			btnRollback.PermissionString = gstrSecurityXML;

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
			if (objProcess!=null)
			{
				hidPOLICY_VERSION_ID.Value = objProcess.POLICY_VERSION_ID.ToString();
				hidROW_ID.Value = objProcess.ROW_ID.ToString();
				hidFormSaved.Value="1";
				hidDisplayBody.Value = "True";
				GetOldDataXml();

				//Saving the session and refreshing the menu
				SetPolicyInSession(objProcess.POLICY_ID, objProcess.POLICY_VERSION_ID, objProcess.CUSTOMER_ID);
			}
			else
			{
				SetPolicyInSession(int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value), int.Parse(hidCUSTOMER_ID.Value));
				hidDisplayBody.Value = "False";
                ClsMessages.SetCustomizedXml(GetLanguageCode());
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

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			objProcess.BeginTransaction();
			try
			{
				Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo = new Cms.Model.Policy.Process.ClsProcessInfo();
				
				objProcessInfo = GetFormValues();
				objProcessInfo.PROCESS_STATUS = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.PROCESS_STATUS_PENDING;
				objProcessInfo.PROCESS_ID = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_NEW_BUSINESS_PROCESS;
				objProcessInfo.ROW_ID = int.Parse(hidROW_ID.Value);

				//Making model object which will contains old data
				ClsProcessInfo objOldProcessInfo = new ClsProcessInfo();
				base.PopulateModelObject(objOldProcessInfo, hidOldData.Value);

				
				//Updating the previous endorsement record
				objProcess.UpdateProcessInformation(objOldProcessInfo, objProcessInfo);

				objProcess.CommitTransaction();
                ClsMessages.SetCustomizedXml(GetLanguageCode());
				lblMessage.Text		= ClsMessages.FetchGeneralMessage("31");
				lblMessage.Visible	= true;
				hidFormSaved.Value = "1";
				GetOldDataXml();
				//Refresh the Policy Top.
				cltPolicyTop.CallPageLoad();
				verifyRule(); 

			}
			catch(Exception objExp)
			{
				hidFormSaved.Value = "0";
				objProcess.RollbackTransaction();
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
				lblMessage.Text = objExp.Message.ToString();
				lblMessage.Visible = true;
			}
		}


      	private void btnCommit_Click(object sender, System.EventArgs e)
		{

			// Check the policy mandatory data
			//			if (! base.VerifyPolicy(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value)))
			//			{
			//				//If rules(mandatory data) are not verified then exiting
			//				return;
			//			}	
			
			// Check for policy mandatory data 
//			if(hidIN_PROCESS.Value == "1")
//				return;
//			hidIN_PROCESS.Value="1";
			

			string strRulesStatus="0";
			bool valid=false;
            bool validuser = false;
			ClsProcessInfo objProcessInfo=null;

			//string strRulesHTML = base.strHTML(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value),out valid,out strRulesStatus);


			Cms.BusinessLayer.BlProcess.clsprocess objPolicyProcess = new clsprocess();

            //Added by Ruchika Chauhan on 7-Feb-2012 for TFS # 3643
            Cms.BusinessLayer.BlProcess.ClsNewBusinessProcess objNewBusinessProcess = new ClsNewBusinessProcess();
            int base_currency_id = int.Parse(GetSYSBaseCurrency());
            validuser = objNewBusinessProcess.ValidateUnderwriterLimits(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value), base_currency_id, int.Parse(hidUNDERWRITER.Value));

            if (validuser == false)
            {
                lblMessage.Text = "Underwriting Authority limits are exceeded.";
                lblMessage.Visible = true;
            }
            else
            {


                //"UnderWriter limits successfully validated. Hence proceed further.

                objPolicyProcess.SystemID = CarrierSystemID;

                string strRulesHTML = objPolicyProcess.strHTML(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value), out valid, out strRulesStatus, "NBS");


                if (valid && strRulesStatus == "0") // then commit
                {
                    valid = true;
                }
                else
                {
                    // show rules msg		
                    //				btnCommit.Text = "Commit";	

                    // chk here for referred/rejected cases
                    ChkReferredRejCase(strRulesHTML, "");
                    //this.mySPAN.InnerHtml=strRulesHTML;
                    myDIV.InnerHtml = strRulesHTML;
                    myDIV.Visible = true;
                    spnURStatus.Visible = true;
                    valid = false;
                }
                if (valid)
                {
                    try
                    {
                        objProcessInfo = GetFormValues();
                        ClsNewBusinessProcess objProcess = new ClsNewBusinessProcess();
                        objProcessInfo.PROCESS_ID = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_NEW_BUSINESS_PROCESS;
                        objProcessInfo.ROW_ID = int.Parse(hidROW_ID.Value);
                        objProcessInfo.COMPLETED_BY = int.Parse(GetUserId());
                        objProcessInfo.COMPLETED_DATETIME = DateTime.Now;

                        if (objProcess.CommitProcess(objProcessInfo, "") == true)
                        {


                            CommitSuccess();
                            if (ClsPolicyProcess.PrintingErrorFlag)
                            {
                                ClsMessages.SetCustomizedXml(GetLanguageCode());
                                lblMessage.Text = ClsMessages.FetchGeneralMessage("908");
                            }
                        }
                        else
                        {
                            //Error occured
                            //						btnCommit.Text = "Commit";	
                            //hidDisplayBody.Value = "False";
                            lblMessage.Text = Cms.BusinessLayer.BlProcess.ClsPolicyErrMsg.strMessage;
                            if (ClsPolicyProcess.PrintingErrorFlag)
                            {
                                ClsMessages.SetCustomizedXml(GetLanguageCode());
                                lblMessage.Text = ClsMessages.FetchGeneralMessage("1017");
                            }
                            //						hidIN_PROCESS.Value="0";
                            //lblMessage.Text = ClsMessages.FetchGeneralMessage("601");

                            //btnCommit.Attributes.Add("style","display:inline");
                            //btnCommitInProgress.Attributes.Add("style","display:none");


                        }
                        //					if(btnCommit.Text == "Commit")
                        //						btnCommit.Text = "Wait for Commit";
                    }
                    catch (System.DllNotFoundException ex)
                    {
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
                        CommitSuccess();
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("908");
                        lblMessage.Visible = true;
                        Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                        btnCommit.Attributes.Add("style", "display:inline");
                        btnCommitInProgress.Attributes.Add("style", "display:none");
                    }
                    catch (Exception objExp)
                    {
                        //					btnCommit.Text = "Commit";	
                        //					hidIN_PROCESS.Value="0";
                        lblMessage.Text = objExp.Message + "\n Please try later.";
                        lblMessage.Visible = true;
                        Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
                        btnCommit.Attributes.Add("style", "display:inline");

                        btnCommitInProgress.Attributes.Add("style", "display:none");

                    }
                    finally
                    {

                        lblMessage.Visible = true;
                        SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
                    }
                }

                lblMessage.Text = "New Business process committed successfully.";
                lblMessage.Visible = true;
            }
			
		}

		private void CommitSuccess()
        {
            ClsMessages.SetCustomizedXml(GetLanguageCode());
			//Committed successfully
			hidFormSaved.Value = "1";
			hidDisplayBody.Value = "True";
			lblMessage.Text = ClsMessages.FetchGeneralMessage("688");
			GetOldDataXml();
			//Display the Buttons
			//btnPrint_Preview.Visible = true;
			btnGenerate_Policy.Visible = true;			//			
			HideButtons();
			//Refresh the Policy Top.
			cltPolicyTop.CallPageLoad();
			//Updating the policy top,session and menus
			
		}

		private void btnRollback_Click(object sender, System.EventArgs e)
		{
			try
			{
				ClsNewBusinessProcess objProcess = new ClsNewBusinessProcess();

				ClsProcessInfo objProcessInfo = GetFormValues();
				objProcessInfo.COMPLETED_BY = int.Parse(GetUserId());
				objProcessInfo.COMPLETED_DATETIME = DateTime.Now;
				objProcessInfo.ROW_ID = int.Parse(hidROW_ID.Value);
                objProcessInfo.POLICY_CURRENT_STATUS = Cms.BusinessLayer.BlCommon.ClsCommon.POLICY_STATUS_REJECT;   
				//Commiting the process 
				if (objProcess.RollbackProcess(objProcessInfo) == true)
				{
					//Committed successfully
					hidFormSaved.Value = "1";
					hidDisplayBody.Value = "True";
                    ClsMessages.SetCustomizedXml(GetLanguageCode());
                    lblMessage.Text = ClsMessages.FetchGeneralMessage("1101");//("695");
					GetOldDataXml();
					HideButtons();
                    HidButtonsOnReject();
                    //open popup for reject reason 
                    Status = "REJECT";
                    
                    string JavascriptText = "<script> window.open('/cms/Policies/Aspx/PolicyRejectReson.aspx?CUSTOMER_ID=" + objProcessInfo.CUSTOMER_ID + "&POLICY_ID=" + objProcessInfo.POLICY_ID + "&POLICY_VERSION_ID=" + objProcessInfo.POLICY_VERSION_ID + "','PolicyReject','resizable=yes,scrollbars=yes,left=150,top=50,width=500,height=360'); </script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "ShowPolicyRejection", JavascriptText);

                    //redirect to policy genral info tab after reject
                    //Session["LoadedAfterSave"] = "true";
                    //ClientScript.RegisterStartupScript(this.GetType(), "LoadPage", "<script>parent.document.location.href='/Cms/Policies/aspx/PolicyTab.aspx?customer_id=" + objProcessInfo.CUSTOMER_ID.ToString() + "&POLICY_ID=" + objProcessInfo.POLICY_ID.ToString() + "&APP_ID=" + objProcessInfo.POLICY_ID.ToString() + "&APP_VERSION_ID=" + objProcessInfo.POLICY_VERSION_ID.ToString() + "&POLICY_VERSION_ID=" + objProcessInfo.POLICY_VERSION_ID.ToString() + "&POLICY_LOB=" + objProcessInfo.LOB_ID.ToString() + "'</script>");


				}
				else
                {
                    ClsMessages.SetCustomizedXml(GetLanguageCode());
					hidFormSaved.Value = "0";
					//Error occured
					hidDisplayBody.Value = "False";
					lblMessage.Text = ClsMessages.FetchGeneralMessage("602");
				}
				
				lblMessage.Visible = true;

				//Refresh the Policy Top.
				cltPolicyTop.CallPageLoad();

				//Updating the policy top,session and menus
				SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
			}
			catch(Exception objExp)
            {
                ClsMessages.SetCustomizedXml(GetLanguageCode());
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

		private void HideButtons()
		{
			btnReset.Visible = false;
			btnRollback.Visible = false;
			btnCommit.Visible = false;
			btnSave.Visible = false;
			btnCommitInProgress.Attributes.Add("style","display:none");

			//			btnDecline.Visible = false;
		}

		//		private void btnDecline_Click(object sender, System.EventArgs e)
		//		{
		//			try
		//			{
		//				ClsNewBusinessProcess objProcess = new ClsNewBusinessProcess();
		//
		//				ClsProcessInfo objProcessInfo = GetFormValues();
		//				objProcessInfo.COMPLETED_BY = int.Parse(GetUserId());
		//				objProcessInfo.COMPLETED_DATETIME = DateTime.Now;
		//				objProcessInfo.ROW_ID = int.Parse(hidROW_ID.Value);  
		//				//Commiting the process 
		//				if (objProcess.DeclinePolicy(objProcessInfo) == true)
		//				{
		//					//Committed successfully
		//					hidFormSaved.Value = "1";
		//					hidDisplayBody.Value = "True";
		//					lblMessage.Text = ClsMessages.FetchGeneralMessage("712");
		//					GetOldDataXml();
		//					HideButtons();
		//				}
		//				else
		//				{
		//					//Error occured
		//					hidDisplayBody.Value = "False";
		//					lblMessage.Text = ClsMessages.FetchGeneralMessage("713");
		//				}
		//				
		//				lblMessage.Visible = true;
		//
		//				//Refresh the Policy Top.
		//				cltPolicyTop.CallPageLoad();
		//
		//				//Updating the policy top,session and menus
		//				SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
		//			}
		//			catch(Exception objExp)
		//			{
		//				lblMessage.Text = "Following error occured while rollbacking process. \n" 
		//					+ objExp.Message + "\n Please try later.";
		//				lblMessage.Visible = true;
		//				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
		//			}
		//		}
		/// <summary>
		/// Get the premium 
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
                ClsMessages.SetCustomizedXml(GetLanguageCode());
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1224")//"Following error occured. \n" 
					+ objExp.Message + "\n Please try later.";
				lblMessage.Visible = true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
			}
		}
		/// chk for application referred vs rejected cases
		private void ChkReferredRejCase(string strRulesHTML, string strCalledFrom)
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
                    ClsMessages.SetCustomizedXml(GetLanguageCode());
					if(lblMessage.Text!="")
						lblMessage.Text+="<br>";
					if(objXmlNodeListRej.Item(0).InnerText=="0")
					{
						chkComitAynway.Visible=btnComitAynway.Visible=false;
						//lblMessage.Text = "Unable to commit process. Because Policy has been rejected as shown below." ;
						if(strCalledFrom==CalledFromPageLoad)
							lblMessage.Text += Cms.CmsWeb.ClsMessages.FetchGeneralMessage("914");
						else
							lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("911");
						lblMessage.Visible=true;
					}
					else if(objXmlNodeList.Item(0).InnerText=="0")
                    {
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
						chkComitAynway.Visible=btnComitAynway.Visible=true;
						chkComitAynway.Checked = false;						
						chkComitAynway.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("893");
						//lblMessage.Text = "Unable to commit process. Because Policy has been referred as shown below." ;
						if(strCalledFrom==CalledFromPageLoad)
							lblMessage.Text += Cms.CmsWeb.ClsMessages.FetchGeneralMessage("915");
						else
							lblMessage.Text += Cms.CmsWeb.ClsMessages.FetchGeneralMessage("912");
						lblMessage.Visible=true;
					}				
				}
				else
                {
                    ClsMessages.SetCustomizedXml(GetLanguageCode());
					//lblMessage.Text = "Unable to commit process. Please fill the mandatory information as shown below." ;
					if(strCalledFrom==CalledFromPageLoad)
						lblMessage.Text += " "+ Cms.CmsWeb.ClsMessages.FetchGeneralMessage("916");
					else
						lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("913");
					lblMessage.Visible=true;
				}
                
				
			}
			catch(Exception ex)
			{
				lblMessage.Text = "Following error occured. \n" 
					+ ex.Message + "\n Please try later.";
				lblMessage.Visible = true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
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
				if(AgencyTerminationFlag==1)
				{
					Cms.BusinessLayer.BlProcess.clsprocess objProcess = new clsprocess();

					objProcess.SystemID = CarrierSystemID; 

					string strRulesHTML = objProcess.strHTML(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value),out valid,out strRulesStatus,"NBS");
			
					if(valid && strRulesStatus == "0") // then commit
					{
						valid=true;
					}
					else
					{
						// show rules msg		
					
				
						// chk here for referred/rejected cases
						ChkReferredRejCase(strRulesHTML,CalledFromPageLoad);
                        //Added By Lalit March 28,2011.
                        //if user not equivalent to supervisor btncommit should not be available
                        if (getIsUserSuperVisor() != "Y") 
                        {
                            btnComitAynway.Visible = false;
                        }
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
		

		private void btnComitAynway_Click(object sender, System.EventArgs e)
		{
			try
			{
				ClsProcessInfo objProcessInfo = GetFormValues ();
				ClsNewBusinessProcess objProcess = new ClsNewBusinessProcess ();				
				objProcessInfo.PROCESS_ID = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_NEW_BUSINESS_PROCESS; 
				objProcessInfo.ROW_ID = int.Parse(hidROW_ID.Value); 
				objProcessInfo.COMPLETED_BY = int.Parse(GetUserId());
				objProcessInfo.COMPLETED_DATETIME = DateTime.Now;				
				//Commiting the process 
				if (objProcess.CommitProcess (objProcessInfo,COMMIT_ANYWAYS_RULES) == true)
				{			
					if(ClsPolicyProcess.PrintingErrorFlag)
					{
						CommitSuccess();
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
						lblMessage.Text = ClsMessages.FetchGeneralMessage("908");
						lblMessage.Visible = true;
					}
					else
					{
						//Committed successfully
						hidFormSaved.Value = "1";
						hidDisplayBody.Value = "True";
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
						lblMessage.Text = ClsMessages.FetchGeneralMessage("688");
						GetOldDataXml();
						//Display the Buttons
						//btnPrint_Preview.Visible = true;
						btnGenerate_Policy.Visible = true;
						//					btnCommit_To_Spool.Visible = true;
						HideButtons();
					}
				}
				else
				{
					//Error occured
					//hidDisplayBody.Value = "False";
					//lblMessage.Text = ClsMessages.FetchGeneralMessage("601");
					lblMessage.Text = Cms.BusinessLayer.BlProcess.ClsPolicyErrMsg.strMessage;
					if(ClsPolicyProcess.PrintingErrorFlag)
                    {
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
						lblMessage.Text = ClsMessages.FetchGeneralMessage("1017");							
					}
						
				}
				
				lblMessage.Visible = true;
				//Refresh the Policy Top.
				cltPolicyTop.CallPageLoad();
				//Updating the policy top,session and menus
				SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
			}
			catch(System.DllNotFoundException ex)
			{
				CommitSuccess();
				lblMessage.Text = ClsMessages.FetchGeneralMessage("908");
				lblMessage.Visible = true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			catch(Exception objExp)
			{
				hidFormSaved.Value = "0";
				lblMessage.Text = objExp.Message + "\n Please try later.";
				lblMessage.Visible = true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
			}
		}
        private void HidButtonsOnReject() 
        {
            btnPolicyDetails.Attributes.Add("style", "display:none");
            btnBack_To_Search.Attributes.Add("style", "display:none");
            btnBack_To_Customer_Assistant.Attributes.Add("style", "display:none");
            btnGet_Premium.Attributes.Add("style", "display:none");
        }

	}
}
