/******************************************************************************************
<Author				: -   Mohit Gupta
<Start Date				: -	5/16/2005 11:03:24 AM
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
	/// Database Model for APP_PROTECT_DEVICES.
	/// </summary>
	public class ClsProtectDevicesInfo : Cms.Model.ClsBaseModel
	{
		private const string APP_PROTECT_DEVICES = "APP_PROTECT_DEVICES";

		public ClsProtectDevicesInfo()
		{
			base.dtModel.TableName = "APP_PROTECT_DEVICES";	 // setting table name for data table that holds property values.
			this.AddColumns();								 // add columns of the database table APP_PROTECT_DEVICES
			base.dtModel.Rows.Add(base.dtModel.NewRow());	 // add a blank row in the datatable
		}

		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("APP_ID",typeof(int));
			base.dtModel.Columns.Add("APP_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("DWELLING_ID",typeof(int));
			base.dtModel.Columns.Add("PROTECTIVE_DEVICES",typeof(string));
			base.dtModel.Columns.Add("TEMPERATURE",typeof(int));
			base.dtModel.Columns.Add("SMOKE",typeof(int));
			base.dtModel.Columns.Add("BURGLAR",typeof(int));
			base.dtModel.Columns.Add("BURGLAR_CODE",typeof(string));
			base.dtModel.Columns.Add("FIRE_PLACES",typeof(string));
			base.dtModel.Columns.Add("NO_OF_WOOD_STOVES",typeof(int));
			base.dtModel.Columns.Add("SWIMMING_POOL",typeof(string));
			base.dtModel.Columns.Add("SWIMMING_POOL_TYPE",typeof(string));
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
		// model for database field DWELLING_ID(int)
		public int DWELLING_ID
		{
			get
			{
				return base.dtModel.Rows[0]["DWELLING_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DWELLING_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DWELLING_ID"] = value;
			}
		}
		// model for database field PROTECTIVE_DEVICES(string)
		public string PROTECTIVE_DEVICES
		{
			get
			{
				return base.dtModel.Rows[0]["PROTECTIVE_DEVICES"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PROTECTIVE_DEVICES"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PROTECTIVE_DEVICES"] = value;
			}
		}
		// model for database field TEMPERATURE(int)
		public int TEMPERATURE
		{
			get
			{
				return base.dtModel.Rows[0]["TEMPERATURE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["TEMPERATURE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["TEMPERATURE"] = value;
			}
		}
		// model for database field SMOKE(int)
		public int SMOKE
		{
			get
			{
				return base.dtModel.Rows[0]["SMOKE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["SMOKE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SMOKE"] = value;
			}
		}
		// model for database field BURGLAR(int)
		public int BURGLAR
		{
			get
			{
				return base.dtModel.Rows[0]["BURGLAR"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["BURGLAR"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["BURGLAR"] = value;
			}
		}

		// model for database field BURGLAR(int)
		public string BURGLAR_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["BURGLAR_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["BURGLAR_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BURGLAR_CODE"] = value;
			}
		}

		// model for database field FIRE_PLACES(string)
		public string FIRE_PLACES
		{
			get
			{
				return base.dtModel.Rows[0]["FIRE_PLACES"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["FIRE_PLACES"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["FIRE_PLACES"] = value;
			}
		}
		// model for database field NO_OF_WOOD_STOVES(int)
		public int NO_OF_WOOD_STOVES
		{
			get
			{
				return base.dtModel.Rows[0]["NO_OF_WOOD_STOVES"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["NO_OF_WOOD_STOVES"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NO_OF_WOOD_STOVES"] = value;
			}
		}
		// model for database field SWIMMING_POOL(string)
		public string SWIMMING_POOL
		{
			get
			{
				return base.dtModel.Rows[0]["SWIMMING_POOL"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SWIMMING_POOL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SWIMMING_POOL"] = value;
			}
		}
		// model for database field SWIMMING_POOL_TYPE(int)
		public string SWIMMING_POOL_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["SWIMMING_POOL_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SWIMMING_POOL_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SWIMMING_POOL_TYPE"] = value;
			}
		}
		#endregion
	}
}
