using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using Cms.CmsWeb;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Cms.BusinessLayer.BlCommon;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.BusinessLayer.BlClient;
using System.Xml;
using System.Web.Mail;
using Cms.CmsWeb.webcontrols;
using Cms.Model.Client;
using System.IO;



namespace Cms.Client.Aspx
{
	/// <summary>
	/// Summary description for Email.
	/// </summary>
	public class Email : Cms.Client.clientbase
	{
		protected System.Web.UI.WebControls.Label lblMessage1;
		
		protected System.Web.UI.WebControls.Label lblATTACHMENT;
		
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTAX_STATE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRootPath;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRECIPIENTS;
		protected System.Web.UI.WebControls.TextBox txtEMAIL_FROM_NAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvFROM_NAME;
		protected System.Web.UI.WebControls.Label capEMAIL_FROM_NAME;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidClaim;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicy;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidApplication;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidQuote;
		protected System.Web.UI.WebControls.Label capEMAIL_FROM;
		protected System.Web.UI.WebControls.TextBox txtEMAIL_FROM;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvFROM_EMAIL;
		protected System.Web.UI.WebControls.Label capEMAIL_SUBJECT;
		protected System.Web.UI.WebControls.TextBox txtEMAIL_SUBJECT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSUBJECT;
		protected System.Web.UI.WebControls.Label capTO;
		//protected System.Web.UI.WebControls.TextBox txtTO;
		protected System.Web.UI.WebControls.Label capRECIPIENTS;
		protected System.Web.UI.WebControls.Label capEMAIL_MESSAGE;
		protected System.Web.UI.WebControls.Label capApplication;
		protected System.Web.UI.WebControls.Label capPOLICY_NUMBER;
		protected System.Web.UI.WebControls.Label capCLAIM_NUMBER;
		protected System.Web.UI.WebControls.Label capQuote;
		protected System.Web.UI.WebControls.TextBox txtEMAIL_MESSAGE;
		protected System.Web.UI.WebControls.TextBox txtRECIPIENTS;
		protected System.Web.UI.WebControls.Label capATTACHMENT;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSend;
		protected System.Web.UI.WebControls.Label capErrMessage;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidInvalid_RECIPIENTS;
		protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;

		//int			intCUSTOMER_ID = 0;
		//string		strRowId ;
		int			intLoggedInUserID;
		int			custID;
		int			claimID;
		string		strDirName;
		string strFileName;

