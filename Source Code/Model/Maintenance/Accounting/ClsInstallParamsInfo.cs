/******************************************************************************************
<Author					: -   Vijay Joshi
<Start Date				: -	  6/6/2005 3:29:04 PM
<End Date				: -	
<Description			: -   Model class for ACT_INSTALL_PARAMS Table
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
namespace Cms.Model.Maintenance.Accounting
{
	/// <summary>
	/// Database Model for ACT_INSTALL_PARAMS.
	/// </summary>
	public class ClsInstallParamsInfo : Cms.Model.ClsCommonModel
	{
		private const string ACT_INSTALL_PARAMS = "ACT_INSTALL_PARAMS";
		public ClsInstallParamsInfo()
		{
			base.dtModel.TableName = "ACT_INSTALL_PARAMS";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table ACT_INSTALL_PARAMS
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("INSTALL_DAYS_IN_ADVANCE",typeof(int));
			base.dtModel.Columns.Add("INSTALL_NOTIFY_ACCOUNTEXE",typeof(string));
			base.dtModel.Columns.Add("INSTALL_NOTIFY_CSR",typeof(string));
			base.dtModel.Columns.Add("INSTALL_NOTIFY_UNDERWRITER",typeof(string));
			base.dtModel.Columns.Add("INSTALL_NOTIFY_OTHER_USERS",typeof(string));
		}
		#region Database schema details
		// model for database field INSTALL_DAYS_IN_ADVANCE(int)
		public int INSTALL_DAYS_IN_ADVANCE
		{
			get
			{
				return base.dtModel.Rows[0]["INSTALL_DAYS_IN_ADVANCE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["INSTALL_DAYS_IN_ADVANCE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INSTALL_DAYS_IN_ADVANCE"] = value;
			}
		}
		// model for database field INSTALL_NOTIFY_ACCOUNTEXE(string)
		public string INSTALL_NOTIFY_ACCOUNTEXE
		{
			get
			{
				return base.dtModel.Rows[0]["INSTALL_NOTIFY_ACCOUNTEXE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["INSTALL_NOTIFY_ACCOUNTEXE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["INSTALL_NOTIFY_ACCOUNTEXE"] = value;
			}
		}
		// model for database field INSTALL_NOTIFY_CSR(string)
		public string INSTALL_NOTIFY_CSR
		{
			get
			{
				return base.dtModel.Rows[0]["INSTALL_NOTIFY_CSR"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["INSTALL_NOTIFY_CSR"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["INSTALL_NOTIFY_CSR"] = value;
			}
		}
		// model for database field INSTALL_NOTIFY_UNDERWRITER(string)
		public string INSTALL_NOTIFY_UNDERWRITER
		{
			get
			{
				return base.dtModel.Rows[0]["INSTALL_NOTIFY_UNDERWRITER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["INSTALL_NOTIFY_UNDERWRITER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["INSTALL_NOTIFY_UNDERWRITER"] = value;
			}
		}
		// model for database field INSTALL_NOTIFY_OTHER_USERS(string)
		public string INSTALL_NOTIFY_OTHER_USERS
		{
			get
			{
				return base.dtModel.Rows[0]["INSTALL_NOTIFY_OTHER_USERS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["INSTALL_NOTIFY_OTHER_USERS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["INSTALL_NOTIFY_OTHER_USERS"] = value;
			}
		}
		#endregion
	}
}
