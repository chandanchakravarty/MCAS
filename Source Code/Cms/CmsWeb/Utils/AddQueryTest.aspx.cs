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
using Cms.BusinessLayer.BlCommon;
namespace Cms.CmsWeb.Utils
{
	/// <summary>
	/// Summary description for AddQueryTest.
	/// </summary>
	public class AddQueryTest : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.DataGrid dgResult;
		protected System.Web.UI.WebControls.TextBox txtQuery;
		protected System.Web.UI.WebControls.TextBox txtPasswd;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label lbltop;
		protected System.Web.UI.WebControls.Label lblPerm;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.Button btnPerm;
		//Added by Charles on 8-Jun-2009
		protected System.Web.UI.WebControls.Button btnViewAgencyDetails;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!Page.IsPostBack)
			{
				Button2.Enabled = false;
				//Added by Charles on 8-Jun-2009
				btnViewAgencyDetails.Enabled=false;
			}
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
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.btnPerm.Click += new System.EventHandler(this.btnPerm_Click);
			//Added by Charles on 8-Jun-2009
			this.btnViewAgencyDetails.Click+= new EventHandler(this.btnViewAgencyDetails_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnPerm_Click(object sender, System.EventArgs e)
		{
			if(txtPasswd.Text == "Pr0ducer")
			{
				lblMessage.Text = "";
				Button2.Enabled = true;
				//Added by Charles on 8-Jun-2009
				btnViewAgencyDetails.Enabled=true;
			}
			else
				lblMessage.Text = "Password is not correct";
		}

		private void Button2_Click(object sender, System.EventArgs e)
		{
			if ( txtQuery.Text.Trim() == "" ) return;

			if ( System.Configuration.ConfigurationSettings.AppSettings["DB_CON_STRING"] == null)
			{
				Response.Write("Key DB_CON_STRING is missing in web.config.");
				return;
			}

            string connString = ClsCommon.ConnStr; //System.Configuration.ConfigurationSettings.AppSettings["DB_CON_STRING"].ToString();

			//Added by Sibin on 4 March 09 to Add encrypted log for Query executed from utils sections 
			int executedBy=int.Parse(GetUserId());
			DateTime executed_On = DateTime.Now;
				
			DataWrapper objDataWrapper = new DataWrapper(connString,CommandType.StoredProcedure);
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@EXECUTED_BY",executedBy);
			objDataWrapper.AddParameter("@EXECUTED_ON",executed_On);
			objDataWrapper.AddParameter("@QUERY_SQL",Cms.BusinessLayer.BlCommon.ClsCommon.EncryptString(txtQuery.Text.Trim()));
			objDataWrapper.ExecuteNonQuery("Proc_InsertUtilLog");
			objDataWrapper.ClearParameteres();
			objDataWrapper.Dispose();

			//Added till here
			try
			{
				DataSet ds = SqlHelper.ExecuteDataset(connString,CommandType.Text,txtQuery.Text.Trim());
			
				dgResult.DataSource = ds;
				dgResult.DataBind();
				ds.Dispose();				
				
			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;
				return;
			}
			
		}

		//Added by Charles on 8-Jun-2009
		private void btnViewAgencyDetails_Click(object sender, EventArgs e)
		{
            string connString = ClsCommon.ConnStr;// System.Configuration.ConfigurationSettings.AppSettings["DB_CON_STRING"].ToString();
			string query = @"SELECT  
							agcyLst.AGENCY_ID AS 'Agency ID',
							ISNULL(agcyLst.AGENCY_CODE,'') AS 'Agency Code',
							ISNULL(agcyLst.AGENCY_DISPLAY_NAME,'') AS 'Display Name',
							ISNULL(agcyLst.AGENCY_LIC_NUM,0) AS 'Total License',
							ISNULL(COUNT(usrLst.USER_SYSTEM_ID),0) AS 'Total Licensed Brics Users'
							FROM MNT_AGENCY_LIST agcyLst
							LEFT JOIN MNT_USER_LIST usrLst 
							ON agcyLst.AGENCY_CODE=usrLst.USER_SYSTEM_ID
							AND usrLst.LIC_BRICS_USER=10963 AND usrLst.IS_ACTIVE='Y'
							GROUP BY agcyLst.AGENCY_ID,agcyLst.AGENCY_CODE,agcyLst.AGENCY_DISPLAY_NAME,agcyLst.AGENCY_LIC_NUM
							ORDER BY agcyLst.AGENCY_ID";
			
			DataSet ds=null;
			int totalLicenseSum =0, totalLicensedBricUserSum=0;

			try
			{
				
				ds= SqlHelper.ExecuteDataset(connString,CommandType.Text,query);				
				
				DataRow dr=ds.Tables[0].NewRow();				
				dr["Agency ID"]=System.DBNull.Value;
				dr["Agency Code"]="";
				dr["Display Name"]="TOTAL";				
				foreach(DataRow row in ds.Tables[0].Rows)
				{
					totalLicenseSum+=Convert.ToInt32(row["Total License"]);
					totalLicensedBricUserSum+=Convert.ToInt32(row["Total Licensed Brics Users"]);
				}
				dr["Total License"]=totalLicenseSum.ToString();
				dr["Total Licensed Brics Users"]=totalLicensedBricUserSum.ToString();
				ds.Tables[0].Rows.Add(dr);
				
				dgResult.DataSource=ds;
				dgResult.DataBind();
				if(dgResult.Items.Count>0)
				dgResult.Items[dgResult.Items.Count-1].Font.Bold=true;			
				
			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;
				return;
			}
			finally
			{
				if(ds!=null)
					ds.Dispose();
			}

		}
	}
}
