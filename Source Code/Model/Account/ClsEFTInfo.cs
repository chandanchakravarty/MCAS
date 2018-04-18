using System;
using System.Data;
using Cms.Model;


namespace Cms.Model.Account
{
	/// <summary>
	/// Summary description for ClsEFTInfo.
	/// </summary>
	public class ClsEFTInfo : Cms.Model.ClsCommonModel
	{
		private const string ACT_APP_EFT_CUST_INFO = "ACT_APP_EFT_CUST_INFO";
		public ClsEFTInfo()
		{
			base.dtModel.TableName = "ACT_APP_EFT_CUST_INFO";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table ACT_JOURNAL_LINE_ITEMS
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
			//
			// TODO: Add constructor logic here
			//
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("APP_ID",typeof(int));
			base.dtModel.Columns.Add("APP_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("FEDERAL_ID",typeof(string));
			base.dtModel.Columns.Add("DFI_ACC_NO",typeof(string));
			base.dtModel.Columns.Add("TRANSIT_ROUTING_NO",typeof(string));
			base.dtModel.Columns.Add("ACCOUNT_TYPE",typeof(string));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("EFT_TENTATIVE_DATE",typeof(int));
			base.dtModel.Columns.Add("CARD_HOLDER_NAME",typeof(string));
			base.dtModel.Columns.Add("CARD_NO",typeof(string));
			base.dtModel.Columns.Add("CARD_DATE_VALID_FROM",typeof(string));
			base.dtModel.Columns.Add("CARD_DATE_VALID_TO",typeof(string));
			base.dtModel.Columns.Add("CARD_CVV_NUMBER",typeof(string));
			base.dtModel.Columns.Add("CARD_TYPE",typeof(int));
			base.dtModel.Columns.Add("REVERIFIED_AC",typeof(int));
			//Customer Info
			base.dtModel.Columns.Add("CUSTOMER_FIRST_NAME",typeof(string));
			base.dtModel.Columns.Add("CUSTOMER_MIDDLE_NAME",typeof(string));
			base.dtModel.Columns.Add("CUSTOMER_LAST_NAME",typeof(string));
			base.dtModel.Columns.Add("CUSTOMER_ADDRESS1",typeof(string));
			base.dtModel.Columns.Add("CUSTOMER_ADDRESS2",typeof(string));
			base.dtModel.Columns.Add("CUSTOMER_CITY",typeof(string));		
			base.dtModel.Columns.Add("CUSTOMER_COUNTRY",typeof(string));
			base.dtModel.Columns.Add("CUSTOMER_STATE",typeof(string));
			base.dtModel.Columns.Add("CUSTOMER_ZIP",typeof(string));
		}
		#region Database schema details
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
		public string FEDERAL_ID
		{
			get
			{
				return base.dtModel.Rows[0]["FEDERAL_ID"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["FEDERAL_ID"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["FEDERAL_ID"] = value;
			}
		}
		
		public string DFI_ACC_NO
		{
			get
			{
				return base.dtModel.Rows[0]["DFI_ACC_NO"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DFI_ACC_NO"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DFI_ACC_NO"] = value;
			}
		}

		public string TRANSIT_ROUTING_NO
		{
			get
			{
				return base.dtModel.Rows[0]["TRANSIT_ROUTING_NO"] == DBNull.Value ? "" : base.dtModel.Rows[0]["TRANSIT_ROUTING_NO"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TRANSIT_ROUTING_NO"] = value;
			}
		}
		public string ACCOUNT_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["ACCOUNT_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ACCOUNT_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ACCOUNT_TYPE"] = value;
			}
		}

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
		
		public int EFT_TENTATIVE_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["EFT_TENTATIVE_DATE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["EFT_TENTATIVE_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EFT_TENTATIVE_DATE"] = value;
			}
		}

		// model for database field CARD_DATE_VALID_FROM(string)
		public string CARD_DATE_VALID_FROM
		{
			get
			{
				return base.dtModel.Rows[0]["CARD_DATE_VALID_FROM"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CARD_DATE_VALID_FROM"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CARD_DATE_VALID_FROM"] = value;
			}
		}

		// model for database field CARD_DATE_VALID_TO(string)
		public string CARD_DATE_VALID_TO
		{
			get
			{
				return base.dtModel.Rows[0]["CARD_DATE_VALID_TO"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CARD_DATE_VALID_TO"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CARD_DATE_VALID_TO"] = value;
			}
		}
		// model for database field CARD_HOLDER_NAME(string)
		public string CARD_HOLDER_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["CARD_HOLDER_NAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CARD_HOLDER_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CARD_HOLDER_NAME"] = value;
			}
		}

		// model for database field CARD_NO(string)
		public string CARD_NO
		{
			get
			{
				return base.dtModel.Rows[0]["CARD_NO"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CARD_NO"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CARD_NO"] = value;
			}
		}
		//Model CVV number varchar
		// model for database field CARD_HOLDER_NAME(string)
		public string CARD_CVV_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["CARD_CVV_NUMBER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CARD_CVV_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CARD_CVV_NUMBER"] = value;
			}
		}
		
		// model for database field CARD_TYPE(int)
		public int CARD_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["CARD_TYPE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CARD_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CARD_TYPE"] = value;
			}
		}
		//Model for Customer Info 
		public string CUSTOMER_FIRST_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_FIRST_NAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CUSTOMER_FIRST_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_FIRST_NAME"] = value;
			}
		}
		public string CUSTOMER_MIDDLE_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_MIDDLE_NAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CUSTOMER_MIDDLE_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_MIDDLE_NAME"] = value;
			}
		}
		public string CUSTOMER_LAST_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_LAST_NAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CUSTOMER_LAST_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_LAST_NAME"] = value;
			}
		}
		public string CUSTOMER_ADDRESS1
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_ADDRESS1"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CUSTOMER_ADDRESS1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_ADDRESS1"] = value;
			}
		}
		public string CUSTOMER_ADDRESS2
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_ADDRESS2"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CUSTOMER_ADDRESS2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_ADDRESS2"] = value;
			}
		}

		public string CUSTOMER_CITY
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_CITY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CUSTOMER_CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_CITY"] = value;
			}
		}

		public string CUSTOMER_COUNTRY
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_COUNTRY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CUSTOMER_COUNTRY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_COUNTRY"] = value;
			}
		}

		public string CUSTOMER_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_STATE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CUSTOMER_STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_STATE"] = value;
			}
		}

		public string CUSTOMER_ZIP
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_ZIP"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CUSTOMER_ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_ZIP"] = value;
			}
		}

		public int REVERIFIED_AC
		{
			get
			{
				return base.dtModel.Rows[0]["REVERIFIED_AC"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["REVERIFIED_AC"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REVERIFIED_AC"] = value;
			}
		}



		#endregion
	}
}
