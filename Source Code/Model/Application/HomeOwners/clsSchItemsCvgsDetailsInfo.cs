using System;
using System.Data;
using Cms.Model;
using Cms.BusinessLayer.BlCommon;

namespace Cms.Model.Application

{
	/// <summary>
	/// Database Model for APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS.
	/// </summary>
	public class clsSchItemsCvgsDetailsInfo: Cms.Model.ClsCommonModel
	{
		private const string APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS = "APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS";
		public clsSchItemsCvgsDetailsInfo()
		{
			base.dtModel.TableName = "APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("APP_ID",typeof(int));
			base.dtModel.Columns.Add("APP_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("ITEM_ID",typeof(int));
			base.dtModel.Columns.Add("ITEM_DETAIL_ID",typeof(int));
			base.dtModel.Columns.Add("ITEM_NUMBER",typeof(string));
			base.dtModel.Columns.Add("ITEM_DESCRIPTION",typeof(string));
			base.dtModel.Columns.Add("ITEM_SERIAL_NUMBER",typeof(string));
			base.dtModel.Columns.Add("ITEM_INSURING_VALUE",typeof(double));
			base.dtModel.Columns.Add("ITEM_APPRAISAL_BILL",typeof(string));
			base.dtModel.Columns.Add("ITEM_PICTURE_ATTACHED",typeof(string));
		}
		#region Database schema details
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
		// model for database field ITEM_ID(int)
		public int ITEM_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ITEM_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ITEM_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ITEM_ID"] = value;
			}
		}
		// model for database field ITEM_DETAIL_ID(int)
		public int ITEM_DETAIL_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ITEM_DETAIL_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ITEM_DETAIL_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ITEM_DETAIL_ID"] = value;
			}
		}
		// model for database field ITEM_NUMBER(string)
		public string ITEM_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["ITEM_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ITEM_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ITEM_NUMBER"] = value;
			}
		}
		// model for database field ITEM_DESCRIPTION(string)
		public string ITEM_DESCRIPTION
		{
			get
			{
				return base.dtModel.Rows[0]["ITEM_DESCRIPTION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ITEM_DESCRIPTION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ITEM_DESCRIPTION"] = value;
			}
		}
		// model for database field ITEM_SERIAL_NUMBER(string)
		public string ITEM_SERIAL_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["ITEM_SERIAL_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ITEM_SERIAL_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ITEM_SERIAL_NUMBER"] = value;
			}
		}
		// model for database field ITEM_INSURING_VALUE(double)
		public double ITEM_INSURING_VALUE
		{
			get
			{
				return base.dtModel.Rows[0]["ITEM_INSURING_VALUE"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["ITEM_INSURING_VALUE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ITEM_INSURING_VALUE"] = value;
			}
		}
		// model for database field ITEM_APPRAISAL_BILL(string)
		public string ITEM_APPRAISAL_BILL
		{
			get
			{
				return base.dtModel.Rows[0]["ITEM_APPRAISAL_BILL"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ITEM_APPRAISAL_BILL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ITEM_APPRAISAL_BILL"] = value;
			}
		}
		// model for database field ITEM_PICTURE_ATTACHED(string)
		public string ITEM_PICTURE_ATTACHED
		{
			get
			{
				return base.dtModel.Rows[0]["ITEM_PICTURE_ATTACHED"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ITEM_PICTURE_ATTACHED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ITEM_PICTURE_ATTACHED"] = value;
			}
		}
		#endregion
	}
}
