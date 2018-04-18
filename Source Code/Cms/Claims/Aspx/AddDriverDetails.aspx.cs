/******************************************************************************************
	<Author					: - > Sumit Chhabra
	<Start Date				: -	> May 04,2006
	<End Date				: - >
	<Description			: - > Page is used to display driver details at claims 
	<Review Date			: - >
	<Reviewed By			: - >
	
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
using System.Resources; 
using System.Reflection; 
using Cms.Model.Claims;
using Cms.CmsWeb.Controls; 
using Cms.ExceptionPublisher; 
using Cms.BusinessLayer.BLClaims;
using Cms.ExceptionPublisher.ExceptionManagement;



namespace Cms.Claims.Aspx
{
	/// <summary>
	/// 
	/// </summary>
	public class AddDriverDetails : Cms.Claims.ClaimBase
	{
		#region Page controls declaration
		
		
		#endregion
		#region Local form variables
		//START:*********** Local form variables *************
		//string oldXML;
		
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId;//, strFormSaved;
			
		
		#endregion		
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.Label lblVEHICLE_ID;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capNAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNAME;		
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNAMES;
		protected System.Web.UI.WebControls.DropDownList cmbNAME;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidNAME;
		protected System.Web.UI.WebControls.TextBox txtNAME;		
		protected System.Web.UI.WebControls.Label capVEHICLE_OWNER;
		protected System.Web.UI.WebControls.DropDownList cmbVEHICLE_OWNER;	
		protected System.Web.UI.WebControls.Label capDRIVER_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbDRIVER_TYPE;	
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTYPE_OF_OWNER;
//		protected System.Web.UI.WebControls.Label capNAME;
//		protected System.Web.UI.WebControls.TextBox txtNAME;
		protected System.Web.UI.WebControls.TextBox txtRELATION_INSURED;
		protected System.Web.UI.WebControls.Label capADDRESS1;
		protected System.Web.UI.WebControls.TextBox txtADDRESS1;
		protected System.Web.UI.WebControls.TextBox txtDRIVERS_INJURY;
		protected System.Web.UI.WebControls.Label capADDRESS2;
		protected System.Web.UI.WebControls.TextBox txtADDRESS2;
		protected System.Web.UI.WebControls.Label capCITY;
		protected System.Web.UI.WebControls.TextBox txtCITY;
		protected System.Web.UI.WebControls.Label capSTATE;
		protected System.Web.UI.WebControls.Label capDRIVERS_INJURY;
		protected System.Web.UI.WebControls.DropDownList cmbSTATE;
		protected System.Web.UI.WebControls.Label capZIP;
		protected System.Web.UI.WebControls.TextBox txtZIP;
		protected System.Web.UI.WebControls.RegularExpressionValidator revZIP;
		protected System.Web.UI.WebControls.Label capHOME_PHONE;
		protected System.Web.UI.WebControls.TextBox txtHOME_PHONE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revHOME_PHONE;
		protected System.Web.UI.WebControls.Label capWORK_PHONE;
		protected System.Web.UI.WebControls.TextBox txtWORK_PHONE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revWORK_PHONE;
		protected System.Web.UI.WebControls.Label capEXTENSION;
		protected System.Web.UI.WebControls.TextBox txtEXTENSION;
		protected System.Web.UI.WebControls.Label capMOBILE_PHONE;
		protected System.Web.UI.WebControls.TextBox txtMOBILE_PHONE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revMOBILE_PHONE;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;//Added for Itrack Issue 6053 on 8 Sept 2009	
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnName;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRESET;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTYPE_OF_DRIVER;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
//		protected System.Web.UI.WebControls.Label capSAME_AS_OWNER;		
		//protected System.Web.UI.WebControls.Label capNAMED_INSURED;
		//protected System.Web.UI.WebControls.DropDownList cmbNAMED_INSURED;
		//protected System.Web.UI.WebControls.Label capDRIVERS;
		//protected System.Web.UI.WebControls.DropDownList cmbDRIVERS;
		protected System.Web.UI.WebControls.Label capRELATION_INSURED;		
		protected System.Web.UI.WebControls.Label capDATE_OF_BIRTH;
		protected System.Web.UI.WebControls.TextBox txtDATE_OF_BIRTH;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDATE_OF_BIRTH;
		protected System.Web.UI.WebControls.Label capLICENSE_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtLICENSE_NUMBER;
		protected System.Web.UI.WebControls.Label capLICENSE_STATE;
		protected System.Web.UI.WebControls.DropDownList cmbLICENSE_STATE;
		//protected System.Web.UI.WebControls.Label capPURPOSE_OF_USE;
		//protected System.Web.UI.WebControls.DropDownList cmbPURPOSE_OF_USE;
		//protected System.Web.UI.WebControls.Label capUSED_WITH_PERMISSION;
		//protected System.Web.UI.WebControls.DropDownList cmbUSED_WITH_PERMISSION;
		//protected System.Web.UI.WebControls.Label capDESCRIBE_DAMAGE;
		//protected System.Web.UI.WebControls.TextBox txtDESCRIBE_DAMAGE;
		//protected System.Web.UI.WebControls.Label capESTIMATE_AMOUNT;
		//protected System.Web.UI.WebControls.TextBox txtESTIMATE_AMOUNT;
		//protected System.Web.UI.WebControls.RangeValidator rngESTIMATE_AMOUNT;
		protected System.Web.UI.WebControls.RangeValidator rngEXTENSION;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDRIVER_ID;
//		protected System.Web.UI.HtmlControls.HtmlTableRow trSAME_AS_OWNER;
//		protected System.Web.UI.WebControls.DropDownList cmbSAME_AS_OWNER;
//		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNAME;
		protected System.Web.UI.WebControls.HyperLink hlkDATE_OF_BIRTH;
		//protected System.Web.UI.WebControls.Label capOTHER_VEHICLE_INSURANCE;
		//protected System.Web.UI.WebControls.TextBox txtOTHER_VEHICLE_INSURANCE;
		protected System.Web.UI.WebControls.CustomValidator csvDATE_OF_BIRTH;
		protected System.Web.UI.WebControls.Label capVEHICLE_ID;
		protected System.Web.UI.WebControls.DropDownList cmbVEHICLE_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVEHICLE_ID;
		protected System.Web.UI.WebControls.Label capCOUNTRY;
		protected System.Web.UI.WebControls.DropDownList cmbCOUNTRY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOUNTRY;
		protected string strCalledComboFrom = "NAME";
	

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{						
			base.ScreenId="306_4_1";
			
			lblMessage.Visible = false;
			

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;
			
			btnSave.CmsButtonClass	=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;

			btnDelete.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnDelete.PermissionString	=	gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass	=	CmsButtonType.Execute;
			btnActivateDeactivate.PermissionString	=	gstrSecurityXML;

			
			
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.AddDriverDetails" ,System.Reflection.Assembly.GetExecutingAssembly());
			
			GetQueryStringValues();
			
			if(!Page.IsPostBack || hidRESET.Value=="1")
			{	
				this.cmbCOUNTRY.SelectedIndex = int.Parse(aCountry);
				hlkDATE_OF_BIRTH.Attributes.Add("OnClick","fPopCalendar(document.CLM_DRIVER_INFORMATION.txtDATE_OF_BIRTH,document.CLM_DRIVER_INFORMATION.txtDATE_OF_BIRTH)"); //Javascript Implementation for Calender						
//				cmbSAME_AS_OWNER.Attributes.Add("OnChange","EnableDisableNameAndAddress();");
				//txtESTIMATE_AMOUNT.Attributes.Add("onBlur","javascript: this.value = formatCurrencyWithCents(this.value);");
				//Check for whether Any Owners Exist for current claim_id
				//Following function has been commented for now as work on insured vehicle is still in progress
//				CheckDataForInsuredVehicle();				
				if(hidRESET.Value=="1")
					ResetFields("");
				hidRESET.Value="0";
				LoadDropDowns();				
				//Load data into controls
				GetOldDataXML(true);				
				btnReset.Attributes.Add("onclick","javascript:return ResetTheForm();");
				SetCaptions();
				SetErrorMessages();								
			}
		}
		#endregion

		#region GetOldDataXML
		private void GetOldDataXML(bool LOAD_DATA_FLAG)
		{
			DataSet dsOldData = new DataSet();
			if(hidDRIVER_ID.Value!="" && hidDRIVER_ID.Value!="0" && hidDRIVER_ID.Value.ToUpper()!="NEW")
			{
				dsOldData	=	ClsDriverDetails.GetDriverDetails(int.Parse(hidDRIVER_ID.Value),int.Parse(hidCLAIM_ID.Value));
				if(dsOldData!=null && dsOldData.Tables.Count>0)
				{
					hidOldData.Value = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dsOldData.Tables[0]);
					if(LOAD_DATA_FLAG)
						LoadData(dsOldData);
					else
						LoadNameDropDown();
					//btnReset.Enabled=false;//Done for Itrack issue 6327
				}
				else
					hidOldData.Value	=	"";
			}
			else
				hidOldData.Value	=	"";
		}
		#endregion

		#region Reset Fields
		private void ResetFields(string strCalledFrom)
		{
			//if(cmbVEHICLE_OWNER.Items.Count>0)
			//	cmbVEHICLE_OWNER.SelectedIndex = 3;			
			txtADDRESS1.Text		=		"";
			txtADDRESS2.Text		=		"";
			txtCITY.Text			=		"";
			cmbSTATE.SelectedIndex	=		-1;
			txtZIP.Text				=		"";
			txtHOME_PHONE.Text		=		"";
			txtWORK_PHONE.Text		=		"";
			txtEXTENSION.Text		=		"";
			txtMOBILE_PHONE.Text	=		"";
			txtNAME.Text			=		"";
			txtDRIVERS_INJURY.Text =		"";
			txtRELATION_INSURED.Text =		"";
			if(strCalledFrom!=strCalledComboFrom)
				cmbNAME.SelectedIndex		=	-1;
			txtDATE_OF_BIRTH.Text = "";
			txtLICENSE_NUMBER.Text = "";
			cmbLICENSE_STATE.SelectedIndex = -1;
//			txtNAME.Text				=	"";
				
		}
		#endregion

		#region LoadData function to load values into controls
		private void LoadData(DataSet dsLoadData)
		{
			try
			{
				if(dsLoadData!=null && dsLoadData.Tables.Count>0&& dsLoadData.Tables[0].Rows.Count>0)
				{
					if(dsLoadData.Tables[0].Rows[0]["VEHICLE_OWNER"]!=null && dsLoadData.Tables[0].Rows[0]["VEHICLE_OWNER"].ToString()!="" && dsLoadData.Tables[0].Rows[0]["VEHICLE_OWNER"].ToString()!="0")
					{
						cmbVEHICLE_OWNER.SelectedValue = dsLoadData.Tables[0].Rows[0]["VEHICLE_OWNER"].ToString();
					
						
					}

										
					txtADDRESS1.Text		=		dsLoadData.Tables[0].Rows[0]["ADDRESS1"].ToString();
					txtADDRESS2.Text		=		dsLoadData.Tables[0].Rows[0]["ADDRESS2"].ToString();
					txtDRIVERS_INJURY.Text  =       dsLoadData.Tables[0].Rows[0]["DRIVERS_INJURY"].ToString();
					txtCITY.Text			=		dsLoadData.Tables[0].Rows[0]["CITY"].ToString();
					if(dsLoadData.Tables[0].Rows[0]["STATE"]!=null && dsLoadData.Tables[0].Rows[0]["STATE"].ToString()!="" && dsLoadData.Tables[0].Rows[0]["STATE"].ToString()!="0")
						cmbSTATE.SelectedValue	=		dsLoadData.Tables[0].Rows[0]["STATE"].ToString();
					txtZIP.Text				=		dsLoadData.Tables[0].Rows[0]["ZIP"].ToString();
					txtHOME_PHONE.Text		=		dsLoadData.Tables[0].Rows[0]["HOME_PHONE"].ToString();
					txtWORK_PHONE.Text		=		dsLoadData.Tables[0].Rows[0]["WORK_PHONE"].ToString();
					txtEXTENSION.Text		=		dsLoadData.Tables[0].Rows[0]["EXTENSION"].ToString();
					txtMOBILE_PHONE.Text	=		dsLoadData.Tables[0].Rows[0]["MOBILE_PHONE"].ToString();
//					txtNAME.Text			=		dsLoadData.Tables[0].Rows[0]["NAME"].ToString();
					hidTYPE_OF_OWNER.Value	=		dsLoadData.Tables[0].Rows[0]["TYPE_OF_OWNER"].ToString();					

					if(dsLoadData.Tables[0].Rows[0]["RELATION_INSURED"]!=null && dsLoadData.Tables[0].Rows[0]["RELATION_INSURED"].ToString()!="")
						txtRELATION_INSURED.Text = dsLoadData.Tables[0].Rows[0]["RELATION_INSURED"].ToString();
					if(dsLoadData.Tables[0].Rows[0]["DATE_OF_BIRTH"]!=null && dsLoadData.Tables[0].Rows[0]["DATE_OF_BIRTH"].ToString()!="")
					txtDATE_OF_BIRTH.Text	=		dsLoadData.Tables[0].Rows[0]["DATE_OF_BIRTH"].ToString().Trim();
					txtLICENSE_NUMBER.Text	=		dsLoadData.Tables[0].Rows[0]["LICENSE_NUMBER"].ToString();
					if(dsLoadData.Tables[0].Rows[0]["COUNTRY"]!=null && dsLoadData.Tables[0].Rows[0]["COUNTRY"].ToString()!="" && dsLoadData.Tables[0].Rows[0]["COUNTRY"].ToString()!="0")
						cmbCOUNTRY.SelectedValue	=		dsLoadData.Tables[0].Rows[0]["COUNTRY"].ToString();
					if(dsLoadData.Tables[0].Rows[0]["LICENSE_STATE"]!=null && dsLoadData.Tables[0].Rows[0]["LICENSE_STATE"].ToString()!="0")
						cmbLICENSE_STATE.SelectedValue = dsLoadData.Tables[0].Rows[0]["LICENSE_STATE"].ToString();
					if(dsLoadData.Tables[0].Rows[0]["VEHICLE_ID"]!=null && dsLoadData.Tables[0].Rows[0]["VEHICLE_ID"].ToString()!="" && dsLoadData.Tables[0].Rows[0]["VEHICLE_ID"].ToString()!="0")
						cmbVEHICLE_ID.SelectedValue = dsLoadData.Tables[0].Rows[0]["VEHICLE_ID"].ToString();
					string strNAME = dsLoadData.Tables[0].Rows[0]["NAME"].ToString();
					txtNAME.Text			=		strNAME;
					hidNAME.Value			=		strNAME;
					//if(dsLoadData.Tables[0].Rows[0]["PURPOSE_OF_USE"]!=null && dsLoadData.Tables[0].Rows[0]["PURPOSE_OF_USE"].ToString()!="")
					//	cmbPURPOSE_OF_USE.SelectedValue = dsLoadData.Tables[0].Rows[0]["PURPOSE_OF_USE"].ToString();
					//if(dsLoadData.Tables[0].Rows[0]["USED_WITH_PERMISSION"]!=null && dsLoadData.Tables[0].Rows[0]["USED_WITH_PERMISSION"].ToString()!="")
					//	cmbUSED_WITH_PERMISSION.SelectedValue = dsLoadData.Tables[0].Rows[0]["USED_WITH_PERMISSION"].ToString();
					//txtDESCRIBE_DAMAGE.Text	=		dsLoadData.Tables[0].Rows[0]["DESCRIBE_DAMAGE"].ToString();
					//if(dsLoadData.Tables[0].Rows[0]["ESTIMATE_AMOUNT"]!=null && dsLoadData.Tables[0].Rows[0]["ESTIMATE_AMOUNT"].ToString()!="0")
					//	txtESTIMATE_AMOUNT.Text=String.Format("{0:,#,###}",Convert.ToInt64(dsLoadData.Tables[0].Rows[0]["ESTIMATE_AMOUNT"]));						
					//if(dsLoadData.Tables[0].Rows[0]["OTHER_VEHICLE_INSURANCE"]!=null && dsLoadData.Tables[0].Rows[0]["OTHER_VEHICLE_INSURANCE"].ToString()!="0")
					//	txtOTHER_VEHICLE_INSURANCE.Text =		dsLoadData.Tables[0].Rows[0]["OTHER_VEHICLE_INSURANCE"].ToString();
	
					LoadNameDropDown();
					if(dsLoadData.Tables[0].Rows[0]["TYPE_OF_DRIVER"]!=null && dsLoadData.Tables[0].Rows[0]["TYPE_OF_DRIVER"].ToString()!="" && dsLoadData.Tables[0].Rows[0]["TYPE_OF_DRIVER"].ToString()!="0")
					{
						cmbDRIVER_TYPE.SelectedIndex = (Convert.ToInt32(dsLoadData.Tables[0].Rows[0]["TYPE_OF_DRIVER"])-1);
						cmbDRIVER_TYPE_SelectedIndexChanged(null,null);
					}
				}
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{}

		}
		#endregion

		private void LoadNameDropDown()
		{
			if(cmbVEHICLE_OWNER.SelectedItem!=null && cmbVEHICLE_OWNER.SelectedItem.Value!="")
			{
				string strVEHICLE_OWNER = cmbVEHICLE_OWNER.SelectedItem.Value;
				//if(strVEHICLE_OWNER== ((int)enumVEHICLE_OWNER.RATED_DRIVER).ToString() || strVEHICLE_OWNER== ((int)enumVEHICLE_OWNER.NAMED_INSURED).ToString())
				//	cmbDRIVER_TYPE_SelectedIndexChanged(null,null);
				if(strVEHICLE_OWNER== ((int)enumVEHICLE_OWNER.INSURED).ToString() || strVEHICLE_OWNER== ((int)enumVEHICLE_OWNER.RATED_DRIVER).ToString() || strVEHICLE_OWNER== ((int)enumVEHICLE_OWNER.NAMED_INSURED).ToString())
				{
					cmbVEHICLE_OWNER_SelectedIndexChanged(null,null);
					ListItem lItem = cmbNAME.Items.FindByText(hidNAME.Value);
					cmbNAME.SelectedIndex = cmbNAME.Items.IndexOf(lItem);
				}
			}	
		}

		
//		private void CheckDataForInsuredVehicle()
//		{
//			
//			if(ClsDriverDetails.OwnerExistsForClaim(int.Parse(hidCLAIM_ID.Value))==1)
//			{
//				//Owner Data exists, lets show Same as Owner dropdown
//				trSAME_AS_OWNER.Visible = true;
//			}
//			else
//			{
//				//Owner Data does not exist, lets hide Same as Owner dropdown
//				trSAME_AS_OWNER.Visible = false;
//			}
//
//		}
		private void GetQueryStringValues()
		{
			if(Request.QueryString["TYPE_OF_OWNER"]!=null && Request.QueryString["TYPE_OF_OWNER"].ToString()!="")
				hidTYPE_OF_OWNER.Value = Request.QueryString["TYPE_OF_OWNER"].ToString();

			if(Request.QueryString["TYPE_OF_DRIVER"]!=null && Request.QueryString["TYPE_OF_DRIVER"].ToString()!="")
				hidTYPE_OF_DRIVER.Value = Request.QueryString["TYPE_OF_DRIVER"].ToString();

			if(Request.QueryString["DRIVER_ID"]!=null && Request.QueryString["DRIVER_ID"].ToString()!="")
				hidDRIVER_ID.Value = Request.QueryString["DRIVER_ID"].ToString();

			if(Request.QueryString["CLAIM_ID"]!=null && Request.QueryString["CLAIM_ID"].ToString()!="")
				hidCLAIM_ID.Value = Request.QueryString["CLAIM_ID"].ToString();

			if(Request.QueryString["CUSTOMER_ID"]!=null && Request.QueryString["CUSTOMER_ID"].ToString()!="" )
				hidCUSTOMER_ID.Value = Request.QueryString["CUSTOMER_ID"].ToString();

			if(Request.QueryString["POLICY_ID"]!=null && Request.QueryString["POLICY_ID"].ToString()!="" )
				hidPOLICY_ID.Value = Request.QueryString["POLICY_ID"].ToString();

			if(Request.QueryString["POLICY_VERSION_ID"]!=null && Request.QueryString["POLICY_VERSION_ID"].ToString()!="" )
				hidPOLICY_VERSION_ID.Value = Request.QueryString["POLICY_VERSION_ID"].ToString();

			if(Request.QueryString["LOB_ID"]!=null && Request.QueryString["LOB_ID"].ToString()!="")
				hidLOB_ID.Value = Request.QueryString["LOB_ID"].ToString();			
			
		}


		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{				
//			this.cmbSAME_AS_OWNER.SelectedIndexChanged += new System.EventHandler(this.cmbSAME_AS_OWNER_SelectedIndexChanged);
			//this.cmbNAMED_INSURED.SelectedIndexChanged += new System.EventHandler(this.cmbNAMED_INSURED_SelectedIndexChanged);
			//this.cmbDRIVERS.SelectedIndexChanged += new System.EventHandler(this.cmbDRIVERS_SelectedIndexChanged);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);//Added for Itrack Issue 6053 on 8 Sept 2009
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);//Added for Itrack Issue 6053 on 8 Sept 2009
			this.Load += new System.EventHandler(this.Page_Load);
			this.cmbVEHICLE_OWNER.SelectedIndexChanged += new System.EventHandler(this.cmbVEHICLE_OWNER_SelectedIndexChanged);			
			this.cmbNAME.SelectedIndexChanged += new System.EventHandler(this.cmbNAME_SelectedIndexChanged);
			this.cmbDRIVER_TYPE.SelectedIndexChanged += new System.EventHandler(this.cmbDRIVER_TYPE_SelectedIndexChanged);			

		}
		#endregion

		

		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			revZIP.ValidationExpression				=		  aRegExpZip;
			revZIP.ErrorMessage						=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("24");
			//			revDEFAULT_PHONE_TO_NOTICE.ValidationExpression	=		  aRegExpEmail;
			//			revDEFAULT_PHONE_TO_NOTICE.ErrorMessage			=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("23");
//			rfvNAME.ErrorMessage						=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("34");						
			//			rfvZIP.ErrorMessage						=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("37");			
			revHOME_PHONE.ValidationExpression	=		  aRegExpPhone;
			revHOME_PHONE.ErrorMessage			=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");						
			revWORK_PHONE.ValidationExpression	=		  aRegExpPhone;
			revWORK_PHONE.ErrorMessage			=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");						
			revMOBILE_PHONE.ValidationExpression	=		  aRegExpPhone;
			revMOBILE_PHONE.ErrorMessage			=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");						
			revDATE_OF_BIRTH.ValidationExpression	=	aRegExpDate;
			revDATE_OF_BIRTH.ErrorMessage			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");						
			csvDATE_OF_BIRTH.ErrorMessage					=		Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"10");
			csvDATE_OF_BIRTH.ErrorMessage					=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("198");
			//rngESTIMATE_AMOUNT.ErrorMessage			=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");			
			rngEXTENSION.ErrorMessage			=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
			rfvVEHICLE_ID.ErrorMessage  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("562");
			rfvCOUNTRY.ErrorMessage				=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("33");			
			//			rfvADDRESS1.ErrorMessage					=		  Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"2");
			//			rfvSTATE.ErrorMessage					=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("35");
			rfvNAME.ErrorMessage						=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("34");			
			rfvNAMES.ErrorMessage						=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("746");		

			
		}

		#endregion

		

		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal=1;	
				//For retreiving the return value of business class save function
				ClsDriverDetails objDriverDetails = new ClsDriverDetails();				

				//Retreiving the form values into model class object
				ClsDriverDetailsInfo objDriverDetailsInfo = GetFormValue();
				
				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objDriverDetailsInfo.CREATED_BY = int.Parse(GetUserId());
					objDriverDetailsInfo.CREATED_DATETIME = DateTime.Now;
					objDriverDetailsInfo.IS_ACTIVE="Y"; 
					
					//Calling the add method of business layer class
					intRetVal = objDriverDetails.Add(objDriverDetailsInfo);

					if(intRetVal>0)
					{
						hidDRIVER_ID.Value = objDriverDetailsInfo.DRIVER_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidIS_ACTIVE.Value = "Y";
						//Don't require to load the data into controls, sending false therefore
						GetOldDataXML(true);
						//btnReset.Enabled=false;//Done for Itrack issue 6327
					}					
					else
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value			=	"2";
					}					
				} // end save case
				else //UPDATE CASE
				{
					//Creating the Model object for holding the Old data
					ClsDriverDetailsInfo objOldDriverDetailsInfo = new ClsDriverDetailsInfo();
					
					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldDriverDetailsInfo,hidOldData.Value);
					
					//Setting those values into the Model object which are not in the page					
					objDriverDetailsInfo.MODIFIED_BY = int.Parse(GetUserId());
					objDriverDetailsInfo.LAST_UPDATED_DATETIME = DateTime.Now;                    
					
					//Updating the record using business layer class object
					intRetVal	= objDriverDetails.Update(objOldDriverDetailsInfo,objDriverDetailsInfo);					
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						//Don't require to load the data into controls, sending false therefore
						//GetOldDataXML(false);
						GetOldDataXML(true);
					}					
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value		=	"1";
					}					
				}
				lblMessage.Visible = true;
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
				//				if(objAgency!= null)
				//					objAgency.Dispose();
			}
		}

		//Added for Itrack Issue 6053 on 8 Sept 2009
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			ClsDriverDetails objDriverDetails = new ClsDriverDetails();
			ClsDriverDetailsInfo objDriverDetailsInfo = new ClsDriverDetailsInfo();
			base.PopulateModelObject(objDriverDetailsInfo,hidOldData.Value);
			objDriverDetailsInfo.MODIFIED_BY = int.Parse(GetUserId());

			try
			{
				int intRetVal;	//For retreiving the return value of business class save function
				intRetVal = objDriverDetails.DeleteDriverDetails(int.Parse(GetClaimID()),int.Parse(hidDRIVER_ID.Value),objDriverDetailsInfo);

				if(intRetVal>0)
				{
					hidFormSaved.Value		=	"1";
					lblDelete.Text		 =	ClsMessages.GetMessage(base.ScreenId,"127");
					trBody.Attributes.Add("style","display:none");
				}
				else if(intRetVal == 0)
				{
					lblDelete.Text		=	ClsMessages.GetMessage(base.ScreenId,"128");
				}
				lblDelete.Visible = true;
				
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
				if(objDriverDetails!= null)
					objDriverDetails.Dispose();
			}
		}
		//Added for Itrack Issue 6053 on 8 Sept 2009
		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			ClsDriverDetails objDriverDetails = new ClsDriverDetails();
			ClsDriverDetailsInfo objDriverDetailsInfo = new ClsDriverDetailsInfo();
			base.PopulateModelObject(objDriverDetailsInfo,hidOldData.Value);
			objDriverDetailsInfo.MODIFIED_BY = int.Parse(GetUserId());

			try
			{
				if(Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("IS_ACTIVE",hidOldData.Value) == "Y")
				{
					objDriverDetails.ActivateDeactivateDriverDetails(int.Parse(hidCLAIM_ID.Value),int.Parse(hidDRIVER_ID.Value),"N",objDriverDetailsInfo);//Done for Itrack Issue 5833 on 21 July 2009
					lblMessage.Text		= ClsMessages.GetMessage(base.ScreenId,"41");
					lblMessage.Visible=true;
				}
				else
				{
					objDriverDetails.ActivateDeactivateDriverDetails(int.Parse(GetClaimID()),int.Parse(hidDRIVER_ID.Value),"Y",objDriverDetailsInfo);//Done for Itrack Issue 5833 on 21 July 2009
					lblMessage.Text		= ClsMessages.GetMessage(base.ScreenId,"40");
					lblMessage.Visible=true;
				}
				DataSet dsOldData= new DataSet();
				dsOldData	=	ClsDriverDetails.GetDriverDetails(int.Parse(hidDRIVER_ID.Value),int.Parse(hidCLAIM_ID.Value));
				if(dsOldData!=null && dsOldData.Tables.Count>0)
				{
					hidOldData.Value = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dsOldData.Tables[0]);
				}
				ClientScript.RegisterStartupScript(this.GetType(),"REFRESHGRID","<script>RefreshWebGrid(1," + hidDRIVER_ID.Value + ");</script>");
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
				if(objDriverDetails!= null)
					objDriverDetails.Dispose();
			}
		}

		private void SetCaptions()
		{			
			capVEHICLE_OWNER.Text					=		objResourceMgr.GetString("cmbVEHICLE_OWNER");
			capNAME.Text							=		objResourceMgr.GetString("txtNAME");
//			capSAME_AS_OWNER.Text					=		objResourceMgr.GetString("cmbSAME_AS_OWNER");
			//capNAMED_INSURED.Text					=		objResourceMgr.GetString("cmbNAMED_INSURED");
//			capNAME.Text							=		objResourceMgr.GetString("txtNAME");
			capADDRESS1.Text						=		objResourceMgr.GetString("txtADDRESS1");
			capDRIVERS_INJURY.Text					=		objResourceMgr.GetString("txtDRIVERS_INJURY");
			capADDRESS2.Text						=		objResourceMgr.GetString("txtADDRESS2");
			capCITY.Text							=		objResourceMgr.GetString("txtCITY");
			capSTATE.Text							=		objResourceMgr.GetString("cmbSTATE");
			capZIP.Text								=		objResourceMgr.GetString("txtZIP");
			capHOME_PHONE.Text						=		objResourceMgr.GetString("txtHOME_PHONE");
			capWORK_PHONE.Text						=		objResourceMgr.GetString("txtWORK_PHONE");
			capEXTENSION.Text						=		objResourceMgr.GetString("txtEXTENSION");
			capMOBILE_PHONE.Text					=		objResourceMgr.GetString("txtMOBILE_PHONE");			
			capRELATION_INSURED.Text				=		objResourceMgr.GetString("txtRELATION_INSURED");			
			capDATE_OF_BIRTH.Text					=		objResourceMgr.GetString("txtDATE_OF_BIRTH");			
			capLICENSE_NUMBER.Text					=		objResourceMgr.GetString("txtLICENSE_NUMBER");			
			capVEHICLE_ID.Text						=		objResourceMgr.GetString("cmbVEHICLE_ID");
			capLICENSE_STATE.Text					=		objResourceMgr.GetString("cmbLICENSE_STATE");			
			//capPURPOSE_OF_USE.Text					=		objResourceMgr.GetString("cmbPURPOSE_OF_USE");			
			//capUSED_WITH_PERMISSION.Text			=		objResourceMgr.GetString("cmbUSED_WITH_PERMISSION");			
			//capDESCRIBE_DAMAGE.Text					=		objResourceMgr.GetString("txtDESCRIBE_DAMAGE");			
			//capESTIMATE_AMOUNT.Text					=		objResourceMgr.GetString("txtESTIMATE_AMOUNT");
			//capDRIVERS.Text							=		objResourceMgr.GetString("cmbDRIVERS");
			//capOTHER_VEHICLE_INSURANCE.Text			=		objResourceMgr.GetString("txtOTHER_VEHICLE_INSURANCE");			
			capCOUNTRY.Text							=		objResourceMgr.GetString("cmbCOUNTRY");
			capDRIVER_TYPE.Text						=		objResourceMgr.GetString("cmbTYPE_OF_DRIVER");
			
		}
	

		#region GetFormValue
		private ClsDriverDetailsInfo GetFormValue()
		{
			ClsDriverDetailsInfo objDriverDetailsInfo = new ClsDriverDetailsInfo();
//			objDriverDetailsInfo.NAME				=	txtNAME.Text.Trim();
			objDriverDetailsInfo.ADDRESS1			=	txtADDRESS1.Text.Trim();
			objDriverDetailsInfo.DRIVERS_INJURY		=   txtDRIVERS_INJURY.Text.Trim();
			objDriverDetailsInfo.ADDRESS2			=	txtADDRESS2.Text.Trim();
			objDriverDetailsInfo.CITY				=	txtCITY.Text.Trim();
			if(cmbSTATE.SelectedItem!=null && cmbSTATE.SelectedItem.Value!="")
				objDriverDetailsInfo.STATE			=	int.Parse(cmbSTATE.SelectedItem.Value);
			
			objDriverDetailsInfo.ZIP				=	txtZIP.Text.Trim();
			objDriverDetailsInfo.HOME_PHONE			=	txtHOME_PHONE.Text.Trim();
			objDriverDetailsInfo.WORK_PHONE			=	txtWORK_PHONE.Text.Trim();
			objDriverDetailsInfo.EXTENSION			=	txtEXTENSION.Text.Trim();
			objDriverDetailsInfo.MOBILE_PHONE		=	txtMOBILE_PHONE.Text.Trim();
			objDriverDetailsInfo.CLAIM_ID			= int.Parse(hidCLAIM_ID.Value);

			//objDriverDetailsInfo.TYPE_OF_DRIVER = int.Parse(hidTYPE_OF_DRIVER.Value);
							
			if(cmbDRIVER_TYPE.SelectedIndex >= 0)
				objDriverDetailsInfo.TYPE_OF_DRIVER = cmbDRIVER_TYPE.SelectedIndex+1;
			
			objDriverDetailsInfo.RELATION_INSURED = txtRELATION_INSURED.Text.Trim();
			if(txtDATE_OF_BIRTH.Text.Trim()!="" && IsDate(txtDATE_OF_BIRTH.Text.Trim()))
				objDriverDetailsInfo.DATE_OF_BIRTH  = Convert.ToDateTime(txtDATE_OF_BIRTH.Text.Trim());
			objDriverDetailsInfo.LICENSE_NUMBER = txtLICENSE_NUMBER.Text.Trim();
			if(cmbLICENSE_STATE.SelectedItem!=null && cmbLICENSE_STATE.SelectedItem.Value!="")
				objDriverDetailsInfo.LICENSE_STATE = int.Parse(cmbLICENSE_STATE.SelectedItem.Value);
			if(cmbVEHICLE_ID.SelectedItem!=null && cmbVEHICLE_ID.SelectedItem.Value!="")
				objDriverDetailsInfo.VEHICLE_ID = int.Parse(cmbVEHICLE_ID.SelectedItem.Value);
			if(cmbCOUNTRY.SelectedItem!=null && cmbCOUNTRY.SelectedItem.Value!="")
				objDriverDetailsInfo.COUNTRY = int.Parse(cmbCOUNTRY.SelectedItem.Value);
			if(hidTYPE_OF_OWNER.Value == ((int)enumTYPE_OF_OWNER.INSURED_VEHICLE).ToString())
			{
				if(cmbVEHICLE_OWNER.SelectedItem!=null && cmbVEHICLE_OWNER.SelectedItem.Value!="")
					objDriverDetailsInfo.VEHICLE_OWNER = int.Parse(cmbVEHICLE_OWNER.SelectedItem.Value);
			}			
			else
				objDriverDetailsInfo.VEHICLE_OWNER = 0;

			objDriverDetailsInfo.TYPE_OF_OWNER = int.Parse(hidTYPE_OF_OWNER.Value);

			if(objDriverDetailsInfo.VEHICLE_OWNER== (int)enumVEHICLE_OWNER.INSURED || objDriverDetailsInfo.VEHICLE_OWNER== (int)enumVEHICLE_OWNER.RATED_DRIVER || objDriverDetailsInfo.VEHICLE_OWNER== (int)enumVEHICLE_OWNER.NAMED_INSURED)							
				objDriverDetailsInfo.NAME		=	cmbNAME.SelectedItem.Text;
			else
				objDriverDetailsInfo.NAME		=	txtNAME.Text.Trim();

			//if(cmbPURPOSE_OF_USE.SelectedItem!=null && cmbPURPOSE_OF_USE.SelectedItem.Value!="")
			//	objDriverDetailsInfo.PURPOSE_OF_USE = cmbPURPOSE_OF_USE.SelectedItem.Value;
			//objDriverDetailsInfo.USED_WITH_PERMISSION = cmbUSED_WITH_PERMISSION.SelectedItem.Value;
			//objDriverDetailsInfo.DESCRIBE_DAMAGE = txtDESCRIBE_DAMAGE.Text.Trim();
			//if(txtESTIMATE_AMOUNT.Text.Trim()!="")
			//	objDriverDetailsInfo.ESTIMATE_AMOUNT = Convert.ToDouble(txtESTIMATE_AMOUNT.Text.Trim());
			//objDriverDetailsInfo.OTHER_VEHICLE_INSURANCE = txtOTHER_VEHICLE_INSURANCE.Text.Trim();
			if(hidDRIVER_ID.Value.ToUpper()=="NEW" || hidDRIVER_ID.Value=="0" || hidDRIVER_ID.Value=="")
				strRowId="NEW";
			else
			{
				strRowId=hidDRIVER_ID.Value;
				objDriverDetailsInfo.DRIVER_ID		=	int.Parse(hidDRIVER_ID.Value);
			}
			return objDriverDetailsInfo;
		}
		#endregion

		private void cmbDRIVER_TYPE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				if(cmbDRIVER_TYPE.SelectedItem!=null && cmbDRIVER_TYPE.SelectedItem.Value!="" && cmbVEHICLE_ID.SelectedItem!=null && cmbVEHICLE_ID.SelectedItem.Value!=null && cmbVEHICLE_ID.SelectedItem.Value!="" && cmbVEHICLE_ID.SelectedItem.Value!="0")//Done for Itrack Issue 6313 on 27 Aug 2009
				{
					if(hidCUSTOMER_ID.Value!="0" && hidPOLICY_ID.Value!="0" && hidPOLICY_VERSION_ID.Value!="0" && hidCUSTOMER_ID.Value!="" && hidPOLICY_ID.Value!="" && hidPOLICY_VERSION_ID.Value!="")
					{
						string strDriverType = "";
						if(cmbDRIVER_TYPE.SelectedItem!=null && cmbDRIVER_TYPE.SelectedItem.Value!="")
							strDriverType = cmbDRIVER_TYPE.SelectedItem.Value;
						DataTable dtNames = ClsOwnerDetails.GetNamedInsured(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value),int.Parse(cmbVEHICLE_OWNER.SelectedItem.Value),int.Parse(hidCLAIM_ID.Value),"",cmbDRIVER_TYPE.SelectedItem.Value,int.Parse(cmbVEHICLE_ID.SelectedItem.Value));//Added for Itrack Issue 6053 on 31 July 2009
						/*if(hidOldData.Value=="" || hidOldData.Value=="0")
								{
									txtNAME.Text = "";
									txtADDRESS1.Text = txtADDRESS2.Text = txtADDRESS2.Text = txtCITY.Text = txtLICENSE_NUMBER.Text = "";
									txtEXTENSION.Text = txtZIP.Text = txtHOME_PHONE.Text = txtRELATION_INSURED.Text = "";
									txtHOME_PHONE.Text = txtMOBILE_PHONE.Text = txtDATE_OF_BIRTH.Text = txtWORK_PHONE.Text = "";
									cmbSTATE.SelectedIndex= cmbCOUNTRY.SelectedIndex = cmbLICENSE_STATE.SelectedIndex =  -1;
								}*/
						
						if(sender!=null && e!=null)
							ResetFields("");
						if(dtNames==null || dtNames.Rows.Count==0)
						{
							cmbNAME.Items.Clear();
						}
						if(dtNames!=null && dtNames.Rows.Count>0)
						{
							cmbNAME.DataSource = dtNames;
							cmbNAME.DataTextField = "NAMED_INSURED";
							cmbNAME.DataValueField = "NAMED_INSURED_ID";
							cmbNAME.DataBind();
							cmbNAME.Items.Insert(0,"");
							ListItem lItem = cmbNAME.Items.FindByText(hidNAME.Value);
							cmbNAME.SelectedIndex = cmbNAME.Items.IndexOf(lItem);
							cmbNAME_SelectedIndexChanged(null,null);
						}
						
					}
				}
				else //Done for Itrack Issue 6313 on 27 Aug 2009
				{
					cmbVEHICLE_OWNER.SelectedIndex=0;
					cmbNAME.Items.Clear();
				}
			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;
				lblMessage.Visible = true;
			}
			finally
			{}
		}
		private void cmbVEHICLE_OWNER_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(cmbVEHICLE_OWNER.SelectedItem!=null && cmbVEHICLE_OWNER.SelectedItem.Value!="")
			{
				if(sender!=null && e!=null)
					ResetFields("");
				string strVEHICLE_OWNER = cmbVEHICLE_OWNER.SelectedItem.Value;
				if(strVEHICLE_OWNER== ((int)enumVEHICLE_OWNER.INSURED).ToString())
				{
				
					if(hidCUSTOMER_ID.Value!="0" && hidPOLICY_ID.Value!="0" && hidPOLICY_VERSION_ID.Value!="0" && hidCUSTOMER_ID.Value!="" && hidPOLICY_ID.Value!="" && hidPOLICY_VERSION_ID.Value!="")
					{							
						DataTable dtNames = ClsOwnerDetails.GetNamedInsured(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value),int.Parse(cmbVEHICLE_OWNER.SelectedItem.Value),int.Parse(hidCLAIM_ID.Value),0);//Added for Itrack Issue 6053 on 31 July 2009
						/*if(hidOldData.Value=="" || hidOldData.Value=="0")
						{
							txtNAME.Text = "";
							txtADDRESS1.Text = txtADDRESS2.Text = txtADDRESS2.Text = txtCITY.Text = txtLICENSE_NUMBER.Text = "";
							txtEXTENSION.Text = txtZIP.Text = txtHOME_PHONE.Text = txtRELATION_INSURED.Text = "";
							txtHOME_PHONE.Text = txtMOBILE_PHONE.Text = txtDATE_OF_BIRTH.Text = txtWORK_PHONE.Text = "";
							cmbSTATE.SelectedIndex= cmbCOUNTRY.SelectedIndex = cmbLICENSE_STATE.SelectedIndex =  -1;
						}*/
						if(dtNames!=null && dtNames.Rows.Count>0)
						{
							cmbNAME.DataSource = dtNames;
							cmbNAME.DataTextField = "NAMED_INSURED";
							cmbNAME.DataValueField = "NAMED_INSURED_ID";
							cmbNAME.DataBind();
							cmbNAME.Items.Insert(0,"");
						}
						
					}

				}
				else if(strVEHICLE_OWNER==((int)enumVEHICLE_OWNER.RATED_DRIVER).ToString() || strVEHICLE_OWNER==((int)enumVEHICLE_OWNER.NAMED_INSURED).ToString()) 
				{
					cmbDRIVER_TYPE.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("WTRPO",null,"Y");
					cmbDRIVER_TYPE.DataTextField	= "LookupDesc";
					cmbDRIVER_TYPE.DataValueField	= "LookupCode";
					cmbDRIVER_TYPE.DataBind();
					cmbDRIVER_TYPE.Items.Add("Other");
					cmbDRIVER_TYPE_SelectedIndexChanged(null,null);
				}
				else
				{
					txtADDRESS1.Enabled = txtADDRESS2.Enabled = txtADDRESS2.Enabled = txtCITY.Enabled = true;
					cmbSTATE.Enabled = txtZIP.Enabled = txtHOME_PHONE.Enabled = txtRELATION_INSURED.Enabled = true;
					txtHOME_PHONE.Enabled = txtMOBILE_PHONE.Enabled = txtDATE_OF_BIRTH.Enabled = txtWORK_PHONE.Enabled = true;
					txtEXTENSION.Enabled = cmbCOUNTRY.Enabled = cmbLICENSE_STATE.Enabled = true;
					txtLICENSE_NUMBER.Enabled = true;
				}
				//Added for Itrack Issue 7069 on 17 Feb 2010
				if(hidOldData.Value!= null && hidOldData.Value != "")
					btnDelete.Enabled = true;
				else
					btnDelete.Enabled = false;
			}			
		}
		private void cmbNAME_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(cmbNAME.SelectedValue !="")
			{
				hidNAME.Value = cmbNAME.SelectedItem.Text;
				if(hidCUSTOMER_ID.Value!="0" && hidPOLICY_ID.Value!="0" && hidPOLICY_VERSION_ID.Value!="0" && hidCUSTOMER_ID.Value!="" && hidPOLICY_ID.Value!="" && hidPOLICY_VERSION_ID.Value!="")
				{
					//DataTable dtNames = ClsOwnerDetails.GetNamedInsured(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value),int.Parse(cmbVEHICLE_OWNER.SelectedItem.Value),int.Parse(hidCLAIM_ID.Value),cmbNAME.SelectedValue);
					string  strcmbDRIVER_TYPE = "";
					if(cmbDRIVER_TYPE.SelectedIndex >= 0)
						strcmbDRIVER_TYPE = cmbDRIVER_TYPE.SelectedItem.Value.ToString();
					DataTable dtNames = ClsOwnerDetails.GetNamedInsured(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value),int.Parse(cmbVEHICLE_OWNER.SelectedItem.Value),int.Parse(hidCLAIM_ID.Value),cmbNAME.SelectedValue,strcmbDRIVER_TYPE,0);//Added for Itrack Issue 6053 on 31 July 2009
					txtNAME.Text = "";

					if(sender!=null && e!=null)
						ResetFields(strCalledComboFrom);
					if(dtNames!=null && dtNames.Rows.Count>0)
					{
						DataRow drTemp = dtNames.Rows[0];

						if(drTemp["ADDRESS1"].ToString().Trim()!="")
						{
							txtADDRESS1.Text = drTemp["ADDRESS1"].ToString().Trim();
							txtADDRESS1.Enabled = false;
						}
						else
							txtADDRESS1.Enabled = true;

						if(drTemp["ADDRESS2"].ToString().Trim()!="")
						{
							txtADDRESS2.Text = drTemp["ADDRESS2"].ToString().Trim();
							txtADDRESS2.Enabled = false;
						}
						else
							txtADDRESS2.Enabled = true;

						if(drTemp["CITY"].ToString().Trim()!="")
						{
							txtCITY.Text = drTemp["CITY"].ToString().Trim();
							txtCITY.Enabled = false;
						}
						else
							txtCITY.Enabled = true;
						
						if (drTemp["STATE"] != null && drTemp["STATE"].ToString().Trim() != "0")
						{
							cmbSTATE.SelectedValue = drTemp["STATE"].ToString().Trim();
							cmbSTATE.Enabled = false;
						}
						else
							cmbSTATE.Enabled = true;
						
						if(drTemp["ZIP_CODE"].ToString().Trim()!="")
						{
							txtZIP.Text = drTemp["ZIP_CODE"].ToString().Trim();
							txtZIP.Enabled = false;
						}
						else
							txtZIP.Enabled = true;

						if(drTemp["COUNTRY"].ToString().Trim()!="")
						{
							cmbCOUNTRY.SelectedValue = drTemp["COUNTRY"].ToString().Trim();
							cmbCOUNTRY.Enabled = false;
						}
						else
							cmbCOUNTRY.Enabled = true;

						if(drTemp["PHONE"].ToString().Trim()!="")
						{
							txtHOME_PHONE.Text = drTemp["PHONE"].ToString().Trim();
							txtHOME_PHONE.Enabled = false;
						}
						else
							txtHOME_PHONE.Enabled = true;

						if(drTemp["MOBILE_PHONE"].ToString().Trim()!="")
						{
							txtMOBILE_PHONE.Text = drTemp["MOBILE_PHONE"].ToString().Trim();
							txtMOBILE_PHONE.Enabled = false;
						}
						else
							txtMOBILE_PHONE.Enabled = true;
						
						
						if(drTemp["DATE_OF_BIRTH"].ToString().Trim()!="")
						{
							txtDATE_OF_BIRTH.Text = drTemp["DATE_OF_BIRTH"].ToString().Trim();
							txtDATE_OF_BIRTH.Enabled = false;
						}
						else
							txtDATE_OF_BIRTH.Enabled = true;
						
						if(drTemp["WORK_PHONE"].ToString().Trim()!="")
						{
							txtWORK_PHONE.Text = drTemp["WORK_PHONE"].ToString().Trim();
							txtWORK_PHONE.Enabled = false;
						}
						else
							txtWORK_PHONE.Enabled = true;

						if(drTemp["EXTENSION"].ToString().Trim()!="")
						{
							txtEXTENSION.Text = drTemp["EXTENSION"].ToString().Trim();
							txtEXTENSION.Enabled = false;
						}
						else
							txtEXTENSION.Enabled = true;

						if(drTemp["RELATION_INSURED"].ToString().Trim()!="")
						{
							txtRELATION_INSURED.Text = drTemp["RELATION_INSURED"].ToString().Trim();
							txtRELATION_INSURED.Enabled = false;
						}
						else
							txtRELATION_INSURED.Enabled = true;

						if(drTemp["RELATION_INSURED"].ToString().Trim()!="")
						{
							txtRELATION_INSURED.Text = drTemp["RELATION_INSURED"].ToString().Trim();
							txtRELATION_INSURED.Enabled = false;
						}
						else
							txtRELATION_INSURED.Enabled = true;		

						if(drTemp["LICENSE_NUMBER"].ToString().Trim()!="")
						{
							txtLICENSE_NUMBER.Text = drTemp["LICENSE_NUMBER"].ToString().Trim();
							txtLICENSE_NUMBER.Enabled = false;
						}
						else
							txtLICENSE_NUMBER.Enabled = true;		

						if(drTemp["LICENSE_STATE"].ToString().Trim()!="")
						{
							cmbLICENSE_STATE.SelectedValue = drTemp["LICENSE_STATE"].ToString().Trim();
							cmbLICENSE_STATE.Enabled = false;
						}
						else
							cmbLICENSE_STATE.Enabled = true;	
						
					}
					else
					{
						txtADDRESS1.Enabled = txtADDRESS2.Enabled = txtADDRESS2.Enabled = txtCITY.Enabled = true;
						cmbSTATE.Enabled = txtZIP.Enabled = txtHOME_PHONE.Enabled = txtRELATION_INSURED.Enabled = true;
						txtHOME_PHONE.Enabled = txtMOBILE_PHONE.Enabled = txtDATE_OF_BIRTH.Enabled = txtWORK_PHONE.Enabled = true;
						txtEXTENSION.Enabled = cmbCOUNTRY.Enabled = cmbLICENSE_STATE.Enabled = txtLICENSE_NUMBER.Enabled = true;
					}

					//Added for Itrack Issue 7069 on 17 Feb 2010
					if(hidOldData.Value!= null && hidOldData.Value != "")
						btnDelete.Enabled = true;
					else
						btnDelete.Enabled = false;
						
				}
			}
			else
				ResetFields(strCalledComboFrom);
		}

		#region LoadDropDowns
		private void LoadDropDowns()
		{
			DataSet dsDriverDropDowns = new DataSet();			
			try
			{
				dsDriverDropDowns = ClsDriverDetails.GetClaimsDriverLookUp(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value),int.Parse(hidCLAIM_ID.Value));
				if(dsDriverDropDowns!=null)
				{
					
					//Loading of states list
					if(dsDriverDropDowns.Tables.Count>0)
					{
						cmbSTATE.DataSource		= dsDriverDropDowns.Tables[0];
						cmbSTATE.DataTextField	= "STATE_NAME";
						cmbSTATE.DataValueField	= "STATE_ID";
						cmbSTATE.DataBind();
						cmbSTATE.Items.Insert(0,"");

						//Loading of states list for licence state dropdown
						cmbLICENSE_STATE.DataSource		= dsDriverDropDowns.Tables[0];
						cmbLICENSE_STATE.DataTextField	= "STATE_NAME";
						cmbLICENSE_STATE.DataValueField	= "STATE_ID";
						cmbLICENSE_STATE.DataBind();
						cmbLICENSE_STATE.Items.Insert(0,"");
					}
					//Loading of yes/no options for Used with permission dropdown
					/*if(dsDriverDropDowns.Tables.Count>1)
					{
						cmbUSED_WITH_PERMISSION.DataSource = dsDriverDropDowns.Tables[1];
						cmbUSED_WITH_PERMISSION.DataTextField="LOOKUP_VALUE_DESC"; 
						cmbUSED_WITH_PERMISSION.DataValueField="LOOKUP_VALUE_CODE";
						cmbUSED_WITH_PERMISSION.DataBind();	
					}*/				
	
					//Load List of Owners for the current claim_id
//					if(dsDriverDropDowns.Tables.Count>2)
//					{												
//						cmbSAME_AS_OWNER.DataSource		=  dsDriverDropDowns.Tables[2];
//						cmbSAME_AS_OWNER.DataTextField	= "NAME";
//						cmbSAME_AS_OWNER.DataValueField	= "OWNER_ID";
//						cmbSAME_AS_OWNER.DataBind();
//						cmbSAME_AS_OWNER.Items.Insert(0,"No");
//					}

					//Load NAMED_INSURED for the customer,policy and policy_version_id 
					/*if(dsDriverDropDowns.Tables.Count>3)
					{
						cmbNAMED_INSURED.DataSource		=  dsDriverDropDowns.Tables[3];
						cmbNAMED_INSURED.DataTextField	= "NAMED_INSURED";
						cmbNAMED_INSURED.DataValueField	= "NAMED_INSURED_ID";
						cmbNAMED_INSURED.DataBind();
						cmbNAMED_INSURED.Items.Insert(0,"");
					}*/

					//Load list of drivers for current policy
					/*if(dsDriverDropDowns.Tables.Count>4)
					{
						cmbDRIVERS.DataSource		=  dsDriverDropDowns.Tables[4];
						cmbDRIVERS.DataTextField	= "DRIVER_NAME";
						cmbDRIVERS.DataValueField	= "DRIVER_ID";
						cmbDRIVERS.DataBind();
						cmbDRIVERS.Items.Insert(0,"");
					}*/
					//Load Purpose of Use Dropdown
					/*if(dsDriverDropDowns.Tables.Count>5)
					cmbPURPOSE_OF_USE.DataSource = dsDriverDropDowns.Tables[5];
					cmbPURPOSE_OF_USE.DataTextField="LOOKUP_VALUE_DESC"; 
					cmbPURPOSE_OF_USE.DataValueField="LOOKUP_UNIQUE_ID";
					cmbPURPOSE_OF_USE.DataBind();*/

					//Load Relation to Insured Dropdown
//					if(dsDriverDropDowns.Tables.Count>6)
//					cmbRELATION_INSURED.DataSource = dsDriverDropDowns.Tables[6];
//					cmbRELATION_INSURED.DataTextField="DETAIL_TYPE_DESCRIPTION"; 
//					cmbRELATION_INSURED.DataValueField="DETAIL_TYPE_ID";
//					cmbRELATION_INSURED.DataBind();		
				}
				//Load Owner of Vehicle dropdown only in the case of Insured Vehicle
				//if(hidTYPE_OF_OWNER.Value == ((int)enumTYPE_OF_OWNER.INSURED_VEHICLE).ToString())
				//{
					cmbVEHICLE_OWNER.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CLVO");
					cmbVEHICLE_OWNER.DataTextField	= "LookupDesc";
					cmbVEHICLE_OWNER.DataValueField	= "LookupID";
					cmbVEHICLE_OWNER.DataBind();		
					Cms.BusinessLayer.BlCommon.ClsCommon.RemoveOptionFromDropdownByValue(cmbVEHICLE_OWNER,"11752");
					cmbVEHICLE_OWNER.Items.Insert(0,"");
					//cmbVEHICLE_OWNER.SelectedIndex=1;
					//Test
					if(cmbVEHICLE_OWNER.SelectedItem!=null && cmbVEHICLE_OWNER.SelectedItem.Value!="")
					{
						string strVEHICLE_OWNER = cmbVEHICLE_OWNER.SelectedItem.Value;
						if(strVEHICLE_OWNER== ((int)enumVEHICLE_OWNER.RATED_DRIVER).ToString() || strVEHICLE_OWNER== ((int)enumVEHICLE_OWNER.NAMED_INSURED).ToString())
							cmbDRIVER_TYPE_SelectedIndexChanged(null,null);
						if(strVEHICLE_OWNER== ((int)enumVEHICLE_OWNER.INSURED).ToString() || strVEHICLE_OWNER== ((int)enumVEHICLE_OWNER.RATED_DRIVER).ToString() || strVEHICLE_OWNER== ((int)enumVEHICLE_OWNER.NAMED_INSURED).ToString())
						{
							cmbNAME.DataSource= dsDriverDropDowns.Tables[2];
							cmbNAME.DataTextField	= "NAME";
							cmbNAME.DataValueField	= "OWNER_ID";
							cmbNAME.DataBind();
							cmbNAME.Items.Insert(0,"");
//							cmbVEHICLE_OWNER_SelectedIndexChanged(null,null);
//							ListItem lItem = cmbNAME.Items.FindByText(hidNAME.Value);
//							cmbNAME.SelectedIndex = cmbNAME.Items.IndexOf(lItem);
						}
					}
					//LoadNameDropDown();
					 //Test
				//}
				DataTable dt				= Cms.CmsWeb.ClsFetcher.Country;
				cmbCOUNTRY.DataSource		= dt;
				cmbCOUNTRY.DataTextField	= "COUNTRY_NAME";
				cmbCOUNTRY.DataValueField	= "COUNTRY_ID";
				cmbCOUNTRY.DataBind();
				ClsInsuredVehicle objInsuredVehicle = new ClsInsuredVehicle();
				DataTable dtPolicyVehicles = objInsuredVehicle.GetClaimVehicles(int.Parse(hidCLAIM_ID.Value));
				/*cmbVEHICLE_ID.DataSource = dtPolicyVehicles;
				cmbVEHICLE_ID.DataTextField = "VIN";
				cmbVEHICLE_ID.DataValueField = "VEHICLE_ID";
				cmbVEHICLE_ID.DataBind();
				cmbVEHICLE_ID.Items.Insert(0,"");*/
				if(dtPolicyVehicles!=null && dtPolicyVehicles.Rows.Count>0)
				{
					//Add an empty listitem when there are multiple vehicles added
					/*Commented by Asfa(24-July-2008) - iTrack #4538
					if(dtPolicyVehicles.Rows.Count>1)
						cmbVEHICLE_ID.Items.Add(new ListItem("",""));
					*/
					foreach(DataRow dtRow in dtPolicyVehicles.Rows)
					{
						string sVal = "";
						string sText = "";
						for(int i=0;i<dtPolicyVehicles.Columns.Count-1;i++)
							sText+= dtRow[i].ToString() + "-";

						sText = sText.Substring(0,sText.Length-1);
						if(dtRow["VEHICLE_ID"]!=null && dtRow["VEHICLE_ID"].ToString()!="")
							sVal = dtRow["VEHICLE_ID"].ToString();
						//sVal=sVal.Substring(0,sVal.Length-5).Trim();
						//sText = dtRow["VEHICLE_ID"].ToString() + "-" + dtRow["VIN"].ToString()  + "-" + dtRow["VEHICLE_YEAR"].ToString()  + "-" + dtRow["MAKE"].ToString()  + "-" + dtRow["MODEL"].ToString()  + "-" + dtRow["BODY_TYPE"].ToString();
						//ListItem lItem = new ListItem(dtRow["VIN"].ToString(),sVal);
						ListItem lItem = new ListItem(sText,sVal);
						cmbVEHICLE_ID.Items.Add(lItem);
					}
					cmbVEHICLE_ID.Items.Insert(0,"");
					lblVEHICLE_ID.Visible = false;
				}
				else
				{
					if(hidLOB_ID.Value == "4")
						lblVEHICLE_ID.Text	=  "No Boat added until now. Please click <a href='javascript:Redirect_boat();' onclick='Redirect_boat();'>here</a> to add boat";
					else 
						lblVEHICLE_ID.Text	=  "No vehicle added until now. Please click <a href='javascript:Redirect();' onclick='Redirect();'>here</a> to add vehicle";
					cmbVEHICLE_ID.Attributes.Add("style","display:none");
					rfvVEHICLE_ID.Attributes.Add("style","display:none");
				}
			}
			catch (Exception ex)
			{
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			finally
			{
				if(dsDriverDropDowns!=null)
					dsDriverDropDowns.Dispose();
			}
			
			
		}
		#endregion

		

