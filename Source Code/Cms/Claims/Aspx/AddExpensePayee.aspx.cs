/******************************************************************************************
	<Author					: - > Sumit Chhabra
	<Start Date				: -	> 17-07-2006
	<End Date				: - >
	<Description			: - > This page is used for Expense Payee Details
	<Review Date			: - >
	<Reviewed By			: - >
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
using Cms.BusinessLayer.BLClaims;
using Cms.Claims;
using Cms.Model.Claims;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;
using Cms.CmsWeb.Controls; 
using Cms.Model.Maintenance.Claims;

namespace Cms.Claims.Aspx
{
	/// <summary>
	/// 
	/// </summary>
	public class AddExpensePayee : Cms.Claims.ClaimBase
	{
		#region Local form variables
		public int AnyPayeeAdded=0;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId;		
		//protected System.Web.UI.WebControls.Label capBANK_NAME;
		//protected System.Web.UI.WebControls.Label lblBANK_NAME;
		//protected System.Web.UI.WebControls.Label capAMOUNT;
		//protected System.Web.UI.WebControls.TextBox txtAMOUNT;
		//protected System.Web.UI.WebControls.RangeValidator rngAMOUNT;
		//protected System.Web.UI.WebControls.Label capACCOUNT_NUMBER;
		//protected System.Web.UI.WebControls.Label lblACCOUNT_NUMBER;
		//protected System.Web.UI.WebControls.Label capACCOUNT_NAME;
		//protected System.Web.UI.WebControls.Label lblACCOUNT_NAME;
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.Label lblMessage;
		//protected System.Web.UI.WebControls.Label capPAYEE;
		//protected System.Web.UI.WebControls.Label capREFERENCE;
		//protected System.Web.UI.WebControls.Label lblREFERENCE;
//		protected System.Web.UI.WebControls.Label capPAYMENT_METHOD;
//		protected System.Web.UI.WebControls.DropDownList cmbPAYMENT_METHOD;
//		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPAYMENT_METHOD;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPARTY_ID;		
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvAMOUNT;		
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOUNTRY;
		protected System.Web.UI.WebControls.Label capADDRESS1;
		protected System.Web.UI.WebControls.TextBox txtADDRESS1;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvADDRESS1;
		protected System.Web.UI.WebControls.Label capADDRESS2;
		protected System.Web.UI.WebControls.TextBox txtADDRESS2;
		protected System.Web.UI.WebControls.Label capCITY;
		protected System.Web.UI.WebControls.TextBox txtCITY;
		protected System.Web.UI.WebControls.Label capCOUNTRY;
		protected System.Web.UI.WebControls.DropDownList cmbCOUNTRY;
		protected System.Web.UI.WebControls.Label capSTATE;
		protected System.Web.UI.WebControls.DropDownList cmbSTATE;
		//protected System.Web.UI.WebControls.DropDownList cmbPAYEE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSTATE;
		protected System.Web.UI.WebControls.Label capZIP;
		protected System.Web.UI.WebControls.TextBox txtZIP;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvZIP;
		protected System.Web.UI.WebControls.RegularExpressionValidator revZIP;
		protected System.Web.UI.WebControls.Label capNARRATIVE;
		protected System.Web.UI.WebControls.TextBox txtNARRATIVE;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvNARRATIVE;
		protected System.Web.UI.WebControls.CustomValidator csvNARRATIVE;
		protected System.Web.UI.WebControls.Label capTO_ORDER_DESC;
		protected System.Web.UI.WebControls.TextBox txtTO_ORDER_DESC;		
		protected System.Web.UI.WebControls.CustomValidator csvTO_ORDER_DESC;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCALLED_FROM;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDefaultValues;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPAYEE_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidACTIVITY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidEXPENSE_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPARTY_ID;
		//protected System.Web.UI.WebControls.Label lblPAYEE;
		protected System.Web.UI.WebControls.Label capINVOICE_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtINVOICE_NUMBER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINVOICE_NUMBER;
		protected System.Web.UI.WebControls.Label capINVOICE_DATE;
		protected System.Web.UI.WebControls.TextBox txtINVOICE_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkINVOICE_DATE;
//		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINVOICE_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revINVOICE_DATE;
		protected System.Web.UI.WebControls.CustomValidator csvINVOICE_DATE;
		protected System.Web.UI.WebControls.Label capSERVICE_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbSERVICE_TYPE;
//		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSERVICE_TYPE;
		protected System.Web.UI.WebControls.Label capSERVICE_DESCRIPTION;
		protected System.Web.UI.WebControls.TextBox txtSERVICE_DESCRIPTION;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvSERVICE_DESCRIPTION;
		protected System.Web.UI.WebControls.CustomValidator csvSERVICE_DESCRIPTION;
		protected System.Web.UI.WebControls.Label capPARTY_ID;
		//protected System.Web.UI.WebControls.DropDownList cmbPARTY_ID;
		protected System.Web.UI.HtmlControls.HtmlSelect cmbPARTY_ID;
		protected System.Web.UI.WebControls.Label capAMOUNT;
		protected System.Web.UI.WebControls.Label lblCHECK_NUMBER;
		protected System.Web.UI.WebControls.Label lblCHECK_STATUS;
		protected System.Web.UI.WebControls.TextBox txtAMOUNT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAMOUNT;
		protected System.Web.UI.WebControls.RangeValidator rngAMOUNT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revAMOUNT;
		ClsPayee objPayee = new ClsPayee();
		string[] arryPartyID;		
		#endregion
	
		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{						
			
			base.ScreenId="309_1_0_1_0_1_1";
			
			lblMessage.Visible = false;
			
			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass		=	CmsButtonType.Write;
			btnReset.PermissionString	=	gstrSecurityXML;
			
			btnSave.CmsButtonClass		=	CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;


			btnSave.Attributes.Add("onclick","javascript:return CheckPartiesForSPH();Page_ClientValidate();");//Added Page_ClientValidate() by Sibin for Itrack Issue 5179 on 23 Dec 08		

			
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.AddExpensePayee" ,System.Reflection.Assembly.GetExecutingAssembly());

			this.cmbCOUNTRY.SelectedIndex = int.Parse(aCountry);
			
			//Added FOR Itrack Issue #5339
			if (Request.Form["__EVENTTARGET"] == "NAME")
			{
				cmbPARTY_ID_SelectedIndexChanged(sender,e);
				return;
			}
			
			string strClaimStatus = GetClaimStatus();
			if(strClaimStatus == CLAIM_STATUS_CLOSED)
				btnSave.Visible=false;
			else
				btnSave.Visible=true;


			if(!Page.IsPostBack)
			{			
				GetQueryStringValues();				
				if(hidPAYEE_ID.Value=="") //Check for whether any payee has already been added 
				{
					ClsPayee objPayee = new ClsPayee();
					AnyPayeeAdded = objPayee.AnyPayeeForClaim(hidCLAIM_ID.Value,hidACTIVITY_ID.Value);
					if(AnyPayeeAdded>0)
					{
						lblDelete.Visible = true;
						lblDelete.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("946");
						trBody.Attributes.Add("style","display:none");
						hidFormSaved.Value = "5";
						return;
					}
				
				}	
				hlkINVOICE_DATE.Attributes.Add("OnClick","fPopCalendar(document.CLM_PAYEE.txtINVOICE_DATE,document.CLM_PAYEE.txtINVOICE_DATE)"); //Javascript Implementation for Invoice Date
//				cmbPAYMENT_METHOD.Attributes.Add("onChange","ShowHideAddressDetails();");
				btnReset.Attributes.Add("onclick","javascript:return formReset();");
				txtAMOUNT.Attributes.Add("onBlur","javascript: this.value=formatCurrencyWithCents(this.value);");
				cmbPARTY_ID.Attributes.Add("onchange","javascript:return FillAddress();");   
				

				FillDropDowns();
				SetCaptions();
				SetErrorMessages();				 
				//BindDropDown(cmbPARTY_ID);
				//SetPayeeDetails();

				if(hidPAYEE_ID.Value!="")
				{
					LoadData();
					GetOldDataXML();
				}

			}
			
		}
		#endregion

		private void GetQueryStringValues()
		{
			if(Request["CLAIM_ID"]!=null && Request["CLAIM_ID"].ToString()!="")
				hidCLAIM_ID.Value = Request["CLAIM_ID"].ToString();
			else
				hidCLAIM_ID.Value = "";

			if (Request["PAYEE_ID"] != null && Request["PAYEE_ID"].ToString()!="")
				hidPAYEE_ID.Value = Request["PAYEE_ID"].ToString();
			else			
				hidPAYEE_ID.Value = "";		

			if (Request["ACTIVITY_ID"] != null && Request["ACTIVITY_ID"].ToString()!="")
				hidACTIVITY_ID.Value = Request["ACTIVITY_ID"].ToString();
			else			
				hidACTIVITY_ID.Value = "";		

			if (Request["EXPENSE_ID"] != null && Request["EXPENSE_ID"].ToString()!="")
				hidEXPENSE_ID.Value = Request["EXPENSE_ID"].ToString();
			else			
				hidEXPENSE_ID.Value = "";		

			if (Request["CALLED_FROM"] != null && Request["CALLED_FROM"].ToString()!="")
				hidCALLED_FROM.Value = Request["CALLED_FROM"].ToString();
			else			
				hidCALLED_FROM.Value = "";		

		}


	
		#region GetOldDataXML
		private void GetOldDataXML()
		{
			if (hidCLAIM_ID.Value != "" && hidPAYEE_ID.Value != "")
                hidOldData.Value = objPayee.GetXmlForPageControls(hidCLAIM_ID.Value, hidACTIVITY_ID.Value, hidPAYEE_ID.Value, int.Parse(GetLanguageID()));
			else
				hidOldData.Value	=	"";
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
			//this.cmbPARTY_ID.SelectedIndexChanged += new System.EventHandler(this.cmbPARTY_ID_SelectedIndexChanged);			
			this.cmbPARTY_ID.ServerChange    += new System.EventHandler(this.cmbPARTY_ID_SelectedIndexChanged);    
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);
			 

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
			rfvPARTY_ID.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("830");
//			rfvPAYMENT_METHOD.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("786");
			rfvADDRESS1.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("787");
			rfvSTATE.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("788");
			rfvZIP.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("789");
			//rfvNARRATIVE.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("790");
			csvNARRATIVE.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("791");
			csvTO_ORDER_DESC.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("985");
			revZIP.ValidationExpression = aRegExpZip; 
			revZIP.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("24");
			rfvCOUNTRY.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("33");
			rngAMOUNT.ErrorMessage				  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
			rfvAMOUNT.ErrorMessage				  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("335");
			rfvINVOICE_NUMBER.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("780");
//			rfvINVOICE_DATE.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("785");
			revINVOICE_DATE.ValidationExpression  = aRegExpDate; 
			revINVOICE_DATE.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			csvINVOICE_DATE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("822");
//			rfvSERVICE_TYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("781");
			//rfvSERVICE_DESCRIPTION.ErrorMessage =		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("782");
			csvSERVICE_DESCRIPTION.ErrorMessage	=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("783");
			//revAMOUNT.ValidationExpression = aRegExpCurrencyformat;//Done by Sibin on 2 Feb 09 for Itrack Issue 5385
			//Done for Itrack Issue 6516 on 15 Oct 09
			//revAMOUNT.ValidationExpression = aRegExpDoublePositiveZero;
			revAMOUNT.ValidationExpression = aRegExpDoublePositiveNonZero;
			revAMOUNT.ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");//Done by Sibin on 2 Feb 09 for Itrack Issue 5385
		}
		#endregion

		/*private void cmbPARTY_ID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataSet dsTemp = new DataSet();			
			try
			{
				//if(cmbPARTY_ID.SelectedItem!=null && cmbPARTY_ID.SelectedItem.Value!="" && cmbPARTY_ID.SelectedItem.Value!="-1")
			      if(cmbPARTY_ID!=null && cmbPARTY_ID.Value!="" && cmbPARTY_ID.Value!="-1")	
			{
					//arryPartyID = cmbPARTY_ID.SelectedItem.Value.Split('^');
					  arryPartyID = cmbPARTY_ID.Value.Split('^');
					//dsTemp = ClsAddPartyDetails.GetValuesForParty(hidCLAIM_ID.Value,cmbPARTY_ID.SelectedItem.Value);
					dsTemp = ClsAddPartyDetails.GetValuesForParty(hidCLAIM_ID.Value,arryPartyID[0]);
					if (dsTemp!=null && dsTemp.Tables.Count>0 && dsTemp.Tables[0].Rows.Count>0)
					{
						DataRow drTemp = dsTemp.Tables[0].Rows[0];					
						//lblPAYEE.Text = drTemp["NAME"].ToString();
						//lblREFERENCE.Text = drTemp["REFERENCE"].ToString();
						if(drTemp["ADDRESS1"]!=null && drTemp["ADDRESS1"].ToString()!="")
							txtADDRESS1.Text = drTemp["ADDRESS1"].ToString();
						if(drTemp["ADDRESS2"]!=null && drTemp["ADDRESS2"].ToString()!="")
							txtADDRESS2.Text =  drTemp["ADDRESS2"].ToString();
						if(drTemp["CITY"]!=null && drTemp["CITY"].ToString()!="")
							txtCITY.Text = drTemp["CITY"].ToString();
						//lblBANK_NAME.Text = drTemp["BANK_NAME"].ToString();
						//lblACCOUNT_NUMBER.Text = drTemp["ACCOUNT_NUMBER"].ToString();
						//lblACCOUNT_NAME.Text = drTemp["ACCOUNT_NAME"].ToString();
						if (drTemp["COUNTRY"]!=null && drTemp["COUNTRY"].ToString()!="" && drTemp["COUNTRY"].ToString() == "0")
							cmbCOUNTRY.SelectedIndex = 0;
						else
							cmbCOUNTRY.SelectedValue = drTemp["COUNTRY"].ToString();
				
						if (drTemp["STATE"]!=null && drTemp["STATE"].ToString()!="" && drTemp["STATE"].ToString() == "0")
							cmbSTATE.SelectedIndex = 0;
						else
							cmbSTATE.SelectedValue = drTemp["STATE"].ToString();
						if(drTemp["ZIP"]!=null && drTemp["ZIP"].ToString()!="")
							txtZIP.Text = drTemp["ZIP"].ToString();
						//hidPARTY_ID.Value = drTemp["PARTY_ID"].ToString();
					}
				}
			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;
				lblMessage.Visible = true;
			}
			finally
			{
				if(dsTemp!=null)
					dsTemp.Dispose();
				if(objPayee!=null)
					objPayee.Dispose();
			}
		}*/
		private void cmbPARTY_ID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			
			DataSet dsTemp = new DataSet();			
			try
			{
				//if(cmbPARTY_ID.SelectedItem!=null && cmbPARTY_ID.SelectedItem.Value!="" && cmbPARTY_ID.SelectedItem.Value!="-1")
				  if(cmbPARTY_ID!=null && cmbPARTY_ID.Value!="" && cmbPARTY_ID.Value!="-1")	
			{
					//arryPartyID = cmbPARTY_ID.SelectedItem.Value.Split('^');
					  arryPartyID = cmbPARTY_ID.Value.Split('^');
					//dsTemp = ClsAddPartyDetails.GetValuesForParty(hidCLAIM_ID.Value,cmbPARTY_ID.SelectedItem.Value);
                      dsTemp = ClsAddPartyDetails.GetValuesForParty(hidCLAIM_ID.Value, arryPartyID[0], int.Parse(GetLanguageID()));
					if (dsTemp!=null && dsTemp.Tables.Count>0 && dsTemp.Tables[0].Rows.Count>0)
					{
						DataRow drTemp = dsTemp.Tables[0].Rows[0];					
						//lblPAYEE.Text = drTemp["NAME"].ToString();
						//lblREFERENCE.Text = drTemp["REFERENCE"].ToString();
						if(drTemp["ADDRESS1"]!=null && drTemp["ADDRESS1"].ToString()!="")
							txtADDRESS1.Text = drTemp["ADDRESS1"].ToString();
						if(drTemp["ADDRESS2"]!=null && drTemp["ADDRESS2"].ToString()!="")
							txtADDRESS2.Text =  drTemp["ADDRESS2"].ToString();
						if(drTemp["CITY"]!=null && drTemp["CITY"].ToString()!="")
							txtCITY.Text = drTemp["CITY"].ToString();
						//lblBANK_NAME.Text = drTemp["BANK_NAME"].ToString();
						//lblACCOUNT_NUMBER.Text = drTemp["ACCOUNT_NUMBER"].ToString();
						//lblACCOUNT_NAME.Text = drTemp["ACCOUNT_NAME"].ToString();
						if (drTemp["COUNTRY"]!=null && drTemp["COUNTRY"].ToString()!="" && drTemp["COUNTRY"].ToString() == "0")
							cmbCOUNTRY.SelectedIndex = 0;
						else
							cmbCOUNTRY.SelectedValue = drTemp["COUNTRY"].ToString();
				
						if (drTemp["STATE"]!=null && drTemp["STATE"].ToString()!="" && drTemp["STATE"].ToString() == "0")
							cmbSTATE.SelectedIndex = 0;
						else
							cmbSTATE.SelectedValue = drTemp["STATE"].ToString();
						if(drTemp["ZIP"]!=null && drTemp["ZIP"].ToString()!="")
							txtZIP.Text = drTemp["ZIP"].ToString();
						//hidPARTY_ID.Value = drTemp["PARTY_ID"].ToString();                          
						
						


					}
				}
			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;
				lblMessage.Visible = true;
			}
			finally
			{
				if(dsTemp!=null)
					dsTemp.Dispose();
				if(objPayee!=null)
					objPayee.Dispose();
				//BindDropDown(ddlToBind,dtSource);

			}
		}



		#region FillDropDowns
		private void FillDropDowns()
		{
			
//			cmbPAYMENT_METHOD.DataSource =  Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("PYMTD");  //Action on Payment Lookup
//			cmbPAYMENT_METHOD.DataTextField="LookupDesc";
//			cmbPAYMENT_METHOD.DataValueField="LookupID";
//			cmbPAYMENT_METHOD.DataBind();

			DataTable dtState = Cms.CmsWeb.ClsFetcher.State;
			cmbSTATE.DataSource			= dtState;
			cmbSTATE.DataTextField		= "STATE_NAME";
			cmbSTATE.DataValueField		= "STATE_ID";

			cmbSTATE.DataBind();
			cmbSTATE.Items.Insert(0,new ListItem("",""));
			cmbSTATE.SelectedIndex=0;

			DataTable dtCountry = Cms.CmsWeb.ClsFetcher.Country;
			cmbCOUNTRY.DataSource			= dtCountry;
			cmbCOUNTRY.DataTextField		= "COUNTRY_NAME";
			cmbCOUNTRY.DataValueField		= "COUNTRY_ID";
			cmbCOUNTRY.DataBind();
			//cmbCOUNTRY.Items.Insert(0,new ListItem("",""));		
			BindDropDown(cmbPARTY_ID);
			//ClsActivityExpense objExpense = new ClsActivityExpense();			
			//cmbPARTY_ID.DataSource =  objExpense.GetClaimAllParties(hidCLAIM_ID.Value);
			/*
			cmbPARTY_ID.DataTextField="NAME";
			cmbPARTY_ID.DataValueField="PARTY_ID";
			cmbPARTY_ID.DataBind();
			cmbPARTY_ID.Items.Insert(0,new ListItem("",""));			
            */
            
//			DataSet ds = objExpense.GetClaimAllParties(hidCLAIM_ID.Value);
//			if(ds!=null)
//			 BindDropDown(cmbPARTY_ID,ds.Tables[0]);
			cmbSERVICE_TYPE.DataSource =  ClsDefaultValues.GetDefaultValuesDetails((int)enumClaimDefaultValues.SERVICE_TYPES);  //Service Types
			cmbSERVICE_TYPE.DataTextField="DETAIL_TYPE_DESCRIPTION";
			cmbSERVICE_TYPE.DataValueField="DETAIL_TYPE_ID";
			cmbSERVICE_TYPE.DataBind();
			cmbSERVICE_TYPE.Items.Insert(0,"");
			//BindDropDown();
			 
		}
		#endregion

		#region SetCaptions
		private void SetCaptions()
		{
			//capPAYEE.Text			=		objResourceMgr.GetString("lblPAYEE");
			//capREFERENCE.Text		=		objResourceMgr.GetString("lblREFERENCE");
//			capPAYMENT_METHOD.Text	=		objResourceMgr.GetString("txtPAYMENT_METHOD");
			capADDRESS1.Text		=		objResourceMgr.GetString("txtADDRESS1");
			capADDRESS2.Text		=		objResourceMgr.GetString("txtADDRESS2");
			capCITY.Text 			=		objResourceMgr.GetString("txtCITY");
			capSTATE.Text 			=		objResourceMgr.GetString("cmbSTATE");
			capCOUNTRY.Text 		=		objResourceMgr.GetString("cmbCOUNTRY");
			capZIP.Text 			=		objResourceMgr.GetString("txtZIP");
			capNARRATIVE.Text 		=		objResourceMgr.GetString("txtNARRATIVE");
			//capBANK_NAME.Text 		=		objResourceMgr.GetString("lblBANK_NAME");
			//capACCOUNT_NUMBER.Text  =		objResourceMgr.GetString("lblACCOUNT_NUMBER");
			//capACCOUNT_NAME.Text 	=		objResourceMgr.GetString("lblACCOUNT_NAME");
			capAMOUNT.Text 	=		objResourceMgr.GetString("txtAMOUNT");
			capPARTY_ID.Text 			=		objResourceMgr.GetString("cmbPARTY_ID");
			capINVOICE_NUMBER.Text 			=		objResourceMgr.GetString("txtINVOICE_NUMBER");
			capINVOICE_DATE.Text 			=		objResourceMgr.GetString("txtINVOICE_DATE");
			capSERVICE_TYPE.Text 			=		objResourceMgr.GetString("cmbSERVICE_TYPE");
			capSERVICE_DESCRIPTION.Text 	=		objResourceMgr.GetString("txtSERVICE_DESCRIPTION");
			capTO_ORDER_DESC.Text 		=		objResourceMgr.GetString("txtTO_ORDER_DESC");
		}
		#endregion

		#region GetFormValue
		private ClsPayeeInfo GetFormValue()
		{
			ClsPayeeInfo objPInfo = new ClsPayeeInfo();
			
			if(hidCLAIM_ID.Value != "")
				objPInfo.CLAIM_ID = int.Parse(hidCLAIM_ID.Value);
				
			if (hidPAYEE_ID.Value != "")
				objPInfo.PAYEE_ID = int.Parse(hidPAYEE_ID.Value);

		

//			objPInfo.PAYMENT_METHOD = int.Parse(cmbPAYMENT_METHOD.SelectedValue);
			/*Lookup values for Payment Method
				11787>Check
				11788>EFT*/
//			if (cmbPAYMENT_METHOD.SelectedValue == "11787")
//			{
				objPInfo.ADDRESS1 = txtADDRESS1.Text;
				objPInfo.ADDRESS2 = txtADDRESS2.Text;
				objPInfo.CITY = txtCITY.Text;
				objPInfo.ZIP = txtZIP.Text;
				if(cmbSTATE.SelectedItem!=null && cmbSTATE.SelectedItem.Value!="")
					objPInfo.STATE = int.Parse(cmbSTATE.SelectedValue);
				if(cmbCOUNTRY.SelectedItem!=null && cmbCOUNTRY.SelectedItem.Value!="")
					objPInfo.COUNTRY = int.Parse(cmbCOUNTRY.SelectedValue);
//			}
//			else
//			{
//				objPInfo.ADDRESS1 = "";
//				objPInfo.ADDRESS2 = "";
//				objPInfo.CITY = "";
//				objPInfo.ZIP = "";
//				objPInfo.STATE = 0;
//				objPInfo.COUNTRY = 0;
//			}

			objPInfo.NARRATIVE = txtNARRATIVE.Text;
			objPInfo.TO_ORDER_DESC = txtTO_ORDER_DESC.Text.Trim();
			if(txtAMOUNT.Text!="")
				objPInfo.AMOUNT = Convert.ToDouble(txtAMOUNT.Text.Trim());
			//objPInfo.PAYEE_ACTIVITY_ID = int.Parse(hidACTIVITY_ID.Value);
			//if(hidCALLED_FROM.Value == CALLED_FROM_EXPENSE)
				//objPInfo.PARTY_ID = int.Parse(hidPARTY_ID.Value);
			/*else
			{
				if(cmbPAYEE.SelectedItem!=null && cmbPAYEE.SelectedItem.Value!="")
					objPInfo.PARTY_ID = int.Parse(cmbPAYEE.SelectedItem.Value);
			}*/
			objPInfo.ACTIVITY_ID = int.Parse(hidACTIVITY_ID.Value);
			objPInfo.EXPENSE_ID = int.Parse(hidEXPENSE_ID.Value);

			//if(cmbPARTY_ID.SelectedItem!=null && cmbPARTY_ID.SelectedItem.Value!="")
			 if(cmbPARTY_ID!=null && cmbPARTY_ID.Value!="")
		    {
				
				arryPartyID = cmbPARTY_ID.Value.Split('^');
				//objPInfo.PARTY_ID = cmbPARTY_ID.SelectedItem.Value;
				objPInfo.PARTY_ID = arryPartyID[0];
			}
			objPInfo.INVOICE_NUMBER = txtINVOICE_NUMBER.Text.Trim();
			if(txtINVOICE_DATE.Text.Trim()!="")
				objPInfo.INVOICE_DATE = Convert.ToDateTime(txtINVOICE_DATE.Text.Trim());

			if(cmbSERVICE_TYPE.SelectedItem!=null && cmbSERVICE_TYPE.SelectedItem.Value!="") 
				objPInfo.SERVICE_TYPE = int.Parse(cmbSERVICE_TYPE.SelectedItem.Value);
			objPInfo.SERVICE_DESCRIPTION = txtSERVICE_DESCRIPTION.Text.Trim();

			if(hidPAYEE_ID.Value == "")
				strRowId="NEW";
			else
			{
				strRowId=hidPAYEE_ID.Value; 
				objPInfo.PAYEE_ID =	int.Parse(hidPAYEE_ID.Value);	
			}
			return objPInfo;
		}
		#endregion

		//Display RED 
		public void BindDropDown(HtmlSelect ddlToBind)		
		{
            ddlToBind.Items.Clear();   
			ClsActivityExpense objExpense = new ClsActivityExpense();
			DataSet ds = objExpense.GetClaimAllParties(hidCLAIM_ID.Value,int.Parse(hidACTIVITY_ID.Value),0);
			//if(ds!=null)
				//BindDropDown(cmbPARTY_ID,ds.Tables[0]);				
			DataView dv = new DataView(ds.Tables[0]);
			
			int i =0;
			try
			{
		
				foreach(DataRowView row in dv)
				{
						
					ddlToBind.Items.Add(new ListItem(row["NAME"].ToString(),row["PARTY_ID"].ToString()));
					string[] strCompanyData ;
					strCompanyData = row["PARTY_ID"].ToString().Split('^');
					if(strCompanyData[1] =="10963") 
					{			
						ddlToBind.Items[i].Attributes.Add ("Class","GrandFatheredRange");
					
					}   
			  
															  
					i++;
				}
				ddlToBind.Items.Insert(0,"");
			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;
			}
			finally
			{}
//		   ClsActivityExpense objExpense = new ClsActivityExpense();
//			DataSet ds = objExpense.GetClaimAllParties(hidCLAIM_ID.Value);
//			if(ds!=null)
//				BindDropDown(cmbPARTY_ID,ds.Tables[0]);	
		}


		/*private void cmbPAYEE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataSet dsTemp = new DataSet();			
			try
			{
				if(cmbPAYEE.SelectedItem!=null && cmbPAYEE.SelectedItem.Value!="")
				{
					dsTemp = ClsAddPartyDetails.GetValuesForParty(hidCLAIM_ID.Value,cmbPAYEE.SelectedItem.Value);
					if (dsTemp!=null && dsTemp.Tables.Count>0 && dsTemp.Tables[0].Rows.Count>0)
					{
						DataRow drTemp = dsTemp.Tables[0].Rows[0];					
						//lblPAYEE.Text = drTemp["NAME"].ToString();
						lblREFERENCE.Text = drTemp["REFERENCE"].ToString();
						txtADDRESS1.Text = drTemp["ADDRESS1"].ToString();
						txtADDRESS2.Text =  drTemp["ADDRESS2"].ToString();
						txtCITY.Text = drTemp["CITY"].ToString();
						//lblBANK_NAME.Text = drTemp["BANK_NAME"].ToString();
						//lblACCOUNT_NUMBER.Text = drTemp["ACCOUNT_NUMBER"].ToString();
						//lblACCOUNT_NAME.Text = drTemp["ACCOUNT_NAME"].ToString();
						if (drTemp["COUNTRY"].ToString() == "0")
							cmbCOUNTRY.SelectedIndex = 0;
						else
							cmbCOUNTRY.SelectedValue = drTemp["COUNTRY"].ToString();
				
						if (drTemp["STATE"]!=null && drTemp["STATE"].ToString()!="" && drTemp["STATE"].ToString() == "0")
							cmbSTATE.SelectedIndex = 0;
						else
							cmbSTATE.SelectedValue = drTemp["STATE"].ToString();

						txtZIP.Text = drTemp["ZIP"].ToString();
						//hidPARTY_ID.Value = drTemp["PARTY_ID"].ToString();
					}
				}
			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;
				lblMessage.Visible = true;
			}
			finally
			{
				if(dsTemp!=null)
					dsTemp.Dispose();
				if(objPayee!=null)
					objPayee.Dispose();
			}
		}*/



		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal=0;	
			    //Condition added For Itrack issue #5857
				string[] selected_value = null;

				if(cmbPARTY_ID.Value!="")
                selected_value = cmbPARTY_ID.Value.ToString().Split('^'); 
				
				string cmbvalue = selected_value[1]; 
				if(cmbvalue == "10963")
				{ 
					lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1050");   
					lblMessage.Visible	=	true;
				}
				else
				{
					//Retreiving the form values into model class object
					ClsPayeeInfo objPInfo = GetFormValue();	
					if(strRowId.ToUpper().Equals("NEW")) //save case
					{
						objPInfo.CREATED_BY = int.Parse(GetUserId());
						objPInfo.CREATED_DATETIME = DateTime.Now;
					
						//Calling the add method of business layer class
						///intRetVal = objPayee.Add(objPInfo,hidCALLED_FROM.Value);

						if(intRetVal>0)
						{
							hidPAYEE_ID.Value = objPInfo.PAYEE_ID.ToString();
							lblMessage.Text				=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"29");
							hidFormSaved.Value			=	"1";
							hidIS_ACTIVE.Value       	= "Y";                       
							LoadData();						
							//BindDropDown(cmbPARTY_ID);  							
							GetOldDataXML();
						}
						else if(intRetVal == -1) 
						{
							lblMessage.Text				=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("819");	
							hidFormSaved.Value			=		"2";
						}
							/*else if(intRetVal == -2)	// Duplicate code exist, update failed
							{
								lblMessage.Text			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("819");	
								hidFormSaved.Value		=	"2";
							}*/
						else
						{
							lblMessage.Text				=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"20");
							hidFormSaved.Value			=	"2";
						}					
					} // end save case
					else //UPDATE CASE
					{
						//Creating the Model object for holding the Old data
						ClsPayeeInfo objOldPayeeInfo = new ClsPayeeInfo();

						//Setting  the Old Page details(XML File containing old details) into the Model Object
						base.PopulateModelObject(objOldPayeeInfo,hidOldData.Value);

						//Setting those values into the Model object which are not in the page					
						objPInfo.MODIFIED_BY = int.Parse(GetUserId());
						objPInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					
						//Updating the record using business layer class object
					///	intRetVal	= objPayee.Update(objOldPayeeInfo,objPInfo,hidCALLED_FROM.Value);					
						if( intRetVal > 0 )			// update successfully performed
						{
							lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
							hidFormSaved.Value		=	"1";
							BindDropDown(cmbPARTY_ID);						
							LoadData();
						}
						else if(intRetVal == -1)	
						{
							lblMessage.Text			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("819");	
							hidFormSaved.Value		=	"2";
						}
							/*else if(intRetVal == -2)	// Duplicate code exist, update failed
							{
								lblMessage.Text			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("819");	
								hidFormSaved.Value		=	"2";
							}*/
						else 
						{
							lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
							hidFormSaved.Value		=	"1";
						}					
					}
					lblMessage.Visible = true;
				}
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
			    
			}  
		}

	
		#region LoadData
		private void LoadData()
		{
			if (hidCLAIM_ID.Value != "" && hidPAYEE_ID.Value != "")
			{
                DataSet dsTemp = objPayee.GetValuesForPageControls(hidCLAIM_ID.Value, hidACTIVITY_ID.Value,  int.Parse(GetLanguageID()));
				if (dsTemp != null && dsTemp.Tables[0].Rows.Count > 0)
				{
					DataRow dr = dsTemp.Tables[0].Rows[0];
					
					//lblPAYEE.Text = dr["NAME"].ToString();
					//lblREFERENCE.Text = dr["REFERENCE"].ToString();
//					cmbPAYMENT_METHOD.SelectedValue = dr["PAYMENT_METHOD"].ToString();
					txtADDRESS1.Text = dr["ADDRESS1"].ToString();
					txtADDRESS2.Text = dr["ADDRESS2"].ToString();
					//lblBANK_NAME.Text = dr["BANK_NAME"].ToString();
					//lblACCOUNT_NUMBER.Text = dr["ACCOUNT_NUMBER"].ToString();
					//lblACCOUNT_NAME.Text = dr["ACCOUNT_NAME"].ToString();
					txtCITY.Text = dr["CITY"].ToString();
					if(dr["AMOUNT"]!=null && dr["AMOUNT"].ToString()!="")
						txtAMOUNT.Text= Double.Parse(dr["AMOUNT"].ToString()).ToString("N");
						//txtAMOUNT.Text=String.Format("{0:,#,###}",Convert.ToInt64(dr["AMOUNT"]));
				
					//Added by Asfa (24-Mar-2008) - iTrack issue #3936
					if(dr["CHECK_NUMBER"]!=null && dr["CHECK_NUMBER"].ToString()!="" && dr["CHECK_NUMBER"].ToString()!="0")
						lblCHECK_NUMBER.Text =dr["CHECK_NUMBER"].ToString();
					if(dr["STATUS"]!=null && dr["STATUS"].ToString()!="")
						lblCHECK_STATUS.Text =dr["STATUS"].ToString();

					/*Lookup values for Payment Method
					11787>Check
					11788>EFT*/
//					if (cmbPAYMENT_METHOD.SelectedValue == "11787")
//					{
						if(dr["STATE"]!=null && dr["STATE"].ToString()!="" && dr["STATE"].ToString()!="0")
							cmbSTATE.SelectedValue =  dr["STATE"].ToString();	
						//if(dr["COUNTRY"]!=null && dr["COUNTRY"].ToString()!="" && dr["COUNTRY"].ToString()!="0")
						//cmbCOUNTRY.SelectedValue = dr["COUNTRY"].ToString();
//					}
//					else
//					{
//						cmbSTATE.SelectedIndex =  0;
//						//cmbCOUNTRY.SelectedIndex = 0;
//					}
					/*if(hidCALLED_FROM.Value == CALLED_FROM_PAYMENT)
					{
						if(dr["PARTY_ID"]!=null && dr["PARTY_ID"].ToString()!="" && dr["PARTY_ID"].ToString()!="")
							cmbPAYEE.SelectedValue = dr["PARTY_ID"].ToString();
					}*/
					BindDropDown(cmbPARTY_ID);
					txtZIP.Text = dr["ZIP"].ToString();
					txtNARRATIVE.Text = dr["NARRATIVE"].ToString();
					txtTO_ORDER_DESC.Text = dr["TO_ORDER_DESC"].ToString();					
					/*if(dr["PARTY_ID"]!=null && dr["PARTY_ID"].ToString()!="")
					{
						if(dr["REQ_SPECIAL_HANDLING"]!=null && dr["REQ_SPECIAL_HANDLING"].ToString()!="")
						{
							cmbPARTY_ID.Value=dr["PARTY_ID"].ToString() + "^" + dr["REQ_SPECIAL_HANDLING"].ToString();
						}

					}*/
					if(dr["PARTY_ID"]!=null && dr["PARTY_ID"].ToString()!="")
					{
					 cmbPARTY_ID.Value = dr["PARTY_ID"].ToString() + "^" + "10964" ;  

					}
					if(dr["SERVICE_TYPE"]!=null && dr["SERVICE_TYPE"].ToString()!="" && dr["SERVICE_TYPE"].ToString()!="0")
						cmbSERVICE_TYPE.SelectedValue=dr["SERVICE_TYPE"].ToString();					
					txtINVOICE_NUMBER.Text = dr["INVOICE_NUMBER"].ToString();
					txtINVOICE_DATE.Text = dr["INVOICE_DATE"].ToString();					
					txtSERVICE_DESCRIPTION.Text=dr["SERVICE_DESCRIPTION"].ToString();
					
				}
			}
		}
		#endregion

