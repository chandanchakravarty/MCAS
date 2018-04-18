/******************************************************************************************
<Author				: -   Mohit Gupta
<Start Date				: -	5/19/2005 12:58:43 PM
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


namespace Cms.Model.Application.HomeOwners
{
	/// <summary>
	/// Summary description for ClsUnitClearanceInfo.
	/// </summary>
	public class ClsUnitClearanceInfo :  Cms.Model.ClsCommonModel 
	{
		private const string APP_HOME_OWNER_UNIT_CLEARANCE = "APP_HOME_OWNER_UNIT_CLEARANCE";
		public ClsUnitClearanceInfo()
		{
			base.dtModel.TableName = "APP_HOME_OWNER_UNIT_CLEARANCE";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table APP_HOME_OWNER_UNIT_CLEARANCE
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("APP_ID",typeof(int));
			base.dtModel.Columns.Add("APP_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("FUEL_ID",typeof(int));
			base.dtModel.Columns.Add("STOVE_INSTALL_SPEC",typeof(string));
			base.dtModel.Columns.Add("DIST_REAR_WALL_FEET",typeof(int));
			base.dtModel.Columns.Add("DIST_REAR_WALL_INCHES",typeof(int));
			base.dtModel.Columns.Add("DIST_LEFT_WALL_FEET",typeof(int));
			base.dtModel.Columns.Add("DIST_LEFT_WALL_INCHES",typeof(int));
			base.dtModel.Columns.Add("DIST_RIGHT_WALL_FEET",typeof(int));
			base.dtModel.Columns.Add("DIST_RIGHT_WALL_INCHES",typeof(int));
			base.dtModel.Columns.Add("DIST_BOTTOM_FLOOR_FEET",typeof(int));
			base.dtModel.Columns.Add("DIST_BOTTOM_FLOOR_INCHES",typeof(int));
			base.dtModel.Columns.Add("DIA_PIPE_FEET",typeof(int));
			base.dtModel.Columns.Add("DIA_PIPE_INCHES",typeof(int));
			base.dtModel.Columns.Add("FRONT_PROTECTION_FEET",typeof(int));
			base.dtModel.Columns.Add("FRONT_PROTECTION_INCHES",typeof(int));
			base.dtModel.Columns.Add("STOVE_WALL_FEET",typeof(int));
			base.dtModel.Columns.Add("STOVE_WALL_INCHES",typeof(int));
			base.dtModel.Columns.Add("TOP_CEILING_FEET",typeof(int));
			base.dtModel.Columns.Add("TOP_CEILING_INCHES",typeof(int));
			base.dtModel.Columns.Add("SHORT_DIST_WALL_FEET",typeof(int));
			base.dtModel.Columns.Add("SHORT_DIST_WALL_INCHES",typeof(int));
			base.dtModel.Columns.Add("SHORT_DIST_CEILING_FEET",typeof(int));
			base.dtModel.Columns.Add("SHORT_DIST_CEILING_INCHES",typeof(int));
			base.dtModel.Columns.Add("DIST_COMBUSTIBLE_FEET",typeof(int));
			base.dtModel.Columns.Add("DIST_COMBUSTIBLE_INCHES",typeof(int));
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
		// model for database field FUEL_ID(int)
		public int FUEL_ID
		{
			get
			{
				return base.dtModel.Rows[0]["FUEL_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["FUEL_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["FUEL_ID"] = value;
			}
		}
		// model for database field STOVE_INSTALL_SPEC(string)
		public string STOVE_INSTALL_SPEC
		{
			get
			{
				return base.dtModel.Rows[0]["STOVE_INSTALL_SPEC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["STOVE_INSTALL_SPEC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["STOVE_INSTALL_SPEC"] = value;
			}
		}
		// model for database field DIST_REAR_WALL_FEET(int)
		public int DIST_REAR_WALL_FEET
		{
			get
			{
				return base.dtModel.Rows[0]["DIST_REAR_WALL_FEET"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DIST_REAR_WALL_FEET"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DIST_REAR_WALL_FEET"] = value;
			}
		}
		// model for database field DIST_REAR_WALL_INCHES(int)
		public int DIST_REAR_WALL_INCHES
		{
			get
			{
				return base.dtModel.Rows[0]["DIST_REAR_WALL_INCHES"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DIST_REAR_WALL_INCHES"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DIST_REAR_WALL_INCHES"] = value;
			}
		}
		// model for database field DIST_LEFT_WALL_FEET(int)
		public int DIST_LEFT_WALL_FEET
		{
			get
			{
				return base.dtModel.Rows[0]["DIST_LEFT_WALL_FEET"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DIST_LEFT_WALL_FEET"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DIST_LEFT_WALL_FEET"] = value;
			}
		}
		// model for database field DIST_LEFT_WALL_INCHES(int)
		public int DIST_LEFT_WALL_INCHES
		{
			get
			{
				return base.dtModel.Rows[0]["DIST_LEFT_WALL_INCHES"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DIST_LEFT_WALL_INCHES"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DIST_LEFT_WALL_INCHES"] = value;
			}
		}
		// model for database field DIST_RIGHT_WALL_FEET(int)
		public int DIST_RIGHT_WALL_FEET
		{
			get
			{
				return base.dtModel.Rows[0]["DIST_RIGHT_WALL_FEET"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DIST_RIGHT_WALL_FEET"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DIST_RIGHT_WALL_FEET"] = value;
			}
		}
		// model for database field DIST_RIGHT_WALL_INCHES(int)
		public int DIST_RIGHT_WALL_INCHES
		{
			get
			{
				return base.dtModel.Rows[0]["DIST_RIGHT_WALL_INCHES"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DIST_RIGHT_WALL_INCHES"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DIST_RIGHT_WALL_INCHES"] = value;
			}
		}
		// model for database field DIST_BOTTOM_FLOOR_FEET(int)
		public int DIST_BOTTOM_FLOOR_FEET
		{
			get
			{
				return base.dtModel.Rows[0]["DIST_BOTTOM_FLOOR_FEET"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DIST_BOTTOM_FLOOR_FEET"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DIST_BOTTOM_FLOOR_FEET"] = value;
			}
		}
		// model for database field DIST_BOTTOM_FLOOR_INCHES(int)
		public int DIST_BOTTOM_FLOOR_INCHES
		{
			get
			{
				return base.dtModel.Rows[0]["DIST_BOTTOM_FLOOR_INCHES"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DIST_BOTTOM_FLOOR_INCHES"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DIST_BOTTOM_FLOOR_INCHES"] = value;
			}
		}
		// model for database field DIA_PIPE_FEET(int)
		public int DIA_PIPE_FEET
		{
			get
			{
				return base.dtModel.Rows[0]["DIA_PIPE_FEET"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DIA_PIPE_FEET"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DIA_PIPE_FEET"] = value;
			}
		}
		// model for database field DIA_PIPE_INCHES(int)
		public int DIA_PIPE_INCHES
		{
			get
			{
				return base.dtModel.Rows[0]["DIA_PIPE_INCHES"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DIA_PIPE_INCHES"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DIA_PIPE_INCHES"] = value;
			}
		}
		// model for database field FRONT_PROTECTION_FEET(int)
		public int FRONT_PROTECTION_FEET
		{
			get
			{
				return base.dtModel.Rows[0]["FRONT_PROTECTION_FEET"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["FRONT_PROTECTION_FEET"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["FRONT_PROTECTION_FEET"] = value;
			}
		}
		// model for database field FRONT_PROTECTION_INCHES(int)
		public int FRONT_PROTECTION_INCHES
		{
			get
			{
				return base.dtModel.Rows[0]["FRONT_PROTECTION_INCHES"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["FRONT_PROTECTION_INCHES"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["FRONT_PROTECTION_INCHES"] = value;
			}
		}
		// model for database field STOVE_WALL_FEET(int)
		public int STOVE_WALL_FEET
		{
			get
			{
				return base.dtModel.Rows[0]["STOVE_WALL_FEET"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["STOVE_WALL_FEET"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["STOVE_WALL_FEET"] = value;
			}
		}
		// model for database field STOVE_WALL_INCHES(int)
		public int STOVE_WALL_INCHES
		{
			get
			{
				return base.dtModel.Rows[0]["STOVE_WALL_INCHES"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["STOVE_WALL_INCHES"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["STOVE_WALL_INCHES"] = value;
			}
		}
		// model for database field TOP_CEILING_FEET(int)
		public int TOP_CEILING_FEET
		{
			get
			{
				return base.dtModel.Rows[0]["TOP_CEILING_FEET"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["TOP_CEILING_FEET"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["TOP_CEILING_FEET"] = value;
			}
		}
		// model for database field TOP_CEILING_INCHES(int)
		public int TOP_CEILING_INCHES
		{
			get
			{
				return base.dtModel.Rows[0]["TOP_CEILING_INCHES"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["TOP_CEILING_INCHES"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["TOP_CEILING_INCHES"] = value;
			}
		}
		// model for database field SHORT_DIST_WALL_FEET(int)
		public int SHORT_DIST_WALL_FEET
		{
			get
			{
				return base.dtModel.Rows[0]["SHORT_DIST_WALL_FEET"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["SHORT_DIST_WALL_FEET"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SHORT_DIST_WALL_FEET"] = value;
			}
		}
		// model for database field SHORT_DIST_WALL_INCHES(int)
		public int SHORT_DIST_WALL_INCHES
		{
			get
			{
				return base.dtModel.Rows[0]["SHORT_DIST_WALL_INCHES"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["SHORT_DIST_WALL_INCHES"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SHORT_DIST_WALL_INCHES"] = value;
			}
		}
		// model for database field SHORT_DIST_CEILING_FEET(int)
		public int SHORT_DIST_CEILING_FEET
		{
			get
			{
				return base.dtModel.Rows[0]["SHORT_DIST_CEILING_FEET"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["SHORT_DIST_CEILING_FEET"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SHORT_DIST_CEILING_FEET"] = value;
			}
		}
		// model for database field SHORT_DIST_CEILING_INCHES(int)
		public int SHORT_DIST_CEILING_INCHES
		{
			get
			{
				return base.dtModel.Rows[0]["SHORT_DIST_CEILING_INCHES"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["SHORT_DIST_CEILING_INCHES"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SHORT_DIST_CEILING_INCHES"] = value;
			}
		}
		// model for database field DIST_COMBUSTIBLE_FEET(int)
		public int DIST_COMBUSTIBLE_FEET
		{
			get
			{
				return base.dtModel.Rows[0]["DIST_COMBUSTIBLE_FEET"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DIST_COMBUSTIBLE_FEET"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DIST_COMBUSTIBLE_FEET"] = value;
			}
		}
		// model for database field DIST_COMBUSTIBLE_INCHES(int)
		public int DIST_COMBUSTIBLE_INCHES
		{
			get
			{
				return base.dtModel.Rows[0]["DIST_COMBUSTIBLE_INCHES"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DIST_COMBUSTIBLE_INCHES"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DIST_COMBUSTIBLE_INCHES"] = value;
			}
		}
		#endregion


	}
}
