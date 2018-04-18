/* ***************************************************************************************
   Author		: Deepak Batra 
   Creation Date: 05/01/2006
   Last Updated : 
   Reviewed By	: 
   Purpose		: This file is used for Excess Layer for a reinsurance contract. 
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
using Cms.Model.Maintenance.Reinsurance;
using Cms.ExceptionPublisher.ExceptionManagement;
using System.Xml;
using Cms;
using Cms.CmsWeb.Controls;

namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for AddReinsuranceContractName.
	/// </summary>
	public class AddReinsuranceContractName : Cms.CmsWeb.cmsbase
	{
		# region D E C L A R A T I O N
		
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capCONTRACT_NAME;
		protected System.Web.UI.WebControls.Label capCONTRACT_DESCRIPTION;

		protected System.Web.UI.WebControls.TextBox txtCONTRACT_NAME;
		protected System.Web.UI.WebControls.TextBox txtCONTRACT_DESCRIPTION;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCONTRACT_NAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCONTRACT_DESCRIPTION;

		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hid_CONTRACT_NAME_ID;

		ClsReinsuranceContractName objReinsuranceContractName;
		System.Resources.ResourceManager objResourceMgr;

		private string strRowId, strFormSaved;
		protected string oldXML;
        
		# endregion 

		# region P A G E   L O A D 

		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				objReinsuranceContractName = new ClsReinsuranceContractName();
				btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");

				base.ScreenId = "265";
				lblMessage.Visible = false;
				SetErrorMessages();

				//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
				btnReset.CmsButtonClass		=	CmsButtonType.Execute;
				btnReset.PermissionString	=	gstrSecurityXML;

				btnSave.CmsButtonClass		=	CmsButtonType.Execute;
				btnSave.PermissionString	=	gstrSecurityXML;

				objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddReinsuranceContractName" ,System.Reflection.Assembly.GetExecutingAssembly());

				if(!Page.IsPostBack)
				{
					if(Request.QueryString["CONTRACT_NAME_ID"]!=null && Request.QueryString["CONTRACT_NAME_ID"].ToString().Length>0)
						hid_CONTRACT_NAME_ID.Value = Request.QueryString["CONTRACT_NAME_ID"].ToString();

					SetCaptions();

					GetDataForEditMode();
				}
			}
			catch(Exception oEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);			
			}
		}

		# endregion 

		# region  S E T   P A G E   V A L I D A T I O N S   E R R O R   M E S S A G E S 

		private void SetErrorMessages()
		{
			try
			{
				rfvCONTRACT_NAME.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
				rfvCONTRACT_DESCRIPTION.ErrorMessage	= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			}
			catch(Exception oEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);
			}
		}

		# endregion

		# region  S E T   L A B E L   C A P T I O N S 

		private void SetCaptions()
		{
			try
			{
				capCONTRACT_NAME.Text			= objResourceMgr.GetString("txtCONTRACT_NAME");
				capCONTRACT_DESCRIPTION.Text	= objResourceMgr.GetString("txtCONTRACT_DESCRIPTION");
			}
			catch(Exception oEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);
			}
		}

		# endregion

		#region G E T   F O R M   V A L U E S 
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsReinsuranceContractNameInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsReinsuranceContractNameInfo objReinsuranceContractNameInfo = new ClsReinsuranceContractNameInfo();

			if(txtCONTRACT_NAME.Text != "")
				objReinsuranceContractNameInfo.CONTRACT_NAME			= txtCONTRACT_NAME.Text;
			if(txtCONTRACT_DESCRIPTION.Text != "")
				objReinsuranceContractNameInfo.CONTRACT_DESCRITION		= txtCONTRACT_DESCRIPTION.Text;
			
			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hid_CONTRACT_NAME_ID.Value;
			//oldXML			=	hidOldData.Value;
			//Returning the model object
			
			return objReinsuranceContractNameInfo;
		}

		#endregion

		#region W E B  F O R M   D E S I G N E R   G E N E R A T E D   C O D E
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
			this.Load			+= new System.EventHandler(this.Page_Load);
			this.btnSave.Click	+= new System.EventHandler(this.btnSave_Click);
		}
		#endregion

		# region S A V E   F O R M   D A T A 

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	//For retreiving the return value of business class save function
				objReinsuranceContractName = new ClsReinsuranceContractName();

				ClsReinsuranceContractNameInfo objReinsuranceContractNameInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //Add Mode
				{
					
					intRetVal = objReinsuranceContractName.Add(objReinsuranceContractNameInfo);

					if(intRetVal>0)
					{
						hid_CONTRACT_NAME_ID.Value		=	objReinsuranceContractNameInfo.CONTRACT_NAME_ID.ToString();
						lblMessage.Text					=	ClsMessages.GetMessage(base.ScreenId,"4");
						hidFormSaved.Value				=	"1";
						hidOldData.Value				=   objReinsuranceContractName.GetDataForPageControls(hid_CONTRACT_NAME_ID.Value).GetXml();
						hidIS_ACTIVE.Value				=	"Y";						
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"3");
						hidFormSaved.Value		=	"2";
					}
					else
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"5");
						hidFormSaved.Value		=	"2";
					}
					lblMessage.Visible = true;
				}
				else // Edit Mode
				{
					//Creating the Model object for holding the Old data
					ClsReinsuranceContractNameInfo objOldReinsuranceContractNameInfo = new ClsReinsuranceContractNameInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldReinsuranceContractNameInfo, hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objReinsuranceContractNameInfo.CONTRACT_NAME_ID		= strRowId ;//int.Parse(strRowId);
					
					intRetVal	= objReinsuranceContractName.Update(objOldReinsuranceContractNameInfo,objReinsuranceContractNameInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"6");
						hidFormSaved.Value		=	"1";
						hidOldData.Value		=	objReinsuranceContractName.GetDataForPageControls(strRowId).GetXml();
					}
					else if(intRetVal == -1)	
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"5");
						hidFormSaved.Value		=	"1";
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"5");
						hidFormSaved.Value		=	"1";
					}
					lblMessage.Visible = true;
				}
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"3") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
				ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objReinsuranceContractName!= null)
					objReinsuranceContractName.Dispose();
			}
		}

		# endregion 

		# region  G E T   D A T A   F O R   E D I T   M O D E 

		private void GetDataForEditMode()
		{
			try
			{
				objReinsuranceContractName = new ClsReinsuranceContractName(); 
				DataSet oDs = objReinsuranceContractName.GetDataForPageControls(hid_CONTRACT_NAME_ID.Value);
				if(oDs.Tables[0].Rows.Count >0)
				{
					DataRow oDr  = oDs.Tables[0].Rows[0];
					
					if(oDr["CONTRACT_NAME"].ToString() != "")
						txtCONTRACT_NAME.Text			= oDr["CONTRACT_NAME"].ToString();

					if(oDr["CONTRACT_DESCRIPTION"].ToString() != "")
						txtCONTRACT_DESCRIPTION.Text	= oDr["CONTRACT_DESCRIPTION"].ToString();
				}
			}
			catch(Exception oEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);
			}
			finally{}
		}

		# endregion 
	}
}
