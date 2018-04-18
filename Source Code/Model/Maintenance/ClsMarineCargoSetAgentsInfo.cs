/******************************************************************************************
<Author				: - Avijit GOswami
<Start Date			: -	16/03/2012
<End Date			: -	
<Description		: - Database Model for MARINE_CARGO_SETTLING_AGENTS
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
    public class ClsMarineCargoSetAgentsInfo : Cms.Model.ClsCommonModel
    {
        /// <summary>
        /// Database Model for MARINE_CARGO_SETTLING_AGENTS.
        /// </summary>

        private const string MNT_CURRENCY_MASTER = "MARINE_CARGO_SETTLING_AGENTS";
        public ClsMarineCargoSetAgentsInfo()
        {
            base.dtModel.TableName = "MARINE_CARGO_SETTLING_AGENTS";	// setting table name for data table that holds property values.
            this.AddColumns();								            // add columns of the database table MNT_CURRENCY_MASTER
            base.dtModel.Rows.Add(base.dtModel.NewRow());	            // add a blank row in the datatable
        }
        private void AddColumns()
        {
            base.dtModel.Columns.Add("AGENT_ID", typeof(int));
            base.dtModel.Columns.Add("AGENT_CODE", typeof(string));
            base.dtModel.Columns.Add("AGENT_NAME", typeof(string));            
            base.dtModel.Columns.Add("AGENT_ADDRESS1", typeof(string));
            base.dtModel.Columns.Add("AGENT_ADDRESS2", typeof(string));
            base.dtModel.Columns.Add("AGENT_CITY", typeof(string));
            base.dtModel.Columns.Add("AGENT_COUNTRY", typeof(string));
            base.dtModel.Columns.Add("AGENT_SURVEY_CODE", typeof(string));
        }
        #region Database schema details        
        public int AGENT_ID
        {
            get
            {
                return base.dtModel.Rows[0]["AGENT_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["AGENT_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["AGENT_ID"] = value;
            }
        }

        public string AGENT_CODE
        {
            get
            {
                return base.dtModel.Rows[0]["AGENT_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["AGENT_CODE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["AGENT_CODE"] = value;
            }
        }

        public string AGENT_NAME
        {
            get
            {
                return base.dtModel.Rows[0]["AGENT_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["AGENT_NAME"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["AGENT_NAME"] = value;
            }
        }

        public string AGENT_ADDRESS1
        {
            get
            {
                return base.dtModel.Rows[0]["AGENT_ADDRESS1"] == DBNull.Value ? "" : base.dtModel.Rows[0]["AGENT_ADDRESS1"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["AGENT_ADDRESS1"] = value;
            }
        }

        public string AGENT_ADDRESS2
        {
            get
            {
                return base.dtModel.Rows[0]["AGENT_ADDRESS2"] == DBNull.Value ? "" : base.dtModel.Rows[0]["AGENT_ADDRESS2"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["AGENT_ADDRESS2"] = value;
            }
        }

        public string AGENT_CITY
        {
            get
            {
                return base.dtModel.Rows[0]["AGENT_CITY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["AGENT_CITY"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["AGENT_CITY"] = value;
            }
        }

        public string AGENT_COUNTRY
        {
            get
            {
                return base.dtModel.Rows[0]["AGENT_COUNTRY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["AGENT_COUNTRY"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["AGENT_COUNTRY"] = value;
            }
        }

        public string AGENT_SURVEY_CODE
        {
            get
            {
                return base.dtModel.Rows[0]["AGENT_SURVEY_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["AGENT_SURVEY_CODE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["AGENT_SURVEY_CODE"] = value;
            }
        }
        
        public string IS_ACTIVE
        {
            get
            {
                return base.dtModel.Rows[0]["IS_ACTIVE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IS_ACTIVE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["IS_ACTIVE"] = value;
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
                base.dtModel.Rows[0]["CREATED_BY"] = value;
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

