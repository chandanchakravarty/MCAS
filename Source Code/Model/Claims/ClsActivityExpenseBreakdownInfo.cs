/******************************************************************************************
<Author				: -   Vijay Arora
<Start Date				: -	6/6/2006 9:45:17 AM
<End Date				: -	
<Description				: - 	Model Class for Claims Expense Activity Breakdown
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
namespace Cms.Model.Claims
{
	/// <summary>
	/// Database Model for CLM_ACTIVITY_EXPENSE_BREAKDOWN.
	/// </summary>
	public class ClsActivityExpenseBreakdownInfo : Cms.Model.ClsCommonModel
	{
		private const string CLM_ACTIVITY_EXPENSE_BREAKDOWN = "CLM_ACTIVITY_EXPENSE_BREAKDOWN";
		public ClsActivityExpenseBreakdownInfo()
		{
			base.dtModel.TableName = "CLM_ACTIVITY_EXPENSE_BREAKDOWN";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table CLM_ACTIVITY_EXPENSE_BREAKDOWN
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CLAIM_ID",typeof(int));
			base.dtModel.Columns.Add("EXPENSE_ID",typeof(int));
			base.dtModel.Columns.Add("EXPENSE_BREAKDOWN_ID",typeof(int));
			base.dtModel.Columns.Add("TRANSACTION_CODE",typeof(int));
			base.dtModel.Columns.Add("COVERAGE_ID",typeof(int));
			base.dtModel.Columns.Add("PAID_AMOUNT",typeof(double));
			base.dtModel.Columns.Add("ACTIVITY_ID",typeof(int));
		}
		#region Database schema details
		// model for database field CLAIM_ID(int)
		public int CLAIM_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CLAIM_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CLAIM_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CLAIM_ID"] = value;
			}
		}
		// model for database field EXPENSE_ID(int)
		public int EXPENSE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["EXPENSE_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["EXPENSE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EXPENSE_ID"] = value;
			}
		}
		// model for database field EXPENSE_BREAKDOWN_ID(int)
		public int EXPENSE_BREAKDOWN_ID
		{
			get
			{
				return base.dtModel.Rows[0]["EXPENSE_BREAKDOWN_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["EXPENSE_BREAKDOWN_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EXPENSE_BREAKDOWN_ID"] = value;
			}
		}
		// model for database field TRANSACTION_CODE(int)
		public int TRANSACTION_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["TRANSACTION_CODE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["TRANSACTION_CODE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["TRANSACTION_CODE"] = value;
			}
		}
		// model for database field COVERAGE_ID(int)
		public int COVERAGE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["COVERAGE_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["COVERAGE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COVERAGE_ID"] = value;
			}
		}
		// model for database field PAID_AMOUNT(double)
		public double PAID_AMOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["PAID_AMOUNT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["PAID_AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PAID_AMOUNT"] = value;
			}
		}
		// model for database field ACTIVITY_ID(int)
		public int ACTIVITY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ACTIVITY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ACTIVITY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ACTIVITY_ID"] = value;
			}
		}

		#endregion
	}
}
