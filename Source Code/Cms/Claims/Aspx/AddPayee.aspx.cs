/******************************************************************************************
	<Author					: - > Vijay Arora
	<Start Date				: -	> 02-06-2006
	<End Date				: - >
	<Description			: - > Class for Claims Payee Details
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
using System.Globalization; 

namespace Cms.Claims.Aspx
{
	/// <summary>
	/// 
	/// </summary>
	public class AddPayee : Cms.Claims.ClaimBase
	{
		#region Local form variables
		public int AnyPayeeAdded=0;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId;

        protected System.Web.UI.WebControls.Label capBANK_NUMBER;
        protected System.Web.UI.WebControls.Label lblBANK_NUMBER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvFIRST_NAME;
		protected System.Web.UI.WebControls.Label capFIRST_NAME;
		protected System.Web.UI.WebControls.TextBox txtFIRST_NAME;
		protected System.Web.UI.WebControls.Label capLAST_NAME;
		protected System.Web.UI.WebControls.TextBox txtLAST_NAME;
		protected System.Web.UI.WebControls.Label capAMOUNT;
		protected System.Web.UI.WebControls.Label lblCHECK_NUMBER;
        protected System.Web.UI.WebControls.Label lblCHECK_STATUS;
        protected System.Web.UI.WebControls.Label capCHECK_STATUS;
		protected System.Web.UI.WebControls.TextBox txtAMOUNT;
	
		protected System.Web.UI.WebControls.RegularExpressionValidator revAMOUNT;
		protected System.Web.UI.WebControls.Label capACCOUNT_NUMBER;
		protected System.Web.UI.WebControls.Label lblACCOUNT_NUMBER;
		protected System.Web.UI.WebControls.Label capACCOUNT_NAME;
		protected System.Web.UI.WebControls.Label lblACCOUNT_NAME;
		protected System.Web.UI.WebControls.Label lblDelete;
        protected System.Web.UI.WebControls.Label lblBANK_BRANCH;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capPAYEE;
		protected System.Web.UI.WebControls.Label capPAYEE_ADDRESS;
//		protected System.Web.UI.WebControls.Label lblREFERENCE;
        protected System.Web.UI.WebControls.Label capPAYMENT_METHOD;
        protected System.Web.UI.WebControls.DropDownList cmbPAYMENT_METHOD;
    	protected System.Web.UI.WebControls.RequiredFieldValidator rfvPAYMENT_METHOD;		
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPAYEE;		
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAMOUNT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOUNTRY;
		protected System.Web.UI.WebControls.Label capADDRESS1;
		protected System.Web.UI.WebControls.TextBox txtADDRESS1;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvADDRESS1;
		protected System.Web.UI.WebControls.Label capADDRESS2;
		protected System.Web.UI.WebControls.TextBox txtADDRESS2;
		protected System.Web.UI.WebControls.Label capCITY;
		protected System.Web.UI.WebControls.TextBox txtCITY;
        protected System.Web.UI.WebControls.Label capBANK_BRANCH; // Added by Santosh kumar Gautam 16 Nov 2010
        protected System.Web.UI.WebControls.TextBox txtINVOICE_DUE_DATE;  // Added by Santosh kumar Gautam 16 Nov 2010
        protected System.Web.UI.WebControls.HyperLink hlkINVOICE_DUE_DATE;  // Added by Santosh kumar Gautam 16 Nov 2010
        protected System.Web.UI.WebControls.Image imgINVOICE_DATE;  // Added by Santosh kumar Gautam 16 Nov 2010
        protected System.Web.UI.WebControls.TextBox txtINVOICE_DATE;  // Added by Santosh kumar Gautam 16 Nov 2010
        protected System.Web.UI.WebControls.HyperLink hlkINVOICE_DATE;  // Added by Santosh kumar Gautam 16 Nov 2010
        protected System.Web.UI.WebControls.Image imgINVOICE_DUE_DATE;  // Added by Santosh kumar Gautam 16 Nov 2010
        protected System.Web.UI.WebControls.RegularExpressionValidator revINVOICE_DUE_DATE;  // Added by Santosh kumar Gautam 16 Nov 2010
        protected System.Web.UI.WebControls.RegularExpressionValidator revINVOICE_DATE;  // Added by Santosh kumar Gautam 16 Nov 2010

        protected System.Web.UI.WebControls.Label capINVOICE_DUE_DATE;
        protected System.Web.UI.WebControls.Label capINVOICE_DATE;
        protected System.Web.UI.WebControls.Label capINVOICE_NUMBER;
        protected System.Web.UI.WebControls.Label capINVOICE_SERIAL_NUMBER;
        protected System.Web.UI.WebControls.Label capHeader;

        protected System.Web.UI.WebControls.TextBox txtINVOICE_NUMBER;
        protected System.Web.UI.WebControls.TextBox txtINVOICE_SERIAL_NUMBER;
      

        
		protected System.Web.UI.WebControls.Label capCOUNTRY;
        protected System.Web.UI.WebControls.Label lblPAYEE_TYPE;
        protected System.Web.UI.WebControls.Label capPAYEE_TYPE;

		protected System.Web.UI.WebControls.DropDownList cmbCOUNTRY;
		protected System.Web.UI.WebControls.Label capSTATE;
		protected System.Web.UI.WebControls.DropDownList cmbSTATE;
		//protected System.Web.UI.WebControls.ListBox cmbPAYEE;
		protected System.Web.UI.HtmlControls.HtmlSelect cmbPAYEE; 
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSTATE;
		protected System.Web.UI.WebControls.Label capZIP;
		protected System.Web.UI.WebControls.TextBox txtZIP;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvZIP;
		protected System.Web.UI.WebControls.RegularExpressionValidator revZIP;
		protected System.Web.UI.WebControls.Label capNARRATIVE;
		protected System.Web.UI.WebControls.TextBox txtNARRATIVE;		
		protected System.Web.UI.WebControls.CustomValidator csvNARRATIVE;
		protected System.Web.UI.WebControls.Label capTO_ORDER_DESC;
		protected System.Web.UI.WebControls.TextBox txtTO_ORDER_DESC;		
		//protected System.Web.UI.WebControls.CustomValidator csvTO_ORDER_DESC;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPAYEE_ADDRESS;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidACTIVITY_REASON;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_PAYMENT_EXPENSE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDefaultValues;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPAYEE_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidACTIVITY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidEXPENSE_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPARTY_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPAYEE_HAS_BANK_INFO;

        protected System.Web.UI.WebControls.RequiredFieldValidator rfvINVOICE_NUMBER;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvINVOICE_SERIAL_NUMBER;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvINVOICE_DATE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPAYEE_ADDRESS;
        
        protected System.Web.UI.WebControls.RegularExpressionValidator revINVOICE_SERIAL_NUMBER;

        protected System.Web.UI.WebControls.Label capREIN_RECOVERY_NUMBER;
        protected System.Web.UI.WebControls.TextBox txtREIN_RECOVERY_NUMBER;

        protected System.Web.UI.WebControls.Label capCHECK_NUMBER;
        protected System.Web.UI.WebControls.Label capRECOVERY_TYPE;
        protected System.Web.UI.WebControls.DropDownList cmbRECOVERY_TYPE;
        
        protected System.Web.UI.WebControls.CustomValidator csvPAYMENT_METHOD;

        protected System.Web.UI.HtmlControls.HtmlTableRow TrRecoveryType;
        
		protected System.Web.UI.WebControls.Label lblPAYEE_PARTY_ID;//lblPAYEE ---- Changed to show the Primary in Trasaction Log - Done for Itrack Issue 6932 on 10 Feb 2010
		//protected System.Web.UI.WebControls.Label capADDITIONAL_PAYEE;
		//protected System.Web.UI.WebControls.DropDownList cmbADDITIONAL_PAYEE;
		//protected System.Web.UI.WebControls.Label capSECONDARY_PARTY_ID;
		//protected System.Web.UI.WebControls.DropDownList cmbSECONDARY_PARTY_ID;
		//protected System.Web.UI.HtmlControls.HtmlTableRow trAdditionalPayee;
		//protected System.Web.UI.HtmlControls.HtmlTableRow trSecondaryParty;
		ClsPayee objPayee = new ClsPayee();
		//protected System.Web.UI.WebControls.Label capPAYEE_ADDRESS;
		protected System.Web.UI.WebControls.DropDownList cmbPAYEE_ADDRESS;
//		private char LOB_DELIMITER = ',';
		private char LOB_DELIMITER = '^';
		string[] arryPayeeID;
        string Account_Number;
		#endregion

        public NumberFormatInfo nfi;

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{						
			
			base.ScreenId="309_1_0_1_0_1_0";
			
			lblMessage.Visible = false;

            if (GetPolicyCurrency() != String.Empty && GetPolicyCurrency() == enumCurrencyId.BR)
                nfi = new CultureInfo(enumCulture.BR, true).NumberFormat;
            else
                nfi = new CultureInfo(enumCulture.US, true).NumberFormat;
            

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass		=	CmsButtonType.Write;
			btnReset.PermissionString	=	gstrSecurityXML;
			
			btnSave.CmsButtonClass		=	CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;
			
			btnSave.Attributes.Add("onclick","javascript:return CheckPartiesForSPH();");
            hlkINVOICE_DUE_DATE.Attributes.Add("OnClick", "fPopCalendar(txtINVOICE_DUE_DATE,txtINVOICE_DUE_DATE)");
            hlkINVOICE_DATE.Attributes.Add("OnClick", "fPopCalendar(txtINVOICE_DATE,txtINVOICE_DATE)");
          
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.AddPayee" ,System.Reflection.Assembly.GetExecutingAssembly());

			//this.cmbCOUNTRY.SelectedIndex = int.Parse(aCountry);

			if (Request.Form["__EVENTTARGET"] == "cmbPAYEE_ADDRESS_Change")
			{
				cmbPAYEE_ADDRESS_SelectedIndexChanged(sender,e);
				return;
			}
			
          

			if(!Page.IsPostBack)
			{		
				GetQueryStringValues();		
		
                //===============================================================
                // MODIFIED BY SANTOSH KR GAUTAM ON 19 JUL 2011 (REF ITRACK:1029)
                //===============================================================
                //if(hidPAYEE_ID.Value=="") //Check for whether any payee has already been added 
                //{
                //    ClsPayee objPayee = new ClsPayee();
                //    AnyPayeeAdded = objPayee.AnyPayeeForClaim(hidCLAIM_ID.Value,hidACTIVITY_ID.Value);
                //    //if(AnyPayeeAdded>0)
                //    //{
                //    //    lblDelete.Visible = true;
                //    //    lblDelete.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("946");
                //    //    trBody.Attributes.Add("style","display:none");
                //    //    hidFormSaved.Value = "5";
                //    //    return;
                //    //}
                    
                //}
                string strClaimStatus = GetClaimStatus();
                //if(strClaimStatus == CLAIM_STATUS_CLOSED)
                //    btnSave.Visible=false;
                //else
                //    btnSave.Visible=true;
                string ClaimStatus = "";
                double AmountToRecover = 0;


                DataSet ds = ClsActivity.GetCalimStatus(hidCLAIM_ID.Value);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ClaimStatus = ds.Tables[0].Rows[0]["CLAIM_STATUS"].ToString();
                    AmountToRecover = double.Parse(ds.Tables[0].Rows[0]["AMOUNT_TO_RECOVER"].ToString());

                }
                //----------------------------------------------------------------------------------------
                // WHEN CLAIM IS CLOSED AND AMOUNT TO RECOVER IS PENDING            
                //----------------------------------------------------------------------------------------
                if (ClaimStatus == CLAIM_STATUS_CLOSED)
                {
                    if (AmountToRecover != 0)                   
                        btnSave.Visible = true;
                    else
                        btnSave.Visible = false;
                   
                }
                else
                {
                    btnSave.Visible = true;
                }		

                int ActivityReason = int.Parse(hidACTIVITY_REASON.Value);

                if (ActivityReason == (int)enumActivityReason.RECOVERY)              
                    TrRecoveryType.Visible = true; 
                else                
                    TrRecoveryType.Visible = false;
               
              
				//cmbADDITIONAL_PAYEE.Attributes.Add("onChange","ShowHideSecondaryPayee();");
//				cmbPAYMENT_METHOD.Attributes.Add("onChange","ShowHideAddressDetails();");
				btnReset.Attributes.Add("onclick","javascript:return formReset();");
				//cmbPAYEE.Attributes.Add("onChange","javascript:return cmbPAYEE_Change();");
				cmbPAYEE_ADDRESS.Attributes.Add("onChange","javascript:return cmbPAYEE_ADDRESS_Change();");
               
                txtAMOUNT.Attributes.Add("onblur", "javascript:this.value=formatAmount(this.value);");
               // txtAMOUNT.Attributes.Add("onBlur","javascript: this.value=formatCurrencyWithCents(this.value);");
				FillDropDowns();
				//Added by Sibin for Itrack Issue 5361 on 28 Jan 09
                BindDropDown(cmbPAYEE);
				SetCaptions();
				SetErrorMessages();	
				SetPayeeDetails();
                LoadData();

				if(hidPAYEE_ID.Value!="")
				{				
					GetOldDataXML();
				}

			}
			
		}
		#endregion

	

		private void GetQueryStringValues()
		{
			if(Request["CLAIM_ID"]!=null && Request["CLAIM_ID"].ToString()!="")
			{
				hidCLAIM_ID.Value = Request["CLAIM_ID"].ToString();
				
								
			}
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
				hidEXPENSE_ID.Value = "0";

            if (Request["ACTIVITY_REASON"] != null && Request["ACTIVITY_REASON"].ToString() != "")
                hidACTIVITY_REASON.Value = Request["ACTIVITY_REASON"].ToString();
			else
                hidACTIVITY_REASON.Value = "";		

            if (Request["IS_PAYMENT_EXPENSE"] != null && Request["IS_PAYMENT_EXPENSE"].ToString() != "")
                hidIS_PAYMENT_EXPENSE.Value = Request["IS_PAYMENT_EXPENSE"].ToString();
			else
                hidIS_PAYMENT_EXPENSE.Value = "";	

            
            
		}

	
		#region GetOldDataXML
		private void GetOldDataXML()
		{
			if (hidCLAIM_ID.Value != "" && hidPAYEE_ID.Value != "")
			{
				hidOldData.Value	=	objPayee.GetXmlForPageControls(hidCLAIM_ID.Value,hidACTIVITY_ID.Value,hidPAYEE_ID.Value,int.Parse(GetLanguageID()));
				//Added For Itrack Issue #5199.
				hidPAYEE_ADDRESS.Value = ClsCommon.FetchValueFromXML("PARTY_ID",hidOldData.Value);
           	}
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
			//this.cmbPAYEE_ADDRESS.SelectedIndexChanged += new System.EventHandler(this.cmbPAYEE_ADDRESS_SelectedIndexChanged);
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
            Cms.CmsWeb.ClsMessages.SetCustomizedXml(GetLanguageCode());
            
//			rfvPAYMENT_METHOD.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("786");
            revINVOICE_DUE_DATE.ValidationExpression = aRegExpDate;
            revINVOICE_DATE.ValidationExpression = aRegExpDate;
            //Change by Shikha 
            revINVOICE_SERIAL_NUMBER.ValidationExpression = aRegExpAlphaNumStrict;
          
            revINVOICE_SERIAL_NUMBER.ErrorMessage= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "7");
            revINVOICE_DUE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
            revINVOICE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
         

			rfvADDRESS1.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("787");
			rfvSTATE.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("788");
			rfvZIP.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("789");			
			csvNARRATIVE.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("791");
			//csvTO_ORDER_DESC.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("985");
			revZIP.ValidationExpression = aRegExpZip; 
			revZIP.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1084");
			rfvCOUNTRY.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("33");
			
			rfvAMOUNT.ErrorMessage				  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("335");
			rfvPAYEE.ErrorMessage				  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("821");
			rfvFIRST_NAME.ErrorMessage				  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("57");

            rfvPAYMENT_METHOD.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("786");
            capHeader.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");

			//revAMOUNT.ValidationExpression = aRegExpCurrencyformat;//Done by Sibin on 2 Feb 09 for Itrack Issue 5385
			//Done for Itrack Issue 6516 on 14 Oct 09
			//revAMOUNT.ValidationExpression = aRegExpDoublePositiveZero;aRegExpDoublePositiveNonZero
            revAMOUNT.ValidationExpression = aRegExpCurrencyformat;
			revAMOUNT.ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");//Done by Sibin on 2 Feb 09 for Itrack Issue 5385
            csvPAYMENT_METHOD.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "6"); 
            rfvPAYEE_ADDRESS.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "4"); 
            if (hidIS_PAYMENT_EXPENSE.Value == "Y")
            {
                rfvINVOICE_DATE.Enabled = true;
                rfvINVOICE_NUMBER.Enabled = true;
                rfvINVOICE_SERIAL_NUMBER.Enabled = true;

                rfvINVOICE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "4"); 
                rfvINVOICE_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1");
                rfvINVOICE_SERIAL_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "2");
            }
            else
            {
                rfvINVOICE_DATE.Enabled = false;
                rfvINVOICE_NUMBER.Enabled = false;
                rfvINVOICE_SERIAL_NUMBER.Enabled = false;
            }

    
		}
		#endregion

		#region FillDropDowns
		private void FillDropDowns()
		{
            cmbRECOVERY_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("XRTYPE");  //Action on Payment Lookup
            cmbRECOVERY_TYPE.DataTextField = "LookupDesc";
            cmbRECOVERY_TYPE.DataValueField = "LookupID";
            cmbRECOVERY_TYPE.DataBind();

            cmbPAYMENT_METHOD.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("PYMTD");  //Action on Payment Lookup
            cmbPAYMENT_METHOD.DataTextField = "LookupDesc";
            cmbPAYMENT_METHOD.DataValueField = "LookupID";
            cmbPAYMENT_METHOD.DataBind();

           

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
            cmbCOUNTRY.SelectedValue = "5";
			//cmbCOUNTRY.Items.Insert(0,new ListItem("",""));		
	
			/*cmbADDITIONAL_PAYEE.Items.Add(new ListItem("No","0"));
			cmbADDITIONAL_PAYEE.Items.Add(new ListItem("Yes","1"));
			cmbADDITIONAL_PAYEE.SelectedIndex=0;*/
		}
		#endregion

		#region SetCaptions
		private void SetCaptions()
		{
            int ActivityReason=int.Parse( hidACTIVITY_REASON.Value);


            if (ActivityReason == (int)enumActivityReason.RECOVERY)
            {
                capPAYMENT_METHOD.Text = objResourceMgr.GetString("RECOVERY_METHOD");
                //capHeader.Text = capPAYMENT_METHOD.Text;
                capREIN_RECOVERY_NUMBER.Text = objResourceMgr.GetString("txtREIN_RECOVERY_NUMBER");
                capAMOUNT.Text = objResourceMgr.GetString("txtAMOUNT_RECOVERY");
                
            }
            else
            {
                capREIN_RECOVERY_NUMBER.Visible = false;
                 txtREIN_RECOVERY_NUMBER.Visible = false;
                 capAMOUNT.Text = objResourceMgr.GetString("txtAMOUNT_PAYMENT");

                 capPAYMENT_METHOD.Text = objResourceMgr.GetString("PAYMENT_METHOD");
                 capHeader.Text = capPAYMENT_METHOD.Text;
            }
           
			capPAYEE.Text			=		objResourceMgr.GetString("lblPAYEE_PARTY_ID");//lblPAYEE ---- Changed to show the Primary in Trasaction Log -Done for Itrack Issue 6932 on 10 Feb 2010
//			capREFERENCE.Text		=		objResourceMgr.GetString("lblREFERENCE");
        	
			capADDRESS1.Text		=		objResourceMgr.GetString("txtADDRESS1");
			capADDRESS2.Text		=		objResourceMgr.GetString("txtADDRESS2");
			capCITY.Text 			=		objResourceMgr.GetString("txtCITY");
			capSTATE.Text 			=		objResourceMgr.GetString("cmbSTATE");
			capCOUNTRY.Text 		=		objResourceMgr.GetString("cmbCOUNTRY");
			capZIP.Text 			=		objResourceMgr.GetString("txtZIP");
			capNARRATIVE.Text 		=		objResourceMgr.GetString("txtNARRATIVE");
			capTO_ORDER_DESC.Text 		=		objResourceMgr.GetString("txtTO_ORDER_DESC");
            capBANK_NUMBER.Text = objResourceMgr.GetString("lblBANK_NUMBER");
            capBANK_BRANCH.Text = objResourceMgr.GetString("lblBANK_BRANCH"); // Added by Santosh Kumar Gautam on 16 Nov 2010
            capINVOICE_DUE_DATE.Text =      objResourceMgr.GetString("txtINVOICE_DUE_DATE"); // Added by Santosh Kumar Gautam on 16 Nov 2010
			capACCOUNT_NUMBER.Text  =		objResourceMgr.GetString("lblACCOUNT_NUMBER");
			capACCOUNT_NAME.Text 	=		objResourceMgr.GetString("lblACCOUNT_NAME");
			
            capFIRST_NAME.Text = objResourceMgr.GetString("txtFIRST_NAME");
            capLAST_NAME.Text = objResourceMgr.GetString("txtLAST_NAME");
            capINVOICE_NUMBER.Text = objResourceMgr.GetString("txtINVOICE_NUMBER");
            capINVOICE_SERIAL_NUMBER.Text = objResourceMgr.GetString("txtINVOICE_SERIAL_NUMBER");
            capINVOICE_DATE.Text = objResourceMgr.GetString("txtINVOICE_DATE");
            capPAYEE_TYPE.Text  =objResourceMgr.GetString("lblPAYEE_TYPE");
            capPAYEE_ADDRESS.Text = objResourceMgr.GetString("capPAYEE_ADDRESS");
            capCHECK_STATUS.Text = objResourceMgr.GetString("lblCHECK_STATUS");
            capCHECK_NUMBER.Text = objResourceMgr.GetString("lblCHECK_NUMBER");
			//capADDITIONAL_PAYEE.Text 	=		objResourceMgr.GetString("cmbADDITIONAL_PAYEE");
            capRECOVERY_TYPE.Text = objResourceMgr.GetString("cmbRECOVERY_TYPE");
         
		}

		#endregion

		#region GetFormValue
        private ClsPayeeInfo GetFormValue()
        {
            ClsPayeeInfo objPInfo = new ClsPayeeInfo();

            if (hidCLAIM_ID.Value != "")
                objPInfo.CLAIM_ID = int.Parse(hidCLAIM_ID.Value);

            if (hidPAYEE_ID.Value != "")
                objPInfo.PAYEE_ID = int.Parse(hidPAYEE_ID.Value);



            objPInfo.PAYMENT_METHOD = int.Parse(cmbPAYMENT_METHOD.SelectedValue);
            /*Lookup values for Payment Method
                11787>Check
                11788>EFT*/
            //			if (cmbPAYMENT_METHOD.SelectedValue == "11787")
            //			{
            objPInfo.ADDRESS1 = txtADDRESS1.Text;
            objPInfo.ADDRESS2 = txtADDRESS2.Text;
            objPInfo.CITY = txtCITY.Text;
            objPInfo.ZIP = txtZIP.Text;
            if (cmbSTATE.SelectedItem != null && cmbSTATE.SelectedItem.Value != "")
                objPInfo.STATE = int.Parse(cmbSTATE.SelectedValue);
            if (cmbCOUNTRY.SelectedItem != null && cmbCOUNTRY.SelectedItem.Value != "")
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

            if (!string.IsNullOrEmpty(txtINVOICE_NUMBER.Text))
                objPInfo.INVOICE_NUMBER = txtINVOICE_NUMBER.Text;

            if (!string.IsNullOrEmpty(txtINVOICE_SERIAL_NUMBER.Text))
                objPInfo.INVOICE_SERIAL_NUMBER = txtINVOICE_SERIAL_NUMBER.Text;

            if (!string.IsNullOrEmpty(txtINVOICE_DATE.Text))
                objPInfo.INVOICE_DATE = ConvertToDate(txtINVOICE_DATE.Text);

            objPInfo.NARRATIVE = txtNARRATIVE.Text;
            objPInfo.TO_ORDER_DESC = txtTO_ORDER_DESC.Text.Trim();
            if (txtAMOUNT.Text != "")
                objPInfo.AMOUNT = Convert.ToDouble(txtAMOUNT.Text.Trim(), nfi);

           

            //Added by Santosh Kumar Gautam on 16 Nov 2010
            if (!string.IsNullOrEmpty(txtINVOICE_DUE_DATE.Text))
                objPInfo.INVOICE_DUE_DATE = ConvertToDate(txtINVOICE_DUE_DATE.Text.Trim());

            //objPInfo.PAYEE_ACTIVITY_ID = int.Parse(hidACTIVITY_ID.Value);
           // if (hidCALLED_FROM.Value == CALLED_FROM_EXPENSE)
            {
                //objPInfo.PARTY_ID = hidPARTY_ID.Value;
                arryPayeeID = cmbPAYEE.Value.Split('^');
                //objPInfo.PARTY_ID = cmbPAYEE.SelectedValue.ToString().Trim();
                objPInfo.PARTY_ID = arryPayeeID[0];

            }
           // else
            {

                //if(cmbPAYEE.SelectedItem!=null && cmbPAYEE.SelectedItem.Value!="")
                if (cmbPAYEE != null && cmbPAYEE.Value != "")
                {

                   // objPInfo.PARTY_ID = GetPayee(LOB_DELIMITER);
                  if(hidPAYEE_ADDRESS.Value!="")   // && hidPAYEE_ADDRESS.Value!="-1" 
                    {
                        //objPInfo.PARTY_ID = hidPAYEE_ADDRESS.Value;
                        arryPayeeID = cmbPAYEE_ADDRESS.SelectedValue.Split('^');
                        objPInfo.PARTY_ID = arryPayeeID[0];
                        //objPInfo.PARTY_ID = cmbPAYEE_ADDRESS.SelectedValue.ToString().Trim();

                    }
                    objPInfo.SECONDARY_PARTY_ID = Cms.BusinessLayer.BlCommon.ClsCommon.GetDelimitedTextValuesFromListbox(cmbPAYEE, LOB_DELIMITER);
                    //Added For Itrack 5124 
                    objPInfo.PAYEE_PARTY_ID = Cms.BusinessLayer.BlCommon.ClsCommon.GetDelimitedFromListbox(cmbPAYEE, LOB_DELIMITER);
                }
                //if(cmbADDITIONAL_PAYEE.SelectedItem!=null && cmbADDITIONAL_PAYEE.SelectedItem.Value=="1" && cmbSECONDARY_PARTY_ID.SelectedItem!=null && cmbSECONDARY_PARTY_ID.SelectedItem.Value!="")
                //	objPInfo.SECONDARY_PARTY_ID = int.Parse(cmbSECONDARY_PARTY_ID.SelectedItem.Value);
            }
            if (objPInfo.PARTY_ID == "" || objPInfo.PARTY_ID == "0" || objPInfo.PARTY_ID == "-1")
            {
                objPInfo.FIRST_NAME = txtFIRST_NAME.Text.Trim();
                objPInfo.LAST_NAME = txtLAST_NAME.Text.Trim();
            }
            //Dummy value for INVOICE_DATE
         //   objPInfo.INVOICE_DATE = DateTime.Now;
            objPInfo.ACTIVITY_ID = int.Parse(hidACTIVITY_ID.Value);
            objPInfo.EXPENSE_ID = int.Parse(hidEXPENSE_ID.Value);

            if (hidPAYEE_ID.Value == "")
                strRowId = "NEW";
            else
            {
                strRowId = hidPAYEE_ID.Value;
                objPInfo.PAYEE_ID = int.Parse(hidPAYEE_ID.Value);
            }

              int ActivityReason=int.Parse( hidACTIVITY_REASON.Value);


              if (ActivityReason == (int)enumActivityReason.RECOVERY)
              {
                  objPInfo.REIN_RECOVERY_NUMBER = txtREIN_RECOVERY_NUMBER.Text.Trim();
                  objPInfo.RECOVERY_TYPE = int.Parse(cmbRECOVERY_TYPE.SelectedValue);
              }
            

            return objPInfo;
        }
     
		#endregion		


		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{   
				//Added For iTrack Issue #5857..
			 	if(CheckSpecialHandling() == -1)
				{
					lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1050");   
					lblMessage.Visible	=	true;
				}
				else
				{
 
                  int intRetVal;
				
					
					ClsPayeeInfo objPInfo = GetFormValue();

					if(strRowId.ToUpper().Equals("NEW")) //save case
					{
						objPInfo.CREATED_BY = int.Parse(GetUserId());
						objPInfo.CREATED_DATETIME = DateTime.Now;
					
						//Calling the add method of business layer class
						intRetVal = objPayee.Add(objPInfo,int.Parse(hidACTIVITY_REASON.Value));

						if(intRetVal>0)
						{
                            // NOT THIS FUNCTION TI COPY CO INSURANCE AND RE INSURANCE DATA INTO [CLM_ACTIVITY_CO_RI_BREAKDOWN] TABLE
                            // AND USED TO UPDATE CLM_ACTIVITY_RESERVE TABLES COLUMNS(RI_RESERVE, RI_RESERVE_TRAN, CO_RESERVE, CO_RESERVE_TRAN
                           int ActivityReason=int.Parse( hidACTIVITY_REASON.Value);

                           if (ActivityReason == (int)enumActivityReason.RECOVERY)
                           {
                               ClsAddReserveDetails ObjReserve = new ClsAddReserveDetails();
                               ObjReserve.CalculateBreakdown(int.Parse(hidCLAIM_ID.Value), int.Parse(hidACTIVITY_ID.Value));
                           }
                                                       
							hidPAYEE_ID.Value = objPInfo.PAYEE_ID.ToString();
							lblMessage.Text				=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"29");
							hidFormSaved.Value			=	"1";
							hidIS_ACTIVE.Value			= "Y";
							BindDropDown(cmbPAYEE);
							LoadData();
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
                        intRetVal = objPayee.Update(objOldPayeeInfo, objPInfo, int.Parse(hidACTIVITY_REASON.Value));					
						if( intRetVal > 0 )			// update successfully performed
						{
                            // NOT THIS FUNCTION TI COPY CO INSURANCE AND RE INSURANCE DATA INTO [CLM_ACTIVITY_CO_RI_BREAKDOWN] TABLE
                            // AND USED TO UPDATE CLM_ACTIVITY_RESERVE TABLES COLUMNS(RI_RESERVE, RI_RESERVE_TRAN, CO_RESERVE, CO_RESERVE_TRAN
                            int ActivityReason = int.Parse(hidACTIVITY_REASON.Value);

                            if (ActivityReason == (int)enumActivityReason.RECOVERY)
                            {
                                ClsAddReserveDetails ObjReserve = new ClsAddReserveDetails();
                                ObjReserve.CalculateBreakdown(int.Parse(hidCLAIM_ID.Value), int.Parse(hidACTIVITY_ID.Value));
                            }

							lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
							hidFormSaved.Value		=	"1";
							BindDropDown(cmbPAYEE);
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
			if (hidCLAIM_ID.Value != "")
			{
                //===============================================================
                // MODIFIED BY SANTOSH KR GAUTAM ON 19 JUL 2011 (REF ITRACK:1029)
                //===============================================================
				DataSet dsTemp =  objPayee.GetValuesForPageControls(hidCLAIM_ID.Value,hidACTIVITY_ID.Value,   int.Parse(GetLanguageID()));
				if (dsTemp != null && dsTemp.Tables[0].Rows.Count > 0)
				{
					DataRow dr = dsTemp.Tables[0].Rows[0];
                   
                    hidPAYEE_ID.Value=  dr["PAYEE_ID"].ToString();
                    string RecoveryType = dr["RECOVERY_TYPE"].ToString();
                    if (int.Parse(hidACTIVITY_REASON.Value)==(int)enumActivityReason.RECOVERY && RecoveryType != "" && RecoveryType != "0")
                    {
                        cmbRECOVERY_TYPE.SelectedValue = RecoveryType;
                        BindDropDown(cmbPAYEE);
                    }



					//lblPAYEE.Text = dr["NAME"].ToString();
					//lblREFERENCE.Text = dr["REFERENCE"].ToString();
                   cmbPAYMENT_METHOD.SelectedValue = dr["PAYMENT_METHOD"].ToString();					
                    string ACTIVITY_STATUS = dr["ACTIVITY_STATUS"].ToString();
                    if (ACTIVITY_STATUS == "11801")
                    {
                        btnSave.Visible = false;
                        btnReset.Visible = false;
                    }

					string strAddress = dr["PARTY_ID"].ToString();
					if( strAddress!=null && strAddress != "" && strAddress != "0")
					{
                        
						Cms.BusinessLayer.BlCommon.ClsCommon.SelectTextValuesAtCombobox(cmbPAYEE_ADDRESS,strAddress,LOB_DELIMITER);   
						//cmbPAYEE_ADDRESS.SelectTextValuesAtCombobox=dr["PARTY_ID"].ToString() ;   
                        txtADDRESS1.Enabled=false;
                        txtADDRESS2.Enabled = false;
                        txtCITY.Enabled = false;
                        txtZIP.Enabled = false;
                        cmbSTATE.Enabled = false;
                        cmbCOUNTRY.Enabled = false;
					}
               
               
					txtADDRESS1.Text = dr["ADDRESS1"].ToString();
					txtADDRESS2.Text = dr["ADDRESS2"].ToString();
                    lblBANK_NUMBER.Text = dr["BANK_NUMBER"].ToString();
                    lblBANK_BRANCH.Text = dr["BANK_BRANCH"].ToString(); // Added by Santosh Kumar Gautam on 16 Nov 2010

                   

                    if (dr["INVOICE_DUE_DATE"] != null && dr["INVOICE_DUE_DATE"].ToString() != "")
                        txtINVOICE_DUE_DATE.Text = ConvertToDateCulture(Convert.ToDateTime((dr["INVOICE_DUE_DATE"].ToString().Trim())));  // Added by Santosh Kumar Gautam on 16 Nov 2010
                    
                    if (dr["INVOICE_DATE"] != null && dr["INVOICE_DATE"].ToString() != "")
                        txtINVOICE_DATE.Text = ConvertToDateCulture(Convert.ToDateTime((dr["INVOICE_DATE"].ToString().Trim())));

                    if(dr["ACCOUNT_NUMBER"].ToString()!="")
                      hidPAYEE_HAS_BANK_INFO.Value = "Y";
                    else
                        hidPAYEE_HAS_BANK_INFO.Value = "N"; 
					//lblACCOUNT_NUMBER.Text = dr["ACCOUNT_NUMBER"].ToString();
                    Account_Number = dr["ACCOUNT_NUMBER"].ToString();
                    if (Account_Number != null && Account_Number != "")
                    {
                        lblACCOUNT_NUMBER.Text = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(Account_Number);
                    }
					lblACCOUNT_NAME.Text = dr["ACCOUNT_NAME"].ToString();
					txtCITY.Text = dr["CITY"].ToString();
					txtFIRST_NAME.Text = dr["FIRST_NAME"].ToString();
					txtLAST_NAME.Text = dr["LAST_NAME"].ToString();
					if(dr["AMOUNT"]!=null && dr["AMOUNT"].ToString()!="")
                        txtAMOUNT.Text = Convert.ToDouble(dr["AMOUNT"].ToString()).ToString("N", nfi);
                           
						//txtAMOUNT.Text=String.Format("{0:,#,###}",Convert.ToInt64(dr["AMOUNT"]));
					
					//Added by Asfa (24-Mar-2008) - iTrack issue #3936
					if(dr["CHECK_NUMBER"]!=null && dr["CHECK_NUMBER"].ToString()!="" && dr["CHECK_NUMBER"].ToString()!="0")
						lblCHECK_NUMBER.Text =dr["CHECK_NUMBER"].ToString();
					if(dr["STATUS"]!=null && dr["STATUS"].ToString()!="")
						lblCHECK_STATUS.Text =dr["STATUS"].ToString();

                    if (dr["TYPE"] != null && dr["TYPE"].ToString() != "")
                        lblPAYEE_TYPE.Text = dr["TYPE"].ToString();

                    txtINVOICE_NUMBER.Text =dr["INVOICE_NUMBER"].ToString();
                    txtINVOICE_SERIAL_NUMBER.Text = dr["INVOICE_SERIAL_NUMBER"].ToString();

                  

					/*Lookup values for Payment Method
					11787>Check
					11788>EFT*/
//					if (cmbPAYMENT_METHOD.SelectedValue == "11787")
//					{
						if(dr["STATE"]!=null && dr["STATE"].ToString()!="" && dr["STATE"].ToString()!="0")
							cmbSTATE.SelectedValue =  dr["STATE"].ToString();
                        if (dr["COUNTRY"] != null && dr["COUNTRY"].ToString() != "" && dr["COUNTRY"].ToString() != "0")
                            cmbCOUNTRY.SelectedValue = dr["COUNTRY"].ToString();
//					}
//					else
//					{
//						cmbSTATE.SelectedIndex =  0;
//						//cmbCOUNTRY.SelectedIndex = 0;
//					}
					//if(hidCALLED_FROM.Value == CALLED_FROM_PAYMENT)
					{
						//if(dr["PARTY_ID"]!=null && dr["PARTY_ID"].ToString()!="")
						//	cmbPAYEE.SelectedValue = dr["PARTY_ID"].ToString();
						//string strPAYEE = dr["PARTY_ID"].ToString();
						//string strPAYEE = dr["SECONDARY_PARTY_ID"].ToString();
						//Added For Itrack 5124
						
						string strPAYEE = dr["PAYEE_PARTY_ID"].ToString();
						if(strPAYEE!="" && strPAYEE!="0")
						{
							//Cms.BusinessLayer.BlCommon.ClsCommon.SelectTextValuesAtListbox(cmbPAYEE,strPAYEE,LOB_DELIMITER);
						    Cms.BusinessLayer.BlCommon.ClsCommon.SelectTextAtListbox(cmbPAYEE,strPAYEE,LOB_DELIMITER);
							//Cms.BusinessLayer.BlCommon.ClsCommon.SelectTextValuesAtListbox(cmbPAYEE,strPAYEE,LOB_DELIMITER);

								
						
						}


						/*if(dr["SECONDARY_PARTY_ID"]!=null && dr["SECONDARY_PARTY_ID"].ToString()!="" && dr["SECONDARY_PARTY_ID"].ToString()!="0")
						{
							cmbSECONDARY_PARTY_ID.SelectedValue = dr["SECONDARY_PARTY_ID"].ToString();
							cmbADDITIONAL_PAYEE.SelectedIndex=1;
						}*/

					}
					/*else
					{
						trAdditionalPayee.Attributes.Add("style","display:none");
						trSecondaryParty.Attributes.Add("style","display:none");
					}*/
					txtZIP.Text = dr["ZIP"].ToString();
					txtNARRATIVE.Text = dr["NARRATIVE"].ToString();
					txtTO_ORDER_DESC.Text = dr["TO_ORDER_DESC"].ToString();
                    txtREIN_RECOVERY_NUMBER.Text = dr["REIN_RECOVERY_NUMBER"].ToString();
					//if(dr["PARTY_ID"].ToString()!="" && dr["PARTY_ID"].ToString()!="0")
                    //if(dr["PARTY_ID"].ToString() == "-1")
                    //{
                    //    //trName.Attributes.Add("style","display:inline");						
                    //    rfvFIRST_NAME.Enabled = true;
                    //}
                    //else if(dr["PARTY_ID"].ToString()!="" && dr["PARTY_ID"].ToString()!="0") 
                    //{
                    //    //trName.Attributes.Add("style","display:none");						
                    //    rfvFIRST_NAME.Enabled = false;
                    //}
				}
			}
		}

		#endregion

        //private string GetPayee(char Delimeter)
        //{
        //    string strPAYEE = "";
        //    if (cmbPAYEE != null && cmbPAYEE.Items.Count > 0)
        //    {
        //        foreach (ListItem li in cmbPAYEE.Items)
        //        {
        //            if (li.Selected)
        //            {
                        
        //                strPAYEE += li.Value.Split(Delimeter)[0].ToString();
        //            }
        //        }
        //        //if (strPAYEE.Length > 0)
        //        //    strPAYEE = strPAYEE.Substring(0, (strPAYEE.Length - 1));
        //    }
        //    return strPAYEE;

          
        //}
        //private void SetPayee(string strSelectedTextValues,char Delimeter)
        //{

        //    string[] strPayees = strSelectedTextValues.Split(Delimeter);
        //    for(int i=0;i<strPayees.Length;i++)
        //    {
        //        for(int j=0;j<cmbPAYEE.Items.Count;j++)
        //        {
        //            //if(combo.Items[j].Value  == strPayees[i].ToString() + "^" + REQ_SPECIAL_HANDLING)
        //            if(cmbPAYEE.Items[j].Value.Trim() != "")
        //            {
        //                string[] temp = cmbPAYEE.Items[j].Value.Split(Delimeter);
        //                if(temp[0]== strPayees[i].ToString()) 
        //                {
        //                    cmbPAYEE.Items[j].Selected = true;
        //                    continue;
        //                }
        //            }

        //        }
        //    }
        //}

		private void SetPayeeDetails()
		{
			//DataSet dsTemp;
            //if(hidCALLED_FROM.Value == CALLED_FROM_EXPENSE)
            //{
            //    dsTemp = objPayee.GetPayeeDetails(hidCLAIM_ID.Value,hidACTIVITY_ID.Value, hidEXPENSE_ID.Value);
            //    if (dsTemp.Tables[0].Rows.Count > 0)
            //    {
            //        DataRow drTemp = dsTemp.Tables[0].Rows[0];
					
            //        //lblPAYEE.Text = drTemp["NAME"].ToString();
            //        //lblREFERENCE.Text = drTemp["REFERENCE"].ToString();
            //        txtADDRESS1.Text = drTemp["ADDRESS1"].ToString();
            //        txtADDRESS2.Text =  drTemp["ADDRESS2"].ToString();
            //        txtCITY.Text = drTemp["CITY"].ToString();
            //        lblBANK_NAME.Text = drTemp["BANK_NAME"].ToString();
            //        lblAGENCY_BANK.Text = drTemp["AGENCY_BANK"].ToString(); // Added by Santosh Kumar Gautam on 16 Nov 2010
            //        txtINVOICE_DUE_DATE.Text = ConvertDBDateToCulture(drTemp["INVOICE_DUE_DATE "].ToString()); // Added by Santosh Kumar Gautam on 16 Nov 2010
            //        lblACCOUNT_NUMBER.Text = drTemp["ACCOUNT_NUMBER"].ToString();
            //        lblACCOUNT_NAME.Text = drTemp["ACCOUNT_NAME"].ToString();
            //        /*if (drTemp["COUNTRY"].ToString() == "0")
            //            cmbCOUNTRY.SelectedIndex = 0;
            //        else
            //            cmbCOUNTRY.SelectedValue = drTemp["COUNTRY"].ToString();*/

                   
                   
				
            //        if (drTemp["STATE"].ToString() == "0")
            //            cmbSTATE.SelectedIndex = 0;
            //        else
            //            cmbSTATE.SelectedValue = drTemp["STATE"].ToString();

            //        txtZIP.Text = drTemp["ZIP"].ToString();
            //        hidPARTY_ID.Value = drTemp["PARTY_ID"].ToString();
            //    }
            //    else
            //        lblBANK_NAME.Text = lblACCOUNT_NAME.Text = lblACCOUNT_NUMBER.Text = "";
            //}
            //else
			{
				ClsActivityExpense objExpense = new ClsActivityExpense();
				DataTable dtPayee = objExpense.GetClaimAllParties(hidCLAIM_ID.Value,int.Parse(hidACTIVITY_ID.Value), int.Parse(cmbRECOVERY_TYPE.SelectedValue)).Tables[0];
				//Change Web Control to HTML dropdown				
                if(dtPayee!=null && dtPayee.Rows.Count>0)
				{
					/*cmbPAYEE.DataSource =  dtPayee;
					cmbPAYEE.DataTextField="NAME";
					cmbPAYEE.DataValueField="PARTY_ID";
					cmbPAYEE.DataBind();									*/
					//cmbPAYEE.Items.Insert(0,new ListItem("","-1"));
					
					/*cmbSECONDARY_PARTY_ID.DataSource =  dtPayee;
					cmbSECONDARY_PARTY_ID.DataTextField="NAME";
					cmbSECONDARY_PARTY_ID.DataValueField="PARTY_ID";
					cmbSECONDARY_PARTY_ID.DataBind();*/

                    
                    BindDropDown(cmbPAYEE);
 
					cmbPAYEE_ADDRESS.DataSource= dtPayee;
					cmbPAYEE_ADDRESS.DataTextField="NAME";
					cmbPAYEE_ADDRESS.DataValueField="PARTY_ID";
					cmbPAYEE_ADDRESS.DataBind();
					cmbPAYEE_ADDRESS.Items.Insert(0,new ListItem("",""));
					//cmbPAYEE_ADDRESS.Items.Insert(dtPayee.Rows.Count+1,new ListItem("Other","-1"));
                    //cmbPAYEE_ADDRESS.SelectedValue = "-1";

				}
			}
		}

		/// <summary>
		/// Check Spcl Handling Checks
		/// </summary>
		/// <returns></returns>
		//Added For iTrack Issue #5857.
		private int CheckSpecialHandling()
		{
			for(int i = 0; i <cmbPAYEE.Items.Count;i++)
			{		   
				if(cmbPAYEE.Items[i].Value.Trim() !="")
				{					  
					string[] selected_value = null;
					selected_value = cmbPAYEE.Items[i].Value.Split('^');
                    
					if(selected_value[1] == "10963")
					{
						if(cmbPAYEE.Items[i].Selected == true)
						{
							return -1;
						
						}//lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1050");   
						//lblMessage.Visible = true;  
					}
				}
			}
			return 1;
		}
             //Display RED 
		public void BindDropDown(HtmlSelect ddlToBind)
		{
			ddlToBind.Items.Clear();
			ClsActivityExpense objExpense = new ClsActivityExpense();
            DataTable dtSource = objExpense.GetClaimAllParties(hidCLAIM_ID.Value, int.Parse(hidACTIVITY_ID.Value),int.Parse(cmbRECOVERY_TYPE.SelectedValue)).Tables[0];
            
			DataView dv = new DataView(dtSource);
			

			try
			{
				int i =0;
				foreach(DataRowView row in dv)
				{
					ddlToBind.Items.Add(new ListItem(row["NAME"].ToString(),row["PARTY_ID"].ToString()));
					string[] strCompanyData ;
					strCompanyData = row["PARTY_ID"].ToString().Split('^');
					if(strCompanyData[1] =="10963") 
						ddlToBind.Items[i].Attributes.Add ("Class","GrandFatheredRange");
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
		}

		private void cmbPAYEE_ADDRESS_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			 
			DataSet dsTemp = new DataSet();			
			try
			{
               
				//if(cmbPAYEE.SelectedItem!=null && cmbPAYEE.SelectedItem.Value!="" && cmbPAYEE.SelectedItem.Value!="-1")
				//if(Request["cmbPAYEE_ADDRESS"]!=null && Request["cmbPAYEE_ADDRESS"].ToString()!="")				
				if(hidPAYEE_ADDRESS.Value!="" && hidPAYEE_ADDRESS.Value!="0" && hidPAYEE_ADDRESS.Value!="-1")
				{
					
					//dsTemp = ClsAddPartyDetails.GetValuesForParty(hidCLAIM_ID.Value,hidPAYEE_ADDRESS.Value);
					//arryPayeeID = cmbPAYEE.SelectedValue.Split('^');
					 arryPayeeID = hidPAYEE_ADDRESS.Value.Split('^'); //cmbPAYEE.Value.Split('^');

					dsTemp = ClsAddPartyDetails.GetValuesForParty(hidCLAIM_ID.Value,arryPayeeID[0],int.Parse(GetLanguageID()));
					if (dsTemp!=null && dsTemp.Tables.Count>0 && dsTemp.Tables[0].Rows.Count>0)
					{
						DataRow drTemp = dsTemp.Tables[0].Rows[0];					
						//lblPAYEE.Text = drTemp["NAME"].ToString();
						//if (drTemp["REFERENCE"]!=null && drTemp["REFERENCE"].ToString()!="")
						//	lblREFERENCE.Text = drTemp["REFERENCE"].ToString();
                        if (drTemp["ADDRESS1"] != null && drTemp["ADDRESS1"].ToString() != "")
                        {
                            txtADDRESS1.Text = drTemp["ADDRESS1"].ToString();
                            txtADDRESS1.Enabled = false;
                        }
                        else
                        {
                            txtADDRESS1.Text = "";
                            txtADDRESS1.Enabled = true;
                        }
                        if (drTemp["ADDRESS2"] != null && drTemp["ADDRESS2"].ToString() != "")
                        {
                            txtADDRESS2.Text = drTemp["ADDRESS2"].ToString();
                            txtADDRESS2.Enabled = false;
                        }
                        else
                        {
                            txtADDRESS2.Text = "";
                            txtADDRESS2.Enabled = true;
                        }

                        if (drTemp["CITY"] != null && drTemp["CITY"].ToString() != "")
                        {
                            txtCITY.Text = drTemp["CITY"].ToString();
                            txtCITY.Enabled = false;
                        }
                        else
                        {
                            txtCITY.Text = "";
                            txtCITY.Enabled = true;
                        }

                        if (drTemp["BANK_NUMBER"] != null && drTemp["BANK_NUMBER"].ToString() != "")
                            lblBANK_NUMBER.Text = drTemp["BANK_NUMBER"].ToString();
						else
                            lblBANK_NUMBER.Text = "";

                     


                        //Added by Santosh Kumar Gautam on 27 Jan 2011
                        if (drTemp["PAYMENT_METHOD"] != null && drTemp["PAYMENT_METHOD"].ToString() != "")
                        {
                            if(cmbPAYMENT_METHOD.Items.FindByValue(drTemp["PAYMENT_METHOD"].ToString())!=null)
                                cmbPAYMENT_METHOD.SelectedValue = drTemp["PAYMENT_METHOD"].ToString();
                        }
                            
                       
                        //Added by Santosh Kumar Gautam on 16 Nov 2010
                        if (drTemp["BANK_BRANCH"] != null && drTemp["BANK_BRANCH"].ToString() != "")
                            lblBANK_BRANCH.Text = drTemp["BANK_BRANCH"].ToString();
                        else
                            lblBANK_BRANCH.Text = "";

                        if (drTemp["TYPE"] != null && drTemp["TYPE"].ToString() != "")
                            lblPAYEE_TYPE.Text = drTemp["TYPE"].ToString();
                        else
                            lblPAYEE_TYPE.Text = "";

                        if (drTemp["ACCOUNT_NUMBER"] != null && drTemp["ACCOUNT_NUMBER"].ToString() != "")
                        {
                            Account_Number =  drTemp["ACCOUNT_NUMBER"].ToString();
                            lblACCOUNT_NUMBER.Text = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(Account_Number);
                            hidPAYEE_HAS_BANK_INFO.Value = "Y";
                        }
                        else
                        {
                            lblACCOUNT_NUMBER.Text = "";
                            hidPAYEE_HAS_BANK_INFO.Value = "N";

                        }
						if (drTemp["ACCOUNT_NAME"]!=null && drTemp["ACCOUNT_NAME"].ToString()!="")
                            lblACCOUNT_NAME.Text = drTemp["ACCOUNT_NAME"].ToString();
							 
						else
							lblACCOUNT_NAME.Text = "";
                        if (drTemp["COUNTRY"].ToString() == "0")
                        {
                            cmbCOUNTRY.SelectedIndex = 0;
                            cmbCOUNTRY.Enabled = true;
                        }
                        else
                        {
                            string Country=drTemp["COUNTRY"].ToString();
                            if(cmbCOUNTRY.Items.FindByValue(Country)!=null)
                            {

                              cmbCOUNTRY.SelectedValue =Country;
                              cmbCOUNTRY.Enabled = false;
                            }
                        }
				
						if (drTemp["STATE"]!=null && drTemp["STATE"].ToString()!="" && drTemp["STATE"].ToString() == "0")
						{
							cmbSTATE.SelectedIndex = 0;
							cmbSTATE.Enabled = true;
						}
						else
						{ 
                            string State=drTemp["STATE"].ToString();
                            if(cmbSTATE.Items.FindByValue(State)!=null)
                            {
                                cmbSTATE.SelectedValue = State;
							    cmbSTATE.Enabled = false;
                            }
						}

                        if (drTemp["ZIP"] != null && drTemp["ZIP"].ToString() != "")
                        {
                            txtZIP.Text = drTemp["ZIP"].ToString();
                            txtZIP.Enabled = false;
                        }
                        else
                        {
                            txtZIP.Text = "";
                            txtZIP.Enabled = true;
                        }
						rfvFIRST_NAME.Enabled = false;
						//trName.Attributes.Add("style","display:none");
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
		}

        protected void txtZIP_TextChanged(object sender, EventArgs e)
        {

        }

        protected void cmbRECOVERY_TYPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDropDown(cmbPAYEE);
        }
	}
}
