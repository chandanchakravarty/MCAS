/******************************************************************************************
<Author				: -   Vijay Arora
<Start Date				: -	6/19/2006 3:28:32 PM
<End Date				: -	
<Description				: - 	Model Class for Re-open Claim
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
namespace Cms
{
	/// <summary>
	/// Database Model for CLM_REOPEN_CLAIM.
	/// </summary>
	public class ClsReopenClaimInfo : Cms.Model.ClsCommonModel
	{
		private const string CLM_REOPEN_CLAIM = "CLM_REOPEN_CLAIM";
		public ClsReopenClaimInfo()
		{
			base.dtModel.TableName = "CLM_REOPEN_CLAIM";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table CLM_REOPEN_CLAIM
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CLAIM_ID",typeof(int));
			base.dtModel.Columns.Add("REOPEN_ID",typeof(int));
			base.dtModel.Columns.Add("REOPEN_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("REOPEN_BY",typeof(int));
			base.dtModel.Columns.Add("REASON",typeof(string));
		}
		#region Database schema details
		// model for database field CLAIM_ID(int)
		public int CLAIM_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CLAIM_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CLAIM_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CLAIM_ID"] = value;
			}
		}
		// model for database field REOPEN_ID(int)
		public int REOPEN_ID
		{
			get
			{
				return base.dtModel.Rows[0]["REOPEN_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["REOPEN_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REOPEN_ID"] = value;
			}
		}
		// model for database field REOPEN_DATE(DateTime)
		public DateTime REOPEN_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["REOPEN_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["REOPEN_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REOPEN_DATE"] = value;
			}
		}
		// model for database field REOPEN_BY(int)
		public int REOPEN_BY
		{
			get
			{
				return base.dtModel.Rows[0]["REOPEN_BY"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["REOPEN_BY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REOPEN_BY"] = value;
			}
		}
		// model for database field REASON(string)
		public string REASON
		{
			get
			{
				return base.dtModel.Rows[0]["REASON"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REASON"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REASON"] = value;
			}
		}
		#endregion
	}
}
