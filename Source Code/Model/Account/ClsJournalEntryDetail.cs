/******************************************************************************************
<Author				: -   Vijay Joshi
<Start Date				: -	6/9/2005 12:37:34 PM
<End Date				: -	
<Description				: - 	Model class for Journal Entry detail screen.
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
	/// Database Model for ACT_JOURNAL_LINE_ITEMS.
	/// </summary>
	public class ClsJournalEntryDetailInfo : Cms.Model.ClsCommonModel
	{
		private const string ACT_JOURNAL_LINE_ITEMS = "ACT_JOURNAL_LINE_ITEMS";
		public ClsJournalEntryDetailInfo()
		{
			base.dtModel.TableName = "ACT_JOURNAL_LINE_ITEMS";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table ACT_JOURNAL_LINE_ITEMS
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("JE_LINE_ITEM_ID",typeof(int));
			base.dtModel.Columns.Add("JOURNAL_ID",typeof(int));
			base.dtModel.Columns.Add("DIV_ID",typeof(int));
			base.dtModel.Columns.Add("DEPT_ID",typeof(int));
			base.dtModel.Columns.Add("PC_ID",typeof(int));
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("AMOUNT",typeof(double));
			base.dtModel.Columns.Add("TYPE",typeof(string));
			base.dtModel.Columns.Add("REGARDING",typeof(string));
			base.dtModel.Columns.Add("REF_CUSTOMER",typeof(int));
			base.dtModel.Columns.Add("ACCOUNT_ID",typeof(int));
			base.dtModel.Columns.Add("BILL_TYPE",typeof(string));
			base.dtModel.Columns.Add("NOTE",typeof(string));
			base.dtModel.Columns.Add("POLICY_NUMBER",typeof(string));
			base.dtModel.Columns.Add("TRAN_CODE",typeof(string));
		}
		#region Database schema details
		// model for database field JE_LINE_ITEM_ID(int)
		public int JE_LINE_ITEM_ID
		{
			get
			{
				return base.dtModel.Rows[0]["JE_LINE_ITEM_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["JE_LINE_ITEM_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["JE_LINE_ITEM_ID"] = value;
			}
		}
		// model for database field JOURNAL_ID(int)
		public int JOURNAL_ID
		{
			get
			{
				return base.dtModel.Rows[0]["JOURNAL_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["JOURNAL_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["JOURNAL_ID"] = value;
			}
		}
		// model for database field DIV_ID(int)
		public int DIV_ID
		{
			get
			{
				return base.dtModel.Rows[0]["DIV_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DIV_ID"].ToString());
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
				return base.dtModel.Rows[0]["DEPT_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DEPT_ID"].ToString());
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
				return base.dtModel.Rows[0]["PC_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["PC_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PC_ID"] = value;
			}
		}
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
		// model for database field POLICY_ID(int)
		public int POLICY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["POLICY_ID"].ToString());
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
				return base.dtModel.Rows[0]["POLICY_VERSION_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["POLICY_VERSION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_VERSION_ID"] = value;
			}
		}
		// model for database field AMOUNT(double)
		public double AMOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["AMOUNT"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AMOUNT"] = value;
			}
		}
		// model for database field TYPE(string)
		public string TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TYPE"] = value;
			}
		}
		// model for database field REGARDING(int)
		public string REGARDING
		{
			get
			{
				return base.dtModel.Rows[0]["REGARDING"] == DBNull.Value ? Convert.ToString(null) : (base.dtModel.Rows[0]["REGARDING"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REGARDING"] = value;
			}
		}
		// model for database field REF_CUSTOMER(int)
		public int REF_CUSTOMER
		{
			get
			{
				return base.dtModel.Rows[0]["REF_CUSTOMER"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["REF_CUSTOMER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REF_CUSTOMER"] = value;
			}
		}
		// model for database field ACCOUNT_ID(int)
		public int ACCOUNT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ACCOUNT_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["ACCOUNT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ACCOUNT_ID"] = value;
			}
		}
		// model for database field BILL_TYPE(string)
		public string BILL_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["BILL_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["BILL_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BILL_TYPE"] = value;
			}
		}
		// model for database field NOTE(string)
		public string NOTE
		{
			get
			{
				return base.dtModel.Rows[0]["NOTE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["NOTE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["NOTE"] = value;
			}
		}
		// model for database field POLICY_NUMBER(string)
		public string POLICY_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_NUMBER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["POLICY_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_NUMBER"] = value;
			}
		}
		// model for database field TRAN_CODE(int)
		public string TRAN_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["TRAN_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["TRAN_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TRAN_CODE"] = value;
			}
		}
		#endregion
	}
}
