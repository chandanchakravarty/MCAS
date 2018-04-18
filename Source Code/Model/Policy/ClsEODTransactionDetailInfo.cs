/******************************************************************************************
<Author					: -		Vijay Arora
<Start Date				: -		2/21/2006 12:02:06 PM
<End Date				: -	
<Description			: - 	Model Class for EOD Details
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using System.Data;
using Cms.Model;
namespace Cms.Model.Policy
{
	/// <summary>
	/// Database Model for POL_EOD_TRANSACTIONS_DETAIL.
	/// </summary>
	public class ClsEODTransactionDetailInfo : Cms.Model.ClsCommonModel
	{
		private const string POL_EOD_TRANSACTIONS_DETAIL = "POL_EOD_TRANSACTIONS_DETAIL";
		public ClsEODTransactionDetailInfo()
		{
			base.dtModel.TableName = "POL_EOD_TRANSACTIONS_DETAIL";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table POL_EOD_TRANSACTIONS_DETAIL
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("ROW_ID",typeof(int));
			base.dtModel.Columns.Add("TRANSACTION_ID",typeof(int));
			base.dtModel.Columns.Add("DATE_OF_LAUNCH",typeof(DateTime));
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("STATUS",typeof(string));
			base.dtModel.Columns.Add("ERROR_DESCRIPTION",typeof(string));
			base.dtModel.Columns.Add("JOURNAL_ID",typeof(int));
			base.dtModel.Columns.Add("PROCESS_DIARY_LIST_ID",typeof(int));
		}
		#region Database schema details
		// model for database field ROW_ID(int)
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
		// model for database field TRANSACTION_ID(int)
		public int TRANSACTION_ID
		{
			get
			{
				return base.dtModel.Rows[0]["TRANSACTION_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["TRANSACTION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["TRANSACTION_ID"] = value;
			}
		}
		// model for database field DATE_OF_LAUNCH(DateTime)
		public DateTime DATE_OF_LAUNCH
		{
			get
			{
				return base.dtModel.Rows[0]["DATE_OF_LAUNCH"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["DATE_OF_LAUNCH"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DATE_OF_LAUNCH"] = value;
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
		// model for database field STATUS(string)
		public string STATUS
		{
			get
			{
				return base.dtModel.Rows[0]["STATUS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["STATUS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["STATUS"] = value;
			}
		}
		// model for database field ERROR_DESCRIPTION(string)
		public string ERROR_DESCRIPTION
		{
			get
			{
				return base.dtModel.Rows[0]["ERROR_DESCRIPTION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ERROR_DESCRIPTION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ERROR_DESCRIPTION"] = value;
			}
		}

		// model for database field JOURNAL_ID(int)
		public int JOURNAL_ID
		{
			get
			{
				return base.dtModel.Rows[0]["JOURNAL_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["JOURNAL_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["JOURNAL_ID"] = value;
			}
		}


		// model for database field PROCESS_DIARY_LIST_ID(int)
		public int PROCESS_DIARY_LIST_ID
		{
			get
			{
				return base.dtModel.Rows[0]["PROCESS_DIARY_LIST_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PROCESS_DIARY_LIST_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PROCESS_DIARY_LIST_ID"] = value;
			}
		}
		#endregion
	}
}
