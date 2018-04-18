/******************************************************************************************
<Author					: -   Ravindra Gupta
<Start Date				: -	  03/07/2006
<End Date				: -	
<Description			: -   Models APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES
<Review Date			: - 
<Reviewed By			: - 	
Modification History

*******************************************************************************************/ 
using System;
using System.Data;
using Cms.Model;

namespace Cms.Model.Application
{
	/// <summary>
	/// Summary description for ClsUnderlyingPolicyCoveragesInfo.
	/// </summary>
	public class ClsUnderlyingPolicyCoveragesInfo : Cms.Model.ClsCommonModel
	{
		public ClsUnderlyingPolicyCoveragesInfo()
		{
			base.dtModel.TableName ="APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES";
			this.AddColumns ();
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}

		#region AddColumns Function --To Add Columns To DataTable
		private void AddColumns()
		{

			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("APP_ID",typeof(int));
			base.dtModel.Columns.Add("APP_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_NUMBER",typeof(string));
			base.dtModel.Columns.Add("COVERAGE_DESC",typeof(string));
			base.dtModel.Columns.Add("COVERAGE_AMOUNT",typeof(string));
			//base.dtModel.Columns.Add("COVERAGE_AMOUNT",typeof(decimal));
			base.dtModel.Columns.Add("POLICY_TEXT",typeof(string));			
			base.dtModel.Columns.Add("IS_POLICY",typeof(bool));
			base.dtModel.Columns.Add("COV_CODE",typeof(string));
			base.dtModel.Columns.Add("POLICY_COMPANY",typeof(string));
			base.dtModel.Columns.Add("COVERAGE_TYPE",typeof(string));
		}
		#endregion 

		#region Database schema details
		
	
		
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
		// model for database field APP_ID(int)
		public int APP_ID
		{
			get
			{
				return base.dtModel.Rows[0]["APP_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["APP_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APP_ID"] = value;
			}
		}
		// model for database field APP_VERSION_ID(int)
		public int APP_VERSION_ID
		{
			get
			{
				return base.dtModel.Rows[0]["APP_VERSION_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["APP_VERSION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APP_VERSION_ID"] = value;
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

		public string COVERAGE_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["COVERAGE_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["COVERAGE_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["COVERAGE_DESC"] = value;
			}
		}

		/*public decimal COVERAGE_AMOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["COVERAGE_AMOUNT"] == DBNull.Value ? Convert.ToDecimal(null) : decimal.Parse(base.dtModel.Rows[0]["COVERAGE_AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COVERAGE_AMOUNT"] = value;
			}
		}*/

		public string COVERAGE_AMOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["COVERAGE_AMOUNT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["COVERAGE_AMOUNT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["COVERAGE_AMOUNT"] = value;
			}
		}		


		public string POLICY_TEXT
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_TEXT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["POLICY_TEXT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_TEXT"] = value;
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
		public string COV_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["COV_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["COV_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["COV_CODE"] = value;
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
		/*model for database field EXCLUDE_UNINSURED_MOTORIST
		
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
		}*/
		
		// model for database field COVERAGE_TYPE(int)

		public string COVERAGE_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["COVERAGE_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["COVERAGE_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["COVERAGE_TYPE"] = value;
			}
		}


		#endregion

	}
}
