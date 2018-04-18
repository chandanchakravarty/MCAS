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
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb; 
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer.BlCommon.Reinsurance;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Maintenance.Reinsurance;
using Cms.ExceptionPublisher.ExceptionManagement;

namespace Cms.CmsWeb.Maintenance.Reinsurance
{
	/// <summary>
	/// Summary description for AddReinsurancePremiumBuilder.
	/// </summary>
	public class AddReinsurancePremiumBuilder :Cms.CmsWeb.cmsbase
	{
		#region page controls declaration
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.TextBox txtEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEFFECTIVE_DATE;

		protected System.Web.UI.WebControls.Label capEXPIRY_DATE;
		protected System.Web.UI.WebControls.TextBox txtEXPIRY_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkEXPIRY_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEXPIRY_DATE;

		protected System.Web.UI.WebControls.Label capLAYER;
		protected System.Web.UI.WebControls.TextBox txtLAYER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLAYER;
		protected System.Web.UI.WebControls.RegularExpressionValidator revLAYER;
		protected System.Web.UI.WebControls.CustomValidator csvLAYER;


		protected System.Web.UI.WebControls.RegularExpressionValidator revEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEXPIRY_DATE;
		protected System.Web.UI.WebControls.Label capLOB_ID;
		protected System.Web.UI.WebControls.DropDownList cmbLOB_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOB_ID;
//		protected System.Web.UI.WebControls.Label capCATEGORY;
//		protected System.Web.UI.WebControls.ListBox cmbFROMCATEGORY;
//		protected System.Web.UI.WebControls.Button btnSELECT;
//		protected System.Web.UI.WebControls.Button btnDESELECT;
//		protected System.Web.UI.WebControls.Label capRECIPIENTS;
//		protected System.Web.UI.WebControls.ListBox cmbCATEGORY;
//		protected System.Web.UI.WebControls.CustomValidator csvCATEGORY;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlTable tblBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldTempData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPREMIUM_BUILDER_ID;
		protected System.Web.UI.WebControls.Label capCONTRACT;
		protected System.Web.UI.WebControls.TextBox txtCONTRACT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCONTRACT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCONTRACT;

		protected System.Web.UI.WebControls.Label capTOTAL_INSURANCE_FROM;
		protected System.Web.UI.WebControls.TextBox txtTOTAL_INSURANCE_FROM;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTOTAL_INSURANCE_FROM;
		protected System.Web.UI.WebControls.RegularExpressionValidator revTOTAL_INSURANCE_FROM;

		protected System.Web.UI.WebControls.Label capTOTAL_INSURANCE_TO;
		protected System.Web.UI.WebControls.TextBox txtTOTAL_INSURANCE_TO;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTOTAL_INSURANCE_TO;
		protected System.Web.UI.WebControls.RegularExpressionValidator revTOTAL_INSURANCE_TO;
		
		protected System.Web.UI.WebControls.Label capOTHER_INST;
		protected System.Web.UI.WebControls.DropDownList cmbOTHER_INST;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvOTHER_INST;

		protected System.Web.UI.WebControls.Label capRATE_APPLIED;
		protected System.Web.UI.WebControls.TextBox txtRATE_APPLIED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvRATE_APPLIED;
		protected System.Web.UI.WebControls.RegularExpressionValidator revRATE_APPLIED;

		protected System.Web.UI.WebControls.Label capCONSTRUCTION;
		protected System.Web.UI.WebControls.DropDownList cmbCONSTRUCTION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCONSTRUCTION;

		protected System.Web.UI.WebControls.Label capPROTECTION;
		protected System.Web.UI.WebControls.ListBox cmbFROMPROTECTION;
		protected System.Web.UI.WebControls.Button btnSELECT_PROTECTION;
		protected System.Web.UI.WebControls.Button btnDESELECT_PROTECTION;
		protected System.Web.UI.WebControls.Label capRECIPIENT;
		protected System.Web.UI.WebControls.ListBox cmbPROTECTION;
//		protected System.Web.UI.WebControls.CustomValidator csvPROTECTION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPROTECTION;

		protected System.Web.UI.WebControls.Label capALARM_CREDIT;
		protected System.Web.UI.WebControls.DropDownList cmbALARM_CREDIT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvALARM_CREDIT;

		protected System.Web.UI.WebControls.Label capALARM_PERCENTAGE;
		protected System.Web.UI.WebControls.TextBox txtALARM_PERCENTAGE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvALARM_PERCENTAGE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revALARM_PERCENTAGE;

		protected System.Web.UI.WebControls.Label capHOME_CREDIT;
		protected System.Web.UI.WebControls.DropDownList cmbHOME_CREDIT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHOME_CREDIT;

		protected System.Web.UI.WebControls.Label capHOME_AGE;
		protected System.Web.UI.WebControls.TextBox txtHOME_AGE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHOME_AGE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revHOME_AGE;

