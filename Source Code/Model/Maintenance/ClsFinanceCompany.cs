using System;

namespace Cms.Model.Maintenance
{
	/// <summary>
	/// Holds the model for Finance Company tab(MNT_FINANCE_COMPANY_LIST)
	/// </summary>
	public class ClsFinanceCompany:Cms.Model.ClsCommonModel
	{
		public ClsFinanceCompany()
		{
			dtModel.TableName = "MNT_FINANCE_COMPANY_LIST";
			AddColumns();							// add columns of the database table CLT_CUSTOMER_LIST
			dtModel.Rows.Add(dtModel.NewRow());	// add a blank row in the datatable
		}

		#region Add columns
		//Adding columns to our datatable
		private void AddColumns()
		{
			
			dtModel.Columns.Add("COMPANY_ID",typeof(int));
			dtModel.Columns.Add("COMPANY_NAME",typeof(string));
			dtModel.Columns.Add("COMPANY_CODE",typeof(string));
			dtModel.Columns.Add("COMPANY_ADD1",typeof(string));
			dtModel.Columns.Add("COMPANY_ADD2",typeof(string));
			dtModel.Columns.Add("COMPANY_CITY",typeof(string));
			dtModel.Columns.Add("COMPANY_COUNTRY",typeof(string));
			dtModel.Columns.Add("COMPANY_STATE",typeof(string));
			dtModel.Columns.Add("COMPANY_ZIP",typeof(string));
			dtModel.Columns.Add("COMPANY_MAIN_PHONE_NO",typeof(string));
			dtModel.Columns.Add("COMPANY_TOLL_FREE_NO",typeof(string));
			dtModel.Columns.Add("COMPANY_EXT",typeof(string));
			dtModel.Columns.Add("COMPANY_FAX",typeof(string));
			dtModel.Columns.Add("COMPANY_EMAIL",typeof(string));
			dtModel.Columns.Add("COMPANY_WEBSITE",typeof(string));
			dtModel.Columns.Add("COMPANY_MOBILE",typeof(string));
			dtModel.Columns.Add("COMPANY_TERMS",typeof(string));
			dtModel.Columns.Add("COMPANY_TERMS_DESC",typeof(string));
			dtModel.Columns.Add("COMPANY_NOTE",typeof(string));

		}
		#endregion

		#region Database schema details

		// model for database field CUSTOMER_ID(int)
		public int CompanyId
		{
			get
			{
				return dtModel.Rows[0]["COMPANY_ID"] == DBNull.Value ? Convert.ToInt32(null) : Convert.ToInt32(dtModel.Rows[0]["COMPANY_ID"]);
			}
			set
			{
				dtModel.Rows[0]["COMPANY_ID"] = value;
			}
		}


		// model for database field CUSTOMER_NAME(int)
		public string CompanyName
		{
			get
			{
				return dtModel.Rows[0]["COMPANY_NAME"] == DBNull.Value ? Convert.ToString(null) : Convert.ToString(dtModel.Rows[0]["COMPANY_NAME"]);
			}
			set
			{
				dtModel.Rows[0]["COMPANY_NAME"] = value;
			}
		}

		// model for database field CUSTOMER_NAME(int)
		public string CompanyCode
		{
			get
			{
				return dtModel.Rows[0]["COMPANY_CODE"] == DBNull.Value ? Convert.ToString(null) : Convert.ToString(dtModel.Rows[0]["COMPANY_CODE"]);
			}
			set
			{
				dtModel.Rows[0]["COMPANY_CODE"] = value;
			}
		}

		// model for database field CUSTOMER_NAME(int)
		public string CompanyAdd1
		{
			get
			{
				return dtModel.Rows[0]["COMPANY_ADD1"] == DBNull.Value ? Convert.ToString(null) : Convert.ToString(dtModel.Rows[0]["COMPANY_ADD1"]);
			}
			set
			{
				dtModel.Rows[0]["COMPANY_ADD1"] = value;
			}
		}

		// model for database field CUSTOMER_NAME(int)
		public string CompanyAdd2
		{
			get
			{
				return dtModel.Rows[0]["COMPANY_ADD2"] == DBNull.Value ? Convert.ToString(null) : Convert.ToString(dtModel.Rows[0]["COMPANY_ADD2"]);
			}
			set
			{
				dtModel.Rows[0]["COMPANY_ADD2"] = value;
			}
		}

		// model for database field CUSTOMER_NAME(int)
		public string CompanyCity
		{
			get
			{
				return dtModel.Rows[0]["COMPANY_CITY"] == DBNull.Value ? Convert.ToString(null) : Convert.ToString(dtModel.Rows[0]["COMPANY_CITY"]);
			}
			set
			{
				dtModel.Rows[0]["COMPANY_CITY"] = value;
			}
		}

		// model for database field CUSTOMER_NAME(int)
		public string CompanyCountry
		{
			get
			{
				return dtModel.Rows[0]["COMPANY_COUNTRY"] == DBNull.Value ? Convert.ToString(null) : Convert.ToString(dtModel.Rows[0]["COMPANY_COUNTRY"]);
			}
			set
			{
				dtModel.Rows[0]["COMPANY_COUNTRY"] = value;
			}
		}

