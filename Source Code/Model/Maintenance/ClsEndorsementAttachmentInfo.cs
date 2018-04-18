using System;

namespace Cms.Model.Maintenance
{
	/// <summary>
	/// Summary description for ClsEndorsementAttachmentInfo.
	/// </summary>
	public class ClsEndorsementAttachmentInfo: Cms.Model.ClsCommonModel
	{
		public ClsEndorsementAttachmentInfo()
		{
			base.dtModel.TableName = "MNT_ENDORSEMENT_ATTACHMENT";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table MNT_ENDORSEMENT_ATTACHMENT
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("ENDORSEMENT_ATTACH_ID",typeof(int));
			base.dtModel.Columns.Add("ENDORSEMENT_ID",typeof(int));
			base.dtModel.Columns.Add("ATTACH_FILE",typeof(string));
			base.dtModel.Columns.Add("VALID_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("EFFECTIVE_TO_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("DISABLED_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("FORM_NUMBER",typeof(string));
			base.dtModel.Columns.Add("EDITION_DATE",typeof(string));
			

		}
		#region Database schema details
		// model for database field VALID_DATE(DateTime)
		public DateTime VALID_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["VALID_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["VALID_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["VALID_DATE"] = value;
			}
		}
		// model for database field ATTACH_FILE(string)
		
		public string ATTACH_FILE
		{
			get
			{
				return base.dtModel.Rows[0]["ATTACH_FILE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ATTACH_FILE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ATTACH_FILE"] = value;
			}
		}
		// model for database field ENDORSEMENT_ATTACH_ID(int)
		public int ENDORSEMENT_ATTACH_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ENDORSEMENT_ATTACH_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["ENDORSEMENT_ATTACH_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ENDORSEMENT_ATTACH_ID"] = value;
			}
		}

		// model for database field ENDORSEMENT_ID(int)
		public int ENDORSEMENT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ENDORSEMENT_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["ENDORSEMENT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ENDORSEMENT_ID"] = value;
			}
		}
		//model for database field EFFECTIVE_TO_DATE
		public DateTime EFFECTIVE_TO_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["EFFECTIVE_TO_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["EFFECTIVE_TO_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EFFECTIVE_TO_DATE"] = value;
			}
		}

		public DateTime DISABLED_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["DISABLED_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["DISABLED_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DISABLED_DATE"] = value;
			}
		}
		// model for database field FORM_NUMBER(string)
		public string FORM_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["FORM_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["FORM_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["FORM_NUMBER"] = value;
			}
		}
		//model for database field EDITION_DATE
		public string EDITION_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["EDITION_DATE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["EDITION_DATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EDITION_DATE"] = value;
			}
		}
		# endregion
	}
}
