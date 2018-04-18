using System;
using System.Data;
using Cms.Model;

namespace Cms.Model.Account
{
	public class ClsPendingVendorInvoicesInfo : Cms.Model.ClsCommonModel
	{
		private const string ACT_VENDOR_INVOICES = "ACT_VENDOR_CHECK_DISTRIBUTION";
		public ClsPendingVendorInvoicesInfo()
		{
			base.dtModel.TableName = "ACT_VENDOR_CHECK_DISTRIBUTION";	// setting table name for data table that holds property values.
			this.AddColumns();											// add columns of the database table ACT_VENDOR_CHECK_DISTRIBUTION
			base.dtModel.Rows.Add(base.dtModel.NewRow());				// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CHECK_ID",typeof(int));
			base.dtModel.Columns.Add("VENDOR_ID",typeof(int));
			base.dtModel.Columns.Add("OPEN_ITEM_ROW_ID",typeof(int));
			base.dtModel.Columns.Add("IDEN_ROW_ID",typeof(int));
			base.dtModel.Columns.Add("AMOUNT_TO_APPLY",typeof(double));
			base.dtModel.Columns.Add("REF_INVOICE_ID",typeof(int));
			base.dtModel.Columns.Add("REF_INVOICE_NO",typeof(string));
			base.dtModel.Columns.Add("REF_INVOICE_REF_NO",typeof(string));

		}
		#region Database schema details

		//Modle for database field REF_INVOICE_ID int
		public int REF_INVOICE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["REF_INVOICE_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["REF_INVOICE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REF_INVOICE_ID"] = value;
			}
		}

		//Modle for database field REF_INVOICE_NO String
		public string REF_INVOICE_NO
		{
			get
			{
				return base.dtModel.Rows[0]["REF_INVOICE_NO"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REF_INVOICE_NO"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REF_INVOICE_NO"] = value;
			}
		}

		//Modle for database field REF_INVOICE_REF_NO String 
		public string REF_INVOICE_REF_NO
		{
			get
			{
				return base.dtModel.Rows[0]["REF_INVOICE_REF_NO"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REF_INVOICE_REF_NO"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REF_INVOICE_REF_NO"] = value;
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
		// model for database field CHECK_ID(int)
		public int CHECK_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CHECK_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CHECK_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CHECK_ID"] = value;
			}
		}
		// model for database field AMOUNT_TO_APPLY(double)
		public double AMOUNT_TO_APPLY
		{
			get
			{
				return base.dtModel.Rows[0]["AMOUNT_TO_APPLY"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["AMOUNT_TO_APPLY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AMOUNT_TO_APPLY"] = value;
			}
		}

		// model for database field OPEN_ITEM_ROW_ID(int)
		public int OPEN_ITEM_ROW_ID
		{
			get
			{
				return base.dtModel.Rows[0]["OPEN_ITEM_ROW_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["OPEN_ITEM_ROW_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["OPEN_ITEM_ROW_ID"] = value;
			}
		}
		// model for database field IDEN_ROW_ID(int)
		public int IDEN_ROW_ID
		{
			get
			{
				return base.dtModel.Rows[0]["IDEN_ROW_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["IDEN_ROW_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["IDEN_ROW_ID"] = value;
			}
		}
		
		#endregion
	}
}


