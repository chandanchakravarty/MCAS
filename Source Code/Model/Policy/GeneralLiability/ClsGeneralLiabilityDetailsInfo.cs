/******************************************************************************************
<Author					: -   Ravindra Gupta
<Start Date				: -	  04-04-2006
<End Date				: -	
<Description			: -   Models POL_GENERAL_LIABILITY_DETAILS
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
	/// Summary description for ClsGeneralLiabilityDetailsInfo.
	/// </summary>
	public class ClsGeneralLiabilityDetailsInfo:Cms.Model.ClsCommonModel 
	{
		public ClsGeneralLiabilityDetailsInfo()
		{
			base.dtModel.TableName ="POL_GENERAL_LIABILITY_DETAILS";
			this.AddColumns ();
			base.dtModel.Rows.Add(base.dtModel.NewRow());	
		}

		#region AddColumns Function --To Add Columns To DataTable
		private void AddColumns()
		{

			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_GEN_ID",typeof(int));
			base.dtModel.Columns.Add("LOCATION_ID",typeof(int));
			base.dtModel.Columns.Add("CLASS_CODE",typeof(string));
			base.dtModel.Columns.Add("BUSINESS_DESCRIPTION",typeof(string));
			base.dtModel.Columns.Add("COVERAGE_TYPE",typeof(int));
			base.dtModel.Columns.Add("COVERAGE_FORM",typeof(int));
			base.dtModel.Columns.Add("EXPOSURE_BASE",typeof(int));
			base.dtModel.Columns.Add("EXPOSURE",typeof(int));
			base.dtModel.Columns.Add("RATE",typeof(int));	

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

		// model for database field POLICY_GEN_ID(int)
		public int POLICY_GEN_ID
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_GEN_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["POLICY_GEN_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_GEN_ID"] = value;
			}
		}		
		// model for database field LOCATION_ID(int)
		public int LOCATION_ID
		{
			get
			{
				return base.dtModel.Rows[0]["LOCATION_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LOCATION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LOCATION_ID"] = value;
			}
		}
		// model for database field CLASS_CODE(string)
		public string CLASS_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["CLASS_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CLASS_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CLASS_CODE"] = value;
			}
		}
		// model for database field BUSINESS_DESCRIPTION(string)
		public string BUSINESS_DESCRIPTION
		{
			get
			{
				return base.dtModel.Rows[0]["BUSINESS_DESCRIPTION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["BUSINESS_DESCRIPTION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BUSINESS_DESCRIPTION"] = value;
			}
		}
		// model for database field COVERAGE_TYPE(int)
		public int COVERAGE_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["COVERAGE_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["COVERAGE_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COVERAGE_TYPE"] = value;
			}
		}
		// model for database field COVERAGE_FORM(int)
		public int COVERAGE_FORM
		{
			get
			{
				return base.dtModel.Rows[0]["COVERAGE_FORM"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["COVERAGE_FORM"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COVERAGE_FORM"] = value;
			}
		}
		// model for database field EXPOSURE_BASE(int)
		public int EXPOSURE_BASE
		{
			get
			{
				return base.dtModel.Rows[0]["EXPOSURE_BASE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["EXPOSURE_BASE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EXPOSURE_BASE"] = value;
			}
		}
		// model for database field EXPOSURE(int)
		public int EXPOSURE
		{
			get
			{
				return base.dtModel.Rows[0]["EXPOSURE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["EXPOSURE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EXPOSURE"] = value;
			}
		}
		// model for database field RATE(int)
		public int RATE
		{
			get
			{
				return base.dtModel.Rows[0]["RATE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["RATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RATE"] = value;
			}
		}
		#endregion
	}
}
