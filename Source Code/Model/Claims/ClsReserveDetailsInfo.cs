/******************************************************************************************
<Author					: - Sumit Chhabra
<Start Date				: -	29/05/2006
<End Date				: -	
<Description			: - Models CLM_ACTIVITY_RESERVE
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
	/// Database Model for CLM_ACTIVITY_RESERVE.
	/// </summary>
	public class ClsReserveDetailsInfo : Cms.Model.ClsCommonModel
	{
		private const string CLM_ACTIVITY_RESERVE = "CLM_ACTIVITY_RESERVE";
		public ClsReserveDetailsInfo()
		{
			base.dtModel.TableName = "CLM_ACTIVITY_RESERVE";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table CLM_ACTIVITY_RESERVE
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CLAIM_ID",typeof(int));
			base.dtModel.Columns.Add("RESERVE_ID",typeof(int));
			base.dtModel.Columns.Add("ACTIVITY_ID",typeof(int));
			base.dtModel.Columns.Add("COVERAGE_ID",typeof(int));			
			base.dtModel.Columns.Add("PRIMARY_EXCESS",typeof(string));
			base.dtModel.Columns.Add("MCCA_ATTACHMENT_POINT",typeof(double));	
			base.dtModel.Columns.Add("MCCA_APPLIES",typeof(string));						
			base.dtModel.Columns.Add("ATTACHMENT_POINT",typeof(double));			
			base.dtModel.Columns.Add("OUTSTANDING",typeof(double));							
			base.dtModel.Columns.Add("REINSURANCE_CARRIER",typeof(int));			
			base.dtModel.Columns.Add("RI_RESERVE",typeof(double));	
			base.dtModel.Columns.Add("VEHICLE_ID",typeof(int));					
			base.dtModel.Columns.Add("CLAIM_RESERVE_AMOUNT",typeof(double));					
			base.dtModel.Columns.Add("POLICY_LIMITS",typeof(double));	
			base.dtModel.Columns.Add("RETENTION_LIMITS",typeof(double));
			base.dtModel.Columns.Add("ACTION_ON_PAYMENT",typeof(int));
			base.dtModel.Columns.Add("DRACCTS",typeof(int));
			base.dtModel.Columns.Add("CRACCTS",typeof(int));
			base.dtModel.Columns.Add("TRANSACTION_ID",typeof(int));
			//Added for Itrack Issue 7663 on 18 Aug 2010
			base.dtModel.Columns.Add("ACTUAL_RISK_ID",typeof(int));
			base.dtModel.Columns.Add("ACTUAL_RISK_TYPE",typeof(string));
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
		// model for database field RESERVE_ID(int)
		public int RESERVE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["RESERVE_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["RESERVE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RESERVE_ID"] = value;
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
		// model for database field PRIMARY_EXCESS(string)
		public string PRIMARY_EXCESS
		{
			get
			{
				return base.dtModel.Rows[0]["PRIMARY_EXCESS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PRIMARY_EXCESS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PRIMARY_EXCESS"] = value;
			}
		}		
		// model for database field MCCA_ATTACHMENT_POINT(double)
		public double MCCA_ATTACHMENT_POINT
		{
			get
			{
				return base.dtModel.Rows[0]["MCCA_ATTACHMENT_POINT"] == DBNull.Value ? Convert.ToDouble(null) : double.Parse(base.dtModel.Rows[0]["MCCA_ATTACHMENT_POINT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MCCA_ATTACHMENT_POINT"] = value;
			}
		}			
		// model for database field MCCA_APPLIES(double)
		public double MCCA_APPLIES
		{
			get
			{
				return base.dtModel.Rows[0]["MCCA_APPLIES"] == DBNull.Value ? Convert.ToDouble(null) : double.Parse(base.dtModel.Rows[0]["MCCA_APPLIES"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MCCA_APPLIES"] = value;
			}
		}
		// model for database field ATTACHMENT_POINT(double)
		public double ATTACHMENT_POINT
		{
			get
			{
				return base.dtModel.Rows[0]["ATTACHMENT_POINT"] == DBNull.Value ? Convert.ToDouble(null) : double.Parse(base.dtModel.Rows[0]["ATTACHMENT_POINT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ATTACHMENT_POINT"] = value;
			}
		}
		// model for database field OUTSTANDING(double)
		public double OUTSTANDING
		{
			get
			{
				return base.dtModel.Rows[0]["OUTSTANDING"] == DBNull.Value ? Convert.ToDouble(null) : double.Parse(base.dtModel.Rows[0]["OUTSTANDING"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["OUTSTANDING"] = value;
			}
		}		
		// model for database field REINSURANCE_CARRIER(int)
		public int REINSURANCE_CARRIER
		{
			get
			{
				return base.dtModel.Rows[0]["REINSURANCE_CARRIER"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["REINSURANCE_CARRIER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REINSURANCE_CARRIER"] = value;
			}
		}
		// model for database field RI_RESERVE(double)
		public double RI_RESERVE
		{
			get
			{
				return base.dtModel.Rows[0]["RI_RESERVE"] == DBNull.Value ? Convert.ToDouble(null) : double.Parse(base.dtModel.Rows[0]["RI_RESERVE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RI_RESERVE"] = value;
			}
		}
		// model for database field VEHICLE_ID(int)
		public int VEHICLE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["VEHICLE_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["VEHICLE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["VEHICLE_ID"] = value;
			}
		}		
		// model for database field CLAIM_RESERVE_AMOUNT(double)
		public double CLAIM_RESERVE_AMOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["CLAIM_RESERVE_AMOUNT"] == DBNull.Value ? Convert.ToDouble(null) : double.Parse(base.dtModel.Rows[0]["CLAIM_RESERVE_AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CLAIM_RESERVE_AMOUNT"] = value;
			}
		}
		// model for database field POLICY_LIMITS(double)
		public double POLICY_LIMITS
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_LIMITS"] == DBNull.Value ? Convert.ToDouble(null) : double.Parse(base.dtModel.Rows[0]["POLICY_LIMITS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_LIMITS"] = value;
			}
		}
		// model for database field RETENTION_LIMITS(double)
		public double RETENTION_LIMITS
		{
			get
			{
				return base.dtModel.Rows[0]["RETENTION_LIMITS"] == DBNull.Value ? Convert.ToDouble(null) : double.Parse(base.dtModel.Rows[0]["RETENTION_LIMITS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RETENTION_LIMITS"] = value;
			}
		}		
		// model for database field ACTION_ON_PAYMENT(int)
		public int ACTION_ON_PAYMENT
		{
			get
			{
				return base.dtModel.Rows[0]["ACTION_ON_PAYMENT"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["ACTION_ON_PAYMENT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ACTION_ON_PAYMENT"] = value;
			}
		}
		// model for database field DRACCTS(int)
		public int DRACCTS
		{
			get
			{
				return base.dtModel.Rows[0]["DRACCTS"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["DRACCTS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DRACCTS"] = value;
			}
		}

		
		// model for database field CRACCTS(int)
		public int CRACCTS
		{
			get
			{
				return base.dtModel.Rows[0]["CRACCTS"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CRACCTS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CRACCTS"] = value;
			}
		}

		//Added for Itrack Issue 7663 on 18 Aug 2010
		/// <summary>
		/// model for database field ACTUAL_RISK_ID(int)
		/// </summary>
		public int ACTUAL_RISK_ID
		{
			get
			{
				return dtModel.Rows[0]["ACTUAL_RISK_ID"] == DBNull.Value ? Convert.ToInt32(null) : Convert.ToInt32(dtModel.Rows[0]["ACTUAL_RISK_ID"]);
			}
			set
			{
				dtModel.Rows[0]["ACTUAL_RISK_ID"] = value;
			}
		}

		// model for database field ACTUAL_RISK_TYPE(string)
		public string ACTUAL_RISK_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["ACTUAL_RISK_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ACTUAL_RISK_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ACTUAL_RISK_TYPE"] = value;
			}
		}

		#endregion
	}
}
