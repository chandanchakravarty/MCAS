
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
using Cms.Model.Maintenance.Accounting;
using Cms.BusinessLayer.BlCommon.Accounting;
using Cms.BusinessLayer.BlCommon;  
using Cms.CmsWeb.Controls; 
using Cms.ExceptionPublisher.ExceptionManagement; 

namespace Cms.CmsWeb.Accounting
{
	public class InflationGuardDetails :  Cms.CmsWeb.cmsbase
	{
		#region "Control variables"
		protected System.Web.UI.WebControls.Label capSTATE_ID;
		protected System.Web.UI.WebControls.Label capLOB_ID;
		protected System.Web.UI.WebControls.Label capEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.Label capFACTOR;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidZIP_CODE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidINFLATION_ID;
		protected System.Web.UI.WebControls.DropDownList cmbSTATE_ID;
		protected System.Web.UI.WebControls.DropDownList cmbLOB_ID;
		protected System.Web.UI.WebControls.TextBox txtEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.TextBox txtFACTOR;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSTATE_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOB_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvFACTOR;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revFACTOR;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.CustomValidator csvFACTOR;
        protected System.Web.UI.WebControls.Label capMessages;
        protected System.Web.UI.WebControls.Label capCOUNTRY;



		#endregion

		#region Local form variables
		System.Resources.ResourceManager objResourceMgr;
		protected System.Web.UI.WebControls.Label lblCOUNTRY_NAME;

		const string COUNTRYID = "1";//countryid hard coded for USA for save case 
		const string COUNTRY="USA";
		public string primaryKeyValues="";
	
		protected System.Web.UI.WebControls.HyperLink hlkFROM_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkTO_DATE;

		ClsRegCommSetup_Agency  objBLInflation ;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEXPIRY_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.Label capEXPIRY_DATE;
		protected System.Web.UI.WebControls.TextBox txtEXPIRY_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkEXPIRY_DATE;
		protected System.Web.UI.WebControls.Label lblZIP_CODE;
		protected System.Web.UI.WebControls.TextBox txtZIP_CODE;
		protected System.Web.UI.WebControls.Label capZIP_CODE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEXPIRY_DATE;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tbDetail;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revZIP_CODE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.WebControls.CustomValidator csvZIP_CODE;
		protected System.Web.UI.WebControls.CustomValidator csvEXPIRY_DATE;
		//protected System.Web.UI.WebControls.CustomValidator csvCHECK_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvZIP_CODE;
		#endregion

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			btnReset.Attributes.Add("onclick","javascript:return formReset();");
			base.ScreenId="365_0";

			lblMessage.Visible = false;
			SetErrorMessages();
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");

			# region Button Permission Settings
			btnReset.CmsButtonClass					=	CmsButtonType.Execute;
			btnReset.PermissionString				=	gstrSecurityXML;

			btnSave.CmsButtonClass					=	CmsButtonType.Write;
			btnSave.PermissionString				=	gstrSecurityXML;

			btnDelete.CmsButtonClass				=	CmsButtonType.Delete;
			btnDelete.PermissionString				=	gstrSecurityXML;
			

			#endregion
			
			hlkFROM_DATE.Attributes.Add("OnClick","fPopCalendar(document.INFLATION_COST_FACTORS.txtEFFECTIVE_DATE,document.INFLATION_COST_FACTORS.txtEFFECTIVE_DATE)");
			hlkEXPIRY_DATE.Attributes.Add("OnClick","fPopCalendar(document.INFLATION_COST_FACTORS.txtEXPIRY_DATE,document.INFLATION_COST_FACTORS.txtEXPIRY_DATE)");
//			btnSave.Attributes.Add("OnClick","javascript:GetZipForState();");
			GetQueryStringValues();
			if(!Page.IsPostBack)
			{

				SetCaptions();
				cmbSTATE_ID.Items.Insert(0,"");
				cmbLOB_ID.Items.Insert(0,"");
				GetOldDataXMLInfo();
				lblCOUNTRY_NAME.Text = COUNTRY;
		
			}
		}
		#endregion

