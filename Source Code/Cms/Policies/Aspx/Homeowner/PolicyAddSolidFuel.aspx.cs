/******************************************************************************************
<Author				    : -   Anurag Verma
<Start Date				: -	  11/18/2005
<End Date				: -	
<Description				: - 	
<Review Date				: - 
<Reviewed By			: - 	

Modification History
<Modified Date			: -		5th Apr,06	
<Modified By			: -		Swastika Gaur
<Purpose				: -		Added Delete Button
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
using Cms.Model.Application.HomeOwners;
using Cms.Model.Policy.Homeowners;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb.Controls;


namespace Cms.Policies.aspx.HomeOwners
{
	/// <summary>
	/// Summary description for PolicyAddSolidFuel.
	/// </summary>
	public class PolicyAddSolidFuel : Cms.Policies.policiesbase
	{
		#region Page controls declaration
		protected System.Web.UI.WebControls.TextBox txtMANUFACTURER;
		protected System.Web.UI.WebControls.TextBox txtBRAND_NAME;
		protected System.Web.UI.WebControls.TextBox txtMODEL_NUMBER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_UNIT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCONSTRUCTION;
		protected System.Web.UI.WebControls.DropDownList cmbSTOVE_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbHAVE_LABORATORY_LABEL;
		protected System.Web.UI.WebControls.DropDownList cmbIS_UNIT;
		protected System.Web.UI.WebControls.DropDownList cmbCONSTRUCTION;
		protected System.Web.UI.WebControls.DropDownList cmbLOCATION;
		protected System.Web.UI.WebControls.DropDownList cmbWAS_PROF_INSTALL_DONE;
		protected System.Web.UI.WebControls.DropDownList cmbINSTALL_INSPECTED_BY;
		protected System.Web.UI.WebControls.DropDownList cmbHEATING_USE;
		protected System.Web.UI.WebControls.DropDownList cmbHEATING_SOURCE;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;

		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btn;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;


		protected System.Web.UI.WebControls.Label lblMessage;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.WebControls.Label lblDelete;
		#endregion

		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		//private int	intLoggedInUserID;
		protected System.Web.UI.WebControls.Label capMANUFACTURER;
		protected System.Web.UI.WebControls.Label capBRAND_NAME;
		protected System.Web.UI.WebControls.Label capMODEL_NUMBER;
		protected System.Web.UI.WebControls.Label capFUEL;
		protected System.Web.UI.WebControls.Label capSTOVE_TYPE;
		protected System.Web.UI.WebControls.Label capHAVE_LABORATORY_LABEL;
		protected System.Web.UI.WebControls.Label capIS_UNIT;
		protected System.Web.UI.WebControls.Label capUNIT_OTHER_DESC;
		protected System.Web.UI.WebControls.Label capCONSTRUCTION;
		protected System.Web.UI.WebControls.Label capLOCATION;
		protected System.Web.UI.WebControls.Label capLOC_OTHER_DESC;
		protected System.Web.UI.WebControls.Label capYEAR_DEVICE_INSTALLED;
		protected System.Web.UI.WebControls.Label capWAS_PROF_INSTALL_DONE;
		protected System.Web.UI.WebControls.Label capINSTALL_INSPECTED_BY;
		protected System.Web.UI.WebControls.Label capINSTALL_OTHER_DESC;
		protected System.Web.UI.WebControls.Label capHEATING_USE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHEATING_USE;
		protected System.Web.UI.WebControls.Label capHEATING_SOURCE;
		protected System.Web.UI.WebControls.Label capOTHER_DESC;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFUEL_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOL_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOL_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.WebControls.TextBox txtUNIT_OTHER_DESC;
		protected System.Web.UI.WebControls.TextBox txtYEAR_DEVICE_INSTALLED;
		protected System.Web.UI.WebControls.Label lblISUNIT;
		protected System.Web.UI.WebControls.Label lblLOCATION;
		protected System.Web.UI.WebControls.Label lblINSTALL;
		protected System.Web.UI.WebControls.Label lblSOURCE;
		protected System.Web.UI.WebControls.Label lblUNIT_OTHER_DESC;
		protected System.Web.UI.WebControls.Label lblLOC_OTHER_DESC;
		protected System.Web.UI.WebControls.Label lblINSTALL_OTHER_DESC;
		protected System.Web.UI.WebControls.Label lblOTHER_DESC;
		protected System.Web.UI.WebControls.RangeValidator rngYEAR_DEVICE_INSTALLED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvYEAR_DEVICE_INSTALLED;
		//Defining the business layer class object
		string strCalledFrom="";
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSTOVE_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOCATION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINSTALL_INSPECTED_BY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvWAS_PROF_INSTALL_DONE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHEATING_SOURCE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMANUFACTURER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMODEL_NUMBER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINSTALL_OTHER_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvOTHER_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvUNIT_OTHER_DESC;
		protected System.Web.UI.WebControls.TextBox txtINSTALL_OTHER_DESC;
		protected System.Web.UI.WebControls.TextBox txtOTHER_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOC_OTHER_DESC;
		protected System.Web.UI.WebControls.TextBox txtLOC_OTHER_DESC;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.WebControls.Label capSTOVE_INSTALLATION_CONFORM_SPECIFICATIONS;
		protected System.Web.UI.WebControls.DropDownList cmbSTOVE_INSTALLATION_CONFORM_SPECIFICATIONS;
		protected System.Web.UI.WebControls.DropDownList cmbFUEL;
		 
		Cms.BusinessLayer.BlApplication.ClsSolidFuel objSolidFuel ;
		//END:*********** Local variables *************

		#endregion
		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// 
		/// </summary>
		private void SetErrorMessages()
		{
			//rfvLOCATION.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"1");
			//rngYEAR_DEVICE_INSTALLED.ErrorMessage          =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage ("164");
			rngYEAR_DEVICE_INSTALLED.ErrorMessage          =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage ("677");
			//revYEAR_DEVICE_INSTALLED.ValidationExpression  =  aRegExpInteger;
			//revYEAR_DEVICE_INSTALLED.ErrorMessage          = Cms.CmsWeb.ClsMessages.FetchGeneralMessage ("26");
			
			rngYEAR_DEVICE_INSTALLED.MaximumValue		   = DateTime.Now.Year.ToString();
			//rngYEAR_DEVICE_INSTALLED.MinimumValue		   = "1000";//aAppMinYear;
			rngYEAR_DEVICE_INSTALLED.MinimumValue		   = aAppMinYear;
			rngYEAR_DEVICE_INSTALLED.Type = System.Web.UI.WebControls.ValidationDataType.Integer;
			rfvYEAR_DEVICE_INSTALLED.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"166");	

			rfvHEATING_SOURCE.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"547");
			rfvHEATING_USE.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"548");
			rfvINSTALL_INSPECTED_BY.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"546");
			rfvLOCATION.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"544");
			rfvSTOVE_TYPE.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"542");
			rfvWAS_PROF_INSTALL_DONE.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"551");
			//rngYEAR_DEVICE_INSTALLED.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"516");
			rfvMANUFACTURER.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"538");
			rfvMODEL_NUMBER.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"540");
			rfvOTHER_DESC.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"550");
			rfvUNIT_OTHER_DESC.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"550");
			rfvLOC_OTHER_DESC.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"550");
			rfvINSTALL_OTHER_DESC.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"550");

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
				hidCalledFrom.Value=Request.QueryString["CalledFrom"].ToString();
			}
		
			//Setting screen Id.	
			switch (strCalledFrom) 
			{
				case "Home":
				case "HOME":
					base.ScreenId="244_0";
					break;
				case "Rental":
				case "RENTAL":
					base.ScreenId="163_0";
					break;
				default:
					base.ScreenId="249";
					break;
			}
			#endregion
			btnReset.Attributes.Add("onclick","javascript:ResetForm('" + Page.Controls[0].ID + "' );EnableDisableCtrls();return false;");
			// phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
			lblMessage.Visible = false;
			SetErrorMessages();

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	CmsButtonType.Write;
			btnReset.PermissionString	=	gstrSecurityXML;

			
			btnSave.CmsButtonClass	 =	CmsButtonType.Write;
			btnSave.PermissionString =	gstrSecurityXML;

			
			btnActivateDeactivate.CmsButtonClass	 =	CmsButtonType.Write;
			btnActivateDeactivate.PermissionString =	gstrSecurityXML;

			
			btnDelete.CmsButtonClass	 =	CmsButtonType.Delete;
			btnDelete.PermissionString =	gstrSecurityXML;
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.aspx.HomeOwners.PolicyAddSolidFuel" ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{
				GetQueryString();
				GetOldDataXML();
				SetCaptions();
				PopulateDropDown();
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
			catch (Exception ex)
			{
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				return false;
			}
		}

		private void PopulateDropDown()
		{
			Cms.BusinessLayer.BlCommon.ClsCommon.BindLookupDDL(cmbSTOVE_TYPE,"STOVET","");
			Cms.BusinessLayer.BlCommon.ClsCommon.BindLookupDDL(cmbIS_UNIT,"ISUNIT","");
			Cms.BusinessLayer.BlCommon.ClsCommon.BindLookupDDL(cmbCONSTRUCTION,"DIVCON","");
			Cms.BusinessLayer.BlCommon.ClsCommon.BindLookupDDL(cmbLOCATION,"DIVLOC","");
			Cms.BusinessLayer.BlCommon.ClsCommon.BindLookupDDL(cmbINSTALL_INSPECTED_BY,"DIVINS","");
			Cms.BusinessLayer.BlCommon.ClsCommon.BindLookupDDL(cmbHEATING_USE,"DIVHEA","");
			Cms.BusinessLayer.BlCommon.ClsCommon.BindLookupDDL(cmbHEATING_SOURCE,"DHSRC","");
			Cms.BusinessLayer.BlCommon.ClsCommon.BindLookupDDL(cmbSTOVE_INSTALLATION_CONFORM_SPECIFICATIONS,"YESNO","");
			Cms.BusinessLayer.BlCommon.ClsCommon.BindLookupDDL(cmbFUEL,"FUEL","");
		}


		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		
		private Cms.Model.Policy.Homeowners.ClsPolicySolidFuelInfo GetFormValue()
		{
			
			//Creating the Model object for holding the New data
			Cms.Model.Policy.Homeowners.ClsPolicySolidFuelInfo objSolidFuelInfo;
			objSolidFuelInfo = new Cms.Model.Policy.Homeowners.ClsPolicySolidFuelInfo();
			
			objSolidFuelInfo.CUSTOMER_ID=int.Parse(hidCUSTOMER_ID.Value);
			objSolidFuelInfo.POLICY_ID=int.Parse(hidPOL_ID.Value);
			objSolidFuelInfo.POLICY_VERSION_ID=int.Parse(hidPOL_VERSION_ID.Value);

			objSolidFuelInfo.MANUFACTURER=	txtMANUFACTURER.Text;
			objSolidFuelInfo.BRAND_NAME=	txtBRAND_NAME.Text;
			objSolidFuelInfo.MODEL_NUMBER=	txtMODEL_NUMBER.Text;
			if (cmbFUEL.SelectedItem != null && cmbFUEL.SelectedValue != "")
				objSolidFuelInfo.FUEL =	cmbFUEL.SelectedValue;
			objSolidFuelInfo.STOVE_TYPE=	cmbSTOVE_TYPE.SelectedValue;
			objSolidFuelInfo.HAVE_LABORATORY_LABEL=	cmbHAVE_LABORATORY_LABEL.SelectedValue;
			objSolidFuelInfo.IS_UNIT=	cmbIS_UNIT.SelectedValue;
			
			if ( cmbIS_UNIT.SelectedItem.Text.ToLower().StartsWith("other"))
			{
				objSolidFuelInfo.UNIT_OTHER_DESC=	txtUNIT_OTHER_DESC.Text;
			}
			else
			{
				objSolidFuelInfo.UNIT_OTHER_DESC=	"";
			}
			
			objSolidFuelInfo.CONSTRUCTION=	cmbCONSTRUCTION.SelectedValue;
			objSolidFuelInfo.LOCATION=	cmbLOCATION.SelectedValue;
			
			if ( cmbLOCATION.SelectedItem.Text.ToLower().StartsWith("other"))
			{
				objSolidFuelInfo.LOC_OTHER_DESC=	txtLOC_OTHER_DESC.Text;
			}
			else
			{
				objSolidFuelInfo.LOC_OTHER_DESC = "";
			}

			if (txtYEAR_DEVICE_INSTALLED.Text.Trim() != "")
				objSolidFuelInfo.YEAR_DEVICE_INSTALLED = int.Parse(txtYEAR_DEVICE_INSTALLED.Text);
			objSolidFuelInfo.WAS_PROF_INSTALL_DONE=	cmbWAS_PROF_INSTALL_DONE.SelectedValue;
		
			objSolidFuelInfo.INSTALL_INSPECTED_BY=	cmbINSTALL_INSPECTED_BY.SelectedValue;
				
			if ( cmbINSTALL_INSPECTED_BY.SelectedItem.Text.ToLower().StartsWith("other"))
			{
				objSolidFuelInfo.INSTALL_OTHER_DESC=	txtINSTALL_OTHER_DESC.Text;
			}
			else
			{
				objSolidFuelInfo.INSTALL_OTHER_DESC = "";
			}
			
			objSolidFuelInfo.HEATING_USE=	cmbHEATING_USE.SelectedValue;
			objSolidFuelInfo.HEATING_SOURCE=	cmbHEATING_SOURCE.SelectedValue;
			
			if ( cmbHEATING_SOURCE.SelectedItem.Text.ToLower().StartsWith("other"))
			{
				objSolidFuelInfo.OTHER_DESC =	txtOTHER_DESC.Text;
			}
			else
			{
				objSolidFuelInfo.OTHER_DESC = "";
			}
			if (cmbSTOVE_INSTALLATION_CONFORM_SPECIFICATIONS.SelectedItem != null && cmbSTOVE_INSTALLATION_CONFORM_SPECIFICATIONS.SelectedValue != "")
			{
				objSolidFuelInfo.STOVE_INSTALLATION_CONFORM_SPECIFICATIONS = int.Parse(cmbSTOVE_INSTALLATION_CONFORM_SPECIFICATIONS.SelectedValue);
			}

			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidFUEL_ID.Value;
			oldXML		= hidOldData.Value;
			//Returning the model object

			return objSolidFuelInfo;
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
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
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
				objSolidFuel = new  Cms.BusinessLayer.BlApplication.ClsSolidFuel(); 
				objSolidFuel.LoggedInUserId	=	int.Parse(GetUserId());
				//Retreiving the form values into model class object
				Model.Policy.Homeowners.ClsPolicySolidFuelInfo objSolidFuelInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objSolidFuelInfo.CREATED_BY = int.Parse(GetUserId());
					objSolidFuelInfo.CREATED_DATETIME = DateTime.Now;

					//Calling the add method of business layer class
					intRetVal = objSolidFuel.Add(objSolidFuelInfo);

					if(intRetVal>0)
					{
						hidFUEL_ID.Value        = objSolidFuelInfo.FUEL_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidIS_ACTIVE.Value = "Y";
						GetOldDataXML();
						SetWorkflow();

						//Showing the endorsement popup window
						base.OpenEndorsementDetails();
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"18");
						hidFormSaved.Value			=		"2";
						return;
					}
					else
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value			=	"2";
						return;
					}
					lblMessage.Visible = true;
				} // end save case
				else //UPDATE CASE
				{

					//Creating the Model object for holding the Old data
					ClsPolicySolidFuelInfo objOldSolidFuelInfo;
					objOldSolidFuelInfo = new ClsPolicySolidFuelInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldSolidFuelInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objSolidFuelInfo.FUEL_ID = Convert.ToInt32(strRowId);
					objSolidFuelInfo.MODIFIED_BY = int.Parse(GetUserId());
					objSolidFuelInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					objSolidFuelInfo.IS_ACTIVE = hidIS_ACTIVE.Value;

					//Updating the record using business layer class object
					intRetVal	= objSolidFuel.Update(objOldSolidFuelInfo,objSolidFuelInfo);
					if( intRetVal >= 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						GetOldDataXML();
						SetWorkflow();

						//Showing the endorsement popup window
						base.OpenEndorsementDetails();
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"18");
						hidFormSaved.Value		=	"2";
						return;
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value		=	"3";
						return;
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
				//ExceptionManager.Publish(ex);

				return;
			}
			finally
			{
				if(objSolidFuel!= null)
					objSolidFuel.Dispose();
			}

			//GetOldDataXML();
		}
		#endregion

		/// <summary>
		/// Activates and deactivates  .
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			int intRetval  =  0 ;
			try
			{
				/*Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo ();					
				objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());	
				objStuTransactionInfo.loggedInUserName = GetUserName();*/ //Commented by Charles on 21-Oct-09 for Itrack 6599	
				
				ClsPolicySolidFuelInfo objSolidFuelInfo = new ClsPolicySolidFuelInfo();//Added by Charles on 21-Oct-09 for Itrack 6599	
				objSolidFuelInfo.MODIFIED_BY=int.Parse(GetUserId());
				objSolidFuelInfo.CUSTOMER_ID=int.Parse(hidCUSTOMER_ID.Value);
				objSolidFuelInfo.POLICY_ID=int.Parse(hidPOL_ID.Value);
				objSolidFuelInfo.POLICY_VERSION_ID=int.Parse(hidPOL_VERSION_ID.Value);
				objSolidFuelInfo.FUEL_ID=int.Parse(hidFUEL_ID.Value);//Added till here
				
				Cms.BusinessLayer.BlApplication.ClsSolidFuel objSolidFuel=new Cms.BusinessLayer.BlApplication.ClsSolidFuel(); 
				
				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					//Commented by Charles on 21-Oct-09 for Itrack 6599
					/*objStuTransactionInfo.transactionDescription = "Deactivated Succesfully."; 
					objSolidFuel.TransactionInfoParams = objStuTransactionInfo; 
					intRetval = objSolidFuel.ActivateDeactivatePolSolidFuel(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOL_ID.Value),int.Parse(hidPOL_VERSION_ID.Value),"N",int.Parse(hidFUEL_ID.Value));*/
					intRetval = objSolidFuel.ActivateDeactivatePolSolidFuel(objSolidFuelInfo,"N");//Added by Charles on 21-Oct-09 for Itrack 6599	
					lblMessage.Text =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";
				}
				else
				{
					//Commented by Charles on 21-Oct-09 for Itrack 6599
					/*objStuTransactionInfo.transactionDescription = "Activated Succesfully.";
					objSolidFuel.TransactionInfoParams = objStuTransactionInfo; 
					intRetval = objSolidFuel.ActivateDeactivatePolSolidFuel(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOL_ID.Value),int.Parse(hidPOL_VERSION_ID.Value),"Y",int.Parse(hidFUEL_ID.Value));*/
					intRetval = objSolidFuel.ActivateDeactivatePolSolidFuel(objSolidFuelInfo,"Y");//Added by Charles on 21-Oct-09 for Itrack 6599	
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";
				}
				hidFormSaved.Value			=	"1";
				GetOldDataXML();
				if(intRetval > 0 )
				{
					//Showing the endorsement popup window
					base.OpenEndorsementDetails();
				}
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21")+ " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				ExceptionManager.Publish(ex);
			}
			finally
			{
				lblMessage.Visible = true;
				if(objSolidFuel!= null)
					objSolidFuel.Dispose();
			}
		}
		

		private void SetCaptions()
		{
			capMANUFACTURER.Text				  =		objResourceMgr.GetString("txtMANUFACTURER");
			capBRAND_NAME.Text					  =		objResourceMgr.GetString("txtBRAND_NAME");
			capMODEL_NUMBER.Text				  =		objResourceMgr.GetString("txtMODEL_NUMBER");
			capFUEL.Text						  =		objResourceMgr.GetString("cmbFUEL");
			capSTOVE_TYPE.Text					  =		objResourceMgr.GetString("cmbSTOVE_TYPE");
			capHAVE_LABORATORY_LABEL.Text		  =		objResourceMgr.GetString("cmbHAVE_LABORATORY_LABEL");
			capIS_UNIT.Text						  =		objResourceMgr.GetString("cmbIS_UNIT");
			capUNIT_OTHER_DESC.Text			      =		objResourceMgr.GetString("txtUNIT_OTHER_DESC");
			capCONSTRUCTION.Text				  =		objResourceMgr.GetString("cmbCONSTRUCTION");
			capLOCATION.Text					  =		objResourceMgr.GetString("cmbLOCATION");
			capLOC_OTHER_DESC.Text				  =		objResourceMgr.GetString("txtLOC_OTHER_DESC");
			capYEAR_DEVICE_INSTALLED.Text		  =		objResourceMgr.GetString("txtYEAR_DEVICE_INSTALLED");
			capWAS_PROF_INSTALL_DONE.Text		  =		objResourceMgr.GetString("cmbWAS_PROF_INSTALL_DONE");
			capINSTALL_INSPECTED_BY.Text		  =		objResourceMgr.GetString("cmbINSTALL_INSPECTED_BY");
			capINSTALL_OTHER_DESC.Text			  =		objResourceMgr.GetString("txtINSTALL_OTHER_DESC");
			capHEATING_USE.Text					  =		objResourceMgr.GetString("cmbHEATING_USE");
			capHEATING_SOURCE.Text				  =		objResourceMgr.GetString("cmbHEATING_SOURCE");
			capOTHER_DESC.Text					  =		objResourceMgr.GetString("txtOTHER_DESC");
			capSTOVE_INSTALLATION_CONFORM_SPECIFICATIONS.Text = objResourceMgr.GetString("cmbSTOVE_INSTALLATION_CONFORM_SPECIFICATIONS");
		}
		private void GetOldDataXML()
		{
			if ( hidFUEL_ID.Value != "" ) 
			{
				hidOldData.Value = Cms.BusinessLayer.BlApplication.ClsSolidFuel.GetPolicySolidFuelXml(
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
			hidFUEL_ID.Value = Request.Params["FUEL_ID"];

		}

		private void SetWorkflow()
		{
			if(base.ScreenId	==	"244_0" || base.ScreenId == "163_0")
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",hidCUSTOMER_ID.Value);
				myWorkFlow.AddKeyValue("POLICY_ID",hidPOL_ID.Value);
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",hidPOL_VERSION_ID.Value);
				if ( hidFUEL_ID.Value != "" ) 
				{
					myWorkFlow.AddKeyValue("FUEL_ID",hidFUEL_ID.Value);
				}
				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();	
				myWorkFlow.WorkflowModule="POL";
			}
			else
			{
				myWorkFlow.Display	=	false;
			}
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			int intRetVal;
			objSolidFuel = new  Cms.BusinessLayer.BlApplication.ClsSolidFuel(); 
			Model.Policy.Homeowners.ClsPolicySolidFuelInfo objSolidFuelInfo = GetFormValue();

			objSolidFuelInfo.MODIFIED_BY = int.Parse(GetUserId());
			objSolidFuel.LoggedInUserId	=	int.Parse(GetUserId());

			if(hidFUEL_ID.Value!=null && hidFUEL_ID.Value!="")
				objSolidFuelInfo.FUEL_ID=int.Parse(hidFUEL_ID.Value);

			intRetVal = objSolidFuel.Delete(objSolidFuelInfo);
						
			if(intRetVal>0)
			{
				lblDelete.Text			= Cms.CmsWeb.ClsMessages.GetMessage("G","127");
				hidFormSaved.Value = "5";
				hidOldData.Value = "";
				trBody.Attributes.Add("style","display:none");
			}
			else if(intRetVal == -1)
			{
			
				lblDelete.Text			=	ClsMessages.GetMessage(base.ScreenId,"128");
				hidFormSaved.Value		=	"2";
			}
			SetWorkflow();
			lblDelete.Visible = true;
		}
	}
}

