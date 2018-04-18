/******************************************************************************************
Modification History
<Modified Date			: - 07/10/2005
<Modified By			: - Sumit Chhabra
<Purpose				: - Added the keyword base and this to relavent methods.

*******************************************************************************************/ 
using System;
using System.Data;
using Cms.Model;

namespace Cms.Model.Client
{
	/// <summary>
	/// Summary description for ClsClientInfo.
	/// </summary>
	public class ClsCustomerInfo : ClsCommonModel
	{
		private const string CLIENTTABLE = "CLT_CUSTOMER_LIST";
		public ClsCustomerInfo()
		{
			base.dtModel.TableName = CLIENTTABLE;
			this.AddColumns();							// add columns of the database table CLT_CUSTOMER_LIST
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}

        private void AddColumns()
        {
            base.dtModel.Columns.Add("CUSTOMER_ID", typeof(int));
            base.dtModel.Columns.Add("CUSTOMER_CODE", typeof(string));
            
            base.dtModel.Columns.Add("CUSTOMER_TYPE", typeof(string));
            
           
            base.dtModel.Columns.Add("CUSTOMER_PARENT", typeof(int));
            base.dtModel.Columns.Add("CUSTOMER_SUFFIX", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_FIRST_NAME", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_MIDDLE_NAME", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_LAST_NAME", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_ADDRESS1", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_ADDRESS2", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_CITY", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_COUNTRY", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_STATE", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_STATE_CODE", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_ZIP", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_BUSINESS_TYPE", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_BUSINESS_DESC", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_CONTACT_NAME", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_BUSINESS_PHONE", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_EXT", typeof(string));
            base.dtModel.Columns.Add("EMP_EXT", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_HOME_PHONE", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_MOBILE", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_FAX", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_PAGER_NO", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_Email", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_WEBSITE", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_INSURANCE_SCORE", typeof(double));
            base.dtModel.Columns.Add("CUSTOMER_INSURANCE_RECEIVED_DATE", typeof(DateTime));
            base.dtModel.Columns.Add("CUSTOMER_REASON_CODE", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_REASON_CODE2", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_REASON_CODE3", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_REASON_CODE4", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_PRODUCER_ID", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_ACCOUNT_EXECUTIVE_ID", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_CSR", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_LATE_CHARGES", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_LATE_NOTICES", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_SEND_STATEMENT", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_RECEIVABLE_DUE_DAYS", typeof(int));
            base.dtModel.Columns.Add("CUSTOMER_REFERRED_BY", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_AGENCY_ID", typeof(int));
            base.dtModel.Columns.Add("CUSTOMER_ATTENTION_NOTE", typeof(string));
            base.dtModel.Columns.Add("PREFIX", typeof(int));
            base.dtModel.Columns.Add("PER_CUST_MOBILE", typeof(string));
            base.dtModel.Columns.Add("ATTENTION_NOTE_UPDATED", typeof(DateTime));
            base.dtModel.Columns.Add("LAST_INSURANCE_SCORE_FETCHED", typeof(DateTime));

            base.dtModel.Columns.Add("APPLICANT_OCCU", typeof(string));
            base.dtModel.Columns.Add("EMPLOYER_NAME", typeof(string));
            base.dtModel.Columns.Add("EMPLOYER_ADDRESS", typeof(string));
            base.dtModel.Columns.Add("YEARS_WITH_CURR_EMPL", typeof(double));
            base.dtModel.Columns.Add("MARITAL_STATUS", typeof(string));
            base.dtModel.Columns.Add("GENDER", typeof(string));
            base.dtModel.Columns.Add("SSN_NO", typeof(string));
            base.dtModel.Columns.Add("DATE_OF_BIRTH", typeof(DateTime));

            base.dtModel.Columns.Add("FACTOR1", typeof(string));
            base.dtModel.Columns.Add("FACTOR2", typeof(string));
            base.dtModel.Columns.Add("FACTOR3", typeof(string));
            base.dtModel.Columns.Add("FACTOR4", typeof(string));

            // added by mohit on 4/11/2005
            base.dtModel.Columns.Add("DESC_APPLICANT_OCCU", typeof(string));
            // end 
            //Added by Sumit on 10/04/2006
            base.dtModel.Columns.Add("EMPLOYER_ADD1", typeof(string));
            base.dtModel.Columns.Add("EMPLOYER_ADD2", typeof(string));
            base.dtModel.Columns.Add("EMPLOYER_CITY", typeof(string));
            base.dtModel.Columns.Add("EMPLOYER_COUNTRY", typeof(string));
            base.dtModel.Columns.Add("EMPLOYER_STATE", typeof(string));
            base.dtModel.Columns.Add("EMPLOYER_ZIPCODE", typeof(string));
            base.dtModel.Columns.Add("EMPLOYER_HOMEPHONE", typeof(string));
            base.dtModel.Columns.Add("EMPLOYER_EMAIL", typeof(string));
            base.dtModel.Columns.Add("YEARS_WITH_CURR_OCCU", typeof(double));

            //--------Start Addded By Lalit on March 11,2010
            base.dtModel.Columns.Add("CPF_CNPJ", typeof(string));
            base.dtModel.Columns.Add("NUMBER", typeof(string));
            
            base.dtModel.Columns.Add("DISTRICT", typeof(string));
            
            base.dtModel.Columns.Add("MAIN_TITLE", typeof(int));
            base.dtModel.Columns.Add("MAIN_POSITION", typeof(int));
            base.dtModel.Columns.Add("MAIN_CPF_CNPJ", typeof(string));
            base.dtModel.Columns.Add("MAIN_ADDRESS", typeof(string));
            base.dtModel.Columns.Add("MAIN_NUMBER", typeof(string));
            base.dtModel.Columns.Add("MAIN_COMPLIMENT", typeof(string));
            base.dtModel.Columns.Add("MAIN_DISTRICT", typeof(string));
            base.dtModel.Columns.Add("MAIN_NOTE", typeof(string));
            base.dtModel.Columns.Add("MAIN_CONTACT_CODE", typeof(string));
            base.dtModel.Columns.Add("REGIONAL_IDENTIFICATION", typeof(string));
            base.dtModel.Columns.Add("REG_ID_ISSUE", typeof(DateTime));
            base.dtModel.Columns.Add("ORIGINAL_ISSUE", typeof(string));
            base.dtModel.Columns.Add("MAIN_ZIPCODE", typeof(string));
            base.dtModel.Columns.Add("MAIN_STATE", typeof(int));
            base.dtModel.Columns.Add("MAIN_COUNTRY", typeof(int));
            base.dtModel.Columns.Add("MAIN_CITY", typeof(string));
            base.dtModel.Columns.Add("MAIN_FIRST_NAME", typeof(string));
            base.dtModel.Columns.Add("MAIN_MIDDLE_NAME", typeof(string));
            base.dtModel.Columns.Add("MAIN_LAST_NAME", typeof(string));
            base.dtModel.Columns.Add("MONTHLY_INCOME", typeof(double));
            base.dtModel.Columns.Add("NET_ASSETS_AMOUNT", typeof(double));
            base.dtModel.Columns.Add("REGIONAL_IDENTIFICATION_TYPE", typeof(string));
            base.dtModel.Columns.Add("NATIONALITY", typeof(string));
            base.dtModel.Columns.Add("EMAIL_ADDRESS", typeof(string));
            base.dtModel.Columns.Add("ID_TYPE", typeof(string));
            base.dtModel.Columns.Add("CADEMP", typeof(string));
            base.dtModel.Columns.Add("AMOUNT_TYPE", typeof(string));
            base.dtModel.Columns.Add("IS_POLITICALLY_EXPOSED", typeof(string));
            base.dtModel.Columns.Add("ACCOUNT_TYPE", typeof(int));
            base.dtModel.Columns.Add("BANK_BRANCH", typeof(string));
            base.dtModel.Columns.Add("BANK_NUMBER", typeof(string));
            base.dtModel.Columns.Add("BANK_NAME", typeof(string));
            base.dtModel.Columns.Add("ACCOUNT_NUMBER", typeof(string));

            //-------End Added

            /*base.dtModel.Columns.Add("ALT_CUSTOMER_ADDRESS1",typeof(string));
            base.dtModel.Columns.Add("ALT_CUSTOMER_ADDRESS2",typeof(string));
            base.dtModel.Columns.Add("ALT_CUSTOMER_CITY",typeof(string));
            base.dtModel.Columns.Add("ALT_CUSTOMER_COUNTRY",typeof(string));
            base.dtModel.Columns.Add("ALT_CUSTOMER_STATE",typeof(string));
            base.dtModel.Columns.Add("ALT_CUSTOMER_STATE_CODE",typeof(string));
            base.dtModel.Columns.Add("ALT_CUSTOMER_ZIP",typeof(string));*/

        }

		
		#region Database schema details
		
		// model for database field DESC_APPLICANT_OCCU(string)
		public string DESC_APPLICANT_OCCU
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_APPLICANT_OCCU"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_APPLICANT_OCCU"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_APPLICANT_OCCU"] = value;
			}
		}

		public DateTime DATE_OF_BIRTH
		{
			get
			{
				return base.dtModel.Rows[0]["DATE_OF_BIRTH"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["DATE_OF_BIRTH"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DATE_OF_BIRTH"] = value;
			}
		}
		// model for database field MARITAL_STATUS(string)
		public string MARITAL_STATUS
		{
			get
			{
				return base.dtModel.Rows[0]["MARITAL_STATUS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MARITAL_STATUS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MARITAL_STATUS"] = value;
			}
		}
		// model for database field GENDER(string)
		public string GENDER
		{
			get
			{
				return base.dtModel.Rows[0]["GENDER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["GENDER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["GENDER"] = value;
			}
		}
		// model for database field SSN_NO(string)
		public string SSN_NO
		{
			get
			{
				return base.dtModel.Rows[0]["SSN_NO"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SSN_NO"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SSN_NO"] = value;
			}
		}
		// model for database field APPLICANT_OCCU(string)
		public string APPLICANT_OCCU
		{
			get
			{
				return base.dtModel.Rows[0]["APPLICANT_OCCU"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["APPLICANT_OCCU"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["APPLICANT_OCCU"] = value;
			}
		}
		// model for database field EMPLOYER_NAME(string)
		public string EMPLOYER_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["EMPLOYER_NAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["EMPLOYER_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EMPLOYER_NAME"] = value;
			}
		}
		// model for database field EMPLOYER_ADDRESS(string)
		public string EMPLOYER_ADDRESS
		{
			get
			{
				return base.dtModel.Rows[0]["EMPLOYER_ADDRESS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["EMPLOYER_ADDRESS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EMPLOYER_ADDRESS"] = value;
			}
		}
		// model for database field YEARS_WITH_CURR_EMPL(double)
		public double YEARS_WITH_CURR_EMPL
		{
			get
			{
				return base.dtModel.Rows[0]["YEARS_WITH_CURR_EMPL"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["YEARS_WITH_CURR_EMPL"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["YEARS_WITH_CURR_EMPL"] = value;
			}
		}
		// model for database field CUSTOMER_ID(int)
		public int CustomerId
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_ID"] == DBNull.Value ? Convert.ToInt32(null) : Convert.ToInt32(base.dtModel.Rows[0]["CUSTOMER_ID"]);
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_ID"] = value;
			}
		}
		
		/// <summary>
		/// model for database field CUSTOMER_CODE(nvarchar 10)
		/// </summary>
		public string CustomerCode
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CUSTOMER_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_CODE"] = value;
			}
		}

        /// model for database field ID_TYPE(nvarchar 10)
        /// </summary>
        public string ID_TYPE
        {
            get
            {
                return base.dtModel.Rows[0]["ID_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ID_TYPE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["ID_TYPE"] = value;
            }
        }

        /// model for database field CADEMP(nvarchar 10)
        /// </summary>
        public string CADEMP
        {
            get
            {
                return base.dtModel.Rows[0]["CADEMP"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CADEMP"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CADEMP"] = value;
            }
        }
		/// <summary>
		/// model for database field CUSTOMER_TYPE(nvarchar 25)
		/// </summary>
		public string CustomerType
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CUSTOMER_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_TYPE"] = value;
			}
		}

        /// model for database field CUSTOMER_TYPE(nvarchar 25)
        /// </summary>
        public string AMOUNT_TYPE
        {
            get
            {
                return base.dtModel.Rows[0]["AMOUNT_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["AMOUNT_TYPE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["AMOUNT_TYPE"] = value;
            }
        }

		/// <summary>
		/// model for database field CUSTOMER_PARENT(int)
		/// </summary>
		public int CustomerParent
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_PARENT"] == DBNull.Value ? Convert.ToInt32(null) : Convert.ToInt32(base.dtModel.Rows[0]["CUSTOMER_PARENT"]);
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_PARENT"] = value;
			}
		}

		/// <summary>
		/// model for database field CUSTOMER_SUFFIX(nvarchar 5)
		/// </summary>
		public string CustomerSuffix
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_SUFFIX"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CUSTOMER_SUFFIX"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_SUFFIX"] = value;
			}
		}

