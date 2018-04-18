/******************************************************************************************
<Author				: -   Vijay Arora
<Start Date			: -	10/27/2005 9:54:51 AM
<End Date			: -	
<Description		: - 	Model Class of Policy Customer Table
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using System.Data;
using Cms.Model;
namespace Cms.Model.Policy
{
	/// <summary>
	/// Database Model for POL_CUSTOMER_POLICY_LIST.
	/// </summary>
	public class ClsPolicyInfo : Cms.Model.ClsCommonModel
	{
		private const string POL_CUSTOMER_POLICY_LIST = "POL_CUSTOMER_POLICY_LIST";
		public ClsPolicyInfo()
		{
			base.dtModel.TableName = "POL_CUSTOMER_POLICY_LIST";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table POL_CUSTOMER_POLICY_LIST
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("APP_ID",typeof(int));
			base.dtModel.Columns.Add("APP_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_TYPE",typeof(string));
			base.dtModel.Columns.Add("POLICY_NUMBER",typeof(string));
			base.dtModel.Columns.Add("POLICY_DISP_VERSION",typeof(string));
			base.dtModel.Columns.Add("POLICY_STATUS",typeof(string));
			base.dtModel.Columns.Add("POLICY_LOB",typeof(string));
			base.dtModel.Columns.Add("POLICY_SUBLOB",typeof(string));
			base.dtModel.Columns.Add("POLICY_TERMS",typeof(string));
			base.dtModel.Columns.Add("POLICY_EFFECTIVE_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("POLICY_EXPIRATION_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("POLICY_DESCRIPTION",typeof(string));
			base.dtModel.Columns.Add("ACCOUNT_EXEC",typeof(int));
			base.dtModel.Columns.Add("CSR",typeof(int));
			base.dtModel.Columns.Add("PRODUCER",typeof(int));
			base.dtModel.Columns.Add("UNDERWRITER",typeof(int));
			base.dtModel.Columns.Add("PROCESS_STATUS",typeof(int));
			base.dtModel.Columns.Add("IS_UNDER_CONFIRMATION",typeof(string));
			base.dtModel.Columns.Add("LAST_PROCESS",typeof(string));
			base.dtModel.Columns.Add("LAST_PROCESS_COMPLETED",typeof(DateTime));
			base.dtModel.Columns.Add("POLICY_ACCOUNT_STATUS",typeof(int));
			base.dtModel.Columns.Add("AGENCY_ID",typeof(int));
			base.dtModel.Columns.Add("PARENT_APP_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("APP_STATUS",typeof(string));
			base.dtModel.Columns.Add("APP_NUMBER",typeof(string));
			base.dtModel.Columns.Add("APP_VERSION",typeof(string));
			base.dtModel.Columns.Add("APP_TERMS",typeof(string));
			base.dtModel.Columns.Add("APP_INCEPTION_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("APP_EFFECTIVE_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("APP_EXPIRATION_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("IS_UNDER_REVIEW",typeof(string));
			base.dtModel.Columns.Add("COUNTRY_ID",typeof(int));
			base.dtModel.Columns.Add("STATE_ID",typeof(int));
			base.dtModel.Columns.Add("DIV_ID",typeof(int));
			base.dtModel.Columns.Add("DEPT_ID",typeof(int));
			base.dtModel.Columns.Add("PC_ID",typeof(int));
			base.dtModel.Columns.Add("BILL_TYPE",typeof(int));
			base.dtModel.Columns.Add("COMPLETE_APP",typeof(string));
			base.dtModel.Columns.Add("PROPRTY_INSP_CREDIT",typeof(string));
			base.dtModel.Columns.Add("INSTALL_PLAN_ID",typeof(int));
			base.dtModel.Columns.Add("CHARGE_OFF_PRMIUM",typeof(string));
			base.dtModel.Columns.Add("RECEIVED_PRMIUM",typeof(double));
			base.dtModel.Columns.Add("PROXY_SIGN_OBTAINED",typeof(int));
			base.dtModel.Columns.Add("SHOW_QUOTE",typeof(string));
			base.dtModel.Columns.Add("APP_VERIFICATION_XML",typeof(string));
			base.dtModel.Columns.Add("YEAR_AT_CURR_RESI",typeof(double));
			base.dtModel.Columns.Add("YEARS_AT_PREV_ADD",typeof(string));
			base.dtModel.Columns.Add("PIC_OF_LOC",typeof(string));
			base.dtModel.Columns.Add("IS_HOME_EMP",typeof(bool));
			base.dtModel.Columns.Add("DOWN_PAY_MODE",typeof(int));
			base.dtModel.Columns.Add("NOT_RENEW",typeof(string));
			base.dtModel.Columns.Add("NOT_RENEW_REASON",typeof(int));
			base.dtModel.Columns.Add("REFER_UNDERWRITER",typeof(string));
			base.dtModel.Columns.Add("REFERAL_INSTRUCTIONS",typeof(string));
			base.dtModel.Columns.Add("REINS_SPECIAL_ACPT",typeof(int));

            //Added by Charles on 17-Mar-10 for Policy Page Implementation
            base.dtModel.Columns.Add("POLICY_CURRENCY", typeof(int));
            base.dtModel.Columns.Add("POLICY_LEVEL_COMISSION", typeof(double));
            
            base.dtModel.Columns.Add("BILLTO", typeof(string));
            base.dtModel.Columns.Add("PAYOR", typeof(int));
            base.dtModel.Columns.Add("CO_INSURANCE", typeof(int));
            base.dtModel.Columns.Add("CONTACT_PERSON", typeof(int));
            //Added till here

            //Added by Charles on 13-May-10 for Policy Page Implementation
            base.dtModel.Columns.Add("POLICY_LEVEL_COMM_APPLIES", typeof(string));
            base.dtModel.Columns.Add("TRANSACTION_TYPE", typeof(int));
            base.dtModel.Columns.Add("PREFERENCE_DAY", typeof(int));
            base.dtModel.Columns.Add("BROKER_REQUEST_NO", typeof(string));
            //base.dtModel.Columns.Add("REMARKS", typeof(string));
            base.dtModel.Columns.Add("BROKER_COMM_FIRST_INSTM", typeof(string));
            //Added till here

            //Added by Agniswar for Singapore Implementation
            base.dtModel.Columns.Add("BILLING_CURRENCY", typeof(int));
            base.dtModel.Columns.Add("FUND_TYPE", typeof(int));


            
		}
		#region Database schema details

        //Added by Charles on 17-Mar-10 for Policy Page Implementation
        public int POLICY_CURRENCY
        {
            get
            {
                return base.dtModel.Rows[0]["POLICY_CURRENCY"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["POLICY_CURRENCY"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["POLICY_CURRENCY"] = value;
            }
        }
        public int PREFERENCE_DAY
        {
            get
            {
                return base.dtModel.Rows[0]["PREFERENCE_DAY"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PREFERENCE_DAY"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["PREFERENCE_DAY"] = value;
            }
        }
        public int TRANSACTION_TYPE
        {
            get
            {
                return base.dtModel.Rows[0]["TRANSACTION_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["TRANSACTION_TYPE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["TRANSACTION_TYPE"] = value;
            }
        }
        public int PAYOR
        {
            get
            {
                return base.dtModel.Rows[0]["PAYOR"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PAYOR"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["PAYOR"] = value;
            }
        }
        public int CO_INSURANCE
        {
            get
            {
                return base.dtModel.Rows[0]["CO_INSURANCE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CO_INSURANCE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["CO_INSURANCE"] = value;
            }
        }
        public int CONTACT_PERSON
        {
            get
            {
                return base.dtModel.Rows[0]["CONTACT_PERSON"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CONTACT_PERSON"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["CONTACT_PERSON"] = value;
            }
        }
        public string BROKER_COMM_FIRST_INSTM
        {
            get
            {
                return base.dtModel.Rows[0]["BROKER_COMM_FIRST_INSTM"] == DBNull.Value ? "" : base.dtModel.Rows[0]["BROKER_COMM_FIRST_INSTM"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["BROKER_COMM_FIRST_INSTM"] = value;
            }
        }
        public string BROKER_REQUEST_NO
        {
            get
            {
                return base.dtModel.Rows[0]["BROKER_REQUEST_NO"] == DBNull.Value ? "" : base.dtModel.Rows[0]["BROKER_REQUEST_NO"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["BROKER_REQUEST_NO"] = value;
            }
        }
        //public string REMARKS
        //{
        //    get
        //    {
        //        return base.dtModel.Rows[0]["REMARKS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REMARKS"].ToString();
        //    }
        //    set
        //    {
        //        base.dtModel.Rows[0]["REMARKS"] = value;
        //    }
        //}
        public string BILLTO
        {
            get
            {
                return base.dtModel.Rows[0]["BILLTO"] == DBNull.Value ? "" : base.dtModel.Rows[0]["BILLTO"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["BILLTO"] = value;
            }
        }
        public double POLICY_LEVEL_COMISSION
        {
            get
            {
                return base.dtModel.Rows[0]["POLICY_LEVEL_COMISSION"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["POLICY_LEVEL_COMISSION"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["POLICY_LEVEL_COMISSION"] = value;
            }
        }
        //Added till here

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
		// model for database field APP_ID(int)
		public int APP_ID
		{
			get
			{
				return base.dtModel.Rows[0]["APP_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["APP_ID"].ToString());
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
				return base.dtModel.Rows[0]["APP_VERSION_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["APP_VERSION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APP_VERSION_ID"] = value;
			}
		}
		// model for database field POLICY_TYPE(string)
		public string POLICY_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["POLICY_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_TYPE"] = value;
			}
		}
		// model for database field POLICY_NUMBER(string)
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
		// model for database field POLICY_DISP_VERSION(string)
		public string POLICY_DISP_VERSION
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_DISP_VERSION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["POLICY_DISP_VERSION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_DISP_VERSION"] = value;
			}
		}
		// model for database field POLICY_STATUS(string)
		public string POLICY_STATUS
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_STATUS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["POLICY_STATUS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_STATUS"] = value;
			}
		}
		// model for database field POLICY_LOB(string)
		public string POLICY_LOB
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_LOB"] == DBNull.Value ? "" : base.dtModel.Rows[0]["POLICY_LOB"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_LOB"] = value;
			}
		}
		// model for database field POLICY_SUBLOB(string)
		public string POLICY_SUBLOB
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_SUBLOB"] == DBNull.Value ? "" : base.dtModel.Rows[0]["POLICY_SUBLOB"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_SUBLOB"] = value;
			}
		}
		// model for database field POLICY_TERMS(string)
		public string POLICY_TERMS
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_TERMS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["POLICY_TERMS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_TERMS"] = value;
			}
		}
		// model for database field POLICY_EFFECTIVE_DATE(DateTime)
		public DateTime POLICY_EFFECTIVE_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_EFFECTIVE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["POLICY_EFFECTIVE_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_EFFECTIVE_DATE"] = value;
			}
		}
		// model for database field POLICY_EXPIRATION_DATE(DateTime)
		public DateTime POLICY_EXPIRATION_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_EXPIRATION_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["POLICY_EXPIRATION_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_EXPIRATION_DATE"] = value;
			}
		}
		// model for database field POLICY_DESCRIPTION(string)
		public string POLICY_DESCRIPTION
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_DESCRIPTION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["POLICY_DESCRIPTION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_DESCRIPTION"] = value;
			}
		}
		// model for database field ACCOUNT_EXEC(int)
		public int ACCOUNT_EXEC
		{
			get
			{
				return base.dtModel.Rows[0]["ACCOUNT_EXEC"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ACCOUNT_EXEC"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ACCOUNT_EXEC"] = value;
			}
		}
		// model for database field CSR(int)
		public int CSR
		{
			get
			{
				return base.dtModel.Rows[0]["CSR"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["CSR"].ToString()); //Changed from 0, Charles (7-Dec-09), Itrack 6791
			}
			set
			{
				base.dtModel.Rows[0]["CSR"] = value;
			}
		}
		public int PRODUCER
		{
			get
			{
				return base.dtModel.Rows[0]["PRODUCER"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PRODUCER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PRODUCER"] = value;
			}
		}
		// model for database field UNDERWRITER(int)
		public int UNDERWRITER
		{
			get
			{
				return base.dtModel.Rows[0]["UNDERWRITER"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["UNDERWRITER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["UNDERWRITER"] = value;
			}
		}
		// model for database field PROCESS_STATUS(int)
		public int PROCESS_STATUS
		{
			get
			{
				return base.dtModel.Rows[0]["PROCESS_STATUS"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PROCESS_STATUS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PROCESS_STATUS"] = value;
			}
		}
		// model for database field IS_UNDER_CONFIRMATION(string)
		public string IS_UNDER_CONFIRMATION
		{
			get
			{
				return base.dtModel.Rows[0]["IS_UNDER_CONFIRMATION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IS_UNDER_CONFIRMATION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_UNDER_CONFIRMATION"] = value;
			}
		}
		// model for database field LAST_PROCESS(string)
		public string LAST_PROCESS
		{
			get
			{
				return base.dtModel.Rows[0]["LAST_PROCESS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LAST_PROCESS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LAST_PROCESS"] = value;
			}
		}
		// model for database field LAST_PROCESS_COMPLETED(DateTime)
		public DateTime LAST_PROCESS_COMPLETED
		{
			get
			{
				return base.dtModel.Rows[0]["LAST_PROCESS_COMPLETED"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["LAST_PROCESS_COMPLETED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LAST_PROCESS_COMPLETED"] = value;
			}
		}
		
		// model for database field POLICY_ACCOUNT_STATUS(int)
		public int POLICY_ACCOUNT_STATUS
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_ACCOUNT_STATUS"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["POLICY_ACCOUNT_STATUS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_ACCOUNT_STATUS"] = value;
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
		// model for database field PARENT_APP_VERSION_ID(int)
		public int PARENT_APP_VERSION_ID
		{
			get
			{
				return base.dtModel.Rows[0]["PARENT_APP_VERSION_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PARENT_APP_VERSION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PARENT_APP_VERSION_ID"] = value;
			}
		}
		// model for database field APP_STATUS(string)
		public string APP_STATUS
		{
			get
			{
				return base.dtModel.Rows[0]["APP_STATUS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["APP_STATUS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["APP_STATUS"] = value;
			}
		}
		// model for database field APP_NUMBER(string)
		public string APP_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["APP_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["APP_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["APP_NUMBER"] = value;
			}
		}
		// model for database field APP_VERSION(string)
		public string APP_VERSION
		{
			get
			{
				return base.dtModel.Rows[0]["APP_VERSION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["APP_VERSION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["APP_VERSION"] = value;
			}
		}
		// model for database field APP_TERMS(string)
		public string APP_TERMS
		{
			get
			{
				return base.dtModel.Rows[0]["APP_TERMS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["APP_TERMS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["APP_TERMS"] = value;
			}
		}
		// model for database field APP_INCEPTION_DATE(DateTime)
		public DateTime APP_INCEPTION_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["APP_INCEPTION_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["APP_INCEPTION_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APP_INCEPTION_DATE"] = value;
			}
		}
		// model for database field APP_EFFECTIVE_DATE(DateTime)
		public DateTime APP_EFFECTIVE_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["APP_EFFECTIVE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["APP_EFFECTIVE_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APP_EFFECTIVE_DATE"] = value;
			}
		}
		// model for database field APP_EXPIRATION_DATE(DateTime)
		public DateTime APP_EXPIRATION_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["APP_EXPIRATION_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["APP_EXPIRATION_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APP_EXPIRATION_DATE"] = value;
			}
		}
		// model for database field IS_UNDER_REVIEW(string)
		public string IS_UNDER_REVIEW
		{
			get
			{
				return base.dtModel.Rows[0]["IS_UNDER_REVIEW"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IS_UNDER_REVIEW"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_UNDER_REVIEW"] = value;
			}
		}
		// model for database field COUNTRY_ID(int)
		public int COUNTRY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["COUNTRY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["COUNTRY_ID"].ToString());
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
				return base.dtModel.Rows[0]["STATE_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["STATE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["STATE_ID"] = value;
			}
		}
		// model for database field DIV_ID(int)
		public int DIV_ID
		{
			get
			{
				return base.dtModel.Rows[0]["DIV_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["DIV_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DIV_ID"] = value;
			}
		}
		// model for database field DEPT_ID(int)
		public int DEPT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["DEPT_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["DEPT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DEPT_ID"] = value;
			}
		}
		// model for database field PC_ID(int)
		public int PC_ID
		{
			get
			{
				return base.dtModel.Rows[0]["PC_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PC_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PC_ID"] = value;
			}
		}
		// model for database field BILL_TYPE(string)
		public int BILL_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["BILL_TYPE"] == DBNull.Value ? 0 :Convert.ToInt32(base.dtModel.Rows[0]["BILL_TYPE"]);
			}
			set
			{
				base.dtModel.Rows[0]["BILL_TYPE"] = value;
			}
		}
		// model for database field COMPLETE_APP(string)
		public string COMPLETE_APP
		{
			get
			{
				return base.dtModel.Rows[0]["COMPLETE_APP"] == DBNull.Value ? "" : base.dtModel.Rows[0]["COMPLETE_APP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["COMPLETE_APP"] = value;
			}
		}
        public string POLICY_LEVEL_COMM_APPLIES
        {
            get
            {
                return base.dtModel.Rows[0]["POLICY_LEVEL_COMM_APPLIES"] == DBNull.Value ? "" : base.dtModel.Rows[0]["POLICY_LEVEL_COMM_APPLIES"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["POLICY_LEVEL_COMM_APPLIES"] = value;
            }
        }
		// model for database field PROPRTY_INSP_CREDIT(string)
		public string PROPRTY_INSP_CREDIT
		{
			get
			{
				return base.dtModel.Rows[0]["PROPRTY_INSP_CREDIT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PROPRTY_INSP_CREDIT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PROPRTY_INSP_CREDIT"] = value;
			}
		}
		// model for database field INSTALL_PLAN_ID(int)
		public int INSTALL_PLAN_ID
		{
			get
			{
				return base.dtModel.Rows[0]["INSTALL_PLAN_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["INSTALL_PLAN_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INSTALL_PLAN_ID"] = value;
			}
		}
		// model for database field CHARGE_OFF_PRMIUM(string)
		public string CHARGE_OFF_PRMIUM
		{
			get
			{
				return base.dtModel.Rows[0]["CHARGE_OFF_PRMIUM"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CHARGE_OFF_PRMIUM"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CHARGE_OFF_PRMIUM"] = value;
			}
		}
		// model for database field RECEIVED_PRMIUM(double)
		public double RECEIVED_PRMIUM
		{
			get
			{
				return base.dtModel.Rows[0]["RECEIVED_PRMIUM"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["RECEIVED_PRMIUM"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RECEIVED_PRMIUM"] = value;
			}
		}
		// model for database field PROXY_SIGN_OBTAINED(int)
		public int PROXY_SIGN_OBTAINED
		{
			get
			{
				return base.dtModel.Rows[0]["PROXY_SIGN_OBTAINED"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PROXY_SIGN_OBTAINED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PROXY_SIGN_OBTAINED"] = value;
			}
		}
		// model for database field SHOW_QUOTE(string)
		public string SHOW_QUOTE
		{
			get
			{
				return base.dtModel.Rows[0]["SHOW_QUOTE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SHOW_QUOTE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SHOW_QUOTE"] = value;
			}
		}
		// model for database field APP_VERIFICATION_XML(string)
		public string APP_VERIFICATION_XML
		{
			get
			{
				return base.dtModel.Rows[0]["APP_VERIFICATION_XML"] == DBNull.Value ? "" : base.dtModel.Rows[0]["APP_VERIFICATION_XML"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["APP_VERIFICATION_XML"] = value;
			}
		}
		// model for database field YEAR_AT_CURR_RESI(double)
		public double YEAR_AT_CURR_RESI
		{
			get
			{
				return base.dtModel.Rows[0]["YEAR_AT_CURR_RESI"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["YEAR_AT_CURR_RESI"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["YEAR_AT_CURR_RESI"] = value;
			}
		}
		// model for database field YEARS_AT_PREV_ADD(string)
		public string YEARS_AT_PREV_ADD
		{
			get
			{
				return base.dtModel.Rows[0]["YEARS_AT_PREV_ADD"] == DBNull.Value ? "" : base.dtModel.Rows[0]["YEARS_AT_PREV_ADD"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["YEARS_AT_PREV_ADD"] = value;
			}
		}
		// model for database field MVR_WIN_SERVICE(string)
		public string MVR_WIN_SERVICE
		{
			get
			{
				return base.dtModel.Rows[0]["MVR_WIN_SERVICE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["MVR_WIN_SERVICE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MVR_WIN_SERVICE"] = value;
			}
		}
		
		// model for database field PIC_OF_LOC(string)
		public string PIC_OF_LOC
		{
			get
			{
				return base.dtModel.Rows[0]["PIC_OF_LOC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PIC_OF_LOC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PIC_OF_LOC"] = value;
			}
		}

		public bool IS_HOME_EMP
		{
			get
			{
				return base.dtModel.Rows[0]["IS_HOME_EMP"] == DBNull.Value ? Convert.ToBoolean(null) : bool.Parse(base.dtModel.Rows[0]["IS_HOME_EMP"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["IS_HOME_EMP"] = value;
			}
		}
		


	    // model for database field DOWN_PAY_MODE(int)
	    public int DOWN_PAY_MODE
	    {
		    get
		    {
			    return base.dtModel.Rows[0]["DOWN_PAY_MODE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["DOWN_PAY_MODE"].ToString());
		    }
		    set
		    {
			    base.dtModel.Rows[0]["DOWN_PAY_MODE"] = value;
		    }
	    }
		
	    // model for database field NOT_RENEW(string)
	    public string NOT_RENEW
	    {
		    get
		    {
			    return base.dtModel.Rows[0]["NOT_RENEW"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["NOT_RENEW"].ToString();
		    }
		    set
		    {
			    base.dtModel.Rows[0]["NOT_RENEW"] = value;
		    }
	    }
		// model for database field NOT_RENEW_REASON(int)
		public int NOT_RENEW_REASON
		{
			get
			{
				return base.dtModel.Rows[0]["NOT_RENEW_REASON"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["NOT_RENEW_REASON"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NOT_RENEW_REASON"] = value;
			}
		}
		// model for database field REFER_UNDERWRITER(string)
		public string REFER_UNDERWRITER
		{
			get
			{
				return base.dtModel.Rows[0]["REFER_UNDERWRITER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["REFER_UNDERWRITER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REFER_UNDERWRITER"] = value;
			}
		}
		// model for database field REFERAL_INSTRUCTIONS(string)
		public string REFERAL_INSTRUCTIONS
		{
			get
			{
				return base.dtModel.Rows[0]["REFERAL_INSTRUCTIONS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["REFERAL_INSTRUCTIONS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REFERAL_INSTRUCTIONS"] = value;
			}
		}
		// model for database field REINS_SPECIAL_ACPT(int)
		public int REINS_SPECIAL_ACPT
		{
			get
			{
				return base.dtModel.Rows[0]["REINS_SPECIAL_ACPT"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["REINS_SPECIAL_ACPT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REINS_SPECIAL_ACPT"] = value;
			}
		}

        // model for database field BILLING_CURRENCY(int)
        public int BILLING_CURRENCY
        {
            get
            {
                return base.dtModel.Rows[0]["BILLING_CURRENCY"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["BILLING_CURRENCY"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["BILLING_CURRENCY"] = value;
            }
        }

        // model for database field FUND_TYPE(int)
        public int FUND_TYPE
        {
            get
            {
                return base.dtModel.Rows[0]["FUND_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["FUND_TYPE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["FUND_TYPE"] = value;
            }
        }
		
#endregion
	}
}
