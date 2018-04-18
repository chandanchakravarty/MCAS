/******************************************************************************************
<Author					: -	Pradeep
<Start Date				: -	28 Apr, 2005
<End Date				: -	
<Description			: - Model class for Auto Applicant
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*****************************************************************************************/

using System;
using Cms.Model;
using System.Data;

namespace Cms.Model.Application
{
	/// <summary>
	/// Summary description for ClsApplicantDetailsInfo.
	/// </summary>
	public class ClsApplicantDetailsInfo : Cms.Model.ClsCommonModel
	{
		public ClsApplicantDetailsInfo()
		{
			base.dtModel.TableName = "APP_AUTO_APPLICANT_DETAILS";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table CLT_CUSTOMER_NOTES
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("APP_ID",typeof(int));
			base.dtModel.Columns.Add("APP_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("CURR_RES_TYPE",typeof(string));
			base.dtModel.Columns.Add("PREV_ADD",typeof(string));
			base.dtModel.Columns.Add("IS_OTHER_THAN_INSURED",typeof(string));
			base.dtModel.Columns.Add("IS_OTHER_THAN_INSURED_YES",typeof(string));
			
			//<Gaurav > 1 June 2005 ; START: Following Fields Added ; BUG No:<535>
			
			base.dtModel.Columns.Add("COMPANY",typeof(string));
			base.dtModel.Columns.Add("POLICY_NO",typeof(string));
			base.dtModel.Columns.Add("EXP_DATE",typeof(DateTime));
			
			//<Gaurav > 1 June 2005 ; END: Following Fields Added ; BUG No:<535>
			
			//<Gaurav > 1 June 2005 ; START: Following Fields not required now ; BUG No:<535>
			/*
			base.dtModel.Columns.Add("YEARS_AT_CURR_RES",typeof(int));
			base.dtModel.Columns.Add("YEARS_AT_PREV_RES",typeof(int));
			*/
			//<Gaurav > 1 June 2005 ; END: Following Fields not required now ; BUG No:<535>
			
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
		
		public string CURR_RES_TYPE
		{
			get
			{
				return this.GetString(dtModel.Rows[0]["CURR_RES_TYPE"]);
			}
			set
			{
				dtModel.Rows[0]["CURR_RES_TYPE"] = value;
			}
		}

		public string PREV_ADD
		{
			get
			{
				return this.GetString(dtModel.Rows[0]["PREV_ADD"]);
			}
			set
			{
				dtModel.Rows[0]["PREV_ADD"] = value;
			}
		}
//<Gaurav > 1 June 2005 ; START: Following Fields Added ; BUG No:<535>
		public string COMPANY
		{
			get
			{
				return this.GetString(dtModel.Rows[0]["COMPANY"]);
			}
			set
			{
				dtModel.Rows[0]["COMPANY"] = value;
			}
		}

		public string POLICY_NO
		{
			get
			{
				return this.GetString(dtModel.Rows[0]["POLICY_NO"]);
			}
			set
			{
				dtModel.Rows[0]["POLICY_NO"] = value;
			}
		}
		public DateTime EXP_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["EXP_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["EXP_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EXP_DATE"] = value;
			}
		}
//<Gaurav > 1 June 2005 ; END: Following Fields Added ; BUG No:<535>
		public string IS_OTHER_THAN_INSURED
		{
			get
			{
				return this.GetString(dtModel.Rows[0]["IS_OTHER_THAN_INSURED"]);
			}
			set
			{
				dtModel.Rows[0]["IS_OTHER_THAN_INSURED"] = value;
			}
		}

		public string IS_OTHER_THAN_INSURED_YES
		{
			get
			{
				return this.GetString(dtModel.Rows[0]["IS_OTHER_THAN_INSURED_YES"]);
			}
			set
			{
				dtModel.Rows[0]["IS_OTHER_THAN_INSURED_YES"] = value;
			}
		}



		//<Gaurav > 1 June 2005 ; START: Following Fields not required now ; BUG No:<535>
		/*
		public int YEARS_AT_CURR_RES
		{
			get
			{
				return this.GetInt(dtModel.Rows[0]["YEARS_AT_CURR_RES"]);
			}
			set
			{
				dtModel.Rows[0]["YEARS_AT_CURR_RES"] = value;
			}
		}

		public int YEARS_AT_PREV_RES
		{
			get
			{
				return this.GetInt(dtModel.Rows[0]["YEARS_AT_PREV_RES"]);
			}
			set
			{
				dtModel.Rows[0]["YEARS_AT_PREV_RES"] = value;
			}
		}
		*/
//<Gaurav > 1 June 2005 ; END: Following Fields not required now ; BUG No:<535>
			

	}
}
