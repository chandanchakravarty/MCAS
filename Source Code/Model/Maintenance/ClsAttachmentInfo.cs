/******************************************************************************************
<Author					: - Sumit Chhabra 
<Start Date				: -	24-01-2006
<End Date				: -	
<Description			: - 	
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

namespace Cms.Model.Maintenance
{
	/// <summary>
	/// Summary description for ClsCoverageDetailsInfo.
	/// </summary>
	public class ClsAttachmentInfo : Cms.Model.ClsCommonModel
	{
		private const string MNT_ATTACHMENT_LIST = "MNT_ATTACHMENT_LIST";
		public ClsAttachmentInfo()
		{
			base.dtModel.TableName = "MNT_ATTACHMENT_LIST";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table MNT_COVERAGE
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}		

		private void AddColumns()
		{
			base.dtModel.Columns.Add("ATTACH_ID",typeof(int));
			base.dtModel.Columns.Add("ATTACH_LOC",typeof(string));
			base.dtModel.Columns.Add("ATTACH_ENT_ID",typeof(int));
			base.dtModel.Columns.Add("ATTACH_FILE_NAME",typeof(string));
			base.dtModel.Columns.Add("ATTACH_DATE_TIME",typeof(DateTime));
			base.dtModel.Columns.Add("ATTACH_USER_ID",typeof(int));
			base.dtModel.Columns.Add("ATTACH_FILE_DESC",typeof(string));
			base.dtModel.Columns.Add("ATTACH_POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("ATTACH_POLICY_VER_TRACKING_ID",typeof(int));
			base.dtModel.Columns.Add("ATTACH_GEN_FILE_NAME",typeof(string));
			base.dtModel.Columns.Add("ATTACH_FILE_TYPE",typeof(string));
			base.dtModel.Columns.Add("ATTACH_ENTITY_TYPE",typeof(string));
			base.dtModel.Columns.Add("ATTACH_CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("ATTACH_APP_ID",typeof(int));
			base.dtModel.Columns.Add("ATTACH_APP_VER_ID",typeof(int));
			base.dtModel.Columns.Add("SOURCE_ATTACH_ID",typeof(int));			
			base.dtModel.Columns.Add("ATTACH_TYPE",typeof(int));			
			
		}
		#region Database schema details
		// model for database field ATTACH_ID(int)
		public int ATTACH_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ATTACH_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ATTACH_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ATTACH_ID"] = value;
			}
		}
		// model for database field ATTACH_ENT_ID(int)
		public int ATTACH_ENT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ATTACH_ENT_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ATTACH_ENT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ATTACH_ENT_ID"] = value;
			}
		}
		// model for database field ATTACH_USER_ID(int)
		public int ATTACH_USER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ATTACH_USER_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ATTACH_USER_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ATTACH_USER_ID"] = value;
			}
		}
		// model for database field ATTACH_POLICY_ID(int)
		public int ATTACH_POLICY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ATTACH_POLICY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ATTACH_POLICY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ATTACH_POLICY_ID"] = value;
			}
		}
		// model for database field ATTACH_POLICY_VER_TRACKING_ID(int)
		public int ATTACH_POLICY_VER_TRACKING_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ATTACH_POLICY_VER_TRACKING_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ATTACH_POLICY_VER_TRACKING_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ATTACH_POLICY_VER_TRACKING_ID"] = value;
			}
		}
		// model for database field ATTACH_CUSTOMER_ID(int)
		public int ATTACH_CUSTOMER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ATTACH_CUSTOMER_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ATTACH_CUSTOMER_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ATTACH_CUSTOMER_ID"] = value;
			}
		}
		// model for database field ATTACH_APP_ID(int)
		public int ATTACH_APP_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ATTACH_APP_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ATTACH_APP_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ATTACH_APP_ID"] = value;
			}
		}
		// model for database field ATTACH_APP_VER_ID(int)
		public int ATTACH_APP_VER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ATTACH_APP_VER_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ATTACH_APP_VER_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ATTACH_APP_VER_ID"] = value;
			}
		}
		// model for database field SOURCE_ATTACH_ID(int)
		public int SOURCE_ATTACH_ID
		{
			get
			{
				return base.dtModel.Rows[0]["SOURCE_ATTACH_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["SOURCE_ATTACH_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SOURCE_ATTACH_ID"] = value;
			}
		}
	
		// model for database field ATTACH_TYPE(int)
		public int ATTACH_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["ATTACH_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ATTACH_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ATTACH_TYPE"] = value;
			}
		}
	// model for database field ATTACH_FILE_NAME(string)
		public string ATTACH_FILE_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["ATTACH_FILE_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ATTACH_FILE_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ATTACH_FILE_NAME"] = value;
			}
		}
		// model for database field ATTACH_FILE_DESC(string)
		public string ATTACH_FILE_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["ATTACH_FILE_DESC"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ATTACH_FILE_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ATTACH_FILE_DESC"] = value;
			}
		}
		// model for database field ATTACH_GEN_FILE_NAME(string)
		public string ATTACH_GEN_FILE_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["ATTACH_GEN_FILE_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ATTACH_GEN_FILE_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ATTACH_GEN_FILE_NAME"] = value;
			}
		}
		// model for database field ATTACH_FILE_TYPE(string)
		public string ATTACH_FILE_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["ATTACH_FILE_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ATTACH_FILE_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ATTACH_FILE_TYPE"] = value;
			}
		}
		// model for database field ATTACH_ENTITY_TYPE(string)
		public string ATTACH_ENTITY_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["ATTACH_ENTITY_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ATTACH_ENTITY_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ATTACH_ENTITY_TYPE"] = value;
			}
		}		
		
		// model for database field ATTACH_LOC(string)
		public string ATTACH_LOC
		{
			get
			{
				return base.dtModel.Rows[0]["ATTACH_LOC"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ATTACH_LOC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ATTACH_LOC"] = value;
			}
		}

		// model for database field ATTACH_DATE_TIME(DateTime)
		public DateTime ATTACH_DATE_TIME
		{
			get
			{
				return base.dtModel.Rows[0]["ATTACH_DATE_TIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["ATTACH_DATE_TIME"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ATTACH_DATE_TIME"] = value;
			}
		}

		#endregion	

	}
}
