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
	/// Summary description for CustomerBalance.
	/// </summary>
	public class CustomerBalance : Cms.Account.AccountBase
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAgencyStatementTD;
		protected System.Web.UI.WebControls.ImageButton btnPrevious;
		protected System.Web.UI.WebControls.ImageButton btnNext;	
		protected System.Web.UI.WebControls.Label lblCurrentPage;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidpolCount;
		public int CurrentPageIndex
		{
			set { ViewState["CurrentPageIndex"] = value;}
			get{ return Convert.ToInt32(ViewState["CurrentPageIndex"]); }
		}
		
		public int intCustomerID=0,intTotalPages=0,remainder = 0,polCount;
		protected System.Web.UI.WebControls.Label capGOTO;
		protected System.Web.UI.WebControls.TextBox txtGOTO;
		protected System.Web.UI.WebControls.RegularExpressionValidator revGOTO;
		protected System.Web.UI.WebControls.CustomValidator csvGOTO;
		protected System.Web.UI.WebControls.Label lblSEARCH_POLICYNUMBER;
		protected System.Web.UI.WebControls.TextBox txtSEARCH_POLICYNUMBER;
		protected Cms.CmsWeb.Controls.CmsButton btnSEARCH_POLICYNUMBER;
		protected Cms.CmsWeb.Controls.CmsButton btnGOTO;
		

	
		private void Page_Load(object sender, System.EventArgs e)
		{
			//Setting the cookie value, To be used by customer manager
			//Setting the screen id to 120_7 instead of 134_2-Added by Sibin
			base.ScreenId = "120_7";
			string strCustomerID = Request.Params["CUSTOMER_ID"].ToString();
			btnGOTO.CmsButtonClass	 = CmsButtonType.Write;
			btnGOTO.PermissionString = gstrSecurityXML;

			btnSEARCH_POLICYNUMBER.CmsButtonClass   = CmsButtonType.Write;
			btnSEARCH_POLICYNUMBER.PermissionString = gstrSecurityXML;
			
			btnSEARCH_POLICYNUMBER.Attributes.Add("onclick","if(document.getElementById('txtSEARCH_POLICYNUMBER').value == '') return false;");
			txtSEARCH_POLICYNUMBER.Attributes.Add("onblur","if (ValidatePolicyNum(document.getElementById('txtSEARCH_POLICYNUMBER').value,'" + strCustomerID + "') == false) return false;");
			SetCookieValue();	
			lblMessage.Visible = false;
			if ( !Page.IsPostBack )
			{
				CurrentPageIndex = 1;
				hidpolCount.Value ="0";
							
			}
			
			if (Request.Params["CUSTOMER_ID"] != null && Request.Params["CUSTOMER_ID"] != "")
			{
				intCustomerID = int.Parse(Request.Params["CUSTOMER_ID"].ToString());
				CreateTable(intCustomerID);
			}
			else
			{
				//Please select customer id
			}
			// set the images 
			SetImages();	
			
			SetErrorMessages();
			
		}

		private void SetErrorMessages()
		{
			try
			{
				csvGOTO.ErrorMessage		 = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("341");
			}
			catch
			{
				return;
			}
		}
		private void SetCookieValue ()
		{
            Response.Cookies["LastVisitedTab"].Value = "5"; //Changed from 6 for Policy Page Implementation
			Response.Cookies["LastVisitedTab"].Expires = DateTime.Now.Add(new TimeSpan(30,0,0,0,0));
		}
		// set the images 
		private void SetImages()
		{
			if(CurrentPageIndex == intTotalPages)
			{
				this.btnNext.ImageUrl = "~/cmsweb/images/nextoff.gif";
				this.btnNext.Attributes.Add("onclick","return OnClick();");
			}
			else
			{
				this.btnNext.ImageUrl = "~/cmsweb/images/next.gif";
				this.btnNext.Attributes.Remove("onclick");
			}
			
			if(CurrentPageIndex == 1)
			{
				this.btnPrevious.ImageUrl = "~/cmsweb/images/prevoff.gif";
				this.btnPrevious.Attributes.Add("onclick","return OnClick();");
			}
			else
			{
				this.btnPrevious.ImageUrl = "~/cmsweb/images/prev.gif";
				this.btnPrevious.Attributes.Remove("onclick");
			}
		}

		public void CreateTable(int intCustomerId)
		{
			
			double RunningBalance = 0;			
			StringBuilder strBuilder = new StringBuilder();
			Cms.BusinessLayer.BlAccount.ClsDeposit objAccount = new Cms.BusinessLayer.BlAccount.ClsDeposit();
			DataSet ds;
			DataTable objData = new DataTable();	

			int pageSize = base.DepositDetailsPageSize; // To 
			int  PageIndex = Convert.ToInt32(ViewState["CurrentPageIndex"]); // from
			
			//From			
			int currentPageIndex =(((PageIndex-1)*pageSize) + 1);			
			//To
			int endPageIndex = pageSize + currentPageIndex;
			
			string PolicyNumber  = txtSEARCH_POLICYNUMBER.Text.Trim() == "" ? "" : txtSEARCH_POLICYNUMBER.Text.Trim();
			
			ds  = objAccount.GetCustomerBalance(intCustomerId,endPageIndex,currentPageIndex,PolicyNumber);
			objData = ds.Tables[0];
			int countPage = int.Parse(ds.Tables[2].Rows[0][0].ToString());
			if(PolicyNumber != "" )
			{
				if(hidpolCount.Value =="0")
				{
					CurrentPageIndex = 1;
					hidpolCount.Value ="1";
				}
				PageIndex = Convert.ToInt32(ViewState["CurrentPageIndex"]); // from
			
				//From			
				currentPageIndex =(((PageIndex-1)*pageSize) + 1);			
				//To
				endPageIndex = pageSize + currentPageIndex;
			}
//			RunningBalance = Double.Parse(ds.Tables[1].Rows[0][0].ToString());
			if(ds.Tables[1].Rows[0][0] != DBNull.Value)
			{
				if(ds.Tables[1].Rows[0][0].ToString() != ""  )
				{
					RunningBalance = Double.Parse(ds.Tables[1].Rows[0][0].ToString());
				}
			}

			if (objData == null)
			{
				lblMessage.Text = "No transaction has been posted for the selected customer.";
				lblMessage.Visible = true;
				return;
			}
			else
			{
				if (objData.Rows.Count == 0)
				{
					lblMessage.Text =  "No transaction has been posted for the selected customer.";
					lblMessage.Visible = true;
					return;
				}
			}
			
			//Here we will get total no of pages 				
			int count = int.Parse(ds.Tables[2].Rows[0][0].ToString());
			if(count > 18)
			{
				intTotalPages = count / pageSize;
				remainder = count % pageSize;
			}
			else
			{
				intTotalPages = 1;
				
			}
			if ( remainder == 0 )
			{
				intTotalPages =intTotalPages;
			}
			else
			{
				intTotalPages = intTotalPages + 1;
			}
		
				
			// show the current page number
			this.lblCurrentPage.Text = "Page " + this.CurrentPageIndex.ToString()+ " of " + intTotalPages.ToString();
			
			
			strBuilder.Append("<table cellSpacing='1' cellPadding='1' border='0' width='100%' align='center'>");
			
			strBuilder.Append("<tr height='20'>");
			strBuilder.Append("<td class='DataGridRow' align='Left' width='9%'><b>Policy Number</b></td>");

			strBuilder.Append("<td class='DataGridRow' align='Left' width='9%'><b>Tran. Date</b></td>");
			strBuilder.Append("<td class='DataGridRow' align='Left' width='20%'><b>Activity</b></td>");
			
			strBuilder.Append("<td class='DataGridRow' align='Left' width='5%'><b>Source Tran #</b></td>");
			strBuilder.Append("<td class='DataGridRow' align='Left' width='10%'><b>Tran Type</b></td>");
			strBuilder.Append("<td class='DataGridRow' align='Left' width='10%'><b>Effective Date</b></td>");
			strBuilder.Append("<td class='DataGridRow' align='Left' width='9%'><b>Due Date</b></td>");
			strBuilder.Append("<td class='DataGridRow' align='Right' width='8%'><b>Amount</b></td>");
			strBuilder.Append("<td class='DataGridRow' align='Right' width='8%'><b>Amount Due</b></td>");
			
			strBuilder.Append("</tr>");

			string strOldPolicyNo = "";
			double PolicyDueAmount = 0;
			try
			{				
				if(objData!=null)
				{
					foreach(DataRow dr in objData.Rows)
					{
						if (strOldPolicyNo != dr["POLICY_NUMBER"].ToString() && strOldPolicyNo != "")
						{
							strBuilder.Append("<tr>");
							strBuilder.Append("<td colspan='8' class='DataGridRow' align='Right'><span class='labelfont'>"+"Total Due"+"</span></td>");
							strBuilder.Append("<td class='DataGridRow' align='Right'><span class='labelfont'>"+PolicyDueAmount.ToString("N")+"</span></td>");
							strBuilder.Append("</tr>");
							//PolicyDueAmount = 0;

						}

						strBuilder.Append("<tr>");
						
						if (strOldPolicyNo != dr["POLICY_NUMBER"].ToString() || strOldPolicyNo == "")
						{
							strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["POLICY_NUMBER"]+"</td>");
						}
						else
						{
							strBuilder.Append("<td class='DataGridRow' align='Left'>"+""+"</td>");
						}
					
						
						strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["SOURCE_TRAN_DATE"]+"</td>");
						strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["TRANS_DESC"]+"</td>");
						
						strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["SOURCE_NUM"]+"</td>");
						strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["SOURCE_TYPE"]+"</td>");
						strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["SOURCE_EFF_DATE"]+"</td>");
						strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["DUE_DATE"]+"</td>");
						strBuilder.Append("<td class='DataGridRow' align='Right'>"+Convert.ToDouble(dr["AMOUNT"]).ToString("N")+"</td>");

						strBuilder.Append("<td class='DataGridRow' align='Right'>" + Convert.ToDouble(dr["AMOUNT_DUE"]).ToString("N") + "</td>");
