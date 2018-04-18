/******************************************************************************************
<Author				: -   Gaurav Tyagi
<Start Date				: -	07/05/2005 2:27:48 PM
<End Date				: -	
<Description				: - 	Models mnt_default_hierarchy table
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
namespace Cms.Model.Maintenance
{
	/// <summary>
	/// Database Model for MNT_DEFAILT_HIERARCHY.
	/// </summary>
	public class ClsDefaultHierarchyInfo : Cms.Model.ClsCommonModel
	{
		private const string MNT_DEFAILT_HIERARCHY = "MNT_DEFAILT_HIERARCHY";
		public ClsDefaultHierarchyInfo()
		{
			base.dtModel.TableName = "MNT_DEFAILT_HIERARCHY";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table MNT_DEFAILT_HIERARCHY
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("REC_ID",typeof(int));
			base.dtModel.Columns.Add("AGENCY_ID",typeof(int));
			base.dtModel.Columns.Add("DIV_ID",typeof(int));
			base.dtModel.Columns.Add("DEPT_ID",typeof(int));
			base.dtModel.Columns.Add("PC_ID",typeof(int));
			
			
		}
		#region Database schema details


		// model for database field DIV_ID(int)
		public int PC_ID
		{
			get
			{
				return base.dtModel.Rows[0]["PC_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["PC_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PC_ID"] = value;
			}
		}

		// model for database field DIV_ID(int)
		public int DIV_ID
		{
			get
			{
				return base.dtModel.Rows[0]["DIV_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DIV_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DIV_ID"] = value;
			}
		}

		// model for database field DEPT_ID(int)
		public int DEPT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["DEPT_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DEPT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DEPT_ID"] = value;
			}
		}

		// model for database field REC_ID(int)
		public int REC_ID
		{
			get
			{
				return base.dtModel.Rows[0]["REC_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["REC_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REC_ID"] = value;
			}
		}

		// model for database field AGENCY_ID(int)
		public int AGENCY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["AGENCY_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["AGENCY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AGENCY_ID"] = value;
			}
		}
		#endregion
		
	}
}