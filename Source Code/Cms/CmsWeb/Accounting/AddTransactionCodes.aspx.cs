/******************************************************************************************
<Author				: -   Ajit Singh Chahal
<Start Date				: -	6/6/2005 8:04:28 PM
<End Date				: -	
<Description				: - 	Code Behind for Add Transaction Codes.
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
<Purpose				: - Code Behind for Add Transaction Codes.

<Modified Date			: - 26/08/2005
<Modified By			: - Anurag Verma
<Purpose				: - Applying Null Check for buttons on aspx page 
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
using Cms.Model.Maintenance.Accounting;
using Cms.BusinessLayer.BlCommon.Accounting;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;

namespace Cms.CmsWeb.Maintenance.Accounting
{
	/// <summary>
	/// Code Behind for Add Transaction Codes.
	/// </summary>
	public class AddTransactionCodes :  Cms.CmsWeb.cmsbase
	{
			#region Page controls declaration
			protected System.Web.UI.WebControls.DropDownList cmbCATEGOTY_CODE;
			protected System.Web.UI.WebControls.DropDownList cmbTRAN_TYPE;
			protected System.Web.UI.WebControls.TextBox txtTRAN_CODE;
			protected System.Web.UI.WebControls.TextBox txtDISPLAY_DESCRIPTION;
			protected System.Web.UI.WebControls.TextBox txtPRINT_DESCRIPTION;
			protected System.Web.UI.WebControls.DropDownList cmbDEF_AMT_CALC_TYPE;
			protected System.Web.UI.WebControls.TextBox txtDEF_AMT;
			protected System.Web.UI.WebControls.CheckBox chkAGENCY_COMM_APPLIES;
			protected System.Web.UI.WebControls.DropDownList cmbGL_INCOME_ACC;
			protected System.Web.UI.WebControls.CheckBox chkIS_DEF_NEGATIVE;

			protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
			protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
			protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
            protected System.Web.UI.HtmlControls.HtmlInputHidden hidAmount;
            protected System.Web.UI.HtmlControls.HtmlInputHidden hidError;
            protected System.Web.UI.HtmlControls.HtmlInputHidden hidError2;
			protected Cms.CmsWeb.Controls.CmsButton btnReset;
			protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
			protected Cms.CmsWeb.Controls.CmsButton btnSave;

			protected System.Web.UI.WebControls.RequiredFieldValidator rfvCATEGOTY_CODE;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvTRAN_TYPE;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvTRAN_CODE;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvDISPLAY_DESCRIPTION;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvPRINT_DESCRIPTION;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvDEF_AMT_CALC_TYPE;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvDEF_AMT;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvGL_INCOME_ACC;

			protected System.Web.UI.WebControls.RegularExpressionValidator revTRAN_CODE;
			protected System.Web.UI.WebControls.RegularExpressionValidator revDEF_AMT;
			protected System.Web.UI.WebControls.Label lblMessage;

			#endregion
			#region Local form variables
			//START:*********** Local form variables *************
			string oldXML;
			//creating resource manager object (used for reading field and label mapping)
			System.Resources.ResourceManager objResourceMgr;
			private string strRowId, strFormSaved;
			//private int	intLoggedInUserID;
		protected System.Web.UI.WebControls.Label capCATEGOTY_CODE;
		protected System.Web.UI.WebControls.Label capTRAN_TYPE;
		protected System.Web.UI.WebControls.Label capTRAN_CODE;
		protected System.Web.UI.WebControls.Label capDISPLAY_DESCRIPTION;
		protected System.Web.UI.WebControls.Label capPRINT_DESCRIPTION;
		protected System.Web.UI.WebControls.Label capDEF_AMT_CALC_TYPE;
		protected System.Web.UI.WebControls.Label capDEF_AMT;
		protected System.Web.UI.WebControls.Label capAGENCY_COMM_APPLIES;
		protected System.Web.UI.WebControls.Label capGL_INCOME_ACC;
		protected System.Web.UI.WebControls.Label capIS_DEF_NEGATIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTRAN_ID;
        protected System.Web.UI.WebControls.Label capMessages;
		//protected System.Web.UI.WebControls.DropDownList cmbDEF_AMT;
			//Defining the business layer class object
			ClsTransactionCodes  objTransactionCodes ;
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
			rfvCATEGOTY_CODE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			rfvTRAN_TYPE.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			rfvTRAN_CODE.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
			rfvDISPLAY_DESCRIPTION.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
			rfvPRINT_DESCRIPTION.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
			rfvDEF_AMT_CALC_TYPE.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6");
			rfvDEF_AMT.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");
			rfvGL_INCOME_ACC.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"8");
			
			revTRAN_CODE.ValidationExpression		=	aRegExpAlphaNum;
			revDEF_AMT.ValidationExpression			=	aRegExpDoublePositiveZero;//@"[0-9]\d{0-6}.[0-9]\d{0-4}";
			
			revTRAN_CODE.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"9");
			revDEF_AMT.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"10");
		}
			#endregion
			#region PageLoad event
			private void Page_Load(object sender, System.EventArgs e)
			{
				btnReset.Attributes.Add("onclick","javascript:return ResetPage();");
				btnSave.Attributes.Add("onclick","javascript:return Validate();");

				// phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
				base.ScreenId="184_0";
				lblMessage.Visible = false;
				SetErrorMessages();
                capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");

				//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
				btnReset.CmsButtonClass	=	CmsButtonType.Write;
				btnReset.PermissionString		=	gstrSecurityXML;

				btnActivateDeactivate.CmsButtonClass	=	CmsButtonType.Write;
				btnActivateDeactivate.PermissionString		=	gstrSecurityXML;

				btnSave.CmsButtonClass	=	CmsButtonType.Write;
				btnSave.PermissionString		=	gstrSecurityXML;

				//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
				objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.Accounting.AddTransactionCodes" ,System.Reflection.Assembly.GetExecutingAssembly());
				if(!Page.IsPostBack)
				{
					//cmbDEF_AMT.Items.Add("");
					//for(int i=1;i<=100;i++)
					//	cmbDEF_AMT.Items.Add(i.ToString());
					ClsGlAccounts.GetAccountsInDropDown(cmbGL_INCOME_ACC,"4");
					SetCaptions();
                    AccountTypeDropDown();
					if(Request.QueryString["TRAN_ID"]!=null && Request.QueryString["TRAN_ID"].Length>0)
						hidOldData.Value = ClsTransactionCodes.GetXmlForPageControls(Request.QueryString["TRAN_ID"].ToString());
				}
			}//end pageload
			#endregion
			
			#region GetFormValue
			/// <summary>
			/// Fetch form's value and stores into model class object and return that object.
			/// </summary>
			private ClsTransactionCodesInfo GetFormValue()
			{
				//Creating the Model object for holding the New data
				ClsTransactionCodesInfo objTransactionCodes;
				objTransactionCodes = new ClsTransactionCodesInfo();

				objTransactionCodes.CATEGOTY_CODE		=	cmbCATEGOTY_CODE.SelectedValue;
				objTransactionCodes.TRAN_TYPE			=	Request.Form["cmbTRAN_TYPE"];
				objTransactionCodes.TRAN_CODE			=	txtTRAN_CODE.Text;
				objTransactionCodes.DISPLAY_DESCRIPTION	=	txtDISPLAY_DESCRIPTION.Text;
				objTransactionCodes.PRINT_DESCRIPTION	=	txtPRINT_DESCRIPTION.Text;
				if(objTransactionCodes.TRAN_TYPE.Equals("fee") || objTransactionCodes.TRAN_TYPE.Equals("dis"))
				{
					objTransactionCodes.DEF_AMT_CALC_TYPE	=	cmbDEF_AMT_CALC_TYPE.SelectedValue;
					objTransactionCodes.DEF_AMT				=	double.Parse(txtDEF_AMT.Text);
				}
								
				if(chkAGENCY_COMM_APPLIES.Checked)
					objTransactionCodes.AGENCY_COMM_APPLIES	=	"Y";
				else
					objTransactionCodes.AGENCY_COMM_APPLIES	=	"N";
				if(objTransactionCodes.TRAN_TYPE.Equals("fee"))
					objTransactionCodes.GL_INCOME_ACC		=	int.Parse(cmbGL_INCOME_ACC.SelectedValue);
				if(chkIS_DEF_NEGATIVE.Checked)
					objTransactionCodes.IS_DEF_NEGATIVE		=	"Y";
				else
					objTransactionCodes.IS_DEF_NEGATIVE		=	"N";

				//These  assignments are common to all pages.
				strFormSaved	=	hidFormSaved.Value;
				strRowId		=	hidTRAN_ID.Value;
				oldXML		= hidOldData.Value;
				//Returning the model object

				return objTransactionCodes;
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
					objTransactionCodes = new  ClsTransactionCodes(true);

					//Retreiving the form values into model class object
					ClsTransactionCodesInfo objTransactionCodesInfo = GetFormValue();

					if(strRowId.ToUpper().Equals("NEW")) //save case
					{
						objTransactionCodesInfo.CREATED_BY				= int.Parse(GetUserId());
						objTransactionCodesInfo.CREATED_DATETIME		= DateTime.Now;
						objTransactionCodesInfo.MODIFIED_BY				= objTransactionCodesInfo.CREATED_BY;
						objTransactionCodesInfo.LAST_UPDATED_DATETIME	= objTransactionCodesInfo.CREATED_DATETIME;
						objTransactionCodesInfo.IS_ACTIVE				= "Y";

						//Calling the add method of business layer class
						intRetVal = objTransactionCodes.Add(objTransactionCodesInfo);

						if(intRetVal>0)
						{
							hidTRAN_ID.Value		=	objTransactionCodesInfo.TRAN_ID.ToString();
							lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
							hidFormSaved.Value		=	"1";
							hidIS_ACTIVE.Value		=	"Y";
							hidOldData.Value = ClsTransactionCodes.GetXmlForPageControls(objTransactionCodesInfo.TRAN_ID.ToString());
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
						ClsTransactionCodesInfo objOldTransactionCodesInfo;
						objOldTransactionCodesInfo = new ClsTransactionCodesInfo();

						//Setting  the Old Page details(XML File containing old details) into the Model Object
						base.PopulateModelObject(objOldTransactionCodesInfo,hidOldData.Value);


						//Setting those values into the Model object which are not in the page
						objTransactionCodesInfo.TRAN_ID = int.Parse(strRowId);
						objTransactionCodesInfo.MODIFIED_BY = int.Parse(GetUserId());
						objTransactionCodesInfo.LAST_UPDATED_DATETIME = DateTime.Now;
						//Updating the record using business layer class object
						intRetVal	= objTransactionCodes.Update(objOldTransactionCodesInfo,objTransactionCodesInfo);
						if( intRetVal > 0 )			// update successfully performed
						{
							lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
							hidFormSaved.Value		=	"1";
							hidOldData.Value = ClsTransactionCodes.GetXmlForPageControls(objTransactionCodesInfo.TRAN_ID.ToString());
						}
						else if(intRetVal == -1)	// Duplicate code exist, update failed
						{
							lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"18");
							hidFormSaved.Value		=	"2";
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
					//ExceptionManager.Publish(ex);
				}
				finally
				{
					if(objTransactionCodes!= null)
						objTransactionCodes.Dispose();
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
					objTransactionCodes =  new ClsTransactionCodes();

					if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
					{
						objStuTransactionInfo.transactionDescription = "Deactivated Succesfully.";
						objTransactionCodes.TransactionInfoParams = objStuTransactionInfo;
						objTransactionCodes.ActivateDeactivate(hidTRAN_ID.Value,"N");
						lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
						hidIS_ACTIVE.Value="N";
					}
					else
					{
						objStuTransactionInfo.transactionDescription = "Activated Succesfully.";
						objTransactionCodes.TransactionInfoParams = objStuTransactionInfo;
						objTransactionCodes.ActivateDeactivate(hidTRAN_ID.Value,"Y");
						lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
						hidIS_ACTIVE.Value="Y";
					}
					hidOldData.Value = ClsTransactionCodes.GetXmlForPageControls(hidTRAN_ID.Value.ToString());
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
					if(objTransactionCodes!= null)
						objTransactionCodes.Dispose();
				}
			}
			#endregion
			private void SetCaptions()
			{
				capCATEGOTY_CODE.Text					=		objResourceMgr.GetString("cmbCATEGOTY_CODE");
				capTRAN_TYPE.Text						=		objResourceMgr.GetString("cmbTRAN_TYPE");
				capTRAN_CODE.Text						=		objResourceMgr.GetString("txtTRAN_CODE");
				capDISPLAY_DESCRIPTION.Text				=		objResourceMgr.GetString("txtDISPLAY_DESCRIPTION");
				capPRINT_DESCRIPTION.Text				=		objResourceMgr.GetString("txtPRINT_DESCRIPTION");
				capDEF_AMT_CALC_TYPE.Text				=		objResourceMgr.GetString("cmbDEF_AMT_CALC_TYPE");
				capDEF_AMT.Text							=		objResourceMgr.GetString("txtDEF_AMT");
				capAGENCY_COMM_APPLIES.Text				=		objResourceMgr.GetString("chkAGENCY_COMM_APPLIES");
				capGL_INCOME_ACC.Text					=		objResourceMgr.GetString("cmbGL_INCOME_ACC");
				capIS_DEF_NEGATIVE.Text					=		objResourceMgr.GetString("chkIS_DEF_NEGATIVE");
                hidAmount.Value = objResourceMgr.GetString("txtDEF_AMT");
                hidError.Value = objResourceMgr.GetString("hidError");
                hidError2.Value = objResourceMgr.GetString("hidError2");
			}

            private void AccountTypeDropDown()
            {

                cmbCATEGOTY_CODE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("%AccTy");
                cmbCATEGOTY_CODE.DataTextField = "LookupDesc";
                cmbCATEGOTY_CODE.DataValueField = "LookupID";
                cmbCATEGOTY_CODE.DataBind();

                cmbDEF_AMT_CALC_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("DEFAMT");
                cmbDEF_AMT_CALC_TYPE.DataTextField = "LookupDesc";
                cmbDEF_AMT_CALC_TYPE.DataValueField = "LookupID";
                cmbDEF_AMT_CALC_TYPE.DataBind();
            }
		}
	}
