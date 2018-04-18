/* ***************************************************************************************
   Author		: Deepak Batra 
   Creation Date: 05/01/2006
   Last Updated : 
   Reviewed By	: 
   Purpose		: This is the model class for the Excess Layer
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
	/// Summary description for ClsReinsuranceExcessLayerInfo.
	/// </summary>
	public class ClsReinsuranceExcessLayerInfo : Cms.Model.ClsCommonModel
	{
		# region D E C L A R A T I O N 
		
		private const string MNT_REINSURANCE_EXCESS = "MNT_REINSURANCE_EXCESS";
		
		public ClsReinsuranceExcessLayerInfo()
		{
			// setting table name for data table that holds property values.
			base.dtModel.TableName = "MNT_REINSURANCE_EXCESS";	

			// add columns of the database table MNT_REINSURANCE_CONTRACT
			this.AddColumns();								
			
			// add a blank row in the datatable
			base.dtModel.Rows.Add(base.dtModel.NewRow());	
		}

		# endregion 

		# region  A D D I N G   C O L U M N S   T O   T A B L E 
		
		private void AddColumns()
		{
			base.dtModel.Columns.Add("EXCESS_ID",typeof(int));
			base.dtModel.Columns.Add("CONTRACT_ID",typeof(int));
			base.dtModel.Columns.Add("LAYER_AMOUNT",typeof(double));
			base.dtModel.Columns.Add("UNDERLYING_AMOUNT",typeof(double));
			base.dtModel.Columns.Add("LAYER_PREMIUM",typeof(double));
			base.dtModel.Columns.Add("CEDING_COMMISSION",typeof(double));
			base.dtModel.Columns.Add("AC_PREMIUM",typeof(double));			
			base.dtModel.Columns.Add("PARTICIPATION_AMOUNT",typeof(double));			
			base.dtModel.Columns.Add("PRORATA_AMOUNT",typeof(double));			
			base.dtModel.Columns.Add("LAYER_TYPE",typeof(string));
		}

		# endregion 

		#region D A T B A S E   S C H E M A   D E T A I L S 

		// model for database field EXCESS_ID(int)
		public int EXCESS_ID
		{
			get
			{
				return base.dtModel.Rows[0]["EXCESS_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["EXCESS_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EXCESS_ID"] = value;
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

		// model for database field LAYER_AMOUNT(double)
		public double LAYER_AMOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["LAYER_AMOUNT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["LAYER_AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LAYER_AMOUNT"] = value;
			}
		}

		// model for database field UNDERLYING_AMOUNT(double)
		public double UNDERLYING_AMOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["LAYER_AMOUNT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["UNDERLYING_AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["UNDERLYING_AMOUNT"] = value;
			}
		}

		// model for database field LAYER_PREMIUM(double)
		public double LAYER_PREMIUM
		{
			get
			{
				return base.dtModel.Rows[0]["LAYER_PREMIUM"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["LAYER_PREMIUM"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LAYER_PREMIUM"] = value;
			}
		}

		// model for database field CEDING_COMMISSION(double)
		public double CEDING_COMMISSION
		{
			get
			{
				return base.dtModel.Rows[0]["CEDING_COMMISSION"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["CEDING_COMMISSION"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CEDING_COMMISSION"] = value;
			}
		}

		// model for database field AC_PREMIUM(double)
		public double AC_PREMIUM
		{
			get
			{
				return base.dtModel.Rows[0]["AC_PREMIUM"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["AC_PREMIUM"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AC_PREMIUM"] = value;
			}
		}

	
		// model for database field PARTICIPATION_AMOUNT(double)
		public double PARTICIPATION_AMOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["PARTICIPATION_AMOUNT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["PARTICIPATION_AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PARTICIPATION_AMOUNT"] = value;
			}
		}

		// model for database field PRORATA_AMOUNT(double)
		public double PRORATA_AMOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["PRORATA_AMOUNT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["PRORATA_AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PRORATA_AMOUNT"] = value;
			}
		}

		// model for database field LAYER_TYPE(string)
		public string LAYER_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["LAYER_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LAYER_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LAYER_TYPE"] = value;
			}
		}
		
		# endregion
	}
}
