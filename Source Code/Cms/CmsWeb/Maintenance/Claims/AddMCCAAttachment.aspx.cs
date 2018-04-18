/******************************************************************************************
	<Author					: - > Vijay Arora
	<Start Date				: -	> 10-08-2006
	<End Date				: - >
	<Description			: - > This page is used for Claims MCCA Attachment Points.
	<Review Date			: - >
	<Reviewed By			: - >
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
using Cms.Model.Maintenance.Claims;
using Cms.CmsWeb.Controls; 
using Cms.ExceptionPublisher; 
using Cms.BusinessLayer.BLClaims;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.Model.Claims;

namespace Cms.CmsWeb.Maintenance.Claims
{
	/// <summary>
	/// 
	/// </summary>
	public class AddMCCAAttachment : Cms.CmsWeb.cmsbase
	{
		#region Local form variables
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId;		
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capPOLICY_PERIOD_DATE_FROM;
		protected System.Web.UI.WebControls.TextBox txtPOLICY_PERIOD_DATE_FROM;
		protected System.Web.UI.WebControls.HyperLink hlkPOLICY_PERIOD_DATE_FROM;
		protected System.Web.UI.WebControls.RegularExpressionValidator revPOLICY_PERIOD_DATE_FROM;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPOLICY_PERIOD_DATE_FROM;
		protected System.Web.UI.WebControls.Label capPOLICY_PERIOD_DATE_TO;
		protected System.Web.UI.WebControls.TextBox txtPOLICY_PERIOD_DATE_TO;
		protected System.Web.UI.WebControls.HyperLink hlkPOLICY_PERIOD_DATE_TO;
		protected System.Web.UI.WebControls.RegularExpressionValidator revPOLICY_PERIOD_DATE_TO;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPOLICY_PERIOD_DATE_TO;
		protected System.Web.UI.WebControls.CustomValidator csvPOLICY_PERIOD_DATE_TO;
		protected System.Web.UI.WebControls.Label capLOSS_PERIOD_DATE_FROM;
		protected System.Web.UI.WebControls.TextBox txtLOSS_PERIOD_DATE_FROM;
		protected System.Web.UI.WebControls.HyperLink hlkLOSS_PERIOD_DATE_FROM;
		protected System.Web.UI.WebControls.RegularExpressionValidator revLOSS_PERIOD_DATE_FROM;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOSS_PERIOD_DATE_FROM;
		protected System.Web.UI.WebControls.Label capLOSS_PERIOD_DATE_TO;
		protected System.Web.UI.WebControls.TextBox txtLOSS_PERIOD_DATE_TO;
		protected System.Web.UI.WebControls.HyperLink hlkLOSS_PERIOD_DATE_TO;
		protected System.Web.UI.WebControls.RegularExpressionValidator revLOSS_PERIOD_DATE_TO;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOSS_PERIOD_DATE_TO;
		protected System.Web.UI.WebControls.CustomValidator csvLOSS_PERIOD_DATE_TO;
		protected System.Web.UI.WebControls.Label capMCCA_ATTACHMENT_POINT;
		protected System.Web.UI.WebControls.TextBox txtMCCA_ATTACHMENT_POINT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMCCA_ATTACHMENT_POINT;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlTableRow trbody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidMCCA_ATTACHMENT_ID;
		protected System.Web.UI.WebControls.RangeValidator rngMCCA_ATTACHMENT_POINT;
        protected System.Web.UI.WebControls.Label capMessages;
		ClsMCCAAttachment objMCCA = new ClsMCCAAttachment();
		#endregion
	
		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{						
			
			base.ScreenId="346_0";
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
			lblMessage.Visible = false;
			
			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass		=	CmsButtonType.Write;
			btnReset.PermissionString	=	gstrSecurityXML;
			
			btnSave.CmsButtonClass		=	CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass		=	CmsButtonType.Write;
			btnActivateDeactivate.PermissionString	=	gstrSecurityXML;
			
			
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.Claims.AddMCCAAttachment" ,System.Reflection.Assembly.GetExecutingAssembly());

			if(!Page.IsPostBack)
			{			
				hlkLOSS_PERIOD_DATE_FROM.Attributes.Add("OnClick","fPopCalendar(document.CLM_MCCA_ATTACHMENT.txtLOSS_PERIOD_DATE_FROM,document.CLM_MCCA_ATTACHMENT.txtLOSS_PERIOD_DATE_FROM)"); //Javascript Implementation for  Date 
				hlkLOSS_PERIOD_DATE_TO.Attributes.Add("OnClick","fPopCalendar(document.CLM_MCCA_ATTACHMENT.txtLOSS_PERIOD_DATE_TO,document.CLM_MCCA_ATTACHMENT.txtLOSS_PERIOD_DATE_TO)"); //Javascript Implementation for Date
				hlkPOLICY_PERIOD_DATE_FROM.Attributes.Add("OnClick","fPopCalendar(document.CLM_MCCA_ATTACHMENT.txtPOLICY_PERIOD_DATE_FROM,document.CLM_MCCA_ATTACHMENT.txtPOLICY_PERIOD_DATE_FROM)"); //Javascript Implementation for Date
				hlkPOLICY_PERIOD_DATE_TO.Attributes.Add("OnClick","fPopCalendar(document.CLM_MCCA_ATTACHMENT.txtPOLICY_PERIOD_DATE_TO,document.CLM_MCCA_ATTACHMENT.txtPOLICY_PERIOD_DATE_TO)"); //Javascript Implementation for Date 
				
				btnReset.Attributes.Add("onclick","javascript:return formReset();");

				if (Request["MCCA_ATTACHMENT_ID"] != null)
					hidMCCA_ATTACHMENT_ID.Value = Request["MCCA_ATTACHMENT_ID"].ToString();
				else
					hidMCCA_ATTACHMENT_ID.Value = "";

				SetCaptions();
				SetErrorMessages();	

				if(Request["MCCA_ATTACHMENT_ID"]!=null)
				{
					LoadData();
					GetOldDataXML();
				}

			}			
			
		}
		#endregion

		#region GetOldDataXML
		private void GetOldDataXML()
		{
			if (hidMCCA_ATTACHMENT_ID.Value != null)
				hidOldData.Value	=	objMCCA.GetXmlForPageControls(int.Parse(hidMCCA_ATTACHMENT_ID.Value));
			else
				hidOldData.Value	=	"";
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion		

		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			rfvPOLICY_PERIOD_DATE_FROM.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("785");
			revPOLICY_PERIOD_DATE_FROM.ValidationExpression	= aRegExpDate; 
			revPOLICY_PERIOD_DATE_FROM .ErrorMessage		=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");

			rfvPOLICY_PERIOD_DATE_TO.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("785");
			revPOLICY_PERIOD_DATE_TO.ValidationExpression	= aRegExpDate; 
			revPOLICY_PERIOD_DATE_TO.ErrorMessage		=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");

			rfvLOSS_PERIOD_DATE_FROM.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("785");
			revLOSS_PERIOD_DATE_FROM.ValidationExpression	= aRegExpDate; 
			revLOSS_PERIOD_DATE_FROM.ErrorMessage		=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");

			rfvLOSS_PERIOD_DATE_TO.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("785");
			revLOSS_PERIOD_DATE_TO.ValidationExpression	= aRegExpDate;
            revLOSS_PERIOD_DATE_TO.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			revLOSS_PERIOD_DATE_FROM.ErrorMessage		=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");

			csvPOLICY_PERIOD_DATE_TO.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("834");
			csvLOSS_PERIOD_DATE_TO.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("834");

			rfvMCCA_ATTACHMENT_POINT.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("835");
			rngMCCA_ATTACHMENT_POINT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("467");
		}
		#endregion

		#region SetCaptions
		private void SetCaptions()
		{
			capPOLICY_PERIOD_DATE_FROM.Text	=	objResourceMgr.GetString("txtPOLICY_PERIOD_DATE_FROM");
			capPOLICY_PERIOD_DATE_TO.Text	=	objResourceMgr.GetString("txtPOLICY_PERIOD_DATE_TO");
			capLOSS_PERIOD_DATE_FROM.Text	=	objResourceMgr.GetString("txtLOSS_PERIOD_DATE_FROM");
			capLOSS_PERIOD_DATE_TO.Text		=	objResourceMgr.GetString("txtLOSS_PERIOD_DATE_TO");
			capMCCA_ATTACHMENT_POINT.Text	=	objResourceMgr.GetString("txtMCCA_ATTACHMENT_POINT");
            btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1333");
		}
		#endregion

		#region GetFormValue
		private ClsMCCAAttachmentInfo  GetFormValue()
		{
			ClsMCCAAttachmentInfo objMCCAInfo = new ClsMCCAAttachmentInfo();
			
			objMCCAInfo.POLICY_PERIOD_DATE_FROM = Convert.ToDateTime(txtPOLICY_PERIOD_DATE_FROM.Text);
			objMCCAInfo.POLICY_PERIOD_DATE_TO = Convert.ToDateTime(txtPOLICY_PERIOD_DATE_TO.Text);
			objMCCAInfo.LOSS_PERIOD_DATE_FROM = Convert.ToDateTime(txtLOSS_PERIOD_DATE_FROM.Text);
			objMCCAInfo.LOSS_PERIOD_DATE_TO = Convert.ToDateTime(txtLOSS_PERIOD_DATE_TO.Text);
			string strAttachPoint = txtMCCA_ATTACHMENT_POINT.Text.Replace(",","");
			objMCCAInfo.MCCA_ATTACHMENT_POINT = Convert.ToInt32(strAttachPoint); 

			if(hidMCCA_ATTACHMENT_ID.Value.ToUpper()=="NEW" || hidMCCA_ATTACHMENT_ID.Value=="0" || hidMCCA_ATTACHMENT_ID.Value=="")
				strRowId="NEW";
			else
			{
				strRowId=hidMCCA_ATTACHMENT_ID.Value;
				objMCCAInfo.MCCA_ATTACHMENT_ID 	=	int.Parse(hidMCCA_ATTACHMENT_ID.Value);
			}
			
			return objMCCAInfo; 
		}
		#endregion

		#region LoadData
		private void LoadData()
		{
			if (hidMCCA_ATTACHMENT_ID.Value != null )
			{
				DataSet dsTemp =  objMCCA.GetValuesOfMCCAAttachment(int.Parse(hidMCCA_ATTACHMENT_ID.Value));
				if (dsTemp != null && dsTemp.Tables[0].Rows.Count > 0)
				{
					DataRow dr = dsTemp.Tables[0].Rows[0];
					txtPOLICY_PERIOD_DATE_FROM.Text = ConvertToDateCulture(Convert.ToDateTime(dr["POLICY_PERIOD_DATE_FROM"].ToString())); 
					txtPOLICY_PERIOD_DATE_TO.Text = ConvertToDateCulture(Convert.ToDateTime(dr["POLICY_PERIOD_DATE_TO"].ToString())); 
					txtLOSS_PERIOD_DATE_FROM.Text = ConvertToDateCulture(Convert.ToDateTime(dr["LOSS_PERIOD_DATE_FROM"].ToString())); 
					txtLOSS_PERIOD_DATE_TO.Text = ConvertToDateCulture(Convert.ToDateTime(dr["LOSS_PERIOD_DATE_TO"].ToString())); 
					txtMCCA_ATTACHMENT_POINT.Text = dr["MCCA_ATTACHMENT_POINT"].ToString(); 
					hidIS_ACTIVE.Value = dr["IS_ACTIVE"].ToString(); 
					txtPOLICY_PERIOD_DATE_FROM.Enabled = false;
					txtPOLICY_PERIOD_DATE_TO.Enabled = false;
					txtLOSS_PERIOD_DATE_FROM.Enabled = false;
					txtLOSS_PERIOD_DATE_TO.Enabled = false;
				}
			}
			else
			{
				txtPOLICY_PERIOD_DATE_FROM.Enabled = true;
				txtPOLICY_PERIOD_DATE_TO.Enabled = true;
				txtLOSS_PERIOD_DATE_FROM.Enabled = true;
				txtLOSS_PERIOD_DATE_TO.Enabled = true;
			}			
		}
		#endregion

		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;					
				
				if(hidIS_ACTIVE.Value.ToUpper()=="Y")
				{
					
					//Calling the add method of business layer class
					intRetVal = objMCCA.ActivateDeactivateMCCAAttachment(int.Parse(hidMCCA_ATTACHMENT_ID.Value),"N");

					if(intRetVal>0)
					{
						hidIS_ACTIVE.Value = "N";
						lblMessage.Text				=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"41");
						hidFormSaved.Value			=	"1";						
						LoadData();
						GetOldDataXML();
					}				
					else
					{
						lblMessage.Text				=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value			=	"2";
					}					
				}
				else 
				{
					
					intRetVal = objMCCA.ActivateDeactivateMCCAAttachment(int.Parse(hidMCCA_ATTACHMENT_ID.Value),"Y");
					if( intRetVal > 0 )
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"40");
						hidFormSaved.Value		=	"1";
						hidIS_ACTIVE.Value = "Y";
						LoadData();
						GetOldDataXML();
					}					
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"1");
						hidFormSaved.Value		=	"1";
					}					
				}
				lblMessage.Visible = true;				
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
			    
			}		

		}
		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	

				//Retreiving the form values into model class object
				ClsMCCAAttachmentInfo objMCCAInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objMCCAInfo.CREATED_BY = int.Parse(GetUserId());
					objMCCAInfo.CREATED_DATETIME = DateTime.Now;
					
					//Calling the add method of business layer class
					intRetVal = objMCCA.Add(objMCCAInfo);

					if(intRetVal>0)
					{
						hidMCCA_ATTACHMENT_ID.Value = objMCCAInfo.MCCA_ATTACHMENT_ID.ToString();
						lblMessage.Text				=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidIS_ACTIVE.Value			= "Y";
						LoadData();
						GetOldDataXML();
					}
					else if(intRetVal == -1) 
					{
						lblMessage.Text				=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("836");	
						hidFormSaved.Value			=		"2";
					}
					else
					{
						lblMessage.Text				=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value			=	"2";
					}					
				} // end save case
				else //UPDATE CASE
				{
					//Creating the Model object for holding the Old data
					ClsMCCAAttachmentInfo objOldMCCAInfo = new ClsMCCAAttachmentInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldMCCAInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page					
					objMCCAInfo.MODIFIED_BY = int.Parse(GetUserId());
					objMCCAInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					
					//Updating the record using business layer class object
					intRetVal	= objMCCA.Update(objOldMCCAInfo,objMCCAInfo);					
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						LoadData();
					}
					else if(intRetVal == -1)	
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("836");	
						hidFormSaved.Value		=	"2";
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value		=	"1";
					}					
				}
				lblMessage.Visible = true;
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
			    
			}
		}

	}
}
