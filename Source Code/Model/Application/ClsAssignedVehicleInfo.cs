/******************************************************************************************
<Author				: -   Priya
<Start Date				: -	5/3/2005 5:26:05 PM
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
	/// Database Model for APP_ASSIGN_VEHICLE.
	/// </summary>
	public class ClsAssignedVehicleInfo : Cms.Model.ClsCommonModel
	{
		private const string APP_ASSIGN_VEHICLE = "APP_ASSIGN_VEHICLE";
		public ClsAssignedVehicleInfo()
		{
			base.dtModel.TableName = "APP_ASSIGN_VEHICLE";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table APP_ASSIGN_VEHICLE
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("APP_ID",typeof(int));
			base.dtModel.Columns.Add("APP_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("DRIVER_ID",typeof(int));
			base.dtModel.Columns.Add("VEHICLE_ID",typeof(int));
			base.dtModel.Columns.Add("INSURED_DRIVER_NO",typeof(int));
			base.dtModel.Columns.Add("PERCENT_DRIVEN",typeof(int));
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
		public int VEHICLE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["VEHICLE_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["VEHICLE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["VEHICLE_ID"] = value;
			}
		}
		// model for database field INSURED_DRIVER_NO(int)
		public int INSURED_DRIVER_NO
		{
			get
			{
				return base.dtModel.Rows[0]["INSURED_DRIVER_NO"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["INSURED_DRIVER_NO"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INSURED_DRIVER_NO"] = value;
			}
		}
		// model for database field PERCENT_DRIVEN(int)
		public int PERCENT_DRIVEN
		{
			get
			{
				return base.dtModel.Rows[0]["PERCENT_DRIVEN"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["PERCENT_DRIVEN"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PERCENT_DRIVEN"] = value;
			}
		}
		#endregion
	}
}
