using System;
using Cms.Model;
using System.Data;

namespace Cms.Model.Application
{
	/// <summary>
	/// Summary description for ClsApplicantInsuredInfo.
	/// </summary>
	public class ClsApplicantInsuredInfo : Cms.Model.ClsCommonModel
	{
		public ClsApplicantInsuredInfo()
		{
			base.dtModel.TableName = "APP_APPLICANT_LIST";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table CLT_CUSTOMER_NOTES
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}		
		
		private void AddColumns()
		{
			base.dtModel.Columns.Add("APPLICANT_ID",typeof(int));
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("APP_ID",typeof(int));
			base.dtModel.Columns.Add("APP_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("IS_PRIMARY_APPLICANT",typeof(int));
			
			
		}
		public int APPLICANT_ID
		{
			get
			{
				return this.GetInt(dtModel.Rows[0]["APPLICANT_ID"]);
			}
			set
			{
				dtModel.Rows[0]["APPLICANT_ID"] = value;
			}
		}

		public int CUSTOMER_ID
		{
			get
			{
				return this.GetInt(dtModel.Rows[0]["CUSTOMER_ID"]);
			}
			set
			{
				dtModel.Rows[0]["CUSTOMER_ID"] = value;
			}
		}

		public int APP_ID
		{
			get
			{
				return this.GetInt(dtModel.Rows[0]["APP_ID"]);
			}
			set
			{
				dtModel.Rows[0]["APP_ID"] = value;
			}
		}

		public int APP_VERSION_ID
		{
			get
			{
				return this.GetInt(dtModel.Rows[0]["APP_VERSION_ID"]);
			}
			set
			{
				
				dtModel.Rows[0]["APP_VERSION_ID"] = value;
			}
		}
		public int IS_PRIMARY_APPLICANT
		{
			get
			{
				return this.GetInt(dtModel.Rows[0]["IS_PRIMARY_APPLICANT"]);
			}
			set
			{
				dtModel.Rows[0]["IS_PRIMARY_APPLICANT"] = value;
			}
		}		
	}
}

