/******************************************************************************************
<Author					: -   Ajit Singh Chahal
<Start Date				: -	  5/18/2005 2:24:46 PM
<End Date				: -	
<Description			: -   Model for GL Accounts.
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
	/// Database Model for ACT_GL_ACCOUNTS.
	/// </summary>
	public class ClsGlAccountsInfo : Cms.Model.ClsCommonModel
		{
			private const string ACT_GL_ACCOUNTS = "ACT_GL_ACCOUNTS";
		public ClsGlAccountsInfo()
		{
			base.dtModel.TableName = "ACT_GL_ACCOUNTS";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table ACT_GL_ACCOUNTS
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("GL_ID",typeof(int));
			base.dtModel.Columns.Add("FISCAL_ID",typeof(int));
			base.dtModel.Columns.Add("ACCOUNT_ID",typeof(int));
			base.dtModel.Columns.Add("CATEGORY_TYPE",typeof(int));
			base.dtModel.Columns.Add("GROUP_TYPE",typeof(int));
			base.dtModel.Columns.Add("ACC_TYPE_ID",typeof(int));
			base.dtModel.Columns.Add("ACC_PARENT_ID",typeof(int));
			base.dtModel.Columns.Add("ACC_NUMBER",typeof(double));
			base.dtModel.Columns.Add("ACC_LEVEL_TYPE",typeof(string));
			base.dtModel.Columns.Add("ACC_DESCRIPTION",typeof(string));
			base.dtModel.Columns.Add("ACC_TOTALS_LEVEL",typeof(string));
			base.dtModel.Columns.Add("ACC_JOURNAL_ENTRY",typeof(string));
			base.dtModel.Columns.Add("ACC_CASH_ACCOUNT",typeof(string));
			base.dtModel.Columns.Add("ACC_CASH_ACC_TYPE",typeof(string));
			base.dtModel.Columns.Add("ACC_CASH_DEF_TYPE",typeof(string));
			base.dtModel.Columns.Add("ACC_DISP_NUMBER",typeof(string));
			base.dtModel.Columns.Add("ACC_PRODUCE_CHECK",typeof(string));
			base.dtModel.Columns.Add("ACC_HAS_CHILDREN",typeof(string));
			base.dtModel.Columns.Add("ACC_NEST_LEVEL",typeof(int));
			base.dtModel.Columns.Add("ACC_CURRENT_BALANCE",typeof(double));
			base.dtModel.Columns.Add("ACC_RELATES_TO_TYPE",typeof(int));
			base.dtModel.Columns.Add("BUDGET_CATEGORY_ID",typeof(int));
			base.dtModel.Columns.Add("WOLVERINE_USER_ID",typeof(int));
		}
		#region Database schema details
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
		// model for database field CATEGORY_TYPE(int)
		public int CATEGORY_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["CATEGORY_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CATEGORY_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CATEGORY_TYPE"] = value;
			}
		}
		// model for database field GROUP_TYPE(int)
		public int GROUP_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["GROUP_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["GROUP_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["GROUP_TYPE"] = value;
			}
		}
		// model for database field ACC_TYPE_ID(int)
		public int ACC_TYPE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ACC_TYPE_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ACC_TYPE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ACC_TYPE_ID"] = value;
			}
		}
		// model for database field ACC_PARENT_ID(int)
		public int ACC_PARENT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ACC_PARENT_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ACC_PARENT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ACC_PARENT_ID"] = value;
			}
		}
		// model for database field ACC_NUMBER(double)
		public double ACC_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["ACC_NUMBER"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["ACC_NUMBER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ACC_NUMBER"] = value;
			}
		}
		// model for database field ACC_LEVEL_TYPE(string)
		public string ACC_LEVEL_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["ACC_LEVEL_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ACC_LEVEL_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ACC_LEVEL_TYPE"] = value;
			}
		}
		// model for database field ACC_DESCRIPTION(string)
		public string ACC_DESCRIPTION
		{
			get
			{
				return base.dtModel.Rows[0]["ACC_DESCRIPTION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ACC_DESCRIPTION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ACC_DESCRIPTION"] = value;
			}
		}
		// model for database field ACC_TOTALS_LEVEL(string)
		public string ACC_TOTALS_LEVEL
		{
			get
			{
				return base.dtModel.Rows[0]["ACC_TOTALS_LEVEL"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ACC_TOTALS_LEVEL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ACC_TOTALS_LEVEL"] = value;
			}
		}
		// model for database field ACC_JOURNAL_ENTRY(string)
		public string ACC_JOURNAL_ENTRY
		{
			get
			{
				return base.dtModel.Rows[0]["ACC_JOURNAL_ENTRY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ACC_JOURNAL_ENTRY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ACC_JOURNAL_ENTRY"] = value;
			}
		}
		// model for database field ACC_CASH_ACCOUNT(string)
		public string ACC_CASH_ACCOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["ACC_CASH_ACCOUNT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ACC_CASH_ACCOUNT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ACC_CASH_ACCOUNT"] = value;
			}
		}
		// model for database field ACC_CASH_ACC_TYPE(string)
		public string ACC_CASH_ACC_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["ACC_CASH_ACC_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ACC_CASH_ACC_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ACC_CASH_ACC_TYPE"] = value;
			}
		}
		// model for database field ACC_CASH_DEF_TYPE(string)
		public string ACC_CASH_DEF_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["ACC_CASH_DEF_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ACC_CASH_DEF_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ACC_CASH_DEF_TYPE"] = value;
			}
		}
		// model for database field ACC_DISP_NUMBER(string)
		public string ACC_DISP_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["ACC_DISP_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ACC_DISP_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ACC_DISP_NUMBER"] = value;
			}
		}
		// model for database field ACC_PRODUCE_CHECK(string)
		public string ACC_PRODUCE_CHECK
		{
			get
			{
				return base.dtModel.Rows[0]["ACC_PRODUCE_CHECK"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ACC_PRODUCE_CHECK"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ACC_PRODUCE_CHECK"] = value;
			}
		}
		// model for database field ACC_HAS_CHILDREN(string)
		public string ACC_HAS_CHILDREN
		{
			get
			{
				return base.dtModel.Rows[0]["ACC_HAS_CHILDREN"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ACC_HAS_CHILDREN"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ACC_HAS_CHILDREN"] = value;
			}
		}
		// model for database field ACC_NEST_LEVEL(int)
		public int ACC_NEST_LEVEL
		{
			get
			{
				return base.dtModel.Rows[0]["ACC_NEST_LEVEL"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ACC_NEST_LEVEL"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ACC_NEST_LEVEL"] = value;
			}
		}
		// model for database field ACC_CURRENT_BALANCE(double)
		public double ACC_CURRENT_BALANCE
		{
			get
			{
				return base.dtModel.Rows[0]["ACC_CURRENT_BALANCE"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["ACC_CURRENT_BALANCE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ACC_CURRENT_BALANCE"] = value;
			}
		}
		// model for database field ACC_RELATES_TO_TYPE(int)
		public int ACC_RELATES_TO_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["ACC_RELATES_TO_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ACC_RELATES_TO_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ACC_RELATES_TO_TYPE"] = value;
			}
		}
		// model for database field BUDGET_CATEGORY_ID(int)
		public int BUDGET_CATEGORY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["BUDGET_CATEGORY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["BUDGET_CATEGORY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["BUDGET_CATEGORY_ID"] = value;
			}
		}
		// model for database field WOLVERINE_USER_ID(int)
		public int WOLVERINE_USER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["WOLVERINE_USER_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["WOLVERINE_USER_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["WOLVERINE_USER_ID"] = value;
			}
		}
		#endregion
	}
}
