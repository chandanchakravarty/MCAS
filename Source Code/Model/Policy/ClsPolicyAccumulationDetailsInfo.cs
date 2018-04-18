/******************************************************************************************
<Author				: - Kuldeep Saxena
<Start Date			: -	11/Feb/2012
<End Date			: -	
<Description		: - Database Model for POL_ACCUMULATION_DETAILS
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: - 
<Modified By		: - 
<Purpose			: - 
*******************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cms.Model;

namespace Cms.Model.Policy
{
   public class ClsPolicyAccumulationDetailsInfo : Cms.Model.ClsCommonModel
    {

        private const string MNT_ACCUMULATION_REFERENCE = "POL_ACCUMULATION_DETAILS";
           public ClsPolicyAccumulationDetailsInfo()
		{
            base.dtModel.TableName = "POL_ACCUMULATION_DETAILS";	// setting table name for data table that holds property values.
            this.AddColumns();								// add columns of the database table POL_ACCUMULATION_DETAILS
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
            base.dtModel.Columns.Add("ACCUMULATION_ID", typeof(int));
            base.dtModel.Columns.Add("ACCUMULATION_CODE", typeof(string));
            base.dtModel.Columns.Add("ACC_REF_NO", typeof(string));
            base.dtModel.Columns.Add("TOTAL_NO_OF_POLICIES", typeof(int));
            base.dtModel.Columns.Add("OWN_RETENTION_LIMIT", typeof(double));
            base.dtModel.Columns.Add("TREATY_CAPACITY_LIMIT", typeof(double));
            base.dtModel.Columns.Add("ACCUMULATION_LIMIT_AVAILABLE", typeof(double));
            base.dtModel.Columns.Add("TOTAL_SUM_INSURED", typeof(double));
            base.dtModel.Columns.Add("FACULTATIVE_RI", typeof(double));
            base.dtModel.Columns.Add("GROSS_RETAINED_SUM_INSURED", typeof(double));
            base.dtModel.Columns.Add("OWN_RETENTION", typeof(double));
            base.dtModel.Columns.Add("QUOTA_SHARE", typeof(double));
            base.dtModel.Columns.Add("FIRST_SURPLUS", typeof(double));
            base.dtModel.Columns.Add("OWN_ABSOLUTE_NET_RETENSTION", typeof(double));

            base.dtModel.Columns.Add("POLICY_ID", typeof(int));
            base.dtModel.Columns.Add("POLICY_VERSION_ID", typeof(int));
            base.dtModel.Columns.Add("CUSTOMER_ID", typeof(int));
            
   		}
		#region Database schema details
        // model for database field [ACC_ID] INT
        public int ACCUMULATION_ID
        {
            get
            {
                return base.dtModel.Rows[0]["ACCUMULATION_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ACCUMULATION_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["ACCUMULATION_ID"] = value;
            }
        }

        // model for database field [POLICY_ID] INT
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

        // model for database field [POLICY_VERSION_ID] SMALLINT
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

        // model for database field [CUSTOMER_ID] INT
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

        // model for database field [ACC_REF_NO] NVARCHAR
        public string ACCUMULATION_CODE
		{
			get
			{
                return base.dtModel.Rows[0]["ACCUMULATION_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ACCUMULATION_CODE"].ToString();
			}
			set
			{
                base.dtModel.Rows[0]["ACCUMULATION_CODE"] = value;
			}
		}
        // model for database field [LOBID] int
        public string ACC_REF_NO
        {
            get
            {
                return base.dtModel.Rows[0]["ACC_REF_NO"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ACC_REF_NO"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["ACC_REF_NO"] = value;
            }
        }
        // model for database field CRITERIA_ID(int)
        public int TOTAL_NO_OF_POLICIES
		{
			get
			{
                return base.dtModel.Rows[0]["TOTAL_NO_OF_POLICIES"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["TOTAL_NO_OF_POLICIES"].ToString());
			}
			set
			{
                base.dtModel.Rows[0]["TOTAL_NO_OF_POLICIES"] = value;
			}
		}

        // model for database field [CRITERIA_VALUE] NVARCHAR
        public double OWN_RETENTION_LIMIT
		{
			get
			{
                return base.dtModel.Rows[0]["OWN_RETENTION_LIMIT"] == DBNull.Value ? 0: double.Parse(base.dtModel.Rows[0]["OWN_RETENTION_LIMIT"].ToString());
			}
			set
			{
                base.dtModel.Rows[0]["OWN_RETENTION_LIMIT"] = value;
			}
		}
        // model for database field  [TREATY_CAPACITY_LIMIT] DECIMAL
        public double TREATY_CAPACITY_LIMIT
		{
			get
			{
                return base.dtModel.Rows[0]["TREATY_CAPACITY_LIMIT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["TREATY_CAPACITY_LIMIT"].ToString());
			}
			set
			{
                base.dtModel.Rows[0]["TREATY_CAPACITY_LIMIT"] = value;
			}
		}

        // model for database [USED_LIMIT] DECIMAL
        public double ACCUMULATION_LIMIT_AVAILABLE
        {
            get
            {
                return base.dtModel.Rows[0]["ACCUMULATION_LIMIT_AVAILABLE"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["ACCUMULATION_LIMIT_AVAILABLE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["ACCUMULATION_LIMIT_AVAILABLE"] = value;
            }
        }

        public double TOTAL_SUM_INSURED
        {
            get
            {
                return base.dtModel.Rows[0]["TOTAL_SUM_INSURED"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["TOTAL_SUM_INSURED"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["TOTAL_SUM_INSURED"] = value;
            }
        }
        public double FACULTATIVE_RI
        {
            get
            {
                return base.dtModel.Rows[0]["FACULTATIVE_RI"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["FACULTATIVE_RI"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["FACULTATIVE_RI"] = value;
            }
        }

        public double GROSS_RETAINED_SUM_INSURED
        {
            get
            {
                return base.dtModel.Rows[0]["GROSS_RETAINED_SUM_INSURED"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["GROSS_RETAINED_SUM_INSURED"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["GROSS_RETAINED_SUM_INSURED"] = value;
            }
        }
        public double OWN_RETENTION
        {
            get
            {
                return base.dtModel.Rows[0]["OWN_RETENTION"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["OWN_RETENTION"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["OWN_RETENTION"] = value;
            }
        }

        public double QUOTA_SHARE
        {
            get
            {
                return base.dtModel.Rows[0]["QUOTA_SHARE"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["QUOTA_SHARE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["QUOTA_SHARE"] = value;
            }
        }
        public double FIRST_SURPLUS
        {
            get
            {
                return base.dtModel.Rows[0]["FIRST_SURPLUS"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["FIRST_SURPLUS"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["FIRST_SURPLUS"] = value;
            }
        }

        public double OWN_ABSOLUTE_NET_RETENSTION
        {
            get
            {
                return base.dtModel.Rows[0]["OWN_ABSOLUTE_NET_RETENSTION"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["OWN_ABSOLUTE_NET_RETENSTION"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["OWN_ABSOLUTE_NET_RETENSTION"] = value;
            }
        }

		#endregion
    }
}
