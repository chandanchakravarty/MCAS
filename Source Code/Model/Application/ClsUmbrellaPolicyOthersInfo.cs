/******************************************************************************************
<Author				: -   Mohit Gupta
<Start Date				: -	5/24/2005 3:26:44 PM
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

namespace Cms.Model.Application 
{
	/// <summary>
	/// Summary description for ClsUmbrellaPolicyOthersInfo.
	/// </summary>
	public class ClsUmbrellaPolicyOthersInfo: Cms.Model.ClsCommonModel
	{
		private const string APP_UMBRELLA_POL_INFO_OTHER= "APP_UMBRELLA_POL_INFO_OTHER";
		public ClsUmbrellaPolicyOthersInfo()
		{
			base.dtModel.TableName = "APP_UMBRELLA_POL_INFO_OTHER";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table APP_UMBRELLA_POL_INFO_OTHER
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("APP_ID",typeof(int));
			base.dtModel.Columns.Add("APP_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("COMBINED_SINGLE_LIMIT",typeof(double));
			base.dtModel.Columns.Add("BODILY_INJURY",typeof(double));
			base.dtModel.Columns.Add("PROPERTY_DAMAGE",typeof(double));
			//base.dtModel.Columns.Add("IS_ACTIVE",typeof(string));
			//base.dtModel.Columns.Add("CREATED_BY",typeof(int));
			//base.dtModel.Columns.Add("CREATED_DATETIME",typeof(DateTime));
			//base.dtModel.Columns.Add("MODIFIED_BY",typeof(int));
			//base.dtModel.Columns.Add("LAST_UPDATED_DATETIME",typeof(DateTime));
		}
		#region Database schema details
		// model for database field CUSTOMER_ID(int)
		public int CUSTOMER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CUSTOMER_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_ID"] = value;
			}
		}
		// model for database field APP_ID(int)
		public int APP_ID
		{
			get
			{
				return base.dtModel.Rows[0]["APP_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["APP_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APP_ID"] = value;
			}
		}
		// model for database field APP_VERSION_ID(int)
		public int APP_VERSION_ID
		{
			get
			{
				return base.dtModel.Rows[0]["APP_VERSION_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["APP_VERSION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APP_VERSION_ID"] = value;
			}
		}
		// model for database field COMBINED_SINGLE_LIMIT(double)
		public double COMBINED_SINGLE_LIMIT
		{
			get
			{
				return base.dtModel.Rows[0]["COMBINED_SINGLE_LIMIT"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["COMBINED_SINGLE_LIMIT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COMBINED_SINGLE_LIMIT"] = value;
			}
		}
		// model for database field BODILY_INJURY(double)
		public double BODILY_INJURY
		{
			get
			{
				return base.dtModel.Rows[0]["BODILY_INJURY"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["BODILY_INJURY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["BODILY_INJURY"] = value;
			}
		}
		// model for database field PROPERTY_DAMAGE(double)
		public double PROPERTY_DAMAGE
		{
			get
			{
				return base.dtModel.Rows[0]["PROPERTY_DAMAGE"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["PROPERTY_DAMAGE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PROPERTY_DAMAGE"] = value;
			}
		}

//		// model for database field IS_ACTIVE(string)
//		public string IS_ACTIVE
//		{
//			get
//			{
//				return base.dtModel.Rows[0]["IS_ACTIVE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_ACTIVE"].ToString();
//			}
//			set
//			{
//				base.dtModel.Rows[0]["IS_ACTIVE"] = value;
//			}
//		}

		// model for database field CREATED_BY(int)
//		public int CREATED_BY
//		{
//			get
//			{
//				return base.dtModel.Rows[0]["CREATED_BY"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CREATED_BY"].ToString());
//			}
//			set
//			{
//				base.dtModel.Rows[0]["CREATED_BY"] = value;
//			}
//		}
//		
//		// model for database field CREATED_DATETIME(DateTime)
//		public DateTime CREATED_DATETIME
//		{
//			get
//			{
//				return base.dtModel.Rows[0]["CREATED_DATETIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["CREATED_DATETIME"].ToString());
//			}
//			set
//			{
//				base.dtModel.Rows[0]["CREATED_DATETIME"] = value;
//			}
//		}
//		
//		// model for database field MODIFIED_BY(int)
//		public int MODIFIED_BY
//		{
//			get
//			{
//				return base.dtModel.Rows[0]["MODIFIED_BY"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["MODIFIED_BY"].ToString());
//			}
//			set
//			{
//				base.dtModel.Rows[0]["MODIFIED_BY"] = value;
//			}
//		}
//
//		// model for database field LAST_UPDATED_DATETIME(int)
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
		#endregion
	}
}



