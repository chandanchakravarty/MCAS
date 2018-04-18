/******************************************************************************************
<Author				: -   Ajit Singh Chahal
<Start Date				: -	5/30/2005 1:17:29 PM
<End Date				: -	
<Description				: - 	Model for Regualr Commission Setup - Agency.
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

namespace Cms.Model.Maintenance.Accounting
{
	/// <summary>
	/// Database Model for ACT_REG_COMM_SETUP.
	/// </summary>
	public class ClsRegCommSetup_AgencyInfo : Cms.Model.ClsCommonModel
	{
		
		private const string ACT_REG_COMM_SETUP = "ACT_REG_COMM_SETUP";
		public ClsRegCommSetup_AgencyInfo()
		{
			base.dtModel.TableName = "ACT_REG_COMM_SETUP";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table ACT_REG_COMM_SETUP
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("COMM_ID",typeof(int));
			base.dtModel.Columns.Add("AGENCY_ID",typeof(int));
			base.dtModel.Columns.Add("COUNTRY_ID",typeof(int));
			base.dtModel.Columns.Add("STATE_ID",typeof(int));
			base.dtModel.Columns.Add("LOB_ID",typeof(int));
			base.dtModel.Columns.Add("SUB_LOB_ID",typeof(int));
			base.dtModel.Columns.Add("CLASS_RISK",typeof(int));
			base.dtModel.Columns.Add("TERM",typeof(string));
			base.dtModel.Columns.Add("EFFECTIVE_FROM_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("EFFECTIVE_TO_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("COMMISSION_PERCENT",typeof(double));
			base.dtModel.Columns.Add("COMMISSION_TYPE",typeof(string));
			base.dtModel.Columns.Add("REMARKS",typeof(string));
			base.dtModel.Columns.Add("AMOUNT_TYPE",typeof(string));
		}
		#region Database schema details
		// model for database field COMM_ID(int)
		public int COMM_ID
		{
			get
			{
				return base.dtModel.Rows[0]["COMM_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["COMM_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COMM_ID"] = value;
			}
		}
		// model for database field AGENCY_ID(int)
		public int AGENCY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["AGENCY_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["AGENCY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AGENCY_ID"] = value;
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
		// model for database field CLASS_RISK(int)
		public int CLASS_RISK
		{
			get
			{
				return base.dtModel.Rows[0]["CLASS_RISK"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CLASS_RISK"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CLASS_RISK"] = value;
			}
		}
		// model for database field TERM(string)
		public string TERM
		{
			get
			{
				return base.dtModel.Rows[0]["TERM"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["TERM"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TERM"] = value;
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
		public double COMMISSION_PERCENT
		{
			get
			{
				return base.dtModel.Rows[0]["COMMISSION_PERCENT"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["COMMISSION_PERCENT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COMMISSION_PERCENT"] = value;
			}
		}
		// model for database field COMMISSION_TYPE(string)
		public string COMMISSION_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["COMMISSION_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["COMMISSION_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["COMMISSION_TYPE"] = value;
			}
		}
		// model for database field REMARKS(string)
		public string REMARKS
		{
			get
			{
				return base.dtModel.Rows[0]["REMARKS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["REMARKS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REMARKS"] = value;
			}
		}
		// model for database field AMOUNT_TYPE(string)
		public string AMOUNT_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["AMOUNT_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["AMOUNT_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["AMOUNT_TYPE"] = value;
			}
		}
		#endregion
	}
}
