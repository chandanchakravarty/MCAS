/******************************************************************************************
<Author				: -   
<Start Date				: -	8/3/2005 2:18:51 PM
<End Date				: -	
<Description				: - 	
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using System.Data;
using Cms.Model;

namespace Cms.Model.Maintenance
{
	/// <summary>
	/// Summary description for ClsLookupInfo.
	/// </summary>
	public class ClsLookupInfo :Cms.Model.ClsCommonModel
	{
		private const string MNT_LOOKUP_VALUES = "MNT_LOOKUP_VALUES";
		public ClsLookupInfo()
		{
			base.dtModel.TableName = "MNT_LOOKUP_VALUES";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table MNT_LOOKUP_VALUES
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("LOOKUP_UNIQUE_ID",typeof(int));
			base.dtModel.Columns.Add("LOOKUP_ID",typeof(int));
			base.dtModel.Columns.Add("LOOKUP_VALUE_ID",typeof(int));
			base.dtModel.Columns.Add("LOOKUP_VALUE_CODE",typeof(string));
			base.dtModel.Columns.Add("LOOKUP_VALUE_DESC",typeof(string));
			base.dtModel.Columns.Add("LOOKUP_SYS_DEF",typeof(string));
			//base.dtModel.Columns.Add("IS_ACTIVE",typeof(string));
			//base.dtModel.Columns.Add("LAST_UPDATED_DATETIME",typeof(DateTime));
			base.dtModel.Columns.Add("LOOKUP_FRAME_OR_MASONRY",typeof(string));
			base.dtModel.Columns.Add("Type",typeof(string));
		}
		#region Database schema details
		// model for database field LOOKUP_UNIQUE_ID(int)
		public int LOOKUP_UNIQUE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["LOOKUP_UNIQUE_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LOOKUP_UNIQUE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LOOKUP_UNIQUE_ID"] = value;
			}
		}
		// model for database field LOOKUP_ID(int)
		public int LOOKUP_ID
		{
			get
			{
				return base.dtModel.Rows[0]["LOOKUP_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LOOKUP_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LOOKUP_ID"] = value;
			}
		}
		// model for database field LOOKUP_VALUE_ID(int)
		public int LOOKUP_VALUE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["LOOKUP_VALUE_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LOOKUP_VALUE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LOOKUP_VALUE_ID"] = value;
			}
		}
		// model for database field LOOKUP_VALUE_CODE(string)
		public string LOOKUP_VALUE_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["LOOKUP_VALUE_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LOOKUP_VALUE_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOOKUP_VALUE_CODE"] = value;
			}
		}
		// model for database field LOOKUP_VALUE_DESC(string)
		public string LOOKUP_VALUE_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["LOOKUP_VALUE_DESC"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LOOKUP_VALUE_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOOKUP_VALUE_DESC"] = value;
			}
		}
		// model for database field LOOKUP_SYS_DEF(string)
		public string LOOKUP_SYS_DEF
		{
			get
			{
				return base.dtModel.Rows[0]["LOOKUP_SYS_DEF"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LOOKUP_SYS_DEF"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOOKUP_SYS_DEF"] = value;
			}
		}
//		// model for database field IS_ACTIVE(string)
//		public string IS_ACTIVE
//		{
//			get
//			{
//				return base.dtModel.Rows[0]["IS_ACTIVE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IS_ACTIVE"].ToString();
//			}
//			set
//			{
//				base.dtModel.Rows[0]["IS_ACTIVE"] = value;
//			}
//		}
//		// model for database field LAST_UPDATED_DATETIME(DateTime)
//		public DateTime LAST_UPDATED_DATETIME
//		{
//			get
//			{
//				return base.dtModel.Rows[0]["LAST_UPDATED_DATETIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["LAST_UPDATED_DATETIME"].ToString());
//			}
//			set
//			{
//				base.dtModel.Rows[0]["LAST_UPDATED_DATETIME"] = value;
//			}
//		}
//		// model for database field LOOKUP_FRAME_OR_MASONRY(string)
		public string LOOKUP_FRAME_OR_MASONRY
		{
			get
			{
				return base.dtModel.Rows[0]["LOOKUP_FRAME_OR_MASONRY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LOOKUP_FRAME_OR_MASONRY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOOKUP_FRAME_OR_MASONRY"] = value;
			}
		}
		// model for database field Type(string)
		public string Type
		{
			get
			{
				return base.dtModel.Rows[0]["Type"] == DBNull.Value ? "" : base.dtModel.Rows[0]["Type"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["Type"] = value;
			}
		}
		#endregion
	}
}

