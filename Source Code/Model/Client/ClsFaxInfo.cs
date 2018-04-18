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
	public class ClsFaxInfo	:	Cms.Model.ClsCommonModel
	{
		private const string CLT_CUSTOMER_FAX = "CLT_CUSTOMER_FAX";

		public ClsFaxInfo()
		{
			base.dtModel.TableName	= "CLT_CUSTOMER_FAX";		// setting table name for data table that holds property values.
			this.AddColumns();									// add columns of the database table CLT_CUSTOMER_FAX
			base.dtModel.Rows.Add(base.dtModel.NewRow());		// add a blank row in the datatable
		}
		private void AddColumns()
		{	
			base.dtModel.Columns.Add("FAX_ROW_ID",typeof(int));
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("FAX_NUMBER",typeof(string));
			base.dtModel.Columns.Add("FAX_FROM_NAME",typeof(string));
			base.dtModel.Columns.Add("FAX_FROM",typeof(string));
			base.dtModel.Columns.Add("FAX_TO",typeof(string));
			base.dtModel.Columns.Add("FAX_RECIPIENTS",typeof(string));
			base.dtModel.Columns.Add("FAX_SUBJECT",typeof(string));
			base.dtModel.Columns.Add("FAX_REFERENCE",typeof(string));
			base.dtModel.Columns.Add("FAX_BODY",typeof(string));
			base.dtModel.Columns.Add("FAX_ATTACH_PATH",typeof(string));
			base.dtModel.Columns.Add("FAX_SEND_DATE",typeof(DateTime));			
			base.dtModel.Columns.Add("FAX_RETURN_CODE",typeof(string));	
			base.dtModel.Columns.Add("DIARY_ITEM_REQ",typeof(string));
			base.dtModel.Columns.Add("FOLLOW_UP_DATE",typeof(DateTime));
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
		// model for database field DIARY_ITEM_REQ(string)
		public string DIARY_ITEM_REQ
		{
			get
			{
				return base.dtModel.Rows[0]["DIARY_ITEM_REQ"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DIARY_ITEM_REQ"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DIARY_ITEM_REQ"] = value;
			}
		}
		// model for database field FOLLOW_UP_DATE(string)
		public DateTime FOLLOW_UP_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["FOLLOW_UP_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["FOLLOW_UP_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["FOLLOW_UP_DATE"] = value;
			}
		}
	
		// model for database field FAX_ROW_ID(int)
		public int FAX_ROW_ID
		{
			get
			{
				return base.dtModel.Rows[0]["FAX_ROW_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["FAX_ROW_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["FAX_ROW_ID"] = value;
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

		// model for database field FAX_NUMBER(string)
		public string FAX_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["FAX_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["FAX_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["FAX_NUMBER"] = value;
			}
		}

		// model for database field FAX_FROM_NAME(string)
		public string FAX_FROM_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["FAX_FROM_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["FAX_FROM_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["FAX_FROM_NAME"] = value;
			}
		}
		// model for database field FAX_FROM(string)
		public string FAX_FROM
		{
			get
			{
				return base.dtModel.Rows[0]["FAX_FROM"] == DBNull.Value ? "" : base.dtModel.Rows[0]["FAX_FROM"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["FAX_FROM"] = value;
			}
		}
		// model for database field FAX_TO(string)
		public string FAX_TO
		{
			get
			{
				return base.dtModel.Rows[0]["FAX_TO"] == DBNull.Value ? "" : base.dtModel.Rows[0]["FAX_TO"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["FAX_TO"] = value;
			}
		}
		// model for database field FAX_RECIPIENTS(string)
		public string FAX_RECIPIENTS
		{
			get
			{
				return base.dtModel.Rows[0]["FAX_RECIPIENTS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["FAX_RECIPIENTS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["FAX_RECIPIENTS"] = value;
			}
		}
		// model for database field FAX_SUBJECT(string)
		public string FAX_SUBJECT
		{
			get
			{
				return base.dtModel.Rows[0]["FAX_SUBJECT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["FAX_SUBJECT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["FAX_SUBJECT"] = value;
			}
		}

		// model for database field FAX_REFERENCE(string)
		public string FAX_REFERENCE
		{
			get
			{
				return base.dtModel.Rows[0]["FAX_REFERENCE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["FAX_REFERENCE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["FAX_REFERENCE"] = value;
			}
		}

		// model for database field FAX_MESSAGE(string)
		public string FAX_BODY
		{
			get
			{
				return base.dtModel.Rows[0]["FAX_BODY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["FAX_BODY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["FAX_BODY"] = value;
			}
		}
		// model for database field FAX_ATTACH_PATH(string)
		public string FAX_ATTACH_PATH
		{
			get
			{
				return base.dtModel.Rows[0]["FAX_ATTACH_PATH"] == DBNull.Value ? "" : base.dtModel.Rows[0]["FAX_ATTACH_PATH"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["FAX_ATTACH_PATH"] = value;
			}
		}

		/// model for database field CREATED_DATETIME(datetime)		
		public DateTime FAX_SEND_DATE
		{
			get
			{
				return dtModel.Rows[0]["FAX_SEND_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dtModel.Rows[0]["FAX_SEND_DATE"]);
			}
			set
			{
				dtModel.Rows[0]["FAX_SEND_DATE"] = value;
			}
		}

		// model for database field FAX_RETURN_CODE(string)
		public string FAX_RETURN_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["FAX_RETURN_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["FAX_RETURN_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["FAX_RETURN_CODE"] = value;
			}
		}

	}
}