/******************************************************************************************
<Author					: -	Shrikant Bhatt
<Start Date				: -	4/27/2005 12:49:57 PM
<End Date				: -	
<Description			: - 	
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
	/// Database Model for APP_ADD_OTHER_INT.
	/// </summary>
	public class ClsAdditionalInterestInfo : Cms.Model.ClsCommonModel
	{
		private const string APP_ADD_OTHER_INT = "APP_ADD_OTHER_INT";
		public ClsAdditionalInterestInfo()
		{
			base.dtModel.TableName = "APP_ADD_OTHER_INT";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table APP_ADD_OTHER_INT
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("APP_ID",typeof(int));
			base.dtModel.Columns.Add("APP_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("HOLDER_ID",typeof(int));

			base.dtModel.Columns.Add("VEHICLE_ID",typeof(int));
			base.dtModel.Columns.Add("DWELLING_ID",typeof(int));
			base.dtModel.Columns.Add("BOAT_ID",typeof(int));
			base.dtModel.Columns.Add("TRAILER_ID",typeof(int));
			base.dtModel.Columns.Add("ENGINE_ID",typeof(int));
			base.dtModel.Columns.Add("RISK_ID",typeof(int));

			base.dtModel.Columns.Add("ADD_INT_ID",typeof(int));
            base.dtModel.Columns.Add("MEMO",typeof(string));
			base.dtModel.Columns.Add("NATURE_OF_INTEREST",typeof(string));
			base.dtModel.Columns.Add("RANK",typeof(int));
			base.dtModel.Columns.Add("LOAN_REF_NUMBER",typeof(string));
			base.dtModel.Columns.Add("CERTIFICATE_REQUIRED",typeof(string));
			base.dtModel.Columns.Add("HOLDER_ADD1",typeof(string));
			base.dtModel.Columns.Add("HOLDER_ADD2",typeof(string));
			base.dtModel.Columns.Add("HOLDER_CITY",typeof(string));
			base.dtModel.Columns.Add("HOLDER_COUNTRY",typeof(string));
			base.dtModel.Columns.Add("HOLDER_STATE",typeof(string));
			base.dtModel.Columns.Add("HOLDER_ZIP",typeof(string));
			base.dtModel.Columns.Add("HOLDER_NAME",typeof(string));
			base.dtModel.Columns.Add("BILL_MORTAGAGEE",typeof(int));
		}
		#region Database schema details
		// model for database field RISK_ID(int)
		public int RISK_ID
		{
			get
			{
				return base.dtModel.Rows[0]["RISK_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["RISK_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RISK_ID"] = value;
			}
		}


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

		public int ADD_INT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ADD_INT_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ADD_INT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ADD_INT_ID"] = value;
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
		// model for database field HOLDER_ID(int)
		public int HOLDER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["HOLDER_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["HOLDER_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["HOLDER_ID"] = value;
			}
		}

		// model for database field HOLDER_NAME(string)
		public string HOLDER_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["HOLDER_NAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HOLDER_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HOLDER_NAME"] = value;
			}
		}

		// model for database field HOLDER_ADD1(string)
		public string HOLDER_ADD1
		{
			get
			{
				return base.dtModel.Rows[0]["HOLDER_ADD1"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HOLDER_ADD1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HOLDER_ADD1"] = value;
			}
		}
		// model for database field HOLDER_ADD2(string)
		public string HOLDER_ADD2
		{
			get
			{
				return base.dtModel.Rows[0]["HOLDER_ADD2"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HOLDER_ADD2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HOLDER_ADD2"] = value;
			}
		}
		// model for database field HOLDER_CITY(string)
		public string HOLDER_CITY
		{
			get
			{
				return base.dtModel.Rows[0]["HOLDER_CITY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HOLDER_CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HOLDER_CITY"] = value;
			}
		}
		// model for database field HOLDER_COUNTRY(string)
		public string HOLDER_COUNTRY
		{
			get
			{
				return base.dtModel.Rows[0]["HOLDER_COUNTRY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HOLDER_COUNTRY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HOLDER_COUNTRY"] = value;
			}
		}
		// model for database field HOLDER_STATE(string)
		public string HOLDER_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["HOLDER_STATE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HOLDER_STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HOLDER_STATE"] = value;
			}
		}
		// model for database field HOLDER_ZIP(string)
		public string HOLDER_ZIP
		{
			get
			{
				return base.dtModel.Rows[0]["HOLDER_ZIP"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HOLDER_ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HOLDER_ZIP"] = value;
			}
		}

		// model for database field VEHICLE_ID(int)
		public int VEHICLE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["VEHICLE_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["VEHICLE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["VEHICLE_ID"] = value;
			}
		}
		// model for database field VEHICLE_ID(int)
		public int DWELLING_ID
		{
			get
			{
				return base.dtModel.Rows[0]["DWELLING_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DWELLING_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DWELLING_ID"] = value;
			}
		}

        // model for database field BOAT_ID(int)
        public int BOAT_ID
        {
            get
            {
                return base.dtModel.Rows[0]["BOAT_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["BOAT_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["BOAT_ID"] = value;
            }
        }
        // model for database field TRAILER_ID(int)
        public int TRAILER_ID
        {
            get
            {
                return base.dtModel.Rows[0]["TRAILER_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["TRAILER_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["TRAILER_ID"] = value;
            }
        }

        // model for database field ENGINE_ID(int)
        public int ENGINE_ID
        {
            get
            {
                return base.dtModel.Rows[0]["ENGINE_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["ENGINE_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["ENGINE_ID"] = value;
            }
        }

		// model for database field MEMO(string)
		public string MEMO
		{
			get
			{
				return base.dtModel.Rows[0]["MEMO"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MEMO"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MEMO"] = value;
			}
		}
		// model for database field NATURE_OF_INTEREST(string)
		public string NATURE_OF_INTEREST
		{
			get
			{
				return base.dtModel.Rows[0]["NATURE_OF_INTEREST"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["NATURE_OF_INTEREST"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["NATURE_OF_INTEREST"] = value;
			}
		}
		// model for database field RANK(int)
		public int RANK
		{
			get
			{
				return base.dtModel.Rows[0]["RANK"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["RANK"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RANK"] = value;
			}
		}
		// model for database field LOAN_REF_NUMBER(string)
		public string LOAN_REF_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["LOAN_REF_NUMBER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LOAN_REF_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOAN_REF_NUMBER"] = value;
			}
		}

		
			// model for database field CERTIFICATE_REQUIRED(string)
			public string CERTIFICATE_REQUIRED
			{
				get
				{
					return base.dtModel.Rows[0]["CERTIFICATE_REQUIRED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CERTIFICATE_REQUIRED"].ToString();
				}
				set
				{
					base.dtModel.Rows[0]["CERTIFICATE_REQUIRED"] = value;
				}
			}
		public int BILL_MORTAGAGEE
		{
			get
			{
				return base.dtModel.Rows[0]["BILL_MORTAGAGEE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["BILL_MORTAGAGEE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["BILL_MORTAGAGEE"] = value;
			}
		}
		#endregion
	}
}

