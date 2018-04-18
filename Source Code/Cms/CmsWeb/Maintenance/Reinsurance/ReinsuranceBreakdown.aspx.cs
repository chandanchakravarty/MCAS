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
using Cms.BusinessLayer.BlApplication;
//using Cms.BusinessLayer.BlQuote;
//using Microsoft.Xml.XQuery;
using Cms.BusinessLayer.BlCommon;
namespace Cms.CmsWeb.Maintenance.Reinsurance
{
	/// <summary>
	/// Summary description for ReinsuranceBreakdown.
	/// </summary>
	public class ReinsuranceBreakdown : Cms.CmsWeb.cmsbase  
	{
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.WebControls.Label lblDetailTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm REINSURANCE_BREAKDOWN;
		protected System.Web.UI.HtmlControls.HtmlTableRow formTable;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTabNumber;
		protected System.Web.UI.WebControls.Label capTRANSACTION_DATE;
		protected System.Web.UI.WebControls.Label txtTRANSACTION_DATE;
		protected System.Web.UI.WebControls.Label capTRANSACTION_NUMBER;
		protected System.Web.UI.WebControls.Label txtTRANSACTION_NUMBER;
		protected System.Web.UI.WebControls.Label capEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.Label txtEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.Label capTRANSACTION_DESC;
		protected System.Web.UI.WebControls.Label txtTRANSACTION_DESC;
		protected System.Web.UI.WebControls.DataGrid dgReinBreakdown;
		protected Cms.CmsWeb.Controls.CmsButton btnExportExcel;

