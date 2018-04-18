/******************************************************************************************
<Author					: - Swastika Gaur
<Start Date				: -	
<End Date				: -	
<Description			: - Model class for Customer Payments from Agency
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

namespace Cms.Model.Account
{
	/// <summary>
	/// Database Model for ACT_CUSTOMER_PAYMENTS_FROM_AGENCY.
	/// </summary>
	public class ClsCustAgencyPaymentsInfo : Cms.Model.ClsCommonModel
	{
		private const string ACT_CUSTOMER_PAYMENTS_FROM_AGENCY = "ACT_CUSTOMER_PAYMENTS_FROM_AGENCY";
		public ClsCustAgencyPaymentsInfo()
		{
			base.dtModel.TableName = "ACT_CUSTOMER_PAYMENTS_FROM_AGENCY";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table ACT_CUSTOMER_PAYMENTS_FROM_AGENCY
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("IDEN_ROW_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_NUMBER",typeof(string));
			base.dtModel.Columns.Add("AGENCY_ID",typeof(int));
			base.dtModel.Columns.Add("AMOUNT",typeof(double));
			base.dtModel.Columns.Add("REF_DEPOSIT_ID",typeof(int));
			base.dtModel.Columns.Add("REF_DEPOSIT_LINE_ITEM_ID",typeof(int));
			base.dtModel.Columns.Add("REF_EFT_SPOOL_ID",typeof(int));
			base.dtModel.Columns.Add("TOTAL_DUE",typeof(double));
			base.dtModel.Columns.Add("MIN_DUE",typeof(double));
			base.dtModel.Columns.Add("MODE",typeof(int));
			base.dtModel.Columns.Add("CREATED_BY_USER",typeof(int));
		}
		#region Schema Details

		// model for database field IDEN_ROW_ID(int)
		public int IDEN_ROW_ID
		{
			get
			{
				return base.dtModel.Rows[0]["IDEN_ROW_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["IDEN_ROW_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["IDEN_ROW_ID"] = value;
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
				
		//model for database field Policy Number(string)
		public string POLICY_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["POLICY_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_NUMBER"] = value;
			}
		}

		// model for database field AGENCY_ID(int)
		public int AGENCY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["AGENCY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["AGENCY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AGENCY_ID"] = value;
			}
		}

		// model for database field AMOUNT(double)
		public double AMOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["AMOUNT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AMOUNT"] = value;
			}
		}

		// model for database field REF_DEPOSIT_ID(int)
		public int REF_DEPOSIT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["REF_DEPOSIT_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["REF_DEPOSIT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REF_DEPOSIT_ID"] = value;
			}
		}

		// model for database field REF_DEPOSIT_LINE_ITEM_ID(int)
		public int REF_DEPOSIT_LINE_ITEM_ID
		{
			get
			{
				return base.dtModel.Rows[0]["REF_DEPOSIT_LINE_ITEM_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["REF_DEPOSIT_LINE_ITEM_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REF_DEPOSIT_LINE_ITEM_ID"] = value;
			}
		}
		 
		// model for database field REF_EFT_SPOOL_ID(int)
		public int REF_EFT_SPOOL_ID
		{
			get
			{
				return base.dtModel.Rows[0]["REF_EFT_SPOOL_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["REF_EFT_SPOOL_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REF_EFT_SPOOL_ID"] = value;
			}
		}

		// model for database field TOTAL_DUE(double)
		public double TOTAL_DUE
		{
			get
			{
				return base.dtModel.Rows[0]["TOTAL_DUE"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["TOTAL_DUE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["TOTAL_DUE"] = value;
			}
		}
		// model for database field MIN_DUE(double)
		public double MIN_DUE
		{
			get
			{
				return base.dtModel.Rows[0]["MIN_DUE"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["MIN_DUE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MIN_DUE"] = value;
			}
		}
		
		// model for database field MODE(int)
		public int MODE
		{
			get
			{
				return base.dtModel.Rows[0]["MODE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["MODE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MODE"] = value;
			}
		}
		// model for database field CREATED_BY_USER(int)
		public int CREATED_BY_USER
		{
			get
			{
				return base.dtModel.Rows[0]["CREATED_BY_USER"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CREATED_BY_USER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CREATED_BY_USER"] = value;
			}
		}
		#endregion
	}
}
