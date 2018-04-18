/******************************************************************************************
<Author				: -   Gaurav
<Start Date				: -	8/26/2005 12:32:11 PM
<End Date				: -	
<Description				: - 	This file is used to 
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
<Purpose				: - 
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
using Cms.Model.Maintenance;
using Cms.BusinessLayer.BlCommon;
using Cms.ExceptionPublisher.ExceptionManagement;
namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// 
	/// </summary>
	public class AddEndorsment : Cms.CmsWeb.cmsbase
	{
		#region Page controls declaration
		protected System.Web.UI.WebControls.DropDownList cmbSTATE_ID;
		protected System.Web.UI.WebControls.DropDownList cmbLOB_ID;
		protected System.Web.UI.WebControls.DropDownList cmbPURPOSE;
		protected System.Web.UI.WebControls.DropDownList cmbTYPE;
		protected System.Web.UI.WebControls.TextBox txtDESCRIPTION;
		protected System.Web.UI.WebControls.TextBox txtTEXT;
		protected System.Web.UI.WebControls.TextBox txtFORM_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtEDITION_DATE;
		protected System.Web.UI.WebControls.DropDownList cmbSELECT_COVERAGE;
		protected System.Web.UI.WebControls.DropDownList cmbENDORS_ASSOC_COVERAGE;
        protected System.Web.UI.WebControls.Label capMessages;
		protected System.Web.UI.WebControls.Label capFORM_NUMBER;
		protected System.Web.UI.WebControls.Label capEDITION_DATE;
		protected System.Web.UI.WebControls.Label capSTATE_ID;
		protected System.Web.UI.WebControls.Label capLOB_ID;
		protected System.Web.UI.WebControls.Label capPURPOSE;
		protected System.Web.UI.WebControls.Label capTYPE;
		protected System.Web.UI.WebControls.Label capTEXT;
		protected System.Web.UI.WebControls.Label capENDORS_ASSOC_COVERAGE;
		protected System.Web.UI.WebControls.Label capSELECT_COVERAGE;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidENDORSMENT_ID;

		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSTATE_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOB_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPURPOSE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESCRIPTION;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvTEXT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvENDORS_ASSOC_COVERAGE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSELECT_COVERAGE;

		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.CustomValidator csvTEXT;
		protected System.Web.UI.WebControls.Label capENDORS_PRINT;
		protected System.Web.UI.WebControls.DropDownList cmbENDORS_PRINT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvENDORS_PRINT;
		//protected System.Web.UI.WebControls.Label lblATTACH_FILE_NAME;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvATTACH_FILE_NAME;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnFileName;
		//protected System.Web.UI.HtmlControls.HtmlInputFile txtATTACH_FILE_NAME;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRootPath;
		protected System.Web.UI.WebControls.Label capENDORSEMENT_CODE;
		protected System.Web.UI.WebControls.Label capDESCRIPTION;
		protected System.Web.UI.WebControls.TextBox txtENDORSEMENT_CODE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvENDORSEMENT_CODE;
		//protected System.Web.UI.WebControls.TextBox txtEFFECTIVE_FROM_DATE;
		//protected System.Web.UI.WebControls.HyperLink hlkEFFECTIVE_FROM_DATE;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvEFFECTIVE_FROM_DATE;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revEFFECTIVE_FROM_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEDITION_DATE;		
		//protected System.Web.UI.WebControls.Label capEFFECTIVE_FROM_DATE;
		//protected System.Web.UI.WebControls.Label capEFFECTIVE_TO_DATE;
//		protected System.Web.UI.WebControls.TextBox txtEFFECTIVE_TO_DATE;
//		protected System.Web.UI.WebControls.HyperLink hlkEFFECTIVE_TO_DATE;
//		protected System.Web.UI.WebControls.RegularExpressionValidator revEFFECTIVE_TO_DATE;
//		protected System.Web.UI.WebControls.CustomValidator csvEFFECTIVE_TO_DATE;
		protected System.Web.UI.WebControls.Label lblATTACH_FILE_NAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvATTACH_FILE_NAME;
////		protected System.Web.UI.WebControls.Label capDISABLED_DATE;
////		protected System.Web.UI.WebControls.TextBox txtDISABLED_DATE;
////		protected System.Web.UI.WebControls.HyperLink hlkDISABLED_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkEDITION_DATE;
//		protected System.Web.UI.WebControls.RegularExpressionValidator revDISABLED_DATE;
//		protected System.Web.UI.WebControls.CustomValidator csvDISABLED_DATE;
		

		#endregion

		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		//private int	intLoggedInUserID;
		protected System.Web.UI.WebControls.Label capINCREASED_LIMIT;
		protected System.Web.UI.WebControls.DropDownList cmbINCREASED_LIMIT;
		protected System.Web.UI.WebControls.Label capPRINT_ORDER;
		protected System.Web.UI.WebControls.TextBox txtPRINT_ORDER;
		protected System.Web.UI.WebControls.RegularExpressionValidator revPRINT_ORDER;
		//Defining the business layer class object
		ClsEndorsmentDetails  objEndorsmentDetails ;
		//END:*********** Local variables *************

		#endregion

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			
			base.ScreenId="394_0";
			lblMessage.Visible = false;
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
		
			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnActivateDeactivate.PermissionString		=	gstrSecurityXML;

			btnSave.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;

			//cmbLOB_ID_SelectedIndexChanged(null,null);
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddEndorsment" ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{

				//Added by Mohit Agarwal 27-Jun-2007
				cmbINCREASED_LIMIT.DataSource		= Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
				cmbINCREASED_LIMIT.DataTextField	= "LookupDesc";
				cmbINCREASED_LIMIT.DataValueField	= "LookupID";
				cmbINCREASED_LIMIT.DataBind();
				cmbINCREASED_LIMIT.Items.Insert(0,"");

				//hlkEFFECTIVE_FROM_DATE.Attributes.Add("OnClick","fPopCalendar(document.getElementById('txtEFFECTIVE_FROM_DATE'), document.getElementById('txtEFFECTIVE_FROM_DATE'))");
//				hlkEFFECTIVE_TO_DATE.Attributes.Add("OnClick","fPopCalendar(document.getElementById('txtEFFECTIVE_TO_DATE'), document.getElementById('txtEFFECTIVE_TO_DATE'))");
//				hlkDISABLED_DATE.Attributes.Add("OnClick","fPopCalendar(document.getElementById('txtDISABLED_DATE'), document.getElementById('txtDISABLED_DATE'))");
				hlkEDITION_DATE.Attributes.Add("OnClick","fPopCalendar(document.getElementById('txtEDITION_DATE'), document.getElementById('txtEDITION_DATE'))");
				btnReset.Attributes.Add("onclick","javascript: return ResetTheForm();");
				#region "Loading singleton"

				//				ClsEndorsmentDetails.GetCoverage(cmbSELECT_COVERAGE,Convert.ToInt32(cmbSTATE_ID.SelectedValue),Convert.ToInt32(cmbLOB_ID.SelectedValue));
				DataTable dt = Cms.CmsWeb.ClsFetcher.ActiveState;
				cmbSTATE_ID.DataSource=dt; 				
				cmbSTATE_ID.DataTextField	= "State_Name";
				cmbSTATE_ID.DataValueField	= "State_Id";
				cmbSTATE_ID.DataBind();
				cmbSTATE_ID.Items.Insert(0,"");

				DataTable dtLOBs = Cms.CmsWeb.ClsFetcher.LOBs;
				cmbLOB_ID.DataSource			= dtLOBs;
				cmbLOB_ID.DataTextField		= "LOB_DESC";
				cmbLOB_ID.DataValueField		= "LOB_ID";
				cmbLOB_ID.DataBind();
				cmbLOB_ID.Items.Insert(0,new ListItem("",""));
				//cmbLOB_ID.SelectedIndex= 
//				int strState = int.Parse(Request.QueryString["STATE_ID"].ToString());
//				int strLob = int.Parse(Request.QueryString["LobId"].ToString());
//				int strCov	= int.Parse(Request.QueryString["COV_ID"].ToString());
//				ListItem liState =new ListItem();
//				ListItem liLob =new ListItem();
//				liState = cmbSTATE_ID.Items.FindByValue((strState).ToString());
//				liLob = cmbLOB_ID.Items.FindByValue((strLob).ToString());
//				if(liState!=null)
//					liState.Selected=true; 
//				if(liLob!=null)
//					liLob.Selected=true; 
//				
//				this.SetInEdit(strLob,strState,strCov);

				//cmbSELECT_COVERAGE.SelectedValue = objdataSet.Tables[0].Rows[0]["SELECT_COVERAGE"].ToString();
				//cmbENDORS_ASSOC_COVERAGE.SelectedValue = "Y";
				#endregion//Loading singleton

				hidRootPath.Value = System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL") + @"/Endorsement/" + hidENDORSMENT_ID.Value ;
				if(Request.QueryString["ENDORSMENT_ID"]!=null && Request.QueryString["ENDORSMENT_ID"]!= "0" && Request.QueryString["ENDORSMENT_ID"].ToString().Length>0)
				{
					hidENDORSMENT_ID.Value=Request.QueryString["ENDORSMENT_ID"].ToString();;
					LoadData();
					hidFormSaved.Value = "0";
				}
				SetCaptions();
				SetValidators();
			}
			EnableDisable();
		}//end pageload
		#endregion

		void EnableDisable()
		{
			if(hidOldData.Value.Trim() != "")
			{
				txtDESCRIPTION.Enabled=false;
				txtENDORSEMENT_CODE.Enabled=false; 
				txtTEXT.Enabled=false;
				cmbENDORS_ASSOC_COVERAGE.Enabled=false;
				//cmbENDORS_PRINT.Enabled=false;
				cmbLOB_ID.Enabled=false;
				cmbPURPOSE.Enabled=false;
				cmbSELECT_COVERAGE.Enabled=false;
				cmbSTATE_ID.Enabled=false;
				cmbTYPE.Enabled=false; 				
			}
		}
	
		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsEndorsmentDetailsInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsEndorsmentDetailsInfo objEndorsmentDetailsInfo;
			objEndorsmentDetailsInfo = new ClsEndorsmentDetailsInfo();

			
			objEndorsmentDetailsInfo.STATE_ID=	Convert.ToInt32(cmbSTATE_ID.SelectedValue);
			objEndorsmentDetailsInfo.LOB_ID=	Convert.ToInt32(cmbLOB_ID.SelectedValue);
			objEndorsmentDetailsInfo.PURPOSE=	cmbPURPOSE.SelectedValue;
			objEndorsmentDetailsInfo.TYPE=	cmbTYPE.SelectedValue;
			objEndorsmentDetailsInfo.DESCRIPTION=	txtDESCRIPTION.Text;
			objEndorsmentDetailsInfo.TEXT=	txtTEXT.Text;	
			objEndorsmentDetailsInfo.ENDORSEMENT_CODE=	txtENDORSEMENT_CODE.Text;
			objEndorsmentDetailsInfo.ENDORS_ASSOC_COVERAGE=	cmbENDORS_ASSOC_COVERAGE.SelectedValue;
			if(cmbSELECT_COVERAGE.SelectedItem!=null)
			objEndorsmentDetailsInfo.SELECT_COVERAGE=	Convert.ToInt32(cmbSELECT_COVERAGE.SelectedValue);
			objEndorsmentDetailsInfo.IS_ACTIVE ="Y";
			objEndorsmentDetailsInfo.ENDORS_PRINT = cmbENDORS_PRINT.SelectedValue;
			objEndorsmentDetailsInfo.INCREASED_LIMIT = int.Parse(cmbINCREASED_LIMIT.SelectedItem.Value==""?"0":cmbINCREASED_LIMIT.SelectedItem.Value);
			
			
			//if(txtEFFECTIVE_FROM_DATE.Text.Trim() != "")
			//	objEndorsmentDetailsInfo.EFFECTIVE_FROM_DATE=Convert.ToDateTime(txtEFFECTIVE_FROM_DATE.Text);

//			if(txtEFFECTIVE_TO_DATE.Text.Trim() != "")
//				objEndorsmentDetailsInfo.EFFECTIVE_TO_DATE=Convert.ToDateTime(txtEFFECTIVE_TO_DATE.Text);

//			if(txtDISABLED_DATE.Text.Trim() != "")
//				objEndorsmentDetailsInfo.DISABLED_DATE=Convert.ToDateTime(txtDISABLED_DATE.Text);

			if(txtEDITION_DATE.Text.Trim() != "")
				objEndorsmentDetailsInfo.EDITION_DATE=Convert.ToDateTime(txtEDITION_DATE.Text);

			objEndorsmentDetailsInfo.FORM_NUMBER=txtFORM_NUMBER.Text;
            try { objEndorsmentDetailsInfo.PRINT_ORDER = int.Parse(txtPRINT_ORDER.Text); }
            catch { } //(Exception ex){}

			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidENDORSMENT_ID.Value;
			oldXML		= hidOldData.Value;
			//Returning the model object

			return objEndorsmentDetailsInfo;
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
			this.cmbSTATE_ID.SelectedIndexChanged += new System.EventHandler(this.cmbSTATE_ID_SelectedIndexChanged);
			this.cmbLOB_ID.SelectedIndexChanged += new System.EventHandler(this.cmbLOB_ID_SelectedIndexChanged);
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
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
				objEndorsmentDetails = new  ClsEndorsmentDetails();
				//Retreiving the form values into model class object
				ClsEndorsmentDetailsInfo objEndorsmentDetailsInfo = GetFormValue();

				if(hidOldData.Value.Trim()  == "") //save case
				{
					objEndorsmentDetailsInfo.CREATED_BY = int.Parse(GetUserId());
					objEndorsmentDetailsInfo.CREATED_DATETIME = DateTime.Now;

					//Calling the add method of business layer class
					intRetVal = objEndorsmentDetails.Add(objEndorsmentDetailsInfo);

					if(intRetVal>0)
					{
						hidENDORSMENT_ID.Value = objEndorsmentDetailsInfo.ENDORSMENT_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						LoadData();
						hidIS_ACTIVE.Value = "Y";

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
					ClsEndorsmentDetailsInfo objOldEndorsmentDetailsInfo;
					objOldEndorsmentDetailsInfo = new ClsEndorsmentDetailsInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldEndorsmentDetailsInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objEndorsmentDetailsInfo.ENDORSMENT_ID = int.Parse(strRowId);
					objEndorsmentDetailsInfo.MODIFIED_BY = int.Parse(GetUserId());
					objEndorsmentDetailsInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					intRetVal	= objEndorsmentDetails.Update(objOldEndorsmentDetailsInfo,objEndorsmentDetailsInfo);
					
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						LoadData();					
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
				if(objEndorsmentDetails!= null)
					objEndorsmentDetails.Dispose();
			}
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
				objEndorsmentDetails =  new ClsEndorsmentDetails();

				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					objStuTransactionInfo.transactionDescription = "Deactivated Succesfully.";
					objEndorsmentDetails.TransactionInfoParams = objStuTransactionInfo;
					objEndorsmentDetails.ActivateDeactivate(hidENDORSMENT_ID.Value,"N");
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";
				}
				else
				{
					objStuTransactionInfo.transactionDescription = "Activated Succesfully.";
					objEndorsmentDetails.TransactionInfoParams = objStuTransactionInfo;
					objEndorsmentDetails.ActivateDeactivate(hidENDORSMENT_ID.Value,"Y");
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";
				}
				DataSet objdataSet = ClsEndorsmentDetails.GetEndorsement(hidENDORSMENT_ID.Value);
				hidOldData.Value = objdataSet.GetXml();
				//hidOldData.Value = ClsEndorsmentDetails.GetXmlForPageControls(hidENDORSMENT_ID.Value);
				hidFormSaved.Value			=	"1";
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
				if(objEndorsmentDetails!= null)
					objEndorsmentDetails.Dispose();
			}
		}
		#endregion


		#region SetCaption Function 
		private void SetCaptions()
		{
			capSTATE_ID.Text					=		objResourceMgr.GetString("cmbSTATE_ID");
			capLOB_ID.Text						=		objResourceMgr.GetString("cmbLOB_ID");
			capPURPOSE.Text						=		objResourceMgr.GetString("cmbPURPOSE");
			capTYPE.Text						=		objResourceMgr.GetString("cmbTYPE");
			capDESCRIPTION.Text					=		objResourceMgr.GetString("txtDESCRIPTION");
			capTEXT.Text						=		objResourceMgr.GetString("txtTEXT");
			capENDORS_ASSOC_COVERAGE.Text		=		objResourceMgr.GetString("cmbENDORS_ASSOC_COVERAGE");
			capSELECT_COVERAGE.Text				=		objResourceMgr.GetString("cmbSELECT_COVERAGE");
			capENDORSEMENT_CODE.Text		    =		objResourceMgr.GetString("txtENDORSEMENT_CODE");			
			capFORM_NUMBER.Text					=		objResourceMgr.GetString("txtFORM_NUMBER");
			capEDITION_DATE.Text				=		objResourceMgr.GetString("txtEDITION_DATE");
			capPRINT_ORDER.Text					=		objResourceMgr.GetString("txtPRINT_ORDER");
			//capEFFECTIVE_FROM_DATE.Text=objResourceMgr.GetString("txtEFFECTIVE_FROM_DATE");
//			capEFFECTIVE_TO_DATE.Text=objResourceMgr.GetString("txtEFFECTIVE_TO_DATE");
//			capDISABLED_DATE.Text=objResourceMgr.GetString("txtDISABLED_DATE");
		}
		#endregion 

		
		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetValidators()
		{
			rfvSTATE_ID.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("35");
			rfvLOB_ID.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"14");//Done for Itrack Issue 5868 on 25 May 2009
			rfvDESCRIPTION.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			//rfvTEXT.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
			rfvSELECT_COVERAGE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
			rfvENDORSEMENT_CODE.ErrorMessage	   = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6");
			//rfvATTACH_FILE_NAME.ErrorMessage		=   Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"50");
			csvTEXT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("442");
			//revEFFECTIVE_FROM_DATE.ValidationExpression=aRegExpDate;
			//revEFFECTIVE_FROM_DATE.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
//			revEFFECTIVE_TO_DATE.ValidationExpression=aRegExpDate;
//			revEFFECTIVE_TO_DATE.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");			
//			revDISABLED_DATE.ValidationExpression=aRegExpDate;
//			revDISABLED_DATE.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			revEDITION_DATE.ValidationExpression=aRegExpDate;
			revPRINT_ORDER.ValidationExpression=aRegExpInteger;
			revEDITION_DATE.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			
			//rfvEFFECTIVE_FROM_DATE.ErrorMessage=ClsMessages.GetMessage(base.ScreenId,"7");
//			csvEFFECTIVE_TO_DATE.ErrorMessage=ClsMessages.GetMessage(base.ScreenId,"8");
//			csvDISABLED_DATE.ErrorMessage=ClsMessages.GetMessage(base.ScreenId,"9");
		}
		#endregion

		private void LoadData()
		{
			DataSet objdataSet = ClsEndorsmentDetails.GetEndorsement(hidENDORSMENT_ID.Value );
			hidOldData.Value = objdataSet.GetXml();

			if(objdataSet.Tables.Count>0)
			{
				if(objdataSet.Tables[0].Rows[0]["DESCRIPTION"] != DBNull.Value)
				{
					txtDESCRIPTION.Text  = objdataSet.Tables[0].Rows[0]["DESCRIPTION"].ToString ();
				}
//				if( objdataSet.Tables[0].Rows[0]["DISABLED_DATE"] != DBNull.Value)
				{
//					txtDISABLED_DATE.Text = objdataSet.Tables[0].Rows[0]["DISABLED_DATE"].ToString(); 
				}
//				if( objdataSet.Tables[0].Rows[0]["EFFECTIVE_FROM_DATE"] != DBNull.Value)
//				{
//					txtEFFECTIVE_FROM_DATE.Text = objdataSet.Tables[0].Rows[0]["EFFECTIVE_FROM_DATE"].ToString();
//				}
//				if(objdataSet.Tables[0].Rows[0]["EFFECTIVE_TO_DATE"] != DBNull.Value)
//				{
//					txtEFFECTIVE_TO_DATE.Text =objdataSet.Tables[0].Rows[0]["EFFECTIVE_TO_DATE"].ToString();
//				}
				if(objdataSet.Tables[0].Rows[0]["ENDORSEMENT_CODE"] != DBNull.Value)
				{
					txtENDORSEMENT_CODE.Text =objdataSet.Tables[0].Rows[0]["ENDORSEMENT_CODE"].ToString(); 
				}
				if(objdataSet.Tables[0].Rows[0]["TEXT"] != DBNull.Value)
				{
					txtTEXT.Text  =objdataSet.Tables[0].Rows[0]["TEXT"].ToString(); 
				}

				if(objdataSet.Tables[0].Rows[0]["ENDORS_PRINT"]!= DBNull.Value && objdataSet.Tables[0].Rows[0]["ENDORS_PRINT"].ToString()!="")
				{
					cmbENDORS_PRINT.SelectedValue = objdataSet.Tables[0].Rows[0]["ENDORS_PRINT"].ToString();					
				}
		
				int stateID = Convert.ToInt32(objdataSet.Tables[0].Rows[0]["STATE_ID"]);
				cmbSTATE_ID.SelectedValue = stateID.ToString();
				cmbSTATE_ID_SelectedIndexChanged(null, null);
					
				int lobID = Convert.ToInt32(objdataSet.Tables[0].Rows[0]["LOB_ID"]);
				//cmbLOB_ID.SelectedValue = lobID.ToString();
				cmbLOB_ID.SelectedIndex = cmbLOB_ID.Items.IndexOf(cmbLOB_ID.Items.FindByValue(lobID.ToString()));
				cmbLOB_ID_SelectedIndexChanged(null, null);
					
				if ( objdataSet.Tables[0].Rows[0]["INCREASED_LIMIT"] != System.DBNull.Value )
				{
					ListItem listItem;
					listItem = cmbINCREASED_LIMIT.Items.FindByValue(Convert.ToString(objdataSet.Tables[0].Rows[0]["INCREASED_LIMIT"]));
					cmbINCREASED_LIMIT.SelectedIndex= cmbINCREASED_LIMIT.Items.IndexOf(listItem);	
				}
				if ( objdataSet.Tables[0].Rows[0]["PRINT_ORDER"] != System.DBNull.Value )
				{	
					txtPRINT_ORDER.Text = objdataSet.Tables[0].Rows[0]["PRINT_ORDER"].ToString();
				}

				if ( objdataSet.Tables[0].Rows[0]["SELECT_COVERAGE"] != System.DBNull.Value && objdataSet.Tables[0].Rows[0]["SELECT_COVERAGE"].ToString()!="")
				{
					//int assoc_cov = Convert.ToInt32(objdataSet.Tables[0].Rows[0]["SELECT_COVERAGE"]);

					this.SetInEdit(lobID,stateID,0);

					/*ListItem li = this.cmbENDORS_ASSOC_COVERAGE.Items.FindByValue(assoc_cov.ToString());

					if ( li != null )
					{
						this.cmbENDORS_ASSOC_COVERAGE.SelectedIndex = cmbENDORS_ASSOC_COVERAGE.Items.IndexOf(li);
					}*/
					try
					{
						cmbSELECT_COVERAGE.SelectedValue = objdataSet.Tables[0].Rows[0]["SELECT_COVERAGE"].ToString();
						cmbENDORS_ASSOC_COVERAGE.SelectedValue = "Y";
					}
					catch//(Exception ex)
					{
					}
				}
				else
					cmbENDORS_ASSOC_COVERAGE.SelectedValue = "N";
			}
		}


		private void SetInEdit(int lobid, int stateid , int covid)
		{
			ClsEndorsmentDetails.GetCoverage(cmbSELECT_COVERAGE,stateid,lobid,covid);
		}

		private void cmbSTATE_ID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			
				try
				{
					int stateID;
					Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGenInfo=new Cms.BusinessLayer.BlApplication.ClsGeneralInformation(); 
					DataSet dsLOB=new DataSet(); 
					cmbSELECT_COVERAGE.Items.Clear();
					stateID=cmbSTATE_ID.SelectedItem==null?-1:int.Parse(cmbSTATE_ID.SelectedItem.Value);     
					if(stateID!=-1)
					{
					
						dsLOB=objGenInfo.GetLOBBYSTATEID(stateID);
						cmbLOB_ID.DataSource=dsLOB;
						cmbLOB_ID.DataTextField="LOB_DESC";
						cmbLOB_ID.DataValueField="LOB_ID"; 
						cmbLOB_ID.DataBind();
						cmbLOB_ID.Items.Insert(0,"");
						

						hidFormSaved.Value="3";
						//cmbAPP_LOB_SelectedIndexChanged(null,null);
					}
				}
				catch(Exception ex)
				{
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				}
			
		}

		private void cmbLOB_ID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				int lobID;
				Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGenInfo=new Cms.BusinessLayer.BlApplication.ClsGeneralInformation(); 
				DataSet dsLOB=new DataSet(); 
				lobID=cmbLOB_ID.SelectedItem.Text==""?-1:int.Parse(cmbLOB_ID.SelectedItem.Value);     
				if(lobID!=-1)
				{
					
					ClsEndorsmentDetails.GetCoverage(cmbSELECT_COVERAGE,Convert.ToInt32(cmbSTATE_ID.SelectedValue),Convert.ToInt32(cmbLOB_ID.SelectedValue),0);
					hidFormSaved.Value="3";
					//cmbAPP_LOB_SelectedIndexChanged(null,null);
				}
				else
				{
					cmbSELECT_COVERAGE.Items.Clear();
				}
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
		}
	}
}
