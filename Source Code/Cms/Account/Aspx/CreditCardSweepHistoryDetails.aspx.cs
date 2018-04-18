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
using Cms.BusinessLayer.BlAccount;

namespace Cms.Account.Aspx
{	
	public class CreditCardSweepHistoryDetails : Cms.Account.AccountBase
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdCreditCardSweepHistoryDetails;
		protected System.Web.UI.WebControls.Label lblAgencyName;
		string Users=""; //Added By Raghav For itrack Issue #4646
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId = "407_0";
			CreateTable();
		}

		private void CreateTable()
		{
			string DateFromSpool="";
			string DateToSpool="";
			string DateFromSweep="";
			string DateToSweep="";
			string ProcessStatus="";
			string TransactionAmount ="";
	
			if(Request.Params["DateFromSpool"]!=null && Request.Params["DateFromSpool"].ToString().Length>0)
				DateFromSpool= Request.Params["DateFromSpool"].ToString();
			if(Request.Params["DateToSpool"]!=null && Request.Params["DateToSpool"].ToString().Length>0)
				DateToSpool	= Request.Params["DateToSpool"].ToString();
			if(Request.Params["DateFromSweep"]!=null && Request.Params["DateFromSweep"].ToString().Length>0)
				DateFromSweep= Request.Params["DateFromSweep"].ToString();
			if(Request.Params["DateToSweep"]!=null && Request.Params["DateToSweep"].ToString().Length>0)
				DateToSweep	= Request.Params["DateToSweep"].ToString();
			if(Request.Params["ProcessStatus"]!=null && Request.Params["ProcessStatus"].ToString().Length>0)
	            ProcessStatus = Request.Params["ProcessStatus"].ToString();
			if(Request.Params["TransactionAmount"]!=null && Request.Params["TransactionAmount"].ToString().Length>0)
				TransactionAmount = Request.Params["TransactionAmount"].ToString();
			//Added By Raghav For itrack Issue #4646
			if(Request.Params ["Users"]!=null && Request.Params["Users"].ToString().Length>0)
				Users = Request.Params["Users"].ToString();  

			StringBuilder strBuilder = new StringBuilder();
			Cms.BusinessLayer.BlAccount.ClsAgencyStatement objAgencyStatement = new ClsAgencyStatement();
			DataSet objDataSet = new DataSet();
	
			objDataSet = objAgencyStatement.GetCreditCardSweepHistoryDetails(DateFromSpool,DateToSpool,DateFromSweep,DateToSweep,ProcessStatus,TransactionAmount,Users);

			//Message Change By Raghav For Itrack Issue #4923
			if (objDataSet == null)
			{
				lblMessage.Text = "No records found for this search criteria.";
				return;
			}
			else
			{
				if (objDataSet.Tables[0].Rows.Count == 0)
				{
					lblMessage.Text =  "No records found for this search criteria.";
					return;
				}
			}

			strBuilder.Append("<table cellSpacing='1' cellPadding='1' border='0' width='100%' align='center'>");
			strBuilder.Append("<tr height='20'>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='20%'><b>Customer Name</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='20%'><b>User Name</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='10%'><b>Policy Number</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='10%'><b>Amount</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='10%'><b>Spooled Date</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Right' width='10%'><b>Processed Date</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Right' width='10%'><b>Status</b></td>");			
			strBuilder.Append("<td class='headereffectWebGrid' align='Right' width='10%'><b>Additional Info</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Right' width='10%'><b>Deposit Number</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Right' width='10%'><b>Confirmation ID</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Right' width='20%'><b>Note</b></td>");
            strBuilder.Append("</tr>");
			
			try
			{
				double intSum = 0.0;
				if(objDataSet!=null)
				{
					foreach(DataRow dr in objDataSet.Tables[0].Rows)
					{
						//get ID
						int id = int.Parse((dr["IDEN_ROW_ID"].ToString()));
						
						strBuilder.Append("<tr>");
						strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["CUSTOMER_NAME"]+"</td>");
						strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["USER_NAME"]+"</td>");
						strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["POLICY_NUMBER"].ToString().ToUpper()+"</td>");
						strBuilder.Append("<td class='DataGridRow' align='Right'>"+ "$" + dr["AMOUNT"]+"</td>");
						strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["SPOOLED_DATETIME"]+"</td>");
						strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["PROCESSED_DATETIME"]+"</td>");
						strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["STATUS"]+"</td>");
						strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["ADDITIONAL_INFO"]+"</td>");
						strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["DEPOSIT_NUMBER"]+"</td>");
						strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["CONFIRMATION_ID"]+"</td>");
						//Commented and changed by Shikha Dixit against #6016.
						//strBuilder.Append("<td class='DataGridRow' align='Left' ><A href='#' onclick=\"window.open('ShowARFee.aspx?CALLED_FROM=CC&SPOOL_ID="+ id +"','','height=400, width=400,left=100px;top=20px;status= no, resizable= no, scrollbars=no, toolbar=no,location=no,menubar=no,left=20px;top=20px')\">" + "View" + "</a></td>");
						strBuilder.Append("<td class='DataGridRow' align='Left' ><A href='#' onclick=\"window.open('ShowARFee.aspx?CALLED_FROM=CC&SPOOL_ID="+ id +"','','height=400, width=400,left=100px;top=20px;status= no, resizable= no, scrollbars=yes, toolbar=no,location=no,menubar=no,left=20px;top=20px')\">" + "View" + "</a></td>");
						strBuilder.Append("</tr>");	
						//Added By Raghav Itrack Issue #4646
						
						if(dr["AMOUNT"].ToString()!="")
							intSum = intSum + double.Parse( dr["AMOUNT"].ToString().Replace(",","") );
					}
					//Added By Raghav Itrack Issue #4646
					strBuilder.Append("<tr>");
					strBuilder.Append("<td class='DataGridRow' align='Right' colspan='4'><b>Total Amount : "+ "$"+FormatMoney(intSum) + "</b></td>");
					strBuilder.Append("<td class='DataGridRow' align='Right' colspan='7'></td>");
					strBuilder.Append("</tr>");
				}
				else
				{
					strBuilder.Append("<tr>");
					strBuilder.Append("<td class='DataGridRow' align='Left' colspan='11'>No Record Found.</td>");
					strBuilder.Append("</tr>");
					tdCreditCardSweepHistoryDetails.InnerHtml = strBuilder.ToString();
					return;
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}			

			strBuilder.Append("<tr height='20'>");
			strBuilder.Append("<td id='buttons' colSpan='11'><span id='spn_Button'>");
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

			tdCreditCardSweepHistoryDetails.InnerHtml = strBuilder.ToString();
		}

		//Added By Raghav For Itrac Issue #4646
		protected string FormatMoney(object amount) 
		{		
			string tempMoney = String.Format("{0:0,0.00}", amount);
			if(tempMoney.StartsWith("0"))
			{
				tempMoney = tempMoney.Substring(1, tempMoney.Length-1);
			}
			return tempMoney;
			//	return String.Format("{0:C}", amount);
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
