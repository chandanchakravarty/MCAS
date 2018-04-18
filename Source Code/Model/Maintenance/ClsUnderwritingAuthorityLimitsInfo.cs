using System;
using System.Data;
using Cms.Model;
namespace Cms.Model.Maintenance
{
    public class ClsUnderwritingAuthorityLimitsInfo : Cms.Model.ClsCommonModel
    {
        /// <summary>
        /// Database Model for MNT_UNDERWRITING_CLAIM_LIMITS.
        /// </summary>
         private const string MNT_UNDERWRITING_CLAIM_LIMITS = "MNT_UNDERWRITING_CLAIM_LIMITS";
         public ClsUnderwritingAuthorityLimitsInfo()
            {
                base.dtModel.TableName = "MNT_UNDERWRITING_CLAIM_LIMITS";		// setting table name for data table that holds property values.
                this.AddColumns();								// add columns of the database table MNT_TAX_ENTITY_LIST
                base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
            }
            private void AddColumns()
            {
                base.dtModel.Columns.Add("ASSIGN_ID", typeof(int));
                base.dtModel.Columns.Add("USER_ID", typeof(int));
                base.dtModel.Columns.Add("COUNTRY_ID", typeof(int));
                base.dtModel.Columns.Add("LOB_ID", typeof(int));
                base.dtModel.Columns.Add("PML_LIMIT", typeof(decimal));
                base.dtModel.Columns.Add("PREMIUM_APPROVAL_LIMIT", typeof(decimal));
                base.dtModel.Columns.Add("CLAIM_RESERVE_LIMIT", typeof(decimal));
                base.dtModel.Columns.Add("CLAIM_REOPEN", typeof(string));
                base.dtModel.Columns.Add("CLAIM_SETTLMENT_LIMIT", typeof(decimal));
                //base.dtModel.Columns.Add("CREATED_BY", typeof(int));
                //base.dtModel.Columns.Add("CREATED_DATETIME", typeof(DateTime));
                //base.dtModel.Columns.Add("MODIFIED_BY", typeof(int));
                //base.dtModel.Columns.Add("LAST_UPDATED_DATETIME", typeof(DateTime));

                //Two dates added by Ruchika Chauhan on 3-Feb-2012 for TFS Bug # 3322
                base.dtModel.Columns.Add("EffectiveDate", typeof(DateTime));
                base.dtModel.Columns.Add("EndDate", typeof(DateTime));
            }
            #region Database schema details
            // model for database field ASSIGN_ID(int)
            public int ASSIGN_ID
            {
                get
                {
                    return base.dtModel.Rows[0]["ASSIGN_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["ASSIGN_ID"].ToString());
                }
                set
                {
                    base.dtModel.Rows[0]["ASSIGN_ID"] = value;
                }
            }
            public int USER_ID
            {
                get
                {
                    return base.dtModel.Rows[0]["USER_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["USER_ID"].ToString());
                }
                set
                {
                    base.dtModel.Rows[0]["USER_ID"] = value;
                }
            }
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
            // model for database field PML_LIMIT(decimal)
            public decimal PML_LIMIT
            {
                get
                {
                    return base.dtModel.Rows[0]["PML_LIMIT"] == DBNull.Value ? Convert.ToDecimal(null) : decimal.Parse(base.dtModel.Rows[0]["PML_LIMIT"].ToString());
                }
                set
                {
                    base.dtModel.Rows[0]["PML_LIMIT"] = value;
                }
            }
            // model for database field PREMIUM_APPROVAL_LIMIT(decimal)
            public decimal PREMIUM_APPROVAL_LIMIT
            {
                get
                {
                    return base.dtModel.Rows[0]["PREMIUM_APPROVAL_LIMIT"] == DBNull.Value ? Convert.ToDecimal(null) : decimal.Parse(base.dtModel.Rows[0]["PREMIUM_APPROVAL_LIMIT"].ToString());
                }
                set
                {
                    base.dtModel.Rows[0]["PREMIUM_APPROVAL_LIMIT"] = value;
                }
            }
            // model for database field CLAIM_RESERVE_LIMIT(decimal)
            public decimal CLAIM_RESERVE_LIMIT
            {
                get
                {
                    return base.dtModel.Rows[0]["CLAIM_RESERVE_LIMIT"] == DBNull.Value ? Convert.ToDecimal(null) : decimal.Parse(base.dtModel.Rows[0]["CLAIM_RESERVE_LIMIT"].ToString());
                }
                set
                {
                    base.dtModel.Rows[0]["CLAIM_RESERVE_LIMIT"] = value;
                }
            }
            // model for database field CLAIM_SETTLMENT_LIMIT(decimal)
            public decimal CLAIM_SETTLMENT_LIMIT
            {
                get
                {
                    return base.dtModel.Rows[0]["CLAIM_SETTLMENT_LIMIT"] == DBNull.Value ? Convert.ToDecimal(null) : decimal.Parse(base.dtModel.Rows[0]["CLAIM_SETTLMENT_LIMIT"].ToString());
                }
                set
                {
                    base.dtModel.Rows[0]["CLAIM_SETTLMENT_LIMIT"] = value;
                }
            }

            public string CLAIM_REOPEN
            {
                get
                {
                    return base.dtModel.Rows[0]["CLAIM_REOPEN"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CLAIM_REOPEN"].ToString();
                }
                set
                {
                    base.dtModel.Rows[0]["CLAIM_REOPEN"] = value;
                }
            }

            public DateTime EffectiveDate
            {
                get
                {
                    return base.dtModel.Rows[0]["EffectiveDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["EffectiveDate"].ToString());
                }
                set
                {
                    base.dtModel.Rows[0]["EffectiveDate"] = value;
                }
            }

            public DateTime EndDate
            {
                get
                {
                    return base.dtModel.Rows[0]["EndDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["EndDate"].ToString());
                }
                set
                {
                    base.dtModel.Rows[0]["EndDate"] = value;
                }
            }
            #endregion
      
    }
}    