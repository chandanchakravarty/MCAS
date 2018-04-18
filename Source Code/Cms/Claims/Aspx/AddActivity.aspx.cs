/******************************************************************************************
	<Author					: - > Vijay Arora
	<Start Date				: -	> 24-05-2006

	<End Date				: - >
	<Description			: - > Class for Claims Activity
	<Review Date			: - >
	<Reviewed By			: - >
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
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BLClaims;
using Cms.Claims;
using Cms.Model.Claims;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer.BlProcess;
using System.Threading;
using System.Globalization;

namespace Cms.Claims.Aspx
{
	/// <summary>
	/// 
	/// </summary>
	public class AddActivity : Cms.Claims.ClaimBase
	{
		#region Local form variables
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId;
        protected System.Web.UI.WebControls.Label capLabel;
		protected System.Web.UI.WebControls.Label capACTIVITY_DATE;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label lblACTIVITY_DATE;
		protected System.Web.UI.WebControls.Label capCREATED_BY;
		protected System.Web.UI.WebControls.Label lblCREATED_BY;
		protected System.Web.UI.WebControls.Label capACTION_ON_PAYMENT;
        protected System.Web.UI.WebControls.Label capID;
       // protected System.Web.UI.WebControls.Label lblDESCRIPTION;
		protected System.Web.UI.WebControls.DropDownList cmbACTION_ON_PAYMENT;
        protected System.Web.UI.WebControls.DropDownList cmbID;
        protected System.Web.UI.HtmlControls.HtmlTableRow TrTextID;
        protected System.Web.UI.HtmlControls.HtmlTableRow TrReasonDesc;
        protected System.Web.UI.HtmlControls.HtmlTableRow TrDesc;
        protected System.Web.UI.HtmlControls.HtmlTableRow TrReasonDescCase;
		protected System.Web.UI.WebControls.Label capREASON_DESCRIPTION;
        protected System.Web.UI.WebControls.Label capREASON_DESCRIPTION_CASE;
		protected System.Web.UI.WebControls.TextBox txtREASON_DESCRIPTION;
        protected System.Web.UI.WebControls.TextBox txtREASON_DESCRIPTION_CASE;
        protected System.Web.UI.WebControls.TextBox txtDESCRIPTION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvACTION_ON_PAYMENT;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvREASON_DESCRIPTION;
        protected System.Web.UI.WebControls.CustomValidator csvREASON_DESCRIPTION_CASE;
//		protected Cms.CmsWeb.Controls.CmsButton btnCancel;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnContinue;
		protected Cms.CmsWeb.Controls.CmsButton btnCompleteActivity;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidALLOW_MANUAL;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_SYSTEM_GENERATED;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidGL_POSTING_REQUIRED;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSummaryRow;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAUTHORIZE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAddGeneralActivity;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidACTION_ON_PAYMENT;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidACTIVITY_STATUS;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidACTIVITY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_STATUS;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTEXT_DESCRIPTION;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTEXT_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidREASON_DESCRIPTION;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidREASON_DESCRIPTION_CASE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvTEXT_ID;
		private ClsActivity objActivity = new ClsActivity();
		/*protected string customerID = "";
		protected string policyID = "";
		protected string policyVersionID = "";
		protected string lobID = "";*/
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		//protected System.Web.UI.HtmlControls.HtmlTableRow trReseve;
		//protected System.Web.UI.WebControls.Label capRESERVE_TRAN_CODE;
		//protected System.Web.UI.WebControls.DropDownList cmbRESERVE_TRAN_CODE;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvRESERVE_TRAN_CODE;	
		
		//Added for Itrack Issue 7169 on 28 April 2010
		protected Cms.CmsWeb.Controls.CmsButton btnVoidActivity;
        protected System.Web.UI.WebControls.Button btnDelete;
        
		protected Cms.CmsWeb.Controls.CmsButton btnReverseActivity;
        protected System.Web.UI.WebControls.Button btnClaimReciept;
        protected System.Web.UI.WebControls.Button btnClaimLetter;
     
        
		protected System.Web.UI.WebControls.CheckBox chkACCOUNTING_SUPPRESSED;
		protected System.Web.UI.WebControls.Label capACCOUNTING_SUPPRESSED;
		protected System.Web.UI.WebControls.Label capACCOUNTING_SUPPRESSED_CHECKED;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_BNK_RECONCILED;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCHECK_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidACCOUNTING_SUPPRESSED;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_VOIDED_REVERSED_ACTIVITY;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidFOLLOWUP_ACTIVITY_AT_VOID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidACTIVITY_AT_VOID;
        protected System.Web.UI.WebControls.DropDownList cmbCOI_TRAN_TYPE;
        protected System.Web.UI.WebControls.Label capCOI_TRAN_TYPE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCloseReserveMessage;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTextMessage;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidValidMessage;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidALERT_FLG;
        protected System.Web.UI.WebControls.Button btnCOI;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPayment_Amt;
        
		public string strActivity_Reason="";
        public string strCo_Insurance_Type;
		#endregion
	

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
            //txtREASON_DESCRIPTION.Text = "";
           
			//Added for Itrack Issue 6079 on 10 July 2009
			Ajax.Utility.RegisterTypeForAjax(typeof(AddActivity));

			base.ScreenId="309_1_0";
			
			lblMessage.Visible = false;
			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
