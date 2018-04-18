/******************************************************************************************
<Author				: -   Priya
<Start Date				: -	6/24/2005 12:30:10 PM
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
namespace  Cms.Model.Maintenance
{
	/// <summary>
	/// Database Model for MNT_LOB_MASTER.
	/// </summary>
	public class ClsPolicyNoSetupInfo : Cms.Model.ClsCommonModel
	{
		private const string MNT_LOB_MASTER = "MNT_LOB_MASTER";
		public ClsPolicyNoSetupInfo()
		{
			base.dtModel.TableName = "MNT_LOB_MASTER";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table MNT_LOB_MASTER
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("LOB_ID",typeof(int));
			base.dtModel.Columns.Add("LOB_PREFIX",typeof(string));
			base.dtModel.Columns.Add("LOB_SUFFIX",typeof(string));
			base.dtModel.Columns.Add("LOB_SEED",typeof(int));
		}
		#region Database schema details
		// model for database field LOB_ID(int)
		public int LOB_ID
		{
			get
			{
				return base.dtModel.Rows[0]["LOB_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LOB_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LOB_ID"] = value;
			}
		}
		// model for database field LOB_PREFIX(string)
		public string LOB_PREFIX
		{
			get
			{
				return base.dtModel.Rows[0]["LOB_PREFIX"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LOB_PREFIX"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOB_PREFIX"] = value;
			}
		}
		// model for database field LOB_SUFFIX(string)
		public string LOB_SUFFIX
		{
			get
			{
				return base.dtModel.Rows[0]["LOB_SUFFIX"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LOB_SUFFIX"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOB_SUFFIX"] = value;
			}
		}
		// model for database field LOB_SEED(int)
		public int LOB_SEED
		{
			get
			{
				return base.dtModel.Rows[0]["LOB_SEED"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LOB_SEED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LOB_SEED"] = value;
			}
		}
		#endregion
	}
}
