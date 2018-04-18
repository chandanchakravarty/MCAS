/******************************************************************************************
<Author				: -   Sumit Chhabra
<Start Date			: -	  06/02/2006 
<End Date			: -	
<Description		: - 	Model Class for Claims Activity Payment
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using System.Data;
using Cms.Model;
namespace Cms.Model.Claims
{
	/// <summary>
	/// Database Model for CLM_ACTIVITY_RECOVERY.
	/// </summary>
	public class ClsActivityRecoveryInfo : Cms.Model.ClsCommonModel
	{
		private const string CLM_ACTIVITY_RECOVERY = "CLM_ACTIVITY_RECOVERY";
		public ClsActivityRecoveryInfo()
		{
			base.dtModel.TableName = "CLM_ACTIVITY_RECOVERY";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table CLM_ACTIVITY_RECOVERY
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CLAIM_ID",typeof(int));
			base.dtModel.Columns.Add("RECOVERY_ID",typeof(int));
			base.dtModel.Columns.Add("RESERVE_ID",typeof(int));
			base.dtModel.Columns.Add("ACTIVITY_ID",typeof(int));
			base.dtModel.Columns.Add("AMOUNT",typeof(double));	
			base.dtModel.Columns.Add("ACTION_ON_RECOVERY",typeof(int));
			base.dtModel.Columns.Add("VEHICLE_ID",typeof(int));
			base.dtModel.Columns.Add("DRACCTS",typeof(int));
			base.dtModel.Columns.Add("CRACCTS",typeof(int));
			base.dtModel.Columns.Add("PAYMENT_METHOD",typeof(int));
			base.dtModel.Columns.Add("CHECK_NUMBER",typeof(string));
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
				return base.dtModel.Rows[0]["CLAIM_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CLAIM_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CLAIM_ID"] = value;
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
		// model for database field RECOVERY_ID(int)
		public int RECOVERY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["RECOVERY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["RECOVERY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RECOVERY_ID"] = value;
			}
		}
		// model for database field RESERVE_ID(int)
		public int RESERVE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["RESERVE_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["RESERVE_ID"].ToString());
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
				return base.dtModel.Rows[0]["ACTIVITY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ACTIVITY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ACTIVITY_ID"] = value;
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
		// model for database field ACTION_ON_RECOVERY(int)
		public int ACTION_ON_RECOVERY
		{
			get
			{
				return base.dtModel.Rows[0]["ACTION_ON_RECOVERY"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ACTION_ON_RECOVERY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ACTION_ON_RECOVERY"] = value;
			}
		}
		// model for database field VEHICLE_ID(int)
		public int VEHICLE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["VEHICLE_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["VEHICLE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["VEHICLE_ID"] = value;
			}
		}
		// model for database field PAYMENT_METHOD(int)
		public int PAYMENT_METHOD
		{
			get
			{
				return base.dtModel.Rows[0]["PAYMENT_METHOD"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PAYMENT_METHOD"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PAYMENT_METHOD"] = value;
			}
		}
		// model for database field CHECK_NUMBER(string)
		public string CHECK_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["CHECK_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CHECK_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CHECK_NUMBER"] = value;
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
