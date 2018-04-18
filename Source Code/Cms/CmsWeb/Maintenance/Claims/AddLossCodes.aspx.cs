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
using Cms.Model.Maintenance.Claims;
using Cms.CmsWeb.Controls; 
using Cms.ExceptionPublisher; 
using Cms.BusinessLayer.BLClaims;




namespace Cms.CmsWeb.Maintenance.Claims
{
	/// <summary>
	/// 
	/// </summary>
	public class AddLossCodes : Cms.CmsWeb.cmsbase  
	{
		#region Page controls declaration
		
		
		#endregion
		#region Local form variables
		//START:*********** Local form variables *************
		//string oldXML;
		private const int LOSS_CODES_ID = 5;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		//private string strRowId, strFormSaved;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.DropDownList cmbLOB_ID;
		protected System.Web.UI.WebControls.Label capUnassignLossCodes;
		protected System.Web.UI.WebControls.Label capAssignedLossCodes;
		protected System.Web.UI.WebControls.ListBox cmbAssignLossCodes;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnAssignLossCodes;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnUnAssignLossCodes;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRESET;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOSS_CODE_TYPE;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOSS_CODE_ID;		
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOB_ID;
		protected System.Web.UI.WebControls.Label capLOB_ID;
		protected System.Web.UI.WebControls.ListBox cmbUnAssignLossCodes;
		private bool LOAD_ALL_LOB = true;
		protected System.Web.UI.HtmlControls.HtmlTableRow trLOB_ID;
		protected System.Web.UI.HtmlControls.HtmlTableRow trLossCodes;
        protected System.Web.UI.WebControls.Label capbtnassign;
        
		
		//private int	intLoggedInUserID;
		
		
		
		

		#endregion
	

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{						
			base.ScreenId="300_0";
			
			lblMessage.Visible = false;			
			

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;
			
			btnSave.CmsButtonClass	=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;			
			
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.Claims.AddLossCodes" ,System.Reflection.Assembly.GetExecutingAssembly());
			

			if(!Page.IsPostBack)
			{	
				//				ShowHideControls(false);	
				cmbLOB_ID.Attributes.Add("onClick","javascript: return cmbLOB_ID_Changed();");
				if(Request.QueryString["LOB_ID"]!=null && Request.QueryString["LOB_ID"].ToString()!="")
				{
					LoadDropDowns(LOAD_ALL_LOB);
					cmbLOB_ID.SelectedValue = Request.QueryString["LOB_ID"].ToString();
					//cmbLOB_ID_SelectedIndexChanged(null,null);
					PopulateAssignedLossCodes();
					cmbLOB_ID.Enabled = false;
				}				
				else
					LoadDropDowns(!(LOAD_ALL_LOB));
				cmbUnAssignLossCodes.Attributes.Add("ondblclick","javascript:AssignLossCodes();");
				cmbAssignLossCodes.Attributes.Add("ondblclick","javascript:UnAssignLossCodes();");				
				btnSave.Attributes.Add("onClick","CountAssignLossCodes();");
				btnReset.Attributes.Add("onclick","javascript:return ResetTheForm();");
				SetCaptions();
				SetErrorMessages();										
				PopulateUnAssignedLossCodes();
			}
			else if(hidRESET.Value=="1")
			{
				if(Request.QueryString["LOB_ID"]!=null && Request.QueryString["LOB_ID"].ToString()!="")
				{
					cmbLOB_ID.SelectedValue = Request.QueryString["LOB_ID"].ToString();
					//cmbLOB_ID_SelectedIndexChanged(null,null);
					PopulateAssignedLossCodes();
				}
				hidRESET.Value="0";
			}			
		}
		#endregion