		#region Get QueryString Values
		private void GetQueryStringValues()
		{
			if(Request.QueryString["INFLATION_ID"]!=null && Request.QueryString["INFLATION_ID"].ToString()!="")
				hidINFLATION_ID.Value = Request.QueryString["INFLATION_ID"].ToString();
			
			if(Request.QueryString["STATE_ID"]!=null && Request.QueryString["STATE_ID"].ToString()!="")
				hidSTATE_ID.Value = Request.QueryString["STATE_ID"].ToString();

			if(Request.QueryString["LOB_ID"]!=null && Request.QueryString["LOB_ID"].ToString()!="")
				hidLOB_ID.Value = Request.QueryString["LOB_ID"].ToString();
			
			if(Request.QueryString["ZIP_CODE"]!=null && Request.QueryString["ZIP_CODE"].ToString()!="")
				hidZIP_CODE.Value = Request.QueryString["ZIP_CODE"].ToString();

			
		}
			
		#endregion

		#region	GetOldDataXMLInfo 
		protected void GetOldDataXMLInfo()
		{
			if(hidSTATE_ID.Value != "" && hidLOB_ID.Value != "" && hidZIP_CODE.Value != "")
				hidOldData.Value =  ClsRegCommSetup_Agency.GetXmlForInflationPageControls(int.Parse(hidSTATE_ID.Value),int.Parse(hidLOB_ID.Value),int.Parse(hidZIP_CODE.Value),int.Parse(hidINFLATION_ID.Value));
		}
		#endregion

		#region Set Error Messages
		private void SetErrorMessages()
		{
			rfvSTATE_ID.ErrorMessage					=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("35");
			rfvLOB_ID.ErrorMessage						=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("865");
			rfvEFFECTIVE_DATE.ErrorMessage				=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("95");
			rfvFACTOR.ErrorMessage						=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("866");
			rfvEXPIRY_DATE.ErrorMessage					=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("96");
			rfvZIP_CODE.ErrorMessage					=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("789");
			revEFFECTIVE_DATE.ValidationExpression		=	aRegExpDate;
			revEXPIRY_DATE.ValidationExpression			=	aRegExpDate;
			revFACTOR.ValidationExpression				=	aRegExpDoublePositiveNonZeroNotLessThanOne;
			revEFFECTIVE_DATE.ErrorMessage				=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("179");
			revEXPIRY_DATE.ErrorMessage					=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("179");
			revFACTOR.ErrorMessage						=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("952");
			//revZIP_CODE.ValidationExpression			= aRegExpZip;
			//revZIP_CODE.ErrorMessage					= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("24");
//			csvCHECK_DATE.ErrorMessage					= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("867");
		}
		#endregion

		#region Set Captions
		private void SetCaptions()
		{
			objResourceMgr = new System.Resources.ResourceManager("cms.cmsWeb.Accounting.InflationGuardDetails" ,System.Reflection.Assembly.GetExecutingAssembly());
			capSTATE_ID.Text								=		objResourceMgr.GetString("cmbSTATE_ID");
			capLOB_ID.Text									=		objResourceMgr.GetString("cmbLOB_ID");
			capEFFECTIVE_DATE.Text							=		objResourceMgr.GetString("txtEFFECTIVE_DATE");
			capEXPIRY_DATE.Text								=		objResourceMgr.GetString("txtEXPIRY_DATE");
			capFACTOR.Text   =		objResourceMgr.GetString("txtFACTOR");
            capCOUNTRY.Text = objResourceMgr.GetString("capCOUNTRY");
		}

		#endregion

		#region GetFormValue
			protected Cms.Model.Maintenance.Accounting.ClsInflationGuardSetup GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsInflationGuardSetup objInflationGuardInfo;
			objInflationGuardInfo = new ClsInflationGuardSetup();
			objInflationGuardInfo.CREATED_BY			=	int.Parse(GetUserId());
			objInflationGuardInfo.STATE_ID				=	int.Parse(cmbSTATE_ID.SelectedValue);
			objInflationGuardInfo.LOB_ID				=	int.Parse(cmbLOB_ID.SelectedValue);
			objInflationGuardInfo.ZIP_CODE				=	int.Parse(txtZIP_CODE.Text);
			objInflationGuardInfo.EFFECTIVE_DATE		=	DateTime.Parse(txtEFFECTIVE_DATE.Text);
			if(txtEXPIRY_DATE.Text.Trim()!="")								
				objInflationGuardInfo.EXPIRY_DATE			=	DateTime.Parse(txtEXPIRY_DATE.Text);

			objInflationGuardInfo.FACTOR				=	double.Parse(txtFACTOR.Text);
		
			return objInflationGuardInfo;
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

