/******************************************************************************************
<Author				: - Pradeep
<Start Date			: -	Apr 26, 2005
<End Date			: -	March 31, 2005
<Description		: - Add/update form for Customer AKA/DBA
<Review Date		: - 
<Reviewed By		: - 	
Modification History

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
using Cms.Model;
using Cms.Model.Client;
using Cms.BusinessLayer.BlClient;
using Cms.BusinessLayer.BlCommon;
using System.Resources;

namespace Cms.Client.Aspx
{
	/// <summary>
	/// Summary description for AddAkaDba.
	/// </summary>
	public class AddAkaDba : clientbase
	{
		protected System.Web.UI.WebControls.Label capNameType;
		protected System.Web.UI.WebControls.Label capName;
		protected System.Web.UI.WebControls.Label capAddress;
		protected System.Web.UI.WebControls.TextBox txtAKADBA_NAME;
		protected System.Web.UI.WebControls.DropDownList cmbAKADBA_TYPE;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAKADBA_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAKADBA_NAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAKADBA_ADD;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.WebControls.Label capCity;
		protected System.Web.UI.WebControls.Label capMemo;
		protected System.Web.UI.WebControls.TextBox txtAKADBA_MEMO;
		protected System.Web.UI.WebControls.Label capDisplayOrder;
		protected System.Web.UI.WebControls.TextBox txtAKADBA_DISP_ORDER;
		protected System.Web.UI.WebControls.Label capNameAppears;
		protected System.Web.UI.WebControls.CheckBox chkAKADBA_NAME_ON_FORM;
		protected System.Web.UI.WebControls.Label capLegalEntityCode;
		protected System.Web.UI.WebControls.DropDownList cmbAKADBA_LEGAL_ENTITY_CODE;
		protected System.Web.UI.WebControls.Label capEmail;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCUSTOMER_Email;
		protected System.Web.UI.WebControls.TextBox txtAKADBA_EMAIL;
		protected System.Web.UI.WebControls.Label capZip;
		protected System.Web.UI.WebControls.RegularExpressionValidator revAKADBA_ZIP;
		protected System.Web.UI.WebControls.TextBox txtAKADBA_ZIP;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAKADBA_ZIP;
		protected System.Web.UI.WebControls.Label capState;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAKADBA_STATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAKADBA_EMAIL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAKADBA_CITY;
		protected System.Web.UI.WebControls.DropDownList cmbAKADBA_STATE;
		protected System.Web.UI.WebControls.Label capCountry;
		protected System.Web.UI.WebControls.Label capAddress2;
		
		protected System.Web.UI.WebControls.DropDownList cmbAKADBA_COUNTRY;
	
		protected System.Web.UI.WebControls.TextBox txtAKADBA_ADD2;
		protected System.Web.UI.WebControls.TextBox txtAKADBA_ADD;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAKADBA_COUNTRY;
		protected System.Web.UI.WebControls.Label capWebsite;
		protected System.Web.UI.WebControls.TextBox txtAKADBA_WEBSITE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revAKADBA_WEBSITE;
		protected Cms.CmsWeb.Controls.CmsButton btnBack;
		protected System.Web.UI.WebControls.RangeValidator rngAKADBA_DISP_ORDER;
		protected System.Web.UI.WebControls.Label capPullCustomerAddress;
		protected Cms.CmsWeb.Controls.CmsButton btnPullCustomerAddress;
		protected System.Web.UI.WebControls.TextBox txtAKADBA_CITY;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			if(Request.QueryString["CalledFrom"] != null && Request.QueryString["CalledFrom"] != "")
			{
				base.ScreenId			= "134_1_0";//can be Alpha Numeric 
				  
			}
			else
			{
				base.ScreenId			= "192_1_0";//can be Alpha Numeric 
			}

			//base.ScreenId			= "134_1_0";//can be Alpha Numeric 
			btnBack.Attributes.Add("onclick","javascript:return DoBack();");
			
			btnBack.CmsButtonClass					=	Cms.CmsWeb.Controls.CmsButtonType.Read;
			btnBack.PermissionString				=	gstrSecurityXML;	

			btnReset.CmsButtonClass					=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnReset.PermissionString				=	gstrSecurityXML;	
				
			btnSave.CmsButtonClass					=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnSave.PermissionString				=	gstrSecurityXML;	
			
			btnPullCustomerAddress.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnPullCustomerAddress.PermissionString	=	gstrSecurityXML;
			

			base.RequiredPullCustAdd(txtAKADBA_ADD, txtAKADBA_ADD2, txtAKADBA_CITY
				, cmbAKADBA_COUNTRY, cmbAKADBA_STATE, txtAKADBA_ZIP
				, btnPullCustomerAddress);

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
				
			if ( !Page.IsPostBack )
			{
				
				hidFormSaved.Value = "0";
				hidCustomerID.Value = Session["AKACustomerID"].ToString();

				SetCaptions();
				
				SetValidators();

				LoadDropdowns();
				
				//chkAKADBA_NAME_ON_FORM.Attributes.Add("onClick","javascript:EnableDisable();");
				btnReset.Attributes.Add("onclick","javascript:return ResetForm();");	
				this.txtAKADBA_WEBSITE.Attributes.Add("onkeydown","javascript:txtAKADBA_WEBSITE_OnChange()");
				//this.txtAKADBA_WEBSITE.Attributes.Add("onblur","javascript:txtAKADBA_WEBSITE_OnChange()");

				//For update
				if ( Request.QueryString["AKADBA_ID"] != null )
				{
					ViewState["AkaDbaID"] = Request.QueryString["AKADBA_ID"];
					LoadData();
				}
				else	//For insert
				{
					
				}

				if ( Request.QueryString["CustomerID"] != null )
				{
					ViewState["CustomerID"] = Request.QueryString["CustomerID"];
				}

				//Set focus to control
				this.SetFocus("cmbAKADBA_TYPE");
				

			}
		}
		
		/// <summary>
		/// Populates all the drop downs in the page
		/// </summary>
		private void LoadDropdowns()
		{
			//Name types
			cmbAKADBA_TYPE.DataTextField = "LookupDesc";
			cmbAKADBA_TYPE.DataValueField = "LookupID";

			cmbAKADBA_TYPE.DataSource = ClsCommon.GetLookup("NAMTP");
			cmbAKADBA_TYPE.DataBind();
			
			//States
			DataTable dt = Cms.CmsWeb.ClsFetcher.State;
			cmbAKADBA_STATE.DataSource = dt;
			cmbAKADBA_STATE.DataTextField = STATE_NAME;
			cmbAKADBA_STATE.DataValueField = STATE_ID;
			cmbAKADBA_STATE.DataBind();
			
			//Country
			DataTable dtCountry = Cms.CmsWeb.ClsFetcher.Country;
			cmbAKADBA_COUNTRY.DataSource = dtCountry;
			cmbAKADBA_COUNTRY.DataTextField = COUNTRY_NAME;
			cmbAKADBA_COUNTRY.DataValueField = COUNTRY_ID;
			cmbAKADBA_COUNTRY.DataBind();

			//Legal entity code
			cmbAKADBA_LEGAL_ENTITY_CODE.DataTextField = "LookupDesc";
			cmbAKADBA_LEGAL_ENTITY_CODE.DataValueField = "LookupID";

			cmbAKADBA_LEGAL_ENTITY_CODE.DataSource = ClsCommon.GetLookup("LECD");
			cmbAKADBA_LEGAL_ENTITY_CODE.DataBind();

		}
		
		
		/// <summary>
		/// Retrieves data from database for update
		/// </summary>
		private void LoadData()
		{
			ListItem listItem = null;
			ClsAkaDba objAkaDba = new ClsAkaDba();

			DataSet dsAkaDba = objAkaDba.GetAkaDbaByID(Convert.ToInt32(ViewState["AkaDbaID"]));
			
			if ( dsAkaDba.Tables[0].Rows.Count == 0 ) 
			{
				return;
			}

			DataTable dt = dsAkaDba.Tables[0];
			
			//Save the old data
			hidOldData.Value = ClsCommon.GetXML(dt);

			//Populate form fields
			if ( dt.Rows[0]["AKADBA_TYPE"] != DBNull.Value )
			{
				listItem = cmbAKADBA_TYPE.Items.FindByValue(Convert.ToString(dt.Rows[0]["AKADBA_TYPE"]));
				cmbAKADBA_TYPE.SelectedIndex= cmbAKADBA_TYPE.Items.IndexOf(listItem);	
			}

			if ( dt.Rows[0]["AKADBA_NAME"] != DBNull.Value )
			{
				txtAKADBA_NAME.Text = Convert.ToString(dt.Rows[0]["AKADBA_NAME"]);
			}

			if ( dt.Rows[0]["AKADBA_ADD"] != DBNull.Value )
			{
				txtAKADBA_ADD.Text= Convert.ToString(dt.Rows[0]["AKADBA_ADD"]);
			}

			if ( dt.Rows[0]["AKADBA_ADD2"] != DBNull.Value )
			{
				txtAKADBA_ADD2.Text= Convert.ToString(dt.Rows[0]["AKADBA_ADD2"]);
			}

			if ( dt.Rows[0]["AKADBA_CITY"] != DBNull.Value )
			{
				txtAKADBA_CITY.Text = Convert.ToString(dt.Rows[0]["AKADBA_CITY"]);
			}
			
			if ( dt.Rows[0]["AKADBA_STATE"] != DBNull.Value )
			{
				listItem = cmbAKADBA_STATE.Items.FindByValue(Convert.ToString(dt.Rows[0]["AKADBA_STATE"]));
				cmbAKADBA_STATE.SelectedIndex = cmbAKADBA_STATE.Items.IndexOf(listItem);
			}
			
			if ( dt.Rows[0]["AKADBA_COUNTRY"] != DBNull.Value )
			{
				listItem = cmbAKADBA_COUNTRY.Items.FindByValue(Convert.ToString(dt.Rows[0]["AKADBA_COUNTRY"]));
				cmbAKADBA_COUNTRY.SelectedIndex = cmbAKADBA_STATE.Items.IndexOf(listItem);
			}

			if ( dt.Rows[0]["AKADBA_ZIP"] != DBNull.Value )
			{
				txtAKADBA_ZIP.Text = Convert.ToString(dt.Rows[0]["AKADBA_ZIP"]);
			}
	
			if ( dt.Rows[0]["AKADBA_WEBSITE"] != DBNull.Value )
			{
				txtAKADBA_WEBSITE.Text = Convert.ToString(dt.Rows[0]["AKADBA_WEBSITE"]);
			}

			if ( dt.Rows[0]["AKADBA_EMAIL"] != DBNull.Value )
			{
				txtAKADBA_EMAIL.Text = Convert.ToString(dt.Rows[0]["AKADBA_EMAIL"]);
			}

			if ( dt.Rows[0]["AKADBA_MEMO"] != DBNull.Value )
			{
				txtAKADBA_MEMO.Text = Convert.ToString(dt.Rows[0]["AKADBA_MEMO"]);
			}

			if ( dt.Rows[0]["AKADBA_LEGAL_ENTITY_CODE"] != DBNull.Value )
			{
				listItem = cmbAKADBA_LEGAL_ENTITY_CODE.Items.FindByValue(Convert.ToString(dt.Rows[0]["AKADBA_LEGAL_ENTITY_CODE"]));
				cmbAKADBA_LEGAL_ENTITY_CODE.SelectedIndex = cmbAKADBA_LEGAL_ENTITY_CODE.Items.IndexOf(listItem);	
			}

			if ( dt.Rows[0]["AKADBA_NAME_ON_FORM"] != DBNull.Value )
			{
				string nameAppears = Convert.ToString(dt.Rows[0]["AKADBA_NAME_ON_FORM"]);
				chkAKADBA_NAME_ON_FORM.Checked = nameAppears == "Y" ? true : false;	
			}
			
			if ( dt.Rows[0]["AKADBA_DISP_ORDER"] != DBNull.Value )
			{
				txtAKADBA_DISP_ORDER.Text = Convert.ToString(dt.Rows[0]["AKADBA_DISP_ORDER"]);
			}
			
			//Enable/disable text box for Display order
			//txtAKADBA_DISP_ORDER.Enabled = chkAKADBA_NAME_ON_FORM.Checked;
			

		}
		
		/// <summary>
		/// Sets the error messages and validation expressions in the validators
		/// </summary>
		private void SetValidators()
		{
			revCUSTOMER_Email.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage("G","124");
			revCUSTOMER_Email.ValidationExpression = aRegExpEmail;

			//rfvAKADBA_EMAIL.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage("G","61");
			rfvAKADBA_TYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"1");
			rfvAKADBA_NAME.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"2");
			rfvAKADBA_ADD.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"32");
			rfvAKADBA_STATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"35");
			
			rfvAKADBA_ZIP.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"37");
			rfvAKADBA_CITY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"56");
			rfvAKADBA_EMAIL.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"61");
			
			revAKADBA_ZIP.ValidationExpression = aRegExpZip;
			revAKADBA_ZIP.ErrorMessage =  Cms.CmsWeb.ClsMessages.GetMessage("G","24");

			this.revAKADBA_WEBSITE.ValidationExpression = aRegExpSiteUrl;
			this.revAKADBA_WEBSITE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage("G","85");

			this.rngAKADBA_DISP_ORDER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"3");

			this.rfvAKADBA_COUNTRY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage("G","33");
		}
		
		/// <summary>
		/// Sets the text of the labels
		/// </summary>
		private void SetCaptions()
		{
			ResourceManager objResourceMgr = new System.Resources.ResourceManager("Cms.Client.Aspx.AddAkaDba",System.Reflection.Assembly.GetExecutingAssembly());
			
			try
			{
				capNameType.Text = objResourceMgr.GetString("cmbAKADBA_TYPE");
				capName.Text = objResourceMgr.GetString("txtAKADBA_NAME");
				capAddress.Text = objResourceMgr.GetString("txtAKADBA_ADD");
				capAddress2.Text = objResourceMgr.GetString("txtAKADBA_ADD2");
				capCity.Text = objResourceMgr.GetString("txtAKADBA_CITY");
				capState.Text = objResourceMgr.GetString("cmbAKADBA_STATE");
				capCountry.Text	= objResourceMgr.GetString("cmbAKADBA_COUNTRY");
				capZip.Text = objResourceMgr.GetString("txtAKADBA_ZIP");
				capWebsite.Text = objResourceMgr.GetString("txtAKADBA_WEBSITE");
				capEmail.Text = objResourceMgr.GetString("txtAKADBA_EMAIL");
				capLegalEntityCode.Text = objResourceMgr.GetString("cmbAKADBA_LEGAL_ENTITY_CODE");
				capDisplayOrder.Text = objResourceMgr.GetString("txtAKADBA_DISP_ORDER");
				capMemo.Text = objResourceMgr.GetString("txtAKADBA_MEMO");
			}
			catch(Exception ex)
			{
				lblMessage.Visible = true;
				lblMessage.Text = ex.Message;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}

		}

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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			Save();
		}
		
		/// <summary>
		/// Inserts/Updates the record in the database
		/// </summary>
		private void Save()
		{
			//BL class
			ClsAkaDba objAkaDba = new ClsAkaDba();
			
			//Model class
			ClsAkaDbaInfo objInfo = new ClsAkaDbaInfo();

			objInfo.AKADBA_ADD = txtAKADBA_ADD.Text.Trim();
			objInfo.AKADBA_ADD2 = txtAKADBA_ADD2.Text.Trim();
			objInfo.AKADBA_CITY = txtAKADBA_CITY.Text.Trim();

			if ( txtAKADBA_DISP_ORDER.Text.Trim() != "" )
			{
				objInfo.AKADBA_DISP_ORDER = Convert.ToInt32(txtAKADBA_DISP_ORDER.Text.Trim());
			}

			objInfo.AKADBA_EMAIL = txtAKADBA_EMAIL.Text.Trim();
			objInfo.AKADBA_LEGAL_ENTITY_CODE = Convert.ToInt32(cmbAKADBA_LEGAL_ENTITY_CODE.SelectedValue);
			objInfo.AKADBA_MEMO = txtAKADBA_MEMO.Text.Trim();
			objInfo.AKADBA_NAME = txtAKADBA_NAME.Text.Trim();
			objInfo.AKADBA_NAME_ON_FORM = chkAKADBA_NAME_ON_FORM.Checked ? "Y" : "N";;
			objInfo.AKADBA_STATE = Convert.ToInt32(cmbAKADBA_STATE.SelectedValue);
			objInfo.AKADBA_COUNTRY = Convert.ToInt32(cmbAKADBA_COUNTRY.SelectedValue);
			objInfo.AKADBA_TYPE = Convert.ToInt32(cmbAKADBA_TYPE.SelectedValue);
			objInfo.AKADBA_WEBSITE = txtAKADBA_WEBSITE.Text.Trim();
			objInfo.AKADBA_ZIP = txtAKADBA_ZIP.Text.Trim();
			objInfo.CREATED_BY = Convert.ToInt32(GetUserId());
			objInfo.MODIFIED_BY = Convert.ToInt32(GetUserId());
		
			lblMessage.Visible = true;	

			try
			{
				//Insert
				if ( ViewState["AkaDbaID"] == null )
				{
					ViewState["CustomerID"] = 1;
					objInfo.CUSTOMER_ID = Convert.ToInt32(Session["AKACustomerID"]);
					int akaDbaID = objAkaDba.Add(objInfo);
				
					ViewState["AkaDbaID"] = akaDbaID;
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","29");

				}
				else	//Update
				{
					objInfo.AKADBA_ID = Convert.ToInt32(ViewState["AkaDbaID"]);
					objAkaDba.Update(objInfo);
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","31");
				}

				//Load the inserted/updated record
				LoadData();

				hidFormSaved.Value = "1";

			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message + ex.InnerException.Message;
				
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			
				return;
			}
		
			//Add script to refresh web grid
			if (!ClientScript.IsStartupScriptRegistered("Refresh"))
			{
				string strCode = "<script>RefreshWebGrid(1," + Convert.ToString(ViewState["AkaDbaID"]) + ")</script>";

				ClientScript.RegisterStartupScript(this.GetType(),"Refresh",strCode);

			}
		
			

		}
	}

}