/******************************************************************************************
<Author				: -   
<Start Date				: -	6/29/2005 12:03:52 PM
<End Date				: -	
<Description				: - 	
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


namespace Cms.Model.Client
{	
	public class ClsEmailInfo:Cms.Model.ClsCommonModel
	{
		private const string CLT_CUSTOMER_EMAIL = "CLT_CUSTOMER_EMAIL";

		public ClsEmailInfo()
		{
			base.dtModel.TableName = "CLT_CUSTOMER_EMAIL";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table CLT_CUSTOMER_EMAIL
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{	
			base.dtModel.Columns.Add("EMAIL_ROW_ID",typeof(int));
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("EMAIL_FROM_NAME",typeof(string));
			base.dtModel.Columns.Add("EMAIL_FROM",typeof(string));
			base.dtModel.Columns.Add("EMAIL_TO",typeof(string));
			base.dtModel.Columns.Add("EMAIL_RECIPIENTS",typeof(string));
			base.dtModel.Columns.Add("EMAIL_SUBJECT",typeof(string));
			base.dtModel.Columns.Add("EMAIL_MESSAGE",typeof(string));
			base.dtModel.Columns.Add("EMAIL_ATTACH_PATH",typeof(string));
			base.dtModel.Columns.Add("EMAIL_SEND_DATE",typeof(DateTime));			
			base.dtModel.Columns.Add("DIARY_ITEM_REQ",typeof(string));
			base.dtModel.Columns.Add("FOLLOW_UP_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("POLICY_NUMBER",typeof(string));
			base.dtModel.Columns.Add("CLAIM_NUMBER",typeof(string));
			base.dtModel.Columns.Add("APP_NUMBER",typeof(string));
			base.dtModel.Columns.Add("QUOTE",typeof(string));
			base.dtModel.Columns.Add("DIARY_ITEM_TO",typeof(int));
		}
		// model for database field DIARY_ITEM_TO(int)
		public int DIARY_ITEM_TO
		{
			get
			{
				return base.dtModel.Rows[0]["DIARY_ITEM_TO"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["DIARY_ITEM_TO"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DIARY_ITEM_TO"] = value;
			}
		}
		// model for database field EMAIL_ROW_ID(int)
		public int EMAIL_ROW_ID
		{
			get
			{
				return base.dtModel.Rows[0]["EMAIL_ROW_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["EMAIL_ROW_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EMAIL_ROW_ID"] = value;
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
		// model for database field EMAIL_FROM_NAME(string)
		public string EMAIL_FROM_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["EMAIL_FROM_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["EMAIL_FROM_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EMAIL_FROM_NAME"] = value;
			}
		}
		// model for database field EMAIL_FROM(string)
		public string EMAIL_FROM
		{
			get
			{
				return base.dtModel.Rows[0]["EMAIL_FROM"] == DBNull.Value ? "" : base.dtModel.Rows[0]["EMAIL_FROM"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EMAIL_FROM"] = value;
			}
		}
		// model for database field EMAIL_TO(string)
		public string EMAIL_TO
		{
			get
			{
				return base.dtModel.Rows[0]["EMAIL_TO"] == DBNull.Value ? "" : base.dtModel.Rows[0]["EMAIL_TO"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EMAIL_TO"] = value;
			}
		}
		// model for database field EMAIL_RECIPIENTS(string)
		public string EMAIL_RECIPIENTS
		{
			get
			{
				return base.dtModel.Rows[0]["EMAIL_RECIPIENTS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["EMAIL_RECIPIENTS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EMAIL_RECIPIENTS"] = value;
			}
		}
		// model for database field EMAIL_SUBJECT(string)
		public string EMAIL_SUBJECT
		{
			get
			{
				return base.dtModel.Rows[0]["EMAIL_SUBJECT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["EMAIL_SUBJECT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EMAIL_SUBJECT"] = value;
			}
		}
		// model for database field EMAIL_MESSAGE(string)
		public string EMAIL_MESSAGE
		{
			get
			{
				return base.dtModel.Rows[0]["EMAIL_MESSAGE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["EMAIL_MESSAGE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EMAIL_MESSAGE"] = value;
			}
		}
		// model for database field EMAIL_ATTACH_PATH(string)
		public string EMAIL_ATTACH_PATH
		{
			get
			{
				return base.dtModel.Rows[0]["EMAIL_ATTACH_PATH"] == DBNull.Value ? "" : base.dtModel.Rows[0]["EMAIL_ATTACH_PATH"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EMAIL_ATTACH_PATH"] = value;
			}
		}

		/// model for database field CREATED_DATETIME(datetime)		
		public DateTime EMAIL_SEND_DATE
		{
			get
			{
				return dtModel.Rows[0]["EMAIL_SEND_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dtModel.Rows[0]["EMAIL_SEND_DATE"]);
			}
			set
			{
				dtModel.Rows[0]["EMAIL_SEND_DATE"] = value;
			}
		}
		/// model for database field FOLLOW_UP_DATE(datetime)		
		public DateTime FOLLOW_UP_DATE
		{
			get
			{
				return dtModel.Rows[0]["FOLLOW_UP_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dtModel.Rows[0]["FOLLOW_UP_DATE"]);
			}
			set
			{
				dtModel.Rows[0]["FOLLOW_UP_DATE"] = value;
			}
		}
		//// model for database field DIARY_ITEM_REQ(string)
		public string DIARY_ITEM_REQ
		{
			get
			{
				return base.dtModel.Rows[0]["DIARY_ITEM_REQ"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DIARY_ITEM_REQ"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DIARY_ITEM_REQ"] = value;
			}
		}
		// model for database field EMAIL_FROM_NAME(string)
		public string POLICY_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["POLICY_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_NUMBER"] = value;
			}
		}

		// model for database field CLAIM_NUMBER(string)
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

		// model for database field EMAIL_FROM_NAME(string)
		public string APP_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["APP_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["APP_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["APP_NUMBER"] = value;
			}
		}
		// model for database field EMAIL_FROM_NAME(string)
		public string QUOTE
		{
			get
			{
				return base.dtModel.Rows[0]["QUOTE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["QUOTE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["QUOTE"] = value;
			}
		}
	}
}
		