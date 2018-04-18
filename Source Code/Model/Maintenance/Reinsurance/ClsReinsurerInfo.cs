/******************************************************************************************

<Author : - Priya

<Start Date : - 1/6/2006 5:37:20 PM

<End Date : - 

<Description : - 

<Review Date : - 

<Reviewed By : - 

Modification History

<Modified Date : - 

<Modified By : - 

<Purpose : - 

*******************************************************************************************/ 

using System;

using System.Data;

using Cms.Model;

namespace Cms.Model.Maintenance.Reinsurance

{

	/// <summary>

	/// Database Model for MNT_REIN_COMPANY_LIST.

	/// </summary>

	public class ClsReinsurerInfo : Cms.Model.ClsCommonModel

	{

		private const string MNT_REIN_COMPANY_LIST = "MNT_REIN_COMPANY_LIST";

		public ClsReinsurerInfo()

		{

			base.dtModel.TableName = "MNT_REIN_COMPANY_LIST"; // setting table name for data table that holds property values.

			this.AddColumns(); // add columns of the database table MNT_REIN_COMPANY_LIST

			base.dtModel.Rows.Add(base.dtModel.NewRow()); // add a blank row in the datatable

		}

		private void AddColumns()

		{

			base.dtModel.Columns.Add("REIN_COMAPANY_ID",typeof(int));
			base.dtModel.Columns.Add("REIN_COMAPANY_CODE",typeof(string));
			base.dtModel.Columns.Add("REIN_COMAPANY_NAME",typeof(string));
			base.dtModel.Columns.Add("REIN_COMPANY_TYPE",typeof(string));

			base.dtModel.Columns.Add("REIN_COMAPANY_ADD1",typeof(string));
			base.dtModel.Columns.Add("REIN_COMAPANY_ADD2",typeof(string));
			base.dtModel.Columns.Add("REIN_COMAPANY_CITY",typeof(string));
			base.dtModel.Columns.Add("REIN_COMAPANY_COUNTRY",typeof(string));
			base.dtModel.Columns.Add("REIN_COMAPANY_STATE",typeof(string));
			base.dtModel.Columns.Add("REIN_COMAPANY_ZIP",typeof(string));
			base.dtModel.Columns.Add("REIN_COMAPANY_PHONE",typeof(string));
			base.dtModel.Columns.Add("REIN_COMAPANY_EXT",typeof(string));
			base.dtModel.Columns.Add("REIN_COMAPANY_FAX",typeof(string));
			base.dtModel.Columns.Add("REIN_COMPANY_SPEED_DIAL",typeof(string));

			base.dtModel.Columns.Add("REIN_COMAPANY_MOBILE",typeof(string));
			base.dtModel.Columns.Add("REIN_COMAPANY_EMAIL",typeof(string));
			base.dtModel.Columns.Add("REIN_COMAPANY_NOTE",typeof(string));
			base.dtModel.Columns.Add("REIN_COMAPANY_ACC_NUMBER",typeof(string));

			base.dtModel.Columns.Add("M_REIN_COMPANY_ADD_1",typeof(string));
			base.dtModel.Columns.Add("M_RREIN_COMPANY_ADD_2",typeof(string));
			base.dtModel.Columns.Add("M_REIN_COMPANY_CITY",typeof(string));
			base.dtModel.Columns.Add("M_REIN_COMPANY_COUNTRY",typeof(string));
			base.dtModel.Columns.Add("M_REIN_COMPANY_STATE",typeof(string));
			base.dtModel.Columns.Add("M_REIN_COMPANY_ZIP",typeof(string));
			base.dtModel.Columns.Add("M_REIN_COMPANY_PHONE",typeof(string));
			base.dtModel.Columns.Add("M_REIN_COMPANY_FAX",typeof(string));
			base.dtModel.Columns.Add("M_REIN_COMPANY_EXT",typeof(string));

			base.dtModel.Columns.Add("REIN_COMPANY_WEBSITE",typeof(string));
			base.dtModel.Columns.Add("REIN_COMPANY_IS_BROKER",typeof(string));
			base.dtModel.Columns.Add("PRINCIPAL_CONTACT",typeof(string));
			base.dtModel.Columns.Add("OTHER_CONTACT",typeof(string));
			base.dtModel.Columns.Add("FEDERAL_ID",typeof(string));
			base.dtModel.Columns.Add("ROUTING_NUMBER",typeof(string));
			base.dtModel.Columns.Add("TERMINATION_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("TERMINATION_REASON",typeof(string));
			base.dtModel.Columns.Add("DOMICILED_STATE",typeof(string));
			base.dtModel.Columns.Add("NAIC_CODE",typeof(string));
			base.dtModel.Columns.Add("AM_BEST_RATING",typeof(string));
			base.dtModel.Columns.Add("EFFECTIVE_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("COMMENTS",typeof(string));
            //added By Chetna
            base.dtModel.Columns.Add("SUSEP_NUM", typeof(string));
            base.dtModel.Columns.Add("COM_TYPE", typeof(string));
            base.dtModel.Columns.Add("DISTRICT", typeof(string));
            base.dtModel.Columns.Add("BANK_NUMBER", typeof(string));
            base.dtModel.Columns.Add("BANK_BRANCH_NUMBER", typeof(string));
            base.dtModel.Columns.Add("CARRIER_CNPJ", typeof(string));
            base.dtModel.Columns.Add("BANK_ACCOUNT_TYPE", typeof(int));
            base.dtModel.Columns.Add("PAYMENT_METHOD", typeof(int));
            base.dtModel.Columns.Add("AGENCY_CLASSIFICATION",typeof(string));
            base.dtModel.Columns.Add("RISK_CLASSIFICATION",typeof(string));

		}

		#region Database schema details

		// model for database field REIN_COMPANY_ID(int)

		public int REIN_COMAPANY_ID

		{

			get

			{

				return base.dtModel.Rows[0]["REIN_COMAPANY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["REIN_COMAPANY_ID"].ToString());

			}

			set

			{

				base.dtModel.Rows[0]["REIN_COMAPANY_ID"] = value;

			}

		}

		// model for database field REIN_COMPANY_CODE(string)

		public string REIN_COMAPANY_CODE

		{

			get

			{

				return base.dtModel.Rows[0]["REIN_COMAPANY_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_COMAPANY_CODE"].ToString();

			}

			set

			{

				base.dtModel.Rows[0]["REIN_COMAPANY_CODE"] = value;

			}

		}

		// model for database field REIN_COMPANY_NAME(string)

		public string REIN_COMAPANY_NAME

		{

			get

			{

				return base.dtModel.Rows[0]["REIN_COMAPANY_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_COMAPANY_NAME"].ToString();

			}

			set

			{

				base.dtModel.Rows[0]["REIN_COMAPANY_NAME"] = value;

			}

		}
		
		// model for database field REIN_COMPANY_ADD1(string)
		public string REIN_COMPANY_TYPE

		{

			get

			{

				return base.dtModel.Rows[0]["REIN_COMPANY_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_COMPANY_TYPE"].ToString();

			}

			set

			{

				base.dtModel.Rows[0]["REIN_COMPANY_TYPE"] = value;

			}

		}
		
		public string REIN_COMAPANY_ADD1

		{

			get

			{

				return base.dtModel.Rows[0]["REIN_COMAPANY_ADD1"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_COMAPANY_ADD1"].ToString();

			}

			set

			{

				base.dtModel.Rows[0]["REIN_COMAPANY_ADD1"] = value;

			}

		}

		// model for database field REIN_COMPANY_ADD2(string)

		public string REIN_COMAPANY_ADD2

		{

			get

			{

				return base.dtModel.Rows[0]["REIN_COMAPANY_ADD2"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_COMAPANY_ADD2"].ToString();

			}

			set

			{

				base.dtModel.Rows[0]["REIN_COMAPANY_ADD2"] = value;

			}

		}

		// model for database field REIN_COMPANY_CITY(string)

		public string REIN_COMAPANY_CITY

		{

			get

			{

				return base.dtModel.Rows[0]["REIN_COMAPANY_CITY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_COMAPANY_CITY"].ToString();

			}

			set

			{

				base.dtModel.Rows[0]["REIN_COMAPANY_CITY"] = value;

			}

		}

		// model for database field REIN_COMPANY_COUNTRY(string)

		public string REIN_COMAPANY_COUNTRY

		{

			get

			{

				return base.dtModel.Rows[0]["REIN_COMAPANY_COUNTRY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_COMAPANY_COUNTRY"].ToString();

			}

			set

			{

				base.dtModel.Rows[0]["REIN_COMAPANY_COUNTRY"] = value;

			}

		}

		// model for database field REIN_COMPANY_STATE(string)

		public string REIN_COMAPANY_STATE

		{

			get

			{

				return base.dtModel.Rows[0]["REIN_COMAPANY_STATE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_COMAPANY_STATE"].ToString();

			}

			set

			{

				base.dtModel.Rows[0]["REIN_COMAPANY_STATE"] = value;

			}

		}

		// model for database field REIN_COMPANY_ZIP(string)

		public string REIN_COMAPANY_ZIP

		{

			get

			{

				return base.dtModel.Rows[0]["REIN_COMAPANY_ZIP"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_COMAPANY_ZIP"].ToString();

			}

			set

			{

				base.dtModel.Rows[0]["REIN_COMAPANY_ZIP"] = value;

			}

		}

		// model for database field REIN_COMPANY_PHONE(string)

		public string REIN_COMAPANY_PHONE

		{

			get

			{

				return base.dtModel.Rows[0]["REIN_COMAPANY_PHONE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_COMAPANY_PHONE"].ToString();

			}

			set

			{

				base.dtModel.Rows[0]["REIN_COMAPANY_PHONE"] = value;

			}

		}

		// model for database field REIN_COMPANY_EXT(string)

		public string REIN_COMAPANY_EXT

		{

			get

			{

				return base.dtModel.Rows[0]["REIN_COMAPANY_EXT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_COMAPANY_EXT"].ToString();

			}

			set

			{

				base.dtModel.Rows[0]["REIN_COMAPANY_EXT"] = value;

			}

		}

		// model for database field REIN_COMPANY_FAX(string)

		public string REIN_COMAPANY_FAX

		{

			get

			{

				return base.dtModel.Rows[0]["REIN_COMAPANY_FAX"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_COMAPANY_FAX"].ToString();

			}

			set

			{

				base.dtModel.Rows[0]["REIN_COMAPANY_FAX"] = value;

			}

		}

		// model for database field REIN_COMPANY_MOBILE(string)
		public string REIN_COMPANY_SPEED_DIAL

		{

			get

			{

				return base.dtModel.Rows[0]["REIN_COMPANY_SPEED_DIAL"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_COMPANY_SPEED_DIAL"].ToString();

			}

			set

			{

				base.dtModel.Rows[0]["REIN_COMPANY_SPEED_DIAL"] = value;

			}

		}

		public string REIN_COMAPANY_MOBILE

		{

			get

			{

				return base.dtModel.Rows[0]["REIN_COMAPANY_MOBILE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_COMAPANY_MOBILE"].ToString();

			}

			set

			{

				base.dtModel.Rows[0]["REIN_COMAPANY_MOBILE"] = value;

			}

		}

		// model for database field REIN_COMPANY_EMAIL(string)

		public string REIN_COMAPANY_EMAIL

		{

			get

			{

				return base.dtModel.Rows[0]["REIN_COMAPANY_EMAIL"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_COMAPANY_EMAIL"].ToString();

			}

			set

			{

				base.dtModel.Rows[0]["REIN_COMAPANY_EMAIL"] = value;

			}

		}
		
		// model for database field REIN_COMPANY_NOTE(string)

		public string REIN_COMAPANY_NOTE

		{

			get

			{

				return base.dtModel.Rows[0]["REIN_COMAPANY_NOTE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_COMAPANY_NOTE"].ToString();

			}

			set

			{

				base.dtModel.Rows[0]["REIN_COMAPANY_NOTE"] = value;

			}

		}

		// model for database field REIN_COMPANY_ACC_NUMBER(string)

		public string REIN_COMAPANY_ACC_NUMBER

		{

			get

			{

				return base.dtModel.Rows[0]["REIN_COMAPANY_ACC_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_COMAPANY_ACC_NUMBER"].ToString();

			}

			set

			{

				base.dtModel.Rows[0]["REIN_COMAPANY_ACC_NUMBER"] = value;

			}

		}

		// model for database field M_REIN_COMPANY_ADD_1(string)

		public string M_REIN_COMPANY_ADD_1

		{

			get

			{

				return base.dtModel.Rows[0]["M_REIN_COMPANY_ADD_1"] == DBNull.Value ? "" : base.dtModel.Rows[0]["M_REIN_COMPANY_ADD_1"].ToString();

			}

			set

			{

				base.dtModel.Rows[0]["M_REIN_COMPANY_ADD_1"] = value;

			}

		}

		// model for database field M_RREIN_COMPANY_ADD_2(string)

		public string M_RREIN_COMPANY_ADD_2

		{

			get

			{

				return base.dtModel.Rows[0]["M_RREIN_COMPANY_ADD_2"] == DBNull.Value ? "" : base.dtModel.Rows[0]["M_RREIN_COMPANY_ADD_2"].ToString();

			}

			set

			{

				base.dtModel.Rows[0]["M_RREIN_COMPANY_ADD_2"] = value;

			}

		}

		// model for database field M_REIN_COMPANY_CITY(string)

		public string M_REIN_COMPANY_CITY

		{

			get

			{

				return base.dtModel.Rows[0]["M_REIN_COMPANY_CITY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["M_REIN_COMPANY_CITY"].ToString();

			}

			set

			{

				base.dtModel.Rows[0]["M_REIN_COMPANY_CITY"] = value;

			}

		}

		// model for database field M_REIN_COMPANY_COUNTRY(string)

		public string M_REIN_COMPANY_COUNTRY

		{

			get

			{

				return base.dtModel.Rows[0]["M_REIN_COMPANY_COUNTRY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["M_REIN_COMPANY_COUNTRY"].ToString();

			}

			set

			{

				base.dtModel.Rows[0]["M_REIN_COMPANY_COUNTRY"] = value;

			}

		}

		// model for database field M_REIN_COMPANY_STATE(string)

		public string M_REIN_COMPANY_STATE

		{

			get

			{

				return base.dtModel.Rows[0]["M_REIN_COMPANY_STATE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["M_REIN_COMPANY_STATE"].ToString();

			}

			set

			{

				base.dtModel.Rows[0]["M_REIN_COMPANY_STATE"] = value;

			}

		}

		// model for database field M_REIN_COMPANY_ZIP(string)

		public string M_REIN_COMPANY_ZIP

		{

			get

			{

				return base.dtModel.Rows[0]["M_REIN_COMPANY_ZIP"] == DBNull.Value ? "" : base.dtModel.Rows[0]["M_REIN_COMPANY_ZIP"].ToString();

			}

			set

			{

				base.dtModel.Rows[0]["M_REIN_COMPANY_ZIP"] = value;

			}

		}

		// model for database field M_REIN_COMPANY_PHONE(string)

		public string M_REIN_COMPANY_PHONE

		{

			get

			{

				return base.dtModel.Rows[0]["M_REIN_COMPANY_PHONE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["M_REIN_COMPANY_PHONE"].ToString();

			}

			set

			{

				base.dtModel.Rows[0]["M_REIN_COMPANY_PHONE"] = value;

			}

		}

		// model for database field M_REIN_COMPANY_FAX(string)

		public string M_REIN_COMPANY_FAX

		{

			get

			{

				return base.dtModel.Rows[0]["M_REIN_COMPANY_FAX"] == DBNull.Value ? "" : base.dtModel.Rows[0]["M_REIN_COMPANY_FAX"].ToString();

			}

			set

			{

				base.dtModel.Rows[0]["M_REIN_COMPANY_FAX"] = value;

			}

		}

		// model for database field M_REIN_COMPANY_EXT(string)

		public string M_REIN_COMPANY_EXT

		{

			get

			{

				return base.dtModel.Rows[0]["M_REIN_COMPANY_EXT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["M_REIN_COMPANY_EXT"].ToString();

			}

			set

			{

				base.dtModel.Rows[0]["M_REIN_COMPANY_EXT"] = value;

			}

		}

	
		// model for database field REIN_COMPANY_WEBSITE(string)

		public string REIN_COMPANY_WEBSITE

		{

			get

			{

				return base.dtModel.Rows[0]["REIN_COMPANY_WEBSITE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_COMPANY_WEBSITE"].ToString();

			}

			set

			{

				base.dtModel.Rows[0]["REIN_COMPANY_WEBSITE"] = value;

			}

		}

		// model for database field REIN_COMPANY_IS_BROKER(string)

		public string REIN_COMPANY_IS_BROKER

		{

			get

			{

				return base.dtModel.Rows[0]["REIN_COMPANY_IS_BROKER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_COMPANY_IS_BROKER"].ToString();

			}

			set

			{

				base.dtModel.Rows[0]["REIN_COMPANY_IS_BROKER"] = value;

			}

		}

		// model for database field PRINCIPAL_CONTACT(string)

		public string PRINCIPAL_CONTACT

		{

			get

			{

				return base.dtModel.Rows[0]["PRINCIPAL_CONTACT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PRINCIPAL_CONTACT"].ToString();

			}

			set

			{

				base.dtModel.Rows[0]["PRINCIPAL_CONTACT"] = value;

			}

		}

		// model for database field OTHER_CONTACT(string)

		public string OTHER_CONTACT

		{

			get

			{

				return base.dtModel.Rows[0]["OTHER_CONTACT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["OTHER_CONTACT"].ToString();

			}

			set

			{

				base.dtModel.Rows[0]["OTHER_CONTACT"] = value;

			}

		}

		// model for database field FEDERAL_ID(string)

		public string FEDERAL_ID

		{

			get

			{

				return base.dtModel.Rows[0]["FEDERAL_ID"] == DBNull.Value ? "" : base.dtModel.Rows[0]["FEDERAL_ID"].ToString();

			}

			set

			{

				base.dtModel.Rows[0]["FEDERAL_ID"] = value;

			}

		}

		// model for database field ROUTING_NUMBER(string)

		public string ROUTING_NUMBER

		{

			get

			{

				return base.dtModel.Rows[0]["ROUTING_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ROUTING_NUMBER"].ToString();

			}

			set

			{

				base.dtModel.Rows[0]["ROUTING_NUMBER"] = value;

			}

		}

		// model for database field TERMINATION_DATE(DateTime)

		
		public DateTime TERMINATION_DATE

		{

			get

			{

				return base.dtModel.Rows[0]["TERMINATION_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["TERMINATION_DATE"].ToString());

			}

			set

			{

				base.dtModel.Rows[0]["TERMINATION_DATE"] = value;

			}

		}

		// model for database field TERMINATION_REASON(string)
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

		public string TERMINATION_REASON

		{

			get

			{

				return base.dtModel.Rows[0]["TERMINATION_REASON"] == DBNull.Value ? "" : base.dtModel.Rows[0]["TERMINATION_REASON"].ToString();

			}

			set

			{

				base.dtModel.Rows[0]["TERMINATION_REASON"] = value;

			}

		}
		public string COMMENTS

		{

			get

			{

				return base.dtModel.Rows[0]["COMMENTS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["COMMENTS"].ToString();

			}

			set

			{

				base.dtModel.Rows[0]["COMMENTS"] = value;

			}

		}
		
		public string NAIC_CODE

		{

			get

			{

				return base.dtModel.Rows[0]["NAIC_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["NAIC_CODE"].ToString();

			}

			set

			{

				base.dtModel.Rows[0]["NAIC_CODE"] = value;

			}

		}
		public string AM_BEST_RATING

		{

			get

			{

				return base.dtModel.Rows[0]["AM_BEST_RATING"] == DBNull.Value ? "" : base.dtModel.Rows[0]["AM_BEST_RATING"].ToString();

			}

			set

			{

				base.dtModel.Rows[0]["AM_BEST_RATING"] = value;

			}

		}
		public string DOMICILED_STATE

		{

			get

			{

				return base.dtModel.Rows[0]["DOMICILED_STATE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DOMICILED_STATE"].ToString();

			}

			set

			{

				base.dtModel.Rows[0]["DOMICILED_STATE"] = value;

			}

		}

        //added By Chetna
        public string SUSEP_NUM
        {

            get
            {

                return base.dtModel.Rows[0]["SUSEP_NUM"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SUSEP_NUM"].ToString();

            }

            set
            {

                base.dtModel.Rows[0]["SUSEP_NUM"] = value;

            }

        }

        public string COM_TYPE
        {

            get
            {

                return base.dtModel.Rows[0]["COM_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["COM_TYPE"].ToString();

            }

            set
            {

                base.dtModel.Rows[0]["COM_TYPE"] = value;

            }

        }
        public string DISTRICT
        {
            get
            {
                return base.dtModel.Rows[0]["DISTRICT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DISTRICT"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["DISTRICT"] = value;
            }
        }
        public string BANK_NUMBER
        {
            get
            {
                return base.dtModel.Rows[0]["BANK_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["BANK_NUMBER"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["BANK_NUMBER"] = value;
            }
        }
        public string BANK_BRANCH_NUMBER
        {
            get
            {
                return base.dtModel.Rows[0]["BANK_BRANCH_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["BANK_BRANCH_NUMBER"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["BANK_BRANCH_NUMBER"] = value;
            }
        }
        public string CARRIER_CNPJ
        {
            get
            {
                return base.dtModel.Rows[0]["CARRIER_CNPJ"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CARRIER_CNPJ"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CARRIER_CNPJ"] = value;
            }
        }
        public int BANK_ACCOUNT_TYPE
        {
            get
            {
                return base.dtModel.Rows[0]["BANK_ACCOUNT_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["BANK_ACCOUNT_TYPE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["BANK_ACCOUNT_TYPE"] = value;
            }
        }
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
        public string AGENCY_CLASSIFICATION
        {
            get
            {
                return base.dtModel.Rows[0]["AGENCY_CLASSIFICATION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["AGENCY_CLASSIFICATION"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["AGENCY_CLASSIFICATION"] = value;//ADDED BY ABHINAV
            }
        }
        public string RISK_CLASSIFICATION
        {
            get
            {
                return base.dtModel.Rows[0]["RISK_CLASSIFICATION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["RISK_CLASSIFICATION"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["RISK_CLASSIFICATION"] = value;//ADDED BY ABHINAV
            }
        }

		#endregion

	}

}
