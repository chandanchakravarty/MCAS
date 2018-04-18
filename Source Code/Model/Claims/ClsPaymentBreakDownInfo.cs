/******************************************************************************************
<Author					: - Sumit Chhabra
<Start Date				: -	05/06/2006
<End Date				: -	
<Description			: - Models CLM_ACTIVITY_PAYMENT_BREAKDOWN
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
	/// Database Model for CLM_ACTIVITY_PAYMENT_BREAKDOWN.
	/// </summary>
	public class ClsPaymentBreakDownInfo : Cms.Model.ClsCommonModel
	{
		private const string CLM_ACTIVITY_PAYMENT_BREAKDOWN = "CLM_ACTIVITY_PAYMENT_BREAKDOWN";
		public ClsPaymentBreakDownInfo()
		{
			base.dtModel.TableName = "CLM_ACTIVITY_PAYMENT_BREAKDOWN";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table CLM_ACTIVITY_PAYMENT_BREAKDOWN
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CLAIM_ID",typeof(int));			
			base.dtModel.Columns.Add("ACTIVITY_ID",typeof(int));
			base.dtModel.Columns.Add("PAYMENT_BREAKDOWN_ID",typeof(int));
			base.dtModel.Columns.Add("TRANSACTION_CODE",typeof(int));			
			base.dtModel.Columns.Add("COVERAGE_ID",typeof(int));						
			base.dtModel.Columns.Add("PAID_AMOUNT",typeof(double));						
		}
		#region Database schema details
		// model for database field CLAIM_ID(int)
		public int CLAIM_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CLAIM_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CLAIM_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CLAIM_ID"] = value;
			}
		}
		// model for database field ACTIVITY_ID(int)
		public int ACTIVITY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ACTIVITY_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["ACTIVITY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ACTIVITY_ID"] = value;
			}
		}
		// model for database field PAYMENT_BREAKDOWN_ID(int)
		public int PAYMENT_BREAKDOWN_ID
		{
			get
			{
				return base.dtModel.Rows[0]["PAYMENT_BREAKDOWN_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["PAYMENT_BREAKDOWN_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PAYMENT_BREAKDOWN_ID"] = value;
			}
		}
		
		// model for database field TRANSACTION_CODE(int)
		public int TRANSACTION_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["TRANSACTION_CODE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["TRANSACTION_CODE"].ToString());
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
				return base.dtModel.Rows[0]["COVERAGE_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["COVERAGE_ID"].ToString());
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
				return base.dtModel.Rows[0]["PAID_AMOUNT"] == DBNull.Value ? Convert.ToDouble(null) : double.Parse(base.dtModel.Rows[0]["PAID_AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PAID_AMOUNT"] = value;
			}
		}
		
		#endregion
	}
}
