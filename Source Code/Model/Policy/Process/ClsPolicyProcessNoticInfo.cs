/******************************************************************************************
<Author					: -		Vijay Arora
<Start Date				: -		12/22/2005 5:16:33 PM
<End Date				: -	
<Description			: - 	Model Class for Policy Processes Master
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

namespace Cms.Model.Policy.Process
{
	/// <summary>
	/// Database Model for POL_POLICY_NOTICE_MASTER.
	/// </summary>
	public class ClsPolicyProcessNoticInfo : Cms.Model.ClsCommonModel
	{
		private const string POL_POLICY_NOTICE_MASTER = "POL_POLICY_NOTICE_MASTER";
		public ClsPolicyProcessNoticInfo()
		{
			base.dtModel.TableName = "POL_POLICY_NOTICE_MASTER";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table POL_POLICY_NOTICE_MASTER
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("ROW_ID",typeof(int));
			base.dtModel.Columns.Add("NOTICE_DESCRIPTION",typeof(string));
			base.dtModel.Columns.Add("NOTICE_TYPE",typeof(string));

		}
		#region Database schema details
		// model for database field CUSTOMER_ID(int)
		public int CUSTOMER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CUSTOMER_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_ID"] = value;
			}
		}
		// model for database field POLICY_ID(int)
		public int POLICY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["POLICY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_ID"] = value;
			}
		}
		// model for database field POLICY_VERSION_ID(int)
		public int POLICY_VERSION_ID
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_VERSION_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["POLICY_VERSION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_VERSION_ID"] = value;
			}
		}
		// model for database field ROW_ID(int)
		public int ROW_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ROW_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ROW_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ROW_ID"] = value;
			}
		}
		// model for database field NOTICE_DESCRIPTION(string)
		public string NOTICE_DESCRIPTION
		{
			get
			{
				return base.dtModel.Rows[0]["NOTICE_DESCRIPTION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["NOTICE_DESCRIPTION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["NOTICE_DESCRIPTION"] = value;
			}
		}
		// model for database field NOTICE_TYPE(string)
		public string NOTICE_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["NOTICE_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["NOTICE_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["NOTICE_TYPE"] = value;
			}
		}
		#endregion
	}
}
