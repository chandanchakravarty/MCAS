/******************************************************************************************
<Author				: -   Amar
<Start Date				: -	5/1/2006 5:06:18 PM
<End Date				: -	
<AGENCY_CODE				: - 	
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
	/// Database AGENCY_FAX for CLM_AGENCY.
	/// </summary>
	public class ClsAgencyDetailsInfo : Cms.Model.ClsCommonModel
	{
		private const string CLM_AGENCY = "CLM_AGENCY";
		public ClsAgencyDetailsInfo()
		{
			base.dtModel.TableName = "CLM_AGENCY";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table CLM_AGENCY
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CLAIM_ID",typeof(int));
			base.dtModel.Columns.Add("AGENCY_ID",typeof(int));			
			base.dtModel.Columns.Add("AGENCY_CODE",typeof(string));
			base.dtModel.Columns.Add("AGENCY_SUB_CODE",typeof(string));
			base.dtModel.Columns.Add("AGENCY_CUSTOMER_ID",typeof(string));									
			base.dtModel.Columns.Add("AGENCY_PHONE",typeof(string));
			base.dtModel.Columns.Add("AGENCY_FAX",typeof(string));					
			
		}
		#region Database schema details
		// AGENCY_FAX for database field CLAIM_ID(int)
		public int CLAIM_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CLAIM_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CLAIM_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CLAIM_ID"] = value;
			}
		}
		// AGENCY_FAX for database field AGENCY_ID(int)
		public int AGENCY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["AGENCY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["AGENCY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AGENCY_ID"] = value;
			}
		}
		
		// AGENCY_FAX for database field AGENCY_CODE(string)
		public string AGENCY_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["AGENCY_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["AGENCY_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["AGENCY_CODE"] = value;
			}
		}
		// AGENCY_FAX for database field AGENCY_SUB_CODE(string)
		public string AGENCY_SUB_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["AGENCY_SUB_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["AGENCY_SUB_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["AGENCY_SUB_CODE"] = value;
			}
		}
		// AGENCY_FAX for database field AGENCY_CUSTOMER_ID(string)
		public string AGENCY_CUSTOMER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["AGENCY_CUSTOMER_ID"] == DBNull.Value ? "" : base.dtModel.Rows[0]["AGENCY_CUSTOMER_ID"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["AGENCY_CUSTOMER_ID"] = value;
			}
		}
		// AGENCY_FAX for database field AGENCY_PHONE(string)
		public string AGENCY_PHONE
		{
			get
			{
				return base.dtModel.Rows[0]["AGENCY_PHONE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["AGENCY_PHONE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["AGENCY_PHONE"] = value;
			}
		}
		// AGENCY_FAX for database field AGENCY_FAX(string)
		public string AGENCY_FAX
		{
			get
			{
				return base.dtModel.Rows[0]["AGENCY_FAX"] == DBNull.Value ? "" : base.dtModel.Rows[0]["AGENCY_FAX"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["AGENCY_FAX"] = value;
			}
		}
		
		#endregion
	}
}
