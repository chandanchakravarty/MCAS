/******************************************************************************************
<Author					: -   Vijay Joshi
<Start Date				: -	12/20/2005 1:14:03 PM
<End Date				: -	
<Description			: - 	Model class for process.
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

namespace Cms.Model.Policy.Process
{
	/// <summary>
	/// Database Model for POL_POLICY_PROCESS.
	/// </summary>
	public class ClsProcessInfo : Cms.Model.ClsBaseModel
	{
		private const string POL_POLICY_PROCESS = "POL_POLICY_PROCESS";
		public ClsProcessInfo()
		{
			base.dtModel.TableName = "POL_POLICY_PROCESS";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table POL_POLICY_PROCESS
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("ROW_ID",typeof(int));
			base.dtModel.Columns.Add("PROCESS_ID",typeof(int));
			base.dtModel.Columns.Add("PROCESS_TYPE",typeof(string));
			base.dtModel.Columns.Add("NEW_CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("NEW_POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("NEW_POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_PREVIOUS_STATUS",typeof(string));
			base.dtModel.Columns.Add("POLICY_CURRENT_STATUS",typeof(string));
			base.dtModel.Columns.Add("PROCESS_STATUS",typeof(string));
			base.dtModel.Columns.Add("CREATED_BY",typeof(int));
			base.dtModel.Columns.Add("CREATED_DATETIME",typeof(DateTime));
			base.dtModel.Columns.Add("COMPLETED_BY",typeof(int));
			base.dtModel.Columns.Add("COMPLETED_DATETIME",typeof(DateTime));
			base.dtModel.Columns.Add("COMMENTS",typeof(string));
			base.dtModel.Columns.Add("PRINT_COMMENTS",typeof(string));
			base.dtModel.Columns.Add("REQUESTED_BY",typeof(int));
			base.dtModel.Columns.Add("EFFECTIVE_DATETIME",typeof(DateTime));
			base.dtModel.Columns.Add("EXPIRY_DATE",typeof(DateTime));

			base.dtModel.Columns.Add("CANCELLATION_OPTION",typeof(int));
			base.dtModel.Columns.Add("CANCELLATION_TYPE",typeof(int));

			base.dtModel.Columns.Add("RESCIND_OPTION",typeof(int));
			base.dtModel.Columns.Add("RESCIND_TYPE",typeof(int));

			base.dtModel.Columns.Add("REASON",typeof(int));
			base.dtModel.Columns.Add("OTHER_REASON",typeof(string));
			base.dtModel.Columns.Add("RETURN_PREMIUM",typeof(double));
			base.dtModel.Columns.Add("PAST_DUE_PREMIUM",typeof(double));
			base.dtModel.Columns.Add("ENDORSEMENT_NO",typeof(int));
			base.dtModel.Columns.Add("PROPERTY_INSPECTION_CREDIT",typeof(string));
			base.dtModel.Columns.Add("POLICY_TERMS",typeof(int));
			base.dtModel.Columns.Add("NEW_POLICY_TERM_EFFECTIVE_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("NEW_POLICY_TERM_EXPIRATION_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("DIARY_LIST_ID",typeof(int));

			base.dtModel.Columns.Add("PRINTING_OPTIONS",typeof(int));
			base.dtModel.Columns.Add("INSURED",typeof(int));
			base.dtModel.Columns.Add("SEND_INSURED_COPY_TO",typeof(int));
			base.dtModel.Columns.Add("AUTO_ID_CARD",typeof(int));
			base.dtModel.Columns.Add("NO_COPIES",typeof(int));
			base.dtModel.Columns.Add("STD_LETTER_REQD",typeof(int));
			base.dtModel.Columns.Add("CUSTOM_LETTER_REQD",typeof(int));
			base.dtModel.Columns.Add("ADVERSE_LETTER_REQD",typeof(int));
			base.dtModel.Columns.Add("SEND_ALL",typeof(int));
			base.dtModel.Columns.Add("ADD_INT",typeof(int));
			base.dtModel.Columns.Add("ADD_INT_ID",typeof(string));
			base.dtModel.Columns.Add("AGENCY_PRINT",typeof(int));
			//OTHER_RES_DATE_CD
			base.dtModel.Columns.Add("OTHER_RES_DATE_CD",typeof(string));
			base.dtModel.Columns.Add("OTHER_RES_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("INTERNAL_CHANGE",typeof(string));
			base.dtModel.Columns.Add("APPLY_REINSTATE_FEE",typeof(int));
			base.dtModel.Columns.Add("SAME_AGENCY",typeof(int));
			base.dtModel.Columns.Add("ANOTHER_AGENCY",typeof(int));
			base.dtModel.Columns.Add("CFD_AMT",typeof(double));

			//Field added only at model..not there at table though
			base.dtModel.Columns.Add("UNDERWRITER",typeof(int));
			base.dtModel.Columns.Add("STATE_ID",typeof(int));
			base.dtModel.Columns.Add("LOB_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_TYPE",typeof(int));
			base.dtModel.Columns.Add("NEW_AGENCY",typeof(int));
			base.dtModel.Columns.Add("BILL_TYPE",typeof(string));	
			base.dtModel.Columns.Add("BASE_POLICY_VERSION_ID",typeof(int));		
			base.dtModel.Columns.Add("BASE_POLICY_DISP_VERSION",typeof(string));	
			base.dtModel.Columns.Add("STATE_CODE",typeof(string));	
			base.dtModel.Columns.Add("AGENCY_CODE",typeof(string));	
			base.dtModel.Columns.Add("EFFECTIVE_TIME",typeof(string));	
			//Will hold due date for the commit of process
			base.dtModel.Columns.Add("DUE_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("CANCELLATION_NOTICE_SENT",typeof(string));
			
			base.dtModel.Columns.Add("REVERT_BACK",typeof(string));
			base.dtModel.Columns.Add("LAST_REVERT_BACK",typeof(string));
			base.dtModel.Columns.Add("INCLUDE_REASON_DESC",typeof(string));
            base.dtModel.Columns.Add("COINSURANCE_NUMBER", typeof(string));
            base.dtModel.Columns.Add("ENDORSEMENT_TYPE", typeof(string));
            base.dtModel.Columns.Add("ENDORSEMENT_OPTION", typeof(int));
            base.dtModel.Columns.Add("SOURCE_VERSION_ID", typeof(int));
            base.dtModel.Columns.Add("CO_APPLICANT_ID", typeof(int));
            base.dtModel.Columns.Add("ENDORSEMENT_RE_ISSUE", typeof(int));
            
            
		}

		#region Database schema details
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
		// model for database field ROW_ID(int)
		public int ROW_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ROW_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ROW_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ROW_ID"] = value;
			}
		}
		// model for database field PROCESS_ID(int)
		public int PROCESS_ID
		{
			get
			{
				return base.dtModel.Rows[0]["PROCESS_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PROCESS_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PROCESS_ID"] = value;
			}
		}
		// model for database field PROCESS_TYPE(string)
		public string PROCESS_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["PROCESS_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PROCESS_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PROCESS_TYPE"] = value;
			}
		}
		// model for database field NEW_CUSTOMER_ID(int)
		public int NEW_CUSTOMER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["NEW_CUSTOMER_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["NEW_CUSTOMER_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NEW_CUSTOMER_ID"] = value;
			}
		}
		// model for database field NEW_POLICY_ID(int)
		public int NEW_POLICY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["NEW_POLICY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["NEW_POLICY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NEW_POLICY_ID"] = value;
			}
		}
		// model for database field NEW_POLICY_VERSION_ID(int)
		public int NEW_POLICY_VERSION_ID
		{
			get
			{
				return base.dtModel.Rows[0]["NEW_POLICY_VERSION_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["NEW_POLICY_VERSION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NEW_POLICY_VERSION_ID"] = value;
			}
		}
		// model for database field POLICY_PREVIOUS_STATUS(string)
		public string POLICY_PREVIOUS_STATUS
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_PREVIOUS_STATUS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["POLICY_PREVIOUS_STATUS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_PREVIOUS_STATUS"] = value;
			}
		}
		// model for database field POLICY_CURRENT_STATUS(string)
		public string POLICY_CURRENT_STATUS
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_CURRENT_STATUS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["POLICY_CURRENT_STATUS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_CURRENT_STATUS"] = value;
			}
		}
		// model for database field PROCESS_STATUS(string)
		public string PROCESS_STATUS
		{
			get
			{
				return base.dtModel.Rows[0]["PROCESS_STATUS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PROCESS_STATUS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PROCESS_STATUS"] = value;
			}
		}
		// model for database field CREATED_BY(int)
		public int CREATED_BY
		{
			get
			{
				return base.dtModel.Rows[0]["CREATED_BY"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CREATED_BY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CREATED_BY"] = value;
			}
		}
		// model for database field CREATED_DATETIME(DateTime)
		public DateTime CREATED_DATETIME
		{
			get
			{
				return base.dtModel.Rows[0]["CREATED_DATETIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["CREATED_DATETIME"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CREATED_DATETIME"] = value;
			}
		}
		// model for database field COMPLETED_BY(int)
		public int COMPLETED_BY
		{
			get
			{
				return base.dtModel.Rows[0]["COMPLETED_BY"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["COMPLETED_BY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COMPLETED_BY"] = value;
			}
		}
		// model for database field COMPLETED_DATETIME(DateTime)
		public DateTime COMPLETED_DATETIME
		{
			get
			{
				return base.dtModel.Rows[0]["COMPLETED_DATETIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["COMPLETED_DATETIME"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COMPLETED_DATETIME"] = value;
			}
		}
		// model for database field COMMENTS(string)
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
		// model for database field PRINT_COMMENTS(string)
		public string PRINT_COMMENTS
		{
			get
			{
				return base.dtModel.Rows[0]["PRINT_COMMENTS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PRINT_COMMENTS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PRINT_COMMENTS"] = value;
			}
		}
		// model for database field REQUESTED_BY(int)
		public int REQUESTED_BY
		{
			get
			{
				return base.dtModel.Rows[0]["REQUESTED_BY"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["REQUESTED_BY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REQUESTED_BY"] = value;
			}
		}
		// model for database field EFFECTIVE_DATETIME(DateTime)
		public DateTime EFFECTIVE_DATETIME
		{
			get
			{
				return base.dtModel.Rows[0]["EFFECTIVE_DATETIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["EFFECTIVE_DATETIME"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EFFECTIVE_DATETIME"] = value;
			}
		}
		// model for database field EXPIRY_DATE(DateTime)
		public DateTime EXPIRY_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["EXPIRY_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["EXPIRY_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EXPIRY_DATE"] = value;
			}
		}
		// model for database field CANCELLATION_OPTION(int)
		public int CANCELLATION_OPTION
		{
			get
			{
				return base.dtModel.Rows[0]["CANCELLATION_OPTION"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CANCELLATION_OPTION"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CANCELLATION_OPTION"] = value;
			}
		}
		// model for database field CANCELLATION_TYPE(int)
		public int CANCELLATION_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["CANCELLATION_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CANCELLATION_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CANCELLATION_TYPE"] = value;
			}
		}

		// model for database field RESCIND_OPTION(int)
		public int RESCIND_OPTION
		{
			get
			{
				return base.dtModel.Rows[0]["RESCIND_OPTION"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["RESCIND_OPTION"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RESCIND_OPTION"] = value;
			}
		}
		// model for database field RESCIND_TYPE(int)
		public int RESCIND_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["RESCIND_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["RESCIND_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RESCIND_TYPE"] = value;
			}
		}
		// model for database field REASON(int)
		public int REASON
		{
			get
			{
				return base.dtModel.Rows[0]["REASON"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["REASON"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REASON"] = value;
			}
		}
		// model for database field OTHER_REASON(string)
		public string OTHER_REASON
		{
			get
			{
				return base.dtModel.Rows[0]["OTHER_REASON"] == DBNull.Value ? "" : base.dtModel.Rows[0]["OTHER_REASON"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["OTHER_REASON"] = value;
			}
		}
		// model for database field RETURN_PREMIUM(double)
		public double RETURN_PREMIUM
		{
			get
			{
				return base.dtModel.Rows[0]["RETURN_PREMIUM"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["RETURN_PREMIUM"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RETURN_PREMIUM"] = value;
			}
		}
		// model for database field PAST_DUE_PREMIUM(double)
		public double PAST_DUE_PREMIUM
		{
			get
			{
				return base.dtModel.Rows[0]["PAST_DUE_PREMIUM"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["PAST_DUE_PREMIUM"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PAST_DUE_PREMIUM"] = value;
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
		// model for database field PROPERTY_INSPECTION_CREDIT(string)
		public string PROPERTY_INSPECTION_CREDIT
		{
			get
			{
				return base.dtModel.Rows[0]["PROPERTY_INSPECTION_CREDIT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PROPERTY_INSPECTION_CREDIT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PROPERTY_INSPECTION_CREDIT"] = value;
			}
		}
		// model for database field POLICY_TERMS(int)
		public int POLICY_TERMS
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_TERMS"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["POLICY_TERMS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_TERMS"] = value;
			}
		}
		// model for database field NEW_POLICY_TERM_EFFECTIVE_DATE		(DateTime)
		public DateTime NEW_POLICY_TERM_EFFECTIVE_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["NEW_POLICY_TERM_EFFECTIVE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["NEW_POLICY_TERM_EFFECTIVE_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NEW_POLICY_TERM_EFFECTIVE_DATE"] = value;
			}
		}
		// model for database field CREATED_DATETIME(DateTime)
		public DateTime NEW_POLICY_TERM_EXPIRATION_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["NEW_POLICY_TERM_EXPIRATION_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["NEW_POLICY_TERM_EXPIRATION_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NEW_POLICY_TERM_EXPIRATION_DATE"] = value;
			}
		}
		// model for database field POLICY_TERMS(int)
		public int DIARY_LIST_ID
		{
			get
			{
				return base.dtModel.Rows[0]["DIARY_LIST_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["DIARY_LIST_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DIARY_LIST_ID"] = value;
			}
		}
		// model for database field PRINTING_OPTIONS(int)
		public int PRINTING_OPTIONS
		{
			get
			{
				return base.dtModel.Rows[0]["PRINTING_OPTIONS"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PRINTING_OPTIONS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PRINTING_OPTIONS"] = value;
			}
		}
		// model for database field INSURED(int)
		public int INSURED
		{
			get
			{
				return base.dtModel.Rows[0]["INSURED"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["INSURED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INSURED"] = value;
			}
		}
		// model for database field SEND_INSURED_COPY_TO(int)
		public int SEND_INSURED_COPY_TO
		{
			get
			{
				return base.dtModel.Rows[0]["SEND_INSURED_COPY_TO"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["SEND_INSURED_COPY_TO"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SEND_INSURED_COPY_TO"] = value;
			}
		}
		// model for database field AUTO_ID_CARD(int)
		public int AUTO_ID_CARD
		{
			get
			{
				return base.dtModel.Rows[0]["AUTO_ID_CARD"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["AUTO_ID_CARD"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AUTO_ID_CARD"] = value;
			}
		}
		// model for database field NO_COPIES(int)
		public int NO_COPIES
		{
			get
			{
				return base.dtModel.Rows[0]["NO_COPIES"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["NO_COPIES"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NO_COPIES"] = value;
			}
		}
		// model for database field STD_LETTER_REQD(int)
		public int STD_LETTER_REQD
		{
			get
			{
				return base.dtModel.Rows[0]["STD_LETTER_REQD"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["STD_LETTER_REQD"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["STD_LETTER_REQD"] = value;
			}
		}
		// model for database field CUSTOM_LETTER_REQD(int)
		public int CUSTOM_LETTER_REQD
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOM_LETTER_REQD"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CUSTOM_LETTER_REQD"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOM_LETTER_REQD"] = value;
			}
		}
		// model for database field ADVERSE_LETTER_REQD(int)
		public int ADVERSE_LETTER_REQD
		{
			get
			{
				return base.dtModel.Rows[0]["ADVERSE_LETTER_REQD"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ADVERSE_LETTER_REQD"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ADVERSE_LETTER_REQD"] = value;
			}
		}
		// model for database field ADD_INT(int)
		public int ADD_INT
		{
			get
			{
				return base.dtModel.Rows[0]["ADD_INT"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ADD_INT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ADD_INT"] = value;
			}
		}
		// model for database field SEND_ALL(int)
		public int SEND_ALL
		{
			get
			{
				return base.dtModel.Rows[0]["SEND_ALL"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["SEND_ALL"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SEND_ALL"] = value;
			}
		}
		// model for database field ADD_INT_ID(string)
		public string ADD_INT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ADD_INT_ID"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ADD_INT_ID"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ADD_INT_ID"] = value;
			}
		}		
		// model for database field AGENCY_PRINT(int)
		public int AGENCY_PRINT
		{
			get
			{
				return base.dtModel.Rows[0]["AGENCY_PRINT"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["AGENCY_PRINT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AGENCY_PRINT"] = value;
			}
		}

		// model for database field OTHER_RES_DATE_CD(string)
		public string OTHER_RES_DATE_CD
		{
			get
			{
				return base.dtModel.Rows[0]["OTHER_RES_DATE_CD"] == DBNull.Value ? "" : base.dtModel.Rows[0]["OTHER_RES_DATE_CD"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["OTHER_RES_DATE_CD"] = value;
			}
		}

		// model for database field OTHER_RES_DATE(DateTime)
		public DateTime OTHER_RES_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["OTHER_RES_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["OTHER_RES_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["OTHER_RES_DATE"] = value;
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
		// model for database field LOB_ID(int)
		public int LOB_ID
		{
			get
			{
				return base.dtModel.Rows[0]["LOB_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LOB_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LOB_ID"] = value;
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
		
		// model for database field POLICY_TYPE(int)
		public int POLICY_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["POLICY_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_TYPE"] = value;
			}
		}

		// model for database field APPLY_REINSTATE_FEE(int)
		public int APPLY_REINSTATE_FEE
		{
			get
			{
				return base.dtModel.Rows[0]["APPLY_REINSTATE_FEE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["APPLY_REINSTATE_FEE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APPLY_REINSTATE_FEE"] = value;
			}
		}

		// model for database field INTERNAL_CHANGE(string) -- only for Endorsement Process
		public string INTERNAL_CHANGE
		{
			get
			{
				return base.dtModel.Rows[0]["INTERNAL_CHANGE"] == DBNull.Value ? "0" : base.dtModel.Rows[0]["INTERNAL_CHANGE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["INTERNAL_CHANGE"] = value;
			}
		}
		
		// model for database field ANOTHER_AGENCY(int)
			public int ANOTHER_AGENCY
			{
				get
				{
					return base.dtModel.Rows[0]["ANOTHER_AGENCY"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ANOTHER_AGENCY"].ToString());
				}
				set
				{
					base.dtModel.Rows[0]["ANOTHER_AGENCY"] = value;
				}
			}
		// model for database field SAME_AGENCY(int)
		public int SAME_AGENCY
		{
			get
			{
				return base.dtModel.Rows[0]["SAME_AGENCY"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["SAME_AGENCY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SAME_AGENCY"] = value;
			}
		}
		// model for database field SAME_AGENCY(int)
		public int NEW_AGENCY
		{
			get
			{
				return base.dtModel.Rows[0]["NEW_AGENCY"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["NEW_AGENCY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NEW_AGENCY"] = value;
			}
		}
		// model for database field CFD_AMT(double)
		public double CFD_AMT
		{
			get
			{
				return base.dtModel.Rows[0]["CFD_AMT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["CFD_AMT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CFD_AMT"] = value;
			}
		}
		// model for database field BILL_TYPE(string)
		public string BILL_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["BILL_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["BILL_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BILL_TYPE"] = value;
			}
		}
		// model for database field BASE_POLICY_VERSION_ID(int)
		public int BASE_POLICY_VERSION_ID
		{
			get
			{
				return base.dtModel.Rows[0]["BASE_POLICY_VERSION_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["BASE_POLICY_VERSION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["BASE_POLICY_VERSION_ID"] = value;
			}
		}
		// model for database field BASE_POLICY_DISP_VERSION(string)
		public string BASE_POLICY_DISP_VERSION
		{
			get
			{
				return base.dtModel.Rows[0]["BASE_POLICY_DISP_VERSION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["BASE_POLICY_DISP_VERSION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BASE_POLICY_DISP_VERSION"] = value;
			}
		}
		// model for database field STATE_CODE(string)
		public string STATE_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["STATE_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["STATE_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["STATE_CODE"] = value;
			}
		}
		// model for database field AGENCY_CODE(string)
		public string AGENCY_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["AGENCY_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["AGENCY_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["AGENCY_CODE"] = value;
			}
		}

		/// <summary>
		/// Due Date for the commit of process
		/// </summary>
		public DateTime DUE_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["DUE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["DUE_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DUE_DATE"] = value;
			}
		}
		// model for database field CANCELLATION_NOTICE_SENT(string)
		public string CANCELLATION_NOTICE_SENT
		{
			get
			{
				return base.dtModel.Rows[0]["CANCELLATION_NOTICE_SENT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CANCELLATION_NOTICE_SENT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CANCELLATION_NOTICE_SENT"] = value;
			}
		}
		// model for database field CANCELLATION_NOTICE_SENT(string)
		public string REVERT_BACK
		{
			get
			{
				return base.dtModel.Rows[0]["REVERT_BACK"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REVERT_BACK"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REVERT_BACK"] = value;
			}
		}
		// model for database field CANCELLATION_NOTICE_SENT(string)
		public string LAST_REVERT_BACK
		{
			get
			{
				return base.dtModel.Rows[0]["LAST_REVERT_BACK"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LAST_REVERT_BACK"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LAST_REVERT_BACK"] = value;
			}
		}
		/// effective for the  process
		/// </summary>
		public string EFFECTIVE_TIME
		{
			get
			{
				return base.dtModel.Rows[0]["EFFECTIVE_TIME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["EFFECTIVE_TIME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EFFECTIVE_TIME"] = value;
			}
		}
		// INCLUDE_REASON_DESC for the  process
		public string INCLUDE_REASON_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["INCLUDE_REASON_DESC"] == DBNull.Value ? "" : base.dtModel.Rows[0]["INCLUDE_REASON_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["INCLUDE_REASON_DESC"] = value;
			}
		}
        public string COINSURANCE_NUMBER
		{
			get
			{
                return base.dtModel.Rows[0]["COINSURANCE_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["COINSURANCE_NUMBER"].ToString();
			}
			set
			{
                base.dtModel.Rows[0]["COINSURANCE_NUMBER"] = value;
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

        public int ENDORSEMENT_OPTION
        {
            get
            {
                return base.dtModel.Rows[0]["ENDORSEMENT_OPTION"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ENDORSEMENT_OPTION"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["ENDORSEMENT_OPTION"] = value;
            }
        }

        public int SOURCE_VERSION_ID
        {
            get
            {
                return base.dtModel.Rows[0]["SOURCE_VERSION_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["SOURCE_VERSION_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["SOURCE_VERSION_ID"] = value;
            }
        }

        public int CO_APPLICANT_ID
        {
            get
            {
                return base.dtModel.Rows[0]["CO_APPLICANT_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CO_APPLICANT_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["CO_APPLICANT_ID"] = value;
            }
        }

        public int ENDORSEMENT_RE_ISSUE
        {
            get
            {
                return base.dtModel.Rows[0]["ENDORSEMENT_RE_ISSUE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ENDORSEMENT_RE_ISSUE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["ENDORSEMENT_RE_ISSUE"] = value;
            }
        }
        
        
		#endregion
	}
}
