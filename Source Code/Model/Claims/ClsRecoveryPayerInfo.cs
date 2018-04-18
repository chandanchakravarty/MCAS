/******************************************************************************************
<Author				: -   Vijay Arora
<Start Date				: -	6/1/2006 11:49:40 AM
<End Date				: -	
<Description				: - 	Model Class Claims Payee Details
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
namespace Cms.Model.Claims
{
	/// <summary>
	/// Database Model for CLM_ACTIVITY_RECOVERY_PAYER.
	/// </summary>
	public class ClsRecoveryPayerInfo : Cms.Model.ClsCommonModel
	{
		private const string CLM_ACTIVITY_RECOVERY_PAYER = "CLM_ACTIVITY_RECOVERY_PAYER";
		public ClsRecoveryPayerInfo()
		{
			base.dtModel.TableName = "CLM_ACTIVITY_RECOVERY_PAYER";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table CLM_ACTIVITY_RECOVERY_PAYER
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CLAIM_ID",typeof(int));
			base.dtModel.Columns.Add("ACTIVITY_ID",typeof(int));			
			base.dtModel.Columns.Add("PAYER_ID",typeof(int));			
			base.dtModel.Columns.Add("RECOVERY_TYPE",typeof(int));			
			base.dtModel.Columns.Add("RECEIVED_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("RECEIVED_FROM",typeof(string));
			base.dtModel.Columns.Add("CHECK_NUMBER",typeof(string));
			base.dtModel.Columns.Add("DESCRIPTION",typeof(string));			
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
		// model for database field ACTIVITY_ID(int)
		public int ACTIVITY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ACTIVITY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ACTIVITY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ACTIVITY_ID"] = value;
			}
		}		
		// model for database field PAYER_ID(int)
		public int PAYER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["PAYER_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PAYER_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PAYER_ID"] = value;
			}
		}	
		// model for database field RECOVERY_TYPE(int)
		public int RECOVERY_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["RECOVERY_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["RECOVERY_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RECOVERY_TYPE"] = value;
			}
		}
		// model for database field RECEIVED_DATE(DateTime)
		public DateTime RECEIVED_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["RECEIVED_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["RECEIVED_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RECEIVED_DATE"] = value;
			}
		}	
		// model for database field RECEIVED_FROM(string)
		public string RECEIVED_FROM
		{
			get
			{
				return base.dtModel.Rows[0]["RECEIVED_FROM"] == DBNull.Value ? "" : base.dtModel.Rows[0]["RECEIVED_FROM"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["RECEIVED_FROM"] = value;
			}
		}
		// model for database field CHECK_NUMBER(string)
		public string CHECK_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["CHECK_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CHECK_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CHECK_NUMBER"] = value;
			}
		}
		// model for database field DESCRIPTION(string)
		public string DESCRIPTION
		{
			get
			{
				return base.dtModel.Rows[0]["DESCRIPTION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DESCRIPTION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESCRIPTION"] = value;
			}
		}
		#endregion
	}
}
