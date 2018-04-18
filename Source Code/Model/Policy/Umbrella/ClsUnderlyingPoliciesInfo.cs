/******************************************************************************************
<Author					: -   Ravindra Gupta
<Start Date				: -	  03/22/2006
<End Date				: -	
<Description			: -   Models POL_UMBRELLA_UMDERLYING_POLICIES
<Review Date			: - 
<Reviewed By			: - 	
Modification History

*******************************************************************************************/ 
using System;
using System.Data;
using Cms.Model;

namespace Cms.Model.Policy.Umbrella
{
	/// <summary>
	/// Summary description for ScheduleOfUnderlyingInfo.
	/// </summary>
	public class ClsUnderlyingPoliciesInfo : Cms.Model.ClsCommonModel
	{
		public ClsUnderlyingPoliciesInfo()
		{
			base.dtModel.TableName ="POL_UMBRELLA_UNDERLYING_POLICIES";
			this.AddColumns ();
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}

		#region AddColumns Function --To Add Columns To DataTable
		private void AddColumns()
		{

			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_NUMBER",typeof(string));
			base.dtModel.Columns.Add("POLICY_LOB",typeof(string));
			base.dtModel.Columns.Add("POLICY_COMPANY",typeof(string));
			base.dtModel.Columns.Add("POLICY_TERMS",typeof(string));
			base.dtModel.Columns.Add("POLICY_START_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("POLICY_EXPIRATION_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("QUESTION",typeof(string));
			base.dtModel.Columns.Add("QUES_DESC",typeof(string));
			base.dtModel.Columns.Add("POLICY_PREMIUM",typeof(decimal));
			base.dtModel.Columns.Add("IS_POLICY",typeof(bool));
			base.dtModel.Columns.Add("STATE_ID",typeof(int));
			base.dtModel.Columns.Add("EXCLUDE_UNINSURED_MOTORIST",typeof(int));
			base.dtModel.Columns.Add("HAS_MOTORIST_PROTECTION",typeof(int));
			base.dtModel.Columns.Add("HAS_SIGNED_A9",typeof(int));
			base.dtModel.Columns.Add("LOWER_LIMITS",typeof(int));

		}
		#endregion 

		#region Database schema details
		

		public decimal POLICY_PREMIUM
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_PREMIUM"] == DBNull.Value ? Convert.ToDecimal(null) : decimal.Parse(base.dtModel.Rows[0]["POLICY_PREMIUM"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_PREMIUM"] = value;
			}
		}
		
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
		// model for database field POLICY_ID(int)
		public int POLICY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["POLICY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_ID"] = value;
			}
		}
		// model for database field POLICY_VERSION_ID(int)
		public int POLICY_VERSION_ID
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_VERSION_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["POLICY_VERSION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_VERSION_ID"] = value;
			}
		}

		public string POLICY_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_NUMBER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["POLICY_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_NUMBER"] = value;
			}
		}

		public string POLICY_LOB
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_LOB"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["POLICY_LOB"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_LOB"] = value;
			}
		}

		public string POLICY_COMPANY
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_COMPANY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["POLICY_COMPANY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_COMPANY"] = value;
			}
		}

		public string POLICY_TERMS
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_TERMS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["POLICY_TERMS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_TERMS"] = value;
			}
		}
		public DateTime POLICY_START_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_START_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["POLICY_START_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_START_DATE"] = value;
			}
		}

		public DateTime POLICY_EXPIRATION_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_EXPIRATION_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["POLICY_EXPIRATION_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_EXPIRATION_DATE"] = value;
			}
		}
		
		
		public string QUESTION
		{
			get
			{
				return base.dtModel.Rows[0]["QUESTION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["QUESTION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["QUESTION"] = value;
			}
		}


		public string QUES_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["QUES_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["QUES_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["QUES_DESC"] = value;
			}
		}
		public bool IS_POLICY
		{
			get
			{
				return base.dtModel.Rows[0]["IS_POLICY"] == DBNull.Value ? Convert.ToBoolean(null) :bool.Parse( base.dtModel.Rows[0]["IS_POLICY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["IS_POLICY"] = value;
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
		// model for database field EXCLUDE_UNINSURED_MOTORIST(int)
		public int EXCLUDE_UNINSURED_MOTORIST
		{
			get
			{
				return base.dtModel.Rows[0]["EXCLUDE_UNINSURED_MOTORIST"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["EXCLUDE_UNINSURED_MOTORIST"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EXCLUDE_UNINSURED_MOTORIST"] = value;
			}
		}
		// model for database field HAS_MOTORIST_PROTECTION(int)
		public int HAS_MOTORIST_PROTECTION
		{
			get
			{
				return base.dtModel.Rows[0]["HAS_MOTORIST_PROTECTION"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["HAS_MOTORIST_PROTECTION"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["HAS_MOTORIST_PROTECTION"] = value;
			}
		}

		// model for database field HAS_SIGNED_A9(int)
		public int HAS_SIGNED_A9
		{
			get
			{
				return base.dtModel.Rows[0]["HAS_SIGNED_A9"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["HAS_SIGNED_A9"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["HAS_SIGNED_A9"] = value;
			}
		}

		// model for database field LOWER_LIMITS(int)
		public int LOWER_LIMITS
		{
			get
			{
				return base.dtModel.Rows[0]["LOWER_LIMITS"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LOWER_LIMITS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LOWER_LIMITS"] = value;
			}
		}

		#endregion
	}
}
