using System;
using System.Data;
using Cms.Model;

namespace Cms.Model.Policy.Watercraft
{
	/// <summary>
	/// Summary description for ClsPolicyVehicleEndorsementInfo.
	/// </summary>
	public class ClsPolicyVehicleEndorsementInfo : Cms.Model.ClsCommonModel 
	{

		public ClsPolicyVehicleEndorsementInfo()
		{
			base.dtModel.TableName = "POL_WATERCRAFT_ENDORSEMENTS";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table POLICY_VEHICLES
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("VEHICLE_ID",typeof(int));
			base.dtModel.Columns.Add("ENDORSEMENT_ID",typeof(int));
			base.dtModel.Columns.Add("REMARKS",typeof(string));
			base.dtModel.Columns.Add("ENDORSEMENT",typeof(string));
			base.dtModel.Columns.Add("VEHICLE_ENDORSEMENT_ID",typeof(int));
			base.dtModel.Columns.Add("ACTION",typeof(string));
		
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
		// model for database field POLICY_ID(int)
		public int POLICY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["POLICY_ID"].ToString());
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
				return base.dtModel.Rows[0]["POLICY_VERSION_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["POLICY_VERSION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_VERSION_ID"] = value;
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

		// model for database field VEHICLE_ID(int)
		public int ENDORSEMENT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ENDORSEMENT_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["ENDORSEMENT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ENDORSEMENT_ID"] = value;
			}
		}
		
		// model for database field VEHICLE_ID(int)
		public int VEHICLE_ENDORSEMENT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["VEHICLE_ENDORSEMENT_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["VEHICLE_ENDORSEMENT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["VEHICLE_ENDORSEMENT_ID"] = value;
			}
		}

		// model for database field VEHICLE_YEAR(int)
		public string REMARKS
		{
			get
			{
				return base.dtModel.Rows[0]["REMARKS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["REMARKS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REMARKS"] = value;
			}
		}
		// model for field ACTION(STRING)
		public string ACTION
		{
			get
			{
				return base.dtModel.Rows[0]["ACTION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ACTION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ACTION"] = value;
			}
		}
		
		// model for database field VEHICLE_YEAR(int)
		public string ENDORSEMENT
		{
			get
			{
				return base.dtModel.Rows[0]["ENDORSEMENT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ENDORSEMENT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ENDORSEMENT"] = value;
			}
		}

	}
}
