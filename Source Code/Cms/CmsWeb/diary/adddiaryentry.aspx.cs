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
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb.WebControls;
using System.Resources;
using System.Reflection;  
using Cms.Model.Diary;  


/******************************************************************************************
	<Author					: - Vijay Joshi>
	<Start Date				: -	February 14, 2005>
	<End Date				: - >
	<Description			: - understanding Diary Module object approach>
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: -March 17, 2005 >
	<Modified By			: - Gaurav Tyagi>
	<Purpose				: - Change procedure to get user name>
    
    <Modified Date			: - >April 6, 2005 
	<Modified By			: - >Anurag Verma
	<Purpose				: - >Implementing calendarpicker control
    
    <Modified Date			: - >April 8, 2005 
	<Modified By			: - >Anurag Verma
	<Purpose				: - >changing hidOperation.Value="1" on update and assigning 

rowId.value=strRowId for having primary key
    

    <Modified Date			: - >April 12, 2005 
	<Modified By			: - >Anurag Verma
	<Purpose				: - >making hidden field according to convention
    
    <Modified Date			: - >April 14, 2005 
	<Modified By			: - >Anurag Verma
	<Purpose				: - > creating buttons on edit mode
    
    <Modified Date			: - > April 22, 2005 
	<Modified By			: - > Anurag Verma
	<Purpose				: - > chainging file for implementing model object
    
    <Modified Date			: - > May 05, 2005 
	<Modified By			: - > Anurag Verma
	<Purpose				: - > Changing code to implement new approach
    
    <Modified Date			: - > May 31, 2005 
	<Modified By			: - > Anurag Verma
	<Purpose				: - > Changing code to remove error related with 

notification list field
    
    <Modified Date			: - > June 22, 2005 
	<Modified By			: - > Anurag Verma
	<Purpose				: - > Change in aspx for transfer entry
	
	<Modified Date			: - > June 27, 2005
	<Modified By			: - > Anshuman
	<Purpose				: - > Adding fields customer_id, app_id, policy_id etc.
    
    <Modified Date			: - > June 28, 2005
	<Modified By			: - > Anurag Verma
	<Purpose				: - > Making provisions for deleting InComplete entries
	
	<Modified Date			: - 26/08/2005
	<Modified By			: - Anurag Verma
	<Purpose				: - Applying Null Check for buttons on aspx page 
*******************************************************************************************/
namespace Cms.CmsWeb.Diary
{
	/// <summary>
	/// Summary description for AddDiaryEntry.
	/// </summary>
	public class AddDiaryEntry : Cms.CmsWeb.cmsbase
	{

		
		//These variables are used to retreive the values from controls 
		private int			intFromUserId; //holds the value of the user id adding entry in data(loggedin user)
		
        /*
        private int			intToUserId; // holds the value of touserid
		private int			intListType; // holds the value of listtypeid
		private int			intNotificationListType; //holds the value of systemfollowupid
		
		private string		strSubjectLine;//holds the value of subjectline
		private string		strNote;//holds the value of notes
		
		private string		strPriority;// holds the value of priority
		private string		strStartTime; // holds the value of starttime
		private string		strEndTime; // holds the value of endtime
		private string		strFollowUp; //holds the value of followupdate
		
		private DateTime	dateFollowUp;//holds the value of followupdate
		private DateTime	dateStartTime;// holds the value of starttime
		private DateTime	dateEndTime;
         */

        private string strRowId;//holds the value of listid, if new row is being added then it will have "NEW"
        private string strFormSaved;//holds value 0 or 1 or 2, 0 - form is loaded first time, 1- successfully saved, 2 -post back but could not save

		private string      stTime="";
		private string      endTime="";
		private string      fDate="";                    
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomInfo;
		
        //Commented by Charles on 22-Apr-10 for Itrack 15
		//protected System.Web.UI.WebControls.TextBox txtFollowUpDate;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvFollowUpDate;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revFollowUpDate;
		//protected System.Web.UI.WebControls.CustomValidator cfvEndTime;
        //Commented till here

		//protected System.Web.UI.WebControls.CustomValidator cfvENDTIMEAMPM;
        
		protected System.Web.UI.WebControls.CustomValidator cfvCheckTime;
		protected System.Web.UI.WebControls.HyperLink hlkCalandarDate;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOperation;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRowId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;//Done for Itrack Issue 6658 on 3 Nov 09
        protected System.Web.UI.HtmlControls.HtmlInputHidden hid_customer;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hid_policy;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hid_Quote;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hid_app;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidFollow_Up_Date;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnComplete;
		protected Cms.CmsWeb.Controls.CmsButton btnTransfer;
		protected System.Web.UI.WebControls.Image imgVerifyApp;
        