		/// <summary>
		/// model for database field CUSTOMER_FIRST_NAME(nvarchar 25)
		/// </summary>
        // model for database field MONTHLY_INCOME(double)
        public double MONTHLY_INCOME
        {
            get
            {
                return base.dtModel.Rows[0]["MONTHLY_INCOME"] == DBNull.Value ? Convert.ToDouble(null) : double.Parse(base.dtModel.Rows[0]["MONTHLY_INCOME"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["MONTHLY_INCOME"] = value;
            }
        }

        // model for database field MONTHLY_INCOME(double)
        public double NET_ASSETS_AMOUNT
        {
            get
            {
                return base.dtModel.Rows[0]["NET_ASSETS_AMOUNT"] == DBNull.Value ? Convert.ToDouble(null) : double.Parse(base.dtModel.Rows[0]["NET_ASSETS_AMOUNT"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["NET_ASSETS_AMOUNT"] = value;
            }
        }

        // model for database field REGIONAL_IDENTIFICATION_TYPE(double)
        public string REGIONAL_IDENTIFICATION_TYPE
        {
            get
            {
                return base.dtModel.Rows[0]["REGIONAL_IDENTIFICATION_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["REGIONAL_IDENTIFICATION_TYPE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["REGIONAL_IDENTIFICATION_TYPE"] = value;
            }
        }

        // model for database field REGIONAL_IDENTIFICATION_TYPE(double)
        public string NATIONALITY
        {
            get
            {
                return base.dtModel.Rows[0]["NATIONALITY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["NATIONALITY"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["NATIONALITY"] = value;
            }
        }


        // model for database field EMAIL_ADDRESS(double)
        public string EMAIL_ADDRESS
        {
            get
            {
                return base.dtModel.Rows[0]["EMAIL_ADDRESS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["EMAIL_ADDRESS"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["EMAIL_ADDRESS"] = value;
            }
        }


		public string CustomerFirstName
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

		/// <summary>
		/// model for database field CUSTOMER_MIDDLE_NAME(nvarchar 10)
		/// </summary>
		public string CustomerMiddleName
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

		/// <summary>
		/// model for database field CUSTOMER_LAST_NAME(nvarchar 25)
		/// </summary>
		public string CustomerLastName
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

		/// <summary>
		/// model for database field CUSTOMER_ADDRESS1(nvarchar 150)
		/// </summary>
		public string CustomerAddress1
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




		/// <summary>
		/// model for database field CUSTOMER_ADDRESS2(nvarchar 150)
		/// </summary>
		public string CustomerAddress2
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

		/// <summary>
		/// model for database field CUSTOMER_CITY(nvarchar 10)
		/// </summary>
		public string CustomerCity
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

		/// <summary>
		/// model for database field CUSTOMER_COUNTRY(nvarchar 10)
		/// </summary>
		public string CustomerCountry
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

		/// <summary>
		/// model for database field CUSTOMER_STATE(nvarchar 10)
		/// </summary>
		public string CustomerState
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
		
		/// <summary>
		/// model for database field CUSTOMER_STATE(nvarchar 10)
		/// </summary>
		public string CustomerStateCode
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_STATE_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CUSTOMER_STATE_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_STATE_CODE"] = value;
			}
		}

		/// <summary>
		/// model for database field CUSTOMER_ZIP(nvarchar 12)
		/// </summary>
		public string CustomerZip
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

		/// <summary>
		/// model for database field CUSTOMER_BUSINESS_TYPE(nvarchar 20)
		/// </summary>
		public string CustomerBusinessType
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_BUSINESS_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CUSTOMER_BUSINESS_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_BUSINESS_TYPE"] = value;
			}
		}

		/// <summary>
		/// model for database field CUSTOMER_BUSINESS_DESC(nvarchar 1000)
		/// </summary>
		public string CustomerBusinessDesc
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_BUSINESS_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CUSTOMER_BUSINESS_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_BUSINESS_DESC"] = value;
			}
		}

		/// <summary>
		/// model for database field CUSTOMER_CONTACT_NAME(nvarchar 35)
		/// </summary>
		public string CustomerContactName
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_CONTACT_NAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CUSTOMER_CONTACT_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_CONTACT_NAME"] = value;
			}
		}

