/******************************************************************************************
<Author					: - Sumit Chhabra
<Start Date				: -	April 26,2006
<End Date				: -	
<Description			: - page to add dummy policies
<Review Date			: - 
<Reviewed By			: - 


<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 

Modification History ********************************************************************************/

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
using Cms.BusinessLayer.BLClaims;
using Cms.Claims;
using Cms.Model.Claims;
using Cms.CmsWeb.Controls; 
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;
namespace  Cms.Claims.Aspx.Policy
{
	/// <summary>
	/// Summary description for DummyPolicyPopUp.
	/// </summary>
	public class DummyPolicyPopUp : Cms.Claims.ClaimBase
	{
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.WebControls.Label capINSURED_NAME;
		protected System.Web.UI.WebControls.TextBox txtINSURED_NAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINSURED_NAME;
		protected System.Web.UI.WebControls.Label capPOLICY_NUMBER;
		protected System.Web.UI.WebControls.CustomValidator csvEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.CustomValidator csvEXPIRATION_DATE;
		protected System.Web.UI.WebControls.TextBox txtDUMMY_ADD1;
		protected System.Web.UI.WebControls.TextBox txtDUMMY_ADD2;
		protected System.Web.UI.WebControls.TextBox txtDUMMY_CITY;
		protected System.Web.UI.WebControls.DropDownList cmbDUMMY_STATE;
		protected System.Web.UI.WebControls.TextBox txtDUMMY_ZIP;
		protected System.Web.UI.WebControls.DropDownList cmbDUMMY_COUNTRY;
		
		
		protected System.Web.UI.WebControls.Label capEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.Label capEXPIRATION_DATE;
		protected System.Web.UI.WebControls.Label capDUMMY_ADD1;
		protected System.Web.UI.WebControls.Label capDUMMY_ADD2;
		protected System.Web.UI.WebControls.Label capDUMMY_CITY;
		protected System.Web.UI.WebControls.Label capDUMMY_ZIP;
		protected System.Web.UI.WebControls.Label capDUMMY_STATE;
		protected System.Web.UI.WebControls.Label capDUMMY_COUNTRY;
		protected System.Web.UI.WebControls.TextBox txtEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.TextBox txtEXPIRATION_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEXPIRATION_DATE;
		//protected System.Web.UI.WebControls.Label capNOTES;
		//protected System.Web.UI.WebControls.TextBox txtNOTES;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDUMMY_POLICY_ID;				
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_NUMBER;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden IS_ACTIVE;
		protected System.Web.UI.WebControls.Label capLOB_ID;
		protected System.Web.UI.WebControls.DropDownList cmbLOB_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOB_ID;		
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.HyperLink hlkEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkEXPIRATION_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDUMMY_ADD1;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDUMMY_CITY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDUMMY_COUNTRY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDUMMY_STATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEXPIRATION_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPOLICY_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtPOLICY_NUMBER;

		protected System.Web.UI.WebControls.RegularExpressionValidator revDUMMY_CITY;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDUMMY_ZIP;
		protected System.Web.UI.WebControls.Image imgZipLookup;
		protected System.Web.UI.WebControls.HyperLink hlkZipLookup;
		
		System.Resources.ResourceManager objResourceMgr;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		//protected System.Web.UI.WebControls.CustomValidator csvNOTES;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.WebControls.ClaimTop cltClaimTop;		
		protected string ClaimID, ActivityID = "";
		protected string strCustomerId,strPolicyID,strPolicyVersionID,strClaimID,strLOB_ID;
		
		private void Page_Load(object sender, System.EventArgs e)
		{

			try
			{
				objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.Policy.DummyPolicyPopUp" ,System.Reflection.Assembly.GetExecutingAssembly());
				base.ScreenId="304_1";

				btnReset.CmsButtonClass	=	CmsButtonType.Write;
				btnReset.PermissionString		=	gstrSecurityXML;

				btnSave.CmsButtonClass	=	CmsButtonType.Write;
				btnSave.PermissionString		=	gstrSecurityXML;

				GetQueryStringValues();					
				if(!IsPostBack)
				{					
					//SetClaimTop();

					btnReset.Attributes.Add("onClick","javascript: return ResetTheForm();");
					hlkEFFECTIVE_DATE.Attributes.Add("OnClick","fPopCalendar(document.CLM_DUMMY_POLICY.txtEFFECTIVE_DATE,document.CLM_DUMMY_POLICY.txtEFFECTIVE_DATE)"); //Javascript Implementation for Effective Date
					hlkEXPIRATION_DATE.Attributes.Add("OnClick","fPopCalendar(document.CLM_DUMMY_POLICY.txtEXPIRATION_DATE,document.CLM_DUMMY_POLICY.txtEXPIRATION_DATE)"); //Javascript Implementation for Effective Date
					
					SetCaptions();
					SetErrorMessages();
					LoadDropDowns();
					FillData();
					
				}
			
			}
			catch(Exception exc)
			{
				lblMessage.Text = exc.Message;
			}
			finally
			{}
		}

