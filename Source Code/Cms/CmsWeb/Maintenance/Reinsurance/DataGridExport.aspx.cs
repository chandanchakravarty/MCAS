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
using System.IO; 
using System.Text;
//using Microsoft.Practices.EnterpriseLibrary.Data; 

namespace Glasses.Test
{
	/// <summary>
	/// Summary description for DataGridExport.
	/// </summary>
	public class DataGridExport : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DataGrid myDataGrid;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.Button Button3;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
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
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Button3.Click += new System.EventHandler(this.Button3_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			Response.Clear();
			Response.AddHeader("content-disposition", "attachment;filename=FileName.xls");
			Response.Charset = "";
			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			Response.ContentType = "application/vnd.xls";
			System.IO.StringWriter stringWrite = new System.IO.StringWriter();
			System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
			myDataGrid.RenderControl(htmlWrite);
			Response.Write(stringWrite.ToString());
			Response.End();
		}

		private void Button2_Click(object sender, System.EventArgs e)
		{
			Response.Clear();
			Response.AddHeader("content-disposition", "attachment;filename=FileName.doc");
			Response.Charset = "";
			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			Response.ContentType = "application/vnd.word";
			System.IO.StringWriter stringWrite = new System.IO.StringWriter();
			System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
			myDataGrid.RenderControl(htmlWrite);
			Response.Write(stringWrite.ToString());
			Response.End();
		}

		private void Button3_Click(object sender, System.EventArgs e)
		{
			/*Database db = DatabaseFactory.CreateDatabase(); 
			DBCommandWrapper selectCommandWrapper = db.GetStoredProcCommandWrapper("sp_GetLatestArticles"); 
			
			DataSet ds = db.ExecuteDataSet(selectCommandWrapper); 

			StringBuilder str = new StringBuilder(); 

			for(int i=0;i<=ds.Tables[0].Rows.Count - 1; i++) 
			{
				for(int j=0;j<=ds.Tables[0].Columns.Count - 1; j++) 
				{
					str.Append(ds.Tables[0].Rows[i][j].ToString()); 
				}
				str.Append("<BR>");
			}

			Response.Clear();
			Response.AddHeader("content-disposition", "attachment;filename=FileName.txt");
			Response.Charset = "";
			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			Response.ContentType = "application/vnd.text";
			System.IO.StringWriter stringWrite = new System.IO.StringWriter();
			System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
			Response.Write(str.ToString());
			Response.End();

            */
		}
	}
}
