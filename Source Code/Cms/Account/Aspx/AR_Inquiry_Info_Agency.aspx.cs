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
using Cms.BusinessLayer.BlCommon;
using System.Web.Security;
using System.Security.Cryptography;
using System.IO; 


namespace Account.Aspx
{
	/// <summary>
	/// Summary description for AR_Inquiry_Info_Agency.
	/// </summary>
    public class AR_Inquiry_Info_Agency : Cms.Account.AccountBase
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.TextBox txtPolicyNo;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPolicyNo;
		protected System.Web.UI.WebControls.Button btnClose;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnPOLICY_NO;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdArReport;
		protected System.Web.UI.WebControls.Button btnSave;
		protected System.Web.UI.HtmlControls.HtmlForm AR_INQUIRY;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICYINFO;
		//vars
		public string callingCustomerId="";
		public string callingAgencyId="";
		public string strPolicyNo="";
		public string calledfrom = "";
		public string URL="";
		protected System.Web.UI.WebControls.TextBox txtAgencyCode;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAgencyCode;
		protected System.Web.UI.WebControls.DropDownList cmbTransYr;
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			btnClose.Attributes.Add("onclick","javascript:window.close();");
			URL = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL();

			if(txtAgencyCode.Text!="")
			 callingAgencyId = txtAgencyCode.Text.ToString().Trim();
			if(txtPolicyNo.Text!="")
			 strPolicyNo  = txtPolicyNo.Text.ToString().Trim();

						

