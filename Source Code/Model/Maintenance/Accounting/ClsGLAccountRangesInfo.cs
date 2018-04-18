	/******************************************************************************************
	<Author					: -   Ajit Singh Chahal
	<Start Date				: -	  5/11/2005 11:19:24 AM
	<End Date				: -	
	<Description			: -   Model for GL AccountRanges
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
	namespace Cms.Model.Maintenance.Accounting
	{
		/// <summary>
		/// Database Model for ACT_GL_ACCOUNT_RANGES.
		/// </summary>
		public class ClsGLAccountRangesInfo : Cms.Model.ClsCommonModel
		{
			private const string ACT_GL_ACCOUNT_RANGES = "ACT_GL_ACCOUNT_RANGES";
			public ClsGLAccountRangesInfo()
			{
				base.dtModel.TableName = "ACT_GL_ACCOUNT_RANGES";	// setting table name for data table that holds property values.
				this.AddColumns();									// add columns of the database table ACT_GL_ACCOUNT_RANGES
				base.dtModel.Rows.Add(base.dtModel.NewRow());		// add a blank row in the datatable
			}
			private void AddColumns()
			{
				base.dtModel.Columns.Add("CATEGORY_ID",typeof(int));
				base.dtModel.Columns.Add("PARENT_CATEGORY_ID",typeof(int));
				base.dtModel.Columns.Add("CATEGORY_DESC",typeof(string));
				base.dtModel.Columns.Add("RANGE_FROM",typeof(double));
				base.dtModel.Columns.Add("RANGE_TO",typeof(double));
			}
			#region Database schema details
			// model for database field CATEGORY_ID(int)
			public int CATEGORY_ID
			{
				get
				{
					return base.dtModel.Rows[0]["CATEGORY_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CATEGORY_ID"].ToString());
				}
				set
				{
					base.dtModel.Rows[0]["CATEGORY_ID"] = value;
				}
			}
			// model for database field PARENT_CATEGORY_ID(int)
			public int PARENT_CATEGORY_ID
			{
				get
				{
					return base.dtModel.Rows[0]["PARENT_CATEGORY_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["PARENT_CATEGORY_ID"].ToString());
				}
				set
				{
					base.dtModel.Rows[0]["PARENT_CATEGORY_ID"] = value;
				}
			}
			// model for database field CATEGORY_DESC(string)
			public string CATEGORY_DESC
			{
				get
				{
					return base.dtModel.Rows[0]["CATEGORY_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CATEGORY_DESC"].ToString();
				}
				set
				{
					base.dtModel.Rows[0]["CATEGORY_DESC"] = value;
				}
			}
			// model for database field RANGE_FROM(double)
			public double RANGE_FROM
			{
				get
				{
					return base.dtModel.Rows[0]["RANGE_FROM"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["RANGE_FROM"].ToString());
				}
				set
				{
					base.dtModel.Rows[0]["RANGE_FROM"] = value;
				}
			}
			// model for database field RANGE_TO(double)
			public double RANGE_TO
			{
				get
				{
					return base.dtModel.Rows[0]["RANGE_TO"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["RANGE_TO"].ToString());
				}
				set
				{
					base.dtModel.Rows[0]["RANGE_TO"] = value;
				}
			}
			#endregion
		}
	}
