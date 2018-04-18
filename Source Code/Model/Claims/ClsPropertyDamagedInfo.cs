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
	/// Database Model for CLM_PROPERTY_DAMAGED.
	/// </summary>
	public class ClsPropertyDamagedInfo : Cms.Model.ClsCommonModel
	{
		private const string CLM_PROPERTY_DAMAGED = "CLM_PROPERTY_DAMAGED";
		public ClsPropertyDamagedInfo()
		{
			base.dtModel.TableName = "CLM_PROPERTY_DAMAGED";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table CLM_PROPERTY_DAMAGED
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("PROPERTY_DAMAGED_ID",typeof(int));
			base.dtModel.Columns.Add("CLAIM_ID",typeof(int));
			base.dtModel.Columns.Add("DAMAGED_ANOTHER_VEHICLE",typeof(int));
			base.dtModel.Columns.Add("NON_OWNED_VEHICLE",typeof(string));			
			base.dtModel.Columns.Add("VEHICLE_ID",typeof(int));
			base.dtModel.Columns.Add("VEHICLE_YEAR",typeof(string));
			base.dtModel.Columns.Add("MAKE",typeof(string));
			base.dtModel.Columns.Add("MODEL",typeof(string));
			base.dtModel.Columns.Add("VIN",typeof(string));
			base.dtModel.Columns.Add("BODY_TYPE",typeof(string));
			base.dtModel.Columns.Add("PLATE_NUMBER",typeof(string));			
			base.dtModel.Columns.Add("DESCRIPTION",typeof(string));
			base.dtModel.Columns.Add("OTHER_INSURANCE",typeof(int));
			base.dtModel.Columns.Add("AGENCY_NAME",typeof(string));
			base.dtModel.Columns.Add("POLICY_NUMBER",typeof(string));
			base.dtModel.Columns.Add("OWNER_ID",typeof(int));
			base.dtModel.Columns.Add("DRIVER_ID",typeof(int));
			base.dtModel.Columns.Add("ESTIMATE_AMOUNT",typeof(double));
			//base.dtModel.Columns.Add("TYPE_OF_HOME",typeof(string));
			base.dtModel.Columns.Add("ADDRESS1",typeof(string));
			base.dtModel.Columns.Add("ADDRESS2",typeof(string));
			base.dtModel.Columns.Add("CITY",typeof(string));
			base.dtModel.Columns.Add("STATE",typeof(int));
			base.dtModel.Columns.Add("ZIP",typeof(string));
			base.dtModel.Columns.Add("COUNTRY",typeof(int));
			base.dtModel.Columns.Add("PROP_DAMAGED_TYPE",typeof(int));
			base.dtModel.Columns.Add("PARTY_TYPE",typeof(int));
			base.dtModel.Columns.Add("PARTY_TYPE_DESC",typeof(string));
			
		}
		#region Database schema details
		// model for database field PROPERTY_DAMAGED_ID(int)
		public int PROPERTY_DAMAGED_ID
		{
			get
			{
				return base.dtModel.Rows[0]["PROPERTY_DAMAGED_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PROPERTY_DAMAGED_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PROPERTY_DAMAGED_ID"] = value;
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
		// model for database field DAMAGED_ANOTHER_VEHICLE(string)
		public int DAMAGED_ANOTHER_VEHICLE
        {
            get
            {
                return base.dtModel.Rows[0]["DAMAGED_ANOTHER_VEHICLE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["DAMAGED_ANOTHER_VEHICLE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["DAMAGED_ANOTHER_VEHICLE"] = value;
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
		// model for database field VEHICLE_ID(int)
		public int VEHICLE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["VEHICLE_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["VEHICLE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["VEHICLE_ID"] = value;
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
		// model for database field DESCRIPTION(string)
		public string DESCRIPTION
		{
			get
			{
				return base.dtModel.Rows[0]["DESCRIPTION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DESCRIPTION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESCRIPTION"] = value;
			}
		}
		// model for database field OTHER_INSURANCE(string)
		
		public int OTHER_INSURANCE
		{
			get
			{
                return base.dtModel.Rows[0]["OTHER_INSURANCE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["OTHER_INSURANCE"].ToString());
			}
			set
			{
                base.dtModel.Rows[0]["OTHER_INSURANCE"] = value;
			}
		}
		// model for database field AGENCY_NAME(string)
		public string AGENCY_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["AGENCY_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["AGENCY_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["AGENCY_NAME"] = value;
			}
		}	
		// model for database field POLICY_NUMBER(string)
		public string POLICY_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["POLICY_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_NUMBER"] = value;
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
		// model for database field TYPE_OF_HOME(string)
		/*public string TYPE_OF_HOME
		{
			get
			{
				return base.dtModel.Rows[0]["TYPE_OF_HOME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["TYPE_OF_HOME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TYPE_OF_HOME"] = value;
			}
		}*/	
		// model for database field ADDRESS1(string)
		public string ADDRESS1
		{
			get
			{
				return base.dtModel.Rows[0]["ADDRESS1"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ADDRESS1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ADDRESS1"] = value;
			}
		}
		// model for database field ADDRESS2(string)
		public string ADDRESS2
		{
			get
			{
				return base.dtModel.Rows[0]["ADDRESS2"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ADDRESS2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ADDRESS2"] = value;
			}
		}
		// model for database field CITY(string)
		public string CITY
		{
			get
			{
				return base.dtModel.Rows[0]["CITY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CITY"] = value;
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
		// model for database field ZIP(string)
		public string ZIP
		{
			get
			{
				return base.dtModel.Rows[0]["ZIP"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ZIP"] = value;
			}
		}
		// model for database field COUNTRY(int)
		public int COUNTRY
		{
			get
			{
				return base.dtModel.Rows[0]["COUNTRY"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["COUNTRY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COUNTRY"] = value;
			}
		}
		// model for database field PROP_DAMAGED_TYPE(int)
		public int PROP_DAMAGED_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["PROP_DAMAGED_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PROP_DAMAGED_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PROP_DAMAGED_TYPE"] = value;
			}
		}		
		// model for database field PARTY_TYPE(int)
		public int PARTY_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["PARTY_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PARTY_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PARTY_TYPE"] = value;
			}
		}
		// model for database field PARTY_TYPE_DESC(string)
		public string PARTY_TYPE_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["PARTY_TYPE_DESC"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PARTY_TYPE_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PARTY_TYPE_DESC"] = value;
			}
		}
		#endregion
	}
}
