using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Cms.Model;

namespace Cms.Model.Policy
{
    public class ClsVoyageInformationInfo: ClsCommonModel
    {
        private const string QQ_CUSTOMER_PARTICULAR = "POL_MARINECARGO_VOYAGE_INFORMATION";
        public ClsVoyageInformationInfo()
        {
            base.dtModel.TableName = "POL_MARINECARGO_VOYAGE_INFORMATION";		// setting table name for data table that holds property values.
            this.AddColumns();								// add columns of the database table POL_MARINECARGO_VOYAGE_INFORMATION
            base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
        }

        private void AddColumns()
        {
            base.dtModel.Columns.Add("CUSTOMER_ID", typeof(int));
            base.dtModel.Columns.Add("POLICY_ID", typeof(int));
            base.dtModel.Columns.Add("POLICY_VERSION_ID", typeof(int));
            base.dtModel.Columns.Add("RISK_ID", typeof(int));
            base.dtModel.Columns.Add("VOYAGE_INFO_ID", typeof(int));

            base.dtModel.Columns.Add("VOYAGE_TO", typeof(Int32));
            base.dtModel.Columns.Add("VOYAGE_FROM", typeof(Int32));
            base.dtModel.Columns.Add("THENCE_TO", typeof(Int32));          
            base.dtModel.Columns.Add("QUANTITY_DESCRIPTION", typeof(String));           

            //base.dtModel.Columns.Add("IS_ACTIVE", typeof(string));
            //base.dtModel.Columns.Add("CREATED_BY", typeof(int));
            //base.dtModel.Columns.Add("CREATED_DATETIME", typeof(DateTime));
            //base.dtModel.Columns.Add("MODIFIED_BY", typeof(int));
            //base.dtModel.Columns.Add("LAST_UPDATED_DATETIME", typeof(DateTime));
        }

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

        public Int32 RISK_ID
        {
            get
            {
                return base.dtModel.Rows[0]["RISK_ID"] == DBNull.Value ? 0 : Int32.Parse(base.dtModel.Rows[0]["RISK_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["RISK_ID"] = value;
            }
        }

        public Int32 VOYAGE_INFO_ID
        {
            get
            {
                return base.dtModel.Rows[0]["VOYAGE_INFO_ID"] == DBNull.Value ? 0 : Int32.Parse(base.dtModel.Rows[0]["VOYAGE_INFO_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["VOYAGE_INFO_ID"] = value;
            }
        }
        public Int32 VOYAGE_TO
        {
            get
            {
                return base.dtModel.Rows[0]["VOYAGE_TO"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["VOYAGE_TO"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["VOYAGE_TO"] = value;
            }
        }

        public Int32 VOYAGE_FROM
        {
            get
            {
                return base.dtModel.Rows[0]["VOYAGE_FROM"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["VOYAGE_FROM"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["VOYAGE_FROM"] = value;
            }
        }

        public Int32 THENCE_TO
        {
            get
            {
                return base.dtModel.Rows[0]["THENCE_TO"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["THENCE_TO"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["THENCE_TO"] = value;
            }
        }

        public string QUANTITY_DESCRIPTION
        {
            get
            {
                return base.dtModel.Rows[0]["QUANTITY_DESCRIPTION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["QUANTITY_DESCRIPTION"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["QUANTITY_DESCRIPTION"] = value;
            }
        }
    }
}
