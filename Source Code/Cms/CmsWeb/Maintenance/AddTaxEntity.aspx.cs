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
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Maintenance;
using System.Reflection;
using System.Resources;

/******************************************************************************************
	<Author					:Priya Arora - >
	<Start Date				:Apr 12,2005- >
	<End Date				: - >
	<Description			: - >
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - 26/08/2005
	<Modified By			: - Anurag Verma
	<Purpose				: - Applying Null Check for buttons on aspx page
*******************************************************************************************/

namespace Cms.CmsWeb.Maintenance
{
	public class AddTaxEntity : Cms.CmsWeb.cmsbase
	{  
		int			intTAX_ID = 0;
		
		System.Resources.ResourceManager objResourceMgr;
		private Cms.BusinessLayer.BlCommon.ClsTaxEntity objTaxEntity;

		#region webForm controls declaration
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.TextBox txtTAX_ADDRESS1;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTAX_ADDRESS1;
		protected System.Web.UI.WebControls.TextBox txtTAX_ADDRESS2;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTAX_COUNTRY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTAX_STATE;
		protected System.Web.UI.WebControls.TextBox txtTAX_ZIP;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTAX_ZIP;
		protected System.Web.UI.WebControls.TextBox txtTAX_PHONE;
		protected System.Web.UI.WebControls.TextBox txtTAX_EXT;
		protected System.Web.UI.WebControls.TextBox txtTAX_FAX;
		protected System.Web.UI.WebControls.TextBox txtTAX_EMAIL;
		protected System.Web.UI.WebControls.TextBox txtTAX_WEBSITE;
		protected System.Web.UI.WebControls.DropDownList cmbTAX_STATE;
		protected System.Web.UI.WebControls.DropDownList cmbTAX_COUNTRY;
		protected System.Web.UI.WebControls.RegularExpressionValidator revTAX_EMAIL;
		protected System.Web.UI.WebControls.RegularExpressionValidator revTAX_ZIP;
		protected System.Web.UI.WebControls.RegularExpressionValidator revTAX_EXT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revTAX_PHONE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revTAX_FAX;
		protected System.Web.UI.WebControls.TextBox txtTAX_NAME;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidZipCode;
		protected System.Web.UI.WebControls.TextBox txtTAX_CITY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTAX_CITY;
		protected System.Web.UI.WebControls.RegularExpressionValidator revTAX_CODE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTAX_CODE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTAX_NAME;
		protected System.Web.UI.WebControls.RegularExpressionValidator revTAX_NAME;
		protected System.Web.UI.WebControls.RegularExpressionValidator revTAX_WEBSITE;
		protected System.Web.UI.WebControls.Label capTAX_NAME;
		protected System.Web.UI.WebControls.Label capTAX_CODE;
		protected System.Web.UI.WebControls.Label capTAX_ADDRESS1;
		protected System.Web.UI.WebControls.Label capTAX_ADDRESS2;
		protected System.Web.UI.WebControls.Label capTAX_CITY;
		protected System.Web.UI.WebControls.Label capTAX_COUNTRY;
		protected System.Web.UI.WebControls.Label capTAX_STATE;
		protected System.Web.UI.WebControls.Label capTAX_ZIP;
		protected System.Web.UI.WebControls.Label capTAX_PHONE;
		protected System.Web.UI.WebControls.Label capTAX_EXT;
		protected System.Web.UI.WebControls.Label capTAX_FAX;
		protected System.Web.UI.WebControls.Label capTAX_EMAIL;
		protected System.Web.UI.WebControls.Label capTAX_WEBSITE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTAX_ID;
		protected System.Web.UI.WebControls.TextBox txtTAX_CODE;
		#endregion
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTAX_Name;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.WebControls.Image imgZipLookup;
		protected System.Web.UI.WebControls.HyperLink hlkZipLookup;
		protected System.Web.UI.WebControls.CustomValidator csvDIV_ZIP;
        protected System.Web.UI.WebControls.Label capMessages;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTAB_TITLES;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTAX_STATE_2;

		//END:*********** Local variables to store valid control values  *************
		private string	strRowId;

	    #region"Page_Load"
		
