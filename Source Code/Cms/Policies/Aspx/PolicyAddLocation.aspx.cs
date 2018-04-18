/******************************************************************************************
<Author				: -   Anurag verma
<Start Date				: -	10/11/2005 4:51:41 PM
<End Date				: -	
<Description				: - 	Show the Add Page for Location.
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 5th Apr,06
<Modified By			: -	Swastika Gaur
<Purpose				: - Added Delete button
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
using Cms.CustomException;
using Cms.CmsWeb.WebControls;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb.Controls;
using Cms.Model.Policy;
using Cms.BusinessLayer.BlApplication;
using Cms.CmsWeb;
using Cms.BusinessLayer.BlCommon;

namespace Cms.Policies.Aspx
{
	/// <summary>
	/// Summary description for PolicyAddLocation.
	/// </summary>
	public class PolicyAddLocation : Cms.Policies.policiesbase
	{
		#region Page controls declaration
		protected System.Web.UI.WebControls.TextBox txtLOC_NUM;
		protected System.Web.UI.WebControls.DropDownList cmbIS_PRIMARY;
		protected System.Web.UI.WebControls.TextBox txtLOC_ADD1;
		protected System.Web.UI.WebControls.TextBox txtLOC_ADD2;
		protected System.Web.UI.WebControls.TextBox txtLOC_CITY;
		protected System.Web.UI.WebControls.TextBox txtLOC_COUNTY;
		protected System.Web.UI.WebControls.DropDownList cmbLOC_STATE;
		protected System.Web.UI.WebControls.TextBox txtLOC_ZIP;
		protected System.Web.UI.WebControls.DropDownList cmbLOC_COUNTRY;
		//Nov11,2005:Sumit Chhabra:Control commented as it will not be used here
		//protected System.Web.UI.WebControls.DropDownList cmbNAMED_PERILL;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLocationCode;

		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlImage imgSelect;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOC_NUM;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOC_ADD1;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOC_ADD2;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOC_COUNTRY;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOC_STATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOC_ZIP;
		protected System.Web.UI.WebControls.RangeValidator rngLOC_NUM;
		protected System.Web.UI.WebControls.Label lblWARNING;
		

		protected System.Web.UI.WebControls.RegularExpressionValidator revLOC_NUM;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revLOC_CITY;
		protected System.Web.UI.WebControls.RegularExpressionValidator revLOC_ZIP;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;


		protected System.Web.UI.WebControls.DropDownList cmbRENTED_WEEKLY;
		protected System.Web.UI.WebControls.Image imgZipLookup;
		protected System.Web.UI.WebControls.HyperLink hlkZipLookup;
		protected System.Web.UI.WebControls.DropDownList cmbREPORT_STATUS;
		protected System.Web.UI.WebControls.Label capREPORT_STATUS; 
	
		//Added by Raghav On 07/17/2008
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidGenerallocation;

		#endregion

		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		// private int	intLoggedInUserID;
		public string primaryKeyValues="";
		protected System.Web.UI.WebControls.Label capLOC_NUM;
		protected System.Web.UI.WebControls.Label capDESCRIPTION;
		protected System.Web.UI.WebControls.Label capIS_PRIMARY;
		protected System.Web.UI.WebControls.Label capLOC_ADD1;
		protected System.Web.UI.WebControls.Label capLOC_ADD2;
		protected System.Web.UI.WebControls.Label capLOC_CITY;
		protected System.Web.UI.WebControls.Label capLOC_COUNTRY;
		protected System.Web.UI.WebControls.Label capLOC_STATE;
		protected System.Web.UI.WebControls.Label capLOC_ZIP;
		protected System.Web.UI.WebControls.Label capLOC_COUNTY;
		//protected System.Web.UI.WebControls.Label capDEDUCTIBLE;
		//protected System.Web.UI.WebControls.Label capNAMED_PERILL;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOCATION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOL_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOL_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.WebControls.Label Label1;
		protected Cms.CmsWeb.Controls.CmsButton btnPullCustomerAddress;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOC_CITY;
		protected System.Web.UI.WebControls.TextBox txtDESCRIPTION;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSubmitZip;
		protected System.Web.UI.WebControls.CustomValidator csvDESCRIPTION;
		//Defining the business layer class object
		ClsLocation  objLocation ;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_TYPE;
		protected System.Web.UI.WebControls.Label capLOCATION_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbLOCATION_TYPE;
		protected System.Web.UI.WebControls.TextBox txtWEEKS_RENTED;
		protected System.Web.UI.WebControls.CompareValidator cpvWEEKS_RENTED;
		protected System.Web.UI.WebControls.CustomValidator csvLOC_ZIP;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOCATION_TYPE;
		//protected System.Web.UI.WebControls.DropDownList cmbDEDUCTIBLE;
		protected System.Web.UI.WebControls.Label capLOSSREPORT_ORDER;
		protected System.Web.UI.WebControls.DropDownList cmbLOSSREPORT_ORDER;
		protected System.Web.UI.WebControls.Label capLOSSREPORT_DATETIME;
		protected System.Web.UI.WebControls.TextBox txtLOSSREPORT_DATETIME;
		protected System.Web.UI.WebControls.HyperLink hlkCalandarDate2;
		protected System.Web.UI.WebControls.RegularExpressionValidator revLOSSREPORT_DATETIME;
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
			rfvLOC_NUM.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
			this.rngLOC_NUM.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"519");
			//rfvDESCRIPTION.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"134");
			//rfvIS_PRIMARY.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"135");
			rfvLOC_ADD1.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"787");
			//rfvLOC_CITY.Enabled = false;
			rfvLOC_CITY.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"56");
			//rfvLOC_COUNTRY.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"33");
			//rfvLOC_STATE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"35");
			rfvLOC_ZIP.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"37");
			//rfvLOC_COUNTY.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"136");
			//rfvPHONE_NUMBER.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"137");
			//rfvFAX_NUMBER.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"138");
			//rfvNAMED_PERILL.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"139");
			//rfvDeductible.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"204");

			revLOC_NUM.ValidationExpression		= aRegExpInteger;
			
			//revLOC_CITY.ValidationExpression	= aRegExpClientName;
            revLOC_ZIP.ValidationExpression = aRegExpZipBrazil;
			//revPHONE_NUMBER.ValidationExpression= aRegExpPhone;
			//revFAX_NUMBER.ValidationExpression	= aRegExpPhone;

			//-------Commented by mohit on 28/09/2005--------------  
			//<Gaurav > 1 June 2005 ; START: This field should be Numeric only; BUG No <556>
			//revDeductible.ValidationExpression		= aRegExpInteger;
			//revDeductible.ErrorMessage				= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"102");
			//-----------------End----------------

			//------------------------- Added by mohit on 28/09/2005---------
			//revDeductible.ValidationExpression = aRegExpDoublePositiveNonZero;
			//revDeductible.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage("G","216");
			//-------------------------------End-------------------------------
			//<Gaurav > 1 June 2005 ; ENDee: This field should be Numeric only; BUG No <556>
			revLOC_NUM.ErrorMessage				= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"102");
			//revLOC_CITY.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"39");
            revLOC_ZIP.ErrorMessage = "Postal Code should be in '######' format.";
			//revPHONE_NUMBER.ErrorMessage		= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"14");
			//revFAX_NUMBER.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"15");

			csvDESCRIPTION.ErrorMessage                           = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("442");  
			lblWARNING.Text =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("800");
			csvLOC_ZIP.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("898");
			revLOSSREPORT_DATETIME.ValidationExpression		=	aRegExpDate;


		}
		#endregion
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			Ajax.Utility.RegisterTypeForAjax(typeof(PolicyAddLocation)); 
			
			//Added By Raghav on 07/17/2008		
			ClsLocation objLocation = new ClsLocation();
			
			//this.txtDEDUCTIBLE.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");
			this.cmbLOC_COUNTRY.SelectedIndex = int.Parse(aCountry);

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
					//base.ScreenId="233_0";
					base.ScreenId="238_0";
					break;
				case "Rental":
				case "RENTAL":
					base.ScreenId="258_0";
					break;			
				default:
					base.ScreenId="752";
					break;
			}
			#endregion

			
			this.cmbLOCATION_TYPE.Attributes.Add("onchange","javascript:ShowHidePrimary();"); 
			this.cmbRENTED_WEEKLY.Attributes.Add("onchange","javascript:ShowHideRentedWeekly();"); 
			// Added by Swarup on 30-mar-2007
			//imgZipLookup.Attributes.Add("style","cursor:hand");
            //base.VerifyAddress(hlkZipLookup, txtLOC_ADD1,txtLOC_ADD2
            //    , txtLOC_CITY, cmbLOC_STATE, txtLOC_ZIP);
			hlkCalandarDate2.Attributes.Add("OnClick","fPopCalendar(document.POL_LOCATIONS.txtLOSSREPORT_DATETIME,document.POL_LOCATIONS.txtLOSSREPORT_DATETIME)");  	
			
			//Added by Manoj Rathore on 29 Jun. 2009 Itrack # 6029			
			//txtLOC_ZIP.Attributes.Add("OnBlur","javascript:GetCounty();");

			// phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
			
			lblMessage.Visible = false;
			lblDelete.Visible = false;
			SetErrorMessages();

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass		= CmsButtonType.Write;
			btnReset.PermissionString	= gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass	= CmsButtonType.Write;
			btnActivateDeactivate.PermissionString	= gstrSecurityXML;

			btnSave.CmsButtonClass		= CmsButtonType.Write;
			btnSave.PermissionString	= gstrSecurityXML;
			
			btnPullCustomerAddress.CmsButtonClass	= CmsButtonType.Write;
			btnPullCustomerAddress.PermissionString = gstrSecurityXML;

			btnDelete.CmsButtonClass	= CmsButtonType.Delete;
			btnDelete.PermissionString	= gstrSecurityXML;


			RequiredPullCustAddWithCounty(txtLOC_ADD1, txtLOC_ADD2, txtLOC_CITY
				, cmbLOC_COUNTRY, cmbLOC_STATE, txtLOC_ZIP,txtLOC_COUNTY,null, btnPullCustomerAddress);
			
			//			if ( this.hidSubmitZip.Value != "" )
			//			{
			//				string county = Cms.BusinessLayer.BlCommon.ClsCommon.GetCountyForZip(hidSubmitZip.Value);
			//				this.txtLOC_COUNTY.Text = county;
			//				return;
			//			}

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.PolicyAddLocation" ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{
				GetQueryString();
				btnReset.Attributes.Add("onclick","javascript:ResetForm('" + Page.Controls[0].ID + "' );ShowHidePrimary(); return false;");
				btnSave.Attributes.Add("onclick","javascript:return Validate();");
				string url = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL();
				imgSelect.Attributes.Add("onclick","javascript:OpenLookup('" + url + "','ZIP','COUNTY','txtLOC_ZIP','txtLOC_COUNTY','COUNTY','County')"); //Changed from OpenLookup('" + url + "','','COUNTY','','txtLOC_COUNTY','COUNTY','County') by Charles on 7-Sep-09 for Itrack 6296
				//Get policy type and state
				ClsDwellingDetails objDwelling = new ClsDwellingDetails();
				//DataSet dtAppType=objDwelling.GetPolicyStateForPolicy(GetCustomerID() ,GetPolicyID (),GetPolicyVersionID());
				DataSet dtAppType=objDwelling.GetPolicyStateForPolicy( hidCUSTOMER_ID.Value,hidPOL_ID.Value,hidPOL_VERSION_ID.Value);
				hidPOLICY_TYPE.Value=dtAppType.Tables[0].Rows[0]["POLICY_TYPE_ID"].ToString();

				
				objLocation = new ClsLocation();				
				GetOldDataXML();
				SetCaptions();
				FillCombo();
				
				#region "Loading singleton"
				DataTable dt = Cms.CmsWeb.ClsFetcher.Country;
				cmbLOC_COUNTRY.DataSource		= dt;
				cmbLOC_COUNTRY.DataTextField	= "Country_Name";
				cmbLOC_COUNTRY.DataValueField	= "Country_Id";
				cmbLOC_COUNTRY.DataBind();
				//	cmbLOC_COUNTRY.Items.Insert(0,"");

				if (strCalledFrom.ToUpper() == "RENTAL") //If check added by Charles on 8-Dec-09 for Itrack 6818
				{
					// Location state will be same as that of the Plicy state : Gen Iss #3260
					cmbLOC_STATE.DataSource		=ClsLocation.GetPolStateNameOnStateID(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOL_ID.Value),int.Parse(hidPOL_VERSION_ID.Value));
					cmbLOC_STATE.DataTextField	= "STATE_NAME";
					cmbLOC_STATE.DataValueField	= "STATE_ID";
					cmbLOC_STATE.DataBind();
				}
				else //Added by Charles on 8-Dec-09 for Itrack 6818
				{
					DataSet dsState = ClsGeneralInformation.GetStateNameId("1");
					cmbLOC_STATE.DataSource			= dsState;
					cmbLOC_STATE.DataTextField		= "STATE_NAME";
					cmbLOC_STATE.DataValueField		= "STATE_ID";
					cmbLOC_STATE.DataBind();
					cmbLOC_STATE.Items.Insert(0,new ListItem("",""));
				}//Added till here
		
				//Added by Mohit Agarwal 30-Oct-2007
				cmbLOSSREPORT_ORDER.DataSource		= Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
				cmbLOSSREPORT_ORDER.DataTextField	= "LookupDesc";
				cmbLOSSREPORT_ORDER.DataValueField	= "LookupID";
				cmbLOSSREPORT_ORDER.DataBind();
				cmbLOSSREPORT_ORDER.Items.Insert(0,"");
				#endregion//Loading singleton

				#region Set Workflow Control
				SetWorkflow();
				#endregion

				if(hidOldData.Value=="" || hidOldData.Value=="0")
				{
					int NewLocationNumber;
					//txtLOC_NUM.Text=objLocation.LocationNumber(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidAPP_ID.Value),int.Parse(hidAPP_VERSION_ID.Value));
					NewLocationNumber=int.Parse(objLocation.PolicyLocationNumber(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOL_ID.Value),int.Parse(hidPOL_VERSION_ID.Value)));
					if ( NewLocationNumber == -1 )
					{
						lblMessage.Visible = true;
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(this.ScreenId,"521");
						return;
					}
					txtLOC_NUM.Text=NewLocationNumber.ToString();
					hidLocationCode.Value=txtLOC_NUM.Text;
				}
			}
			else
			{
				if ( this.hidSubmitZip.Value != "" )
				{
					string county = Cms.BusinessLayer.BlCommon.ClsCommon.GetCountyForZip(hidSubmitZip.Value);
					this.txtLOC_COUNTY.Text = county;
					hidSubmitZip.Value = "";
					hidFormSaved.Value = "3";
				}
			}

			//Added by Raghav On 07/17/2008	
			if(hidCalledFrom.Value == "Home" || hidCalledFrom.Value == "HOME" || hidCalledFrom.Value == "Rental" || hidCalledFrom.Value == "RENTAL")
			{
				int retVal= objLocation.GeneralLocation (int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOL_ID.Value),int.Parse(hidPOL_VERSION_ID.Value),"POL");
				hidGenerallocation.Value= retVal.ToString();
			}
		}


		private void FillCombo()
		{
			if (hidCalledFrom.Value.ToUpper().Equals("RENTAL")) 
			{
				cmbLOCATION_TYPE.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("REN_LO");
			}
			else
			{
				cmbLOCATION_TYPE.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("LOCTYP");
			}
			cmbLOCATION_TYPE.DataTextField = "LookupDesc";
			cmbLOCATION_TYPE.DataValueField = "LookupID";
			cmbLOCATION_TYPE.DataBind();
			cmbLOCATION_TYPE.Items.Insert(0,new ListItem("",""));
			cmbLOCATION_TYPE.SelectedIndex=0;


			//cmbDESCRIPTION.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("LOBCD");
			//cmbDESCRIPTION.DataTextField="LookupDesc";
			//cmbDESCRIPTION.DataValueField="LookupID";
			//cmbDESCRIPTION.DataBind();
			cmbREPORT_STATUS.Items.Clear();
			ListItem list = new ListItem("", "0");
			cmbREPORT_STATUS.Items.Add(list);
			list = new ListItem("Clear", "C");
			cmbREPORT_STATUS.Items.Add(list);
			list = new ListItem("Non Clear", "V");
			cmbREPORT_STATUS.Items.Add(list);
			list = new ListItem("Not Found", "N");
			cmbREPORT_STATUS.Items.Add(list);
			list = new ListItem("Error/Reject", "E");
			cmbREPORT_STATUS.Items.Add(list);

		}


		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsPolicyLocationInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsPolicyLocationInfo objLocationInfo;
			objLocationInfo = new ClsPolicyLocationInfo();
		
			objLocationInfo.POLICY_ID = int.Parse(hidPOL_ID.Value);
			objLocationInfo.CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);
			objLocationInfo.POLICY_VERSION_ID = int.Parse(hidPOL_VERSION_ID.Value);

			if(txtLOC_NUM.Text.Trim()!="")
				objLocationInfo.LOC_NUM=	int.Parse(txtLOC_NUM.Text);			
			
			if(txtLOC_ADD1.Text.Trim()!="")
				objLocationInfo.LOC_ADD1=	txtLOC_ADD1.Text;
			
			if(txtLOC_ADD2.Text.Trim()!="")
				objLocationInfo.LOC_ADD2=	txtLOC_ADD2.Text;
			
			if(txtLOC_CITY.Text.Trim()!="")
				objLocationInfo.LOC_CITY=	txtLOC_CITY.Text;
			
			if(txtLOC_COUNTY.Text.Trim()!="")
				objLocationInfo.LOC_COUNTY=	txtLOC_COUNTY.Text.Trim();//Added Trim by Charles on 7-Sep-09 for Itrack 6296
			
			objLocationInfo.LOC_STATE=	cmbLOC_STATE.SelectedValue;
			objLocationInfo.LOC_ZIP=	txtLOC_ZIP.Text.Trim();//Added Trim by Charles on 7-Sep-09 for Itrack 6296
			if (cmbLOC_COUNTRY.SelectedItem != null)
				objLocationInfo.LOC_COUNTRY=	cmbLOC_COUNTRY.SelectedItem.Value ;
			
			objLocationInfo.DESCRIPTION=	txtDESCRIPTION.Text.Trim();

			if(cmbLOSSREPORT_ORDER.SelectedValue != "")
				objLocationInfo.LOSSREPORT_ORDER = int.Parse(cmbLOSSREPORT_ORDER.SelectedValue);

			if(txtLOSSREPORT_DATETIME.Text.Trim() != "")
				objLocationInfo.LOSSREPORT_DATETIME = DateTime.Parse(txtLOSSREPORT_DATETIME.Text.Trim());
		
			if(cmbREPORT_STATUS.SelectedValue != "")
				objLocationInfo.REPORT_STATUS = cmbREPORT_STATUS.SelectedValue;


			if(cmbLOCATION_TYPE.SelectedIndex>0)
			{
				objLocationInfo.LOCATION_TYPE=Convert.ToInt32(cmbLOCATION_TYPE.SelectedValue);
				if(objLocationInfo.LOCATION_TYPE == 11813 || objLocationInfo.LOCATION_TYPE == 11814)
				{
					if(cmbIS_PRIMARY.SelectedValue.Trim()!="")
						objLocationInfo.IS_PRIMARY=	cmbIS_PRIMARY.SelectedValue;
				}
				else if(objLocationInfo.LOCATION_TYPE == 11849)//Seasonal/Rental
				{
					if (cmbRENTED_WEEKLY.SelectedIndex > 0)
						objLocationInfo.RENTED_WEEKLY	=   cmbRENTED_WEEKLY.SelectedValue;
				
					if (cmbRENTED_WEEKLY.SelectedIndex != 2)//Blank it in case val <> yes
						txtWEEKS_RENTED.Text			= "";
				
					objLocationInfo.WEEKS_RENTED	= txtWEEKS_RENTED.Text;
				}
				else
				{
					objLocationInfo.IS_PRIMARY = "";

					txtWEEKS_RENTED.Text			= "";
					objLocationInfo.WEEKS_RENTED	= txtWEEKS_RENTED.Text;

					cmbRENTED_WEEKLY.SelectedIndex	= 0;
					objLocationInfo.RENTED_WEEKLY	= cmbRENTED_WEEKLY.SelectedValue;
				}
			}
			else
			{
				objLocationInfo.IS_PRIMARY		= "";
				
				txtWEEKS_RENTED.Text			= "";
				objLocationInfo.WEEKS_RENTED	= txtWEEKS_RENTED.Text;

				cmbRENTED_WEEKLY.SelectedIndex	= 0;
				objLocationInfo.RENTED_WEEKLY	= cmbRENTED_WEEKLY.SelectedValue;
			}	 
				 


			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidLOCATION_ID.Value;
			oldXML		= hidOldData.Value;
			//Returning the model object

			return objLocationInfo;
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
				objLocation = new  ClsLocation();

				//Retreiving the form values into model class object
				ClsPolicyLocationInfo  objLocationInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objLocationInfo.CREATED_BY = int.Parse(GetUserId());
					objLocationInfo.CREATED_DATETIME = DateTime.Now;

					//Calling the add method of business layer class
					intRetVal = objLocation.Add(objLocationInfo);

					if(intRetVal>0)
					{
						hidLOCATION_ID.Value = objLocationInfo.LOCATION_ID.ToString();
						primaryKeyValues=hidLOCATION_ID.Value  + "^" + hidCUSTOMER_ID.Value + "^" + hidPOL_ID.Value + "^" + hidPOL_VERSION_ID.Value;  
						lblMessage.Text			=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidIS_ACTIVE.Value = "Y";
						GetOldDataXML();
						SetWorkflow();

						//Showing the endorsement popup window
						base.OpenEndorsementDetails();
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"165");
						hidFormSaved.Value			=		"2";
					}
					else
					{
						base.OpenEndorsementDetails();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value			=	"2";
					}
					lblMessage.Visible = true;
				} // end save case
				else //UPDATE CASE
				{

					//Creating the Model object for holding the Old data
					ClsPolicyLocationInfo objOldLocationInfo;
					objOldLocationInfo = new ClsPolicyLocationInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldLocationInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objLocationInfo.LOCATION_ID = int.Parse(strRowId);
					objLocationInfo.MODIFIED_BY = int.Parse(GetUserId());
					objLocationInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					objLocationInfo.IS_ACTIVE = hidIS_ACTIVE.Value;

					//Updating the record using business layer class object
					intRetVal	= objLocation.Update(objOldLocationInfo,objLocationInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						primaryKeyValues=hidLOCATION_ID.Value  + "^" + hidCUSTOMER_ID.Value + "^" + hidPOL_ID.Value + "^" + hidPOL_VERSION_ID.Value;  
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						GetOldDataXML();
						SetWorkflow();

						//Showing the endorsement popup window
						base.OpenEndorsementDetails();
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"165");
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
				//ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objLocation!= null)
					objLocation.Dispose();
			}
		}


		#region ActivateDeactivate
		/// <summary>
		/// Activates and deactivates  .
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			int locationID = int.Parse(this.hidLOCATION_ID.Value);
			int polID = int.Parse(hidPOL_ID.Value);
			int polVersionID = int.Parse(hidPOL_VERSION_ID.Value);
			int customerID = int.Parse(hidCUSTOMER_ID.Value);
			int retVal = 0;
            int modifiedby = int.Parse(GetUserId());

			try
			{
				Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo ();
				objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
				objStuTransactionInfo.loggedInUserName = GetUserName();
				objLocation =  new ClsLocation();

				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					objStuTransactionInfo.transactionDescription = "Location Deactivated Succesfully.";
					objLocation.TransactionInfoParams = objStuTransactionInfo;
				

					//objLocation.ActivateDeactivate(hidLOCATION_ID.Value, "N");

                    retVal = objLocation.ActivateDeactivatePolicy(customerID, polID, polVersionID, locationID, "N", modifiedby);
					
					if ( retVal == -1 )
					{
						lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"1");
						//lblMessage.Text = "This location cannot be deactivated as it is being used in Dwelling Information.";
						return;
					}
					
					if ( retVal == -2 )
					{
						lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"2");
						//lblMessage.Text = "This location cannot be deactivated as it is being used in Subjects of insurance.";
						return;
					}
			
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";
					base.OpenEndorsementDetails();
				}
				else
				{
					objStuTransactionInfo.transactionDescription = "Activated Succesfully.";
					objLocation.TransactionInfoParams = objStuTransactionInfo;
					
					//objLocation.ActivateDeactivate(hidLOCATION_ID.Value, "Y");

                    retVal = objLocation.ActivateDeactivatePolicy(customerID, polID, polVersionID, locationID, "Y", modifiedby);
                    
					//Added By Raghav On 07/17/2008
					if(hidCalledFrom.Value == "Home" || hidCalledFrom.Value == "HOME" || hidCalledFrom.Value == "Rental" || hidCalledFrom.Value == "RENTAL")
					{
						if ( retVal == 0 )
						{
							lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"5");
							//lblMessage.Text = "This location cannot be deactivated as it is being used in Subjects of insurance.";
							return;
						}
					}
					
					if(retVal>0)
					base.OpenEndorsementDetails();
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";
				}
				hidFormSaved.Value			=	"0";
				//hidFormSaved.Value			=	"1";
				primaryKeyValues = hidLOCATION_ID.Value  + "^" + hidCUSTOMER_ID.Value + "^" + hidPOL_ID.Value + "^" + hidPOL_VERSION_ID.Value;  
				ClientScript.RegisterStartupScript(this.GetType(),"REFRESHGRID","<script>RefreshWebGrid('1','" + primaryKeyValues + "',true);</script>");

				//Generating the XML again
				GetOldDataXML();

				//Showing the endorsement popup window
				base.OpenEndorsementDetails();

			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21")+ " - " + ex.Message + " Try again!";
				ExceptionManager.Publish(ex);
			}
			finally
			{
				lblMessage.Visible = true;
				if(objLocation != null)
					objLocation.Dispose();
			}
		}
		#endregion

		#endregion

		/// <summary>
		/// Show the caption of labels from resource file
		/// </summary>
		private void SetCaptions()
		{
			capLOC_NUM.Text			=		objResourceMgr.GetString("txtLOC_NUM");
			capIS_PRIMARY.Text		=		objResourceMgr.GetString("cmbIS_PRIMARY");
			capLOC_ADD1.Text		=		objResourceMgr.GetString("txtLOC_ADD1");
			capLOC_ADD2.Text		=		objResourceMgr.GetString("txtLOC_ADD2");
			capLOC_CITY.Text		=		objResourceMgr.GetString("txtLOC_CITY");
			capLOC_COUNTY.Text		=		objResourceMgr.GetString("txtLOC_COUNTY");
			capLOC_STATE.Text		=		objResourceMgr.GetString("cmbLOC_STATE");
			capLOC_ZIP.Text			=		objResourceMgr.GetString("txtLOC_ZIP");
			capLOC_COUNTRY.Text		=		objResourceMgr.GetString("cmbLOC_COUNTRY");
			//capPHONE_NUMBER.Text	=		objResourceMgr.GetString("txtPHONE_NUMBER");
			//capFAX_NUMBER.Text		=		objResourceMgr.GetString("txtFAX_NUMBER");
			//capDEDUCTIBLE.Text		=		objResourceMgr.GetString("cmbDEDUCTIBLE");
			//capNAMED_PERILL.Text	=		objResourceMgr.GetString("cmbNAMED_PERILL");
			capDESCRIPTION.Text		=		objResourceMgr.GetString("txtDESCRIPTION");
			capLOCATION_TYPE.Text			=objResourceMgr.GetString("cmbLOCATION_TYPE");
			capLOSSREPORT_DATETIME.Text		= objResourceMgr.GetString("txtLOSSREPORT_DATETIME");
			capLOSSREPORT_ORDER.Text		= objResourceMgr.GetString("cmbLOSSREPORT_ORDER");
			capREPORT_STATUS.Text			= objResourceMgr.GetString("cmbREPORT_STATUS");
		}

		/// <summary>
		/// Get query string from url into hidden controls
		/// </summary>
		private void GetQueryString()
		{
			hidCUSTOMER_ID.Value = Request.Params["CUSTOMER_ID"];
			hidPOL_ID.Value = Request.Params["POL_ID"];
			hidPOL_VERSION_ID.Value = Request.Params["POL_VERSION_ID"];
			hidLOCATION_ID.Value = Request.Params["LOCATION_ID"];

		}

		/// <summary>
		/// retreive the information about selected record in the form of XML
		/// and saves it into hidden control
		/// </summary>
		private void GetOldDataXML()
		{
			if ( hidLOCATION_ID.Value != "" ) 
			{
				hidOldData.Value = ClsLocation.GetPolicyLocationInfo(int.Parse(hidCUSTOMER_ID.Value)
					, int.Parse(hidPOL_ID.Value)
					, int.Parse(hidPOL_VERSION_ID.Value)
					, int.Parse(hidLOCATION_ID.Value));
			}
			
		}
		
		
		private void SetWorkflow()
		{
			if(base.ScreenId	==	"238_0" || base.ScreenId == "258_0")
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",hidCUSTOMER_ID.Value);
				myWorkFlow.AddKeyValue("POLICY_ID",hidPOL_ID.Value);
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",hidPOL_VERSION_ID.Value);
				if ( hidLOCATION_ID.Value != "" )
				{
					myWorkFlow.AddKeyValue("LOCATION_ID",hidLOCATION_ID.Value);
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
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			int intRetVal;

			objLocation = new  ClsLocation();
			ClsPolicyLocationInfo objLocationInfo = GetFormValue();
			
			objLocationInfo.MODIFIED_BY = int.Parse(GetUserId());

			if(hidLOCATION_ID.Value!=null && hidLOCATION_ID.Value!="")
				objLocationInfo.LOCATION_ID=int.Parse(hidLOCATION_ID.Value);

			intRetVal = objLocation.Delete(objLocationInfo);
						
			if(intRetVal>0)
			{
				lblDelete.Text			= Cms.CmsWeb.ClsMessages.GetMessage("G","127");
				//itrack # 6254 13-aug-09 -Manoj
				lblDelete.Visible = true;				
				hidFormSaved.Value = "5";
				hidOldData.Value = "";
				trBody.Attributes.Add("style","display:none");
			}
			else if(intRetVal == -1)
			{
			
				//lblDelete.Text		=	ClsMessages.GetMessage(base.ScreenId,"3");
				//itrack # 6254 13-aug-09 -Manoj
				lblMessage.Text			=   ClsMessages.GetMessage(base.ScreenId,"3");
				hidFormSaved.Value		=	"2";
				lblMessage.Visible	= true;
			}
			SetWorkflow();
					

		}
		[Ajax.AjaxMethod()]
		public string AjaxFetchZipForState(int stateID, string ZipID)
		{
			CmsWeb.webservices.ClsWebServiceCommon obj =  new CmsWeb.webservices.ClsWebServiceCommon();
			string result = "";
			result = obj.FetchZipForState(stateID,ZipID);
			return result;
		}
		//Added by Manoj Rathore on 30 Jun.2009 Itrack # 6029
		[Ajax.AjaxMethod()]
		public string AjaxGetCountyForZip(string strZip)
		{
			CmsWeb.webservices.ClsWebServiceCommon obj =  new CmsWeb.webservices.ClsWebServiceCommon();
			string result = "";
			result = obj.GetCountyForZip(strZip);
			return result;
		}
	}
}
