/******************************************************************************************
<Author					: -		Ajit Singh Chahal
<Start Date				: -		4/14/2005 4:06:36 PM
<End Date				: -	
<Description			: - 	Model For Vendor List.
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
	/// Database Model for MNT_VENDOR_LIST.
	/// </summary>
	public class ClsVendorInfo : Cms.Model.ClsCommonModel
	{
		private const string MNT_VENDOR_LIST = "MNT_VENDOR_LIST";
		public ClsVendorInfo()
		{
			base.dtModel.TableName = "MNT_VENDOR_LIST";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table MNT_VENDOR_LIST
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("VENDOR_ID",typeof(int));
			base.dtModel.Columns.Add("VENDOR_CODE",typeof(string));
			base.dtModel.Columns.Add("VENDOR_FNAME",typeof(string));
			base.dtModel.Columns.Add("VENDOR_LNAME",typeof(string));
			base.dtModel.Columns.Add("VENDOR_ADD1",typeof(string));
			base.dtModel.Columns.Add("VENDOR_ADD2",typeof(string));
			base.dtModel.Columns.Add("VENDOR_CITY",typeof(string));
			base.dtModel.Columns.Add("VENDOR_COUNTRY",typeof(string));
			base.dtModel.Columns.Add("VENDOR_STATE",typeof(string));
			base.dtModel.Columns.Add("VENDOR_ZIP",typeof(string));
			base.dtModel.Columns.Add("VENDOR_PHONE",typeof(string));
			base.dtModel.Columns.Add("VENDOR_EXT",typeof(string));
			base.dtModel.Columns.Add("VENDOR_FAX",typeof(string));
			base.dtModel.Columns.Add("VENDOR_MOBILE",typeof(string));
			base.dtModel.Columns.Add("VENDOR_EMAIL",typeof(string));
			base.dtModel.Columns.Add("VENDOR_SALUTATION",typeof(string));
			base.dtModel.Columns.Add("VENDOR_FEDERAL_NUM",typeof(string));
			base.dtModel.Columns.Add("VENDOR_NOTE",typeof(string));
			base.dtModel.Columns.Add("VENDOR_ACC_NUMBER",typeof(string));
			//Added By Pravesh
			base.dtModel.Columns.Add("BUSI_OWNERNAME",typeof(string));
			base.dtModel.Columns.Add("COMPANY_NAME",typeof(string));
			base.dtModel.Columns.Add("CHK_MAIL_ADD1",typeof(string));
			base.dtModel.Columns.Add("CHK_MAIL_ADD2",typeof(string));
			base.dtModel.Columns.Add("CHK_MAIL_CITY",typeof(string));
			base.dtModel.Columns.Add("CHK_MAIL_STATE",typeof(string));
			base.dtModel.Columns.Add("CHKCOUNTRY",typeof(string));
			base.dtModel.Columns.Add("CHK_MAIL_ZIP",typeof(string));
			base.dtModel.Columns.Add("MAIL_1099_ADD1",typeof(string));

			base.dtModel.Columns.Add("MAIL_1099_ADD2",typeof(string));
			base.dtModel.Columns.Add("MAIL_1099_CITY",typeof(string));
			base.dtModel.Columns.Add("MAIL_1099_STATE",typeof(string));
			base.dtModel.Columns.Add("MAIL_1099_COUNTRY",typeof(string));
			base.dtModel.Columns.Add("MAIL_1099_ZIP",typeof(string));
			base.dtModel.Columns.Add("MAIL_1099_NAME",typeof(string));
			base.dtModel.Columns.Add("PROCESS_1099_OPT",typeof(string));
			base.dtModel.Columns.Add("W9_FORM",typeof(string));

			base.dtModel.Columns.Add("ALLOWS_EFT",typeof(int));
			base.dtModel.Columns.Add("BANK_NAME",typeof(string));
			base.dtModel.Columns.Add("BANK_BRANCH",typeof(string));
			base.dtModel.Columns.Add("DFI_ACCOUNT_NUMBER",typeof(string));
			base.dtModel.Columns.Add("ROUTING_NUMBER",typeof(string));
			base.dtModel.Columns.Add("ACCOUNT_VERIFIED_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("ACCOUNT_ISVERIFIED",typeof(int));
			base.dtModel.Columns.Add("REVERIFIED_AC",typeof(int));
			base.dtModel.Columns.Add("REASON",typeof(char));
			base.dtModel.Columns.Add("ACCOUNT_TYPE",typeof(string));
			base.dtModel.Columns.Add("REQ_SPECIAL_HANDLING",typeof(int));   
            //added By chetna
            base.dtModel.Columns.Add("SUSEP_NUM", typeof(string));
            base.dtModel.Columns.Add("ACTIVITY", typeof(int));
            base.dtModel.Columns.Add("REGIONAL_IDENTIFICATION", typeof(string));
            base.dtModel.Columns.Add("REG_ID_ISSUE", typeof(string));
            base.dtModel.Columns.Add("CPF", typeof(string));
            base.dtModel.Columns.Add("DATE_OF_BIRTH", typeof(DateTime));
            base.dtModel.Columns.Add("REG_ID_ISSUE_DATE", typeof(DateTime));
			

			//end here
		}
		#region Database schema details
		// model for database field BUSI_OWNERNAME(string)
		public string BUSI_OWNERNAME
		{
			get
			{
				return base.dtModel.Rows[0]["BUSI_OWNERNAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["BUSI_OWNERNAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BUSI_OWNERNAME"] = value;
			}
		}
		// model for database field COMPANY_NAME(string)
		public string COMPANY_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["COMPANY_NAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["COMPANY_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["COMPANY_NAME"] = value;
			}
		}
		// model for database field CHK_MAIL_ADD1(string)
		public string CHK_MAIL_ADD1
		{
			get
			{
				return base.dtModel.Rows[0]["CHK_MAIL_ADD1"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CHK_MAIL_ADD1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CHK_MAIL_ADD1"] = value;
			}
		}
// model for database field CHK_MAIL_ADD2(string)
		public string CHK_MAIL_ADD2
		{
			get
			{
				return base.dtModel.Rows[0]["CHK_MAIL_ADD2"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CHK_MAIL_ADD2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CHK_MAIL_ADD2"] = value;
			}
		}
		// model for database field CHK_MAIL_CITY(string)
		public string CHK_MAIL_CITY
		{
			get
			{
				return base.dtModel.Rows[0]["CHK_MAIL_CITY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CHK_MAIL_CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CHK_MAIL_CITY"] = value;
			}
		}
		// model for database field CHK_MAIL_CITY(string)
		public string CHK_MAIL_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["CHK_MAIL_STATE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CHK_MAIL_STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CHK_MAIL_STATE"] = value;
			}
		}
		// model for database field CHKCOUNTRY(string)
		public string CHKCOUNTRY
		{
			get
			{
				return base.dtModel.Rows[0]["CHKCOUNTRY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CHKCOUNTRY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CHKCOUNTRY"] = value;
			}
		}
		// model for database field CHK_MAIL_ZIP(string)
		public string CHK_MAIL_ZIP
		{
			get
			{
				return base.dtModel.Rows[0]["CHK_MAIL_ZIP"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CHK_MAIL_ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CHK_MAIL_ZIP"] = value;
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
		// model for database field PROCESS_1099_OPT(string)
		public string PROCESS_1099_OPT
		{
			get
			{
				return base.dtModel.Rows[0]["PROCESS_1099_OPT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PROCESS_1099_OPT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PROCESS_1099_OPT"] = value;
			}
		}
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


        //added By chetna
        public string SUSEP_NUM
        {
            get
            {
                return base.dtModel.Rows[0]["SUSEP_NUM"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SUSEP_NUM"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["SUSEP_NUM"] = value;
            }
        }


		// model for database field VENDOR_ID(int)
		public int VENDOR_ID
		{
			get
			{
				return base.dtModel.Rows[0]["VENDOR_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["VENDOR_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["VENDOR_ID"] = value;
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
		// model for database field VENDOR_FNAME(string)
		public string VENDOR_FNAME
		{
			get
			{
				return base.dtModel.Rows[0]["VENDOR_FNAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["VENDOR_FNAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["VENDOR_FNAME"] = value;
			}
		}
		// model for database field VENDOR_LNAME(string)
		public string VENDOR_LNAME
		{
			get
			{
				return base.dtModel.Rows[0]["VENDOR_LNAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["VENDOR_LNAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["VENDOR_LNAME"] = value;
			}
		}
		// model for database field VENDOR_ADD1(string)
		public string VENDOR_ADD1
		{
			get
			{
				return base.dtModel.Rows[0]["VENDOR_ADD1"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["VENDOR_ADD1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["VENDOR_ADD1"] = value;
			}
		}
		// model for database field VENDOR_ADD2(string)
		public string VENDOR_ADD2
		{
			get
			{
				return base.dtModel.Rows[0]["VENDOR_ADD2"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["VENDOR_ADD2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["VENDOR_ADD2"] = value;
			}
		}
		// model for database field VENDOR_CITY(string)
		public string VENDOR_CITY
		{
			get
			{
				return base.dtModel.Rows[0]["VENDOR_CITY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["VENDOR_CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["VENDOR_CITY"] = value;
			}
		}
		// model for database field VENDOR_COUNTRY(string)
		public string VENDOR_COUNTRY
		{
			get
			{
				return base.dtModel.Rows[0]["VENDOR_COUNTRY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["VENDOR_COUNTRY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["VENDOR_COUNTRY"] = value;
			}
		}
		// model for database field VENDOR_STATE(string)
		public string VENDOR_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["VENDOR_STATE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["VENDOR_STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["VENDOR_STATE"] = value;
			}
		}
		// model for database field VENDOR_ZIP(string)
		public string VENDOR_ZIP
		{
			get
			{
				return base.dtModel.Rows[0]["VENDOR_ZIP"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["VENDOR_ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["VENDOR_ZIP"] = value;
			}
		}
		// model for database field VENDOR_PHONE(string)
		public string VENDOR_PHONE
		{
			get
			{
				return base.dtModel.Rows[0]["VENDOR_PHONE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["VENDOR_PHONE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["VENDOR_PHONE"] = value;
			}
		}
		// model for database field VENDOR_EXT(string)
		public string VENDOR_EXT
		{
			get
			{
				return base.dtModel.Rows[0]["VENDOR_EXT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["VENDOR_EXT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["VENDOR_EXT"] = value;
			}
		}
		// model for database field VENDOR_FAX(string)
		public string VENDOR_FAX
		{
			get
			{
				return base.dtModel.Rows[0]["VENDOR_FAX"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["VENDOR_FAX"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["VENDOR_FAX"] = value;
			}
		}
		// model for database field VENDOR_MOBILE(string)
		public string VENDOR_MOBILE
		{
			get
			{
				return base.dtModel.Rows[0]["VENDOR_MOBILE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["VENDOR_MOBILE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["VENDOR_MOBILE"] = value;
			}
		}
		// model for database field VENDOR_EMAIL(string)
		public string VENDOR_EMAIL
		{
			get
			{
				return base.dtModel.Rows[0]["VENDOR_EMAIL"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["VENDOR_EMAIL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["VENDOR_EMAIL"] = value;
			}
		}
		// model for database field VENDOR_SALUTATION(string)
		public string VENDOR_SALUTATION
		{
			get
			{
				return base.dtModel.Rows[0]["VENDOR_SALUTATION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["VENDOR_SALUTATION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["VENDOR_SALUTATION"] = value;
			}
		}
		// model for database field VENDOR_FEDERAL_NUM(string)
		public string VENDOR_FEDERAL_NUM
		{
			get
			{
				return base.dtModel.Rows[0]["VENDOR_FEDERAL_NUM"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["VENDOR_FEDERAL_NUM"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["VENDOR_FEDERAL_NUM"] = value;
			}
		}
		// model for database field VENDOR_NOTE(string)
		public string VENDOR_NOTE
		{
			get
			{
				return base.dtModel.Rows[0]["VENDOR_NOTE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["VENDOR_NOTE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["VENDOR_NOTE"] = value;
			}
		}
		// model for database field VENDOR_ACC_NUMBER(string)
		public string VENDOR_ACC_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["VENDOR_ACC_NUMBER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["VENDOR_ACC_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["VENDOR_ACC_NUMBER"] = value;
			}
		}
//Added by swarup
		// model for database field ALLOWS_EFT(string)
		public int ALLOWS_EFT
		{
			get
			{
				return base.dtModel.Rows[0]["ALLOWS_EFT"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["ALLOWS_EFT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ALLOWS_EFT"] = value;
			}
		}

		// model for database field BANK_NAME(string)
		public string BANK_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["BANK_NAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["BANK_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BANK_NAME"] = value;
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


		// model for database field BANK_NAME_2(string)
		public string BANK_NAME_2
		{
			get
			{
				return base.dtModel.Rows[0]["BANK_NAME_2"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["BANK_NAME_2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BANK_NAME_2"] = value;
			}
		}

		// model for database field BANK_BRANCH(string)
		public string BANK_BRANCH_2
		{
			get
			{
				return base.dtModel.Rows[0]["BANK_BRANCH_2"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["BANK_BRANCH_2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BANK_BRANCH_2"] = value;
			}
		}


		// model for database field DFI_ACCOUNT_NUMBER(string)
		public string DFI_ACCOUNT_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["DFI_ACCOUNT_NUMBER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DFI_ACCOUNT_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DFI_ACCOUNT_NUMBER"] = value;
			}
		}

		// model for database field ROUTING_NUMBER(string)
		public string ROUTING_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["ROUTING_NUMBER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ROUTING_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ROUTING_NUMBER"] = value;
			}
		}

		
		// model for database field REASON(string)
		public string REASON
		{
			get
			{
				return base.dtModel.Rows[0]["REASON"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["REASON"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REASON"] = value;
			}
		}
		// model for database field ACCOUNT_TYPE(string)
		public string ACCOUNT_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["ACCOUNT_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ACCOUNT_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ACCOUNT_TYPE"] = value;
			}
		}

		// model for database field ACCOUNT_TYPE_2(string)
		public string ACCOUNT_TYPE_2
		{
			get
			{
				return base.dtModel.Rows[0]["ACCOUNT_TYPE_2"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ACCOUNT_TYPE_2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ACCOUNT_TYPE_2"] = value;
			}
		}

		//Reverify Fields
		public int REVERIFIED_AC
		{
			get
			{
				return base.dtModel.Rows[0]["REVERIFIED_AC"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["REVERIFIED_AC"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REVERIFIED_AC"] = value;
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
		public int ACCOUNT_ISVERIFIED
		{
			get
			{
				return base.dtModel.Rows[0]["ACCOUNT_ISVERIFIED"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["ACCOUNT_ISVERIFIED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ACCOUNT_ISVERIFIED"] = value;
			}
		}
		public DateTime ACCOUNT_VERIFIED_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["ACCOUNT_VERIFIED_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["ACCOUNT_VERIFIED_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ACCOUNT_VERIFIED_DATE"] = value;
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