		/// <summary>
		/// model for database field CUSTOMER_BUSINESS_PHONE(nvarchar 15)
		/// </summary>
		public string CustomerBusinessPhone
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_BUSINESS_PHONE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CUSTOMER_BUSINESS_PHONE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_BUSINESS_PHONE"] = value;
			}
		}

		/// <summary>
		/// model for database field CUSTOMER_EXT(nvarchar 6)
		/// </summary>
		public string CustomerExt
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_EXT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CUSTOMER_EXT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_EXT"] = value;
			}
		}

		/// <summary>
		/// model for database field EMP_EXT(nvarchar 6)
		/// </summary>
		public string EMP_EXT
		{
			get
			{
				return base.dtModel.Rows[0]["EMP_EXT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["EMP_EXT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EMP_EXT"] = value;
			}
		}

		/// <summary>
		/// model for database field CUSTOMER_HOME_PHONE(nvarchar 15)
		/// </summary>
		public string CustomerHomePhone
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_HOME_PHONE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CUSTOMER_HOME_PHONE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_HOME_PHONE"] = value;
			}
		}

		/// <summary>
		/// model for database field CUSTOMER_MOBILE(nvarchar 15)
		/// </summary>
		public string CustomerMobile
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_MOBILE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CUSTOMER_MOBILE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_MOBILE"] = value;
			}
		}

		/// <summary>
		/// model for database field CUSTOMER_FAX(nvarchar 15)
		/// </summary>
		public string CustomerFax
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_FAX"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CUSTOMER_FAX"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_FAX"] = value;
			}
		}

		/// <summary>
		/// model for database field CUSTOMER_PAGER_NO(nvarchar 15)
		/// </summary>
		public string CustomerPagerNo
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_PAGER_NO"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CUSTOMER_PAGER_NO"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_PAGER_NO"] = value;
			}
		}

		/// <summary>
		/// model for database field CUSTOMER_Email(nvarchar 50)
		/// </summary>
		public string CustomerEmail
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_Email"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CUSTOMER_Email"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_Email"] = value;
			}
		}

		/// <summary>
		/// model for database field CUSTOMER_WEBSITE(nvarchar 150)
		/// </summary>
		public string CustomerWebsite
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_WEBSITE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CUSTOMER_WEBSITE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_WEBSITE"] = value;
			}
		}

		/// <summary>
		/// model for database field CUSTOMER_INSURANCE_SCORE(numeric 9)
		/// </summary>
		public decimal CustomerInsuranceScore
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_INSURANCE_SCORE"] == DBNull.Value ? -1 : Convert.ToDecimal(base.dtModel.Rows[0]["CUSTOMER_INSURANCE_SCORE"]);
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_INSURANCE_SCORE"] = value;
			}
		}

		/// <summary>
		/// model for database field CUSTOMER_INSURANCE_RECEIVED_DATE(datetime)
		/// </summary>
		public DateTime CustomerInsuranceReceivedDate
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_INSURANCE_RECEIVED_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["CUSTOMER_INSURANCE_RECEIVED_DATE"]);
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_INSURANCE_RECEIVED_DATE"] = value;
			}
		}

		/// <summary>
		/// model for database field CUSTOMER_REASON_CODE(smallint )
		/// </summary>
		public string CustomerReasonCode
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_REASON_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CUSTOMER_REASON_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_REASON_CODE"] = value;
			}
		}

		/// <summary>
		/// model for database field CUSTOMER_REASON_CODE(smallint)
		/// </summary>
		public string CustomerReasonCode2
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_REASON_CODE2"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CUSTOMER_REASON_CODE2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_REASON_CODE2"] = value;
			}
		}
		/// <summary>
		/// model for database field CUSTOMER_REASON_CODE(smallint)
		/// </summary>
		public string CustomerReasonCode3
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_REASON_CODE3"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CUSTOMER_REASON_CODE3"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_REASON_CODE3"] = value;
			}
		}
		/// <summary>
		/// model for database field CUSTOMER_REASON_CODE(smallint)
		/// </summary>
		public string CustomerReasonCode4
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_REASON_CODE4"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CUSTOMER_REASON_CODE4"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_REASON_CODE4"] = value;
			}
		}

		/// <summary>
		/// model for database field CUSTOMER_PRODUCER_ID(nvarchar 30)
		/// </summary>
		public string CustomerProducerId
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_PRODUCER_ID"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CUSTOMER_PRODUCER_ID"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_PRODUCER_ID"] = value;
			}
		}

		/// <summary>
		/// model for database field CUSTOMER_ACCOUNT_EXECUTIVE_ID(nvarchar 30)
		/// </summary>
		public string CustomerAccountExecutiveId
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_ACCOUNT_EXECUTIVE_ID"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CUSTOMER_ACCOUNT_EXECUTIVE_ID"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_ACCOUNT_EXECUTIVE_ID"] = value;
			}
		}

		/// <summary>
		/// model for database field CUSTOMER_CSR(nvarchar 30)
		/// </summary>
		public string CustomerCsr
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_CSR"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CUSTOMER_CSR"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_CSR"] = value;
			}
		}

		/// <summary>
		/// model for database field CUSTOMER_LATE_CHARGES(nchar 1)
		/// </summary>
		public string CustomerLateCharges
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_LATE_CHARGES"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CUSTOMER_LATE_CHARGES"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_LATE_CHARGES"] = value;
			}
		}

		/// <summary>
		/// model for database field CUSTOMER_LATE_NOTICES(nchar 1)
		/// </summary>
		public string CustomerLateNotices
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_LATE_NOTICES"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CUSTOMER_LATE_NOTICES"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_LATE_NOTICES"] = value;
			}
		}

		/// <summary>
		/// model for database field CUSTOMER_SEND_STATEMENT(nchar 1)
		/// </summary>
		public string CustomerSendStatement
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_SEND_STATEMENT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CUSTOMER_SEND_STATEMENT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_SEND_STATEMENT"] = value;
			}
		}

		/// <summary>
		/// model for database field CUSTOMER_RECEIVABLE_DUE_DAYS(int)
		/// </summary>
		public int CustomerReceivableDueDays
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_RECEIVABLE_DUE_DAYS"] == DBNull.Value ? Convert.ToInt32(null) : Convert.ToInt32(base.dtModel.Rows[0]["CUSTOMER_RECEIVABLE_DUE_DAYS"]);
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_RECEIVABLE_DUE_DAYS"] = value;}
		}

		/// <summary>
		/// model for database field CUSTOMER_REFERRED_BY(nvarchar 25)
		/// </summary>
		public string CustomerReferredBy
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_REFERRED_BY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CUSTOMER_REFERRED_BY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_REFERRED_BY"] = value;
			}
		}

		/// <summary>
		/// model for database field CUSTOMER_AGENCY_ID(smallint)
		/// </summary>
		public int CustomerAgencyId
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_AGENCY_ID"] == DBNull.Value ? Convert.ToInt32(null) : Convert.ToInt32(base.dtModel.Rows[0]["CUSTOMER_AGENCY_ID"]);
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_AGENCY_ID"] = value;
			}
		}

		/// <summary>
		/// model for database field IS_ACTIVE(nchar 1)
		/// </summary>
		public string IsActive
		{
			get
			{
				return base.dtModel.Rows[0]["IS_ACTIVE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_ACTIVE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_ACTIVE"] = value;
			}
		}
		/// <summary>
		/// model for database field IS_ACTIVE(nchar 1)
		/// </summary>
		public string Customer_Attention_Note
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_ATTENTION_NOTE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CUSTOMER_ATTENTION_NOTE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_ATTENTION_NOTE"] = value;
			}
		}
		/// <summary>
		/// model for database field PREFIX(int)
		/// </summary>
		public int PREFIX
		{
			get
			{
				return base.dtModel.Rows[0]["PREFIX"] == DBNull.Value ? Convert.ToInt32(null) : Convert.ToInt32(base.dtModel.Rows[0]["PREFIX"]);
			}
			set
			{
				base.dtModel.Rows[0]["PREFIX"] = value;
			}
		}

		/// <summary>
		/// model for database field PER_CUST_MOBILE(nvarchar 15)
		/// </summary>
		public string PER_CUST_MOBILE
		{
			get
			{
				return base.dtModel.Rows[0]["PER_CUST_MOBILE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PER_CUST_MOBILE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PER_CUST_MOBILE"] = value;
			}
		}

		public DateTime ATTENTION_NOTE_UPDATED
		{
			get
			{
				return base.dtModel.Rows[0]["ATTENTION_NOTE_UPDATED"] == DBNull.Value ? DateTime.MinValue:Convert.ToDateTime(base.dtModel.Rows[0]["ATTENTION_NOTE_UPDATED"]);

			}
			set
			{
				base.dtModel.Rows[0]["ATTENTION_NOTE_UPDATED"] = value;
			}
		}

		public DateTime LAST_INSURANCE_SCORE_FETCHED
		{
			get
			{
				return base.dtModel.Rows[0]["LAST_INSURANCE_SCORE_FETCHED"] == DBNull.Value ? DateTime.MinValue:Convert.ToDateTime(base.dtModel.Rows[0]["LAST_INSURANCE_SCORE_FETCHED"]);

			}
			set
			{
				base.dtModel.Rows[0]["LAST_INSURANCE_SCORE_FETCHED"] = value;
			}
		}
		
		public string FACTOR1
		{
			get
			{
				return base.dtModel.Rows[0]["FACTOR1"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["FACTOR1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["FACTOR1"] = value;
			}
		}
		
		public string FACTOR2
		{
			get
			{
				return base.dtModel.Rows[0]["FACTOR2"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["FACTOR2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["FACTOR2"] = value;
			}
		}
		
		public string FACTOR3
		{
			get
			{
				return base.dtModel.Rows[0]["FACTOR3"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["FACTOR3"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["FACTOR3"] = value;
			}
		}
		
		public string FACTOR4
		{
			get
			{
				return base.dtModel.Rows[0]["FACTOR4"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["FACTOR4"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["FACTOR4"] = value;
			}
		}
		//Model for database field EMPLOYER_ADD1(string)
		public string EMPLOYER_ADD1
		{
			get
			{
				return base.dtModel.Rows[0]["EMPLOYER_ADD1"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["EMPLOYER_ADD1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EMPLOYER_ADD1"] = value;
			}
		}
		//Model for database field EMPLOYER_ADD2(string)
		public string EMPLOYER_ADD2
		{
			get
			{
				return base.dtModel.Rows[0]["EMPLOYER_ADD2"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["EMPLOYER_ADD2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EMPLOYER_ADD2"] = value;
			}
		}
		//Model for database field EMPLOYER_CITY(string)
		public string EMPLOYER_CITY
		{
			get
			{
				return base.dtModel.Rows[0]["EMPLOYER_CITY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["EMPLOYER_CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EMPLOYER_CITY"] = value;
			}
		}
		//Model for database field EMPLOYER_COUNTRY(string)
		public string EMPLOYER_COUNTRY
		{
			get
			{
				return base.dtModel.Rows[0]["EMPLOYER_COUNTRY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["EMPLOYER_COUNTRY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EMPLOYER_COUNTRY"] = value;
			}
		}
		//Model for database field EMPLOYER_STATE(string)
		public string EMPLOYER_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["EMPLOYER_STATE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["EMPLOYER_STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EMPLOYER_STATE"] = value;
			}
		}
		//Model for database field EMPLOYER_ZIPCODE(string)
		public string EMPLOYER_ZIPCODE
		{
			get
			{
				return base.dtModel.Rows[0]["EMPLOYER_ZIPCODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["EMPLOYER_ZIPCODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EMPLOYER_ZIPCODE"] = value;
			}
		}
		//Model for database field EMPLOYER_HOMEPHONE(string)
		public string EMPLOYER_HOMEPHONE
		{
			get
			{
				return base.dtModel.Rows[0]["EMPLOYER_HOMEPHONE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["EMPLOYER_HOMEPHONE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EMPLOYER_HOMEPHONE"] = value;
			}
		}
		//Model for database field EMPLOYER_EMAIL(string)
		public string EMPLOYER_EMAIL
		{
			get
			{
				return base.dtModel.Rows[0]["EMPLOYER_EMAIL"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["EMPLOYER_EMAIL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EMPLOYER_EMAIL"] = value;
			}
		}
		// model for database field YEARS_WITH_CURR_OCCU(double)
		public double YEARS_WITH_CURR_OCCU
		{
			get
			{
				return base.dtModel.Rows[0]["YEARS_WITH_CURR_OCCU"] == DBNull.Value ? Convert.ToDouble(null) : double.Parse(base.dtModel.Rows[0]["YEARS_WITH_CURR_OCCU"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["YEARS_WITH_CURR_OCCU"] = value;
			}
		}

        public string IS_POLITICALLY_EXPOSED
        {
            get
            {
                return base.dtModel.Rows[0]["IS_POLITICALLY_EXPOSED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_POLITICALLY_EXPOSED"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["IS_POLITICALLY_EXPOSED"] = value;
            }
        }

		
		/*public string Alt_CustomerAddress1
		{
			get
			{
				return base.dtModel.Rows[0]["ALT_CUSTOMER_ADDRESS1"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ALT_CUSTOMER_ADDRESS1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ALT_CUSTOMER_ADDRESS1"] = value;
			}
		}




		/// <summary>
		/// model for database field CUSTOMER_ADDRESS2(nvarchar 150)
		/// </summary>
		public string Alt_CustomerAddress2
		{
			get
			{
				return base.dtModel.Rows[0]["ALT_CUSTOMER_ADDRESS2"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ALT_CUSTOMER_ADDRESS2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ALT_CUSTOMER_ADDRESS2"] = value;
			}
		}

		/// <summary>
		/// model for database field CUSTOMER_CITY(nvarchar 10)
		/// </summary>
		public string Alt_CustomerCity
		{
			get
			{
				return base.dtModel.Rows[0]["ALT_CUSTOMER_CITY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ALT_CUSTOMER_CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ALT_CUSTOMER_CITY"] = value;
			}
		}

		/// <summary>
		/// model for database field CUSTOMER_COUNTRY(nvarchar 10)
		/// </summary>
		public string Alt_CustomerCountry
		{
			get
			{
				return base.dtModel.Rows[0]["ALT_CUSTOMER_COUNTRY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ALT_CUSTOMER_COUNTRY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ALT_CUSTOMER_COUNTRY"] = value;
			}
		}

		/// <summary>
		/// model for database field CUSTOMER_STATE(nvarchar 10)
		/// </summary>
		public string Alt_CustomerState
		{
			get
			{
				return base.dtModel.Rows[0]["ALT_CUSTOMER_STATE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ALT_CUSTOMER_STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ALT_CUSTOMER_STATE"] = value;
			}
		}
		


		/// <summary>
		/// model for database field CUSTOMER_ZIP(nvarchar 12)
		/// </summary>
		public string Alt_CustomerZip
		{
			get
			{
				return base.dtModel.Rows[0]["ALT_CUSTOMER_ZIP"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CUSTOMER_ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ALT_CUSTOMER_ZIP"] = value;
			}
		}*/

        //--------Start Addded By Lalit on March 11,2010

        /// <summary>
        /// Model for database field CPF_CNPJ(String)
        /// </summary>
        public string CPF_CNPJ
        {
            get
            {

                return base.dtModel.Rows[0]["CPF_CNPJ"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CPF_CNPJ"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CPF_CNPJ"] = value;
            }
        }
        /// <summary>
        /// Model for database field NUMBER(String)
        /// </summary>
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
       
        /// <summary>
        /// Model for database field DISTRICT(String)
        /// </summary>
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
       
        
        /// <summary>
        /// Model for database field MAIN_TITLE(String)
        /// </summary>
        public int MAIN_TITLE  
        {
            get
            {

                return base.dtModel.Rows[0]["MAIN_TITLE"] == DBNull.Value ? Convert.ToInt32(null) : Convert.ToInt32(base.dtModel.Rows[0]["MAIN_TITLE"]);
            }
            set
            {
                base.dtModel.Rows[0]["MAIN_TITLE"] = value;
            }
        }
        /// <summary>
        ///  Model for database field MAIN_POSITION(String)
        /// </summary>
        public int MAIN_POSITION 
        {
            get
            {

                return base.dtModel.Rows[0]["MAIN_POSITION"] == DBNull.Value ? Convert.ToInt32(null) : Convert.ToInt32(base.dtModel.Rows[0]["MAIN_POSITION"]);
            }
            set
            {
                base.dtModel.Rows[0]["MAIN_POSITION"] = value;
            }
        }
        /// <summary>
        /// Model for database field MAIN_CPF_CNPJ(String)
        /// </summary>
        public String MAIN_CPF_CNPJ
        {
            get
            {

                return base.dtModel.Rows[0]["MAIN_CPF_CNPJ"] == DBNull.Value ? Convert.ToString(null) : Convert.ToString(base.dtModel.Rows[0]["MAIN_CPF_CNPJ"]);
            }
            set
            {
                base.dtModel.Rows[0]["MAIN_CPF_CNPJ"] = value;
            }
        }
        /// <summary>
        /// Model for database field MAIN_ADDRESS(String)
        /// </summary>
        public String MAIN_ADDRESS
        {
            get
            {

                return base.dtModel.Rows[0]["MAIN_ADDRESS"] == DBNull.Value ? Convert.ToString(null) : Convert.ToString(base.dtModel.Rows[0]["MAIN_ADDRESS"]);
            }
            set
            {
                base.dtModel.Rows[0]["MAIN_ADDRESS"] = value;
            }
        }
        /// <summary>
        /// Model for database field MAIN_NUMBER(String)
        /// </summary>
        public String MAIN_NUMBER
        {
            get
            {

                return base.dtModel.Rows[0]["MAIN_NUMBER"] == DBNull.Value ? Convert.ToString(null) : Convert.ToString(base.dtModel.Rows[0]["MAIN_NUMBER"]);
            }
            set
            {
                base.dtModel.Rows[0]["MAIN_NUMBER"] = value;
            }
        }
        /// <summary>
        /// Model for database field MAIN_COMPLIMENT(String)
        /// </summary>
        public String MAIN_COMPLIMENT
        {
            get
            {

                return base.dtModel.Rows[0]["MAIN_COMPLIMENT"] == DBNull.Value ? Convert.ToString(null) : Convert.ToString(base.dtModel.Rows[0]["MAIN_COMPLIMENT"]);
            }
            set
            {
                base.dtModel.Rows[0]["MAIN_COMPLIMENT"] = value;
            }
        }
        /// <summary>
        /// Model for database field MAIN_DISTRICT(String)
        /// </summary>
        public String MAIN_DISTRICT
        {
            get
            {

                return base.dtModel.Rows[0]["MAIN_DISTRICT"] == DBNull.Value ? Convert.ToString(null) : Convert.ToString(base.dtModel.Rows[0]["MAIN_DISTRICT"]);
            }
            set
            {
                base.dtModel.Rows[0]["MAIN_DISTRICT"] = value;
            }
        }
        /// <summary>
        /// Model for database field MAIN_NOTE(String)
        /// </summary>
        public String MAIN_NOTE
        {
            get
            {
                return base.dtModel.Rows[0]["MAIN_NOTE"] == DBNull.Value ? Convert.ToString(null) : Convert.ToString(base.dtModel.Rows[0]["MAIN_NOTE"]);
            }
            set
            {
                base.dtModel.Rows[0]["MAIN_NOTE"] = value;
            }
        }
          /// <summary>
        /// Model for database field MAIN_NOTE(String)
        /// </summary>
        public String MAIN_CONTACT_CODE
        {
            get
            {
                return base.dtModel.Rows[0]["MAIN_CONTACT_CODE"] == DBNull.Value ? Convert.ToString(null) : Convert.ToString(base.dtModel.Rows[0]["MAIN_CONTACT_CODE"]);
            }
            set
            {
                base.dtModel.Rows[0]["MAIN_CONTACT_CODE"] = value;
            }
        }
         /// <summary>
        /// Model for database field REGIONAL_IDENTIFICATION(String)
        /// </summary>
        public String REGIONAL_IDENTIFICATION
        {
            get
            {
                return base.dtModel.Rows[0]["REGIONAL_IDENTIFICATION"] == DBNull.Value ? Convert.ToString(null) : Convert.ToString(base.dtModel.Rows[0]["REGIONAL_IDENTIFICATION"]);
            }
            set
            {
                base.dtModel.Rows[0]["REGIONAL_IDENTIFICATION"] = value;
            }
        }

        /// <summary>
        /// Model for database field REG_ID_ISSUE(DateTime)
        /// </summary>
        public DateTime REG_ID_ISSUE
        {
            get
            {
                return base.dtModel.Rows[0]["REG_ID_ISSUE"] == DBNull.Value ? Convert.ToDateTime(null) : Convert.ToDateTime(base.dtModel.Rows[0]["REG_ID_ISSUE"]);
            }
            set
            {
                base.dtModel.Rows[0]["REG_ID_ISSUE"] = value;
            }
        }
        /// <summary>
        /// Model for database field ORIGINAL_ISSUE(String)
        /// </summary>
        public String ORIGINAL_ISSUE
        {
            get
            {
                return base.dtModel.Rows[0]["ORIGINAL_ISSUE"] == DBNull.Value ? Convert.ToString(null) : Convert.ToString(base.dtModel.Rows[0]["ORIGINAL_ISSUE"]);
            }
            set
            {
                base.dtModel.Rows[0]["ORIGINAL_ISSUE"] = value;
            }
       }
      
        /// <summary>
        /// Model for database field MAIN_ZIPCODE(String)
        /// </summary>
        public string MAIN_ZIPCODE
        {
            get
            {
                return base.dtModel.Rows[0]["MAIN_ZIPCODE"] == DBNull.Value ? Convert.ToString(null) : Convert.ToString(base.dtModel.Rows[0]["MAIN_ZIPCODE"]);
            }
            set
            {
                base.dtModel.Rows[0]["MAIN_ZIPCODE"] = value;
            }
        }
        /// <summary>
        /// Model for database field MAIN_CITY(String)
        /// </summary>
        public string MAIN_CITY
        {
            get
            {
                return base.dtModel.Rows[0]["MAIN_CITY"] == DBNull.Value ? Convert.ToString(null) : Convert.ToString(base.dtModel.Rows[0]["MAIN_CITY"]);
            }
            set
            {
                base.dtModel.Rows[0]["MAIN_CITY"] = value;
            }
        }
        /// <summary>
        /// Model for database field MAIN_STATE(Int)
        /// </summary>
        public int MAIN_STATE
        {
            get
            {
                return base.dtModel.Rows[0]["MAIN_STATE"] == DBNull.Value ? Convert.ToInt32(null) : Convert.ToInt32(base.dtModel.Rows[0]["MAIN_STATE"]);
            }
            set
            {
                base.dtModel.Rows[0]["MAIN_STATE"] = value;
            }
        }
        /// <summary>
        /// Model for database field MAIN_COUNTRY(Int)
        /// </summary>
        public int MAIN_COUNTRY
        {
            get
            {
                return base.dtModel.Rows[0]["MAIN_COUNTRY"] == DBNull.Value ? Convert.ToInt32(null) : Convert.ToInt32(base.dtModel.Rows[0]["MAIN_COUNTRY"]);
            }
            set
            {
                base.dtModel.Rows[0]["MAIN_COUNTRY"] = value;
            }
        }
         /// <summary>
        /// Model for database field MAIN_FIRST_NAME(String)
        /// </summary>
        public String MAIN_FIRST_NAME
        {
            get
            {
                return base.dtModel.Rows[0]["MAIN_FIRST_NAME"] == DBNull.Value ? Convert.ToString(null) : Convert.ToString(base.dtModel.Rows[0]["MAIN_FIRST_NAME"]);
            }
            set
            {
                base.dtModel.Rows[0]["MAIN_FIRST_NAME"] = value;
            }
        }
         /// <summary>
        /// Model for database field MAIN_MIDDLE_NAME(String)
        /// </summary>
        public String MAIN_MIDDLE_NAME
        {
            get
            {
                return base.dtModel.Rows[0]["MAIN_MIDDLE_NAME"] == DBNull.Value ? Convert.ToString(null) : Convert.ToString(base.dtModel.Rows[0]["MAIN_MIDDLE_NAME"]);
            }
            set
            {
                base.dtModel.Rows[0]["MAIN_MIDDLE_NAME"] = value;

            }
        }     
            
        /// <summary>
        /// Model for database field MAIN_LAST_NAME(String)
        /// </summary>
        public String MAIN_LAST_NAME
        {
            get
            {
                return base.dtModel.Rows[0]["MAIN_LAST_NAME"] == DBNull.Value ? Convert.ToString(null) : Convert.ToString(base.dtModel.Rows[0]["MAIN_LAST_NAME"]);
            }
            set
            {
                base.dtModel.Rows[0]["MAIN_LAST_NAME"] = value;

            }
        }
        public int ACCOUNT_TYPE
        {

            get
            {
                return base.dtModel.Rows[0]["ACCOUNT_TYPE"] == DBNull.Value ? Convert.ToInt32(null) : Convert.ToInt32(base.dtModel.Rows[0]["ACCOUNT_TYPE"]);
            }
            set
            {
                base.dtModel.Rows[0]["ACCOUNT_TYPE"] = value;
            }
        }

        public String BANK_BRANCH
        {
            get
            {
                return base.dtModel.Rows[0]["BANK_BRANCH"] == DBNull.Value ? Convert.ToString(null) : Convert.ToString(base.dtModel.Rows[0]["BANK_BRANCH"]);
            }
            set
            {
                base.dtModel.Rows[0]["BANK_BRANCH"] = value;
            }
        }

        public String BANK_NUMBER
        {
            get
            {
                return base.dtModel.Rows[0]["BANK_NUMBER"] == DBNull.Value ? Convert.ToString(null) : Convert.ToString(base.dtModel.Rows[0]["BANK_NUMBER"]);
            }
            set
            {
                base.dtModel.Rows[0]["BANK_NUMBER"] = value;
            }
        }

        public String BANK_NAME
        {
            get
            {
                return base.dtModel.Rows[0]["BANK_NAME"] == DBNull.Value ? Convert.ToString(null) : Convert.ToString(base.dtModel.Rows[0]["BANK_NAME"]);
            }
            set
            {
                base.dtModel.Rows[0]["BANK_NAME"] = value;
            }
        }

        public String ACCOUNT_NUMBER
        {
            get
            {
                return base.dtModel.Rows[0]["ACCOUNT_NUMBER"] == DBNull.Value ? Convert.ToString(null) : Convert.ToString(base.dtModel.Rows[0]["ACCOUNT_NUMBER"]);
            }
            set
            {
                base.dtModel.Rows[0]["ACCOUNT_NUMBER"] = value;
            }
        }

        
        //--------End Added
        #endregion

		
	}
}
