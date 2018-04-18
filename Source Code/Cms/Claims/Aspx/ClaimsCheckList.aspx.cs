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

using Cms.Claims;
using Cms.Model.Claims;
using Cms.CmsWeb.Controls; 
using Cms.CmsWeb.WebControls;

using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BLClaims;

using Cms.ExceptionPublisher.ExceptionManagement;

using System.Data.SqlClient;

namespace Claims.Aspx
{
	/// <summary>
	/// Summary description for ClaimsCheckList.
	/// </summary>
	public class ClaimsCheckList : Cms.Claims.ClaimBase
	{
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCheckedRowIDs;
		protected System.Web.UI.WebControls.DataGrid dgCheckList;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			#region GETTING BASE COLOR FOR ROW SELECTION
			string colorScheme=GetColorScheme();
			string colors="";

			switch (int.Parse(colorScheme))
			{
				case 1:
					colors=System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR1").ToString();     
					break; 
				case 2:
                    colors = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR2").ToString();     
					break;
				case 3:
                    colors = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR3").ToString();     
					break;
				case 4:
                    colors = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR4").ToString();     
					break;
			}

			if(colors!="")
			{
				string [] baseColor=colors.Split(new char []{','});  
				if(baseColor.Length>0)
					colors= "#" + baseColor[0];
			}
			#endregion         
			
			string strSelectClause = "  SELECT CHECK_NUMBER as 'Check Number', CONVERT(VARCHAR, CHECK_DATE, 101) as Date, CHECK_AMOUNT as Amount, CHECK_NOTE as Notes FROM ACT_CHECK_INFORMATION  ";
			string strWhereClause  = " "; 

			switch (Request.QueryString["ID"].ToString().ToUpper())
			{
				case "ISSUED":
					strWhereClause  =" WHERE  CHECK_TYPE=9937  ";
					break;

				case "CLEARED":
					strWhereClause  =" WHERE IS_BNK_RECONCILED = 'Y'AND CHECK_TYPE=9937 ";
					break;

				case "OUTSTANDING":
					strWhereClause  =" WHERE IS_BNK_RECONCILED_VOID = 'Y' AND CHECK_TYPE=9937 ";
					break;
			}

            SqlDataAdapter DA = new SqlDataAdapter(strSelectClause + strWhereClause, System.Configuration.ConfigurationManager.AppSettings.Get("DB_CON_STRING"));
			DataSet DS = new DataSet();
			DA.Fill(DS);
			dgCheckList.DataSource=DS;
			dgCheckList.DataBind();

			
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
