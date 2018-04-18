using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Cms.CmsWeb.Aspx
{
	/// <summary>
	/// Summary description for error.
	/// </summary>
	public class error : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblUrl;
		protected System.Web.UI.WebControls.Label lblExDetail;
		
		private void Page_Load(object sender, System.EventArgs e)
		{
//			if((HttpContext.Current.Items["refPath"] != null)||(HttpContext.Current.Items["ExMesssage"] != null))
//			{
//				if (HttpContext.Current.Items["refPath"] != null)
//				{
//					lblUrl.Text			=	HttpContext.Current.Items["refPath"].ToString();
//				}
//				if(HttpContext.Current.Items["ExMesssage"] != null)
//				{
//					lblExDetail.Text	=	HttpContext.Current.Items["ExMesssage"].ToString();
//				}
//				HttpContext.Current.Server.ClearError();
//			}
//			else
//			{
//				if(Request["refPath"] != null)
//					lblUrl.Text			=	Request["refPath"];
//				if(Request["ExMesssage"] != null)
//					lblExDetail.Text	=	Request["ExMesssage"];
//			}
//			

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
