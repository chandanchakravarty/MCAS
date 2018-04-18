using System;
using System.Data;
using Cms.Model;
namespace  Cms.Model.Policy.Homeowners
{
	/// <summary>
	/// Summary description for ClsPolicyHomeOwnerEndorsementInfo.
	/// </summary>
	public class ClsPolicyHomeOwnerEndorsementInfo : Cms.Model.ClsCommonModel
	{
		public ClsPolicyHomeOwnerEndorsementInfo()
		{
			base.dtModel.TableName = "POL_DWELLING_ENDORSEMENTS";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table APP_DWELLING_ENDORSEMENTS
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("DWELLING_ID",typeof(int));
			base.dtModel.Columns.Add("ENDORSEMENT_ID",typeof(int));
			base.dtModel.Columns.Add("REMARKS",typeof(string));
			base.dtModel.Columns.Add("DWELLING_ENDORSEMENT_ID",typeof(int));
			base.dtModel.Columns.Add("ENDORSEMENT",typeof(string));
			base.dtModel.Columns.Add("ACTION",typeof(string));
			base.dtModel.Columns.Add("EDITION_DATE",typeof(string));
			
		}

		#region Database schema details
		// model for database field ENDORSEMENT(string)
		public string ENDORSEMENT
		{
			get
			{
				return base.dtModel.Rows[0]["ENDORSEMENT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ENDORSEMENT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ENDORSEMENT"] = value;
			}
		}
		// model for database field DWELLING_ENDORSEMENT_ID(int)
		public int DWELLING_ENDORSEMENT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["DWELLING_ENDORSEMENT_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DWELLING_ENDORSEMENT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DWELLING_ENDORSEMENT_ID"] = value;
			}
		}
		// model for database field CUSTOMER_ID(int)
		public int CUSTOMER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_ID"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["CUSTOMER_ID"].ToString());
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
				return base.dtModel.Rows[0]["POLICY_ID"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["POLICY_ID"].ToString());
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
				return base.dtModel.Rows[0]["POLICY_VERSION_ID"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["POLICY_VERSION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_VERSION_ID"] = value;
			}
		}
		// model for database field DWELLING_ID(int)
		public int DWELLING_ID
		{
			get
			{
				return base.dtModel.Rows[0]["DWELLING_ID"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["DWELLING_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DWELLING_ID"] = value;
			}
		}
		// model for database field ENDORSEMENT_ID(int)
		public int ENDORSEMENT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ENDORSEMENT_ID"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["ENDORSEMENT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ENDORSEMENT_ID"] = value;
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

		// model for database field ACTION(string)
		public string ACTION
		{
			get
			{
				return base.dtModel.Rows[0]["ACTION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ACTION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ACTION"] = value;
			}
		}
		
			// model for database field EDITION_DATE(string)
			public string EDITION_DATE
			{
				get
				{
					return base.dtModel.Rows[0]["EDITION_DATE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["EDITION_DATE"].ToString();
				}
				set
				{
					base.dtModel.Rows[0]["EDITION_DATE"] = value;
				}
			}
		# endregion
	}
}
