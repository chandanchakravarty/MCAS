/******************************************************************************************
<Author					: - Vijay Joshi
<Start Date				: -	6/9/2005 11:54:15 AM
<End Date				: -	
<Description			: - Model class for Journal Entry Master table
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
namespace Cms.Model.Account
{
	/// <summary>
	/// Database Model for ACT_JOURNAL_MASTER.
	/// </summary>
	public class ClsJournalEntryMasterInfo : Cms.Model.ClsCommonModel
	{
		private const string ACT_JOURNAL_MASTER = "ACT_JOURNAL_MASTER";
		public ClsJournalEntryMasterInfo()
		{
			base.dtModel.TableName = "ACT_JOURNAL_MASTER";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table ACT_JOURNAL_MASTER
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("JOURNAL_ID",typeof(int));
			base.dtModel.Columns.Add("JOURNAL_GROUP_TYPE",typeof(string));
			base.dtModel.Columns.Add("TRANS_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("JOURNAL_GROUP_CODE",typeof(string));
			base.dtModel.Columns.Add("JOURNAL_ENTRY_NO",typeof(string));
			base.dtModel.Columns.Add("DESCRIPTION",typeof(string));
			base.dtModel.Columns.Add("DIV_ID",typeof(int));
			base.dtModel.Columns.Add("DEPT_ID",typeof(int));
			base.dtModel.Columns.Add("PC_ID",typeof(int));
			base.dtModel.Columns.Add("GL_ID",typeof(int));
			base.dtModel.Columns.Add("FISCAL_ID",typeof(int));
			base.dtModel.Columns.Add("FREQUENCY",typeof(string));
			base.dtModel.Columns.Add("START_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("END_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("DAY_OF_THE_WK",typeof(string));
			base.dtModel.Columns.Add("LAST_PROCESSED_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("IS_COMMITED",typeof(string));
			base.dtModel.Columns.Add("DATE_COMMITED",typeof(DateTime));
			base.dtModel.Columns.Add("IMPORTSTATUS",typeof(string));
			base.dtModel.Columns.Add("LAST_VALID_POSTING_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("NO_OF_RUN",typeof(int));
			base.dtModel.Columns.Add("TEMP_JE_NUM",typeof(int));
		}
		#region Database schema details
		// model for database field JOURNAL_ID(int)
		public int JOURNAL_ID
		{
			get
			{
				return base.dtModel.Rows[0]["JOURNAL_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["JOURNAL_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["JOURNAL_ID"] = value;
			}
		}
		// model for database field JOURNAL_GROUP_TYPE(string)
		public string JOURNAL_GROUP_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["JOURNAL_GROUP_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["JOURNAL_GROUP_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["JOURNAL_GROUP_TYPE"] = value;
			}
		}
		// model for database field TRANS_DATE(DateTime)
		public DateTime TRANS_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["TRANS_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["TRANS_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["TRANS_DATE"] = value;
			}
		}
		// model for database field JOURNAL_GROUP_CODE(string)
		public string JOURNAL_GROUP_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["JOURNAL_GROUP_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["JOURNAL_GROUP_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["JOURNAL_GROUP_CODE"] = value;
			}
		}
		// model for database field JOURNAL_ENTRY_NO(string)
		public string JOURNAL_ENTRY_NO
		{
			get
			{
				return base.dtModel.Rows[0]["JOURNAL_ENTRY_NO"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["JOURNAL_ENTRY_NO"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["JOURNAL_ENTRY_NO"] = value;
			}
		}
		// model for database field DESCRIPTION(string)
		public string DESCRIPTION
		{
			get
			{
				return base.dtModel.Rows[0]["DESCRIPTION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESCRIPTION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESCRIPTION"] = value;
			}
		}
		// model for database field DIV_ID(int)
		public int DIV_ID
		{
			get
			{
				return base.dtModel.Rows[0]["DIV_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DIV_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DIV_ID"] = value;
			}
		}
		// model for database field DEPT_ID(int)
		public int DEPT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["DEPT_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DEPT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DEPT_ID"] = value;
			}
		}
		// model for database field PC_ID(int)
		public int PC_ID
		{
			get
			{
				return base.dtModel.Rows[0]["PC_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["PC_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PC_ID"] = value;
			}
		}
		// model for database field GL_ID(int)
		public int GL_ID
		{
			get
			{
				return base.dtModel.Rows[0]["GL_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["GL_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["GL_ID"] = value;
			}
		}
		// model for database field FISCAL_ID(int)
		public int FISCAL_ID
		{
			get
			{
				return base.dtModel.Rows[0]["FISCAL_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["FISCAL_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["FISCAL_ID"] = value;
			}
		}
		// model for database field FREQUENCY(string)
		public string FREQUENCY
		{
			get
			{
				return base.dtModel.Rows[0]["FREQUENCY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["FREQUENCY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["FREQUENCY"] = value;
			}
		}
		// model for database field START_DATE(DateTime)
		public DateTime START_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["START_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["START_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["START_DATE"] = value;
			}
		}
		// model for database field END_DATE(DateTime)
		public DateTime END_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["END_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["END_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["END_DATE"] = value;
			}
		}
		// model for database field DAY_OF_THE_WK(string)
		public string DAY_OF_THE_WK
		{
			get
			{
				return base.dtModel.Rows[0]["DAY_OF_THE_WK"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DAY_OF_THE_WK"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DAY_OF_THE_WK"] = value;
			}
		}
		// model for database field LAST_PROCESSED_DATE(DateTime)
		public DateTime LAST_PROCESSED_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["LAST_PROCESSED_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["LAST_PROCESSED_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LAST_PROCESSED_DATE"] = value;
			}
		}
		// model for database field IS_COMMITED(string)
		public string IS_COMMITED
		{
			get
			{
				return base.dtModel.Rows[0]["IS_COMMITED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_COMMITED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_COMMITED"] = value;
			}
		}
		// model for database field DATE_COMMITED(DateTime)
		public DateTime DATE_COMMITED
		{
			get
			{
				return base.dtModel.Rows[0]["DATE_COMMITED"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["DATE_COMMITED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DATE_COMMITED"] = value;
			}
		}
		// model for database field IMPORTSTATUS(string)
		public string IMPORTSTATUS
		{
			get
			{
				return base.dtModel.Rows[0]["IMPORTSTATUS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IMPORTSTATUS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IMPORTSTATUS"] = value;
			}
		}
		// model for database field LAST_VALID_POSTING_DATE(DateTime)
		public DateTime LAST_VALID_POSTING_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["LAST_VALID_POSTING_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["LAST_VALID_POSTING_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LAST_VALID_POSTING_DATE"] = value;
			}
		}
		// model for database field NO_OF_RUN(int)
		public int NO_OF_RUN
		{
			get
			{
				return base.dtModel.Rows[0]["NO_OF_RUN"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["NO_OF_RUN"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NO_OF_RUN"] = value;
			}
		}
		// model for database field TEMP_JE_NUM(int)
		public int TEMP_JE_NUM
		{
			get
			{
				return base.dtModel.Rows[0]["TEMP_JE_NUM"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["TEMP_JE_NUM"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["TEMP_JE_NUM"] = value;
			}
		}
		
		#endregion
	}
}
