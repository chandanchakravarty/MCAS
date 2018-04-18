/******************************************************************************************
	<Author					: - > Anurag Verma	
	<Start Date				: -	> March 25,2005
	<End Date				: - >
	<Description			: - > This is a model class that represents mnt_default_value_list table
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - > 13/06/2005    
	<Modified By			: - > Anurag Verma
	<Purpose				: - > Adding APP_ID and APP_VERSION_ID  
    
*******************************************************************************************/


using System;

namespace Cms.Model.Application.PriorLoss
{
	/// <summary>
	/// Summary description for ClsPriorLossInfo.
	/// </summary>
    public class ClsPriorLossInfo : Cms.Model.ClsCommonModel
    {
        private const string APP_PRIOR_LOSS_INFO = "APP_PRIOR_LOSS_INFO";
        public ClsPriorLossInfo()
        {
            base.dtModel.TableName = "APP_PRIOR_LOSS_INFO";		// setting table name for data table that holds property values.
            this.AddColumns();								// add columns of the database table APP_PRIOR_LOSS_INFO
            base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
        }
        private void AddColumns()
        {
            base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));        
            base.dtModel.Columns.Add("LOSS_ID",typeof(int));
            base.dtModel.Columns.Add("OCCURENCE_DATE",typeof(DateTime));
            //base.dtModel.Columns.Add("CLAIM_DATE",typeof(DateTime));
            base.dtModel.Columns.Add("LOB",typeof(string));
            base.dtModel.Columns.Add("LOSS_TYPE",typeof(int));
            base.dtModel.Columns.Add("AMOUNT_PAID",typeof(double));
          //  base.dtModel.Columns.Add("AMOUNT_RESERVED",typeof(double));
            base.dtModel.Columns.Add("CLAIM_STATUS",typeof(string));
           // base.dtModel.Columns.Add("LOSS_DESC",typeof(string));
            base.dtModel.Columns.Add("REMARKS",typeof(string));
            //base.dtModel.Columns.Add("MOD",typeof(string));
           // base.dtModel.Columns.Add("LOSS_RUN",typeof(string));
           // base.dtModel.Columns.Add("CAT_NO",typeof(string));
            base.dtModel.Columns.Add("CLAIMID",typeof(string));
            base.dtModel.Columns.Add("APP_ID",typeof(int));
            base.dtModel.Columns.Add("APP_VERSION_ID",typeof(int));

