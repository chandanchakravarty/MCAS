/******************************************************************************************
<Author				: -   Ajit Singh Chahal
<Start Date				: -	6/30/2005 12:37:50 PM
<End Date				: -	
<Description				: - 	Model for check information.
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
namespace Cms.Model.Account
{
	/// <summary>
	/// Database Model for ACT_CHECK_INFORMATION.
	/// </summary>
	public class ClsChecksInfo : Cms.Model.ClsCommonModel
	{
		private const string ACT_CHECK_INFORMATION = "ACT_CHECK_INFORMATION";
		public ClsChecksInfo()
		{
			base.dtModel.TableName = "ACT_CHECK_INFORMATION";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table ACT_CHECK_INFORMATION
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CHECK_ID",typeof(int));
			base.dtModel.Columns.Add("CHECK_TYPE",typeof(string));
			base.dtModel.Columns.Add("SELECT_FROM",typeof(string));
			base.dtModel.Columns.Add("ACCOUNT_ID",typeof(int));
			base.dtModel.Columns.Add("MANUAL_CHECK",typeof(string));
			base.dtModel.Columns.Add("CHECK_NUMBER",typeof(string));
			base.dtModel.Columns.Add("CHECK_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("CHECK_AMOUNT",typeof(double));
			base.dtModel.Columns.Add("CHECK_NOTE",typeof(string));
			base.dtModel.Columns.Add("PAYEE_ENTITY_ID",typeof(int));
			base.dtModel.Columns.Add("PAYEE_ENTITY_TYPE",typeof(string));
			base.dtModel.Columns.Add("PAYEE_ENTITY_NAME",typeof(string));
			base.dtModel.Columns.Add("PAYEE_ADD1",typeof(string));
			base.dtModel.Columns.Add("PAYEE_ADD2",typeof(string));
			base.dtModel.Columns.Add("PAYEE_CITY",typeof(string));
			base.dtModel.Columns.Add("PAYEE_STATE",typeof(string));
			base.dtModel.Columns.Add("PAYEE_ZIP",typeof(string));
			base.dtModel.Columns.Add("PAYEE_NOTE",typeof(string));
			base.dtModel.Columns.Add("CREATED_IN",typeof(string));
			base.dtModel.Columns.Add("DIV_ID",typeof(int));
			base.dtModel.Columns.Add("DEPT_ID",typeof(int));
			base.dtModel.Columns.Add("PC_ID",typeof(int));
			base.dtModel.Columns.Add("IS_COMMITED",typeof(string));
			base.dtModel.Columns.Add("DATE_COMMITTED",typeof(DateTime));
			base.dtModel.Columns.Add("COMMITED_BY",typeof(int));
			base.dtModel.Columns.Add("IN_RECON",typeof(string));
			base.dtModel.Columns.Add("AVAILABLE_BALANCE",typeof(double));
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VER_TRACKING_ID",typeof(int));
			base.dtModel.Columns.Add("GL_UPDATE",typeof(string));
			base.dtModel.Columns.Add("IS_BNK_RECONCILED",typeof(string));
			base.dtModel.Columns.Add("CHECKSIGN_1",typeof(string));
			base.dtModel.Columns.Add("CHECKSIGN_2",typeof(string));
			base.dtModel.Columns.Add("CHECK_MEMO",typeof(string));
			base.dtModel.Columns.Add("IS_BNK_RECONCILED_VOID",typeof(string));
			base.dtModel.Columns.Add("IN_BNK_RECON",typeof(int));
			base.dtModel.Columns.Add("SPOOL_STATUS",typeof(int));
			base.dtModel.Columns.Add("TRAN_TYPE",typeof(int));
			base.dtModel.Columns.Add("IS_DISPLAY_ON_STUB",typeof(string));
			base.dtModel.Columns.Add("OPEN_ITEM_ROW_ID",typeof(int));
			base.dtModel.Columns.Add("MONTH",typeof(int)); //Month
			base.dtModel.Columns.Add("YEAR",typeof(int)); //Year
			base.dtModel.Columns.Add("IS_RECONCILED",typeof(string)); //IS_Reconciled
			base.dtModel.Columns.Add("COMM_TYPE",typeof(string)); //IS_Reconciled
			base.dtModel.Columns.Add("TMP_CHECK_ID",typeof(int));
			base.dtModel.Columns.Add("PAYMENT_MODE",typeof(int));
			base.dtModel.Columns.Add("AGENCY_ID",typeof(int));
			base.dtModel.Columns.Add("OPEN_ITEM_ROW_IDS",typeof(string)); //IS_Reconciled

		}
		#region Database schema details

		public int AGENCY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["AGENCY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["AGENCY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AGENCY_ID"] = value;
			}
		}

		// model for database field CHECK_ID(int)
		public int OPEN_ITEM_ROW_ID
		{
			get
			{
				return base.dtModel.Rows[0]["OPEN_ITEM_ROW_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["OPEN_ITEM_ROW_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["OPEN_ITEM_ROW_ID"] = value;
			}
		}

		public string OPEN_ITEM_ROW_IDS
		{
			get
			{
				return base.dtModel.Rows[0]["OPEN_ITEM_ROW_IDS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["OPEN_ITEM_ROW_IDS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["OPEN_ITEM_ROW_IDS"] = value;
			}
		}
		// model for database field CHECK_ID(int)
		public int CHECK_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CHECK_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CHECK_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CHECK_ID"] = value;
			}
		}

		// model for database field TMP_CHECK_ID(int)
		public int TMP_CHECK_ID
		{
			get
			{
				return base.dtModel.Rows[0]["TMP_CHECK_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["TMP_CHECK_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["TMP_CHECK_ID"] = value;
			}
		}

		//model for PAYMENT_MODE
		public int PAYMENT_MODE
		{
			get
			{
				return base.dtModel.Rows[0]["PAYMENT_MODE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PAYMENT_MODE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PAYMENT_MODE"] = value;
			}
		}


		// model for database field CHECK_TYPE(string)
		public string CHECK_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["CHECK_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CHECK_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CHECK_TYPE"] = value;
			}
		}
		// model for database field SELECT_FROM(string)
		public string SELECT_FROM
		{
			get
			{
				return base.dtModel.Rows[0]["SELECT_FROM"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SELECT_FROM"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SELECT_FROM"] = value;
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
		// model for database field MANUAL_CHECK(string)
		public string MANUAL_CHECK
		{
			get
			{
				return base.dtModel.Rows[0]["MANUAL_CHECK"] == DBNull.Value ? "" : base.dtModel.Rows[0]["MANUAL_CHECK"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MANUAL_CHECK"] = value;
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
		// model for database field CHECK_AMOUNT(double)
		public double CHECK_AMOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["CHECK_AMOUNT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["CHECK_AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CHECK_AMOUNT"] = value;
			}
		}
		// model for database field CHECK_NOTE(string)
		public string CHECK_NOTE
		{
			get
			{
				return base.dtModel.Rows[0]["CHECK_NOTE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CHECK_NOTE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CHECK_NOTE"] = value;
			}
		}
		// model for database field PAYEE_ENTITY_ID(int)
		public int PAYEE_ENTITY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["PAYEE_ENTITY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PAYEE_ENTITY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PAYEE_ENTITY_ID"] = value;
			}
		}
		// model for database field PAYEE_ENTITY_TYPE(string)
		public string PAYEE_ENTITY_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["PAYEE_ENTITY_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PAYEE_ENTITY_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PAYEE_ENTITY_TYPE"] = value;
			}
		}
		// model for database field PAYEE_ENTITY_NAME(string)
		public string PAYEE_ENTITY_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["PAYEE_ENTITY_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PAYEE_ENTITY_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PAYEE_ENTITY_NAME"] = value;
			}
		}
		// model for database field PAYEE_ADD1(string)
		public string PAYEE_ADD1
		{
			get
			{
				return base.dtModel.Rows[0]["PAYEE_ADD1"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PAYEE_ADD1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PAYEE_ADD1"] = value;
			}
		}
		// model for database field PAYEE_ADD2(string)
		public string PAYEE_ADD2
		{
			get
			{
				return base.dtModel.Rows[0]["PAYEE_ADD2"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PAYEE_ADD2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PAYEE_ADD2"] = value;
			}
		}
		// model for database field PAYEE_CITY(string)
		public string PAYEE_CITY
		{
			get
			{
				return base.dtModel.Rows[0]["PAYEE_CITY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PAYEE_CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PAYEE_CITY"] = value;
			}
		}
		// model for database field PAYEE_STATE(string)
		public string PAYEE_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["PAYEE_STATE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PAYEE_STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PAYEE_STATE"] = value;
			}
		}
		// model for database field PAYEE_ZIP(string)
		public string PAYEE_ZIP
		{
			get
			{
				return base.dtModel.Rows[0]["PAYEE_ZIP"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PAYEE_ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PAYEE_ZIP"] = value;
			}
		}
		// model for database field PAYEE_NOTE(string)
		public string PAYEE_NOTE
		{
			get
			{
				return base.dtModel.Rows[0]["PAYEE_NOTE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PAYEE_NOTE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PAYEE_NOTE"] = value;
			}
		}
		// model for database field CREATED_IN(string)
		public string CREATED_IN
		{
			get
			{
				return base.dtModel.Rows[0]["CREATED_IN"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CREATED_IN"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CREATED_IN"] = value;
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
		// model for database field DATE_COMMITTED(DateTime)
		public DateTime DATE_COMMITTED
		{
			get
			{
				return base.dtModel.Rows[0]["DATE_COMMITTED"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["DATE_COMMITTED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DATE_COMMITTED"] = value;
			}
		}
		// model for database field COMMITED_BY(int)
		public int COMMITED_BY
		{
			get
			{
				return base.dtModel.Rows[0]["COMMITED_BY"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["COMMITED_BY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COMMITED_BY"] = value;
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
		// model for database field POLICY_VER_TRACKING_ID(int)
		public int POLICY_VER_TRACKING_ID
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_VER_TRACKING_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["POLICY_VER_TRACKING_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_VER_TRACKING_ID"] = value;
			}
		}
		// model for database field GL_UPDATE(string)
		public string GL_UPDATE
		{
			get
			{
				return base.dtModel.Rows[0]["GL_UPDATE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["GL_UPDATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["GL_UPDATE"] = value;
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
		// model for database field CHECKSIGN_1(STRING)
		public string CHECKSIGN_1
		{
			get
			{
				return base.dtModel.Rows[0]["CHECKSIGN_1"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CHECKSIGN_1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CHECKSIGN_1"] = value;
			}
		}
		// model for database field CHECKSIGN_2(string)
		public string CHECKSIGN_2
		{
			get
			{
				return base.dtModel.Rows[0]["CHECKSIGN_2"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CHECKSIGN_2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CHECKSIGN_2"] = value;
			}
		}
		// model for database field CHECK_MEMO(string)
		public string CHECK_MEMO
		{
			get
			{
				return base.dtModel.Rows[0]["CHECK_MEMO"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CHECK_MEMO"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CHECK_MEMO"] = value;
			}
		}
		// model for database field IS_BNK_RECONCILED_VOID(string)
		public string IS_BNK_RECONCILED_VOID
		{
			get
			{
				return base.dtModel.Rows[0]["IS_BNK_RECONCILED_VOID"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IS_BNK_RECONCILED_VOID"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_BNK_RECONCILED_VOID"] = value;
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
		// model for database field SPOOL_STATUS(int)
		public int SPOOL_STATUS
		{
			get
			{
				return base.dtModel.Rows[0]["SPOOL_STATUS"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["SPOOL_STATUS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SPOOL_STATUS"] = value;
			}
		}
		// model for database field TRAN_TYPE(int)
		public int TRAN_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["TRAN_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["TRAN_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["TRAN_TYPE"] = value;
			}
		}
		// model for database field IS_DISPLAY_ON_STUB(string)
		public string IS_DISPLAY_ON_STUB
		{
			get
			{
				return base.dtModel.Rows[0]["IS_DISPLAY_ON_STUB"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IS_DISPLAY_ON_STUB"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_DISPLAY_ON_STUB"] = value;
			}
		}
		// model for database field MONTH(int)
		public int MONTH
		{
			get
			{
				return base.dtModel.Rows[0]["MONTH"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["MONTH"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MONTH"] = value;
			}
		}
		// model for database field YEAR(int)
		public int YEAR
		{
			get
			{
				return base.dtModel.Rows[0]["YEAR"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["YEAR"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["YEAR"] = value;
			}
		}
		// model for database field IS_RECONCILED(string)
		public string IS_RECONCILED
		{
			get
			{
				return base.dtModel.Rows[0]["IS_RECONCILED"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IS_RECONCILED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_RECONCILED"] = value;
			}
		}
		// model for database field COMM_TYPE(string)
		public string COMM_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["COMM_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["COMM_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["COMM_TYPE"] = value;
			}
		}

		#endregion
	}
}