		protected Cms.CmsWeb.Controls.CmsButton btnReminder;

		
		ClsDiary objDiary	= new ClsDiary();
		//Done for Itrack Issue 6658 on 3 Nov 09
		//protected System.Web.UI.WebControls.DropDownList cmbTOUSERID;
		protected System.Web.UI.HtmlControls.HtmlSelect cmbTOUSERID;
		protected System.Web.UI.WebControls.DropDownList cmbLISTTYPEID;
		protected System.Web.UI.WebControls.DropDownList cmbPRIORITY;
		protected System.Web.UI.WebControls.DropDownList cmbSYSTEMFOLLOWUPID;
		protected System.Web.UI.WebControls.DropDownList cmbSTARTTIMEMINUTE;
		protected System.Web.UI.WebControls.DropDownList cmbSTARTTIMEMERIDIAN;
		protected System.Web.UI.WebControls.CustomValidator cfvSTARTTIME;
		protected System.Web.UI.WebControls.DropDownList cmbENDTIMEMINUTE;
		protected System.Web.UI.WebControls.TextBox txtNOTE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLISTOPEN;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRULES_VERIFIED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvFOLLOWUPDATE;
		protected System.Web.UI.WebControls.CustomValidator cfvENDTIME;
		protected System.Web.UI.WebControls.RegularExpressionValidator revFOLLOWUPDATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSUBJECTLINE;
		protected System.Web.UI.WebControls.Label lblMessage;		
		protected System.Web.UI.WebControls.Label lblFROMUSERNAME;
		protected System.Web.UI.WebControls.Label capFOLLOWUPDATE;
		protected System.Web.UI.WebControls.Label capSTARTTIMEHOUR;
        protected System.Web.UI.WebControls.Label capRecorded;
		protected System.Web.UI.WebControls.Label capENDTIMEHOUR;
		protected System.Web.UI.WebControls.Label capSUBJECTLINE;
		protected System.Web.UI.WebControls.Label capTOUSERID;
		protected System.Web.UI.WebControls.Label capPRIORITY;
		protected System.Web.UI.WebControls.TextBox txtFOLLOWUPDATE;
		protected System.Web.UI.WebControls.Label capNOTIFICATIONLIST;
		protected System.Web.UI.WebControls.DropDownList cmbSTARTTIMEHOUR;
		protected System.Web.UI.WebControls.DropDownList cmbENDTIMEHOUR;
		protected System.Web.UI.WebControls.TextBox txtSUBJECTLINE;
		protected System.Web.UI.WebControls.TextBox txtCUSTOMER_NAME;
		protected System.Web.UI.HtmlControls.HtmlImage imgCustomer;
		protected System.Web.UI.WebControls.Label capNOTE;
		protected System.Web.UI.WebControls.Label capCUSTOMER_NAME;
		protected System.Web.UI.WebControls.DropDownList cmbENDTIMEMERIDIAN;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFROMUSERNAME;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLISTID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRECDATE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFROMUSERID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSYSTEMFOLLOWUPID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTYPEDESC;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTARTTIMEHOUR;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTARTTIMEMINUTE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidENDTIMEHOUR;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidENDTIMEMINUTE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.WebControls.Label capLISTTYPEID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLISTTYPEID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTOUSERID; //Added by Charles on 14-Sep-09 for Itrack 6401
		protected System.Web.UI.WebControls.Label capAPP_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtAPP_NUMBER;
		protected System.Web.UI.WebControls.Label capPOLICY_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtPOLICY_NUMBER;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_NUMBER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID_NAME;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
        protected System.Web.UI.WebControls.Label capheader;
		
		////		
		protected System.Web.UI.WebControls.Label capQUOTE_NUMBER;			
		protected System.Web.UI.WebControls.TextBox txtQUOTE_NUMBER;
		
		// diary calendar variables
		protected string gStrEEC="0", gStrQPAC="0", gStrQPBC="0", gStrRRC="0", gStrBRC="0", 

			gStrERC="0", gStrAAC="0", cStrCRE = "0", cStrANF = "0", cStrCF = "0";
		protected string gStrAppDates="";
		protected string listtypeid1="",listtypeid2="",listtypeid3="",listtypeid4="",listtypeid5="",listtypeid6="",listtypeid7="",listtypeid8="",listtypeid9="",listtypeid10="";
		
		protected ResourceManager objRescMgr; 
		public string delStr="0";
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_APP_NUMBER;
		protected int intToUserID=0;
		protected string strCalledFrom = "";

		private const string CALLED_FROM_CLIENT = "CLT";
		protected Cms.CmsWeb.Controls.CmsButton btnPolicy;

        protected System.Web.UI.HtmlControls.HtmlTableRow trQUOTE_NUMBER;
		protected System.Web.UI.WebControls.Label capCLAIM_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtCLAIM_NUMBER;
		protected Cms.CmsWeb.Controls.CmsButton btnGoToClaim;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_APP_CUSTOMER_ID_NAME;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidQQ_ID;
		//protected System.Web.UI.WebControls.CustomValidator cfvENDTIMEAMPM;
		private const string CALLED_FROM_INCLIENT = "INCLT";
		
		#region TEMPORARY VARIABLE FOR HOLDING DATE SETTINGS
		
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			
			//btnReset.Attributes.Add ("onclick","javascript:return ResetForm('Page.Controls[0].ID');");
            btnReminder.Visible = false;
			btnReset.Attributes.Add ("onclick","javascript:return ResetTheForm();");
			//screenid of the page
			//base.ScreenId =	"0";
            capheader.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
            hid_customer.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1395");
            hid_app.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1396");
            hid_Quote.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1397");
            hid_policy.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1398");
			if(Request.QueryString["TOUSERID"]!=null)
				intToUserID=int.Parse(Request.QueryString["TOUSERID"].ToString());
			else
				intToUserID=int.Parse(GetUserId().ToString());

			if(Request.QueryString["CalledFrom"] != null)
				strCalledFrom = Request.QueryString["CalledFrom"];

//			if(Request.QueryString["QQ_NUMBER"] != null)
//			{
//				hidQQ_ID.Value = Request.QueryString["QQ_NUMBER"];
//				//txtQUOTE_NUMBER.Text = Request.QueryString["QQ_NUMBER"];
//			}

			if(strCalledFrom != "InCLT")
			{
				base.ScreenId ="1_0";
				imgCustomer.Attributes.Add("onclick","OpenCustomerLookup();");
			}
			else
			{
				base.ScreenId ="120_5_0";
				txtCUSTOMER_NAME.CssClass = "midcolora";
				txtCUSTOMER_NAME.BorderStyle = BorderStyle.None;
				imgCustomer.Visible = false;
			}
            trQUOTE_NUMBER.Style.Add("display", "none");
            
			//intFromUserId				    =	int.Parse(GetUserId());
			intFromUserId                   =   intToUserID;
			
			btnSave.PermissionString	    =	gstrSecurityXML;
			btnSave.CmsButtonClass		    =	Cms.CmsWeb.Controls.CmsButtonType.Write;
			