		protected System.Web.UI.WebControls.Label capHOME_PERCENTAGE;
		protected System.Web.UI.WebControls.TextBox txtHOME_PERCENTAGE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHOME_PERCENTAGE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revHOME_PERCENTAGE;

		protected System.Web.UI.WebControls.Label capCOVERAGE_CATEGORY;
		protected System.Web.UI.WebControls.ListBox cmbFROMCATEGORY;
		protected System.Web.UI.WebControls.Button btnSELECT;
		protected System.Web.UI.WebControls.Button btnDESELECT;
		protected System.Web.UI.WebControls.Label capRECIPIENTS;
		protected System.Web.UI.WebControls.ListBox cmbCOVERAGE_CATEGORY;
		protected System.Web.UI.WebControls.CustomValidator csvCOVERAGE_CATEGORY;

		protected System.Web.UI.WebControls.Label capCALCULATION_BASE;
		protected System.Web.UI.WebControls.DropDownList cmbCALCULATION_BASE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCALCULATION_BASE;

		protected System.Web.UI.WebControls.Label capINSURANCE_VALUE;
		protected System.Web.UI.WebControls.TextBox txtINSURANCE_VALUE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINSURANCE_VALUE;
		protected System.Web.UI.WebControls.RegularExpressionValidator	revINSURANCE_VALUE;
		protected System.Web.UI.WebControls.CheckBox chkSelectAll; 


		protected System.Web.UI.WebControls.Label capCOMMENTS;
		protected System.Web.UI.WebControls.TextBox txtCOMMENTS;
		private string strRowId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCONTRACT_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCATAGORY;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPROTECTION;

		protected System.Web.UI.WebControls.CustomValidator csvCHECK_DATE;
		protected System.Web.UI.WebControls.CustomValidator csvALARM_PERCENTAGE;
		protected System.Web.UI.WebControls.CustomValidator csvHOME_PERCENTAGE;
		protected System.Web.UI.WebControls.CustomValidator csvRATE_APPLIED;
        protected System.Web.UI.WebControls.Label capMessages;

		#endregion

		#region Local form variables
		System.Resources.ResourceManager objResourceMgr;	//creating resource manager object

		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			base.ScreenId = "262_8";
			#region Setting the properties of CmsButton 
			//START:** Setting permissions and class (Read/write/execute/delete) of Cmsbutton**********
			btnReset.CmsButtonClass		= CmsButtonType.Write;
			btnReset.PermissionString	= gstrSecurityXML;

			btnSave.CmsButtonClass		= CmsButtonType.Write;
			btnSave.PermissionString	= gstrSecurityXML;

			btnDelete.CmsButtonClass	= CmsButtonType.Delete;
			btnDelete.PermissionString	= gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass	= CmsButtonType.Write;
			btnActivateDeactivate.PermissionString	= gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			#endregion
			
			#region Set Attributes
			btnSave.Attributes.Add("onclick","javascript:return setCatagories();setProtections();");   
			btnSELECT.Attributes.Add("onclick","javascript:selectCatagories();return false;");
			btnDESELECT.Attributes.Add("onclick","javascript:deselectCatagories();return false;");
			btnSELECT_PROTECTION.Attributes.Add("onclick","javascript:selectProtection();return false;");
			btnDESELECT_PROTECTION.Attributes.Add("onclick","javascript:deselectProtection();return false;");
			hlkEFFECTIVE_DATE.Attributes.Add("OnClick","fPopCalendar(document.REIN_PREMIUM_BUILDER.txtEFFECTIVE_DATE,document.REIN_PREMIUM_BUILDER.txtEFFECTIVE_DATE)");
			hlkEXPIRY_DATE.Attributes.Add("OnClick","fPopCalendar(document.REIN_PREMIUM_BUILDER.txtEXPIRY_DATE,document.REIN_PREMIUM_BUILDER.txtEXPIRY_DATE)");
//			btnReset.Attributes.Add("onclick","javascript:document.forms[0].reset();return false;");
			btnReset.Attributes.Add("onclick","javascript:Reset();");
			//txtLAYER.Attributes.Add("onBlur","this.value=validatecurrency(this);");
			txtINSURANCE_VALUE.Attributes.Add("onBlur","this.value=formatDeductCurrency(this.value);");
			txtTOTAL_INSURANCE_FROM.Attributes.Add("onBlur","this.value=validatecurrency(this);");
			txtTOTAL_INSURANCE_TO.Attributes.Add("onBlur","this.value=validatecurrency(this);");
			txtRATE_APPLIED.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");
			//txtALARM_PERCENTAGE.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");
			txtHOME_AGE.Attributes.Add("onBlur","this.value=validatecurrency(this);");

