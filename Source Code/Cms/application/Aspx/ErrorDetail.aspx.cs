using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using Cms.CmsWeb;
using Cms.CmsWeb.Controls;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Cms.BusinessLayer.BlApplication;


namespace Cms.Application.Aspx
{
	/// <summary>
	/// Summary description for ErrorDetail.
	/// </summary>
	public class ErrorDetail :Cms.Application.appbase	
	{
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capEXCEPTION_ID;
		protected System.Web.UI.WebControls.Label lblEXCEPTION_ID;
		protected System.Web.UI.WebControls.Label capEXCEPTION_DATE;
		protected System.Web.UI.WebControls.Label lblEXCEPTION_DATE;
		protected System.Web.UI.WebControls.Label capERRORDETAIL;
		protected System.Web.UI.WebControls.Label lblERRORDETAIL;
		protected System.Web.UI.WebControls.Label capEXCEPTION_TYPE;
		protected System.Web.UI.WebControls.Label lblEXCEPTION_TYPE;
		protected System.Web.UI.WebControls.Label capCUSTOMER_ID;
		protected System.Web.UI.WebControls.Label lblCUSTOMER_ID;
		protected System.Web.UI.WebControls.Label capAPP_ID;
		protected System.Web.UI.WebControls.Label lblAPP_ID;
		protected System.Web.UI.WebControls.Label capAPP_VER_ID;
		protected System.Web.UI.WebControls.Label lblAPP_VER_ID;
		protected System.Web.UI.WebControls.Label capPOL_ID;
		protected System.Web.UI.WebControls.Label lblPOL_ID;
		protected System.Web.UI.WebControls.Label capPOL_VER_ID;
		protected System.Web.UI.WebControls.Label lblPOL_VER_ID;
		protected System.Web.UI.WebControls.Label capCLAIM_ID;
		protected System.Web.UI.WebControls.Label lblCLAIM_ID;
		protected System.Web.UI.WebControls.Label capSOURCE;
		protected System.Web.UI.WebControls.Label lblSOURCE;
		protected System.Web.UI.WebControls.Label capDETAIL;
		protected System.Web.UI.WebControls.Label lblDETAIL;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPDF_WORDINGS;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidEXCEPTIONID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidROW_ID;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidErrMsg;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tblErrorDetail;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			base.ScreenId="217_0";
			btnDelete.CmsButtonClass	=	CmsButtonType.Delete;
			btnDelete.PermissionString		=	gstrSecurityXML;
			btnDelete.Attributes.Add("onclick","javascript:ShowHideTable()");
			GetQueryStrings();
			FillExceptionDetails();
		}

		#region "Fill Levels"

		private void FillExceptionDetails()
		{
			
			DataSet dsException = new DataSet();
			Cms.BusinessLayer.BlApplication.clsapplication 	objException = new Cms.BusinessLayer.BlApplication.clsapplication();			
			dsException= objException.FillExceptionDetails(Convert.ToInt32(hidEXCEPTIONID.Value));

			if(dsException.Tables[0].Rows.Count>0)
			{
				lblEXCEPTION_ID.Text	= dsException.Tables[0].Rows[0]["EXCEPTIONID"].ToString();
				lblEXCEPTION_DATE.Text	= dsException.Tables[0].Rows[0]["EXCEPTIONDATE"].ToString();
				lblERRORDETAIL.Text		= dsException.Tables[0].Rows[0]["MESSAGE"].ToString();
				lblCUSTOMER_ID.Text		= dsException.Tables[0].Rows[0]["CUSTOMER_ID"].ToString();
				lblAPP_ID.Text			= dsException.Tables[0].Rows[0]["APP_ID"].ToString();
				lblAPP_VER_ID.Text		= dsException.Tables[0].Rows[0]["APP_VERSION_ID"].ToString();
				lblPOL_ID.Text			= dsException.Tables[0].Rows[0]["POLICY_ID"].ToString();
				lblPOL_VER_ID.Text		= dsException.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString();
				lblCLAIM_ID.Text		= dsException.Tables[0].Rows[0]["CLAIM_ID"].ToString();
				lblSOURCE.Text			= dsException.Tables[0].Rows[0]["SOURCE"].ToString();
				lblDETAIL.Text			= dsException.Tables[0].Rows[0]["EXCEPTIONDESC"].ToString();
				lblEXCEPTION_TYPE.Text	= dsException.Tables[0].Rows[0]["EXCEPTION_TYPE"].ToString();

			}
		}	

		#endregion

		#region "Get Querystring Values"
		private void GetQueryStrings()
		{
			if(Request.QueryString["EXCEPTIONID"] != null)
			{
				hidEXCEPTIONID.Value = Request.QueryString["EXCEPTIONID"].ToString();
			}
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			Delete();
		}


		/// <summary>

		/// Delets an exception log entry from the database

		/// </summary>
		/// 
		private void Delete()
		{

			Cms.BusinessLayer.BlCommon.ClsDiary objDiary = new Cms.BusinessLayer.BlCommon.ClsDiary();
			string ExceptionID="(EXCEPTIONID=" + hidEXCEPTIONID.Value + ")"; 
			//int listID = 0;//Convert.ToInt32(this.hidKeyValues.Value);
			int result=objDiary.DeleteDiaryEntry(ExceptionID,"exceptionlog");
			lblMessage.Visible=true;
			lblMessage.Text="<br>Records have been successfully deleted";
			if(result >0)
				this.hidErrMsg.Value= "1";	
			tblErrorDetail.Visible=false;
		}

	}
}
