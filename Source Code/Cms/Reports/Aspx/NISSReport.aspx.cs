using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.IO;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer;
using Cms.DataLayer;
using Cms.CmsWeb;

namespace Reports.Aspx
{
	/// <summary>
	/// Summary description for NISSReport.
	/// </summary>
	public class NISSReport : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Label capPOLICY_ID;
		protected System.Web.UI.WebControls.TextBox txtPOLICY_ID;	
		protected System.Web.UI.WebControls.Label capAPP_LOB;
		protected System.Web.UI.WebControls.DropDownList cmbAPP_LOB;
		protected System.Web.UI.WebControls.TextBox Textbox1;
		protected System.Web.UI.WebControls.CompareValidator Comparevalidator1;
		protected System.Web.UI.WebControls.Label capCalendarDate;
		protected System.Web.UI.WebControls.TextBox txtCalendarDate;		
		protected System.Web.UI.WebControls.TextBox txtAccountingDate;	
		protected System.Web.UI.WebControls.TextBox txtAct_To_Date;		
		protected System.Web.UI.WebControls.Label capCustomer;
		protected System.Web.UI.WebControls.TextBox txtCustomer;
		protected System.Web.UI.WebControls.Label capCumulationDate;
		protected System.Web.UI.WebControls.Label capCumulationFromDate;
		protected System.Web.UI.WebControls.Label capCumulationToDate;	
		protected System.Web.UI.WebControls.Label capValue_Of_Date;
		protected System.Web.UI.WebControls.TextBox txtValue_Of_Date;
		protected System.Web.UI.WebControls.TextBox txtCumulationFromDate;
		protected System.Web.UI.WebControls.TextBox txtCumulationToDate;		
		protected System.Web.UI.WebControls.Label capAccountingFromDate;	
		protected System.Web.UI.WebControls.Label capAccountingDate;
		protected System.Web.UI.WebControls.Label capAct_To_Date;
		protected System.Web.UI.WebControls.Label capNAMEONPOLICY;
		protected System.Web.UI.WebControls.Label capPOLICYNUMBER;
		protected System.Web.UI.WebControls.Label capTRANSEFFECTIVEDATE;
		protected System.Web.UI.WebControls.CheckBox chkNAMEONPOLICY; 
		protected System.Web.UI.WebControls.CheckBox chkPOLICYNUMBER;  
		protected System.Web.UI.WebControls.CheckBox chkTRANSEFFECTIVEDATE; 
		protected Cms.CmsWeb.Controls.CmsButton btnReport;
		protected Cms.CmsWeb.Controls.CmsButton btnExportExcel;
		protected Cms.CmsWeb.Controls.CmsButton btnTextFile;
		protected System.Web.UI.WebControls.CompareValidator cmpToDate;
		protected string strShowFiles = "";
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			
		   // hlkCalendarDate.Attributes.Add("OnClick","fPopCalendar(document.RPT_NISS_REPORT.txtCalendarDate,document.RPT_NISS_REPORT.txtCalendarDate)"); //Javascript Implementation for Calender				
			//hlkAct_To_Date.Attributes.Add("OnClick","fPopCalendar(document.RPT_NISS_REPORT.txtAct_To_Date,document.RPT_NISS_REPORT.txtAct_To_Date)"); //Javascript Implementation for Calender				
			
			//hlkCumulationToDate.Attributes.Add("OnClick","fPopCalendar(document.RPT_NISS_REPORT.txtCumulationToDate,document.RPT_NISS_REPORT.txtCumulationToDate)"); //Javascript Implementation for Calender				
			//hlkCumulationFromDate.Attributes.Add("OnClick","fPopCalendar(document.RPT_NISS_REPORT.txtCumulationFromDate,document.RPT_NISS_REPORT.txtCumulationFromDate)"); //Javascript Implementation for Calender				
			base.ScreenId = "407";
			btnReport.CmsButtonClass	= CmsButtonType.Execute;
			btnReport.PermissionString	= gstrSecurityXML;

			btnExportExcel.CmsButtonClass	= CmsButtonType.Execute;
			btnExportExcel.PermissionString	= gstrSecurityXML;

			btnTextFile.CmsButtonClass	= CmsButtonType.Execute;
			btnTextFile.PermissionString	= gstrSecurityXML;
			
			btnExportExcel.Attributes.Add("onclick","javascript:return LobMessage();");
			btnReport.Attributes.Add("onclick","javascript:return LobMessage();");

