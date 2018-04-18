/******************************************************************************************
<Author				: -   Amar
<Start Date				: -	5/1/2006 5:06:18 PM
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
namespace Cms.Model.Claims
{
	/// <summary>
	/// Database Model for CLM_INSURED_VEHICLE.
	/// </summary>
	public class ClsInsuredVehicleInfo : Cms.Model.ClsCommonModel
	{
		private const string CLM_INSURED_VEHICLE = "CLM_INSURED_VEHICLE";
		public ClsInsuredVehicleInfo()
		{
			base.dtModel.TableName = "CLM_INSURED_VEHICLE";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table CLM_INSURED_VEHICLE
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("INSURED_VEHICLE_ID",typeof(int));
			base.dtModel.Columns.Add("CLAIM_ID",typeof(int));
			base.dtModel.Columns.Add("NON_OWNED_VEHICLE",typeof(string));
			base.dtModel.Columns.Add("VEHICLE_YEAR",typeof(string));
			base.dtModel.Columns.Add("MAKE",typeof(string));
			base.dtModel.Columns.Add("MODEL",typeof(string));
			base.dtModel.Columns.Add("VIN",typeof(string));
			base.dtModel.Columns.Add("BODY_TYPE",typeof(string));
			base.dtModel.Columns.Add("PLATE_NUMBER",typeof(string));
			base.dtModel.Columns.Add("STATE",typeof(int));
			base.dtModel.Columns.Add("WHERE_VEHICLE_SEEN",typeof(string));
			base.dtModel.Columns.Add("WHEN_VEHICLE_SEEN",typeof(string));
			base.dtModel.Columns.Add("OWNER_ID",typeof(int));
			base.dtModel.Columns.Add("DRIVER_ID",typeof(int));

			base.dtModel.Columns.Add("PURPOSE_OF_USE",typeof(string));
			base.dtModel.Columns.Add("USED_WITH_PERMISSION",typeof(int));
			base.dtModel.Columns.Add("DESCRIBE_DAMAGE",typeof(string));
			base.dtModel.Columns.Add("ESTIMATE_AMOUNT",typeof(double));			
			base.dtModel.Columns.Add("OTHER_VEHICLE_INSURANCE",typeof(string));			
			base.dtModel.Columns.Add("POLICY_VEHICLE_ID",typeof(int));
		}
		#region Database schema details
		// model for database field INSURED_VEHICLE_ID(int)
		public int INSURED_VEHICLE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["INSURED_VEHICLE_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["INSURED_VEHICLE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INSURED_VEHICLE_ID"] = value;
			}
		}
		// model for database field CLAIM_ID(int)
		public int CLAIM_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CLAIM_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CLAIM_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CLAIM_ID"] = value;
			}
		}
		// model for database field NON_OWNED_VEHICLE(string)
		public string NON_OWNED_VEHICLE
		{
			get
			{
				return base.dtModel.Rows[0]["NON_OWNED_VEHICLE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["NON_OWNED_VEHICLE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["NON_OWNED_VEHICLE"] = value;
			}
		}
		// model for database field VEHICLE_YEAR(string)
		public string VEHICLE_YEAR
		{
			get
			{
				return base.dtModel.Rows[0]["VEHICLE_YEAR"] == DBNull.Value ? "" : base.dtModel.Rows[0]["VEHICLE_YEAR"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["VEHICLE_YEAR"] = value;
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
		// model for database field VIN(string)
		public string VIN
		{
			get
			{
				return base.dtModel.Rows[0]["VIN"] == DBNull.Value ? "" : base.dtModel.Rows[0]["VIN"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["VIN"] = value;
			}
		}
		// model for database field BODY_TYPE(string)
		public string BODY_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["BODY_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["BODY_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BODY_TYPE"] = value;
			}
		}
		// model for database field PLATE_NUMBER(string)
		public string PLATE_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["PLATE_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PLATE_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PLATE_NUMBER"] = value;
			}
		}
		// model for database field STATE(int)
		public int STATE
		{
			get
			{
				return base.dtModel.Rows[0]["STATE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["STATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["STATE"] = value;
			}
		}
		// model for database field WHERE_VEHICLE_SEEN(string)
		public string WHERE_VEHICLE_SEEN
		{
			get
			{
				return base.dtModel.Rows[0]["WHERE_VEHICLE_SEEN"] == DBNull.Value ? "" : base.dtModel.Rows[0]["WHERE_VEHICLE_SEEN"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["WHERE_VEHICLE_SEEN"] = value;
			}
		}
		// model for database field WHEN_VEHICLE_SEEN(string)
		public string WHEN_VEHICLE_SEEN
		{
			get
			{
				return base.dtModel.Rows[0]["WHEN_VEHICLE_SEEN"] == DBNull.Value ? "" : base.dtModel.Rows[0]["WHEN_VEHICLE_SEEN"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["WHEN_VEHICLE_SEEN"] = value;
			}
		}		

		// model for database field OWNER_ID(int)
		public int OWNER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["OWNER_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["OWNER_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["OWNER_ID"] = value;
			}
		}

		// model for database field DRIVER_ID(int)
		public int DRIVER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["DRIVER_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_ID"] = value;
			}
		}
		// model for database field PURPOSE_OF_USE(string)
		public string PURPOSE_OF_USE
		{
			get
			{
				return base.dtModel.Rows[0]["PURPOSE_OF_USE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PURPOSE_OF_USE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PURPOSE_OF_USE"] = value;
			}
		}		
		
		// model for database field USED_WITH_PERMISSION(int)
		public int USED_WITH_PERMISSION
		{
			get
			{
				return base.dtModel.Rows[0]["USED_WITH_PERMISSION"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["USED_WITH_PERMISSION"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["USED_WITH_PERMISSION"] = value;
			}
		}

		// model for database field DESCRIBE_DAMAGE(string)
		public string DESCRIBE_DAMAGE
		{
			get
			{
				return base.dtModel.Rows[0]["DESCRIBE_DAMAGE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DESCRIBE_DAMAGE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESCRIBE_DAMAGE"] = value;
			}
		}		
		
		// model for database field ESTIMATE_AMOUNT(double)
		public double ESTIMATE_AMOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["ESTIMATE_AMOUNT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["ESTIMATE_AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ESTIMATE_AMOUNT"] = value;
			}
		}
		// model for database field OTHER_VEHICLE_INSURANCE(string)
		public string OTHER_VEHICLE_INSURANCE
		{
			get
			{
				return base.dtModel.Rows[0]["OTHER_VEHICLE_INSURANCE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["OTHER_VEHICLE_INSURANCE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["OTHER_VEHICLE_INSURANCE"] = value;
			}
		}		
		// model for database field POLICY_VEHICLE_ID(int)
		public int POLICY_VEHICLE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_VEHICLE_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["POLICY_VEHICLE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_VEHICLE_ID"] = value;
			}
		}

		#endregion
	}
}