		/// <summary>
		/// Put user code to initialize the page here
		/// </summary>
		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				Ajax.Utility.RegisterTypeForAjax(typeof(AddTaxEntity));				
				// This allows to enter extension number on page only if user has entered the phone number
                //txtTAX_PHONE.Attributes.Add("OnBlur", "javascript:DisableExt('txtTAX_PHONE','txtTAX_EXT');FormatPhone();");
                txtTAX_PHONE.Attributes.Add("OnBlur", "javascript:DisableExt('txtTAX_PHONE','txtTAX_EXT');");

				txtTAX_NAME.Attributes.Add("onblur","javascript:generateCode();");  		       
				btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
				// Added by Swarup on 30-mar-2007
				imgZipLookup.Attributes.Add("style","cursor:hand");
				base.VerifyAddress(hlkZipLookup, txtTAX_ADDRESS1,txtTAX_ADDRESS2
					, txtTAX_CITY, cmbTAX_STATE, txtTAX_ZIP);

				base.ScreenId = "36_0";

				objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddTaxEntity",System.Reflection.Assembly.GetExecutingAssembly());

                capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
                hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCon");
				//Retreiving the security XML to be used in CmsButton and CmsPanel
				btnSave.CmsButtonClass		=	Cms.CmsWeb.Controls.CmsButtonType.Write;
				btnSave.PermissionString	=	gstrSecurityXML;

				btnReset.CmsButtonClass     = Cms.CmsWeb.Controls.CmsButtonType.Write;
				btnReset.PermissionString	=	gstrSecurityXML;

				
				btnActivateDeactivate.CmsButtonClass	=   Cms.CmsWeb.Controls.CmsButtonType.Write;
				btnActivateDeactivate.PermissionString	=	gstrSecurityXML;

				btnDelete.CmsButtonClass		=	Cms.CmsWeb.Controls.CmsButtonType.Delete;
				btnDelete.PermissionString	=	gstrSecurityXML;
				

				#region If form is posted back then setting the default values
				lblMessage.Visible = false;

