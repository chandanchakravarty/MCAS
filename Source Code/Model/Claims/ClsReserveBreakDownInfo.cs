/******************************************************************************************
<Author					: - Sumit Chhabra
<Start Date				: -	01/06/2006
<End Date				: -	
<Description			: - Models CLM_ACTIVITY_RESERVE_BREAKDOWN
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
	/// Database Model for CLM_ACTIVITY_RESERVE_BREAKDOWN.
	/// </summary>
	public class ClsReserveBreakDownInfo : Cms.Model.ClsCommonModel
	{
		private const string CLM_ACTIVITY_RESERVE_BREAKDOWN = "CLM_ACTIVITY_RESERVE_BREAKDOWN";
		public ClsReserveBreakDownInfo()
		{
			base.dtModel.TableName = "CLM_ACTIVITY_RESERVE_BREAKDOWN";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table CLM_ACTIVITY_RESERVE_BREAKDOWN
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CLAIM_ID",typeof(int));			
			base.dtModel.Columns.Add("ACTIVITY_ID",typeof(int));
			base.dtModel.Columns.Add("RESERVE_BREAKDOWN_ID",typeof(int));
			base.dtModel.Columns.Add("TRANSACTION_CODE",typeof(int));			
			base.dtModel.Columns.Add("BASIS",typeof(int));			
			base.dtModel.Columns.Add("VALUE",typeof(double));				
			base.dtModel.Columns.Add("AMOUNT",typeof(double));						
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
		// model for database field RESERVE_BREAKDOWN_ID(int)
		public int RESERVE_BREAKDOWN_ID
		{
			get
			{
				return base.dtModel.Rows[0]["RESERVE_BREAKDOWN_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["RESERVE_BREAKDOWN_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RESERVE_BREAKDOWN_ID"] = value;
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
		// model for database field BASIS(int)
		public int BASIS
		{
			get
			{
				return base.dtModel.Rows[0]["BASIS"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["BASIS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["BASIS"] = value;
			}
		}
		// model for database field VALUE(double)
		public double VALUE
		{
			get
			{
				return base.dtModel.Rows[0]["VALUE"] == DBNull.Value ? Convert.ToDouble(null) : double.Parse(base.dtModel.Rows[0]["VALUE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["VALUE"] = value;
			}
		}
		
		// model for database field AMOUNT(double)
		public double AMOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["AMOUNT"] == DBNull.Value ? Convert.ToDouble(null) : double.Parse(base.dtModel.Rows[0]["AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AMOUNT"] = value;
			}
		}
		
		#endregion
	}
}
