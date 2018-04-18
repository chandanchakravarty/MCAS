/******************************************************************************************
<Author				: -   Vijay Arora
<Start Date				: -	11/30/2005 10:16:38 AM
<End Date				: -	
<Description				: - 	Model Class for Policy WaterCraft Equipment Info
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
namespace Cms.Model.Policy.Watercraft
{
	/// <summary>
	/// Database Model for POL_WATERCRAFT_EQUIP_DETAILLS.
	/// </summary>
	public class ClsPolicyWatercraftEquipmentInfo : Cms.Model.ClsCommonModel
	{
		private const string POL_WATERCRAFT_EQUIP_DETAILLS = "POL_WATERCRAFT_EQUIP_DETAILLS";
		public ClsPolicyWatercraftEquipmentInfo()
		{
			base.dtModel.TableName = "POL_WATERCRAFT_EQUIP_DETAILLS";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table POL_WATERCRAFT_EQUIP_DETAILLS
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("EQUIP_ID",typeof(int));
			base.dtModel.Columns.Add("EQUIP_NO",typeof(int));
			base.dtModel.Columns.Add("EQUIP_TYPE",typeof(int));
			base.dtModel.Columns.Add("SHIP_TO_SHORE",typeof(int));
			base.dtModel.Columns.Add("YEAR",typeof(int));
			base.dtModel.Columns.Add("MAKE",typeof(string));
			base.dtModel.Columns.Add("MODEL",typeof(string));
			base.dtModel.Columns.Add("SERIAL_NO",typeof(string));
			base.dtModel.Columns.Add("ASSOCIATED_BOAT",typeof(int));
			base.dtModel.Columns.Add("INSURED_VALUE",typeof(double));
			base.dtModel.Columns.Add("EQUIP_AMOUNT",typeof(double));
			base.dtModel.Columns.Add("OTHER_DESCRIPTION",typeof(string));
			base.dtModel.Columns.Add("EQUIPMENT_TYPE",typeof(int));
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
		// model for database field EQUIP_ID(int)
		public int EQUIP_ID
		{
			get
			{
				return base.dtModel.Rows[0]["EQUIP_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["EQUIP_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EQUIP_ID"] = value;
			}
		}
		// model for database field EQUIP_NO(int)
		public int EQUIP_NO
		{
			get
			{
				return base.dtModel.Rows[0]["EQUIP_NO"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["EQUIP_NO"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EQUIP_NO"] = value;
			}
		}
		// model for database field EQUIP_TYPE(int)
		public int EQUIP_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["EQUIP_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["EQUIP_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EQUIP_TYPE"] = value;
			}
		}
		// model for database field SHIP_TO_SHORE(int)
		public int SHIP_TO_SHORE
		{
			get
			{
				return base.dtModel.Rows[0]["SHIP_TO_SHORE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["SHIP_TO_SHORE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SHIP_TO_SHORE"] = value;
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
		// model for database field SERIAL_NO(string)
		public string SERIAL_NO
		{
			get
			{
				return base.dtModel.Rows[0]["SERIAL_NO"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SERIAL_NO"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SERIAL_NO"] = value;
			}
		}
		// model for database field ASSOCIATED_BOAT(int)
		public int ASSOCIATED_BOAT
		{
			get
			{
				return base.dtModel.Rows[0]["ASSOCIATED_BOAT"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ASSOCIATED_BOAT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ASSOCIATED_BOAT"] = value;
			}
		}
		// model for database field INSURED_VALUE(double)
		public double INSURED_VALUE
		{
			get
			{
				return base.dtModel.Rows[0]["INSURED_VALUE"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["INSURED_VALUE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INSURED_VALUE"] = value;
			}
		}
		public double EQUIP_AMOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["EQUIP_AMOUNT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["EQUIP_AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EQUIP_AMOUNT"] = value;
			}
		}
		// model for database field OTHER_DESCRIPTION (string)
		public string OTHER_DESCRIPTION
		{
			get
			{
				return base.dtModel.Rows[0]["OTHER_DESCRIPTION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["OTHER_DESCRIPTION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["OTHER_DESCRIPTION"] = value;
			}
		}
		// model for database field EQUIPMENT_TYPE(int)
		public int EQUIPMENT_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["EQUIPMENT_TYPE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["EQUIPMENT_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EQUIPMENT_TYPE"] = value;
			}
		}
		#endregion
	}
}
