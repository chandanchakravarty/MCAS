/* ***************************************************************************************
   Author		: Swarup Pal
   Creation Date: 22-Feb-2007
   Last Updated : 
   Reviewed By	: 
   Purpose		: 
   Comments		: 
   ------------------------------------------------------------------------------------- 
   History	Date	     Modified By		Description
   
   ------------------------------------------------------------------------------------- 
   *****************************************************************************************/
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
using Cms.Model.Maintenance;
using Cms.CmsWeb;
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher;
using System.IO;
using System.Resources; 

namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for AdditionalWordings.
	/// </summary>
	public class AdditionalWordings : Cms.CmsWeb.cmsbase
	{
		System.Resources.ResourceManager objResourceMgr;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capFORM_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtFORM_NUMBER;
		protected System.Web.UI.WebControls.Label capPDF_WORDINGS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPDF_WORDINGS;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		//protected System.Web.UI.HtmlControls.HtmlGenericControl spnFileName;
		protected System.Web.UI.WebControls.TextBox txtPDF_WORDINGS;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		//private string strRowId="";
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidWORDINGS_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidROW_ID;
		protected System.Web.UI.WebControls.Label capSTATE;
		protected System.Web.UI.WebControls.Label capLOB;
		protected System.Web.UI.WebControls.Label capPOLICY_PROCESS;
		protected System.Web.UI.WebControls.DropDownList cmbPOLICY_PROCESS;
		protected System.Web.UI.WebControls.DropDownList cmbSTATE;
		protected System.Web.UI.WebControls.DropDownList cmbLOB;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSTATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOB;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPOLICY_PROCESS;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPDF_WORDINGS;
        protected System.Web.UI.WebControls.Label capMessages;
	
		#region Set Validators ErrorMessages
		private void SetErrorMessages()
		{
//			rfvPDF_WORDINGS.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
		}
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId="394_0";
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
			btnReset.CmsButtonClass	=	CmsButtonType.Execute;
			btnReset.PermissionString		=	gstrSecurityXML;
			btnReset.Attributes.Add("onclick","javascript:return ResetScreen();"); 


			btnSave.CmsButtonClass	=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;
			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AdditionalWordings" ,System.Reflection.Assembly.GetExecutingAssembly());

			// Put user code to initialize the page here
			if(!Page.IsPostBack)
			{
				hidROW_ID.Value = "NEW";
				FillDropdowns();
				GetQueryStrings();
				SetValidators();
                if (ClsCommon.IsXMLResourceExists(@Request.PhysicalApplicationPath + "CmsWeb/Support/PageXml/" + GetSystemId(), "AdditionalWordings.xml"))
                {
                    setPageControls(Page, @Request.PhysicalApplicationPath + "/CmsWeb/support/PageXML/" + GetSystemId() + "/AdditionalWordings.xml");
                }
				if(hidWORDINGS_ID.Value != "")
				{
					SetXml(int.Parse(hidWORDINGS_ID.Value));				
					cmbSTATE.Enabled=false;
					cmbLOB.Enabled=false;
					cmbPOLICY_PROCESS.Enabled=false;
                }
                
            }
			SetCaptions();
		}
		#region Set Captions
		private void SetCaptions()
		{
			capSTATE.Text=objResourceMgr.GetString("cmbSTATE");
			capLOB.Text=objResourceMgr.GetString("cmbLOB");
			capPOLICY_PROCESS.Text=objResourceMgr.GetString("cmbPOLICY_PROCESS");
			capPDF_WORDINGS.Text=objResourceMgr.GetString("txtPDF_WORDINGS");
		}
		#endregion 

		#region Set Validators ErrorMessages
		private void SetValidators()
		{
			rfvSTATE.ErrorMessage					=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"13");
			rfvLOB.ErrorMessage						=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"14");
			rfvPOLICY_PROCESS.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"15");
			rfvPDF_WORDINGS.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"16");
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		

		#region "Fill DropDowns"
		private void FillDropdowns()
		{
			DataTable dtState = Cms.CmsWeb.ClsFetcher.ActiveState ;
			cmbSTATE.DataSource			= dtState;
			cmbSTATE.DataTextField		= "STATE_NAME";
			cmbSTATE.DataValueField		= "STATE_ID";
			cmbSTATE.DataBind();
			cmbSTATE.Items.Insert(0,new ListItem("",""));
			cmbSTATE.SelectedIndex=0;

			DataTable dtLOBs = Cms.CmsWeb.ClsFetcher.LOBs;
			cmbLOB.DataSource			= dtLOBs;
			cmbLOB.DataTextField		= "LOB_DESC";
			cmbLOB.DataValueField		= "LOB_ID";
			cmbLOB.DataBind();
			cmbLOB.Items.Insert(0,"");

			DataTable dtPolicyProcess = Cms.CmsWeb.ClsFetcher.PolicyProcess;
			cmbPOLICY_PROCESS.DataSource			= dtPolicyProcess;
			cmbPOLICY_PROCESS.DataTextField			= "PROCESS_DESC";
			cmbPOLICY_PROCESS.DataValueField		= "PROCESS_ID";
			cmbPOLICY_PROCESS.DataBind();
			cmbPOLICY_PROCESS.Items.Insert(0,"");

		
		}
		#endregion
		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsAdditionalWordingsInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsAdditionalWordingsInfo objAdditionalWordingsInfo;
			objAdditionalWordingsInfo = new ClsAdditionalWordingsInfo();

			
			objAdditionalWordingsInfo.STATE_ID			= int.Parse(cmbSTATE.SelectedValue);
			objAdditionalWordingsInfo.LOB_ID			= int.Parse(cmbLOB.SelectedValue);
			objAdditionalWordingsInfo.PROCESS_ID		= int.Parse(cmbPOLICY_PROCESS.SelectedValue);    
			
			if (txtPDF_WORDINGS.Text.Trim() !="")
			{
				objAdditionalWordingsInfo.PDF_WORDINGS			= txtPDF_WORDINGS.Text;  
				hidPDF_WORDINGS.Value = objAdditionalWordingsInfo.PDF_WORDINGS;
			}
			objAdditionalWordingsInfo.IS_ACTIVE="Y";
			///

			//These  assignments are common to all pages.
		//	strRowId = hidIDEN_PLAN_ID.Value;
			
			//Returning the model object
			return objAdditionalWordingsInfo;
		}
		#endregion

		private void GetQueryStrings()
		{
			if(Request.QueryString["WORDINGS_ID"] != null)
			{
				hidWORDINGS_ID.Value = Request.QueryString["WORDINGS_ID"].ToString();
			}
		}

