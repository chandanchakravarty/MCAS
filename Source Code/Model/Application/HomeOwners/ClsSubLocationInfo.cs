/******************************************************************************************
<Author				: -   Vijay Joshi
<Start Date				: -	5/12/2005 2:53:59 PM
<End Date				: -	
<Description				: - 	Model class for APP_SUB_LOCATIONS
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
namespace Cms.Model.Application.HomeOwners
{
	/// <summary>
	/// Database Model for APP_SUB_LOCATIONS.
	/// </summary>
	public class ClsSubLocationInfo : Cms.Model.ClsCommonModel
	{
		private const string APP_SUB_LOCATIONS = "APP_SUB_LOCATIONS";
		public ClsSubLocationInfo()
		{
			base.dtModel.TableName = "APP_SUB_LOCATIONS";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table APP_SUB_LOCATIONS
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
			base.dtModel.Columns.Add("ID",typeof(string));
		//<Gaurav> 1 June 2005 ; START: Following fields not required now; BUG No<543>
			/*
			base.dtModel.Columns.Add("SUB_LOC_YEAR_BUILT",typeof(int));
			base.dtModel.Columns.Add("SUB_LOC_AREA_IN_SQ_FOOT",typeof(string));
			base.dtModel.Columns.Add("SUB_LOC_PROT_CLASS",typeof(string));
			base.dtModel.Columns.Add("SUB_LOC_HYDRANT_DIST",typeof(double));
			base.dtModel.Columns.Add("SUB_LOC_FIRE_DIST",typeof(double));
			base.dtModel.Columns.Add("SUB_LOC_INSIDE_CITY_LIMITS",typeof(string));
			base.dtModel.Columns.Add("SUB_LOC_SURROUND_EXP",typeof(string));
			*/
		//<Gaurav> 1 June 2005 ; END: Following fields not required now; BUG No<543>
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

		//<Gaurav> 1 June 2005 ; START: Following fields not required now; BUG No<543>
		/*
		// model for database field SUB_LOC_YEAR_BUILT(int)
		public int SUB_LOC_YEAR_BUILT
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_LOC_YEAR_BUILT"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["SUB_LOC_YEAR_BUILT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SUB_LOC_YEAR_BUILT"] = value;
			}
		}
		// model for database field SUB_LOC_AREA_IN_SQ_FOOT(string)
		public string SUB_LOC_AREA_IN_SQ_FOOT
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_LOC_AREA_IN_SQ_FOOT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SUB_LOC_AREA_IN_SQ_FOOT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SUB_LOC_AREA_IN_SQ_FOOT"] = value;
			}
		}
		// model for database field SUB_LOC_PROT_CLASS(string)
		public string SUB_LOC_PROT_CLASS
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_LOC_PROT_CLASS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SUB_LOC_PROT_CLASS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SUB_LOC_PROT_CLASS"] = value;
			}
		}
		// model for database field SUB_LOC_HYDRANT_DIST(double)
		public double SUB_LOC_HYDRANT_DIST
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_LOC_HYDRANT_DIST"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["SUB_LOC_HYDRANT_DIST"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SUB_LOC_HYDRANT_DIST"] = value;
			}
		}
		// model for database field SUB_LOC_FIRE_DIST(double)
		public double SUB_LOC_FIRE_DIST
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_LOC_FIRE_DIST"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["SUB_LOC_FIRE_DIST"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SUB_LOC_FIRE_DIST"] = value;
			}
		}
		// model for database field SUB_LOC_INSIDE_CITY_LIMITS(string)
		public string SUB_LOC_INSIDE_CITY_LIMITS
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_LOC_INSIDE_CITY_LIMITS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SUB_LOC_INSIDE_CITY_LIMITS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SUB_LOC_INSIDE_CITY_LIMITS"] = value;
			}
		}
		
		// model for database field SUB_LOC_SURROUND_EXP(string)
		public string SUB_LOC_SURROUND_EXP
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_LOC_SURROUND_EXP"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SUB_LOC_SURROUND_EXP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SUB_LOC_SURROUND_EXP"] = value;
			}
		}
		*/
	//<Gaurav> 1 June 2005 ; END: Following fields not required now; BUG No<543>
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
			
		public string ID
		{
			get
			{
				return base.dtModel.Rows[0]["ID"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ID"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ID"] = value;
			}
		}
		
		#endregion
	}
}
