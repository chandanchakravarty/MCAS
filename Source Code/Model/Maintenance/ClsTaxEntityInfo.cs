/******************************************************************************************
<Author				: -   Priya Arora
<Start Date				: -	4/14/2005 4:43:17 PM
<End Date				: -	
<Description		    : -Model For Tax entity.
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
	/// Database Model for MNT_TAX_ENTITY_LIST.
	/// </summary>
	public class ClsTaxEntityInfo : Cms.Model.ClsCommonModel
	{
		private const string MNT_TAX_ENTITY_LIST = "MNT_TAX_ENTITY_LIST";
		public ClsTaxEntityInfo()
		{
			base.dtModel.TableName = "MNT_TAX_ENTITY_LIST";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table MNT_TAX_ENTITY_LIST
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("TAX_ID",typeof(int));
			base.dtModel.Columns.Add("TAX_NAME",typeof(string));
			base.dtModel.Columns.Add("TAX_CODE",typeof(string));
			base.dtModel.Columns.Add("TAX_ADDRESS1",typeof(string));
			base.dtModel.Columns.Add("TAX_ADDRESS2",typeof(string));
			base.dtModel.Columns.Add("TAX_CITY",typeof(string));
			base.dtModel.Columns.Add("TAX_COUNTRY",typeof(string));
			base.dtModel.Columns.Add("TAX_STATE",typeof(string));
			base.dtModel.Columns.Add("TAX_ZIP",typeof(string));
			base.dtModel.Columns.Add("TAX_PHONE",typeof(string));
			base.dtModel.Columns.Add("TAX_EXT",typeof(string));
			base.dtModel.Columns.Add("TAX_FAX",typeof(string));
			base.dtModel.Columns.Add("TAX_EMAIL",typeof(string));
			base.dtModel.Columns.Add("TAX_WEBSITE",typeof(string));
		}
		#region Database schema details
		// model for database field TAX_ID(int)
		public int TAX_ID
		{
			get
			{
				return base.dtModel.Rows[0]["TAX_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["TAX_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["TAX_ID"] = value;
			}
		}
		// model for database field TAX_NAME(string)
		public string TAX_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["TAX_NAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["TAX_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TAX_NAME"] = value;
			}
		}
		// model for database field TAX_CODE(string)
		public string TAX_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["TAX_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["TAX_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TAX_CODE"] = value;
			}
		}
		// model for database field TAX_ADDRESS1(string)
		public string TAX_ADDRESS1
		{
			get
			{
				return base.dtModel.Rows[0]["TAX_ADDRESS1"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["TAX_ADDRESS1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TAX_ADDRESS1"] = value;
			}
		}
		// model for database field TAX_ADDRESS2(string)
		public string TAX_ADDRESS2
		{
			get
			{
				return base.dtModel.Rows[0]["TAX_ADDRESS2"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["TAX_ADDRESS2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TAX_ADDRESS2"] = value;
			}
		}
		// model for database field TAX_CITY(string)
		public string TAX_CITY
		{
			get
			{
				return base.dtModel.Rows[0]["TAX_CITY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["TAX_CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TAX_CITY"] = value;
			}
		}
		// model for database field TAX_COUNTRY(string)
		public string TAX_COUNTRY
		{
			get
			{
				return base.dtModel.Rows[0]["TAX_COUNTRY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["TAX_COUNTRY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TAX_COUNTRY"] = value;
			}
		}
		// model for database field TAX_STATE(string)
		public string TAX_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["TAX_STATE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["TAX_STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TAX_STATE"] = value;
			}
		}
		// model for database field TAX_ZIP(string)
		public string TAX_ZIP
		{
			get
			{
				return base.dtModel.Rows[0]["TAX_ZIP"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["TAX_ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TAX_ZIP"] = value;
			}
		}
		// model for database field TAX_PHONE(string)
		public string TAX_PHONE
		{
			get
			{
				return base.dtModel.Rows[0]["TAX_PHONE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["TAX_PHONE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TAX_PHONE"] = value;
			}
		}
		// model for database field TAX_EXT(string)
		public string TAX_EXT
		{
			get
			{
				return base.dtModel.Rows[0]["TAX_EXT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["TAX_EXT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TAX_EXT"] = value;
			}
		}
		// model for database field TAX_FAX(string)
		public string TAX_FAX
		{
			get
			{
				return base.dtModel.Rows[0]["TAX_FAX"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["TAX_FAX"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TAX_FAX"] = value;
			}
		}
		// model for database field TAX_EMAIL(string)
		public string TAX_EMAIL
		{
			get
			{
				return base.dtModel.Rows[0]["TAX_EMAIL"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["TAX_EMAIL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TAX_EMAIL"] = value;
			}
		}
		// model for database field TAX_WEBSITE(string)
		public string TAX_WEBSITE
		{
			get
			{
				return base.dtModel.Rows[0]["TAX_WEBSITE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["TAX_WEBSITE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TAX_WEBSITE"] = value;
			}
		}
		#endregion
	}
}
