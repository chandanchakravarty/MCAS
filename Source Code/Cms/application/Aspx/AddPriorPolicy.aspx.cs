/******************************************************************************************
<Author					: -   Vijay
<Start Date				: -	4/26/2005 12:09:19 PM
<End Date				: -	
<Description			: - 	Shows the prior policy page.
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: -  4/5/2005
<Modified By				: - Anurag Verma
<Purpose				: - Removing use of app_id and app_version_id

<Modified Date			: - 25/08/2005
<Modified By			: - Anurag Verma 
<Purpose				: - Applying null check for the buttons
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
using System.Reflection;
using System.Resources;

namespace Cms.Application.Aspx
{
	/// <summary>
	/// 
	/// </summary>
	public class AddPriorPolicy : Cms.Application.appbase
	{
		#region Page controls declaration
		protected System.Web.UI.WebControls.TextBox txtAPP_PRIOR_CARRIER_INFO_ID;
		protected System.Web.UI.WebControls.TextBox txtOLD_POLICY_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtCARRIER;
		protected System.Web.UI.WebControls.TextBox txtEFF_DATE;
		protected System.Web.UI.WebControls.TextBox txtEXP_DATE;
		protected System.Web.UI.WebControls.TextBox txtPOLICY_TYPE;
		protected System.Web.UI.WebControls.TextBox txtYEARS_PRIOR_COMP;
		protected System.Web.UI.WebControls.TextBox txtACTUAL_PREM;
		protected System.Web.UI.WebControls.TextBox txtMOD_FACTOR;
		protected System.Web.UI.WebControls.TextBox txtANNUAL_PREM;
		protected System.Web.UI.WebControls.DropDownList cmbPOLICY_CATEGORY;
		protected System.Web.UI.WebControls.DropDownList cmbLOB;
		protected System.Web.UI.WebControls.DropDownList cmbPOLICY_TERM_CODE;
		protected System.Web.UI.WebControls.DropDownList cmbPRIOR_PRODUCER_INFO_ID;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		
		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOBXML;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOBId;
		
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPOLICY_TYPE;

		protected System.Web.UI.WebControls.RegularExpressionValidator revEFF_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEXP_DATE;
		protected System.Web.UI.WebControls.Label lblMessage;

		protected System.Web.UI.WebControls.Label capAPP_PRIOR_CARRIER_INFO_ID;
		protected System.Web.UI.WebControls.Label capOLD_POLICY_NUMBER;
		protected System.Web.UI.WebControls.Label capCARRIER;
		protected System.Web.UI.WebControls.Label capLOB;
		protected System.Web.UI.WebControls.Label capSUB_LOB;
		protected System.Web.UI.WebControls.Label capEFF_DATE;
		protected System.Web.UI.WebControls.Label capEXP_DATE;
        protected System.Web.UI.WebControls.Label capMessage;
        protected System.Web.UI.WebControls.Label capPOLICY_CATEGORY;
		protected System.Web.UI.WebControls.Label capPOLICY_TERM_CODE;
		protected System.Web.UI.WebControls.Label capPOLICY_TYPE;
		protected System.Web.UI.WebControls.Label capYEARS_PRIOR_COMP;
		protected System.Web.UI.WebControls.Label capACTUAL_PREM;
		protected System.Web.UI.WebControls.Label capASSIGNEDRISKYN;
		protected System.Web.UI.WebControls.Label capPRIOR_PRODUCER_INFO_ID;
		protected System.Web.UI.WebControls.Label capRISK_NEW_AGENCY;
		protected System.Web.UI.WebControls.Label capMOD_FACTOR;
		protected System.Web.UI.WebControls.Label capANNUAL_PREM;
		protected System.Web.UI.WebControls.CustomValidator  csvEXP_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revYEARS_PRIOR_COMP;
		protected System.Web.UI.WebControls.RegularExpressionValidator revACTUAL_PREM;
        protected System.Web.UI.WebControls.RegularExpressionValidator revANNUAL_PREM;
		protected System.Web.UI.WebControls.RegularExpressionValidator revMOD_FACTOR;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_PRIOR_CARRIER_INFO_ID;
		protected System.Web.UI.WebControls.CheckBox chkRISK_NEW_AGENCY;
		protected System.Web.UI.WebControls.HyperLink hlkEXP_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkEFF_DATE;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEXP_DATE;
		//Added for Itrack issue 6449 on 23 Oct 09
		protected System.Web.UI.WebControls.Label capPRIOR_BI_CSL_LIMIT;
		protected System.Web.UI.WebControls.TextBox txtPRIOR_BI_CSL_LIMIT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPRIOR_BI_CSL_LIMIT;
		protected System.Web.UI.HtmlControls.HtmlImage imgSelect;
        //System.Resources.ResourceManager objResourceMgr;
		#endregion

		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		// private int	intLoggedInUserID;
		protected System.Web.UI.WebControls.DropDownList cmbASSIGNEDRISKYN;
		//protected System.Web.UI.WebControls.DropDownList cmbSUB_LOB_NAME;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSUB_LOB;
		protected System.Web.UI.WebControls.CompareValidator cvDateCompare;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvOLD_POLICY_NUMBER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCARRIER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOB;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPOLICY_CATEGORY;
	
		//Defining the business layer class object
		Cms.BusinessLayer.BlApplication.ClsPriorPolicy  objPriorPolicy ;
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
			
			revEFF_DATE.ValidationExpression		=  aRegExpDate;
			revEXP_DATE.ValidationExpression		=  aRegExpDate;
			revYEARS_PRIOR_COMP.ValidationExpression= aRegExpDoublePositiveNonZero;
			revACTUAL_PREM.ValidationExpression		= aRegExpCurrencyformat;
			revANNUAL_PREM.ValidationExpression		= aRegExpCurrencyformat; //aRegExpDoublePositiveNonZero changed to aRegExpCurrencyformat - Done by Sibin on 11 Nov 08 for Itrack Issue 4955;
			revMOD_FACTOR.ValidationExpression		= aRegExpDoublePositiveNonZero;

			rfvPOLICY_TYPE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"101");
			revEFF_DATE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"22");
			revEXP_DATE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"22");
			revYEARS_PRIOR_COMP.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"216");
			revACTUAL_PREM.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"216");
			revANNUAL_PREM.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"216");
			csvEXP_DATE.ErrorMessage            =  ClsMessages.GetMessage(base.ScreenId,"1");
			revMOD_FACTOR.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"216");
			rfvOLD_POLICY_NUMBER.ErrorMessage	=  ClsMessages.GetMessage(base.ScreenId,"2");
			rfvCARRIER.ErrorMessage				=  ClsMessages.GetMessage(base.ScreenId,"3");
			rfvPOLICY_CATEGORY.ErrorMessage		=  ClsMessages.GetMessage(base.ScreenId,"4");
			rfvLOB.ErrorMessage					=  ClsMessages.GetMessage(base.ScreenId,"5");
			rfvPRIOR_BI_CSL_LIMIT.ErrorMessage	=  ClsMessages.GetMessage(base.ScreenId,"6");//Added for Itrack issue 6449 on 23 Oct 09

		}
		#endregion

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			SetControlAttributes();
            
			base.ScreenId="117_0";
			lblMessage.Visible = false;
			SetErrorMessages();
            objResourceMgr = new ResourceManager("Cms.Application.Aspx.AddPriorPolicy", Assembly.GetExecutingAssembly());


			/* This Security XML has been explicitly specified.
			  * It is done coz, when we go to this page, the ApplicationID session variable has a value.
			  * This in turn checks for a converted Application and then accordingly sets security XML,
			  * resulting in different Permission XML. Refer Support>Appbase.cs (Line no 70)
			 */
			
			//Commented by Sibin on 07-10-08

			//gstrSecurityXML = "<Security><Read>Y</Read><Write>Y</Write><Delete>Y</Delete><Execute>Y</Execute></Security>";
			
			//modified By Sibin on 29 dec 08 as per above Comment; to handle Rights of this page, this page will fetch SecurityXML again - Itrack Issue 5158
			SetSecurityXML(base.ScreenId, int.Parse(GetUserId()));
			//base.InitializeSecuritySettings();

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass		=		CmsButtonType.Write;
			btnReset.PermissionString	=	gstrSecurityXML;

			btnDelete.CmsButtonClass	=	CmsButtonType.Delete;
			btnDelete.PermissionString	=	gstrSecurityXML;

			btnSave.CmsButtonClass		=	CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			//objResourceMgr = new System.Resources.ResourceManager("Cms.Application.Aspx.AddPriorPolicy" ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{
				if (Request.Params.Count != 0)
				{
					hidCUSTOMER_ID.Value				= Request.Params["CUSTOMER_ID"];
					hidAPP_PRIOR_CARRIER_INFO_ID.Value	= Request.Params["APP_PRIOR_CARRIER_INFO_ID"];
				}
				else
				{
					hidCUSTOMER_ID.Value				= "";
					hidAPP_PRIOR_CARRIER_INFO_ID.Value	= "";
				}
				GetOldDataXML();
				SetCaptions();
                capMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
				PopulateComboBoxes();
				hidLOBXML.Value = Cms.BusinessLayer.BlApplication.ClsPriorPolicy.GetXmlForLob();
				//Added for Itrack issue 6449 on 23 Oct 09
				string url = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL();
				imgSelect.Attributes.Add("onclick","javascript:OpenLookup('" + url + "','LOOKUP_VALUE_CODE','LOOKUP_VALUE_DESC','','txtPRIOR_BI_CSL_LIMIT','PRIOR_BI_CSL_LIMIT','Prior BI/CSL Limit')");
			}
		}//end pageload
		#endregion

		#region SetControlAttributes
		private void SetControlAttributes()
		{
			btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
			cmbLOB.Attributes.Add("OnChange","javascript:FillSubLOB();");
			//cmbSUB_LOB_NAME.Attributes.Add("OnChange","javascript:SetSubLBId();");

			/*Setting the attributes for currency field*/
			txtACTUAL_PREM.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");
			txtANNUAL_PREM.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");			

			hlkEXP_DATE.Attributes.Add("OnClick","fPopCalendar(document.APP_PRIOR_CARRIER_INFO.txtEXP_DATE, document.APP_PRIOR_CARRIER_INFO.txtEXP_DATE)");
			hlkEFF_DATE.Attributes.Add("OnClick","fPopCalendar(document.APP_PRIOR_CARRIER_INFO.txtEFF_DATE, document.APP_PRIOR_CARRIER_INFO.txtEFF_DATE)");
		}
		#endregion

		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private Cms.Model.Application.ClsPriorPolicyInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			Cms.Model.Application.ClsPriorPolicyInfo objPriorPolicyInfo;
			objPriorPolicyInfo = new Cms.Model.Application.ClsPriorPolicyInfo();

			objPriorPolicyInfo.CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);
		
			if (hidAPP_PRIOR_CARRIER_INFO_ID.Value.ToUpper() != "NEW")
				objPriorPolicyInfo.APP_PRIOR_CARRIER_INFO_ID = int.Parse(hidAPP_PRIOR_CARRIER_INFO_ID.Value)	;
			else
				objPriorPolicyInfo.APP_PRIOR_CARRIER_INFO_ID = 0	;

			objPriorPolicyInfo.OLD_POLICY_NUMBER=	txtOLD_POLICY_NUMBER.Text;
			objPriorPolicyInfo.CARRIER=	txtCARRIER.Text;
			
			//--------
			if (hidLOBId.Value.Trim() == "")
				objPriorPolicyInfo.LOB = Convert.ToString(null) ;
			else
				objPriorPolicyInfo.LOB = hidLOBId.Value;

			//-------------

