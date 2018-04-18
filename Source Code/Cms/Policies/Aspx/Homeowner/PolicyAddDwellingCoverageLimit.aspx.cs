/******************************************************************************************
<Author				    : -   Anurag Verma
<Start Date				: -	  14/11/2005 3:13:02 PM
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
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb;
using Cms.Model;
using Cms.CmsWeb.Controls;
using Cms.Model.Policy.Homeowners;
using Cms.BusinessLayer.BlApplication.HomeOwners;
using System.Reflection;
using System.Resources;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb.WebControls;
using Cms.BusinessLayer.BlApplication;




namespace Cms.Policies.aspx.HomeOwners
{
	/// <summary>
	/// Summary description for PolicyAddDwellingCoverageLimit.
	/// </summary>
	public class PolicyAddDwellingCoverageLimit : Cms.Policies.policiesbase 
	{
		

		protected System.Web.UI.WebControls.TextBox txtDWELLING_LIMIT;
		protected System.Web.UI.WebControls.DropDownList cmbDWELLING_REPLACE_COST;
		protected System.Web.UI.WebControls.TextBox txtOTHER_STRU_LIMIT;
		protected System.Web.UI.WebControls.TextBox txtOTHER_STRU_DESC;
		protected System.Web.UI.WebControls.TextBox txtPERSONAL_PROP_LIMIT;
		protected System.Web.UI.WebControls.DropDownList cmbREPLACEMENT_COST_CONTS;
		protected System.Web.UI.WebControls.TextBox txtLOSS_OF_USE;
		protected System.Web.UI.WebControls.TextBox txtTHEFT_DEDUCTIBLE_AMT;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;

		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;

		protected System.Web.UI.WebControls.Label lblMessage;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;

		
		
		//START:*********** Local form variables *************
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		//private int	intLoggedInUserID;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label capDWELLING_LIMIT;
		protected System.Web.UI.WebControls.Label capDWELLING_REPLACE_COST;
		protected System.Web.UI.WebControls.Label capOTHER_STRU_LIMIT;
		protected System.Web.UI.WebControls.Label capOTHER_STRU_DESC;
		protected System.Web.UI.WebControls.Label capPERSONAL_PROP_LIMIT;
		protected System.Web.UI.WebControls.Label capREPLACEMENT_COST_CONTS;
		protected System.Web.UI.WebControls.Label capLOSS_OF_USE;
		protected System.Web.UI.WebControls.Label capPERSONAL_LIAB_LIMIT;
		protected System.Web.UI.WebControls.Label capMED_PAY_EACH_PERSON;
		protected System.Web.UI.WebControls.Label capALL_PERILL_DEDUCTIBLE_AMT;
		protected System.Web.UI.WebControls.Label capTHEFT_DEDUCTIBLE_AMT;
		protected Cms.CmsWeb.Controls.CmsButton Cmsbutton1;
		protected Cms.CmsWeb.Controls.CmsButton Cmsbutton2;
		protected Cms.CmsWeb.Controls.CmsButton Cmsbutton3;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomer_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidApp_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidApp_Version_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDWELLING_ID;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDWELLING_LIMIT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revOTHER_STRU_LIMIT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revPERSONAL_PROP_LIMIT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revTHEFT_DEDUCTIBLE_AMT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDWELLING_LIMIT;
		//protected System.Web.UI.WebControls.CustomValidator csvDWELLING_LIMIT;
		//Defining the business layer class object
		ClsDwellingCoverageLimit objDwellingCoverageLimit;
		protected System.Web.UI.WebControls.RegularExpressionValidator revLOSS_OF_USE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolcyType;
		protected System.Web.UI.WebControls.CompareValidator cmpDWELLING_LIMIT;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidStateId;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnmand;
		protected System.Web.UI.WebControls.DropDownList cmbMED_PAY_EACH_PERSON;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMED_PAY_EACH_PERSON;
		protected System.Web.UI.HtmlControls.HtmlGenericControl Span1;
		protected System.Web.UI.WebControls.DropDownList cmbPERSONAL_LIAB_LIMIT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPERSONAL_LIAB_LIMIT;
		protected System.Web.UI.WebControls.DropDownList cmbALL_PERILL_DEDUCTIBLE_AMT;
		protected System.Web.UI.WebControls.Label capCOVERAGEB;
		protected System.Web.UI.WebControls.Label capCOVERAGEC;
		protected System.Web.UI.WebControls.Label capCOVERAGED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPERSONAL_PROP_LIMIT;
		public string strCalledFrom="";
		public decimal replValue = 0;
		protected System.Web.UI.WebControls.Label capCOVERAGEA;
		protected System.Web.UI.WebControls.RangeValidator rngDWELLING_LIMIT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rngOTHER_STRU_LIMIT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rngLOSS_OF_USE;
		protected System.Web.UI.WebControls.RangeValidator rngPERSONAL_PROP_LIMIT;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidReplValue;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDefaultValue;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnMandatoryC;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDwellReplaceCost;
		protected System.Web.UI.HtmlControls.HtmlGenericControl Span2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPol_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPol_Version_ID;
		public decimal marketValue = 0;

		
		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			
			revDWELLING_LIMIT.ValidationExpression = aRegExpDoublePositiveZero;
			//revDWELLING_PREMIUM.ValidationExpression = cmsbase.aRegExpDoublePositiveZero;
			revOTHER_STRU_LIMIT.ValidationExpression = aRegExpDoublePositiveZero;
			revPERSONAL_PROP_LIMIT.ValidationExpression = aRegExpDoublePositiveZero;	
			revLOSS_OF_USE.ValidationExpression = aRegExpDoublePositiveZero;	
			//revPERSONAL_LIAB_LIMIT.ValidationExpression = cmsbase.aRegExpDoublePositiveZero;
			//revPERSONAL_LIAB_PREMIUM.ValidationExpression = cmsbase.aRegExpDoublePositiveZero;
			//revMED_PAY_EACH_PERSON_PREMIUM.ValidationExpression = cmsbase.aRegExpDoublePositiveZero;
			//revALL_PERILL_DEDUCTIBLE_AMT.ValidationExpression = cmsbase.aRegExpDoublePositiveZero;
			//revWIND_HAIL_DEDUCTIBLE_AMT.ValidationExpression = cmsbase.aRegExpDoublePositiveZero;
			revTHEFT_DEDUCTIBLE_AMT.ValidationExpression = aRegExpDoublePositiveZero;
			//revLOSS_OF_USE_PREMIUM.ValidationExpression = cmsbase.aRegExpDoublePositiveZero;
			//revMED_PAY_EACH_PERSON.ValidationExpression = cmsbase.aRegExpDoublePositiveZero;
			//aRegExpDouble
			//revMED_PAY_EACH_PERSON.ErrorMessage         =              Cms.CmsWeb.ClsMessages.FetchGeneralMessage("217");	
			revDWELLING_LIMIT.ErrorMessage              =              Cms.CmsWeb.ClsMessages.FetchGeneralMessage("217");	
			//revDWELLING_PREMIUM.ErrorMessage            =              Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"217");		
			revOTHER_STRU_LIMIT.ErrorMessage            =              Cms.CmsWeb.ClsMessages.FetchGeneralMessage("217");			
			revPERSONAL_PROP_LIMIT.ErrorMessage         =              Cms.CmsWeb.ClsMessages.FetchGeneralMessage("217");			
			revLOSS_OF_USE.ErrorMessage                 =              Cms.CmsWeb.ClsMessages.FetchGeneralMessage("217");			
			//revPERSONAL_LIAB_LIMIT.ErrorMessage         =              Cms.CmsWeb.ClsMessages.FetchGeneralMessage("217");			
			//revPERSONAL_LIAB_PREMIUM.ErrorMessage       =              Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"217");		
			//revMED_PAY_EACH_PERSON_PREMIUM.ErrorMessage =        Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"217");		
			//revALL_PERILL_DEDUCTIBLE_AMT.ErrorMessage   =        Cms.CmsWeb.ClsMessages.FetchGeneralMessage("217");			
			//revWIND_HAIL_DEDUCTIBLE_AMT.ErrorMessage    =        Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"217");		
			revPERSONAL_PROP_LIMIT.ErrorMessage         =        Cms.CmsWeb.ClsMessages.FetchGeneralMessage("217");			
			revLOSS_OF_USE.ErrorMessage         =        Cms.CmsWeb.ClsMessages.FetchGeneralMessage("217");			
			//revLOSS_OF_USE_PREMIUM.ErrorMessage         =        Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"217");
			revTHEFT_DEDUCTIBLE_AMT.ErrorMessage         =        Cms.CmsWeb.ClsMessages.FetchGeneralMessage("217");			


			//rfvFORM.ErrorMessage= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			//rfvCOVERAGE.ErrorMessage= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
			rfvDWELLING_LIMIT.ErrorMessage= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("488");
			rfvPERSONAL_LIAB_LIMIT.ErrorMessage= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("515");
			rfvMED_PAY_EACH_PERSON.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			//csvDWELLING_LIMIT.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("443");
			
		}
		#endregion
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			#region Setting ScreenId
			// Check from where the screen is called.
			if (Request.QueryString["CalledFrom"] != null && Request.QueryString["CalledFrom"] !="")
			{
				strCalledFrom=Request.QueryString["CalledFrom"].ToString();
				this.hidCalledFrom.Value = strCalledFrom;
			}
		
			//Setting screen Id.	
			switch (strCalledFrom) 
			{
				case "Home":
				case "HOME":
					base.ScreenId="239_3";
					break;
				case "Rental":
				case "RENTAL":
					base.ScreenId="259_3";
					break;
				default:
					base.ScreenId="977";
					break;
			}
			#endregion
			btnReset.Attributes.Add("onclick","javascript:return Reset();");
			
			//Coverage A
			txtDWELLING_LIMIT.Attributes.Add("onBlur","this.value=formatCurrency(this.value);CalculateLimits('A',this);");
			
			//Coverage B
			//txtOTHER_STRU_LIMIT.Attributes.Add("onBlur","this.value=formatCurrency(this.value);CalculateLimits('B',this);");
			
			//Coverage C
			txtPERSONAL_PROP_LIMIT.Attributes.Add("onBlur","this.value=formatCurrency(this.value);CalculateLimits('C',this);");
			
			//Coverage D
			//txtLOSS_OF_USE.Attributes.Add("onBlur","this.value=formatCurrency(this.value);CalculateLimits('D',this);");
	
			cmbDWELLING_REPLACE_COST.Attributes.Add("onChange","javascript:resetTextBox();SetHidden();");
					
			txtTHEFT_DEDUCTIBLE_AMT.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");

		
			// phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
			
			//lblMessage.Visible = false;
			SetErrorMessages();

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;

			btnSave.CmsButtonClass	=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.aspx.HomeOwners.PolicyAddDwellingCoverageLimit" ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{
				SetHiddenFields();
				GetOldDataXML();
				SetCaptions();
				
				SetWorkFlow();
				
				//Get the dwelling details
				GetDwellingDetails();
				
				//SetDefault();

				GetPolicyType();
				FillCombo();
			}

			//Check for no coverage in case of rental
			if ( strCalledFrom.ToUpper() == "RENTAL" )
			{
				btnSave.Attributes.Add("onclick","javascript:return ChkNocoverage();");
				
				//Disable Repl cost contents and Expanded Bldg repl cost drop downs
				this.cmbREPLACEMENT_COST_CONTS.Enabled = false;
				cmbREPLACEMENT_COST_CONTS.SelectedIndex = 2;

				this.cmbDWELLING_REPLACE_COST.Enabled = false;
				this.cmbDWELLING_REPLACE_COST.SelectedIndex = 2;

			}

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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void GetPolicyType()
		{
			DataSet dsCoverages = new DataSet();
			Cms.BusinessLayer.BlApplication.clsapplication 	objCovInformation = new Cms.BusinessLayer.BlApplication.clsapplication();
					
			dsCoverages=objCovInformation.GetPolicyTypeAndStateForPolicy
				(Convert.ToInt32(hidCustomer_ID.Value),
				Convert.ToInt32(hidPol_ID.Value),
				Convert.ToInt32(hidPol_Version_ID.Value));
			
			
			if(dsCoverages.Tables[0].Rows.Count>0)
			{
				hidPolcyType.Value = dsCoverages.Tables[0].Rows[0]["LOOKUP_VALUE_CODE"].ToString();
				hidStateId.Value = dsCoverages.Tables[0].Rows[0]["STATE_ID"].ToString();
			}

			decimal minValue = 0;
			decimal eightyPerc = 0;
			int defaultValue = 0;
			string message = "Message";
			int maxValue = 999999999;

			if(hidStateId.Value!=null)
			{
				EnableDisableValidators(Convert.ToInt32(hidStateId.Value),hidPolcyType.Value);

				int stateID = Convert.ToInt32(hidStateId.Value);

				switch(hidPolcyType.Value)
				{
					case "HO-2":
							
						minValue = stateID == 14 ? 50000 : 35000;
							
						if ( replValue == 0 ) replValue = minValue;
							
						//Coverage A = 80-100% of Repl value
						eightyPerc =  this.replValue * Convert.ToDecimal(0.80);

						//Min amount is default min or 80% whichever is higher
						if ( eightyPerc > minValue )
						{
							minValue = eightyPerc;
						}
						
						if ( minValue == replValue )
						{
							message = "Coverage A for HO-2 must be equal to " + minValue.ToString("#,#");
						}
						else
						{
							message = "Coverage A for HO-2 must be between " + minValue.ToString("#,#") + " and " + this.replValue.ToString("#,#");
						}

						this.hidDefaultValue.Value = this.replValue.ToString();

						rngDWELLING_LIMIT.MinimumValue = Convert.ToInt32(minValue).ToString();
						rngDWELLING_LIMIT.MaximumValue = Convert.ToInt32(this.replValue).ToString();
							
						rngDWELLING_LIMIT.ErrorMessage = message;

						break;
						
					case "HO-3":
							
						minValue = stateID == 14 ? 50000 : 40000;
							
						if ( replValue == 0 ) replValue = minValue;
						//Coverage A = 80-100% of Repl value
						eightyPerc =  this.replValue * Convert.ToDecimal(0.80);

						if ( eightyPerc > minValue )
						{
							minValue = eightyPerc;
						}
							
						if ( minValue == replValue )
						{
							message = "Coverage A for HO-3 must be equal to " + minValue.ToString("#,#");
						}
						else
						{
							message = "Coverage A for HO-3 must be between " + minValue.ToString("#,#") + " and " + this.replValue.ToString("#,#");
						}

						this.hidDefaultValue.Value = this.replValue.ToString();

						rngDWELLING_LIMIT.MinimumValue = Convert.ToInt32(minValue).ToString();
						rngDWELLING_LIMIT.MaximumValue = Convert.ToInt32(this.replValue).ToString();
							
						rngDWELLING_LIMIT.ErrorMessage = message;

						break;
					case "HO-3 Premier":
							
						minValue = 125000;
							
						if ( replValue == 0 ) replValue = minValue;

						this.hidDefaultValue.Value = replValue.ToString();

						rngDWELLING_LIMIT.MinimumValue = Convert.ToInt32(this.replValue).ToString();
						rngDWELLING_LIMIT.MaximumValue = Convert.ToInt32(this.replValue).ToString();
						rngDWELLING_LIMIT.ErrorMessage = "Coverage A for HO-3 Premier must be equal to " +  this.replValue.ToString("#,#");

						break;
					case "HO-4":
							
						capPERSONAL_PROP_LIMIT.Text="Coverage C - Contents Insurance Amount";
							
						//----------------------------------------
						//Covg. B & Covg. A should not be present.  
							
						txtDWELLING_LIMIT.Attributes.Add("style","display:none");

						//this.txtDWELLING_LIMIT.Visible = false;
						this.rfvDWELLING_LIMIT.Enabled = false;
						this.rfvPERSONAL_PROP_LIMIT.Enabled = true;
						//cmpDWELLING_LIMIT.Enabled = false;
						rngDWELLING_LIMIT.Enabled = false;
						rngDWELLING_LIMIT.MinimumValue = "0";
						rngDWELLING_LIMIT.MaximumValue = "0";

						txtOTHER_STRU_LIMIT.Attributes.Add("style","display:none");
						//this.txtOTHER_STRU_LIMIT.Visible = false;
						revOTHER_STRU_LIMIT.Enabled = false;

						this.capCOVERAGEB.Visible = true;
						this.capCOVERAGEA.Visible = true;

						//covg. D should be non-editable.
						this.txtLOSS_OF_USE.ReadOnly = true;
						//txtLOSS_OF_USE.Enabled = false;
						txtPERSONAL_PROP_LIMIT.Enabled = true;
						txtPERSONAL_PROP_LIMIT.ReadOnly = false;

						rngPERSONAL_PROP_LIMIT.Enabled = true;

						minValue = 10000;
						if ( replValue == 0 ) replValue = minValue;
						this.hidDefaultValue.Value = replValue.ToString();
							
						message = "Coverage A for HO-4 must be between " + minValue.ToString("#,#") + " and " + maxValue.ToString("#,#");
							
						this.rngPERSONAL_PROP_LIMIT.MinimumValue = Convert.ToInt32(minValue).ToString();
						this.rngPERSONAL_PROP_LIMIT.MaximumValue = maxValue.ToString();

						rngPERSONAL_PROP_LIMIT.ErrorMessage = message;
						//---------------------------------------------------

						break;
					case "HO-4 Deluxe":
							
						capPERSONAL_PROP_LIMIT.Text="Coverage C - Contents Insurance Amount";
							
						//----------------------------
						//Covg. B & Covg. A should not be present.  
						//this.txtDWELLING_LIMIT.Visible = false;
						this.rfvDWELLING_LIMIT.Enabled = false;
						this.rfvPERSONAL_PROP_LIMIT.Enabled = true;

						//cmpDWELLING_LIMIT.Enabled = false;
						rngDWELLING_LIMIT.Enabled = false;
						rngDWELLING_LIMIT.MinimumValue = "0";
						rngDWELLING_LIMIT.MaximumValue = "0";

						txtDWELLING_LIMIT.Attributes.Add("style","display:none");
							
						txtOTHER_STRU_LIMIT.Attributes.Add("style","display:none");
						//this.txtOTHER_STRU_LIMIT.Visible = false;
						revOTHER_STRU_LIMIT.Enabled = false;

						this.capCOVERAGEB.Visible = true;
						this.capCOVERAGEA.Visible = true;
							
						//covg. D should be non-editable.
						this.txtLOSS_OF_USE.ReadOnly = true;
						//txtLOSS_OF_USE.Enabled = false;
							
						txtPERSONAL_PROP_LIMIT.ReadOnly = false;
						txtPERSONAL_PROP_LIMIT.Enabled = true;
						rngPERSONAL_PROP_LIMIT.Enabled = true;
						minValue = 25000;
						if ( replValue == 0 ) replValue = minValue;

						this.hidDefaultValue.Value = replValue.ToString();
								
						message = "Coverage A for HO-4 Deluxe must be between " + minValue.ToString("#,#") + " and " + maxValue.ToString("#,#");
							
						this.rngPERSONAL_PROP_LIMIT.MinimumValue = Convert.ToInt32(minValue).ToString();
						this.rngPERSONAL_PROP_LIMIT.MaximumValue = maxValue.ToString();
							
						rngPERSONAL_PROP_LIMIT.ErrorMessage = message;
						//-----------------------------------------

						break;
					case "HO-5":
							
						minValue = 125000;
						if ( replValue == 0 ) replValue = minValue;
						this.hidDefaultValue.Value = replValue.ToString();

						rngDWELLING_LIMIT.MinimumValue = Convert.ToInt32(replValue).ToString();
						rngDWELLING_LIMIT.MaximumValue = Convert.ToInt32(this.replValue).ToString();
						rngDWELLING_LIMIT.ErrorMessage = "Coverage A for HO-5 must be equal to " + replValue.ToString("#,#");// + " and " + this.replValue.ToString("#,#");
							
						//Repl cost DDL mandatory
						this.cmbDWELLING_REPLACE_COST.Enabled = false;
						cmbDWELLING_REPLACE_COST.SelectedIndex = 1;
						this.hidDwellReplaceCost.Value = "1";

						break;
					case "HO-5 Premier":
							
				
						minValue = 125000;
						if ( replValue == 0 ) replValue = minValue;
						this.hidDefaultValue.Value = replValue.ToString();

						rngDWELLING_LIMIT.MinimumValue = Convert.ToInt32(replValue).ToString();
						rngDWELLING_LIMIT.MaximumValue = Convert.ToInt32(replValue).ToString();
						rngDWELLING_LIMIT.ErrorMessage = "Coverage A for HO-5 Premier must be equal to " + replValue.ToString("#,#");// + minValue.ToString("#,#");
							
						//Repl cost DDL mandatory
						this.cmbDWELLING_REPLACE_COST.Enabled = false;
						this.hidDwellReplaceCost.Value = "1";

						break;
					case "HO-6":
							
						capPERSONAL_PROP_LIMIT.Text="Coverage C - Contents Insurance Amount";
							
						//-------------------------------------------------------
						//No Coverage B
						txtOTHER_STRU_LIMIT.Attributes.Add("style","display:none");
						//txtOTHER_STRU_LIMIT.Visible = false;
						this.capCOVERAGEB.Visible = true;
						this.revOTHER_STRU_LIMIT.Enabled = false;

						//Coverage A & covg. D should be non editable.
						this.txtDWELLING_LIMIT.ReadOnly = true;
						this.txtLOSS_OF_USE.ReadOnly = true;

							

						rngDWELLING_LIMIT.MinimumValue = "0";
						rngDWELLING_LIMIT.MaximumValue = "0";

						this.txtPERSONAL_PROP_LIMIT.ReadOnly = false;
						txtPERSONAL_PROP_LIMIT.Enabled = true;

						//Rule of Min. coverage should be applied on Coverage C. 
						rngDWELLING_LIMIT.Enabled = false; 
						rngPERSONAL_PROP_LIMIT.Enabled = true;
						//this.cmpPERSONAL_PROP_LIMIT.ValueToCompare = this.replValue.ToString();	
							
							
							
						minValue = 15000;
						if ( replValue == 0 ) replValue = minValue;
						this.hidDefaultValue.Value = replValue.ToString();

						message = "Coverage C for HO-6 must be between " + minValue.ToString("#,#") + " and " + maxValue.ToString("#,#");
							
						this.rngPERSONAL_PROP_LIMIT.MinimumValue = Convert.ToInt32(minValue).ToString();
						this.rngPERSONAL_PROP_LIMIT.MaximumValue = maxValue.ToString();

						rngPERSONAL_PROP_LIMIT.ErrorMessage = message;
						//-------------------------------------------------------

						break;
					case "HO-6 Deluxe":
							
						capPERSONAL_PROP_LIMIT.Text="Coverage C - Contents Insurance Amount";
							
						//-----------------------------------------
						//No Coverage B
						txtOTHER_STRU_LIMIT.Attributes.Add("style","display:none");
						//txtOTHER_STRU_LIMIT.Visible = false;
						this.capCOVERAGEB.Visible = true;
						this.revOTHER_STRU_LIMIT.Enabled = false;

						//Coverage A & covg. D should be non editable.
						this.txtDWELLING_LIMIT.ReadOnly = true;
						this.txtLOSS_OF_USE.ReadOnly = true;

						

						this.txtPERSONAL_PROP_LIMIT.ReadOnly = false;
						txtPERSONAL_PROP_LIMIT.Enabled = true;
	
						////Rule of Min. coverage should be applied on Coverage C. 
						//cmpDWELLING_LIMIT.Enabled = false;
						rngDWELLING_LIMIT.Enabled = false;
						rngDWELLING_LIMIT.MinimumValue = "0";
						rngDWELLING_LIMIT.MaximumValue = "0";
						//Rule of Min. coverage should be applied on Coverage C.
						rngPERSONAL_PROP_LIMIT.Enabled = true;
						this.rfvPERSONAL_PROP_LIMIT.Enabled = true;

						//this.cmpPERSONAL_PROP_LIMIT.ValueToCompare = this.replValue.ToString();	
							 
						minValue = 25000;
						if ( replValue == 0 ) replValue = minValue;
						this.hidDefaultValue.Value = replValue.ToString();
						
						message = "Coverage C for HO-6 Deluxe must be between " + minValue.ToString("#,#") + " and " + maxValue.ToString("#,#");
							
						this.rngPERSONAL_PROP_LIMIT.MinimumValue = Convert.ToInt32(minValue).ToString();
						this.rngPERSONAL_PROP_LIMIT.MaximumValue = maxValue.ToString();

						rngPERSONAL_PROP_LIMIT.ErrorMessage = message;
						//-----------------------------------------

						break;
					case "HO-3 Repair Cost":

						cmbREPLACEMENT_COST_CONTS.SelectedIndex =2;
						cmbREPLACEMENT_COST_CONTS.Enabled =false;
							
						minValue = stateID == 14 ? 50000 : 40000;
						if ( marketValue == 0 ) marketValue = minValue;

						//minValue = Convert.ToDecimal(this.marketValue);	
						this.hidDefaultValue.Value = this.marketValue.ToString();
							
						message = "Coverage A for HO-3 Repair Cost must be equal to " + marketValue.ToString("#,#");
							
						rngDWELLING_LIMIT.MinimumValue = Convert.ToInt32(marketValue).ToString();
						rngDWELLING_LIMIT.MaximumValue = Convert.ToInt32(marketValue).ToString();
							
						rngDWELLING_LIMIT.ErrorMessage = message;

						break;
					case "HO-2 Repair Cost":
						cmbREPLACEMENT_COST_CONTS.SelectedIndex =2;
						cmbREPLACEMENT_COST_CONTS.Enabled =false;
							
						minValue = stateID == 14 ? 50000 : 15000;
						if ( marketValue == 0 ) marketValue = minValue;
						//minValue = Convert.ToDecimal(this.marketValue);	
						this.hidDefaultValue.Value = marketValue.ToString();
						
						message = "Coverage A for HO-2 Repair Cost must be equal to " + marketValue.ToString("#,#");
						
						rngDWELLING_LIMIT.MinimumValue = Convert.ToInt32(marketValue).ToString();
						rngDWELLING_LIMIT.MaximumValue = Convert.ToInt32(marketValue).ToString();

						rngDWELLING_LIMIT.ErrorMessage = message;

						break;
					
					
					case "DP-2":
							
						minValue = 30000;

						if ( replValue == 0 ) replValue = minValue;
						//Coverage A = 80-100% of Repl value
						eightyPerc =  this.replValue * Convert.ToDecimal(0.80);
							
							
						//Eighty Percent or MinValue whichever is higher
						if ( eightyPerc > minValue )
						{
							minValue = eightyPerc;
						}

						this.hidDefaultValue.Value = this.replValue.ToString();
							
						if ( minValue == replValue )
						{
							message = "Coverage A for DP-2 must be equal to " + minValue.ToString("#,#");
						}
						else
						{
							message = "Coverage A for DP-2 must be between " + minValue.ToString("#,#") + " and " + this.replValue.ToString("#,#");
						}

						rngDWELLING_LIMIT.MinimumValue = Convert.ToInt32(minValue).ToString();
						rngDWELLING_LIMIT.MaximumValue = Convert.ToInt32(this.replValue).ToString();

						rngDWELLING_LIMIT.ErrorMessage = message;

						break;
					case "DP-3":
							
						minValue = 75000;

						if ( replValue == 0 ) replValue = minValue;
							
						//Coverage A = 80-100% of Repl value
						eightyPerc =  this.replValue * Convert.ToDecimal(0.80);
							
							
						//Eighty Percent or MinValue whichever is higher
						if ( eightyPerc > minValue )
						{
							minValue = eightyPerc;
						}

						this.hidDefaultValue.Value = this.replValue.ToString();
							
						if ( minValue == replValue )
						{
							message = "Coverage A for DP-3 must be equal to " + minValue.ToString("#,#");
						}
						else
						{
							message = "Coverage A for DP-3 must be between " + minValue.ToString("#,#") + " and " + this.replValue.ToString("#,#");
						}

						rngDWELLING_LIMIT.MinimumValue = Convert.ToInt32(minValue).ToString();
						rngDWELLING_LIMIT.MaximumValue = Convert.ToInt32(this.replValue).ToString();
						rngDWELLING_LIMIT.ErrorMessage = message;

						break;
					case "DP-2 Repair Cost":
							
						minValue = 10000;
	
						if ( replValue == 0 ) replValue = minValue;
						
						defaultValue = Convert.ToInt32(replValue);
					
						this.hidDefaultValue.Value = this.replValue.ToString();
							
						if ( minValue == replValue )
						{
							message = "Coverage A for DP-2 Repair Cost must be equal to " + minValue.ToString("#,#");
						}
						else
						{
							message = "Coverage A for DP-2 Repair Cost must be between " + minValue.ToString("#,#") + " and " + this.replValue.ToString("#,#");
						}

						rngDWELLING_LIMIT.MinimumValue = Convert.ToInt32(minValue).ToString();
						rngDWELLING_LIMIT.MaximumValue = Convert.ToInt32(this.replValue).ToString();
						rngDWELLING_LIMIT.ErrorMessage = message;

						break;
					case "DP-3 Repair Cost":
							
						minValue = 75000;
						if ( replValue == 0 ) replValue = minValue;
							
						this.hidDefaultValue.Value = this.replValue.ToString();
							
						if ( minValue == replValue )
						{
							message = "Coverage A for DP-3 Repair Cost must be equal to " + minValue.ToString("#,#");
						}
						else
						{
							message = "Coverage A for DP-3 Repair Cost must be between " + minValue.ToString("#,#") + " and " + maxValue.ToString("#,#");
						}

						rngDWELLING_LIMIT.MinimumValue = Convert.ToInt32(minValue).ToString();
						rngDWELLING_LIMIT.MaximumValue = maxValue.ToString();
						rngDWELLING_LIMIT.ErrorMessage = message;


						break;
						//Sumit Chhabra:20-12-2005:New case being added for DP-3 premier for michigan state
					case "DP-3 Premier":
							
							
						minValue = 75000;
						if ( replValue == 0 ) replValue = minValue;

						this.hidDefaultValue.Value = this.replValue.ToString();

						rngDWELLING_LIMIT.MinimumValue = Convert.ToInt32(replValue).ToString();
						rngDWELLING_LIMIT.MaximumValue = Convert.ToInt32(replValue).ToString();
						rngDWELLING_LIMIT.ErrorMessage = "Coverage A for DP-3 Premier must be equal to " + this.replValue.ToString("#,#");

						break;
				}		
				
			}
		}


		/// <summary>
		/// Gets the dweliing information
		/// </summary>
		private void GetDwellingDetails()
		{
			Cms.BusinessLayer.BlApplication.ClsDwellingDetails objdwelling = new ClsDwellingDetails();

			DataSet dsDwelling = objdwelling.GetPolicyDwellingInfoByID
				(Convert.ToInt32(this.hidCustomer_ID.Value),
				Convert.ToInt32(this.hidPol_ID.Value),
				Convert.ToInt32(this.hidPol_Version_ID.Value),
				Convert.ToInt32(this.hidDWELLING_ID.Value));
			


			if ( dsDwelling.Tables[0].Rows.Count == 0 ) return;
			
			//Replacement cost
			if ( dsDwelling.Tables[0].Rows[0]["REPLACEMENT_COST"] != System.DBNull.Value )
			{
				this.replValue = Convert.ToDecimal(dsDwelling.Tables[0].Rows[0]["REPLACEMENT_COST"]);
				this.hidReplValue.Value = replValue.ToString();
			}
			
			//Market value
			if ( dsDwelling.Tables[0].Rows[0]["MARKET_VALUE"] != System.DBNull.Value )
			{
				this.marketValue = Convert.ToDecimal(dsDwelling.Tables[0].Rows[0]["MARKET_VALUE"]);

			}


		}


		private void EnableDisableValidators(int stateid,string product)
		{

			if ( stateid == 14 || stateid == 22 )
			{
				//For 4, 4D, 6 and 6D Coverage C will be main
				if ( product == "HO-6" || product == "HO-6 Deluxe" || product == "HO-4" || product == "HO-4 Deluxe")
				{
					this.rngPERSONAL_PROP_LIMIT.Enabled = true;
					this.rngDWELLING_LIMIT.Enabled = false;
					
					this.rfvPERSONAL_PROP_LIMIT.Enabled = true;
					this.rfvDWELLING_LIMIT.Enabled = false;

					this.spnMandatoryC.Visible = true;
					this.spnmand.Visible = false;

				}
				else
				{
					//Coverage A will be main coverage
					this.rngPERSONAL_PROP_LIMIT.Enabled = false;
					this.rngDWELLING_LIMIT.Enabled = true;
					this.spnMandatoryC.Visible = false;
					this.spnmand.Visible = true;	
					this.rfvPERSONAL_PROP_LIMIT.Enabled = false;
					this.rfvDWELLING_LIMIT.Enabled = true;

				}
			}
			
		

		}

		
		#region Fill Combo 
		private void FillCombo()
		{
			DataSet ds =   ClsDwellingCoverageLimit.GetLiabilityDeductiblesForPolicy(int.Parse(hidCustomer_ID.Value),int.Parse(hidPol_ID.Value),int.Parse(hidPol_Version_ID.Value));

			cmbPERSONAL_LIAB_LIMIT.DataTextField = "LIMIT_DEDUC_AMOUNT";
			cmbPERSONAL_LIAB_LIMIT.DataValueField = "LIMIT_DEDUC_AMOUNT";
			cmbPERSONAL_LIAB_LIMIT.DataSource =	  ds;
			
			cmbPERSONAL_LIAB_LIMIT.DataBind();

			//For Rental Only
			if( strCalledFrom=="RENTAL" || strCalledFrom=="Rental")
			{
				cmbPERSONAL_LIAB_LIMIT.Items.Insert(0,new ListItem("No Coverage","-1"));
				this.cmbMED_PAY_EACH_PERSON.Items.Insert(0,new ListItem("No Coverage","-1"));
			}

		

		}

		#endregion
		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsPolicyDwellingCoverageLimitInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			try
			{
				ClsPolicyDwellingCoverageLimitInfo 
				objDwellingCoverageLimitInfo = new ClsPolicyDwellingCoverageLimitInfo();


				objDwellingCoverageLimitInfo.CUSTOMER_ID=int.Parse(hidCustomer_ID.Value);
				objDwellingCoverageLimitInfo.POLICY_ID =int.Parse(hidPol_ID.Value);
				objDwellingCoverageLimitInfo.POLICY_VERSION_ID =int.Parse(hidPol_Version_ID.Value);
				objDwellingCoverageLimitInfo.DWELLING_ID=int.Parse(hidDWELLING_ID.Value);

	
				objDwellingCoverageLimitInfo.DWELLING_LIMIT=	double.Parse(txtDWELLING_LIMIT.Text.Trim() ==""?"-1":txtDWELLING_LIMIT.Text.Trim());
				objDwellingCoverageLimitInfo.DWELLING_REPLACE_COST=	cmbDWELLING_REPLACE_COST.SelectedValue;

				objDwellingCoverageLimitInfo.DWELLING_REPLACE_COST = this.hidDwellReplaceCost.Value;

				objDwellingCoverageLimitInfo.OTHER_STRU_LIMIT=	double.Parse(txtOTHER_STRU_LIMIT.Text.Trim()==""?"-1":txtOTHER_STRU_LIMIT.Text.Trim());
				objDwellingCoverageLimitInfo.OTHER_STRU_DESC=	txtOTHER_STRU_DESC.Text;
				objDwellingCoverageLimitInfo.PERSONAL_PROP_LIMIT=	double.Parse(txtPERSONAL_PROP_LIMIT.Text.Trim() ==""?"-1":txtPERSONAL_PROP_LIMIT.Text.Trim());
				objDwellingCoverageLimitInfo.REPLACEMENT_COST_CONTS=cmbREPLACEMENT_COST_CONTS.SelectedValue;
				//	objDwellingCoverageLimitInfo.LOSS_OF_USE=double.Parse(txtLOSS_OF_USE.Text==""?"-1":txtLOSS_OF_USE.Text);
			
				if ( txtLOSS_OF_USE.Text.Trim() != "" )
				{
					objDwellingCoverageLimitInfo.LOSS_OF_USE=	double.Parse(txtLOSS_OF_USE.Text.Trim() );
				}
				
			
				//objDwellingCoverageLimitInfo.PERSONAL_LIAB_LIMIT=	double.Parse(txtPERSONAL_LIAB_LIMIT.Text==""?"-1":txtPERSONAL_LIAB_LIMIT.Text);
				if(cmbPERSONAL_LIAB_LIMIT.SelectedItem!=null && cmbPERSONAL_LIAB_LIMIT.SelectedValue!="-1")
					objDwellingCoverageLimitInfo.PERSONAL_LIAB_LIMIT  = double.Parse(cmbPERSONAL_LIAB_LIMIT.SelectedValue);
				if(cmbMED_PAY_EACH_PERSON.SelectedItem!=null && cmbMED_PAY_EACH_PERSON.SelectedValue!="-1")
					objDwellingCoverageLimitInfo.MED_PAY_EACH_PERSON=	double.Parse(cmbMED_PAY_EACH_PERSON.SelectedValue);
				if(cmbALL_PERILL_DEDUCTIBLE_AMT.SelectedValue!="")
				{
					objDwellingCoverageLimitInfo.ALL_PERILL_DEDUCTIBLE_AMT=	double.Parse(cmbALL_PERILL_DEDUCTIBLE_AMT.SelectedValue);
				}
			
				if ( txtTHEFT_DEDUCTIBLE_AMT.Text.Trim() != "" )
				{
					objDwellingCoverageLimitInfo.THEFT_DEDUCTIBLE_AMT=	double.Parse(txtTHEFT_DEDUCTIBLE_AMT.Text.Trim());
				}

				//These  assignments are common to all pages.
				strFormSaved	=	hidFormSaved.Value;
				oldXML		= hidOldData.Value;
				if (hidOldData.Value != "")
				{
					strRowId=hidDWELLING_ID.Value;			
				}
				else
				{
					strRowId="NEW";				
				}			
				//Returning the model object
				return objDwellingCoverageLimitInfo;
			}
			catch(Exception ex)
			{
				throw ex;		
			}
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
				objDwellingCoverageLimit = new  ClsDwellingCoverageLimit();

				//Retreiving the form values into model class object
			
				ClsPolicyDwellingCoverageLimitInfo  objDwellingCoverageLimitInfo = GetFormValue();
				objDwellingCoverageLimitInfo.CREATED_BY = int.Parse(GetUserId());

				//if(strRowId.ToUpper().Equals("NEW"))
				if(hidOldData.Value =="" || hidOldData.Value == "0") //save case
				{
					
					//objDwellingCoverageLimit.CREATED_DATETIME = DateTime.Now;

					//Calling the add method of business layer class
					intRetVal = objDwellingCoverageLimit.Add(objDwellingCoverageLimitInfo);

					if(intRetVal>0)
					{
						hidDWELLING_ID.Value = objDwellingCoverageLimitInfo.DWELLING_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
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
					hidOldData.Value =ClsDwellingCoverageLimit.GetPolicyDwellingCoverageXml(int.Parse(hidCustomer_ID.Value),int.Parse(hidPol_ID.Value ),int.Parse(hidPol_Version_ID.Value),int.Parse(hidDWELLING_ID.Value));			

				} // end save case
				else //UPDATE CASE
				{

					//Creating the Model object for holding the Old data
					ClsPolicyDwellingCoverageLimitInfo  objOldDwellingCoverageLimitInfo;
					objOldDwellingCoverageLimitInfo = new ClsPolicyDwellingCoverageLimitInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldDwellingCoverageLimitInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objDwellingCoverageLimitInfo.DWELLING_ID = int.Parse(strRowId);
					//objDwellingCoverageLimitInfo.MODIFIED_BY = int.Parse(GetUserId());
					//objDwellingCoverageLimitInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					//objDwellingCoverageLimitInfo.IS_ACTIVE = hidIS_ACTIVE.Value;

					//Updating the record using business layer class object
					intRetVal	= objDwellingCoverageLimit.Update(objOldDwellingCoverageLimitInfo,objDwellingCoverageLimitInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
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
					hidOldData.Value =ClsDwellingCoverageLimit.GetPolicyDwellingCoverageXml(int.Parse(hidCustomer_ID.Value),int.Parse(hidPol_ID.Value ),int.Parse(hidPol_Version_ID.Value),int.Parse(hidDWELLING_ID.Value));			
				}
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
				//ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objDwellingCoverageLimit!= null)
					objDwellingCoverageLimit.Dispose();
			}
		}
		#endregion
		
		private void SetCaptions()
		{
			//		capFORM.Text						=		objResourceMgr.GetString("cmbFORM");
			//		capFORM_OTHER_DESC.Text			    =		objResourceMgr.GetString("txtFORM_OTHER_DESC");
			//		capCOVERAGE.Text					=		objResourceMgr.GetString("cmbCOVERAGE");
			//		capCOVERAGE_OTHER_DESC.Text			=		objResourceMgr.GetString("txtCOVERAGE_OTHER_DESC");
			capDWELLING_LIMIT.Text				=		objResourceMgr.GetString("txtDWELLING_LIMIT");
			//capDWELLING_PREMIUM.Text			=		objResourceMgr.GetString("txtDWELLING_PREMIUM");
			capDWELLING_REPLACE_COST.Text		=		objResourceMgr.GetString("cmbDWELLING_REPLACE_COST");
			capOTHER_STRU_LIMIT.Text			=		objResourceMgr.GetString("txtOTHER_STRU_LIMIT");
			capOTHER_STRU_DESC.Text				=		objResourceMgr.GetString("txtOTHER_STRU_DESC");
			capPERSONAL_PROP_LIMIT.Text			=		objResourceMgr.GetString("txtPERSONAL_PROP_LIMIT");
			capREPLACEMENT_COST_CONTS.Text		=		objResourceMgr.GetString("cmbREPLACEMENT_COST_CONTS");
			capLOSS_OF_USE.Text					=		objResourceMgr.GetString("txtLOSS_OF_USE");
			//capLOSS_OF_USE_PREMIUM.Text			=		objResourceMgr.GetString("txtLOSS_OF_USE_PREMIUM");
			capPERSONAL_LIAB_LIMIT.Text			=		objResourceMgr.GetString("cmbPERSONAL_LIAB_LIMIT");
			//capPERSONAL_LIAB_PREMIUM.Text		=		objResourceMgr.GetString("txtPERSONAL_LIAB_PREMIUM");
			capMED_PAY_EACH_PERSON.Text			=		objResourceMgr.GetString("cmbMED_PAY_EACH_PERSON");
			//capMED_PAY_EACH_PERSON_PREMIUM.Text	=		objResourceMgr.GetString("txtMED_PAY_EACH_PERSON_PREMIUM");
			//capINFLATION_GUARD.Text				=		objResourceMgr.GetString("txtINFLATION_GUARD");
			capALL_PERILL_DEDUCTIBLE_AMT.Text	=		objResourceMgr.GetString("cmbALL_PERILL_DEDUCTIBLE_AMT");
			//capWIND_HAIL_DEDUCTIBLE_AMT.Text	=		objResourceMgr.GetString("txtWIND_HAIL_DEDUCTIBLE_AMT");
			capTHEFT_DEDUCTIBLE_AMT.Text		=		objResourceMgr.GetString("txtTHEFT_DEDUCTIBLE_AMT");
		}

		private void GetOldDataXML()
		{
			
			this.hidOldData.Value  = ClsDwellingCoverageLimit.GetPolicyDwellingCoverageXml(int.Parse(hidCustomer_ID.Value),int.Parse(hidPol_ID.Value ),int.Parse(hidPol_Version_ID.Value),int.Parse(hidDWELLING_ID.Value));			

			//Add/Mode
			if ( this.hidOldData.Value == "0" || this.hidOldData.Value == "")
			{
				lblMessage.Visible = true;
				this.lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("613");	
			}
			else
			{
				lblMessage.Visible = false;
			}


		}

		private void SetDefault(DataSet ds)
		{
			
		}

		/// <summary>
		/// For setting values of the hidden field from the Query String.
		/// </summary>
		private void SetHiddenFields()
		{
			//cms/policies/aspx/Homeowner/PolicyAddDwellingCoverageLimit.aspx?CustomerID=770&PolID=18&Pol_Version_ID=1&DWELLINGID=1&CalledFrom=Rental&DWELLING_ID=1&transferdata=
			if (Request.QueryString["CustomerID"] != null && Request.QueryString["CustomerID"].ToString() != "")
			{ 						
				hidCustomer_ID.Value=Request.QueryString["CustomerID"].ToString(); 
			}				
			if (Request.QueryString["PolID"]!=null && Request.QueryString["PolID"].ToString() != "")
			{ 						
				hidPol_ID.Value =Request.QueryString["PolID"].ToString();
			}				
			if (Request.QueryString["Pol_Version_ID"]!=null && Request.QueryString["Pol_Version_ID"].ToString() != "")
			{ 					
				hidPol_Version_ID.Value=Request.QueryString["Pol_Version_ID"].ToString();
			}
			if (Request.QueryString["DWELLINGID"]!=null && Request.QueryString["DWELLINGID"].ToString() != "")
			{ 					
				hidDWELLING_ID.Value =Request.QueryString["DWELLINGID"].ToString();
			}		
		}

		private void SetWorkFlow()
		{
			if(base.ScreenId	==	"259_3" || base.ScreenId == "159_5")
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",hidCustomer_ID.Value);
				myWorkFlow.AddKeyValue("POLICY_ID",hidPol_ID.Value);
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",hidPol_Version_ID.Value);
				myWorkFlow.AddKeyValue("DWELLING_ID",hidDWELLING_ID.Value);
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