		private void FillData()
		{
			DataTable dtClaimData = ClsDummyPolicy.GetClaimDataForDummyPolicy(hidCLAIM_NUMBER.Value, int.Parse(hidCLAIM_ID.Value));
			if(dtClaimData!=null && dtClaimData.Rows.Count>0)
			{
				txtINSURED_NAME.Text = dtClaimData.Rows[0]["INSURED_NAME"].ToString();
				txtEFFECTIVE_DATE.Text = dtClaimData.Rows[0]["EFFECTIVE_DATE"].ToString().Trim();
				txtEXPIRATION_DATE.Text = dtClaimData.Rows[0]["EXPIRATION_DATE"].ToString().Trim();
				txtPOLICY_NUMBER.Text = dtClaimData.Rows[0]["POLICY_NUMBER"].ToString().Trim();
				txtDUMMY_ADD1.Text = dtClaimData.Rows[0]["DUMMY_ADD1"].ToString().Trim();
				txtDUMMY_ADD2.Text = dtClaimData.Rows[0]["DUMMY_ADD2"].ToString().Trim();
				txtDUMMY_CITY.Text = dtClaimData.Rows[0]["DUMMY_CITY"].ToString().Trim();
				txtDUMMY_ZIP.Text = dtClaimData.Rows[0]["DUMMY_ZIP"].ToString().Trim();
				cmbDUMMY_STATE.SelectedValue = dtClaimData.Rows[0]["DUMMY_STATE"].ToString();
				cmbDUMMY_COUNTRY.SelectedValue = dtClaimData.Rows[0]["DUMMY_COUNTRY"].ToString();
				cmbLOB_ID.SelectedValue = dtClaimData.Rows[0]["LOB_ID"].ToString();
			}
		}

		private void LoadDropDowns()
		{
			DataSet dsRecieve = new DataSet();
			Cms.BusinessLayer.BlCommon.ClsStates objState = new Cms.BusinessLayer.BlCommon.ClsStates();
			dsRecieve = objState.PoplateLob();				
			cmbLOB_ID.DataSource =	dsRecieve.Tables[0];
			cmbLOB_ID.DataValueField = dsRecieve.Tables[0].Columns[0].ToString();
			cmbLOB_ID.DataTextField = dsRecieve.Tables[0].Columns[1].ToString();
			cmbLOB_ID.DataBind();
			cmbLOB_ID.Items.Insert(0,"");
			cmbLOB_ID.SelectedIndex=0;
			DataTable dt = Cms.CmsWeb.ClsFetcher.Country;
			cmbDUMMY_COUNTRY.DataSource		= dt;
			cmbDUMMY_COUNTRY.DataTextField	= "Country_Name";
			cmbDUMMY_COUNTRY.DataValueField	= "Country_Id";
			cmbDUMMY_COUNTRY.DataBind();
			dt = Cms.CmsWeb.ClsFetcher.State;
			cmbDUMMY_STATE.DataSource		= dt;
			cmbDUMMY_STATE.DataTextField	= "State_Name";
			cmbDUMMY_STATE.DataValueField	= "State_Id";
			cmbDUMMY_STATE.DataBind();
		}


