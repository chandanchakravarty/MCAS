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
using Cms.Model.Account;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb;
using Cms.CmsWeb.Controls;
using Cms.DataLayer;

namespace Cms.CmsWeb.Maintenance.Reinsurance
{
	/// <summary>
	/// Summary description for ReinsuranceReports.
	/// </summary>
	public class ReinsuranceReports : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Label capContractNo;
		protected System.Web.UI.WebControls.Label lblContractNo;
		protected System.Web.UI.WebControls.Label capContractDates;
		protected System.Web.UI.WebControls.Label lblContractDates;
		protected System.Web.UI.WebControls.Label capTypeRein;
		protected System.Web.UI.WebControls.Label lblTypeRein;
		protected System.Web.UI.WebControls.Label capDesc;
		protected System.Web.UI.WebControls.Label lblDesc;
		protected System.Web.UI.WebControls.Label capForPeriod;
		protected System.Web.UI.WebControls.Label lblForPeriod;
		protected System.Web.UI.WebControls.Label capTransCode;
		protected System.Web.UI.WebControls.Label lblTransCode;
		protected System.Web.UI.HtmlControls.HtmlTableRow trSpecialAccep;
		protected System.Web.UI.WebControls.Label capSpecialAccep;
		protected System.Web.UI.WebControls.Label capInsuranceValue;
		protected System.Web.UI.WebControls.Label capAccount;
		protected System.Web.UI.WebControls.Label lblAccount;
		protected System.Web.UI.WebControls.Label capDateGenerated;
		protected System.Web.UI.WebControls.Label lblDateGenerated;
		protected System.Web.UI.WebControls.Label capRequestedBy;
		protected System.Web.UI.WebControls.Label lblRequestedBy;
		protected System.Web.UI.WebControls.Label lblInsuranceValue;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidStringValue;
		protected System.Web.UI.WebControls.DataGrid dgReinReport;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tbDataGrid;
		protected System.Web.UI.WebControls.Label lblDatagrid;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidContract_num;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidtran_type;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidsp_accep;

		protected System.Web.UI.WebControls.Label capTotals;
		protected System.Web.UI.WebControls.Label capSum;
		protected System.Web.UI.WebControls.Label capTotWrittenPrem;
		protected System.Web.UI.WebControls.Label capTotReturnPrem;
		protected System.Web.UI.WebControls.Label capSubTotal;
		protected System.Web.UI.WebControls.Label capLessCommission;
		protected System.Web.UI.WebControls.Label capNetDue;

		protected Cms.CmsWeb.Controls.CmsButton btnDoGeneral;
		protected System.Web.UI.WebControls.Label capGOTO;
		protected System.Web.UI.WebControls.TextBox txtGOTO;
		protected System.Web.UI.WebControls.RegularExpressionValidator revGOTO;
		protected System.Web.UI.WebControls.CustomValidator csvGOTO;
		protected Cms.CmsWeb.Controls.CmsButton btnGOTO;
		protected System.Web.UI.WebControls.ImageButton btnPrevious;
		protected System.Web.UI.WebControls.ImageButton btnNext;	
		protected System.Web.UI.WebControls.Label lblCurrentPage;

		public int CurrentPageIndex
		{
			set { ViewState["CurrentPageIndex"] = value;}
			get{ return Convert.ToInt32(ViewState["CurrentPageIndex"]); }
		}
		public int intTotalPages=0;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			base.ScreenId="";
			#region Setting the properties of CmsButton 

			btnDoGeneral.CmsButtonClass		= CmsButtonType.Execute;
			btnDoGeneral.PermissionString	= gstrSecurityXML;

			btnGOTO.CmsButtonClass	 = CmsButtonType.Execute;
			btnGOTO.PermissionString = gstrSecurityXML;


			#endregion

