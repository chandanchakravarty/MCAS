using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Cms.Model;

namespace Cms.Model.Quote
{
    public class ClsQQMarineRiskDetailsInfo:ClsCommonModel
    {
        private const string QQ_CUSTOMER_PARTICULAR = "QQ_MARINECARGO_RISK_DETAILS";
        public ClsQQMarineRiskDetailsInfo()
        {
            base.dtModel.TableName = "QQ_MARINECARGO_RISK_DETAILS";		// setting table name for data table that holds property values.
            this.AddColumns();								// add columns of the database table QQ_MARINECARGO_RISK_DETAILS
            base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
        }

        private void AddColumns()
        {
            base.dtModel.Columns.Add("CUSTOMER_ID", typeof(int));
            base.dtModel.Columns.Add("POLICY_ID", typeof(int));
            base.dtModel.Columns.Add("POLICY_VERSION_ID", typeof(int));
            base.dtModel.Columns.Add("QUOTE_ID", typeof(int));

            base.dtModel.Columns.Add("VOYAGE_TO", typeof(Int32));
            base.dtModel.Columns.Add("VOYAGE_FROM", typeof(Int32));
            base.dtModel.Columns.Add("THENCE_TO", typeof(Int32));
            base.dtModel.Columns.Add("VESSEL", typeof(Int32));
            base.dtModel.Columns.Add("AIRCRAFT_NUMBER", typeof(String));
            base.dtModel.Columns.Add("LAND_TRANSPORT", typeof(String));
            base.dtModel.Columns.Add("VOYAGE_FROM_DATE", typeof(DateTime));
            base.dtModel.Columns.Add("QUANTITY_DESCRIPTION", typeof(String));
            base.dtModel.Columns.Add("INSURANCE_CONDITIONS1", typeof(Double));
            base.dtModel.Columns.Add("INSURANCE_CONDITIONS2", typeof(Double));
            base.dtModel.Columns.Add("INSURANCE_CONDITIONS3", typeof(Double));
            base.dtModel.Columns.Add("INSURANCE_CONDITIONS1_SELECTION", typeof(String));
            base.dtModel.Columns.Add("MARINE_RATE", typeof(Double));

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

        public int QUOTE_ID
        {
            get
            {
                return base.dtModel.Rows[0]["QUOTE_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["QUOTE_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["QUOTE_ID"] = value;
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

        public Int32 VESSEL
        {
            get
            {
                return base.dtModel.Rows[0]["VESSEL"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["VESSEL"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["VESSEL"] = value;
            }
        }

        public string AIRCRAFT_NUMBER
        {
            get
            {
                return base.dtModel.Rows[0]["AIRCRAFT_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["AIRCRAFT_NUMBER"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["AIRCRAFT_NUMBER"] = value;
            }
        }


        public string LAND_TRANSPORT
        {
            get
            {
                return base.dtModel.Rows[0]["LAND_TRANSPORT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LAND_TRANSPORT"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["LAND_TRANSPORT"] = value;
            }
        }


        public DateTime VOYAGE_FROM_DATE
        {
            get
            {
                return base.dtModel.Rows[0]["VOYAGE_FROM_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["VOYAGE_FROM_DATE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["VOYAGE_FROM_DATE"] = value;
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


        public Double INSURANCE_CONDITIONS1
        {
            get
            {
                return base.dtModel.Rows[0]["INSURANCE_CONDITIONS1"] == DBNull.Value ? 0.00 : Double.Parse(base.dtModel.Rows[0]["INSURANCE_CONDITIONS1"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["INSURANCE_CONDITIONS1"] = value;
            }
        }

        public Double INSURANCE_CONDITIONS2
        {
            get
            {
                return base.dtModel.Rows[0]["INSURANCE_CONDITIONS2"] == DBNull.Value ? 0.00 : Double.Parse(base.dtModel.Rows[0]["INSURANCE_CONDITIONS2"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["INSURANCE_CONDITIONS2"] = value;
            }
        }


        public Double INSURANCE_CONDITIONS3
        {
            get
            {
                return base.dtModel.Rows[0]["INSURANCE_CONDITIONS3"] == DBNull.Value ? 0.00 : Double.Parse(base.dtModel.Rows[0]["INSURANCE_CONDITIONS3"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["INSURANCE_CONDITIONS3"] = value;
            }
        }


        public string INSURANCE_CONDITIONS1_SELECTION
        {
            get
            {
                return base.dtModel.Rows[0]["INSURANCE_CONDITIONS1_SELECTION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["INSURANCE_CONDITIONS1_SELECTION"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["INSURANCE_CONDITIONS1_SELECTION"] = value;
            }
        }


        public Double MARINE_RATE
        {
            get
            {
                return base.dtModel.Rows[0]["MARINE_RATE"] == DBNull.Value ? 0.00 : Double.Parse(base.dtModel.Rows[0]["MARINE_RATE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["MARINE_RATE"] = value;
            }
        }



    }
}
