/* ***************************************************************************************
   Author		: Harmanjeet Singh
   Creation Date: April 18, 2007
   Last Updated : 
   Reviewed By	: 
   Purpose		: This file is used for SPECIAL ACCEPTANCE AMOUNT REINSURANCE. 
   Comments		: 
   ------------------------------------------------------------------------------------- 
   History	Date	     Modified By		Description
   
   ------------------------------------------------------------------------------------- 
   *****************************************************************************************/

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
using Cms.Model.Maintenance;
using Cms.BusinessLayer.BlCommon;  
using Cms.CmsWeb.Controls; 
using Cms.ExceptionPublisher; 
using Cms.Model.Maintenance.Reinsurance;

namespace Cms.CmsWeb.Maintenance.Reinsurance
{
	/// <summary>
	/// Summary description for ReinsuranceBankingDetails.
	/// </summary>
	public class ReinsuranceBankingDetails : Cms.CmsWeb.cmsbase
	{
		#region DECELARATION

		
		
		ClsReinsuranceBankingDetails objReinsuranceBankingDetail; //=new ClsReinsuranceBankingDetails();
		Cms.Model.Maintenance.Reinsurance.ClsReinsuranceBankingDetailsInfo objReinsuranceBankingDetailsInfo;
			
		System.Resources.ResourceManager objResourceMgr;
		
		
		private string strRowId, strFormSaved;
		protected string oldXML;

		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.HyperLink hlkZipLookup;
		protected System.Web.UI.WebControls.Label lblCopy_Address;
		protected Cms.CmsWeb.Controls.CmsButton btnCopyPhysicalAddress;
		protected System.Web.UI.WebControls.HyperLink hlkMZipLookup;
		protected System.Web.UI.WebControls.Label capREIN_BANK_NAME;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnBackToReinsurer;
		protected Cms.CmsWeb.Controls.CmsButton btnPullPhysicalAddress;
		protected Cms.CmsWeb.Controls.CmsButton btnPullMailingAddress;
        protected System.Web.UI.WebControls.Label capMessages;


		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidREIN_CONTACT_ID;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCompany_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAddress1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAddress2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCity;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidState;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCountry;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidZip;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidM_Address1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidM_Address2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidM_City;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidM_State;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidM_Country;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidM_Zip;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCheckAdd;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCarriername;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidcarriercode;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidcarrierType;
       
		protected System.Web.UI.WebControls.Label capREIN_BANK_DETAIL_ADDRESS_1;
//		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_BANK_DETAIL_ADDRESS_1;
		protected System.Web.UI.WebControls.Label capREIN_BANK_DETAIL_ADDRESS_2;
		protected System.Web.UI.WebControls.Label capREIN_BANK_DETAIL_CITY;
		protected System.Web.UI.WebControls.Label capREIN_BANK_DETAIL_COUNTRY;
		protected System.Web.UI.WebControls.Label capREIN_BANK_DETAIL_STATE;
		protected System.Web.UI.WebControls.DropDownList cmbREIN_BANK_DETAIL_STATE;
		protected System.Web.UI.WebControls.Label capREIN_BANK_DETAIL_ZIP;
		protected System.Web.UI.WebControls.Label capM_REIN_BANK_DETAIL_ADDRESS_1;
//		protected System.Web.UI.WebControls.RequiredFieldValidator rfvM_REIN_BANK_DETAIL_ADDRESS_1;
		protected System.Web.UI.WebControls.Label capM_REIN_BANK_DETAIL_ADDRESS_2;
		protected System.Web.UI.WebControls.Label capM_REIN_BANK_DETAIL_CITY;
		protected System.Web.UI.WebControls.Label capM_REIN_BANK_DETAIL_COUNTRY;
		protected System.Web.UI.WebControls.DropDownList cmbM_REIN_BANK_DETAIL_COUNTRY;
		protected System.Web.UI.WebControls.Label capM_REIN_BANK_DETAIL_STATE;
		protected System.Web.UI.WebControls.DropDownList cmbM_REIN_BANK_DETAIL_STATE;
		protected System.Web.UI.WebControls.Label capM_REIN_BANK_DETAIL_ZIP;
		protected System.Web.UI.WebControls.Label capREIN_TRANSIT_ROUTING;
		protected System.Web.UI.WebControls.Label capREIN_BANK_ACCOUNT;
        protected System.Web.UI.WebControls.Label capPHYSICAL;//SNEHA
        protected System.Web.UI.WebControls.Label capMAIL; //sneha
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidREIN_BANK_DETAIL_ID;
		protected System.Web.UI.WebControls.Label capREIN_PAYMENT_BASIS;
		protected System.Web.UI.WebControls.TextBox txtREIN_BANK_NAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_BANK_NAME;
		protected System.Web.UI.WebControls.TextBox txtREIN_BANK_ACCOUNT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_BANK_ACCOUNT;
		
		protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_BANK_ACCOUNT;
		protected System.Web.UI.WebControls.TextBox txtREIN_BANK_DETAIL_ZIP;
		protected System.Web.UI.WebControls.TextBox txtM_REIN_BANK_DETAIL_ADDRESS_1;
		protected System.Web.UI.WebControls.TextBox txtM_REIN_BANK_DETAIL_CITY;
		protected System.Web.UI.WebControls.TextBox txtM_REIN_BANK_DETAIL_ZIP;
		protected System.Web.UI.WebControls.DropDownList cmbREIN_PAYMENT_BASIS;
		protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_BANK_DETAIL_ADDRESS_1;
		protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_BANK_DETAIL_ADDRESS_2;
		protected System.Web.UI.WebControls.TextBox txtREIN_BANK_DETAIL_ADDRESS_1;
		protected System.Web.UI.WebControls.TextBox txtREIN_BANK_DETAIL_ADDRESS_2;
		protected System.Web.UI.WebControls.TextBox txtREIN_BANK_DETAIL_CITY;
		protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_BANK_DETAIL_CITY;
		protected System.Web.UI.WebControls.DropDownList cmbREIN_BANK_DETAIL_COUNTRY;
		protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_BANK_DETAIL_ZIP;
		protected System.Web.UI.WebControls.RegularExpressionValidator revM_REIN_BANK_DETAIL_ADDRESS_1;
		protected System.Web.UI.WebControls.RegularExpressionValidator revM_REIN_BANK_DETAIL_CITY;
		protected System.Web.UI.WebControls.RegularExpressionValidator revM_REIN_BANK_DETAIL_ZIP;
		protected System.Web.UI.WebControls.TextBox txtM_REIN_BANK_DETAIL_ADDRESS_2;
		protected System.Web.UI.WebControls.RegularExpressionValidator revM_REIN_BANK_DETAIL_ADDRESS_2;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_PAYMENT_BASIS;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidReset;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidREIN_COMAPANY_ID;

		protected System.Web.UI.WebControls.TextBox txtREIN_TRANSIT_ROUTING;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_TRANSIT_ROUTING;
		protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_TRANSIT_ROUTING;
		protected System.Web.UI.WebControls.CustomValidator csvREIN_TRANSIT_ROUTING;
		protected System.Web.UI.WebControls.CustomValidator	csvVERIFY_ROUTING_NUMBER;
		protected System.Web.UI.WebControls.CustomValidator	csvVERIFY_ROUTING_NUMBER_LENGHT;
		# endregion
	
