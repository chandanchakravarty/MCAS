/******************************************************************************************
<Author				: -   Pradeep Iyer
<Start Date			: -	5/18/2005 4:59:12 PM
<End Date			: -	
<Description		: - 	Model class for Home Owner subjects of Insurance
<Review Date		: - 
<Reviewed By		: - 	

Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*******************************************************************************************/ 

using System;
using System.Data;
using Cms.Model;


namespace Cms.Model.Application.HomeOwners
{
	/// <summary>
	/// Database Model for APP_HOME_OWNER_SUB_INSU.
	/// </summary>
	public class ClsSubjectsInsuranceInfo : Cms.Model.ClsCommonModel
	{
		private const string APP_HOME_OWNER_SUB_INSU = "APP_HOME_OWNER_SUB_INSU";
		
		public ClsSubjectsInsuranceInfo()
		{
			base.dtModel.TableName = "APP_HOME_OWNER_SUB_INSU";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table APP_HOME_OWNER_SUB_INSU
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}

		private void AddColumns()
		{
			base.dtModel.Columns.Add("SUB_INSU_ID",typeof(int));
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("APP_ID",typeof(int));
			base.dtModel.Columns.Add("APP_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("IS_BLANKET_COV",typeof(string));
			base.dtModel.Columns.Add("BLANKET_APPLY_TO",typeof(string));
			base.dtModel.Columns.Add("LOCATION_ID",typeof(string));
			base.dtModel.Columns.Add("SUB_LOC_ID",typeof(string));
			base.dtModel.Columns.Add("SUBJECT_OF_INSURANCE",typeof(int));
			base.dtModel.Columns.Add("AMOUNT",typeof(double));
			base.dtModel.Columns.Add("OTHERS_DESC",typeof(string));
			base.dtModel.Columns.Add("DEDUCTIBLE",typeof(double));
			base.dtModel.Columns.Add("FORMS_CONDITIONS_APPLY",typeof(string));
			base.dtModel.Columns.Add("IS_PROPERTY_EVER_RENTED",typeof(string));
			base.dtModel.Columns.Add("KEEP_WHEN_NOT_IN_USE",typeof(string));
		}
		
		#region Database schema details
		// model for database field CUSTOMER_ID(int)
		
		public int SUB_INSU_ID
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_INSU_ID"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["SUB_INSU_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SUB_INSU_ID"] = value;
			}
		}

		public int CUSTOMER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_ID"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["CUSTOMER_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_ID"] = value;
			}
		}

		// model for database field APP_ID(int)
		public int APP_ID
		{
			get
			{
				return base.dtModel.Rows[0]["APP_ID"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["APP_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APP_ID"] = value;
			}
		}

		// model for database field APP_VERSION_ID(int)
		public int APP_VERSION_ID
		{
			get
			{
				return base.dtModel.Rows[0]["APP_VERSION_ID"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["APP_VERSION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APP_VERSION_ID"] = value;
			}
		}

		// model for database field IS_BLANKET_COV(string)
		public string IS_BLANKET_COV
		{
			get
			{
				return base.dtModel.Rows[0]["IS_BLANKET_COV"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_BLANKET_COV"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_BLANKET_COV"] = value;
			}
		}
		// model for database field BLANKET_APPLY_TO(string)
		public string BLANKET_APPLY_TO
		{
			get
			{
				return base.dtModel.Rows[0]["BLANKET_APPLY_TO"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["BLANKET_APPLY_TO"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BLANKET_APPLY_TO"] = value;
			}
		}
		// model for database field LOCATIONS(string)
		public int LOCATION_ID
		{
			get
			{
				return base.dtModel.Rows[0]["LOCATION_ID"] == DBNull.Value ? -1 : Convert.ToInt32(base.dtModel.Rows[0]["LOCATION_ID"]);
			}
			set
			{
				base.dtModel.Rows[0]["LOCATION_ID"] = value;
			}
		}
		// model for database field SUB_LOCATIONS(string)
		public int SUB_LOC_ID
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_LOC_ID"] == DBNull.Value ? -1 : Convert.ToInt32(base.dtModel.Rows[0]["SUB_LOC_ID"]);
			}
			set
			{
				base.dtModel.Rows[0]["SUB_LOC_ID"] = value;
			}
		}
		// model for database field SUBJECT_OF_INSURANCE(string)
		public int SUBJECT_OF_INSURANCE
		{
			get
			{
				return base.dtModel.Rows[0]["SUBJECT_OF_INSURANCE"] == DBNull.Value ? -1 : Convert.ToInt32(base.dtModel.Rows[0]["SUBJECT_OF_INSURANCE"]);
			}
			set
			{
				base.dtModel.Rows[0]["SUBJECT_OF_INSURANCE"] = value;
			}
		}
		// model for database field AMOUNT(double)
		public double AMOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["AMOUNT"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AMOUNT"] = value;
			}
		}
		// model for database field OTHERS_DESC(string)
		public string OTHERS_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["OTHERS_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["OTHERS_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["OTHERS_DESC"] = value;
			}
		}
		// model for database field DEDUCTIBLE(double)
		public double DEDUCTIBLE
		{
			get
			{
				return base.dtModel.Rows[0]["DEDUCTIBLE"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["DEDUCTIBLE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DEDUCTIBLE"] = value;
			}
		}
		// model for database field FORMS_CONDITIONS_APPLY(string)
		public string FORMS_CONDITIONS_APPLY
		{
			get
			{
				return base.dtModel.Rows[0]["FORMS_CONDITIONS_APPLY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["FORMS_CONDITIONS_APPLY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["FORMS_CONDITIONS_APPLY"] = value;
			}
		}
		// model for database field IS_PROPERTY_EVER_RENTED(string)
		public string IS_PROPERTY_EVER_RENTED
		{
			get
			{
				return base.dtModel.Rows[0]["IS_PROPERTY_EVER_RENTED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_PROPERTY_EVER_RENTED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_PROPERTY_EVER_RENTED"] = value;
			}
		}
		// model for database field KEEP_WHEN_NOT_IN_USE(string)
		public string KEEP_WHEN_NOT_IN_USE
		{
			get
			{
				return base.dtModel.Rows[0]["KEEP_WHEN_NOT_IN_USE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["KEEP_WHEN_NOT_IN_USE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["KEEP_WHEN_NOT_IN_USE"] = value;
			}
		}
		#endregion
	}

}