			btnReset.PermissionString       =	gstrSecurityXML;
			btnReset.CmsButtonClass		    =	Cms.CmsWeb.Controls.CmsButtonType.Write ;

			

			btnDelete.PermissionString      =   gstrSecurityXML;
			btnDelete.CmsButtonClass        =   Cms.CmsWeb.Controls.CmsButtonType.Delete;

			btnComplete.PermissionString    =   gstrSecurityXML;
			btnComplete.CmsButtonClass      =   Cms.CmsWeb.Controls.CmsButtonType.Write;
 
			btnTransfer.PermissionString    =   gstrSecurityXML;
			btnTransfer.CmsButtonClass      =   Cms.CmsWeb.Controls.CmsButtonType.Write;

			btnReminder.PermissionString    =   gstrSecurityXML;
			btnReminder.CmsButtonClass      =   Cms.CmsWeb.Controls.CmsButtonType.Write;
			
			btnPolicy.PermissionString		=   gstrSecurityXML;
			btnPolicy.CmsButtonClass		=   Cms.CmsWeb.Controls.CmsButtonType.Write;


			btnGoToClaim.PermissionString		=   gstrSecurityXML;
			btnGoToClaim.CmsButtonClass      =   Cms.CmsWeb.Controls.CmsButtonType.Write;

            
            
			#region code for calendar picker
               

			hlkCalandarDate.Attributes.Add("OnClick","fPopCalendar(document.DiaryForm.txtFOLLOWUPDATE,document.DiaryForm.txtFOLLOWUPDATE)"); //Javascript Implementation for Calender				
			#endregion
			
			 btnPolicy.Attributes.Add("onClick","javascript:return OpenPolicy();");
			

			#region NOT POSTBACK
			lblMessage.Visible = false;
			if(!Page.IsPostBack)
			{
				imgVerifyApp.ImageUrl    =  "~/cmsweb/Images" + GetColorScheme()  + 

					"/Rule_ver.gif";
				hidCustomInfo.Value="";
                //cmbPRIORITY.Items.Add(new ListItem("Low","L"));
                //cmbPRIORITY.Items.Add(new ListItem("Medium","M"));
                //cmbPRIORITY.Items.Add(new ListItem("High","H"));
                //cmbPRIORITY.Items.Insert(0,"");



                //cmbSYSTEMFOLLOWUPID.Items.Add(new ListItem("At the Appointment Time","0"));
                //cmbSYSTEMFOLLOWUPID.Items.Add(new ListItem("15 Minutes before appointment","15"));
                //cmbSYSTEMFOLLOWUPID.Items.Add(new ListItem("30 Minutes before appointment","30"));
                //cmbSYSTEMFOLLOWUPID.Items.Add(new ListItem("45 Minutes before appointment","45"));
                //cmbSYSTEMFOLLOWUPID.Items.Add(new ListItem("1 Hour before appointment","60"));
                //cmbSYSTEMFOLLOWUPID.Items.Insert(0,"");

				//Done for Itrack Issue 6658 on 3 Nov 09
				DataSet ds =  ClsCommon.GetToDoUserList(int.Parse(intToUserID.ToString()));
				DataTable dtUsers = ds.Tables[0];
				cmbTOUSERID.Items.Clear();
				if(dtUsers!=null && dtUsers.Rows.Count>0)
				{
					cmbTOUSERID.DataSource		=	new DataView(dtUsers);
					cmbTOUSERID.DataTextField	=	USERNAME;
					cmbTOUSERID.DataValueField	=	USERID;
					cmbTOUSERID.DataBind();
					if(ds.Tables[1].Rows[0]["IS_ACTIVE"].ToString() == "N")
					{
						hidIS_ACTIVE.Value = "N";
						cmbTOUSERID.Items.FindByValue(intToUserID.ToString()).Attributes.Add("style", "color:red");
					}
					cmbTOUSERID.Items.Insert(0,"");//Added by Charles on 14-Sep-09 for Itrack 6401
				}

				ListItem li=new ListItem(); 
				li=cmbTOUSERID.Items.FindByValue(intToUserID.ToString());   
				if(li!=null)
					li.Selected=true; 

				// fill todolist type options
				cmbLISTTYPEID.DataSource		=	ClsFetcher.TodolistType;
				cmbLISTTYPEID.DataValueField	=	TYPEID;
				cmbLISTTYPEID.DataTextField	=	TYPEDESC;
				cmbLISTTYPEID.DataBind();
				cmbLISTTYPEID.Items.Insert(0,"");

                cmbPRIORITY.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("PRIOR");
                cmbPRIORITY.DataTextField = "LookupDesc";
                cmbPRIORITY.DataValueField = "LookupCode";
                cmbPRIORITY.DataBind();
                cmbPRIORITY.Items.Insert(0, "");

                cmbSYSTEMFOLLOWUPID.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("NOTLIT");
                cmbSYSTEMFOLLOWUPID.DataTextField = "LookupDesc";
                cmbSYSTEMFOLLOWUPID.DataValueField = "LookupCode";
                cmbSYSTEMFOLLOWUPID.DataBind();
                cmbSYSTEMFOLLOWUPID.Items.Insert(0, "");
                
				// fill start time & end time with options
				FillTimeOptions();
				
				//lblFROMUSERNAME.Text		=	cmbTOUSERID.SelectedItem.Text;  
				btnTransfer.Attributes.Add("onclick","javascript:return openWindow();"); 
				btnGoToClaim.Attributes.Add("onClick","javascript:OpenClaim();return false;"); 
				SetPageLabels();
				if(Request.QueryString["CUSTOMER_ID"]!=null && Request.QueryString["CUSTOMER_ID"].ToString()!="")
					hidCUSTOMER_ID.Value = Request.QueryString["CUSTOMER_ID"];
				if(Request.QueryString["LISTID"]!=null)
				{
					GenerateXML(Request.QueryString["LISTID"].ToString());
				}
				else
				{
					hidFROMUSERNAME.Value = GetUserFLName();
					if(strCalledFrom == "InCLT")
					{
						int customer_ID = 

							int.Parse(Request.QueryString["CUSTOMER_ID"]);
						DataSet dsCustomer = 

							Cms.BusinessLayer.BlClient.ClsCustomer.GetCustomerDetails(customer_ID);
						
						txtCUSTOMER_NAME.Text	=	

							(dsCustomer.Tables[0].Rows[0]["CUSTOMER_FIRST_NAME"].ToString() + " " + 

							dsCustomer.Tables[0].Rows[0]["CUSTOMER_MIDDLE_NAME"].ToString() + " " + 

							dsCustomer.Tables[0].Rows[0]["CUSTOMER_LAST_NAME"].ToString()).Trim();
					}
					//else
					//	hidCUSTOMER_ID.Value=GetCustomerID();
				}
			}
			#endregion

			
		}

        
		/// <summary>
		/// fetching data based on query string values
		/// </summary>
		private void GenerateXML(string listID)
		{
			string strListId=listID;
            
			ClsDiary objToDoList=new ClsDiary();   
  
			if(strListId!="" && strListId!=null)
			{
				try
				{
					DataSet ds=new DataSet(); 
					ds=objToDoList.FetchData(int.Parse(strListId)); 
					if (ds.Tables[0].Rows[0]["CLAIM_NUMBER"] != null && 

						ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString().Trim() != "")
					{
						txtCLAIM_NUMBER.Text = 

							ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
						hidCLAIM_ID.Value = 

							ds.Tables[0].Rows[0]["CLAIM_ID"].ToString();
					}
					//Done for Itrack Issue 6658 on 3 Nov 09
					if(hidIS_ACTIVE.Value == "N")
					{
						cmbTOUSERID.Items.FindByValue(intToUserID.ToString()).Attributes.Add("style", "color:red");
					}
                    hidOldData.Value = ds.GetXml();
                    if (ClsCommon.FetchValueFromXML("FOLLOWUPDATE", hidOldData.Value) != "")
                    {
                        string followupdate = ClsCommon.FetchValueFromXML("FOLLOWUPDATE", hidOldData.Value);

                        DateTime followupdatenew = Convert.ToDateTime(followupdate);
                        hidFollow_Up_Date.Value = followupdatenew.ToShortDateString();

                    }
                   
				}
				catch(Exception ex)
				{
					lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + 

						ex.Message + " Try again!";
					lblMessage.Visible	=	true;
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
					hidFormSaved.Value			=	"2";                                   
				}
				finally
				{
					if(objToDoList!= null)
						objToDoList.Dispose();
				}  
                
			}
		}

        

