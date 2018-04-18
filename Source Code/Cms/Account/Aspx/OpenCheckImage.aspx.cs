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
using Cms.BusinessLayer.BlAccount;
using Cms.CmsWeb.Controls; 
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlCommon.Accounting;



namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for CheckAmount.
	/// </summary>
   public class CheckAmount :Cms.Account.AccountBase
  {
	   
	   protected System.Web.UI.HtmlControls.HtmlAnchor  hrefCheckFilePath_Front;
       protected System.Web.UI.HtmlControls.HtmlAnchor  hrefCheckFilePath_Back;
	   protected System.Web.UI.HtmlControls.HtmlAnchor  hrefStubFilePath_Front;
	   protected System.Web.UI.HtmlControls.HtmlAnchor  hrefStubFilePath_Back;
	   protected System.Web.UI.WebControls.Label lblMessage;
	   protected System.Web.UI.HtmlControls.HtmlTable tblRTL_IMAGES;
	   protected System.Web.UI.HtmlControls.HtmlTable tblRTL_MSG;
	   protected System.Web.UI.WebControls.Label lblCheckFilePath_Front;
	   protected System.Web.UI.WebControls.Repeater rpRt_IMG;
	   protected System.Web.UI.WebControls.Label lblStubFilePath_Front;
	   
		private void Page_Load(object sender, System.EventArgs e)
		{               
			if(!IsPostBack)
			{
				if(Request.QueryString!= null )
				{
					//TO MOVE TO LOCAL VSS
					string batch_no = Request.QueryString["BN"]; 
					string group_no = Request.QueryString["GN"] ;
					DataSet ds = null;
					ds = ClsOpenCheckImage.OpenCheck(batch_no,group_no);								
					if(ds!=null && ds.Tables.Count >0 && ds.Tables[0].Rows.Count > 0)   
					{
						/*if(ds.Tables[0].Rows[0]["CheckFilePath_Front"] != null && ds.Tables[0].Rows[0]["CheckFilePath_Front"].ToString().Trim() != "")
						{
							hrefCheckFilePath_Front.HRef =ds.Tables[0].Rows[0]["CheckFilePath_Front"].ToString();
						}
						
						if(ds.Tables[0].Rows[0]["CheckFilePath_Back"] != null && ds.Tables[0].Rows[0]["CheckFilePath_Back"].ToString().Trim() != "")
						{
							hrefCheckFilePath_Back.HRef = ds.Tables[0].Rows[0]["CheckFilePath_Back"].ToString();
						}
						
						if(ds.Tables[0].Rows[0]["StubFilePath_Front"] != null && ds.Tables[0].Rows[0]["StubFilePath_Front"].ToString().Trim() != "")
						{
							hrefStubFilePath_Front.HRef = ds.Tables[0].Rows[0]["StubFilePath_Front"].ToString();
						}
						
						if(ds.Tables[0].Rows[0]["StubFilePath_Back"] != null && ds.Tables[0].Rows[0]["StubFilePath_Back"].ToString().Trim() != "")
						{
							hrefStubFilePath_Back.HRef = ds.Tables[0].Rows[0]["StubFilePath_Back"].ToString();
						}
						tblRTL_IMAGES.Visible = true;
						tblRTL_MSG.Visible= false;	*/
	                    rpRt_IMG.DataSource = ds.Tables[0]; 
						rpRt_IMG.DataBind(); 

					}	
					else
					{
						//tblRTL_IMAGES.Visible = false;
						//tblRTL_MSG.Visible= true;
						lblMessage.Text = "No image found in RTL View associated with this deposit. ";
					}
					
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
