using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Cms.BusinessLayer.BlAccount;
namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for ShowARFee.
	/// </summary>
	public class ShowARFee :  Cms.Account.AccountBase
	{
		protected System.Web.UI.HtmlControls.HtmlTableCell tdArReport;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdHeader;
		public string strTitle = "";
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			StringBuilder strBuilder=new StringBuilder();
			strBuilder.Append("");
			if(Request.QueryString["CALLED_FROM"] != null && Request.QueryString["CALLED_FROM"] == "F")
			{
				tdHeader.InnerText = "Fees Report";
				strTitle = "Fee Details";
				
				if(Request.QueryString["INSF_FEE"] != null)
				{
					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td class='DataGridRow' align='Left' width='5%'><b>Install Fee</b></td>");
					strBuilder.Append("<td class='DataGridRow' align='Left' width='5%'>" + Request.QueryString["INSF_FEE"] + "</b></td>");
					strBuilder.Append("</tr>");


				}
				if(Request.QueryString["LATE_FEE"] != null)
				{
					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td class='DataGridRow' align='Left' width='5%'><b>Late Fee</b></td>");
					strBuilder.Append("<td class='DataGridRow' align='Left' width='5%'>" + Request.QueryString["LATE_FEE"] + "</b></td>");
					strBuilder.Append("</tr>");


				}
				if(Request.QueryString["REINS_FEE"] != null)
				{
					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td class='DataGridRow' align='Left' width='5%'><b>Reins Fee</b></td>");
					strBuilder.Append("<td class='DataGridRow' align='Left' width='5%'>" + Request.QueryString["REINS_FEE"] + "</b></td>");
					strBuilder.Append("</tr>");


				}
				if(Request.QueryString["NSF_FEE"] != null)
				{
					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td class='DataGridRow' align='Left' width='5%'><b>NSF Fee</b></td>");
					//strBuilder.Append("<td class='DataGridRow' align='Left' width='5%'>" + Request.QueryString["NSF"] + "</b></td>");
					strBuilder.Append("<td class='DataGridRow' align='Left' width='5%'>" + Request.QueryString["NSF_FEE"] + "</b></td>");
					strBuilder.Append("</tr>");

				}
			}
			else if(Request.QueryString["CALLED_FROM"] != null && 
					(Request.QueryString["CALLED_FROM"] == "A" || Request.QueryString["CALLED_FROM"] == "D")
				)
			{
				tdHeader.InnerText = "Adjustment Details";
				int OpenItemID = 0, PolicyID = 0;

				if(Request.QueryString["OP_ID"] != null && Request.QueryString["OP_ID"].Trim() != "")
				{
					OpenItemID = Convert.ToInt32(Request.QueryString["OP_ID"].Trim());
				}
				if(Request.QueryString["POL_ID"] != null && Request.QueryString["POL_ID"].Trim() != "")
				{
					PolicyID = Convert.ToInt32(Request.QueryString["POL_ID"].Trim());
				}
				string CalledFrom =  Request.QueryString["CALLED_FROM"] ;
				ClsDeposit objD = new ClsDeposit();
				DataSet ds =  objD.GetAdjustmentDetails(OpenItemID,PolicyID,CalledFrom );

				if(ds != null && ds.Tables.Count > 0)
				{
					DataTable dt = ds.Tables[0];
					for(int i = 0 ; i< dt.Rows.Count ; i++)
					{
						strBuilder.Append("<tr height='20'>");
						strBuilder.Append("<td class='DataGridRow' align='Left' width='5%'><b>" + dt.Rows[i]["POLICY_NUMBER"].ToString() + "</b></td>");
						strBuilder.Append("<td class='DataGridRow' align='Left' width='5%'><b>" + dt.Rows[i]["AMOUNT_APPLIED"].ToString()+ "</b></td>");
						strBuilder.Append("</tr>");
					}
				}
			}

			tdArReport.InnerHtml =strBuilder.ToString();

			#region CC Notes Description
			int spoolId = 0;
			if(Request.QueryString["CALLED_FROM"] != null && 
				Request.QueryString["CALLED_FROM"].ToString() != "")
			{
				if(Request.QueryString["CALLED_FROM"].ToString().ToUpper().Trim() == "CC")
				{
					tdHeader.InnerText = "Note";
					strTitle = "Credit Card Note Details";
					if(Request.QueryString["SPOOL_ID"] != null && 
						Request.QueryString["SPOOL_ID"].ToString() != "")
					{
							spoolId = int.Parse(Request.QueryString["SPOOL_ID"].ToString());
						ClsAccount obj = new ClsAccount();
						DataSet ds = null;
						ds = obj.GetCCNoteDetails(spoolId);
						if(ds!=null && ds.Tables[0].Rows.Count > 0)
						{
							strBuilder.Append("<tr height='20'>");
							strBuilder.Append("<td class='DataGridRow' align='Left' width='5%'>" + ds.Tables[0].Rows[0]["NOTE"].ToString().Replace("\r\n","<br>")+ "</td>");
							strBuilder.Append("</tr>");

							tdArReport.InnerHtml =strBuilder.ToString();
							

                        }
					}
				}
			}
			#endregion		
		
		
	
		}

			public string GetTitle()
			{
				return strTitle;
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