        private string strServerName = System.Configuration.ConfigurationManager.AppSettings.Get("ServerName").ToString();
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMESSAGE;
		protected System.Web.UI.HtmlControls.HtmlTableRow trMessage;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRowId;
		protected System.Web.UI.WebControls.RegularExpressionValidator revFROM_EMAIL;
		protected System.Web.UI.HtmlControls.HtmlInputFile txtATTACHMENT;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.WebControls.Label capCONTACTDETAILS;
		protected System.Web.UI.WebControls.ListBox cmbCONTACTDETAILS;
		protected System.Web.UI.WebControls.Label capADDITIONAL;
		protected System.Web.UI.WebControls.ListBox cmbRECIPIENTS;
		protected System.Web.UI.WebControls.TextBox txtADDITIONAL;
		protected System.Web.UI.WebControls.Button btnSELECT;
		protected System.Web.UI.WebControls.Button btnDESELECT;
		protected System.Web.UI.WebControls.Label capRECIPIENTS1;
		protected System.Web.UI.WebControls.RegularExpressionValidator revADDITIONAL;
		protected System.Web.UI.WebControls.Label capDIARY_ITEM_REQ;
		protected System.Web.UI.WebControls.DropDownList cmbDIARY_ITEM_REQ;
		protected System.Web.UI.WebControls.Label capFOLLOW_UP_DATE;
		protected System.Web.UI.WebControls.TextBox txtFOLLOW_UP_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkFOLLOW_UP_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvFOLLOW_UP_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revFOLLOW_UP_DATE;
		protected System.Web.UI.WebControls.CustomValidator csvFOLLOW_UP_DATE;		
		protected System.Web.UI.WebControls.TextBox txtPOLICY_NUMBER;		
		protected System.Web.UI.WebControls.TextBox txtCLAIM_NUMBER;		
		protected System.Web.UI.WebControls.TextBox txtAPP_NUMBER;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_NUMBER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID_NAME;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_APP_NUMBER;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidMergeId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidQQ_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_APP_CUSTOMER_ID_NAME;
		protected System.Web.UI.WebControls.Label capDIARY_ITEM_TO;
		protected System.Web.UI.WebControls.DropDownList cmbDIARY_ITEM_TO;

		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvRECIPIENTS;
		protected System.Web.UI.WebControls.CustomValidator csvRECIPIENTS;	
		protected System.Web.UI.WebControls.TextBox txtQuote;
		protected string strCalledFor="";
		protected int intToUserID=0; // holds the value of touserid	

		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			intLoggedInUserID		=	int.Parse(base.GetUserId());
			if(GetCalledFor()!="CLAIM")
				base.ScreenId			=	"109_0";//can be Alpha Numeric
			else
				base.ScreenId			=	"314_0";
			hlkFOLLOW_UP_DATE.Attributes.Add("OnClick","fPopCalendar(document.getElementById('txtFOLLOW_UP_DATE'),document.getElementById('txtFOLLOW_UP_DATE'))"); //Javascript Implementation for Calender		
			cmbDIARY_ITEM_REQ.Attributes.Add("Onclick","javascript:return ShowFOLLOW_UP_DATE();");   
			cmbDIARY_ITEM_REQ.Attributes.Add("Onblur","javascript:return ShowFOLLOW_UP_DATE();");   
				
			btnReset.Attributes.Add("onclick","javascript:document.forms[0].reset();return false;");
			btnSELECT.Attributes.Add("onclick","javascript:selectRecipients();return false;");
			btnDESELECT.Attributes.Add("onclick","javascript:deselectRecipients();return false;");
			btnSend.Attributes.Add("onclick","javascript:return setRecipients();"); 
           
			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass					=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnReset.PermissionString				=	gstrSecurityXML;	
				
			btnSend.CmsButtonClass					=	Cms.CmsWeb.Controls.CmsButtonType.Execute;
			btnSend.PermissionString				=	gstrSecurityXML;
			
			objResourceMgr = new System.Resources.ResourceManager("cms.client.Aspx.Email",System.Reflection.Assembly.GetExecutingAssembly());
			
	        custID=GetCustomerID()=="" ? 0 : int.Parse(GetCustomerID()); 
			hidCUSTOMER_ID.Value=Convert.ToString(custID);
			
			SetPageLabels();
			//SetFocus("txtEMAIL_FROM");

            hidRootPath.Value = System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString() + "//Email//" + custID.ToString();					
			//FillUserEmail();
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
		
			#region If form is not posted back then setting the default values
			if( ! Page.IsPostBack)
			{
				//Setting xml for the page to be displayed in page controls
				if(Request.QueryString["EMAIL_ROW_ID"]!=null && Request.QueryString["EMAIL_ROW_ID"].ToString().Length>0)
				{
					int intEmailRowId= int.Parse(Request.QueryString["EMAIL_ROW_ID"].ToString());
					int intCustomerId =int.Parse(Request.QueryString["CUSTOMER_ID"].ToString());
					hidOldData.Value = ClsEmail.GetEmailXml(intEmailRowId,intCustomerId);					
				}
				if(Request.QueryString["cmbDIARY_ITEM_TO"]!=null)
					intToUserID=int.Parse(Request.QueryString["cmbDIARY_ITEM_TO"].ToString());
				else
					intToUserID=int.Parse(GetUserId().ToString());

				SetCaptions();
				loadDropDowns();
				SetPageLabels();
				GetUserEmail();
				GetCustomerEMail();
				GetApplicantEMail();
				GetCSREMail();
				GetCustomerAppPolicyValues();
			
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
			this.txtEMAIL_MESSAGE.TextChanged += new System.EventHandler(this.txtEMAIL_MESSAGE_TextChanged);
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		private void  loadDropDowns()
		{
			try
			{
				int UserId		=	intToUserID == 0 ? -1 : intToUserID;
				cmbDIARY_ITEM_TO.DataSource		=	ClsCommon.GetUserList();
				cmbDIARY_ITEM_TO.DataTextField	=	USERNAME;
				cmbDIARY_ITEM_TO.DataValueField	=	USERID;
				cmbDIARY_ITEM_TO.DataBind();

				ListItem li=new ListItem(); 
				li=cmbDIARY_ITEM_TO.Items.FindByValue(intToUserID.ToString());   
				if(li!=null)
					li.Selected=true;

                cmbDIARY_ITEM_REQ.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YES_NO");
                cmbDIARY_ITEM_REQ.DataTextField = "LookupDesc";
                cmbDIARY_ITEM_REQ.DataValueField = "LookupCode";
                cmbDIARY_ITEM_REQ.DataBind();
                cmbDIARY_ITEM_REQ.Items.Insert(0, "");
			}
			catch(Exception objExcep)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
			}

		}
	
