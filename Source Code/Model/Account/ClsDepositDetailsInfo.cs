/******************************************************************************************
<Author					: - Vijay Joshi
<Start Date				: -	6/22/2005 7:11:00 PM
<End Date				: -	
<Description			: - Model class for deposit details
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
	/// Database Model for ACT_CURRENT_DEPOSIT_LINE_ITEMS.
	/// </summary>
	public class ClsDepositDetailsInfo : Cms.Model.ClsCommonModel
	{
		private const string ACT_CURRENT_DEPOSIT_LINE_ITEMS = "ACT_CURRENT_DEPOSIT_LINE_ITEMS";
		public ClsDepositDetailsInfo()
		{
			base.dtModel.TableName = "ACT_CURRENT_DEPOSIT_LINE_ITEMS";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table ACT_CURRENT_DEPOSIT_LINE_ITEMS
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CD_LINE_ITEM_ID",typeof(int));
			base.dtModel.Columns.Add("DEPOSIT_ID",typeof(int));
			base.dtModel.Columns.Add("LINE_ITEM_INTERNAL_NUMBER",typeof(int));
			base.dtModel.Columns.Add("DIV_ID",typeof(int));
			base.dtModel.Columns.Add("PC_ID",typeof(int));
			base.dtModel.Columns.Add("ACCOUNT_ID",typeof(int));
			base.dtModel.Columns.Add("DEPOSIT_TYPE",typeof(string));
			base.dtModel.Columns.Add("BANK_NAME",typeof(string));
			base.dtModel.Columns.Add("CHECK_NUM",typeof(string));
			base.dtModel.Columns.Add("RECEIPT_NUM",typeof(string));
			base.dtModel.Columns.Add("RECEIPT_AMOUNT",typeof(double));
			base.dtModel.Columns.Add("PAYOR_TYPE",typeof(string));
			base.dtModel.Columns.Add("RECEIPT_FROM_ID",typeof(int));
			base.dtModel.Columns.Add("RECEIPT_FROM_CODE",typeof(string));
			base.dtModel.Columns.Add("RECEIPT_FROM_NAME",typeof(string));
			base.dtModel.Columns.Add("BILL_TYPE",typeof(string));
			base.dtModel.Columns.Add("LINE_ITEM_DESCRIPTION",typeof(string));
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("IN_RECON",typeof(string));
			base.dtModel.Columns.Add("AVAILABLE_BALANCE",typeof(double));
			base.dtModel.Columns.Add("REF_CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("IS_BNK_RECONCILED",typeof(string));
			base.dtModel.Columns.Add("POLICY_NO",typeof(string));
			base.dtModel.Columns.Add("CLAIM_NUMBER",typeof(string));
			base.dtModel.Columns.Add("POLICY_MONTH",typeof(int));
			base.dtModel.Columns.Add("MONTH_YEAR",typeof(int));
			base.dtModel.Columns.Add("CREATED_FROM",typeof(string));
			base.dtModel.Columns.Add("PAGE_ID",typeof(string));
			base.dtModel.Columns.Add("RTL_BATCH_NUMBER",typeof(string));
			base.dtModel.Columns.Add("RTL_GROUP_NUMBER",typeof(string));


		}
		#region Database schema details
		
		//model for database field policy month(int)
		public int MONTH_YEAR
		{
			get
			{
				return base.dtModel.Rows[0]["MONTH_YEAR"] == DBNull.Value ? 0 : Convert.ToInt32(base.dtModel.Rows[0]["MONTH_YEAR"]);
			}
			set
			{
				base.dtModel.Rows[0]["MONTH_YEAR"] = value;
			}
		}


		//model for database field policy month(int)
		public int POLICY_MONTH
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_MONTH"] == DBNull.Value ? 0 : Convert.ToInt32(base.dtModel.Rows[0]["POLICY_MONTH"]);
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_MONTH"] = value;
			}
		}

		//model for database field claim Number(string)
		public string CLAIM_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["CLAIM_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CLAIM_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CLAIM_NUMBER"] = value;
			}
		}

		//model for database field Policy Number(string)
		public string POLICY_NO
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_NO"] == DBNull.Value ? "" : base.dtModel.Rows[0]["POLICY_NO"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_NO"] = value;
			}
		}

		// model for database field CD_LINE_ITEM_ID(int)
		public int CD_LINE_ITEM_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CD_LINE_ITEM_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CD_LINE_ITEM_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CD_LINE_ITEM_ID"] = value;
			}
		}
		// model for database field DEPOSIT_ID(int)
		public int DEPOSIT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["DEPOSIT_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["DEPOSIT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DEPOSIT_ID"] = value;
			}
		}
		// model for database field LINE_ITEM_INTERNAL_NUMBER(int)
		public int LINE_ITEM_INTERNAL_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["LINE_ITEM_INTERNAL_NUMBER"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LINE_ITEM_INTERNAL_NUMBER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LINE_ITEM_INTERNAL_NUMBER"] = value;
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
		// model for database field DEPOSIT_TYPE(string)
		public string DEPOSIT_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["DEPOSIT_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DEPOSIT_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DEPOSIT_TYPE"] = value;
			}
		}
		// model for database field BANK_NAME(string)
		public string BANK_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["BANK_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["BANK_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BANK_NAME"] = value;
			}
		}
		// model for database field CHECK_NUM(string)
		public string CHECK_NUM
		{
			get
			{
				return base.dtModel.Rows[0]["CHECK_NUM"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CHECK_NUM"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CHECK_NUM"] = value;
			}
		}
		// model for database field RECEIPT_NUM(string)
		public string RECEIPT_NUM
		{
			get
			{
				return base.dtModel.Rows[0]["RECEIPT_NUM"] == DBNull.Value ? "" : base.dtModel.Rows[0]["RECEIPT_NUM"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["RECEIPT_NUM"] = value;
			}
		}
		// model for database field RECEIPT_AMOUNT(double)
		public double RECEIPT_AMOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["RECEIPT_AMOUNT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["RECEIPT_AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RECEIPT_AMOUNT"] = value;
			}
		}
		// model for database field PAYOR_TYPE(string)
		public string PAYOR_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["PAYOR_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PAYOR_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PAYOR_TYPE"] = value;
			}
		}
		// model for database field RECEIPT_FROM_ID(int)
		public int RECEIPT_FROM_ID
		{
			get
			{
				return base.dtModel.Rows[0]["RECEIPT_FROM_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["RECEIPT_FROM_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RECEIPT_FROM_ID"] = value;
			}
		}
		// model for database field RECEIPT_FROM_CODE(string)
		public string RECEIPT_FROM_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["RECEIPT_FROM_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["RECEIPT_FROM_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["RECEIPT_FROM_CODE"] = value;
			}
		}
		// model for database field RECEIPT_FROM_NAME(string)
		public string RECEIPT_FROM_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["RECEIPT_FROM_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["RECEIPT_FROM_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["RECEIPT_FROM_NAME"] = value;
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
		// model for database field LINE_ITEM_DESCRIPTION(string)
		public string LINE_ITEM_DESCRIPTION
		{
			get
			{
				return base.dtModel.Rows[0]["LINE_ITEM_DESCRIPTION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LINE_ITEM_DESCRIPTION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LINE_ITEM_DESCRIPTION"] = value;
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
		// model for database field IN_RECON(string)
		public string IN_RECON
		{
			get
			{
				return base.dtModel.Rows[0]["IN_RECON"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IN_RECON"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IN_RECON"] = value;
			}
		}
		// model for database field AVAILABLE_BALANCE(double)
		public double AVAILABLE_BALANCE
		{
			get
			{
				return base.dtModel.Rows[0]["AVAILABLE_BALANCE"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["AVAILABLE_BALANCE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AVAILABLE_BALANCE"] = value;
			}
		}
		// model for database field REF_CUSTOMER_ID(int)
		public int REF_CUSTOMER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["REF_CUSTOMER_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["REF_CUSTOMER_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REF_CUSTOMER_ID"] = value;
			}
		}
		// model for database field IS_BNK_RECONCILED(string)
		public string IS_BNK_RECONCILED
		{
			get
			{
				return base.dtModel.Rows[0]["IS_BNK_RECONCILED"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IS_BNK_RECONCILED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_BNK_RECONCILED"] = value;
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
		// model for database field CREATED_FROM(string)
		public string CREATED_FROM
		{
			get
			{
				return base.dtModel.Rows[0]["CREATED_FROM"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CREATED_FROM"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CREATED_FROM"] = value;
			}
		}
		//raghav
		// model for database field RTL_BATCH_NUMBER(string)
		public string RTL_BATCH_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["RTL_BATCH_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["RTL_BATCH_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["RTL_BATCH_NUMBER"] = value;
			}
		}
		// model for database field RTL_GROUP_NUMBER(string)
		public string RTL_GROUP_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["RTL_GROUP_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["RTL_GROUP_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["RTL_GROUP_NUMBER"] = value;
			}
		}

		// model for database field PAGE_ID(string)
		public string PAGE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["PAGE_ID"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PAGE_ID"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PAGE_ID"] = value;
			}
		}
		#endregion
	}
}
