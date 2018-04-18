/******************************************************************************************
<Author				: -   Nidhi
<Start Date				: -	5/18/2005 6:10:16 PM
<End Date				: -	
<Description				: - 	Model class for Watercraft Engine
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 11/10/2005
<Modified By			: - Anurag Verma
<Purpose				: - Adding Insuring Value field and removing fuel_type,current_value,limits_desired,deductible,premium
*******************************************************************************************/ 
using System;
using System.Data;
using Cms.Model;
namespace Cms.Model.Application.Watercrafts
{
	/// <summary>
	/// Database Model for APP_WATERCRAFT_ENGINE_INFO.
	/// </summary>
	public class ClsWatercraftEngineInfo  : Cms.Model.ClsCommonModel
	{
		private const string APP_WATERCRAFT_ENGINE_INFO = "APP_WATERCRAFT_ENGINE_INFO";
		public ClsWatercraftEngineInfo ()
		{
			base.dtModel.TableName = "APP_WATERCRAFT_ENGINE_INFO";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table APP_WATERCRAFT_ENGINE_INFO
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("APP_ID",typeof(int));
			base.dtModel.Columns.Add("APP_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("ENGINE_ID",typeof(int));
			base.dtModel.Columns.Add("ENGINE_NO",typeof(string));
			base.dtModel.Columns.Add("YEAR",typeof(int));
			base.dtModel.Columns.Add("MAKE",typeof(string));
			base.dtModel.Columns.Add("MODEL",typeof(string));
			base.dtModel.Columns.Add("SERIAL_NO",typeof(string));
			base.dtModel.Columns.Add("HORSEPOWER",typeof(string));
			
			base.dtModel.Columns.Add("INSURING_VALUE",typeof(double));
			base.dtModel.Columns.Add("ASSOCIATED_BOAT",typeof(int));
			base.dtModel.Columns.Add("OTHER",typeof(string));
			base.dtModel.Columns.Add("FUEL_TYPE",typeof(int));
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
		// model for database field ENGINE_ID(int)
		public int ENGINE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ENGINE_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["ENGINE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ENGINE_ID"] = value;
			}
		}
		// model for database field ENGINE_NO(string)
		public string ENGINE_NO
		{
			get
			{
				return base.dtModel.Rows[0]["ENGINE_NO"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ENGINE_NO"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ENGINE_NO"] = value;
			}
		}
		// model for database field YEAR(int)
		public int YEAR
		{
			get
			{
				return base.dtModel.Rows[0]["YEAR"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["YEAR"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["YEAR"] = value;
			}
		}
		// model for database field MAKE(string)
		public string MAKE
		{
			get
			{
				return base.dtModel.Rows[0]["MAKE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MAKE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MAKE"] = value;
			}
		}
		// model for database field MODEL(string)
		public string MODEL
		{
			get
			{
				return base.dtModel.Rows[0]["MODEL"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MODEL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MODEL"] = value;
			}
		}
		// model for database field SERIAL_NO(string)
		public string SERIAL_NO
		{
			get
			{
				return base.dtModel.Rows[0]["SERIAL_NO"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SERIAL_NO"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SERIAL_NO"] = value;
			}
		}
		// model for database field HORSEPOWER(string)
		public string HORSEPOWER
		{
			get
			{
				return base.dtModel.Rows[0]["HORSEPOWER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HORSEPOWER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HORSEPOWER"] = value;
			}
		}
		
		// model for database field INSURING_VALUE(double)
		public double INSURING_VALUE
		{
			get
			{
				return base.dtModel.Rows[0]["INSURING_VALUE"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["INSURING_VALUE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INSURING_VALUE"] = value;
			}
		}
		// model for database field ASSOCIATED_BOAT(int)
		public int ASSOCIATED_BOAT
		{
			get
			{
				return base.dtModel.Rows[0]["ASSOCIATED_BOAT"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["ASSOCIATED_BOAT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ASSOCIATED_BOAT"] = value;
			}
		}
		// model for database field OTHER(string)
		public string OTHER
		{
			get
			{
				return base.dtModel.Rows[0]["OTHER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["OTHER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["OTHER"] = value;
			}
		}
		// model for database field FUEL_TYPE(int)
		public int FUEL_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["FUEL_TYPE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["FUEL_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["FUEL_TYPE"] = value;
			}
		}
		#endregion
	}
}
