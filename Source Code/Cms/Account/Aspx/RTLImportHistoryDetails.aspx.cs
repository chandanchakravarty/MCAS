/******************************************************************************************
	<Author					: - > Swarup	
	<Start Date				: -	> 27-feb-2008
	<End Date				: - > 
	<Description			: - > To Show the report of RTL Import
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
using System.Text;
using System.Reflection;
using System.Resources;
using Cms.BusinessLayer.BlAccount;

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for RTLImportHistoryDetails.
	/// </summary>
	public class RTLImportHistoryDetails : Cms.Account.AccountBase
	{
		protected System.Web.UI.HtmlControls.HtmlTableCell tdRTLImportHistoryDetails;
		protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.WebControls.Label capHeader;
        protected System.Web.UI.WebControls.Label capDate;
        protected System.Web.UI.WebControls.Label capTime;
        protected System.Web.UI.WebControls.Label capRTLImportHistory;
        protected string Strtitle;
        System.Resources.ResourceManager objResourceMgr;
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			base.ScreenId = "420_0";//Screen_id set to 420_0 instead of 399_0 - Done by Sibin for Itrack Issue 4798 on 14 Jan 09
            objResourceMgr = new System.Resources.ResourceManager("Cms.Account.Aspx.RTLImportHistoryDetails", System.Reflection.Assembly.GetExecutingAssembly());
            CreateTable();
            setCaptions();
            Strtitle = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2010");
		}

        private void setCaptions()
        {
            capDate.Text = objResourceMgr.GetString("capDate");
            capHeader.Text = objResourceMgr.GetString("capHeader");
            capRTLImportHistory.Text = objResourceMgr.GetString("capRTLImportHistory");
            capTime.Text = objResourceMgr.GetString("capTime");
            
        }
		private void CreateTable()
		{
            DateTime DateFrom = Convert.ToDateTime(null);
            DateTime DateTo = Convert.ToDateTime(null);
			string DepositNumber="";
			string ProcessStatus = "";
            string DepositType = "";


            if (Request.Params["DateFrom"] != null && Request.Params["DateFrom"].ToString().Length > 0)
                DateFrom =ConvertToDate(Request.Params["DateFrom"].ToString());
                
            if (Request.Params["DateTo"] != null && Request.Params["DateTo"].ToString().Length > 0)
                DateTo =ConvertToDate(Request.Params["DateTo"].ToString());
            
			if(Request.Params["DepositNumber"]!=null && Request.Params["DepositNumber"].ToString().Length>0)
				DepositNumber = Request.Params["DepositNumber"].ToString();

            if (Request.Params["DepositType"] != null && Request.Params["DepositType"].ToString().Length > 0)
                DepositType = Request.Params["DepositType"].ToString();

			if(Request.Params["ProcessStatus"]!=null && Request.Params["ProcessStatus"].ToString().Length>0)
				ProcessStatus = Request.Params["ProcessStatus"].ToString();
			StringBuilder strBuilder = new StringBuilder();
			Cms.BusinessLayer.BlAccount.ClsAgencyStatement objAgencyStatement = new ClsAgencyStatement();
			DataSet objDataSet = new DataSet();

            objDataSet = objAgencyStatement.GetRTLImportHistoryDetails(DateFrom, DateTo, DepositNumber, ProcessStatus, DepositType);

			if (objDataSet == null)
			{
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1875");// "No records found for this search criteria.";
				return;
			}
			else
			{
				if (objDataSet.Tables[0].Rows.Count == 0)
				{
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1875"); //"No records found for this search criteria.";
					return;
				}
			}

            if (Convert.ToInt32(DepositType.ToString()) == 14832)
            {

                strBuilder.Append("<table cellSpacing='1' cellPadding='1' border='0' width='100%' align='center'>");
                strBuilder.Append("<tr height='20'>");
                strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='10%'><b>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1865") + "</b></td>");
                strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='10%'><b>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1968") + "</b></td>");
                strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='10%'><b>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1967") + "</b></td>");
                strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='15%'><b>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1970") + "</b></td>");
                strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='10%'><b>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1969") + "</b></td>"); 
                strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='15%'><b>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1870") + "</b></td>");
                strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='30%'><b>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1871") + "</b></td>");
            }
            else {

                strBuilder.Append("<table cellSpacing='1' cellPadding='1' border='0' width='100%' align='center'>");
                strBuilder.Append("<tr height='20'>");
                strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='10%'><b>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1865") + "</b></td>");
                strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='10%'><b>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1866") + "</b></td>");
                strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='22%'><b>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1867") + "</b></td>");
                strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='15%'><b>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1868") + "</b></td>");
                strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='15%'><b>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1869") + "</b></td>");
                strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='10%'><b>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1870") + "</b></td>");
                strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='25%'><b>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1871") + "</b></td>");
                strBuilder.Append("</tr>");

            }
			
			try
			{				
				if(objDataSet!=null)
				{
                    if (Convert.ToInt32(DepositType.ToString()) == 14832)
                    {
                        foreach (DataRow dr in objDataSet.Tables[0].Rows)
                        {

                            strBuilder.Append("<tr>");
                            strBuilder.Append("<td class='DataGridRow' align='Left'>" + dr["DEPOSIT_NUMBER"] + "</td>");
                            strBuilder.Append("<td class='DataGridRow' align='Left'>" + dr["LAEDER_POLICY_NO"] + "</td>");
                            strBuilder.Append("<td class='DataGridRow' align='Left'>" + dr["ENDORSMENT_NO"] + "</td>");
                            strBuilder.Append("<td class='DataGridRow' align='Left'>" + ConvertToDateCulture(Convert.ToDateTime(dr["PAYMENT_DATE"])) + "</td>");
                            strBuilder.Append("<td class='DataGridRow' align='Right'>" + dr["RISK_PREMIUM"] + "</td>");//Change Align=right of risk premium 
                            strBuilder.Append("<td class='DataGridRow' align='Center'>" + dr["STATUS"] + "</td>");
                            strBuilder.Append("<td class='DataGridRow' align='Left'>" + dr["ADDITIONAL_INFO"] + "</td>");
                            strBuilder.Append("</tr>");
                        }
                    }
                    else
                    {
                        foreach (DataRow dr in objDataSet.Tables[0].Rows)
                        {
                            strBuilder.Append("<tr>");
                            strBuilder.Append("<td class='DataGridRow' align='Left'>" + dr["DEPOSIT_NUMBER"] + "</td>");
                            strBuilder.Append("<td class='DataGridRow' align='Left'>" + ConvertToDateCulture(Convert.ToDateTime(dr["DATE"])) + "</td>");
                            strBuilder.Append("<td class='DataGridRow' align='Left'>" + dr["POLICY_NUMBER"].ToString().ToUpper() + "</td>");
                            strBuilder.Append("<td class='DataGridRow' align='Left'>" + dr["OUR_NUMBER"].ToString().ToUpper() + "</td>");
                            strBuilder.Append("<td class='DataGridRow' align='Right'>" + dr["AMOUNT"] + "</td>");
                            strBuilder.Append("<td class='DataGridRow' align='Center'>" + dr["STATUS"] + "</td>");
                            strBuilder.Append("<td class='DataGridRow' align='Left'>" + dr["ADDITIONAL_INFO"] + "</td>");
                            strBuilder.Append("</tr>");
                        }
                    }
				}
				else
				{
					strBuilder.Append("<tr>");
					strBuilder.Append("<td class='DataGridRow' align='Left' colspan='11'>"+Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1872")+"</td>");
					strBuilder.Append("</tr>");
					tdRTLImportHistoryDetails.InnerHtml = strBuilder.ToString();
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
			strBuilder.Append("<td height='24' class='midcolora'><INPUT class='clsButton' onclick='JavaScript:showPrint();' type='button' value='"+Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1873")+"'>");
			strBuilder.Append("<!-- <a href='JavaScript:showPrint()'><img src='../images/Print_blue.gif' border='0' WIDTH='78' HEIGHT='24'></a> --></td>");
			strBuilder.Append("<td height='24' class='midcolorr'><INPUT class='clsButton' onclick='JavaScript:window.close();' type='button' value='"+Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1874")+"'>");
			strBuilder.Append("<!-- <a href='JavaScript:window.close()'><img src='../images/close_blue.gif' border='0' WIDTH='78' HEIGHT='24'></a> --></td>");
			strBuilder.Append("</tr>");
			strBuilder.Append("</table>");
			strBuilder.Append("</span>");
			strBuilder.Append("</td>");
			strBuilder.Append("</tr>");
			strBuilder.Append("</TD></TR></table>");

			tdRTLImportHistoryDetails.InnerHtml = strBuilder.ToString();
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
