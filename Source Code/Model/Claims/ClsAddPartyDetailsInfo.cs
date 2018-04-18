/******************************************************************************************
<Author				: -   Amar
<Start Date				: -	5/8/2006 4:59:10 PM
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
namespace Cms.Model.Claims
{
	/// <summary>
	/// Database Model for CLM_PARTIES.
	/// </summary>
	public class ClsAddPartyDetailsInfo : Cms.Model.ClsCommonModel
	{
		private const string CLM_PARTIES = "CLM_PARTIES";
		public ClsAddPartyDetailsInfo()
		{
			base.dtModel.TableName = "CLM_PARTIES";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table CLM_PARTIES
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("PARTY_ID",typeof(int));
			base.dtModel.Columns.Add("CLAIM_ID",typeof(int));
			base.dtModel.Columns.Add("NAME",typeof(string));
			base.dtModel.Columns.Add("ADDRESS1",typeof(string));
			base.dtModel.Columns.Add("ADDRESS2",typeof(string));
			base.dtModel.Columns.Add("CITY",typeof(string));
			base.dtModel.Columns.Add("STATE",typeof(int));
			base.dtModel.Columns.Add("ZIP",typeof(string));
			base.dtModel.Columns.Add("CONTACT_PHONE",typeof(string));
			base.dtModel.Columns.Add("CONTACT_EMAIL",typeof(string));
			base.dtModel.Columns.Add("OTHER_DETAILS",typeof(string));
			base.dtModel.Columns.Add("PARTY_TYPE_ID",typeof(int));
			base.dtModel.Columns.Add("COUNTRY",typeof(int));
			base.dtModel.Columns.Add("PARTY_DETAIL",typeof(int));
			base.dtModel.Columns.Add("AGE",typeof(int));
			base.dtModel.Columns.Add("EXTENT_OF_INJURY",typeof(string));
			base.dtModel.Columns.Add("REFERENCE",typeof(string));
			base.dtModel.Columns.Add("BANK_NAME",typeof(string));
            base.dtModel.Columns.Add("AGENCY_BANK", typeof(string));//Added by Santosh Gautam 15 Nov 2010
			base.dtModel.Columns.Add("ACCOUNT_NUMBER",typeof(string));
			base.dtModel.Columns.Add("ACCOUNT_NAME",typeof(string));
			base.dtModel.Columns.Add("CONTACT_PHONE_EXT",typeof(string));
			base.dtModel.Columns.Add("CONTACT_FAX",typeof(string));
			base.dtModel.Columns.Add("PARTY_TYPE_DESC",typeof(string));
			base.dtModel.Columns.Add("PARENT_ADJUSTER",typeof(int));
			base.dtModel.Columns.Add("FEDRERAL_ID",typeof(string));
			base.dtModel.Columns.Add("PROCESSING_OPTION_1099",typeof(int));
			base.dtModel.Columns.Add("MASTER_VENDOR_CODE",typeof(string));
			base.dtModel.Columns.Add("VENDOR_CODE",typeof(string));
			base.dtModel.Columns.Add("CONTACT_NAME",typeof(string));
			base.dtModel.Columns.Add("EXPERT_SERVICE_TYPE",typeof(int));
			base.dtModel.Columns.Add("EXPERT_SERVICE_TYPE_DESC",typeof(string));
			base.dtModel.Columns.Add("SUB_ADJUSTER_CONTACT_NAME",typeof(string));
			base.dtModel.Columns.Add("SA_ADDRESS1",typeof(string));
			base.dtModel.Columns.Add("SA_ADDRESS2",typeof(string));
			base.dtModel.Columns.Add("SA_CITY",typeof(string));
			base.dtModel.Columns.Add("SA_COUNTRY",typeof(string));
			base.dtModel.Columns.Add("SA_STATE",typeof(int));
			base.dtModel.Columns.Add("SA_ZIPCODE",typeof(string));
			base.dtModel.Columns.Add("SA_PHONE",typeof(string));
			base.dtModel.Columns.Add("SA_FAX",typeof(string));
			base.dtModel.Columns.Add("SUB_ADJUSTER",typeof(string));
			//
			base.dtModel.Columns.Add("PROP_DAMAGED_ID",typeof(int));
			base.dtModel.Columns.Add("MAIL_1099_ADD1",typeof(string));

			base.dtModel.Columns.Add("MAIL_1099_ADD2",typeof(string));
			base.dtModel.Columns.Add("MAIL_1099_CITY",typeof(string));
			base.dtModel.Columns.Add("MAIL_1099_STATE",typeof(string));
			base.dtModel.Columns.Add("MAIL_1099_COUNTRY",typeof(string));
			base.dtModel.Columns.Add("MAIL_1099_ZIP",typeof(string));

			base.dtModel.Columns.Add("MAIL_1099_NAME",typeof(string));
			base.dtModel.Columns.Add("W9_FORM",typeof(string));
            //base.dtModel.Columns.Add("W9_FORM", typeof(string));
            base.dtModel.Columns.Add("ACCOUNT_TYPE", typeof(int));
            base.dtModel.Columns.Add("DISTRICT", typeof(string));
            base.dtModel.Columns.Add("REGIONAL_ID", typeof(string));
            base.dtModel.Columns.Add("REGIONAL_ID_ISSUANCE", typeof(string));
            base.dtModel.Columns.Add("REGIONAL_ID_ISSUE_DATE", typeof(DateTime));
            base.dtModel.Columns.Add("MARITAL_STATUS", typeof(int));
            base.dtModel.Columns.Add("GENDER", typeof(int));
            base.dtModel.Columns.Add("BANK_NUMBER", typeof(string));
            base.dtModel.Columns.Add("BANK_BRANCH", typeof(string));
            base.dtModel.Columns.Add("PARTY_TYPE", typeof(int));
            base.dtModel.Columns.Add("PAYMENT_METHOD", typeof(int));

            base.dtModel.Columns.Add("PARTY_CPF_CNPJ", typeof(string));
            base.dtModel.Columns.Add("IS_BENEFICIARY", typeof(string));
            

		}
		#region Database schema details
		// model for database field PARTY_ID(int)
		public int PARTY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["PARTY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PARTY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PARTY_ID"] = value;
			}
		}

        public int ACCOUNT_TYPE
        {
            get
            {
                return base.dtModel.Rows[0]["ACCOUNT_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ACCOUNT_TYPE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["ACCOUNT_TYPE"] = value;
            }
        }
		// model for database field CLAIM_ID(int)
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
		// model for database field NAME(string)
		public string NAME
		{
			get
			{
				return base.dtModel.Rows[0]["NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["NAME"] = value;
			}
		}
		// model for database field ADDRESS1(string)
		public string ADDRESS1
		{
			get
			{
				return base.dtModel.Rows[0]["ADDRESS1"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ADDRESS1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ADDRESS1"] = value;
			}
		}
		// model for database field ADDRESS2(string)
		public string ADDRESS2
		{
			get
			{
				return base.dtModel.Rows[0]["ADDRESS2"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ADDRESS2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ADDRESS2"] = value;
			}
		}
		// model for database field CITY(string)
		public string CITY
		{
			get
			{
				return base.dtModel.Rows[0]["CITY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CITY"] = value;
			}
		}
		// model for database field STATE(int)
		public int STATE
		{
			get
			{
				return base.dtModel.Rows[0]["STATE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["STATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["STATE"] = value;
			}
		}
		// model for database field ZIP(string)
		public string ZIP
		{
			get
			{
				return base.dtModel.Rows[0]["ZIP"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ZIP"] = value;
			}
		}
		// model for database field CONTACT_PHONE(string)
		public string CONTACT_PHONE
		{
			get
			{
				return base.dtModel.Rows[0]["CONTACT_PHONE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CONTACT_PHONE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTACT_PHONE"] = value;
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
		// model for database field PARTY_TYPE_ID(int)
		public int PARTY_TYPE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["PARTY_TYPE_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PARTY_TYPE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PARTY_TYPE_ID"] = value;
			}
		}
		// model for database field COUNTRY(int)
		public int COUNTRY
		{
			get
			{
				return base.dtModel.Rows[0]["COUNTRY"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["COUNTRY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COUNTRY"] = value;
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
		// model for database field REFERENCE		(string)
		public string REFERENCE
		{
			get
			{
				return base.dtModel.Rows[0]["REFERENCE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REFERENCE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REFERENCE"] = value;
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

        //Added by Santosh Kumar Gautam 15 Nov 2010
        // model for database field AGENCY_BANK		(string)
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
		// model for database field CONTACT_PHONE_EXT		(string)
		public string CONTACT_PHONE_EXT
		{
			get
			{
				return base.dtModel.Rows[0]["CONTACT_PHONE_EXT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CONTACT_PHONE_EXT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTACT_PHONE_EXT"] = value;
			}
		}
		// model for database field CONTACT_FAX		(string)
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
		// model for database field PARTY_TYPE_DESC		(string)
		public string PARTY_TYPE_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["PARTY_TYPE_DESC"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PARTY_TYPE_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PARTY_TYPE_DESC"] = value;
			}
		}
		// model for database field PARENT_ADJUSTER(int)
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
		// model for database field FEDRERAL_ID		(string)
		public string FEDRERAL_ID
		{
			get
			{
				return base.dtModel.Rows[0]["FEDRERAL_ID"] == DBNull.Value ? "" : base.dtModel.Rows[0]["FEDRERAL_ID"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["FEDRERAL_ID"] = value;
			}
		}		
		// model for database field PROCESSING_OPTION_1099(int)
		public int PROCESSING_OPTION_1099
		{
			get
			{
				return base.dtModel.Rows[0]["PROCESSING_OPTION_1099"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["PROCESSING_OPTION_1099"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PROCESSING_OPTION_1099"] = value;
			}
		}		
		// model for database field MASTER_VENDOR_CODE(string)
		public string MASTER_VENDOR_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["MASTER_VENDOR_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MASTER_VENDOR_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MASTER_VENDOR_CODE"] = value;
			}
		}
		// model for database field VENDOR_CODE(string)
		public string VENDOR_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["VENDOR_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["VENDOR_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["VENDOR_CODE"] = value;
			}
		}		
		// model for database field CONTACT_NAME(string)
		public string CONTACT_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["CONTACT_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CONTACT_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTACT_NAME"] = value;
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

		// model for database field SUB_ADJUSTER(string)
		public string SUB_ADJUSTER
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_ADJUSTER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SUB_ADJUSTER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SUB_ADJUSTER"] = value;
			}
		}
		// model for database field SUB_ADJUSTER_CONTACT_NAME(string)
		public string SUB_ADJUSTER_CONTACT_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_ADJUSTER_CONTACT_NAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SUB_ADJUSTER_CONTACT_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SUB_ADJUSTER_CONTACT_NAME"] = value;
			}
		}
		// model for database field SA_ADDRESS1(string)
		public string SA_ADDRESS1
		{
			get
			{
				return base.dtModel.Rows[0]["SA_ADDRESS1"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SA_ADDRESS1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SA_ADDRESS1"] = value;
			}
		}
		// model for database field SA_ADDRESS2(string)
		public string SA_ADDRESS2
		{
			get
			{
				return base.dtModel.Rows[0]["SA_ADDRESS2"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SA_ADDRESS2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SA_ADDRESS2"] = value;
			}
		}
		// model for database field SA_CITY(string)
		public string SA_CITY
		{
			get
			{
				return base.dtModel.Rows[0]["SA_CITY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SA_CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SA_CITY"] = value;
			}
		}
		// model for database field SA_COUNTRY(string)
		public string SA_COUNTRY
		{
			get
			{
				return base.dtModel.Rows[0]["SA_COUNTRY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SA_COUNTRY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SA_COUNTRY"] = value;
			}
		}
		// model for database field SA_STATE(int)
		public int SA_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["SA_STATE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["SA_STATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SA_STATE"] = value;
			}
		}
		// model for database field SA_ZIPCODE(string)
		public string SA_ZIPCODE
		{
			get
			{
				return base.dtModel.Rows[0]["SA_ZIPCODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SA_ZIPCODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SA_ZIPCODE"] = value;
			}
		}
		// model for database field SA_PHONE(string)
		public string SA_PHONE
		{
			get
			{
				return base.dtModel.Rows[0]["SA_PHONE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SA_PHONE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SA_PHONE"] = value;
			}
		}
		// model for database field SA_FAX(string)
		public string SA_FAX
		{
			get
			{
				return base.dtModel.Rows[0]["SA_FAX"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SA_FAX"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SA_FAX"] = value;
			}
		}

		// model for database field PROP_DAMAGED_ID(int)
		public int PROP_DAMAGED_ID
		{
			get
			{
				return base.dtModel.Rows[0]["PROP_DAMAGED_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PROP_DAMAGED_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PROP_DAMAGED_ID"] = value;
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

        // model for database field DISTRICT(string)
        public string DISTRICT
        {
            get
            {
                return base.dtModel.Rows[0]["DISTRICT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DISTRICT"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["DISTRICT"] = value;
            }
        }

        // model for database field REGIONAL_ID(string)
		public string REGIONAL_ID
		{
			get
			{
				return base.dtModel.Rows[0]["REGIONAL_ID"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["REGIONAL_ID"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REGIONAL_ID"] = value;
			}
		}

        // model for database field REGIONAL_ID_ISSUANCE(string)
		public string REGIONAL_ID_ISSUANCE
		{
			get
			{
				return base.dtModel.Rows[0]["REGIONAL_ID_ISSUANCE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["REGIONAL_ID_ISSUANCE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REGIONAL_ID_ISSUANCE"] = value;
			}
		}
  
        // model for database field MARITAL_STATUS(int)
		public int MARITAL_STATUS
		{
			get
			{
				return base.dtModel.Rows[0]["MARITAL_STATUS"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["MARITAL_STATUS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MARITAL_STATUS"] = value;
			}
		} 
 
         // model for database field GENDER(int)
		public int GENDER
		{
			get
			{
				return base.dtModel.Rows[0]["GENDER"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["GENDER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["GENDER"] = value;
			}
		} 

         // model for database field BANK_NUMBER(string)
		public string BANK_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["BANK_NUMBER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["BANK_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BANK_NUMBER"] = value;
			}
		}

          // model for database field BANK_BRANCH(string)
		public string BANK_BRANCH
		{
			get
			{
				return base.dtModel.Rows[0]["BANK_BRANCH"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["BANK_BRANCH"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BANK_BRANCH"] = value;
			}
		}
 
         // model for database field REGIONAL_ID_ISSUE_DATE(DateTime)
		public DateTime REGIONAL_ID_ISSUE_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["REGIONAL_ID_ISSUE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["REGIONAL_ID_ISSUE_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REGIONAL_ID_ISSUE_DATE"] = value;
			}
		}

        // model for database field PARTY_TYPE(int)
        public int PARTY_TYPE
        {
            get
            {
                return base.dtModel.Rows[0]["PARTY_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PARTY_TYPE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["PARTY_TYPE"] = value;
            }
        }

        // model for database field PAYMENT_METHOD(int)
        public int PAYMENT_METHOD
        {
            get
            {
                return base.dtModel.Rows[0]["PAYMENT_METHOD"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PAYMENT_METHOD"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["PAYMENT_METHOD"] = value;
            }
        }

        // model for database field PARTY_CPF_CNPJ(string)
        public string PARTY_CPF_CNPJ
        {
            get
            {
                return base.dtModel.Rows[0]["PARTY_CPF_CNPJ"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PARTY_CPF_CNPJ"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["PARTY_CPF_CNPJ"] = value;
            }
        }

        public string IS_BENEFICIARY
        {
            get
            {
                return base.dtModel.Rows[0]["IS_BENEFICIARY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_BENEFICIARY"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["IS_BENEFICIARY"] = value;
            }
        }

		#endregion
	}
}
