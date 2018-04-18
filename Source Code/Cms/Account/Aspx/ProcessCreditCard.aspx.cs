#region NameSpaces
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
using Cms.Model.Account;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlAccount;
using Cms.BusinessLayer.BlCommon;  
using Cms.CmsWeb;
using System.Configuration;
using System.Xml;
using Cms.CmsWeb.Utils;
using Cms.ExceptionPublisher.ExceptionManagement; 
#endregion
namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for ProcessCreditCard.
	/// </summary>
	public class ProcessCreditCard : Cms.Account.AccountBase
	{
		#region Page Variable declarations
		protected System.Web.UI.WebControls.Label capCARD_NO;
		protected System.Web.UI.WebControls.TextBox txtCARD_NO;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCARD_NO;
		protected System.Web.UI.WebControls.CustomValidator csvCARD_NO;
		protected System.Web.UI.WebControls.Label capCARD_DATE_VALID_TO;
		protected System.Web.UI.WebControls.TextBox txtCARD_DATE_VALID_TO;
		protected System.Web.UI.WebControls.Label capCARD_CVV_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtCARD_CVV_NUMBER;
		protected System.Web.UI.WebControls.CustomValidator csvCARD_CVV_NUMBER;
		protected System.Web.UI.WebControls.Label capCARD_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbCARD_TYPE;
		//Done for Itrack Issue 6099 on 14 July 2009
		//protected Cms.CmsWeb.Controls.CmsButton btnCCSave;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected System.Web.UI.WebControls.Label capPOLICY_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtPOLICY_NUMBER;
		protected System.Web.UI.WebControls.Panel pnlCCCust;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnPOLICY_NO;
		protected System.Web.UI.HtmlControls.HtmlImage imgPOLICY_NO;
		public string URL;
		public string ServiceURL; 
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICYINFO;
		protected System.Web.UI.WebControls.Label capAMOUNT;
		protected System.Web.UI.WebControls.TextBox txtAMOUNT;
		protected System.Web.UI.WebControls.Label capREFERENCE_ID;
		protected System.Web.UI.WebControls.TextBox txtREFERENCE_ID;
		protected System.Web.UI.WebControls.RegularExpressionValidator revAMOUNT;
		protected System.Web.UI.WebControls.DropDownList cmbCARD_DATE_VALID_TO;
		protected System.Web.UI.WebControls.RangeValidator rngCARD_DATE_VALID_TO;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPOLICY_NUMBER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAMOUNT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCARD_NO;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCARD_DATE_VALID_TO;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCARD_CVV_NUMBER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCARD_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREFERENCE_ID;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCARD_CVV_NUMBER;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label lblTOTAL_DUE;
		protected System.Web.UI.WebControls.Label lblMIN_DUE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCARD_TYPE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_NAME;
		protected System.Web.UI.WebControls.CustomValidator csvCARD_DATE_VALID_TO;
		System.Resources.ResourceManager objResourceMgr;
		protected System.Web.UI.WebControls.Label capCUSTOMER_FIRST_NAME;
		protected System.Web.UI.WebControls.TextBox txtCUSTOMER_FIRST_NAME;
		protected System.Web.UI.WebControls.Label capCUSTOMER_MIDDLE_NAME;
		protected System.Web.UI.WebControls.TextBox txtCUSTOMER_MIDDLE_NAME;
		protected System.Web.UI.WebControls.Label capCUSTOMER_LAST_NAME;
		protected System.Web.UI.WebControls.TextBox txtCUSTOMER_LAST_NAME;
		protected System.Web.UI.WebControls.Label capCUSTOMER_ADDRESS1;
		protected System.Web.UI.WebControls.TextBox txtCUSTOMER_ADDRESS1;
		protected System.Web.UI.WebControls.Label capCUSTOMER_ADDRESS2;
		protected System.Web.UI.WebControls.TextBox txtCUSTOMER_ADDRESS2;
		protected System.Web.UI.WebControls.Label capCUSTOMER_CITY;
		protected System.Web.UI.WebControls.TextBox txtCUSTOMER_CITY;
		protected System.Web.UI.WebControls.Label capCUSTOMER_COUNTRY;
		protected System.Web.UI.WebControls.DropDownList cmbCUSTOMER_COUNTRY;
		protected System.Web.UI.WebControls.Label capCUSTOMER_STATE;
		protected System.Web.UI.WebControls.DropDownList cmbCUSTOMER_STATE;
		protected System.Web.UI.WebControls.Label capCUSTOMER_ZIP;
		protected System.Web.UI.WebControls.TextBox txtCUSTOMER_ZIP;
		protected System.Web.UI.WebControls.HyperLink hlkZipLookup;
		protected System.Web.UI.WebControls.CustomValidator csvCUSTOMER_ZIP;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCUSTOMER_ZIP;
		protected System.Web.UI.WebControls.Label capNOTE;
		protected System.Web.UI.WebControls.TextBox txtNOTE;
		protected System.Web.UI.WebControls.CustomValidator csvDESCRIPTION;
		protected System.Web.UI.WebControls.Image imgZipLookup;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE_COUNTRY_LIST;//Added on 25 Nov 2008 by Sibin
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE_ID; //Added on 25 Nov 2008 by Sibin
		string strAmt;
		protected Cms.CmsWeb.Controls.CmsButton btnCCProcessing;	//Added by Shikha on 10 Mar 2009, #5534.
		
		#endregion
	
		#region Page Load
        private void Page_Prerender(object sender, System.EventArgs e)
        {
            btnSave.Text = ClsMessages.GetButtonsText(ScreenId, "btnSave");
            btnReset.Text = ClsMessages.GetButtonsText(ScreenId, "btnReset");
        }


		private void Page_Load(object sender, System.EventArgs e)
		{
			Ajax.Utility.RegisterTypeForAjax(typeof(ProcessCreditCard)); 			
			base.ScreenId="404_0";
			URL = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL();
			btnReset.Attributes.Add("onclick","javascript:return ResetForm();");
			//Done for Itrack Issue 6099 on 14 July 2009
			//btnCCSave.Attributes.Add("Onclick","javascript:HideShowTransactionInProgress();DisableZipForCanada();");	//Changed by Shikha #5534(10/03/09).
			btnSave.Attributes.Add("Onclick","javascript:HideShowTransactionInProgress();DisableZipForCanada();");	//Changed by Shikha #5534(10/03/09).
			//btnCCSave.Attributes.Add("Onclick","javascript:DisableZipForCanada();"); //Added on 25 Nov 2008 : To validate without Blur Text BOX-Added by Sibin

			btnReset.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Execute;
			btnReset.PermissionString = gstrSecurityXML;
			//Done for Itrack Issue 6099 on 14 July 2009
			//btnCCSave.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Execute;
			//btnCCSave.PermissionString = gstrSecurityXML;
			btnSave.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Execute;
			btnSave.PermissionString = gstrSecurityXML;

			ServiceURL =System.Configuration.ConfigurationSettings.AppSettings["CmsWebUrl"].ToString();
			ServiceURL+="webservices/wscmsweb.asmx?WSDL";	
		
			//Added by Shikha For #5534, 10/03/09.
			btnCCProcessing.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Execute;
			btnCCProcessing.PermissionString = gstrSecurityXML;
			//End of addition.
					
			objResourceMgr = new System.Resources.ResourceManager("Cms.Account.Aspx.ProcessCreditCard" ,System.Reflection.Assembly.GetExecutingAssembly());

			if(!Page.IsPostBack)
			{
				SetCaptions();
				setErrorMessages();
				fillCombos();
				//btnCCSave.Attributes.Add("onClick","javascript:ChkForNegAmt(document.getElementById('txtAMOUNT'));");
				imgZipLookup.Attributes.Add("style","cursor:hand");
				base.VerifyAddress(hlkZipLookup, txtCUSTOMER_ADDRESS1,txtCUSTOMER_ADDRESS2
					, txtCUSTOMER_CITY, cmbCUSTOMER_STATE, txtCUSTOMER_ZIP);
				cmbCOUNTRY_Changed();
			}
		}
		#endregion

		#region Helper Functions : ErrorMessages / FillCombos / SetCaptions
		private void setErrorMessages()
		{
			revCARD_NO.ErrorMessage							=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");	
			revCARD_NO.ValidationExpression					=	aRegExpInteger;
			//	csvCARD_NO.ErrorMessage							=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("955");	
			csvCARD_CVV_NUMBER.ErrorMessage					=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("959");
			revCARD_CVV_NUMBER.ErrorMessage					= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");	
			revCARD_CVV_NUMBER.ValidationExpression			= aRegExpInteger;
			revAMOUNT.ErrorMessage							=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");
			revAMOUNT.ValidationExpression					=	aRegExpDoublePositiveNonZero;//aRegExpCurrencyformat;
			rngCARD_DATE_VALID_TO.MinimumValue				=	"0";
			rngCARD_DATE_VALID_TO.MaximumValue				=	"99";
			rngCARD_DATE_VALID_TO.ErrorMessage				=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			rfvAMOUNT.ErrorMessage							=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
			rfvCARD_CVV_NUMBER.ErrorMessage					=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
			rfvCARD_DATE_VALID_TO.ErrorMessage				=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
			rfvCARD_NO.ErrorMessage							=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6");
			rfvCARD_TYPE.ErrorMessage						=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");
			rfvPOLICY_NUMBER.ErrorMessage					=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"8");
			csvCARD_DATE_VALID_TO.ErrorMessage				=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"9");
			rfvREFERENCE_ID.ErrorMessage					=   Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"10");
			csvCARD_CVV_NUMBER.ErrorMessage                 =   Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"11");      
			//this.csvDESCRIPTION.ErrorMessage				=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("441");
			revCUSTOMER_ZIP.ErrorMessage					=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"24");
			revCUSTOMER_ZIP.ValidationExpression			=	aRegExpZip;
		}

		private void fillCombos()
		{
			cmbCARD_TYPE.DataSource		= Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CCTYPE");
			cmbCARD_TYPE.DataTextField	= "LookupDesc";
			cmbCARD_TYPE.DataValueField	= "LookupID";
			cmbCARD_TYPE.DataBind();
			cmbCARD_TYPE.Items.Insert(0,"");

			//DataTable dt = Cms.CmsWeb.ClsFetcher.Country;
			DataTable dt = Cms.CmsWeb.ClsFetcher.AllCountry;
			cmbCUSTOMER_COUNTRY.DataSource			= dt;
			cmbCUSTOMER_COUNTRY.DataTextField		= COUNTRY_NAME;
			cmbCUSTOMER_COUNTRY.DataValueField		= COUNTRY_ID;
			cmbCUSTOMER_COUNTRY.DataBind();
			cmbCUSTOMER_COUNTRY.Items.Insert(0,"");

			dt = Cms.CmsWeb.ClsFetcher.State;
			cmbCUSTOMER_STATE.DataSource			= dt;
			cmbCUSTOMER_STATE.DataTextField			= STATE_NAME;
			cmbCUSTOMER_STATE.DataValueField		= STATE_ID;
			cmbCUSTOMER_STATE.DataBind();
			cmbCUSTOMER_STATE.Items.Insert(0,"");
		}

		private void SetCaptions()
		{	
			capPOLICY_NUMBER.Text		= objResourceMgr.GetString("txtPOLICY_NUMBER");
			capCARD_CVV_NUMBER.Text		= objResourceMgr.GetString("txtCARD_CVV_NUMBER");
			capCARD_DATE_VALID_TO.Text	= objResourceMgr.GetString("txtCARD_DATE_VALID_TO");
			capCARD_NO.Text  			= objResourceMgr.GetString("txtCARD_NO");
			capCARD_TYPE.Text  			= objResourceMgr.GetString("cmbCARD_TYPE");
			capAMOUNT.Text				= objResourceMgr.GetString("txtAMOUNT");
			//Cust Info
			capCUSTOMER_FIRST_NAME.Text		= objResourceMgr.GetString("txtCUSTOMER_FIRST_NAME");
			capCUSTOMER_MIDDLE_NAME.Text		= objResourceMgr.GetString("txtCUSTOMER_MIDDLE_NAME");
			capCUSTOMER_LAST_NAME.Text			= objResourceMgr.GetString("txtCUSTOMER_LAST_NAME");
			capCUSTOMER_ADDRESS1.Text		= objResourceMgr.GetString("txtCUSTOMER_ADDRESS1");
			capCUSTOMER_ADDRESS2.Text		= objResourceMgr.GetString("txtCUSTOMER_ADDRESS2");
			capCUSTOMER_CITY.Text			= objResourceMgr.GetString("txtCUSTOMER_CITY");
			capCUSTOMER_COUNTRY.Text			= objResourceMgr.GetString("cmbCUSTOMER_COUNTRY");
			capCUSTOMER_STATE.Text			= objResourceMgr.GetString("cmbCUSTOMER_STATE");
			capCUSTOMER_ZIP.Text				= objResourceMgr.GetString("txtCUSTOMER_ZIP");
            
		}

		#endregion

		//sibin

		private void cmbCOUNTRY_Changed()
		{
			try
			{
				if(cmbCUSTOMER_COUNTRY.SelectedItem!=null && cmbCUSTOMER_COUNTRY.SelectedItem.Value!="")
				{
					PopulateStateDropDown(int.Parse(cmbCUSTOMER_COUNTRY.SelectedItem.Value));										
				}
				else
					PopulateStateDropDown(1);
			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;
				lblMessage.Visible = true;
			}
			finally
			{
			}
		}

		private void PopulateStateDropDown(int COUNTRY_ID)
		{
			cmbCUSTOMER_STATE.Items.Clear();
			ClsStates objStates = new ClsStates();
			DataSet dsStates;
			if(COUNTRY_ID==0)
				return;
			else
			{
				dsStates = objStates.GetStatesCountry(COUNTRY_ID);
				hidSTATE_COUNTRY_LIST.Value=ClsCommon.GetXMLEncoded(dsStates.Tables[0]);

			}

//			cmbCUSTOMER_STATE.Items.Clear();
			DataTable dtStates = objStates.GetStatesForCountry(COUNTRY_ID);
			if(dtStates!=null && dtStates.Rows.Count>0)
			{
				cmbCUSTOMER_STATE.DataSource = dtStates;
				cmbCUSTOMER_STATE.DataTextField			= STATE_NAME;
				cmbCUSTOMER_STATE.DataValueField		= STATE_ID;
				cmbCUSTOMER_STATE.DataBind();
				cmbCUSTOMER_STATE.Items.Insert(0,"");					
			}

			if(COUNTRY_ID!=1)
			{
				revCUSTOMER_ZIP.Enabled = false;				
				imgZipLookup.Attributes.Add("style","display:none");				
				//spnCUSTOMER_STATE.Attributes.Add("style","display:none");			
				//spnCUSTOMER_ZIP.Attributes.Add("style","display:none");			
			}
			else
			{
				revCUSTOMER_ZIP.Enabled = true;				
				imgZipLookup.Attributes.Add("style","display:inline");			
				//spnCUSTOMER_STATE.Attributes.Add("style","display:inline");			
				//spnCUSTOMER_ZIP.Attributes.Add("style","display:inline");							
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
			//Done for Itrack Issue 6099 on 14 July 2009
			//this.btnCCSave.Click += new System.EventHandler(this.btnCCSave_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);
            this.PreRender += new System.EventHandler(this.Page_Prerender); 
		}
		#endregion
		
		#region Process Transaction
		//Done for Itrack Issue 6099 on 14 July 2009
		//private void btnCCSave_Click(object sender, System.EventArgs e)
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			
			ClsCreditCard objCreditCardBL = new ClsCreditCard();
			TransactionInfo objTranInfo = new TransactionInfo();
			ClsStates objStates = new ClsStates();
		
			try
			{	
				objCreditCardBL.PayPalAPI.UserName    = ConfigurationManager.AppSettings.Get("PayPalUserName");
                objCreditCardBL.PayPalAPI.VendorName = ConfigurationManager.AppSettings.Get("PayPalVendor");
                objCreditCardBL.PayPalAPI.HostName = ConfigurationManager.AppSettings.Get("HostName");
                objCreditCardBL.PayPalAPI.PartnerName = ConfigurationManager.AppSettings.Get("PaypalPartner");
                objCreditCardBL.PayPalAPI.Password = ConfigurationManager.AppSettings.Get("PayPalPassword");

								
				if(txtAMOUNT.Text !="")
				{
					strAmt = txtAMOUNT.Text.Trim();
					if(strAmt.StartsWith("-"))
					{
						char[] arrTrimChar = {'-'};
						objTranInfo.Amount = strAmt.TrimStart(arrTrimChar);
					}
					else
					{
						objTranInfo.Amount = txtAMOUNT.Text.Trim();

						//Card Number / Date / CVV Number not used for (-) AMOUNT 
						// Card Number
						if(txtCARD_NO.Text != "")
							objTranInfo.CardNumber = txtCARD_NO.Text.Trim();
						// From To Date
						string strToDate = cmbCARD_DATE_VALID_TO.SelectedValue + txtCARD_DATE_VALID_TO.Text;
						objTranInfo.ExpiryDate = strToDate;
						// CVV Number
						objTranInfo.CVV2 = txtCARD_CVV_NUMBER.Text.Trim();
					}
				}

				//Policy Number
				objTranInfo.PolicyNumber = txtPOLICY_NUMBER.Text;

				//Customer Name
				if(hidCUSTOMER_NAME.Value != "")
					objTranInfo.CustomerName = hidCUSTOMER_NAME.Value;			

				//Previous Reference Id for -ve Amount (V78A0BC59AD5)
				if(txtREFERENCE_ID.Text != "")
					objTranInfo.RefrenceID = txtREFERENCE_ID.Text;

				#region Set Customer Info in PP Transaction
				if(txtCUSTOMER_FIRST_NAME.Text !="")
					objTranInfo.FirstName = txtCUSTOMER_FIRST_NAME.Text.ToString().Trim();
				if(txtCUSTOMER_LAST_NAME.Text !="")
					objTranInfo.LastName = txtCUSTOMER_LAST_NAME.Text.ToString().Trim();
				if(txtCUSTOMER_MIDDLE_NAME.Text !="")
					objTranInfo.MiddleName = txtCUSTOMER_MIDDLE_NAME.Text.ToString().Trim();

				if(txtCUSTOMER_ADDRESS1.Text !="" || txtCUSTOMER_ADDRESS2.Text!="" )
					objTranInfo.Street = txtCUSTOMER_ADDRESS1.Text.ToString() + " " + txtCUSTOMER_ADDRESS2.Text.ToString();

				if(txtCUSTOMER_CITY.Text!="")
					objTranInfo.City = txtCUSTOMER_CITY.Text.ToString().Trim();

				if(txtCUSTOMER_ZIP.Text!="")
					objTranInfo.Zip = txtCUSTOMER_ZIP.Text.ToString().Trim();

				objTranInfo.State = Request["cmbCUSTOMER_STATE"].ToString();


				if(txtNOTE.Text!="")
				{
					objTranInfo.NOTE = txtNOTE.Text.ToString();
					//Added For Note 
					//objTranInfo.NOTE = objTranInfo.NOTE.Replace("\r\n","");
				}
				#endregion

				string PolicyNumber = txtPOLICY_NUMBER.Text;
				int CustomerID = Convert.ToInt32(hidCUSTOMER_ID.Value);
				int PolicyID = Convert.ToInt32(hidPOLICY_ID.Value);
				int PolicyVersionID = Convert.ToInt32(hidPOLICY_VERSION_ID.Value);

				PayPalResponse objResponse;
                objResponse = objCreditCardBL.ProcessCC(objTranInfo,PolicyNumber,CustomerID,PolicyID,PolicyVersionID,GetUserId());
				
				if(Convert.ToInt32(objResponse.Result) == (int)PayPalResult.Approved)
				{
					lblMessage.Text = "Transaction processed successfully. Your Confirmation ID is: "+ objResponse.PNRefrence;
					//Itrack #3837 Credit Card screen, once we process credit card transaction and it is succefully processed, then do not show Process Tranaction button. Let user start a new transaction for this.
					//Done for Itrack Issue 6099 on 14 July 2009
					//btnCCSave.Enabled = false;
					btnSave.Enabled = false;

				}
				else if (Convert.ToInt32(objResponse.Result) == (int)PayPalResult.Exception)
				{
					//lblMessage.Text = "Transaction processed successfully with PayPal but system is unable to post this deposit in BRICS at this time. Deposit will be posted in BRICS at End of Day for this policy.";
					lblMessage.Text =  "Unable to post payment in EBIX ADVANTAGE, transaction processed and voided with Credit Card Processor";
				}
				else
				{
					lblMessage.Text = "Unable to process Transaction: " + objResponse.ResponseMessage ;	
				}

				lblMessage.Visible = true;
			}
			catch(Exception ex)
			{
				lblMessage.Text = "Transaction can't be processed, following error occured: " + ex.Message ;
				lblMessage.Visible = true;
			}
		}
		#endregion
		[Ajax.AjaxMethod()]
		public string AjaxFetchZipForState(int stateID, string ZipID)
		{
			CmsWeb.webservices.ClsWebServiceCommon obj =  new CmsWeb.webservices.ClsWebServiceCommon();
			string result = "";
			result = obj.FetchZipForState(stateID,ZipID);
			return result;
			
		}
		[Ajax.AjaxMethod()]
		public string AjaxFillState(string CountryID)
		{
			CmsWeb.webservices.ClsWebServiceCommon obj =  new CmsWeb.webservices.ClsWebServiceCommon();
			string result = "";
			result = obj.FillState(CountryID);
			return result;
			
		}
	}
}
