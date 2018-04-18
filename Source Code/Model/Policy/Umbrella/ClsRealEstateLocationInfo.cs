/******************************************************************************************
<Author					: -   Ravindra Gupta
<Start Date				: -	  03/20/2006
<End Date				: -	
<Description			: -   Models POL_UMBRELLA_REAL_ESTATE_LOCATION
<Review Date			: - 
<Reviewed By			: - 	
Modification History

*******************************************************************************************/ 
using System;
using System.Data;
using Cms.Model;

namespace Cms.Model.Policy.Umbrella
{
	/// <summary>
	/// Database Model for POL_UMBRELLA_REAL_ESTATE_LOCATION.
	/// </summary>
	public class ClsRealEstateLocationInfo : Cms.Model.ClsCommonModel
	{
		
		#region Constructor
		public ClsRealEstateLocationInfo()
		{
			base.dtModel.TableName = "POL_UMBRELLA_REAL_ESTATE_LOCATION";		
			this.AddColumns();							
			base.dtModel.Rows.Add(base.dtModel.NewRow());
		}
		#endregion

		#region AddColumns Function
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("LOCATION_ID",typeof(int));
			base.dtModel.Columns.Add("CLIENT_LOCATION_NUMBER",typeof(string));
			base.dtModel.Columns.Add("LOCATION_NUMBER",typeof(int));
			base.dtModel.Columns.Add("ADDRESS_1",typeof(string));
			base.dtModel.Columns.Add("ADDRESS_2",typeof(string));
			base.dtModel.Columns.Add("CITY",typeof(string));
			base.dtModel.Columns.Add("COUNTY",typeof(string));
			base.dtModel.Columns.Add("STATE",typeof(int));
			base.dtModel.Columns.Add("ZIPCODE",typeof(string));
			base.dtModel.Columns.Add("PHONE_NUMBER",typeof(string));
			base.dtModel.Columns.Add("FAX_NUMBER",typeof(string));
			base.dtModel.Columns.Add("REMARKS",typeof(string));
			base.dtModel.Columns.Add("OCCUPIED_BY",typeof(int));
			base.dtModel.Columns.Add("NUM_FAMILIES",typeof(int));
			base.dtModel.Columns.Add("BUSS_FARM_PURSUITS",typeof(int));
			base.dtModel.Columns.Add("BUSS_FARM_PURSUITS_DESC",typeof(string));
			base.dtModel.Columns.Add("LOC_EXCLUDED",typeof(int));
			base.dtModel.Columns.Add("PERS_INJ_COV_82",typeof(int));
			base.dtModel.Columns.Add("OTHER_POLICY",typeof(string));


		}
		#endregion

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
		// model for database field CLIENT_LOCATION_NUMBER(string)
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


		public string CLIENT_LOCATION_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["CLIENT_LOCATION_NUMBER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CLIENT_LOCATION_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CLIENT_LOCATION_NUMBER"] = value;
			}
		}
		// model for database field LOCATION_ID(int)
		public int LOCATION_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["LOCATION_NUMBER"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LOCATION_NUMBER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LOCATION_NUMBER"] = value;
			}
		}
		// model for database field ADDRESS_1(string)
		public string ADDRESS_1
		{
			get
			{
				return base.dtModel.Rows[0]["ADDRESS_1"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ADDRESS_1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ADDRESS_1"] = value;
			}
		}
		// model for database field ADDRESS_2(string)
		public string ADDRESS_2
		{
			get
			{
				return base.dtModel.Rows[0]["ADDRESS_2"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ADDRESS_2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ADDRESS_2"] = value;
			}
		}
		// model for database field CITY(string)
		public string CITY
		{
			get
			{
				return base.dtModel.Rows[0]["CITY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CITY"] = value;
			}
		}
		// model for database field COUNTY(string)
		public string COUNTY
		{
			get
			{
				return base.dtModel.Rows[0]["COUNTY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["COUNTY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["COUNTY"] = value;
			}
		}
		// model for database field STATE(int)
		public int STATE
		{
			get
			{
				return base.dtModel.Rows[0]["STATE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["STATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["STATE"] = value;
			}
		}
		// model for database field ZIPCODE(string)
		public string ZIPCODE
		{
			get
			{
				return base.dtModel.Rows[0]["ZIPCODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ZIPCODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ZIPCODE"] = value;
			}
		}
		// model for database field PHONE_NUMBER(string)
		public string PHONE_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["PHONE_NUMBER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PHONE_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PHONE_NUMBER"] = value;
			}
		}
		// model for database field FAX_NUMBER(string)
		public string FAX_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["FAX_NUMBER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["FAX_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["FAX_NUMBER"] = value;
			}
		}

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
		// model for database field OCCUPIED_BY(int)
		public int OCCUPIED_BY
		{
			get
			{
				return base.dtModel.Rows[0]["OCCUPIED_BY"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["OCCUPIED_BY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["OCCUPIED_BY"] = value;
			}
		}
		// model for database field NUM_FAMILIES(int)
		public int NUM_FAMILIES
		{
			get
			{
				return base.dtModel.Rows[0]["NUM_FAMILIES"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["NUM_FAMILIES"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NUM_FAMILIES"] = value;
			}
		}
		// model for database field BUSS_FARM_PURSUITS(int)
		public int BUSS_FARM_PURSUITS
		{
			get
			{
				return base.dtModel.Rows[0]["BUSS_FARM_PURSUITS"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["BUSS_FARM_PURSUITS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["BUSS_FARM_PURSUITS"] = value;
			}
		}
		// model for database field BUSS_FARM_PURSUITS_DESC(string)
		public string BUSS_FARM_PURSUITS_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["BUSS_FARM_PURSUITS_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["BUSS_FARM_PURSUITS_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BUSS_FARM_PURSUITS_DESC"] = value;
			}
		}
		// model for database field LOC_EXCLUDED(int)
		public int LOC_EXCLUDED
		{
			get
			{
				return base.dtModel.Rows[0]["LOC_EXCLUDED"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LOC_EXCLUDED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LOC_EXCLUDED"] = value;
			}
		}
		// model for database field PERS_INJ_COV_82(int)
		public int PERS_INJ_COV_82
		{
			get
			{
				return base.dtModel.Rows[0]["PERS_INJ_COV_82"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["PERS_INJ_COV_82"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PERS_INJ_COV_82"] = value;
			}
		}		
		#endregion
		// model for database field OTHER_POLICY(string)
		public string OTHER_POLICY
		{
			get
			{
				return base.dtModel.Rows[0]["OTHER_POLICY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["OTHER_POLICY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["OTHER_POLICY"] = value;
			}
		}
	}
}
