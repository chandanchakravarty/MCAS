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
using Cms.BusinessLayer.BlApplication.HomeOwners;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.Model.Policy.Homeowners;
using Cms.CmsWeb;

namespace Cms.Policies.aspx.Homeowners
{
	/// <summary>
	/// Summary description for policyAddOtherStructures.
	/// </summary>
	public class PolicyAddOtherStructures: Cms.Policies.policiesbase
	{
		#region Page controls declaration
		protected System.Web.UI.WebControls.DropDownList cmbPREMISES_LOCATION;
		protected System.Web.UI.WebControls.TextBox txtPREMISES_DESCRIPTION;
		protected System.Web.UI.WebControls.TextBox txtPREMISES_USE;
		protected System.Web.UI.WebControls.DropDownList cmbPREMISES_CONDITION;
		protected System.Web.UI.WebControls.DropDownList cmbPICTURE_ATTACHED;
		protected System.Web.UI.WebControls.DropDownList cmbCOVERAGE_BASIS;
		protected System.Web.UI.WebControls.DropDownList cmbSATELLITE_EQUIPMENT;
		protected System.Web.UI.WebControls.DropDownList cmbSOLID_FUEL_DEVICE; //Added by Charles on 27-Nov-09 for Itrack 6681
		protected System.Web.UI.WebControls.DropDownList cmbAPPLY_ENDS; //Added by Charles on 3-Dec-09 for Itrack 6405

		protected System.Web.UI.WebControls.TextBox txtLOCATION_ADDRESS;
		protected System.Web.UI.WebControls.TextBox txtLOCATION_CITY;
		protected System.Web.UI.WebControls.DropDownList cmbLOCATION_STATE;
		protected System.Web.UI.WebControls.TextBox txtLOCATION_ZIP;
		protected System.Web.UI.WebControls.TextBox txtADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED;
		protected System.Web.UI.WebControls.TextBox txtINSURING_VALUE;
		protected System.Web.UI.WebControls.TextBox txtINSURING_VALUE_OFF_PREMISES;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerAddress;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerCity;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerState;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerZip;
		protected System.Web.UI.WebControls.Label capLIABILITY_EXTENDED;
		protected System.Web.UI.WebControls.DropDownList cmbLIABILITY_EXTENDED;

		protected System.Web.UI.WebControls.Label capPREMISES_LOCATION;
		protected System.Web.UI.WebControls.Label capPREMISES_DESCRIPTION;
		protected System.Web.UI.WebControls.Label capPREMISES_USE;
		protected System.Web.UI.WebControls.Label capPREMISES_CONDITION;
		protected System.Web.UI.WebControls.Label capPICTURE_ATTACHED;
		protected System.Web.UI.WebControls.Label capCOVERAGE_BASIS;
		protected System.Web.UI.WebControls.Label capINSURING_VALUE;
		protected System.Web.UI.WebControls.Label capADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED;
		protected System.Web.UI.WebControls.Label capINSURING_VALUE_OFF_PREMISES;
		protected System.Web.UI.WebControls.Label capLOCATION_ADDRESS;
		protected System.Web.UI.WebControls.Label capLOCATION_CITY;
		protected System.Web.UI.WebControls.Label capLOCATION_STATE;
		protected System.Web.UI.WebControls.Label capLOCATION_ZIP;
		protected System.Web.UI.WebControls.Label capSATELLITE_EQUIPMENT;
		protected System.Web.UI.WebControls.Label capSOLID_FUEL_DEVICE; //Added by Charles on 27-Nov-09 for Itrack 6681

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;

		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPREMISES_LOCATION;

		protected System.Web.UI.WebControls.RegularExpressionValidator revINSURING_VALUE_OFF_PREMISES;
		protected System.Web.UI.WebControls.RegularExpressionValidator revLOCATION_ZIP;
		protected System.Web.UI.WebControls.RegularExpressionValidator revINSURING_VALUE;		
		protected System.Web.UI.WebControls.RegularExpressionValidator revCOVERAGE_AMOUNT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED;		
		protected System.Web.UI.WebControls.Label lblMessage;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.HtmlControls.HtmlTableRow trError;
		protected System.Web.UI.HtmlControls.HtmlTableRow 	trBody;
		protected System.Web.UI.HtmlControls.HtmlTableRow trSOLID_FUEL_DEVICE; //Added by Charles on 27-Nov-09 for Itrack 6681

