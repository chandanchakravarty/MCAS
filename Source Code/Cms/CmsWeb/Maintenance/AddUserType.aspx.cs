/******************************************************************************************
<Author				: -   Gaurav Tyagi
<Start Date				: -	5/10/2005 11:11:20 AM
<End Date				: -	
<Description				: - 	This file is used to
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 26/08/2005
<Modified By			: - Anurag Verma
<Purpose				: - Applying Null Check for buttons on aspx page
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
using Cms.Model.Maintenance;
using Cms.CmsWeb.Controls;
namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// This Class is used to
	/// </summary>
	public class clsAddUserType : Cms.CmsWeb.cmsbase
	{
		#region Page controls declaration
		protected System.Web.UI.WebControls.TextBox txtUSER_TYPE_CODE;
		protected System.Web.UI.WebControls.TextBox txtUSER_TYPE_DESC;
        protected System.Web.UI.WebControls.Label capMessages;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidActive;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDeActive;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidactivate;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDeactivate;

		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvUSER_TYPE_CODE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvUSER_TYPE_DESC;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTAB_TITLES;

		protected System.Web.UI.WebControls.Label lblMessage;

		#endregion
		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		string strPageMode="";
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		//private int	intLoggedInUserID;
		protected System.Web.UI.WebControls.Label capUSER_TYPE_CODE;
		protected System.Web.UI.WebControls.Label capUSER_TYPE_DESC;
		protected System.Web.UI.WebControls.Label capStatus;
		protected System.Web.UI.WebControls.Label lblStatus;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidUSER_TYPE_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidUSER_TYPE_SYSTEM;
		protected System.Web.UI.HtmlControls.HtmlTableRow trSysGenCode;
		//Defining the business layer class object
		ClsUserType  objUserType ;
		//END:*********** Local variables *************

		#endregion
		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			rfvUSER_TYPE_CODE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"70");
				rfvUSER_TYPE_DESC.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"71");
				}
		#endregion
		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
			// phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
			base.ScreenId="21_0";
			lblMessage.Visible = false;
			//Set the focus on the first control on the form
			SetFocus("txtUSER_TYPE_CODE");
			SetErrorMessages();
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
            hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId,"TabCon");
            hidactivate.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1314");
            hidDeactivate.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1315");
            btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1333");
			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.clsAddUserType",System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{

				GetOldDataXML();
				SetCaptions();

                string strSysID = GetSystemId();
                if (strSysID == "ALBAUAT")
                    strSysID = "ALBA";
                if (ClsCommon.IsXMLResourceExists(Request.PhysicalApplicationPath + "/CmsWeb/support/PageXml/" + strSysID, "AddUserType.xml"))
                    setPageControls(Page, Request.PhysicalApplicationPath + "/CmsWeb/support/PageXml/" + strSysID + "/AddUserType.xml");  

				#region "Loading singleton"
				#endregion//Loading singleton

				//Setting xml for the page to be displayed in page controls
				if(Request.QueryString["USER_TYPE_ID"]!=null && Request.QueryString["USER_TYPE_ID"].ToString().Length>0)
				{
					SetXml(Request.QueryString["USER_TYPE_ID"]);
					strPageMode = "Edit";
				}
				else if(hidUSER_TYPE_ID.Value != null && hidUSER_TYPE_ID.Value.ToString().Length > 0)
				{
					SetXml(hidUSER_TYPE_ID.Value.ToString());
					strPageMode = "Edit";
				}
				else
					strPageMode = "Add";
			}
			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass					=	CmsButtonType.Write;
			btnReset.PermissionString				=	gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass	=	CmsButtonType.Write;
			btnActivateDeactivate.PermissionString	=	gstrSecurityXML;

			btnSave.CmsButtonClass					=	CmsButtonType.Write;
			btnSave.PermissionString				=	gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
            hidActive.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1323");
            hidDeActive.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1322");
		}//end pageload
		#endregion

		private void SetXml(string strUserTypeId)
		{
			hidOldData.Value = ClsUserType.GetXmlForPageControls(strUserTypeId);
			if(hidOldData.Value != "")
			{
				System.Xml.XmlDocument objXMLDoc = new System.Xml.XmlDocument();
				objXMLDoc.LoadXml(hidOldData.Value);
				string strSysGenCode = ClsCommon.GetNodeValue(objXMLDoc,"//SYSTEM_GENERATED_CODE");
				// Initialise Security Seetings on basis of SYSTEM GENERATED CODES
				// If the code is system generated : DO NOT Display the buttons
				if(strSysGenCode.Equals("1"))
				{
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2008");
					lblMessage.Visible = true;
					txtUSER_TYPE_CODE.ReadOnly=true;
					txtUSER_TYPE_DESC.ReadOnly = true;
					gstrSecurityXML = "<Security><Read>N</Read><Write>N</Write><Delete>N</Delete><Execute>N</Execute></Security>";
				}
			}
			else
			{
				lblMessage.Visible = false;
				gstrSecurityXML = "<Security><Read>Y</Read><Write>Y</Write><Delete>Y</Delete><Execute>Y</Execute></Security>";
			}
		}

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
			catch (Exception ex)
			{
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				return false;
			}
		}
		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private Cms.Model.Maintenance.ClsUserTypeInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsUserTypeInfo objUserTypeInfo;
			objUserTypeInfo = new ClsUserTypeInfo();

			objUserTypeInfo.USER_TYPE_CODE=	txtUSER_TYPE_CODE.Text;
			objUserTypeInfo.USER_TYPE_DESC=	txtUSER_TYPE_DESC.Text;
			//objUserTypeInfo.IS_ACTIVE ="Y";
			objUserTypeInfo.IS_ACTIVE =hidIS_ACTIVE.Value;
			objUserTypeInfo.USER_TYPE_SYSTEM ="N";
			objUserTypeInfo.USER_TYPE_FOR_CARRIER = 1;

			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidUSER_TYPE_ID.Value;
			oldXML		= hidOldData.Value;
			//Returning the model object

			return objUserTypeInfo;
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
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

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
			try
			{
				int intRetVal;	//For retreiving the return value of business class save function
				objUserType = new  ClsUserType();

				//Retreiving the form values into model class object
				ClsUserTypeInfo objUserTypeInfo = GetFormValue();
				objUserTypeInfo.IS_ACTIVE = "Y";
				

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objUserTypeInfo.CREATED_BY = int.Parse(GetUserId());
					objUserTypeInfo.CREATED_DATETIME = DateTime.Now;
					

					//Calling the add method of business layer class
					intRetVal = objUserType.Add(objUserTypeInfo);

					if(intRetVal>0)
					{
						hidUSER_TYPE_ID.Value = objUserTypeInfo.USER_TYPE_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						btnActivateDeactivate.Enabled=false;
						hidIS_ACTIVE.Value = "Y";
						hidOldData.Value = ClsUserType.GetXmlForPageControls(hidUSER_TYPE_ID.Value);
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
				} // end save case
				else //UPDATE CASE
				{

					//Creating the Model object for holding the Old data
					ClsUserTypeInfo objOldUserTypeInfo;
					objOldUserTypeInfo = new ClsUserTypeInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldUserTypeInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objUserTypeInfo.USER_TYPE_ID = int.Parse(strRowId);
					objUserTypeInfo.MODIFIED_BY = int.Parse(GetUserId());
					objUserTypeInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					objUserTypeInfo.IS_ACTIVE = hidIS_ACTIVE.Value;

					//Updating the record using business layer class object
					intRetVal	= objUserType.Update(objOldUserTypeInfo,objUserTypeInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"18");
						hidFormSaved.Value		=	"1";
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value		=	"1";
					}
					lblMessage.Visible = true;
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
				if(objUserType!= null)
					objUserType.Dispose();
			}
		}

		/// <summary>
		/// Activates and deactivates  .
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
//		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
//		{
//				try
//				{
//					Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo ();
//					objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
//					objStuTransactionInfo.loggedInUserName = GetUserName();
//					ClsUserType objUserType=new ClsUserType();
//					if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
//					{
//						objStuTransactionInfo.transactionDescription = "UserType is Deactivated Succesfully.";
//						objUserType.TransactionInfoParams = objStuTransactionInfo;
//						objUserType.ActivateDeactivate(hidUSER_TYPE_ID.Value,"N");
//						lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
//						hidIS_ACTIVE.Value="N";
//					}
//					else
//					{
//						objStuTransactionInfo.transactionDescription = "UserType is Activated Succesfully.";
//						objUserType.TransactionInfoParams = objStuTransactionInfo;
//						objUserType.ActivateDeactivate(hidUSER_TYPE_ID.Value,"Y");
//						lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
//						hidIS_ACTIVE.Value="Y";
//					}
//					hidFormSaved.Value			=	"1";
//					SetXml(hidUSER_TYPE_ID.Value);
//			}
//			catch(Exception ex)
//			{
//				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21")+ " - " + ex.Message + " Try again!";
//				lblMessage.Visible	=	true;
//				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
//			}
//			finally
//			{
//				lblMessage.Visible = true;
//				if(objUserType!= null)
//					objUserType.Dispose();
//			}
//		}
		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			ClsUserType objUserType=new ClsUserType();
			ClsUserTypeInfo objUserTypeInfo = GetFormValue();
			int ModifiedBy = int.Parse(GetUserId());
			string CustomerInfo="";
			CustomerInfo = "User Code:" + objUserTypeInfo.USER_TYPE_CODE +"<br>"+
						   "User Description:" + objUserTypeInfo.USER_TYPE_DESC;
			int returnResult = 1;
			try
			{
				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					
					returnResult = objUserType.ActivateDeactivateUserType(int.Parse(hidUSER_TYPE_ID.Value),"N",CustomerInfo,ModifiedBy);
					if(returnResult>0)
					{
						lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
						hidIS_ACTIVE.Value="N";
						hidFormSaved.Value			=	"1";
					}
					else if(returnResult == -1)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"938");
						hidFormSaved.Value			=		"2";
					}					
					else
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value			=	"2";
					}
				}
				else
				{
					
					returnResult = objUserType.ActivateDeactivateUserType(int.Parse(hidUSER_TYPE_ID.Value),"Y",CustomerInfo,ModifiedBy);
					if(returnResult>0)
					{
						lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
						hidIS_ACTIVE.Value="Y";
						hidFormSaved.Value			=	"1";
					}
					else if(returnResult == -1)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"938");
						hidFormSaved.Value			=		"2";
					}					
					else
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value			=	"2";
					}
				}
				SetXml(hidUSER_TYPE_ID.Value.ToString());
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
				if(objUserType!= null)
					objUserType.Dispose();
			}
		}
		#endregion
		private void SetCaptions()
		{
			capUSER_TYPE_CODE.Text						=		objResourceMgr.GetString("txtUSER_TYPE_CODE");
			capUSER_TYPE_DESC.Text						=		objResourceMgr.GetString("txtUSER_TYPE_DESC");
			capStatus.Text								=		objResourceMgr.GetString("lblStatus");
		}
		private void GetOldDataXML()
		{
			if ( Request.Params.Count != 0 ) 
			{
			}
			else 
			{
			}
		}
	}
}
