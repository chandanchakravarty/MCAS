/******************************************************************************************
<Author				: -   Amar
<Start Date				: -	5/8/2006 5:10:52 PM
<End Date				: -	
<Description				: - 	Test
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
using Cms.CmsWeb;
using System.Resources; 
using System.Reflection; 
using Cms.Model.Claims;
using Cms.CmsWeb.Controls; 
using Cms.ExceptionPublisher; 
using Cms.BusinessLayer.BLClaims;
using Cms.BusinessLayer.BlCommon;
using Cms.ExceptionPublisher.ExceptionManagement;
using System.Xml;

namespace Cms.Claims.Aspx
{
	/// <summary>
	/// 
	/// </summary>
	public class AddPartyDetails : Cms.Claims.ClaimBase
	{
		#region Page controls declaration
		protected System.Web.UI.WebControls.Label capEXPERT_SERVICE_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbEXPERT_SERVICE_TYPE;
		
		protected System.Web.UI.WebControls.Label capEXPERT_SERVICE_TYPE_DESC;
		protected System.Web.UI.WebControls.TextBox txtEXPERT_SERVICE_TYPE_DESC;		
				
		protected System.Web.UI.WebControls.Label capCONTACT_NAME;
		protected System.Web.UI.WebControls.TextBox txtCONTACT_NAME;
		protected System.Web.UI.WebControls.Label capPARENT_ADJUSTER;
		protected System.Web.UI.WebControls.TextBox txtPARENT_ADJUSTER;
		protected System.Web.UI.HtmlControls.HtmlImage imgSelect;
		protected System.Web.UI.WebControls.TextBox txtNAME;
		protected System.Web.UI.WebControls.TextBox txtADDRESS1;
		protected System.Web.UI.WebControls.TextBox txtADDRESS2;
		protected System.Web.UI.WebControls.TextBox txtCITY;
		protected System.Web.UI.WebControls.TextBox txtZIP;
		protected System.Web.UI.WebControls.TextBox txtCONTACT_PHONE;
		protected System.Web.UI.WebControls.TextBox txtCONTACT_EMAIL;
		protected System.Web.UI.WebControls.TextBox txtOTHER_DETAILS;
		protected System.Web.UI.WebControls.TextBox txtAGE;
        protected System.Web.UI.WebControls.TextBox txtAGENCY_BANK;
		protected System.Web.UI.WebControls.TextBox txtEXTENT_OF_INJURY;
		protected System.Web.UI.WebControls.TextBox txtCONTACT_PHONE_EXT;
		protected System.Web.UI.WebControls.TextBox txtCONTACT_FAX;
		protected System.Web.UI.WebControls.TextBox txtPARTY_TYPE_DESC;
//		protected System.Web.UI.WebControls.TextBox txtREFERENCE;
		protected System.Web.UI.WebControls.TextBox hidCUSTOMER_NAME;
		protected System.Web.UI.WebControls.TextBox hidCLAIMANT_NAME;
		protected System.Web.UI.WebControls.TextBox hidDefaultData;
		protected System.Web.UI.WebControls.Label capFEDRERAL_ID;
		protected System.Web.UI.WebControls.TextBox txtFEDRERAL_ID;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvFEDRERAL_ID;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revFEDRERAL_ID;
		protected System.Web.UI.WebControls.DropDownList cmbPARTY_TYPE_ID;
		protected System.Web.UI.WebControls.DropDownList cmbCOUNTRY;
		protected System.Web.UI.WebControls.DropDownList cmbSTATE;
		protected System.Web.UI.WebControls.DropDownList cmbPARTY_DETAIL;

		protected System.Web.UI.WebControls.Label capPROCESSING_OPTION_1099;
		protected System.Web.UI.WebControls.DropDownList cmbPROCESSING_OPTION_1099;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvPROCESSING_OPTION_1099;

		protected System.Web.UI.WebControls.Label capMASTER_VENDOR_CODE;
		protected System.Web.UI.WebControls.TextBox txtMASTER_VENDOR_CODE;
		protected System.Web.UI.HtmlControls.HtmlImage imgMASTER_VENDOR_CODE;
		

		protected System.Web.UI.WebControls.Label capVENDOR_CODE;
		protected System.Web.UI.WebControls.TextBox txtVENDOR_CODE;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvVENDOR_CODE;

		protected System.Web.UI.WebControls.Label capCONTACT_FAX;
		protected System.Web.UI.WebControls.Label capPARTY_TYPE_DESC;
		protected System.Web.UI.WebControls.Label capCONTACT_PHONE_EXT;
		protected System.Web.UI.WebControls.Label capNAME;
		protected System.Web.UI.WebControls.Label capADDRESS1;
		protected System.Web.UI.WebControls.Label capADDRESS2;
		protected System.Web.UI.WebControls.Label capCITY;
		protected System.Web.UI.WebControls.Label capSTATE;
		protected System.Web.UI.WebControls.Label capZIP;
		protected System.Web.UI.WebControls.Label capCONTACT_PHONE;
		protected System.Web.UI.WebControls.Label capCONTACT_EMAIL;
		protected System.Web.UI.WebControls.Label capOTHER_DETAILS;
		protected System.Web.UI.WebControls.Label capPARTY_TYPE_ID;
		protected System.Web.UI.WebControls.Label capCOUNTRY;
		protected System.Web.UI.WebControls.Label capEXTENT_OF_INJURY;
		protected System.Web.UI.WebControls.Label capAGE;
        protected System.Web.UI.WebControls.Label capAGENCY_BANK;
		protected System.Web.UI.WebControls.Label capPARTY_DETAIL;
        protected System.Web.UI.WebControls.Label capAdd;
//		protected System.Web.UI.WebControls.Label capREFERENCE;
        protected System.Web.UI.WebControls.Label capheaders;
        protected System.Web.UI.WebControls.Label capIS_BENEFICIARY;

		protected System.Web.UI.WebControls.TextBox txtBANK_NAME;
		protected System.Web.UI.WebControls.Label capBANK_NAME;
		protected System.Web.UI.WebControls.TextBox txtACCOUNT_NAME;
		protected System.Web.UI.WebControls.Label capACCOUNT_NAME;
		protected System.Web.UI.WebControls.TextBox txtACCOUNT_NUMBER;
		protected System.Web.UI.WebControls.Label capACCOUNT_NUMBER;

        //Shikha Add beneficiary
        protected System.Web.UI.WebControls.Label CapPARTY_PERCENTAGE;
        protected System.Web.UI.WebControls.TextBox txtPARTY_PERCENTAGE;


        protected System.Web.UI.WebControls.DropDownList cmbACCOUNT_TYPE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidEXPERT_SERVICE_TYPE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidMASTER_VENDOR_CODE_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidEXPERT_SERVICE_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPARENT_ADJUSTER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCONCAT_DATA;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData,hidPARTY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPROPERTY_DAMAGED_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPARTY_DETAIL;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidHeader;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;		
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPARTY_TYPE_DESC;
	//	protected System.Web.UI.WebControls.RequiredFieldValidator rfvCONTACT_EMAIL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvOTHER_DETAILS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPARTY_TYPE_ID;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOUNTRY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNAME;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvACCOUNT_TYPE;
	//  protected System.Web.UI.WebControls.RequiredFieldValidator rfvADDRESS1;		
//		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCITY;
//		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSTATE;
//		protected System.Web.UI.WebControls.RequiredFieldValidator rfvZIP;
		

		protected System.Web.UI.WebControls.RegularExpressionValidator revCONTACT_FAX;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCONTACT_PHONE_EXT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revADDRESS1;
		protected System.Web.UI.WebControls.RegularExpressionValidator revADDRESS2;
//		protected System.Web.UI.WebControls.RegularExpressionValidator revCITY;
		protected System.Web.UI.WebControls.RegularExpressionValidator revZIP;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCONTACT_PHONE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCONTACT_EMAIL;
		protected System.Web.UI.WebControls.RegularExpressionValidator revOTHER_DETAILS;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEXTENT_OF_INJURY;

        //protected System.Web.UI.WebControls.RegularExpressionValidator revACCOUNT_NUMBER;
        //protected System.Web.UI.WebControls.RegularExpressionValidator revBANK_BRANCH;	
		

	
		protected System.Web.UI.WebControls.Label capMAIL_1099_ADD1;
		protected System.Web.UI.WebControls.TextBox txtMAIL_1099_ADD1;
		protected System.Web.UI.WebControls.Label capMAIL_1099_ADD2;
		protected System.Web.UI.WebControls.TextBox txtMAIL_1099_ADD2;
		protected System.Web.UI.WebControls.Label capMAIL_1099_CITY;
		protected System.Web.UI.WebControls.TextBox txtMAIL_1099_CITY;
		protected System.Web.UI.WebControls.Label capMAIL_1099_STATE;
		protected System.Web.UI.WebControls.TextBox txtMAIL_1099_ZIP;
		protected System.Web.UI.WebControls.TextBox txtMAIL_1099_COUNTRY;
		protected System.Web.UI.WebControls.Label capMAIL_1099_COUNTRY;
		protected System.Web.UI.WebControls.DropDownList cmbMAIL_1099_COUNTRY;
		protected System.Web.UI.WebControls.DropDownList cmbMAIL_1099_STATE;
		protected System.Web.UI.WebControls.Label capMAIL_1099_ZIP;
	//	protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAIL_1099_ADD1;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAIL_1099_ADD2;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAIL_1099_CITY;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAIL_1099_STATE;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAIL_1099_COUNTRY;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAIL_1099_ZIP;
		protected System.Web.UI.WebControls.Image imgMAIL_1099_ZIP;
		protected System.Web.UI.WebControls.HyperLink hlkMAIL_1099_ZIP;
		protected System.Web.UI.WebControls.CheckBox chkCopyData;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revMAIL_1099_ZIP;

		protected System.Web.UI.WebControls.Label capMAIL_1099_NAME;
		protected System.Web.UI.WebControls.TextBox txtMAIL_1099_NAME;
		protected System.Web.UI.WebControls.Label capW9_FORM;
		protected System.Web.UI.WebControls.DropDownList cmbW9_FORM;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAIL_1099_NAME;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvW9_FORM;

        protected System.Web.UI.WebControls.Label capACCOUNT_TYPE;

        protected System.Web.UI.WebControls.Label capPAYMENT_METHOD;

        protected System.Web.UI.WebControls.DropDownList cmbPAYMENT_METHOD;
        
		protected System.Web.UI.WebControls.Label lblMessage;
		private const int EXPERT_SERVICE_PROVIDER_ID=4;				

        // Added by santosh kumar gautam on 03 march 2011 Itrack:874
        protected System.Web.UI.WebControls.Label capPARTY_CPF_CNPJ;
        protected System.Web.UI.WebControls.TextBox txtPARTY_CPF_CNPJ;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCPF_CNPJ;
        protected System.Web.UI.WebControls.CustomValidator csvCPF_CNPJ;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCPF_CNPJ;
 
        
        
		#endregion
		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		protected string gStrClaimID="";
        string strACCOUNT_NUMBER = ""; //Added by Aditya for tfs bug # 2522
		protected System.Web.UI.WebControls.RangeValidator rngAGE;
		protected System.Web.UI.WebControls.Label capSUB_ADJUSTER;
		protected System.Web.UI.WebControls.TextBox txtSUB_ADJUSTER;
		protected System.Web.UI.WebControls.Label capSUB_ADJUSTER_CONTACT_NAME;
		protected System.Web.UI.WebControls.TextBox txtSUB_ADJUSTER_CONTACT_NAME;
		protected System.Web.UI.WebControls.Label capSA_ADDRESS1;
		protected System.Web.UI.WebControls.TextBox txtSA_ADDRESS1;
		protected System.Web.UI.WebControls.Label capSA_ADDRESS2;
		protected System.Web.UI.WebControls.TextBox txtSA_ADDRESS2;
		protected System.Web.UI.WebControls.Label capSA_CITY;
		protected System.Web.UI.WebControls.TextBox txtSA_CITY;
		protected System.Web.UI.WebControls.Label capSA_COUNTRY;
		protected System.Web.UI.WebControls.DropDownList cmbSA_COUNTRY;
		protected System.Web.UI.WebControls.Label capSA_STATE;
		protected System.Web.UI.WebControls.DropDownList cmbSA_STATE;
		protected System.Web.UI.WebControls.Label capSA_ZIPCODE;
		protected System.Web.UI.WebControls.TextBox txtSA_ZIPCODE;
		protected System.Web.UI.WebControls.HyperLink hlkSAZipLookup;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revSA_ZIPCODE;
		protected System.Web.UI.WebControls.Label capSA_PHONE;
		protected System.Web.UI.WebControls.TextBox txtSA_PHONE;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revSA_PHONE;
		protected System.Web.UI.WebControls.Label capSA_FAX;
		protected System.Web.UI.WebControls.TextBox txtSA_FAX;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revSA_FAX;		
		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFEDRERAL_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidACCOUNT_NUMBER;  //Added by Aditya for tfs bug # 2522
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidNEW_ACCOUNT_NUMBER; //Added by Aditya for tfs bug # 2522
		protected System.Web.UI.WebControls.Label capFEDRERAL_ID_HID;
        protected System.Web.UI.WebControls.Panel PnlPartyOtherDetails;


        protected System.Web.UI.WebControls.Label capREGIONAL_ID;
        protected System.Web.UI.WebControls.Label capDISTRICT;
        protected System.Web.UI.WebControls.Label capREGIONAL_ID_ISSUANCE;
        protected System.Web.UI.WebControls.Label capREGIONAL_ID_ISSUE_DATE;
        protected System.Web.UI.WebControls.Label capGENDER;
        protected System.Web.UI.WebControls.Label capMARITAL_STATUS;
        protected System.Web.UI.WebControls.Label capPARTY_TYPE;
        protected System.Web.UI.WebControls.Label capBANK_BRANCH;
        protected System.Web.UI.WebControls.Label capBANK_NUMBER;

        protected System.Web.UI.WebControls.TextBox txtREGIONAL_ID;
        protected System.Web.UI.WebControls.TextBox txtDISTRICT;
        protected System.Web.UI.WebControls.TextBox txtREGIONAL_ID_ISSUANCE;
        protected System.Web.UI.WebControls.TextBox txtREGIONAL_ID_ISSUE_DATE;
        protected System.Web.UI.WebControls.TextBox txtBANK_BRANCH;
        protected System.Web.UI.WebControls.TextBox txtBANK_NUMBER;
        
        protected System.Web.UI.WebControls.DropDownList cmbMARITAL_STATUS;
        protected System.Web.UI.WebControls.DropDownList cmbGENDER;
        protected System.Web.UI.WebControls.DropDownList cmbPARTY_TYPE;
        protected System.Web.UI.WebControls.CheckBox chkIS_BENEFICIARY;


        protected System.Web.UI.WebControls.HyperLink hlkREGIONAL_ID_ISSUE_DATE;  // Added by Santosh kumar Gautam 19 Jan 2011
        protected System.Web.UI.WebControls.Image imgREGIONAL_ID_ISSUE_DATE;  // Added by Santosh kumar Gautam 19 Jan 2011
        protected System.Web.UI.WebControls.RegularExpressionValidator revREGIONAL_ID_ISSUE_DATE;  // Added by Santosh kumar Gautam 19 Jan 2011


		//Defining the business layer class object
		ClsAddPartyDetails  objPartyDetails ;
		//END:*********** Local variables *************

		#endregion

        public string javasciptmsg, javasciptCPFmsg, javasciptCNPJmsg, CPF_invalid, CNPJ_invalid;


		//Added by Mohit Agarwal 24-Sep 08 to Encrypt Federal Id
		private void setEncryptXml()
		{           
			//Added by Mohit Agarwal 23-Sep-08
			if(hidOldData.Value.IndexOf("NewDataSet") >= 0)
			{
				XmlDocument objxml = new XmlDocument();

				objxml.LoadXml(hidOldData.Value);
				txtVENDOR_CODE.ReadOnly=true;  //Added by Sibin for Itrack Issue 5055 on 20 Nov 08 to make the Vendor Code non-editable 
				XmlNode node = objxml.SelectSingleNode("NewDataSet");
				foreach(XmlNode nodes in node.SelectNodes("Table"))
				{
					XmlNode noder1 = nodes.SelectSingleNode("FEDRERAL_ID");
                    
                    XmlNode noder2 = nodes.SelectSingleNode("ACCOUNT_NUMBER"); //Added by Aditya for tfs bug # 2522
                    
                    hidFEDRERAL_ID.Value = noder1.InnerText;
                    if (noder2 != null)  //Added by Aditya for tfs bug # 2522
                        hidACCOUNT_NUMBER.Value = noder2.InnerText; //Added by Aditya for tfs bug # 2522
                   
                    string strFEDRERAL_ID = "";
                    
					//noder1.InnerText = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(noder1.InnerText);
                    if(noder1.InnerText=="0")
                       noder1.InnerText="";

                       strFEDRERAL_ID = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(noder1.InnerText);
                       if (noder2 != null)
                       {
                           if (noder2.InnerText == "0")  //Added by Aditya for tfs bug # 2522
                               noder2.InnerText = "";
                           if (noder2.InnerText.Trim().EndsWith("=") == true) //Added by Aditya for tfs bug # 2522
                           strACCOUNT_NUMBER = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(noder2.InnerText);  //Added by Aditya for tfs bug # 2522          
                           hidNEW_ACCOUNT_NUMBER.Value = strACCOUNT_NUMBER;
                       }

					if(strFEDRERAL_ID != "")
					{
						string strvaln = "";
						for(int len=0; len < strFEDRERAL_ID.Length-4; len++)
							strvaln += "x";

						strvaln += strFEDRERAL_ID.Substring(strvaln.Length, strFEDRERAL_ID.Length - strvaln.Length);
						capFEDRERAL_ID_HID.Text = strvaln;
					}
					else
						capFEDRERAL_ID_HID.Text = "";
				}
				objxml = null;
			}
		}

		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			rfvPARTY_TYPE_ID.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			rfvNAME.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			//rfvADDRESS1.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");			
			//rfvCITY.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
			//rfvSTATE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6");
			//rfvZIP.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");
			revADDRESS1.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"8");
			revADDRESS2.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"9");
//			revCITY.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"10");
			revZIP.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1181");
		//	rfvCONTACT_PHONE.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"12");
		//	rfvCONTACT_EMAIL.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"13");
		//  rfvCOUNTRY.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"14");
            //revCONTACT_PHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1083");//Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"15");
            //revCONTACT_PHONE_EXT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("25");
            //revCONTACT_FAX.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1083");//Cms.CmsWeb.ClsMessages.FetchGeneralMessage("15");
			revCONTACT_EMAIL.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"16");
			rfvPARTY_TYPE_DESC.ErrorMessage	=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("550");
			revOTHER_DETAILS.ErrorMessage   =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"17");			
			revEXTENT_OF_INJURY.ErrorMessage=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"19");
			rngAGE.ErrorMessage=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"18");
           // rfvW9_FORM.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1307");
            revCONTACT_PHONE.ValidationExpression = aRegExpAgencyPhone;
            revCONTACT_FAX.ValidationExpression = aRegExpAgencyPhone;
            revCONTACT_PHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1974");
            revCONTACT_FAX.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1974");
            revCONTACT_PHONE_EXT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("25");
			revADDRESS1.ValidationExpression		= aRegExpAlphaNum;
			revADDRESS2.ValidationExpression		= aRegExpAlphaNum;
//			revCITY.ValidationExpression			= aRegExpAlpha;
            revZIP.ValidationExpression = aRegExpZip;
            //revCONTACT_PHONE.ValidationExpression = aRegExpPhoneBrazil;
			revCONTACT_PHONE_EXT.ValidationExpression = aRegExpExtn;
           // revCONTACT_FAX.ValidationExpression = aRegExpPhoneBrazil;
			revCONTACT_EMAIL.ValidationExpression	= aRegExpEmail;
			revOTHER_DETAILS.ValidationExpression   = aRegExpTextArea500;			
			revEXTENT_OF_INJURY.ValidationExpression= aRegExpAlphaNum;
            //rfvFEDRERAL_ID.ErrorMessage				=		  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"20");
            //revFEDRERAL_ID.ValidationExpression   = aRegExpFederalID;
            //revFEDRERAL_ID.ErrorMessage				=		  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"24");
            //rfvPROCESSING_OPTION_1099.ErrorMessage				=		  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"21");
            //rfvVENDOR_CODE.ErrorMessage				=		  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"22");
            //revSA_ZIPCODE.ValidationExpression					= aRegExpZip;
            //revSA_PHONE.ValidationExpression				= aRegExpPhone;
            //revSA_FAX.ValidationExpression					= aRegExpPhone;

            //revSA_ZIPCODE.ErrorMessage							= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");
            //revSA_PHONE.ErrorMessage						= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"15");
            //revSA_FAX.ErrorMessage							= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("15");
			//rfvEXPERT_SERVICE_TYPE_DESC.ErrorMessage	=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("550");
			//rfvEXPERT_SERVICE_TYPE.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"25");
           // rfvACCOUNT_TYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "26");
			 
            //rfvMAIL_1099_CITY.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage("32_0","56");
            //rfvMAIL_1099_STATE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage("32_0","35");
            //rfvMAIL_1099_COUNTRY.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage("32_0","33");
            //rfvMAIL_1099_ZIP.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage("32_0","37");
            //revMAIL_1099_ZIP.ValidationExpression	=	aRegExpZip;
            //revMAIL_1099_ZIP.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("24");

            revREGIONAL_ID_ISSUE_DATE.ValidationExpression = aRegExpDate;
            revREGIONAL_ID_ISSUE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");

            //revACCOUNT_NUMBER.ValidationExpression = aRegExpAccountNumber;
            //revACCOUNT_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1198");

            //revBANK_BRANCH.ValidationExpression = aRegExpBankBranch;
           // revBANK_BRANCH.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1199");

            revCPF_CNPJ.Attributes.Add("ErrMsgcpf", Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "21"));
            revCPF_CNPJ.Attributes.Add("ErrMsgcnpj", Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "22"));

         
            revCPF_CNPJ.ValidationExpression = aRegExpCpf_Cnpj;
            revCPF_CNPJ.Attributes.Add("ErrMsgcpf", Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "28"));
            revCPF_CNPJ.Attributes.Add("ErrMsgcnpj", Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "29"));

           
       

            rfvCPF_CNPJ.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "30");
		}
		#endregion
		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
           

            string LangID = "";
            LangID = GetLanguageID();
              btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralButtonsText("btnActivateDeactivate");
               btnActivateDeactivate.Enabled = false;
			if(Request.QueryString["CLAIM_ID"]!=null)
				gStrClaimID = Request.QueryString["CLAIM_ID"].ToString();
			btnReset.Attributes.Add("onClick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");

			chkCopyData.Attributes.Add("onClick","javascript:CopyData('txtNAME','txtMAIL_1099_NAME');CopyData('txtADDRESS1','txtMAIL_1099_ADD1');CopyData('txtADDRESS2','txtMAIL_1099_ADD2');CopyData('txtCITY','txtMAIL_1099_CITY');CopyData('txtZIP','txtMAIL_1099_ZIP');CopyData('cmbSTATE','cmbMAIL_1099_STATE');");        
			// phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
			base.ScreenId="307_0";
			lblMessage.Visible = false;
			SetErrorMessages();
            capheaders.Text=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
            
            
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.AddPartyDetails" ,System.Reflection.Assembly.GetExecutingAssembly());


            javasciptCPFmsg = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "28");
            javasciptCNPJmsg = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "29");
            CPF_invalid = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "31");
            CNPJ_invalid = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "32");
            javasciptmsg = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "33");
           

            if(!Page.IsPostBack)
            {
                capAGENCY_BANK.Visible = false;
                txtAGENCY_BANK.Visible = false;
                capBANK_NAME.Visible = false;
                txtBANK_NAME.Visible = false;
             
                //CapPARTY_PERCENTAGE.Visible = false;
                //txtPARTY_PERCENTAGE.Visible = false;

                FillDropdwon();
                PnlPartyOtherDetails.Visible = false;// Added by santosh gautam on 30 dec 2010 reference Itrack :631

				imgMAIL_1099_ZIP.Attributes.Add("style","cursor:hand");
                hlkREGIONAL_ID_ISSUE_DATE.Attributes.Add("OnClick", "fPopCalendar(txtREGIONAL_ID_ISSUE_DATE,txtREGIONAL_ID_ISSUE_DATE)");
				base.VerifyAddress(hlkMAIL_1099_ZIP,txtMAIL_1099_ADD1,txtMAIL_1099_ADD2, txtMAIL_1099_CITY, cmbMAIL_1099_STATE, txtMAIL_1099_ZIP);
				cmbEXPERT_SERVICE_TYPE.Attributes.Add("onChange","javascript:HideShowTypeDesc();");
				imgSelect.Attributes.Add("onclick","javascript:OpenParentAdjusterLookup('')");
				imgMASTER_VENDOR_CODE.Attributes.Add("onclick","javascript:OpenMasterVendorCodeLookup();");//Done By Sibin for Itrack Issue 5055 on 19 Nov 08

                cmbPARTY_TYPE.Attributes.Add("onChange", "javascript:OnCustomerTypeChange();");

				if(Request.QueryString["PARTY_ID"]!=null && Request.QueryString["PARTY_ID"].ToString().Length>0)
				{
					hidPARTY_ID.Value = Request.QueryString["PARTY_ID"].ToString();
                   
                    hidOldData.Value = ClsAddPartyDetails.GetXmlForPageControls(hidPARTY_ID.Value, gStrClaimID, int.Parse(GetLanguageID()));
					setEncryptXml();
                    string Party_Type_ID = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("PARTY_TYPE_ID", hidOldData.Value);
                    string IS_BENEFICIARY = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("IS_BENEFICIARY", hidOldData.Value);
                    if (IS_BENEFICIARY.Trim() == "Y")
                        chkIS_BENEFICIARY.Checked = true;
                    else
                        chkIS_BENEFICIARY.Checked = false;

                    if(Party_Type_ID=="157")
                    {
                            CapPARTY_PERCENTAGE.Visible = true;
                            txtPARTY_PERCENTAGE.Visible = true;
                    }
                    
                    string strRegionalDate = ClsCommon.FetchValueFromXML("REGIONAL_ID_ISSUE_DATE", hidOldData.Value.ToString());
                    if (!string.IsNullOrEmpty(strRegionalDate))
                    {
                        txtREGIONAL_ID_ISSUE_DATE.Text = strRegionalDate;
                    }
                    
				}


                
             
				//Added sep 28 2007
                if (Request.QueryString["PROPERTY_DAMAGED_ID"] != null && Request.QueryString["PROPERTY_DAMAGED_ID"].ToString() != "")
                {
                    hidPROPERTY_DAMAGED_ID.Value = Request.QueryString["PROPERTY_DAMAGED_ID"].ToString();
                    //hidOldData.Value = ClsAddPartyDetails.GetXmlForPageControlsPD(hidPROPERTY_DAMAGED_ID.Value.ToString(),gStrClaimID);
                    //if(hidOldData.Value == "<NewDataSet />")
                    //	hidOldData.Value = "0";


                }
                
               SetCaptions();
				
			}
			//Sumit Chhabra:
			/*We have an issue to not allow parties added through system. Since system will be the first 
			 * to add any parties, we can safely assume that the PARTY_ID generated by the system will be 1 and 2.
			 * We can add a check on PARTY_ID that if it is less than 3 (ie 1&2), then its system generated and 
			 * hence editing of these items should not be allowed
			 * */
			/*ITrack Issue # 1375.. We should be able to make changes to system added parties.
			 * Following code is therefore being commented
			*/
			/*if(hidPARTY_ID.Value=="1" || hidPARTY_ID.Value=="2")
			{
				lblMessage.Text =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"23");				
				lblMessage.Visible = true;
				gstrSecurityXML = "<Security><Read>Y</Read><Write>N</Write><Delete>N</Delete><Execute>N</Execute></Security>";
			}*/

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass				=	CmsButtonType.Execute;
			btnReset.PermissionString			=	gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass				=	CmsButtonType.Execute;
			btnActivateDeactivate.PermissionString			=	gstrSecurityXML;

			btnSave.CmsButtonClass		=	CmsButtonType.Execute;
			btnSave.PermissionString	=	gstrSecurityXML;

			//txtVENDOR_CODE.ReadOnly=true;
			
		}//end pageload
		#endregion
		/// <summary>
		/// validate posted data from form
		/// </summary>
		/// <returns>True if posted data is valid else false</returns>
		private bool doValidationCheck()
		{
			try
			{
				return true;
			}
			catch (Exception ex)
			{
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				return false;
			}
		}

        private void FillDropdwon()
        {
            #region "Loading singleton"
            DataTable dt = Cms.CmsWeb.ClsFetcher.Country;
            cmbCOUNTRY.DataSource = dt;
            cmbCOUNTRY.DataTextField = "Country_Name";
            cmbCOUNTRY.DataValueField = "Country_Id";
            cmbCOUNTRY.DataBind();

            cmbPAYMENT_METHOD.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("PYMTD");  //Action on Payment Lookup
            cmbPAYMENT_METHOD.DataTextField = "LookupDesc";
            cmbPAYMENT_METHOD.DataValueField = "LookupID";
            cmbPAYMENT_METHOD.DataBind();
            cmbPAYMENT_METHOD.Items.Insert(0, "");
            //cmbMAIL_1099_COUNTRY.DataSource		= dt;
            //cmbMAIL_1099_COUNTRY.DataTextField	= "Country_Name";
            //cmbMAIL_1099_COUNTRY.DataValueField	= "Country_Id";
            //cmbMAIL_1099_COUNTRY.DataBind();

            //dt = Cms.CmsWeb.ClsFetcher.State;
            //cmbMAIL_1099_STATE.DataSource =dt;
            //cmbMAIL_1099_STATE.DataTextField ="State_Name";
            //cmbMAIL_1099_STATE.DataValueField ="State_Id";
            //cmbMAIL_1099_STATE.DataBind();

            if (cmbCOUNTRY.SelectedValue != "")
            {
                ClsStates objStates = new ClsStates();
                DataSet ds = objStates.GetStatesCountry(int.Parse(cmbCOUNTRY.SelectedValue));
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    cmbSTATE.DataSource = ds;
                    cmbSTATE.DataTextField = STATE_NAME;
                    cmbSTATE.DataValueField = STATE_ID;
                    cmbSTATE.DataBind();
                }
            }
            //cmbSA_STATE.DataSource		= dt;
            //cmbSA_STATE.DataTextField		= "STATE_NAME";
            //cmbSA_STATE.DataValueField	= "STATE_ID";
            //cmbSA_STATE.DataBind();
            //cmbSA_STATE.Items.Insert(0,new ListItem("",""));
            //cmbSA_STATE.SelectedIndex=0;

            dt = ClsAddPartyDetails.GetPartyTypes(int.Parse(GetLanguageID()));
            cmbPARTY_TYPE_ID.DataSource = dt;
            cmbPARTY_TYPE_ID.DataTextField = "Detail_Type_Description";
            cmbPARTY_TYPE_ID.DataValueField = "Detail_Type_ID";
            cmbPARTY_TYPE_ID.DataBind();
            cmbPARTY_TYPE_ID.Items.Insert(0, "");

            

            // Added by santosh kumar gautam 20 jan 2011 
            cmbPARTY_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CUSTYP"); ;
            cmbPARTY_TYPE.DataTextField = "LookupDesc";
            cmbPARTY_TYPE.DataValueField = "LookupID";
            cmbPARTY_TYPE.DataBind();
         

            cmbACCOUNT_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("ACTYP");
            cmbACCOUNT_TYPE.DataTextField = "LookupDesc";
            cmbACCOUNT_TYPE.DataValueField = "LookupID";
            cmbACCOUNT_TYPE.DataBind();
            cmbACCOUNT_TYPE.Items.Insert(0, "");


            cmbPARTY_DETAIL.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("PTYDET");
            cmbPARTY_DETAIL.DataTextField = "LookupDesc";
            cmbPARTY_DETAIL.DataValueField = "LookupID";
            cmbPARTY_DETAIL.DataBind();
            cmbPARTY_DETAIL.Items.Insert(0, new ListItem("", ""));

            //cmbPROCESSING_OPTION_1099.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("ESPPO",null,"S");
            //cmbPROCESSING_OPTION_1099.DataTextField="LookupDesc";
            //cmbPROCESSING_OPTION_1099.DataValueField="LookupID";
            //cmbPROCESSING_OPTION_1099.DataBind();
            //cmbPROCESSING_OPTION_1099.Items.Insert(0,"");

            // Added by santosh kumar gautam 20 jan 2011 
            cmbMARITAL_STATUS.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("MARST");
            cmbMARITAL_STATUS.DataTextField = "LookupDesc";
            cmbMARITAL_STATUS.DataValueField = "LookupID";
            cmbMARITAL_STATUS.DataBind();
            cmbMARITAL_STATUS.Items.Insert(0, "");


            //Bind Gender

            cmbGENDER.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("Sex");
            cmbGENDER.DataTextField = "LookupDesc";
            cmbGENDER.DataValueField = "LookupID";
            cmbGENDER.DataBind();
            cmbGENDER.Items.Insert(0, "");

            // Added by Mohit Agarwal 13-Oct 2008 ITrack 4795 Reopen
            Cms.BusinessLayer.BlCommon.ClsCommon.RemoveOptionFromDropdownByValue(cmbPROCESSING_OPTION_1099, "14123");

            DataTable dtServiceProviders = ClsDefaultValues.GetDefaultValuesDetails(EXPERT_SERVICE_PROVIDER_ID);
            if (dtServiceProviders != null)
            {
                cmbEXPERT_SERVICE_TYPE.DataSource = dtServiceProviders;
                cmbEXPERT_SERVICE_TYPE.DataTextField = "DETAIL_TYPE_DESCRIPTION";
                cmbEXPERT_SERVICE_TYPE.DataValueField = "DETAIL_TYPE_ID";
                cmbEXPERT_SERVICE_TYPE.DataBind();
                cmbEXPERT_SERVICE_TYPE.Items.Insert(0, "");
            }
            //Added by Mohit Agarwal 25-Aug-2008
            //cmbW9_FORM.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("W9FORM");
            //cmbW9_FORM.DataTextField	= "LookupDesc";
            //cmbW9_FORM.DataValueField	= "LookupID";
            //cmbW9_FORM.DataBind();
            //cmbW9_FORM.Items.Insert(0,""); 

            string[] arr = ClsAddPartyDetails.GetDefaultCustomerClaimantNames(int.Parse(gStrClaimID));
            hidCUSTOMER_NAME.Text = arr[0];
            hidCLAIMANT_NAME.Text = arr[1];
            for (int i = 0; i < arr.Length; i++)
                hidDefaultData.Text += arr[i] + "^^^^";

            if (hidDefaultData.Text.Trim() != "")
                hidDefaultData.Text = hidDefaultData.Text.Trim().Substring(0, hidDefaultData.Text.Trim().Length - 4);


            #endregion//Loading singleton
        }
		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsAddPartyDetailsInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsAddPartyDetailsInfo objPartyDetailsInfo;
			objPartyDetailsInfo = new ClsAddPartyDetailsInfo();

			objPartyDetailsInfo.NAME		 =	txtNAME.Text;
			objPartyDetailsInfo.ADDRESS1	 =	txtADDRESS1.Text;
			objPartyDetailsInfo.ADDRESS2	 =	txtADDRESS2.Text;
			objPartyDetailsInfo.CITY		 =	txtCITY.Text;
			objPartyDetailsInfo.STATE		 =	int.Parse(cmbSTATE.SelectedValue);
			objPartyDetailsInfo.ZIP			 =	txtZIP.Text;
			objPartyDetailsInfo.CONTACT_PHONE=	txtCONTACT_PHONE.Text;
			objPartyDetailsInfo.CONTACT_EMAIL=	txtCONTACT_EMAIL.Text;
			objPartyDetailsInfo.OTHER_DETAILS=	txtOTHER_DETAILS.Text;
			objPartyDetailsInfo.PARTY_TYPE_ID=	int.Parse(cmbPARTY_TYPE_ID.SelectedValue);
			objPartyDetailsInfo.COUNTRY		 =	int.Parse(cmbCOUNTRY.SelectedValue);

			//Added by Mohit Agarwal 20-Aug-2008
			objPartyDetailsInfo.MAIL_1099_ADD1	=	txtMAIL_1099_ADD1.Text.Trim();
			objPartyDetailsInfo.MAIL_1099_ADD2	=	txtMAIL_1099_ADD2.Text.Trim();
			objPartyDetailsInfo.MAIL_1099_CITY		=	txtMAIL_1099_CITY.Text.Trim();
			if(cmbMAIL_1099_STATE.SelectedItem!=null && cmbMAIL_1099_STATE.SelectedItem.Value!="")
				objPartyDetailsInfo.MAIL_1099_STATE		=	cmbMAIL_1099_STATE.SelectedItem.Value;

            //Added by Santosh Kumar Gautam 27 Jan 2011
            if (cmbPAYMENT_METHOD.SelectedValue != "")
                objPartyDetailsInfo.PAYMENT_METHOD = int.Parse(cmbPAYMENT_METHOD.SelectedValue);

			objPartyDetailsInfo.MAIL_1099_ZIP			=	txtMAIL_1099_ZIP.Text.Trim();
			objPartyDetailsInfo.MAIL_1099_COUNTRY =	cmbMAIL_1099_COUNTRY.SelectedValue;

			objPartyDetailsInfo.MAIL_1099_NAME	=	txtMAIL_1099_NAME.Text.Trim();
			objPartyDetailsInfo.W9_FORM =	cmbW9_FORM.SelectedValue;
			
			//Done for Itrack Issue 7775 on 27 Sept 2010
			//objPartyDetailsInfo.AGE			 =	0;
			if(txtAGE.Text.Trim()!="")
				objPartyDetailsInfo.AGE		 =	int.Parse(txtAGE.Text);

			//objPartyDetailsInfo.PARTY_DETAIL =  0;
			if(cmbPARTY_DETAIL.SelectedItem!=null && cmbPARTY_DETAIL.SelectedValue !="")
				objPartyDetailsInfo.PARTY_DETAIL =	int.Parse(cmbPARTY_DETAIL.SelectedValue);
			//Done for Itrack Issue 7775 on 27 Sept 2010
