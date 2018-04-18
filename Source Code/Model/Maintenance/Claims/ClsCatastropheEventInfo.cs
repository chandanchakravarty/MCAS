/******************************************************************************************
<Author					: -		Vijay Arora
<Start Date				: -		4/24/2006 11:59:30 AM
<End Date				: -	
<Description			: - 	Model Class for Catatrophe Event
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
namespace Cms.Model.Maintenance.Claims
{
	/// <summary>
	/// Database Model for CLM_CATASTROPHE_EVENT.
	/// </summary>
	public class ClsCatastropheEventInfo : Cms.Model.ClsCommonModel
	{
		private const string CLM_CATASTROPHE_EVENT = "CLM_CATASTROPHE_EVENT";
		public ClsCatastropheEventInfo()
		{
			base.dtModel.TableName = "CLM_CATASTROPHE_EVENT";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table CLM_CATASTROPHE_EVENT
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CATASTROPHE_EVENT_ID",typeof(int));
			base.dtModel.Columns.Add("CATASTROPHE_EVENT_TYPE",typeof(int));
			base.dtModel.Columns.Add("DATE_FROM",typeof(DateTime));
			base.dtModel.Columns.Add("DATE_TO",typeof(DateTime));
			base.dtModel.Columns.Add("DESCRIPTION",typeof(string));
			base.dtModel.Columns.Add("CAT_CODE",typeof(string));
		}
		#region Database schema details
		// model for database field CATASTROPHE_EVENT_ID(int)
		public int CATASTROPHE_EVENT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CATASTROPHE_EVENT_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CATASTROPHE_EVENT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CATASTROPHE_EVENT_ID"] = value;
			}
		}
		// model for database field CATASTROPHE_EVENT_TYPE(int)
		public int CATASTROPHE_EVENT_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["CATASTROPHE_EVENT_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CATASTROPHE_EVENT_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CATASTROPHE_EVENT_TYPE"] = value;
			}
		}
		// model for database field DATE_FROM(DateTime)
		public DateTime DATE_FROM
		{
			get
			{
				return base.dtModel.Rows[0]["DATE_FROM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["DATE_FROM"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DATE_FROM"] = value;
			}
		}
		// model for database field DATE_TO(DateTime)
		public DateTime DATE_TO
		{
			get
			{
				return base.dtModel.Rows[0]["DATE_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["DATE_TO"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DATE_TO"] = value;
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
		// model for database field CAT_CODE(string)
		public string CAT_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["CAT_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CAT_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CAT_CODE"] = value;
			}
		}
		#endregion
	}
}