		// model for database field CUSTOMER_NAME(int)
		public string CompanyState
		{
			get
			{
				return dtModel.Rows[0]["COMPANY_STATE"] == DBNull.Value ? Convert.ToString(null) : Convert.ToString(dtModel.Rows[0]["COMPANY_STATE"]);
			}
			set
			{
				dtModel.Rows[0]["COMPANY_STATE"] = value;
			}
		}

		// model for database field CUSTOMER_NAME(int)
		public string CompanyZip
		{
			get
			{
				return dtModel.Rows[0]["COMPANY_ZIP"] == DBNull.Value ? Convert.ToString(null) : Convert.ToString(dtModel.Rows[0]["COMPANY_ZIP"]);
			}
			set
			{
				dtModel.Rows[0]["COMPANY_ZIP"] = value;
			}
		}

		// model for database field CUSTOMER_NAME(int)
		public string CompanyPhoneNo
		{
			get
			{
				return dtModel.Rows[0]["COMPANY_MAIN_PHONE_NO"] == DBNull.Value ? Convert.ToString(null) : Convert.ToString(dtModel.Rows[0]["COMPANY_MAIN_PHONE_NO"]);
			}
			set
			{
				dtModel.Rows[0]["COMPANY_MAIN_PHONE_NO"] = value;
			}
		}

		// model for database field CUSTOMER_NAME(int)
		public string CompanyTollFreeNo
		{
			get
			{
				return dtModel.Rows[0]["COMPANY_TOLL_FREE_NO"] == DBNull.Value ? Convert.ToString(null) : Convert.ToString(dtModel.Rows[0]["COMPANY_TOLL_FREE_NO"]);
			}
			set
			{
				dtModel.Rows[0]["COMPANY_TOLL_FREE_NO"] = value;
			}
		}

		// model for database field CUSTOMER_NAME(int)
		public string CompanyExt
		{
			get
			{
				return dtModel.Rows[0]["COMPANY_EXT"] == DBNull.Value ? Convert.ToString(null) : Convert.ToString(dtModel.Rows[0]["COMPANY_EXT"]);
			}
			set
			{
				dtModel.Rows[0]["COMPANY_EXT"] = value;
			}
		}

		// model for database field CUSTOMER_NAME(int)
		public string CompanyFax
		{
			get
			{
				return dtModel.Rows[0]["COMPANY_FAX"] == DBNull.Value ? Convert.ToString(null) : Convert.ToString(dtModel.Rows[0]["COMPANY_FAX"]);
			}
			set
			{
				dtModel.Rows[0]["COMPANY_FAX"] = value;
			}
		}

		// model for database field CUSTOMER_NAME(int)
		public string CompanyEmail
		{
			get
			{
				return dtModel.Rows[0]["COMPANY_EMAIL"] == DBNull.Value ? Convert.ToString(null) : Convert.ToString(dtModel.Rows[0]["COMPANY_EMAIL"]);
			}
			set
			{
				dtModel.Rows[0]["COMPANY_EMAIL"] = value;
			}
		}

		// model for database field CUSTOMER_NAME(int)
		public string CompanyWebsite
		{
			get
			{
				return dtModel.Rows[0]["COMPANY_WEBSITE"] == DBNull.Value ? Convert.ToString(null) : Convert.ToString(dtModel.Rows[0]["COMPANY_WEBSITE"]);
			}
			set
			{
				dtModel.Rows[0]["COMPANY_WEBSITE"] = value;
			}
		}

		// model for database field CUSTOMER_NAME(int)
		public string CompanyMobile
		{
			get
			{
				return dtModel.Rows[0]["COMPANY_MOBILE"] == DBNull.Value ? Convert.ToString(null) : Convert.ToString(dtModel.Rows[0]["COMPANY_MOBILE"]);
			}
			set
			{
				dtModel.Rows[0]["COMPANY_MOBILE"] = value;
			}
		}

		// model for database field CUSTOMER_NAME(int)
		public string CompanyTerms
		{
			get
			{
				return dtModel.Rows[0]["COMPANY_TERMS"] == DBNull.Value ? Convert.ToString(null) : Convert.ToString(dtModel.Rows[0]["COMPANY_TERMS"]);
			}
			set
			{
				dtModel.Rows[0]["COMPANY_TERMS"] = value;
			}
		}

		// model for database field CUSTOMER_NAME(int)
		public string CompanyTermsDescription
		{
			get
			{
				return dtModel.Rows[0]["COMPANY_TERMS_DESC"] == DBNull.Value ? Convert.ToString(null) : Convert.ToString(dtModel.Rows[0]["COMPANY_TERMS_DESC"]);
			}
			set
			{
				dtModel.Rows[0]["COMPANY_TERMS_DESC"] = value;
			}
		}

		// model for database field CUSTOMER_NAME(int)
		public string CompanyNote
		{
			get
			{
				return dtModel.Rows[0]["COMPANY_NOTE"] == DBNull.Value ? Convert.ToString(null) : Convert.ToString(dtModel.Rows[0]["COMPANY_NOTE"]);
			}
			set
			{
				dtModel.Rows[0]["COMPANY_NOTE"] = value;
			}
		}
		#endregion
	}
}
