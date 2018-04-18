/******************************************************************************************
<Author					: -   Raghav Gupta
<Start Date				: -	  8/8/2008 
<End Date				: -	
<Description			: -   Report for Entities Excluded 1099
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
using Cms.CmsWeb.Controls;
using Cms.CmsWeb;
namespace Reports.Aspx
{
	/// <summary>
	/// Summary description for Entities_Excluded_In_1099.
	/// </summary>
	public class Entities_Excluded_In_1099 : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.DropDownList cmb_Year;  
		protected Cms.CmsWeb.Controls.CmsButton btnReport;
		private void Page_Load(object sender, System.EventArgs e)
		{

			// Put user code to initialize the page here
			base.ScreenId="431";
			btnReport.CmsButtonClass		=	CmsButtonType.Write;
			btnReport.PermissionString		=	gstrSecurityXML;
			btnReport.Attributes.Add("onClick","ShowReport();return false;");

			for (int i=System.DateTime.Now.Year;i>=2000;i--)
			{
				cmb_Year.Items.Add(i.ToString());
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
