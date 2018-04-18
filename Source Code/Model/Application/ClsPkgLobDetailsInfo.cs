/******************************************************************************************
<Author				: -   Ajit Singh Chahal
<Start Date				: -	4/28/2005 8:31:40 PM
<End Date				: -	
<Description				: - 	Model for PkgLobDetails
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
	/// Database Model for APP_PKG_LOB_DETAILS.
	/// </summary>
	public class ClsPkgLobDetailsInfo : Cms.Model.ClsCommonModel
	{
		private const string APP_PKG_LOB_DETAILS = "APP_PKG_LOB_DETAILS";
		public ClsPkgLobDetailsInfo()
		{
			base.dtModel.TableName = "APP_PKG_LOB_DETAILS";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table APP_PKG_LOB_DETAILS
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("REC_ID",typeof(int));
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("APP_ID",typeof(int));
			base.dtModel.Columns.Add("APP_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("LOB",typeof(string));
			base.dtModel.Columns.Add("SUB_LOB",typeof(string));
		}
		#region Database schema details
		// model for database field REC_ID(int)
		public int REC_ID
		{
			get
			{
				return base.dtModel.Rows[0]["REC_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["REC_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REC_ID"] = value;
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
		// model for database field LOB(string)
		public string LOB
		{
			get
			{
				return base.dtModel.Rows[0]["LOB"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LOB"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOB"] = value;
			}
		}
		// model for database field SUB_LOB(string)
		public string SUB_LOB
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_LOB"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SUB_LOB"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SUB_LOB"] = value;
			}
		}
		#endregion
	}
}