//			if (hidSUB_LOB.Value.Trim() == "")
//				objPriorPolicyInfo.SUB_LOB = "0";
//			else
//				objPriorPolicyInfo.SUB_LOB = hidSUB_LOB.Value;

			if(txtEFF_DATE.Text.Trim() != "")
				objPriorPolicyInfo.EFF_DATE = base.ConvertToDate(txtEFF_DATE.Text);

			if(txtEXP_DATE.Text.Trim() != "")
				objPriorPolicyInfo.EXP_DATE = base.ConvertToDate(txtEXP_DATE.Text);

			if (cmbPOLICY_CATEGORY.SelectedIndex > 0)
				objPriorPolicyInfo.POLICY_CATEGORY = (cmbPOLICY_CATEGORY.SelectedValue);

			if (cmbPOLICY_TERM_CODE.SelectedIndex  > 0)
				objPriorPolicyInfo.POLICY_TERM_CODE = (cmbPOLICY_TERM_CODE.SelectedValue);

			objPriorPolicyInfo.POLICY_TYPE = txtPOLICY_TYPE.Text;

			if (txtYEARS_PRIOR_COMP.Text.Trim() != "")
				objPriorPolicyInfo.YEARS_PRIOR_COMP = int.Parse(txtYEARS_PRIOR_COMP.Text);

			if (txtACTUAL_PREM.Text.Trim() != "")
				objPriorPolicyInfo.ACTUAL_PREM = Double.Parse( txtACTUAL_PREM.Text);
			
			//Added for Itrack issue 6449 on 23 Oct 09
			//Auto -> 8501
			if (txtPRIOR_BI_CSL_LIMIT.Text != "" && cmbPOLICY_CATEGORY.SelectedValue=="8501")
				objPriorPolicyInfo.PRIOR_BI_CSL_LIMIT = txtPRIOR_BI_CSL_LIMIT.Text;
			else
				objPriorPolicyInfo.PRIOR_BI_CSL_LIMIT = "";

			objPriorPolicyInfo.ASSIGNEDRISKYN= cmbASSIGNEDRISKYN.SelectedValue;
			
			//----
			if (cmbPRIOR_PRODUCER_INFO_ID.SelectedIndex > 0)
				objPriorPolicyInfo.PRIOR_PRODUCER_INFO_ID=	int.Parse(cmbPRIOR_PRODUCER_INFO_ID.SelectedValue);
			//else
			//	objPriorPolicyInfo.PRIOR_PRODUCER_INFO_ID=Convert.ToInt32(null);
			//-----------
			
			//-------------
			if (chkRISK_NEW_AGENCY.Checked == true)
			{
				objPriorPolicyInfo.RISK_NEW_AGENCY = "Y";
			}
			else
			{
				objPriorPolicyInfo.RISK_NEW_AGENCY =  Convert.ToString(null); 
			}
			

			objPriorPolicyInfo.MOD_FACTOR=	txtMOD_FACTOR.Text;
			objPriorPolicyInfo.ANNUAL_PREM=	txtANNUAL_PREM.Text.Trim();

			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidAPP_PRIOR_CARRIER_INFO_ID.Value;
			oldXML		= hidOldData.Value;
			//Returning the model object

			return objPriorPolicyInfo;
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
				objPriorPolicy = new  Cms.BusinessLayer.BlApplication.ClsPriorPolicy();

				//Retreiving the form values into model class object
				Cms.Model.Application.ClsPriorPolicyInfo objPriorPolicyInfo = GetFormValue();

				if(hidAPP_PRIOR_CARRIER_INFO_ID.Value.ToUpper().Equals("NEW")) //save case
				{
					objPriorPolicyInfo.CREATED_BY = int.Parse(GetUserId());
					objPriorPolicyInfo.CREATED_DATETIME = DateTime.Now;

					//Calling the add method of business layer class
					intRetVal = objPriorPolicy.Add(objPriorPolicyInfo);

					if(intRetVal>0)
					{
						hidAPP_PRIOR_CARRIER_INFO_ID.Value	= objPriorPolicyInfo.APP_PRIOR_CARRIER_INFO_ID.ToString();
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
					Cms.Model.Application.ClsPriorPolicyInfo objOldPriorPolicyInfo;
					objOldPriorPolicyInfo = new Cms.Model.Application.ClsPriorPolicyInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldPriorPolicyInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					//objPriorPolicyInfo.APP_ID = strRowId;
					objPriorPolicyInfo.MODIFIED_BY = int.Parse(GetUserId());
					objPriorPolicyInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					
					objPriorPolicyInfo.CREATED_BY = objOldPriorPolicyInfo.CREATED_BY;
					objPriorPolicyInfo.CREATED_DATETIME = objOldPriorPolicyInfo.CREATED_DATETIME;

					objPriorPolicyInfo.IS_ACTIVE = hidIS_ACTIVE.Value;

					//Updating the record using business layer class object
					intRetVal	= objPriorPolicy.Update(objOldPriorPolicyInfo,objPriorPolicyInfo);
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
				//Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objPriorPolicy!= null)
					objPriorPolicy.Dispose();
			}
		}

		#endregion

		#region Populatecombobox
		// <summary>
		/// Polulating the combo box data using the bl object
		/// </summary>
		private void PopulateComboBoxes()
		{
			//Get the customer details
			DataSet dsCustomer = Cms.BusinessLayer.BlClient.ClsCustomer.GetCustomerDetails(int.Parse(hidCUSTOMER_ID.Value));
			string customerType = dsCustomer.Tables[0].Rows[0]["CUSTOMER_TYPE_DESC"].ToString();
			if(customerType != "")
				customerType	=	customerType.Substring(0,1);
			//Populating the lob
			DataTable dt = Cms.CmsWeb.ClsFetcher.LOBs;

			//cmbLOB.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("LOBCD",customerType);
			cmbLOB.DataSource=dt.DefaultView;
			cmbLOB.DataTextField = "LOB_DESC";
			cmbLOB.DataValueField = "LOB_ID";
			cmbLOB.DataBind();
			cmbLOB.Items.Insert(0,"");
			//cmbLOB.Items.Insert(0,new System.Web.UI.WebControls.ListItem("","0"));

			//Populating the Policy category
			cmbPOLICY_CATEGORY.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("POLGRY");
			cmbPOLICY_CATEGORY.DataTextField = "LookupDesc";
			cmbPOLICY_CATEGORY.DataValueField = "LookupID";
			cmbPOLICY_CATEGORY.DataBind();
			//cmbPOLICY_CATEGORY.Items.Insert(0,new System.Web.UI.WebControls.ListItem("","0"));
			cmbPOLICY_CATEGORY.Items.Insert(0,"");

			//Populating the Policy Termination Code – 
			cmbPOLICY_TERM_CODE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("PHIND");
			cmbPOLICY_TERM_CODE.DataTextField = "LookupDesc";
			cmbPOLICY_TERM_CODE.DataValueField = "LookupID";
			cmbPOLICY_TERM_CODE.DataBind();
			cmbPOLICY_TERM_CODE.Items.Insert(0,new System.Web.UI.WebControls.ListItem("","0"));

			//Populating the producer
			Cms.BusinessLayer.BlCommon.ClsUser objUser = new Cms.BusinessLayer.BlCommon.ClsUser();
			dt = objUser.FillUser("PRO").Tables[0];
			cmbPRIOR_PRODUCER_INFO_ID.DataSource = dt;
			cmbPRIOR_PRODUCER_INFO_ID.DataTextField = "USER_NAME";
			cmbPRIOR_PRODUCER_INFO_ID.DataValueField = "USER_ID";
			cmbPRIOR_PRODUCER_INFO_ID.DataBind();
			cmbPRIOR_PRODUCER_INFO_ID.Items.Insert(0,new System.Web.UI.WebControls.ListItem("","0"));


            cmbASSIGNEDRISKYN.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YES_NO");
            cmbASSIGNEDRISKYN.DataTextField = "LookupDesc";
            cmbASSIGNEDRISKYN.DataValueField = "LookupCode";
            cmbASSIGNEDRISKYN.DataBind();
            cmbASSIGNEDRISKYN.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", ""));

			objUser.Dispose();
           

		}
		#endregion

		#region GetOldDataXML
		// <summary>
		/// Fetch old data from database on the basis of parameters passed to the page
		/// </summary>
		private void GetOldDataXML()
		{
			if (hidAPP_PRIOR_CARRIER_INFO_ID.Value != "" && hidCUSTOMER_ID.Value != "")
			{
				hidOldData.Value = Cms.BusinessLayer.BlApplication.ClsPriorPolicy.GetPriorPolicyInfo(
				int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidAPP_PRIOR_CARRIER_INFO_ID.Value));
			}
			else
			{
				//If parameters not passed 
				hidOldData.Value					= "";
			}
		}
		#endregion

		// <summary>
		/// Setting the captions of labels from resource files
		/// </summary>
		private void SetCaptions()
		{
			capOLD_POLICY_NUMBER.Text				=		objResourceMgr.GetString("txtOLD_POLICY_NUMBER");
			capCARRIER.Text							=		objResourceMgr.GetString("txtCARRIER");
			capLOB.Text								=		objResourceMgr.GetString("cmbLOB");
			//capSUB_LOB.Text							=		objResourceMgr.GetString("cmbSUB_LOB_NAME");
			capEFF_DATE.Text						=		objResourceMgr.GetString("txtEFF_DATE");
			capEXP_DATE.Text						=		objResourceMgr.GetString("txtEXP_DATE");
			capPOLICY_CATEGORY.Text					=		objResourceMgr.GetString("cmbPOLICY_CATEGORY");
			capPOLICY_TERM_CODE.Text				=		objResourceMgr.GetString("cmbPOLICY_TERM_CODE");
			capPOLICY_TYPE.Text						=		objResourceMgr.GetString("txtPOLICY_TYPE");
			capYEARS_PRIOR_COMP.Text				=		objResourceMgr.GetString("txtYEARS_PRIOR_COMP");
			capACTUAL_PREM.Text						=		objResourceMgr.GetString("txtACTUAL_PREM");
			capASSIGNEDRISKYN.Text					=		objResourceMgr.GetString("txtASSIGNEDRISKYN");
			capPRIOR_PRODUCER_INFO_ID.Text			=		objResourceMgr.GetString("cmbPRIOR_PRODUCER_INFO_ID");
			capRISK_NEW_AGENCY.Text					=		objResourceMgr.GetString("chkRISK_NEW_AGENCY");
			capMOD_FACTOR.Text						=		objResourceMgr.GetString("txtMOD_FACTOR");
			capANNUAL_PREM.Text						=		objResourceMgr.GetString("txtANNUAL_PREM");
			capPRIOR_BI_CSL_LIMIT.Text				=		objResourceMgr.GetString("txtPRIOR_BI_CSL_LIMIT");//Added for Itrack issue 6449 on 23 Oct 09
            rfvEXP_DATE.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "7");
		}

		/// <summary>
		/// Activates and deactivates  .
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			try
			{
				Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo ();
				objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
				objStuTransactionInfo.loggedInUserName = GetUserName();
				objPriorPolicy =  new Cms.BusinessLayer.BlApplication.ClsPriorPolicy();

				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					objStuTransactionInfo.transactionDescription = "Prior Policy Deactivated Succesfully.";
					objPriorPolicy.TransactionInfoParams = objStuTransactionInfo;
					objPriorPolicy.ActivateDeactivate(hidAPP_PRIOR_CARRIER_INFO_ID.Value,"N");
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";
				}
				else
				{
					objStuTransactionInfo.transactionDescription = "Activated Succesfully.";
					objPriorPolicy.TransactionInfoParams = objStuTransactionInfo;
					objPriorPolicy.ActivateDeactivate(hidAPP_PRIOR_CARRIER_INFO_ID.Value,"Y");
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
			}
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
		    try
            {
            int retValue = Delete();

            if (retValue > 0)
            {
                lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","127");
                hidAPP_PRIOR_CARRIER_INFO_ID.Value="";
                hidFormSaved.Value = "5";
                hidOldData.Value = "";
				trBody.Attributes.Add("style","display:none");
            }
            else if(retValue == -2)
            {
                lblDelete.Text			=	ClsMessages.FetchGeneralMessage("334");
                hidFormSaved.Value		=	"2";
            }
            else
            {
                lblDelete.Text         =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("334"); 
                hidFormSaved.Value      =   "2";
            }
			lblDelete.Visible=true;
           
        }
        catch (Exception objEx)
        {
        //lblMessage.Text = objEx.Message.ToString();
        //lblMessage.Visible = true;
        Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objEx);
        }
	}

		private void RegisterScript()
		{
            if (!ClientScript.IsStartupScriptRegistered("Refresh"))
			{
				string strCode = @"<script>
									RefreshWebGrid(1,1)
									</script>";

                ClientScript.RegisterStartupScript(this.GetType(),"Refresh", strCode);

			}

		}

		private int Delete()
		{
			Cms.BusinessLayer.BlApplication.ClsPriorPolicy objPriorPolicy = new Cms.BusinessLayer.BlApplication.ClsPriorPolicy();

			int intCustomerId = int.Parse(hidCUSTOMER_ID.Value);
			int intPolicyId	 = int.Parse(hidAPP_PRIOR_CARRIER_INFO_ID.Value);
			int intUserId=int.Parse(GetUserId());

			int retValue = objPriorPolicy.Delete(intCustomerId,intPolicyId,intUserId);
			return retValue;
		}
	}
}
