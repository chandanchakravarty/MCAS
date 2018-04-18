/******************************************************************************************
<Author					: -   Vijay Joshi
<Start Date				: -	6/9/2005 12:30:10 PM
<End Date				: -	
<Description			: - 	Page class for JournalEntryDetail screen
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - Class for showing Joural entry detail screen.
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
using Cms.CmsWeb;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.Model.Account;
using Cms.BusinessLayer.BlAccount;
using Cms.CmsWeb.Controls;

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for AddJournalEntryDetail.
	/// </summary>
	public class AddJournalEntryDetail : Cms.Account.AccountBase
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capAMOUNT;
		protected System.Web.UI.WebControls.TextBox txtAMOUNT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAMOUNT;
		protected System.Web.UI.WebControls.Label capTYPE;
		protected System.Web.UI.WebControls.DropDownList cmbTYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTYPE;
		protected System.Web.UI.WebControls.Label capREF_CUSTOMER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREF_CUSTOMER;
		protected System.Web.UI.WebControls.Label capREGARDING;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREGARDING;
		protected System.Web.UI.WebControls.Label capPOLICY_ID;
		protected System.Web.UI.WebControls.DropDownList cmbPOLICY_NUMBER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPOLICY_ID;
		protected System.Web.UI.WebControls.Label capACCOUNT_ID;
		protected System.Web.UI.WebControls.DropDownList cmbACCOUNT_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvACCOUNT_ID;
		protected System.Web.UI.WebControls.Label capDEPT_ID;
		protected System.Web.UI.WebControls.DropDownList cmbDEPT_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDEPT_ID;
		protected System.Web.UI.WebControls.Label capPC_ID;
		protected System.Web.UI.WebControls.DropDownList cmbPC_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPC_ID;
		protected System.Web.UI.WebControls.Label capNOTE;
		protected System.Web.UI.WebControls.TextBox txtNOTE;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidJE_LINE_ITEM_ID;
		protected System.Web.UI.WebControls.CustomValidator csvDESCRIPTION;
		string url;
		public string strACC_ID="";
		protected Cms.WebControls.AjaxLookup txtREF_CUSTOMER_NAME;

		#region Local form variables
		//START:*********** Local form variables *************
		//string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId;//, strFormSaved;
		//private int	intLoggedInUserID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidJOURNAL_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDeptXML;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPC_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidGLAccountXML;
		protected System.Web.UI.WebControls.Label lblACC_DESC;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		//protected System.Web.UI.WebControls.TextBox txtREF_CUSTOMER_NAME_123;
		protected System.Web.UI.HtmlControls.HtmlImage imgSelect;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidREF_CUSTOMER;
		protected System.Web.UI.WebControls.TextBox txtOTHER_REGARDING;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvOTHER_REGARDING;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidJournalInfoXML;
		protected System.Web.UI.HtmlControls.HtmlImage imgREGARDING;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidREGARDING;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnReferenceCustomerMan;
		protected System.Web.UI.WebControls.CustomValidator csvAMOUNT;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		protected System.Web.UI.WebControls.TextBox txtREF_CUSTOMER_NAME_123;
		protected System.Web.UI.WebControls.TextBox txtREGARDING_NAME;
		protected System.Web.UI.WebControls.TextBox txtPOLICY_NUMBER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPOLICY_NUMBER;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_NUMBER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID_NAME;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_APP_NUMBER;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidMergeId;
		protected System.Web.UI.HtmlControls.HtmlTableRow trPageHeader;
		protected System.Web.UI.WebControls.TextBox txtAccount_ID;
		protected System.Web.UI.WebControls.Label capTRAN_CODE;
		protected System.Web.UI.WebControls.DropDownList cmbTRAN_CODE;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnTRAN_CODE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTRAN_CODE;
				
		//Defining the business layer class object
		ClsJournalEntryDetail  objJournalEntryDetail ;
		//END:*********** Local variables *************

		#endregion

		#region Set Validators ErrorMessages
		private void SetErrorMessages()
		{
			rfvAMOUNT.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			rfvTYPE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			rfvREF_CUSTOMER.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
			rfvREGARDING.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"9");
			rfvOTHER_REGARDING.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
			rfvPOLICY_ID.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
			rfvACCOUNT_ID.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6");
			rfvDEPT_ID.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");
			rfvPC_ID.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"8");
			csvAMOUNT.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"116");
			rfvTRAN_CODE.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"12");
			this.csvDESCRIPTION.ErrorMessage =		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("441");

		}
		#endregion

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			Ajax.Utility.RegisterTypeForAjax(typeof(AddJournalEntryDetail));
					
			url = ClsCommon.GetLookupURL();			
			imgSelect.Attributes.Add("onclick","javascript:OpenLookupWithFunction('" + url + "','CUSTOMER_ID','Name','hidREF_CUSTOMER','txtREF_CUSTOMER_NAME','CustLookupForm','Customer','','splitPolicy()');");


			//btnReset.Attributes.Add("onclick","javascript:return ResetForm('ACT_JOURNAL_LINE_ITEMS');");
			btnReset.Attributes.Add("onclick","javascript:return ResetForm();");
			//txtAMOUNT.Attributes.Add("onBlur","FormatDollar('ACT_JOURNAL_LINE_ITEMS',this.name,this.value,',');");					
			txtAMOUNT.Attributes.Add("onBlur","FormatAmount(document.getElementById('txtAMOUNT'));");
			btnDelete.Attributes.Add("OnClick","javascript:return OnDeleteClick();");
			
			//Setting the screen id
			SetScreenId();

			lblMessage.Visible = false;
			SetErrorMessages();

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	CmsButtonType.Write;
			btnReset.PermissionString	=	gstrSecurityXML;

			btnSave.CmsButtonClass	=	CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;

			btnDelete.CmsButtonClass	= CmsButtonType.Delete;
			btnDelete.PermissionString	= gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Account.Aspx.AddJournalEntryDetail" ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{
				cmbTYPE.SelectedValue = "OTH";
				GetQueryString();
				GetOldDataXML();
				FillCombos();
				SetCaptions();				
			}			
			Ajax.Utility.RegisterTypeForAjax(typeof (AccountBase));
			
		}//end pageload
		#endregion
		
		#region GetFormValue
		private ClsJournalEntryDetailInfo GetFormValue()
		{
			//Creating the Model object for holding the New data4
			ClsJournalEntryDetailInfo objJournalEntryDetailInfo;
			objJournalEntryDetailInfo = new ClsJournalEntryDetailInfo();			
			
			//if(cmbTYPE.SelectedValue.ToString().ToUpper() == "CUS" ||cmbTYPE.SelectedValue.ToString().ToUpper() == "VEN")
			// Commented by Rajan, as this was setting ref customer id in case of vendor which is not applicable.
			// Remmoved Vendor Condition
			if(cmbTYPE.SelectedValue.ToString().ToUpper() == "CUS")
			{
				if(hidREGARDING.Value != "")	
				{
					objJournalEntryDetailInfo.REF_CUSTOMER = int.Parse(hidREGARDING.Value);		
					objJournalEntryDetailInfo.CUSTOMER_ID = int.Parse(hidREGARDING.Value);		
				}

			}
			else
			{
				//if(txtREF_CUSTOMER_NAME.DataValue != "")	
				if(hidREF_CUSTOMER.Value != "")	
				{
					//objJournalEntryDetailInfo.REF_CUSTOMER = int.Parse(txtREF_CUSTOMER_NAME.DataValue);
					objJournalEntryDetailInfo.REF_CUSTOMER = int.Parse(hidREF_CUSTOMER.Value);		
					objJournalEntryDetailInfo.CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);		
				}
			}
			

			objJournalEntryDetailInfo.JOURNAL_ID = int.Parse(hidJOURNAL_ID.Value);

			if (cmbDEPT_ID.SelectedValue != null && cmbDEPT_ID.SelectedValue.Trim() != "")
				objJournalEntryDetailInfo.DEPT_ID = int.Parse(cmbDEPT_ID.SelectedValue);

			if (hidPC_ID.Value != "" )
				objJournalEntryDetailInfo.PC_ID = int.Parse(hidPC_ID.Value);


			

			if (txtAMOUNT.Text.Trim() != "")
				objJournalEntryDetailInfo.AMOUNT = Double.Parse(txtAMOUNT.Text);

			if (txtPOLICY_NUMBER.Text.Trim() != "")
				objJournalEntryDetailInfo.POLICY_NUMBER = txtPOLICY_NUMBER.Text.ToUpper();

			objJournalEntryDetailInfo.TYPE = cmbTYPE.SelectedValue;

			if (cmbTYPE.SelectedValue == "OTH")
			{
				if (cmbACCOUNT_ID.SelectedValue != null && cmbACCOUNT_ID.SelectedValue.Trim() != "")
					objJournalEntryDetailInfo.ACCOUNT_ID = int.Parse(cmbACCOUNT_ID.SelectedValue);

				objJournalEntryDetailInfo.REGARDING = txtOTHER_REGARDING.Text;
			}
			else
			{
				objJournalEntryDetailInfo.REGARDING = hidREGARDING.Value;
				switch(cmbTYPE.SelectedValue.ToUpper())
				{
					case "AGN":
						if(cmbTRAN_CODE.SelectedIndex > 0)
							objJournalEntryDetailInfo.TRAN_CODE = cmbTRAN_CODE.SelectedItem.Value;
						cmbTRAN_CODE_SelectedIndexChanged(null,null);
						objJournalEntryDetailInfo.ACCOUNT_ID =int.Parse(strACC_ID.ToString());
						if (hidPOLICY_ID.Value != null && hidPOLICY_ID.Value.ToString().Trim() != "")
						{
							objJournalEntryDetailInfo.POLICY_ID = int.Parse(hidPOLICY_ID.Value);
							objJournalEntryDetailInfo.POLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value);

						}

						break;
					case "CUS":
						if(cmbTRAN_CODE.SelectedIndex > 0)
							objJournalEntryDetailInfo.TRAN_CODE = cmbTRAN_CODE.SelectedItem.Value;
						cmbTRAN_CODE_SelectedIndexChanged(null,null);
						objJournalEntryDetailInfo.ACCOUNT_ID =int.Parse(strACC_ID.ToString());
						if (hidPOLICY_ID.Value != null && hidPOLICY_ID.Value.ToString().Trim() != "")
						{
							objJournalEntryDetailInfo.POLICY_ID = int.Parse(hidPOLICY_ID.Value);
							objJournalEntryDetailInfo.POLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value);

						}

						break;
					/* case "MOR":
						objJournalEntryDetailInfo.ACCOUNT_ID = int.Parse(GetValueFromGLAccountXML("MORTGAGE_ACCOUNT_ID"));
						if (hidPOLICY_ID.Value != null && hidPOLICY_ID.Value.ToString().Trim() != "")
						{
							objJournalEntryDetailInfo.POLICY_ID = int.Parse(hidPOLICY_ID.Value);
							objJournalEntryDetailInfo.POLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value);

						}

						break;
					case "TAX":
						if (cmbACCOUNT_ID.SelectedValue != null && cmbACCOUNT_ID.SelectedValue.Trim() != "")
							objJournalEntryDetailInfo.ACCOUNT_ID = int.Parse(cmbACCOUNT_ID.SelectedValue);

						break;
						*/

					case "VEN":
						objJournalEntryDetailInfo.ACCOUNT_ID = int.Parse(GetValueFromGLAccountXML("VENDOR_ACCOUNT_ID"));
						break;	
				}
			}

			objJournalEntryDetailInfo.NOTE = txtNOTE.Text;

			//These  assignments are common to all pages.
			strRowId		=	hidJE_LINE_ITEM_ID.Value;
			
			//Returning the model object
			return objJournalEntryDetailInfo;
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
			this.cmbTYPE.SelectedIndexChanged += new System.EventHandler(this.cmbTYPE_SelectedIndexChanged);
			this.cmbTRAN_CODE.SelectedIndexChanged += new System.EventHandler(this.cmbTRAN_CODE_SelectedIndexChanged);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region "Web Event Handlers"
	
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal=0;	//For retreiving the return value of business class save function
				objJournalEntryDetail = new  ClsJournalEntryDetail();

				//Retreiving the form values into model class object
				ClsJournalEntryDetailInfo objJournalEntryDetailInfo = GetFormValue();
				

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objJournalEntryDetailInfo.CREATED_BY = int.Parse(GetUserId());
					objJournalEntryDetailInfo.CREATED_DATETIME = DateTime.Now;

					intRetVal = objJournalEntryDetail.Add(objJournalEntryDetailInfo);

					if(intRetVal>0)
					{
						hidJE_LINE_ITEM_ID.Value = objJournalEntryDetailInfo.JE_LINE_ITEM_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						GetOldDataXML();
						GetJournalEntryMasterXML();
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"18");
						hidFormSaved.Value			=		"2";
					}
					else if(intRetVal == -5)
					{
						lblMessage.Text				=		"Policy entered is of AB type, Please enter DB policy.";
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
					ClsJournalEntryDetailInfo objOldJournalEntryDetailInfo;
					objOldJournalEntryDetailInfo = new ClsJournalEntryDetailInfo();
                 //   ClsJournalEntryMaster.GetMaxEntryNo().ToString();
					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldJournalEntryDetailInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objJournalEntryDetailInfo.JE_LINE_ITEM_ID = int.Parse(strRowId);
					objJournalEntryDetailInfo.MODIFIED_BY = int.Parse(GetUserId());
					objJournalEntryDetailInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					objJournalEntryDetailInfo.IS_ACTIVE = "Y";				

					//Updating the record using business layer class object
					intRetVal	= objJournalEntryDetail.Update(objOldJournalEntryDetailInfo,objJournalEntryDetailInfo);

					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						GetOldDataXML();
						GetJournalEntryMasterXML();
					}
					else if(intRetVal == -2)
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"10");
						hidFormSaved.Value		=	"2";
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"18");
						hidFormSaved.Value		=	"2";
					}
					else if(intRetVal == -5)
					{
						lblMessage.Text				=		"Policy entered is of AB type, Please enter DB policy.";
						hidFormSaved.Value			=		"2";
					}
					else if(intRetVal == -8) //added 24 JAN 08
					{
						lblMessage.Text				=		"Policy Number does not belong to selected Agency.";
						hidFormSaved.Value			=		"2";
					}
					else if(intRetVal == -7) //added 24 JAN 08
					{
						lblMessage.Text				=		"Policy Number does not belong to selected Customer.";
						hidFormSaved.Value			=		"2";
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value		=	"2";
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
				ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objJournalEntryDetail!= null)
					objJournalEntryDetail.Dispose();
			}
		}

		
		#endregion

		private void SetCaptions()
		{
			capDEPT_ID.Text		= objResourceMgr.GetString("cmbDEPT_ID");
			//capPC_ID.Text		= objResourceMgr.GetString("cmbPC");
			capPC_ID.Text		= objResourceMgr.GetString("cmbPC_ID");
			capPOLICY_ID.Text	= objResourceMgr.GetString("cmbPOLICY_NUMBER");
			capAMOUNT.Text		= objResourceMgr.GetString("txtAMOUNT");
			capTYPE.Text		= objResourceMgr.GetString("cmbTYPE");
			capREGARDING.Text	= objResourceMgr.GetString("txtREGARDING_NAME");
			capREF_CUSTOMER.Text= objResourceMgr.GetString("cmbREF_CUSTOMER");
			capACCOUNT_ID.Text	= objResourceMgr.GetString("cmbACCOUNT_ID");
			capNOTE.Text		= objResourceMgr.GetString("txtNOTE");
			capTRAN_CODE.Text	= objResourceMgr.GetString("cmbTRAN_CODE");
		}

	
		private void GetOldDataXML()
		{
			if (hidJE_LINE_ITEM_ID.Value != "" && hidJE_LINE_ITEM_ID.Value.ToString().ToUpper() != "NEW" )
			{
				hidOldData.Value = ClsJournalEntryDetail.GetJournalEntryDetailInfo(int.Parse(hidJE_LINE_ITEM_ID.Value));
			}
		}
		
		private void GetJournalEntryMasterXML()
		{
			//storing the XML of master journal entry also
			//This xml will passed to parent docuemnt for refreshing its header band
			hidJournalInfoXML.Value = ClsJournalEntryMaster.GetJournalEntryInfo(int.Parse(hidJOURNAL_ID.Value));
		}

	
		private void GetQueryString()
		{
			hidJOURNAL_ID.Value		= Request.Params["JOURNAL_ID"];
			hidJE_LINE_ITEM_ID.Value = Request.Params["JE_LINE_ITEM_ID"];
			if(Request.QueryString["CalledFrom"]!=null && Request.QueryString["CalledFrom"].ToString()!="")
				hidCalledFrom.Value = Request.QueryString["CalledFrom"].ToString().ToUpper();
		}

	
		private void FillCombos()
		{
			try
			{
				//Populating the com boxes
				ClsJournalEntryMaster.GetDepartmentDropdown(cmbDEPT_ID,int.Parse(hidJOURNAL_ID.Value));
				hidDeptXML.Value = ClsDepartment.GetXmlForProfitCenterDropDown();

				hidGLAccountXML.Value = ClsJournalEntryMaster.GetGLAccountXML(int.Parse(hidJOURNAL_ID.Value));

				Cms.BusinessLayer.BlCommon.Accounting.ClsGeneralLedger.GetGeneralLedgersIndropDown(cmbACCOUNT_ID);
				//Cms.BusinessLayer.BlCommon.clsc
				if (hidOldData.Value.Trim() != "")
				{
					/*If old XML exists then populating the regarding combobox*/
					/*Which populates on the basis of selected type(cmbTYPE)*/
					/*Hence setting the type and then we will call its selectedIndex change event*/
					/*Which will populates the entries in cmbRegarding drop down*/
					System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
					doc.LoadXml(hidOldData.Value);
					System.Xml.XmlNode node = doc.SelectSingleNode("/NewDataSet/Table/TYPE");
					
					if (node != null)
					{
						if (node.InnerXml.Trim() != "")
						{
							//Selecting the type
							cmbTYPE.SelectedValue = node.InnerXml.Trim().ToUpper();
						}
					}
					doc = null;
				}
				else
				{	
					//Taking the first item as default type
					if (cmbTYPE.Items.Count > 0)
						cmbTYPE.SelectedValue = "AGN";
				}
			
				//Populating the regarding combo 
				if (cmbTYPE.SelectedValue != null)
					ShowRegardingInformation(cmbTYPE.SelectedValue);

				//Populating the Transaction Codes for Agency and customer
				if(cmbTYPE.SelectedValue!= null)
					ShowTransactionCodes(cmbTYPE.SelectedValue);
			}
			catch(Exception objEx)
			{
				lblMessage.Text = objEx.Message.ToString();
				lblMessage.Visible = true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objEx);
			}
		}
		
	
		private void ShowAgencyInfo()
		{
			imgREGARDING.Attributes.Add("onclick","javascript:OpenLookupWithFunction('" + url + "','AGENCY_ID','Name','hidREGARDING','txtREGARDING_NAME','Agency','Agency','');");
		}

		private void ShowCustomerInfo()
		{
			imgREGARDING.Attributes.Add("onclick","javascript:OpenLookupWithFunction('" + url + "','CUSTOMER_ID','Name','hidREGARDING','txtREGARDING_NAME','CustLookupForm','Customer');");
		}

		private void ShowVendorInfo()
		{
			imgREGARDING.Attributes.Add("onclick","javascript:OpenLookup('" + url + "','VENDOR_ID','Name','hidREGARDING','txtREGARDING_NAME','Vendor','Vendor');");
			lblACC_DESC.Text = GetValueFromGLAccountXML("VENDOR_ACCOUNT");
		}

		private void ShowOtherInfo()
		{
			lblACC_DESC.Text = "";

			if (hidGLAccountXML.Value != "")
			{
				Cms.BusinessLayer.BlCommon.Accounting.ClsGlAccounts.GetAccountsInDropdown(cmbACCOUNT_ID,
						int.Parse(GetValueFromGLAccountXML("GL_ID")),
						int.Parse(GetValueFromGLAccountXML("FISCAL_ID")),"Y");
			}
		}
		
		/// <summary>
		/// Returns the value of specified node from hidGLAccountXML
		/// </summary>
		/// <param name="nodeName">Name of node whose value to returnes</param>
		/// <returns>Value of node</returns>
		private string GetValueFromGLAccountXML(string nodeName)
		{
			if (hidGLAccountXML.Value != "")
			{
				System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
				doc.LoadXml(hidGLAccountXML.Value);

				//Retreiving the GL ID and Fiscal Id
				System.Xml.XmlNode objNode = doc.SelectSingleNode("/NewDataSet/Table/" + nodeName );
				
				if(objNode != null)
				{	
					return objNode.InnerXml;
				}
				else
				{
					return "";
				}
			}
			else
			{
				return "";
			}
		}

		/// <summary>
		/// Setting the screen id
		/// </summary>
		private void SetScreenId()
		{
			try
			{
				if (hidJournalInfoXML.Value.Trim() == "")
				{
					GetQueryString();
					GetJournalEntryMasterXML();
				}
				
				System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
				
				xmlDoc.LoadXml(hidJournalInfoXML.Value);
				string strType = ClsCommon.GetNodeValue(xmlDoc, "//JOURNAL_GROUP_TYPE");

				switch(strType.ToUpper())
				{
					case "ML":	//Manual journal entry
						base.ScreenId = "173_1_0";
						break;
					case "RC":	//Recurring journal entry
						base.ScreenId = "175_1_0";
						break;
					case "TP":	//Template journal entry
						base.ScreenId = "174_1_0";
						break;
					case "13":	//13th Period adjustment
						base.ScreenId = "176_1_0";
						break;
				}
			}
			catch(Exception objExp)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
				lblMessage.Text = objExp.Message.ToString();
				lblMessage.Visible = true;
			}
		}
		
		private void ShowRegardingInformation(string strType)
		{
			txtREGARDING_NAME.Text = "";
			hidREGARDING.Value = "";

			//filling the regarding dropdown as per the selected value
			switch(strType.ToUpper())
			{
				case "AGN":
					ShowAgencyInfo();
					rfvREF_CUSTOMER.Enabled = true;
					spnReferenceCustomerMan.Visible = true;
					capREF_CUSTOMER.Text= objResourceMgr.GetString("cmbREF_CUSTOMER");
					capREF_CUSTOMER.Visible = true;
					break;
				case "CUS":
					ShowCustomerInfo();
					rfvREF_CUSTOMER.Enabled = false;
					spnReferenceCustomerMan.Visible = false;
					capREF_CUSTOMER.Text = "Insured Name";
					capREF_CUSTOMER.Visible = false;
					break;
				/*case "TAX":
					ShowTaxInfo();
					rfvREF_CUSTOMER.Enabled = false;
					spnReferenceCustomerMan.Visible = false;
					break;
				*/
				case "VEN":
					ShowVendorInfo();
					rfvREF_CUSTOMER.Enabled = false;
					spnReferenceCustomerMan.Visible = false;
					capREF_CUSTOMER.Text = "Insured Name";
					capREF_CUSTOMER.Visible = false;
					break;
				case "OTH":
					ShowOtherInfo();
					rfvREF_CUSTOMER.Enabled = false;
					spnReferenceCustomerMan.Visible = false;
					capREF_CUSTOMER.Text = "Insured Name";
					capREF_CUSTOMER.Visible = false;
					break;
				/*case "MOR":
					ShowMortgageInfo();
					rfvREF_CUSTOMER.Enabled = true;
					spnReferenceCustomerMan.Visible = true;
					break;
				*/
			}
		}

		private void ShowTransactionCodes(string strType)
		{
			if(strType.Equals("AGN"))
			{
                spnTRAN_CODE.Visible=true;
				capTRAN_CODE.Visible=true;
				cmbTRAN_CODE.Visible=true;
				rfvTRAN_CODE.Enabled=true;
				rfvTRAN_CODE.Visible=true;
				cmbTRAN_CODE.Items.Clear();
				cmbTRAN_CODE.Items.Insert(0,"");
				cmbTRAN_CODE.Items.Insert(1,new ListItem("AB Commission","AB-COMM"));
				cmbTRAN_CODE.Items.Insert(2,new ListItem("AB Premium","AB-PREM"));
				cmbTRAN_CODE.Items.Insert(3,new ListItem("DB Commission","DB-COMM"));
				cmbTRAN_CODE.DataBind();
			}
			else if(strType.Equals("CUS"))
			{
				spnTRAN_CODE.Visible=true;
				capTRAN_CODE.Visible=true;
				cmbTRAN_CODE.Visible=true;
				rfvTRAN_CODE.Enabled=true;
				rfvTRAN_CODE.Visible=true;
				cmbTRAN_CODE.Items.Clear();
				cmbTRAN_CODE.Items.Insert(0,"");
				cmbTRAN_CODE.Items.Insert(1,new ListItem("Billing Fee","BILL-FEE"));
				cmbTRAN_CODE.Items.Insert(2,new ListItem("DB Premium","DB-PREM"));
				cmbTRAN_CODE.Items.Insert(3,new ListItem("Late Fee","LATE-FEE"));
				cmbTRAN_CODE.Items.Insert(4,new ListItem("NSF","NSF"));
				cmbTRAN_CODE.Items.Insert(5,new ListItem("Reinstatement Fee","REINS-FEE"));
				cmbTRAN_CODE.DataBind();
			}
			else
			{	
				spnTRAN_CODE.Visible=false;
				capTRAN_CODE.Visible=false;
				cmbTRAN_CODE.Visible=false;
				rfvTRAN_CODE.Enabled=false;
				rfvTRAN_CODE.Visible=false;
			}
		}


		private void cmbTYPE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			
			try
			{	
				hidFormSaved.Value = "3";

				if (cmbTYPE.SelectedValue == null) 
					return;
			
				ShowRegardingInformation(cmbTYPE.SelectedValue);
				ShowTransactionCodes(cmbTYPE.SelectedValue);
				SetFocus("cmbTYPE");
			}
			catch (Exception objEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objEx);
				lblMessage.Text = objEx.Message.ToString();
				lblMessage.Visible = true;
			}
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				ClsJournalEntryDetailInfo objJournalEntryDetailInfo = GetFormValue();
				objJournalEntryDetailInfo.CREATED_BY = int.Parse(GetUserId());
				/*Deleting the record*/
				objJournalEntryDetail = new ClsJournalEntryDetail();
				int intRetVal = objJournalEntryDetail.Delete(int.Parse(hidJOURNAL_ID.Value), int.Parse(hidJE_LINE_ITEM_ID.Value),objJournalEntryDetailInfo);

				if (intRetVal > 0)
				{
					trPageHeader.Visible = false;
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "127");
					hidJE_LINE_ITEM_ID.Value = "";
					hidFormSaved.Value = "5";
					hidOldData.Value = "";
					GetJournalEntryMasterXML();
				}
				else if(intRetVal == -2)
				{
					lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"11");
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

		private void cmbTRAN_CODE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string TranCode = cmbTRAN_CODE.SelectedItem.Value;
			if (cmbTYPE.SelectedValue == null) 
				return;
			if (cmbTRAN_CODE.SelectedValue == "") 
				return;
			GetAccountIdFromXML(TranCode);
			hidFormSaved.Value = "8"; // Value 8 : To persist add time data on postback of Tran Code Combo.
			SetFocus("cmbTRAN_CODE");
		}	
		private void GetAccountIdFromXML(string TranCode)
		{
			string AccountIdXML = ClsJournalEntryMaster.GetAccountXML(TranCode);
			if (AccountIdXML != "")
			{
				System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
				doc.LoadXml(AccountIdXML);
				System.Xml.XmlNode objNode = doc.SelectSingleNode("/NewDataSet/Table/" + TranCode );
				System.Xml.XmlNode objNodeAccID = doc.SelectSingleNode("/NewDataSet/Table/ACCOUNT_ID");
				
				if(objNode != null)
				{	
					lblACC_DESC.Text = objNode.InnerXml;
					strACC_ID = objNodeAccID.InnerXml;
				}
			}
		}
		[Ajax.AjaxMethod()]
		public string AjaxGetCustomerPolicies(int Customer_ID,int Agency)
		{
			CmsWeb.webservices.ClsWebServiceCommon obj =  new CmsWeb.webservices.ClsWebServiceCommon();
			string result = "";
			result = obj.GetCustomerPolicies(Customer_ID,Agency);
			return result;
			
		}
	}
}
