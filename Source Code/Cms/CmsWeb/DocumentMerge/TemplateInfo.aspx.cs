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
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer.BlCommon;
using System.IO;
using System.Configuration;
using System.Reflection;
using System.Resources;

namespace Cms.CmsWeb.DocumentMerge
{
	/// <Created By->Deepak Gupta</Created>
	/// <Dated>Sep-8-2006</Dated>
	/// <Purpose>Add/Edit Template Information</Purpose>
	public class TemplateInfo : cmsbase
	{
		#region Web Control Declarations
		protected System.Web.UI.WebControls.TextBox txtTemplateName;
		protected System.Web.UI.WebControls.TextBox txtVersion;
		protected System.Web.UI.WebControls.Label lblTemplateDesc;
		protected System.Web.UI.WebControls.TextBox txtTemplateDesc;
		protected System.Web.UI.WebControls.Label lblTemplateType;
		protected System.Web.UI.WebControls.Label lblCreatedBy;
		protected System.Web.UI.WebControls.DropDownList ddlTemplateType;
		protected System.Web.UI.WebControls.DropDownList ddlCreatedBy;
		protected System.Web.UI.WebControls.Label lblLob;
		protected System.Web.UI.WebControls.DropDownList ddlLob;
		protected System.Web.UI.WebControls.Label lblAgency;
		protected System.Web.UI.WebControls.DropDownList ddlAgency;
		//protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnEdit;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.Controls.CmsButton btnCopy;
		protected System.Web.UI.WebControls.Label lblTemplateName;
		protected System.Web.UI.WebControls.Label lblVersion;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTemplateId;
        protected System.Web.UI.WebControls.Label capMessage;
		#endregion

		#region Variables Declarations
		private ClsDocumentMerge DocMerge;
		protected bool UpdateGrid=false;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvRequiredVersion;
		protected System.Web.UI.WebControls.RangeValidator rfvRangeVersion;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvRequireTemplateType;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvRequiredTemplateName;
       
		//protected Cms.CmsWeb.Controls.CmsButton btnCopy;
        //creating resource manager object (used for reading field and label mapping)
        System.Resources.ResourceManager objResourceMgr;
		protected bool DeleteFlag=true;
		#endregion