			btnTextFile.Attributes.Add("onclick","javascript:return LobMessage();");

			if(!Page.IsPostBack)
			{
				FillValue();
				FillControl();
				SetValidators();
			}

		}
		private void FillControl()
		{
			DataTable dtLOBs = Cms.CmsWeb.ClsFetcher.LOBs;
			cmbAPP_LOB.DataSource			= dtLOBs;
			cmbAPP_LOB.DataTextField		= "LOB_DESC";
			cmbAPP_LOB.DataValueField		= "LOB_ID";
			cmbAPP_LOB.DataBind();
			cmbAPP_LOB.Items.Insert(0,"");
			cmbAPP_LOB.Items.Insert(8,"Inland Marine");
		}
		private void FillValue()
		{			
			Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure , DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
			DataSet ds = new DataSet();
			ds = objDataWrapper.ExecuteDataSet("Proc_GetNissData");			
			txtCalendarDate.Text = ds.Tables[0].Rows[0]["CALENDAR_YEAR"].ToString();
			txtAccountingDate.Text = ds.Tables[0].Rows[0]["ACCOUNTING_FROM_YEAR"].ToString();
			txtAct_To_Date.Text = ds.Tables[0].Rows[0]["ACCOUNTING_TO_YEAR"].ToString();
		}
		private void SetValidators()
		{
	
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
			this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
			this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
			this.btnTextFile.Click += new System.EventHandler(this.btnTextFile_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		private void btnExportExcel_Click(object sender, System.EventArgs e)
		{
			string lob = cmbAPP_LOB.Items[cmbAPP_LOB.SelectedIndex].Text;
			Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure , DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.AddParameter("@FROMDATE", DateTime.Parse(txtAccountingDate.Text));
			objDataWrapper.AddParameter("@TODATE", DateTime.Parse(txtAct_To_Date.Text));
			if(lob=="Inland Marine")
			{objDataWrapper.AddParameter("@CALENDARYEAR", txtCalendarDate.Text);}			
			if(chkNAMEONPOLICY.Checked==true)
			{objDataWrapper.AddParameter("@NAMEONPOLICY","NameOnPolicy" );}
			if(chkPOLICYNUMBER.Checked==true)
			{objDataWrapper.AddParameter("@POLICYNUMBER",txtPOLICY_ID.Text.ToString());	}
			if(chkTRANSEFFECTIVEDATE.Checked==true)
			{objDataWrapper.AddParameter("@TRANSEFFECTIVEDATE",DateTime.Parse(txtAct_To_Date.Text));}
			DataSet ds  = new DataSet();

			switch(lob)
			{
				case "Automobile":
					 ds = objDataWrapper.ExecuteDataSet("Proc_GetNissData_Auto");
					break;
//				case "General Liability":
//					 ds = objDataWrapper.ExecuteDataSet("Proc_GetNissData_Auto");
//					break;
				case "Homeowners":
					 ds = objDataWrapper.ExecuteDataSet("Proc_GetNissData_home");	
					break;
				case "Motorcycle":
					 ds = objDataWrapper.ExecuteDataSet("Proc_GetNissData_Auto_Excess");	
					break;
				case "Rental":
					 ds = objDataWrapper.ExecuteDataSet("Proc_GetNissData_ReDw");	
					break;
				case "Umbrella":
					 ds = objDataWrapper.ExecuteDataSet("Proc_GetNissData_UMB");	
					break;
				case "Watercraft":
					 ds = objDataWrapper.ExecuteDataSet("Proc_GetNissData_WaterCraft");	
					break;
				case "Inland Marine":
					ds = objDataWrapper.ExecuteDataSet("Proc_GetNissData_InlandMarine");	
					break;

			}						
			DataGrid excelgrid = new DataGrid();
			excelgrid.DataSource = ds.Tables[0];
			excelgrid.DataBind();
			Response.Clear();
			Response.Buffer= true;			
			Response.AddHeader("content-disposition", "attachment;filename=NissReport.xls");
			Response.Charset = "";
			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			Response.ContentType="application/vnd.ms-excel";			
			Response.ContentEncoding = System.Text.Encoding.UTF7;
			EnableViewState = false;
			System.IO.StringWriter stringWrite = new System.IO.StringWriter();
			System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
			excelgrid.RenderControl(htmlWrite);
			Response.Write("<html><head><meta http-equiv=Content-Type content='text/html; charset=utf-8'>") ;
			Response.Write("<style>td{mso-number-format:\\@}</style>");
			Response.Write(stringWrite.ToString());	
			Response.Write("</body></html>");
			stringWrite.Close();
			htmlWrite.Close();
			Response.End();
		}

		private void btnReport_Click(object sender, System.EventArgs e)
		{
			/*string strValue = "<script>"
			+ "window.open('CreditCardSweepHistoryDetails.aspx?CalendarDate="+ txtCalendarDate.Text + "&AccountingFromDate=" + txtAccountingDate.Text + "&CumulationToDate=" + txtCumulationToDate.Text + "&CumulationFromDate=" + txtCumulationFromDate.Text + "'); </script>";	
			if(!Page.IsClientScriptBlockRegistered("CreditCardSweepHistoryDetails"))
			Page.RegisterClientScriptBlock("CreditCardSweepHistoryDetails",strValue);*/

			Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure , DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
			//string strSql = "Proc_GetNissData_Auto '" + txtAccountingDate.Text + "', '" + txtAct_To_Date.Text + "'";
			objDataWrapper.AddParameter("@FROMDATE", DateTime.Parse(txtAccountingDate.Text));
			objDataWrapper.AddParameter("@TODATE", DateTime.Parse(txtAct_To_Date.Text));
			string lob = cmbAPP_LOB.Items[cmbAPP_LOB.SelectedIndex].Text;
			if(lob=="Inland Marine")
			{objDataWrapper.AddParameter("@CALENDARYEAR", txtCalendarDate.Text);}			
			if(chkNAMEONPOLICY.Checked==true)
			{objDataWrapper.AddParameter("@NAMEONPOLICY","NameOnPolicy" );}
			if(chkPOLICYNUMBER.Checked==true)
			{objDataWrapper.AddParameter("@POLICYNUMBER",txtPOLICY_ID.Text.ToString());	}
			if(chkTRANSEFFECTIVEDATE.Checked==true)
			{objDataWrapper.AddParameter("@TRANSEFFECTIVEDATE",DateTime.Parse(txtAct_To_Date.Text));}
			DataSet ds  = new DataSet();

			switch(lob)
			{
				case "Automobile":
					ds = objDataWrapper.ExecuteDataSet("Proc_GetNissData_Auto");
				break;
				/*
				case "General Liability":
					ds = objDataWrapper.ExecuteDataSet("Proc_GetNissData_Auto");
				break;
				*/
				case "Homeowners":
					ds = objDataWrapper.ExecuteDataSet("Proc_GetNissData_home");	
					break;
				case "Motorcycle":
					ds = objDataWrapper.ExecuteDataSet("Proc_GetNissData_Auto_Excess");	
					break;
				case "Rental":
					ds = objDataWrapper.ExecuteDataSet("Proc_GetNissData_ReDw");	
					break;
				case "Umbrella":
					ds = objDataWrapper.ExecuteDataSet("Proc_GetNissData_UMB");	
					break;
				case "Watercraft":
					ds = objDataWrapper.ExecuteDataSet("Proc_GetNissData_WaterCraft");	
					break;
				case "Inland Marine":
					ds = objDataWrapper.ExecuteDataSet("Proc_GetNissData_InlandMarine");	
					break;
			}					

			string filePath = Server.MapPath("~");

			File.Delete(filePath + "\\NissReport.txt");
			System.IO.StreamWriter nissWrite = new System.IO.StreamWriter(filePath + "\\NissReport.txt", true);

			string  [] rows = new string[ds.Tables[0].Columns.Count];
			for(int j=0;j<=ds.Tables[0].Columns.Count-1; j++)
			{
				rows[j] = ds.Tables[0].Columns[j].ColumnName + " ";
				if(rows[j].IndexOf("BLANK") == 0)
					rows[j] = "BLANK ";
			}

			for(int i=0; i<= ds.Tables[0].Columns.Count-1; i++)
			{
				nissWrite.Write(rows[i]);
			}
			nissWrite.WriteLine("") ;

			for(int i=0; i<= ds.Tables[0].Columns.Count-1; i++)
			{
				string temp="--------------------------------------------";
				temp=temp.Substring(0,rows[i].Length-1);
				nissWrite.Write(temp);
				nissWrite.Write(" ");
			}
			nissWrite.WriteLine("") ;


			for(int i=0;i<=ds.Tables[0].Rows.Count - 1; i++)
			{
				for(int j=0;j<=ds.Tables[0].Columns.Count - 1; j++)
				{
					string temp = ds.Tables[0].Rows[i][j].ToString();
					if(ds.Tables[0].Rows[i][j] == System.DBNull.Value)
						temp = "NULL";
					int length = rows[j].Length-1;
					if(temp.Length > length)
						temp = temp.Substring(0, length);
					else
						temp = temp.PadRight(length, ' ');
					nissWrite.Write(temp);
					nissWrite.Write(" ") ;
				}
				nissWrite.WriteLine("") ;
			}

			nissWrite.Close();

			Response.Write("<script> window.open('/cms/NissReport.txt') </script>");
		}

		private void btnTextFile_Click(object sender, System.EventArgs e)
		{
			
			Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure , DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.AddParameter("@FROMDATE", DateTime.Parse(txtAccountingDate.Text));
			objDataWrapper.AddParameter("@TODATE", DateTime.Parse(txtAct_To_Date.Text));
			string lob = cmbAPP_LOB.Items[cmbAPP_LOB.SelectedIndex].Text;
			if(lob=="Inland Marine")
			{objDataWrapper.AddParameter("@CALENDARYEAR", txtCalendarDate.Text);}			
			
			DataSet ds  = new DataSet();

			switch(lob)
			{
				case "Automobile":
					ds = objDataWrapper.ExecuteDataSet("Proc_GetNissData_Auto");
					break;
				case "Homeowners":
					ds = objDataWrapper.ExecuteDataSet("Proc_GetNissData_home");	
					break;
				case "Motorcycle":
					ds = objDataWrapper.ExecuteDataSet("Proc_GetNissData_Auto_Excess");	
					break;
				case "Rental":
					ds = objDataWrapper.ExecuteDataSet("Proc_GetNissData_ReDw");	
					break;
				case "Umbrella":
					ds = objDataWrapper.ExecuteDataSet("Proc_GetNissData_UMB");	
					break;
				case "Watercraft":
					ds = objDataWrapper.ExecuteDataSet("Proc_GetNissData_WaterCraft");	
					break;
				case "Inland Marine":
					ds = objDataWrapper.ExecuteDataSet("Proc_GetNissData_InlandMarine");	
					break;
			}					
		

			string filePath = Server.MapPath("~");

			File.Delete(filePath + "\\NissReport.txt");
			System.IO.StreamWriter nissWrite = new System.IO.StreamWriter(filePath + "\\NissReport.txt", true);
			 
			string  [] rows = new string[ds.Tables[0].Columns.Count];
			for(int j=0;j<=ds.Tables[0].Columns.Count-1; j++)
			{
				rows[j] = ds.Tables[0].Columns[j].ColumnName + " ";
				if(rows[j].IndexOf("BLANK") == 0)
					rows[j] = "BLANK ";
			}

			for(int i=0; i<= ds.Tables[0].Columns.Count-1; i++)
			{
				nissWrite.Write(rows[i]);
			}
			nissWrite.WriteLine("") ;

			for(int i=0; i<= ds.Tables[0].Columns.Count-1; i++)
			{
				string temp="--------------------------------------------";
				temp=temp.Substring(0,rows[i].Length-1);
				nissWrite.Write(temp);
				nissWrite.Write(" ");
			}
			nissWrite.WriteLine("") ;


			for(int i=0;i<=ds.Tables[0].Rows.Count - 1; i++)
			{
				for(int j=0;j<=ds.Tables[0].Columns.Count - 1; j++)
				{
					string temp = ds.Tables[0].Rows[i][j].ToString();
					if(ds.Tables[0].Rows[i][j] == System.DBNull.Value)
						temp = "NULL";
					int length = rows[j].Length-1;
					if(temp.Length > length)
						temp = temp.Substring(0, length);
					else
						temp = temp.PadRight(length, ' ');
					nissWrite.Write(temp);
					nissWrite.Write(" ") ;
				}
				nissWrite.WriteLine("") ;

			}

			nissWrite.Close();			

			string sGenName = "NissReport.txt";		
			
			System.IO.FileStream fs = null;
			fs = System.IO.File.Open(filePath + "\\NissReport.txt", System.IO.FileMode.Open);
			byte[] btFile = new byte[fs.Length];
			fs.Read(btFile, 0, Convert.ToInt32(fs.Length));
			fs.Close();
			Response.Clear();
			Response.AddHeader("Content-disposition", "attachment; filename=" + sGenName);
			Response.ContentType = "application/octet-stream";
			Response.BinaryWrite(btFile);
			Response.End();	
		
		}		
	}
}