		#region GetOldDataXML
		private void GetOldDataXML()
		{
			//			if(hidLOSS_CODE_ID.Value!="" && hidLOSS_CODE_ID.Value!="0")
			//				hidOldData.Value	=	ClsExpertServiceProviders.GetExpertServiceProviders(int.Parse(hidLOSS_CODE_ID.Value));
			//			else
			//				hidOldData.Value	=	"";
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
			//			revEXPERT_SERVICE_ZIP.ValidationExpression				=		  aRegExpZip;
			//			revEXPERT_SERVICE_ZIP.ErrorMessage						=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("24");
			//			revEXPERT_SERVICE_CONTACT_EMAIL.ValidationExpression	=		  aRegExpEmail;
			//			revEXPERT_SERVICE_CONTACT_EMAIL.ErrorMessage			=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("23");
			//			rfvEXPERT_SERVICE_NAME.ErrorMessage						=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("34");			
			//			rfvEXPERT_SERVICE_ZIP.ErrorMessage						=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("37");			
			//			revEXPERT_SERVICE_CONTACT_PHONE.ValidationExpression	=		  aRegExpPhone;
			//			revEXPERT_SERVICE_CONTACT_PHONE.ErrorMessage			=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");						
			//			rfvEXPERT_SERVICE_ADDRESS1.ErrorMessage					=		  Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"2");
			//			rfvEXPERT_SERVICE_STATE.ErrorMessage					=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("35");
			//			rfvEXPERT_SERVICE_VENDOR_CODE.ErrorMessage				=		  Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"3");
			//			rfvEXPERT_SERVICE_TYPE.ErrorMessage						=		  Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"1");
		}

		#endregion

		

		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	
				//For retreiving the return value of business class save function
				ClsLossCodes objLossCodes = new ClsLossCodes();				

				//Retreiving the form values into model class object
				ClsLossCodesInfo objLossCodesInfo = GetFormValue();

				objLossCodesInfo.CREATED_BY = int.Parse(GetUserId());
				objLossCodesInfo.CREATED_DATETIME = DateTime.Now;
				objLossCodesInfo.IS_ACTIVE="Y"; 
					
				//Calling the add method of business layer class
				intRetVal = objLossCodes.Add(objLossCodesInfo,hidLOSS_CODE_TYPE.Value);				

				if(intRetVal>0)
				{
					lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
					hidFormSaved.Value			=	"1";
					hidIS_ACTIVE.Value = "Y";
					PopulateAssignedLossCodes();
					PopulateUnAssignedLossCodes();
					cmbLOB_ID.Enabled = false;
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
			capLOB_ID.Text=		objResourceMgr.GetString("cmbLOB_ID");
            
            capbtnassign.Text = objResourceMgr.GetString("capbtnassign");
            capAssignedLossCodes.Text = objResourceMgr.GetString("capAssignedLossCodes");
            capUnassignLossCodes.Text = objResourceMgr.GetString("capUnassignLossCodes");
			
		}
	

		#region GetFormValue
		private ClsLossCodesInfo GetFormValue()
		{
			ClsLossCodesInfo objLossCodesInfo = new ClsLossCodesInfo();			
			if(cmbLOB_ID.SelectedItem!=null && cmbLOB_ID.SelectedItem.Value!="")
				objLossCodesInfo.LOB_ID	=	int.Parse(cmbLOB_ID.SelectedItem.Value);
			
			return objLossCodesInfo;
		}
		#endregion

