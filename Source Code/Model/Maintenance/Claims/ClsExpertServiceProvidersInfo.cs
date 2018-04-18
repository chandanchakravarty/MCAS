/******************************************************************************************
<Author					: - Sumit Chhabra
<Start Date				: -	20/04/2006
<End Date				: -	
<Description				: - 	Models CLM_EXPERT_SERVICE_PROVIDERS
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
namespace Cms.Model.Maintenance.Claims
{
	/// <summary>
	/// Database Model for CLM_EXPERT_SERVICE_PROVIDERS.
	/// </summary>
	public class ClsExpertServiceProvidersInfo : Cms.Model.ClsCommonModel
	{
		private const string CLM_EXPERT_SERVICE_PROVIDERS = "CLM_EXPERT_SERVICE_PROVIDERS";
		public ClsExpertServiceProvidersInfo()
		{
			base.dtModel.TableName = "CLM_EXPERT_SERVICE_PROVIDERS";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table CLM_EXPERT_SERVICE_PROVIDERS
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{

			base.dtModel.Columns.Add("EXPERT_SERVICE_ID",typeof(int));
			base.dtModel.Columns.Add("EXPERT_SERVICE_TYPE",typeof(int));
			base.dtModel.Columns.Add("EXPERT_SERVICE_NAME",typeof(string));
			base.dtModel.Columns.Add("EXPERT_SERVICE_ADDRESS1",typeof(string));
			base.dtModel.Columns.Add("EXPERT_SERVICE_ADDRESS2",typeof(string));
			base.dtModel.Columns.Add("EXPERT_SERVICE_CITY",typeof(string));
			base.dtModel.Columns.Add("EXPERT_SERVICE_STATE",typeof(int));
			base.dtModel.Columns.Add("EXPERT_SERVICE_ZIP",typeof(string));
			base.dtModel.Columns.Add("EXPERT_SERVICE_VENDOR_CODE",typeof(string));
			base.dtModel.Columns.Add("EXPERT_SERVICE_MASTER_VENDOR_CODE",typeof(string));
			base.dtModel.Columns.Add("EXPERT_SERVICE_CONTACT_NAME",typeof(string));
			base.dtModel.Columns.Add("EXPERT_SERVICE_CONTACT_PHONE",typeof(string));
			base.dtModel.Columns.Add("EXPERT_SERVICE_CONTACT_EMAIL",typeof(string));
			base.dtModel.Columns.Add("EXPERT_SERVICE_FEDRERAL_ID",typeof(string));
			base.dtModel.Columns.Add("EXPERT_SERVICE_1099_PROCESSING_OPTION",typeof(int));
			base.dtModel.Columns.Add("EXPERT_SERVICE_COUNTRY",typeof(string));
			base.dtModel.Columns.Add("EXPERT_SERVICE_TYPE_DESC",typeof(string));
			base.dtModel.Columns.Add("OTHER_DETAILS",typeof(string));
			base.dtModel.Columns.Add("PARTY_DETAIL",typeof(int));
			base.dtModel.Columns.Add("AGE",typeof(int));
			base.dtModel.Columns.Add("EXTENT_OF_INJURY",typeof(string));			
			base.dtModel.Columns.Add("BANK_NAME",typeof(string));
            //Added by Santosh Santosh Kumar Gautam
            base.dtModel.Columns.Add("AGENCY_BANK", typeof(string));
			base.dtModel.Columns.Add("ACCOUNT_NUMBER",typeof(string));
			base.dtModel.Columns.Add("ACCOUNT_NAME",typeof(string));			

			base.dtModel.Columns.Add("PARENT_ADJUSTER",typeof(int));
			base.dtModel.Columns.Add("EXPERT_SERVICE_CONTACT_PHONE_EXT",typeof(string));			
			base.dtModel.Columns.Add("EXPERT_SERVICE_CONTACT_FAX",typeof(string));
			base.dtModel.Columns.Add("MAIL_1099_ADD1",typeof(string));

			base.dtModel.Columns.Add("MAIL_1099_ADD2",typeof(string));
			base.dtModel.Columns.Add("MAIL_1099_CITY",typeof(string));
			base.dtModel.Columns.Add("MAIL_1099_STATE",typeof(string));
			base.dtModel.Columns.Add("MAIL_1099_COUNTRY",typeof(string));
			base.dtModel.Columns.Add("MAIL_1099_ZIP",typeof(string));

			base.dtModel.Columns.Add("MAIL_1099_NAME",typeof(string));
			base.dtModel.Columns.Add("W9_FORM",typeof(string));
			base.dtModel.Columns.Add("REQ_SPECIAL_HANDLING",typeof(int));
            base.dtModel.Columns.Add("ACTIVITY", typeof(int));
            base.dtModel.Columns.Add("REGIONAL_IDENTIFICATION", typeof(string));
            base.dtModel.Columns.Add("REG_ID_ISSUE", typeof(string));
            base.dtModel.Columns.Add("CPF", typeof(string));
            base.dtModel.Columns.Add("DATE_OF_BIRTH", typeof(DateTime));
            base.dtModel.Columns.Add("REG_ID_ISSUE_DATE", typeof(DateTime));

		}
		#region Database schema details
		// model for database field EXPERT_SERVICE_ID(int)
		public int EXPERT_SERVICE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["EXPERT_SERVICE_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["EXPERT_SERVICE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EXPERT_SERVICE_ID"] = value;
			}
		}
		// model for database field EXPERT_SERVICE_TYPE(int)
		public int EXPERT_SERVICE_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["EXPERT_SERVICE_TYPE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["EXPERT_SERVICE_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EXPERT_SERVICE_TYPE"] = value;
			}
		}
		// model for database field EXPERT_SERVICE_TYPE_DESC(string)
		public string EXPERT_SERVICE_TYPE_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["EXPERT_SERVICE_TYPE_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["EXPERT_SERVICE_TYPE_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EXPERT_SERVICE_TYPE_DESC"] = value;
			}
		}		
		// model for database field EXPERT_SERVICE_NAME(string)
		public string EXPERT_SERVICE_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["EXPERT_SERVICE_NAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["EXPERT_SERVICE_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EXPERT_SERVICE_NAME"] = value;
			}
		}
		// model for database field EXPERT_SERVICE_ADDRESS1(string)
		public string EXPERT_SERVICE_ADDRESS1
		{
			get
			{
				return base.dtModel.Rows[0]["EXPERT_SERVICE_ADDRESS1"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["EXPERT_SERVICE_ADDRESS1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EXPERT_SERVICE_ADDRESS1"] = value;
			}
		}
		// model for database field EXPERT_SERVICE_ADDRESS2(string)
		public string EXPERT_SERVICE_ADDRESS2
		{
			get
			{
				return base.dtModel.Rows[0]["EXPERT_SERVICE_ADDRESS2"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["EXPERT_SERVICE_ADDRESS2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EXPERT_SERVICE_ADDRESS2"] = value;
			}
		}
		// model for database field EXPERT_SERVICE_CITY(string)
		public string EXPERT_SERVICE_CITY
		{
			get
			{
				return base.dtModel.Rows[0]["EXPERT_SERVICE_CITY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["EXPERT_SERVICE_CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EXPERT_SERVICE_CITY"] = value;
			}
		}
		// model for database field EXPERT_SERVICE_STATE(int)
		public int EXPERT_SERVICE_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["EXPERT_SERVICE_STATE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["EXPERT_SERVICE_STATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EXPERT_SERVICE_STATE"] = value;
			}
		}	
		// model for database field EXPERT_SERVICE_ZIP(string)
		public string EXPERT_SERVICE_ZIP
		{
			get
			{
				return base.dtModel.Rows[0]["EXPERT_SERVICE_ZIP"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["EXPERT_SERVICE_ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EXPERT_SERVICE_ZIP"] = value;
			}
		}
		// model for database field EXPERT_SERVICE_VENDOR_CODE(string)
		public string EXPERT_SERVICE_VENDOR_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["EXPERT_SERVICE_VENDOR_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["EXPERT_SERVICE_VENDOR_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EXPERT_SERVICE_VENDOR_CODE"] = value;
			}
		}
		// model for database field EXPERT_SERVICE_CONTACT_NAME(string)
		public string EXPERT_SERVICE_CONTACT_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["EXPERT_SERVICE_CONTACT_NAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["EXPERT_SERVICE_CONTACT_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EXPERT_SERVICE_CONTACT_NAME"] = value;
			}
		}
		// model for database field EXPERT_SERVICE_CONTACT_PHONE(string)
		public string EXPERT_SERVICE_CONTACT_PHONE
		{
			get
			{
				return base.dtModel.Rows[0]["EXPERT_SERVICE_CONTACT_PHONE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["EXPERT_SERVICE_CONTACT_PHONE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EXPERT_SERVICE_CONTACT_PHONE"] = value;
			}
		}
		// model for database field EXPERT_SERVICE_CONTACT_EMAIL(string)
		public string EXPERT_SERVICE_CONTACT_EMAIL
		{
			get
			{
				return base.dtModel.Rows[0]["EXPERT_SERVICE_CONTACT_EMAIL"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["EXPERT_SERVICE_CONTACT_EMAIL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EXPERT_SERVICE_CONTACT_EMAIL"] = value;
			}
		}
		// model for database field EXPERT_SERVICE_FEDRERAL_ID(string)
		public string EXPERT_SERVICE_FEDRERAL_ID
		{
			get
			{
				return base.dtModel.Rows[0]["EXPERT_SERVICE_FEDRERAL_ID"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["EXPERT_SERVICE_FEDRERAL_ID"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EXPERT_SERVICE_FEDRERAL_ID"] = value;
			}
		}
		// model for database field EXPERT_SERVICE_1099_PROCESSING_OPTION(int)
		public int EXPERT_SERVICE_1099_PROCESSING_OPTION
		{
			get
			{
				return base.dtModel.Rows[0]["EXPERT_SERVICE_1099_PROCESSING_OPTION"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["EXPERT_SERVICE_1099_PROCESSING_OPTION"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EXPERT_SERVICE_1099_PROCESSING_OPTION"] = value;
			}
		}		
		// model for database field EXPERT_SERVICE_COUNTRY(string)
		public string EXPERT_SERVICE_COUNTRY
		{
			get
			{
				return base.dtModel.Rows[0]["EXPERT_SERVICE_COUNTRY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["EXPERT_SERVICE_COUNTRY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EXPERT_SERVICE_COUNTRY"] = value;
			}
		}	
		// model for database field EXPERT_SERVICE_MASTER_VENDOR_CODE(string)
		public string EXPERT_SERVICE_MASTER_VENDOR_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["EXPERT_SERVICE_MASTER_VENDOR_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["EXPERT_SERVICE_MASTER_VENDOR_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EXPERT_SERVICE_MASTER_VENDOR_CODE"] = value;
			}
		}
		// model for database field PARTY_DETAIL(int)
		public int PARTY_DETAIL
		{
			get
			{
				return base.dtModel.Rows[0]["PARTY_DETAIL"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PARTY_DETAIL"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PARTY_DETAIL"] = value;
			}
		}
		// model for database field AGE(smallint)
		public int AGE
		{
			get
			{
				return base.dtModel.Rows[0]["AGE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["AGE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AGE"] = value;
			}
		}
		// model for database field EXTENT_OF_INJURY(string)
		public string EXTENT_OF_INJURY
		{
			get
			{
				return base.dtModel.Rows[0]["EXTENT_OF_INJURY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["EXTENT_OF_INJURY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EXTENT_OF_INJURY"] = value;
			}
		}
		// model for database field OTHER_DETAILS(string)
		public string OTHER_DETAILS
		{
			get
			{
				return base.dtModel.Rows[0]["OTHER_DETAILS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["OTHER_DETAILS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["OTHER_DETAILS"] = value;
			}
		}
		// model for database field BANK_NAME		(string)
		public string BANK_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["BANK_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["BANK_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BANK_NAME"] = value;
			}
		}

        //Added by Santosh Kumar Gautam on 15 Nov 2010
        // model for database field BANK_NAME		(string)
        public string AGENCY_BANK
        {
            get
            {
                return base.dtModel.Rows[0]["AGENCY_BANK"] == DBNull.Value ? "" : base.dtModel.Rows[0]["AGENCY_BANK"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["AGENCY_BANK"] = value;
            }
        }
		// model for database field ACCOUNT_NUMBER		(string)
		public string ACCOUNT_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["ACCOUNT_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ACCOUNT_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ACCOUNT_NUMBER"] = value;
			}
		}
		// model for database field ACCOUNT_NAME		(string)
		public string ACCOUNT_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["ACCOUNT_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ACCOUNT_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ACCOUNT_NAME"] = value;
			}
		}
		// model for database field EXPERT_SERVICE_CONTACT_PHONE_EXT		(string)
		public string EXPERT_SERVICE_CONTACT_PHONE_EXT
		{
			get
			{
				return base.dtModel.Rows[0]["EXPERT_SERVICE_CONTACT_PHONE_EXT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["EXPERT_SERVICE_CONTACT_PHONE_EXT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EXPERT_SERVICE_CONTACT_PHONE_EXT"] = value;
			}
		}
		// model for database field EXPERT_SERVICE_CONTACT_FAX		(string)
		public string EXPERT_SERVICE_CONTACT_FAX
		{
			get
			{
				return base.dtModel.Rows[0]["EXPERT_SERVICE_CONTACT_FAX"] == DBNull.Value ? "" : base.dtModel.Rows[0]["EXPERT_SERVICE_CONTACT_FAX"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EXPERT_SERVICE_CONTACT_FAX"] = value;
			}
		}
		// model for database field PARENT_ADJUSTER(smallint)
		public int PARENT_ADJUSTER
		{
			get
			{
				return base.dtModel.Rows[0]["PARENT_ADJUSTER"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PARENT_ADJUSTER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PARENT_ADJUSTER"] = value;
			}
		}
		// model for database field MAIL_1099_ADD1(string)
		public string MAIL_1099_ADD1
		{
			get
			{
				return base.dtModel.Rows[0]["MAIL_1099_ADD1"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MAIL_1099_ADD1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MAIL_1099_ADD1"] = value;
			}
		}
		// model for database field MAIL_1099_ADD2(string)
		public string MAIL_1099_ADD2
		{
			get
			{
				return base.dtModel.Rows[0]["MAIL_1099_ADD2"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MAIL_1099_ADD2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MAIL_1099_ADD2"] = value;
			}
		}
		// model for database field MAIL_1099_CITY(string)
		public string MAIL_1099_CITY
		{
			get
			{
				return base.dtModel.Rows[0]["MAIL_1099_CITY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MAIL_1099_CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MAIL_1099_CITY"] = value;
			}
		}
		// model for database field MAIL_1099_STATE(string)
		public string MAIL_1099_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["MAIL_1099_STATE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MAIL_1099_STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MAIL_1099_STATE"] = value;
			}
		}
		// model for database field MAIL_1099_COUNTRY(string)
		public string MAIL_1099_COUNTRY
		{
			get
			{
				return base.dtModel.Rows[0]["MAIL_1099_COUNTRY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MAIL_1099_COUNTRY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MAIL_1099_COUNTRY"] = value;
			}
		}
		// model for database field MAIL_1099_ZIP(string)
		public string MAIL_1099_ZIP
		{
			get
			{
				return base.dtModel.Rows[0]["MAIL_1099_ZIP"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MAIL_1099_ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MAIL_1099_ZIP"] = value;
			}
		}

		// model for database field MAIL_1099_NAME(string)
		public string MAIL_1099_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["MAIL_1099_NAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MAIL_1099_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MAIL_1099_NAME"] = value;
			}
		}
		//Added by Raghav For Itrack #Issue 4810
		public int REQ_SPECIAL_HANDLING
		{
			get
			{
				return base.dtModel.Rows[0]["REQ_SPECIAL_HANDLING"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["REQ_SPECIAL_HANDLING"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REQ_SPECIAL_HANDLING"] = value;
			}
		}

		// model for database field W9_FORM(string)
		public string W9_FORM
		{
			get
			{
				return base.dtModel.Rows[0]["W9_FORM"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["W9_FORM"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["W9_FORM"] = value;
			}
		}
        public int ACTIVITY
        {
            get
            {
                return base.dtModel.Rows[0]["ACTIVITY"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["ACTIVITY"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["ACTIVITY"] = value;
            }
        }
        public string CPF
        {
            get
            {
                return base.dtModel.Rows[0]["CPF"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CPF"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CPF"] = value;
            }
        }
        public string REG_ID_ISSUE
        {
            get
            {
                return base.dtModel.Rows[0]["REG_ID_ISSUE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["REG_ID_ISSUE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["REG_ID_ISSUE"] = value;
            }
        }
        public string REGIONAL_IDENTIFICATION
        {
            get
            {
                return base.dtModel.Rows[0]["REGIONAL_IDENTIFICATION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["REGIONAL_IDENTIFICATION"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["REGIONAL_IDENTIFICATION"] = value;
            }
        }
        public DateTime DATE_OF_BIRTH
        {
            get
            {
                return base.dtModel.Rows[0]["DATE_OF_BIRTH"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["DATE_OF_BIRTH"]);

            }
            set
            {
                base.dtModel.Rows[0]["DATE_OF_BIRTH"] = value;
            }
        }
        public DateTime REG_ID_ISSUE_DATE
        {
            get
            {
                return base.dtModel.Rows[0]["REG_ID_ISSUE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["REG_ID_ISSUE_DATE"]);

            }
            set
            {
                base.dtModel.Rows[0]["REG_ID_ISSUE_DATE"] = value;
            }
        }


		#endregion
	}
}