		string strCUSTOMER_ID ;
		string strPolicyID ;
		string strPolVerId ;
		string strProcessId ;
		string strTranNo;

		
		private void Page_Load(object sender, System.EventArgs e)
		{
			
			#region Setting screen id
			base.ScreenId	=	"224_13"; 
			#endregion
			//strCUSTOMER_ID = GetCustomerID();
			//strPolicyID = GetPolicyID();
			//strPolVerId = GetPolicyVersionID();
			strCUSTOMER_ID=Request.QueryString["CUSTOMER_ID"].ToString();
			strPolicyID=Request.QueryString["POLICY_ID"].ToString();
			strPolVerId=Request.QueryString["POL_VERSION_ID"].ToString();
			strProcessId=Request.QueryString["PROCESSID"].ToString();
			strTranNo=Request.QueryString["TRAN_SN"].ToString();
			capMessage.Visible=false;
			btnExportExcel.CmsButtonClass	= Cms.CmsWeb.Controls.CmsButtonType.Execute;
			btnExportExcel.PermissionString	= gstrSecurityXML;
			if (!Page.IsPostBack)
			{
				fillInfo();
			}
		}
		private void fillInfo()
		{
			try
			{
				Cms.BusinessLayer.BlApplication.clsapplication objapplication = new clsapplication();
				DataSet dsReins = objapplication.GetReinsurance_Breakdown_Details(int.Parse(strCUSTOMER_ID), int.Parse(strPolicyID), int.Parse(strPolVerId),int.Parse(strProcessId));
				StringBuilder strBuilder = new StringBuilder();
				
				strBuilder.Append("");
				//int policyVersionID=0;
				string lobID="0";
				if(dsReins != null && dsReins.Tables[0].Rows.Count > 0)
				{
				
					strBuilder.Append("<table width='100% class='tableWidth'>");
					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td class='DataGridRow' align='center' colspan='6'><b>Reinsurance Breakdown</b></td>");
					strBuilder.Append("</tr>");

					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td width='15%' class='DataGridRow' align='left'><b>Policy Number</b></td>");
					strBuilder.Append("<td width='15%' class='DataGridRow' align='left'>" + dsReins.Tables[0].Rows[0]["POLICY_NUMBER"].ToString() +"</td>");

					strBuilder.Append("<td width='15%' class='DataGridRow' align='left'><b>Line of Business</b></td>");
					strBuilder.Append("<td width='20%' class='DataGridRow' align='left'>" + dsReins.Tables[0].Rows[0]["LOB_DESC"].ToString() + "</td>");

					strBuilder.Append("<td width='15%' class='DataGridRow' align='left'><b>Insured/Address</b></td>");
					strBuilder.Append("<td width='20%' class='DataGridRow' align='left'>" + dsReins.Tables[0].Rows[0]["INSURED_NAME"].ToString() + " " + dsReins.Tables[0].Rows[0]["ADDRESS"].ToString() + "</td>");

					strBuilder.Append("</tr>");

					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td width='15%' class='DataGridRow' align='left'><b>State</b></td>");
					strBuilder.Append("<td width='15%' class='DataGridRow' align='left'>" + dsReins.Tables[0].Rows[0]["STATE_DESC"].ToString() +"</td>");

					strBuilder.Append("<td width='15%' class='DataGridRow' align='left'><b>Policy Effective Date</b></td>");
					strBuilder.Append("<td width='20%' class='DataGridRow' align='left'>" + dsReins.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString() + "</td>");

					strBuilder.Append("<td width='15%' class='DataGridRow' align='left'><b>Policy Expiration Date</b></td>");
					strBuilder.Append("<td width='20%' class='DataGridRow' align='left'>" + dsReins.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString() + "</td>");
					strBuilder.Append("</tr>");

					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td width='15%' class='DataGridRow' align='left'><b>Date Of Change/ Cancellation</b></td>");
					strBuilder.Append("<td width='15%' class='DataGridRow' align='left'>" + dsReins.Tables[0].Rows[0]["TRAN_EFF_DATE"].ToString() +"</td>");
					
					strBuilder.Append("<td width='15%' class='DataGridRow' align='left'><b>Transaction Type</b></td>");
					strBuilder.Append("<td width='20%' class='DataGridRow' align='left'>" + dsReins.Tables[0].Rows[0]["TRAN_DESC"].ToString() + "</td>");
					
					strBuilder.Append("<td width='15%' class='DataGridRow' align='left'><b>Transaction Date</b></td>");
					strBuilder.Append("<td width='20%' class='DataGridRow' align='left'>" + dsReins.Tables[0].Rows[0]["TRAN_DATE"].ToString() + "</td>");
					strBuilder.Append("</tr>");
					
					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td width='15%' class='DataGridRow' align='left'><b>Transaction Sequence #</b></td>");
					strBuilder.Append("<td width='15%' class='DataGridRow' align='left'>" + strTranNo +"</td>");
					
					strBuilder.Append("<td width='15%' class='DataGridRow' align='left'><b>Special Acceptances</b></td>");
					strBuilder.Append("<td width='15%' class='DataGridRow' align='left'>" + dsReins.Tables[0].Rows[0]["REINS_SPECIAL_ACPT"].ToString() +"</td>");
					
					strBuilder.Append("<td  width='70%' class='DataGridRow' colspan='2'></td>");
					strBuilder.Append("</tr>");

//					strBuilder.Append("<tr height='20'>");
//					strBuilder.Append("<td width='15%' class='DataGridRow' align='left'><b>Special Acceptances</b></td>");
//					strBuilder.Append("<td width='15%' class='DataGridRow' align='left'>" + dsReins.Tables[0].Rows[0]["REINS_SPECIAL_ACPT"].ToString() +"</td>");
//					strBuilder.Append("<td  width='70%' class='DataGridRow' colspan='4'></td>");
//					strBuilder.Append("</tr>");
					strBuilder.Append("</table>");
					lobID=dsReins.Tables[0].Rows[0]["LOB_ID"].ToString();
					lblTemplate.Text=strBuilder.ToString();
					//Contract Section
					strBuilder.Length=0;
					strBuilder.Append("<table class='tableWidth'>");
					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td class='headerEffectSystemParams' colspan='6'><b>Breakdown Details</b></td>");
					strBuilder.Append("</tr>");
					/*
					strBuilder.Append("<td  class='DataGridRow' align='left'><b>Contract#</b></td>");
					strBuilder.Append("<td  class='DataGridRow' align='left'><b>Premium Basis</b></td>");
					strBuilder.Append("<td  class='DataGridRow' align='left'><b>Contract Year</b></td>");
					strBuilder.Append("<td  class='DataGridRow' align='left'><b>Total Insurance Value</b></td>");
					strBuilder.Append("<td  class='DataGridRow' align='left'><b>TIV Reinsurance Group</b></td>");
					strBuilder.Append("<td  class='DataGridRow' align='left'><b>Layer</b></td>");
					strBuilder.Append("<td  class='DataGridRow' align='left'><b>Layer Amount</b></td>");
					strBuilder.Append("<td  class='DataGridRow' align='left'><b>Reinsurance Limit/FAB</b></td>");
					strBuilder.Append("<td  class='DataGridRow' align='left'><b>Coverage Category</b></td>");
					strBuilder.Append("<td  class='DataGridRow' align='left'><b>Transaction Premium</b></td>");
					strBuilder.Append("<td  class='DataGridRow' align='left'><b>Earned</b></td>");
					strBuilder.Append("<td  class='DataGridRow' align='left'><b>Written</b></td>");
					strBuilder.Append("<td  class='DataGridRow' align='left'><b>Contruction</b></td>");
					strBuilder.Append("<td  class='DataGridRow' align='left'><b>Protection</b></td>");
					strBuilder.Append("<td  class='DataGridRow' align='left'><b>Central Alarm</b></td>");
					strBuilder.Append("<td  class='DataGridRow' align='left'><b>New Home</b></td>");
					strBuilder.Append("<td  class='DataGridRow' align='left'><b>Age Of Home</b></td>");
					strBuilder.Append("<td  class='DataGridRow' align='left'><b>Deduction Layer 1</b></td>");
					strBuilder.Append("<td  class='DataGridRow' align='left'><b>Deduction Layer 2</b></td>");
					strBuilder.Append("<td  class='DataGridRow' align='left'><b>Rate</b></td>");
					strBuilder.Append("<td  class='DataGridRow' align='left'><b>Reinsurance Premium</b></td>");
					strBuilder.Append("<td  class='DataGridRow' align='left'><b>Commission %</b></td>");
					strBuilder.Append("<td  class='DataGridRow' align='left'><b>Comm. Amount</b></td>");
					strBuilder.Append("<td  class='DataGridRow' align='left'><b>Net Due</b></td>");
					strBuilder.Append("<td  class='DataGridRow' align='left'><b>Major Participant</b></td>");
					strBuilder.Append("<td  class='DataGridRow' align='left'><b>Retention %</b></td>");
					strBuilder.Append("<td  class='DataGridRow' align='left'><b>Reinsurance Ceded $</b></td>");
					strBuilder.Append("<td  class='DataGridRow' align='left'><b>Special Acceptance</b></td>");
					strBuilder.Append("</tr>");*/
					strBuilder.Append("</table>");
					
					lblDetailTemplate.Text=strBuilder.ToString();

//					dgReinBreakdown.Columns[4].ItemStyle.HorizontalAlign=HorizontalAlign.Right;
//					dgReinBreakdown.Columns[7].ItemStyle.HorizontalAlign=HorizontalAlign.Right;
//					dgReinBreakdown.Columns[8].ItemStyle.HorizontalAlign=HorizontalAlign.Right;
//					dgReinBreakdown.Columns[10].ItemStyle.HorizontalAlign=HorizontalAlign.Right;
//					dgReinBreakdown.Columns[11].ItemStyle.HorizontalAlign=HorizontalAlign.Right;
//					dgReinBreakdown.Columns[12].ItemStyle.HorizontalAlign=HorizontalAlign.Right;
//
//					dgReinBreakdown.Columns[20].ItemStyle.HorizontalAlign=HorizontalAlign.Right;
//					dgReinBreakdown.Columns[21].ItemStyle.HorizontalAlign=HorizontalAlign.Right;
//					dgReinBreakdown.Columns[22].ItemStyle.HorizontalAlign=HorizontalAlign.Right;
//					dgReinBreakdown.Columns[23].ItemStyle.HorizontalAlign=HorizontalAlign.Right;
//					dgReinBreakdown.Columns[24].ItemStyle.HorizontalAlign=HorizontalAlign.Right;
//					dgReinBreakdown.Columns[26].ItemStyle.HorizontalAlign=HorizontalAlign.Right;
//					dgReinBreakdown.Columns[27].ItemStyle.HorizontalAlign=HorizontalAlign.Right;
//
//					dgReinBreakdown.DataSource=dsReins.Tables[2];
//					dgReinBreakdown.DataBind();
//					dsReins.Dispose();
					BindGrid() ;
				}
			}
			catch(Exception ex)
			{
				//throw(ex);
				capMessage.Text="Error." + ex.Message;
				capMessage.Visible=true;
			}
		}
		protected void dgReinBreakdown_Paging(object sender, System.Web.UI.WebControls.DataGridPageChangedEventArgs  e)
		{
			
			dgReinBreakdown.CurrentPageIndex = e.NewPageIndex; 
			BindGrid() ; //hidContract_num.Value,hidtran_type.Value);

		}
		private void BindGrid()
		{
			try
			{
				Cms.BusinessLayer.BlApplication.clsapplication objapplication = new clsapplication();
				DataSet dsReins = objapplication.GetReinsurance_Breakdown_Details(int.Parse(strCUSTOMER_ID), int.Parse(strPolicyID), int.Parse(strPolVerId),int.Parse(strProcessId));

				dgReinBreakdown.Columns[4].ItemStyle.HorizontalAlign=HorizontalAlign.Right;
				dgReinBreakdown.Columns[7].ItemStyle.HorizontalAlign=HorizontalAlign.Right;
				dgReinBreakdown.Columns[8].ItemStyle.HorizontalAlign=HorizontalAlign.Right;
				dgReinBreakdown.Columns[10].ItemStyle.HorizontalAlign=HorizontalAlign.Right;
				dgReinBreakdown.Columns[11].ItemStyle.HorizontalAlign=HorizontalAlign.Right;
				dgReinBreakdown.Columns[12].ItemStyle.HorizontalAlign=HorizontalAlign.Right;

				dgReinBreakdown.Columns[20].ItemStyle.HorizontalAlign=HorizontalAlign.Right;
				dgReinBreakdown.Columns[21].ItemStyle.HorizontalAlign=HorizontalAlign.Right;
				dgReinBreakdown.Columns[22].ItemStyle.HorizontalAlign=HorizontalAlign.Right;
				dgReinBreakdown.Columns[23].ItemStyle.HorizontalAlign=HorizontalAlign.Right;
				dgReinBreakdown.Columns[24].ItemStyle.HorizontalAlign=HorizontalAlign.Right;
				dgReinBreakdown.Columns[26].ItemStyle.HorizontalAlign=HorizontalAlign.Right;
				dgReinBreakdown.Columns[27].ItemStyle.HorizontalAlign=HorizontalAlign.Right;



				dgReinBreakdown.DataSource=dsReins.Tables[2];
				dgReinBreakdown.DataBind();

				dsReins.Dispose();
			}
			catch(Exception ex)
			{
				capMessage.Text="Error." + ex.Message;
				capMessage.Visible=true;
			}

		}
		private void dgReinBreakdown_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
						
