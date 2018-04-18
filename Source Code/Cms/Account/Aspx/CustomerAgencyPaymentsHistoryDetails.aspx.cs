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
using Cms.CmsWeb.Controls;

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for CustomerAgencyPaymentsHistoryDetails.
	/// </summary>
	public class CustomerAgencyPaymentsHistoryDetails : Cms.Account.AccountBase
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdCustomerAgencyPaymentsHistory;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tbDataGrid;		
		protected System.Web.UI.HtmlControls.HtmlTableRow  trPrint;
		protected System.Web.UI.WebControls.DataGrid dgVenPendInv;
		//protected System.Web.UI.WebControls.Label lblMessage;
		//protected Cms.CmsWeb.Controls.CmsButton btnPrint;
		//protected Cms.CmsWeb.Controls.CmsButton btnClose;
		string PolicyNo="";
		string FromDate="";
		string ToDate="";
		string Agency="";
		string Customer="";
		string Amount="";
		public static int numberDiv;
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			base.ScreenId = " ";
			GetFormValues();
			BindGrid(PolicyNo,FromDate,ToDate,Agency,Customer,Amount);
			
			//CreateTable();

			//btnPrint.CmsButtonClass	= CmsButtonType.Execute;
			//btnPrint.PermissionString	= gstrSecurityXML;

			//btnClose.CmsButtonClass	= CmsButtonType.Execute;
			//btnClose.PermissionString	= gstrSecurityXML;

			if(!Page.IsPostBack)
			{
//				btnPrint.Attributes.Add("onclick","JavaScript:window.print();");
//				btnClose.Attributes.Add("onclick","JavaScript:window.close();");
			}

		}

		protected void GetFormValues()
		{		
	
			if(Request.Params["PolicyNo"]!=null && Request.Params["PolicyNo"].ToString().Length>0)
				PolicyNo= Request.Params["PolicyNo"].ToString();
			if(Request.Params["FromDate"]!=null && Request.Params["FromDate"].ToString().Length>0)
				FromDate	= Request.Params["FromDate"].ToString();
			if(Request.Params["ToDate"]!=null && Request.Params["ToDate"].ToString().Length>0)
				ToDate= Request.Params["ToDate"].ToString();
			if(Request.Params["Agency"]!=null && Request.Params["Agency"].ToString().Length>0)
				Agency	= Request.Params["Agency"].ToString();
			if(Request.Params["Customer"]!=null && Request.Params["Customer"].ToString().Length>0)
				Customer = Request.Params["Customer"].ToString();
			if(Request.Params["Amount"]!=null && Request.Params["Amount"].ToString().Length>0)
				Amount = Request.Params["Amount"].ToString();
			
		}
		
		protected string FormatMoney(object amount) 
		{		
			string tempMoney = String.Format("{0:0,0.00}",amount);
			if(Convert.ToDouble(tempMoney) < 0)
			{
				tempMoney = tempMoney.Substring(1, tempMoney.Length-1);
				if(tempMoney.StartsWith("0"))
				{
					tempMoney = tempMoney.Substring(1, tempMoney.Length-1);
				}
				tempMoney = "-" + tempMoney;
			}
			else
			{
				if(tempMoney.StartsWith("0"))
				{
					tempMoney = tempMoney.Substring(1, tempMoney.Length-1);
				}
			}
			return tempMoney;
		}
		//Code Added  For Itrack Issue #6610	
		private void BindGridSort(string PolicyNo,string FromDate,string ToDate,string Agency,string Customer,string Amount,string sortExpr)
		{
			try
			{
				GetFormValues();
		
				Cms.BusinessLayer.BlAccount.ClsAgencyStatement objAgencyStatement = new ClsAgencyStatement();
				DataSet objDataSet = new DataSet();
				objDataSet = objAgencyStatement.GetCustomerAgencyPaymentsHistory(FromDate,ToDate,PolicyNo,Agency,Customer,Amount);
			
				if(objDataSet.Tables[0].Rows.Count > 0)
				{	
					DataView dvSortView = new DataView(objDataSet.Tables[0]);
					if( (numberDiv%2) == 0)
						dvSortView.Sort = sortExpr + " " + "ASC";
					else
						dvSortView.Sort = sortExpr + " " + "DESC";
					numberDiv++;
					dgVenPendInv.DataSource = dvSortView;
					dgVenPendInv.DataBind();	
					//end
					tbDataGrid.Visible=true;
					//btnPrint.Visible = true;
					//btnClose.Visible = true; 
					trPrint.Visible = true;

					
					
					
				}
				else
				{
					lblMessage.Text = "Customer Agency Payments history details not found.";
					lblMessage.Visible = true;
					tbDataGrid.Visible=false;
					//btnPrint.Visible = false;
                    //btnClose.Visible = false;  
					trPrint.Visible = false;
				}
				
			}
			catch (Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);

			}
		}
		//Code Added  For Itrack Issue #6610	
		public void Sort_Grid(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs  e)
		{
			BindGridSort(PolicyNo,FromDate,ToDate,Agency,Customer,Amount,e.SortExpression.ToString());			
		}
		//Code Added  For Itrack Issue #6610	
		private void BindGrid(string PolicyNo,string FromDate,string ToDate,string Agency,string Customer,string Amount)
		{
			try
			{
				GetFormValues();
		        Cms.BusinessLayer.BlAccount.ClsAgencyStatement objAgencyStatement = new ClsAgencyStatement();
				DataSet objDataSet = new DataSet();
				objDataSet = objAgencyStatement.GetCustomerAgencyPaymentsHistory(FromDate,ToDate,PolicyNo,Agency,Customer,Amount);
				
				if(objDataSet.Tables[0].Rows.Count > 0)
				{
					dgVenPendInv.DataSource =  objDataSet.Tables[0];
					dgVenPendInv.DataBind();
					tbDataGrid.Visible=true;					
					//btnPrint.Visible = true;
					//btnClose.Visible = true;  
					trPrint.Visible = true;
					
					
				}
				else
				{
					
					lblMessage.Text = "Customer Agency Payments history details not found.";
					lblMessage.Visible = true;					
					//btnPrint.Visible = false;
					//btnClose.Visible = false; 
					tbDataGrid.Visible = false;					
					trPrint.Visible = false;
				}

				
			}
			catch (Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);

			}
		}
	//Code commented For Itrack Issue #6610
	/*	private void CreateTable()
		{
			string PolicyNo="";
			string FromDate="";
			string ToDate="";
			string Agency="";
			string Customer="";
			string Amount="";
	
			if(Request.Params["PolicyNo"]!=null && Request.Params["PolicyNo"].ToString().Length>0)
				PolicyNo= Request.Params["PolicyNo"].ToString();
			if(Request.Params["FromDate"]!=null && Request.Params["FromDate"].ToString().Length>0)
				FromDate	= Request.Params["FromDate"].ToString();
			if(Request.Params["ToDate"]!=null && Request.Params["ToDate"].ToString().Length>0)
				ToDate= Request.Params["ToDate"].ToString();
			if(Request.Params["Agency"]!=null && Request.Params["Agency"].ToString().Length>0)
				Agency	= Request.Params["Agency"].ToString();
			if(Request.Params["Customer"]!=null && Request.Params["Customer"].ToString().Length>0)
				Customer = Request.Params["Customer"].ToString();
			if(Request.Params["Amount"]!=null && Request.Params["Amount"].ToString().Length>0)
				Amount = Request.Params["Amount"].ToString();

			StringBuilder strBuilder = new StringBuilder();
			Cms.BusinessLayer.BlAccount.ClsAgencyStatement objAgencyStatement = new ClsAgencyStatement();
			DataSet objDataSet = new DataSet();
	
			objDataSet = objAgencyStatement.GetCustomerAgencyPaymentsHistory(FromDate,ToDate,PolicyNo,Agency,Customer,Amount);

			if (objDataSet == null)
			{
				//lblMessage.Text = "Unable to get Customer Agency Payments history details.";
				lblMessage.Text = "Customer Agency Payments history details not found.";
				return;
			}
			else
			{
				if (objDataSet.Tables[0].Rows.Count == 0)
				{
					//lblMessage.Text =  "Unable to get Customer Agency Payments history details.";
					lblMessage.Text = "Customer Agency Payments history details not found.";
					return;
				}
			}

			strBuilder.Append("<table cellSpacing='1' cellPadding='1' border='0' width='100%' align='center'>");
			strBuilder.Append("<tr height='20'>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='15%'><b>Customer Name</b></td>");
			//Received User change to Received By For Itrack Issue #5461.
			strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='15%'><b>Received By</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='15%'><b>Policy Number</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='20%'><b>Agency </b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='13%'><b>Amount </b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Right' width='12%'><b>Date Of Payment </b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Right' width='10%'><b>Mode Of Payment</b></td>");			
			strBuilder.Append("</tr>");
			
			try
			{				
				if(objDataSet!=null)
				{
					foreach(DataRow dr in objDataSet.Tables[0].Rows)
					{
						
						strBuilder.Append("<tr>");
						strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["CUSTOMER_NAME"]+"</td>");
						strBuilder.Append("<td class='DataGridRow' align ='Left'>"+dr["USER_NAME"]+"</td>"); 
						strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["POLICY_NUMBER"]+"</td>");
						strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["AGENCY_NAME"]+"</td>");
						strBuilder.Append("<td class='DataGridRow' align='Right'>"+ "$" + dr["AMOUNT"]+"&nbsp;&nbsp;</td>");
						strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["DATE_COMMITTED"]+"</td>");
						strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["MODE"]+"</td>");
						strBuilder.Append("</tr>");					
					}
				}
				else
				{
					strBuilder.Append("<tr>");
					strBuilder.Append("<td class='DataGridRow' align='Left' colspan='11'>No Record Found.</td>");
					strBuilder.Append("</tr>");
					tdCustomerAgencyPaymentsHistory.InnerHtml = strBuilder.ToString();
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

			tdCustomerAgencyPaymentsHistory.InnerHtml = strBuilder.ToString();
		}*/
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
