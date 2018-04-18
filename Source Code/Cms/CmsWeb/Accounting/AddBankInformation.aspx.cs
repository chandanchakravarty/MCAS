/******************************************************************************************
<Author					: -   Ajit Singh Chahal
<Start Date				: -	5/25/2005 11:21:37 AM
<End Date				: -	
<Description			: - 	Code behind for Add Bank Information.
<Review Date			: - 
<Reviewed By			: - 	
Modification History

<Modified Date			: - 
<Modified By			: - 
<Purpose				: - Code behind for Add Bank Information.

<Modified Date			: - 30-Nov-2006
<Modified By			: - Mohit Agarwal
<Purpose				: - 4 New Route Position Code and MICR Code.
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
using Cms.Model.Maintenance.Accounting;
using Cms.BusinessLayer.BlCommon.Accounting;
using Cms.BusinessLayer.BlCommon;  
using Cms.CmsWeb.Controls; 
using Cms.ExceptionPublisher.ExceptionManagement;
using System.IO;

namespace Cms.CmsWeb.Maintenance.Accounting
{
	/// <summary>
	/// Code behind for Add Bank Information.
	/// </summary>
	public class AddBankInformation : Cms.CmsWeb.cmsbase 
	{
			#region Page controls declaration
			protected System.Web.UI.WebControls.TextBox txtBANK_NAME;
			protected System.Web.UI.WebControls.TextBox txtBANK_ADDRESS1;
			protected System.Web.UI.WebControls.TextBox txtBANK_ADDRESS2;
			protected System.Web.UI.WebControls.TextBox txtBANK_CITY;
			protected System.Web.UI.WebControls.DropDownList cmbBANK_STATE;
			protected System.Web.UI.WebControls.TextBox txtBANK_ZIP;
			protected System.Web.UI.WebControls.TextBox txtBANK_ACC_TITLE;
			protected System.Web.UI.WebControls.TextBox txtBANK_NUMBER;
			protected System.Web.UI.WebControls.TextBox txtSTARTING_DEPOSIT_NUMBER;
            protected System.Web.UI.WebControls.TextBox txtBRANCH_NUMBER;
            protected System.Web.UI.WebControls.TextBox txtAGREEMENT_NUMBER;
			protected System.Web.UI.WebControls.CheckBox chkIS_CHECK_ISSUED;

			protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
			protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
			protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
            protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFor;
            protected System.Web.UI.HtmlControls.HtmlInputHidden hidBANK_ID;
            protected System.Web.UI.HtmlControls.HtmlInputHidden hidRootPath2;// changed by praveer for TFS# 763
            protected System.Web.UI.HtmlControls.HtmlInputHidden hidRootPathMain;// changed by praveer for TFS# 763
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvBANK_NAME;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvBANK_NUMBER;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvBANK_ACC_TITLE;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvSTARTING_DEPOSIT_NUMBER;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvTRANSIT_ROUTING_NUMBER;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOMPANY_ID;
            protected System.Web.UI.WebControls.RequiredFieldValidator rfvACCOUNT_TYPE;
            protected System.Web.UI.WebControls.RequiredFieldValidator rfvAGREEMENT_NUMBER;
            protected System.Web.UI.WebControls.RequiredFieldValidator rfvBRANCH_NUMBER;
            

			protected System.Web.UI.WebControls.RegularExpressionValidator revBANK_NUMBER;
			protected System.Web.UI.WebControls.RegularExpressionValidator revBANK_ZIP;
			protected System.Web.UI.WebControls.RegularExpressionValidator revSTARTING_DEPOSIT_NUMBER;
		    protected System.Web.UI.WebControls.RegularExpressionValidator revBANK_MICR_CODE;
          
			protected System.Web.UI.WebControls.Label lblMessage;
            protected System.Web.UI.WebControls.Label capMessages;
            protected System.Web.UI.WebControls.Label capACCOUNT_TYPE;
            protected System.Web.UI.WebControls.Label capBANK_TYPE; //added by aditya for itrack # 1505 on 08-8-2011
            protected System.Web.UI.WebControls.Label capSIGN_FILE_1;
            protected System.Web.UI.WebControls.Label capSIGN_FILE_2;
            protected System.Web.UI.WebControls.DropDownList cmbACCOUNT_TYPE;
            protected System.Web.UI.WebControls.DropDownList cmbBANK_TYPE; //added by aditya for itrack # 1505 on 08-8-2011

            //Added by pradeep kushwaha
            
            protected System.Web.UI.WebControls.Label capNUMBER;
            protected System.Web.UI.WebControls.TextBox txtNUMBER;

            protected System.Web.UI.WebControls.Label capREGISTERED;
            protected System.Web.UI.WebControls.DropDownList cmbREGISTERED;
        
            protected System.Web.UI.WebControls.Label capSTARTING_OUR_NUMBER;
            protected System.Web.UI.WebControls.TextBox txtSTARTING_OUR_NUMBER;

            protected System.Web.UI.WebControls.Label capENDING_OUR_NUMBER;
            protected System.Web.UI.WebControls.TextBox txtENDING_OUR_NUMBER;
            protected System.Web.UI.WebControls.Label capBRANCH_NUMBER;
            protected System.Web.UI.WebControls.Label capAGREEMENT_NUMBER;

            protected System.Web.UI.WebControls.CustomValidator csvBANK_NAME;
            protected System.Web.UI.WebControls.CustomValidator csvBANK_NUMBER; 
            protected System.Web.UI.WebControls.CustomValidator csvBRANCH_NUMBER; 
            #endregion

			#region Local form variables
			//START:*********** Local form variables *************
			string oldXML;
			//creating resource manager object (used for reading field and label mapping)
			System.Resources.ResourceManager objResourceMgr;
			private string strRowId, strFormSaved;
			//private int	intLoggedInUserID;

			protected System.Web.UI.WebControls.Label capBANK_NAME;
			protected System.Web.UI.WebControls.Label capBANK_NUMBER;
			protected System.Web.UI.WebControls.Label capBANK_ADDRESS1;
			protected System.Web.UI.WebControls.Label capBANK_ADDRESS2;
			protected System.Web.UI.WebControls.Label capBANK_CITY;
			protected System.Web.UI.WebControls.Label capBANK_STATE;
			protected System.Web.UI.WebControls.Label capBANK_ZIP;
			protected System.Web.UI.WebControls.Label capBANK_ACC_TITLE;
			protected System.Web.UI.WebControls.Label capIS_CHECK_ISSUED;
			protected System.Web.UI.WebControls.Label capSTARTING_DEPOSIT_NUMBER;
            protected System.Web.UI.WebControls.Label capBANK_COUNTRY;
            protected System.Web.UI.WebControls.Label capBNKRUTNO; //sneha
            protected System.Web.UI.WebControls.Label capEFTINFO; //sneha


		protected System.Web.UI.HtmlControls.HtmlInputHidden hidGL_ID;

		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.WebControls.Label capSTART_CHECK_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtSTART_CHECK_NUMBER;
        //protected System.Web.UI.WebControls.TextBox txtBANK_COUNTRY;  commented by avijit for singapore project
        protected System.Web.UI.WebControls.DropDownList cmbBANK_COUNTRY;// added by avijit for singapore project
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSTART_CHECK_NUMBER;
		protected System.Web.UI.WebControls.RegularExpressionValidator revSTART_CHECK_NUMBER;
		protected System.Web.UI.WebControls.Label capEND_CHECK_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtEND_CHECK_NUMBER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEND_CHECK_NUMBER;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEND_CHECK_NUMBER;
		protected Cms.CmsWeb.Controls.CmsButton btnResetSeries;
		protected System.Web.UI.WebControls.Label capROUTE_POSITION_CODE1;
		protected System.Web.UI.WebControls.TextBox txtROUTE_POSITION_CODE1;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvROUTE_POSITION_CODE1;
		protected System.Web.UI.WebControls.Label capROUTE_POSITION_CODE2;
		protected System.Web.UI.WebControls.TextBox txtROUTE_POSITION_CODE2;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvROUTE_POSITION_CODE2;
		protected System.Web.UI.WebControls.Label capROUTE_POSITION_CODE3;
		protected System.Web.UI.WebControls.TextBox txtROUTE_POSITION_CODE3;
		protected System.Web.UI.WebControls.Label capROUTE_POSITION_CODE4;
		protected System.Web.UI.WebControls.TextBox txtROUTE_POSITION_CODE4;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvROUTE_POSITION_CODE4;
		protected System.Web.UI.WebControls.Label capBANK_MICR_CODE;
		protected System.Web.UI.WebControls.TextBox txtBANK_MICR_CODE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvBANK_MICR_CODE;
		protected System.Web.UI.HtmlControls.HtmlInputFile txtSIGN_FILE_2;
		protected System.Web.UI.HtmlControls.HtmlInputFile txtSIGN_FILE_1;
		protected System.Web.UI.WebControls.Label lblSIGN_FILE_1;
		protected System.Web.UI.WebControls.Label lblSIGN_FILE_2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRootPath;
		protected string strUserName, strPassWd, strDomain, strFileName1, strFileName2;
		public string AccID;
        protected String Bnkid;
		protected System.Web.UI.WebControls.Label capCOMPANY_ID;
		protected System.Web.UI.WebControls.TextBox txtCOMPANY_ID;
		protected System.Web.UI.WebControls.Label capTRANSIT_ROUTING_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtTRANSIT_ROUTING_NUMBER;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCOMPANY_ID;
		protected System.Web.UI.WebControls.RegularExpressionValidator revTRANSIT_ROUTING_NUMBER;
		protected System.Web.UI.WebControls.CustomValidator csvTRANSIT_ROUTING_NUMBER;
		protected System.Web.UI.WebControls.CustomValidator csvVERIFY_TRANSIT_ROUTING_NUMBER;
		protected System.Web.UI.WebControls.CustomValidator csvVERIFY_TRANSIT_ROUTING_NUMBER_LENGHT;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnCOMPANY_ID;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnTRANSIT_ROUTING_NUMBER;
		protected System.Web.UI.WebControls.CustomValidator  csvCOMPANY_ID;
//		protected System.Web.UI.WebControls.CustomValidator csvCOMPANY_ID;
//		protected static bool flag;

        //Shikha ITrack - 57
        protected System.Web.UI.WebControls.Label capADD_NUMBER;
        protected System.Web.UI.WebControls.TextBox txtADD_NUMBER; 
        protected System.Web.UI.WebControls.RegularExpressionValidator revSTARTING_OUR_NUMBER;
        protected System.Web.UI.WebControls.RegularExpressionValidator revENDING_OUR_NUMBER;

        //Added by Amit as per singapore requirement
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvBANK_ZIP;
	    
			//Defining the business layer class object
			ClsBankInformation  objBankInformation ;
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
            ClsMessages.SetCustomizedXml(GetLanguageCode());
			rfvBANK_NAME.ErrorMessage						=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			rfvBANK_NUMBER.ErrorMessage						=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			rfvBANK_ACC_TITLE.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
			rfvSTARTING_DEPOSIT_NUMBER.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
			rfvSTART_CHECK_NUMBER.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"8");
			rfvEND_CHECK_NUMBER.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"9");										
			revSTART_CHECK_NUMBER.ValidationExpression		=	aRegExpInteger;
			revEND_CHECK_NUMBER.ValidationExpression		=	aRegExpInteger;
            rfvBANK_ZIP.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("37");
            //Shikha itrack- 57
            revSTARTING_OUR_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
            revSTARTING_OUR_NUMBER.ValidationExpression = aRegExpInteger;

            revENDING_OUR_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
            revENDING_OUR_NUMBER.ValidationExpression = aRegExpInteger;
			//changes on 12 Nov by uday
			//revBANK_NUMBER.ValidationExpression				=	aRegExpAlphaNumStrict;
			revBANK_NUMBER.ValidationExpression				=	aRegExpInteger;
			revBANK_MICR_CODE.ValidationExpression			=	aRegExpInteger;
			//end
			revBANK_ZIP.ValidationExpression				=	aRegExpZip;
			revSTARTING_DEPOSIT_NUMBER.ValidationExpression	=	aRegExpInteger;
			//revCOMPANY_ID.ValidationExpression				=	aRegExpDoublePositiveNonZeroStartWithZero;//aRegExpFederalID;
			revCOMPANY_ID.ValidationExpression				=	aRegExpDoublePositiveNonZeroStartWithZeroForFedId;//aRegExpFederalID;
			revTRANSIT_ROUTING_NUMBER.ValidationExpression	=   aRegExpDoublePositiveNonZeroStartWithZero;
											
			revSTART_CHECK_NUMBER.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"10");
			revEND_CHECK_NUMBER.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"11");
			revBANK_NUMBER.ErrorMessage						=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
			revBANK_ZIP.ErrorMessage						=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6");
			revSTARTING_DEPOSIT_NUMBER.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");
			revBANK_MICR_CODE.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"12");
			//revCOMPANY_ID.ErrorMessage						=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("434");
			revTRANSIT_ROUTING_NUMBER.ErrorMessage			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
			revCOMPANY_ID.ErrorMessage		                =	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
            rfvACCOUNT_TYPE.ErrorMessage                    =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1274");
            rfvAGREEMENT_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "14");
            rfvBRANCH_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "13");
            rfvROUTE_POSITION_CODE1.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "16");
            rfvROUTE_POSITION_CODE2.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "17");
            rfvROUTE_POSITION_CODE4.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "18");
            rfvBANK_MICR_CODE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "15");
            rfvCOMPANY_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "19");
            rfvTRANSIT_ROUTING_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "21");
            revTRANSIT_ROUTING_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "22");
            csvTRANSIT_ROUTING_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "23");
            csvVERIFY_TRANSIT_ROUTING_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "24");
            csvVERIFY_TRANSIT_ROUTING_NUMBER_LENGHT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "25");
            csvCOMPANY_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"26");
            //Added for itrack-927
            csvBANK_NAME.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "27"); 
            csvBANK_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "28");
            csvBRANCH_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "29");
            //Added till here 


		}
			#endregion

			#region Set Captions
		private void SetCaptions()
		{
			capBANK_NAME.Text						=		objResourceMgr.GetString("txtBANK_NAME");
			capBANK_ADDRESS1.Text					=		objResourceMgr.GetString("txtBANK_ADDRESS1");
			capBANK_ADDRESS2.Text					=		objResourceMgr.GetString("txtBANK_ADDRESS2");
			capBANK_CITY.Text						=		objResourceMgr.GetString("txtBANK_CITY");
			capBANK_STATE.Text						=		objResourceMgr.GetString("cmbBANK_STATE");
			capBANK_ZIP.Text						=		objResourceMgr.GetString("txtBANK_ZIP");
			capBANK_ACC_TITLE.Text					=		objResourceMgr.GetString("txtBANK_ACC_TITLE");
			capBANK_NUMBER.Text						=		objResourceMgr.GetString("txtBANK_NUMBER");
			capSTARTING_DEPOSIT_NUMBER.Text			=		objResourceMgr.GetString("txtSTARTING_DEPOSIT_NUMBER");
			capIS_CHECK_ISSUED.Text					=		objResourceMgr.GetString("chkIS_CHECK_ISSUED");
			capSTART_CHECK_NUMBER.Text				=		objResourceMgr.GetString("txtSTART_CHECK_NUMBER");
			capEND_CHECK_NUMBER.Text				=		objResourceMgr.GetString("txtEND_CHECK_NUMBER");
			capROUTE_POSITION_CODE1.Text			=		objResourceMgr.GetString("txtROUTE_POSITION_CODE1");
			capROUTE_POSITION_CODE2.Text			=		objResourceMgr.GetString("txtROUTE_POSITION_CODE2");
			capROUTE_POSITION_CODE3.Text			=		objResourceMgr.GetString("txtROUTE_POSITION_CODE3");
			capROUTE_POSITION_CODE4.Text			=		objResourceMgr.GetString("txtROUTE_POSITION_CODE4");
			capBANK_MICR_CODE.Text					=		objResourceMgr.GetString("txtBANK_MICR_CODE");
			capCOMPANY_ID.Text						=		objResourceMgr.GetString("txtCOMPANY_ID");
			capTRANSIT_ROUTING_NUMBER.Text			=		objResourceMgr.GetString("txtTRANSIT_ROUTING_NUMBER");
            capACCOUNT_TYPE.Text = objResourceMgr.GetString("cmbACCOUNT_TYPE");
            capBANK_TYPE.Text = objResourceMgr.GetString("cmbBANK_TYPE"); //added by aditya for itrack # 1505 on 08-8-2011

            capBNKRUTNO.Text = objResourceMgr.GetString("capBNKRUTNO"); //sneha		
            capEFTINFO.Text = objResourceMgr.GetString("capEFTINFO"); //sneha		
            btnResetSeries.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1334"); //sneha		
		

            //Added by pradeep
            capNUMBER.Text = objResourceMgr.GetString("txtNUMBER");
            capREGISTERED.Text = objResourceMgr.GetString("capREGISTERED");
            capSTARTING_OUR_NUMBER.Text = objResourceMgr.GetString("capSTARTING_OUR_NUMBER");
            capENDING_OUR_NUMBER.Text = objResourceMgr.GetString("capENDING_OUR_NUMBER");
            capBRANCH_NUMBER.Text = objResourceMgr.GetString("capBRANCH_NUMBER");
            capAGREEMENT_NUMBER.Text = objResourceMgr.GetString("capAGREEMENT_NUMBER");
            capBANK_COUNTRY.Text = objResourceMgr.GetString("capBANK_COUNTRY");
            //Shikha - itrack 57
            capADD_NUMBER.Text = objResourceMgr.GetString("txtADD_NUMBER");
            capSIGN_FILE_1.Text = objResourceMgr.GetString("capSIGN_FILE_1");
            capSIGN_FILE_2.Text = objResourceMgr.GetString("capSIGN_FILE_2");
		}

		#endregion

			#region PageLoad event
			private void Page_Load(object sender, System.EventArgs e)
			{
				btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
                if (Request.QueryString["CALLED_FOR"]!=null && Request.QueryString["CALLED_FOR"].ToString() == "BANKINFO")
                    base.ScreenId = "451_0";
                else
				    base.ScreenId="125_1_1";
				lblMessage.Visible = false;
                capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");

                //START:*********** Setting permissions and class (Read/write/execute/delete)  *************
				btnReset.CmsButtonClass					=	CmsButtonType.Write;
				btnReset.PermissionString				=	gstrSecurityXML;

				btnSave.CmsButtonClass					=	CmsButtonType.Write;
				btnSave.PermissionString				=	gstrSecurityXML;

				btnResetSeries.CmsButtonClass			=	CmsButtonType.Write;
				btnResetSeries.PermissionString			=	gstrSecurityXML;


				//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
				objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.Accounting.AddBankInformation" ,System.Reflection.Assembly.GetExecutingAssembly());

				//DO NOT SHIFT inside if(!Page.IsPostBack)
                //Added by pradeep kushwaha on 17-03-2010

                if (Request.QueryString["CALLED_FOR"] != null && Request.QueryString["CALLED_FOR"].ToString() != "")
                {
                    if (Request.QueryString["CALLED_FOR"].ToString() == "BANKINFO")
                    {
                        Bnkid = Request.QueryString["BANK_ID"];
                        hidBANK_ID.Value = Request.QueryString["BANK_ID"];
                        hidCalledFor.Value = Request.QueryString["CALLED_FOR"].ToString();
                        if(Request.QueryString["ACCOUNT_ID"]!=null)
                            AccID = Request.QueryString["ACCOUNT_ID"].ToString();
                    }
                }
                else
                {
                    btnResetSeries.Visible = true;
                    //if (Request.QueryString["CALLED_FOR"]!=null && Request.QueryString["CALLED_FOR"].ToString() != "BANKINFO")
                    // {
                        if (Request.QueryString["ACCOUNT_ID"] != null && Request.QueryString["ACCOUNT_ID"].ToString() != "")
                        {
                           AccID = Request.QueryString["ACCOUNT_ID"];
                        }
                        else if (Session["ACCOUNT_ID"] != null)
                        {
                           AccID = Session["ACCOUNT_ID"].ToString();
                        }                       
                     //}
               }

				//
				int DefaultAccID = ClsBankInformation.FetchDefaultAccountID();
				if(AccID == DefaultAccID.ToString())
				{
					// make mandatory
					rfvTRANSIT_ROUTING_NUMBER.IsValid = false;
					rfvTRANSIT_ROUTING_NUMBER.Enabled = true;
					rfvTRANSIT_ROUTING_NUMBER.Visible = true;
					spnTRANSIT_ROUTING_NUMBER.Visible = true;
					rfvCOMPANY_ID.IsValid = false;
					rfvCOMPANY_ID.Enabled = true;
					spnCOMPANY_ID.Visible = true;
				}
				else
				{
					rfvTRANSIT_ROUTING_NUMBER.IsValid = true;
					rfvTRANSIT_ROUTING_NUMBER.Enabled = false;
					rfvTRANSIT_ROUTING_NUMBER.Visible = false;
					spnTRANSIT_ROUTING_NUMBER.Visible = false;
					rfvCOMPANY_ID.IsValid = true;
					rfvCOMPANY_ID.Enabled = false;
					spnCOMPANY_ID.Visible = false;
				}
				
				if(!Page.IsPostBack)
				{
                    SetErrorMessages();
                    //added by pradeep kushwaha
                    if (Request.QueryString["BANK_ID"] != null && Request.QueryString["BANK_ID"].ToString() != "" )
                    {
                        hidOldData.Value = ClsBankInformation.GetXmlForPageControls(Session["GL_ID"] == null ? null : Session["GL_ID"].ToString(), AccID == null ? null : AccID, Bnkid == null ? null : Bnkid);

                    }
                    if (Request.QueryString["ACCOUNT_ID"] != null && Request.QueryString["ACCOUNT_ID"].ToString() != "" && Session["GL_ID"] != null) 
                    {                   
                        hidOldData.Value = ClsBankInformation.GetXmlForPageControls(Session["GL_ID"] == null ? null : Session["GL_ID"].ToString(), AccID == null ? null : AccID, Bnkid == null ? null : Bnkid);
                    }

                       
					this.SetCaptions();
                    this.BindBankStateDropDown();
                    this.setAccount_type();
                    this.setBank_type(); //added by aditya for itrack # 1505 on 08-8-2011
                    this.REGISTERED_YESNO();
                    this.LoadDropDowns(); //added by avijit for singapore project
                    
                    string strSysID = GetSystemId();
                    if (strSysID == "ALBAUAT")
                        strSysID = "ALBA";
                    if (ClsCommon.IsXMLResourceExists(@Request.PhysicalApplicationPath +"CmsWeb/support/PageXML/" + strSysID , "AddBankInformation.xml"))
                    {
                        setPageControls(Page, @Request.PhysicalApplicationPath+"CmsWeb/support/PageXML/" + strSysID + "/AddBankInformation.xml");                      
                    }

                    hidRootPathMain.Value = System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL") + @"/SignatureFileAttachments/" + GetSystemId() + "/";// changed by praveer for TFS# 763
                    Getolddata();// changed by praveer for TFS# 763
				}
               
			}//end pageload
			#endregion
            private void Getolddata()// changed by praveer for TFS# 763
            {
                try
                {
                    string filename;
                    if (ClsCommon.FetchValueFromXML("SIGN_FILE_1", hidOldData.Value) != "" && ClsCommon.FetchValueFromXML("SIGN_FILE_1", hidOldData.Value) != null)
                    {
                        if (Request.QueryString["CALLED_FOR"] == null || Request.QueryString["CALLED_FOR"] == "" || (AccID != null && AccID !="" && int.Parse(AccID) > 0))
                        {
                          
                            filename = hidRootPathMain.Value + AccID + "_" + ClsCommon.FetchValueFromXML("GL_ID", hidOldData.Value) +"_" + ClsCommon.FetchValueFromXML("SIGN_FILE_1", hidOldData.Value);
                        }
                        else
                        {
                             filename = hidRootPathMain.Value + hidBANK_ID.Value + "_" + ClsCommon.FetchValueFromXML("SIGN_FILE_1", hidOldData.Value);
                        }
                        int startOfFile = filename.IndexOf("Upload");
                        string filePath = filename.Substring(startOfFile + 6);
                        string[] fileURL = filePath.Split('.');
                        string EncryptedPath = ClsCommon.CreateContentViewerURL(filePath, fileURL[1].ToUpper());
                        hidRootPath.Value = EncryptedPath;
                        
                    }
                     if (ClsCommon.FetchValueFromXML("SIGN_FILE_2", hidOldData.Value) != "" && ClsCommon.FetchValueFromXML("SIGN_FILE_2", hidOldData.Value) != null)
                    {
                        if (Request.QueryString["CALLED_FOR"] == null || Request.QueryString["CALLED_FOR"] == "" ||  (AccID != null && AccID !="" && int.Parse(AccID) > 0))
                        {

                            filename = hidRootPathMain.Value + AccID + "_" + ClsCommon.FetchValueFromXML("GL_ID", hidOldData.Value) + "_" + ClsCommon.FetchValueFromXML("SIGN_FILE_2", hidOldData.Value);
                        }
                        else
                        {
                            filename = hidRootPathMain.Value + hidBANK_ID.Value + "_" + ClsCommon.FetchValueFromXML("SIGN_FILE_2", hidOldData.Value);
                        }
                        int startOfFile = filename.IndexOf("Upload");
                        string filePath = filename.Substring(startOfFile + 6);
                        string[] fileURL = filePath.Split('.');
                        string EncryptedPath = ClsCommon.CreateContentViewerURL(filePath, fileURL[1].ToUpper());
                        hidRootPath2.Value = EncryptedPath;

                    }
                }
                catch (Exception ex)
                {
                    Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                }
            
            }
            private void BindBankStateDropDown()
            {
                #region "Loading singleton"
                DataTable dt = Cms.CmsWeb.ClsFetcher.ActiveState;
                cmbBANK_STATE.DataSource = dt;
                cmbBANK_STATE.DataTextField = "STATE_NAME";
                cmbBANK_STATE.DataValueField = "STATE_ID";
                cmbBANK_STATE.DataBind();
                #endregion//Loading singleton
            }
            private void setAccount_type()
            {
                cmbACCOUNT_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("ACTYP");
                cmbACCOUNT_TYPE.DataTextField = "LookupDesc";
                cmbACCOUNT_TYPE.DataValueField = "LookupID";
                cmbACCOUNT_TYPE.DataBind();
                cmbACCOUNT_TYPE.Items.Insert(0, "");
                cmbACCOUNT_TYPE.SelectedIndex = cmbACCOUNT_TYPE.Items.IndexOf(cmbACCOUNT_TYPE.Items.FindByValue( ClsCommon.FetchValueFromXML("ACCOUNT_TYPE", hidOldData.Value)));

            }

            private void setBank_type()  //added by aditya for itrack # 1505 on 08-8-2011
            {
                cmbBANK_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("BNKTYP");
                cmbBANK_TYPE.DataTextField = "LookupDesc";
                cmbBANK_TYPE.DataValueField = "LookupID";
                cmbBANK_TYPE.DataBind();
                //cmbBANK_TYPE.Items.Insert(0, "");
                cmbBANK_TYPE.SelectedIndex = cmbBANK_TYPE.Items.IndexOf(cmbBANK_TYPE.Items.FindByValue(ClsCommon.FetchValueFromXML("ACCOUNT_TYPE", hidOldData.Value)));

            }
            private void REGISTERED_YESNO()
            {
                cmbREGISTERED.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("yesno");
                cmbREGISTERED.DataTextField = "LookupDesc";
                cmbREGISTERED.DataValueField = "LookupID";
                cmbREGISTERED.DataBind();
            }

            private void LoadDropDowns() //function added by avijit for singapore project
            {
                DataTable dt = Cms.CmsWeb.ClsFetcher.AllCountry;
                cmbBANK_COUNTRY.DataSource = dt;
                cmbBANK_COUNTRY.DataTextField = COUNTRY_NAME;
                cmbBANK_COUNTRY.DataValueField = COUNTRY_ID;
                cmbBANK_COUNTRY.DataBind();

            }

			#region Validation Check
		private bool DoValidationCheck()
		{
			try
			{	
				if(strRowId.ToUpper().Equals("NEW"))
				{
					if( txtSIGN_FILE_1.PostedFile.FileName.Trim().Equals(""))
					{
						return false;
					}
					if( txtSIGN_FILE_2.PostedFile.FileName.Trim().Equals(""))
					{
						return false;
					}

					if( txtSIGN_FILE_1.Value.Trim().Equals(""))
					{
						return false;
					}
					if( txtSIGN_FILE_2.Value.Trim().Equals(""))
					{
						return false;
					}
					if (txtSIGN_FILE_1.PostedFile.InputStream.Length == 0)
					{
						lblMessage.Visible = true;
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","123");
						hidFormSaved.Value = "1";
						return false;
					}
					if (txtSIGN_FILE_2.PostedFile.InputStream.Length == 0)
					{
						lblMessage.Visible = true;
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","123");
						hidFormSaved.Value = "1";
						return false;
					}
				}
				return true;
			}
			catch 
			{
				return false;
			}
		}
		#endregion

			#region GetFormValue
			private ClsBankInformationInfo GetFormValue()
			{
				//Creating the Model object for holding the New data
				ClsBankInformationInfo objBankInformationInfo;
				objBankInformationInfo = new ClsBankInformationInfo();

                if (Session["GL_ID"] != null && Session["GL_ID"].ToString() != "")
                {
                    objBankInformationInfo.GL_ID = int.Parse(Session["GL_ID"].ToString());
                }
                else
                {
                    objBankInformationInfo.GL_ID = 0;

                }
                if (AccID != null && AccID.ToString() != "")
                {
                    objBankInformationInfo.ACCOUNT_ID = Convert.ToInt32(int.Parse(AccID));
                }
                else
                {
                    objBankInformationInfo.ACCOUNT_ID = 0;
                }               				
				
				objBankInformationInfo.BANK_NAME			=	txtBANK_NAME.Text;
				objBankInformationInfo.BANK_ADDRESS1		=	txtBANK_ADDRESS1.Text;
				objBankInformationInfo.BANK_ADDRESS2		=	txtBANK_ADDRESS2.Text;
				objBankInformationInfo.BANK_CITY			=	txtBANK_CITY.Text;
				objBankInformationInfo.BANK_STATE			=	cmbBANK_STATE.SelectedValue;
				objBankInformationInfo.BANK_ZIP				=	txtBANK_ZIP.Text;
				objBankInformationInfo.BANK_ACC_TITLE		=	txtBANK_ACC_TITLE.Text;
				objBankInformationInfo.BANK_NUMBER		    =	txtBANK_NUMBER.Text;
                if (txtSTART_CHECK_NUMBER.Text.Trim() != string.Empty)
                {
                    objBankInformationInfo.STARTING_DEPOSIT_NUMBER = int.Parse(txtSTARTING_DEPOSIT_NUMBER.Text);
                }
                objBankInformationInfo.START_CHECK_NUMBER = int.Parse(txtSTART_CHECK_NUMBER.Text == "" ? "-1" : txtSTART_CHECK_NUMBER.Text);
				objBankInformationInfo.END_CHECK_NUMBER		= 	int.Parse(txtEND_CHECK_NUMBER.Text == ""?"-1":txtEND_CHECK_NUMBER.Text);
				objBankInformationInfo.ROUTE_POSITION_CODE1 =	txtROUTE_POSITION_CODE1.Text;
				objBankInformationInfo.ROUTE_POSITION_CODE2 =	txtROUTE_POSITION_CODE2.Text;
				objBankInformationInfo.ROUTE_POSITION_CODE3 =	txtROUTE_POSITION_CODE3.Text;
				objBankInformationInfo.ROUTE_POSITION_CODE4 =   txtROUTE_POSITION_CODE4.Text;
				objBankInformationInfo.BANK_MICR_CODE		=	txtBANK_MICR_CODE.Text;
				objBankInformationInfo.TRANSIT_ROUTING_NUMBER = txtTRANSIT_ROUTING_NUMBER.Text;
				objBankInformationInfo.COMPANY_ID			 = txtCOMPANY_ID.Text;
                if (cmbACCOUNT_TYPE.SelectedValue.ToString() != string.Empty)
                {
                    objBankInformationInfo.ACCOUNT_TYPE = int.Parse(cmbACCOUNT_TYPE.SelectedValue);
                }
                if (!string.IsNullOrEmpty(cmbBANK_TYPE.SelectedValue))
                objBankInformationInfo.BANK_TYPE = int.Parse(cmbBANK_TYPE.SelectedValue); //added by aditya for itrack # 1505 on 08-8-2011
               
                //added by pradeep  kushwaha on 17-03-2010

                //objBankInformationInfo.BANK_ID =Convert.ToInt32(int.Parse(Bnkid));
                objBankInformationInfo.NUMBER = txtNUMBER.Text;
                objBankInformationInfo.REGISTERED = Convert.ToInt32(cmbREGISTERED.SelectedValue);
                objBankInformationInfo.STARTING_OUR_NUMBER = txtSTARTING_OUR_NUMBER.Text;
                objBankInformationInfo.ENDING_OUR_NUMBER = txtENDING_OUR_NUMBER.Text;
                objBankInformationInfo.BRANCH_NUMBER = txtBRANCH_NUMBER.Text;
                objBankInformationInfo.AGREEMENT_NUMBER = txtAGREEMENT_NUMBER.Text;
                //objBankInformationInfo.BANK_COUNTRY = txtBANK_COUNTRY.Text;   commented by avijit for singapore project
                objBankInformationInfo.BANK_COUNTRY = cmbBANK_COUNTRY.Text; //added by avijit for singapore project
                //end
                objBankInformationInfo.ADD_NUMBER = txtADD_NUMBER.Text;
				if(chkIS_CHECK_ISSUED.Checked)
					objBankInformationInfo.IS_CHECK_ISSUED=	"Y";
				else
					objBankInformationInfo.IS_CHECK_ISSUED=	"N";

				//These  assignments are common to all pages.
				strFormSaved	=	hidFormSaved.Value;
				strRowId		=	hidGL_ID.Value;
				oldXML		= hidOldData.Value;
				//Returning the model object

				//=========  Attachments ===========START
			
				strUserName = System.Configuration.ConfigurationManager.AppSettings.Get("IUserName");
				strPassWd = System.Configuration.ConfigurationManager.AppSettings.Get("IPassWd");
				strDomain = System.Configuration.ConfigurationManager.AppSettings.Get("IDomain");

				//---- Begin
				//Below code gets the Signature File names from Label or Text i.e; 
				//in either update or saved case.
				if(oldXML != "")
				{
					System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
					xmlDoc.LoadXml(oldXML);
		
					System.Xml.XmlNode xmlNode1 = xmlDoc.SelectSingleNode("/NewDataSet/Table/SIGN_FILE_1");
					if(xmlNode1!=null)
						lblSIGN_FILE_1.Text		=  xmlNode1.InnerText;
					
					System.Xml.XmlNode xmlNode2 = xmlDoc.SelectSingleNode("/NewDataSet/Table/SIGN_FILE_2");
					if(xmlNode2!=null)
						lblSIGN_FILE_2.Text		=  xmlNode2.InnerText;
				}
                if (Request.Form["hidFileInputVisible1"].ToString().Equals("Y"))
                {
                    //added by amit mishra
                    if (txtSIGN_FILE_1.PostedFile != null)
                    strFileName1 = txtSIGN_FILE_1.PostedFile.FileName;

                }
                else
                    strFileName1 = lblSIGN_FILE_1.Text;


                if (Request.Form["hidFileInputVisible2"].ToString().Equals("Y"))
                {
                    if (txtSIGN_FILE_2.PostedFile != null) //added by amit mishra
                        strFileName2 = txtSIGN_FILE_2.PostedFile.FileName;
                }
                else
                    strFileName2 = lblSIGN_FILE_2.Text;
					
				//----  End
                if (!string.IsNullOrEmpty(strFileName1))//added by amit mishra
                {
                    int intIndex1 = strFileName1.LastIndexOf("\\");
                    strFileName1 = strFileName1.Substring(intIndex1 + 1);	//Taking only file name not whole path
                    objBankInformationInfo.SIGN_FILE_1 =   strFileName1;
                }
                //added by amit mishra
                if (!string.IsNullOrEmpty(strFileName2))
                {
                    int intIndex2 = strFileName2.LastIndexOf("\\");
                    strFileName2 = strFileName2.Substring(intIndex2 + 1);
                    objBankInformationInfo.SIGN_FILE_2 = strFileName2;
                }
				
				
				
				//=========  Attachments ===========END

				return objBankInformationInfo;
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
				this.btnResetSeries.Click += new System.EventHandler(this.btnResetSeries_Click);
				this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
				this.Load += new System.EventHandler(this.Page_Load);

			}
			#endregion

			#region "Web Event Handlers"
		
			private void btnSave_Click(object sender, System.EventArgs e)
			{
				try
				{
					int intRetVal;	//For retreiving the return value of business class save function
					objBankInformation = new  ClsBankInformation(true);

					//Retreiving the form values into model class object
					ClsBankInformationInfo objBankInformationInfo = GetFormValue();
	
					string EntityType = "";
					if(Request.QueryString["EntityType"] != null)
						EntityType = Request.QueryString["EntityType"].ToString();
					//Checking whether the values supplied are valid or not
					//if ( DoValidationCheck() == false ) return false;

					if(strRowId.ToUpper().Equals("NEW") || strRowId.Trim().Equals("")) //save case
					{
						objBankInformationInfo.CREATED_BY = int.Parse(GetUserId());
						objBankInformationInfo.CREATED_DATETIME = DateTime.Now;
						objBankInformationInfo.MODIFIED_BY = objBankInformationInfo.CREATED_BY;
						objBankInformationInfo.LAST_UPDATED_DATETIME = objBankInformationInfo.CREATED_DATETIME;
						//Calling the add method of business layer class
						intRetVal = objBankInformation.Add(objBankInformationInfo,strFileName1,strFileName2, EntityType);

						if(intRetVal>0)
						{
							hidGL_ID.Value = objBankInformationInfo.GL_ID.ToString();
                            hidBANK_ID.Value = objBankInformationInfo.GL_ID.ToString();
                            lblMessage.Text = ClsMessages.FetchGeneralMessage("29");
							hidFormSaved.Value			=	"1";
							hidIS_ACTIVE.Value = "Y";

                            AddAttachment(Convert.ToString(objBankInformationInfo.BANK_ID));
                            
                            hidOldData.Value = ClsBankInformation.GetXmlForPageControls(Session["GL_ID"] == null ? null : Session["GL_ID"].ToString(), AccID == null ? null : AccID, Convert.ToString(objBankInformationInfo.BANK_ID));
                            hidBANK_ID.Value = Convert.ToString(objBankInformationInfo.BANK_ID); //Added by Aditya on 20-09-2011 for TFS Bug # 763
                            Getolddata();// changed by praveer for TFS# 763
						}
                        else if (intRetVal == -10)
                        {
                            lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "20");
                            hidFormSaved.Value = "2";
                        }
                        else
                        {
                            lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                            hidFormSaved.Value = "2";
                        }
						lblMessage.Visible = true;
					//	flag=true; // used in GetFormValue() to update attachments
					} // end save case
					else //UPDATE CASE
					{
						//Creating the Model object for holding the Old data
						ClsBankInformationInfo objOldBankInformationInfo;
						objOldBankInformationInfo = new ClsBankInformationInfo();

						//Setting  the Old Page details(XML File containing old details) into the Model Object
						base.PopulateModelObject(objOldBankInformationInfo,hidOldData.Value);

						//Setting those values into the Model object which are not in the page
						objBankInformationInfo.GL_ID = int.Parse(strRowId);
						objBankInformationInfo.MODIFIED_BY = int.Parse(GetUserId());
						objBankInformationInfo.LAST_UPDATED_DATETIME = DateTime.Now;
						

						//Updating the record using business layer class object
						intRetVal	= objBankInformation.Update(objOldBankInformationInfo,objBankInformationInfo,strFileName1,strFileName2, EntityType);
						if( intRetVal > 0 )			// update successfully performed
						{
                            lblMessage.Text = ClsMessages.FetchGeneralMessage("31");
							hidFormSaved.Value		=	"1";
                            AddAttachment(Convert.ToString(objOldBankInformationInfo.BANK_ID));
							hidOldData.Value  = ClsBankInformation.GetXmlForPageControls(Session["GL_ID"]==null?null:Session["GL_ID"].ToString(),AccID==null?null:AccID,Convert.ToString(objOldBankInformationInfo.BANK_ID));
                            hidBANK_ID.Value = Convert.ToString(objOldBankInformationInfo.BANK_ID); //Added by Aditya on 20-09-2011 for TFS Bug # 763
                            Getolddata();// changed by praveer for TFS# 763
						}
                        else if (intRetVal == -10)
                        {
                            lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "20");
                            hidFormSaved.Value = "2";
                        }
                        else
                        {
                            lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                            hidFormSaved.Value = "2";
                        }
						lblMessage.Visible = true;
					//	flag=false; // used in GetFormValue() to update attachments
					}
				}
				catch(Exception ex)
				{
					lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
					lblMessage.Visible	=	true;
					hidFormSaved.Value			=	"2";
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
					
					//ExceptionManager.Publish(ex);
				}
				finally
				{
					if(objBankInformation!= null)
						objBankInformation.Dispose();
				}
			}
		
		#region ATTACHMENTS :: Add / Create Directory Structure / Save Uploaded File

		protected void AddAttachment(string bankid)
		{
			//=================== ATTACHMENTS ====================
			//Beginigng the impersonation 
			Cms.BusinessLayer.BlCommon.ClsAttachment objAttachment = new ClsAttachment();
			if (objAttachment.ImpersonateUser(strUserName, strPassWd, strDomain))
			{

                if (AccID != null && Session["GL_ID"] != null && Session["GL_ID"].ToString() != "")
                {
                    if (!SaveUploadedFile(txtSIGN_FILE_1, txtSIGN_FILE_2, AccID == null ? null : AccID, Session["GL_ID"] == null ? null : Session["GL_ID"].ToString(), bankid == null ? null : bankid))
                    {
                        //Some error occured while uploading 
                        lblMessage.Text += "\n Unable to upload the file.";
                    }
                }
                else
                {
                    if (!SaveUploadedFile(txtSIGN_FILE_1, txtSIGN_FILE_2, AccID==null?null:AccID , Session["GL_ID"]==null?null:Session["GL_ID"].ToString(), bankid == null ? null : bankid))
                    {
                        //Some error occured while uploading 
                        lblMessage.Text += "\n Unable to upload the file.";
                    }
 
                }
              
										
				//ending the impersonation 
				objAttachment.endImpersonation();
			}
			else
			{
				//Impersation failed
				lblMessage.Text += "\n Unable to upload the file. User impersonation failed.";
			}
			//=================== ATTACHMENTS ====================
		}
		private bool SaveUploadedFile(HtmlInputFile objFile1,HtmlInputFile objFile2,string GLId,string intAccountId,string BankID)
		{
			try
			{
				//Stores the name of the directory where file will get stored
				string strDirName;
				strDirName = CreateDirStructure();
				//Retreiving the extension -- FILE 1
                if (objFile1.PostedFile != null)
                {
                    string strFileName1;
                    int Index1 = objFile1.PostedFile.FileName.LastIndexOf("\\");
                    if (Index1 >= 0)
                    {
                        strFileName1 = objFile1.PostedFile.FileName.Substring(Index1 + 1);
                    }
                    else
                    {
                        strFileName1 = objFile1.PostedFile.FileName;
                    }
                    if (Convert.ToString(GLId) != null && Convert.ToString(intAccountId) != null)
                    {
                        objFile1.PostedFile.SaveAs(strDirName + "\\" + GLId + "_" + intAccountId + "_" + strFileName1);
                    }
                    else
                    {
                        objFile1.PostedFile.SaveAs(strDirName + "\\" + BankID + "_" + strFileName1);
                    }
                }
				//Retreiving the extension -- FILE 2
                if (objFile2.PostedFile != null)
                {
                    string strFileName2;
                    int Index2 = objFile2.PostedFile.FileName.LastIndexOf("\\");
                    if (Index2 >= 0)
                    {
                        strFileName2 = objFile2.PostedFile.FileName.Substring(Index2 + 1);
                    }
                    else
                    {
                        strFileName2 = objFile2.PostedFile.FileName;
                    }
                    if (Convert.ToString(GLId) != null && Convert.ToString(intAccountId) != null)
                    {
                        objFile2.PostedFile.SaveAs(strDirName + "\\" + GLId + "_" + intAccountId + "_" + strFileName2);
                    }
                    else 
                    {
                        objFile2.PostedFile.SaveAs(strDirName + "\\" + BankID + "_" + strFileName2); 
                    }
                }
                ////copying the files
                //if (Convert.ToString(GLId) != null && Convert.ToString(intAccountId) != null)
                //{
                //    objFile1.PostedFile.SaveAs(strDirName + "\\" + GLId + "_" + intAccountId + "_" + strFileName1);
                //    objFile2.PostedFile.SaveAs(strDirName + "\\" + GLId + "_" + intAccountId + "_" + strFileName2);
                //}
                //else
                //{
                //    objFile1.PostedFile.SaveAs(strDirName + "\\" + BankID + "_" + strFileName1);
                //    objFile2.PostedFile.SaveAs(strDirName + "\\" + BankID + "_" + strFileName2); 
                
                //}
				return true;
			}
			catch (Exception objExp)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
				return false;
			}
		}

		
		private string CreateDirStructure()
		{
			
			string strRoot, strDirName = "";
			try
			{
				string strAgencyCode = GetSystemId();  //Login agency name, will come from session
				
				strRoot = System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL");
			
				strDirName = Server.MapPath(strRoot);
				
					//Creating the Attachment folder if not exists
					strDirName = strDirName + "\\SignatureFileAttachments";
					if (!System.IO.Directory.Exists(strDirName))
					{
						//Creating the directory
						System.IO.Directory.CreateDirectory(strDirName);
					}
				
				//Creating the login agence code, under attachment if not exists
				strDirName = strDirName + "\\" + strAgencyCode;
				if (!System.IO.Directory.Exists(strDirName))
				{
					//Creating the directory
					System.IO.Directory.CreateDirectory(strDirName);
				}

			}
			catch (Exception objEx)
			{
				throw (objEx);
			}
			return strDirName;
		}
		#endregion

	
		private void btnResetSeries_Click(object sender, System.EventArgs e)
		{
            if (AccID!=null && AccID != "")
            {
                ClsBankInformation.ResetCheckNumber(int.Parse(AccID));
            }
		}


		#endregion
            //Added for itrack-927
            /// <summary>
            /// Get the bank details
            /// </summary>
            /// <param name="SearchData"></param>
            /// <param name="CalledFor"></param>
            /// <returns></returns>
            [System.Web.Services.WebMethod]
            public static System.Collections.Generic.List<String> GetBankDetails(String SearchData, String SerachFor,String CalledFor,String Calledfrom)
            {
                try
                {
                    System.Collections.Generic.List<String> sbSearchedData = new System.Collections.Generic.List<String>();
                    CmsWeb.webservices.ClsWebServiceCommon obj = new CmsWeb.webservices.ClsWebServiceCommon();  //Declare the object ob common webservice

                    sbSearchedData = obj.GetBankData(SearchData, SerachFor, CalledFor, Calledfrom);
                    return sbSearchedData;
                }
                catch (Exception exc)
                {
                    Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(exc);
                    return null;
                }
                finally
                { }

            }//public static System.Collections.Generic.List<String> GetBankDetails(String SearchData, String CalledFor)

        //Added till here 

        protected void cmbACCOUNT_TYPE_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
			
		}
	}