		#region Method code to do form's processing
		
		/// <summary>
		/// Fetch form's value and stores into variables.
		/// </summary>
		private TodolistInfo  GetFormValue()
		{
			TodolistInfo objModelInfo = new TodolistInfo();
			fDate                           =   txtFOLLOWUPDATE.Text	==	""	?	

				DateTime.Now.ToString()	:	txtFOLLOWUPDATE.Text;
			stTime                          =   (cmbSTARTTIMEHOUR.SelectedValue == "-1"? "0" : 

				cmbSTARTTIMEHOUR.SelectedValue)  + ":" + (cmbSTARTTIMEMINUTE.SelectedValue=="-1" ? "0" : 

				cmbSTARTTIMEMINUTE.SelectedValue) + " " + (cmbSTARTTIMEMERIDIAN.SelectedValue=="-1"? "AM" : 

				cmbSTARTTIMEMERIDIAN.SelectedValue) ;
			endTime                         =   (cmbENDTIMEHOUR.SelectedValue =="-1" ? "0" : 

				cmbENDTIMEHOUR.SelectedValue)  + ":" + (cmbENDTIMEMINUTE.SelectedValue=="-1" ? "0" : 

				cmbENDTIMEMINUTE.SelectedValue) + " " + (cmbENDTIMEMERIDIAN.SelectedValue=="-1" ? "AM" : 

				cmbENDTIMEMERIDIAN.SelectedValue);            
			strRowId                        =   hidLISTID.Value;        
			if(hidLISTID.Value!="NEW")
			{
				objModelInfo.LISTID         =   int.Parse(hidLISTID.Value);  
				objModelInfo.FROMUSERID		=	int.Parse(hidFROMUSERID.Value);
			}
			else
				objModelInfo.FROMUSERID         =   int.Parse(GetUserId()); 
			//Done for Itrack Issue 6658 on 3 Nov 09
			//objModelInfo.TOUSERID           =   int.Parse(intToUserID.ToString());
            objModelInfo.TOUSERID           =   int.Parse(cmbTOUSERID.Value.ToString());

			objModelInfo.LISTTYPEID         =   cmbLISTTYPEID.SelectedItem.Value == "" ? -1 : 

				int.Parse(cmbLISTTYPEID.SelectedItem.Value);            
			objModelInfo.PRIORITY           =   cmbPRIORITY.SelectedItem.Value;
			objModelInfo.SUBJECTLINE        =   txtSUBJECTLINE.Text;
			objModelInfo.NOTE               =   txtNOTE.Text;
			objModelInfo.SYSTEMFOLLOWUPID   =   cmbSYSTEMFOLLOWUPID.SelectedItem.Value=="" ? -1 : 

				int.Parse(cmbSYSTEMFOLLOWUPID.SelectedItem.Value);            
			objModelInfo.LISTOPEN           =   hidLISTOPEN.Value; 
			if(hidCUSTOMER_ID.Value!="" && hidCUSTOMER_ID.Value!="0")
			{
				if(hidCustomInfo.Value=="")
					

					hidCustomInfo.Value=ClsDiary.GetCustomerDetails(hidCUSTOMER_ID.Value);

				objModelInfo.CUSTOMER_ID		=	

					int.Parse(hidCUSTOMER_ID.Value);
			}
            objModelInfo.CUSTOMER_NAME = Request["txtCUSTOMER_NAME"].ToString().Trim();
            objModelInfo.APPLICATION_NUMBER = Request["txtAPP_NUMBER"].ToString().Trim();
            if (Request["txtAPP_NUMBER"].ToString().Trim() != "")
			{
				objModelInfo.APP_ID				=	int.Parse(hidAPP_ID.Value);	
				objModelInfo.APP_VERSION_ID		=	int.Parse(hidAPP_VERSION_ID.Value);
			}
			else
			{
				objModelInfo.APP_ID				=	-1;	
				objModelInfo.APP_VERSION_ID		=	-1;
			}


			if(hidQQ_ID.Value!="")
			{
				objModelInfo.QUOTEID 			=	int.Parse(hidQQ_ID.Value);
			}
			if(txtQUOTE_NUMBER.Text.Trim() == "")
			{
				objModelInfo.QUOTEID 			=	-1;
			}
            objModelInfo.POLICY_NUMBER = Request["txtPOLICY_NUMBER"].ToString().Trim();
            if (Request["txtPOLICY_NUMBER"].ToString().Trim() != "")
			{
				objModelInfo.POLICY_ID			=	int.Parse(hidPOLICY_ID.Value);
				objModelInfo.POLICY_VERSION_ID	=	int.Parse(hidPOLICY_VERSION_ID.Value);
			}
			else
			{
				objModelInfo.POLICY_ID			=	0;
				objModelInfo.POLICY_VERSION_ID	=	0;
			}

			strFormSaved			        =	hidFormSaved.Value;

			stTime                          =   fDate + " " + stTime;
			endTime                         =   fDate + " " + endTime;

			objModelInfo.FOLLOWUPDATE       =   Convert.ToDateTime(fDate);

			objModelInfo.STARTTIME            =	Convert.ToDateTime(stTime);
			objModelInfo.ENDTIME              =	Convert.ToDateTime(endTime);    
			objModelInfo.RECDATE = DateTime.Now;

			string starttime="", endtime="";
			starttime = objModelInfo.STARTTIME.ToShortTimeString();
			endtime = objModelInfo.ENDTIME.ToShortTimeString();

			objModelInfo.STARTTIMEONLY = starttime;
			objModelInfo.ENDTIMEONLY = endtime;
			return objModelInfo;			
		}
		#endregion