		#region "Web Event Handlers (SAVE BUTTON)"
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;
				int InflationId=0;
				objBLInflation = new  ClsRegCommSetup_Agency(true);
				ClsInflationGuardSetup objInflationGuardInfo = GetFormValue();
				if(hidOldData.Value=="" || hidOldData.Value=="0")
				{
					objInflationGuardInfo.IS_ACTIVE			= "Y";
					intRetVal = objBLInflation.AddInflation(objInflationGuardInfo, out InflationId);

					if(intRetVal>0)
					{
						lblMessage.Text				=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						//hidINFLATION_ID.Value		=	objInflationGuardInfo.INFLATION_ID.ToString();
						hidINFLATION_ID.Value		=	InflationId.ToString();
						hidSTATE_ID.Value			=	objInflationGuardInfo.STATE_ID.ToString();
						hidLOB_ID.Value				=	objInflationGuardInfo.LOB_ID.ToString();
						hidZIP_CODE.Value			=	objInflationGuardInfo.ZIP_CODE.ToString();
						primaryKeyValues = hidSTATE_ID.Value  + "^" + hidLOB_ID.Value + "^" + hidZIP_CODE.Value;  
						GetOldDataXMLInfo();
					}
					else if(intRetVal < 0)
					{
						lblMessage.Text				=		ClsMessages.FetchGeneralMessage("864");
						hidFormSaved.Value			=		"2";
					}
					/*else if(intRetVal == -2)
					{
						lblMessage.Text				=		ClsMessages.FetchGeneralMessage("864");
						hidFormSaved.Value			=		"2";
					}
					else if(intRetVal == -3)
					{
						lblMessage.Text				=		ClsMessages.FetchGeneralMessage("864");
						hidFormSaved.Value			=		"2";
					}
					else if(intRetVal == -4)
					{
						lblMessage.Text				=		ClsMessages.FetchGeneralMessage("864");
						hidFormSaved.Value			=		"2";
					}*/
					else
					{
						lblMessage.Text				=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value			=	"2";
					}
					lblMessage.Visible = true;
				} 
				else //UPDATE CASE
				{

					ClsInflationGuardSetup objOldInflationGuardInfo;
					objOldInflationGuardInfo = new ClsInflationGuardSetup();
					base.PopulateModelObject(objOldInflationGuardInfo,hidOldData.Value);

					objInflationGuardInfo.INFLATION_ID = int.Parse(hidINFLATION_ID.Value);
					intRetVal	= objBLInflation.UpdateInflation(objOldInflationGuardInfo,objInflationGuardInfo);
					
					if( intRetVal > 0 )	
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						//hidINFLATION_ID.Value		=	objInflationGuardInfo.INFLATION_ID.ToString();
						hidSTATE_ID.Value			=	objInflationGuardInfo.STATE_ID.ToString();
						hidLOB_ID.Value				=	objInflationGuardInfo.LOB_ID.ToString();
						hidZIP_CODE.Value			=	objInflationGuardInfo.ZIP_CODE.ToString();
						primaryKeyValues = hidSTATE_ID.Value  + "^" + hidLOB_ID.Value + "^" + hidZIP_CODE.Value;  
						GetOldDataXMLInfo();
					}
					else if(intRetVal < 0)
					{
						lblMessage.Text				=		ClsMessages.FetchGeneralMessage("864");
						hidFormSaved.Value			=		"2";
					}
					else
					{
						lblMessage.Text				=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value			=	"2";
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
				if(objBLInflation!= null)
					objBLInflation.Dispose();
			}
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	
				objBLInflation = new  ClsRegCommSetup_Agency(true);
				ClsInflationGuardSetup objInflationGuardInfo = GetFormValue();
				objInflationGuardInfo.INFLATION_ID = int.Parse(hidINFLATION_ID.Value);

				intRetVal = objBLInflation.DeleteInflation(objInflationGuardInfo);

				if(intRetVal>0)
				{
					lblMessage.Text				=	ClsMessages.GetMessage(base.ScreenId,"5");
					hidFormSaved.Value			=	"5";
					lblMessage.Visible = true;
					tbDetail.Visible = false;
					hidINFLATION_ID.Value		=	"-1";
					primaryKeyValues = hidSTATE_ID.Value  + "^" + hidLOB_ID.Value + "^" + hidZIP_CODE.Value;  
					ClientScript.RegisterStartupScript(this.GetType(),"REFRESHGRID","<script>RefreshWebGrid('1','" + primaryKeyValues + "',true);</script>");
					primaryKeyValues = hidSTATE_ID.Value  + "^" + hidLOB_ID.Value + "^" + hidZIP_CODE.Value;  
	
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
				if(objBLInflation!= null)
					objBLInflation.Dispose();
			}
		}

		#endregion

		
	}
}
