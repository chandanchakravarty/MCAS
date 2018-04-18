/******************************************************************************************
<Author					: -  Mohit Gupta 
<Start Date				: -	7/4/2005 12:45:49 PM
<End Date				: -	
<Description			: - 	
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - Ravindra Gupta
<Modified By			: - 05-08-2006
<Purpose				: - Added Field 
*******************************************************************************************/ 
using System;
using System.Data;
using Cms.Model;

namespace Cms.Model.Maintenance
{
	/// <summary>
	/// Summary description for ClsCoverageDetailsInfo.
	/// </summary>
	public class ClsCoverageDetailsInfo : Cms.Model.ClsCommonModel
	{
		private const string MNT_COVERAGE = "MNT_COVERAGE";
		public ClsCoverageDetailsInfo()
		{
			base.dtModel.TableName = "MNT_COVERAGE";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table MNT_COVERAGE
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("COV_ID",typeof(int));
			base.dtModel.Columns.Add("COV_REF_CODE",typeof(string));
			base.dtModel.Columns.Add("COV_CODE",typeof(string));
			base.dtModel.Columns.Add("COV_DES",typeof(string));
			base.dtModel.Columns.Add("STATE_ID",typeof(int));
			base.dtModel.Columns.Add("LOB_ID",typeof(int));
			base.dtModel.Columns.Add("IS_DEFAULT",typeof(bool));
			base.dtModel.Columns.Add("TYPE",typeof(int));
			base.dtModel.Columns.Add("PURPOSE",typeof(int));
			base.dtModel.Columns.Add("LIMIT_TYPE",typeof(int));
			base.dtModel.Columns.Add("DEDUCTIBLE_TYPE",typeof(int));
			base.dtModel.Columns.Add("IsLimitApplicable",typeof(int));
			base.dtModel.Columns.Add("IsDeductApplicable",typeof(int));
			base.dtModel.Columns.Add("IS_MANDATORY",typeof(int));
			base.dtModel.Columns.Add("INCLUDED",typeof(double));
			base.dtModel.Columns.Add("COVERAGE_TYPE",typeof(string));
			base.dtModel.Columns.Add("RANK",typeof(int));
			base.dtModel.Columns.Add("EFFECTIVE_FROM_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("EFFECTIVE_TO_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("DISABLED_DATE",typeof(DateTime));
			//by pravesh
			base.dtModel.Columns.Add("ADDDEDUCTIBLE_TYPE",typeof(int));
			base.dtModel.Columns.Add("ISADDDEDUCTIBLE_APP",typeof(int));
			//by swarup
			base.dtModel.Columns.Add("REINSURANCE_LOB",typeof(int));
			base.dtModel.Columns.Add("REINSURANCE_COV",typeof(int));
			base.dtModel.Columns.Add("ASLOB",typeof(int));
			base.dtModel.Columns.Add("REINSURANCE_CALC",typeof(int));
			base.dtModel.Columns.Add("FORM_NUMBER",typeof(string));
			base.dtModel.Columns.Add("REIN_REPORT_BUCK",typeof(int));

			base.dtModel.Columns.Add("COMM_VEHICLE",typeof(int));
			base.dtModel.Columns.Add("COMM_REIN_COV_CAT",typeof(int));
			base.dtModel.Columns.Add("REIN_ASLOB",typeof(int));
			base.dtModel.Columns.Add("COMM_CALC",typeof(int));
			base.dtModel.Columns.Add("REIN_REPORT_BUCK_COMM",typeof(int));
			base.dtModel.Columns.Add("IS_SYSTEM_GENERAED",typeof(string));
            base.dtModel.Columns.Add("MANDATORY_DATE", typeof(DateTime));
            base.dtModel.Columns.Add("NON_MANDATORY_DATE", typeof(DateTime));
            base.dtModel.Columns.Add("DEFAULT_DATE", typeof(DateTime));
            base.dtModel.Columns.Add("NON_DEFAULT_DATE", typeof(DateTime));
           
            //Added by Praveen Kumar 19/08/2010
            base.dtModel.Columns.Add("DISPLAY_ON_CLAIM", typeof(int));
            base.dtModel.Columns.Add("CLAIM_RESERVE_APPLY", typeof(int));

            //Added by Ankit Goel
            base.dtModel.Columns.Add("IS_MAIN", typeof(int));
            base.dtModel.Columns.Add("SUB_LOB_ID", typeof(int));

            //Added by shikha
            base.dtModel.Columns.Add("COV_TYPE_ABBR", typeof(string));
            base.dtModel.Columns.Add("SUSEP_COV_CODE", typeof(int));


		}
		#region Database schema details
		// model for database field RANK(int)
		public int RANK
		{
			get
			{
				return base.dtModel.Rows[0]["RANK"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["RANK"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RANK"] = value;
			}
		}
		// model for database field COVERAGE_TYPE(string)
		public string COVERAGE_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["COVERAGE_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["COVERAGE_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["COVERAGE_TYPE"] = value;
			}
		}
		// model for database field IS_MANDATORY(int)
		public int IS_MANDATORY
		{
			get
			{
				return base.dtModel.Rows[0]["IS_MANDATORY"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["IS_MANDATORY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["IS_MANDATORY"] = value;
			}
		}
		// model for database field INCLUDED(double)
		public double INCLUDED
		{
			get
			{
				return base.dtModel.Rows[0]["INCLUDED"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["INCLUDED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INCLUDED"] = value;
			}
		}
		// model for database field COV_ID(int)
		public int COV_ID
		{
			get
			{
				return base.dtModel.Rows[0]["COV_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["COV_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COV_ID"] = value;
			}
		}
		// model for database field COV_REF_CODE(string)
		public string COV_REF_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["COV_REF_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["COV_REF_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["COV_REF_CODE"] = value;
			}
		}
		// model for database field COV_CODE(string)
		public string COV_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["COV_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["COV_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["COV_CODE"] = value;
			}
		}
		// model for database field COV_DES(string)
		public string COV_DES
		{
			get
			{
				return base.dtModel.Rows[0]["COV_DES"] == DBNull.Value ? "" : base.dtModel.Rows[0]["COV_DES"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["COV_DES"] = value;
			}
		}
		// model for database field STATE_ID(int)
		public int STATE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["STATE_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["STATE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["STATE_ID"] = value;
			}
		}
		// model for database field LOB_ID(int)
		public int LOB_ID
		{
			get
			{
				return base.dtModel.Rows[0]["LOB_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LOB_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LOB_ID"] = value;
			}
		}
		// model for database field COV_ID(int)
		public bool IS_DEFAULT
		{
			get
			{
				return Convert.ToBoolean(base.dtModel.Rows[0]["IS_DEFAULT"]);
			}
			set
			{
				base.dtModel.Rows[0]["IS_DEFAULT"] = value;
			}
		}
		// model for database field TYPE(int)
		public int TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["TYPE"] = value;
			}
		}
		// model for database field PURPOSE(int)
		public int PURPOSE
		{
			get
			{
				return base.dtModel.Rows[0]["PURPOSE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PURPOSE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PURPOSE"] = value;
			}
		}
		// model for database field LIMIT_TYPE(int)
		public int LIMIT_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["LIMIT_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LIMIT_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LIMIT_TYPE"] = value;
			}
		}
		// model for database field DEDUCTIBLE_TYPE(int)
		public int DEDUCTIBLE_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["DEDUCTIBLE_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["DEDUCTIBLE_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DEDUCTIBLE_TYPE"] = value;
			}
		}
		// model for database field IsLimitApplicable(int)
		public int IsLimitApplicable
		{
			get
			{
				return base.dtModel.Rows[0]["IsLimitApplicable"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["IsLimitApplicable"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["IsLimitApplicable"] = value;
			}
		}
		// model for database field IsDeductApplicable(int)
		public int IsDeductApplicable
		{
			get
			{
				return base.dtModel.Rows[0]["IsDeductApplicable"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["IsDeductApplicable"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["IsDeductApplicable"] = value;
			}
		}

		public DateTime EFFECTIVE_FROM_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["EFFECTIVE_FROM_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["EFFECTIVE_FROM_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EFFECTIVE_FROM_DATE"] = value;
			}
		}

		public DateTime EFFECTIVE_TO_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["EFFECTIVE_TO_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["EFFECTIVE_TO_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EFFECTIVE_TO_DATE"] = value;
			}
		}

		public DateTime DISABLED_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["DISABLED_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["DISABLED_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DISABLED_DATE"] = value;
			}
		}
		public int ISADDDEDUCTIBLE_APP
		{
			get
			{
				return base.dtModel.Rows[0]["ISADDDEDUCTIBLE_APP"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ISADDDEDUCTIBLE_APP"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ISADDDEDUCTIBLE_APP"] = value;
			}
		}
		public int ADDDEDUCTIBLE_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["ADDDEDUCTIBLE_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ADDDEDUCTIBLE_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ADDDEDUCTIBLE_TYPE"] = value;
			}
		}

		//ADDED BY SWARUP
		public int REINSURANCE_LOB
		{
			get
			{
				return base.dtModel.Rows[0]["REINSURANCE_LOB"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["REINSURANCE_LOB"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REINSURANCE_LOB"] = value;
			}
		}

		public int REINSURANCE_COV
		{
			get
			{
				return base.dtModel.Rows[0]["REINSURANCE_COV"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["REINSURANCE_COV"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REINSURANCE_COV"] = value;
			}
		}

		public int ASLOB
		{
			get
			{
				return base.dtModel.Rows[0]["ASLOB"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ASLOB"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ASLOB"] = value;
			}
		}

		public int REINSURANCE_CALC
		{
			get
			{
				return base.dtModel.Rows[0]["REINSURANCE_CALC"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["REINSURANCE_CALC"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REINSURANCE_CALC"] = value;
			}
		}

		public string FORM_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["FORM_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["FORM_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["FORM_NUMBER"] = value;
			}
		}

		public int REIN_REPORT_BUCK
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_REPORT_BUCK"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["REIN_REPORT_BUCK"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REIN_REPORT_BUCK"] = value;
			}
		}
/// <summary>
/// 
/// </summary>
		public int COMM_VEHICLE
		{
			get
			{
				return base.dtModel.Rows[0]["COMM_VEHICLE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["COMM_VEHICLE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COMM_VEHICLE"] = value;
			}
		}
		public int COMM_REIN_COV_CAT
		{
			get
			{
				return base.dtModel.Rows[0]["COMM_REIN_COV_CAT"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["COMM_REIN_COV_CAT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COMM_REIN_COV_CAT"] = value;
			}
		}
		public int REIN_ASLOB
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_ASLOB"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["REIN_ASLOB"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REIN_ASLOB"] = value;
			}
		}
		public int COMM_CALC
		{
			get
			{
				return base.dtModel.Rows[0]["COMM_CALC"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["COMM_CALC"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COMM_CALC"] = value;
			}
		}
		public int REIN_REPORT_BUCK_COMM
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_REPORT_BUCK_COMM"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["REIN_REPORT_BUCK_COMM"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REIN_REPORT_BUCK_COMM"] = value;
			}
		}

		public string IS_SYSTEM_GENERAED
		{
			get
			{
				return base.dtModel.Rows[0]["IS_SYSTEM_GENERAED"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IS_SYSTEM_GENERAED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_SYSTEM_GENERAED"] = value;
			}
		}
        public DateTime MANDATORY_DATE
        {
            get
            {
                return base.dtModel.Rows[0]["MANDATORY_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["MANDATORY_DATE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["MANDATORY_DATE"] = value;
            }
        }
        public DateTime NON_MANDATORY_DATE
        {
            get
            {
                return base.dtModel.Rows[0]["NON_MANDATORY_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["NON_MANDATORY_DATE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["NON_MANDATORY_DATE"] = value;
            }
        }
        public DateTime DEFAULT_DATE
        {
            get
            {
                return base.dtModel.Rows[0]["DEFAULT_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["DEFAULT_DATE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["DEFAULT_DATE"] = value;
            }
        }
        public DateTime NON_DEFAULT_DATE
        {
            get
            {
                return base.dtModel.Rows[0]["NON_DEFAULT_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["NON_DEFAULT_DATE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["NON_DEFAULT_DATE"] = value;
            }
        }
        //Added by Praveen Kumar (19/08/2010)
        // model for database field DISPLAY_ON_CLAIM(int)
        public int DISPLAY_ON_CLAIM
        {
            get
            {
                return base.dtModel.Rows[0]["DISPLAY_ON_CLAIM"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["DISPLAY_ON_CLAIM"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["DISPLAY_ON_CLAIM"] = value;
            }
        }
        // model for database field CLAIM_RESERVE_APPLY(int)
        public int CLAIM_RESERVE_APPLY
        {
            get
            {
                return base.dtModel.Rows[0]["CLAIM_RESERVE_APPLY"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CLAIM_RESERVE_APPLY"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["CLAIM_RESERVE_APPLY"] = value;
            }
        }
		#endregion
        //Ankit Added Is_main
        public int IS_MAIN
        {
            get
            {
                return base.dtModel.Rows[0]["IS_MAIN"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["IS_MAIN"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["IS_MAIN"] = value;
            }
        }
        public int SUB_LOB_ID
        {
            get
            {
                return base.dtModel.Rows[0]["SUB_LOB_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["SUB_LOB_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["SUB_LOB_ID"] = value;
            }
        }

        public string COV_TYPE_ABBR
        {
            get
            {
                return base.dtModel.Rows[0]["COV_TYPE_ABBR"] == DBNull.Value ? "" : base.dtModel.Rows[0]["COV_TYPE_ABBR"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["COV_TYPE_ABBR"] = value;
            }
        }

        public int SUSEP_COV_CODE
        {
            get
            {
                return base.dtModel.Rows[0]["SUSEP_COV_CODE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["SUSEP_COV_CODE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["SUSEP_COV_CODE"] = value;
            }
        }
	}
}
