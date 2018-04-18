/******************************************************************************************
<Author				: -   Pravesh chandel
<Start Date				: -	27/10/2006
<End Date				: -	
<Description				: - 	Model for Minimum Premium at Cancellation
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

namespace Cms.Model.Maintenance 
{
	/// <summary>
	/// Summary description for ClsMinimumPremAmt.
	/// </summary>
	public class ClsMinimumPremAmt : Cms.Model.ClsCommonModel
	{
	    	private const string ACT_MINIMUM_PREM_CANCEL= "ACT_MINIMUM_PREM_CANCEL";
			public ClsMinimumPremAmt() 
			{
				base.dtModel.TableName = "ACT_MINIMUM_PREM_CANCEL";		// setting table name for data table that holds property values.
				this.AddColumns();								// add columns of the database table ACT_MINIMUM_PREM_CANCEL
				base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
			}
			private void AddColumns()
			{
			base.dtModel.Columns.Add("ROW_ID",typeof(int));
			base.dtModel.Columns.Add("LOB_ID",typeof(int));
			base.dtModel.Columns.Add("SUB_LOB_ID",typeof(int));
			base.dtModel.Columns.Add("STATE_ID",typeof(int));
			base.dtModel.Columns.Add("COUNTRY_ID",typeof(int));
			//base.dtModel.Columns.Add("IS_ACTIVE",typeof(char));
			base.dtModel.Columns.Add("EFFECTIVE_FROM_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("EFFECTIVE_TO_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("PREMIUM_AMT",typeof(double));
			}

		#region Database schema details
		// model for database field ROW_ID(int)
		public int ROW_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ROW_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["ROW_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ROW_ID"] = value;
			}
		}
		// model for database field COUNTRY_ID(int)
		public int COUNTRY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["COUNTRY_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["COUNTRY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COUNTRY_ID"] = value;
			}
		}
		// model for database field STATE_ID(int)
		public int STATE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["STATE_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["STATE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["STATE_ID"] = value;
			}
		}
		// model for database field LOB_ID(int)
		public int LOB_ID
		{
			get
			{
				return base.dtModel.Rows[0]["LOB_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LOB_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LOB_ID"] = value;
			}
		}
		// model for database field SUB_LOB_ID(int)
		public int SUB_LOB_ID
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_LOB_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["SUB_LOB_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SUB_LOB_ID"] = value;
			}
		}
		// model for database field EFFECTIVE_FROM_DATE(DateTime)
		public DateTime EFFECTIVE_FROM_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["EFFECTIVE_FROM_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["EFFECTIVE_FROM_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EFFECTIVE_FROM_DATE"] = value;
			}
		}
		// model for database field EFFECTIVE_TO_DATE(DateTime)
		public DateTime EFFECTIVE_TO_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["EFFECTIVE_TO_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["EFFECTIVE_TO_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EFFECTIVE_TO_DATE"] = value;
			}
		}
		// model for database field COMMISSION_PERCENT(double)
		public double PREMIUM_AMT
		{
			get
			{
				return base.dtModel.Rows[0]["PREMIUM_AMT"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["PREMIUM_AMT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PREMIUM_AMT"] = value;
			}
		}
		
		
		#endregion
	}
}