			//Adding Style to Alternating Item
			e.Item.Attributes.Add("Class","midcolora");
			if ( e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item )
			{
				//Label lblCOV_ID = (Label)e.Item.FindControl("lblCOV_ID");
				//DropDownList ddlSigObt = (DropDownList)e.Item.FindControl("ddlSignatureObtained");
				//Label lblSigObt = (Label)e.Item.FindControl("lblSigObt");
				//dgReinBreakdown.Columns(0).ItemStyle.HorizontalAlign=HorizontalAlign.Right;
				

				
			}

			
		}
		private void btnExportExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				Response.Clear();
				Response.Buffer= true;			
				Response.AddHeader("content-disposition", "attachment;filename=ReinsuranceBreakDown.xls");
				Response.Charset = "";
				Response.Cache.SetCacheability(HttpCacheability.NoCache);
				Response.ContentType="application/vnd.ms-excel";

				Response.ContentEncoding = System.Text.Encoding.UTF7;
				EnableViewState = false;

				System.IO.StringWriter stringWrite = new System.IO.StringWriter();
				System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
				//REINSURANCE_BREAKDOWN.RenderControl(htmlWrite);
				dgReinBreakdown.RenderControl(htmlWrite);
				Response.Write("<html><head><meta http-equiv=Content-Type content='text/html; charset=utf-8'>") ;
				Response.Write("<style>td{mso-number-format:\\@}</style>");
				Response.Write(stringWrite.ToString());	
				Response.Write("</body></html>");
				stringWrite.Close();
				htmlWrite.Close();
				Response.End();
			}
			catch(Exception ex)
			{
				capMessage.Text="Error :" + ex.Message;
				capMessage.Visible=true;
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
			this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
		}
		#endregion
	}
}
