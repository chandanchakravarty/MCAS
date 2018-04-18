using System;
using System.Data;
using Cms.Model;

namespace Cms.Model.Account
{
	/// <summary>
	/// Summary description for ClsFeeReversalInfo.
	/// </summary>
	public class ClsFeeReversalInfo:Cms.Model.ClsCommonModel
	{
		private const string ACT_FEE_REVERSAL = "ACT_FEE_REVERSAL";
		public ClsFeeReversalInfo()
		{
			base.dtModel.TableName = "ACT_FEE_REVERSAL"; // setting table name for data table that holds property values.
			this.AddColumns();								 // add columns of the database table ACT_CURRENT_DEPOSITS
			base.dtModel.Rows.Add(base.dtModel.NewRow());	 // add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("IDEN_ROW_ID",typeof(int));
			base.dtModel.Columns.Add("CUSTOMER_OPEN_ITEM_ID",typeof(int));
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("FEE_TYPE",typeof(string));
			base.dtModel.Columns.Add("FEES_AMOUNT",typeof(double));
			base.dtModel.Columns.Add("FEES_REVERSE",typeof(double));
			base.dtModel.Columns.Add("IS_COMMITTED",typeof(string));			
		}
		
		#region Database schema details
		
		// model for database field IDEN_ROW_ID(int)
		public int IDEN_ROW_ID
		{
			get
			{
				return base.dtModel.Rows[0]["IDEN_ROW_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["IDEN_ROW_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["IDEN_ROW_ID"] = value;
			}
		}

		// model for database field CUSTOMER_OPEN_ITEM_ID(int)
		public int CUSTOMER_OPEN_ITEM_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_OPEN_ITEM_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CUSTOMER_OPEN_ITEM_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_OPEN_ITEM_ID"] = value;
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

		// model for database field POLICY_ID(int)
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

		// model for database field FEE_TYPE(string)
		public string FEE_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["FEE_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["FEE_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["FEE_TYPE"] = value;
			}
		}

		// model for database field FEES_AMOUNT(double)
		public double FEES_AMOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["FEES_AMOUNT"] == DBNull.Value ? Convert.ToDouble(null) : Double.Parse(base.dtModel.Rows[0]["FEES_AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["FEES_AMOUNT"] = value;
			}
		}
		// model for database field FEES_REVERSE(double)
		public double FEES_REVERSE
		{
			get
			{
				return base.dtModel.Rows[0]["FEES_REVERSE"] == DBNull.Value ? Convert.ToDouble(null) : Double.Parse(base.dtModel.Rows[0]["FEES_REVERSE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["FEES_REVERSE"] = value;
			}
		}
		// model for database field IS_COMMITTED(bit)
		public byte IS_COMMITTED
		{
			get
			{
				return base.dtModel.Rows[0]["IS_COMMITTED"] == DBNull.Value ? Convert.ToByte(null) : Byte.Parse(base.dtModel.Rows[0]["IS_COMMITTED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["IS_COMMITTED"] = value;
			}
		}



		
		#endregion


	}
}
