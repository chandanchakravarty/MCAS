/******************************************************************************************
<Author				: -		Agniswar Das
<Start Date			: -		08-11-2011
<End Date			: -	
<Description		: - 	Model Class for nature of business
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: - 
<Modified By		: - 
<Purpose			: -		Insurors
*******************************************************************************************/
using System;
using System.Data;
using Cms.Model;
namespace Cms.Model.Policy
{
    /// <summary>
    /// Database Model for POL_NATURE_OF_BUSINESS.
    /// </summary>
    public class ClsNatureBusinessInfo : Cms.Model.ClsCommonModel
    {
        private const string POL_NATURE_OF_BUSINESS = "POL_NATURE_OF_BUSINESS";
        public ClsNatureBusinessInfo()
        {
            base.dtModel.TableName = "POL_NATURE_OF_BUSINESS";		// setting table name for data table that holds property values.
            this.AddColumns();								// add columns of the database table POL_VEHICLES
            base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
        }
        private void AddColumns()
        {
            base.dtModel.Columns.Add("POLICY_ID", typeof(int));
            base.dtModel.Columns.Add("POLICY_VERSION_ID", typeof(int));
            base.dtModel.Columns.Add("CUSTOMER_ID", typeof(int));
            base.dtModel.Columns.Add("BUSINESS_ID", typeof(int));
            base.dtModel.Columns.Add("BUSINESS_NATURE", typeof(int));
            base.dtModel.Columns.Add("PRIMARY_OPERATION", typeof(string));
            //base.dtModel.Columns.Add("BUSINESS_START_DATE", typeof(DateTime));
            base.dtModel.Columns.Add("BUSINESS_START_DATE", typeof(int));
            base.dtModel.Columns.Add("OTHER_OPERATION", typeof(string));
            base.dtModel.Columns.Add("REPAIR_WORK", typeof(int));
            base.dtModel.Columns.Add("PREMISES_WORK", typeof(int));
            base.dtModel.Columns.Add("RETAIL_STORE", typeof(int)); //RETAIL_STORE



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
        // model for database field BUSINESS_ID(int)
        public int BUSINESS_ID
        {
            get
            {
                return base.dtModel.Rows[0]["BUSINESS_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["BUSINESS_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["BUSINESS_ID"] = value;
            }
        }
        // model for database field BUSINESS_NATURE(int)
        public int BUSINESS_NATURE
        {
            get
            {
                return base.dtModel.Rows[0]["BUSINESS_NATURE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["BUSINESS_NATURE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["BUSINESS_NATURE"] = value;
            }
        }
        // model for database field PRIMARY_OPERATION(string)
        public string PRIMARY_OPERATION
        {
            get
            {
                return base.dtModel.Rows[0]["PRIMARY_OPERATION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PRIMARY_OPERATION"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["PRIMARY_OPERATION"] = value;
            }
        }

        // model for database field BUSINESS_START_DATE(DateTime):This was changed as 
        //public DateTime BUSINESS_START_DATE
        //{
        //    get
        //    {
        //        return base.dtModel.Rows[0]["BUSINESS_START_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["BUSINESS_START_DATE"].ToString());
        //    }
        //    set
        //    {
        //        base.dtModel.Rows[0]["BUSINESS_START_DATE"] = value;
        //    }
        //}
        public int BUSINESS_START_DATE
        {
            get
            {
                return base.dtModel.Rows[0]["BUSINESS_START_DATE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["BUSINESS_START_DATE"].ToString());
                
            }
            set
            {
                base.dtModel.Rows[0]["BUSINESS_START_DATE"] = value;
            }
        }
        // model for database field OTHER_OPERATION(string)
        public string OTHER_OPERATION
        {
            get
            {
                return base.dtModel.Rows[0]["OTHER_OPERATION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["OTHER_OPERATION"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["OTHER_OPERATION"] = value;
            }
        }

        // model for database field REPAIR_WORK(int)
        public int REPAIR_WORK
        {
            get
            {
                return base.dtModel.Rows[0]["REPAIR_WORK"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["REPAIR_WORK"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["REPAIR_WORK"] = value;
            }
        }
        // model for database field PREMISES_WORK(int)
        public int PREMISES_WORK
        {
            get
            {
                return base.dtModel.Rows[0]["PREMISES_WORK"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PREMISES_WORK"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["PREMISES_WORK"] = value;
            }
        }

        // model for database field RETAIL_STORE(int)
        public int RETAIL_STORE
        {
            get
            {
                return base.dtModel.Rows[0]["RETAIL_STORE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["RETAIL_STORE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["RETAIL_STORE"] = value;
            }
        }

        #endregion
    }
}
