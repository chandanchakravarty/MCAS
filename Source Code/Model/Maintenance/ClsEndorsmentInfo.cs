/******************************************************************************************
<Author				: -   Gaurav
<Start Date				: -	8/26/2005 12:21:20 PM
<End Date				: -	
<Description				: - 	This file will be used as model for endorsement
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
namespace Cms.BusinessLayer.BlCommon
{
	/// <summary>
	/// Database Model for MNT_ENDORSMENT_DETAILS.
	/// </summary>
	public class ClsEndorsmentDetailsInfo : Cms.Model.ClsCommonModel
	{
		private const string MNT_ENDORSMENT_DETAILS = "MNT_ENDORSMENT_DETAILS";
		public ClsEndorsmentDetailsInfo()
		{
			base.dtModel.TableName = "MNT_ENDORSMENT_DETAILS";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table MNT_ENDORSMENT_DETAILS
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("ENDORSMENT_ID",typeof(int));
			base.dtModel.Columns.Add("STATE_ID",typeof(int));
			base.dtModel.Columns.Add("LOB_ID",typeof(int));
			base.dtModel.Columns.Add("PURPOSE",typeof(string));
			base.dtModel.Columns.Add("TYPE",typeof(string));
			base.dtModel.Columns.Add("DESCRIPTION",typeof(string));
			base.dtModel.Columns.Add("TEXT",typeof(string));
			base.dtModel.Columns.Add("ENDORSEMENT_CODE",typeof(string));
			base.dtModel.Columns.Add("ENDORS_ASSOC_COVERAGE",typeof(string));
			base.dtModel.Columns.Add("SELECT_COVERAGE",typeof(int));
			base.dtModel.Columns.Add("ENDORS_PRINT",typeof(string));
			base.dtModel.Columns.Add("ENDORS_ATTACHMENT",typeof(string));
			base.dtModel.Columns.Add("EFFECTIVE_FROM_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("EFFECTIVE_TO_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("DISABLED_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("FORM_NUMBER",typeof(string));
			base.dtModel.Columns.Add("EDITION_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("INCREASED_LIMIT",typeof(int));
			base.dtModel.Columns.Add("PRINT_ORDER",typeof(int));
		}
		#region Database schema details

		
		// model for database field ENDORS_ATTACHMENT(string)
		public string ENDORS_ATTACHMENT
		{
			get
			{
				return base.dtModel.Rows[0]["ENDORS_ATTACHMENT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ENDORS_ATTACHMENT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ENDORS_ATTACHMENT"] = value;
			}
		}
		// model for database field ENDORS_PRINT(string)
		public string ENDORS_PRINT
		{
			get
			{
				return base.dtModel.Rows[0]["ENDORS_PRINT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ENDORS_PRINT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ENDORS_PRINT"] = value;
			}
		}

		// model for database field ENDORSMENT_ID(int)
		public int ENDORSMENT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ENDORSMENT_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ENDORSMENT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ENDORSMENT_ID"] = value;
			}
		}
		// model for database field STATE_ID(int)
		public int STATE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["STATE_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["STATE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["STATE_ID"] = value;
			}
		}

		// model for database field ENDORSEMENT_CODE(string)
		public string ENDORSEMENT_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["ENDORSEMENT_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ENDORSEMENT_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ENDORSEMENT_CODE"] = value;
			}
		}
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
		// model for database field PURPOSE(string)
		public string PURPOSE
		{
			get
			{
				return base.dtModel.Rows[0]["PURPOSE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PURPOSE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PURPOSE"] = value;
			}
		}
		// model for database field TYPE(string)
		public string TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TYPE"] = value;
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
		// model for database field TEXT(string)
		public string TEXT
		{
			get
			{
				return base.dtModel.Rows[0]["TEXT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["TEXT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TEXT"] = value;
			}
		}
		// model for database field ENDORS_ASSOC_COVERAGE(string)
		public string ENDORS_ASSOC_COVERAGE
		{
			get
			{
				return base.dtModel.Rows[0]["ENDORS_ASSOC_COVERAGE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ENDORS_ASSOC_COVERAGE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ENDORS_ASSOC_COVERAGE"] = value;
			}
		}
		// model for database field SELECT_COVERAGE(int)
		public int SELECT_COVERAGE
		{
			get
			{
				return base.dtModel.Rows[0]["SELECT_COVERAGE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["SELECT_COVERAGE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SELECT_COVERAGE"] = value;
			}
		}
		public DateTime EFFECTIVE_FROM_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["EFFECTIVE_FROM_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["EFFECTIVE_FROM_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EFFECTIVE_FROM_DATE"] = value;
			}
		}

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
		public DateTime EDITION_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["EDITION_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["EDITION_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EDITION_DATE"] = value;
			}
		}
		public int INCREASED_LIMIT
		{
			get
			{
				return base.dtModel.Rows[0]["INCREASED_LIMIT"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["INCREASED_LIMIT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INCREASED_LIMIT"] = value;
			}
		}
		public int PRINT_ORDER
		{
			get
			{
				return base.dtModel.Rows[0]["PRINT_ORDER"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PRINT_ORDER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PRINT_ORDER"] = value;
			}
		}

		#endregion
	}
}