//			else
//				objPartyDetailsInfo.PARTY_DETAIL =	int.Parse(hidPARTY_DETAIL.Value); 

			objPartyDetailsInfo.CONTACT_PHONE_EXT = txtCONTACT_PHONE_EXT.Text.Trim();
			objPartyDetailsInfo.CONTACT_FAX = txtCONTACT_FAX.Text.Trim();
			objPartyDetailsInfo.EXTENT_OF_INJURY	 =	txtEXTENT_OF_INJURY.Text;
//			objPartyDetailsInfo.REFERENCE = txtREFERENCE.Text; 
			objPartyDetailsInfo.BANK_NAME = txtBANK_NAME.Text;
            //Added by Santosh Kumar Gautam on 15 Nov 2010
            if(!string.IsNullOrEmpty(txtAGENCY_BANK.Text))
                objPartyDetailsInfo.AGENCY_BANK = txtAGENCY_BANK.Text.Trim();
            if (txtACCOUNT_NUMBER.Text.Trim() != "")  //Added by Aditya for tfs bug # 2522
            {
                objPartyDetailsInfo.ACCOUNT_NUMBER = Cms.BusinessLayer.BlCommon.ClsCommon.EncryptString(txtACCOUNT_NUMBER.Text.Trim());
                txtACCOUNT_NUMBER.Text = "";
            }
            else
                objPartyDetailsInfo.ACCOUNT_NUMBER = hidACCOUNT_NUMBER.Value;
			objPartyDetailsInfo.ACCOUNT_NAME = txtACCOUNT_NAME.Text; 
			objPartyDetailsInfo.CLAIM_ID = int.Parse(gStrClaimID);
			if(hidPARTY_ID.Value!="" && hidPARTY_ID.Value!="0" && hidPARTY_ID.Value.ToUpper()!="NEW")			
				objPartyDetailsInfo.PARTY_ID = int.Parse(hidPARTY_ID.Value);
			if(objPartyDetailsInfo.PARTY_TYPE_ID==111)//Other
				objPartyDetailsInfo.PARTY_TYPE_DESC = txtPARTY_TYPE_DESC.Text.Trim();
			if(hidPARENT_ADJUSTER_ID.Value!="")
				objPartyDetailsInfo.PARENT_ADJUSTER = int.Parse(hidPARENT_ADJUSTER_ID.Value.Trim());
			//objPartyDetailsInfo.FEDRERAL_ID=	txtFEDRERAL_ID.Text.Trim();
			if(txtFEDRERAL_ID.Text.Trim()!="")
			{
				objPartyDetailsInfo.FEDRERAL_ID			=	Cms.BusinessLayer.BlCommon.ClsCommon.EncryptString(txtFEDRERAL_ID.Text.Trim());
				txtFEDRERAL_ID.Text = "";
			}
			else
				objPartyDetailsInfo.FEDRERAL_ID			= hidFEDRERAL_ID.Value;

			objPartyDetailsInfo.CONTACT_NAME = txtCONTACT_NAME.Text.Trim();

			if(cmbPROCESSING_OPTION_1099.SelectedItem!=null && cmbPROCESSING_OPTION_1099.SelectedItem.Value!="")
				objPartyDetailsInfo.PROCESSING_OPTION_1099 = int.Parse(cmbPROCESSING_OPTION_1099.SelectedItem.Value);
			objPartyDetailsInfo.MASTER_VENDOR_CODE = hidMASTER_VENDOR_CODE_ID.Value.Trim();

            if (cmbACCOUNT_TYPE.SelectedItem != null && cmbACCOUNT_TYPE.SelectedItem.Value != "")
                objPartyDetailsInfo.ACCOUNT_TYPE = int.Parse(cmbACCOUNT_TYPE.SelectedValue);
			//Done for Itrack Issue 7775 on 23 Sept 2010
			if(objPartyDetailsInfo.PARTY_TYPE_ID==11)//When the party type chosen is Expert/Service Provider
			{
				objPartyDetailsInfo.VENDOR_CODE = txtVENDOR_CODE.Text.Trim();
			}
			else
				objPartyDetailsInfo.VENDOR_CODE = "";
