/******************************************************************************************
<Author				: -   Priya
<Start Date				: -	5/12/2005 10:31:53 AM
<End Date				: -	
<Description				: - 	
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
namespace Cms.Model.Application
{
	/// <summary>
	/// Database Model for APP_HOME_APPLICANT_DETAILS.
	/// </summary>
	public class ClsApplicantsInfo : Cms.Model.ClsBaseModel
	{
		private const string APP_HOME_APPLICANT_DETAILS = "APP_HOME_APPLICANT_DETAILS";
		public ClsApplicantsInfo()
		{
			base.dtModel.TableName = "APP_HOME_APPLICANT_DETAILS";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table APP_HOME_APPLICANT_DETAILS
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("APP_ID",typeof(int));
			base.dtModel.Columns.Add("APP_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("DATE_AT_CURR_RESI",typeof(DateTime));
			base.dtModel.Columns.Add("YEARS_AT_PREV_ADD",typeof(double));
			base.dtModel.Columns.Add("PREV_ADD1",typeof(string));
			//base.dtModel.Columns.Add("PREV_ADD2",typeof(string));
			base.dtModel.Columns.Add("PREV_CITY",typeof(string));
			base.dtModel.Columns.Add("PREV_STATE",typeof(string));
			base.dtModel.Columns.Add("PREV_ZIP",typeof(string));
			base.dtModel.Columns.Add("APPLICANT_OCCU",typeof(string));
			base.dtModel.Columns.Add("EMPLOYER_NAME",typeof(string));
			base.dtModel.Columns.Add("EMPLOYER_ADDRESS",typeof(string));
			base.dtModel.Columns.Add("YEARS_WITH_CURR_EMPL",typeof(double));
			base.dtModel.Columns.Add("IS_COAPPLICANT",typeof(string));
			base.dtModel.Columns.Add("CO_APPLI_OCCU",typeof(string));
			base.dtModel.Columns.Add("CO_APPLI_EMPL_NAME",typeof(string));
			base.dtModel.Columns.Add("CO_APPLI_EMPL_ADDRESS",typeof(string));
			base.dtModel.Columns.Add("CO_APPLI_YEARS_WITH_CURR_EMPL",typeof(double));
			base.dtModel.Columns.Add("MARITAL_STATUS",typeof(string));
			base.dtModel.Columns.Add("SSN_NO",typeof(string));
			base.dtModel.Columns.Add("WRITTEN_PREMIUM",typeof(double));
			base.dtModel.Columns.Add("DEPOSIT_PREMIUM",typeof(double));
			base.dtModel.Columns.Add("DATE_OF_BIRTH",typeof(DateTime));
			base.dtModel.Columns.Add("LAST_INSPECTED_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("CO_APPL_YEAR_CURR_OCCU",typeof(double));
			//base.dtModel.Columns.Add("CO_APPL_YEAR_CURR_EMPL",typeof(double));
			base.dtModel.Columns.Add("CO_APPL_MARITAL_STATUS",typeof(string));
			base.dtModel.Columns.Add("CO_APPL_DOB",typeof(DateTime));
			base.dtModel.Columns.Add("CO_APPL_SSN_NO",typeof(string));
			base.dtModel.Columns.Add("FULL_TERM_PREMIUM",typeof(double));
		}
		#region Database schema details
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
		// model for database field DATE_AT_CURR_RESI(DateTime)
		public DateTime DATE_AT_CURR_RESI
		{
			get
			{
				return base.dtModel.Rows[0]["DATE_AT_CURR_RESI"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["DATE_AT_CURR_RESI"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DATE_AT_CURR_RESI"] = value;
			}
		}
		// model for database field YEARS_AT_PREV_ADD(double)
		public double YEARS_AT_PREV_ADD
		{
			get
			{
				return base.dtModel.Rows[0]["YEARS_AT_PREV_ADD"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["YEARS_AT_PREV_ADD"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["YEARS_AT_PREV_ADD"] = value;
			}
		}
		// model for database field PREV_ADD1(string)
		public string PREV_ADD1
		{
			get
			{
				return base.dtModel.Rows[0]["PREV_ADD1"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PREV_ADD1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PREV_ADD1"] = value;
			}
		}
		// model for database field PREV_ADD2(string)
		public string PREV_ADD2
		{
			get
			{
				return base.dtModel.Rows[0]["PREV_ADD2"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PREV_ADD2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PREV_ADD2"] = value;
			}
		}
		// model for database field PREV_CITY(string)
		public string PREV_CITY
		{
			get
			{
				return base.dtModel.Rows[0]["PREV_CITY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PREV_CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PREV_CITY"] = value;
			}
		}
		// model for database field PREV_STATE(string)
		public string PREV_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["PREV_STATE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PREV_STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PREV_STATE"] = value;
			}
		}
		// model for database field PREV_ZIP(string)
		public string PREV_ZIP
		{
			get
			{
				return base.dtModel.Rows[0]["PREV_ZIP"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PREV_ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PREV_ZIP"] = value;
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
		// model for database field IS_COAPPLICANT(string)
		public string IS_COAPPLICANT
		{
			get
			{
				return base.dtModel.Rows[0]["IS_COAPPLICANT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_COAPPLICANT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_COAPPLICANT"] = value;
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
		// model for database field WRITTEN_PREMIUM(double)
		public double WRITTEN_PREMIUM
		{
			get
			{
				return base.dtModel.Rows[0]["WRITTEN_PREMIUM"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["WRITTEN_PREMIUM"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["WRITTEN_PREMIUM"] = value;
			}
		}
		// model for database field DEPOSIT_PREMIUM(double)
		public double DEPOSIT_PREMIUM
		{
			get
			{
				return base.dtModel.Rows[0]["DEPOSIT_PREMIUM"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["DEPOSIT_PREMIUM"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DEPOSIT_PREMIUM"] = value;
			}
		}
		// model for database field DATE_OF_BIRTH(DateTime)
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
		// model for database field LAST_INSPECTED_DATE(DateTime)
		public DateTime LAST_INSPECTED_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["LAST_INSPECTED_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["LAST_INSPECTED_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LAST_INSPECTED_DATE"] = value;
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
		// model for database field CO_APPL_YEAR_CURR_EMPL(double)
		public double CO_APPL_YEAR_CURR_EMPL
		{
			get
			{
				return base.dtModel.Rows[0]["CO_APPL_YEAR_CURR_EMPL"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["CO_APPL_YEAR_CURR_EMPL"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CO_APPL_YEAR_CURR_EMPL"] = value;
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
		// model for database field FULL_TERM_PREMIUM(double)
		public double FULL_TERM_PREMIUM
		{
			get
			{
				return base.dtModel.Rows[0]["FULL_TERM_PREMIUM"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["FULL_TERM_PREMIUM"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["FULL_TERM_PREMIUM"] = value;
			}
		}
		#endregion
	}
}
