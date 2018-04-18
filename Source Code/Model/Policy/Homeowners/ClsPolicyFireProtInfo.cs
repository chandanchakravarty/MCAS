/******************************************************************************************
<Author				: - Anurag Verma
<Start Date			: -	18/11/2005 5:40:31 PM
<End Date			: -	
<Description		: - Model class for POL_HOME_OWNER_FIRE_PROT_CLEAN table
<Review Date		: - 
<Reviewed By		: - 	
*******************************************************************************************/ 

using System;
using System.Collections;
using System.Data;
using Cms.Model;

namespace Cms.Model.Policy.Homeowners
{
	/// <summary>
	/// Summary description for ClsPolicyFireProtInfo.
	/// </summary>
	public class ClsPolicyFireProtInfo : Cms.Model.ClsCommonModel 
	{
		public ClsPolicyFireProtInfo()
		{
			base.dtModel.TableName = "POL_HOME_OWNER_FIRE_PROT_CLEAN";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table APP_HOME_OWNER_SOLID_FUEL
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("FUEL_ID",typeof(int));
			base.dtModel.Columns.Add("IS_SMOKE_DETECTOR",typeof(string));
			base.dtModel.Columns.Add("IS_PROTECTIVE_MAT_FLOOR",typeof(string));
			base.dtModel.Columns.Add("IS_PROTECTIVE_MAT_WALLS",typeof(string));
			base.dtModel.Columns.Add("PROT_MAT_SPACED",typeof(string));
			base.dtModel.Columns.Add("STOVE_SMOKE_PIPE_CLEANED",typeof(string));
			base.dtModel.Columns.Add("STOVE_CLEANER",typeof(string));
			base.dtModel.Columns.Add("REMARKS",typeof(string));
		}
		#region Database schema details
		// model for database field CUSTOMER_ID(int)
		public int CUSTOMER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CUSTOMER_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_ID"] = value;
			}
		}
		// model for database field APP_ID(int)
		public int POLICY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["POLICY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_ID"] = value;
			}
		}
		// model for database field APP_VERSION_ID(int)
		public int POLICY_VERSION_ID
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_VERSION_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["POLICY_VERSION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_VERSION_ID"] = value;
			}
		}
		// model for database field FUEL_ID(int)
		public int FUEL_ID
		{
			get
			{
				return base.dtModel.Rows[0]["FUEL_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["FUEL_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["FUEL_ID"] = value;
			}
		}
		// model for database field IS_SMOKE_DETECTOR(string)
		public string IS_SMOKE_DETECTOR
		{
			get
			{
				return base.dtModel.Rows[0]["IS_SMOKE_DETECTOR"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_SMOKE_DETECTOR"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_SMOKE_DETECTOR"] = value;
			}
		}
		// model for database field IS_PROTECTIVE_MAT_FLOOR(string)
		public string IS_PROTECTIVE_MAT_FLOOR
		{
			get
			{
				return base.dtModel.Rows[0]["IS_PROTECTIVE_MAT_FLOOR"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_PROTECTIVE_MAT_FLOOR"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_PROTECTIVE_MAT_FLOOR"] = value;
			}
		}
		// model for database field IS_PROTECTIVE_MAT_WALLS(string)
		public string IS_PROTECTIVE_MAT_WALLS
		{
			get
			{
				return base.dtModel.Rows[0]["IS_PROTECTIVE_MAT_WALLS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_PROTECTIVE_MAT_WALLS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_PROTECTIVE_MAT_WALLS"] = value;
			}
		}
		// model for database field PROT_MAT_SPACED(string)
		public string PROT_MAT_SPACED
		{
			get
			{
				return base.dtModel.Rows[0]["PROT_MAT_SPACED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PROT_MAT_SPACED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PROT_MAT_SPACED"] = value;
			}
		}
		// model for database field STOVE_SMOKE_PIPE_CLEANED(string)
		public string STOVE_SMOKE_PIPE_CLEANED
		{
			get
			{
				return base.dtModel.Rows[0]["STOVE_SMOKE_PIPE_CLEANED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["STOVE_SMOKE_PIPE_CLEANED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["STOVE_SMOKE_PIPE_CLEANED"] = value;
			}
		}
		// model for database field STOVE_CLEANER(string)
		public string STOVE_CLEANER
		{
			get
			{
				return base.dtModel.Rows[0]["STOVE_CLEANER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["STOVE_CLEANER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["STOVE_CLEANER"] = value;
			}
		}
		// model for database field REMARKS(string)
		public string REMARKS
		{
			get
			{
				return base.dtModel.Rows[0]["REMARKS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["REMARKS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REMARKS"] = value;
			}
		}
		#endregion
	}
}
