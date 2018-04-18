/******************************************************************************************
<Author				: -   Vijay Arora
<Start Date			: -	5/24/2006 3:40:21 PM
<End Date			: -	
<Description		: - 	Model Class for Claims Activity
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
namespace Cms.Model.Claims
{
	/// <summary>
	/// Database Model for CLM_ACTIVITY.
	/// </summary>
	public class ClsActivityInfo : Cms.Model.ClsCommonModel
	{
		private const string CLM_ACTIVITY = "CLM_ACTIVITY";
		public ClsActivityInfo()
		{
			base.dtModel.TableName = "CLM_ACTIVITY";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table CLM_ACTIVITY
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CLAIM_ID",typeof(int));
			base.dtModel.Columns.Add("ACTIVITY_ID",typeof(int));
			base.dtModel.Columns.Add("ACTIVITY_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("ACTIVITY_REASON",typeof(int));
			base.dtModel.Columns.Add("ACTION_ON_PAYMENT",typeof(int));
			base.dtModel.Columns.Add("REASON_DESCRIPTION",typeof(string));
			base.dtModel.Columns.Add("RESERVE_AMOUNT",typeof(double));			
			base.dtModel.Columns.Add("PAYMENT_AMOUNT",typeof(double));			
			base.dtModel.Columns.Add("PAYEE_PARTIES_ID",typeof(string));		
			base.dtModel.Columns.Add("RECOVERY",typeof(double));			
			base.dtModel.Columns.Add("EXPENSES",typeof(double));
			base.dtModel.Columns.Add("ACTIVITY_STATUS",typeof(int));	
			base.dtModel.Columns.Add("RESERVE_TRAN_CODE",typeof(int));
			base.dtModel.Columns.Add("RI_RESERVE",typeof(double));
			base.dtModel.Columns.Add("LOB_ID",typeof(int));
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("GL_POSTING_REQUIRED",typeof(string));
			base.dtModel.Columns.Add("ACCOUNTING_SUPPRESSED",typeof(string));
            base.dtModel.Columns.Add("COI_TRAN_TYPE", typeof(int));
            base.dtModel.Columns.Add("TEXT_ID", typeof(int));
            base.dtModel.Columns.Add("TEXT_DESCRIPTION", typeof(string));
		}
		#region Database schema details
		// model for database field CLAIM_ID(int)
		public int CLAIM_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CLAIM_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CLAIM_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CLAIM_ID"] = value;
			}
		}
		// model for database field ACTIVITY_ID(int)
		public int ACTIVITY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ACTIVITY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ACTIVITY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ACTIVITY_ID"] = value;
			}
		}
		// model for database field ACTIVITY_DATE(DateTime)
		public DateTime ACTIVITY_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["ACTIVITY_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["ACTIVITY_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ACTIVITY_DATE"] = value;
			}
		}
		// model for database field ACTIVITY_REASON(int)
		public int ACTIVITY_REASON
		{
			get
			{
				return base.dtModel.Rows[0]["ACTIVITY_REASON"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ACTIVITY_REASON"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ACTIVITY_REASON"] = value;
			}
		}
		// model for database field REASON_DESCRIPTION(string)
		public string REASON_DESCRIPTION
		{
			get
			{
				return base.dtModel.Rows[0]["REASON_DESCRIPTION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REASON_DESCRIPTION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REASON_DESCRIPTION"] = value;
			}
		}
		// model for database field RESERVE_AMOUNT(double)
		public double RESERVE_AMOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["RESERVE_AMOUNT"] == DBNull.Value ? Convert.ToDouble(null) : double.Parse(base.dtModel.Rows[0]["RESERVE_AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RESERVE_AMOUNT"] = value;
			}
		}
		// model for database field PAYMENT_AMOUNT(double)
		public double PAYMENT_AMOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["PAYMENT_AMOUNT"] == DBNull.Value ? Convert.ToDouble(null) : double.Parse(base.dtModel.Rows[0]["PAYMENT_AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PAYMENT_AMOUNT"] = value;
			}
		}
		// model for database field PAYEE_PARTIES_ID(string)
		public string PAYEE_PARTIES_ID
		{
			get
			{
				return base.dtModel.Rows[0]["PAYEE_PARTIES_ID"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PAYEE_PARTIES_ID"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PAYEE_PARTIES_ID"] = value;
			}
		}		
		// model for database field RECOVERY(double)
		public double RECOVERY
		{
			get
			{
				return base.dtModel.Rows[0]["RECOVERY"] == DBNull.Value ? Convert.ToDouble(null) : double.Parse(base.dtModel.Rows[0]["RECOVERY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RECOVERY"] = value;
			}
		}
		// model for database field EXPENSES(double)
		public double EXPENSES
		{
			get
			{
				return base.dtModel.Rows[0]["EXPENSES"] == DBNull.Value ? Convert.ToDouble(null) : double.Parse(base.dtModel.Rows[0]["EXPENSES"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EXPENSES"] = value;
			}
		}
		// model for database field ACTIVITY_STATUS(int)
		public int ACTIVITY_STATUS
		{
			get
			{
				return base.dtModel.Rows[0]["ACTIVITY_STATUS"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ACTIVITY_STATUS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ACTIVITY_STATUS"] = value;
			}
		}
		// model for database field RESERVE_TRAN_CODE(int)
		public int RESERVE_TRAN_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["RESERVE_TRAN_CODE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["RESERVE_TRAN_CODE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RESERVE_TRAN_CODE"] = value;
			}
		}
		// model for database field RI_RESERVE(double)
		public double RI_RESERVE
		{
			get
			{
				return base.dtModel.Rows[0]["RI_RESERVE"] == DBNull.Value ? Convert.ToDouble(null) : double.Parse(base.dtModel.Rows[0]["RI_RESERVE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RI_RESERVE"] = value;
			}
		}		
		// model for database field ACTION_ON_PAYMENT(int)
		public int ACTION_ON_PAYMENT
		{
			get
			{
				return base.dtModel.Rows[0]["ACTION_ON_PAYMENT"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ACTION_ON_PAYMENT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ACTION_ON_PAYMENT"] = value;
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
		// model for database field GL_POSTING_REQUIRED(string)
		public string GL_POSTING_REQUIRED
		{
			get
			{
				return base.dtModel.Rows[0]["GL_POSTING_REQUIRED"] == DBNull.Value ? "" : base.dtModel.Rows[0]["GL_POSTING_REQUIRED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["GL_POSTING_REQUIRED"] = value;
			}
		}
		public byte ACCOUNTING_SUPPRESSED
		{
			get
			{
				return base.dtModel.Rows[0]["ACCOUNTING_SUPPRESSED"] == DBNull.Value ? Convert.ToByte(null) : Byte.Parse(base.dtModel.Rows[0]["ACCOUNTING_SUPPRESSED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ACCOUNTING_SUPPRESSED"] = value;
			}
		}

        public int COI_TRAN_TYPE
        {
            get
            {
                return base.dtModel.Rows[0]["COI_TRAN_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["COI_TRAN_TYPE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["COI_TRAN_TYPE"] = value;
            }
        }

        public int TEXT_ID
        {
            get
            {
                return base.dtModel.Rows[0]["TEXT_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["TEXT_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["TEXT_ID"] = value;
            }
        }

        


        public string TEXT_DESCRIPTION
        {
            get
            {
                return base.dtModel.Rows[0]["TEXT_DESCRIPTION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["TEXT_DESCRIPTION"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["TEXT_DESCRIPTION"] = value;
            }
        }
		#endregion
	}
}