				if( ! Page.IsPostBack)
				{
					
					//Initializing the validators and other controls
					SetPageLabels();
                    SetCaptions();
					btnActivateDeactivate.Enabled = false;
					btnDelete.Enabled	= false;
					DataTable dt = Cms.CmsWeb.ClsFetcher.Country;

					cmbTAX_COUNTRY.DataSource		= dt;
					cmbTAX_COUNTRY.DataTextField	= COUNTRY_NAME;
					cmbTAX_COUNTRY.DataValueField	= COUNTRY_ID;
					cmbTAX_COUNTRY.DataBind();

                    ClsStates objStates = new ClsStates();
                    //DataTable dtStates = objStates.GetStatesForCountry(Convert.ToInt32(dt.Rows[0]["Country_Id"].ToString()));
                    

                    //dt = Cms.CmsWeb.ClsFetcher.ActiveState;
                    //cmbTAX_STATE.DataSource		= dt;
                    //cmbTAX_STATE.DataTextField	= STATE_NAME;
                    //cmbTAX_STATE.DataValueField	= STATE_ID;
                    //cmbTAX_STATE.DataBind();
                    //cmbTAX_STATE.Items.Insert(0,"");
                    //cmbTAX_STATE.SelectedIndex=0;
					btnActivateDeactivate.Enabled = false;

					if ( Request.QueryString["Tax_ID"] != null )
					{
						hidTAX_ID.Value = Request.QueryString["Tax_ID"].ToString();
						LoadData();
					}
					else
					{
						this.hidTAX_ID.Value = "New";
					}
                    if (ClsCommon.IsXMLResourceExists(@Request.PhysicalApplicationPath + "CmsWeb/support/PageXML/" + GetSystemId(), "AddTaxEntity.xml"))
                    {
                        setPageControls(Page, @Request.PhysicalApplicationPath + "/CmsWeb/support/PageXML/" + GetSystemId() + "/AddTaxEntity.xml");
                    }
                    DataTable dtStates = objStates.GetStatesForCountry(Convert.ToInt32(cmbTAX_COUNTRY.SelectedValue));
                    if (dtStates != null && dtStates.Rows.Count > 0)
                    {
                        cmbTAX_STATE.DataSource = dtStates;
                        cmbTAX_STATE.DataTextField = STATE_NAME;
                        cmbTAX_STATE.DataValueField = STATE_ID;
                        cmbTAX_STATE.DataBind();
                        cmbTAX_STATE.Items.Insert(0, "");
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
		
		private void LoadData()
		{
			ClsTaxEntity objEntity = new ClsTaxEntity();

			int taxID = Convert.ToInt32(hidTAX_ID.Value);

			DataSet dsEntity = objEntity.GetTaxEntityByID(taxID);

			if(dsEntity.Tables[0].Rows.Count >0)
			hidOldData.Value = dsEntity.GetXml();
			else
				hidOldData.Value ="";

            DataTable dt = dsEntity.Tables[0];

            if (dt.Rows[0]["TAX_COUNTRY"] != System.DBNull.Value)
                FillStates(cmbTAX_STATE, int.Parse(dt.Rows[0]["TAX_COUNTRY"].ToString()));


		}

		/// <summary>
		/// Fetch form's value and stores into variables.
		/// </summary>
		private ClsTaxEntityInfo GetFormValue()
		{
		    ClsTaxEntityInfo objNewTaxEntityInfo=new ClsTaxEntityInfo();
			objNewTaxEntityInfo.TAX_NAME=txtTAX_NAME.Text;
			objNewTaxEntityInfo.TAX_CODE=txtTAX_CODE.Text;
			objNewTaxEntityInfo.TAX_ADDRESS1 = txtTAX_ADDRESS1.Text;
			objNewTaxEntityInfo.TAX_ADDRESS2 = txtTAX_ADDRESS2.Text;
			objNewTaxEntityInfo.TAX_CITY=txtTAX_CITY.Text;
			objNewTaxEntityInfo.TAX_COUNTRY=cmbTAX_COUNTRY.SelectedValue;
            if (cmbTAX_STATE.SelectedValue != "")
                objNewTaxEntityInfo.TAX_STATE = cmbTAX_STATE.SelectedValue;
            else
            {
                if (!string.IsNullOrEmpty(hidTAX_STATE_2.Value) && hidTAX_STATE_2.Value != "0")
                    objNewTaxEntityInfo.TAX_STATE = hidTAX_STATE_2.Value;
            }
            //objNewTaxEntityInfo.TAX_STATE = hidTAX_STATE_2.Value;
			objNewTaxEntityInfo.TAX_ZIP=txtTAX_ZIP.Text;
			objNewTaxEntityInfo.TAX_PHONE=txtTAX_PHONE.Text;
			objNewTaxEntityInfo.TAX_EXT=txtTAX_EXT.Text;
			objNewTaxEntityInfo.TAX_FAX=txtTAX_FAX.Text;
			objNewTaxEntityInfo.TAX_EMAIL=txtTAX_EMAIL.Text;
			objNewTaxEntityInfo.TAX_WEBSITE=txtTAX_WEBSITE.Text;

			//objNewTaxEntityInfo.TransactLabel	=	MapTransactionLabel("AddTaxEntity.aspx.resx");

	        return objNewTaxEntityInfo;
		}
		

		#region Web Form Designer generated code

		override protected void OnInit(EventArgs e)
		{
			
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
		[Ajax.AjaxMethod()]
		public string AjaxFetchZipForState(int stateID, string ZipID)
		{
			CmsWeb.webservices.ClsWebServiceCommon obj =  new CmsWeb.webservices.ClsWebServiceCommon();
			string result = "";
			result = obj.FetchZipForState(stateID,ZipID);
			return result;
			
		}
		#region Save function
		/// <summary>
		/// saves the posted data into table using the business layer class (clsTaxEntity)
		/// </summary>
		/// <returns>void </returns>
		private int SaveFormValue()
		{


			// Creating Business layer object to do processing
			Cms.BusinessLayer.BlCommon.ClsTaxEntity objTaxEntity;
			objTaxEntity = new  Cms.BusinessLayer.BlCommon.ClsTaxEntity();
			
			lblMessage.Visible = true;

			try
			{
				int intRetVal;	//For retreiving the return value of business class save function
				objTaxEntity.TransactionLogRequired = true;

				//Creating the Model object for holding the New data
				Model.Maintenance.ClsTaxEntityInfo  objNewTaxEntity; 

				//Retreiving the controls values into model class object
				objNewTaxEntity = GetFormValue();
			
				//Insert new Tax Entity
				if(this.hidTAX_ID.Value.ToUpper() == "NEW")
				{
					//Mapping feild and Lebel to maintain the transction log into the database.
					objNewTaxEntity.TAX_ID			=	0;
					objNewTaxEntity.CREATED_BY		=	int.Parse(GetUserId());
					objNewTaxEntity.CREATED_DATETIME=	DateTime.Now;
					intRetVal						=	objTaxEntity.Add(objNewTaxEntity);
					
					if( intRetVal > 0)			// Value inserted successfully.
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"29");
						hidFormSaved.Value		=	"1";
						hidTAX_ID.Value			=	objNewTaxEntity.TAX_ID.ToString();
						hidIS_ACTIVE.Value		=	"Y";
					}
					else if(intRetVal == -1)	// Duplicate code exist, insert failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"18");
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
				//Update Tax Entity
				else 
				{

					//Creating the Model object for holding the Old data
					Model.Maintenance.ClsTaxEntityInfo objOldTaxEntity = new Model.Maintenance.ClsTaxEntityInfo();

					//tax id for the record which is being updated
					intTAX_ID	= int.Parse(hidTAX_ID.Value);
					
					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldTaxEntity,hidOldData.Value);
					
					//Setting those values into the Model object which are not in the page
					objNewTaxEntity.IS_ACTIVE				=	hidIS_ACTIVE.Value;
					objNewTaxEntity.TAX_ID			        =	intTAX_ID;
					objNewTaxEntity.MODIFIED_BY				=	int.Parse(GetUserId());
					objNewTaxEntity.LAST_UPDATED_DATETIME	=	DateTime.Now;
					
					//Setting those values into the Model object which are not in the page
					objOldTaxEntity.TAX_ID		=	intTAX_ID;
					
					intRetVal						=	objTaxEntity.Update(objOldTaxEntity,objNewTaxEntity);
					
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"18");
						hidFormSaved.Value		=	"2";
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

				return 0;
			}
			finally
			{
				if(objTaxEntity!= null)
				{
					objTaxEntity.Dispose();
				}
			}
			
			return 1;
			

		}
		#endregion

		#region activate deactivate
		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			//Here we will call the Activate/Deactivate method of ClsTax Entity class
			//To activate or deactivate the record
			try
			{
				stuTransactionInfo objStuTransactionInfo = new  stuTransactionInfo();
				objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
				
				objStuTransactionInfo.loggedInUserName = GetUserName();
				strRowId		=	hidTAX_ID.Value;
				ClsTaxEntity objTaxEntity = new  ClsTaxEntity();
				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					objStuTransactionInfo.transactionDescription = "Tax Entity Deactivated Successfully.";
					objTaxEntity.TransactionInfoParams = objStuTransactionInfo;
					objTaxEntity.ActivateDeactivate(strRowId,"N");
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";
				}
				else
				{
					objStuTransactionInfo.transactionDescription = "Tax Entity Activated Successfully.";
					objTaxEntity.TransactionInfoParams = objStuTransactionInfo;
					objTaxEntity.ActivateDeactivate(strRowId,"Y");
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";
				}
				LoadData();
				hidFormSaved.Value		=	"1";
				lblMessage.Visible		=	true;
			}
				
