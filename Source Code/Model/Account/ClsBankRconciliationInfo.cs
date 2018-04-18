/******************************************************************************************
<Author					: -   Ajit Singh Chahal 
<Start Date				: -	  7/8/2005 12:15:24 PM
<End Date				: -	
<Description			: -   Model for bank reconciliation.
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
	/// Database Model for ACT_BANK_RECONCILIATION.
	/// </summary>
	public class ClsBankRconciliationInfo: Cms.Model.ClsCommonModel
	{
		private const string ACT_BANK_RECONCILIATION = "ACT_BANK_RECONCILIATION";
		public ClsBankRconciliationInfo()
		{
			base.dtModel.TableName = "ACT_BANK_RECONCILIATION";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table ACT_BANK_RECONCILIATION
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("AC_RECONCILIATION_ID",typeof(int));
			base.dtModel.Columns.Add("ACCOUNT_ID",typeof(int));
			base.dtModel.Columns.Add("DIV_ID",typeof(int));
			base.dtModel.Columns.Add("DEPT_ID",typeof(int));
			base.dtModel.Columns.Add("PC_ID",typeof(int));
			base.dtModel.Columns.Add("START_STATEMENT_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("STATEMENT_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("STARTING_BALANCE",typeof(double));
			base.dtModel.Columns.Add("ENDING_BALANCE",typeof(double));
			base.dtModel.Columns.Add("BANK_CHARGES_CREDITS",typeof(double));
			base.dtModel.Columns.Add("LAST_RECONCILED",typeof(DateTime));
			base.dtModel.Columns.Add("IS_COMMITED",typeof(string));
			base.dtModel.Columns.Add("DATE_COMMITED",typeof(DateTime));
			base.dtModel.Columns.Add("COMMITTED_BY",typeof(int));
			// Columns added for ACT_BANK_RECON_CHECK_FILE table

			base.dtModel.Columns.Add("ACCOUNT_NUMBER",typeof(string));
			base.dtModel.Columns.Add("SERIAL_NUMBER",typeof(string));
			base.dtModel.Columns.Add("CHECK_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("AMOUNT",typeof(double));
			base.dtModel.Columns.Add("ADDITIONAL_DATA",typeof(string));
			base.dtModel.Columns.Add("SEQUENCE_NUMBER",typeof(int));
			base.dtModel.Columns.Add("REF_FILE_ID",typeof(int));
			base.dtModel.Columns.Add("RECON_GROUP_ID",typeof(int));
			base.dtModel.Columns.Add("IMPORT_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("MATCHED_RECORD_STATUS",typeof(int));
			base.dtModel.Columns.Add("ERROR_DESC",typeof(string));
			base.dtModel.Columns.Add("CHECK_NUMBER",typeof(string));

		}
		#region Database schema details
		// model for database field AC_RECONCILIATION_ID(int)
		public int AC_RECONCILIATION_ID
		{
			get
			{
				return base.dtModel.Rows[0]["AC_RECONCILIATION_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["AC_RECONCILIATION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AC_RECONCILIATION_ID"] = value;
			}
		}
		// model for database field ACCOUNT_ID(int)
		public int ACCOUNT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ACCOUNT_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ACCOUNT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ACCOUNT_ID"] = value;
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
		// model for database field START_STATEMENT_DATE(DateTime)
		public DateTime START_STATEMENT_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["START_STATEMENT_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["START_STATEMENT_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["START_STATEMENT_DATE"] = value;
			}
		}
		// model for database field STATEMENT_DATE(DateTime)
		public DateTime STATEMENT_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["STATEMENT_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["STATEMENT_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["STATEMENT_DATE"] = value;
			}
		}
		// model for database field STARTING_BALANCE(double)
		public double STARTING_BALANCE
		{
			get
			{
				return base.dtModel.Rows[0]["STARTING_BALANCE"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["STARTING_BALANCE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["STARTING_BALANCE"] = value;
			}
		}
		// model for database field ENDING_BALANCE(double)
		public double ENDING_BALANCE
		{
			get
			{
				return base.dtModel.Rows[0]["ENDING_BALANCE"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["ENDING_BALANCE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ENDING_BALANCE"] = value;
			}
		}
		// model for database field BANK_CHARGES_CREDITS(double)
		public double BANK_CHARGES_CREDITS
		{
			get
			{
				return base.dtModel.Rows[0]["BANK_CHARGES_CREDITS"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["BANK_CHARGES_CREDITS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["BANK_CHARGES_CREDITS"] = value;
			}
		}
		// model for database field LAST_RECONCILED(DateTime)
		public DateTime LAST_RECONCILED
		{
			get
			{
				return base.dtModel.Rows[0]["LAST_RECONCILED"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["LAST_RECONCILED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LAST_RECONCILED"] = value;
			}
		}
		// model for database field IS_COMMITED(string)
		public string IS_COMMITED
		{
			get
			{
				return base.dtModel.Rows[0]["IS_COMMITED"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IS_COMMITED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_COMMITED"] = value;
			}
		}
		// model for database field DATE_COMMITED(DateTime)
		public DateTime DATE_COMMITED
		{
			get
			{
				return base.dtModel.Rows[0]["DATE_COMMITED"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["DATE_COMMITED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DATE_COMMITED"] = value;
			}
		}
		// model for database field COMMITTED_BY(int)
		public int COMMITTED_BY
		{
			get
			{
				return base.dtModel.Rows[0]["COMMITTED_BY"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["COMMITTED_BY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COMMITTED_BY"] = value;
			}
		}
	
		/// <summary>
		/// FIELDS FOR ACT_BANK_RECON_CHECK_FILE TABLE
		/// </summary>

		// model for database field ACCOUNT_NUMBER(string)
		public string ACCOUNT_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["ACCOUNT_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ACCOUNT_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ACCOUNT_NUMBER"] = value;
			}
		}
		
		// model for database field SERIAL_NUMBER(string)
		public string SERIAL_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["SERIAL_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SERIAL_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SERIAL_NUMBER"] = value;
			}
		}
		// model for database field CHECK_DATE(DateTime)
		public DateTime CHECK_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["CHECK_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["CHECK_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CHECK_DATE"] = value;
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
		// model for database field ADDITIONAL_DATA(string)
		public string ADDITIONAL_DATA
		{
			get
			{
				return base.dtModel.Rows[0]["ADDITIONAL_DATA"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ADDITIONAL_DATA"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ADDITIONAL_DATA"] = value;
			}
		}
		// model for database field SEQUENCE_NUMBER(int)
		public int SEQUENCE_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["SEQUENCE_NUMBER"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["SEQUENCE_NUMBER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SEQUENCE_NUMBER"] = value;
			}
		}
		
		// model for database field REF_FILE_ID(int)
		public int REF_FILE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["REF_FILE_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["REF_FILE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REF_FILE_ID"] = value;
			}
		}

		// model for database field RECON_GROUP_ID(int)
		public int RECON_GROUP_ID
		{
			get
			{
				return base.dtModel.Rows[0]["RECON_GROUP_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["RECON_GROUP_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RECON_GROUP_ID"] = value;
			}
		}
		
		// model for database field IMPORT_DATE(DateTime)
		public DateTime IMPORT_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["IMPORT_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["IMPORT_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["IMPORT_DATE"] = value;
			}
		}
		
		// model for database field MATCHED_RECORD_STATUS(int)
		public int MATCHED_RECORD_STATUS
		{
			get
			{
				return base.dtModel.Rows[0]["MATCHED_RECORD_STATUS"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["MATCHED_RECORD_STATUS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MATCHED_RECORD_STATUS"] = value;
			}
		}
		// model for database field ERROR_DESC(string)
		public string ERROR_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["ERROR_DESC"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ERROR_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ERROR_DESC"] = value;
			}
		}
		// model for database field CHECK_NUMBER(string)
		public string CHECK_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["CHECK_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CHECK_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CHECK_NUMBER"] = value;
			}
		}
		#endregion
	}
}
