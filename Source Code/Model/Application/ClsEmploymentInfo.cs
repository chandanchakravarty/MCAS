/******************************************************************************************
<Author					: -   Pradeep Iyer
<Start Date				: -	5/30/2005 10:51:16 AM
<End Date				: -	
<Description			: - 	Model class fp Employment Information
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

namespace Cms.Model.Application
{
	/// <summary>
	/// Database Model for APP_UMBRELLA_EMPLOYMENT_INFO.
	/// </summary>
	public class ClsEmploymentInfo : Cms.Model.ClsBaseModel
	{
		private const string APP_UMBRELLA_EMPLOYMENT_INFO = "APP_UMBRELLA_EMPLOYMENT_INFO";
		
		public ClsEmploymentInfo()
		{
			base.dtModel.TableName = "APP_UMBRELLA_EMPLOYMENT_INFO";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table APP_UMBRELLA_EMPLOYMENT_INFO
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("APP_ID",typeof(int));
			base.dtModel.Columns.Add("APP_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("APP_OCCUPATION",typeof(int));
			base.dtModel.Columns.Add("CO_APP_OCCUPATION",typeof(int));
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
		public int APP_ID
		{
			get
			{
				return base.dtModel.Rows[0]["APP_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["APP_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APP_ID"] = value;
			}
		}
		// model for database field APP_VERSION_ID(int)
		public int APP_VERSION_ID
		{
			get
			{
				return base.dtModel.Rows[0]["APP_VERSION_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["APP_VERSION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APP_VERSION_ID"] = value;
			}
		}
		// model for database field APP_OCCUPATION(int)
		public int APP_OCCUPATION
		{
			get
			{
				return base.dtModel.Rows[0]["APP_OCCUPATION"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["APP_OCCUPATION"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APP_OCCUPATION"] = value;
			}
		}
		// model for database field CO_APP_OCCUPATION(int)
		public int CO_APP_OCCUPATION
		{
			get
			{
				return base.dtModel.Rows[0]["CO_APP_OCCUPATION"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CO_APP_OCCUPATION"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CO_APP_OCCUPATION"] = value;
			}
		}
		#endregion
	}
}
