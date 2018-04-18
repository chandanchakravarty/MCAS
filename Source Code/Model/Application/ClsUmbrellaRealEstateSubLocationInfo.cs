/******************************************************************************************
<Author				: -   Priya
<Start Date				: -	5/30/2005 12:17:12 PM
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
	/// Database Model for APP_UMBRELLA_REAL_ESTATE_SUB_LOC.
	/// </summary>
	public class ClsUmbrellaRealEstateSubLocInfo: Cms.Model.ClsCommonModel
	{
		private const string APP_UMBRELLA_REAL_ESTATE_SUB_LOC = "APP_UMBRELLA_REAL_ESTATE_SUB_LOC";
		public ClsUmbrellaRealEstateSubLocInfo()
		{
			base.dtModel.TableName = "APP_UMBRELLA_REAL_ESTATE_SUB_LOC";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table APP_UMBRELLA_REAL_ESTATE_SUB_LOC
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("APP_ID",typeof(int));
			base.dtModel.Columns.Add("APP_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("LOCATION_ID",typeof(int));
			base.dtModel.Columns.Add("SUB_LOC_ID",typeof(int));
			base.dtModel.Columns.Add("SUB_LOC_NUMBER",typeof(int));
			base.dtModel.Columns.Add("SUB_LOC_TYPE",typeof(string));
			base.dtModel.Columns.Add("SUB_LOC_DESC",typeof(string));
			base.dtModel.Columns.Add("SUB_LOC_CITY_LIMITS",typeof(int));
			base.dtModel.Columns.Add("SUB_LOC_INTEREST",typeof(int));
			base.dtModel.Columns.Add("SUB_LOC_OCCUPIED_PERCENT",typeof(double));
			base.dtModel.Columns.Add("SUB_LOC_OCC_DESC",typeof(string));
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
		// model for database field LOCATION_ID(int)
		public int LOCATION_ID
		{
			get
			{
				return base.dtModel.Rows[0]["LOCATION_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LOCATION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LOCATION_ID"] = value;
			}
		}
		// model for database field SUB_LOC_ID(int)
		public int SUB_LOC_ID
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_LOC_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["SUB_LOC_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SUB_LOC_ID"] = value;
			}
		}
		// model for database field SUB_LOC_NUMBER(int)
		public int SUB_LOC_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_LOC_NUMBER"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["SUB_LOC_NUMBER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SUB_LOC_NUMBER"] = value;
			}
		}
		// model for database field SUB_LOC_TYPE(string)
		public string SUB_LOC_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_LOC_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SUB_LOC_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SUB_LOC_TYPE"] = value;
			}
		}
		// model for database field SUB_LOC_DESC(string)
		public string SUB_LOC_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_LOC_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SUB_LOC_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SUB_LOC_DESC"] = value;
			}
		}
		// model for database field SUB_LOC_CITY_LIMITS(int)
		public int SUB_LOC_CITY_LIMITS
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_LOC_CITY_LIMITS"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["SUB_LOC_CITY_LIMITS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SUB_LOC_CITY_LIMITS"] = value;
			}
		}
		// model for database field SUB_LOC_INTEREST(int)
		public int SUB_LOC_INTEREST
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_LOC_INTEREST"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["SUB_LOC_INTEREST"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SUB_LOC_INTEREST"] = value;
			}
		}
		// model for database field SUB_LOC_OCCUPIED_PERCENT(double)
		public double SUB_LOC_OCCUPIED_PERCENT
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_LOC_OCCUPIED_PERCENT"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["SUB_LOC_OCCUPIED_PERCENT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SUB_LOC_OCCUPIED_PERCENT"] = value;
			}
		}
		// model for database field SUB_LOC_OCC_DESC(string)
		public string SUB_LOC_OCC_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_LOC_OCC_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SUB_LOC_OCC_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SUB_LOC_OCC_DESC"] = value;
			}
		}
		// model for database field IS_ACTIVE(string)
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
//		// model for database field CREATED_BY(int)
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
		#endregion
	}
}
