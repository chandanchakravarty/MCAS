/* *****************************************************************************************
<Author					: -   Ashwani
<Start Date				: -	  07 Mar 2006
<End Date				: -	
<Description			: -  model class for saving policy quote 
<Review Date			: - 
<Reviewed By			: - 	
Modification History
****************************************************************************************** */ 
using System;
using System.Data;
using Cms.Model;

namespace Cms.Model.Quote
{
	/// <summary>
	/// Summary description for ClsGeneratePolicyQuoteInfo.
	/// </summary>
	public class ClsGeneratePolicyQuoteInfo : Cms.Model.ClsCommonModel 
	{
		private const string QOT_CUSTOMER_QUOTE_LIST = "QOT_CUSTOMER_QUOTE_LIST_POL";
		public ClsGeneratePolicyQuoteInfo()
		{			
			base.dtModel.TableName = "QOT_CUSTOMER_QUOTE_LIST";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table QOT_CUSTOMER_QUOTE_LIST
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("QUOTE_ID",typeof(int));
			base.dtModel.Columns.Add("QUOTE_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("QUOTE_TYPE",typeof(string));
			base.dtModel.Columns.Add("QUOTE_NUMBER",typeof(string));
			base.dtModel.Columns.Add("QUOTE_DESCRIPTION",typeof(string));
			base.dtModel.Columns.Add("IS_ACCEPTED",typeof(string));
			base.dtModel.Columns.Add("QUOTE_XML",typeof(string));
			base.dtModel.Columns.Add("QUOTE_INPUT_XML",typeof(string));
			base.dtModel.Columns.Add("RATE_EFFECTIVE_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("BUSINESS_TYPE",typeof(string));
		}
		#region Database schema details
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
		// model for database field QUOTE_ID(int)
		public int QUOTE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["QUOTE_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["QUOTE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["QUOTE_ID"] = value;
			}
		}
		// model for database field QUOTE_VERSION_ID(int)
		public int QUOTE_VERSION_ID
		{
			get
			{
				return base.dtModel.Rows[0]["QUOTE_VERSION_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["QUOTE_VERSION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["QUOTE_VERSION_ID"] = value;
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
		// model for database field QUOTE_TYPE(string)
		public string QUOTE_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["QUOTE_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["QUOTE_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["QUOTE_TYPE"] = value;
			}
		}
		// model for database field QUOTE_NUMBER(string)
		public string QUOTE_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["QUOTE_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["QUOTE_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["QUOTE_NUMBER"] = value;
			}
		}
		// model for database field QUOTE_DESCRIPTION(string)
		public string QUOTE_DESCRIPTION
		{
			get
			{
				return base.dtModel.Rows[0]["QUOTE_DESCRIPTION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["QUOTE_DESCRIPTION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["QUOTE_DESCRIPTION"] = value;
			}
		}
		// model for database field IS_ACCEPTED(string)
		public string IS_ACCEPTED
		{
			get
			{
				return base.dtModel.Rows[0]["IS_ACCEPTED"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IS_ACCEPTED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_ACCEPTED"] = value;
			}
		}

		// model for database field QUOTE_XML(string)
		public string QUOTE_XML
		{
			get
			{
				return base.dtModel.Rows[0]["QUOTE_XML"] == DBNull.Value ? "" : base.dtModel.Rows[0]["QUOTE_XML"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["QUOTE_XML"] = value;
			}
		}
		// added by ashwani on 28 Feb. 2006 
		// model for database field QUOTE_INPUT_XML(string)
		public string QUOTE_INPUT_XML
		{
			get
			{
				return base.dtModel.Rows[0]["QUOTE_INPUT_XML"] == DBNull.Value ? "" : base.dtModel.Rows[0]["QUOTE_INPUT_XML"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["QUOTE_INPUT_XML"] = value;
			}
		}
		// model for database field RATE_EFFECTIVE_DATE(DateTime)
		public DateTime RATE_EFFECTIVE_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["RATE_EFFECTIVE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["RATE_EFFECTIVE_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RATE_EFFECTIVE_DATE"] = value;
			}
		}
		// model for database field BUSINESS_TYPE(string)
		public string BUSINESS_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["BUSINESS_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["BUSINESS_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BUSINESS_TYPE"] = value;
			}
		}
		#endregion
	}
}