		private void btnReset_Click(object sender, System.EventArgs e)
		{
		
		}

		#region Filling Data CALLING FUNCTIONS
		private void GetCustomerAppPolicyValues()
		{
			strCalledFor=GetCalledFor();

			if(strCalledFor=="CUSTOMER" || strCalledFor=="APPLICATION")
			{
				DataSet DsQQApp=new DataSet();
				if(GetAppID()!="")
					hidAPP_ID.Value=GetAppID();

				if(GetAppVersionID()!="")
					hidAPP_VERSION_ID.Value = GetAppVersionID();

				if(GetQQ_ID()!="")
					hidQQ_ID.Value=GetQQ_ID();
					
				if(strCalledFor=="CUSTOMER")
				{
					DsQQApp = ClsEmail.GetQQAppNumber(0,int.Parse(hidQQ_ID.Value), int.Parse(hidCUSTOMER_ID.Value),strCalledFor);
					if(DsQQApp!=null && DsQQApp.Tables[0].Rows.Count > 0)
					{
						if(DsQQApp.Tables[0].Rows[0]["CUSTOMER_NAME"]!=null && DsQQApp.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString()!="")
						{
							txtEMAIL_SUBJECT.Text = DsQQApp.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString();
						}
							
					}
				}
				else
					DsQQApp = ClsEmail.GetQQAppNumber(int.Parse(hidAPP_ID.Value),0, int.Parse(hidCUSTOMER_ID.Value),strCalledFor);


				if(DsQQApp!=null && DsQQApp.Tables[0].Rows.Count > 0)
				{
					if(DsQQApp.Tables[0].Rows[0]["APP_NUMBER"]!=null && DsQQApp.Tables[0].Rows[0]["APP_NUMBER"].ToString()!="" && DsQQApp.Tables[0].Rows[0]["APP_NUMBER"].ToString()!="0")
					{
						txtAPP_NUMBER.Text= DsQQApp.Tables[0].Rows[0]["APP_NUMBER"].ToString();
						txtEMAIL_SUBJECT.Text = DsQQApp.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString() + "/" + DsQQApp.Tables[0].Rows[0]["APP_NUMBER"].ToString();
					}
					if(DsQQApp.Tables[0].Rows[0]["QQ_NUMBER"]!=null && DsQQApp.Tables[0].Rows[0]["QQ_NUMBER"].ToString()!="" && DsQQApp.Tables[0].Rows[0]["QQ_NUMBER"].ToString()!="0")
						txtQuote.Text=DsQQApp.Tables[0].Rows[0]["QQ_NUMBER"].ToString();
				}
			}
			else if(strCalledFor=="POLICY")
			{
				if(GetPolicyID()!="")
					hidPOLICY_ID.Value=GetPolicyID();
				if(GetPolicyVersionID()!="")
					hidPOLICY_VERSION_ID.Value = GetPolicyVersionID();

				DataSet PolDs = ClsEmail.GetPolicyAppNumber(int.Parse(hidPOLICY_ID.Value), int.Parse(hidCUSTOMER_ID.Value));

				if(PolDs!=null && PolDs.Tables[0].Rows.Count>0)
				{
					if(PolDs.Tables[0].Rows[0]["POLICY_NUMBER"]!=null && PolDs.Tables[0].Rows[0]["POLICY_NUMBER"].ToString()!="" && PolDs.Tables[0].Rows[0]["POLICY_NUMBER"].ToString()!="0")
					{
						txtPOLICY_NUMBER.Text= PolDs.Tables[0].Rows[0]["POLICY_NUMBER"].ToString();
						txtEMAIL_SUBJECT.Text = PolDs.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString() + "/" + PolDs.Tables[0].Rows[0]["POLICY_NUMBER"].ToString();
					}
					if(PolDs.Tables[0].Rows[0]["APP_NUMBER"]!=null && PolDs.Tables[0].Rows[0]["APP_NUMBER"].ToString()!="" && PolDs.Tables[0].Rows[0]["APP_NUMBER"].ToString()!="0")
						txtAPP_NUMBER.Text= PolDs.Tables[0].Rows[0]["APP_NUMBER"].ToString();
					if(PolDs.Tables[0].Rows[0]["QQ_NUMBER"]!=null && PolDs.Tables[0].Rows[0]["QQ_NUMBER"].ToString()!="" && PolDs.Tables[0].Rows[0]["QQ_NUMBER"].ToString()!="0")
						txtQuote.Text=PolDs.Tables[0].Rows[0]["QQ_NUMBER"].ToString();
				}
			}
			else if(strCalledFor=="CLAIM")
			{
				claimID=GetClaimID()=="" ? 0 : int.Parse(GetClaimID());
				hidCLAIM_ID.Value=Convert.ToString(claimID);
				if(hidCLAIM_ID.Value!=null && hidCLAIM_ID.Value!="" && hidCLAIM_ID.Value!="0")
				{
					DataSet ds= ClsEmail.GetClaimDetails(int.Parse(hidCLAIM_ID.Value));
					if(ds !=null && ds.Tables[0].Rows.Count>0)
					{
						if(ds.Tables[0].Rows[0]["CLAIM_NUMBER"]!=null && ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString() != "" && ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString() !="0")
							txtCLAIM_NUMBER.Text = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
						if(ds.Tables[0].Rows[0]["POLICY_ID"]!=null && ds.Tables[0].Rows[0]["POLICY_ID"].ToString() != "" && ds.Tables[0].Rows[0]["POLICY_ID"].ToString()!="0")
						{
							hidPOLICY_ID.Value = ds.Tables[0].Rows[0]["POLICY_ID"].ToString();
							hidPOLICY_VERSION_ID.Value=ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString();

							DataSet PolDs = ClsEmail.GetPolicyAppNumber(int.Parse(hidPOLICY_ID.Value), int.Parse(hidCUSTOMER_ID.Value));
							if(PolDs!=null && PolDs.Tables[0].Rows.Count > 0)
							{
								if(PolDs.Tables[0].Rows[0]["POLICY_NUMBER"]!=null && PolDs.Tables[0].Rows[0]["POLICY_NUMBER"].ToString()!="" && PolDs.Tables[0].Rows[0]["POLICY_NUMBER"].ToString()!="0")
                                    txtPOLICY_NUMBER.Text= PolDs.Tables[0].Rows[0]["POLICY_NUMBER"].ToString();
							}
						}
					}
				}
			}
		}
		#endregion

