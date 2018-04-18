using System;
using System.Data;
using Cms.Model;

namespace Cms.Model.Account
{
	/// <summary>
	/// Summary description for ClsDistributeCheckInfo.
	/// </summary>
	public class ClsDistributeCheckInfo : Cms.Model.ClsCommonModel
	{
		private const string TEMP_ACT_DISTRIBUTION_DETAILS = "TEMP_ACT_DISTRIBUTION_DETAILS";
		public ClsDistributeCheckInfo()
		{
			base.dtModel.TableName = "TEMP_ACT_DISTRIBUTION_DETAILS";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table POL_CUSTOMER_POLICY_LIST
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("IDEN_ROW_ID",typeof(int));
			base.dtModel.Columns.Add("GROUP_ID",typeof(int));
			base.dtModel.Columns.Add("GROUP_TYPE",typeof(string));
			base.dtModel.Columns.Add("ACCOUNT_ID",typeof(int));
			base.dtModel.Columns.Add("DISTRIBUTION_PERCT",typeof(double));
			base.dtModel.Columns.Add("DISTRIBUTION_AMOUNT",typeof(double));
			//base.dtModel.Columns.Add("CREATED_BY",typeof(int));
			base.dtModel.Columns.Add("NOTE",typeof(string));
			

		}
//		// model for database field CREATED_BY(int)
//		public int CREATED_BY
//		{
//			get
//			{
//				return base.dtModel.Rows[0]["CREATED_BY"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CREATED_BY"].ToString());
//			}
//			set
//			{
//				base.dtModel.Rows[0]["CREATED_BY"] = value;
//			}
//		}

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
		public int GROUP_ID
		{
			get
			{
				return base.dtModel.Rows[0]["GROUP_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["GROUP_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["GROUP_ID"] = value;
			}
		}
		public string GROUP_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["GROUP_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["GROUP_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["GROUP_TYPE"] = value;
			}
		}

		public int ACCOUNT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ACCOUNT_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["GROUP_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ACCOUNT_ID"] = value;
			}
		}
		public double DISTRIBUTION_PERCT
		{
			get
			{
				return base.dtModel.Rows[0]["DISTRIBUTION_PERCT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["DISTRIBUTION_PERCT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DISTRIBUTION_PERCT"] = value;
			}
		}

		public double DISTRIBUTION_AMOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["DISTRIBUTION_AMOUNT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["DISTRIBUTION_AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DISTRIBUTION_AMOUNT"] = value;
			}
		}
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

	}
}
