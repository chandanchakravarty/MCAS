/******************************************************************************************
<Author					: - Sumit Chhabra
<Start Date				: -	24/04/2006
<End Date				: -	
<Description				: - 	Models CLM_ADJUSTER_AUTHORITY
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
namespace Cms.Model.Maintenance.Claims
{
	/// <summary>
	/// Database Model for CLM_ADJUSTER_AUTHORITY.
	/// </summary>
	public class ClsClaimsAdjusterAuthorityInfo : Cms.Model.ClsCommonModel
	{
		private const string CLM_ADJUSTER_AUTHORITY = "CLM_ADJUSTER_AUTHORITY";
		public ClsClaimsAdjusterAuthorityInfo()
		{
			base.dtModel.TableName = "CLM_ADJUSTER_AUTHORITY";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table CLM_ADJUSTER_AUTHORITY
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{

			base.dtModel.Columns.Add("ADJUSTER_AUTHORITY_ID",typeof(int));
			base.dtModel.Columns.Add("ADJUSTER_ID",typeof(int));
			base.dtModel.Columns.Add("LOB_ID",typeof(int));			
			base.dtModel.Columns.Add("LIMIT_ID",typeof(int));
			base.dtModel.Columns.Add("EFFECTIVE_DATE",typeof(DateTime));			
			base.dtModel.Columns.Add("NOTIFY_AMOUNT",typeof(double));			
		}
		#region Database schema details
		// model for database field ADJUSTER_AUTHORITY_ID(int)
		public int ADJUSTER_AUTHORITY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ADJUSTER_AUTHORITY_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["ADJUSTER_AUTHORITY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ADJUSTER_AUTHORITY_ID"] = value;
			}
		}
		// model for database field ADJUSTER_AUTHORITY_ID(int)
		public int ADJUSTER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ADJUSTER_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["ADJUSTER_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ADJUSTER_ID"] = value;
			}
		}		
		// model for database field NOTIFY_AMOUNT(double)
		public double NOTIFY_AMOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["NOTIFY_AMOUNT"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["NOTIFY_AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NOTIFY_AMOUNT"] = value;
			}
		}
		// model for database field LOB_ID(int)
		public int LOB_ID
		{
			get
			{
				return base.dtModel.Rows[0]["LOB_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LOB_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LOB_ID"] = value;
			}
		}
		
		
		// model for database field LIMIT_ID(int)
		public int LIMIT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["LIMIT_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LIMIT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LIMIT_ID"] = value;
			}
		}	
		
		// model for database field EFFECTIVE_DATE(DateTime)
		public DateTime EFFECTIVE_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["EFFECTIVE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["EFFECTIVE_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EFFECTIVE_DATE"] = value;
			}
		}
		#endregion
	}
}
