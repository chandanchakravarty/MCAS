/******************************************************************************************
<Author				: -   Shrikant Bhatt
<Start Date				: -	4/28/2005 5:42:32 PM
<End Date				: -	
<Description				: - 	
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: -  12/05/2005
<Modified By			: -  Nidhi
<Purpose				: - Added 2 more columns.(SPECIAL_WORD_FRONT & SPECIAL_WORD_BACK)
*******************************************************************************************/ 
using System;
using System.Data;
using Cms.Model;
namespace Cms.Model.Application
{
	/// <summary>
	/// Database Model for APP_AUTO_ID_CARD_INFO.
	/// </summary>
	public class ClsAutoIDInfo : Cms.Model.ClsCommonModel
	{
		private const string APP_AUTO_ID_CARD_INFO = "APP_AUTO_ID_CARD_INFO";
		public ClsAutoIDInfo()
		{
			base.dtModel.TableName = "APP_AUTO_ID_CARD_INFO";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table APP_AUTO_ID_CARD_INFO
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("AUTO_CARD_ID",typeof(int));
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("APP_ID",typeof(int));
			base.dtModel.Columns.Add("APP_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("VEHICLE_ID",typeof(int));
			base.dtModel.Columns.Add("ID_EFFECTIVE_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("ID_EXPITATION_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("NAME_TYPE",typeof(string));
			base.dtModel.Columns.Add("NAME_ID",typeof(int));
			base.dtModel.Columns.Add("A_NAME",typeof(string));
			base.dtModel.Columns.Add("ADDRESS1",typeof(string));
			base.dtModel.Columns.Add("ADDRESS2",typeof(string));
			base.dtModel.Columns.Add("CITY",typeof(string));
			base.dtModel.Columns.Add("STATE",typeof(string));
			base.dtModel.Columns.Add("ZIP",typeof(string));
			base.dtModel.Columns.Add("EMAIL",typeof(string));
			base.dtModel.Columns.Add("NAME_PRINT",typeof(string));
			base.dtModel.Columns.Add("STATE_TYPE",typeof(string));
			base.dtModel.Columns.Add("SPECIAL_WORD_FRONT",typeof(string));
			base.dtModel.Columns.Add("SPECIAL_WORD_BACK",typeof(string));
			base.dtModel.Columns.Add("IS_UPDATED",typeof(int));
		}
		#region Database schema details
		// model for database field AUTO_CARD_ID(int)
		public int AUTO_CARD_ID
		{
			get
			{
				return base.dtModel.Rows[0]["AUTO_CARD_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["AUTO_CARD_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AUTO_CARD_ID"] = value;
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
		// model for database field ID_EFFECTIVE_DATE(DateTime)
		public DateTime ID_EFFECTIVE_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["ID_EFFECTIVE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["ID_EFFECTIVE_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ID_EFFECTIVE_DATE"] = value;
			}
		}
		// model for database field ID_EXPITATION_DATE(DateTime)
		public DateTime ID_EXPITATION_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["ID_EXPITATION_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["ID_EXPITATION_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ID_EXPITATION_DATE"] = value;
			}
		}
		// model for database field NAME_TYPE(string)
		public string NAME_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["NAME_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["NAME_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["NAME_TYPE"] = value;
			}
		}
		// model for database field NAME_ID(int)
		public int NAME_ID
		{
			get
			{
				return base.dtModel.Rows[0]["NAME_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["NAME_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NAME_ID"] = value;
			}
		}
		// model for database field NAME(string)
		public string NAME
		{
			get
			{
				return base.dtModel.Rows[0]["A_NAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["A_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["A_NAME"] = value;
			}
		}
		// model for database field ADDRESS1(string)
		public string ADDRESS1
		{
			get
			{
				return base.dtModel.Rows[0]["ADDRESS1"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ADDRESS1"].ToString();
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
				return base.dtModel.Rows[0]["ADDRESS2"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ADDRESS2"].ToString();
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
				return base.dtModel.Rows[0]["CITY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CITY"] = value;
			}
		}
		// model for database field STATE(string)
		public string STATE
		{
			get
			{
				return base.dtModel.Rows[0]["STATE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["STATE"].ToString();
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
				return base.dtModel.Rows[0]["ZIP"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ZIP"] = value;
			}
		}
		// model for database field EMAIL(string)
		public string EMAIL
		{
			get
			{
				return base.dtModel.Rows[0]["EMAIL"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["EMAIL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EMAIL"] = value;
			}
		}
		// model for database field NAME_PRINT(string)
		public string NAME_PRINT
		{
			get
			{
				return base.dtModel.Rows[0]["NAME_PRINT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["NAME_PRINT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["NAME_PRINT"] = value;
			}
		}
		// model for database field STATE_TYPE(string)
		public string STATE_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["STATE_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["STATE_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["STATE_TYPE"] = value;
			}
		}

		// model for database field SPEACIAL_WORD_FRONT(string)
		public string SPECIAL_WORD_FRONT
		{
			get
			{
				return base.dtModel.Rows[0]["SPECIAL_WORD_FRONT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SPECIAL_WORD_FRONT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SPECIAL_WORD_FRONT"] = value;
			}
		}
		// model for database field SPEACIAL_WORD_BACK(string)
		public string SPECIAL_WORD_BACK
		{
			get
			{
				return base.dtModel.Rows[0]["SPECIAL_WORD_BACK"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SPECIAL_WORD_BACK"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SPECIAL_WORD_BACK"] = value;
			}
		}
		// model for database field IS_UPDATED(bit)
		public int IS_UPDATED
		{
			get
			{
				return base.dtModel.Rows[0]["IS_UPDATED"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["IS_UPDATED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["IS_UPDATED"] = value;
			}
		}
		#endregion
	}
}
