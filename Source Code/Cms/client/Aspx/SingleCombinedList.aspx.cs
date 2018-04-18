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
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer;
using System.Text;

namespace Cms.Client.Aspx
{
	/// <summary>
	/// Summary description for SingleCombinedList.
	/// </summary>
	public class SingleCombinedList : Cms.Client.clientbase
	{
		protected Cms.CmsWeb.Controls.CmsButton btnBack;
		protected System.Web.UI.WebControls.TextBox txtDescription;
		protected System.Web.UI.WebControls.Label Label1;
        protected System.Web.UI.WebControls.Label capHeader;
		int	intCustId=0;
		protected string strdelString = "";
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			btnBack.Attributes.Add("onclick","javascript:return DoBack();");
			if(GetCalledFor()!="CLAIM")
				base.ScreenId = "108_1";	
			else
				base.ScreenId = "313_1";
			btnBack.CmsButtonClass		= CmsButtonType.Read;
			btnBack.PermissionString	= gstrSecurityXML;

			if(Request.Params["customer_ID"]!=null && Request.Params["customer_ID"].ToString().Length>0)
			intCustId	= int.Parse(Request.Params["customer_ID"].ToString());
			strdelString = Request.Params["NOTES_ID"].ToString();
			GetNotes(intCustId,strdelString);
            setCaptions();
		}

        private void setCaptions()
        {
            capHeader.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1848");
        }
		private void GetNotes(int intCustId,string strdelString)
		{
			StringBuilder strDescription = new StringBuilder();
			DataSet dsCustomer = new DataSet();
			dsCustomer = Cms.BusinessLayer.BlClient.clsCustomerNotes.GetCustomerInfo(intCustId,strdelString);

			if(dsCustomer!=null)
			{
				foreach(DataRow dr in dsCustomer.Tables[0].Rows)
				{
					strDescription.Append("\n");
					strDescription.Append(Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1845")+dr["SUBJECT"]);
					strDescription.Append("\r\n");
					strDescription.Append(Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1846")+dr["NOTES_DESC"]);
					strDescription.Append("\r\n");
					strDescription.Append("\r\n");
					

				}
				txtDescription.Text = strDescription.ToString();
			}
			else
			{
                txtDescription.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1847");//"No Record Found";
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
