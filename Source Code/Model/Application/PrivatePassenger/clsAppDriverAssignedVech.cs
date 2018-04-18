using System;
using System.Data;
using Cms.Model;

namespace Cms.Model.Application.PrivatePassenger
{
	/// <summary>
	/// Summary description for clsAppDriverAssignedVech.
	/// </summary>
	public class clsAppDriverAssignedVech : Cms.Model.ClsCommonModel
	{
		private const string APP_UMBRELLA_LIMITS = "APP_UMBRELLA_LIMITS";

		public clsAppDriverAssignedVech()
		{
			base.dtModel.TableName = "APP_DRIVER_ASSIGNED_VEHICLE";		// setting table name for data table that holds property values.
			this.AddColumns();											// add columns of the database table APP_UMBRELLA_LIMITS
			base.dtModel.Rows.Add(base.dtModel.NewRow());				// add a blank row in the datatable
		}

		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("APP_ID",typeof(int));
			base.dtModel.Columns.Add("APP_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("VEHICLE_ID",typeof(string));
			base.dtModel.Columns.Add("DRIVER_ID",typeof(int));
			base.dtModel.Columns.Add("APP_VEHICLE_PRIN_OCC_ID",typeof(int));
			
		}

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
		// model for database field DRIVER_ID(int)
		public int DRIVER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DRIVER_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_ID"] = value;
			}
		}
		// model for database field VEHICLE_ID(int)
		public string VEHICLE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["VEHICLE_ID"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["VEHICLE_ID"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["VEHICLE_ID"] = value;
			}
		}
		// model for database field APP_VEHICLE_PRIN_OCC_ID(int)
		public string APP_VEHICLE_PRIN_OCC_ID
		{
			get
			{
				return base.dtModel.Rows[0]["APP_VEHICLE_PRIN_OCC_ID"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["APP_VEHICLE_PRIN_OCC_ID"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["APP_VEHICLE_PRIN_OCC_ID"] = value;
			}
		}
	}
}
