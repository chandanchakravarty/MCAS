/******************************************************************************************
<Author					: - Sumit Chhabra
<Start Date				: -	26/04/2006
<End Date				: -	
<Description			: - Models CLM_DUMMY_POLICY
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
namespace Cms.Model.Claims
{
	/// <summary>
	/// Database Model for CLM_DUMMY_POLICY.
	/// </summary>
	public class ClsDummyPolicyInfo : Cms.Model.ClsCommonModel
	{
		private const string CLM_DUMMY_POLICY = "CLM_DUMMY_POLICY";
		public ClsDummyPolicyInfo()
		{
			base.dtModel.TableName = "CLM_DUMMY_POLICY";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table CLM_DUMMY_POLICY
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{

			base.dtModel.Columns.Add("DUMMY_POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("CLAIM_ID",typeof(int));
			base.dtModel.Columns.Add("INSURED_NAME",typeof(string));
			base.dtModel.Columns.Add("POLICY_NUMBER",typeof(string));	
			base.dtModel.Columns.Add("EFFECTIVE_DATE",typeof(DateTime));			
			base.dtModel.Columns.Add("EXPIRATION_DATE",typeof(DateTime));			
			base.dtModel.Columns.Add("LOB_ID",typeof(int));			
			base.dtModel.Columns.Add("NOTES",typeof(string));
			base.dtModel.Columns.Add("DUMMY_ADD1",typeof(string));
			base.dtModel.Columns.Add("DUMMY_ADD2",typeof(string));
			base.dtModel.Columns.Add("DUMMY_CITY",typeof(string));
			base.dtModel.Columns.Add("DUMMY_ZIP",typeof(string));
			base.dtModel.Columns.Add("DUMMY_STATE",typeof(string));
			base.dtModel.Columns.Add("DUMMY_COUNTRY",typeof(string));
			
		}
		#region Database schema details
		// model for database field DUMMY_POLICY_ID(int)
		public int DUMMY_POLICY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["DUMMY_POLICY_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DUMMY_POLICY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DUMMY_POLICY_ID"] = value;
			}
		}
		// model for database field CLAIM_ID(int)
		public int CLAIM_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CLAIM_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CLAIM_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CLAIM_ID"] = value;
			}
		}
		
		// model for database field INSURED_NAME(string)
		public string INSURED_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["INSURED_NAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["INSURED_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["INSURED_NAME"] = value;
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
		// model for database field LOB_ID(int)
		public int LOB_ID
		{
			get
			{
				return base.dtModel.Rows[0]["LOB_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LOB_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LOB_ID"] = value;
			}
		}		
		// model for database field NOTES(string)
		public string NOTES
		{
			get
			{
				return base.dtModel.Rows[0]["NOTES"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["NOTES"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["NOTES"] = value;
			}
		}		
		// model for database field EFFECTIVE_DATE(DateTime)
		public DateTime EFFECTIVE_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["EFFECTIVE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["EFFECTIVE_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EFFECTIVE_DATE"] = value;
			}
		}
		// model for database field EXPIRATION_DATE(DateTime)
		public DateTime EXPIRATION_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["EXPIRATION_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["EXPIRATION_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EXPIRATION_DATE"] = value;
			}
		}
		// model for database field DUMMY_ADD1(string)
		public string DUMMY_ADD1
		{
			get
			{
				return base.dtModel.Rows[0]["DUMMY_ADD1"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DUMMY_ADD1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DUMMY_ADD1"] = value;
			}
		}		
		// model for database field DUMMY_ADD2(string)
		public string DUMMY_ADD2
		{
			get
			{
				return base.dtModel.Rows[0]["DUMMY_ADD2"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DUMMY_ADD2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DUMMY_ADD2"] = value;
			}
		}		
		// model for database field DUMMY_CITY(string)
		public string DUMMY_CITY
		{
			get
			{
				return base.dtModel.Rows[0]["DUMMY_CITY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DUMMY_CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DUMMY_CITY"] = value;
			}
		}		
		// model for database field DUMMY_ZIP(string)
		public string DUMMY_ZIP
		{
			get
			{
				return base.dtModel.Rows[0]["DUMMY_ZIP"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DUMMY_ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DUMMY_ZIP"] = value;
			}
		}		
		// model for database field DUMMY_STATE(string)
		public string DUMMY_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["DUMMY_STATE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DUMMY_STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DUMMY_STATE"] = value;
			}
		}		
		// model for database field DUMMY_COUNTRY(string)
		public string DUMMY_COUNTRY
		{
			get
			{
				return base.dtModel.Rows[0]["DUMMY_COUNTRY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DUMMY_COUNTRY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DUMMY_COUNTRY"] = value;
			}
		}		
		#endregion
	}
}
