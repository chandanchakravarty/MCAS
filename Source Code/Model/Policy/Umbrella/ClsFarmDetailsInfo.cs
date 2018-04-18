/******************************************************************************************
<Author					: -   Ravindra Gupta
<Start Date				: -	  03/20/2006
<End Date				: -	
<Description			: -   Models POL_UMBRELLA_FARM_INFO
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
	/// Summary description for ClsUmbrellaFarmInfo.
	/// </summary>
	public class ClsFarmDetailsInfo :Cms.Model.ClsCommonModel 
	{
		#region Constructor
		public ClsFarmDetailsInfo()
		{
			base.dtModel.TableName = "POL_UMBRELLA_FARM_INFO";
			AddColumns ();
			base.dtModel.Rows.Add(base.dtModel.NewRow());
			
		}
		#endregion 

		#region AddColumns Function
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("FARM_ID",typeof(int));
			base.dtModel.Columns.Add("ADDRESS_1",typeof(string));
			base.dtModel.Columns.Add("ADDRESS_2",typeof(string));
			base.dtModel.Columns.Add("CITY",typeof(string));
			base.dtModel.Columns.Add("COUNTY",typeof(string));
			base.dtModel.Columns.Add("STATE",typeof(int));
			base.dtModel.Columns.Add("ZIPCODE",typeof(string));
			base.dtModel.Columns.Add("PHONE_NUMBER",typeof(string));
			base.dtModel.Columns.Add("FAX_NUMBER",typeof(string));
			base.dtModel.Columns.Add("REMARKS",typeof(string));
			base.dtModel.Columns.Add("NO_OF_ACRES",typeof(int));
			base.dtModel.Columns.Add("OCCUPIED_BY_APPLICANT",typeof(string));
			base.dtModel.Columns.Add("RENTED_TO_OTHER",typeof(string));
			base.dtModel.Columns.Add("EMP_FULL_PART",typeof(string));
			base.dtModel.Columns.Add("LOCATION_NUMBER",typeof(int));
			
		}
		#endregion 

		#region Database schema details

		// model for database field CUSTOMER_ID(int)
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
		// model for database field FARM_ID(int)
		public int FARM_ID
		{
			get
			{
				return base.dtModel.Rows[0]["FARM_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["FARM_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["FARM_ID"] = value;
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

		// model for database field REMARKS(string)
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

		// model for database field NO_OF_ACRES(int)
		public int NO_OF_ACRES
		{
			get
			{
				return base.dtModel.Rows[0]["NO_OF_ACRES"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["NO_OF_ACRES"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NO_OF_ACRES"] = value;
			}
		}

		// model for database field OCCUPIED_BY_APPLICANT(string)
		public string OCCUPIED_BY_APPLICANT 
		{
			get
			{
				return base.dtModel.Rows[0]["OCCUPIED_BY_APPLICANT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["OCCUPIED_BY_APPLICANT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["OCCUPIED_BY_APPLICANT"] = value;
			}
		}

		// model for database field RENTED_TO_OTHER(string)
		public string RENTED_TO_OTHER
		{
			get
			{
				return base.dtModel.Rows[0]["RENTED_TO_OTHER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["RENTED_TO_OTHER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["RENTED_TO_OTHER"] = value;
			}
		}

		// model for database field EMP_FULL_PART(string)
		public string EMP_FULL_PART
		{
			get
			{
				return base.dtModel.Rows[0]["EMP_FULL_PART"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["EMP_FULL_PART"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EMP_FULL_PART"] = value;
			}
		}

		#endregion
	}
}
