/******************************************************************************************
<Author					: -		Vijay Arora
<Start Date				: -		12/22/2005 5:16:33 PM
<End Date				: -	
<Description			: - 	Model Class for Policy Processes Master
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using System.Data;
using Cms.Model;

namespace Cms.Model.Policy.Process
{
	/// <summary>
	/// Database Model for PRINT_JOBS.
	/// </summary>
	public class ClsPrintJobsInfo : Cms.Model.ClsCommonModel
	{
		private const string PRINT_JOBS = "PRINT_JOBS";
		public ClsPrintJobsInfo()
		{
			base.dtModel.TableName = "PRINT_JOBS";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table PRINT_JOBS
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("PRINT_JOBS_ID",typeof(int));
			base.dtModel.Columns.Add("DOCUMENT_CODE",typeof(string));
			base.dtModel.Columns.Add("URL_PATH",typeof(string));
			base.dtModel.Columns.Add("ONDEMAND_FLAG",typeof(string));
			base.dtModel.Columns.Add("PRINT_SUCCESSFUL",typeof(string));
			base.dtModel.Columns.Add("DUPLEX",typeof(string));			
			base.dtModel.Columns.Add("PRINT_DATETIME",typeof(DateTime));
			base.dtModel.Columns.Add("PRINTED_DATETIME",typeof(DateTime));			
			base.dtModel.Columns.Add("ENTITY_TYPE",typeof(string));			
			base.dtModel.Columns.Add("FILE_NAME",typeof(string));			
			base.dtModel.Columns.Add("AGENCY_ID",typeof(int));
			base.dtModel.Columns.Add("PROCESS_ID",typeof(int));
			base.dtModel.Columns.Add("PROCESS_ROW_ID",typeof(int));
            base.dtModel.Columns.Add("ENTITY_ID", typeof(int));
            // MODIFIED BY SANTOSH GAUTAM ON 23 FEB 2011 
            base.dtModel.Columns.Add("CLAIM_ID", typeof(int));
            base.dtModel.Columns.Add("ACTIVITY_ID", typeof(int));
            
		}
		#region Database schema details
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
		// model for database field PRINT_JOBS_ID(int)

        public int CLAIM_ID
        {
            get
            {
                return base.dtModel.Rows[0]["CLAIM_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CLAIM_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["CLAIM_ID"] = value;
            }
        }
        // model for database field CLAIM_ID(int)

        public int ACTIVITY_ID
        {
            get
            {
                return base.dtModel.Rows[0]["ACTIVITY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ACTIVITY_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["ACTIVITY_ID"] = value;
            }
        }
        // model for database field PRINT_JOBS_ID(int)

		public int PRINT_JOBS_ID
		{
			get
			{
				return base.dtModel.Rows[0]["PRINT_JOBS_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PRINT_JOBS_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PRINT_JOBS_ID"] = value;
			}
		}		
		// model for database field DOCUMENT_CODE(string)
		public string DOCUMENT_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["DOCUMENT_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DOCUMENT_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DOCUMENT_CODE"] = value;
			}
		}
		// model for database field URL_PATH(string)
		public string URL_PATH
		{
			get
			{
				return base.dtModel.Rows[0]["URL_PATH"] == DBNull.Value ? "" : base.dtModel.Rows[0]["URL_PATH"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["URL_PATH"] = value;
			}
		}		
		// model for database field ONDEMAND_FLAG(string)
		public string ONDEMAND_FLAG
		{
			get
			{
				return base.dtModel.Rows[0]["ONDEMAND_FLAG"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ONDEMAND_FLAG"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ONDEMAND_FLAG"] = value;
			}
		}		
		// model for database field PRINT_SUCCESSFUL(string)
		public string PRINT_SUCCESSFUL
		{
			get
			{
				return base.dtModel.Rows[0]["PRINT_SUCCESSFUL"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PRINT_SUCCESSFUL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PRINT_SUCCESSFUL"] = value;
			}
		}		
		// model for database field DUPLEX(string)
		public string DUPLEX
		{
			get
			{
				return base.dtModel.Rows[0]["DUPLEX"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DUPLEX"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DUPLEX"] = value;
			}
		}				
		// model for database field PRINT_DATETIME(DateTime)
		public DateTime PRINT_DATETIME
		{
			get
			{
				return base.dtModel.Rows[0]["PRINT_DATETIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["PRINT_DATETIME"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PRINT_DATETIME"] = value;
			}
		}
		// model for database field PRINTED_DATETIME(DateTime)
		public DateTime PRINTED_DATETIME
		{
			get
			{
				return base.dtModel.Rows[0]["PRINTED_DATETIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["PRINTED_DATETIME"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PRINTED_DATETIME"] = value;
			}
		}
		// model for database field ENTITY_TYPE(string)
		public string ENTITY_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["ENTITY_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ENTITY_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ENTITY_TYPE"] = value;
			}
		}
        // model for database field ENTITY_ID(int)
        public int ENTITY_ID
        {
            get
            {
                return base.dtModel.Rows[0]["ENTITY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ENTITY_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["ENTITY_ID"] = value;
            }
        }
		// model for database field AGENCY_ID(int)
		public int AGENCY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["AGENCY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["AGENCY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AGENCY_ID"] = value;
			}
		}
		// model for database field FILE_NAME(string)
		public string FILE_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["FILE_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["FILE_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["FILE_NAME"] = value;
			}
		}
		// model for database field PROCESS_ID(int)
		public int PROCESS_ID
		{
			get
			{
				return base.dtModel.Rows[0]["PROCESS_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PROCESS_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PROCESS_ID"] = value;
			}
		}
		// model for database field PROCESS_ID(int)
		public int PROCESS_ROW_ID
		{
			get
			{
				return base.dtModel.Rows[0]["PROCESS_ROW_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PROCESS_ROW_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PROCESS_ROW_ID"] = value;
			}
		}
		
		#endregion
	}
}
