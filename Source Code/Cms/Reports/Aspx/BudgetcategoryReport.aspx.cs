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
using Cms.DataLayer;
using Cms.CmsWeb;

namespace Reports.Aspx
{
	public class BudgetcategoryReport : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Label lblFinancialYearStart;
		protected System.Web.UI.WebControls.Panel Panel1;
		protected System.Web.UI.WebControls.DropDownList cmbFinancialYear;
		protected System.Web.UI.WebControls.Label lblBudgetCategory;
		protected System.Web.UI.WebControls.DropDownList cmbBudgetCategory;
		protected System.Web.UI.WebControls.DropDownList cmbReportType;
		protected System.Web.UI.WebControls.Label lblDepartment;
		protected System.Web.UI.WebControls.DropDownList cmbDepartment;
		protected Cms.CmsWeb.Controls.CmsButton btnReport;
		protected string strCalledfrom="";

		private void Page_Load(object sender, System.EventArgs e)
		{			
				
			base.ScreenId="410";
			
			btnReport.CmsButtonClass                =   Cms.CmsWeb.Controls.CmsButtonType.Read; 
			btnReport.PermissionString				=	gstrSecurityXML;	
			
			this.cmbReportType.Attributes.Add("onChange","javascript: ShowDetails();");

			btnReport.Attributes.Add("onClick","ShowReport();return false;");

			Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.Text , DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);	
			
			if(!IsPostBack)
			{
				try
				{	
					DataSet ds1 = objDataWrapper.ExecuteDataSet("Select convert(varchar,year(FISCAL_BEGIN_DATE))+ '-' + convert(varchar,year(FISCAL_END_DATE)) as DISPLAYDATE , FISCAL_ID as ID from act_general_ledger order by DISPLAYDATE");
					cmbFinancialYear.DataSource = ds1.Tables[0];
					cmbFinancialYear.DataTextField = "DISPLAYDATE";
					cmbFinancialYear.DataValueField = "ID";
					cmbFinancialYear.DataBind();
					//this.cmbFinancialYear.Items.Insert(0,"All");
					//this.cmbFinancialYear.SelectedIndex =0;

					DataSet ds2 = objDataWrapper.ExecuteDataSet("select CATEGORY_DEPARTEMENT_NAME AS NAME,CATEGEORY_ID as ID from ACT_BUDGET_CATEGORY where IS_ACTIVE='Y'order by NAME");
					cmbBudgetCategory.DataSource = ds2.Tables[0];
					cmbBudgetCategory.DataTextField = "Name";
					cmbBudgetCategory.DataValueField = "ID";
					cmbBudgetCategory.DataBind();
					this.cmbBudgetCategory.Items.Insert(0,"All");
					this.cmbBudgetCategory.SelectedIndex =0;

					this.cmbReportType.Items.Insert( 0,new ListItem("By Budget Category","0"));
					this.cmbReportType.Items.Insert( 1,new ListItem("By Department","1"));
					//this.cmbReportType.Items.Insert(1,"By Department");
					this.cmbReportType.SelectedIndex =0;
					string systemID= Cms.CmsWeb.cmsbase.CarrierSystemID;
					//string strSQLQuery = "SELECT ISNULL(USER_FNAME,'') + ' ' + ISNULL(USER_LNAME,'') AS WOLVERINE_USERS,[USER_ID] as WOLVERINE_USER_ID FROM MNT_USER_LIST WHERE USER_SYSTEM_ID = '" +systemID + "'";
					//DataSet ds3 = objDataWrapper.ExecuteDataSet("select CATEGORY_DEPARTEMENT_NAME AS NAME,CATEGEORY_ID as ID from ACT_BUDGET_CATEGORY where IS_ACTIVE='Y'order by NAME");
					DataSet ds3 = objDataWrapper.ExecuteDataSet("SELECT ISNULL(USER_FNAME,'') + ' ' + ISNULL(USER_LNAME,'') AS NAME,[USER_ID] as ID FROM MNT_USER_LIST WHERE USER_SYSTEM_ID = '" + systemID + "' order by NAME");
					cmbDepartment.DataSource = ds3.Tables[0];
					cmbDepartment.DataTextField = "Name";
					cmbDepartment.DataValueField = "ID";
					cmbDepartment.DataBind();
					this.cmbDepartment.Items.Insert(0,"All");
					this.cmbDepartment.SelectedIndex =0;
				}
				catch(Exception ex)
				{					
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				}
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
