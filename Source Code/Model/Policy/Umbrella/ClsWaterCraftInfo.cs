/******************************************************************************************
<Author					: -   Sumit Chhabra
<Start Date				: -	03/27/2005 
<End Date				: -	
<Description				: - 	Model Class for Policy Umbrella Watercraft Information
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
namespace Cms.Model.Policy.Umbrella
{
	/// <summary>
	/// Database Model for POL_UMBRELLA_WATERCRAFT_INFO.
	/// </summary>
	public class ClsWaterCraftInfo : Cms.Model.ClsCommonModel
	{
		private const string POL_UMBRELLA_WATERCRAFT_INFO = "POL_UMBRELLA_WATERCRAFT_INFO";
		public ClsWaterCraftInfo()
		{
			base.dtModel.TableName = "POL_UMBRELLA_WATERCRAFT_INFO";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table POL_UMBRELLA_WATERCRAFT_INFO
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("BOAT_ID",typeof(int));
			base.dtModel.Columns.Add("BOAT_NO",typeof(int));
			base.dtModel.Columns.Add("BOAT_NAME",typeof(string));
			base.dtModel.Columns.Add("YEAR",typeof(int));
			base.dtModel.Columns.Add("MAKE",typeof(string));
			base.dtModel.Columns.Add("MODEL",typeof(string));
			base.dtModel.Columns.Add("HULL_ID_NO",typeof(string));
			base.dtModel.Columns.Add("STATE_REG",typeof(string));
			base.dtModel.Columns.Add("OTHER_HULL_TYPE",typeof(string));
			base.dtModel.Columns.Add("HULL_MATERIAL",typeof(int));
			base.dtModel.Columns.Add("FUEL_TYPE",typeof(int));
			base.dtModel.Columns.Add("DATE_PURCHASED",typeof(DateTime));
			base.dtModel.Columns.Add("LENGTH",typeof(string));
			base.dtModel.Columns.Add("WEIGHT",typeof(string));
			base.dtModel.Columns.Add("MAX_SPEED",typeof(double));
			base.dtModel.Columns.Add("BERTH_LOC",typeof(string));
			base.dtModel.Columns.Add("WATERS_NAVIGATED",typeof(string));
			base.dtModel.Columns.Add("TERRITORY",typeof(string));
			base.dtModel.Columns.Add("TYPE_OF_WATERCRAFT",typeof(string));
			base.dtModel.Columns.Add("INSURING_VALUE",typeof(double));
			base.dtModel.Columns.Add("WATERCRAFT_HORSE_POWER",typeof(int));
			base.dtModel.Columns.Add("TWIN_SINGLE",typeof(int));
			base.dtModel.Columns.Add("DESC_OTHER_WATERCRAFT",typeof(string));
			base.dtModel.Columns.Add("INCHES",typeof(string));
			base.dtModel.Columns.Add("LORAN_NAV_SYSTEM",typeof(int));
			base.dtModel.Columns.Add("DIESEL_ENGINE",typeof(int));
			base.dtModel.Columns.Add("SHORE_STATION",typeof(int));
			base.dtModel.Columns.Add("HALON_FIRE_EXT_SYSTEM",typeof(int));
			base.dtModel.Columns.Add("DUAL_OWNERSHIP",typeof(int));
			base.dtModel.Columns.Add("REMOVE_SAILBOAT",typeof(int));
			base.dtModel.Columns.Add("COV_TYPE_BASIS",typeof(int));
			base.dtModel.Columns.Add("LOCATION_ADDRESS",typeof(string));
			base.dtModel.Columns.Add("LOCATION_CITY",typeof(string));
			base.dtModel.Columns.Add("LOCATION_STATE",typeof(string));
			base.dtModel.Columns.Add("LOCATION_ZIP",typeof(string));
			base.dtModel.Columns.Add("USED_PARTICIPATE",typeof(int));
			base.dtModel.Columns.Add("WATERCRAFT_CONTEST",typeof(string));
			base.dtModel.Columns.Add("OTHER_POLICY",typeof(string));
			base.dtModel.Columns.Add("IS_BOAT_EXCLUDED",typeof(int));
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
		// model for database field BOAT_ID(int)
		public int BOAT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["BOAT_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["BOAT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["BOAT_ID"] = value;
			}
		}
		// model for database field BOAT_NO(int)
		public int BOAT_NO
		{
			get
			{
				return base.dtModel.Rows[0]["BOAT_NO"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["BOAT_NO"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["BOAT_NO"] = value;
			}
		}
		// model for database field BOAT_NAME(string)
		public string BOAT_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["BOAT_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["BOAT_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BOAT_NAME"] = value;
			}
		}
		// model for database field YEAR(int)
		public int YEAR
		{
			get
			{
				return base.dtModel.Rows[0]["YEAR"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["YEAR"].ToString());
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
				return base.dtModel.Rows[0]["MAKE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["MAKE"].ToString();
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
				return base.dtModel.Rows[0]["MODEL"] == DBNull.Value ? "" : base.dtModel.Rows[0]["MODEL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MODEL"] = value;
			}
		}
		// model for database field HULL_ID_NO(string)
		public string HULL_ID_NO
		{
			get
			{
				return base.dtModel.Rows[0]["HULL_ID_NO"] == DBNull.Value ? "" : base.dtModel.Rows[0]["HULL_ID_NO"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HULL_ID_NO"] = value;
			}
		}
		// model for database field STATE_REG(string)
		public string STATE_REG
		{
			get
			{
				return base.dtModel.Rows[0]["STATE_REG"] == DBNull.Value ? "" : base.dtModel.Rows[0]["STATE_REG"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["STATE_REG"] = value;
			}
		}
		// model for database field COV_TYPE_BASIS(int)
		public int COV_TYPE_BASIS
		{
			get
			{
				return base.dtModel.Rows[0]["COV_TYPE_BASIS"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["COV_TYPE_BASIS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COV_TYPE_BASIS"] = value;
			}
			
		}
		// model for database field OTHER_HULL_TYPE(string)
		public string OTHER_HULL_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["OTHER_HULL_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["OTHER_HULL_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["OTHER_HULL_TYPE"] = value;
			}
		}
		// model for database field HULL_MATERIAL(int)
		public int HULL_MATERIAL
		{
			get
			{
				return base.dtModel.Rows[0]["HULL_MATERIAL"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["HULL_MATERIAL"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["HULL_MATERIAL"] = value;
			}
		}
		// model for database field FUEL_TYPE(int)
		public int FUEL_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["FUEL_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["FUEL_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["FUEL_TYPE"] = value;
			}
		}
		// model for database field DATE_PURCHASED(DateTime)
		public DateTime DATE_PURCHASED
		{
			get
			{
				return base.dtModel.Rows[0]["DATE_PURCHASED"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["DATE_PURCHASED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DATE_PURCHASED"] = value;
			}
		}
		// model for database field LENGTH(string)
		public string LENGTH
		{
			get
			{
				return base.dtModel.Rows[0]["LENGTH"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LENGTH"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LENGTH"] = value;
			}
		}
		// model for database field WEIGHT(string)
		public string WEIGHT
		{
			get
			{
				return base.dtModel.Rows[0]["WEIGHT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["WEIGHT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["WEIGHT"] = value;
			}
		}
		// model for database field MAX_SPEED(double)
		public double MAX_SPEED
		{
			get
			{
				return base.dtModel.Rows[0]["MAX_SPEED"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["MAX_SPEED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MAX_SPEED"] = value;
			}
		}
		// model for database field BERTH_LOC(string)
		public string BERTH_LOC
		{
			get
			{
				return base.dtModel.Rows[0]["BERTH_LOC"] == DBNull.Value ? "" : base.dtModel.Rows[0]["BERTH_LOC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BERTH_LOC"] = value;
			}
		}
		// model for database field WATERS_NAVIGATED(string)
		public string WATERS_NAVIGATED
		{
			get
			{
				return base.dtModel.Rows[0]["WATERS_NAVIGATED"] == DBNull.Value ? "" : base.dtModel.Rows[0]["WATERS_NAVIGATED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["WATERS_NAVIGATED"] = value;
			}
		}
		// model for database field TERRITORY(string)
		public string TERRITORY
		{
			get
			{
				return base.dtModel.Rows[0]["TERRITORY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["TERRITORY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TERRITORY"] = value;
			}
		}
		// model for database field TYPE_OF_WATERCRAFT(string)
		public string TYPE_OF_WATERCRAFT
		{
			get
			{
				return base.dtModel.Rows[0]["TYPE_OF_WATERCRAFT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["TYPE_OF_WATERCRAFT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TYPE_OF_WATERCRAFT"] = value;
			}
		}
		// model for database field INSURING_VALUE(double)
		public double INSURING_VALUE
		{
			get
			{
				return base.dtModel.Rows[0]["INSURING_VALUE"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["INSURING_VALUE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INSURING_VALUE"] = value;
			}
		}
		// model for database field WATERCRAFT_HORSE_POWER(int)
		public int WATERCRAFT_HORSE_POWER
		{
			get
			{
				return base.dtModel.Rows[0]["WATERCRAFT_HORSE_POWER"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["WATERCRAFT_HORSE_POWER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["WATERCRAFT_HORSE_POWER"] = value;
			}
		}
		// model for database field TWIN_SINGLE(int)
		public int TWIN_SINGLE
		{
			get
			{
				return base.dtModel.Rows[0]["TWIN_SINGLE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["TWIN_SINGLE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["TWIN_SINGLE"] = value;
			}
		}
		// model for database field DESC_OTHER_WATERCRAFT(string)
		public string DESC_OTHER_WATERCRAFT
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_OTHER_WATERCRAFT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DESC_OTHER_WATERCRAFT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_OTHER_WATERCRAFT"] = value;
			}
		}
		public string INCHES
		{
			get
			{
				return base.dtModel.Rows[0]["INCHES"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["INCHES"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["INCHES"] = value;
			}
		}

		// model for database field LORAN_NAV_SYSTEM(int)
		public int LORAN_NAV_SYSTEM
		{
			get
			{
				return base.dtModel.Rows[0]["LORAN_NAV_SYSTEM"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LORAN_NAV_SYSTEM"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LORAN_NAV_SYSTEM"] = value;
			}
		}

		// model for database field DIESEL_ENGINE(int)
		public int DIESEL_ENGINE
		{
			get
			{
				return base.dtModel.Rows[0]["DIESEL_ENGINE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DIESEL_ENGINE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DIESEL_ENGINE"] = value;
			}
		}

		
		// model for database field SHORE_STATION(int)
		public int SHORE_STATION
		{
			get
			{
				return base.dtModel.Rows[0]["SHORE_STATION"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["SHORE_STATION"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SHORE_STATION"] = value;
			}
		}

		// model for database field HALON_FIRE_EXT_SYSTEM(int)
		public int HALON_FIRE_EXT_SYSTEM
		{
			get
			{
				return base.dtModel.Rows[0]["HALON_FIRE_EXT_SYSTEM"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["HALON_FIRE_EXT_SYSTEM"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["HALON_FIRE_EXT_SYSTEM"] = value;
			}
		}
			
			
		// model for database field DUAL_OWNERSHIP(int)
		public int DUAL_OWNERSHIP
		{
			get
			{
				return base.dtModel.Rows[0]["DUAL_OWNERSHIP"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DUAL_OWNERSHIP"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DUAL_OWNERSHIP"] = value;
			}
		}

		// model for database field REMOVE_SAILBOAT(int)
		public int REMOVE_SAILBOAT
		{
			get
			{
				return base.dtModel.Rows[0]["REMOVE_SAILBOAT"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["REMOVE_SAILBOAT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REMOVE_SAILBOAT"] = value;
			}
		}

		//Model for LOCATION_ADDRESS (string)
					   public string LOCATION_ADDRESS
					   {
						   get
						   {
							   return base.dtModel.Rows[0]["LOCATION_ADDRESS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LOCATION_ADDRESS"].ToString();
						   }
						   set
						   {
							   base.dtModel.Rows[0]["LOCATION_ADDRESS"] = value;
						   }
					   }



		//Model for LOCATION_CITY (string)
		public string LOCATION_CITY
		{
			get
			{
				return base.dtModel.Rows[0]["LOCATION_CITY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LOCATION_CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOCATION_CITY"] = value;
			}
		}



		//Model for LOCATION_STATE (string)
		public string LOCATION_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["LOCATION_STATE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LOCATION_STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOCATION_STATE"] = value;
			}
		}


		//Model for LOCATION_ZIP (string)
		public string LOCATION_ZIP
		{
			get
			{
				return base.dtModel.Rows[0]["LOCATION_ZIP"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LOCATION_ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOCATION_ZIP"] = value;
			}
		}
		// model for database field USED_PARTICIPATE(int)
		public int USED_PARTICIPATE
		{
			get
			{
				return base.dtModel.Rows[0]["USED_PARTICIPATE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["USED_PARTICIPATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["USED_PARTICIPATE"] = value;
			}
		}

		//Model for WATERCRAFT_CONTEST (string)
		public string WATERCRAFT_CONTEST
		{
			get
			{
				return base.dtModel.Rows[0]["WATERCRAFT_CONTEST"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["WATERCRAFT_CONTEST"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["WATERCRAFT_CONTEST"] = value;
			}
		}

		// model for database field OTHER_POLICY(string)
		public string OTHER_POLICY 
		{
			get
			{
				return base.dtModel.Rows[0]["OTHER_POLICY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["OTHER_POLICY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["OTHER_POLICY"] = value;
			}
		}
		// model for database field IS_BOAT_EXCLUDED(int)
		public int IS_BOAT_EXCLUDED
		{
			get
			{
				return base.dtModel.Rows[0]["IS_BOAT_EXCLUDED"] == DBNull.Value ? Convert.ToInt32(null) : Convert.ToInt32(base.dtModel.Rows[0]["IS_BOAT_EXCLUDED"]);
			}
			set
			{
				base.dtModel.Rows[0]["IS_BOAT_EXCLUDED"] = value;
			}
		}

		#endregion
	}
}
