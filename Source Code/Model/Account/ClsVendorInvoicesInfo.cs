/******************************************************************************************
<Author				: -   Ajit Singh Chahal
<Start Date				: -	6/28/2005 10:03:05 AM
<End Date				: -	
<Description				: - 	Model for Vendor invoices.
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using System.Data;
using Cms.Model;

namespace Cms.Model.Account
{
	/// <summary>
	/// Database Model for ACT_VENDOR_INVOICES.
	/// </summary>
	
	public class ClsVendorInvoicesInfo : Cms.Model.ClsCommonModel
	{
		private const string ACT_VENDOR_INVOICES = "ACT_VENDOR_INVOICES";
		public ClsVendorInvoicesInfo()
		{
			base.dtModel.TableName = "ACT_VENDOR_INVOICES";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table ACT_VENDOR_INVOICES
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("INVOICE_ID",typeof(int));
			base.dtModel.Columns.Add("VENDOR_ID",typeof(int));
			base.dtModel.Columns.Add("INVOICE_NUM",typeof(string));
			base.dtModel.Columns.Add("REF_PO_NUM",typeof(string));
			base.dtModel.Columns.Add("TRANSACTION_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("DUE_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("INVOICE_AMOUNT",typeof(double));
			base.dtModel.Columns.Add("NOTE",typeof(string));
			base.dtModel.Columns.Add("IS_COMMITTED",typeof(string));
			base.dtModel.Columns.Add("DATE_COMMITTED",typeof(DateTime));
			base.dtModel.Columns.Add("IS_APPROVED",typeof(string));
			base.dtModel.Columns.Add("APPROVED_BY",typeof(int));
			base.dtModel.Columns.Add("APPROVED_DATE_TIME",typeof(DateTime));
			base.dtModel.Columns.Add("FISCAL_ID",typeof(int));
		}
		#region Database schema details
		// model for database field INVOICE_ID(int)
		public int INVOICE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["INVOICE_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["INVOICE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INVOICE_ID"] = value;
			}
		}
		// model for database field VENDOR_ID(int)
		public int VENDOR_ID
		{
			get
			{
				return base.dtModel.Rows[0]["VENDOR_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["VENDOR_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["VENDOR_ID"] = value;
			}
		}
		// model for database field INVOICE_NUM(string)
		public string INVOICE_NUM
		{
			get
			{
				return base.dtModel.Rows[0]["INVOICE_NUM"] == DBNull.Value ? "" : base.dtModel.Rows[0]["INVOICE_NUM"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["INVOICE_NUM"] = value;
			}
		}
		// model for database field REF_PO_NUM(string)
		public string REF_PO_NUM
		{
			get
			{
				return base.dtModel.Rows[0]["REF_PO_NUM"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REF_PO_NUM"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REF_PO_NUM"] = value;
			}
		}
		// model for database field TRANSACTION_DATE(DateTime)
		public DateTime TRANSACTION_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["TRANSACTION_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["TRANSACTION_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["TRANSACTION_DATE"] = value;
			}
		}
		// model for database field DUE_DATE(DateTime)
		public DateTime DUE_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["DUE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["DUE_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DUE_DATE"] = value;
			}
		}
		// model for database field INVOICE_AMOUNT(double)
		public double INVOICE_AMOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["INVOICE_AMOUNT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["INVOICE_AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INVOICE_AMOUNT"] = value;
			}
		}
		// model for database field NOTE(string)
		public string NOTE
		{
			get
			{
				return base.dtModel.Rows[0]["NOTE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["NOTE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["NOTE"] = value;
			}
		}
		// model for database field IS_COMMITTED(string)
		public string IS_COMMITTED
		{
			get
			{
				return base.dtModel.Rows[0]["IS_COMMITTED"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IS_COMMITTED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_COMMITTED"] = value;
			}
		}
		// model for database field DATE_COMMITTED(DateTime)
		public DateTime DATE_COMMITTED
		{
			get
			{
				return base.dtModel.Rows[0]["DATE_COMMITTED"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["DATE_COMMITTED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DATE_COMMITTED"] = value;
			}
		}
		// model for database field IS_APPROVED(string)
		public string IS_APPROVED
		{
			get
			{
				return base.dtModel.Rows[0]["IS_APPROVED"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IS_APPROVED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_APPROVED"] = value;
			}
		}
		// model for database field APPROVED_BY(int)
		public int APPROVED_BY
		{
			get
			{
				return base.dtModel.Rows[0]["APPROVED_BY"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["APPROVED_BY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APPROVED_BY"] = value;
			}
		}
		// model for database field APPROVED_DATE_TIME(DateTime)
		public DateTime APPROVED_DATE_TIME
		{
			get
			{
				return base.dtModel.Rows[0]["APPROVED_DATE_TIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["APPROVED_DATE_TIME"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APPROVED_DATE_TIME"] = value;
			}
		}
		// model for database field FISCAL_ID(int)
		public int FISCAL_ID
		{
			get
			{
				return base.dtModel.Rows[0]["FISCAL_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["FISCAL_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["FISCAL_ID"] = value;
			}
		}
		#endregion
	}
}
