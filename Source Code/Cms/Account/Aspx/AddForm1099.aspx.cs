/******************************************************************************************
<Author					: -   Praveen
<Start Date				: -	
<End Date				: -	
<Description			: - 	
<Review Date			: - 
<Reviewed By			: - 	
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
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer.BlAccount;
using Cms.BusinessLayer.BlCommon;
using System.Xml; //Added by Sibin on 19 Dec 08 

namespace Cms.Account.Aspx
{
	/// <summary>
	/// 
	/// </summary>
	public class AddForm1099 : Cms.Account.AccountBase
	{
		#region Page controls declaration
		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFORM_1099_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRECIPIENT_IDENTIFICATION;//Added by Sibin on 19 Dec 08 to Encrypt Recipient Identification 
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.Controls.CmsButton btnFreeze1099;
		protected Cms.CmsWeb.Controls.CmsButton btnViewDetails;
		protected Cms.CmsWeb.Controls.CmsButton btnGenrateForm1099;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Image imgRECIPIENT_ZIP;
	
		
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;

		#endregion

		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		//protected System.Web.UI.WebControls.DropDownList cmbSUB_LOB_NAME;
		protected System.Web.UI.WebControls.Label capRECIPIENT_IDENTIFICATION;
		protected System.Web.UI.WebControls.Label capRECIPIENT_IDENTIFICATION_HID;//Added by Sibin on 19 Dec 08 to Encrypt Recipient Identification 
		protected System.Web.UI.WebControls.TextBox txtRECIPIENT_IDENTIFICATION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvRECIPIENT_IDENTIFICATION;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.WebControls.Label capRECIPIENT_NAME;
		protected System.Web.UI.WebControls.TextBox txtRECIPIENT_NAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvRECIPIENT_NAME;
		protected System.Web.UI.WebControls.Label capRECIPIENT_STREET_ADDRESS1;
		protected System.Web.UI.WebControls.TextBox txtRECIPIENT_STREET_ADDRESS1;
		protected System.Web.UI.WebControls.TextBox txtRECIPIENT_STREET_ADDRESS2;
		protected System.Web.UI.WebControls.Label capRECIPIENT_STREET_ADDRESS2;
		protected System.Web.UI.WebControls.Label capRECIPIENT_CITY;
		protected System.Web.UI.WebControls.TextBox txtRECIPIENT_CITY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvRECIPIENT_CITY;
		protected System.Web.UI.WebControls.Label capRECIPIENT_STATE;
		//Added By Raghav For Itrack Issue #4797
		protected System.Web.UI.WebControls.Label capPROCESSING_OPTION;
		protected System.Web.UI.WebControls.DropDownList cmbFED_SSN_1099;
		protected System.Web.UI.WebControls.DropDownList cmbRECIPIENT_STATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvRECIPIENT_STATE;
		protected System.Web.UI.WebControls.Label capRECIPIENT_ZIP;
		protected System.Web.UI.WebControls.TextBox txtRECIPIENT_ZIP;
		protected System.Web.UI.WebControls.HyperLink hlkRECIPIENT_ZIP;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvRECIPIENT_ZIP;
		protected System.Web.UI.WebControls.RegularExpressionValidator revRECIPIENT_ZIP;
		protected System.Web.UI.WebControls.Label capMAIL_1099_COUNTRY;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_PRIOR_CARRIER_INFO_ID;
		protected System.Web.UI.WebControls.Label capRENTS;
		protected System.Web.UI.WebControls.TextBox txtRENTS;
		protected System.Web.UI.WebControls.Label capOTHERINCOME;
		protected System.Web.UI.WebControls.TextBox txtOTHERINCOME;
		protected System.Web.UI.WebControls.Label capROYALATIES;
		protected System.Web.UI.WebControls.TextBox txtROYALATIES;
		protected System.Web.UI.WebControls.Label capFISHING_BOAT_PROCEEDS;
		protected System.Web.UI.WebControls.TextBox txtFISHING_BOAT_PROCEEDS;
		protected System.Web.UI.WebControls.Label capFEDERAL_INCOME_TAXWITHHELD;
		protected System.Web.UI.WebControls.TextBox txtFEDERAL_INCOME_TAXWITHHELD;
		protected System.Web.UI.WebControls.Label capMEDICAL_AND_HEALTH_CARE_PRODUCTS;
		protected System.Web.UI.WebControls.TextBox txtMEDICAL_AND_HEALTH_CARE_PRODUCTS;
		protected System.Web.UI.WebControls.Label capNON_EMPLOYEMENT_COMPENSATION;
		protected System.Web.UI.WebControls.TextBox txtNON_EMPLOYEMENT_COMPENSATION;
		protected System.Web.UI.WebControls.Label capSUBSTITUTE_PAYMENTS;
		protected System.Web.UI.WebControls.TextBox txtSUBSTITUTE_PAYMENTS;
		protected System.Web.UI.WebControls.Label capPAYER_MADE_DIRECT_SALES;
		protected System.Web.UI.WebControls.TextBox txtPAYER_MADE_DIRECT_SALES;
		protected System.Web.UI.WebControls.Label capCROP_INSURANCE_PROCEED;
		protected System.Web.UI.WebControls.TextBox txtCROP_INSURANCE_PROCEED;
		protected System.Web.UI.WebControls.Label capEXCESS_GOLDEN_PARACHUTE_PAYMENTS;
		protected System.Web.UI.WebControls.TextBox txtEXCESS_GOLDEN_PARACHUTE_PAYMENTS;
		protected System.Web.UI.WebControls.Label capGROSS_PROCEEDS_PAID_TO_AN_ATTORNEY;
		protected System.Web.UI.WebControls.TextBox txtGROSS_PROCEEDS_PAID_TO_AN_ATTORNEY;
		protected System.Web.UI.WebControls.Label capSTATE_TAX_WITHHELD;
		protected System.Web.UI.WebControls.TextBox txtSTATE_TAX_WITHHELD;
		protected System.Web.UI.WebControls.Label capSTATE_PAYER_STATE_NO;
		protected System.Web.UI.WebControls.TextBox txtSTATE_PAYER_STATE_NO;
		protected System.Web.UI.WebControls.Label capSTATE_INCOME;
		protected System.Web.UI.WebControls.TextBox txtSTATE_INCOME;
		protected System.Web.UI.WebControls.Label capACCOUNT_NO;
		protected System.Web.UI.WebControls.TextBox txtACCOUNT_NO;
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.RegularExpressionValidator revRENTS;
		protected System.Web.UI.WebControls.RegularExpressionValidator revROYALATIES;
		protected System.Web.UI.WebControls.RegularExpressionValidator revOTHERINCOME;
		protected System.Web.UI.WebControls.RegularExpressionValidator revFEDERAL_INCOME_TAXWITHHELD;
		protected System.Web.UI.WebControls.RegularExpressionValidator revNON_EMPLOYEMENT_COMPENSATION;
		protected System.Web.UI.WebControls.RegularExpressionValidator revSUBSTITUTE_PAYMENTS;
		protected System.Web.UI.WebControls.RegularExpressionValidator revPAYER_MADE_DIRECT_SALES;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEXCESS_GOLDEN_PARACHUTE_PAYMENTS;
		protected System.Web.UI.WebControls.RegularExpressionValidator revGROSS_PROCEEDS_PAID_TO_AN_ATTORNEY;
		protected System.Web.UI.WebControls.RegularExpressionValidator revSTATE_TAX_WITHHELD;
		protected System.Web.UI.WebControls.RegularExpressionValidator revSTATE_INCOME;
		protected System.Web.UI.WebControls.RegularExpressionValidator revRECIPIENT_IDENTIFICATION;
		protected System.Web.UI.WebControls.RegularExpressionValidator revSTATE_PAYER_STATE_NO;
		protected System.Web.UI.WebControls.RegularExpressionValidator revMEDICAL_AND_HEALTH_CARE_PRODUCTS;
		protected System.Web.UI.WebControls.RegularExpressionValidator revFISHING_BOAT_PROCEEDS;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCROP_INSURANCE_PROCEED;

		

	
		//Defining the business layer class object
		//Cms.BusinessLayer.BlApplication.ClsPriorPolicy  objPriorPolicy ;
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
			revRECIPIENT_IDENTIFICATION.ValidationExpression	= aRegExpFederalID;
			revRECIPIENT_IDENTIFICATION.ErrorMessage			= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("434");
	
			revRECIPIENT_ZIP.ValidationExpression				=  aRegExpZip;
			revRECIPIENT_ZIP.ErrorMessage						=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("24");

			revRENTS.ErrorMessage								=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");
			revRENTS.ValidationExpression						=	aRegExpCurrencyformat;

			revROYALATIES.ErrorMessage							=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");
			revROYALATIES.ValidationExpression					=	aRegExpCurrencyformat;

			revSTATE_INCOME.ErrorMessage						=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");
			revSTATE_INCOME.ValidationExpression				=	aRegExpCurrencyformat;

			revSTATE_TAX_WITHHELD.ErrorMessage					=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");
			revSTATE_TAX_WITHHELD.ValidationExpression			=	aRegExpCurrencyformat;

			revSUBSTITUTE_PAYMENTS.ErrorMessage					=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");
			revSUBSTITUTE_PAYMENTS.ValidationExpression			=	aRegExpCurrencyformat;

			revPAYER_MADE_DIRECT_SALES.ErrorMessage				=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");
			revPAYER_MADE_DIRECT_SALES.ValidationExpression		=	aRegExpCurrencyformat;

			revOTHERINCOME.ErrorMessage							=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");
			revOTHERINCOME.ValidationExpression					=	aRegExpCurrencyformat;

			revNON_EMPLOYEMENT_COMPENSATION.ErrorMessage		=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");
			revNON_EMPLOYEMENT_COMPENSATION.ValidationExpression=	aRegExpCurrencyformat;

			revGROSS_PROCEEDS_PAID_TO_AN_ATTORNEY.ErrorMessage		=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");
			revGROSS_PROCEEDS_PAID_TO_AN_ATTORNEY.ValidationExpression	=	aRegExpCurrencyformat;

			revFEDERAL_INCOME_TAXWITHHELD.ErrorMessage				=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");
			revFEDERAL_INCOME_TAXWITHHELD.ValidationExpression		=	aRegExpCurrencyformat;

			revEXCESS_GOLDEN_PARACHUTE_PAYMENTS.ErrorMessage		=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");
			revEXCESS_GOLDEN_PARACHUTE_PAYMENTS.ValidationExpression=	aRegExpCurrencyformat;

			revSTATE_PAYER_STATE_NO.ErrorMessage					=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1051");
			revSTATE_PAYER_STATE_NO.ValidationExpression			=	aRegExpCurrencyformat;

			revMEDICAL_AND_HEALTH_CARE_PRODUCTS.ErrorMessage		=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");
			revMEDICAL_AND_HEALTH_CARE_PRODUCTS.ValidationExpression	=	aRegExpCurrencyformat;

			revFISHING_BOAT_PROCEEDS.ErrorMessage				=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");
			revFISHING_BOAT_PROCEEDS.ValidationExpression		=	aRegExpCurrencyformat;

			revCROP_INSURANCE_PROCEED.ErrorMessage				=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");
			revCROP_INSURANCE_PROCEED.ValidationExpression		=	aRegExpCurrencyformat;

			
								
		}
		#endregion

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			SetControlAttributes();
			
			base.ScreenId="427_0";
			lblMessage.Visible = false;
			SetErrorMessages();
			btnFreeze1099.Attributes.Add("onClick","return confirmCommit();");
			btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");

			
			base.InitializeSecuritySettings();

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass		=	CmsButtonType.Write;
			btnReset.PermissionString	=	gstrSecurityXML;

			btnSave.CmsButtonClass		=	CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;

			btnFreeze1099.CmsButtonClass	=	CmsButtonType.Write;
			btnFreeze1099.PermissionString	=	gstrSecurityXML;

			btnViewDetails.CmsButtonClass	=	CmsButtonType.Write;
			btnViewDetails.PermissionString	=	gstrSecurityXML;

			btnGenrateForm1099.CmsButtonClass  =  CmsButtonType.Execute;
			btnGenrateForm1099.PermissionString  = gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Account.Aspx.AddForm1099" ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{
				if (Request.Params.Count != 0)
				{
					hidFORM_1099_ID.Value				= Request.Params["FORM_1099_ID"];
				}
				else
				{
					hidFORM_1099_ID.Value				= "";
				}
				GetOldDataXML();
				SetCaptions();
				PopulateComboBoxes();
                //Move Local To VSS.
				imgRECIPIENT_ZIP.Attributes.Add("style","cursor:hand");
				base.VerifyAddress(hlkRECIPIENT_ZIP, txtRECIPIENT_STREET_ADDRESS1,txtRECIPIENT_STREET_ADDRESS2
				, txtRECIPIENT_CITY, cmbRECIPIENT_STATE, txtRECIPIENT_ZIP);

				
			}
			/*if(hidFORM_1099_ID.Value.ToString().Trim() != "")
			{
				btnGenrateForm1099.Attributes.Add("Style","display:inline");
			}*/
			
		}//end pageload
		#endregion

		#region SetControlAttributes
		private void SetControlAttributes()
		{
			btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
			btnGenrateForm1099.Attributes.Add("onclick","javascript:return ShowForm1099();");
			btnViewDetails.Attributes.Add("onclick","javascript:return Show1099CheckDetails();");
			
			
		}
		#endregion

		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private Cms.Model.Account.ClsForm1099 GetFormValue()
		{
			
			//Creating the Model object for holding the New data
			Cms.Model.Account.ClsForm1099 objForm1099;
			objForm1099 = new Cms.Model.Account.ClsForm1099();

				
			if (hidFORM_1099_ID.Value.ToUpper() != "NEW")
				objForm1099.FORM_1099_ID = int.Parse(hidFORM_1099_ID.Value)	;
			else
				objForm1099.FORM_1099_ID = 0;

			//objForm1099.RECIPIENT_IDENTIFICATION=	txtRECIPIENT_IDENTIFICATION.Text;
			//Added by Sibin on 19 Dec 08 to Encrypt Recipient Identification 
			if(txtRECIPIENT_IDENTIFICATION.Text.Trim()!="")
			{
				objForm1099.RECIPIENT_IDENTIFICATION			=	Cms.BusinessLayer.BlCommon.ClsCommon.EncryptString(txtRECIPIENT_IDENTIFICATION.Text.Trim());
				txtRECIPIENT_IDENTIFICATION.Text = "";
			}
			else
				objForm1099.RECIPIENT_IDENTIFICATION			= hidRECIPIENT_IDENTIFICATION.Value;
			//Added till here

			objForm1099.RECIPIENT_NAME=	txtRECIPIENT_NAME.Text;
			objForm1099.RECIPIENT_STREET_ADDRESS1=	txtRECIPIENT_STREET_ADDRESS1.Text;
			objForm1099.RECIPIENT_STREET_ADDRESS2=	txtRECIPIENT_STREET_ADDRESS2.Text;
			objForm1099.RECIPIENT_CITY = txtRECIPIENT_CITY.Text;
			objForm1099.RECIPIENT_ZIP = txtRECIPIENT_ZIP.Text;
			//if(cmbRECIPIENT_STATE.SelectedIndex > 0)
				objForm1099.RECIPIENT_STATE = cmbRECIPIENT_STATE.SelectedValue;
			//Added By Raghav For ITrack Issue # 4797     
			objForm1099.FED_SSN_1099  =  cmbFED_SSN_1099.SelectedValue; 
			    

			if(txtRENTS.Text!="")
			{
				string strRents = txtRENTS.Text.Trim();
				objForm1099.RENTS = Double.Parse(strRents);
			}

			if(txtROYALATIES.Text!="")
			{
				string strRoyalties = txtROYALATIES.Text.Trim();
				objForm1099.ROYALATIES = Double.Parse(strRoyalties);
			}

			if(txtOTHERINCOME.Text!="")
			{
				string strOtherIncome = txtOTHERINCOME.Text.Trim();
				objForm1099.OTHERINCOME = Double.Parse(strOtherIncome);
			}

			if(txtFEDERAL_INCOME_TAXWITHHELD.Text!="")
			{
				string strtxtFEDERAL_INCOME_TAXWITHHELD = txtFEDERAL_INCOME_TAXWITHHELD.Text.Trim();
				objForm1099.FEDERAL_INCOME_TAXWITHHELD = Double.Parse(strtxtFEDERAL_INCOME_TAXWITHHELD);
			}

			if(txtNON_EMPLOYEMENT_COMPENSATION.Text!="")
			{
				string strNON_EMPLOYEMENT_COMPENSATION = txtNON_EMPLOYEMENT_COMPENSATION.Text.Trim();
				objForm1099.NON_EMPLOYEMENT_COMPENSATION = Double.Parse(strNON_EMPLOYEMENT_COMPENSATION);
			}

			if(txtSUBSTITUTE_PAYMENTS.Text!="")
			{
				string strSUBSTITUTE_PAYMENTS = txtSUBSTITUTE_PAYMENTS.Text.Trim();
				objForm1099.SUBSTITUTE_PAYMENTS = Double.Parse(strSUBSTITUTE_PAYMENTS);
			}

			if(txtPAYER_MADE_DIRECT_SALES.Text!="")
			{
				string strPAYER_MADE_DIRECT_SALES = txtPAYER_MADE_DIRECT_SALES.Text.Trim();
				objForm1099.PAYER_MADE_DIRECT_SALES = Double.Parse(strPAYER_MADE_DIRECT_SALES);
			}

			if(txtEXCESS_GOLDEN_PARACHUTE_PAYMENTS.Text!="")
			{
				string strEXCESS_GOLDEN_PARACHUTE_PAYMENTS = txtEXCESS_GOLDEN_PARACHUTE_PAYMENTS.Text.Trim();
				objForm1099.EXCESS_GOLDEN_PARACHUTE_PAYMENTS = Double.Parse(strEXCESS_GOLDEN_PARACHUTE_PAYMENTS);
			}

		
			if(txtSTATE_TAX_WITHHELD.Text!="")
			{
				string strSTATE_TAX_WITHHELD = txtSTATE_TAX_WITHHELD.Text.Trim();
				objForm1099.STATE_TAX_WITHHELD = Double.Parse(strSTATE_TAX_WITHHELD);
			}

			if(txtSTATE_INCOME.Text!="")
			{
				string strSTATE_INCOME = txtSTATE_INCOME.Text.Trim();
				objForm1099.STATE_INCOME = Double.Parse(strSTATE_INCOME);
			}

			if(txtACCOUNT_NO.Text!="")
			{
				objForm1099.ACCOUNT_NO = txtACCOUNT_NO.Text.Trim();
			}			

			if(txtFISHING_BOAT_PROCEEDS.Text!="")
			{
				objForm1099.FISHING_BOAT_PROCEEDS = Double.Parse(txtFISHING_BOAT_PROCEEDS.Text.ToString().Trim());
			}

			if(txtCROP_INSURANCE_PROCEED.Text!="")
			{
				objForm1099.CROP_INSURANCE_PROCEED = Double.Parse(txtCROP_INSURANCE_PROCEED.Text.ToString().Trim());
			}

			if(txtGROSS_PROCEEDS_PAID_TO_AN_ATTORNEY.Text!="")
			{
				objForm1099.GROSS_PROCEEDS_PAID_TO_AN_ATTORNEY = Double.Parse(txtGROSS_PROCEEDS_PAID_TO_AN_ATTORNEY.Text.ToString().Trim());
			}
			
			if(txtSTATE_PAYER_STATE_NO.Text!="")
			{
				objForm1099.STATE_PAYER_STATE_NO = Double.Parse(txtSTATE_PAYER_STATE_NO.Text.ToString().Trim());
			}

			if(txtMEDICAL_AND_HEALTH_CARE_PRODUCTS.Text!="")
			{
				objForm1099.MEDICAL_AND_HEALTH_CARE_PRODUCTS = Double.Parse(txtMEDICAL_AND_HEALTH_CARE_PRODUCTS.Text.ToString().Trim());
			}



			
			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidFORM_1099_ID.Value;
			oldXML		= hidOldData.Value;
			//Returning the model object*/

			return objForm1099;
			
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
			this.Load += new System.EventHandler(this.Page_Load);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnFreeze1099.Click += new System.EventHandler(this.btnFreeze1099_Click);
			
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
			Cms.BusinessLayer.BlAccount.ClsAccount objAccount;
			objAccount = new Cms.BusinessLayer.BlAccount.ClsAccount();
			try
			{
				int intRetVal=0;	//For retreiving the return value of business class save function
							

				//Retreiving the form values into model class object
				Cms.Model.Account.ClsForm1099 objForm1099Info = GetFormValue();

				if(hidFORM_1099_ID.Value.ToUpper().Equals("NEW")) //save case
				{
					objForm1099Info.CREATED_BY			= int.Parse(GetUserId());
					objForm1099Info.CREATED_DATETIME	= DateTime.Now;

					//Calling the add method of business layer class
					intRetVal = objAccount.AddForm1099(objForm1099Info);

					if(intRetVal>0)
					{
						hidFORM_1099_ID.Value				= objForm1099Info.FORM_1099_ID.ToString();
						lblMessage.Text						= ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value					= "1";
						hidIS_ACTIVE.Value					= "Y";

						//Generating the old XML
						GetOldDataXML();
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text						= ClsMessages.GetMessage(base.ScreenId,"18");
						hidFormSaved.Value					= "2";
					}
					else
					{
						lblMessage.Text						= ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value					= "2";
					}
					lblMessage.Visible = true;
				} // end save case
				else //UPDATE CASE
				{

					//Creating the Model object for holding the Old data
					Cms.Model.Account.ClsForm1099 objOldForm1099Info;
					objOldForm1099Info = new Cms.Model.Account.ClsForm1099();

					
					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldForm1099Info,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					//objPriorPolicyInfo.APP_ID = strRowId;
					objForm1099Info.MODIFIED_BY = int.Parse(GetUserId());
					objForm1099Info.LAST_UPDATED_DATETIME = DateTime.Now;
					
					objForm1099Info.CREATED_BY = objOldForm1099Info.CREATED_BY;
					objForm1099Info.CREATED_DATETIME = objOldForm1099Info.CREATED_DATETIME;

					objForm1099Info.IS_ACTIVE = hidIS_ACTIVE.Value;

					//Updating the record using business layer class object
					intRetVal	= objAccount.UpdateForm1099(objOldForm1099Info,objForm1099Info);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";

						//Generating the old XML
						GetOldDataXML();
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
				if(objAccount!= null)
					objAccount.Dispose();
			}
		}

		private void btnFreeze1099_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal=0;	//For retreiving the return value of business class save function
				if(hidFORM_1099_ID.Value !="" && hidFORM_1099_ID.Value.ToUpper()!="NEW")
				{
					intRetVal = ClsAccount.Freeze1099(int.Parse(hidFORM_1099_ID.Value.ToString()));
					if(intRetVal>0)
					{
						lblMessage.Text	=	"1099 has been Freezed.";//To be custmz XML
						lblMessage.Visible	=	true;
						GetOldDataXML();

					}
				}
				else
				{
					lblMessage.Text	=	"1099 has not been Saved.";//To be custmz XML
					lblMessage.Visible	=	true;
				}

							
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			
		}
		#endregion

		#region Populatecombobox
		// <summary>
		/// Polulating the combo box data using the bl object
		/// </summary>
		private void PopulateComboBoxes()
		{
			#region "Loading singleton"
			DataTable dt = Cms.CmsWeb.ClsFetcher.Country;
			dt = Cms.CmsWeb.ClsFetcher.State;
			cmbRECIPIENT_STATE.DataSource		= dt;
			cmbRECIPIENT_STATE.DataTextField	= "State_Name";
			cmbRECIPIENT_STATE.DataValueField	= "State_Id";
			cmbRECIPIENT_STATE.DataBind();

			#endregion
		}
		#endregion

		#region GetOldDataXML
		// <summary>
		/// Fetch old data from database on the basis of parameters passed to the page
		/// </summary>
		private void GetOldDataXML()
		{
			if (hidFORM_1099_ID.Value != "")
			{
				hidOldData.Value = Cms.BusinessLayer.BlAccount.ClsAccount.Get1099Info(int.Parse(hidFORM_1099_ID.Value));
				EnableDisableButtons(hidOldData.Value.ToString());
			}
			else
			{
				//If parameters not passed 
				hidOldData.Value					= "";
			}

			//Added by Sibin on 19 Dec 08 to Encrypt/Decrypt Recipient Identification 
			if(hidOldData.Value.IndexOf("NewDataSet") >= 0)
			{
				XmlDocument objxml = new XmlDocument();

				objxml.LoadXml(hidOldData.Value);

				XmlNode node = objxml.SelectSingleNode("NewDataSet");
				foreach(XmlNode nodes in node.SelectNodes("Table"))
				{
					XmlNode noder1 = nodes.SelectSingleNode("RECIPIENT_IDENTIFICATION");

					hidRECIPIENT_IDENTIFICATION.Value = noder1.InnerText;
					string strRECIPIENT_IDENTIFICATION = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(noder1.InnerText);
					if(strRECIPIENT_IDENTIFICATION != "")
					{
						string strvaln = "";
						for(int len=0; len < strRECIPIENT_IDENTIFICATION.Length-4; len++)
							strvaln += "x";

						strvaln += strRECIPIENT_IDENTIFICATION.Substring(strvaln.Length, strRECIPIENT_IDENTIFICATION.Length - strvaln.Length);
						capRECIPIENT_IDENTIFICATION_HID.Text = strvaln;
					}
					else
						capRECIPIENT_IDENTIFICATION_HID.Text = "";
				}
				objxml = null;
			}
			//Added till here
		}

		private void EnableDisableButtons(string xml)
		{
			if(xml!="")
			{
				string isCommited = ClsCommon.FetchValueFromXML("IS_COMMITED",xml);
				if(isCommited!="N")
				{
					btnFreeze1099.Enabled = false;
					btnGenrateForm1099.Visible = true;
					//btnGenrateForm1099.Enabled = true;
					btnSave.Enabled = false;
				}
				else
				{
					//btnGenrateForm1099.Enabled = false;
				}
			}
		}
		#endregion

		// <summary>
		/// Setting the captions of labels from resource files
		/// </summary>
		private void SetCaptions()
		{
			//Form 1099 RESX FOR MODEL
			//Added By Raghav For Itrack Issue #4797
			capPROCESSING_OPTION.Text               =       objResourceMgr.GetString ("cmbFED_SSN_1099");
			capRECIPIENT_CITY.Text					=		objResourceMgr.GetString("txtRECIPIENT_CITY");
			capRECIPIENT_STATE.Text					=		objResourceMgr.GetString("cmbRECIPIENT_STATE");
			capRECIPIENT_ZIP.Text					=		objResourceMgr.GetString("txtRECIPIENT_ZIP");
			capRECIPIENT_IDENTIFICATION.Text		=		objResourceMgr.GetString("txtRECIPIENT_IDENTIFICATION");
			capRECIPIENT_NAME.Text					=		objResourceMgr.GetString("txtRECIPIENT_NAME");
			capRECIPIENT_STREET_ADDRESS1.Text		=		objResourceMgr.GetString("txtRECIPIENT_STREET_ADDRESS1");
			capRECIPIENT_STREET_ADDRESS2.Text		=		objResourceMgr.GetString("txtRECIPIENT_STREET_ADDRESS2");
			capOTHERINCOME.Text						=		objResourceMgr.GetString("txtOTHERINCOME");
			capRENTS.Text							=		objResourceMgr.GetString("txtRENTS");
			capROYALATIES.Text						=		objResourceMgr.GetString("txtROYALATIES");
			capFISHING_BOAT_PROCEEDS.Text				=		objResourceMgr.GetString("txtFISHING_BOAT_PROCEEDS");
			capFEDERAL_INCOME_TAXWITHHELD.Text			=		objResourceMgr.GetString("txtFEDERAL_INCOME_TAXWITHHELD");
			capMEDICAL_AND_HEALTH_CARE_PRODUCTS.Text		=		objResourceMgr.GetString("txtMEDICAL_AND_HEALTH_CARE_PRODUCTS");
			capNON_EMPLOYEMENT_COMPENSATION.Text		=		objResourceMgr.GetString("txtNON_EMPLOYEMENT_COMPENSATION");
			capSUBSTITUTE_PAYMENTS.Text					=		objResourceMgr.GetString("txtSUBSTITUTE_PAYMENTS");
			capPAYER_MADE_DIRECT_SALES.Text			=		objResourceMgr.GetString("txtPAYER_MADE_DIRECT_SALES");
			capCROP_INSURANCE_PROCEED.Text				=		objResourceMgr.GetString("txtCROP_INSURANCE_PROCEED");
			capEXCESS_GOLDEN_PARACHUTE_PAYMENTS.Text		=		objResourceMgr.GetString("txtEXCESS_GOLDEN_PARACHUTE_PAYMENTS");
			capGROSS_PROCEEDS_PAID_TO_AN_ATTORNEY.Text	=		objResourceMgr.GetString("txtGROSS_PROCEEDS_PAID_TO_AN_ATTORNEY");
			capSTATE_TAX_WITHHELD.Text				=		objResourceMgr.GetString("txtSTATE_TAX_WITHHELD");
			capSTATE_PAYER_STATE_NO.Text					=		objResourceMgr.GetString("txtSTATE_PAYER_STATE_NO");
			capSTATE_INCOME.Text					=		objResourceMgr.GetString("txtSTATE_INCOME");
			capACCOUNT_NO.Text						=		objResourceMgr.GetString("txtACCOUNT_NO");			
			
		}

		/// <summary>
		/// Activates and deactivates  .
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			/*try
			{
				Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo ();
				objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
				objStuTransactionInfo.loggedInUserName = GetUserName();
				objPriorPolicy =  new Cms.BusinessLayer.BlApplication.ClsPriorPolicy();

				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					objStuTransactionInfo.transactionDescription = "Prior Policy Deactivated Succesfully.";
					objPriorPolicy.TransactionInfoParams = objStuTransactionInfo;
					objPriorPolicy.ActivateDeactivate(hidFORM_1099_ID.Value,"N");
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";
				}
				else
				{
					objStuTransactionInfo.transactionDescription = "Activated Succesfully.";
					objPriorPolicy.TransactionInfoParams = objStuTransactionInfo;
					objPriorPolicy.ActivateDeactivate(hidFORM_1099_ID.Value,"Y");
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";
				}
				hidFormSaved.Value			=	"1";
				
				//Generating the XML again
				GetOldDataXML();

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
				if(objPriorPolicy!= null)
					objPriorPolicy.Dispose();
			}*/
		}

		private void RegisterScript()
		{
			if (!ClientScript.IsStartupScriptRegistered("Refresh"))
			{
				string strCode = @"<script>
									RefreshWebGrid(1,1)
									</script>";

				ClientScript.RegisterStartupScript(this.GetType(),"Refresh",strCode);

			}

		}

		

	}
}