//			btnCancel.CmsButtonClass		=	CmsButtonType.Write;
//			btnCancel.PermissionString		=	gstrSecurityXML;
//			btnCancel.Visible = false;
			
			btnContinue.CmsButtonClass		=	CmsButtonType.Read;
			btnContinue.PermissionString	=	gstrSecurityXML;


			btnActivateDeactivate.CmsButtonClass		=	CmsButtonType.Write;
			btnActivateDeactivate.PermissionString	=	gstrSecurityXML;

            //btnCOI.CmsButtonClass = CmsButtonType.Write;
            //btnCOI.PermissionString = gstrSecurityXML;

            btnCOI.Attributes.Add("OnClick", "javascript:return viewCOILetter()");


			btnCompleteActivity.CmsButtonClass		=	CmsButtonType.Write;
			//btnCompleteActivity.PermissionString	=	gstrSecurityXML;
			btnCompleteActivity.PermissionString	=	"<Security><Read>Y</Read><Write>Y</Write><Delete>Y</Delete><Execute>Y</Execute></Security>";
			//Added return in ConfirmCompletion() For Itrack Issue #6353. 
			//btnCompleteActivity.Attributes.Add("onclick","return ConfirmCompletion();DisableButton(this);return false;");//Done for Itrack Issue 6096 on 16 July 2009
			btnCompleteActivity.Attributes.Add("onclick","javascript:return ConfirmCompletion();return false;");//Removed DisableButton(this) for Itrack Issue 6353 and now called inside ConfirmCompletion()
            btnVoidActivity.Attributes.Add("onclick", "javascript:DisableButton(this);");//Added For Itrack Issue #7577 on 3 July 2010	
			btnReverseActivity.Attributes.Add("onclick","javascript:DisableButton(this);");//Added For Itrack Issue #7577 on 3 July 2010	
			btnContinue.Attributes.Add("onclick","javascript:DisableButton(this);");

			//Added for Itrack Issue 7169 on 28 April 2010
			btnVoidActivity.CmsButtonClass		=	CmsButtonType.Write;
			btnVoidActivity.PermissionString	=	"<Security><Read>Y</Read><Write>Y</Write><Delete>Y</Delete><Execute>Y</Execute></Security>";

			btnReverseActivity.CmsButtonClass	=	CmsButtonType.Write;
			btnReverseActivity.PermissionString	=	"<Security><Read>Y</Read><Write>Y</Write><Delete>Y</Delete><Execute>Y</Execute></Security>";

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.AddActivity" ,System.Reflection.Assembly.GetExecutingAssembly());		

			if (Request.Form["__EVENTTARGET"] == "AddingGeneralActivity")
			{   
				CompleteActivityStatus();						
				trBody.Attributes.Add("style","display:none");
				return;
			}

            btnReverseActivity.Visible = false;
            btnVoidActivity.Visible = false;
			if(!Page.IsPostBack)
			{
                btnClaimReciept.Visible = false; 
                btnClaimLetter.Visible = false;
                chkACCOUNTING_SUPPRESSED.Visible = false;
                capACCOUNTING_SUPPRESSED.Visible = false;
                GetQueryStringValues();
                TrTextID.Visible = false;
                TrReasonDesc.Visible = false;
                TrDesc.Visible = false;
                TrReasonDescCase.Visible = true;
                btnCOI.Visible = false;
                //cmbID.Visible = false;                
                //capID.Visible = false;
                //capREASON_DESCRIPTION.Visible = false;
                //txtDESCRIPTION.Visible = false;
                //txtREASON_DESCRIPTION.Visible = false;


                FillDropDowns();
                SetErrorMessages();
                if (hidCLAIM_ID.Value != "" && hidCLAIM_ID.Value != "0")
                {
                    DataSet ds = ClsActivity.GetCalimStatus(hidCLAIM_ID.Value);
                    if (ds.Tables[0].Rows.Count > 0)
                        hidCLAIM_STATUS.Value = ds.Tables[0].Rows[0]["CLAIM_STATUS"].ToString();
                }
                rfvTEXT_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "16");
                 

             

				if(hidACTIVITY_ID.Value!="" && hidALLOW_MANUAL.Value == "N" && hidIS_SYSTEM_GENERATED.Value=="Y")
				{
					lblDelete.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("809");	
					lblDelete.Visible = true;
					trBody.Attributes.Add("style","display:none");
					return;	
				}

				//Set the Activity Status in the Session
				if (Request["ACTIVITY_STATUS1"] != null)
					SetActivityStatus(Request["ACTIVITY_STATUS1"].ToString());
				else
					SetActivityStatus("");
				SetCaptions();

                DataSet dsTemp = objActivity.GetClaimDetails(int.Parse(hidCLAIM_ID.Value));

                
                 string Co_Insurance_Type="";
                if (dsTemp.Tables[0] != null && dsTemp.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = dsTemp.Tables[0].Rows[0];
                    hidCUSTOMER_ID.Value = dr["CUSTOMER_ID"].ToString();
                    hidPOLICY_ID.Value = dr["POLICY_ID"].ToString();
                    hidPOLICY_VERSION_ID.Value = dr["POLICY_VERSION_ID"].ToString();
                    hidLOB_ID.Value = dr["POLICY_LOB"].ToString();
                    Co_Insurance_Type = dr["CO_INSURANCE_TYPE"].ToString();
                    hidALERT_FLG.Value= dr["ALERT_FLG"].ToString();
                }
                //cmbACTIVITY_REASON.Attributes.Add("onChange","javascript: return cmbACTIVITY_REASON_Change();");

                string PrintReceiptURL = "ClaimReceipt.aspx?REPORT_TYPE=CLAIM_RECEIPT&CLAIM_ID=" + hidCLAIM_ID.Value + "&ACTIVITY_ID=" + hidACTIVITY_ID.Value + "&CUSTOMER_ID=" + hidCUSTOMER_ID.Value + "&POLICY_ID=" + hidPOLICY_ID.Value + "&POLICY_VERSION_ID=" + hidPOLICY_VERSION_ID.Value;

                btnClaimReciept.Attributes.Add("OnClick", "return PrintClaimReceipt('" + PrintReceiptURL + "');");

                string PrintClaimLetterURL = "ClaimReceipt.aspx?REPORT_TYPE=REMIND_LETTER&CLAIM_ID=" + hidCLAIM_ID.Value + "&ACTIVITY_ID=" + hidACTIVITY_ID.Value + "&CUSTOMER_ID=" + hidCUSTOMER_ID.Value + "&POLICY_ID=" + hidPOLICY_ID.Value + "&POLICY_VERSION_ID=" + hidPOLICY_VERSION_ID.Value; 

                btnClaimLetter.Attributes.Add("OnClick", "return PrintClaimReceipt('" + PrintClaimLetterURL + "');");


               

                ClaimHasClosedReservedActivity();

				if(hidACTIVITY_ID.Value!="" && hidACTIVITY_ID.Value!="0")
				{
                 

					//Check for activity reason...if it is first notification or new reserve, then display
					//messsage and does not display details page					
					//if(hidACTIVITY_REASON.Value == ((int)enumActivityReason.FIRST_NOTIFICATION).ToString() || hidACTIVITY_REASON.Value == ((int)enumActivityReason.NEW_RESERVE).ToString())
					
					//Commented by Asfa - START (19/Sept/2007)
					//New Reserve Activity Details should be displayed. iTrack # 2557

//					if(hidACTION_ON_PAYMENT.Value == ((int)enumClaimActionOnPayment.FIRST_NOTIFICATION).ToString() || hidACTION_ON_PAYMENT.Value == ((int)enumClaimActionOnPayment.NEW_RESERVE).ToString())
//					{
//						lblDelete.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("809");	
//						lblDelete.Visible = true;
//						trBody.Attributes.Add("style","display:none");
//						return;						
//					}
					//Commented by Asfa - END (19/Sept/2007)
					/*if(hidACTION_ON_PAYMENT.Value == ((int)enumActivityReason.RESERVE_UPDATE).ToString())
					{
						Page.RegisterStartupScript("GoToActivityReserve","<script>GoToActivityReserve();</script>");
						return;
					}*/					

					LoadData();
					GetOldDataXML();
					CheckActivityStatus();
				
					//Added for Itrack Issue 7169 on 29 April 2010
					string []strACTION_ON_PAYMENT = ClsCommon.FetchValueFromXML("ACTIVITY_REASON",hidOldData.Value).Split('^');
					hidACCOUNTING_SUPPRESSED.Value = ClsCommon.FetchValueFromXML("ACCOUNTING_SUPPRESSED",hidOldData.Value);
                    //Modified by shubhanshu on date 05/07/2011 Itrack 1263 
					strActivity_Reason = strACTION_ON_PAYMENT[0];
                    if (hidACTION_ON_PAYMENT.Value == "180" || hidACTION_ON_PAYMENT.Value == "181" || hidACTION_ON_PAYMENT.Value == "190" || hidACTION_ON_PAYMENT.Value == "192")
                    {
                        btnClaimReciept.Visible = true;
    
                        //cmbID.Visible = true;
                        //capID.Visible = true;
                        //capREASON_DESCRIPTION.Visible = true;
                        //txtDESCRIPTION.Visible = true;
                        //txtREASON_DESCRIPTION.Visible = true;
                        TrTextID.Visible = true;
                        TrReasonDesc.Visible = true;
                        TrDesc.Visible = true;
                        TrReasonDescCase.Visible = false;
                    }


                
                    if (hidACTIVITY_STATUS.Value == "11801")
                    {
                        btnDelete.Visible = false;

                        // PAYMENT ACTIVITY
                        if (hidACTION_ON_PAYMENT.Value == "180" || hidACTION_ON_PAYMENT.Value == "181" || hidACTION_ON_PAYMENT.Value == "190" || hidACTION_ON_PAYMENT.Value == "192")
                        {
                            btnClaimReciept.Visible = true;

                            cmbID.Enabled = false;
                           // txtREASON_DESCRIPTION.Enabled = false;  
                            txtREASON_DESCRIPTION.ReadOnly = true;
                            if (txtREASON_DESCRIPTION.ReadOnly == true)
                            {
                                txtREASON_DESCRIPTION.Attributes.Add("OnBlur", "");
                            }
                            //TO SHOW CLAIM LETTER BUTTON
                            //IF CO_INSURNCE_TYPE IS LEADER 
                            if (Co_Insurance_Type == "14548")
                                btnClaimLetter.Visible = true;
                        }
                        else
                        {
                            //cmbID.Visible = false;
                            TrTextID.Visible = false;
                            TrReasonDesc.Visible = false;
                            TrDesc.Visible = false;
                            TrReasonDescCase.Visible = true;
                            //capID.Visible = false;
                            //capREASON_DESCRIPTION.Visible = false;
                            //txtDESCRIPTION.Visible = false;
                            //txtREASON_DESCRIPTION.Visible = false;
                            btnClaimReciept.Visible = false;
                            
                        }

                        if (hidACTION_ON_PAYMENT.Value == "165" || hidACTION_ON_PAYMENT.Value == "166" || hidACTION_ON_PAYMENT.Value == "180" || hidACTION_ON_PAYMENT.Value == "181" || hidACTION_ON_PAYMENT.Value == "168" ||( (hidCLAIM_STATUS.Value == "11740" || hidCLAIM_STATUS.Value == "11745")&& hidACTION_ON_PAYMENT.Value=="167"))
                        {
                            if (Co_Insurance_Type == "14548")
                            {
                                if (hidLOB_ID.Value == "9" || hidLOB_ID.Value == "10" || hidLOB_ID.Value == "11" || hidLOB_ID.Value == "13" || hidLOB_ID.Value == "14" || hidLOB_ID.Value == "16" || hidLOB_ID.Value == "17" || hidLOB_ID.Value == "18" || hidLOB_ID.Value == "20" || hidLOB_ID.Value == "21" || hidLOB_ID.Value == "23" || hidLOB_ID.Value == "27" || hidLOB_ID.Value == "29" || hidLOB_ID.Value == "31" || hidLOB_ID.Value == "33" || hidLOB_ID.Value == "34" || hidLOB_ID.Value == "35")
                                {
                                    btnCOI.Visible = true;
                                }
                            }
                        }
                        else
                            btnCOI.Visible = false;


                        if (hidACTIVITY_AT_VOID.Value == "10963" && hidIS_VOIDED_REVERSED_ACTIVITY.Value != "10963")// CURRENT ACTIVITY IS VOIDED ACTIVITY
                            btnVoidActivity.Visible = true;
                        else
                            btnVoidActivity.Visible = false;
                    }
                    else
                        btnClaimReciept.Visible = false;

					if(strActivity_Reason == "11774" || strActivity_Reason == "11775" || strActivity_Reason == "11776")
					{
						
						if((strActivity_Reason == "11774" || strActivity_Reason == "11775") && hidACTIVITY_STATUS.Value == "11800")
						{
							chkACCOUNTING_SUPPRESSED.Visible = false;
                            capACCOUNTING_SUPPRESSED.Visible = false;
						}
						else if(strActivity_Reason == "11776" && hidACTIVITY_STATUS.Value == "11800")
						{
							chkACCOUNTING_SUPPRESSED.Visible = false;
							capACCOUNTING_SUPPRESSED.Visible = false;
						}

						if(hidACCOUNTING_SUPPRESSED.Value == "true" && hidACTIVITY_STATUS.Value == "11801")
						{
							capACCOUNTING_SUPPRESSED_CHECKED.Visible = true;
							//Done for Itrack Issue 7169 on 18 June 2010
							btnVoidActivity.Visible = false;
							btnReverseActivity.Visible = false;
						}
						else
							capACCOUNTING_SUPPRESSED_CHECKED.Visible = false;
					}
					else
					{	
						btnVoidActivity.Visible = false;
						btnReverseActivity.Visible = false;
						capACCOUNTING_SUPPRESSED_CHECKED.Visible = false;
						chkACCOUNTING_SUPPRESSED.Visible = false;
						capACCOUNTING_SUPPRESSED.Visible = false;
					}

                    string strVoidActivity = objActivity.CheckClaimVoidedActivity(int.Parse(hidCLAIM_ID.Value), int.Parse(hidACTIVITY_ID.Value));
                    if (strVoidActivity == "10963")
                    {
                        btnVoidActivity.Visible = false;
                        hidIS_VOIDED_REVERSED_ACTIVITY.Value = "10963";
                    }
                   
                   // string isVisiblebtnVoid_Reverse = objActivity.chkVisiblebtnVoid_Reserve(int.Parse(hidCLAIM_ID.Value));
                    //if(isVisiblebtnVoid_Reverse == "-1")
                    //{
                    //    btnVoidActivity.Visible = false;
                    //    btnReverseActivity.Visible = false;
                    //}


					//Check for existence of any activity still not completed/authorized					
					//Added For iTrack Issue #5926.
					bool IncompleteUnAuthorizedActivity1 = false;
					IncompleteUnAuthorizedActivity1 = objActivity.AnyIncompleteActivity(hidCLAIM_ID.Value,hidACTIVITY_ID.Value);
					if(IncompleteUnAuthorizedActivity1)
					{
						//lblDelete.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("806");// + "<br><a href='javascript:AddingGeneralActivity()'>Click here to authorize transactions -- TEMPORARY</a>"; 	
						//lblDelete.Visible = true;
						FillDropDowns();
						//trBody.Attributes.Add("style","display:none");							
						btnCompleteActivity.Visible = false;
						btnActivateDeactivate.Visible = false;
                        btnDelete.Visible = false;
						chkACCOUNTING_SUPPRESSED.Visible = false;
						capACCOUNTING_SUPPRESSED.Visible = false;
						//btnContinue.Visible = false;

						return;

					}
				}
				else
				{


                    btnDelete.Visible = false;
					//Check whether we should allow the user to click new activity using request value
					if (hidAddGeneralActivity.Value=="" || hidAddGeneralActivity.Value=="0")
					{
						//Check for existence of any activity still not completed/authorized					
						
						bool IncompleteUnAuthorizedActivity = false;
						IncompleteUnAuthorizedActivity = objActivity.AnyIncompleteActivity(hidCLAIM_ID.Value);
						if(IncompleteUnAuthorizedActivity)
						{
							lblDelete.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("806");// + "<br><a href='javascript:AddingGeneralActivity()'>Click here to authorize transactions -- TEMPORARY</a>"; 	
							lblDelete.Visible = true;
							trBody.Attributes.Add("style","display:none");							
							return;

						}
					}

                  
                    

                    lblACTIVITY_DATE.Text = DateTime.Now.ToShortDateString(); 
					lblCREATED_BY.Text = objActivity.GetUserName(int.Parse(GetUserId()));

					//Done for Itrack Issue 7169 on 21 June 2010
					string result="";
					DataSet dsActivityComplete = objActivity.CheckAllowActivityComplete(int.Parse(hidCLAIM_ID.Value),-1);
					if(dsActivityComplete!=null && dsActivityComplete.Tables[0].Rows.Count >0 && dsActivityComplete.Tables[0].Rows[0]["RESULT"].ToString().Trim()!="")
						result = dsActivityComplete.Tables[0].Rows[0]["RESULT"].ToString();
					if(result == "-1")
					{
						//Done for Itrack Issue 7562(Ravindra Mail - 1 July 2010)
						lblMessage.Text	= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");//"Reversal Activity Outstanding – need to post equal transaction and check off suppress accounting";
//						if(dsActivityComplete!=null && dsActivityComplete.Tables[1].Rows.Count >0 && dsActivityComplete.Tables[1].Rows[0]["TRANSACTION_CODE"].ToString().Trim()!="")
//							strtextActivity_Reason = dsActivityComplete.Tables[1].Rows[0]["TRANSACTION_CODE"].ToString();
//						else
//							strtextActivity_Reason = "";
//					
//						if(strtextActivity_Reason == "11774")
//							lblMessage.Text				=		"An Expense activity has been reversed on this claim, please post an equivalent Expense(same amount) with 'Suppress Accounting' checked before posting any other activity.";
//						else if(strtextActivity_Reason == "11775")
//							lblMessage.Text				=		"A Payment activity has been reversed on this claim, please post an equivalent Payment(same amount) with 'Suppress Accounting' checked before posting any other activity.";
						lblMessage.Visible = true;
					}
				}
				//LoadDropDowns();

                
				
			

