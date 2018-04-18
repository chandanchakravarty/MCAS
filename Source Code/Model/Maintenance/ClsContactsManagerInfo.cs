/******************************************************************************************
<Author					: - Manoj Rathore
<Start Date				: -	04-07-2007
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

namespace Cms.Model.Maintenance
{
	/// <summary>
	/// Summary description for ClsContactsManagerInfo.
	/// </summary>
	public class ClsContactsManagerInfo : Cms.Model.ClsCommonModel
	{
		private const string MNT_CONTACT_LIST = "MNT_CONTACT_LIST";
		public ClsContactsManagerInfo()
		{
			base.dtModel.TableName = "MNT_CONTACT_LIST";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table MNT_COVERAGE
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}		

		private void AddColumns()
		{
			base.dtModel.Columns.Add("CONTACT_ID",typeof(int));
			base.dtModel.Columns.Add("CONTACT_CODE",typeof(string));
			base.dtModel.Columns.Add("CONTACT_TYPE_ID",typeof(int));
			base.dtModel.Columns.Add("CONTACT_SALUTATION",typeof(string));
			base.dtModel.Columns.Add("CONTACT_POS",typeof(string));
			base.dtModel.Columns.Add("INDIVIDUAL_CONTACT_ID",typeof(int));
			base.dtModel.Columns.Add("CONTACT_FNAME",typeof(string));
			base.dtModel.Columns.Add("CONTACT_MNAME",typeof(string));
			base.dtModel.Columns.Add("CONTACT_LNAME",typeof(string));
			base.dtModel.Columns.Add("CONTACT_ADD1",typeof(string));
            base.dtModel.Columns.Add("NUMBER", typeof(string));
            base.dtModel.Columns.Add("DISTRICT", typeof(string));
			base.dtModel.Columns.Add("CONTACT_ADD2",typeof(string));
			base.dtModel.Columns.Add("CONTACT_CITY",typeof(string));
			base.dtModel.Columns.Add("CONTACT_STATE",typeof(string));
			base.dtModel.Columns.Add("CONTACT_ZIP",typeof(string));
			base.dtModel.Columns.Add("CONTACT_COUNTRY",typeof(string));
			base.dtModel.Columns.Add("CONTACT_BUSINESS_PHONE",typeof(string));
			base.dtModel.Columns.Add("CONTACT_EXT",typeof(string));
			base.dtModel.Columns.Add("CONTACT_FAX",typeof(string));			
			base.dtModel.Columns.Add("CONTACT_MOBILE",typeof(string));			
			base.dtModel.Columns.Add("CONTACT_EMAIL",typeof(string));
			base.dtModel.Columns.Add("CONTACT_PAGER",typeof(string));
			base.dtModel.Columns.Add("CONTACT_HOME_PHONE",typeof(string));
			base.dtModel.Columns.Add("CONTACT_TOLL_FREE",typeof(string));			
			base.dtModel.Columns.Add("CONTACT_NOTE",typeof(string));	
			base.dtModel.Columns.Add("CONTACT_AGENCY_ID",typeof(int));
			//base.dtModel.Columns.Add("IS_ACTIVE",typeof(string));			
			//base.dtModel.Columns.Add("CREATED_BY",typeof(int));	
			//base.dtModel.Columns.Add("CREATED_DATETIME",typeof(DateTime));	
			//base.dtModel.Columns.Add("MODIFIED_BY",typeof(int));
			//base.dtModel.Columns.Add("LAST_UPDATED_DATETIME",typeof(DateTime));
            base.dtModel.Columns.Add("ACTIVITY", typeof(int));
            base.dtModel.Columns.Add("REGIONAL_IDENTIFICATION", typeof(string));
            base.dtModel.Columns.Add("REG_ID_ISSUE", typeof(string));
            base.dtModel.Columns.Add("CPF_CNPJ", typeof(string));
            base.dtModel.Columns.Add("DATE_OF_BIRTH", typeof(DateTime));
            base.dtModel.Columns.Add("REG_ID_ISSUE_DATE", typeof(DateTime));
            base.dtModel.Columns.Add("REGIONAL_ID_TYPE", typeof(string));
            base.dtModel.Columns.Add("NATIONALITY", typeof(string));
		}
		#region Database schema details
		// model for database field CONTACT_ID(int)
		public int CONTACT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CONTACT_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CONTACT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CONTACT_ID"] = value;
			}
		}
		// model for database field CONTACT_CODE(string)
		public string CONTACT_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["CONTACT_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CONTACT_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTACT_CODE"] = value;
			}
		}
		// model for database field CONTACT_TYPE_ID(int)
		public int CONTACT_TYPE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CONTACT_TYPE_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CONTACT_TYPE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CONTACT_TYPE_ID"] = value;
			}
		}
		// model for database field CONTACT_SALUTATION(string)
		public string CONTACT_SALUTATION
		{
			get
			{
				return base.dtModel.Rows[0]["CONTACT_SALUTATION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CONTACT_SALUTATION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTACT_SALUTATION"] = value;
			}
		}
        // model for database field NUMBER(string)

        public string NUMBER
        {

            get
            {
                return base.dtModel.Rows[0]["NUMBER"] == DBNull.Value ? null : Convert.ToString(base.dtModel.Rows[0]["NUMBER"]);
            }
            set
            {
                base.dtModel.Rows[0]["NUMBER"] = value;
            }
        }

        /// Model for database field DISTRICT(String)
        /// </summary> 
        public string DISTRICT
        {
            get
            {
                return base.dtModel.Rows[0]["DISTRICT"] == DBNull.Value ? null : Convert.ToString(base.dtModel.Rows[0]["DISTRICT"]);
            }
            set
            {
                base.dtModel.Rows[0]["DISTRICT"] = value;
            }
        }
		// model for database field CONTACT_POS(string)
		public string CONTACT_POS
		{
			get
			{
				return base.dtModel.Rows[0]["CONTACT_POS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CONTACT_POS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTACT_POS"] = value;
			}
		}
		// model for database field INDIVIDUAL_CONTACT_ID(int)
		public int INDIVIDUAL_CONTACT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["INDIVIDUAL_CONTACT_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["INDIVIDUAL_CONTACT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INDIVIDUAL_CONTACT_ID"] = value;
			}
		}
		// model for database field CONTACT_FNAME(string)
		public string CONTACT_FNAME
		{
			get
			{
				return base.dtModel.Rows[0]["CONTACT_FNAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CONTACT_FNAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTACT_FNAME"] = value;
			}
		}
		// model for database field CONTACT_MNAME(string)
		public string CONTACT_MNAME
		{
			get
			{
				return base.dtModel.Rows[0]["CONTACT_MNAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CONTACT_MNAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTACT_MNAME"] = value;
			}
		}
		// model for database field CONTACT_LNAME(string)
		public string CONTACT_LNAME
		{
			get
			{
				return base.dtModel.Rows[0]["CONTACT_LNAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CONTACT_LNAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTACT_LNAME"] = value;
			}
		}
		// model for database field CONTACT_ADD1(string)
		public string CONTACT_ADD1
		{
			get
			{
				return base.dtModel.Rows[0]["CONTACT_ADD1"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CONTACT_ADD1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTACT_ADD1"] = value;
			}
		}
		// model for database field CONTACT_ADD2(string)
		public string CONTACT_ADD2
		{
			get
			{
				return base.dtModel.Rows[0]["CONTACT_ADD2"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CONTACT_ADD2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTACT_ADD2"] = value;
			}
		}
		// model for database field CONTACT_CITY(string)
		public string CONTACT_CITY
		{
			get
			{
				return base.dtModel.Rows[0]["CONTACT_CITY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CONTACT_CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTACT_CITY"] = value;
			}
		}
		// model for database field CONTACT_STATE(string)
		public string CONTACT_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["CONTACT_STATE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CONTACT_STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTACT_STATE"] = value;
			}
		}
		// model for database field CONTACT_ZIP(string)
		public string CONTACT_ZIP
		{
			get
			{
				return base.dtModel.Rows[0]["CONTACT_ZIP"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CONTACT_ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTACT_ZIP"] = value;
			}
		}

		// model for database field CONTACT_COUNTRY(string)
		public string CONTACT_COUNTRY
		{
			get
			{
				return base.dtModel.Rows[0]["CONTACT_COUNTRY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CONTACT_COUNTRY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTACT_COUNTRY"] = value;
			}
		}
		// model for database field CONTACT_BUSINESS_PHONE(string)
		public string CONTACT_BUSINESS_PHONE
		{
			get
			{
				return base.dtModel.Rows[0]["CONTACT_BUSINESS_PHONE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CONTACT_BUSINESS_PHONE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTACT_BUSINESS_PHONE"] = value;
			}
		}
		// model for database field CONTACT_EXT(string)
		public string CONTACT_EXT
		{
			get
			{
				return base.dtModel.Rows[0]["CONTACT_EXT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CONTACT_EXT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTACT_EXT"] = value;
			}
		}
		// model for database field CONTACT_FAX(string)
		public string CONTACT_FAX
		{
			get
			{
				return base.dtModel.Rows[0]["CONTACT_FAX"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CONTACT_FAX"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTACT_FAX"] = value;
			}
		}
		// model for database field CONTACT_MOBILE(string)
		public string CONTACT_MOBILE
		{
			get
			{
				return base.dtModel.Rows[0]["CONTACT_MOBILE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CONTACT_MOBILE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTACT_MOBILE"] = value;
			}
		}
		// model for database field CONTACT_EMAIL(string)
		public string CONTACT_EMAIL
		{
			get
			{
				return base.dtModel.Rows[0]["CONTACT_EMAIL"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CONTACT_EMAIL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTACT_EMAIL"] = value;
			}
		}
		// model for database field CONTACT_PAGER(string)
		public string CONTACT_PAGER
		{
			get
			{
				return base.dtModel.Rows[0]["CONTACT_PAGER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CONTACT_PAGER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTACT_PAGER"] = value;
			}
		}
		// model for database field CONTACT_HOME_PHONE(string)
		public string CONTACT_HOME_PHONE
		{
			get
			{
				return base.dtModel.Rows[0]["CONTACT_HOME_PHONE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CONTACT_HOME_PHONE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTACT_HOME_PHONE"] = value;
			}
		}
		// model for database field CONTACT_TOLL_FREE(string)
		public string CONTACT_TOLL_FREE
		{
			get
			{
				return base.dtModel.Rows[0]["CONTACT_TOLL_FREE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CONTACT_TOLL_FREE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTACT_TOLL_FREE"] = value;
			}
		}
		// model for database field CONTACT_NOTE(string)
		public string CONTACT_NOTE
		{
			get
			{
				return base.dtModel.Rows[0]["CONTACT_NOTE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CONTACT_NOTE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTACT_NOTE"] = value;
			}
		}
		// model for database field CONTACT_AGENCY_ID(int)
		public int CONTACT_AGENCY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CONTACT_AGENCY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CONTACT_AGENCY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CONTACT_AGENCY_ID"] = value;
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
        public string CPF_CNPJ
        {
            get
            {
                return base.dtModel.Rows[0]["CPF_CNPJ"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CPF_CNPJ"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CPF_CNPJ"] = value;
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

        /// model for database field REGIONAL_ID_TYPE(string)
        /// </summary>
        public string REGIONAL_ID_TYPE
        {
            get
            {
                return base.dtModel.Rows[0]["REGIONAL_ID_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["REGIONAL_ID_TYPE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["REGIONAL_ID_TYPE"] = value;
            }
        }

        public string NATIONALITY
        {
            get
            {
                return base.dtModel.Rows[0]["NATIONALITY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["NATIONALITY"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["NATIONALITY"] = value;
            }
        }

//		// model for database field IS_ACTIVE(string)
//		public string IS_ACTIVE
//		{
//			get
//			{
//				return base.dtModel.Rows[0]["IS_ACTIVE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IS_ACTIVE"].ToString();
//			}
//			set
//			{
//				base.dtModel.Rows[0]["IS_ACTIVE"] = value;
//			}
//		}
//		// model for database field ATTACH_POLICY_ID(int)
//		public int CREATED_BY 
//		{
//			get
//			{
//				return base.dtModel.Rows[0]["CREATED_BY "] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CREATED_BY "].ToString());
//			}
//			set
//			{
//				base.dtModel.Rows[0]["CREATED_BY "] = value;
//			}
//		}
//		// model for database field MODIFIED_BY (int)
//		public int MODIFIED_BY 
//		{
//			get
//			{
//				return base.dtModel.Rows[0]["MODIFIED_BY "] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["MODIFIED_BY "].ToString());
//			}
//			set
//			{
//				base.dtModel.Rows[0]["MODIFIED_BY "] = value;
//			}
//		}
		
		// model for database field CREATED_DATETIME(DateTime)
//		public DateTime CREATED_DATETIME
//		{
//			get
//			{
//				return base.dtModel.Rows[0]["CREATED_DATETIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["CREATED_DATETIME"].ToString());
//			}
//			set
//			{
//				base.dtModel.Rows[0]["CREATED_DATETIME"] = value;
//			}
//		}
		// model for database field LAST_UPDATED_DATETIME(DateTime)
//		public DateTime LAST_UPDATED_DATETIME
//		{
//			get
//			{
//				return base.dtModel.Rows[0]["LAST_UPDATED_DATETIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["LAST_UPDATED_DATETIME"].ToString());
//			}
//			set
//			{
//				base.dtModel.Rows[0]["LAST_UPDATED_DATETIME"] = value;
//			}
//		}

		#endregion	

	}
}
