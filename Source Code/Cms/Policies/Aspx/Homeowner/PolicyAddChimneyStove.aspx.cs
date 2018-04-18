/******************************************************************************************
<Author					: -		Vijay Arora
<Start Date				: -		18-11-2005
<End Date				: -	
<Description			: -		Add / Update for Policy Home Owner Chimney Stove
<Review Date			: - 
<Reviewed By			: - 	
*******************************************************************************************/ 

using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Cms.CmsWeb;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer.BlApplication.HomeOwners;
using Cms.Model.Application.HomeOwners;
using Cms.Model.Policy.HomeOwners;
using System.Collections;

namespace Cms.Policies.aspx.HomeOwners
{
	/// <summary>
	/// Summary description for AddChimneyStove.
	/// </summary>
	public class PolicyAddChimneyStove : Cms.Policies.policiesbase
	{
		#region Page controls declaration
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capIS_STOVE_VENTED;
		protected System.Web.UI.WebControls.DropDownList cmbIS_STOVE_VENTED;
		protected System.Web.UI.WebControls.Label capOTHER_DEVICES_ATTACHED;
		protected System.Web.UI.WebControls.TextBox txtOTHER_DEVICES_ATTACHED;
		protected System.Web.UI.WebControls.Label capCHIMNEY_CONSTRUCTION;
		protected System.Web.UI.WebControls.DropDownList cmbCHIMNEY_CONSTRUCTION;
		protected System.Web.UI.WebControls.Label capCONSTRUCT_OTHER_DESC;
		protected System.Web.UI.WebControls.TextBox txtCONSTRUCT_OTHER_DESC;
		protected System.Web.UI.WebControls.Label capIS_TILE_FLUE_LINING;
		protected System.Web.UI.WebControls.DropDownList cmbIS_TILE_FLUE_LINING;
		protected System.Web.UI.WebControls.Label capIS_CHIMNEY_GROUND_UP;
		protected System.Web.UI.WebControls.DropDownList cmbIS_CHIMNEY_GROUND_UP;
		protected System.Web.UI.WebControls.Label capCHIMNEY_INST_AFTER_HOUSE_BLT;
		protected System.Web.UI.WebControls.DropDownList cmbCHIMNEY_INST_AFTER_HOUSE_BLT;
		protected System.Web.UI.WebControls.Label capIS_CHIMNEY_COVERED;
		protected System.Web.UI.WebControls.DropDownList cmbIS_CHIMNEY_COVERED;
		protected System.Web.UI.WebControls.Label capDIST_FROM_SMOKE_PIPE;
		protected System.Web.UI.WebControls.TextBox txtDIST_FROM_SMOKE_PIPE;
		protected System.Web.UI.WebControls.Label capTHIMBLE_OR_MATERIAL;
		protected System.Web.UI.WebControls.TextBox txtTHIMBLE_OR_MATERIAL;
		protected System.Web.UI.WebControls.Label capSTOVE_PIPE_IS;
		protected System.Web.UI.WebControls.DropDownList cmbSTOVE_PIPE_IS;
		protected System.Web.UI.WebControls.Label capDOES_SMOKE_PIPE_FIT;
		protected System.Web.UI.WebControls.DropDownList cmbDOES_SMOKE_PIPE_FIT;
		protected System.Web.UI.WebControls.Label capSMOKE_PIPE_WASTE_HEAT;
		protected System.Web.UI.WebControls.DropDownList cmbSMOKE_PIPE_WASTE_HEAT;
		protected System.Web.UI.WebControls.Label capSTOVE_CONN_SECURE;
		protected System.Web.UI.WebControls.DropDownList cmbSTOVE_CONN_SECURE;
		protected System.Web.UI.WebControls.Label capSMOKE_PIPE_PASS;
		protected System.Web.UI.WebControls.DropDownList cmbSMOKE_PIPE_PASS;
		protected System.Web.UI.WebControls.Label capSELECT_PASS;
		protected System.Web.UI.WebControls.DropDownList cmbSELECT_PASS;
		protected System.Web.UI.WebControls.Label capPASS_INCHES;
		protected System.Web.UI.WebControls.TextBox txtPASS_INCHES;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFUEL_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDIST_FROM_SMOKE_PIPE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revPASS_INCHES;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		#endregion
	
		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		// private int	intLoggedInUserID;
		//Defining the business layer class object
		ClsChimneyStove  objChimneyStove ;
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
			this.revDIST_FROM_SMOKE_PIPE.ValidationExpression	=	aRegExpDoublePositiveNonZero;
			this.revDIST_FROM_SMOKE_PIPE.ErrorMessage			=	ClsMessages.GetMessage(ScreenId, "1");

