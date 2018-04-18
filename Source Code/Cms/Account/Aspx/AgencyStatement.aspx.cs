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
using System.Text;

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for AgencyStatement.
	/// </summary>
	public class AgencyStatement : Cms.Account.AccountBase
	{
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAgencyStatementTD;
		protected System.Web.UI.WebControls.Label lblAgencyName;
		protected System.Web.UI.WebControls.Label lblMessage;
		public string MonthName = "";
		public int MonthYear = DateTime.Now.Year;

		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId = "222_0";
			int intMonthNumber = 10;
			CreateTable(intMonthNumber);
		}

		private void GetMonthName(int Month)
		{
		

			switch(Month)
			{
				case 1:
					MonthName = "January";
					break;
				case 2:
					MonthName = "Febuary";
					break;
				case 3:
					MonthName = "March";
					break;
				case 4:
					MonthName = "April";
					break;
				case 5:
					MonthName = "May";
					break;
				case 6:
					MonthName = "June";
					break;
				case 7:
					MonthName = "July";
					break;
				case 8:
					MonthName = "August";
					break;
				case 9:
					MonthName = "September";
					break;
				case 10:
					MonthName = "October";
					break;
				case 11:
					MonthName = "November";
					break;
				case 12:
					MonthName = "December";
					break;
			}
		}

		private void CreateTable(int intJournalId)
		{
			
			int AgencyId, MonthNumber;//,MonthYear;
			string commType = "";
			double BillCodeWiseBalance = 0;
			double NetBalance = 0;
			string BillCode = "";

			double GrossTotalPremium = 0;
			double NetTotalPremium = 0;

			double GrossComm = 0;
			double NetComm = 0;

			AgencyId	= Convert.ToInt32(Request.Params["AGENCY_ID"]);
			MonthNumber	= Convert.ToInt32(Request.Params["MONTH"]);
			MonthYear =  Convert.ToInt32(Request.Params["YEAR"]);
			commType =  Request.Params["COMM_TYPE"].ToString().Trim();


			GetMonthName(MonthNumber);

			StringBuilder strBuilder = new StringBuilder();
			Cms.BusinessLayer.BlAccount.ClsAgencyStatement objAgencyStatement = new ClsAgencyStatement();
			DataSet objDataSet = new DataSet();
	
			objDataSet = objAgencyStatement.GetAgencyStatement(AgencyId, MonthNumber,MonthYear,commType);

			if (objDataSet == null)
			{
				lblMessage.Text = "Unable to get agency statement details.";
				return;
			}
			else
			{
				if (objDataSet.Tables[0].Rows.Count == 0)
				{
					lblMessage.Text =  "Unable to get agency statement details.";
					return;
				}
			}

			

			strBuilder.Append("<table cellSpacing='1' cellPadding='1' border='0' width='100%' align='center'>");
			
			strBuilder.Append("<tr height='20'>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='15%'><b>Customer Name</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='10%'><b>Policy Number</b></td>");
			//strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='10%'><b>LOB</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='10%'><b>Tran Type</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='10%'><b>Source Effective Date</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Right' width='10%'><b>Gross Premium</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Right' width='10%'><b>Fees</b></td>");			
			strBuilder.Append("<td class='headereffectWebGrid' align='Right' width='10%'><b>Net Premium</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Right' width='10%'><b>Commission Rate</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Right' width='10%'><b>Commission Amount</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Right' width='10%'><b>Due Amount</b></td>");

			strBuilder.Append("</tr>");

			
			//objDataSet=objAgencyStatement.PrintPriview(intJournalId);
			try
			{				
				if(objDataSet!=null)
				{
					foreach(DataRow dr in objDataSet.Tables[0].Rows)
					{
						lblAgencyName.Text = "Agency : " + dr["AGENCY_DISPLAY_NAME"].ToString();

						if (BillCode != "" && BillCode != dr["BILL_TYPE"].ToString())
						{

							strBuilder.Append("<tr><td class='midcolora' colspan='11'>&nbsp;</td></tr>");
							//Showing the bill code wise total
							strBuilder.Append("<tr>");
							strBuilder.Append("<td colspan='4' class='DataGridRow' align='Right'><span class='labelfont'>" + GetBillTypeDesc(BillCode) + " Premium</span></td>");
							strBuilder.Append("<td class='DataGridRow' colspan='3' align='Right'></td>");
							strBuilder.Append("<td colspan='4' class='DataGridRow' align='Left'><span class='labelfont'>"+ GrossTotalPremium.ToString("N") +"</span></td>");
							strBuilder.Append("</tr>");


							strBuilder.Append("<tr>");
							strBuilder.Append("<td colspan='4' class='DataGridRow' align='Right'><span class='labelfont'>" + GetBillTypeDesc(BillCode) + " Commission</span></td>");
							strBuilder.Append("<td class='DataGridRow' colspan='3' align='Right'></td>");
							strBuilder.Append("<td colspan='4' class='DataGridRow' align='Left'><span class='labelfont'>"+ GrossComm.ToString("N") +"</span></td>");
							strBuilder.Append("</tr>");

							strBuilder.Append("<tr>");
							strBuilder.Append("<td colspan='4' class='DataGridRow' align='Right'><span class='labelfont'>" + GetBillTypeDesc(BillCode) + " Amount Due</span></td>");
							strBuilder.Append("<td class='DataGridRow' colspan='3' align='Right'></td>");
							
							if (BillCode != "DB")
							{
								strBuilder.Append("<td colspan='4' class='DataGridRow' align='Left'><span class='labelfont'>"+ (GrossTotalPremium + GrossComm).ToString("N") +"</span></td>");
								NetBalance = NetBalance + GrossTotalPremium + GrossComm;
							}
							else
							{
								strBuilder.Append("<td colspan='4' class='DataGridRow' align='Left'><span class='labelfont'>"+ (GrossComm).ToString("N") +"</span></td>");
								NetBalance = NetBalance + GrossComm;
							}

							
							strBuilder.Append("</tr>");
							strBuilder.Append("<tr><td class='midcolora' colspan='11'>&nbsp;</td></tr>");

							
							
							NetTotalPremium+= GrossTotalPremium ;
							GrossTotalPremium = 0;

							GrossComm+=NetComm;
							GrossComm=0;
							
							
						}
						BillCode = dr["BILL_TYPE"].ToString();

						//dblTotalAmount =dblTotalAmount+double.Parse(dr["AMOUNT"].ToString());
						strBuilder.Append("<tr>");
						strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["CUSTOMER_NAME"]+"</td>");
						strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["POLICY_NUMBER"]+"</td>");
						//strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["LOB_CODE"]+"</td>");
						strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["TRAN_TYPE"]+"</td>");
						strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["SOURCE_EFF_DATE"]+"</td>");

						//Added By ravindra(07-24-2006)
						if (dr["GROSS_PREMIUM"].ToString() != "")
						{
							strBuilder.Append("<td class='DataGridRow' align='Right'>"+ Convert.ToDouble(dr["GROSS_PREMIUM"]).ToString("N") +"</td>");
							
						}
						else
						{
							strBuilder.Append("<td class='DataGridRow' align='Right'></td>");
						}
						if (dr["MCCA_FEES"].ToString() != "")
						{
							strBuilder.Append("<td class='DataGridRow' align='Right'>"+ Convert.ToDouble(dr["MCCA_FEES"]).ToString("N") +"</td>");
							
						}
						else
						{
							strBuilder.Append("<td class='DataGridRow' align='Right'></td>");
						}
						///////////////

						if (dr["AMOUNT_FOR_CALCULATION"].ToString() != "")
						{
							strBuilder.Append("<td class='DataGridRow' align='Right'>"+ Convert.ToDouble(dr["AMOUNT_FOR_CALCULATION"]).ToString("N") +"</td>");
							
						}
						else
						{
							strBuilder.Append("<td class='DataGridRow' align='Right'></td>");
						}

						if (dr["COMMISSION_RATE"].ToString() != "")
						{
							strBuilder.Append("<td class='DataGridRow' align='Right'>" + Convert.ToDouble(dr["COMMISSION_RATE"]).ToString("N") + "</td>");
						}
						else
						{
							strBuilder.Append("<td class='DataGridRow' align='Right'></td>");
						}

						if (dr["COMMISSION_AMOUNT"].ToString() != "")
						{
							strBuilder.Append("<td class='DataGridRow' align='Right'>" + Convert.ToDouble(dr["COMMISSION_AMOUNT"]).ToString("N") + "</td>");
							GrossComm += Convert.ToDouble(dr["COMMISSION_AMOUNT"]);

							if (Convert.ToDouble(dr["COMMISSION_AMOUNT"]) == 0 || BillCode == "DB" )
								GrossTotalPremium += Convert.ToDouble(dr["AMOUNT_FOR_CALCULATION"]);
								
						}
						else
						{
							strBuilder.Append("<td class='DataGridRow' align='Right'></td>");
							GrossTotalPremium += Convert.ToDouble(dr["AMOUNT_FOR_CALCULATION"]);
						}
						


						
						strBuilder.Append("<td class='DataGridRow' align='Right'>" + Convert.ToDouble(dr["DUE_AMOUNT"]).ToString("N")+"</td>");

						BillCodeWiseBalance += Convert.ToDouble(dr["AMOUNT_FOR_CALCULATION"]);
						
						

						strBuilder.Append("</tr>");
					
					}
					
					//Showing the gross total balance
					//Showing the bill code wise total
					strBuilder.Append("<tr><td class='midcolora' colspan='11'>&nbsp;</td></tr>");
					strBuilder.Append("<tr>");
					strBuilder.Append("<td colspan='4' class='DataGridRow' align='Right'><span class='labelfont'>" + GetBillTypeDesc(BillCode) + "Premium </span></td>");
					strBuilder.Append("<td class='DataGridRow' colspan='3' align='Right'></td> ");
					strBuilder.Append("<td colspan='4' class='DataGridRow' align='Left'><span class='labelfont'>"+ GrossTotalPremium.ToString("N") +"</span></td>");
					strBuilder.Append("</tr>");

					strBuilder.Append("<tr>");
					strBuilder.Append("<td colspan='4' class='DataGridRow' align='Right'><span class='labelfont'>" + GetBillTypeDesc(BillCode) + " Commission</span></td>");
					strBuilder.Append("<td class='DataGridRow' colspan='3' align='Right'></td>");
					strBuilder.Append("<td colspan='4' class='DataGridRow' align='Left'><span class='labelfont'>"+ GrossComm.ToString("N") +"</span></td>");
					strBuilder.Append("</tr>");

					strBuilder.Append("<tr>");
					strBuilder.Append("<td colspan='4' class='DataGridRow' align='Right'><span class='labelfont'>" + GetBillTypeDesc(BillCode) + " Amount Due</span></td>");
					strBuilder.Append("<td class='DataGridRow' colspan='3' align='Right'></td>");

					if (BillCode != "DB")
					{
						strBuilder.Append("<td colspan='6' class='DataGridRow' align='Left'><span class='labelfont'>"+ (GrossTotalPremium + GrossComm).ToString("N") +"</span></td>");
						NetBalance = NetBalance + GrossTotalPremium + GrossComm;
					}
					else
					{
						strBuilder.Append("<td colspan='6' class='DataGridRow' align='Left'><span class='labelfont'>"+ (GrossComm).ToString("N") +"</span></td>");
						NetBalance = NetBalance + GrossComm;
					}

					strBuilder.Append("</tr>");
					strBuilder.Append("<tr><td class='midcolora' colspan='11'>&nbsp;</td></tr>");


					//Showing the nett total balance
					strBuilder.Append("<tr>");
					strBuilder.Append("<td colspan='4' class='DataGridRow' align='Right'><span class='labelfont'>Net Amount</span></td>");
					strBuilder.Append("<td class='DataGridRow' colspan='4' align='Right'></td>");
					strBuilder.Append("<td colspan='4' class='DataGridRow' align='Left'><span class='labelfont'>"+ NetBalance.ToString("N") +"</span></td>");
					strBuilder.Append("</tr>");


				}
				else
				{
					//strBuilder.Append("<table>");
					strBuilder.Append("<tr>");
					strBuilder.Append("<td class='DataGridRow' align='Left' colspan='11'>No Record Found.</td>");
					strBuilder.Append("</tr>");
					tdAgencyStatementTD.InnerHtml = strBuilder.ToString();
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

			tdAgencyStatementTD.InnerHtml = strBuilder.ToString();
		}

		private string GetBillTypeDesc(string BillCode)
		{
			switch (BillCode.ToUpper())
			{
				case "AB":
					return "Agency Billed ";
				case "DB":
					return "Direct Billed ";
				default:
					return "";
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