		#region set page labels
		private void SetPageLabels()
		{
			rfvLISTTYPEID.ErrorMessage                  =   ClsMessages.GetMessage(ScreenId,"205");
			rfvFOLLOWUPDATE.ErrorMessage				=	ClsMessages.GetMessage(ScreenId, "42");
			revFOLLOWUPDATE.ValidationExpression		=	aRegExpDate;
			revFOLLOWUPDATE.ErrorMessage				=	ClsMessages.GetMessage(ScreenId, "22");	
			rfvSUBJECTLINE.ErrorMessage					=	ClsMessages.GetMessage(ScreenId, "43");
			rfvTOUSERID.ErrorMessage					=	"Please select To."; //Added by Charles on 14-Sep-09 for Itrack 6401
			cfvSTARTTIME.ErrorMessage					=	ClsMessages.GetMessage(ScreenId, "44");
			cfvENDTIME.ErrorMessage						=	ClsMessages.GetMessage(ScreenId, "726");
			cfvCheckTime.ErrorMessage					=	ClsMessages.GetMessage(ScreenId, "28");
			//			cfvENDTIMEAMPM.ErrorMessage					=	

			ClsMessages.GetMessage(ScreenId,"721");

			objRescMgr=new ResourceManager("Cms.Cmsweb.Diary.AddDiaryEntry",Assembly.GetExecutingAssembly()); 
            
			capFOLLOWUPDATE.Text=objRescMgr.GetString("txtFOLLOWUPDATE") ;
			capSTARTTIMEHOUR.Text=objRescMgr.GetString("cmbSTARTTIMEHOUR") ;
			capENDTIMEHOUR.Text=objRescMgr.GetString("cmbENDTIMEHOUR") ;
			capSUBJECTLINE.Text=objRescMgr.GetString("txtSUBJECTLINE") ;
			capTOUSERID.Text=objRescMgr.GetString("cmbTOUSERID") ;
			capLISTTYPEID.Text=objRescMgr.GetString("cmbLISTTYPEID") ;
			capPRIORITY.Text=objRescMgr.GetString("cmbPRIORITY") ;
			capNOTIFICATIONLIST.Text=objRescMgr.GetString("cmbSYSTEMFOLLOWUPID") ;
			capNOTE.Text=objRescMgr.GetString("txtNOTE") ;
			capCUSTOMER_NAME.Text=objRescMgr.GetString("txtCUSTOMER_NAME");
			capAPP_NUMBER.Text=objRescMgr.GetString("txtAPP_NUMBER");
			capPOLICY_NUMBER.Text=objRescMgr.GetString("txtPOLICY_NUMBER");
			capCLAIM_NUMBER.Text=objRescMgr.GetString("txtCLAIM_NUMBER");
			////
			capQUOTE_NUMBER.Text=objRescMgr.GetString("txtQUOTE_NUMBER");
            capRecorded.Text = objRescMgr.GetString("capRecorded");
            btnComplete.Text = objRescMgr.GetString("btnComplete");
            btnTransfer.Text = objRescMgr.GetString("btnTransfer");
            btnGoToClaim.Text = objRescMgr.GetString("btnGoToClaim");
            btnPolicy.Text = objRescMgr.GetString("btnPolicy");
            btnReminder.Text = objRescMgr.GetString("btnReminder");
}
		#endregion