			base.dtModel.Columns.Add("APLUS_REPORT_ORDERED",typeof(int));
			base.dtModel.Columns.Add("DRIVER_ID",typeof(int));
			base.dtModel.Columns.Add("DRIVER_NAME",typeof(string));
			base.dtModel.Columns.Add("RELATIONSHIP",typeof(int));
			base.dtModel.Columns.Add("CLAIMS_TYPE",typeof(int));
			base.dtModel.Columns.Add("AT_FAULT",typeof(int));
			base.dtModel.Columns.Add("CHARGEABLE",typeof(int));
			base.dtModel.Columns.Add("LOSS_LOCATION",typeof(string));
			base.dtModel.Columns.Add("CAUSE_OF_LOSS",typeof(string));
			base.dtModel.Columns.Add("POLICY_NUM",typeof(string));
			base.dtModel.Columns.Add("LOSS_CARRIER",typeof(string));
			base.dtModel.Columns.Add("OTHER_DESC",typeof(string));
			base.dtModel.Columns.Add("NAME_MATCH",typeof(int));//Done for Itrack Issue 6723 on 27 Nov 09


        }
        #region Database schema details
        // model for database field CUSTOMER_ID(int)
        public int CUSTOMER_ID
        {
            get
            {
                return base.dtModel.Rows[0]["CUSTOMER_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CUSTOMER_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["CUSTOMER_ID"] = value;
            }
        }
        // model for database field LOSS_ID(int)
        public int LOSS_ID
        {
            get
            {
                return base.dtModel.Rows[0]["LOSS_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LOSS_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["LOSS_ID"] = value;
            }
        }

        // model for database field EXPDT_ID(int)
        public int APP_VERSION_ID
        {
            get
            {
                return base.dtModel.Rows[0]["APP_VERSION_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["APP_VERSION_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["APP_VERSION_ID"] = value;
            }
        }
     




        // model for database field EXPDT_ID(int)
        public int APP_ID
        {
            get
            {
                return base.dtModel.Rows[0]["APP_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["APP_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["APP_ID"] = value;
            }
        }
     




        // model for database field OCCURENCE_DATE(DateTime)
        public DateTime OCCURENCE_DATE
        {
            get
            {
                return base.dtModel.Rows[0]["OCCURENCE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["OCCURENCE_DATE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["OCCURENCE_DATE"] = value;
            }
        }
        // model for database field CLAIM_DATE(DateTime)
        public DateTime CLAIM_DATE
        {
            get
            {
                return base.dtModel.Rows[0]["CLAIM_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["CLAIM_DATE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["CLAIM_DATE"] = value;
            }
        }
        // model for database field LOB(string)
        public string LOB
        {
            get
            {
                return base.dtModel.Rows[0]["LOB"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LOB"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["LOB"] = value;
            }
        }
        // model for database field LOSS_TYPE(int)
        public int LOSS_TYPE
        {
            get
            {
                return base.dtModel.Rows[0]["LOSS_TYPE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LOSS_TYPE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["LOSS_TYPE"] = value;
            }
        }
        // model for database field AMOUNT_PAID(double)
        public double AMOUNT_PAID
        {
            get
            {
                return base.dtModel.Rows[0]["AMOUNT_PAID"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["AMOUNT_PAID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["AMOUNT_PAID"] = value;
            }
        }
        // model for database field AMOUNT_RESERVED(double)
        public double AMOUNT_RESERVED
        {
            get
            {
                return base.dtModel.Rows[0]["AMOUNT_RESERVED"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["AMOUNT_RESERVED"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["AMOUNT_RESERVED"] = value;
            }
        }
        // model for database field CLAIM_STATUS(string)
        public string CLAIM_STATUS
        {
            get
            {
                return base.dtModel.Rows[0]["CLAIM_STATUS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CLAIM_STATUS"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CLAIM_STATUS"] = value;
            }
        }
        // model for database field LOSS_DESC(string)
        public string LOSS_DESC
        {
            get
            {
                return base.dtModel.Rows[0]["LOSS_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LOSS_DESC"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["LOSS_DESC"] = value;
            }
        }
        // model for database field REMARKS(string)
        public string REMARKS
        {
            get
            {
                return base.dtModel.Rows[0]["REMARKS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["REMARKS"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["REMARKS"] = value;
            }
        }
        // model for database field MOD(string)
        public string MOD
        {
            get
            {
                return base.dtModel.Rows[0]["MOD"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MOD"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["MOD"] = value;
            }
        }
        // model for database field LOSS_RUN(string)
        public string LOSS_RUN
        {
            get
            {
                return base.dtModel.Rows[0]["LOSS_RUN"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LOSS_RUN"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["LOSS_RUN"] = value;
            }
        }
        // model for database field CAT_NO(string)
        public string CAT_NO
        {
            get
            {
                return base.dtModel.Rows[0]["CAT_NO"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CAT_NO"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CAT_NO"] = value;
            }
        }
        // model for database field CLAIMID(string)
        public string CLAIMID
        {
            get
            {
                return base.dtModel.Rows[0]["CLAIMID"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CLAIMID"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CLAIMID"] = value;
            }
        }
		//ARC 19 May, 2005 Commented following 5 properties which are not 
		//required here, because they are already declared in 
		//base class i.e. Cms.Model.ClsCommonModel
		#region Commented by Arun
//        // model for database field IS_ACTIVE(string)
//        public string IS_ACTIVE
//        {
//            get
//            {
//                return base.dtModel.Rows[0]["IS_ACTIVE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_ACTIVE"].ToString();
//            }
//            set
//            {
//                base.dtModel.Rows[0]["IS_ACTIVE"] = value;
//            }
//        }
//        // model for database field CREATED_BY(int)
//        public int CREATED_BY
//        {
//            get
//            {
//                return base.dtModel.Rows[0]["CREATED_BY"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CREATED_BY"].ToString());
//            }
//            set
//            {
//                base.dtModel.Rows[0]["CREATED_BY"] = value;
//            }
//        }
//        // model for database field CREATED_DATETIME(DateTime)
//        public DateTime CREATED_DATETIME
//        {
//            get
//            {
//                return base.dtModel.Rows[0]["CREATED_DATETIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["CREATED_DATETIME"].ToString());
//            }
//            set
//            {
//                base.dtModel.Rows[0]["CREATED_DATETIME"] = value;
//            }
//        }
//        // model for database field MODIFIED_BY(int)
//        public int MODIFIED_BY
//        {
//            get
//            {
//                return base.dtModel.Rows[0]["MODIFIED_BY"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["MODIFIED_BY"].ToString());
//            }
//            set
//            {
//                base.dtModel.Rows[0]["MODIFIED_BY"] = value;
//            }
//        }
//        // model for database field LAST_UPDATED_DATETIME(DateTime)
//        public DateTime LAST_UPDATED_DATETIME
//        {
//            get
//            {
//                return base.dtModel.Rows[0]["LAST_UPDATED_DATETIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["LAST_UPDATED_DATETIME"].ToString());
//            }
//            set
//            {
//                base.dtModel.Rows[0]["LAST_UPDATED_DATETIME"] = value;
//            }
//        }
		#endregion Commented by Arun

		// model for database field APLUS_REPORT_ORDERED(int)
		public int APLUS_REPORT_ORDERED
		{
			get
			{
				return base.dtModel.Rows[0]["APLUS_REPORT_ORDERED"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["APLUS_REPORT_ORDERED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APLUS_REPORT_ORDERED"] = value;
			}
		}
		//Done for Itrack Issue 6723 on 27 Nov 09
		// model for database field NAME_MATCH(int)
		public int NAME_MATCH
		{
			get
			{
				return base.dtModel.Rows[0]["NAME_MATCH"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["NAME_MATCH"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NAME_MATCH"] = value;
			}
		}
		// model for database field DRIVER_ID(int)
		public int DRIVER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DRIVER_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_ID"] = value;
			}
		}
		// model for database field DRIVER_NAME(string)
		public string DRIVER_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_NAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_NAME"] = value;
			}
		}
		// model for database field RELATIONSHIP(int)
		public int RELATIONSHIP
		{
			get
			{
				return base.dtModel.Rows[0]["RELATIONSHIP"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["RELATIONSHIP"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RELATIONSHIP"] = value;
			}
		}
		// model for database field CLAIMS_TYPE(int)
		public int CLAIMS_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["CLAIMS_TYPE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CLAIMS_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CLAIMS_TYPE"] = value;
			}
		}
		// model for database field AT_FAULT(int)
		public int AT_FAULT
		{
			get
			{
				return base.dtModel.Rows[0]["AT_FAULT"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["AT_FAULT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AT_FAULT"] = value;
			}
		}
		// model for database field CHARGEABLE(int)
		public int CHARGEABLE
		{
			get
			{
				return base.dtModel.Rows[0]["CHARGEABLE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CHARGEABLE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CHARGEABLE"] = value;
			}
		}		
		// model for database field LOSS_LOCATION(string)
		public string LOSS_LOCATION
		{
			get
			{
				return base.dtModel.Rows[0]["LOSS_LOCATION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LOSS_LOCATION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOSS_LOCATION"] = value;
			}
		}
		// model for database field CAUSE_OF_LOSS(string)
		public string CAUSE_OF_LOSS
		{
			get
			{
				return base.dtModel.Rows[0]["CAUSE_OF_LOSS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CAUSE_OF_LOSS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CAUSE_OF_LOSS"] = value;
			}
		}
		// model for database field POLICY_NUM(string)
		public string POLICY_NUM
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_NUM"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["POLICY_NUM"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_NUM"] = value;
			}
		}
		// model for database field LOSS_CARRIER(string)
		public string LOSS_CARRIER
		{
			get
			{
				return base.dtModel.Rows[0]["LOSS_CARRIER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LOSS_CARRIER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOSS_CARRIER"] = value;
			}
		}
		// model for database field OTHER_DESC(string)
		public string OTHER_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["OTHER_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["OTHER_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["OTHER_DESC"] = value;
			}
		}

		#endregion
    }
}
