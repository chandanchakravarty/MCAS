/******************************************************************************************
	<Author					: - > Sumit Chhabra
	<Start Date				: -	> 06-04-2007
	<End Date				: - >
	<Description			: - > Report screen for Paid Loss Claims By Coverage
	<Review Date			: - >
	<Reviewed By			: - >
*******************************************************************************************/
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
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BLClaims;
using Cms.Claims;
using Cms.Model.Claims;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;
using Cms.CmsWeb.Controls; 

namespace Cms.Claims.Aspx.Reports
{
	/// <summary>
	/// Summary description for PaidClaimsByCoverageReport.
	/// </summary>
	public class PaidClaimsByCoverageReport : Cms.Claims.ClaimBase
	{
		protected System.Web.UI.WebControls.Label lblMessage;		
		protected Cms.CmsWeb.Controls.CmsButton btnExportToExcel;
		protected Cms.CmsWeb.Controls.CmsButton btnExportToFile;
		protected Cms.CmsWeb.Controls.CmsButton btnPrintReport;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPAGE_ACTION;
		//protected System.Web.UI.HtmlControls.HtmlInputHidden hidEXPORT_EXCEL;
		protected System.Web.UI.HtmlControls.HtmlTableRow trButtonRow;
		//protected System.Web.UI.WebControls.DataGrid dgPaidClaims;
		protected System.Web.UI.WebControls.Table tblPaidClaims; 
		protected System.Web.UI.WebControls.Panel pnlCoverageReport;
		public enum enumPAGE_ACTION
		{
			EXPORT_TO_EXCEL = 1,
			EXPORT_TO_FILE = 2,
			PRINT_FILE = 3
		}
		public string pathPlus	=	"/cms/cmsweb/Images/plus2.gif";
		public string pathMinus	=	"/cms/cmsweb/Images/minus2.gif";
		protected System.Web.UI.HtmlControls.HtmlForm ADJUSTER_SUMMARY;
		public System.Text.StringBuilder sbReportHandler = new System.Text.StringBuilder();
		
		
	
