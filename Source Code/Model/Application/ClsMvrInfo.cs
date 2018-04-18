/******************************************************************************************
<Author				: -   Sumit Chhabra
<Start Date				: -	9/27/2005 9:57:10 AM
<End Date				: -	
<Description				: - 	Model Class
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
	/// Database Model for APP_MVR_INFORMATION.
	/// </summary>
	public class ClsMvrInfo : Cms.Model.ClsCommonModel
	{
		private const string APP_MVR_INFORMATION = "APP_MVR_INFORMATION";
		public ClsMvrInfo()
		{
			base.dtModel.TableName = "APP_MVR_INFORMATION";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table APP_MVR_INFORMATION
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("APP_MVR_ID",typeof(int));
			base.dtModel.Columns.Add("APP_WATER_MVR_ID",typeof(int));
			base.dtModel.Columns.Add("APP_UMB_MVR_ID",typeof(int));
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("APP_ID",typeof(int));
			base.dtModel.Columns.Add("APP_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("VIOLATION_ID",typeof(int));
			base.dtModel.Columns.Add("VIOLATION_TYPE",typeof(int));
			base.dtModel.Columns.Add("VIOLATION_CODE",typeof(string));
			base.dtModel.Columns.Add("VIOLATION_DES",typeof(string));
			base.dtModel.Columns.Add("DRIVER_ID",typeof(int));
			
			base.dtModel.Columns.Add("MVR_AMOUNT",typeof(double));
			base.dtModel.Columns.Add("MVR_DEATH",typeof(string));
			base.dtModel.Columns.Add("MVR_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("DRIVER_CODE",typeof(string));
			base.dtModel.Columns.Add("DRIVER_NAME",typeof(string));
			base.dtModel.Columns.Add("DRIVER_REF",typeof(string));
			base.dtModel.Columns.Add("VERIFIED",typeof(int));
			base.dtModel.Columns.Add("OCCURENCE_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("DETAILS",typeof(string));
			base.dtModel.Columns.Add("POINTS_ASSIGNED",typeof(int));
			base.dtModel.Columns.Add("ADJUST_VIOLATION_POINTS",typeof(int));
			base.dtModel.Columns.Add("ENTERED_TLOG",typeof(string));
			base.dtModel.Columns.Add("ADJUST_VIOLATION_POINTS_TLOG",typeof(string));
			base.dtModel.Columns.Add("VIOLATION_TLOG",typeof(string));
			base.dtModel.Columns.Add("POINTS_ASSIGNED_TLOG",typeof(string));
		}
		#region Database schema details
		// model for database field APP_MVR_ID(int)
		public int APP_MVR_ID
		{
			get
			{
				return base.dtModel.Rows[0]["APP_MVR_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["APP_MVR_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APP_MVR_ID"] = value;
			}
		}
		// model for database field APP_WATER_MVR_ID(int)
		public int APP_WATER_MVR_ID
		{
			get
			{
				return base.dtModel.Rows[0]["APP_WATER_MVR_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["APP_WATER_MVR_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APP_WATER_MVR_ID"] = value;
			}
		}
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
		// model for database field APP_ID(int)
		public int APP_ID
		{
			get
			{
				return base.dtModel.Rows[0]["APP_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["APP_ID"].ToString());
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
				return base.dtModel.Rows[0]["APP_VERSION_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["APP_VERSION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APP_VERSION_ID"] = value;
			}
		}
		// model for database field VIOLATION_ID(int)
		public int VIOLATION_ID
		{
			get
			{
				return base.dtModel.Rows[0]["VIOLATION_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["VIOLATION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["VIOLATION_ID"] = value;
			}
		}
		// model for database field VIOLATION_ID(int)
		public int VIOLATION_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["VIOLATION_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["VIOLATION_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["VIOLATION_TYPE"] = value;
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
		// model for database field MVR_AMOUNT(double)
		public double MVR_AMOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["MVR_AMOUNT"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["MVR_AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MVR_AMOUNT"] = value;
			}
		}
		// model for database field MVR_DEATH(string)
		public string MVR_DEATH
		{
			get
			{
				return base.dtModel.Rows[0]["MVR_DEATH"] == DBNull.Value ? "" : base.dtModel.Rows[0]["MVR_DEATH"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MVR_DEATH"] = value;
			}
		}
		// model for database field MVR_DATE(DateTime)
		public DateTime MVR_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["MVR_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["MVR_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MVR_DATE"] = value;
			}
		}
		// model for database field DRIVER_CODE(string)
		public string DRIVER_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DRIVER_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_CODE"] = value;
			}
		}
		// model for database field DRIVER_NAME(string)
		public string DRIVER_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DRIVER_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_NAME"] = value;
			}
		}

		// model for database field DRIVER_NAME(string)
		public string DRIVER_REF
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_REF"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DRIVER_REF"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_REF"] = value;
			}
		}
		
		public string VIOLATION_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["VIOLATION_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["VIOLATION_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["VIOLATION_CODE"] = value;
			}
		}
		
		public string VIOLATION_DES
		{
			get
			{
				return base.dtModel.Rows[0]["VIOLATION_DES"] == DBNull.Value ? "" : base.dtModel.Rows[0]["VIOLATION_DES"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["VIOLATION_DES"] = value;
			}
		}

		
		// model for database field VERIFIED(int)
		public int VERIFIED
		{
			get
			{
				return base.dtModel.Rows[0]["VERIFIED"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["VERIFIED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["VERIFIED"] = value;
			}
		}		
		// model for database field APP_UMB_MVR_ID(int)
		public int APP_UMB_MVR_ID
		{
			get
			{
				return base.dtModel.Rows[0]["APP_UMB_MVR_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["APP_UMB_MVR_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APP_UMB_MVR_ID"] = value;
			}
		}
		public string DETAILS
		{
			get
			{
				return base.dtModel.Rows[0]["DETAILS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DETAILS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DETAILS"] = value;
			}
		}

		public DateTime OCCURENCE_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["OCCURENCE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["OCCURENCE_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["OCCURENCE_DATE"] = value;
			}
		}
		public int POINTS_ASSIGNED
		{
			get
			{
				return base.dtModel.Rows[0]["POINTS_ASSIGNED"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["POINTS_ASSIGNED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POINTS_ASSIGNED"] = value;
			}
		}
		public int ADJUST_VIOLATION_POINTS
		{
			get
			{
				return base.dtModel.Rows[0]["ADJUST_VIOLATION_POINTS"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ADJUST_VIOLATION_POINTS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ADJUST_VIOLATION_POINTS"] = value;
			}
		}
		// model for database field ENTERED_TLOG(string)
		public string ENTERED_TLOG
		{
			get
			{
				return base.dtModel.Rows[0]["ENTERED_TLOG"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ENTERED_TLOG"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ENTERED_TLOG"] = value;
			}
		}

		// model for database field ADJUST_VIOLATION_POINTS_TLOG(string)
		public string ADJUST_VIOLATION_POINTS_TLOG
		{
			get
			{
				return base.dtModel.Rows[0]["ADJUST_VIOLATION_POINTS_TLOG"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ADJUST_VIOLATION_POINTS_TLOG"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ADJUST_VIOLATION_POINTS_TLOG"] = value;
			}
		}

		// model for database field VIOLATION_TLOG(string)
		public string VIOLATION_TLOG
		{
			get
			{
				return base.dtModel.Rows[0]["VIOLATION_TLOG"] == DBNull.Value ? "" : base.dtModel.Rows[0]["VIOLATION_TLOG"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["VIOLATION_TLOG"] = value;
			}
		}
		// model for database field POINTS_ASSIGNED_TLOG(string)
		public string POINTS_ASSIGNED_TLOG
		{
			get
			{
				return base.dtModel.Rows[0]["POINTS_ASSIGNED_TLOG"] == DBNull.Value ? "" : base.dtModel.Rows[0]["POINTS_ASSIGNED_TLOG"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["POINTS_ASSIGNED_TLOG"] = value;
			}
		}
		#endregion
	}
}
