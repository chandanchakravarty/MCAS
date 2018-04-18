/******************************************************************************************
<Author				: - Avijit GOswami
<Start Date			: -	21/03/2012
<End Date			: -	
<Description		: - Database Model for POL_MARINECARGO_AIRCRAFT
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
    public class ClsAircraftInfo : Cms.Model.ClsCommonModel
    {
        private const string MNT_CURRENCY_MASTER = "POL_MARINECARGO_AIRCRAFT";
        public ClsAircraftInfo()
        {
            base.dtModel.TableName = "POL_MARINECARGO_AIRCRAFT";	// setting table name for data table that holds property values.
            this.AddColumns();								            // add columns of the database table POL_MARINECARGO_AIRCRAFT
            base.dtModel.Rows.Add(base.dtModel.NewRow());	            // add a blank row in the datatable
        }
        private void AddColumns()
        {
            base.dtModel.Columns.Add("AIRCRAFT_ID", typeof(int));
            base.dtModel.Columns.Add("AIRCRAFT_NUMBER", typeof(string));
            base.dtModel.Columns.Add("AIRLINE", typeof(string));
            base.dtModel.Columns.Add("AIRCRAFT_FROM", typeof(string));
            base.dtModel.Columns.Add("AIRCRAFT_TO", typeof(string));
            base.dtModel.Columns.Add("AIRWAY_BILL", typeof(string));
        }
        #region Database schema details
        public int AIRCRAFT_ID
        {
            get
            {
                return base.dtModel.Rows[0]["AIRCRAFT_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["AIRCRAFT_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["AIRCRAFT_ID"] = value;
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

        public string AIRLINE
        {
            get
            {
                return base.dtModel.Rows[0]["AIRLINE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["AIRLINE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["AIRLINE"] = value;
            }
        }

        public string AIRCRAFT_FROM
        {
            get
            {
                return base.dtModel.Rows[0]["AIRCRAFT_FROM"] == DBNull.Value ? "" : base.dtModel.Rows[0]["AIRCRAFT_FROM"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["AIRCRAFT_FROM"] = value;
            }
        }

        public string AIRCRAFT_TO
        {
            get
            {
                return base.dtModel.Rows[0]["AIRCRAFT_TO"] == DBNull.Value ? "" : base.dtModel.Rows[0]["AIRCRAFT_TO"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["AIRCRAFT_TO"] = value;
            }
        }

        public string AIRWAY_BILL
        {
            get
            {
                return base.dtModel.Rows[0]["AIRWAY_BILL"] == DBNull.Value ? "" : base.dtModel.Rows[0]["AIRWAY_BILL"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["AIRWAY_BILL"] = value;
            }
        }


        public int CREATED_BY
        {
            get
            {
                return base.dtModel.Rows[0]["CREATED_BY"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CREATED_BY"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["CREATED_BY"] = value;
            }
        }

        public DateTime CREATED_DATETIME
        {
            get
            {
                return base.dtModel.Rows[0]["CREATED_DATETIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["CREATED_DATETIME"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["CREATED_DATETIME"] = value;
            }
        }

        public int MODIFIED_BY
        {
            get
            {
                return base.dtModel.Rows[0]["MODIFIED_BY"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["MODIFIED_BY"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["MODIFIED_BY"] = value;
            }
        }

        public DateTime LAST_UPDATED_DATETIME
        {
            get
            {
                return base.dtModel.Rows[0]["LAST_UPDATED_DATETIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["LAST_UPDATED_DATETIME"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["LAST_UPDATED_DATETIME"] = value;
            }
        }
        #endregion
    }
}