			txtRATE_APPLIED.Attributes.Add("onblur","javascript:FormatAmount(this)");
//			txtALARM_PERCENTAGE.Attributes.Add("onBlur","javascript:FormatAmount(this)");
//			txtHOME_PERCENTAGE.Attributes.Add("onBlur","javascript:FormatAmount(this)");

			
			#endregion
			//Making resource manager object for reading the resource file
			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.Reinsurance.AddReinsurancePremiumBuilder" ,System.Reflection.Assembly.GetExecutingAssembly());

			if(!Page.IsPostBack)
			{
				GetQueryStringValues();
				GetOldDataXML();
				SetCaptions();
				fillDropdowns();
				SetErrorMessages();
                capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
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
			this.Load += new System.EventHandler(this.Page_Load);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);


		}
		#endregion

		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsReinsurancePremiumBuildInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsReinsurancePremiumBuildInfo objReinsurancePremiumBuildInfo = new ClsReinsurancePremiumBuildInfo();

			objReinsurancePremiumBuildInfo.CREATED_BY = objReinsurancePremiumBuildInfo.MODIFIED_BY = int.Parse(GetUserId());

			objReinsurancePremiumBuildInfo.CONTRACT_ID = int.Parse(hidCONTRACT_ID.Value);			
			//Populating the fields values of model class object from controls
			if(txtCONTRACT.Text.Trim()!= "")
			{
				objReinsurancePremiumBuildInfo.CONTRACT = txtCONTRACT.Text;
			}

			if (txtEFFECTIVE_DATE.Text.Trim() != "")
				objReinsurancePremiumBuildInfo.EFFECTIVE_DATE = Convert.ToDateTime(txtEFFECTIVE_DATE.Text.Trim());
			if (txtEXPIRY_DATE.Text.Trim() != "")
				objReinsurancePremiumBuildInfo.EXPIRY_DATE = Convert.ToDateTime(txtEXPIRY_DATE.Text.Trim());

			if(txtLAYER.Text.Trim() != "")
				objReinsurancePremiumBuildInfo.LAYER = int.Parse(txtLAYER.Text.Trim());
			

			if (cmbCALCULATION_BASE.SelectedValue != null && cmbCALCULATION_BASE.SelectedValue.Trim() != "")
				objReinsurancePremiumBuildInfo.CALCULATION_BASE = int.Parse(cmbCALCULATION_BASE.SelectedValue);

			if(txtINSURANCE_VALUE.Text.Trim() != "")
				objReinsurancePremiumBuildInfo.INSURANCE_VALUE = txtINSURANCE_VALUE.Text;

			if(txtTOTAL_INSURANCE_FROM.Text.Trim() != "")
				objReinsurancePremiumBuildInfo.TOTAL_INSURANCE_FROM = txtTOTAL_INSURANCE_FROM.Text;

			if(txtTOTAL_INSURANCE_TO.Text.Trim() != "")
				objReinsurancePremiumBuildInfo.TOTAL_INSURANCE_TO = txtTOTAL_INSURANCE_TO.Text;

			if (cmbOTHER_INST.SelectedValue != null && cmbOTHER_INST.SelectedValue.Trim() != "")
				objReinsurancePremiumBuildInfo.OTHER_INST = int.Parse(cmbOTHER_INST.SelectedValue);

			if(txtRATE_APPLIED.Text.Trim() != "")
				objReinsurancePremiumBuildInfo.RATE_APPLIED = double.Parse(txtRATE_APPLIED.Text.Trim());
			
			if (cmbCONSTRUCTION.SelectedValue != null && cmbCONSTRUCTION.SelectedValue.Trim() != "")
				objReinsurancePremiumBuildInfo.CONSTRUCTION = int.Parse(cmbCONSTRUCTION.SelectedValue);

			if (cmbPROTECTION.SelectedValue != null && cmbPROTECTION.SelectedValue.Trim() != "")
				objReinsurancePremiumBuildInfo.PROTECTION = cmbPROTECTION.SelectedValue;

			if (cmbALARM_CREDIT.SelectedValue != null && cmbALARM_CREDIT.SelectedValue.Trim() != "")
				objReinsurancePremiumBuildInfo.ALARM_CREDIT = int.Parse(cmbALARM_CREDIT.SelectedValue);

			if(txtALARM_PERCENTAGE.Text.Trim() != "")
				objReinsurancePremiumBuildInfo.ALARM_PERCENTAGE= double.Parse(txtALARM_PERCENTAGE.Text.Trim());

			if (cmbHOME_CREDIT.SelectedValue != null && cmbHOME_CREDIT.SelectedValue.Trim() != "")
				objReinsurancePremiumBuildInfo.HOME_CREDIT = int.Parse(cmbHOME_CREDIT.SelectedValue);
			
