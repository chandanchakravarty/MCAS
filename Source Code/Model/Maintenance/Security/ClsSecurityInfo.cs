using System;

namespace Cms.Model.Maintenance.Security
{
	/// <summary>
	/// Summary description for ClsSecurityInfo.
	/// </summary>
	public class ClsSecurityInfo:Cms.Model.ClsCommonModel
	{
		private const string MNT_USER_TYPE_PERMISSION = "MNT_USER_TYPE_PERMISSION";
		public ClsSecurityInfo()
		{
			base.dtModel.TableName = "MNT_USER_TYPE_PERMISSION";	// setting table name for data table that holds property values.
			this.AddColumns();										// add columns of the database table APP_HOME_OWNER_CHIMNEY_STOVE
			base.dtModel.Rows.Add(base.dtModel.NewRow());			// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("USER_ID",typeof(int));
			base.dtModel.Columns.Add("USER_TYPE_ID",typeof(int));
			base.dtModel.Columns.Add("SCREEN_ID",typeof(string));
			base.dtModel.Columns.Add("PERMISSION_XML",typeof(string));
		}
		#region Database schema details
		// model for database field USER_ID(int)
		public int USER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["USER_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["USER_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["USER_ID"] = value;
			}
		}
		// model for database field USER_TYPE_ID(int)
		public int USER_TYPE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["USER_TYPE_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["USER_TYPE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["USER_TYPE_ID"] = value;
			}
		}
		// model for database field SCREEN_ID(string)
		public string SCREEN_ID
		{
			get
			{
				return base.dtModel.Rows[0]["SCREEN_ID"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SCREEN_ID"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SCREEN_ID"] = value;
			}
		}
		// model for database field PERMISSION_XML(string)
		public string PERMISSION_XML
		{
			get
			{
				return base.dtModel.Rows[0]["PERMISSION_XML"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PERMISSION_XML"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PERMISSION_XML"] = value;
			}
		}
		#endregion
	}
}
