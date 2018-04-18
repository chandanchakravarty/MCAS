/******************************************************************************************
<Author					: -		Vijay Arora
<Start Date				: -		2/21/2006 11:58:18 AM
<End Date				: -	
<Description			: - 	Model for EOD Transanction Master
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
	/// Database Model for POL_EOD_TRANSACTIONS_MASTER.
	/// </summary>
	public class ClsEODTransactionMasterInfo : Cms.Model.ClsCommonModel
	{
		private const string POL_EOD_TRANSACTIONS_MASTER = "POL_EOD_TRANSACTIONS_MASTER";

		public ClsEODTransactionMasterInfo()
		{
			base.dtModel.TableName = "POL_EOD_TRANSACTIONS_MASTER";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table POL_EOD_TRANSACTIONS_MASTER
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("ROW_ID",typeof(int));
			base.dtModel.Columns.Add("TRANSACTION_ID",typeof(int));
			base.dtModel.Columns.Add("DATE_OF_LAUNCH",typeof(DateTime));
			base.dtModel.Columns.Add("STATUS",typeof(string));
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
		#endregion
	}
}
