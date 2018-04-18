/******************************************************************************************
<Author				: -   Vijay Arora
<Start Date				: -	6/1/2006 11:49:40 AM
<End Date				: -	
<Description				: - 	Model Class Claims Payee Details
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

namespace Cms.Model.Claims
{
	/// <summary>
	/// Database Model for CLM_PAYEE.
	/// </summary>
	public class ClsPayeeInfo : Cms.Model.ClsCommonModel
	{
		private const string CLM_PAYEE = "CLM_PAYEE";
		public ClsPayeeInfo()
		{
			base.dtModel.TableName = "CLM_PAYEE";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table CLM_PAYEE
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CLAIM_ID",typeof(int));
			base.dtModel.Columns.Add("ACTIVITY_ID",typeof(int));
			base.dtModel.Columns.Add("EXPENSE_ID",typeof(int));
			base.dtModel.Columns.Add("PAYEE_ID",typeof(int));
			//base.dtModel.Columns.Add("PAYEE_ACTIVITY_ID",typeof(int));
			base.dtModel.Columns.Add("PARTY_ID",typeof(string));
			base.dtModel.Columns.Add("PAYMENT_METHOD",typeof(int));
			base.dtModel.Columns.Add("ADDRESS1",typeof(string));
			base.dtModel.Columns.Add("ADDRESS2",typeof(string));
			base.dtModel.Columns.Add("CITY",typeof(string));
			base.dtModel.Columns.Add("STATE",typeof(int));
			base.dtModel.Columns.Add("ZIP",typeof(string));
			base.dtModel.Columns.Add("COUNTRY",typeof(int));
			base.dtModel.Columns.Add("NARRATIVE",typeof(string));
			base.dtModel.Columns.Add("FIRST_NAME",typeof(string));
			base.dtModel.Columns.Add("LAST_NAME",typeof(string));
			base.dtModel.Columns.Add("AMOUNT",typeof(double));	
			//base.dtModel.Columns.Add("INVOICED_BY",typeof(int));
			base.dtModel.Columns.Add("INVOICE_NUMBER",typeof(string));
			base.dtModel.Columns.Add("INVOICE_DATE",typeof(DateTime));
            base.dtModel.Columns.Add("INVOICE_DUE_DATE", typeof(DateTime));

            base.dtModel.Columns.Add("INVOICE_SERIAL_NUMBER", typeof(string));
            

			base.dtModel.Columns.Add("SERVICE_TYPE",typeof(int));
			base.dtModel.Columns.Add("SERVICE_DESCRIPTION",typeof(string));
			base.dtModel.Columns.Add("SECONDARY_PARTY_ID",typeof(string));
			base.dtModel.Columns.Add("PAYEE_PARTY_ID",typeof(string));
			base.dtModel.Columns.Add("TO_ORDER_DESC",typeof(string));
            base.dtModel.Columns.Add("REIN_RECOVERY_NUMBER", typeof(string));
            base.dtModel.Columns.Add("RECOVERY_TYPE", typeof(int));
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
		
		//Added PAYEE_PARTY_ID  For Itrack 5124
		public string PAYEE_PARTY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["PAYEE_PARTY_ID"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PAYEE_PARTY_ID"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PAYEE_PARTY_ID"] = value;
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
		// model for database field EXPENSE_ID(int)
		public int EXPENSE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["EXPENSE_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["EXPENSE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EXPENSE_ID"] = value;
			}
		}
		// model for database field PAYEE_ID(int)
		public int PAYEE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["PAYEE_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PAYEE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PAYEE_ID"] = value;
			}
		}
		// model for database field PAYEE_ACTIVITY_ID(int)
		/*public int PAYEE_ACTIVITY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["PAYEE_ACTIVITY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PAYEE_ACTIVITY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PAYEE_ACTIVITY_ID"] = value;
			}
		}*/
		// model for database field PARTY_ID(string)
		public string PARTY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["PARTY_ID"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PARTY_ID"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PARTY_ID"] = value;
			}
		}
		// model for database field PAYMENT_METHOD(int)
		public int PAYMENT_METHOD
		{
			get
			{
				return base.dtModel.Rows[0]["PAYMENT_METHOD"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PAYMENT_METHOD"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PAYMENT_METHOD"] = value;
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
		// model for database field STATE(int)
		public int STATE
		{
			get
			{
				return base.dtModel.Rows[0]["STATE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["STATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["STATE"] = value;
			}
		}
		// model for database field ZIP(string)
		public string ZIP
		{
			get
			{
				return base.dtModel.Rows[0]["ZIP"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ZIP"] = value;
			}
		}
		// model for database field COUNTRY(int)
		public int COUNTRY
		{
			get
			{
				return base.dtModel.Rows[0]["COUNTRY"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["COUNTRY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COUNTRY"] = value;
			}
		}
		// model for database field NARRATIVE(string)
		public string NARRATIVE
		{
			get
			{
				return base.dtModel.Rows[0]["NARRATIVE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["NARRATIVE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["NARRATIVE"] = value;
			}
		}
		// model for database field TO_ORDER_DESC(string)
		public string TO_ORDER_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["TO_ORDER_DESC"] == DBNull.Value ? "" : base.dtModel.Rows[0]["TO_ORDER_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TO_ORDER_DESC"] = value;
			}
		}
		// model for database field AMOUNT(double)
		public double AMOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["AMOUNT"] == DBNull.Value ? Convert.ToDouble(null) : double.Parse(base.dtModel.Rows[0]["AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AMOUNT"] = value;
			}
		}

        
        public string INVOICE_SERIAL_NUMBER
        {
            get
            {
                return base.dtModel.Rows[0]["INVOICE_SERIAL_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["INVOICE_SERIAL_NUMBER"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["INVOICE_SERIAL_NUMBER"] = value;
            }
        }

		// model for database field INVOICE_NUMBER(string)
		public string INVOICE_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["INVOICE_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["INVOICE_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["INVOICE_NUMBER"] = value;
			}
		}
		// model for database field INVOICE_DATE(DateTime)
		public DateTime INVOICE_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["INVOICE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["INVOICE_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INVOICE_DATE"] = value;
			}
		}

        // model for database field INVOICE_DUE_DATE(DateTime)
        public DateTime INVOICE_DUE_DATE
        {
            get
            {
                return base.dtModel.Rows[0]["INVOICE_DUE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["INVOICE_DUE_DATE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["INVOICE_DUE_DATE"] = value;
            }
        }
		// model for database field SERVICE_TYPE(int)
		public int SERVICE_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["SERVICE_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["SERVICE_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SERVICE_TYPE"] = value;
			}
		}
		// model for database field SERVICE_DESCRIPTION(string)
		public string SERVICE_DESCRIPTION
		{
			get
			{
				return base.dtModel.Rows[0]["SERVICE_DESCRIPTION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SERVICE_DESCRIPTION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SERVICE_DESCRIPTION"] = value;
			}
		}		
		// model for database field SECONDARY_PARTY_ID(string)
		public string SECONDARY_PARTY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["SECONDARY_PARTY_ID"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SECONDARY_PARTY_ID"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SECONDARY_PARTY_ID"] = value;
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

        // model for database field REIN_RECOVERY_NUMBER(string)
        public string REIN_RECOVERY_NUMBER
        {
            get
            {
                return base.dtModel.Rows[0]["REIN_RECOVERY_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_RECOVERY_NUMBER"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["REIN_RECOVERY_NUMBER"] = value;
            }
        }


        // model for database field RECOVERY_TYPE(int)
        public int RECOVERY_TYPE
        {
            get
            {
                return base.dtModel.Rows[0]["RECOVERY_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["RECOVERY_TYPE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["RECOVERY_TYPE"] = value;
            }
        }
		#endregion
	}
}