			if(txtHOME_AGE.Text.Trim() != "")
				objReinsurancePremiumBuildInfo.HOME_AGE= int.Parse(txtHOME_AGE.Text.Trim());
			
			if(txtHOME_PERCENTAGE.Text.Trim() != "")
				objReinsurancePremiumBuildInfo.HOME_PERCENTAGE= double.Parse(txtHOME_PERCENTAGE.Text.Trim());

			if(txtCOMMENTS.Text.Trim() != "")
				objReinsurancePremiumBuildInfo.COMMENTS= txtCOMMENTS.Text;

			string catagory=(string)hidCATAGORY.Value;
			if (catagory !="" && catagory != "0")
			{
				string[] catagories= catagory.Split(',');  
				catagory="";
				for (int i=0;i <catagories.GetLength(0)-1 ;i++)
				{
					catagory=catagory + catagories[i].ToString()  + ","; 	
				}
			}
			if (catagory =="0" ) 
				catagory="";			
			objReinsurancePremiumBuildInfo.COVERAGE_CATEGORY = catagory;

			///
			string protection=(string)hidPROTECTION.Value;
			if (protection !="" && protection != "0")
			{
				string[] protections= protection.Split(',');  
				protection="";
				for (int i=0;i <protections.GetLength(0)-1 ;i++)
				{
					protection=protection + protections[i].ToString()  + ","; 	
				}
			}
			if (protection =="0" ) 
				protection="";			
			objReinsurancePremiumBuildInfo.PROTECTION = protection;