		#region Form Load

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			base.ScreenId ="349_0";
			DocMerge = new ClsDocumentMerge();
			lblMessage.Text = "";
			lblMessage.Visible = false;
            SetErrorMessages(); //sneha
            objResourceMgr = new System.Resources.ResourceManager("cms.CmsWeb.DocumentMerge.TemplateInfo", System.Reflection.Assembly.GetExecutingAssembly());
			if (!IsPostBack)
			{
				SetCaptions();
                capMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
                #region Filling Drop Downs
				DataSet DsTemp = new DataSet();
				//Line Of Business
				  DataTable dtLOBs = Cms.CmsWeb.ClsFetcher.LOBs;
                  ddlLob.DataSource = dtLOBs;
                  ddlLob.DataTextField = "LOB_DESC";
                  ddlLob.DataValueField = "LOB_ID";
                  ddlLob.DataBind();

                  /*============================================================================================
                   * SANTOSH GAUTAM : BELOW LINE TO INSERT FIRST ITEM IN DROPDOWN IS MODIFIED ON 29 OCT 2010
                   * 1. OLD VALUE => this.ddlLob.Items.Insert(0, 'All');
                   *===========================================================================================*/
                  if (ClsCommon.BL_LANG_ID =='2')
                  {
                      ListItem FirstItem = new ListItem("Todos", "0");
                      this.ddlLob.Items.Insert(0, FirstItem);
                  }
                  else
                  {
                      ListItem FirstItem = new ListItem("All", "0");
                      this.ddlLob.Items.Insert(0, FirstItem);
                  }

                  this.ddlLob.SelectedIndex = 0;
				//Agency
				DsTemp = new ClsAgency().FillAgency();
				ddlAgency.DataSource = DsTemp;
				ddlAgency.DataTextField = "AGENCY_DISPLAY_NAME";
				ddlAgency.DataValueField = "AGENCY_ID";
				ddlAgency.DataBind();
				ddlAgency.Items.Insert(0,new ListItem("","0"));

				//User
				DsTemp = DocMerge.getUserDataSet();
				ddlCreatedBy.DataSource = DsTemp;
				ddlCreatedBy.DataTextField = "USER_NAME";
				ddlCreatedBy.DataValueField = "USER_ID";
				ddlCreatedBy.DataBind();

				//Template Type
				DsTemp = DocMerge.getTemplateTypeDataSet();
				ddlTemplateType.DataSource = DsTemp;
				ddlTemplateType.DataTextField = "LOOKUP_VALUE_DESC";
				ddlTemplateType.DataValueField = "LOOKUP_UNIQUE_ID";
				ddlTemplateType.DataBind();
				ddlTemplateType.Items.Insert(0,new ListItem("",""));
				#endregion

				btnCopy.Attributes.Add("OnClick","javascript:return copy();");
				if (Request.QueryString["TEMPLATE_ID"]!=null)
				{	
					#region Template Edit Mode
					DataSet DsTemplate = new DataSet();
					string strMode = "EDIT";
					try
					{
						strMode = Request.QueryString["MODE"].ToString().Trim().ToUpper();
					}
					catch {}
					DsTemplate = DocMerge.getTemplateInfo(Request.QueryString["TEMPLATE_ID"].ToString(),GetUserId(),strMode);

                    if (DsTemplate.Tables[0].Rows[0]["IS_ACTIVE"].ToString().Trim() == "Y")
                        btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1315"); //"Deactivate";
                    else
                        btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1314"); //"Activate";

					txtTemplateName.Text = DsTemplate.Tables[0].Rows[0]["DISPLAYNAME"].ToString();
					txtTemplateDesc.Text = DsTemplate.Tables[0].Rows[0]["DESCRIPTION"].ToString();
					txtVersion.Text = DsTemplate.Tables[0].Rows[0]["VERSION"].ToString();
					
					#region Setting Dropdowns
					//lob
					ListItem lstItem  = ddlLob.Items.FindByValue(DsTemplate.Tables[0].Rows[0]["LOB"].ToString().Trim());
					if(lstItem !=null )
					{
						ddlLob.SelectedIndex =-1;
						lstItem.Selected=true;
					}
					//Agency
					ListItem lstItemAgency  = ddlAgency.Items.FindByValue(DsTemplate.Tables[0].Rows[0]["AGENCY_ID"].ToString().Trim());
					if(lstItemAgency !=null )
					{
						ddlAgency.SelectedIndex =-1;
						lstItemAgency.Selected=true;
					}

					//User
					ListItem lstItemUser  = ddlCreatedBy.Items.FindByValue(DsTemplate.Tables[0].Rows[0]["CREATED_BY"].ToString().Trim());
					if(lstItemUser !=null )
					{
						ddlCreatedBy.SelectedIndex =-1;
						lstItemUser.Selected=true;
					}

					//Template Type
					ListItem lstItemType  = ddlTemplateType.Items.FindByValue(DsTemplate.Tables[0].Rows[0]["LETTERTYPE"].ToString().Trim());
					if(lstItemType !=null )
					{
						ddlTemplateType.SelectedIndex =-1;
						lstItemType.Selected=true;
					}
					#endregion
					#endregion

					//hidTemplateId.Value = Request.QueryString["TEMPLATE_ID"].ToString();
					hidTemplateId.Value = DsTemplate.Tables[0].Rows[0]["TEMPLATE_ID"].ToString();
					
					#region //Copying Template
					if (strMode=="COPY")
					{
						UpdateGrid = true;

                        string lstrUserName = ConfigurationManager.AppSettings.Get("IUserName");
                        string lstrPassword = ConfigurationManager.AppSettings.Get("IPassWd");
                        string lstrDomain = ConfigurationManager.AppSettings.Get("IDomain");
			
						ClsAttachment lImpertionation =  new ClsAttachment();
			
						if (lImpertionation.ImpersonateUser(lstrUserName,lstrPassword,lstrDomain))
						{
                            string NewTemplateFileName = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString()) + "\\DocumentMerge\\DocMergeTemplates\\Template_" + DsTemplate.Tables[0].Rows[0]["TEMPLATE_ID"].ToString() + ".rtf";
                            string oldTemplateFileName = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString()) + "\\DocumentMerge\\DocMergeTemplates\\Template_" + Request.QueryString["TEMPLATE_ID"].ToString() + ".rtf";
				
							File.Copy(oldTemplateFileName,NewTemplateFileName,true);
                            lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "6"); // "Copy of template has been created."; //sneha
							lblMessage.Visible = true;

							lImpertionation.endImpersonation();
						}
						else
						{
                            lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "7"); // "Impersonation Failed."; //sneha
							lblMessage.Visible = true;
						}
					}
					#endregion
				}
				else
				{
					#region New Template
					btnDelete.Visible = false;
					btnActivateDeactivate.Visible = false;
					btnCopy.Visible=false;
					btnEdit.Visible = false;

					//User
					ListItem lstUser  = ddlCreatedBy.Items.FindByValue(GetUserId());
					if(lstUser !=null )
					{
						ddlCreatedBy.SelectedIndex =-1;
						lstUser.Selected=true;
					}
					#endregion 

					hidTemplateId.Value = "-1";
				}
			}

			#region Setting Button Premissions
			btnSave.CmsButtonClass					=	CmsButtonType.Write;
			btnSave.PermissionString				=	gstrSecurityXML;
			btnCopy.CmsButtonClass					=	CmsButtonType.Write;
			btnCopy.PermissionString				=	gstrSecurityXML;
			btnEdit.CmsButtonClass					=   CmsButtonType.Write; //permission made Write instead of Read by Sibin on 27 Oct 08
			btnEdit.PermissionString				=	gstrSecurityXML;
			btnDelete.CmsButtonClass				=   CmsButtonType.Delete;
			btnDelete.PermissionString				=	gstrSecurityXML;
			btnActivateDeactivate.CmsButtonClass	=   CmsButtonType.Delete;
			btnActivateDeactivate.PermissionString	=	gstrSecurityXML;
			#endregion
		}
		#endregion



        private void SetErrorMessages()
        {
            rfvRequiredTemplateName.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1"); //sneha
            rfvRequireTemplateType.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "2"); //sneha
            rfvRequiredVersion.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "3"); //sneha
            rfvRangeVersion.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "4"); //sneha
        }

        private void SetCaptions()
        {
            lblTemplateName.Text = objResourceMgr.GetString("txtTemplateName");
            lblVersion.Text = objResourceMgr.GetString("txtVersion");
            lblTemplateDesc.Text = objResourceMgr.GetString("txtTemplateDesc");
            lblTemplateType.Text = objResourceMgr.GetString("ddlTemplateType");
            lblCreatedBy.Text = objResourceMgr.GetString("ddlCreatedBy");
            lblLob.Text = objResourceMgr.GetString("ddlLob");
            lblAgency.Text = objResourceMgr.GetString("ddlAgency");
            btnCopy.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5"); //sneha
        }
		#region Button Click Events
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			string strOldTemplateId = hidTemplateId.Value.ToString();

			#region Saving Template
			string[] strTemplateInfo = new string[8];
			strTemplateInfo[0] = hidTemplateId.Value.ToString();				//Template Id
			strTemplateInfo[1] = txtTemplateName.Text.ToString();				//Template Name
			strTemplateInfo[2] = txtTemplateDesc.Text.ToString();				//Template Description
			strTemplateInfo[3] = txtVersion.Text.ToString();					//Template Version
			strTemplateInfo[4] = ddlLob.SelectedValue.ToString();				//Line Of business
			strTemplateInfo[5] = ddlAgency.SelectedValue.ToString();			//Agency
			strTemplateInfo[6] = ddlCreatedBy.SelectedValue.ToString();			//Created By User Id
			strTemplateInfo[7] = ddlTemplateType.SelectedValue.ToString();		//Template Type
			
			//Saving TemplateInfo
			hidTemplateId.Value = DocMerge.InsertUpdateTemplateInfo(strTemplateInfo);
            lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "8"); //"Template Has Been Saved Successfully."; //sneha
			lblMessage.Visible = true;
			UpdateGrid = true;
			
			btnDelete.Visible = true;
			btnActivateDeactivate.Visible = true;
			btnCopy.Visible=true;
			btnEdit.Visible = true;
			#endregion

			#region Creating Blank Template
			if (strOldTemplateId == "-1")
			{
				btnActivateDeactivate.Text = "Deactivate";
                string lstrUserName = ConfigurationManager.AppSettings.Get("IUserName");
                string lstrPassword = ConfigurationManager.AppSettings.Get("IPassWd");
                string lstrDomain = ConfigurationManager.AppSettings.Get("IDomain");
			
				ClsAttachment lImpertionation =  new ClsAttachment();
			
				if (lImpertionation.ImpersonateUser(lstrUserName,lstrPassword,lstrDomain))
				{
                    string NewTemplateFileName = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString()) + "\\DocumentMerge\\DocMergeTemplates\\Template_" + hidTemplateId.Value.ToString() + ".rtf";
                    string oldTemplateFileName = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString()) + "\\DocumentMerge\\DocMergeTemplates\\Template_0.rtf";
				
					File.Copy(oldTemplateFileName,NewTemplateFileName,true);
					lImpertionation.endImpersonation();
				}
				else
				{
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "7"); // "Impersonation Failed.";
					lblMessage.Visible = true;
				}
			}
			#endregion
		}
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			DocMerge.DeleteActivateDeactivateTemplate(hidTemplateId.Value.ToString(),"DELETE");
            lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "9");//"Template Has Been Deleted Successfully."; //sneha
			lblMessage.Visible = true;
			UpdateGrid=true;
			DeleteFlag=false;
		}
		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			DocMerge.DeleteActivateDeactivateTemplate(hidTemplateId.Value.ToString(),btnActivateDeactivate.Text.ToString().Trim().ToUpper());
            string act = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1314");
			if (btnActivateDeactivate.Text.ToString() == act)
			{
                btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1315");//"Deactivate";
                lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "10"); //"Template Has Been Activated Successfully.";//sneha
			}
			else
			{
                btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1314");//"Activate";
                lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "11");//"Template Has Been Deactivated Successfully."; //sneha
			}
			lblMessage.Visible = true;
			UpdateGrid=true;
		}
		private void btnEdit_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("TemplateLoad.aspx?Mode=EDIT&TemplateId=" + hidTemplateId.Value.ToString());
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
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion	
	}
}
