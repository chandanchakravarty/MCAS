/******************************************************************************************
<Author					: -  Anurag Verma
<Start Date				: -	 Nov 18, 2005
<End Date				: -	
<Description			: - Add/Edit page for POL_HOME_OWNER_FIRE_PROT_CLEAN	
<Review Date			: - 
<Reviewed By			: - 	
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
using System.Resources;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb; 
using Cms.CmsWeb.Controls;    
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Policy.Homeowners; 

namespace Cms.Policies.aspx.HomeOwners
{
	/// <summary>
	/// Summary description for PolicyAddFireProt.
	/// </summary>
	public class PolicyAddFireProt : Cms.Policies.policiesbase
	{
		#region Page controls declaration
		protected System.Web.UI.WebControls.DropDownList cmbIS_SMOKE_DETECTOR;
		protected System.Web.UI.WebControls.DropDownList cmbIS_PROTECTIVE_MAT_FLOOR;
		protected System.Web.UI.WebControls.DropDownList cmbIS_PROTECTIVE_MAT_WALLS;
		protected System.Web.UI.WebControls.TextBox txtPROT_MAT_SPACED;
		protected System.Web.UI.WebControls.TextBox txtSTOVE_SMOKE_PIPE_CLEANED;
		protected System.Web.UI.WebControls.TextBox txtSTOVE_CLEANER;
		protected System.Web.UI.WebControls.TextBox txtREMARKS;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hid;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;

		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;


		protected System.Web.UI.WebControls.Label lblMessage;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		#endregion
		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		// private int	intLoggedInUserID;
		protected System.Web.UI.WebControls.Label capIS_SMOKE_DETECTOR;
		protected System.Web.UI.WebControls.Label capIS_PROTECTIVE_MAT_FLOOR;
		protected System.Web.UI.WebControls.Label capIS_PROTECTIVE_MAT_WALLS;
		protected System.Web.UI.WebControls.Label capPROT_MAT_SPACED;
		protected System.Web.UI.WebControls.Label capSTOVE_SMOKE_PIPE_CLEANED;
		protected System.Web.UI.WebControls.Label capSTOVE_CLEANER;
		protected System.Web.UI.WebControls.Label capREMARKS;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFUEL_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOL_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOL_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		//Defining the business layer class object
		Cms.BusinessLayer.BlApplication.ClsFireProt  objFireProt ;
		protected System.Web.UI.WebControls.CustomValidator csvREMARKS;
		protected System.Web.UI.WebControls.CustomValidator csvPROT_MAT_SPACED;
		string strCalledFrom="";
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
			csvPROT_MAT_SPACED.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("455");
			csvREMARKS.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("455");
		}
		#endregion
		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			#region Setting ScreenId
			// Check from where the screen is called.
			if (Request.QueryString["CalledFrom"] != null || Request.QueryString["CalledFrom"] !="")
			{
				strCalledFrom=Request.QueryString["CalledFrom"].ToString();
			}
		
			//Setting screen Id.	
			switch (strCalledFrom) 
			{
				case "Home":
				case "HOME":
					base.ScreenId="244_1";
					break;
				case "Rental":
				case "RENTAL":
					base.ScreenId="163_1";
					break;
				default:
					base.ScreenId="729";
					break;
			}
			#endregion

			btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");

			// phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
			
			lblMessage.Visible = false;
			SetErrorMessages();

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	CmsButtonType.Write;
			btnReset.PermissionString=	gstrSecurityXML;

			btnSave.CmsButtonClass	=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.aspx.HomeOwners.PolicyAddFireProt" ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{
				GetQueryString();
				GetOldDataXML();
				SetCaptions();
				#region "Loading singleton"
				#endregion//Loading singleton

				#region Set Workflow Control
				SetWorkflow();
				#endregion
			}
		}//end pageload
		#endregion
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
			catch// (Exception ex)
			{
				return false;
			}
		}
		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsPolicyFireProtInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsPolicyFireProtInfo  objFireProtInfo;
			objFireProtInfo = new ClsPolicyFireProtInfo ();

			objFireProtInfo.CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);
			objFireProtInfo.POLICY_ID = int.Parse(hidPOL_ID.Value);
			objFireProtInfo.POLICY_VERSION_ID = int.Parse(hidPOL_VERSION_ID.Value);
			objFireProtInfo.FUEL_ID = int.Parse(hidFUEL_ID.Value);
			objFireProtInfo.IS_SMOKE_DETECTOR  =	cmbIS_SMOKE_DETECTOR.SelectedValue;
			objFireProtInfo.IS_PROTECTIVE_MAT_FLOOR=	cmbIS_PROTECTIVE_MAT_FLOOR.SelectedValue;
			objFireProtInfo.IS_PROTECTIVE_MAT_WALLS=	cmbIS_PROTECTIVE_MAT_WALLS.SelectedValue;
			objFireProtInfo.PROT_MAT_SPACED=	txtPROT_MAT_SPACED.Text;
			objFireProtInfo.STOVE_SMOKE_PIPE_CLEANED=	txtSTOVE_SMOKE_PIPE_CLEANED.Text;
			objFireProtInfo.STOVE_CLEANER=	txtSTOVE_CLEANER.Text;
			objFireProtInfo.REMARKS=	txtREMARKS.Text;

			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			if (hidOldData.Value == "")
			{
				strRowId = "New";
			}
			else
			{
				strRowId		=	hidFUEL_ID.Value;
			}
			oldXML		= hidOldData.Value;
			//Returning the model object

			return objFireProtInfo;
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
				objFireProt = new  Cms.BusinessLayer.BlApplication.ClsFireProt();

				//Retreiving the form values into model class object
				ClsPolicyFireProtInfo objFireProtInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objFireProtInfo.CREATED_BY = int.Parse(GetUserId());
					//objFireProtInfo.CREATED_DATETIME = DateTime.Now;

					//Calling the add method of business layer class
					intRetVal = objFireProt.Add(objFireProtInfo);

					if(intRetVal>0)
					{
						hidFUEL_ID.Value = objFireProtInfo.FUEL_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						//hidIS_ACTIVE.Value = "Y";
						GetOldDataXML();
						//Open Endorsement Screen
						base.OpenEndorsementDetails();
						SetWorkflow();
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
					ClsPolicyFireProtInfo objOldFireProtInfo;
					objOldFireProtInfo = new ClsPolicyFireProtInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldFireProtInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objFireProtInfo.FUEL_ID = Convert.ToInt32(strRowId);
					objFireProtInfo.MODIFIED_BY = int.Parse(GetUserId());
					objFireProtInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					//objFireProtInfo.IS_ACTIVE = hidIS_ACTIVE.Value;

					//Updating the record using business layer class object
					intRetVal	= objFireProt.Update(objOldFireProtInfo,objFireProtInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						SetWorkflow();
						//Open Endorsement Screen
						base.OpenEndorsementDetails();
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
				ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objFireProt!= null)
					objFireProt.Dispose();
			}
		}

		
		#endregion
		private void SetCaptions()
		{
			capIS_SMOKE_DETECTOR.Text					=		objResourceMgr.GetString("cmbIS_SMOKE_DETECTOR");
			capIS_PROTECTIVE_MAT_FLOOR.Text				=		objResourceMgr.GetString("cmbIS_PROTECTIVE_MAT_FLOOR");
			capIS_PROTECTIVE_MAT_WALLS.Text				=		objResourceMgr.GetString("cmbIS_PROTECTIVE_MAT_WALLS");
			capPROT_MAT_SPACED.Text						=		objResourceMgr.GetString("txtPROT_MAT_SPACED");
			capSTOVE_SMOKE_PIPE_CLEANED.Text			=		objResourceMgr.GetString("txtSTOVE_SMOKE_PIPE_CLEANED");
			capSTOVE_CLEANER.Text						=		objResourceMgr.GetString("txtSTOVE_CLEANER");
			capREMARKS.Text						        =		objResourceMgr.GetString("txtREMARKS");
		}
		private void GetOldDataXML()
		{
			if ( hidFUEL_ID.Value != "" ) 
			{
				hidOldData.Value = Cms.BusinessLayer.BlApplication.ClsFireProt.GetPolicyFireProtXml(
					int.Parse(hidCUSTOMER_ID.Value)
					, int.Parse(hidPOL_ID.Value)
					, int.Parse(hidPOL_VERSION_ID.Value)
					,int.Parse(hidFUEL_ID.Value));
			}
		}
		private void GetQueryString()
		{
			hidCUSTOMER_ID.Value = Request.Params["CUSTOMER_ID"];
			hidPOL_ID.Value = Request.Params["POL_ID"];
			hidPOL_VERSION_ID.Value = Request.Params["POL_VERSION_ID"];
			hidFUEL_ID.Value = Request.Params["FUELID"];

		}

		private void SetWorkflow()
		{
			if(base.ScreenId	==	"244_1" || base.ScreenId == "163_1")
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",hidCUSTOMER_ID.Value);
				myWorkFlow.AddKeyValue("POLICY_ID",hidPOL_ID.Value);
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",hidPOL_VERSION_ID.Value);
				//myWorkFlow.AddKeyValue("APP_ID",hidPOL_ID.Value);
				//myWorkFlow.AddKeyValue("APP_VERSION_ID",hidPOL_VERSION_ID.Value);
				if ( hidFUEL_ID.Value != "" )
				{
					myWorkFlow.AddKeyValue("FUEL_ID",hidFUEL_ID.Value);
				}
				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
			}
			else
			{
				myWorkFlow.Display	=	false;
			}
		}
	}
}
