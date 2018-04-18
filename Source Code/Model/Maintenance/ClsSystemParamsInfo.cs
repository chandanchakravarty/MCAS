/******************************************************************************************
<Author					: - Manoj Rathore 
<Start Date				: -	09 July 2007 4:28:03 PM
<End Date				: -	
<Description			: - 	Model for General SetUP table.
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 30-June-2010
<Modified By			: - Pradeep Kushwaha
<Purpose				: - Add new column NOTIFY_RECVE_INSURED int 
*******************************************************************************************/

using System;
using System.Data;
using Cms.Model;
namespace Cms.Model.Maintenance
{
	/// <summary>
	/// Database Model for MNT_SYSTEM_PARAMS.
	/// </summary>
	public class ClsSystemParamsInfo : Cms.Model.ClsCommonModel
	{
		private const string MNT_SYSTEM_PARAMS = "MNT_SYSTEM_PARAMS";
		public ClsSystemParamsInfo()
		{
			base.dtModel.TableName = "MNT_SYSTEM_PARAMS";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table MNT_SYSTEM_PARAMS
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("SYS_BAD_LOGON_ATTMPT",typeof(int));
			base.dtModel.Columns.Add("SYS_RENEWAL_FOR_ALERT",typeof(int));
			base.dtModel.Columns.Add("SYS_PENDING_QUOTE_FOR_ALERT",typeof(int));
			base.dtModel.Columns.Add("SYS_QUOTED_QUOTE_FOR_ALERT",typeof(int));
			base.dtModel.Columns.Add("SYS_NUM_DAYS_EXPIRE",typeof(int));
			base.dtModel.Columns.Add("SYS_NUM_DAYS_PEN_TO_NTU",typeof(int));
			base.dtModel.Columns.Add("SYS_NUM_DAYS_EXPIRE_QUOTE",typeof(int));
			base.dtModel.Columns.Add("SYS_DEFAULT_POL_TERM",typeof(int));
			base.dtModel.Columns.Add("SYS_GRAPH_LOGO_ALLOW",typeof(string));
			base.dtModel.Columns.Add("SYS_INSTALLMENT_FEES",typeof(decimal));
			base.dtModel.Columns.Add("SYS_NON_SUFFICIENT_FUND_FEES",typeof(decimal));
			base.dtModel.Columns.Add("SYS_REINSTATEMENT_FEES",typeof(decimal));
			base.dtModel.Columns.Add("SYS_EMPLOYEE_DISCOUNT",typeof(decimal));
			base.dtModel.Columns.Add("SYS_PRINT_FOLLOWING",typeof(string));
			base.dtModel.Columns.Add("SYS_CLAIM_NO",typeof(int));
			base.dtModel.Columns.Add("SYS_INSURANCE_SCORE_VALIDITY",typeof(string));
			base.dtModel.Columns.Add("SYS_Min_Install_Plan",typeof(decimal));
			base.dtModel.Columns.Add("SYS_AmtUnder_Payment",typeof(decimal));
			base.dtModel.Columns.Add("SYS_MinDays_Premium",typeof(int));
			base.dtModel.Columns.Add("SYS_MinDays_Cancel",typeof(int));
			base.dtModel.Columns.Add("SYS_Post_Phone",typeof(int));
			base.dtModel.Columns.Add("SYS_Post_Cancel",typeof(int));
			base.dtModel.Columns.Add("SYS_INDICATE_POL_AS",typeof(string));
			base.dtModel.Columns.Add("POSTAGE_FEE",typeof(int));
			base.dtModel.Columns.Add("RESTR_DELIV_FEE",typeof(int));
			base.dtModel.Columns.Add("CERTIFIED_FEE",typeof(int));
			base.dtModel.Columns.Add("RET_RECEIPT_FEE",typeof(int));
            base.dtModel.Columns.Add("NOTIFY_RECVE_INSURED", typeof(int));
            base.dtModel.Columns.Add("BASE_CURRENCY", typeof(int));
            base.dtModel.Columns.Add("DAYS_FOR_BOLETO_EXPIRATION", typeof(int));
			
		}
		#region Database schema details
		// model for database field SYS_BAD_LOGON_ATTMPT(int)
		public int SYS_BAD_LOGON_ATTMPT
		{
			get
			{
				return base.dtModel.Rows[0]["SYS_BAD_LOGON_ATTMPT"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["SYS_BAD_LOGON_ATTMPT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SYS_BAD_LOGON_ATTMPT"] = value;
			}
		}
		// model for database field SYS_RENEWAL_FOR_ALERT(int)
		public int SYS_RENEWAL_FOR_ALERT
		{
			get
			{
				return base.dtModel.Rows[0]["SYS_RENEWAL_FOR_ALERT"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["SYS_RENEWAL_FOR_ALERT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SYS_RENEWAL_FOR_ALERT"] = value;
			}
		}
		// model for database field SYS_PENDING_QUOTE_FOR_ALERT(int)
		public int SYS_PENDING_QUOTE_FOR_ALERT
		{
			get
			{
				return base.dtModel.Rows[0]["SYS_PENDING_QUOTE_FOR_ALERT"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["SYS_PENDING_QUOTE_FOR_ALERT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SYS_PENDING_QUOTE_FOR_ALERT"] = value;
			}
		}
		// model for database field SYS_QUOTED_QUOTE_FOR_ALERT(int)
		public int SYS_QUOTED_QUOTE_FOR_ALERT
		{
			get
			{
				return base.dtModel.Rows[0]["SYS_QUOTED_QUOTE_FOR_ALERT"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["SYS_QUOTED_QUOTE_FOR_ALERT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SYS_QUOTED_QUOTE_FOR_ALERT"] = value;
			}
		}
		// model for database field SYS_NUM_DAYS_EXPIRE(int)
		public int SYS_NUM_DAYS_EXPIRE
		{
			get
			{
				return base.dtModel.Rows[0]["SYS_NUM_DAYS_EXPIRE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["SYS_NUM_DAYS_EXPIRE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SYS_NUM_DAYS_EXPIRE"] = value;
			}
		}
		// model for database field SYS_NUM_DAYS_PEN_TO_NTU(int)
		public int SYS_NUM_DAYS_PEN_TO_NTU
		{
			get
			{
				return base.dtModel.Rows[0]["SYS_NUM_DAYS_PEN_TO_NTU"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["SYS_NUM_DAYS_PEN_TO_NTU"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SYS_NUM_DAYS_PEN_TO_NTU"] = value;
			}
		}
		// model for database field SYS_NUM_DAYS_EXPIRE_QUOTE(int)
		public int SYS_NUM_DAYS_EXPIRE_QUOTE
		{
			get
			{
				return base.dtModel.Rows[0]["SYS_NUM_DAYS_EXPIRE_QUOTE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["SYS_NUM_DAYS_EXPIRE_QUOTE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SYS_NUM_DAYS_EXPIRE_QUOTE"] = value;
			}
		}
		// model for database field SYS_DEFAULT_POL_TERM(int)
		public int SYS_DEFAULT_POL_TERM
		{
			get
			{
				return base.dtModel.Rows[0]["SYS_DEFAULT_POL_TERM"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["SYS_DEFAULT_POL_TERM"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SYS_DEFAULT_POL_TERM"] = value;
			}
		}
		// model for database field SYS_INSTALLMENT_FEES(decimal)
		public decimal SYS_INSTALLMENT_FEES
		{
			get
			{
				return base.dtModel.Rows[0]["SYS_INSTALLMENT_FEES"] == DBNull.Value ? 0 : decimal.Parse(base.dtModel.Rows[0]["SYS_INSTALLMENT_FEES"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SYS_INSTALLMENT_FEES"] = value;
			}
		}
		// model for database field SYS_GRAPH_LOGO_ALLOW(string)
		public string SYS_GRAPH_LOGO_ALLOW
		{
			get
			{
				return base.dtModel.Rows[0]["SYS_GRAPH_LOGO_ALLOW"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SYS_GRAPH_LOGO_ALLOW"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SYS_GRAPH_LOGO_ALLOW"] = value;
			}
		}
		// model for database field SYS_NON_SUFFICIENT_FUND_FEES(decimal)
		public decimal SYS_NON_SUFFICIENT_FUND_FEES
		{
			get
			{
				return base.dtModel.Rows[0]["SYS_NON_SUFFICIENT_FUND_FEES"] == DBNull.Value ? 0 : decimal.Parse(base.dtModel.Rows[0]["SYS_NON_SUFFICIENT_FUND_FEES"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SYS_NON_SUFFICIENT_FUND_FEES"] = value;
			}
		}
		// model for database field SYS_REINSTATEMENT_FEES(decimal)
		public decimal SYS_REINSTATEMENT_FEES
		{
			get
			{
				return base.dtModel.Rows[0]["SYS_REINSTATEMENT_FEES"] == DBNull.Value ? 0 : decimal.Parse(base.dtModel.Rows[0]["SYS_REINSTATEMENT_FEES"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SYS_REINSTATEMENT_FEES"] = value;
			}
		}
		// model for database field SYS_EMPLOYEE_DISCOUNT(decimal)
		public decimal SYS_EMPLOYEE_DISCOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["SYS_EMPLOYEE_DISCOUNT"] == DBNull.Value ? 0 : decimal.Parse(base.dtModel.Rows[0]["SYS_EMPLOYEE_DISCOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SYS_EMPLOYEE_DISCOUNT"] = value;
			}
		}
		// model for database field SYS_PRINT_FOLLOWING(string)
		public string SYS_PRINT_FOLLOWING
		{
			get
			{
				return base.dtModel.Rows[0]["SYS_PRINT_FOLLOWING"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SYS_PRINT_FOLLOWING"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SYS_PRINT_FOLLOWING"] = value;
			}
		}
		// model for database field SYS_CLAIM_NO(int)
		public int SYS_CLAIM_NO
		{
			get
			{
				return base.dtModel.Rows[0]["SYS_CLAIM_NO"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["SYS_CLAIM_NO"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SYS_CLAIM_NO"] = value;
			}
		}
		// model for database field SYS_INSURANCE_SCORE_VALIDITY(string)
		public string SYS_INSURANCE_SCORE_VALIDITY
		{
			get
			{
				return base.dtModel.Rows[0]["SYS_INSURANCE_SCORE_VALIDITY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SYS_INSURANCE_SCORE_VALIDITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SYS_INSURANCE_SCORE_VALIDITY"] = value;
			}
		}
		// model for database field SYS_Min_Install_Plan(decimal)
		public decimal SYS_Min_Install_Plan
		{
			get
			{
				return base.dtModel.Rows[0]["SYS_Min_Install_Plan"] == DBNull.Value ? 0 : decimal.Parse(base.dtModel.Rows[0]["SYS_Min_Install_Plan"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SYS_Min_Install_Plan"] = value;
			}
		}
		// model for database field Reinstatement_Fees(decimal)
		public decimal SYS_AmtUnder_Payment
		{
			get
			{
				return base.dtModel.Rows[0]["SYS_AmtUnder_Payment"] == DBNull.Value ? 0 : decimal.Parse(base.dtModel.Rows[0]["SYS_AmtUnder_Payment"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SYS_AmtUnder_Payment"] = value;
			}
		}
		// model for database field SYS_MinDays_Premium(int)
		public int SYS_MinDays_Premium
		{
			get
			{
				return base.dtModel.Rows[0]["SYS_MinDays_Premium"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["SYS_MinDays_Premium"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SYS_MinDays_Premium"] = value;
			}
		}
		// model for database field SYS_Post_Cancel(int)
		public int SYS_Post_Cancel
		{
			get
			{
				return base.dtModel.Rows[0]["SYS_Post_Cancel"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["SYS_Post_Cancel"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SYS_Post_Cancel"] = value;
			}
		}
		// model for database field SYS_Post_Phone(int)
		public int SYS_Post_Phone
		{
			get
			{
				return base.dtModel.Rows[0]["SYS_Post_Phone"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["SYS_Post_Phone"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SYS_Post_Phone"] = value;
			}
		}
		// model for database field SYS_MinDays_Cancel(int)
		public int SYS_MinDays_Cancel
		{
			get
			{
				return base.dtModel.Rows[0]["SYS_MinDays_Cancel"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["SYS_MinDays_Cancel"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SYS_MinDays_Cancel"] = value;
			}
		}
		// model for database field SYS_INDICATE_POL_AS(string)
		public string SYS_INDICATE_POL_AS
		{
			get
			{
				return base.dtModel.Rows[0]["SYS_INDICATE_POL_AS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SYS_INDICATE_POL_AS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SYS_INDICATE_POL_AS"] = value;
			}
		}
		
		// model for database field POSTAGE_FEE(int)
		public int POSTAGE_FEE
		{
			get
			{
				return base.dtModel.Rows[0]["POSTAGE_FEE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["POSTAGE_FEE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POSTAGE_FEE"] = value;
			}
		}
		// model for database field RESTR_DELIV_FEE(int)
		public int RESTR_DELIV_FEE
		{
			get
			{
				return base.dtModel.Rows[0]["RESTR_DELIV_FEE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["RESTR_DELIV_FEE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RESTR_DELIV_FEE"] = value;
			}
		}
		// model for database field CERTIFIED_FEE(int)
		public int CERTIFIED_FEE
		{
			get
			{
				return base.dtModel.Rows[0]["CERTIFIED_FEE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CERTIFIED_FEE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CERTIFIED_FEE"] = value;
			}
		}
		// model for database field RET_RECEIPT_FEE(int)
		public int RET_RECEIPT_FEE
		{
			get
			{
				return base.dtModel.Rows[0]["RET_RECEIPT_FEE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["RET_RECEIPT_FEE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RET_RECEIPT_FEE"] = value;
			}
		}
        //Added by pradeep kushwaha
        public int NOTIFY_RECVE_INSURED
		{
			get
			{
                return base.dtModel.Rows[0]["NOTIFY_RECVE_INSURED"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["NOTIFY_RECVE_INSURED"].ToString());
			}
			set
			{
                base.dtModel.Rows[0]["NOTIFY_RECVE_INSURED"] = value;
			}
        }//public int NOTIFY_RECVE_INSURED
        public int BASE_CURRENCY
        {
            get
            {
                return base.dtModel.Rows[0]["BASE_CURRENCY"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["BASE_CURRENCY"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["BASE_CURRENCY"] = value;
            }
        }//Added by Abhinav Agarwal

        // model for database field DAYS_FOR_BOLETO_EXPIRATION
        public int DAYS_FOR_BOLETO_EXPIRATION
        {
            get
            {
                return base.dtModel.Rows[0]["DAYS_FOR_BOLETO_EXPIRATION"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DAYS_FOR_BOLETO_EXPIRATION"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["DAYS_FOR_BOLETO_EXPIRATION"] = value;
            }
        
        }
		#endregion
	}
}
