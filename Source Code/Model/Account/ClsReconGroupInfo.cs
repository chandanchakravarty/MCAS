/******************************************************************************************
<Author				: -   Vijay Joshi
<Start Date				: -	6/29/2005 12:28:51 PM
<End Date				: -	
<Description				: - 	Model class for ACT_RECONCIALTION_GROUP table
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
namespace Cms.Model.Account
{
	/// <summary>
	/// Database Model for ACT_RECONCILIATION_GROUPS.
	/// </summary>
	public class ClsReconGroupInfo : Cms.Model.ClsCommonModel
	{
		private const string ACT_RECONCILIATION_GROUPS = "ACT_RECONCILIATION_GROUPS";
		public ClsReconGroupInfo ()
		{
			base.dtModel.TableName = "ACT_RECONCILIATION_GROUPS";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table ACT_RECONCILIATION_GROUPS
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("GROUP_ID",typeof(int));
			base.dtModel.Columns.Add("RECON_ENTITY_ID",typeof(int));
			base.dtModel.Columns.Add("RECON_ENTITY_TYPE",typeof(string));
			base.dtModel.Columns.Add("IS_COMMITTED",typeof(string));
			base.dtModel.Columns.Add("DATE_COMMITTED",typeof(DateTime));
			base.dtModel.Columns.Add("COMMITTED_BY",typeof(int));
			base.dtModel.Columns.Add("CD_LINE_ITEM_ID",typeof(int));
		}
		#region Database schema details
		// model for database field CD_LINE_ITEM_ID(int)
		public int CD_LINE_ITEM_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CD_LINE_ITEM_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CD_LINE_ITEM_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CD_LINE_ITEM_ID"] = value;
			}
		}
		
		// model for database field GROUP_ID(int)
		public int GROUP_ID
		{
			get
			{
				return base.dtModel.Rows[0]["GROUP_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["GROUP_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["GROUP_ID"] = value;
			}
		}
		// model for database field RECON_ENTITY_ID(int)
		public int RECON_ENTITY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["RECON_ENTITY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["RECON_ENTITY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RECON_ENTITY_ID"] = value;
			}
		}
		// model for database field RECON_ENTITY_TYPE(string)
		public string RECON_ENTITY_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["RECON_ENTITY_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["RECON_ENTITY_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["RECON_ENTITY_TYPE"] = value;
			}
		}
		// model for database field IS_COMMITTED(string)
		public string IS_COMMITTED
		{
			get
			{
				return base.dtModel.Rows[0]["IS_COMMITTED"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IS_COMMITTED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_COMMITTED"] = value;
			}
		}
		// model for database field DATE_COMMITTED(DateTime)
		public DateTime DATE_COMMITTED
		{
			get
			{
				return base.dtModel.Rows[0]["DATE_COMMITTED"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["DATE_COMMITTED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DATE_COMMITTED"] = value;
			}
		}
		// model for database field COMMITTED_BY(int)
		public int COMMITTED_BY
		{
			get
			{
				return base.dtModel.Rows[0]["COMMITTED_BY"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["COMMITTED_BY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COMMITTED_BY"] = value;
			}
		}
		#endregion
	}
}