			if(!Page.IsPostBack)
			{
				rfvPolicyNo.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("485");
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSave_Click(object sender, System.EventArgs e)
		{

			if(txtAgencyCode.Text!="")
				callingAgencyId = txtAgencyCode.Text.ToString().Trim(); //Combined Agency Code
			if(txtPolicyNo.Text!="")
				strPolicyNo  = txtPolicyNo.Text.ToString().Trim();

			CreateTable(strPolicyNo ,callingAgencyId);

		}
		public void CreateTable(string PolicyNumber , string AgencyCode)
		{
			double RunningBalance = 0;	
			lblMessage.Visible =false;
			int trans = 0;
			DateTime PastTrans;

			StringBuilder strBuilder = new StringBuilder();
			
			DataSet ds = null;
			Cms.BusinessLayer.BlAccount.ClsDeposit objAccount = new Cms.BusinessLayer.BlAccount.ClsDeposit();
			
			PastTrans = DateTime.Now;
			
			PastTrans = Convert.ToDateTime(Convert.ToString(PastTrans.Month) + "//" + Convert.ToString(PastTrans.Day) + "//" + Convert.ToString(PastTrans.Year - 0 - 1));

			//Get Agency Code for Agency Combined Code
			DataSet dsAgency = null;
			dsAgency = objAccount.GetAgencyCodeForAgency(PolicyNumber,AgencyCode);
			if (dsAgency.Tables.Count != 0)
			{
				//Get The Agency Information of Policy
				if (dsAgency.Tables[0].Rows.Count > 0 )
					AgencyCode = dsAgency.Tables[0].Rows[0]["AGENCY_CODE"].ToString().Trim();
			}


			ds=objAccount.GetArInfo(0,0,PolicyNumber,PastTrans,AgencyCode,languageId);
			
			if (ds.Tables.Count != 0)
			{
				//Get The General Information of Policy
				if (ds.Tables[0].Rows.Count <=0 )
				{
					//lblMessage.Text ="Either Policy Number is not valid Or Policy does not belong to this agency.";
					lblMessage.Text ="Either Policy Number is not valid Or Combined Agency Code is Invalid.";
					lblMessage.Visible =true;
					tdArReport.Visible = false;
					ds = null;
					return;
				}
				else
				{tdArReport.Visible = true;}
			}
			else
			{
				
				lblMessage.Text ="Please Enter a Valid Policy Number.";
				lblMessage.Visible =true;
				tdArReport.Visible = false;
				ds = null;
				dsAgency = null;
				return;
			}
			DataTable dt=ds.Tables[0];
			
			//	strBuilder.Append("<table cellSpacing='1' cellPadding='1' border='0' width='100%' align='center'>");
				
			strBuilder.Append("<tr height='20'>");
			strBuilder.Append("<td width='9%' class='DataGridRow' align='Left'><b>Policy Number</b></td>");

			strBuilder.Append("<td width='9%' class='DataGridRow' align='Left'><b>Policy Term</b></td>");
			strBuilder.Append("<td width='10%' class='DataGridRow' align='Left'><b>Insured Name</b></td>");
				
			strBuilder.Append("<td width='20%' class='DataGridRow' align='Left'><b>Agency</b></td>");
			strBuilder.Append("<td width='10%' class='DataGridRow' align='Left'><b>Agency Code</b></td>");
			strBuilder.Append("<td width='14%' class='DataGridRow' align='Left'><b>Line of Business</b></td>");
			strBuilder.Append("<td width='12%' class='DataGridRow' align='Left'><b>Payment Plan</b></td>");
			strBuilder.Append("<td width='70%' class='DataGridRow' align='Left' colspan='3'><b>Status</b></td>");
				
			strBuilder.Append("</tr>");
				
			strBuilder.Append("<td class='DataGridRow' align='Left'>"+dt.Rows[0]["POLICY_NO"]+"</td>");
			strBuilder.Append("<td class='DataGridRow' align='Left'>"+dt.Rows[0]["APP_TERM"]+"</td>");
							
			strBuilder.Append("<td class='DataGridRow' align='Left'>"+dt.Rows[0]["CUSTOMER_NAME"]+"</td>");
			strBuilder.Append("<td class='DataGridRow' align='Left'>"+dt.Rows[0]["AGEN_NAME"]+"</td>");
			strBuilder.Append("<td class='DataGridRow' align='Left'>"+dt.Rows[0]["AGENCY_ID"]+"</td>");
			strBuilder.Append("<td class='DataGridRow' align='Left'>"+dt.Rows[0]["LOB_DESC"]+"</td>");
			strBuilder.Append("<td class='DataGridRow' align='Left'>"+dt.Rows[0]["PMT_PLAN"]+"</td>");
			strBuilder.Append("<td class='DataGridRow' width='70%' align='left' colspan='3'>"+dt.Rows[0]["STATUS"]+"</td>");
			
			string strPolicyType = dt.Rows[0]["POLICY_TYPE"].ToString();
			string strBillType	 = dt.Rows[0]["BILL_TYPE"].ToString();
			
			//Get The General Information of Accounting
			DataTable objData = new DataTable();	
			//			if(ds == null)
			//			{
			//				lblMessage.Visible =true;
			//				//lblMessage.Text = "No transaction has been posted for the selected Policy.";
			//				lblMessage.Text = "No Account Transaction has been Posted for AB Type Policies.";
			//				return;
			//			}
			//			else
			//			{
			//				if (ds.Tables.Count == 0)
			//				{
			//					lblMessage.Visible =true;
			//					lblMessage.Text =  "No Account Transaction has been Posted for AB Type Policies.";
			//					return;
			//				}
			//
			//			}

			if(strBillType.Equals("AB"))
			{
				if(ds.Tables[7].Rows.Count == 0)
				{
					lblMessage.Text    =  "No Account Transaction has been Posted for AB Type Policies.";
					//"No Account Transaction has been Posted for AB Type Policies. There are no associated policies for the selected customer.";
					lblMessage.Visible =  true;
					//	return;
				}
				else
				{
					if(ds.Tables[0].Rows[0]["RENEWED"].ToString() != "YES")
					{
						lblMessage.Text    =  "No Account Transaction has been Posted for AB Type Policies.";
						lblMessage.Visible =  true;
					}
					//	return;
				}

				
			}
			else if(ds.Tables[7].Rows.Count == 0)
			{
				//lblMessage.Text    =  "There are no associated policies for the selected policy.";
				lblMessage.Text    =  "";//"There is no policy for the selected customer.";
				lblMessage.Visible =  true;
				//	return;
			}

			
			//			if (ds.Tables.Count == 0)
			//			{
			//				lblMessage.Visible =true;
			//				lblMessage.Text =  lblMessage.Text + "/n No associated policies for the selected policy.";
			//				return;
			//			}

			objData=ds.Tables[1]; 
		
			//
			//			if (objData == null)
			//			{
			//				lblMessage.Visible =true;
			//				//lblMessage.Text = "No transaction has been posted for the selected policy.";
			//				lblMessage.Text = "No Account Transaction has been Posted for AB Type Policies.";
			//				trans = 1;
			////				tdArReport.InnerHtml = strBuilder.ToString();
			////				return;
			//			}
			//			else
			//			{
			//				if (objData.Rows.Count == 0)
			//				{
			//					lblMessage.Visible =true;
			//					lblMessage.Text = "No Account Transaction has been Posted for AB Type Policies.";
			//					trans = 1;
			////					tdArReport.InnerHtml = strBuilder.ToString();
			////					return;
			//				}
			//			}

			if(trans == 0)
			{
				strBuilder.Append("<tr height='20'>");
				strBuilder.Append("<tr height='20'>");
				strBuilder.Append("<td class='DataGridRow' align='Left' width='9%'><b>Transaction Date</b></td>");
				strBuilder.Append("<td class='DataGridRow' align='Left' width='9%'><b>Effective Date</b></td>");
				strBuilder.Append("<td class='DataGridRow' align='Left' width='9%'><b>Due Date</b></td>");
				strBuilder.Append("<td class='DataGridRow' align='Left' width='20%'><b>Description</b></td>");
				strBuilder.Append("<td class='DataGridRow' align='Left' width='10%'><b>Total Amount</b></td>");
				strBuilder.Append("<td class='DataGridRow' align='Left' width='12%'><b>Premium Amount</b></td>");
				strBuilder.Append("<td class='DataGridRow' align='Left' width='8%'><b>Total Fee</b></td>");
				strBuilder.Append("<td class='DataGridRow' align='Left' width='20%' colspan='4'><b>Adjusted Policy Amount</b></td>");
				strBuilder.Append("</tr>");
				try
				{				
					if(objData!=null)
					{
						int i=0;
						foreach(DataRow dr in objData.Rows)
						{
							i++;
							strBuilder.Append("<tr height='20'>");
							strBuilder.Append("<td class='DataGridRow' align='Left'>" + dr["SOURCE_TRAN_DATE"] + "</td>");
							strBuilder.Append("<td class='DataGridRow' align='Left'>" + dr["SOURCE_EFF_DATE"] + "</td>");
							strBuilder.Append("<td class='DataGridRow' align='Left' >" + dr["POSTING_DATE"] + "</td>");
							strBuilder.Append("<td class='DataGridRow' align='Left' >" + dr["Description"] ); // + "</td>");
							if(dr["NOTICE_URL"] != DBNull.Value && Convert.ToString(dr["NOTICE_URL"]) != "NA")
							{
								strBuilder.Append("&nbsp;&nbsp;<img id='imgNotice_" + i.ToString() +"' title='Open Notice' onClick=\"window.open('");
								strBuilder.Append(Convert.ToString(dr["NOTICE_URL"]));
								strBuilder.Append("')\" src='../../cmsweb/Images1/Rule_ver.gif' alt='' align='AbsMiddle' style='border-width:0px;border-style:None;height:15px;CURSOR:hand' />");
								
							}
							strBuilder.Append("</td>");
							if(dr["TOTAL_AMOUNT"] != DBNull.Value )
							{
								strBuilder.Append("<td class='DataGridRow' align='Right' >" +Convert.ToDouble(dr["TOTAL_AMOUNT"]).ToString("N") + "</td>");

							}
							else
							{
								strBuilder.Append("<td class='DataGridRow' align='Right' >" +dr["TOTAL_AMOUNT"] + "</td>");
							}
						
							//if(dr["TYPE"].ToString()  != "P")
							//{
							
							if(dr["TOTAL_PREMIUM"]!=DBNull.Value )
							{
								strBuilder.Append("<td class='DataGridRow' align='Right' >" + Convert.ToDouble(dr["TOTAL_PREMIUM"]).ToString("N") + "</td>");
								RunningBalance+=Convert.ToDouble(dr["TOTAL_PREMIUM"].ToString());
							}
							else
							{
								strBuilder.Append("<td class='DataGridRow' align='Right' >" + dr["TOTAL_PREMIUM"] + "</td>");
							}
							/*}
							else
							{
							
								if(dr["TOTAL_AMOUNT"] != DBNull.Value)
								{
									strBuilder.Append("<td class='DataGridRow' align='Right' >" + Convert.ToDouble(dr["TOTAL_AMOUNT"]).ToString("N") + "</td>");
									RunningBalance+=Convert.ToDouble(dr["TOTAL_AMOUNT"].ToString());
								}
								else
								{
									strBuilder.Append("<td class='DataGridRow' align='Right' >" + dr["TOTAL_AMOUNT"] + "</td>");
								}

							}*/
						
							if(dr["TYPE"].ToString()  != "P")
							{
								if(dr["TOTAL_FEE"]!=DBNull.Value)
								{
									if( Convert.ToDouble(dr["TOTAL_FEE"]) != 0)
									{
										//strBuilder.Append("<td class='DataGridRow' align='Right' ><A href='#' onclick=\"window.open('ShowARFee.aspx?CALLED_FROM=F&INSF_FEE=" +  dr["INSF_FEE"].ToString()  + "&LATE_FEE=" + dr["LATE_FEE"].ToString() + "&REINS_FEE=" + dr["REINS_FEE"].ToString() + "&S_DATE=" + dr["SOURCE_TRAN_DATE"].ToString() + "&E_DATE=" + dr["SOURCE_EFF_DATE"].ToString() + "&NSF_FEE=" + dr["NSF_FEE"].ToString() + "','','height=400, width=400,left=100px;top=20px;status= no, resizable= no, scrollbars=no, toolbar=no,location=no,menubar=no,left=20px;top=20px')\">" + Convert.ToDouble(dr["TOTAL_FEE"]).ToString("N") + "</a></td>");
										strBuilder.Append("<td class='DataGridRow' align='Right' >" + dr["TOTAL_FEE"] + "</td>");
										//strBuilder.Append("<td class='DataGridRow' align='Left' > </td>");
										//strBuilder.Append("<td class='DataGridRow' WIDTH='25%' align='left' ></td>");
									}
									else
									{
										strBuilder.Append("<td class='DataGridRow' align='Left' > </td>");
										//	strBuilder.Append("<td class='DataGridRow' WIDTH='25%' align='left' ></td>");
									}
								}
								else
								{
									strBuilder.Append("<td class='DataGridRow' align='Left' > </td>");
									//strBuilder.Append("<td class='DataGridRow' WIDTH='25%' align='left' ></td>");
								}
								if(dr["ADJUSTED"]!=DBNull.Value && dr["ADJUSTED"].ToString().Trim() != "0" )
								{
									string Type = dr["TYPE"].ToString();
									//strBuilder.Append("<td class='DataGridRow' align='Left' > </td>");
									//strBuilder.Append("<td class='DataGridRow'  WIDTH='25%' colspan='4' align='Right'>"  + dr["ADJUSTED"] + "</td>");
									strBuilder.Append("<td class='DataGridRow'  WIDTH='25%' colspan='4' align='Right'><A href='#' onclick=\"window.open('ShowARFee.aspx?CALLED_FROM="+ Type +  "&OP_ID=" +  dr["OPEN_ITEM_ID"].ToString()  + "&POL_ID=" + dr["POL_ID"].ToString() + "','','height=400, width=400,left=100px;top=20px;status= no, resizable= no, scrollbars=no, toolbar=no,location=no,menubar=no,left=20px;top=20px')\">" 
										+ dr["ADJUSTED"] + "</a></td>");
									//	strBuilder.Append("<td class='DataGridRow' align='Left' > </td>");
								}
								else
								{
									strBuilder.Append("<td class='DataGridRow' align='Left' colspan='4' > </td>");
								}

							}
							else
							{
								strBuilder.Append("<td class='DataGridRow' align='Left' > </td>");
								strBuilder.Append("<td class='DataGridRow' WIDTH='25%' colspan='4' align='Left'></td>");

							}
							
							strBuilder.Append("</tr>");
						
					
						}				
						strBuilder.Append("<tr>");
						strBuilder.Append("<td colspan='4' class='DataGridRow' align='Right'><span class='labelfont'>"+"Total Due"+"</span></td>");
						strBuilder.Append("<td class='DataGridRow'></td>");
						strBuilder.Append("<td class='DataGridRow' align='Right' ><span class='labelfont'>"+RunningBalance.ToString("N")+"</span></td>");
						strBuilder.Append("<td class='DataGridRow' COLSPAN='5' WIDTH='25%' align='left' ></td>");

					
					
						strBuilder.Append("</tr>");
					
						//					tdArReport.InnerHtml =strBuilder.ToString();
					}
					else
					{
						//strBuilder.Append("<table>");
						strBuilder.Append("<tr>");
						strBuilder.Append("<td class='DataGridRow' align='Left' colspan='6'>No Record Found.</td>");
						strBuilder.Append("</tr>");
						//					tdArReport.InnerHtml = strBuilder.ToString();
						return;
					}
				}
				catch(Exception ex)
				{
					throw ex;
				}
			}

			// Added Mohit Agarwal 27-Oct-2006
			DataTable add1 = new DataTable();

			add1 = ds.Tables[2];
			strBuilder.Append("<tr height='20'>");
			strBuilder.Append("<td align='Right' width='15%'><b>&nbsp;</b></td>");
			strBuilder.Append("</tr>");

			if(add1 != null && add1.Rows.Count != 0)
			{
				strBuilder.Append("<tr height='20'>");
				strBuilder.Append("<td class='DataGridRow' align='Left' colspan='2' ><b>Insured Mailing Address: </b></td>");
				//	strBuilder.Append("</tr>");

				//	strBuilder.Append("<tr height='20'>");
				//	strBuilder.Append("<td class='DataGridRow' align='Right' colspan='2'>&nbsp;</td>");
				strBuilder.Append("<td class='DataGridRow' align='Left' colspan='8'>"+dt.Rows[0]["CUSTOMER_NAME"]+"</td>");
				strBuilder.Append("</tr>");
				strBuilder.Append("<tr height='20'>");
				strBuilder.Append("<td class='DataGridRow' align='Right' colspan='2'><b>&nbsp;</b></td>");
				if(add1.Rows[0]["CUSTOMER_ADDRESS2"].ToString() != "" && add1.Rows[0]["CUSTOMER_ADDRESS2"]!= System.DBNull.Value)
					strBuilder.Append("<td class='DataGridRow' align='Left' colspan='8'>"+add1.Rows[0]["CUSTOMER_ADDRESS1"]+", "+add1.Rows[0]["CUSTOMER_ADDRESS2"]+"</td>");
				else
					strBuilder.Append("<td class='DataGridRow' align='Left' colspan='8'>"+add1.Rows[0]["CUSTOMER_ADDRESS1"]+"</td>");
				strBuilder.Append("</tr>");

				strBuilder.Append("<tr height='20'>");
				strBuilder.Append("<td class='DataGridRow' align='Right' colspan='2'><b>&nbsp;</b></td>");
				strBuilder.Append("<td class='DataGridRow' align='Left' colspan='1'>"+add1.Rows[0]["CUSTOMER_CITY"]+"</td>");
				strBuilder.Append("<td class='DataGridRow' align='Left' colspan='7'>"+add1.Rows[0]["CUSTOMER_STATE"]+" "+add1.Rows[0]["CUSTOMER_ZIP"]+"</td>");
				strBuilder.Append("</tr>");

				strBuilder.Append("<tr height='20'>");
				strBuilder.Append("<td align='Right' width='15%'><b>&nbsp;</b></td>");
				strBuilder.Append("</tr>");
			}

			DataTable add2 = new DataTable();

			add2 = ds.Tables[3];

			int cover = 0;

			if(add2 != null && add2.Rows.Count != 0 && (add2.Rows[0]["LOB_ID"].ToString() == "1" ||add2.Rows[0]["LOB_ID"].ToString() == "6"))
			{
				strBuilder.Append("<tr height='20'>");
				strBuilder.Append("<td class='DataGridRow' align='Left' colspan='2'><b>"+add2.Rows[0]["LOC_TYPE"]+"</b></td>");
				//strBuilder.Append("</tr>");

			
				//strBuilder.Append("<td class='DataGridRow' align='Left' colspan='8'>&nbsp;</td>");
				//	strBuilder.Append("</tr>");
				//	strBuilder.Append("<tr height='20'>");
				if(add2.Rows[0]["LOC_ADD1"].ToString().Trim() == "")
					strBuilder.Append("<td class='DataGridRow' align='Left' colspan='8'>"+add2.Rows[0]["LOC_ADD2"]+"</td>");
				else
				{
					if(add2.Rows[0]["LOC_ADD2"].ToString().Trim()!="")
						strBuilder.Append("<td class='DataGridRow' align='Left' colspan='8'>"+add2.Rows[0]["LOC_ADD1"]+", "+add2.Rows[0]["LOC_ADD2"]+"</td>");
					else
						strBuilder.Append("<td class='DataGridRow' align='Left' colspan='8'>"+add2.Rows[0]["LOC_ADD1"]+"</td>");

				}

				strBuilder.Append("</tr>");

				strBuilder.Append("<tr height='20'>");
				strBuilder.Append("<td class='DataGridRow' align='Right' colspan='2'><b>&nbsp;</b></td>");
				strBuilder.Append("<td class='DataGridRow' align='Left' colspan='1'>"+add2.Rows[0]["LOC_CITY"]+"</td>");
				strBuilder.Append("<td class='DataGridRow' align='Left' colspan='7'>"+add2.Rows[0]["LOC_STATE"]+" "+add2.Rows[0]["LOC_ZIP"]+"</td>");
				strBuilder.Append("</tr>");

				strBuilder.Append("<tr height='20'>");
				strBuilder.Append("<td align='Right' width='15%'><b>&nbsp;</b></td>");
				strBuilder.Append("</tr>");
				cover = 1;
			}

			DataTable cov = ds.Tables[4];
			int intCov = 0;
			string strCov = "";
			
			
			if(cov != null && cov.Rows.Count != 0 && cover == 1)
			{
				cover = 0;
				// Coverage 'C' will only be shown for Policy Type : HO-4 & HO-6
				if(strPolicyType == HomeProductType.HO6_UNIT || strPolicyType == HomeProductType.HO4_TENANT)
				{
					if(cov.Rows[0]["COV_DESC"].ToString().Trim() != "")
					{
						cover = 1;
						//Convert to Currency : 
						intCov = int.Parse(cov.Rows[0]["LIMITC"].ToString());
						strCov = String.Format("{0:c}",intCov);
						strBuilder.Append("<tr height='20'>");
						strBuilder.Append("<td class='DataGridRow' align='Left' colspan='2'><b>"+cov.Rows[0]["COV_DESC"]+"</b></td>");
						strBuilder.Append("<td class='DataGridRow' align='Left' colspan='8'>"+ strCov +"</td>");
						strBuilder.Append("</tr>");
					}
				}
				else // Coverage 'A' will only be shown for Policy Types other than : HO-4 & HO-6
				{
					if(cov.Rows[0]["COV_DESA"].ToString().Trim() != "")
					{
						cover = 1;
						//Convert to Currency : 
						intCov = int.Parse(cov.Rows[0]["LIMITA"].ToString());
						strCov = String.Format("{0:c}",intCov);
						strBuilder.Append("<tr height='20'>");
						strBuilder.Append("<td class='DataGridRow' align='Left' colspan='2'><b>"+cov.Rows[0]["COV_DESA"]+"</b></td>");
						strBuilder.Append("<td class='DataGridRow' align='Left' colspan='8'>"+ strCov +"</td>");
						strBuilder.Append("</tr>");
					}
				}

				if(cov.Rows[0]["DEDUC_DESC"].ToString().Trim() != "")
				{
					cover = 1;
					//Convert to Currency : 
					intCov = int.Parse(cov.Rows[0]["DEDUCTIBLE"].ToString());
					strCov = String.Format("{0:c}",intCov);
					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td class='DataGridRow' align='Left' colspan='2'><b>"+cov.Rows[0]["DEDUC_DESC"]+"</b></td>");
					strBuilder.Append("<td class='DataGridRow' align='Left' colspan='8'>"+ strCov +"</td>");
					strBuilder.Append("</tr>");
				}

				if(cover == 1)
				{
					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td align='Right' width='15%'><b>&nbsp;</b></td>");
					strBuilder.Append("</tr>");
				}
			}

			DataTable bill_desc = ds.Tables[5];

			if(bill_desc != null && bill_desc.Rows.Count != 0)
			{
				strBuilder.Append("<tr height='20'>");
				strBuilder.Append("<td  colspan='2' class='DataGridRow' align='Left'><b>Billed To: </b></td>");
				//	strBuilder.Append("</tr>");

				//	strBuilder.Append("<tr height='20'>");
				//	strBuilder.Append("<td class='DataGridRow' align='Left' colspan='2'>&nbsp;</td>");
				strBuilder.Append("<td class='DataGridRow' align='Left' colspan='8'>"+bill_desc.Rows[0]["LOOKUP_VALUE_DESC"]+"</td>");
				strBuilder.Append("</tr>");

				strBuilder.Append("<tr height='20'>");
				strBuilder.Append("<td align='Right' width='15%'><b>&nbsp;</b></td>");
				strBuilder.Append("</tr>");
			}

			DataTable add3 = new DataTable();

			add3 = ds.Tables[6];

			if(add3 != null && add3.Rows.Count != 0 && add3.Rows[0]["HOLDER_TYPE"].ToString() != "")
			{
				strBuilder.Append("<tr height='20'>");
				strBuilder.Append("<td class='DataGridRow' align='Left' colspan='2'><b>"+add3.Rows[0]["HOLDER_TYPE"]+"</b></td>");
				strBuilder.Append("<td class='DataGridRow' align='Left' colspan='2'><b>Holder Name</b></td>");
				strBuilder.Append("<td class='DataGridRow' align='Left' colspan='8'><b>Holder Address</b></td>");
				//strBuilder.Append("<td class='DataGridRow' align='Left' colspan='8'>"+add3.Rows[0]["HOLDER_NAME"]+"</td>");
				strBuilder.Append("</tr>");
				try
				{				
					foreach(DataRow dr in add3.Rows)
					{
						strBuilder.Append("<tr height='20'>");
						strBuilder.Append("<td class='DataGridRow' align='Left' colspan='2'><b>&nbsp;</b></td>");
						strBuilder.Append("<td class='DataGridRow' align='Left' colspan='2'>"+dr["HOLDER_NAME"]+"</td>");
						if(dr["HOLDER_ADD1"] != System.DBNull.Value && dr["HOLDER_ADD1"].ToString() != "" && dr["HOLDER_ADD2"] != System.DBNull.Value && dr["HOLDER_ADD2"].ToString() != "")
							strBuilder.Append("<td class='DataGridRow' align='Left' colspan='6'>"+dr["HOLDER_ADD1"]+", "+dr["HOLDER_ADD2"]+"</td>");
						else if(dr["HOLDER_ADD1"] != System.DBNull.Value && dr["HOLDER_ADD1"].ToString() != "")
							strBuilder.Append("<td class='DataGridRow' align='Left' colspan='6'>"+dr["HOLDER_ADD1"]+"</td>");
						else if(dr["HOLDER_ADD2"] != System.DBNull.Value && dr["HOLDER_ADD2"].ToString() != "")
							strBuilder.Append("<td class='DataGridRow' align='Left' colspan='6'>"+dr["HOLDER_ADD2"]+"</td>");
						strBuilder.Append("</tr>");

						strBuilder.Append("<tr height='20'>");
						strBuilder.Append("<td class='DataGridRow' align='Right' colspan='4'><b>&nbsp;</b></td>");
						strBuilder.Append("<td class='DataGridRow' align='Left' colspan='1'>"+dr["HOLDER_CITY"]+"</td>");
						strBuilder.Append("<td class='DataGridRow' align='Left' colspan='5'>"+dr["HOLDER_STATE"]+" "+dr["HOLDER_ZIP"]+"</td>");
						strBuilder.Append("</tr>");
					}
					

					//				strBuilder.Append("<tr height='20'>");
					//				strBuilder.Append("<td class='DataGridRow' align='Right' colspan='2'><b>&nbsp;</b></td>");
					//				strBuilder.Append("<td class='DataGridRow' align='Left' colspan='1'>"+add3.Rows[0]["HOLDER_CITY"]+"</td>");
					//				strBuilder.Append("<td class='DataGridRow' align='Left' colspan='7'>"+add3.Rows[0]["HOLDER_STATE"]+" "+add3.Rows[0]["HOLDER_ZIP"]+"</td>");
					//				strBuilder.Append("</tr>");

					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td align='Right' width='15%'><b>&nbsp;</b></td>");
					strBuilder.Append("</tr>");
				}
				catch(Exception ex)
				{
					throw ex;
				}
			}

			//Commented By Ravindra (March 05 2007) 
			//Why to have to datasets for Additional interest can fetch in one recordset only
			/*DataTable add4 = new DataTable();

			add4 = ds.Tables[7];

			if(add4 != null && add4.Rows.Count != 0 && add4.Rows[0]["HOLDER_TYPE"].ToString() != "")
			{
				strBuilder.Append("<tr height='20'>");
				strBuilder.Append("<td class='DataGridRow' align='Right' colspan='10'><b>"+add4.Rows[0]["HOLDER_TYPE"]+"</b></td>");
				strBuilder.Append("</tr>");

				strBuilder.Append("<tr height='20'>");
				strBuilder.Append("<td class='DataGridRow' align='Left' colspan='2'>&nbsp;</td>");
				if(add4.Rows[0]["HOLDER_ADD1"].ToString().Trim() == "")
					strBuilder.Append("<td class='DataGridRow' align='Left' colspan='8'>"+add4.Rows[0]["HOLDER_ADD2"]+"</td>");
				else
					strBuilder.Append("<td class='DataGridRow' align='Left' colspan='8'>"+add4.Rows[0]["HOLDER_ADD1"]+", "+add4.Rows[0]["HOLDER_ADD2"]+"</td>");

				strBuilder.Append("</tr>");

				strBuilder.Append("<tr height='20'>");
				strBuilder.Append("<td class='DataGridRow' align='Right' colspan='2'><b>&nbsp;</b></td>");
				strBuilder.Append("<td class='DataGridRow' align='Left' colspan='1'>"+add4.Rows[0]["HOLDER_CITY"]+"</td>");
				strBuilder.Append("<td class='DataGridRow' align='Left' colspan='7'>"+add4.Rows[0]["HOLDER_STATE"]+" "+add4.Rows[0]["HOLDER_ZIP"]+"</td>");
				strBuilder.Append("</tr>");

				strBuilder.Append("<tr height='20'>");
				strBuilder.Append("<td align='Right' width='15%'><b>&nbsp;</b></td>");
				strBuilder.Append("</tr>");
			}*/


			/*DataTable assocPol = new DataTable();
			assocPol=ds.Tables[8]; */

			DataTable assocPol = assocPol=ds.Tables[7]; 


			// Added Mohit Agarwal 26-Oct-2006
			//Commented on 4 April 2008 
			/*
			strBuilder.Append("<tr height='20'>");
			strBuilder.Append("<td class='DataGridRow' align='Left' width='15%'><b>Other Policies of </b></td>");
			strBuilder.Append("<td class='DataGridRow' align='Left' colspan='9'><b>" + dt.Rows[0]["CUSTOMER_NAME"] + "</b></td>");

			strBuilder.Append("</tr>");

			strBuilder.Append("<tr height='20'>");
			strBuilder.Append("<td class='DataGridRow' align='Left' width='9%'><b>Policy Number</b></td>");

			strBuilder.Append("<td class='DataGridRow' align='Left' width='9%'><b>Policy Term</b></td>");
			strBuilder.Append("<td class='DataGridRow' align='Left' width='10%'><b>Insured Name</b></td>");
			
			strBuilder.Append("<td class='DataGridRow' align='Left' width='20%'><b>Agency</b></td>");
			//strBuilder.Append("<td class='DataGridRow' align='Left' width='10%'><b>Agency Code</b></td>");
			strBuilder.Append("<td class='DataGridRow' align='Left' width='10%'><b>Policy Status</b></td>");
			strBuilder.Append("<td class='DataGridRow' align='Left' width='14%'><b>Line of Business</b></td>");
			strBuilder.Append("<td class='DataGridRow' align='Left' colspan='4'><b>Payment Plan</b></td>");
			//strBuilder.Append("<td class='DataGridRow' align='left' COLSPAN='4' WIDTH='25%'><b>Status</b></td>");
			
			strBuilder.Append("</tr>");
			*/

			
		
			//
			//			if (assocPol == null)
			//			{
			//				lblMessage.Visible =true;
			//				lblMessage.Text = "No associated policies for the selected policy.";
			//				tdArReport.InnerHtml = strBuilder.ToString();
			//				return;
			//			}
			//			else
			//			{
			//				if (assocPol.Rows.Count == 0)
			//				{
			//					lblMessage.Visible =true;
			//					lblMessage.Text =  "No associated policies for the selected policy.";
			//					tdArReport.InnerHtml = strBuilder.ToString();
			//					return;
			//				}
			//			}			
			
			try
			{				
				if(assocPol!=null)
				{
					/*foreach(DataRow dr in assocPol.Rows)
					{
						if(dt.Rows[0]["POLICY_NO"].ToString() != dr["POLICY_NO"].ToString())
						{
							strBuilder.Append("<td class='DataGridRow' align='Left'><a style='CURSOR: hand;color: #0000FF' onclick='ShowAssoc(\"" + dr["POLICY_NO"] + "\")'>" + dr["POLICY_NO"]+ " </a></td>");
							//strBuilder.Append("<td class='DataGridRow' align='Left'><img style='CURSOR: hand' src='../../cmsweb/Images1/blank.gif' alt="+ dr["POLICY_NO"] + " onclick='ShowAssoc(\"" + dr["POLICY_NO"] + "\")'></img></td>");
							strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["APP_TERM"]+"</td>");
						
							strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["CUSTOMER_NAME"]+"</td>");
							strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["AGEN_NAME"]+"</td>");
							//strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["AGENCY_ID"]+"</td>");
							strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["POLICY_STATUS"]+"</td>");
							strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["LOB_DESC"]+"</td>");
							strBuilder.Append("<td class='DataGridRow' align='Left' colspan='4'>"+dr["PMT_PLAN"]+"</td>");
							//strBuilder.Append("<td class='DataGridRow' COLSPAN='4' WIDTH='25%' align='left'>"+dt.Rows[0]["STATUS"]+"</td>");
							strBuilder.Append("</tr>");
						}
					}*/
					
					strBuilder.Append("</tr>");
					
					tdArReport.InnerHtml =strBuilder.ToString();
					calledfrom = Request.QueryString["CalledFrom"];

				}
				else
				{
					//strBuilder.Append("<table>");
					strBuilder.Append("<tr>");
					strBuilder.Append("<td class='DataGridRow' align='Left' colspan='6'>No Record Found.</td>");
					strBuilder.Append("</tr>");
					tdArReport.InnerHtml = strBuilder.ToString();
					return;
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				ds.Dispose();
			}
				   
		}

	
	}
}