			if(!IsPostBack)
			{
				if (Request.QueryString["SRTING_VALUE"] != null && Request.QueryString["SRTING_VALUE"].ToString() != "")
				{
					hidStringValue.Value = Request.QueryString["SRTING_VALUE"].ToString();
				}
				FillLabels();
				//page_load ="new";
			}
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
		# region FillLabels
		private void FillLabels()
		{
			string strStringValue=(string)hidStringValue.Value;
			if (strStringValue !="" && strStringValue != "0")
			{
				string[] strStringValues= strStringValue.Split('^'); 
 
				string contract_number = "";
				string contract_dates ="";
				string type_report = "";
				string report ="";
				string month_ending = "";
				string year="";
				string major_part ="";
				string major_desc="";	
				string tran_type="";
				string tot_value_from="";
				string tot_value_to="";
				string sp_accep ="";
				string user_id = "";
				string insu_value ="";

				contract_number = strStringValues[0].ToString();
				contract_dates = strStringValues[1].ToString();
				type_report = strStringValues[2].ToString();
				report = strStringValues[3].ToString();
				month_ending = strStringValues[4].ToString();
				year = strStringValues[5].ToString();
				major_part = strStringValues[6].ToString();
				major_desc = strStringValues[7].ToString();
			
				sp_accep= strStringValues[8].ToString();
				tot_value_from = strStringValues[9].ToString();
				tot_value_to = strStringValues[10].ToString();
				tran_type =strStringValues[11].ToString();
				user_id = strStringValues[12].ToString();
				insu_value = strStringValues[13].ToString();

				hidsp_accep.Value = sp_accep;
				try
				{	
					Cms.BusinessLayer.BlCommon.ClsReinsuranceContact objReinsuranceContact=new Cms.BusinessLayer.BlCommon.ClsReinsuranceContact(); 

					DataSet objDataSet = objReinsuranceContact.FetchReinsurancePremReport(contract_number,contract_dates,type_report,report,month_ending,year,major_part,major_desc,sp_accep,tot_value_from,tot_value_to,tran_type,user_id,insu_value);
					if(objDataSet.Tables.Count>0)
					{
						if(objDataSet.Tables[0].Rows.Count >0 && objDataSet.Tables[0].Rows[0]["REIN_COMAPANY_NAME"] != DBNull.Value)
						{
							lblAccount.Text  = objDataSet.Tables[0].Rows[0]["REIN_COMAPANY_NAME"].ToString ();
						}
						if(objDataSet.Tables[1].Rows.Count >0 && objDataSet.Tables[1].Rows[0]["CONTRACT_NUMBER"] != DBNull.Value)
						{
							lblContractNo.Text  = objDataSet.Tables[1].Rows[0]["CONTRACT_NUMBER"].ToString ();
						}
						if(objDataSet.Tables[2].Rows.Count >0 && objDataSet.Tables[2].Rows[0]["CONTRACT_DATES"] != DBNull.Value)
						{
							lblContractDates.Text  = objDataSet.Tables[2].Rows[0]["CONTRACT_DATES"].ToString ();
						}
						if(objDataSet.Tables[3].Rows.Count >0 && objDataSet.Tables[3].Rows[0]["TYPE_REPORT"] != DBNull.Value)
						{
							lblTypeRein.Text  = objDataSet.Tables[3].Rows[0]["TYPE_REPORT"].ToString ();
						}
						if(objDataSet.Tables[4].Rows.Count >0 && objDataSet.Tables[4].Rows[0]["MAJOR_DESC"] != DBNull.Value)
						{
							lblDesc.Text  = objDataSet.Tables[4].Rows[0]["MAJOR_DESC"].ToString ();
						}
						if(objDataSet.Tables[4].Rows.Count >0 && objDataSet.Tables[4].Rows[0]["FOR_PERIOD"] != DBNull.Value)
						{
							lblForPeriod.Text  = objDataSet.Tables[4].Rows[0]["FOR_PERIOD"].ToString ();
						}
						if(objDataSet.Tables[5].Rows.Count >0 && objDataSet.Tables[5].Rows[0]["TRAN_TYPE"] != DBNull.Value)
						{
							lblTransCode.Text  = objDataSet.Tables[5].Rows[0]["TRAN_TYPE"].ToString ();
						}
						

						lblDateGenerated.Text = DateTime.Now.ToShortDateString();
						if(objDataSet.Tables[6].Rows.Count >0 && objDataSet.Tables[6].Rows[0]["USER_NAME"] != DBNull.Value)
						{
							lblRequestedBy.Text  = objDataSet.Tables[6].Rows[0]["USER_NAME"].ToString ();
						}
						if(objDataSet.Tables[7].Rows.Count >0 && objDataSet.Tables[7].Rows[0]["INSU_VALUE"] != DBNull.Value)
						{
							lblInsuranceValue.Text  = objDataSet.Tables[7].Rows[0]["INSU_VALUE"].ToString ();
						}

//						if(sp_accep.ToString() == "1")
//						{
//							trSpecialAccep.Attributes.Add("Style","display:inline");
//							//trSpecialAccep.Visible = true;	
//
//						}
//						else
//						{
//							trSpecialAccep.Attributes.Add("Style","display:none");
//							//trSpecialAccep.Visible = false;	
//						}
					}
					BindGrid(contract_number,tran_type);
					hidContract_num.Value = contract_number.ToString();
					hidtran_type.Value = tran_type.ToString();

					dgReinReport.CurrentPageIndex =0;
				}
				catch (Exception ex)
				{
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				}

			}
		}
		#endregion