		private void SetCaptions()
		{
			capINSURED_NAME.Text				=		objResourceMgr.GetString("txtINSURED_NAME");
			capEFFECTIVE_DATE.Text				=		objResourceMgr.GetString("txtEFFECTIVE_DATE");
			capEXPIRATION_DATE.Text				=		objResourceMgr.GetString("txtEXPIRATION_DATE");
			capDUMMY_ADD1.Text				=		objResourceMgr.GetString("txtDUMMY_ADD1");
			//capPOLICY_NUMBER.Text				=		objResourceMgr.GetString("txtPOLICY_NUMBER");
			capDUMMY_ADD2.Text				=		objResourceMgr.GetString("txtDUMMY_ADD2");
			capDUMMY_CITY.Text				=		objResourceMgr.GetString("txtDUMMY_CITY");
			capDUMMY_ZIP.Text				=		objResourceMgr.GetString("txtDUMMY_ZIP");
			capDUMMY_STATE.Text				=		objResourceMgr.GetString("cmbDUMMY_STATE");
			capDUMMY_COUNTRY.Text				=		objResourceMgr.GetString("cmbDUMMY_COUNTRY");
			capLOB_ID.Text						=		objResourceMgr.GetString("cmbLOB_ID");
			//capNOTES.Text						=		objResourceMgr.GetString("txtNOTES");			
		}

		private void SetErrorMessages()
		{
			rfvINSURED_NAME.ErrorMessage		=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("740");
			rfvEFFECTIVE_DATE.ErrorMessage		=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("95");
			rfvEXPIRATION_DATE.ErrorMessage		=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("96");
			rfvLOB_ID.ErrorMessage				=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("97");
			//csvNOTES.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("741");
			csvEFFECTIVE_DATE.ErrorMessage			= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("828");
			//csvEXPIRATION_DATEE.ErrorMessage			= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("828");
			this.revDUMMY_ZIP.ValidationExpression		= aRegExpZip;
			this.revDUMMY_CITY.ValidationExpression		= aRegExpClientName;
			revEFFECTIVE_DATE.ValidationExpression		=	aRegExpDate;
			revEXPIRATION_DATE.ValidationExpression		=	aRegExpDate;
//			this.revDEPT_ZIP.ErrorMessage				= Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"24");
//			this.rfvDEPT_ADD1.ErrorMessage				= Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"909");
//			this.rfvDEPT_COUNTRY.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"33");			
//			this.rfvDEPT_STATE.ErrorMessage				= Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"35");
//			this.rfvDEPT_CITY.ErrorMessage				= Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"56");		
//			this.rfvDEPT_ZIP.ErrorMessage				= Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"37");
		}

