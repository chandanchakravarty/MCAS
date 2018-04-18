/******************************************************************************************
<Author				: -   Anurag Verma
<Start Date				: -	5/9/2005 12:50:04 PM
<End Date				: -	
<Description				: - 	Models MNT_AGENCY_LIST
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
    /// Database Model for MNT_AGENCY_LIST.
    /// </summary>
    public class ClsAgencyInfo : Cms.Model.ClsCommonModel
    {
        private const string MNT_AGENCY_LIST = "MNT_AGENCY_LIST";
        public ClsAgencyInfo()
        {
            base.dtModel.TableName = "MNT_AGENCY_LIST";		// setting table name for data table that holds property values.
            this.AddColumns();								// add columns of the database table MNT_AGENCY_LIST
            base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
        }
        private void AddColumns()
        {
            base.dtModel.Columns.Add("AGENCY_ID",typeof(int));
            base.dtModel.Columns.Add("AGENCY_CODE",typeof(string));
			base.dtModel.Columns.Add("AGENCY_COMBINED_CODE",typeof(string));
			base.dtModel.Columns.Add("AGENCY_DISPLAY_NAME",typeof(string));
            base.dtModel.Columns.Add("AGENCY_DBA",typeof(string));
			base.dtModel.Columns.Add("AgencyName",typeof(string));
            base.dtModel.Columns.Add("AGENCY_LIC_NUM",typeof(int));
            base.dtModel.Columns.Add("AGENCY_ADD1",typeof(string));
            base.dtModel.Columns.Add("AGENCY_ADD2",typeof(string));
            base.dtModel.Columns.Add("AGENCY_CITY",typeof(string));
            base.dtModel.Columns.Add("AGENCY_STATE",typeof(string));
            base.dtModel.Columns.Add("AGENCY_ZIP",typeof(string));
            base.dtModel.Columns.Add("AGENCY_COUNTRY",typeof(string));
            base.dtModel.Columns.Add("AGENCY_PHONE",typeof(string));
            base.dtModel.Columns.Add("AGENCY_EXT",typeof(string));
			base.dtModel.Columns.Add("AGENCY_SPEED_DIAL",typeof(string));
            base.dtModel.Columns.Add("AGENCY_FAX",typeof(string));
            base.dtModel.Columns.Add("AGENCY_EMAIL",typeof(string));
            base.dtModel.Columns.Add("AGENCY_WEBSITE",typeof(string));
            base.dtModel.Columns.Add("AGENCY_PAYMENT_METHOD",typeof(string));
            base.dtModel.Columns.Add("AGENCY_COMMISSION",typeof(double));
            base.dtModel.Columns.Add("AGENCY_BILL_TYPE",typeof(string));
            base.dtModel.Columns.Add("AGENCY_SIGNATURES",typeof(int));

			base.dtModel.Columns.Add("PRINCIPAL_CONTACT",typeof(string));
			base.dtModel.Columns.Add("OTHER_CONTACT",typeof(string));
			base.dtModel.Columns.Add("FEDERAL_ID",typeof(string));
			base.dtModel.Columns.Add("ORIGINAL_CONTRACT_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("CURRENT_CONTRACT_DATE",typeof(DateTime));

			base.dtModel.Columns.Add("UNDERWRITER_ASSIGNED_AGENCY",typeof(string));
			
			base.dtModel.Columns.Add("ALLOWS_EFT",typeof(string));
			base.dtModel.Columns.Add("ALLOWS_CUSTOMER_SWEEP",typeof(string));

			base.dtModel.Columns.Add("BANK_ACCOUNT_NUMBER",typeof(string));
			base.dtModel.Columns.Add("BANK_ACCOUNT_NUMBER1",typeof(string));
			base.dtModel.Columns.Add("ROUTING_NUMBER",typeof(string));
			base.dtModel.Columns.Add("ROUTING_NUMBER1",typeof(string));
			base.dtModel.Columns.Add("BANK_NAME",typeof(string));
			base.dtModel.Columns.Add("BANK_BRANCH",typeof(string));

			base.dtModel.Columns.Add("BANK_NAME_2",typeof(string));
			base.dtModel.Columns.Add("BANK_BRANCH_2",typeof(string));


			base.dtModel.Columns.Add("NUM_AGENCY_CODE",typeof(int));

			base.dtModel.Columns.Add("M_AGENCY_ADD_1",typeof(string));
			base.dtModel.Columns.Add("M_AGENCY_ADD_2",typeof(string));
			base.dtModel.Columns.Add("M_AGENCY_CITY",typeof(string));
			base.dtModel.Columns.Add("M_AGENCY_STATE",typeof(string));
			base.dtModel.Columns.Add("M_AGENCY_ZIP",typeof(string));
			base.dtModel.Columns.Add("M_AGENCY_COUNTRY",typeof(string));
			base.dtModel.Columns.Add("M_AGENCY_PHONE",typeof(string));
			base.dtModel.Columns.Add("M_AGENCY_EXT",typeof(string));
			base.dtModel.Columns.Add("M_AGENCY_FAX",typeof(string));
	
			// Added by Mohit.

			base.dtModel.Columns.Add("TERMINATION_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("TERMINATION_REASON",typeof(string));
			base.dtModel.Columns.Add("NOTES",typeof(string));
			// End 
			base.dtModel.Columns.Add("TERMINATION_DATE_RENEW",typeof(DateTime));
			base.dtModel.Columns.Add("TERMINATION_NOTICE",typeof(string));
			base.dtModel.Columns.Add("INCORPORATED_LICENSE",typeof(string));
			base.dtModel.Columns.Add("ACCOUNT_TYPE",typeof(string));
			base.dtModel.Columns.Add("ACCOUNT_TYPE_2",typeof(string));
			base.dtModel.Columns.Add("PROCESS_1099",typeof(string));
			base.dtModel.Columns.Add("REVERIFIED_AC1",typeof(int));
			base.dtModel.Columns.Add("REVERIFIED_AC2",typeof(int));			
			base.dtModel.Columns.Add("REQ_SPECIAL_HANDLING",typeof(int));
            //Added by Pradeep Kushwaha
            base.dtModel.Columns.Add("SUSEP_NUMBER", typeof(string));
            base.dtModel.Columns.Add("BROKER_CURRENCY", typeof(int));
            base.dtModel.Columns.Add("AGENCY_TYPE_ID", typeof(int));
            base.dtModel.Columns.Add("BROKER_TYPE", typeof(int));
            base.dtModel.Columns.Add("DISTRICT", typeof(string));
            base.dtModel.Columns.Add("NUMBER", typeof(string));
            base.dtModel.Columns.Add("BROKER_CPF_CNPJ", typeof(string));
            base.dtModel.Columns.Add("BROKER_DATE_OF_BIRTH", typeof(DateTime));
            base.dtModel.Columns.Add("BROKER_REGIONAL_ID", typeof(string));
            base.dtModel.Columns.Add("REGIONAL_ID_ISSUANCE", typeof(string));
            base.dtModel.Columns.Add("REGIONAL_ID_ISSUE_DATE", typeof(DateTime));
            base.dtModel.Columns.Add("BROKER_BANK_NUMBER", typeof(string));
            base.dtModel.Columns.Add("MARITAL_STATUS", typeof(int));
            base.dtModel.Columns.Add("GENDER", typeof(int));


        
        }
        #region Database schema details


		// model for database field TERMINATION_DATE(DateTime)
		public DateTime TERMINATION_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["TERMINATION_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["TERMINATION_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["TERMINATION_DATE"] = value;
			}
		}
        public int BROKER_CURRENCY
        {
            get
            {
                return base.dtModel.Rows[0]["BROKER_CURRENCY"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["BROKER_CURRENCY"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["BROKER_CURRENCY"] = value;
            }
        }

		// model for database field TERMINATION_REASON(string)
		public string TERMINATION_REASON
		{
			get
			{
				return base.dtModel.Rows[0]["TERMINATION_REASON"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["TERMINATION_REASON"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TERMINATION_REASON"] = value;
			}
		}

		// model for database field ORIGINAL_CONTRACT_DATE(DateTime)
		public DateTime ORIGINAL_CONTRACT_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["ORIGINAL_CONTRACT_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["ORIGINAL_CONTRACT_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ORIGINAL_CONTRACT_DATE"] = value;
			}
		}
		
		// model for database field ORIGINAL_CONTRACT_DATE(DateTime)
		public DateTime CURRENT_CONTRACT_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["CURRENT_CONTRACT_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["CURRENT_CONTRACT_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CURRENT_CONTRACT_DATE"] = value;
			}
		}


		// model for database field ROUTING_NUMBER(string)
		public string ROUTING_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["ROUTING_NUMBER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ROUTING_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ROUTING_NUMBER"] = value;
			}
		}
		// model for database field BANK_ACCOUNT_NUMBER(string)
		public string BANK_ACCOUNT_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["BANK_ACCOUNT_NUMBER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["BANK_ACCOUNT_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BANK_ACCOUNT_NUMBER"] = value;
			}
		}
		// model for database field ROUTING_NUMBER1(string)
		public string ROUTING_NUMBER1
		{
			get
			{
				return base.dtModel.Rows[0]["ROUTING_NUMBER1"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ROUTING_NUMBER1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ROUTING_NUMBER1"] = value;
			}
		}
		// model for database field BANK_ACCOUNT_NUMBER1(string)
		public string BANK_ACCOUNT_NUMBER1
		{
			get
			{
				return base.dtModel.Rows[0]["BANK_ACCOUNT_NUMBER1"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["BANK_ACCOUNT_NUMBER1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BANK_ACCOUNT_NUMBER1"] = value;
			}
		}

		// model for database field UNDERWRITER_ASSIGNED_AGENCY(string)
		public string UNDERWRITER_ASSIGNED_AGENCY
		{
			get
			{
				return base.dtModel.Rows[0]["UNDERWRITER_ASSIGNED_AGENCY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["UNDERWRITER_ASSIGNED_AGENCY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["UNDERWRITER_ASSIGNED_AGENCY"] = value;
			}
		}
		//model for database field ALLOWS_EFT(string)
		public int ALLOWS_EFT
		{
			get
			{
				return base.dtModel.Rows[0]["ALLOWS_EFT"] == DBNull.Value ? Convert.ToInt32(null) : Convert.ToInt32(base.dtModel.Rows[0]["ALLOWS_EFT"]);
			}
			set
			{
				base.dtModel.Rows[0]["ALLOWS_EFT"] = value;
			}
		}
        public int BROKER_TYPE
        {
            get
            {
                return base.dtModel.Rows[0]["BROKER_TYPE"] == DBNull.Value ? Convert.ToInt32(null) : Convert.ToInt32(base.dtModel.Rows[0]["BROKER_TYPE"]);
            }
            set
            {
                base.dtModel.Rows[0]["BROKER_TYPE"] = value;
            }
        }
		//Model object for Customer Sweep
		public int ALLOWS_CUSTOMER_SWEEP
		{
			get
			{
				return base.dtModel.Rows[0]["ALLOWS_CUSTOMER_SWEEP"] == DBNull.Value ? Convert.ToInt32(null) : Convert.ToInt32(base.dtModel.Rows[0]["ALLOWS_CUSTOMER_SWEEP"]);
			}
			set
			{
				base.dtModel.Rows[0]["ALLOWS_CUSTOMER_SWEEP"] = value;
			}
		}
		// model for database field BANK_NAME(string)
		public string BANK_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["BANK_NAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["BANK_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BANK_NAME"] = value;
			}
		}

		// model for database field BANK_BRANCH(string)
		public string BANK_BRANCH
		{
			get
			{
				return base.dtModel.Rows[0]["BANK_BRANCH"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["BANK_BRANCH"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BANK_BRANCH"] = value;
			}
		}

		// model for database field BANK_NAME(string)
		public string BANK_NAME_2
		{
			get
			{
				return base.dtModel.Rows[0]["BANK_NAME_2"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["BANK_NAME_2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BANK_NAME_2"] = value;
			}
		}

		// model for database field BANK_BRANCH(string)
		public string BANK_BRANCH_2
		{
			get
			{
				return base.dtModel.Rows[0]["BANK_BRANCH_2"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["BANK_BRANCH_2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BANK_BRANCH_2"] = value;
			}
		}

		// model for database field FEDERAL_ID(string)
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

		
		// model for database field OTHER_CONTACT(string)
		public string OTHER_CONTACT
		{
			get
			{
				return base.dtModel.Rows[0]["OTHER_CONTACT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["OTHER_CONTACT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["OTHER_CONTACT"] = value;
			}
		}


		// model for database field PRINCIPAL_CONTACT(string)
		public string PRINCIPAL_CONTACT
		{
			get
			{
				return base.dtModel.Rows[0]["PRINCIPAL_CONTACT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PRINCIPAL_CONTACT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PRINCIPAL_CONTACT"] = value;
			}
		}


        // model for database field AGENCY_ID(int)
        public int AGENCY_ID
        {
            get
            {
                return base.dtModel.Rows[0]["AGENCY_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["AGENCY_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["AGENCY_ID"] = value;
            }
        }
        // model for database field AGENCY_CODE(string)
        public string AGENCY_CODE
        {
            get
            {
                return base.dtModel.Rows[0]["AGENCY_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["AGENCY_CODE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["AGENCY_CODE"] = value;
            }
        }
		// model for database field AGENCY_COMBINED_CODE(string)
		public string AGENCY_COMBINED_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["AGENCY_COMBINED_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["AGENCY_COMBINED_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["AGENCY_COMBINED_CODE"] = value;
			}
		}
        // model for database field AGENCY_DISPLAY_NAME(string)
        public string AGENCY_DISPLAY_NAME
        {
            get
            {
                return base.dtModel.Rows[0]["AGENCY_DISPLAY_NAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["AGENCY_DISPLAY_NAME"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["AGENCY_DISPLAY_NAME"] = value;
            }
        }
		// model for database field AGENCY_DBA(string)
		public string AGENCY_DBA
		{
			get
			{
				return base.dtModel.Rows[0]["AGENCY_DBA"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["AGENCY_DBA"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["AGENCY_DBA"] = value;
			}
		}

		

			// model for database field AGENCY_DBA(string)
			public string AgencyName
			{
				get
				{
					return base.dtModel.Rows[0]["AgencyName"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["AgencyName"].ToString();
				}
				set
				{
					base.dtModel.Rows[0]["AgencyName"] = value;
				}
			}

        // model for database field AGENCY_LIC_NUM(int)
        public int AGENCY_LIC_NUM
        {
            get
            {
                return base.dtModel.Rows[0]["AGENCY_LIC_NUM"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["AGENCY_LIC_NUM"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["AGENCY_LIC_NUM"] = value;
            }
        }
        // model for database field AGENCY_ADD1(string)
        public string AGENCY_ADD1
        {
            get
            {
                return base.dtModel.Rows[0]["AGENCY_ADD1"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["AGENCY_ADD1"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["AGENCY_ADD1"] = value;
            }
        }
        // model for database field AGENCY_ADD2(string)
        public string AGENCY_ADD2
        {
            get
            {
                return base.dtModel.Rows[0]["AGENCY_ADD2"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["AGENCY_ADD2"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["AGENCY_ADD2"] = value;
            }
        }
        // model for database field AGENCY_CITY(string)
        public string AGENCY_CITY
        {
            get
            {
                return base.dtModel.Rows[0]["AGENCY_CITY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["AGENCY_CITY"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["AGENCY_CITY"] = value;
            }
        }
        // model for database field AGENCY_STATE(string)
        public string AGENCY_STATE
        {
            get
            {
                return base.dtModel.Rows[0]["AGENCY_STATE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["AGENCY_STATE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["AGENCY_STATE"] = value;
            }
        }
        // model for database field AGENCY_ZIP(string)
        public string AGENCY_ZIP
        {
            get
            {
                return base.dtModel.Rows[0]["AGENCY_ZIP"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["AGENCY_ZIP"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["AGENCY_ZIP"] = value;
            }
        }
        // model for database field AGENCY_COUNTRY(string)
        public string AGENCY_COUNTRY
        {
            get
            {
                return base.dtModel.Rows[0]["AGENCY_COUNTRY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["AGENCY_COUNTRY"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["AGENCY_COUNTRY"] = value;
            }
        }
        // model for database field AGENCY_PHONE(string)
        public string AGENCY_PHONE
        {
            get
            {
                return base.dtModel.Rows[0]["AGENCY_PHONE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["AGENCY_PHONE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["AGENCY_PHONE"] = value;
            }
        }
        // model for database field AGENCY_EXT(string)
        public string AGENCY_EXT
        {
            get
            {
                return base.dtModel.Rows[0]["AGENCY_EXT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["AGENCY_EXT"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["AGENCY_EXT"] = value;
            }
        }
        // model for database field AGENCY_FAX(string)
        public string AGENCY_FAX
        {
            get
            {
                return base.dtModel.Rows[0]["AGENCY_FAX"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["AGENCY_FAX"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["AGENCY_FAX"] = value;
            }
        }
		// model for database field AGENCY_SPEED_DIAL(string)
		public string AGENCY_SPEED_DIAL
		{
			get
			{
				return base.dtModel.Rows[0]["AGENCY_SPEED_DIAL"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["AGENCY_SPEED_DIAL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["AGENCY_SPEED_DIAL"] = value;
			}
		}
        // model for database field AGENCY_EMAIL(string)
        public string AGENCY_EMAIL
        {
            get
            {
                return base.dtModel.Rows[0]["AGENCY_EMAIL"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["AGENCY_EMAIL"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["AGENCY_EMAIL"] = value;
            }
        }
        // model for database field AGENCY_WEBSITE(string)
        public string AGENCY_WEBSITE
        {
            get
            {
                return base.dtModel.Rows[0]["AGENCY_WEBSITE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["AGENCY_WEBSITE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["AGENCY_WEBSITE"] = value;
            }
        }
        // model for database field AGENCY_PAYMENT_METHOD(string)
        public string AGENCY_PAYMENT_METHOD
        {
            get
            {
                return base.dtModel.Rows[0]["AGENCY_PAYMENT_METHOD"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["AGENCY_PAYMENT_METHOD"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["AGENCY_PAYMENT_METHOD"] = value;
            }
        }
        // model for database field AGENCY_COMMISSION(double)
        public double AGENCY_COMMISSION
        {
            get
            {
                return base.dtModel.Rows[0]["AGENCY_COMMISSION"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["AGENCY_COMMISSION"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["AGENCY_COMMISSION"] = value;
            }
        }
        // model for database field AGENCY_BILL_TYPE(string)
        public string AGENCY_BILL_TYPE
        {
            get
            {
                return base.dtModel.Rows[0]["AGENCY_BILL_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["AGENCY_BILL_TYPE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["AGENCY_BILL_TYPE"] = value;
            }
        }
        // model for database field AGENCY_SIGNATURES(int)
        public int AGENCY_SIGNATURES
        {
            get
            {
                return base.dtModel.Rows[0]["AGENCY_SIGNATURES"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["AGENCY_SIGNATURES"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["AGENCY_SIGNATURES"] = value;
            }
        }

		// model for database field M_AGENCY_ADD_1(string)
		public string M_AGENCY_ADD_1
		{
			get
			{
				return base.dtModel.Rows[0]["M_AGENCY_ADD_1"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["M_AGENCY_ADD_1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["M_AGENCY_ADD_1"] = value;
			}
		}
		// model for database field AGENCY_ADD2(string)
		public string M_AGENCY_ADD_2
		{
			get
			{
				return base.dtModel.Rows[0]["M_AGENCY_ADD_2"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["M_AGENCY_ADD_2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["M_AGENCY_ADD_2"] = value;
			}
		}
		// model for database field M_AGENCY_CITY(string)
		public string M_AGENCY_CITY
		{
			get
			{
				return base.dtModel.Rows[0]["M_AGENCY_CITY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["M_AGENCY_CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["M_AGENCY_CITY"] = value;
			}
		}
		// model for database field M_AGENCY_STATE(string)
		public string M_AGENCY_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["M_AGENCY_STATE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["M_AGENCY_STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["M_AGENCY_STATE"] = value;
			}
		}
		// model for database field M_AGENCY_ZIP(string)
		public string M_AGENCY_ZIP
		{
			get
			{
				return base.dtModel.Rows[0]["M_AGENCY_ZIP"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["M_AGENCY_ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["M_AGENCY_ZIP"] = value;
			}
		}
		// model for database field M_AGENCY_COUNTRY(string)
		public string M_AGENCY_COUNTRY
		{
			get
			{
				return base.dtModel.Rows[0]["M_AGENCY_COUNTRY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["M_AGENCY_COUNTRY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["M_AGENCY_COUNTRY"] = value;
			}
		}
		// model for database field M_AGENCY_PHONE(string)
		public string M_AGENCY_PHONE
		{
			get
			{
				return base.dtModel.Rows[0]["M_AGENCY_PHONE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["M_AGENCY_PHONE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["M_AGENCY_PHONE"] = value;
			}
		}
		// model for database field M_AGENCY_EXT(string)
		public string M_AGENCY_EXT
		{
			get
			{
				return base.dtModel.Rows[0]["M_AGENCY_EXT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["M_AGENCY_EXT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["M_AGENCY_EXT"] = value;
			}
		}
		// model for database field M_AGENCY_FAX(string)
		public string M_AGENCY_FAX
		{
			get
			{
				return base.dtModel.Rows[0]["M_AGENCY_FAX"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["M_AGENCY_FAX"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["M_AGENCY_FAX"] = value;
			}
		}
		// model for database field NOTES(string)
		public string NOTES
		{
			get
			{
				return base.dtModel.Rows[0]["NOTES"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["NOTES"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["NOTES"] = value;
			}
		}
		// model for database field NUM_AGENCY_CODE(int)
		public int NUM_AGENCY_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["NUM_AGENCY_CODE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["NUM_AGENCY_CODE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NUM_AGENCY_CODE"] = value;
			}
		}
		// model for database field TERMINATION_DATE_RENEW(DateTime)
		public DateTime TERMINATION_DATE_RENEW
		{
			get
			{
				return base.dtModel.Rows[0]["TERMINATION_DATE_RENEW"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["TERMINATION_DATE_RENEW"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["TERMINATION_DATE_RENEW"] = value;
			}
		}
        // model for database field TERMINATION_NOTICE(string)
		public string TERMINATION_NOTICE
		{
			get
			{
				return base.dtModel.Rows[0]["TERMINATION_NOTICE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["TERMINATION_NOTICE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TERMINATION_NOTICE"] = value;
			}
		}
		// model for database field TERMINATION_NOTICE(string)
		public string INCORPORATED_LICENSE
		{
			get
			{
				return base.dtModel.Rows[0]["INCORPORATED_LICENSE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["INCORPORATED_LICENSE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["INCORPORATED_LICENSE"] = value;
			}
		}

		// model for database field ACCOUNT_TYPE(string)
		public string ACCOUNT_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["ACCOUNT_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ACCOUNT_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ACCOUNT_TYPE"] = value;
			}
		}

		// model for database field ACCOUNT_TYPE(string)
		public string ACCOUNT_TYPE_2
		{
			get
			{
				return base.dtModel.Rows[0]["ACCOUNT_TYPE_2"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ACCOUNT_TYPE_2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ACCOUNT_TYPE_2"] = value;
			}
		}

		// model for database field PROCESS_1099(string)
		public int PROCESS_1099
		{
			get
			{
				return base.dtModel.Rows[0]["PROCESS_1099"] == DBNull.Value ? Convert.ToInt32(null) : Convert.ToInt32(base.dtModel.Rows[0]["PROCESS_1099"]);
			}
			set
			{
				base.dtModel.Rows[0]["PROCESS_1099"] = value;
			}
		}
		//Reverify Options
		public int REVERIFIED_AC1
		{
			get
			{
				return base.dtModel.Rows[0]["REVERIFIED_AC1"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["REVERIFIED_AC1"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REVERIFIED_AC1"] = value;
			}
		}
		public int REVERIFIED_AC2
		{
			get
			{
				return base.dtModel.Rows[0]["REVERIFIED_AC2"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["REVERIFIED_AC2"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REVERIFIED_AC2"] = value;
			}
		}
		//Added by Raghav For Itrack #Issue 4810
		public int REQ_SPECIAL_HANDLING
		{
			get
			{
				return base.dtModel.Rows[0]["REQ_SPECIAL_HANDLING"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["REQ_SPECIAL_HANDLING"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REQ_SPECIAL_HANDLING"] = value;
			}
		}

        // model for database field SUSEP_NUMBER(string)
        public string SUSEP_NUMBER
        {
            get
            {
                return base.dtModel.Rows[0]["SUSEP_NUMBER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SUSEP_NUMBER"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["SUSEP_NUMBER"] = value;
            }
        }
        public string DISTRICT
        {
            get
            {
                return base.dtModel.Rows[0]["DISTRICT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DISTRICT"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["DISTRICT"] = value;
            }
        }
        public string NUMBER
        {
            get
            {
                return base.dtModel.Rows[0]["NUMBER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["NUMBER"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["NUMBER"] = value;
            }
        }
        public string BROKER_CPF_CNPJ
        {
            get
            {
                return base.dtModel.Rows[0]["BROKER_CPF_CNPJ"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["BROKER_CPF_CNPJ"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["BROKER_CPF_CNPJ"] = value;
            }
        }



        //public string BROKER_DATE_OF_BIRTH
        //{
        //    get
        //    {
        //        return base.dtModel.Rows[0]["BROKER_DATE_OF_BIRTH"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["BROKER_DATE_OF_BIRTH"].ToString();
        //    }
        //    set
        //    {
        //        base.dtModel.Rows[0]["BROKER_DATE_OF_BIRTH"] = value;
        //    }
        //}

        public DateTime BROKER_DATE_OF_BIRTH
        {
            get
            {
                return base.dtModel.Rows[0]["BROKER_DATE_OF_BIRTH"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["BROKER_DATE_OF_BIRTH"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["BROKER_DATE_OF_BIRTH"] = value;
            }
        }

        // model for database field BROKER_DATE_OF_BIRTH(DateTime)
        public string BROKER_REGIONAL_ID
        {
            get
            {
                return base.dtModel.Rows[0]["BROKER_REGIONAL_ID"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["BROKER_REGIONAL_ID"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["BROKER_REGIONAL_ID"] = value;
            }
        }
        public string REGIONAL_ID_ISSUANCE
        {
            get
            {
                return base.dtModel.Rows[0]["REGIONAL_ID_ISSUANCE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["REGIONAL_ID_ISSUANCE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["REGIONAL_ID_ISSUANCE"] = value;
            }
        }
        //public string REGIONAL_ID_ISSUE_DATE
        //{
        //    get
        //    {
        //        return base.dtModel.Rows[0]["REGIONAL_ID_ISSUE_DATE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["REGIONAL_ID_ISSUE_DATE"].ToString();
        //    }
        //    set
        //    {
        //        base.dtModel.Rows[0]["REGIONAL_ID_ISSUE_DATE"] = value;
        //    }
        //}
        public DateTime REGIONAL_ID_ISSUE_DATE
        {
            get
            {
                return base.dtModel.Rows[0]["REGIONAL_ID_ISSUE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["REGIONAL_ID_ISSUE_DATE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["REGIONAL_ID_ISSUE_DATE"] = value;
            }
        }

        // model for database field REGIONAL_ID_ISSUE_DATE(DateTime)

        public string BROKER_BANK_NUMBER
        {
            get
            {
                return base.dtModel.Rows[0]["BROKER_BANK_NUMBER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["BROKER_BANK_NUMBER"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["BROKER_BANK_NUMBER"] = value;
            }
        }

        // model for database field AGENCY_TYPE_ID(int)
        public int AGENCY_TYPE_ID
        {
            get
            {
                return base.dtModel.Rows[0]["AGENCY_TYPE_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["AGENCY_TYPE_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["AGENCY_TYPE_ID"] = value;
            }
        }


        public int MARITAL_STATUS
        {
            get
            {
                return base.dtModel.Rows[0]["MARITAL_STATUS"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["MARITAL_STATUS"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["MARITAL_STATUS"] = value;
            }
        }
        public int GENDER
        {
            get
            {
                return base.dtModel.Rows[0]["GENDER"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["GENDER"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["GENDER"] = value;
            }
        }
		


		#endregion
		
    }
}