		private void btnSend_Click(object sender, System.EventArgs e)
		{
			//string strTo			=		"aa";//txtTO.Text.Trim().Replace("'","''");
			string strFrom			=		txtEMAIL_FROM.Text.Trim().Replace("'","''");
			string strSubject		=		txtEMAIL_SUBJECT.Text.Trim().Replace("'","''");
			string strMessage		=		txtEMAIL_MESSAGE.Text.Trim().Replace("'","''");	
			if(AttachmentFileName()!="")
			{
				string strAttachment	=		AttachmentFileName();
			}

			string recipient=(string)hidRECIPIENTS.Value;
			if (recipient !="" && recipient != "0")
			{
				string[] recipients= recipient.Split(',');  
				recipient="";
				for (int i=0;i <recipients.GetLength(0)-1 ;i++)
				{
					recipient=recipient + recipients[i].ToString()  + ","; 	
				}
			recipient = recipient.Substring(0,recipient.LastIndexOf(","));
			}			
			if (recipient =="0" ) 
				recipient="";	
			if (SendEmail(strFrom,strSubject,recipient,strMessage)==false)
			{
				lblMessage1.Text += "\n Email can not be sent.";
				lblMessage1.Visible=true;
				hidFormSaved.Value="2";
				return;
			}
			
				ClsEmail objEmail=new ClsEmail();
				ClsEmailInfo objEmailInfo=GetFormValue();
				int intRetVal=0;
			 	
			    intRetVal=objEmail.AddNew(objEmailInfo,intLoggedInUserID,int.Parse(hidPOLICY_ID.Value) ,int.Parse(hidPOLICY_VERSION_ID.Value),int.Parse(hidAPP_ID.Value),int.Parse(hidAPP_VERSION_ID.Value),int.Parse(hidQQ_ID.Value),int.Parse(hidCLAIM_ID.Value));
				if( intRetVal > 0 )			
				{
					lblMessage1.Text				=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"984");
					hidFormSaved.Value				=	"1";
					hidRowId.Value=Convert.ToString(objEmailInfo.EMAIL_ROW_ID);				
					hidOldData.Value = ClsEmail.GetEmailXml(intRetVal,int.Parse(hidCUSTOMER_ID.Value));
				}
				else if(intRetVal == -1)	// Duplicate code exist, update failed
				{	
					lblMessage1.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"18");
					hidFormSaved.Value			=	"2";
				}
				else						// Error occured while processing, update failed
				{
					lblMessage1.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
					hidFormSaved.Value			=	"2";
				}
			
