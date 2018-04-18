/******************************************************************************************
<Author				: - Kuldeep Saxena
<Start Date			: -	14/03/2012
<End Date			: -	
<Description		: - Database Model for MNT_PORT_MASTER
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

namespace Cms.Model.Maintenance
{
    public class ClsPortMasterInfo : Cms.Model.ClsCommonModel
    {/// <summary>
        /// Database Model for MNT_ACCUMULATION_CRITERIA_MASTER.
        /// </summary>

        private const string MNT_ACCUMULATION_CRITERIA_MASTER = "MNT_PORT_MASTER";
        public ClsPortMasterInfo()
        {
            base.dtModel.TableName = "MNT_PORT_MASTER";	// setting table name for data table that holds property values.
            this.AddColumns();								// add columns of the database table MNT_PORT_MASTER
            base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
        }
//PORT_CODE int NOT NULL,    
// ISO_CODE NVARCHAR(10) ,    
// PORT_TYPE NVARCHAR(10),    
// COUNTRY NVARCHAR(40),    
// ADDITIONAL_WAR_RATE DECIMAL(18,2),    
// FROM_DATE DATETIME,    
// TO_DATE DATETIME,    
// SETTLEMENT_AGENT_CODE Nvarchar(10),
// SETTLING_AGENT_NAME NVARCHAR(25),
        private void AddColumns()
        {
            base.dtModel.Columns.Add("PORT_CODE", typeof(int));
            base.dtModel.Columns.Add("ISO_CODE", typeof(string));
            base.dtModel.Columns.Add("PORT_TYPE", typeof(string));
            base.dtModel.Columns.Add("COUNTRY", typeof(string));
             base.dtModel.Columns.Add("ADDITIONAL_WAR_RATE", typeof(double));
             base.dtModel.Columns.Add("FROM_DATE", typeof(DateTime));
             base.dtModel.Columns.Add("TO_DATE", typeof(DateTime));
             base.dtModel.Columns.Add("SETTLEMENT_AGENT_CODE", typeof(string));
             base.dtModel.Columns.Add("SETTLING_AGENT_NAME", typeof(string));

        }
        #region Database schema details

        public int PORT_CODE
        {
            get
            {
                return base.dtModel.Rows[0]["PORT_CODE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PORT_CODE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["PORT_CODE"] = value;
            }
        }

        public string ISO_CODE
        {
            get
            {
                return base.dtModel.Rows[0]["ISO_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ISO_CODE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["ISO_CODE"] = value;
            }
        }

        public string PORT_TYPE
        {
            get
            {
                return base.dtModel.Rows[0]["PORT_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PORT_TYPE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["PORT_TYPE"] = value;
            }
        }

        public string COUNTRY
        {
            get
            {
                return base.dtModel.Rows[0]["COUNTRY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["COUNTRY"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["COUNTRY"] = value;
            }
        }
        
        public double ADDITIONAL_WAR_RATE
        {
            get
            {
                return base.dtModel.Rows[0]["ADDITIONAL_WAR_RATE"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["ADDITIONAL_WAR_RATE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["ADDITIONAL_WAR_RATE"] = value;
            }
        }

        public DateTime FROM_DATE
        {
            get
            {
                return base.dtModel.Rows[0]["FROM_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["FROM_DATE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["FROM_DATE"] = value;
            }
        }

        public DateTime TO_DATE
        {
            get
            {
                return base.dtModel.Rows[0]["TO_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["TO_DATE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["TO_DATE"] = value;
            }
        }

        public string SETTLEMENT_AGENT_CODE
        {
            get
            {
                return base.dtModel.Rows[0]["SETTLEMENT_AGENT_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SETTLEMENT_AGENT_CODE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["SETTLEMENT_AGENT_CODE"] = value;
            }
        }

        public string SETTLING_AGENT_NAME
        {
            get
            {
                return base.dtModel.Rows[0]["SETTLING_AGENT_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SETTLING_AGENT_NAME"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["SETTLING_AGENT_NAME"] = value;
            }
        }

        #endregion
    }
}
