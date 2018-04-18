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

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for AccountDetails.
	/// </summary>
	public class AccountDetails : Cms.Account.AccountBase
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.HtmlControls.HtmlTableCell myTd;

		private void Page_Load(object sender, System.EventArgs e)
		{
			
			base.ScreenId = "200_0";
		// Put user code to initialize the page here
			int frmSource=0,toSource=0,state=0,lob=0;
			double accountNo=0;
			string updFrom="",frmDate="",toDate="";
				
			if(Request.Params["frmSource"]!=null && Request.Params["frmSource"].ToString().Length>0)
				frmSource	= int.Parse(Request.Params["frmSource"].ToString());
			if(Request.Params["toSource"]!=null && Request.Params["toSource"].ToString().Length>0)
				toSource	= int.Parse(Request.Params["toSource"].ToString());
			if(Request.Params["frmDate"]!=null && Request.Params["frmDate"].ToString().Length>0)
				frmDate= Request.Params["frmDate"].ToString();
			if(Request.Params["toDate"]!=null && Request.Params["toDate"].ToString().Length>0)
				toDate	= Request.Params["toDate"].ToString();

			if(Request.Params["acc"]!=null && Request.Params["acc"].ToString().Length>0)
				accountNo	= double.Parse(Request.Params["acc"].ToString());
			if(Request.Params["updFrom"]!=null && Request.Params["updFrom"].ToString().Length>0)
				updFrom	= Request.Params["updFrom"].ToString();
			if(Request.Params["lob"]!=null && Request.Params["lob"].ToString().Length>0)
				lob= int.Parse(Request.Params["lob"].ToString());
			if(Request.Params["state"]!=null && Request.Params["state"].ToString().Length>0)
				state	= int.Parse(Request.Params["state"].ToString());

			AccountDetail(frmSource,toSource,frmDate,toDate,accountNo,updFrom,lob,state);
			
			
		}

		private void AccountDetail(int frmSource,int toSource,string frmDate,string toDate,double accountNo,string updFrom,int lob,int state)
		{
			StringBuilder strBuilder = new StringBuilder();
			Cms.BusinessLayer.BlCommon.Accounting.ClsGlAccounts objAccount = new Cms.BusinessLayer.BlCommon.Accounting.ClsGlAccounts();
			DataSet objDataSet = new DataSet();

			//string Datet =DateTime.Now.ToString().Substring(0,DateTime.Now.ToString().IndexOf(' ')).ToString();
			//string TimeD =DateTime.Now.TimeOfDay.ToString().Substring(0,DateTime.Now.TimeOfDay.ToString().IndexOf('.')).ToString();

			//string strTransactionDate="";
			//strBuilder.Append("<table cellSpacing='1' cellPadding='1' border='0' class='tableWidthHeader' align=center>");
			strBuilder.Append("<table cellSpacing='1' cellPadding='1' border='0' width='100%' align='center'>");
			
			/*strBuilder.Append("<tr>");
			strBuilder.Append("<td align='center' bgColor='white' colSpan='16'>");
			strBuilder.Append("<h4>Account Details</h4>");
			strBuilder.Append("</td>");
			strBuilder.Append("</tr>");

			strBuilder.Append("<tr>");
			strBuilder.Append("<td align='center' colSpan='17' class='DataGridRow'>Today's Date&nbsp;"+Datet+"</td>");
			strBuilder.Append("</tr>");
			
			strBuilder.Append("<tr>");
			strBuilder.Append("<td align='center' colSpan='17' class='DataGridRow'>Time &nbsp;"+TimeD+"</td>");
			strBuilder.Append("</tr>");

			strBuilder.Append("<tr> <!--commented by kailash 05/27/2005 itrack# 12105-->");
			//strBuilder.Append("<!--<th colspan='10'>");
			//strBuilder.Append("Preview Item List");
			//strBuilder.Append("</th>-->");
			
			strBuilder.Append("<th  class=headereffectCenter colspan=17>");
			strBuilder.Append("Account Posting Details");

			strBuilder.Append("</th>");
			strBuilder.Append("</tr>");*/

		//	strBuilder.Append("<tr>");
			//strBuilder.Append("<td align='center' bgColor='white' colSpan='17'>&nbsp;</td>");
		//	strBuilder.Append("</tr>");

//			strBuilder.Append("<tr>");
//			strBuilder.Append("<td class='DataGridRow' align='Left'><b>Transaction Date : </b>"+strTransactionDate+"</td>");
//			strBuilder.Append("</tr>");
			
			strBuilder.Append("<tr height='20'>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='20%'><b>Account</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Right' width='4%'><b>Source #</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='8%'><b>Date</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Right' width='8%'><b>Amount</b></td>");
			//strBuilder.Append("<td class='headereffectWebGrid' align='Right' width='8%'><b>Agency Comm.</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='13%'><b>Agency Name</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='15%'><b>Customer Name</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='12%'><b>Policy #</b></td>");
			
//		    strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='0%'><b>Policy Ver Id</b></td>");

			strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='4%'><b>Bill Code</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Right' width='8%'><b>Gross Amount</b></td>");
		
			//strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='8%'><b>Transaction Code</b></td>");
			//strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='6%'><b>Lob</b></td>");

			//strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='6%'><b>State</b></td>");
			
			//strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='0%'><b>Vendor</b></td>");			
			//strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='0%'><b>Tax</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='8%'><b>Update From</b></td>");
			strBuilder.Append("</tr>");
			
			//double dblTotalAmount=0.0;
			objDataSet=objAccount.GetAccountDetails(frmSource,toSource,frmDate,toDate,accountNo,updFrom,lob,state);
			try
			{				
				if(objDataSet!=null)
				{
					foreach(DataRow dr in objDataSet.Tables[0].Rows)
					{
						//dblTotalAmount =dblTotalAmount+double.Parse(dr["AMOUNT"].ToString());
						strBuilder.Append("<tr>");
						strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["ACCOUNT"]+"</td>");
						strBuilder.Append("<td class='DataGridRow' align='Right'>"+dr["SOURCE_NUM"]+"</td>");
						strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["SOURCE_TRAN_DATE"]+"</td>");
						strBuilder.Append("<td class='DataGridRow' align='Right'>"+dr["TRANSACTION_AMOUNT"]+"</td>");
						//strBuilder.Append("<td class='DataGridRow' align='Right'>"+dr["AGENCY_COMM_AMT"]+"</td>");
						strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["AGENCY_DISPLAY_NAME"]+"</td>");
						strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["CUSTOMER_NAME"]+"</td>");
						strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["POLICY_NUMBER"]+"</td>");
						//strBuilder.Append("<td class='DataGridRow' align='Right'>"+dr["POLICY_VERSION_ID"]+"</td>");
						strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["BILL_CODE"]+"</td>");

						strBuilder.Append("<td class='DataGridRow' align='Right'>"+dr["GROSS_AMOUNT"]+"</td>");
						//strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["ITEM_TRAN_CODE"]+"</td>");
						//strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["LOB_DESC"]+"</td>");
						//strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["STATE_NAME"]+"</td>");
						//strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["VENDOR_NAME"]+"</td>");
						//strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["TAX_NAME"]+"</td>");
						strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["UPDATED_FROM"]+"</td>");
						
						strBuilder.Append("</tr>");
						
						
					
					}
				}
				else
				{
					//strBuilder.Append("<table>");
					strBuilder.Append("<tr>");
					strBuilder.Append("<td class='DataGridRow' align='Left' colspan='17'>No Record Found.</td>");
					strBuilder.Append("</tr>");
					myTd.InnerHtml = strBuilder.ToString();
					return;


				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
			

			/*strBuilder.Append("<TR>");
			strBuilder.Append("<td align='right' colSpan='17'></b></td>");
			//strBuilder.Append("<td class='DataGridRow' align='right'>"+dblTotalAmount+"</td>");
			//strBuilder.Append("<td align='right' colSpan='2'></td>");
			
			strBuilder.Append("</TR>");*/

			strBuilder.Append("<tr>");
			strBuilder.Append("<td align='center' bgColor='white' colSpan='12'>&nbsp;</td>");
//			strBuilder.Append("<td align='center' bgColor='white' colSpan='17'>&nbsp;</td>");
//			strBuilder.Append("<td align='center' bgColor='white' colSpan='17'>&nbsp;</td>");
//			strBuilder.Append("<td align='center' bgColor='white' colSpan='17'>&nbsp;</td>");
			strBuilder.Append("</tr>");
			strBuilder.Append("<tr height='20'>");
			strBuilder.Append("<td id='buttons' colSpan='12'><span id='spn_Button'>");
			strBuilder.Append("<table cellSpacing='0' cellPadding='2'>");
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
