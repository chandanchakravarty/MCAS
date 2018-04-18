/******************************************************************************************
<Author					: -   Sumit Chhabra
<Start Date				: -		24/10/2005 02:21:13 PM
<End Date				: -	
<Description				: - 	model class for Umbrella Operator
<Review Date				: - 
<Reviewed By			: - 	
*******************************************************************************************/ 
using System;
using System.Data;
using Cms.Model;

namespace Cms.Model.Application
{
	/// <summary>
	/// Summary description for ClsUmbrellaOperatorInfo.
	/// </summary>
	public class ClsUmbrellaOperatorInfo : Cms.Model.ClsCommonModel
	{
		private const string APP_UMBRELLA_OPERATOR_INFO = "APP_UMBRELLA_OPERATOR_INFO";
		public ClsUmbrellaOperatorInfo()
		{
			base.dtModel.TableName = "APP_UMBRELLA_OPERATOR_INFO";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table APP_UMBRELLA_OPERATOR_INFO
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("APP_ID",typeof(int));
			base.dtModel.Columns.Add("APP_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("DRIVER_ID",typeof(int));			
			base.dtModel.Columns.Add("DRIVER_FNAME",typeof(string));
			base.dtModel.Columns.Add("DRIVER_MNAME",typeof(string));
			base.dtModel.Columns.Add("DRIVER_LNAME",typeof(string));
			base.dtModel.Columns.Add("DRIVER_CODE",typeof(string));
			base.dtModel.Columns.Add("DRIVER_SUFFIX",typeof(string));
			base.dtModel.Columns.Add("DRIVER_ADD1",typeof(string));
			base.dtModel.Columns.Add("DRIVER_ADD2",typeof(string));
			base.dtModel.Columns.Add("DRIVER_CITY",typeof(string));
			base.dtModel.Columns.Add("DRIVER_STATE",typeof(string));
			base.dtModel.Columns.Add("DRIVER_ZIP",typeof(string));
			base.dtModel.Columns.Add("DRIVER_COUNTRY",typeof(string));			
			base.dtModel.Columns.Add("DRIVER_DOB",typeof(DateTime));
			base.dtModel.Columns.Add("DRIVER_SSN",typeof(string));			
			base.dtModel.Columns.Add("DRIVER_SEX",typeof(string));
			base.dtModel.Columns.Add("DRIVER_DRIV_LIC",typeof(string));
			base.dtModel.Columns.Add("DRIVER_LIC_STATE",typeof(string));
			base.dtModel.Columns.Add("DRIVER_COST_GAURAD_AUX",typeof(int));			
			base.dtModel.Columns.Add("EXPERIENCE_CREDIT",typeof(double));
			base.dtModel.Columns.Add("VEHICLE_ID",typeof(string));
			base.dtModel.Columns.Add("PERCENT_DRIVEN",typeof(double));
			base.dtModel.Columns.Add("DATE_EXP_START",typeof(DateTime));
			base.dtModel.Columns.Add("APP_VEHICLE_PRIN_OCC_ID",typeof(int));

		}
		#region Database schema details
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

		public double PERCENT_DRIVEN
		{
			get
			{
				return base.dtModel.Rows[0]["PERCENT_DRIVEN"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["PERCENT_DRIVEN"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PERCENT_DRIVEN"] = value;
			}
		}


		public double EXPERIENCE_CREDIT
		{
			get
			{
				return base.dtModel.Rows[0]["EXPERIENCE_CREDIT"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["EXPERIENCE_CREDIT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EXPERIENCE_CREDIT"] = value;
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
		// model for database field DRIVER_TITLE(int)
		public int DRIVER_TITLE
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_TITLE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DRIVER_TITLE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_TITLE"] = value;
			}
		}
		// model for database field DRIVER_FNAME(string)
		public string DRIVER_FNAME
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_FNAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_FNAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_FNAME"] = value;
			}
		}
		// model for database field DRIVER_MNAME(string)
		public string DRIVER_MNAME
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_MNAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_MNAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_MNAME"] = value;
			}
		}
		// model for database field DRIVER_LNAME(string)
		public string DRIVER_LNAME
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_LNAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_LNAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_LNAME"] = value;
			}
		}
		// model for database field DRIVER_CODE(string)
		public string DRIVER_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_CODE"] = value;
			}
		}
		// model for database field DRIVER_SUFFIX(string)
		public string DRIVER_SUFFIX
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_SUFFIX"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_SUFFIX"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_SUFFIX"] = value;
			}
		}
		// model for database field DRIVER_ADD1(string)
		public string DRIVER_ADD1
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_ADD1"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_ADD1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_ADD1"] = value;
			}
		}
		// model for database field DRIVER_ADD2(string)
		public string DRIVER_ADD2
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_ADD2"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_ADD2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_ADD2"] = value;
			}
		}
		// model for database field DRIVER_CITY(string)
		public string DRIVER_CITY
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_CITY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_CITY"] = value;
			}
		}
		// model for database field DRIVER_STATE(string)
		public string DRIVER_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_STATE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_STATE"] = value;
			}
		}
		// model for database field DRIVER_ZIP(string)
		public string DRIVER_ZIP
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_ZIP"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_ZIP"] = value;
			}
		}
		// model for database field DRIVER_COUNTRY(string)
		public string DRIVER_COUNTRY
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_COUNTRY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_COUNTRY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_COUNTRY"] = value;
			}
		}
		// model for database field DRIVER_HOME_PHONE(string)
		public string DRIVER_HOME_PHONE
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_HOME_PHONE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_HOME_PHONE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_HOME_PHONE"] = value;
			}
		}
		// model for database field DRIVER_DOB(DateTime)
		public DateTime DRIVER_DOB
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_DOB"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["DRIVER_DOB"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_DOB"] = value;
			}
		}
		// model for database field DRIVER_SSN(string)
		public string DRIVER_SSN
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_SSN"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_SSN"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_SSN"] = value;
			}
		}
		// model for database field DRIVER_MART_STAT(string)
		public int DRIVER_MART_STAT
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_MART_STAT"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DRIVER_MART_STAT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_MART_STAT"] = value;
			}
		}
		// model for database field DRIVER_SEX(string)		
		public string DRIVER_SEX
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_SEX"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_SEX"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_SEX"] = value;
			}
		}
		// model for database field DRIVER_DRIV_LIC(string)
		public string DRIVER_DRIV_LIC
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_DRIV_LIC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_DRIV_LIC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_DRIV_LIC"] = value;
			}
		}
		// model for database field DRIVER_LIC_STATE(string)
		public string DRIVER_LIC_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_LIC_STATE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_LIC_STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_LIC_STATE"] = value;
			}
		}
		// model for database field DRIVER_COST_GAURAD_AUX(int)
		public int DRIVER_COST_GAURAD_AUX
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_COST_GAURAD_AUX"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DRIVER_COST_GAURAD_AUX"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_COST_GAURAD_AUX"] = value;
			}
		}
		// model for database field DATE_EXP_START(DateTime)
		public DateTime DATE_EXP_START
		{
			get
			{
				return base.dtModel.Rows[0]["DATE_EXP_START"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["DATE_EXP_START"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DATE_EXP_START"] = value;
			}
		}
		// model for database field APP_VEHICLE_PRIN_OCC_ID(int)
		public int APP_VEHICLE_PRIN_OCC_ID
		{
			get
			{
				return base.dtModel.Rows[0]["APP_VEHICLE_PRIN_OCC_ID"] == DBNull.Value ? -1 : Convert.ToInt32(base.dtModel.Rows[0]["APP_VEHICLE_PRIN_OCC_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APP_VEHICLE_PRIN_OCC_ID"] = value;
			}
		}
		#endregion
	}
}
