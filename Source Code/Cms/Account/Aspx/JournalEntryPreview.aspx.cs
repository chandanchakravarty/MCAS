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
using System.Text;

using System.Data.SqlClient;
using Cms.BusinessLayer.BlAccount;

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for JournalEntryPreview.
	/// </summary>
	public class JournalEntryPreview : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.HtmlControls.HtmlTableCell myTd;
		protected string journal_num="";
		public string journal_type="";
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId = "172_0";
			// Put user code to initialize the page here
			int intJournalId=0;
			if(Request.QueryString["JOURNAL_ID"]!=null && Request.QueryString["JOURNAL_ID"].ToString().Length>0)
			{
				intJournalId = int.Parse(Request.QueryString["JOURNAL_ID"].ToString());
			}
			CreateTable(intJournalId);
			
		}



		private void CreateTable(int intJournalId)
		{
			StringBuilder strBuilder = new StringBuilder();
			ClsJournalEntryMaster objJournalEntryMaster = new ClsJournalEntryMaster();
			DataSet objDataSet = new DataSet();
	
			objDataSet=objJournalEntryMaster.PrintPriview(intJournalId);

			if (objDataSet == null)
			{
				lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("462");
				return;
			}
			else
			{
				if (objDataSet.Tables[0].Rows.Count == 0)
				{
					lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("462");
					return;
				}
			}

			string Datet =DateTime.Now.ToString().Substring(0,DateTime.Now.ToString().IndexOf(' ')).ToString();
			string TimeD =DateTime.Now.TimeOfDay.ToString().Substring(0,DateTime.Now.TimeOfDay.ToString().IndexOf('.')).ToString();
			string strTransactionDate="";
			if(objDataSet.Tables[0].Rows.Count>0)
			{
				strTransactionDate = objDataSet.Tables[0].Rows[0]["TRANS_DATE"].ToString();
				journal_num = objDataSet.Tables[0].Rows[0]["JOURNAL_ENTRY_NO"].ToString();
				journal_type = objDataSet.Tables[0].Rows[0]["JOURNAL_GROUP_TYPE"].ToString();
			}
			strBuilder.Append("<table cellSpacing='1' cellPadding='1' border='0' width='100%' align='center'>");
			
			strBuilder.Append("<tr height='20'>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='8%'><b>Type</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='15%'><b>Regarding</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='15%'><b>Reference Customer</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Right' width='8%'><b>Policy #</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='22%'><b>Account Description</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='7%'><b>Dept</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='7%'><b>Profit Center</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Right' width='10%'><b>Amount</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='8%'><b>Note</b></td>");
			strBuilder.Append("</tr>");
			
			double dblTotalAmount=0.0;
			//objDataSet=objJournalEntryMaster.PrintPriview(intJournalId);
			try
			{				
				if(objDataSet!=null)
				{
					foreach(DataRow dr in objDataSet.Tables[0].Rows)
					{
						dblTotalAmount =dblTotalAmount+double.Parse(dr["AMOUNT"].ToString());
						strBuilder.Append("<tr>");
						strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["TYPE"]+"</td>");
						strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["REGARDING_NAME"]+"</td>");
						strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["REF_CUSTOMER"]+"</td>");
						strBuilder.Append("<td class='DataGridRow' align='Right'>"+dr["POLICY_NUMBER"]+"</td>");
						strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["ACC_NUMBER"]+ " - " + dr["ACC_DESCRIPTION"]+"</td>");
						strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["DEPT_NAME"]+"</td>");
						strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["PC_NAME"]+"</td>");
						strBuilder.Append("<td class='DataGridRow' align='Right'>"+dr["AMOUNT"]+"</td>");
						strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["NOTE"]+"</td>");
						strBuilder.Append("</tr>");
						
						
					
					}
				}
				else
				{
					//strBuilder.Append("<table>");
					strBuilder.Append("<tr>");
					strBuilder.Append("<td class='DataGridRow' align='Left' colspan='10'>No Record Found.</td>");
					strBuilder.Append("</tr>");
					myTd.InnerHtml = strBuilder.ToString();
					return;


				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
			

			strBuilder.Append("<TR>");
			strBuilder.Append("<td class='DataGridRow' align='right' colSpan='7'><b>Total Amount</b></td>");
			strBuilder.Append("<td class='DataGridRow' colSpan='2' align='right'>"+dblTotalAmount.ToString("N")+"</td>");
			strBuilder.Append("<td class='DataGridRow' align='right'></td>");
			strBuilder.Append("</TR>");

//			strBuilder.Append("<tr>");
//			strBuilder.Append("<td colspan='10' align='center' bgColor='white' colSpan='10'>&nbsp;</td>");
//			strBuilder.Append("</tr>");

			strBuilder.Append("<tr height='20'>");
			strBuilder.Append("<td id='buttons' colSpan='10'><span id='spn_Button'>");
			strBuilder.Append("<table cellSpacing='0' cellPadding='2' width='100%'>");
			strBuilder.Append("<tr class='tableFooter'>");
			strBuilder.Append("<td height='24' class='midcolora'><INPUT class='clsButton' onclick='JavaScript:showPrint();' type='button' value='Print'>");
			strBuilder.Append("<!-- <a href='JavaScript:showPrint()'><img src='../images/Print_blue.gif' border='0' WIDTH='78' HEIGHT='24'></a> --></td>");
			strBuilder.Append("<td height='24' class='midcolorr'><INPUT class='clsButton' onclick='JavaScript:window.close();' type='button' value='Close'>");
			strBuilder.Append("<!-- <a href='JavaScript:window.close()'><img src='../images/close_blue.gif' border='0' WIDTH='78' HEIGHT='24'></a> --></td>");
			strBuilder.Append("</tr>");
			strBuilder.Append("</table>");
			strBuilder.Append("</span>");
			strBuilder.Append("</td>");
			strBuilder.Append("</tr>");
			strBuilder.Append("</TD></TR></table>");

			myTd.InnerHtml = strBuilder.ToString();
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
