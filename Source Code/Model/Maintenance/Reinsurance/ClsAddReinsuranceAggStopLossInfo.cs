/* ***************************************************************************************
   Author		: Deepak Batra 
   Creation Date: 05/01/2006
   Last Updated : 
   Reviewed By	: 
   Purpose		: This is the model class for the Aggregate Stop Loss
   Comments		: 
   ------------------------------------------------------------------------------------- 
   History	Date	     Modified By		Description
   
   ------------------------------------------------------------------------------------- 
   *****************************************************************************************/
using System;
using System.Data;
using Cms.Model;

namespace Cms.Model.Maintenance.Reinsurance
{
	/// <summary>
	/// Summary description for ClsAddReinsuranceAggStopLossInfo.
	/// </summary>
	public class ClsAddReinsuranceAggStopLossInfo : Cms.Model.ClsCommonModel
	{
		# region D E C L A R A T I O N 
		
		private const string MNT_REIN_AGGREGATE_STOP_LOSS = "MNT_REIN_AGGREGATE_STOP_LOSS";
		
		public ClsAddReinsuranceAggStopLossInfo()
		{
			// setting table name for data table that holds property values.
			base.dtModel.TableName = "MNT_REIN_AGGREGATE_STOP_LOSS";	

			// add columns of the database table MNT_REINSURANCE_CONTRACT
			this.AddColumns();								
			
			// add a blank row in the datatable
			base.dtModel.Rows.Add(base.dtModel.NewRow());	
		}

		# endregion 

		# region  A D D I N G   C O L U M N S   T O   T A B L E 
		
		private void AddColumns()
		{
			base.dtModel.Columns.Add("AGGREGATE_ID",typeof(int));
			base.dtModel.Columns.Add("CONTRACT_ID",typeof(int));
			base.dtModel.Columns.Add("REINSURANCE_COMPANY",typeof(string));
			base.dtModel.Columns.Add("REINSURANCE_ACC_NUMBER",typeof(string));
			base.dtModel.Columns.Add("EFFECTIVE_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("EXPIRATION_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("LINE_OF_BUSINESS",typeof(int));			
			base.dtModel.Columns.Add("COVERAGE_CODE",typeof(int));			
			base.dtModel.Columns.Add("CLASS_CODE",typeof(int));			
			base.dtModel.Columns.Add("PERIL",typeof(int));			
			base.dtModel.Columns.Add("STATED_AMOUNT",typeof(double));
			base.dtModel.Columns.Add("SPECIFIC_LOSS_RATIO",typeof(double));
		}

		# endregion 

		#region D A T B A S E   S C H E M A   D E T A I L S 

		// model for database field AGGREGATE_ID(int)
		public int AGGREGATE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["AGGREGATE_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["AGGREGATE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AGGREGATE_ID"] = value;
			}
		}

		// model for database field CONTRACT_ID(int)
		public int CONTRACT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CONTRACT_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CONTRACT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CONTRACT_ID"] = value;
			}
		}

		// model for database field REINSURANCE_COMPANY(string)
		public string REINSURANCE_COMPANY
		{
			get
			{
				return base.dtModel.Rows[0]["REINSURANCE_COMPANY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REINSURANCE_COMPANY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REINSURANCE_COMPANY"] = value;
			}
		}

		// model for database field REINSURANCE_ACC_NUMBER(string)
		public string REINSURANCE_ACC_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["REINSURANCE_ACC_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REINSURANCE_ACC_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REINSURANCE_ACC_NUMBER"] = value;
			}
		}

		// model for database field EFFECTIVE_DATE(datetime)
		public DateTime EFFECTIVE_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["EFFECTIVE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["EFFECTIVE_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EFFECTIVE_DATE"] = value;
			}
		}

		// model for database field EXPIRATION_DATE(datetime)
		public DateTime EXPIRATION_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["EXPIRATION_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["EXPIRATION_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EXPIRATION_DATE"] = value;
			}
		}

		// model for database field LINE_OF_BUSINESS(int)
		public int LINE_OF_BUSINESS
		{
			get
			{
				return base.dtModel.Rows[0]["LINE_OF_BUSINESS"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LINE_OF_BUSINESS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LINE_OF_BUSINESS"] = value;
			}
		}

		// model for database field COVERAGE_CODE(int)
		public int COVERAGE_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["COVERAGE_CODE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["COVERAGE_CODE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COVERAGE_CODE"] = value;
			}
		}

		// model for database field CLASS_CODE(int)
		public int CLASS_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["CLASS_CODE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CLASS_CODE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CLASS_CODE"] = value;
			}
		}

		// model for database field PERIL(int)
		public int PERIL
		{
			get
			{
				return base.dtModel.Rows[0]["PERIL"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PERIL"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PERIL"] = value;
			}
		}

		// model for database field STATED_AMOUNT(double)
		public double STATED_AMOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["STATED_AMOUNT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["STATED_AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["STATED_AMOUNT"] = value;
			}
		}

		// model for database field SPECIFIC_LOSS_RATIO(double)
		public double SPECIFIC_LOSS_RATIO
		{
			get
			{
				return base.dtModel.Rows[0]["SPECIFIC_LOSS_RATIO"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["SPECIFIC_LOSS_RATIO"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SPECIFIC_LOSS_RATIO"] = value;
			}
		}

		# endregion 
	}
}