//				btnCancel.Attributes.Add("onClick","ShowClaimDetail();");
			}

            //DataSet ds = ClsActivity.GetCalimStatus(hidCLAIM_ID.Value);
            //if (ds.Tables[0].Rows.Count >0)
            //    hidCLAIM_STATUS.Value = ds.Tables[0].Rows[0]["CLAIM_STATUS"].ToString();
			//Added  For Itrack Issue #6274,6372 on 23-sep-2009
			if(hidACTION_ON_PAYMENT.Value=="167" || hidACTION_ON_PAYMENT.Value=="171") // Close Reserve && Close Reinsurance Reserve
			{
				btnContinue.Visible=false;
			}
			//Added for Itrack Issue 6835 on 16 Dec 09
			if(hidACTIVITY_ID.Value == "1")
			{
				btnActivateDeactivate.Visible = false;
			}
			else
			{
				btnActivateDeactivate.Visible = true;
			}

           
		}
		#endregion

        private void ClaimHasClosedReservedActivity()
        {
            //----------------------------------------------------
            // ADDED BY SANTOSH KUMAR GAUTAM ON 23 MARCH 2011 I-TRACK(971)
            // WHEN CLAIM IS CLOSED THEN SHOW ONLY RECOVERY PARTIAL AND RECOVERY FULL
            // ACTIVITY ONLY AMOUNT IS PENDING TO RECOVER
            //----------------------------------------------------

            string ClaimStatus = "";
            double AmountToRecover = 0; 
          
            DataSet ds = ClsActivity.GetCalimStatus(hidCLAIM_ID.Value);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ClaimStatus = ds.Tables[0].Rows[0]["CLAIM_STATUS"].ToString();
                AmountToRecover = double.Parse(ds.Tables[0].Rows[0]["AMOUNT_TO_RECOVER"].ToString());   
            }

            if (ClaimStatus == CLAIM_STATUS_CLOSED && hidACTIVITY_STATUS.Value != "11801")
            {
                btnVoidActivity.Enabled = false;
                if (AmountToRecover!=0)
                {
                    // SHOW ONLY RECOVERY ACTIVITY

                    // REMOVE ALL THE ACTIVITY FROM DROPDOWN EXCEPT RECOVERY PARTIAL AND RECOVERY FULL

                    ListItem LstRecoveryPartial = cmbACTION_ON_PAYMENT.Items.FindByValue("11776^190");
                    ListItem LstRecoveryFull = cmbACTION_ON_PAYMENT.Items.FindByValue("11776^192");

                    cmbACTION_ON_PAYMENT.Items.Clear();
                    cmbACTION_ON_PAYMENT.Items.Add("");
                    cmbACTION_ON_PAYMENT.Items.Add(LstRecoveryPartial);
                    cmbACTION_ON_PAYMENT.Items.Add(LstRecoveryFull);

                   
                }
               
                 return;

            }

            // Added by santosh kumar gautam on 29 dec 2010
            // to check whether last activity for this claim is close reserve or not
            bool ClaimHasClosedReservedActivity = false;
            ClaimHasClosedReservedActivity = objActivity.ClaimHasCloseReserveActivity(int.Parse(hidCLAIM_ID.Value));
            if (ClaimHasClosedReservedActivity && hidACTIVITY_ID.Value == "")
            {
                // REMOVE ALL THE ACTIVITY FROM DROPDOWN EXCEPT REOPEN RESERVE

                ListItem LstItem = cmbACTION_ON_PAYMENT.Items.FindByValue("11773^168");

                cmbACTION_ON_PAYMENT.Items.Clear();
                cmbACTION_ON_PAYMENT.Items.Add("");
                cmbACTION_ON_PAYMENT.Items.Add(LstItem);
            }
            else
            {
                if (hidACTIVITY_ID.Value == "")
                {
                    ListItem LstItem = cmbACTION_ON_PAYMENT.Items.FindByValue("11773^168");
                    if (LstItem != null)
                        cmbACTION_ON_PAYMENT.Items.Remove(LstItem);
                }
            }
        }
		private void CompleteActivityStatus()
		{
			try
			{
				int intRetVal;								
				ClsActivity objActivity = new ClsActivity();
				//Calling the add method of business layer class
				intRetVal = objActivity.CompleteActivitiesStatus(hidCLAIM_ID.Value);
				if(intRetVal>0)
				{
					lblDelete.Text ="";
					trBody.Attributes.Add("style","display:none");
					hidFormSaved.Value			=	"1";					
				}					
				else
				{
					lblMessage.Text				=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"20");
					hidFormSaved.Value			=	"2";
				}									
				lblMessage.Visible = true;			

			}
			catch(Exception ex)
			{
				lblMessage.Text	=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
			    
			}
		}


		/*private void LoadDropDowns()
		{
			DataTable dtReserveCode = ClsActivity.ReserveTransactionCodes(hidCLAIM_ID.Value);
			if(dtReserveCode!=null && dtReserveCode.Rows.Count>0)
			{
				cmbRESERVE_TRAN_CODE.DataSource = dtReserveCode;
				cmbRESERVE_TRAN_CODE.DataTextField = "LOOKUP_VALUE_DESC";
				cmbRESERVE_TRAN_CODE.DataValueField = "LOOKUP_UNIQUE_ID";
				cmbRESERVE_TRAN_CODE.DataBind();
				cmbRESERVE_TRAN_CODE.Items.Insert(0,"");
			}
		}*/

		private void GetQueryStringValues()
		{
			if(Request["CLAIM_ID"]!=null && Request["CLAIM_ID"].ToString()!="")
				hidCLAIM_ID.Value = Request["CLAIM_ID"].ToString();
			else	
				hidCLAIM_ID.Value = "";

			if (Request["ACTIVITY_ID"]!= null && Request["ACTIVITY_ID"].ToString()!="")
			{
				hidACTIVITY_ID.Value = Request["ACTIVITY_ID"].ToString();
				cmbACTION_ON_PAYMENT.Enabled = false;
				//cmbRESERVE_TRAN_CODE.Enabled = false;
				//txtREASON_DESCRIPTION.Enabled = false;
                cmbCOI_TRAN_TYPE.Enabled = false;
			}
			else
				hidACTIVITY_ID.Value = "";

			if(Request["ACTION_ON_PAYMENT"]!=null && Request["ACTION_ON_PAYMENT"].ToString()!="")
				hidACTION_ON_PAYMENT.Value = Request["ACTION_ON_PAYMENT"].ToString();
			else	
				hidACTION_ON_PAYMENT.Value = "";

			if(Request["ADD_GENERAL"]!=null && Request["ADD_GENERAL"].ToString()!="")
				hidAddGeneralActivity.Value = Request["ADD_GENERAL"].ToString();
			else	
				hidAddGeneralActivity.Value = "";

			if(Request.QueryString["AUTHORIZE"]!=null && Request.QueryString["AUTHORIZE"].ToString()!="")			
				hidAUTHORIZE.Value = Request.QueryString["AUTHORIZE"].ToString(); 
			else
				hidAUTHORIZE.Value = "0"; 
			
			if(Request["ALLOW_MANUAL"] != null && Request["ALLOW_MANUAL"].ToString() !="")
				hidALLOW_MANUAL.Value = Request.QueryString["ALLOW_MANUAL"].ToString();

			if(Request["IS_SYSTEM_GENERATED"] != null && Request["IS_SYSTEM_GENERATED"].ToString() !="")
				hidIS_SYSTEM_GENERATED.Value = Request.QueryString["IS_SYSTEM_GENERATED"].ToString();

			if(Request["GL_POSTING_REQUIRED"] != null && Request["GL_POSTING_REQUIRED"].ToString() !="")
				hidGL_POSTING_REQUIRED.Value = Request.QueryString["GL_POSTING_REQUIRED"].ToString();

            if (Request["ACTIVITY_AT_VOID"] != null && Request["ACTIVITY_AT_VOID"].ToString() != "")
                hidACTIVITY_AT_VOID.Value = Request.QueryString["ACTIVITY_AT_VOID"].ToString();

            if (Request["FOLLOWUP_ACTIVITY_AT_VOID"] != null && Request["FOLLOWUP_ACTIVITY_AT_VOID"].ToString() != "")
                hidFOLLOWUP_ACTIVITY_AT_VOID.Value = Request.QueryString["FOLLOWUP_ACTIVITY_AT_VOID"].ToString();

            if (Request["IS_VOIDED_REVERSED_ACTIVITY"] != null && Request["IS_VOIDED_REVERSED_ACTIVITY"].ToString() != "")
                hidIS_VOIDED_REVERSED_ACTIVITY.Value = Request.QueryString["IS_VOIDED_REVERSED_ACTIVITY"].ToString();
			
            
		}

		private void CheckActivityStatus()
		{
			/*Check for status of the activity
			 * Display a message if the status of the activity is awaiting authorisation			
			*/
			if(hidACTIVITY_STATUS.Value!="" && hidACTIVITY_STATUS.Value==((int)enumClaimActivityStatus.AWAITING_AUTHORIZATION).ToString())
			{
				lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("796");
				lblMessage.Visible = true;
			}
		}

		#region GetOldDataXML
		private void GetOldDataXML()
		{
            if (hidCLAIM_ID.Value != "" && hidACTIVITY_ID.Value != "")
            {
                hidOldData.Value = objActivity.GetXmlForPageControls(hidCLAIM_ID.Value, hidACTIVITY_ID.Value);
                string COI_TRAN_TYPE = ClsCommon.FetchValueFromXML("COI_TRAN_TYPE", hidOldData.Value);
                string TEXT_ID = ClsCommon.FetchValueFromXML("TEXT_ID", hidOldData.Value);
                string TEXT_DESCRIPTION = ClsCommon.FetchValueFromXML("TEXT_DESCRIPTION", hidOldData.Value);
                string REASON_DESCRIPTION = ClsCommon.FetchValueFromXML("REASON_DESCRIPTION", hidOldData.Value);
                if (COI_TRAN_TYPE != "")
                    cmbCOI_TRAN_TYPE.SelectedValue = COI_TRAN_TYPE;
                if (TEXT_ID !="" && TEXT_ID !="0")
                {
                    ListItem findItem= cmbID.Items.FindByText(TEXT_ID);
                    if (findItem != null)
                    {
                        int ListIndex=cmbID.Items.IndexOf(findItem);
                        cmbID.SelectedIndex=ListIndex;
                        //Modified by shubhanshu on date 05/07/2011 Itrack 1263 
                        if (hidACTION_ON_PAYMENT.Value == "180" || hidACTION_ON_PAYMENT.Value == "181" || hidACTION_ON_PAYMENT.Value == "190" || hidACTION_ON_PAYMENT.Value == "192" && ListIndex > 0)
                        {
                            txtREASON_DESCRIPTION.Text = REASON_DESCRIPTION;
                            hidREASON_DESCRIPTION.Value = REASON_DESCRIPTION;
                            hidTEXT_ID.Value = TEXT_ID;
                        }
                        else
                        {
                            txtREASON_DESCRIPTION_CASE.Text = REASON_DESCRIPTION;
                            hidREASON_DESCRIPTION_CASE.Value = REASON_DESCRIPTION;
                        }
                        //txtREASON_DESCRIPTION.Text = REASON_DESCRIPTION;
                        //hidREASON_DESCRIPTION.Value = REASON_DESCRIPTION;

                        txtDESCRIPTION.Text = TEXT_DESCRIPTION;
                        hidTEXT_DESCRIPTION.Value = TEXT_DESCRIPTION;
                    }

                    
                }
               
            }
            else
                hidOldData.Value = "";
		}
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{			
			this.btnCompleteActivity.Click += new System.EventHandler(this.btnCompleteActivity_Click);
            this.btnClaimReciept.Click += new System.EventHandler(this.btnClaimReciept_Click);
           
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
			this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
			this.Load += new System.EventHandler(this.Page_Load);
			this.btnVoidActivity.Click += new System.EventHandler(this.btnVoidActivity_Click);
			//this.btnReverseActivity.Click += new System.EventHandler(this.btnReverseActivity_Click);
        
            
		}
		#endregion		

		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
        {
            Cms.CmsWeb.ClsMessages.SetCustomizedXml(GetLanguageCode());
			rfvACTION_ON_PAYMENT.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("762");
			//rfvREASON_DESCRIPTION.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("763");
            csvREASON_DESCRIPTION_CASE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("766");
            capLabel.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
		}
		#endregion

       



        #region FillDropDowns
        private void FillDropDowns()
		{
			//ListItem iListItem;
			//cmbACTION_ON_PAYMENT.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("TCODE");
			try
			{
                cmbACTION_ON_PAYMENT.Items.Clear();
				ClsActivity objActivity = new ClsActivity();
                if (hidOldData.Value == "" )//&& hidACTION_ON_PAYMENT.Value!="165")
					cmbACTION_ON_PAYMENT.DataSource = objActivity.GetActivityCodes("New",int.Parse(GetLanguageID()));
				else
					cmbACTION_ON_PAYMENT.DataSource = objActivity.GetActivityCodes("Old",int.Parse(GetLanguageID()));

				cmbACTION_ON_PAYMENT.DataTextField="TransCodeDesc";
				cmbACTION_ON_PAYMENT.DataValueField="TransCodeId";
				cmbACTION_ON_PAYMENT.DataBind();


                cmbCOI_TRAN_TYPE.DataSource = ClsCommon.GetLookup("FLPT");
                cmbCOI_TRAN_TYPE.DataTextField = "LookupDesc";
                cmbCOI_TRAN_TYPE.DataValueField = "LookupID";
                cmbCOI_TRAN_TYPE.DataBind();

                //shubhanshu

                cmbID.DataSource = objActivity.GetDescription();
                cmbID.DataTextField = "TEXT_ID";
                cmbID.DataValueField = "DESCRIPTION";
                cmbID.DataBind();
                //Commented by Abhishek Goel on 27 Apr 2012
                //cmbID.Items.Insert(0, "");

				//			ListItem li = null;			

				//Commented by Asfa - (19/Sept/2007)
				//New Reserve Activity Details should be displayed. iTrack # 2557

				//			ClsCommon.RemoveOptionFromDropdownByValue(cmbACTION_ON_PAYMENT,((int)enumActivityReason.RESERVE_UPDATE).ToString() + "^" + ((int)enumClaimActionOnPayment.NEW_RESERVE).ToString());
				//			ClsCommon.RemoveOptionFromDropdownByValue(cmbACTION_ON_PAYMENT,((int)enumActivityReason.FIRST_NOTIFICATION).ToString() + "^" + ((int)enumClaimActionOnPayment.FIRST_NOTIFICATION).ToString());
			
				cmbACTION_ON_PAYMENT.Items.Insert(0,"");
				//No need to remove the items, as they will not come at datasource(dropdown) itself
				/*Check for Reserve Update only in the case of Add New..
				 In case of Update, Activity Reason is already disabled, so no need to remove the item 
				 We also need to remove first notification and reinsurance option as this too cannot be added from this screen
				*/
				//			if(hidACTIVITY_ID.Value=="" || hidACTIVITY_ID.Value=="0")
				//			{
				//				/*Remove the check to remove Reserve Update
				//				bool ReserveUpdate = ClsActivity.AnyReserveUpdateAdded(hidCLAIM_ID.Value);
				//				if(ReserveUpdate)//Reserve update has been added once, lets remove that option from the list
				//				{
				//					iListItem = cmbACTIVITY_REASON.Items.FindByValue(((int)enumActivityReason.RESERVE_UPDATE).ToString());
				//					if(iListItem!=null)
				//						cmbACTIVITY_REASON.Items.Remove(iListItem);
				//				}
				//				*/
				//				//Remove first notification								
				//				iListItem = cmbACTION_ON_PAYMENT.Items.FindByValue(((int)enumActivityReason.FIRST_NOTIFICATION).ToString());
				//				if(iListItem!=null)
				//					cmbACTION_ON_PAYMENT.Items.Remove(iListItem);
				//				//Remove new reserve				
				//				iListItem = cmbACTION_ON_PAYMENT.Items.FindByValue(((int)enumActivityReason.NEW_RESERVE).ToString());
				//				if(iListItem!=null)
				//					cmbACTION_ON_PAYMENT.Items.Remove(iListItem);
				//				
				//			}
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}
		#endregion

		#region SetCaptions
		private void SetCaptions()
        {
            Cms.CmsWeb.ClsMessages.SetCustomizedXml(GetLanguageCode());
			capACTIVITY_DATE.Text				=		objResourceMgr.GetString("lblACTIVITY_DATE");
			capCREATED_BY.Text					=		objResourceMgr.GetString("lblCREATED_BY");
			capACTION_ON_PAYMENT.Text		    =		objResourceMgr.GetString("cmbACTION_ON_PAYMENT");
			capREASON_DESCRIPTION.Text			=		objResourceMgr.GetString("txtREASON_DESCRIPTION");
            capREASON_DESCRIPTION_CASE.Text = objResourceMgr.GetString("txtREASON_DESCRIPTION");
			capACCOUNTING_SUPPRESSED.Text		=		objResourceMgr.GetString("chkACCOUNTING_SUPPRESSED");//Added for Itrack Issue 7169
            capCOI_TRAN_TYPE.Text = objResourceMgr.GetString("cmbCOI_TRAN_TYPE");
            btnContinue.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1308");
            btnCompleteActivity.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1309");
            btnVoidActivity.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "7");
            btnDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "8");
            btnClaimReciept.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "10");
            btnClaimLetter.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "12");
            capID.Text = objResourceMgr.GetString("capID");
            //lblDESCRIPTION.Text = objResourceMgr.GetString("lblDESCRIPTION");

            hidCloseReserveMessage.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "13");
            hidTextMessage.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "14");
            hidValidMessage.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "15");
            btnCOI.Text = objResourceMgr.GetString("btnCOI");
           
		}
		#endregion

		#region GetFormValue
		private ClsActivityInfo GetFormValue()
		{
			ClsActivityInfo objActivityInfo = new ClsActivityInfo();
			
			if(hidCLAIM_ID.Value != "")
				objActivityInfo.CLAIM_ID = int.Parse(hidCLAIM_ID.Value);
				
			if (hidACTIVITY_ID.Value != "")
				objActivityInfo.ACTIVITY_ID = int.Parse(hidACTIVITY_ID.Value);
			
			if(cmbACTION_ON_PAYMENT.SelectedItem!=null && cmbACTION_ON_PAYMENT.SelectedItem.Value!="")
			{
				string [] arrAction = cmbACTION_ON_PAYMENT.SelectedItem.Value.Split('^');
				if(arrAction==null || arrAction.Length<2)
					objActivityInfo.ACTIVITY_REASON = 0;
				else
				{
					objActivityInfo.ACTIVITY_REASON = int.Parse(arrAction[0]);
					objActivityInfo.ACTION_ON_PAYMENT = int.Parse(arrAction[1]);
				}
			}

            //ADDED BY SANTOSH KR GAUTAM ON 07 DEC 2011 FOR ITRACK 1827
            if (objActivityInfo.ACTION_ON_PAYMENT == 180 || objActivityInfo.ACTION_ON_PAYMENT == 181)
            {
                objActivityInfo.COI_TRAN_TYPE = int.Parse(cmbCOI_TRAN_TYPE.SelectedValue);
            }
            else
                objActivityInfo.COI_TRAN_TYPE = 14849; // for full

			//Done for Itrack Issue 6932 on 6 July 2010
			if(hidCUSTOMER_ID.Value != "" && hidCUSTOMER_ID.Value != "0")
				objActivityInfo.CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);
			else
				objActivityInfo.CUSTOMER_ID = int.Parse(GetCustomerID());
			if(hidPOLICY_ID.Value != "" && hidPOLICY_ID.Value != "0")
				objActivityInfo.POLICY_ID = int.Parse(hidPOLICY_ID.Value);
			else
				objActivityInfo.POLICY_ID = int.Parse(GetPolicyID());
			if(hidPOLICY_VERSION_ID.Value != "" && hidPOLICY_VERSION_ID.Value != "0")
				objActivityInfo.POLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value);
			else
				objActivityInfo.POLICY_VERSION_ID = int.Parse(GetPolicyVersionID());
			if(hidLOB_ID.Value != "" && hidLOB_ID.Value != "0")
				objActivityInfo.LOB_ID = int.Parse(hidLOB_ID.Value);
			else
				objActivityInfo.LOB_ID = int.Parse(GetLOBID());
			objActivityInfo.CREATED_BY = objActivityInfo.MODIFIED_BY = int.Parse(GetUserId());

			//objActivityInfo.REASON_DESCRIPTION = txtREASON_DESCRIPTION.Text;
            if (cmbID.SelectedItem.Text != "")
            {
               if(hidTEXT_ID.Value!="")
                  objActivityInfo.TEXT_ID = int.Parse(hidTEXT_ID.Value);

                objActivityInfo.TEXT_DESCRIPTION = hidTEXT_DESCRIPTION.Value;
                txtDESCRIPTION.Text = hidTEXT_DESCRIPTION.Value;
               // objActivityInfo.REASON_DESCRIPTION = txtREASON_DESCRIPTION.Text.Trim();
                
            }
            //Modified by shubhanshu on date 05/07/2011 Itrack 1263 
            if (hidACTION_ON_PAYMENT.Value == "180" || hidACTION_ON_PAYMENT.Value == "181" || hidACTION_ON_PAYMENT.Value == "190" || hidACTION_ON_PAYMENT.Value == "192")
            {
                //objActivityInfo.REASON_DESCRIPTION = txtREASON_DESCRIPTION.Text.Trim();
                objActivityInfo.REASON_DESCRIPTION = hidREASON_DESCRIPTION.Value;
            }
            else
            {
                objActivityInfo.REASON_DESCRIPTION = txtREASON_DESCRIPTION_CASE.Text.Trim();
            }
           
			objActivityInfo.ACTIVITY_DATE = ConvertToDate( DateTime.Now.ToShortDateString()); 

			//if(cmbRESERVE_TRAN_CODE.SelectedItem!=null && cmbRESERVE_TRAN_CODE.SelectedItem.Value!="")
			//	objActivityInfo.RESERVE_TRAN_CODE = int.Parse(cmbRESERVE_TRAN_CODE.SelectedItem.Value);
			
			if(hidACTIVITY_ID.Value == "")
				strRowId="NEW";
			else
			{
				strRowId=hidACTIVITY_ID.Value; 
				objActivityInfo.ACTIVITY_ID 	=	int.Parse(hidACTIVITY_ID.Value);
			}
			
			if(hidGL_POSTING_REQUIRED.Value!="")
				objActivityInfo.GL_POSTING_REQUIRED =hidGL_POSTING_REQUIRED.Value;
			
			if(chkACCOUNTING_SUPPRESSED.Checked == true)
			{
				objActivityInfo.ACCOUNTING_SUPPRESSED = 1;
			}
			else
			{
				objActivityInfo.ACCOUNTING_SUPPRESSED = 0;
			}
			return objActivityInfo; 
		}
		#endregion

		private void btnContinue_Click(object sender, System.EventArgs e)
		{

            if (IsValid == true)
            {
                AddClaimActivity();
            }
           
		}


        private void AddClaimActivity()
        {
            try
            {
                int intRetVal;

                if ((hidACTIVITY_STATUS.Value != ((int)enumClaimActivityStatus.COMPLETE).ToString()) && (hidACTIVITY_STATUS.Value != ((int)enumClaimActivityStatus.VOID).ToString()))//Added for Itrack Issue 7169 on 15 Oct 2010
                {
                    //Retreiving the form values into model class object
                    ClsActivityInfo objActivityInfo = GetFormValue();
                    //Done for Itrack Issue 7169 on 16 June 2010
                    //Done for Itrack Issue 7892 on 14 Sept 2010
                    if (hidACTIVITY_STATUS.Value == "11800" && hidACCOUNTING_SUPPRESSED.Value == "False")
                        objActivityInfo.ACCOUNTING_SUPPRESSED = 0;
                    else if (hidACTIVITY_STATUS.Value == "11800" && (hidACCOUNTING_SUPPRESSED.Value == "True" || hidACCOUNTING_SUPPRESSED.Value == "true"))
                        objActivityInfo.ACCOUNTING_SUPPRESSED = 1;

                    if (strRowId.ToUpper().Equals("NEW")) //save case
                    {
                        objActivityInfo.CREATED_BY = int.Parse(GetUserId());
                        objActivityInfo.CREATED_DATETIME = ConvertToDate(DateTime.Now.ToString());

                        
                        // hidalertmessage has yes then prompt a alert
                        if (hidALERT_FLG.Value == "Y")
                        {
                            string AlertMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "21");
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script language=javascript>alert('" + AlertMessage + "')</script>");
                        }

                        
                        //Calling the add method of business layer class
                        intRetVal = objActivity.Add(objActivityInfo);

                        // IF CLAIM LITIGATION_FILE IS YES AND LITIGATION INFORMATION IS NOT PROVIDED THEN 
                        // USER CANNOT CREATE PAYMENT TYPE ACTIVTIY
                        if (objActivityInfo.ACTIVITY_ID == -2)
                        {
                            Cms.CmsWeb.ClsMessages.SetCustomizedXml(GetLanguageCode());
                            lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1");
                            hidFormSaved.Value = "2";
                            lblMessage.Visible = true;
                            return;
                        }
                        // RESERVE AMOUNT IS ZERO THEN NOT ALLOWED TO ADD NEW PAYMENT ACTIVITY
                        if (objActivityInfo.ACTIVITY_ID == -3)
                        {
                            Cms.CmsWeb.ClsMessages.SetCustomizedXml(GetLanguageCode());
                            lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "20");
                            hidFormSaved.Value = "2";
                            lblMessage.Visible = true;
                            return;
                        }

                        if (intRetVal > 0)
                        {
                            Cms.CmsWeb.ClsMessages.SetCustomizedXml(GetLanguageCode());
                            hidACTIVITY_ID.Value = objActivityInfo.ACTIVITY_ID.ToString();
                            lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "29");
                            hidFormSaved.Value = "1";
                            hidIS_ACTIVE.Value = "Y";
                            LoadData();
                            GetOldDataXML();
                            chkACCOUNTING_SUPPRESSED.Visible = false;
                            capACCOUNTING_SUPPRESSED.Visible = false;
                            capACCOUNTING_SUPPRESSED_CHECKED.Visible = false;
                            btnVoidActivity.Visible = false;
                            btnReverseActivity.Visible = false;
                        }
                        else if (intRetVal == -1) //Duplicate Authority Limit
                        {
                            Cms.CmsWeb.ClsMessages.SetCustomizedXml(GetLanguageCode());
                            lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "18");
                            hidFormSaved.Value = "2";
                        }

                        else
                        {
                            Cms.CmsWeb.ClsMessages.SetCustomizedXml(GetLanguageCode());
                            lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "20");
                            hidFormSaved.Value = "2";
                        }
                    } // end save case
                    else //UPDATE CASE
                    {
                        
                        //Creating the Model object for holding the Old data
                        ClsActivityInfo objOldActivityInfo = new ClsActivityInfo();
                        //Done for Itrack Issue 7169 on 16 June 2010
                        //						if(hidACTIVITY_STATUS.Value == "11800" && hidACCOUNTING_SUPPRESSED.Value == "False")
                        //							objActivityInfo.ACCOUNTING_SUPPRESSED = 0;
                        //Setting  the Old Page details(XML File containing old details) into the Model Object
                        base.PopulateModelObject(objOldActivityInfo, hidOldData.Value);
                        //objOldActivityInfo.ACCOUNTING_SUPPRESSED =
                        //Setting those values into the Model object which are not in the page					
                        objActivityInfo.MODIFIED_BY = int.Parse(GetUserId());
                        objActivityInfo.LAST_UPDATED_DATETIME = ConvertToDate(DateTime.Now.ToString());

                        //Updating the record using business layer class object
                        intRetVal = objActivity.Update(objOldActivityInfo, objActivityInfo);
                        if (intRetVal > 0)			// update successfully performed
                        {
                            lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "31");
                            hidFormSaved.Value = "1";
                            LoadData();
                            GetOldDataXML();
                            chkACCOUNTING_SUPPRESSED.Visible = false;
                            capACCOUNTING_SUPPRESSED.Visible = false;
                            capACCOUNTING_SUPPRESSED_CHECKED.Visible = false;
                            btnVoidActivity.Visible = false;
                            btnReverseActivity.Visible = false;
                           
                        }
                        else if (intRetVal == -1)	// Duplicate code exist, update failed
                        {
                            Cms.CmsWeb.ClsMessages.SetCustomizedXml(GetLanguageCode());
                            lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "18");
                            hidFormSaved.Value = "2";
                        }
                        else
                        {
                            Cms.CmsWeb.ClsMessages.SetCustomizedXml(GetLanguageCode());
                            lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "20");
                            hidFormSaved.Value = "1";
                        }
                    }
                    lblMessage.Visible = true;
                }
                ClientScript.RegisterStartupScript(this.GetType(),"Activity", "<script>GoToActivity();</script>");
            }
            catch (Exception ex)
            {
                Cms.CmsWeb.ClsMessages.SetCustomizedXml(GetLanguageCode());
                lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";

            }
        }


        private void btnClaimReciept_Click(object sender, System.EventArgs e)
        {


        }
		#region Complete Activity Button Handler
		private void btnCompleteActivity_Click(object sender, System.EventArgs e)
		{
			try
			{

                //============= Added by Shubhanshu for Itrack 1356 (tfs # 870)(To adding Claim Receipt Amount into hist text) on 12/07/2011.
                if (hidPayment_Amt.Value != "0.00" && hidPayment_Amt.Value != "0,00" && hidPayment_Amt.Value != "")
                {
                    ClsCommon ObjClsCommon = new ClsCommon();
                    System.Globalization.CultureInfo oldculture = Thread.CurrentThread.CurrentCulture;
                    NumberFormatInfo nfi = new CultureInfo(enumCulture.BR, true).NumberFormat;
                    nfi.NumberDecimalDigits = 2;


                    String strReceiptAmount = Convert.ToDouble(hidPayment_Amt.Value).ToString("N", nfi).ToString();

                    Double num = Double.Parse(strReceiptAmount, CultureInfo.GetCultureInfo("pt-BR").NumberFormat);

                    Thread.CurrentThread.CurrentCulture = oldculture;

                    String AmountText = ObjClsCommon.changeNumericToWords(num).Trim();


                    hidTEXT_DESCRIPTION.Value.Replace("$$$$", AmountText);
                    hidTEXT_DESCRIPTION.Value = hidTEXT_DESCRIPTION.Value.Replace("$$$$", AmountText);
                }

                // hidalertmessage has yes then prompt a alert
                if (hidALERT_FLG.Value == "Y")
                {
                    string AlertMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "21");
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script language=javascript>alert('" + AlertMessage + "')</script>");
                }
                
				string strAction = cmbACTION_ON_PAYMENT.SelectedValue.ToString();
				int is_dr_cr_exists =0;//Added  For Itrack Issue #6274,6372 on 23-sep-2009
				ClsActivityInfo objActivityInfo = GetFormValue();

             

                int ActivityReason = int.Parse(strAction.Split('^')[0]);
                int ActionOnPayment= int.Parse(strAction.Split('^')[1]);
				if(strAction.Split('^')[1] == "167") // CLOSE RESERVE ACTIVITY
				{
                    //Calling the add method of business layer class
                    int intRetVal = objActivity.Add(objActivityInfo);

                    // IF CLAIM LITIGATION_FILE IS YES AND LITIGATION INFORMATION IS NOT PROVIDED THEN 
                    // USER CANNOT CREATE PAYMENT TYPE ACTIVTIY
                    if (objActivityInfo.ACTIVITY_ID == -2)
                    {
                        Cms.CmsWeb.ClsMessages.SetCustomizedXml(GetLanguageCode());
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1");
                        hidFormSaved.Value = "2";
                        lblMessage.Visible = true;
                        return;
                    }

                    if (intRetVal > 0)
                    {
                        hidACTIVITY_ID.Value = objActivityInfo.ACTIVITY_ID.ToString();
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "29");
                        hidFormSaved.Value = "1";
                        hidIS_ACTIVE.Value = "Y";
                        LoadData();
                        GetOldDataXML();
                        chkACCOUNTING_SUPPRESSED.Visible = false;
                        capACCOUNTING_SUPPRESSED.Visible = false;
                        capACCOUNTING_SUPPRESSED_CHECKED.Visible = false;
                        btnVoidActivity.Visible = false;
                        btnReverseActivity.Visible = false;
                        btnCompleteActivity.Visible = false;
                        btnContinue.Visible = false;
                        btnDelete.Visible = false;
                    }
                    else if (intRetVal == -1) //Duplicate Authority Limit
                    {
                        Cms.CmsWeb.ClsMessages.SetCustomizedXml(GetLanguageCode());
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "18");
                        hidFormSaved.Value = "2";
                    }

                    else
                    {
                        Cms.CmsWeb.ClsMessages.SetCustomizedXml(GetLanguageCode());
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "20");
                        hidFormSaved.Value = "2";
                    }
				}
				
					//Added For Itrack Issue #6144.
				else if(strAction.Split('^')[1] == "171") // CLOSE REINSUARNCE  RESERVE ACTIVITY
				{
					btnContinue.Visible=false;
					hidACTIVITY_ID.Value = ClsReserveDetails.AddCloseReinsuranceReserveDetails(objActivityInfo,hidCLAIM_ID.Value,GetUserId(),hidACTIVITY_ID.Value,out is_dr_cr_exists);//Added  For Itrack Issue #6274,6372 on 23-sep-2009
					if(hidACTIVITY_ID.Value!="")
					{
						//Added  For Itrack Issue #6274,6372 on 23-sep-2009
						if(is_dr_cr_exists == 0)
						{
							hidFormSaved.Value="1";
                            hidSummaryRow.Value = objActivity.GetActivitySummary(hidCLAIM_ID.Value, int.Parse(GetPolicyCurrency())).Replace("@SUMMARY@", Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "11"));
							ClientScript.RegisterStartupScript(this.GetType(),"SetSummaryRow","<script>SetParentActivitySummaryRow();</script>");
							btnContinue.Visible=false;
							btnCompleteActivity.Visible = false;
							btnActivateDeactivate.Visible = false;
							cmbACTION_ON_PAYMENT.Enabled=false;
							lblMessage.Text				=		Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"811");	
						}
						//Added  For Itrack Issue #6274,6372 on 23-sep-2009
						else
						{
						    lblMessage.Text				=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("931");
							btnContinue.Visible	= true;
							btnCompleteActivity.Visible = true;
							btnActivateDeactivate.Visible = true;
                            btnDelete.Visible = true;
						}
						GetOldDataXML();
					}
					else
					{
						lblMessage.Text				=		"Activity could not be completed";
					}	
					lblMessage.Visible = true;
					chkACCOUNTING_SUPPRESSED.Visible = false;
					capACCOUNTING_SUPPRESSED.Visible = false;
					capACCOUNTING_SUPPRESSED_CHECKED.Visible = false;
					btnVoidActivity.Visible = false;
					btnReverseActivity.Visible = false;
				} 
                 
				else
				{
					int intRetVal;
					//Fetch the wolverine user from web.config to whom diary entry notification needs to be sent
					/*string strWolverineUser1, strWolverineUser2;
					strWolverineUser1 = ClsCommon.GetKeyValue("WolverineUser1");
					strWolverineUser2 = ClsCommon.GetKeyValue("WolverineUser2");*/
					//Calling the add method of business layer class
					ClsActivity objActivity = new ClsActivity();
					//ClsActivityInfo objActivityInfo = GetFormValue();
					//				if(hidAUTHORIZE.Value=="1")//if we want the current logged in user to authorize the activity, send the User_Id
					//					intRetVal = objActivity.CompleteActivity(hidCLAIM_ID.Value, hidACTIVITY_ID.Value,GetUserId(),GetUserId(),strWolverineUser1,strWolverineUser2);
					//				else
					//					intRetVal = objActivity.CompleteActivity(hidCLAIM_ID.Value, hidACTIVITY_ID.Value,"0",GetUserId(),strWolverineUser1,strWolverineUser2);	

					//Done for Itrack Issue 7169 on 16 June 2010
					//When a claim activity is reversed system will not allow adding any other activity until a Payment Activity with “Suppress Accounting” checked is posted. This will reduce chances of error.
					//Done for Itrack Issue 7169 on 21 June 2010
					string result = "0",strActivity_reason="",strcommit="";
                    int RetError = 0;
                    // Added by SANTOSH KUMAR GAUTAM on 3 DEC 2010
                    // OLD VALUE DataSet dsActivityComplete = objActivity.CheckAllowActivityComplete(int.Parse(hidCLAIM_ID.Value), objActivityInfo.ACTIVITY_ID);
                    DataSet dsActivityComplete = objActivity.CheckAllowActivityComplete(int.Parse(hidCLAIM_ID.Value), objActivityInfo.ACTIVITY_ID);
					if(dsActivityComplete!=null && dsActivityComplete.Tables[0].Rows.Count >0 && dsActivityComplete.Tables[0].Rows[0]["RESULT"].ToString().Trim()!="")
					{
						result = dsActivityComplete.Tables[0].Rows[0]["RESULT"].ToString();
						strActivity_reason = dsActivityComplete.Tables[1].Rows[0]["TRANSACTION_CODE"].ToString();
						strcommit = dsActivityComplete.Tables[2].Rows[0]["RESULT"].ToString();

					}
					if(result == "1" && strcommit == "1")
					{

                      
                        

						if(chkACCOUNTING_SUPPRESSED.Checked == true)
						{
                            if (hidAUTHORIZE.Value == "1")//if we want the current logged in user to authorize the activity, send the User_Id
                            {	//intRetVal = objActivity.CompleteActivity(objActivityInfo,GetUserId());
                                intRetVal = objActivity.CompleteActivity(objActivityInfo, ActivityReason, ActionOnPayment,int.Parse(GetUserId()), out RetError,int.Parse(GetLanguageID()));
                                if (RetError == -9 || RetError == -10 || RetError == -11)
                                    intRetVal=RetError;
                                else if (RetError > 0)
                                    intRetVal = 1;

                            }
                            else
                            {
                                //intRetVal = objActivity.CompleteActivity(objActivityInfo, "0");
                                intRetVal = objActivity.CompleteActivity(objActivityInfo, ActivityReason, ActionOnPayment, int.Parse(GetUserId()), out RetError, int.Parse(GetLanguageID()));
                                if (RetError == -9 || RetError == -10 || RetError == -11)
                                    intRetVal = RetError;
                                else if (RetError > 0)
                                    intRetVal = 1;

                            }
						}
						else if(strActivity_reason == "11773")
						{
                            //if(hidAUTHORIZE.Value=="1")//if we want the current logged in user to authorize the activity, send the User_Id
                            //    intRetVal = objActivity.CompleteActivity(objActivityInfo,GetUserId());
                            //else
                            //    intRetVal = objActivity.CompleteActivity(objActivityInfo,"0");

                            if (hidAUTHORIZE.Value == "1")//if we want the current logged in user to authorize the activity, send the User_Id
                            {
                                intRetVal = objActivity.CompleteActivity(objActivityInfo, ActivityReason, ActionOnPayment, int.Parse(GetUserId()), out RetError, int.Parse(GetLanguageID()));
                                if (RetError == -9 || RetError == -10 || RetError == -11)
                                    intRetVal = RetError;
                                else if (RetError > 0)
                                    intRetVal = 1;

                            }
                            else
                            {
                                intRetVal = objActivity.CompleteActivity(objActivityInfo, ActivityReason, ActionOnPayment, int.Parse(GetUserId()), out RetError, int.Parse(GetLanguageID()));
                                if (RetError == -9 || RetError == -10 || RetError == -11)
                                    intRetVal = RetError;
                                else if (RetError > 0)
                                    intRetVal = 1;

                            }


						}
						else
						{
							intRetVal = -2;
						}
					}
					else if(result == "2")
					{
                        //if(hidAUTHORIZE.Value=="1")//if we want the current logged in user to authorize the activity, send the User_Id
                        //    intRetVal = objActivity.CompleteActivity(objActivityInfo,GetUserId());
                        //else
                        //    intRetVal = objActivity.CompleteActivity(objActivityInfo,"0");

                        if (hidAUTHORIZE.Value == "1")//if we want the current logged in user to authorize the activity, send the User_Id
                        {
                            intRetVal = objActivity.CompleteActivity(objActivityInfo, ActivityReason, ActionOnPayment, int.Parse(GetUserId()), out RetError, int.Parse(GetLanguageID()));
                            if (RetError == -9 || RetError == -10 || RetError == -11)
                                intRetVal = RetError;
                            else if (RetError > 0)
                                intRetVal = 1;

                        }
                        else
                        {
                            intRetVal = objActivity.CompleteActivity(objActivityInfo, ActivityReason, ActionOnPayment, int.Parse(GetUserId()), out RetError, int.Parse(GetLanguageID()));
                            if (RetError == -9 || RetError == -10 || RetError == -11)
                                intRetVal = RetError;
                            else if (RetError > 0)
                                intRetVal = 1;

                        }

					}
					else if(result == "-5")//Done for Ravindra Mail - 6 Oct 2010
					{
						intRetVal = -5;
					}
					else if(strcommit == "-3")
					{
						intRetVal = -3;
					}
					else if(strcommit == "-4")
					{
						intRetVal = -4;
					}
					else
					{

						intRetVal = -2;
					}
					switch(intRetVal)
					{
						case 1: //Activity has been completed successfully
						{
							if(hidAUTHORIZE.Value!="1")
							{
                                hidSummaryRow.Value = hidSummaryRow.Value = objActivity.GetActivitySummary(hidCLAIM_ID.Value, int.Parse(GetPolicyCurrency())).Replace("@SUMMARY@", Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "11"));
								ClientScript.RegisterStartupScript(this.GetType(),"SetSummaryRow","<script>SetParentActivitySummaryRow();</script>");
							}
							btnActivateDeactivate.Visible = false;
							btnCompleteActivity.Visible = false;
                            btnDelete.Visible = false;
							chkACCOUNTING_SUPPRESSED.Visible = false;
							capACCOUNTING_SUPPRESSED.Visible = false;
							string []strACTION_ON_PAYMENT = ClsCommon.FetchValueFromXML("ACTIVITY_REASON",hidOldData.Value).Split('^');
							strActivity_Reason = strACTION_ON_PAYMENT[0];
							if((strActivity_Reason == "11774" || strActivity_Reason == "11775" || strActivity_Reason == "11776") && objActivityInfo.ACCOUNTING_SUPPRESSED == 0)
							{
								//Added for Itrack Issue 7798 on 17 Aug 2010
                                if (hidACTIVITY_AT_VOID.Value == "10963" && hidIS_VOIDED_REVERSED_ACTIVITY.Value != "10963")// CURRENT ACTIVITY IS VOIDED ACTIVITY
                                    btnVoidActivity.Visible = true;
                                else
                                    btnVoidActivity.Visible = false;
                                  

                                //if(strActivity_Reason == "11774" || strActivity_Reason == "11775")
                                //    btnReverseActivity.Visible = false;
                                //else
                                //   btnReverseActivity.Visible = false;
							}
							else
							{
								btnVoidActivity.Visible = false;
								btnReverseActivity.Visible = false;
							}
							if(objActivityInfo.ACCOUNTING_SUPPRESSED == 1)
							{
								capACCOUNTING_SUPPRESSED_CHECKED.Visible = true;
							}
							else
							{
								capACCOUNTING_SUPPRESSED_CHECKED.Visible = false;
							}
							lblMessage.Text				=		Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"811");					
							//Set the Activity Status in the Session
							SetActivityStatus("Complete");
							//Added by Asfa (15-Jan-2008) - iTrack issue #3393
							string strClmLimit="";
							
							DataSet dsClmDiaryLimit = ClsActivity.GetCalimDiaryLimits(objActivityInfo.ACTIVITY_REASON);
							if(dsClmDiaryLimit!=null && dsClmDiaryLimit.Tables.Count>0 && dsClmDiaryLimit.Tables[0].Rows.Count>0)
							{
								if(objActivityInfo.ACTIVITY_REASON==(int)enumTransactionLookup.CLAIM_PAYMENT || objActivityInfo.ACTIVITY_REASON==(int)enumTransactionLookup.EXPENSE_PAYMENT)
									strClmLimit = dsClmDiaryLimit.Tables[0].Rows[0]["CLAIM_PAYMENT_LIMIT"].ToString();
								else if(objActivityInfo.ACTIVITY_REASON==(int)enumTransactionLookup.RESERVE_UPDATE)
									strClmLimit = dsClmDiaryLimit.Tables[0].Rows[0]["CLAIM_RESERVE_LIMIT"].ToString();
							}
					
							DataSet dsTemp = objActivity.GetValuesForPageControls(hidCLAIM_ID.Value,hidACTIVITY_ID.Value);
							if (dsTemp != null && dsTemp.Tables[0].Rows.Count > 0)
							{
								DataRow dr = dsTemp.Tables[0].Rows[0];					
								if(objActivityInfo.ACTIVITY_REASON==(int)enumTransactionLookup.CLAIM_PAYMENT)					
								{
									if(dr["PAYMENT_AMOUNT"] != null && dr["PAYMENT_AMOUNT"].ToString()!="")
										objActivityInfo.PAYMENT_AMOUNT= Convert.ToDouble(dr["PAYMENT_AMOUNT"]);
								}
								if(objActivityInfo.ACTIVITY_REASON==(int)enumTransactionLookup.EXPENSE_PAYMENT)					
								{
									if(dr["EXPENSES"] != null && dr["EXPENSES"].ToString()!="")
										objActivityInfo.EXPENSES = Convert.ToDouble(dr["EXPENSES"]);
								}
							}

                            dsTemp = objActivity.GetClaimDetails(objActivityInfo.CLAIM_ID);
                            strCo_Insurance_Type = dsTemp.Tables[0].Rows[0]["CO_INSURANCE_TYPE"].ToString();
							if((objActivityInfo.ACTIVITY_REASON==(int)enumTransactionLookup.CLAIM_PAYMENT &&  objActivityInfo.PAYMENT_AMOUNT > Convert.ToDouble(strClmLimit)) || (objActivityInfo.ACTIVITY_REASON==(int)enumTransactionLookup.EXPENSE_PAYMENT)&& objActivityInfo.EXPENSES > Convert.ToDouble(strClmLimit))
							{
								objActivity.SendNotifyDiaryEntry(objActivityInfo);
							}
							else if(objActivityInfo.ACTIVITY_REASON==(int)enumTransactionLookup.RESERVE_UPDATE)
							{
								
								if (dsTemp.Tables[0] != null && dsTemp.Tables[0].Rows.Count > 0)
								{
									if(Convert.ToDouble(dsTemp.Tables[0].Rows[0]["OUTSTANDING_RESERVE"].ToString()) > Convert.ToDouble(strClmLimit))
									{
										objActivity.SendNotifyDiaryEntry(objActivityInfo);	
									}
								}
							}

                            
                            //if (hidACTION_ON_PAYMENT.Value == "180" || hidACTION_ON_PAYMENT.Value == "181")
                            if (hidACTION_ON_PAYMENT.Value == "180" || hidACTION_ON_PAYMENT.Value == "181" || hidACTION_ON_PAYMENT.Value == "190" || hidACTION_ON_PAYMENT.Value == "192")
                            {
                                //--------------------------------------------------------------------
                                // MODIFIED BY SANTOSH KUMAR GAUTAM ON 22 FEB 20111 
                                // IF PAYMENT ACTIVITY IS COMPLETED THEN GENERATE REPORT FOR CLAIM RECEIPT
                                //--------------------------------------------------------------------
                                   ClsProductPdfXml ObjPdfXML = new ClsProductPdfXml();
                                int PolicyID= int.Parse(hidPOLICY_ID.Value);
                                int PolicyVersionID = int.Parse(hidPOLICY_VERSION_ID.Value);
                                int CustomerID = int.Parse(hidCUSTOMER_ID.Value);
                                int UserID = int.Parse(GetUserId());
                                int ClaimID =int.Parse(hidCLAIM_ID.Value);
                                int ActivityID =int.Parse(hidACTIVITY_ID.Value);
                                ObjPdfXML.generateProductClaimReceipt(ClaimID, ActivityID, CustomerID, PolicyID, PolicyVersionID, UserID,"N");
                                   
                                
                                if (dsTemp.Tables.Count > 0 && dsTemp.Tables[0].Rows.Count > 0)
                                {
                                    if (dsTemp.Tables[0].Rows[0]["CO_INSURANCE_TYPE"].ToString() == "14548")
                                    {
                                        //--------------------------------------------------------------------
                                        // IF PAYMENT ACTIVITY IS COMPLETED THEN GENERATE REPORT FOR CLAIM REMIND LETTER                                        
                                        // INSERT RECORD IN PRINT JOBS
                                        //--------------------------------------------------------------------
                                        ObjPdfXML.generateProductClaimRemindLetter(ClaimID, ActivityID, CustomerID, PolicyID, PolicyVersionID, UserID);
                                       
                                      
                                        btnClaimLetter.Visible = true;
                                    }
                                    else
                                        btnClaimLetter.Visible = false;

                                }

                                btnClaimReciept.Visible = true;
                            }
                            else
                                btnClaimReciept.Visible = false;
                           

							break;
						}
						case 2: //Payment for the current claim has not been paid yet
						{
							//						btnActivateDeactivate.Visible =  false;
							lblMessage.Text				=		Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"810");					
							break;
						}
						case 3: //Payment breakdown is less than total payment
						{
							lblMessage.Text				=		Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"823");					
							break;
						}
						case 4: //Activity put up at authorization queue of next adjuster
						{
							//btnActivateDeactivate.Visible = false;
							btnCompleteActivity.Visible = false;
							lblMessage.Text				= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"812");		
							
							if( objActivityInfo.ACTIVITY_REASON==(int)enumTransactionLookup.CLAIM_PAYMENT || objActivityInfo.ACTIVITY_REASON==(int)enumTransactionLookup.EXPENSE_PAYMENT)
							{
								objActivity.SendNotifyDiaryAuthorityLimitExceeded(objActivityInfo);
							}
							else if(objActivityInfo.ACTIVITY_REASON==(int)enumTransactionLookup.RESERVE_UPDATE)
							{
								objActivity.SendNotifyDiaryAuthorityLimitExceeded(objActivityInfo);
							}
							break;
						}
						case 5: //No adjuster has been found..
						{
							//						btnActivateDeactivate.Visible = false;
							lblMessage.Text				=		Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"837");	
				
							break;
						}
						case 6: //No payee has been added for the current activity
						{
							//						btnActivateDeactivate.Visible = false;
							lblMessage.Text				=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("932");					
							break;
						}
						case 7: //Debit/Credit a/c for current activity not being set
						{
							//						btnActivateDeactivate.Visible = false;
							lblMessage.Text				=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("931");					
							break;
						}
						case -1: //Error
						{
							lblMessage.Text				=		Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"20");					
							break;
						}
						//Added FOR Itrack Issue #5751. 
						case 9:
						{
							lblMessage.Text               =       Cms.CmsWeb.ClsMessages.FetchGeneralMessage("819");	
							break;
						}	
						case -2: //Done for Itrack Issue 7169 on 16 June 2010
						{
							//Done for Itrack Issue 7562(Ravindra Mail - 1 July 2010)
							lblMessage.Text	= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");//Reversal Activity Outstanding – need to post equal transaction and check off suppress accounting.
//							if(dsActivityComplete!=null && dsActivityComplete.Tables[1].Rows.Count >0 && dsActivityComplete.Tables[1].Rows[0]["TRANSACTION_CODE"].ToString().Trim()!="")
//								strActivity_Reason = dsActivityComplete.Tables[1].Rows[0]["TRANSACTION_CODE"].ToString();
//							else
//								strActivity_Reason = "";
//
//							if(strActivity_Reason == "11774")
//								lblMessage.Text				=		"An Expense activity has been reversed on this claim, please post an equivalent Expense(same amount) with 'Suppress Accounting' checked before posting any other activity.";
//							else if(strActivity_Reason == "11775" || strActivity_Reason == "11776")
//								lblMessage.Text				=		"A Payment activity has been reversed on this claim, please post an equivalent Payment(same amount) with 'Suppress Accounting' checked before posting any other activity.";					

							break;
						}
						case -3: //Error
						{
							lblMessage.Text				=		Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");//A Paid Loss, Final activity has been reversed on this claim, please post an equivalent Paid Loss, Final activity(same amount) with 'Suppress Accounting' checked before posting any other activity.					
							break;
						}
						case -4: //Error
						{
							lblMessage.Text				=		Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");//Please commit a payment activity other than Paid Loss, Final activity.			
							break;
						}
						case -5: //Error//Done for Ravindra Mail - 6 Oct 2010
						{
							lblMessage.Text				=		Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");//Total Outstanding does not match with reserve amount of this activity. Please review and save reserve page.
							break;
						}
                        case -9: //Added by Santosh Kumar Gautam 13 Dec 2010
                        {
                            if (ActivityReason == (int)enumActivityReason.CLAIM_PAYMENT)
                                  lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "2");
                             if (ActivityReason == (int)enumActivityReason.RECOVERY)
                                 lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "3");
                            break;
                        }
                        //payment amount is greater then the limit amount
                        case -10: //Added by Santosh Kumar Gautam 13 April 2011
                        {  
                            lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "17");
                            break;
                        }
                        //reserve amount is greater then the limit amount
                        case -11: //Added by Santosh Kumar Gautam 13 April 2011
                        {
                           lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "18");
                            break;
                        }
						default:
						{
							lblMessage.Text				=		Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"20");					
							break;
						}
					}
					hidFormSaved.Value			=		"1";				
					lblMessage.Visible = true;				
				}

                // Added by santosh kumar gautam on 21 dec 2010
               // btnVoidActivity.Visible = false;
                btnReverseActivity.Visible = false;
                btnReverseActivity.Visible = false;
                if (hidACTION_ON_PAYMENT.Value == "165" || hidACTION_ON_PAYMENT.Value == "166" || hidACTION_ON_PAYMENT.Value == "180" || hidACTION_ON_PAYMENT.Value == "181" || hidACTION_ON_PAYMENT.Value == "168" || ((hidCLAIM_STATUS.Value == "11740" || hidCLAIM_STATUS.Value == "11745") && hidACTION_ON_PAYMENT.Value == "167"))
                {
                    if (strCo_Insurance_Type == "14548")
                    {
                        if (hidLOB_ID.Value == "9" || hidLOB_ID.Value == "10" || hidLOB_ID.Value == "11" || hidLOB_ID.Value == "13" || hidLOB_ID.Value == "14" || hidLOB_ID.Value == "16" || hidLOB_ID.Value == "17" || hidLOB_ID.Value == "18" || hidLOB_ID.Value == "20" || hidLOB_ID.Value == "21" || hidLOB_ID.Value == "23" || hidLOB_ID.Value == "27" || hidLOB_ID.Value == "29" || hidLOB_ID.Value == "31" || hidLOB_ID.Value == "33" || hidLOB_ID.Value == "34" || hidLOB_ID.Value == "35")
                        {
                            btnCOI.Visible = true;
                        }
                    }
                    else
                        btnCOI.Visible = false;
                }
                else
                    btnCOI.Visible = false;
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
			    
			}
		}

		#endregion

		#region Deactivate Activity Button Handler
		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	

				//Calling the add method of business layer class
				ClsActivity objActivity = new ClsActivity();
				ClsActivityInfo objActivityInfo = GetFormValue();
				if(hidIS_ACTIVE.Value.ToUpper()=="Y")
				{
					intRetVal = objActivity.ActivateDeactivateActivity(objActivityInfo,hidCLAIM_ID.Value, hidACTIVITY_ID.Value,((int)enumClaimActivityStatus.DEACTIVATE).ToString(),"N");//Done for Itrack Issue 6932 on 1 Feb 2010
                    if (intRetVal > 0)
                    {
                        hidIS_ACTIVE.Value = "N";
                        lblMessage.Text = "";
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "41");
                        btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchActivateDeactivateButtonsText("N");
                        btnCompleteActivity.Visible = false;
                    }
				}
				else
				{
					intRetVal = objActivity.ActivateDeactivateActivity(objActivityInfo,hidCLAIM_ID.Value, hidACTIVITY_ID.Value,((int)enumClaimActivityStatus.INCOMPLETE).ToString(),"Y");//Done for Itrack Issue 6932 on 1 Feb 2010
                    if (intRetVal > 0)
                    {
                        hidIS_ACTIVE.Value = "Y";
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "40");
                        btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchActivateDeactivateButtonsText("Y");
                        btnCompleteActivity.Visible = true;
                    }
				}

				if(intRetVal >0 ) 
				{
					if(hidAUTHORIZE.Value!="1")
					{


                        hidSummaryRow.Value = objActivity.GetActivitySummary(hidCLAIM_ID.Value, int.Parse(GetPolicyCurrency())).Replace("@SUMMARY@", Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "11"));
						ClientScript.RegisterStartupScript(this.GetType(),"SetSummaryRow","<script>SetParentActivitySummaryRow();</script>");
					}
					//btnCompleteActivity.Visible = false;
					//lblMessage.Text				=		Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"41");
					hidFormSaved.Value			=		"5";
				}
                if (intRetVal ==-1)
                {
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "19");
                    hidFormSaved.Value = "2";
                }
                if (intRetVal == 0)
                {
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "20");
                    hidFormSaved.Value = "2";
                }
				lblMessage.Visible = true;				

			}
			catch(Exception ex)
			{
				lblMessage.Text	=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
			    
			}
		}



        public void btnVoidActivity_Click(object sender, System.EventArgs e)
        {
            int intRetVal;
            double Factor = 1.0;
            try
            {
                ClsActivity objActivity = new ClsActivity();
                objActivity.IS_VOID_ACTIVITY = "Y";
                ClsActivityInfo objActivityInfo = GetFormValue();


                objActivityInfo.ACTION_ON_PAYMENT = (int)enumClaimActionOnPayment.CHANGE_RESERVE;
                objActivityInfo.ACTIVITY_REASON = (int)enumActivityReason.RESERVE_UPDATE;

                objActivityInfo.MODIFIED_BY = int.Parse(GetUserId());
                objActivityInfo.LAST_UPDATED_DATETIME = ConvertToDate(DateTime.Now.ToShortDateString());
               
                intRetVal = objActivity.VoidClaimActivity(objActivityInfo, int.Parse(hidACTIVITY_ID.Value), Factor);
                if (intRetVal > 0)
                {
                    btnVoidActivity.Visible = false;
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
                    lblMessage.Visible = true;
                    btnClaimReciept.Visible = false;
                  
                    LoadData();
                    GetOldDataXML();
                    hidFormSaved.Value = "1";
                    ClientScript.RegisterStartupScript(this.GetType(),"SetSummaryRow", "<script>SetParentActivitySummaryRow();</script>");

                }
                else
                {
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "4");
                    lblMessage.Visible = true;
                }
                
            }
            catch (Exception ex)
            {
                lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);

            }

        }

        //public void btnVoidActivity_Click(object sender,System.EventArgs e)
        //{
        //    int intRetVal;

        //    try
        //    {
        //        ClsActivity objActivity = new ClsActivity();
        //        ClsActivityInfo objActivityInfo = GetFormValue();
        //        string strcommit="";
        //        DataSet dsActivityComplete = objActivity.CheckAllowActivityComplete(int.Parse(hidCLAIM_ID.Value),-1);
				
        //        strcommit = dsActivityComplete.Tables[0].Rows[0]["RESULT"].ToString();

        //        if(strcommit == "1")
        //        {

        //            int isPaidLossFinalVoided = 0;
        //            intRetVal = objActivity.AddAutoActivity(objActivityInfo,int.Parse(hidCLAIM_ID.Value),int.Parse(hidACTIVITY_ID.Value),int.Parse(hidACTION_ON_PAYMENT.Value),int.Parse(GetUserId()),0,out isPaidLossFinalVoided);
        //            if(intRetVal >0) 
        //            {
        //                btnVoidActivity.Visible = false;
        //                if(hidIS_BNK_RECONCILED.Value == "Y")
        //                    btnReverseActivity.Visible = true;
        //                else
        //                    btnReverseActivity.Visible = false;
        //                hidSummaryRow.Value =  objActivity.GetActivitySummary(hidCLAIM_ID.Value);
        //                hidFormSaved.Value="1";
        //                if(isPaidLossFinalVoided == 1)
        //                    objActivity.SendPaidLossFinalDiaryEntry(objActivityInfo);
        //                Page.RegisterStartupScript("SetSummaryRow","<script>SetParentActivitySummaryRow();</script>");
        //                GetOldDataXML();
        //            }
        //        }
        //        else if(strcommit == "-3")
        //        {
        //            lblMessage.Text				=		Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");//"A Paid Loss, Final activity has been reversed on this claim, please post an equivalent Paid Loss, Final activity(same amount) with 'Suppress Accounting' checked before posting any other activity.";					
        //            lblMessage.Visible	=	true;
        //        }
        //        else 
        //        {
        //            lblMessage.Text	= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");//"Reversal Activity Outstanding – need to post equal transaction and check off suppress accounting";
        //            lblMessage.Visible	=	true;
        //        }

        //    }
        //    catch(Exception ex)
        //    {
        //        lblMessage.Text	=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
        //        lblMessage.Visible	=	true;
        //        Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			    
        //    }
			
        //}

      
		public void btnReverseActivity_Click(object sender,System.EventArgs e)
		{
			int intRetVal;
			
			try
			{
				ClsActivity objActivity = new ClsActivity();
				ClsActivityInfo objActivityInfo = GetFormValue();
				objActivityInfo.LOB_ID = int.Parse(GetLOBID());
				string strcommit="";
				DataSet dsActivityComplete = objActivity.CheckAllowActivityComplete(int.Parse(hidCLAIM_ID.Value),-1);
				
				strcommit = dsActivityComplete.Tables[0].Rows[0]["RESULT"].ToString();

				
				if(strcommit == "1")
				{

					int isPaidLossFinalVoided = 0;
					intRetVal = objActivity.AddAutoActivity(objActivityInfo,int.Parse(hidCLAIM_ID.Value),int.Parse(hidACTIVITY_ID.Value),int.Parse(hidACTION_ON_PAYMENT.Value),int.Parse(GetUserId()),1,out isPaidLossFinalVoided);
				
					if(intRetVal >0) 
					{
						btnReverseActivity.Visible = false;
						btnVoidActivity.Visible = false;
						capACCOUNTING_SUPPRESSED_CHECKED.Visible = true;
                        hidSummaryRow.Value = hidSummaryRow.Value = objActivity.GetActivitySummary(hidCLAIM_ID.Value, int.Parse(GetPolicyCurrency())).Replace("@SUMMARY@", Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "11"));
						ClientScript.RegisterStartupScript(this.GetType(),"SetSummaryRow","<script>SetParentActivitySummaryRow();</script>");
						hidFormSaved.Value="1";
						objActivity.SendReversePaymentDiaryEntry(objActivityInfo);
						if(isPaidLossFinalVoided == 1)
							objActivity.SendPaidLossFinalDiaryEntry(objActivityInfo);

					}
					GetOldDataXML();
				}
				else 
				{
					lblMessage.Text	= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");//"Reversal Activity Outstanding – need to post equal transaction and check off suppress accounting";
					lblMessage.Visible	=	true;
				}
				
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			    
			}
		}
		#endregion
	
		#region LoadData
		private void LoadData()
		{
			if (hidCLAIM_ID.Value != "" && hidACTIVITY_ID.Value != "")
			{
				DataSet dsTemp = objActivity.GetValuesForPageControls(hidCLAIM_ID.Value,hidACTIVITY_ID.Value);
				if (dsTemp != null && dsTemp.Tables[0].Rows.Count > 0)
				{
					DataRow dr = dsTemp.Tables[0].Rows[0];
					
					lblACTIVITY_DATE.Text = ConvertDBDateToCulture( dr["ACTIVITY_DATE"].ToString());
					lblCREATED_BY.Text = dr["USERNAME"].ToString();
					cmbACTION_ON_PAYMENT.SelectedValue = dr["ACTIVITY_REASON"].ToString();
                    if (hidACTION_ON_PAYMENT.Value != "180" || hidACTION_ON_PAYMENT.Value != "181")                    
                    {
                        txtREASON_DESCRIPTION_CASE.Text = dr["REASON_DESCRIPTION"].ToString();
                    }
                  
					if(dr["ACTIVITY_STATUS"]!=null && dr["ACTIVITY_STATUS"].ToString()!="")
						hidACTIVITY_STATUS.Value = dr["ACTIVITY_STATUS"].ToString();
					else
						hidACTIVITY_STATUS.Value = "";
					hidIS_ACTIVE.Value = dr["IS_ACTIVE"].ToString();
                    if (hidIS_ACTIVE.Value.ToUpper() == "Y")
                        btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchActivateDeactivateButtonsText("Y");
                    else
                        btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchActivateDeactivateButtonsText("N");
						
					//if(dr["RESERVE_TRAN_CODE"]!=null && dr["RESERVE_TRAN_CODE"].ToString()!="" && dr["RESERVE_TRAN_CODE"].ToString()!="0")
					//	cmbRESERVE_TRAN_CODE.SelectedValue = dr["RESERVE_TRAN_CODE"].ToString();

                    //============= Added by Shubhanshu for Itrack 1356 (tfs # 870)(To adding Claim Receipt Amount into hist text) on 12/07/2011.
                    if (dsTemp.Tables.Count > 3 && dsTemp.Tables[3].Rows.Count > 0)
                    {
                        hidPayment_Amt.Value= dsTemp.Tables[3].Rows[0]["Amount"].ToString();
                    }

				}
			}
		}
		#endregion
		//Added for Itrack Issue 6079 on 10 July 2009
		[Ajax.AjaxMethod()]
		public string CheckCloseReserveDetails(string claim_id,int intACTION_ON_PAYMENT)
		{	
			ClsReserveDetails obj = new ClsReserveDetails();
			string result = "0";
			//Added For Itrack Issue #6372/6274.
			result = obj.CheckCloseReserveDetails(claim_id.Trim(),intACTION_ON_PAYMENT);	
			return result;
			
		}

		//Added for Itrack Issue 7169 on 4 May 2010
		[Ajax.AjaxMethod()]
		public string CheckVoidedActivity(int intACTION_ON_PAYMENT)
		{	
			ClsActivity obj = new ClsActivity();
			string result = "0";
			result = obj.CheckVoidedActivity(intACTION_ON_PAYMENT);	
			return result;
			
		}

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int intRetVal;

                if ((hidACTIVITY_STATUS.Value != ((int)enumClaimActivityStatus.COMPLETE).ToString()) && (hidACTIVITY_STATUS.Value != ((int)enumClaimActivityStatus.VOID).ToString()))//Added for Itrack Issue 7169 on 15 Oct 2010
                {
                    //Retreiving the form values into model class object
                    ClsActivityInfo objActivityInfo = GetFormValue();

                    //Calling the add method of business layer class
                    intRetVal = objActivity.DeleteActivity(int.Parse(hidCLAIM_ID.Value),int.Parse(hidACTIVITY_ID.Value));


                    if (intRetVal > 0)
                    {
                        hidACTIVITY_ID.Value = objActivityInfo.ACTIVITY_ID.ToString();
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "9");
                        hidFormSaved.Value = "1";
                        hidIS_ACTIVE.Value = "Y";
                        LoadData();
                        GetOldDataXML();
                        chkACCOUNTING_SUPPRESSED.Visible = false;
                        capACCOUNTING_SUPPRESSED.Visible = false;
                        capACCOUNTING_SUPPRESSED_CHECKED.Visible = false;
                        btnVoidActivity.Visible = false;
                        btnReverseActivity.Visible = false;
                        btnDelete.Visible = false;
                        btnContinue.Visible = false;
                        btnCompleteActivity.Visible = false;
                        btnActivateDeactivate.Visible = false;
                    }
                    else if (intRetVal == -1) //Duplicate Authority Limit
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "18");
                        hidFormSaved.Value = "2";
                    }

                    else
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "20");
                        hidFormSaved.Value = "2";
                    }

                    lblMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";

            }
        }

        protected void btnClaimReciept_Click1(object sender, EventArgs e)
        {

        }

        

       
	}
}
