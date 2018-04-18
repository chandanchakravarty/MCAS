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
	public class EFTSweepHistoryDetails : Cms.Account.AccountBase
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdEFTSweepHistoryDetails;
		protected System.Web.UI.WebControls.Label lblAgencyName;
		//added variables
		public string pathPlus	=	"/cms/cmsweb/Images/plus2.gif";
		public string pathMinus	=	"/cms/cmsweb/Images/minus2.gif";
		
		//

		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId = "406_0";
			CreateTable();
		}

		private void CreateTable()
		{
			string DateFromSpool="";
			string DateToSpool="";
			string DateFromSweep="";
			string DateToSweep="";
			string EntityType="";
			//string strEntityType="";
			//string strBatchNumber="";
			string TransactionAmount = "";
         
			if(Request.Params["DateFromSpool"]!=null && Request.Params["DateFromSpool"].ToString().Length>0)
				DateFromSpool= Request.Params["DateFromSpool"].ToString();
			if(Request.Params["DateToSpool"]!=null && Request.Params["DateToSpool"].ToString().Length>0)
				DateToSpool	= Request.Params["DateToSpool"].ToString();
			if(Request.Params["DateFromSweep"]!=null && Request.Params["DateFromSweep"].ToString().Length>0)
				DateFromSweep= Request.Params["DateFromSweep"].ToString();
			if(Request.Params["DateToSweep"]!=null && Request.Params["DateToSweep"].ToString().Length>0)
				DateToSweep	= Request.Params["DateToSweep"].ToString();
			if(Request.Params["EntityType"]!=null && Request.Params["EntityType"].ToString().Length>0)
				EntityType	= Request.Params["EntityType"].ToString();		
	
			if(Request.Params["TransactionAmount"]!=null && Request.Params["TransactionAmount"].ToString().Length>0)
				TransactionAmount = Request.Params["TransactionAmount"].ToString();

			StringBuilder strBuilder = new StringBuilder();
			Cms.BusinessLayer.BlAccount.ClsAgencyStatement objAgencyStatement = new ClsAgencyStatement();
			DataSet objDataSet = new DataSet();	        
			objDataSet = objAgencyStatement.GetSweepHistoryDetailsEFT(DateFromSpool,DateToSpool,DateFromSweep,DateToSweep,EntityType,TransactionAmount);

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
			//added code on 26 Oct by uday
			string hStyle = "cursor:hand;";
			
			
		    //end
			strBuilder.Append("<table cellSpacing='1' cellPadding='1' border='0' width='100%' align='center'>");
			//changes on 26 Oct
		/*	strBuilder.Append("<tr height='20'>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='10%'><b>Entity Type</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='15%'><b>Entity Name</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='10%'><b>Policy Number</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Right' width='10%'><b>Amount</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='10%'><b>Spooled Date</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Right' width='10%'><b>Processed Date</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Right' width='5%'><b>Status</b></td>");			
			strBuilder.Append("<td class='headereffectWebGrid' align='Right' width='10%'><b>Additional Info</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Right' width='10%'><b>Check Number</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Right' width='10%'><b>Deposit Number</b></td>");
			strBuilder.Append("</tr>");*/
			//end
			try
			{				
				if(objDataSet!=null)
				{
					if (objDataSet.Tables[1].Rows.Count == 0)
					{
						lblMessage.Text =  "No records found for this search criteria.";
						return;
					}
					foreach(DataRow drBatch in objDataSet.Tables[1].Rows)
					{

						string imgid = drBatch["ENTITY_TYPE"].ToString().Trim().ToUpper()+ "_" + drBatch["BATCH_NUMBER"].ToString().Trim() + "_imgID";
						string Type	= drBatch["ENTITY_TYPE"].ToString().Trim().ToUpper()+ "_" + drBatch["BATCH_NUMBER"].ToString().Trim();
						string jsFunction = "javascript:showHideInfo('" + Type + "');";
						string ToolTip = Type;

						
						strBuilder.Append("<tr>");
						strBuilder.Append("<tr height='20'>");
						strBuilder.Append("<td colspan=2 class='headereffectWebGrid' align='Left' width='10%'><b>Entity Type</b></td>");
						strBuilder.Append("<td colspan=2 class='headereffectWebGrid' align='Left' width='15%'><b>Nacha File Transmitted</b></td>");	
						strBuilder.Append("<td colspan=2 class='headereffectWebGrid' align='Left' width='10%'><b>Total Debit</b></td>");	
						strBuilder.Append("<td colspan=2 class='headereffectWebGrid' align='Left' width='10%'><b>Total Credit</b></td>");	
						strBuilder.Append("<td colspan=2 class='headereffectWebGrid' align='Left' width='15%'><b>Created Date</b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img id=" + imgid + " src=" + pathPlus + " style=" + hStyle + " onclick=" + jsFunction + " alt=" + ToolTip +" ></td>");

						strBuilder.Append("</tr>");
						strBuilder.Append("<td class='DataGridRow' colspan=2 align='Left'>"+drBatch["ENTITY_TYPE"] +"</td>");
						strBuilder.Append("<td class='DataGridRow' colspan=2 align='Left'>"+drBatch["NACHA_FILE_NAME"]+"</td>");	
						strBuilder.Append("<td class='DataGridRow' colspan=2 align='Right'>"+ "$" + drBatch["TOTAL_DEBIT"]+ "(" + drBatch["TRANSACTION_CODE_DEBIT"] + ")"+"</td>");	
						strBuilder.Append("<td class='DataGridRow' colspan=2 align='Right'>"+ "$" + drBatch["TOTAL_CREDIT"]+ "(" +  drBatch["TRANSACTION_CODE_CREDIT"] + ")"+"</td>");	
						strBuilder.Append("<td class='DataGridRow' colspan=2 align='Left'>"+drBatch["CREATED_DATE_TIME"]+"</td>");	
						strBuilder.Append("</tr>");	

					
						strBuilder.Append("<tr style= display:'none' height='20' id = " + Type + " >");
						strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='10%'><b>Entity Type</b></td>");
						strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='15%'><b>Entity Name</b></td>");
						strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='10%'><b>Policy Number</b></td>");
						strBuilder.Append("<td class='headereffectWebGrid' align='Right' width='10%'><b>Amount</b></td>");
						strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='10%'><b>Spooled Date</b></td>");
						strBuilder.Append("<td class='headereffectWebGrid' align='Right' width='10%'><b>Processed Date</b></td>");
						strBuilder.Append("<td class='headereffectWebGrid' align='Right' width='5%'><b>Status</b></td>");
						strBuilder.Append("<td class='headereffectWebGrid' align='Right' width='10%'><b>Additional Info</b></td>");
						strBuilder.Append("<td class='headereffectWebGrid' align='Right' width='10%'><b>Check Number</b></td>");
						strBuilder.Append("<td class='headereffectWebGrid' align='Right' width='10%'><b>Deposit Number</b></td>");
						strBuilder.Append("</tr>");

						foreach(DataRow dr in objDataSet.Tables[0].Rows)
						{
							if(
								(drBatch["BATCH_NUMBER"].ToString().Trim() == dr["BATCH_NUMBER"].ToString().Trim())
								&&
								(drBatch["ENTITY_TYPE_CODE"].ToString().Trim() == dr["ENTITY_TYPE_CODE"].ToString().Trim())
							  )
							{

								string tableid = dr["ENTITY_TYPE"].ToString().Trim() + "_" + dr["BATCH_NUMBER"].ToString().Trim();;

								strBuilder.Append("<tr style= display:'none' id = " + tableid + " height='20'>");
								strBuilder.Append("<td class='DataGridRow' align='Left' width='10%'>"+dr["ENTITY_TYPE"]+"</td>");
								strBuilder.Append("<td class='DataGridRow' align='Left' width='15%'>"+dr["ENTITY_NAME"]+"</td>");
								strBuilder.Append("<td class='DataGridRow' align='Left' width='10%'>"+dr["POLICY_NUMBER"].ToString().ToUpper()+"</td>");
								//Format Added For Itrack Issue #6738
								strBuilder.Append("<td class='DataGridRow' align='Right' width='10%'>"+ "$" + String.Format("{0:n}",dr["AMOUNT"])+"</td>");
								strBuilder.Append("<td class='DataGridRow' align='Left' width='10%'>"+dr["SPOOLED_DATETIME"]+"</td>");
								strBuilder.Append("<td class='DataGridRow' align='Right' width='10%'>"+dr["PROCESSED_DATETIME"]+"</td>");
								strBuilder.Append("<td class='DataGridRow' align='Right' width='5%'>"+dr["STATUS"]+"</td>");
								strBuilder.Append("<td class='DataGridRow' align='Right' width='10%'>"+dr["ADDITIONAL_INFO"]+"</td>");
								strBuilder.Append("<td class='DataGridRow' align='Right' width='10%'>"+dr["CHECK_NUMBER"]+"</td>");
								strBuilder.Append("<td class='DataGridRow' align='Right' width='10%'>"+dr["DEPOSIT_NUMBER"]+"</td>");
								strBuilder.Append("</tr>");
							
								//string tableid = dr["ENTITY_TYPE"].ToString() ;
								/*strBuilder.Append("<tr id = " + tableid + ">");
								strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["ENTITY_TYPE"]+"</td>");*/
//								if(strEntityType != dr["ENTITY_TYPE"].ToString())
//								{
//									strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["ENTITY_TYPE"]+"</td>");
//								}
//								else
//								{
//									strBuilder.Append("<td class='DataGridRow' align='Left'> </td>");
//								}
						

								/*strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["ENTITY_NAME"]+"</td>");
								strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["POLICY_NUMBER"]+"</td>");
								strBuilder.Append("<td class='DataGridRow' align='Right'>"+ "$" + dr["AMOUNT"]+"</td>");
								strEntityType=dr["ENTITY_TYPE"].ToString();
						
								strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["SPOOLED_DATETIME"]+"</td>");
								strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["PROCESSED_DATETIME"]+"</td>");
								strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["STATUS"]+"</td>");
								strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["ADDITIONAL_INFO"]+"</td>");
								strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["CHECK_NUMBER"]+"</td>");
								strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["DEPOSIT_NUMBER"]+"</td>");
								strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["BATCH_NUMBER"]+"</td>");
								strBuilder.Append("</tr>");	*/				
							}		
						}
					}
				}
				else
				{
					strBuilder.Append("<tr>");
					strBuilder.Append("<td class='DataGridRow' align='Left' colspan='11'>No Record Found.</td>");
					strBuilder.Append("</tr>");
					tdEFTSweepHistoryDetails.InnerHtml = strBuilder.ToString();
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

			tdEFTSweepHistoryDetails.InnerHtml = strBuilder.ToString();
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
