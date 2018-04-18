/******************************************************************************************
<Author					: -   Vijay Joshi
<Start Date				: -	4/15/2005 2:47:38 PM
<End Date				: -	
<Description			: - 	Creates model for Holder Interest (mortgage) table
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using System.Data;
using Cms.Model;
namespace Cms.Model.Maintenance
{
	/// <summary>
	/// Database Model for MNT_HOLDER_INTEREST_LIST.
	/// </summary>
	public class ClsHolderInfo : Cms.Model.ClsCommonModel
	{
		private const string MNT_HOLDER_INTEREST_LIST = "MNT_HOLDER_INTEREST_LIST";
		public ClsHolderInfo()
		{
			base.dtModel.TableName = "MNT_HOLDER_INTEREST_LIST";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table MNT_HOLDER_INTEREST_LIST
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("HOLDER_ID",typeof(int));
			base.dtModel.Columns.Add("HOLDER_NAME",typeof(string));
			base.dtModel.Columns.Add("HOLDER_CODE",typeof(string));
			base.dtModel.Columns.Add("HOLDER_ADD1",typeof(string));
			base.dtModel.Columns.Add("HOLDER_ADD2",typeof(string));
			base.dtModel.Columns.Add("HOLDER_CITY",typeof(string));
			base.dtModel.Columns.Add("HOLDER_COUNTRY",typeof(string));
			base.dtModel.Columns.Add("HOLDER_STATE",typeof(string));
			base.dtModel.Columns.Add("HOLDER_ZIP",typeof(string));
			base.dtModel.Columns.Add("HOLDER_MAIN_PHONE_NO",typeof(string));
			base.dtModel.Columns.Add("HOLDER_EXT",typeof(string));
			base.dtModel.Columns.Add("HOLDER_MOBILE",typeof(string));
			base.dtModel.Columns.Add("HOLDER_FAX",typeof(string));
			base.dtModel.Columns.Add("HOLDER_EMAIL",typeof(string));
			base.dtModel.Columns.Add("HOLDER_LEGAL_ENTITY",typeof(string));
			base.dtModel.Columns.Add("HOLDER_TYPE",typeof(string));
			base.dtModel.Columns.Add("HOLDER_MEMO",typeof(string));
		}
		#region Database schema details
		// model for database field HOLDER_ID(int)
		public int HOLDER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["HOLDER_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["HOLDER_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["HOLDER_ID"] = value;
			}
		}
		// model for database field HOLDER_NAME(string)
		public string HOLDER_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["HOLDER_NAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HOLDER_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HOLDER_NAME"] = value;
			}
		}
		// model for database field HOLDER_CODE(string)
		public string HOLDER_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["HOLDER_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HOLDER_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HOLDER_CODE"] = value;
			}
		}
		// model for database field HOLDER_ADD1(string)
		public string HOLDER_ADD1
		{
			get
			{
				return base.dtModel.Rows[0]["HOLDER_ADD1"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HOLDER_ADD1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HOLDER_ADD1"] = value;
			}
		}
		// model for database field HOLDER_ADD2(string)
		public string HOLDER_ADD2
		{
			get
			{
				return base.dtModel.Rows[0]["HOLDER_ADD2"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HOLDER_ADD2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HOLDER_ADD2"] = value;
			}
		}
		// model for database field HOLDER_CITY(string)
		public string HOLDER_CITY
		{
			get
			{
				return base.dtModel.Rows[0]["HOLDER_CITY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HOLDER_CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HOLDER_CITY"] = value;
			}
		}
		// model for database field HOLDER_COUNTRY(string)
		public string HOLDER_COUNTRY
		{
			get
			{
				return base.dtModel.Rows[0]["HOLDER_COUNTRY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HOLDER_COUNTRY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HOLDER_COUNTRY"] = value;
			}
		}
		// model for database field HOLDER_STATE(string)
		public string HOLDER_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["HOLDER_STATE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HOLDER_STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HOLDER_STATE"] = value;
			}
		}
		// model for database field HOLDER_ZIP(string)
		public string HOLDER_ZIP
		{
			get
			{
				return base.dtModel.Rows[0]["HOLDER_ZIP"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HOLDER_ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HOLDER_ZIP"] = value;
			}
		}
		// model for database field HOLDER_MAIN_PHONE_NO(string)
		public string HOLDER_MAIN_PHONE_NO
		{
			get
			{
				return base.dtModel.Rows[0]["HOLDER_MAIN_PHONE_NO"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HOLDER_MAIN_PHONE_NO"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HOLDER_MAIN_PHONE_NO"] = value;
			}
		}
		// model for database field HOLDER_EXT(string)
		public string HOLDER_EXT
		{
			get
			{
				return base.dtModel.Rows[0]["HOLDER_EXT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HOLDER_EXT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HOLDER_EXT"] = value;
			}
		}
		// model for database field HOLDER_MOBILE(string)
		public string HOLDER_MOBILE
		{
			get
			{
				return base.dtModel.Rows[0]["HOLDER_MOBILE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HOLDER_MOBILE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HOLDER_MOBILE"] = value;
			}
		}
		// model for database field HOLDER_FAX(string)
		public string HOLDER_FAX
		{
			get
			{
				return base.dtModel.Rows[0]["HOLDER_FAX"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HOLDER_FAX"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HOLDER_FAX"] = value;
			}
		}
		// model for database field HOLDER_EMAIL(string)
		public string HOLDER_EMAIL
		{
			get
			{
				return base.dtModel.Rows[0]["HOLDER_EMAIL"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HOLDER_EMAIL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HOLDER_EMAIL"] = value;
			}
		}
		// model for database field HOLDER_LEGAL_ENTITY(string)
		public string HOLDER_LEGAL_ENTITY
		{
			get
			{
				return base.dtModel.Rows[0]["HOLDER_LEGAL_ENTITY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HOLDER_LEGAL_ENTITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HOLDER_LEGAL_ENTITY"] = value;
			}
		}
		// model for database field HOLDER_TYPE(string)
		public string HOLDER_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["HOLDER_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HOLDER_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HOLDER_TYPE"] = value;
			}
		}
		// model for database field HOLDER_MEMO(string)
		public string HOLDER_MEMO
		{
			get
			{
				return base.dtModel.Rows[0]["HOLDER_MEMO"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HOLDER_MEMO"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HOLDER_MEMO"] = value;
			}
		}
		#endregion
	}
}