			//These  assignments are common to all pages.
			strRowId		=	hidPREMIUM_BUILDER_ID.Value;
			if(!strRowId.ToUpper().Equals("NEW"))
				objReinsurancePremiumBuildInfo.PREMIUM_BUILDER_ID= int.Parse(strRowId);
			//Returning the model object
			return objReinsurancePremiumBuildInfo;
		}
		#endregion

		# region fillDropdowns
		private void fillDropdowns()
		{
			//if(hidPREMIUM_BUILDER_ID.Value == "0")
			//{
				hidCONTRACT_ID.Value = Request.QueryString["ContractID"].ToString();
				hidOldTempData.Value = ClsReinsuranceInformation.GetDataSetForPageControls(hidCONTRACT_ID.Value).GetXml();
				System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
				doc.LoadXml(hidOldTempData.Value);
				System.Xml.XmlNode contractnum	= doc.SelectSingleNode("/NewDataSet/Table/CONTRACT_NUMBER");
				if (contractnum != null)
				{
					if (contractnum.InnerXml.Trim() != "")
					{
						txtCONTRACT .Text = contractnum.InnerXml.Trim();
					}
				}
				System.Xml.XmlNode effdate	= doc.SelectSingleNode("/NewDataSet/Table/EFFECTIVE_DATE");
				if (effdate != null)
				{
					if (effdate.InnerXml.Trim() != "")
					{
						txtEFFECTIVE_DATE .Text = effdate.InnerXml.Trim();
					}
				}
				System.Xml.XmlNode expdate	= doc.SelectSingleNode("/NewDataSet/Table/EXPIRATION_DATE");
				if (expdate != null)
				{
					if (expdate.InnerXml.Trim() != "")
					{
						txtEXPIRY_DATE .Text = expdate.InnerXml.Trim();
					}
				}


	
				hidOldTempData.Value = "";
			//}

			cmbALARM_CREDIT.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("yesno");
			cmbALARM_CREDIT.DataTextField	= "LookupDesc";
			cmbALARM_CREDIT.DataValueField	= "LookupID";
			cmbALARM_CREDIT.DataBind(); 
			cmbALARM_CREDIT.Items.Insert(0,"");

			cmbHOME_CREDIT.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("yesno");
			cmbHOME_CREDIT.DataTextField	= "LookupDesc";
			cmbHOME_CREDIT.DataValueField	= "LookupID";
			cmbHOME_CREDIT.DataBind(); 
			cmbHOME_CREDIT.Items.Insert(0,"");

			cmbFROMCATEGORY.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("RRB");
			cmbFROMCATEGORY.DataTextField	= "LookupDesc";
			cmbFROMCATEGORY.DataValueField	= "LookupID";
			cmbFROMCATEGORY.DataBind(); 

			cmbFROMPROTECTION.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("PRCLS","-1","Y");
			cmbFROMPROTECTION.DataTextField	= "LookupDesc";
			cmbFROMPROTECTION.DataValueField	= "LookupID";
			cmbFROMPROTECTION.DataBind(); 

			ListItem iListItem = null;
			iListItem = cmbFROMPROTECTION.Items.FindByValue("11862"); //11862 --> 8B
			if(iListItem!=null)
				cmbFROMPROTECTION.Items.Remove(iListItem);

			cmbCALCULATION_BASE.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("RCAL");
			cmbCALCULATION_BASE.DataTextField	= "LookupDesc";
			cmbCALCULATION_BASE.DataValueField	= "LookupID";
			cmbCALCULATION_BASE.DataBind();
			cmbCALCULATION_BASE.Items.Insert(0,"");

			cmbCONSTRUCTION.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("RCONSC");
			cmbCONSTRUCTION.DataTextField	= "LookupDesc";
			cmbCONSTRUCTION.DataValueField	= "LookupID";
			cmbCONSTRUCTION.DataBind();
			cmbCONSTRUCTION.Items.Insert(0,"");

			
			cmbOTHER_INST.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("OTINST","-1","Y");
			cmbOTHER_INST.DataTextField	= "LookupDesc";
			cmbOTHER_INST.DataValueField	= "LookupID";
			cmbOTHER_INST.DataBind();
			cmbOTHER_INST.Items.Insert(0,"");
			/*cmbOTHER_INST.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("RCONSC");
			cmbOTHER_INST.DataTextField	= "LookupDesc";
			cmbOTHER_INST.DataValueField	= "LookupID";
			cmbOTHER_INST.DataBind();*/

		}
		#endregion

		# region button actions
		private void btnReset_Click(object sender, System.EventArgs e)
		{
		
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				/*Deleting the whole record*/
				ClsReinsurancePremiumBuildInfo objReinsurancePremiumBuildInfo;
				objReinsurancePremiumBuildInfo = new ClsReinsurancePremiumBuildInfo();

				//Retreiving the form values into model class object
				objReinsurancePremiumBuildInfo = GetFormValue();
				Cms.BusinessLayer.BlCommon.ClsReinsuranceInformation objReinsuranceInformation = new Cms.BusinessLayer.BlCommon.ClsReinsuranceInformation();

				int intRetVal = objReinsuranceInformation.DeleteReinPremiumBuilder(objReinsurancePremiumBuildInfo,int.Parse(hidPREMIUM_BUILDER_ID.Value),int.Parse(GetUserId()));

				if (intRetVal > 0)
				{
					//Deleted successfully
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "127");
					hidPREMIUM_BUILDER_ID.Value = "";
					hidFormSaved.Value = "5";
					hidOldData.Value = "";
					tblBody.Attributes.Add("style","display:none");
				}
				else if(intRetVal == -2)
				{
					lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"6");
					hidFormSaved.Value		=	"2";
				}
				else
				{
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "128");
					hidFormSaved.Value = "2";
				}
				lblMessage.Visible = true;
			}
			catch (Exception objEx)
			{
				lblMessage.Text = objEx.Message.ToString();
				lblMessage.Visible = true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objEx);
			}
		
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	//For retreiving the return value of business class save function
				ClsReinsurancePremiumBuildInfo objReinsurancePremiumBuildInfo;
				objReinsurancePremiumBuildInfo = new ClsReinsurancePremiumBuildInfo();

				//Retreiving the form values into model class object
				objReinsurancePremiumBuildInfo = GetFormValue();
				Cms.BusinessLayer.BlCommon.ClsReinsuranceInformation objReinsuranceInformation = new Cms.BusinessLayer.BlCommon.ClsReinsuranceInformation();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{

					//Filling the CREATED_BY to current login user and CREATED_DATE to current date
					objReinsurancePremiumBuildInfo.CREATED_BY = objReinsurancePremiumBuildInfo.MODIFIED_BY = int.Parse(GetUserId());
					objReinsurancePremiumBuildInfo.CREATED_DATETIME = DateTime.Now;

					//Calling the add method of business layer class
					intRetVal = objReinsuranceInformation.AddReinPremiumBuilder(objReinsurancePremiumBuildInfo);

					if(intRetVal>0)				//Saved successfully
					{
						hidPREMIUM_BUILDER_ID.Value = objReinsurancePremiumBuildInfo.PREMIUM_BUILDER_ID.ToString();
						lblMessage.Text		=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value	=	"1";
						hidIS_ACTIVE.Value	=	"Y";
						btnActivateDeactivate.Text="Deactivate";
						GetOldDataXML();
					}
					else if(intRetVal == -1)	//Duplicate journal entry number error occured
					{
						//Showing the error message from customized message file
						lblMessage.Text		=	ClsMessages.GetMessage(base.ScreenId,"18");
						hidFormSaved.Value	=	"2";
					}
					else						//Some other error occured
					{
						//Showing the error message from customized message file
						lblMessage.Text		=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value	=	"2";
					}
					lblMessage.Visible = true;
				}	// end save case
				else //UPDATE CASE
				{

					//Creating the Model object for holding the Old data
					ClsReinsurancePremiumBuildInfo objOldReinsurancePremiumBuildInfo;
					objOldReinsurancePremiumBuildInfo = new ClsReinsurancePremiumBuildInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldReinsurancePremiumBuildInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objReinsurancePremiumBuildInfo.MODIFIED_BY = int.Parse(GetUserId());
					objReinsurancePremiumBuildInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					objReinsurancePremiumBuildInfo.IS_ACTIVE = hidIS_ACTIVE.Value;

					//Updating the record using business layer class object
					intRetVal	= objReinsuranceInformation.UpdateReinPremiumBuilder(objOldReinsurancePremiumBuildInfo,objReinsurancePremiumBuildInfo);
					if( intRetVal > 0 )				//update successfully performed
					{
						
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						GetOldDataXML();
					}
					
					lblMessage.Visible = true;
				}
			}
			catch(Exception ex)
			{
				//Some exception occured in code, hence showing the exception error message
				lblMessage.Text		=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				hidFormSaved.Value	=	"2";
				
				//Publishing the exception
				ExceptionManager.Publish(ex);
			}
			finally
			{
				//			if(objCoverageCategoriesInfo!= null)
				//				objCoverageCategoriesInfo.Dispose();
			}
		}

		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
		
			ClsReinsurancePremiumBuildInfo objReinsurancePremiumBuildInfo;
			objReinsurancePremiumBuildInfo = new ClsReinsurancePremiumBuildInfo();

			//Retreiving the form values into model class object
			objReinsurancePremiumBuildInfo = GetFormValue();
			Cms.BusinessLayer.BlCommon.ClsReinsuranceInformation objReinsuranceInformation = new Cms.BusinessLayer.BlCommon.ClsReinsuranceInformation();
			
			try
			{
				//Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo();
				
				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					objReinsuranceInformation.ActivateDeactivateReinPremiumBuilder(objReinsurancePremiumBuildInfo,"N");
					lblMessage.Text = ClsMessages.FetchGeneralMessage("41");
					btnActivateDeactivate.Text="Activate";
					hidIS_ACTIVE.Value="N";
				}
				else
				{					
					objReinsuranceInformation.ActivateDeactivateReinPremiumBuilder(objReinsurancePremiumBuildInfo,"Y");
					lblMessage.Text = ClsMessages.FetchGeneralMessage("40");
					btnActivateDeactivate.Text="Deactivate";
					hidIS_ACTIVE.Value="Y";
				}