		private void Page_Load(object sender, System.EventArgs e)		
		{
			Page.EnableViewState = false;
			base.ScreenId="309_2";
			
			btnPrintReport.CmsButtonClass		=	CmsButtonType.Read;
			btnPrintReport.PermissionString		=	gstrSecurityXML;

			btnExportToExcel.CmsButtonClass		=	CmsButtonType.Read;
			btnExportToExcel.PermissionString		=	gstrSecurityXML;

			btnExportToFile.CmsButtonClass		=	CmsButtonType.Read;
			btnExportToFile.PermissionString		=	gstrSecurityXML;

			//btnExportToExcel.Attributes.Add("onClick","ExportToExcel();return false;");
			btnExportToFile.Attributes.Add("onClick","ExportToFile();return false;");
			btnPrintReport.Attributes.Add("onClick","return PrintReport();");
			//btnPrintReport.Attributes.Add("onclick","javascript:window.print();");
			GetQueryStringValues();
			if(hidPAGE_ACTION.Value==((int)enumPAGE_ACTION.EXPORT_TO_EXCEL).ToString().Trim())			
				ExportToExcel();
			else if(hidPAGE_ACTION.Value==((int)enumPAGE_ACTION.EXPORT_TO_FILE).ToString().Trim())
				ExportToFile();			
			else
			{
				int TableBorder=0;
				sbReportHandler = BindGrid(TableBorder);
				//if(sbReportHandler!=null)
				//	Response.Write(sbReportHandler.ToString().Trim());				
			}			
			
		}
		private void GetQueryStringValues()
		{
			if(Request.QueryString["CLAIM_ID"]!=null && Request.QueryString["CLAIM_ID"].ToString().Trim()!="")
				hidCLAIM_ID.Value = Request.QueryString["CLAIM_ID"].ToString().Trim();

			if(Request.QueryString["PAGE_ACTION"]!=null && Request.QueryString["PAGE_ACTION"].ToString().Trim()!="")
				hidPAGE_ACTION.Value = Request.QueryString["PAGE_ACTION"].ToString().Trim();

			//if(Request.QueryString["EXPORT_FILE"]!=null && Request.QueryString["EXPORT_FILE"].ToString().Trim()!="")
			//	hidEXPORT_FILE.Value = Request.QueryString["EXPORT_FILE"].ToString().Trim();

		}

//		private System.Text.StringBuilder BindGrids(int TableBorder)
//		{
//			try
//			{
//				string strCurrencySign = "$";
//				int ReturnValue = 1;
//				ClsClaimsNotification objClaimsNotification = new ClsClaimsNotification();
//				DataSet dsTemp = objClaimsNotification.GetPaidLoss(int.Parse(hidCLAIM_ID.Value),out ReturnValue);
//				if(ReturnValue==2)
//				{
//					lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("969");
//					lblMessage.Visible = true;
//					btnPrintReport.Visible = btnExportToExcel.Visible = false;		
//					trButtonRow.Attributes.Add("style","display:none");
//					return null;
//				}
//			
//				System.Text.StringBuilder sbReport = new System.Text.StringBuilder();
//			
//				//sbReport.Append("<div style='OVERFLOW: auto;HEIGHT: 300px'><table  border='" + TableBorder.ToString().Trim() + "'>");
//				sbReport.Append("<table id='tblCovReport'  border='" + TableBorder.ToString().Trim() + "'>");
//				sbReport.Append("<tr><td colspan='12' class='headereffectCenter'>Paid Loss Claims By Coverage Report</td></tr>");
//				sbReport.Append("<tr><td class='midcolora'><b>Customer Name</b></td><td class='midcolora' colspan=8>"+dsTemp.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString().Trim()+"</td>");
//				sbReport.Append("<td class='midcolora'><b>Agency</b></td><td class='midcolora'colspan=2>"+dsTemp.Tables[0].Rows[0]["AGENCY_NAME"].ToString().Trim()+"</td></tr>");
//				sbReport.Append("<tr><td class='midcolora'><b>Address:</b></td><td class='midcolora' colspan=8>"+dsTemp.Tables[0].Rows[0]["CUSTOMER_ADDRESS_LINE1"].ToString().Trim()+"</td>");
//				sbReport.Append("<td class='midcolora'><b>Adjuster</b></td><td class='midcolora' colspan=2 >"+dsTemp.Tables[0].Rows[0]["ADJUSTER_NAME"].ToString().Trim()+"</td></tr>");
//				sbReport.Append("<tr><td class='midcolora'>&nbsp;</td><td class='midcolora' colspan=8>"+dsTemp.Tables[0].Rows[0]["CUSTOMER_ADDRESS_LINE2"].ToString().Trim()+"</td>");
//				sbReport.Append("<td class='midcolora'><b>Run Date:</b></td><td class='midcolora'colspan=2>" + System.DateTime.Now.ToShortDateString() + "</td></tr>");
//				sbReport.Append("<tr><td class='midcolora'><b>Policy</b></td><td class='midcolora' colspan=8>" + dsTemp.Tables[0].Rows[0]["POLICY_DETAILS"].ToString().Trim()+"</td>");
//				sbReport.Append("<td class='midcolora'><b>Home #:</b></td><td class='midcolora'colspan=2>" + dsTemp.Tables[0].Rows[0]["CUSTOMER_HOME_PHONE"].ToString().Trim()+"</td>");
//				sbReport.Append("<tr><td class='midcolora'><b>Claim No.</b></td><td class='midcolora' colspan=8>" + dsTemp.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString().Trim()+"</td>");
//				sbReport.Append("<td class='midcolora'><b>Business #:</b></td><td class='midcolora'colspan=2>" + dsTemp.Tables[0].Rows[0]["CUSTOMER_BUSINESS_PHONE"].ToString().Trim()+"</td>");
//			
//				sbReport.Append("<tr><td colspan='15'></td></tr><tr><td colspan='15'></td></tr><tr><td colspan='15'></td></tr>");
//				sbReport.Append("<tr><td class='headereffectWebGrid'  >Date of Loss</td><td class='headereffectWebGrid'  >Description of Loss</td><td class='headereffectWebGrid'  >Entry Date</td><td class='headereffectWebGrid'  >Trans. Code</td><td class='headereffectWebGrid'  >Coverage</td><td class='headereffectWebGrid'  >Paid Loss</td><td class='headereffectWebGrid'  >Other   Expense</td><td class='headereffectWebGrid'  >Legal  Expense</td><td class='headereffectWebGrid'  >Adjustment  Expense</td><td class='headereffectWebGrid'  >Reinsurance Recovery</td><td class='headereffectWebGrid'  >Check #</td><td class='headereffectWebGrid'  >Date Cleared</td></tr>");
//			
//				bool isAddedOnce = false;
//				string cur_covg="";
//				string prev_covg="";
//				long SubPaidLossSum=0,SubLegalExpenseSum=0,SubAdjustmentExpenseSum=0,SubOtherExpenseSum=0;//,PaidLoss=0;				
//				long GrandPaidLossSum=0,GrandLegalExpenseSum=0,GrandAdjustmentExpenseSum=0,GrandOtherExpenseSum=0;//,PaidLoss=0;				
//				prev_covg = dsTemp.Tables[0].Rows[0]["COV_DESC"].ToString().Trim();
//				//PaidLoss = Convert.ToInt64(dsTemp.Tables[0].Rows[0]["PAID_LOSS"]);
//				for (int ctr=0;ctr<dsTemp.Tables[0].Rows.Count;ctr++)
//				{
//					if(!(Convert.ToInt64(dsTemp.Tables[0].Rows[ctr]["PAID_LOSS"])>0 || Convert.ToInt64(dsTemp.Tables[0].Rows[ctr]["OTHER_EXPENSE"])>0 || Convert.ToInt64(dsTemp.Tables[0].Rows[ctr]["LEGAL_EXPENSE"])>0 || Convert.ToInt64(dsTemp.Tables[0].Rows[ctr]["ADJUSTMENT_EXPENSE"])>0) || Convert.ToInt64(dsTemp.Tables[0].Rows[ctr]["REINSURANCE_RECOVERY"])>0)
//						continue;
//					cur_covg = dsTemp.Tables[0].Rows[ctr]["COV_DESC"].ToString().Trim();
//					//PaidLoss = 0;
//					if (cur_covg != prev_covg && SubPaidLossSum>0)
//					{
//						//PaidLoss = Convert.ToInt64(dsTemp.Tables[0].Rows[ctr]["PAID_LOSS"]);
//						prev_covg=cur_covg;
//						sbReport.Append("<tr> <td class='midcolorr' colspan=5><b>Sub Total</b></td><td class='midcolorr'><b>" + strCurrencySign +  SubPaidLossSum.ToString().Trim() + "</b></td><td class='midcolorr'><b>" + strCurrencySign +  SubOtherExpenseSum.ToString().Trim() + "</b></td><td class='midcolorr'><b>" + strCurrencySign +  SubLegalExpenseSum.ToString().Trim() + "</b></td><td class='midcolorr'><b>" + strCurrencySign +  SubAdjustmentExpenseSum.ToString().Trim() + "</b></td><td colspan='3' class='midcolorr'>&nbsp;</td> </tr>");
//						GrandPaidLossSum+= SubPaidLossSum;
//						GrandLegalExpenseSum+=SubLegalExpenseSum;
//						GrandAdjustmentExpenseSum+=SubAdjustmentExpenseSum;
//						GrandOtherExpenseSum+=SubOtherExpenseSum;
//						SubPaidLossSum = SubAdjustmentExpenseSum = SubLegalExpenseSum = SubOtherExpenseSum = 0;
//					}
//					
//					//Response.Write(dsTemp.Tables[0].Rows[ctr]["payment_amount"].ToString().Trim() + "<br>");
//
//					sbReport.Append("<tr>");
//					if(!isAddedOnce)
//					{
//						isAddedOnce = true;
//						sbReport.Append("<td class='midcolora'> " +  dsTemp.Tables[0].Rows[ctr]["LOSS_DATE"].ToString().Trim()  + "</td>");
//						sbReport.Append("<td class='midcolora'> " +  dsTemp.Tables[0].Rows[ctr]["CLAIM_DESCRIPTION"].ToString().Trim() + "</td>");
//					}
//					else
//					{
//						sbReport.Append("<td class='midcolora' colspan=2 >&nbsp;</td>");
//					}
//					sbReport.Append("<td class='midcolora'> " +  dsTemp.Tables[0].Rows[ctr]["ENTRY_DATE"].ToString().Trim()  + "</td>");
//					sbReport.Append("<td class='midcolora'> " +  dsTemp.Tables[0].Rows[ctr]["TRANSACTION_CODE"].ToString().Trim()  + "</td>");
//					sbReport.Append("<td class='midcolora'> " +  dsTemp.Tables[0].Rows[ctr]["COV_DESC"].ToString().Trim()  + "</td>");
//					//if (PaidLoss!=0)
//						sbReport.Append("<td class='midcolorr'> " + strCurrencySign +  String.Format("{0:,#,###}",Convert.ToInt64(dsTemp.Tables[0].Rows[ctr]["PAID_LOSS"])) + "</td>");
//					//else
//					//	sbReport.Append("<td class='midcolorr'>&nbsp;</td>");
//					SubPaidLossSum = Convert.ToInt64(dsTemp.Tables[0].Rows[ctr]["PAID_LOSS"]);
//					SubLegalExpenseSum+= Convert.ToInt64(dsTemp.Tables[0].Rows[ctr]["LEGAL_EXPENSE"]);
//					SubAdjustmentExpenseSum+= Convert.ToInt64(dsTemp.Tables[0].Rows[ctr]["ADJUSTMENT_EXPENSE"]);
//					SubOtherExpenseSum+= Convert.ToInt64(dsTemp.Tables[0].Rows[ctr]["OTHER_EXPENSE"]);
//					sbReport.Append("<td class='midcolorr'> " + strCurrencySign +   String.Format("{0:,#,###}",Convert.ToInt64(dsTemp.Tables[0].Rows[ctr]["OTHER_EXPENSE"])) + "</td>");
//					sbReport.Append("<td class='midcolorr'>" + strCurrencySign +   String.Format("{0:,#,###}",Convert.ToInt64(dsTemp.Tables[0].Rows[ctr]["LEGAL_EXPENSE"])) + "</td>");
//					sbReport.Append("<td class='midcolorr'>" + strCurrencySign +   String.Format("{0:,#,###}",Convert.ToInt64(dsTemp.Tables[0].Rows[ctr]["ADJUSTMENT_EXPENSE"])) + "</td>");
//					sbReport.Append("<td class='midcolorr'>" + strCurrencySign +   String.Format("{0:,#,###}",Convert.ToInt64(dsTemp.Tables[0].Rows[ctr]["REINSURANCE_RECOVERY"])) + "</td>");
//					sbReport.Append("<td class='midcolora'>" +  dsTemp.Tables[0].Rows[ctr]["CHECK_NUMBER"].ToString().Trim()  + "</td>");
//					sbReport.Append("<td class='midcolora'>" +  dsTemp.Tables[0].Rows[ctr]["CLEARING_DATE"].ToString().Trim()  + "</td>");
//					sbReport.Append("</tr>");
//				}
//				GrandPaidLossSum+= SubPaidLossSum;
//				GrandLegalExpenseSum+=SubLegalExpenseSum;
//				GrandAdjustmentExpenseSum+=SubAdjustmentExpenseSum;
//				GrandOtherExpenseSum+=SubOtherExpenseSum;						
//				sbReport.Append("<tr> <td class='midcolorr' colspan=5><b>Sub Total</b></td><td class='midcolorr'><b>" + strCurrencySign +  String.Format("{0:,#,###}",SubPaidLossSum) + "</b></td><td class='midcolorr'><b>" + strCurrencySign +  String.Format("{0:,#,###}",SubOtherExpenseSum) + "</b></td><td class='midcolorr'><b>" + strCurrencySign +  String.Format("{0:,#,###}",SubLegalExpenseSum) + "</b></td><td class='midcolorr'><b>" + strCurrencySign +  String.Format("{0:,#,###}",SubAdjustmentExpenseSum) + "</b></td><td colspan='3' class='midcolorr'>&nbsp;</td> </tr>");
//				sbReport.Append("<tr> <td class='midcolorr' colspan=12>&nbsp;</td> </tr>");
//				sbReport.Append("<tr> <td class='midcolorr' colspan=5><b>Grand Total</b></td><td class='midcolorr'><b>" + strCurrencySign +  String.Format("{0:,#,###}",GrandPaidLossSum) + "</b></td><td class='midcolorr'><b>" + strCurrencySign +  String.Format("{0:,#,###}",GrandOtherExpenseSum) + "</b></td><td class='midcolorr'><b>" + strCurrencySign +  String.Format("{0:,#,###}",GrandLegalExpenseSum) + "</b></td><td class='midcolorr'><b>" + strCurrencySign +  String.Format("{0:,#,###}",GrandAdjustmentExpenseSum) + "</b></td><td colspan='3' class='midcolorr'>&nbsp;</td> </tr>");
//				//sbReport.Append("</table></div>");
//				sbReport.Append("</table>");				
//				return sbReport;
//			}
//			catch(Exception ex)
//			{
//				lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("953");	
//				lblMessage.Visible = true;
//				btnPrintReport.Visible = btnExportToExcel.Visible = false;		
//				trButtonRow.Attributes.Add("style","display:none");
//				return null;
//			}		
//		}



