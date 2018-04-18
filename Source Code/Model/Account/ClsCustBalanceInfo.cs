using System;

namespace Cms.Model.Account
{
	/// <summary>
	/// Summary description for ClsCustBalanceInfo.
	/// </summary>
	public class ClsCustBalanceInfo : Cms.Model.ClsCommonModel
	{
		private const string ACT_CUSTOMER_BALANCE_INFORMATION = "ACT_CUSTOMER_BALANCE_INFORMATION";

		public ClsCustBalanceInfo()
		{
			base.dtModel.TableName = "ACT_CUSTOMER_BALANCE_INFORMATION";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table ACT_CUSTOMER_PAYMENTS_FROM_AGENCY
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("ROW_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("OPEN_ITEM_ROW_ID",typeof(int));
			base.dtModel.Columns.Add("AMOUNT",typeof(double));
			base.dtModel.Columns.Add("AMOUNT_DUE",typeof(double));
			base.dtModel.Columns.Add("TRAN_DESC",typeof(string));
			base.dtModel.Columns.Add("UPDATED_FROM",typeof(string));
			base.dtModel.Columns.Add("CREATED_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("IS_PRINTED",typeof(int));
			base.dtModel.Columns.Add("PRINT_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("SOURCE_ROW_ID",typeof(int));
			base.dtModel.Columns.Add("DUE_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("PRINTED_ON_NOTICE",typeof(string));
			base.dtModel.Columns.Add("NOTICE_URL",typeof(string));
			base.dtModel.Columns.Add("NOTICE_TYPE",typeof(string));
			base.dtModel.Columns.Add("MIN_DUE",typeof(double));
			base.dtModel.Columns.Add("TOTAL_PREMIUM_DUE",typeof(double));
		}
		#region Schema Details

		// model for database field IDEN_ROW_ID(int)
		public int ROW_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ROW_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ROW_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ROW_ID"] = value;
			}
		}

		// model for database field POLICY_ID(int)
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

		// model for database field POLICY_VERSION_ID(int)
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
		// model for database field CUSTOMER_ID(int)
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
			
		public double TOTAL_PREMIUM_DUE
		{
			get
			{
				return base.dtModel.Rows[0]["TOTAL_PREMIUM_DUE"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["TOTAL_PREMIUM_DUE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["TOTAL_PREMIUM_DUE"] = value;
			}
		}

		// model for database field AMOUNT(double)
		public double AMOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["AMOUNT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AMOUNT"] = value;
			}
		}

		// model for database field AMOUNT_DUE(double)
		public double AMOUNT_DUE
		{
			get
			{
				return base.dtModel.Rows[0]["AMOUNT_DUE"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["AMOUNT_DUE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AMOUNT_DUE"] = value;
			}
		}

		// model for database field MIN_DUE(double)
		public double MIN_DUE
		{
			get
			{
				return base.dtModel.Rows[0]["MIN_DUE"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["MIN_DUE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MIN_DUE"] = value;
			}
		}

		//model for database field TRAN_DESC(string)
		public string TRAN_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["TRAN_DESC"] == DBNull.Value ? "" : base.dtModel.Rows[0]["TRAN_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TRAN_DESC"] = value;
			}
		}

		//model for database field UPDATED_FROM(string)
		public string UPDATED_FROM
		{
			get
			{
				return base.dtModel.Rows[0]["UPDATED_FROM"] == DBNull.Value ? "" : base.dtModel.Rows[0]["UPDATED_FROM"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["UPDATED_FROM"] = value;
			}
		}

		// model for database field CREATED_DATE(DateTime)
		public DateTime CREATED_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["CREATED_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["CREATED_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CREATED_DATE"] = value;
			}
		}

		// model for database field IS_PRINTED(int)
		public int IS_PRINTED
		{
			get
			{
				return base.dtModel.Rows[0]["IS_PRINTED"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["IS_PRINTED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["IS_PRINTED"] = value;
			}
		}

		// model for database field PRINT_DATE(DateTime)
		public DateTime PRINT_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["PRINT_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["PRINT_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PRINT_DATE"] = value;
			}
		}
		
		// model for database field SOURCE_ROW_ID(int)
		public int SOURCE_ROW_ID
		{
			get
			{
				return base.dtModel.Rows[0]["SOURCE_ROW_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["SOURCE_ROW_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SOURCE_ROW_ID"] = value;
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

		//model for database field UPDATED_FROM(string)
		public string PRINTED_ON_NOTICE
		{
			get
			{
				return base.dtModel.Rows[0]["PRINTED_ON_NOTICE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PRINTED_ON_NOTICE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PRINTED_ON_NOTICE"] = value;
			}
		}

		//model for database field NOTICE_URL(string)
		public string NOTICE_URL
		{
			get
			{
				return base.dtModel.Rows[0]["NOTICE_URL"] == DBNull.Value ? "" : base.dtModel.Rows[0]["NOTICE_URL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["NOTICE_URL"] = value;
			}
		}

		//model for database field NOTICE_TYPE(string)
		public string NOTICE_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["NOTICE_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["NOTICE_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["NOTICE_TYPE"] = value;
			}
		}

		#endregion

	}
}
