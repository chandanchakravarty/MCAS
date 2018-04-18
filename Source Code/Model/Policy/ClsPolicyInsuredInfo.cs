/******************************************************************************************
<Author				: -   Vijay Arora
<Start Date			: -	10/28/2005 1:02:19 PM
<End Date			: -	
<Description		: - 	Model Class for Policy Insured
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: - 
<Modified By		: - 
<Purpose			: - 
*******************************************************************************************/ 
using System;
using System.Data;
using Cms.Model;
namespace Cms.Model.Policy

{
	/// <summary>
	/// Database Model for POL_APPLICANT_LIST.
	/// </summary>
	public class ClsPolicyInsuredInfo : Cms.Model.ClsCommonModel
	{
		private const string POL_APPLICANT_LIST = "POL_APPLICANT_LIST";
		public ClsPolicyInsuredInfo()
		{
			base.dtModel.TableName = "POL_APPLICANT_LIST";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table POL_APPLICANT_LIST
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("APPLICANT_ID",typeof(int));
			base.dtModel.Columns.Add("APP_ID",typeof(int));
			base.dtModel.Columns.Add("APP_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("IS_PRIMARY_APPLICANT",typeof(int));
            base.dtModel.Columns.Add("COMMISSION_PERCENT", typeof(double));
            base.dtModel.Columns.Add("FEES_PERCENT", typeof(double));
            base.dtModel.Columns.Add("PRO_LABORE_PERCENT", typeof(double));
		}
		#region Database schema details
		// model for database field POLICY_ID(int)
		public int POLICY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["POLICY_ID"].ToString());
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
				return base.dtModel.Rows[0]["POLICY_VERSION_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["POLICY_VERSION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_VERSION_ID"] = value;
			}
		}
		// model for database field CUSTOMER_ID(int)
		public int CUSTOMER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CUSTOMER_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_ID"] = value;
			}
		}
		// model for database field APPLICANT_ID(int)
		public int APPLICANT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["APPLICANT_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["APPLICANT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APPLICANT_ID"] = value;
			}
		}
		// model for database field APP_ID(int)
		public int APP_ID
		{
			get
			{
				return base.dtModel.Rows[0]["APP_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["APP_ID"].ToString());
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
				return base.dtModel.Rows[0]["APP_VERSION_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["APP_VERSION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APP_VERSION_ID"] = value;
			}
		}
		// model for database field IS_PRIMARY_APPLICANT(int)
		public int IS_PRIMARY_APPLICANT
		{
			get
			{
				return base.dtModel.Rows[0]["IS_PRIMARY_APPLICANT"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["IS_PRIMARY_APPLICANT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["IS_PRIMARY_APPLICANT"] = value;
			}
		}

        public double COMMISSION_PERCENT
        {
            get
            {
                return base.dtModel.Rows[0]["COMMISSION_PERCENT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["COMMISSION_PERCENT"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["COMMISSION_PERCENT"] = value;
            }
        }

        public double FEES_PERCENT
        {
            get
            {
                return base.dtModel.Rows[0]["FEES_PERCENT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["FEES_PERCENT"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["FEES_PERCENT"] = value;
            }
        }
        public double PRO_LABORE_PERCENT
        {
            get
            {
                return base.dtModel.Rows[0]["PRO_LABORE_PERCENT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["PRO_LABORE_PERCENT"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["PRO_LABORE_PERCENT"] = value;
            }
        }

		#endregion
	}
}