		private System.Text.StringBuilder BindGrid(int TableBorder)
		{
			try
			{
				string strCurrencySign = "$";
				System.Text.StringBuilder sbPaymentAmount = new System.Text.StringBuilder();
				int ReturnValue = 1;
				ClsClaimsNotification objClaimsNotification = new ClsClaimsNotification();
				DataSet dsTemp = objClaimsNotification.GetPaidLoss(int.Parse(hidCLAIM_ID.Value),out ReturnValue);
				if(ReturnValue==2)
				{
					lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("969");
					lblMessage.Visible = true;
					btnPrintReport.Visible = btnExportToExcel.Visible = false;		
					trButtonRow.Attributes.Add("style","display:none");
					return null;
				}
						
				System.Text.StringBuilder sbReport = new System.Text.StringBuilder();
			
				//sbReport.Append("<div style='OVERFLOW: auto;HEIGHT: 300px'><table  border='" + TableBorder.ToString().Trim() + "'>");
				sbReport.Append("<table id='tblCovReport' width='100%' align='center' border='" + TableBorder.ToString().Trim() + "'>");				
				//Report Header Section Starts Here
				/*sbReport.Append("<tr><td colspan='8' class='headereffectCenter' >Paid Loss Claims By Coverage Report</td></tr>");
				sbReport.Append("<tr><td class='midcolora' colspan='2'><b>Customer Name</b></td><td class='midcolora' colspan='3'>"+dsTemp.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString().Trim()+"</td>");
				sbReport.Append("<td class='midcolora' colspan='1'><b>Agency</b></td><td class='midcolora' colspan='2'>"+dsTemp.Tables[0].Rows[0]["AGENCY_NAME"].ToString().Trim()+"</td></tr>");
				sbReport.Append("<tr><td class='midcolora' colspan='2'><b>Address:</b></td><td class='midcolora' colspan='3'>"+dsTemp.Tables[0].Rows[0]["CUSTOMER_ADDRESS_LINE1"].ToString().Trim()+"</td>");
				sbReport.Append("<td class='midcolora' colspan='1'><b>Adjuster</b></td><td class='midcolora' colspan='2'>"+dsTemp.Tables[0].Rows[0]["ADJUSTER_NAME"].ToString().Trim()+"</td></tr>");
				sbReport.Append("<tr><td class='midcolora' colspan='2'>&nbsp;</td><td class='midcolora' colspan='3'>"+dsTemp.Tables[0].Rows[0]["CUSTOMER_ADDRESS_LINE2"].ToString().Trim()+"</td>");
				sbReport.Append("<td class='midcolora' colspan='1'><b>Run Date:</b></td><td class='midcolora' colspan='2'>" + System.DateTime.Now.ToShortDateString() + "</td></tr>");
				sbReport.Append("<tr><td class='midcolora' colspan='2'><b>Policy No.</b></td><td class='midcolora' colspan='3'>" + dsTemp.Tables[0].Rows[0]["POLICY_DETAILS"].ToString().Trim()+"</td>");
				sbReport.Append("<td class='midcolora' colspan='1'><b>Home #:</b></td><td class='midcolora' colspan='2'>" + dsTemp.Tables[0].Rows[0]["CUSTOMER_HOME_PHONE"].ToString().Trim()+"</td></tr>");
				sbReport.Append("<tr><td class='midcolora' colspan='2'><b>Claim No.</b></td><td class='midcolora' colspan='3'>" + dsTemp.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString().Trim()+"</td>");
				sbReport.Append("<td class='midcolora' colspan='1'><b>Business #:</b></td><td class='midcolora' colspan='2'>" + dsTemp.Tables[0].Rows[0]["CUSTOMER_BUSINESS_PHONE"].ToString().Trim()+"</td></tr>");*/

				//New TAB :Modified on 05 Oct 2007
				string hStyle = "cursor:hand;";
				string jsFunction = "javascript:showHideCustInfo()";
				sbReport.Append("<tr><td colspan='4' class='headereffectCenter'  align='right' >Paid Loss Claims By Coverage Report&nbsp;&nbsp;&nbsp;<img id='img' src=" + pathPlus + " style=" + hStyle + " onclick=" + jsFunction + " ></td></tr>");
				sbReport.Append("<tr><td class='midcolora'><b>Claim No. #:</b></td><td class='midcolora'>"+dsTemp.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString().Trim()+"</td>");
				sbReport.Append("<td class='midcolora'><b>Customer</b></td><td class='midcolora'>"+dsTemp.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString().Trim()+"</td></tr>");
				sbReport.Append("<tr><td class='midcolora'><b>Policy No.</b></td><td class='midcolora' colspan='3'>"+dsTemp.Tables[0].Rows[0]["POLICY_DETAILS"].ToString().Trim()+"</td></tr>");
				sbReport.Append("<tr><td class='midcolora'><b>Agency</b></td><td class='midcolora'>"+dsTemp.Tables[0].Rows[0]["AGENCY_NAME"].ToString().Trim()+"</td>");
				sbReport.Append("<td class='midcolora'><b>Adjuster</b></td><td class='midcolora'>"+dsTemp.Tables[0].Rows[0]["ADJUSTER_NAME"].ToString().Trim()+"</td></tr>");
				sbReport.Append("<tr><td class='midcolora'><b>Run Date:</b></td><td class='midcolora' align='left'>" + System.DateTime.Now.ToShortDateString() + "</td>");
				sbReport.Append("<td class='midcolora'><b>Home #:</b></td><td class='midcolora'>"+dsTemp.Tables[0].Rows[0]["CUSTOMER_HOME_PHONE"].ToString().Trim()+"</td></tr>");
				sbReport.Append("<tr><td class='midcolora'><b>Business #:</b></td><td class='midcolora'>" + dsTemp.Tables[0].Rows[0]["CUSTOMER_BUSINESS_PHONE"].ToString().Trim()+"</td>");
				sbReport.Append("<td class='midcolora'></td><td class='midcolora'></td></tr>");
				//sbReport.Append("<tr><td colspan='8'>&nbsp;</td></tr>");			
				
				//Customer data				
				
				//sbReport.Append("<tr><td colspan='8' class='headereffectWebGrid'><img id='img' src=" + pathPlus + " style=" + hStyle + " onclick=" + jsFunction + " ></td></tr>");
				//sbReport.Append("<tr id='trCust1'><td class='midcolora'><b>Customer</b></td><td class='midcolora'>"+dsTemp.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString().Trim()+"</td>");
				sbReport.Append("<tr id='trCust1'><td class='midcolora'><b>Address</b></td><td class='midcolora'>"+dsTemp.Tables[0].Rows[0]["CUSTOMER_ADDRESS_LINE1"].ToString().Trim()+"</td>");
				sbReport.Append("<td class='midcolora'><b></b></td><td class='midcolora'></td></tr>");
				//sbReport.Append("<tr id='trCust2'><td class='midcolora'><b>Policy No.</b></td><td class='midcolora'>"+dsTemp.Tables[0].Rows[0]["POLICY_DETAILS"].ToString().Trim()+"</td>");
				sbReport.Append("<tr id='trCust2'><td class='midcolora'><b></b></td><td class='midcolora'>"+dsTemp.Tables[0].Rows[0]["CUSTOMER_ADDRESS_LINE2"].ToString().Trim()+"</td>");
				sbReport.Append("<td class='midcolora'>&nbsp;</td><td class='midcolora'></td></tr>");
				//sbReport.Append("<tr id='trCust3'><td class='midcolora'><b>Claim No. #:</b></td><td class='midcolora'>" + dsTemp.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString().Trim()+"</td>");
				//sbReport.Append("<td class='midcolora'></td><td class='midcolora'></td></tr>");
				sbReport.Append("<tr id='trCust4'><td colspan='4'>&nbsp;</td></tr>");
				//END Customer Data : 05 Oct 2007 (Expandable)
				//Coverage Report Section Starts Here

				//----------------------												 
				
				sbReport.Append("<tr><td class='midcolora' colspan='1'><b>Date of Loss</b></td><td class='midcolora' colspan='3' align='left'>"+dsTemp.Tables[1].Rows[0]["DATE_OF_LOSS"].ToString().Trim()+"</td></tr>");
				sbReport.Append("<tr><td class='midcolora' colspan='1'><b>Description of Loss</b></td><td class='midcolora' colspan='3'>"+dsTemp.Tables[1].Rows[0]["CLAIM_DESCRIPTION"].ToString().Trim()+"</td></tr><tr><td colspan='4'>");
				//Added by Asfa Praveen(10-Oct-2007) - iTrack issue 2699 (Coverages having no value should not be shown)
				sbReport.Append("<table width='100%'>");
				if(dsTemp.Tables[1].Rows[0]["PAYMENT_AMOUNT"].ToString().Trim() != "" && dsTemp.Tables[1].Rows[0]["PAYMENT_AMOUNT"].ToString().Trim() !="0")
					sbReport.Append("<tr><td colspan='7' class='headereffectWebGrid'><b>COVERAGE : "+dsTemp.Tables[1].Rows[0]["COVERAGE_DESC"].ToString().Trim().Replace("–","-")+"</b></td></tr>");
				//-------------------
				//sbReport.Append("<tr><td class='headereffectWebGrid'>Date of Loss</td><td class='headereffectWebGrid'>Description of Loss</td><td class='headereffectWebGrid'>Entry Date</td><td class='headereffectWebGrid'  >Trans. Code</td><td class='headereffectWebGrid'  >Coverage</td><td class='headereffectWebGrid'  >Paid Loss</td><td class='headereffectWebGrid'  >Other Expense</td><td class='headereffectWebGrid'  >Legal Expense</td><td class='headereffectWebGrid'  >Adjustment Expense</td><td class='headereffectWebGrid'  >Reinsurance Recovery</td><td class='headereffectWebGrid'  >Check #</td><td class='headereffectWebGrid'  >Date Cleared</td></tr>");
				
				/*Commented by Asfa (18-Apr-2008) - iTrack issue #4063
				sbReport.Append("<tr><td class='headereffectWebGrid' width = '10%'>Activity Date</td><td class='headereffectWebGrid' width = '40%' >Transaction Type</td><td class='headereffectWebGrid' width = '10%'>Paid</td><td class='headereffectWebGrid' width = '10%'>Recoveries</td><td class='headereffectWebGrid' width = '10%'>Expense</td><td class='headereffectWebGrid' width = '10%'>Loss Reinsurance Recovered</td><td class='headereffectWebGrid' width = '10%'>Adjustment Expense Reinsurance Recovered</td></tr>");
				*/
				sbReport.Append("<tr><td class='headereffectWebGrid' width = '10%'>Activity Date</td><td class='headereffectWebGrid' width = '40%' >Transaction Type</td><td class='headereffectWebGrid' width = '10%'>Paid</td><td class='headereffectWebGrid' width = '10%'>Recoveries</td><td class='headereffectWebGrid' width = '10%'>Expense</td><td class='headereffectWebGrid' width = '10%'>Loss Reinsurance Recovered</td><td class='headereffectWebGrid' width = '10%'>Expense Reinsurance Recovered</td></tr>");
				string strNewCovg,strCurCovg;
				//bool isAddedOnce=false;
				strCurCovg=strNewCovg="";
				strNewCovg = strCurCovg = dsTemp.Tables[1].Rows[0]["COV_ID"].ToString();
				int TranCode=0;
				double SubPaid,SubRecovery,SubExpense,SubLossReinsuranceRecovered,SubAdjustmentExpenseReinsuranceRecovered;
				double GrandPaid,GrandRecovery,GrandExpense,GrandLossReinsuranceRecovered,GrandAdjustmentExpenseReinsuranceRecovered;
				SubPaid=SubRecovery=SubExpense=SubLossReinsuranceRecovered=SubAdjustmentExpenseReinsuranceRecovered=GrandPaid=GrandRecovery=GrandExpense=GrandLossReinsuranceRecovered=GrandAdjustmentExpenseReinsuranceRecovered=0;
				for (int ctr=0;ctr<dsTemp.Tables[1].Rows.Count;ctr++)
				{
					strNewCovg = dsTemp.Tables[1].Rows[ctr]["COV_ID"].ToString();									
				
					//Done for Itrack Issue 7758 on 11 Aug 2010
					//if(dsTemp.Tables[1].Rows[ctr]["PAYMENT_AMOUNT"]!=null && Convert.ToInt64(dsTemp.Tables[1].Rows[ctr]["PAYMENT_AMOUNT"])< 1)
					if(dsTemp.Tables[1].Rows[ctr]["PAYMENT_AMOUNT"]!=null && Convert.ToInt64(dsTemp.Tables[1].Rows[ctr]["PAYMENT_AMOUNT"])< 0)
						continue;

					if(strNewCovg!=strCurCovg)					
					{
						strCurCovg = strNewCovg;
						GrandPaid+=SubPaid;
						GrandRecovery+=SubRecovery;
						GrandExpense+=SubExpense;
						GrandLossReinsuranceRecovered+=SubLossReinsuranceRecovered;
						GrandAdjustmentExpenseReinsuranceRecovered+=SubAdjustmentExpenseReinsuranceRecovered;
						sbReport.Append("<tr><td class='midcolorr'></td>");
						if(SubPaid != 0.0 || SubRecovery != 0.0 || SubExpense != 0.0 || SubLossReinsuranceRecovered != 0.0 || SubAdjustmentExpenseReinsuranceRecovered != 0.0)
							sbReport.Append("<td class='midcolorr'><b>Sub Total</b></td>");
						else
							sbReport.Append("<td class='midcolorr'><b></b></td>");

						if(SubPaid == 0.0)
							sbReport.Append("<td class='midcolorr'><b></b></td>");
						else
							sbReport.Append("<td class='midcolorr'><b>"   +  strCurrencySign + Double.Parse(SubPaid.ToString()).ToString("N")  + "</b></td>");

						if(SubRecovery == 0.0)
							sbReport.Append("<td class='midcolorr'><b></b></td>");
						else
							sbReport.Append("<td class='midcolorr'><b>"   +  strCurrencySign + Double.Parse(SubRecovery.ToString()).ToString("N")  + "</b></td>");
					
						if(SubExpense == 0.0)
							sbReport.Append("<td class='midcolorr'><b></b></td>");
						else
							sbReport.Append("<td class='midcolorr'><b>"   +  strCurrencySign + Double.Parse(SubExpense.ToString()).ToString("N")  + "</b></td>");
						
					
						if(SubLossReinsuranceRecovered == 0.0)
							sbReport.Append("<td class='midcolorr'><b></b></td>");
						else
							sbReport.Append("<td class='midcolorr'><b>"   +  strCurrencySign + Double.Parse(SubLossReinsuranceRecovered.ToString()).ToString("N")  + "</b></td>");
					
						if(SubAdjustmentExpenseReinsuranceRecovered == 0.0)
							sbReport.Append("<td class='midcolorr'><b></b></td></tr>");
						else
							sbReport.Append("<td class='midcolorr'><b>"   +  strCurrencySign + Double.Parse(SubAdjustmentExpenseReinsuranceRecovered.ToString()).ToString("N")  + "</b></td></tr>");
						sbReport.Append("<tr><td class='midcolorr'>&nbsp;</td></tr>");
						sbReport.Append("<tr><td colspan='7' class='headereffectWebGrid'><b>COVERAGE : "+ dsTemp.Tables[1].Rows[ctr]["COVERAGE_DESC"].ToString().Trim().Replace("-","-")+"</b></td></tr>");
					
						//						sbReport.Append("<td class='midcolorr'><b>"   +  String.Format("{0:C}",Convert.ToInt64(SubOtherExpense))  + "</b></td>");
						//						sbReport.Append("<td class='midcolorr'><b>"   +  String.Format("{0:C}",Convert.ToInt64(SubLegalExpense))  + "</b></td>");
						//						sbReport.Append("<td class='midcolorr'><b>"   +  String.Format("{0:C}",Convert.ToInt64(SubAdjustmentExpense))  + "</b></td>");
						//sbReport.Append("<td class='midcolorr' colspan='2'></td></tr>");
						SubPaid=SubRecovery=SubExpense=SubLossReinsuranceRecovered=SubAdjustmentExpenseReinsuranceRecovered=0;
					}	
				
					sbReport.Append("<tr>");

					//					if(!isAddedOnce)					
					//					{
					//						sbReport.Append("<td class='midcolora'>" + dsTemp.Tables[1].Rows[ctr]["DATE_OF_LOSS"].ToString() +"</td>");
					//						sbReport.Append("<td class='midcolora'>" + dsTemp.Tables[1].Rows[ctr]["CLAIM_DESCRIPTION"].ToString() +"</td>");
					//						isAddedOnce=true;
					//					}
					//					else
					//						sbReport.Append("<td class='midcolorr' colspan='2'></td>");		

					sbReport.Append("<td class='midcolora' align='left'>" + dsTemp.Tables[1].Rows[ctr]["ENTRY_DATE"].ToString() + "</td>");
					sbReport.Append("<td class='midcolora'>" + dsTemp.Tables[1].Rows[ctr]["TRANS_DESCRIPTION"].ToString() + "</td>");
					//sbReport.Append("<td class='midcolora'>" +   dsTemp.Tables[1].Rows[ctr]["COVERAGE_DESC"] + "</td>");					
					TranCode = int.Parse(dsTemp.Tables[1].Rows[ctr]["ACTION_ON_PAYMENT"].ToString());

					sbPaymentAmount.Length = 0;
					sbPaymentAmount.Append(Double.Parse(dsTemp.Tables[1].Rows[ctr]["PAYMENT_AMOUNT"].ToString()).ToString("N"));
					switch(TranCode)
					{
						case 180://Paid Loss, Partial
						case 181://Paid Loss, Final
						//Added for Itrack Issue 7758 on 5 Aug 2010
						case 265://Void - Credit to Paid loss
							//sbReport.Append("<td class='midcolorr'> " +  String.Format("{0:C}",Convert.ToInt64(dsTemp.Tables[1].Rows[ctr]["PAYMENT_AMOUNT"])) + "</td><td class='midcolorr'>" + strCurrencySign + "</td><td class='midcolorr'>" + strCurrencySign + "</td><td class='midcolorr'>" + strCurrencySign + "</td>");
							//sbReport.Append("<td colspan='1' class='midcolorr'>" +  strCurrencySign + sbPaymentAmount.ToString() + "</td><td colspan='1' class='midcolorr'>" + strCurrencySign + "</td><td colspan='1' class='midcolorr'>" + strCurrencySign + "</td><td colspan='1' class='midcolorr'>" + strCurrencySign + "</td><td colspan='1' class='midcolorr'>" + strCurrencySign + "</td>");
							sbReport.Append("<td class='midcolorr'>" +  strCurrencySign + sbPaymentAmount.ToString() + "</td><td class='midcolorr'></td><td class='midcolorr'></td><td class='midcolorr'></td><td class='midcolorr'></td>");
							SubPaid+=Double.Parse(dsTemp.Tables[1].Rows[ctr]["PAYMENT_AMOUNT"].ToString());
							break;
						case 188://Credit to Paid loss
							sbReport.Append("<td class='midcolorr'>" +  "-" +  strCurrencySign + sbPaymentAmount.ToString() + "</td><td class='midcolorr'></td><td class='midcolorr'></td><td class='midcolorr'></td><td class='midcolorr'></td>");
							SubPaid-=Double.Parse(dsTemp.Tables[1].Rows[ctr]["PAYMENT_AMOUNT"].ToString());
							break;
						case 240://Void - Credit to Paid Loss
							sbReport.Append("<td class='midcolorr'>" +  strCurrencySign +  "-" + sbPaymentAmount.ToString() + "</td><td class='midcolorr'></td><td class='midcolorr'></td><td class='midcolorr'></td><td class='midcolorr'></td>");
							SubPaid-=Double.Parse(dsTemp.Tables[1].Rows[ctr]["PAYMENT_AMOUNT"].ToString());
							break;
						case 189://Salvage (Check Received)
						case 190://Subrogation (Check Received)
							//sbReport.Append("<td colspan='1' class='midcolorr'>" + strCurrencySign + "</td><td colspan='1' class='midcolorr'>" +  strCurrencySign +  "-" + sbPaymentAmount.ToString() + "</td><td colspan='1' class='midcolorr'>" + strCurrencySign + "</td><td colspan='1' class='midcolorr'>" + strCurrencySign + "</td><td colspan='1' class='midcolorr'>" + strCurrencySign + "</td>");
							sbReport.Append("<td class='midcolorr'></td><td class='midcolorr'>" +  strCurrencySign +  "-" + sbPaymentAmount.ToString() + "</td><td class='midcolorr'></td><td class='midcolorr'></td><td class='midcolorr'></td>");
							SubRecovery-=Double.Parse(dsTemp.Tables[1].Rows[ctr]["PAYMENT_AMOUNT"].ToString());								
							break;
						case 177://Subrogation Expense
						case 176://Salvage Expense
						//Added for Itrack Issue 7758 on 5 Aug 2010
						case 266://Void - Salvage (Check Received)
						case 267://Void - Subrogation (Check Received)
						case 272://Void - Credit to Subrogation Expense
						case 273://Void - Credit to Salvage Expense
							//sbReport.Append("<td colspan='1' class='midcolorr'>" + strCurrencySign + "</td><td colspan='1' class='midcolorr'>" +  strCurrencySign + sbPaymentAmount.ToString() + "</td><td colspan='1' class='midcolorr'>" + strCurrencySign + "</td><td colspan='1' class='midcolorr'>" + strCurrencySign + "</td><td colspan='1' class='midcolorr'>" + strCurrencySign + "</td>");
							sbReport.Append("<td class='midcolorr'></td><td class='midcolorr'>" +  strCurrencySign + sbPaymentAmount.ToString() + "</td><td class='midcolorr'></td><td class='midcolorr'></td><td class='midcolorr'></td>");
							SubRecovery+=Double.Parse(dsTemp.Tables[1].Rows[ctr]["PAYMENT_AMOUNT"].ToString());								
							break;
						case 242://Credit to Subrogation Expense
						case 243://Void - Credit to Subrogation Expense
						case 244://Credit to Salvage Expense
						case 245://Void - Credit to Salvage Expense
							//sbReport.Append("<td colspan='1' class='midcolorr'>" + strCurrencySign + "</td><td colspan='1' class='midcolorr'>" +  strCurrencySign + sbPaymentAmount.ToString() + "</td><td colspan='1' class='midcolorr'>" + strCurrencySign + "</td><td colspan='1' class='midcolorr'>" + strCurrencySign + "</td><td colspan='1' class='midcolorr'>" + strCurrencySign + "</td>");
							sbReport.Append("<td class='midcolorr'></td><td class='midcolorr'>" +  strCurrencySign +  "-"  + sbPaymentAmount.ToString() + "</td><td class='midcolorr'></td><td class='midcolorr'></td><td class='midcolorr'></td>");
							SubRecovery-=Double.Parse(dsTemp.Tables[1].Rows[ctr]["PAYMENT_AMOUNT"].ToString());								
							break;
						case 175://Adjustment Expense
						case 173://Other Expense
						case 174://Legal Expense
						//Added for Itrack Issue 7758 on 5 Aug 2010
						case 268://Void - Credit to Legal Expense
						case 269://Void - Credit to Other expense
						case 270://Void - Credit to Adjuster Expense
							//sbReport.Append("<td colspan='1' class='midcolorr'>" + strCurrencySign + "</td><td colspan='1' class='midcolorr'>" + strCurrencySign + "</td><td colspan='1' class='midcolorr'>" + strCurrencySign + sbPaymentAmount.ToString() + "</td><td colspan='1' class='midcolorr'>" +  strCurrencySign + "</td><td colspan='1' class='midcolorr'>" +  strCurrencySign +"</td>");
							sbReport.Append("<td class='midcolorr'></td><td class='midcolorr'></td><td class='midcolorr'>" + strCurrencySign + sbPaymentAmount.ToString() + "</td><td class='midcolorr'></td><td class='midcolorr'></td>");
							SubExpense+=Double.Parse(dsTemp.Tables[1].Rows[ctr]["PAYMENT_AMOUNT"].ToString());								
							break;
						case 207://Credit to Adjuster Expense
						case 203://Credit to legal Expense
						case 204://Credit to other expense
						case 241://Void - Credit To Expense
						case 254://Void - Adjuster Expense
						case 255://Void - Legal Expense
							sbReport.Append("<td class='midcolorr'></td><td class='midcolorr'></td><td class='midcolorr'>" + strCurrencySign + "-" +sbPaymentAmount.ToString() + "</td><td class='midcolorr'></td><td class='midcolorr'></td>");
							SubExpense-=Double.Parse(dsTemp.Tables[1].Rows[ctr]["PAYMENT_AMOUNT"].ToString());								
							break;
						case 178://Salvage Reinsurance Returned
						case 179://Subrogation Reinsurance Returned
						//Added for Itrack Issue 7758 on 5 Aug 2010
						case 278://Void - Salvage Expense Reinsurance Recovered
						case 279://Void - Subrogation Expense Reinsurance Recovered
						case 274://Void - Loss Reinsurance Recovered - Paid Loss
							//sbReport.Append("<td colspan='1' class='midcolorr'>" + strCurrencySign + "</td><td colspan='1' class='midcolorr'>" +  strCurrencySign + "</td><td colspan='1' class='midcolorr'>" +  strCurrencySign + "</td><td colspan='1' class='midcolorr'>" +  strCurrencySign + "-" + sbPaymentAmount.ToString() + "</td><td colspan='1' class='midcolorr'>" + strCurrencySign + "</td>");
							sbReport.Append("<td class='midcolorr'></td><td class='midcolorr'></td><td class='midcolorr'></td><td class='midcolorr'>" +  strCurrencySign + "-" + sbPaymentAmount.ToString() + "</td><td class='midcolorr'></td>");
							SubLossReinsuranceRecovered-=Double.Parse(dsTemp.Tables[1].Rows[ctr]["PAYMENT_AMOUNT"].ToString());								
							break;
						case 182://Loss Reinsurance Recovered - Paid Loss
						case 186:// Salvage Expense Reinsurance Recovered
						case 187:// Subrogation Expense Reinsurance Recovered
							//sbReport.Append("<td colspan='1' class='midcolorr'>" + strCurrencySign + "</td><td colspan='1' class='midcolorr'>" +  strCurrencySign + "</td><td colspan='1' class='midcolorr'>" +  strCurrencySign + "</td><td colspan='1' class='midcolorr'>" +  strCurrencySign + sbPaymentAmount.ToString() + "</td><td colspan='1' class='midcolorr'>" + strCurrencySign + "</td>");
							sbReport.Append("<td class='midcolorr'></td><td class='midcolorr'></td><td class='midcolorr'></td><td class='midcolorr'>" +  strCurrencySign + sbPaymentAmount.ToString() + "</td><td class='midcolorr'></td>");
							SubLossReinsuranceRecovered+=Double.Parse(dsTemp.Tables[1].Rows[ctr]["PAYMENT_AMOUNT"].ToString());								
							break;
						case 183://Other Expense Reinsurance Recovered
						case 184://Legal Expense Reinsurance Recovered
						case 185://Adjustment Expense Reinsurance Recovered
							//sbReport.Append("<td colspan='1' class='midcolorr'>" + strCurrencySign + "</td><td colspan='1' class='midcolorr'>" + strCurrencySign + "</td><td colspan='1' class='midcolorr'>" + strCurrencySign + "</td><td colspan='1' class='midcolorr'>" + strCurrencySign + "</td><td colspan='1' class='midcolorr'>" +  strCurrencySign + sbPaymentAmount.ToString() + "</td>");
							sbReport.Append("<td class='midcolorr'></td><td class='midcolorr'></td><td class='midcolorr'></td><td class='midcolorr'></td><td class='midcolorr'>" +  strCurrencySign + sbPaymentAmount.ToString() + "</td>");
							SubAdjustmentExpenseReinsuranceRecovered+=Double.Parse(dsTemp.Tables[1].Rows[ctr]["PAYMENT_AMOUNT"].ToString());								
							break;

						// Modified ITRack 4815 25-Sep-08 Mohit Agarwal
						case 246://Reinsurance Returned - Adjuster Expense
						case 247://Reinsurance Returned - Legal Expense
						case 248://Reinsurance Returned - Other Expense
						//Added for Itrack Issue 7758 on 5 Aug 2010
						case 275://Void - Other Expense Reinsurance Recovered
						case 276://Void - Legal Expense Reinsurance Recovered
						case 277://Void - Adjuster Expense Reinsurance Recovered
							//sbReport.Append("<td colspan='1' class='midcolorr'>" + strCurrencySign + "</td><td colspan='1' class='midcolorr'>" + strCurrencySign + "</td><td colspan='1' class='midcolorr'>" + strCurrencySign + "</td><td colspan='1' class='midcolorr'>" + strCurrencySign + "</td><td colspan='1' class='midcolorr'>" +  strCurrencySign + sbPaymentAmount.ToString() + "</td>");
							sbReport.Append("<td class='midcolorr'></td><td class='midcolorr'></td><td class='midcolorr'></td><td class='midcolorr'></td><td class='midcolorr'>" + "-" + strCurrencySign + sbPaymentAmount.ToString() + "</td>");
							SubAdjustmentExpenseReinsuranceRecovered-=Double.Parse(dsTemp.Tables[1].Rows[ctr]["PAYMENT_AMOUNT"].ToString());								
							break;

				//Added by Mohit Agarwal 22-Aug-08 Track 4664
						case 252:// Void to Reinsurance Returned Paid Loss
						case 253:// Void to Reinsurance Returned Salvage/Sub Expense
							//sbReport.Append("<td colspan='1' class='midcolorr'>" + strCurrencySign + "</td><td colspan='1' class='midcolorr'>" +  strCurrencySign + "</td><td colspan='1' class='midcolorr'>" +  strCurrencySign + "</td><td colspan='1' class='midcolorr'>" +  strCurrencySign + sbPaymentAmount.ToString() + "</td><td colspan='1' class='midcolorr'>" + strCurrencySign + "</td>");
							sbReport.Append("<td class='midcolorr'></td><td class='midcolorr'></td><td class='midcolorr'></td><td class='midcolorr'>" +  strCurrencySign + sbPaymentAmount.ToString() + "</td><td class='midcolorr'></td>");
							SubLossReinsuranceRecovered+=Double.Parse(dsTemp.Tables[1].Rows[ctr]["PAYMENT_AMOUNT"].ToString());								
							break;
						case 249://Void to Reinsurance Returned - Adjuster Expense
						case 250://Void to Reinsurance Returned - Other Expense
						case 251://Void to Reinsurance Returned - Legal Expense
							//sbReport.Append("<td colspan='1' class='midcolorr'>" + strCurrencySign + "</td><td colspan='1' class='midcolorr'>" + strCurrencySign + "</td><td colspan='1' class='midcolorr'>" + strCurrencySign + "</td><td colspan='1' class='midcolorr'>" + strCurrencySign + "</td><td colspan='1' class='midcolorr'>" +  strCurrencySign + sbPaymentAmount.ToString() + "</td>");
							sbReport.Append("<td class='midcolorr'></td><td class='midcolorr'></td><td class='midcolorr'></td><td class='midcolorr'></td><td class='midcolorr'>" +  strCurrencySign + sbPaymentAmount.ToString() + "</td>");
							SubAdjustmentExpenseReinsuranceRecovered+=Double.Parse(dsTemp.Tables[1].Rows[ctr]["PAYMENT_AMOUNT"].ToString());								
							break;
						default:
							sbReport.Append("<td class='midcolorr'></td>");
							break;
					}						
					//sbReport.Append("<td class='midcolorr'>" + strCurrencySign + "</td><td class='midcolorr'>" + dsTemp.Tables[1].Rows[ctr]["CHECK_NUMBER"].ToString() + "</td><td class='midcolorr'>"+  dsTemp.Tables[1].Rows[ctr]["CHECK_DATE"].ToString() + "</td>");
					sbReport.Append("</tr>");
				}	
				GrandPaid+=SubPaid;
				GrandRecovery+=SubRecovery;
				GrandExpense+=SubExpense;
				GrandLossReinsuranceRecovered+=SubLossReinsuranceRecovered;
				GrandAdjustmentExpenseReinsuranceRecovered+=SubAdjustmentExpenseReinsuranceRecovered;
				sbReport.Append("<tr>"); 
				sbReport.Append("<td class='midcolorr'></td>");
				sbReport.Append("<td class='midcolorr'><b>Sub Total</b></td>");


				sbPaymentAmount.Length = 0;
				sbPaymentAmount.Append(Double.Parse(SubPaid.ToString()).ToString("N"));
				if(SubPaid == 0.0)
					sbReport.Append("<td  class='midcolorr'><b></b></td>");
				else
					sbReport.Append("<td  class='midcolorr'><b>"   +  strCurrencySign + sbPaymentAmount.ToString() + "</b></td>");

				sbPaymentAmount.Length = 0;
				sbPaymentAmount.Append(Double.Parse(SubRecovery.ToString()).ToString("N"));
				if(SubRecovery == 0.0)
					sbReport.Append("<td class='midcolorr'><b></b></td>");
				else
					sbReport.Append("<td class='midcolorr'><b>"   +  strCurrencySign + sbPaymentAmount.ToString() + "</b></td>");

				sbPaymentAmount.Length = 0;
				sbPaymentAmount.Append(Double.Parse(SubExpense.ToString()).ToString("N"));
				if(SubExpense == 0.0)
					sbReport.Append("<td class='midcolorr'><b></b></td>");
				else
					sbReport.Append("<td class='midcolorr'><b>"   +  strCurrencySign + sbPaymentAmount.ToString() + "</b></td>");

				sbPaymentAmount.Length = 0;
				sbPaymentAmount.Append(Double.Parse(SubLossReinsuranceRecovered.ToString()).ToString("N"));
				if(SubLossReinsuranceRecovered == 0.0)
					sbReport.Append("<td class='midcolorr'><b></b></td>");						
				else
					sbReport.Append("<td class='midcolorr'><b>"   +  strCurrencySign + sbPaymentAmount.ToString() + "</b></td>");						

				sbPaymentAmount.Length = 0;
				sbPaymentAmount.Append(Double.Parse(SubAdjustmentExpenseReinsuranceRecovered.ToString()).ToString("N"));
				if(SubAdjustmentExpenseReinsuranceRecovered == 0.0)
					sbReport.Append("<td class='midcolorr'><b></b></td>");						
				else
					sbReport.Append("<td class='midcolorr'><b>"   +  strCurrencySign + sbPaymentAmount.ToString() + "</b></td>");						


			
				sbReport.Append("</tr><tr><td class='midcolorr' colspan='7'>&nbsp;</td></tr>");
				sbReport.Append("<tr><td class='midcolorr'></td>");
				sbReport.Append("<td class='midcolorr'><b>Grand Total</b></td>");

				sbPaymentAmount.Length = 0;
				sbPaymentAmount.Append(Double.Parse(GrandPaid.ToString()).ToString("N"));
				if(GrandPaid == 0.0)
					sbReport.Append("<td class='midcolorr'><b></b></td>");
				else
					sbReport.Append("<td class='midcolorr'><b>"   +  strCurrencySign + sbPaymentAmount.ToString() + "</b></td>");

				sbPaymentAmount.Length = 0;
				sbPaymentAmount.Append(Double.Parse(GrandRecovery.ToString()).ToString("N"));
				if(GrandRecovery == 0.0)
					sbReport.Append("<td class='midcolorr'><b></b></td>");
				else
					sbReport.Append("<td class='midcolorr'><b>"   +  strCurrencySign + sbPaymentAmount.ToString() + "</b></td>");


				sbPaymentAmount.Length = 0;
				sbPaymentAmount.Append(Double.Parse(GrandExpense.ToString()).ToString("N"));
				if(GrandExpense == 0.0)
					sbReport.Append("<td class='midcolorr'><b></b></td>");
				else
					sbReport.Append("<td class='midcolorr'><b>"   +  strCurrencySign + sbPaymentAmount.ToString() + "</b></td>");

				sbPaymentAmount.Length = 0;
				sbPaymentAmount.Append(Double.Parse(GrandLossReinsuranceRecovered.ToString()).ToString("N"));
				if(GrandLossReinsuranceRecovered == 0.0)
					sbReport.Append("<td class='midcolorr'><b></b></td>");
				else
					sbReport.Append("<td class='midcolorr'><b>"   +  strCurrencySign + sbPaymentAmount.ToString() + "</b></td>");

				sbPaymentAmount.Length = 0;
				sbPaymentAmount.Append(Double.Parse(GrandAdjustmentExpenseReinsuranceRecovered.ToString()).ToString("N"));
				if(GrandAdjustmentExpenseReinsuranceRecovered == 0.0)
					sbReport.Append("<td class='midcolorr'><b></b></td>");
				else
					sbReport.Append("<td class='midcolorr'><b>"   +  strCurrencySign + sbPaymentAmount.ToString() + "</b></td>");

				//sbReport.Append("<td class='midcolorr' colspan='2'></td>");
				sbReport.Append("</tr></table></td></tr>");
				sbReport.Append("</table>");				
				return sbReport;
			}
			catch (Exception ex)
			{
				lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("953");
				lblMessage.Visible = true;
				btnPrintReport.Visible = btnExportToExcel.Visible = false;		
				trButtonRow.Attributes.Add("style","display:none");
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				return null;
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
			this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void ExportToExcel()
		{
				
			//Set the content type to Excel.
			/*Response.ContentType = "application/vnd.ms-excel";
			//Remove the charset from the Content-Type header.
			Response.Charset = "";		
	
			//Turn off the view state.
			this.EnableViewState = false;

			System.Text.StringBuilder sbReportHandler = new System.Text.StringBuilder();
			int TableBorder =1;
			sbReportHandler = BindGrid(TableBorder);
			string strScript="javascript: ExportToExcel";
			RegisterStartupScript("StartUpScript",strScript);
			if(sbReportHandler!=null)
				Response.Write(sbReportHandler.ToString().Trim());
			Response.End();*/

			Response.Clear();
			Response.AddHeader("content-disposition", "attachment;filename=FileName.xls");
			Response.Charset = "";
			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			Response.ContentType = "application/vnd.xls";

			System.Text.StringBuilder stringWrite = new System.Text.StringBuilder();
			//System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

			int TableBorder =1;
			stringWrite = BindGrid(TableBorder);
			Response.Write(stringWrite.ToString());
			Response.End();
		}

//		private void ExportToFile()
//		{
//			
//
//			System.Text.StringBuilder sbReportHandler = new System.Text.StringBuilder();
//			int TableBorder =1;
//			sbReportHandler = BindGrid(TableBorder);
//			sbReportHandler.Replace("<table>"," ");
//			sbReportHandler.Replace("<tr>","\n");
//			sbReportHandler.Replace("<td>"," , ");
//			sbReportHandler.Replace("</td>","");
//			sbReportHandler.Replace("</tr>","");
//			sbReportHandler.Replace("</table>","");			
//			string strScript="javascript: ExportToExcel";
//			RegisterStartupScript("StartUpScript",strScript);
//			Response.Write(sbReportHandler.ToString().Trim());
//			Response.End();
//		}


		private void ExportToFile()
		{
			//Set the content type to Excel.
			Response.ContentType = "application/vnd.text";
			//Remove the charset from the Content-Type header.
			Response.Charset = "";			
			//Turn off the view state.
			this.EnableViewState = false;
			//			try
			//			{
			int ReturnValue = 1;
			ClsClaimsNotification objClaimsNotification = new ClsClaimsNotification();
			DataSet dsTemp = objClaimsNotification.GetPaidLoss(int.Parse(hidCLAIM_ID.Value),out ReturnValue);
			if(ReturnValue==2)
			{
				lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("969");
				lblMessage.Visible = true;
				btnPrintReport.Visible = btnExportToExcel.Visible = false;		
				trButtonRow.Attributes.Add("style","display:none");
				return;
			}
			
			System.Text.StringBuilder sbReport = new System.Text.StringBuilder();
			
			
			sbReport.Append("Paid Loss Claims By Coverage Report<br>");
			sbReport.Append(" Customer Name"+dsTemp.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString().Trim()+" ");
			sbReport.Append("Agency       "+dsTemp.Tables[0].Rows[0]["AGENCY_NAME"].ToString().Trim()+"");
			sbReport.Append(" Address:"+dsTemp.Tables[0].Rows[0]["CUSTOMER_ADDRESS_LINE1"].ToString().Trim()+" ");
			sbReport.Append("Adjuster       "+dsTemp.Tables[0].Rows[0]["ADJUSTER_NAME"].ToString().Trim()+"");
			sbReport.Append(" "+dsTemp.Tables[0].Rows[0]["CUSTOMER_ADDRESS_LINE2"].ToString().Trim()+" ");
			sbReport.Append("Run Date:       " + System.DateTime.Now.ToShortDateString() + "");
			sbReport.Append(" Policy" + dsTemp.Tables[0].Rows[0]["POLICY_DETAILS"].ToString().Trim()+" ");
			sbReport.Append("Home #:       " + dsTemp.Tables[0].Rows[0]["CUSTOMER_HOME_PHONE"].ToString().Trim()+" ");
			sbReport.Append(" Claim No." + dsTemp.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString().Trim()+" ");
			sbReport.Append("Business #:       " + dsTemp.Tables[0].Rows[0]["CUSTOMER_BUSINESS_PHONE"].ToString().Trim()+" ");
			
			sbReport.Append("   ");
			sbReport.Append("Date of Loss       Description of Loss       Entry Date       Trans. Code       Coverage        Paid Loss       Other   Expense        Legal  Expense        Adjustment  Expense        Reinsurance Recovery       Check #         Date Cleared");
			

			string cur_covg="";
			string prev_covg="";
			long SubPaidLossSum=0,SubLegalExpenseSum=0,SubAdjustmentExpenseSum=0,SubOtherExpenseSum=0;//,PaidLoss=0;
			long GrandPaidLossSum=0,GrandLegalExpenseSum=0,GrandAdjustmentExpenseSum=0,GrandOtherExpenseSum=0;//,PaidLoss=0;				
			prev_covg = dsTemp.Tables[0].Rows[0]["COV_DESC"].ToString().Trim();
			//PaidLoss = Convert.ToInt64(dsTemp.Tables[0].Rows[0]["PAID_LOSS"]);
			for (int ctr=0;ctr< dsTemp.Tables[0].Rows.Count;ctr++)
			{
				cur_covg = dsTemp.Tables[0].Rows[ctr]["COV_DESC"].ToString().Trim();
				//PaidLoss = 0;
				if (cur_covg != prev_covg)
				{
					//PaidLoss = Convert.ToInt64(dsTemp.Tables[0].Rows[ctr]["PAID_LOSS"]);
					prev_covg=cur_covg;
					sbReport.Append(" " + SubPaidLossSum.ToString().Trim() + "" + SubOtherExpenseSum.ToString().Trim() + "" + SubLegalExpenseSum.ToString().Trim() + "" + SubAdjustmentExpenseSum.ToString().Trim() + "    colspan='3'     ");
					GrandPaidLossSum+= SubPaidLossSum;
					GrandLegalExpenseSum+=SubLegalExpenseSum;
					GrandAdjustmentExpenseSum+=SubAdjustmentExpenseSum;
					GrandOtherExpenseSum+=SubOtherExpenseSum;						
					SubPaidLossSum = SubAdjustmentExpenseSum = SubLegalExpenseSum = SubOtherExpenseSum = 0;
				}
					
				//Response.Write(dsTemp.Tables[0].Rows[ctr]["payment_amount"].ToString().Trim() + " br ");

				sbReport.Append(" ");
				if(ctr==0)
				{
					sbReport.Append( dsTemp.Tables[0].Rows[ctr]["LOSS_DATE"].ToString().Trim()  + "");
					sbReport.Append( dsTemp.Tables[0].Rows[ctr]["CLAIM_DESCRIPTION"].ToString().Trim()  + "");
				}
				else
				{
					sbReport.Append("");
				}
				sbReport.Append( dsTemp.Tables[0].Rows[ctr]["ENTRY_DATE"].ToString().Trim()  + "");
				sbReport.Append( dsTemp.Tables[0].Rows[ctr]["TRANSACTION_CODE"].ToString().Trim()  + "");
				sbReport.Append( dsTemp.Tables[0].Rows[ctr]["COV_DESC"].ToString().Trim()  + "");
				//if (PaidLoss!=0)
				sbReport.Append( String.Format("{0:,#,###}",Convert.ToInt64(dsTemp.Tables[0].Rows[ctr]["PAID_LOSS"])) + "");
				//else
				//	sbReport.Append("    ");
				SubPaidLossSum = Convert.ToInt64(dsTemp.Tables[0].Rows[ctr]["PAID_LOSS"]);
				SubLegalExpenseSum+= Convert.ToInt64(dsTemp.Tables[0].Rows[ctr]["LEGAL_EXPENSE"]);
				SubAdjustmentExpenseSum+= Convert.ToInt64(dsTemp.Tables[0].Rows[ctr]["ADJUSTMENT_EXPENSE"]);
				SubOtherExpenseSum+= Convert.ToInt64(dsTemp.Tables[0].Rows[ctr]["OTHER_EXPENSE"]);
				sbReport.Append( String.Format("{0:,#,###}",Convert.ToInt64(dsTemp.Tables[0].Rows[ctr]["OTHER_EXPENSE"])) + "");
				sbReport.Append( String.Format("{0:,#,###}",Convert.ToInt64(dsTemp.Tables[0].Rows[ctr]["LEGAL_EXPENSE"])) + "");
				sbReport.Append( String.Format("{0:,#,###}",Convert.ToInt64(dsTemp.Tables[0].Rows[ctr]["ADJUSTMENT_EXPENSE"])) + "");
				sbReport.Append( String.Format("{0:,#,###}",Convert.ToInt64(dsTemp.Tables[0].Rows[ctr]["REINSURANCE_RECOVERY"])) + "");
				sbReport.Append( dsTemp.Tables[0].Rows[ctr]["CHECK_NUMBER"].ToString().Trim()  + "");
				sbReport.Append( dsTemp.Tables[0].Rows[ctr]["CLEARING_DATE"].ToString().Trim()  + "");
				sbReport.Append(" ");
			}
			GrandPaidLossSum+= SubPaidLossSum;
			GrandLegalExpenseSum+=SubLegalExpenseSum;
			GrandAdjustmentExpenseSum+=SubAdjustmentExpenseSum;
			GrandOtherExpenseSum+=SubOtherExpenseSum;						
			sbReport.Append("<tr> <td class='midcolorr' colspan=5><b>Sub Total</b></td><td class='midcolorr'><b>" + String.Format("{0:,#,###}",SubPaidLossSum) + "</b></td><td class='midcolorr'><b>" + String.Format("{0:,#,###}",SubOtherExpenseSum) + "</b></td><td class='midcolorr'><b>" + String.Format("{0:,#,###}",SubLegalExpenseSum) + "</b></td><td class='midcolorr'><b>" + String.Format("{0:,#,###}",SubAdjustmentExpenseSum) + "</b></td><td colspan='3' class='midcolorr'>&nbsp;</td> </tr>");
			sbReport.Append("<tr> <td class='midcolorr' colspan=12>&nbsp;</td> </tr>");
			sbReport.Append("<tr> <td class='midcolorr' colspan=5><b>Grand Total</b></td><td class='midcolorr'><b>" + String.Format("{0:,#,###}",GrandPaidLossSum) + "</b></td><td class='midcolorr'><b>" + String.Format("{0:,#,###}",GrandOtherExpenseSum) + "</b></td><td class='midcolorr'><b>" + String.Format("{0:,#,###}",GrandLegalExpenseSum) + "</b></td><td class='midcolorr'><b>" + String.Format("{0:,#,###}",GrandAdjustmentExpenseSum) + "</b></td><td colspan='3' class='midcolorr'>&nbsp;</td> </tr>");
			//Response.Write(sbReport.ToString().Trim());
			//return sbReport;
			//			}
			//			catch(Exception ex)
			//			{
			//				lblMessage.Text = "Claim Activities have been not performed or completed.";
			//				lblMessage.Visible = true;
			//				btnPrintReport.Visible = btnExportToExcel.Visible = false;
			//			}	
	
			string strScript="javascript: ExportToFile";
			ClientScript.RegisterStartupScript(this.GetType(),"StartUpScript",strScript);
			Response.Write(sbReport.ToString().Trim());
			Response.End();			
		}

		private void btnExportToExcel_Click(object sender, System.EventArgs e)
		{
			ExportToExcel();		
		}


		
	}
}