		private void BindGrid(string contract_number, string tran_type)
		{
			try
			{	

				//Cms.BusinessLayer.BlCommon.ClsReinsuranceContact objReinsuranceContact=new Cms.BusinessLayer.BlCommon.ClsReinsuranceContact(); 

				DataSet objDataSet = Cms.BusinessLayer.BlCommon.ClsReinsuranceContact.FetchReinsuranceUmbReport(contract_number,tran_type);

				if(objDataSet.Tables[0].Rows.Count > 0)
				{
					//Here we will get total no of pages 				
					int count = int.Parse(objDataSet.Tables[0].Rows.Count.ToString());
					intTotalPages = count / 10;
					int remainder = count % 10;
					if ( remainder == 0 )
					{
						intTotalPages =intTotalPages;
					}
					else
					{
						intTotalPages = intTotalPages + 1;
					}
		
				
					// show the current page number
					this.lblCurrentPage.Text = "Page " + dgReinReport.CurrentPageIndex + " of " + intTotalPages.ToString();
			
					dgReinReport.DataSource =  objDataSet.Tables[0];
					dgReinReport.DataBind();
					lblDatagrid.Visible = false;	
				

				}
				else
				{
					lblDatagrid.Text = "No Records found.";
					lblDatagrid.Visible = true;
					tbDataGrid.Visible=false;
				}
				
			}
			catch (Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
		}
		private void btnPrevious_Click(object sender, System.Web.UI.ImageClickEventArgs  e)
		{
			dgReinReport.CurrentPageIndex--;
			// get total no of records 
			BindGrid(hidContract_num.Value,hidtran_type.Value);
			SetImages();
		}

		private void btnNext_Click(object sender, System.Web.UI.ImageClickEventArgs  e)
		{
			dgReinReport.CurrentPageIndex ++;
			// Get total no of records against the customer
			BindGrid(hidContract_num.Value,hidtran_type.Value);	
			SetImages();
		}

		private void btnGOTO_Click(object sender, System.EventArgs e)
		{
			if(txtGOTO.Text.Trim() != "" & txtGOTO.Text.Trim() != "0")
			{							
				dgReinReport.CurrentPageIndex = Convert.ToInt32(txtGOTO.Text);
				BindGrid(hidContract_num.Value,hidtran_type.Value);
				SetImages();
			}
		}

		protected void dgReinReport_Paging(object sender, System.Web.UI.WebControls.DataGridPageChangedEventArgs  e)
		{
			dgReinReport.CurrentPageIndex = e.NewPageIndex; 
			BindGrid(hidContract_num.Value,hidtran_type.Value);

		}

		private void SetImages()
		{
			if(dgReinReport.CurrentPageIndex == intTotalPages)
			{
				this.btnNext.ImageUrl = "~/cmsweb/images/nextoff.gif";
				this.btnNext.Attributes.Add("onclick","return OnClick();");
			}
			else
			{
				this.btnNext.ImageUrl = "~/cmsweb/images/next.gif";
				this.btnNext.Attributes.Remove("onclick");
			}
			
			if(dgReinReport.CurrentPageIndex == 1)
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
			this.btnGOTO.Click += new System.EventHandler(this.btnGOTO_Click);
			this.btnPrevious.Click += new System.Web.UI.ImageClickEventHandler(this.btnPrevious_Click);
			this.btnNext.Click += new System.Web.UI.ImageClickEventHandler(this.btnNext_Click);
		}
		#endregion
	}
}