		#region LoadDropDowns
		private void LoadDropDowns(bool LOAD_ALL_LOB_FLAG)
		{
			DataTable dtLOB;
			if(LOAD_ALL_LOB_FLAG)
			{
				Cms.BusinessLayer.BlCommon.ClsStates objState = new Cms.BusinessLayer.BlCommon.ClsStates();
				dtLOB = objState.PoplateLob().Tables[0];
			}
			else
				dtLOB = ClsLossCodes.GetRemainingLOBForLossCodes();

			if(dtLOB!=null && dtLOB.Rows.Count>0)
			{
				cmbLOB_ID.DataSource =	dtLOB;
				cmbLOB_ID.DataValueField = "LOB_ID";
				cmbLOB_ID.DataTextField = "LOB_DESC";
				cmbLOB_ID.DataBind();
				cmbLOB_ID.Items.Insert(0,"");
				cmbLOB_ID.SelectedIndex=0;
			}		
			else
			{
				trLossCodes.Attributes.Add("style","display:none");
				trLOB_ID.Attributes.Add("style","display:none");
				btnReset.Visible = false;
				lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("749");
				lblMessage.Visible = true;
				//Set the value of hidFormSaved as in the case of delete to prevent a pop-up message asking 
				//for save to come. When no control is being displayed on the page, there is no use of showing
				//save message pop-up either.
				hidFormSaved.Value = "5";
			}
			
		}
		#endregion

		#region Populate UnAssigned Loss Codes
		private void PopulateUnAssignedLossCodes()
		{
			//Populate Loss Codes
			//DataTable dtLossCodes = ClsDefaultValues.GetDefaultValuesDetails(LOSS_CODES_ID);
			string LOB_ID;
			//When add new is clicked, then lets pass 0 for LOB_ID and hence display all the records
			if(Request.QueryString["LOB_ID"]!=null && Request.QueryString["LOB_ID"].ToString()!="")
				LOB_ID = Request.QueryString["LOB_ID"].ToString();
			else
				LOB_ID = "0";
			DataTable dtLossCodes = ClsLossCodes.GetRemainingLossCodes(LOB_ID,LOSS_CODES_ID);
			if(dtLossCodes!=null)
			{
				cmbUnAssignLossCodes.DataSource	=	dtLossCodes;
				cmbUnAssignLossCodes.DataTextField=	"DETAIL_TYPE_DESCRIPTION";
				cmbUnAssignLossCodes.DataValueField=	"DETAIL_TYPE_ID";
				cmbUnAssignLossCodes.DataBind();
			}
		}
		#endregion

		#region Populate Assigned Loss Codes
		private void PopulateAssignedLossCodes()
		{
			//Populate Loss Codes
			cmbAssignLossCodes.Items.Clear();
			DataTable dtLossCodes = ClsLossCodes.GetLossCodes(int.Parse(cmbLOB_ID.SelectedItem.Value),int.Parse(GetLanguageID()));
			if(dtLossCodes!=null && dtLossCodes.Rows.Count>0)
			{
				cmbAssignLossCodes.DataSource	=	dtLossCodes;
				cmbAssignLossCodes.DataTextField=	"DESCRIPTION";
				cmbAssignLossCodes.DataValueField=	"LOSS_CODE_TYPE";
				cmbAssignLossCodes.DataBind();
				btnSave.Attributes.Add("style","display:inline");
				//btnReset.Attributes.Add("style","display:inline");
			}
			else
			{
				btnSave.Attributes.Add("style","display:none");
				//btnReset.Attributes.Add("style","display:none");
			}

		}
		#endregion

		//		private void cmbLOB_ID_SelectedIndexChanged(object sender, System.EventArgs e)
		//		{
		//			if(cmbLOB_ID.SelectedItem!=null && cmbLOB_ID.SelectedItem.Value!="")
		//			{
		//				PopulateAssignedLossCodes();
		////				ShowHideControls(true);
		//			}
		////			else
		////				ShowHideControls(false);
		//		}

		//		private void ShowHideControls(bool flag)
		//		{
		////			cmbUnAssignLossCodes.Visible = flag;
		////			cmbAssignLossCodes.Visible	= 	flag;
		////			capUnassignLossCodes.Visible = flag;
		////			capAssignedLossCodes.Visible = flag;
		////			btnAssignLossCodes.Visible =flag;
		////			btnUnAssignLossCodes.Visible = flag;
		////			btnSave.Visible = flag;
		////			//btnReset.Visible = flag;
		//		}
	}
}
