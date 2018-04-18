/******************************************************************************************
<Author					: -   Ajit Singh Chahal
<Start Date				: -	  6/24/2005 2:33:05 PM
<End Date				: -	
<Description			: -   Code behind for edit deposit.
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - Code behind for edit deposit.
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
using Cms.BusinessLayer.BlAccount;
using Cms.Model.Account;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb;
using Cms.CmsWeb.Controls;

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Code behind for edit deposit.
	/// </summary>
	public class EditDeposit : Cms.Account.AccountBase
	{
		#region Page controls declaration
		protected System.Web.UI.WebControls.TextBox txtLINE_ITEM_INTERNAL_NUMBER;
		protected System.Web.UI.WebControls.DropDownList cmbAccountingEntity;
		protected System.Web.UI.WebControls.TextBox txtACCOUNT_ID;
		protected System.Web.UI.WebControls.DropDownList cmbDEPOSIT_TYPE;
		protected System.Web.UI.WebControls.TextBox txtBANK_NAME;
		protected System.Web.UI.WebControls.TextBox txtCHECK_NUM;
		protected System.Web.UI.WebControls.TextBox txtRECEIPT_AMOUNT;
		protected System.Web.UI.WebControls.DropDownList cmbPAYOR_TYPE;
		protected System.Web.UI.WebControls.TextBox txtRECEIPT_FROM_ID;
		protected System.Web.UI.WebControls.TextBox txtLINE_ITEM_DESCRIPTION;
		protected System.Web.UI.WebControls.DropDownList cmbPOLICY_ID;
		protected System.Web.UI.WebControls.TextBox txtREF_CUSTOMER_ID;
		protected System.Web.UI.WebControls.TextBox txtAppliedStatus;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;

		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnApplyOpenItems;
		protected Cms.CmsWeb.Controls.CmsButton btnDeleteLineItem;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAccountingEntity;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDEPOSIT_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvRECEIPT_AMOUNT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPAYOR_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvRECEIPT_FROM_ID;

		protected System.Web.UI.WebControls.RegularExpressionValidator revRECEIPT_AMOUNT;
		
		protected System.Web.UI.WebControls.Label lblMessage;

		#endregion
		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		//private int	intLoggedInUserID;
		protected System.Web.UI.WebControls.Label capAccountingEntity;
		protected System.Web.UI.WebControls.Label capLINE_ITEM_INTERNAL_NUMBER;
		protected System.Web.UI.WebControls.Label capDEPOSIT_TYPE;
		protected System.Web.UI.WebControls.Label capBANK_NAME;
		protected System.Web.UI.WebControls.Label capCHECK_NUM;
		protected System.Web.UI.WebControls.Label capRECEIPT_AMOUNT;
		protected System.Web.UI.WebControls.Label capPAYOR_TYPE;
		protected System.Web.UI.WebControls.Label capRECEIPT_FROM_ID;
		protected System.Web.UI.WebControls.Label capPOLICY_ID;
		protected System.Web.UI.WebControls.Label capACCOUNT_ID;
		protected System.Web.UI.WebControls.Label capLINE_ITEM_DESCRIPTION;
		protected System.Web.UI.WebControls.Label capAppliedStatus;
		protected System.Web.UI.WebControls.Label capREF_CUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCD_LINE_ITEM_ID;
		protected Cms.CmsWeb.Controls.CmsButton btnApplyOpen_Items;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete_LineItem;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRECEIPT_FROM_ID_HID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidREF_CUSTOMER_ID_HID;
		public string URL="";
		protected System.Web.UI.HtmlControls.HtmlImage imgSelect;
		protected System.Web.UI.HtmlControls.HtmlImage Img1;
		//Defining the business layer class object
		ClsDepositDetails  objDepositDetails ;
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
			rfvAccountingEntity.ErrorMessage		=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			rfvDEPOSIT_TYPE.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			rfvRECEIPT_AMOUNT.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
			rfvPAYOR_TYPE.ErrorMessage				=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
			rfvRECEIPT_FROM_ID.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
			
			revRECEIPT_AMOUNT.ValidationExpression	=	aRegExpCurrencyformat;
			
			revRECEIPT_AMOUNT.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6");
			
		}
		#endregion
		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");

            // phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
			base.ScreenId="187_1_0";
			lblMessage.Visible = false;
			SetErrorMessages();

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass					=	CmsButtonType.Execute;
			btnReset.PermissionString				=	gstrSecurityXML;

			btnApplyOpenItems.CmsButtonClass		=	CmsButtonType.Execute;
			btnApplyOpenItems.PermissionString		=	gstrSecurityXML;

			btnDeleteLineItem.CmsButtonClass		=	CmsButtonType.Execute;
			btnDeleteLineItem.PermissionString		=	gstrSecurityXML;

			btnSave.CmsButtonClass					=	CmsButtonType.Execute;
			btnSave.PermissionString				=	gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Account.Aspx.EditDeposit" ,System.Reflection.Assembly.GetExecutingAssembly());
			URL = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL();															
			
			if(!Page.IsPostBack)
            {
				hidOldData.Value  = ClsDepositDetails.GetXmlForEditPageControls(Request.QueryString["CD_LINE_ITEM_ID"].ToString());
				SetCaptions();				
				// filling drop downs
				Cms.BusinessLayer.BlCommon.ClsDivision.GetDefaultHierarchyDropDown(cmbAccountingEntity);

				
			}
		}//end pageload
		#endregion
		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsDepositDetailsInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsDepositDetailsInfo objDepositDetailsInfo;
			objDepositDetailsInfo = new ClsDepositDetailsInfo();

			objDepositDetailsInfo.LINE_ITEM_INTERNAL_NUMBER=	int.Parse(txtLINE_ITEM_INTERNAL_NUMBER.Text);

			string AcEntity = cmbAccountingEntity.SelectedValue;
			objDepositDetailsInfo.DIV_ID	=  int.Parse(AcEntity.Split('_')[0]);
			objDepositDetailsInfo.DEPT_ID	=  int.Parse(AcEntity.Split('_')[1]);
			objDepositDetailsInfo.PC_ID		=  int.Parse(AcEntity.Split('_')[2]);

			//objDepositDetailsInfo.ACCOUNT_ID=	int.Parse(txtACCOUNT_ID.Text);
			objDepositDetailsInfo.DEPOSIT_TYPE=	cmbDEPOSIT_TYPE.SelectedValue;
			objDepositDetailsInfo.BANK_NAME=	txtBANK_NAME.Text;
			objDepositDetailsInfo.CHECK_NUM=	txtCHECK_NUM.Text;
			objDepositDetailsInfo.RECEIPT_AMOUNT=	double.Parse(txtRECEIPT_AMOUNT.Text);
			objDepositDetailsInfo.PAYOR_TYPE=	cmbPAYOR_TYPE.SelectedValue;
			if(objDepositDetailsInfo.PAYOR_TYPE.ToUpper().Equals("OTH"))
			//if payer type is other data will go into RECEIPT_FROM_NAME and not into RECEIPT_FROM_ID
			{
				objDepositDetailsInfo.RECEIPT_FROM_NAME = txtRECEIPT_FROM_ID.Text;
			}
			else
			{
				objDepositDetailsInfo.RECEIPT_FROM_ID=	int.Parse(hidRECEIPT_FROM_ID_HID.Value);
			}
			objDepositDetailsInfo.LINE_ITEM_DESCRIPTION=	txtLINE_ITEM_DESCRIPTION.Text;
			objDepositDetailsInfo.POLICY_ID=	int.Parse(cmbPOLICY_ID.SelectedValue);
			objDepositDetailsInfo.REF_CUSTOMER_ID=	int.Parse(hidREF_CUSTOMER_ID_HID.Value);
			//objDepositDetailsInfo.AppliedStatus=	txtAppliedStatus.Text;

			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidCD_LINE_ITEM_ID.Value;
			oldXML			=   hidOldData.Value;
			//Returning the model object

			return objDepositDetailsInfo;
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
			this.btnDeleteLineItem.Click += new System.EventHandler(this.btnDeleteLineItem_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

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
				int intRetVal;	//For retreiving the return value of business class save function
				objDepositDetails = new  ClsDepositDetails(true);

				//Retreiving the form values into model class object
				ClsDepositDetailsInfo objDepositDetailsInfo = GetFormValue();

				/*if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objDepositDetailsInfo.CREATED_BY = int.Parse(GetUserId());
					objDepositDetailsInfo.CREATED_DATETIME = DateTime.Now;

					//Calling the add method of business layer class
					intRetVal = objDepositDetails.Add(objDepositDetailsInfo);

					if(intRetVal>0)
					{
						hidCD_LINE_ITEM_ID.Value	=   objDepositDetailsInfo.CD_LINE_ITEM_ID.ToString();
						lblMessage.Text				=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
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
				else*/

				//UPDATE CASE
				{

					//Creating the Model object for holding the Old data
					ClsDepositDetailsInfo objOldDepositDetailsInfo;
					objOldDepositDetailsInfo = new ClsDepositDetailsInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldDepositDetailsInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objDepositDetailsInfo.CD_LINE_ITEM_ID = int.Parse(strRowId);
					objDepositDetailsInfo.MODIFIED_BY = int.Parse(GetUserId());
					objDepositDetailsInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					//Updating the record using business layer class object
					intRetVal	= objDepositDetails.Update(objOldDepositDetailsInfo,objDepositDetailsInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						hidOldData.Value		=   ClsDepositDetails.GetXmlForEditPageControls(Request.QueryString["CD_LINE_ITEM_ID"].ToString());
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
				ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objDepositDetails!= null)
					objDepositDetails.Dispose();
			}
		}

		
		#endregion
		private void SetCaptions()
		{
			capLINE_ITEM_INTERNAL_NUMBER.Text		=		objResourceMgr.GetString("txtLINE_ITEM_INTERNAL_NUMBER");
			capAccountingEntity.Text				=		objResourceMgr.GetString("cmbAccountingEntity");
			capACCOUNT_ID.Text						=		objResourceMgr.GetString("txtACCOUNT_ID");
			capDEPOSIT_TYPE.Text					=		objResourceMgr.GetString("cmbDEPOSIT_TYPE");
			capBANK_NAME.Text						=		objResourceMgr.GetString("txtBANK_NAME");
			capCHECK_NUM.Text						=		objResourceMgr.GetString("txtCHECK_NUM");
			capRECEIPT_AMOUNT.Text					=		objResourceMgr.GetString("txtRECEIPT_AMOUNT");
			capPAYOR_TYPE.Text						=		objResourceMgr.GetString("cmbPAYOR_TYPE");
			capRECEIPT_FROM_ID.Text					=		objResourceMgr.GetString("txtRECEIPT_FROM_ID");
			capLINE_ITEM_DESCRIPTION.Text			=		objResourceMgr.GetString("txtLINE_ITEM_DESCRIPTION");
			capPOLICY_ID.Text						=		objResourceMgr.GetString("cmbPOLICY_ID");
			capREF_CUSTOMER_ID.Text					=		objResourceMgr.GetString("txtREF_CUSTOMER_ID");
			capAppliedStatus.Text					=		objResourceMgr.GetString("txtAppliedStatus");
		}

		private void btnDeleteLineItem_Click(object sender, System.EventArgs e)
		{
			int intRetVal = new ClsDepositDetails().Delete(int.Parse(hidCD_LINE_ITEM_ID.Value));
			if( intRetVal > 0 )			// delete successfully performed
			{

				lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"127");
				hidFormSaved.Value		=	"1";
				hidOldData.Value		=	"";
			}
			else if(intRetVal == -1)	// delete can not be performed as account is used in transactions
			{
				lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"12");
				hidFormSaved.Value		=	"2";
			}
			else 

			{
				lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"128");
				hidFormSaved.Value		=	"2";
			}

			lblMessage.Visible = true;
		}
		
	}
}
