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
	public class ProfitandLoss : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.DropDownList cmbMonth;
		protected System.Web.UI.WebControls.Label lblFinancialYearStart;
		protected System.Web.UI.WebControls.Panel Panel1;
		protected System.Web.UI.WebControls.DropDownList cmbFinancialYear;
		protected System.Web.UI.WebControls.Label lblGeneralLedger;
		protected System.Web.UI.WebControls.DropDownList cmbGeneralLedger;
		protected Cms.CmsWeb.Controls.CmsButton btnReport;
		protected string strCalledfrom="";

		private void Page_Load(object sender, System.EventArgs e)
		{			
			strCalledfrom=Request.QueryString["CALLEDFROM"].ToString();
			if (strCalledfrom == "PL")
				base.ScreenId="354";
			else if (strCalledfrom =="TB")
				base.ScreenId="355";
			else if (strCalledfrom == "BS")
				base.ScreenId="357";

			btnReport.CmsButtonClass                 =   Cms.CmsWeb.Controls.CmsButtonType.Read; 
			btnReport.PermissionString				=	gstrSecurityXML;	

			btnReport.Attributes.Add("onClick","ShowReport();return false;");

			Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.Text , DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);	
			if(!IsPostBack)
			{
				try
				{						
					//DataSet ds1 = objDataWrapper.ExecuteDataSet("Select convert(varchar,FISCAL_BEGIN_DATE,101)+ '-' + convert(varchar,FISCAL_END_DATE,101) as DISPLAYDATE from act_general_ledger order by DISPLAYDATE");
					DataSet ds1 = objDataWrapper.ExecuteDataSet("Select convert(varchar,year(FISCAL_BEGIN_DATE))+ '-' + convert(varchar,year(FISCAL_END_DATE)) as DISPLAYDATE from act_general_ledger order by DISPLAYDATE");
					cmbFinancialYear.DataSource = ds1.Tables[0];
					cmbFinancialYear.DataTextField = "DISPLAYDATE";
					cmbFinancialYear.DataValueField = "DISPLAYDATE";
					cmbFinancialYear.DataBind();

					DataSet ds2 = objDataWrapper.ExecuteDataSet("Select distinct LEDGER_NAME as Name,GL_ID as ID from Act_general_ledger");
					cmbGeneralLedger.DataSource = ds2.Tables[0];
					cmbGeneralLedger.DataTextField = "Name";
					cmbGeneralLedger.DataValueField = "ID";
					cmbGeneralLedger.DataBind();
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
