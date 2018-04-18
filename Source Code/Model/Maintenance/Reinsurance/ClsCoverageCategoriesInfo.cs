/******************************************************************************************
<Author					: -		Swarup Kumar Pal
<Start Date				: -		09-Aug-2007 
<End Date				: -	
<Description			: - 	Model for ClsCoverageCategoriesInfo.
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

namespace Cms.Model.Maintenance.Reinsurance
{
	/// <summary>
	/// Summary description for ClsCoverageCategoriesInfo.
	/// </summary>
	public class ClsCoverageCategoriesInfo:Cms.Model.ClsCommonModel
	{
		# region D E C L A R A T I O N 
		public ClsCoverageCategoriesInfo()
		{
			// setting table name for data table that holds property values.
			base.dtModel.TableName = "MNT_REINSURANCE_COVERAGE_CATEGORY";	

			// add columns of the database table MNT_REINSURANCE_CONTRACT
			this.AddColumns();								
			
			// add a blank row in the datatable
			base.dtModel.Rows.Add(base.dtModel.NewRow());
		}
		#endregion

		# region  A D D I N G   C O L U M N S   T O   T A B L E 
		
		private void AddColumns()
		{
			base.dtModel.Columns.Add("COVERAGE_CATEGORY_ID",typeof(int)); //1
			base.dtModel.Columns.Add("EFFECTIVE_DATE",typeof(DateTime));//2
			base.dtModel.Columns.Add("LOB_ID",typeof(int));//3
			base.dtModel.Columns.Add("CATEGORY",typeof(string));//4
		}

		# endregion 

		#region D A T B A S E   S C H E M A   D E T A I L S 
		// model for database field COVERAGE_CATEGORY_ID(int)
		public int COVERAGE_CATEGORY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["COVERAGE_CATEGORY_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["COVERAGE_CATEGORY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COVERAGE_CATEGORY_ID"] = value;
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

		// model for database field CATEGORY(string)
		public string CATEGORY
		{
			get
			{
				return base.dtModel.Rows[0]["CATEGORY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CATEGORY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CATEGORY"] = value;
			}
		}

		#endregion
	}
}
