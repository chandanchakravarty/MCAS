/******************************************************************************************
<Author				: -   Anurag Verma
<Start Date				: -	11/08/2005 9:54:31 AM
<End Date				: -	
<Description				: - 	desc
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
<Purpose				: - desc

<Modified Date			: - 09/02/2006
<Modified By			: - Shafi
<Purpose				: - Check On Violation Date Greater Than DOB,Fill Vialation ComboBox,Validator For Valdation Combo Box
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
using Cms.BusinessLayer.BlApplication;
using Cms.CmsWeb;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.Model.Policy ;
using Cms.Model.Application;
using Cms.CmsWeb.Controls;

namespace Cms.Policies.Aspx
{
	/// <summary>
	/// Summary description for PolicyAutoMVR.
	/// </summary>
	public class PolicyAutoMVR : Cms.Policies.policiesbase
	{
		#region Page controls declaration
		//protected System.Web.UI.WebControls.TextBox txtMVR_AMOUNT;
		//protected System.Web.UI.WebControls.CheckBox chkMVR_DEATH;
		protected System.Web.UI.WebControls.TextBox txtMVR_DATE;

		//protected System.Web.UI.WebControls.Label capMVR_AMOUNT;
		//protected System.Web.UI.WebControls.Label capMVR_DEATH;
		protected System.Web.UI.WebControls.Label capMVR_DATE;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidVIOLATION_ID;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomer_Id;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicy_Id;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicy_Version_Id;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOL_MVR_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOL_WATER_MVR_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOL_UMB_MVR_ID;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidVIOLATION_DES;		
		protected System.Web.UI.WebControls.DropDownList cmbVERIFIED;
		protected System.Web.UI.WebControls.Label capVERIFIED;
		
		
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.WebControls.Label lblDelete;

		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvMVR_AMOUNT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMVR_DATE;

		//protected System.Web.UI.WebControls.RegularExpressionValidator revMVR_AMOUNT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revMVR_DATE;
		protected System.Web.UI.WebControls.Label lblMessage;		
		protected System.Web.UI.WebControls.HyperLink hlkDRIVER_MVR_DATE;
		#endregion
		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		//the following constant string will be used to determine where the page is being called from (Application/Polcy)
		public const string CALLED_FROM = "POLICY";
		public const string CALLED_FROM_UMBRELLA = "UMB";
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		
		protected System.Web.UI.WebControls.Label capMVR_VIOLATION;
		protected System.Web.UI.WebControls.DropDownList cmbVIOLATION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDriver_Id;
		protected System.Web.UI.WebControls.RegularExpressionValidator revMVR_FUTURE_DATE;
		protected System.Web.UI.WebControls.CustomValidator csvMVR_DATE;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;		
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		string strCalledFrom="";
		DataTable dtDriverDetails;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvVIOLATION_ID;
		protected System.Web.UI.WebControls.CustomValidator csvMVR_DATE_COMPARE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDRIVER_DOB;
		protected System.Web.UI.WebControls.Label capVIOLATION_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbVIOLATION_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVIOLATION_TYPE;
		protected System.Web.UI.WebControls.TextBox txtDETAILS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDETAILS;
		protected System.Web.UI.WebControls.CustomValidator csvDETAILS;
		protected System.Web.UI.WebControls.Label capOCCURENCE_DATE;
		protected System.Web.UI.WebControls.TextBox txtOCCURENCE_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkOCCURENCE_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvOCCURENCE_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revOCCURENCE_DATE;
		protected System.Web.UI.WebControls.CustomValidator csvOCCURENCE_DATE;
		protected System.Web.UI.WebControls.Label capPOINTS_ASSIGNED;
		protected System.Web.UI.WebControls.TextBox txtPOINTS_ASSIGNED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPOINTS_ASSIGNED;
		protected System.Web.UI.WebControls.RegularExpressionValidator revPOINTS_ASSIGNED;
		protected System.Web.UI.WebControls.Label capADJUST_VIOLATION_POINTS;
		protected System.Web.UI.WebControls.TextBox txtADJUST_VIOLATION_POINTS;
		protected System.Web.UI.WebControls.RegularExpressionValidator revADJUST_VIOLATION_POINTS;
		
		//Defining the business layer class object
		ClsMvrInformation  objMvrInformation ;
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
			rfvVIOLATION_TYPE.ErrorMessage		= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"711");
            //if (GetSystemId().ToString().ToUpper() == "S001" || GetSystemId().ToString().ToUpper() == "SUAT")
            //    rfvVIOLATION_ID.Enabled = false;
            //else
            //rfvVIOLATION_ID.ErrorMessage        = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("557");
			//rfvMVR_AMOUNT.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"335");
			rfvMVR_DATE.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"504");
			//revMVR_AMOUNT.ValidationExpression	=	aRegExpDouble;
			revOCCURENCE_DATE.ValidationExpression	=	aRegExpDate;
			revPOINTS_ASSIGNED.ValidationExpression = aRegExpIntegerSign;
			revADJUST_VIOLATION_POINTS.ValidationExpression = aRegExpIntegerSign;
			//revMVR_AMOUNT.ValidationExpression  = aRegExpDoublePositiveWithZero;
			revMVR_DATE.ValidationExpression	=	aRegExpDate;			
			csvMVR_DATE.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"491");
			//revMVR_AMOUNT.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"116");
			revMVR_DATE.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"22");
			csvMVR_DATE_COMPARE.ErrorMessage    = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"552");
		}
		#endregion
		private void Page_Load(object sender, System.EventArgs e)
		{
			Ajax.Utility.RegisterTypeForAjax(typeof(PolicyAutoMVR));		
			#region setting screen id
			strCalledFrom = Request.QueryString["CALLEDFROM"];
			switch(strCalledFrom.ToUpper())
			{
				case "PPA" :
					base.ScreenId	=	"228_1_0";
					break;
				case "MOT" :
					base.ScreenId	=	"237_1_0";
					break;
				case "UMB" :
					base.ScreenId	=	"278_1_0";
					break;
				case "WAT" :
					base.ScreenId	=	"247_1_0";
					break;
				case "Home":
				case "HOME":
					//Added ScreenID for MVR Info
					base.ScreenId	=  "252_1_0";
					//base.ScreenId	=  "149_1_0";
					break;
				case "RENT":
					base.ScreenId	=  "167_1_0";
					break;
				default :
					base.ScreenId	=	"45_1_0";
					break;
			}
			#endregion			

			lblMessage.Visible = false;
			SetErrorMessages();

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;
			
			btnActivateDeactivate.CmsButtonClass	= CmsButtonType.Write;
			btnActivateDeactivate.PermissionString	= gstrSecurityXML;
			
			

			btnSave.CmsButtonClass	=	CmsButtonType.Execute;
			btnSave.PermissionString		=	gstrSecurityXML;

			btnDelete.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Delete;
			btnDelete.PermissionString		=	gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.PolicyAutoMVR" ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{
				GetQueryString();
				//txtMVR_AMOUNT.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");
				hlkDRIVER_MVR_DATE.Attributes.Add("OnClick","fPopCalendar(document.POL_MVR_INFORMATION.txtMVR_DATE,document.POL_MVR_INFORMATION.txtMVR_DATE)");			
				hlkOCCURENCE_DATE.Attributes.Add("OnClick","fPopCalendar(document.POL_MVR_INFORMATION.txtOCCURENCE_DATE,document.POL_MVR_INFORMATION.txtOCCURENCE_DATE)");			
				//btnReset.Attributes.Add("onclick","javascript:ResetForm('" + Page.Controls[0].ID + "' );return formatCurrencyOnLoad();");
				btnSave.Attributes.Add("onClick","SetViolationDesc();");
				btnReset.Attributes.Add("onclick","javascript:ResetPolMVR();");
				//added by manoj itrack # 6416 on 18 Sep. 2009
				cmbVIOLATION_TYPE.Attributes.Add("OnChange","Javascript:FetchViolations();CheckIIX(2);");
				PopulateMVRList();
				fxnCheckDriverPointsAssigned();
		
				if(Request.QueryString["POL_MVR_ID"]!=null && Request.QueryString["POL_MVR_ID"].ToString().Length>0)
				{
					hidOldData.Value = ClsMvrInformation.GetXmlForPolicyPageControls(Request.QueryString["POL_MVR_ID"],int.Parse(hidCustomer_Id.Value),int.Parse(hidPolicy_Id.Value),int.Parse(hidPolicy_Version_Id.Value),int.Parse(hidDriver_Id.Value));
					
				}
				else if(Request.QueryString["APP_WATER_MVR_ID"]!=null && Request.QueryString["APP_WATER_MVR_ID"].ToString().Length>0)
				{
					hidOldData.Value = ClsMvrInformation.GetXmlForPolicyWaterCraft(int.Parse(hidCustomer_Id.Value),int.Parse(hidPolicy_Id.Value),int.Parse(hidPolicy_Version_Id.Value),int.Parse(hidDriver_Id.Value),int.Parse(Request["APP_WATER_MVR_ID"]));
					//btnDelete.Visible=true;
					//btnActivateDeactivate.Visible=true;
				}				
				else if(Request.QueryString["POL_UMB_MVR_ID"]!=null && Request.QueryString["POL_UMB_MVR_ID"].ToString().Length>0)
				{
					hidOldData.Value = ClsMvrInformation.GetXmlForPolicyUmbrella(int.Parse(hidCustomer_Id.Value),int.Parse(hidPolicy_Id.Value),int.Parse(hidPolicy_Version_Id.Value),int.Parse(hidDriver_Id.Value),int.Parse(Request["POL_UMB_MVR_ID"]));
					//btnDelete.Visible=true;
					//btnActivateDeactivate.Visible=true;
				}								
				if((Request.QueryString["POL_MVR_ID"]!=null && Request.QueryString["POL_MVR_ID"].ToString().Length>0) || (Request.QueryString["APP_WATER_MVR_ID"]!=null && Request.QueryString["APP_WATER_MVR_ID"].ToString().Length>0) || (Request.QueryString["POL_UMB_MVR_ID"]!=null && Request.QueryString["POL_UMB_MVR_ID"].ToString().Length>0))
				{
					btnDelete.Visible=true;
					btnActivateDeactivate.Visible=true;
               	}
				else
				{
					btnDelete.Visible=false;
					btnActivateDeactivate.Visible=false;
				}



				//hidOldData.Value = ClsMvrInformation.GetXmlForPageControls(Request.QueryString["CUSTOMER_ID"],Request.QueryString["APP_ID"],Request.QueryString["APP_VERSION_ID"]);
				
				SetCaptions();

				// Added by Shafi 09/02/06
				// for finding the driver_date of birth.
				hidDRIVER_DOB.Value=ClsMvrInformation.GetDriverDOBForPolicy(int.Parse(hidCustomer_Id.Value),int.Parse(hidPolicy_Id.Value),int.Parse(hidPolicy_Version_Id.Value),int.Parse(hidDriver_Id.Value),strCalledFrom);
				// end.	

				if((Request.QueryString["POL_MVR_ID"]!=null && Request.QueryString["POL_MVR_ID"].ToString().Length>0) || (Request.QueryString["APP_WATER_MVR_ID"]!=null && Request.QueryString["APP_WATER_MVR_ID"].ToString().Length>0) || (Request.QueryString["POL_UMB_MVR_ID"]!=null && Request.QueryString["POL_UMB_MVR_ID"].ToString().Length>0))
				{
					GetOldDataXML();
					hidPOL_WATER_MVR_ID.Value = Request.QueryString["APP_WATER_MVR_ID"];  
					hidPOL_UMB_MVR_ID.Value   = Request.QueryString["POL_UMB_MVR_ID"];  

				}
				else
				{
					hidOldData.Value = "";
				}

				#region "Loading singleton"
//				cmbVERIFIED.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
//				cmbVERIFIED.DataTextField	= "LookupDesc";
//				cmbVERIFIED.DataValueField	= "LookupCode";
//				cmbVERIFIED.DataBind();
				//cmbVERIFIED.Items.Insert(0,"");

				cmbVERIFIED.Items.Clear();
				ListItem list = new ListItem("Manual", "0");
				cmbVERIFIED.Items.Add(list);
				list = new ListItem("IIX", "1");
				cmbVERIFIED.Items.Add(list);
				list = new ListItem("EARS", "2");
				cmbVERIFIED.Items.Add(list);

				#endregion//Loading singleton
			}
		
			SetWorkflow();
		}

		protected void fxnCheckDriverPointsAssigned()
		{
			
			int tempVal = new ClsDriverDetail().FetchPolPointsAssignedInfo(int.Parse(hidCustomer_Id.Value),int.Parse(hidPolicy_Id.Value),int.Parse(hidPolicy_Version_Id.Value),int.Parse(hidDriver_Id.Value));
			if (tempVal>0)
			{
				lblMessage.Visible=true;
				lblMessage.Text = "You have opted for No points assignment for this driver";
			}
						
		}

		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsPolicyAutoMVR GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsPolicyAutoMVR objMvrInfo;
			objMvrInfo = new ClsPolicyAutoMVR();

//			if (txtMVR_AMOUNT.Text != "")
//			{
//				objMvrInfo.MVR_AMOUNT=	double.Parse(txtMVR_AMOUNT.Text);
//			}
			
			objMvrInfo.POLICY_ID=int.Parse(hidPolicy_Id.Value);
			objMvrInfo.POLICY_VERSION_ID=int.Parse(hidPolicy_Version_Id.Value);
			objMvrInfo.CUSTOMER_ID=int.Parse(hidCustomer_Id.Value);			
			//objMvrInfo.VIOLATION_ID=int.Parse(cmbVIOLATION_ID.SelectedValue);	
			if(Request.Form["cmbVIOLATION_ID"]!=null)
			{
				//Violation ID was not updating correctly for these Violations.
				string cmbVal = "0";
				if(Request.Form["cmbVIOLATION_ID"].ToString()=="")
					cmbVal = "0";
				else
					cmbVal = Request.Form["cmbVIOLATION_ID"].ToString();

				objMvrInfo.VIOLATION_ID=int.Parse(cmbVal);
				hidVIOLATION_ID.Value=Request.Form["cmbVIOLATION_ID"];
			}
			//if(cmbVIOLATION_TYPE.SelectedIndex>0)
			if(cmbVIOLATION_TYPE.SelectedItem!=null)
				objMvrInfo.VIOLATION_TYPE = int.Parse(cmbVIOLATION_TYPE.SelectedValue);
			objMvrInfo.IS_ACTIVE=hidIS_ACTIVE.Value;
			
			objMvrInfo.DRIVER_ID=int.Parse(hidDriver_Id.Value);
			objMvrInfo.MODIFIED_BY = int.Parse(GetUserId());

//			if(chkMVR_DEATH.Checked==true)
//				objMvrInfo.MVR_DEATH=	"Y";
//			else
//				objMvrInfo.MVR_DEATH=	"N";

			objMvrInfo.DETAILS = txtDETAILS.Text.Trim();
            if (Request["txtPOINTS_ASSIGNED"].ToString().Trim() != "")
            {
                objMvrInfo.POINTS_ASSIGNED = int.Parse(Request["txtPOINTS_ASSIGNED"].ToString().Trim());
                rfvPOINTS_ASSIGNED.Enabled = false;
            }
            else
                objMvrInfo.POINTS_ASSIGNED = 100;

			if(txtADJUST_VIOLATION_POINTS.Text.Trim() != "")
				objMvrInfo.ADJUST_VIOLATION_POINTS = int.Parse(txtADJUST_VIOLATION_POINTS.Text.Trim());
			else
				objMvrInfo.ADJUST_VIOLATION_POINTS = 100;


			if(txtMVR_DATE.Text.Trim() != "")
				objMvrInfo.MVR_DATE=	ConvertToDate(txtMVR_DATE.Text);

			objMvrInfo.OCCURENCE_DATE=	ConvertToDate(txtOCCURENCE_DATE.Text);

			dtDriverDetails	=	ClsMvrInformation.GetDriverDetailsForPolicyMVR(objMvrInfo.CUSTOMER_ID.ToString(),objMvrInfo.POLICY_ID.ToString(),objMvrInfo.POLICY_VERSION_ID.ToString(),objMvrInfo.DRIVER_ID.ToString(),strCalledFrom);
			
			if(dtDriverDetails!=null)
			{
				if(dtDriverDetails.Rows[0]["DRIVER_NAME"] != null)
					objMvrInfo.DRIVER_NAME = dtDriverDetails.Rows[0]["DRIVER_NAME"].ToString();
				if(dtDriverDetails.Rows[0]["DRIVER_CODE"] != null)
					objMvrInfo.DRIVER_CODE= dtDriverDetails.Rows[0]["DRIVER_CODE"].ToString();						
			}
			if(cmbVERIFIED.SelectedItem!=null)
				objMvrInfo.VERIFIED=int.Parse(cmbVERIFIED.SelectedItem.Value==""?"0":cmbVERIFIED.SelectedItem.Value);

			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;

			if (hidOldData.Value == "")
				strRowId = "NEW";
			else
			{
				if((strCalledFrom=="WAT") || (strCalledFrom=="Home"))
					strRowId = hidPOL_WATER_MVR_ID.Value;
				else if(strCalledFrom.ToUpper()==CALLED_FROM_UMBRELLA)
					strRowId = hidPOL_UMB_MVR_ID.Value;
				else
					strRowId = hidPOL_MVR_ID.Value;
			}

			oldXML		= hidOldData.Value;
			//Returning the model object

			return objMvrInfo;
		}
		#endregion


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

		private void AdjustPointsTranLog(string oldadjustPoints, string newadjustPoints)
		{
			string trans_desc = "Violation Points adjusted";
			string trans_custom = ";Violation/Note Type: " + cmbVIOLATION_TYPE.Items[cmbVIOLATION_TYPE.SelectedIndex].Text;
			if(int.Parse(cmbVIOLATION_TYPE.SelectedValue) < 15000 && cmbVIOLATION_TYPE.Items[cmbVIOLATION_TYPE.SelectedIndex].Text!="SUSPENSION")
				trans_custom += ";Violation: " + hidVIOLATION_DES.Value.Trim();
			else
				trans_custom += ";Violation: " + txtDETAILS.Text;

			trans_custom += ";Adjust Violation Points: Before '" + oldadjustPoints + "' After '" + newadjustPoints + "'";
			trans_custom += ";Occurrence Date: " + txtOCCURENCE_DATE.Text;


			Cms.BusinessLayer.BlApplication.ClsGeneralInformation  objGenInfo = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
			if(oldadjustPoints != newadjustPoints)
				objGenInfo.WriteTransactionLog(int.Parse(hidCustomer_Id.Value),int.Parse(hidPolicy_Id.Value),int.Parse(hidPolicy_Version_Id.Value), trans_desc, int.Parse(GetUserId()),trans_custom, "Policy");

		}
		
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
				objMvrInformation = new  ClsMvrInformation();

				//Retreiving the form values into model class object
				ClsPolicyAutoMVR objMvrInfo = GetFormValue();
				
				if(objMvrInfo.VERIFIED == 0)
					objMvrInfo.ENTERED_TLOG = "Manual";
				else if(objMvrInfo.VERIFIED == 1)
					objMvrInfo.ENTERED_TLOG = "IIX";
				else if(objMvrInfo.VERIFIED == 2)
					objMvrInfo.ENTERED_TLOG = "EARS";

				if(objMvrInfo.ADJUST_VIOLATION_POINTS >= 100)
					objMvrInfo.ADJUST_VIOLATION_POINTS_TLOG = "";
				else
					objMvrInfo.ADJUST_VIOLATION_POINTS_TLOG = objMvrInfo.ADJUST_VIOLATION_POINTS.ToString();

				if(objMvrInfo.POINTS_ASSIGNED >= 100)
					objMvrInfo.POINTS_ASSIGNED_TLOG = "";
				else
					objMvrInfo.POINTS_ASSIGNED_TLOG = objMvrInfo.POINTS_ASSIGNED.ToString();

				if(objMvrInfo.VIOLATION_ID == 0)
					objMvrInfo.VIOLATION_TLOG = objMvrInfo.DETAILS;
				else
					objMvrInfo.VIOLATION_TLOG = objMvrInfo.VIOLATION_ID.ToString();
				
				if(strRowId.ToUpper().Equals("NEW")) //save case
					//if(!(strRowId.ToUpper().Equals("NEW"))) //save case
				{
					objMvrInfo.CREATED_BY = int.Parse(GetUserId());
					objMvrInfo.CREATED_DATETIME = DateTime.Now;
					
					

					//Calling the add method of business layer class
					if((strCalledFrom=="WAT") || (strCalledFrom=="Home"))
						intRetVal = objMvrInformation.AddPolicyWaterCraft(objMvrInfo);
					if(strCalledFrom.ToUpper()==CALLED_FROM_UMBRELLA)
						intRetVal = objMvrInformation.AddPolicyUmbrella(objMvrInfo);						
					else
						intRetVal = objMvrInformation.Add(objMvrInfo,strCalledFrom.ToUpper());
						//intRetVal = objMvrInformation.Add(objMvrInfo);

					if(intRetVal>0)
					{
						if((strCalledFrom=="WAT") || (strCalledFrom =="Home"))
						{
							hidPOL_WATER_MVR_ID.Value = objMvrInfo.POL_WATER_MVR_ID.ToString();
							hidOldData.Value = ClsMvrInformation.GetXmlForPolicyWaterCraft(int.Parse(hidCustomer_Id.Value),int.Parse(hidPolicy_Id.Value),int.Parse(hidPolicy_Version_Id.Value),int.Parse(hidDriver_Id.Value),objMvrInfo.POL_WATER_MVR_ID);
						}
						else if(strCalledFrom.ToUpper()==CALLED_FROM_UMBRELLA)
						{
							hidPOL_UMB_MVR_ID.Value = objMvrInfo.POL_UMB_MVR_ID.ToString();
							hidOldData.Value = ClsMvrInformation.GetXmlForPolicyUmbrella(int.Parse(hidCustomer_Id.Value),int.Parse(hidPolicy_Id.Value),int.Parse(hidPolicy_Version_Id.Value),int.Parse(hidDriver_Id.Value),objMvrInfo.POL_UMB_MVR_ID);
						}
						else
						{
							hidPOL_MVR_ID.Value = objMvrInfo.APP_MVR_ID.ToString();
							hidOldData.Value = ClsMvrInformation.GetXmlForPolicyPageControls(objMvrInfo.APP_MVR_ID.ToString(),int.Parse(hidCustomer_Id.Value),int.Parse(hidPolicy_Id.Value),int.Parse(hidPolicy_Version_Id.Value),int.Parse(hidDriver_Id.Value));					
						}

						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";						
						
							
						hidIS_ACTIVE.Value = "Y";

						//Seting the workflow
						SetWorkflow();

						//Opening the endorsement details widnow
						OpenEndorsementDetails();
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"1030");
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
					string oldAdjustPoints = "";
					string newAdjustPoints = "";
					ClsPolicyAutoMVR objOldMvrInfo;
					objOldMvrInfo = new ClsPolicyAutoMVR();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldMvrInfo,hidOldData.Value);							

					if(objOldMvrInfo.VERIFIED == 0)
						objOldMvrInfo.ENTERED_TLOG = "Manual";
					else if(objOldMvrInfo.VERIFIED == 1)
						objOldMvrInfo.ENTERED_TLOG = "IIX";
					else if(objOldMvrInfo.VERIFIED == 2)
						objOldMvrInfo.ENTERED_TLOG = "EARS";

					if(objOldMvrInfo.ADJUST_VIOLATION_POINTS >= 100)
						objOldMvrInfo.ADJUST_VIOLATION_POINTS_TLOG = "";
					else
						objOldMvrInfo.ADJUST_VIOLATION_POINTS_TLOG = objOldMvrInfo.ADJUST_VIOLATION_POINTS.ToString();

					if(objOldMvrInfo.POINTS_ASSIGNED >= 100)
						objOldMvrInfo.POINTS_ASSIGNED_TLOG = "";
					else
						objOldMvrInfo.POINTS_ASSIGNED_TLOG = objOldMvrInfo.POINTS_ASSIGNED.ToString();

					if(objOldMvrInfo.VIOLATION_ID == 0)
						objOldMvrInfo.VIOLATION_TLOG = objOldMvrInfo.DETAILS;
					else
						objOldMvrInfo.VIOLATION_TLOG = objOldMvrInfo.VIOLATION_ID.ToString();

					if(objOldMvrInfo.ADJUST_VIOLATION_POINTS != 0)
						oldAdjustPoints = objOldMvrInfo.ADJUST_VIOLATION_POINTS.ToString();

					if(objMvrInfo.ADJUST_VIOLATION_POINTS < 100)
						newAdjustPoints = objMvrInfo.ADJUST_VIOLATION_POINTS.ToString();

					//Setting those values into the Model object which are not in the page
					if((strCalledFrom=="WAT") || (strCalledFrom=="Home"))				
						objMvrInfo.POL_WATER_MVR_ID = int.Parse(strRowId);
					else if(strCalledFrom.ToUpper()==CALLED_FROM_UMBRELLA)
						objMvrInfo.POL_UMB_MVR_ID = int.Parse(strRowId);
					else
						objMvrInfo.APP_MVR_ID = int.Parse(strRowId);

					objMvrInfo.MODIFIED_BY = int.Parse(GetUserId());
					objMvrInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					//Updating the record using business layer class object
					if((strCalledFrom=="WAT") || (strCalledFrom=="Home"))
						intRetVal	= objMvrInformation.UpdatePolicyWaterCraft(objOldMvrInfo,objMvrInfo);
					else if(strCalledFrom.ToUpper()==CALLED_FROM_UMBRELLA)
						intRetVal	= objMvrInformation.UpdatePolicyUmbrella(objOldMvrInfo,objMvrInfo);
					else
						intRetVal	= objMvrInformation.Update(objOldMvrInfo,objMvrInfo,strCalledFrom.ToUpper());
						//intRetVal	= objMvrInformation.Update(objOldMvrInfo,objMvrInfo);

					if( intRetVal > 0 )			// update successfully performed
					{
						AdjustPointsTranLog(oldAdjustPoints, newAdjustPoints);

						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";						
						
						if((strCalledFrom=="WAT") || (strCalledFrom=="Home"))
							hidOldData.Value = ClsMvrInformation.GetXmlForPolicyWaterCraft(int.Parse(hidCustomer_Id.Value),int.Parse(hidPolicy_Id.Value),int.Parse(hidPolicy_Version_Id.Value),int.Parse(hidDriver_Id.Value),objMvrInfo.POL_WATER_MVR_ID);
						else if(strCalledFrom.ToUpper()==CALLED_FROM_UMBRELLA)
							hidOldData.Value = ClsMvrInformation.GetXmlForPolicyUmbrella(int.Parse(hidCustomer_Id.Value),int.Parse(hidPolicy_Id.Value),int.Parse(hidPolicy_Version_Id.Value),int.Parse(hidDriver_Id.Value),objMvrInfo.POL_UMB_MVR_ID);
						else
							hidOldData.Value = ClsMvrInformation.GetXmlForPolicyPageControls(objMvrInfo.APP_MVR_ID.ToString(),int.Parse(hidCustomer_Id.Value),int.Parse(hidPolicy_Id.Value),int.Parse(hidPolicy_Version_Id.Value),int.Parse(hidDriver_Id.Value));					

						//Seting the workflow	
					
						//Opening the endorsement details widnow
						OpenEndorsementDetails();
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"1030");
						hidFormSaved.Value		=	"1";
					}
					
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value		=	"1";
					}
					lblMessage.Visible = true;
				}
				SetWorkflow();
				btnDelete.Visible=true;
                btnActivateDeactivate.Visible = true;                
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
				if(objMvrInformation!= null)
					objMvrInformation.Dispose();
			}			
		}

		/// <summary>
		/// Deletes a record.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			int intRetVal;
			ClsPolicyAutoMVR objMvrInfo = GetFormValue();

			if(hidPOL_MVR_ID.Value!=null && hidPOL_MVR_ID.Value!="")
				objMvrInfo.APP_MVR_ID= int.Parse(hidPOL_MVR_ID.Value);			

			if(hidPOL_WATER_MVR_ID.Value!=null && hidPOL_WATER_MVR_ID.Value!="")
				objMvrInfo.POL_WATER_MVR_ID=int.Parse(hidPOL_WATER_MVR_ID.Value);

			if(hidPOL_UMB_MVR_ID.Value!=null && hidPOL_UMB_MVR_ID.Value!="")
				objMvrInfo.POL_UMB_MVR_ID=int.Parse(hidPOL_UMB_MVR_ID.Value);
						
			objMvrInformation = new Cms.BusinessLayer.BlApplication.ClsMvrInformation();

			if((strCalledFrom=="WAT") || (strCalledFrom=="Home"))
				intRetVal = objMvrInformation.DeletePolicyWaterCraft(objMvrInfo);
			else if(strCalledFrom.ToUpper()==CALLED_FROM_UMBRELLA)
				intRetVal = objMvrInformation.DeletePolicyUmbrella(objMvrInfo);
			else
				intRetVal = objMvrInformation.Delete(objMvrInfo,strCalledFrom.ToUpper());
				//intRetVal = objMvrInformation.Delete(objMvrInfo);
			
			
			if(intRetVal>0)
			{
				//following single line has been commented
				lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","127");
				//hidFormSaved.Value = "1";
				hidFormSaved.Value = "5";
				hidOldData.Value = "";
				trBody.Attributes.Add("style","display:none");
				SetWorkflow();
				base.OpenEndorsementDetails();

			}
			else if(intRetVal == -1)
			{
				//following single line has been commented
				lblDelete.Text		=	ClsMessages.GetMessage(base.ScreenId,"128");
				hidFormSaved.Value		=	"2";
			}
			lblDelete.Visible = true;
		}

		/// <summary>
		/// Activates and deactivates  .
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// 
		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{			
			try
			{
				
				Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo ();
				objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
				objStuTransactionInfo.loggedInUserName = GetUserName();
				objMvrInformation =  new ClsMvrInformation();

				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					objStuTransactionInfo.transactionDescription = "MVR Information Deactivated Succesfully.";
					objMvrInformation.TransactionInfoParams = objStuTransactionInfo;					
					if((strCalledFrom=="WAT") || (strCalledFrom=="Home"))
						objMvrInformation.ActivateDeactivatePolicyWaterCraft(int.Parse(hidCustomer_Id.Value),int.Parse(hidPolicy_Id.Value),int.Parse(hidPolicy_Version_Id.Value),int.Parse(hidDriver_Id.Value),"N",int.Parse(hidPOL_WATER_MVR_ID.Value));    
					else if(strCalledFrom.ToUpper()==CALLED_FROM_UMBRELLA)
						objMvrInformation.ActivateDeactivatePolicyUmbrella(int.Parse(hidCustomer_Id.Value),int.Parse(hidPolicy_Id.Value),int.Parse(hidPolicy_Version_Id.Value),int.Parse(hidDriver_Id.Value),"N",int.Parse(hidPOL_UMB_MVR_ID.Value));    
					else
					{						
						objMvrInformation.ActivateDeactivatePolMVR(int.Parse(hidCustomer_Id.Value),int.Parse(hidPolicy_Id.Value),int.Parse(hidPolicy_Version_Id.Value),int.Parse(hidDriver_Id.Value),"N",int.Parse(hidPOL_MVR_ID.Value));    
						//objMvrInformation.Activate_Deactivate_Proc="Proc_ActivateDeactivatePOL_MVR_INFORMATION";
						//objMvrInformation.ActivateDeactivate(hidPOL_MVR_ID.Value,"N");
					}
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";							
				}
				else
				{
					objStuTransactionInfo.transactionDescription = "MVR Information Activated Succesfully.";
					objMvrInformation.TransactionInfoParams = objStuTransactionInfo;
					if((strCalledFrom=="WAT") || (strCalledFrom=="Home"))
						objMvrInformation.ActivateDeactivatePolicyWaterCraft(int.Parse(hidCustomer_Id.Value),int.Parse(hidPolicy_Id.Value),int.Parse(hidPolicy_Version_Id.Value),int.Parse(hidDriver_Id.Value),"Y",int.Parse(hidPOL_WATER_MVR_ID.Value));    
					else if(strCalledFrom.ToUpper()==CALLED_FROM_UMBRELLA)
						objMvrInformation.ActivateDeactivatePolicyUmbrella(int.Parse(hidCustomer_Id.Value),int.Parse(hidPolicy_Id.Value),int.Parse(hidPolicy_Version_Id.Value),int.Parse(hidDriver_Id.Value),"Y",int.Parse(hidPOL_UMB_MVR_ID.Value));    
					else
					{						
						objMvrInformation.ActivateDeactivatePolMVR(int.Parse(hidCustomer_Id.Value),int.Parse(hidPolicy_Id.Value),int.Parse(hidPolicy_Version_Id.Value),int.Parse(hidDriver_Id.Value),"Y",int.Parse(hidPOL_MVR_ID.Value));    
						//objMvrInformation.Activate_Deactivate_Proc="Proc_ActivateDeactivatePOL_MVR_INFORMATION";
						//objMvrInformation.ActivateDeactivate(hidPOL_MVR_ID.Value,"N");
					}
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";										
				}
				
				hidFormSaved.Value			=	"0";
				if((strCalledFrom=="WAT") || (strCalledFrom=="Home"))
				{
					hidOldData.Value =ClsMvrInformation.GetXmlForPolicyWaterCraft(int.Parse(hidCustomer_Id.Value),int.Parse(hidPolicy_Id.Value),int.Parse(hidPolicy_Version_Id.Value),int.Parse(hidDriver_Id.Value),int.Parse(hidPOL_WATER_MVR_ID.Value));
					ClientScript.RegisterStartupScript(this.GetType(),"REFRESHGRID","<script>RefreshWebGrid(1," + hidPOL_WATER_MVR_ID.Value + ");</script>");
				}
				else if(strCalledFrom.ToUpper()==CALLED_FROM_UMBRELLA)
				{
					hidOldData.Value =ClsMvrInformation.GetXmlForPolicyUmbrella(int.Parse(hidCustomer_Id.Value),int.Parse(hidPolicy_Id.Value),int.Parse(hidPolicy_Version_Id.Value),int.Parse(hidDriver_Id.Value),int.Parse(hidPOL_UMB_MVR_ID.Value));
					ClientScript.RegisterStartupScript(this.GetType(), "REFRESHGRID","<script>RefreshWebGrid(1," + hidPOL_UMB_MVR_ID.Value + ");</script>");
				}
				else
				{
					hidOldData.Value = ClsMvrInformation.GetXmlForPolicyPageControls(hidPOL_MVR_ID.Value,int.Parse(hidCustomer_Id.Value),int.Parse(hidPolicy_Id.Value),int.Parse(hidPolicy_Version_Id.Value),int.Parse(hidDriver_Id.Value));					
					ClientScript.RegisterStartupScript(this.GetType(), "REFRESHGRID","<script>RefreshWebGrid(1," + hidPOL_MVR_ID.Value + ");</script>");
				}	

				//Opening the endorsement details widnow
				base.OpenEndorsementDetails();
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
				if(objMvrInformation!= null)
					objMvrInformation.Dispose();
			}
		}
		#endregion
	
		
		#region Populate MVR listbox
		private void PopulateMVRList()
		{
             

			/*string LOB_ID="0";
			if(strCalledFrom=="WAT" || strCalledFrom=="HOME" )
			{
				LOB_ID="4";
			}
			else if(strCalledFrom=="MOT")
			{
				LOB_ID="3";
			}   
			else
			{
				LOB_ID="2";
			}			
			cmbVIOLATION_ID.DataSource=ClsMntViolations.GetCompletePolicyXmlForPageControls(hidCustomer_Id.Value,hidPolicy_Id.Value,hidPolicy_Version_Id.Value,LOB_ID );
			cmbVIOLATION_ID.DataTextField="VIOLATION_DES";
			cmbVIOLATION_ID.DataValueField="VIOLATION_ID";
			cmbVIOLATION_ID.DataBind();*/

			cmbVIOLATION_TYPE.DataSource=ClsMntViolations.GetViolationTypesForPolicy(hidCustomer_Id.Value,hidPolicy_Id.Value,hidPolicy_Version_Id.Value);
			cmbVIOLATION_TYPE.DataTextField="VIOLATION_DES";
			cmbVIOLATION_TYPE.DataValueField="VIOLATION_ID";
			cmbVIOLATION_TYPE.DataBind();

			cmbVIOLATION_TYPE.Items.Insert(0,"");

		}
		#endregion
		private void SetCaptions()
		{
			//capMVR_AMOUNT.Text						=		objResourceMgr.GetString("txtMVR_AMOUNT");
			//capMVR_DEATH.Text						=		objResourceMgr.GetString("chkMVR_DEATH");
			capMVR_DATE.Text						=		objResourceMgr.GetString("txtMVR_DATE");
			//capVERIFIED.Text						=       objResourceMgr.GetString("cmbVERIFIED");
			capVIOLATION_TYPE.Text					=       objResourceMgr.GetString("cmbVIOLATION_TYPE");
			
		}
		private void GetOldDataXML()
		{
			//hidOldData.Value = ClsMvrInformation.GetXmlForPageControls(hidCustomer_Id.Value,hidApp_Id.Value,hidApp_Version_Id.Value);
			
			if((strCalledFrom=="WAT") || (strCalledFrom=="Home"))		 	
				hidOldData.Value = ClsMvrInformation.GetXmlForPolicyWaterCraft(int.Parse(hidCustomer_Id.Value),int.Parse(hidPolicy_Id.Value),int.Parse(hidPolicy_Version_Id.Value),int.Parse(hidDriver_Id.Value),int.Parse(Request["APP_WATER_MVR_ID"]));
			else if(strCalledFrom.ToUpper()==CALLED_FROM_UMBRELLA)
				hidOldData.Value = ClsMvrInformation.GetXmlForPolicyUmbrella(int.Parse(hidCustomer_Id.Value),int.Parse(hidPolicy_Id.Value),int.Parse(hidPolicy_Version_Id.Value),int.Parse(hidDriver_Id.Value),int.Parse(Request["POL_UMB_MVR_ID"]));
			else
			{
				hidOldData.Value = ClsMvrInformation.GetXmlForPolicyPageControls(Request.QueryString["POL_MVR_ID"],int.Parse(hidCustomer_Id.Value),int.Parse(hidPolicy_Id.Value),int.Parse(hidPolicy_Version_Id.Value),int.Parse(hidDriver_Id.Value));			
				/*if(dtDriverDetails.Rows.Count > 0)
				{
					if(dtDriverDetails.Rows[0]["DRIVER_NAME"] != null)
						= dtDriverDetails.Rows[0]["DRIVER_NAME"].ToString();
					if(dtDriverDetails.Rows[0]["DRIVER_CODE"] != null)
						strAdditionalInfo = dtDriverDetails.Rows[0]["DRIVER_CODE"].ToString();						
				}*/
			}

		}

		private void SetWorkflow()
		{//Added the MVR ScreenID
			if(base.ScreenId == "228_1_0" || base.ScreenId == "237_1_0" || base.ScreenId == "247_1_0" || base.ScreenId == "252_1_0" || base.ScreenId == "278_1_0")
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				myWorkFlow.WorkflowModule="POL";
				if ( hidDriver_Id.Value != null && hidDriver_Id.Value != "" )
				{
					myWorkFlow.AddKeyValue("DRIVER_ID",hidDriver_Id.Value);
				}
				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
			}
			else
			{
				myWorkFlow.Display	=	false;
			}
		}
		#region GetQueryString values into hiddent variables
		private void GetQueryString()
		{
			//Load values for Customer_Id,APP_Id,App_Version_Id
			hidCustomer_Id.Value=GetCustomerID();
			hidPolicy_Id.Value=GetPolicyID();
			hidPolicy_Version_Id.Value=GetPolicyVersionID();
			if(Request.QueryString["DRIVER_ID"]!="")
				hidDriver_Id.Value=Request.QueryString["DRIVER_ID"];
			else
				hidDriver_Id.Value="";
			if(Request.QueryString["POL_MVR_ID"]!="")
				hidPOL_MVR_ID.Value=Request.QueryString["POL_MVR_ID"];
			else				
				hidPOL_MVR_ID.Value="";

			/*if(Request.QueryString["POL_WATER_MVR_ID"]!="")
				hidPOL_WATER_MVR_ID.Value=Request.QueryString["POL_WATER_MVR_ID"];
			else				
				hidPOL_WATER_MVR_ID.Value="";*/
			if(Request.QueryString["APP_WATER_MVR_ID"]!="")
				hidPOL_WATER_MVR_ID.Value=Request.QueryString["APP_WATER_MVR_ID"];
			else				
				hidPOL_WATER_MVR_ID.Value="";

			if(Request.QueryString["POL_UMB_MVR_ID"]!="")
				hidPOL_UMB_MVR_ID.Value=Request.QueryString["POL_UMB_MVR_ID"];
			else				
				hidPOL_UMB_MVR_ID.Value="";

			strCalledFrom=Request.QueryString["CALLEDFROM"];
		}		
		#endregion
		[Ajax.AjaxMethod()]
		public string AjaxGetViolations(int CUSTOMER_ID, int POL_ID, int POL_VERSION_ID,int VIOLATION_ID,string CALLED_FROM)
		{
			CmsWeb.webservices.ClsWebServiceCommon obj =  new CmsWeb.webservices.ClsWebServiceCommon();
			string result = "";
			result = obj.GetViolations(CUSTOMER_ID,POL_ID,POL_VERSION_ID,VIOLATION_ID,CALLED_FROM);
			return result;
			
		}

	}
}