//				hidOldData.Value = ClsCoverageCategories.GetCoverageCatagoryInfo(int.Parse(hidPREMIUM_BUILDER_ID.Value));
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
				if(objReinsuranceInformation!= null)
					objReinsuranceInformation.Dispose();
			}
				
		}

		#endregion
		
		# region GetOldDataXML function
		private void GetOldDataXML()
		{
			if ( hidPREMIUM_BUILDER_ID.Value != "" ) 
			{
				//Retreiving the information of selected journal entry in the form of XML 				
				hidOldData.Value = ClsReinsuranceInformation.GetReinPremiumBuilder(int.Parse(hidPREMIUM_BUILDER_ID.Value));

				if (hidOldData.Value != "")
				{
					System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
					doc.LoadXml(hidOldData.Value);
				
					System.Xml.XmlNode contract	= doc.SelectSingleNode("/NewDataSet/Table/CONTRACT");
					System.Xml.XmlNode effDate  = doc.SelectSingleNode("/NewDataSet/Table/EFFECTIVE_DATE");
					System.Xml.XmlNode expDate  = doc.SelectSingleNode("/NewDataSet/Table/EXPIRY_DATE");
					System.Xml.XmlNode layer    = doc.SelectSingleNode("/NewDataSet/Table/LAYER");
					System.Xml.XmlNode covCat	= doc.SelectSingleNode("/NewDataSet/Table/COVERAGE_CATEGORY");
					System.Xml.XmlNode calcBase = doc.SelectSingleNode("/NewDataSet/Table/CALCULATION_BASE");
					System.Xml.XmlNode insValue = doc.SelectSingleNode("/NewDataSet/Table/INSURANCE_VALUE");
					System.Xml.XmlNode insFrom	= doc.SelectSingleNode("/NewDataSet/Table/TOTAL_INSURANCE_FROM");
					System.Xml.XmlNode insTo	= doc.SelectSingleNode("/NewDataSet/Table/TOTAL_INSURANCE_TO");
					System.Xml.XmlNode otherIns	= doc.SelectSingleNode("/NewDataSet/Table/OTHER_INST");
					System.Xml.XmlNode rateAppl	= doc.SelectSingleNode("/NewDataSet/Table/RATE_APPLIED");
					System.Xml.XmlNode constr 	= doc.SelectSingleNode("/NewDataSet/Table/CONSTRUCTION");
					System.Xml.XmlNode protec	= doc.SelectSingleNode("/NewDataSet/Table/PROTECTION");
					System.Xml.XmlNode alarCred = doc.SelectSingleNode("/NewDataSet/Table/ALARM_CREDIT");
					System.Xml.XmlNode alarPer 	= doc.SelectSingleNode("/NewDataSet/Table/ALARM_PERCENTAGE");
					System.Xml.XmlNode homeCred	= doc.SelectSingleNode("/NewDataSet/Table/HOME_CREDIT");
					System.Xml.XmlNode homeAge	= doc.SelectSingleNode("/NewDataSet/Table/HOME_AGE");
					System.Xml.XmlNode homePer 	= doc.SelectSingleNode("/NewDataSet/Table/HOME_PERCENTAGE");
					System.Xml.XmlNode comment	= doc.SelectSingleNode("/NewDataSet/Table/COMMENTS");
					/*if (contract != null)
					{
						if (contract.InnerXml.Trim() != "")
						{
							txtCONTRACT .Text = contract.InnerXml.Trim();
						}
					}
					
					if (effDate != null)
					{
						if (effDate.InnerXml.Trim() != "")
						{
							txtEFFECTIVE_DATE.Text = effDate.InnerXml.Trim().ToUpper();
						}
					}
					if (expDate != null)
					{
						if (expDate.InnerXml.Trim() != "")
						{
							txtEXPIRY_DATE.Text = expDate.InnerXml.Trim().ToUpper();
						}
					}*/

					if (layer != null)
					{
						if (layer.InnerXml.Trim() != "")
						{
							txtLAYER.Text = layer.InnerXml.Trim();
						}
					}

					if (calcBase != null)
					{
						if (calcBase.InnerXml.Trim() != "")
						{
							cmbCALCULATION_BASE.SelectedValue = calcBase.InnerXml.Trim().ToUpper();
						}
					}

					if (insValue != null)
					{
						if (insValue.InnerXml.Trim() != "")
						{
							txtINSURANCE_VALUE.Text = insValue.InnerXml.Trim();
						}
					}

					if (insFrom != null)
					{
						if (insFrom.InnerXml.Trim() != "")
						{
							txtTOTAL_INSURANCE_FROM.Text = insFrom.InnerXml.Trim();
						}
					}

					if (insTo != null)
					{
						if (insTo.InnerXml.Trim() != "")
						{
							txtTOTAL_INSURANCE_TO.Text = insTo.InnerXml.Trim();
						}
					}
					if (otherIns != null)
					{
						if (otherIns.InnerXml.Trim() != "")
						{
							cmbOTHER_INST.SelectedValue = otherIns.InnerXml.Trim().ToUpper();
						}
					}

					if (rateAppl != null)
					{
						if (rateAppl.InnerXml.Trim() != "")
						{
							txtRATE_APPLIED.Text = rateAppl.InnerXml.Trim();
						}
					}
					if (constr != null)
					{
						if (constr.InnerXml.Trim() != "")
						{
							cmbCONSTRUCTION.SelectedValue = constr.InnerXml.Trim().ToUpper();
						}
					}
					if (alarCred != null)
					{
						if (alarCred.InnerXml.Trim() != "")
						{
							cmbALARM_CREDIT.SelectedValue = alarCred.InnerXml.Trim().ToUpper();
						}
					}
					if (alarPer != null)
					{
						if (alarPer.InnerXml.Trim() != "")
						{
							txtALARM_PERCENTAGE.Text = alarPer.InnerXml.Trim();
						}
					}
					if (homeCred != null)
					{
						if (homeCred.InnerXml.Trim() != "")
						{
							cmbHOME_CREDIT.SelectedValue = homeCred.InnerXml.Trim().ToUpper();
						}
					}
					if (homeAge != null)
					{
						if (homeAge.InnerXml.Trim() != "")
						{
							txtHOME_AGE.Text = homeAge.InnerXml.Trim();
						}
					}
					if (homePer != null)
					{
						if (homePer.InnerXml.Trim() != "")
						{
							txtHOME_PERCENTAGE.Text = homePer.InnerXml.Trim();
						}
					}
					if (comment != null)
					{
						if (comment.InnerXml.Trim() != "")
						{
							txtCOMMENTS.Text = comment.InnerXml.Trim();
						}
					}

					if (covCat != null)
					{
						if (covCat.InnerXml.Trim() != "")
						{
							hidCATAGORY.Value = covCat.InnerXml.Trim();
						}
					}

					if (protec != null)
					{
						if (protec.InnerXml.Trim() != "")
						{
							hidPROTECTION.Value = protec.InnerXml.Trim();
						}
					}
					System.Xml.XmlNode IsActive = doc.SelectSingleNode("/NewDataSet/Table/IS_ACTIVE");
					if (IsActive != null)
					{
						if (IsActive.InnerXml.Trim() != "")
						{
							hidIS_ACTIVE.Value= IsActive.InnerXml.Trim().ToUpper();
							if (IsActive.InnerXml.Trim()=="Y")
								btnActivateDeactivate.Text= "Deactivate";
							else if(IsActive.InnerXml.Trim()=="N")
								btnActivateDeactivate.Text= "Activate";
						}
					}
					doc = null;
				}

			}
		}
					
		#endregion
		# region SetCaptions

		private void SetCaptions()
		{
			capCONTRACT.Text				= objResourceMgr.GetString("txtCONTRACT");
			capEFFECTIVE_DATE.Text			= objResourceMgr.GetString("txtEFFECTIVE_DATE");
			capEXPIRY_DATE.Text				= objResourceMgr.GetString("txtEXPIRY_DATE");
			capLAYER.Text					= objResourceMgr.GetString("txtLAYER");
			capCOVERAGE_CATEGORY.Text		= objResourceMgr.GetString("cmbCOVERAGE_CATEGORY_TLOG");
			capCALCULATION_BASE.Text		= objResourceMgr.GetString("cmbCALCULATION_BASE");
			capINSURANCE_VALUE.Text			= objResourceMgr.GetString("txtINSURANCE_VALUE");
			capTOTAL_INSURANCE_FROM.Text	= objResourceMgr.GetString("txtTOTAL_INSURANCE_FROM");
			capTOTAL_INSURANCE_TO.Text		= objResourceMgr.GetString("txtTOTAL_INSURANCE_TO");
			capOTHER_INST.Text				= objResourceMgr.GetString("cmbOTHER_INST");
			capRATE_APPLIED.Text			= objResourceMgr.GetString("txtRATE_APPLIED");
			capCONSTRUCTION.Text			= objResourceMgr.GetString("cmbCONSTRUCTION");
			capPROTECTION.Text				= objResourceMgr.GetString("cmbPROTECTION_TLOG");
			capALARM_CREDIT.Text			= objResourceMgr.GetString("cmbALARM_CREDIT");
			capALARM_PERCENTAGE.Text		= objResourceMgr.GetString("txtALARM_PERCENTAGE");
			capHOME_CREDIT.Text				= objResourceMgr.GetString("cmbHOME_CREDIT");
			capHOME_AGE.Text				= objResourceMgr.GetString("txtHOME_AGE");
			capHOME_PERCENTAGE.Text			= objResourceMgr.GetString("txtHOME_PERCENTAGE");
			capCOMMENTS.Text				= objResourceMgr.GetString("txtCOMMENTS");
		}
		# endregion

		#region GetQueryStringValues
		private void GetQueryStringValues()
		{
			if(Request.QueryString["ContractID"]!=null && Request.QueryString["ContractID"].ToString()!="")
				hidCONTRACT_ID.Value = Request.QueryString["ContractID"].ToString();
			if(Request.QueryString["PREMIUM_BUILDER_ID"]!=null && Request.QueryString["PREMIUM_BUILDER_ID"].ToString()!="")
				hidPREMIUM_BUILDER_ID.Value = Request.QueryString["PREMIUM_BUILDER_ID"].ToString();

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
			rfvEFFECTIVE_DATE.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("95");
			revEFFECTIVE_DATE.ValidationExpression =aRegExpDate;
			revEFFECTIVE_DATE.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("179");
			rfvEXPIRY_DATE.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("96");
			revEXPIRY_DATE.ValidationExpression =aRegExpDate;
			revEXPIRY_DATE.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("179");

			revLAYER.ValidationExpression =aRegExpInteger;
			revLAYER.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");

			revINSURANCE_VALUE.ValidationExpression =aRegExpNegativeCurrency;
			revINSURANCE_VALUE.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");

			revTOTAL_INSURANCE_FROM.ValidationExpression =aRegExpDoublePositiveWithZero;
			revTOTAL_INSURANCE_FROM.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("217");

			revTOTAL_INSURANCE_TO.ValidationExpression =aRegExpDoublePositiveWithZero;
			revTOTAL_INSURANCE_TO.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("217");

			revRATE_APPLIED.ValidationExpression =aRegExpDoublePositiveWithZeroFourDeci;
			revRATE_APPLIED.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");

			revALARM_PERCENTAGE.ValidationExpression =aRegExpDoublePositiveNonZero;
			revALARM_PERCENTAGE.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");

			revHOME_AGE.ValidationExpression =aRegExpDoublePositiveNonZero;
			revHOME_AGE.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
			
			revHOME_PERCENTAGE.ValidationExpression =aRegExpDoublePositiveNonZero;
			revHOME_PERCENTAGE.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
			
			csvCHECK_DATE.ErrorMessage					=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("867");


//			csvALARM_PERCENTAGE.ErrorMessage			= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("990");


		}
		#endregion

	}
}
