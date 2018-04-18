#region Namespaces
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
using System.Reflection;
using System.Resources;
#endregion

namespace Cms.CmsWeb.Maintenance
{	
	public class ReinsuranceCoverageEndorsementReport : Cms.CmsWeb.cmsbase
	{
		#region Page Controls Declaration

		protected Cms.CmsWeb.Controls.CmsButton btnReport;
		protected System.Web.UI.WebControls.DataGrid dgVenPendInv;
		protected Cms.CmsWeb.Controls.CmsButton btnExcelReport;

		protected System.Web.UI.HtmlControls.HtmlGenericControl tbDataGrid;
		protected System.Web.UI.WebControls.Label lblDatagrid;
		protected System.Web.UI.WebControls.ListBox lstStateName;
		protected System.Web.UI.WebControls.ListBox lstLOB;
        protected System.Web.UI.WebControls.Label lblState;
        protected System.Web.UI.WebControls.Label lblLOB;
        protected System.Web.UI.WebControls.Label lblheading;
        protected System.Web.UI.WebControls.Label lblmessage;
        //System.Resources.ResourceManager objResourceMgr;
		
        ResourceManager objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.ReinsuranceCoverageEndorsementReport", System.Reflection.Assembly.GetExecutingAssembly());

		#endregion
		
		//string statelist = "";
		//string loblist="";

		#region Page Load
		private void Page_Load(object sender, System.EventArgs e)
		{
			
			#region Button Permissions / Screen ID
			base.ScreenId = "413";
            Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.Text, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
			btnReport.PermissionString	= gstrSecurityXML;
			btnReport.CmsButtonClass	= CmsButtonType.Execute;

			btnExcelReport.PermissionString	= gstrSecurityXML;
			btnExcelReport.CmsButtonClass	= CmsButtonType.Execute;
			
			#endregion
			if(!Page.IsPostBack)
            {
                Setcaption();			
				try
					{			
						DataSet ds2 = objDataWrapper.ExecuteDataSet("select (isnull(STATE_NAME,'')) as StateName ,STATE_ID as ID from MNT_COUNTRY_STATE_LIST order by StateName");
						lstStateName.DataSource = ds2.Tables[0];					
						lstStateName.DataTextField = "StateName";
						lstStateName.DataValueField = "ID";
						lstStateName.DataBind();
						this.lstStateName.Items.Insert(0,"All");
						this.lstStateName.SelectedIndex =0;

                        //DataSet ds3 = objDataWrapper.ExecuteDataSet("select (isnull(LOB_DESC,'') + ' ') as LOB ,LOB_ID as ID from MNT_LOB_MASTER order by LOB");
                        //lstLOB.DataSource = ds3.Tables[0];
                        //lstLOB.DataTextField = "LOB";
                        //lstLOB.DataValueField = "ID";
                        //lstLOB.DataBind();
                        //this.lstLOB.Items.Insert(0, "All");
                        //this.lstLOB.SelectedIndex = 0;
                        DataTable dtLOBs = Cms.CmsWeb.ClsFetcher.LOBs;
                        lstLOB.DataSource = dtLOBs;
                        lstLOB.DataTextField = "LOB_DESC";
                        lstLOB.DataValueField = "LOB_ID";
                        lstLOB.DataBind();
                        this.lstLOB.Items.Insert(0, "All");
                        this.lstLOB.SelectedIndex = 0;

					}
					catch(Exception ex)
					{
						Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
					}				
				
				BindGrid("","");
				tbDataGrid.Visible=false;
			}	
							
		}
        protected void Setcaption()
        {
            lblState.Text = objResourceMgr.GetString("lstStateName");
            lblLOB.Text = objResourceMgr.GetString("lstLOB");
            lblheading.Text = objResourceMgr.GetString("lblheading");
            lblmessage.Text = objResourceMgr.GetString("lblmessage");
            btnReport.Text = objResourceMgr.GetString("btnReport");
            btnExcelReport.Text = objResourceMgr.GetString("btnExcelReport");
        }
            

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		
	
