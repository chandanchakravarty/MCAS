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
using Cms.DataLayer;  

namespace Reports.Aspx
{
	/// <summary>
	/// Summary description for NonPayCancellation.
	/// </summary>
	public class NonPayCancellation : Cms.CmsWeb.cmsbase   //System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblFromTransactionDate;
		protected System.Web.UI.WebControls.TextBox txtFromTransactionDate;
		protected System.Web.UI.WebControls.HyperLink hlkFromTransactionDate;
		protected System.Web.UI.WebControls.RegularExpressionValidator revFromTransactionDate;
		protected System.Web.UI.WebControls.CompareValidator cmpFromTransactionDate;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvFromDate;  
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvToDate;
		
		protected System.Web.UI.WebControls.Label lblToTransactionDate;
		protected System.Web.UI.WebControls.TextBox txtToTransactionDate;
		protected System.Web.UI.WebControls.HyperLink hlkToTransactionDate;
		protected System.Web.UI.WebControls.RegularExpressionValidator revToTransactionDate;
		protected System.Web.UI.WebControls.ListBox lstOrderBy;
		protected System.Web.UI.WebControls.Button btnSel;
		protected System.Web.UI.WebControls.Button btnDeSel;
		protected Cms.CmsWeb.Controls.CmsButton btnNonpay;
		protected System.Web.UI.WebControls.ListBox txtAGENCY_NAME;
		public string  strSystemID="";
		public string strAgencyID="";


		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			base.ScreenId = "442_0";
			
			btnNonpay.CmsButtonClass                 =   Cms.CmsWeb.Controls.CmsButtonType.Execute; 
			btnNonpay.PermissionString				=	gstrSecurityXML;	


			strSystemID = GetSystemId();
            string strCarrierSystemID = CarrierSystemID;//System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID").ToString();
			Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.Text , DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

			if ( strSystemID.Trim().ToUpper() != strCarrierSystemID.Trim().ToUpper())		
			{
				//Sorted on the basis if agency_name.
				string selStmt = "select (isnull(AGENCY_DISPLAY_NAME,'')) + '-'+RTRIM(ISNULL (AGENCY_CODE,'')) + '-' + cast(isnull(NUM_AGENCY_CODE,'') as varchar) as Name ,AGENCY_ID as ID from MNT_AGENCY_LIST where AGENCY_CODE = '" + strSystemID + "' order by AGENCY_DISPLAY_NAME,AGENCY_CODE";
				DataSet ds2 = objDataWrapper.ExecuteDataSet(selStmt);
				txtAGENCY_NAME.DataSource = ds2.Tables[0];
				txtAGENCY_NAME.DataTextField = "Name";
				txtAGENCY_NAME.DataValueField = "ID";
				txtAGENCY_NAME.DataBind();
				this.txtAGENCY_NAME.SelectedIndex =0;
			}
			else
			{				
				//Sorted On the basis of agency_name 
				DataSet ds2  = objDataWrapper.ExecuteDataSet("select (isnull(AGENCY_DISPLAY_NAME,'')) + '-' + RTRIM(ISNULL (AGENCY_CODE,'')) + '-' + cast(isnull(NUM_AGENCY_CODE,'') as varchar) as Name ,AGENCY_ID as ID from MNT_AGENCY_LIST order by AGENCY_DISPLAY_NAME,AGENCY_CODE"); 
				txtAGENCY_NAME.DataSource = ds2.Tables[0];
				txtAGENCY_NAME.DataTextField = "Name";
				txtAGENCY_NAME.DataValueField = "ID";
				txtAGENCY_NAME.DataBind();
				this.txtAGENCY_NAME.Items.Insert(0,"All");
				this.txtAGENCY_NAME.SelectedIndex =0;
			}
			
			hlkFromTransactionDate.Attributes.Add("OnClick","fPopCalendar(document.forms[0].txtFromTransactionDate,document.forms[0].txtFromTransactionDate)");			
			hlkToTransactionDate.Attributes.Add("OnClick","fPopCalendar(document.forms[0].txtToTransactionDate,document.forms[0].txtToTransactionDate)");			

			
			btnSel.Attributes.Add("onClick","funcSetOrderBy();return false;");
			btnDeSel.Attributes.Add("onClick","funcRemoveOrderBy();return false;");
			
			revFromTransactionDate.ValidationExpression = aRegExpDate;
			revToTransactionDate.ValidationExpression = aRegExpDate;

			btnNonpay.Attributes.Add("onClick","Page_ClientValidate();ShowReport();return false;");
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
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
