/******************************************************************************************
<Author					: -   Ashwani Kumar
<Start Date				: -	4/25/2005 7:21:19 PM
<End Date				: -	
<Description			: - 	
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
namespace Cms.Model.Client
{
	/// <summary>
	/// Database Model for CLT_CUSTOMER_NOTES.
	/// </summary>
	public class ClsCustomerNotesInfo : Cms.Model.ClsCommonModel
	{
		private const string CLT_CUSTOMER_NOTES = "CLT_CUSTOMER_NOTES";
		public ClsCustomerNotesInfo()
		{
			base.dtModel.TableName = "CLT_CUSTOMER_NOTES";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table CLT_CUSTOMER_NOTES
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("NOTES_ID",typeof(int));
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("NOTES_SUBJECT",typeof(string));
			base.dtModel.Columns.Add("NOTES_TYPE",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VER_TRACKING_ID",typeof(int));
			base.dtModel.Columns.Add("CLAIMS_ID",typeof(int));
			base.dtModel.Columns.Add("NOTES_DESC",typeof(string));
			base.dtModel.Columns.Add("QQ_APP_POL",typeof(string));
			base.dtModel.Columns.Add("DIARY_ITEM_REQ",typeof(string));
			base.dtModel.Columns.Add("FOLLOW_UP_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("VISIBLE_TO_AGENCY",typeof(string));

		}
		#region Database schema details
		// model for database field NOTES_ID(int)
		public int NOTES_ID
		{
			get
			{
				return base.dtModel.Rows[0]["NOTES_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["NOTES_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NOTES_ID"] = value;
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
		// model for database field NOTES_SUBJECT(string)
		public string NOTES_SUBJECT
		{
			get
			{
				return base.dtModel.Rows[0]["NOTES_SUBJECT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["NOTES_SUBJECT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["NOTES_SUBJECT"] = value;
			}
		}
		// model for database field NOTES_TYPE(int)
		public int NOTES_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["NOTES_TYPE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["NOTES_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NOTES_TYPE"] = value;
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
		// model for database field POLICY_VER_TRACKING_ID(int)
		public int POLICY_VER_TRACKING_ID
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_VER_TRACKING_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["POLICY_VER_TRACKING_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_VER_TRACKING_ID"] = value;
			}
		}
		// model for database field CLAIMS_ID(int)
		public int CLAIMS_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CLAIMS_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CLAIMS_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CLAIMS_ID"] = value;
			}
		}
		// model for database field NOTES_DESC(string)
		public string NOTES_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["NOTES_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["NOTES_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["NOTES_DESC"] = value;
			}
		}
		
		// model for database field QQ_APP_POL(string)
		public string QQ_APP_POL
		{
			get
			{
				return base.dtModel.Rows[0]["QQ_APP_POL"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["QQ_APP_POL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["QQ_APP_POL"] = value;
			}
		}
		// model for database field VISIBLE_TO_AGENCY(string)
		public string VISIBLE_TO_AGENCY
		{
			get
			{
				return base.dtModel.Rows[0]["VISIBLE_TO_AGENCY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["VISIBLE_TO_AGENCY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["VISIBLE_TO_AGENCY"] = value;
			}
		}
		#endregion
	}
}
