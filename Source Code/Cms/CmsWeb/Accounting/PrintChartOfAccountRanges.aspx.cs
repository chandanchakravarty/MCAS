	/******************************************************************************************
	<Author					: -   Ajit Singh Chahal
	<Start Date				: -   5/16/2005 11:31:31 AM
	<End Date				: -	
	<Description			: -   Business Logic for GL Account Ranges.
	<Review Date			: - 
	<Reviewed By			: - 	
	Modification History
	<Modified Date			: - 
	<Modified By			: - 
	<Purpose				: - 
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
using Cms.Model.Maintenance.Accounting;
using Cms.BusinessLayer.BlCommon.Accounting;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb;
using System.Resources;
using System.Reflection;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;

namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for PrintChartOfAccountRanges.
	/// </summary>
	public class PrintChartOfAccountRanges  : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
        protected System.Web.UI.WebControls.Label capChart_of_Account_Ranges;
        protected System.Web.UI.WebControls.Label capDate;
        protected System.Web.UI.WebControls.Label capTime;
        protected System.Web.UI.WebControls.Button btnPrint;
        protected ResourceManager objResourceMgr; 
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId="124_0_0";
            objResourceMgr = new System.Resources.ResourceManager("cms.cmsweb.Maintenance.PrintChartOfAccountRanges", System.Reflection.Assembly.GetExecutingAssembly());
			DataGrid1.DataSource = ClsGLAccountRanges.GetSubRanges();
			DataGrid1.DataBind();
            SetCaptions();
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

        private void SetCaptions()
        {
            capChart_of_Account_Ranges.Text = objResourceMgr.GetString("capChart_of_Account_Ranges");
            capDate.Text = objResourceMgr.GetString("capDate");
            capTime.Text = objResourceMgr.GetString("capTime");
            
        }
	}
}