			catch(Exception ex)
			{
				lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"21") + "\n" + ex.Message + "\n Try again!"; 
				lblMessage.Visible		=	true;
				//Publishing the exception using the static method of Exception manager class
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				
			}
		}

		#endregion

		#region "Function SetPageLabels()"

		/// <summary>
		/// Setting validation expression for different validation control
		/// and setting error message 
		/// </summary>
		private void SetPageLabels()
		{
			
			try
			{
				//setting validation expression for different validation control
                this.revTAX_ZIP.ValidationExpression = aRegExpZipBrazil; // aRegExpZip;
                //this.revTAX_PHONE.ValidationExpression=aRegExpPhone;
                //this.revTAX_FAX.ValidationExpression=aRegExpFax;
				this.revTAX_EXT.ValidationExpression=aRegExpExtn;
				this.revTAX_EMAIL.ValidationExpression=aRegExpEmail;
				this.revTAX_WEBSITE.ValidationExpression=aRegExpSiteUrl;
				

				this.revTAX_NAME.ValidationExpression=aRegExpClientName;
				this.revTAX_CODE.ValidationExpression=aRegExpClientName;
				//this.revTAX_CITY.ValidationExpression=aRegExpClientName;
				
			
				this.revTAX_ZIP.ErrorMessage   =	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"24");  
                //this.revTAX_PHONE.ErrorMessage =	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14"); 
				this.revTAX_EXT.ErrorMessage   =	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("25");
				this.revTAX_EMAIL.ErrorMessage =	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("23"); 
				//this.revTAX_FAX.ErrorMessage   =	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("15");
				this.rfvTAX_NAME.ErrorMessage   =	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"34");			
				this.rfvTAX_CODE.ErrorMessage   =	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"38");
				this.rfvTAX_ADDRESS1.ErrorMessage=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"909");
				this.rfvTAX_COUNTRY.ErrorMessage =	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"33");
				this.rfvTAX_STATE.ErrorMessage  =	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"35");
				this.rfvTAX_CITY.ErrorMessage   =	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"56");
				this.revTAX_NAME.ErrorMessage   =	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"63");
				this.revTAX_CODE.ErrorMessage   =	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"64");
				//this.revTAX_CITY.ErrorMessage  =	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"39");
				this.rfvTAX_ZIP.ErrorMessage =	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"37");	
				this.revTAX_WEBSITE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");

             

                if (GetLanguageID() == "1")
                {
                    this.revTAX_PHONE.ValidationExpression = aRegExpPhone;
                    this.revTAX_FAX.ValidationExpression = aRegExpFax;
                    this.revTAX_PHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");
                    this.revTAX_FAX.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("15");
                }

                else
                {
                    this.revTAX_PHONE.ValidationExpression = aRegExpAgencyPhone;
                    this.revTAX_FAX.ValidationExpression = aRegExpAgencyPhone;                   
                    this.revTAX_PHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1913");
                    this.revTAX_FAX.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1921");
                    
                }
	   
			}
			catch(Exception objExcep)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
			}
			

		}
		#endregion 



		private void btnSave_Click(object sender, System.EventArgs e)
		{
			
			int result = SaveFormValue();
			
			if ( result == 1 )
			{
				LoadData();
				RegisterScript();
			}
		}

		#region Set captions
		/// <summary>
		/// Function for setting the label Captions by reading resource file
		/// </summary>
		private void SetCaptions()
		{
			capTAX_NAME.Text						    =		objResourceMgr.GetString("txtTAX_NAME");
			capTAX_CODE.Text						    =		objResourceMgr.GetString("txtTAX_CODE");
			capTAX_ADDRESS1.Text						=		objResourceMgr.GetString("txtTAX_ADDRESS1");
			capTAX_ADDRESS2.Text					    =		objResourceMgr.GetString("txtTAX_ADDRESS2");
			capTAX_CITY.Text				            =		objResourceMgr.GetString("txtTAX_CITY");
			capTAX_COUNTRY.Text					        =		objResourceMgr.GetString("cmbTAX_COUNTRY");
			capTAX_STATE.Text						    =		objResourceMgr.GetString("cmbTAX_STATE");
			capTAX_ZIP.Text					            =		objResourceMgr.GetString("txtTAX_ZIP");
			capTAX_PHONE.Text				 	        =		objResourceMgr.GetString("txtTAX_PHONE");
			capTAX_EXT.Text						        =		objResourceMgr.GetString("txtTAX_EXT");
			capTAX_FAX.Text					            =		objResourceMgr.GetString("txtTAX_FAX");
			capTAX_EMAIL.Text					     	=		objResourceMgr.GetString("txtTAX_EMAIL");
			capTAX_WEBSITE.Text						    =		objResourceMgr.GetString("txtTAX_WEBSITE");
            btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1333");
            hidZipCode.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1");
			#endregion

		}

			/// <summary>
			/// Adds script to refresh web grid
			/// </summary>
			private void RegisterScript()
			{
                if (!ClientScript.IsStartupScriptRegistered("Refresh"))
				{
					string strCode = "<script>" + 
										"RefreshWebGrid(1," + this.hidTAX_ID.Value + ");" +
									"</script>";

					ClientScript.RegisterStartupScript(this.GetType(),"Refresh",strCode);

				}
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

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			int intRetVal;	
			int intTaxID = int.Parse(hidTAX_ID.Value);
			objTaxEntity = new Cms.BusinessLayer.BlCommon.ClsTaxEntity();
			intRetVal = objTaxEntity.Delete(intTaxID);
			if(intRetVal>0)
			{
				lblDelete.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("338"); 
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
            catch// (Exception ex)
            {
                return null;
            }
        }
		
	}
}