			this.revPASS_INCHES.ValidationExpression			=	aRegExpDoublePositiveNonZero;
			this.revPASS_INCHES.ErrorMessage					=	ClsMessages.GetMessage(ScreenId, "2");
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
			switch (strCalledFrom.ToUpper()) 
			{
				case "HOME":
					base.ScreenId="244_2";
					break;
				case "RENTAL":
					base.ScreenId="163_2";
					break;
				default:
					base.ScreenId="729";
					break;
			}
			#endregion

			lblMessage.Visible = false;
			SetErrorMessages();

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;

			btnSave.CmsButtonClass	=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.aspx.Homeowners.PolicyAddChimneyStove" ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{
				GetSessionValues();
				GetOldDataXML();

				FillControls();
				SetCaptions();
				btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
				
				//Setting the workflow
				SetWorkflow();
			}
		
		}//end pageload
		#endregion


		private void FillControls()
		{
			try
			{
				IList lstYesNo =  Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
				cmbIS_STOVE_VENTED.DataSource =lstYesNo;
				cmbIS_STOVE_VENTED.DataTextField="LookupDesc"; 
				cmbIS_STOVE_VENTED.DataValueField="LookupCode";
				cmbIS_STOVE_VENTED.DataBind();
				cmbIS_STOVE_VENTED.Items.Insert(0,"");

				cmbIS_TILE_FLUE_LINING.DataSource =lstYesNo;
				cmbIS_TILE_FLUE_LINING.DataTextField="LookupDesc"; 
				cmbIS_TILE_FLUE_LINING.DataValueField="LookupCode";
				cmbIS_TILE_FLUE_LINING.DataBind();
				cmbIS_TILE_FLUE_LINING.Items.Insert(0,"");
					
				cmbIS_CHIMNEY_GROUND_UP.DataSource =lstYesNo;
				cmbIS_CHIMNEY_GROUND_UP.DataTextField="LookupDesc"; 
				cmbIS_CHIMNEY_GROUND_UP.DataValueField="LookupCode";
				cmbIS_CHIMNEY_GROUND_UP.DataBind();
				cmbIS_CHIMNEY_GROUND_UP.Items.Insert(0,"");
					
				cmbCHIMNEY_INST_AFTER_HOUSE_BLT.DataSource =lstYesNo;
				cmbCHIMNEY_INST_AFTER_HOUSE_BLT.DataTextField="LookupDesc"; 
				cmbCHIMNEY_INST_AFTER_HOUSE_BLT.DataValueField="LookupCode";
				cmbCHIMNEY_INST_AFTER_HOUSE_BLT.DataBind();
				cmbCHIMNEY_INST_AFTER_HOUSE_BLT.Items.Insert(0,"");
					
				cmbIS_CHIMNEY_COVERED.DataSource =lstYesNo;
				cmbIS_CHIMNEY_COVERED.DataTextField="LookupDesc"; 
				cmbIS_CHIMNEY_COVERED.DataValueField="LookupCode";
				cmbIS_CHIMNEY_COVERED.DataBind();
				cmbIS_CHIMNEY_COVERED.Items.Insert(0,"");
					
				cmbDOES_SMOKE_PIPE_FIT.DataSource =lstYesNo;
				cmbDOES_SMOKE_PIPE_FIT.DataTextField="LookupDesc"; 
				cmbDOES_SMOKE_PIPE_FIT.DataValueField="LookupCode";
				cmbDOES_SMOKE_PIPE_FIT.DataBind();
				cmbDOES_SMOKE_PIPE_FIT.Items.Insert(0,"");
					
				cmbSMOKE_PIPE_WASTE_HEAT.DataSource =lstYesNo;
				cmbSMOKE_PIPE_WASTE_HEAT.DataTextField="LookupDesc"; 
				cmbSMOKE_PIPE_WASTE_HEAT.DataValueField="LookupCode";
				cmbSMOKE_PIPE_WASTE_HEAT.DataBind();
				cmbSMOKE_PIPE_WASTE_HEAT.Items.Insert(0,"");

				cmbSTOVE_CONN_SECURE.DataSource =lstYesNo;
				cmbSTOVE_CONN_SECURE.DataTextField="LookupDesc"; 
				cmbSTOVE_CONN_SECURE.DataValueField="LookupCode";
				cmbSTOVE_CONN_SECURE.DataBind();
				cmbSTOVE_CONN_SECURE.Items.Insert(0,"");
					
				cmbSMOKE_PIPE_PASS.DataSource =lstYesNo;
				cmbSMOKE_PIPE_PASS.DataTextField="LookupDesc"; 
				cmbSMOKE_PIPE_PASS.DataValueField="LookupCode";
				cmbSMOKE_PIPE_PASS.DataBind();
				cmbSMOKE_PIPE_PASS.Items.Insert(0,"");
				
				cmbCHIMNEY_CONSTRUCTION.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CHCON");
				cmbCHIMNEY_CONSTRUCTION.DataTextField="LookupDesc";
				cmbCHIMNEY_CONSTRUCTION.DataValueField="LookupID";
				cmbCHIMNEY_CONSTRUCTION.DataBind(); 
				cmbCHIMNEY_CONSTRUCTION.Items.Insert(0,"");

				cmbSTOVE_PIPE_IS.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("STPIP");
				cmbSTOVE_PIPE_IS.DataTextField="LookupDesc";
				cmbSTOVE_PIPE_IS.DataValueField="LookupID";
				cmbSTOVE_PIPE_IS.DataBind(); 
				cmbSTOVE_PIPE_IS.Items.Insert(0,"");

				cmbSELECT_PASS.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("SELO");
				cmbSELECT_PASS.DataTextField="LookupDesc";
				cmbSELECT_PASS.DataValueField="LookupID";
				cmbSELECT_PASS.DataBind(); 
				cmbSELECT_PASS.Items.Insert(0,"");

			}
			catch//(Exception exc)
			{}
			finally
			{}
		
		}

		#region Validation Check
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
		#endregion

		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private Cms.Model.Policy.Homeowners.ClsPolicyChimneyStoveInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			Cms.Model.Policy.Homeowners.ClsPolicyChimneyStoveInfo objChimneyStoveInfo = new Cms.Model.Policy.Homeowners.ClsPolicyChimneyStoveInfo();
			
			objChimneyStoveInfo.CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);
			objChimneyStoveInfo.POLICY_ID = int.Parse(hidPOLICY_ID.Value);
			objChimneyStoveInfo.POLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value);
			if(hidFUEL_ID.Value.ToUpper()!="NEW")
				objChimneyStoveInfo.FUEL_ID = int.Parse(hidFUEL_ID.Value);


			if(cmbIS_STOVE_VENTED.SelectedItem != null)
			{
				objChimneyStoveInfo.IS_STOVE_VENTED=	cmbIS_STOVE_VENTED.SelectedItem.Value;
			}

			objChimneyStoveInfo.OTHER_DEVICES_ATTACHED=	txtOTHER_DEVICES_ATTACHED.Text;
			
			if(cmbCHIMNEY_CONSTRUCTION.SelectedItem != null)
			{
				objChimneyStoveInfo.CHIMNEY_CONSTRUCTION=	cmbCHIMNEY_CONSTRUCTION.SelectedValue;
			}
			objChimneyStoveInfo.CONSTRUCT_OTHER_DESC=	txtCONSTRUCT_OTHER_DESC.Text;

			if(cmbIS_TILE_FLUE_LINING.SelectedItem != null)
			{
				objChimneyStoveInfo.IS_TILE_FLUE_LINING=	cmbIS_TILE_FLUE_LINING.SelectedValue;
			}
			
			if(cmbIS_CHIMNEY_GROUND_UP.SelectedItem != null)
			{
				objChimneyStoveInfo.IS_CHIMNEY_GROUND_UP=	cmbIS_CHIMNEY_GROUND_UP.SelectedValue;
			}
			
			if(cmbCHIMNEY_INST_AFTER_HOUSE_BLT.SelectedItem != null)
			{
				objChimneyStoveInfo.CHIMNEY_INST_AFTER_HOUSE_BLT=	cmbCHIMNEY_INST_AFTER_HOUSE_BLT.SelectedValue;
			}
			if(cmbIS_CHIMNEY_COVERED.SelectedItem != null)
			{
				objChimneyStoveInfo.IS_CHIMNEY_COVERED=	cmbIS_CHIMNEY_COVERED.SelectedValue;
			}
			if(txtDIST_FROM_SMOKE_PIPE.Text.Trim() != "")
			{
				objChimneyStoveInfo.DIST_FROM_SMOKE_PIPE=	Convert.ToInt32(txtDIST_FROM_SMOKE_PIPE.Text);
			}
			
			objChimneyStoveInfo.THIMBLE_OR_MATERIAL=	txtTHIMBLE_OR_MATERIAL.Text;
			if(cmbSTOVE_PIPE_IS.SelectedItem != null)
			{
				objChimneyStoveInfo.STOVE_PIPE_IS=	cmbSTOVE_PIPE_IS.SelectedValue;
			}
			if(cmbDOES_SMOKE_PIPE_FIT.SelectedItem != null)
			{
				objChimneyStoveInfo.DOES_SMOKE_PIPE_FIT=	cmbDOES_SMOKE_PIPE_FIT.SelectedValue;
			}
			if(cmbSMOKE_PIPE_WASTE_HEAT.SelectedItem != null)
			{
				objChimneyStoveInfo.SMOKE_PIPE_WASTE_HEAT=	cmbSMOKE_PIPE_WASTE_HEAT.SelectedValue;
			}
			if(cmbSTOVE_CONN_SECURE.SelectedItem != null)
			{
				objChimneyStoveInfo.STOVE_CONN_SECURE=	cmbSTOVE_CONN_SECURE.SelectedValue;
			}
			if(cmbSMOKE_PIPE_PASS.SelectedItem != null)
			{
				objChimneyStoveInfo.SMOKE_PIPE_PASS=	cmbSMOKE_PIPE_PASS.SelectedValue;
			}
			if(cmbSELECT_PASS.SelectedItem != null)
			{
				objChimneyStoveInfo.SELECT_PASS=	cmbSELECT_PASS.SelectedValue;
			}

			if(txtPASS_INCHES.Text.Trim()!="")
			{
				objChimneyStoveInfo.PASS_INCHES=	Convert.ToDouble(txtPASS_INCHES.Text.Trim());//Changed from Convert.ToInt32 by Charles on 21-Oct-09 for Itrack 6599
			}

			//These  assignments are common to all pages.

			strFormSaved	=	hidFormSaved.Value;
			if (hidOldData.Value != "")
			{
				strRowId=hidFUEL_ID.Value;					
			}
			else
			{
				strRowId="NEW";	
				
			}

			oldXML		= hidOldData.Value;
			return objChimneyStoveInfo;
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
				objChimneyStove = new  ClsChimneyStove();
				objChimneyStove.LoggedinUserId = int.Parse(GetUserId());

				//Retreiving the form values into model class object
				Cms.Model.Policy.Homeowners.ClsPolicyChimneyStoveInfo objChimneyStoveInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					//Calling the add method of business layer class
					objChimneyStoveInfo.IS_ACTIVE="Y";
					intRetVal = objChimneyStove.AddPolicyChimneyStove(objChimneyStoveInfo);

					if(intRetVal>0)
					{
						hidFUEL_ID.Value = objChimneyStoveInfo.FUEL_ID.ToString();
						lblMessage.Text		=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value	=	"1";
						GetOldDataXML();
						hidIS_ACTIVE.Value	=	"Y";
						
						//Setting the workflow
						SetWorkflow();

						//Showing the endorsement popup window
						base.OpenEndorsementDetails();

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
					Cms.Model.Policy.Homeowners.ClsPolicyChimneyStoveInfo objOldChimneyStoveInfo = new Cms.Model.Policy.Homeowners.ClsPolicyChimneyStoveInfo();
					
					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldChimneyStoveInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objChimneyStoveInfo.FUEL_ID = int.Parse(strRowId);
					
					//Updating the record using business layer class object
					
					intRetVal	= objChimneyStove.UpdatePolicyChimneyStove(objOldChimneyStoveInfo,objChimneyStoveInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						GetOldDataXML();

						//Setting the workflow
						SetWorkflow();

						//Showing the endorsement popup window
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
			}
			finally
			{
				if(objChimneyStove!= null)
					objChimneyStove.Dispose();
			}
		}
		#endregion

		#region Set Captions
		private void SetCaptions()
		{
			capIS_STOVE_VENTED.Text						=		objResourceMgr.GetString("cmbIS_STOVE_VENTED");
			capOTHER_DEVICES_ATTACHED.Text						=		objResourceMgr.GetString("txtOTHER_DEVICES_ATTACHED");
			capCHIMNEY_CONSTRUCTION.Text						=		objResourceMgr.GetString("cmbCHIMNEY_CONSTRUCTION");
			capCONSTRUCT_OTHER_DESC.Text						=		objResourceMgr.GetString("txtCONSTRUCT_OTHER_DESC");
			capIS_TILE_FLUE_LINING.Text						=		objResourceMgr.GetString("cmbIS_TILE_FLUE_LINING");
			capIS_CHIMNEY_GROUND_UP.Text						=		objResourceMgr.GetString("cmbIS_CHIMNEY_GROUND_UP");
			capCHIMNEY_INST_AFTER_HOUSE_BLT.Text						=		objResourceMgr.GetString("cmbCHIMNEY_INST_AFTER_HOUSE_BLT");
			capIS_CHIMNEY_COVERED.Text						=		objResourceMgr.GetString("cmbIS_CHIMNEY_COVERED");
			capDIST_FROM_SMOKE_PIPE.Text						=		objResourceMgr.GetString("txtDIST_FROM_SMOKE_PIPE");
			capTHIMBLE_OR_MATERIAL.Text						=		objResourceMgr.GetString("txtTHIMBLE_OR_MATERIAL");
			capSTOVE_PIPE_IS.Text						=		objResourceMgr.GetString("cmbSTOVE_PIPE_IS");
			capDOES_SMOKE_PIPE_FIT.Text						=		objResourceMgr.GetString("cmbDOES_SMOKE_PIPE_FIT");
			capSMOKE_PIPE_WASTE_HEAT.Text						=		objResourceMgr.GetString("cmbSMOKE_PIPE_WASTE_HEAT");
			capSTOVE_CONN_SECURE.Text						=		objResourceMgr.GetString("cmbSTOVE_CONN_SECURE");
			capSMOKE_PIPE_PASS.Text						=		objResourceMgr.GetString("cmbSMOKE_PIPE_PASS");
			capSELECT_PASS.Text						=		objResourceMgr.GetString("cmbSELECT_PASS");
			capPASS_INCHES.Text						=		objResourceMgr.GetString("txtPASS_INCHES");
		}
		#endregion

		#region Get Old Data
		private void GetOldDataXML()
		{
			if ( hidFUEL_ID.Value != "" ) 
			{
				hidOldData.Value=ClsChimneyStove.GetPolicyChimneyInformationXml(
					int.Parse(this.hidCUSTOMER_ID.Value),
					int.Parse(this.hidPOLICY_ID.Value),
					int.Parse(this.hidPOLICY_VERSION_ID.Value),
					int.Parse(this.hidFUEL_ID.Value));
			}
		}
		#endregion

		#region Set Session Values
		private void GetSessionValues()
		{
			this.hidCUSTOMER_ID.Value		=	Request.QueryString["CUSTOMER_ID"];
			this.hidAPP_VERSION_ID.Value	=	Request.QueryString["APP_VERSION_ID"];
			this.hidAPP_ID.Value			=	Request.QueryString["APP_ID"];
			this.hidFUEL_ID.Value			=	Request.QueryString["FUEL_ID"];
			hidPOLICY_ID.Value				=   GetPolicyID();
			hidPOLICY_VERSION_ID.Value		=	GetPolicyVersionID();  
		}
		#endregion

		#region Setworkflow
		/// <summary>
		/// Sets the workflow properties
		/// </summary>
		private void SetWorkflow()
		{
			if(base.ScreenId	==	"244_2" || base.ScreenId == "163_2" || base.ScreenId == "729")
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID", hidCUSTOMER_ID.Value);
				myWorkFlow.AddKeyValue("POLICY_ID", hidPOLICY_ID.Value);
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID", hidPOLICY_VERSION_ID.Value);
				if ( hidFUEL_ID.Value != "" ) 
				{
					myWorkFlow.AddKeyValue("FUEL_ID", hidFUEL_ID.Value);
				}
				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();

			}
			else
			{
				myWorkFlow.Display	=	false;
			}
		}
		#endregion


		
	
	}
}