		/// <summary>
		/// this function is an event handler for performaing click operation of complete button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnComplete_Click(object sender, System.EventArgs e)
		{
			try
			{
				TodolistInfo objDefModelInfo= new TodolistInfo();				
				int result;
				if(hidCUSTOMER_ID.Value!="" && hidCUSTOMER_ID.Value!="0")
				{
					objDefModelInfo.CUSTOMER_ID=int.Parse(hidCUSTOMER_ID.Value);
					objDefModelInfo.CREATED_BY	= int.Parse(GetUserId());
					objDefModelInfo.LISTID=int.Parse(hidLISTID.Value);
					

					result=objDiary.CompleteDiaryEntry(objDefModelInfo,hidCustomInfo.Value);
				}
				else
				{
					DataSet dsUser=new DataSet();
					string strCustomInfo="";
					dsUser= ClsUser.GetUserName(GetUserId().ToString() );
					if(dsUser!=null && dsUser.Tables.Count>0)
						if(dsUser.Tables[0].Rows.Count>0  )
							strCustomInfo=	cmbLISTTYPEID.SelectedItem.Text + " diary item is completed by " + dsUser.Tables[0].Rows[0][0].ToString() + "."   ;


					

																											  result=objDiary.CompleteDiaryEntry(int.Parse(hidLISTID.Value),strCustomInfo);  
				}
					



				hidOperation.Value      =   "1";
				//calling method of clsDiary method
				//int result=objDiary.CompleteDiaryEntry(int.Parse(hidLISTID.Value));  
				//int result=objDiary.CompleteDiaryEntry(objDefModelInfo);
               
				if(result>0)
				{
					lblMessage.Text			=	ClsMessages.GetMessage(ScreenId,"86");       

             
					hidFormSaved.Value		=	"1";
					hidLISTOPEN.Value       =   "N";
					// hidLISTID.Value				=	strRowId;
					SetCountVariables();
					SetDiaryDates();
				}
				else
				{
					lblMessage.Text			=	ClsMessages.GetMessage(ScreenId,"20");
					hidFormSaved.Value		=	"2";
				}
				lblMessage.Visible = true;
			}
			catch(Exception ex)
			{
				ExceptionManager.Publish(ex);
				lblMessage.Text			=	ClsMessages.GetMessage(ScreenId,"21")
					+ ex.Message + "\n Try again!";
					
				hidFormSaved.Value			=	"2";
				lblMessage.Visible		=	true;
			}
			finally
			{
				if(objDiary != null) 
					objDiary.Dispose();
			}
			GenerateXML(hidLISTID.Value);
		}


		/// <summary>
		/// this function is an event handler for performaing click operation of delete button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				hidOperation.Value      =   "1";
				//calling method of clsDiary method
				TodolistInfo objDefModelInfo= new TodolistInfo();
				objDefModelInfo.CREATED_BY	= int.Parse(GetUserId());
				int result;
				if(hidCUSTOMER_ID.Value!="" && hidCUSTOMER_ID.Value!="0")
				{
					objDefModelInfo.CUSTOMER_ID=int.Parse(hidCUSTOMER_ID.Value);
					objDefModelInfo.LISTID=int.Parse(hidLISTID.Value);
					//Added by Anurag Verma on 27/03/2007 
					

					result=objDiary.DeleteDiaryEntry(objDefModelInfo,hidCustomInfo.Value,cmbLISTTYPEID.SelectedItem.Text);
				}
				else
				{
					DataSet dsUser=new DataSet();
					string strCustomInfo="";
					dsUser= ClsUser.GetUserName(GetUserId().ToString() );
					if(dsUser!=null && dsUser.Tables.Count>0)
						if(dsUser.Tables[0].Rows.Count>0  )
							strCustomInfo=	cmbLISTTYPEID.SelectedItem.Text + " diary item is deleted by " + dsUser.Tables[0].Rows[0][0].ToString() + "."   ;

					

																											result=objDiary.DeleteDiaryEntry(int.Parse(hidLISTID.Value),strCustomInfo);  					

				}
               
