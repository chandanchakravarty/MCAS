/******************************************************************************************
<Author					: -  Ravindra Kumar Gupta
<Start Date				: -	 Feb 15, 2006
<End Date				: -	
<Description			: -  Model Class for UMBRELLA DWELLINGS
<Review Date			: - 
<Reviewed By			: - 	
Modification History
************************************************************************************************/
using System;
using System.Data;
using System.Collections;
using Cms.Model;

namespace Cms.Model.Application
{
	/// <summary>
	/// Summary description for ClsUmbrellaDwellingInfo.
	/// </summary>
	public class ClsUmbrellaDwellingInfo : Cms.Model.ClsCommonModel
	{
		#region Private Variable Declaration
		
		private const string APP_UMBRELLA_DWELLINGS_INFO = "APP_UMBRELLA_DWELLINGS_INFO";
		
		#endregion
		
		public ClsUmbrellaDwellingInfo()
		{
			base.dtModel.TableName ="APP_UMBRELLA_DWELLING_INFO";
			this.AddColumns();
			base.dtModel.Rows.Add(base.dtModel.NewRow());
	
		}

		#region Private Function Definetions
		private void AddColumns()
		{

			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("APP_ID",typeof(int));
			base.dtModel.Columns.Add("APP_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("DWELLING_ID",typeof(int));
			base.dtModel.Columns.Add("DWELLING_NUMBER",typeof(int));
			base.dtModel.Columns.Add("LOCATION_ID",typeof(int));
			base.dtModel.Columns.Add("SUB_LOC_ID",typeof(int));
			base.dtModel.Columns.Add("YEAR_BUILT",typeof(int));
			base.dtModel.Columns.Add("PURCHASE_YEAR",typeof(int));
			base.dtModel.Columns.Add("PURCHASE_PRICE",typeof(double));
			base.dtModel.Columns.Add("MARKET_VALUE",typeof(double));
			base.dtModel.Columns.Add("REPLACEMENT_COST",typeof(double));
			base.dtModel.Columns.Add("BUILDING_TYPE",typeof(int));
			base.dtModel.Columns.Add("OCCUPANCY",typeof(int));
			base.dtModel.Columns.Add("OCCUPANCY_CODE",typeof(string));
			base.dtModel.Columns.Add("NEED_OF_UNITS",typeof(int));
			base.dtModel.Columns.Add("USAGE",typeof(string));
			base.dtModel.Columns.Add("NEIGHBOURS_VISIBLE",typeof(string));
			base.dtModel.Columns.Add("OCCUPIED_DAILY",typeof(string));
			base.dtModel.Columns.Add("NO_WEEKS_RENTED",typeof(int));
			base.dtModel.Columns.Add("LOCATION_REF",typeof(string));
			base.dtModel.Columns.Add("SUB_LOCATION_REF",typeof(string));
			base.dtModel.Columns.Add("REPAIR_COST",typeof(double));
			
		}
		#endregion
		#region Database Schema details
		
		// model for database field CUSTOMER_ID(int)
		public int CUSTOMER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_ID"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["CUSTOMER_ID"].ToString());
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
				return base.dtModel.Rows[0]["APP_ID"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["APP_ID"].ToString());
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
				return base.dtModel.Rows[0]["APP_VERSION_ID"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["APP_VERSION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APP_VERSION_ID"] = value;
			}
		}
		
		// model for database field DWELLING_ID(int)
		public int DWELLING_ID
		{
			get
			{
				return base.dtModel.Rows[0]["DWELLING_ID"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["DWELLING_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DWELLING_ID"] = value;
			}
		}
		
		// model for database field DWELLING_NUMBER(int)
		public int DWELLING_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["DWELLING_NUMBER"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["DWELLING_NUMBER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DWELLING_NUMBER"] = value;
			}
		}
		
		// model for database field LOCATION_ID(int)
		public int LOCATION_ID
		{
			get
			{
				return base.dtModel.Rows[0]["LOCATION_ID"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["LOCATION_ID"].ToString());
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
				return base.dtModel.Rows[0]["SUB_LOC_ID"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["SUB_LOC_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SUB_LOC_ID"] = value;
			}
		}
		
		// model for database field YEAR_BUILT(int)
		public int YEAR_BUILT
		{
			get
			{
				return base.dtModel.Rows[0]["YEAR_BUILT"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["YEAR_BUILT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["YEAR_BUILT"] = value;
			}
		}
		
		// model for database field PURCHASE_YEAR(int)
		public int PURCHASE_YEAR
		{
			get
			{
				return base.dtModel.Rows[0]["PURCHASE_YEAR"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["PURCHASE_YEAR"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PURCHASE_YEAR"] = value;
			}
		}
		
		// model for database field PURCHASE_PRICE(double)
		public double PURCHASE_PRICE
		{
			get
			{
				return base.dtModel.Rows[0]["PURCHASE_PRICE"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["PURCHASE_PRICE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PURCHASE_PRICE"] = value;
			}
		}
		
		// model for database field MARKET_VALUE(double)
		public double MARKET_VALUE
		{
			get
			{
				return base.dtModel.Rows[0]["MARKET_VALUE"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["MARKET_VALUE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MARKET_VALUE"] = value;
			}
		}
		
		// model for database field REPLACEMENT_COST(double)
		public double REPLACEMENT_COST
		{
			get
			{
				return base.dtModel.Rows[0]["REPLACEMENT_COST"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["REPLACEMENT_COST"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REPLACEMENT_COST"] = value;
			}
		}
		
		// model for database field BUILDING_TYPE(int)
		public int BUILDING_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["BUILDING_TYPE"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["BUILDING_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["BUILDING_TYPE"] = value;
			}
		}
		
		// model for database field OCCUPANCY(int)
		public int OCCUPANCY
		{
			get
			{
				return base.dtModel.Rows[0]["OCCUPANCY"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["OCCUPANCY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["OCCUPANCY"] = value;
			}
		}
		
		// model for database field NEED_OF_UNITS(int)
		public int NEED_OF_UNITS
		{
			get
			{
				return base.dtModel.Rows[0]["NEED_OF_UNITS"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["NEED_OF_UNITS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NEED_OF_UNITS"] = value;
			}
		}
		
		// model for database field USAGE(string)
		public string USAGE
		{
			get
			{
				return base.dtModel.Rows[0]["USAGE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["USAGE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["USAGE"] = value;
			}
		}
		
		// model for database field NEIGHBOURS_VISIBLE(string)
		public string NEIGHBOURS_VISIBLE
		{
			get
			{
				return base.dtModel.Rows[0]["NEIGHBOURS_VISIBLE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["NEIGHBOURS_VISIBLE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["NEIGHBOURS_VISIBLE"] = value;
			}
		}
		
		// model for database field OCCUPIED_DAILY(string)
		public string OCCUPIED_DAILY
		{
			get
			{
				return base.dtModel.Rows[0]["OCCUPIED_DAILY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["OCCUPIED_DAILY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["OCCUPIED_DAILY"] = value;
			}
		}
		
		// model for database field NO_WEEKS_RENTED(int)
		public int NO_WEEKS_RENTED
		{
			get
			{
				return base.dtModel.Rows[0]["NO_WEEKS_RENTED"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["NO_WEEKS_RENTED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NO_WEEKS_RENTED"] = value;
			}
		}
		
		
		// For Repair Cost

		public double REPAIR_COST
		{
			get
			{
				return base.dtModel.Rows[0]["REPAIR_COST"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["REPAIR_COST"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REPAIR_COST"] = value;
			}
		}


		#endregion

	}
}
