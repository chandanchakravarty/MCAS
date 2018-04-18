/******************************************************************************************
<Author				: -   Ajit Singh Chahal
<Start Date				: -	5/26/2005 10:27:46 AM
<End Date				: -	
<Description				: - 	Model for Posting Interface.
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

namespace Cms.Model.Maintenance.Accounting
{
	/// <summary>
	/// Database Model for ACT_GENERAL_LEDGER.
	/// </summary>
	public class ClsPostingInterfaceInfo : Cms.Model.ClsBaseModel
	{
		private const string ACT_GENERAL_LEDGER = "ACT_GENERAL_LEDGER";
		public ClsPostingInterfaceInfo()
		{
			base.dtModel.TableName = "ACT_GENERAL_LEDGER";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table ACT_GENERAL_LEDGER
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("GL_ID",typeof(int));
			base.dtModel.Columns.Add("FISCAL_ID",typeof(int));
			base.dtModel.Columns.Add("MODIFIED_BY",typeof(int));
			base.dtModel.Columns.Add("LAST_UPDATED_DATETIME",typeof(DateTime));
			base.dtModel.Columns.Add("AST_UNCOLL_PRM_CUSTOMER",typeof(int));
			base.dtModel.Columns.Add("AST_UNCOLL_PRM_AGENCY",typeof(int));
			base.dtModel.Columns.Add("AST_UNCOLL_PRM_SUSPENSE_AGENCY_BILL",typeof(int));
			base.dtModel.Columns.Add("AST_PRM_WRIT_SUSPENSE_DIRECT_BILL",typeof(int));
			base.dtModel.Columns.Add("AST_PRM_WRIT_SUSPENSE_AGENCY_BILL",typeof(int));
			base.dtModel.Columns.Add("AST_MCCA_FEE_SUSPENSE_DIRECT_BILL",typeof(int));
			base.dtModel.Columns.Add("AST_MCCA_FEE_SUSPENSE_AGENCY_BILL",typeof(int));
			base.dtModel.Columns.Add("AST_OTHER_STATE_ASSMT_FEE_SUSPENSE_DIRECT_BILL",typeof(int));
			base.dtModel.Columns.Add("AST_OTHER_STATE_ASSMT_FEE_SUSPENSE_AGENCY_BILL",typeof(int));
			base.dtModel.Columns.Add("AST_COMM_RECV_REINS_EXCESS_CONTRACT",typeof(int));
			base.dtModel.Columns.Add("AST_COMM_RECV_REINS_UMBRELLA_CONTRACT",typeof(int));
			base.dtModel.Columns.Add("AST_UNCOLL_PREM_IN_SUSPENSE_DIRECT_BILL",typeof(int));
			base.dtModel.Columns.Add("AST_COMM_EXPENSE_IN_SUSPENSE_DIRECT_BILL",typeof(int));
			base.dtModel.Columns.Add("AST_COMM_PAYABLE_IN_SUSPENSE_DIRECT_BILL",typeof(int));
			base.dtModel.Columns.Add("AST_COMM_EXPENSE_IN_SUSPENSE_AGENCY_BILL",typeof(int));
			base.dtModel.Columns.Add("AST_COMM_PAYABLE_IN_SUSPENSE_AGENCY_BILL",typeof(int));
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
			base.dtModel.Columns.Add("EQU_UNASSIGNED_SURPLUS",typeof(int));
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
			base.dtModel.Columns.Add("INC_INSTALLMENT_FEES",typeof(int));
			base.dtModel.Columns.Add("INC_RE_INSTATEMENT_FEES",typeof(int));
			base.dtModel.Columns.Add("INC_NON_SUFFICIENT_FUND_FEES",typeof(int));
			base.dtModel.Columns.Add("INC_LATE_FEES",typeof(int));
			base.dtModel.Columns.Add("INC_SERVICE_CHARGE",typeof(int));
			base.dtModel.Columns.Add("INC_CONVENIENCE_FEE",typeof(int));
			base.dtModel.Columns.Add("EXP_COMM_INCURRED",typeof(int));
			base.dtModel.Columns.Add("EXP_REINS_COMM_EXCESS_CON",typeof(int));
			base.dtModel.Columns.Add("EXP_REINS_COMM_UMBRELLA_CON",typeof(int));
			base.dtModel.Columns.Add("EXP_ASSIGNED_CLAIMS",typeof(int));
			base.dtModel.Columns.Add("EXP_REINS_PAID_LOSSES",typeof(int));
			base.dtModel.Columns.Add("EXP_REINS_PAID_LOSSES_CAT",typeof(int));
			base.dtModel.Columns.Add("EXP_SMALL_BALANCE_WRITE_OFF",typeof(int));
			base.dtModel.Columns.Add("BNK_SUSPENSE_AMOUNT",typeof(int));
			base.dtModel.Columns.Add("BNK_RETURN_PRM_PAYMENT",typeof(int));
			base.dtModel.Columns.Add("BNK_OVER_PAYMENT",typeof(int));
			base.dtModel.Columns.Add("BNK_CLAIMS_DEFAULT_AC",typeof(int));
			base.dtModel.Columns.Add("BNK_REINSURANCE_DEFAULT_AC",typeof(int));
			base.dtModel.Columns.Add("BNK_DEPOSITS_DEFAULT_AC",typeof(int));
			base.dtModel.Columns.Add("BNK_MISC_CHK_DEFAULT_AC",typeof(int));
			base.dtModel.Columns.Add("BNK_CUST_DEP_EFT_CARD",typeof(int));
			base.dtModel.Columns.Add("CLM_CHECK_DEFAULT_AC",typeof(int));
			base.dtModel.Columns.Add("BNK_AGEN_CHK_DEFAULT_AC",typeof(int));

            base.dtModel.Columns.Add("INC_INTEREST_AMOUNT", typeof(int));
            base.dtModel.Columns.Add("INC_POLICY_TAXES", typeof(int));
            base.dtModel.Columns.Add("INC_POLICY_FEES", typeof(int));

		}
		#region Database schema details
		
		// model for database field BNK_SUSPENSE_AMOUNT(int)
		public int BNK_SUSPENSE_AMOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["BNK_SUSPENSE_AMOUNT"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["BNK_SUSPENSE_AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["BNK_SUSPENSE_AMOUNT"] = value;
			}
		}
		// model for database field BNK_RETURN_PRM_PAYMENT(int)
		public int BNK_RETURN_PRM_PAYMENT
		{
			get
			{
				return base.dtModel.Rows[0]["BNK_RETURN_PRM_PAYMENT"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["BNK_RETURN_PRM_PAYMENT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["BNK_RETURN_PRM_PAYMENT"] = value;
			}
		}
		// model for database field GL_ID(int)
		public int BNK_OVER_PAYMENT
		{
			get
			{
				return base.dtModel.Rows[0]["BNK_OVER_PAYMENT"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["BNK_OVER_PAYMENT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["BNK_OVER_PAYMENT"] = value;
			}
		}	
		// model for database field GL_ID(int)
		public int GL_ID
		{
			get
			{
				return base.dtModel.Rows[0]["GL_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["GL_ID"].ToString());
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
				return base.dtModel.Rows[0]["FISCAL_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["FISCAL_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["FISCAL_ID"] = value;
			}
		}
		// model for database field MODIFIED_BY(int)
		public int MODIFIED_BY
		{
			get
			{
				return base.dtModel.Rows[0]["MODIFIED_BY"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["MODIFIED_BY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MODIFIED_BY"] = value;
			}
		}
		// model for database field LAST_UPDATED_DATETIME(DateTime)
		public DateTime LAST_UPDATED_DATETIME
		{
			get
			{
				return base.dtModel.Rows[0]["LAST_UPDATED_DATETIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["LAST_UPDATED_DATETIME"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LAST_UPDATED_DATETIME"] = value;
			}
		}
		// model for database field AST_UNCOLL_PRM_CUSTOMER(int)
		public int AST_UNCOLL_PRM_CUSTOMER
		{
			get
			{
				return base.dtModel.Rows[0]["AST_UNCOLL_PRM_CUSTOMER"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["AST_UNCOLL_PRM_CUSTOMER"].ToString());
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
				return base.dtModel.Rows[0]["AST_UNCOLL_PRM_AGENCY"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["AST_UNCOLL_PRM_AGENCY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AST_UNCOLL_PRM_AGENCY"] = value;
			}
		}
		// model for database field AST_UNCOLL_PRM_SUSPENSE_AGENCY_BILL(int)
		public int AST_UNCOLL_PRM_SUSPENSE_AGENCY_BILL
		{
			get
			{
				return base.dtModel.Rows[0]["AST_UNCOLL_PRM_SUSPENSE_AGENCY_BILL"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["AST_UNCOLL_PRM_SUSPENSE_AGENCY_BILL"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AST_UNCOLL_PRM_SUSPENSE_AGENCY_BILL"] = value;
			}
		}
		// model for database field AST_PRM_WRIT_SUSPENSE_DIRECT_BILL(int)
		public int AST_PRM_WRIT_SUSPENSE_DIRECT_BILL
		{
			get
			{
				return base.dtModel.Rows[0]["AST_PRM_WRIT_SUSPENSE_DIRECT_BILL"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["AST_PRM_WRIT_SUSPENSE_DIRECT_BILL"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AST_PRM_WRIT_SUSPENSE_DIRECT_BILL"] = value;
			}
		}
		// model for database field AST_PRM_WRIT_SUSPENSE_AGENCY_BILL(int)
		public int AST_PRM_WRIT_SUSPENSE_AGENCY_BILL
		{
			get
			{
				return base.dtModel.Rows[0]["AST_PRM_WRIT_SUSPENSE_AGENCY_BILL"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["AST_PRM_WRIT_SUSPENSE_AGENCY_BILL"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AST_PRM_WRIT_SUSPENSE_AGENCY_BILL"] = value;
			}
		}
		// model for database field AST_MCCA_FEE_SUSPENSE_DIRECT_BILL(int)
		public int AST_MCCA_FEE_SUSPENSE_DIRECT_BILL
		{
			get
			{
				return base.dtModel.Rows[0]["AST_MCCA_FEE_SUSPENSE_DIRECT_BILL"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["AST_MCCA_FEE_SUSPENSE_DIRECT_BILL"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AST_MCCA_FEE_SUSPENSE_DIRECT_BILL"] = value;
			}
		}
		// model for database field AST_MCCA_FEE_SUSPENSE_AGENCY_BILL(int)
		public int AST_MCCA_FEE_SUSPENSE_AGENCY_BILL
		{
			get
			{
				return base.dtModel.Rows[0]["AST_MCCA_FEE_SUSPENSE_AGENCY_BILL"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["AST_MCCA_FEE_SUSPENSE_AGENCY_BILL"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AST_MCCA_FEE_SUSPENSE_AGENCY_BILL"] = value;
			}
		}
		// model for database field AST_OTHER_STATE_ASSMT_FEE_SUSPENSE_DIRECT_BILL(int)
		public int AST_OTHER_STATE_ASSMT_FEE_SUSPENSE_DIRECT_BILL
		{
			get
			{
				return base.dtModel.Rows[0]["AST_OTHER_STATE_ASSMT_FEE_SUSPENSE_DIRECT_BILL"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["AST_OTHER_STATE_ASSMT_FEE_SUSPENSE_DIRECT_BILL"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AST_OTHER_STATE_ASSMT_FEE_SUSPENSE_DIRECT_BILL"] = value;
			}
		}
		// model for database field AST_OTHER_STATE_ASSMT_FEE_SUSPENSE_AGENCY_BILL(int)
		public int AST_OTHER_STATE_ASSMT_FEE_SUSPENSE_AGENCY_BILL
		{
			get
			{
				return base.dtModel.Rows[0]["AST_OTHER_STATE_ASSMT_FEE_SUSPENSE_AGENCY_BILL"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["AST_OTHER_STATE_ASSMT_FEE_SUSPENSE_AGENCY_BILL"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AST_OTHER_STATE_ASSMT_FEE_SUSPENSE_AGENCY_BILL"] = value;
			}
		}
		// model for database field AST_COMM_RECV_REINS_EXCESS_CONTRACT(int)
		public int AST_COMM_RECV_REINS_EXCESS_CONTRACT
		{
			get
			{
				return base.dtModel.Rows[0]["AST_COMM_RECV_REINS_EXCESS_CONTRACT"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["AST_COMM_RECV_REINS_EXCESS_CONTRACT"].ToString());
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
				return base.dtModel.Rows[0]["AST_COMM_RECV_REINS_UMBRELLA_CONTRACT"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["AST_COMM_RECV_REINS_UMBRELLA_CONTRACT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AST_COMM_RECV_REINS_UMBRELLA_CONTRACT"] = value;
			}
		}
		// model for database field AST_UNCOLL_PREM_IN_SUSPENSE_DIRECT_BILL(int)
		public int AST_UNCOLL_PREM_IN_SUSPENSE_DIRECT_BILL
		{
			get
			{
				return base.dtModel.Rows[0]["AST_UNCOLL_PREM_IN_SUSPENSE_DIRECT_BILL"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["AST_UNCOLL_PREM_IN_SUSPENSE_DIRECT_BILL"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AST_UNCOLL_PREM_IN_SUSPENSE_DIRECT_BILL"] = value;
			}
		}
		// model for database field AST_COMM_EXPENSE_IN_SUSPENSE_DIRECT_BILL(int)
		public int AST_COMM_EXPENSE_IN_SUSPENSE_DIRECT_BILL
		{
			get
			{
				return base.dtModel.Rows[0]["AST_COMM_EXPENSE_IN_SUSPENSE_DIRECT_BILL"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["AST_COMM_EXPENSE_IN_SUSPENSE_DIRECT_BILL"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AST_COMM_EXPENSE_IN_SUSPENSE_DIRECT_BILL"] = value;
			}
		}
		// model for database field AST_COMM_PAYABLE_IN_SUSPENSE_DIRECT_BILL(int)
		public int AST_COMM_PAYABLE_IN_SUSPENSE_DIRECT_BILL
		{
			get
			{
				return base.dtModel.Rows[0]["AST_COMM_PAYABLE_IN_SUSPENSE_DIRECT_BILL"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["AST_COMM_PAYABLE_IN_SUSPENSE_DIRECT_BILL"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AST_COMM_PAYABLE_IN_SUSPENSE_DIRECT_BILL"] = value;
			}
		}
		// model for database field AST_COMM_EXPENSE_IN_SUSPENSE_AGENCY_BILL(int)
		public int AST_COMM_EXPENSE_IN_SUSPENSE_AGENCY_BILL
		{
			get
			{
				return base.dtModel.Rows[0]["AST_COMM_EXPENSE_IN_SUSPENSE_AGENCY_BILL"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["AST_COMM_EXPENSE_IN_SUSPENSE_AGENCY_BILL"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AST_COMM_EXPENSE_IN_SUSPENSE_AGENCY_BILL"] = value;
			}
		}
		// model for database field AST_COMM_PAYABLE_IN_SUSPENSE_AGENCY_BILL(int)
		public int AST_COMM_PAYABLE_IN_SUSPENSE_AGENCY_BILL
		{
			get
			{
				return base.dtModel.Rows[0]["AST_COMM_PAYABLE_IN_SUSPENSE_AGENCY_BILL"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["AST_COMM_PAYABLE_IN_SUSPENSE_AGENCY_BILL"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AST_COMM_PAYABLE_IN_SUSPENSE_AGENCY_BILL"] = value;
			}
		}
		// model for database field LIB_COMM_PAYB_AGENCY_BILL(int)
		public int LIB_COMM_PAYB_AGENCY_BILL
		{
			get
			{
				return base.dtModel.Rows[0]["LIB_COMM_PAYB_AGENCY_BILL"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LIB_COMM_PAYB_AGENCY_BILL"].ToString());
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
				return base.dtModel.Rows[0]["LIB_COMM_PAYB_DIRECT_BILL"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LIB_COMM_PAYB_DIRECT_BILL"].ToString());
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
				return base.dtModel.Rows[0]["LIB_REINS_PAYB_EXCESS_CONTRACT"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LIB_REINS_PAYB_EXCESS_CONTRACT"].ToString());
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
				return base.dtModel.Rows[0]["LIB_REINS_PAYB_CAT_CONTRACT"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LIB_REINS_PAYB_CAT_CONTRACT"].ToString());
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
				return base.dtModel.Rows[0]["LIB_REINS_PAYB_MCCA"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LIB_REINS_PAYB_MCCA"].ToString());
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
				return base.dtModel.Rows[0]["LIB_REINS_PAYB_UMBRELLA"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LIB_REINS_PAYB_UMBRELLA"].ToString());
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
				return base.dtModel.Rows[0]["LIB_REINS_PAYB_FACULTATIVE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LIB_REINS_PAYB_FACULTATIVE"].ToString());
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
				return base.dtModel.Rows[0]["LIB_OUT_DRAFTS"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LIB_OUT_DRAFTS"].ToString());
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
				return base.dtModel.Rows[0]["LIB_ADVCE_PRM_DEPOSIT"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LIB_ADVCE_PRM_DEPOSIT"].ToString());
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
				return base.dtModel.Rows[0]["LIB_ADVCE_PRM_DEPOSIT_2M"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LIB_ADVCE_PRM_DEPOSIT_2M"].ToString());
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
				return base.dtModel.Rows[0]["LIB_UNEARN_PRM"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LIB_UNEARN_PRM"].ToString());
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
				return base.dtModel.Rows[0]["LIB_UNEARN_PRM_MCCA"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LIB_UNEARN_PRM_MCCA"].ToString());
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
				return base.dtModel.Rows[0]["LIB_UNEARN_PRM_CEDED_UNEARN_MCCA_REINS"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LIB_UNEARN_PRM_CEDED_UNEARN_MCCA_REINS"].ToString());
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
				return base.dtModel.Rows[0]["LIB_UNEARN_PRM_CEDED_UNEARN_UMBRELLA_REINS"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LIB_UNEARN_PRM_CEDED_UNEARN_UMBRELLA_REINS"].ToString());
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
				return base.dtModel.Rows[0]["LIB_UNEARN_PRM_OTH_STATE_ASSES_FEE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LIB_UNEARN_PRM_OTH_STATE_ASSES_FEE"].ToString());
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
				return base.dtModel.Rows[0]["LIB_TAX_PAYB"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LIB_TAX_PAYB"].ToString());
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
				return base.dtModel.Rows[0]["LIB_VENDOR_PAYB"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LIB_VENDOR_PAYB"].ToString());
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
				return base.dtModel.Rows[0]["LIB_COLL_ON_NONISSUED_POLICY"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LIB_COLL_ON_NONISSUED_POLICY"].ToString());
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
				return base.dtModel.Rows[0]["EQU_TRANSFER"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["EQU_TRANSFER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EQU_TRANSFER"] = value;
			}
		}
		// model for database field EQU_UNASSIGNED_SURPLUS(int)
		public int EQU_UNASSIGNED_SURPLUS
		{
			get
			{
				return base.dtModel.Rows[0]["EQU_UNASSIGNED_SURPLUS"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["EQU_UNASSIGNED_SURPLUS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EQU_UNASSIGNED_SURPLUS"] = value;
			}
		}
		
		// model for database field INC_PRM_WRTN(int)
		public int INC_PRM_WRTN
		{
			get
			{
				return base.dtModel.Rows[0]["INC_PRM_WRTN"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["INC_PRM_WRTN"].ToString());
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
				return base.dtModel.Rows[0]["INC_PRM_WRTN_MCCA"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["INC_PRM_WRTN_MCCA"].ToString());
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
				return base.dtModel.Rows[0]["INC_PRM_WRTN_OTH_STATE_ASSESS_FEE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["INC_PRM_WRTN_OTH_STATE_ASSESS_FEE"].ToString());
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
				return base.dtModel.Rows[0]["INC_REINS_CEDED_EXCESS_CON"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["INC_REINS_CEDED_EXCESS_CON"].ToString());
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
				return base.dtModel.Rows[0]["INC_REINS_CEDED_CAT_CON"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["INC_REINS_CEDED_CAT_CON"].ToString());
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
				return base.dtModel.Rows[0]["INC_REINS_CEDED_UMBRELLA_CON"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["INC_REINS_CEDED_UMBRELLA_CON"].ToString());
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
				return base.dtModel.Rows[0]["INC_REINS_CEDED_FACUL_CON"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["INC_REINS_CEDED_FACUL_CON"].ToString());
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
				return base.dtModel.Rows[0]["INC_REINS_CEDED_MCCA_CON"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["INC_REINS_CEDED_MCCA_CON"].ToString());
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
				return base.dtModel.Rows[0]["INC_CHG_UNEARN_PRM"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["INC_CHG_UNEARN_PRM"].ToString());
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
				return base.dtModel.Rows[0]["INC_CHG_UNEARN_PRM_MCCA"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["INC_CHG_UNEARN_PRM_MCCA"].ToString());
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
				return base.dtModel.Rows[0]["INC_CHG_UNEARN_PRM_OTH_STATE_FEE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["INC_CHG_UNEARN_PRM_OTH_STATE_FEE"].ToString());
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
				return base.dtModel.Rows[0]["INC_CHG_CEDED_UNEARN_MCCA"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["INC_CHG_CEDED_UNEARN_MCCA"].ToString());
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
				return base.dtModel.Rows[0]["INC_CHG_CEDED_UNEARN_UMBRELLA_REINS"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["INC_CHG_CEDED_UNEARN_UMBRELLA_REINS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INC_CHG_CEDED_UNEARN_UMBRELLA_REINS"] = value;
			}
		}
		// model for database field INC_INSTALLMENT_FEES
		public int INC_INSTALLMENT_FEES
		{
			get
			{
				return base.dtModel.Rows[0]["INC_INSTALLMENT_FEES"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["INC_INSTALLMENT_FEES"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INC_INSTALLMENT_FEES"] = value;
			}
		}
		// model for database field INC_RE_INSTATEMENT_FEES
		public int INC_RE_INSTATEMENT_FEES
		{
			get
			{
				return base.dtModel.Rows[0]["INC_RE_INSTATEMENT_FEES"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["INC_RE_INSTATEMENT_FEES"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INC_RE_INSTATEMENT_FEES"] = value;
			}
		}
		// model for database field INC_NON_SUFFICIENT_FUND_FEES
		public int INC_NON_SUFFICIENT_FUND_FEES
		{
			get
			{
				return base.dtModel.Rows[0]["INC_NON_SUFFICIENT_FUND_FEES"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["INC_NON_SUFFICIENT_FUND_FEES"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INC_NON_SUFFICIENT_FUND_FEES"] = value;
			}
		}
		// model for database field INC_LATE_FEES
		public int INC_LATE_FEES
		{
			get
			{
				return base.dtModel.Rows[0]["INC_LATE_FEES"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["INC_LATE_FEES"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INC_LATE_FEES"] = value;
			}
		}
		public int INC_SERVICE_CHARGE
		{
			get
			{
				return base.dtModel.Rows[0]["INC_SERVICE_CHARGE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["INC_SERVICE_CHARGE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INC_SERVICE_CHARGE"] = value;
			}
		}
		public int INC_CONVENIENCE_FEE
		{
			get
			{
				return base.dtModel.Rows[0]["INC_CONVENIENCE_FEE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["INC_CONVENIENCE_FEE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INC_CONVENIENCE_FEE"] = value;
			}
		}
		// model for database field EXP_COMM_INCURRED(int)
		public int EXP_COMM_INCURRED
		{
			get
			{
				return base.dtModel.Rows[0]["EXP_COMM_INCURRED"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["EXP_COMM_INCURRED"].ToString());
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
				return base.dtModel.Rows[0]["EXP_REINS_COMM_EXCESS_CON"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["EXP_REINS_COMM_EXCESS_CON"].ToString());
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
				return base.dtModel.Rows[0]["EXP_REINS_COMM_UMBRELLA_CON"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["EXP_REINS_COMM_UMBRELLA_CON"].ToString());
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
				return base.dtModel.Rows[0]["EXP_ASSIGNED_CLAIMS"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["EXP_ASSIGNED_CLAIMS"].ToString());
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
				return base.dtModel.Rows[0]["EXP_REINS_PAID_LOSSES"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["EXP_REINS_PAID_LOSSES"].ToString());
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
				return base.dtModel.Rows[0]["EXP_REINS_PAID_LOSSES_CAT"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["EXP_REINS_PAID_LOSSES_CAT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EXP_REINS_PAID_LOSSES_CAT"] = value;
			}
		}
		// model for database field EXP_SMALL_BALANCE_WRITE_OFF(int)
		public int EXP_SMALL_BALANCE_WRITE_OFF
		{
			get
			{
				return base.dtModel.Rows[0]["EXP_SMALL_BALANCE_WRITE_OFF"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["EXP_SMALL_BALANCE_WRITE_OFF"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EXP_SMALL_BALANCE_WRITE_OFF"] = value;
			}
		}
		// model for database field BNK_CLAIMS_DEFAULT_AC(int)
		public int BNK_CLAIMS_DEFAULT_AC
		{
			get
			{
				return base.dtModel.Rows[0]["BNK_CLAIMS_DEFAULT_AC"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["BNK_CLAIMS_DEFAULT_AC"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["BNK_CLAIMS_DEFAULT_AC"] = value;
			}
		}
		// model for database field BNK_REINSURANCE_DEFAULT_AC(int)
		public int BNK_REINSURANCE_DEFAULT_AC
		{
			get
			{
				return base.dtModel.Rows[0]["BNK_REINSURANCE_DEFAULT_AC"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["BNK_REINSURANCE_DEFAULT_AC"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["BNK_REINSURANCE_DEFAULT_AC"] = value;
			}
		}
		// model for database field BNK_DEPOSITS_DEFAULT_AC(int)
		public int BNK_DEPOSITS_DEFAULT_AC
		{
			get
			{
				return base.dtModel.Rows[0]["BNK_DEPOSITS_DEFAULT_AC"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["BNK_DEPOSITS_DEFAULT_AC"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["BNK_DEPOSITS_DEFAULT_AC"] = value;
			}
		}
		// model for database field BNK_MISC_CHK_DEFAULT_AC(int)
		public int BNK_MISC_CHK_DEFAULT_AC
		{
			get
			{
				return base.dtModel.Rows[0]["BNK_MISC_CHK_DEFAULT_AC"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["BNK_MISC_CHK_DEFAULT_AC"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["BNK_MISC_CHK_DEFAULT_AC"] = value;
			}
		}

		// model for database field CLM_CHECK_DEFAULT_AC(int)
		public int CLM_CHECK_DEFAULT_AC
		{
			get
			{
				return base.dtModel.Rows[0]["CLM_CHECK_DEFAULT_AC"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CLM_CHECK_DEFAULT_AC"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CLM_CHECK_DEFAULT_AC"] = value;
			}
		}
		// model for database field BNK_CUST_DEP_EFT_CARD(int)
		public int BNK_CUST_DEP_EFT_CARD
		{
			get
			{
				return base.dtModel.Rows[0]["BNK_CUST_DEP_EFT_CARD"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["BNK_CUST_DEP_EFT_CARD"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["BNK_CUST_DEP_EFT_CARD"] = value;
			}
		}	

		//Model for BNK_AGEN_CHK_DEFAULT_AC
		public int BNK_AGEN_CHK_DEFAULT_AC
		{
			get
			{
				return base.dtModel.Rows[0]["BNK_AGEN_CHK_DEFAULT_AC"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["BNK_AGEN_CHK_DEFAULT_AC"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["BNK_AGEN_CHK_DEFAULT_AC"] = value;
			}
		}

        //Added by Pradeep Kushwaha on 30-August-2010

        //Model for INC_INTEREST_AMOUNT
        public int INC_INTEREST_AMOUNT
        {
            get
            {
                return base.dtModel.Rows[0]["INC_INTEREST_AMOUNT"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["INC_INTEREST_AMOUNT"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["INC_INTEREST_AMOUNT"] = value;
            }
        }
        //Model for INC_POLICY_TAXES
        public int INC_POLICY_TAXES
        {
            get
            {
                return base.dtModel.Rows[0]["INC_POLICY_TAXES"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["INC_POLICY_TAXES"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["INC_POLICY_TAXES"] = value;
            }
        }
        //Model for INC_POLICY_FEES
        public int INC_POLICY_FEES
        {
            get
            {
                return base.dtModel.Rows[0]["INC_POLICY_FEES"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["INC_POLICY_FEES"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["INC_POLICY_FEES"] = value;
            }
        }
        //Added till here 
		#endregion
	}
}