//			if(hidEXPERT_SERVICE_ID.Value=="0" || hidEXPERT_SERVICE_ID.Value=="" || hidEXPERT_SERVICE_ID.Value=="NEW")
			//{
				/*if(cmbEXPERT_SERVICE_TYPE.SelectedItem!=null && cmbEXPERT_SERVICE_TYPE.SelectedItem.Value!="")
					hidEXPERT_SERVICE_TYPE.Value	=	cmbEXPERT_SERVICE_TYPE.SelectedItem.Value + "~";

				if(hidEXPERT_SERVICE_TYPE.Value==EXPERT_SERVICE_PROVIDER_TYPE_OTHER.ToString() && txtEXPERT_SERVICE_TYPE_DESC.Text.Trim()!="")
					hidEXPERT_SERVICE_TYPE.Value+= txtEXPERT_SERVICE_TYPE_DESC.Text.Trim();*/

				if(cmbEXPERT_SERVICE_TYPE.SelectedItem!=null && cmbEXPERT_SERVICE_TYPE.SelectedItem.Value!="")
					objPartyDetailsInfo.EXPERT_SERVICE_TYPE	=	int.Parse(cmbEXPERT_SERVICE_TYPE.SelectedItem.Value);// + "~";

				if(objPartyDetailsInfo.EXPERT_SERVICE_TYPE==EXPERT_SERVICE_PROVIDER_TYPE_OTHER && txtEXPERT_SERVICE_TYPE_DESC.Text.Trim()!="")
					objPartyDetailsInfo.EXPERT_SERVICE_TYPE_DESC = txtEXPERT_SERVICE_TYPE_DESC.Text.Trim();
