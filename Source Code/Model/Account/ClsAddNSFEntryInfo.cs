using System;
using System.Data;
using Cms.Model;

namespace Cms.Model.Account
{
	/// <summary>
	/// Summary description for ClsAddNSFEntryInfo.
	/// </summary>
	public class ClsAddNSFEntryInfo: Cms.Model.ClsCommonModel
	{
		public ClsAddNSFEntryInfo()
		{
			
				base.dtModel.TableName = "ACT_CUSTOMER_OPEN_ITEMS";		// setting table name for data table that holds property values.
				this.AddColumns();								// add columns of the database table ACT_BANK_RECONCILIATION
				base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("TOTAL_DUE",typeof(double));
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_NO",typeof(string));
		}
		#region Database schema details
		// model for database field AC_RECONCILIATION_ID(int)
		public double TOTAL_DUE
		{
			get
			{
				return base.dtModel.Rows[0]["TOTAL_DUE"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["TOTAL_DUE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["TOTAL_DUE"] = value;
			}
		}

		public string POLICY_NO
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_NO"] == DBNull.Value ? "" : base.dtModel.Rows[0]["POLICY_NO"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_NO"] = value;
			}
		}
		public int AGENCY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["AGENCY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["AGENCY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ACCOUNT_ID"] = value;
			}
		}
		// model for database field DIV_ID(int)
		public int CUSTOMER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CUSTOMER_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_ID"] = value;
			}
		}
		// model for database field DEPT_ID(int)
		public int POLICY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["POLICY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_ID"] = value;
			}
		}
		// model for database field PC_ID(int)
		public int POLICY_VERSION_ID
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_VERSION_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["POLICY_VERSION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_VERSION_ID"] = value;
			}
		}
		#endregion
	}
}