		private void GetQueryStringValues()
		{
			if(Request.QueryString["CLAIM_NUMBER"]!=null && Request.QueryString["CLAIM_NUMBER"].ToString()!="")
				hidCLAIM_NUMBER.Value = Request.QueryString["CLAIM_NUMBER"].ToString();

			if(Request.QueryString["CLAIM_ID"]!=null && Request.QueryString["CLAIM_ID"].ToString()!="")
				hidCLAIM_ID.Value = Request.QueryString["CLAIM_ID"].ToString();
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

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				ClsDummyPolicy objDummyPolicy = new ClsDummyPolicy();
				ClsDummyPolicyInfo objDummyPolicyInfo = GetFormValue();
				string DummyAdd = GetFormAddress();
				objDummyPolicyInfo.CREATED_BY	=	int.Parse(GetUserId());
				objDummyPolicyInfo.CREATED_DATETIME	=	System.DateTime.Now;

				string tempUrl = "/cms/claims/aspx/ClaimsTab.aspx?&NEW_RECORD=1&DUMMY_POLICY=T&INSURED_NAME=" + objDummyPolicyInfo.INSURED_NAME.ToString() + "&EFFECTIVE_DATE=" + objDummyPolicyInfo.EFFECTIVE_DATE.ToString()  + "&EXPIRATION_DATE=" + txtEXPIRATION_DATE.Text + "&NOTES=" + objDummyPolicyInfo.NOTES.ToString() + "&LOB=" + objDummyPolicyInfo.LOB_ID.ToString() + "&ADDRESS=" + DummyAdd;
				//string tempUrl = "/cms/claims/aspx/ClaimsTab.aspx?&NEW_RECORD=1&DUMMY_POLICY=T&";

				//Server.Transfer(tempUrl,true);

				int retVal = objDummyPolicy.Add(objDummyPolicyInfo);

				if(retVal>0)
				{
					tempUrl += "&DUMMY_POLICY_ID=" + retVal.ToString();
					Response.Redirect(tempUrl);
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"29");					
					hidFormSaved.Value		=	"1";
					objDummyPolicyInfo.DUMMY_POLICY_ID = retVal;
					hidDUMMY_POLICY_ID.Value = retVal.ToString();					
					RegisterScript();
				}				
				else
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"20");
					hidFormSaved.Value			=	"0";
				}
				lblMessage.Visible = true;

				/*
				retVal = objDummyPolicy.Add(objDummyPolicyInfo);

				if(retVal>0)
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"29");					
					hidFormSaved.Value		=	"1";
					objDummyPolicyInfo.DUMMY_POLICY_ID = retVal;
					hidDUMMY_POLICY_ID.Value = retVal.ToString();					
					RegisterScript();
				}				
				else
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"20");
					hidFormSaved.Value			=	"0";
				}
				lblMessage.Visible = true;
				*/
			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;
				lblMessage.Visible = true;
			}
			finally{}
		}

		private void RegisterScript()
		{
			//if ( this.sbScript.ToString() == "" ) return;

            if (!ClientScript.IsStartupScriptRegistered("Test"))
			{
				string strCode = @"<script>StartUpScript();</script>" ; 
				
				ClientScript.RegisterStartupScript(this.GetType(),"Test", strCode);
				
			}
		}
         

		#region GetFormValue
		private ClsDummyPolicyInfo GetFormValue()
		{
			ClsDummyPolicyInfo objDummyPolicyInfo = new ClsDummyPolicyInfo();
			objDummyPolicyInfo.INSURED_NAME		=	txtINSURED_NAME.Text.Trim();		
			objDummyPolicyInfo.EFFECTIVE_DATE	=	Convert.ToDateTime(txtEFFECTIVE_DATE.Text.Trim());		
			//objDummyPolicyInfo.NOTES			=	txtNOTES.Text.Trim();	
			objDummyPolicyInfo.NOTES			=	"";	
			objDummyPolicyInfo.LOB_ID			=	int.Parse(cmbLOB_ID.SelectedItem.Value);

			objDummyPolicyInfo.CLAIM_ID = 0;
			objDummyPolicyInfo.EXPIRATION_DATE	=	Convert.ToDateTime(txtEXPIRATION_DATE.Text.Trim());		
			objDummyPolicyInfo.CREATED_BY = int.Parse(GetUserId());
			objDummyPolicyInfo.CREATED_DATETIME = DateTime.Now;

			objDummyPolicyInfo.POLICY_NUMBER = txtPOLICY_NUMBER.Text;
			objDummyPolicyInfo.DUMMY_ADD1 = txtDUMMY_ADD1.Text;
			objDummyPolicyInfo.DUMMY_ADD2 = txtDUMMY_ADD2.Text;
			objDummyPolicyInfo.DUMMY_CITY = txtDUMMY_CITY.Text;
			objDummyPolicyInfo.DUMMY_ZIP = txtDUMMY_ZIP.Text;
			objDummyPolicyInfo.DUMMY_STATE = cmbDUMMY_STATE.SelectedValue;
			objDummyPolicyInfo.DUMMY_COUNTRY = cmbDUMMY_COUNTRY.SelectedValue;

			return objDummyPolicyInfo;
		}

		private string GetFormAddress()
		{
			string address = "";
			address += txtPOLICY_NUMBER.Text + "~";
			address += txtDUMMY_ADD1.Text + "~";
			address += txtDUMMY_ADD2.Text + "~";
			address += txtDUMMY_CITY.Text + "~";
			address += txtDUMMY_ZIP.Text + "~";
			address += cmbDUMMY_STATE.SelectedValue + "~";
			address += cmbDUMMY_COUNTRY.SelectedValue;
			return address;
		}

		#endregion
		


		private void SetClaimTop()
		{
			
			strCustomerId = GetCustomerID();
			strPolicyID = GetPolicyID();
			strPolicyVersionID = GetPolicyVersionID();
			strClaimID = GetClaimID();
			strLOB_ID = GetLOBID();

			if(strCustomerId!=null && strCustomerId!="")
			{
				cltClaimTop.CustomerID = int.Parse(strCustomerId);
			}			

			if(strPolicyID!=null && strPolicyID!="")
			{
				cltClaimTop.PolicyID = int.Parse(strPolicyID);
			}

			if(strPolicyVersionID!=null && strPolicyVersionID!="")
			{
				cltClaimTop.PolicyVersionID = int.Parse(strPolicyVersionID);
			}
			if(strClaimID!=null && strClaimID!="")
			{
				cltClaimTop.ClaimID = int.Parse(strClaimID);
			}
			if(strLOB_ID!=null && strLOB_ID!="")
			{
				cltClaimTop.LobID = int.Parse(strLOB_ID);
			}
        
			cltClaimTop.ShowHeaderBand ="Claim";

			cltClaimTop.Visible = true;        
		}


	}
}
