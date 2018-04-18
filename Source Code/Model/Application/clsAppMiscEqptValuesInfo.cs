/******************************************************************************************
<Author				: -   Nidhi
<Start Date				: -	4/28/2005 12:02:25 PM
<End Date				: -	
<Description				: - 	Model Class for Vehicle information
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using System.Collections;
using System.Data;
using Cms.Model;
namespace Cms.Model.Application
{
	/// <summary>
	/// Database Model for APP_MISCELLANEOUS_EQUIPMENT_VALUES.
	/// </summary>
	public class clsAppMiscEqptValuesInfo : Cms.Model.ClsCommonModel
	{
		private const string APP_MISCELLANEOUS_EQUIPMENT_VALUES = "APP_MISCELLANEOUS_EQUIPMENT_VALUES";
		
		
		public clsAppMiscEqptValuesInfo()
		{
			base.dtModel.TableName = "APP_MISCELLANEOUS_EQUIPMENT_VALUES";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table APP_MISCELLANEOUS_EQUIPMENT_VALUES
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		

		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("APP_ID",typeof(int));
			base.dtModel.Columns.Add("APP_VERSION_ID",typeof(int));			
			base.dtModel.Columns.Add("VEHICLE_ID",typeof(int));
			base.dtModel.Columns.Add("ITEM_ID",typeof(int));
			base.dtModel.Columns.Add("ITEM_DESCRIPTION",typeof(string));
			base.dtModel.Columns.Add("ITEM_VALUE",typeof(double));
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
		// model for database field ITEM_ID(int)
		public int ITEM_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ITEM_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["ITEM_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ITEM_ID"] = value;
			}
		}				
		// model for database field ITEM_DESCRIPTION(string)
		public string ITEM_DESCRIPTION
		{
			get
			{
				return base.dtModel.Rows[0]["ITEM_DESCRIPTION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ITEM_DESCRIPTION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ITEM_DESCRIPTION"] = value;
			}
		}
		// model for database field ITEM_VALUE(double)
		public double ITEM_VALUE
		{
			get
			{
				return base.dtModel.Rows[0]["ITEM_VALUE"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["ITEM_VALUE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ITEM_VALUE"] = value;
			}
		}
		#endregion
	}
}
