/******************************************************************************************
<Author					: -   Vijay Joshi
<Start Date				: -	12/26/2005 12:54:01 PM
<End Date				: -	
<Description			: - 	fdfd
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - To save data in enodrsement details page.
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
using Cms.Model.Policy.Process;
using Cms.BusinessLayer.BlProcess;
using Cms.CmsWeb;
using Cms.CmsWeb.Controls;

namespace Cms.Policies.Processes
{
	/// <summary>
	/// Summary description for EndorsementDetails.
	/// </summary>
	public class EndorsementDetails : Cms.Policies.policiesbase
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capENDORSEMENT_DATE;
		protected System.Web.UI.WebControls.Label capENDORSEMENT_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbENDORSEMENT_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvENDORSEMENT_TYPE;
		protected System.Web.UI.WebControls.Label capENDORSEMENT_DESC;
		protected System.Web.UI.WebControls.TextBox txtENDORSEMENT_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvENDORSEMENT_DESC;
		protected System.Web.UI.WebControls.Label capREMARKS;
		protected System.Web.UI.WebControls.TextBox txtREMARKS;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidENDORSEMENT_DETAIL_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidENDORSEMENT_NO;
		protected System.Web.UI.WebControls.Label lblENDORSEMENT_DATE;
		protected System.Web.UI.WebControls.Image imgENDORSEMENT_DATE;
        protected System.Web.UI.WebControls.Label capHeader;
        protected System.Web.UI.WebControls.Label capMessage;
	
		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			rfvENDORSEMENT_TYPE.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "598");
			rfvENDORSEMENT_DESC.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "599");
		}
		#endregion	

		#region GetQueryString
		/// <summary>
		/// Gets the query string values into hidden controls
		/// </summary>
		private void GetQueryString()
		{
			hidCUSTOMER_ID.Value		= Request.Params["CUSTOMER_ID"].ToString();
			hidPOLICY_ID.Value			= Request.Params["POLICY_ID"].ToString();
			hidPOLICY_VERSION_ID.Value	= Request.Params["POLICY_VERSION_ID"].ToString();
			hidENDORSEMENT_NO.Value		= Request.Params["ENDORSEMENT_NO"].ToString();
		}
		#endregion

		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsEndorsementDetailInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsEndorsementDetailInfo objEndorsementDetailInfo;
			objEndorsementDetailInfo = new ClsEndorsementDetailInfo();

			objEndorsementDetailInfo.CUSTOMER_ID		= int.Parse(hidCUSTOMER_ID.Value);
			objEndorsementDetailInfo.POLICY_ID			= int.Parse(hidPOLICY_ID.Value);
			objEndorsementDetailInfo.POLICY_VERSION_ID	= int.Parse(hidPOLICY_VERSION_ID.Value);
			objEndorsementDetailInfo.ENDORSEMENT_NO		= int.Parse(hidENDORSEMENT_NO.Value);

			DateTime dt = ConvertToDate(lblENDORSEMENT_DATE.Text);
			dt = new DateTime(dt.Year, dt.Month, dt.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
			objEndorsementDetailInfo.ENDORSEMENT_DATE = dt;
			
			
			if (cmbENDORSEMENT_TYPE.SelectedItem != null && cmbENDORSEMENT_TYPE.SelectedValue != "")
				objEndorsementDetailInfo.ENDORSEMENT_TYPE = int.Parse(cmbENDORSEMENT_TYPE.SelectedValue);

			objEndorsementDetailInfo.ENDORSEMENT_DESC	=	txtENDORSEMENT_DESC.Text;
			objEndorsementDetailInfo.REMARKS			=	txtREMARKS.Text;
			objEndorsementDetailInfo.TRANS_ID			=	Request.Params["TRANS_ID"];

			//Returning the model object
			return objEndorsementDetailInfo;
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

		#region Setcaption
		/// <summary>
		/// Sets the caption of labels from resource file
		/// </summary>
		private void SetCaptions()
		{
			
			System.Resources.ResourceManager objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Processes.EndorsementDetails" ,System.Reflection.Assembly.GetExecutingAssembly());
			capENDORSEMENT_DATE.Text	=	objResourceMgr.GetString("lblENDORSEMENT_DATE");
			capENDORSEMENT_TYPE.Text	=	objResourceMgr.GetString("cmbENDORSEMENT_TYPE");
			capENDORSEMENT_DESC.Text	=	objResourceMgr.GetString("txtENDORSEMENT_DESC");
			capREMARKS.Text				=	objResourceMgr.GetString("txtREMARKS");
		}
		#endregion

		#region Save
		/// <summary>
		/// Saves the details using ClsEndorsmentProcess class
		/// </summary>
		private void SaveEndorsementDetails()
		{
			Cms.BusinessLayer.BlProcess.ClsEndorsmentProcess objEndorsementDetails = null;
			try
			{
				int intRetVal;	//For retreiving the return value of business class save function
				objEndorsementDetails = new  ClsEndorsmentProcess();

				//Retreiving the form values into model class object
				ClsEndorsementDetailInfo objEndorsementDetailInfo = GetFormValue();

				if(hidENDORSEMENT_DETAIL_ID.Value.ToUpper().Equals("NEW")) //save case
				{
					objEndorsementDetailInfo.CREATED_BY = int.Parse(GetUserId());
					objEndorsementDetailInfo.CREATED_DATETIME = DateTime.Now;

					//Calling the add method of business layer class
					intRetVal = objEndorsementDetails.AddEndorsementLogDetails(objEndorsementDetailInfo);

					if(intRetVal>0)
					{
						hidENDORSEMENT_DETAIL_ID.Value	=	objEndorsementDetailInfo.ENDORSEMENT_DETAIL_ID.ToString();
						lblMessage.Text					=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value				=	"1";
						hidIS_ACTIVE.Value				=	"Y";

					}
					else if(intRetVal == -1)
					{
						lblMessage.Text					=	ClsMessages.GetMessage(base.ScreenId,"18");
						hidFormSaved.Value				=	"2";
					}
					else
					{
						lblMessage.Text					=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value				=	"2";
					}
				} // end save case
				else
				{
					//Calling the add method of business layer class
					objEndorsementDetailInfo.ENDORSEMENT_DETAIL_ID = int.Parse(hidENDORSEMENT_DETAIL_ID.Value);
					intRetVal = objEndorsementDetails.UpdateEndorsementLogDetails(objEndorsementDetailInfo);

					if(intRetVal>0)
					{
						hidENDORSEMENT_DETAIL_ID.Value	=	objEndorsementDetailInfo.ENDORSEMENT_DETAIL_ID.ToString();
						lblMessage.Text					=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value				=	"1";
						hidIS_ACTIVE.Value				=	"Y";
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text					=	ClsMessages.GetMessage(base.ScreenId,"18");
						hidFormSaved.Value				=	"2";
					}
					else
					{
						lblMessage.Text					=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value				=	"2";
					}

				}
				Session["InValidateSession"] = "Y";
			}
			catch(Exception ex)
			{
				lblMessage.Text		=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				hidFormSaved.Value	=	"2";
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish (ex);
			}
			finally
			{
				if(objEndorsementDetails!= null)
					objEndorsementDetails.Dispose();

				lblMessage.Visible = true;
			}
		}
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId = "5000_25";
			btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
			//imgENDORSEMENT_DATE.Attributes.Add("onclick","fPopCalendar(document.POL_POLICY_ENDORSEMENTS_DETAILS.txtENDORSEMENT_DATE, document.POL_POLICY_ENDORSEMENTS_DETAILS.txtENDORSEMENT_DATE)");
			SetErrorMessages();

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass		=	CmsButtonType.Execute;
			btnReset.PermissionString	=	gstrSecurityXML;

			
			btnSave.CmsButtonClass		=	CmsButtonType.Execute;
			btnSave.PermissionString	=	gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			
			if(!Page.IsPostBack)
			{

				//Getting the query string data
				GetQueryString();
				lblENDORSEMENT_DATE.Text = DateTime.Now.ToString("MM/dd/yyyy");
				PopulateCombobox();

				//If default endrsement type is passed then selecting that
				if (Request.Params["ENDORSEMENT_TYPE"] != null)
				{
					cmbENDORSEMENT_TYPE.SelectedValue = Request.Params["ENDORSEMENT_TYPE"].ToString();
				}

				//Setting the caption of label
				SetCaptions();
                capHeader.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1247");
                capMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");

				//Saving the details of endorsement
				SaveEndorsementDetails();
				hidFormSaved.Value = "0";
				lblMessage.Text = "";
			}

		}

		private void PopulateCombobox()
		{
			cmbENDORSEMENT_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("PPAEND");
			cmbENDORSEMENT_TYPE.DataValueField = "LookupID";
			cmbENDORSEMENT_TYPE.DataTextField = "LookupDesc";
			cmbENDORSEMENT_TYPE.DataBind();
			cmbENDORSEMENT_TYPE.Items.Insert(0,"");
			cmbENDORSEMENT_TYPE.SelectedIndex=cmbENDORSEMENT_TYPE.Items.IndexOf(cmbENDORSEMENT_TYPE.Items.FindByValue("11619")); //default to Other


		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				SaveEndorsementDetails();

				//Closing the window
				ClientScript.RegisterStartupScript(this.GetType(),"CLOSEWINDOW","<script>window.close();</script>");
			}
			catch(Exception objExp)
			{
				lblMessage.Text = objExp.Message.ToString();
			}
		}

		
	}
}
