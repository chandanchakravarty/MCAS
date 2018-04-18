	/******************************************************************************************
	<Author					: -   Ajit Singh Chahal
	<Start Date				: -	  5/16/2005 2:59:42 PM
	<End Date				: -	
	<Description			: -   Model for General Ledger.
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

namespace Cms.Model.Maintenance.Accounting	
{
		/// <summary>
		/// Database Model for ACT_GENERAL_LEDGER.
		/// </summary>
	public class ClsGeneralLedgerInfo : Cms.Model.ClsCommonModel
		{
			private const string ACT_GENERAL_LEDGER = "ACT_GENERAL_LEDGER";
		public ClsGeneralLedgerInfo()
		{
			base.dtModel.TableName = "ACT_GENERAL_LEDGER";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table ACT_GENERAL_LEDGER
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("GL_ID",typeof(int));
			base.dtModel.Columns.Add("FISCAL_ID",typeof(int));
			base.dtModel.Columns.Add("LEDGER_NAME",typeof(string));
			base.dtModel.Columns.Add("FISCAL_BEGIN_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("FISCAL_END_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("MONTH_BEGINING",typeof(int));
			base.dtModel.Columns.Add("FORBID_POSTING",typeof(string));
			base.dtModel.Columns.Add("SMALL_BALANCE",typeof(double));
			base.dtModel.Columns.Add("IS_SYSTEM_LOCK",typeof(string));
			base.dtModel.Columns.Add("IS_SYS_OUT_OF_BAL",typeof(string));
			base.dtModel.Columns.Add("LAST_EOD_RUN_DATE_TIME",typeof(DateTime));
			base.dtModel.Columns.Add("LAST_EOM_RUN_DATE_TIME",typeof(DateTime));
			base.dtModel.Columns.Add("LAST_EOY_RUN_DATE_TIME",typeof(DateTime));
			base.dtModel.Columns.Add("ACC_SORT_ORDER",typeof(int));
			base.dtModel.Columns.Add("AST_UNCOLL_PRM_CUSTOMER",typeof(int));
			base.dtModel.Columns.Add("AST_UNCOLL_PRM_AGENCY",typeof(int));
			base.dtModel.Columns.Add("AST_UNCOLL_PRM_SUSPENSE",typeof(int));
			base.dtModel.Columns.Add("AST_PRM_SUSPENSE",typeof(int));
			base.dtModel.Columns.Add("AST_MCCA_FEE_SUSPENSE",typeof(int));
			base.dtModel.Columns.Add("AST_OTHER_STATE_ASSMT_FEE_SUSPENSE",typeof(int));
			base.dtModel.Columns.Add("AST_COMM_RECV_REINS_EXCESS_CONTRACT",typeof(int));
			base.dtModel.Columns.Add("AST_COMM_RECV_REINS_UMBRELLA_CONTRACT",typeof(int));
			base.dtModel.Columns.Add("LIB_COMM_PAYB_AGENCY_BILL",typeof(int));
			base.dtModel.Columns.Add("LIB_COMM_PAYB_DIRECT_BILL",typeof(int));
			base.dtModel.Columns.Add("LIB_REINS_PAYB_EXCESS_CONTRACT",typeof(int));
			base.dtModel.Columns.Add("LIB_REINS_PAYB_CAT_CONTRACT",typeof(int));
			base.dtModel.Columns.Add("LIB_REINS_PAYB_MCCA",typeof(int));
			base.dtModel.Columns.Add("LIB_REINS_PAYB_UMBRELLA",typeof(int));
			base.dtModel.Columns.Add("LIB_REINS_PAYB_FACULTATIVE",typeof(int));
			base.dtModel.Columns.Add("LIB_OUT_DRAFTS",typeof(int));
			base.dtModel.Columns.Add("LIB_ADVCE_PRM_DEPOSIT",typeof(int));
			base.dtModel.Columns.Add("LIB_ADVCE_PRM_DEPOSIT_2M",typeof(int));
			base.dtModel.Columns.Add("LIB_UNEARN_PRM",typeof(int));
			base.dtModel.Columns.Add("LIB_UNEARN_PRM_MCCA",typeof(int));
			base.dtModel.Columns.Add("LIB_UNEARN_PRM_CEDED_UNEARN_MCCA_REINS",typeof(int));
			base.dtModel.Columns.Add("LIB_UNEARN_PRM_CEDED_UNEARN_UMBRELLA_REINS",typeof(int));
			base.dtModel.Columns.Add("LIB_UNEARN_PRM_OTH_STATE_ASSES_FEE",typeof(int));
			base.dtModel.Columns.Add("LIB_TAX_PAYB",typeof(int));
			base.dtModel.Columns.Add("LIB_VENDOR_PAYB",typeof(int));
			base.dtModel.Columns.Add("LIB_COLL_ON_NONISSUED_POLICY",typeof(int));
			base.dtModel.Columns.Add("EQU_TRANSFER",typeof(int));
			base.dtModel.Columns.Add("INC_PRM_WRTN",typeof(int));
			base.dtModel.Columns.Add("INC_PRM_WRTN_MCCA",typeof(int));
			base.dtModel.Columns.Add("INC_PRM_WRTN_OTH_STATE_ASSESS_FEE",typeof(int));
			base.dtModel.Columns.Add("INC_REINS_CEDED_EXCESS_CON",typeof(int));
			base.dtModel.Columns.Add("INC_REINS_CEDED_CAT_CON",typeof(int));
			base.dtModel.Columns.Add("INC_REINS_CEDED_UMBRELLA_CON",typeof(int));
			base.dtModel.Columns.Add("INC_REINS_CEDED_FACUL_CON",typeof(int));
			base.dtModel.Columns.Add("INC_REINS_CEDED_MCCA_CON",typeof(int));
			base.dtModel.Columns.Add("INC_CHG_UNEARN_PRM",typeof(int));
			base.dtModel.Columns.Add("INC_CHG_UNEARN_PRM_MCCA",typeof(int));
			base.dtModel.Columns.Add("INC_CHG_UNEARN_PRM_OTH_STATE_FEE",typeof(int));
			base.dtModel.Columns.Add("INC_CHG_CEDED_UNEARN_MCCA",typeof(int));
			base.dtModel.Columns.Add("INC_CHG_CEDED_UNEARN_UMBRELLA_REINS",typeof(int));
			base.dtModel.Columns.Add("EXP_COMM_INCURRED",typeof(int));
			base.dtModel.Columns.Add("EXP_REINS_COMM_EXCESS_CON",typeof(int));
			base.dtModel.Columns.Add("EXP_REINS_COMM_UMBRELLA_CON",typeof(int));
			base.dtModel.Columns.Add("EXP_ASSIGNED_CLAIMS",typeof(int));
			base.dtModel.Columns.Add("EXP_REINS_PAID_LOSSES",typeof(int));
			base.dtModel.Columns.Add("EXP_REINS_PAID_LOSSES_CAT",typeof(int));
		}
		#region Database schema details
		// model for database field GL_ID(int)
		public int GL_ID
		{
			get
			{
				return base.dtModel.Rows[0]["GL_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["GL_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["GL_ID"] = value;
			}
		}
		// model for database field FISCAL_ID(int)
		public int FISCAL_ID
		{
			get
			{
				return base.dtModel.Rows[0]["FISCAL_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["FISCAL_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["FISCAL_ID"] = value;
			}
		}
		// model for database field LEDGER_NAME(string)
		public string LEDGER_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["LEDGER_NAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LEDGER_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LEDGER_NAME"] = value;
			}
		}
		// model for database field FISCAL_BEGIN_DATE(DateTime)
		public DateTime FISCAL_BEGIN_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["FISCAL_BEGIN_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["FISCAL_BEGIN_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["FISCAL_BEGIN_DATE"] = value;
			}
		}
		// model for database field FISCAL_END_DATE(DateTime)
		public DateTime FISCAL_END_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["FISCAL_END_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["FISCAL_END_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["FISCAL_END_DATE"] = value;
			}
		}
		// model for database field MONTH_BEGINING(int)
		public int MONTH_BEGINING
		{
			get
			{
				return base.dtModel.Rows[0]["MONTH_BEGINING"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["MONTH_BEGINING"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MONTH_BEGINING"] = value;
			}
		}
		// model for database field FORBID_POSTING(string)
		public string FORBID_POSTING
		{
			get
			{
				return base.dtModel.Rows[0]["FORBID_POSTING"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["FORBID_POSTING"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["FORBID_POSTING"] = value;
			}
		}
		// model for database field SMALL_BALANCE(double)
		public double SMALL_BALANCE
		{
			get
			{
				return base.dtModel.Rows[0]["SMALL_BALANCE"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["SMALL_BALANCE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SMALL_BALANCE"] = value;
			}
		}
		// model for database field IS_SYSTEM_LOCK(string)
		public string IS_SYSTEM_LOCK
		{
			get
			{
				return base.dtModel.Rows[0]["IS_SYSTEM_LOCK"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_SYSTEM_LOCK"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_SYSTEM_LOCK"] = value;
			}
		}
		// model for database field IS_SYS_OUT_OF_BAL(string)
		public string IS_SYS_OUT_OF_BAL
		{
			get
			{
				return base.dtModel.Rows[0]["IS_SYS_OUT_OF_BAL"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_SYS_OUT_OF_BAL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_SYS_OUT_OF_BAL"] = value;
			}
		}
		// model for database field LAST_EOD_RUN_DATE_TIME(DateTime)
		public DateTime LAST_EOD_RUN_DATE_TIME
		{
			get
			{
				return base.dtModel.Rows[0]["LAST_EOD_RUN_DATE_TIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["LAST_EOD_RUN_DATE_TIME"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LAST_EOD_RUN_DATE_TIME"] = value;
			}
		}
		// model for database field LAST_EOM_RUN_DATE_TIME(DateTime)
		public DateTime LAST_EOM_RUN_DATE_TIME
		{
			get
			{
				return base.dtModel.Rows[0]["LAST_EOM_RUN_DATE_TIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["LAST_EOM_RUN_DATE_TIME"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LAST_EOM_RUN_DATE_TIME"] = value;
			}
		}
		// model for database field LAST_EOY_RUN_DATE_TIME(DateTime)
		public DateTime LAST_EOY_RUN_DATE_TIME
		{
			get
			{
				return base.dtModel.Rows[0]["LAST_EOY_RUN_DATE_TIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["LAST_EOY_RUN_DATE_TIME"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LAST_EOY_RUN_DATE_TIME"] = value;
			}
		}
		// model for database field ACC_SORT_ORDER(int)
		public int ACC_SORT_ORDER
		{
			get
			{
				return base.dtModel.Rows[0]["ACC_SORT_ORDER"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["ACC_SORT_ORDER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ACC_SORT_ORDER"] = value;
			}
		}
		// model for database field AST_UNCOLL_PRM_CUSTOMER(int)
		public int AST_UNCOLL_PRM_CUSTOMER
		{
			get
			{
				return base.dtModel.Rows[0]["AST_UNCOLL_PRM_CUSTOMER"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["AST_UNCOLL_PRM_CUSTOMER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AST_UNCOLL_PRM_CUSTOMER"] = value;
			}
		}
		// model for database field AST_UNCOLL_PRM_AGENCY(int)
		public int AST_UNCOLL_PRM_AGENCY
		{
			get
			{
				return base.dtModel.Rows[0]["AST_UNCOLL_PRM_AGENCY"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["AST_UNCOLL_PRM_AGENCY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AST_UNCOLL_PRM_AGENCY"] = value;
			}
		}
		// model for database field AST_UNCOLL_PRM_SUSPENSE(int)
		public int AST_UNCOLL_PRM_SUSPENSE
		{
			get
			{
				return base.dtModel.Rows[0]["AST_UNCOLL_PRM_SUSPENSE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["AST_UNCOLL_PRM_SUSPENSE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AST_UNCOLL_PRM_SUSPENSE"] = value;
			}
		}
		// model for database field AST_PRM_SUSPENSE(int)
		public int AST_PRM_SUSPENSE
		{
			get
			{
				return base.dtModel.Rows[0]["AST_PRM_SUSPENSE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["AST_PRM_SUSPENSE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AST_PRM_SUSPENSE"] = value;
			}
		}
		// model for database field AST_MCCA_FEE_SUSPENSE(int)
		public int AST_MCCA_FEE_SUSPENSE
		{
			get
			{
				return base.dtModel.Rows[0]["AST_MCCA_FEE_SUSPENSE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["AST_MCCA_FEE_SUSPENSE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AST_MCCA_FEE_SUSPENSE"] = value;
			}
		}
		// model for database field AST_OTHER_STATE_ASSMT_FEE_SUSPENSE(int)
		public int AST_OTHER_STATE_ASSMT_FEE_SUSPENSE
		{
			get
			{
				return base.dtModel.Rows[0]["AST_OTHER_STATE_ASSMT_FEE_SUSPENSE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["AST_OTHER_STATE_ASSMT_FEE_SUSPENSE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AST_OTHER_STATE_ASSMT_FEE_SUSPENSE"] = value;
			}
		}
		// model for database field AST_COMM_RECV_REINS_EXCESS_CONTRACT(int)
		public int AST_COMM_RECV_REINS_EXCESS_CONTRACT
		{
			get
			{
				return base.dtModel.Rows[0]["AST_COMM_RECV_REINS_EXCESS_CONTRACT"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["AST_COMM_RECV_REINS_EXCESS_CONTRACT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AST_COMM_RECV_REINS_EXCESS_CONTRACT"] = value;
			}
		}
		// model for database field AST_COMM_RECV_REINS_UMBRELLA_CONTRACT(int)
		public int AST_COMM_RECV_REINS_UMBRELLA_CONTRACT
		{
			get
			{
				return base.dtModel.Rows[0]["AST_COMM_RECV_REINS_UMBRELLA_CONTRACT"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["AST_COMM_RECV_REINS_UMBRELLA_CONTRACT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AST_COMM_RECV_REINS_UMBRELLA_CONTRACT"] = value;
			}
		}
		// model for database field LIB_COMM_PAYB_AGENCY_BILL(int)
		public int LIB_COMM_PAYB_AGENCY_BILL
		{
			get
			{
				return base.dtModel.Rows[0]["LIB_COMM_PAYB_AGENCY_BILL"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LIB_COMM_PAYB_AGENCY_BILL"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LIB_COMM_PAYB_AGENCY_BILL"] = value;
			}
		}
		// model for database field LIB_COMM_PAYB_DIRECT_BILL(int)
		public int LIB_COMM_PAYB_DIRECT_BILL
		{
			get
			{
				return base.dtModel.Rows[0]["LIB_COMM_PAYB_DIRECT_BILL"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LIB_COMM_PAYB_DIRECT_BILL"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LIB_COMM_PAYB_DIRECT_BILL"] = value;
			}
		}
		// model for database field LIB_REINS_PAYB_EXCESS_CONTRACT(int)
		public int LIB_REINS_PAYB_EXCESS_CONTRACT
		{
			get
			{
				return base.dtModel.Rows[0]["LIB_REINS_PAYB_EXCESS_CONTRACT"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LIB_REINS_PAYB_EXCESS_CONTRACT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LIB_REINS_PAYB_EXCESS_CONTRACT"] = value;
			}
		}
		// model for database field LIB_REINS_PAYB_CAT_CONTRACT(int)
		public int LIB_REINS_PAYB_CAT_CONTRACT
		{
			get
			{
				return base.dtModel.Rows[0]["LIB_REINS_PAYB_CAT_CONTRACT"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LIB_REINS_PAYB_CAT_CONTRACT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LIB_REINS_PAYB_CAT_CONTRACT"] = value;
			}
		}
		// model for database field LIB_REINS_PAYB_MCCA(int)
		public int LIB_REINS_PAYB_MCCA
		{
			get
			{
				return base.dtModel.Rows[0]["LIB_REINS_PAYB_MCCA"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LIB_REINS_PAYB_MCCA"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LIB_REINS_PAYB_MCCA"] = value;
			}
		}
		// model for database field LIB_REINS_PAYB_UMBRELLA(int)
		public int LIB_REINS_PAYB_UMBRELLA
		{
			get
			{
				return base.dtModel.Rows[0]["LIB_REINS_PAYB_UMBRELLA"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LIB_REINS_PAYB_UMBRELLA"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LIB_REINS_PAYB_UMBRELLA"] = value;
			}
		}
		// model for database field LIB_REINS_PAYB_FACULTATIVE(int)
		public int LIB_REINS_PAYB_FACULTATIVE
		{
			get
			{
				return base.dtModel.Rows[0]["LIB_REINS_PAYB_FACULTATIVE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LIB_REINS_PAYB_FACULTATIVE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LIB_REINS_PAYB_FACULTATIVE"] = value;
			}
		}
		// model for database field LIB_OUT_DRAFTS(int)
		public int LIB_OUT_DRAFTS
		{
			get
			{
				return base.dtModel.Rows[0]["LIB_OUT_DRAFTS"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LIB_OUT_DRAFTS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LIB_OUT_DRAFTS"] = value;
			}
		}
		// model for database field LIB_ADVCE_PRM_DEPOSIT(int)
		public int LIB_ADVCE_PRM_DEPOSIT
		{
			get
			{
				return base.dtModel.Rows[0]["LIB_ADVCE_PRM_DEPOSIT"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LIB_ADVCE_PRM_DEPOSIT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LIB_ADVCE_PRM_DEPOSIT"] = value;
			}
		}
		// model for database field LIB_ADVCE_PRM_DEPOSIT_2M(int)
		public int LIB_ADVCE_PRM_DEPOSIT_2M
		{
			get
			{
				return base.dtModel.Rows[0]["LIB_ADVCE_PRM_DEPOSIT_2M"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LIB_ADVCE_PRM_DEPOSIT_2M"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LIB_ADVCE_PRM_DEPOSIT_2M"] = value;
			}
		}
		// model for database field LIB_UNEARN_PRM(int)
		public int LIB_UNEARN_PRM
		{
			get
			{
				return base.dtModel.Rows[0]["LIB_UNEARN_PRM"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LIB_UNEARN_PRM"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LIB_UNEARN_PRM"] = value;
			}
		}
		// model for database field LIB_UNEARN_PRM_MCCA(int)
		public int LIB_UNEARN_PRM_MCCA
		{
			get
			{
				return base.dtModel.Rows[0]["LIB_UNEARN_PRM_MCCA"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LIB_UNEARN_PRM_MCCA"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LIB_UNEARN_PRM_MCCA"] = value;
			}
		}
		// model for database field LIB_UNEARN_PRM_CEDED_UNEARN_MCCA_REINS(int)
		public int LIB_UNEARN_PRM_CEDED_UNEARN_MCCA_REINS
		{
			get
			{
				return base.dtModel.Rows[0]["LIB_UNEARN_PRM_CEDED_UNEARN_MCCA_REINS"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LIB_UNEARN_PRM_CEDED_UNEARN_MCCA_REINS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LIB_UNEARN_PRM_CEDED_UNEARN_MCCA_REINS"] = value;
			}
		}
		// model for database field LIB_UNEARN_PRM_CEDED_UNEARN_UMBRELLA_REINS(int)
		public int LIB_UNEARN_PRM_CEDED_UNEARN_UMBRELLA_REINS
		{
			get
			{
				return base.dtModel.Rows[0]["LIB_UNEARN_PRM_CEDED_UNEARN_UMBRELLA_REINS"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LIB_UNEARN_PRM_CEDED_UNEARN_UMBRELLA_REINS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LIB_UNEARN_PRM_CEDED_UNEARN_UMBRELLA_REINS"] = value;
			}
		}
		// model for database field LIB_UNEARN_PRM_OTH_STATE_ASSES_FEE(int)
		public int LIB_UNEARN_PRM_OTH_STATE_ASSES_FEE
		{
			get
			{
				return base.dtModel.Rows[0]["LIB_UNEARN_PRM_OTH_STATE_ASSES_FEE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LIB_UNEARN_PRM_OTH_STATE_ASSES_FEE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LIB_UNEARN_PRM_OTH_STATE_ASSES_FEE"] = value;
			}
		}
		// model for database field LIB_TAX_PAYB(int)
		public int LIB_TAX_PAYB
		{
			get
			{
				return base.dtModel.Rows[0]["LIB_TAX_PAYB"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LIB_TAX_PAYB"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LIB_TAX_PAYB"] = value;
			}
		}
		// model for database field LIB_VENDOR_PAYB(int)
		public int LIB_VENDOR_PAYB
		{
			get
			{
				return base.dtModel.Rows[0]["LIB_VENDOR_PAYB"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LIB_VENDOR_PAYB"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LIB_VENDOR_PAYB"] = value;
			}
		}
		// model for database field LIB_COLL_ON_NONISSUED_POLICY(int)
		public int LIB_COLL_ON_NONISSUED_POLICY
		{
			get
			{
				return base.dtModel.Rows[0]["LIB_COLL_ON_NONISSUED_POLICY"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LIB_COLL_ON_NONISSUED_POLICY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LIB_COLL_ON_NONISSUED_POLICY"] = value;
			}
		}
		// model for database field EQU_TRANSFER(int)
		public int EQU_TRANSFER
		{
			get
			{
				return base.dtModel.Rows[0]["EQU_TRANSFER"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["EQU_TRANSFER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EQU_TRANSFER"] = value;
			}
		}
		// model for database field INC_PRM_WRTN(int)
		public int INC_PRM_WRTN
		{
			get
			{
				return base.dtModel.Rows[0]["INC_PRM_WRTN"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["INC_PRM_WRTN"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INC_PRM_WRTN"] = value;
			}
		}
		// model for database field INC_PRM_WRTN_MCCA(int)
		public int INC_PRM_WRTN_MCCA
		{
			get
			{
				return base.dtModel.Rows[0]["INC_PRM_WRTN_MCCA"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["INC_PRM_WRTN_MCCA"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INC_PRM_WRTN_MCCA"] = value;
			}
		}
		// model for database field INC_PRM_WRTN_OTH_STATE_ASSESS_FEE(int)
		public int INC_PRM_WRTN_OTH_STATE_ASSESS_FEE
		{
			get
			{
				return base.dtModel.Rows[0]["INC_PRM_WRTN_OTH_STATE_ASSESS_FEE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["INC_PRM_WRTN_OTH_STATE_ASSESS_FEE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INC_PRM_WRTN_OTH_STATE_ASSESS_FEE"] = value;
			}
		}
		// model for database field INC_REINS_CEDED_EXCESS_CON(int)
		public int INC_REINS_CEDED_EXCESS_CON
		{
			get
			{
				return base.dtModel.Rows[0]["INC_REINS_CEDED_EXCESS_CON"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["INC_REINS_CEDED_EXCESS_CON"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INC_REINS_CEDED_EXCESS_CON"] = value;
			}
		}
		// model for database field INC_REINS_CEDED_CAT_CON(int)
		public int INC_REINS_CEDED_CAT_CON
		{
			get
			{
				return base.dtModel.Rows[0]["INC_REINS_CEDED_CAT_CON"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["INC_REINS_CEDED_CAT_CON"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INC_REINS_CEDED_CAT_CON"] = value;
			}
		}
		// model for database field INC_REINS_CEDED_UMBRELLA_CON(int)
		public int INC_REINS_CEDED_UMBRELLA_CON
		{
			get
			{
				return base.dtModel.Rows[0]["INC_REINS_CEDED_UMBRELLA_CON"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["INC_REINS_CEDED_UMBRELLA_CON"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INC_REINS_CEDED_UMBRELLA_CON"] = value;
			}
		}
		// model for database field INC_REINS_CEDED_FACUL_CON(int)
		public int INC_REINS_CEDED_FACUL_CON
		{
			get
			{
				return base.dtModel.Rows[0]["INC_REINS_CEDED_FACUL_CON"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["INC_REINS_CEDED_FACUL_CON"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INC_REINS_CEDED_FACUL_CON"] = value;
			}
		}
		// model for database field INC_REINS_CEDED_MCCA_CON(int)
		public int INC_REINS_CEDED_MCCA_CON
		{
			get
			{
				return base.dtModel.Rows[0]["INC_REINS_CEDED_MCCA_CON"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["INC_REINS_CEDED_MCCA_CON"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INC_REINS_CEDED_MCCA_CON"] = value;
			}
		}
		// model for database field INC_CHG_UNEARN_PRM(int)
		public int INC_CHG_UNEARN_PRM
		{
			get
			{
				return base.dtModel.Rows[0]["INC_CHG_UNEARN_PRM"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["INC_CHG_UNEARN_PRM"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INC_CHG_UNEARN_PRM"] = value;
			}
		}
		// model for database field INC_CHG_UNEARN_PRM_MCCA(int)
		public int INC_CHG_UNEARN_PRM_MCCA
		{
			get
			{
				return base.dtModel.Rows[0]["INC_CHG_UNEARN_PRM_MCCA"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["INC_CHG_UNEARN_PRM_MCCA"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INC_CHG_UNEARN_PRM_MCCA"] = value;
			}
		}
		// model for database field INC_CHG_UNEARN_PRM_OTH_STATE_FEE(int)
		public int INC_CHG_UNEARN_PRM_OTH_STATE_FEE
		{
			get
			{
				return base.dtModel.Rows[0]["INC_CHG_UNEARN_PRM_OTH_STATE_FEE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["INC_CHG_UNEARN_PRM_OTH_STATE_FEE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INC_CHG_UNEARN_PRM_OTH_STATE_FEE"] = value;
			}
		}
		// model for database field INC_CHG_CEDED_UNEARN_MCCA(int)
		public int INC_CHG_CEDED_UNEARN_MCCA
		{
			get
			{
				return base.dtModel.Rows[0]["INC_CHG_CEDED_UNEARN_MCCA"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["INC_CHG_CEDED_UNEARN_MCCA"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INC_CHG_CEDED_UNEARN_MCCA"] = value;
			}
		}
		// model for database field INC_CHG_CEDED_UNEARN_UMBRELLA_REINS(int)
		public int INC_CHG_CEDED_UNEARN_UMBRELLA_REINS
		{
			get
			{
				return base.dtModel.Rows[0]["INC_CHG_CEDED_UNEARN_UMBRELLA_REINS"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["INC_CHG_CEDED_UNEARN_UMBRELLA_REINS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INC_CHG_CEDED_UNEARN_UMBRELLA_REINS"] = value;
			}
		}
		// model for database field EXP_COMM_INCURRED(int)
		public int EXP_COMM_INCURRED
		{
			get
			{
				return base.dtModel.Rows[0]["EXP_COMM_INCURRED"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["EXP_COMM_INCURRED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EXP_COMM_INCURRED"] = value;
			}
		}
		// model for database field EXP_REINS_COMM_EXCESS_CON(int)
		public int EXP_REINS_COMM_EXCESS_CON
		{
			get
			{
				return base.dtModel.Rows[0]["EXP_REINS_COMM_EXCESS_CON"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["EXP_REINS_COMM_EXCESS_CON"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EXP_REINS_COMM_EXCESS_CON"] = value;
			}
		}
		// model for database field EXP_REINS_COMM_UMBRELLA_CON(int)
		public int EXP_REINS_COMM_UMBRELLA_CON
		{
			get
			{
				return base.dtModel.Rows[0]["EXP_REINS_COMM_UMBRELLA_CON"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["EXP_REINS_COMM_UMBRELLA_CON"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EXP_REINS_COMM_UMBRELLA_CON"] = value;
			}
		}
		// model for database field EXP_ASSIGNED_CLAIMS(int)
		public int EXP_ASSIGNED_CLAIMS
		{
			get
			{
				return base.dtModel.Rows[0]["EXP_ASSIGNED_CLAIMS"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["EXP_ASSIGNED_CLAIMS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EXP_ASSIGNED_CLAIMS"] = value;
			}
		}
		// model for database field EXP_REINS_PAID_LOSSES(int)
		public int EXP_REINS_PAID_LOSSES
		{
			get
			{
				return base.dtModel.Rows[0]["EXP_REINS_PAID_LOSSES"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["EXP_REINS_PAID_LOSSES"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EXP_REINS_PAID_LOSSES"] = value;
			}
		}
		// model for database field EXP_REINS_PAID_LOSSES_CAT(int)
		public int EXP_REINS_PAID_LOSSES_CAT
		{
			get
			{
				return base.dtModel.Rows[0]["EXP_REINS_PAID_LOSSES_CAT"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["EXP_REINS_PAID_LOSSES_CAT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EXP_REINS_PAID_LOSSES_CAT"] = value;
			}
		}
		#endregion
	}
}