/*		public string MapURL(string strPath)
		{
			string AppPath = HttpContext.Current.Server.MapPath("~");
		    string url = String.Format("/cms{0}" , strPath.Replace(AppPath, "").Replace("\\", "/"));
			return url;
		 }
*/

		private void SetXml(int strWordingsId)
		{
			DataSet oldDs = ClsAdditionalWordings.GetXmlForPageControls(strWordingsId);
			int indexcombo = 0;

			if(oldDs != null && oldDs.Tables[0].Rows.Count > 0)
			{
				hidOldData.Value = ClsAdditionalWordings.GetXmlForPageControls(strWordingsId).GetXml();
				for(indexcombo = 0; indexcombo < cmbSTATE.Items.Count; indexcombo++)
				{
					if(cmbSTATE.Items[indexcombo].Value == oldDs.Tables[0].Rows[0]["STATE_ID"].ToString())
					{
						cmbSTATE.SelectedIndex = indexcombo;
					}
				}

				for(indexcombo = 0; indexcombo < cmbLOB.Items.Count; indexcombo++)
				{
					if(cmbLOB.Items[indexcombo].Value == oldDs.Tables[0].Rows[0]["LOB_ID"].ToString())
					{
						cmbLOB.SelectedIndex = indexcombo;
					}
				}

				for(indexcombo = 0; indexcombo < cmbPOLICY_PROCESS.Items.Count; indexcombo++)
				{
					if(cmbPOLICY_PROCESS.Items[indexcombo].Value == oldDs.Tables[0].Rows[0]["PROCESS_ID"].ToString())
					{
						cmbPOLICY_PROCESS.SelectedIndex = indexcombo;
					}
				}
				hidPDF_WORDINGS.Value = oldDs.Tables[0].Rows[0]["PDF_WORDINGS"].ToString();
				txtPDF_WORDINGS.Text = oldDs.Tables[0].Rows[0]["PDF_WORDINGS"].ToString();

				hidROW_ID.Value = "UPDATE";
			}

		}

		#region "Web Event Handlers"
		/// <summary>
		/// If form is posted back then add entry in database using the BL object
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			ClsAdditionalWordings objAdditionalWordings=null;
			try
			{
				int intRetVal;	//For retreiving the return value of business class save function
				objAdditionalWordings = new  ClsAdditionalWordings();
				string Custom="";
				Custom =cmbPOLICY_PROCESS.Items[cmbPOLICY_PROCESS.SelectedIndex].Text + "~";
				Custom +=cmbSTATE.Items[cmbSTATE.SelectedIndex].Text + "~";
				Custom +=cmbLOB.Items[cmbLOB.SelectedIndex].Text;

				//Retreiving the form values into model class object
				ClsAdditionalWordingsInfo objAdditionalWordingsInfo = GetFormValue();

				if(hidROW_ID.Value.ToUpper().Equals("NEW")) //save case
				{
					objAdditionalWordingsInfo.CREATED_BY = int.Parse(GetUserId());
					objAdditionalWordingsInfo.CREATED_DATETIME = DateTime.Now;

					//Calling the add method of business layer class
					intRetVal = objAdditionalWordings.Add(objAdditionalWordingsInfo);

					if(intRetVal>0)			// Insert successfully performed
					{
				//		hidIDEN_PLAN_ID.Value = objAdditionalWordingsInfo.IDEN_PLAN_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
				//		GetOldDataXML();
					}
					else if(intRetVal == -1)		// Duplicate code exist, Insert failed
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"910");
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
					//Creating the Model object for holding the Old data
					ClsAdditionalWordingsInfo objOldAdditionalWordingsInfo;
					objOldAdditionalWordingsInfo = new ClsAdditionalWordingsInfo();
					base.PopulateModelObject(objOldAdditionalWordingsInfo, hidOldData.Value);
					//Setting  the Old Page details(XML File containing old details) into the Model Object
					
					if((objAdditionalWordingsInfo.PDF_WORDINGS == null)||(objAdditionalWordingsInfo.PDF_WORDINGS == ""))
						objAdditionalWordingsInfo.PDF_WORDINGS = hidPDF_WORDINGS.Value;

					//Setting those values into the Model object which are not in the page
					objAdditionalWordingsInfo.WORDINGS_ID = int.Parse(hidWORDINGS_ID.Value);
					objAdditionalWordingsInfo.MODIFIED_BY = int.Parse(GetUserId());
					objAdditionalWordingsInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					objAdditionalWordingsInfo.IS_ACTIVE = "Y";

					//Updating the record using business layer class object
					intRetVal	= objAdditionalWordings.Update(objOldAdditionalWordingsInfo,objAdditionalWordingsInfo,Custom);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
					//	GetOldDataXML();
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"910");
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
				//ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objAdditionalWordings!= null)
					objAdditionalWordings.Dispose();
			}
		}
		#endregion


				
	}
}