		# region PAGE LOAD
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			try
			{
				//this.txtSpecialAcceptanceLimit.Text=objClsReinsuranceSpecialAcceptanceAmount.PersistValue();
				//objReinsuranceContractType = new ClsReinsuranceContractType();
				//objReinsuranceContractName = new ClsReinsuranceContractName();
				btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
				btnSave.Attributes.Add("onclick","javascript:chkAdd();");
				objReinsuranceBankingDetail=new ClsReinsuranceBankingDetails();
				//btnReset.Attributes.Add("onclick","javascript:return Reset();");
				btnBackToReinsurer.Attributes.Add("onclick","setURL();");
				this.cmbREIN_PAYMENT_BASIS.Attributes.Add("onchange","javascript:return ShowEFTDetail();");

				this.btnPullPhysicalAddress.Attributes.Add("onclick","return CopyPhysicalAddress();");
				this.btnPullMailingAddress.Attributes.Add("onclick","return CopyMailingAddress();");

				base.VerifyAddress(hlkZipLookup, txtREIN_BANK_DETAIL_ADDRESS_1,txtREIN_BANK_DETAIL_ADDRESS_2
					, txtREIN_BANK_DETAIL_CITY, cmbREIN_BANK_DETAIL_STATE, txtREIN_BANK_DETAIL_ZIP);
				
				base.VerifyAddress(hlkMZipLookup, txtM_REIN_BANK_DETAIL_ADDRESS_1,txtM_REIN_BANK_DETAIL_ADDRESS_2
					, txtM_REIN_BANK_DETAIL_CITY, cmbM_REIN_BANK_DETAIL_STATE, txtM_REIN_BANK_DETAIL_ZIP);


				//base.ScreenId = "402";
				base.ScreenId = "263_1_1";
				lblMessage.Visible = false;
				SetErrorMessages();
                capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");

				//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
				btnReset.CmsButtonClass		=	CmsButtonType.Write;
				btnReset.PermissionString	=	gstrSecurityXML;

				btnSave.CmsButtonClass		=	CmsButtonType.Write;
				btnSave.PermissionString	=	gstrSecurityXML;

				//this.btnActivateDeactivate.CmsButtonClass=CmsButtonType.Execute;
				//this.btnActivateDeactivate.PermissionString=gstrSecurityXML;

				this.btnBackToReinsurer.CmsButtonClass=CmsButtonType.Write;
				this.btnBackToReinsurer.PermissionString=gstrSecurityXML;

				this.btnPullMailingAddress.CmsButtonClass=CmsButtonType.Write;
				this.btnPullMailingAddress.PermissionString=gstrSecurityXML;
			
				this.btnPullPhysicalAddress.CmsButtonClass=CmsButtonType.Write;
				this.btnPullPhysicalAddress.PermissionString=gstrSecurityXML;
			
				
				objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.Reinsurance.ReinsuranceBankingDetails" ,System.Reflection.Assembly.GetExecutingAssembly());

				if(!Page.IsPostBack)
				{
					//if(Request.QueryString["SPECIAL_ACCEPTANCE_LIMIT_ID"]!=null && Request.QueryString["SPECIAL_ACCEPTANCE_LIMIT_ID"].ToString().Length>0)
					//	hid_SPECIAL_ACCEPTANCE_LIMIT_ID.Value = Request.QueryString["SPECIAL_ACCEPTANCE_LIMIT_ID"].ToString();
					hidCompany_ID.Value = Request.Params["REIN_COMAPANY_ID"].ToString();

					
					if (Request.Params["REIN_COMAPANY_ID"] != null)
					{
						if (Request.Params["REIN_COMAPANY_ID"].ToString() != "")
						{
							this.hidREIN_COMAPANY_ID.Value = Request.Params["REIN_COMAPANY_ID"].ToString();
							GenerateXML(hidREIN_COMAPANY_ID.Value);
						}
					}
					
					SetCaptions();
					fillCombo();
					GetDataForEditMode();
					getAddressValues();
					
				}
			}
			catch(Exception oEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);			
			}

		}
		# endregion PAGE LOAD
		private void fillCombo()
		{
			//using singleton object for country and state dropdown
				
			DataTable dt = Cms.CmsWeb.ClsFetcher.Country;
			this.cmbREIN_BANK_DETAIL_COUNTRY.DataSource		= dt;
			cmbREIN_BANK_DETAIL_COUNTRY.DataTextField	= "Country_Name";
			cmbREIN_BANK_DETAIL_COUNTRY.DataValueField	= "Country_Id";
			cmbREIN_BANK_DETAIL_COUNTRY.DataBind();
			cmbREIN_BANK_DETAIL_COUNTRY.Items[0].Selected=true;  

			cmbM_REIN_BANK_DETAIL_COUNTRY.DataSource		= dt;
			cmbM_REIN_BANK_DETAIL_COUNTRY.DataTextField	= "Country_Name";
			cmbM_REIN_BANK_DETAIL_COUNTRY.DataValueField	= "Country_Id";
			cmbM_REIN_BANK_DETAIL_COUNTRY.DataBind();
			cmbM_REIN_BANK_DETAIL_COUNTRY.Items[0].Selected=true; 

			dt = Cms.CmsWeb.ClsFetcher.State;
			cmbREIN_BANK_DETAIL_STATE.DataSource		= dt;
			cmbREIN_BANK_DETAIL_STATE.DataTextField	= "STATE_NAME";
			cmbREIN_BANK_DETAIL_STATE.DataValueField	= "STATE_ID";
			cmbREIN_BANK_DETAIL_STATE.DataBind();
			cmbREIN_BANK_DETAIL_STATE.Items.Insert(0,"");
				
			//START APRIL 11 HARMANJEET
			this.cmbREIN_PAYMENT_BASIS.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("PYMTY");
			cmbREIN_PAYMENT_BASIS.DataTextField="LookupDesc";
			cmbREIN_PAYMENT_BASIS.DataValueField="LookupCode";
			cmbREIN_PAYMENT_BASIS.DataBind();
			cmbREIN_PAYMENT_BASIS.Items.Insert(0,"");	
			//END HARMANJEET


			dt = Cms.CmsWeb.ClsFetcher.State;
			cmbM_REIN_BANK_DETAIL_STATE.DataSource		= dt;
			cmbM_REIN_BANK_DETAIL_STATE.DataTextField	= "STATE_NAME";
			cmbM_REIN_BANK_DETAIL_STATE.DataValueField	= "STATE_ID";
			cmbM_REIN_BANK_DETAIL_STATE.DataBind();
			cmbM_REIN_BANK_DETAIL_STATE.Items.Insert(0,"");
				
		}

		# region  S E T   P A G E   V A L I D A T I O N S   E R R O R   M E S S A G E S 

		private void SetErrorMessages()
		{
			
			try
			{
//				this.rfvM_REIN_BANK_DETAIL_ADDRESS_1.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("742");
//				this.rfvREIN_BANK_DETAIL_ADDRESS_1.ErrorMessage				=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("742");
				//this.rfvREIN_BANK_NAME.ErrorMessage							=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"17");
				//this.rfvREIN_TRANSIT_ROUTING.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"17");
				//this.rfvREIN_BANK_ACCOUNT.ErrorMessage						=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"17");
				rfvREIN_PAYMENT_BASIS.ErrorMessage			=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6"); //sneha			=  //"Please Select Payment Basis";

                this.revREIN_BANK_DETAIL_ZIP.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1084");
                revREIN_BANK_DETAIL_ZIP.ValidationExpression = aRegExpZipBrazil;

                this.revM_REIN_BANK_DETAIL_ZIP.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1084");
                revM_REIN_BANK_DETAIL_ZIP.ValidationExpression = aRegExpZipBrazil;

                revREIN_BANK_DETAIL_ADDRESS_1.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1"); //sneha
                revREIN_BANK_DETAIL_CITY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "2"); //sneha
                revM_REIN_BANK_DETAIL_ADDRESS_1.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1"); //sneha
                revM_REIN_BANK_DETAIL_CITY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "2"); //sneha
                rfvREIN_BANK_NAME.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "3"); //sneha
                rfvREIN_BANK_ACCOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "4"); //sneha
                revREIN_BANK_ACCOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5"); //sneha
                revREIN_BANK_DETAIL_ADDRESS_2.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1"); //sneha
                revM_REIN_BANK_DETAIL_ADDRESS_2.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1");
                rfvREIN_TRANSIT_ROUTING.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "7"); //sneha
                csvREIN_TRANSIT_ROUTING.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "8"); //sneha
                revREIN_TRANSIT_ROUTING.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "9"); //sneha
                csvVERIFY_ROUTING_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "10"); //sneha
                csvVERIFY_ROUTING_NUMBER_LENGHT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "11"); //sneha
			}
			catch(Exception oEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);
			}
			
			//Regular Expression for validation
			//revSPECIAL_ACCEPTANCE_LIMIT.ValidationExpression				=	aRegExpDecimal;
		}

		# endregion
		
		# region  S E T   L A B E L   C A P T I O N S 

		private void SetCaptions()
		{
			try
			{
				//capSPECIAL_ACCEPTANCE_LIMIT.Text	=	objResourceMgr.GetString("txtSPECIAL_ACCEPTANCE_LIMIT");
				this.capREIN_BANK_DETAIL_ADDRESS_1.Text=objResourceMgr.GetString("txtREIN_BANK_DETAIL_ADDRESS_1");
				this.capREIN_BANK_DETAIL_ADDRESS_2.Text=objResourceMgr.GetString("txtREIN_BANK_DETAIL_ADDRESS_2");
				this.capREIN_BANK_DETAIL_CITY.Text=objResourceMgr.GetString("txtREIN_BANK_DETAIL_CITY");
				this.capREIN_BANK_DETAIL_COUNTRY.Text=objResourceMgr.GetString("cmbREIN_BANK_DETAIL_COUNTRY");
				this.capREIN_BANK_DETAIL_STATE.Text=objResourceMgr.GetString("cmbREIN_BANK_DETAIL_STATE");
				this.capREIN_BANK_DETAIL_ZIP.Text=objResourceMgr.GetString("txtREIN_BANK_DETAIL_ZIP");
				
				this.capM_REIN_BANK_DETAIL_ADDRESS_1.Text=objResourceMgr.GetString("txtM_REIN_BANK_DETAIL_ADDRESS_1");
				this.capM_REIN_BANK_DETAIL_ADDRESS_2.Text=objResourceMgr.GetString("txtM_REIN_BANK_DETAIL_ADDRESS_2");
				this.capM_REIN_BANK_DETAIL_CITY.Text=objResourceMgr.GetString("txtM_REIN_BANK_DETAIL_CITY");
				this.capM_REIN_BANK_DETAIL_COUNTRY.Text=objResourceMgr.GetString("cmbM_REIN_BANK_DETAIL_COUNTRY");
				this.capM_REIN_BANK_DETAIL_STATE.Text=objResourceMgr.GetString("cmbM_REIN_BANK_DETAIL_STATE");
				this.capM_REIN_BANK_DETAIL_ZIP.Text=objResourceMgr.GetString("txtM_REIN_BANK_DETAIL_ZIP");

				this.capREIN_PAYMENT_BASIS.Text=objResourceMgr.GetString("cmbREIN_PAYMENT_BASIS");
				this.capREIN_BANK_NAME.Text=objResourceMgr.GetString("txtREIN_BANK_NAME");
				

				this.capREIN_TRANSIT_ROUTING.Text=objResourceMgr.GetString("txtREIN_TRANSIT_ROUTING");
				this.capREIN_BANK_ACCOUNT.Text=objResourceMgr.GetString("txtREIN_BANK_ACCOUNT");
				//this.capM_REIN_BANKING_DETAIL_ADDRESS_1.Text=objResourceMgr.GetString("");
                capPHYSICAL.Text = objResourceMgr.GetString("capPHYSICAL"); //SNEHA
                btnPullPhysicalAddress.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1345"); //sneha
                btnPullMailingAddress.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1346"); //sneha
                capMAIL.Text = objResourceMgr.GetString("capMAIL"); //sneha
                btnBackToReinsurer.Text = objResourceMgr.GetString("btnBackToReinsurer"); //SNEHA
			}
			catch(Exception oEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);
			}
		}

		# endregion
		
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
			this.btnBackToReinsurer.Click += new System.EventHandler(this.btnBackToReinsurer_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		# region PULL ADDRESS VALUES
		private void getAddressValues()
		{
			try
			{
				ClsReinsurer objReinsurer=new ClsReinsurer();
				string str=Request.QueryString["REIN_COMAPANY_ID"];
				//Response.Write(str);
				DataSet oDs=objReinsurer.GetDataForPageControls(str);
				if(oDs.Tables[0].Rows.Count > 0)
				{
					DataRow oDr=oDs.Tables[0].Rows[0];

                    hidAddress1.Value = oDr["REIN_COMAPANY_ADD1"].ToString();
                    hidAddress2.Value = oDr["REIN_COMAPANY_ADD2"].ToString();
                    hidCity.Value = oDr["REIN_COMAPANY_CITY"].ToString();
                    hidZip.Value = oDr["REIN_COMAPANY_ZIP"].ToString();
                    hidState.Value = oDr["REIN_COMAPANY_STATE"].ToString();
                    hidCountry.Value = oDr["REIN_COMAPANY_COUNTRY"].ToString();

                    hidCarriername.Value = oDr["REIN_COMAPANY_NAME"].ToString();
                    hidcarriercode.Value = oDr["REIN_COMAPANY_CODE"].ToString();
                    hidcarrierType.Value = oDr["REIN_COMAPANY_TYPE"].ToString();

					hidM_Address1.Value=oDr["M_REIN_COMPANY_ADD_1"].ToString();
					hidM_Address2.Value=oDr["M_RREIN_COMPANY_ADD_2"].ToString();
					hidM_City.Value=oDr["M_REIN_COMPANY_CITY"].ToString();
					hidM_Zip.Value=oDr["M_REIN_COMPANY_ZIP"].ToString();
					hidM_State.Value=oDr["M_REIN_COMPANY_STATE"].ToString();
					hidM_Country.Value=oDr["M_REIN_COMPANY_COUNTRY"].ToString();



				}

			}
			catch(Exception oEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);
			}
			finally{}

		}

		# endregion 

		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsReinsuranceBankingDetailsInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			objReinsuranceBankingDetailsInfo = new ClsReinsuranceBankingDetailsInfo();

			
			objReinsuranceBankingDetailsInfo.REIN_BANK_DETAIL_ADDRESS_1=this.txtREIN_BANK_DETAIL_ADDRESS_1.Text;
			objReinsuranceBankingDetailsInfo.REIN_BANK_DETAIL_ADDRESS_2=this.txtREIN_BANK_DETAIL_ADDRESS_2.Text;
			objReinsuranceBankingDetailsInfo.REIN_BANK_DETAIL_CITY  =	txtREIN_BANK_DETAIL_CITY.Text;
			objReinsuranceBankingDetailsInfo.REIN_BANK_DETAIL_COUNTRY=	cmbREIN_BANK_DETAIL_COUNTRY.SelectedValue;
			objReinsuranceBankingDetailsInfo.REIN_BANK_DETAIL_STATE  =	cmbREIN_BANK_DETAIL_STATE.SelectedValue;
			objReinsuranceBankingDetailsInfo.REIN_BANK_DETAIL_ZIP    =	txtREIN_BANK_DETAIL_ZIP.Text;
			
			
			objReinsuranceBankingDetailsInfo.M_REIN_BANK_DETAIL_ADDRESS_1= txtM_REIN_BANK_DETAIL_ADDRESS_1.Text;
			objReinsuranceBankingDetailsInfo.M_REIN_BANK_DETAIL_ADDRESS_2= this.txtM_REIN_BANK_DETAIL_ADDRESS_2.Text;
			objReinsuranceBankingDetailsInfo.M_REIN_BANK_DETAIL_CITY =txtM_REIN_BANK_DETAIL_CITY.Text;			
			objReinsuranceBankingDetailsInfo.M_REIN_BANK_DETAIL_COUNTRY= cmbM_REIN_BANK_DETAIL_COUNTRY.SelectedValue;
			objReinsuranceBankingDetailsInfo.M_REIN_BANK_DETAIL_STATE= cmbM_REIN_BANK_DETAIL_STATE.SelectedValue;
			objReinsuranceBankingDetailsInfo.M_REIN_BANK_DETAIL_ZIP= txtM_REIN_BANK_DETAIL_ZIP.Text;
			

			objReinsuranceBankingDetailsInfo.REIN_PAYMENT_BASIS   =	cmbREIN_PAYMENT_BASIS.SelectedValue;
			if (cmbREIN_PAYMENT_BASIS.SelectedValue=="EFT")
			{
				objReinsuranceBankingDetailsInfo.REIN_BANK_NAME    =	txtREIN_BANK_NAME.Text;
				objReinsuranceBankingDetailsInfo.REIN_TRANSIT_ROUTING=this.txtREIN_TRANSIT_ROUTING.Text;
				objReinsuranceBankingDetailsInfo.REIN_BANK_ACCOUNT = this.txtREIN_BANK_ACCOUNT.Text;
			}
			objReinsuranceBankingDetailsInfo.REIN_COMAPANY_ID = int.Parse(this.hidCompany_ID.Value);
            objReinsuranceBankingDetailsInfo.CUSTOM_INFO = ClsMessages.GetMessage("263_0", "6") + ":" + hidCarriername.Value + "<br>"
                                   + ClsMessages.GetMessage("263_0", "7") + ":" + hidcarriercode.Value + "<br>"
                                  + ClsMessages.GetMessage("263_0", "8") + ":" + hidcarrierType.Value;
					
			//objReinsuranceContactInfo.IS_ACTIVE=hidIS_ACTIVE.Value;
			
			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	this.hidREIN_BANK_DETAIL_ID.Value;
			//oldXML		= hidOldData.Value;
			//Returning the model object

			return objReinsuranceBankingDetailsInfo;
		}
		#endregion
		
		
		
		# region GENERATE XML FILE
		/// <summary>
		/// fetching data based on query string values
		/// </summary>
		private void GenerateXML(string REIN_CONTACT_ID)
		{
			string strREIN_CONTACT_ID=REIN_CONTACT_ID;
            

			objReinsuranceBankingDetail=new ClsReinsuranceBankingDetails(); 
  
			
			if(strREIN_CONTACT_ID!="" && strREIN_CONTACT_ID!=null)
			{
				try
				{
					DataSet ds=new DataSet(); 
					ds=objReinsuranceBankingDetail.GetDataForPageControls(strREIN_CONTACT_ID);
					if(ds.Tables[0].Rows.Count>0)
					hidOldData.Value=ds.GetXml(); 
					else
						hidOldData.Value=""; 
					//hidFormSaved.Value="1"; 
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
					if(objReinsuranceBankingDetail!= null)
						objReinsuranceBankingDetail.Dispose();
				}  
                
			}
                
		}

				
		# endregion

		# region  G E T   D A T A   F O R   E D I T   M O D E 

		private void GetDataForEditMode()
		{
			try
			{
				
				objReinsuranceBankingDetail = new ClsReinsuranceBankingDetails();
				DataSet oDs = objReinsuranceBankingDetail.GetDataForPageControls(this.hidCompany_ID.Value);
				if(oDs.Tables[0].Rows.Count >0)
				{
					DataRow oDr  = oDs.Tables[0].Rows[0];
					
					//if(oDr["REIN_COMAPANY_NAME"].ToString() != "")
					//	txtREIN_COMAPANY_NAME.Text		= oDr["REIN_COMAPANY_NAME"].ToString();
					
					
					txtREIN_BANK_DETAIL_ADDRESS_1.Text=oDr["REIN_BANK_DETAIL_ADDRESS_1"].ToString();
					txtREIN_BANK_DETAIL_ADDRESS_2.Text=oDr["REIN_BANK_DETAIL_ADDRESS_2"].ToString();
					txtREIN_BANK_DETAIL_CITY.Text=oDr["REIN_BANK_DETAIL_CITY"].ToString();
					//cmbREIN_BANK_DETAIL_COUNTRY.SelectedValue=oDr["REIN_BANK_DETAIL_COUNTRY"].ToString();
					//cmbREIN_BANK_DETAIL_STATE.SelectedValue=oDr["REIN_BANK_DETAIL_STATE"].ToString();
					txtREIN_BANK_DETAIL_ZIP.Text=oDr["REIN_BANK_DETAIL_ZIP"].ToString();


					
					//cmbREIN_PAYMENT_BASIS.Text=oDr["REIN_PAYMENT_BASIS"].ToString();
					txtREIN_BANK_NAME.Text=oDr["REIN_BANK_NAME"].ToString();
					txtREIN_TRANSIT_ROUTING.Text=oDr["REIN_TRANSIT_ROUTING"].ToString();
					txtREIN_BANK_ACCOUNT.Text=oDr["REIN_BANK_ACCOUNT"].ToString();


					txtM_REIN_BANK_DETAIL_ADDRESS_1.Text=oDr["M_REIN_BANK_DETAIL_ADDRESS_1"].ToString();
					txtM_REIN_BANK_DETAIL_ADDRESS_2.Text=oDr["M_REIN_BANK_DETAIL_ADDRESS_2"].ToString();
					txtM_REIN_BANK_DETAIL_CITY.Text=oDr["M_REIN_BANK_DETAIL_CITY"].ToString();			
					//cmbM_REIN_BANK_DETAIL_COUNTRY.SelectedValue=oDr["M_REIN_BANK_DETAIL_COUNTRY"].ToString();
					// cmbM_REIN_BANK_DETAIL_STATE.SelectedValue=oDr["M_REIN_BANK_DETAIL_STATE"].ToString();
					txtM_REIN_BANK_DETAIL_ZIP.Text=oDr["M_REIN_BANK_DETAIL_ZIP"].ToString();
					
					
					
								
					

					//if(oDr["IS_ACTIVE"].ToString()=="Y")
					//	btnActivateDeactivate.Text="DeActivate";
					//if(oDr["IS_ACTIVE"].ToString()=="N")
					//	btnActivateDeactivate.Text="Activate";
					
				}
			}
			catch(Exception oEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);
			}
			finally{}
		}

		# endregion G E T   D A T A   F O R   E D I T   M O D E

		

		
		#region S A V E B U T T O N E V E N T
		/// <summary>
		/// If form is posted back then add entry in database using the BL object
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			if(hidCheckAdd.Value != "0")
			{
				hidFormSaved.Value			=	"1";
				return;
			}
            try
			{
				int intRetVal;	//For retreiving the return value of business class save function
				objReinsuranceBankingDetail = new ClsReinsuranceBankingDetails();

				//Retreiving the form values into model class object
                ClsReinsuranceBankingDetailsInfo objReinsuranceBankingDetailsInfo = GetFormValue();
              

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objReinsuranceBankingDetailsInfo.CREATED_BY = int.Parse(GetUserId());
					objReinsuranceBankingDetailsInfo.CREATED_DATETIME = DateTime.Now;
					objReinsuranceBankingDetailsInfo.IS_ACTIVE="Y"; 

					//Calling the add method of business layer class
					intRetVal = objReinsuranceBankingDetail.Add(objReinsuranceBankingDetailsInfo);

					if(intRetVal>0)
					{
						this.hidREIN_BANK_DETAIL_ID.Value = objReinsuranceBankingDetailsInfo.REIN_BANK_DETAIL_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						this.hidOldData.Value	= objReinsuranceBankingDetail.GetDataForPageControls(this.hidCompany_ID.Value).GetXml();
                      
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
					ClsReinsuranceBankingDetailsInfo objOldReinsuranceBankingDetailsInfo;
					objOldReinsuranceBankingDetailsInfo = new ClsReinsuranceBankingDetailsInfo();
					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldReinsuranceBankingDetailsInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					if(strRowId!="")
						objReinsuranceBankingDetailsInfo.REIN_BANK_DETAIL_ID = int.Parse(strRowId);
					objReinsuranceBankingDetailsInfo.MODIFIED_BY = int.Parse(GetUserId());
					objReinsuranceBankingDetailsInfo.LAST_UPDATED_DATETIME = DateTime.Now;
                    

					//Updating the record using business layer class object
					intRetVal	= objReinsuranceBankingDetail.Update(objOldReinsuranceBankingDetailsInfo,objReinsuranceBankingDetailsInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						this.hidOldData.Value	= objReinsuranceBankingDetail.GetDataForPageControls(hidCompany_ID.Value).GetXml();
                   
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
                
			}
			finally
			{
				if(objReinsuranceBankingDetail!= null)
					objReinsuranceBankingDetail.Dispose();
			}
	
		}
		

		#endregion

		private void btnBackToReinsurer_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("../AddReinsurer.aspx?REIN_COMAPANY_ID="+hidCompany_ID.Value);
		}

		

	}
}


