/******************************************************************************************
<Author					: - Praveen kasana
<Start Date				: -	4 march 2008 3:48:17 PM
<End Date				: -	
<Description			: - MODEL CLASS FOR FORM 1099 SCREEN.
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
namespace Cms.Model.Account
{
	/// <summary>
	/// Database Model for ACT_CURRENT_DEPOSITS.
	/// </summary>
	public class ClsForm1099 : Cms.Model.ClsCommonModel
	{
		private const string FORM_1099 = "FORM_1099";
		public ClsForm1099()
		{
			base.dtModel.TableName = "FORM_1099";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table FORM_1099
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("FORM_1099_ID",typeof(int));
			base.dtModel.Columns.Add("ENTITY_ID",typeof(int));
			base.dtModel.Columns.Add("ENTITY_TYPE",typeof(string));
			base.dtModel.Columns.Add("PAYORS_NAME",typeof(string));
			base.dtModel.Columns.Add("STREET_ADDRESS",typeof(string));
			base.dtModel.Columns.Add("CITY",typeof(string));
			base.dtModel.Columns.Add("STATE",typeof(string));
			base.dtModel.Columns.Add("ZIP",typeof(string));
			base.dtModel.Columns.Add("TELEPHONE",typeof(string));
			base.dtModel.Columns.Add("RENTS",typeof(double));
			base.dtModel.Columns.Add("ROYALATIES",typeof(double));
			base.dtModel.Columns.Add("OTHERINCOME",typeof(double));
			base.dtModel.Columns.Add("FEDERAL_INCOME_TAXWITHHELD",typeof(double));
			base.dtModel.Columns.Add("FISHING_BOAT_PROCEEDS",typeof(double));
			base.dtModel.Columns.Add("MEDICAL_AND_HEALTH_CARE_PRODUCTS",typeof(double));
			base.dtModel.Columns.Add("NON_EMPLOYEMENT_COMPENSATION",typeof(double));
			base.dtModel.Columns.Add("SUBSTITUTE_PAYMENTS",typeof(double));
			base.dtModel.Columns.Add("PAYER_MADE_DIRECT_SALES",typeof(double));
			base.dtModel.Columns.Add("CROP_INSURANCE_PROCEED",typeof(double));
			base.dtModel.Columns.Add("EXCESS_GOLDEN_PARACHUTE_PAYMENTS",typeof(double));
			base.dtModel.Columns.Add("GROSS_PROCEEDS_PAID_TO_AN_ATTORNEY",typeof(double));
			base.dtModel.Columns.Add("STATE_TAX_WITHHELD",typeof(double));
			base.dtModel.Columns.Add("STATE_PAYER_STATE_NO",typeof(double));
			base.dtModel.Columns.Add("STATE_INCOME",typeof(double));
			base.dtModel.Columns.Add("PAYER_FEDERAL_IDENTIFICATION",typeof(string));
			base.dtModel.Columns.Add("RECIPIENT_IDENTIFICATION",typeof(string));
			base.dtModel.Columns.Add("RECIPIENT_NAME",typeof(string));
			base.dtModel.Columns.Add("RECIPIENT_STREET_ADDRESS1",typeof(string));
			base.dtModel.Columns.Add("RECIPIENT_STREET_ADDRESS2",typeof(string));
			base.dtModel.Columns.Add("RECIPIENT_CITY",typeof(string));
			base.dtModel.Columns.Add("RECIPIENT_STATE",typeof(string));
			base.dtModel.Columns.Add("RECIPIENT_ZIP",typeof(string));
			base.dtModel.Columns.Add("ACCOUNT_NO",typeof(string));
			base.dtModel.Columns.Add("FED_SSN_1099",typeof(string));
			
		}
		#region Database schema details
		
		
		// model for database field FORM_1099_ID(int)
		public int FORM_1099_ID
		{
			get
			{
				return base.dtModel.Rows[0]["FORM_1099_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["FORM_1099_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["FORM_1099_ID"] = value;
			}
		}

		public int ENTITY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ENTITY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ENTITY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ENTITY_ID"] = value;
			}
		}

		public string ENTITY_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["ENTITY_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ENTITY_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ENTITY_TYPE"] = value;
			}
		}
		
		
		// model for database field PAYORS_NAME(string)
		public string PAYORS_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["PAYORS_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PAYORS_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PAYORS_NAME"] = value;
			}
		}
		// model for database field STREET_ADDRESS(string)
		public string STREET_ADDRESS
		{
			get
			{
				return base.dtModel.Rows[0]["STREET_ADDRESS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["STREET_ADDRESS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["STREET_ADDRESS"] = value;
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

		public string STATE
		{
			get
			{
				return base.dtModel.Rows[0]["STATE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["STATE"] = value;
			}
		}

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
		// model for database field TELEPHONE(string)
		public string TELEPHONE
		{
			get
			{
				return base.dtModel.Rows[0]["TELEPHONE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["TELEPHONE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TELEPHONE"] = value;
			}
		}
		// model for database field RENTS(double)
		public double RENTS
		{
			get
			{
				return base.dtModel.Rows[0]["RENTS"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["RENTS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RENTS"] = value;
			}
		}		

		public double ROYALATIES
		{
			get
			{
				return base.dtModel.Rows[0]["ROYALATIES"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["ROYALATIES"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ROYALATIES"] = value;
			}
		}		

		public double OTHERINCOME
		{
			get
			{
				return base.dtModel.Rows[0]["OTHERINCOME"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["OTHERINCOME"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["OTHERINCOME"] = value;
			}
		}		

		public double FEDERAL_INCOME_TAXWITHHELD
		{
			get
			{
				return base.dtModel.Rows[0]["FEDERAL_INCOME_TAXWITHHELD"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["FEDERAL_INCOME_TAXWITHHELD"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["FEDERAL_INCOME_TAXWITHHELD"] = value;
			}
		}		
		

		public double FISHING_BOAT_PROCEEDS
		{
			get
			{
				return base.dtModel.Rows[0]["FISHING_BOAT_PROCEEDS"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["FISHING_BOAT_PROCEEDS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["FISHING_BOAT_PROCEEDS"] = value;
			}
		}

		public double MEDICAL_AND_HEALTH_CARE_PRODUCTS
		{
			get
			{
				return base.dtModel.Rows[0]["MEDICAL_AND_HEALTH_CARE_PRODUCTS"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["MEDICAL_AND_HEALTH_CARE_PRODUCTS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MEDICAL_AND_HEALTH_CARE_PRODUCTS"] = value;
			}
		}


		public double NON_EMPLOYEMENT_COMPENSATION
		{
			get
			{
				return base.dtModel.Rows[0]["NON_EMPLOYEMENT_COMPENSATION"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["NON_EMPLOYEMENT_COMPENSATION"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NON_EMPLOYEMENT_COMPENSATION"] = value;
			}
		}		
		

		public double SUBSTITUTE_PAYMENTS
		{
			get
			{
				return base.dtModel.Rows[0]["SUBSTITUTE_PAYMENTS"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["SUBSTITUTE_PAYMENTS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SUBSTITUTE_PAYMENTS"] = value;
			}
		}		
		

		public double PAYER_MADE_DIRECT_SALES
		{
			get
			{
				return base.dtModel.Rows[0]["PAYER_MADE_DIRECT_SALES"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["PAYER_MADE_DIRECT_SALES"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PAYER_MADE_DIRECT_SALES"] = value;
			}
		}		
		
		public double CROP_INSURANCE_PROCEED
		{
			get
			{
				return base.dtModel.Rows[0]["CROP_INSURANCE_PROCEED"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["CROP_INSURANCE_PROCEED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CROP_INSURANCE_PROCEED"] = value;
			}
		}

		public double EXCESS_GOLDEN_PARACHUTE_PAYMENTS
		{
			get
			{
				return base.dtModel.Rows[0]["EXCESS_GOLDEN_PARACHUTE_PAYMENTS"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["EXCESS_GOLDEN_PARACHUTE_PAYMENTS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EXCESS_GOLDEN_PARACHUTE_PAYMENTS"] = value;
			}
		}		

		

		public double STATE_TAX_WITHHELD
		{
			get
			{
				return base.dtModel.Rows[0]["STATE_TAX_WITHHELD"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["STATE_TAX_WITHHELD"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["STATE_TAX_WITHHELD"] = value;
			}
		}		
		

		public double STATE_PAYER_STATE_NO
		{
			get
			{
				return base.dtModel.Rows[0]["STATE_PAYER_STATE_NO"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["STATE_PAYER_STATE_NO"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["STATE_PAYER_STATE_NO"] = value;
			}
		}

		public double STATE_INCOME
		{
			get
			{
				return base.dtModel.Rows[0]["STATE_INCOME"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["STATE_INCOME"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["STATE_INCOME"] = value;
			}
		}		
		

		public string PAYER_FEDERAL_IDENTIFICATION
		{
			get
			{
				return base.dtModel.Rows[0]["PAYER_FEDERAL_IDENTIFICATION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PAYER_FEDERAL_IDENTIFICATION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PAYER_FEDERAL_IDENTIFICATION"] = value;
			}
		}


		public string RECIPIENT_IDENTIFICATION
		{
			get
			{
				return base.dtModel.Rows[0]["RECIPIENT_IDENTIFICATION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["RECIPIENT_IDENTIFICATION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["RECIPIENT_IDENTIFICATION"] = value;
			}
		}

		public string RECIPIENT_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["RECIPIENT_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["RECIPIENT_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["RECIPIENT_NAME"] = value;
			}
		}


		public string RECIPIENT_STREET_ADDRESS1
		{
			get
			{
				return base.dtModel.Rows[0]["RECIPIENT_STREET_ADDRESS1"] == DBNull.Value ? "" : base.dtModel.Rows[0]["RECIPIENT_STREET_ADDRESS1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["RECIPIENT_STREET_ADDRESS1"] = value;
			}
		}

		public string RECIPIENT_STREET_ADDRESS2
		{
			get
			{
				return base.dtModel.Rows[0]["RECIPIENT_STREET_ADDRESS2"] == DBNull.Value ? "" : base.dtModel.Rows[0]["RECIPIENT_STREET_ADDRESS2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["RECIPIENT_STREET_ADDRESS2"] = value;
			}
		}

		public string RECIPIENT_CITY
		{
			get
			{
				return base.dtModel.Rows[0]["RECIPIENT_CITY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["RECIPIENT_CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["RECIPIENT_CITY"] = value;
			}
		}

		public string RECIPIENT_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["RECIPIENT_STATE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["RECIPIENT_STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["RECIPIENT_STATE"] = value;
			}
		}


		public string RECIPIENT_ZIP
		{
			get
			{
				return base.dtModel.Rows[0]["RECIPIENT_ZIP"] == DBNull.Value ? "" : base.dtModel.Rows[0]["RECIPIENT_ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["RECIPIENT_ZIP"] = value;
			}
		}

		public string ACCOUNT_NO
		{
			get
			{
				return base.dtModel.Rows[0]["ACCOUNT_NO"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ACCOUNT_NO"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ACCOUNT_NO"] = value;
			}
		}

		public double GROSS_PROCEEDS_PAID_TO_AN_ATTORNEY
		{
			get
			{
				return base.dtModel.Rows[0]["GROSS_PROCEEDS_PAID_TO_AN_ATTORNEY"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["GROSS_PROCEEDS_PAID_TO_AN_ATTORNEY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["GROSS_PROCEEDS_PAID_TO_AN_ATTORNEY"] = value;
			}
		}		
           //Added By Raghav For Itrack Issue #4797
		public string FED_SSN_1099
		{
			get
			{
				
			    return base.dtModel.Rows[0]["FED_SSN_1099"] == DBNull.Value ? "" : base.dtModel.Rows[0]["FED_SSN_1099"].ToString(); 
			}
			set
			{
				base.dtModel.Rows[0]["FED_SSN_1099"] = value;
			}
		}
		

		

	#endregion
	}
}
