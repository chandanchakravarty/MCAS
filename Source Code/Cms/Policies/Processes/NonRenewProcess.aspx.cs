/******************************************************************************************
<Author				: -  
<Start Date			: -	 
<End Date			: -	 
<Description		: -  
<Review Date		: - 
<Reviewed By		: - 	

Modification History
<Modified By		: - Vijay Arora
<Modified Date		: - 23-01-2006
<Purpose			: - Added the Process Launch, Rollback and Complted functionality. 
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
using System.Reflection;
using System.Resources;  
using Cms.CmsWeb.Controls;
using Cms.Model.Policy.Process;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb;
using Cms.BusinessLayer.BlProcess;
using System.Xml;


namespace Policies.Processes
{
	/// <summary>
	/// Summary description for NonRenewProcess.
	/// </summary>
	public class NonRenewProcess : Cms.Policies.policiesbase
	{
		
		#region Declarations
		protected System.Web.UI.WebControls.Label capEFFECTIVE_DATETIME;
		protected System.Web.UI.WebControls.TextBox txtEFFECTIVE_DATETIME;
		protected System.Web.UI.WebControls.HyperLink hlkEFFECTIVE_DATETIME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEFFECTIVE_DATETIME;
		protected System.Web.UI.WebControls.Label capReason;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.WebControls.DropDownList cmbReason;
		protected System.Web.UI.WebControls.Label capOTHER_REASON;
		protected Cms.CmsWeb.WebControls.PolicyTop cltPolicyTop;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvReason;
		protected System.Web.UI.WebControls.TextBox txtOTHER_REASON;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidROW_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDisplayBody;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEFFECTIVE_DATETIME;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnBackToCustomer;
		protected Cms.CmsWeb.Controls.CmsButton btnBackToSearch;
		protected Cms.CmsWeb.Controls.CmsButton btnGenNonRenew;
		protected Cms.CmsWeb.Controls.CmsButton btnPrint;
		protected Cms.CmsWeb.Controls.CmsButton btnCommit;
		protected Cms.CmsWeb.Controls.CmsButton btnComplete;
		protected Cms.CmsWeb.Controls.CmsButton btnPolicyDetails;
        
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		System.Resources.ResourceManager objResourceMgr;
		protected Cms.CmsWeb.Controls.CmsButton btnRollback;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPROCESS_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList Dropdownlist2;
		protected System.Web.UI.WebControls.Label capAGENTPHONENO;
		protected System.Web.UI.WebControls.Label lblAGENT_PHONE_NUMBER;
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
		protected System.Web.UI.HtmlControls.HtmlInputButton btnAssignLossCodes;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnUnAssignLossCodes;
		protected System.Web.UI.WebControls.CustomValidator Customvalidator1;
		protected System.Web.UI.WebControls.RequiredFieldValidator Requiredfieldvalidator2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidADD_INT_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidUNDERWRITER;
		protected System.Web.UI.WebControls.Label capCANCELLATION_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbCANCELLATION_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCANCELLATION_TYPE;
		protected System.Web.UI.WebControls.Label capNON_RENEW_OPTION;
		protected System.Web.UI.WebControls.DropDownList cmbNON_RENEW_OPTION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNON_RENEW_OPTION;
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
		protected System.Web.UI.WebControls.CustomValidator csvOTHER_REASON;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvOTHER_REASON;
		protected System.Web.UI.WebControls.Label capCANCELLATION_OPTION;
		protected System.Web.UI.WebControls.DropDownList cmbCANCELLATION_OPTION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCANCELLATION_OPTION;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidNEW_POLICY_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		protected Cms.CmsWeb.Controls.CmsButton btnCommitInProgress;
        protected System.Web.UI.WebControls.Label capMessage;
        protected System.Web.UI.WebControls.Label capHeader;
        protected System.Web.UI.WebControls.Label capAdditional;
        protected System.Web.UI.WebControls.Label capPrinting;
        protected System.Web.UI.WebControls.CustomValidator csvEFFECTIVE_HOUR;
		bool Agency_Flag;
		bool Agency_NotificationFlag;
		protected System.Web.UI.WebControls.RangeValidator rngEFFECTIVE_DATE;
		DateTime APP_EFFECTIVE_DATE,APP_EXPIRATION_DATE ;
		protected System.Web.UI.WebControls.Label capINCLUDE_REASON_DESC;
		protected System.Web.UI.WebControls.CheckBox chkINCLUDE_REASON_DESC;
		ClsEndorsmentProcess objProcess=new ClsEndorsmentProcess();
		#endregion

		#region PageLoad
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			((cmsbase) this).ScreenId = "5000_30";
			SetButtonsSecurityXML();
		
			//Javascript Implementation for Calender
			hlkEFFECTIVE_DATETIME.Attributes.Add("OnClick","fPopCalendar(document.NonRenewProcess.txtEFFECTIVE_DATETIME,document.NonRenewProcess.txtEFFECTIVE_DATETIME)"); 				
			cmbReason.Attributes.Add("Onchange","javascript:ShowTextFiled()");
			btnPolicyDetails.Attributes.Add("onclick","javascript:return ShowDetailsPolicy();");
			btnReset.Attributes.Add("onclick","javascript:return formReset();");
			cmbADD_INT.Attributes.Add("onChange","javascript:return cmbADD_INT_Change();");
			chkSEND_ALL.Attributes.Add("onClick","javascript: chkSEND_ALL_Change();");
			btnSave.Attributes.Add("onClick","javascript: GetAssignAddInt();Page_ClientValidate();return Page_IsValid;");
			btnComplete.Attributes.Add("onClick","javascript:GetAssignAddInt();HideShowCommitInProgress();Page_ClientValidate();return Page_IsValid;");  	
			btnRollback.Attributes.Add("onClick","javascript:HideShowCommit();");  
			cmbMERIDIEM.Attributes.Add("onchange","javascript:disableHourValidator();");//Added by Charles on 1-Sep-09 for Itrack 6323
			//cmbPRINT_COMMENTS.Attributes.Add("onchange","javascript:CommentEnable();");
			cltPolicyTop.UseRequestVariables = false;

			
			objResourceMgr = new ResourceManager("Policies.Processes.NonRenewProcess" ,Assembly.GetExecutingAssembly());			
			if(!Page.IsPostBack)
			{
				SetHiddenFields();
				
				cltPolicyTop.CustomerID = int.Parse(hidCUSTOMER_ID.Value);
				cltPolicyTop.PolicyID = int.Parse(hidPOLICY_ID.Value);
				cltPolicyTop.PolicyVersionID = int.Parse(hidPOLICY_VERSION_ID.Value);

				Setcaption();
				SetValidators();
				SetCombobox();
                capMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
                capHeader.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1236");


				if (hidPROCESS_ID.Value == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_NON_RENEWAL_PROCESS.ToString())
				{
					objProcess.BeginTransaction();
					if (objProcess.CheckProcessEligibility(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value), int.Parse(hidPROCESS_ID.Value))==1)
					{
						//Starting the process
						setDefaultValues();
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
			}


		}

		#endregion
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
				}

				if (dsTemp.Tables[0].Rows[0]["EFFECTIVE_DATETIME"] != DBNull.Value)
				{
                    txtEFFECTIVE_DATETIME.Text = Convert.ToDateTime(ConvertDBDateToCulture(dsTemp.Tables[0].Rows[0]["EFFECTIVE_DATETIME"].ToString())).ToShortDateString();
				}
				
				if (dsTemp.Tables[0].Rows[0]["CANCELLATION_TYPE"] != DBNull.Value && dsTemp.Tables[0].Rows[0]["CANCELLATION_TYPE"].ToString().Trim()!="" && dsTemp.Tables[0].Rows[0]["CANCELLATION_TYPE"].ToString().Trim()!="0")
				{
					cmbCANCELLATION_TYPE.SelectedValue = dsTemp.Tables[0].Rows[0]["CANCELLATION_TYPE"].ToString();
				}
				
				if (dsTemp.Tables[0].Rows[0]["REASON"] != DBNull.Value && dsTemp.Tables[0].Rows[0]["REASON"].ToString().Trim() != ""  && dsTemp.Tables[0].Rows[0]["REASON"].ToString().Trim()!="0")
				{
					cmbReason.SelectedIndex =  cmbReason.Items.IndexOf(cmbReason.Items.FindByValue(dsTemp.Tables[0].Rows[0]["REASON"].ToString())) ;
				}
				if (dsTemp.Tables[0].Rows[0]["OTHER_REASON"] != DBNull.Value)
				{
					txtOTHER_REASON.Text = dsTemp.Tables[0].Rows[0]["OTHER_REASON"].ToString();
				}
                //hidOldData.Value = dsTemp.GetXml();

                //Shikha - itrack - 1230
                #region Remove Return EFFECTIVE_DATETIME node from Xml
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(dsTemp.GetXml());
                XmlNode xNodeMAIN = xDoc.SelectSingleNode("NewDataSet/Table");
                if (xNodeMAIN != null)
                {
                    XmlNode xNode = xNodeMAIN.SelectSingleNode("EFFECTIVE_DATETIME"); //xDoc.NextSibling.SelectSingleNode("RETURN_PREMIUM");
                    if (xNode != null)
                    {
                        xNodeMAIN.RemoveChild(xNode);
                        hidOldData.Value = xDoc.InnerXml.ToString();
                    }
                }

                #endregion
			}
		}

		#region Get Old Data XML

		private void GetOldXml()
		{
				if (hidROW_ID.Value.Trim() != "")
				{
					hidOldData.Value = objProcess.GetOldDataXml(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value),
						int.Parse(hidPOLICY_VERSION_ID.Value), int.Parse(hidROW_ID.Value));

                    //Shikha - itrack - 1230
                    #region Remove Return EFFECTIVE_DATETIME node from Xml
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.LoadXml(hidOldData.Value);
                    XmlNode xNodeMAIN = xDoc.SelectSingleNode("NewDataSet/Table");
                    if (xNodeMAIN != null)
                    {
                        XmlNode xNode = xNodeMAIN.SelectSingleNode("EFFECTIVE_DATETIME"); //xDoc.NextSibling.SelectSingleNode("RETURN_PREMIUM");
                        if (xNode != null)
                        {
                            xNodeMAIN.RemoveChild(xNode);
                            hidOldData.Value = xDoc.InnerXml.ToString();
                        }
                    }

                    #endregion
				}

		}

		#endregion

		#region Get and Set Security Settings
		private void SetButtonsSecurityXML()
		{
			btnSave.CmsButtonClass = CmsButtonType.Write;
			btnSave.PermissionString = gstrSecurityXML;

			btnReset.CmsButtonClass = CmsButtonType.Read;
			btnReset.PermissionString = gstrSecurityXML;
            
			btnBackToCustomer.CmsButtonClass =CmsButtonType.Read;
			btnBackToCustomer.PermissionString = gstrSecurityXML;

			btnBackToSearch.CmsButtonClass=CmsButtonType.Read;
			btnBackToSearch.PermissionString=gstrSecurityXML;

			btnGenNonRenew.CmsButtonClass=CmsButtonType.Write;
			btnGenNonRenew.PermissionString=gstrSecurityXML;

			btnPrint.CmsButtonClass=CmsButtonType.Write;
			btnPrint.PermissionString =gstrSecurityXML ;

			btnCommit.CmsButtonClass =CmsButtonType.Write;
			btnCommit.PermissionString=gstrSecurityXML ;	

			btnRollback.CmsButtonClass =CmsButtonType.Write;
			btnRollback.PermissionString=gstrSecurityXML ;	

			btnComplete.CmsButtonClass =CmsButtonType.Write;
			btnComplete.PermissionString=gstrSecurityXML ;	

			
			btnPolicyDetails.CmsButtonClass = CmsButtonType.Read;
			btnPolicyDetails.PermissionString = gstrSecurityXML;

			btnCommitInProgress.CmsButtonClass = CmsButtonType.Read;
			btnCommitInProgress.PermissionString = gstrSecurityXML;


		}

		#endregion

		#region Get Form Values
		private ClsProcessInfo GetFormValues()
		{
			ClsProcessInfo objProcessInfo = new ClsProcessInfo();

			objProcessInfo.CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);
			objProcessInfo.POLICY_ID = int.Parse(hidPOLICY_ID.Value);
			objProcessInfo.POLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value);
			objProcessInfo.NEW_CUSTOMER_ID  = int.Parse(hidCUSTOMER_ID.Value);
			objProcessInfo.NEW_POLICY_ID = int.Parse(hidPOLICY_ID.Value);
			if (hidNEW_POLICY_VERSION_ID.Value !="" && hidNEW_POLICY_VERSION_ID.Value !="0")
				objProcessInfo.NEW_POLICY_VERSION_ID = int.Parse(hidNEW_POLICY_VERSION_ID.Value);
			else
				objProcessInfo.NEW_POLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value);
			objProcessInfo.POLICY_CURRENT_STATUS = objProcess.GetPolicyStatus(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.NEW_POLICY_VERSION_ID);  //GetPolicyStatus();
			objProcessInfo.POLICY_PREVIOUS_STATUS = objProcess.GetPolicyStatus(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.POLICY_VERSION_ID);  //GetPolicyStatus();
			if (hidLOB_ID.Value!="" && hidLOB_ID.Value!="0")
				objProcessInfo.LOB_ID = int.Parse(hidLOB_ID.Value); 
			//objProcess.PROCESS_ID = POLICY_NON_RENEWAL_PROCESS;
			if (txtEFFECTIVE_DATETIME.Text != "")
				objProcessInfo.EFFECTIVE_DATETIME = ConvertToDate(txtEFFECTIVE_DATETIME.Text);
			/*if (cmbREQUESTED_BY.SelectedItem.Value != "")
				objProcess.REQUESTED_BY  = Convert.ToInt32(cmbREQUESTED_BY.SelectedItem.Value) ;
            */
			if(cmbReason.SelectedItem.Value!="")
			  objProcessInfo.REASON =Convert.ToInt32(cmbReason.SelectedItem.Value) ;  
			if(txtOTHER_REASON.Text!="" ) //&& (cmbReason.SelectedItem.Value =="11510" || cmbReason.SelectedItem.Value =="11505"  || cmbReason.SelectedItem.Value =="11517"  || cmbReason.SelectedItem.Value =="11528"  || cmbReason.SelectedItem.Value =="11523" ))
              objProcessInfo.OTHER_REASON=txtOTHER_REASON.Text ; 
			///////
			if (txtEFFECTIVE_DATETIME.Text != "")
			{
				DateTime EffDate = ConvertToDate(txtEFFECTIVE_DATETIME.Text);

				int Hr = int.Parse(txtEFFECTIVE_HOUR.Text);
				
				if(Hr==12 && cmbMERIDIEM.SelectedIndex==0) //If added by Charles on 31-Aug-09 for Itrack 6323
				{
					Hr=00;
				}
				if(cmbMERIDIEM.SelectedIndex == 1 && Hr!=12) //Added condition Hr!=12 by Charles on 31-Aug-09 for Itrack 6323
				{
					Hr+=12;
				}
				/*if(Hr==24) //Commented by Charles on 31-Aug-09 for Itrack 6323
				{
					Hr=00;
				}*/

				objProcessInfo.EFFECTIVE_DATETIME = new DateTime(EffDate.Year, EffDate.Month, EffDate.Day
					, Hr, int.Parse(txtEFFECTIVE_MINUTE.Text)
					, 0);
				objProcessInfo.EFFECTIVE_TIME=objProcessInfo.EFFECTIVE_DATETIME.ToShortTimeString();
			}
			/*
			if(txtRETURN_PREMIUM.Text.Trim()!="")
				objProcess.RETURN_PREMIUM  = Convert.ToDouble(txtRETURN_PREMIUM.Text.Trim());			
		    
			if (cmbPRINT_COMMENTS.SelectedItem != null)
				objProcess.PRINT_COMMENTS = cmbPRINT_COMMENTS.SelectedValue;

			if (cmbPRINT_COMMENTS.SelectedValue != "0")
				objProcess.COMMENTS = txtCOMMENTS.Text;
			else
				objProcess.COMMENTS = "";
			*/
			if ( cmbCANCELLATION_TYPE.SelectedValue !="")
			  objProcessInfo.CANCELLATION_TYPE = int.Parse(cmbCANCELLATION_TYPE.SelectedValue);  
			if (cmbCANCELLATION_OPTION.SelectedValue !="")
				objProcessInfo.CANCELLATION_OPTION = int.Parse(cmbCANCELLATION_OPTION.SelectedValue);  
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
			
			if(cmbADD_INT.SelectedItem!=null && cmbADD_INT.SelectedItem.Value!="")
			{
				objProcessInfo.ADD_INT = int.Parse(cmbADD_INT.SelectedItem.Value);

				if(objProcessInfo.ADD_INT==int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.COMMIT_PRINT_SPOOL).ToString()))
				{
					objProcessInfo.ADD_INT_ID = hidADD_INT_ID.Value;
					if(chkSEND_ALL.Checked==true)
						objProcessInfo.SEND_ALL = (int)(enumYESNO_LOOKUP_CODE.YES);
					else
						objProcessInfo.SEND_ALL = (int)(enumYESNO_LOOKUP_CODE.NO);
				}
			}
			if(chkINCLUDE_REASON_DESC.Checked==true)
				objProcessInfo.INCLUDE_REASON_DESC = "Y";//(string)enumYESNO_LOOKUP_CODE.YES.ToString();
			else
				objProcessInfo.INCLUDE_REASON_DESC = "N";//(string)enumYESNO_LOOKUP_CODE.NO.ToString();

			//
       		return objProcessInfo;
		}

		#endregion

		#region Fill Drop Downs
		private void setDefaultValues()
		{
			ClsPolicyProcess objPolProcess = new ClsPolicyProcess(); 
			if(objPolProcess.AgenyTerminationVerification(int.Parse(hidCUSTOMER_ID.Value) ,int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value)) !=1)
			{
				Agency_Flag=true;
				if (objPolProcess.AgenyNotificationVerification(int.Parse(hidCUSTOMER_ID.Value) ,int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value))==1)
					Agency_NotificationFlag =true;
				else
					Agency_NotificationFlag =false;
			}
			else
				Agency_Flag=false;
			txtEFFECTIVE_DATETIME.Text=APP_EXPIRATION_DATE.ToShortDateString();
			//agency terminated
			if (Agency_Flag)  
			{
				string lobValue="";
				lobValue=GetLOBString();
				if (Agency_NotificationFlag) //Agecny Notification 
				{
					cmbCANCELLATION_TYPE.SelectedIndex = cmbCANCELLATION_TYPE.Items.IndexOf(cmbCANCELLATION_TYPE.Items.FindByValue(((int)clsprocess.enumPROC_CANCEL_TYPE.AGENCY_TERMINATE_NOTIFICATION).ToString())); //for Non Renewal default value
					cmbINSURED.SelectedIndex =cmbINSURED.Items.IndexOf(cmbINSURED.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.ON_DEMAND).ToString()));    
					cmbAGENCY_PRINT.SelectedIndex =cmbAGENCY_PRINT.Items.IndexOf(cmbAGENCY_PRINT.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.COMMIT_PRINT_SPOOL).ToString()));    
					cmbADD_INT.SelectedIndex =cmbADD_INT.Items.IndexOf(cmbADD_INT.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.COMMIT_PRINT_SPOOL).ToString()));    
				}
				else // No Agecny Notification 
				{
					cmbCANCELLATION_TYPE.SelectedIndex = cmbCANCELLATION_TYPE.Items.IndexOf(cmbCANCELLATION_TYPE.Items.FindByValue(((int)clsprocess.enumPROC_CANCEL_TYPE.AGENCY_TERMINATE_NO_NOTIFICATION).ToString())); //for Non Renewal default value
					cmbINSURED.SelectedIndex =cmbINSURED.Items.IndexOf(cmbINSURED.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.NOT_REQUIRED).ToString()));    
					cmbAGENCY_PRINT.SelectedIndex =cmbAGENCY_PRINT.Items.IndexOf(cmbAGENCY_PRINT.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.NOT_REQUIRED).ToString()));    
					cmbADD_INT.SelectedIndex =cmbADD_INT.Items.IndexOf(cmbADD_INT.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.NOT_REQUIRED).ToString()));    
				}
				cmbCANCELLATION_OPTION.SelectedIndex = cmbCANCELLATION_OPTION.Items.IndexOf(cmbCANCELLATION_OPTION.Items.FindByValue("11995")); // 11995 ;//Flat
				if(lobValue=="HOME" || lobValue=="RENT" || lobValue=="RENTAL")
					cmbReason.SelectedIndex = cmbReason.Items.IndexOf(cmbReason.Items.FindByValue("13049")); 
				else if(lobValue=="WAT")
					cmbReason.SelectedIndex = cmbReason.Items.IndexOf(cmbReason.Items.FindByValue("13050")); 
				else if(lobValue=="MOT" || lobValue=="PPA" || lobValue=="APP" || lobValue=="AUTO")
					cmbReason.SelectedIndex = cmbReason.Items.IndexOf(cmbReason.Items.FindByValue("13051")); 
				else if(lobValue=="UMB")
					cmbReason.SelectedIndex = cmbReason.Items.IndexOf(cmbReason.Items.FindByValue("13053")); 
				else 
					cmbReason.SelectedIndex = cmbReason.Items.IndexOf(cmbReason.Items.FindByValue("13052")); 
			}
			else //Non Renewal WMIC
			{
				Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGen = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();    
				DataSet dsPolicy=objGen.GetPolicyDataSet(int.Parse(hidCUSTOMER_ID.Value) ,int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value));  
				if(dsPolicy.Tables[0].Rows[0]["NOT_RENEW_REASON"]!=System.DBNull.Value) 
				{
					string strReason=dsPolicy.Tables[0].Rows[0]["NOT_RENEW_REASON"].ToString() ;    //Non Renewal / Underwriting Reason . Pull the Reason Description from the Renewal Instruction Tab - Non Renewal Details
					cmbReason.SelectedIndex = cmbReason.Items.IndexOf(cmbReason.Items.FindByValue(strReason)); 
				}
				txtOTHER_REASON.Text =dsPolicy.Tables[0].Rows[0]["NOT_RENEW_REASON_DESC"].ToString();
				dsPolicy.Clear();
				dsPolicy.Dispose(); 
				cmbCANCELLATION_TYPE.SelectedIndex = cmbCANCELLATION_TYPE.Items.IndexOf(cmbCANCELLATION_TYPE.Items.FindByValue(((int)clsprocess.enumPROC_CANCEL_TYPE.NON_RENEWAL).ToString())); //for Non Renewal default value
				cmbCANCELLATION_OPTION.SelectedIndex = cmbCANCELLATION_OPTION.Items.IndexOf(cmbCANCELLATION_OPTION.Items.FindByValue("13028")); // 13028;//NOt Applicable
				cmbINSURED.SelectedIndex =cmbINSURED.Items.IndexOf(cmbINSURED.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.ON_DEMAND).ToString()));    
				cmbAGENCY_PRINT.SelectedIndex =cmbAGENCY_PRINT.Items.IndexOf(cmbAGENCY_PRINT.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.COMMIT_PRINT_SPOOL).ToString()));    
				cmbADD_INT.SelectedIndex =cmbADD_INT.Items.IndexOf(cmbADD_INT.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.COMMIT_PRINT_SPOOL).ToString()));    
			}

		}
		private void SetCombobox()
		{
			
			//
			cmbMERIDIEM.Items.Insert(0,"AM");
			cmbMERIDIEM.Items[0].Value = "0";
			cmbMERIDIEM.Items.Insert(1,"PM");
			cmbMERIDIEM.Items[1].Value = "1";

			cmbCANCELLATION_TYPE.DataSource		=	Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("NNREN");
			cmbCANCELLATION_TYPE.DataTextField	=	"LookupDesc";
			cmbCANCELLATION_TYPE.DataValueField	=	"LookupID";
			cmbCANCELLATION_TYPE.DataBind();
			cmbCANCELLATION_TYPE.Items.Insert(0,"");
		
			cmbCANCELLATION_TYPE.SelectedIndex = cmbCANCELLATION_TYPE.Items.IndexOf(cmbCANCELLATION_TYPE.Items.FindByValue("11991")); //for Non Renewal default value
			//cmbCANCELLATION_TYPE.Enabled =false;
		
			cmbCANCELLATION_OPTION.DataSource		=	Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CANOPT",null,"S");
			cmbCANCELLATION_OPTION.DataTextField	=	"LookupDesc";
			cmbCANCELLATION_OPTION.DataValueField	=	"LookupID";
			cmbCANCELLATION_OPTION.DataBind();		
			cmbCANCELLATION_OPTION.SelectedIndex = cmbCANCELLATION_OPTION.Items.IndexOf(cmbCANCELLATION_OPTION.Items.FindByText("Not Applicable"));  //13028
			//
			/*ListItem lstItem = new ListItem("Not Applicable","0"); 
			cmbNON_RENEW_OPTION.Items.Add (lstItem); 
			*/
			string lobValue="";
			lobValue=GetLOBString();
			if(lobValue=="HOME" || lobValue=="RENT" || lobValue=="RENTAL")
			{
				cmbReason.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("HNRPS","-1","Y");
			}
			else if(lobValue=="WAT")
			{
				cmbReason.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("WNRPS","-1","Y");
			}
			else if(lobValue=="MOT" || lobValue=="PPA" || lobValue=="APP" || lobValue=="AUTO")
			{
				cmbReason.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("MANRPS","-1","Y");

			}
			else if(lobValue=="UMB")
			{
				cmbReason.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("UNRPS","-1","Y");

			}
			else 
			{
				cmbReason.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("GWNRPS","-1","Y");

			}

			cmbReason.DataTextField = "LookupDesc";
			cmbReason.DataValueField = "LookupID";
			cmbReason.DataBind();
			cmbReason.Items.Insert(0,new ListItem("",""));
			//Non Renewal / Underwriting Reason default
			if(lobValue=="HOME" || lobValue=="RENT" || lobValue=="RENTAL")
				cmbReason.SelectedIndex = cmbReason.Items.IndexOf(cmbReason.Items.FindByValue("13021")); 
			else if(lobValue=="WAT")
				cmbReason.SelectedIndex = cmbReason.Items.IndexOf(cmbReason.Items.FindByValue("13022")); 
			else if(lobValue=="MOT" || lobValue=="PPA" || lobValue=="APP" || lobValue=="AUTO")
				cmbReason.SelectedIndex = cmbReason.Items.IndexOf(cmbReason.Items.FindByValue("13023")); 
			else if(lobValue=="UMB")
				cmbReason.SelectedIndex = cmbReason.Items.IndexOf(cmbReason.Items.FindByValue("13025")); 
			else 
				cmbReason.SelectedIndex = cmbReason.Items.IndexOf(cmbReason.Items.FindByValue("13024")); 
			/*
			cmbREQUESTED_BY.Items.Insert(0,"");
			cmbREQUESTED_BY.Items[0].Value = "0";
			cmbREQUESTED_BY.Items.Insert(1,"Agent");
			cmbREQUESTED_BY.Items[1].Value = "1";
			cmbREQUESTED_BY.Items.Insert(2,"Company");
			cmbREQUESTED_BY.Items[2].Value = "2";
			cmbREQUESTED_BY.Items.Insert(3,"Customer");
			cmbREQUESTED_BY.Items[3].Value = "3";
			
			cmbPRINT_COMMENTS.Items.Insert(0,"No");
			cmbPRINT_COMMENTS.Items[0].Value = "0";
			cmbPRINT_COMMENTS.Items.Insert(1,"Yes");
			cmbPRINT_COMMENTS.Items[1].Value = "1";
			*/
			IList ListSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("PRNCAN");  //("PRNTOP"); //"PRNCAN"
			ListItem iListItem = null;			
			cmbINSURED.DataSource=ListSource;
			cmbINSURED.DataTextField="LookupDesc";
			cmbINSURED.DataValueField="LookupID";
			cmbINSURED.DataBind();

			iListItem = null;
			iListItem = cmbINSURED.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.DOWNLOAD).ToString());
			if(iListItem!=null)
				cmbINSURED.Items.Remove(iListItem);
			iListItem = null;
			iListItem = cmbINSURED.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.COMMIT_PRINT_SPOOL).ToString());
			if(iListItem!=null)
				cmbINSURED.Items.Remove(iListItem);
			//iListItem = cmbINSURED.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.NOT_REQUIRED).ToString() );
			//if(iListItem!=null)
			//	cmbINSURED.Items.Remove(iListItem);
			iListItem = cmbINSURED.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.MICHIGAN_MAILERS).ToString());
			if(iListItem!=null)
				cmbINSURED.Items.Remove(iListItem);

			cmbINSURED.SelectedIndex =cmbINSURED.Items.IndexOf(cmbINSURED.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.ON_DEMAND).ToString()));    
			
			cmbADD_INT.DataSource=ListSource;
			cmbADD_INT.DataTextField="LookupDesc";
			cmbADD_INT.DataValueField="LookupID";
			cmbADD_INT.DataBind();
			iListItem = null;
			iListItem = cmbADD_INT.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.DOWNLOAD).ToString());
			if(iListItem!=null)
				cmbADD_INT.Items.Remove(iListItem);
			iListItem = null;
			//iListItem = cmbADD_INT.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.NOT_REQUIRED).ToString());
			//if(iListItem!=null)
			//	cmbADD_INT.Items.Remove(iListItem);
			iListItem = cmbADD_INT.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.MICHIGAN_MAILERS).ToString());
			if(iListItem!=null)
				cmbADD_INT.Items.Remove(iListItem);
			cmbADD_INT.SelectedIndex =cmbADD_INT.Items.IndexOf(cmbADD_INT.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.COMMIT_PRINT_SPOOL).ToString()));    

			cmbAGENCY_PRINT.DataSource=ListSource;
			cmbAGENCY_PRINT.DataTextField="LookupDesc";
			cmbAGENCY_PRINT.DataValueField="LookupID";
			cmbAGENCY_PRINT.DataBind();

			iListItem = null;
			iListItem = cmbAGENCY_PRINT.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.ON_DEMAND).ToString());
			if(iListItem!=null)
				cmbAGENCY_PRINT.Items.Remove(iListItem);
			//iListItem = cmbAGENCY_PRINT.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.NOT_REQUIRED).ToString() );
			//if(iListItem!=null)
			//	cmbAGENCY_PRINT.Items.Remove(iListItem);
			iListItem = cmbAGENCY_PRINT.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.MICHIGAN_MAILERS).ToString());
			if(iListItem!=null)
				cmbAGENCY_PRINT.Items.Remove(iListItem);
			chkSEND_ALL.Checked =true; 
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

		#endregion

		#region Set Validator and Captions
		private void SetValidators()
		{
          revEFFECTIVE_DATETIME.ValidationExpression =aRegExpDate;
          revEFFECTIVE_DATETIME.ErrorMessage			= ClsMessages.FetchGeneralMessage("22");
          rfvEFFECTIVE_DATETIME.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1234");//"Please enter the effective date";
          //rfvREQUESTED_BY.ErrorMessage =ClsMessages.GetMessage(base.ScreenId,"1");
		 // rfvCANCELLATION_TYPE.ErrorMessage		=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("889");
          rfvReason.ErrorMessage =ClsMessages.GetMessage(base.ScreenId,"2");
          rfvCANCELLATION_OPTION.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1937");
          csvEFFECTIVE_HOUR.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1938");
          rfvEFFECTIVE_HOUR.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1939");
          rfvEFFECTIVE_MINUTE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1939");
          rfvMERIDIEM.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1940");
          rfvCANCELLATION_TYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1941");
			rngEFFECTIVE_DATE.MinimumValue = APP_EFFECTIVE_DATE.ToString("d"); 
			rngEFFECTIVE_DATE.MaximumValue = APP_EXPIRATION_DATE.ToString("d");
            rngEFFECTIVE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1235");//"Date should be between policy effective date & expiry date";
            rnvtEFFECTIVE_MINUTE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1935");
            rnvEFFECTIVE_HOUR.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1934");
            rfvOTHER_REASON.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1229");//"Please insert Reason description.";
            rfvReason.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1210");//"Please select Reason.";	
			//this.csvCOMMENTS.ErrorMessage			= "Please enter only 250 characters.";
			//Uncommented by Charles on 12-Aug-09 for Itrack 6251
            this.csvOTHER_REASON.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1211");//"Please enter only 250 characters.";
			//rfvCOMMENTS.ErrorMessage				= "Please enter comments.";
			//revRETURN_PREMIUM.ValidationExpression	= aRegExpDoublePositiveNonZero;
			//revRETURN_PREMIUM.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"116");
		}
		
		
		private void  Setcaption()
		{
			//capEFFECTIVE_DATETIME.Text		=objResourceMgr.GetString("txtEFFECTIVE_DATETIME"); //Commented by Charles on 31-Aug-09 for Itrack 6323
			capCANCELLATION_TYPE.Text		=objResourceMgr.GetString("cmbCANCELLATION_TYPE");
			capReason.Text					=objResourceMgr.GetString("cmbREASON");
			capOTHER_REASON.Text			=objResourceMgr.GetString("txtOTHER_REASON");
			this.capCANCELLATION_OPTION.Text	= objResourceMgr.GetString("cmbCANCELLATION_OPTION");
			this.capEFFECTIVE_DATETIME.Text  	= objResourceMgr.GetString("txtEFFECTIVE_DATETIME");
			this.capEFFECTIVE_TIME.Text		= objResourceMgr.GetString("txtEFFECTIVE_TIME");
			//this.capPAST_DUE_PREMIUM.Text	= objResourceMgr.GetString("txtPAST_DUE_PREMIUM");
			//this.capPRINT_COMMENTS.Text		= objResourceMgr.GetString("cmbPRINT_COMMENTS");
			//this.capRETURN_PREMIUM.Text		= objResourceMgr.GetString("txtRETURN_PREMIUM");	
			capINSURED.Text						=	objResourceMgr.GetString("cmbINSURED");			
			capPRINTING_OPTIONS.Text			=	objResourceMgr.GetString("chkPRINTING_OPTIONS");													
			capADD_INT.Text						=	objResourceMgr.GetString("cmbADD_INT");			
			capSEND_ALL.Text					=	objResourceMgr.GetString("chkSEND_ALL");										
			capAGENCY_PRINT.Text				=	objResourceMgr.GetString("cmbAGENCY_PRINT");
			capINCLUDE_REASON_DESC.Text			=	objResourceMgr.GetString("txtINCLUDE_REASON_DESC");
            btnReset.Text=Cms.CmsWeb.ClsMessages.GetButtonsText(base.ScreenId,"btnReset");
            btnComplete.Text=Cms.CmsWeb.ClsMessages.GetButtonsText(base.ScreenId,"btnComplete");
            btnCommitInProgress.Text = Cms.CmsWeb.ClsMessages.GetButtonsText(base.ScreenId, "btnCommitInProgress");
            btnSave.Text = Cms.CmsWeb.ClsMessages.GetButtonsText(base.ScreenId, "btnSave");
            btnGenNonRenew.Text = Cms.CmsWeb.ClsMessages.GetButtonsText(base.ScreenId, "btnGenNonRenew");
            btnBackToSearch.Text = Cms.CmsWeb.ClsMessages.GetButtonsText(base.ScreenId, "btnBackToSearch");
            btnPrint.Text=Cms.CmsWeb.ClsMessages.GetButtonsText(base.ScreenId, "btnPrint");
            btnBackToCustomer.Text=Cms.CmsWeb.ClsMessages.GetButtonsText(base.ScreenId, "btnBackToCustomer");
            btnRollback.Text = Cms.CmsWeb.ClsMessages.GetButtonsText("5000_30", "btnRollback");
            btnPolicyDetails.Text=Cms.CmsWeb.ClsMessages.GetButtonsText(base.ScreenId, "btnPolicyDetails");
            capPrinting.Text = objResourceMgr.GetString("capPrinting");
            capAdditional.Text = objResourceMgr.GetString("capAdditional");
            capUnassignLossCodes.Text = objResourceMgr.GetString("capUnassignLossCodes");
            capAssignedLossCodes.Text = objResourceMgr.GetString("capAssignedLossCodes");
           



        }
		private void SetHiddenFields()
		{
			if (Request.QueryString["Customer_ID"] != null && Request.QueryString["Customer_ID"].ToString() != "")
			{ 						
				hidCUSTOMER_ID.Value=Request.QueryString["Customer_ID"].ToString(); 
			}	
			if (Request.QueryString["Policy_ID"] != null && Request.QueryString["Policy_ID"].ToString()!="")
			{
				hidPOLICY_ID.Value =Request.QueryString["Policy_ID"];
			}
			if(Request.QueryString["policyVersionID"] != null && Request.QueryString["policyVersionID"].ToString()!="")
			{
				hidPOLICY_VERSION_ID.Value =Request.QueryString["policyVersionID"];
			}

			if (Request.Params["process"].ToString().ToUpper() == "NRENEW")
				hidPROCESS_ID.Value = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_NON_RENEWAL_PROCESS.ToString();
			else if (Request.Params["process"].ToString().ToUpper() == "CNRENEW")
			{
				hidPROCESS_ID.Value = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_NON_RENEWAL_PROCESS.ToString();
				btnRollback.Attributes.Add("style","display:none");
			}
			else if (Request.Params["process"].ToString().ToUpper() == "RNRENEW")
			{
					hidPROCESS_ID.Value = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ROLLBACK_NON_RENEWAL_PROCESS.ToString();
				  btnCommit.Attributes.Add("style","display:none");
				  btnComplete.Attributes.Add("style","display:none");
			}
			Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGeneralInformation = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
			DataSet dsPolicy = objGeneralInformation.GetPolicyDataSet(int.Parse(hidCUSTOMER_ID.Value==""?"0": hidCUSTOMER_ID.Value) ,int.Parse (hidPOLICY_ID.Value ==""?"0":hidPOLICY_ID.Value),int.Parse (hidPOLICY_VERSION_ID.Value==""?"0":hidPOLICY_VERSION_ID.Value));

			if(dsPolicy.Tables[0].Rows[0]["UNDERWRITER"]!=null && dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString()!="" && dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString()!="0")
				hidUNDERWRITER.Value = dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString();
			if(dsPolicy.Tables[0].Rows[0]["POLICY_LOB"]!=null && dsPolicy.Tables[0].Rows[0]["POLICY_LOB"].ToString()!="" && dsPolicy.Tables[0].Rows[0]["POLICY_LOB"].ToString()!="0")
				hidLOB_ID.Value = dsPolicy.Tables[0].Rows[0]["POLICY_LOB"].ToString();
			
			if(dsPolicy.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"]!=null && dsPolicy.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()!="")
                APP_EFFECTIVE_DATE =Convert.ToDateTime(ConvertDBDateToCulture(dsPolicy.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()));
            //Convert.ToDateTime(dsPolicy.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"]);
			if(dsPolicy.Tables[0].Rows[0]["APP_EXPIRATION_DATE"]!=null && dsPolicy.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString()!="")
                APP_EXPIRATION_DATE = Convert.ToDateTime(ConvertDBDateToCulture(dsPolicy.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString()));
			
			dsPolicy.Clear();
			dsPolicy.Dispose(); 
	
		}

		#endregion

		#region Populate Process Info
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

				if (objProcess.NEW_POLICY_VERSION_ID.ToString()!="" && objProcess.NEW_POLICY_VERSION_ID.ToString() !="0")
					hidNEW_POLICY_VERSION_ID.Value = objProcess.NEW_POLICY_VERSION_ID.ToString();
				//			else
				//				hidPOLICY_VERSION_ID.Value = objProcess.POLICY_VERSION_ID.ToString();

				hidROW_ID.Value = objProcess.ROW_ID.ToString();
				GetOldXml();

				//Saving the session and refreshing the menu
				if (objProcess.NEW_POLICY_VERSION_ID.ToString()!="" && objProcess.NEW_POLICY_VERSION_ID.ToString() !="0")
					SetPolicyInSession(objProcess.POLICY_ID, objProcess.NEW_POLICY_VERSION_ID, objProcess.CUSTOMER_ID);
				else
					SetPolicyInSession(objProcess.POLICY_ID, objProcess.POLICY_VERSION_ID, objProcess.CUSTOMER_ID);

				Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGeneralInformation = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
				DataSet dsPolicy = objGeneralInformation.GetPolicyDataSet(int.Parse(hidCUSTOMER_ID.Value==""?"0": hidCUSTOMER_ID.Value) ,int.Parse (hidPOLICY_ID.Value ==""?"0":hidPOLICY_ID.Value),int.Parse (hidPOLICY_VERSION_ID.Value==""?"0":hidPOLICY_VERSION_ID.Value));
				if(dsPolicy.Tables[0].Rows[0]["UNDERWRITER"]!=null && dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString()!="" && dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString()!="0")
					hidUNDERWRITER.Value = dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString();
				if(dsPolicy.Tables[0].Rows[0]["POLICY_LOB"]!=null && dsPolicy.Tables[0].Rows[0]["POLICY_LOB"].ToString()!="" && dsPolicy.Tables[0].Rows[0]["POLICY_LOB"].ToString()!="0")
					hidLOB_ID.Value = dsPolicy.Tables[0].Rows[0]["POLICY_LOB"].ToString();
			
				if(dsPolicy.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"]!=null && dsPolicy.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()!="")
                    APP_EFFECTIVE_DATE = Convert.ToDateTime(ConvertDBDateToCulture(dsPolicy.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()));
				if(dsPolicy.Tables[0].Rows[0]["APP_EXPIRATION_DATE"]!=null && dsPolicy.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString()!="")
                    APP_EXPIRATION_DATE = Convert.ToDateTime(ConvertDBDateToCulture(dsPolicy.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString()));
				dsPolicy.Clear();
				dsPolicy.Dispose(); 
				LoadData();
			}
			else
			{
				SetPolicyInSession(int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value), int.Parse(hidCUSTOMER_ID.Value));
				hidDisplayBody.Value = "False";
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1217");//"No Process in Progress on this Policy.";
				lblMessage.Visible=true;
			}

		}

		#endregion

		#region Save
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				/*ClsNonRenewProcess  objNonRenewProcess=new ClsNonRenewProcess(); 
				bool retVal=objNonRenewProcess.PrepareProcessObjectAutoStart(int.Parse(hidCUSTOMER_ID.Value.ToString()),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value))  ;
				return;
				//*/
				Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo = new Cms.Model.Policy.Process.ClsProcessInfo();
				ClsNonRenewProcess objNonRenewProcess = new ClsNonRenewProcess() ; 
				 
				objProcessInfo = GetFormValues();
				Cms.Model.Policy.Process.ClsProcessInfo objProcessInfotmp= objNonRenewProcess.GetRunningProcess(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID);
				objProcessInfo.POLICY_PREVIOUS_STATUS = objProcessInfotmp.POLICY_PREVIOUS_STATUS;
				objProcessInfo.POLICY_CURRENT_STATUS = objProcessInfotmp.POLICY_CURRENT_STATUS ; 
 
				objProcessInfo.PROCESS_STATUS = ClsPolicyProcess.PROCESS_STATUS_PENDING;
				objProcessInfo.PROCESS_ID = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_NON_RENEWAL_PROCESS;
				objProcessInfo.ROW_ID = int.Parse(hidROW_ID.Value);
				if (hidNEW_POLICY_VERSION_ID.Value!="" && hidNEW_POLICY_VERSION_ID.Value!="0")
					objProcessInfo.NEW_POLICY_VERSION_ID = int.Parse(hidNEW_POLICY_VERSION_ID.Value);
				//Making model object which will contains old data
				ClsProcessInfo objOldProcessInfo = new ClsProcessInfo();
				base.PopulateModelObject(objOldProcessInfo, hidOldData.Value);
				string strEffTime = ClsCommon.FetchValueFromXML("EFFECTIVETIME",hidOldData.Value);				
				if(strEffTime!="")
				{
					objOldProcessInfo.EFFECTIVE_DATETIME = Convert.ToDateTime(strEffTime);
				}
						
				//Updating the Non Renew Record
				objProcess.BeginTransaction();
				objProcess.UpdateProcessInformation(objOldProcessInfo, objProcessInfo);
				objProcess.CommitTransaction();
				lblMessage.Text		= ClsMessages.FetchGeneralMessage("31");
				lblMessage.Visible	= true;
				hidFormSaved.Value = "1";
				//Refresh the Policy Top.
				cltPolicyTop.CallPageLoad();
				GetOldXml(); 
				SetPolicyInSession(objProcessInfo.POLICY_ID,objProcessInfo.NEW_POLICY_VERSION_ID,objProcessInfo.CUSTOMER_ID);     
				//Refresh the Policy Top.
				cltPolicyTop.CallPageLoad(); 
				LoadData();
			}
			catch(Exception objExp)
			{
				objProcess.RollbackTransaction();
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
				lblMessage.Text = objExp.Message.ToString();
				lblMessage.Visible = true;
			}
		}
		#endregion
		
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
			this.btnBackToSearch.Click += new System.EventHandler(this.btnBackToSearch_Click);
			this.btnBackToCustomer.Click += new System.EventHandler(this.btnBackToCustomer_Click);
			this.btnComplete.Click += new System.EventHandler(this.btnComplete_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region StartProcess
		private void StartProcess()
		{
			try
			{
				Cms.Model.Policy.Process.ClsProcessInfo  objProcessInfo = new Cms.Model.Policy.Process.ClsProcessInfo();
				
				objProcessInfo = GetFormValues();
				ClsNonRenewProcess  objProcess = new ClsNonRenewProcess();
				objProcessInfo.CREATED_BY=objProcessInfo.COMPLETED_BY  = int.Parse(GetUserId());
				objProcessInfo.CREATED_DATETIME = DateTime.Now;

				//Hard Corded Value for Testing Purpose -- Please remove the same.
				//objProcessInfo.DIARY_LIST_ID = 535;
				

				if (objProcess.StartProcess(objProcessInfo) == true)
				{ 
					hidFormSaved.Value = "1";
					//saved successfully
					hidROW_ID.Value = objProcessInfo.ROW_ID.ToString();
					hidNEW_POLICY_VERSION_ID.Value =objProcessInfo.NEW_POLICY_VERSION_ID.ToString();   
					hidDisplayBody.Value = "True";
					lblMessage.Text = ClsMessages.FetchGeneralMessage("686");
					//Saving the session and refreshing the menu
					cltPolicyTop.PolicyVersionID =objProcessInfo.NEW_POLICY_VERSION_ID;  
					SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
					//Generating the xml of old data
					GetOldXml();
				}
				else
				{
					hidDisplayBody.Value = "False";
					lblMessage.Text = ClsMessages.FetchGeneralMessage("594");
					//Hiding the extra buttons
					HideButtons();
					//Saving the session and refreshing the menu
					cltPolicyTop.PolicyVersionID =objProcessInfo.POLICY_VERSION_ID;
					SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
				}
				
				lblMessage.Visible = true;
				//Refresh the Policy Top.
				cltPolicyTop.CallPageLoad();
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

		#endregion

		/// <summary>
		/// Hides the commit and rollback button
		/// </summary>
		private void HideButtons()
		{
			btnSave.Attributes.Add("style","display:none");
			btnComplete.Attributes.Add("style","display:none");
			btnRollback.Attributes.Add("style","display:none");
			btnCommitInProgress.Attributes.Add("style","display:none");

		}


		#region Rollback
		private void btnRollback_Click(object sender, System.EventArgs e)
		{
			try
			{
				ClsNonRenewProcess objProcess = new ClsNonRenewProcess();
				ClsProcessInfo objProcessInfo = GetFormValues();
				objProcessInfo.PROCESS_ID = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ROLLBACK_NON_RENEWAL_PROCESS;
				objProcessInfo.COMPLETED_BY = objProcessInfo.CREATED_BY =  int.Parse(GetUserId());
				objProcessInfo.COMPLETED_DATETIME = DateTime.Now;
				objProcessInfo.ROW_ID = int.Parse(hidROW_ID.Value); 
				//Rollbacking the process 
				if (objProcess.RollbackProcess(objProcessInfo) == true)
				{
					//rolled back successfully
					hidDisplayBody.Value = "True";
					lblMessage.Text = ClsMessages.FetchGeneralMessage("700");
					hidNEW_POLICY_VERSION_ID.Value ="";
					cltPolicyTop.PolicyVersionID =objProcessInfo.POLICY_VERSION_ID;  
					//Hiding the extra buttons
					HideButtons();
				}
				else
				{
					hidDisplayBody.Value = "False";
					lblMessage.Text = ClsMessages.FetchGeneralMessage("602");
					cltPolicyTop.PolicyVersionID =objProcessInfo.NEW_POLICY_VERSION_ID; 
				}
				
				//Refresh the Policy Top.
				//cltPolicyTop.CallPageLoad();
				lblMessage.Visible = true;
				//Updating the policy top,session and menus
				if (hidNEW_POLICY_VERSION_ID.Value !="" && hidNEW_POLICY_VERSION_ID.Value !="0")
					SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID,objProcessInfo.CUSTOMER_ID);
				else
					SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
				cltPolicyTop.CallPageLoad();
			}
			catch(Exception objExp)
			{
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1231")//"Following error occured while rollbacking process. \n" 
					+ objExp.Message + "\n Please try later.";
				lblMessage.Visible = true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
			}
		}
		#endregion

		#region Complete
		private void btnComplete_Click(object sender, System.EventArgs e)
		{
			try
			{
				ClsNonRenewProcess objProcess = new ClsNonRenewProcess();
				 
				ClsProcessInfo objProcessInfo = GetFormValues();
				ClsProcessInfo objProcessInfotmp= objProcess.GetRunningProcess(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID) ;
				objProcessInfo.POLICY_CURRENT_STATUS = objProcessInfotmp.POLICY_CURRENT_STATUS;
				objProcessInfo.POLICY_PREVIOUS_STATUS = objProcessInfotmp.POLICY_PREVIOUS_STATUS ;
				objProcessInfo.PROCESS_ID = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_NON_RENEWAL_PROCESS;
				objProcessInfo.COMPLETED_BY =objProcessInfo.CREATED_BY =  int.Parse(GetUserId());
				objProcessInfo.COMPLETED_DATETIME = DateTime.Now;
				objProcessInfo.ROW_ID = int.Parse(hidROW_ID.Value); 
				
				//Commiting the process 
				if (objProcess.CommitProcess(objProcessInfo) == true)
				{
					//Committed successfully
					hidFormSaved.Value = "1";
					hidDisplayBody.Value = "True";
					lblMessage.Text = ClsMessages.FetchGeneralMessage("693");
					//Hiding the extra buttons
					HideButtons();
					LoadData();
				}
				else
				{
					hidDisplayBody.Value = "False";
					lblMessage.Text = ClsMessages.FetchGeneralMessage("601");
					btnComplete.Attributes.Add("style","display:inline");
					btnCommitInProgress.Attributes.Add("style","display:none");

				}
				lblMessage.Visible = true;
				//Updating the policy top,session and menus
				//Saving the session and refreshing the menu
				if (objProcessInfo.NEW_POLICY_VERSION_ID.ToString()!="" && objProcessInfo.NEW_POLICY_VERSION_ID.ToString() !="0")
				{
					SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
					cltPolicyTop.PolicyVersionID =objProcessInfo.NEW_POLICY_VERSION_ID;  
				
				}
				else
				{
					SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
					cltPolicyTop.PolicyVersionID =objProcessInfo.POLICY_VERSION_ID;
				}
				//Refresh the Policy Top.
				cltPolicyTop.CallPageLoad();
				
				//SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
			}
			catch(Exception objExp)
			{
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1225")//"Following error occured while commiting process. \n" 
					+ objExp.Message + "\n Please try later.";
				lblMessage.Visible = true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
				btnComplete.Attributes.Add("style","display:inline");
				btnCommitInProgress.Attributes.Add("style","display:none");

			}
		}

		#endregion

		
		private void btnBackToSearch_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("/cms/client/aspx/CustomerManagerSearch.aspx");
		}

		private void btnBackToCustomer_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("/cms/client/aspx/CustomerManagerIndex.aspx?Customer_ID=" + hidCUSTOMER_ID.Value);
		}

	}
}
