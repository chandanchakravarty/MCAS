/******************************************************************************************
<Author					: -   Vijay Joshi
<Start Date				: -	6/29/2005 12:01:08 PM
<End Date				: -	
<Description			: - 	Code behind class file for recon group file.
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - Class file for reconciliation file.
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
using Cms.CmsWeb;
using Cms.Model.Account;
using Cms.BusinessLayer.BlAccount;
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher.ExceptionManagement;

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for AddReconGroup.
	/// </summary>
	public class AddReconGroup : Cms.Account.AccountBase
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capRECON_ENTITY_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvRECON_ENTITY_ID;
		protected System.Web.UI.WebControls.Label capRECON_ENTITY_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbRECON_ENTITY_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvRECON_ENTITY_TYPE;
		protected System.Web.UI.WebControls.Label capCREATED_DATETIME;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidGROUP_ID;
	
		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		//private int	intLoggedInUserID;
		protected System.Web.UI.WebControls.Label lblCREATED_DATETIME;
		protected System.Web.UI.HtmlControls.HtmlImage imgENTITY_ID;
		//Defining the business layer class object
		ClsReconGroup  objReconGroup ;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidENTITY_ID;
		protected System.Web.UI.WebControls.TextBox txtRECON_ENTITY_ID;
		protected Cms.CmsWeb.Controls.CmsButton btnCommit;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.HtmlControls.HtmlGenericControl trBody;
		//END:*********** Local variables *************
		public string URL;
		#endregion

		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			rfvRECON_ENTITY_ID.ErrorMessage		= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			rfvRECON_ENTITY_TYPE.ErrorMessage	= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
		}
		#endregion

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			URL = ClsCommon.GetLookupURL();
			imgENTITY_ID.Attributes.Add("onclick","javascript:OpenNewLookup();");		
			btnReset.Attributes.Add("onclick","javascript:return formReset();");
			btnCommit.Attributes.Add("onclick","javascript:return confirmCommit();");   
			// phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
			base.ScreenId="191_0";
			lblMessage.Visible = false;
			SetErrorMessages();

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass		=	CmsButtonType.Execute;
			btnReset.PermissionString	=	gstrSecurityXML;

			btnSave.CmsButtonClass		=	CmsButtonType.Execute;
			btnSave.PermissionString	=	gstrSecurityXML;

			btnCommit.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnCommit.PermissionString	=   gstrSecurityXML;

			btnDelete.CmsButtonClass	=   CmsButtonType.Delete;
			btnDelete.PermissionString	=	gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Account.Aspx.AddReconGroup" ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{
				GetQueryString();
				GetOldDataXML();
				SetCaptions();
				lblCREATED_DATETIME.Text = DateTime.Now.ToString("MM/dd/yyyy");
			}			
		}//end pageload
		#endregion

		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsReconGroupInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsReconGroupInfo objReconGroupInfo;
			objReconGroupInfo = new ClsReconGroupInfo();
			if (hidENTITY_ID.Value != "")
				objReconGroupInfo.RECON_ENTITY_ID = int.Parse(hidENTITY_ID.Value);

			if(cmbRECON_ENTITY_TYPE.SelectedValue == ReconEntityType.CUST.ToString() )
				objReconGroupInfo.RECON_ENTITY_TYPE = ReconEntityType.CUST.ToString();

			else if( cmbRECON_ENTITY_TYPE.SelectedValue == ReconEntityType.AGN .ToString() )
				objReconGroupInfo.RECON_ENTITY_TYPE = ReconEntityType.AGN .ToString();

			else if(cmbRECON_ENTITY_TYPE.SelectedValue == ReconEntityType.VEN.ToString())
				objReconGroupInfo.RECON_ENTITY_TYPE = ReconEntityType.VEN.ToString();

			if (lblCREATED_DATETIME.Text != "")
				objReconGroupInfo.CREATED_DATETIME = ConvertToDate(lblCREATED_DATETIME.Text);

			

			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidGROUP_ID.Value;
			oldXML		= hidOldData.Value;
			//Returning the model object

			return objReconGroupInfo;
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
			this.btnCommit.Click += new System.EventHandler(this.btnCommit_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
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
				objReconGroup = new  ClsReconGroup();

				//Retreiving the form values into model class object
				ClsReconGroupInfo objReconGroupInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objReconGroupInfo.CREATED_BY = int.Parse(GetUserId());
					objReconGroupInfo.CREATED_DATETIME = DateTime.Now;

					//Calling the add method of business layer class
					intRetVal = objReconGroup.Add(objReconGroupInfo);

					if(intRetVal>0)
					{
						hidGROUP_ID.Value = objReconGroupInfo.GROUP_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value		=	"1";
						GetOldDataXML();
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text			=		ClsMessages.GetMessage(base.ScreenId,"18");
						hidFormSaved.Value		=		"2";
					}
					else
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value		=	"2";
					}
					lblMessage.Visible = true;
				} // end save case
				else //UPDATE CASE
				{

					//Creating the Model object for holding the Old data
					ClsReconGroupInfo objOldReconGroupInfo;
					objOldReconGroupInfo = new ClsReconGroupInfo();

					string EntityName = ClsCommon.FetchValueFromXML("RECON_ENTITY_ID",hidOldData.Value.ToString());//itrack 4946

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldReconGroupInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objReconGroupInfo.GROUP_ID = int.Parse(strRowId);
					objReconGroupInfo.MODIFIED_BY = int.Parse(GetUserId());
					objReconGroupInfo.LAST_UPDATED_DATETIME = DateTime.Now;

					//Updating the record using business layer class object
					intRetVal	= objReconGroup.Update(objOldReconGroupInfo,objReconGroupInfo,EntityName);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						GetOldDataXML();
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"18");
						hidFormSaved.Value		=	"2";
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value		=	"2";
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
				ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objReconGroup!= null)
					objReconGroup.Dispose();
			}
		}
		#endregion

		private void SetCaptions()
		{
			capRECON_ENTITY_ID.Text		= objResourceMgr.GetString("txtRECON_ENTITY_ID");
			capRECON_ENTITY_TYPE.Text	= objResourceMgr.GetString("cmbRECON_ENTITY_TYPE");
			capCREATED_DATETIME.Text	= objResourceMgr.GetString("lblCREATED_DATETIME");
		}
		
		/// <summary>
		/// Get query string from url into hidden controls
		/// </summary>
		private void GetQueryString()
		{			
			if(Request.QueryString["GROUP_ID"] != null  && Request.QueryString["GROUP_ID"].ToString() != "" )
				hidGROUP_ID.Value = Request.QueryString["GROUP_ID"];

			if(hidGROUP_ID.Value == null || hidGROUP_ID.Value == "" || hidGROUP_ID.Value == "0")
			{
				if(Request.Params["GROUP_ID"] != null && Request.Params["GROUP_ID"] != ""  && Request.Params["GROUP_ID"] != "0" )
					hidGROUP_ID.Value = Request.Params["GROUP_ID"];		
				else if(Request.Params["GROUP_ID_NEW"] != null && Request.Params["GROUP_ID_NEW"] != "" )
					hidGROUP_ID.Value = Request.Params["GROUP_ID_NEW"];		
				else
					hidGROUP_ID.Value = Request.Params["GRP_ID"];		
			}
			
		}

		/// <summary>
		/// Retreive the information about selected record in the form of XML
		/// and saves it into hidden control
		/// </summary>
		private void GetOldDataXML()
		{
			if (hidGROUP_ID.Value != "")
			{
				hidOldData.Value = ClsReconGroup.GetReconGroupInfo(int.Parse(hidGROUP_ID.Value));			
				if(hidOldData.Value !="")
				{
					btnCommit.Visible = true;
					btnDelete.Visible = true;
				}
			}
		}

		private void btnCommit_Click(object sender, System.EventArgs e)
		{
			//GET GROUP ID AND ENTITY ID
			int group_ID = 0;
			int entity_ID = 0;
			string entity_Type = "";
			int intRetVal;	//For retreiving the return value of business class save function

			string EntityName = ClsCommon.FetchValueFromXML("RECON_ENTITY_ID",hidOldData.Value.ToString());//itrack 4946
			group_ID = int.Parse(ClsCommon.FetchValueFromXML("GROUP_ID",hidOldData.Value.ToString()));
			entity_Type =ClsCommon.FetchValueFromXML("RECON_ENTITY_TYPE",hidOldData.Value.ToString());
			entity_ID = int.Parse(hidENTITY_ID.Value.ToString());
			int UserID = Convert.ToInt32(GetUserId()); 

			try
			{
				objReconGroup = new  ClsReconGroup();
				//Calling the COMMIT method of business layer class
				intRetVal = objReconGroup.CommitReconGroup(entity_ID,group_ID,entity_Type,UserID,EntityName);
				if(intRetVal == -1)
				{
					lblMessage.Text = "No records exists against this group."; //TO BE CHANGED
				}
				else
				{
					hidFormSaved.Value		=	"5";
					hidOldData.Value = "";
					lblMessage.Text = "Reconciliation Group committed successfully."; //TO be CHANGED
					trBody.Attributes.Add("style","display:none");
				}
				
				lblMessage.Visible=true;
				
			}
			catch(Exception ex)
			{
				//to be changed : 
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
				ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objReconGroup!= null)
					objReconGroup.Dispose();
			}

	}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			int intRetVal;
			objReconGroup = new  ClsReconGroup();
			string EntityType =ClsCommon.FetchValueFromXML("RECON_ENTITY_TYPE",hidOldData.Value.ToString());
			string EntityName = ClsCommon.FetchValueFromXML("RECON_ENTITY_ID",hidOldData.Value.ToString());
			intRetVal = objReconGroup.DeleteReconGroup(int.Parse(hidGROUP_ID.Value),EntityType,int.Parse(GetUserId()),EntityName);
			
						
			if(intRetVal>0)
			{
				lblMessage.Text			= Cms.CmsWeb.ClsMessages.GetMessage("G","127");
				hidFormSaved.Value = "5";
				hidOldData.Value = "";
				trBody.Attributes.Add("style","display:none");
			}
			else
			{
			
				lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"128");
				hidFormSaved.Value		=	"2";
			}
			lblMessage.Visible = true;
		}
	}
}
