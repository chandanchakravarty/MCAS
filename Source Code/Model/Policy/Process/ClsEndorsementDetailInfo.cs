using System;

namespace Cms.Model.Policy.Process
{
	/// <summary>
	/// Database Model for POL_POLICY_ENDORSEMENTS_DETAILS.
	/// </summary>
	public class ClsEndorsementDetailInfo : Cms.Model.ClsCommonModel
	{
		private const string POL_POLICY_ENDORSEMENTS_DETAILS = "POL_POLICY_ENDORSEMENTS_DETAILS";
		public ClsEndorsementDetailInfo()
		{
			base.dtModel.TableName = "POL_POLICY_ENDORSEMENTS_DETAILS";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table POL_POLICY_ENDORSEMENTS_DETAILS
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("ENDORSEMENT_NO",typeof(int));
			base.dtModel.Columns.Add("ENDORSEMENT_DETAIL_ID",typeof(int));
			base.dtModel.Columns.Add("ENDORSEMENT_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("ENDORSEMENT_TYPE",typeof(int));
			base.dtModel.Columns.Add("ENDORSEMENT_DESC",typeof(string));
			base.dtModel.Columns.Add("REMARKS",typeof(string));
			base.dtModel.Columns.Add("TRANS_ID",typeof(string));
			base.dtModel.Columns.Add("ENDORSEMENT_STATUS",typeof(string));
			
		}
		#region Database schema details
		// model for database field endorsement status(string)
		public string ENDORSEMENT_STATUS
		{
			get
			{
				return base.dtModel.Rows[0]["ENDORSEMENT_STATUS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ENDORSEMENT_STATUS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ENDORSEMENT_STATUS"] = value;
			}
		}

		// model for database field TRANS_ID(string)
		public string TRANS_ID
		{
			get
			{
				return base.dtModel.Rows[0]["TRANS_ID"] == DBNull.Value ? "" : base.dtModel.Rows[0]["TRANS_ID"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TRANS_ID"] = value;
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
		// model for database field ENDORSEMENT_NO(int)
		public int ENDORSEMENT_NO
		{
			get
			{
				return base.dtModel.Rows[0]["ENDORSEMENT_NO"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ENDORSEMENT_NO"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ENDORSEMENT_NO"] = value;
			}
		}
		// model for database field ENDORSEMENT_DETAIL_ID(int)
		public int ENDORSEMENT_DETAIL_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ENDORSEMENT_DETAIL_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ENDORSEMENT_DETAIL_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ENDORSEMENT_DETAIL_ID"] = value;
			}
		}
		// model for database field ENDORSEMENT_DATE(DateTime)
		public DateTime ENDORSEMENT_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["ENDORSEMENT_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["ENDORSEMENT_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ENDORSEMENT_DATE"] = value;
			}
		}
		// model for database field ENDORSEMENT_TYPE(int)
		public int ENDORSEMENT_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["ENDORSEMENT_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ENDORSEMENT_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ENDORSEMENT_TYPE"] = value;
			}
		}
		// model for database field ENDORSEMENT_DESC(string)
		public string ENDORSEMENT_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["ENDORSEMENT_DESC"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ENDORSEMENT_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ENDORSEMENT_DESC"] = value;
			}
		}
		// model for database field REMARKS(string)
		public string REMARKS
		{
			get
			{
				return base.dtModel.Rows[0]["REMARKS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REMARKS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REMARKS"] = value;
			}
		}
		#endregion
	}

}
