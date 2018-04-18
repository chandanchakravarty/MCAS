/******************************************************************************************
<Author				: -		Vijay Arora
<Start Date			: -		10/4/2005 11:25:38 AM
<End Date			: -	
<Description		: -		Code behind for Budget Category
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: - 
<Modified By		: - 
<Purpose			: - 
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

namespace Cms.CmsWeb.Accounting
{
	/// <summary>
	/// This class is used to add, update the budget category.
	/// </summary>
	public class AddBudgetCatgory : Cms.CmsWeb.cmsbase
	{
		#region Page controls declaration
		protected System.Web.UI.WebControls.TextBox txtCATEGEORY_CODE;
		protected System.Web.UI.WebControls.TextBox txtCATEGORY_DEPARTEMENT_NAME;
		protected System.Web.UI.WebControls.TextBox txtRESPONSIBLE_EMPLOYEE_NAME;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCATEGEORY_ID;

		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCATEGEORY_CODE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCATEGORY_DEPARTEMENT_NAME;

		protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.WebControls.Label capMessages;
		protected System.Web.UI.WebControls.Label capCATEGEORY_CODE;
		protected System.Web.UI.WebControls.Label capCATEGORY_DEPARTEMENT_NAME;
		protected System.Web.UI.WebControls.Label capRESPONSIBLE_EMPLOYEE_NAME;

		#endregion

		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		protected System.Web.UI.WebControls.RangeValidator rnvCode;
				
		//Defining the business layer class object
		ClsBudgetCategory  objBudgetCategory;
		
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
			rfvCATEGEORY_CODE.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			rfvCATEGORY_DEPARTEMENT_NAME.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
		}
		#endregion

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			btnReset.Attributes.Add("onclick","javascript:return ResetForm();");

			// phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
			base.ScreenId="221_0";
			lblMessage.Visible = false;
			SetErrorMessages();
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	CmsButtonType.Write;      //permission made Write instead of Execute by Sibin on 27 Oct 08
			btnReset.PermissionString	=	gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass	=	CmsButtonType.Write;  //permission made Write instead of Execute by Sibin on 27 Oct 08
			btnActivateDeactivate.PermissionString	=	gstrSecurityXML;

			btnSave.CmsButtonClass	=	CmsButtonType.Write;   //permission made Write instead of Execute by Sibin on 27 Oct 08
			btnSave.PermissionString	=	gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Accounting.AddBudgetCatgory" ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{

				if(Request.QueryString["CATEGEORY_ID"]!=null && Request.QueryString["CATEGEORY_ID"].ToString().Length>0)
				{
					//Edit mode
					hidCATEGEORY_ID.Value = Request.QueryString["CATEGEORY_ID"].ToString();
					LoadData();
				}
				else
				{
					//Add mode
				}
				SetCaptions();
			}
		}//end pageload
		#endregion
		
		private void LoadData()
		{
			//Assign values to text boxes
		
			if (hidCATEGEORY_ID.Value.ToString().ToUpper().Trim() != "NEW")
			{
				DataSet objTempDst = new DataSet();
				objBudgetCategory = new ClsBudgetCategory();
				objTempDst = objBudgetCategory.GetBudgetCategory(hidCATEGEORY_ID.Value);
			
				this.hidOldData.Value = objTempDst.GetXml();
		
				this.txtCATEGEORY_CODE.Text = Convert.ToString(objTempDst.Tables[0].Rows[0]["CATEGEORY_CODE"]);
				this.txtCATEGORY_DEPARTEMENT_NAME.Text = Convert.ToString(objTempDst.Tables[0].Rows[0]["CATEGORY_DEPARTEMENT_NAME"]);
				this.txtRESPONSIBLE_EMPLOYEE_NAME.Text = Convert.ToString(objTempDst.Tables[0].Rows[0]["RESPONSIBLE_EMPLOYEE_NAME"]);
				this.hidIS_ACTIVE.Value = Convert.ToString(objTempDst.Tables[0].Rows[0]["IS_ACTIVE"]);

				if (Convert.ToString(objTempDst.Tables[0].Rows[0]["IS_ACTIVE"]).ToUpper().Trim() == "Y")
				{
					btnActivateDeactivate.Text = "Deactivate";
				}
				else
				{
					btnActivateDeactivate.Text = "Activate";
				}

				btnActivateDeactivate.Enabled = true;
			}	
		
		}

		#region Validation Check 
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
		#endregion

		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsBudgetCategoryInfo GetFormValue()
		{
			//Creating the Model object for holding the New data

			ClsBudgetCategoryInfo objBudgetCatgoryInfo;
			objBudgetCatgoryInfo = new ClsBudgetCategoryInfo();

			objBudgetCatgoryInfo.CATEGEORY_CODE=Convert.ToInt32(txtCATEGEORY_CODE.Text);
			objBudgetCatgoryInfo.CATEGORY_DEPARTEMENT_NAME=	txtCATEGORY_DEPARTEMENT_NAME.Text;
			objBudgetCatgoryInfo.RESPONSIBLE_EMPLOYEE_NAME=	txtRESPONSIBLE_EMPLOYEE_NAME.Text;

			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidCATEGEORY_ID.Value;
			oldXML		= hidOldData.Value;
			//Returning the model object

			return objBudgetCatgoryInfo;
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
				objBudgetCategory = new ClsBudgetCategory();

				//Retreiving the form values into model class object
				ClsBudgetCategoryInfo objBudgetCatgoryInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objBudgetCatgoryInfo.CREATED_BY = int.Parse(GetUserId());
					objBudgetCatgoryInfo.CREATED_DATETIME = DateTime.Now;

					//Calling the add method of business layer class
					intRetVal = objBudgetCategory.Add(objBudgetCatgoryInfo);

					if(intRetVal>0)
					{
						hidCATEGEORY_ID.Value = objBudgetCatgoryInfo.CATEGEORY_ID.ToString();
						lblMessage.Text		  =	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value	  =	"1";
						hidIS_ACTIVE.Value	  = "Y";
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text		  =		ClsMessages.GetMessage(base.ScreenId,"18");
						hidFormSaved.Value	  =		"2";
					}
					else
					{
						lblMessage.Text				=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value			=	"2";
					}
					lblMessage.Visible = true;
					
				} // end save case
				else //UPDATE CASE
				{

					//Creating the Model object for holding the Old data
					ClsBudgetCategoryInfo objOldBudgetCatgoryInfo;
					objOldBudgetCatgoryInfo = new ClsBudgetCategoryInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldBudgetCatgoryInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objBudgetCatgoryInfo.CATEGEORY_ID = int.Parse(strRowId);
					objBudgetCatgoryInfo.MODIFIED_BY = int.Parse(GetUserId());
					objBudgetCatgoryInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					//Updating the record using business layer class object
					intRetVal	= objBudgetCategory.Update(objOldBudgetCatgoryInfo,objBudgetCatgoryInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
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

				LoadData();
			}
			catch(Exception ex)
			{
				lblMessage.Text		=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value	=	"2";
				ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objBudgetCategory!= null)
					objBudgetCategory.Dispose();
			}
		}

		/// <summary>
		/// Activates and deactivates  .
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			try
			{
				Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo ();
				objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
				objStuTransactionInfo.loggedInUserName = GetUserName();
				objBudgetCategory =  new ClsBudgetCategory();

				if(Convert.ToString(hidIS_ACTIVE.Value).ToUpper().Trim() == "Y")
				{
					objStuTransactionInfo.transactionDescription = "Budget Category Deactivated Succesfully.";
					objBudgetCategory.TransactionInfoParams = objStuTransactionInfo;
					objBudgetCategory.ActivateDeactivate(hidCATEGEORY_ID.Value,"N");
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";
					btnActivateDeactivate.Text = "Deactivate";
					
				}
				else
				{
					objStuTransactionInfo.transactionDescription = "Budget Category Activated Succesfully.";
					objBudgetCategory.TransactionInfoParams = objStuTransactionInfo;
					objBudgetCategory.ActivateDeactivate(hidCATEGEORY_ID.Value,"Y");
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";
					btnActivateDeactivate.Text = "Activate";
				}
				LoadData();
				btnActivateDeactivate.Enabled = true;
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
				if(objBudgetCategory!= null)
					objBudgetCategory.Dispose();
			}
		}
		#endregion

		#region Caption and Old Data XML Function
		private void SetCaptions()
		{
			capCATEGEORY_CODE.Text						=		objResourceMgr.GetString("txtCATEGEORY_CODE");
			capCATEGORY_DEPARTEMENT_NAME.Text						=		objResourceMgr.GetString("txtCATEGORY_DEPARTEMENT_NAME");
			capRESPONSIBLE_EMPLOYEE_NAME.Text						=		objResourceMgr.GetString("txtRESPONSIBLE_EMPLOYEE_NAME");
		}
		private void GetOldDataXML()
		{
			if ( Request.Params.Count != 0 ) 
			{
			}
			else 
			{
			}
		}
		#endregion
	}
}
