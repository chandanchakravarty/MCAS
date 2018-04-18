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
using Cms.BusinessLayer.BlCommon;

/******************************************************************************************
	<Author					: - Vijay Joshi>
	<Start Date				: -	13 April 2005>
	<End Date				: - >
	<Description			: - To generate the AddFinanceCompany page>
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: 11 May 05	- >
	<Modified By			: Gaurav- >
	<Purpose				: Add validators for few fields on the page- >
	
    <Modified Date			:  22 Jun 05	- >
	<Modified By			: Priya- >
	<Purpose				: Add generate code functon- >
	
	<Modified Date			: - 26/08/2005
	<Modified By			: - Anurag Verma
	<Purpose				: - Applying Null Check for buttons on aspx page
*******************************************************************************************/

namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for AddFinanceCompany.
	/// </summary>
	public class AddFinanceCompany : Cms.CmsWeb.cmsbase
	{

		#region Form variables
		int intCOMPANY_ID;
		Cms.BusinessLayer.BlCommon.ClsFinanceCompany objCompany;
		System.Resources.ResourceManager objResourceMgr;
		#endregion

		#region Web form designer declared webcontrold
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.TextBox txtCOMPANY_NAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOMPANY_NAME;
		protected System.Web.UI.WebControls.TextBox txtCOMPANY_CODE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOMPANY_CODE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCOMPANY_CODE;
		protected System.Web.UI.WebControls.TextBox txtCOMPANY_ADD1;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOMPANY_ADD1;
		protected System.Web.UI.WebControls.TextBox txtCOMPANY_ADD2;
		protected System.Web.UI.WebControls.TextBox txtCOMPANY_CITY;
		protected System.Web.UI.WebControls.DropDownList cmbCOMPANY_COUNTRY;
		protected System.Web.UI.WebControls.DropDownList cmbCOMPANY_STATE;
		protected System.Web.UI.WebControls.TextBox txtCOMPANY_ZIP;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCOMPANY_ZIP;
		protected System.Web.UI.WebControls.TextBox txtCOMPANY_MAIN_PHONE_NO;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCOMPANY_MAIN_PHONE_NO;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCOMPANY_MAIN_TOLL_FREE_NO;
		protected System.Web.UI.WebControls.TextBox txtCOMPANY_EXT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCOMPANY_EXT;
		protected System.Web.UI.WebControls.TextBox txtCOMPANY_FAX;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCOMPANY_FAX;
		protected System.Web.UI.WebControls.TextBox txtCOMPANY_EMAIL;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCOMPANY_EMAIL;
		protected System.Web.UI.WebControls.TextBox txtCOMPANY_MOBILE;
		protected System.Web.UI.WebControls.TextBox txtCOMPANY_TERMS;
		protected System.Web.UI.WebControls.TextBox txtCOMPANY_NOTE;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCOMPANY_MOBILE;
		protected System.Web.UI.WebControls.TextBox txtCOMPANY_TOLL_FREE_NO;
		protected System.Web.UI.WebControls.TextBox txtCOMPANY_TERMS_DESC;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCOMPANY_WEBSITE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCOMPANY_NAME;
		protected System.Web.UI.WebControls.TextBox txtCOMPANY_WEBSITE;
		protected System.Web.UI.WebControls.Label capCOMPANY_NAME;
		protected System.Web.UI.WebControls.Label capCOMPANY_CODE;
		protected System.Web.UI.WebControls.Label capCOMPANY_ADD1;
		protected System.Web.UI.WebControls.Label capCOMPANY_CITY;
		protected System.Web.UI.WebControls.Label capCOMPANY_ADD2;
		protected System.Web.UI.WebControls.Label capCOMPANY_STATE;
		protected System.Web.UI.WebControls.Label capCOMPANY_MAIN_PHONE_NO;
		protected System.Web.UI.WebControls.Label capCOMPANY_TOLL_FREE_NO;
		protected System.Web.UI.WebControls.Label capCOMPANY_EXT;
		protected System.Web.UI.WebControls.Label capCOMPANY_FAX;
		protected System.Web.UI.WebControls.Label capCOMPANY_EMAIL;
		protected System.Web.UI.WebControls.Label capCOMPANY_WEBSITE;
		protected System.Web.UI.WebControls.Label capCOMPANY_NOTE;
		protected System.Web.UI.WebControls.Label capCOMPANY_COUNTRY;
		protected System.Web.UI.WebControls.Label capCOMPANY_ZIP;
		protected System.Web.UI.WebControls.Label capCOMPANY_MOBILE;
		protected System.Web.UI.WebControls.Label capCOMPANY_TERMS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOMPANY_CITY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOMPANY_COUNTRY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOMPANY_STATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOMPANY_ZIP;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCOMPANY_Name;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCOMPANY_ID;
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.CustomValidator csvCOMPANY_TERMS_DESC;
		protected System.Web.UI.WebControls.Label capCOMPANY_TERMS_DESC;
		protected System.Web.UI.WebControls.CustomValidator csvCOMPANY_NOTE;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		#endregion

		#region Page Load
		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				//Assigning the screen id of form
				base.ScreenId = "35_0";
				btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
			    txtCOMPANY_NAME.Attributes.Add("onblur","javascript:generateCode();"); 


				//Assigning the security XML to be used in CmsButton and CmsPanel
				btnSave.CmsButtonClass		=	Cms.CmsWeb.Controls.CmsButtonType.Write;
				btnSave.PermissionString	=	gstrSecurityXML;

				btnReset.CmsButtonClass		= Cms.CmsWeb.Controls.CmsButtonType.Write;
				btnReset.PermissionString	=	gstrSecurityXML;

				btnActivateDeactivate.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Write;
				btnActivateDeactivate.PermissionString	=	gstrSecurityXML;

				btnDelete.CmsButtonClass		=	Cms.CmsWeb.Controls.CmsButtonType.Delete;
				btnDelete.PermissionString	=	gstrSecurityXML;

			
				txtCOMPANY_MAIN_PHONE_NO.Attributes.Add("onBlur","javascript:DisableExt('txtCOMPANY_MAIN_PHONE_NO','txtCOMPANY_EXT');");

				objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddFinanceCompany",System.Reflection.Assembly.GetExecutingAssembly());
				

				#region If form is posted back then setting the default values
				lblMessage.Visible = false;
				
				if( ! Page.IsPostBack)
				{
					//Setting the labels of caption
					SetCaptions();

					btnActivateDeactivate.Enabled = false;
					btnDelete.Enabled =false;
					hidCOMPANY_ID.Value = "New";
				
					DataTable dt = Cms.CmsWeb.ClsFetcher.Country;
					cmbCOMPANY_COUNTRY.DataSource			= dt;
					cmbCOMPANY_COUNTRY.DataTextField		= COUNTRY_NAME;
					cmbCOMPANY_COUNTRY.DataValueField		= COUNTRY_ID;
					cmbCOMPANY_COUNTRY.DataBind();
					cmbCOMPANY_COUNTRY.SelectedIndex		= 0;

					dt = Cms.CmsWeb.ClsFetcher.State;
					cmbCOMPANY_STATE.DataSource				= dt;
					cmbCOMPANY_STATE.DataTextField			= STATE_NAME;
					cmbCOMPANY_STATE.DataValueField			= STATE_ID;
					cmbCOMPANY_STATE.DataBind();
					cmbCOMPANY_STATE.Items.Insert(0,"");
					cmbCOMPANY_STATE.SelectedIndex			= 0;
					btnActivateDeactivate.Enabled	= false;

					//Initializing the validators and other controls
					SetPageLabels();

					if ( Request.QueryString["Company_ID"] != null )
					{
						this.hidCOMPANY_ID.Value = Request.QueryString["Company_ID"].ToString();

						LoadData();
					}
				}
				#endregion
			}
			catch(Exception objExcep)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
			}	
		}
		#endregion

		#region Private form methods

		#region GetFormValues()

		private Cms.Model.Maintenance.ClsFinanceCompany GetFormValues()
		{
			//Creating the Model object for holding the New data
			Model.Maintenance.ClsFinanceCompany  objNewCompany = new Cms.Model.Maintenance.ClsFinanceCompany();

			try
			{
				//Setting those values into the Model object which are not in the page
				objNewCompany.CompanyName	= txtCOMPANY_NAME.Text;
				objNewCompany.CompanyCode	= txtCOMPANY_CODE.Text;
				objNewCompany.CompanyAdd1	= txtCOMPANY_ADD1.Text;
				objNewCompany.CompanyAdd2	= txtCOMPANY_ADD2.Text;
				objNewCompany.CompanyCity	= txtCOMPANY_CITY.Text;
				objNewCompany.CompanyCountry= cmbCOMPANY_COUNTRY.SelectedValue;
				objNewCompany.CompanyState	= cmbCOMPANY_STATE.SelectedValue;
				objNewCompany.CompanyZip	= txtCOMPANY_ZIP.Text;
				objNewCompany.CompanyEmail	= txtCOMPANY_EMAIL.Text;
				objNewCompany.CompanyExt	= txtCOMPANY_EXT.Text;
				objNewCompany.CompanyFax	= txtCOMPANY_FAX.Text;
				objNewCompany.CompanyMobile	= txtCOMPANY_MOBILE.Text;
				objNewCompany.CompanyNote	= txtCOMPANY_NOTE.Text;
				objNewCompany.CompanyPhoneNo= txtCOMPANY_MAIN_PHONE_NO.Text;
				objNewCompany.CompanyTerms	= txtCOMPANY_TERMS.Text;
				objNewCompany.CompanyTermsDescription = txtCOMPANY_TERMS_DESC.Text;
				objNewCompany.CompanyTollFreeNo = txtCOMPANY_TOLL_FREE_NO.Text;
				objNewCompany.CompanyWebsite = txtCOMPANY_WEBSITE.Text;


			}
			catch (Exception objExp)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
			}

			return objNewCompany;
		}
		#endregion


		#region Save function
		/// <summary>
		/// saves the posted data into table using the business layer class (clsCompany)
		/// </summary>
		/// <returns>void </returns>
		private int SaveFormValue()
		{
			try
			{
				int intRetVal;	//For retreiving the return value of business class save function
				
				// Creating Business layer object to do processing
				objCompany = new  Cms.BusinessLayer.BlCommon.ClsFinanceCompany();

				//Creating the Model object for holding the New data
				Model.Maintenance.ClsFinanceCompany  objNewCompany;

				//Retreiving the form values in to Model object
				objNewCompany = GetFormValues();
				
				lblMessage.Visible = true;

				//Insert new Company
				//if(strRowId.ToUpper() == "NEW") 
				if(this.hidCOMPANY_ID.Value.ToUpper() == "NEW")
				{
					objNewCompany.CREATED_BY 		=	int.Parse(GetUserId());
					objNewCompany.CREATED_DATETIME 	=	DateTime.Now;
					intRetVal						=	objCompany.Add(objNewCompany);
					
					if( intRetVal > 0)			// Value inserted successfully.
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"29");
						hidFormSaved.Value		=	"1";
						hidCOMPANY_ID.Value		=	objNewCompany.CompanyId.ToString();
						hidIS_ACTIVE.Value		=	"Y";
					}
					else if(intRetVal == -1)	// Duplicate code exist, insert failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage("G","18");
						//txtCOMPANY_CODE.Text="";
						//Response.Write("<script language ='javascript'>document.getElementById('txtCOMPANY_CODE').focus();</script>");
						hidFormSaved.Value		=	"2";
						return -1;
					}
					else						// Error occured while processing, insert failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value		=	"2";
					}
					lblMessage.Visible = true;
				} 
				//Update Company
				else 
				{
					//Creating the Model object for holding the Old data
					Model.Maintenance.ClsFinanceCompany objOldCompany = new Cms.Model.Maintenance.ClsFinanceCompany();

					//Company id for the record which is being updated
					intCOMPANY_ID 		= int.Parse(hidCOMPANY_ID.Value);
					
					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldCompany,hidOldData.Value);
					
					//Setting those values into the Model object which are not in the page
					objNewCompany.IS_ACTIVE			=	hidIS_ACTIVE.Value;
					objNewCompany.CompanyId			=	intCOMPANY_ID;
					objNewCompany.MODIFIED_BY 		=	int.Parse(GetUserId());
					objNewCompany.LAST_UPDATED_DATETIME 	=	DateTime.Now;
					
					//Setting those values into the Model object which are not in the page
					objOldCompany.CompanyId			=	intCOMPANY_ID;
					
					intRetVal						=	objCompany.Update(objOldCompany,objNewCompany);
					
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage("G","18");
						hidFormSaved.Value		=	"2";
						return -1;
					}
					else						// Error occured while processing, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value		=	"2";
					}
					
				}
			}
			catch(Exception ex)
			{
				// Show exception message
				lblMessage.Text		=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"21") + " " + ex.Message + " Try again!" ;
				lblMessage.Visible	=	true;
				hidFormSaved.Value	= "2";
				//Publishing the exception using the static method of Exception manager class
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				

				return -2;
			}
			finally
			{
				if(objCompany!= null)
				{
					objCompany.Dispose();
				}
			}
			
			return 0;
			

		}
		#endregion

		#region "SetPageLabels" function assigning the validators controls
		/// <summary>
		/// This function will set the Error Message,validation expresion of all validators controls
		/// </summary>
		private void SetPageLabels()
		{			
			
			//setting error message by calling singleton functions
			rfvCOMPANY_NAME.ErrorMessage				= Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"34");
			rfvCOMPANY_ADD1.ErrorMessage				= Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"32");
			rfvCOMPANY_CODE.ErrorMessage				= Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"38");
			
			rfvCOMPANY_CITY.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"56");
			rfvCOMPANY_STATE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"35");
			rfvCOMPANY_COUNTRY.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"33");
			rfvCOMPANY_ZIP.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"37");
			


			revCOMPANY_CODE.ErrorMessage 				= Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "64");
			revCOMPANY_MAIN_PHONE_NO.ErrorMessage 		= Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "14");
			revCOMPANY_EMAIL.ErrorMessage				= Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"23");
			revCOMPANY_EXT.ErrorMessage					= Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"25");
			revCOMPANY_FAX.ErrorMessage					= Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"15");
			revCOMPANY_MOBILE.ErrorMessage				= Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"16");
			revCOMPANY_ZIP.ErrorMessage					= Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"24");
			//revCOMPANY_CITY.ErrorMessage				= Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"39");
			revCOMPANY_WEBSITE.ErrorMessage				= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("85");
			revCOMPANY_MAIN_TOLL_FREE_NO.ErrorMessage	= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");

			revCOMPANY_NAME.ErrorMessage   =	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"63");
			
			revCOMPANY_NAME.ValidationExpression		=	aRegExpClientName;
			revCOMPANY_CODE.ValidationExpression 			= aRegExpClientName;
			revCOMPANY_MAIN_PHONE_NO.ValidationExpression	= aRegExpPhone;
			revCOMPANY_EMAIL.ValidationExpression			= aRegExpEmail;
			revCOMPANY_EXT.ValidationExpression				= aRegExpExtn;
			revCOMPANY_FAX.ValidationExpression				= aRegExpFax;
			revCOMPANY_MOBILE.ValidationExpression			= aRegExpMobile;
			revCOMPANY_ZIP.ValidationExpression				= aRegExpZip;
			//revCOMPANY_CITY.ValidationExpression			= aRegExpClientName;
			revCOMPANY_WEBSITE.ValidationExpression			= aRegExpSiteUrl;
			revCOMPANY_MAIN_TOLL_FREE_NO.ValidationExpression = aRegExpPhone;	
			csvCOMPANY_NOTE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("451");
			csvCOMPANY_TERMS_DESC.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("451");

		}
		#endregion

		#region SetCaption 
		/// <summary>
		/// Function for setting the label Captions by reading resource file
		/// </summary>
		private void SetCaptions()
		{
			capCOMPANY_NAME.Text				= objResourceMgr.GetString("txtCOMPANY_NAME");
			capCOMPANY_CODE.Text				= objResourceMgr.GetString("txtCOMPANY_CODE");
			capCOMPANY_ADD1.Text				= objResourceMgr.GetString("txtCOMPANY_ADD1");
			capCOMPANY_ADD2.Text				= objResourceMgr.GetString("txtCOMPANY_ADD2");
			capCOMPANY_CITY.Text				= objResourceMgr.GetString("txtCOMPANY_CITY");
			capCOMPANY_STATE.Text				= objResourceMgr.GetString("cmbCOMPANY_STATE");
			capCOMPANY_COUNTRY.Text				= objResourceMgr.GetString("cmbCOMPANY_COUNTRY");
			capCOMPANY_ZIP.Text					= objResourceMgr.GetString("txtCOMPANY_ZIP");
			capCOMPANY_MAIN_PHONE_NO.Text		= objResourceMgr.GetString("txtCOMPANY_MAIN_PHONE_NO");
			capCOMPANY_TOLL_FREE_NO.Text		= objResourceMgr.GetString("txtCOMPANY_TOLL_FREE_NO");
			capCOMPANY_EXT.Text					= objResourceMgr.GetString("txtCOMPANY_EXT");
			capCOMPANY_FAX.Text					= objResourceMgr.GetString("txtCOMPANY_FAX");
			capCOMPANY_EMAIL.Text				= objResourceMgr.GetString("txtCOMPANY_EMAIL");
			capCOMPANY_WEBSITE.Text				= objResourceMgr.GetString("txtCOMPANY_WEBSITE");
			capCOMPANY_MOBILE.Text				= objResourceMgr.GetString("txtCOMPANY_MOBILE");
			capCOMPANY_TERMS.Text				= objResourceMgr.GetString("txtCOMPANY_TERMS");
			capCOMPANY_TERMS_DESC.Text			= objResourceMgr.GetString("txtCOMPANY_TERMS_DESC");
			capCOMPANY_NOTE.Text				= objResourceMgr.GetString("txtCOMPANY_NOTE");
		}

		#endregion

		private void LoadData()
		{
			ClsFinanceCompany objCompany = new ClsFinanceCompany();
			
			int companyID = Convert.ToInt32(this.hidCOMPANY_ID.Value);

			DataSet dsCompany = objCompany.GetFinanceCompanyByID(companyID);

			if(dsCompany.Tables[0].Rows.Count > 0)
				hidOldData.Value = dsCompany.GetXml();
			else
				hidOldData.Value="";

		}


		#endregion

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
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			int retVal = SaveFormValue();
			
			if ( retVal == 0 )
			{
				LoadData();
				RegisterScript();
			}

		}
		

		#region activate deactivate
		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			try
			{
				Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo ();
				objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
				
				objStuTransactionInfo.loggedInUserName = GetUserName();
				objCompany = new  Cms.BusinessLayer.BlCommon.ClsFinanceCompany();

				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					objStuTransactionInfo.transactionDescription = "Finance Company Deactivated Succesfully.";
					objCompany.TransactionInfoParams = objStuTransactionInfo;
					objCompany.ActivateDeactivate(hidCOMPANY_ID.Value,"N");
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";
				}
				else
				{
					objStuTransactionInfo.transactionDescription = "Finance Company Activated Succesfully.";
					objCompany.TransactionInfoParams = objStuTransactionInfo;
					objCompany.ActivateDeactivate(hidCOMPANY_ID.Value,"Y");
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";
				}
				LoadData();
				hidFormSaved.Value			=	"1";
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21")+ " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			finally
			{
				lblMessage.Visible = true;
				if(objCompany!= null)
					objCompany.Dispose();
			}
		}

		#endregion

		/// <summary>
		/// Adds script to refresh web grid
		/// </summary>
		private void RegisterScript()
		{
			if (!ClientScript.IsStartupScriptRegistered("Refresh"))
			{
				string strCode = "<script>" + 
					"RefreshWebGrid(1," + this.hidCOMPANY_ID.Value + ", true);" +
					"</script>";

                ClientScript.RegisterStartupScript(this.GetType(),"Refresh", strCode);

			}

		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			int intRetVal;	
			int intCompanyID = int.Parse(hidCOMPANY_ID.Value);
			objCompany = new Cms.BusinessLayer.BlCommon.ClsFinanceCompany();
			intRetVal = objCompany.Delete(intCompanyID);
			if(intRetVal>0)
			{
				lblDelete.Text		 =	ClsMessages.GetMessage(base.ScreenId,"127");
				hidFormSaved.Value	 =	"5";
				hidOldData.Value	 =  "";
				trBody.Attributes.Add("style","display:none");
			}
			else if(intRetVal == -1)
			{
				lblDelete.Text         =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("334"); 
				hidFormSaved.Value		=	"2";
			}
		   lblDelete.Visible = true;

		}

	}
}
