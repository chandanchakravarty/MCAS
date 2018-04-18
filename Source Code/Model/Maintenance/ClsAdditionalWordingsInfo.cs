/******************************************************************************************
<Author					: -   Swarup Pal	
<Start Date				: -	  22-Feb-2007 12:50:04 PM
<End Date				: -	
<Description			: - 	Models MNT_PROCESS_WORDINGS
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;

namespace Cms.Model.Maintenance
{
	/// <summary>
	/// Summary description for ClsAdditionalWordingsInfo.
	/// </summary>
	public class ClsAdditionalWordingsInfo : Cms.Model.ClsCommonModel
	{
		public ClsAdditionalWordingsInfo()
		{
			base.dtModel.TableName = "MNT_PROCESS_WORDINGS";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table MNT_PROCESS_WORDINGS
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("WORDINGS_ID",typeof(int));
			base.dtModel.Columns.Add("STATE_ID",typeof(int));
			base.dtModel.Columns.Add("PROCESS_ID",typeof(int));
			base.dtModel.Columns.Add("LOB_ID",typeof(int));
			base.dtModel.Columns.Add("PDF_WORDINGS",typeof(string));

		}
		#region Database schema details
		
		
		// model for database field WORDINGS_ID(int)
		public int WORDINGS_ID
		{
			get
			{
				return base.dtModel.Rows[0]["WORDINGS_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["WORDINGS_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["WORDINGS_ID"] = value;
			}
		}

		// model for database field STATE_ID(int)
		public int STATE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["STATE_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["STATE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["STATE_ID"] = value;
			}
		}

		// model for database field PROCESS_ID(int)
		public int PROCESS_ID
		{
			get
			{
				return base.dtModel.Rows[0]["PROCESS_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["PROCESS_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PROCESS_ID"] = value;
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
		
		// model for database field PDF_WORDINGS(string)
		public string PDF_WORDINGS
		{
			get
			{
				return base.dtModel.Rows[0]["PDF_WORDINGS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PDF_WORDINGS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PDF_WORDINGS"] = value;
			}
		}
		
		# endregion
	}
}
