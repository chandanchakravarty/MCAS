/******************************************************************************************
<Author					: -		Vijay Arora
<Start Date				: -		3/22/2006 9:28:28 AM
<End Date				: -	
<Description			: - 	
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
	/// Database Model for MNT_REQUEST_RESPONSE_LOG.
	/// </summary>
	public class ClsRequestResponseLogInfo : Cms.Model.ClsCommonModel
	{
		private const string MNT_REQUEST_RESPONSE_LOG = "MNT_REQUEST_RESPONSE_LOG";
		public ClsRequestResponseLogInfo()
		{
			base.dtModel.TableName = "MNT_REQUEST_RESPONSE_LOG";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table MNT_REQUEST_RESPONSE_LOG
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("ROW_ID",typeof(int));
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("CATEGORY_ID",typeof(int));
			base.dtModel.Columns.Add("SERVICE_VENDOR",typeof(string));
			base.dtModel.Columns.Add("REQUEST_DATETIME",typeof(DateTime));
			base.dtModel.Columns.Add("RESPONSE_DATETIME",typeof(DateTime));
			base.dtModel.Columns.Add("IIX_REQUEST",typeof(string));
			base.dtModel.Columns.Add("IIX_RESPONSE",typeof(string));
		}
		#region Database schema details
		// model for database field ROW_ID(int)
		public int ROW_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ROW_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ROW_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ROW_ID"] = value;
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
		// model for database field POLICY_ID(int)
		public int POLICY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["POLICY_ID"].ToString());
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
				return base.dtModel.Rows[0]["POLICY_VERSION_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["POLICY_VERSION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_VERSION_ID"] = value;
			}
		}
		// model for database field CATEGORY_ID(int)
		public int CATEGORY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CATEGORY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CATEGORY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CATEGORY_ID"] = value;
			}
		}
		// model for database field SERVICE_VENDOR(string)
		public string SERVICE_VENDOR
		{
			get
			{
				return base.dtModel.Rows[0]["SERVICE_VENDOR"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SERVICE_VENDOR"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SERVICE_VENDOR"] = value;
			}
		}
		// model for database field REQUEST_DATETIME(DateTime)
		public DateTime REQUEST_DATETIME
		{
			get
			{
				return base.dtModel.Rows[0]["REQUEST_DATETIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["REQUEST_DATETIME"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REQUEST_DATETIME"] = value;
			}
		}
		// model for database field RESPONSE_DATETIME(DateTime)
		public DateTime RESPONSE_DATETIME
		{
			get
			{
				return base.dtModel.Rows[0]["RESPONSE_DATETIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["RESPONSE_DATETIME"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RESPONSE_DATETIME"] = value;
			}
		}
		// model for database field IIX_REQUEST(string)
		public string IIX_REQUEST
		{
			get
			{
				return base.dtModel.Rows[0]["IIX_REQUEST"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IIX_REQUEST"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IIX_REQUEST"] = value;
			}
		}
		// model for database field IIX_RESPONSE(string)
		public string IIX_RESPONSE
		{
			get
			{
				return base.dtModel.Rows[0]["IIX_RESPONSE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IIX_RESPONSE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IIX_RESPONSE"] = value;
			}
		}
		#endregion
	}
}
