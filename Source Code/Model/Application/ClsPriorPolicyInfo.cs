/******************************************************************************************
<Author				: -   Vijay Joshi
<Start Date				: -	4/26/2005 12:30:12 PM
<End Date				: -	
<Description				: - 	Creates the Model class for Prior Policy
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: -  4/5/2005
<Modified By				: - Anurag Verma
<Purpose				: - Removing use of app_id and app_version_id
*******************************************************************************************/ 
using System;
using System.Data;
using Cms.Model;
namespace Cms.Model.Application
{
	/// <summary>
	/// Database Model for APP_PRIOR_CARRIER_INFO.
	/// </summary>
	public class ClsPriorPolicyInfo : Cms.Model.ClsCommonModel
	{
		private const string APP_PRIOR_CARRIER_INFO = "APP_PRIOR_CARRIER_INFO";
		public ClsPriorPolicyInfo()
		{
			base.dtModel.TableName = "APP_PRIOR_CARRIER_INFO";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table APP_PRIOR_CARRIER_INFO
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("APP_PRIOR_CARRIER_INFO_ID",typeof(int));
			
			base.dtModel.Columns.Add("OLD_POLICY_NUMBER",typeof(string));
			base.dtModel.Columns.Add("CARRIER",typeof(string));
			base.dtModel.Columns.Add("LOB",typeof(string));
			base.dtModel.Columns.Add("SUB_LOB",typeof(string));
			base.dtModel.Columns.Add("EFF_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("EXP_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("POLICY_CATEGORY",typeof(string));
			base.dtModel.Columns.Add("POLICY_TERM_CODE",typeof(string));
			base.dtModel.Columns.Add("POLICY_TYPE",typeof(string));
			base.dtModel.Columns.Add("YEARS_PRIOR_COMP",typeof(int));
			base.dtModel.Columns.Add("ACTUAL_PREM",typeof(double));
			base.dtModel.Columns.Add("ASSIGNEDRISKYN",typeof(string));
			base.dtModel.Columns.Add("PRIOR_PRODUCER_INFO_ID",typeof(int));
			base.dtModel.Columns.Add("RISK_NEW_AGENCY",typeof(string));
			base.dtModel.Columns.Add("MOD_FACTOR",typeof(string));
			base.dtModel.Columns.Add("ANNUAL_PREM",typeof(string));
			base.dtModel.Columns.Add("PRIOR_BI_CSL_LIMIT",typeof(string));//Added for Itrack Issue 6449 on 23 Oct 09
		}
		#region Database schema details
		// model for database field CUSTOMER_ID(int)
		public int CUSTOMER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CUSTOMER_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_ID"] = value;
			}
		}
		// model for database field APP_PRIOR_CARRIER_INFO_ID(int)
		public int APP_PRIOR_CARRIER_INFO_ID
		{
			get
			{
				return base.dtModel.Rows[0]["APP_PRIOR_CARRIER_INFO_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["APP_PRIOR_CARRIER_INFO_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APP_PRIOR_CARRIER_INFO_ID"] = value;
			}
		}
		// model for database field OLD_POLICY_NUMBER(string)
		public string OLD_POLICY_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["OLD_POLICY_NUMBER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["OLD_POLICY_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["OLD_POLICY_NUMBER"] = value;
			}
		}
		// model for database field CARRIER(string)
		public string CARRIER
		{
			get
			{
				return base.dtModel.Rows[0]["CARRIER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CARRIER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CARRIER"] = value;
			}
		}
		// model for database field LOB(string)
		public string LOB
		{
			get
			{
				return base.dtModel.Rows[0]["LOB"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LOB"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOB"] = value;
			}
		}
		// model for database field SUB_LOB(string)
		public string SUB_LOB
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_LOB"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SUB_LOB"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SUB_LOB"] = value;
			}
		}
		// model for database field EFF_DATE(DateTime)
		public DateTime EFF_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["EFF_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["EFF_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EFF_DATE"] = value;
			}
		}
		// model for database field EXP_DATE(DateTime)
		public DateTime EXP_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["EXP_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["EXP_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EXP_DATE"] = value;
			}
		}
		// model for database field POLICY_CATEGORY(string)
		public string POLICY_CATEGORY
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_CATEGORY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["POLICY_CATEGORY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_CATEGORY"] = value;
			}
		}
		// model for database field POLICY_TERM_CODE(string)
		public string POLICY_TERM_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_TERM_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["POLICY_TERM_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_TERM_CODE"] = value;
			}
		}
		// model for database field POLICY_TYPE(string)
		public string POLICY_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["POLICY_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_TYPE"] = value;
			}
		}
		// model for database field YEARS_PRIOR_COMP(int)
		public int YEARS_PRIOR_COMP
		{
			get
			{
				return base.dtModel.Rows[0]["YEARS_PRIOR_COMP"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["YEARS_PRIOR_COMP"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["YEARS_PRIOR_COMP"] = value;
			}
		}
		// model for database field ACTUAL_PREM(double)
		public double ACTUAL_PREM
		{
			get
			{
				return base.dtModel.Rows[0]["ACTUAL_PREM"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["ACTUAL_PREM"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ACTUAL_PREM"] = value;
			}
		}
		// model for database field ASSIGNEDRISKYN(string)
		public string ASSIGNEDRISKYN
		{
			get
			{
				return base.dtModel.Rows[0]["ASSIGNEDRISKYN"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ASSIGNEDRISKYN"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ASSIGNEDRISKYN"] = value;
			}
		}
		// model for database field PRIOR_PRODUCER_INFO_ID(int)
		public int PRIOR_PRODUCER_INFO_ID
		{
			get
			{
				return base.dtModel.Rows[0]["PRIOR_PRODUCER_INFO_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["PRIOR_PRODUCER_INFO_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PRIOR_PRODUCER_INFO_ID"] = value;
			}
		}
		// model for database field RISK_NEW_AGENCY(string)
		public string RISK_NEW_AGENCY
		{
			get
			{
				return base.dtModel.Rows[0]["RISK_NEW_AGENCY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["RISK_NEW_AGENCY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["RISK_NEW_AGENCY"] = value;
			}
		}
		// model for database field MOD_FACTOR(string)
		public string MOD_FACTOR
		{
			get
			{
				return base.dtModel.Rows[0]["MOD_FACTOR"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MOD_FACTOR"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MOD_FACTOR"] = value;
			}
		}
		// model for database field ANNUAL_PREM(string)
		public string ANNUAL_PREM
		{
			get
			{
				return base.dtModel.Rows[0]["ANNUAL_PREM"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANNUAL_PREM"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANNUAL_PREM"] = value;
			}
		}
		//Added for Itrack Issue 6449 on 23 Oct 09
		// model for database field PRIOR_BI_CSL_LIMIT(string)
		public string PRIOR_BI_CSL_LIMIT
		{
			get
			{
				return base.dtModel.Rows[0]["PRIOR_BI_CSL_LIMIT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PRIOR_BI_CSL_LIMIT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PRIOR_BI_CSL_LIMIT"] = value;
			}
		}
		#endregion
	}
}