//		private void cmbNAMED_INSURED_SelectedIndexChanged(object sender, System.EventArgs e)
//		{
//			try
//			{
//				if(cmbNAMED_INSURED.SelectedItem!=null && cmbNAMED_INSURED.SelectedItem.Value!="")
//				{
//					int intNAMED_INSURED_ID = int.Parse(cmbNAMED_INSURED.SelectedItem.Value);
//					//Get Data for the Named Insured 
//					//Vehicle Owner parameter is being 0 to indicate that the request is coming for driver details screen
//					DataTable dtNamedInsuredDetails = ClsOwnerDetails.GetNamedInsureDetails(0,intNAMED_INSURED_ID);
//					if(dtNamedInsuredDetails!=null & dtNamedInsuredDetails.Rows.Count>0)
//					{
//						if(dtNamedInsuredDetails.Rows[0]["NAME"]!=null && dtNamedInsuredDetails.Rows[0]["NAME"].ToString()!="")
//							txtNAME.Text = dtNamedInsuredDetails.Rows[0]["NAME"].ToString();
//						if(dtNamedInsuredDetails.Rows[0]["ADDRESS1"]!=null && dtNamedInsuredDetails.Rows[0]["ADDRESS1"].ToString()!="")
//							txtADDRESS1.Text = dtNamedInsuredDetails.Rows[0]["ADDRESS1"].ToString();
//						if(dtNamedInsuredDetails.Rows[0]["ADDRESS2"]!=null && dtNamedInsuredDetails.Rows[0]["ADDRESS2"].ToString()!="")
//							txtADDRESS2.Text = dtNamedInsuredDetails.Rows[0]["ADDRESS2"].ToString();
//						if(dtNamedInsuredDetails.Rows[0]["CITY"]!=null && dtNamedInsuredDetails.Rows[0]["CITY"].ToString()!="")
//							txtCITY.Text = dtNamedInsuredDetails.Rows[0]["CITY"].ToString();
//						if(dtNamedInsuredDetails.Rows[0]["STATE"]!=null && dtNamedInsuredDetails.Rows[0]["STATE"].ToString()!="")
//							cmbSTATE.SelectedValue = dtNamedInsuredDetails.Rows[0]["STATE"].ToString();
//						if(dtNamedInsuredDetails.Rows[0]["ZIP"]!=null && dtNamedInsuredDetails.Rows[0]["ZIP"].ToString()!="")
//							txtZIP.Text = dtNamedInsuredDetails.Rows[0]["ZIP"].ToString();
//						if(dtNamedInsuredDetails.Rows[0]["HOME_PHONE"]!=null && dtNamedInsuredDetails.Rows[0]["HOME_PHONE"].ToString()!="")
//							txtHOME_PHONE.Text = dtNamedInsuredDetails.Rows[0]["HOME_PHONE"].ToString();
//						if(dtNamedInsuredDetails.Rows[0]["WORK_PHONE"]!=null && dtNamedInsuredDetails.Rows[0]["WORK_PHONE"].ToString()!="")
//							txtWORK_PHONE.Text = dtNamedInsuredDetails.Rows[0]["WORK_PHONE"].ToString();
//						if(dtNamedInsuredDetails.Rows[0]["EXTENSION"]!=null && dtNamedInsuredDetails.Rows[0]["EXTENSION"].ToString()!="")
//							txtEXTENSION.Text = dtNamedInsuredDetails.Rows[0]["EXTENSION"].ToString();
//						if(dtNamedInsuredDetails.Rows[0]["MOBILE_PHONE"]!=null && dtNamedInsuredDetails.Rows[0]["MOBILE_PHONE"].ToString()!="")
//							txtMOBILE_PHONE.Text = dtNamedInsuredDetails.Rows[0]["MOBILE_PHONE"].ToString();					
//					}
//				}
//			}
//			catch(Exception ex)
//			{
//				lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
//				lblMessage.Visible = true;
//			}
//			finally{}
//		}

		/*private void cmbSAME_AS_OWNER_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataSet dsOwnerDetails = new DataSet();
			try
			{
				if(cmbSAME_AS_OWNER.SelectedItem!=null && cmbSAME_AS_OWNER.SelectedItem.Value!="" && cmbSAME_AS_OWNER.SelectedItem.Value.ToUpper()!="NO")
				{
					dsOwnerDetails = ClsOwnerDetails.GetOwnerDetails(int.Parse(cmbSAME_AS_OWNER.SelectedItem.Value),int.Parse(hidCLAIM_ID.Value));

					if(dsOwnerDetails!=null && dsOwnerDetails.Tables.Count>0)
					{
						
						txtADDRESS1.Text		=		dsOwnerDetails.Tables[0].Rows[0]["ADDRESS1"].ToString();
						txtADDRESS2.Text		=		dsOwnerDetails.Tables[0].Rows[0]["ADDRESS2"].ToString();
						txtCITY.Text			=		dsOwnerDetails.Tables[0].Rows[0]["CITY"].ToString();
						if(dsOwnerDetails.Tables[0].Rows[0]["STATE"]!=null && dsOwnerDetails.Tables[0].Rows[0]["STATE"].ToString()!="" && dsOwnerDetails.Tables[0].Rows[0]["STATE"].ToString()!="0")
							cmbSTATE.SelectedValue	=		dsOwnerDetails.Tables[0].Rows[0]["STATE"].ToString();
						txtZIP.Text				=		dsOwnerDetails.Tables[0].Rows[0]["ZIP"].ToString();
						txtHOME_PHONE.Text		=		dsOwnerDetails.Tables[0].Rows[0]["HOME_PHONE"].ToString();
						txtWORK_PHONE.Text		=		dsOwnerDetails.Tables[0].Rows[0]["WORK_PHONE"].ToString();
						txtEXTENSION.Text		=		dsOwnerDetails.Tables[0].Rows[0]["EXTENSION"].ToString();
						txtMOBILE_PHONE.Text	=		dsOwnerDetails.Tables[0].Rows[0]["MOBILE_PHONE"].ToString();	
						txtNAME.Text			=		dsOwnerDetails.Tables[0].Rows[0]["NAME"].ToString();

						txtNAME.Enabled = false;
						txtADDRESS1.Enabled = false;
						txtADDRESS2.Enabled = false;
						txtCITY.Enabled = false;
						cmbSTATE.Enabled = false;
						txtZIP.Enabled = false;
						txtHOME_PHONE.Enabled = false;
						cmbCOUNTRY.Enabled = false;

					}
				}
			}
			catch(Exception ex)
			{
				lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
				lblMessage.Visible = true;
			}
			finally{}
		}*/

		/*private void cmbDRIVERS_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataTable dtDriverDetails = new DataTable();
			try
			{
				if(cmbDRIVERS.SelectedItem!=null && cmbDRIVERS.SelectedItem.Value!="")
				{
					dtDriverDetails = ClsDriverDetails.GetPolicyDriversDetails(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value),int.Parse(cmbDRIVERS.SelectedItem.Value));

					if(dtDriverDetails!=null && dtDriverDetails.Rows.Count>0)
					{
						
						txtADDRESS1.Text		=		dtDriverDetails.Rows[0]["DRIVER_ADD1"].ToString();
						txtADDRESS2.Text		=		dtDriverDetails.Rows[0]["DRIVER_ADD2"].ToString();
						txtCITY.Text			=		dtDriverDetails.Rows[0]["DRIVER_CITY"].ToString();
						if(dtDriverDetails.Rows[0]["DRIVER_STATE"]!=null && dtDriverDetails.Rows[0]["DRIVER_STATE"].ToString()!="" && dtDriverDetails.Rows[0]["DRIVER_STATE"].ToString()!="0")
							cmbSTATE.SelectedValue	=		dtDriverDetails.Rows[0]["DRIVER_STATE"].ToString();
						txtZIP.Text				=		dtDriverDetails.Rows[0]["DRIVER_ZIP"].ToString();						
						txtNAME.Text			=		dtDriverDetails.Rows[0]["DRIVER_NAME"].ToString();
						txtDATE_OF_BIRTH.Text	=		dtDriverDetails.Rows[0]["DRIVER_DOB"].ToString().Trim();
						txtLICENSE_NUMBER.Text		=		dtDriverDetails.Rows[0]["DRIVER_DRIV_LIC"].ToString();
						if(dtDriverDetails.Rows[0]["DRIVER_LIC_STATE"]!=null && dtDriverDetails.Rows[0]["DRIVER_LIC_STATE"].ToString()!="" && dtDriverDetails.Rows[0]["DRIVER_LIC_STATE"].ToString()!="0")
							cmbLICENSE_STATE.SelectedValue	=		dtDriverDetails.Rows[0]["DRIVER_LIC_STATE"].ToString();
						
					}
				}
			}
			catch(Exception ex)
			{
				lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
				lblMessage.Visible = true;
			}
			finally{}
		}*/
		
	}
}
