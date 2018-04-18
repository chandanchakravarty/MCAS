/******************************************************************************************
<Author					: - Sumit Chhabra
<Start Date				: -	19/05/2006
<End Date				: -	
<Description			: - Models CLM_CLAIM_COMPANY
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
	/// Database Model for CLM_CLAIM_COMPANY.
	/// </summary>
	public class ClsWatercraftCompanyInfo : Cms.Model.ClsCommonModel
	{
		private const string CLM_CLAIM_COMPANY = "CLM_CLAIM_COMPANY";
		public ClsWatercraftCompanyInfo()
		{
			base.dtModel.TableName = "CLM_CLAIM_COMPANY";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table CLM_CLAIM_COMPANY
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CLAIM_ID",typeof(int));			
			base.dtModel.Columns.Add("COMPANY_ID",typeof(int));
			base.dtModel.Columns.Add("AGENCY_ID",typeof(int));
			base.dtModel.Columns.Add("NAIC_CODE",typeof(string));
			base.dtModel.Columns.Add("REFERENCE_NUMBER",typeof(string));
			base.dtModel.Columns.Add("CAT_NUMBER",typeof(string));			
			base.dtModel.Columns.Add("EFFECTIVE_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("EXPIRATION_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("ACCIDENT_DATE_TIME",typeof(DateTime));
			base.dtModel.Columns.Add("PREVIOUSLY_REPORTED",typeof(string));
			base.dtModel.Columns.Add("INSURED_CONTACT_ID",typeof(int));						
			base.dtModel.Columns.Add("CONTACT_NAME",typeof(string));			
			base.dtModel.Columns.Add("CONTACT_ADDRESS1",typeof(string));
			base.dtModel.Columns.Add("CONTACT_ADDRESS2",typeof(string));
			base.dtModel.Columns.Add("CONTACT_CITY",typeof(string));
			base.dtModel.Columns.Add("CONTACT_STATE",typeof(int));
			base.dtModel.Columns.Add("CONTACT_COUNTRY",typeof(int));
			base.dtModel.Columns.Add("CONTACT_ZIP",typeof(string));
			base.dtModel.Columns.Add("CONTACT_HOMEPHONE",typeof(string));
			base.dtModel.Columns.Add("CONTACT_WORKPHONE",typeof(string));
			base.dtModel.Columns.Add("LOSS_TIME_AM_PM",typeof(int));
		}
		#region Database schema details
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
		// model for database field COMPANY_ID(int)
		public int COMPANY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["COMPANY_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["COMPANY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COMPANY_ID"] = value;
			}
		}
		// model for database field AGENCY_ID(int)
		public int AGENCY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["AGENCY_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["AGENCY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AGENCY_ID"] = value;
			}
		}
		// model for database field NAIC_CODE(string)
		public string NAIC_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["NAIC_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["NAIC_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["NAIC_CODE"] = value;
			}
		}
		
		// model for database field REFERENCE_NUMBER(string)
		public string REFERENCE_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["REFERENCE_NUMBER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["REFERENCE_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REFERENCE_NUMBER"] = value;
			}
		}
				
		// model for database field CAT_NUMBER(string)		
		public string CAT_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["CAT_NUMBER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CAT_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CAT_NUMBER"] = value;
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
		// model for database field ACCIDENT_DATE_TIME(DateTime)
		public DateTime ACCIDENT_DATE_TIME
		{
			get
			{
				return base.dtModel.Rows[0]["ACCIDENT_DATE_TIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["ACCIDENT_DATE_TIME"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ACCIDENT_DATE_TIME"] = value;
			}
		}
		
		// model for database field PREVIOUSLY_REPORTED(string)
		public string PREVIOUSLY_REPORTED
		{
			get
			{
				return base.dtModel.Rows[0]["PREVIOUSLY_REPORTED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PREVIOUSLY_REPORTED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PREVIOUSLY_REPORTED"] = value;
			}
		}	
		// model for database field INSURED_CONTACT_ID(int)
		public int INSURED_CONTACT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["INSURED_CONTACT_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["INSURED_CONTACT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INSURED_CONTACT_ID"] = value;
			}
		}
		// model for database field CONTACT_NAME(string)
		public string CONTACT_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["CONTACT_NAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CONTACT_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTACT_NAME"] = value;
			}
		}
		// model for database field CONTACT_ADDRESS1(string)
		public string CONTACT_ADDRESS1
		{
			get
			{
				return base.dtModel.Rows[0]["CONTACT_ADDRESS1"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CONTACT_ADDRESS1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTACT_ADDRESS1"] = value;
			}
		}
		// model for database field CONTACT_ADDRESS2(string)
		public string CONTACT_ADDRESS2
		{
			get
			{
				return base.dtModel.Rows[0]["CONTACT_ADDRESS2"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CONTACT_ADDRESS2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTACT_ADDRESS2"] = value;
			}
		}
		// model for database field CONTACT_CITY(string)
		public string CONTACT_CITY
		{
			get
			{
				return base.dtModel.Rows[0]["CONTACT_CITY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CONTACT_CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTACT_CITY"] = value;
			}
		}
		// model for database field CONTACT_STATE(int)
		public int CONTACT_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["CONTACT_STATE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CONTACT_STATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CONTACT_STATE"] = value;
			}
		}	
				
		// model for database field CONTACT_COUNTRY(int)
		public int CONTACT_COUNTRY
		{
			get
			{
				return base.dtModel.Rows[0]["CONTACT_COUNTRY"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CONTACT_COUNTRY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CONTACT_COUNTRY"] = value;
			}
		}
				
		// model for database field CONTACT_ZIP(string)		
		public string CONTACT_ZIP
		{
			get
			{
				return base.dtModel.Rows[0]["CONTACT_ZIP"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CONTACT_ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTACT_ZIP"] = value;
			}
		}	
		// model for database field CONTACT_HOMEPHONE(string)
		public string CONTACT_HOMEPHONE
		{
			get
			{
				return base.dtModel.Rows[0]["CONTACT_HOMEPHONE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CONTACT_HOMEPHONE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTACT_HOMEPHONE"] = value;
			}
		}
		// model for database field CONTACT_WORKPHONE(string)
		public string CONTACT_WORKPHONE
		{
			get
			{
				return base.dtModel.Rows[0]["CONTACT_WORKPHONE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CONTACT_WORKPHONE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTACT_WORKPHONE"] = value;
			}
		}
		// model for database field LOSS_TIME_AM_PM(int)
		public int LOSS_TIME_AM_PM
		{
			get
			{
				return base.dtModel.Rows[0]["LOSS_TIME_AM_PM"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LOSS_TIME_AM_PM"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LOSS_TIME_AM_PM"] = value;
			}
		}	
		
		
		
		#endregion
	}
}
