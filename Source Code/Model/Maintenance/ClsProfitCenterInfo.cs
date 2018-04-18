/******************************************************************************************
<Author				: -   Priya
<Start Date				: -	5/9/2005 4:15:00 PM
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
namespace Cms.Model.Maintenance
{
	/// <summary>
	/// Database Model for MNT_PROFIT_CENTER_LIST.
	/// </summary>
	public class ClsProfitCenterInfo : Cms.Model.ClsCommonModel
	{
		private const string MNT_PROFIT_CENTER_LIST = "MNT_PROFIT_CENTER_LIST";
		public ClsProfitCenterInfo()
		{
			base.dtModel.TableName = "MNT_PROFIT_CENTER_LIST";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table MNT_PROFIT_CENTER_LIST
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("PC_ID",typeof(int));
			base.dtModel.Columns.Add("PC_CODE",typeof(string));
			base.dtModel.Columns.Add("PC_NAME",typeof(string));
			base.dtModel.Columns.Add("PC_ADD1",typeof(string));
			base.dtModel.Columns.Add("PC_ADD2",typeof(string));
			base.dtModel.Columns.Add("PC_CITY",typeof(string));
			base.dtModel.Columns.Add("PC_STATE",typeof(string));
			base.dtModel.Columns.Add("PC_ZIP",typeof(string));
			base.dtModel.Columns.Add("PC_COUNTRY",typeof(string));
			base.dtModel.Columns.Add("PC_PHONE",typeof(string));
			base.dtModel.Columns.Add("PC_EXT",typeof(string));
			base.dtModel.Columns.Add("PC_FAX",typeof(string));
			base.dtModel.Columns.Add("PC_EMAIL",typeof(string));
		}
		#region Database schema details
		// model for database field PC_ID(int)
		public int PC_ID
		{
			get
			{
				return base.dtModel.Rows[0]["PC_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["PC_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PC_ID"] = value;
			}
		}
		// model for database field PC_CODE(string)
		public string PC_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["PC_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PC_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PC_CODE"] = value;
			}
		}
		// model for database field PC_NAME(string)
		public string PC_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["PC_NAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PC_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PC_NAME"] = value;
			}
		}
		// model for database field PC_ADD1(string)
		public string PC_ADD1
		{
			get
			{
				return base.dtModel.Rows[0]["PC_ADD1"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PC_ADD1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PC_ADD1"] = value;
			}
		}
		// model for database field PC_ADD2(string)
		public string PC_ADD2
		{
			get
			{
				return base.dtModel.Rows[0]["PC_ADD2"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PC_ADD2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PC_ADD2"] = value;
			}
		}
		// model for database field PC_CITY(string)
		public string PC_CITY
		{
			get
			{
				return base.dtModel.Rows[0]["PC_CITY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PC_CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PC_CITY"] = value;
			}
		}
		// model for database field PC_STATE(string)
		public string PC_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["PC_STATE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PC_STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PC_STATE"] = value;
			}
		}
		// model for database field PC_ZIP(string)
		public string PC_ZIP
		{
			get
			{
				return base.dtModel.Rows[0]["PC_ZIP"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PC_ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PC_ZIP"] = value;
			}
		}
		// model for database field PC_COUNTRY(string)
		public string PC_COUNTRY
		{
			get
			{
				return base.dtModel.Rows[0]["PC_COUNTRY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PC_COUNTRY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PC_COUNTRY"] = value;
			}
		}
		// model for database field PC_PHONE(string)
		public string PC_PHONE
		{
			get
			{
				return base.dtModel.Rows[0]["PC_PHONE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PC_PHONE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PC_PHONE"] = value;
			}
		}
		// model for database field PC_EXT(string)
		public string PC_EXT
		{
			get
			{
				return base.dtModel.Rows[0]["PC_EXT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PC_EXT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PC_EXT"] = value;
			}
		}
		// model for database field PC_FAX(string)
		public string PC_FAX
		{
			get
			{
				return base.dtModel.Rows[0]["PC_FAX"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PC_FAX"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PC_FAX"] = value;
			}
		}
		// model for database field PC_EMAIL(string)
		public string PC_EMAIL
		{
			get
			{
				return base.dtModel.Rows[0]["PC_EMAIL"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PC_EMAIL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PC_EMAIL"] = value;
			}
		}
		#endregion
	}
}
