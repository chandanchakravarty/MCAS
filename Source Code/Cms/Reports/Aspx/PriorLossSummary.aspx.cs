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
	/// Summary description for PriorLossSummary.
	/// </summary>
	public class PriorLossSummary : Cms.CmsWeb.cmsbase 
	{
		protected System.Web.UI.WebControls.Label lblStartDate;
		protected System.Web.UI.WebControls.TextBox txtStartDate;
		protected System.Web.UI.WebControls.HyperLink hlkStartDate;
		protected System.Web.UI.WebControls.RegularExpressionValidator revStartDate;
		protected System.Web.UI.WebControls.CompareValidator cmpStartDate;


		protected System.Web.UI.WebControls.Label lblEndDate;
		protected System.Web.UI.WebControls.TextBox txtEndDate;
		protected System.Web.UI.WebControls.HyperLink hlkEndDate;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEndDate;
		protected System.Web.UI.WebControls.ListBox txtAGENCY_NAME;
		protected System.Web.UI.WebControls.ListBox txtlob;
		protected System.Web.UI.WebControls.TextBox txtCUSTOMER_NAME;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomer_ID;
		protected System.Web.UI.WebControls.Label lblOrderBy_2;
		protected System.Web.UI.WebControls.Label lblOrderBy_1;
		protected System.Web.UI.WebControls.ListBox lstOrderBy;
		protected System.Web.UI.WebControls.Button btnSel;
		protected System.Web.UI.WebControls.Button btnDeSel;		
		protected Cms.CmsWeb.Controls.CmsButton btnDisplayreport;
		
		
		public string  strSystemID="";
		public string strAgencyID="";
		//System.Resources.ResourceManager objResourceMgr;
		private void Page_Load(object sender, System.EventArgs e)
		{
			
			base.ScreenId = "440_1";
			revStartDate.ValidationExpression = aRegExpDate;
			revEndDate.ValidationExpression = aRegExpDate;

			btnDisplayreport.CmsButtonClass                 =   Cms.CmsWeb.Controls.CmsButtonType.Read; 
			btnDisplayreport.PermissionString				=	gstrSecurityXML;	

			hlkStartDate.Attributes.Add("OnClick","fPopCalendar(document.forms[0].txtStartDate,document.forms[0].txtStartDate)");			
			hlkEndDate.Attributes.Add("OnClick","fPopCalendar(document.forms[0].txtEndDate,document.forms[0].txtEndDate)");		
			btnSel.Attributes.Add("onClick","funcSetOrderBy();return false;");
			btnDeSel.Attributes.Add("onClick","funcRemoveOrderBy();return false;");
			btnDisplayreport.Attributes.Add("onClick","Page_ClientValidate();ShowReport();return false;");

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

			DataTable dtLOBs = Cms.CmsWeb.ClsFetcher.LOBs;
			txtlob.DataSource		= dtLOBs;
			txtlob.DataTextField		= "LOB_DESC";
			txtlob.DataValueField		= "LOB_ID";
			txtlob.DataBind();
			this.txtlob.Items.Insert(0,"All");
			this.txtlob.SelectedIndex =0;
         


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

		
		/*private void SetCaptions()
		{
			capFromDate.Text			=		objResourceMgr.GetString("txtFromDate");
			capToDate.Text			=		objResourceMgr.GetString("txtToDate");
		}*/
		
	}
}