//			}
//			else
//				objPartyDetailsInfo.EXPERT_SERVICE_TYPE = 191; //Other

			//Sep 27 2007
			if(hidPROPERTY_DAMAGED_ID.Value!="")
				objPartyDetailsInfo.PROP_DAMAGED_ID = Convert.ToInt32(hidPROPERTY_DAMAGED_ID.Value);

            // Added by santosh kumar gautam on 19 Jan 2011
            if (cmbMARITAL_STATUS.SelectedValue != "")
                objPartyDetailsInfo.MARITAL_STATUS = Convert.ToInt32(cmbMARITAL_STATUS.SelectedValue);

            if (cmbGENDER.SelectedValue != "")
                objPartyDetailsInfo.GENDER = Convert.ToInt32(cmbGENDER.SelectedValue);

            if (cmbPARTY_TYPE.SelectedValue != "")
                objPartyDetailsInfo.PARTY_TYPE = Convert.ToInt32(cmbPARTY_TYPE.SelectedValue);

            objPartyDetailsInfo.DISTRICT = txtDISTRICT.Text.Trim();
            objPartyDetailsInfo.REGIONAL_ID = txtREGIONAL_ID.Text.Trim();
            objPartyDetailsInfo.BANK_BRANCH = txtBANK_BRANCH.Text.Trim();
            objPartyDetailsInfo.BANK_NUMBER = txtBANK_NUMBER.Text.Trim();
            objPartyDetailsInfo.REGIONAL_ID_ISSUANCE = txtREGIONAL_ID_ISSUANCE.Text.Trim();

            if (txtREGIONAL_ID_ISSUE_DATE.Text != "")
                objPartyDetailsInfo.REGIONAL_ID_ISSUE_DATE = ConvertToDate(txtREGIONAL_ID_ISSUE_DATE.Text.Trim());


            objPartyDetailsInfo.PARTY_CPF_CNPJ = txtPARTY_CPF_CNPJ.Text.Trim();

            if (chkIS_BENEFICIARY.Checked == true)
            {
                objPartyDetailsInfo.IS_BENEFICIARY = "Y";
            }
            else
            {
                objPartyDetailsInfo.IS_BENEFICIARY = "N";
            }

            
			//Sub-Adjuster Details being fetched
			if(objPartyDetailsInfo.PARTY_TYPE_ID==12)//When the party type chosen is adjuster
			{
				objPartyDetailsInfo.SUB_ADJUSTER=	txtSUB_ADJUSTER.Text;
				objPartyDetailsInfo.SUB_ADJUSTER_CONTACT_NAME = txtSUB_ADJUSTER_CONTACT_NAME.Text.Trim();
				objPartyDetailsInfo.SA_ADDRESS1 =	txtSA_ADDRESS1.Text; 
				objPartyDetailsInfo.SA_ADDRESS2 =	txtSA_ADDRESS2.Text;
				objPartyDetailsInfo.SA_CITY =	txtSA_CITY.Text;

				if(cmbSA_COUNTRY.SelectedItem!=null && cmbSA_COUNTRY.SelectedItem.Value!="")
					objPartyDetailsInfo.SA_COUNTRY =	cmbSA_COUNTRY.SelectedValue;

				if(cmbSA_STATE.SelectedItem!=null && cmbSA_STATE.SelectedItem.Value!="")
					objPartyDetailsInfo.SA_STATE =	int.Parse(cmbSA_STATE.SelectedValue.ToString());
					
				objPartyDetailsInfo.SA_ZIPCODE  =	txtSA_ZIPCODE.Text; 
				objPartyDetailsInfo.SA_PHONE =	txtSA_PHONE.Text;
				objPartyDetailsInfo.SA_FAX =	txtSA_FAX.Text;
			}
            
			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidPARTY_ID.Value;
			oldXML			= hidOldData.Value;
			//Returning the model object

			return objPartyDetailsInfo;
          
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
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	//For retreiving the return value of business class save function				
				objPartyDetails = new  ClsAddPartyDetails();

				//Retreiving the form values into model class object
				ClsAddPartyDetailsInfo objPartyDetailsInfo = new ClsAddPartyDetailsInfo();	
				objPartyDetailsInfo = GetFormValue();
				objPartyDetailsInfo.CLAIM_ID = int.Parse(gStrClaimID);
				objPartyDetailsInfo.PARTY_ID = int.Parse(hidPARTY_ID.Value);
				objPartyDetailsInfo.MODIFIED_BY = int.Parse(GetUserId());
                objPartyDetailsInfo.LAST_UPDATED_DATETIME = ConvertToDate(DateTime.Now.ToString()); ;
                //btnActivateDeactivate.Visible=true;
				if(hidIS_ACTIVE.Value.ToUpper().Equals("Y"))
				{
					//Calling the add method of business layer class
					intRetVal = objPartyDetails.ActivateDeactivate(objPartyDetailsInfo,"N");					
					if(intRetVal>0)
                    {
                        btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1314");
						lblMessage.Text		= ClsMessages.GetMessage(base.ScreenId,"41");
						hidFormSaved.Value	= "1";
                        hidOldData.Value = ClsAddPartyDetails.GetXmlForPageControls(hidPARTY_ID.Value, gStrClaimID, int.Parse(GetLanguageID()));
                        string IS_BENEFICIARY = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("IS_BENEFICIARY", hidOldData.Value);
                        if (IS_BENEFICIARY.Trim() == "Y")
                            chkIS_BENEFICIARY.Checked = true;
                        else
                            chkIS_BENEFICIARY.Checked = false;
                        string strRegionalDate = ClsCommon.FetchValueFromXML("REGIONAL_ID_ISSUE_DATE", hidOldData.Value.ToString());
                        if(!string.IsNullOrEmpty( strRegionalDate))
                            txtREGIONAL_ID_ISSUE_DATE.Text = strRegionalDate;

						setEncryptXml();
						hidIS_ACTIVE.Value	= "N";
					}					
					else
                    {
                        btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1315");
						lblMessage.Text		= ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value	= "2";
					}				
				}
				else					
				{
					//Calling the add method of business layer class
					intRetVal = objPartyDetails.ActivateDeactivate(objPartyDetailsInfo,"Y");					
					if(intRetVal>0)
                    {
                        btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1315");
						lblMessage.Text		= ClsMessages.GetMessage(base.ScreenId,"40");
						hidFormSaved.Value	= "1";
                        hidOldData.Value = ClsAddPartyDetails.GetXmlForPageControls(hidPARTY_ID.Value, gStrClaimID, int.Parse(GetLanguageID()));
                        string IS_BENEFICIARY = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("IS_BENEFICIARY", hidOldData.Value);
                        if (IS_BENEFICIARY.Trim() == "Y")
                            chkIS_BENEFICIARY.Checked = true;
                        else
                            chkIS_BENEFICIARY.Checked = false;
                        string strRegionalDate = ClsCommon.FetchValueFromXML("REGIONAL_ID_ISSUE_DATE", hidOldData.Value.ToString());
                        if (!string.IsNullOrEmpty(strRegionalDate))
                            txtREGIONAL_ID_ISSUE_DATE.Text = strRegionalDate;
						setEncryptXml();
						hidIS_ACTIVE.Value	= "Y";
					}					
					else
                    {
                        btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1314");
						lblMessage.Text		= ClsMessages.GetMessage(base.ScreenId,"20");
                        hidFormSaved.Value = "2";
					}					
				}
					lblMessage.Visible = true;
			}
			catch(Exception ex)
			{
				lblMessage.Text		=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
				ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objPartyDetails!= null)
					objPartyDetails.Dispose();
			}
		}


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
                int intRetVal = 0;	//For retreiving the return value of business class save function
                objPartyDetails = new ClsAddPartyDetails();
                string NewExpertServiceId = "0";

                //Retreiving the form values into model class object
                ClsAddPartyDetailsInfo objPartyDetailsInfo = GetFormValue();

                if (strRowId.ToUpper().Equals("NEW")) //save case
                {
                    objPartyDetailsInfo.CREATED_BY = objPartyDetailsInfo.MODIFIED_BY = int.Parse(GetUserId());
                    objPartyDetailsInfo.CREATED_DATETIME = objPartyDetailsInfo.LAST_UPDATED_DATETIME = ConvertToDate(DateTime.Now.ToString());

                    //					if(objPartyDetailsInfo.PARTY_TYPE_ID==int.Parse(((int)enumClaimPartyTypes.EXPERTSERVICEPROVIDERS).ToString()))
                    //					{
                    //						intRetVal = AddUpdateExpertData();
                    //					}				

                    //Calling the add method of business layer class
                    //Added 28 sep 2007



                    if (hidPROPERTY_DAMAGED_ID.Value != "0")
                        intRetVal = objPartyDetails.AddPartiesPropDamage(objPartyDetailsInfo, hidEXPERT_SERVICE_TYPE.Value, hidEXPERT_SERVICE_ID.Value, out NewExpertServiceId);
                    else
                        intRetVal = objPartyDetails.Add(objPartyDetailsInfo, hidEXPERT_SERVICE_TYPE.Value, hidEXPERT_SERVICE_ID.Value, out NewExpertServiceId);



                    if (intRetVal > 0)
                    {
                        if (NewExpertServiceId != "0")
                            hidEXPERT_SERVICE_ID.Value = NewExpertServiceId;
                        hidPARTY_ID.Value = objPartyDetailsInfo.PARTY_ID.ToString();
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("29");
                        hidFormSaved.Value = "1";
                        hidOldData.Value = ClsAddPartyDetails.GetXmlForPageControls(hidPARTY_ID.Value, gStrClaimID, int.Parse(GetLanguageID()));
                        string IS_BENEFICIARY = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("IS_BENEFICIARY", hidOldData.Value);
                        if (IS_BENEFICIARY.Trim() == "Y")
                            chkIS_BENEFICIARY.Checked = true;
                        else
                            chkIS_BENEFICIARY.Checked = false;
                        string strRegionalDate = ClsCommon.FetchValueFromXML("REGIONAL_ID_ISSUE_DATE", hidOldData.Value.ToString());
                        if (!string.IsNullOrEmpty(strRegionalDate))
                            txtREGIONAL_ID_ISSUE_DATE.Text = strRegionalDate;
                        setEncryptXml();
                        hidIS_ACTIVE.Value = "Y";
                        txtVENDOR_CODE.Enabled = false;

                    }
                    else if (intRetVal == -1)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "18");
                        hidFormSaved.Value = "2";
                        txtVENDOR_CODE.Enabled = true;
                    }
                    //					else if(intRetVal == -2)
                    //					{
                    //						lblMessage.Text		= "Vendor code for expert service provider must be unique";
                    //						hidFormSaved.Value	= "2";
                    //					}
                    else if (intRetVal == -2)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "838");
                        hidFormSaved.Value = "2";
                        txtVENDOR_CODE.Enabled = false;
                    }
                    else if (intRetVal == -3)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "34");
                        hidFormSaved.Value = "2";
                        txtVENDOR_CODE.Enabled = false;
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "20");
                        hidFormSaved.Value = "2";
                        txtVENDOR_CODE.Enabled = true;
                    }


                    lblMessage.Visible = true;
                } // end save case
                else //UPDATE CASE
                {
                    txtVENDOR_CODE.Enabled = false;
                    //Creating the Model object for holding the Old data
                    ClsAddPartyDetailsInfo objOldPartyDetailsInfo;
                    objOldPartyDetailsInfo = new ClsAddPartyDetailsInfo();
                    //Setting  the Old Page details(XML File containing old details) into the Model Object
                    base.PopulateModelObject(objOldPartyDetailsInfo, hidOldData.Value);
                    //Done for Itrack Issue 6932(Attachment 4 - Point 1)
                    objOldPartyDetailsInfo.MASTER_VENDOR_CODE = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("MASTER_VENDOR_CODE_ID", hidOldData.Value);
                    if (Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("PARENT_ADJUSTER_ID", hidOldData.Value) != "")
                        objOldPartyDetailsInfo.PARENT_ADJUSTER = int.Parse(Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("PARENT_ADJUSTER_ID", hidOldData.Value));

                    //Setting those values into the Model object which are not in the page					
                    objPartyDetailsInfo.CREATED_BY = objPartyDetailsInfo.MODIFIED_BY = int.Parse(GetUserId());
                    objPartyDetailsInfo.CREATED_DATETIME = objPartyDetailsInfo.LAST_UPDATED_DATETIME = DateTime.Now;

                    //Updating the record using business layer class object
                    //Added Sep 2007
                    if (hidPROPERTY_DAMAGED_ID.Value != "0")
                        intRetVal = objPartyDetails.UpdatePartiesPropDamage(objOldPartyDetailsInfo, objPartyDetailsInfo, hidEXPERT_SERVICE_TYPE.Value, hidEXPERT_SERVICE_ID.Value, out NewExpertServiceId);
                    else
                        intRetVal = objPartyDetails.Update(objOldPartyDetailsInfo, objPartyDetailsInfo, hidEXPERT_SERVICE_TYPE.Value, hidEXPERT_SERVICE_ID.Value, out NewExpertServiceId);

                    if (intRetVal > 0)			// update successfully performed
                    {
                        //						if(objPartyDetailsInfo.PARTY_TYPE_ID==int.Parse(((int)enumClaimPartyTypes.EXPERTSERVICEPROVIDERS).ToString()))
                        //						{
                        //							AddUpdateExpertData();
                        //						}
                        if (NewExpertServiceId != "0")
                            hidEXPERT_SERVICE_ID.Value = NewExpertServiceId;
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("31");
                        hidFormSaved.Value = "1";
                        hidOldData.Value = ClsAddPartyDetails.GetXmlForPageControls(hidPARTY_ID.Value, gStrClaimID, int.Parse(GetLanguageID()));
                        string IS_BENEFICIARY = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("IS_BENEFICIARY", hidOldData.Value);
                        if (IS_BENEFICIARY.Trim() == "Y")
                            chkIS_BENEFICIARY.Checked = true;
                        else
                            chkIS_BENEFICIARY.Checked = false;
                        string strRegionalDate = ClsCommon.FetchValueFromXML("REGIONAL_ID_ISSUE_DATE", hidOldData.Value.ToString());
                        if (!string.IsNullOrEmpty(strRegionalDate))
                            txtREGIONAL_ID_ISSUE_DATE.Text = strRegionalDate;
                        setEncryptXml();

                    }
                    else if (intRetVal == -1)	// Duplicate code exist, update failed
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "18");
                        hidFormSaved.Value = "2";
                    }
                    //					else if(intRetVal == -2)
                    //					{
                    //						lblMessage.Text		= "Vendor code for expert service provider must be unique";
                    //						hidFormSaved.Value	= "2";
                    //					}
                    else if (intRetVal == -2)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "838");
                        hidFormSaved.Value = "2";
                    }

                    else
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "20");
                        hidFormSaved.Value = "2";
                    }
                    lblMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";
                ExceptionManager.Publish(ex);
                txtVENDOR_CODE.Enabled = true;
            }
            finally
            {
                if (objPartyDetails != null)
                    objPartyDetails.Dispose();
            }


        }
		#endregion

		

		private void SetCaptions()
		{
			capEXPERT_SERVICE_TYPE_DESC.Text =objResourceMgr.GetString("txtEXPERT_SERVICE_TYPE_DESC");
			capEXPERT_SERVICE_TYPE.Text		=objResourceMgr.GetString("cmbEXPERT_SERVICE_TYPE");
			capNAME.Text			=	objResourceMgr.GetString("txtNAME");
			capADDRESS1.Text		=	objResourceMgr.GetString("txtADDRESS1");
			capADDRESS2.Text		=	objResourceMgr.GetString("txtADDRESS2");
			capCITY.Text			=	objResourceMgr.GetString("txtCITY");
			capSTATE.Text			=	objResourceMgr.GetString("cmbSTATE");
			capZIP.Text				=	objResourceMgr.GetString("txtZIP");
			capCONTACT_PHONE.Text	=	objResourceMgr.GetString("txtCONTACT_PHONE");
			capCONTACT_EMAIL.Text	=	objResourceMgr.GetString("txtCONTACT_EMAIL");
			capOTHER_DETAILS.Text	=	objResourceMgr.GetString("txtOTHER_DETAILS");
			capPARTY_TYPE_ID.Text	=	objResourceMgr.GetString("cmbPARTY_TYPE_ID");
			capCOUNTRY.Text			=	objResourceMgr.GetString("cmbCOUNTRY");
			capEXTENT_OF_INJURY.Text=	objResourceMgr.GetString("txtEXTENT_OF_INJURY");
			capAGE.Text				=	objResourceMgr.GetString("txtAGE");
			capPARTY_DETAIL.Text	=	objResourceMgr.GetString("cmbPARTY_DETAIL");
//			capREFERENCE.Text = objResourceMgr.GetString("txtREFERENCE");
			capACCOUNT_NUMBER.Text = objResourceMgr.GetString("txtACCOUNT_NUMBER");
			capBANK_NAME.Text = objResourceMgr.GetString("txtBANK_NAME");
            // Added by Santosh Kumar Gautam on 15 Nov 2010
            capAGENCY_BANK.Text = objResourceMgr.GetString("txtAGENCY_BANK");
			capACCOUNT_NAME.Text = objResourceMgr.GetString("txtACCOUNT_NAME");
			capPARTY_TYPE_DESC.Text = objResourceMgr.GetString("txtPARTY_TYPE_DESC");
			
			capCONTACT_PHONE_EXT.Text = objResourceMgr.GetString("txtCONTACT_PHONE_EXT");
			capCONTACT_FAX.Text = objResourceMgr.GetString("txtCONTACT_FAX");
			capPARENT_ADJUSTER.Text			=		objResourceMgr.GetString("txtPARENT_ADJUSTER");
			capFEDRERAL_ID.Text		=		objResourceMgr.GetString("txtFEDRERAL_ID");

			capMASTER_VENDOR_CODE.Text		=		objResourceMgr.GetString("txtMASTER_VENDOR_CODE");
			capVENDOR_CODE.Text		=		objResourceMgr.GetString("txtVENDOR_CODE");
			capPROCESSING_OPTION_1099.Text		=		objResourceMgr.GetString("cmbPROCESSING_OPTION_1099");
			capCONTACT_NAME.Text		=		objResourceMgr.GetString("txtCONTACT_NAME");
			capSUB_ADJUSTER_CONTACT_NAME.Text			=		objResourceMgr.GetString("txtSUB_ADJUSTER_CONTACT_NAME");
			capSA_ADDRESS1.Text							=		objResourceMgr.GetString("txtSA_ADDRESS1");
			capSA_ADDRESS2.Text							=		objResourceMgr.GetString("txtSA_ADDRESS2");
			capSA_CITY.Text								=		objResourceMgr.GetString("txtSA_CITY");
			capSA_COUNTRY.Text							=		objResourceMgr.GetString("cmbSA_COUNTRY");
			capSA_STATE.Text							=		objResourceMgr.GetString("cmbSA_STATE");
			capSA_ZIPCODE.Text							=		objResourceMgr.GetString("txtSA_ZIPCODE");
			capSA_PHONE.Text							=		objResourceMgr.GetString("txtSA_PHONE");
			capSA_FAX.Text								=		objResourceMgr.GetString("txtSA_FAX");
			capSUB_ADJUSTER.Text						=		objResourceMgr.GetString("txtSUB_ADJUSTER");
			
			capMAIL_1099_ADD1.Text				=		objResourceMgr.GetString("txtMAIL_1099_ADD1");
			capMAIL_1099_ADD2.Text				=		objResourceMgr.GetString("txtMAIL_1099_ADD2");
			capMAIL_1099_CITY.Text				=		objResourceMgr.GetString("txtMAIL_1099_CITY");
			capMAIL_1099_STATE.Text				=		objResourceMgr.GetString("txtMAIL_1099_STATE");
			capMAIL_1099_COUNTRY.Text			=		objResourceMgr.GetString("txtMAIL_1099_COUNTRY");
			capMAIL_1099_ZIP.Text				=		objResourceMgr.GetString("txtMAIL_1099_ZIP");
			capMAIL_1099_NAME.Text				=		objResourceMgr.GetString("txtMAIL_1099_NAME");
			capW9_FORM.Text			=		objResourceMgr.GetString("txtW9_FORM");
            chkCopyData.Text = objResourceMgr.GetString("chkCopyData");
            capAdd.Text = objResourceMgr.GetString("capAdd");

            capACCOUNT_TYPE.Text = objResourceMgr.GetString("cmbACCOUNT_TYPE");

            capPARTY_TYPE.Text = objResourceMgr.GetString("cmbPARTY_TYPE");
            capMARITAL_STATUS.Text = objResourceMgr.GetString("cmbMARITAL_STATUS");
            capGENDER.Text = objResourceMgr.GetString("cmbGENDER");
            capREGIONAL_ID.Text = objResourceMgr.GetString("txtREGIONAL_ID");

            capDISTRICT.Text = objResourceMgr.GetString("txtDISTRICT");
            capREGIONAL_ID_ISSUANCE.Text = objResourceMgr.GetString("txtREGIONAL_ID_ISSUANCE");
            capREGIONAL_ID_ISSUE_DATE.Text = objResourceMgr.GetString("txtREGIONAL_ID_ISSUE_DATE");

             capBANK_BRANCH.Text = objResourceMgr.GetString("txtBANK_BRANCH");
             capBANK_NUMBER.Text = objResourceMgr.GetString("txtBANK_NUMBER");
             capPAYMENT_METHOD.Text = objResourceMgr.GetString("cmbPAYMENT_METHOD");
             capPARTY_CPF_CNPJ.Text = objResourceMgr.GetString("txtPARTY_CPF_CNPJ");
             capIS_BENEFICIARY.Text = objResourceMgr.GetString("chkIS_BENEFICIARY");
            //shikha
             CapPARTY_PERCENTAGE.Text = objResourceMgr.GetString("txtPARTY_PERCENTAGE");
             hidHeader.Value = objResourceMgr.GetString("hidHeader");
		}		
	}
}
