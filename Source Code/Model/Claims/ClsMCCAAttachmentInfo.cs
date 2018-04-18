/******************************************************************************************
<Author					: -  Vijay Arora
<Start Date				: -	08-08-2006
<End Date				: -	
<Description			: - 	Model Class for CLM_MCCA_ATTACHMENT
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
namespace Cms.Model.Claims
{
	/// <summary>
	/// Database Model for CLM_MCCA_ATTACHMENT.
	/// </summary>
	public class ClsMCCAAttachmentInfo : Cms.Model.ClsCommonModel
	{
		private const string CLM_MCCA_ATTACHMENT = "CLM_MCCA_ATTACHMENT";
		public ClsMCCAAttachmentInfo()
		{
			base.dtModel.TableName = "CLM_MCCA_ATTACHMENT";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table CLM_MCCA_ATTACHMENT
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("MCCA_ATTACHMENT_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_PERIOD_DATE_FROM",typeof(DateTime));
			base.dtModel.Columns.Add("POLICY_PERIOD_DATE_TO",typeof(DateTime));
			base.dtModel.Columns.Add("LOSS_PERIOD_DATE_FROM",typeof(DateTime));
			base.dtModel.Columns.Add("LOSS_PERIOD_DATE_TO",typeof(DateTime));
			base.dtModel.Columns.Add("MCCA_ATTACHMENT_POINT",typeof(int));
		}
		#region Database schema details
		// model for database field MCCA_ATTACHMENT_ID(int)
		public int MCCA_ATTACHMENT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["MCCA_ATTACHMENT_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["MCCA_ATTACHMENT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MCCA_ATTACHMENT_ID"] = value;
			}
		}
		// model for database field POLICY_PERIOD_DATE_FROM(DateTime)
		public DateTime POLICY_PERIOD_DATE_FROM
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_PERIOD_DATE_FROM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["POLICY_PERIOD_DATE_FROM"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_PERIOD_DATE_FROM"] = value;
			}
		}
		// model for database field POLICY_PERIOD_DATE_TO(DateTime)
		public DateTime POLICY_PERIOD_DATE_TO
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_PERIOD_DATE_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["POLICY_PERIOD_DATE_TO"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_PERIOD_DATE_TO"] = value;
			}
		}
		// model for database field LOSS_PERIOD_DATE_FROM(DateTime)
		public DateTime LOSS_PERIOD_DATE_FROM
		{
			get
			{
				return base.dtModel.Rows[0]["LOSS_PERIOD_DATE_FROM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["LOSS_PERIOD_DATE_FROM"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LOSS_PERIOD_DATE_FROM"] = value;
			}
		}
		// model for database field LOSS_PERIOD_DATE_TO(DateTime)
		public DateTime LOSS_PERIOD_DATE_TO
		{
			get
			{
				return base.dtModel.Rows[0]["LOSS_PERIOD_DATE_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["LOSS_PERIOD_DATE_TO"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LOSS_PERIOD_DATE_TO"] = value;
			}
		}
		// model for database field MCCA_ATTACHMENT_POINT(int)
		public int MCCA_ATTACHMENT_POINT
		{
			get
			{
				return base.dtModel.Rows[0]["MCCA_ATTACHMENT_POINT"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["MCCA_ATTACHMENT_POINT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MCCA_ATTACHMENT_POINT"] = value;
			}
		}
		#endregion
	}
}
