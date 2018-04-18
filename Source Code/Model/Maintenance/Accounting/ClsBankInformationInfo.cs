/******************************************************************************************
<Author				: -   Ajit Singh Chahal
<Start Date				: -	5/25/2005 10:16:48 AM
<End Date				: -	
<Description				: - 	Model for Bank Information.
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

namespace Cms.Model.Maintenance.Accounting
{
	/// <summary>
	/// Database Model for ACT_BANK_INFORMATION.
	/// </summary>
	public class ClsBankInformationInfo: Cms.Model.ClsCommonModel
	{
	
		private const string ACT_BANK_INFORMATION = "ACT_BANK_INFORMATION";
		public ClsBankInformationInfo()
		{
			base.dtModel.TableName = "ACT_BANK_INFORMATION";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table ACT_BANK_INFORMATION
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("GL_ID",typeof(int));
			base.dtModel.Columns.Add("ACCOUNT_ID",typeof(int));
			base.dtModel.Columns.Add("BANK_NAME",typeof(string));
			base.dtModel.Columns.Add("BANK_ADDRESS1",typeof(string));
			base.dtModel.Columns.Add("BANK_ADDRESS2",typeof(string));
			base.dtModel.Columns.Add("BANK_CITY",typeof(string));
			base.dtModel.Columns.Add("BANK_COUNTRY",typeof(string));
			base.dtModel.Columns.Add("BANK_STATE",typeof(string));
			base.dtModel.Columns.Add("BANK_ZIP",typeof(string));
			base.dtModel.Columns.Add("BANK_ACC_TITLE",typeof(string));
			base.dtModel.Columns.Add("BANK_NUMBER",typeof(string));
			base.dtModel.Columns.Add("STARTING_DEPOSIT_NUMBER",typeof(int));
			base.dtModel.Columns.Add("IS_CHECK_ISSUED",typeof(string));
			base.dtModel.Columns.Add("CHECK_COUNTER",typeof(int));
			base.dtModel.Columns.Add("END_CHECK_NUMBER",typeof(int));
			base.dtModel.Columns.Add("START_CHECK_NUMBER",typeof(int));
			base.dtModel.Columns.Add("ROUTE_POSITION_CODE1",typeof(string));
			base.dtModel.Columns.Add("ROUTE_POSITION_CODE2",typeof(string));
			base.dtModel.Columns.Add("ROUTE_POSITION_CODE3",typeof(string));
			base.dtModel.Columns.Add("ROUTE_POSITION_CODE4",typeof(string));
			base.dtModel.Columns.Add("SIGN_FILE_1",typeof(string));
			base.dtModel.Columns.Add("SIGN_FILE_2",typeof(string));
			base.dtModel.Columns.Add("BANK_MICR_CODE",typeof(string));
			base.dtModel.Columns.Add("COMPANY_ID",typeof(string));
			base.dtModel.Columns.Add("TRANSIT_ROUTING_NUMBER",typeof(string));
            //Added by pradeep Kushwaha on 17-03-2010
            base.dtModel.Columns.Add("BANK_ID", typeof(int));
            base.dtModel.Columns.Add("NUMBER", typeof(string));
            base.dtModel.Columns.Add("REGISTERED", typeof(int));
            base.dtModel.Columns.Add("STARTING_OUR_NUMBER", typeof(string));
            base.dtModel.Columns.Add("ENDING_OUR_NUMBER", typeof(string));
            base.dtModel.Columns.Add("ACCOUNT_TYPE", typeof(int));
            base.dtModel.Columns.Add("BRANCH_NUMBER", typeof(string));
            base.dtModel.Columns.Add("AGREEMENT_NUMBER", typeof(string));
            base.dtModel.Columns.Add("ADD_NUMBER", typeof(string));
            base.dtModel.Columns.Add("BANK_TYPE", typeof(int)); //added by aditya for itrack # 1505 on 08-8-2011


		}
		#region Database schema details
		// model for database field GL_ID(int)
		public int GL_ID
		{
			get
			{
				return base.dtModel.Rows[0]["GL_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["GL_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["GL_ID"] = value;
			}
		}
		// model for database field ACCOUNT_ID(int)
		public int ACCOUNT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ACCOUNT_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ACCOUNT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ACCOUNT_ID"] = value;
			}
		}
		// model for database field BANK_NAME(string)
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
		// model for database field BANK_ADDRESS1(string)
		public string BANK_ADDRESS1
		{
			get
			{
				return base.dtModel.Rows[0]["BANK_ADDRESS1"] == DBNull.Value ? "" : base.dtModel.Rows[0]["BANK_ADDRESS1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BANK_ADDRESS1"] = value;
			}
		}
		// model for database field BANK_ADDRESS2(string)
		public string BANK_ADDRESS2
		{
			get
			{
				return base.dtModel.Rows[0]["BANK_ADDRESS2"] == DBNull.Value ? "" : base.dtModel.Rows[0]["BANK_ADDRESS2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BANK_ADDRESS2"] = value;
			}
		}
		// model for database field BANK_CITY(string)
		public string BANK_CITY
		{
			get
			{
				return base.dtModel.Rows[0]["BANK_CITY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["BANK_CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BANK_CITY"] = value;
			}
		}
		// model for database field BANK_COUNTRY(string)
		public string BANK_COUNTRY
		{
			get
			{
				return base.dtModel.Rows[0]["BANK_COUNTRY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["BANK_COUNTRY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BANK_COUNTRY"] = value;
			}
		}
		// model for database field BANK_STATE(string)
		public string BANK_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["BANK_STATE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["BANK_STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BANK_STATE"] = value;
			}
		}
		// model for database field BANK_ZIP(string)
		public string BANK_ZIP
		{
			get
			{
				return base.dtModel.Rows[0]["BANK_ZIP"] == DBNull.Value ? "" : base.dtModel.Rows[0]["BANK_ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BANK_ZIP"] = value;
			}
		}
		// model for database field BANK_ACC_TITLE(string)
		public string BANK_ACC_TITLE
		{
			get
			{
				return base.dtModel.Rows[0]["BANK_ACC_TITLE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["BANK_ACC_TITLE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BANK_ACC_TITLE"] = value;
			}
		}
		// model for database field BANK_NUMBER(string)
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
		// model for database field STARTING_DEPOSIT_NUMBER(int)
		public int STARTING_DEPOSIT_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["STARTING_DEPOSIT_NUMBER"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["STARTING_DEPOSIT_NUMBER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["STARTING_DEPOSIT_NUMBER"] = value;
			}
		}
		// model for database field IS_CHECK_ISSUED(string)
		public string IS_CHECK_ISSUED
		{
			get
			{
				return base.dtModel.Rows[0]["IS_CHECK_ISSUED"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IS_CHECK_ISSUED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_CHECK_ISSUED"] = value;
			}
		}
		// model for database field CHECK_COUNTER(int)
		public int CHECK_COUNTER
		{
			get
			{
				return base.dtModel.Rows[0]["CHECK_COUNTER"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CHECK_COUNTER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CHECK_COUNTER"] = value;
			}
		}
		// model for database field END_CHECK_NUMBER(int)
		public int END_CHECK_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["END_CHECK_NUMBER"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["END_CHECK_NUMBER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["END_CHECK_NUMBER"] = value;
			}
		}
		// model for database field START_CHECK_NUMBER(int)
		public int START_CHECK_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["START_CHECK_NUMBER"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["START_CHECK_NUMBER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["START_CHECK_NUMBER"] = value;
			}
		}
		public string ROUTE_POSITION_CODE1
		{
			get
			{
				return base.dtModel.Rows[0]["ROUTE_POSITION_CODE1"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ROUTE_POSITION_CODE1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ROUTE_POSITION_CODE1"] = value;
			}
		}
		public string ROUTE_POSITION_CODE2
		{
			get
			{
				return base.dtModel.Rows[0]["ROUTE_POSITION_CODE2"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ROUTE_POSITION_CODE2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ROUTE_POSITION_CODE2"] = value;
			}
		}
		public string ROUTE_POSITION_CODE3
		{
			get
			{
				return base.dtModel.Rows[0]["ROUTE_POSITION_CODE3"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ROUTE_POSITION_CODE3"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ROUTE_POSITION_CODE3"] = value;
			}
		}
		public string SIGN_FILE_1
		{
			get
			{
				return base.dtModel.Rows[0]["SIGN_FILE_1"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SIGN_FILE_1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SIGN_FILE_1"] = value;
			}
		}
		public string SIGN_FILE_2
		{
			get
			{
				return base.dtModel.Rows[0]["SIGN_FILE_2"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SIGN_FILE_2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SIGN_FILE_2"] = value;
			}
		}
		public string ROUTE_POSITION_CODE4
		{
			get
			{
				return base.dtModel.Rows[0]["ROUTE_POSITION_CODE4"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ROUTE_POSITION_CODE4"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ROUTE_POSITION_CODE4"] = value;
			}
		}
		public string BANK_MICR_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["BANK_MICR_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["BANK_MICR_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BANK_MICR_CODE"] = value;
			}
		}
		public string TRANSIT_ROUTING_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["TRANSIT_ROUTING_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["TRANSIT_ROUTING_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TRANSIT_ROUTING_NUMBER"] = value;
			}
		}
		public string COMPANY_ID
			{
				get
				{
					return base.dtModel.Rows[0]["COMPANY_ID"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["COMPANY_ID"].ToString();
				}
				set
				{
					base.dtModel.Rows[0]["COMPANY_ID"] = value;
				}
			}

        //Added by pradeep kushwaha on 17-03-2010

        // model for database field BANK_ID(int)
        public int BANK_ID
        {
            get
            {
                return base.dtModel.Rows[0]["BANK_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["BANK_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["BANK_ID"] = value;
            }
        }
        // model for database field NUMBER(string)
        public string NUMBER
        {
            get
            {
                return base.dtModel.Rows[0]["NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["NUMBER"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["NUMBER"] = value;
            }
        }
        // model for database field REGISTERED (int)
        public int REGISTERED
        {
            get
            {
                return base.dtModel.Rows[0]["REGISTERED"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["REGISTERED"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["REGISTERED"] = value;
            }
        }
        // model for database field STARTING_OUR_NUMBER(string)
        public string STARTING_OUR_NUMBER
        {
            get
            {
                return base.dtModel.Rows[0]["STARTING_OUR_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["STARTING_OUR_NUMBER"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["STARTING_OUR_NUMBER"] = value;
            }
        }
        // model for database field STARTING_OUR_NUMBER(string)
        public string ENDING_OUR_NUMBER
        {
            get
            {
                return base.dtModel.Rows[0]["ENDING_OUR_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ENDING_OUR_NUMBER"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["ENDING_OUR_NUMBER"] = value;
            }
        }

        // model for database field BANK_ACCOUNT_TYPE(INT)
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

        public int BANK_TYPE //added by aditya for itrack # 1505 on 08-8-2011
        {
            get
            {
                return base.dtModel.Rows[0]["BANK_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["BANK_TYPE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["BANK_TYPE"] = value;
            }
        }

        public string BRANCH_NUMBER
        {
            get
            {
                return base.dtModel.Rows[0]["BRANCH_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["BRANCH_NUMBER"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["BRANCH_NUMBER"] = value;
            }
        }
        public string AGREEMENT_NUMBER
        {
            get
            {
                return base.dtModel.Rows[0]["AGREEMENT_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["AGREEMENT_NUMBER"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["AGREEMENT_NUMBER"] = value;
            }
        }
        public string ADD_NUMBER
        {
            get
            {
                return base.dtModel.Rows[0]["ADD_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ADD_NUMBER"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["ADD_NUMBER"] = value;
            }
        }

		#endregion
	}
}