				if(result>0)
				{
					lblMessage.Text			=	ClsMessages.GetMessage(ScreenId,"333");      

              
					hidFormSaved.Value		=	"1";
					hidLISTOPEN.Value       =   "Y";                            
					hidLISTID.Value			=	strRowId;
					delStr                  =   "1";
					SetCountVariables();
					SetDiaryDates();    
				}
				else
				{
					lblMessage.Text			=	ClsMessages.GetMessage(ScreenId,"334");
					hidFormSaved.Value		=	"2";
				}
				lblMessage.Visible = true;
			}
			catch(Exception ex)
			{
				ExceptionManager.Publish(ex);
				lblMessage.Text			=	ClsMessages.GetMessage(ScreenId,"334")
					+ ex.Message + "\n Try again!";
					
				hidFormSaved.Value			=	"2";
				lblMessage.Visible		=	true;
			}
			finally
			{
				if(objDiary != null) 
					objDiary.Dispose();
			}
			GenerateXML(hidLISTID.Value);
		}

		/// <summary>
		/// this function is an event handler for performaing click operation of save and reminder button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnReminder_Click(object sender, System.EventArgs e)
		{
			SaveFormValue();
		}

		/// <summary>
		/// this function is an event handler for performaing click operation of save button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			SaveFormValue();		
		}

		/// <summary>
		/// Saves the posted data from form
		/// </summary>
		private void SaveFormValue()
		{
			try
			{
				objRescMgr         = new 

					ResourceManager("Cms.Cmsweb.diary.AddDiaryEntry",Assembly.GetExecutingAssembly());  
                                
				TodolistInfo objDefModelInfo=GetFormValue();				
				int returnResult;                     
				if(strRowId == "NEW")
				{
					hidOperation.Value="1"; 
					objDefModelInfo.CREATED_BY	= int.Parse(GetUserId());
					if(hidCustomInfo.Value!="")
						returnResult = 

							objDiary.Add(objDefModelInfo,hidCustomInfo.Value);
					else
						returnResult = objDiary.Add(objDefModelInfo);
                
					if(returnResult > 0)
					{
							
						lblMessage.Text			=	ClsMessages.GetMessage(ScreenId,"29");
						hidLISTID.Value			=	returnResult.ToString();
						hidFormSaved.Value		=	"1";
						SetCountVariables();
						SetDiaryDates();
					}
					else
					{
						lblMessage.Text			=	ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value			=	"2";
					}
					lblMessage.Visible = true;
				}
				else
				{
					TodolistInfo objOldInfo=new TodolistInfo(); 
					string starttime="", endtime="";
					base.PopulateModelObject(objOldInfo,hidOldData.Value);
                  
					starttime = objOldInfo.STARTTIME.ToShortTimeString();
					endtime = objOldInfo.ENDTIME.ToShortTimeString();

					objOldInfo.STARTTIMEONLY = starttime;
					objOldInfo.ENDTIMEONLY = endtime;


					objDefModelInfo.MODIFIED_BY = int.Parse(GetUserId());
					int ret;
					if(hidCustomInfo.Value!="")
						ret = 

							objDiary.Update(objOldInfo,objDefModelInfo,hidCustomInfo.Value);                           
					else
						ret = objDiary.Update(objOldInfo,objDefModelInfo);           

                
                    
                    
					hidOperation.Value="1";                    
					if(ret>0)
					{
						lblMessage.Text 			=	

							ClsMessages.GetMessage(ScreenId,"31");
						hidLISTID.Value				=	strRowId;
						hidFormSaved.Value			=	"1";
						SetCountVariables();
						SetDiaryDates();
					}
					else
					{
						lblMessage.Text			=	ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value			=	"2";
					}
					lblMessage.Visible = true;
				}

				GenerateXML(hidLISTID.Value);
			}
			catch(Exception ex)
			{
				ExceptionManager.Publish(ex);
				lblMessage.Text			=	ClsMessages.GetMessage(ScreenId,"21")
					+ ex.Message + "\n Try again!";
					
				hidFormSaved.Value			=	"2";
				lblMessage.Visible		=	true;
			}
			finally
			{
				if(objDiary != null) 
					objDiary.Dispose();
			}
		
		}

		private void FillTimeOptions()
		{
			//filling  the Hour drop down list
			// clear it first
			cmbSTARTTIMEHOUR.Items.Clear();
			cmbENDTIMEHOUR.Items.Clear();

			int intLoopCount;
			cmbSTARTTIMEHOUR.Items.Add(new ListItem("Hour","-1"));
			cmbENDTIMEHOUR.Items.Add(new ListItem("Hour","-1"));
			for(intLoopCount = 0; intLoopCount < 13; intLoopCount++)
			{
				if(intLoopCount < 10)
				{
					cmbSTARTTIMEHOUR.Items.Add(new ListItem("0" + 

						intLoopCount.ToString(),"0" + intLoopCount.ToString()));
					cmbENDTIMEHOUR.Items.Add(new ListItem("0" + 

						intLoopCount.ToString(),"0" + intLoopCount.ToString()));
				}
				else
				{
					cmbSTARTTIMEHOUR.Items.Add(new 

						ListItem(intLoopCount.ToString(),intLoopCount.ToString()));
					cmbENDTIMEHOUR.Items.Add(new 

						ListItem(intLoopCount.ToString(),intLoopCount.ToString()));
				}
			}

			//filling  the Minute drop down list
			// clear it first.
			cmbSTARTTIMEMINUTE.Items.Clear();
			cmbENDTIMEMINUTE.Items.Clear();

			cmbSTARTTIMEMINUTE.Items.Add(new ListItem("Minute","-1"));
			cmbENDTIMEMINUTE.Items.Add(new ListItem("Minute","-1"));
			// Minutes made from 0-59 : By Swastika on 13th Apr'06 for GI # 2584
			for(intLoopCount = 0; intLoopCount < 60; intLoopCount++)
			{
				if(intLoopCount < 10)
				{
					cmbSTARTTIMEMINUTE.Items.Add(new ListItem("0" + 

						intLoopCount.ToString(),"0" + intLoopCount.ToString()));
					cmbENDTIMEMINUTE.Items.Add(new ListItem("0" + 

						intLoopCount.ToString(),"0" + intLoopCount.ToString()));
				}
				else
				{
					cmbSTARTTIMEMINUTE.Items.Add(new 

						ListItem(intLoopCount.ToString(),intLoopCount.ToString()));
					cmbENDTIMEMINUTE.Items.Add(new 

						ListItem(intLoopCount.ToString(),intLoopCount.ToString()));
				}
			}

            
			//filling  the Meridian drop down list
			// clear it first
			cmbSTARTTIMEMERIDIAN.Items.Clear();
			cmbENDTIMEMERIDIAN.Items.Clear();

            cmbSTARTTIMEMERIDIAN.Items.Add(new ListItem(Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1383"), "-1"));
            cmbSTARTTIMEMERIDIAN.Items.Add(new ListItem(Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1381"), "AM"));
            cmbSTARTTIMEMERIDIAN.Items.Add(new ListItem(Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1382"), "PM"));

            cmbENDTIMEMERIDIAN.Items.Add(new ListItem(Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1383"), "-1"));
            cmbENDTIMEMERIDIAN.Items.Add(new ListItem(Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1381"), "AM"));
            cmbENDTIMEMERIDIAN.Items.Add(new ListItem(Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1382"), "PM"));
		}

		private void SetCountVariables()
		{
			objDiary		=	new ClsDiary();
			int UserId		=	intToUserID == 0 ? -1 : intToUserID;
			string Customer	=	hidCUSTOMER_ID.Value == "0" ? "" : hidCUSTOMER_ID.Value;
			if(strCalledFrom.ToUpper() != CALLED_FROM_INCLIENT)
				Customer	=	"";
			DataSet lObjDs	=	objDiary.GetCountListType(UserId,Customer);
			foreach(DataRow lRowJoin in lObjDs.Tables[0].Rows)
			{
				if(lRowJoin["listtypeid"].ToString().Equals("1"))
				{
					gStrEEC=lRowJoin["Counting"].ToString();
					listtypeid1=lRowJoin["listtypeid"].ToString();
				}				
				if(lRowJoin["listtypeid"].ToString().Equals("3"))
				{
					gStrQPAC=lRowJoin["Counting"].ToString();
					listtypeid2=lRowJoin["listtypeid"].ToString();
				}
				if(lRowJoin["listtypeid"].ToString().Equals("4"))
				{
					gStrQPBC=lRowJoin["Counting"].ToString();
					listtypeid3=lRowJoin["listtypeid"].ToString();
				}
				if(lRowJoin["listtypeid"].ToString().Equals("2"))
				{
					gStrRRC=lRowJoin["Counting"].ToString();
					listtypeid4=lRowJoin["listtypeid"].ToString();
				}
				if(lRowJoin["listtypeid"].ToString().Equals("7"))
				{
					gStrBRC=lRowJoin["Counting"].ToString();
					listtypeid5=lRowJoin["listtypeid"].ToString();
				}
				if(lRowJoin["listtypeid"].ToString().Equals("8"))
				{
					gStrERC=lRowJoin["Counting"].ToString();
					listtypeid6=lRowJoin["listtypeid"].ToString();
				}
				if(lRowJoin["listtypeid"].ToString().Equals("6"))
				{
					gStrAAC=lRowJoin["Counting"].ToString();
					listtypeid7=lRowJoin["listtypeid"].ToString();
				}
				if(lRowJoin["listtypeid"].ToString().Equals("9"))
				{
					cStrCF=lRowJoin["Counting"].ToString();
					listtypeid8=lRowJoin["listtypeid"].ToString();
				}
				if(lRowJoin["listtypeid"].ToString().Equals("10"))
				{
					cStrANF=lRowJoin["Counting"].ToString();
					listtypeid9=lRowJoin["listtypeid"].ToString();
				}
				if(lRowJoin["listtypeid"].ToString().Equals("11"))
				{
					cStrCRE=lRowJoin["Counting"].ToString();
					listtypeid10=lRowJoin["listtypeid"].ToString();
				}
			}
			objDiary.Dispose();
		}

		private void SetDiaryDates()
		{
			objDiary		=	new ClsDiary();
			int UserId		=	intToUserID == 0 ? -1 : intToUserID;
			string Customer	=	hidCUSTOMER_ID.Value == "0" ? "" : hidCUSTOMER_ID.Value;
			if(strCalledFrom.ToUpper() != CALLED_FROM_INCLIENT)
				Customer	=	"";
			gStrAppDates	=	objDiary.SetDiaryDates(UserId,Customer);
			objDiary.Dispose();
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
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnComplete.Click += new System.EventHandler(this.btnComplete_Click);
			this.btnReminder.Click += new System.EventHandler(this.btnReminder_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnReset_Click(object sender, System.EventArgs e)
		{
		
		}

		//		private void btnGoToClaim_Click(object sender, System.EventArgs e)
		//		{
		//			GoToClaim(hidLISTID.Value);
		//		}
		

		/*private void GoToClaim(string listID)
		{
			string strListId=listID;
			string urlPath = "";
			string tempStr = "";
            
			ClsDiary objToDoList=new ClsDiary();   
  
			if(strListId!="" && strListId!=null)
			{
				try
				{
					DataSet ds=new DataSet(); 
					ds=objToDoList.FetchData(int.Parse(strListId));                  
					DataRow dr = ds.Tables[0].Rows[0];
					
					if (dr["CUSTOMER_ID"] != null && dr["CUSTOMER_ID"].ToString() != "" 

&& dr["CUSTOMER_ID"].ToString().Trim() != "0" 
						&& dr["POLICY_ID"] != null && dr["POLICY_ID"].ToString() != 

"" && dr["POLICY_ID"].ToString().Trim() != "0" 
						&& dr["POLICY_VERSION_ID"] != null && 

dr["POLICY_VERSION_ID"].ToString() != "" && dr["POLICY_VERSION_ID"].ToString().Trim() != "0" 
						&& dr["CLAIM_ID"] != null && dr["CLAIM_ID"].ToString() != "" 

&& dr["CLAIM_ID"].ToString().Trim() != "0" 
						&& dr["LOB_ID"] != null && dr["LOB_ID"].ToString() != "" && 

dr["LOB_ID"].ToString().Trim() != "0" 
						&& dr["LISTID"] != null && dr["LISTID"].ToString() != "" && 

dr["LISTID"].ToString().Trim() != "0") 
					{
						urlPath = "/cms/claims/aspx/ClaimsTab.aspx?&CUSTOMER_ID=" + 

dr["CUSTOMER_ID"].ToString()
							+ "&POLICY_ID=" + dr["POLICY_ID"].ToString()
							+ "&POLICY_VERSION_ID=" + 

dr["POLICY_VERSION_ID"].ToString()
							+ "&CLAIM_ID=" + dr["CLAIM_ID"].ToString()
							+ "&LOB_ID=" + dr["LOB_ID"].ToString()
							+ "&DIARY_ID=" + dr["LISTID"].ToString()
							+ 

"&ADD_NEW=0&HOMEOWNER=0&RECR_VEH=0&IN_MARINE=0&LOSS_DATE=&";	
											
						tempStr = "<script>OpenClaim('" + urlPath + "')</script>";
						RegisterStartupScript("GoToClaimDetail",tempStr);
					}
				}
				catch(Exception ex)
				{
					lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + 

" - " + ex.Message + " Try again!";
					lblMessage.Visible	=	true;
					

Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
					hidFormSaved.Value			=	"2";                 

                  
				}
				finally
				{
					if(objToDoList!= null)
						objToDoList.Dispose();
				}  
                
			}
		}*/
	}
}
