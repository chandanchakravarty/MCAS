/******************************************************************************************
<Author					: -   Nidhi
<Start Date				: -	1/4/2006 5:41:53 PM
<End Date				: -	
<Description			: - 	
<Review Date			: - 
<Reviewed By			: - 	
<Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using System.Data;
using Cms.Model;

namespace Cms.Model.Maintenance.Reinsurance
{
	/// <summary>
	/// Database Model for MNT_REINSURANCE_CONTRACT.
	/// </summary>
	public class ClsReinsuranceInfo : Cms.Model.ClsCommonModel
	{
		private const string MNT_REINSURANCE_CONTRACT = "MNT_REINSURANCE_CONTRACT";
		
		public ClsReinsuranceInfo()
		{
			base.dtModel.TableName = "MNT_REINSURANCE_CONTRACT";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table MNT_REINSURANCE_CONTRACT
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CONTRACT_ID",typeof(int));
			base.dtModel.Columns.Add("CONTRACT_TYPE",typeof(int));
			base.dtModel.Columns.Add("CONTRACT_NUMBER",typeof(string));
			base.dtModel.Columns.Add("CONTRACT_DESC",typeof(string));
			base.dtModel.Columns.Add("LOSS_ADJUSTMENT_EXPENSE",typeof(string));
			base.dtModel.Columns.Add("RISK_EXPOSURE",typeof(string));
			base.dtModel.Columns.Add("CONTRACT_LOB",typeof(string));
			base.dtModel.Columns.Add("STATE_ID",typeof(string));
			base.dtModel.Columns.Add("ORIGINAL_CONTACT_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("CONTACT_YEAR",typeof(string));
			base.dtModel.Columns.Add("EFFECTIVE_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("EXPIRATION_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("COMMISSION",typeof(double));
			base.dtModel.Columns.Add("CALCULATION_BASE",typeof(int));
            base.dtModel.Columns.Add("CASH_CALL_LIMIT", typeof(double));
		
			base.dtModel.Columns.Add("PER_OCCURRENCE_LIMIT",typeof(string));
			base.dtModel.Columns.Add("ANNUAL_AGGREGATE",typeof(string));
			base.dtModel.Columns.Add("DEPOSIT_PREMIUMS",typeof(string));
			base.dtModel.Columns.Add("DEPOSIT_PREMIUM_PAYABLE",typeof(string));
			base.dtModel.Columns.Add("MINIMUM_PREMIUM",typeof(string));
			base.dtModel.Columns.Add("SEQUENCE_NUMBER",typeof(string));
			base.dtModel.Columns.Add("TERMINATION_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("TERMINATION_REASON",typeof(string));
			base.dtModel.Columns.Add("COMMENTS",typeof(string));
			base.dtModel.Columns.Add("FOLLOW_UP_FIELDS",typeof(string));
			base.dtModel.Columns.Add("COMMISSION_APPLICABLE",typeof(int));
			base.dtModel.Columns.Add("REINSURANCE_PREMIUM_ACCOUNT",typeof(string));
			base.dtModel.Columns.Add("REINSURANCE_PAYABLE_ACCOUNT",typeof(string));
			base.dtModel.Columns.Add("REINSURANCE_COMMISSION_ACCOUNT",typeof(string));
            base.dtModel.Columns.Add("REINSURANCE_COMMISSION_RECEIVABLE_ACCOUNT",typeof(string));
			
			
			base.dtModel.Columns.Add("CONTRACT_NAME_ID",typeof(int));
			base.dtModel.Columns.Add("BROKERID",typeof(int));
			base.dtModel.Columns.Add("REINSURER_REFERENCE_NUM",typeof(string));
			base.dtModel.Columns.Add("UW_YEAR",typeof(int));
			base.dtModel.Columns.Add("ASLOB",typeof(long));
			base.dtModel.Columns.Add("SUBLINE_CODE",typeof(string));
			base.dtModel.Columns.Add("COVERAGE_CODE",typeof(string));
			base.dtModel.Columns.Add("CESSION",typeof(double));
			base.dtModel.Columns.Add("LOB_TLOG",typeof(string));
			base.dtModel.Columns.Add("STATE_TLOG",typeof(string));
			base.dtModel.Columns.Add("RISK_TLOG",typeof(string));
			base.dtModel.Columns.Add("FOLLOW_UP_FOR",typeof(int));
            base.dtModel.Columns.Add("MAX_NO_INSTALLMENT", typeof(int)); //Added by Aditya for TFS BUG # 2512
		
			
		}
		#region Database schema details
		// model for database field CONTRACT_ID(int)
		public int CONTRACT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CONTRACT_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CONTRACT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CONTRACT_ID"] = value;
			}
		}
		// model for database field CONTRACT_TYPE(int)
		public int CONTRACT_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["CONTRACT_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CONTRACT_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CONTRACT_TYPE"] = value;
			}
		}
		// model for database field CONTRACT_NUMBER(string)
		public string CONTRACT_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["CONTRACT_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CONTRACT_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTRACT_NUMBER"] = value;
			}
		}
		
		// model for database field CONTRACT_DESC(string)
		public string CONTRACT_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["CONTRACT_DESC"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CONTRACT_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTRACT_DESC"] = value;
			}
		}
	
		public string LOSS_ADJUSTMENT_EXPENSE
		{
			get
			{
				return base.dtModel.Rows[0]["LOSS_ADJUSTMENT_EXPENSE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LOSS_ADJUSTMENT_EXPENSE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOSS_ADJUSTMENT_EXPENSE"] = value;
			}
		}
		public string RISK_EXPOSURE
		{
			get
			{
				return base.dtModel.Rows[0]["RISK_EXPOSURE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["RISK_EXPOSURE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["RISK_EXPOSURE"] = value;
			}
		}
	
		// model for database field CONTRACT_LOB(int)
		public string CONTRACT_LOB
		{
			get
			{
				return base.dtModel.Rows[0]["CONTRACT_LOB"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CONTRACT_LOB"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTRACT_LOB"] = value;
			}
		}
		// model for database field STATE_ID(int)
		public string STATE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["STATE_ID"] == DBNull.Value ? "" : base.dtModel.Rows[0]["STATE_ID"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["STATE_ID"] = value;
			}
		}

		public DateTime ORIGINAL_CONTACT_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["ORIGINAL_CONTACT_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["ORIGINAL_CONTACT_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ORIGINAL_CONTACT_DATE"] = value;
			}
		}
		public string CONTACT_YEAR
		{
			get
			{
				return base.dtModel.Rows[0]["CONTACT_YEAR"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CONTACT_YEAR"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTACT_YEAR"] = value;
			}
		}
	
		// model for database field EFFECTIVE_DATE(DateTime)
		public DateTime EFFECTIVE_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["EFFECTIVE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["EFFECTIVE_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EFFECTIVE_DATE"] = value;
			}
		}
		// model for database field EXPIRATION_DATE(DateTime)
		public DateTime EXPIRATION_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["EXPIRATION_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["EXPIRATION_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EXPIRATION_DATE"] = value;
			}
		}

        public double COMMISSION
		{
			get
			{
				return base.dtModel.Rows[0]["COMMISSION"] == DBNull.Value ? 0 :double.Parse(base.dtModel.Rows[0]["COMMISSION"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COMMISSION"] = value;
			}
		}
       
		public int CALCULATION_BASE
		{
			get
			{
				return base.dtModel.Rows[0]["CALCULATION_BASE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CALCULATION_BASE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CALCULATION_BASE"] = value;
			}
		}
		

		public string PER_OCCURRENCE_LIMIT
		{
			get
			{
				return base.dtModel.Rows[0]["PER_OCCURRENCE_LIMIT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PER_OCCURRENCE_LIMIT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PER_OCCURRENCE_LIMIT"] = value;
			}
		}
		
		public string ANNUAL_AGGREGATE
		{
			get
			{
				return base.dtModel.Rows[0]["ANNUAL_AGGREGATE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ANNUAL_AGGREGATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANNUAL_AGGREGATE"] = value;
			}
		}
		public string DEPOSIT_PREMIUMS
		{
			get
			{
				return base.dtModel.Rows[0]["DEPOSIT_PREMIUMS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DEPOSIT_PREMIUMS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DEPOSIT_PREMIUMS"] = value;
			}
		}
		public string DEPOSIT_PREMIUM_PAYABLE
		{
			get
			{
				return base.dtModel.Rows[0]["DEPOSIT_PREMIUM_PAYABLE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DEPOSIT_PREMIUM_PAYABLE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DEPOSIT_PREMIUM_PAYABLE"] = value;
			}
		}
		public string MINIMUM_PREMIUM
		{
			get
			{
				return base.dtModel.Rows[0]["MINIMUM_PREMIUM"] == DBNull.Value ? "" : base.dtModel.Rows[0]["MINIMUM_PREMIUM"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MINIMUM_PREMIUM"] = value;
			}
		}
		public string SEQUENCE_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["SEQUENCE_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SEQUENCE_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SEQUENCE_NUMBER"] = value;
			}
		}

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
		
		public string TERMINATION_REASON
		{
			get
			{
				return base.dtModel.Rows[0]["TERMINATION_REASON"] == DBNull.Value ? "" : base.dtModel.Rows[0]["TERMINATION_REASON"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TERMINATION_REASON"] = value;
			}
		}
		public string COMMENTS
		{
			get
			{
				return base.dtModel.Rows[0]["COMMENTS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["COMMENTS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["COMMENTS"] = value;
			}
		}
	
		public string FOLLOW_UP_FIELDS
		{
			get
			{
				return base.dtModel.Rows[0]["FOLLOW_UP_FIELDS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["FOLLOW_UP_FIELDS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["FOLLOW_UP_FIELDS"] = value;
			}
		}
		public int COMMISSION_APPLICABLE
		{
			get
			{
				return base.dtModel.Rows[0]["COMMISSION_APPLICABLE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["COMMISSION_APPLICABLE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COMMISSION_APPLICABLE"] = value;
			}
		}
		public string REINSURANCE_PREMIUM_ACCOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["REINSURANCE_PREMIUM_ACCOUNT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REINSURANCE_PREMIUM_ACCOUNT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REINSURANCE_PREMIUM_ACCOUNT"] = value;
			}
		}
		public string REINSURANCE_PAYABLE_ACCOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["REINSURANCE_PAYABLE_ACCOUNT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REINSURANCE_PAYABLE_ACCOUNT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REINSURANCE_PAYABLE_ACCOUNT"] = value;
			}
		}
		public string REINSURANCE_COMMISSION_ACCOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["REINSURANCE_COMMISSION_ACCOUNT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REINSURANCE_COMMISSION_ACCOUNT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REINSURANCE_COMMISSION_ACCOUNT"] = value;
			}
		}
		public string REINSURANCE_COMMISSION_RECEIVABLE_ACCOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["REINSURANCE_COMMISSION_RECEIVABLE_ACCOUNT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REINSURANCE_COMMISSION_RECEIVABLE_ACCOUNT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REINSURANCE_COMMISSION_RECEIVABLE_ACCOUNT"] = value;
			}
		}
		
		// model for database field REINSURER_REFERENCE_NUM(string)
		public string REINSURER_REFERENCE_NUM
		{
			get
			{
				return base.dtModel.Rows[0]["REINSURER_REFERENCE_NUM"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REINSURER_REFERENCE_NUM"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REINSURER_REFERENCE_NUM"] = value;
			}
		}
		// model for database field UW_YEAR(int)
		public int UW_YEAR
		{
			get
			{
				return base.dtModel.Rows[0]["UW_YEAR"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["UW_YEAR"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["UW_YEAR"] = value;
			}
		}
		// model for database field ASLOB(long)
		public long ASLOB
		{
			get
			{
				return base.dtModel.Rows[0]["ASLOB"] == DBNull.Value ? 0 : long.Parse(base.dtModel.Rows[0]["ASLOB"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ASLOB"] = value;
			}
		}
		// model for database field SUBLINE_CODE(string)
		public string SUBLINE_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["SUBLINE_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SUBLINE_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SUBLINE_CODE"] = value;
			}
		}
		// model for database field COVERAGE_CODE(string)
		public string COVERAGE_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["COVERAGE_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["COVERAGE_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["COVERAGE_CODE"] = value;
			}
		}
		// model for database field CESSION(double)
		public double CESSION
		{
			get
			{
				return base.dtModel.Rows[0]["CESSION"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["CESSION"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CESSION"] = value;
			}
		}
		// model for database field CONTRACT_NAME_ID(int)
		public int CONTRACT_NAME_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CONTRACT_NAME_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CONTRACT_NAME_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CONTRACT_NAME_ID"] = value;
			}
		}
		
		// model for database field BROKERID(int)
		public int BROKERID
		{
			get
			{
				return base.dtModel.Rows[0]["BROKERID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["BROKERID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["BROKERID"] = value;
			}
		}
		
		// model for database field LOB_TLOG(string)
		public string LOB_TLOG
		{
			get
			{
				return base.dtModel.Rows[0]["LOB_TLOG"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LOB_TLOG"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOB_TLOG"] = value;
			}
		}
		// model for database field STATE_TLOG(string)
		public string STATE_TLOG
		{
			get
			{
				return base.dtModel.Rows[0]["STATE_TLOG"] == DBNull.Value ? "" : base.dtModel.Rows[0]["STATE_TLOG"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["STATE_TLOG"] = value;
			}
		}
		// model for database field RISK_TLOG(string)
		public string RISK_TLOG
		{
			get
			{
				return base.dtModel.Rows[0]["RISK_TLOG"] == DBNull.Value ? "" : base.dtModel.Rows[0]["RISK_TLOG"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["RISK_TLOG"] = value;
			}
		}
		
		

		// model for database field FOLLOW_UP_FOR(int)
		public int FOLLOW_UP_FOR
		{
			get
			{
				return base.dtModel.Rows[0]["FOLLOW_UP_FOR"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["FOLLOW_UP_FOR"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["FOLLOW_UP_FOR"] = value;
			}
		}

        // model for database field CASH_CALL_LIMIT(double)
        public double CASH_CALL_LIMIT
        {
            get
            {
                return base.dtModel.Rows[0]["CASH_CALL_LIMIT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["CASH_CALL_LIMIT"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["CASH_CALL_LIMIT"] = value;
            }
        }

        // model for database field MAX_NO_INSTALLMENT(int)
        public int MAX_NO_INSTALLMENT  //Added by Aditya for TFS BUG # 2512
        {
            get
            {
                return base.dtModel.Rows[0]["MAX_NO_INSTALLMENT"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["MAX_NO_INSTALLMENT"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["MAX_NO_INSTALLMENT"] = value;
            }
        }

		#endregion
	}
}

