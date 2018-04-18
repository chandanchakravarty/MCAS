/******************************************************************************************
<Author				: -   Sumit Chhabra
<Start Date				: -	4/30/2007
<End Date				: -	
<Description		    : -Model For State Country List
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
	/// Database Model for MNT_COUNTRY_STATE_LIST.
	/// </summary>
	public class ClsStateInfo : Cms.Model.ClsCommonModel
	{
		private const string MNT_COUNTRY_STATE_LIST = "MNT_COUNTRY_STATE_LIST";
		public ClsStateInfo()
		{
			base.dtModel.TableName = "MNT_COUNTRY_STATE_LIST";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table MNT_COUNTRY_STATE_LIST
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("COUNTRY_ID",typeof(int));
			base.dtModel.Columns.Add("STATE_CODE",typeof(string));
			base.dtModel.Columns.Add("STATE_NAME",typeof(string));
			base.dtModel.Columns.Add("STATE_DESC",typeof(string));
			base.dtModel.Columns.Add("STATE_ID",typeof(int));			
		}
		#region Database schema details
		// model for database field COUNTRY_ID(int)
		public int COUNTRY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["COUNTRY_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["COUNTRY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COUNTRY_ID"] = value;
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
		// model for database field STATE_CODE(string)
		public string STATE_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["STATE_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["STATE_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["STATE_CODE"] = value;
			}
		}
		// model for database field STATE_NAME(string)
		public string STATE_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["STATE_NAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["STATE_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["STATE_NAME"] = value;
			}
		}
		// model for database field STATE_DESC(string)
		public string STATE_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["STATE_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["STATE_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["STATE_DESC"] = value;
			}
		}
		#endregion
	}
}
