using System;
using System.Data;
using Cms.Model;

namespace Cms.Model.Quote
{
	/// <summary>
	/// Summary description for ClsQuickQuoteInfo.
	/// </summary>
	public class ClsQuickQuoteInfo : Cms.Model.ClsCommonModel 
	{
		private const string MNT_QUICKQUOTE_USER_XML = "MNT_QUICKQUOTE_USER_XML";
		public ClsQuickQuoteInfo()
		{
			base.dtModel.TableName = "MNT_QUICKQUOTE_USER_XML";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table QOT_CUSTOMER_QUOTE_LIST
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("USER_ID",typeof(int));
			base.dtModel.Columns.Add("DEFAULT_XML",typeof(string));
			base.dtModel.Columns.Add("LOB",typeof(string));
			base.dtModel.Columns.Add("STATE",typeof(string));
		}
		
		public int USER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["USER_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["USER_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["USER_ID"] = value;
			}
		}
		public string DEFAULT_XML
		{
			get
			{
				return base.dtModel.Rows[0]["DEFAULT_XML"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DEFAULT_XML"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DEFAULT_XML"] = value;
			}
		}
		public string LOB
		{
			get
			{
				return base.dtModel.Rows[0]["LOB"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LOB"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOB"] = value;
			}
		}
		public string STATE
		{
			get
			{
				return base.dtModel.Rows[0]["STATE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["STATE"] = value;
			}
		}
	}
}