//		private void SetPayeeDetails()
//		{
//			DataSet dsTemp;
//			//if(hidCALLED_FROM.Value == CALLED_FROM_EXPENSE)
//			//{
//				dsTemp = objPayee.GetPayeeDetails(hidCLAIM_ID.Value,hidACTIVITY_ID.Value, hidEXPENSE_ID.Value);
//				if (dsTemp.Tables[0].Rows.Count > 0)
//				{
//					DataRow drTemp = dsTemp.Tables[0].Rows[0];
//					
//					lblPAYEE.Text = drTemp["NAME"].ToString();
//					lblREFERENCE.Text = drTemp["REFERENCE"].ToString();
//					txtADDRESS1.Text = drTemp["ADDRESS1"].ToString();
//					txtADDRESS2.Text =  drTemp["ADDRESS2"].ToString();
//					txtCITY.Text = drTemp["CITY"].ToString();
//					//lblBANK_NAME.Text = drTemp["BANK_NAME"].ToString();
//					//lblACCOUNT_NUMBER.Text = drTemp["ACCOUNT_NUMBER"].ToString();
//					//lblACCOUNT_NAME.Text = drTemp["ACCOUNT_NAME"].ToString();
//					/*if (drTemp["COUNTRY"].ToString() == "0")
//						cmbCOUNTRY.SelectedIndex = 0;
//					else
//						cmbCOUNTRY.SelectedValue = drTemp["COUNTRY"].ToString();*/
//				
//					if (drTemp["STATE"].ToString() == "0")
//						cmbSTATE.SelectedIndex = 0;
//					else
//						cmbSTATE.SelectedValue = drTemp["STATE"].ToString();
//
//					txtZIP.Text = drTemp["ZIP"].ToString();
//					hidPARTY_ID.Value = drTemp["PARTY_ID"].ToString();
//				}
//			//}
//			/*else
//			{
//				dsTemp = objPayee.GetPayeeDetailsForPayment(hidCLAIM_ID.Value,hidACTIVITY_ID.Value);
//				if(dsTemp!=null && dsTemp.Tables.Count>0 && dsTemp.Tables[0].Rows.Count>0)
//				{
//					cmbPAYEE.DataSource = dsTemp.Tables[0];
//					cmbPAYEE.DataTextField = "NAME";
//					cmbPAYEE.DataValueField = "PARTY_ID";
//					cmbPAYEE.DataBind();
//					cmbPAYEE.Items.Insert(0,"");
//				}		
//			}*/
//		}
	}
}
