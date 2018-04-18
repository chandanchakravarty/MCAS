/******************************************************************************************
<Author					: - Vijay Joshi
<Start Date				: -	6/20/2005 2:36:17 PM
<End Date				: -	
<Description			: - Model class for deposit screen.
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
	/// Database Model for ACT_CURRENT_DEPOSITS.
	/// </summary>
	public class ClsDepositInfo : Cms.Model.ClsCommonModel
	{
		private const string ACT_CURRENT_DEPOSITS = "ACT_CURRENT_DEPOSITS";
		public ClsDepositInfo()
		{
			base.dtModel.TableName = "ACT_CURRENT_DEPOSITS";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table ACT_CURRENT_DEPOSITS
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("DEPOSIT_ID",typeof(int));
			base.dtModel.Columns.Add("GL_ID",typeof(int));
			base.dtModel.Columns.Add("FISCAL_ID",typeof(int));
			base.dtModel.Columns.Add("ACCOUNT_ID",typeof(int));
			base.dtModel.Columns.Add("DEPOSIT_NUMBER",typeof(int));
			base.dtModel.Columns.Add("DEPOSIT_TRAN_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("TOTAL_DEPOSIT_AMOUNT",typeof(double));
			base.dtModel.Columns.Add("DEPOSIT_NOTE",typeof(string));
			base.dtModel.Columns.Add("IS_BNK_RECONCILED",typeof(string));
			base.dtModel.Columns.Add("IN_BNK_RECON",typeof(int));
			base.dtModel.Columns.Add("IS_COMMITED",typeof(string));
			base.dtModel.Columns.Add("DATE_COMMITED",typeof(DateTime));
			base.dtModel.Columns.Add("DEPOSIT_TYPE",typeof(string));
			base.dtModel.Columns.Add("RECEIPT_MODE",typeof(int));
			base.dtModel.Columns.Add("RTL_FILE",typeof(string));
			base.dtModel.Columns.Add("ACCOUNT_BALANCE",typeof(double));
			
		}
		#region Database schema details
		
		// model for database field DEPOSIT_TYPEint)
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
		// model for database field DEPOSIT_NUMBER(int)
		public int DEPOSIT_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["DEPOSIT_NUMBER"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["DEPOSIT_NUMBER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DEPOSIT_NUMBER"] = value;
			}
		}
		// model for database field DEPOSIT_TRAN_DATE(DateTime)
		public DateTime DEPOSIT_TRAN_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["DEPOSIT_TRAN_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["DEPOSIT_TRAN_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DEPOSIT_TRAN_DATE"] = value;
			}
		}
		// model for database field TOTAL_DEPOSIT_AMOUNT(double)
		public double TOTAL_DEPOSIT_AMOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["TOTAL_DEPOSIT_AMOUNT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["TOTAL_DEPOSIT_AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["TOTAL_DEPOSIT_AMOUNT"] = value;
			}
		}
		// model for database field DEPOSIT_NOTE(string)
		public string DEPOSIT_NOTE
		{
			get
			{
				return base.dtModel.Rows[0]["DEPOSIT_NOTE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DEPOSIT_NOTE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DEPOSIT_NOTE"] = value;
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
		// model for database field IN_BNK_RECON(int)
		public int IN_BNK_RECON
		{
			get
			{
				return base.dtModel.Rows[0]["IN_BNK_RECON"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["IN_BNK_RECON"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["IN_BNK_RECON"] = value;
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
		// model for database field RECEIPT_MODE(int)
		public int RECEIPT_MODE
		{
			get
			{
				return base.dtModel.Rows[0]["RECEIPT_MODE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["RECEIPT_MODE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RECEIPT_MODE"] = value;
			}
		}
		// model for database field RTL_FILE(string)
		public string RTL_FILE
		{
			get
			{
				return base.dtModel.Rows[0]["RTL_FILE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["RTL_FILE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["RTL_FILE"] = value;
			}
		}
		// model for database field ACCOUNT_BALANCE(double)
		public double ACCOUNT_BALANCE
		{
			get
			{
				return base.dtModel.Rows[0]["ACCOUNT_BALANCE"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["ACCOUNT_BALANCE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ACCOUNT_BALANCE"] = value;
			}
		}		
		#endregion
	}
}
