/******************************************************************************************
<Author				: -   Ajit Singh chahal
<Start Date				: -	6/7/2005 7:32:52 PM
<End Date				: -	
<Description				: - 	Model For Transaction Code Group.
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

namespace Cms.Model.Maintenance.Accounting
{
	/// <summary>
	/// Database Model for ACT_TRAN_CODE_GROUP.
	/// </summary>
	public class ClsTransactionCodeGroupInfo: Cms.Model.ClsCommonModel
	{
		private const string ACT_TRAN_CODE_GROUP = "ACT_TRAN_CODE_GROUP";
		public ClsTransactionCodeGroupInfo()
		{
			base.dtModel.TableName = "ACT_TRAN_CODE_GROUP";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table ACT_TRAN_CODE_GROUP
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("TRAN_GROUP_ID",typeof(int));
			base.dtModel.Columns.Add("COUNTRY_ID",typeof(int));
			base.dtModel.Columns.Add("STATE_ID",typeof(int));
			base.dtModel.Columns.Add("LOB_ID",typeof(int));
			base.dtModel.Columns.Add("SUB_LOB_ID",typeof(int));
			base.dtModel.Columns.Add("CLASS_RISK",typeof(int));
			base.dtModel.Columns.Add("TERM",typeof(string));
			base.dtModel.Columns.Add("POLICY_TYPE",typeof(string));
			base.dtModel.Columns.Add("NEW_BUSINESS",typeof(string));
			base.dtModel.Columns.Add("CHANGE_IN_NEW_BUSINESS",typeof(string));
			base.dtModel.Columns.Add("RENEWAL",typeof(string));
			base.dtModel.Columns.Add("CHANGE_IN_RENEWAL",typeof(string));
			base.dtModel.Columns.Add("REINSTATE_SAME_TERM",typeof(string));
			base.dtModel.Columns.Add("REINSTATE_NEW_TERM",typeof(string));
			base.dtModel.Columns.Add("CANCELLATION",typeof(string));
		}
		#region Database schema details
		// model for database field TRAN_GROUP_ID(int)
		public int TRAN_GROUP_ID
		{
			get
			{
				return base.dtModel.Rows[0]["TRAN_GROUP_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["TRAN_GROUP_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["TRAN_GROUP_ID"] = value;
			}
		}
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
		// model for database field SUB_LOB_ID(int)
		public int SUB_LOB_ID
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_LOB_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["SUB_LOB_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SUB_LOB_ID"] = value;
			}
		}
		// model for database field CLASS_RISK(int)
		public int CLASS_RISK
		{
			get
			{
				return base.dtModel.Rows[0]["CLASS_RISK"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CLASS_RISK"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CLASS_RISK"] = value;
			}
		}
		// model for database field TERM(string)
		public string TERM
		{
			get
			{
				return base.dtModel.Rows[0]["TERM"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["TERM"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TERM"] = value;
			}
		}
		// model for database field POLICY_TYPE(string)
		public string POLICY_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["POLICY_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_TYPE"] = value;
			}
		}
		// model for database field NEW_BUSINESS(string)
		public string NEW_BUSINESS
		{
			get
			{
				return base.dtModel.Rows[0]["NEW_BUSINESS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["NEW_BUSINESS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["NEW_BUSINESS"] = value;
			}
		}
		// model for database field CHANGE_IN_NEW_BUSINESS(string)
		public string CHANGE_IN_NEW_BUSINESS
		{
			get
			{
				return base.dtModel.Rows[0]["CHANGE_IN_NEW_BUSINESS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CHANGE_IN_NEW_BUSINESS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CHANGE_IN_NEW_BUSINESS"] = value;
			}
		}
		// model for database field RENEWAL(string)
		public string RENEWAL
		{
			get
			{
				return base.dtModel.Rows[0]["RENEWAL"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["RENEWAL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["RENEWAL"] = value;
			}
		}
		// model for database field CHANGE_IN_RENEWAL(string)
		public string CHANGE_IN_RENEWAL
		{
			get
			{
				return base.dtModel.Rows[0]["CHANGE_IN_RENEWAL"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CHANGE_IN_RENEWAL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CHANGE_IN_RENEWAL"] = value;
			}
		}
		// model for database field REINSTATE_SAME_TERM(string)
		public string REINSTATE_SAME_TERM
		{
			get
			{
				return base.dtModel.Rows[0]["REINSTATE_SAME_TERM"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["REINSTATE_SAME_TERM"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REINSTATE_SAME_TERM"] = value;
			}
		}
		// model for database field REINSTATE_NEW_TERM(string)
		public string REINSTATE_NEW_TERM
		{
			get
			{
				return base.dtModel.Rows[0]["REINSTATE_NEW_TERM"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["REINSTATE_NEW_TERM"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REINSTATE_NEW_TERM"] = value;
			}
		}
		// model for database field CANCELLATION(string)
		public string CANCELLATION
		{
			get
			{
				return base.dtModel.Rows[0]["CANCELLATION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CANCELLATION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CANCELLATION"] = value;
			}
		}
		#endregion
	}
}