//						if (dr["AMOUNT_DUE"].ToString() != "")
//						{
//							PolicyDueAmount += Double.Parse(dr["POLICY_BALANCE"].ToString());
//						}
						
//						PolicyDueAmount = Double.Parse(dr["POLICY_BALANCE"].ToString());						
						if(dr["POLICY_BALANCE"] != DBNull.Value)
						{
							if(dr["POLICY_BALANCE"].ToString() != "")
							{
								PolicyDueAmount = Double.Parse(dr["POLICY_BALANCE"].ToString());						
							}
						}

						strOldPolicyNo = dr["POLICY_NUMBER"].ToString();

						strBuilder.Append("</tr>");
					
					}				
					strBuilder.Append("<tr>");
					strBuilder.Append("<td colspan='8' class='DataGridRow' align='Right'><span class='labelfont'>"+"Total Due"+"</span></td>");
					strBuilder.Append("<td class='DataGridRow' align='Right'><span class='labelfont'>"+PolicyDueAmount.ToString("N")+"</span></td>");
					strBuilder.Append("</tr>");
					//PolicyDueAmount = 0;
				}
				else
				{
					//strBuilder.Append("<table>");
					strBuilder.Append("<tr>");
					strBuilder.Append("<td class='DataGridRow' align='Left' colspan='6'>No Record Found.</td>");
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

			strBuilder.Append("<td id='buttons' colSpan='8'>");

			strBuilder.Append("<span id='spn_Button'>");

			strBuilder.Append("<table cellSpacing='0' cellPadding='2' width='100%'>");
			strBuilder.Append("<tr class='tableFooter'>");
			strBuilder.Append("<td height='24' class='midcolora'><INPUT class='clsButton' onclick='JavaScript:showPrint();' type='button' value='Print'>");
			strBuilder.Append("<!-- <a href='JavaScript:showPrint()'><img src='../images/Print_blue.gif' border='0' WIDTH='78' HEIGHT='24'></a> --></td>");
			strBuilder.Append("<td height='24' class='midcolorr'>");
			strBuilder.Append("<!-- <a href='JavaScript:window.close()'><img src='../images/close_blue.gif' border='0' WIDTH='78' HEIGHT='24'></a> --></td>");
			
			strBuilder.Append("<td class='midcolorr'>");
			strBuilder.Append("<B>Net Balance :</B>");
			strBuilder.Append("</td>");

			strBuilder.Append("</tr>");
			strBuilder.Append("</table>");

			strBuilder.Append("</span>");
			strBuilder.Append("</td>");

			strBuilder.Append("<td class='midcolorr'><B>");
			strBuilder.Append(RunningBalance.ToString("N"));
			strBuilder.Append("</B></td>");
			
			strBuilder.Append("</tr>");		
		
			
			strBuilder.Append("</TD></TR></table>");

			tdAgencyStatementTD.InnerHtml = strBuilder.ToString();
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
			this.btnSEARCH_POLICYNUMBER.Click += new System.EventHandler(this.btnSEARCH_POLICYNUMBER_Click);
			this.btnGOTO.Click += new System.EventHandler(this.btnGOTO_Click);
			this.btnPrevious.Click += new System.Web.UI.ImageClickEventHandler(this.btnPrevious_Click);
			this.btnNext.Click += new System.Web.UI.ImageClickEventHandler(this.btnNext_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		

		private void btnPrevious_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			CurrentPageIndex--;
			hidpolCount.Value ="1";
			// get total no of records 
			CreateTable(intCustomerID);
			SetImages();
			//txtGOTO.Text="";
		}

		private void btnNext_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			CurrentPageIndex ++;
			hidpolCount.Value ="1";
			// Get total no of records against the customer
			CreateTable(intCustomerID);		
			SetImages();
		}

		private void btnGOTO_Click(object sender, System.EventArgs e)
		{
			if(txtGOTO.Text.Trim() != "" & txtGOTO.Text.Trim() != "0")
			{							
				CurrentPageIndex = Convert.ToInt32(txtGOTO.Text);
				hidpolCount.Value ="1";
				CreateTable(intCustomerID);	
				SetImages();
			}
		}

		private void btnSEARCH_POLICYNUMBER_Click(object sender, System.EventArgs e)
		{
		
		}

		
//
//		private void btnPrevious_Click(object sender, System.EventArgs e)
//		{
//			try
//			{
//				ViewState["CurrentPageIndex"] = Convert.ToInt32(ViewState["CurrentPageIndex"]) - 1;
//
//				if (Convert.ToInt32(ViewState["CurrentPageIndex"]) < 1)
//					ViewState["CurrentPageIndex"] = "1";
//
//				CreateTable(int.Parse(Request.Params["CUSTOMER_ID"].ToString()));
//			}
//			catch(Exception ex)
//			{
//				throw ex;
//			}
//			
//		}
//
//		private void btnNext_Click(object sender, System.EventArgs e)
//		{
//			try
//			{
//				ViewState["CurrentPageIndex"] = Convert.ToInt32(ViewState["CurrentPageIndex"]) + 1;
//				CreateTable(int.Parse(Request.Params["CUSTOMER_ID"].ToString()));
//			}
//			catch(Exception ex)
//			{
//				throw ex;
//			}
//		
//		}
	}
}
