/******************************************************************************************
<Author				: -   Ashwani
<Start Date				: -	5/9/2005 1:58:10 PM
<End Date				: -	
<Description				: - 	Model class for Department table.
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
	/// Database Model for MNT_DEPT_LIST.
	/// </summary>
	public class ClsDepartmentInfo : Cms.Model.ClsCommonModel
	{
		private const string MNT_DEPT_LIST = "MNT_DEPT_LIST";
		public ClsDepartmentInfo()
		{
			base.dtModel.TableName = "MNT_DEPT_LIST";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table MNT_DEPT_LIST
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("DEPT_ID",typeof(int));
			base.dtModel.Columns.Add("DEPT_CODE",typeof(string));
			base.dtModel.Columns.Add("DEPT_NAME",typeof(string));
			base.dtModel.Columns.Add("DEPT_ADD1",typeof(string));
			base.dtModel.Columns.Add("DEPT_ADD2",typeof(string));
			base.dtModel.Columns.Add("DEPT_CITY",typeof(string));
			base.dtModel.Columns.Add("DEPT_STATE",typeof(string));
			base.dtModel.Columns.Add("DEPT_ZIP",typeof(string));
			base.dtModel.Columns.Add("DEPT_COUNTRY",typeof(string));
			base.dtModel.Columns.Add("DEPT_PHONE",typeof(string));
			base.dtModel.Columns.Add("DEPT_EXT",typeof(string));
			base.dtModel.Columns.Add("DEPT_FAX",typeof(string));
			base.dtModel.Columns.Add("DEPT_EMAIL",typeof(string));
		}
		#region Database schema details
		// model for database field DEPT_ID(int)
		public int DEPT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["DEPT_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DEPT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DEPT_ID"] = value;
			}
		}
		// model for database field DEPT_CODE(string)
		public string DEPT_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["DEPT_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DEPT_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DEPT_CODE"] = value;
			}
		}
		// model for database field DEPT_NAME(string)
		public string DEPT_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["DEPT_NAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DEPT_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DEPT_NAME"] = value;
			}
		}
		// model for database field DEPT_ADD1(string)
		public string DEPT_ADD1
		{
			get
			{
				return base.dtModel.Rows[0]["DEPT_ADD1"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DEPT_ADD1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DEPT_ADD1"] = value;
			}
		}
		// model for database field DEPT_ADD2(string)
		public string DEPT_ADD2
		{
			get
			{
				return base.dtModel.Rows[0]["DEPT_ADD2"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DEPT_ADD2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DEPT_ADD2"] = value;
			}
		}
		// model for database field DEPT_CITY(string)
		public string DEPT_CITY
		{
			get
			{
				return base.dtModel.Rows[0]["DEPT_CITY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DEPT_CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DEPT_CITY"] = value;
			}
		}
		// model for database field DEPT_STATE(string)
		public string DEPT_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["DEPT_STATE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DEPT_STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DEPT_STATE"] = value;
			}
		}
		// model for database field DEPT_ZIP(string)
		public string DEPT_ZIP
		{
			get
			{
				return base.dtModel.Rows[0]["DEPT_ZIP"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DEPT_ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DEPT_ZIP"] = value;
			}
		}
		// model for database field DEPT_COUNTRY(string)
		public string DEPT_COUNTRY
		{
			get
			{
				return base.dtModel.Rows[0]["DEPT_COUNTRY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DEPT_COUNTRY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DEPT_COUNTRY"] = value;
			}
		}
		// model for database field DEPT_PHONE(string)
		public string DEPT_PHONE
		{
			get
			{
				return base.dtModel.Rows[0]["DEPT_PHONE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DEPT_PHONE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DEPT_PHONE"] = value;
			}
		}
		// model for database field DEPT_EXT(string)
		public string DEPT_EXT
		{
			get
			{
				return base.dtModel.Rows[0]["DEPT_EXT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DEPT_EXT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DEPT_EXT"] = value;
			}
		}
		// model for database field DEPT_FAX(string)
		public string DEPT_FAX
		{
			get
			{
				return base.dtModel.Rows[0]["DEPT_FAX"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DEPT_FAX"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DEPT_FAX"] = value;
			}
		}
		// model for database field DEPT_EMAIL(string)
		public string DEPT_EMAIL
		{
			get
			{
				return base.dtModel.Rows[0]["DEPT_EMAIL"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DEPT_EMAIL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DEPT_EMAIL"] = value;
			}
		}
		#endregion
	}
}