			#region Saving the TRANSACTION LOG			
			/*Model.Maintenance.ClsTransactionInfo objTrans = new Model.Maintenance.ClsTransactionInfo();
			ClsCommon objCommon								= new ClsCommon();
			objTrans.CLIENT_ID								= Convert.ToInt32(GetCustomerID());
			objTrans.RECORD_DATE_TIME						= DateTime.Now;
			objTrans.RECORDED_BY							= Convert.ToInt32(GetUserId());
			string TransDesc								= "Email has been Send From " + strFrom + " To " + recipient ; 
			if (strAttachment!="")
			{
				TransDesc = TransDesc + "<br><br>To view mail attachement <a href='"+strDirName+"\\"+ strAttachment +"' target='blank'>click here</a>.";
			}
			objTrans.TRANS_DESC				=		TransDesc;
			objTrans.CHANGE_XML				=		"<LabelFieldMapping><Map label='Message' field='Message' OldValue='null' NewValue='" + strMessage + "' /></LabelFieldMapping>";			
			objCommon.TransactionLogEntry(objTrans);*/
			#endregion
			
		
			lblMessage1.Visible=true;					   
			
		}
					
		private void SetCaptions()
		{
			capEMAIL_FROM_NAME.Text		=		objResourceMgr.GetString("txtEMAIL_FROM_NAME");
			capEMAIL_FROM.Text			=		objResourceMgr.GetString("txtEMAIL_FROM");
			capEMAIL_SUBJECT.Text		=		objResourceMgr.GetString("txtEMAIL_SUBJECT");
			capTO.Text					=		objResourceMgr.GetString("cmbRECIPIENTS");
			capRECIPIENTS.Text			=		objResourceMgr.GetString("txtRECIPIENTS");
			capEMAIL_MESSAGE.Text		=		objResourceMgr.GetString("txtEMAIL_MESSAGE");
			capATTACHMENT.Text			=		objResourceMgr.GetString("txtATTACHMENT");  // ASHISH
			//changes by uday to hide attachment text when file is not attached
			//if(objResourceMgr.GetString("txtATTACHMENT")=="")
           // capATTACHMENT.Text			=       "";
			//
			capFOLLOW_UP_DATE.Text		=		objResourceMgr.GetString("txtFOLLOW_UP_DATE");
			capDIARY_ITEM_REQ.Text		=		objResourceMgr.GetString("txtDIARY_ITEM_REQ");
			capApplication.Text			=		objResourceMgr.GetString("txtAPP_NUMBER");
			capPOLICY_NUMBER.Text		=		objResourceMgr.GetString("txtPOLICY_NUMBER");
			capCLAIM_NUMBER.Text		=		objResourceMgr.GetString("txtCLAIM_NUMBER");
			capQuote.Text				=		objResourceMgr.GetString("txtQuote");
            capCONTACTDETAILS.Text      =       objResourceMgr.GetString("txtCONTACTDETAILS"); //ASHISH
            btnSend.Text                =        objResourceMgr.GetString("btnSend");     // ASHISH
            capADDITIONAL.Text          =       objResourceMgr.GetString("txtADDITIONAL"); //ASHISH
            capRECIPIENTS1.Text         =       objResourceMgr.GetString("txtRECIPIENTS1"); //ASHISH
            hidClaim.Value              =          objResourceMgr.GetString("hidClaim");
            hidPolicy.Value     =         objResourceMgr.GetString("hidPolicy");
            hidApplication.Value  =       objResourceMgr.GetString("hidApplication");
            hidQuote.Value  =             objResourceMgr.GetString("hidQuote");
            capDIARY_ITEM_TO.Text = objResourceMgr.GetString("capDIARY_ITEM_TO");
        }


		private void SetPageLabels()
		{			
			
			//setting error message by calling singleton functions
			rfvFROM_NAME.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"1");
			rfvFROM_EMAIL.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"2");
			rfvSUBJECT.ErrorMessage				=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"3");
			rfvMESSAGE.ErrorMessage				=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"5");
			revADDITIONAL.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"124");
			revADDITIONAL.ValidationExpression	=	aRegExpEmail;
			revFROM_EMAIL.ValidationExpression	=	aRegExpEmail;
			revFROM_EMAIL.ErrorMessage			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("23");
			revFOLLOW_UP_DATE.ValidationExpression			=   aRegExpDate;
			revFOLLOW_UP_DATE.ErrorMessage		=	"<br>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			csvFOLLOW_UP_DATE.ErrorMessage		=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6");
			rfvFOLLOW_UP_DATE.ErrorMessage		=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7"); 
			csvRECIPIENTS.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4"); 

		}

		private void txtEMAIL_MESSAGE_TextChanged(object sender, System.EventArgs e)
		{
		
		}
		#region Send Email
		/// <summary>
		/// This function is used to send email if password found
		/// </summary>
		/// <param name="strEmail">Email Id of the User</param>
		/// <returns></returns>
		public bool SendEmail(string strFromEmail, string strSubject, string strRecipients, string strMessage)
		{
			try
			{				
				#region Save Attachment	- Attach File
                string lstrUserName = System.Configuration.ConfigurationManager.AppSettings.Get("IUserName");
                string lstrPassword = System.Configuration.ConfigurationManager.AppSettings.Get("IPassWd");
                string lstrDomain = System.Configuration.ConfigurationManager.AppSettings.Get("IDomain");
					
				ClsAttachment lImpertionation =  new ClsAttachment();
				if (lImpertionation.ImpersonateUser(lstrUserName,lstrPassword,lstrDomain))
				{							
					if ( SaveUploadedFile(txtATTACHMENT) == false)
					{
						lblMessage1.Text += "\n Unable to upload the file.";
						lblMessage1.Visible=true;
					}
					//lImpertionation.endImpersonation();
					//}
						

					MailMessage objMailMessage =  new MailMessage();
				
					objMailMessage.To				=	strRecipients;
					objMailMessage.From				=	strFromEmail;
					//objMailMessage.Cc				=   strRecipients;
					objMailMessage.BodyFormat		=	MailFormat.Html;
					objMailMessage.Subject			=	strSubject;
					objMailMessage.Priority			=	MailPriority.Normal;			
					if (txtATTACHMENT.PostedFile != null && txtATTACHMENT.PostedFile.FileName != "")
					{
						HttpPostedFile attFile = txtATTACHMENT.PostedFile;
						int attachFileLength = attFile.ContentLength; 
						string strFilePath;
						//string strFileName;
						if (attachFileLength > 0)
						{ 
							try
							{
						
								//strFileName = txtATTACHMENT.PostedFile.FileName; 
								strFilePath = strDirName + "\\" + strFileName;
								MailAttachment attach = new MailAttachment(strFilePath);
								objMailMessage.Attachments.Add(attach);
							
							}
							catch(Exception exp)
							{
								lblMessage1.Text = "Invalid Attachment.";
								lblMessage1.Visible=true;
								Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(exp);
								return false;
							}
						}
					} 
					objMailMessage.Body				=	strMessage;
					SmtpMail.SmtpServer				=	strServerName;
					//SmtpMail.SmtpServer.Insert(0,"127.0.0.1");
				
					try
					{
						SmtpMail.Send(objMailMessage);
					}
					catch(Exception exp)
					{
						lblMessage1.Text = "Invalid E-Mail address(s).";
						lblMessage1.Visible=true;
						hidInvalid_RECIPIENTS.Value = "1";
						Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(exp);
						return false;
					}
					hidInvalid_RECIPIENTS.Value = "0";

					lImpertionation.endImpersonation();
					return true;
				}
				else //If Impersonation Fails
					return false;

				#endregion 
			}
			catch(Exception exp)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(exp);
				return false;
			}
		}
		#endregion

		private void GetUserEmail()
		{
			DataRow drCustDetails;
			
			ClsCustomer ojCustomer;
			ojCustomer			=	new ClsCustomer();
			int UserId			=	Convert.ToInt32(GetUserId());
			//drCustDetails		=	ojCustomer.FillCustomerEmail(custID).Rows[0];
			drCustDetails		=	ojCustomer.FillUserEmail(UserId).Rows[0];

			txtEMAIL_FROM_NAME.Text	=	drCustDetails[0].ToString() + " " + drCustDetails[1].ToString();
			txtEMAIL_FROM.Text	=	drCustDetails[2].ToString();
		}
		private void GetCustomerEMail()
		{
			DataRow drCustDetails;
			
			ClsCustomer ojCustomer;
			ojCustomer			=	new ClsCustomer();
			int CustId			=	Convert.ToInt32(GetCustomerID());
			//drCustDetails		=	ojCustomer.FillCustomerEmail(custID).Rows[0];
			if(txtRECIPIENTS.Text.Trim()=="")
			{
				drCustDetails		=	ojCustomer.FetchCustomerEMailAddress(CustId).Rows[0];

				txtRECIPIENTS.Text	=	drCustDetails[0].ToString();
			}
			
		}
		private void GetApplicantEMail()
		{
			
			ClsCustomer ojCustomer;
			ojCustomer			=	new ClsCustomer();
			int CustId			=	Convert.ToInt32(GetCustomerID());
						
			cmbCONTACTDETAILS.DataSource  		=	ojCustomer.FetchApplicantEMailAddress(CustId,false);
			cmbCONTACTDETAILS.DataTextField ="CUSTOMER_NAME";
			cmbCONTACTDETAILS.DataValueField ="CUSTOMER_Email"; 
			cmbCONTACTDETAILS.DataBind(); 
			
		}
		private void GetCSREMail()
		{
			
			ClsCustomer ojCustomer;
			ojCustomer			=	new ClsCustomer();
			int CustId			=	Convert.ToInt32(GetCustomerID());
						
			cmbRECIPIENTS.DataSource  		=	ojCustomer.FetchApplicantEMailAddress(CustId,true);
			cmbRECIPIENTS.DataTextField ="CUSTOMER_NAME";
			cmbRECIPIENTS.DataValueField ="CUSTOMER_Email"; 
			cmbRECIPIENTS.DataBind(); 
			
		}


		public ClsEmailInfo GetFormValue()
		{
			ClsEmailInfo objMailInfo=new ClsEmailInfo();	
	
			objMailInfo.CUSTOMER_ID=custID;
			objMailInfo.CREATED_BY=int.Parse(GetUserId());
			objMailInfo.EMAIL_FROM_NAME=txtEMAIL_FROM_NAME.Text;
			objMailInfo.EMAIL_FROM=txtEMAIL_FROM.Text;
			objMailInfo.CLAIM_NUMBER=txtCLAIM_NUMBER.Text;
			objMailInfo.POLICY_NUMBER=txtPOLICY_NUMBER.Text;
			objMailInfo.APP_NUMBER=txtAPP_NUMBER.Text;
			objMailInfo.QUOTE=txtQuote.Text;
			//objMailInfo.EMAIL_TO=txtTO.Text;

			/*string recipient=(string)hidRECIPIENTS.Value;
			if (recipient !="" && recipient != "0")
			{
				string[] recipients= recipient.Split(',');  
				recipient="";
				recipient=recipients[0].ToString();
				for (int i=0;i <recipients.GetLength(0)-1 ;i++)
				{
				//	recipient=recipient + recipients[i].ToString() + ", ";											
					recipient=recipient + ", " + recipients[i].ToString();	
				}
			}*/
			string recipient=(string)hidRECIPIENTS.Value;
			if (recipient !="" && recipient != "0")
			{
				string[] recipients= recipient.Split(',');  
				recipient="";
				for (int i=0;i <recipients.GetLength(0)-1 ;i++)
				{
					recipient=recipient + recipients[i].ToString()  + ","; 	
				}
				recipient = recipient.Substring(0,recipient.LastIndexOf(","));
			}			



			if (recipient =="0" ) 
				recipient="";			
			objMailInfo.EMAIL_RECIPIENTS=recipient;
			objMailInfo.EMAIL_SUBJECT=txtEMAIL_SUBJECT.Text;
			objMailInfo.EMAIL_MESSAGE=txtEMAIL_MESSAGE.Text;
			objMailInfo.EMAIL_ATTACH_PATH=AttachmentFileName();
			objMailInfo.EMAIL_SEND_DATE=DateTime.Now;
			objMailInfo.DIARY_ITEM_REQ =cmbDIARY_ITEM_REQ.SelectedValue;
			if (cmbDIARY_ITEM_REQ.SelectedValue == "Y")
			{
				objMailInfo.FOLLOW_UP_DATE= Convert.ToDateTime(txtFOLLOW_UP_DATE.Text);    
				objMailInfo.DIARY_ITEM_TO = int.Parse(cmbDIARY_ITEM_TO.SelectedValue);  

			}
			return objMailInfo;
		}

		public string AttachmentFileName()
		{
			//string strFileName;
			int Index = txtATTACHMENT.PostedFile.FileName.LastIndexOf("\\");
			if (Index >=0 )
			{
				strFileName = txtATTACHMENT.PostedFile.FileName.Substring(Index+1);
			}
			else
			{
				strFileName = txtATTACHMENT.PostedFile.FileName;
			}
			return strFileName;
		}

		private bool SaveUploadedFile(HtmlInputFile objFile)
		{
			try
			{
                strDirName = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString()) + "\\Email\\" + custID.ToString();
				if (!System.IO.Directory.Exists(strDirName))
				{
					System.IO.Directory.CreateDirectory(strDirName);
				}
				//Retreiving the extension
				
				int Index = objFile.PostedFile.FileName.LastIndexOf("\\");
				if (Index >=0 )
				{
					strFileName = objFile.PostedFile.FileName.Substring(Index+1);
				}
				else
				{
					strFileName = objFile.PostedFile.FileName;
				}
				
				//copying the file
				if(strFileName!="")
					objFile.PostedFile.SaveAs(strDirName + "\\" + strFileName);
				else
					return false;

				return true;
			}
			catch (Exception objExp)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
				return false;
			}
		}		
		
		
	}
}