		protected System.Web.UI.WebControls.Label lblRentedDwellingPolicies;
		protected System.Web.UI.HtmlControls.HtmlInputHidden CalledFROM;

		
		#endregion
		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		int intCustomerId,intPolID,intPolVersionId;//,intDwellingId;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		//private int	intLoggedInUserID;
		protected System.Web.UI.WebControls.CustomValidator csvPREMISES_DESCRIPTION;
		protected System.Web.UI.WebControls.CompareValidator cmpINSURING_VALUE;
		protected System.Web.UI.WebControls.CompareValidator cmpADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED;
		protected Cms.CmsWeb.Controls.CmsButton btnPullCustomerAddress;
		protected System.Web.UI.WebControls.CompareValidator cmpINSURING_VALUE_OFF_PREMISES;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOTHER_STRUCTURE_ID;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		public string strCalledFrom;
		protected System.Web.UI.WebControls.Label capCOVERAGE_AMOUNT;
		protected System.Web.UI.WebControls.TextBox txtCOVERAGE_AMOUNT;
		protected System.Web.UI.WebControls.RangeValidator rngCOVERAGE_AMOUNT;
		//Defining the business layer class object
		ClsOtherStructures  objOtherStructures ;
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
			//	rfvPREMISES_LOCATION.ErrorMessage			= "ooo";
			revLOCATION_ZIP.ValidationExpression	    =	aRegExpZip;
			revLOCATION_ZIP.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"24");//"Please enter a valid zip code.";
			rngCOVERAGE_AMOUNT.ErrorMessage				=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
			revCOVERAGE_AMOUNT.ValidationExpression	= aRegExpDoublePositiveNonZero;
			revCOVERAGE_AMOUNT.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"116");
			/*Commented by Asfa (04-June-2008) - iTrack #3953
			revINSURING_VALUE.ValidationExpression	= aRegExpDoublePositiveNonZero;
			*/
			revINSURING_VALUE.ValidationExpression	= aRegExpCurrencyformat;
			revINSURING_VALUE.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"116");
			//revADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED.ValidationExpression	= aRegExpDoublePositiveNonZero;
			/*Commented by Asfa (04-June-2008) - iTrack #3953
			revADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED.ValidationExpression	= aRegExpDoublePositiveWithZero;
			*/
			revADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED.ValidationExpression	= aRegExpCurrencyformat;
			revADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"116");
			revINSURING_VALUE_OFF_PREMISES.ValidationExpression	= aRegExpDoublePositiveNonZero;
			revINSURING_VALUE_OFF_PREMISES.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"116");
			
			//	revINSURING_VALUE.ValidationExpression   	=	aRegExpInteger;
		
			//	revINSURING_VALUE.ErrorMessage			    =  "fsdfsd";
		}
		#endregion
		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{

			if (Request.QueryString["CalledFrom"] != null)
			{
				CalledFROM.Value = Request.QueryString["CalledFrom"].ToString();
			}
			btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");

			btnPullCustomerAddress.Attributes.Add("onClick","javascript:PullCustomerAddress();return false;");
	
			#region Setting ScreenId
			 strCalledFrom = CalledFROM.Value;
			//Setting screen Id.	
			switch (strCalledFrom.ToUpper()) 
			{
				case "HOME":
					base.ScreenId="239_7_0";
					break;
				case "RENTAL":
					base.ScreenId="259_3_0";
					break;
						
			}
			#endregion
			
			

			// phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
	
			lblMessage.Visible = false;
			SetErrorMessages();

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Execute;
			btnReset.PermissionString		=	gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnActivateDeactivate.PermissionString		=	gstrSecurityXML;

			btnSave.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;

			btnPullCustomerAddress.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Read;
			btnPullCustomerAddress.PermissionString		=	gstrSecurityXML;

			btnDelete.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Delete;
			btnDelete.PermissionString		=	gstrSecurityXML;


			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
		
			objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.aspx.Homeowners.PolicyAddOtherStructures" ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{

				GetDataFromSession();
				if(Request.QueryString["OTHER_STRUCTURE_ID"]!=null )
					hidOTHER_STRUCTURE_ID.Value=Request.QueryString["OTHER_STRUCTURE_ID"].ToString();
				GetOldDataXML();
				SetCaptions();
				SetWorkFlow();
				LoadDropDown();
			}
			this.txtADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");
		
			//TO FETCH CLIENT ADDRESS, CITY, ZIP AND STATE IN HIDDEN VARIABLES
			getCustomerAddress(GetCustomerID());

		}//end pageload
		#endregion
		private void LoadDropDown()
		{
			#region "Loading singleton"
			DataTable dt = Cms.CmsWeb.ClsFetcher.State;
			cmbLOCATION_STATE.DataSource		= dt;
			cmbLOCATION_STATE.DataTextField	= "State_Name";
			cmbLOCATION_STATE.DataValueField	= "State_Id";
			cmbLOCATION_STATE.DataBind();
			cmbLOCATION_STATE.Items.Insert(0,"");//Added by Charles on 15-Sep-09 for Itrack 6405
			#endregion//Loading singleton
			
			cmbPREMISES_LOCATION.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("OSTPL",null,"S");
			cmbPREMISES_LOCATION.DataTextField="LookupDesc";
			cmbPREMISES_LOCATION.DataValueField="LookupID";
			cmbPREMISES_LOCATION.DataBind();
			cmbPREMISES_LOCATION.Items.Insert(0,"");

			//show a Message in red indicating that "Rented Dwelling Policies cover On Premises Locations only "
			if (CalledFROM.Value.ToUpper().Equals("RENTAL") == true)//RENTAL
			{	
				lblRentedDwellingPolicies.Text="Rented Dwelling Policies cover On Premises Locations only.";				
			}
			else//HOME
			{
				lblRentedDwellingPolicies.Text="";
			}

			cmbPREMISES_CONDITION.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CONDS");
			cmbPREMISES_CONDITION.DataTextField="LookupDesc";
			cmbPREMISES_CONDITION.DataValueField="LookupID";
			cmbPREMISES_CONDITION.DataBind();
			cmbPREMISES_CONDITION.Items.Insert(0,"");

			cmbCOVERAGE_BASIS.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("COVB");
			cmbCOVERAGE_BASIS.DataTextField="LookupDesc";
			cmbCOVERAGE_BASIS.DataValueField="LookupID";
			cmbCOVERAGE_BASIS.DataBind();
			cmbCOVERAGE_BASIS.Items.Insert(0,"");
			
			cmbLIABILITY_EXTENDED.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbLIABILITY_EXTENDED.DataTextField="LookupDesc"; 
			cmbLIABILITY_EXTENDED.DataValueField="LookupId";
			cmbLIABILITY_EXTENDED.DataBind();
			cmbLIABILITY_EXTENDED.Items.Insert(0,"");

			cmbPICTURE_ATTACHED.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbPICTURE_ATTACHED.DataTextField="LookupDesc"; 
			cmbPICTURE_ATTACHED.DataValueField="LookupId";
			cmbPICTURE_ATTACHED.DataBind();	
			cmbPICTURE_ATTACHED.Items.Insert(0,"");
			
			cmbSATELLITE_EQUIPMENT.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbSATELLITE_EQUIPMENT.DataTextField="LookupDesc"; 
			cmbSATELLITE_EQUIPMENT.DataValueField="LookupId";
			cmbSATELLITE_EQUIPMENT.DataBind();	
			cmbSATELLITE_EQUIPMENT.Items.Insert(0,"");

			//Added by Charles on 27-Nov-09 for Itrack 6681
			if(strCalledFrom.ToUpper() == "RENTAL")
			{
				trSOLID_FUEL_DEVICE.Visible=false;
			}
			else
			{
				cmbSOLID_FUEL_DEVICE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
				cmbSOLID_FUEL_DEVICE.DataTextField="LookupDesc"; 
				cmbSOLID_FUEL_DEVICE.DataValueField="LookupId";
				cmbSOLID_FUEL_DEVICE.DataBind();	
				cmbSOLID_FUEL_DEVICE.Items.Insert(0,"");
			}//Added till here
		}
		private void GetDataFromSession()
		{
			try
			{
				intPolID =int.Parse(GetPolicyID());
				intPolVersionId =int.Parse(GetPolicyVersionID());
				intCustomerId=int.Parse(GetCustomerID());

			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
				ExceptionManager.Publish(ex);
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
		private ClsPolicyOtherStructuresInfo GetFormValue()
		{
			
		
			//Creating the Model object for holding the New data
			ClsPolicyOtherStructuresInfo objOtherStructuresInfo = new ClsPolicyOtherStructuresInfo();
			objOtherStructuresInfo.POLICY_ID = int.Parse(GetPolicyID());
			objOtherStructuresInfo.POLICY_VERSION_ID = int.Parse(GetPolicyVersionID());
			objOtherStructuresInfo.CUSTOMER_ID = int.Parse(GetCustomerID());
			objOtherStructuresInfo.DWELLING_ID = int.Parse(Request.QueryString["DWELLINGID"].ToString());
			objOtherStructuresInfo.CalledFrom =CalledFROM.Value ;

			objOtherStructuresInfo.PREMISES_LOCATION=	cmbPREMISES_LOCATION.SelectedValue;
			objOtherStructuresInfo.INSURING_VALUE =-1;
			objOtherStructuresInfo.INSURING_VALUE_OFF_PREMISES = -1;
			objOtherStructuresInfo.ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED = -1;
			objOtherStructuresInfo.PREMISES_DESCRIPTION = txtPREMISES_DESCRIPTION.Text.Trim();

			if (cmbPREMISES_LOCATION.SelectedValue == "11840")	//Off premises
			{
				objOtherStructuresInfo.LOCATION_ADDRESS=	txtLOCATION_ADDRESS.Text;
				objOtherStructuresInfo.LOCATION_CITY=	txtLOCATION_CITY.Text;
				if(cmbLOCATION_STATE.SelectedItem!=null && cmbLOCATION_STATE.SelectedItem.Value!="")//Added by Charles on 15-Sep-09 for Itrack 6405
					objOtherStructuresInfo.LOCATION_STATE=	cmbLOCATION_STATE.SelectedValue;
				objOtherStructuresInfo.LOCATION_ZIP=	txtLOCATION_ZIP.Text;
				if(cmbLIABILITY_EXTENDED.SelectedItem!=null && cmbLIABILITY_EXTENDED.SelectedItem.Value!="")
					objOtherStructuresInfo.LIABILITY_EXTENDED = int.Parse(cmbLIABILITY_EXTENDED.SelectedItem.Value);

				if(txtINSURING_VALUE_OFF_PREMISES.Text.Trim() != "")
				{
					objOtherStructuresInfo.INSURING_VALUE_OFF_PREMISES=	double.Parse(txtINSURING_VALUE_OFF_PREMISES.Text);
				}
			}
			if(objOtherStructuresInfo.PREMISES_LOCATION==ClsOtherStructures.PREMISES_LOCATION_ON_PREMISES_RENTED_OTHERS && txtCOVERAGE_AMOUNT.Text.Trim()!=""  && cmbCOVERAGE_BASIS.SelectedValue=="114213")			
				objOtherStructuresInfo.COVERAGE_AMOUNT = double.Parse(txtCOVERAGE_AMOUNT.Text.Trim());

			objOtherStructuresInfo.PREMISES_USE=	txtPREMISES_USE.Text.Trim();
			objOtherStructuresInfo.PREMISES_CONDITION=	cmbPREMISES_CONDITION.SelectedValue;
			objOtherStructuresInfo.PICTURE_ATTACHED=	cmbPICTURE_ATTACHED.SelectedValue;
			
			if (cmbCOVERAGE_BASIS.SelectedItem != null && cmbCOVERAGE_BASIS.SelectedValue != "")
			{
				objOtherStructuresInfo.COVERAGE_BASIS=	cmbCOVERAGE_BASIS.SelectedValue;

				//if (cmbCOVERAGE_BASIS.SelectedIndex == 1)
				if (cmbCOVERAGE_BASIS.SelectedValue   == "11846")
				{
					//value="1">Repair</option>
					if(txtINSURING_VALUE.Text.Trim() != "")
					{
						objOtherStructuresInfo.INSURING_VALUE = double.Parse(txtINSURING_VALUE.Text.Trim());
					}
					else
					{
						objOtherStructuresInfo.INSURING_VALUE =-1;
					}	
	
					//Added by Charles on 14-Oct-09 for Itrack 6405					
					if(txtADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED.Text.Trim() != "")
					{
						objOtherStructuresInfo.ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED=	double.Parse(txtADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED.Text.Trim());
					}
					else
					{
						objOtherStructuresInfo.ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED =-1;
					}						
					objOtherStructuresInfo.SATELLITE_EQUIPMENT=	cmbSATELLITE_EQUIPMENT.SelectedValue;				
					//Added till here
				}
				//else if (cmbCOVERAGE_BASIS.SelectedIndex == 2)
				else if (cmbCOVERAGE_BASIS.SelectedValue   == "11847")
				{
					//value="2">Replacement </option>
					if(txtADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED.Text.Trim() != "")
					{
						objOtherStructuresInfo.ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED=	double.Parse(txtADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED.Text.Trim());
					}
					else
					{
						objOtherStructuresInfo.ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED =-1;
					}
					objOtherStructuresInfo.SATELLITE_EQUIPMENT=	cmbSATELLITE_EQUIPMENT.SelectedValue;
					
				}
				else
				{
					if(cmbPREMISES_LOCATION.SelectedValue == "11968" || cmbPREMISES_LOCATION.SelectedValue == "11841")//Added by Charles on 3-Dec-09 for Itrack 6405
					{
						if(cmbAPPLY_ENDS.SelectedValue != "")
							objOtherStructuresInfo.APPLY_ENDS = cmbAPPLY_ENDS.SelectedValue;
					}//Added till here
					objOtherStructuresInfo.INSURING_VALUE =-1;					
					objOtherStructuresInfo.ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED =-1;
				}
			}			
			//Added by Charles on 27-Nov-09 for Itrack 6681
			if(trSOLID_FUEL_DEVICE.Visible)
				objOtherStructuresInfo.SOLID_FUEL_DEVICE = cmbSOLID_FUEL_DEVICE.SelectedValue; //Added till here

			if(Request.QueryString["OTHER_STRUCTURE_ID"]!= null)
				objOtherStructuresInfo.OTHER_STRUCTURE_ID = int.Parse(Request.QueryString["OTHER_STRUCTURE_ID"].ToString());
			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidOTHER_STRUCTURE_ID.Value;
			oldXML		= hidOldData.Value;
			//Returning the model object

			return objOtherStructuresInfo;
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
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.ID = "APP_OTHER_STRUCTURE_DWELLING";
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
				objOtherStructures = new  ClsOtherStructures();

				//Retreiving the form values into model class object
				ClsPolicyOtherStructuresInfo objOtherStructuresInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objOtherStructuresInfo.CREATED_BY = int.Parse(GetUserId());
					objOtherStructuresInfo.CREATED_DATETIME = DateTime.Now;

					//Calling the add method of business layer class
					intRetVal = objOtherStructures.Add_Pol(objOtherStructuresInfo);

					if(intRetVal>0)
					{
						hidOTHER_STRUCTURE_ID.Value = objOtherStructuresInfo.OTHER_STRUCTURE_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						GetOldDataXML();
						hidIS_ACTIVE.Value = "Y";
						base.OpenEndorsementDetails();
						SetWorkFlow();
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
					ClsPolicyOtherStructuresInfo objOldOtherStructuresInfo;
					objOldOtherStructuresInfo = new ClsPolicyOtherStructuresInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldOtherStructuresInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objOtherStructuresInfo.OTHER_STRUCTURE_ID = int.Parse(strRowId);
					objOtherStructuresInfo.MODIFIED_BY = int.Parse(GetUserId());
					objOtherStructuresInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					//Updating the record using business layer class object
					intRetVal	= objOtherStructures.Update_Pol(objOldOtherStructuresInfo,objOtherStructuresInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						GetOldDataXML();
						base.OpenEndorsementDetails();
						SetWorkFlow();
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
				SetActivateDeactivate();
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
				if(objOtherStructures!= null)
					objOtherStructures.Dispose();
			}
		}
	
		#region Activate/ Deactivate feature
		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{			
			int intRetVal;
			try
			{
				objOtherStructures = new  ClsOtherStructures();
				ClsPolicyOtherStructuresInfo objNewOtherStructuresInfo = GetFormValue();

				ClsPolicyOtherStructuresInfo objOldOtherStructuresInfo= new ClsPolicyOtherStructuresInfo();
				objNewOtherStructuresInfo.OTHER_STRUCTURE_ID=  int.Parse(hidOTHER_STRUCTURE_ID.Value);
				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					objNewOtherStructuresInfo.IS_ACTIVE="N";
					intRetVal	=	objOtherStructures.ActivateDeActivatePolOtherStructureDetails(objNewOtherStructuresInfo,false);
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";	
				}
				else
				{
					objNewOtherStructuresInfo.IS_ACTIVE="Y";
					intRetVal	=	objOtherStructures.ActivateDeActivatePolOtherStructureDetails(objNewOtherStructuresInfo,true);
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";	
				}					
				hidFormSaved.Value			=	"1";
				base.OpenEndorsementDetails();						
				SetActivateDeactivate();
					
			}

			catch(Exception ex)
			{
				lblMessage.Text	=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"21")+ " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				ExceptionManager.Publish(ex);
			}
			finally
			{
				lblMessage.Visible = true;
				
			}
		}

		private void SetActivateDeactivate()
		{
			try
			{
				hidIS_ACTIVE.Value	=	hidIS_ACTIVE.Value.Trim();
				btnActivateDeactivate.Visible=true;
				if (hidIS_ACTIVE.Value == "N")
				{
					btnActivateDeactivate.Text = "Activate";
				}
				else if (hidIS_ACTIVE.Value == "Y")
				{
					btnActivateDeactivate.Text = "Deactivate";
				}
				else
					btnActivateDeactivate.Visible=false;
			}
			catch(Exception objEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objEx);
			}
		}
	#endregion
		#endregion
	
		private void SetCaptions()
		{
			capPREMISES_LOCATION.Text						=		objResourceMgr.GetString("cmbPREMISES_LOCATION");
			capPREMISES_DESCRIPTION.Text						=		objResourceMgr.GetString("txtPREMISES_DESCRIPTION");
			capPREMISES_USE.Text						=		objResourceMgr.GetString("txtPREMISES_USE");
			capPREMISES_CONDITION.Text						=		objResourceMgr.GetString("cmbPREMISES_CONDITION");
			capPICTURE_ATTACHED.Text						=		objResourceMgr.GetString("cmbPICTURE_ATTACHED");
			capCOVERAGE_BASIS.Text						=		objResourceMgr.GetString("cmbCOVERAGE_BASIS");
			capSATELLITE_EQUIPMENT.Text						=		objResourceMgr.GetString("cmbSATELLITE_EQUIPMENT");
			capLOCATION_ADDRESS.Text						=		objResourceMgr.GetString("txtLOCATION_ADDRESS");
			capLOCATION_CITY.Text						=		objResourceMgr.GetString("txtLOCATION_CITY");
			capLOCATION_STATE.Text						=		objResourceMgr.GetString("cmbLOCATION_STATE");
			capLOCATION_ZIP.Text						=		objResourceMgr.GetString("txtLOCATION_ZIP");
			capADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED.Text						=		objResourceMgr.GetString("txtADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED");
			capINSURING_VALUE.Text						=		objResourceMgr.GetString("txtINSURING_VALUE");
			capINSURING_VALUE_OFF_PREMISES.Text						=		objResourceMgr.GetString("txtINSURING_VALUE_OFF_PREMISES");
			capCOVERAGE_AMOUNT.Text						=		objResourceMgr.GetString("txtCOVERAGE_AMOUNT");
			capLIABILITY_EXTENDED.Text					=		objResourceMgr.GetString("cmbLIABILITY_EXTENDED");
		
		}

		private void getCustomerAddress(string strCustID)
		{	
			System.Xml.XmlDocument objXmlDoc = new System.Xml.XmlDocument(); 
			objXmlDoc.LoadXml ((new Cms.BusinessLayer.BlClient.ClsCustomer()).FillCustomerDetails(Convert.ToInt32(strCustID)).ToString());
			
			if (objXmlDoc.FirstChild.FirstChild.SelectSingleNode("CUSTOMER_ADDRESS1") != null)
				hidCustomerAddress.Value	= objXmlDoc.FirstChild.FirstChild.SelectSingleNode("CUSTOMER_ADDRESS1").InnerText;
			else
				hidCustomerAddress.Value	= "";
					
					
			if (objXmlDoc.FirstChild.FirstChild.SelectSingleNode("CUSTOMER_ADDRESS2") != null)
				hidCustomerAddress.Value	= hidCustomerAddress.Value + " " + objXmlDoc.FirstChild.FirstChild.SelectSingleNode("CUSTOMER_ADDRESS2").InnerText;


			if (objXmlDoc.FirstChild.FirstChild.SelectSingleNode("CUSTOMER_CITY") != null)
				hidCustomerCity.Value		= objXmlDoc.FirstChild.FirstChild.SelectSingleNode("CUSTOMER_CITY").InnerText;
			else
				hidCustomerCity.Value		= "";

			hidCustomerState.Value		= objXmlDoc.FirstChild.FirstChild.SelectSingleNode("CUSTOMER_STATE").InnerText;
			hidCustomerZip.Value		= objXmlDoc.FirstChild.FirstChild.SelectSingleNode("CUSTOMER_ZIP").InnerText;			
		}

		private void GetOldDataXML()
		{
			if(hidOTHER_STRUCTURE_ID.Value != "")
				hidOldData.Value = ClsOtherStructures.GetXmlForPageControls_Pol(int.Parse(GetCustomerID()),int.Parse(GetPolicyID()),int.Parse(GetPolicyVersionID()),int.Parse(Request.QueryString["DWELLINGID"].ToString()),int.Parse(hidOTHER_STRUCTURE_ID.Value));
		}

		private void SetWorkFlow()
		{
			if(base.ScreenId == "239_7_0" || base.ScreenId == "259_3_0")
			{
				myWorkFlow.IsTop = false;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				myWorkFlow.AddKeyValue("DWELLING_ID",int.Parse(Request.QueryString["DWELLINGID"].ToString()));
				myWorkFlow.WorkflowModule="POL";
				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
			}
			
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				objOtherStructures = new  ClsOtherStructures();

				//Retreiving the form values into model class object
				ClsPolicyOtherStructuresInfo objOtherStructuresInfo=new ClsPolicyOtherStructuresInfo();
				base.PopulateModelObject(objOtherStructuresInfo,hidOldData.Value);
				objOtherStructuresInfo.MODIFIED_BY=int.Parse(GetUserId());
				objOtherStructuresInfo.CalledFrom =CalledFROM.Value;
				int retVal=objOtherStructures.Delete_Pol(objOtherStructuresInfo);
			
				/*int  intCustomerID = Convert.ToInt32(hidCustomerID.Value);
				int intAppID = Convert.ToInt32(hidAppID.Value);
				int intAppVersionID = Convert.ToInt32(hidAppVersionID.Value);
				int intDwellingID = Convert.ToInt32(hidDWELLING_ID.Value);

				int retVal = objDwelling.Delete(intCustomerID,intAppID,intAppVersionID,intDwellingID);*/
				if(retVal>0)
				{
					ShowMessage(Cms.CmsWeb.ClsMessages.GetMessage("G","127"));
					hidFormSaved.Value ="5";
					base.OpenEndorsementDetails();
					SetWorkFlow();
				}
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"128") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"1";
				ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objOtherStructures!= null)
					objOtherStructures.Dispose();
			}
			
		}
		private void ShowMessage(string strMessage)
		{
			trBody.Attributes.Add("style","display:none");
			lblError.Text = strMessage;
			trError.Visible = true;
			//return;
		}

	}
}
