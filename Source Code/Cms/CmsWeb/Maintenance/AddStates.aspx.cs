/******************************************************************************************
	<Author					: - > Sumit Chhabra
	<Start Date				: -	> April 20,2006
	<End Date				: - >
	<Description			: - > Page is used to assign limits to authority
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History


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
using Cms.CmsWeb;
using System.Resources; 
using System.Reflection; 
using Cms.Model.Maintenance;
using Cms.CmsWeb.Controls; 
using Cms.ExceptionPublisher; 
using Cms.BusinessLayer.BlCommon;




namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// 
	/// </summary>
	public class AddStates : Cms.CmsWeb.cmsbase  
	{
		string strRowId="";
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.WebControls.Label capCOUNTRY_ID;
		protected System.Web.UI.WebControls.Label capSTATE_DESC;
		protected System.Web.UI.WebControls.TextBox txtSTATE_DESC;
		protected System.Web.UI.WebControls.Label capSTATE_CODE;
		protected System.Web.UI.WebControls.TextBox txtSTATE_CODE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOUNTRY_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSTATE_CODE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revSTATE_CODE;
		protected System.Web.UI.WebControls.Label capSTATE_NAME;
		protected System.Web.UI.WebControls.TextBox txtSTATE_NAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSTATE_NAME;
		protected System.Web.UI.WebControls.RegularExpressionValidator revSTATE_NAME;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCOUNTRY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE_ID;
		protected System.Web.UI.WebControls.DropDownList cmbCOUNTRY_ID;
        protected System.Web.UI.WebControls.Label capMessages;

		#region Page controls declaration
		
		
		#endregion
		#region Local form variables
		
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		
		
		
		
		

		#endregion
	

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{						
			base.ScreenId="403_0";
			
			lblMessage.Visible = false;
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;
			
			btnSave.CmsButtonClass	=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass	=	CmsButtonType.Write;
			btnActivateDeactivate.PermissionString		=	gstrSecurityXML;
			
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddStates" ,System.Reflection.Assembly.GetExecutingAssembly());
			
			if(!Page.IsPostBack)
			{			
				GetQueryStringValues();
				LoadDropDowns();
				btnReset.Attributes.Add("onclick","javascript:return ResetTheForm();");
				SetCaptions();
				SetErrorMessages();
                // Added by Amit Mishra for Screen Customization on 26rd sep 2011
                if (ClsCommon.IsXMLResourceExists(Request.PhysicalApplicationPath + "cmsweb/support/PageXML/" + GetSystemId(), "AddStates.xml"))
                    setPageControls(Page, Request.PhysicalApplicationPath + "cmsweb/support/PageXml/" + GetSystemId() + "/AddStates.xml");
				GetOldDataXML();
			}
		}
		#endregion

		#region GetOldDataXML
		private void GetOldDataXML()
		{
			ClsStates objState = new ClsStates();
			if(hidSTATE_ID.Value!="" && hidSTATE_ID.Value!="0")
				hidOldData.Value = objState.GetOldData(int.Parse(hidSTATE_ID.Value));
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
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

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
			rfvCOUNTRY_ID.ErrorMessage						=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("33");
			rfvSTATE_NAME.ErrorMessage						=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("950");
			rfvSTATE_CODE.ErrorMessage						=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("949");
			revSTATE_NAME.ValidationExpression				=		  aRegExpAlpha;			
			revSTATE_NAME.ErrorMessage						=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("475");
			revSTATE_CODE.ValidationExpression				=		  aRegExpAlpha;
			revSTATE_CODE.ErrorMessage						=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("475");
			
		}

		#endregion

		private void GetQueryStringValues()
		{
			if(Request.QueryString["COUNTRY_ID"]!=null && Request.QueryString["COUNTRY_ID"].ToString()!="")
				hidCOUNTRY_ID.Value = Request.QueryString["COUNTRY_ID"].ToString();

			if(Request.QueryString["STATE_ID"]!=null && Request.QueryString["STATE_ID"].ToString()!="")
				hidSTATE_ID.Value = Request.QueryString["STATE_ID"].ToString();
			else
				hidSTATE_ID.Value = "0";
		}

		

		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	
				//For retreiving the return value of business class save function
				ClsStates objStates = new ClsStates();	
				

				//Retreiving the form values into model class object
				ClsStateInfo objStateInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objStateInfo.CREATED_BY = int.Parse(GetUserId());
					objStateInfo.CREATED_DATETIME = DateTime.Now;
					objStateInfo.IS_ACTIVE="Y"; 
					
					//Calling the add method of business layer class
					intRetVal = objStates.AddState(objStateInfo);

					if(intRetVal>0)
					{
						hidSTATE_ID.Value = objStateInfo.STATE_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidIS_ACTIVE.Value = "Y";
						GetOldDataXML();
					}
					else if(intRetVal == -2) //Duplicate Vendor Code
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"951");
						hidFormSaved.Value			=		"2";
					}
					else if(intRetVal == -1) //Duplicate Authority Limit
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"18");
						hidFormSaved.Value			=		"2";
					}
					else
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value			=	"2";
					}					
				} // end save case
				else //UPDATE CASE
				{
					//Creating the Model object for holding the Old data
					ClsStateInfo objOldStateInfo = new ClsStateInfo();					

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldStateInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page					
					objStateInfo.MODIFIED_BY = int.Parse(GetUserId());
					objStateInfo.LAST_UPDATED_DATETIME = DateTime.Now;                    

					//Updating the record using business layer class object
					intRetVal	= objStates.UpdateState(objOldStateInfo,objStateInfo);					
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						GetOldDataXML();
					}
					else if(intRetVal == -2)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"951");
						hidFormSaved.Value		=	"2";
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value		=	"1";
					}					
				}
				lblMessage.Visible = true;
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
			    
			}
			finally
			{
				//				if(objAgency!= null)
				//					objAgency.Dispose();
			}
		}
		private void SetCaptions()
		{
			capSTATE_CODE.Text				=		objResourceMgr.GetString("txtSTATE_CODE");
			capSTATE_DESC.Text				=		objResourceMgr.GetString("txtSTATE_DESC");
			capSTATE_NAME.Text				=		objResourceMgr.GetString("txtSTATE_NAME");
			capCOUNTRY_ID.Text				=		objResourceMgr.GetString("cmbCOUNTRY_ID");
		}
	

		#region GetFormValue
		private ClsStateInfo GetFormValue()
		{
			ClsStateInfo objStateInfo = new ClsStateInfo();
			if(cmbCOUNTRY_ID.SelectedItem!=null && cmbCOUNTRY_ID.SelectedItem.Value!="")
				objStateInfo.COUNTRY_ID = int.Parse(cmbCOUNTRY_ID.SelectedItem.Value);
			objStateInfo.STATE_NAME = txtSTATE_NAME.Text.Trim();
			objStateInfo.STATE_DESC = txtSTATE_DESC.Text.Trim();
			objStateInfo.STATE_CODE = txtSTATE_CODE.Text.Trim();
			if(hidSTATE_ID.Value=="" || hidSTATE_ID.Value=="0")
			{
				objStateInfo.STATE_ID = 0;
				strRowId = "NEW";
			}
			else
			{
				objStateInfo.STATE_ID = int.Parse(hidSTATE_ID.Value);
				strRowId = hidSTATE_ID.Value;
			}
			return objStateInfo;
		}
		#endregion

		#region LoadDropDowns
		private void LoadDropDowns()
		{
			DataTable dt = Cms.CmsWeb.ClsFetcher.AllCountry;
			cmbCOUNTRY_ID.DataSource			= dt;
			cmbCOUNTRY_ID.DataTextField		= COUNTRY_NAME;
			cmbCOUNTRY_ID.DataValueField		= COUNTRY_ID;
			cmbCOUNTRY_ID.DataBind();
			
		}
		#endregion

		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			try
			{
//				Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo ();
//				objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
//				objStuTransactionInfo.loggedInUserName = GetUserName();
//				ClsExpertServiceProviders objBL = new ClsExpertServiceProviders();				
//				string strRetVal = "";
//
//				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
//				{
//					objStuTransactionInfo.transactionDescription = "Deactivated Succesfully.";
//					objBL.TransactionInfoParams = objStuTransactionInfo;
//					strRetVal = objBL.ActivateDeactivate(hidEXPERT_SERVICE_ID.Value,"N");
//					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
//					hidIS_ACTIVE.Value="N";
//				}
//				else
//				{
//					objStuTransactionInfo.transactionDescription = "Activated Succesfully.";
//					objBL.TransactionInfoParams = objStuTransactionInfo;
//					objBL.ActivateDeactivate(hidEXPERT_SERVICE_ID.Value,"Y");
//					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
//					hidIS_ACTIVE.Value="Y";
//				}
//				
//				if (strRetVal == "-1")
//				{
//					/*Profit Center is assigned*/
//					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"514");
//				}
//
//				lblMessage.Visible = true;
//				hidFormSaved.Value			=	"0";
//				Page.RegisterStartupScript("RefreshGRid","<script>RefreshWebGrid(1,1,true)</script>");
//				GetOldDataXML();
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21")+ " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
		
		}
		
	}
}