		private void InitializeComponent()
		{    
			this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
			this.btnExcelReport.Click += new System.EventHandler(this.btnExcelReport_Click);
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion

		#region Run Report / Bindgrid / Paging
		

		private void BindGrid(string lob,string state)
		{
			try
			{	

				DataSet objDataSet = Cms.BusinessLayer.BlAccount.ClsVendorInvoices.FetchReinsuranceCoverageReport(lob,state);

				if(objDataSet.Tables[0].Rows.Count > 0)
				{
					dgVenPendInv.DataSource =  objDataSet.Tables[0];
					dgVenPendInv.DataBind();
					tbDataGrid.Visible=true;
					lblDatagrid.Visible = false;	
				

				}
				else
				{
                    lblDatagrid.Text = objResourceMgr.GetString("lblDatagrid"); //"No Records found.";
					lblDatagrid.Visible = true;
					tbDataGrid.Visible=false;
				}
				
			}
			catch (Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
		}
       
        
		protected void dgVenPendInv_Paging(object sender, System.Web.UI.WebControls.DataGridPageChangedEventArgs  e)
		{
			dgVenPendInv.CurrentPageIndex = e.NewPageIndex; 
					
			string statelist = "";
			for(int stateindex = 0; stateindex < lstStateName.Items.Count; stateindex++)
			{
				if(lstStateName.Items[0].Selected == true)
				{
					statelist = "0";
					break;
				}
				if(lstStateName.Items[stateindex].Selected == true)
					statelist += lstStateName.Items[stateindex].Value + ",";
			}
			if(statelist.Length > 1)
				statelist = statelist.Substring(0, statelist.Length - 1);

			
			string loblist = "";
			for(int lobindex = 0; lobindex < lstLOB.Items.Count; lobindex++)
			{
				if(lstLOB.Items[0].Selected == true)
				{
					loblist = "0";
					break;
				}
				if(lstLOB.Items[lobindex].Selected == true)
					loblist += lstLOB.Items[lobindex].Value + ",";
			}
			if(loblist.Length > 1)
				loblist = loblist.Substring(0, loblist.Length - 1);

			BindGrid(loblist,statelist);			

		}


		private void btnReport_Click(object sender, System.EventArgs e)
		{
			string statelist = "";
			for(int stateindex = 0; stateindex < lstStateName.Items.Count; stateindex++)
			{
				if(lstStateName.Items[0].Selected == true)
				{
					statelist = "0";
					break;
				}
				if(lstStateName.Items[stateindex].Selected == true)
					statelist += lstStateName.Items[stateindex].Value + ",";
			}
			if(statelist.Length > 1)
			statelist = statelist.Substring(0, statelist.Length - 1);

			
			string loblist = "";
			for(int lobindex = 0; lobindex < lstLOB.Items.Count; lobindex++)
			{
				if(lstLOB.Items[0].Selected == true)
				{
					loblist = "0";
					break;
				}
				if(lstLOB.Items[lobindex].Selected == true)
					loblist += lstLOB.Items[lobindex].Value + ",";
			}
			if(loblist.Length > 1)
			loblist = loblist.Substring(0, loblist.Length - 1);

			dgVenPendInv.CurrentPageIndex =0;
			dgVenPendInv.Visible = true;

			BindGrid(loblist,statelist);			

		}
        

		private void btnExcelReport_Click(object sender, System.EventArgs e)
		{
			string statelist = "";
			for(int stateindex = 0; stateindex < lstStateName.Items.Count; stateindex++)
			{
				if(lstStateName.Items[0].Selected == true)
				{
					statelist = "0";
					break;
				}
				if(lstStateName.Items[stateindex].Selected == true)
					statelist += lstStateName.Items[stateindex].Value + ",";
			}
			if(statelist.Length > 1)
				statelist = statelist.Substring(0, statelist.Length - 1);

			
			string loblist = "";
			for(int lobindex = 0; lobindex < lstLOB.Items.Count; lobindex++)
			{
				if(lstLOB.Items[0].Selected == true)
				{
					loblist = "0";
					break;
				}
				if(lstLOB.Items[lobindex].Selected == true)
					loblist += lstLOB.Items[lobindex].Value + ",";
			}
			if(loblist.Length > 1)
				loblist = loblist.Substring(0, loblist.Length - 1);

			dgVenPendInv.CurrentPageIndex =0;
			dgVenPendInv.Visible = true;

			BindGrid(loblist,statelist);
			
			/*			Response.Clear();
						Response.Charset = "";

						Response.AddHeader("content-disposition", "attachment;filename=ReinsuranceCoverageEndorsementReport.aspx.xls");
						Response.ContentType="application/vnd.ms-excel";
						System.IO.StringWriter stringWrite = new System.IO.StringWriter();

						System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

						DataGrid excelgrid = new DataGrid();
						excelgrid.DataSource = dgVenPendInv.DataSource;

						excelgrid.DataBind();

						excelgrid.RenderControl(htmlWrite);

						Response.Write(stringWrite.ToString());
			*/
			//Response.Redirect("../../Reports/aspx/TrailBalance.aspx");

			DataGrid excelgrid = new DataGrid();
			excelgrid.DataSource = dgVenPendInv.DataSource;

			excelgrid.DataBind();
			if(excelgrid.Items.Count > 0)
			{
				Response.Clear();
				Response.Buffer= true;			
				Response.AddHeader("content-disposition", "attachment;filename=ReinsuranceReport.xls");
				Response.Charset = "";
				Response.Cache.SetCacheability(HttpCacheability.NoCache);
				Response.ContentType="application/vnd.ms-excel";
				System.IO.StringWriter stringWrite = new System.IO.StringWriter();
				System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

				excelgrid.RenderControl(htmlWrite);

				Response.Write(stringWrite.ToString());	
				Response.End();
			}
			else
				Response.Write("<script> alert('No records avaiable for export to Excel.'); </script>");
		}
		#endregion

        protected void dgVenPendInv_DataBound(object sender, DataGridItemEventArgs e)
        {
            //System.Web.UI.WebControls.BoundField obj = (BoundField)dgVenPendInv.Columns[0];
            if (e.Item.ItemType == ListItemType.Header) 
            {
                e.Item.Cells[0].Text = objResourceMgr.GetString("lblCoverage_Desc");//"Coverage Desc";
                e.Item.Cells[1].Text = objResourceMgr.GetString("lblState_Code");//"State Code";
                e.Item.Cells[2].Text = objResourceMgr.GetString("lblProduct_Code");//"Product Code";
                e.Item.Cells[3].Text = objResourceMgr.GetString("lblDisplay_on_Reinsurance_Product");//"Display on Reinsurance Product";
                e.Item.Cells[4].Text = objResourceMgr.GetString("lblReinsurance_Coverage_Category");//"Reinsurance Coverage Category";
                e.Item.Cells[5].Text = objResourceMgr.GetString("lblASLOB");//"ASLOB";
                e.Item.Cells[6].Text = objResourceMgr.GetString("lblCalculation");//"Calculation";
                e.Item.Cells[7].Text = objResourceMgr.GetString("lblAlways_Display_on_Reinsurance_Inquiry");//"Always Display on Reinsurance Inquiry";
                e.Item.Cells[8].Text = objResourceMgr.GetString("lblStart_Date");//"Start Date";
                e.Item.Cells[9].Text = objResourceMgr.GetString("lblEnd_Date");//"End Date";
                e.Item.Cells[10].Text = objResourceMgr.GetString("lblReinsurance_Report_Buckets");//"Reinsurance Report Buckets";
            }
           
            //dgVenPendInv.Items[]
           //// if (obj.DataField == "COLUMN_1")
           // {
           //     obj.DataField = "COLUMN_2";
           // }
           // else
           // {
           //     obj.DataField = "COLUMN_2";
           // }

        }
	}
}
        #endregion;