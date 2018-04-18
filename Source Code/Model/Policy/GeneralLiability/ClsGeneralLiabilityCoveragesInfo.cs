/******************************************************************************************
<Author					: -   Ravindra Gupta
<Start Date				: -	  04-03-2006
<End Date				: -	
<Description			: -   Models POL_GENERAL_COVERAGE_LIMITS
<Review Date			: - 
<Reviewed By			: - 	
Modification History

*******************************************************************************************/ 
using System;
using System.Data;
using Cms.Model;

namespace Cms.Model.Policy.GeneralLiability
{
	/// <summary>
	/// Summary description for ClsGeneralLiabilityCoveragesInfo.
	/// </summary>
	public class ClsGeneralLiabilityCoveragesInfo :Cms.Model.ClsCommonModel
	{
		public ClsGeneralLiabilityCoveragesInfo()
		{
			base.dtModel.TableName ="POL_GENERAL_COVERAGE_LIMITS";
			this.AddColumns ();
			base.dtModel.Rows.Add(base.dtModel.NewRow());	
		}

		#region AddColumns Function --To Add Columns To DataTable
		private void AddColumns()
		{

			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("COVERAGE_L_AMOUNT",typeof(decimal));
			base.dtModel.Columns.Add("COVERAGE_L_ID",typeof(int));
			base.dtModel.Columns.Add("COVERAGE_L_AGGREGATE",typeof(decimal));
			base.dtModel.Columns.Add("COVERAGE_O_AMOUNT",typeof(decimal));
			base.dtModel.Columns.Add("COVERAGE_O_ID",typeof(int));
			base.dtModel.Columns.Add("COVERAGE_O_AGGREGATE",typeof(decimal));
			base.dtModel.Columns.Add("COVERAGE_M_EACH_PERSON_AMOUNT",typeof(decimal));
			base.dtModel.Columns.Add("COVERAGE_M_EACH_PERSON_ID",typeof(int));
			base.dtModel.Columns.Add("COVERAGE_M_EACH_OCC_AMOUNT",typeof(decimal));
			base.dtModel.Columns.Add("COVERAGE_M_EACH_OCC_ID",typeof(int));
			base.dtModel.Columns.Add("TOTAL_GENERAL_AGGREGATE",typeof(decimal));

		}
		#endregion 

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
		
		// model for database field COVERAGE_L_AMOUNT(decimal)
		public decimal COVERAGE_L_AMOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["COVERAGE_L_AMOUNT"] == DBNull.Value ? Convert.ToInt32(null) : decimal.Parse(base.dtModel.Rows[0]["COVERAGE_L_AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COVERAGE_L_AMOUNT"] = value;
			}
		}

		// model for database field COVERAGE_L_ID(int)
		public int COVERAGE_L_ID
		{
			get
			{
				return base.dtModel.Rows[0]["COVERAGE_L_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["COVERAGE_L_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COVERAGE_L_ID"] = value;
			}
		}
		
		// model for database field COVERAGE_L_AGGREGATE(decimal)
		public decimal COVERAGE_L_AGGREGATE
		{
			get
			{
				return base.dtModel.Rows[0]["COVERAGE_L_AGGREGATE"] == DBNull.Value ? Convert.ToInt32(null) : decimal.Parse(base.dtModel.Rows[0]["COVERAGE_L_AGGREGATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COVERAGE_L_AGGREGATE"] = value;
			}
		}
		
		// model for database field COVERAGE_O_AMOUNT(decimal)
		public decimal COVERAGE_O_AMOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["COVERAGE_O_AMOUNT"] == DBNull.Value ? Convert.ToInt32(null) : decimal.Parse(base.dtModel.Rows[0]["COVERAGE_O_AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COVERAGE_O_AMOUNT"] = value;
			}
		}

		// model for database field COVERAGE_O_ID(int)
		public int COVERAGE_O_ID
		{
			get
			{
				return base.dtModel.Rows[0]["COVERAGE_O_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["COVERAGE_O_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COVERAGE_O_ID"] = value;
			}
		}
		
		// model for database field COVERAGE_O_AGGREGATE(decimal)
		public decimal COVERAGE_O_AGGREGATE
		{
			get
			{
				return base.dtModel.Rows[0]["COVERAGE_O_AGGREGATE"] == DBNull.Value ? Convert.ToInt32(null) : decimal.Parse(base.dtModel.Rows[0]["COVERAGE_O_AGGREGATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COVERAGE_O_AGGREGATE"] = value;
			}
		}

		// model for database field COVERAGE_M_EACH_PERSON_AMOUNT(decimal)
		public decimal COVERAGE_M_EACH_PERSON_AMOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["COVERAGE_M_EACH_PERSON_AMOUNT"] == DBNull.Value ? Convert.ToInt32(null) : decimal.Parse(base.dtModel.Rows[0]["COVERAGE_M_EACH_PERSON_AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COVERAGE_M_EACH_PERSON_AMOUNT"] = value;
			}
		}

		// model for database field COVERAGE_L_ID(int)
		public int COVERAGE_M_EACH_PERSON_ID
		{
			get
			{
				return base.dtModel.Rows[0]["COVERAGE_M_EACH_PERSON_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["COVERAGE_M_EACH_PERSON_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COVERAGE_M_EACH_PERSON_ID"] = value;
			}
		}
		
		// model for database field COVERAGE_M_EACH_OCC_AMOUNT(decimal)
		public decimal COVERAGE_M_EACH_OCC_AMOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["COVERAGE_M_EACH_OCC_AMOUNT"] == DBNull.Value ? Convert.ToInt32(null) : decimal.Parse(base.dtModel.Rows[0]["COVERAGE_M_EACH_OCC_AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COVERAGE_M_EACH_OCC_AMOUNT"] = value;
			}
		}

		// model for database field COVERAGE_M_EACH_OCC_ID(int)
		public int COVERAGE_M_EACH_OCC_ID
		{
			get
			{
				return base.dtModel.Rows[0]["COVERAGE_M_EACH_OCC_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["COVERAGE_M_EACH_OCC_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COVERAGE_M_EACH_OCC_ID"] = value;
			}
		}
		
		// model for database field TOTAL_GENERAL_AGGREGATE(decimal)
		public decimal TOTAL_GENERAL_AGGREGATE
		{
			get
			{
				return base.dtModel.Rows[0]["TOTAL_GENERAL_AGGREGATE"] == DBNull.Value ? Convert.ToInt32(null) : decimal.Parse(base.dtModel.Rows[0]["TOTAL_GENERAL_AGGREGATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["TOTAL_GENERAL_AGGREGATE"] = value;
			}
		}
		#endregion
	}
}
