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
using System.Reflection;
using System.Resources;

/******************************************************************************************
	<Author					: - Vijay Joshi>
	<Start Date				: -	April 13,2005>
	<End Date				: - >
	<Description			: -  Display the AddMortgage page>
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: 11 May 2005- >
	<Modified By			: Gaurav- >
	<Purpose				: Add validations to few fields- >
	<Modified Date			: 23 May 2005- >
	<Modified By			: Gaurav- >
	<Purpose				: Added FillCombo Function to fill values from LookUp- >
	
	<Modified Date			: - 26/08/2005
	<Modified By			: - Anurag Verma
	<Purpose				: - Applying Null Check for buttons on aspx page
*******************************************************************************************/

namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for AddMortgage.
	/// </summary>
	public class AddMortgage : Cms.CmsWeb.cmsbase
	{

		#region Form variables

		Cms.BusinessLayer.BlCommon.ClsMortgage objMortgage;
		System.Resources.ResourceManager objResourceMgr;
		#endregion

		#region "Web controls declaration"
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHOLDER_NAME;
		protected System.Web.UI.WebControls.RegularExpressionValidator revHOLDER_NAME;
		protected System.Web.UI.WebControls.TextBox txtHOLDER_CODE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHOLDER_CODE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revHOLDER_CODE;
		protected System.Web.UI.WebControls.TextBox txtHOLDER_ADD1;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHOLDER_ADD1;
		protected System.Web.UI.WebControls.TextBox txtHOLDER_ADD2;
		protected System.Web.UI.WebControls.TextBox txtHOLDER_CITY;
		protected System.Web.UI.WebControls.DropDownList cmbHOLDER_COUNTRY;
		protected System.Web.UI.WebControls.DropDownList cmbHOLDER_STATE;
		protected System.Web.UI.WebControls.TextBox txtHOLDER_ZIP;
		protected System.Web.UI.WebControls.RegularExpressionValidator revHOLDER_ZIP;
		protected System.Web.UI.WebControls.TextBox txtHOLDER_MAIN_PHONE_NO;
		protected System.Web.UI.WebControls.RegularExpressionValidator revHOLDER_MAIN_PHONE_NO;
		protected System.Web.UI.WebControls.TextBox txtHOLDER_EXT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revHOLDER_EXT;
		protected System.Web.UI.WebControls.TextBox txtHOLDER_MOBILE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revHOLDER_MOBILE;
		protected System.Web.UI.WebControls.TextBox txtHOLDER_FAX;
		protected System.Web.UI.WebControls.RegularExpressionValidator revHOLDER_FAX;
		protected System.Web.UI.WebControls.TextBox txtHOLDER_EMAIL;
		protected System.Web.UI.WebControls.RegularExpressionValidator revHOLDER_EMAIL;
		protected System.Web.UI.WebControls.DropDownList cmbHOLDER_LEGAL_ENTITY;
		protected System.Web.UI.WebControls.DropDownList cmbHOLDER_TYPE;
		protected System.Web.UI.WebControls.TextBox txtHOLDER_MEMO;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidZip;
		protected System.Web.UI.WebControls.Label capHOLDER_NAME;
		protected System.Web.UI.WebControls.TextBox txtHOLDER_NAME;
		protected System.Web.UI.WebControls.Label capHOLDER_CODE;
		protected System.Web.UI.WebControls.Label capHOLDER_ADD1;
		protected System.Web.UI.WebControls.Label capHOLDER_ADD2;
		protected System.Web.UI.WebControls.Label capHOLDER_COUNTRY;
		protected System.Web.UI.WebControls.Label capHOLDER_CITY;
		protected System.Web.UI.WebControls.Label capHOLDER_STATE;
		protected System.Web.UI.WebControls.Label capHOLDER_ZIP;
		protected System.Web.UI.WebControls.Label capHOLDER_MAIN_PHONE_NO;
		protected System.Web.UI.WebControls.Label capHOLDER_EXT;
		protected System.Web.UI.WebControls.Label capHOLDER_MOBILE;
		protected System.Web.UI.WebControls.Label capHOLDER_FAX;
		protected System.Web.UI.WebControls.Label capHOLDER_EMAIL;
		protected System.Web.UI.WebControls.Label capHOLDER_LEGAL_ENTITY;
		protected System.Web.UI.WebControls.Label capHOLDER_TYPE;
		protected System.Web.UI.WebControls.Label capHOLDER_MEMO;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHOLDER_CITY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHOLDER_COUNTRY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHOLDER_STATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHOLDER_ZIP;
		protected System.Web.UI.WebControls.CustomValidator csvHOLDER_MEMO;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidHOLDER_ID;
		protected System.Web.UI.WebControls.Image imgZipLookup;
		protected System.Web.UI.WebControls.HyperLink hlkZipLookup;
		protected System.Web.UI.WebControls.CustomValidator csvHOLDER_ZIP;
        protected System.Web.UI.WebControls.Label capMessages;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTAB_TITLES;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidHOLDER_STATE_2;
		#endregion

		#region Page Load
		private void Page_Load(object sender, System.EventArgs e)
		{

			try
			{
				Ajax.Utility.RegisterTypeForAjax(typeof(AddMortgage));				
				//Assigning the screen id of form
				base.ScreenId = "37_0";
				txtHOLDER_NAME.Attributes.Add("onblur","javascript:generateCode();"); 
				btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
				// Added by Swarup on 30-mar-2007
				imgZipLookup.Attributes.Add("style","cursor:hand");
				base.VerifyAddress(hlkZipLookup, txtHOLDER_ADD1,txtHOLDER_ADD2
					, txtHOLDER_CITY, cmbHOLDER_STATE, txtHOLDER_ZIP);


				//Assigning the security XML to be used in CmsButton and CmsPanel
				btnSave.CmsButtonClass		=	Cms.CmsWeb.Controls.CmsButtonType.Write;
				btnSave.PermissionString	=	gstrSecurityXML;

				btnReset.CmsButtonClass		= Cms.CmsWeb.Controls.CmsButtonType.Write;
				btnReset.PermissionString	=	gstrSecurityXML;

				btnActivateDeactivate.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Write;
				btnActivateDeactivate.PermissionString	=	gstrSecurityXML;

                //Commented by Ruchika Chauhan for TFS # 846 on 27-Dec-2011                
                if(getCarrierSystemID().ToString().ToUpper()!="S001")
                {
                    txtHOLDER_MAIN_PHONE_NO.Attributes.Add("onBlur", "javascript:DisableExt('txtHOLDER_MAIN_PHONE_NO','txtHOLDER_EXT');FormatPhone();");
                    txtHOLDER_MOBILE.Attributes.Add("onblur", "javascript:FormatPhone();ValidatorOnChange()");
                }

                txtHOLDER_MOBILE.Attributes.Add("onblur", "javascript:ValidatorOnChange()");



                

				objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddMortgage",System.Reflection.Assembly.GetExecutingAssembly());

				//Initializing the validators and other controls
				SetPageLabels();
                capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
                hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCon");
			    #region If form is posted back then setting the default values

                
				lblMessage.Visible = false;
				if( ! Page.IsPostBack)
				{
                    SetCaptions();
					btnActivateDeactivate.Enabled = false;
					hidHOLDER_ID.Value = "New";
				
					DataTable dt = Cms.CmsWeb.ClsFetcher.Country;
					cmbHOLDER_COUNTRY.DataSource			= dt;
					cmbHOLDER_COUNTRY.DataTextField		= COUNTRY_NAME;
					cmbHOLDER_COUNTRY.DataValueField		= COUNTRY_ID;
					cmbHOLDER_COUNTRY.DataBind();
					//cmbHOLDER_COUNTRY.SelectedIndex		= 0;
                    cmbHOLDER_COUNTRY.Items.Insert(0, "");
                    ClsStates objStates = new ClsStates();
                    DataTable dtStates = objStates.GetStatesForCountry(Convert.ToInt32(dt.Rows[0]["Country_Id"].ToString()));
                    if (dtStates != null && dtStates.Rows.Count > 0)
                    {
                        cmbHOLDER_STATE.DataSource = dtStates;
                        cmbHOLDER_STATE.DataTextField = STATE_NAME;
                        cmbHOLDER_STATE.DataValueField = STATE_ID;
                        cmbHOLDER_STATE.DataBind();
                        cmbHOLDER_STATE.Items.Insert(0, "");
                    }

                    dt = Cms.CmsWeb.ClsFetcher.State;
                    cmbHOLDER_STATE.DataSource = dt;
                    cmbHOLDER_STATE.DataTextField = STATE_NAME;
                    cmbHOLDER_STATE.DataValueField = STATE_ID;
                    cmbHOLDER_STATE.DataBind();
                    //cmbHOLDER_STATE.SelectedIndex = 0;
                    cmbHOLDER_STATE.Items.Insert(0, "");
					btnActivateDeactivate.Enabled	= false;

					if ( Request.QueryString["Holder_ID"] != null )
					{
						hidHOLDER_ID.Value = Request.QueryString["Holder_ID"].ToString();
						LoadData();
					}

					FillCombo();
                    if(ClsCommon.IsXMLResourceExists(@Request.PhysicalApplicationPath+"CmsWeb/Support/PageXml/"+GetSystemId(),"AddMortgage.xml"))
                    {
                        setPageControls(Page, @Request.PhysicalApplicationPath + "/CmsWeb/support/PageXML/" + GetSystemId() + "/AddMortgage.xml");
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


		private void FillCombo()
		{
			cmbHOLDER_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("%HLDTY");
			cmbHOLDER_TYPE.DataTextField="LookupDesc"; 
			cmbHOLDER_TYPE.DataValueField="LookupID";
			cmbHOLDER_TYPE.DataBind();

			cmbHOLDER_LEGAL_ENTITY.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("%LEGAL");
			cmbHOLDER_LEGAL_ENTITY.DataTextField="LookupDesc"; 
			cmbHOLDER_LEGAL_ENTITY.DataValueField="LookupID";
			cmbHOLDER_LEGAL_ENTITY.DataBind();
			cmbHOLDER_LEGAL_ENTITY.Items.Insert(0,"");	
		}

		#region Private form methods

		#region GetFormValues()

		private Cms.Model.Maintenance.ClsHolderInfo GetFormValues()
		{
			//Creating the Model object for holding the New data
			Model.Maintenance.ClsHolderInfo  objNewMortgage = new Cms.Model.Maintenance.ClsHolderInfo();

			try
			{
				//Setting those values into the Model object which are not in the page
				objNewMortgage.HOLDER_NAME		= txtHOLDER_NAME.Text;
				objNewMortgage.HOLDER_CODE		= txtHOLDER_CODE.Text;
				objNewMortgage.HOLDER_ADD1		= txtHOLDER_ADD1.Text;
				objNewMortgage.HOLDER_ADD2		= txtHOLDER_ADD2.Text;
				objNewMortgage.HOLDER_CITY		= txtHOLDER_CITY.Text;
				objNewMortgage.HOLDER_COUNTRY	= cmbHOLDER_COUNTRY.SelectedValue;
                if (cmbHOLDER_STATE.SelectedValue != "")
                    objNewMortgage.HOLDER_STATE = cmbHOLDER_STATE.SelectedValue;
                else
                {
                    if (!string.IsNullOrEmpty(hidHOLDER_STATE_2.Value) && hidHOLDER_STATE_2.Value != "0")
                        objNewMortgage.HOLDER_STATE = hidHOLDER_STATE_2.Value;
                }
				//objNewMortgage.HOLDER_STATE		= cmbHOLDER_STATE.SelectedValue;
				objNewMortgage.HOLDER_ZIP		= txtHOLDER_ZIP.Text;
				objNewMortgage.HOLDER_EMAIL		= txtHOLDER_EMAIL.Text;
				objNewMortgage.HOLDER_EXT		= txtHOLDER_EXT.Text;
				objNewMortgage.HOLDER_FAX		= txtHOLDER_FAX.Text;
				objNewMortgage.HOLDER_MOBILE	= txtHOLDER_MOBILE.Text;
				objNewMortgage.HOLDER_MAIN_PHONE_NO= txtHOLDER_MAIN_PHONE_NO.Text;
				objNewMortgage.HOLDER_LEGAL_ENTITY = cmbHOLDER_LEGAL_ENTITY.SelectedValue;
				objNewMortgage.HOLDER_MEMO		= txtHOLDER_MEMO.Text;
				objNewMortgage.HOLDER_TYPE		= cmbHOLDER_TYPE.SelectedValue;


				//Mapping feild and Lebel to maintain the transction log into the database.
                /*==========================================================
                 * SANTOSH GAUTAM : BELOW LINE MODIFIED ON 28 OCT 2010
                 * 1. OLD VALUE =>objNewMortgage.TransactLabel = MapTransactionLabel("AddMortgage.aspx.resx");
                 *==========================================================*/
                objNewMortgage.TransactLabel = ClsCommon.MapTransactionLabel("Cmsweb/Maintenance/AddMortgage.aspx.resx");

			}
			catch(Exception objExcep)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
			}
			return objNewMortgage;
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
				objMortgage = new  Cms.BusinessLayer.BlCommon.ClsMortgage();

				//Creating the Model object for holding the New data
				Model.Maintenance.ClsHolderInfo  objNewMortgage;
				
				//Retreiving the values of controls in to model class object
				objNewMortgage = GetFormValues();

				//Insert new Mortgage
				if(this.hidHOLDER_ID.Value.ToUpper() == "NEW")
				{
					objNewMortgage.CREATED_BY 		=	int.Parse(GetUserId());
					objNewMortgage.CREATED_DATETIME =	DateTime.Now;
					intRetVal						=	objMortgage.Add(objNewMortgage);
					
					if( intRetVal > 0)			// Value inserted successfully.
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"29");
						hidFormSaved.Value		=	"1";
						hidHOLDER_ID.Value		=	objNewMortgage.HOLDER_ID.ToString();
						hidIS_ACTIVE.Value		=	"Y";
					}
					else if(intRetVal == -1)	// Duplicate code exist, insert failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"18");
						hidFormSaved.Value		=	"2";
						lblMessage.Visible = true;
						return -1;
					}
					else						// Error occured while processing, insert failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value		=	"2";
					}
					lblMessage.Visible = true;
				} 
					//Update Mortgage
				else 
				{
					//Creating the Model object for holding the Old data
					Model.Maintenance.ClsHolderInfo objOldMortgage = new Cms.Model.Maintenance.ClsHolderInfo();

					//Mortgage id for the record which is being updated
					int intHOLDER_ID 		= int.Parse(hidHOLDER_ID.Value);
					
					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldMortgage,hidOldData.Value);
					
					//Setting those values into the Model object which are not in the page
					objNewMortgage.HOLDER_ID			=	intHOLDER_ID;
					objNewMortgage.MODIFIED_BY 			=	int.Parse(GetUserId());
					objNewMortgage.LAST_UPDATED_DATETIME=	DateTime.Now;
					objNewMortgage.IS_ACTIVE			=	hidIS_ACTIVE.Value;
					
					intRetVal	=	objMortgage.Update(objOldMortgage,objNewMortgage);
					
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"18");
						hidFormSaved.Value		=	"2";
						lblMessage.Visible = true;
						return -1;
					}
					else						// Error occured while processing, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value		=	"2";
					}
					lblMessage.Visible = true;
				}
			}
			catch(Exception ex)
			{
				// Show exception message
				lblMessage.Text		=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"21") + " " + ex.Message + " Try again!" ;
				lblMessage.Visible	=	true;
				
				//Publishing the exception using the static method of Exception manager class
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value	= "2";

				return -1;
			}
			finally
			{
				if(objMortgage!= null)
				{
					objMortgage.Dispose();
				}
			}

			return 0;
		}
		#endregion

		#region function assigning the validators controls
		/// <summary>
		/// This function will set the Error Message,validation expresion of all validators controls
		/// </summary>
		private void SetPageLabels()
		{			
			
			//setting error message by calling singleton functions
			rfvHOLDER_NAME.ErrorMessage				= Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"2086");
			rfvHOLDER_ADD1.ErrorMessage				= Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"909");
			rfvHOLDER_CODE.ErrorMessage				= Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"2087");
			
			rfvHOLDER_CITY.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"56");
			rfvHOLDER_STATE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"35");
			rfvHOLDER_COUNTRY.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"33");
			rfvHOLDER_ZIP.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"37");
			

			revHOLDER_CODE.ErrorMessage 				= Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "64");
			//revHOLDER_MAIN_PHONE_NO.ErrorMessage 		= Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "14");
			revHOLDER_EMAIL.ErrorMessage				= Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"23");
			revHOLDER_EXT.ErrorMessage					= Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"25");
			//revHOLDER_FAX.ErrorMessage					= Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"15");
			//revHOLDER_MOBILE.ErrorMessage				= Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"16");
			revHOLDER_ZIP.ErrorMessage					= Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"24");
			revHOLDER_NAME.ErrorMessage					= Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"63");
		
			revHOLDER_CODE.ValidationExpression 			= aRegExpClientName;
			//revHOLDER_MAIN_PHONE_NO.ValidationExpression	= aRegExpPhone;
			revHOLDER_EMAIL.ValidationExpression			= aRegExpEmail;
			revHOLDER_EXT.ValidationExpression				= aRegExpExtn;
			//revHOLDER_FAX.ValidationExpression				= aRegExpFax;
            //revHOLDER_MOBILE.ValidationExpression			= aRegExpMobile;
            revHOLDER_ZIP.ValidationExpression = aRegExpZipBrazil; // aRegExpZip;
			revHOLDER_NAME.ValidationExpression				= aRegExpClientName;
			csvHOLDER_MEMO.ErrorMessage					  =	  Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"429");

            if (GetLanguageID() == "1")
            {
                revHOLDER_MAIN_PHONE_NO.ValidationExpression = aRegExpPhone;
                revHOLDER_FAX.ValidationExpression = aRegExpFax;
                revHOLDER_MOBILE.ValidationExpression = aRegExpMobile;
                revHOLDER_MAIN_PHONE_NO.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");
                revHOLDER_FAX.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("15");
                revHOLDER_MOBILE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "16");
            }

            else
            {
                revHOLDER_MAIN_PHONE_NO.ValidationExpression = aRegExpAgencyPhone;
                revHOLDER_FAX.ValidationExpression = aRegExpAgencyPhone;
                revHOLDER_MOBILE.ValidationExpression = aRegExpAgencyPhone;
                revHOLDER_MAIN_PHONE_NO.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1913");
                revHOLDER_FAX.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1921");
                revHOLDER_MOBILE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1922");

            }
		
		}
		#endregion

		#region SetCaption 
		/// <summary>
		/// Function for setting the label Captions by reading resource file
		/// </summary>
		private void SetCaptions()
		{
			capHOLDER_NAME.Text					= objResourceMgr.GetString("txtHOLDER_NAME");
			capHOLDER_CODE.Text					= objResourceMgr.GetString("txtHOLDER_CODE");
			capHOLDER_ADD1.Text					= objResourceMgr.GetString("txtHOLDER_ADD1");
			capHOLDER_ADD2.Text					= objResourceMgr.GetString("txtHOLDER_ADD2");
			capHOLDER_CITY.Text					= objResourceMgr.GetString("txtHOLDER_CITY");
			capHOLDER_STATE.Text				= objResourceMgr.GetString("cmbHOLDER_STATE");
			capHOLDER_COUNTRY.Text				= objResourceMgr.GetString("cmbHOLDER_COUNTRY");
			capHOLDER_ZIP.Text					= objResourceMgr.GetString("txtHOLDER_ZIP");
			capHOLDER_MAIN_PHONE_NO.Text		= objResourceMgr.GetString("txtHOLDER_MAIN_PHONE_NO");
			capHOLDER_EXT.Text					= objResourceMgr.GetString("txtHOLDER_EXT");
			capHOLDER_FAX.Text					= objResourceMgr.GetString("txtHOLDER_FAX");
			capHOLDER_EMAIL.Text				= objResourceMgr.GetString("txtHOLDER_EMAIL");
			capHOLDER_MOBILE.Text				= objResourceMgr.GetString("txtHOLDER_MOBILE");
			capHOLDER_LEGAL_ENTITY.Text			= objResourceMgr.GetString("cmbHOLDER_LEGAL_ENTITY");
			capHOLDER_TYPE.Text					= objResourceMgr.GetString("cmbHOLDER_TYPE");
			capHOLDER_MEMO.Text					= objResourceMgr.GetString("txtHOLDER_MEMO");
            btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1333");
            hidZip.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1");
			
		}

		#endregion


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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

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
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			int retVal = SaveFormValue();

			if ( retVal == 0 )
			{
				LoadData();
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
				objMortgage = new  Cms.BusinessLayer.BlCommon.ClsMortgage();

				Model.Maintenance.ClsHolderInfo  objNewMortgage;
				objNewMortgage = GetFormValues();
				string strCustomInfo ="";
				
				strCustomInfo = ";Additional Interest Name = " + objNewMortgage.HOLDER_NAME + "<BR>Code = " + objNewMortgage.HOLDER_CODE;

				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					

					objStuTransactionInfo.transactionDescription = "Additional Interest Deactivated successfully.";
					objMortgage.TransactionInfoParams = objStuTransactionInfo;
					
					objMortgage.ActivateDeactivate(hidHOLDER_ID.Value,"N",strCustomInfo);
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";
				}
				else
				{
					objStuTransactionInfo.transactionDescription = "Additional Interest Activated successfully.";
					objMortgage.TransactionInfoParams = objStuTransactionInfo;
					objMortgage.ActivateDeactivate(hidHOLDER_ID.Value,"Y",strCustomInfo);
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";
				}
				LoadData();		
				hidFormSaved.Value			=	"1";
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21")+ " - " + ex.Message + " Try again!";
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			finally
			{
				lblMessage.Visible = true;
				if(objMortgage!= null)
					objMortgage.Dispose();
			}
		}

		#endregion

		private void LoadData()
		{
			ClsMortgage objMortgage = new ClsMortgage();
			
			int holderID = Convert.ToInt32(hidHOLDER_ID.Value);
			DataSet dsMortgage = objMortgage.GetHolderByID(holderID);

			hidOldData.Value = dsMortgage.GetXml();

            if (dsMortgage!=null && dsMortgage.Tables.Count>0 && dsMortgage.Tables[0].Rows.Count>0)
            {
                hidIS_ACTIVE.Value = dsMortgage.Tables[0].Rows[0]["IS_ACTIVE"].ToString();
                if( hidIS_ACTIVE.Value=="Y" ||  hidIS_ACTIVE.Value=="N")
                  btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(hidIS_ACTIVE.Value);
            }
            DataTable dt = dsMortgage.Tables[0];

            if (dt.Rows[0]["HOLDER_COUNTRY"] != System.DBNull.Value)
                FillStates(cmbHOLDER_STATE, int.Parse(dt.Rows[0]["HOLDER_COUNTRY"].ToString()));
			
		}


        private void FillStates(DropDownList cmbSTATE, int COUNTRY_ID)
        {
            ClsStates objStates = new ClsStates();
            DataTable dtStates = objStates.GetStatesForCountry(COUNTRY_ID);
            if (dtStates != null && dtStates.Rows.Count > 0)
            {
                cmbSTATE.DataSource = dtStates;
                cmbSTATE.DataTextField = STATE_NAME;
                cmbSTATE.DataValueField = STATE_ID;
                cmbSTATE.DataBind();
                cmbSTATE.Items.Insert(0, "");
            }

        }

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public DataSet AjaxFillState(string CountryID)
        {
            try
            {
                CmsWeb.webservices.ClsWebServiceCommon obj = new CmsWeb.webservices.ClsWebServiceCommon();
                string result = "";
                result = obj.FillState(CountryID);
                DataSet ds = new DataSet();
                ds.ReadXml(new System.IO.StringReader(result));

                return ds;
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                return null;
            }
        }

	}
}
