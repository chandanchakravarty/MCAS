/******************************************************************************************
<Author				: -   
<Start Date				: -	8/30/2005 4:50:50 PM
<End Date				: -	
<Description				: - 	
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 6/10/2005
<Modified By				: - Mohit Gupta
<Purpose				: - Adding field IS_PRIMARY_APPLICANT.

*******************************************************************************************/ 
using System;
using System.Data;
using Cms.Model;

namespace Cms.Model.Client
{
    /// <summary>
    /// Summary description for ClsApplicantInsued.
    /// </summary>
    public class ClsApplicantInsuedInfo : ClsCommonModel
    {
        public ClsApplicantInsuedInfo()
        {
            base.dtModel.TableName = "CLT_APPLICANT_LIST";		// setting table name for data table that holds property values.
            this.AddColumns();								// add columns of the database table CLT_APPLICANT_LIST
            base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
        }
        private const string CLT_APPLICANT_LIST = "CLT_APPLICANT_LIST";

        private void AddColumns()
        {

            base.dtModel.Columns.Add("APPLICANT_ID", typeof(int));
            base.dtModel.Columns.Add("CUSTOMER_ID", typeof(int));
            base.dtModel.Columns.Add("APPLICANT_STATUS", typeof(string));
            base.dtModel.Columns.Add("TITLE", typeof(int));
            base.dtModel.Columns.Add("SUFFIX", typeof(string));
            base.dtModel.Columns.Add("FIRST_NAME", typeof(string));
            base.dtModel.Columns.Add("MIDDLE_NAME", typeof(string));
            base.dtModel.Columns.Add("LAST_NAME", typeof(string));
            base.dtModel.Columns.Add("ADDRESS1", typeof(string));
            base.dtModel.Columns.Add("ADDRESS2", typeof(string));
            base.dtModel.Columns.Add("CITY", typeof(string));
            base.dtModel.Columns.Add("COUNTRY", typeof(string));
            base.dtModel.Columns.Add("STATE", typeof(string));
            base.dtModel.Columns.Add("ZIP_CODE", typeof(string));
            base.dtModel.Columns.Add("PHONE", typeof(string));
            base.dtModel.Columns.Add("MOBILE", typeof(string));
            base.dtModel.Columns.Add("BUSINESS_PHONE", typeof(string));
            base.dtModel.Columns.Add("EXT", typeof(string));
            base.dtModel.Columns.Add("EMAIL", typeof(string));

            base.dtModel.Columns.Add("CO_APPLI_OCCU", typeof(string));
            base.dtModel.Columns.Add("CO_APPLI_EMPL_NAME", typeof(string));
            base.dtModel.Columns.Add("CO_APPLI_EMPL_ADDRESS", typeof(string));
            base.dtModel.Columns.Add("CO_APPLI_EMPL_ADDRESS1", typeof(string));
            base.dtModel.Columns.Add("CO_APPLI_YEARS_WITH_CURR_EMPL", typeof(double));
            base.dtModel.Columns.Add("CO_APPL_YEAR_CURR_OCCU", typeof(double));
            base.dtModel.Columns.Add("CO_APPL_MARITAL_STATUS", typeof(string));
            base.dtModel.Columns.Add("CO_APPL_DOB", typeof(DateTime));
            base.dtModel.Columns.Add("CO_APPL_SSN_NO", typeof(string));
            //Added by Swastika on 10th Apr'06 for # 2367
            base.dtModel.Columns.Add("CO_APPLI_EMPL_CITY", typeof(string));
            base.dtModel.Columns.Add("CO_APPLI_EMPL_COUNTRY", typeof(string));
            base.dtModel.Columns.Add("CO_APPLI_EMPL_STATE", typeof(string));
            base.dtModel.Columns.Add("CO_APPLI_EMPL_ZIP_CODE", typeof(string));
            base.dtModel.Columns.Add("CO_APPLI_EMPL_PHONE", typeof(string));
            base.dtModel.Columns.Add("CO_APPLI_EMPL_EMAIL", typeof(string));
            //Added by Neeraj Singh on 08 Nov for isu no. 9
            base.dtModel.Columns.Add("CO_APPL_RELATIONSHIP", typeof(string));
            base.dtModel.Columns.Add("CO_APPL_GENDER", typeof(string));
            //base.dtModel.Columns.Add("IS_PRIMARY_APPLICANT",typeof(int));
            // Added by mohit on 4/11/2005..
            base.dtModel.Columns.Add("DESC_CO_APPLI_OCCU", typeof(string));

            base.dtModel.Columns.Add("CUSTOMER_FIRST_NAME", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_MIDDLE_NAME", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_LAST_NAME", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_CODE", typeof(string));

            //Added  By Lalit March 15,2010

            base.dtModel.Columns.Add("POSITION", typeof(int));
            base.dtModel.Columns.Add("CONTACT_CODE", typeof(string));
            base.dtModel.Columns.Add("ID_TYPE", typeof(int));
            base.dtModel.Columns.Add("ID_TYPE_NUMBER", typeof(string));
            base.dtModel.Columns.Add("NUMBER", typeof(string));
            base.dtModel.Columns.Add("COMPLIMENT", typeof(string));
            base.dtModel.Columns.Add("DISTRICT", typeof(string));
            base.dtModel.Columns.Add("NOTE", typeof(string));

            base.dtModel.Columns.Add("REGIONAL_IDENTIFICATION", typeof(string));
            base.dtModel.Columns.Add("REG_ID_ISSUE", typeof(DateTime));
            base.dtModel.Columns.Add("ORIGINAL_ISSUE", typeof(string));
            base.dtModel.Columns.Add("CPF_CNPJ", typeof(string));
            base.dtModel.Columns.Add("FAX", typeof(string));
            base.dtModel.Columns.Add("TYPE", typeof(int));
            base.dtModel.Columns.Add("ACCOUNT_TYPE", typeof(int));
            base.dtModel.Columns.Add("BANK_BRANCH", typeof(string));
            base.dtModel.Columns.Add("BANK_NUMBER", typeof(string));
            base.dtModel.Columns.Add("BANK_NAME", typeof(string));
            base.dtModel.Columns.Add("ACCOUNT_NUMBER", typeof(string));

        }
        #region Database schema details
        // model for database field CO_APPL_RELATIONSHIP(string)
        public string CO_APPL_GENDER
        {
            get
            {
                return base.dtModel.Rows[0]["CO_APPL_GENDER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CO_APPL_GENDER"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CO_APPL_GENDER"] = value;
            }
        }

        public string CO_APPL_RELATIONSHIP
        {
            get
            {
                return base.dtModel.Rows[0]["CO_APPL_RELATIONSHIP"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CO_APPL_RELATIONSHIP"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CO_APPL_RELATIONSHIP"] = value;
            }
        }

        // model for database field DESC_CO_APPLI_OCCU(string)
        public string DESC_CO_APPLI_OCCU
        {
            get
            {
                return base.dtModel.Rows[0]["DESC_CO_APPLI_OCCU"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_CO_APPLI_OCCU"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["DESC_CO_APPLI_OCCU"] = value;
            }
        }


        // model for database field CO_APPL_MARITAL_STATUS(string)
        public string CO_APPL_MARITAL_STATUS
        {
            get
            {
                return base.dtModel.Rows[0]["CO_APPL_MARITAL_STATUS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CO_APPL_MARITAL_STATUS"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CO_APPL_MARITAL_STATUS"] = value;
            }
        }
        //		// model for database field IS_PRIMARY_APPLICANT(string)
        //		public string IS_PRIMARY_APPLICANT
        //		{
        //			get
        //			{
        //				return base.dtModel.Rows[0]["IS_PRIMARY_APPLICANT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_PRIMARY_APPLICANT"].ToString();
        //			}
        //			set
        //			{
        //				base.dtModel.Rows[0]["IS_PRIMARY_APPLICANT"] = value;
        //			}
        //		}
        // model for database field CO_APPL_DOB(DateTime)
        public DateTime CO_APPL_DOB
        {
            get
            {
                return base.dtModel.Rows[0]["CO_APPL_DOB"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["CO_APPL_DOB"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["CO_APPL_DOB"] = value;
            }
        }
        // model for database field CO_APPL_SSN_NO(string)
        public string CO_APPL_SSN_NO
        {
            get
            {
                return base.dtModel.Rows[0]["CO_APPL_SSN_NO"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CO_APPL_SSN_NO"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CO_APPL_SSN_NO"] = value;
            }
        }
        // model for database field CO_APPL_YEAR_CURR_OCCU(double)
        public double CO_APPL_YEAR_CURR_OCCU
        {
            get
            {
                return base.dtModel.Rows[0]["CO_APPL_YEAR_CURR_OCCU"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["CO_APPL_YEAR_CURR_OCCU"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["CO_APPL_YEAR_CURR_OCCU"] = value;
            }
        }
        // model for database field CO_APPLI_OCCU(string)
        public string CO_APPLI_OCCU
        {
            get
            {
                return base.dtModel.Rows[0]["CO_APPLI_OCCU"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CO_APPLI_OCCU"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CO_APPLI_OCCU"] = value;
            }
        }
        // model for database field CO_APPLI_EMPL_NAME(string)
        public string CO_APPLI_EMPL_NAME
        {
            get
            {
                return base.dtModel.Rows[0]["CO_APPLI_EMPL_NAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CO_APPLI_EMPL_NAME"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CO_APPLI_EMPL_NAME"] = value;
            }
        }
        // model for database field CO_APPLI_EMPL_ADDRESS(string)
        public string CO_APPLI_EMPL_ADDRESS
        {
            get
            {
                return base.dtModel.Rows[0]["CO_APPLI_EMPL_ADDRESS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CO_APPLI_EMPL_ADDRESS"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CO_APPLI_EMPL_ADDRESS"] = value;
            }
        }
        // model for database field CO_APPLI_EMPL_ADDRESS1(string)
        public string CO_APPLI_EMPL_ADDRESS1
        {
            get
            {
                return base.dtModel.Rows[0]["CO_APPLI_EMPL_ADDRESS1"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CO_APPLI_EMPL_ADDRESS1"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CO_APPLI_EMPL_ADDRESS1"] = value;
            }
        }
        // model for database field CO_APPLI_YEARS_WITH_CURR_EMPL(double)
        public double CO_APPLI_YEARS_WITH_CURR_EMPL
        {
            get
            {
                return base.dtModel.Rows[0]["CO_APPLI_YEARS_WITH_CURR_EMPL"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["CO_APPLI_YEARS_WITH_CURR_EMPL"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["CO_APPLI_YEARS_WITH_CURR_EMPL"] = value;
            }
        }
        // model for database field CO_APPLI_EMPL_CITY(string)
        public string CO_APPLI_EMPL_CITY
        {
            get
            {
                return base.dtModel.Rows[0]["CO_APPLI_EMPL_CITY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CO_APPLI_EMPL_CITY"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CO_APPLI_EMPL_CITY"] = value;
            }
        }
        // model for database field CO_APPLI_EMPL_COUNTRY(string)
        public string CO_APPLI_EMPL_COUNTRY
        {
            get
            {
                return base.dtModel.Rows[0]["CO_APPLI_EMPL_COUNTRY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CO_APPLI_EMPL_COUNTRY"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CO_APPLI_EMPL_COUNTRY"] = value;
            }
        }
        // model for database field CO_APPLI_EMPL_STATE(string)
        public string CO_APPLI_EMPL_STATE
        {
            get
            {
                return base.dtModel.Rows[0]["CO_APPLI_EMPL_STATE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CO_APPLI_EMPL_STATE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CO_APPLI_EMPL_STATE"] = value;
            }
        }
        // model for database field CO_APPLI_EMPL_ZIP_CODE(string)
        public string CO_APPLI_EMPL_ZIP_CODE
        {
            get
            {
                return base.dtModel.Rows[0]["CO_APPLI_EMPL_ZIP_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CO_APPLI_EMPL_ZIP_CODE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CO_APPLI_EMPL_ZIP_CODE"] = value;
            }
        }
        // model for database field CO_APPLI_EMPL_PHONE(string)
        public string CO_APPLI_EMPL_PHONE
        {
            get
            {
                return base.dtModel.Rows[0]["CO_APPLI_EMPL_PHONE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CO_APPLI_EMPL_PHONE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CO_APPLI_EMPL_PHONE"] = value;
            }
        }
        // model for database field CO_APPLI_EMPL_EMAIL(string)
        public string CO_APPLI_EMPL_EMAIL
        {
            get
            {
                return base.dtModel.Rows[0]["CO_APPLI_EMPL_EMAIL"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CO_APPLI_EMPL_EMAIL"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CO_APPLI_EMPL_EMAIL"] = value;
            }
        }
        // model for database field CUSTOMER_ID(int)
        public int CUSTOMER_ID
        {
            get
            {
                return dtModel.Rows[0]["CUSTOMER_ID"] == DBNull.Value ? Convert.ToInt32(null) : Convert.ToInt32(dtModel.Rows[0]["CUSTOMER_ID"]);
            }
            set
            {
                dtModel.Rows[0]["CUSTOMER_ID"] = value;
            }
        }

        // model for database field APPLICANT_ID(int)
        public int APPLICANT_ID
        {
            get
            {
                return dtModel.Rows[0]["APPLICANT_ID"] == DBNull.Value ? Convert.ToInt32(null) : Convert.ToInt32(dtModel.Rows[0]["APPLICANT_ID"]);
            }
            set
            {
                dtModel.Rows[0]["APPLICANT_ID"] = value;
            }
        }
        // model for database field APPLICANT_STATUS(string)
        public string APPLICANT_STATUS
        {
            get
            {
                return base.dtModel.Rows[0]["APPLICANT_STATUS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["APPLICANT_STATUS"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["APPLICANT_STATUS"] = value;
            }
        }
        // model for database field TITLE(string)
        
        public int TITLE
        {

            get
            {
                return base.dtModel.Rows[0]["TITLE"] == DBNull.Value ? Convert.ToInt32(null) : Convert.ToInt32(base.dtModel.Rows[0]["TITLE"]);
            }
            set
            {
                base.dtModel.Rows[0]["TITLE"] = value;
            }
        }
        // model for database field SUFFIX(string)
        public string SUFFIX
        {
            get
            {
                return base.dtModel.Rows[0]["SUFFIX"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SUFFIX"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["SUFFIX"] = value;
            }
        }
        // model for database field FIRST_NAME(string)
        public string FIRST_NAME
        {
            get
            {
                return base.dtModel.Rows[0]["FIRST_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["FIRST_NAME"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["FIRST_NAME"] = value;
            }
        }
        // model for database field MIDDLE_NAME(string)
        public string MIDDLE_NAME
        {
            get
            {
                return base.dtModel.Rows[0]["MIDDLE_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["MIDDLE_NAME"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["MIDDLE_NAME"] = value;
            }
        }
        // model for database field LAST_NAME(string)
        public string LAST_NAME
        {
            get
            {
                return base.dtModel.Rows[0]["LAST_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LAST_NAME"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["LAST_NAME"] = value;
            }
        }
        // model for database field ADDRESS1(string)
        public string ADDRESS1
        {
            get
            {
                return base.dtModel.Rows[0]["ADDRESS1"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ADDRESS1"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["ADDRESS1"] = value;
            }
        }
        // model for database field ADDRESS2(string)
        public string ADDRESS2
        {
            get
            {
                return base.dtModel.Rows[0]["ADDRESS2"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ADDRESS2"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["ADDRESS2"] = value;
            }
        }
        // model for database field CITY(string)
        public string CITY
        {
            get
            {
                return base.dtModel.Rows[0]["CITY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CITY"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CITY"] = value;
            }
        }
        // model for database field COUNTRY(string)
        public string COUNTRY
        {
            get
            {
                return base.dtModel.Rows[0]["COUNTRY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["COUNTRY"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["COUNTRY"] = value;
            }
        }
        // model for database field STATE(string)
        public string STATE
        {
            get
            {
                return base.dtModel.Rows[0]["STATE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["STATE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["STATE"] = value;
            }
        }
        // model for database field ZIP_CODE(string)
        public string ZIP_CODE
        {
            get
            {
                return base.dtModel.Rows[0]["ZIP_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ZIP_CODE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["ZIP_CODE"] = value;
            }
        }
        // model for database field PHONE(string)
        public string PHONE
        {
            get
            {
                return base.dtModel.Rows[0]["PHONE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PHONE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["PHONE"] = value;
            }
        }

        // Added By Swastika on 21st mar'06 for Gen Iss #2367 :model for database field MOBILE(string)
        public string MOBILE
        {
            get
            {
                return base.dtModel.Rows[0]["MOBILE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["MOBILE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["MOBILE"] = value;
            }
        }

        // model for database field BUSINESS_PHONE(string)
        public string BUSINESS_PHONE
        {
            get
            {
                return base.dtModel.Rows[0]["BUSINESS_PHONE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["BUSINESS_PHONE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["BUSINESS_PHONE"] = value;
            }
        }
        // model for database field EXT(string)
        public string EXT
        {
            get
            {
                return base.dtModel.Rows[0]["EXT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["EXT"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["EXT"] = value;
            }
        }

        // model for database field EMAIL(string)
        public string EMAIL
        {
            get
            {
                return base.dtModel.Rows[0]["EMAIL"] == DBNull.Value ? "" : base.dtModel.Rows[0]["EMAIL"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["EMAIL"] = value;
            }
        }

        ///

        public string CUSTOMER_FIRST_NAME
        {
            get
            {
                return base.dtModel.Rows[0]["CUSTOMER_FIRST_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CUSTOMER_FIRST_NAME"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CUSTOMER_FIRST_NAME"] = value;
            }
        }

        // model for database field CUSTOMER_MIDDLE_NAME(string)
        public string CUSTOMER_MIDDLE_NAME
        {
            get
            {
                return base.dtModel.Rows[0]["CUSTOMER_MIDDLE_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CUSTOMER_MIDDLE_NAME"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CUSTOMER_MIDDLE_NAME"] = value;
            }
        }
        // model for database field CUSTOMER_LAST_NAME(string)
        public string CUSTOMER_LAST_NAME
        {
            get
            {
                return base.dtModel.Rows[0]["CUSTOMER_LAST_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CUSTOMER_LAST_NAME"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CUSTOMER_LAST_NAME"] = value;
            }
        }

        //Added By Lalit March 15,2010

        /// <summary>
        /// Model for database field CUSTOMER_CODE(string)
        /// </summary> 

        public string CUSTOMER_CODE
        {
            get
            {
                return base.dtModel.Rows[0]["CUSTOMER_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CUSTOMER_CODE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CUSTOMER_CODE"] = value;
            }
        }

        /// <summary>
        /// Model for database field Position(Int)
        /// </summary>    
        public int POSITION
        {

            get
            {
                return base.dtModel.Rows[0]["POSITION"] == DBNull.Value ? Convert.ToInt32(null) : Convert.ToInt32(base.dtModel.Rows[0]["POSITION"]);
            }
            set
            {
                base.dtModel.Rows[0]["POSITION"] = value;
            }
        }

        /// <summary>
        /// Model for database field CONTACT_CODE(String)
        /// </summary> 
        public string CONTACT_CODE
        {


            get
            {
                return base.dtModel.Rows[0]["CONTACT_CODE"] == DBNull.Value ? null : Convert.ToString(base.dtModel.Rows[0]["CONTACT_CODE"]);
            }
            set
            {
                base.dtModel.Rows[0]["CONTACT_CODE"] = value;
            }
        }
        /// <summary>
        /// Model for database field ID_TYPE(Int)
        /// </summary> 
        public int ID_TYPE
        {

            get
            {
                return base.dtModel.Rows[0]["ID_TYPE"] == DBNull.Value ? Convert.ToInt32(null) : Convert.ToInt32(base.dtModel.Rows[0]["ID_TYPE"]);
            }
            set
            {
                base.dtModel.Rows[0]["ID_TYPE"] = value;
            }
        }

        /// <summary>
        /// Model for database field ID_TYPE_NUMBER(String)
        /// </summary> 
        public string ID_TYPE_NUMBER
        {

            get
            {
                return base.dtModel.Rows[0]["ID_TYPE_NUMBER"] == DBNull.Value ? null : Convert.ToString(base.dtModel.Rows[0]["ID_TYPE_NUMBER"]);
            }
            set
            {
                base.dtModel.Rows[0]["ID_TYPE_NUMBER"] = value;
            }
        }
        /// <summary>
        /// Model for database field NUMBER(String)
        /// </summary> 
        public string NUMBER
        {

            get
            {
                return base.dtModel.Rows[0]["NUMBER"] == DBNull.Value ? null : Convert.ToString(base.dtModel.Rows[0]["NUMBER"]);
            }
            set
            {
                base.dtModel.Rows[0]["NUMBER"] = value;
            }
        }

        /// <summary>
        /// Model for database field COMPLIMENT(String)
        /// </summary> 
        public string COMPLIMENT
        {
            get
            {
                return base.dtModel.Rows[0]["COMPLIMENT"] == DBNull.Value ? null : Convert.ToString(base.dtModel.Rows[0]["COMPLIMENT"]);
            }
            set
            {
                base.dtModel.Rows[0]["COMPLIMENT"] = value;
            }
        }
        /// <summary>
        /// Model for database field DISTRICT(String)
        /// </summary> 
        public string DISTRICT
        {
            get
            {
                return base.dtModel.Rows[0]["DISTRICT"] == DBNull.Value ? null : Convert.ToString(base.dtModel.Rows[0]["DISTRICT"]);
            }
            set
            {
                base.dtModel.Rows[0]["DISTRICT"] = value;
            }
        }

        /// <summary>
        /// Model for database field NOTE(String)
        /// </summary>
        public string NOTE
        {
            get
            {
                return base.dtModel.Rows[0]["NOTE"] == DBNull.Value ? null : Convert.ToString(base.dtModel.Rows[0]["NOTE"]);
            }
            set
            {
                base.dtModel.Rows[0]["NOTE"] = value;
            }
        }
         /// <summary>
        /// Model for database field REGIONAL_IDENTIFICATION(String)
        /// </summary>
        public string REGIONAL_IDENTIFICATION
        {
            get
            {
                return base.dtModel.Rows[0]["REGIONAL_IDENTIFICATION"] == DBNull.Value ? null : Convert.ToString(base.dtModel.Rows[0]["REGIONAL_IDENTIFICATION"]);
            }
            set
            {
                base.dtModel.Rows[0]["REGIONAL_IDENTIFICATION"] = value;
            }
        }
         /// <summary>
        /// Model for database field REG_ID_ISSUE(Int)
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
        /// Model for database field CPF_CNPJ(String)
        /// </summary>
        public String CPF_CNPJ
        {
            get
            {
                return base.dtModel.Rows[0]["CPF_CNPJ"] == DBNull.Value ? Convert.ToString(null) : Convert.ToString(base.dtModel.Rows[0]["CPF_CNPJ"]);
            }
            set
            {
                base.dtModel.Rows[0]["CPF_CNPJ"] = value;
            }
        }
        /// Model for database field FAX(String)
        /// </summary>
        public String FAX
        {
            get
            {
                return base.dtModel.Rows[0]["FAX"] == DBNull.Value ? Convert.ToString(null) : Convert.ToString(base.dtModel.Rows[0]["FAX"]);
            }
            set
            {
                base.dtModel.Rows[0]["FAX"] = value;
            }
        }
        /// Model for database field TITLE(Int)
        /// </summary>
        public int TYPE
        {
            get
            {
                return base.dtModel.Rows[0]["TYPE"] == DBNull.Value ? Convert.ToInt32(null) : Convert.ToInt32(base.dtModel.Rows[0]["TYPE"]);
            }
            set
            {
                base.dtModel.Rows[0]["TYPE"] = value;
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
            
            
            

        #endregion
    }
}




	