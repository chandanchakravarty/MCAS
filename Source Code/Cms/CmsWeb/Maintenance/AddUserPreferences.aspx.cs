/******************************************************************************************
<Author					: -   Mohit Gupta
<Start Date				: -	  5/24/2005 1:40:20 PM
<End Date				: -	
<Description			: - 	
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - This file is used to
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
using System.Xml;
using Cms.Model.Maintenance;
using Cms.ExceptionPublisher;
using Cms.CmsWeb.Controls;
using System.Resources;
using System.Reflection;


namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for AddUserPreferences.
	/// </summary>
	public class AddUserPreferences : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.WebControls.Label capUSER_COLOR_SCHEME;
		protected System.Web.UI.WebControls.DropDownList cmbUSER_COLOR_SCHEME;
        protected System.Web.UI.WebControls.DropDownList cmbLANG_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvUSER_COLOR_SCHEME;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.WebControls.Label capUSER_CONFIRM_PWD;
		protected System.Web.UI.WebControls.TextBox txtUSER_CONFIRM_PWD;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvUSER_CONFIRM_PWD;
		protected System.Web.UI.WebControls.CompareValidator cvPassword;
		protected System.Web.UI.WebControls.Label capGRID_SIZE;
		protected System.Web.UI.WebControls.TextBox txtGRID_SIZE;
        protected System.Web.UI.WebControls.Label capMessages;
        protected System.Web.UI.WebControls.Label capLANG_ID;
		//string oldXML;
	    private string strRowId, strFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidUSER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPassword;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfv_GRID_SIZE;
		protected System.Web.UI.WebControls.RegularExpressionValidator rev_GRID_SIZE;
		protected System.Web.UI.WebControls.RangeValidator rv_GRID_SIZE;
		protected System.Web.UI.WebControls.DropDownList cmbGRID_SIZE;
		public string strCalledFrom="";
		//string CalledFrom = "";
		//string CALLAGENCY = "";
		ClsUser  objUser ;
        System.Resources.ResourceManager objResourceMgr;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Request.QueryString["CALLAGENCY"] != null || Request.QueryString["CALLAGENCY"] != "")
			{
				strCalledFrom				=	Request.QueryString["CALLAGENCY"];
			}
			if(strCalledFrom == "AGENCY")
			{
				base.ScreenId="10_1_1";
			}   
			else
				base.ScreenId ="25_1";

            objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddUserPreferences", System.Reflection.Assembly.GetExecutingAssembly());
			
			// Put user code to initialize the page here
			btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");

			// phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
			//base.ScreenId="25_1";
			lblMessage.Visible = false;
			SetErrorMessages();
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass=CmsButtonType.Write;
			btnReset.PermissionString=gstrSecurityXML;

			//btnActivateDeactivate.CmsButtonClass=CmsButtonType.Write;
			//btnActivateDeactivate.PermissionString=gstrSecurityXML;

			btnSave.CmsButtonClass=CmsButtonType.Write;
			btnSave.PermissionString=gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			
			
			if(!Page.IsPostBack)
			{
				SetHiddenValue();
				GetOldDataXML();
				SetCaptions();
                //added by Chetna
                FillDDLLanguage();		
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

		
		private void SetHiddenValue()
		{			
			hidUSER_ID.Value=Request.QueryString["USERID"].ToString();					
		}

		private void SetErrorMessages()
		{			
			//rev_GRID_SIZE.ValidationExpression=aRegExpInteger;
			rfvUSER_COLOR_SCHEME.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
			//rfv_GRID_SIZE.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"10");
			//rev_GRID_SIZE.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"12");
			//rv_GRID_SIZE.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"11");
		}

		private void GetOldDataXML()
		{
			hidOldData.Value = ClsUser.GetXmlForUserPreferences(int.Parse(hidUSER_ID.Value));
		}
		
		private void SetCaptions()
        {
            capGRID_SIZE.Text = objResourceMgr.GetString("cmbGRID_SIZE");
            capUSER_COLOR_SCHEME.Text = objResourceMgr.GetString("cmbUSER_COLOR_SCHEME");
            capLANG_ID.Text = objResourceMgr.GetString("cmbLANG_ID");
			
		}

        //Added by Chetna on 26th Feb,10
        //Start

        private void FillDDLLanguage()
        {
            objUser = new ClsUser();
            cmbLANG_ID.DataSource = objUser.GetDDLLanguage();
            cmbLANG_ID.DataTextField = objUser.GetDDLLanguage().Tables[0].Columns["LANG_NAME"].ToString();
            cmbLANG_ID.DataValueField = objUser.GetDDLLanguage().Tables[0].Columns["LANG_ID"].ToString();
            cmbLANG_ID.SelectedIndex = 0;
            cmbLANG_ID.DataBind();
            // Color Bind
            cmbUSER_COLOR_SCHEME.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("COLRS");
            cmbUSER_COLOR_SCHEME.DataTextField = "LookupDesc";
            cmbUSER_COLOR_SCHEME.DataValueField = "LookupCode";
            cmbUSER_COLOR_SCHEME.DataBind();
        }

        
        //End
		
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsUserInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsUserInfo objUserInfo;
			objUserInfo = new ClsUserInfo();
			objUserInfo.USER_LOGIN_ID	=hidUSER_ID.Value;
			objUserInfo.USER_COLOR_SCHEME=	cmbUSER_COLOR_SCHEME.SelectedValue.ToString();//GetColorScheme()==""?"1":GetColorScheme();

            //Added by Chetna
            objUserInfo.LANG_ID = Convert.ToInt32(cmbLANG_ID.SelectedValue);

			//objUserInfo.GRID_SIZE=int.Parse(txtGRID_SIZE.Text);
			objUserInfo.GRID_SIZE=int.Parse(cmbGRID_SIZE.SelectedValue);
			strFormSaved	=	hidFormSaved.Value;
			if (hidOldData.Value != "")
			{
				strRowId=hidUSER_ID.Value;
			}
			else
			{
				strRowId="NEW";	
			}			
			return objUserInfo;
		}	
		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	
				//For retreiving the return value of business class save function
				objUser = new  ClsUser();

				//Retreiving the form values into model class object
				ClsUserInfo objUserInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objUserInfo.CREATED_BY = int.Parse(GetUserId());
					objUserInfo.CREATED_DATETIME = DateTime.Now;

					//Calling the add method of business layer class
					intRetVal = objUser.Add(objUserInfo);

					if(intRetVal>0)
					{
						hidUSER_ID.Value=objUserInfo.USER_ID.ToString();
					   	lblMessage.Text=ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value="1";
						SetColorScheme(cmbUSER_COLOR_SCHEME.SelectedValue.ToString());
						//<Mohit Gupta>  31-May-2005 : START : <Session object is added for setting the size of grid on index page.>
						//Session["GridSize"]=objUserInfo.GRID_SIZE.ToString();
						//<Mohit Gupta>  31-May-2005 : END
						//hidIS_ACTIVE.Value = "Y";
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text=ClsMessages.GetMessage(base.ScreenId,"1");
						hidFormSaved.Value="2";
					}
					else
					{
						lblMessage.Text=ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value="2";
					}
					lblMessage.Visible = true;
					hidOldData.Value = ClsUser.GetXmlForUserPreferences(int.Parse(hidUSER_ID.Value));
				} // end save case
				else //UPDATE CASE
				{

					//Creating the Model object for holding the Old data
					ClsUserInfo objOldUserInfo;
					objOldUserInfo = new ClsUserInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldUserInfo,hidOldData.Value);

					
					objUserInfo.USER_ID = int.Parse(strRowId);
					objUserInfo.MODIFIED_BY = int.Parse(GetUserId());
					objUserInfo.LAST_UPDATED_DATETIME = DateTime.Now;

                   					
					intRetVal	= objUser.UpdateUserPreferences(objOldUserInfo,objUserInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"478");
						hidFormSaved.Value		=	"1";
						//Session object is added for setting the size of grid on index page.
						//Session["GridSize"]=objUserInfo.GRID_SIZE.ToString();
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
						hidFormSaved.Value		=	"2";
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value		=	"2";
					}
					//					}
					lblMessage.Visible = true;
					hidOldData.Value = ClsUser.GetXmlForUserPreferences(int.Parse(hidUSER_ID.Value));
				}
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
				//Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objUser!= null)
				objUser.Dispose();
			}
		}
		
	}
}
