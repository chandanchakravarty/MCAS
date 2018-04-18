/******************************************************************************************
<Author				: -   Gaurav Tyagi
<Start Date				: -	5/10/2005 10:58:29 AM
<End Date				: -	
<Description				: - 	This file is used to
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
	/// Database Model for MNT_USER_TYPES.
	/// </summary>
	public class ClsUserTypeInfo : Cms.Model.ClsCommonModel
	{
		private const string MNT_USER_TYPES = "MNT_USER_TYPES";
		public ClsUserTypeInfo()
		{
			base.dtModel.TableName = "MNT_USER_TYPES";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table MNT_USER_TYPES
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("USER_TYPE_ID",typeof(int));
			base.dtModel.Columns.Add("USER_TYPE_CODE",typeof(string));
			base.dtModel.Columns.Add("USER_TYPE_DESC",typeof(string));
			base.dtModel.Columns.Add("USER_TYPE_SYSTEM",typeof(string));
			base.dtModel.Columns.Add("USER_TYPE_FOR_CARRIER",typeof(int));
		}
		#region Database schema details
		// model for database field USER_TYPE_ID(int)
		public int USER_TYPE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["USER_TYPE_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["USER_TYPE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["USER_TYPE_ID"] = value;
			}
		}
		// model for database field USER_TYPE_CODE(string)
		public string USER_TYPE_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["USER_TYPE_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["USER_TYPE_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["USER_TYPE_CODE"] = value;
			}
		}
		// model for database field USER_TYPE_DESC(string)
		public string USER_TYPE_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["USER_TYPE_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["USER_TYPE_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["USER_TYPE_DESC"] = value;
			}
		}
		// model for database field USER_TYPE_SYSTEM(string)
		public string USER_TYPE_SYSTEM
		{
			get
			{
				return base.dtModel.Rows[0]["USER_TYPE_SYSTEM"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["USER_TYPE_SYSTEM"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["USER_TYPE_SYSTEM"] = value;
			}
		}
		// model for database field USER_TYPE_FOR_CARRIER(int)
		public int USER_TYPE_FOR_CARRIER
		{
			get
			{
				return base.dtModel.Rows[0]["USER_TYPE_FOR_CARRIER"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["USER_TYPE_FOR_CARRIER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["USER_TYPE_FOR_CARRIER"] = value;
			}
		}
		#endregion
	}
}
