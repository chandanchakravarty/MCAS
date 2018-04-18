/******************************************************************************************
<Author					: -  Vijay Arora
<Start Date				: -	 01-03-2006	
<End Date				: -	
<Description			: - 	
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using System.Text;
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
using System.Xml;

namespace Cms.Policies.Aspx
{
	/// <summary>
	/// Summary description for TransactionLogDetail.
	/// </summary>
	public class PolicyProcessLogDetail : Cms.Policies.policiesbase
	{
		protected System.Web.UI.WebControls.Label capDescription;
		protected System.Web.UI.WebControls.Label lblDescription;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capAppNumber;
		protected System.Web.UI.WebControls.Label lblAppNumber;
		protected System.Web.UI.WebControls.Label capVersionNnmber;
		protected System.Web.UI.WebControls.Label lblVersionNumber;
		protected System.Web.UI.HtmlControls.HtmlTable tblHeadings;
		protected System.Web.UI.WebControls.DataGrid dgTrans;
		protected System.Web.UI.WebControls.Literal ltCoverage;
		StringBuilder sbTran = new StringBuilder();
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			
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
			//this.dgTrans.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgTrans_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

	}
}
